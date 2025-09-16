// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxCalculationModeExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public abstract class TaxCalculationModeExtension<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  public PXSelectExtension<EntityWithTaxCalcMode> Entity;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>();

  protected virtual TaxCalculationModeExtension<TGraph, TPrimary>.EntityMapping GetEntityMapping()
  {
    return new TaxCalculationModeExtension<TGraph, TPrimary>.EntityMapping(typeof (TPrimary));
  }

  public IEnumerable<Tax> Taxes => this.GetTaxes();

  protected abstract IEnumerable<Tax> GetTaxes();

  public virtual bool SkipValidation => false;

  public virtual void _(Events.RowPersisting<EntityWithTaxCalcMode> e)
  {
    if (this.SkipValidation || string.IsNullOrEmpty(e.Row?.TaxCalcMode))
      return;
    if (this.Taxes.Count<Tax>() == 0)
      return;
    try
    {
      TaxCalculationModeExtension<TGraph, TPrimary>.VerifyTransactions(e.Row.TaxCalcMode, this.Taxes);
    }
    catch (PXException ex)
    {
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EntityWithTaxCalcMode>>) e).Cache.RaiseExceptionHandling<EntityWithTaxCalcMode.taxCalcMode>((object) e.Row, (object) e.Row.TaxCalcMode, (Exception) new PXSetPropertyException(((Exception) ex).Message));
      throw ex;
    }
  }

  protected static void VerifyTransactions(string calcMode, IEnumerable<Tax> taxes)
  {
    switch (calcMode)
    {
      case "G":
        if (taxes.Any<Tax>((Func<Tax, bool>) (tax => tax.TaxType == "P")))
          throw new PXException("The Gross tax calculation mode cannot be used in a document where a tax with the {0} type is specified.", new object[1]
          {
            (object) "Use"
          });
        if (!taxes.Any<Tax>((Func<Tax, bool>) (tax => tax.ReverseTax.GetValueOrDefault())))
          break;
        throw new PXException("The Gross tax calculation mode cannot be used in a document where a reversed tax is specified.");
      case "N":
        if (!taxes.Any<Tax>((Func<Tax, bool>) (tax => tax.TaxType == "W")))
          break;
        throw new PXException("The Net tax calculation mode cannot be used in a document where a tax with the {0} type is specified.", new object[1]
        {
          (object) "Withholding"
        });
    }
  }

  protected class EntityMapping : IBqlMapping
  {
    protected Type _table;
    public Type TaxCalcMode = typeof (EntityWithTaxCalcMode.taxCalcMode);

    public Type Extension => typeof (EntityWithTaxCalcMode);

    public Type Table => this._table;

    public EntityMapping(Type table) => this._table = table;
  }
}
