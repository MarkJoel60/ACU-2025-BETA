// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CuryARHistoryYtd
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
public class CuryARHistoryYtd : CuryARHistory
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    CuryARHistoryYtd>.By<CuryARHistoryYtd.branchID, CuryARHistoryYtd.accountID, CuryARHistoryYtd.subID, CuryARHistoryYtd.customerID, CuryARHistoryYtd.finPeriodID>
  {
    public static CuryARHistoryYtd Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (CuryARHistoryYtd) PrimaryKeyOf<CuryARHistoryYtd>.By<CuryARHistoryYtd.branchID, CuryARHistoryYtd.accountID, CuryARHistoryYtd.subID, CuryARHistoryYtd.customerID, CuryARHistoryYtd.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtd.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtd.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtd.subID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtd.customerID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtd.curyID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryYtd.finPeriodID>
  {
  }

  public new abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdSales>
  {
  }

  public new abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdDrAdjustments>
  {
  }

  public new abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdFinCharges>
  {
  }

  public new abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdPayments>
  {
  }

  public new abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdCrAdjustments>
  {
  }

  public new abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdDiscounts>
  {
  }

  public new abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdDeposits>
  {
  }

  public new abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdRetainageReleased>
  {
  }

  public new abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdRetainageWithheld>
  {
  }

  public new abstract class finPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.finPtdRGOL>
  {
  }

  public new abstract class curyFinPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdSales>
  {
  }

  public new abstract class curyFinPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdDrAdjustments>
  {
  }

  public new abstract class curyFinPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdFinCharges>
  {
  }

  public new abstract class curyFinPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdPayments>
  {
  }

  public new abstract class curyFinPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdCrAdjustments>
  {
  }

  public new abstract class curyFinPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdDiscounts>
  {
  }

  public new abstract class curyFinPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdDeposits>
  {
  }

  public new abstract class curyFinPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdRetainageReleased>
  {
  }

  public new abstract class curyFinPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdRetainageWithheld>
  {
  }

  public abstract class curyFinPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyFinPtdRGOL>
  {
  }

  public new abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdSales>
  {
  }

  public new abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdDrAdjustments>
  {
  }

  public new abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdFinCharges>
  {
  }

  public new abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdPayments>
  {
  }

  public new abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdCrAdjustments>
  {
  }

  public new abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdDiscounts>
  {
  }

  public new abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdDeposits>
  {
  }

  public new abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdRetainageReleased>
  {
  }

  public new abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdRetainageWithheld>
  {
  }

  public new abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.tranPtdRGOL>
  {
  }

  public new abstract class curyTranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdSales>
  {
  }

  public new abstract class curyTranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdDrAdjustments>
  {
  }

  public new abstract class curyTranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdFinCharges>
  {
  }

  public new abstract class curyTranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdPayments>
  {
  }

  public new abstract class curyTranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdCrAdjustments>
  {
  }

  public new abstract class curyTranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdDiscounts>
  {
  }

  public new abstract class curyTranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdDeposits>
  {
  }

  public new abstract class curyTranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdRetainageReleased>
  {
  }

  public new abstract class curyTranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdRetainageWithheld>
  {
  }

  public abstract class curyTranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtd.curyTranPtdRGOL>
  {
  }
}
