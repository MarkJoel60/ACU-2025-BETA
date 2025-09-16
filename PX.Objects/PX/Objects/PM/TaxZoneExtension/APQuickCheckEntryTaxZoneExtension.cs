// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.APQuickCheckEntryTaxZoneExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class APQuickCheckEntryTaxZoneExtension : PXGraphExtension<APQuickCheckEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Default<PX.Objects.AP.APRegister.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APQuickCheck.taxZoneID> _)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APQuickCheck, APQuickCheck.taxZoneID> e)
  {
    APQuickCheck row = e.Row;
    if (row == null)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, row.ProjectID);
    if (string.IsNullOrEmpty(pmProject?.CostTaxZoneID))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APQuickCheck, APQuickCheck.taxZoneID>, APQuickCheck, object>) e).NewValue = (object) pmProject.CostTaxZoneID;
  }
}
