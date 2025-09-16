// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationTranslationSet
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
public class LocalizationTranslationSet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private System.DateTime? _systemTime;

  [PXSelector(typeof (Search<LocalizationTranslationSet.id>), DescriptionField = typeof (LocalizationTranslationSet.description), SelectorMode = PXSelectorMode.DisplayModeText)]
  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Translation Set", Visibility = PXUIVisibility.Visible)]
  public virtual Guid? Id { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.Visible)]
  [PXDefault]
  public virtual 
  #nullable disable
  string Description { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Collected", Enabled = false)]
  public virtual bool? IsCollected { get; set; }

  [PXUIField(DisplayName = "Unbound Resources To Collect")]
  [PXStringList(MultiSelect = true)]
  [PXDBString(512 /*0x0200*/)]
  public virtual string ResourceToCollect { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "System Time", Enabled = false)]
  public virtual System.DateTime? SystemTime
  {
    get => this._systemTime;
    set => this._systemTime = value;
  }

  [PXDBString(32 /*0x20*/, IsUnicode = true)]
  [PXUIField(DisplayName = "System Version", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string SystemVersion { get; set; }

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

  [PXString(32 /*0x20*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Current System Version", Enabled = false)]
  public virtual string CurrentSystemVersion { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <exclude />
  public abstract class id : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LocalizationTranslationSet.id>
  {
  }

  /// <exclude />
  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSet.description>
  {
  }

  /// <exclude />
  public abstract class isCollected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationTranslationSet.isCollected>
  {
  }

  /// <exclude />
  public abstract class resourceToCollect : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSet.resourceToCollect>
  {
  }

  /// <exclude />
  public abstract class systemTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationTranslationSet.systemTime>
  {
  }

  /// <exclude />
  public abstract class systemVersion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSet.systemVersion>
  {
  }

  /// <exclude />
  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationTranslationSet.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSet.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationTranslationSet.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationTranslationSet.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSet.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationTranslationSet.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    LocalizationTranslationSet.tStamp>
  {
  }

  /// <exclude />
  public abstract class currentSystemVersion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationTranslationSet.currentSystemVersion>
  {
  }

  /// <exclude />
  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocalizationTranslationSet.selected>
  {
  }
}
