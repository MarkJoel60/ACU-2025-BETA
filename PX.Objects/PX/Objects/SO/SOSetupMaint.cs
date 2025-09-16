// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.ML.CrossSales.DAC;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class SOSetupMaint : PXGraph<SOSetupMaint>
{
  public PXSave<SOSetup> Save;
  public PXCancel<SOSetup> Cancel;
  public PXSelect<SOSetup> sosetup;

  public class ApprovalsExtension : PXGraphExtension<SOSetupMaint>
  {
    public PXSelect<SOSetupApproval> SetupApproval;
    public PXSelect<SOSetupInvoiceApproval> SetupInvoiceApproval;

    protected virtual void _(PX.Data.Events.RowInserted<SOSetup> e)
    {
      SOSetup row = e.Row;
      if ((row != null ? (row.OrderRequestApproval.HasValue ? 1 : 0) : 0) == 0)
        return;
      this.SyncOrderApprovalsWithOrderRequestApprovalFlag(e.Row.OrderRequestApproval);
    }

    protected virtual void _(PX.Data.Events.RowUpdated<SOSetup> e)
    {
      if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOSetup>>) e).Cache.ObjectsEqual<SOSetup.orderRequestApproval>((object) e.Row, (object) e.OldRow))
        return;
      this.SyncOrderApprovalsWithOrderRequestApprovalFlag(e.Row.OrderRequestApproval);
    }

    protected virtual void _(PX.Data.Events.RowPersisting<SOSetup> e)
    {
      PXPersistingCheck pxPersistingCheck = PXAccess.FeatureInstalled<FeaturesSet.interBranch>() ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2;
      PXDefaultAttribute.SetPersistingCheck<SOSetup.dfltIntercompanyOrderType>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOSetup>>) e).Cache, (object) e.Row, pxPersistingCheck);
      PXDefaultAttribute.SetPersistingCheck<SOSetup.dfltIntercompanyRMAType>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOSetup>>) e).Cache, (object) e.Row, pxPersistingCheck);
    }

    protected virtual void _(PX.Data.Events.RowInserted<SOSetupApproval> e)
    {
      this.SyncOrderRequestApprovalFlagWithOrderApprovals(e.Row);
    }

    protected virtual void _(PX.Data.Events.RowUpdated<SOSetupApproval> e)
    {
      this.SyncOrderRequestApprovalFlagWithOrderApprovals(e.Row);
    }

    private void SyncOrderApprovalsWithOrderRequestApprovalFlag(bool? newOrderRequestApproval)
    {
      foreach (PXResult<SOSetupApproval> pxResult in PXSelectBase<SOSetupApproval, PXSelect<SOSetupApproval>.Config>.Select((PXGraph) this.Base, (object[]) null))
      {
        SOSetupApproval soSetupApproval = PXResult<SOSetupApproval>.op_Implicit(pxResult);
        soSetupApproval.IsActive = newOrderRequestApproval;
        ((PXSelectBase<SOSetupApproval>) this.SetupApproval).Update(soSetupApproval);
      }
    }

    private void SyncOrderRequestApprovalFlagWithOrderApprovals(SOSetupApproval approval)
    {
      PXSelect<SOSetup> sosetup = this.Base.sosetup;
      if (!approval.IsActive.GetValueOrDefault() || ((PXSelectBase<SOSetup>) sosetup).Current.OrderRequestApproval.GetValueOrDefault())
        return;
      ((PXSelectBase<SOSetup>) sosetup).Current.OrderRequestApproval = new bool?(true);
      ((PXSelectBase<SOSetup>) sosetup).UpdateCurrent();
    }
  }

  public class NotificationSetupExtension : PXGraphExtension<SOSetupMaint>
  {
    public CRNotificationSetupList<SONotification> Notifications;
    public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<SONotification.setupID>>>> Recipients;

    [PXDBString(10)]
    [PXDefault]
    [VendorContactType.ClassList]
    [PXUIField(DisplayName = "Contact Type")]
    [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
    protected virtual void _(
      PX.Data.Events.CacheAttached<NotificationSetupRecipient.contactType> e)
    {
    }

    [PXDBInt]
    [PXUIField(DisplayName = "Contact ID")]
    [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>>))]
    protected virtual void _(
      PX.Data.Events.CacheAttached<NotificationSetupRecipient.contactID> e)
    {
    }
  }

  public class PickPackShipExtension : PXGraphExtension<SOSetupMaint>
  {
    public PXSelect<SOPickPackShipSetup, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>> PickPackShipSetup;
    public PXSelect<SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.isOverridden, Equal<False>>> PickPackShipUserSetups;

    protected virtual IEnumerable pickPackShipSetup()
    {
      return (IEnumerable) this.EnsurePickPackShipSetup();
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showPickTab> e)
    {
      if ((bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showPickTab>, SOPickPackShipSetup, object>) e).NewValue)
        return;
      SOPickPackShipSetup current1 = ((PXSelectBase<SOPickPackShipSetup>) this.PickPackShipSetup).Current;
      if ((current1 != null ? (!current1.ShowPackTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      SOPickPackShipSetup current2 = ((PXSelectBase<SOPickPackShipSetup>) this.PickPackShipSetup).Current;
      if ((current2 != null ? (!current2.ShowShipTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showPickTab>, SOPickPackShipSetup, object>) e).NewValue = (object) true;
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showPackTab> e)
    {
      if ((bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showPackTab>, SOPickPackShipSetup, object>) e).NewValue)
        return;
      SOPickPackShipSetup current1 = ((PXSelectBase<SOPickPackShipSetup>) this.PickPackShipSetup).Current;
      if ((current1 != null ? (!current1.ShowPickTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      SOPickPackShipSetup current2 = ((PXSelectBase<SOPickPackShipSetup>) this.PickPackShipSetup).Current;
      if ((current2 != null ? (!current2.ShowShipTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showPackTab>, SOPickPackShipSetup, object>) e).NewValue = (object) true;
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showShipTab> e)
    {
      if ((bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showShipTab>, SOPickPackShipSetup, object>) e).NewValue)
        return;
      SOPickPackShipSetup current1 = ((PXSelectBase<SOPickPackShipSetup>) this.PickPackShipSetup).Current;
      if ((current1 != null ? (!current1.ShowPickTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      SOPickPackShipSetup current2 = ((PXSelectBase<SOPickPackShipSetup>) this.PickPackShipSetup).Current;
      if ((current2 != null ? (!current2.ShowPackTab.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOPickPackShipSetup, SOPickPackShipSetup.showShipTab>, SOPickPackShipSetup, object>) e).NewValue = (object) true;
    }

    protected virtual void _(PX.Data.Events.RowUpdated<SOPickPackShipSetup> e)
    {
      if (e.Row == null)
        return;
      foreach (PXResult<SOPickPackShipUserSetup> pxResult in ((PXSelectBase<SOPickPackShipUserSetup>) this.PickPackShipUserSetups).Select(Array.Empty<object>()))
        ((PXSelectBase<SOPickPackShipUserSetup>) this.PickPackShipUserSetups).Update(PXResult<SOPickPackShipUserSetup>.op_Implicit(pxResult).ApplyValuesFrom(e.Row));
    }

    protected virtual void _(PX.Data.Events.RowSelected<SOPickPackShipSetup> e)
    {
      if (e.Row == null)
        return;
      bool? showPickTab = e.Row.ShowPickTab;
      bool flag = false;
      if (!(showPickTab.GetValueOrDefault() == flag & showPickTab.HasValue) || !PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>())
        return;
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOPickPackShipSetup>>) e).Cache.RaiseExceptionHandling<SOPickPackShipSetup.showPickTab>((object) e.Row, (object) e.Row.ShowPickTab, (Exception) new PXSetPropertyException("Wave and batch pick lists cannot be processed if the Display the Pick Tab check box is cleared.", (PXErrorLevel) 2));
    }

    private IEnumerable<SOPickPackShipSetup> EnsurePickPackShipSetup()
    {
      return (IEnumerable<SOPickPackShipSetup>) new SOPickPackShipSetup[1]
      {
        PXResultset<SOPickPackShipSetup>.op_Implicit(((PXSelectBase<SOPickPackShipSetup>) new PXSelect<SOPickPackShipSetup, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>>((PXGraph) this.Base)).Select(Array.Empty<object>())) ?? ((PXSelectBase<SOPickPackShipSetup>) this.PickPackShipSetup).Insert()
      };
    }
  }

  public class MachineLearningExtension : PXGraphExtension<SOSetupMaint>
  {
    public PXSelectJoin<SOSetupCrossSellExcludedItemClasses, InnerJoin<INItemClass, On<INItemClass.itemClassID, Equal<SOSetupCrossSellExcludedItemClasses.itemClassID>>>> MLExcludedItemClasses;
    public PXSelect<MLCrossSalesSetup> mlcrosssalessetup;

    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.relatedItemAssistant>();

    protected virtual IEnumerable Mlcrosssalessetup()
    {
      return (IEnumerable) this.EnsureMLCrossSalesSetup();
    }

    private IEnumerable<MLCrossSalesSetup> EnsureMLCrossSalesSetup()
    {
      using (new ReadOnlyScope(new PXCache[1]
      {
        ((PXSelectBase) this.mlcrosssalessetup).Cache
      }))
        return (IEnumerable<MLCrossSalesSetup>) new MLCrossSalesSetup[1]
        {
          PXResultset<MLCrossSalesSetup>.op_Implicit(((PXSelectBase<MLCrossSalesSetup>) new PXSelect<MLCrossSalesSetup>((PXGraph) this.Base)).Select(Array.Empty<object>())) ?? ((PXSelectBase<MLCrossSalesSetup>) this.mlcrosssalessetup).Insert()
        };
    }
  }
}
