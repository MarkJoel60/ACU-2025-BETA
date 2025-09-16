// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRetainedTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class PMRetainedTaxAttribute : PMTaxAttribute
{
  public PMRetainedTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this.CuryLineTotal = typeof (PMProformaLine.curyRetainage);
    this.CuryTranAmt = typeof (PMProformaLine.curyRetainage);
    this.CuryDocBal = (Type) null;
    this.CuryTaxTotal = typeof (PMProforma.curyRetainageTaxTotal);
    this.CuryTaxInclTotal = typeof (PMProforma.curyRetainageTaxInclTotal);
    this.DocDate = typeof (PMProforma.invoiceDate);
    this._CuryTaxableAmt = typeof (PMTax.curyRetainedTaxableAmt).Name;
    this._CuryTaxAmt = typeof (PMTax.curyRetainedTaxAmt).Name;
    this.SortOrder = 1;
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType = "I")
  {
    PMProformaLine pmProformaLine = (PMProformaLine) row;
    return this.IsRetainedTaxes(sender.Graph) ? pmProformaLine.CuryRetainage : new Decimal?(0M);
  }

  protected override List<object> SelectTaxes<WhereType>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return !this.IsRetainedTaxes(graph) ? new List<object>() : base.SelectTaxes<WhereType>(graph, row, taxchk, parameters);
  }

  protected override bool IsRetainedTaxes(PXGraph graph)
  {
    ARSetup current = graph.Caches[typeof (ARSetup)].Current as ARSetup;
    return PXAccess.FeatureInstalled<FeaturesSet.retainage>() && current != null && current.RetainTaxes.GetValueOrDefault();
  }

  protected override void ReDefaultTaxes(PXCache cache, List<object> details)
  {
  }

  protected override void ReDefaultTaxes(
    PXCache cache,
    object clearDet,
    object defaultDet,
    bool defaultExisting = true)
  {
  }

  protected override void DefaultTaxes(PXCache sender, object row, bool DefaultExisting)
  {
    if (!this.IsRetainedTaxes(sender.Graph))
      return;
    base.DefaultTaxes(sender, row, DefaultExisting);
  }
}
