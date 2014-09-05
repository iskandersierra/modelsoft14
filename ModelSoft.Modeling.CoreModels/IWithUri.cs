using System;
using ModelSoft.Framework.DomainObjects;

namespace ModelSoft.Core
{
    public interface IWithUri : IModelElement
    {
        Uri Uri { get; set; }

        //[Computed(typeof(WithUriImplementation), "Compute_AbsolteUri")]
        Uri AbsolteUri { get; }

        [Container]
        IUriSpace ContainerSpace { get; }
    }

    public static class WithUriImplementation
    {
        public static Uri Compute_AbsolteUri(IWithUri self)
        {
            if (self.ContainerSpace == null)
                return self.Uri;
            return new Uri(self.ContainerSpace.AbsolteUri, self.Uri);
        }
    }
}