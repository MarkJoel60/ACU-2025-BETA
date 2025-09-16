// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistoryYtdGrouped
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

[PXProjection(typeof (Select5<ARHistory, LeftJoin<ARHistoryYtd, On<ARHistoryYtd.customerID, Equal<ARHistory.customerID>, And<ARHistoryYtd.branchID, Equal<ARHistory.branchID>, And<ARHistoryYtd.accountID, Equal<ARHistory.accountID>, And<ARHistoryYtd.subID, Equal<ARHistory.subID>, And<ARHistoryYtd.finPeriodID, LessEqual<ARHistory.finPeriodID>>>>>>>, Aggregate<GroupBy<ARHistory.branchID, GroupBy<ARHistory.accountID, GroupBy<ARHistory.subID, GroupBy<ARHistory.customerID, GroupBy<ARHistory.finPeriodID, GroupBy<ARHistory.finBegBalance, GroupBy<ARHistory.finPtdSales, GroupBy<ARHistory.finPtdDrAdjustments, GroupBy<ARHistory.finPtdFinCharges, GroupBy<ARHistory.finPtdPayments, GroupBy<ARHistory.finPtdCrAdjustments, GroupBy<ARHistory.finPtdDiscounts, GroupBy<ARHistory.finPtdRGOL, GroupBy<ARHistory.finYtdBalance, GroupBy<ARHistory.finYtdDeposits, GroupBy<ARHistory.finYtdRetainageReleased, GroupBy<ARHistory.finYtdRetainageWithheld, GroupBy<ARHistory.tranBegBalance, GroupBy<ARHistory.tranPtdSales, GroupBy<ARHistory.tranPtdDrAdjustments, GroupBy<ARHistory.tranPtdFinCharges, GroupBy<ARHistory.tranPtdPayments, GroupBy<ARHistory.tranPtdCrAdjustments, GroupBy<ARHistory.tranPtdDiscounts, GroupBy<ARHistory.tranPtdRGOL, GroupBy<ARHistory.tranYtdBalance, GroupBy<ARHistory.tranYtdDeposits, GroupBy<ARHistory.tranYtdRetainageReleased, GroupBy<ARHistory.tranYtdRetainageWithheld, Sum<ARHistoryYtd.finPtdSales, Sum<ARHistoryYtd.finPtdDrAdjustments, Sum<ARHistoryYtd.finPtdFinCharges, Sum<ARHistoryYtd.finPtdPayments, Sum<ARHistoryYtd.finPtdCrAdjustments, Sum<ARHistoryYtd.finPtdDiscounts, Sum<ARHistoryYtd.finPtdRGOL, Sum<ARHistoryYtd.finPtdDeposits, Sum<ARHistoryYtd.finPtdRetainageReleased, Sum<ARHistoryYtd.finPtdRetainageWithheld, Sum<ARHistoryYtd.tranPtdSales, Sum<ARHistoryYtd.tranPtdDrAdjustments, Sum<ARHistoryYtd.tranPtdFinCharges, Sum<ARHistoryYtd.tranPtdPayments, Sum<ARHistoryYtd.tranPtdCrAdjustments, Sum<ARHistoryYtd.tranPtdDiscounts, Sum<ARHistoryYtd.tranPtdRGOL, Sum<ARHistoryYtd.tranPtdDeposits, Sum<ARHistoryYtd.tranPtdRetainageReleased, Sum<ARHistoryYtd.tranPtdRetainageWithheld>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
[PXHidden]
public class ARHistoryYtdGrouped : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  public virtual Decimal? FinBegBalance { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinYtdBalance { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinYtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinYtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? FinYtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranBegBalance { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdSales { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdDrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdFinCharges { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdPayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdCrAdjustments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdDiscounts { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranPtdRGOL { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranYtdBalance { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranYtdDeposits { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranYtdRetainageReleased { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARHistory))]
  public virtual Decimal? TranYtdRetainageWithheld { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdSales))]
  public virtual Decimal? FinPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdDrAdjustments))]
  public virtual Decimal? FinPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdFinCharges))]
  public virtual Decimal? FinPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdPayments))]
  public virtual Decimal? FinPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdCrAdjustments))]
  public virtual Decimal? FinPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdDiscounts))]
  public virtual Decimal? FinPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdRGOL))]
  public virtual Decimal? FinPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdDeposits))]
  public virtual Decimal? FinYtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdRetainageReleased))]
  public virtual Decimal? FinYtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.finPtdRetainageWithheld))]
  public virtual Decimal? FinYtdRetainageWithheldSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdSales))]
  public virtual Decimal? TranPtdSalesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdDrAdjustments))]
  public virtual Decimal? TranPtdDrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdFinCharges))]
  public virtual Decimal? TranPtdFinChargesSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdPayments))]
  public virtual Decimal? TranPtdPaymentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdCrAdjustments))]
  public virtual Decimal? TranPtdCrAdjustmentsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdDiscounts))]
  public virtual Decimal? TranPtdDiscountsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdRGOL))]
  public virtual Decimal? TranPtdRGOLSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdDeposits))]
  public virtual Decimal? TranYtdDepositsSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdRetainageReleased))]
  public virtual Decimal? TranYtdRetainageReleasedSum { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARHistoryYtd.tranPtdRetainageWithheld))]
  public virtual Decimal? TranYtdRetainageWithheldSum { get; set; }

  public class PK : 
    PrimaryKeyOf<ARHistoryYtdGrouped>.By<ARHistoryYtdGrouped.branchID, ARHistoryYtdGrouped.accountID, ARHistoryYtdGrouped.subID, ARHistoryYtdGrouped.customerID, ARHistoryYtdGrouped.finPeriodID>
  {
    public static ARHistoryYtdGrouped Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (ARHistoryYtdGrouped) PrimaryKeyOf<ARHistoryYtdGrouped>.By<ARHistoryYtdGrouped.branchID, ARHistoryYtdGrouped.accountID, ARHistoryYtdGrouped.subID, ARHistoryYtdGrouped.customerID, ARHistoryYtdGrouped.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtdGrouped.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtdGrouped.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtdGrouped.subID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtdGrouped.customerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPeriodID>
  {
  }

  public abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finBegBalance>
  {
  }

  public abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdSales>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdFinCharges>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdPayments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdDiscounts>
  {
  }

  public abstract class finPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdRGOL>
  {
  }

  public abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finYtdBalance>
  {
  }

  public abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finYtdDeposits>
  {
  }

  public abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finYtdRetainageReleased>
  {
  }

  public abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finYtdRetainageWithheld>
  {
  }

  public abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranBegBalance>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdSales>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdFinCharges>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdPayments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdDiscounts>
  {
  }

  public abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdRGOL>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranYtdBalance>
  {
  }

  public abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranYtdDeposits>
  {
  }

  public abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranYtdRetainageReleased>
  {
  }

  public abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranYtdRetainageWithheld>
  {
  }

  public abstract class finPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdSalesSum>
  {
  }

  public abstract class finPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdDrAdjustmentsSum>
  {
  }

  public abstract class finPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdFinChargesSum>
  {
  }

  public abstract class finPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdPaymentsSum>
  {
  }

  public abstract class finPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdCrAdjustmentsSum>
  {
  }

  public abstract class finPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdDiscountsSum>
  {
  }

  public abstract class finPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finPtdRGOLSum>
  {
  }

  public abstract class finYtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finYtdDepositsSum>
  {
  }

  public abstract class finYtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finYtdRetainageReleasedSum>
  {
  }

  public abstract class finYtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.finYtdRetainageWithheldSum>
  {
  }

  public abstract class tranPtdSalesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdSalesSum>
  {
  }

  public abstract class tranPtdDrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdDrAdjustmentsSum>
  {
  }

  public abstract class tranPtdFinChargesSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdFinChargesSum>
  {
  }

  public abstract class tranPtdPaymentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdPaymentsSum>
  {
  }

  public abstract class tranPtdCrAdjustmentsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdCrAdjustmentsSum>
  {
  }

  public abstract class tranPtdDiscountsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdDiscountsSum>
  {
  }

  public abstract class tranPtdRGOLSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranPtdRGOLSum>
  {
  }

  public abstract class tranYtdDepositsSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranYtdDepositsSum>
  {
  }

  public abstract class tranYtdRetainageReleasedSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranYtdRetainageReleasedSum>
  {
  }

  public abstract class tranYtdRetainageWithheldSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtdGrouped.tranYtdRetainageWithheldSum>
  {
  }
}
