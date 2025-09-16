// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance.GI;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN.Matrix.Attributes;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable enable
namespace PX.Objects.CS;

[DebuggerDisplay("[{AttributeID}]: {Description}")]
[PXPrimaryGraph(new Type[] {typeof (CSAttributeMaint)}, new Type[] {typeof (Select<CSAttribute, Where<CSAttribute.attributeID, Equal<Current<CSAttribute.attributeID>>>>)})]
[PXCacheName("Attribute")]
public class CSAttribute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private const 
  #nullable disable
  string ObsoleteMsg = "Use a corresponding member of the PX.CS.AttributeControlType class instead.";
  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public const int Text = 1;
  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public const int Combo = 2;
  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public const int CheckBox = 4;
  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public const int Datetime = 5;
  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public const int MultiSelectCombo = 6;
  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public const int GISelector = 7;
  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public const int Number = 8;
  protected string _AttributeID;
  protected string _Description;
  protected int? _ControlType;
  protected string _EntryMask;
  protected string _RegExp;
  protected string _List;
  protected bool? _IsInternal;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _ObjectName;
  protected string _FieldName;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CSAttribute.attributeID, Where<CSAttribute.attributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>>))]
  public virtual string AttributeID
  {
    get => this._AttributeID;
    set => this._AttributeID = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXAttributeControlType]
  [PXDefault(1)]
  public virtual int? ControlType
  {
    get => this._ControlType;
    set => this._ControlType = value;
  }

  [PXDBString(60)]
  [PXUIField(DisplayName = "Entry Mask")]
  [PXUIVisible(typeof (Where<CSAttribute.controlType, NotEqual<AttributeControlType.giSelector>>))]
  public virtual string EntryMask
  {
    get => this._EntryMask;
    set => this._EntryMask = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Reg. Exp.")]
  [PXUIVisible(typeof (Where<CSAttribute.controlType, NotEqual<AttributeControlType.giSelector>>))]
  public virtual string RegExp
  {
    get => this._RegExp;
    set => this._RegExp = value;
  }

  /// <inheritdoc cref="P:PX.CS.CSAttribute.Precision" />
  [PXDBInt(MinValue = 0, MaxValue = 8)]
  [PXDefault(0)]
  [PXFormula(typeof (Default<CSAttribute.controlType>))]
  [PXUIField(DisplayName = "Decimal Places")]
  [PXUIEnabled(typeof (BqlOperand<CSAttribute.controlType, IBqlInt>.IsEqual<AttributeControlType.number>))]
  public virtual int? Precision { get; set; }

  [XmlIgnore]
  [PXDBLocalizableString(IsUnicode = true)]
  public virtual string List
  {
    get => this._List;
    set => this._List = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsInternal
  {
    get => this._IsInternal;
    set => this._IsInternal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Contains Personal Data", FieldClass = "GDPR")]
  public virtual bool? ContainsPersonalData { get; set; }

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

  [PXDBString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  [PXUIField]
  [PXUIVisible(typeof (Where<CSAttribute.controlType, Equal<AttributeControlType.giSelector>>))]
  [PXTablesSelector]
  public virtual string ObjectName
  {
    get => this._ObjectName;
    set => this._ObjectName = value;
  }

  [PXDBString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Schema Field")]
  [PXUIVisible(typeof (Where<CSAttribute.controlType, Equal<AttributeControlType.giSelector>>))]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [Obsolete("Use a corresponding member of the PX.CS.AttributeControlType class instead.", true)]
  public abstract class AttrType
  {
    public class giSelector : AttributeControlType.giSelector
    {
    }
  }

  public class PK : PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>
  {
    public static CSAttribute Find(PXGraph graph, string attributeID, PKFindOptions options = 0)
    {
      return (CSAttribute) PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.FindBy(graph, (object) attributeID, options);
    }
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

  public abstract class list : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttribute.list>
  {
  }

  public abstract class isInternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAttribute.isInternal>
  {
  }

  public abstract class containsPersonalData : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CSAttribute.containsPersonalData>
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
    IBqlDateTime, DateTime>.Field<
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
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSAttribute.lastModifiedDateTime>
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
