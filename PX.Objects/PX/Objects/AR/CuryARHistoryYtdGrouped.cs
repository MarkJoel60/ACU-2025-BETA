// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CuryARHistoryYtdGrouped
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

[PXProjection(typeof (Select5<CuryARHistory, LeftJoin<CuryARHistoryYtd, On<CuryARHistoryYtd.customerID, Equal<CuryARHistory.customerID>, And<CuryARHistoryYtd.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistoryYtd.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistoryYtd.subID, Equal<CuryARHistory.subID>, And<CuryARHistoryYtd.curyID, Equal<CuryARHistory.curyID>, And<CuryARHistoryYtd.finPeriodID, LessEqual<CuryARHistory.finPeriodID>>>>>>>>, Aggregate<GroupBy<CuryARHistory.branchID, GroupBy<CuryARHistory.accountID, GroupBy<CuryARHistory.subID, GroupBy<CuryARHistory.customerID, GroupBy<CuryARHistory.curyID, GroupBy<CuryARHistory.finPeriodID, GroupBy<CuryARHistory.finBegBalance, GroupBy<CuryARHistory.finPtdSales, GroupBy<CuryARHistory.finPtdDrAdjustments, GroupBy<CuryARHistory.finPtdFinCharges, GroupBy<CuryARHistory.finPtdPayments, GroupBy<CuryARHistory.finPtdCrAdjustments, GroupBy<CuryARHistory.finPtdDiscounts, GroupBy<CuryARHistory.finPtdRGOL, GroupBy<CuryARHistory.finYtdBalance, GroupBy<CuryARHistory.finYtdDeposits, GroupBy<CuryARHistory.finYtdRetainageReleased, GroupBy<CuryARHistory.finYtdRetainageWithheld, GroupBy<CuryARHistory.curyFinBegBalance, GroupBy<CuryARHistory.curyFinPtdSales, GroupBy<CuryARHistory.curyFinPtdDrAdjustments, GroupBy<CuryARHistory.curyFinPtdFinCharges, GroupBy<CuryARHistory.curyFinPtdPayments, GroupBy<CuryARHistory.curyFinPtdCrAdjustments, GroupBy<CuryARHistory.curyFinPtdDiscounts, GroupBy<CuryARHistory.curyFinYtdBalance, GroupBy<CuryARHistory.curyFinYtdDeposits, GroupBy<CuryARHistory.curyFinYtdRetainageReleased, GroupBy<CuryARHistory.curyFinYtdRetainageWithheld, GroupBy<CuryARHistory.tranBegBalance, GroupBy<CuryARHistory.tranPtdSales, GroupBy<CuryARHistory.tranPtdDrAdjustments, GroupBy<CuryARHistory.tranPtdFinCharges, GroupBy<CuryARHistory.tranPtdPayments, GroupBy<CuryARHistory.tranPtdCrAdjustments, GroupBy<CuryARHistory.tranPtdDiscounts, GroupBy<CuryARHistory.tranPtdRGOL, GroupBy<CuryARHistory.tranYtdBalance, GroupBy<CuryARHistory.tranYtdDeposits, GroupBy<CuryARHistory.tranYtdRetainageReleased, GroupBy<CuryARHistory.tranYtdRetainageWithheld, GroupBy<CuryARHistory.curyTranBegBalance, GroupBy<CuryARHistory.curyTranPtdSales, GroupBy<CuryARHistory.curyTranPtdDrAdjustments, GroupBy<CuryARHistory.curyTranPtdFinCharges, GroupBy<CuryARHistory.curyTranPtdPayments, GroupBy<CuryARHistory.curyTranPtdCrAdjustments, GroupBy<CuryARHistory.curyTranPtdDiscounts, GroupBy<CuryARHistory.curyTranYtdBalance, GroupBy<CuryARHistory.curyTranYtdDeposits, GroupBy<CuryARHistory.curyTranYtdRetainageReleased, GroupBy<CuryARHistory.curyTranYtdRetainageWithheld, Sum<CuryARHistoryYtd.finPtdSales, Sum<CuryARHistoryYtd.finPtdDrAdjustments, Sum<CuryARHistoryYtd.finPtdFinCharges, Sum<CuryARHistoryYtd.finPtdPayments, Sum<CuryARHistoryYtd.finPtdCrAdjustments, Sum<CuryARHistoryYtd.finPtdDiscounts, Sum<CuryARHistoryYtd.finPtdDeposits, Sum<CuryARHistoryYtd.finPtdRetainageReleased, Sum<CuryARHistoryYtd.finPtdRetainageWithheld, Sum<CuryARHistoryYtd.finPtdRGOL, Sum<CuryARHistoryYtd.curyFinPtdSales, Sum<CuryARHistoryYtd.curyFinPtdDrAdjustments, Sum<CuryARHistoryYtd.curyFinPtdFinCharges, Sum<CuryARHistoryYtd.curyFinPtdPayments, Sum<CuryARHistoryYtd.curyFinPtdCrAdjustments, Sum<CuryARHistoryYtd.curyFinPtdDiscounts, Sum<CuryARHistoryYtd.curyFinPtdDeposits, Sum<CuryARHistoryYtd.curyFinPtdRetainageReleased, Sum<CuryARHistoryYtd.curyFinPtdRetainageWithheld, Sum<CuryARHistoryYtd.tranPtdSales, Sum<CuryARHistoryYtd.tranPtdDrAdjustments, Sum<CuryARHistoryYtd.tranPtdFinCharges, Sum<CuryARHistoryYtd.tranPtdPayments, Sum<CuryARHistoryYtd.tranPtdCrAdjustments, Sum<CuryARHistoryYtd.tranPtdDiscounts, Sum<CuryARHistoryYtd.tranPtdDeposits, Sum<CuryARHistoryYtd.tranPtdRetainageReleased, Sum<CuryARHistoryYtd.tranPtdRetainageWithheld, Sum<CuryARHistoryYtd.tranPtdRGOL, Sum<CuryARHistoryYtd.curyTranPtdSales, Sum<CuryARHistoryYtd.curyTranPtdDrAdjustments, Sum<CuryARHistoryYtd.curyTranPtdFinCharges, Sum<CuryARHistoryYtd.curyTranPtdPayments, Sum<CuryARHistoryYtd.curyTranPtdCrAdjustments, Sum<CuryARHistoryYtd.curyTranPtdDiscounts, Sum<CuryARHistoryYtd.curyTranPtdDeposits, Sum<CuryARHistoryYtd.curyTranPtdRetainageReleased, Sum<CuryARHistoryYtd.curyTranPtdRetainageWithheld>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
public class CuryARHistoryYtdGrouped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? AccountID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? SubID { get; set; }

  [Customer(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual string FinPeriodID { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdSales))]
  public virtual Decimal? FinPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdDrAdjustments))]
  public virtual Decimal? FinPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdFinCharges))]
  public virtual Decimal? FinPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdPayments))]
  public virtual Decimal? FinPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdCrAdjustments))]
  public virtual Decimal? FinPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdDiscounts))]
  public virtual Decimal? FinPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdDeposits))]
  public virtual Decimal? FinYtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdRetainageReleased))]
  public virtual Decimal? FinYtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdRetainageWithheld))]
  public virtual Decimal? FinYtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.finPtdRGOL))]
  public virtual Decimal? FinPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdSales))]
  public virtual Decimal? CuryFinPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdDrAdjustments))]
  public virtual Decimal? CuryFinPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdFinCharges))]
  public virtual Decimal? CuryFinPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdPayments))]
  public virtual Decimal? CuryFinPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdCrAdjustments))]
  public virtual Decimal? CuryFinPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdDiscounts))]
  public virtual Decimal? CuryFinPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdDeposits))]
  public virtual Decimal? CuryFinYtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdRetainageReleased))]
  public virtual Decimal? CuryFinYtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyFinPtdRetainageWithheld))]
  public virtual Decimal? CuryFinYtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdSales))]
  public virtual Decimal? TranPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdDrAdjustments))]
  public virtual Decimal? TranPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdFinCharges))]
  public virtual Decimal? TranPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdPayments))]
  public virtual Decimal? TranPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdCrAdjustments))]
  public virtual Decimal? TranPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdDiscounts))]
  public virtual Decimal? TranPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdDeposits))]
  public virtual Decimal? TranYtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdRetainageReleased))]
  public virtual Decimal? TranYtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdRetainageWithheld))]
  public virtual Decimal? TranYtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.tranPtdRGOL))]
  public virtual Decimal? TranPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdSales))]
  public virtual Decimal? CuryTranPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdDrAdjustments))]
  public virtual Decimal? CuryTranPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdFinCharges))]
  public virtual Decimal? CuryTranPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdPayments))]
  public virtual Decimal? CuryTranPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdCrAdjustments))]
  public virtual Decimal? CuryTranPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdDiscounts))]
  public virtual Decimal? CuryTranPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdDeposits))]
  public virtual Decimal? CuryTranYtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdRetainageReleased))]
  public virtual Decimal? CuryTranYtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryYtd.curyTranPtdRetainageWithheld))]
  public virtual Decimal? CuryTranYtdRetainageWithheldSum { get; set; }

  public class PK : 
    PrimaryKeyOf<CuryARHistoryYtdGrouped>.By<CuryARHistoryYtdGrouped.branchID, CuryARHistoryYtdGrouped.accountID, CuryARHistoryYtdGrouped.subID, CuryARHistoryYtdGrouped.customerID, CuryARHistoryYtdGrouped.curyID, CuryARHistoryYtdGrouped.finPeriodID>
  {
    public static CuryARHistoryYtdGrouped Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string curyID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (CuryARHistoryYtdGrouped) PrimaryKeyOf<CuryARHistoryYtdGrouped>.By<CuryARHistoryYtdGrouped.branchID, CuryARHistoryYtdGrouped.accountID, CuryARHistoryYtdGrouped.subID, CuryARHistoryYtdGrouped.customerID, CuryARHistoryYtdGrouped.curyID, CuryARHistoryYtdGrouped.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) curyID, (object) finPeriodID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtdGrouped.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtdGrouped.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtdGrouped.subID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryYtdGrouped.customerID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryYtdGrouped.curyID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPeriodID>
  {
  }

  public abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdSales>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdFinCharges>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdPayments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdDiscounts>
  {
  }

  public abstract class finPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdRGOL>
  {
  }

  public abstract class curyFinPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdSales>
  {
  }

  public abstract class curyFinPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdDrAdjustments>
  {
  }

  public abstract class curyFinPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdFinCharges>
  {
  }

  public abstract class curyFinPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdPayments>
  {
  }

  public abstract class curyFinPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdCrAdjustments>
  {
  }

  public abstract class curyFinPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdDiscounts>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdSales>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdFinCharges>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdPayments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdDiscounts>
  {
  }

  public abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdRGOL>
  {
  }

  public abstract class curyTranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdSales>
  {
  }

  public abstract class curyTranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdDrAdjustments>
  {
  }

  public abstract class curyTranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdFinCharges>
  {
  }

  public abstract class curyTranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdPayments>
  {
  }

  public abstract class curyTranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdCrAdjustments>
  {
  }

  public abstract class curyTranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdDiscounts>
  {
  }

  public abstract class finPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdSalesSum>
  {
  }

  public abstract class finPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdDrAdjustmentsSum>
  {
  }

  public abstract class finPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdFinChargesSum>
  {
  }

  public abstract class finPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdPaymentsSum>
  {
  }

  public abstract class finPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdCrAdjustmentsSum>
  {
  }

  public abstract class finPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdDiscountsSum>
  {
  }

  public abstract class finYtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finYtdDepositsSum>
  {
  }

  public abstract class finYtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finYtdRetainageReleasedSum>
  {
  }

  public abstract class finYtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finYtdRetainageWithheldSum>
  {
  }

  public abstract class finPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.finPtdRGOLSum>
  {
  }

  public abstract class curyFinPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdSalesSum>
  {
  }

  public abstract class curyFinPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdDrAdjustmentsSum>
  {
  }

  public abstract class curyFinPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdFinChargesSum>
  {
  }

  public abstract class curyFinPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdPaymentsSum>
  {
  }

  public abstract class curyFinPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdCrAdjustmentsSum>
  {
  }

  public abstract class curyFinPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinPtdDiscountsSum>
  {
  }

  public abstract class curyFinYtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinYtdDepositsSum>
  {
  }

  public abstract class curyFinYtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinYtdRetainageReleasedSum>
  {
  }

  public abstract class curyFinYtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyFinYtdRetainageWithheldSum>
  {
  }

  public abstract class tranPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdSalesSum>
  {
  }

  public abstract class tranPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdDrAdjustmentsSum>
  {
  }

  public abstract class tranPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdFinChargesSum>
  {
  }

  public abstract class tranPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdPaymentsSum>
  {
  }

  public abstract class tranPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdCrAdjustmentsSum>
  {
  }

  public abstract class tranPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdDiscountsSum>
  {
  }

  public abstract class tranYtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranYtdDepositsSum>
  {
  }

  public abstract class tranYtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranYtdRetainageReleasedSum>
  {
  }

  public abstract class tranYtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranYtdRetainageWithheldSum>
  {
  }

  public abstract class tranPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.tranPtdRGOLSum>
  {
  }

  public abstract class curyTranPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdSalesSum>
  {
  }

  public abstract class curyTranPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdDrAdjustmentsSum>
  {
  }

  public abstract class curyTranPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdFinChargesSum>
  {
  }

  public abstract class curyTranPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdPaymentsSum>
  {
  }

  public abstract class curyTranPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdCrAdjustmentsSum>
  {
  }

  public abstract class curyTranPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranPtdDiscountsSum>
  {
  }

  public abstract class curyTranYtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranYtdDepositsSum>
  {
  }

  public abstract class curyTranYtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranYtdRetainageReleasedSum>
  {
  }

  public abstract class curyTranYtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryYtdGrouped.curyTranYtdRetainageWithheldSum>
  {
  }
}
