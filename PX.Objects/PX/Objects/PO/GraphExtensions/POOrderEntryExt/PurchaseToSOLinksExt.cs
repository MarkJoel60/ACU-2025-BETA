// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.PurchaseToSOLinksExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class PurchaseToSOLinksExt : PXGraphExtension<POOrderEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.dropShipments>() || PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>();
  }

  protected virtual void POOrder_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.PO.POOrder row = e.Row as PX.Objects.PO.POOrder;
    if (e.Row == null || row.OrderType == "DP")
      return;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POOrder.sOOrderType>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POOrder.sOOrderNbr>(cache, (object) row, false);
  }

  protected virtual void POLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PX.Objects.PO.POLine row = (PX.Objects.PO.POLine) e.Row;
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || row == null)
      return;
    bool? nullable = current.IsLegacyDropShip;
    if (!nullable.GetValueOrDefault() && current.OrderType == "DP" && EnumerableExtensions.IsIn<string>(row.LineType, "GP", "NP") || ((PXGraph) this.Base).IsExport && !((PXGraph) this.Base).IsContractBasedAPI)
      return;
    nullable = current.IsLegacyDropShip;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.Completed;
      if (nullable.GetValueOrDefault())
      {
        num = this.Base.IsLinkedToSO(row) ? 1 : 0;
        goto label_7;
      }
    }
    num = 0;
