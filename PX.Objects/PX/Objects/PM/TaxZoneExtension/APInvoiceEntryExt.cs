// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.APInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class APInvoiceEntryExt : PXGraphExtension<APInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Default<PX.Objects.AP.APInvoice.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APInvoice.taxZoneID> _)
  {
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(PX.Objects.AP.APInvoice row, Func<PX.Objects.AP.APInvoice, string> baseMethod)
  {
    if (row.Status == "X")
      return row.TaxZoneID;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) row?.ProjectID);
    return string.IsNullOrEmpty(pmProject?.CostTaxZoneID) ? baseMethod(row) : pmProject.CostTaxZoneID;
  }

  [PXOverride]
  public virtual void InvoicePOOrder(
    PX.Objects.PO.POOrder order,
    bool createNew,
    bool keepOrderTaxes,
    Action<PX.Objects.PO.POOrder, bool, bool> baseMethod)
  {
    baseMethod(order, createNew, keepOrderTaxes);
    if (createNew || ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current == null)
      return;
    ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<PX.Objects.AP.APInvoice.taxZoneID>((object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current);
  }

  [PXOverride]
  public virtual void ProcessPOOrderLines(
    IEnumerable<IAPTranSource> orderlines,
    HashSet<PX.Objects.AP.APTran> duplicates,
    bool keepOrderTaxes,
    Action<IEnumerable<IAPTranSource>, HashSet<PX.Objects.AP.APTran>, bool> baseMethod)
  {
    baseMethod(orderlines, duplicates, keepOrderTaxes);
    if (keepOrderTaxes || ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current == null)
      return;
    ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<PX.Objects.AP.APInvoice.taxZoneID>((object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current);
  }
}
