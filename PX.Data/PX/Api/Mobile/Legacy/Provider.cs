// Decompiled with JetBrains decompiler
// Type: PX.Api.Mobile.Legacy.Provider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Api.Mobile.Legacy;

[PXInternalUseOnly]
public static class Provider
{
  private static readonly IMobileSiteMapCache _mobileSiteMapCache = ServiceLocator.Current.GetInstance<IMobileSiteMapCache>();
  private static readonly IMobileScreenMapService _mobileScreenMapService = ServiceLocator.Current.GetInstance<IMobileScreenMapService>();
  private static readonly IMobileWorkspacesCache _mobileWorkspacesCache = ServiceLocator.Current.GetInstance<IMobileWorkspacesCache>();

  public static Screen GetScreenMap(string screenId, bool throwException = true)
  {
    Provider.SelectTimeStampForContext();
    return Provider._mobileScreenMapService.GetScreenMap(screenId, throwException);
  }

  [PXInternalUseOnly]
  public static (IEnumerable<InqField> Parameters, IEnumerable<InqField> Fields) GetGIParameters(
    string screenId)
  {
    GIDataScreen giDataScreen = new GIDataScreen(screenId);
    return (giDataScreen.GetParameters(), giDataScreen.GetFields());
  }

  [PXInternalUseOnly]
  public static void InitCaches()
  {
    foreach (IMobileCache allInstance in ServiceLocator.Current.GetAllInstances<IMobileCache>())
      allInstance.Init();
  }

  [PXInternalUseOnly]
  public static void PrepareSiteMap()
  {
    Provider.SelectTimeStampForContext();
    Provider._mobileSiteMapCache.SiteMap();
    Provider._mobileWorkspacesCache.Workspaces();
  }

  private static void SelectTimeStampForContext()
  {
    bool? slot = PXContext.GetSlot<bool?>("PX.Api.Mobile.Legacy.Provider");
    bool flag = true;
    if (slot.GetValueOrDefault() == flag & slot.HasValue)
      return;
    PXDatabase.SelectTimeStamp();
    PXContext.SetSlot<bool>("PX.Api.Mobile.Legacy.Provider", true);
  }
}
