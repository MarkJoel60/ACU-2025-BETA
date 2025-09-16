// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PerUnitTax.PerUnitTaxDataEntryGraphExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.TX;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.PerUnitTax;

/// <summary>
/// A per-unit tax graph extension for data entry graphs which will forbid edit of per-unit taxes in UI.
/// </summary>
public abstract class PerUnitTaxDataEntryGraphExtension<TGraph, TaxDetailDAC> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TaxDetailDAC : TaxDetail, IBqlTable, new()
{
  protected static bool IsActiveBase()
  {
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.perUnitTaxSupport>();
  }

  protected virtual void _(Events.RowSelected<TaxDetailDAC> e)
  {
    if ((object) e.Row == null)
      return;
    PXUIFieldAttribute pxuiFieldAttribute = e.Cache.GetAttributesOfType<PXUIFieldAttribute>((object) e.Row, "TaxID").FirstOrDefault<PXUIFieldAttribute>();
    if (pxuiFieldAttribute == null || !pxuiFieldAttribute.Enabled)
      return;
    Tax tax = this.GetTax(e.Row);
    this.ConfigureUI(e.Cache, e.Row, tax);
  }

  protected virtual void TaxDetailDAC_RowPersisting(Events.RowPersisting<TaxDetailDAC> e)
  {
    if (!((object) e.Row is TaxTran))
      return;
    Tax tax = this.GetTax(e.Row);
    if (tax == null)
      return;
    this.ConfigureChecks(e.Cache, e.Row, tax);
  }

  /// <summary>
  /// Gets a tax from <see cref="T:PX.Objects.TX.TaxDetail" />.
  /// </summary>
  /// <param name="taxDetail">The taxDetail to act on.</param>
  /// <returns />
  protected Tax GetTax(TaxDetailDAC taxDetail)
  {
    if ((object) taxDetail == null)
      return (Tax) null;
    return (Tax) PXSelectBase<Tax, PXSelect<Tax, Where<Tax.taxID, Equal<Required<Tax.taxID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) taxDetail.TaxID);
  }

  protected virtual void ConfigureUI(PXCache cache, TaxDetailDAC taxDetail, Tax tax)
  {
    bool flag = tax?.TaxType == "Q";
    PXUIFieldAttribute.SetEnabled(cache, (object) taxDetail, !flag);
  }

  protected virtual void ConfigureChecks(PXCache cache, TaxDetailDAC taxDetail, Tax tax)
  {
    PXPersistingCheck persistingCheck = tax.TaxType != "Q" || tax.PerUnitTaxPostMode == "T" ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing;
    cache.Adjust<PXDefaultAttribute>((object) taxDetail).For<TaxTran.accountID>((System.Action<PXDefaultAttribute>) (a => a.PersistingCheck = persistingCheck)).SameFor<TaxTran.subID>();
  }

  protected void _(Events.RowDeleting<TaxDetailDAC> e)
  {
    if (e.ExternalCall && this.CheckIfTaxDetailHasPerUnitTaxType(e.Row))
    {
      e.Cancel = true;
      throw new PXException("Per-unit taxes cannot be deleted manually.");
    }
  }

  private bool CheckIfTaxDetailHasPerUnitTaxType(TaxDetailDAC taxDeatil)
  {
    return this.GetTax(taxDeatil)?.TaxType == "Q";
  }
}
