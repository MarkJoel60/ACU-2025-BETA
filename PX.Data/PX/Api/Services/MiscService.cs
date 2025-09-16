// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.MiscService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Mobile;
using PX.Api.Mobile.Legacy;
using PX.Common;
using System;

#nullable disable
namespace PX.Api.Services;

public class MiscService : IMiscService
{
  private readonly IMobileSiteMapCache _mobileSiteMapCache;

  public MiscService(IMobileSiteMapCache mobileSiteMapCache)
  {
    this._mobileSiteMapCache = mobileSiteMapCache;
  }

  public void SetBusinessDate(DateTime businessDate)
  {
    PXContext.SetBusinessDate(new DateTime?(businessDate));
  }

  public void SetLocaleName(string localeName)
  {
    PXContext.Session.SetString("LocaleName", localeName);
  }

  public string GetSiteMapCacheHash() => this._mobileSiteMapCache.Hash();

  public SiteMap GetSiteMap() => this._mobileSiteMapCache.SiteMap();
}
