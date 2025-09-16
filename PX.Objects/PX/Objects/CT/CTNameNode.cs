// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.CTNameNode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;
using System;

#nullable disable
namespace PX.Objects.CT;

public class CTNameNode : NameNode
{
  protected bool isAttribute;

  public CTObjectType ObjectName { get; protected set; }

  public string FieldName { get; protected set; }

  public CTNameNode(ExpressionNode node, string tokenString, ParserContext context)
    : base(node, tokenString, context)
  {
    string[] strArray = this.Name.Split('.');
    if (strArray.Length == 3)
    {
      this.isAttribute = true;
      this.ObjectName = (CTObjectType) Enum.Parse(typeof (CTObjectType), strArray[0], true);
      this.FieldName = strArray[2].Trim('[', ']').Trim();
    }
    else if (strArray.Length == 2)
    {
      this.ObjectName = (CTObjectType) Enum.Parse(typeof (CTObjectType), strArray[0], true);
      if (strArray[1].Trim().EndsWith("_Attributes"))
      {
        this.isAttribute = true;
        this.FieldName = strArray[1].Substring(0, strArray[1].Length - 11);
      }
      else
        this.FieldName = strArray[1];
    }
    else
    {
      this.ObjectName = CTObjectType.Contract;
      this.FieldName = this.Name;
    }
  }

  protected bool IsActionInvoice
  {
    get => this.name.StartsWith("@ActionInvoice", StringComparison.InvariantCultureIgnoreCase);
  }

  protected bool IsReport
  {
    get => this.name.StartsWith("Report", StringComparison.InvariantCultureIgnoreCase);
  }

  protected bool IsActionItem
  {
    get => this.name.StartsWith("@ActionItem", StringComparison.InvariantCultureIgnoreCase);
  }

  protected bool IsPrefix
  {
    get => this.name.StartsWith("@Prefix", StringComparison.InvariantCultureIgnoreCase);
  }

  public bool IsAttribute => this.isAttribute;

  public virtual object Eval(object row)
  {
    if (this.IsActionInvoice)
      return (object) ((CTExpressionContext) this.context).GetParametrActionInvoice((CTFormulaDescriptionContainer) row);
    if (this.IsActionItem)
      return (object) ((CTExpressionContext) this.context).GetParametrActionItem((CTFormulaDescriptionContainer) row);
    if (this.IsPrefix)
      return (object) ((CTExpressionContext) this.context).GetParametrInventoryPrefix((CTFormulaDescriptionContainer) row);
    return this.IsReport ? (object) ((CTExpressionContext) this.context).Data : ((CTExpressionContext) this.context).Evaluate(this, (CTFormulaDescriptionContainer) row);
  }
}
