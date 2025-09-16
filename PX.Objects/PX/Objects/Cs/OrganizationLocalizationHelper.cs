// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.OrganizationLocalizationHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public static class OrganizationLocalizationHelper
{
  public static CurrentLocalization GetCurrentLocalization(PXGraph graph)
  {
    return new CurrentLocalization(OrganizationLocalizationHelper.GetCurrentLocalizationCode(graph));
  }

  public static string GetCurrentLocalizationCode(PXGraph graph)
  {
    return OrganizationLocalizationHelper.GetCurrentLocalizationCode(graph.Views[graph.PrimaryView]);
  }

  public static CurrentLocalization GetCurrentLocalization(PXView primaryView)
  {
    return new CurrentLocalization(OrganizationLocalizationHelper.GetCurrentLocalizationCode(primaryView));
  }

  public static string GetCurrentLocalizationCode(PXView primaryView)
  {
    string str1 = ((List<string>) primaryView.Cache.Fields).Find((Predicate<string>) (s => string.Equals(s, "branchID", StringComparison.OrdinalIgnoreCase)));
    if (!string.IsNullOrEmpty(str1))
      return OrganizationLocalizationHelper.GetCurrentLocalizationCodeForBranch(primaryView.Cache.GetValue(primaryView.Cache.Current, str1) as int?);
    string str2 = ((List<string>) primaryView.Cache.Fields).Find((Predicate<string>) (s => string.Equals(s, "organizationID", StringComparison.OrdinalIgnoreCase)));
    return !string.IsNullOrEmpty(str2) ? OrganizationLocalizationHelper.GetCurrentLocalizationCodeForOrg(primaryView.Cache.GetValue(primaryView.Cache.Current, str2) as int?) : "00";
  }

  public static string GetCurrentLocalizationCodeForBranch(int? branchId)
  {
    if (branchId.HasValue)
    {
      PXAccess.Organization parentOrganization = PXAccess.GetParentOrganization(branchId);
      if (!string.IsNullOrEmpty(parentOrganization?.OrganizationLocalizationCode))
        return parentOrganization?.OrganizationLocalizationCode;
    }
    return "00";
  }

  public static string GetCurrentLocalizationCodeForOrg(int? organizationId)
  {
    if (organizationId.HasValue)
    {
      PXAccess.Organization organizationById = (PXAccess.Organization) PXAccess.GetOrganizationByID(organizationId);
      if (!string.IsNullOrEmpty(organizationById?.OrganizationLocalizationCode))
        return organizationById?.OrganizationLocalizationCode;
    }
    return "00";
  }
}
