// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiryNavigationExpressionEvaluator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Parser;
using PX.Data.Automation.Services;
using System;
using System.Linq;

#nullable disable
namespace PX.Data;

internal class GenericInquiryNavigationExpressionEvaluator : INavigationExpressionEvaluator
{
  public object Evaluate(
    PXGraph graph,
    object row,
    string expression,
    bool? isFromSchema = null,
    bool useExt = true,
    bool applyMask = false)
  {
    bool? nullable = isFromSchema;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      return (object) expression;
    return !(graph is PXGenericInqGrph graph1) ? (object) null : GenericInquiryNavigationExpressionEvaluator.NavigationExpressionParser.Evaluate(graph1, row as GenericResult, expression, useExt, applyMask);
  }

  /// <exclude />
  private class NavigationExpressionParser : GIFormulaParser
  {
    private NavigationExpressionParser(
      PXGenericInqGrph graph,
      string text,
      bool useExt,
      bool applyMask)
      : base(graph, text, useExt, applyMask)
    {
    }

    protected override NameNode CreateNameNode(ExpressionNode node, string tokenString)
    {
      return (NameNode) new GenericInquiryNavigationExpressionEvaluator.NavigationExpressionParser.NavigationNameNode(node, tokenString, (GIFormulaParser.GIExpressionContext) this.Context);
    }

    public new static object Evaluate(
      PXGenericInqGrph graph,
      GenericResult row,
      string formula,
      bool useExt,
      bool applyMask)
    {
      return GenericInquiryNavigationExpressionEvaluator.NavigationExpressionParser.Parse(graph, formula, useExt, applyMask).Eval((object) row);
    }

    private static ExpressionNode Parse(
      PXGenericInqGrph graph,
      string formula,
      bool useExt,
      bool applyMask)
    {
      formula = !formula.StartsWith("=") ? $"[{formula}]" : formula.Substring(1);
      return new GenericInquiryNavigationExpressionEvaluator.NavigationExpressionParser(graph, formula, useExt, applyMask).Parse();
    }

    private class NavigationNameNode : GIFormulaParser.GINameNode
    {
      public NavigationNameNode(
        ExpressionNode node,
        string name,
        GIFormulaParser.GIExpressionContext context)
        : base(node, name, context)
      {
        if (string.IsNullOrEmpty(name) || !name.StartsWith("@"))
          return;
        this.IsParameter = context.DataGraph.Filter.Cache.Fields.Any<string>((Func<string, bool>) (f => "@" + f == name));
        this.IsRelativeDate = !this.IsParameter && RelativeDatesManager.IsRelativeDatesString(this.Name);
        this.IsDataField = !this.IsParameter && !this.IsRelativeDate;
      }

      public override object Eval(object row)
      {
        return this.IsParameter && this.name.StartsWith("@") ? this.EvalParameter(this.name.Substring(1, this.name.Length - 1), row) : base.Eval(row);
      }
    }
  }
}
