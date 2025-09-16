// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Services.ProjectTaskValidationServiceBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.Services;

public abstract class ProjectTaskValidationServiceBase
{
  private readonly PXGraph graph;

  protected ProjectTaskValidationServiceBase()
  {
    this.graph = PXGraph.CreateInstance(typeof (PXGraph));
  }

  protected abstract bool IsTaskUsedInCostDocument(int? projectID, int? taskId);

  protected abstract bool IsTaskUsedInRevenueDocument(int? projectID, int? taskId);

  protected bool IsTaskUsed<TTable, TProjectField, TProjectTaskField>(
    int? projectID,
    int? projectTaskId)
    where TTable : class, IBqlTable, new()
    where TProjectField : IBqlField
    where TProjectTaskField : IBqlField
  {
    return ((PXSelectBase<TTable>) new PXSelect<TTable, Where<TProjectField, Equal<Required<TProjectField>>, And<TProjectTaskField, Equal<Required<TProjectTaskField>>>>>(this.graph)).Select(new object[2]
    {
      (object) projectID,
      (object) projectTaskId
    }).FirstTableItems.Any<TTable>();
  }

  protected bool IsTaskUsed<TTable, TProjectField, TProjectTaskField, TBudgetTypeField>(
    int? projectID,
    int? projectTaskId,
    string budgetType)
    where TTable : class, IBqlTable, new()
    where TProjectField : IBqlField
    where TProjectTaskField : IBqlField
    where TBudgetTypeField : IBqlField
  {
    return ((PXSelectBase<TTable>) new PXSelect<TTable, Where<TProjectField, Equal<Required<TProjectField>>, And<TProjectTaskField, Equal<Required<TProjectTaskField>>, And<TBudgetTypeField, Equal<Required<TBudgetTypeField>>>>>>(this.graph)).Select(new object[3]
    {
      (object) projectID,
      (object) projectTaskId,
      (object) budgetType
    }).FirstTableItems.Any<TTable>();
  }
}
