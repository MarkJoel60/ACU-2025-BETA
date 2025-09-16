// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCustomerClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CR.DAC;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public class CRCustomerClassMaint : PXGraph<CRCustomerClassMaint, CRCustomerClass>
{
  [PXViewName("Customer Class")]
  public PXSelect<CRCustomerClass> CustomerClass;
  [PXHidden]
  public PXSelect<CRCustomerClass, Where<CRCustomerClass.cRCustomerClassID, Equal<Current<CRCustomerClass.cRCustomerClassID>>>> CustomerClassCurrent;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<CRCustomerClass, BAccount> Mapping;
  public CRClassNotificationSourceList<CRCustomerClass.cRCustomerClassID, CRNotificationSource.bAccount> NotificationSources;
  public PXSelect<NotificationRecipient, Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Optional<NotificationSource.sourceID>>>>> NotificationRecipients;
  [PXHidden]
  public PXSelect<CRSetup> Setup;

  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<CRNotificationSource.bAccount>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CRCustomerClass.cRCustomerClassID))]
  [PXParent(typeof (Select2<CRCustomerClass, InnerJoin<NotificationSetup, On<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>, Where<CRCustomerClass.cRCustomerClassID, Equal<Current<NotificationSource.classID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ClassID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search<SiteMap.screenID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SiteMap.screenID, Like<PXModule.cr_>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SiteMap.url, Like<PX.Objects.CA.urlReports>>>>>.Or<BqlOperand<SiteMap.url, IBqlString>.IsLike<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [CRMContactType.List]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency")]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCustomerClass.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Attribute")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CSAttributeGroup.attributeID> e)
  {
  }

  protected virtual void CRCustomerClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CRCustomerClass row))
      return;
    ((PXAction) this.Delete).SetEnabled(this.CanDelete(row));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CRCustomerClass, CRCustomerClass.defaultOwner> e)
  {
    CRCustomerClass row = e.Row;
    if (row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CRCustomerClass, CRCustomerClass.defaultOwner>, CRCustomerClass, object>) e).NewValue != null)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CRCustomerClass, CRCustomerClass.defaultOwner>, CRCustomerClass, object>) e).NewValue = (object) (row.DefaultOwner ?? "N");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRCustomerClass, CRCustomerClass.defaultOwner> e)
  {
    CRCustomerClass row = e.Row;
    if (row == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CRCustomerClass, CRCustomerClass.defaultOwner>, CRCustomerClass, object>) e).OldValue)
      return;
    row.DefaultAssignmentMapID = new int?();
  }

  protected virtual void CRCustomerClass_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is CRCustomerClass row))
      return;
    CRSetup crSetup = PXResultset<CRSetup>.op_Implicit(((PXSelectBase<CRSetup>) this.Setup).Select(Array.Empty<object>()));
    if (crSetup == null || !(crSetup.DefaultCustomerClassID == row.CRCustomerClassID))
      return;
    crSetup.DefaultCustomerClassID = (string) null;
    ((PXSelectBase<CRSetup>) this.Setup).Update(crSetup);
  }

  protected virtual void CRCustomerClass_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is CRCustomerClass row && !this.CanDelete(row))
      throw new PXException("This record is referenced and cannot be deleted.");
  }

  private bool CanDelete(CRCustomerClass row)
  {
    if (row != null)
    {
      if (PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.classID, Equal<Required<BAccount.classID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.CRCustomerClassID
      })) != null)
        return false;
    }
    return true;
  }
}
