// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.LicenseWarningService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SP;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
public class LicenseWarningService : ILicenseWarningService
{
  private static readonly PXLicenseState[] _portalWarningStates = new PXLicenseState[4]
  {
    PXLicenseState.Expired,
    PXLicenseState.Invalid,
    PXLicenseState.Suspended,
    PXLicenseState.Rejected
  };
  private string warningMessage = string.Empty;

  public void AddMessage(string message) => this.warningMessage = message;

  public string GetMessage() => PXMessages.LocalizeNoPrefix(this.warningMessage);

  public bool ShowMessage() => !string.IsNullOrEmpty(this.warningMessage);

  public bool BypassWarning(PXLicense license)
  {
    bool flag = false;
    if (license != null)
    {
      if (license.State == PXLicenseState.Bypass)
        flag = true;
      else if (license.State == PXLicenseState.Valid)
      {
        PXSessionContext pxIdentity = PXContext.PXIdentity;
        if (pxIdentity == null || !pxIdentity.Authenticated)
          flag = true;
      }
      if (PortalHelper.IsPortalContext() && !((IEnumerable<PXLicenseState>) LicenseWarningService._portalWarningStates).Contains<PXLicenseState>(license.State))
        flag = true;
    }
    return flag;
  }
}
