using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;
using ModelSoft.Framework;
using ModelSoft.Modeling.Definitions.Common;

namespace ModelSoft.Modeling
{
  [ContentProperty("Name")]
  public class ReferenceExtension : MarkupExtension
  {
    public ReferenceExtension()
    {
    }

    public ReferenceExtension(string name)
    {
      this.Name = name;
    }

    [ConstructorArgument("name")]
    public string Name { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      if (Name.IsWS())
        throw new ArgumentNullException("Reference name cannot be null or empty.");
      if (!this.Name.IsValidReferenceName())
        throw new FormatException("Reference name is invalid.");

      var ctx = ModelLoaderContext.Current;

      //var rootProvider = serviceProvider.GetService<IRootObjectProvider>();
      var targetService = serviceProvider.GetService<IProvideValueTarget>();
      //var model = rootProvider.RootObject as IModel;
      var target = targetService.TargetObject;
      var element = target as IModelElement;
      Action<ModelLoaderContext> action;
      Func<ModelLoaderContext, IModelElement> findElement = c =>
        {
          var namedElements = element.FindByName(this.Name).Take(2).ToArray();
          if (namedElements.Length == 0)
            throw new AmbiguousMatchException("There is no matching element named {0}".Fmt(Name));
          if (namedElements.Length == 2)
            throw new AmbiguousMatchException("There is an ambiguous match for element named {0}".Fmt(Name));
          return namedElements[0];
        };
      if (target is IModelElementCollection)
      {
        var collection = (IModelElementCollection)target;
        var position = collection.Count;
        element = collection.Owner;
        action = c =>
          {
            var found = findElement(c);
            collection[position] = found;
          };
      }
      else
      {
        var prop = targetService.TargetProperty as PropertyInfo;
        action = c => 
        {
          var found = findElement(c);
          prop.SetValue(target, found);
        };
      }
      ctx.RegisterAfterLoadAction(action);

      return null;
    }
  }
}
