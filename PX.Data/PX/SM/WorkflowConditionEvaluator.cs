// Decompiled with JetBrains decompiler
// Type: PX.SM.WorkflowConditionEvaluator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using System;

#nullable disable
namespace PX.SM;

internal class WorkflowConditionEvaluator : IWorkflowCondition
{
  private readonly Readonly.Condition condition;
  public IBqlTable Record;

  public WorkflowConditionEvaluator(Readonly.Condition condition) => this.condition = condition;

  public bool Eval(PXCache cache, object current)
  {
    if (this.condition.Constant.HasValue)
      return this.condition.Constant.Value;
    if (this.condition.BqlExpression != null)
      return this.Eval(this.condition.BqlExpression, cache, current);
    if (this.condition.Lambda != null)
      return this.condition.Lambda((IBqlTable) current);
    return this.condition.NamedCondition != null && this.condition.NamedCondition.Eval(cache, current);
  }

  private bool Eval(IBqlUnary bqlExpression, PXCache cache, object current)
  {
    bool? result = new bool?();
    object obj = (object) null;
    BqlFormula.Verify(cache, current, (IBqlCreator) bqlExpression, ref result, ref obj);
    bool? nullable = result;
    bool flag = true;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }

  public string DisplayName { get; set; }

  public string Name { get; set; }

  public string ConditionName => this.condition.Name;

  public Guid? Id => this.condition.Id;

  public static string GetShortName(Readonly.Condition condition) => (string) null;

  public string GetExpression()
  {
    if (this.condition.Constant.HasValue)
      return Convert.ToString(this.condition.Constant.Value);
    return this.condition.BqlExpression != null ? this.condition.BqlExpression.GetType().ToCodeString() : "Internal method";
  }
}
