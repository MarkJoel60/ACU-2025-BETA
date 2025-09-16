// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Services.INavigationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation.Services;

internal interface INavigationService
{
  void NavigateTo(
    string destinationScreenIdOrUrl,
    IReadOnlyDictionary<string, object> parameters,
    PXBaseRedirectException.WindowMode windowMode);

  void NavigateTo(
    string destinationScreenIdOrUrl,
    IReadOnlyDictionary<string, object> parameters,
    PXBaseRedirectException.WindowMode windowMode,
    string navigationMessage);

  object GetPrimaryRow(PXView primaryView, IReadOnlyDictionary<string, object> parameters);
}
