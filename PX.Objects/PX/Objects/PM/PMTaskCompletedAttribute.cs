// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaskCompletedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class PMTaskCompletedAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber
{
  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null)
      return;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    if (e.Row is PMBudget)
    {
      PMBudget row = e.Row as PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
    }
    else if (e.Row is PX.Objects.PM.Lite.PMBudget)
    {
      PX.Objects.PM.Lite.PMBudget row = e.Row as PX.Objects.PM.Lite.PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
    }
    if (!nullable1.HasValue || !nullable2.HasValue)
      return;
    this.CalculateTaskCompleted(sender.Graph, nullable1.Value, nullable2.Value, false);
  }

  public void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Row == null)
      return;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    if (e.Row is PMBudget)
    {
      PMBudget row = e.Row as PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
    }
    else if (e.Row is PX.Objects.PM.Lite.PMBudget)
    {
      PX.Objects.PM.Lite.PMBudget row = e.Row as PX.Objects.PM.Lite.PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
    }
    if (!nullable1.HasValue || !nullable2.HasValue || e.Row is PMCostBudget)
      return;
    ((PXCache) GraphHelper.Caches<PMCostBudget>(sender.Graph)).ClearQueryCache();
    ((PXCache) GraphHelper.Caches<PMCostBudget>(sender.Graph)).Clear();
    this.CalculateTaskCompleted(sender.Graph, nullable1.Value, nullable2.Value, true);
  }

  public void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (e.Row == null)
      return;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    bool? nullable3 = new bool?();
    if (e.Row is PMBudget)
    {
      PMBudget row = e.Row as PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
      nullable3 = row.IsProduction;
    }
    else if (e.Row is PX.Objects.PM.Lite.PMBudget)
    {
      PX.Objects.PM.Lite.PMBudget row = e.Row as PX.Objects.PM.Lite.PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
      nullable3 = row.IsProduction;
    }
    if (!nullable1.HasValue || !nullable2.HasValue || !nullable3.GetValueOrDefault())
      return;
    this.CalculateTaskCompleted(sender.Graph, nullable1.Value, nullable2.Value, false);
  }

  public void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    bool? nullable3 = new bool?();
    if (e.Row is PMBudget)
    {
      PMBudget row = e.Row as PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
      nullable3 = row.IsProduction;
    }
    else if (e.Row is PX.Objects.PM.Lite.PMBudget)
    {
      PX.Objects.PM.Lite.PMBudget row = e.Row as PX.Objects.PM.Lite.PMBudget;
      nullable1 = row.ProjectID;
      nullable2 = row.ProjectTaskID;
      nullable3 = row.IsProduction;
    }
    if (!nullable1.HasValue || !nullable2.HasValue || !nullable3.GetValueOrDefault())
      return;
    this.CalculateTaskCompleted(sender.Graph, nullable1.Value, nullable2.Value, false);
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.OldRow is PMBudget && e.Row is PMBudget)
    {
      PMBudget oldRow = (PMBudget) e.OldRow;
      PMBudget row = (PMBudget) e.Row;
      if (oldRow == null || row == null)
        return;
      int? nullable1 = row.ProjectID;
      if (!nullable1.HasValue)
        return;
      nullable1 = oldRow.ProjectTaskID;
      int? projectTaskId = row.ProjectTaskID;
      if (!(nullable1.GetValueOrDefault() == projectTaskId.GetValueOrDefault() & nullable1.HasValue == projectTaskId.HasValue))
      {
        int? nullable2 = oldRow.ProjectTaskID;
        if (nullable2.HasValue)
        {
          PXGraph graph = sender.Graph;
          nullable2 = row.ProjectID;
          int projectID = nullable2.Value;
          nullable2 = oldRow.ProjectTaskID;
          int taskID = nullable2.Value;
          this.CalculateTaskCompleted(graph, projectID, taskID, false);
        }
        nullable2 = row.ProjectTaskID;
        if (!nullable2.HasValue)
          return;
        PXGraph graph1 = sender.Graph;
        nullable2 = row.ProjectID;
        int projectID1 = nullable2.Value;
        nullable2 = row.ProjectTaskID;
        int taskID1 = nullable2.Value;
        this.CalculateTaskCompleted(graph1, projectID1, taskID1, false);
      }
      else
      {
        int? nullable3 = row.ProjectTaskID;
        if (!nullable3.HasValue)
          return;
        bool? isProduction1 = oldRow.IsProduction;
        bool? isProduction2 = row.IsProduction;
        if (isProduction1.GetValueOrDefault() == isProduction2.GetValueOrDefault() & isProduction1.HasValue == isProduction2.HasValue)
        {
          Decimal? nullable4 = oldRow.ActualQty;
          Decimal? actualQty = row.ActualQty;
          if (nullable4.GetValueOrDefault() == actualQty.GetValueOrDefault() & nullable4.HasValue == actualQty.HasValue)
          {
            Decimal? nullable5 = oldRow.RevisedQty;
            nullable4 = row.RevisedQty;
            if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
            {
              nullable4 = oldRow.CuryActualAmount;
              nullable5 = row.CuryActualAmount;
              if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
              {
                nullable5 = oldRow.CuryRevisedAmount;
                nullable4 = row.CuryRevisedAmount;
                if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue && !(oldRow.UOM != row.UOM))
                {
                  nullable3 = oldRow.AccountGroupID;
                  nullable1 = row.AccountGroupID;
                  if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
                  {
                    nullable1 = oldRow.InventoryID;
                    nullable3 = row.InventoryID;
                    if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
                      return;
                  }
                }
              }
            }
          }
        }
        PXGraph graph = sender.Graph;
        nullable3 = row.ProjectID;
        int projectID = nullable3.Value;
        nullable3 = row.ProjectTaskID;
        int taskID = nullable3.Value;
        this.CalculateTaskCompleted(graph, projectID, taskID, false);
      }
    }
    else
    {
      if (!(e.OldRow is PX.Objects.PM.Lite.PMBudget) || !(e.Row is PX.Objects.PM.Lite.PMBudget))
        return;
      PX.Objects.PM.Lite.PMBudget oldRow = (PX.Objects.PM.Lite.PMBudget) e.OldRow;
      PX.Objects.PM.Lite.PMBudget row = (PX.Objects.PM.Lite.PMBudget) e.Row;
      if (oldRow == null || row == null)
        return;
      int? nullable6 = row.ProjectID;
      if (!nullable6.HasValue)
        return;
      nullable6 = oldRow.ProjectTaskID;
      int? projectTaskId = row.ProjectTaskID;
      if (!(nullable6.GetValueOrDefault() == projectTaskId.GetValueOrDefault() & nullable6.HasValue == projectTaskId.HasValue))
      {
        int? nullable7 = oldRow.ProjectTaskID;
        if (nullable7.HasValue)
        {
          PXGraph graph = sender.Graph;
          nullable7 = row.ProjectID;
          int projectID = nullable7.Value;
          nullable7 = oldRow.ProjectTaskID;
          int taskID = nullable7.Value;
          this.CalculateTaskCompleted(graph, projectID, taskID, false);
        }
        nullable7 = row.ProjectTaskID;
        if (!nullable7.HasValue)
          return;
        PXGraph graph2 = sender.Graph;
        nullable7 = row.ProjectID;
        int projectID2 = nullable7.Value;
        nullable7 = row.ProjectTaskID;
        int taskID2 = nullable7.Value;
        this.CalculateTaskCompleted(graph2, projectID2, taskID2, false);
      }
      else
      {
        int? nullable8 = row.ProjectTaskID;
        if (!nullable8.HasValue)
          return;
        bool? isProduction3 = oldRow.IsProduction;
        bool? isProduction4 = row.IsProduction;
        if (isProduction3.GetValueOrDefault() == isProduction4.GetValueOrDefault() & isProduction3.HasValue == isProduction4.HasValue)
        {
          Decimal? nullable9 = oldRow.CuryActualAmount;
          Decimal? curyActualAmount = row.CuryActualAmount;
          if (nullable9.GetValueOrDefault() == curyActualAmount.GetValueOrDefault() & nullable9.HasValue == curyActualAmount.HasValue)
          {
            Decimal? curyRevisedAmount = oldRow.CuryRevisedAmount;
            nullable9 = row.CuryRevisedAmount;
            if (curyRevisedAmount.GetValueOrDefault() == nullable9.GetValueOrDefault() & curyRevisedAmount.HasValue == nullable9.HasValue && !(oldRow.UOM != row.UOM))
            {
              nullable8 = oldRow.AccountGroupID;
              nullable6 = row.AccountGroupID;
              if (nullable8.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable8.HasValue == nullable6.HasValue)
              {
                nullable6 = oldRow.InventoryID;
                nullable8 = row.InventoryID;
                if (nullable6.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable6.HasValue == nullable8.HasValue)
                  return;
              }
            }
          }
        }
        PXGraph graph = sender.Graph;
        nullable8 = row.ProjectID;
        int projectID = nullable8.Value;
        nullable8 = row.ProjectTaskID;
        int taskID = nullable8.Value;
        this.CalculateTaskCompleted(graph, projectID, taskID, false);
      }
    }
  }

  public static Decimal GetCompletionPercentageOfCompletedTask(PMTask task)
  {
    return PMTaskCompletedAttribute.GetCompletionPercentageOfCompletedTask(task.CompletedPctMethod, task.CompletedPercent.GetValueOrDefault());
  }

  private static Decimal GetCompletionPercentageOfCompletedTask(
    string completedPctMethod,
    Decimal completionPercentage)
  {
    return !(completedPctMethod == "M") ? completionPercentage : Math.Max(100M, completionPercentage);
  }

  protected void CalculateTaskCompleted(
    PXGraph graph,
    int projectID,
    int taskID,
    bool inPersisted)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.Select(graph, new object[1]
    {
      (object) projectID
    }));
    if (pmProject == null || !(pmProject.BaseType == "P"))
      return;
    PMTask task = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) projectID,
      (object) taskID
    }));
    if (task == null || !(task.CompletedPctMethod == "A") && !(task.CompletedPctMethod == "Q"))
      return;
    Decimal completionPercentage = PMTaskCompletedAttribute.CalculateTaskCompletionPercentage(graph, task);
    if (task.Status == "F")
      completionPercentage = PMTaskCompletedAttribute.GetCompletionPercentageOfCompletedTask(task.CompletedPctMethod, completionPercentage);
    Decimal? completedPercent = task.CompletedPercent;
    Decimal num = completionPercentage;
    if (completedPercent.GetValueOrDefault() == num & completedPercent.HasValue)
      return;
    if (inPersisted)
    {
      ((PXCache) GraphHelper.Caches<PMTask>(graph)).SetValue<PMTask.completedPercent>((object) task, (object) completionPercentage);
      ((PXCache) GraphHelper.Caches<PMTask>(graph)).SetStatus((object) task, (PXEntryStatus) 1);
    }
    else
    {
      task.CompletedPercent = new Decimal?(completionPercentage);
      GraphHelper.Caches<PMTask>(graph).Update(task);
    }
  }

  public static Decimal CalculateTaskCompletionPercentage(PXGraph graph, PMTask task)
  {
    Decimal completionPercentage = 0M;
    IEnumerable<PMCostBudget> source1 = GraphHelper.RowCast<PMCostBudget>((IEnumerable) PXSelectBase<PMCostBudget, PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Required<PMTask.projectID>>, And<PMCostBudget.projectTaskID, Equal<Required<PMTask.taskID>>, And<PMCostBudget.isProduction, Equal<True>, And<PMCostBudget.type, Equal<AccountType.expense>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) task.ProjectID,
      (object) task.TaskID
    }));
    if (source1 != null)
    {
      double num1 = 0.0;
      int num2 = 0;
      Decimal num3 = 0M;
      Decimal num4 = 0M;
      foreach (IGrouping<string, PMCostBudget> source2 in source1.GroupBy<PMCostBudget, string>((Func<PMCostBudget, string>) (c => PMTaskCompletedAttribute.GetGroupKey(c))))
      {
        if (task.CompletedPctMethod == "Q")
        {
          Decimal num5 = source2.Sum<PMCostBudget>((Func<PMCostBudget, Decimal>) (c => c.RevisedQty ?? 0M));
          if (num5 != 0M)
          {
            ++num2;
            double num6 = num1;
            Decimal num7 = (Decimal) 100;
            Decimal? nullable1 = source2.Sum<PMCostBudget>((Func<PMCostBudget, Decimal?>) (c => c.ActualQty));
            Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num7 * nullable1.GetValueOrDefault()) : new Decimal?();
            Decimal num8 = num5;
            Decimal? nullable3;
            if (!nullable2.HasValue)
            {
              nullable1 = new Decimal?();
              nullable3 = nullable1;
            }
            else
              nullable3 = new Decimal?(nullable2.GetValueOrDefault() / num8);
            double num9 = Convert.ToDouble((object) nullable3);
            num1 = num6 + num9;
          }
        }
        else if (task.CompletedPctMethod == "A")
        {
          num3 += source2.Sum<PMCostBudget>((Func<PMCostBudget, Decimal>) (c => c.CuryActualAmount ?? 0M));
          num4 += source2.Sum<PMCostBudget>((Func<PMCostBudget, Decimal>) (c => c.CuryRevisedAmount ?? 0M));
        }
      }
      completionPercentage = !(task.CompletedPctMethod == "A") ? (num2 == 0 ? 0M : Convert.ToDecimal(num1 / (double) num2)) : (num4 == 0M ? 0M : Convert.ToDecimal(100M * num3 / num4));
    }
    return completionPercentage;
  }

  protected static string GetGroupKey(PMCostBudget budget)
  {
    string empty = string.Empty;
    string str1;
    int? nullable;
    if (!budget.AccountGroupID.HasValue)
    {
      str1 = empty + "null";
    }
    else
    {
      string str2 = empty;
      nullable = budget.AccountGroupID;
      string str3 = nullable.ToString();
      str1 = str2 + str3;
    }
    string str4 = str1 + "_";
    nullable = budget.InventoryID;
    string str5;
    if (!nullable.HasValue)
    {
      str5 = str4 + "null";
    }
    else
    {
      string str6 = str4;
      nullable = budget.InventoryID;
      string str7 = nullable.ToString();
      str5 = str6 + str7;
    }
    return $"{str5}_{budget.UOM}";
  }
}
