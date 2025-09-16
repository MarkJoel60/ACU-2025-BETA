// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.EMailSyncPolicyMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CS;

public class EMailSyncPolicyMaintExt : PXGraphExtension<EMailSyncPolicyMaint>
{
  public PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Current<EMailSyncPolicy.contactsClass>>>> ContactClass;

  public virtual void Initialize()
  {
    ((PXSelectBase) this.Base.Preferences).Cache.AllowSelect = true;
  }

  protected virtual void EMailSyncAccountPreferences_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EMailSyncAccountPreferences row) || !row.EmployeeID.HasValue)
      return;
    foreach (PXResult<EPEmployee, PX.Objects.CR.Contact> pxResult in PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, InnerJoin<PX.Objects.CR.Contact, On<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>>, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.EmployeeID
    }))
    {
      row.EmployeeCD = PXResult<EPEmployee, PX.Objects.CR.Contact>.op_Implicit(pxResult).AcctName;
      row.Address = PXResult<EPEmployee, PX.Objects.CR.Contact>.op_Implicit(pxResult).EMail;
    }
  }

  protected virtual void EMailSyncPolicy_ContactsClass_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e,
    PXFieldUpdated sel)
  {
    sel?.Invoke(cache, e);
    EMailSyncPolicy row = e.Row as EMailSyncPolicy;
    CRContactClass crContactClass = ((PXSelectBase<CRContactClass>) this.ContactClass).SelectSingle(Array.Empty<object>());
    if (crContactClass == null || string.IsNullOrWhiteSpace(crContactClass.DefaultOwner))
      return;
    cache.RaiseExceptionHandling<EMailSyncPolicy.contactsClass>((object) row, (object) row.ContactsClass, (Exception) new PXSetPropertyException("The selected class does not allow creating contacts with the specified filter. You can adjust the class settings, select a different class, or leave the box blank.", (PXErrorLevel) 2));
  }

  protected virtual void EMailSyncPolicy_ContactsFilter_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e,
    PXFieldUpdated sel)
  {
    sel?.Invoke(cache, e);
    EMailSyncPolicy row = e.Row as EMailSyncPolicy;
    cache.RaiseFieldUpdated<EMailSyncPolicy.contactsClass>((object) row, (object) row.ContactsClass);
  }
}
