// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PXOrgAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

public static class PXOrgAccess
{
  public static string GetBaseCuryID(string bAccountCD)
  {
    string str = bAccountCD?.Trim();
    if (string.IsNullOrEmpty(str))
      return (string) null;
    string baseCuryId = PXAccess.GetBranch(PXAccess.GetBranchID(str))?.BaseCuryID;
    if (baseCuryId != null)
      return baseCuryId;
    return ((PXAccess.Organization) PXAccess.GetOrganizationByID(PXAccess.GetOrganizationID(str)))?.BaseCuryID;
  }

  public static string GetBaseCuryID(int? bAccountID)
  {
    if (!bAccountID.HasValue)
      return (string) null;
    string baseCuryId = PXAccess.GetBranchByBAccountID(bAccountID)?.BaseCuryID;
    if (baseCuryId != null)
      return baseCuryId;
    return ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(bAccountID))?.BaseCuryID;
  }

  public static string GetCD(int? bAccountID)
  {
    if (!bAccountID.HasValue)
      return (string) null;
    string branchCd = PXAccess.GetBranchByBAccountID(bAccountID)?.BranchCD;
    if (branchCd != null)
      return branchCd;
    return ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(bAccountID))?.OrganizationCD;
  }
}
