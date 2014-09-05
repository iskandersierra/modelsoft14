using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ModelSoft.Framework;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.Modeling.Definitions.Common
{
  public static class CommonExtensions
  {
    #region [ IModelElement ]
    public static IEnumerable<IModelElement> GetAncestorsEx(this IModelElement element, Type ofType = null, bool exactType = false)
    {
      var result = element.Unfold(e => e.ParentElement).WhileNonDuplicated(ModelSoft.Framework.ObjectEqualityComparer<IModelElement>.Default);
      if (ofType != null)
        if (exactType)
          result = result.Where(e => e.GetType() == ofType);
        else
          result = result.Where(e => ofType.IsInstanceOfType(e));
      return result;
    }

    public static IEnumerable<T> GetAncestorsEx<T>(this IModelElement element)
      where T: class, IModelElement
    {
      return element.GetAncestorsEx(typeof(T)).Cast<T>();
    }

    public static IEnumerable<ModelElementPropertyValue> GetPropertiesEx(this IModelElement element, ERelationshipType? relationshipType = null)
    {
      element.RequireNotNull("element");

      var properties = element.GetType()
        .GetProperties()
        .Where(p => !p.HasAttribute<IsHiddenPropertyAttribute>())
        .Select(p => new { Property = p, Type = p.GetAttribute<RelationshipTypeAttribute>().GetValueOr(a => a.Type, ERelationshipType.Value) });
#if TRACE
      properties = properties.ToArray();
#endif

      if (relationshipType.HasValue)
      {
        properties = properties
          .Where(p => p.Type == relationshipType.Value);
#if TRACE
        properties = properties.ToArray();
#endif
      }

      var result = properties
        .Select(p => new ModelElementPropertyValue(p.Property, p.Property.GetValue(element, null), p.Type));
#if TRACE
      result = result.ToArray();
#endif
      return result;
    }

    public static IEnumerable<IModelElement> GetChildrenEx(this IModelElement element)
    {
      element.RequireNotNull("element");

      var children = element
        .GetPropertiesEx(ERelationshipType.Content)
        .SelectMany(p => p.ModelValues);
#if TRACE
      children = children.ToArray();
#endif

      return children;
    }

    public static IEnumerable<IModelElement> GetDescendantsAndSelfEx(this IModelElement element)
    {
      element.RequireNotNull("element");

      var result = element.DepthFirstSearch(e => e.GetChildrenEx());
#if TRACE
      result = result.ToArray();
#endif

      return result;
    }

    public static string PrintModel(this IModelElement element, string indentation = "    ", string currentIndent = "", bool indentCurrent = true)
    {
      using (var writer = new StringWriter())
      {
        element.PrintModel(writer, indentation, currentIndent, indentCurrent);
        return writer.ToString();
      }
    }

    public static void PrintModel(this IModelElement element, TextWriter writer, string indentation = "    ", string currentIndent = "", bool indentCurrent = true)
    {
      if (indentCurrent)
        writer.Write(currentIndent);

      if (element == null)
        writer.WriteLine("<null>");
      else
      {
        writer.WriteLine(Print_SimpleReference(element));

        var propIndent = currentIndent + indentation;
        var valueIndent = propIndent + indentation;

        var valueProperties = element.ValueProperties.ToArray();
        var associationProperties = element.AssociationProperties.ToArray();
        var contentProperties = element.ContentProperties.ToArray();
        var containerProperties = element.ContainerProperties.ToArray();
        var maxPropNameLen = valueProperties
          .Concat(associationProperties)
          .Concat(contentProperties)
          //.Concat(containerProperties)
          .MaxOr(e => e.PropertyName.Length, 0);
        
        // Value properties
        foreach (var propVal in valueProperties)
        {
          writer.Write(propIndent);
          writer.Write(propVal.PropertyName.PadRight(maxPropNameLen, ' '));
          var strVal = Print_SimpleValue(propVal.Value);
          if (strVal.IsWS())
          {
            if (propVal.Value is IEnumerable)
            {
              writer.WriteLine();
              foreach (var item in (IEnumerable)propVal.Value)
              {
                writer.Write(valueIndent);
                writer.WriteLine(Print_SimpleValue(item));
              }
            }
            else
            {
              writer.Write(" = ");
              writer.Write("<unknown value type>");
              writer.WriteLine();
            }
          }
          else
          {
            writer.Write(" = ");
            writer.Write(strVal);
            writer.WriteLine();
          }
        }

        // Association and Container properties
        foreach (var propVal in associationProperties/*.Concat(containerProperties)*/)
        {
          var count = propVal.ModelValues.Count();
          if (count == 0)
          {
            //writer.Write(" = ");
            //writer.Write("null");
            //writer.WriteLine();
          }
          else if (count == 1)
          {
            writer.Write(propIndent);
            writer.Write(propVal.PropertyName.PadRight(maxPropNameLen, ' '));
            writer.Write(" = ");
            writer.Write(Print_SimpleReference(propVal.ModelValues.Single()));
            writer.WriteLine();
          }
          else
          {
            writer.Write(propIndent);
            writer.Write(propVal.PropertyName.PadRight(maxPropNameLen, ' '));
            writer.WriteLine();
            foreach (var item in propVal.ModelValues)
            {
              writer.Write(valueIndent);
              writer.WriteLine(Print_SimpleReference(item));
            }
          }
        }

        // Content properties
        foreach (var propVal in contentProperties)
        {
          if (propVal.ModelValues.Any())
          {
            writer.Write(propIndent);
            writer.Write(propVal.PropertyName.PadRight(maxPropNameLen, ' '));
            writer.WriteLine();
            foreach (var item in propVal.ModelValues)
            {
              item.PrintModel(writer, indentation, valueIndent, true);
            }
          }
        }
      }
    }

    private static string Print_SimpleValue(object value)
    {
      if (value == null)
        return "null";
      else if (value is string)
        return string.Format(@"@""{0}""", value.ToString().CutWithPeriods(80).AsVerbatim());
      else if (value is byte[])
        return string.Format(@"binary({0})", Convert.ToBase64String((byte[])value).CutWithPeriods(80));
      return value.ToString().CutWithPeriods(80);
    }

    private static string Print_SimpleReference(IModelElement element)
    {
      if (element == null)
        return "null";
      
      if (element is IANamedElement)
        return string.Format(@"{0} {1}", element.ModelElementTypeName, ((IANamedElement)element).Name);
      else
        return string.Format(element.ModelElementTypeName);
    }
    #endregion

    #region [ IANamespace ]
    public const string NamespaceAxisDelimiter = "::";
    //public const string GlobalAxis = "global";
    public const string ModelAxis = "model";
    public const string SelfAxis = "self";
    public const string ParentAxis = "parent";
    public const string ImportedAxis = "imported";
    public const string AncestorAxis = "ancestor";
    public const string AncestorOrSelfAxis = "ancestororself";
    public const string AllAxis = "all";

    public const string NamespaceDelimiter = ".";
    public const string NameFirstChar = @"[\p{L}\p{Nl}_]";
    public const string NameChar = @"[\p{L}\p{Nl}\p{Nd}\p{Pc}\p{Mn}\p{Mc}\p{Cf}]";

    public static readonly string NameFragment = @"{0}{1}*".Fmt(NameFirstChar, NameChar);
    public static readonly string NameFragmentGroup = @"(?<name>{0})".Fmt(NameFragment);
    public static readonly string NamespaceFragment = @"{0}(\.{0})*".Fmt(NameFragment);
    public static readonly string NamespaceFragmentGroup = @"{0}(\.{0})*".Fmt(NameFragmentGroup);
    public static readonly string AxisFragmentGroup = @"(?<axis>{1}|{2}|{3}|{4}|{5}|{6}|{7}){0}".Fmt(NamespaceAxisDelimiter, ModelAxis, SelfAxis, ParentAxis, ImportedAxis, AncestorAxis, AncestorOrSelfAxis, AllAxis);

    public static readonly Regex NameRegex = new Regex(@"^{0}$".Fmt(NameFragmentGroup), RegexOptions.Compiled);
    public static readonly Regex NamespaceRegex = new Regex(@"^{0}$".Fmt(NamespaceFragmentGroup), RegexOptions.Compiled);
    public static readonly Regex ReferenceNameRegex = new Regex(@"^({0})?{1}|{0}({1})?$".Fmt(AxisFragmentGroup, NamespaceFragmentGroup), RegexOptions.Compiled);

    public static IEnumerable<IARequiredNamedElement> GetNamedChildrenEx(this IANamespace aNamespace)
    {
      aNamespace.RequireNotNull("aNamespace");

      var result = aNamespace.Children.OfType<IARequiredNamedElement>();

      return result;
    }

    public static IEnumerable<IModelElement> GetFindByNameEx(this IModelElement anElement, string aReferenceName)
    {
      anElement.RequireNotNull("anElement");
      aReferenceName.RequireNotEmpty("aReferenceName");

      var match = ReferenceNameRegex.Match(aReferenceName);
      if (!match.Success)
        throw new FormatException("Invalid reference name format [axis::]Name1.Name2.Name3 or axis::[Name1.Name2.Name3]: " + aReferenceName);

      var axisGroup = match.Groups["axis"];
      var axis = axisGroup.Captures.Count == 1 ? axisGroup.Captures[0].Value : AllAxis;

      var nameGroup = match.Groups["name"];
      var names = nameGroup.Captures.Cast<Capture>().Select(c => c.Value).ToArray();

      var result = GetFindByNameEx(anElement, axis, names).Distinct();

      return result;
    }
    private static IEnumerable<IModelElement> GetFindByNameEx(IModelElement anElement, string axis, string[] names) // , int currentPosition
    {
      var axisElements = GetAxisElements(anElement, axis);
#if TRACE
      axisElements = axisElements.ToArray();
#endif

      var result = axisElements.SelectMany(elem => GetNamedElements(elem, names, 0));
      result = result.Distinct();
#if TRACE
      result = result.ToArray();
#endif
      return result;
      //// if there is no more names available return empty collection
      //if (currentPosition >= names.Length)
      //{
      //  yield break;
      //}
      //  // If name is global:: then only Models without parent element can be looked up
      //else if (names[currentPosition] == GlobalNamespace)
      //{
      //  if (aNamespace is IModel && aNamespace.ParentElement == null)
      //  {
      //    foreach (var element in GetFindByNameEx(aNamespace, names, currentPosition + 1))
      //    {
      //      yield return element;
      //    }
      //  }
      //  else
      //  {
      //    yield break;
      //  }
      //}
      //  // If name is a valid one search among named children
      //else
      //{
      //  var matches = aNamespace.NamedChildren.Where(n => n.Name == names[currentPosition]);
      //  // If is last name in reference return each matched element
      //  if (currentPosition == names.Length - 1)
      //  {
      //    foreach (var element in matches)
      //    {
      //      yield return element;
      //    }
      //  }
      //  else
      //  {
      //    // search in each child namespace (Models, ComplexTypes, etc.)
      //    foreach (var container in matches.OfType<IANamespace>())
      //    {
      //      foreach (var element in GetFindByNameEx(container, names, currentPosition + 1))
      //      {
      //        yield return element;
      //      }
      //    }
      //    // if it is a model look for imported models too
      //    if (aNamespace is IModel)
      //    {
      //      foreach (var importedModel in ((IModel)aNamespace).ImportedModels)
      //      {
      //        // if import alias matches current name find inside imported model
      //        if (!importedModel.Alias.IsWS() && names[currentPosition] == importedModel.Alias)
      //        {
      //          foreach (var element in GetFindByNameEx(importedModel.Model, names, currentPosition + 1))
      //          {
      //            yield return element;
      //          }
      //        }
      //        else
      //        {
      //          foreach (var element in GetFindByNameEx(importedModel.Model, names, currentPosition))
      //          {
      //            yield return element;
      //          }
      //        }
      //      }
      //    }
      //  }
      //}
    }

    private static IEnumerable<IModelElement> GetAxisElements(IModelElement anElement, string axis)
    {
      switch (axis)
      {
        case SelfAxis:
          {
            return anElement.Singleton();
          }
        case ModelAxis:
          {
            var model = anElement.RootModel;
            return model.Singleton().NotNull();
          }
        case ParentAxis:
          {
            var parent = anElement.RootModel;
            return parent.Singleton().NotNull();
          }
        case ImportedAxis:
          {
            var model = anElement.RootModel;
            IEnumerable<IModelElement> imported;
            if (model != null)
              imported = model.ImportedModels.Cast<IModelElement>();
            else
              imported = Enumerable.Empty<IModelElement>();
#if TRACE
            imported = imported.ToArray();
#endif
            return imported;
          }
        case AncestorOrSelfAxis:
          {
            var ancestors = anElement.Ancestors;
#if TRACE
            ancestors = ancestors.ToArray();
#endif
            return ancestors;
          }
        case AncestorAxis:
          {
            var ancestors = anElement.Ancestors.Skip(1);
#if TRACE
            ancestors = ancestors.ToArray();
#endif
            return ancestors;
          }
        case AllAxis:
          {
            var model = anElement.RootModel;
            IEnumerable<IModelElement> imported;
            if (model != null)
              imported = model.ImportedModels.Cast<IModelElement>();
            else
              imported = Enumerable.Empty<IModelElement>();
            var result = anElement.Singleton()
              .Concat(model.Singleton().NotNull())
              .Concat(imported);
#if TRACE
            result = result.ToArray();
#endif
            return result;
          }
        default:
          throw new NotSupportedException("Invalid axis: " + axis);
      }
    }

    private static IEnumerable<IModelElement> GetNamedElements(IModelElement anElement, string[] names, int currentPosition)
    {
      // Case 0: Nothing found
      if (anElement == null)
      {
        yield break;
      }

      // Case 1: current element is the one
      if (currentPosition == names.Length)
      {
        yield return anElement;
      }

      // Case 2: from a Namespace element
      var aNs = anElement as IANamespace;
      if (aNs != null)
      {
        var nsMatch = NamespaceRegex.Match(aNs.FullName);

        //var fullMatchSucceeded = false;
        // Try search a full name search first
        if (nsMatch.Success)
        {
          var nsNames = nsMatch.Groups["name"].Captures.Cast<Capture>().Select(e => e.Value).ToArray();
          if (nsNames.SameSequenceAs(names.Skip(currentPosition).Take(nsNames.Length)))
          {
            //fullMatchSucceeded = true;
            if (nsNames.Length == names.Length)
            {
              yield return aNs;
            }
            else
            {
              var pos = currentPosition + nsNames.Length;
              foreach (var elem in GetSearchNamespaceDirectly(aNs, names, pos))
                yield return elem;
            }
          }
        }
        // Try search the name directly
        foreach (var elem in GetSearchNamespaceDirectly(aNs, names, currentPosition))
          yield return elem;
      }

      // Case 3: Imported Models
      var impMod = anElement as IImportedModel;
      if (impMod != null)
      {
        // Search by alias
        if (!impMod.Alias.IsEmpty() && impMod.Alias == names[currentPosition])
        {
          foreach (var elem in GetSearchNamespaceDirectly(impMod.Model, names, currentPosition + 1))
            yield return elem;
        }
        // Search directly
        foreach (var elem in GetNamedElements(impMod.Model, names, currentPosition))
          yield return elem;
      }

      yield break;
    }
    private static IEnumerable<IModelElement> GetSearchNamespaceDirectly(IANamespace aNamespace, string[] names, int currentPosition)
    {
      if (aNamespace == null)
        yield break;

      if (currentPosition >= names.Length)
        yield break;

      var named = aNamespace.NamedChildren.Where(e => e.Name == names[currentPosition]).FirstOrDefault();
      foreach (var elem in GetNamedElements(named, names, currentPosition + 1))
        yield return elem;
    }

    public static bool IsValidName(this string aName)
    {
      if (aName == null) return false;
      var result = NameRegex.IsMatch(aName);
      return result;
    }

    public static bool IsValidNamespace(this string aNamespace)
    {
      if (aNamespace.IsWS()) return false;
      var result = NamespaceRegex.IsMatch(aNamespace);
      return result;
    }

    public static bool IsValidReferenceName(this string aRefName)
    {
      if (aRefName.IsWS()) return false;
      var result = ReferenceNameRegex.IsMatch(aRefName);
      return result;
    }

    public static string CombineAsNamespace(this string aNamespace, string aName)
    {
      if (!aName.IsValidName())
        throw new ArgumentOutOfRangeException("aName", "aName must be a valid identifier");
      if (aNamespace.IsWS())
        return aName;
      if (!aNamespace.IsValidNamespace())
        throw new ArgumentOutOfRangeException("aNamespace", "aNamespace must be a valid namespace");
      return string.Format("{0}.{1}", aNamespace, aName);
    }
    #endregion

    #region [ IARequiredNamedElement ]
    public static string GetFullNameEx(this IARequiredNamedElement aNamedElement)
    {
      aNamedElement.RequireNotNull("aNamedElement");

      var result = aNamedElement.Name;
      if (aNamedElement is IWithNamespace)
        result = ((IWithNamespace)aNamedElement).Namespace.CombineAsNamespace(result);

      var aNamespace = aNamedElement.ParentNamespace;
      if (aNamespace != null)
        result = aNamespace.FullName.CombineAsNamespace(aNamedElement.Name);

      return result;
    }

    public static IANamespace GetParentNamespaceEx(this IARequiredNamedElement aNamedElement)
    {
      aNamedElement.RequireNotNull("aNamedElement");

      var aNamespace = aNamedElement.ParentElement as IANamespace;

      return aNamespace;
    }
    #endregion
  }
}
