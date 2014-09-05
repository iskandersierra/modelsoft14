using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace ModelSoft.Framework
{
  public delegate bool TryConvertDelegate<TSource, TTarget>(TSource item, out TTarget result);

  public class DelegateItemConverter<TSource, TTarget> : 
    ItemConverterBase<TSource, TTarget>
  {
    Func<TSource, TTarget> innerConverter;
    TryConvertDelegate<TSource, TTarget> innerTryConverter;

    public DelegateItemConverter(Func<TSource, TTarget> innerConverter)
    {
      Contract.Requires(innerConverter != null);

      this.innerConverter = innerConverter;
    }

    public DelegateItemConverter(TryConvertDelegate<TSource, TTarget> innerTryConverter)
    {
      Contract.Requires(innerTryConverter != null);

      this.innerTryConverter = innerTryConverter;
    }

    public override TTarget Convert(TSource item)
    {
      if (innerConverter != null)
        return innerConverter(item);
      TTarget result;
      if (TryConvert(item, out result))
        return result;
      throw new FormatException("Unable to convert item");
    }

    public override bool TryConvert(TSource item, out TTarget result)
    {
      if (innerTryConverter != null)
        return innerTryConverter(item, out result);
      try
      {
        result = innerConverter(item);
        return true;
      }
      catch (FormatException)
      {
        result = default(TTarget);
        return false;
      }
    }
  }
}
