// Decompiled with JetBrains decompiler
// Type: PX.SP.PortalService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using PX.Common;
using PX.Common.Context;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

#nullable enable
namespace PX.SP;

/// <summary>Provides Portals info.</summary>
/// <param name="portalDescriptor">Portal descriptor implementation.</param>
/// <summary>Provides Portals info.</summary>
/// <param name="portalDescriptor">Portal descriptor implementation.</param>
internal class PortalService(IPortalDescriptor portalDescriptor) : 
  IPortalService,
  IPortalInitializationService
{
  public bool IsPortalContext() => PXPortalContext.GetPortal() != null;

  public void InitPortal(HttpContext httpContext)
  {
    PortalInfo portalInfo = this.IdentifyPortal(httpContext.Request);
    if (portalInfo == null)
      return;
    HttpContextExtensions.Slots(httpContext).SetPortal(portalInfo);
  }

  private PortalInfo? IdentifyPortal(HttpRequest httpRequest)
  {
    List<PortalInfo> list = portalDescriptor.GetAllPortals().Where<PortalInfo>((Func<PortalInfo, bool>) (_ => _.IsActive)).ToList<PortalInfo>();
    if (list.Count == 0)
      return (PortalInfo) null;
    string forcedPortalID = WebConfig.PortalSiteID;
    if (!string.IsNullOrEmpty(forcedPortalID))
    {
      PortalInfo portalInfo = list.FirstOrDefault<PortalInfo>((Func<PortalInfo, bool>) (p => p.Name == forcedPortalID));
      if (portalInfo != null)
      {
        if (!UrlMatcher.MatchUrl(httpRequest.Host, httpRequest.PathBase, portalInfo.Url))
          throw new Exception("Invalid Portal URL");
        return portalInfo;
      }
    }
    PortalInfo[] array = list.Where<PortalInfo>((Func<PortalInfo, bool>) (p => UrlMatcher.MatchUrl(httpRequest.Host, httpRequest.PathBase, p.Url))).ToArray<PortalInfo>();
    if (array.Length > 1)
      PXTrace.WriteError("An attempt was made to add a duplicate entry.");
    return ((IEnumerable<PortalInfo>) array).FirstOrDefault<PortalInfo>();
  }

  public int? GetPortalID() => PXPortalContext.GetPortal()?.ID;

  public PortalInfo? GetPortal() => PXPortalContext.GetPortal();

  public List<PortalInfo> GetPortals() => portalDescriptor.GetPortals();

  public bool IsPortalFeatureInstalled()
  {
    return PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+modernPortalModule");
  }

  public bool IsPortalAccessAllowed(string userName)
  {
    if (!this.IsPortalContext())
      return false;
    bool flag = false;
    string accessRole = this.GetPortal()?.AccessRole;
    string[] rolesForUser = System.Web.Security.Roles.GetRolesForUser(userName);
    if (!string.IsNullOrEmpty(accessRole))
    {
      List<string> second = new List<string>(2)
      {
        accessRole,
        PXAccess.GetPortalAdministratorRole()
      };
      if (((IEnumerable<string>) rolesForUser).Intersect<string>((IEnumerable<string>) second).Any<string>())
        flag = true;
    }
    else
      flag = true;
    return flag;
  }

  public bool ValidateUser(MembershipUser user)
  {
    bool flag = false;
    int num = user.IsGuest() ? 1 : 0;
    PortalInfo portal = this.GetPortal();
    if (num == 0 && portal == null)
      flag = true;
    if (portal != null && portal.IsActive && ((IEnumerable<string>) System.Web.Security.Roles.GetRolesForUser(user.UserName)).Contains<string>(PXAccess.GetPortalAdministratorRole()))
      flag = true;
    if (portal != null)
      flag = this.IsPortalAccessAllowed(user.UserName);
    if (flag)
      return flag;
    PXAuditJournal.Register(PXAuditJournal.Operation.LoginFailed, user.UserName, (string) null, "The user type does not match the application.");
    return flag;
  }
}
