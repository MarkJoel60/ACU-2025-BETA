// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMNameNode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;
using System;

#nullable disable
namespace PX.Objects.PM;

public class PMNameNode : NameNode
{
  protected bool isAttribute;

  public PMObjectType ObjectName { get; protected set; }

  public string FieldName { get; protected set; }

  public PMNameNode(ExpressionNode node, string tokenString, ParserContext context)
    : base(node, tokenString, context)
  {
    string[] strArray = this.Name.Split('.');
    if (strArray.Length == 3)
    {
      this.isAttribute = true;
      this.ObjectName = (PMObjectType) Enum.Parse(typeof (PMObjectType), strArray[0], true);
      this.FieldName = strArray[2].Trim('[', ']').Trim();
    }
    else if (strArray.Length == 2)
    {
      this.ObjectName = (PMObjectType) Enum.Parse(typeof (PMObjectType), strArray[0], true);
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
      this.ObjectName = PMObjectType.PMTran;
      this.FieldName = this.Name;
    }
  }

  protected bool IsRate
  {
    get => this.name.StartsWith("@Rate", StringComparison.InvariantCultureIgnoreCase);
  }

  protected bool IsPrice
  {
    get => this.name.StartsWith("@Price", StringComparison.InvariantCultureIgnoreCase);
  }

  public bool IsAttribute => this.isAttribute;

  public virtual object Eval(object row)
  {
    if (this.IsRate)
      return (object) ((PMTran) row).Rate;
    return this.IsPrice ? (object) ((PMExpressionContext) this.context).GetPrice((PMTran) row) : ((PMExpressionContext) this.context).Evaluate(this, (PMTran) row);
  }
}
