// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AccBalanceByAssetInqMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FA;

public class AccBalanceByAssetInqMultipleBaseCurrencies : PXGraphExtension<AccBalanceByAssetInq>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public virtual BqlCommand GetSelectCommand(
    AccBalanceByAssetInq.AccBalanceByAssetFilter filter,
    AccBalanceByAssetInqMultipleBaseCurrencies.GetSelectCommandDelegate baseDelegate)
  {
    BqlCommand selectCommand = baseDelegate(filter);
    if (!filter.OrganizationID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Current2<AccBalanceByAssetInq.AccBalanceByAssetFilter.organizationID>>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>();
    return selectCommand;
  }

  public delegate BqlCommand GetSelectCommandDelegate(
    AccBalanceByAssetInq.AccBalanceByAssetFilter filter);
}
