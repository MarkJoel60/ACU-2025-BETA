// Decompiled with JetBrains decompiler
// Type: PX.Api.SYMappingCondition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYMappingCondition : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISYObjectField
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (SYMapping.mappingID))]
  public Guid? MappingID { get; set; }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (SYMapping.conditionCntr))]
  [PXParent(typeof (Select<SYMapping, Where<SYMapping.mappingID, Equal<Current<SYMappingCondition.mappingID>>>>))]
  public virtual short? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"-", "(", "((", "(((", "((((", "((((("})]
  [PXUIField(DisplayName = "Brackets")]
  public virtual int? OpenBrackets { get; set; }

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

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name", Required = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FieldName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Native Field Name", Enabled = false, Visible = false)]
  public virtual string FieldNameHidden
  {
    get => this.FieldName;
    set
    {
    }
  }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Condition", Required = true)]
  [SYConditionType(null)]
  public virtual int? Condition { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value 2")]
  public virtual string Value2 { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"-", ")", "))", ")))", "))))", ")))))"})]
  [PXUIField(DisplayName = "Brackets")]
  public virtual int? CloseBrackets { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1}, new string[] {"And", "Or"})]
  [PXUIField(FieldName = "Operator", DisplayName = "Operator")]
  public virtual int? Operator { get; set; }

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

  public abstract class mappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMappingCondition.mappingID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYMappingCondition.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMappingCondition.isActive>
  {
  }

  public abstract class openBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMappingCondition.openBrackets>
  {
  }

  public abstract class objectName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingCondition.objectName>
  {
  }

  public abstract class objectNameHidden : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingCondition.objectNameHidden>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingCondition.fieldName>
  {
  }

  public abstract class fieldNameHidden : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingCondition.fieldNameHidden>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMappingCondition.condition>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingCondition.value>
  {
  }

  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMappingCondition.value2>
  {
  }

  public abstract class closeBrackets : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMappingCondition.closeBrackets>
  {
  }

  public abstract class operatoR : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMappingCondition.operatoR>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMappingCondition.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMappingCondition.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingCondition.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMappingCondition.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SYMappingCondition.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingCondition.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMappingCondition.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYMappingCondition.tStamp>
  {
  }
}
