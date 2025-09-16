// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARBalancesSharedCredit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("AR Balance Shared Credit")]
[PXProjection(typeof (Select5<ARBalances, InnerJoin<CustomerSharedCredit, On<CustomerSharedCredit.bAccountID, Equal<ARBalances.customerID>>>, Where<CustomerSharedCredit.sharedCreditCustomerID, Equal<CustomerSharedCredit.sharedCreditCustomerID>, Or<CustomerSharedCredit.bAccountID, Equal<ARBalances.customerID>>>, Aggregate<GroupBy<CustomerSharedCredit.sharedCreditCustomerID, GroupBy<CustomerSharedCredit.creditLimit, Sum<ARBalances.currentBal, Sum<ARBalances.totalOpenOrders, Sum<ARBalances.totalPrepayments, Sum<ARBalances.totalShipped, Sum<ARBalances.unreleasedBal>>>>>>>>>))]
[Serializable]
public class ARBalancesSharedCredit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (ARBalances))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlTable = typeof (CustomerSharedCredit))]
  public virtual int? SharedCreditCustomerID { get; set; }

  [PXDBString(1, IsFixed = true, BqlTable = typeof (CustomerSharedCredit))]
  [PX.Objects.AR.CreditRule]
  [PXUIField(DisplayName = "Credit Verification")]
  public virtual 
  #nullable disable
  string CreditRule { get; set; }

  [PXDBBaseCury(null, null, BqlTable = typeof (CustomerSharedCredit))]
  [PXUIField(DisplayName = "Credit Limit")]
  public virtual Decimal? CreditLimit { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARBalances))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CurrentBal { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARBalances))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnreleasedBal { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARBalances))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalPrepayments { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARBalances))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalOpenOrders { get; set; }

  [PXDBDecimal(4, BqlTable = typeof (ARBalances))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalShipped { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARBalancesSharedCredit.branchID>
  {
  }

  public abstract class sharedCreditCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARBalancesSharedCredit.sharedCreditCustomerID>
  {
  }

  public abstract class creditRule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARBalancesSharedCredit.creditRule>
  {
  }

  public abstract class creditLimit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesSharedCredit.creditLimit>
  {
  }

  public abstract class currentBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesSharedCredit.currentBal>
  {
  }

  public abstract class unreleasedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesSharedCredit.unreleasedBal>
  {
  }

  public abstract class totalPrepayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesSharedCredit.totalPrepayments>
  {
  }

  public abstract class totalOpenOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesSharedCredit.totalOpenOrders>
  {
  }

  public abstract class totalShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesSharedCredit.totalShipped>
  {
  }
}
