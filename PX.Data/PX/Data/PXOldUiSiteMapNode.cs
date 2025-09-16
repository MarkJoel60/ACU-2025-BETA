// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOldUiSiteMapNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[method: PXInternalUseOnly]
public class PXOldUiSiteMapNode(
  PXSiteMapProvider provider,
  Guid nodeID,
  string url,
  string title,
  PXRoleList roles,
  string graphType,
  string screeenID) : PXSiteMapNode(provider, nodeID, url, title, roles, graphType, screeenID)
{
  internal override string SelectedUI => "E";

  internal new delegate PXOldUiSiteMapNode Factory(
    PXSiteMapProvider provider,
    Guid nodeID,
    string url,
    string title,
    PXRoleList roles,
    string graphType,
    string screeenID);
}
