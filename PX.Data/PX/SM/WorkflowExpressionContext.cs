// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowExpressionContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Parser;
using PX.Data;
using PX.Data.ProjectDefinition.Workflow;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class WorkflowExpressionContext : ExpressionContext
{
  private readonly IReadOnlyDictionary<string, object> _formValues;
  private readonly bool _useGetExtValue;

  public string FieldName { get; }

  public WorkflowExpressionContext(
    IReadOnlyDictionary<string, object> formValues,
    PXCache cache,
    string fieldName,
    bool useGetExtValue = false)
  {
    this.FieldName = fieldName;
    this._formValues = formValues;
    this._useGetExtValue = useGetExtValue;
    this.Cache = cache;
  }

  public object GetFormValue(string name)
  {
    object obj;
    return this._formValues != null && this._formValues.TryGetValue(name, out obj) ? obj : (object) null;
  }

  public object GetFormCache(string name, object row)
  {
    return !this._useGetExtValue && !this.Cache.IsKvExtAttribute(name) ? this.Cache.GetValue(row, name) : PXFieldState.UnwrapValue(this.Cache.GetValueExt(row, name));
  }

  public PXCache Cache { get; }

  public object GetGraphParameterValue(string name)
  {
    ExpressionParameterInfo expressionParameterInfo;
    return this.Cache.Graph.GetAvailableExpressionParameters().TryGetValue(name, out expressionParameterInfo) ? expressionParameterInfo.Value : (object) null;
  }
}
