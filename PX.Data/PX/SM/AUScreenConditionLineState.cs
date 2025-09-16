// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenConditionLineState
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
public class AUScreenConditionLineState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Value2;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public virtual string ScreenID { get; set; }

  [PXDBGuid(false, IsKey = true)]
  [PXParent(typeof (Select<AUScreenConditionState, Where<AUScreenConditionState.screenID, Equal<Current<AUScreenConditionLineState.screenID>>, And<AUScreenConditionState.conditionID, Equal<Current<AUScreenConditionLineState.conditionID>>>>>))]
  public virtual Guid? ConditionID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (AUScreenConditionState))]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"-", "(", "((", "(((", "((((", "((((("})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public virtual int? OpenBrackets { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name", Required = true)]
  public virtual string FieldName { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Condition", Required = true)]
  [AUExtendedConditionType]
  public virtual int? Condition { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsFromScheme { get; set; }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXFormulaEditor(IsDBField = false, DisplayName = "Value", BqlTable = typeof (AUScreenConditionLineState))]
  [PXFormulaEditor.AddFunctions]
  [PXFormulaEditor.AddOperators]
  [PXStringList(new string[] {""}, new string[] {""}, ExclusiveValues = false)]
  public virtual string Value { get; set; }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXFormulaEditor(IsDBField = false, DisplayName = "Value 2", BqlTable = typeof (AUScreenConditionLineState))]
  [PXFormulaEditor.AddFunctions]
  [PXFormulaEditor.AddOperators]
  [PXStringList(new string[] {""}, new string[] {""}, ExclusiveValues = false)]
  public virtual string Value2 { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"-", ")", "))", ")))", "))))", ")))))"})]
  [PXUIField(DisplayName = "Brackets")]
  [PXDefault(0)]
  public virtual int? CloseBrackets { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {0, 1}, new string[] {"And", "Or"})]
  [PXUIField(FieldName = "Operator")]
  [PXDefault(0)]
  public virtual int? Operator { get; set; }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionLineState.screenID>
  {
  }

  public abstract class conditionID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenConditionLineState.conditionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenConditionLineState.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUScreenConditionLineState.isActive>
  {
  }

  public abstract class openBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenConditionLineState.openBrackets>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenConditionLineState.fieldName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenConditionLineState.condition>
  {
  }

  public abstract class isFromScheme : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenConditionLineState.isFromScheme>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenConditionLineState.value>
  {
  }

  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenConditionLineState.value2>
  {
  }

  public abstract class closeBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AUScreenConditionLineState.closeBrackets>
  {
  }

  public abstract class operatoR : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUScreenConditionLineState.operatoR>
  {
  }
}
