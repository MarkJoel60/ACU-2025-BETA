// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowExpressionParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Parser;
using PX.Data;
using PX.Data.ProjectDefinition.Workflow;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class WorkflowExpressionParser : ExpressionParser
{
  private readonly string _fieldName;
  private readonly IReadOnlyDictionary<string, object> _formValues;
  private readonly PXCache _cache;
  private readonly bool _useGetExtValue;

  public WorkflowExpressionParser(
    string text,
    string fieldName,
    IReadOnlyDictionary<string, object> formValues,
    PXCache cache,
    bool useGetExtValue = false)
    : base(text)
  {
    this._fieldName = fieldName;
    this._formValues = formValues;
    this._cache = cache;
    this._useGetExtValue = useGetExtValue;
  }

  protected virtual ParserContext CreateContext()
  {
    return (ParserContext) new WorkflowExpressionContext(this._formValues, this._cache, this._fieldName, this._useGetExtValue);
  }

  protected virtual NameNode CreateNameNode(ExpressionNode node, string tokenString)
  {
    return (NameNode) new WorkflowNameNode(node, tokenString, (WorkflowExpressionContext) this.Context);
  }

  protected virtual void ValidateName(NameNode node, string tokenString)
  {
    throw new NotImplementedException();
  }

  protected virtual bool IsAggregate(string nodeName) => false;

  public static object Eval(
    PXCache cache,
    string fieldName,
    string formula,
    IReadOnlyDictionary<string, object> formValues,
    object row,
    bool useGetExtValue = false)
  {
    string str;
    if (!formula.StartsWith("="))
      str = formula;
    else
      str = formula.TrimStart('=');
    string text = str;
    if (string.IsNullOrEmpty(text))
      return (object) text;
    object returnValue = new WorkflowExpressionParser(text, fieldName, formValues, cache, useGetExtValue).Parse().Eval(row);
    if (fieldName != null && returnValue != null)
    {
      if (!fieldName.StartsWith("@"))
      {
        if (cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt && stateExt.DataType != returnValue.GetType())
        {
          cache.RaiseFieldSelecting(fieldName, (object) null, ref returnValue, false);
          returnValue = PXFieldState.UnwrapValue(returnValue);
        }
      }
      else
      {
        ExpressionParameterInfo expressionParameterInfo;
        if (cache.Graph.GetAvailableExpressionParameters().TryGetValue(fieldName.Substring(1), out expressionParameterInfo) && expressionParameterInfo.Cache != null && !string.IsNullOrEmpty(expressionParameterInfo.FieldName) && expressionParameterInfo.Cache.GetValueExt((object) null, expressionParameterInfo.FieldName) is PXFieldState valueExt && valueExt.DataType != returnValue.GetType())
        {
          expressionParameterInfo.Cache.RaiseFieldSelecting(expressionParameterInfo.FieldName, (object) null, ref returnValue, false);
          returnValue = PXFieldState.UnwrapValue(returnValue);
        }
      }
    }
    return returnValue;
  }
}
