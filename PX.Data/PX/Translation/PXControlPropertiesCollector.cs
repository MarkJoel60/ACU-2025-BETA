// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXControlPropertiesCollector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;

#nullable disable
namespace PX.Translation;

/// <exclude />
public class PXControlPropertiesCollector
{
  public static Guid COLLECTION_PROVIDER_VALUE = Guid.NewGuid();
  public ResourceCollection result;

  public void CollectRes(string value, string resInfo, LocalizationResourceType resType)
  {
    if (value == null || !(value.Trim() != string.Empty))
      return;
    this.result.AddResource(new LocalizationResourceLite(resInfo, resType, value));
  }

  public void Collect(LocalizationResourceLite resource, CollectResourceSettings resourceSettings)
  {
    if (resource == null)
      return;
    if ((resourceSettings & CollectResourceSettings.Resource) == CollectResourceSettings.Resource)
      this.result.AddResource(resource);
    if ((resourceSettings & CollectResourceSettings.ResourceByScreen) != CollectResourceSettings.ResourceByScreen)
      return;
    this.result.AddResourceByScreen(resource);
  }
}
