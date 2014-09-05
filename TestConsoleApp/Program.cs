using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using ModelSoft.Core;
using ModelSoft.Framework.DomainObjects;
using ModelSoft.Framework.Logging;
using ModelSoft.Framework.NLogAddapter;
using NLog.Targets;

namespace TestConsoleApp
{
    class Program
    {
        private const string FirstTestModelUrl = @"http://www.imodelsoft.com/models/tests/FirstModel.model";

        static void Main(string[] args)
        {
            LogManager.SetService(new NLogChannelProvider());

            //TestLogAttributes();
            TestModelElementBase();

            Console.WriteLine(new string('-', 80));
            var memLog = (MemoryTarget)NLog.LogManager.Configuration.FindTargetByName("memory");
            foreach (var log in memLog.Logs)
            {
                Console.WriteLine(log);
            }

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to continue ...");
                Console.ReadKey();
            }

        }

        private static void TestModelElementBase()
        {
            //var package = new Package();
            //var classes = ModelElementBase.GetRegisteredClasses().ToList();

            //foreach (var aClass in classes)
            //{
            //    Console.WriteLine(aClass);
            //    foreach (var property in aClass.Properties)
            //    {
            //        Console.WriteLine("    {0}", property);
            //    }
            //}
        }

        private static void TestLogAttributes()
        {
            var result = TestLog("{0}={1}+{2}", 1234, 7, 3, 4);
            Console.WriteLine("Result: {0}", result);
        }

        [Log(ChannelNameType.Type, ShowFullTypeName=true, ShowParametersName=true, ShowParametersType=true)]
        private static string TestLog(string format, int integer, params object[] array)
        {
            var str = string.Format(format, array);
            str = "[" + integer + "]" + str;
            return str;
        }

    }
}
