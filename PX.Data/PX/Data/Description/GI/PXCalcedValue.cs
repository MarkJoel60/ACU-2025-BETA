// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXCalcedValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXCalcedValue : IPXValue
{
  private string _Expression;
  private PXGIFormulaProcessor _Processor;

  internal PXCalcedValue(string expression, PXGIFormulaProcessor processor)
  {
    this._Expression = expression;
    this._Processor = processor;
  }

  public override object[] GetParameters(Func<string, IPXValue> paramHandler, bool tryInline = false)
  {
    List<object> result = new List<object>();
    this._Processor.TransformToExpression(this._Expression, (SyFormulaFinalDelegate) (args =>
    {
      result.AddRange((IEnumerable<object>) paramHandler(args[0]).GetParameters(paramHandler));
      return (object) SQLExpression.None();
    }));
    return result.ToArray();
  }

  public override PXDataValue[] GetDataValueParameters(
    Func<string, IPXValue> paramHandler,
    bool tryInline = false)
  {
    List<PXDataValue> result = new List<PXDataValue>();
    this._Processor.TransformToExpression(this._Expression, (SyFormulaFinalDelegate) (args =>
    {
      result.AddRange((IEnumerable<PXDataValue>) paramHandler(args[0]).GetDataValueParameters(paramHandler));
      return (object) SQLExpression.None();
    }));
    return result.ToArray();
  }

  public override SQLExpression GetExpression(
    Func<string, SQLExpression> onParameter,
    bool tryInline = false)
  {
    return this._Processor.TransformToExpression(this._Expression, (SyFormulaFinalDelegate) (args => (object) onParameter(args[0])));
  }

  public override string ToString() => this._Expression;

  public override bool Equals(IPXValue other)
  {
    PXCalcedValue pxCalcedValue = other as PXCalcedValue;
    if (other == null || pxCalcedValue == null)
      return false;
    return this == other || string.Equals(this._Expression, pxCalcedValue._Expression, StringComparison.OrdinalIgnoreCase);
  }
}
