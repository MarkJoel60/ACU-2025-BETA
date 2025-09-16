// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldRestrict
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataFieldRestrict : PXDataFieldParam
{
  public readonly PXComp Comp;
  internal bool CheckResultOnly;
  public static PXDataFieldRestrict OperationSwitchAllowed = new PXDataFieldRestrict("CompanyID", (object) null);

  public int OpenBrackets { get; set; }

  public int CloseBrackets { get; set; }

  public bool OrOperator { get; set; }

  public PXDataFieldRestrict(string fieldName, object value)
    : this(new Column(fieldName), value)
  {
  }

  public PXDataFieldRestrict(Column column, object value)
    : base(column, value)
  {
  }

  public PXDataFieldRestrict(string fieldName, PXDbType valueType, object value)
    : this(new Column(fieldName), valueType, value)
  {
  }

  public PXDataFieldRestrict(Column column, PXDbType valueType, object value)
    : base(column, valueType, value)
  {
  }

  public PXDataFieldRestrict(string fieldName, PXDbType valueType, int? valueLength, object value)
    : this(new Column(fieldName), valueType, valueLength, value)
  {
  }

  public PXDataFieldRestrict(Column column, PXDbType valueType, int? valueLength, object value)
    : base(column, valueType, valueLength, value)
  {
  }

  public PXDataFieldRestrict(
    string fieldName,
    PXDbType valueType,
    int? valueLength,
    object value,
    PXComp comp)
    : this(new Column(fieldName), valueType, valueLength, value, comp)
  {
  }

  public PXDataFieldRestrict(
    Column column,
    PXDbType valueType,
    int? valueLength,
    object value,
    PXComp comp)
    : base(column, valueType, valueLength, value)
  {
    this.Comp = comp;
  }

  public override string ToString()
  {
    return $"{this.GetType()}: {this.Column} {this.Comp} {(this.Value == null ? (object) "(null)" : (object) this.StringValue)}";
  }

  internal PXDataFieldRestrict copyAndRename(Column column)
  {
    if (this.Storage != StorageBehavior.Table)
      return this;
    return new PXDataFieldRestrict(column, this.ValueType, this.ValueLength, this.Value, this.Comp)
    {
      CloseBrackets = this.CloseBrackets,
      OpenBrackets = this.OpenBrackets,
      OrOperator = this.OrOperator,
      CheckResultOnly = this.CheckResultOnly
    };
  }
}
