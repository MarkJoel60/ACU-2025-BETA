// Decompiled with JetBrains decompiler
// Type: PX.SM.AUTableExtensionState
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
public class AUTableExtensionState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _StateID;
  protected 
  #nullable disable
  string _TableName;
  protected string _FieldName;
  protected int? _StorageType;
  protected int? _ControlType;
  protected int? _DataType;
  protected string _Length;
  protected string _DisplayMask;
  protected string _DisplayName;
  protected string _ComboValues;
  protected string _ComboXml;
  protected string _ComboAttr;
  protected string _DefaultValue;
  protected string _DefaultXml;
  protected string _DefaultAttr;
  protected string _Selector;
  protected string _SelectorXml;
  protected string _SelectorAttr;
  protected bool? _IsRequired;
  protected bool? _IsVisible;
  protected bool? _IsEnable;
  protected string _MappedToTable;
  protected string _MappedToField;
  protected string _CustomAttribute;

  [PXDBGuid(false, IsKey = true)]
  [PXDefaultGuid]
  public virtual Guid? StateID
  {
    get => this._StateID;
    set => this._StateID = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXUIField(DisplayName = "Table Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(typeof (AUTableDefinition.tableName))]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (AUTableExtension.fieldName))]
  [PXUIField(DisplayName = "Field Name", Enabled = false)]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Storage Type", Enabled = false)]
  [PXIntList(typeof (AUTableExtensionState.EnumStorageType), false)]
  public virtual int? StorageType
  {
    get => this._StorageType;
    set => this._StorageType = value;
  }

  public AUTableExtensionState.EnumStorageType? StorageTypeEx
  {
    get
    {
      return !this.StorageType.HasValue ? new AUTableExtensionState.EnumStorageType?() : new AUTableExtensionState.EnumStorageType?((AUTableExtensionState.EnumStorageType) this.StorageType.Value);
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Control Type")]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, new string[] {"TextEdit", "NumberEdit", "MaskEdit", "Selector", "DateEdit", "ComboBox", "CheckBox", "GroupBox", "LinkEdit", "MailEdit", "ImageUploader"})]
  public virtual int? ControlType
  {
    get => this._ControlType;
    set => this._ControlType = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Data Type", Enabled = false)]
  [AUDataType]
  public virtual int? DataType
  {
    get => this._DataType;
    set => this._DataType = value;
  }

  [PXDBString(10)]
  [PXUIField(DisplayName = "Length")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIRequired(typeof (Where<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.decimal_t>, Or<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.nvarchar_t>>>))]
  [PXUIEnabled(typeof (Where<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.decimal_t>, Or<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.nvarchar_t>>>))]
  [PXFormula(typeof (Switch<Case<Where<AUTableExtensionState.dataType, NotEqual<AUDataTypeAttribute.decimal_t>, And<AUTableExtensionState.dataType, NotEqual<AUDataTypeAttribute.nvarchar_t>>>, Null>, AUTableExtensionState.length>))]
  public virtual string Length
  {
    get => this._Length;
    set => this._Length = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Display Mask")]
  [PXUIEnabled(typeof (Where<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.nvarchar_t>>))]
  [PXFormula(typeof (Switch<Case<Where<AUTableExtensionState.dataType, NotEqual<AUDataTypeAttribute.nvarchar_t>>, Null>, AUTableExtensionState.displayMask>))]
  public virtual string DisplayMask
  {
    get => this._DisplayMask;
    set => this._DisplayMask = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Display Name")]
  public virtual string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Combo Values", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.int_t>, Or<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.nvarchar_t>>>, Null>, AUTableExtensionState.comboValues>))]
  public virtual string ComboValues
  {
    get => this._ComboValues;
    set => this._ComboValues = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Combo Xml", Enabled = false, Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.int_t>, Or<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.nvarchar_t>>>, Null>, AUTableExtensionState.comboXml>))]
  public virtual string ComboXml
  {
    get => this._ComboXml;
    set => this._ComboXml = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Combo Attr", Enabled = false, Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.int_t>, Or<AUTableExtensionState.dataType, Equal<AUDataTypeAttribute.nvarchar_t>>>, Null>, AUTableExtensionState.comboAttr>))]
  public virtual string ComboAttr
  {
    get => this._ComboAttr;
    set => this._ComboAttr = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Default Value", Enabled = false)]
  public virtual string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Default Xml", Enabled = false, Visible = false)]
  public virtual string DefaultXml
  {
    get => this._DefaultXml;
    set => this._DefaultXml = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Default Attribute", Enabled = false, Visible = false)]
  public virtual string DefaultAttr
  {
    get => this._DefaultAttr;
    set => this._DefaultAttr = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Lookup", Enabled = false)]
  public virtual string Selector
  {
    get => this._Selector;
    set => this._Selector = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Selector Xml", Enabled = false, Visible = false)]
  public virtual string SelectorXml
  {
    get => this._SelectorXml;
    set => this._SelectorXml = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Selector Attr", Enabled = false, Visible = false)]
  public virtual string SelectorAttr
  {
    get => this._SelectorAttr;
    set => this._SelectorAttr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Required")]
  public virtual bool? IsRequired
  {
    get => this._IsRequired;
    set => this._IsRequired = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Visible")]
  public virtual bool? IsVisible
  {
    get => this._IsVisible;
    set => this._IsVisible = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Enable")]
  public virtual bool? IsEnable
  {
    get => this._IsEnable;
    set => this._IsEnable = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Mapped to Table", Visible = false)]
  public virtual string MappedToTable
  {
    get => this._MappedToTable;
    set => this._MappedToTable = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Mapped to Field", Visible = false)]
  public virtual string MappedToField
  {
    get => this._MappedToField;
    set => this._MappedToField = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Custom Attribute", Enabled = false)]
  public virtual string CustomAttribute
  {
    get => this._CustomAttribute;
    set => this._CustomAttribute = value;
  }

  public abstract class stateID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUTableExtensionState.stateID>
  {
  }

  public abstract class tableName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.tableName>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.fieldName>
  {
  }

  public abstract class storageType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTableExtensionState.storageType>
  {
  }

  public enum EnumStorageType
  {
    Virtual,
    Attribute,
    Table,
    Base,
    SeparateTable,
    Projection,
  }

  public abstract class controlType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTableExtensionState.controlType>
  {
  }

  public abstract class dataType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTableExtensionState.dataType>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTableExtensionState.length>
  {
  }

  public abstract class displayMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.displayMask>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.displayName>
  {
  }

  public abstract class comboValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.comboValues>
  {
  }

  public abstract class comboXml : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTableExtensionState.comboXml>
  {
  }

  public abstract class comboAttr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.comboAttr>
  {
  }

  public abstract class defaultValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.defaultValue>
  {
  }

  public abstract class defaultXml : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.defaultXml>
  {
  }

  public abstract class defaultAttr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.defaultAttr>
  {
  }

  public abstract class selector : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTableExtensionState.selector>
  {
  }

  public abstract class selectorXml : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.selectorXml>
  {
  }

  public abstract class selectorAttr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.selectorAttr>
  {
  }

  public abstract class isRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUTableExtensionState.isRequired>
  {
  }

  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUTableExtensionState.isVisible>
  {
  }

  public abstract class isEnable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUTableExtensionState.isEnable>
  {
  }

  public abstract class mappedToTable : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.mappedToTable>
  {
  }

  public abstract class mappedToField : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.mappedToField>
  {
  }

  public abstract class customAttribute : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtensionState.customAttribute>
  {
  }
}
