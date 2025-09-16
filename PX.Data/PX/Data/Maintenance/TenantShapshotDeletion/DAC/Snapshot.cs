// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Maintenance.TenantShapshotDeletion.DAC;

public sealed class Snapshot : PXCacheExtension<
#nullable disable
TenantSnapshotDeletion>
{
  [PXUIField(DisplayName = "Snapshot Name")]
  [PXString(IsUnicode = true)]
  public string SnapshotName { get; set; }

  [PXUIField(DisplayName = "Description")]
  [PXString(IsUnicode = true)]
  public string Description { get; set; }

  [PXUIField(DisplayName = "Visibility")]
  [PXString(IsUnicode = true)]
  public string Visibility { get; set; }

  [PXUIField(DisplayName = "Created On")]
  [PXDateAndTime]
  public System.DateTime? CreatedOn { get; set; }

  [PXUIField(DisplayName = "Version")]
  [PXString(20, IsUnicode = true, IsFixed = true)]
  public string Version { get; set; }

  [PXUIField(DisplayName = "Export Mode")]
  [PXString(255 /*0xFF*/, IsUnicode = true)]
  public string ExportMode { get; set; }

  [PXInt]
  public int? SourceCompany { get; set; }

  public static bool IsActive() => true;

  public abstract class snapshotName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Snapshot.snapshotName>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Snapshot.description>
  {
  }

  public abstract class visibility : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Snapshot.visibility>
  {
  }

  public abstract class createdOn : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Snapshot.createdOn>
  {
  }

  public abstract class version : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Snapshot.version>
  {
  }

  public abstract class exportMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Snapshot.exportMode>
  {
  }

  public abstract class sourceCompany : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Snapshot.sourceCompany>
  {
  }
}
