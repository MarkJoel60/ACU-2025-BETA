// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReleaseProcessExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.TX;
using PX.TaxProvider;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CAReleaseProcessExternalTax : PXGraphExtension<CAReleaseProcess>
{
  protected Func<PXGraph, string, ITaxProvider> TaxProviderFactory;
  protected Lazy<CATranEntry> LazyCaTranEntry = new Lazy<CATranEntry>((Func<CATranEntry>) (() => PXGraph.CreateInstance<CATranEntry>()));

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  public virtual void Initialize()
  {
    this.TaxProviderFactory = ExternalTaxBase<PXGraph>.TaxProviderFactory;
  }

  public virtual bool IsExternalTax(string taxZoneID)
  {
    return ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, taxZoneID);
  }

  [PXOverride]
  public virtual void OnBeforeRelease(CAAdj doc)
  {
    if (doc == null || doc.IsTaxValid.GetValueOrDefault() || !this.IsExternalTax(doc.TaxZoneID))
      return;
    CATranEntry caTranEntry = this.LazyCaTranEntry.Value;
    ((PXGraph) caTranEntry).Clear();
    caTranEntry.CalculateExternalTax(doc);
  }

  [PXOverride]
  public virtual CAAdj CommitExternalTax(CAAdj doc)
  {
    if (doc != null && doc.IsTaxValid.GetValueOrDefault() && !doc.NonTaxable.GetValueOrDefault() && this.IsExternalTax(doc.TaxZoneID) && !doc.IsTaxPosted.GetValueOrDefault() && TaxPluginMaint.IsActive((PXGraph) this.Base, doc.TaxZoneID))
    {
      ITaxProvider itaxProvider = ExternalTaxBase<PXGraph>.TaxProviderFactory((PXGraph) this.Base, doc.TaxZoneID);
      CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
      ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current = doc;
      CommitTaxRequest commitTaxRequest = ((PXGraph) instance).GetExtension<CATranEntryExternalTax>().BuildCommitTaxRequest(doc);
      if (((ResultBase) itaxProvider.CommitTax(commitTaxRequest)).IsSuccess)
        doc.IsTaxPosted = new bool?(true);
    }
    return doc;
  }
}
