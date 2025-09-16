// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntryExternalTaxImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.MassProcess;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntryExternalTaxImport : PXGraphExtension<ARInvoiceEntry>
{
  [PXVirtualDAC]
  public PXFilter<ARTaxTranImported> ImportedTaxes;

  public static bool IsActive() => true;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    typeof (FieldValue).GetCustomAttributes(typeof (PXVirtualAttribute), false);
  }

  protected virtual void _(Events.RowInserting<ARTaxTran> e)
  {
    if (e.Row == null)
      return;
    if (((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<ARTaxTran>>) e).Cache.Current == null && ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<ARTaxTran>>) e).Cache.Graph.IsImport)
      ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<ARTaxTran>>) e).Cache.Current = NonGenericIEnumerableExtensions.FirstOrDefault_(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<ARTaxTran>>) e).Cache.Inserted);
    ARTaxTran row = e.Row;
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
    if (!e.ExternalCall || current == null || !current.ExternalTaxesImportInProgress.GetValueOrDefault() || !((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<ARTaxTran>>) e).Cache.Graph.IsContractBasedAPI)
      return;
    ARTaxTranImported instance = (ARTaxTranImported) ((PXSelectBase) this.ImportedTaxes).Cache.CreateInstance();
    ((PXSelectBase) this.Base.Taxes).Cache.RestoreCopy((object) instance, (object) row);
    ((PXSelectBase<ARTaxTranImported>) this.ImportedTaxes).Insert(instance);
    foreach (PXResult<ARTaxTran> pxResult in ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
    {
      ARTaxTran arTaxTran = PXResult<ARTaxTran>.op_Implicit(pxResult);
      if (((PXSelectBase) this.Base.Taxes).Cache.GetStatus((object) arTaxTran) == null && string.Equals(arTaxTran.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
        ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Delete(arTaxTran);
    }
    foreach (TaxDetail taxDetail in ((PXSelectBase) this.Base.Taxes).Cache.Inserted)
    {
      if (string.Equals(taxDetail.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
      {
        e.Cancel = true;
        break;
      }
    }
  }

  protected virtual void _(
    Events.FieldVerifying<ARTaxTran, ARTaxTran.taxID> e)
  {
    if (e.Row == null)
      return;
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
    PX.Objects.TX.TaxZone current2 = ((PXSelectBase<PX.Objects.TX.TaxZone>) this.Base.taxzone).Current;
    if (current1 == null || current2 == null)
      return;
    bool? nullable = current1.ExternalTaxesImportInProgress;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current2.IsExternal;
    if (!nullable.GetValueOrDefault())
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<ARTaxTran, ARTaxTran.taxID>>) e).Cancel = true;
  }

  [PXOverride]
  public virtual void InsertImportedTaxes()
  {
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
    if (current1 == null || !current1.ExternalTaxesImportInProgress.GetValueOrDefault() || !((PXGraph) this.Base).IsContractBasedAPI || this.IsSkipExternalTaxCalcOnSave())
      return;
    if (((PXSelectBase<ARInvoice>) this.Base.Document).Current == null)
      return;
    try
    {
      if (!NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.ImportedTaxes).Cache.Inserted))
      {
        TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
        foreach (PXResult<ARTaxTran> pxResult in ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
          ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Delete(PXResult<ARTaxTran>.op_Implicit(pxResult));
      }
      else
      {
        PX.Objects.TX.TaxZone current2 = ((PXSelectBase<PX.Objects.TX.TaxZone>) this.Base.taxzone).Current;
        if ((((PXSelectBase<PX.Objects.TX.TaxZone>) this.Base.taxzone).Current == null ? 0 : (((PXSelectBase<PX.Objects.TX.TaxZone>) this.Base.taxzone).Current.IsExternal.GetValueOrDefault() ? 1 : 0)) != 0)
        {
          this.OnExternalTaxZone(current1);
        }
        else
        {
          List<KeyValuePair<string, Dictionary<string, string>>> source = new List<KeyValuePair<string, Dictionary<string, string>>>();
          foreach (ARTaxTran arTaxTran in ((PXSelectBase) this.Base.Taxes).Cache.Cached)
          {
            ((PXSelectBase) this.Base.Taxes).Cache.GetStatus((object) arTaxTran);
            Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(((PXSelectBase) this.Base.Taxes).Cache, (object) arTaxTran, Array.Empty<PXErrorLevel>());
            if (errors.Count != 0)
              source.Add(new KeyValuePair<string, Dictionary<string, string>>(arTaxTran.TaxID, errors));
          }
          if (source.Any<KeyValuePair<string, Dictionary<string, string>>>())
          {
            string str = string.Empty;
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in source)
              str = $"{str}{$"Tax {keyValuePair.Key} was not imported. Error: {keyValuePair.Value.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value)).Aggregate<string>((Func<string, string, string>) ((e1, e2) => $"{e1}; {e2}"))}"} ";
            throw new PXException(str);
          }
          TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
          PXResultset<ARTaxTran> pxResultset = ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Select(Array.Empty<object>());
          foreach (PXResult<ARTaxTran> pxResult in pxResultset)
          {
            ARTaxTran taxTran = PXResult<ARTaxTran>.op_Implicit(pxResult);
            if (this.GetMatchingTax(taxTran) == null)
              ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Delete(taxTran);
          }
          foreach (PXResult<ARTaxTran> pxResult in pxResultset)
          {
            ARTaxTran taxTran = PXResult<ARTaxTran>.op_Implicit(pxResult);
            ARTaxTranImported matchingTax = this.GetMatchingTax(taxTran);
            if (matchingTax != null)
            {
              if (matchingTax.CuryTaxableAmt.HasValue)
                taxTran.CuryTaxableAmt = matchingTax.CuryTaxableAmt;
              if (matchingTax.CuryTaxAmt.HasValue)
                taxTran.CuryTaxAmt = matchingTax.CuryTaxAmt;
              ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Update(taxTran);
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

  public virtual ARTaxTranImported GetMatchingTax(ARTaxTran taxTran)
  {
    ARTaxTranImported matchingTax = (ARTaxTranImported) null;
    foreach (ARTaxTranImported arTaxTranImported in ((PXSelectBase) this.ImportedTaxes).Cache.Inserted)
    {
      if (string.Equals(arTaxTranImported.TaxID, taxTran.TaxID, StringComparison.OrdinalIgnoreCase))
        matchingTax = arTaxTranImported;
    }
    return matchingTax;
  }

  public virtual bool IsSkipExternalTaxCalcOnSave() => false;

  public virtual void OnExternalTaxZone(ARInvoice invoice)
  {
  }
}
