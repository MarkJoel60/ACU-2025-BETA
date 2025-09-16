// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowConditionEvaluateService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.Automation.Services;
using PX.Data.Automation.State;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

/// <inheritdoc />
internal class WorkflowConditionEvaluateService : IWorkflowConditionEvaluateService
{
  private readonly IScreenToGraphWorkflowMappingService _screenToGraphWorkflowMappingService;
  private readonly IConditionEvaluator _conditionEvaluator;

  public WorkflowConditionEvaluateService(
    IScreenToGraphWorkflowMappingService screenToGraphWorkflowMappingService,
    IConditionEvaluator conditionEvaluator)
  {
    this._screenToGraphWorkflowMappingService = screenToGraphWorkflowMappingService;
    this._conditionEvaluator = conditionEvaluator ?? throw new ArgumentNullException(nameof (conditionEvaluator));
  }

  public IReadOnlyDictionary<string, Lazy<bool>> EvaluateConditions(
    PXGraph graph,
    object row,
    Screen screen,
    string form,
    IReadOnlyDictionary<string, object> formValues,
    PXCache cache = null,
    string screenId = null)
  {
    Dictionary<string, Lazy<bool>> result = new Dictionary<string, Lazy<bool>>()
    {
      {
        bool.TrueString,
        new Lazy<bool>((Func<bool>) (() => true))
      },
      {
        bool.FalseString,
        new Lazy<bool>((Func<bool>) (() => false))
      }
    };
    PXView primaryView = !string.IsNullOrEmpty(graph.PrimaryView) ? graph.Views[graph.PrimaryView] : graph.Views.FirstOrDefault<KeyValuePair<string, PXView>>().Value;
    if (cache == null)
      cache = graph.Caches[graph.PrimaryItemType];
    if (screen == null)
      return (IReadOnlyDictionary<string, Lazy<bool>>) result;
    string screenID = string.IsNullOrEmpty(screenId) ? this._screenToGraphWorkflowMappingService.GetScreenIDFromGraphType(graph.GetType()) : screenId;
    this.EvaluateConditionsInternal(graph, row, screen, form, result, screenID, cache, primaryView, formValues);
    return (IReadOnlyDictionary<string, Lazy<bool>>) result;
  }

  private void EvaluateConditionsInternal(
    PXGraph graph,
    object row,
    Screen screen,
    string form,
    Dictionary<string, Lazy<bool>> result,
    string screenID,
    PXCache cache,
    PXView primaryView,
    IReadOnlyDictionary<string, object> formValues)
  {
    foreach (StateMap<ScreenCondition>.NamedState condition in screen.Conditions.GetList())
      this.EvaluateCondition(graph, row, form, condition, result, screenID, cache, primaryView, formValues);
  }

