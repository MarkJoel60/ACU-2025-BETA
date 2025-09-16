// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXInValue`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXInValue<T> : IPXValue
{
  private readonly T[] _parameters;

  public PXInValue(IEnumerable<T> values) => this._parameters = values.ToArray<T>();

  public override object[] GetParameters(Func<string, IPXValue> paramHandler, bool tryInline = false)
  {
    return this._parameters.Cast<object>().ToArray<object>();
  }

  public override PXDataValue[] GetDataValueParameters(
    Func<string, IPXValue> paramHandler,
    bool tryInline = false)
  {
    return ((IEnumerable<T>) this._parameters).Select<T, PXDataValue>((Func<T, PXDataValue>) (p => new PXDataValue((object) p))).ToArray<PXDataValue>();
  }

  public override SQLExpression GetExpression(
    Func<string, SQLExpression> onParameter,
    bool tryInline = false)
  {
    SQLExpression expression = SQLExpression.None();
    foreach (SQLExpression r in ((IEnumerable<T>) this._parameters).Select<T, SQLExpression>((Func<T, SQLExpression>) (p => onParameter((string) null))))
      expression = expression.Seq(r);
    return expression;
  }

  public override bool Equals(IPXValue other)
  {
    if (!(other is PXInValue<T> pxInValue))
      return false;
    if (this == pxInValue)
      return true;
    return this._parameters.Length == pxInValue._parameters.Length && ((IEnumerable<T>) this._parameters).SequenceEqual<T>((IEnumerable<T>) pxInValue._parameters);
  }
}
