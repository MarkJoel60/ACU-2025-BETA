// Decompiled with JetBrains decompiler
// Type: PX.CS.CSAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance.GI;
using System;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

#nullable enable
namespace PX.CS;

[PXHidden]
public class CSAttribute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private const 
  #nullable disable
  string ObsoleteMsg = "Use a corresponding member of the AttributeControlType class instead.";
  [Obsolete("Use a corresponding member of the AttributeControlType class instead.", true)]
  public const int Text = 1;
  [Obsolete("Use a corresponding member of the AttributeControlType class instead.", true)]
  public const int Combo = 2;
  [Obsolete("Use a corresponding member of the AttributeControlType class instead.", true)]
  public const int CheckBox = 4;
  [Obsolete("Use a corresponding member of the AttributeControlType class instead.", true)]
  public const int Datetime = 5;
  [Obsolete("Use a corresponding member of the AttributeControlType class instead.", true)]
  public const int MultiSelectCombo = 6;
  [Obsolete("Use a corresponding member of the AttributeControlType class instead.", true)]
  public const int GISelector = 7;
  [Obsolete("Use a corresponding member of the AttributeControlType class instead.", true)]
  public const int Number = 8;
  protected string _EntryMask;
  protected string _RegExp;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected string _List;
  protected string _ObjectName;
  protected string _FieldName;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField(DisplayName = "Attribute ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (CSAttribute.attributeID))]
  public virtual string AttributeID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description { get; set; }

  [PXAttributeControlType]
  [PXDefault(1)]
  public virtual int? ControlType { get; set; }

  [PXDBString(60)]
  [PXUIField(DisplayName = "Entry Mask")]
  public virtual string EntryMask
  {
    get => this._EntryMask;
    set => this._EntryMask = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Reg. Exp.")]
  public virtual string RegExp
  {
    get => this._RegExp;
    set => this._RegExp = value;
  }

  /// <summary>
  /// The number of decimal places that a value of the <see cref="F:PX.CS.AttributeControlType.Number" /> type has.
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 8)]
  [PXDefault(0)]
  [PXFormula(typeof (Default<CSAttribute.controlType>))]
  [PXUIField(DisplayName = "Decimal Places")]
  [PXUIEnabled(typeof (BqlOperand<CSAttribute.controlType, IBqlInt>.IsEqual<AttributeControlType.number>))]
  public virtual int? Precision { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Internal", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual bool? IsInternal { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  [XmlIgnore]
  [ScriptIgnore]
  [PXDBLocalizableString(IsUnicode = true)]
  public virtual string List
  {
    get
    {
      return string.IsNullOrEmpty(this._List) ? string.Empty : Convert.ToBase64String(Encoding.ASCII.GetBytes(this._List));
    }
    set => this._List = value;
  }

  [PXDBString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXTablesSelector]
  public virtual string ObjectName
  {
    get => this._ObjectName;
    set => this._ObjectName = value;
  }

  [PXDBString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  public abstract class attributeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.attributeID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.description>
  {
  }

  public abstract class controlType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSAttribute.controlType>
  {
  }

  public abstract class entryMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.entryMask>
  {
  }

  public abstract class regExp : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.regExp>
  {
  }

  public abstract class precision : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSAttribute.precision>
  {
  }

  public abstract class isInternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAttribute.isInternal>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAttribute.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CSAttribute.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAttribute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttribute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CSAttribute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CSAttribute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttribute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    CSAttribute.lastModifiedDateTime>
  {
  }

  public abstract class list : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.list>
  {
  }

  public abstract class objectName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.objectName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.fieldName>
  {
  }
}
