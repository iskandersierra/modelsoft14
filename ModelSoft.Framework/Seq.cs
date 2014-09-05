namespace ModelSoft.Framework
{
  using global::System.ComponentModel;
  using global::System.Collections.Generic;
  using global::System;

  /// <summary>
  /// Contains methods for generating sequences (IEnumerables)
  /// </summary>
  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public static partial class Seq
  {
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(params T[] args)
    {
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    #region [ With 1 enumerable(s) ]
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    #endregion [ With 1 enumerable(s) ]
    #region [ With 2 enumerable(s) ]
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(T value00, T value01, T value02, T value03, IEnumerable<T> enumerable0, T value10, T value11, T value12, T value13, IEnumerable<T> enumerable1, T value20, T value21, T value22, T value23, params T[] args)
    {
      yield return value00;
      
      yield return value01;
      
      yield return value02;
      
      yield return value03;
      
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      yield return value10;
      
      yield return value11;
      
      yield return value12;
      
      yield return value13;
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      yield return value20;
      
      yield return value21;
      
      yield return value22;
      
      yield return value23;
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    #endregion [ With 2 enumerable(s) ]
    #region [ Only enumerables ]
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IEnumerable<T> enumerable3, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      if(enumerable3 != null)
      foreach( var value in enumerable3)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IEnumerable<T> enumerable3, IEnumerable<T> enumerable4, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      if(enumerable3 != null)
      foreach( var value in enumerable3)
      {
        yield return value;
      }
      
      if(enumerable4 != null)
      foreach( var value in enumerable4)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IEnumerable<T> enumerable3, IEnumerable<T> enumerable4, IEnumerable<T> enumerable5, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      if(enumerable3 != null)
      foreach( var value in enumerable3)
      {
        yield return value;
      }
      
      if(enumerable4 != null)
      foreach( var value in enumerable4)
      {
        yield return value;
      }
      
      if(enumerable5 != null)
      foreach( var value in enumerable5)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IEnumerable<T> enumerable3, IEnumerable<T> enumerable4, IEnumerable<T> enumerable5, IEnumerable<T> enumerable6, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      if(enumerable3 != null)
      foreach( var value in enumerable3)
      {
        yield return value;
      }
      
      if(enumerable4 != null)
      foreach( var value in enumerable4)
      {
        yield return value;
      }
      
      if(enumerable5 != null)
      foreach( var value in enumerable5)
      {
        yield return value;
      }
      
      if(enumerable6 != null)
      foreach( var value in enumerable6)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IEnumerable<T> enumerable3, IEnumerable<T> enumerable4, IEnumerable<T> enumerable5, IEnumerable<T> enumerable6, IEnumerable<T> enumerable7, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      if(enumerable3 != null)
      foreach( var value in enumerable3)
      {
        yield return value;
      }
      
      if(enumerable4 != null)
      foreach( var value in enumerable4)
      {
        yield return value;
      }
      
      if(enumerable5 != null)
      foreach( var value in enumerable5)
      {
        yield return value;
      }
      
      if(enumerable6 != null)
      foreach( var value in enumerable6)
      {
        yield return value;
      }
      
      if(enumerable7 != null)
      foreach( var value in enumerable7)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IEnumerable<T> enumerable3, IEnumerable<T> enumerable4, IEnumerable<T> enumerable5, IEnumerable<T> enumerable6, IEnumerable<T> enumerable7, IEnumerable<T> enumerable8, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      if(enumerable3 != null)
      foreach( var value in enumerable3)
      {
        yield return value;
      }
      
      if(enumerable4 != null)
      foreach( var value in enumerable4)
      {
        yield return value;
      }
      
      if(enumerable5 != null)
      foreach( var value in enumerable5)
      {
        yield return value;
      }
      
      if(enumerable6 != null)
      foreach( var value in enumerable6)
      {
        yield return value;
      }
      
      if(enumerable7 != null)
      foreach( var value in enumerable7)
      {
        yield return value;
      }
      
      if(enumerable8 != null)
      foreach( var value in enumerable8)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    /// <summary>
    /// Generates a sequence from a set of values or enumerations.
    /// </summary>
    public static IEnumerable<T> Build<T>(IEnumerable<T> enumerable0, IEnumerable<T> enumerable1, IEnumerable<T> enumerable2, IEnumerable<T> enumerable3, IEnumerable<T> enumerable4, IEnumerable<T> enumerable5, IEnumerable<T> enumerable6, IEnumerable<T> enumerable7, IEnumerable<T> enumerable8, IEnumerable<T> enumerable9, params T[] args)
    {
      if(enumerable0 != null)
      foreach( var value in enumerable0)
      {
        yield return value;
      }
      
      if(enumerable1 != null)
      foreach( var value in enumerable1)
      {
        yield return value;
      }
      
      if(enumerable2 != null)
      foreach( var value in enumerable2)
      {
        yield return value;
      }
      
      if(enumerable3 != null)
      foreach( var value in enumerable3)
      {
        yield return value;
      }
      
      if(enumerable4 != null)
      foreach( var value in enumerable4)
      {
        yield return value;
      }
      
      if(enumerable5 != null)
      foreach( var value in enumerable5)
      {
        yield return value;
      }
      
      if(enumerable6 != null)
      foreach( var value in enumerable6)
      {
        yield return value;
      }
      
      if(enumerable7 != null)
      foreach( var value in enumerable7)
      {
        yield return value;
      }
      
      if(enumerable8 != null)
      foreach( var value in enumerable8)
      {
        yield return value;
      }
      
      if(enumerable9 != null)
      foreach( var value in enumerable9)
      {
        yield return value;
      }
      
      foreach(var item in args)
      {
        yield return item;
      }
    }
    
    #endregion [ Only enumerables ]
    #region [ From source and funcs ]
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1)
    {
      yield return item1(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2)
    {
      yield return item1(obj);
      yield return item2(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3, Func<TSource, T> item4)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
      yield return item4(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3, Func<TSource, T> item4, Func<TSource, T> item5)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
      yield return item4(obj);
      yield return item5(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3, Func<TSource, T> item4, Func<TSource, T> item5, Func<TSource, T> item6)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
      yield return item4(obj);
      yield return item5(obj);
      yield return item6(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3, Func<TSource, T> item4, Func<TSource, T> item5, Func<TSource, T> item6, Func<TSource, T> item7)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
      yield return item4(obj);
      yield return item5(obj);
      yield return item6(obj);
      yield return item7(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3, Func<TSource, T> item4, Func<TSource, T> item5, Func<TSource, T> item6, Func<TSource, T> item7, Func<TSource, T> item8)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
      yield return item4(obj);
      yield return item5(obj);
      yield return item6(obj);
      yield return item7(obj);
      yield return item8(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3, Func<TSource, T> item4, Func<TSource, T> item5, Func<TSource, T> item6, Func<TSource, T> item7, Func<TSource, T> item8, Func<TSource, T> item9)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
      yield return item4(obj);
      yield return item5(obj);
      yield return item6(obj);
      yield return item7(obj);
      yield return item8(obj);
      yield return item9(obj);
    }
    public static IEnumerable<T> BuildSeq<TSource, T>(this TSource obj, Func<TSource, T> item1, Func<TSource, T> item2, Func<TSource, T> item3, Func<TSource, T> item4, Func<TSource, T> item5, Func<TSource, T> item6, Func<TSource, T> item7, Func<TSource, T> item8, Func<TSource, T> item9, Func<TSource, T> item10)
    {
      yield return item1(obj);
      yield return item2(obj);
      yield return item3(obj);
      yield return item4(obj);
      yield return item5(obj);
      yield return item6(obj);
      yield return item7(obj);
      yield return item8(obj);
      yield return item9(obj);
      yield return item10(obj);
    }
    #endregion [ From source and funcs ]
  }
}

