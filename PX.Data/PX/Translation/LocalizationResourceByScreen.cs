// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationResourceByScreen
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Translation;

/// <exclude />
[Serializable]
public class LocalizationResourceByScreen : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(8, IsFixed = true, IsKey = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID", Enabled = false)]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(32 /*0x20*/, IsKey = true)]
  public virtual string IdValue { get; set; }

  [PXDBString(32 /*0x20*/, IsKey = true)]
  public virtual string IdRes { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created DateTime")]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Title", Enabled = false)]
  public virtual string Title { get; set; }

  /// <exclude />
  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceByScreen.screenID>
  {
  }

  /// <exclude />
  public abstract class idValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceByScreen.idValue>
  {
  }

  /// <exclude />
  public abstract class idRes : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationResourceByScreen.idRes>
  {
  }

  /// <exclude />
  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationResourceByScreen.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceByScreen.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationResourceByScreen.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationResourceByScreen.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceByScreen.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationResourceByScreen.lastModifiedDateTime>
  {
  }

  /// <exclude />
  public abstract class tStamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    LocalizationResourceByScreen.tStamp>
  {
  }

  /// <exclude />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationResourceByScreen.title>
  {
  }
}
