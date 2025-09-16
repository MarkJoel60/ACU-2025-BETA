// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Overrides.ARDocumentRelease.ARHistory2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Overrides.ARDocumentRelease;

public class ARHistory2 : ARHistory
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ARHistory2.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory2.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory2.subID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory2.customerID>
  {
  }

  public new abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finBegBalance>
  {
  }

  public new abstract class finPtdSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory2.finPtdSales>
  {
  }

  public new abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdPayments>
  {
  }

  public new abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdDrAdjustments>
  {
  }

  public new abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdCrAdjustments>
  {
  }

  public new abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdDiscounts>
  {
  }

  public new abstract class finPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory2.finPtdCOGS>
  {
  }

  public new abstract class finPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory2.finPtdRGOL>
  {
  }

  public new abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdFinCharges>
  {
  }

  public new abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finYtdBalance>
  {
  }

  public new abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdDeposits>
  {
  }

  public new abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finYtdDeposits>
  {
  }

  public new abstract class finPtdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdItemDiscounts>
  {
  }

  public new abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranBegBalance>
  {
  }

  public new abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdSales>
  {
  }

  public new abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdPayments>
  {
  }

  public new abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdDrAdjustments>
  {
  }

  public new abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdCrAdjustments>
  {
  }

  public new abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdDiscounts>
  {
  }

  public new abstract class tranPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory2.tranPtdRGOL>
  {
  }

  public new abstract class tranPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistory2.tranPtdCOGS>
  {
  }

  public new abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdFinCharges>
  {
  }

  public new abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranYtdBalance>
  {
  }

  public new abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdDeposits>
  {
  }

  public new abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranYtdDeposits>
  {
  }

  public new abstract class tranPtdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdItemDiscounts>
  {
  }

  public new abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdRetainageWithheld>
  {
  }

  public new abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdRetainageWithheld>
  {
  }

  public new abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finYtdRetainageWithheld>
  {
  }

  public new abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranYtdRetainageWithheld>
  {
  }

  public new abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdRetainageReleased>
  {
  }

  public new abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranPtdRetainageReleased>
  {
  }

  public new abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finYtdRetainageReleased>
  {
  }

  public new abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.tranYtdRetainageReleased>
  {
  }

  public new abstract class numberInvoicePaid : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARHistory2.numberInvoicePaid>
  {
  }

  public new abstract class paidInvoiceDays : IBqlField, IBqlOperand
  {
  }

  public new abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARHistory2.detDeleted>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARHistory2.finPeriodID>
  {
  }

  public new abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistory2.finPtdRevalued>
  {
  }
}
