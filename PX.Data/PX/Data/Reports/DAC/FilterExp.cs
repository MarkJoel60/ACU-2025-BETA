// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.FilterExp
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Reports;

#nullable enable
namespace PX.Data.Reports.DAC;

[PXHidden]
public class FilterExp : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXDefault(-1)]
  public int? LineNbr { get; set; }

  [PXString]
  [PXStringList(IsLocalizable = false)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Property")]
  public 
  #nullable disable
  string FieldName { get; set; }

  [PXInt]
  [PXIntList(typeof (FilterCondition), false)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Condition")]
  public int? Condition { get; set; }

  [PXInt]
  [PXIntList(typeof (FilterOperator), false)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Operator")]
  public int? Operation { get; set; }

  [PXInt]
  [FilterRow.OpenBrackets]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Brackets")]
  public int? OpenBraces { get; set; }

  [PXInt]
  [FilterRow.CloseBrackets]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Brackets")]
  public int? CloseBraces { get; set; }

  [PXString]
  [PXDefault]
  [PXUIField(DisplayName = "Value")]
  public string Value { get; set; }

  [PXString]
  [PXDefault]
  [PXUIField(DisplayName = "Second Value")]
  public string Value2 { get; set; }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterExp.lineNbr>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterExp.fieldName>
  {
  }

  public abstract class condition : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterExp.condition>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterExp.operation>
  {
  }

  public abstract class openBraces : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterExp.openBraces>
  {
  }

  public abstract class closeBraces : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterExp.closeBraces>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterExp.value>
  {
  }

  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterExp.value2>
  {
  }
}
