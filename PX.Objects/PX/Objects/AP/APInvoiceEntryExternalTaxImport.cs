// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryExternalTaxImport
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
namespace PX.Objects.AP;

public class APInvoiceEntryExternalTaxImport : 
  PXGraphExtension<APInvoiceEntryExternalTax, APInvoiceEntry>
{
  [PXVirtualDAC]
  public PXFilter<APTaxTranImported> ImportedTaxes;

  public override void Initialize()
  {
    base.Initialize();
    typeof (FieldValue).GetCustomAttributes(typeof (PXVirtualAttribute), false);
  }

  protected virtual void _(PX.Data.Events.RowInserting<APTaxTran> e)
  {
    if (e.Row == null)
      return;
    if (e.Cache.Current == null && e.Cache.Graph.IsImport)
      e.Cache.Current = NonGenericIEnumerableExtensions.FirstOrDefault_(e.Cache.Inserted);
    APTaxTran row = e.Row;
    APInvoice current = this.Base.Document.Current;
    if (!e.ExternalCall || current == null || !current.ExternalTaxesImportInProgress.GetValueOrDefault() || !e.Cache.Graph.IsContractBasedAPI)
      return;
    APTaxTranImported instance = (APTaxTranImported) this.ImportedTaxes.Cache.CreateInstance();
    this.Base.Taxes.Cache.RestoreCopy((object) instance, (object) row);
    this.ImportedTaxes.Insert(instance);
    foreach (PXResult<APTaxTran> pxResult in this.Base.Taxes.Select())
    {
      APTaxTran apTaxTran = (APTaxTran) pxResult;
      if (this.Base.Taxes.Cache.GetStatus((object) apTaxTran) == PXEntryStatus.Notchanged && string.Equals(apTaxTran.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
        this.Base.Taxes.Delete(apTaxTran);
    }
    foreach (TaxDetail taxDetail in this.Base.Taxes.Cache.Inserted)
    {
      if (string.Equals(taxDetail.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
      {
        e.Cancel = true;
        break;
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APTaxTran, APTaxTran.taxID> e)
  {
    if (e.Row == null)
      return;
    APInvoice current1 = this.Base.Document.Current;
    PX.Objects.TX.TaxZone current2 = this.Base.taxzone.Current;
    if (current1 == null || current2 == null)
      return;
    bool? nullable = current1.ExternalTaxesImportInProgress;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current2.IsExternal;
    if (!nullable.GetValueOrDefault())
      return;
    e.Cancel = true;
  }

  [PXOverride]
  public virtual void InsertImportedTaxes()
  {
    APInvoice current1 = this.Base.Document.Current;
    if (current1 == null || !current1.ExternalTaxesImportInProgress.GetValueOrDefault() || !this.Base.IsContractBasedAPI || this.Base1.skipExternalTaxCalcOnSave)
      return;
    if (this.Base.Document.Current == null)
      return;
    try
    {
      if (!NonGenericIEnumerableExtensions.Any_(this.ImportedTaxes.Cache.Inserted))
      {
        TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
        foreach (PXResult<APTaxTran> pxResult in this.Base.Taxes.Select())
          this.Base.Taxes.Delete((APTaxTran) pxResult);
      }
      else
      {
        PX.Objects.TX.TaxZone current2 = this.Base.taxzone.Current;
        int num1 = this.Base.taxzone.Current == null ? 0 : (this.Base.taxzone.Current.IsExternal.GetValueOrDefault() ? 1 : 0);
        GetTaxResult result = new GetTaxResult();
        List<TaxLine> taxLineList = new List<TaxLine>();
        List<TaxDetail> taxDetailList = new List<TaxDetail>();
        Decimal num2 = 0M;
        if (num1 != 0)
        {
          Sign documentSign = this.Base1.GetDocumentSign(current1);
          foreach (APTaxTranImported apTaxTranImported in this.ImportedTaxes.Cache.Inserted)
          {
            Sign sign1 = documentSign;
            Decimal? nullable = apTaxTranImported.CuryTaxableAmt;
            Decimal valueOrDefault1 = (nullable.HasValue ? new Decimal?(Sign.op_Multiply(sign1, nullable.GetValueOrDefault())) : new Decimal?()).GetValueOrDefault();
            Sign sign2 = documentSign;
            nullable = apTaxTranImported.CuryTaxAmt;
            Decimal valueOrDefault2 = (nullable.HasValue ? new Decimal?(Sign.op_Multiply(sign2, nullable.GetValueOrDefault())) : new Decimal?()).GetValueOrDefault();
            Decimal num3;
            if (apTaxTranImported.TaxRate.IsNullOrZero())
            {
              if (!apTaxTranImported.CuryTaxableAmt.IsNullOrZero())
              {
                nullable = apTaxTranImported.CuryTaxAmt;
                Decimal valueOrDefault3 = nullable.GetValueOrDefault();
                nullable = apTaxTranImported.CuryTaxableAmt;
                Decimal num4 = nullable ?? 1M;
                num3 = Decimal.Round(valueOrDefault3 / num4, 6);
              }
              else
                num3 = 0M;
            }
            else
            {
              nullable = apTaxTranImported.TaxRate;
              num3 = nullable.GetValueOrDefault();
            }
            Decimal num5 = num3;
            TaxDetail taxDetail = new TaxDetail()
            {
              TaxName = apTaxTranImported.TaxID,
              TaxableAmount = valueOrDefault1,
              TaxAmount = valueOrDefault2,
              Rate = num5
            };
            Decimal num6 = num2;
            nullable = apTaxTranImported.CuryTaxAmt;
            Decimal valueOrDefault4 = nullable.GetValueOrDefault();
            num2 = num6 + valueOrDefault4;
            taxDetailList.Add(taxDetail);
          }
          result.TaxSummary = taxDetailList.ToArray();
          result.TotalTaxAmount = Sign.op_Multiply(documentSign, num2);
          this.ImportedTaxes.Cache.Clear();
          using (new PXTimeStampScope((byte[]) null))
            this.Base1.ApplyExternalTaxes(current1, result);
        }
        else
        {
          List<KeyValuePair<string, Dictionary<string, string>>> source = new List<KeyValuePair<string, Dictionary<string, string>>>();
          foreach (APTaxTran data in this.Base.Taxes.Cache.Cached)
          {
            int status = (int) this.Base.Taxes.Cache.GetStatus((object) data);
            Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(this.Base.Taxes.Cache, (object) data);
            if (errors.Count != 0)
              source.Add(new KeyValuePair<string, Dictionary<string, string>>(data.TaxID, errors));
          }
          if (source.Any<KeyValuePair<string, Dictionary<string, string>>>())
          {
            string message = string.Empty;
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in source)
              message = $"{message}{$"Tax {keyValuePair.Key} was not imported. Error: {keyValuePair.Value.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value)).Aggregate<string>((Func<string, string, string>) ((e1, e2) => $"{e1}; {e2}"))}"} ";
            throw new PXException(message);
          }
          TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Base.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
          PXResultset<APTaxTran> pxResultset = this.Base.Taxes.Select();
          foreach (PXResult<APTaxTran> pxResult in pxResultset)
          {
            APTaxTran taxTran = (APTaxTran) pxResult;
            if (this.GetMatchingTax(taxTran) == null)
              this.Base.Taxes.Delete(taxTran);
          }
          foreach (PXResult<APTaxTran> pxResult in pxResultset)
          {
            APTaxTran taxTran = (APTaxTran) pxResult;
            APTaxTranImported matchingTax = this.GetMatchingTax(taxTran);
            if (matchingTax != null)
            {
              if (matchingTax.CuryTaxableAmt.HasValue)
                taxTran.CuryTaxableAmt = matchingTax.CuryTaxableAmt;
              if (matchingTax.CuryTaxAmt.HasValue)
                taxTran.CuryTaxAmt = matchingTax.CuryTaxAmt;
              this.Base.Taxes.Update(taxTran);
            }
          }
        }
      }
    }
    finally
    {
      this.ImportedTaxes.Cache.Clear();
    }
  }

  public virtual APTaxTranImported GetMatchingTax(APTaxTran taxTran)
  {
    APTaxTranImported matchingTax = (APTaxTranImported) null;
    foreach (APTaxTranImported apTaxTranImported in this.ImportedTaxes.Cache.Inserted)
    {
      if (string.Equals(apTaxTranImported.TaxID, taxTran.TaxID, StringComparison.OrdinalIgnoreCase))
        matchingTax = apTaxTranImported;
    }
    return matchingTax;
  }
}
