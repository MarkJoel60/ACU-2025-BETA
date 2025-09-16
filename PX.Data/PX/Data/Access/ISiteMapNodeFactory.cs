// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ISiteMapNodeFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Access;

internal interface ISiteMapNodeFactory
{
  PXSiteMapNode CreateNode(
    string selectedUi,
    PXSiteMapProvider provider,
    Guid nodeId,
    string url,
    string title,
    PXRoleList roles,
    string graphType,
    string screenId,
    bool isTestTenant);
}
