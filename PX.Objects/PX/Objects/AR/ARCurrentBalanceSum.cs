// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCurrentBalanceSum
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

[PXProjection(typeof (Select4<ARCurrentBalance, Aggregate<GroupBy<ARCurrentBalance.branchID, GroupBy<ARCurrentBalance.customerID, GroupBy<ARCurrentBalance.customerLocationID, GroupBy<ARCurrentBalance.currentBal, Sum<ARCurrentBalance.currentBalSigned>>>>>>>), Persistent = false)]
[PXHidden]
public class ARCurrentBalanceSum : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlTable = typeof (ARCurrentBalance))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlTable = typeof (ARCurrentBalance))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlTable = typeof (ARCurrentBalance))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARCurrentBalance))]
  public virtual Decimal? CurrentBal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARCurrentBalance.currentBalSigned))]
  public virtual Decimal? CurrentBalSum { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARCurrentBalanceSum>.By<ARCurrentBalanceSum.branchID, ARCurrentBalanceSum.customerID, ARCurrentBalanceSum.customerLocationID>
  {
    public static ARCurrentBalanceSum Find(
      PXGraph graph,
      int branchID,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARCurrentBalanceSum) PrimaryKeyOf<ARCurrentBalanceSum>.By<ARCurrentBalanceSum.branchID, ARCurrentBalanceSum.customerID, ARCurrentBalanceSum.customerLocationID>.FindBy(graph, (object) branchID, (object) customerID, (object) customerLocationID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCurrentBalanceSum.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCurrentBalanceSum.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceSum.customerLocationID>
  {
  }

  public abstract class currentBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceSum.currentBal>
  {
  }

  public abstract class currentBalSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceSum.currentBalSum>
  {
  }
}
