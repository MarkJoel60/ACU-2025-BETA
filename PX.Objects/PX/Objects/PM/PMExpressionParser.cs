// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMExpressionParser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;
using PX.Reports.Parser;

#nullable disable
namespace PX.Objects.PM;

public class PMExpressionParser : ExpressionParser
{
  public IRateTable Engine { get; protected set; }

  private PMExpressionParser(IRateTable engine, string text)
    : base(text)
  {
    this.Engine = engine;
  }

  protected virtual ParserContext CreateContext()
  {
    return (ParserContext) new PMExpressionContext(this.Engine);
  }

  protected virtual NameNode CreateNameNode(ExpressionNode node, string tokenString)
  {
    return (NameNode) new PMNameNode(node, tokenString, this.Context);
  }

  protected virtual void ValidateName(NameNode node, string tokenString)
  {
  }

  protected virtual bool IsAggregate(string nodeName) => ReportAggregateNode.IsAggregate(nodeName);

  protected virtual AggregateNode CreateAggregateNode(string name, string dataField)
  {
    return (AggregateNode) new ReportAggregateNode(name, dataField);
  }

  public static ExpressionNode Parse(IRateTable engine, string formula)
  {
    if (formula.StartsWith("="))
      formula = formula.Substring(1);
    return new PMExpressionParser(engine, formula).Parse();
  }

  protected virtual FunctionNode CreateFunctionNode(ExpressionNode node, string name)
  {
    return (FunctionNode) new PMFunctionNode((ExpressionNode) null, name, (PMExpressionContext) this.Context);
  }
}
