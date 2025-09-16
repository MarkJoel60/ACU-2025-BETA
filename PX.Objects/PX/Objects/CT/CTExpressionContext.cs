// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.CTExpressionContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;
using PX.Data.Reports;

#nullable disable
namespace PX.Objects.CT;

public class CTExpressionContext : ExpressionContext
{
  protected IContractInformation engine;

  public SoapNavigator.DATA Data { get; protected set; }

  public CTExpressionContext(IContractInformation engine)
  {
    this.engine = engine;
    this.Data = new SoapNavigator.DATA();
  }

  public virtual object Evaluate(CTNameNode node, CTFormulaDescriptionContainer row)
  {
    return node.IsAttribute ? this.engine.Evaluate(node.ObjectName, (string) null, node.FieldName, row) : this.engine.Evaluate(node.ObjectName, node.FieldName, (string) null, row);
  }

  public virtual string GetParametrInventoryPrefix(CTFormulaDescriptionContainer row)
  {
    return this.engine.GetParametrInventoryPrefix(row);
  }

  public virtual string GetParametrActionInvoice(CTFormulaDescriptionContainer row)
  {
    return this.engine.GetParametrActionInvoice(row);
  }

  public virtual string GetParametrActionItem(CTFormulaDescriptionContainer row)
  {
    return this.engine.GetParametrActionItem(row);
  }
}
