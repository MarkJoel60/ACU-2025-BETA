// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCurrentBalanceUnreleasedSum
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

[PXProjection(typeof (Select4<ARCurrentBalanceUnreleased, Aggregate<GroupBy<ARCurrentBalanceUnreleased.branchID, GroupBy<ARCurrentBalanceUnreleased.customerID, GroupBy<ARCurrentBalanceUnreleased.customerLocationID, GroupBy<ARCurrentBalanceUnreleased.unreleasedBal, Sum<ARCurrentBalanceUnreleased.unreleasedBalSigned>>>>>>>), Persistent = false)]
[PXHidden]
public class ARCurrentBalanceUnreleasedSum : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlTable = typeof (ARCurrentBalanceUnreleased))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlTable = typeof (ARCurrentBalanceUnreleased))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlTable = typeof (ARCurrentBalanceUnreleased))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARCurrentBalanceUnreleased))]
  public virtual Decimal? UnreleasedBal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (ARCurrentBalanceUnreleased.unreleasedBalSigned))]
  public virtual Decimal? UnreleasedBalSum { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARCurrentBalanceUnreleasedSum>.By<ARCurrentBalanceUnreleasedSum.branchID, ARCurrentBalanceUnreleasedSum.customerID, ARCurrentBalanceUnreleasedSum.customerLocationID>
  {
    public static ARCurrentBalanceUnreleasedSum Find(
      PXGraph graph,
      int branchID,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARCurrentBalanceUnreleasedSum) PrimaryKeyOf<ARCurrentBalanceUnreleasedSum>.By<ARCurrentBalanceUnreleasedSum.branchID, ARCurrentBalanceUnreleasedSum.customerID, ARCurrentBalanceUnreleasedSum.customerLocationID>.FindBy(graph, (object) branchID, (object) customerID, (object) customerLocationID, options);
    }
  }

  public abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceUnreleasedSum.branchID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceUnreleasedSum.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceUnreleasedSum.customerLocationID>
  {
  }

  public abstract class unreleasedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceUnreleasedSum.unreleasedBal>
  {
  }

  public abstract class unreleasedBalSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceUnreleasedSum.unreleasedBalSum>
  {
  }
}
