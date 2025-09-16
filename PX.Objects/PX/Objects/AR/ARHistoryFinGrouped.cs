// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistoryFinGrouped
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

[PXProjection(typeof (Select5<ARHistory, LeftJoin<ARHistoryTran, On<ARHistoryTran.customerID, Equal<ARHistory.customerID>, And<ARHistoryTran.branchID, Equal<ARHistory.branchID>, And<ARHistoryTran.accountID, Equal<ARHistory.accountID>, And<ARHistoryTran.subID, Equal<ARHistory.subID>, And<ARHistoryTran.finPeriodID, Equal<ARHistory.finPeriodID>>>>>>>, Aggregate<GroupBy<ARHistory.branchID, GroupBy<ARHistory.accountID, GroupBy<ARHistory.subID, GroupBy<ARHistory.customerID, GroupBy<ARHistory.finPeriodID, GroupBy<ARHistory.finPtdSales, GroupBy<ARHistory.finPtdPayments, GroupBy<ARHistory.finPtdDrAdjustments, GroupBy<ARHistory.finPtdCrAdjustments, GroupBy<ARHistory.finPtdDiscounts, GroupBy<ARHistory.finPtdItemDiscounts, GroupBy<ARHistory.finPtdRGOL, GroupBy<ARHistory.finPtdFinCharges, GroupBy<ARHistory.finPtdDeposits, GroupBy<ARHistory.finPtdRetainageWithheld, GroupBy<ARHistory.finPtdRetainageReleased, Sum<ARHistoryTran.ptdSales, Sum<ARHistoryTran.ptdPayments, Sum<ARHistoryTran.ptdDrAdjustments, Sum<ARHistoryTran.ptdCrAdjustments, Sum<ARHistoryTran.ptdDiscounts, Sum<ARHistoryTran.ptdItemDiscounts, Sum<ARHistoryTran.ptdRGOL, Sum<ARHistoryTran.ptdFinCharges, Sum<ARHistoryTran.ptdDeposits, Sum<ARHistoryTran.ptdRetainageWithheld, Sum<ARHistoryTran.ptdRetainageReleased>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
public class ARHistoryFinGrouped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  public virtual Decimal? FinPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdItemDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdSales))]
  public virtual Decimal? FinPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdPayments))]
  public virtual Decimal? FinPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdDrAdjustments))]
  public virtual Decimal? FinPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdCrAdjustments))]
  public virtual Decimal? FinPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdDiscounts))]
  public virtual Decimal? FinPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdItemDiscounts))]
  public virtual Decimal? FinPtdItemDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdRGOL))]
  public virtual Decimal? FinPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdFinCharges))]
  public virtual Decimal? FinPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdDeposits))]
  public virtual Decimal? FinPtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdRetainageWithheld))]
  public virtual Decimal? FinPtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryTran.ptdRetainageReleased))]
  public virtual Decimal? FinPtdRetainageReleasedSum { get; set; }

  public class PK : 
    PrimaryKeyOf<ARHistoryFinGrouped>.By<ARHistoryFinGrouped.branchID, ARHistoryFinGrouped.accountID, ARHistoryFinGrouped.subID, ARHistoryFinGrouped.customerID, ARHistoryFinGrouped.finPeriodID>
  {
    public static ARHistoryFinGrouped Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (ARHistoryFinGrouped) PrimaryKeyOf<ARHistoryFinGrouped>.By<ARHistoryFinGrouped.branchID, ARHistoryFinGrouped.accountID, ARHistoryFinGrouped.subID, ARHistoryFinGrouped.customerID, ARHistoryFinGrouped.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryFinGrouped.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryFinGrouped.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryFinGrouped.subID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryFinGrouped.customerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPeriodID>
  {
  }

  public abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdSales>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdPayments>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdDiscounts>
  {
  }

  public abstract class finPtdItemDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdItemDiscounts>
  {
  }

  public abstract class finPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdRGOL>
  {
  }

  public abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdFinCharges>
  {
  }

  public abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdDeposits>
  {
  }

  public abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdRetainageWithheld>
  {
  }

  public abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdRetainageReleased>
  {
  }

  public abstract class finPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdSalesSum>
  {
  }

  public abstract class finPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdPaymentsSum>
  {
  }

  public abstract class finPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdDrAdjustmentsSum>
  {
  }

  public abstract class finPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdCrAdjustmentsSum>
  {
  }

  public abstract class finPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdDiscountsSum>
  {
  }

  public abstract class finPtdItemDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdItemDiscountsSum>
  {
  }

  public abstract class finPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdRGOLSum>
  {
  }

  public abstract class finPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdFinChargesSum>
  {
  }

  public abstract class finPtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdDepositsSum>
  {
  }

  public abstract class finPtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdRetainageWithheldSum>
  {
  }

  public abstract class finPtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryFinGrouped.finPtdRetainageReleasedSum>
  {
  }
}
