// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.MigrationMode.APMigrationModeDependentInvoiceTypeListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP.MigrationMode;

public class APMigrationModeDependentInvoiceTypeListAttribute : APInvoiceType.ListAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    APSetup apSetup = (APSetup) PXSelectBase<APSetup, PXSelectReadonly<APSetup>.Config>.Select(sender.Graph);
    if (apSetup == null || !apSetup.MigrationMode.GetValueOrDefault())
    {
      base.CacheAttached(sender);
    }
    else
    {
      this._AllowedValues = new string[5]
      {
        "INV",
        "ADR",
        "ACR",
        "PPM",
        "PPI"
      };
      this._AllowedLabels = new string[5]
      {
        "Bill",
        "Debit Adj.",
        "Credit Adj.",
        "Prepayment",
        "Prepmt. Invoice"
      };
      this._NeutralAllowedLabels = new string[5]
      {
        "Bill",
        "Debit Adj.",
        "Credit Adj.",
        "Prepayment",
        "Prepmt. Invoice"
      };
      base.CacheAttached(sender);
    }
  }
}
