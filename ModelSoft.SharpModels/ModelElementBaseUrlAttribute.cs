using System;

namespace ModelSoft.SharpModels
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ModelElementBaseUrlAttribute : Attribute
    {
        private readonly string _baseUrl;

        public string BaseUrl
        {
            get { return _baseUrl; }
        }

        public ModelElementBaseUrlAttribute(string baseUrl)
        {
            if (baseUrl == null) throw new ArgumentNullException("baseUrl");
            if (!Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute)) throw new ArgumentException(string.Format("Malformed base uri: {0}", baseUrl), "baseUrl");
            if (!baseUrl.EndsWith("/"))
                throw new ArgumentException(string.Format("Base url must end with character '/': {0}", baseUrl));
            var uri = new Uri(baseUrl, UriKind.Absolute);

            _baseUrl = baseUrl;
        }
    }
}