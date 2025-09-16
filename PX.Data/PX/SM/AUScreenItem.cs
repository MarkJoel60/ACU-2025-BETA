// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenItem
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
public class AUScreenItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected Guid? _ProjectID;
  protected string _ItemType;
  protected string _ItemCD;
  protected string _ParentID;
  protected string _ItemID;
  protected int? _ChildRowCntr;
  protected bool? _IsOverride;
  protected bool? _Inherit;
  protected int? _SortOrder;
  protected string _Description;
  protected bool? _IsActive;
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

  [PXDBString(2, IsUnicode = false, InputMask = "", IsKey = true)]
  [PXDefault]
  public virtual string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXParent(typeof (Select<AUScreenDefinition, Where<AUScreenDefinition.screenID, Equal<Current<AUScreenItem.screenID>>, And<AUScreenDefinition.projectID, Equal<Current<AUScreenItem.projectID>>>>>))]
  public virtual string ItemCD
  {
    get => this._ItemCD;
    set => this._ItemCD = value;
  }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXDefault("")]
  public virtual string ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXFormula(typeof (Add<Switch<Case<Where<AUScreenItem.parentID, NotEqual<StringEmpty>>, Add<AUScreenItem.parentID, AUScreenItem.ParentSeparator>>, StringEmpty>, Add<AUScreenItem.itemType, Add<AUScreenItem.TypeSeparator, AUScreenItem.itemCD>>>))]
  public virtual string ItemID
  {
    get => this._ItemID;
    set => this._ItemID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChildRowCntr
  {
    get => this._ChildRowCntr;
    set => this._ChildRowCntr = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override", Enabled = false)]
  [PXDBCalced(typeof (Switch<Case<Where<AUScreenItem.projectID, Equal<CurrentValue<AUScreenDefinition.projectID>>>, PX.Data.True>, False>), typeof (bool))]
  [PXFormula(typeof (Switch<Case<Where<AUScreenItem.projectID, Equal<CurrentValue<AUScreenDefinition.projectID>>>, PX.Data.True>, False>))]
  public virtual bool? IsOverride
  {
    get => this._IsOverride;
    set => this._IsOverride = value;
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? Inherit
  {
    get => this._Inherit;
    set => this._Inherit = value;
  }

  [PXInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
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
  AUScreenItem.screenID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenItem.projectID>
  {
  }

  public abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItem.itemType>
  {
  }

  public abstract class itemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItem.itemCD>
  {
  }

  public abstract class parentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItem.parentID>
  {
  }

  public abstract class itemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItem.itemID>
  {
  }

  public abstract class childRowCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenItem.childRowCntr>
  {
  }

  public abstract class isOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenItem.isOverride>
  {
  }

  public abstract class inherit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenItem.inherit>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenItem.sortOrder>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenItem.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenItem.isActive>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenItem.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScreenItem.tStamp>
  {
  }

  protected class TypeSeparator : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItem.TypeSeparator>
  {
    public TypeSeparator()
      : base("@")
    {
    }
  }

  protected class ParentSeparator : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenItem.ParentSeparator>
  {
    public ParentSeparator()
      : base(".")
    {
    }
  }
}
