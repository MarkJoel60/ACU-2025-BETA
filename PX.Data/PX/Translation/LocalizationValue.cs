// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Translation;

[Serializable]
public class LocalizationValue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _NeutralValue;

  [PXDBString(32 /*0x20*/, IsFixed = true, IsKey = true)]
  public virtual string Id { get; set; }

  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  public virtual string NeutralValue
  {
    get => this._NeutralValue;
    set => this._NeutralValue = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsSite { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPortal { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsObsolete { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsObsoletePortal { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? IsNotLocalized { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  public virtual int? TranslationCount { get; set; }

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

  public void ActivateSiteUsage(ref bool updated)
  {
    bool? isSite = this.IsSite;
    bool flag = false;
    if (!(isSite.GetValueOrDefault() == flag & isSite.HasValue))
      return;
    this.IsSite = new bool?(true);
    updated = true;
  }

  public void DeactivateSiteUsage(ref bool updated)
  {
    bool? isSite = this.IsSite;
    bool flag = true;
    if (!(isSite.GetValueOrDefault() == flag & isSite.HasValue))
      return;
    this.IsSite = new bool?(false);
    updated = true;
  }

  public void ActivatePortalUsage(ref bool updated)
  {
    bool? isPortal = this.IsPortal;
    bool flag = false;
    if (!(isPortal.GetValueOrDefault() == flag & isPortal.HasValue))
      return;
    this.IsPortal = new bool?(true);
    updated = true;
  }

  public void DeactivatePortalUsage(ref bool updated)
  {
    bool? isPortal = this.IsPortal;
    bool flag = true;
    if (!(isPortal.GetValueOrDefault() == flag & isPortal.HasValue))
      return;
    this.IsPortal = new bool?(false);
    updated = true;
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationValue.id>
  {
  }

  public abstract class neutralValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationValue.neutralValue>
  {
  }

  public abstract class isSite : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocalizationValue.isSite>
  {
  }

  public abstract class isPortal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocalizationValue.isPortal>
  {
  }

  public abstract class isObsolete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocalizationValue.isObsolete>
  {
  }

  public abstract class isObsoletePortal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationValue.isObsoletePortal>
  {
  }

  public abstract class isNotLocalized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationValue.isNotLocalized>
  {
  }

  public abstract class translationCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocalizationValue.translationCount>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LocalizationValue.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationValue.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationValue.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationValue.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationValue.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationValue.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LocalizationValue.tStamp>
  {
  }
}
