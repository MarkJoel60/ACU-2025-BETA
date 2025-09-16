// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.PXSiteMapLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.LocalizationKeyGenerators;

#nullable disable
namespace PX.Data.Localizers;

/// <exclude />
public class PXSiteMapLocalizer : IPXObjectLocalizer<PXSiteMapNode>, IPXObjectLocalizer
{
  public void Localize(PXSiteMapNode node)
  {
    if (node == null)
      return;
    string localizationKey = PXSiteMapKeyGenerator.GetLocalizationKey(node);
    node.Title = PXLocalizer.Localize(node.Title, localizationKey);
  }
}
