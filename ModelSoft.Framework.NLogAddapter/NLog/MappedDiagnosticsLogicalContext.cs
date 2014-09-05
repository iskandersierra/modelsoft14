using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

// ReSharper disable once CheckNamespace
namespace NLog
{
    /// <summary>
    /// Extracted from http://www.bfcamara.com/post/67752957760/nlog-how-to-include-custom-context-variables-in-all
    /// </summary>
    public static class MappedDiagnosticsLogicalContext
    {
        private const string Key = "ModelSoft.NLog.MappedDiagnosticsLogicalContext";

        private static IDictionary<string, string> LogicalContextDict
        {
            get
            {
                var dict = CallContext.LogicalGetData(Key) as ConcurrentDictionary<string, string>;
                if (dict == null)
                {
                    dict = new ConcurrentDictionary<string, string>();
                    CallContext.LogicalSetData(Key, dict);
                }
                return dict;
            }
        }

        public static void Set(string item, string value)
        {
            LogicalContextDict[item] = value;
        }

        public static string Get(string item)
        {
            string s;

            if (!LogicalContextDict.TryGetValue(item, out s))
            {
                s = string.Empty;
            }

            return s;
        }
    }
}
