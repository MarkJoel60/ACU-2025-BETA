// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.DropShipLinksExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.DAC;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class DropShipLinksExt : PXGraphExtension<POReceiptEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<DropShipLink, Where<DropShipLink.pOOrderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<DropShipLink.pOOrderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<DropShipLink.pOLineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>> DropShipLinks;
  [PXCopyPasteHiddenView]
  public PXSelect<SOTax> SOTaxes;
  [PXCopyPasteHiddenView]
  public PXSelect<SOTaxTran> SOTaxTrans;
  protected HashSet<string> prefetched = new HashSet<string>();

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOOrderType> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOOrderNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBIntAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOLineNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOOrderType> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOOrderNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBIntAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOLineNbr> e)
  {
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.PrefetchDropShipLinks(PX.Objects.PO.POOrder)" />
  /// </summary>
  [PXOverride]
  public virtual void PrefetchDropShipLinks(PX.Objects.PO.POOrder order)
  {
    if (order == null || order.OrderType != "DP" || order.IsLegacyDropShip.GetValueOrDefault())
      return;
    PXSelectReadonly2<PX.Objects.PO.POLine, LeftJoin<DropShipLink, On<DropShipLink.FK.POLine>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>> pxSelectReadonly2 = new PXSelectReadonly2<PX.Objects.PO.POLine, LeftJoin<DropShipLink, On<DropShipLink.FK.POLine>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>((PXGraph) this.Base);
    Type[] typeArray = new Type[4]
    {
      typeof (PX.Objects.PO.POLine.orderType),
      typeof (PX.Objects.PO.POLine.orderNbr),
      typeof (PX.Objects.PO.POLine.lineNbr),
      typeof (DropShipLink)
    };
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, typeArray))
    {
      int startRow = PXView.StartRow;
      int num = 0;
      foreach (PXResult<PX.Objects.PO.POLine, DropShipLink> pxResult in ((PXSelectBase) pxSelectReadonly2).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        PX.Objects.PO.POLine poLine = PXResult<PX.Objects.PO.POLine, DropShipLink>.op_Implicit(pxResult);
        PXCommandKey pxCommandKey = new PXCommandKey(new object[3]
        {
          (object) poLine.OrderType,
          (object) poLine.OrderNbr,
          (object) poLine.LineNbr
        });
        this.DropShipLinkStoreCached(PXResult<PX.Objects.PO.POLine, DropShipLink>.op_Implicit(pxResult), PXResult<PX.Objects.PO.POLine, DropShipLink>.op_Implicit(pxResult));
      }
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.ValidatePOOrder(PX.Objects.PO.POOrder)" />
  /// </summary>
  [PXOverride]
  public virtual void ValidatePOOrder(PX.Objects.PO.POOrder order)
  {
    if (order.OrderType != "DP" || order.IsLegacyDropShip.GetValueOrDefault() || order.SOOrderNbr == null)
      return;
    PX.Objects.PO.DemandSOOrder parent = KeysRelation<CompositeKey<Field<PX.Objects.PO.POOrder.sOOrderType>.IsRelatedTo<PX.Objects.PO.DemandSOOrder.orderType>, Field<PX.Objects.PO.POOrder.sOOrderNbr>.IsRelatedTo<PX.Objects.PO.DemandSOOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.DemandSOOrder, PX.Objects.PO.POOrder>, PX.Objects.PO.DemandSOOrder, PX.Objects.PO.POOrder>.FindParent((PXGraph) this.Base, order, (PKFindOptions) 0);
    if (!this.IsDemandOrderReadyForReceipt(parent))
      throw new PXException("The purchase receipt cannot be created because the linked sales order has the {0} status.", new object[1]
      {
        (object) ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)).GetStateExt<PX.Objects.SO.SOOrder.status>((object) new PX.Objects.SO.SOOrder()
        {
          Status = parent.Status
        })?.ToString()
      });
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.ValidatePOLine(PX.Objects.PO.POLine,PX.Objects.PO.POReceipt)" />
  /// </summary>
  [PXOverride]
  public virtual void ValidatePOLine(PX.Objects.PO.POLine poline, PX.Objects.PO.POReceipt receipt)
  {
    PX.Objects.PO.POOrder parent1 = KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.orderType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POLine.orderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POLine>, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>.FindParent((PXGraph) this.Base, poline, (PKFindOptions) 0);
    if (parent1.OrderType != "DP" || parent1.IsLegacyDropShip.GetValueOrDefault() || receipt.ReceiptType != "RT")
      return;
    if (!this.IsDropShipOrderReadyForReceipt(parent1))
    {
      string str = ((PXCache) GraphHelper.Caches<PX.Objects.PO.POOrder>((PXGraph) this.Base)).GetStateExt<PX.Objects.PO.POOrder.status>((object) parent1)?.ToString();
      throw new FailedToAddPOOrderException("The {0} line of the {1} purchase order cannot be added because the purchase order has the {2} status.", new object[3]
      {
        (object) poline.LineNbr,
        (object) parent1.OrderNbr,
        (object) str
      });
    }
    if (!EnumerableExtensions.IsIn<string>(poline.LineType, "GP", "NP"))
      return;
    PX.Objects.PO.DemandSOOrder parent2 = KeysRelation<CompositeKey<Field<PX.Objects.PO.POOrder.sOOrderType>.IsRelatedTo<PX.Objects.PO.DemandSOOrder.orderType>, Field<PX.Objects.PO.POOrder.sOOrderNbr>.IsRelatedTo<PX.Objects.PO.DemandSOOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.DemandSOOrder, PX.Objects.PO.POOrder>, PX.Objects.PO.DemandSOOrder, PX.Objects.PO.POOrder>.FindParent((PXGraph) this.Base, parent1, (PKFindOptions) 0);
    if (!this.IsDemandOrderReadyForReceipt(parent2))
    {
      string str = ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)).GetStateExt<PX.Objects.SO.SOOrder.status>((object) new PX.Objects.SO.SOOrder()
      {
        Status = parent2.Status
      })?.ToString();
      throw new FailedToAddPOOrderException("The {0} purchase order cannot be added because the linked sales order has the {1} status.", new object[2]
      {
        (object) parent1.OrderNbr,
        (object) str
      });
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.InsertReceiptLine(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POReceipt,PX.Objects.PO.POLine)" />
  /// </summary>
  [PXOverride]
  public virtual PX.Objects.PO.POReceiptLine InsertReceiptLine(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.PO.POLine poline,
    Func<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceipt, PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine> baseMethod)
  {
    PX.Objects.PO.POReceiptLine poReceiptLine = baseMethod(line, receipt, poline);
    Decimal? receiptQty = line.ReceiptQty;
    Decimal num = 0M;
    if (receiptQty.GetValueOrDefault() < num & receiptQty.HasValue || receipt.ReceiptType != "RT")
      return poReceiptLine;
    PX.Objects.PO.POOrder parent = KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.orderType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POLine.orderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POLine>, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>.FindParent((PXGraph) this.Base, poline, (PKFindOptions) 0);
    if (parent.OrderType != "DP" || parent.IsLegacyDropShip.GetValueOrDefault())
      return poReceiptLine;
    DropShipLink dropShipLink = this.GetDropShipLink(poline.OrderType, poline.OrderNbr, poline.LineNbr);
    if (dropShipLink == null)
      return poReceiptLine;
    dropShipLink.InReceipt = new bool?(true);
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).Update(dropShipLink);
    return poReceiptLine;
  }

  [PXOverride]
  public virtual PXSelectBase<PX.Objects.PO.POReceiptLine> GetLinesToReleaseQuery(
    Func<PXSelectBase<PX.Objects.PO.POReceiptLine>> baseMethod)
  {
    PXSelectBase<PX.Objects.PO.POReceiptLine> linesToReleaseQuery = baseMethod();
    linesToReleaseQuery.Join<LeftJoin<DropShipLink, On<DropShipLink.pOOrderType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<DropShipLink.pOOrderNbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<DropShipLink.pOLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.pOType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<PX.Objects.SO.SOLineSplit.pONbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<PX.Objects.SO.SOLineSplit.pOLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>, And<PX.Objects.SO.SOLineSplit.pOSource, Equal<INReplenishmentSource.dropShipToOrder>>>>>, LeftJoin<PX.Objects.PO.DemandSOOrder, On<PX.Objects.PO.DemandSOOrder.orderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<PX.Objects.PO.DemandSOOrder.orderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>>>>>>>();
    return linesToReleaseQuery;
  }

  [PXOverride]
  public virtual void ValidateReceiptLineOnRelease(PXResult<PX.Objects.PO.POReceiptLine> row)
  {
    PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(row);
    PX.Objects.PO.POOrder poOrder = ((PXResult) row).GetItem<PX.Objects.PO.POOrder>();
    DropShipLink dropShipLink = ((PXResult) row).GetItem<DropShipLink>();
    PX.Objects.SO.SOLineSplit soLineSplit = ((PXResult) row).GetItem<PX.Objects.SO.SOLineSplit>();
    PX.Objects.PO.DemandSOOrder soOrder = ((PXResult) row).GetItem<PX.Objects.PO.DemandSOOrder>();
    if (poOrder == null || poReceiptLine.ReceiptType != "RT" || EnumerableExtensions.IsNotIn<string>(poReceiptLine.LineType, "GP", "NP"))
      return;
    if (soLineSplit.OrderNbr == null || !poOrder.IsLegacyDropShip.GetValueOrDefault() && !dropShipLink.Active.GetValueOrDefault())
      throw new PXException("There are one or multiple lines in the {0} drop-ship purchase order that are not linked to a sales order. Link all lines in the {0} drop-ship purchase order first.", new object[1]
      {
        (object) poOrder.OrderNbr
      });
    if (!this.IsDemandOrderReadyForReceipt(soOrder))
    {
      string str = ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)).GetStateExt<PX.Objects.SO.SOOrder.status>((object) new PX.Objects.SO.SOOrder()
      {
        Status = soOrder.Status
      })?.ToString();
      throw new PXException("The {0} purchase receipt cannot be released because the linked {1} sales order has the {2} status.", new object[3]
      {
        (object) poReceiptLine.ReceiptNbr,
        (object) soOrder.OrderNbr,
        (object) str
      });
    }
  }

  [PXOverride]
  public PX.Objects.PO.POLine UpdateReceiptLineOnRelease(
    PXResult<PX.Objects.PO.POReceiptLine> row,
    PX.Objects.PO.POLine poLine,
    Func<PXResult<PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POLine, PX.Objects.PO.POLine> base_UpdateReceiptLineOnRelease)
  {
    poLine = base_UpdateReceiptLineOnRelease(row, poLine);
    PX.Objects.PO.POReceiptLine receiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(row);
    if (((PXResult) row).GetItem<PX.Objects.PO.POOrder>() == null || receiptLine.ReceiptType != "RT" || EnumerableExtensions.IsNotIn<string>(receiptLine.LineType, "GP", "NP"))
      return poLine;
    DropShipLink dropShipLink1 = ((PXResult) row).GetItem<DropShipLink>();
    if (dropShipLink1 == null || dropShipLink1.POOrderNbr == null)
      return poLine;
    DropShipLink dropShipLink2 = ((PXSelectBase<DropShipLink>) this.DropShipLinks).Locate(dropShipLink1) ?? dropShipLink1;
    DropShipLink dropShipLink3 = dropShipLink2;
    Decimal? baseReceivedQty = dropShipLink3.BaseReceivedQty;
    Decimal? baseQty = receiptLine.BaseQty;
    dropShipLink3.BaseReceivedQty = baseReceivedQty.HasValue & baseQty.HasValue ? new Decimal?(baseReceivedQty.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
    dropShipLink2.InReceipt = new bool?(this.HasOtherUnreleasedReceipts(receiptLine));
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).Update(dropShipLink2);
    return poLine;
  }

  public virtual bool HasOtherUnreleasedReceipts(PX.Objects.PO.POReceiptLine receiptLine)
  {
    int? rowCount = PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectGroupBy<PX.Objects.PO.POReceiptLine, Where<PX.Objects.PO.POReceiptLine.released, NotEqual<True>, And<PX.Objects.PO.POReceiptLine.pOType, Equal<Required<PX.Objects.PO.POReceiptLine.pOType>>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<Required<PX.Objects.PO.POReceiptLine.pONbr>>, And<PX.Objects.PO.POReceiptLine.pOLineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.pOLineNbr>>, And<Where<PX.Objects.PO.POReceiptLine.receiptType, NotEqual<Required<PX.Objects.PO.POReceiptLine.receiptType>>, Or<PX.Objects.PO.POReceiptLine.receiptNbr, NotEqual<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, Or<PX.Objects.PO.POReceiptLine.lineNbr, NotEqual<Required<PX.Objects.PO.POReceiptLine.lineNbr>>>>>>>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this.Base, new object[6]
    {
      (object) receiptLine.POType,
      (object) receiptLine.PONbr,
      (object) receiptLine.POLineNbr,
      (object) receiptLine.ReceiptType,
      (object) receiptLine.ReceiptNbr,
      (object) receiptLine.LineNbr
    }).RowCount;
    int num = 0;
    return rowCount.GetValueOrDefault() > num & rowCount.HasValue;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.PrefetchWithDetails" />
  /// </summary>
  [PXOverride]
  public virtual void PrefetchWithDetails()
  {
    PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current;
    if (current == null || current.ReceiptType != "RT")
      return;
    int? lineCntr = current.LineCntr;
    int num1 = 0;
    if (lineCntr.GetValueOrDefault() == num1 & lineCntr.HasValue || this.prefetched.Contains(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.ReceiptType + ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.ReceiptNbr) || PXView.MaximumRows == 1)
      return;
    PXSelectReadonly2<PX.Objects.PO.POReceiptLine, LeftJoin<DropShipLink, On<DropShipLink.pOOrderType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<DropShipLink.pOOrderNbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<DropShipLink.pOLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Current<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Current<PX.Objects.PO.POReceipt.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.pOLineNbr, IsNotNull>>>> pxSelectReadonly2 = new PXSelectReadonly2<PX.Objects.PO.POReceiptLine, LeftJoin<DropShipLink, On<DropShipLink.pOOrderType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<DropShipLink.pOOrderNbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<DropShipLink.pOLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Current<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Current<PX.Objects.PO.POReceipt.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.pOLineNbr, IsNotNull>>>>((PXGraph) this.Base);
    Type[] typeArray = new Type[7]
    {
      typeof (PX.Objects.PO.POReceiptLine.receiptType),
      typeof (PX.Objects.PO.POReceiptLine.receiptNbr),
      typeof (PX.Objects.PO.POReceiptLine.lineNbr),
      typeof (PX.Objects.PO.POReceiptLine.pOType),
      typeof (PX.Objects.PO.POReceiptLine.pONbr),
      typeof (PX.Objects.PO.POReceiptLine.pOLineNbr),
      typeof (DropShipLink)
    };
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, typeArray))
    {
      int startRow = PXView.StartRow;
      int num2 = 0;
      foreach (PXResult<PX.Objects.PO.POReceiptLine, DropShipLink> pxResult in ((PXSelectBase) pxSelectReadonly2).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num2))
        this.DropShipLinkStoreCached(PXResult<PX.Objects.PO.POReceiptLine, DropShipLink>.op_Implicit(pxResult), PXResult<PX.Objects.PO.POReceiptLine, DropShipLink>.op_Implicit(pxResult));
    }
    this.prefetched.Add(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.ReceiptType + ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current.ReceiptNbr);
  }

  public virtual DropShipLink GetDropShipLink(string orderType, string orderNbr, int? lineNbr)
  {
    if (orderType == null || orderNbr == null || !lineNbr.HasValue)
      return (DropShipLink) null;
    return PXResultset<DropShipLink>.op_Implicit(((PXSelectBase<DropShipLink>) this.DropShipLinks).SelectWindowed(0, 1, new object[3]
    {
      (object) orderType,
      (object) orderNbr,
      (object) lineNbr
    }));
  }

  public virtual void DropShipLinkStoreCached(DropShipLink link, PX.Objects.PO.POReceiptLine line)
  {
    List<object> objectList = new List<object>(1);
    if (link != null && link.POOrderType != null)
      objectList.Add((object) link);
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).StoreResult(objectList, PXQueryParameters.ExplicitParameters(new object[3]
    {
      (object) line.POType,
      (object) line.PONbr,
      (object) line.POLineNbr
    }));
  }

  public virtual void DropShipLinkStoreCached(DropShipLink link, PX.Objects.PO.POLine line)
  {
    List<object> objectList = new List<object>(1);
    if (link != null && link.POOrderType != null)
      objectList.Add((object) link);
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).StoreResult(objectList, PXQueryParameters.ExplicitParameters(new object[3]
    {
      (object) line.OrderType,
      (object) line.OrderNbr,
      (object) line.LineNbr
    }));
  }

  public virtual bool IsDemandOrderReadyForReceipt(PX.Objects.PO.DemandSOOrder soOrder)
  {
    return !soOrder.Hold.GetValueOrDefault() && soOrder.Approved.GetValueOrDefault() && soOrder.PrepaymentReqSatisfied.GetValueOrDefault();
  }

  public virtual bool IsDropShipOrderReadyForReceipt(PX.Objects.PO.POOrder order)
  {
    if (!order.Hold.GetValueOrDefault() && order.Approved.GetValueOrDefault() && order.PrintedExt.GetValueOrDefault() && order.EmailedExt.GetValueOrDefault() && !order.Cancelled.GetValueOrDefault())
    {
      int? nullable = order.LinesToCloseCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
      {
        nullable = order.LinesToCompleteCntr;
        int num2 = 0;
        if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
        {
          nullable = order.DropShipOpenLinesCntr;
          int num3 = 0;
          if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
            return true;
          nullable = order.DropShipNotLinkedLinesCntr;
          int num4 = 0;
          return nullable.GetValueOrDefault() == num4 & nullable.HasValue;
        }
      }
    }
    return false;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.PO.POReceiptLine> e)
  {
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row.POType, e.Row.PONbr, e.Row.POLineNbr);
    if (dropShipLink == null)
      return;
    dropShipLink.InReceipt = new bool?(this.HasOtherUnreleasedReceipts(e.Row));
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).Update(dropShipLink);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POLine> e)
  {
    bool? completed1 = e.Row.Completed;
    bool? completed2 = e.OldRow.Completed;
    if (completed1.GetValueOrDefault() == completed2.GetValueOrDefault() & completed1.HasValue == completed2.HasValue)
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row.OrderType, e.Row.OrderNbr, e.Row.LineNbr);
    if (dropShipLink == null)
      return;
    dropShipLink.POCompleted = e.Row.Completed;
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).Update(dropShipLink);
  }

  private bool IsGoodsForDropShipOrProject(PX.Objects.PO.POReceiptLine poReceiptLine)
  {
    return poReceiptLine != null && EnumerableExtensions.IsIn<string>(poReceiptLine.LineType, "GP", "PG");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.inventoryID> e)
  {
    if (!this.IsGoodsForDropShipOrProject(e.Row))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.inventoryID>>) e).Cache.SetDefaultExt<PX.Objects.PO.POReceiptLine.lotSerialNbrRequiredForDropship>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.lotSerialNbrRequiredForDropship> e)
  {
    if (!this.IsGoodsForDropShipOrProject(e.Row))
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
    if (inventoryItem == null)
      return;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, inventoryItem.LotSerClassID);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.lotSerialNbrRequiredForDropship>, PX.Objects.PO.POReceiptLine, object>) e).NewValue = (object) (bool) (inLotSerClass != null ? (inLotSerClass.RequiredForDropship.GetValueOrDefault() ? 1 : 0) : 0);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.lotSerialNbrRequiredForDropship>>) e).Cancel = true;
  }
}
