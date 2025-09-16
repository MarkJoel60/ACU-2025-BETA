// Decompiled with JetBrains decompiler
// Type: PX.Translation.ResourceCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Translation;

/// <summary>Is used for collecting resources for translation.</summary>
/// <exclude />
public class ResourceCollection : IEnumerable
{
  private readonly HashSet<LocalizationResourceLite> collectedResourses;

  public static LocalizationResourceType[] PermittedResourceTypes { get; set; }

  public ResourceByScreenCollection ResourcesByScreens { get; private set; }

  public ResourceCollection()
  {
    this.collectedResourses = new HashSet<LocalizationResourceLite>();
    this.ResourcesByScreens = new ResourceByScreenCollection();
  }

  public void AddResource(LocalizationResourceLite newResource)
  {
    if (ResourceCollection.PermittedResourceTypes != null && !((IEnumerable<LocalizationResourceType>) ResourceCollection.PermittedResourceTypes).Contains<LocalizationResourceType>((LocalizationResourceType) newResource.Type))
      return;
    this.collectedResourses.Add(newResource);
  }

  public void AddResourceByScreen(LocalizationResourceLite newResource)
  {
    if (ResourceCollection.PermittedResourceTypes != null && !((IEnumerable<LocalizationResourceType>) ResourceCollection.PermittedResourceTypes).Contains<LocalizationResourceType>((LocalizationResourceType) newResource.Type))
      return;
    if (!this.collectedResourses.Contains(newResource))
      this.collectedResourses.Add(newResource);
    this.ResourcesByScreens.Add(newResource);
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this.collectedResourses.GetEnumerator();
}
