// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RevenueTaskExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public abstract class RevenueTaskExt<T, D> : PXGraphExtension<T>
  where T : PXGraph
  where D : PMBudget, IBqlTable, new()
{
  public bool SuppressRevenueTaskIDDefaulting { get; set; }

  protected virtual void _(
    Events.FieldDefaulting<D, PMBudget.revenueTaskID> e)
  {
    if (this.SuppressRevenueTaskIDDefaulting)
      return;
    PMProject project = this.GetProject();
    if (project == null || (string) ((Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<D, PMBudget.revenueTaskID>>) e).Cache.GetValue<PMBudget.type>((object) e.Row) == "I")
      return;
    if (this.Base.IsImportFromExcel || this.Base.IsImport)
    {
      object valuePending = ((Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<D, PMBudget.revenueTaskID>>) e).Cache.GetValuePending<PMBudget.revenueTaskID>((object) e.Row);
      if (valuePending != PXCache.NotSetValue)
      {
        PMTask pmTask = PMTask.UK.Find((PXGraph) this.Base, project.ContractID, (string) valuePending, (PKFindOptions) 1);
        if (pmTask != null && pmTask.Type != "Cost")
        {
          ((Events.FieldDefaultingBase<Events.FieldDefaulting<D, PMBudget.revenueTaskID>, D, object>) e).NewValue = (object) pmTask.TaskID;
          return;
        }
      }
    }
    if ((object) e.Row == null || project == null || !e.Row.TaskID.HasValue)
      return;
    PMTask pmTask1 = PMTask.PK.Find((PXGraph) this.Base, project.ContractID, e.Row.TaskID, (PKFindOptions) 1);
    if (pmTask1 == null || !(pmTask1.Type == "CostRev"))
      return;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<D, PMBudget.revenueTaskID>, D, object>) e).NewValue = (object) pmTask1.TaskID;
  }

  protected virtual void _(Events.FieldUpdated<D, PMBudget.projectTaskID> e)
  {
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<D, PMBudget.projectTaskID>>) e).Cache.SetDefaultExt((object) e.Row, "RevenueTaskID", (object) null);
  }

  protected virtual void _(Events.FieldUpdated<PMTask, PMTask.type> e)
  {
    if (!(e.Row.Type == "Cost"))
      return;
    this.ClearRevenueTask(e.Row.TaskID);
  }

  protected virtual void _(Events.RowDeleted<PMTask> e) => this.ClearRevenueTask(e.Row.TaskID);

  protected virtual void ClearRevenueTask(int? taskID)
  {
    foreach (PXResult<D> pxResult in ((PXSelectBase<D>) new PXSelect<D, Where<PMBudget.revenueTaskID, Equal<Required<PMBudget.revenueTaskID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) taskID
    }))
    {
      D d = PXResult<D>.op_Implicit(pxResult);
      this.Base.Caches[typeof (D)].SetValue<PMBudget.revenueTaskID>((object) d, (object) null);
      GraphHelper.MarkUpdated(this.Base.Caches[typeof (D)], (object) d);
    }
  }

  protected abstract PMProject GetProject();
}
