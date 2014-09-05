using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ModelSoft.Framework
{
  public class NotifyPropertyChangedChanging : 
    NotifyPropertyChanged, 
    INotifyPropertyChanging
  {
    public event PropertyChangingEventHandler PropertyChanging;

    protected virtual bool OnPropertyChanging(string propertyName)
    {
      if (PropertyChanging != null)
        PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
      return true;
    }
  }
}
