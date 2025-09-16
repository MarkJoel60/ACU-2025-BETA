// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.SM;

public static class WorkflowAction
{
  public static bool? HasWorkflowActionEnabled<TEntity>(
    this PXGraph graph,
    string actionName,
    TEntity entity)
    where TEntity : class, IBqlTable, new()
  {
    return WorkflowAction.Of<PXGraph>.IsEnabled<TEntity>(graph, entity, actionName);
  }

  public static bool? HasWorkflowActionEnabled<TGraph, TEntity>(
    this TGraph graph,
    Expression<Func<TGraph, PXAction<TEntity>>> actionSelector,
    TEntity entity)
    where TGraph : PXGraph, new()
    where TEntity : class, IBqlTable, new()
  {
    return WorkflowAction.Of<TGraph>.IsEnabled<TEntity>(graph, entity, actionSelector);
  }

  public static class Of<TGraph> where TGraph : PXGraph, new()
  {
    public static bool? IsEnabled<TEntity>(
      TGraph graph,
      TEntity entity,
      Expression<Func<TGraph, PXAction<TEntity>>> actionSelector)
      where TEntity : class, IBqlTable, new()
    {
      return WorkflowAction.Of<TGraph>.IsEnabled<TEntity>(graph, entity, ((MemberExpression) actionSelector.Body).Member.Name);
    }

    public static bool? IsEnabled<TEntity>(TGraph graph, TEntity entity, string actionName) where TEntity : class, IBqlTable, new()
    {
      if ((object) graph == null || (object) entity == null || string.IsNullOrEmpty(actionName))
        return new bool?();
      IScreenToGraphWorkflowMappingService instance = ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<IScreenToGraphWorkflowMappingService>() : (IScreenToGraphWorkflowMappingService) null;
      if (instance == null)
        return new bool?();
      string screenIdFromGraphType = instance.GetScreenIDFromGraphType(typeof (TGraph));
      if (string.IsNullOrEmpty(screenIdFromGraphType))
        return new bool?();
      PXCache cach = graph.Caches[typeof (TEntity)];
      WorkflowAction.WorkflowActionChecker workflow;
      bool? nullable = WorkflowAction.WorkflowActionChecker.Get(screenIdFromGraphType, actionName, new Func<string, System.Type>(cach.GetBqlField), out workflow);
      return nullable.HasValue && nullable.GetValueOrDefault() ? workflow.Verify(cach, (object) entity) : new bool?();
    }
  }

  public sealed class IsEnabled<TEntity, TActionField> : IBqlUnary, IBqlCreator, IBqlVerifier
    where TEntity : IBqlTable
    where TActionField : IBqlField
  {
    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      if (graph == null || !info.BuildExpression || info.Tables == null)
        return true;
      if (!info.Tables.Contains(typeof (TEntity)))
        return WorkflowAction.IsEnabled<TEntity, TActionField>.Full(out exp);
      (string screenID, string actionName) = this.GetScreenActionFromProcessingActionField(graph);
      if (string.IsNullOrEmpty(screenID) || string.IsNullOrEmpty(actionName))
        return WorkflowAction.IsEnabled<TEntity, TActionField>.Empty(out exp);
      WorkflowAction.WorkflowActionChecker workflow;
      bool? nullable = WorkflowAction.WorkflowActionChecker.Get(screenID, actionName, new Func<string, System.Type>(graph.Caches[typeof (TEntity)].GetBqlField), out workflow);
      if (!nullable.HasValue)
        return WorkflowAction.IsEnabled<TEntity, TActionField>.Full(out exp);
      return nullable.GetValueOrDefault() && workflow.CreateExpression(out exp);
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      result = new bool?();
      if (cache == null || item == null)
        return;
      (string screenID, string actionName) = this.GetScreenActionFromProcessingActionField(cache.Graph);
      if (string.IsNullOrEmpty(screenID) || string.IsNullOrEmpty(actionName))
        return;
      WorkflowAction.WorkflowActionChecker workflow;
      bool? nullable = WorkflowAction.WorkflowActionChecker.Get(screenID, actionName, new Func<string, System.Type>(cache.GetBqlField), out workflow);
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      result = workflow.Verify(cache, item);
    }

    private static bool Empty(out SQLExpression exp)
    {
      exp = SQLExpression.IsTrue(false);
      return true;
    }

    private static bool Full(out SQLExpression exp)
    {
      exp = SQLExpression.IsTrue(true);
      return true;
    }

