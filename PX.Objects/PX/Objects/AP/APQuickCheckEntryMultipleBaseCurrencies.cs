// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APQuickCheckEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.Standalone;
using PX.Objects.Common;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

public sealed class APQuickCheckEntryMultipleBaseCurrencies : PXGraphExtension<APQuickCheckEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  protected void _(PX.Data.Events.FieldVerifying<APQuickCheck.branchID> e)
  {
    if (e.NewValue == null)
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<APQuickCheck.branchID>(e.Cache, e.Row, (object) (int) e.NewValue) as PX.Objects.GL.Branch;
    BAccountR baccountR = PXSelectorAttribute.Select<APQuickCheck.vendorID>(e.Cache, e.Row) as BAccountR;
    if (branch != null && baccountR != null && baccountR.BaseCuryID != null && branch.BaseCuryID != baccountR.BaseCuryID)
    {
      e.NewValue = (object) branch.BranchCD;
      throw new PXSetPropertyException("The branch base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD(baccountR.VOrgBAccountID),
        (object) baccountR.AcctCD
      });
    }
  }
}
