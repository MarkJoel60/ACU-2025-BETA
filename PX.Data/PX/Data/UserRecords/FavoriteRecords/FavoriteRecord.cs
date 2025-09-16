// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.FavoriteRecords.FavoriteRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.UserRecords.FavoriteRecords;

[PXCacheName("Favorite Record", PXDacType.Catalogue)]
[PXInternalUseOnly]
public class FavoriteRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IUserRecord
{
  [PXDBBool(IsKey = true)]
  [IsPortalDefault]
  public bool? IsPortal { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Record Note ID")]
  public Guid? RefNoteId { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, IsKey = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  public virtual 
  #nullable disable
  string EntityType { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "User ID")]
  public virtual Guid? UserID { get; set; }

  /// <summary>The pre-cached record content returned to user.</summary>
  [PXDBText(IsUnicode = true)]
  public virtual string RecordContent { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  public abstract class isPortal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FavoriteRecord.isPortal>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FavoriteRecord.refNoteID>
  {
  }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FavoriteRecord.entityType>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FavoriteRecord.userID>
  {
  }

  public abstract class recordContent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FavoriteRecord.recordContent>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FavoriteRecord.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FavoriteRecord.createdDateTime>
  {
  }
}
