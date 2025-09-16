// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.PXSiteMapKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
public static class PXSiteMapKeyGenerator
{
  public const string KEYBASE = "SiteMap ->";

  public static string GetLocalizationKey(Guid? nodeID, string screenID)
  {
    return $"{"SiteMap ->"} {nodeID}{(screenID == null ? (object) string.Empty : (object) $"_{screenID}")}";
  }

  public static string GetLocalizationKey(PX.SM.SiteMap sm)
  {
    return sm != null ? PXSiteMapKeyGenerator.GetLocalizationKey(sm.NodeID, sm.ScreenID) : (string) null;
  }

  public static string GetLocalizationKey(PXSiteMapNode node)
  {
    return node != null ? PXSiteMapKeyGenerator.GetLocalizationKey(new Guid?(node.NodeID), node.ScreenID) : (string) null;
  }
}
