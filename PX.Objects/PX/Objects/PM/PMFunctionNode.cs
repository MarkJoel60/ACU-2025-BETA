// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMFunctionNode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;

#nullable disable
namespace PX.Objects.PM;

public class PMFunctionNode : FunctionNode
{
  private PMExpressionContext context;

  public PMFunctionNode(ExpressionNode node, string name, PMExpressionContext context)
    : base(node, name, (ParserContext) context)
  {
    this.context = context;
  }
}
