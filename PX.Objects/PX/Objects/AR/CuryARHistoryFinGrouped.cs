// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CuryARHistoryFinGrouped
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

[PXProjection(typeof (Select5<CuryARHistory, LeftJoin<CuryARHistoryTran, On<CuryARHistoryTran.customerID, Equal<CuryARHistory.customerID>, And<CuryARHistoryTran.branchID, Equal<CuryARHistory.branchID>, And<CuryARHistoryTran.accountID, Equal<CuryARHistory.accountID>, And<CuryARHistoryTran.subID, Equal<CuryARHistory.subID>, And<CuryARHistoryTran.finPeriodID, Equal<CuryARHistory.finPeriodID>, And<CuryARHistoryTran.curyID, Equal<CuryARHistory.curyID>>>>>>>>, Aggregate<GroupBy<CuryARHistory.branchID, GroupBy<CuryARHistory.accountID, GroupBy<CuryARHistory.subID, GroupBy<CuryARHistory.curyID, GroupBy<CuryARHistory.customerID, GroupBy<CuryARHistory.finPeriodID, GroupBy<CuryARHistory.finPtdSales, GroupBy<CuryARHistory.finPtdPayments, GroupBy<CuryARHistory.finPtdDrAdjustments, GroupBy<CuryARHistory.finPtdCrAdjustments, GroupBy<CuryARHistory.finPtdDiscounts, GroupBy<CuryARHistory.finPtdRGOL, GroupBy<CuryARHistory.finPtdFinCharges, GroupBy<CuryARHistory.finPtdDeposits, GroupBy<CuryARHistory.finPtdRetainageWithheld, GroupBy<CuryARHistory.finPtdRetainageReleased, GroupBy<CuryARHistory.curyFinPtdSales, GroupBy<CuryARHistory.curyFinPtdPayments, GroupBy<CuryARHistory.curyFinPtdDrAdjustments, GroupBy<CuryARHistory.curyFinPtdCrAdjustments, GroupBy<CuryARHistory.curyFinPtdDiscounts, GroupBy<CuryARHistory.curyFinPtdFinCharges, GroupBy<CuryARHistory.curyFinPtdDeposits, GroupBy<CuryARHistory.curyFinPtdRetainageWithheld, GroupBy<CuryARHistory.curyFinPtdRetainageReleased, Sum<CuryARHistoryTran.ptdSales, Sum<CuryARHistoryTran.ptdPayments, Sum<CuryARHistoryTran.ptdDrAdjustments, Sum<CuryARHistoryTran.ptdCrAdjustments, Sum<CuryARHistoryTran.ptdDiscounts, Sum<CuryARHistoryTran.ptdRGOL, Sum<CuryARHistoryTran.ptdFinCharges, Sum<CuryARHistoryTran.ptdDeposits, Sum<CuryARHistoryTran.ptdRetainageWithheld, Sum<CuryARHistoryTran.ptdRetainageReleased, Sum<CuryARHistoryTran.curyPtdSales, Sum<CuryARHistoryTran.curyPtdPayments, Sum<CuryARHistoryTran.curyPtdDrAdjustments, Sum<CuryARHistoryTran.curyPtdCrAdjustments, Sum<CuryARHistoryTran.curyPtdDiscounts, Sum<CuryARHistoryTran.curyPtdFinCharges, Sum<CuryARHistoryTran.curyPtdDeposits, Sum<CuryARHistoryTran.curyPtdRetainageWithheld, Sum<CuryARHistoryTran.curyPtdRetainageReleased>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
public class CuryARHistoryFinGrouped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  public virtual Decimal? FinPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? FinPtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (CuryARHistory))]
  public virtual Decimal? CuryFinPtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdSales))]
  public virtual Decimal? FinPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdPayments))]
  public virtual Decimal? FinPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdDrAdjustments))]
  public virtual Decimal? FinPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdCrAdjustments))]
  public virtual Decimal? FinPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdDiscounts))]
  public virtual Decimal? FinPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdRGOL))]
  public virtual Decimal? FinPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdFinCharges))]
  public virtual Decimal? FinPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdDeposits))]
  public virtual Decimal? FinPtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdRetainageWithheld))]
  public virtual Decimal? FinPtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.ptdRetainageReleased))]
  public virtual Decimal? FinPtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdSales))]
  public virtual Decimal? CuryFinPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdPayments))]
  public virtual Decimal? CuryFinPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdDrAdjustments))]
  public virtual Decimal? CuryFinPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdCrAdjustments))]
  public virtual Decimal? CuryFinPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdDiscounts))]
  public virtual Decimal? CuryFinPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdFinCharges))]
  public virtual Decimal? CuryFinPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdDeposits))]
  public virtual Decimal? CuryFinPtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdRetainageWithheld))]
  public virtual Decimal? CuryFinPtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (CuryARHistoryTran.curyPtdRetainageReleased))]
  public virtual Decimal? CuryFinPtdRetainageReleasedSum { get; set; }

  public class PK : 
    PrimaryKeyOf<CuryARHistoryFinGrouped>.By<CuryARHistoryFinGrouped.branchID, CuryARHistoryFinGrouped.accountID, CuryARHistoryFinGrouped.subID, CuryARHistoryFinGrouped.curyID, CuryARHistoryFinGrouped.customerID, CuryARHistoryFinGrouped.finPeriodID>
  {
    public static CuryARHistoryFinGrouped Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      string curyID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (CuryARHistoryFinGrouped) PrimaryKeyOf<CuryARHistoryFinGrouped>.By<CuryARHistoryFinGrouped.branchID, CuryARHistoryFinGrouped.accountID, CuryARHistoryFinGrouped.subID, CuryARHistoryFinGrouped.curyID, CuryARHistoryFinGrouped.customerID, CuryARHistoryFinGrouped.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) curyID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryFinGrouped.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryFinGrouped.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryFinGrouped.subID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistoryFinGrouped.curyID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistoryFinGrouped.customerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPeriodID>
  {
  }

  public abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdSales>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdPayments>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdDiscounts>
  {
  }

  public abstract class finPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdRGOL>
  {
  }

  public abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdFinCharges>
  {
  }

  public abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdDeposits>
  {
  }

  public abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdRetainageWithheld>
  {
  }

  public abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdRetainageReleased>
  {
  }

  public abstract class curyFinPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdSales>
  {
  }

  public abstract class curyFinPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdPayments>
  {
  }

  public abstract class curyFinPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdDrAdjustments>
  {
  }

  public abstract class curyFinPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdCrAdjustments>
  {
  }

  public abstract class curyFinPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdDiscounts>
  {
  }

  public abstract class curyFinPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdFinCharges>
  {
  }

  public abstract class curyFinPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdDeposits>
  {
  }

  public abstract class curyFinPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdRetainageWithheld>
  {
  }

  public abstract class curyFinPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdRetainageReleased>
  {
  }

  public abstract class finPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdSalesSum>
  {
  }

  public abstract class finPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdPaymentsSum>
  {
  }

  public abstract class finPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdDrAdjustmentsSum>
  {
  }

  public abstract class finPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdCrAdjustmentsSum>
  {
  }

  public abstract class finPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdDiscountsSum>
  {
  }

  public abstract class finPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdRGOLSum>
  {
  }

  public abstract class finPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdFinChargesSum>
  {
  }

  public abstract class finPtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdDepositsSum>
  {
  }

  public abstract class finPtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdRetainageWithheldSum>
  {
  }

  public abstract class finPtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.finPtdRetainageReleasedSum>
  {
  }

  public abstract class curyFinPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdSalesSum>
  {
  }

  public abstract class curyFinPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdPaymentsSum>
  {
  }

  public abstract class curyFinPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdDrAdjustmentsSum>
  {
  }

  public abstract class curyFinPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdCrAdjustmentsSum>
  {
  }

  public abstract class curyFinPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdDiscountsSum>
  {
  }

  public abstract class curyFinPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdFinChargesSum>
  {
  }

  public abstract class curyFinPtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdDepositsSum>
  {
  }

  public abstract class curyFinPtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdRetainageWithheldSum>
  {
  }

  public abstract class curyFinPtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistoryFinGrouped.curyFinPtdRetainageReleasedSum>
  {
  }
}
