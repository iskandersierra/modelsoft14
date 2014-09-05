using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelSoft.Core;
using ModelSoft.Framework.Serialization;

namespace ModelSoft.Framework.DomainObjects
{
    [TestClass]
    public class DomainModelElementBaseTests
    {
        public TestContext TestContext { get; set; }
        private UnityContainer Container { get; set; }
        private IServiceLocator ServiceLocator { get; set; }

        [TestInitialize]
        public void DomainModelElementBase_TestInitialize()
        {
            Container = new UnityContainer();

            ServiceLocator = new UnityServiceLocator(Container);

            Container.RegisterInstance<IServiceLocator>(ServiceLocator, new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IUnityContainer>(Container, new ContainerControlledLifetimeManager());


            #region [ Configure IDomainModelingServices ]

            // IStringSerializer: XML, mscorlib
            Container.RegisterType<IStringSerializer, XmlConvertStringSerializer>(
                "XML", new ContainerControlledLifetimeManager());
            Container.RegisterType<IStringSerializer, MSCoreLibStringSerializer>(
                "mscorlib", new ContainerControlledLifetimeManager());

            // IClrTypeStringSerializerFactory: <default>, XML, mscorlib
            Container .RegisterType<IClrTypeStringSerializerFactory, StringSerializerToClrTypeStringSerializerAdapterFactory>
                (
                "XML", new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IStringSerializer>("XML"))
                );
            Container.RegisterType<IClrTypeStringSerializerFactory, StringSerializerToClrTypeStringSerializerAdapterFactory>
                (
                "mscorlib", new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IStringSerializer>("mscorlib"), 
                    new ResolvedParameter<IClrTypeStringSerializerFactory>("XML"))
                );
            Container.RegisterType<IClrTypeStringSerializerFactory, AttributedClrTypeStringSerializerFactory>
                (
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IClrTypeStringSerializerFactory>("mscorlib"))
                );

            // IDomainModelingServices: <default>

            #endregion

        }

        [TestMethod]
        public void DomainModelElementBase_Inherits()
        {
            //var package = new Package();
            //var classes = ModelElementBase.GetRegisteredClasses().ToList();

            //Assert.IsTrue(classes.Count() >= 2, "classes.Count() >= 2");
            //var packageClass = classes.FirstOrDefault(e => e.ClrType == typeof (Package));
            //Assert.IsNotNull(packageClass, "packageClass");
            //var mebClass = classes.FirstOrDefault(e => e.ClrType == typeof(ModelElementBase));
            //Assert.IsNotNull(mebClass, "mebClass");
            //Assert.AreSame(mebClass, packageClass.BaseClass, "mebClass == packageClass.BaseClass");
        }
    }
}