// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.Intercompany
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class Intercompany : PXGraphExtension<SOOrderEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    int num1 = !string.IsNullOrEmpty(eventArgs.Row.IntercompanyPONbr) ? 1 : (!string.IsNullOrEmpty(eventArgs.Row.IntercompanyPOReturnNbr) ? 1 : 0);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<PX.Objects.SO.SOOrder.intercompanyPOType>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = eventArgs.Row.IsIntercompany.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(eventArgs.Row.Behavior, "SO", "IN");
      a.Enabled = false;
    })).SameFor<PX.Objects.SO.SOOrder.intercompanyPONbr>();
    chained.For<PX.Objects.SO.SOOrder.intercompanyPOReturnNbr>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = eventArgs.Row.IsIntercompany.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(eventArgs.Row.Behavior, "RM", "CM");
      a.Enabled = false;
    }));
    if (num1 != 0)
    {
      SOSetup current1 = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current;
      bool? nullable;
      int num2;
      if (current1 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current1.DisableAddingItemsForIntercompany;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 != 0)
      {
        ((PXAction) this.Base.addInvoice).SetEnabled(false);
        if ((!(eventArgs.Row.Behavior == "RM") ? 0 : (KeysRelation<Field<SOOrderTypeOperation.orderType>.IsRelatedTo<PX.Objects.SO.SOOrderType.orderType>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOOrderType, SOOrderTypeOperation>, PX.Objects.SO.SOOrderType, SOOrderTypeOperation>.SelectChildren((PXGraph) this.Base, ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current).Any<SOOrderTypeOperation>((Func<SOOrderTypeOperation, bool>) (op => op.Operation == "I" && op.Active.GetValueOrDefault())) ? 1 : 0)) == 0)
        {
          ((PXAction) ((PXGraph) this.Base).GetExtension<SOOrderSiteStatusLookupExt>().showItems).SetEnabled(false);
          if (((OrderedDictionary) ((PXGraph) this.Base).Actions).Contains((object) "showMatrixPanel"))
            ((PXGraph) this.Base).Actions["showMatrixPanel"].SetEnabled(false);
          ((PXSelectBase) this.Base.Transactions).Cache.AllowInsert = false;
          ((PXSelectBase) this.Base.Transactions).Cache.AllowDelete = false;
        }
      }
      SOSetup current2 = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current;
      int num3;
      if (current2 == null)
      {
        num3 = 0;
      }
      else
      {
        nullable = current2.DisableEditingPricesDiscountsForIntercompany;
        num3 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num3 != 0 && EnumerableExtensions.IsIn<string>(eventArgs.Row.Behavior, "SO", "IN"))
      {
        ((PXAction) this.Base.recalculateDiscountsAction).SetEnabled(false);
        attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row);
        chained = attributeAdjuster.For<PX.Objects.SO.SOOrder.curyDiscTot>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
        chained.SameFor<PX.Objects.SO.SOOrder.disableAutomaticDiscountCalculation>();
        ((PXSelectBase) this.Base.DiscountDetails).Cache.AllowInsert = false;
        ((PXSelectBase) this.Base.DiscountDetails).Cache.AllowDelete = false;
        attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.DiscountDetails).Cache, (object) null);
        chained = attributeAdjuster.For<SOOrderDiscountDetail.curyDiscountAmt>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
        chained.SameFor<SOOrderDiscountDetail.discountPct>();
      }
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row);
      attributeAdjuster.For<PX.Objects.SO.SOOrder.customerID>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    }
    if (string.IsNullOrEmpty(eventArgs.Row.IntercompanyPONbr))
      return;
    PX.Objects.PO.POOrder poOrder;
    using (new PXReadBranchRestrictedScope())
      poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) this.Base, eventArgs.Row.IntercompanyPOType, eventArgs.Row.IntercompanyPONbr);
    if (poOrder == null)
      return;
    if (poOrder.Cancelled.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.intercompanyPONbr>((object) eventArgs.Row, (object) eventArgs.Row.IntercompanyPONbr, (Exception) new PXSetPropertyException("The related purchase order has been canceled.", (PXErrorLevel) 2));
    }
    else
    {
      if (eventArgs.Row.IntercompanyPOWithEmptyInventory.GetValueOrDefault())
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.intercompanyPONbr>((object) eventArgs.Row, (object) eventArgs.Row.IntercompanyPONbr, (Exception) new PXSetPropertyException("The lines without inventory ID cannot be copied to a sales order. The sales order has been created without these lines.", (PXErrorLevel) 2));
      Decimal? nullable1 = eventArgs.Row.CuryTaxTotal;
      Decimal? nullable2 = poOrder.CuryTaxTotal;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) && string.IsNullOrEmpty(PXUIFieldAttribute.GetWarning<PX.Objects.SO.SOOrder.curyTaxTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row)))
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.curyTaxTotal>((object) eventArgs.Row, (object) eventArgs.Row.CuryTaxTotal, (Exception) new PXSetPropertyException("The sales order tax total differs from the tax total in the related purchase order.", (PXErrorLevel) 2));
      nullable2 = eventArgs.Row.CuryOrderTotal;
      nullable1 = poOrder.CuryOrderTotal;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue || !string.IsNullOrEmpty(PXUIFieldAttribute.GetWarning<PX.Objects.SO.SOOrder.curyOrderTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, (object) eventArgs.Row)))
        return;
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.curyOrderTotal>((object) eventArgs.Row, (object) eventArgs.Row.CuryOrderTotal, (Exception) new PXSetPropertyException("The sales order total differs from the order total in the related purchase order.", (PXErrorLevel) 2));
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLine> eventArgs)
  {
    if (eventArgs.Row == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.IntercompanyPONbr) && string.IsNullOrEmpty(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.IntercompanyPOReturnNbr) || !eventArgs.Row.IntercompanyPOLineNbr.HasValue)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) eventArgs).Cache, (object) eventArgs.Row);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<PX.Objects.SO.SOLine.inventoryID>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    chained = chained.SameFor<PX.Objects.SO.SOLine.isFree>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.orderQty>();
    chained.SameFor<PX.Objects.SO.SOLine.uOM>();
    SOSetup current = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current;
    if ((current != null ? (current.DisableEditingPricesDiscountsForIntercompany.GetValueOrDefault() ? 1 : 0) : 0) == 0 || !EnumerableExtensions.IsIn<string>(eventArgs.Row.Behavior, "SO", "IN"))
      return;
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) eventArgs).Cache, (object) eventArgs.Row);
    chained = attributeAdjuster.For<PX.Objects.SO.SOLine.curyUnitPrice>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    chained = chained.SameFor<PX.Objects.SO.SOLine.manualPrice>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.curyExtPrice>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.discPct>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.curyDiscAmt>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.manualDisc>();
    chained.SameFor<PX.Objects.SO.SOLine.discountID>();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(e.Operation, (PXDBOperation) 2, (PXDBOperation) 1) || !e.Row.IntercompanyPOLineNbr.HasValue || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.IntercompanyPONbr == null || !e.Row.OrderQty.HasValue || !e.Row.VerifyOrderQty.HasValue)
      return;
    Decimal? orderQty = e.Row.OrderQty;
    Decimal? verifyOrderQty = e.Row.VerifyOrderQty;
    if (orderQty.GetValueOrDefault() == verifyOrderQty.GetValueOrDefault() & orderQty.HasValue == verifyOrderQty.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) e.Row, (object) e.Row.OrderQty, (Exception) new PXSetPropertyException("The quantity cannot be changed for a line of an intercompany sales order. Click Line Details on the table toolbar and correct the quantities in the line splits to have {0} in total.", new object[1]
    {
      (object) e.Row.VerifyOrderQty
    }));
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLineSplit> eventArgs)
  {
    if (eventArgs.Row == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.IntercompanyPONbr) && string.IsNullOrEmpty(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.IntercompanyPOReturnNbr))
      return;
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Current;
    if ((current != null ? (current.IntercompanyPOLineNbr.HasValue ? 1 : 0) : 0) == 0)
      return;
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLineSplit>>) eventArgs).Cache, (object) eventArgs.Row).For<PX.Objects.SO.SOLineSplit.qty>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
  }

  protected virtual void SOLine_SalesAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    PXFieldDefaulting baseFunc)
  {
    PX.Objects.SO.SOLine row = (PX.Objects.SO.SOLine) e.Row;
    if (row != null)
    {
      PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      bool? nullable;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current1.IsTransferOrder;
        bool flag = false;
        num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
      }
      if (num1 != 0)
      {
        PX.Objects.AR.Customer current2 = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current;
        int num2;
        if (current2 == null)
        {
          num2 = 0;
        }
        else
        {
          nullable = current2.IsBranch;
          num2 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num2 != 0)
        {
          PX.Objects.IN.InventoryItem data = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryID);
          if (data == null)
            return;
          switch (((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.IntercompanySalesAcctDefault)
          {
            case "I":
              e.NewValue = this.Base.GetValue<PX.Objects.IN.InventoryItem.salesAcctID>((object) data);
              ((CancelEventArgs) e).Cancel = true;
              return;
            case "L":
              PX.Objects.CR.Location current3 = ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).Current;
              e.NewValue = this.Base.GetValue<PX.Objects.CR.Location.cSalesAcctID>((object) current3);
              ((CancelEventArgs) e).Cancel = true;
              return;
            default:
              return;
          }
        }
      }
    }
    baseFunc.Invoke(sender, e);
  }
}
