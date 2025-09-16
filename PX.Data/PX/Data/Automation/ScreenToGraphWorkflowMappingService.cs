// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.ScreenToGraphWorkflowMappingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Automation;

internal class ScreenToGraphWorkflowMappingService : IScreenToGraphWorkflowMappingService
{
  private readonly IPXPageIndexingService _pageIndexingService;

  public ScreenToGraphWorkflowMappingService(IPXPageIndexingService pageIndexingService)
  {
    this._pageIndexingService = pageIndexingService;
  }

  public string GetScreenIDFromGraphType(System.Type type)
  {
    IList<string> screensIdFromGraphType = this._pageIndexingService.GetScreensIDFromGraphType(type);
    if (screensIdFromGraphType == null || !screensIdFromGraphType.Any<string>())
      return (string) null;
    if (screensIdFromGraphType.Count == 1)
      return screensIdFromGraphType[0];
    string screenId = PXSiteMap.Provider.FindSiteMapNodeByGraphTypeUnsecure(CustomizedTypeManager.GetTypeNotCustomized(type).FullName)?.ScreenID;
    return !string.IsNullOrEmpty(screenId) && screensIdFromGraphType.Any<string>((Func<string, bool>) (it => it.Equals(screenId, StringComparison.OrdinalIgnoreCase))) ? screenId : screensIdFromGraphType[0];
  }

  public string GetGraphTypeByScreenID(string screenId)
  {
    return this._pageIndexingService.GetGraphTypeByScreenID(screenId);
  }

  public string GetPrimaryViewForScreen(string screenId)
  {
    return this._pageIndexingService.GetPrimaryViewForScreen(screenId);
  }
}
