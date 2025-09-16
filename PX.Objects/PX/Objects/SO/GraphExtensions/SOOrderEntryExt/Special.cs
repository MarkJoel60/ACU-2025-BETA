// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.Special
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class Special : 
  PXGraphExtension<PurchaseToSOLinkDialog, POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>
{
  protected HashSet<int?> _deletedCostCenters;
  protected List<(IBqlTable Row, PXEntryStatus Status)> _rowsToRestore;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    if (this.HasLinkedSpecialLine(e.Row))
      ((PXAction) ((PXGraphExtension<SOOrderEntry>) this).Base.Delete).SetConfirmationMessage("At least one line with a special-order item in the sales order is linked to a line of a purchase order. Do you want to remove the link and delete the sales order?");
    else
      ((PXAction) ((PXGraphExtension<SOOrderEntry>) this).Base.Delete).SetConfirmationMessage("The current {0} record will be deleted.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID> e)
  {
    if (e.Row != null && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue) && this.HasLinkedSpecialLine(e.Row))
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, e.Row.ProjectID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) pmProject?.ContractCD;
      throw new PXSetPropertyException("At least one special-order line has been linked to a purchase order. To make changes in this box, unlink the purchase orders by clicking the PO Link button for a line and unlinking the purchase order in the Purchasing Details dialog box.");
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyID> e)
  {
    if (e.Row != null && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.curyID>, PX.Objects.SO.SOOrder, object>) e).NewValue) && this.HasLinkedSpecialLine(e.Row))
      throw new PXSetPropertyException("At least one special-order line has been linked to a purchase order. To make changes in this box, unlink the purchase orders by clicking the PO Link button for a line and unlinking the purchase order in the Purchasing Details dialog box.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled> e)
  {
    bool? cancelled = e.Row.Cancelled;
    bool flag = false;
    if (!(cancelled.GetValueOrDefault() == flag & cancelled.HasValue) || !((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>, PX.Objects.SO.SOOrder, object>) e).NewValue).GetValueOrDefault() || !this.HasLinkedSpecialLine(e.Row))
      return;
    if (this.HasReceivedSpecialLine(e.Row))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) false;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>>) e).Cancel = true;
      throw new PXSetPropertyException("The sales order cannot be canceled because it contains lines with special-order items. To cancel the order, you should update the inventory costs with the special costs by creating a transfer from the Special cost layer to Normal on the Transfers (IN304000) form.");
    }
    if (PXLongOperation.IsLongOperationContext())
      throw new PXException("Sales orders with special-order lines cannot be canceled on the current form. To cancel such orders, use the Sales Orders (SO301000) form.");
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Ask("Warning", "At least one line with a special-order item in the sales order is linked to a line of the purchase order. Do you want to cancel the sales order?", (MessageButtons) 4) == 6)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) false;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.destinationSiteID> e)
  {
    if (e.Row?.Behavior == "TR" && !object.Equals(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.destinationSiteID>, PX.Objects.SO.SOOrder, object>) e).NewValue, e.OldValue) && this.HasSpecialLine(e.Row))
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, e.Row.DestinationSiteID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.destinationSiteID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) inSite?.SiteCD;
      throw new PXSetPropertyException("You can select only {0} as the destination warehouse because the transfer contains special-order items for this warehouse.", new object[1]
      {
        (object) inSite?.SiteCD?.Trim()
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e, PXRowUpdated baseEventHandler)
  {
    if (e.Row.Cancelled.GetValueOrDefault() && !e.OldRow.Cancelled.GetValueOrDefault() && this.HasSpecialLine(e.Row))
    {
      foreach (PXResult<PX.Objects.SO.SOLine> linkedSpecialLine in this.GetLinkedSpecialLines(e.Row))
        this.RemovePOLink(PXResult<PX.Objects.SO.SOLine>.op_Implicit(linkedSpecialLine));
    }
    baseEventHandler?.Invoke(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOOrder> e)
  {
    if (!this.HasLinkedSpecialLine(e.Row))
      return;
    if (this.HasBilledOrCompletedSpecialLine(e.Row))
      throw new PXException("You cannot delete the sales order because at least one special-order line is linked to a completed or billed purchase order line.");
    if (this.HasReceivedSpecialLine(e.Row))
      throw new PXException("The sales order cannot be deleted because special-order items have been received for it. To delete the order, you can transfer the items to the normal stock by creating a transfer from the Special cost layer to Normal on the Transfers (IN304000) form or write off the items by creating an issue for the Special cost layer on the Issues (IN302000) form.");
  }

  [PXMergeAttributes]
  [PXDBCalced(typeof (PX.Objects.SO.SOLine.isSpecialOrder), typeof (bool), Persistent = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.origIsSpecialOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOOrder order = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row).For<PX.Objects.SO.SOLine.isSpecialOrder>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      int num;
      if (EnumerableExtensions.IsIn<string>(e.Row.Behavior, "SO", "RM", "QT"))
      {
        bool? nullable = order.Hold;
        if (!nullable.GetValueOrDefault())
        {
          nullable = order.Cancelled;
          if (!nullable.GetValueOrDefault())
          {
            nullable = order.Completed;
            if (!nullable.GetValueOrDefault())
            {
              nullable = order.DontApprove;
              if (!nullable.GetValueOrDefault())
                goto label_7;
            }
            else
              goto label_7;
          }
          else
            goto label_7;
        }
        if (e.Row.Operation == "I" && e.Row.LineType == "GI")
        {
          num = EnumerableExtensions.IsIn<string>(e.Row.POSource, (string) null, "O") ? 1 : 0;
          goto label_8;
        }
      }
label_7:
      num = 0;
label_8:
      pxuiFieldAttribute.Enabled = num != 0;
    }));
    bool? nullable1 = e.Row.IsSpecialOrder;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = e.Row.IsCostUpdatedOnPO;
      if (nullable1.GetValueOrDefault())
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.curyUnitCost>((object) e.Row, (object) e.Row.CuryUnitCost, (Exception) new PXSetPropertyException("The cost has been updated in the related purchase order line.", (PXErrorLevel) 2));
    }
    if (!(e.Row.Behavior == "TR"))
      return;
    nullable1 = e.Row.IsSpecialOrder;
    if (!nullable1.GetValueOrDefault())
      return;
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row).For<PX.Objects.SO.SOLine.siteID>((Action<PXUIFieldAttribute>) (a => a.Enabled = false)).SameFor<PX.Objects.SO.SOLine.pOCreate>();
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.uOM> e)
  {
    PX.Objects.SO.SOLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && e.Row.POCreated.GetValueOrDefault() && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.uOM>, PX.Objects.SO.SOLine, object>) e).NewValue))
      throw new PXSetPropertyException("The line has been linked to the lines of the following purchase orders: {0}. To make changes in this column, you can try to unlink the purchase order lines by clicking the PO Link button and unlinking the purchase orders in the Purchasing Details dialog box.", new object[1]
      {
        (object) this.GetPurchaseOrderNumbers(e.Row)
      });
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taskID> e)
  {
    PX.Objects.SO.SOLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && e.Row.POCreated.GetValueOrDefault() && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taskID>, PX.Objects.SO.SOLine, object>) e).NewValue))
    {
      PMTask pmTask = PMTask.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, e.Row.ProjectID, e.Row.TaskID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taskID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) pmTask?.TaskCD;
      throw new PXSetPropertyException("The line has been linked to the lines of the following purchase orders: {0}. To make changes in this column, you can try to unlink the purchase order lines by clicking the PO Link button and unlinking the purchase orders in the Purchasing Details dialog box.", new object[1]
      {
        (object) this.GetPurchaseOrderNumbers(e.Row)
      });
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.costCodeID> e)
  {
    PX.Objects.SO.SOLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && e.Row.POCreated.GetValueOrDefault() && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.costCodeID>, PX.Objects.SO.SOLine, object>) e).NewValue))
    {
      PMCostCode pmCostCode = PMCostCode.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, e.Row.CostCodeID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.costCodeID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) pmCostCode?.CostCodeCD;
      throw new PXSetPropertyException("The line has been linked to the lines of the following purchase orders: {0}. To make changes in this column, you can try to unlink the purchase order lines by clicking the PO Link button and unlinking the purchase orders in the Purchasing Details dialog box.", new object[1]
      {
        (object) this.GetPurchaseOrderNumbers(e.Row)
      });
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID> e)
  {
    PX.Objects.SO.SOLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 && e.Row.POCreated.GetValueOrDefault() && !object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue))
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, e.Row.SiteID);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) inSite?.SiteCD;
      throw new PXSetPropertyException("The line has been linked to the lines of the following purchase orders: {0}. To make changes in this column, you can try to unlink the purchase order lines by clicking the PO Link button and unlinking the purchase orders in the Purchasing Details dialog box.", new object[1]
      {
        (object) this.GetPurchaseOrderNumbers(e.Row)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder> e)
  {
    if (e.Row == null || object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>, PX.Objects.SO.SOLine, object>) e).NewValue))
      return;
    bool? nullable1 = e.Row.POCreated;
    if (!nullable1.GetValueOrDefault())
      return;
    nullable1 = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>, PX.Objects.SO.SOLine, object>) e).NewValue;
    bool flag = false;
    if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
    {
      PXView view = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).View;
      object[] objArray1 = new object[1]{ (object) e.Row };
      object[] objArray2 = Array.Empty<object>();
      foreach (PX.Objects.SO.SOLineSplit soLineSplit in view.SelectMultiBound(objArray1, objArray2))
      {
        if (!string.IsNullOrEmpty(soLineSplit.POReceiptNbr))
        {
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) true;
          throw new PXSetPropertyException("The Special Order check box cannot be cleared because the related purchase order has been processed.");
        }
      }
      foreach (PX.Objects.PO.POOrder purchaseOrder in this.GetPurchaseOrders(e.Row))
      {
        bool? nullable2 = purchaseOrder.Cancelled;
        if (!nullable2.GetValueOrDefault())
        {
          nullable2 = purchaseOrder.Hold;
          if (!nullable2.GetValueOrDefault())
          {
            ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) true;
            throw new PXSetPropertyException("The Special Order check box cannot be cleared because the related purchase order has been processed.");
          }
        }
      }
    }
    else
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) false;
      throw new PXSetPropertyException("The Special Order check box cannot be selected because the line has already been linked to a purchase order line.");
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.completed> e)
  {
    PX.Objects.SO.SOLine row = e.Row;
    if ((row != null ? (row.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    bool? nullable = e.Row.POCreated;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = e.Row.Completed;
    if (nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.completed>, PX.Objects.SO.SOLine, object>) e).NewValue;
    if (!nullable.GetValueOrDefault())
      return;
    int? valueOriginal = (int?) ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Cache.GetValueOriginal<PX.Objects.SO.SOOrder.openShipmentCntr>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current);
    int num = 0;
    if (!(valueOriginal.GetValueOrDefault() > num & valueOriginal.HasValue) && this.HasReceivedSpecialLine(e.Row))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.completed>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) false;
      throw new PXSetPropertyException("The sales order cannot be completed because special-order items have been received for it but not fully issued. To complete the order, you can transfer the items to the normal stock by creating a transfer from the Special cost layer to Normal on the Transfers (IN304000) form or write off the items by creating an issue for the Special cost layer on the Issues (IN302000) form.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder> e)
  {
    if (e.Row == null || object.Equals(((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>, PX.Objects.SO.SOLine, object>) e).OldValue, e.NewValue) || e.Row.Operation == "R" || e.Row.Behavior == "TR")
      return;
    bool? nullable = e.Row.IsSpecialOrder;
    if (nullable.GetValueOrDefault())
    {
      nullable = e.Row.POCreate;
      if (!nullable.GetValueOrDefault())
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.pOCreate>((object) e.Row, (object) true);
      if (!(e.Row.POSource != "O"))
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.pOSource>((object) e.Row, (object) "O");
    }
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.isSpecialOrder>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.unitCost>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine> e)
  {
    bool? isSpecialOrder = e.Row.IsSpecialOrder;
    bool flag = false;
    if (!(isSpecialOrder.GetValueOrDefault() == flag & isSpecialOrder.HasValue) || !e.OldRow.IsSpecialOrder.GetValueOrDefault() || !e.OldRow.POCreated.GetValueOrDefault())
      return;
    this.RemovePOLink(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.SO.SOLine> e)
  {
    switch ((int) e.TranStatus)
    {
      case 0:
        bool? nullable = e.Row.OrigIsSpecialOrder;
        if (!nullable.GetValueOrDefault())
          break;
        nullable = e.Row.IsSpecialOrder;
        if (nullable.GetValueOrDefault() && PXDBOperationExt.Command(e.Operation) != 3)
          break;
        using (IEnumerator<PXResult<INCostCenter>> enumerator = PXSelectBase<INCostCenter, PXViewOf<INCostCenter>.BasedOn<SelectFromBase<INCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostCenter.sOOrderType, Equal<BqlField<PX.Objects.SO.SOLine.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<INCostCenter.sOOrderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<INCostCenter.sOOrderLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLine.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
        {
          (object) e.Row
        }, Array.Empty<object>()).GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.DeleteCostCenter(PXResult<INCostCenter>.op_Implicit(enumerator.Current).CostCenterID);
          break;
        }
      case 1:
        this._deletedCostCenters?.Clear();
        this._rowsToRestore?.Clear();
        break;
      case 2:
        if (this._rowsToRestore != null)
        {
          foreach ((IBqlTable Row, PXEntryStatus Status) tuple in this._rowsToRestore)
            ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[tuple.Row.GetType()].SetStatus((object) tuple.Row, tuple.Status);
          this._rowsToRestore.Clear();
        }
        this._deletedCostCenters?.Clear();
        break;
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOLine> e)
  {
    if (!e.Row.IsSpecialOrder.GetValueOrDefault() || !e.Row.POCreated.GetValueOrDefault())
      return;
    if (this.HasBilledOrCompletedSpecialLine(e.Row))
      throw new PXException("You cannot delete the special-order line because it is linked to a completed purchase order line.");
    if (this.HasReceivedSpecialLine(e.Row))
      throw new PXException("The special-order line cannot be deleted because it is linked to a received purchase order line. To delete the line, you can transfer the items to the normal stock by creating a transfer from the Special cost layer to Normal on the Transfers (IN304000) form or write off the items by creating an issue for the Special cost layer on the Issues (IN302000) form.");
    if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current) == 3 || ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Ask("Warning", "The line with a special-order item is linked to a line of a purchase order. Do you want to remove the link and delete the sales order line?", (MessageButtons) 4) == 6)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOLine> e)
  {
    if (!e.Row.IsSpecialOrder.GetValueOrDefault() || !e.Row.POCreated.GetValueOrDefault())
      return;
    this.RemovePOLink(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INCostCenter> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 2 || !(e.Row.CostLayerType == "S"))
      return;
    int? costCenterId = e.Row.CostCenterID;
    int num = 0;
    if (!(costCenterId.GetValueOrDefault() < num & costCenterId.HasValue))
      return;
    PX.Objects.SO.SOLine soLine = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Locate(new PX.Objects.SO.SOLine()
    {
      OrderType = e.Row.SOOrderType,
      OrderNbr = e.Row.SOOrderNbr,
      LineNbr = e.Row.SOOrderLineNbr
    });
    if (!EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.GetStatus((object) soLine), (PXEntryStatus) 4, (PXEntryStatus) 3) && (soLine == null || soLine.IsSpecialOrder.GetValueOrDefault()) && (soLine != null || PX.Objects.SO.SOLine.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, e.Row.SOOrderType, e.Row.SOOrderNbr, e.Row.SOOrderLineNbr) != null))
      return;
    e.Cancel = true;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INCostCenter>>) e).Cache.SetStatus((object) e.Row, (PXEntryStatus) 4);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 2)
      return;
    int? costCenterId = e.Row.CostCenterID;
    int num = 0;
    if (!(costCenterId.GetValueOrDefault() < num & costCenterId.HasValue))
      return;
    PXCache<INCostCenter> pxCache = GraphHelper.Caches<INCostCenter>((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base);
    INCostCenter inCostCenter = pxCache.Locate(new INCostCenter()
    {
      CostCenterID = e.Row.CostCenterID
    });
    if (!(inCostCenter?.CostLayerType == "S") || pxCache.GetStatus(inCostCenter) != 4)
      return;
    e.Cancel = true;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>>) e).Cache.SetStatus((object) e.Row, (PXEntryStatus) 4);
    if (!e.Row.IsZero())
      throw new PXLockViolationException(typeof (INCostCenter), (PXDBOperation) 3, new object[1]
      {
        (object) e.Row.CostCenterID
      });
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<SupplyPOLine, SupplyPOLine.selected> e)
  {
    if (e.Row == null || object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SupplyPOLine, SupplyPOLine.selected>, SupplyPOLine, object>) e).NewValue))
      return;
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    bool? nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SupplyPOLine, SupplyPOLine.selected>, SupplyPOLine, object>) e).NewValue;
    if (!nullable.GetValueOrDefault() || current == null)
      return;
    nullable = current.IsSpecialOrder;
    if (nullable.GetValueOrDefault() && this.HasPOWithDifferentCost(current, e.Row))
      throw new PXSetPropertyException("The special-order line cannot be linked to this purchase order line because it has already been linked to at least one purchase order line with a different cost.");
  }

  protected virtual PX.Objects.PO.POOrder[] GetPurchaseOrders(PX.Objects.SO.SOLine line)
  {
    return GraphHelper.RowCast<PX.Objects.PO.POOrder>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<PX.Objects.SO.SOLineSplit.FK.POOrder>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOLine.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOLine.orderNbr>, Field<PX.Objects.SO.SOLineSplit.lineNbr>.IsRelatedTo<PX.Objects.SO.SOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
    {
      (object) line
    }, Array.Empty<object>())).DistinctByKeys<PX.Objects.PO.POOrder>((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).ToArray<PX.Objects.PO.POOrder>();
  }

  protected virtual string GetPurchaseOrderNumbers(PX.Objects.SO.SOLine line)
  {
    return string.Join(", ", ((IEnumerable<PX.Objects.PO.POOrder>) this.GetPurchaseOrders(line)).Select<PX.Objects.PO.POOrder, string>((Func<PX.Objects.PO.POOrder, string>) (o => $"{o.OrderType} {o.OrderNbr}")));
  }

  protected virtual bool HasSpecialLine(PX.Objects.SO.SOOrder order)
  {
    int? specialLineCntr = order.SpecialLineCntr;
    int num = 0;
    return specialLineCntr.GetValueOrDefault() > num & specialLineCntr.HasValue;
  }

  protected virtual bool HasLinkedSpecialLine(PX.Objects.SO.SOOrder order)
  {
    return this.HasSpecialLine(order) && PXResultset<PX.Objects.SO.SOLine>.op_Implicit(this.GetLinkedSpecialLines(order)) != null;
  }

  protected virtual PXResultset<PX.Objects.SO.SOLine> GetLinkedSpecialLines(PX.Objects.SO.SOOrder order)
  {
    return PXSelectBase<PX.Objects.SO.SOLine, PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLine.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLine.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine>.SameAsCurrent>, And<BqlOperand<PX.Objects.SO.SOLine.isSpecialOrder, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.SO.SOLine.pOCreated, IBqlBool>.IsEqual<True>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
    {
      (object) order
    }, Array.Empty<object>());
  }

  protected virtual bool HasBilledOrCompletedSpecialLine(PX.Objects.SO.SOOrder order)
  {
    if (!this.HasSpecialLine(order))
      return false;
    return PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<PX.Objects.SO.SOLineSplit.FK.POLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>.SameAsCurrent>, And<BqlOperand<PX.Objects.PO.POLine.isSpecialOrder, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.billedQty, NotEqual<decimal0>>>>, Or<BqlOperand<PX.Objects.PO.POLine.completed, IBqlBool>.IsEqual<True>>>, Or<BqlOperand<PX.Objects.PO.POLine.cancelled, IBqlBool>.IsEqual<True>>>>.Or<BqlOperand<PX.Objects.PO.POLine.closed, IBqlBool>.IsEqual<True>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
    {
      (object) order
    }, Array.Empty<object>())) != null;
  }

  protected virtual bool HasBilledOrCompletedSpecialLine(PX.Objects.SO.SOLine line)
  {
    return PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<PX.Objects.SO.SOLineSplit.FK.POLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOLine.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOLine.orderNbr>, Field<PX.Objects.SO.SOLineSplit.lineNbr>.IsRelatedTo<PX.Objects.SO.SOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.SameAsCurrent>, And<BqlOperand<PX.Objects.PO.POLine.isSpecialOrder, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.billedQty, NotEqual<decimal0>>>>, Or<BqlOperand<PX.Objects.PO.POLine.completed, IBqlBool>.IsEqual<True>>>, Or<BqlOperand<PX.Objects.PO.POLine.cancelled, IBqlBool>.IsEqual<True>>>>.Or<BqlOperand<PX.Objects.PO.POLine.closed, IBqlBool>.IsEqual<True>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
    {
      (object) line
    }, Array.Empty<object>())) != null;
  }

  protected virtual bool HasReceivedSpecialLine(PX.Objects.SO.SOOrder order)
  {
    return this.HasSpecialLine(order) && this.HasReceivedSpecialLine(order.OrderType, order.OrderNbr);
  }

  protected virtual bool HasReceivedSpecialLine(PX.Objects.SO.SOLine line)
  {
    return this.HasReceivedSpecialLine(line.OrderType, line.OrderNbr, line.LineNbr);
  }

  private bool HasReceivedSpecialLine(string orderType, string orderNbr, int? lineNbr = null)
  {
    return PXResultset<INCostCenter>.op_Implicit(PXSelectBase<INCostCenter, PXViewOf<INCostCenter>.BasedOn<SelectFromBase<INCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSiteStatusByCostCenter>.On<BqlOperand<INCostCenter.costCenterID, IBqlInt>.IsEqual<INSiteStatusByCostCenter.costCenterID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostCenter.costLayerType, Equal<CostLayerType.special>>>>, And<BqlOperand<INCostCenter.sOOrderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.orderType, IBqlString>.AsOptional>>>, And<BqlOperand<INCostCenter.sOOrderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.orderNbr, IBqlString>.AsOptional>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostCenter.sOOrderLineNbr, Equal<P.AsInt>>>>>.Or<BqlOperand<Required<Parameter.ofInt>, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.qtyActual, Greater<decimal0>>>>>.Or<BqlOperand<INSiteStatusByCostCenter.qtyPOFixedReceipts, IBqlDecimal>.IsGreater<decimal0>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[4]
    {
      (object) orderType,
      (object) orderNbr,
      (object) lineNbr,
      (object) lineNbr
    })) != null;
  }

  protected virtual void RemovePOLink(PX.Objects.SO.SOLine line)
  {
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
    try
    {
      ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current = line;
      foreach (PXResult<SupplyPOLine> pxResult in ((PXSelectBase<SupplyPOLine>) ((PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>) this).Base2.SupplyPOLines).Select(Array.Empty<object>()))
      {
        SupplyPOLine supplyPoLine = PXResult<SupplyPOLine>.op_Implicit(pxResult);
        if (supplyPoLine.Selected.GetValueOrDefault())
        {
          supplyPoLine.Selected = new bool?(false);
          if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.GetStatus((object) line), (PXEntryStatus) 3, (PXEntryStatus) 4))
            supplyPoLine.SODeleted = new bool?(true);
          ((PXSelectBase<SupplyPOLine>) ((PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>) this).Base2.SupplyPOLines).Update(supplyPoLine);
        }
      }
      ((PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>) this).Base2.LinkPOSupply(line);
    }
    finally
    {
      ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current = current;
    }
  }

  protected virtual void DeleteCostCenter(int? costCenterID)
  {
    if (costCenterID.HasValue)
    {
      int? nullable = costCenterID;
      int num = 0;
      if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
      {
        if (this._deletedCostCenters == null)
          this._deletedCostCenters = new HashSet<int?>();
        if (this._rowsToRestore == null)
          this._rowsToRestore = new List<(IBqlTable, PXEntryStatus)>();
        if (PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.costCenterID, IBqlInt>.IsEqual<BqlField<INCostCenter.costCenterID, IBqlInt>.AsOptional>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
        {
          (object) costCenterID
        })) != null)
          throw new PXLockViolationException(typeof (INCostCenter), (PXDBOperation) 3, new object[1]
          {
            (object) costCenterID
          });
        this.DeleteStatusRecord<INSiteStatusByCostCenter, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(costCenterID);
        this.DeleteStatusRecord<INLocationStatusByCostCenter, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter>(costCenterID);
        this.DeleteStatusRecord<INLotSerialStatusByCostCenter, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>(costCenterID);
        if (!PXDatabase.Delete<INCostSite>(new PXDataFieldRestrict[2]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<INCostSite.costSiteID>((object) costCenterID),
          (PXDataFieldRestrict) new PXDataFieldRestrict<INCostSite.costSiteType>((object) "INCostCenter")
        }))
          throw new PXLockViolationException(typeof (INCostCenter), (PXDBOperation) 3, new object[1]
          {
            (object) costCenterID
          });
        PXCache<INCostCenter> pxCache = GraphHelper.Caches<INCostCenter>((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base);
        INCostCenter inCostCenter = pxCache.Locate(new INCostCenter()
        {
          CostCenterID = costCenterID
        });
        if (inCostCenter != null)
        {
          this._rowsToRestore.Add(((IBqlTable) inCostCenter, pxCache.GetStatus(inCostCenter)));
          pxCache.Remove(inCostCenter);
        }
        if (!PXDatabase.Delete<INCostCenter>(new PXDataFieldRestrict[1]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<INCostCenter.costCenterID>((object) costCenterID)
        }))
          throw new PXLockViolationException(typeof (INCostCenter), (PXDBOperation) 3, new object[1]
          {
            (object) costCenterID
          });
        this._deletedCostCenters.Add(costCenterID);
        return;
      }
    }
    throw new PXArgumentException(nameof (costCenterID));
  }

  protected virtual void DeleteStatusRecord<TStatus, TStatusAccumulator>(int? costCenterID)
    where TStatus : class, IStatus, IBqlTable, new()
    where TStatusAccumulator : class, IStatus, IBqlTable, new()
  {
    PXCache cach = ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (TStatusAccumulator)];
    foreach (TStatusAccumulator accumulator in cach.Inserted)
    {
      int? nullable1 = (int?) cach.GetValue((object) accumulator, "CostCenterID");
      int? nullable2 = costCenterID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        this.DeleteStatusRecord<TStatus, TStatusAccumulator>(cach, costCenterID, accumulator);
        this._rowsToRestore.Add(((IBqlTable) accumulator, (PXEntryStatus) 2));
        cach.Remove((object) accumulator);
      }
    }
    this.DeleteStatusRecord<TStatus, TStatusAccumulator>(cach, costCenterID, default (TStatusAccumulator));
  }

  protected virtual void DeleteStatusRecord<TStatus, TStatusAccumulator>(
    PXCache cache,
    int? costCenterID,
    TStatusAccumulator accumulator)
    where TStatus : class, IStatus, IBqlTable, new()
    where TStatusAccumulator : class, IStatus, IBqlTable, new()
  {
    List<PXDataFieldRestrict> dataFieldRestrictList = new List<PXDataFieldRestrict>(cache.BqlFields.Count);
    foreach (string field in (List<string>) cache.Fields)
    {
      if (field.StartsWith("qty", StringComparison.OrdinalIgnoreCase) && cache.GetAttributesReadonly(field).OfType<PXDBDecimalAttribute>().Any<PXDBDecimalAttribute>())
      {
        Decimal? nullable = (object) accumulator != null ? (Decimal?) cache.GetValue((object) accumulator, field) : new Decimal?();
        dataFieldRestrictList.Add(new PXDataFieldRestrict(field, (object) (-1M * nullable.GetValueOrDefault())));
      }
    }
    if ((object) accumulator != null)
    {
      foreach (string key in (IEnumerable<string>) cache.Keys)
        dataFieldRestrictList.Add(new PXDataFieldRestrict(key, cache.GetValue((object) accumulator, key)));
    }
    else
      dataFieldRestrictList.Add(new PXDataFieldRestrict("CostCenterID", (object) costCenterID));
    PXDatabase.Delete<TStatus>(dataFieldRestrictList.ToArray());
  }

  protected virtual void OnBeforeCommitValidateDeletedCostCenter(int? costCenterID)
  {
    if (this.FindCostCenterData<INCostCenter>(costCenterID) || this.FindCostCenterData<INItemPlan>(costCenterID) || this.FindCostCenterData<INSiteStatusByCostCenter>(costCenterID) || this.FindCostCenterData<INLocationStatusByCostCenter>(costCenterID) || this.FindCostCenterData<INLotSerialStatusByCostCenter>(costCenterID))
      throw new PXLockViolationException(typeof (INCostCenter), (PXDBOperation) 3, new object[1]
      {
        (object) costCenterID
      });
  }

  protected virtual bool FindCostCenterData<TDac>(int? costCenterID) where TDac : class, IBqlTable, new()
  {
    return PXDatabase.SelectSingle<TDac>(new PXDataField[2]
    {
      (PXDataField) new PXDataFieldValue("CostCenterID", (object) costCenterID),
      new PXDataField("CostCenterID")
    }) != null;
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).OnBeforeCommit += (Action<PXGraph>) (graph =>
    {
      HashSet<int?> deletedCostCenters = this._deletedCostCenters;
      if (deletedCostCenters == null)
        return;
      EnumerableExtensions.ForEach<int?>((IEnumerable<int?>) deletedCostCenters, new Action<int?>(this.OnBeforeCommitValidateDeletedCostCenter));
    });
    ((PXAction) ((PXGraphExtension<SOOrderEntry>) this).Base.Delete).SetDynamicText(true);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.IsCuryUnitCostEnabled(PX.Objects.SO.SOLine,PX.Objects.SO.SOOrder)" />
  /// </summary>
  [PXOverride]
  public virtual bool IsCuryUnitCostEnabled(
    PX.Objects.SO.SOLine line,
    PX.Objects.SO.SOOrder order,
    Func<PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, bool> baseMethod)
  {
    bool? nullable;
    int num;
    if (line == null)
    {
      num = 1;
    }
    else
    {
      nullable = line.IsSpecialOrder;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
      return baseMethod(line, order);
    nullable = line.POCreated;
    return !nullable.GetValueOrDefault() && line.Behavior != "TR" && line.Operation == "I";
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.IsCuryUnitCostVisible(PX.Objects.SO.SOOrder)" />
  /// </summary>
  [PXOverride]
  public virtual bool IsCuryUnitCostVisible(PX.Objects.SO.SOOrder order, Func<PX.Objects.SO.SOOrder, bool> baseMethod)
  {
    return true;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.PurchaseSupplyBaseExt.SetPOCreateEnabled(PX.Data.PXCache,PX.Objects.SO.SOLine,System.Boolean)" />
  /// </summary>
  [PXOverride]
  public virtual void SetPOCreateEnabled(
    PXCache cache,
    PX.Objects.SO.SOLine soline,
    bool poCreateEnabled,
    Action<PXCache, PX.Objects.SO.SOLine, bool> baseMethod)
  {
    baseMethod(cache, soline, poCreateEnabled && (soline == null || !soline.IsSpecialOrder.GetValueOrDefault()));
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.PurchaseSupplyBaseExt.ClearPOFieldsOnWarehouseChange(PX.Data.PXCache,PX.Objects.SO.SOLine)" />
  /// </summary>
  [PXOverride]
  public virtual void ClearPOFieldsOnWarehouseChange(
    PXCache cache,
    PX.Objects.SO.SOLine line,
    Action<PXCache, PX.Objects.SO.SOLine> baseMethod)
  {
    if (line.IsSpecialOrder.GetValueOrDefault())
      return;
    baseMethod(cache, line);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.PurchaseToSOLinkDialog.UnlinkSupply(PX.Objects.SO.SupplyPOLine,PX.Objects.SO.SOLine,System.Collections.Generic.IList{PX.Objects.SO.SOLineSplit},System.Boolean)" />
  /// </summary>
  [PXOverride]
  public virtual bool UnlinkSupply(
    SupplyPOLine supply,
    PX.Objects.SO.SOLine currentSOLine,
    IList<PX.Objects.SO.SOLineSplit> splits,
    bool deleted,
    Func<SupplyPOLine, PX.Objects.SO.SOLine, IList<PX.Objects.SO.SOLineSplit>, bool, bool> baseMethod)
  {
    bool flag = baseMethod(supply, currentSOLine, splits, deleted);
    if (supply.IsSpecialOrder.GetValueOrDefault())
    {
      supply.IsSpecialOrder = new bool?(false);
      supply.CostCenterID = new int?(0);
      ((PXSelectBase<SupplyPOLine>) ((PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>) this).Base2.SupplyPOLines).Update(supply);
      if (supply.PlanID.HasValue)
      {
        INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<INItemPlan.planID, IBqlLong>.AsOptional>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
        {
          (object) supply.PlanID
        }));
        if (inItemPlan == null)
          throw new RowNotFoundException((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base), new object[1]
          {
            (object) supply.PlanID
          });
        inItemPlan.CostCenterID = new int?(0);
        ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (INItemPlan)].Update((object) inItemPlan);
      }
    }
    return flag;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.PurchaseToSOLinkDialog.LinkSupply(PX.Objects.SO.SupplyPOLine,PX.Objects.SO.SOLine,System.Collections.Generic.IList{PX.Objects.SO.SOLineSplit})" />
  /// </summary>
  [PXOverride]
  public virtual bool LinkSupply(
    SupplyPOLine supply,
    PX.Objects.SO.SOLine currentSOLine,
    IList<PX.Objects.SO.SOLineSplit> splits,
    Func<SupplyPOLine, PX.Objects.SO.SOLine, IList<PX.Objects.SO.SOLineSplit>, bool> baseMethod)
  {
    if (currentSOLine.IsSpecialOrder.GetValueOrDefault())
    {
      supply.IsSpecialOrder = new bool?(true);
      supply.CostCenterID = currentSOLine.CostCenterID;
      ((PXSelectBase<SupplyPOLine>) ((PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>) this).Base2.SupplyPOLines).Update(supply);
      currentSOLine = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Locate(currentSOLine) ?? currentSOLine;
      Decimal? curyUnitCost1 = currentSOLine.CuryUnitCost;
      Decimal? curyUnitCost2 = supply.CuryUnitCost;
      if (!(curyUnitCost1.GetValueOrDefault() == curyUnitCost2.GetValueOrDefault() & curyUnitCost1.HasValue == curyUnitCost2.HasValue))
      {
        currentSOLine.CuryUnitCost = supply.CuryUnitCost;
        currentSOLine = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Update(currentSOLine);
      }
    }
    return baseMethod(supply, currentSOLine, splits);
  }

  protected virtual bool HasPOWithDifferentCost(PX.Objects.SO.SOLine soline, SupplyPOLine newPOLine)
  {
    foreach (PXResult<SupplyPOLine> pxResult in ((PXSelectBase<SupplyPOLine>) ((PXGraphExtension<POLinkDialog, PurchaseSupplyBaseExt, SOOrderEntry>) this).Base2.SupplyPOLines).Select(Array.Empty<object>()))
    {
      SupplyPOLine supplyPoLine = PXResult<SupplyPOLine>.op_Implicit(pxResult);
      if (supplyPoLine.Selected.GetValueOrDefault())
      {
        Decimal? curyUnitCost1 = supplyPoLine.CuryUnitCost;
        Decimal? curyUnitCost2 = newPOLine.CuryUnitCost;
        if (!(curyUnitCost1.GetValueOrDefault() == curyUnitCost2.GetValueOrDefault() & curyUnitCost1.HasValue == curyUnitCost2.HasValue))
          return true;
      }
    }
    return false;
  }

  [PXLocalizable]
  public static class ExtensionMessages
  {
    public const string SpecialLineExistsDeleteOrder = "At least one line with a special-order item in the sales order is linked to a line of a purchase order. Do you want to remove the link and delete the sales order?";
    public const string SpecialCheckboxCannotBeSelectedPOExists = "The Special Order check box cannot be selected because the line has already been linked to a purchase order line.";
  }
}
