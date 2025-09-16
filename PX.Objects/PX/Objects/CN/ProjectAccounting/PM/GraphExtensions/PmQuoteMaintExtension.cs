// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.GraphExtensions.PmQuoteMaintExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.ProjectAccounting.PM.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.GraphExtensions;

public class PmQuoteMaintExtension : PXGraphExtension<PMQuoteMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [InjectDependency]
  public IProjectTaskDataProvider ProjectTaskDataProvider { get; set; }

  [PXOverride]
  public virtual void AddingTasksToProject(
    PMQuote quote,
    ProjectEntry projectEntry,
    Dictionary<string, int> taskMap,
    Dictionary<int, int> templateToNewTaskMap,
    bool? copyNotes,
    bool? copyFiles,
    Action<PMQuote, ProjectEntry, Dictionary<string, int>, Dictionary<int, int>, bool?, bool?> baseHandler)
  {
    baseHandler(quote, projectEntry, taskMap, templateToNewTaskMap, copyNotes, copyFiles);
    IEnumerable<PMQuoteTask> quoteTasks = ((PXSelectBase<PMQuoteTask>) this.Base.Tasks).Select(Array.Empty<object>()).FirstTableItems;
    EnumerableExtensions.ForEach<PMTask>(((PXSelectBase<PMTask>) projectEntry.Tasks).Select(Array.Empty<object>()).FirstTableItems, (Action<PMTask>) (t => PmQuoteMaintExtension.CopyTaskType(t, quoteTasks)));
  }

  [PXOverride]
  public virtual void RedefaultTasksFromTemplate(PMQuote quote, Action<PMQuote> baseHandler)
  {
    this.Base.DeleteAllTasks();
    foreach (PMTask projectTask in this.ProjectTaskDataProvider.GetProjectTasks((PXGraph) this.Base, quote.TemplateID).Where<PMTask>((Func<PMTask, bool>) (t => t.AutoIncludeInPrj.GetValueOrDefault())))
      this.Base.InsertNewTaskWithProjectTask(quote, projectTask, (Action<PMQuoteTask, PMTask>) ((qt, pt) => qt.Type = pt.Type));
  }

  private static void CopyTaskType(PMTask task, IEnumerable<PMQuoteTask> quoteTasks)
  {
    PMQuoteTask pmQuoteTask = quoteTasks.SingleOrDefault<PMQuoteTask>((Func<PMQuoteTask, bool>) (qt => qt.TaskCD == task.TaskCD));
    task.Type = pmQuoteTask?.Type ?? "CostRev";
  }
}
