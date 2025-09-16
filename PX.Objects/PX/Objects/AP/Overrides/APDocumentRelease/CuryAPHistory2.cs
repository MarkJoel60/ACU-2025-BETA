// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.APDocumentRelease.CuryAPHistory2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Overrides.APDocumentRelease;

public class CuryAPHistory2 : CuryAPHistory
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory2.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory2.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory2.subID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory2.vendorID>
  {
  }

  public new abstract class curyID : IBqlField, IBqlOperand
  {
  }

  public new abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finBegBalance>
  {
  }

  public new abstract class finPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdPurchases>
  {
  }

  public new abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdPayments>
  {
  }

  public new abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdDrAdjustments>
  {
  }

  public new abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdCrAdjustments>
  {
  }

  public new abstract class finPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdDiscTaken>
  {
  }

  public new abstract class finPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdRGOL>
  {
  }

  public new abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finYtdBalance>
  {
  }

  public new abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdDeposits>
  {
  }

  public new abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finYtdDeposits>
  {
  }

  public new abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranBegBalance>
  {
  }

  public new abstract class tranPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdPurchases>
  {
  }

  public new abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdPayments>
  {
  }

  public new abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdDrAdjustments>
  {
  }

  public new abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdCrAdjustments>
  {
  }

  public new abstract class tranPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdDiscTaken>
  {
  }

  public new abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdRGOL>
  {
  }

  public new abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranYtdBalance>
  {
  }

  public new abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdDeposits>
  {
  }

  public new abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranYtdDeposits>
  {
  }

  public new abstract class curyFinBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinBegBalance>
  {
  }

  public new abstract class curyFinPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdPurchases>
  {
  }

  public new abstract class curyFinPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdPayments>
  {
  }

  public new abstract class curyFinPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdDrAdjustments>
  {
  }

  public new abstract class curyFinPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdCrAdjustments>
  {
  }

  public new abstract class curyFinPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdDiscTaken>
  {
  }

  public new abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinYtdBalance>
  {
  }

  public new abstract class curyFinPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdDeposits>
  {
  }

  public new abstract class curyFinYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinYtdDeposits>
  {
  }

  public new abstract class curyTranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranBegBalance>
  {
  }

  public new abstract class curyTranPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdPurchases>
  {
  }

  public new abstract class curyTranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdPayments>
  {
  }

  public new abstract class curyTranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdDrAdjustments>
  {
  }

  public new abstract class curyTranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdCrAdjustments>
  {
  }

  public new abstract class curyTranPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdDiscTaken>
  {
  }

  public new abstract class curyTranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranYtdBalance>
  {
  }

  public new abstract class curyTranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdDeposits>
  {
  }

  public new abstract class curyTranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranYtdDeposits>
  {
  }

  public abstract class tstamp : IBqlField, IBqlOperand
  {
  }

  public new abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CuryAPHistory2.detDeleted>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryAPHistory2.finPeriodID>
  {
  }

  public new abstract class curyFinPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdWhTax>
  {
  }

  public new abstract class curyTranPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdWhTax>
  {
  }

  public new abstract class finPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdWhTax>
  {
  }

  public new abstract class tranPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdWhTax>
  {
  }

  public new abstract class curyFinPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdRetainageWithheld>
  {
  }

  public new abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdRetainageWithheld>
  {
  }

  public new abstract class curyTranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdRetainageWithheld>
  {
  }

  public new abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdRetainageWithheld>
  {
  }

  public new abstract class curyFinYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinYtdRetainageWithheld>
  {
  }

  public new abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finYtdRetainageWithheld>
  {
  }

  public new abstract class curyTranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranYtdRetainageWithheld>
  {
  }

  public new abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranYtdRetainageWithheld>
  {
  }

  public new abstract class curyFinPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinPtdRetainageReleased>
  {
  }

  public new abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdRetainageReleased>
  {
  }

  public new abstract class curyTranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranPtdRetainageReleased>
  {
  }

  public new abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranPtdRetainageReleased>
  {
  }

  public new abstract class curyFinYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyFinYtdRetainageReleased>
  {
  }

  public new abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finYtdRetainageReleased>
  {
  }

  public new abstract class curyTranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.curyTranYtdRetainageReleased>
  {
  }

  public new abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.tranYtdRetainageReleased>
  {
  }

  public new abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory2.finPtdRevalued>
  {
  }
}
