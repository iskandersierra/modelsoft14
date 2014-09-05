using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework.IoC
{
  public static class ServiceComposerExtensions
  {
    public static IServiceComposer RegisterTypes(
      this IServiceComposer composer,
      IEnumerable<Type> services,
      Func<Type, IEnumerable<Type>> fromInterfaces,
      Func<Type, ServiceLifeTime> lifetimeFromService = null,
      Func<Type, Type, string> keyFromInterfaceAndService = null,
      Func<Type, IEnumerable<InjectMemberInfo>> injectMembersFromService = null)
    {
      composer.RequireNotNull("composer");
      services.RequireNotNull("services");
      fromInterfaces.RequireNotNull("fromInterfaces");

      if (keyFromInterfaceAndService == null)
        keyFromInterfaceAndService = (interf, serv) => null;
      if (lifetimeFromService == null)
        lifetimeFromService = (serv) => ServiceLifeTime.Singleton;
      if (injectMembersFromService == null)
        injectMembersFromService = (serv) => Inject.GetAttributedInjectMembers(serv);
        //injectMembersFromService = (serv) => Enumerable.Empty<InjectMemberInfo>();
#if TRACE
      services = services.ToList();
#endif
      foreach (var service in services)
      {
        var lifetime = lifetimeFromService(service);
        var interfaces = fromInterfaces(service).ToList();
        var injectMembers = injectMembersFromService(service).ToArray();

        foreach (var @interface in interfaces)
        {
          var key = keyFromInterfaceAndService(@interface, service);

          composer.RegisterType(@interface, @service, key, lifetime, injectMembers);
        }
      }

      return composer;
    }

    public static IServiceComposer RegisterType(
      this IServiceComposer composer,
      Type service,
      IEnumerable<Type> fromInterfaces,
      ServiceLifeTime lifetime = ServiceLifeTime.Singleton,
      Func<Type, string> keyFromInterface = null,
      IEnumerable<InjectMemberInfo> injectMembers = null)
    {
      composer.RequireNotNull("composer");
      service.RequireNotNull("services");
      fromInterfaces.RequireNotNull("fromInterfaces");

      composer.RegisterTypes(
        Seq.Build(service),
        s => fromInterfaces,
        s => lifetime,
        keyFromInterface == null ? (Func<Type, Type, string>)null : (i, s) => keyFromInterface(i),
        injectMembers == null ? (Func<Type, IEnumerable<InjectMemberInfo>>)null : s => injectMembers
      );
      return composer;
    }

    public static IServiceComposer RegisterType<TService>(
      this IServiceComposer composer,
      IEnumerable<Type> fromInterfaces,
      ServiceLifeTime lifetime = ServiceLifeTime.Singleton,
      Func<Type, string> keyFromInterface = null,
      Action<InjectOn<TService>> onInject = null)
    {
      composer.RequireNotNull("composer");
      fromInterfaces.RequireNotNull("fromInterfaces");

      var service = typeof(TService);

      IEnumerable<InjectMemberInfo> injectMembers = null;
      if (onInject != null)
      {
        var injector = new InjectOn<TService>();
        onInject(injector);
        injectMembers = injector.Infos;
      }

      composer.RegisterType(
        service,
        fromInterfaces,
        lifetime,
        keyFromInterface,
        injectMembers.GetAsEmptyIfNull().ToArray()
      );
      return composer;
    }
  }
}
