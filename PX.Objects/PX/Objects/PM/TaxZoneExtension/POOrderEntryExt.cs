// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.POOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class POOrderEntryExt : PXGraphExtension<POOrderEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Default<POOrder.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<POOrder.taxZoneID> _)
  {
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(POOrder row, Func<POOrder, string> baseMethod)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) row?.ProjectID);
    return string.IsNullOrEmpty(pmProject?.CostTaxZoneID) ? baseMethod(row) : pmProject.CostTaxZoneID;
  }

  [PXOverride]
  public virtual void AddPOLine(
    POOrderEntry.POLineS aLine,
    bool blanked,
    Action<POOrderEntry.POLineS, bool> baseMethod)
  {
    baseMethod(aLine, blanked);
    if (((PXSelectBase<POOrder>) this.Base.Document).Current == null)
      return;
    ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<POOrder.taxZoneID>((object) ((PXSelectBase<POOrder>) this.Base.Document).Current);
  }
}
