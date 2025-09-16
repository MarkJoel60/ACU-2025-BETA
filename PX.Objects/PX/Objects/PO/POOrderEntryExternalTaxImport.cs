// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderEntryExternalTaxImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.MassProcess;
using PX.Objects.Common.Extensions;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO;

public class POOrderEntryExternalTaxImport : PXGraphExtension<POOrderEntryExternalTax, POOrderEntry>
{
  [PXVirtualDAC]
  public PXFilter<POTaxTranImported> ImportedTaxes;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    typeof (FieldValue).GetCustomAttributes(typeof (PXVirtualAttribute), false);
  }

  protected virtual void _(Events.RowInserting<POTaxTran> e)
  {
    if (e.Row == null)
      return;
    POTaxTran row = e.Row;
    POOrder current = ((PXSelectBase<POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current;
    if (!e.ExternalCall || current == null || !current.ExternalTaxesImportInProgress.GetValueOrDefault() || !((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<POTaxTran>>) e).Cache.Graph.IsContractBasedAPI)
      return;
    POTaxTranImported instance = (POTaxTranImported) ((PXSelectBase) this.ImportedTaxes).Cache.CreateInstance();
    ((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Cache.RestoreCopy((object) instance, (object) row);
    ((PXSelectBase<POTaxTranImported>) this.ImportedTaxes).Insert(instance);
    foreach (PXResult<POTaxTran> pxResult in ((PXSelectBase<POTaxTran>) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>()))
    {
      POTaxTran poTaxTran = PXResult<POTaxTran>.op_Implicit(pxResult);
      if (((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Cache.GetStatus((object) poTaxTran) == null && string.Equals(poTaxTran.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
        ((PXSelectBase<POTaxTran>) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Delete(poTaxTran);
    }
    foreach (TaxDetail taxDetail in ((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Cache.Inserted)
    {
      if (string.Equals(taxDetail.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
      {
        e.Cancel = true;
        break;
      }
    }
  }

  protected virtual void _(
    Events.FieldVerifying<POTaxTran, POTaxTran.taxID> e)
  {
    if (e.Row == null)
      return;
    POOrder current1 = ((PXSelectBase<POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current;
    PX.Objects.TX.TaxZone current2 = ((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<POOrderEntry>) this).Base.taxzone).Current;
    if (current1 == null || current2 == null)
      return;
    bool? nullable = current1.ExternalTaxesImportInProgress;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current2.IsExternal;
    if (!nullable.GetValueOrDefault())
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<POTaxTran, POTaxTran.taxID>>) e).Cancel = true;
  }

  [PXOverride]
  public virtual void InsertImportedTaxes()
  {
    POOrder current1 = ((PXSelectBase<POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current;
    if (current1 == null || !current1.ExternalTaxesImportInProgress.GetValueOrDefault() || !((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base).IsContractBasedAPI || this.Base1.skipExternalTaxCalcOnSave)
      return;
    if (((PXSelectBase<POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current == null)
      return;
    try
    {
      if (!NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.ImportedTaxes).Cache.Inserted))
      {
        TaxBaseAttribute.SetTaxCalc<POLine.taxCategoryID>(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
        foreach (PXResult<POTaxTran> pxResult in ((PXSelectBase<POTaxTran>) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>()))
          ((PXSelectBase<POTaxTran>) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Delete(PXResult<POTaxTran>.op_Implicit(pxResult));
      }
      else
      {
        PX.Objects.TX.TaxZone current2 = ((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<POOrderEntry>) this).Base.taxzone).Current;
        int num1 = ((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<POOrderEntry>) this).Base.taxzone).Current == null ? 0 : (((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<POOrderEntry>) this).Base.taxzone).Current.IsExternal.GetValueOrDefault() ? 1 : 0);
        GetTaxResult getTaxResult = new GetTaxResult();
        List<TaxLine> taxLineList = new List<TaxLine>();
        List<TaxDetail> taxDetailList = new List<TaxDetail>();
        Decimal num2 = 0M;
        if (num1 != 0)
        {
          foreach (POTaxTranImported poTaxTranImported in ((PXSelectBase) this.ImportedTaxes).Cache.Inserted)
          {
            Decimal? nullable = poTaxTranImported.CuryTaxableAmt;
            Decimal valueOrDefault1 = nullable.GetValueOrDefault();
            nullable = poTaxTranImported.CuryTaxAmt;
            Decimal valueOrDefault2 = nullable.GetValueOrDefault();
            Decimal num3;
            if (poTaxTranImported.TaxRate.IsNullOrZero())
            {
              if (!poTaxTranImported.CuryTaxableAmt.IsNullOrZero())
              {
                nullable = poTaxTranImported.CuryTaxAmt;
                Decimal valueOrDefault3 = nullable.GetValueOrDefault();
                nullable = poTaxTranImported.CuryTaxableAmt;
                Decimal num4 = nullable ?? 1M;
                num3 = Decimal.Round(valueOrDefault3 / num4, 6);
              }
              else
                num3 = 0M;
            }
            else
            {
              nullable = poTaxTranImported.TaxRate;
              num3 = nullable.GetValueOrDefault();
            }
            Decimal num5 = num3;
            TaxDetail taxDetail = new TaxDetail()
            {
              TaxName = poTaxTranImported.TaxID,
              TaxableAmount = valueOrDefault1,
              TaxAmount = valueOrDefault2,
              Rate = num5
            };
            Decimal num6 = num2;
            nullable = poTaxTranImported.CuryTaxAmt;
            Decimal valueOrDefault4 = nullable.GetValueOrDefault();
            num2 = num6 + valueOrDefault4;
            taxDetailList.Add(taxDetail);
          }
          getTaxResult.TaxSummary = taxDetailList.ToArray();
          getTaxResult.TotalTaxAmount = num2;
          ((PXSelectBase) this.ImportedTaxes).Cache.Clear();
          using (new PXTimeStampScope((byte[]) null))
            this.Base1.ApplyExternalTaxes(current1, getTaxResult, getTaxResult);
        }
        else
        {
          List<KeyValuePair<string, Dictionary<string, string>>> source = new List<KeyValuePair<string, Dictionary<string, string>>>();
          foreach (POTaxTran poTaxTran in ((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Cache.Cached)
          {
            ((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Cache.GetStatus((object) poTaxTran);
            Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Cache, (object) poTaxTran, Array.Empty<PXErrorLevel>());
            if (errors.Count != 0)
              source.Add(new KeyValuePair<string, Dictionary<string, string>>(poTaxTran.TaxID, errors));
          }
          if (source.Any<KeyValuePair<string, Dictionary<string, string>>>())
          {
            string str = string.Empty;
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in source)
              str = $"{str}{$"Tax {keyValuePair.Key} was not imported. Error: {keyValuePair.Value.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value)).Aggregate<string>((Func<string, string, string>) ((e1, e2) => $"{e1}; {e2}"))}"} ";
            throw new PXException(str);
          }
          TaxBaseAttribute.SetTaxCalc<POLine.taxCategoryID>(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
          PXResultset<POTaxTran> pxResultset = ((PXSelectBase<POTaxTran>) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>());
          foreach (PXResult<POTaxTran> pxResult in pxResultset)
          {
            POTaxTran taxTran = PXResult<POTaxTran>.op_Implicit(pxResult);
            if (this.GetMatchingTax(taxTran) == null)
              ((PXSelectBase<POTaxTran>) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Delete(taxTran);
          }
          foreach (PXResult<POTaxTran> pxResult in pxResultset)
          {
            POTaxTran taxTran = PXResult<POTaxTran>.op_Implicit(pxResult);
            POTaxTranImported matchingTax = this.GetMatchingTax(taxTran);
            if (matchingTax != null)
            {
              if (matchingTax.CuryTaxableAmt.HasValue)
                taxTran.CuryTaxableAmt = matchingTax.CuryTaxableAmt;
              if (matchingTax.CuryTaxAmt.HasValue)
                taxTran.CuryTaxAmt = matchingTax.CuryTaxAmt;
              ((PXSelectBase<POTaxTran>) ((PXGraphExtension<POOrderEntry>) this).Base.Taxes).Update(taxTran);
            }
          }
        }
      }
    }
    finally
    {
      ((PXSelectBase) this.ImportedTaxes).Cache.Clear();
    }
  }

  public virtual POTaxTranImported GetMatchingTax(POTaxTran taxTran)
  {
    POTaxTranImported matchingTax = (POTaxTranImported) null;
    foreach (POTaxTranImported poTaxTranImported in ((PXSelectBase) this.ImportedTaxes).Cache.Inserted)
    {
      if (string.Equals(poTaxTranImported.TaxID, taxTran.TaxID, StringComparison.OrdinalIgnoreCase))
        matchingTax = poTaxTranImported;
    }
    return matchingTax;
  }
}
