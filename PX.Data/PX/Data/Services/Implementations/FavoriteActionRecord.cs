// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Implementations.FavoriteActionRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Services.Implementations;

[PXCacheName("Favorite Action", PXDacType.Catalogue)]
[PXInternalUseOnly]
public class FavoriteActionRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool(IsKey = true)]
  [IsPortalDefault]
  public bool? IsPortal { get; set; }

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Action Name")]
  public virtual string ActionName { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "User ID")]
  public virtual Guid? UserID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  public abstract class isPortal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FavoriteActionRecord.isPortal>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FavoriteActionRecord.screenID>
  {
  }

  public abstract class actionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FavoriteActionRecord.actionName>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FavoriteActionRecord.userID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FavoriteActionRecord.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FavoriteActionRecord.createdDateTime>
  {
  }
}
