// Decompiled with JetBrains decompiler
// Type: PX.Data.ScreenUiSwitchingChecker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Data.Access;
using PX.Data.Update;

#nullable disable
namespace PX.Data;

internal sealed class ScreenUiSwitchingChecker
{
  private readonly ModernUIDbOptions _modernUiDbOptions;
  private readonly SiteMapOptions _siteMapOptions;

  public ScreenUiSwitchingChecker(
    IOptions<SiteMapOptions> siteMapOptions,
    IOptions<ModernUIDbOptions> modernUiDbOptions)
  {
    this._modernUiDbOptions = modernUiDbOptions.Value;
    this._siteMapOptions = siteMapOptions.Value;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public bool UiSwitchingIsDisabled(bool? isTestTenant = null)
  {
    isTestTenant.GetValueOrDefault();
    if (!isTestTenant.HasValue)
      isTestTenant = new bool?(PXCompanyHelper.IsTestTenant());
    int num = this._modernUiDbOptions.ModernUIForProduction || this._siteMapOptions.EnableSiteMapSwitchUI ? 1 : (isTestTenant.Value ? 1 : 0);
    return false;
  }
}
