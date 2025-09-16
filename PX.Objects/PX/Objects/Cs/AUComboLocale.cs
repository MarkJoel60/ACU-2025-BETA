// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AUComboLocale
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

[PXHidden]
[Serializable]
public class AUComboLocale : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected string _TypeName;
  protected string _FieldName;
  protected string _LocaleName;
  protected string _Value;
  protected string _Description;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true)]
  [PXDefault]
  public virtual string TypeName
  {
    get => this._TypeName;
    set => this._TypeName = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBString(5, IsKey = true, IsFixed = true, InputMask = "aa->aa")]
  [PXDefault]
  [PXUIField(DisplayName = "Language Name")]
  public virtual string LocaleName
  {
    get => this._LocaleName;
    set => this._LocaleName = value;
  }

  [PXDBString(10, IsKey = true)]
  [PXDefault]
  public virtual string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
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
  public virtual DateTime? CreatedDateTime
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
  public virtual DateTime? LastModifiedDateTime
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

  public abstract class typeName : IBqlField, IBqlOperand
  {
  }

  public abstract class fieldName : IBqlField, IBqlOperand
  {
  }

  public abstract class localeName : IBqlField, IBqlOperand
  {
  }

  public abstract class value : IBqlField, IBqlOperand
  {
  }

  public abstract class description : IBqlField, IBqlOperand
  {
  }

  public abstract class createdByID : IBqlField, IBqlOperand
  {
  }

  public abstract class createdByScreenID : IBqlField, IBqlOperand
  {
  }

  public abstract class createdDateTime : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedByID : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedByScreenID : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedDateTime : IBqlField, IBqlOperand
  {
  }

  public abstract class tStamp : IBqlField, IBqlOperand
  {
  }
}
