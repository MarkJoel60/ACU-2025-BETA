// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPRuleEmployeeCondition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Assignment/Approval Rule Employee Condition")]
[PXTable]
[Serializable]
public class EPRuleEmployeeCondition : EPRuleBaseCondition, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Rule ID")]
  [PXParent(typeof (Select<EPRule, Where<EPRule.ruleID, Equal<Current<EPRuleEmployeeCondition.ruleID>>>>))]
  public override Guid? RuleID { get; set; }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PX.Data.RowNbr]
  public virtual short? RowNbr { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5, 6, 7}, new string[] {"-", "(", "((", "(((", "((((", "(((((", "Activity Exists (", "Activity Not Exists ("})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public override int? OpenBrackets { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = false)]
  [EPRuleEmployeeConditionEntity.List]
  [PXDefault]
  [PXUIField(DisplayName = "Entity", Required = true)]
  public override 
  #nullable disable
  string Entity { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Field Name", Required = true)]
  public override string FieldName { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Condition")]
  [EPConditionType]
  public override int? Condition { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Relative")]
  public override bool? IsRelative { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Doc.")]
  public override bool? IsField { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public override string Value { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value 2")]
  public override string Value2 { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"-", ")", "))", ")))", "))))", ")))))"})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public override int? CloseBrackets { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1}, new string[] {"And", "Or"})]
  [PXUIField(FieldName = "Operator")]
  [PXDefault(0)]
  public override int? Operator { get; set; }

  /// <summary>
  /// Identifier of the record, it is a calculated field and is used for UI only.
  /// Similar to <see cref="P:PX.Data.Note.NoteID" />.
  /// </summary>
  [PXGuid]
  [PXUIField]
  [PXUnboundDefault]
  public override Guid? UiNoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class ruleID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRuleEmployeeCondition.ruleID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  EPRuleEmployeeCondition.rowNbr>
  {
  }

  public abstract class openBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPRuleEmployeeCondition.openBrackets>
  {
  }

  public abstract class entity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRuleEmployeeCondition.entity>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPRuleEmployeeCondition.fieldName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRuleEmployeeCondition.condition>
  {
  }

  public abstract class isRelative : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPRuleEmployeeCondition.isRelative>
  {
  }

  public abstract class isField : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPRuleEmployeeCondition.isField>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRuleEmployeeCondition.value>
  {
  }

  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPRuleEmployeeCondition.value2>
  {
  }

  public abstract class closeBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPRuleEmployeeCondition.closeBrackets>
  {
  }

  public abstract class operatoR : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPRuleEmployeeCondition.operatoR>
  {
  }

  public abstract class uiNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPRuleEmployeeCondition.uiNoteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPRuleEmployeeCondition.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPRuleEmployeeCondition.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPRuleEmployeeCondition.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPRuleEmployeeCondition.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPRuleEmployeeCondition.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPRuleEmployeeCondition.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPRuleEmployeeCondition.Tstamp>
  {
  }
}
