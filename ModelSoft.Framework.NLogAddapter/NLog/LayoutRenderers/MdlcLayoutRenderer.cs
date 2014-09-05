using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
    [LayoutRenderer("mdlc")]
    public class MdlcLayoutRenderer : LayoutRenderer
    {
        [RequiredParameter]
        [DefaultParameter]
        public string Item { get; set; }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            string msg = MappedDiagnosticsLogicalContext.Get(this.Item);
            builder.Append(msg);
        }

        public static void RegisterFactory()
        {
            NLog.Config.ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("mdlc", typeof(MdlcLayoutRenderer));
        }
    }
}
