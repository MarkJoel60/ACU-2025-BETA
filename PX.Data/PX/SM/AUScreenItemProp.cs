// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenItemProp
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUScreenItemProp : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAUItemProp
{
  protected 
  #nullable disable
  string _ScreenID;
  protected Guid? _ProjectID;
  protected string _ItemID;
  protected string _propertyType;
  protected string _propertyID;
  protected string _PropertyName;
  protected string _OriginalValue;
  protected string _OverrideValue;
  protected string _PropertyValue;
  protected bool? _IsOverride;
  protected bool? _Inherit;
  protected int? _SortOrder;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault(typeof (AUScreenDefinition.screenID))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (AUScreenDefinition.projectID))]
  public virtual Guid? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBString(1024 /*0x0400*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXParent(typeof (Select<AUScreenItem, Where<AUScreenItem.screenID, Equal<Current<AUScreenItemProp.screenID>>, And<AUScreenItem.projectID, Equal<Current<AUScreenItemProp.projectID>>, And<AUScreenItem.itemID, Equal<Current<AUScreenItemProp.itemID>>>>>>))]
  public virtual string ItemID
  {
    get => this._ItemID;
    set => this._ItemID = value;
  }

  [PXDBString(10, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault("")]
  public virtual string PropertyType
  {
    get => this._propertyType;
    set => this._propertyType = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string PropertyID
  {
    get => this._propertyID;
    set => this._propertyID = value;
  }

  [PXString(128 /*0x80*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Property", Enabled = false)]
  public virtual string PropertyName
  {
    get => this._PropertyName;
    set => this._PropertyName = value;
  }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Original", Enabled = false)]
  public virtual string OriginalValue
  {
    get => this._OriginalValue;
    set => this._OriginalValue = value;
  }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Override", Enabled = true)]
  public virtual string OverrideValue
  {
    get => this._OverrideValue;
    set => this._OverrideValue = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string PropertyValue
  {
    get => this._PropertyValue;
    set => this._PropertyValue = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  [PXDBCalced(typeof (Switch<Case<Where<AUScreenItemProp.projectID, Equal<CurrentValue<AUScreenDefinition.projectID>>>, PX.Data.True>, False>), typeof (bool))]
  [PXFormula(typeof (Switch<Case<Where<AUScreenItemProp.projectID, Equal<CurrentValue<AUScreenDefinition.projectID>>>, PX.Data.True>, False>))]
  public virtual bool? IsOverride
  {
    get => this._IsOverride;
    set => this._IsOverride = value;
  }

  [PXBool]
  public virtual bool? Inherit
  {
    get => new bool?(true);
    set
    {
    }
  }

  [PXInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItemProp.screenID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenItemProp.projectID>
  {
  }

  public abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItemProp.itemID>
  {
  }

  public abstract class propertyType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItemProp.propertyType>
  {
  }

  public abstract class propertyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItemProp.propertyID>
  {
  }

  public abstract class propertyName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItemProp.propertyName>
  {
  }

  public abstract class originalValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItemProp.originalValue>
  {
  }

  public abstract class overrideValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItemProp.overrideValue>
  {
  }

  public abstract class propertyValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItemProp.propertyValue>
  {
  }

  public abstract class isOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenItemProp.isOverride>
  {
  }

  public abstract class inherit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenItemProp.inherit>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenItemProp.sortOrder>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenItemProp.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItemProp.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenItemProp.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenItemProp.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItemProp.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenItemProp.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScreenItemProp.tStamp>
  {
  }
}
