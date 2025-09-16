// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBiddingRequisitionLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXHidden]
[Serializable]
public class RQBiddingRequisitionLine : RQRequisitionLine
{
  protected Decimal? _QuoteCost;

  [PXDBCalced(typeof (Mult<RQRequisitionLine.orderQty, RQBidding.quoteUnitCost>), typeof (Decimal))]
  public virtual Decimal? QuoteCost
  {
    get => this._QuoteCost;
    set => this._QuoteCost = value;
  }

  public abstract class quoteCost : 
    BqlType<IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBiddingRequisitionLine.quoteCost>
  {
  }
}
