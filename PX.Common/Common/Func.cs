// Decompiled with JetBrains decompiler
// Type: PX.Common.Func
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Common;

public static class Func
{
  /// <summary>Returns function that returns its parameter</summary>
  public static Func<T, T> Id<T>() => Func.\u0003\u2009<T>.\u0002();

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TOut> Memorize<TOut>(Func<TOut> originalFunc)
  {
    Func.\u0008<TOut> obj = new Func.\u0008<TOut>();
    obj.\u0002 = originalFunc != null ? Lazy.By<TOut>(originalFunc) : throw new ArgumentNullException(nameof (originalFunc));
    return new Func<TOut>(obj.\u0002);
  }

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TIn">Function input parameter type</typeparam>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TIn, TOut> Memorize<TIn, TOut>(Func<TIn, TOut> originalFunc)
  {
    Func.\u0003<TIn, TOut> obj = new Func.\u0003<TIn, TOut>();
    obj.\u000E = originalFunc;
    if (obj.\u000E == null)
      throw new ArgumentNullException(nameof (originalFunc));
    obj.\u0002 = new Dictionary<TIn, TOut>();
    return new Func<TIn, TOut>(obj.\u0002);
  }

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TIn1">Function first input parameter type</typeparam>
  /// <typeparam name="TIn2">Function second input parameter type</typeparam>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TIn1, TIn2, TOut> Memorize<TIn1, TIn2, TOut>(
    Func<TIn1, TIn2, TOut> originalFunc)
  {
    Func.\u000F<TIn1, TIn2, TOut> obj = new Func.\u000F<TIn1, TIn2, TOut>();
    obj.\u0002 = originalFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (originalFunc));
    obj.\u000E = new Dictionary<Tuple<TIn1, TIn2>, TOut>();
    obj.\u0006 = new Func<Tuple<TIn1, TIn2>, TOut>(obj.\u0002);
    return new Func<TIn1, TIn2, TOut>(obj.\u000E);
  }

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TIn1">Function first input parameter type</typeparam>
  /// <typeparam name="TIn2">Function second input parameter type</typeparam>
  /// <typeparam name="TIn3">Function third input parameter type</typeparam>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TIn1, TIn2, TIn3, TOut> Memorize<TIn1, TIn2, TIn3, TOut>(
    Func<TIn1, TIn2, TIn3, TOut> originalFunc)
  {
    Func.\u0005<TIn1, TIn2, TIn3, TOut> obj = new Func.\u0005<TIn1, TIn2, TIn3, TOut>();
    obj.\u0002 = originalFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (originalFunc));
    obj.\u000E = new Dictionary<Tuple<TIn1, TIn2, TIn3>, TOut>();
    obj.\u0006 = new Func<Tuple<TIn1, TIn2, TIn3>, TOut>(obj.\u0002);
    return new Func<TIn1, TIn2, TIn3, TOut>(obj.\u000E);
  }

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TIn1">Function first input parameter type</typeparam>
  /// <typeparam name="TIn2">Function second input parameter type</typeparam>
  /// <typeparam name="TIn3">Function third input parameter type</typeparam>
  /// <typeparam name="TIn4">Function fourth input parameter type</typeparam>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TIn1, TIn2, TIn3, TIn4, TOut> Memorize<TIn1, TIn2, TIn3, TIn4, TOut>(
    Func<TIn1, TIn2, TIn3, TIn4, TOut> originalFunc)
  {
    Func.\u0002\u2009<TIn1, TIn2, TIn3, TIn4, TOut> obj = new Func.\u0002\u2009<TIn1, TIn2, TIn3, TIn4, TOut>();
    obj.\u0002 = originalFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (originalFunc));
    obj.\u000E = new Dictionary<Tuple<TIn1, TIn2, TIn3, TIn4>, TOut>();
    obj.\u0006 = new Func<Tuple<TIn1, TIn2, TIn3, TIn4>, TOut>(obj.\u0002);
    return new Func<TIn1, TIn2, TIn3, TIn4, TOut>(obj.\u000E);
  }

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TIn1">Function first input parameter type</typeparam>
  /// <typeparam name="TIn2">Function second input parameter type</typeparam>
  /// <typeparam name="TIn3">Function third input parameter type</typeparam>
  /// <typeparam name="TIn4">Function fourth input parameter type</typeparam>
  /// <typeparam name="TIn5">Function fifth input parameter type</typeparam>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> Memorize<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(
    Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> originalFunc)
  {
    Func.\u000E\u2009<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> obj = new Func.\u000E\u2009<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>();
    obj.\u0002 = originalFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (originalFunc));
    obj.\u000E = new Dictionary<Tuple<TIn1, TIn2, TIn3, TIn4, TIn5>, TOut>();
    obj.\u0006 = new Func<Tuple<TIn1, TIn2, TIn3, TIn4, TIn5>, TOut>(obj.\u0002);
    return new Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(obj.\u000E);
  }

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TIn1">Function first input parameter type</typeparam>
  /// <typeparam name="TIn2">Function second input parameter type</typeparam>
  /// <typeparam name="TIn3">Function third input parameter type</typeparam>
  /// <typeparam name="TIn4">Function fourth input parameter type</typeparam>
  /// <typeparam name="TIn5">Function fifth input parameter type</typeparam>
  /// <typeparam name="TIn6">Function sixth input parameter type</typeparam>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> Memorize<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(
    Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> originalFunc)
  {
    Func.\u0006\u2009<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> obj = new Func.\u0006\u2009<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>();
    obj.\u0002 = originalFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (originalFunc));
    obj.\u000E = new Dictionary<Tuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>, TOut>();
    obj.\u0006 = new Func<Tuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>, TOut>(obj.\u0002);
    return new Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(obj.\u000E);
  }

  /// <summary>
  /// Apply memorizing behavior to the function,
  /// which means that original function would be
  /// called only once for a certain input parameter
  /// </summary>
  /// <typeparam name="TIn1">Function first input parameter type</typeparam>
  /// <typeparam name="TIn2">Function second input parameter type</typeparam>
  /// <typeparam name="TIn3">Function third input parameter type</typeparam>
  /// <typeparam name="TIn4">Function fourth input parameter type</typeparam>
  /// <typeparam name="TIn5">Function fifth input parameter type</typeparam>
  /// <typeparam name="TIn6">Function sixth input parameter type</typeparam>
  /// <typeparam name="TIn7">Function seventh input parameter type</typeparam>
  /// <typeparam name="TOut">Function output result type</typeparam>
  /// <param name="originalFunc">Function</param>
  /// <returns>Function with memorizing behavior</returns>
  public static Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> Memorize<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(
    Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> originalFunc)
  {
    Func.\u0008\u2009<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> obj = new Func.\u0008\u2009<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>();
    obj.\u0002 = originalFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (originalFunc));
    obj.\u000E = new Dictionary<Tuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>, TOut>();
    obj.\u0006 = new Func<Tuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>, TOut>(obj.\u0002);
    return new Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(obj.\u000E);
  }

  private static TValue \u0002<TKey, TValue>(
    this IDictionary<TKey, TValue> _param0,
    TKey _param1,
    Func<TKey, TValue> _param2)
  {
    TValue obj1;
    if (_param0.TryGetValue(_param1, out obj1))
      return obj1;
    TValue obj2 = _param2(_param1);
    _param0.Add(_param1, obj2);
    return obj2;
  }

  /// <summary>Combines two Func delagates (AND condition)</summary>
  /// <typeparam name="TIn">Function input parameter type</typeparam>
  /// <param name="firstFunc">First function</param>
  /// <param name="secondFunc">Second function</param>
  /// <returns>Returns (firstFunc &amp;&amp; secondFunc)</returns>
  public static Func<TIn, bool> Conjoin<TIn>(Func<TIn, bool> firstFunc, Func<TIn, bool> secondFunc)
  {
    Func.\u0002<TIn> obj = new Func.\u0002<TIn>();
    obj.\u0002 = firstFunc;
    obj.\u000E = secondFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (firstFunc));
    return obj.\u000E != null ? new Func<TIn, bool>(obj.\u0002) : throw new ArgumentNullException(nameof (secondFunc));
  }

  /// <summary>Combines two Func delagates (OR condition)</summary>
  /// <typeparam name="TIn">Function input parameter type</typeparam>
  /// <param name="firstFunc">First function</param>
  /// <param name="secondFunc">Second function</param>
  /// <returns>Returns (firstFunc || secondFunc)</returns>
  public static Func<TIn, bool> Disjoin<TIn>(Func<TIn, bool> firstFunc, Func<TIn, bool> secondFunc)
  {
    Func.\u000E<TIn> obj = new Func.\u000E<TIn>();
    obj.\u0002 = firstFunc;
    obj.\u000E = secondFunc;
    if (obj.\u0002 == null)
      throw new ArgumentNullException(nameof (firstFunc));
    return obj.\u000E != null ? new Func<TIn, bool>(obj.\u0002) : throw new ArgumentNullException(nameof (secondFunc));
  }

  /// <summary>Negates the Func delegate (NOT condition)</summary>
  /// <typeparam name="TIn">Function input parameter type</typeparam>
  /// <param name="func">Original function</param>
  /// <returns>Returns !func</returns>
  public static Func<TIn, bool> Negate<TIn>(Func<TIn, bool> func)
  {
    Func.\u0006<TIn> obj = new Func.\u0006<TIn>();
    obj.\u0002 = func;
    return obj.\u0002 != null ? new Func<TIn, bool>(obj.\u0002) : throw new ArgumentNullException(nameof (func));
  }

  private sealed class \u0002<\u0002> where \u0002 : notnull
  {
    public 
    #nullable disable
    Func<\u0002, bool> \u0002;
    public Func<\u0002, bool> \u000E;

    internal bool \u0002(\u0002 _param1) => this.\u0002(_param1) && this.\u000E(_param1);
  }

  private sealed class \u0002\u2009<\u0002, \u000E, \u0006, \u0008, \u0003>
    where \u0002 : 
    #nullable enable
    notnull
    where \u000E : notnull
    where \u0006 : notnull
    where \u0008 : notnull
    where \u0003 : notnull
  {
    public 
    #nullable disable
    Func<\u0002, \u000E, \u0006, \u0008, \u0003> \u0002;
    public Dictionary<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008>, \u0003> \u000E;
    public Func<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008>, \u0003> \u0006;

    internal \u0003 \u0002(
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008> _param1)
    {
      return this.\u0002(_param1.Item1, _param1.Item2, _param1.Item3, _param1.Item4);
    }

    internal \u0003 \u000E(\u0002 _param1, \u000E _param2, \u0006 _param3, \u0008 _param4)
    {
      return this.\u000E.\u0002<Tuple<\u0002, \u000E, \u0006, \u0008>, \u0003>(Tuple.Create<\u0002, \u000E, \u0006, \u0008>(_param1, _param2, _param3, _param4), this.\u0006);
    }
  }

  private sealed class \u0003<\u0002, \u000E>
    where \u0002 : 
    #nullable enable
    notnull
    where \u000E : notnull
  {
    public 
    #nullable disable
    Dictionary<\u0002, \u000E> \u0002;
    public Func<\u0002, \u000E> \u000E;

    internal \u000E \u0002(\u0002 _param1)
    {
      return this.\u0002.\u0002<\u0002, \u000E>(_param1, this.\u000E);
    }
  }

  private static class \u0003\u2009<\u0002>
  {
    private static readonly 
    #nullable enable
    Func<\u0002, \u0002> \u0002 = new Func<\u0002, \u0002>(Func.\u0003\u2009<\u0002>.\u0002.\u0002.\u0002);

    public static Func<\u0002, \u0002> \u0002() => Func.\u0003\u2009<\u0002>.\u0002;

    [Serializable]
    private sealed class \u0002
    {
      public static readonly 
      #nullable disable
      Func.\u0003\u2009<\u0002>.\u0002 \u0002 = new Func.\u0003\u2009<\u0002>.\u0002();

      internal 
      #nullable enable
      \u0002 \u0002(\u0002 _param1) => _param1;
    }
  }

  private sealed class \u0005<\u0002, \u000E, \u0006, \u0008>
    where \u0002 : notnull
    where \u000E : notnull
    where \u0006 : notnull
    where \u0008 : notnull
  {
    public 
    #nullable disable
    Func<\u0002, \u000E, \u0006, \u0008> \u0002;
    public Dictionary<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006>, \u0008> \u000E;
    public Func<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006>, \u0008> \u0006;

    internal \u0008 \u0002(
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006> _param1) => this.\u0002(_param1.Item1, _param1.Item2, _param1.Item3);

    internal \u0008 \u000E(\u0002 _param1, \u000E _param2, \u0006 _param3)
    {
      return this.\u000E.\u0002<Tuple<\u0002, \u000E, \u0006>, \u0008>(Tuple.Create<\u0002, \u000E, \u0006>(_param1, _param2, _param3), this.\u0006);
    }
  }

  private sealed class \u0006<\u0002> where \u0002 : 
  #nullable enable
  notnull
  {
    public 
    #nullable disable
    Func<\u0002, bool> \u0002;

    internal bool \u0002(\u0002 _param1) => !this.\u0002(_param1);
  }

  private sealed class \u0006\u2009<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005>
    where \u0002 : 
    #nullable enable
    notnull
    where \u000E : notnull
    where \u0006 : notnull
    where \u0008 : notnull
    where \u0003 : notnull
    where \u000F : notnull
    where \u0005 : notnull
  {
    public 
    #nullable disable
    Func<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005> \u0002;
    public Dictionary<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008, \u0003, \u000F>, \u0005> \u000E;
    public Func<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008, \u0003, \u000F>, \u0005> \u0006;

    internal \u0005 \u0002(
      #nullable enable
      Tuple<
      #nullable disable
      \u0002, \u000E, \u0006, \u0008, \u0003, \u000F> _param1)
    {
      return this.\u0002(_param1.Item1, _param1.Item2, _param1.Item3, _param1.Item4, _param1.Item5, _param1.Item6);
    }

    internal \u0005 \u000E(
      \u0002 _param1,
      \u000E _param2,
      \u0006 _param3,
      \u0008 _param4,
      \u0003 _param5,
      \u000F _param6)
    {
      return this.\u000E.\u0002<Tuple<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F>, \u0005>(Tuple.Create<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F>(_param1, _param2, _param3, _param4, _param5, _param6), this.\u0006);
    }
  }

  private sealed class \u0008<\u0002> where \u0002 : 
  #nullable enable
  notnull
  {
    public 
    #nullable disable
    Lazy<\u0002> \u0002;

    internal \u0002 \u0002() => this.\u0002.Value;
  }

  private sealed class \u0008\u2009<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005, \u0002\u2009>
    where \u0002 : 
    #nullable enable
    notnull
    where \u000E : notnull
    where \u0006 : notnull
    where \u0008 : notnull
    where \u0003 : notnull
    where \u000F : notnull
    where \u0005 : notnull
    where \u0002\u2009 : notnull
  {
    public 
    #nullable disable
    Func<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005, \u0002\u2009> \u0002;
    public Dictionary<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005>, \u0002\u2009> \u000E;
    public Func<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005>, \u0002\u2009> \u0006;

    internal \u0002\u2009 \u0002(
      #nullable enable
      Tuple<
      #nullable disable
      \u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005> _param1)
    {
      return this.\u0002(_param1.Item1, _param1.Item2, _param1.Item3, _param1.Item4, _param1.Item5, _param1.Item6, _param1.Item7);
    }

    internal \u0002\u2009 \u000E(
      \u0002 _param1,
      \u000E _param2,
      \u0006 _param3,
      \u0008 _param4,
      \u0003 _param5,
      \u000F _param6,
      \u0005 _param7)
    {
      return this.\u000E.\u0002<Tuple<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005>, \u0002\u2009>(Tuple.Create<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F, \u0005>(_param1, _param2, _param3, _param4, _param5, _param6, _param7), this.\u0006);
    }
  }

  private sealed class \u000E<\u0002> where \u0002 : 
  #nullable enable
  notnull
  {
    public 
    #nullable disable
    Func<\u0002, bool> \u0002;
    public Func<\u0002, bool> \u000E;

    internal bool \u0002(\u0002 _param1) => this.\u0002(_param1) || this.\u000E(_param1);
  }

  private sealed class \u000E\u2009<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F>
    where \u0002 : 
    #nullable enable
    notnull
    where \u000E : notnull
    where \u0006 : notnull
    where \u0008 : notnull
    where \u0003 : notnull
    where \u000F : notnull
  {
    public 
    #nullable disable
    Func<\u0002, \u000E, \u0006, \u0008, \u0003, \u000F> \u0002;
    public Dictionary<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008, \u0003>, \u000F> \u000E;
    public Func<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E, \u0006, \u0008, \u0003>, \u000F> \u0006;

    internal \u000F \u0002(
      #nullable enable
      Tuple<
      #nullable disable
      \u0002, \u000E, \u0006, \u0008, \u0003> _param1)
    {
      return this.\u0002(_param1.Item1, _param1.Item2, _param1.Item3, _param1.Item4, _param1.Item5);
    }

    internal \u000F \u000E(
      \u0002 _param1,
      \u000E _param2,
      \u0006 _param3,
      \u0008 _param4,
      \u0003 _param5)
    {
      return this.\u000E.\u0002<Tuple<\u0002, \u000E, \u0006, \u0008, \u0003>, \u000F>(Tuple.Create<\u0002, \u000E, \u0006, \u0008, \u0003>(_param1, _param2, _param3, _param4, _param5), this.\u0006);
    }
  }

  private sealed class \u000F<\u0002, \u000E, \u0006>
    where \u0002 : 
    #nullable enable
    notnull
    where \u000E : notnull
    where \u0006 : notnull
  {
    public 
    #nullable disable
    Func<\u0002, \u000E, \u0006> \u0002;
    public Dictionary<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E>, \u0006> \u000E;
    public Func<
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E>, \u0006> \u0006;

    internal \u0006 \u0002(
    #nullable enable
    Tuple<
    #nullable disable
    \u0002, \u000E> _param1) => this.\u0002(_param1.Item1, _param1.Item2);

    internal \u0006 \u000E(\u0002 _param1, \u000E _param2)
    {
      return this.\u000E.\u0002<Tuple<\u0002, \u000E>, \u0006>(Tuple.Create<\u0002, \u000E>(_param1, _param2), this.\u0006);
    }
  }
}
