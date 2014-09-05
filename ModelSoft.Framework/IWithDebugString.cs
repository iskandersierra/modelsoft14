using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
    public static class DebugStringExtensions
    {
        public static string GetDebugString(this object obj, bool writeObjectId = false, bool indent = true, int depth = 3)
        {
            if (obj == null)
                return Resources.NullString;
            using (var stringWriter = new StringWriter())
            using (var writer = new IndentedTextWriter(stringWriter))
            {
                var printed = new Dictionary<object, int>(ObjectEqualityComparer<object>.Default);
                FillDebugString(obj, printed, writer, writeObjectId, indent, depth);
                writer.Flush();
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Writes the debug string of an object
        /// </summary>
        /// <param name="obj">The object to write the debug string from</param>
        /// <param name="printed">The objects already written to avoid writing them more than once. Associated value contains object id for reference identification</param>
        /// <param name="writer">output writer</param>
        /// <param name="writeObjectId">Indicates wether the object id must be printed</param>
        /// <param name="indent">Indicates wether output must be indented or not</param>
        /// <param name="depth">Indicate the max depth the method must go inside children before stopping. -1 indicates non stop printing</param>
        /// <returns>True if output contains line-breaks</returns>
        internal static bool FillDebugString(object obj, Dictionary<object, int> printed, IndentedTextWriter writer, bool writeObjectId, bool indent, int depth)
        {
            if (obj == null)
            {
                writer.Write(Resources.NullString);
                return false;
            }

            var type = obj.GetType();

            bool showAsDepth0 = depth == 0 || printed.ContainsKey(obj);
            ///TODO:Terminar
            return false;
        }
    }

    public enum DebugPropertyType
    {
        Plain,
        Child,
        Reference,
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
    public sealed class UseDebugStringAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DebugStringAttribute : Attribute
    {
        public DebugStringAttribute() : this(DebugPropertyType.Plain)
        {
        }

        public DebugStringAttribute(int minLevel) : this(DebugPropertyType.Plain, minLevel)
        {
        }

        public DebugStringAttribute(DebugPropertyType propertyType, int minLevel = 1)
        {
            minLevel.RequireMinValue(0, "minLevel");
            propertyType.RequireIsValidEnum("propertyType");

            PropertyType = propertyType;
            MinLevel = minLevel;
        }

        public int MinLevel { get; set; }
        public DebugPropertyType PropertyType { get; set; }
    }
}
