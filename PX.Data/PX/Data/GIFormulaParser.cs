// Decompiled with JetBrains decompiler
// Type: PX.Data.GIFormulaParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Parser;
using System;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data;

internal class GIFormulaParser : ExpressionParser
{
  private readonly PXGenericInqGrph _graph;
  private readonly bool _useExt;
  private readonly bool _applyMask;

  public static object Evaluate(
    PXGenericInqGrph graph,
    GenericResult row,
    string formula,
    bool useExt,
    bool applyMask)
  {
    return GIFormulaParser.Parse(graph, formula, useExt, applyMask).Eval((object) row);
  }

  private static ExpressionNode Parse(
    PXGenericInqGrph graph,
    string formula,
    bool useExt,
    bool applyMask)
  {
    formula = !formula.StartsWith("=") ? $"[{formula}]" : formula.Substring(1);
    return new GIFormulaParser(graph, formula, useExt, applyMask).Parse();
  }

  protected GIFormulaParser(PXGenericInqGrph graph, string text, bool useExt, bool applyMask)
    : base(text)
  {
    this._graph = graph ?? throw new ArgumentNullException(nameof (graph));
    this._useExt = useExt;
    this._applyMask = applyMask;
  }

  protected virtual ParserContext CreateContext()
  {
    return (ParserContext) new GIFormulaParser.GIExpressionContext(this._graph, this._useExt, this._applyMask);
  }

  protected virtual NameNode CreateNameNode(ExpressionNode node, string tokenString)
  {
    return (NameNode) new GIFormulaParser.GINameNode(node, tokenString, (GIFormulaParser.GIExpressionContext) this.Context);
  }

  protected virtual void ValidateName(NameNode node, string tokenString)
  {
  }

  protected virtual bool IsAggregate(string nodeName) => false;

  public class GIExpressionContext : ExpressionContext
  {
    public GIExpressionContext(PXGenericInqGrph graph, bool useExt, bool applyMask)
    {
      this.DataGraph = graph ?? throw new ArgumentNullException(nameof (graph));
      this.UseExt = useExt;
      this.ApplyMask = applyMask;
    }

    public PXGenericInqGrph DataGraph { get; }

    public bool UseExt { get; }

    public bool ApplyMask { get; }

    public virtual bool SubstringIsOneBased => true;
  }

  public class GINameNode : NameNode
  {
    public GINameNode(
      ExpressionNode node,
      string name,
      GIFormulaParser.GIExpressionContext context)
      : base(node, name, (ParserContext) context)
    {
      if (string.IsNullOrEmpty(name))
        return;
      this.IsParameter = context.DataGraph.Filter.Cache.Fields.Any<string>((Func<string, bool>) (f => f == this.Name));
      this.IsRelativeDate = RelativeDatesManager.IsRelativeDatesString(this.Name);
      this.IsDataField = !this.IsRelativeDate && !this.IsParameter;
    }

    protected GIFormulaParser.GIExpressionContext Context
    {
      get => (GIFormulaParser.GIExpressionContext) this.context;
    }

    public bool IsRelativeDate { get; protected set; }

    public bool IsDataField { get; protected set; }

    public bool IsParameter { get; protected set; }

    public virtual object Eval(object row)
    {
      PXGenericInqGrph dataGraph = this.Context.DataGraph;
      object component = (object) this.context;
      if (this.node != null)
      {
        component = this.node.Eval(row);
      }
      else
      {
        if (this.IsRelativeDate)
          return (object) RelativeDatesManager.EvaluateAsDateTime(this.name);
        if (this.IsDataField)
        {
          if (row == null)
            return (object) null;
          PXCache cache = (PXCache) dataGraph.Results.Cache;
          string name = this.name;
          string[] strArray = name != null ? name.Split('.', 2) : (string[]) null;
          string fieldName = strArray == null || strArray.Length < 2 ? this.name : $"{strArray[0]}_{strArray[1]}";
          return this.FormatVal(this.Context.UseExt || dataGraph.IsVirtualField(fieldName) ? cache.GetValueExt(row, fieldName) : cache.GetValue(row, fieldName));
        }
        if (this.IsParameter)
          return this.EvalParameter(this.Name, row);
      }
      return this.FormatVal((TypeDescriptor.GetProperties(component).Find(this.name, true) ?? throw ExpressionException.UndefinedObject(this.name)).GetValue(component));
    }

    protected object EvalParameter(string parameterName, object row)
    {
      PXGenericInqGrph dataGraph = this.Context.DataGraph;
      if (dataGraph.Filter.Cache.Current == null)
        return (object) null;
      object obj = PXFieldState.UnwrapValue(dataGraph.Filter.Cache.GetValueExt((object) dataGraph.Filter.Current, parameterName));
      string str = obj as string;
      if (RelativeDatesManager.IsRelativeDatesString(str))
        obj = (object) RelativeDatesManager.EvaluateAsDateTime(str);
      return this.FormatVal(obj);
    }

    private object FormatVal(object value)
    {
      object obj = PXFieldState.UnwrapValue(value);
      if (this.Context.ApplyMask)
      {
        PXStringState pxStringState = value as PXStringState;
        PXDateState pxDateState = value as PXDateState;
        string str = pxStringState?.InputMask ?? pxDateState?.InputMask;
        if (!string.IsNullOrEmpty(str))
          return this.FormatVal(obj, $"{{{str}}}");
      }
      return base.FormatVal(obj);
    }
  }
}
