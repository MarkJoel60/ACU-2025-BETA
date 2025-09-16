// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.RQ;

public class RQRequestClassMaint : PXGraph<RQRequestClassMaint, RQRequestClass>
{
  public PXSelect<RQRequestClass> Classes;
  public PXSelect<RQRequestClass, Where<RQRequestClass.reqClassID, Equal<Current<RQRequestClass.reqClassID>>>> CurrentClass;
  public PXSelectJoin<RQRequestClassItem, InnerJoin<RQInventoryItem, On<RQInventoryItem.inventoryID, Equal<RQRequestClassItem.inventoryID>>>, Where<RQRequestClassItem.reqClassID, Equal<Optional<RQRequestClass.reqClassID>>>> ClassItems;

  protected virtual void RQRequestClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQRequestClass row = (RQRequestClass) e.Row;
    if (row == null)
      return;
    PXCache cache1 = ((PXSelectBase) this.ClassItems).Cache;
    PXCache cache2 = ((PXSelectBase) this.ClassItems).Cache;
    PXCache cache3 = ((PXSelectBase) this.ClassItems).Cache;
    bool? restrictItemList = row.RestrictItemList;
    int num1;
    bool flag1 = (num1 = restrictItemList.GetValueOrDefault() ? 1 : 0) != 0;
    cache3.AllowDelete = num1 != 0;
    int num2;
    bool flag2 = (num2 = flag1 ? 1 : 0) != 0;
    cache2.AllowUpdate = num2 != 0;
    int num3 = flag2 ? 1 : 0;
    cache1.AllowInsert = num3 != 0;
    int? budgetValidation = row.BudgetValidation;
    int num4 = 0;
    bool flag3 = budgetValidation.GetValueOrDefault() > num4 & budgetValidation.HasValue;
    PXUIFieldAttribute.SetEnabled<RQRequestClass.expenseAccountDefault>(sender, (object) row, flag3);
    PXUIFieldAttribute.SetEnabled<RQRequestClass.expenseSubMask>(sender, (object) row, flag3);
    PXUIFieldAttribute.SetEnabled<RQRequestClass.expenseAcctID>(sender, (object) row, flag3);
    PXUIFieldAttribute.SetEnabled<RQRequestClass.expenseSubID>(sender, (object) row, flag3);
    PXCache pxCache1 = sender;
    RQRequestClass rqRequestClass1 = row;
    bool? nullable = row.RestrictItemList;
    int num5 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestClass.hideInventoryID>(pxCache1, (object) rqRequestClass1, num5 != 0);
    PXCache pxCache2 = sender;
    RQRequestClass rqRequestClass2 = row;
    nullable = row.VendorNotRequest;
    int num6 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestClass.vendorMultiply>(pxCache2, (object) rqRequestClass2, num6 != 0);
    PXCache pxCache3 = sender;
    RQRequestClass rqRequestClass3 = row;
    nullable = row.CustomerRequest;
    int num7 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestClass.budgetValidation>(pxCache3, (object) rqRequestClass3, num7 != 0);
    RQRequest rqRequest = PXResultset<RQRequest>.op_Implicit(PXSelectBase<RQRequest, PXSelect<RQRequest, Where<RQRequest.reqClassID, Equal<Required<RQRequest.reqClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ReqClassID
    }));
    PXUIFieldAttribute.SetEnabled<RQRequestClass.customerRequest>(sender, (object) row, rqRequest == null);
  }

  protected virtual void RQRequestClass_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQRequestClass newRow = (RQRequestClass) e.NewRow;
    bool? nullable;
    if (newRow != null)
    {
      nullable = newRow.CustomerRequest;
      if (nullable.GetValueOrDefault())
      {
        newRow.BudgetValidation = new int?(0);
        if (newRow.ExpenseAccountDefault == "D")
          sender.RaiseExceptionHandling<RQRequestClass.expenseAccountDefault>((object) newRow, (object) newRow.ExpenseAccountDefault, (Exception) new PXSetPropertyException("Unable to default expense account by department for customer request class."));
        if (newRow.ExpenseSubMask != null && newRow.ExpenseAccountDefault != "N" && newRow.ExpenseSubMask.Contains("D"))
          sender.RaiseExceptionHandling<RQRequestClass.expenseSubMask>((object) newRow, (object) newRow.ExpenseSubMask, (Exception) new PXSetPropertyException("Unable to combine expense subaccount by department for customer request class."));
      }
    }
    if (newRow == null)
      return;
    nullable = newRow.HideInventoryID;
    if (!nullable.GetValueOrDefault())
      return;
    if (newRow.ExpenseAccountDefault == "I")
      sender.RaiseExceptionHandling<RQRequestClass.expenseAccountDefault>((object) newRow, (object) newRow.ExpenseAccountDefault, (Exception) new PXSetPropertyException("Unable to default expense account by purchase item when inventory item is hidden."));
    if (newRow.ExpenseSubMask == null || !(newRow.ExpenseAccountDefault != "N") || !newRow.ExpenseSubMask.Contains("I"))
      return;
    sender.RaiseExceptionHandling<RQRequestClass.expenseSubMask>((object) newRow, (object) newRow.ExpenseSubMask, (Exception) new PXSetPropertyException("Unable to combine expense subaccount by purchase item when inventory item is hidden."));
  }

  protected virtual void RQRequestClass_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RQRequestClass row = (RQRequestClass) e.Row;
    if (row == null)
      return;
    bool? nullable = row.VendorNotRequest;
    if (nullable.GetValueOrDefault())
      row.VendorMultiply = new bool?(false);
    nullable = row.RestrictItemList;
    if (!nullable.GetValueOrDefault())
      return;
    row.HideInventoryID = new bool?(false);
  }

  protected virtual void RQRequestClass_RestrictItemList_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequestClass row = (RQRequestClass) e.Row;
  }

  protected virtual void RQRequestClass_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    RQRequestClass row = (RQRequestClass) e.Row;
    PXDefaultAttribute.SetPersistingCheck<RQRequestClass.expenseAcctID>(sender, (object) row, row.ExpenseAccountDefault == "Q" || row.ExpenseAccountDefault == "I" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<RQRequestClass.expenseSubID>(sender, (object) row, row.ExpenseAccountDefault == "Q" || row.ExpenseAccountDefault == "I" || row.ExpenseSubMask != null && (row.ExpenseSubMask.Contains("Q") || row.ExpenseSubMask.Contains("I")) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (e.Operation != 2 && e.Operation != 1 || !row.RestrictItemList.GetValueOrDefault())
      return;
    RQRequestClassItem requestClassItem = PXResultset<RQRequestClassItem>.op_Implicit(((PXSelectBase<RQRequestClassItem>) this.ClassItems).SelectWindowed(0, 1, new object[1]
    {
      (object) row.ReqClassID
    }));
    if (requestClassItem == null)
      throw new PXRowPersistedException(typeof (RQRequestClass).Name, (object) requestClassItem, "Item list should be defined when restriction is set.");
  }

  protected virtual void RQRequestClass_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    RQRequestClass row = (RQRequestClass) e.Row;
    if (row == null)
      return;
    if (PXResultset<RQSetup>.op_Implicit(PXSelectBase<RQSetup, PXSelect<RQSetup, Where<RQSetup.defaultReqClassID, Equal<Required<RQSetup.defaultReqClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ReqClassID
    })) != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXRowPersistingException(sender.GetItemType().Name, (object) row, "Request class is used in setup. It can't be deleted.");
    }
    if (PXResultset<RQRequest>.op_Implicit(PXSelectBase<RQRequest, PXSelect<RQRequest, Where<RQRequest.reqClassID, Equal<Required<RQRequest.reqClassID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.ReqClassID
    })) != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXRowPersistingException(sender.GetItemType().Name, (object) row, "Request class is used in request items. It can't be deleted.");
    }
  }

  protected virtual void RQRequestClassItem_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    GraphHelper.MarkUpdated(((PXSelectBase) this.Classes).Cache, ((PXSelectBase) this.Classes).Cache.Current, true);
    RQRequestClassItem row = (RQRequestClassItem) e.Row;
    if (row == null)
      return;
    ((CancelEventArgs) e).Cancel = !this.ValidateDuplicates(sender, row, (RQRequestClassItem) null);
  }

  protected virtual void RQRequestClassItem_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    GraphHelper.MarkUpdated(((PXSelectBase) this.Classes).Cache, ((PXSelectBase) this.Classes).Cache.Current, true);
    RQRequestClassItem row = (RQRequestClassItem) e.Row;
    RQRequestClassItem newRow = (RQRequestClassItem) e.NewRow;
    if (row == null || newRow == null || row == newRow)
      return;
    if (!(row.ReqClassID != newRow.ReqClassID))
    {
      int? inventoryId1 = row.InventoryID;
      int? inventoryId2 = newRow.InventoryID;
      if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        return;
    }
    ((CancelEventArgs) e).Cancel = !this.ValidateDuplicates(sender, newRow, row);
  }

  protected virtual void RQRequestClassItem_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    GraphHelper.MarkUpdated(((PXSelectBase) this.Classes).Cache, ((PXSelectBase) this.Classes).Cache.Current, true);
  }

  private bool ValidateDuplicates(
    PXCache sender,
    RQRequestClassItem row,
    RQRequestClassItem oldRow)
  {
    if (row.InventoryID.HasValue)
    {
      foreach (PXResult<RQRequestClassItem> pxResult in ((PXSelectBase<RQRequestClassItem>) this.ClassItems).Select(new object[1]
      {
        (object) row.ReqClassID
      }))
      {
        RQRequestClassItem requestClassItem = PXResult<RQRequestClassItem>.op_Implicit(pxResult);
        if (string.Equals(requestClassItem.ReqClassID, row.ReqClassID, StringComparison.OrdinalIgnoreCase))
        {
          int? inventoryId1 = requestClassItem.InventoryID;
          int? inventoryId2 = row.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? lineId1 = row.LineID;
            int? lineId2 = requestClassItem.LineID;
            if (!(lineId1.GetValueOrDefault() == lineId2.GetValueOrDefault() & lineId1.HasValue == lineId2.HasValue))
            {
              if (oldRow == null || oldRow.ReqClassID != row.ReqClassID)
                sender.RaiseExceptionHandling<RQRequestClassItem.reqClassID>((object) row, (object) row.ReqClassID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
              if (oldRow != null)
              {
                int? inventoryId3 = oldRow.InventoryID;
                int? inventoryId4 = row.InventoryID;
                if (inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue)
                  goto label_11;
              }
              sender.RaiseExceptionHandling<RQRequestClassItem.inventoryID>((object) row, (object) row.InventoryID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
label_11:
              return false;
            }
          }
        }
      }
    }
    PXUIFieldAttribute.SetError<RQRequestClassItem.reqClassID>(sender, (object) row, (string) null);
    PXUIFieldAttribute.SetError<RQRequestClassItem.inventoryID>(sender, (object) row, (string) null);
    return true;
  }
}
