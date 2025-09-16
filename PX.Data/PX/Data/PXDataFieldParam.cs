// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataFieldParam
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXDataFieldParam
{
  public Column Column;
  public PXDbType ValueType;
  public int? ValueLength;
  public object Value;

  internal StorageBehavior Storage { get; set; }

  public PXDataFieldParam(string fieldName, object value)
    : this(fieldName, PXDbType.Unspecified, new int?(), value)
  {
  }

  public PXDataFieldParam(Column column, object value)
    : this(column, PXDbType.Unspecified, new int?(), value)
  {
  }

  public PXDataFieldParam(string fieldName, PXDbType valueType, object value)
    : this(fieldName, valueType, new int?(), value)
  {
  }

  public PXDataFieldParam(Column column, PXDbType valueType, object value)
    : this(column, valueType, new int?(), value)
  {
  }

  public PXDataFieldParam(string fieldName, PXDbType valueType, int? valueLength, object value)
    : this(new Column(fieldName), valueType, valueLength, value)
  {
  }

  public PXDataFieldParam(Column column, PXDbType valueType, int? valueLength, object value)
  {
    this.Column = column != null && !string.IsNullOrEmpty(column.Name) ? column : throw new PXArgumentException(nameof (column), "The argument cannot be null.");
    this.ValueType = valueType;
    this.Value = value;
    this.ValueLength = valueLength;
  }

  protected string StringValue
  {
    get
    {
      return this.ValueType != PXDbType.Timestamp ? this.Value.ToString() : PXSqlDatabaseProvider.TimestampToString((byte[]) this.Value);
    }
  }

  public override string ToString()
  {
    return $"{base.ToString()}: {this.Column?.ToString()} -> {(this.Value == null ? "(null)" : this.StringValue)}";
  }
}
