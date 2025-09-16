// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.DAC.TenantSnapshotDeletion
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Maintenance.TenantShapshotDeletion.DAC;

[PXCacheName("Tenant or Snapshot Deletion")]
public class TenantSnapshotDeletion : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "ID", Enabled = false)]
  [PXDBGuid(false)]
  public virtual Guid? Id { get; set; }

  [PXUIField(DisplayName = "Tenant ID", Enabled = false)]
  [PXDBInt(IsKey = true)]
  public virtual int? TenantId { get; set; }

  [PXUIField(DisplayName = "Snapshot ID", Enabled = false)]
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? SnapshotId { get; set; }

  [PXUIField(DisplayName = "Deletion Status", Enabled = false)]
  [DeletionStatusList]
  [PXDBString]
  [PXDefault("N")]
  public virtual 
  #nullable disable
  string DeletionStatus { get; set; }

  [PXUIField(DisplayName = "Deletion Progress", Enabled = false, Visible = false)]
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string DeletionProgress { get; set; }

  [PXUIField(DisplayName = "Deletion Heartbeat")]
  [PXDBDate(UseTimeZone = true, UseSmallDateTime = false, PreserveTime = true)]
  public virtual System.DateTime? DeletionHeartbeat { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXUIField(DisplayName = "Type", Enabled = false)]
  [PXIntList(typeof (DeletionType), false)]
  [PXInt]
  public virtual int? Type { get; set; }

  [PXUIField(DisplayName = "Size on Disk (MB)", Enabled = false)]
  [PXDecimal(2)]
  public virtual Decimal? SizeMB { get; set; }

  [PXUIField(DisplayName = "Selected")]
  [PXBool]
  public virtual bool? Selected { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TenantSnapshotDeletion.id>
  {
  }

  public abstract class tenantId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TenantSnapshotDeletion.tenantId>
  {
  }

  public abstract class snapshotId : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TenantSnapshotDeletion.snapshotId>
  {
  }

  public abstract class deletionStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TenantSnapshotDeletion.deletionStatus>
  {
  }

  public abstract class deletionProgress : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TenantSnapshotDeletion.deletionProgress>
  {
  }

  public abstract class deletionHeartbeat : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TenantSnapshotDeletion.deletionHeartbeat>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TenantSnapshotDeletion.noteID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TenantSnapshotDeletion.type>
  {
  }

  public abstract class sizeMB : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TenantSnapshotDeletion.sizeMB>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TenantSnapshotDeletion.selected>
  {
  }
}
