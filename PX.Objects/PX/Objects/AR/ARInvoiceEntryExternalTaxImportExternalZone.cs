// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntryExternalTaxImportExternalZone
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.TaxProvider;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntryExternalTaxImportExternalZone : 
  PXGraphExtension<ARInvoiceEntryExternalTax, ARInvoiceEntryExternalTaxImport, ARInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  [PXOverride]
  public virtual bool IsSkipExternalTaxCalcOnSave() => this.Base2.skipExternalTaxCalcOnSave;

  [PXOverride]
  public virtual void OnExternalTaxZone(ARInvoice invoice)
  {
    GetTaxResult result = new GetTaxResult();
    List<TaxLine> taxLineList = new List<TaxLine>();
    List<TaxDetail> taxDetailList = new List<TaxDetail>();
    Decimal num1 = 0M;
    Sign documentSign = this.Base2.GetDocumentSign(invoice);
    foreach (ARTaxTranImported arTaxTranImported in ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntryExternalTaxImport, ARInvoiceEntry>) this).Base1.ImportedTaxes).Cache.Inserted)
    {
      Sign sign1 = documentSign;
      Decimal? nullable1 = arTaxTranImported.CuryTaxableAmt;
      Decimal? nullable2;
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(Sign.op_Multiply(sign1, nullable1.GetValueOrDefault()));
      nullable2 = nullable3;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      Sign sign2 = documentSign;
      nullable1 = arTaxTranImported.CuryTaxAmt;
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(Sign.op_Multiply(sign2, nullable1.GetValueOrDefault()));
      nullable2 = nullable4;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num2;
      if (arTaxTranImported.TaxRate.IsNullOrZero())
      {
        if (!arTaxTranImported.CuryTaxableAmt.IsNullOrZero())
        {
          nullable1 = arTaxTranImported.CuryTaxAmt;
          Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
          nullable1 = arTaxTranImported.CuryTaxableAmt;
          Decimal num3 = nullable1 ?? 1M;
          num2 = Decimal.Round(valueOrDefault3 / num3, 6);
        }
        else
          num2 = 0M;
      }
      else
      {
        nullable1 = arTaxTranImported.TaxRate;
        num2 = nullable1.GetValueOrDefault();
      }
      Decimal num4 = num2;
      TaxDetail taxDetail = new TaxDetail()
      {
        TaxName = arTaxTranImported.TaxID,
        TaxableAmount = valueOrDefault1,
        TaxAmount = valueOrDefault2,
        Rate = num4
      };
      if (arTaxTranImported.LineNbr.GetValueOrDefault() == 32000)
      {
        TaxLine taxLine = new TaxLine()
        {
          Index = (int) short.MinValue,
          TaxableAmount = valueOrDefault1,
          TaxAmount = valueOrDefault2,
          Rate = num4
        };
        taxLineList.Add(taxLine);
      }
      Decimal num5 = num1;
      nullable1 = arTaxTranImported.CuryTaxAmt;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      num1 = num5 + valueOrDefault4;
      taxDetailList.Add(taxDetail);
    }
    result.TaxSummary = taxDetailList.ToArray();
    result.TotalTaxAmount = Sign.op_Multiply(documentSign, num1);
    ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntryExternalTaxImport, ARInvoiceEntry>) this).Base1.ImportedTaxes).Cache.Clear();
    using (new PXTimeStampScope((byte[]) null))
      this.Base2.ApplyTax(invoice, result);
  }
}
