// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.RecentlyVisitedRecords.VisitedRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.UserRecords.RecentlyVisitedRecords;

[PXCacheName("Viewed Record", PXDacType.Catalogue)]
[PXInternalUseOnly]
public class VisitedRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IUserRecord
{
  [PXDBBool(IsKey = true)]
  [IsPortalDefault]
  public bool? IsPortal { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "User ID")]
  public virtual Guid? UserID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Record Note ID")]
  public Guid? RefNoteId { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, IsKey = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Visit Count")]
  public int? VisitCount { get; set; }

  /// <summary>The pre-cached record content returned to user.</summary>
  [PXDBText(IsUnicode = true)]
  public virtual string RecordContent { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  public abstract class isPortal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VisitedRecord.isPortal>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  VisitedRecord.userID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  VisitedRecord.refNoteID>
  {
  }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VisitedRecord.entityType>
  {
  }

  public abstract class visitCount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VisitedRecord.visitCount>
  {
  }

  public abstract class recordContent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VisitedRecord.recordContent>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VisitedRecord.createdDateTime>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VisitedRecord.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VisitedRecord.lastModifiedDateTime>
  {
  }
}
