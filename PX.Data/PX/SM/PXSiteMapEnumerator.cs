// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSiteMapEnumerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

/// <summary>
/// Default site map enumerator that enumerates all nodes and applies defined filter.
/// </summary>
/// <exclude />
public class PXSiteMapEnumerator(bool isSecureAccess, PXSiteMapNodeFilter siteMapNodeFilter) : 
  PXSiteMapEnumeratorBase(isSecureAccess, siteMapNodeFilter)
{
  public override IEnumerable<SiteMap> SiteMapNodes(Guid? nodeID)
  {
    bool needRoot = false;
    if (!nodeID.HasValue)
    {
      nodeID = new Guid?(Guid.Empty);
      needRoot = true;
    }
    return this.EnumNodes(nodeID.Value, needRoot);
  }

  private IEnumerable<SiteMap> EnumNodes(Guid nodeID, bool needRoot)
  {
    PXSiteMapEnumerator siteMapEnumerator = this;
    PXSiteMapNode nodeById = siteMapEnumerator.GetNodeById(nodeID);
    if (nodeById != null)
    {
      if (needRoot)
      {
        yield return PXSiteMapEnumeratorBase.CreateSiteMap(nodeById);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        foreach (SiteMap siteMap in siteMapEnumerator.NodeFilter.Filter(siteMapEnumerator.GetChildNodes(nodeById).Cast<PXSiteMapNode>()).Select<PXSiteMapNode, SiteMap>(PXSiteMapEnumerator.\u003C\u003EO.\u003C0\u003E__CreateSiteMap ?? (PXSiteMapEnumerator.\u003C\u003EO.\u003C0\u003E__CreateSiteMap = new Func<PXSiteMapNode, SiteMap>(PXSiteMapEnumeratorBase.CreateSiteMap))))
          yield return siteMap;
      }
    }
  }
}
