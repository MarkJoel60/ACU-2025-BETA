// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.Prepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceEntryExt;

public class Prepayments : PXGraphExtension<APInvoiceEntry.MultiCurrency, APInvoiceEntry>
{
  public PXSelect<PX.Objects.PO.POLine> POLines;
  public PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<POOrderPrepayment.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Optional<POOrderPrepayment.orderNbr>>>>> POOrders;
  public PXSelect<POOrderPrepayment, Where<POOrderPrepayment.aPDocType, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<POOrderPrepayment.aPRefNbr, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>, And<Current<PX.Objects.AP.APInvoice.docType>, Equal<APDocType.prepayment>>>>> PrepaidOrders;

  [PXMergeAttributes]
  [PXParent(typeof (Select<POOrderPrepayment, Where<POOrderPrepayment.orderType, Equal<Current<PX.Objects.AP.APTran.pOOrderType>>, And<POOrderPrepayment.orderNbr, Equal<Current<PX.Objects.AP.APTran.pONbr>>, And<POOrderPrepayment.aPDocType, Equal<Current<PX.Objects.AP.APTran.tranType>>, And<POOrderPrepayment.aPRefNbr, Equal<Current<PX.Objects.AP.APTran.refNbr>>, And<Current<PX.Objects.AP.APTran.tranType>, Equal<APDocType.prepayment>>>>>>>), ParentCreate = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APTran.pONbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (ManualDiscountMode))]
  [PrepaymentDiscount(typeof (PX.Objects.AP.APTran.curyDiscAmt), typeof (PX.Objects.AP.APTran.curyTranAmt), typeof (PX.Objects.AP.APTran.discPct), typeof (PX.Objects.AP.APTran.freezeManualDisc), DiscountFeatureType.VendorDiscount)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APTran.manualDisc> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (POOrderPrepayment.FK.Order))]
  protected virtual void _(PX.Data.Events.CacheAttached<POOrderPrepayment.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [CurrencyInfo(typeof (PX.Objects.PO.POOrder.curyInfoID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POOrderPrepayment.curyInfoID> e)
  {
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AP.APInvoiceEntry.MultiCurrency.GetChildren" />.
  /// </summary>
  [PXOverride]
  public virtual PXSelectBase[] GetChildren(Func<PXSelectBase[]> baseMethod)
  {
    return ((IEnumerable<PXSelectBase>) baseMethod()).Union<PXSelectBase>((IEnumerable<PXSelectBase>) new PXSelectBase[1]
    {
      (PXSelectBase) this.PrepaidOrders
    }).ToArray<PXSelectBase>();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.GetTrackedExceptChildren" />.
  /// </summary>
  [PXOverride]
  public virtual PXSelectBase[] GetTrackedExceptChildren(Func<PXSelectBase[]> baseMethod)
  {
    return ((IEnumerable<PXSelectBase>) baseMethod()).Union<PXSelectBase>((IEnumerable<PXSelectBase>) new PXSelectBase[1]
    {
      (PXSelectBase) this.POLines
    }).ToArray<PXSelectBase>();
  }

  protected virtual void _(PX.Data.Events.RowInserted<POOrderPrepayment> e)
  {
    if (!(((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current?.OrigModule == "AP"))
      return;
    ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.OrigModule = "PO";
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current, true);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<POOrderPrepayment> e)
  {
    if (!(((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current?.OrigModule == "PO") || ((IEnumerable<PXResult<POOrderPrepayment>>) ((PXSelectBase<POOrderPrepayment>) this.PrepaidOrders).Select(Array.Empty<object>())).AsEnumerable<PXResult<POOrderPrepayment>>().Any<PXResult<POOrderPrepayment>>())
      return;
    ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.OrigModule = "AP";
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct> e)
  {
    if (e.Row.TranType != "PPM" && e.Row.TranType != "PPI")
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct>, PX.Objects.AP.APTran, object>) e).NewValue = (object) 0M;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct>>) e).Cancel = true;
    }
    else
    {
      if (!string.IsNullOrEmpty(e.Row.POOrderType) && !string.IsNullOrEmpty(e.Row.PONbr))
      {
        PX.Objects.PO.POOrder poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXSelectReadonly<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
        {
          (object) e.Row.POOrderType,
          (object) e.Row.PONbr
        }));
        if (poOrder != null && poOrder.PrepaymentPct.HasValue)
        {
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct>, PX.Objects.AP.APTran, object>) e).NewValue = (object) poOrder.PrepaymentPct;
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct>>) e).Cancel = true;
          return;
        }
      }
      if (e.Row.InventoryID.HasValue)
      {
        POVendorInventory poVendorInventory = PXResultset<POVendorInventory>.op_Implicit(PXSelectBase<POVendorInventory, PXSelectReadonly<POVendorInventory, Where<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<POVendorInventory.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[1]
        {
          (object) e.Row.InventoryID
        }));
        if (poVendorInventory != null && poVendorInventory.PrepaymentPct.HasValue)
        {
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct>, PX.Objects.AP.APTran, object>) e).NewValue = (object) poVendorInventory.PrepaymentPct;
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct>>) e).Cancel = true;
          return;
        }
      }
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.prepaymentPct>, PX.Objects.AP.APTran, object>) e).NewValue = (object) ((Decimal?) ((PXSelectBase<PX.Objects.CR.Location>) ((PXGraphExtension<APInvoiceEntry>) this).Base.location).Current?.VPrepaymentPct).GetValueOrDefault();
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, PX.Objects.AP.APTran.inventoryID> e)
  {
    if (!(e.Row.TranType == "PPM"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, PX.Objects.AP.APTran.inventoryID>>) e).Cache.SetDefaultExt<PX.Objects.AP.APTran.prepaymentPct>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pONbr> e)
  {
    if (!(e.Row.TranType == "PPM"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pONbr>>) e).Cache.SetDefaultExt<PX.Objects.AP.APTran.prepaymentPct>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AP.APTran> e)
  {
    if (e.Row.TranType != "PPM" || string.IsNullOrEmpty(e.Row.POOrderType) || string.IsNullOrEmpty(e.Row.PONbr))
      return;
    POOrderPrepayment poOrderPrepayment = PXParentAttribute.SelectParent<POOrderPrepayment>(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) e.Row);
    if (poOrderPrepayment == null || ((IEnumerable<object>) PXParentAttribute.SelectChildren(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) poOrderPrepayment, typeof (POOrderPrepayment))).Any<object>())
      return;
    ((PXSelectBase<POOrderPrepayment>) this.PrepaidOrders).Delete(poOrderPrepayment);
  }

  public virtual void AddPOOrderProc(PX.Objects.PO.POOrder order, bool createNew)
  {
    if (createNew)
    {
      PXSelectJoin<PX.Objects.AP.APInvoice, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.AP.APInvoice.vendorID>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<Optional<PX.Objects.AP.APInvoice.docType>>, And2<Where<PX.Objects.AP.APRegister.origModule, NotEqual<BatchModule.moduleTX>, Or<PX.Objects.AP.APInvoice.released, Equal<True>>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>>> document = ((PXGraphExtension<APInvoiceEntry>) this).Base.Document;
      PX.Objects.AP.APInvoice apInvoice1 = new PX.Objects.AP.APInvoice();
      apInvoice1.DocType = "PPM";
      PX.Objects.AP.APInvoice apInvoice2 = ((PXSelectBase<PX.Objects.AP.APInvoice>) document).Insert(apInvoice1);
      apInvoice2.BranchID = order.BranchID;
      apInvoice2.DocDesc = order.OrderDesc;
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
      {
        apInvoice2.VendorID = order.PayToVendorID;
        PX.Objects.AP.APInvoice apInvoice3 = apInvoice2;
        int? vendorId = order.VendorID;
        int? nullable1 = order.PayToVendorID;
        int? nullable2;
        if (!(vendorId.GetValueOrDefault() == nullable1.GetValueOrDefault() & vendorId.HasValue == nullable1.HasValue))
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = order.VendorLocationID;
        apInvoice3.VendorLocationID = nullable2;
        apInvoice2.SuppliedByVendorID = order.VendorID;
        apInvoice2.SuppliedByVendorLocationID = order.VendorLocationID;
      }
      else
      {
        apInvoice2.VendorID = apInvoice2.SuppliedByVendorID = order.VendorID;
        apInvoice2.VendorLocationID = apInvoice2.SuppliedByVendorLocationID = order.VendorLocationID;
      }
      apInvoice2.CuryID = order.CuryID;
      ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Update(apInvoice2);
      apInvoice2.TaxCalcMode = order.TaxCalcMode;
      apInvoice2.InvoiceNbr = order.OrderNbr;
      apInvoice2.DueDate = order.OrderDate;
      apInvoice2.TaxZoneID = order.TaxZoneID;
      apInvoice2.EntityUsageType = order.EntityUsageType;
      apInvoice2.ExternalTaxExemptionNumber = order.ExternalTaxExemptionNumber;
      ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Update(apInvoice2);
    }
    else
    {
      PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    }
    TaxBaseAttribute.SetTaxCalc<PX.Objects.AP.APTran.taxCategoryID, TaxAttribute>(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    if (!this.AddPOOrderLines((IEnumerable<POLineRS>) GraphHelper.RowCast<POLineRS>((IEnumerable) PXSelectBase<POLineRS, PXSelectReadonly<POLineRS, Where<POLineRS.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<POLineRS.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>, OrderBy<Asc<POLineRS.sortOrder, Asc<POLineRS.lineNbr>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    })).ToList<POLineRS>()))
      throw new PXException("There are no lines in this document that may be entered in AP Bill Document directly");
    ((PXGraphExtension<APInvoiceEntry>) this).Base.AddOrderTaxes(order);
    TaxBaseAttribute.SetTaxCalc<PX.Objects.AP.APTran.taxCategoryID, TaxAttribute>(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
  }

  public virtual bool AddPOOrderLines(IEnumerable<POLineRS> lines)
  {
    bool flag1 = false;
    foreach (POLineRS line in lines.Where<POLineRS>((Func<POLineRS, bool>) (l =>
    {
      Decimal? nullable19;
      Decimal? nullable20;
      Decimal? nullable21;
      if (!this.IsNegativeNonStockPrice(l))
      {
        nullable19 = l.CuryExtCost;
        Decimal? curyRetainageAmt = l.CuryRetainageAmt;
        nullable20 = nullable19.HasValue & curyRetainageAmt.HasValue ? new Decimal?(nullable19.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
        nullable21 = l.CuryReqPrepaidAmt;
        if (nullable20.GetValueOrDefault() > nullable21.GetValueOrDefault() & nullable20.HasValue & nullable21.HasValue)
          goto label_4;
      }
      if (this.IsNegativeNonStockPrice(l))
      {
        Decimal? curyExtCost = l.CuryExtCost;
        nullable19 = l.CuryRetainageAmt;
        nullable21 = curyExtCost.HasValue & nullable19.HasValue ? new Decimal?(curyExtCost.GetValueOrDefault() + nullable19.GetValueOrDefault()) : new Decimal?();
        nullable20 = l.CuryReqPrepaidAmt;
        if (!(nullable21.GetValueOrDefault() < nullable20.GetValueOrDefault() & nullable21.HasValue & nullable20.HasValue))
          goto label_9;
      }
      else
        goto label_9;
label_4:
      bool? nullable22 = l.Cancelled;
      bool flag2 = false;
      if (nullable22.GetValueOrDefault() == flag2 & nullable22.HasValue)
      {
        nullable22 = l.Closed;
        bool flag3 = false;
        if (nullable22.GetValueOrDefault() == flag3 & nullable22.HasValue)
        {
          nullable22 = l.Billed;
          bool flag4 = false;
          return nullable22.GetValueOrDefault() == flag4 & nullable22.HasValue || l.LineType == "SV";
        }
      }
label_9:
      return false;
    })))
    {
      PX.Objects.AP.APTran apTran1 = new PX.Objects.AP.APTran();
      apTran1.IsStockItem = line.IsStockItem;
      apTran1.InventoryID = line.InventoryID;
      apTran1.ProjectID = line.ProjectID;
      apTran1.TaskID = line.TaskID;
      apTran1.CostCodeID = line.CostCodeID;
      apTran1.TaxID = line.TaxID;
      apTran1.TaxCategoryID = line.TaxCategoryID;
      apTran1.TranDesc = line.TranDesc;
      apTran1.UOM = line.UOM;
      apTran1.CuryUnitCost = line.CuryUnitCost;
      apTran1.DiscPct = line.DiscPct;
      apTran1.ManualPrice = new bool?(true);
      apTran1.ManualDisc = new bool?(true);
      apTran1.FreezeManualDisc = new bool?(true);
      apTran1.DiscountID = line.DiscountID;
      apTran1.DiscountSequenceID = line.DiscountSequenceID;
      apTran1.POOrderType = line.OrderType;
      apTran1.PONbr = line.OrderNbr;
      apTran1.POLineNbr = line.LineNbr;
      Decimal? nullable1 = new Decimal?();
      apTran1.RetainagePct = nullable1;
      nullable1 = new Decimal?();
      apTran1.CuryRetainageAmt = nullable1;
      PX.Objects.AP.APTran apTran2 = apTran1;
      nullable1 = line.ReqPrepaidQty;
      Decimal? nullable2 = line.OrderBilledQty;
      Decimal? nullable3;
      Decimal? nullable4;
      if (!(nullable1.HasValue & nullable2.HasValue))
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
      Decimal? nullable5 = nullable4;
      PX.Objects.AP.APTran apTran3 = apTran2;
      nullable2 = line.OrderQty;
      nullable1 = nullable5;
      Decimal? nullable6;
      if (!(nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue))
      {
        nullable1 = line.OrderQty;
        nullable2 = nullable5;
        if (!(nullable1.HasValue & nullable2.HasValue))
        {
          nullable3 = new Decimal?();
          nullable6 = nullable3;
        }
        else
          nullable6 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
      }
      else
        nullable6 = line.OrderQty;
      apTran3.Qty = nullable6;
      nullable2 = line.CuryReqPrepaidAmt;
      nullable1 = line.CuryOrderBilledAmt;
      Decimal? nullable7;
      if (!(nullable2.HasValue & nullable1.HasValue))
      {
        nullable3 = new Decimal?();
        nullable7 = nullable3;
      }
      else
        nullable7 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
      Decimal? nullable8 = nullable7;
      nullable1 = nullable8;
      Decimal num = 0M;
      if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      {
        apTran2.CuryLineAmt = line.CuryLineAmt;
        apTran2.CuryDiscAmt = line.CuryDiscAmt;
      }
      else
      {
        Decimal? nullable9;
        if (!this.IsNegativeNonStockPrice(line))
        {
          nullable3 = line.CuryExtCost;
          nullable9 = line.CuryRetainageAmt;
          nullable1 = nullable3.HasValue & nullable9.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
          nullable2 = nullable8;
          if (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
            goto label_19;
        }
        if (this.IsNegativeNonStockPrice(line))
        {
          nullable9 = line.CuryExtCost;
          nullable3 = line.CuryRetainageAmt;
          nullable2 = nullable9.HasValue & nullable3.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          nullable1 = nullable8;
          if (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
            goto label_19;
        }
        PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
        Decimal? curyExtCost = line.CuryExtCost;
        Decimal? nullable10 = line.CuryRetainageAmt;
        nullable3 = curyExtCost.HasValue & nullable10.HasValue ? new Decimal?(curyExtCost.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new Decimal?();
        nullable9 = nullable8;
        Decimal? nullable11;
        if (!(nullable3.HasValue & nullable9.HasValue))
        {
          nullable10 = new Decimal?();
          nullable11 = nullable10;
        }
        else
          nullable11 = new Decimal?(nullable3.GetValueOrDefault() - nullable9.GetValueOrDefault());
        nullable1 = nullable11;
        nullable9 = line.CuryExtCost;
        nullable3 = line.CuryRetainageAmt;
        Decimal? nullable12;
        if (!(nullable9.HasValue & nullable3.HasValue))
        {
          nullable10 = new Decimal?();
          nullable12 = nullable10;
        }
        else
          nullable12 = new Decimal?(nullable9.GetValueOrDefault() + nullable3.GetValueOrDefault());
        nullable2 = nullable12;
        Decimal? nullable13;
        if (!(nullable1.HasValue & nullable2.HasValue))
        {
          nullable3 = new Decimal?();
          nullable13 = nullable3;
        }
        else
          nullable13 = new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault());
        Decimal? nullable14 = nullable13;
        PX.Objects.AP.APTran apTran4 = apTran2;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = defaultCurrencyInfo;
        nullable2 = nullable14;
        nullable1 = line.CuryLineAmt;
        Decimal? nullable15;
        if (!(nullable2.HasValue & nullable1.HasValue))
        {
          nullable3 = new Decimal?();
          nullable15 = nullable3;
        }
        else
          nullable15 = new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault());
        nullable3 = nullable15;
        Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
        Decimal? nullable16 = new Decimal?(currencyInfo1.RoundCury(valueOrDefault1));
        apTran4.CuryLineAmt = nullable16;
        PX.Objects.AP.APTran apTran5 = apTran2;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = defaultCurrencyInfo;
        nullable2 = nullable14;
        nullable1 = line.CuryDiscAmt;
        Decimal? nullable17;
        if (!(nullable2.HasValue & nullable1.HasValue))
        {
          nullable3 = new Decimal?();
          nullable17 = nullable3;
        }
        else
          nullable17 = new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault());
        nullable3 = nullable17;
        Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
        Decimal? nullable18 = new Decimal?(currencyInfo2.RoundCury(valueOrDefault2));
        apTran5.CuryDiscAmt = nullable18;
        goto label_36;
label_19:
        apTran2.CuryLineAmt = new Decimal?(0M);
        apTran2.CuryDiscAmt = new Decimal?(0M);
        apTran2.CuryRetainageAmt = new Decimal?(0M);
        apTran2.CuryTranAmt = new Decimal?(0M);
      }
label_36:
      if (apTran2.InventoryID.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, apTran2.InventoryID);
        if (inventoryItem != null)
        {
          DRDeferredCode deferralCode = DRDeferredCode.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, inventoryItem.DeferredCode);
          if (deferralCode != null)
            apTran2.RequiresTerms = new bool?(((PXGraphExtension<APInvoiceEntry>) this).Base.DoesRequireTerms(deferralCode));
        }
      }
      PX.Objects.AP.APTran apTran6 = ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Insert(apTran2);
      if (PXParentAttribute.SelectParent<POOrderPrepayment>(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) apTran6) == null)
        PXParentAttribute.CreateParent(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) apTran6, typeof (POOrderPrepayment));
      flag1 = true;
    }
    if (!flag1 && lines.All<POLineRS>((Func<POLineRS, bool>) (l =>
    {
      Decimal? rcptQtyThreshold = l.RcptQtyThreshold;
      Decimal num = 0M;
      return rcptQtyThreshold.GetValueOrDefault() == num & rcptQtyThreshold.HasValue;
    })))
      throw new PXException("The prepayment cannot be created because the value in the Complete On (%) column is equal to zero for all the lines of the purchase order on the Purchase Orders (PO301000) form. The prepayment amount cannot be calculated for such lines.");
    ((PXGraphExtension<APInvoiceEntry>) this).Base.AutoRecalculateDiscounts();
    return flag1;
  }

  private bool IsNegativeNonStockPrice(POLineRS line)
  {
    if (!POLineType.IsNonStock(line.LineType))
      return false;
    Decimal? curyExtCost = line.CuryExtCost;
    Decimal? curyRetainageAmt = line.CuryRetainageAmt;
    Decimal? nullable = curyExtCost.HasValue & curyRetainageAmt.HasValue ? new Decimal?(curyExtCost.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
    Decimal num = 0M;
    return nullable.GetValueOrDefault() < num & nullable.HasValue;
  }

  [PXOverride]
  public virtual void VoidPrepayment(PX.Objects.AP.APRegister doc, Action<PX.Objects.AP.APRegister> baseMethod)
  {
    foreach (PXResult<PX.Objects.AP.APTran, PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.TransactionsPOLine).Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POLine poLine1 = PXResult<PX.Objects.AP.APTran, PX.Objects.PO.POLine>.op_Implicit(pxResult);
      if (poLine1 != null && poLine1.OrderNbr != null)
      {
        PX.Objects.PO.POLine copy = PXCache<PX.Objects.PO.POLine>.CreateCopy(poLine1);
        PX.Objects.AP.APTran apTran = PXResult<PX.Objects.AP.APTran, PX.Objects.PO.POLine>.op_Implicit(pxResult);
        PX.Objects.PO.POLine poLine2 = copy;
        Decimal? nullable1 = poLine2.ReqPrepaidQty;
        Decimal? nullable2 = apTran.Qty;
        Decimal? nullable3;
        Decimal? nullable4;
        if (!(nullable1.HasValue & nullable2.HasValue))
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
        poLine2.ReqPrepaidQty = nullable4;
        PX.Objects.PO.POLine poLine3 = copy;
        nullable2 = poLine3.CuryReqPrepaidAmt;
        nullable3 = apTran.CuryTranAmt;
        Decimal? nullable5 = apTran.CuryRetainageAmt;
        nullable1 = nullable3.HasValue & nullable5.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable6;
        if (!(nullable2.HasValue & nullable1.HasValue))
        {
          nullable5 = new Decimal?();
          nullable6 = nullable5;
        }
        else
          nullable6 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
        poLine3.CuryReqPrepaidAmt = nullable6;
        ((PXSelectBase<PX.Objects.PO.POLine>) this.POLines).Update(copy);
      }
    }
    POOrderPrepayment poOrderPrepayment1 = PXResultset<POOrderPrepayment>.op_Implicit(((PXSelectBase<POOrderPrepayment>) this.PrepaidOrders).Select(Array.Empty<object>()));
    if (poOrderPrepayment1 != null)
    {
      POOrderPrepayment copy1 = PXCache<POOrderPrepayment>.CreateCopy(poOrderPrepayment1);
      POOrderPrepayment poOrderPrepayment2 = copy1;
      Decimal? nullable7 = poOrderPrepayment2.CuryAppliedAmt;
      Decimal? nullable8 = doc.CuryOrigDocAmt;
      poOrderPrepayment2.CuryAppliedAmt = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<POOrderPrepayment>) this.PrepaidOrders).Update(copy1);
      PX.Objects.PO.POOrder copy2 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.POOrders).Select(new object[2]
      {
        (object) poOrderPrepayment1.OrderType,
        (object) poOrderPrepayment1.OrderNbr
      })));
      PX.Objects.PO.POOrder poOrder = copy2;
      nullable8 = poOrder.CuryPrepaidTotal;
      nullable7 = doc.CuryDocBal;
      poOrder.CuryPrepaidTotal = nullable8.HasValue & nullable7.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.POOrders).Update(copy2);
    }
    baseMethod(doc);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AP.APTran> e)
  {
    PX.Objects.AP.APTran row = e.Row;
    if (row == null || EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || row.Released.GetValueOrDefault() || !(row.TranType == "PPM") || string.IsNullOrEmpty(row.PONbr) || !row.POLineNbr.HasValue)
      return;
    PX.Objects.PO.POLine poLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXSelectReadonly<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, 0, 1, new object[3]
    {
      (object) row.POOrderType,
      (object) row.PONbr,
      (object) row.POLineNbr
    }));
    Decimal? nullable1;
    Decimal? nullable2;
    int num1;
    if (POLineType.IsNonStock(poLine.LineType))
    {
      Decimal? curyExtCost = poLine.CuryExtCost;
      nullable1 = poLine.CuryRetainageAmt;
      nullable2 = curyExtCost.HasValue & nullable1.HasValue ? new Decimal?(curyExtCost.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      num1 = nullable2.GetValueOrDefault() < num2 & nullable2.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    nullable2 = row.Qty;
    nullable1 = poLine.OrderQty;
    if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.qty>((object) row, (object) row.Qty, (Exception) new PXSetPropertyException("The prepaid quantity cannot exceed the quantity in the corresponding line of the purchase order."));
    if (num1 == 0)
    {
      Decimal? nullable3 = poLine.CuryReqPrepaidAmt;
      Decimal? curyBilledAmt = poLine.CuryBilledAmt;
      Decimal? nullable4 = nullable3.GetValueOrDefault() > curyBilledAmt.GetValueOrDefault() & nullable3.HasValue & curyBilledAmt.HasValue ? poLine.CuryReqPrepaidAmt : poLine.CuryBilledAmt;
      Decimal? nullable5 = row.CuryTranAmt;
      Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable7 = row.CuryRetainageAmt;
      Decimal? nullable8;
      if (!(nullable6.HasValue & nullable7.HasValue))
      {
        nullable5 = new Decimal?();
        nullable8 = nullable5;
      }
      else
        nullable8 = new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault());
      nullable1 = nullable8;
      nullable7 = poLine.CuryExtCost;
      nullable6 = poLine.CuryRetainageAmt;
      Decimal? nullable9;
      if (!(nullable7.HasValue & nullable6.HasValue))
      {
        nullable5 = new Decimal?();
        nullable9 = nullable5;
      }
      else
        nullable9 = new Decimal?(nullable7.GetValueOrDefault() + nullable6.GetValueOrDefault());
      nullable2 = nullable9;
      if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      {
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.curyTranAmt>((object) row, (object) row.CuryTranAmt, (Exception) new PXSetPropertyException("The prepaid amount cannot exceed the amount in the corresponding line of the purchase order."));
      }
      else
      {
        Decimal? curyReqPrepaidAmt = poLine.CuryReqPrepaidAmt;
        nullable3 = poLine.CuryBilledAmt;
        nullable5 = curyReqPrepaidAmt.HasValue & nullable3.HasValue ? new Decimal?(curyReqPrepaidAmt.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        nullable4 = row.CuryTranAmt;
        Decimal? nullable10;
        if (!(nullable5.HasValue & nullable4.HasValue))
        {
          nullable3 = new Decimal?();
          nullable10 = nullable3;
        }
        else
          nullable10 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
        nullable6 = nullable10;
        nullable7 = row.CuryRetainageAmt;
        Decimal? nullable11;
        if (!(nullable6.HasValue & nullable7.HasValue))
        {
          nullable4 = new Decimal?();
          nullable11 = nullable4;
        }
        else
          nullable11 = new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault());
        nullable2 = nullable11;
        nullable7 = poLine.CuryExtCost;
        nullable6 = poLine.CuryRetainageAmt;
        Decimal? nullable12;
        if (!(nullable7.HasValue & nullable6.HasValue))
        {
          nullable4 = new Decimal?();
          nullable12 = nullable4;
        }
        else
          nullable12 = new Decimal?(nullable7.GetValueOrDefault() + nullable6.GetValueOrDefault());
        nullable1 = nullable12;
        if (!(nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue))
          return;
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.curyTranAmt>((object) row, (object) row.CuryTranAmt, (Exception) new PXSetPropertyException("The {0} purchase order already has a partial prepayment. Make sure that the amount of the current prepayment does not exceed the purchase order amount.", (PXErrorLevel) 2, new object[1]
        {
          (object) poLine.OrderNbr
        }));
      }
    }
    else
    {
      Decimal? nullable13 = poLine.CuryReqPrepaidAmt;
      Decimal? curyBilledAmt = poLine.CuryBilledAmt;
      Decimal? nullable14 = nullable13.GetValueOrDefault() > curyBilledAmt.GetValueOrDefault() & nullable13.HasValue & curyBilledAmt.HasValue ? poLine.CuryBilledAmt : poLine.CuryReqPrepaidAmt;
      Decimal? nullable15 = row.CuryTranAmt;
      Decimal? nullable16 = nullable14.HasValue & nullable15.HasValue ? new Decimal?(nullable14.GetValueOrDefault() + nullable15.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable17 = row.CuryRetainageAmt;
      Decimal? nullable18;
      if (!(nullable16.HasValue & nullable17.HasValue))
      {
        nullable15 = new Decimal?();
        nullable18 = nullable15;
      }
      else
        nullable18 = new Decimal?(nullable16.GetValueOrDefault() + nullable17.GetValueOrDefault());
      nullable1 = nullable18;
      nullable17 = poLine.CuryExtCost;
      nullable16 = poLine.CuryRetainageAmt;
      Decimal? nullable19;
      if (!(nullable17.HasValue & nullable16.HasValue))
      {
        nullable15 = new Decimal?();
        nullable19 = nullable15;
      }
      else
        nullable19 = new Decimal?(nullable17.GetValueOrDefault() + nullable16.GetValueOrDefault());
      nullable2 = nullable19;
      if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      {
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.curyTranAmt>((object) row, (object) row.CuryTranAmt, (Exception) new PXSetPropertyException("The prepaid amount cannot be less than the negative amount in the corresponding line of the purchase order."));
      }
      else
      {
        Decimal? curyReqPrepaidAmt = poLine.CuryReqPrepaidAmt;
        nullable13 = poLine.CuryBilledAmt;
        nullable15 = curyReqPrepaidAmt.HasValue & nullable13.HasValue ? new Decimal?(curyReqPrepaidAmt.GetValueOrDefault() + nullable13.GetValueOrDefault()) : new Decimal?();
        nullable14 = row.CuryTranAmt;
        Decimal? nullable20;
        if (!(nullable15.HasValue & nullable14.HasValue))
        {
          nullable13 = new Decimal?();
          nullable20 = nullable13;
        }
        else
          nullable20 = new Decimal?(nullable15.GetValueOrDefault() + nullable14.GetValueOrDefault());
        nullable16 = nullable20;
        nullable17 = row.CuryRetainageAmt;
        Decimal? nullable21;
        if (!(nullable16.HasValue & nullable17.HasValue))
        {
          nullable14 = new Decimal?();
          nullable21 = nullable14;
        }
        else
          nullable21 = new Decimal?(nullable16.GetValueOrDefault() + nullable17.GetValueOrDefault());
        nullable2 = nullable21;
        nullable17 = poLine.CuryExtCost;
        nullable16 = poLine.CuryRetainageAmt;
        Decimal? nullable22;
        if (!(nullable17.HasValue & nullable16.HasValue))
        {
          nullable14 = new Decimal?();
          nullable22 = nullable14;
        }
        else
          nullable22 = new Decimal?(nullable17.GetValueOrDefault() + nullable16.GetValueOrDefault());
        nullable1 = nullable22;
        if (!(nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue))
          return;
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.curyTranAmt>((object) row, (object) row.CuryTranAmt, (Exception) new PXSetPropertyException("The {0} purchase order line corresponding to this line already has a partial prepayment. The sum of amounts in the existing prepayments and current prepayment for purchase order line ({0}, {1}, {2}) cannot be less than the negative amount in the purchase order line.", (PXErrorLevel) 2, new object[3]
        {
          (object) poLine.OrderNbr,
          (object) poLine.OrderType,
          (object) poLine.LineNbr
        }));
      }
    }
  }
}
