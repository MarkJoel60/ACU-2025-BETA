// Decompiled with JetBrains decompiler
// Type: PX.Data.SidePanelNavigationAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal sealed class SidePanelNavigationAction
{
  public SidePanelNavigationAction(string actionName, string destinationScreenId, string icon)
  {
    this.ActionName = actionName;
    this.DestinationScreenId = destinationScreenId;
    this.Icon = icon;
  }

  public string DestinationScreenId { get; }

  public string ActionName { get; }

  public string Icon { get; }

  public string Title { get; set; }

  public string TitleDetail { get; set; }

  public IReadOnlyDictionary<string, Lazy<object>> ParametersWithValues { get; set; }
}
