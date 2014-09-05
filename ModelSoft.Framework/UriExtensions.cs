using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSoft.Framework
{
  public static class UriExtensions
  {
    public static Uri Combine(this Uri baseUri, Uri relativeUri)
    {
      baseUri.RequireNotNull("baseUri");
      relativeUri.RequireNotNull("relativeUri");

      if (relativeUri.IsAbsoluteUri)
        return relativeUri;

      if (baseUri.IsAbsoluteUri)
        return new Uri(baseUri, relativeUri);

      var absUri = new Uri(new Uri("http://localhost/", UriKind.Absolute), baseUri.ToString());
      absUri = new Uri(absUri, relativeUri);

      var resultStr = absUri.PathAndQuery;
      if (baseUri.ToString().StartsWith("/") && !resultStr.StartsWith("/"))
        resultStr = "/" + resultStr;

      var result = new Uri(resultStr, UriKind.Relative);

      return result;    
    }

    public static Uri Combine(this Uri baseUri, string relativeUri)
    {
      return baseUri.Combine(new Uri(relativeUri, UriKind.RelativeOrAbsolute));
    }
  }
}
