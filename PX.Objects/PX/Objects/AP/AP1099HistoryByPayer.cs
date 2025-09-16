// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099HistoryByPayer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (SelectFromBase<AP1099BAccountHistory, TypeArrayOf<IFbqlJoin>.Empty>.Aggregate<To<GroupBy<AP1099BAccountHistory.bAccountID>, GroupBy<AP1099BAccountHistory.vendorID>, GroupBy<AP1099BAccountHistory.finYear>, GroupBy<AP1099BAccountHistory.boxNbr>, Sum<AP1099BAccountHistory.histAmt>>>))]
[PXCacheName("AP 1099 History by Payer")]
public class AP1099HistoryByPayer : AP1099BAccountHistory
{
  [Payer1099Selector(BqlField = typeof (AP1099BAccountHistory.bAccountID), IsKey = true)]
  public override int? BAccountID { get; set; }

  [Branch(null, null, true, true, false, BqlTable = typeof (AP1099BAccountHistory), Visible = false, Visibility = PXUIVisibility.Invisible)]
  public override int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (AP1099BAccountHistory))]
  public override int? VendorID { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true, BqlTable = typeof (AP1099BAccountHistory))]
  public override 
  #nullable disable
  string FinYear { get; set; }

  [PXDBShort(IsKey = true, BqlTable = typeof (AP1099BAccountHistory))]
  public override short? BoxNbr { get; set; }

  [PXDBBaseCury(null, null, BqlTable = typeof (AP1099BAccountHistory))]
  public override Decimal? HistAmt { get; set; }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099HistoryByPayer.bAccountID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099HistoryByPayer.branchID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099HistoryByPayer.vendorID>
  {
  }

  public new abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099HistoryByPayer.finYear>
  {
  }

  public new abstract class boxNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AP1099HistoryByPayer.boxNbr>
  {
  }

  public new abstract class histAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AP1099HistoryByPayer.histAmt>
  {
  }
}
