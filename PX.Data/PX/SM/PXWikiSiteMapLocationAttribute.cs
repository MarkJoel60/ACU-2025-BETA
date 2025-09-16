// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWikiSiteMapLocationAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXWikiSiteMapLocationAttribute : PXCustomSelectorAttribute
{
  public PXWikiSiteMapLocationAttribute()
    : base(typeof (SiteMap.nodeID))
  {
    this.DescriptionField = typeof (SiteMap.title);
  }

  public IEnumerable GetRecords()
  {
    PXCache cach = this._Graph.Caches[typeof (WikiDescriptor)];
    if (cach != null && cach.Current != null)
    {
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.Provider.FindSiteMapNodeFromKey(((WikiDescriptor) cach.Current).SitemapParent.Value);
      if (siteMapNodeFromKey != null)
        yield return (object) new SiteMap()
        {
          NodeID = new Guid?(siteMapNodeFromKey.NodeID),
          Title = siteMapNodeFromKey.Title
        };
    }
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }
}
