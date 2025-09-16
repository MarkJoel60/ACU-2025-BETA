// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXFieldValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXFieldValue : IPXValue
{
  private readonly string _Field;

  public PXFieldValue(string field)
  {
    this._Field = field;
    if (string.IsNullOrEmpty(field))
      return;
    string[] strArray = field.Split('.');
    if (strArray.Length > 1)
    {
      this.TableName = strArray[0];
      this.FieldName = strArray[1];
    }
    else
      this.FieldName = strArray[0];
  }

  public PXFieldValue(string tableName, string fieldName)
  {
    this._Field = $"{tableName}.{fieldName}";
    this.TableName = tableName;
    this.FieldName = fieldName;
  }

  internal string TableName { get; private set; }

  internal string FieldName { get; private set; }

  public override object[] GetParameters(Func<string, IPXValue> paramHandler, bool tryInline = false)
  {
    return new object[0];
  }

  public override PXDataValue[] GetDataValueParameters(
    Func<string, IPXValue> paramHandler,
    bool tryInline = false)
  {
    return new PXDataValue[0];
  }

  public override SQLExpression GetExpression(
    Func<string, SQLExpression> paramHandler,
    bool tryInline = false)
  {
    return paramHandler(this._Field);
  }

  public override string ToString() => this._Field;

  public override bool Equals(IPXValue other)
  {
    PXFieldValue pxFieldValue = other as PXFieldValue;
    if (other == null || pxFieldValue == null)
      return false;
    return this == other || string.Equals(this._Field, pxFieldValue._Field, StringComparison.OrdinalIgnoreCase);
  }
}
