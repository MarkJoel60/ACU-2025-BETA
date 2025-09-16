// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAFormulaCalculationContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CAFormulaCalculationContext : IDisposable
{
  private CABatchEntry graph { get; set; }

  public CAFormulaCalculationContext(CABatchEntry graph)
  {
    this.graph = graph;
    ((PXSelectBase) graph.AddendaInfo).AllowSelect = true;
  }

  public void Dispose() => ((PXSelectBase) this.graph.AddendaInfo).AllowSelect = true;
}
