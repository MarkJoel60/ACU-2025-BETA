// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProductLinesSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ProductLinesSelect : 
  PXOrderedSelect<PMQuote, CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Current<PMQuote.quoteID>>>, OrderBy<Asc<CROpportunityProducts.sortOrder>>>
{
  public ProductLinesSelect(PXGraph graph)
    : base(graph)
  {
  }

  public ProductLinesSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }
}
