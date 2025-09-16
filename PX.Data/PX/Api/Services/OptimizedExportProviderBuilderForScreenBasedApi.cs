// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.OptimizedExportProviderBuilderForScreenBasedApi
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Data;
using PX.Data.Api.Export;
using PX.Data.Description;
using System.Collections.Generic;

#nullable disable
namespace PX.Api.Services;

internal delegate IOptimizedExportProvider OptimizedExportProviderBuilderForScreenBasedApi(
  PXSiteMap.ScreenInfo screenInfo,
  HashSet<string> views,
  PXViewDescription[] containers,
  Command[] commands,
  string screenId,
  Dictionary<string, PXFilterRow[]> filters,
  string locale = null,
  bool mobile = false,
  bool showArchive = false);
