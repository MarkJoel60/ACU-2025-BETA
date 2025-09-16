// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Overrides.ARDocumentRelease.CuryARHistory2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Overrides.ARDocumentRelease;

public class CuryARHistory2 : CuryARHistory
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory2.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory2.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory2.subID>
  {
  }

  public new abstract class curyID : IBqlField, IBqlOperand
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory2.customerID>
  {
  }

  public new abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finBegBalance>
  {
  }

  public new abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdSales>
  {
  }

  public new abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdPayments>
  {
  }

  public new abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdDrAdjustments>
  {
  }

  public new abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdCrAdjustments>
  {
  }

  public new abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdDiscounts>
  {
  }

  public new abstract class finPtdCOGS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdCOGS>
  {
  }

  public new abstract class finPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdRGOL>
  {
  }

  public new abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdFinCharges>
  {
  }

  public new abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finYtdBalance>
  {
  }

  public new abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdDeposits>
  {
  }

  public new abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finYtdDeposits>
  {
  }

  public new abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranBegBalance>
  {
  }

  public new abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdSales>
  {
  }

  public new abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdPayments>
  {
  }

  public new abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdDrAdjustments>
  {
  }

  public new abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdCrAdjustments>
  {
  }

  public new abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdDiscounts>
  {
  }

  public new abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdRGOL>
  {
  }

  public new abstract class tranPtdCOGS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdCOGS>
  {
  }

  public new abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdFinCharges>
  {
  }

  public new abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranYtdBalance>
  {
  }

  public new abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdDeposits>
  {
  }

  public new abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranYtdDeposits>
  {
  }

  public new abstract class curyFinBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinBegBalance>
  {
  }

  public new abstract class curyFinPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdSales>
  {
  }

  public new abstract class curyFinPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdPayments>
  {
  }

  public new abstract class curyFinPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdDrAdjustments>
  {
  }

  public new abstract class curyFinPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdCrAdjustments>
  {
  }

  public new abstract class curyFinPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdDiscounts>
  {
  }

  public new abstract class curyFinPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdFinCharges>
  {
  }

  public new abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinYtdBalance>
  {
  }

  public new abstract class curyFinPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdDeposits>
  {
  }

  public new abstract class curyFinYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinYtdDeposits>
  {
  }

  public new abstract class curyTranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranBegBalance>
  {
  }

  public new abstract class curyTranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdSales>
  {
  }

  public new abstract class curyTranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdPayments>
  {
  }

  public new abstract class curyTranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdDrAdjustments>
  {
  }

  public new abstract class curyTranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdCrAdjustments>
  {
  }

  public new abstract class curyTranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdDiscounts>
  {
  }

  public new abstract class curyTranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdFinCharges>
  {
  }

  public new abstract class curyTranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranYtdBalance>
  {
  }

  public new abstract class curyTranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdDeposits>
  {
  }

  public new abstract class curyTranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranYtdDeposits>
  {
  }

  public abstract class tstamp : IBqlField, IBqlOperand
  {
  }

  public new abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CuryARHistory2.detDeleted>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistory2.finPeriodID>
  {
  }

  public new abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdRevalued>
  {
  }

  public new abstract class curyFinPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdRetainageWithheld>
  {
  }

  public new abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdRetainageWithheld>
  {
  }

  public new abstract class curyTranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdRetainageWithheld>
  {
  }

  public new abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdRetainageWithheld>
  {
  }

  public new abstract class curyFinYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinYtdRetainageWithheld>
  {
  }

  public new abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finYtdRetainageWithheld>
  {
  }

  public new abstract class curyTranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranYtdRetainageWithheld>
  {
  }

  public new abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranYtdRetainageWithheld>
  {
  }

  public new abstract class curyFinPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinPtdRetainageReleased>
  {
  }

  public new abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finPtdRetainageReleased>
  {
  }

  public new abstract class curyTranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranPtdRetainageReleased>
  {
  }

  public new abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranPtdRetainageReleased>
  {
  }

  public new abstract class curyFinYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyFinYtdRetainageReleased>
  {
  }

  public new abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.finYtdRetainageReleased>
  {
  }

  public new abstract class curyTranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.curyTranYtdRetainageReleased>
  {
  }

  public new abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory2.tranYtdRetainageReleased>
  {
  }
}
