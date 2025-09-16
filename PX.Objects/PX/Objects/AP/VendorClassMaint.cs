// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Descriptor;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP.Standalone;
using PX.Objects.PM;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AP;

public class VendorClassMaint : PXGraph<VendorClassMaint>
{
  public PXSave<VendorClass> Save;
  public PXAction<VendorClass> cancel;
  public PXInsert<VendorClass> Insert;
  public PXCopyPasteAction<VendorClass> Edit;
  public PXDelete<VendorClass> Delete;
  public PXFirst<VendorClass> First;
  public PXPrevious<VendorClass> Prev;
  public PXNext<VendorClass> Next;
  public PXLast<VendorClass> Last;
  public PXSelectJoin<VendorClass, LeftJoin<EPEmployeeClass, On<EPEmployeeClass.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<EPEmployeeClass.vendorClassID, IsNull>> VendorClassRecord;
  public PXSelect<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<VendorClass.vendorClassID>>>> CurVendorClassRecord;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<VendorClass, Vendor> Mapping;
  public CRClassNotificationSourceList<VendorClass.vendorClassID, APNotificationSource.vendor> NotificationSources;
  public PXSelect<NotificationRecipient, Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Optional<NotificationSource.sourceID>>>>> NotificationRecipients;
  public PXSelect<Vendor, Where<Vendor.vendorClassID, Equal<Current<VendorClass.vendorClassID>>>> Vendors;
  public PXAction<VendorClass> resetGroup;
  public PXMenuAction<VendorClass> ActionsMenu;
  public PXSelect<Neighbour> Neighbours;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  [PXCancelButton]
  [PXUIField(MapEnableRights = PXCacheRights.Select)]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    VendorClassMaint graph = this;
    foreach (PXResult<VendorClass, EPEmployeeClass> row in new PXCancel<VendorClass>((PXGraph) graph, nameof (Cancel)).Press(a))
    {
      if (graph.VendorClassRecord.Cache.GetStatus((object) (VendorClass) row) == PXEntryStatus.Inserted)
      {
        if ((EPEmployeeClass) PXSelectBase<EPEmployeeClass, PXSelect<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Required<EPEmployeeClass.vendorClassID>>>>.Config>.Select((PXGraph) graph, (object) ((VendorClass) row).VendorClassID) != null)
          graph.VendorClassRecord.Cache.RaiseExceptionHandling<VendorClass.vendorClassID>((object) (VendorClass) row, (object) ((VendorClass) row).VendorClassID, (Exception) new PXSetPropertyException("This ID is already used for the Employee Class."));
      }
      yield return (object) row;
    }
  }

  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<APNotificationSource.vendor>>>), DescriptionField = typeof (NotificationSetup.notificationCD), SelectorMode = PXSelectorMode.NoAutocomplete | PXSelectorMode.DisplayModeText)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (VendorClass.vendorClassID))]
  [PXParent(typeof (Select2<VendorClass, InnerJoin<NotificationSetup, On<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>, Where<VendorClass.vendorClassID, Equal<Current<NotificationSource.classID>>>>))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void NotificationSource_ClassID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search<PX.SM.SiteMap.screenID, Where2<Where<PX.SM.SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<PX.SM.SiteMap.url, Like<urlReportsInNewUi>>>, And<Where<PX.SM.SiteMap.screenID, Like<PXModule.ap_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.po_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.sc_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.cl_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.rq_>>>>>>>>, OrderBy<Asc<PX.SM.SiteMap.screenID>>>), new System.Type[] {typeof (PX.SM.SiteMap.screenID), typeof (PX.SM.SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (PX.SM.SiteMap.title))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [VendorContactType.ClassList]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>>>))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXProcessButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Include Vendors in Restriction Group")]
  protected virtual IEnumerable ResetGroup(PXAdapter adapter)
  {
    if (this.VendorClassRecord.Ask("Warning", "All vendors of the class will be included in the group specified in the Default Restriction Group box and excluded from the group to which they currently belong. Do you want to proceed?", MessageButtons.OKCancel) == WebDialogResult.OK)
    {
      this.Save.Press();
      string classID = this.VendorClassRecord.Current.VendorClassID;
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => VendorClassMaint.Reset(classID)));
    }
    return adapter.Get();
  }

  protected static void Reset(string classID)
  {
    VendorClassMaint instance = PXGraph.CreateInstance<VendorClassMaint>();
    instance.VendorClassRecord.Current = (VendorClass) instance.VendorClassRecord.Search<VendorClass.vendorClassID>((object) classID);
    if (instance.VendorClassRecord.Current == null)
      return;
    foreach (PXResult<Vendor> pxResult in instance.Vendors.Select())
    {
      Vendor vendor = (Vendor) pxResult;
      vendor.GroupMask = instance.VendorClassRecord.Current.GroupMask;
      instance.Vendors.Cache.SetStatus((object) vendor, PXEntryStatus.Updated);
    }
    instance.Save.Press();
  }

  public override void Persist()
  {
    if (this.VendorClassRecord.Current != null)
    {
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Users), typeof (Users));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Vendor), typeof (Vendor));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (VendorClass), typeof (VendorClass));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Users), typeof (Vendor));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Vendor), typeof (Users));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Users), typeof (VendorClass));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (VendorClass), typeof (Users));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (VendorClass), typeof (Vendor));
      SingleGroupAttribute.PopulateNeighbours<VendorClass.groupMask>((PXSelectBase) this.VendorClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Vendor), typeof (VendorClass));
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.BAccountRestrictionHelper.Persist();
      base.Persist();
      GroupHelper.Clear();
      transactionScope.Complete();
    }
  }

  public VendorClassMaint()
  {
    PX.Objects.GL.GLSetup current = this.GLSetup.Current;
    PXUIFieldAttribute.SetVisible<VendorClass.localeName>(this.VendorClassRecord.Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
  }

  public virtual void VendorClass_CashAcctID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
  }

  public virtual void VendorClass_CashAcctID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    VendorClass row = (VendorClass) e.Row;
    if (row == null)
      return;
    VendorClass vendorClass = (VendorClass) PXSelectBase<VendorClass, PXSelectJoin<VendorClass, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>.Config>.Select((PXGraph) this);
    if (vendorClass != null && row.VendorClassID != vendorClass.VendorClassID && row.PaymentMethodID == vendorClass.PaymentMethodID)
    {
      e.NewValue = (object) vendorClass.CashAcctID;
      e.Cancel = true;
    }
    else
    {
      e.NewValue = (object) null;
      e.Cancel = true;
    }
  }

  public virtual void VendorClass_PaymentMethodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<VendorClass.cashAcctID>(e.Row);
  }

  protected virtual void VendorClass_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool isVisible = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<VendorClass.curyID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorClass.curyRateTypeID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorClass.allowOverrideCury>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorClass.allowOverrideRate>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorClass.unrealizedGainAcctID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorClass.unrealizedGainSubID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorClass.unrealizedLossAcctID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorClass.unrealizedLossSubID>(cache, (object) null, isVisible);
    if (e.Row == null)
      return;
    VendorClass row = (VendorClass) e.Row;
    PXUIFieldAttribute.SetEnabled<VendorClass.cashAcctID>(cache, e.Row, !string.IsNullOrEmpty(row.PaymentMethodID));
  }

  public virtual void VendorClass_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    VendorClass row = (VendorClass) e.Row;
    if (row == null)
      return;
    APSetup apSetup = (APSetup) PXSelectBase<APSetup, PXSelect<APSetup>.Config>.Select((PXGraph) this);
    if (apSetup != null && row.VendorClassID == apSetup.DfltVendorClassID)
      throw new PXException("This Vendor Class can not be deleted because it is used in Accounts Payable Preferences.");
  }

  protected virtual void VendorClass_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void VendorClass_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    e.Cancel = true;
  }

  protected virtual void VendorClass_CuryRateTypeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    e.Cancel = true;
  }

  public virtual void VendorClass_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    VendorClass row = (VendorClass) e.Row;
    PXSelectBase<PX.Objects.CA.CashAccount> pxSelectBase = (PXSelectBase<PX.Objects.CA.CashAccount>) new PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<VendorClass.cashAcctID>>>>((PXGraph) this);
    if (!string.IsNullOrEmpty(row.CuryID) && !row.AllowOverrideCury.GetValueOrDefault())
    {
      PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) pxSelectBase.Select((object) row.CashAcctID);
      if (cashAccount != null && row.CuryID != cashAccount.CuryID)
      {
        if (cache.RaiseExceptionHandling<VendorClass.cashAcctID>(e.Row, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException("Vendor currency is different from the default Cash Account Currency.", new object[1]
        {
          (object) "[cashAcctID]"
        })))
          throw new PXRowPersistingException(typeof (VendorClass.cashAcctID).Name, (object) null, "Vendor currency is different from the default Cash Account Currency.", new object[1]
          {
            (object) "cashAcctID"
          });
      }
    }
    if (this.VendorClassRecord.Cache.GetStatus(e.Row) != PXEntryStatus.Inserted)
      return;
    if ((EPEmployeeClass) PXSelectBase<EPEmployeeClass, PXSelect<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Current<VendorClass.vendorClassID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }) != null)
    {
      cache.IsDirty = false;
      e.Cancel = true;
      throw new PXRowPersistingException(typeof (VendorClass.vendorClassID).Name, (object) null, "This ID is already used for the Employee Class.");
    }
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Project Currency")]
  public void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }
}
