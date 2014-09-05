using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelSoft.Framework.Properties;

namespace ModelSoft.Framework
{
  public interface IItemConverter
  {
    object Convert(object item);
    bool TryConvert(object item, out object result);
  }

  public interface IItemConverter<in TSource, TTarget> 
    : IItemConverter
  {
    TTarget Convert(TSource item);
    bool TryConvert(TSource item, out TTarget result);
  }

  public abstract class ItemConverterBase<TSource, TTarget> 
    : IItemConverter<TSource, TTarget>
  {
    #region IItemConverter<TSource,TTarget> Members

    public virtual TTarget Convert(TSource item)
    {
      TTarget result;
      if (TryConvert(item, out result))
        return result;
      throw new FormatException(Resources.ExMsg_ItemConverterBase_ErrorConvertingSource);
    }

    public abstract bool TryConvert(TSource item, out TTarget result);

    #endregion

    #region IItemConverter Members

    object IItemConverter.Convert(object item)
    {
      return this.Convert((TSource)item);
    }

    bool IItemConverter.TryConvert(object item, out object result)
    {
      TSource sourceItem = (TSource)item;
      TTarget targetResult;
      if (this.TryConvert(sourceItem, out targetResult))
      {
        result = targetResult;
        return true;
      }
      result = targetResult;
      return false;
    }

    #endregion
  }
}
