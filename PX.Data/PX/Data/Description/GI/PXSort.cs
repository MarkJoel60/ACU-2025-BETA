// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXSort
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
[DebuggerDisplay("ORDER BY {Table != null? Table.Alias + '.' +(DataField as PXFieldValue).FieldName: (DataField as PXCalcedValue).ToString(),nq} {Order.ToString(),nq}")]
public class PXSort : IEquatable<PXSort>, ICloneable
{
  public PXTable Table;
  public IPXValue DataField;
  public SortOrder Order;

  public bool Equals(PXSort other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    return (this.Table == other.Table || this.Table != null && this.Table.Equals(other.Table)) && (this.DataField == other.DataField || this.DataField != null && this.DataField.Equals(other.DataField)) && this.Order == other.Order;
  }

  public object Clone()
  {
    return (object) new PXSort()
    {
      Table = (this.Table == null ? (PXTable) null : (PXTable) this.Table.Clone()),
      DataField = this.DataField,
      Order = this.Order
    };
  }
}
