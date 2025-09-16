// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.INTransferEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class INTransferEntryExt : ProjectCostCenterSupport<INTransferEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXMergeAttributes]
  [PXUIField(DisplayName = "From Inventory Source")]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.inventorySource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<INTran.toProjectID>(((PXSelectBase) this.Base.transactions).Cache, (object) null, e.Row.TransferType == "1");
    PXUIFieldAttribute.SetVisible<INTran.toTaskID>(((PXSelectBase) this.Base.transactions).Cache, (object) null, e.Row.TransferType == "1");
    PXUIFieldAttribute.SetVisible<INTran.toCostLayerType>(((PXSelectBase) this.Base.transactions).Cache, (object) null, false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INTran> e)
  {
    if (e.Row == null)
      return;
    PXCacheEx.Adjust<CostLayerType.ListAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row).For<INTran.toCostLayerType>((Action<CostLayerType.ListAttribute>) (a => a.AllowProjects = EnumerableExtensions.IsIn<string>(e.Row.CostLayerType, "N", "P")));
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache.GetAttributes<INTran.toCostLayerType>((object) e.Row).OfType<CostLayerType.ListAttribute>().FirstOrDefault<CostLayerType.ListAttribute>()?.SetValues(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row);
  }

  [PXOverride]
  public virtual (bool? Project, bool? Task) IsProjectTaskEnabled(
    INTran row,
    Func<INTran, (bool? Project, bool? Task)> baseMethod)
  {
    (bool?, bool?) valueTuple = baseMethod(row);
    INTransferEntryExt.LinkedInfo linkedInfo = this.GetLinkedInfo(row.LocationID);
    return (new bool?((valueTuple.Item1 ?? true) && !linkedInfo.IsLinked), new bool?((valueTuple.Item2 ?? true) && !linkedInfo.IsTaskRestricted));
  }

  [PXOverride]
  public virtual (bool? ToProject, bool? ToTask) IsToProjectTaskEnabled(
    INTran row,
    Func<INTran, (bool? ToProject, bool? ToTask)> baseMethod)
  {
    (bool?, bool?) valueTuple = baseMethod(row);
    INTransferEntryExt.LinkedInfo linkedInfo = this.GetLinkedInfo(row.ToLocationID);
    return (new bool?((valueTuple.Item1 ?? true) && !linkedInfo.IsLinked), new bool?((valueTuple.Item2 ?? true) && !linkedInfo.IsTaskRestricted));
  }

  protected override void _(PX.Data.Events.FieldDefaulting<INTran, INTran.projectID> e)
  {
    base._(e);
    INTran row = e.Row;
    if (row == null || !row.LocationID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.LocationID);
    if (!inLocation.ProjectID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.projectID>, INTran, object>) e).NewValue = (object) inLocation.ProjectID;
  }

  protected override void _(PX.Data.Events.FieldVerifying<INTran, INTran.projectID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue is int?) || !((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>>) e).ExternalCall)
      return;
    base._(e);
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue);
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || !(pmProject.AccountingMode == "L") || !e.Row.LocationID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, e.Row.LocationID);
    if (inLocation == null)
      return;
    int? projectId = inLocation.ProjectID;
    int? nullable = pmProject.ContractID;
    if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue)
      return;
    nullable = inLocation.ProjectID;
    if (nullable.HasValue || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>())
      throw new PXSetPropertyException("The {0} project cannot be selected because the {1} warehouse location specified in the line is not linked to this project. Select another project, or change the warehouse location.", (PXErrorLevel) 4, new object[2]
      {
        (object) pmProject.ContractCD,
        (object) inLocation.LocationCD
      })
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.taskID> e)
  {
    INTran row = e.Row;
    if (row == null || !row.LocationID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.LocationID);
    if (inLocation.TaskID.HasValue)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>, INTran, object>) e).NewValue = (object) inLocation.TaskID;
    }
    else
    {
      if (!inLocation.ProjectID.HasValue)
        return;
      PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.isActive, Equal<True>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
      {
        (object) inLocation.ProjectID
      }));
      if (pmTask == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.taskID>, INTran, object>) e).NewValue = (object) pmTask.TaskID;
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.locationID> e)
  {
    if (e.Row == null || ((PXSelectBase<INRegister>) this.Base.CurrentDocument).Current == null)
      return;
    if (this.IsProjectLocation((int?) e.NewValue))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValue<INTran.inventorySource>((object) e.Row, (object) "P");
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.projectID>((object) e.Row);
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.GetValuePending<INTran.taskID>((object) e.Row) == null)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetValuePending<INTran.taskID>((object) e.Row, PXCache.NotSetValue);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.locationID>>) e).Cache.SetDefaultExt<INTran.taskID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTran, INTran.toProjectID> e)
  {
    INTran row = e.Row;
    if (row == null)
      return;
    if (this.IsTransferSalesOrderBasedProjectTrackingTran(row))
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toProjectID>, INTran, object>) e).NewValue = (object) row.ProjectID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toProjectID>>) e).Cancel = true;
    }
    else
    {
      if (!row.ToLocationID.HasValue || !(e.Row.ToInventorySource != "F"))
        return;
      INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.ToLocationID);
      if (!inLocation.ProjectID.HasValue)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toProjectID>, INTran, object>) e).NewValue = (object) inLocation.ProjectID;
    }
  }

  protected override void _(
    PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID>, INTran, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID>, INTran, object>) e).NewValue is int?))
      return;
    base._(e);
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID>, INTran, object>) e).NewValue);
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || !(pmProject.AccountingMode == "L") || !e.Row.ToLocationID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, e.Row.ToLocationID);
    if (inLocation == null)
      return;
    int? projectId = inLocation.ProjectID;
    int? contractId = pmProject.ContractID;
    if (!(projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue) && (inLocation.ProjectID.HasValue || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>()))
      throw new PXSetPropertyException("The {0} project cannot be selected because the {1} warehouse location specified in the line is not linked to this project. Select another project, or change the warehouse location.", (PXErrorLevel) 4, new object[2]
      {
        (object) pmProject.ContractCD,
        (object) inLocation.LocationCD
      })
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.toTaskID> e)
  {
    INTran row = e.Row;
    if (row == null)
      return;
    if (this.IsTransferSalesOrderBasedProjectTrackingTran(row))
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toTaskID>, INTran, object>) e).NewValue = (object) row.TaskID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toTaskID>>) e).Cancel = true;
    }
    else
    {
      if (!row.ToLocationID.HasValue)
        return;
      INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, row.ToLocationID);
      int? nullable = inLocation.TaskID;
      if (nullable.HasValue)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toTaskID>, INTran, object>) e).NewValue = (object) inLocation.TaskID;
      }
      else
      {
        nullable = inLocation.ProjectID;
        if (!nullable.HasValue)
          return;
        PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.isActive, Equal<True>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
        {
          (object) inLocation.ProjectID
        }));
        if (pmTask == null)
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toTaskID>, INTran, object>) e).NewValue = (object) pmTask.TaskID;
      }
    }
  }

  public override string GetDestinationInventorySource(INTran tran)
  {
    if (!this.IsTransferSalesOrderBasedProjectTrackingTran(tran))
      return base.GetDestinationInventorySource(tran);
    return !ProjectDefaultAttribute.IsNonProject(tran.ProjectID) ? "P" : "F";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INTran, INTran.toCostCodeID> e)
  {
    if (!this.IsTransferSalesOrderBasedProjectTrackingTran(e.Row))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toCostCodeID>, INTran, object>) e).NewValue = (object) e.Row.CostCodeID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.toCostCodeID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.toLocationID> e)
  {
    if (e.Row == null || ((PXSelectBase<INRegister>) this.Base.CurrentDocument).Current == null || ((PXGraph) this.Base).IsImportFromExcel)
      return;
    if (this.IsProjectLocation((int?) e.NewValue))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.toLocationID>>) e).Cache.SetValue<INTran.toInventorySource>((object) e.Row, (object) "P");
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.toLocationID>>) e).Cache.SetDefaultExt<INTran.toProjectID>((object) e.Row);
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.toLocationID>>) e).Cache.GetValuePending<INTran.toTaskID>((object) e.Row) == null)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.toLocationID>>) e).Cache.SetValuePending<INTran.toTaskID>((object) e.Row, PXCache.NotSetValue);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.toLocationID>>) e).Cache.SetDefaultExt<INTran.toTaskID>((object) e.Row);
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

  private INTransferEntryExt.LinkedInfo GetLinkedInfo(int? locationID)
  {
    INTransferEntryExt.LinkedInfo linkedInfo = new INTransferEntryExt.LinkedInfo();
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

  protected virtual bool IsTransferSalesOrderBasedProjectTrackingTran(INTran tran)
  {
    return tran != null && tran.InventoryID.HasValue && tran.SOOrderType == "TR" && tran.SOOrderNbr != null && this.GetAccountingMode(tran.ProjectID) != "L";
  }

  private struct LinkedInfo
  {
    public bool IsLinked;
    public bool IsTaskRestricted;
  }
}
