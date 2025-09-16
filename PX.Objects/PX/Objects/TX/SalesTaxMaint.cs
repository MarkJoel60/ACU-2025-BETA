// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SalesTaxMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.TX;

public class SalesTaxMaint : PXGraph<
#nullable disable
SalesTaxMaint>
{
  public PXSave<PX.Objects.TX.Tax> Save;
  public PXCancel<PX.Objects.TX.Tax> Cancel;
  public PXInsert<PX.Objects.TX.Tax> Insert;
  public PXDelete<PX.Objects.TX.Tax> Delete;
  public PXFirst<PX.Objects.TX.Tax> First;
  public PXPrevious<PX.Objects.TX.Tax> Previous;
  public PXNext<PX.Objects.TX.Tax> Next;
  public PXLast<PX.Objects.TX.Tax> Last;
  public PXSelect<PX.Objects.TX.Tax> Tax;
  public PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>>> CurrentTax;
  public PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>>> TaxForPrintingParametersTab;
  public PXSelect<TaxRev, Where<TaxRev.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>>, OrderBy<Asc<TaxRev.taxType, Desc<TaxRev.startDate>>>> TaxRevisions;
  public PXSelectJoin<TaxCategoryDet, LeftJoin<TaxCategory, On<TaxCategory.taxCategoryID, Equal<TaxCategoryDet.taxCategoryID>>>, Where<TaxCategoryDet.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>>> Categories;
  public PXSelectJoin<TaxZoneDet, LeftJoin<TaxZone, On<TaxZone.taxZoneID, Equal<TaxZoneDet.taxZoneID>>>, Where<TaxZoneDet.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>>> Zones;
  public PXSelectReadonly<TaxCategory> Category;
  public PXSelectReadonly<TaxZone> Zone;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  public SalesTaxMaint()
  {
    PX.Objects.AP.APSetup current = ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current;
    PXUIFieldAttribute.SetVisible<TaxCategoryDet.taxID>(((PXSelectBase) this.Categories).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<TaxCategoryDet.taxID>(((PXSelectBase) this.Categories).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<TaxCategoryDet.taxCategoryID>(((PXSelectBase) this.Categories).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<TaxCategoryDet.taxCategoryID>(((PXSelectBase) this.Categories).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<TaxZoneDet.taxID>(((PXSelectBase) this.Zones).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<TaxZoneDet.taxID>(((PXSelectBase) this.Zones).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<TaxZoneDet.taxZoneID>(((PXSelectBase) this.Zones).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<TaxZoneDet.taxZoneID>(((PXSelectBase) this.Zones).Cache, (object) null, true);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(SalesTaxMaint.\u003C\u003Ec.\u003C\u003E9__17_0 ?? (SalesTaxMaint.\u003C\u003Ec.\u003C\u003E9__17_0 = new PXFieldDefaulting((object) SalesTaxMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__17_0))));
    PXStringListAttribute.SetList<PX.Objects.TX.Tax.taxType>(((PXSelectBase) this.Tax).Cache, (object) null, CSTaxType.GetTaxTypesWithLabels(PXAccess.FeatureInstalled<FeaturesSet.vATReporting>(), PXAccess.FeatureInstalled<FeaturesSet.perUnitTaxSupport>()).ToArray<(string, string)>());
    this.PopulateBucketList(new PX.Objects.TX.Tax() { TaxType = "S" });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<TaxRev> e)
  {
    if (((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current == null || ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current.DeductibleVAT.GetValueOrDefault() || e.Row == null)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<TaxRev>>) e).Cache.SetDefaultExt<TaxRev.nonDeductibleTaxRate>((object) e.Row);
  }

  protected virtual void Tax_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PX.Objects.TX.Tax row = (PX.Objects.TX.Tax) e.Row;
    PX.Objects.TX.Tax oldRow = (PX.Objects.TX.Tax) e.OldRow;
    this.ClearOldWarningsAndErrors(sender, row);
    this.CheckAndFixTaxRates(row);
    this.SetWarningsOnRowUpdate(sender, row, oldRow);
    this.ProcessTaxRevOnTaxVendorChangeOnTaxUpdate(sender, row, oldRow);
    if (row.TaxType != oldRow.TaxType)
      this.ProccessTaxTypeChangeOnTaxUpdate(row, oldRow);
    this.VerifyTaxRevReportingGroupTypesOnTaxUpdate(row, oldRow);
    if (!sender.ObjectsEqual<PX.Objects.TX.Tax.outDate>(e.Row, e.OldRow))
    {
      PXSelectBase<TaxRev> pxSelectBase = (PXSelectBase<TaxRev>) new PXSelect<TaxRev, Where<TaxRev.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>, And<TaxRev.startDate, Greater<Required<PX.Objects.TX.Tax.outDate>>>>>((PXGraph) this);
      if (row.OutDate.HasValue)
      {
        if (pxSelectBase.Select(new object[1]
        {
          (object) row.OutDate
        }).Count > 0)
        {
          ((PXSelectBase) this.Tax).Cache.RaiseExceptionHandling<PX.Objects.TX.Tax.outDate>((object) row, (object) row.OutDate, (Exception) new PXSetPropertyException("Entered date is before some of tax revisions' starting dates", (PXErrorLevel) 2));
          goto label_14;
        }
      }
      sender.SetValue<PX.Objects.TX.Tax.outdated>(e.Row, (object) row.OutDate.HasValue);
      DateTime dateTime = row.OutDate ?? new DateTime(9999, 6, 6);
      foreach (PXResult<TaxRev> pxResult in ((PXSelectBase<TaxRev>) this.TaxRevisions).Select(Array.Empty<object>()))
      {
        TaxRev copy = (TaxRev) ((PXSelectBase) this.TaxRevisions).Cache.CreateCopy((object) PXResult<TaxRev>.op_Implicit(pxResult));
        copy.EndDate = new DateTime?(dateTime);
        ((PXSelectBase) this.TaxRevisions).Cache.Update((object) copy);
      }
    }
label_14:
    if (!sender.ObjectsEqual<PX.Objects.TX.Tax.reportExpenseToSingleAccount>(e.Row, e.OldRow))
    {
      bool? expenseToSingleAccount = row.ReportExpenseToSingleAccount;
      bool flag = false;
      if (expenseToSingleAccount.GetValueOrDefault() == flag & expenseToSingleAccount.HasValue)
      {
        sender.SetValue<PX.Objects.TX.Tax.expenseAccountID>((object) row, (object) null);
        sender.SetValue<PX.Objects.TX.Tax.expenseSubID>((object) row, (object) null);
      }
    }
    if (sender.ObjectsEqual<PX.Objects.TX.Tax.reportExpenseToSingleAccount>(e.Row, e.OldRow) || !row.ReportExpenseToSingleAccount.GetValueOrDefault())
      return;
    sender.SetDefaultExt<PX.Objects.TX.Tax.expenseAccountID>(e.Row);
    sender.SetDefaultExt<PX.Objects.TX.Tax.expenseSubID>(e.Row);
  }

  /// <summary>
  /// Populate values list for the combobox on <see cref="P:PX.Objects.TX.TaxRev.TaxBucketID" /> field.
  /// </summary>
  /// <param name="tax">The tax.</param>
  public virtual void PopulateBucketList(PX.Objects.TX.Tax tax)
  {
    List<int> allowedValues = new List<int>();
    List<string> allowedLabels = new List<string>();
    switch (tax.TaxType)
    {
      case "V":
        this.FillAllowedValuesAndLabelsForVatTax(tax, allowedValues, allowedLabels);
        break;
      case "P":
      case "W":
        this.FillAllowedValuesAndLabelsForUseOrWitholdingTax(tax, allowedValues, allowedLabels);
        break;
      case "S":
        this.FillAllowedValuesAndLabelsForSalesTax(tax, allowedValues, allowedLabels);
        break;
      case "Q":
        this.FillAllowedValuesAndLabelsForPerUnitTax(tax, allowedValues, allowedLabels);
        break;
    }
    if (allowedValues.Count > 0)
      PXIntListAttribute.SetList<TaxRev.taxBucketID>(((PXSelectBase) this.TaxRevisions).Cache, (object) null, allowedValues.ToArray(), allowedLabels.ToArray());
    else
      PXIntListAttribute.SetList<TaxRev.taxBucketID>(((PXSelectBase) this.TaxRevisions).Cache, (object) null, new (int, string)[1]
      {
        (0, "undefined")
      });
  }

  protected virtual void FillAllowedValuesAndLabelsForUseOrWitholdingTax(
    PX.Objects.TX.Tax tax,
    List<int> allowedValues,
    List<string> allowedLabels)
  {
    if (!tax.TaxVendorID.HasValue)
    {
      allowedValues.Add(-2);
      allowedLabels.Add("Default Output Group");
    }
    else
    {
      foreach (PXResult<TaxBucket> pxResult in PXSelectBase<TaxBucket, PXSelectReadonly<TaxBucket, Where<TaxBucket.vendorID, Equal<Required<TaxBucket.vendorID>>, And<TaxBucket.bucketType, Equal<CSTaxBucketType.sales>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) tax.TaxVendorID
      }))
      {
        TaxBucket taxBucket = PXResult<TaxBucket>.op_Implicit(pxResult);
        allowedValues.Add(taxBucket.BucketID.Value);
        allowedLabels.Add(taxBucket.Name);
      }
    }
  }

  protected virtual void FillAllowedValuesAndLabelsForPerUnitTax(
    PX.Objects.TX.Tax perUnitTax,
    List<int> allowedValues,
    List<string> allowedLabels)
  {
    this.FillAllowedValuesAndLabelsForSalesTax(perUnitTax, allowedValues, allowedLabels);
  }

  protected virtual void FillAllowedValuesAndLabelsForSalesTax(
    PX.Objects.TX.Tax salesTax,
    List<int> allowedValues,
    List<string> allowedLabels)
  {
    if (!salesTax.TaxVendorID.HasValue)
    {
      allowedValues.Add(-2);
      allowedLabels.Add("Default Output Group");
      allowedValues.Add(-1);
      allowedLabels.Add("Default Input Group");
    }
    else
    {
      foreach (PXResult<TaxBucket> pxResult in PXSelectBase<TaxBucket, PXSelectReadonly<TaxBucket, Where<TaxBucket.vendorID, Equal<Required<TaxBucket.vendorID>>, And<Where<TaxBucket.bucketType, Equal<CSTaxBucketType.sales>, Or<TaxBucket.bucketType, Equal<CSTaxBucketType.purchase>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) salesTax.TaxVendorID
      }))
      {
        TaxBucket taxBucket = PXResult<TaxBucket>.op_Implicit(pxResult);
        allowedValues.Add(taxBucket.BucketID.Value);
        allowedLabels.Add(taxBucket.Name);
      }
    }
  }

  /// <summary>
  /// Fill allowed values and labels for VAT tax on the population of values for combobox on <see cref="P:PX.Objects.TX.TaxRev.TaxBucketID" /> field.
  /// </summary>
  /// <param name="vatTax">The VAT tax.</param>
  /// <param name="allowedValues">The allowed values.</param>
  /// <param name="allowedLabels">The allowed labels.</param>
  public virtual void FillAllowedValuesAndLabelsForVatTax(
    PX.Objects.TX.Tax vatTax,
    List<int> allowedValues,
    List<string> allowedLabels)
  {
    if (!vatTax.TaxVendorID.HasValue)
    {
      allowedValues.Add(-1);
      allowedLabels.Add("Default Input Group");
      bool? deductibleVat = vatTax.DeductibleVAT;
      bool flag = false;
      if (!(deductibleVat.GetValueOrDefault() == flag & deductibleVat.HasValue))
        return;
      allowedValues.Add(-2);
      allowedLabels.Add("Default Output Group");
    }
    else
    {
      PXResultset<TaxBucket> pxResultset;
      if (vatTax.DeductibleVAT.GetValueOrDefault())
        pxResultset = PXSelectBase<TaxBucket, PXSelectReadonly<TaxBucket, Where<TaxBucket.vendorID, Equal<Required<TaxBucket.vendorID>>, And<TaxBucket.bucketType, Equal<Required<TaxBucket.bucketType>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) vatTax.TaxVendorID,
          (object) "P"
        });
      else
        pxResultset = PXSelectBase<TaxBucket, PXSelectReadonly<TaxBucket, Where<TaxBucket.vendorID, Equal<Required<TaxBucket.vendorID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) vatTax.TaxVendorID
        });
      foreach (PXResult<TaxBucket> pxResult in pxResultset)
      {
        TaxBucket taxBucket = PXResult<TaxBucket>.op_Implicit(pxResult);
        allowedValues.Add(taxBucket.BucketID.Value);
        allowedLabels.Add(taxBucket.Name);
      }
    }
  }

  /// <summary>
  /// Clears the old warnings and errors. Called on <see cref="M:PX.Objects.TX.SalesTaxMaint.Tax_RowUpdated(PX.Data.PXCache,PX.Data.PXRowUpdatedEventArgs)" />.
  /// </summary>
  /// <param name="cache">The cache.</param>
  /// <param name="newTax">The new tax.</param>
  public virtual void ClearOldWarningsAndErrors(PXCache cache, PX.Objects.TX.Tax newTax)
  {
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxType>((object) newTax, (object) newTax.TaxType, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) newTax.TaxCalcRule, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcLevel2Exclude>((object) newTax, (object) newTax.TaxCalcLevel2Exclude, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.reverseTax>((object) newTax, (object) newTax.ReverseTax, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.pendingTax>((object) newTax, (object) newTax.PendingTax, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.exemptTax>((object) newTax, (object) newTax.ExemptTax, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.statisticalTax>((object) newTax, (object) newTax.StatisticalTax, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.directTax>((object) newTax, (object) newTax.DirectTax, (Exception) null);
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.includeInTaxable>((object) newTax, (object) newTax.IncludeInTaxable, (Exception) null);
  }

  /// <summary>
  /// Check and fix tax rates. Called on <see cref="M:PX.Objects.TX.SalesTaxMaint.Tax_RowUpdated(PX.Data.PXCache,PX.Data.PXRowUpdatedEventArgs)" />.
  /// </summary>
  /// <param name="newTax">The new tax.</param>
  public virtual void CheckAndFixTaxRates(PX.Objects.TX.Tax newTax)
  {
    if (!newTax.ExemptTax.GetValueOrDefault())
      return;
    foreach (PXResult<TaxRev> pxResult in ((PXSelectBase<TaxRev>) this.TaxRevisions).Select(Array.Empty<object>()))
    {
      TaxRev taxRev = PXResult<TaxRev>.op_Implicit(pxResult);
      Decimal? taxRate = taxRev.TaxRate;
      if (taxRate.HasValue)
      {
        taxRate = taxRev.TaxRate;
        Decimal num = 0M;
        if (taxRate.GetValueOrDefault() == num & taxRate.HasValue)
          continue;
      }
      taxRev.TaxRate = new Decimal?(0M);
      ((PXSelectBase) this.TaxRevisions).Cache.Update((object) taxRev);
    }
  }

  /// <summary>
  /// Sets warnings on row update. Called on <see cref="M:PX.Objects.TX.SalesTaxMaint.Tax_RowUpdated(PX.Data.PXCache,PX.Data.PXRowUpdatedEventArgs)" />.
  /// </summary>
  /// <param name="cache">The cache.</param>
  /// <param name="newTax">The new tax.</param>
  /// <param name="oldTax">The old tax.</param>
  public virtual void SetWarningsOnRowUpdate(PXCache cache, PX.Objects.TX.Tax newTax, PX.Objects.TX.Tax oldTax)
  {
    if (newTax.TaxType != "V" && newTax.DirectTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxType>((object) newTax, (object) newTax.TaxType, (Exception) new PXSetPropertyException("This option can only be used with tax type VAT."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.directTax>((object) newTax, (object) newTax.DirectTax, (Exception) new PXSetPropertyException("This option can only be used with tax type VAT."));
    }
    else if (newTax.ExemptTax.GetValueOrDefault() && newTax.DirectTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.exemptTax>((object) newTax, (object) newTax.ExemptTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.directTax>((object) newTax, (object) newTax.DirectTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.StatisticalTax.GetValueOrDefault() && newTax.DirectTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.statisticalTax>((object) newTax, (object) newTax.StatisticalTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.directTax>((object) newTax, (object) newTax.DirectTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.DeductibleVAT.GetValueOrDefault() && newTax.DirectTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.deductibleVAT>((object) newTax, (object) newTax.DeductibleVAT, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.directTax>((object) newTax, (object) newTax.DirectTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.ReverseTax.GetValueOrDefault() && newTax.DirectTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.reverseTax>((object) newTax, (object) newTax.ReverseTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.directTax>((object) newTax, (object) newTax.DirectTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.TaxType != "V" && newTax.PendingTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxType>((object) newTax, (object) newTax.TaxType, (Exception) new PXSetPropertyException("This option can only be used with tax type VAT."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.pendingTax>((object) newTax, (object) newTax.PendingTax, (Exception) new PXSetPropertyException("This option can only be used with tax type VAT."));
    }
    else if (newTax.ReverseTax.GetValueOrDefault() && newTax.ExemptTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.reverseTax>((object) newTax, (object) newTax.ReverseTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.exemptTax>((object) newTax, (object) newTax.ExemptTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.ReverseTax.GetValueOrDefault() && newTax.StatisticalTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.reverseTax>((object) newTax, (object) newTax.ReverseTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.statisticalTax>((object) newTax, (object) newTax.StatisticalTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.PendingTax.GetValueOrDefault() && newTax.StatisticalTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.pendingTax>((object) newTax, (object) newTax.PendingTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.statisticalTax>((object) newTax, (object) newTax.StatisticalTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.PendingTax.GetValueOrDefault() && newTax.ExemptTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.pendingTax>((object) newTax, (object) newTax.PendingTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.exemptTax>((object) newTax, (object) newTax.ExemptTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.StatisticalTax.GetValueOrDefault() && newTax.ExemptTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.statisticalTax>((object) newTax, (object) newTax.StatisticalTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.exemptTax>((object) newTax, (object) newTax.ExemptTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else if (newTax.TaxCalcLevel == "0" && newTax.TaxCalcLevel2Exclude.GetValueOrDefault() && !newTax.DirectTax.GetValueOrDefault())
    {
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) newTax.TaxCalcRule, (Exception) new PXSetPropertyException("These two options can't be combined."));
      cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcLevel2Exclude>((object) newTax, (object) newTax.TaxCalcLevel2Exclude, (Exception) new PXSetPropertyException("These two options can't be combined."));
    }
    else
    {
      if (newTax.TaxType == "P")
      {
        bool? calcLevel2Exclude = newTax.TaxCalcLevel2Exclude;
        bool flag = false;
        if (calcLevel2Exclude.GetValueOrDefault() == flag & calcLevel2Exclude.HasValue)
        {
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxType>((object) newTax, (object) newTax.TaxType, (Exception) new PXSetPropertyException("These two options should be combined."));
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcLevel2Exclude>((object) newTax, (object) newTax.TaxCalcLevel2Exclude, (Exception) new PXSetPropertyException("These two options should be combined."));
          return;
        }
      }
      if (newTax.TaxType == "P" && newTax.TaxCalcLevel == "0")
      {
        cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxType>((object) newTax, (object) newTax.TaxType, (Exception) new PXSetPropertyException("These two options can't be combined."));
        cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) newTax.TaxCalcRule, (Exception) new PXSetPropertyException("These two options can't be combined."));
      }
      else
      {
        if (newTax.TaxType == "P")
        {
          bool? expenseToSingleAccount = newTax.ReportExpenseToSingleAccount;
          bool flag = false;
          if (expenseToSingleAccount.GetValueOrDefault() == flag & expenseToSingleAccount.HasValue && newTax.TaxCalcType != "I")
          {
            cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) newTax.TaxCalcRule, (Exception) new PXSetPropertyException("These two options can't be combined."));
            cache.RaiseExceptionHandling<PX.Objects.TX.Tax.reportExpenseToSingleAccount>((object) newTax, (object) newTax.ReportExpenseToSingleAccount, (Exception) new PXSetPropertyException("These two options can't be combined."));
            return;
          }
        }
        if (newTax.TaxType == "W" && newTax.TaxCalcLevel != "0")
        {
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxType>((object) newTax, (object) newTax.TaxType, (Exception) new PXSetPropertyException("These two options can't be combined."));
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) newTax.TaxCalcRule, (Exception) new PXSetPropertyException("These two options can't be combined."));
        }
        else if (newTax.ReverseTax.GetValueOrDefault() && newTax.TaxCalcLevel == "0")
        {
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.reverseTax>((object) newTax, (object) newTax.ReverseTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) newTax.TaxCalcRule, (Exception) new PXSetPropertyException("These two options can't be combined."));
        }
        else if (newTax.ExemptTax.GetValueOrDefault() && newTax.IncludeInTaxable.GetValueOrDefault())
        {
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.exemptTax>((object) newTax, (object) newTax.ExemptTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.includeInTaxable>((object) newTax, (object) newTax.IncludeInTaxable, (Exception) new PXSetPropertyException("These two options can't be combined."));
        }
        else if (newTax.StatisticalTax.GetValueOrDefault() && newTax.IncludeInTaxable.GetValueOrDefault())
        {
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.statisticalTax>((object) newTax, (object) newTax.StatisticalTax, (Exception) new PXSetPropertyException("These two options can't be combined."));
          cache.RaiseExceptionHandling<PX.Objects.TX.Tax.includeInTaxable>((object) newTax, (object) newTax.IncludeInTaxable, (Exception) new PXSetPropertyException("These two options can't be combined."));
        }
        else
        {
          if (newTax.DeductibleVAT.GetValueOrDefault())
          {
            bool? expenseToSingleAccount = newTax.ReportExpenseToSingleAccount;
            bool flag = false;
            if (expenseToSingleAccount.GetValueOrDefault() == flag & expenseToSingleAccount.HasValue && newTax.TaxCalcType != "I")
            {
              cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) newTax.TaxCalcRule, (Exception) new PXSetPropertyException("These two options can't be combined."));
              cache.RaiseExceptionHandling<PX.Objects.TX.Tax.reportExpenseToSingleAccount>((object) newTax, (object) newTax.ReportExpenseToSingleAccount, (Exception) new PXSetPropertyException("These two options can't be combined."));
              return;
            }
          }
          bool? directTax = newTax.DirectTax;
          bool? valueOriginal = cache.GetValueOriginal<PX.Objects.TX.Tax.directTax>((object) newTax) as bool?;
          string errorMessage;
          if (!(directTax.GetValueOrDefault() == valueOriginal.GetValueOrDefault() & directTax.HasValue == valueOriginal.HasValue) && !this.TryValidateTaxCombinations(cache, newTax, oldTax, out errorMessage))
            cache.RaiseExceptionHandling<PX.Objects.TX.Tax.directTax>((object) newTax, (object) newTax.DirectTax, (Exception) new PXSetPropertyException(errorMessage));
          else
            this.ResetTaxFieldsOnTaxUpdate(cache, newTax, oldTax);
        }
      }
    }
  }

  /// <summary>Resets some of tax fields on tax row updated event.</summary>
  protected virtual void ResetTaxFieldsOnTaxUpdate(PXCache cache, PX.Objects.TX.Tax newTax, PX.Objects.TX.Tax oldTax)
  {
    switch (newTax.TaxType)
    {
      case "V":
        if (newTax.TaxType != "V")
        {
          cache.SetValue<PX.Objects.TX.Tax.reverseTax>((object) newTax, (object) false);
          cache.SetValue<PX.Objects.TX.Tax.pendingTax>((object) newTax, (object) false);
          cache.SetValue<PX.Objects.TX.Tax.exemptTax>((object) newTax, (object) false);
          cache.SetValue<PX.Objects.TX.Tax.statisticalTax>((object) newTax, (object) false);
          cache.SetValue<PX.Objects.TX.Tax.deductibleVAT>((object) newTax, (object) false);
        }
        bool? nullable = newTax.PendingTax;
        if (!nullable.GetValueOrDefault())
        {
          this.ResetPendingSalesTax(cache, newTax);
          cache.SetValue<PX.Objects.TX.Tax.pendingPurchTaxAcctID>((object) newTax, (object) null);
          cache.SetValue<PX.Objects.TX.Tax.pendingPurchTaxSubID>((object) newTax, (object) null);
        }
        nullable = newTax.DeductibleVAT;
        int num1;
        if (nullable.GetValueOrDefault())
        {
          nullable = newTax.DeductibleVAT;
          bool? deductibleVat = oldTax.DeductibleVAT;
          num1 = !(nullable.GetValueOrDefault() == deductibleVat.GetValueOrDefault() & nullable.HasValue == deductibleVat.HasValue) ? 1 : 0;
        }
        else
          num1 = 0;
        bool? deductibleVat1 = newTax.DeductibleVAT;
        bool flag1 = false;
        int num2;
        if (deductibleVat1.GetValueOrDefault() == flag1 & deductibleVat1.HasValue)
        {
          bool? deductibleVat2 = newTax.DeductibleVAT;
          nullable = oldTax.DeductibleVAT;
          num2 = !(deductibleVat2.GetValueOrDefault() == nullable.GetValueOrDefault() & deductibleVat2.HasValue == nullable.HasValue) ? 1 : 0;
        }
        else
          num2 = 0;
        bool flag2 = num2 != 0;
        if (newTax.TaxType != "P" & flag2)
        {
          cache.SetValue<PX.Objects.TX.Tax.expenseAccountID>((object) newTax, (object) null);
          cache.SetValue<PX.Objects.TX.Tax.expenseSubID>((object) newTax, (object) null);
        }
        if (flag2)
          cache.SetValue<PX.Objects.TX.Tax.reportExpenseToSingleAccount>((object) newTax, (object) true);
        if (num1 == 0)
          break;
        cache.SetDefaultExt<PX.Objects.TX.Tax.expenseAccountID>((object) newTax);
        cache.SetDefaultExt<PX.Objects.TX.Tax.expenseSubID>((object) newTax);
        break;
      case "Q":
        if (!(newTax.PerUnitTaxPostMode == "T"))
        {
          if (newTax.PerUnitTaxPostMode == "L")
          {
            cache.SetValue<PX.Objects.TX.Tax.salesTaxAcctID>((object) newTax, (object) null);
            cache.SetValue<PX.Objects.TX.Tax.salesTaxSubID>((object) newTax, (object) null);
            cache.SetValue<PX.Objects.TX.Tax.purchTaxAcctID>((object) newTax, (object) null);
            cache.SetValue<PX.Objects.TX.Tax.purchTaxSubID>((object) newTax, (object) null);
            goto case "V";
          }
          goto default;
        }
        goto case "V";
      default:
        cache.SetValue<PX.Objects.TX.Tax.purchTaxAcctID>((object) newTax, (object) null);
        cache.SetValue<PX.Objects.TX.Tax.purchTaxSubID>((object) newTax, (object) null);
        goto case "V";
    }
  }

  protected virtual void ResetPendingSalesTax(PXCache cache, PX.Objects.TX.Tax newTax)
  {
    cache.SetValue<PX.Objects.TX.Tax.pendingSalesTaxAcctID>((object) newTax, (object) null);
    cache.SetValue<PX.Objects.TX.Tax.pendingSalesTaxSubID>((object) newTax, (object) null);
  }

  /// <summary>
  /// Process the tax revisions on tax vendor change on tax update. Called on <see cref="M:PX.Objects.TX.SalesTaxMaint.Tax_RowUpdated(PX.Data.PXCache,PX.Data.PXRowUpdatedEventArgs)" />.
  /// </summary>
  /// <param name="sender">The sender.</param>
  /// <param name="newTax">The new tax.</param>
  /// <param name="oldTax">The old tax.</param>
  public virtual void ProcessTaxRevOnTaxVendorChangeOnTaxUpdate(
    PXCache sender,
    PX.Objects.TX.Tax newTax,
    PX.Objects.TX.Tax oldTax)
  {
    if (sender.ObjectsEqual<PX.Objects.TX.Tax.taxVendorID>((object) newTax, (object) oldTax))
      return;
    foreach (PXResult<TaxRev> pxResult in ((PXSelectBase<TaxRev>) this.TaxRevisions).Select(Array.Empty<object>()))
    {
      TaxRev taxRev1 = PXResult<TaxRev>.op_Implicit(pxResult);
      int? nullable1 = newTax.TaxVendorID;
      if (!nullable1.HasValue)
      {
        taxRev1.TaxBucketID = new int?(taxRev1.TaxType == "S" ? -2 : -1);
      }
      else
      {
        TaxRev taxRev2 = taxRev1;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        taxRev2.TaxBucketID = nullable2;
        ((PXSelectBase) this.TaxRevisions).Cache.RaiseExceptionHandling<TaxRev.taxBucketID>((object) taxRev1, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
      }
      GraphHelper.MarkUpdated(((PXSelectBase) this.TaxRevisions).Cache, (object) taxRev1);
    }
    this.PopulateBucketList(newTax);
  }

  /// <summary>
  /// Proccess tax type change during tax row updated event.
  /// Depending on the tax type defaults values in some of <see cref="T:PX.Objects.TX.TaxRev" /> fields.
  /// </summary>
  public virtual void ProccessTaxTypeChangeOnTaxUpdate(PX.Objects.TX.Tax newTax, PX.Objects.TX.Tax oldTax)
  {
    bool flag1 = newTax?.TaxType == "Q";
    bool flag2 = oldTax?.TaxType == "Q";
    if (flag1)
    {
      string str = "I1";
      ((PXSelectBase) this.Tax).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) str);
      ((PXSelectBase) this.Tax).Cache.SetValueExt<PX.Objects.TX.Tax.taxApplyTermsDisc>((object) newTax, (object) "N");
      ((PXSelectBase) this.Tax).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcLevel2Exclude>((object) newTax, (object) false);
    }
    else if (flag2)
    {
      string str = "I1";
      ((PXSelectBase) this.Tax).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcRule>((object) newTax, (object) str);
      ((PXSelectBase) this.Tax).Cache.SetDefaultExt<PX.Objects.TX.Tax.taxUOM>((object) newTax);
      ((PXSelectBase) this.Tax).Cache.SetDefaultExt<PX.Objects.TX.Tax.perUnitTaxPostMode>((object) newTax);
    }
    foreach (PXResult<TaxRev> pxResult in ((PXSelectBase<TaxRev>) this.TaxRevisions).Select(Array.Empty<object>()))
    {
      TaxRev taxRev = PXResult<TaxRev>.op_Implicit(pxResult);
      if (flag1)
      {
        ((PXSelectBase) this.TaxRevisions).Cache.SetDefaultExt<TaxRev.taxableMax>((object) taxRev);
        ((PXSelectBase) this.TaxRevisions).Cache.SetDefaultExt<TaxRev.taxableMin>((object) taxRev);
      }
      else
        ((PXSelectBase) this.TaxRevisions).Cache.SetDefaultExt<TaxRev.taxableMaxQty>((object) taxRev);
    }
  }

  /// <summary>
  /// Verify tax reverse reporting group types on tax update. Called on <see cref="M:PX.Objects.TX.SalesTaxMaint.Tax_RowUpdated(PX.Data.PXCache,PX.Data.PXRowUpdatedEventArgs)" />.
  /// </summary>
  /// <param name="newTax">The new tax.</param>
  /// <param name="oldTax">The old tax.</param>
  public virtual void VerifyTaxRevReportingGroupTypesOnTaxUpdate(PX.Objects.TX.Tax newTax, PX.Objects.TX.Tax oldTax)
  {
    int num;
    if (newTax.TaxType == "V" && newTax.DeductibleVAT.GetValueOrDefault())
    {
      bool? deductibleVat1 = newTax.DeductibleVAT;
      bool? deductibleVat2 = oldTax.DeductibleVAT;
      num = !(deductibleVat1.GetValueOrDefault() == deductibleVat2.GetValueOrDefault() & deductibleVat1.HasValue == deductibleVat2.HasValue) ? 1 : 0;
    }
    else
      num = 0;
    bool taxIsDeductibleVAT = num != 0;
    bool taxIsWithholding = newTax.TaxType == "W" && newTax.TaxType != oldTax.TaxType;
    if (!taxIsDeductibleVAT && !taxIsWithholding)
      return;
    foreach (PXResult<TaxRev> pxResult in ((PXSelectBase<TaxRev>) this.TaxRevisions).Select(Array.Empty<object>()))
    {
      TaxRev taxRevision = PXResult<TaxRev>.op_Implicit(pxResult);
      (bool IsCompatible, string ErrorMsg) = this.CheckTaxRevisionCompatibilityWithTax(taxRevision, taxIsDeductibleVAT, taxIsWithholding);
      if (!IsCompatible)
      {
        PXSetPropertyException propertyException = new PXSetPropertyException(ErrorMsg);
        ((PXSelectBase) this.TaxRevisions).Cache.RaiseExceptionHandling<TaxRev.taxType>((object) taxRevision, (object) taxRevision.TaxType, (Exception) propertyException);
        ((PXSelectBase) this.TaxRevisions).Cache.RaiseExceptionHandling<TaxRev.taxBucketID>((object) taxRevision, (object) null, (Exception) propertyException);
      }
    }
  }

  /// <summary>
  /// Validates the tax revision compatibility with tax. Returns a pair of values - result of the check (true is success) and error message.
  /// </summary>
  /// <param name="taxRevision">The tax revision.</param>
  /// <param name="taxIsDeductibleVAT">True if tax is deductible VAT.</param>
  /// <param name="taxIsWithholding">True if tax is withholding.</param>
  /// <returns />
  private (bool IsCompatible, string ErrorMsg) CheckTaxRevisionCompatibilityWithTax(
    TaxRev taxRevision,
    bool taxIsDeductibleVAT,
    bool taxIsWithholding)
  {
    if (taxIsDeductibleVAT && taxRevision.TaxType == "S")
      return (false, "A partially deductible VAT cannot be included in the output reporting group.");
    return taxIsWithholding && taxRevision.TaxType == "P" ? (false, "A withholding tax cannot be included in the input reporting group.") : (true, (string) null);
  }

  /// <summary>
  /// In a given Tax record, sets all VAT related fields to their default values.
  /// </summary>
  protected void DefaultAllVatRelatedFields(PXCache cache, PX.Objects.TX.Tax taxRecord)
  {
    cache.SetDefaultExt<PX.Objects.TX.Tax.deductibleVAT>((object) taxRecord);
    cache.SetDefaultExt<PX.Objects.TX.Tax.reverseTax>((object) taxRecord);
    cache.SetDefaultExt<PX.Objects.TX.Tax.statisticalTax>((object) taxRecord);
    cache.SetDefaultExt<PX.Objects.TX.Tax.exemptTax>((object) taxRecord);
    cache.SetDefaultExt<PX.Objects.TX.Tax.includeInTaxable>((object) taxRecord);
    cache.SetDefaultExt<PX.Objects.TX.Tax.pendingTax>((object) taxRecord);
    cache.SetDefaultExt<PX.Objects.TX.Tax.directTax>((object) taxRecord);
  }

  protected virtual void Tax_TaxVendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.TX.Tax row))
      return;
    sender.SetDefaultExt<PX.Objects.TX.Tax.salesTaxAcctID>((object) row);
    sender.SetDefaultExt<PX.Objects.TX.Tax.salesTaxSubID>((object) row);
    if (row.TaxType == "V")
    {
      sender.SetDefaultExt<PX.Objects.TX.Tax.purchTaxAcctID>((object) row);
      sender.SetDefaultExt<PX.Objects.TX.Tax.purchTaxSubID>((object) row);
    }
    if (row.PendingTax.GetValueOrDefault())
    {
      sender.SetDefaultExt<PX.Objects.TX.Tax.pendingSalesTaxAcctID>((object) row);
      sender.SetDefaultExt<PX.Objects.TX.Tax.pendingSalesTaxSubID>((object) row);
      sender.SetDefaultExt<PX.Objects.TX.Tax.pendingPurchTaxAcctID>((object) row);
      sender.SetDefaultExt<PX.Objects.TX.Tax.pendingPurchTaxSubID>((object) row);
    }
    if (row.TaxType == "P" || row.TaxType == "S" || this.ExpenseAccountRequired(row))
    {
      sender.SetDefaultExt<PX.Objects.TX.Tax.expenseAccountID>((object) row);
      sender.SetDefaultExt<PX.Objects.TX.Tax.expenseSubID>((object) row);
    }
    foreach (TaxRev taxRev in ((PXSelectBase) this.TaxRevisions).View.SelectMultiBound((object[]) new PX.Objects.TX.Tax[1]
    {
      row
    }, Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.TaxRevisions).Cache, (object) taxRev);
  }

  protected virtual void Tax_TaxType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.TX.Tax row = e.Row as PX.Objects.TX.Tax;
    if (row.TaxType != "V")
      this.DefaultAllVatRelatedFields(sender, row);
    if (row.TaxType == "P")
      sender.SetValueExt<PX.Objects.TX.Tax.taxCalcLevel2Exclude>((object) row, (object) true);
    if (!(row.TaxType == "W"))
      return;
    sender.SetValueExt<PX.Objects.TX.Tax.taxCalcRule>((object) row, (object) "I0");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxCategoryDet, TaxCategoryDet.taxCategoryID> e)
  {
    TaxCategoryDet row = e.Row;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategoryDet, TaxCategoryDet.taxCategoryID>, TaxCategoryDet, object>) e).NewValue;
    if (string.IsNullOrEmpty(newValue))
      return;
    string[] taxCategoryIds = new string[1]{ newValue };
    string taxId = ((PXSelectBase<PX.Objects.TX.Tax>) this.CurrentTax).Current?.TaxID;
    PX.Objects.TX.Tax current1 = ((PXSelectBase<PX.Objects.TX.Tax>) this.CurrentTax).Current;
    bool? directTax;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      directTax = current1.DirectTax;
      num1 = directTax.GetValueOrDefault() ? 1 : 0;
    }
    PXResultset<TaxCategoryDet> pxResultset;
    ref PXResultset<TaxCategoryDet> local = ref pxResultset;
    if (!this.TryValidateDirectIndirectTaxCombinationWithCategories(taxCategoryIds, taxId, num1 != 0, out local))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategoryDet, TaxCategoryDet.taxCategoryID>>) e).Cancel = true;
      throw new PXSetPropertyException("Direct-entry and non-direct-entry taxes cannot be included in the same tax category ({0}).", new object[1]
      {
        (object) newValue
      });
    }
    string[] array = GraphHelper.RowCast<TaxZoneDet>((IEnumerable) ((PXSelectBase<TaxZoneDet>) this.Zones).Select(Array.Empty<object>())).Select<TaxZoneDet, string>((Func<TaxZoneDet, string>) (a => a.TaxZoneID)).ToArray<string>();
    PX.Objects.TX.Tax current2 = ((PXSelectBase<PX.Objects.TX.Tax>) this.CurrentTax).Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      directTax = current2.DirectTax;
      num2 = directTax.GetValueOrDefault() ? 1 : 0;
    }
    if (num2 == 0)
      return;
    PXResultset<TaxCategoryDet, TaxZoneDet> invalidZoneCategoryCombinations;
    if (!this.TryValidteDirectTaxCombinationWithTaxZoneAndTaxCategory(array, new string[1]
    {
      newValue
    }, ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current?.TaxID, out invalidZoneCategoryCombinations))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxCategoryDet, TaxCategoryDet.taxCategoryID>>) e).Cancel = true;
      throw new PXSetPropertyException("Multiple direct-entry taxes cannot be included in the same tax category ({0}) and tax zone ({1}).", new object[2]
      {
        (object) newValue,
        (object) PXResultset<TaxCategoryDet, TaxZoneDet>.op_Implicit(invalidZoneCategoryCombinations)?.TaxZoneID
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxZoneDet, TaxZoneDet.taxZoneID> e)
  {
    TaxZoneDet row = e.Row;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneDet, TaxZoneDet.taxZoneID>, TaxZoneDet, object>) e).NewValue;
    if (string.IsNullOrEmpty(newValue))
      return;
    string[] array = GraphHelper.RowCast<TaxCategoryDet>((IEnumerable) ((PXSelectBase<TaxCategoryDet>) this.Categories).Select(Array.Empty<object>())).Select<TaxCategoryDet, string>((Func<TaxCategoryDet, string>) (a => a.TaxCategoryID)).ToArray<string>();
    PXResultset<TaxCategoryDet, TaxZoneDet> invalidZoneCategoryCombinations;
    if (this.TryValidteDirectTaxCombinationWithTaxZoneAndTaxCategory(new string[1]
    {
      newValue
    }, array, ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current?.TaxID, out invalidZoneCategoryCombinations))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneDet, TaxZoneDet.taxZoneID>>) e).Cancel = true;
    if (((PXSelectBase<PX.Objects.TX.Tax>) this.CurrentTax).Current.DirectTax.GetValueOrDefault())
      throw new PXSetPropertyException("Multiple direct-entry taxes cannot be included in the same tax category ({0}) and tax zone ({1}).", new object[2]
      {
        (object) PXResultset<TaxCategoryDet, TaxZoneDet>.op_Implicit(invalidZoneCategoryCombinations)?.TaxCategoryID,
        (object) newValue
      });
    throw new PXSetPropertyException("Direct-entry and non-direct-entry taxes cannot be included in the same tax category ({0}).", new object[1]
    {
      (object) PXResultset<TaxCategoryDet, TaxZoneDet>.op_Implicit(invalidZoneCategoryCombinations)?.TaxCategoryID
    });
  }

  protected virtual bool TryValidateDirectIndirectTaxCombinationWithCategories(
    string[] taxCategoryIds,
    string taxId,
    bool isDirectTax,
    out PXResultset<TaxCategoryDet> invalidCategoryCombinations)
  {
    if (taxCategoryIds.Length == 0)
    {
      invalidCategoryCombinations = new PXResultset<TaxCategoryDet>();
      return true;
    }
    invalidCategoryCombinations = PXSelectBase<TaxCategoryDet, PXViewOf<TaxCategoryDet>.BasedOn<SelectFromBase<TaxCategoryDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<TaxCategory>.On<BqlOperand<TaxCategory.taxCategoryID, IBqlString>.IsEqual<TaxCategoryDet.taxCategoryID>>>, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<TaxCategoryDet.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategoryDet.taxCategoryID, In<P.AsString>>>>, And<BqlOperand<TaxCategory.taxCatFlag, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsNotEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.TX.Tax.directTax, IBqlBool>.IsNotEqual<P.AsBool>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) taxCategoryIds,
      (object) taxId,
      (object) isDirectTax
    });
    return invalidCategoryCombinations.Count == 0;
  }

  protected virtual bool TryValidteDirectTaxCombinationWithTaxZoneAndTaxCategory(
    string[] taxZoneIds,
    string[] taxCategoryIds,
    string taxId,
    out PXResultset<TaxCategoryDet, TaxZoneDet> invalidZoneCategoryCombinations)
  {
    invalidZoneCategoryCombinations = new PXResultset<TaxCategoryDet, TaxZoneDet>();
    if (taxZoneIds.Length == 0 || taxCategoryIds.Length == 0)
      return true;
    foreach (PXResult<TaxCategoryDet, TaxZoneDet, TaxCategory, PX.Objects.TX.Tax> pxResult in PXSelectBase<TaxCategoryDet, PXViewOf<TaxCategoryDet>.BasedOn<SelectFromBase<TaxCategoryDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<TaxZoneDet>.On<BqlOperand<TaxZoneDet.taxID, IBqlString>.IsEqual<TaxCategoryDet.taxID>>>, FbqlJoins.Inner<TaxCategory>.On<BqlOperand<TaxCategory.taxCategoryID, IBqlString>.IsEqual<TaxCategoryDet.taxCategoryID>>>, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<TaxCategoryDet.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneDet.taxZoneID, In<P.AsString>>>>, And<BqlOperand<TaxCategoryDet.taxCategoryID, IBqlString>.IsIn<P.AsString>>>, And<BqlOperand<TaxCategory.taxCatFlag, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.TX.Tax.directTax, IBqlBool>.IsEqual<True>>>, And<BqlOperand<TaxZoneDet.taxID, IBqlString>.IsNotNull>>>.And<BqlOperand<TaxCategoryDet.taxID, IBqlString>.IsNotEqual<P.AsString>>>.Aggregate<To<GroupBy<TaxCategoryDet.taxCategoryID>, GroupBy<TaxZoneDet.taxZoneID>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) taxZoneIds,
      (object) taxCategoryIds,
      (object) taxId
    }))
      ((PXResultset<TaxCategoryDet>) invalidZoneCategoryCombinations).Add((PXResult<TaxCategoryDet>) pxResult);
    return ((PXResultset<TaxCategoryDet>) invalidZoneCategoryCombinations).Count == 0;
  }

  protected bool TryValidateTaxCombinations(
    PXCache cache,
    PX.Objects.TX.Tax newTax,
    PX.Objects.TX.Tax oldTax,
    out string errorMessage)
  {
    errorMessage = string.Empty;
    string[] array = GraphHelper.RowCast<TaxCategoryDet>((IEnumerable) ((PXSelectBase<TaxCategoryDet>) this.Categories).Select(Array.Empty<object>())).Select<TaxCategoryDet, string>((Func<TaxCategoryDet, string>) (a => a.TaxCategoryID)).ToArray<string>();
    PXResultset<TaxCategoryDet> invalidCategoryCombinations;
    if (!this.TryValidateDirectIndirectTaxCombinationWithCategories(array, newTax.TaxID, newTax.DirectTax.GetValueOrDefault(), out invalidCategoryCombinations))
    {
      TaxCategoryDet taxCategoryDet = PXResultset<TaxCategoryDet>.op_Implicit(invalidCategoryCombinations);
      errorMessage = PXMessages.LocalizeFormat("Direct-entry and non-direct-entry taxes cannot be included in the same tax category ({0}).", new object[1]
      {
        (object) taxCategoryDet?.TaxCategoryID
      });
      return false;
    }
    PXResultset<TaxCategoryDet, TaxZoneDet> invalidZoneCategoryCombinations;
    if (this.TryValidteDirectTaxCombinationWithTaxZoneAndTaxCategory(GraphHelper.RowCast<TaxZoneDet>((IEnumerable) ((PXSelectBase<TaxZoneDet>) this.Zones).Select(Array.Empty<object>())).Select<TaxZoneDet, string>((Func<TaxZoneDet, string>) (a => a.TaxZoneID)).ToArray<string>(), array, ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current?.TaxID, out invalidZoneCategoryCombinations))
      return true;
    TaxCategoryDet taxCategoryDet1 = PXResultset<TaxCategoryDet, TaxZoneDet>.op_Implicit(invalidZoneCategoryCombinations);
    TaxZoneDet taxZoneDet = PXResultset<TaxCategoryDet, TaxZoneDet>.op_Implicit(invalidZoneCategoryCombinations);
    errorMessage = PXMessages.LocalizeFormat("Multiple direct-entry taxes cannot be included in the same tax category ({0}) and tax zone ({1}).", new object[2]
    {
      (object) taxCategoryDet1?.TaxCategoryID,
      (object) taxZoneDet?.TaxZoneID
    });
    return false;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax> e)
  {
    PX.Objects.TX.Tax row = e.Row;
    if (row == null)
      return;
    if (row.DirectTax.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax>>) e).Cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxApplyTermsDisc>((object) row, (object) "N", (Exception) null);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcLevel2Exclude>((object) row, (object) true);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.taxApplyTermsDisc>((object) row, (object) "N");
      string str = "I0";
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcLevel>((object) row, (object) "0");
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcType>((object) row, (object) "I");
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcRule>((object) row, (object) str);
    }
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.directTax>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.taxCalcLevel2Exclude>((object) row, (object) false);
  }

  protected virtual void Tax_TaxUOM_FieldVerifying(PX.Data.Events.FieldVerifying<PX.Objects.TX.Tax, PX.Objects.TX.Tax.taxUOM> e)
  {
    if (e.Row?.TaxType != "Q" || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.TX.Tax, PX.Objects.TX.Tax.taxUOM>, PX.Objects.TX.Tax, object>) e).NewValue != null)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.TX.Tax, PX.Objects.TX.Tax.taxUOM>>) e).Cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxUOM>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.TX.Tax.taxUOM>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.TX.Tax, PX.Objects.TX.Tax.taxUOM>>) e).Cache)
    }));
  }

  protected virtual void Tax_PurchTaxAcctIDOverride_FieldUpdated(
    PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxAcctIDOverride> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxAcctIDOverride>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.purchTaxAcctID>((object) e.Row, e.NewValue);
  }

  protected virtual void Tax_PurchTaxSubIDOverride_FieldUpdated(
    PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxSubIDOverride> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxSubIDOverride>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.purchTaxSubID>((object) e.Row, e.NewValue);
  }

  protected virtual void Tax_SalesTaxAcctIDOverride_FieldUpdated(
    PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxAcctIDOverride> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxAcctIDOverride>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.salesTaxAcctID>((object) e.Row, e.NewValue);
  }

  protected virtual void Tax_SalesTaxSubIDOverride_FieldUpdated(
    PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxSubIDOverride> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxSubIDOverride>>) e).Cache.SetValueExt<PX.Objects.TX.Tax.salesTaxSubID>((object) e.Row, e.NewValue);
  }

  public virtual void Tax_SalesTaxAcctID_ExceptionHandling(
    PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxAcctID> e)
  {
    this.MapErrorFromOriginalFieldToSubstitute<PX.Objects.TX.Tax.salesTaxAcctIDOverride>(((PX.Data.Events.Event<PXExceptionHandlingEventArgs, PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxAcctID>>) e).Cache, e.Row, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxAcctID>, PX.Objects.TX.Tax, object>) e).NewValue, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxAcctID>>) e).Exception);
  }

  public virtual void Tax_SalesTaxSubIDOverride_ExceptionHandling(
    PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxSubID> e)
  {
    this.MapErrorFromOriginalFieldToSubstitute<PX.Objects.TX.Tax.salesTaxSubIDOverride>(((PX.Data.Events.Event<PXExceptionHandlingEventArgs, PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxSubID>>) e).Cache, e.Row, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxSubID>, PX.Objects.TX.Tax, object>) e).NewValue, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.salesTaxSubID>>) e).Exception);
  }

  public virtual void Tax_PurchTaxAcctIDOverride_ExceptionHandling(
    PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxAcctID> e)
  {
    this.MapErrorFromOriginalFieldToSubstitute<PX.Objects.TX.Tax.purchTaxAcctIDOverride>(((PX.Data.Events.Event<PXExceptionHandlingEventArgs, PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxAcctID>>) e).Cache, e.Row, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxAcctID>, PX.Objects.TX.Tax, object>) e).NewValue, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxAcctID>>) e).Exception);
  }

  public virtual void Tax_PurchTaxSubIDOverride_ExceptionHandling(
    PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxSubID> e)
  {
    this.MapErrorFromOriginalFieldToSubstitute<PX.Objects.TX.Tax.purchTaxSubIDOverride>(((PX.Data.Events.Event<PXExceptionHandlingEventArgs, PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxSubID>>) e).Cache, e.Row, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxSubID>, PX.Objects.TX.Tax, object>) e).NewValue, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.TX.Tax, PX.Objects.TX.Tax.purchTaxSubID>>) e).Exception);
  }

  private void MapErrorFromOriginalFieldToSubstitute<TSubstituteAccountField>(
    PXCache cache,
    PX.Objects.TX.Tax tax,
    object newValue,
    Exception exception)
    where TSubstituteAccountField : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TSubstituteAccountField>
  {
    if (tax?.TaxType != "Q")
      return;
    cache.RaiseExceptionHandling<TSubstituteAccountField>((object) tax, newValue, exception);
  }

  protected virtual void Tax_PurchTaxAcctID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.SetAccountIDOrSubIDWithValidation<PX.Objects.TX.Tax.purchTaxAcctID>(sender, e.Row as PX.Objects.TX.Tax, e.NewValue as int?);
  }

  protected virtual void Tax_PurchTaxSubID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.SetAccountIDOrSubIDWithValidation<PX.Objects.TX.Tax.purchTaxSubID>(sender, e.Row as PX.Objects.TX.Tax, e.NewValue as int?);
  }

  protected virtual void Tax_SalesTaxAcctID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.SetAccountIDOrSubIDWithValidation<PX.Objects.TX.Tax.salesTaxAcctID>(sender, e.Row as PX.Objects.TX.Tax, e.NewValue as int?);
  }

  protected virtual void Tax_SalesTaxSubID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.SetAccountIDOrSubIDWithValidation<PX.Objects.TX.Tax.salesTaxSubID>(sender, e.Row as PX.Objects.TX.Tax, e.NewValue as int?);
  }

  private void SetAccountIDOrSubIDWithValidation<Field>(
    PXCache sender,
    PX.Objects.TX.Tax tax,
    int? newAccountOrSubValue)
    where Field : IBqlField
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.taxEntryFromGL>())
    {
      PX.Objects.TX.Tax copy = (PX.Objects.TX.Tax) sender.CreateCopy((object) tax);
      sender.SetValue<Field>((object) copy, (object) newAccountOrSubValue);
      int? purchTaxAcctId = copy.PurchTaxAcctID;
      int? nullable = copy.SalesTaxAcctID;
      if (purchTaxAcctId.GetValueOrDefault() == nullable.GetValueOrDefault() & purchTaxAcctId.HasValue == nullable.HasValue)
      {
        nullable = copy.PurchTaxSubID;
        int? salesTaxSubId = copy.SalesTaxSubID;
        if (nullable.GetValueOrDefault() == salesTaxSubId.GetValueOrDefault() & nullable.HasValue == salesTaxSubId.HasValue)
        {
          PXSetPropertyException propertyException = new PXSetPropertyException("Tax Claimable and Tax Payable accounts and subaccounts for Tax {0} are the same. It's impossible to enter this Tax via GL in this configuration.", (PXErrorLevel) 2, new object[1]
          {
            (object) copy.TaxID
          });
          sender.RaiseExceptionHandling<PX.Objects.TX.Tax.purchTaxAcctID>((object) tax, (object) null, (Exception) propertyException);
          sender.RaiseExceptionHandling<PX.Objects.TX.Tax.purchTaxSubID>((object) tax, (object) null, (Exception) propertyException);
          sender.RaiseExceptionHandling<PX.Objects.TX.Tax.salesTaxAcctID>((object) tax, (object) null, (Exception) propertyException);
          sender.RaiseExceptionHandling<PX.Objects.TX.Tax.salesTaxSubID>((object) tax, (object) null, (Exception) propertyException);
        }
      }
    }
    sender.SetValue<Field>((object) tax, (object) newAccountOrSubValue);
  }

  protected virtual void Tax_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.TX.Tax row))
      return;
    bool flag1 = PXSelectBase<TaxZone, PXSelect<TaxZone, Where<TaxZone.taxID, Equal<Required<TaxZone.taxID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TaxID
    }).Count != 0;
    bool flag2 = row.OutDate.HasValue && row.OutDate.Value.CompareTo((object) ((PXGraph) this).Accessinfo.BusinessDate) < 0;
    if (((PXSelectBase) this.Tax).Cache.GetStatus((object) row) != 1)
      ((PXSelectBase) this.TaxRevisions).Cache.SetAllEditPermissions(!flag2);
    bool valueOrDefault1 = row.IsExternal.GetValueOrDefault();
    ((PXSelectBase) this.TaxRevisions).Cache.SetAllEditPermissions(!valueOrDefault1);
    ((PXSelectBase) this.Categories).Cache.SetAllEditPermissions(!valueOrDefault1);
    ((PXSelectBase) this.Zones).Cache.SetAllEditPermissions(!valueOrDefault1);
    bool flag3 = row.TaxType == "S";
    bool flag4 = row.TaxType == "V";
    bool isPerUnitTax = row.TaxType == "Q";
    bool flag5 = row.TaxType == "P";
    bool? nullable = row.DeductibleVAT;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = row.DirectTax;
    bool valueOrDefault3 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.reverseTax>(cache, (object) row, flag4 && !valueOrDefault1 & !flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.pendingTax>(cache, (object) row, flag4 && !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.directTax>(cache, (object) row, flag4 && !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.statisticalTax>(cache, (object) row, flag4 && !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.exemptTax>(cache, (object) row, flag4 && !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.deductibleVAT>(cache, (object) row, flag4 && !valueOrDefault1 & !flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.includeInTaxable>(cache, (object) row, flag4 && !valueOrDefault1);
    this.SetGLAccounts(cache, row);
    PXUIFieldAttribute.SetVisible<PX.Objects.TX.Tax.reportExpenseToSingleAccount>(cache, (object) row, valueOrDefault2 | flag5 | flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.reportExpenseToSingleAccount>(cache, (object) row, valueOrDefault2 | flag5 | flag3);
    this.SetTaxRevisionsUIPropertiesForTax(row);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.taxType>(cache, (object) row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.taxCalcType>(cache, (object) row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.taxCalcRule>(cache, (object) row, !valueOrDefault1 && !valueOrDefault3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.taxCalcLevel2Exclude>(cache, (object) row, !valueOrDefault1 && !isPerUnitTax && !valueOrDefault3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.taxApplyTermsDisc>(cache, (object) row, !valueOrDefault1 && !isPerUnitTax && !valueOrDefault3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.TX.Tax.outDate>(cache, (object) row, !valueOrDefault1);
    PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) row).For<PX.Objects.TX.Tax.taxUOM>((Action<PXUIFieldAttribute>) (a => a.Enabled = a.Required = a.Visible = isPerUnitTax)).For<PX.Objects.TX.Tax.perUnitTaxPostMode>((Action<PXUIFieldAttribute>) (a => a.Enabled = a.Visible = isPerUnitTax));
    this.PopulateBucketList(row);
    this.SetAllowedTaxCalculationRulesList(row);
    this.SetAllowedTaxTermDiscountsList(row);
    this.SetPrintingSettingsTabUI(row);
    if (!valueOrDefault2 || !(row.TaxApplyTermsDisc == "P"))
      return;
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.deductibleVAT>((object) row, (object) row.DeductibleVAT, (Exception) new PXSetPropertyException("The combination of a partially deductible VAT and a cash discount that reduces taxable amount on early payment is not supported.", (PXErrorLevel) 4));
    cache.RaiseExceptionHandling<PX.Objects.TX.Tax.taxApplyTermsDisc>((object) row, (object) row.TaxApplyTermsDisc, (Exception) new PXSetPropertyException("The combination of a partially deductible VAT and a cash discount that reduces taxable amount on early payment is not supported.", (PXErrorLevel) 4));
  }

  protected virtual void SetGLAccounts(PXCache cache, PX.Objects.TX.Tax tax)
  {
    bool requireExpenseAccount = this.ExpenseAccountRequired(tax);
    this.SetSalesTaxGAccountAndSubPersistingCheck(cache, tax);
    this.SetExpenseGLAccountAndSubPersistingCheck(cache, tax, requireExpenseAccount);
    this.SetSalesTaxGLAccountUI(cache, tax);
    this.SetPurchaiseTaxGLAccountUI(cache, tax);
    this.SetRetainageGLAccountUI(cache, tax);
    this.SetExpenseGLAccountUI(cache, tax, requireExpenseAccount);
    this.SetPendingGLAccountsUI(cache, tax);
  }

  private void SetSalesTaxGAccountAndSubPersistingCheck(PXCache cache, PX.Objects.TX.Tax tax)
  {
    bool flag1 = tax.TaxType == "Q";
    bool flag2 = tax.PerUnitTaxPostMode == "T";
    PXPersistingCheck actualPersistingCheck = !flag1 || flag2 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2;
    PXCacheEx.Adjust<PXDefaultAttribute>(cache, (object) tax).For<PX.Objects.TX.Tax.salesTaxAcctID>((Action<PXDefaultAttribute>) (a => a.PersistingCheck = actualPersistingCheck)).SameFor<PX.Objects.TX.Tax.salesTaxSubID>();
  }

  private void SetExpenseGLAccountAndSubPersistingCheck(
    PXCache cache,
    PX.Objects.TX.Tax tax,
    bool requireExpenseAccount)
  {
    PXPersistingCheck actualPersistingCheck = requireExpenseAccount ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXCacheEx.Adjust<PXDefaultAttribute>(cache, (object) tax).For<PX.Objects.TX.Tax.expenseAccountID>((Action<PXDefaultAttribute>) (a => a.PersistingCheck = actualPersistingCheck)).SameFor<PX.Objects.TX.Tax.expenseSubID>();
  }

  private void SetSalesTaxGLAccountUI(PXCache cache, PX.Objects.TX.Tax tax)
  {
    bool isSalesTaxAccountUsed = tax.TaxType != "Q";
    bool isOverrideAccountUsed = tax.TaxType == "Q" && tax.PerUnitTaxPostMode == "T";
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) tax).For<PX.Objects.TX.Tax.salesTaxAcctID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = a.Required = isSalesTaxAccountUsed)).SameFor<PX.Objects.TX.Tax.salesTaxSubID>();
    chained = chained.For<PX.Objects.TX.Tax.salesTaxAcctIDOverride>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = a.Required = isOverrideAccountUsed));
    chained.SameFor<PX.Objects.TX.Tax.salesTaxSubIDOverride>();
  }

  private void SetPurchaiseTaxGLAccountUI(PXCache cache, PX.Objects.TX.Tax tax)
  {
    bool isTaxClaimableAccountUsed = tax.TaxType == "V";
    bool isOverrideAccountUsed = tax.TaxType == "Q" && tax.PerUnitTaxPostMode == "T";
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) tax).For<PX.Objects.TX.Tax.purchTaxAcctID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = a.Required = isTaxClaimableAccountUsed)).SameFor<PX.Objects.TX.Tax.purchTaxSubID>();
    chained = chained.For<PX.Objects.TX.Tax.purchTaxAcctIDOverride>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = a.Required = isOverrideAccountUsed));
    chained.SameFor<PX.Objects.TX.Tax.purchTaxSubIDOverride>();
  }

  private void SetRetainageGLAccountUI(PXCache cache, PX.Objects.TX.Tax tax)
  {
    bool isPerUnitTax = tax.TaxType == "Q";
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) tax).For<PX.Objects.TX.Tax.retainageTaxPayableAcctID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = !isPerUnitTax));
    chained = chained.SameFor<PX.Objects.TX.Tax.retainageTaxPayableSubID>();
    chained = chained.SameFor<PX.Objects.TX.Tax.retainageTaxClaimableAcctID>();
    chained.SameFor<PX.Objects.TX.Tax.retainageTaxClaimableSubID>();
  }

  private void SetExpenseGLAccountUI(PXCache cache, PX.Objects.TX.Tax tax, bool requireExpenseAccount)
  {
    bool isPerUnitTax = tax.TaxType == "Q";
    bool enabled = tax.ReportExpenseToSingleAccount.GetValueOrDefault() && (tax.TaxType == "P" || tax.TaxType == "S" || tax.DeductibleVAT.GetValueOrDefault());
    PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) tax).For<PX.Objects.TX.Tax.expenseAccountID>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = enabled;
      a.Required = requireExpenseAccount;
      a.Visible = !isPerUnitTax;
    })).SameFor<PX.Objects.TX.Tax.expenseSubID>();
  }

  protected virtual void SetPendingGLAccountsUI(PXCache cache, PX.Objects.TX.Tax tax)
  {
    bool isPending = tax.PendingTax.GetValueOrDefault();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) tax).For<PX.Objects.TX.Tax.pendingSalesTaxAcctID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = isPending));
    chained = chained.SameFor<PX.Objects.TX.Tax.pendingSalesTaxSubID>();
    chained = chained.SameFor<PX.Objects.TX.Tax.pendingPurchTaxAcctID>();
    chained.SameFor<PX.Objects.TX.Tax.pendingPurchTaxSubID>();
  }

  /// <summary>
  /// Sets "Printing Settings" tab user interface  for the given <paramref name="tax" />. An extensibility point.
  /// Additional logic for the fields displayed on the tab could be added here.
  /// </summary>
  /// <param name="tax">The tax.</param>
  protected virtual void SetPrintingSettingsTabUI(PX.Objects.TX.Tax tax)
  {
    ((PXSelectBase) this.TaxForPrintingParametersTab).AllowSelect = this.IsPrintingSettingsTabVisible(tax);
  }

  /// <summary>
  /// Check if Printing Settings tab should be visible for the given <paramref name="tax" />. An extensibility point.
  /// </summary>
  /// <param name="tax">The tax.</param>
  /// <returns />
  protected virtual bool IsPrintingSettingsTabVisible(PX.Objects.TX.Tax tax)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.perUnitTaxSupport>();
  }

  public virtual void SetAllowedTaxCalculationRulesList(PX.Objects.TX.Tax tax)
  {
    (string, string)[] valueTupleArray;
    if (tax?.TaxType == "Q")
      valueTupleArray = new (string, string)[2]
      {
        ("I1", "Exclusive Line-Level"),
        ("I0", "Inclusive Line-Level")
      };
    else
      valueTupleArray = new (string, string)[6]
      {
        ("I0", "Inclusive Line-Level"),
        ("I1", "Exclusive Line-Level"),
        ("I2", "Compound Line-Level"),
        ("D0", "Inclusive Document-Level"),
        ("D1", "Exclusive Document-Level"),
        ("D2", "Compound Document-Level")
      };
    PXStringListAttribute.SetList<PX.Objects.TX.Tax.taxCalcRule>(((PXSelectBase) this.Tax).Cache, (object) null, valueTupleArray);
  }

  public virtual void SetAllowedTaxTermDiscountsList(PX.Objects.TX.Tax tax)
  {
    int num = tax.TaxType == "V" ? 1 : 0;
    bool flag = tax.TaxType == "Q";
    if (num != 0)
      PXStringListAttribute.SetList<PX.Objects.TX.Tax.taxApplyTermsDisc>(((PXSelectBase) this.Tax).Cache, (object) tax, (PXStringListAttribute) new CSTaxTermsDiscount.ListAttribute());
    else if (flag)
      PXStringListAttribute.SetList<PX.Objects.TX.Tax.taxApplyTermsDisc>(((PXSelectBase) this.Tax).Cache, (object) tax, new string[1]
      {
        "N"
      }, new string[1]{ "Does Not Affect Taxable Amount" });
    else
      PXStringListAttribute.SetList<PX.Objects.TX.Tax.taxApplyTermsDisc>(((PXSelectBase) this.Tax).Cache, (object) tax, new string[2]
      {
        "X",
        "N"
      }, new string[2]
      {
        "Reduces Taxable Amount",
        "Does Not Affect Taxable Amount"
      });
  }

  protected virtual void SetTaxRevisionsUIPropertiesForTax(PX.Objects.TX.Tax tax)
  {
    bool valueOrDefault = tax.DeductibleVAT.GetValueOrDefault();
    bool isPerUnitTax = tax.TaxType == "Q";
    PXCache cache = ((PXSelectBase) this.TaxRevisions).Cache;
    bool? exemptTax = tax.ExemptTax;
    bool flag = false;
    int num = exemptTax.GetValueOrDefault() == flag & exemptTax.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<TaxRev.taxRate>(cache, (object) null, num != 0);
    PXUIFieldAttribute.SetEnabled<TaxRev.nonDeductibleTaxRate>(((PXSelectBase) this.TaxRevisions).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<TaxRev.nonDeductibleTaxRate>(((PXSelectBase) this.TaxRevisions).Cache, (object) null, valueOrDefault);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.TaxRevisions).Cache, (object) null).For<TaxRev.taxableMaxQty>((Action<PXUIFieldAttribute>) (a => a.Enabled = a.Visible = isPerUnitTax));
    chained = chained.For<TaxRev.taxableMin>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = !isPerUnitTax && (tax.TaxCalcLevel == "1" || tax.TaxCalcLevel == "2");
      a.Visible = !isPerUnitTax;
    }));
    chained.SameFor<TaxRev.taxableMax>();
    PXUIFieldAttribute.SetDisplayName<TaxRev.taxRate>(((PXSelectBase) this.TaxRevisions).Cache, isPerUnitTax ? "Tax Amount" : "Tax Rate");
  }

  protected void ThrowFieldIsEmpty<Field>(PXCache sender, PXRowPersistingEventArgs e) where Field : IBqlField
  {
    sender.RaiseExceptionHandling<Field>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<Field>(sender)
    }));
  }

  public void CheckFieldIsEmpty<Field>(PXCache sender, PXRowPersistingEventArgs e) where Field : IBqlField
  {
    if (sender.GetValue<Field>(e.Row) != null)
      return;
    this.ThrowFieldIsEmpty<Field>(sender, e);
  }

  protected virtual void Tax_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    PX.Objects.TX.Tax row = (PX.Objects.TX.Tax) e.Row;
    if (this.ExpenseAccountRequired(row))
    {
      this.CheckFieldIsEmpty<PX.Objects.TX.Tax.expenseAccountID>(sender, e);
      this.CheckFieldIsEmpty<PX.Objects.TX.Tax.expenseSubID>(sender, e);
    }
    if (row.TaxType == "V" || row.TaxType == "Q" && row.PerUnitTaxPostMode == "T")
    {
      this.CheckFieldIsEmpty<PX.Objects.TX.Tax.purchTaxAcctID>(sender, e);
      this.CheckFieldIsEmpty<PX.Objects.TX.Tax.purchTaxSubID>(sender, e);
    }
    if (row.TaxType == "Q")
      this.CheckFieldIsEmpty<PX.Objects.TX.Tax.taxUOM>(sender, e);
    if (!row.PendingTax.GetValueOrDefault())
      return;
    this.CheckFieldIsEmpty<PX.Objects.TX.Tax.pendingSalesTaxAcctID>(sender, e);
    this.CheckFieldIsEmpty<PX.Objects.TX.Tax.pendingSalesTaxSubID>(sender, e);
    this.CheckFieldIsEmpty<PX.Objects.TX.Tax.pendingPurchTaxAcctID>(sender, e);
    this.CheckFieldIsEmpty<PX.Objects.TX.Tax.pendingPurchTaxSubID>(sender, e);
  }

  protected bool ExpenseAccountRequired(PX.Objects.TX.Tax tax)
  {
    if (!tax.ReportExpenseToSingleAccount.GetValueOrDefault())
      return false;
    if (tax.TaxType == "P" || tax.DeductibleVAT.GetValueOrDefault())
      return true;
    return tax.TaxType == "S" && ((IEnumerable<PXResult<TaxRev>>) ((PXSelectBase<TaxRev>) this.TaxRevisions).Select(Array.Empty<object>())).ToList<PXResult<TaxRev>>().Any<PXResult<TaxRev>>((Func<PXResult<TaxRev>, bool>) (_ => PXResult<TaxRev>.op_Implicit(_).TaxType == "P"));
  }

  public virtual void Persist()
  {
    this.PrepareRevisions();
    ((PXGraph) this).Persist();
  }

  protected virtual void TaxRev_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    this.ForcePersisting();
  }

  private bool UpdateRevisions(
    PXCache sender,
    TaxRev item,
    PXResultset<TaxRev> summary,
    bool PerformUpdate)
  {
    foreach (PXResult<TaxRev> pxResult in summary)
    {
      TaxRev taxRev = PXResult<TaxRev>.op_Implicit(pxResult);
      if (!sender.ObjectsEqual((object) taxRev, (object) item))
      {
        if (PerformUpdate)
        {
          taxRev.TaxBucketID = item.TaxBucketID;
          taxRev.TaxableMax = item.TaxableMax;
          taxRev.TaxableMin = item.TaxableMin;
          taxRev.TaxRate = item.TaxRate;
          taxRev.Outdated = item.Outdated;
          ((PXSelectBase) this.TaxRevisions).Cache.Update((object) taxRev);
        }
        return true;
      }
    }
    return false;
  }

  protected virtual void TaxRev_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is TaxRev row))
      return;
    int? nullable = ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current.TaxVendorID;
    if (!nullable.HasValue)
    {
      nullable = row.TaxBucketID;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case -2:
            row.TaxType = "S";
            break;
          case -1:
            row.TaxType = "P";
            break;
        }
      }
    }
    else
    {
      nullable = row.TaxBucketID;
      if (nullable.HasValue)
      {
        TaxBucket taxBucket = PXResultset<TaxBucket>.op_Implicit(PXSelectBase<TaxBucket, PXSelect<TaxBucket, Where<TaxBucket.vendorID, Equal<Current<PX.Objects.TX.Tax.taxVendorID>>, And<TaxBucket.bucketID, Equal<Required<TaxBucket.bucketID>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.TaxBucketID
        }));
        if (taxBucket != null)
          row.TaxType = taxBucket.BucketType;
      }
    }
    PX.Objects.TX.Tax current = ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current;
    bool taxIsDeductibleVAT = current.TaxType == "V" && current.DeductibleVAT.GetValueOrDefault();
    bool taxIsWithholding = current.TaxType == "W";
    (bool IsCompatible, string ErrorMsg) = this.CheckTaxRevisionCompatibilityWithTax(row, taxIsDeductibleVAT, taxIsWithholding);
    if (!IsCompatible)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException<TaxRev.taxType>(ErrorMsg);
    }
    PXResultset<TaxRev> summary = PXSelectBase<TaxRev, PXSelect<TaxRev, Where<TaxRev.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>, And<TaxRev.taxType, Equal<Required<TaxRev.taxType>>, And<TaxRev.startDate, Equal<Required<TaxRev.startDate>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.TaxType,
      (object) row.StartDate
    });
    ((CancelEventArgs) e).Cancel = this.UpdateRevisions(sender, row, summary, true);
  }

  protected virtual void TaxRev_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxRev row))
      return;
    PXUIFieldAttribute.SetEnabled<TaxRev.nonDeductibleTaxRate>(sender, (object) row, row.TaxType != "S");
    PXUIFieldAttribute.SetEnabled<TaxRev.nonDeductibleTaxRate>(sender, (object) row, row.TaxType != "S");
  }

  protected virtual void TaxRev_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is TaxRev newRow))
      return;
    int? nullable = ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current.TaxVendorID;
    if (!nullable.HasValue)
    {
      nullable = newRow.TaxBucketID;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case -2:
            newRow.TaxType = "S";
            break;
          case -1:
            newRow.TaxType = "P";
            break;
        }
      }
    }
    else
    {
      nullable = newRow.TaxBucketID;
      if (nullable.HasValue)
      {
        TaxBucket taxBucket = PXResultset<TaxBucket>.op_Implicit(PXSelectBase<TaxBucket, PXSelect<TaxBucket, Where<TaxBucket.vendorID, Equal<Current<PX.Objects.TX.Tax.taxVendorID>>, And<TaxBucket.bucketID, Equal<Required<TaxBucket.bucketID>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) newRow.TaxBucketID
        }));
        if (taxBucket != null)
          newRow.TaxType = taxBucket.BucketType;
      }
    }
    PXResultset<TaxRev> summary = PXSelectBase<TaxRev, PXSelect<TaxRev, Where<TaxRev.taxID, Equal<Current<PX.Objects.TX.Tax.taxID>>, And<TaxRev.taxType, Equal<Required<TaxRev.taxType>>, And<TaxRev.startDate, Equal<Required<TaxRev.startDate>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) newRow.TaxType,
      (object) newRow.StartDate
    });
    ((CancelEventArgs) e).Cancel = this.UpdateRevisions(sender, newRow, summary, false);
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void TaxRev_TaxType_CacheAttached(PXCache sender)
  {
  }

  protected virtual void TaxRev_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    DateTime? startDate1 = ((TaxRev) e.OldRow).StartDate;
    DateTime? startDate2 = ((TaxRev) e.Row).StartDate;
    if ((startDate1.HasValue == startDate2.HasValue ? (startDate1.HasValue ? (startDate1.GetValueOrDefault() != startDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    this.ForcePersisting();
  }

  protected virtual void TaxRev_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    this.ForcePersisting();
  }

  private void ForcePersisting()
  {
    GraphHelper.MarkUpdated(((PXSelectBase) this.Tax).Cache, (object) ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current);
  }

  private void PrepareRevisions()
  {
    PX.Objects.TX.Tax current = ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current;
    if (((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Current == null)
      return;
    DateTime? nullable1 = current.OutDate;
    if (nullable1.HasValue && !current.Outdated.Value)
    {
      ((PXSelectBase<TaxRev>) this.TaxRevisions).Insert(new TaxRev()
      {
        StartDate = current.OutDate,
        TaxType = "S"
      });
      if (current.TaxType == "V")
        ((PXSelectBase<TaxRev>) this.TaxRevisions).Insert(new TaxRev()
        {
          StartDate = current.OutDate,
          TaxType = "P"
        });
      current.Outdated = new bool?(true);
      ((PXSelectBase<PX.Objects.TX.Tax>) this.Tax).Update(current);
    }
    DateTime? nullable2 = new DateTime?();
    DateTime? nullable3 = new DateTime?();
    foreach (PXResult<TaxRev> pxResult in ((PXSelectBase<TaxRev>) this.TaxRevisions).Select(Array.Empty<object>()))
    {
      TaxRev taxRev1 = PXResult<TaxRev>.op_Implicit(pxResult);
      TaxRev taxRev2 = (TaxRev) ((PXSelectBase) this.TaxRevisions).Cache.Locate((object) taxRev1) ?? taxRev1;
      if (taxRev2.TaxType == "S" && !nullable2.HasValue || taxRev2.TaxType == "P" && !nullable3.HasValue)
      {
        nullable1 = current.OutDate;
        if (nullable1.HasValue)
        {
          TaxRev taxRev3 = taxRev2;
          nullable1 = current.OutDate;
          DateTime? nullable4 = new DateTime?(nullable1.Value);
          taxRev3.EndDate = nullable4;
        }
        else
        {
          nullable1 = taxRev2.EndDate;
          DateTime defaultEndDate = taxRev2.GetDefaultEndDate();
          if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() != defaultEndDate ? 1 : 0) : 1) != 0)
            ((PXSelectBase) this.TaxRevisions).Cache.SetDefaultExt<TaxRev.endDate>((object) taxRev2);
        }
      }
      nullable1 = current.OutDate;
      if (nullable1.HasValue)
      {
        nullable1 = current.OutDate;
        if (nullable1.Value.CompareTo((object) taxRev2.StartDate) <= 0)
        {
          taxRev2.Outdated = new bool?(true);
          goto label_17;
        }
      }
      taxRev2.Outdated = new bool?(false);
label_17:
      DateTime? nullable5;
      if (taxRev2.TaxType == "S")
      {
        if (nullable2.HasValue)
        {
          nullable1 = taxRev2.EndDate;
          nullable5 = nullable2;
          if ((nullable1.HasValue == nullable5.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable5.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
            taxRev2.EndDate = nullable2;
        }
        nullable5 = taxRev2.StartDate;
        nullable2 = new DateTime?(nullable5.Value.AddDays(-1.0));
      }
      else if (taxRev2.TaxType == "P")
      {
        if (nullable3.HasValue)
        {
          nullable5 = taxRev2.EndDate;
          nullable1 = nullable3;
          if ((nullable5.HasValue == nullable1.HasValue ? (nullable5.HasValue ? (nullable5.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
            taxRev2.EndDate = nullable3;
        }
        nullable1 = taxRev2.StartDate;
        nullable3 = new DateTime?(nullable1.Value.AddDays(-1.0));
      }
      ((PXSelectBase<TaxRev>) this.TaxRevisions).Update(taxRev2);
    }
  }

  protected virtual void TaxZoneDet_TaxID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = ((PXSelectBase) this.Tax).Cache.Current is PX.Objects.TX.Tax current ? (object) current.TaxID : (object) (string) null;
  }

  protected virtual void TaxCategoryDet_TaxID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = ((PXSelectBase) this.Tax).Cache.Current is PX.Objects.TX.Tax current ? (object) current.TaxID : (object) (string) null;
  }
}
