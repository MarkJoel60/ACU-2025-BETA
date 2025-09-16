// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationResource
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
public class LocalizationResource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ResKey;

  [PXDBString(32 /*0x20*/, IsFixed = true, IsKey = true)]
  public virtual string IdValue { get; set; }

  [PXDBString(32 /*0x20*/, IsFixed = true, IsKey = true)]
  public virtual string Id { get; set; }

  [PXDBString(1000, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string ResKey
  {
    get => this._ResKey;
    set => this._ResKey = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/}, new string[] {"Unknown", "Site Map", "Page", "Report Field", "Class Field", "Report Prompt", "Report Value", "Resource File", "Report Constant", "String List", "Int List", "Db String List", "Message", "Entity Info", "Automation Button", "Table Field", "Action"})]
  public virtual int? ResType { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsSite { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPortal { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? IsNotLocalized { get; set; }

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

  public abstract class idValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationResource.idValue>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationResource.id>
  {
  }

  public abstract class resKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationResource.resKey>
  {
  }

  public abstract class resType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocalizationResource.resType>
  {
  }

  public abstract class isSite : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocalizationResource.isSite>
  {
  }

  public abstract class isPortal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocalizationResource.isPortal>
  {
  }

  public abstract class isNotLocalized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationResource.isNotLocalized>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LocalizationResource.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResource.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationResource.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocalizationResource.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResource.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    LocalizationResource.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LocalizationResource.tStamp>
  {
  }
}
