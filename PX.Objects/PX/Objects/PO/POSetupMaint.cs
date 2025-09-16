// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PO;

public class POSetupMaint : PXGraph<POSetupMaint>
{
  public PXSave<POSetup> Save;
  public PXCancel<POSetup> Cancel;
  public PXSelect<POSetup> Setup;
  public PXSelect<POSetupApproval> SetupApproval;
  public CRNotificationSetupList<PONotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<PONotification.setupID>>>> Recipients;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetupOptional<INSetup> insetup;
  public CMSetupSelect CMSetup;
  public PXSelect<POReceivePutAwaySetup, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>> ReceivePutAwaySetup;
  public PXSelect<POReceivePutAwayUserSetup, Where<POReceivePutAwayUserSetup.isOverridden, Equal<False>>> ReceivePutAwayUserSetups;

  [PXDBString(10)]
  [PXDefault]
  [VendorContactType.ClassList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  protected virtual void NotificationSetupRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>>))]
  protected virtual void NotificationSetupRecipient_ContactID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    POSetup row = (POSetup) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<POSetup.copyLineNoteSO>(sender, (object) row, row.CopyLineDescrSO.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<POSetup.pPVReasonCodeID>(sender, (object) row, row.PPVAllocationMode == "I");
    PXUIFieldAttribute.SetRequired<POSetup.pPVReasonCodeID>(sender, row.PPVAllocationMode == "I");
  }

  protected virtual void POSetup_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    POSetup row = (POSetup) e.Row;
    if (row == null)
      return;
    PXDefaultAttribute.SetPersistingCheck<POSetup.pPVReasonCodeID>(sender, (object) row, row.PPVAllocationMode == "I" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POSetup.rCReturnReasonCodeID>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.inventory>() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void POSetup_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    POSetup row = (POSetup) e.Row;
    if (row == null)
      return;
    bool? copyLineDescrSo = row.CopyLineDescrSO;
    bool flag = false;
    if (!(copyLineDescrSo.GetValueOrDefault() == flag & copyLineDescrSo.HasValue))
      return;
    row.CopyLineNoteSO = new bool?(false);
  }

  protected virtual void POSetup_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    POSetup row = (POSetup) e.Row;
    if (row != null)
    {
      bool? copyLineDescrSo = row.CopyLineDescrSO;
      bool flag = false;
      if (copyLineDescrSo.GetValueOrDefault() == flag & copyLineDescrSo.HasValue)
        row.CopyLineNoteSO = new bool?(false);
    }
    if (sender.ObjectsEqual<POSetup.changeCuryRateOnReceipt>(e.Row, e.OldRow))
      return;
    PXPageCacheUtils.InvalidateCachedPages();
  }

  protected virtual void POSetup_OrderRequestApproval_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (POSetupApproval)];
    foreach (PXResult<POSetupApproval> pxResult in PXSelectBase<POSetupApproval, PXSelect<POSetupApproval>.Config>.Select(sender.Graph, (object[]) null))
    {
      POSetupApproval poSetupApproval = PXResult<POSetupApproval>.op_Implicit(pxResult);
      poSetupApproval.IsActive = (bool?) e.NewValue;
      cach.Update((object) poSetupApproval);
    }
  }

  protected virtual void POSetup_AddServicesFromNormalPOtoPR_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CheckPartiallyReceiptedPOServices(sender, e, "RO");
  }

  protected virtual void POSetup_AddServicesFromDSPOtoPR_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CheckPartiallyReceiptedPOServices(sender, e, "DP");
  }

  public virtual void CheckPartiallyReceiptedPOServices(
    PXCache sender,
    PXFieldUpdatedEventArgs e,
    string poOrderType)
  {
    POSetup row = (POSetup) e.Row;
    if (row == null)
      return;
    PXResultset<POLine> pxResultset = PXSelectBase<POLine, PXSelectReadonly2<POLine, InnerJoin<PX.Objects.IN.InventoryItem, On<POLine.FK.InventoryItem>>, Where<POLine.orderType, Equal<Required<POLine.orderType>>, And<POLine.lineType, Equal<POLineType.service>, And<POLine.completed, NotEqual<True>, And<POLine.receivedQty, NotEqual<decimal0>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1000, new object[1]
    {
      (object) poOrderType
    });
    if (pxResultset.Count <= 0)
      return;
    if (poOrderType == "RO")
    {
      if (row.AddServicesFromNormalPOtoPR.GetValueOrDefault())
        PXUIFieldAttribute.SetWarning<POSetup.addServicesFromNormalPOtoPR>(sender, (object) row, "Changing this setting could lead to overbilling open service lines in the purchase orders for which you have already created AP bills. Please see the list of the affected purchase orders in the trace log.");
      else
        PXUIFieldAttribute.SetWarning<POSetup.addServicesFromNormalPOtoPR>(sender, (object) row, "Changing this setting could lead to overbilling open service lines in the purchase orders for which you have already created purchase receipts. Please see the list of the affected purchase orders in the trace log.");
    }
    else if (row.AddServicesFromDSPOtoPR.GetValueOrDefault())
      PXUIFieldAttribute.SetWarning<POSetup.addServicesFromDSPOtoPR>(sender, (object) row, "Changing this setting could lead to overbilling open service lines in the drop-ship purchase orders for which you have already created AP bills. Please see the list of the affected drop-ship purchase orders in the trace log.");
    else
      PXUIFieldAttribute.SetWarning<POSetup.addServicesFromDSPOtoPR>(sender, (object) row, "Changing this setting could lead to overbilling open service lines in the drop-ship purchase orders for which you have already created purchase receipts. Please see the list of the affected drop-ship purchase orders in the trace log.");
    string str = "List of open service lines that are partially billed or receipted (Top 1000): \n";
    int num = 0;
    foreach (PXResult<POLine, PX.Objects.IN.InventoryItem> pxResult in pxResultset)
    {
      POLine poLine = PXResult<POLine, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<POLine, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      str = $"{str}{$"Order Type: {(poOrderType == "RO" ? (object) "Normal" : (object) "Drop-Ship")}, Order Nbr.: {poLine.OrderNbr}, Line Nbr.: {poLine.LineNbr}, Inventory ID: {inventoryItem.InventoryCD}"}\n";
      ++num;
      if (num >= 1000)
        break;
    }
    PXTrace.WriteWarning(str);
  }

  protected virtual IEnumerable receivePutAwaySetup()
  {
    return (IEnumerable) new POReceivePutAwaySetup[1]
    {
      PXResultset<POReceivePutAwaySetup>.op_Implicit(((PXSelectBase<POReceivePutAwaySetup>) new PXSelect<POReceivePutAwaySetup, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>>((PXGraph) this)).Select(Array.Empty<object>())) ?? ((PXSelectBase<POReceivePutAwaySetup>) this.ReceivePutAwaySetup).Insert()
    };
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showReceivingTab> e)
  {
    if ((bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showReceivingTab>, POReceivePutAwaySetup, object>) e).NewValue)
      return;
    POReceivePutAwaySetup current1 = ((PXSelectBase<POReceivePutAwaySetup>) this.ReceivePutAwaySetup).Current;
    if ((current1 != null ? (!current1.ShowPutAwayTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    POReceivePutAwaySetup current2 = ((PXSelectBase<POReceivePutAwaySetup>) this.ReceivePutAwaySetup).Current;
    if ((current2 != null ? (!current2.ShowReceiveTransferTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showReceivingTab>, POReceivePutAwaySetup, object>) e).NewValue = (object) true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showPutAwayTab> e)
  {
    if ((bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showPutAwayTab>, POReceivePutAwaySetup, object>) e).NewValue)
      return;
    POReceivePutAwaySetup current1 = ((PXSelectBase<POReceivePutAwaySetup>) this.ReceivePutAwaySetup).Current;
    if ((current1 != null ? (!current1.ShowReceivingTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    POReceivePutAwaySetup current2 = ((PXSelectBase<POReceivePutAwaySetup>) this.ReceivePutAwaySetup).Current;
    if ((current2 != null ? (!current2.ShowReceiveTransferTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showPutAwayTab>, POReceivePutAwaySetup, object>) e).NewValue = (object) true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showReceiveTransferTab> e)
  {
    if ((bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showReceiveTransferTab>, POReceivePutAwaySetup, object>) e).NewValue)
      return;
    POReceivePutAwaySetup current1 = ((PXSelectBase<POReceivePutAwaySetup>) this.ReceivePutAwaySetup).Current;
    if ((current1 != null ? (!current1.ShowReceivingTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    POReceivePutAwaySetup current2 = ((PXSelectBase<POReceivePutAwaySetup>) this.ReceivePutAwaySetup).Current;
    if ((current2 != null ? (!current2.ShowPutAwayTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceivePutAwaySetup, POReceivePutAwaySetup.showReceiveTransferTab>, POReceivePutAwaySetup, object>) e).NewValue = (object) true;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POReceivePutAwaySetup> e)
  {
    if (e.Row == null)
      return;
    foreach (PXResult<POReceivePutAwayUserSetup> pxResult in ((PXSelectBase<POReceivePutAwayUserSetup>) this.ReceivePutAwayUserSetups).Select(Array.Empty<object>()))
      ((PXSelectBase<POReceivePutAwayUserSetup>) this.ReceivePutAwayUserSetups).Update(PXResult<POReceivePutAwayUserSetup>.op_Implicit(pxResult).ApplyValuesFrom(e.Row));
  }

  protected virtual void _(PX.Data.Events.RowSelected<POReceivePutAwaySetup> e)
  {
    bool? nullable1;
    if (e.Row != null && e.Row.DefaultLotSerialNumber.GetValueOrDefault())
    {
      nullable1 = e.Row.DefaultExpireDate;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      {
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwaySetup>>) e).Cache.RaiseExceptionHandling<POReceivePutAwaySetup.defaultExpireDate>((object) e.Row, (object) e.Row.DefaultExpireDate, (Exception) new PXException("The {0} check box cannot be cleared if the {1} check box is selected.", new object[2]
        {
          (object) PXUIFieldAttribute.GetDisplayName<POReceivePutAwaySetup.defaultExpireDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwaySetup>>) e).Cache),
          (object) PXUIFieldAttribute.GetDisplayName<POReceivePutAwaySetup.defaultLotSerialNumber>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwaySetup>>) e).Cache)
        }));
        return;
      }
    }
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwaySetup>>) e).Cache;
    POReceivePutAwaySetup row1 = e.Row;
    POReceivePutAwaySetup row2 = e.Row;
    bool? nullable2;
    if (row2 == null)
    {
      nullable1 = new bool?();
      nullable2 = nullable1;
    }
    else
      nullable2 = row2.DefaultExpireDate;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> local = (ValueType) nullable2;
    cache.RaiseExceptionHandling<POReceivePutAwaySetup.defaultExpireDate>((object) row1, (object) local, (Exception) null);
  }
}
