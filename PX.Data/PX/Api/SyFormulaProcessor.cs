// Decompiled with JetBrains decompiler
// Type: PX.Api.SyFormulaProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Parser;
using System.Collections.Generic;

#nullable disable
namespace PX.Api;

[PXInternalUseOnly]
public class SyFormulaProcessor
{
  private readonly Dictionary<string, ExpressionNode> Cache = new Dictionary<string, ExpressionNode>();

  public object Evaluate(string formula, SyFormulaFinalDelegate getter)
  {
    return this.Evaluate(formula, getter, out bool _);
  }

  internal object Evaluate(
    string formula,
    SyFormulaFinalDelegate getter,
    out bool isNullOperatorResult)
  {
    isNullOperatorResult = false;
    switch (formula)
    {
      case null:
        return (object) null;
      case "":
        return getter(formula);
      default:
        if (formula[0] == '=')
        {
          ExpressionNode expression = this.GetExpression(formula.Substring(1));
          object obj = expression.Eval((object) getter);
          isNullOperatorResult = expression.IsNullOperatorResult;
          return obj;
        }
        goto case "";
    }
  }

  private ExpressionNode GetExpression(string formula)
  {
    ExpressionNode expression;
    if (!this.Cache.TryGetValue(formula, out expression))
    {
      expression = SyExpressionParser.Parse(formula);
      this.Cache[formula] = expression;
    }
    return expression;
  }
}
