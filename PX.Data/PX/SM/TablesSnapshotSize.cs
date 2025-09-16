// Decompiled with JetBrains decompiler
// Type: PX.SM.TablesSnapshotSize
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class TablesSnapshotSize : TableSize
{
  [PXUIField(DisplayName = "Size in DB", Enabled = false)]
  [PXDecimal]
  public virtual Decimal? Size { get; set; }

  [PXUIField(DisplayName = "Size in DB (MB)", Enabled = false)]
  [PXDecimal(2)]
  public virtual Decimal? SizeMB
  {
    get
    {
      return new Decimal?(this.Size.HasValue ? Decimal.Round(Convert.ToDecimal(this.Size.Value) / 1048576M, 2, MidpointRounding.AwayFromZero) : 0M);
    }
    set
    {
    }
  }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Snapshot Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual 
  #nullable disable
  string SnapshotName { get; set; }

  public abstract class size : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TablesSnapshotSize.size>
  {
  }

  public abstract class sizeMB : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TablesSnapshotSize.sizeMB>
  {
  }

  public abstract class snapshotName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TablesSnapshotSize.snapshotName>
  {
  }
}
