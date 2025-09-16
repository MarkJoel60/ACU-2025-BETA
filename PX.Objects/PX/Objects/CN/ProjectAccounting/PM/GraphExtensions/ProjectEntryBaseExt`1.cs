// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.GraphExtensions.ProjectEntryBaseExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CN.ProjectAccounting.PM.Services;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.GraphExtensions;

public class ProjectEntryBaseExt<TProjectGraph> : ProjectEntrySharedComponent<TProjectGraph> where TProjectGraph : ProjectEntryBase<TProjectGraph>
{
  public PXAction<PMProject> aia;
  public PXAction<PMProject> costProjection;
  protected int? outdatedForProject;
  protected HashSet<string> outdatedAIA;

  [PXUIField(DisplayName = "AIA Report")]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false)]
  protected virtual IEnumerable Aia(PXAdapter adapter)
  {
    if (((PXSelectBase<PMBillingRecord>) this.Base.Invoices).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PMBillingRecord>) this.Base.Invoices).Current.ProformaRefNbr))
    {
      ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
      ProformaEntryExt extension = ((PXGraph) instance).GetExtension<ProformaEntryExt>();
      ((PXSelectBase<PMProforma>) instance.Document).Current = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMBillingRecord.proformaRefNbr>>, And<PMProforma.corrected, NotEqual<True>>>>.Config>.Select((PXGraph) (object) this.Base, Array.Empty<object>()));
      ((PXAction) extension.aia).Press();
    }
    return adapter.Get();
  }

  protected virtual void _(Events.RowPersisting<PMProject> e)
  {
    Dictionary<int, PMTask> dictionary1 = GraphHelper.RowCast<PMTask>((IEnumerable) ((PXSelectBase<PMTask>) this.Base.Tasks).Select(Array.Empty<object>())).ToDictionary<PMTask, int>((Func<PMTask, int>) (task => task.TaskID.Value));
    foreach (PMCostBudget row in ((PXSelectBase) this.Base.CostBudget).Cache.Inserted)
    {
      int? projectTaskId = row.ProjectTaskID;
      if (projectTaskId.HasValue)
      {
        Dictionary<int, PMTask> dictionary2 = dictionary1;
        projectTaskId = row.ProjectTaskID;
        int key = projectTaskId.Value;
        PMTask pmTask;
        ref PMTask local = ref pmTask;
        if (dictionary2.TryGetValue(key, out local) && pmTask.Type == "Rev")
          ((PXSelectBase) this.Base.CostBudget).Cache.RaiseException<PMCostBudget.projectTaskID>((object) row, "Project Task Type is not valid. Only Tasks of 'Cost Task' and 'Cost and Revenue Task' types are allowed.", (object) pmTask.TaskCD);
      }
    }
    foreach (PMCostBudget row in ((PXSelectBase) this.Base.CostBudget).Cache.Updated)
    {
      int? projectTaskId = row.ProjectTaskID;
      if (projectTaskId.HasValue)
      {
        Dictionary<int, PMTask> dictionary3 = dictionary1;
        projectTaskId = row.ProjectTaskID;
        int key = projectTaskId.Value;
        PMTask pmTask;
        ref PMTask local = ref pmTask;
        if (dictionary3.TryGetValue(key, out local) && pmTask.Type == "Rev")
          ((PXSelectBase) this.Base.CostBudget).Cache.RaiseException<PMCostBudget.projectTaskID>((object) row, "Project Task Type is not valid. Only Tasks of 'Cost Task' and 'Cost and Revenue Task' types are allowed.", (object) pmTask.TaskCD);
      }
    }
    foreach (PMRevenueBudget row in ((PXSelectBase) this.Base.RevenueBudget).Cache.Inserted)
    {
      int? projectTaskId = row.ProjectTaskID;
      if (projectTaskId.HasValue)
      {
        Dictionary<int, PMTask> dictionary4 = dictionary1;
        projectTaskId = row.ProjectTaskID;
        int key = projectTaskId.Value;
        PMTask pmTask;
        ref PMTask local = ref pmTask;
        if (dictionary4.TryGetValue(key, out local) && pmTask.Type == "Cost")
          ((PXSelectBase) this.Base.RevenueBudget).Cache.RaiseException<PMRevenueBudget.projectTaskID>((object) row, "Project Task Type is not valid. Only Tasks of 'Revenue Task' and 'Cost and Revenue Task' types are allowed.", (object) pmTask.TaskCD);
      }
    }
    foreach (PMRevenueBudget row in ((PXSelectBase) this.Base.RevenueBudget).Cache.Updated)
    {
      int? projectTaskId = row.ProjectTaskID;
      if (projectTaskId.HasValue)
      {
        Dictionary<int, PMTask> dictionary5 = dictionary1;
        projectTaskId = row.ProjectTaskID;
        int key = projectTaskId.Value;
        PMTask pmTask;
        ref PMTask local = ref pmTask;
        if (dictionary5.TryGetValue(key, out local) && pmTask.Type == "Cost")
          ((PXSelectBase) this.Base.RevenueBudget).Cache.RaiseException<PMRevenueBudget.projectTaskID>((object) row, "Project Task Type is not valid. Only Tasks of 'Revenue Task' and 'Cost and Revenue Task' types are allowed.", (object) pmTask.TaskCD);
      }
    }
  }

  protected virtual void _(Events.RowSelected<PMProject> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.costProjection).SetEnabled(e.Row.Status != "L");
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CostProjection(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Base.Project).Current != null)
    {
      ProjectCostProjectionByDateEntry instance = PXGraph.CreateInstance<ProjectCostProjectionByDateEntry>();
      PMCostProjectionByDate projectionByDate = PXResultset<PMCostProjectionByDate>.op_Implicit(PXSelectBase<PMCostProjectionByDate, PXViewOf<PMCostProjectionByDate>.BasedOn<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMCostProjectionByDate.projectID, IBqlInt>.IsEqual<P.AsInt>>.Order<By<Desc<PMCostProjectionByDate.date>>>>.Config>.Select((PXGraph) (object) this.Base, Array.Empty<object>()));
      if (projectionByDate != null)
      {
        ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current = projectionByDate;
      }
      else
      {
        ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Insert();
        ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current.ProjectID = ((PXSelectBase<PMProject>) this.Base.Project).Current.ContractID;
        ((PXSelectBase) instance.Document).Cache.IsDirty = false;
      }
      throw new PXRedirectRequiredException((PXGraph) instance, nameof (CostProjection));
    }
    return adapter.Get();
  }

  protected virtual void _(Events.RowPersisting<PMTask> args)
  {
    PMTask row = args.Row;
    if (row == null)
      return;
    new ProjectTaskTypeUsageInConstructionValidationService().ValidateProjectTaskType(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<PMTask>>) args).Cache, row);
  }

  protected virtual void _(Events.RowSelected<PMBillingRecord> e)
  {
    if (e.Row == null || string.IsNullOrEmpty(e.Row.ProformaRefNbr) || !this.IsAIAOutdated(e.Row.ProjectID, e.Row.ProformaRefNbr))
      return;
    PXUIFieldAttribute.SetWarning(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMBillingRecord>>) e).Cache, (object) e.Row, "ProformaRefNbr", "The AIA report should be reprinted.");
  }

  protected virtual bool IsAIAOutdated(int? projectID, string proformaRefNbr)
  {
    if (this.outdatedForProject.HasValue)
    {
      int? outdatedForProject = this.outdatedForProject;
      int? nullable = projectID;
      if (!(outdatedForProject.GetValueOrDefault() == nullable.GetValueOrDefault() & outdatedForProject.HasValue == nullable.HasValue))
      {
        this.outdatedForProject = new int?();
        this.outdatedAIA = (HashSet<string>) null;
      }
    }
    if (this.outdatedAIA == null)
    {
      PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Required<PMProject.contractID>>, And<PMProforma.isAIAOutdated, Equal<True>>>> pxSelect = new PXSelect<PMProforma, Where<PMProforma.projectID, Equal<Required<PMProject.contractID>>, And<PMProforma.isAIAOutdated, Equal<True>>>>((PXGraph) (object) this.Base);
      this.outdatedForProject = projectID;
      this.outdatedAIA = new HashSet<string>();
      object[] objArray = new object[1]
      {
        (object) projectID
      };
      foreach (PXResult<PMProforma> pxResult in ((PXSelectBase<PMProforma>) pxSelect).Select(objArray))
        this.outdatedAIA.Add(PXResult<PMProforma>.op_Implicit(pxResult).RefNbr);
    }
    return this.outdatedAIA.Contains(proformaRefNbr);
  }

  protected virtual void _(Events.RowInserted<PMCostBudget> e)
  {
    if (!this.Base.IsCopyPaste)
      return;
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<PMCostBudget>>) e).Cache.SetValue<PMBudget.costProjectionCompletedPct>((object) e.Row, (object) 0M);
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<PMCostBudget>>) e).Cache.SetValue<PMBudget.costProjectionCostAtCompletion>((object) e.Row, (object) 0M);
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<PMCostBudget>>) e).Cache.SetValue<PMBudget.costProjectionCostToComplete>((object) e.Row, (object) 0M);
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<PMCostBudget>>) e).Cache.SetValue<PMBudget.costProjectionQtyAtCompletion>((object) e.Row, (object) 0M);
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<PMCostBudget>>) e).Cache.SetValue<PMBudget.costProjectionQtyToComplete>((object) e.Row, (object) 0M);
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<PMCostBudget>>) e).Cache.SetValue<PMBudget.curyCostProjectionCostAtCompletion>((object) e.Row, (object) 0M);
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<PMCostBudget>>) e).Cache.SetValue<PMBudget.curyCostProjectionCostToComplete>((object) e.Row, (object) 0M);
  }
}
