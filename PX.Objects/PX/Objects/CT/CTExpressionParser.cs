// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.CTExpressionParser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;
using PX.Reports.Parser;

#nullable disable
namespace PX.Objects.CT;

public class CTExpressionParser : ExpressionParser
{
  public IContractInformation Engine { get; protected set; }

  private CTExpressionParser(IContractInformation engine, string text)
    : base(text)
  {
    this.Engine = engine;
  }

  protected virtual ParserContext CreateContext()
  {
    return (ParserContext) new CTExpressionContext(this.Engine);
  }

  protected virtual NameNode CreateNameNode(ExpressionNode node, string tokenString)
  {
    return (NameNode) new CTNameNode(node, tokenString, this.Context);
  }

  protected virtual void ValidateName(NameNode node, string tokenString)
  {
  }

  protected virtual bool IsAggregate(string nodeName) => ReportAggregateNode.IsAggregate(nodeName);

  protected virtual AggregateNode CreateAggregateNode(string name, string dataField)
  {
    return (AggregateNode) new ReportAggregateNode(name, dataField);
  }

  public static ExpressionNode Parse(IContractInformation engine, string formula)
  {
    if (formula.StartsWith("="))
      formula = formula.Substring(1);
    return new CTExpressionParser(engine, formula).Parse();
  }
}
