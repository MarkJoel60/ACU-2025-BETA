// Decompiled with JetBrains decompiler
// Type: PX.Api.SYMappingField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Xml.Serialization;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYMappingField : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISYObjectField
{
  [PXDBGuid(false, IsKey = true)]
  public Guid? MappingID { get; set; }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (SYMapping.fieldCntr), IncrementStep = 32 /*0x20*/)]
  [PXParent(typeof (Select<SYMapping, Where<SYMapping.mappingID, Equal<Current<SYMappingField.mappingID>>>>))]
  public virtual short? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXUIField(DisplayName = "Is Visible", Visible = false)]
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsVisible { get; set; }

  [PXDBShort]
  public virtual short? ParentLineNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? OrderNumber { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  [PXUIField(DisplayName = "Target Object", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual 
  #nullable disable
  string ObjectName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Native Object Name", Enabled = false, Visible = false)]
  public virtual string ObjectNameHidden
  {
    get => this.ObjectName;
    set
    {
    }
  }

  [PXDBString(4000, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field or Action", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FieldName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Native Field / Action Name", Enabled = false, Visible = false)]
  public virtual string FieldNameHidden
  {
    get => this.FieldName;
    set
    {
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Native Field / Action Name", Enabled = false, Visible = false)]
  public virtual string FullFieldNameHidden { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXFormulaEditor(DisplayName = "Source Field or Value", IsDBField = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXFormulaEditor_AddInternalFields]
  [PXFormulaEditor_AddExternalFields]
  [PXFormulaEditor_AddMultiselectSubstitute]
  public string Value { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Commit")]
  public virtual bool? NeedCommit { get; set; }

  [PXUIField(DisplayName = "Search", Visible = false)]
  [PXDBBool]
  public virtual bool? NeedSearch { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ignore Error")]
  public virtual bool? IgnoreError { get; set; }

  [PXUIField(DisplayName = "Execute Action", Enabled = false)]
  [ExecuteActionBehaviorList]
  [PXDBString(1, IsFixed = true)]
  public virtual string ExecuteActionBehavior { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  [JsonIgnore]
  [XmlIgnore]
  internal bool UseCurrent { get; set; }

  public abstract class mappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMappingField.mappingID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYMappingField.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMappingField.isActive>
  {
  }

  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMappingField.isVisible>
  {
  }

  public abstract class parentLineNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYMappingField.parentLineNbr>
  {
  }

  public abstract class orderNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMappingField.orderNumber>
  {
  }

  public abstract class objectName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingField.objectName>
  {
  }

  public abstract class objectNameHidden : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingField.objectNameHidden>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingField.fieldName>
  {
  }

  public abstract class fieldNameHidden : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingField.fieldNameHidden>
  {
  }

  public abstract class fullFieldNameHidden : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingField.fullFieldNameHidden>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingField.value>
  {
  }

  public abstract class needCommit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMappingField.needCommit>
  {
  }

  public abstract class needSearch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMappingField.needSearch>
  {
  }

  public abstract class ignoreError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMappingField.ignoreError>
  {
  }

  public abstract class executeActionBehavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingField.executeActionBehavior>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMappingField.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMappingField.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingField.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMappingField.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SYMappingField.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingField.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMappingField.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYMappingField.tStamp>
  {
  }
}
