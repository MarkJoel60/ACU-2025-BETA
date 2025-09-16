// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMSubAuditReportChangeOrderLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PM;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <summary>
/// A projection over the <see cref="T:PX.Objects.PO.POLine" /> class joined with the <see cref="T:PX.Objects.PM.PMChangeOrder" /> class
/// and <see cref="T:PX.Objects.PM.PMChangeOrderLine" /> class and grouped by <see cref="T:PX.Objects.PO.POLine" />.
/// The projection is used in Subcontract Audit Report.
/// </summary>
[PXCacheName("PM Subcontract Audit Report Change Order Line")]
[PXProjection(typeof (SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrderLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.orderNbr, Equal<PMChangeOrderLine.pOOrderNbr>>>>>.And<BqlOperand<POLine.lineNbr, IBqlInt>.IsEqual<PMChangeOrderLine.pOLineNbr>>>>, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderLine.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>>.Where<BqlOperand<PMChangeOrderLine.released, IBqlBool>.IsEqual<True>>.AggregateTo<GroupBy<PMChangeOrderLine.pOOrderNbr>, GroupBy<PMChangeOrderLine.pOLineNbr>, Sum<PMChangeOrderLine.qty>, Sum<PMChangeOrderLine.amountInProjectCury>, Sum<PMChangeOrderLine.retainageAmtInProjectCury>>))]
[Serializable]
public class PMSubAuditReportChangeOrderLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PO.POLine.OrderNbr" />
  [PXDBString(IsKey = true, BqlField = typeof (POLine.orderNbr))]
  public virtual 
  #nullable disable
  string OrderNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLine.LineNbr" />
  [PXDBInt(IsKey = true, BqlField = typeof (POLine.lineNbr))]
  public virtual int? LineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Date" />
  [PXDBDate(BqlField = typeof (PMChangeOrder.date))]
  public virtual DateTime? Date { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderLine.Qty" />
  [PXDBDecimal(BqlField = typeof (PMChangeOrderLine.qty))]
  public virtual Decimal? ChangeOrderQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderLine.AmountInProjectCury" />
  [PXDBDecimal(BqlField = typeof (PMChangeOrderLine.amountInProjectCury))]
  public virtual Decimal? ChangeOrderAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderLine.RetainageAmtInProjectCury" />
  [PXDBDecimal(BqlField = typeof (PMChangeOrderLine.retainageAmtInProjectCury))]
  public virtual Decimal? ChangeOrderRetainage { get; set; }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportChangeOrderLine.orderNbr>
  {
  }

  public abstract class lineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMSubAuditReportChangeOrderLine.lineNbr>
  {
  }

  public abstract class date : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMSubAuditReportChangeOrderLine.date>
  {
  }

  public abstract class changeOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMSubAuditReportChangeOrderLine.changeOrderQty>
  {
  }

  public abstract class changeOrderAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMSubAuditReportChangeOrderLine.changeOrderAmount>
  {
  }

  public abstract class changeOrderRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMSubAuditReportChangeOrderLine.changeOrderRetainage>
  {
  }
}
