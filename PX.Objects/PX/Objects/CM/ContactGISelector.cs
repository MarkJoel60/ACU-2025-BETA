// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.ContactGISelector
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Maintenance.GI;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CM;

public sealed class ContactGISelector : PXCustomSelectorAttribute
{
  public ContactGISelector()
    : base(typeof (Search3<GIDesign.designID, OrderBy<Asc<GIDesign.name>>>), new System.Type[3]
    {
      typeof (GIDesign.name),
      typeof (GIDesign.sitemapTitle),
      typeof (GIDesign.sitemapScreenID)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (GIDesign.name);
  }

  public IEnumerable GetRecords()
  {
    GenericInquiryDesigner graph = PXGraph.CreateInstance<GenericInquiryDesigner>();
    return (IEnumerable) ((IEnumerable<GIDescription>) PXGenericInqGrph.Def).Where<GIDescription>((Func<GIDescription, bool>) (d => d.Tables.Any<GITable>((Func<GITable, bool>) (t =>
    {
      if (!EnumerableExtensions.IsIn<string>(t.Name, typeof (Contact).FullName, typeof (CRLead).FullName))
        return false;
      return d.Relations.Any<GIRelation>((Func<GIRelation, bool>) (x => x.ParentTable == t.Alias || x.ChildTable == t.Alias || x.ParentTable == null)) || !d.Relations.Any<GIRelation>();
    })))).Select<GIDescription, GIDesign>((Func<GIDescription, GIDesign>) (d =>
    {
      GIDesign copy = PXCache<GIDesign>.CreateCopy(d.Design);
      PXSiteMapNode pxSiteMapNode = ((PXScreenToSiteMapAddHelperBase<GIDesign>) graph.ScreenToSiteMapAddHelper).FindNodes(copy).FirstOrDefault<PXSiteMapNode>();
      if (pxSiteMapNode != null)
      {
        copy.SitemapScreenID = pxSiteMapNode.ScreenID;
        copy.SitemapTitle = pxSiteMapNode.Title;
      }
      return copy;
    }));
  }

  public string ViewName => ((PXSelectorAttribute) this)._ViewName;
}
