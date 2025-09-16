// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AccBalanceByAssetInqMultipleCalendarsSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FA;

public class AccBalanceByAssetInqMultipleCalendarsSupport : PXGraphExtension<AccBalanceByAssetInq>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>();
  }

  [PXOverride]
  public virtual BqlCommand GetSelectCommand(
    AccBalanceByAssetInq.AccBalanceByAssetFilter filter,
    AccBalanceByAssetInqMultipleCalendarsSupport.GetSelectCommandDelegate baseDelegate)
  {
    BqlCommand selectCommand = baseDelegate(filter);
    if (!filter.OrganizationID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Current2<AccBalanceByAssetInq.AccBalanceByAssetFilter.organizationID>>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>();
    return selectCommand;
  }

  public delegate BqlCommand GetSelectCommandDelegate(
    AccBalanceByAssetInq.AccBalanceByAssetFilter filter);
}
