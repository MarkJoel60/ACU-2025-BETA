// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationTranslationSetItem
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
public class LocalizationTranslationSetItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string IS_COLLECTED_DISPLAY_NAME = "Collected";

  [PXParent(typeof (Select<LocalizationTranslationSet, Where<LocalizationTranslationSet.id, Equal<Current<LocalizationTranslationSetItem.setId>>>>))]
  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Translation Set ID")]
  public virtual Guid? SetId { get; set; }

  [PXDBString(8, IsFixed = true, IsKey = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID", Enabled = false)]
  public virtual string ScreenID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Collected", Enabled = false)]
  public virtual bool? IsCollected { get; set; }

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

  [PXString]
  [PXUIField(DisplayName = "Title", Enabled = false)]
  public virtual string Title { get; set; }

  [PXString]
  public string NameForStringCollection { get; set; }

  /// <exclude />
  public abstract class setId : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LocalizationTranslationSetItem.setId>
  {
  }

  /// <exclude />
  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSetItem.screenID>
  {
  }

  /// <exclude />
  public abstract class isActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationTranslationSetItem.isActive>
  {
  }

  /// <exclude />
  public abstract class isCollected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationTranslationSetItem.isCollected>
  {
  }

  /// <exclude />
  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationTranslationSetItem.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSetItem.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationTranslationSetItem.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationTranslationSetItem.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSetItem.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationTranslationSetItem.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    LocalizationTranslationSetItem.tStamp>
  {
  }

  /// <exclude />
  public abstract class title : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSetItem.title>
  {
  }

  /// <exclude />
  public abstract class nameForStringCollection : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSetItem.nameForStringCollection>
  {
  }
}