    private (string screenID, string actionName) GetScreenActionFromProcessingActionField(
      PXGraph processingGraph)
    {
      PXCache cach = processingGraph.Caches[BqlCommand.GetItemType<TActionField>()];
      string source = cach.GetValue(cach.Current, typeof (TActionField).Name)?.ToString();
      if (string.IsNullOrEmpty(source) || !source.Contains<char>('$'))
        return ((string) null, (string) null);
      string str1;
      string str2;
      ArrayDeconstruct.Deconstruct<string>(source.Split('$'), ref str1, ref str2);
      return (str1, str2);
    }
  }

  private class WorkflowActionChecker
  {
    private const string DefaultFlow = "DEFAULT";

    private System.Type FlowField { get; }

    private System.Type SubFlowField { get; }

    private IEnumerable<AUWorkflow> Flows { get; }

    private System.Type StateField { get; }

    private IEnumerable<(string workflowId, string workflowSubId, string stateName)> States { get; }

    private WorkflowActionChecker(
      System.Type flowField,
      System.Type subFlowField,
      IEnumerable<AUWorkflow> flows,
      System.Type stateField,
      IEnumerable<(string workflowId, string workflowSubId, string stateName)> states)
    {
      this.FlowField = flowField;
      this.SubFlowField = subFlowField;
      this.Flows = flows;
      this.StateField = stateField;
      this.States = states;
    }

    public bool CreateExpression(out SQLExpression exp)
    {
      exp = SQLExpression.None();
      foreach ((string str1, string str2, string str3) in this.States)
      {
        SQLExpression sqlExpression = SQLExpressionExt.EQ(Column.SQLColumn(this.StateField), (SQLExpression) new SQLConst((object) str3));
        SQLExpression r = SQLExpression.None();
        if (this.FlowField != (System.Type) null && this.SubFlowField != (System.Type) null)
        {
          Column flow = Column.SQLColumn(this.FlowField);
          Column subFlow = Column.SQLColumn(this.SubFlowField);
          if (string.IsNullOrEmpty(workflowId) && string.IsNullOrEmpty(str2))
          {
            SQLExpression[] array = this.Flows.Where<AUWorkflow>((Func<AUWorkflow, bool>) (f => f.WorkflowID != null && f.WorkflowSubID != null)).Select<AUWorkflow, SQLExpression>((Func<AUWorkflow, SQLExpression>) (f => SQLExpression.Not(SQLExpressionExt.EQ(flow, (SQLExpression) new SQLConst((object) f.WorkflowID)).And(SQLExpressionExt.EQ(subFlow, (SQLExpression) new SQLConst((object) f.WorkflowSubID)))))).Concat<SQLExpression>(this.Flows.Where<AUWorkflow>((Func<AUWorkflow, bool>) (f => f.WorkflowID != null && f.WorkflowSubID == null)).Select<AUWorkflow, SQLExpression>((Func<AUWorkflow, SQLExpression>) (f => SQLExpression.Not(SQLExpressionExt.EQ(flow, (SQLExpression) new SQLConst((object) f.WorkflowID)).And(WorkflowAction.WorkflowActionChecker.NotIn(subFlow, this.Flows.Where<AUWorkflow>((Func<AUWorkflow, bool>) (f1 => f1.WorkflowID == f.WorkflowID && f1.WorkflowSubID != null)).Select<AUWorkflow, string>((Func<AUWorkflow, string>) (f1 => f1.WorkflowSubID)))))))).ToArray<SQLExpression>();
            r = ((IEnumerable<SQLExpression>) array).Any<SQLExpression>() ? SQLExpression.And((IEnumerable<SQLExpression>) array) : SQLExpression.None();
          }
          else
            r = !string.IsNullOrEmpty(str2) ? (!string.IsNullOrEmpty(workflowId) ? SQLExpressionExt.EQ(flow, (SQLExpression) new SQLConst((object) workflowId)).And(SQLExpressionExt.EQ(subFlow, (SQLExpression) new SQLConst((object) str2))) : SQLExpressionExt.EQ(new SQLConst((object) 0), (SQLExpression) new SQLConst((object) 1))) : SQLExpressionExt.EQ(flow, (SQLExpression) new SQLConst((object) workflowId)).And(WorkflowAction.WorkflowActionChecker.NotIn(subFlow, this.Flows.Where<AUWorkflow>((Func<AUWorkflow, bool>) (f => f.WorkflowID == str1 && f.WorkflowSubID != null)).Select<AUWorkflow, string>((Func<AUWorkflow, string>) (f => f.WorkflowSubID))));
        }
        else if (this.FlowField != (System.Type) null)
        {
          Column column = Column.SQLColumn(this.FlowField);
          r = !string.IsNullOrEmpty(str1) ? SQLExpressionExt.EQ(column, (SQLExpression) new SQLConst((object) str1)) : WorkflowAction.WorkflowActionChecker.NotIn(column, this.Flows.Where<AUWorkflow>((Func<AUWorkflow, bool>) (f => f.WorkflowID != null)).Select<AUWorkflow, string>((Func<AUWorkflow, string>) (f => f.WorkflowID)));
        }
        exp = exp.Or(sqlExpression.And(r));
      }
      exp = exp.Embrace();
      return true;
    }

    private static SQLExpression NotIn(Column column, IEnumerable<string> values)
    {
      string[] array = values.ToArray<string>();
      return !((IEnumerable<string>) array).Any<string>() ? SQLExpression.None() : column.NotIn(((IEnumerable<string>) array).Aggregate<string, SQLExpression>(SQLExpression.None(), (Func<SQLExpression, string, SQLExpression>) ((acc, elem) => acc.Seq((object) elem))));
    }

    public bool? Verify(PXCache cache, object entity)
    {
      bool flag1 = false;
      string str = (string) cache.GetValue(entity, this.StateField.Name);
      foreach ((string workflowId, string workflowSubId, string stateName) in this.States)
      {
        bool flag2 = str == stateName;
        bool flag3 = true;
        if (this.FlowField != (System.Type) null && this.SubFlowField != (System.Type) null)
        {
          string flow = (string) cache.GetValue(entity, this.FlowField.Name);
          string subFlow = (string) cache.GetValue(entity, this.SubFlowField.Name);
          flag3 = !string.IsNullOrEmpty(workflowId) || !string.IsNullOrEmpty(workflowSubId) ? (!string.IsNullOrEmpty(workflowSubId) ? (!string.IsNullOrEmpty(workflowId) ? flow == workflowId && subFlow == workflowSubId : subFlow == workflowSubId && this.Flows.All<AUWorkflow>((Func<AUWorkflow, bool>) (f =>
          {
            if (f.WorkflowID != flow)
              return true;
            return f.WorkflowID == flow && f.WorkflowSubID != subFlow;
          }))) : flow == workflowId && this.Flows.All<AUWorkflow>((Func<AUWorkflow, bool>) (f =>
          {
            if (f.WorkflowSubID != subFlow)
              return true;
            return f.WorkflowSubID == subFlow && f.WorkflowID == null;
          }))) : this.Flows.All<AUWorkflow>((Func<AUWorkflow, bool>) (f => f.WorkflowID != flow || f.WorkflowSubID != subFlow));
        }
        else if (this.FlowField != (System.Type) null)
        {
          string flow = (string) cache.GetValue(entity, this.FlowField.Name);
          flag3 = !string.IsNullOrEmpty(workflowId) ? flow == workflowId : this.Flows.All<AUWorkflow>((Func<AUWorkflow, bool>) (f => f.WorkflowID != flow));
        }
        flag1 |= flag2 & flag3;
      }
      return new bool?(flag1);
    }

    public static bool? Get(
      string screenID,
      string actionName,
      Func<string, System.Type> fieldNameToBqlField,
      out WorkflowAction.WorkflowActionChecker workflow)
    {
      workflow = (WorkflowAction.WorkflowActionChecker) null;
      IAUWorkflowEngine auWorkflowEngine = (IAUWorkflowEngine) null;
      IAUWorkflowActionsEngine workflowActionsEngine = (IAUWorkflowActionsEngine) null;
      if (ServiceLocator.IsLocationProviderSet)
      {
        auWorkflowEngine = ServiceLocator.Current.GetInstance<IAUWorkflowEngine>();
        workflowActionsEngine = ServiceLocator.Current.GetInstance<IAUWorkflowActionsEngine>();
      }
      AUWorkflowDefinition screenWorkflows = auWorkflowEngine?.GetScreenWorkflows(screenID);
      if (screenWorkflows == null)
        return new bool?();
      IEnumerable<(string, string, string)> statesWithAction = workflowActionsEngine?.GetStatesWithAction(screenID, actionName);
      if ((statesWithAction != null ? (!statesWithAction.Any<(string, string, string)>() ? 1 : 0) : 1) != 0)
        return new bool?();
      IEnumerable<AUWorkflow> allFlows = auWorkflowEngine.GetAllFlows(screenID);
      System.Type stateField = fieldNameToBqlField(screenWorkflows.StateField);
      System.Type flowField = screenWorkflows.FlowTypeField != null ? fieldNameToBqlField(screenWorkflows.FlowTypeField) : (System.Type) null;
      System.Type subFlowField = screenWorkflows.FlowSubTypeField != null ? fieldNameToBqlField(screenWorkflows.FlowSubTypeField) : (System.Type) null;
      workflow = new WorkflowAction.WorkflowActionChecker(flowField, subFlowField, allFlows, stateField, statesWithAction);
      return new bool?(true);
    }
  }
}
