// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDataValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDataValue
{
  public readonly PXDbType ValueType;
  public readonly int? ValueLength;
  public readonly object Value;

  public PXDataValue(object value)
    : this(PXDbType.Unspecified, new int?(), value)
  {
  }

  public PXDataValue(PXDbType valueType, object value)
    : this(valueType, new int?(), value)
  {
  }

  public PXDataValue(PXDbType valueType, int? valueLength, object value)
  {
    this.ValueType = valueType;
    this.Value = value;
    this.ValueLength = valueLength;
  }

  public override string ToString() => $"{this.GetType().Name} ({this.ValueType}) = {this.Value}";
}
