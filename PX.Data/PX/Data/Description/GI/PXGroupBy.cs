// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXGroupBy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
[DebuggerDisplay("GROUP BY {Table != null? Table.Alias + '.' +(DataField as PXFieldValue).FieldName: (DataField as PXCalcedValue).ToString(),nq}")]
public class PXGroupBy : ICloneable
{
  public PXTable Table;
  public IPXValue DataField;

  public object Clone()
  {
    return (object) new PXGroupBy()
    {
      Table = (this.Table == null ? (PXTable) null : (PXTable) this.Table.Clone()),
      DataField = this.DataField
    };
  }
}