label_7:
    bool flag = num != 0;
    nullable = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.Hold;
    if (!(!nullable.GetValueOrDefault() | flag))
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (current.Status == "H" & flag)
      propertyException = new PXSetPropertyException("The line cannot be reopened because it is linked to a sales order.", (PXErrorLevel) 3);
    ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.lineType>((object) row, (object) row.LineType, (Exception) propertyException);
  }

  protected virtual void POLine_InventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    PX.Objects.PO.POLine row = (PX.Objects.PO.POLine) e.Row;
    if (current == null || current.OrderType == "DP" && !current.IsLegacyDropShip.GetValueOrDefault() || (row != null ? (!row.InventoryID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PX.Objects.SO.SOLineSplit soLineSplit = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.pOType, Equal<Required<PX.Objects.SO.SOLineSplit.pOType>>, And<PX.Objects.SO.SOLineSplit.pONbr, Equal<Required<PX.Objects.SO.SOLineSplit.pONbr>>, And<PX.Objects.SO.SOLineSplit.pOLineNbr, Equal<Required<PX.Objects.SO.SOLineSplit.pOLineNbr>>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[3]
    {
      (object) row.OrderType,
      (object) row.OrderNbr,
      (object) row.LineNbr
    }));
    if (soLineSplit == null)
      return;
    int? inventoryId = soLineSplit.InventoryID;
    int? newValue = (int?) e.NewValue;
    if (!(inventoryId.GetValueOrDefault() == newValue.GetValueOrDefault() & inventoryId.HasValue == newValue.HasValue))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, (int?) e.NewValue);
      PXSetPropertyException<PX.Objects.PO.POLine.inventoryID> propertyException = new PXSetPropertyException<PX.Objects.PO.POLine.inventoryID>("This line is linked to a sales order line. To replace the item in this line, you have to replace it in the linked sales order first.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) inventoryItem?.InventoryCD;
      throw propertyException;
    }
  }

  protected virtual void POOrder_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    PX.Objects.PO.POOrder row = (PX.Objects.PO.POOrder) e.Row;
    if (row.OrderType == "DP" && !row.IsLegacyDropShip.GetValueOrDefault())
      return;
    ((PXSelectBase) this.Base.Transactions).View.SetAnswer((string) null, (WebDialogResult) 1);
  }

  protected virtual void POLine_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    PX.Objects.PO.POLine row = (PX.Objects.PO.POLine) e.Row;
    bool? nullable;
    if (!(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderType != "DP"))
    {
      nullable = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IsLegacyDropShip;
      if (!nullable.GetValueOrDefault())
        return;
    }
    if (!EnumerableExtensions.IsIn<string>(row.LineType, "GS", "GP", "NO", "NP"))
      return;
    nullable = row.IsSpecialOrder;
    if (nullable.GetValueOrDefault())
      return;
    PX.Objects.SO.SOLineSplit soLineSplit;
    using (new PXFieldScope(((PXSelectBase) this.Base.RelatedSOLineSplit).View, new Type[4]
    {
      typeof (PX.Objects.SO.SOLineSplit.orderType),
      typeof (PX.Objects.SO.SOLineSplit.orderNbr),
      typeof (PX.Objects.SO.SOLineSplit.lineNbr),
      typeof (PX.Objects.SO.SOLineSplit.splitLineNbr)
    }))
      soLineSplit = (PX.Objects.SO.SOLineSplit) ((PXSelectBase) this.Base.RelatedSOLineSplit).View.SelectMultiBound(new object[1]
      {
        e.Row
      }, Array.Empty<object>()).FirstOrDefault<object>();
    if (soLineSplit == null)
      return;
    if (((PXSelectBase) this.Base.Transactions).View.Ask(PXMessages.LocalizeFormatNoPrefixNLA("Deletion of the purchase order line will unlink sales order '{0}' from this purchase order. Do you want to continue?", new object[1]
    {
      (object) soLineSplit.OrderNbr
    }), (MessageButtons) 1) != 2)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void POLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderType == "DP" && !((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IsLegacyDropShip.GetValueOrDefault())
      return;
    PX.Objects.PO.POLine row = (PX.Objects.PO.POLine) e.Row;
    using (new PXFieldScope(((PXSelectBase) this.Base.RelatedSOLineSplit).View, new Type[4]
    {
      typeof (PX.Objects.SO.SOLineSplit.orderType),
      typeof (PX.Objects.SO.SOLineSplit.orderNbr),
      typeof (PX.Objects.SO.SOLineSplit.lineNbr),
      typeof (PX.Objects.SO.SOLineSplit.splitLineNbr)
    }))
    {
      foreach (PX.Objects.SO.SOLineSplit soLineSplit in ((PXSelectBase) this.Base.RelatedSOLineSplit).View.SelectMultiBound(new object[1]
      {
        e.Row
      }, Array.Empty<object>()))
      {
        POOrderEntry.SOLineSplit3 split = PXResultset<POOrderEntry.SOLineSplit3>.op_Implicit(PXSelectBase<POOrderEntry.SOLineSplit3, PXSelect<POOrderEntry.SOLineSplit3, Where<POOrderEntry.SOLineSplit3.orderType, Equal<Required<POOrderEntry.SOLineSplit3.orderType>>, And<POOrderEntry.SOLineSplit3.orderNbr, Equal<Required<POOrderEntry.SOLineSplit3.orderNbr>>, And<POOrderEntry.SOLineSplit3.lineNbr, Equal<Required<POOrderEntry.SOLineSplit3.lineNbr>>, And<POOrderEntry.SOLineSplit3.splitLineNbr, Equal<Required<POOrderEntry.SOLineSplit3.splitLineNbr>>>>>>>.Config>.Select((PXGraph) this.Base, new object[4]
        {
          (object) soLineSplit.OrderType,
          (object) soLineSplit.OrderNbr,
          (object) soLineSplit.LineNbr,
          (object) soLineSplit.SplitLineNbr
        }));
        split.POType = (string) null;
        split.PONbr = (string) null;
        split.POLineNbr = new int?();
        split.POCancelled = new bool?(false);
        split.POCompleted = new bool?(false);
        split.RefNoteID = new Guid?();
        bool poCreated = false;
        bool? nullable1 = split.POCreated;
        if (nullable1.GetValueOrDefault())
          poCreated = PXResultset<POOrderEntry.SOLineSplit3>.op_Implicit(PXSelectBase<POOrderEntry.SOLineSplit3, PXSelect<POOrderEntry.SOLineSplit3, Where<POOrderEntry.SOLineSplit3.orderType, Equal<Required<POOrderEntry.SOLineSplit3.orderType>>, And<POOrderEntry.SOLineSplit3.orderNbr, Equal<Required<POOrderEntry.SOLineSplit3.orderNbr>>, And<POOrderEntry.SOLineSplit3.lineNbr, Equal<Required<POOrderEntry.SOLineSplit3.lineNbr>>, And<POOrderEntry.SOLineSplit3.pONbr, IsNotNull, And<POOrderEntry.SOLineSplit3.splitLineNbr, NotEqual<Required<POOrderEntry.SOLineSplit3.splitLineNbr>>>>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[4]
          {
            (object) split.OrderType,
            (object) split.OrderNbr,
            (object) split.LineNbr,
            (object) split.SplitLineNbr
          })) != null;
        this.Base.UpdateSOLine(split, split.VendorID, poCreated);
        nullable1 = split.LinePOCreate;
        bool flag1 = false;
        if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
          split.POCreate = new bool?(false);
        ((PXSelectBase<POOrderEntry.SOLineSplit3>) this.Base.FixedDemand).Update(split);
        INItemPlan inItemPlan1 = INItemPlan.PK.Find((PXGraph) this.Base, split.PlanID);
        if (inItemPlan1 != null && inItemPlan1.PlanType != null)
        {
          long? supplyPlanId = inItemPlan1.SupplyPlanID;
          long? nullable2 = row.PlanID;
          if (supplyPlanId.GetValueOrDefault() == nullable2.GetValueOrDefault() & supplyPlanId.HasValue == nullable2.HasValue)
          {
            nullable1 = split.POCreate;
            bool flag2 = false;
            if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
            {
              SOOrderTypeOperation orderTypeOperation = SOOrderTypeOperation.PK.Find((PXGraph) this.Base, split.OrderType, split.Operation);
              if (orderTypeOperation != null && orderTypeOperation.OrderPlanType != null)
                inItemPlan1.PlanType = orderTypeOperation.OrderPlanType;
            }
            INItemPlan inItemPlan2 = inItemPlan1;
            nullable2 = new long?();
            long? nullable3 = nullable2;
            inItemPlan2.SupplyPlanID = nullable3;
            sender.Graph.Caches[typeof (INItemPlan)].Update((object) inItemPlan1);
          }
        }
      }
    }
  }

  protected virtual void POLine_SiteID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PX.Objects.PO.POLine row = (PX.Objects.PO.POLine) e.Row;
    if (!(e.NewValue is int newValue1))
      return;
    int newValue = newValue1;
    if (!EnumerableExtensions.IsIn<string>(row.LineType, "GS", "NO"))
      return;
    POOrderEntry.SOLineSplit3 soLineSplit3 = GraphHelper.RowCast<POOrderEntry.SOLineSplit3>((IEnumerable) ((IEnumerable<PXResult<POOrderEntry.SOLineSplit3>>) ((PXSelectBase<POOrderEntry.SOLineSplit3>) this.Base.FixedDemand).Select(Array.Empty<object>())).AsEnumerable<PXResult<POOrderEntry.SOLineSplit3>>()).FirstOrDefault<POOrderEntry.SOLineSplit3>((Func<POOrderEntry.SOLineSplit3, bool>) (s =>
    {
      if (!(s.Behavior == "BL"))
        return false;
      int? siteId = s.SiteID;
      int num = newValue;
      return !(siteId.GetValueOrDefault() == num & siteId.HasValue);
    }));
    if (soLineSplit3 != null)
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, new int?(newValue));
      throw new PXSetPropertyException("Cannot change the warehouse because the line has a link to a line of the {0} blanket sales order.", new object[1]
      {
        (object) soLineSplit3.OrderNbr
      })
      {
        ErrorValue = (inSite != null ? (object) inSite.SiteCD : (object) null) ?? e.NewValue
      };
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POOrderEntry.Persist" />
  /// </summary>
  [PXOverride]
  public virtual void Persist(Action baseMethod)
  {
    this.UpdateDemandSchedules();
    this.CleanLinksToDeletedSupplyPlans();
    baseMethod();
  }

  protected virtual void UpdateDemandSchedules()
  {
    PXCache lineCache = ((PXSelectBase) this.Base.Transactions).Cache;
    foreach (PX.Objects.PO.POLine poLine in lineCache.Inserted.Cast<PX.Objects.PO.POLine>().Where<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (l =>
    {
      if (!EnumerableExtensions.IsIn<string>(l.LineType, "GS", "NO"))
        return false;
      return l.Completed.GetValueOrDefault() || l.Cancelled.GetValueOrDefault();
    })).Union<PX.Objects.PO.POLine>(lineCache.Updated.Cast<PX.Objects.PO.POLine>()).Where<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (l =>
    {
      if (!EnumerableExtensions.IsIn<string>(l.LineType, "GS", "NO"))
        return false;
      bool? nullable1 = l.Completed;
      bool? nullable2 = (bool?) lineCache.GetValueOriginal<PX.Objects.PO.POLine.completed>((object) l);
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return true;
      nullable2 = l.Cancelled;
      nullable1 = (bool?) lineCache.GetValueOriginal<PX.Objects.PO.POLine.cancelled>((object) l);
      return !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue);
    })))
    {
      foreach (POOrderEntry.SOLineSplit3 soLineSplit3 in ((PXSelectBase) this.Base.FixedDemand).View.SelectMultiBound((object[]) new PX.Objects.PO.POLine[1]
      {
        poLine
      }, Array.Empty<object>()))
      {
        bool? nullable = soLineSplit3.POCompleted;
        bool? completed = poLine.Completed;
        if (nullable.GetValueOrDefault() == completed.GetValueOrDefault() & nullable.HasValue == completed.HasValue)
        {
          bool? poCancelled = soLineSplit3.POCancelled;
          nullable = poLine.Cancelled;
          if (poCancelled.GetValueOrDefault() == nullable.GetValueOrDefault() & poCancelled.HasValue == nullable.HasValue)
            continue;
        }
        soLineSplit3.POCompleted = poLine.Completed;
        soLineSplit3.POCancelled = poLine.Cancelled;
        ((PXSelectBase<POOrderEntry.SOLineSplit3>) this.Base.FixedDemand).Update(soLineSplit3);
      }
    }
  }

  protected virtual void CleanLinksToDeletedSupplyPlans()
  {
    PXCache cach = ((PXGraph) this.Base).Caches[typeof (INItemPlan)];
    foreach (INItemPlan inItemPlan1 in cach.Deleted.Cast<INItemPlan>().Where<INItemPlan>((Func<INItemPlan, bool>) (p => EnumerableExtensions.IsIn<string>(p.PlanType, "74", "79", "78", "76"))))
    {
      foreach (INItemPlan inItemPlan2 in GraphHelper.RowCast<INItemPlan>((IEnumerable) PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.supplyPlanID, IBqlLong>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inItemPlan1.PlanID
      })))
      {
        inItemPlan2.SupplyPlanID = new long?();
        GraphHelper.MarkUpdated(cach, (object) inItemPlan2);
      }
    }
  }
}
