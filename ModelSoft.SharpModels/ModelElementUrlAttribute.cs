using System;
using ModelSoft.Framework;
using ModelSoft.Framework.Reflection;

namespace ModelSoft.SharpModels
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ModelElementUrlAttribute : Attribute
    {
        private readonly string _url;

        public string Url
        {
            get { return _url; }
        }

        public ModelElementUrlAttribute(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                if (url.EndsWith("/")) throw new ArgumentException(string.Format("Element url must not end with character '/': {0}", url));
                var uri = new Uri(url, UriKind.Absolute);
                if (!string.IsNullOrEmpty(uri.Query) || !string.IsNullOrEmpty(uri.Fragment))
                    throw new ArgumentException(string.Format("Element url must not have query or fragment part: {0}", url));
                var segments = uri.Segments;
                segments[segments.Length - 1].ValidateCSharpIdentifier(throwOnError: true);
            }
            else if (Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                url.ValidateCSharpIdentifier(throwOnError: true);
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid model element url: {0}", url));
            }

            _url = url;
        }

        public static string GetElementUrl(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException("interfaceType");

            var urlAttr = interfaceType.GetAttribute<ModelElementUrlAttribute>();
            if (urlAttr == null) return null;
            if (Uri.IsWellFormedUriString(urlAttr.Url, UriKind.Absolute))
                return urlAttr.Url;
            var asmAttr = interfaceType.Assembly.GetAttribute<ModelElementBaseUrlAttribute>();
            if (asmAttr == null) return null;
            var result = asmAttr.BaseUrl + urlAttr.Url;
            return result;
        }
    }
}