// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXSimpleValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data.Description.GI;

public class PXSimpleValue : IPXValue
{
  private object _Value;
  private PXDbType _DataType = PXDbType.Unspecified;

  public PXSimpleValue(object value)
  {
    if (value is PXFieldState pxFieldState)
    {
      this._Value = pxFieldState.Value;
      if (!(pxFieldState.DataType != (System.Type) null))
        return;
      this._DataType = System.Type.GetTypeCode(pxFieldState.DataType).TypeCodeToPXDbType();
    }
    else
      this._Value = value;
  }

  public PXSimpleValue(object value, PXDbType dataType)
  {
    this._Value = value;
    this._DataType = dataType;
  }

  public override object[] GetParameters(Func<string, IPXValue> paramHandler, bool tryInline = false)
  {
    if (tryInline)
      return (object[]) null;
    return new object[1]{ this._Value };
  }

  public override PXDataValue[] GetDataValueParameters(
    Func<string, IPXValue> paramHandler,
    bool tryInline = false)
  {
    if (tryInline)
      return (PXDataValue[]) null;
    return new PXDataValue[1]
    {
      this._DataType == PXDbType.Unspecified ? new PXDataValue(this._Value) : new PXDataValue(this._DataType, this._Value)
    };
  }

  public override SQLExpression GetExpression(
    Func<string, SQLExpression> paramHandler,
    bool tryInline = false)
  {
    return tryInline ? (SQLExpression) new SQLConst(this._Value) : paramHandler((string) null);
  }

  public override string ToString() => this._Value?.ToString() ?? string.Empty;

  public override bool Equals(IPXValue other)
  {
    PXSimpleValue pxSimpleValue = other as PXSimpleValue;
    if (other == null || pxSimpleValue == null)
      return false;
    if (this == other || this._Value == pxSimpleValue._Value)
      return true;
    return this._Value != null && this._Value.Equals(pxSimpleValue._Value);
  }
}
