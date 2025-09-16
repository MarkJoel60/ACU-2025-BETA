// Decompiled with JetBrains decompiler
// Type: PX.Common.PropertyTransfer
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Common;

public static class PropertyTransfer
{
  public static TTo Transfer<TFrom, TTo>(
    TFrom source,
    TTo target,
    Action<TFrom, TTo>? additionTransfer = null)
  {
    if ((object) source == null)
      throw new ArgumentNullException(nameof (source));
    if ((object) target == null)
      throw new ArgumentNullException(nameof (target));
    if (false)
    {
      foreach (var data in ((IEnumerable<PropertyInfo>) typeof (TFrom).GetProperties()).Where<PropertyInfo>(PropertyTransfer.\u0002<TFrom, TTo>.\u000E ?? (PropertyTransfer.\u0002<TFrom, TTo>.\u000E = new Func<PropertyInfo, bool>(PropertyTransfer.\u0002<TFrom, TTo>.\u0002.\u0002))).Join(((IEnumerable<PropertyInfo>) typeof (TTo).GetProperties()).Where<PropertyInfo>(PropertyTransfer.\u0002<TFrom, TTo>.\u0006 ?? (PropertyTransfer.\u0002<TFrom, TTo>.\u0006 = new Func<PropertyInfo, bool>(PropertyTransfer.\u0002<TFrom, TTo>.\u0002.\u000E))), PropertyTransfer.\u0002<TFrom, TTo>.\u0008 ?? (PropertyTransfer.\u0002<TFrom, TTo>.\u0008 = new Func<PropertyInfo, (string, Type)>(PropertyTransfer.\u0002<TFrom, TTo>.\u0002.\u0006)), PropertyTransfer.\u0002<TFrom, TTo>.\u0003 ?? (PropertyTransfer.\u0002<TFrom, TTo>.\u0003 = new Func<PropertyInfo, (string, Type)>(PropertyTransfer.\u0002<TFrom, TTo>.\u0002.\u0008)), PropertyTransfer.\u0002<TFrom, TTo>.\u000F ?? (PropertyTransfer.\u0002<TFrom, TTo>.\u000F = new Func<PropertyInfo, PropertyInfo, \u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo>>(PropertyTransfer.\u0002<TFrom, TTo>.\u0002.\u0003))))
        data.tp.SetValue((object) target, data.sp.GetValue((object) source));
    }
    else
      target = PropertyTransfer.\u000E<TFrom, TTo>.\u000E(source, target);
    if (additionTransfer != null)
      additionTransfer(source, target);
    return target;
  }

  public static TTo? PopulateFrom<TFrom, TTo>(
    this TTo? target,
    TFrom source,
    Action<TFrom, TTo>? additionTransfer = null)
  {
    if ((object) target == null)
      return default (TTo);
    TTo target1 = target;
    return PropertyTransfer.Transfer<TFrom, TTo>(source, target1, additionTransfer);
  }

  [Serializable]
  private sealed class \u0002<\u0002, \u000E>
    where \u0002 : notnull
    where \u000E : notnull
  {
    public static readonly 
    #nullable disable
    PropertyTransfer.\u0002<\u0002, \u000E> \u0002 = new PropertyTransfer.\u0002<\u0002, \u000E>();
    public static Func<PropertyInfo, bool> \u000E;
    public static Func<PropertyInfo, bool> \u0006;
    public static Func<PropertyInfo, (string, Type)> \u0008;
    public static Func<PropertyInfo, (string, Type)> \u0003;
    public static Func<PropertyInfo, PropertyInfo, \u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo>> \u000F;

    internal bool \u0002(PropertyInfo _param1) => _param1.CanRead;

    internal bool \u000E(PropertyInfo _param1) => _param1.CanWrite;

    internal (string, Type) \u0006(PropertyInfo _param1) => (_param1.Name, _param1.PropertyType);

    internal (string, Type) \u0008(PropertyInfo _param1) => (_param1.Name, _param1.PropertyType);

    internal \u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo> \u0003(
      PropertyInfo _param1,
      PropertyInfo _param2)
    {
      return new{ sp = _param1, tp = _param2 };
    }
  }

