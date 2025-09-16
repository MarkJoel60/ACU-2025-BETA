// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteSetupMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.FS;

public class RouteSetupMaint : PXGraph<RouteSetupMaint>
{
  [PXHidden]
  public PXSelect<FSSetup> SetupRecord;
  public PXSelect<FSRouteSetup> RouteSetupRecord;
  public PXSave<FSRouteSetup> Save;
  public PXCancel<FSRouteSetup> Cancel;
  public CRNotificationSetupList<FSCTNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<FSCTNotification.setupID>>>> Recipients;

  public RouteSetupMaint()
  {
    if (PXResultset<FSSetup>.op_Implicit(PXSelectBase<FSSetup, PXSelectReadonly<FSSetup>.Config>.Select((PXGraph) this, Array.Empty<object>())) == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (FSSetup), new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSetup))
      });
  }

  [PXDBString(10)]
  [PXDefault]
  [ContractContactType.ClassList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckUnique(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  public virtual void NotificationSetupRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>>))]
  public virtual void NotificationSetupRecipient_ContactID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSRouteSetup> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSetup>) this.SetupRecord).Current != null)
      return;
    ((PXSelectBase<FSSetup>) this.SetupRecord).Current = PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>()));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSRouteSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRouteSetup> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSetup, FSSetup.contractPostTo> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    if (!(row.ContractPostTo != (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSetup, FSSetup.contractPostTo>, FSSetup, object>) e).OldValue))
      return;
    SharedFunctions.ValidatePostToByFeatures<FSSetup.contractPostTo>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSetup, FSSetup.contractPostTo>>) e).Cache, (object) row, row.ContractPostTo);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSetup> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSetup>>) e).Cache;
    EquipmentSetupMaint.EnableDisable_Document(cache, row);
    SharedFunctions.ValidatePostToByFeatures<FSSetup.contractPostTo>(cache, (object) row, row.ContractPostTo);
    FSPostTo.SetLineTypeList<FSSetup.contractPostTo>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSetup>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSetup> e)
  {
    if (e.Row == null)
      return;
    FSSetup row = e.Row;
    if (row.ContractPostTo == "SI")
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSSetup>>) e).Cache.RaiseExceptionHandling<FSSetup.contractPostTo>((object) row, (object) null, (Exception) new PXSetPropertyException("The ability to generate SO invoices for service contracts has not been implemented yet. Please select another option.", (PXErrorLevel) 4));
    SharedFunctions.ValidatePostToByFeatures<FSSetup.contractPostTo>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSSetup>>) e).Cache, (object) row, row.ContractPostTo);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSetup> e)
  {
  }
}
