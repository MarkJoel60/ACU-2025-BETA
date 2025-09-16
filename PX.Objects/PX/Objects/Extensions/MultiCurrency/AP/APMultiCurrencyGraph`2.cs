// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.AP.APMultiCurrencyGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency.AP;

public abstract class APMultiCurrencyGraph<TGraph, TPrimary> : 
  FinDocMultiCurrencyGraph<TGraph, TPrimary>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  protected override string Module => "AP";

  protected override IEnumerable<System.Type> FieldWhichShouldBeRecalculatedAnyway
  {
    get
    {
      yield return typeof (APInvoice.curyDocBal);
      yield return typeof (APInvoice.curyDiscBal);
    }
  }

  protected override MultiCurrencyGraph<TGraph, TPrimary>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<TGraph, TPrimary>.CurySourceMapping(typeof (Vendor));
  }

  protected override bool ShouldBeDisabledDueToDocStatus()
  {
    string documentStatus = this.DocumentStatus;
    if (documentStatus != null && documentStatus.Length == 1)
    {
      switch (documentStatus[0])
      {
        case 'C':
        case 'E':
        case 'K':
        case 'N':
        case 'U':
        case 'V':
        case 'Y':
        case 'Z':
          return true;
      }
    }
    return false;
  }
}
