// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.INReceiptEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.PM;

public class INReceiptEntryExt : ProjectCostCenterSupport<INReceiptEntry>
{
  public PXSetupOptional<PMSetup> Setup;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual bool IsPMVisible
  {
    get
    {
      PMSetup current = ((PXSelectBase<PMSetup>) this.Setup).Current;
      return current != null && current.VisibleInIN.GetValueOrDefault();
    }
  }

  protected override void _(PX.Data.Events.FieldDefaulting<INTran, INTran.projectID> e)
  {
    base._(e);
    INTran row = e.Row;
    if (row == null || !ProjectAttribute.IsPMVisible("IN") || !row.LocationID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
    if (inventoryItem != null && !inventoryItem.StkItem.GetValueOrDefault())
      return;
    foreach (PXResult<INLocation, PMProject> pxResult in PXSelectBase<INLocation, PXSelectReadonly2<INLocation, LeftJoin<PMProject, On<PMProject.contractID, Equal<INLocation.projectID>>>, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.locationID, Equal<Required<INLocation.locationID>>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTran, INTran.projectID>>) e).Cache.Graph, new object[2]
    {
      (object) row.SiteID,
      (object) row.LocationID
    }))
    {
      PMProject pmProject = PXResult<INLocation, PMProject>.op_Implicit(pxResult);
      if (pmProject != null && pmProject.ContractCD != null && pmProject.VisibleInIN.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.projectID>, INTran, object>) e).NewValue = (object) pmProject.ContractCD;
        break;
      }
    }
  }

  protected override void _(PX.Data.Events.FieldVerifying<INTran, INTran.projectID> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue is int?))
      return;
    base._(e);
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue);
    if (pmProject == null)
      return;
    bool? nullable2 = pmProject.NonProject;
    if (nullable2.GetValueOrDefault() || !(pmProject.AccountingMode == "L"))
      return;
    nullable1 = e.Row.LocationID;
    if (!nullable1.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, e.Row.LocationID);
    if (inLocation == null)
      return;
    nullable1 = inLocation.ProjectID;
    int? nullable3 = pmProject.ContractID;
    if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
      return;
    nullable3 = inLocation.ProjectID;
    if (!nullable3.HasValue && !PXAccess.FeatureInstalled<FeaturesSet.materialManagement>())
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
    int num2;
    if (inventoryItem == null)
    {
      num2 = 0;
    }
    else
    {
      nullable2 = inventoryItem.StkItem;
      num2 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    if (num2 != 0)
    {
      PXSetPropertyException<INTran.projectID> propertyException = new PXSetPropertyException<INTran.projectID>("The {0} project cannot be selected because the {1} warehouse location specified in the line is not linked to this project. Select another project, or change the warehouse location.", (PXErrorLevel) 4, new object[2]
      {
        (object) pmProject.ContractCD,
        (object) inLocation.LocationCD
      });
      ((PXSetPropertyException) propertyException).ErrorValue = (object) pmProject.ContractCD;
      throw propertyException;
    }
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.taskID> e)
  {
    INTran row = e.Row;
    if (row == null || !ProjectAttribute.IsPMVisible("IN") || !row.LocationID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
    if (inventoryItem != null && !inventoryItem.StkItem.GetValueOrDefault())
      return;
    foreach (PXResult<INLocation, PMTask> pxResult in PXSelectBase<INLocation, PXSelectReadonly2<INLocation, LeftJoin<PMTask, On<PMTask.projectID, Equal<INLocation.projectID>, And<PMTask.taskID, Equal<INLocation.taskID>>>>, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.locationID, Equal<Required<INLocation.locationID>>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>>) e).Cache.Graph, new object[2]
    {
      (object) row.SiteID,
      (object) row.LocationID
    }))
    {
      PMTask pmTask = PXResult<INLocation, PMTask>.op_Implicit(pxResult);
      if (pmTask != null && pmTask.TaskCD != null && pmTask.VisibleInIN.GetValueOrDefault() && pmTask.IsActive.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>, INTran, object>) e).NewValue = (object) pmTask.TaskCD;
        break;
      }
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.locationID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
    if ((inventoryItem != null ? (inventoryItem.StkItem.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    if (this.IsProjectLocation((int?) e.NewValue))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValue<INTran.inventorySource>((object) e.Row, (object) "P");
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.projectID>((object) e.Row);
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.GetValuePending<INTran.taskID>((object) e.Row) == null)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValuePending<INTran.taskID>((object) e.Row, PXCache.NotSetValue);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.taskID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode> e)
  {
    INTran row = e.Row;
    if (row == null)
      return;
    PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this.Base, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode>, INTran, object>) e).NewValue);
    if (reasonCode == null || !row.ProjectID.HasValue || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) reasonCode.AccountID
    }));
    if (account == null || account.AccountGroupID.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode>>) e).Cache.RaiseExceptionHandling<INTran.reasonCode>((object) e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 2, new object[1]
    {
      (object) account.AccountCD
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.projectID> e)
  {
    if (!(PXParentAttribute.SelectParent(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.projectID>>) e).Cache, (object) e.Row, typeof (INTran)) is INTran inTran))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.projectID>, INTranSplit, object>) e).NewValue = (object) inTran.ProjectID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.taskID> e)
  {
    if (!(PXParentAttribute.SelectParent(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.taskID>>) e).Cache, (object) e.Row, typeof (INTran)) is INTran inTran))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTranSplit, INTranSplit.taskID>, INTranSplit, object>) e).NewValue = (object) inTran.TaskID;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.INRegister> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<INTran.projectID>(((PXSelectBase) this.Base.transactions).Cache, (object) null, this.IsPMVisible);
    PXUIFieldAttribute.SetVisible<INTran.taskID>(((PXSelectBase) this.Base.transactions).Cache, (object) null, this.IsPMVisible);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INTran> e)
  {
    INTran row = e.Row;
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1 || !(PMProject.PK.Find((PXGraph) this.Base, e.Row.ProjectID)?.AccountingMode == "L"))
      return;
    this.CheckForSingleLocation(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache, e.Row);
    this.CheckLocationTaskRule(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache, e.Row);
  }

  protected virtual void CheckLocationTaskRule(PXCache sender, INTran row)
  {
    if (!row.TaskID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.LocationID);
    if (inLocation == null)
      return;
    int? taskId1 = inLocation.TaskID;
    int? taskId2 = row.TaskID;
    if (taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue || !inLocation.TaskID.HasValue)
      return;
    sender.RaiseExceptionHandling<INTran.locationID>((object) row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The Project Task specified for the given Location do not match the selected Project Task.", (PXErrorLevel) 2));
  }

  protected virtual void CheckForSingleLocation(PXCache sender, INTran row)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryID);
    if (inventoryItem == null || !inventoryItem.StkItem.GetValueOrDefault() || POLineType.IsProjectDropShip(row.POLineType) || !row.TaskID.HasValue || row.LocationID.HasValue)
      return;
    sender.RaiseExceptionHandling<INTran.locationID>((object) row, (object) null, (Exception) new PXSetPropertyException("When posting to Project Location must be the same for all splits."));
  }
}
