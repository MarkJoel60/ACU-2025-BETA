// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMExpressionContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;
using System;

#nullable disable
namespace PX.Objects.PM;

public class PMExpressionContext : ExpressionContext
{
  protected IRateTable engine;

  public PMExpressionContext(IRateTable engine) => this.engine = engine;

  public virtual object Evaluate(PMNameNode node, PMTran row)
  {
    return node.IsAttribute ? this.engine.Evaluate(node.ObjectName, (string) null, node.FieldName, row) : this.engine.Evaluate(node.ObjectName, node.FieldName, (string) null, row);
  }

  public virtual Decimal? GetPrice(PMTran row) => this.engine.GetPrice(row);

  public virtual Decimal? ConvertAmountToCurrency(
    FunctionContext context,
    string fromCuryID,
    string toCuryID,
    string rateType,
    DateTime? effectiveDate,
    Decimal? value)
  {
    return this.engine.ConvertAmountToCurrency(fromCuryID, toCuryID, rateType, effectiveDate, value);
  }
}
