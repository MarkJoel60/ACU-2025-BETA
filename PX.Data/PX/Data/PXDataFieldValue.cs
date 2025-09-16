// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataFieldValue : PXDataField
{
  public readonly PXDbType ValueType;
  public readonly int? ValueLength;
  public readonly object Value;
  public readonly PXComp Comp;
  internal bool CheckResultOnly;

  public int OpenBrackets { get; set; }

  public int CloseBrackets { get; set; }

  public bool OrOperator { get; set; }

  public PXDataFieldValue(string fieldName, object value)
    : this((SQLExpression) new Column(fieldName), value)
  {
  }

  public PXDataFieldValue(SQLExpression fieldName, object value)
    : this(fieldName, PXDbType.Unspecified, new int?(), value, PXComp.EQ)
  {
  }

  public PXDataFieldValue(string fieldName, PXDbType valueType, object value)
    : this((SQLExpression) new Column(fieldName), valueType, value)
  {
  }

  public PXDataFieldValue(SQLExpression fieldName, PXDbType valueType, object value)
    : this(fieldName, valueType, new int?(), value, PXComp.EQ)
  {
  }

  public PXDataFieldValue(string fieldName, object value, PXComp comp)
    : this((SQLExpression) new Column(fieldName), value, comp)
  {
  }

  public PXDataFieldValue(SQLExpression fieldName, object value, PXComp comp)
    : this(fieldName, PXDbType.Unspecified, new int?(), value, comp)
  {
  }

  public PXDataFieldValue(string fieldName, PXDbType valueType, int? valueLength, object value)
    : this((SQLExpression) new Column(fieldName), valueType, valueLength, value)
  {
  }

  public PXDataFieldValue(
    SQLExpression fieldName,
    PXDbType valueType,
    int? valueLength,
    object value)
    : this(fieldName, valueType, valueLength, value, PXComp.EQ)
  {
  }

  public PXDataFieldValue(
    string fieldName,
    PXDbType valueType,
    int? valueLength,
    object value,
    PXComp comp)
    : this((SQLExpression) new Column(fieldName), valueType, valueLength, value, comp)
  {
  }

  public PXDataFieldValue(
    SQLExpression fieldName,
    PXDbType valueType,
    int? valueLength,
    object value,
    PXComp comp)
    : base(fieldName)
  {
    this.ValueType = valueType;
    this.Value = value;
    this.ValueLength = valueLength;
    this.Comp = comp;
  }

  internal override PXDataField copyAndRename(string newName)
  {
    return (PXDataField) new PXDataFieldValue(newName, this.ValueType, this.ValueLength, this.Value, this.Comp)
    {
      CloseBrackets = this.CloseBrackets,
      OpenBrackets = this.OpenBrackets,
      OrOperator = this.OrOperator,
      CheckResultOnly = this.CheckResultOnly
    };
  }
}
