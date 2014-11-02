using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework
{
    public interface IIndentedFormatted
    {
        void Format(IndentedTextWriter writer);
    }

    public static class IndentedFormattedExtensions
    {
        public static string Format(this IIndentedFormatted formatted)
        {
            using (var stream = new StringWriter())
            {
                using (var writer = new IndentedTextWriter(stream, "    "))
                {
                    formatted.Format(writer);
                    writer.Flush();
                    return stream.ToString();
                }
            }
        }
    }
}
