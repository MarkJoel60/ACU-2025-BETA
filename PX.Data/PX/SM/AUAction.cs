// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAction
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
public class AUAction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected string _ActionName;
  protected string _MenuText;
  protected string _MenuIcon;
  protected short? _RefCntr;
  protected short? _RowNbr;
  protected short? _OrderNbr;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (AUStep.screenID))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault]
  public virtual string ActionName
  {
    get => this._ActionName;
    set => this._ActionName = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Menu Text")]
  public virtual string MenuText
  {
    get => this._MenuText;
    set => this._MenuText = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Icon")]
  [PXIconsList]
  public virtual string MenuIcon
  {
    get => this._MenuIcon;
    set => this._MenuIcon = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? RefCntr
  {
    get => this._RefCntr;
    set => this._RefCntr = value;
  }

  [PXDBShort]
  [PXDefault]
  public virtual short? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  [PXDBShort]
  [PXDefault]
  public virtual short? OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
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
  AUAction.screenID>
  {
  }

  public abstract class actionName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAction.actionName>
  {
  }

  public abstract class menuText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAction.menuText>
  {
  }

  public abstract class menuIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAction.menuIcon>
  {
  }

  public abstract class refCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUAction.refCntr>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUAction.rowNbr>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUAction.orderNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUAction.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUAction.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUAction.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUAction.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUAction.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUAction.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUAction.tStamp>
  {
  }
}