  private void EvaluateCondition(
    PXGraph graph,
    object row,
    string form,
    StateMap<ScreenCondition>.NamedState condition,
    Dictionary<string, Lazy<bool>> result,
    string screenID,
    PXCache cache,
    PXView primaryView,
    IReadOnlyDictionary<string, object> formValues)
  {
    ScreenCondition c = condition.Value;
    if (c.InternalImplementation != null)
    {
      if (result.ContainsKey(c.InternalImplementation.Name))
      {
        result[condition.Name] = result[c.InternalImplementation.Name];
      }
      else
      {
        Lazy<bool> lazy = Lazy.By<bool>((Func<bool>) (() =>
        {
          bool condition1 = c.InternalImplementation.Eval(cache, row);
          if (c.InvertCondition)
            condition1 = !condition1;
          return condition1;
        }));
        result[condition.Name] = lazy;
        result[c.InternalImplementation.Name] = lazy;
      }
    }
    else if (condition.Value.Filters.All<ScreenConditionFilter>((Func<ScreenConditionFilter, bool>) (f => !f.FieldName.StartsWith("@"))))
    {
      Lazy<bool> lazy1 = Lazy.By<bool>((Func<bool>) (() =>
      {
        if (c.ParentCondition != null)
        {
          Lazy<bool> lazy2;
          Lazy<bool> lazy3;
          if (result.TryGetValue(c.ParentCondition, out lazy2))
          {
            lazy3 = lazy2;
          }
          else
          {
            lazy2 = Lazy.By<bool>((Func<bool>) (() => PXSystemWorkflows.Definition.SystemWorkflowContainer.SystemConditions[$"{screenID}_{c.ParentCondition}"].Eval(cache, row)));
            lazy3 = lazy2;
            result[c.ParentCondition] = lazy2;
          }
          bool? nullable = new bool?();
          bool? conditionMethodAnd1 = c.ParentConditionMethodAnd;
          bool flag1 = true;
          if (conditionMethodAnd1.GetValueOrDefault() == flag1 & conditionMethodAnd1.HasValue && !lazy3.Value)
            nullable = new bool?(false);
          bool? conditionMethodAnd2 = c.ParentConditionMethodAnd;
          bool flag2 = false;
          if (conditionMethodAnd2.GetValueOrDefault() == flag2 & conditionMethodAnd2.HasValue && lazy3.Value)
            nullable = new bool?(true);
          if (nullable.HasValue)
            return nullable.Value;
        }
        try
        {
          List<PXFilterRow> filters = new List<PXFilterRow>();
          foreach (ScreenConditionFilter filter in condition.Value.Filters)
          {
            object obj1 = (object) filter.Value;
            object obj2 = (object) filter.Value2;
            if (filter.IsExpression)
            {
              obj1 = this.EvalWorkflowExpression(graph, row, form, cache, obj1, filter.FieldName, formValues);
              obj2 = this.EvalWorkflowExpression(graph, row, form, cache, obj2, filter.FieldName, formValues);
            }
            else
            {
              if (obj1 != null && RelativeDatesManager.IsRelativeDatesString(obj1.ToString()))
                obj1 = (object) RelativeDatesManager.EvaluateAsDateTime(obj1.ToString());
              if (obj2 != null && RelativeDatesManager.IsRelativeDatesString(obj2.ToString()))
                obj2 = (object) RelativeDatesManager.EvaluateAsDateTime(obj2.ToString());
            }
            FilterVariableType? variable = new FilterVariableType?();
            if (filter.Value != null)
            {
              switch (filter.Value)
              {
                case "@me":
                  variable = new FilterVariableType?(FilterVariableType.CurrentUser);
                  break;
                case "@mygroups":
                  variable = new FilterVariableType?(FilterVariableType.CurrentUserGroups);
                  break;
                case "@myworktree":
                  variable = new FilterVariableType?(FilterVariableType.CurrentUserGroupsTree);
                  break;
                case "@branch":
                  variable = new FilterVariableType?(FilterVariableType.CurrentBranch);
                  break;
                case "@company":
                  variable = new FilterVariableType?(FilterVariableType.CurrentOrganization);
                  break;
              }
            }
            PXFilterRow pxFilterRow = new PXFilterRow(filter.FieldName, (PXCondition) (filter.Condition - 1), obj1, obj2, variable)
            {
              OpenBrackets = filter.OpenBrackets,
              CloseBrackets = filter.CloseBrackets,
              OrOperator = filter.Operator == 1
            };
            filters.Add(pxFilterRow);
          }
          bool condition2 = this._conditionEvaluator.Evaluate(graph, primaryView, row, (IEnumerable<PXFilterRow>) filters);
          if (c.InvertCondition)
            condition2 = !condition2;
          return condition2;
        }
        catch
        {
          return false;
        }
      }));
      result[condition.Name] = lazy1;
    }
    else
    {
      Lazy<bool> lazy4 = Lazy.By<bool>((Func<bool>) (() =>
      {
        if (c.ParentCondition != null)
        {
          Lazy<bool> lazy5;
          Lazy<bool> lazy6;
          if (result.TryGetValue(c.ParentCondition, out lazy5))
          {
            lazy6 = lazy5;
          }
          else
          {
            lazy5 = Lazy.By<bool>((Func<bool>) (() => PXSystemWorkflows.Definition.SystemWorkflowContainer.SystemConditions[$"{screenID}_{c.ParentCondition}"].Eval(cache, row)));
            lazy6 = lazy5;
            result[c.ParentCondition] = lazy5;
          }
          bool? nullable = new bool?();
          bool? conditionMethodAnd3 = c.ParentConditionMethodAnd;
          bool flag3 = true;
          if (conditionMethodAnd3.GetValueOrDefault() == flag3 & conditionMethodAnd3.HasValue && !lazy6.Value)
            nullable = new bool?(false);
          bool? conditionMethodAnd4 = c.ParentConditionMethodAnd;
          bool flag4 = false;
          if (conditionMethodAnd4.GetValueOrDefault() == flag4 & conditionMethodAnd4.HasValue && lazy6.Value)
            nullable = new bool?(true);
          if (nullable.HasValue)
            return nullable.Value;
        }
        string formula = "";
        for (int index1 = 0; index1 < condition.Value.Filters.Count; ++index1)
        {
          ScreenConditionFilter filter = condition.Value.Filters[index1];
          string str1 = filter.FieldName;
          if (!str1.StartsWith("@"))
            str1 = $"[{str1}]";
          string str2 = "";
          for (int index2 = 0; index2 < filter.OpenBrackets; ++index2)
            str2 += " ( ";
          string str3 = str2 + " ( ";
          switch (filter.Condition)
          {
            case 1:
              str3 = $"{str3}{str1} = {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ";
              break;
            case 2:
              str3 = $"{str3}{str1} <> {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ";
              break;
            case 3:
              str3 = $"{str3}{str1} > {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ";
              break;
            case 4:
              str3 = $"{str3}{str1} >= {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ";
              break;
            case 5:
              str3 = $"{str3}{str1} < {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ";
              break;
            case 6:
              str3 = $"{str3}{str1} >= {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ";
              break;
            case 7:
              str3 = $"{str3} InStr( {str1} , {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)}) >= 0 ";
              break;
            case 8:
              str3 = $"{str3} InStrRev( {str1} , {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)}) = ( Len( {str1} ) - Len( {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ) ) ";
              break;
            case 9:
              str3 = $"{str3} InStr( {str1} , {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)}) = 0 ";
              break;
            case 10:
              str3 = $"{str3} InStr( {str1} , {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)}) < 0 ";
              break;
            case 11:
              str3 = $"{str3} {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} <= {str1} <= {this.FormulaValueConverter((object) filter.Value2, filter.IsExpression)} ";
              break;
            case 12:
              str3 = $"{str3} {str1} = Null ";
              break;
            case 13:
              str3 = $"{str3} {str1} <> Null ";
              break;
            case 22:
              str3 = $"{str3} {str1} In ( {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} ) ";
              break;
            case 23:
              str3 = $"{str3} Not ( {str1} In ( {this.FormulaValueConverter((object) filter.Value, filter.IsExpression)} )) ";
              break;
          }
          string str4 = str3 + " ) ";
          for (int index3 = 0; index3 < filter.CloseBrackets; ++index3)
            str4 += " ) ";
          if (index1 != condition.Value.Filters.Count - 1)
            str4 += filter.Operator == 1 ? " Or " : " And ";
          formula += str4;
        }
        try
        {
          bool condition3 = (bool) WorkflowExpressionParser.Eval(cache, (string) null, formula, formValues, row);
          if (c.InvertCondition)
            condition3 = !condition3;
          return condition3;
        }
        catch
        {
          return false;
        }
      }));
      result[condition.Name] = lazy4;
    }
  }

  private object EvalWorkflowExpression(
    PXGraph graph,
    object row,
    string form,
    PXCache cache,
    object value,
    string fieldName,
    IReadOnlyDictionary<string, object> formValues)
  {
    try
    {
      return value != null ? WorkflowExpressionParser.Eval(cache, fieldName, value.ToString(), formValues, row) : (object) null;
    }
    catch
    {
      return value;
    }
  }

  private string FormulaValueConverter(object value, bool isExpression)
  {
    return isExpression ? (value == null ? "Null" : value.ToString()) : (value == null ? "Null" : $"'{value?.ToString()}'");
  }
}
