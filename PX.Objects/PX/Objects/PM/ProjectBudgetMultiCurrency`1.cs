// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectBudgetMultiCurrency`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public abstract class ProjectBudgetMultiCurrency<TGraph> : MultiCurrencyGraph<TGraph, PMProject> where TGraph : PXGraph
{
  protected override string Module => "PM";

  protected override MultiCurrencyGraph<TGraph, PMProject>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<TGraph, PMProject>.CurySourceMapping(typeof (PMProject));
  }

  protected override MultiCurrencyGraph<TGraph, PMProject>.DocumentMapping GetDocumentMapping()
  {
    return new MultiCurrencyGraph<TGraph, PMProject>.DocumentMapping(typeof (PMProject))
    {
      CuryInfoID = typeof (PMProject.curyInfoID)
    };
  }

  protected override bool ShouldMainCurrencyInfoBeReadonly() => true;

  protected override bool AllowOverrideCury() => false;

  protected override void CuryRowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e,
    List<CuryField> fields,
    Dictionary<Type, string> topCuryInfoIDs)
  {
    if (this.IsAccumulator(e.Row))
      return;
    base.CuryRowInserting(sender, e, fields, topCuryInfoIDs);
  }

  protected override void CuryRowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e,
    List<CuryField> fields)
  {
    if (this.IsAccumulator(e.Row))
      return;
    this.recalculateRowBaseValues(sender, e.Row, (IEnumerable<CuryField>) fields);
  }

  protected virtual bool IsAccumulator(object row) => row is PMBudgetAccum;
}
