// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.ExpenseClaimDetailEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class ExpenseClaimDetailEntryExt : PXGraphExtension<ExpenseClaimDetailEntry>
{
  [PXMergeAttributes]
  [PXFormula(typeof (Default<EPExpenseClaimDetails.contractID>))]
  protected virtual void EPExpenseClaimDetails_TaxZoneID_CacheAttached(PXCache cache)
  {
  }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(
    EPExpenseClaimDetails row,
    Func<EPExpenseClaimDetails, string> baseMethod)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) row?.ContractID);
    return pmProject != null && !string.IsNullOrEmpty(pmProject.CostTaxZoneID) ? pmProject.CostTaxZoneID : baseMethod(row);
  }
}
