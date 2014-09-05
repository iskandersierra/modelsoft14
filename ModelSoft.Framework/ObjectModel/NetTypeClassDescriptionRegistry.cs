using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.ObjectModel
{
  public class NetTypeClassDescriptionRegistry :
    FreezableBase,
    INetTypeClassDescriptionRegistry
  {
    public NetTypeClassDescriptionRegistry()
    {
      registry = new Dictionary<Type, NetTypeClassDescription>();

      ClassDescriptionSetup setup;
      var baseType = GetNetTypeClassHierarchyBase(out setup);
      baseType.RequireNotNull("baseType");
      baseType.IsClass.RequireCondition("baseType", @"Type ""{0}"" is not a class".Fmt(baseType.FullName));
      if (setup == null)
        setup = CreateSetup(baseType);

      var result = new NetTypeClassDescription(this, baseType, null, setup);
      registry.Add(baseType, result);
      classHierarchyBase = result;
    }

    #region [ ClassDescriptionRegistry ]
    private readonly NetTypeClassDescription classHierarchyBase;
    private readonly Dictionary<Type, NetTypeClassDescription> registry;
    private readonly object registryLock = new object();

    public IEnumerable<IClassDescription> RegisteredClasses
    {
      get 
      {
        // lock? ToList?
        return registry.Values.AsEnumerable<IClassDescription>();
      }
    }

    public IClassDescription ClassHierarchyBase
    {
      get { return classHierarchyBase; }
    }

    public IClassDescription GetClassDescription(Type type)
    {
      type.RequireNotNull("type");

      IClassDescription result;
      if (TryGetClassDescription(type, out result))
        return result;
      throw new ArgumentException(@"Type ""{0}"" is not registered.".Fmt(type.FullName));
    }

    public bool TryGetClassDescription(Type type, out IClassDescription registeredClass)
    {
      type.RequireNotNull("type");

      if (!IsFrozen)
      {
        lock (registryLock)
        {
          return this.TryGetClassDescriptionNoLock(type, out registeredClass);
        }
      }
      else
      {
        return this.TryGetClassDescriptionNoLock(type, out registeredClass);
      }
    }

    public IClassDescription RegisterClass(Type type, ClassDescriptionSetup setup = null)
    {
      IClassDescription result;
      if (TryRegisterClass(type, out result, setup))
        return result;
      throw new ArgumentException(@"Cannot register type ""{0}""".Fmt(type.FullName));
    }

    public bool TryRegisterClass(Type type, out IClassDescription registeredClass, ClassDescriptionSetup setup = null)
    {
      type.RequireNotNull("type");
      CheckModifyFrozen();

      lock (registryLock)
      {
        return this.TryRegisterClassNoLock(type, out registeredClass, setup);
      }
    }

    private bool TryRegisterClassNoLock(Type type, out IClassDescription registeredClass, ClassDescriptionSetup setup)
    {
      NetTypeClassDescription result;
      if (registry.TryGetValue(type, out result))
      {
        registeredClass = result;
        return false;
      }

      type.IsClass.RequireCondition("type", @"Type ""{0}"" is not a class".Fmt(type.FullName));
      classHierarchyBase.type.IsAssignableFrom(type).RequireCondition(@"Type ""{0}"" do not inherite from type ""{1}"", hence cannot be registered.".Fmt(type.FullName, classHierarchyBase.type.FullName));

      IClassDescription baseClass;
      if (!TryGetClassDescription(type.BaseType, out baseClass))
        throw new ArgumentException(@"Type ""{0}"" cannot be registered because its base type ""{1}"" is not registered yet.".Fmt(type.FullName, type.BaseType.FullName));

      if (setup == null)
        setup = CreateSetup(type);
      result = new NetTypeClassDescription(this, type, baseClass, setup);
      registry.Add(type, result);

      registeredClass = result;
      return true;
    }

    private bool TryGetClassDescriptionNoLock(Type type, out IClassDescription registeredClass)
    {
      NetTypeClassDescription result;
      var found = registry.TryGetValue(type, out result);
      registeredClass = result;
      return found;
    }

    protected virtual Type GetNetTypeClassHierarchyBase(out ClassDescriptionSetup setup)
    {
      setup = null;
      return typeof(object);
    }

    protected virtual ClassDescriptionSetup CreateSetup(Type type)
    {
      var result = new ClassDescriptionSetup
      {
        Name = type.Name,
      };
      return result;
    }

    protected override void OnFreeze()
    {
      foreach (var freezableClass in RegisteredClasses.OfType<IFreezable>())
      {
        freezableClass.Freeze();
      }
      base.OnFreeze();
    }
    #endregion

    #region [ NetTypeClassDescription ]
    private class NetTypeClassDescription :
      FreezableBase,
      IClassDescription
    {
      internal readonly NetTypeClassDescriptionRegistry registry;
      internal readonly Type type;
      internal readonly string name;
      internal readonly string resourceName;
      internal readonly Func<string, string> friendlyNameFunc;
      internal readonly IClassDescription baseClass;

      public NetTypeClassDescription(NetTypeClassDescriptionRegistry registry, Type type, IClassDescription baseClass, ClassDescriptionSetup setup)
      {
        registry.RequireNotNull("registry");
        type.RequireNotNull("type");
        setup.RequireNotNull("setup");

        this.baseClass = baseClass;
        this.registry = registry;
        this.type = type;
        this.name = setup.Name;
        this.resourceName = setup.ResourceName;
        this.friendlyNameFunc = setup.GetFriendlyNameFunc();
      }

      public IClassDescriptionRegistry Registry
      {
        get { return registry; }
      }

      public string Name
      {
        get { return name; }
      }

      public string ResourceName
      {
        get { return resourceName; }
      }

      public string FriendlyName
      {
        get { return friendlyNameFunc(resourceName); }
      }

      public IClassDescription BaseClass
      {
        get { return baseClass; }
      }

      public IEnumerable<IPropertyDescription> Properties
      {
        get { throw new NotImplementedException(); }
      }

      public IObjectData CreateInstance(Type type, IObjectDataHost host)
      {
        throw new NotImplementedException();
      }

      public void RegisterProperty(IPropertyDescription propertyDescription)
      {
        throw new NotImplementedException();
      }

      public bool TryRegisterProperty(IPropertyDescription propertyDescription)
      {
        throw new NotImplementedException();
      }

      public override string ToString()
      {
        return Name;
      }
    }
    #endregion

  }
}
