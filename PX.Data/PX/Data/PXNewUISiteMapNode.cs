// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNewUISiteMapNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

internal class PXNewUISiteMapNode(
  PXSiteMapProvider provider,
  Guid nodeID,
  string url,
  string title,
  PXRoleList roles,
  string graphType,
  string screeenID) : PXSiteMapNode(provider, nodeID, url, title, roles, graphType, screeenID)
{
  internal const string ModernUIScreensRootFolder = "/Scripts/Screens";

  internal string TypeScriptUrl => $"~/Scripts/Screens/{this.ScreenID}.html";

  /// <summary>
  /// Represents URL in original case (for case-sensitive usages).
  /// </summary>
  internal override string OriginalUrl => this.TypeScriptUrl;

  public override string Url
  {
    get => this.TypeScriptUrl;
    set => throw new InvalidOperationException("Url is readonly.");
  }

  internal override bool IsExternalUrl => false;

  internal override string SelectedUI => "T";

  internal new delegate PXNewUISiteMapNode Factory(
    PXSiteMapProvider provider,
    Guid nodeID,
    string url,
    string title,
    PXRoleList roles,
    string graphType,
    string screeenID);
}
