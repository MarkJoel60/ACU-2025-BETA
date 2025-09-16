// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EquipmentSetupMaint
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

public class EquipmentSetupMaint : PXGraph<EquipmentSetupMaint>
{
  public PXSave<FSEquipmentSetup> Save;
  public PXCancel<FSEquipmentSetup> Cancel;
  public PXSelect<FSEquipmentSetup> EquipmentSetupRecord;
  public CRNotificationSetupList<FSCTNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<FSCTNotification.setupID>>>> Recipients;

  public EquipmentSetupMaint()
  {
    if (PXResultset<FSEquipmentSetup>.op_Implicit(PXSelectBase<FSEquipmentSetup, PXSelectReadonly<FSEquipmentSetup>.Config>.Select((PXGraph) this, Array.Empty<object>())) == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (FSEquipmentSetup), new object[1]
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

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSEquipmentSetup, FSSetup.contractPostTo> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentSetup row = e.Row;
    if (!((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSEquipmentSetup, FSSetup.contractPostTo>, FSEquipmentSetup, object>) e).NewValue == "SI"))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSEquipmentSetup, FSSetup.contractPostTo>>) e).Cache.RaiseExceptionHandling<FSSetup.contractPostTo>((object) row, (object) null, (Exception) new PXSetPropertyException("The ability to generate SO invoices for service contracts has not been implemented yet. Please select another option.", (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentSetup, FSSetup.contractPostTo> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentSetup row = e.Row;
    if (!(row.ContractPostTo != (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSEquipmentSetup, FSSetup.contractPostTo>, FSEquipmentSetup, object>) e).OldValue))
      return;
    SharedFunctions.ValidatePostToByFeatures<FSSetup.contractPostTo>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSEquipmentSetup, FSSetup.contractPostTo>>) e).Cache, (object) row, row.ContractPostTo);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSEquipmentSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSEquipmentSetup> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentSetup row = e.Row;
    EquipmentSetupMaint.EnableDisable_Document(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSEquipmentSetup>>) e).Cache, (FSSetup) row);
    FSPostTo.SetLineTypeList<FSSetup.contractPostTo>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSEquipmentSetup>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSEquipmentSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSEquipmentSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSEquipmentSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSEquipmentSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSEquipmentSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSEquipmentSetup> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSEquipmentSetup> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentSetup row = e.Row;
    if (row.ContractPostTo == "SI")
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSEquipmentSetup>>) e).Cache.RaiseExceptionHandling<FSSetup.contractPostTo>((object) row, (object) null, (Exception) new PXSetPropertyException("The ability to generate SO invoices for service contracts has not been implemented yet. Please select another option.", (PXErrorLevel) 4));
    SharedFunctions.ValidatePostToByFeatures<FSSetup.contractPostTo>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSEquipmentSetup>>) e).Cache, (object) row, row.ContractPostTo);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSEquipmentSetup> e)
  {
  }

  public static void EnableDisable_Document(PXCache cache, FSSetup fsEquipmentSetupRow)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
    PXUIFieldAttribute.SetVisible<FSSetup.contractPostOrderType>(cache, (object) fsEquipmentSetupRow, flag && fsEquipmentSetupRow.ContractPostTo == "SO");
    if (fsEquipmentSetupRow.ContractPostTo == "SO")
      PXDefaultAttribute.SetPersistingCheck<FSSetup.contractPostOrderType>(cache, (object) fsEquipmentSetupRow, (PXPersistingCheck) 1);
    else
      PXDefaultAttribute.SetPersistingCheck<FSSetup.contractPostOrderType>(cache, (object) fsEquipmentSetupRow, (PXPersistingCheck) 2);
  }
}
