// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.INAdjustmentEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class INAdjustmentEntryExt : ProjectCostCenterSupport<INAdjustmentEntry>
{
  private List<PXTaskSetPropertyException> _taskExceptions = new List<PXTaskSetPropertyException>();

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXMergeAttributes]
  [PXVerifySelector(typeof (Search2<INCostStatus.receiptNbr, InnerJoin<INCostSubItemXRef, On<INCostSubItemXRef.costSubItemID, Equal<INCostStatus.costSubItemID>>, InnerJoin<INLocation, On<INLocation.locationID, Equal<Optional<INTran.locationID>>>, InnerJoin<PMProject, On<PMProject.contractID, Equal<Optional<INTran.projectID>>>>>>, Where<INCostStatus.inventoryID, Equal<Optional<INTran.inventoryID>>, And<INCostSubItemXRef.subItemID, Equal<Optional<INTran.subItemID>>, And<Where2<Where<INCostStatus.costSiteID, Equal<Optional<INTran.siteID>>, And2<Where<Optional<INTran.costCenterID>, Equal<CostCenter.freeStock>, Or<PMProject.accountingMode, Equal<ProjectAccountingModes.valuated>>>, And<INLocation.isCosted, Equal<False>, Or<INCostStatus.costSiteID, Equal<Optional<INTran.locationID>>>>>>, Or<INCostStatus.costSiteID, Equal<Optional<INTran.costCenterID>>>>>>>>), VerifyField = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.origRefNbr> e)
  {
  }

  public bool IsTaskErrorsHandlingIsEnabled { get; set; }

  public List<PXTaskSetPropertyException> TaskExceptions => this._taskExceptions;

  [PXOverride]
  public virtual (bool? Project, bool? Task) IsProjectTaskEnabled(
    INTran row,
    Func<INTran, (bool? Project, bool? Task)> baseMethod)
  {
    (bool?, bool?) valueTuple = baseMethod(row);
    INAdjustmentEntryExt.LinkedInfo linkedInfo = this.GetLinkedInfo(row.LocationID);
    return (new bool?((valueTuple.Item1 ?? true) && !linkedInfo.IsLinked), new bool?((valueTuple.Item2 ?? true) && !linkedInfo.IsTaskRestricted));
  }

  protected override void _(PX.Data.Events.FieldDefaulting<INTran, INTran.projectID> e)
  {
    base._(e);
    if (FlaggedModeScopeBase<SkipDefaultingFromLocationScope>.IsActive)
      return;
    INTran row = e.Row;
    if (row == null || !row.LocationID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.LocationID);
    if (!inLocation.ProjectID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.projectID>, INTran, object>) e).NewValue = (object) inLocation.ProjectID;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.taskID> e)
  {
    if (FlaggedModeScopeBase<SkipDefaultingFromLocationScope>.IsActive)
      return;
    INTran row = e.Row;
    if (row == null || !row.LocationID.HasValue)
      return;
    INLocation location = INLocation.PK.Find((PXGraph) this.Base, row.LocationID);
    if (location.TaskID.HasValue)
    {
      this.InvokeActionWithTaskErrorsHandling((Action) (() => BaseProjectTaskAttribute.VerifyTaskIsActive(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>>) e).Cache, (object) location.ProjectID, (object) location.TaskID)));
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>, INTran, object>) e).NewValue = (object) location.TaskID;
    }
    else
    {
      if (!location.ProjectID.HasValue)
        return;
      PMTask firstTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) location.ProjectID
      }));
      if (firstTask == null)
        return;
      this.InvokeActionWithTaskErrorsHandling((Action) (() => BaseProjectTaskAttribute.VerifyTaskIsActive(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>>) e).Cache, (object) location.ProjectID, (object) firstTask.TaskID)));
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>, INTran, object>) e).NewValue = (object) firstTask.TaskID;
    }
  }

  protected override void _(PX.Data.Events.FieldVerifying<INTran, INTran.projectID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue is int?))
      return;
    base._(e);
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue);
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || !(pmProject.AccountingMode == "L") || !e.Row.LocationID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, e.Row.LocationID);
    if (inLocation == null)
      return;
    int? projectId = inLocation.ProjectID;
    int? contractId = pmProject.ContractID;
    if (!(projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue) && (inLocation.ProjectID.HasValue || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>()))
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

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.locationID> e)
  {
    if (e.Row == null)
      return;
    if (this.IsProjectLocation((int?) e.NewValue))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValue<INTran.inventorySource>((object) e.Row, (object) "P");
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.projectID>((object) e.Row);
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.GetValuePending<INTran.taskID>((object) e.Row) == null)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValuePending<INTran.taskID>((object) e.Row, PXCache.NotSetValue);
    this.InvokeActionWithTaskErrorsHandling((Action) (() => ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.taskID>((object) e.Row)));
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

  private INAdjustmentEntryExt.LinkedInfo GetLinkedInfo(int? locationID)
  {
    INAdjustmentEntryExt.LinkedInfo linkedInfo = new INAdjustmentEntryExt.LinkedInfo();
    if (locationID.HasValue)
    {
      INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, locationID);
      if (inLocation != null && inLocation.ProjectID.HasValue)
      {
        PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, inLocation.ProjectID);
        if (pmProject != null)
        {
          linkedInfo.IsLinked = pmProject.AccountingMode == "L";
          linkedInfo.IsTaskRestricted = inLocation.TaskID.HasValue;
        }
      }
    }
    return linkedInfo;
  }

  private void InvokeActionWithTaskErrorsHandling(Action action)
  {
    try
    {
      action();
    }
    catch (PXTaskSetPropertyException ex)
    {
      if (this.IsTaskErrorsHandlingIsEnabled)
        this._taskExceptions.Add(ex);
      else
        throw;
    }
  }

  private struct LinkedInfo
  {
    public bool IsLinked;
    public bool IsTaskRestricted;
  }
}
