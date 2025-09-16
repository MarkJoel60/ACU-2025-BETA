// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SalesTaxMaintVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.TX;

public class SalesTaxMaintVATRecognitionOnPrepayments : PXGraphExtension<SalesTaxMaint>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>() || PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAP>();
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  [PXOverride]
  public virtual void SetPendingGLAccountsUI(PXCache cache, Tax tax)
  {
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  [PXOverride]
  public virtual void ResetPendingSalesTax(
    PXCache cache,
    Tax newTax,
    SalesTaxMaintVATRecognitionOnPrepayments.ResetPendingSalesTaxDelegate baseMethod)
  {
    baseMethod(cache, newTax);
  }

  [PXOverride]
  public virtual void SetGLAccounts(PXCache cache, Tax tax)
  {
    this.SetOnAPARPrepaymentAccountsUI(cache, tax);
  }

  protected virtual void SetOnAPARPrepaymentAccountsUI(PXCache cache, Tax tax)
  {
    bool isVAT = tax.TaxType == "V";
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained;
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
    {
      chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) tax).For<Tax.onARPrepaymentTaxAcctID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = isVAT));
      chained.SameFor<Tax.onARPrepaymentTaxSubID>();
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAP>())
      return;
    chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) tax).For<Tax.onAPPrepaymentTaxAcctID>((Action<PXUIFieldAttribute>) (a => a.Visible = a.Enabled = isVAT));
    chained.SameFor<Tax.onAPPrepaymentTaxSubID>();
  }

  public virtual void Tax_TaxVendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Tax row))
      return;
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
    {
      sender.SetDefaultExt<Tax.onARPrepaymentTaxAcctID>((object) row);
      sender.SetDefaultExt<Tax.onARPrepaymentTaxSubID>((object) row);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAP>())
      return;
    sender.SetDefaultExt<Tax.onAPPrepaymentTaxAcctID>((object) row);
    sender.SetDefaultExt<Tax.onAPPrepaymentTaxSubID>((object) row);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public delegate void ResetPendingSalesTaxDelegate(PXCache cache, Tax newTax);
}
