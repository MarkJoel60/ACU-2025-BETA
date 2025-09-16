// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationTranslation
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
public class LocalizationTranslation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(32 /*0x20*/, IsKey = true)]
  public virtual 
  #nullable disable
  string IdValue { get; set; }

  [PXDBString(32 /*0x20*/, IsKey = true)]
  public virtual string IdRes { get; set; }

  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual string Locale { get; set; }

  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string Value { get; set; }

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

  /// <exclude />
  public abstract class idValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationTranslation.idValue>
  {
  }

  /// <exclude />
  public abstract class idRes : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationTranslation.idRes>
  {
  }

  /// <exclude />
  public abstract class locale : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationTranslation.locale>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationTranslation.value>
  {
  }

  /// <exclude />
  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationTranslation.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslation.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationTranslation.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationTranslation.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslation.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationTranslation.lastModifiedDateTime>
  {
  }

  /// <exclude />
  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LocalizationTranslation.tStamp>
  {
  }
}
