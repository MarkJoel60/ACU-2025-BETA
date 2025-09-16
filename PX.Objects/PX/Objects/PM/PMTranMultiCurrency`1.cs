// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTranMultiCurrency`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.PM;

public abstract class PMTranMultiCurrency<TGraph> : MultiCurrencyGraph<TGraph, PMTran> where TGraph : PXGraph
{
  public bool UseDocumentRowInsertingFromBase { get; set; }

  protected override MultiCurrencyGraph<TGraph, PMTran>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<TGraph, PMTran>.CurySourceMapping(typeof (Company))
    {
      CuryID = typeof (Company.baseCuryID)
    };
  }

  protected override bool AllowOverrideCury() => true;

  protected override MultiCurrencyGraph<TGraph, PMTran>.DocumentMapping GetDocumentMapping()
  {
    return new MultiCurrencyGraph<TGraph, PMTran>.DocumentMapping(typeof (PMTran))
    {
      BAccountID = typeof (PMTran.bAccountID),
      BranchID = typeof (PMTran.branchID),
      CuryInfoID = typeof (PMTran.baseCuryInfoID),
      CuryID = typeof (PMTran.tranCuryID),
      DocumentDate = typeof (PMTran.date)
    };
  }

  protected override void DocumentRowInserting<CuryInfoID, CuryID>(PXCache sender, object row)
  {
    if (this.UseDocumentRowInsertingFromBase)
    {
      base.DocumentRowInserting<CuryInfoID, CuryID>(sender, row);
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo info = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(new PX.Objects.CM.Extensions.CurrencyInfo());
      ((PXSelectBase) this.currencyinfo).Cache.IsDirty = false;
      if (info == null)
        return;
      sender.SetValue<CuryInfoID>(row, (object) info.CuryInfoID);
      this.defaultCurrencyRate(((PXSelectBase) this.currencyinfo).Cache, info, true, true);
      sender.SetValue<CuryID>(row, (object) info.CuryID);
    }
  }

  protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID> e)
  {
    long? curyInfoId = e.Row.CuryInfoID;
    PMTran pmTran = (PMTran) e.Row.Base;
    long? nullable1 = curyInfoId;
    long? baseCuryInfoId = pmTran.BaseCuryInfoID;
    if (!(nullable1.GetValueOrDefault() == baseCuryInfoId.GetValueOrDefault() & nullable1.HasValue == baseCuryInfoId.HasValue))
    {
      long? nullable2 = curyInfoId;
      long? projectCuryInfoId = pmTran.ProjectCuryInfoID;
      if (!(nullable2.GetValueOrDefault() == projectCuryInfoId.GetValueOrDefault() & nullable2.HasValue == projectCuryInfoId.HasValue))
        return;
    }
    base._(e);
  }
}
