// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCurrentBalanceOpenOrdersSum
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

[PXProjection(typeof (Select4<ARCurrentBalanceOpenOrders, Aggregate<GroupBy<ARCurrentBalanceOpenOrders.branchID, GroupBy<ARCurrentBalanceOpenOrders.customerID, GroupBy<ARCurrentBalanceOpenOrders.customerLocationID, Sum<ARCurrentBalanceOpenOrders.unbilledOrderTotal>>>>>>), Persistent = false)]
[PXHidden]
public class ARCurrentBalanceOpenOrdersSum : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlTable = typeof (ARCurrentBalanceOpenOrders))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlTable = typeof (ARCurrentBalanceOpenOrders))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlTable = typeof (ARCurrentBalanceOpenOrders))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBDecimal(BqlField = typeof (ARCurrentBalanceOpenOrders.unbilledOrderTotal))]
  public virtual Decimal? UnbilledOrderTotal { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARCurrentBalanceOpenOrdersSum>.By<ARCurrentBalanceOpenOrdersSum.branchID, ARCurrentBalanceOpenOrdersSum.customerID, ARCurrentBalanceOpenOrdersSum.customerLocationID>
  {
    public static ARCurrentBalanceOpenOrdersSum Find(
      PXGraph graph,
      int branchID,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARCurrentBalanceOpenOrdersSum) PrimaryKeyOf<ARCurrentBalanceOpenOrdersSum>.By<ARCurrentBalanceOpenOrdersSum.branchID, ARCurrentBalanceOpenOrdersSum.customerID, ARCurrentBalanceOpenOrdersSum.customerLocationID>.FindBy(graph, (object) branchID, (object) customerID, (object) customerLocationID, options);
    }
  }

  public abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrdersSum.branchID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrdersSum.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrdersSum.customerLocationID>
  {
  }

  public abstract class unbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrdersSum.unbilledOrderTotal>
  {
  }
}
