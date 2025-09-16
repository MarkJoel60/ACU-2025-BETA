// Decompiled with JetBrains decompiler
// Type: PX.SP.PortalHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Security;

#nullable disable
namespace PX.SP;

[PXInternalUseOnly]
public static class PortalHelper
{
  private static Lazy<IPortalDescriptor> _portalDescriptor = new Lazy<IPortalDescriptor>((Func<IPortalDescriptor>) (() => ServiceLocator.Current.GetInstance<IPortalDescriptor>()), LazyThreadSafetyMode.ExecutionAndPublication);
  private static Lazy<IPortalService> _portalService = new Lazy<IPortalService>((Func<IPortalService>) (() => ServiceLocator.Current.GetInstance<IPortalService>()), LazyThreadSafetyMode.ExecutionAndPublication);

  private static IPortalDescriptor PortalDescriptor
  {
    get
    {
      if (!ServiceLocator.IsLocationProviderSet)
        return (IPortalDescriptor) null;
      return PortalHelper._portalDescriptor?.Value;
    }
  }

  private static IPortalService PortalService
  {
    get
    {
      if (!ServiceLocator.IsLocationProviderSet)
        return (IPortalService) null;
      return PortalHelper._portalService?.Value;
    }
  }

  public static int? GetPortalID() => PXPortalContext.GetPortal()?.ID;

  public static List<PortalInfo> GetPortals() => PortalHelper.PortalDescriptor?.GetPortals();

  public static PortalInfo GetPortal() => PortalHelper.PortalService?.GetPortal();

  public static PortalInfo GetPortal(int? portalID)
  {
    return PortalHelper.PortalDescriptor?.GetPortal(portalID);
  }

  public static PortalInfo GetPortal(string portalName)
  {
    return PortalHelper.PortalDescriptor?.GetPortal(portalName);
  }

  public static string GetPortalTheme()
  {
    if (!PortalHelper.IsPortalContext())
      return string.Empty;
    return PortalHelper.GetPortal()?.Theme;
  }

  public static bool IsPortalContext(PortalContexts context = PortalContexts.Any)
  {
    if ((context & PortalContexts.Legacy) > (PortalContexts) 0 && PXSiteMap.IsPortal)
      return true;
    if ((context & PortalContexts.Modern) <= (PortalContexts) 0)
      return false;
    IPortalService portalService = PortalHelper.PortalService;
    return portalService != null && portalService.IsPortalContext();
  }

  public static bool ValidateAccessRights(MembershipUser user)
  {
    IPortalService portalService = PortalHelper.PortalService;
    return portalService != null && portalService.ValidateUser(user);
  }
}
