// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRExpenseProjectionAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.DR;

[DRExpenseAccum]
[PXHidden]
[Serializable]
public class DRExpenseProjectionAccum : DRExpenseProjection
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  DRExpenseProjectionAccum.branchID>
  {
  }

  public new abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseProjectionAccum.acctID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseProjectionAccum.subID>
  {
  }

  public new abstract class componentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRExpenseProjectionAccum.componentID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseProjectionAccum.vendorID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRExpenseProjectionAccum.projectID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRExpenseProjectionAccum.finPeriodID>
  {
  }

  public new abstract class pTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseProjectionAccum.pTDProjected>
  {
  }

  public new abstract class pTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseProjectionAccum.pTDRecognized>
  {
  }

  public new abstract class pTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseProjectionAccum.pTDRecognizedSamePeriod>
  {
  }

  public new abstract class tranPTDProjected : IBqlField, IBqlOperand
  {
  }

  public new abstract class tranPTDRecognized : IBqlField, IBqlOperand
  {
  }

  public new abstract class tranPTDRecognizedSamePeriod : IBqlField, IBqlOperand
  {
  }
}