  private sealed class \u000E<\u0002, \u000E>
  {
    private static 
    #nullable enable
    Lazy<Func<\u0002, \u000E, \u000E>> \u0002 = Lazy.By<Func<\u0002, \u000E, \u000E>>(new Func<Func<\u0002, \u000E, \u000E>>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002));

    private static Func<\u0002, \u000E, \u000E> \u0002()
    {
      PropertyTransfer.\u000E<\u0002, \u000E>.\u000E obj = new PropertyTransfer.\u000E<\u0002, \u000E>.\u000E();
      IEnumerable<\u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo>> source = ((IEnumerable<PropertyInfo>) typeof (\u0002).GetProperties()).Where<PropertyInfo>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u000E ?? (PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u000E = new Func<PropertyInfo, bool>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0002.\u0002))).Join(((IEnumerable<PropertyInfo>) typeof (\u000E).GetProperties()).Where<PropertyInfo>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0006 ?? (PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0006 = new Func<PropertyInfo, bool>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0002.\u000E))), PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0008 ?? (PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0008 = new Func<PropertyInfo, (string, Type)>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0002.\u0006)), PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0003 ?? (PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0003 = new Func<PropertyInfo, (string, Type)>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0002.\u0008)), PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u000F ?? (PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u000F = new Func<PropertyInfo, PropertyInfo, \u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo>>(PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.\u0002.\u0003)));
      obj.\u000E = Expression.Parameter(typeof (\u0002), "source");
      obj.\u0002 = Expression.Parameter(typeof (\u000E), "target");
      Func<\u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo>, Expression> selector = new Func<\u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo>, Expression>(obj.\u0002);
      return Expression.Lambda<Func<\u0002, \u000E, \u000E>>((Expression) Expression.Block(Enumerable.ToArray<Expression>(source.Select(selector).Append<Expression>((Expression) obj.\u0002))), obj.\u000E, obj.\u0002).Compile();
    }

    public static \u000E \u000E(\u0002 _param0, \u000E _param1)
    {
      return PropertyTransfer.\u000E<\u0002, \u000E>.\u0002.Value(_param0, _param1);
    }

    [Serializable]
    private sealed class \u0002
    {
      public static readonly 
      #nullable disable
      PropertyTransfer.\u000E<\u0002, \u000E>.\u0002 \u0002 = new PropertyTransfer.\u000E<\u0002, \u000E>.\u0002();
      public static Func<PropertyInfo, bool> \u000E;
      public static Func<PropertyInfo, bool> \u0006;
      public static Func<PropertyInfo, (string, Type)> \u0008;
      public static Func<PropertyInfo, (string, Type)> \u0003;
      public static Func<PropertyInfo, PropertyInfo, \u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo>> \u000F;

      internal bool \u0002(PropertyInfo _param1) => _param1.CanRead;

      internal bool \u000E(PropertyInfo _param1) => _param1.CanWrite;

      internal (string, Type) \u0006(PropertyInfo _param1) => (_param1.Name, _param1.PropertyType);

      internal (string, Type) \u0008(PropertyInfo _param1) => (_param1.Name, _param1.PropertyType);

      internal \u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo> \u0003(
        PropertyInfo _param1,
        PropertyInfo _param2)
      {
        return new{ sp = _param1, tp = _param2 };
      }
    }

    private sealed class \u000E
    {
      public ParameterExpression \u0002;
      public ParameterExpression \u000E;

      internal Expression \u0002(
        \u003C\u003Ef__AnonymousType1<PropertyInfo, PropertyInfo> _param1)
      {
        return (Expression) Expression.Assign((Expression) Expression.Property((Expression) this.\u0002, _param1.tp), (Expression) Expression.Property((Expression) this.\u000E, _param1.sp));
      }
    }
  }
}
