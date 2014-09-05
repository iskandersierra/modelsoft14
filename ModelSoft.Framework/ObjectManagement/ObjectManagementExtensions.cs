using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework.ObjectManagement
{
  public static class ObjectManagementExtensions
  {
    #region [ Validations ]
    public static void CheckDuplicatedObjectInCollection<T>(this IEnumerable<T> collection, string collectionName, IEqualityComparer<T> comparer = null)
    {
      collection.RequireNotNull("collection");
      collectionName.RequireNotWhitespace("collectionName");

      var duplicates = collection
        .GetDuplicates(e => e, comparer)
        .Take(1)
        .ToList();

      if (duplicates.Any())
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_DuplicatedObjectsInCollection(collectionName, "" + duplicates[0]));
    }

    public static void CheckDuplicatedPropertyInCollection<T, V>(this IEnumerable<T> collection, string collectionName, Func<T, V> property, string propertyName, IEqualityComparer<V> comparer = null)
    {
      collection.RequireNotNull("collection");
      collectionName.RequireNotWhitespace("collectionName");
      property.RequireNotNull("property");
      propertyName.RequireNotWhitespace("propertyName");

      var duplicates = collection
        .GetDuplicates(property, comparer)
        .Take(1)
        .Select(property)
        .ToList();

      if (duplicates.Any())
        throw new InvalidFreezingOperationException(FormattedResources.ExMsg_DuplicatedPropertyInCollection(propertyName, collectionName, "" + duplicates[0]));
    }

    public static void CheckDuplicatedIdentifiersInCollection<T>(this IEnumerable<T> collection, string collectionName)
      where T : IIdentifiedDescription
    {
      collection
        .Where(e => !e.Identifier.IsEmpty())
        .CheckDuplicatedPropertyInCollection(collectionName, e => e.Identifier, "Identifier");
    }

    public static void CheckDuplicatedNamesInCollection<T>(this IEnumerable<T> collection, string collectionName)
      where T : INamedDescription
    {
      collection
        .Where(e => !e.Name.IsEmpty())
        .CheckDuplicatedPropertyInCollection(collectionName, e => e.Name, "Name");
    }
    #endregion

    #region [ IClassDescription Extensions ]
    public static IEnumerable<IClassDescription> GetAllClasses(this IClassDescription aClass)
    {
      aClass.RequireNotNull("aClass");

      return aClass.DepthLastSearch(c => c.BaseClasses, true)
                   .Distinct();
    }
    #endregion
  }
}
