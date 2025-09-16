// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.ARGLDiscrepancyEnqGraphBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;

#nullable disable
namespace ReconciliationTools;

[TableAndChartDashboardType]
public class ARGLDiscrepancyEnqGraphBase<TGraph, TEnqFilter, TEnqResult> : 
  DiscrepancyEnqGraphBase<TGraph, TEnqFilter, TEnqResult>
  where TGraph : PXGraph
  where TEnqFilter : DiscrepancyEnqFilter, new()
  where TEnqResult : class, IBqlTable, IDiscrepancyEnqResult, new()
{
  protected override Decimal CalcGLTurnover(GLTran tran)
  {
    Decimal? nullable = tran.DebitAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = tran.CreditAmt;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return valueOrDefault1 - valueOrDefault2;
  }
}
