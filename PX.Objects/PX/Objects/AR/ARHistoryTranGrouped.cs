// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistoryTranGrouped
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

[PXProjection(typeof (Select5<ARHistory, LeftJoin<ARHistoryTran, On<ARHistoryTran.customerID, Equal<ARHistory.customerID>, And<ARHistoryTran.branchID, Equal<ARHistory.branchID>, And<ARHistoryTran.accountID, Equal<ARHistory.accountID>, And<ARHistoryTran.subID, Equal<ARHistory.subID>, And<ARHistoryTran.tranPeriodID, Equal<ARHistory.finPeriodID>>>>>>>, Aggregate<GroupBy<ARHistory.branchID, GroupBy<ARHistory.accountID, GroupBy<ARHistory.subID, GroupBy<ARHistory.customerID, GroupBy<ARHistory.finPeriodID, GroupBy<ARHistory.finPtdSales, GroupBy<ARHistory.finPtdPayments, GroupBy<ARHistory.finPtdDrAdjustments, GroupBy<ARHistory.finPtdCrAdjustments, GroupBy<ARHistory.finPtdDiscounts, GroupBy<ARHistory.finPtdItemDiscounts, GroupBy<ARHistory.finPtdRGOL, GroupBy<ARHistory.finPtdFinCharges, GroupBy<ARHistory.finPtdDeposits, GroupBy<ARHistory.finPtdRetainageWithheld, GroupBy<ARHistory.finPtdRetainageReleased, Sum<ARHistoryTran.ptdSales, Sum<ARHistoryTran.ptdPayments, Sum<ARHistoryTran.ptdDrAdjustments, Sum<ARHistoryTran.ptdCrAdjustments, Sum<ARHistoryTran.ptdDiscounts, Sum<ARHistoryTran.ptdItemDiscounts, Sum<ARHistoryTran.ptdRGOL, Sum<ARHistoryTran.ptdFinCharges, Sum<ARHistoryTran.ptdDeposits, Sum<ARHistoryTran.ptdRetainageWithheld, Sum<ARHistoryTran.ptdRetainageReleased>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
public class ARHistoryTranGrouped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (ARHistory))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (ARHistory))]
  public virtual int? AccountID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (ARHistory))]
  public virtual int? SubID { get; set; }

  [Customer(IsKey = true, BqlTable = typeof (ARHistory))]
  public virtual int? CustomerID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlTable = typeof (ARHistory))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdItemDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdSales))]
  public virtual Decimal? TranPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdPayments))]
  public virtual Decimal? TranPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdDrAdjustments))]
  public virtual Decimal? TranPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdCrAdjustments))]
  public virtual Decimal? TranPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdDiscounts))]
  public virtual Decimal? TranPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdItemDiscounts))]
  public virtual Decimal? TranPtdItemDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdRGOL))]
  public virtual Decimal? TranPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdFinCharges))]
  public virtual Decimal? TranPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdDeposits))]
  public virtual Decimal? TranPtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdRetainageWithheld))]
  public virtual Decimal? TranPtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdRetainageReleased))]
  public virtual Decimal? TranPtdRetainageReleasedSum { get; set; }

  public class PK : 
    PrimaryKeyOf<ARHistoryTranGrouped>.By<ARHistoryTranGrouped.branchID, ARHistoryTranGrouped.accountID, ARHistoryTranGrouped.subID, ARHistoryTranGrouped.customerID, ARHistoryTranGrouped.finPeriodID>
  {
    public static ARHistoryTranGrouped Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (ARHistoryTranGrouped) PrimaryKeyOf<ARHistoryTranGrouped>.By<ARHistoryTranGrouped.branchID, ARHistoryTranGrouped.accountID, ARHistoryTranGrouped.subID, ARHistoryTranGrouped.customerID, ARHistoryTranGrouped.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryTranGrouped.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryTranGrouped.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryTranGrouped.subID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryTranGrouped.customerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistoryTranGrouped.finPeriodID>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdSales>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdPayments>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdDiscounts>
  {
  }

  public abstract class tranPtdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdItemDiscounts>
  {
  }

  public abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdRGOL>
  {
  }

  public abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdFinCharges>
  {
  }

  public abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdDeposits>
  {
  }

  public abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdRetainageWithheld>
  {
  }

  public abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdRetainageReleased>
  {
  }

  public abstract class tranPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdSalesSum>
  {
  }

  public abstract class tranPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdPaymentsSum>
  {
  }

  public abstract class tranPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdDrAdjustmentsSum>
  {
  }

  public abstract class tranPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdCrAdjustmentsSum>
  {
  }

  public abstract class tranPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdDiscountsSum>
  {
  }

  public abstract class tranPtdItemDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdItemDiscountsSum>
  {
  }

  public abstract class tranPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdRGOLSum>
  {
  }

  public abstract class tranPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdFinChargesSum>
  {
  }

  public abstract class tranPtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdDepositsSum>
  {
  }

  public abstract class tranPtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdRetainageWithheldSum>
  {
  }

  public abstract class tranPtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryTranGrouped.tranPtdRetainageReleasedSum>
  {
  }
}
