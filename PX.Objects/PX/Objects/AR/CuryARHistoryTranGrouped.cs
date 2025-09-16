// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CuryARHistoryTranGrouped
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

[PXProjection(typeof (Select5<CuryARHistory, LeftJoin<CuryARHistoryTran, On<CuryARHistoryTran.customerID, Equal<CuryARHistory.customerID>, And<CuryARHistoryTran.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistoryTran.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistoryTran.subID, Equal<CuryARHistory.subID>, And<CuryARHistoryTran.tranPeriodID, Equal<CuryARHistory.finPeriodID>, And<CuryARHistoryTran.curyID, Equal<CuryARHistory.curyID>>>>>>>>, Aggregate<GroupBy<CuryARHistory.branchID, GroupBy<CuryARHistory.accountID, GroupBy<CuryARHistory.subID, GroupBy<CuryARHistory.curyID, GroupBy<CuryARHistory.customerID, GroupBy<CuryARHistory.finPeriodID, GroupBy<CuryARHistory.finPtdSales, GroupBy<CuryARHistory.finPtdPayments, GroupBy<CuryARHistory.finPtdDrAdjustments, GroupBy<CuryARHistory.finPtdCrAdjustments, GroupBy<CuryARHistory.finPtdDiscounts, GroupBy<CuryARHistory.finPtdRGOL, GroupBy<CuryARHistory.finPtdFinCharges, GroupBy<CuryARHistory.finPtdDeposits, GroupBy<CuryARHistory.finPtdRetainageWithheld, GroupBy<CuryARHistory.finPtdRetainageReleased, GroupBy<CuryARHistory.curyFinPtdSales, GroupBy<CuryARHistory.curyFinPtdPayments, GroupBy<CuryARHistory.curyFinPtdDrAdjustments, GroupBy<CuryARHistory.curyFinPtdCrAdjustments, GroupBy<CuryARHistory.curyFinPtdDiscounts, GroupBy<CuryARHistory.curyFinPtdFinCharges, GroupBy<CuryARHistory.curyFinPtdDeposits, GroupBy<CuryARHistory.curyFinPtdRetainageWithheld, GroupBy<CuryARHistory.curyFinPtdRetainageReleased, Sum<CuryARHistoryTran.ptdSales, Sum<CuryARHistoryTran.ptdPayments, Sum<CuryARHistoryTran.ptdDrAdjustments, Sum<CuryARHistoryTran.ptdCrAdjustments, Sum<CuryARHistoryTran.ptdDiscounts, Sum<CuryARHistoryTran.ptdRGOL, Sum<CuryARHistoryTran.ptdFinCharges, Sum<CuryARHistoryTran.ptdDeposits, Sum<CuryARHistoryTran.ptdRetainageWithheld, Sum<CuryARHistoryTran.ptdRetainageReleased, Sum<CuryARHistoryTran.curyPtdSales, Sum<CuryARHistoryTran.curyPtdPayments, Sum<CuryARHistoryTran.curyPtdDrAdjustments, Sum<CuryARHistoryTran.curyPtdCrAdjustments, Sum<CuryARHistoryTran.curyPtdDiscounts, Sum<CuryARHistoryTran.curyPtdFinCharges, Sum<CuryARHistoryTran.curyPtdDeposits, Sum<CuryARHistoryTran.curyPtdRetainageWithheld, Sum<CuryARHistoryTran.curyPtdRetainageReleased>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
public class CuryARHistoryTranGrouped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? AccountID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? SubID { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [Customer(IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual int? CustomerID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlTable = typeof (CuryARHistory))]
  public virtual string FinPeriodID { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? TranPtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryTranPtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdSales))]
  public virtual Decimal? TranPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdPayments))]
  public virtual Decimal? TranPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdDrAdjustments))]
  public virtual Decimal? TranPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdCrAdjustments))]
  public virtual Decimal? TranPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdDiscounts))]
  public virtual Decimal? TranPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdRGOL))]
  public virtual Decimal? TranPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdFinCharges))]
  public virtual Decimal? TranPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdDeposits))]
  public virtual Decimal? TranPtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdRetainageWithheld))]
  public virtual Decimal? TranPtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdRetainageReleased))]
  public virtual Decimal? TranPtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdSales))]
  public virtual Decimal? CuryTranPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdPayments))]
  public virtual Decimal? CuryTranPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdDrAdjustments))]
  public virtual Decimal? CuryTranPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdCrAdjustments))]
  public virtual Decimal? CuryTranPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdDiscounts))]
  public virtual Decimal? CuryTranPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdFinCharges))]
  public virtual Decimal? CuryTranPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdDeposits))]
  public virtual Decimal? CuryTranPtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdRetainageWithheld))]
  public virtual Decimal? CuryTranPtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdRetainageReleased))]
  public virtual Decimal? CuryTranPtdRetainageReleasedSum { get; set; }

  public class PK : 
    PrimaryKeyOf<CuryARHistoryTranGrouped>.By<CuryARHistoryTranGrouped.branchID, CuryARHistoryTranGrouped.accountID, CuryARHistoryTranGrouped.subID, CuryARHistoryTranGrouped.curyID, CuryARHistoryTranGrouped.customerID, CuryARHistoryTranGrouped.finPeriodID>
  {
    public static CuryARHistoryTranGrouped Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      string curyID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (CuryARHistoryTranGrouped) PrimaryKeyOf<CuryARHistoryTranGrouped>.By<CuryARHistoryTranGrouped.branchID, CuryARHistoryTranGrouped.accountID, CuryARHistoryTranGrouped.subID, CuryARHistoryTranGrouped.curyID, CuryARHistoryTranGrouped.customerID, CuryARHistoryTranGrouped.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) curyID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTranGrouped.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTranGrouped.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTranGrouped.subID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryTranGrouped.curyID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryTranGrouped.customerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.finPeriodID>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdSales>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdPayments>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdDiscounts>
  {
  }

  public abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdRGOL>
  {
  }

  public abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdFinCharges>
  {
  }

  public abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdDeposits>
  {
  }

  public abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdRetainageWithheld>
  {
  }

  public abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdRetainageReleased>
  {
  }

  public abstract class curyTranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdSales>
  {
  }

  public abstract class curyTranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdPayments>
  {
  }

  public abstract class curyTranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdDrAdjustments>
  {
  }

  public abstract class curyTranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdCrAdjustments>
  {
  }

  public abstract class curyTranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdDiscounts>
  {
  }

  public abstract class curyTranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdFinCharges>
  {
  }

  public abstract class curyTranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdDeposits>
  {
  }

  public abstract class curyTranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdRetainageWithheld>
  {
  }

  public abstract class curyTranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdRetainageReleased>
  {
  }

  public abstract class tranPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdSalesSum>
  {
  }

  public abstract class tranPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdPaymentsSum>
  {
  }

  public abstract class tranPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdDrAdjustmentsSum>
  {
  }

  public abstract class tranPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdCrAdjustmentsSum>
  {
  }

  public abstract class tranPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdDiscountsSum>
  {
  }

  public abstract class tranPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdRGOLSum>
  {
  }

  public abstract class tranPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdFinChargesSum>
  {
  }

  public abstract class tranPtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdDepositsSum>
  {
  }

  public abstract class tranPtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdRetainageWithheldSum>
  {
  }

  public abstract class tranPtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.tranPtdRetainageReleasedSum>
  {
  }

  public abstract class curyTranPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdSalesSum>
  {
  }

  public abstract class curyTranPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdPaymentsSum>
  {
  }

  public abstract class curyTranPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdDrAdjustmentsSum>
  {
  }

  public abstract class curyTranPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdCrAdjustmentsSum>
  {
  }

  public abstract class curyTranPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdDiscountsSum>
  {
  }

  public abstract class curyTranPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdFinChargesSum>
  {
  }

  public abstract class curyTranPtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdDepositsSum>
  {
  }

  public abstract class curyTranPtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdRetainageWithheldSum>
  {
  }

  public abstract class curyTranPtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryTranGrouped.curyTranPtdRetainageReleasedSum>
  {
  }
}
