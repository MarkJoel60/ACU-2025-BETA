// Decompiled with JetBrains decompiler
// Type: PX.SM.UPSnapshotSize
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Snapshot Size")]
public class UPSnapshotSize : UPSnapshot
{
  [PXUIField(DisplayName = "Size in DB", Enabled = false)]
  [PXDecimal]
  public virtual Decimal? SizeInDb { get; set; }

  [PXUIField(DisplayName = "Size in DB (MB)")]
  [PXDecimal(2)]
  public virtual Decimal? SizeInDbMB
  {
    get
    {
      return new Decimal?(this.SizeInDb.HasValue ? Decimal.Round(Convert.ToDecimal(this.SizeInDb.Value) / 1048576M, 2, MidpointRounding.AwayFromZero) : 0M);
    }
    set
    {
    }
  }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  UPSnapshotSize>.By<UPSnapshot.snapshotID>
  {
    public static UPSnapshotSize Find(PXGraph graph, Guid? snapshotID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<UPSnapshotSize>.By<UPSnapshot.snapshotID>.FindBy(graph, (object) snapshotID, options);
    }
  }

  public new static class FK
  {
    public class SourceCompany : 
      PrimaryKeyOf<UPCompany>.By<UPCompany.companyID>.ForeignKeyOf<UPSnapshotSize>.By<UPSnapshot.sourceCompany>
    {
    }
  }

  public abstract class sizeInDb : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UPSnapshotSize.sizeInDb>
  {
  }

  public abstract class sizeInDbMB : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UPSnapshotSize.sizeInDbMB>
  {
  }
}
