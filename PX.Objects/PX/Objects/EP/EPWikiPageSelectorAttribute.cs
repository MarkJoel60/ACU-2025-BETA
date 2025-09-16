// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPWikiPageSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Allow show articles of certain wiki.</summary>
/// <example>[EPWikiPageSelector]</example>
public sealed class EPWikiPageSelectorAttribute : PXCustomSelectorAttribute
{
  private readonly Type _wiki;

  public EPWikiPageSelectorAttribute()
    : this((Type) null)
  {
  }

  public EPWikiPageSelectorAttribute(Type wiki)
    : base(typeof (WikiPageSimple.pageID))
  {
    this._wiki = wiki;
    ((PXSelectorAttribute) this)._ViewName = ((PXSelectorAttribute) this).GenerateViewName();
  }

  protected virtual string GenerateViewName()
  {
    return $"{((PXSelectorAttribute) this).GenerateViewName()}_{(this._wiki == (Type) null ? (string) null : this._wiki.Name)}";
  }

  public IEnumerable GetRecords([PXDBGuid(false)] Guid? wikiId)
  {
    EPWikiPageSelectorAttribute selectorAttribute = this;
    if (wikiId.HasValue || selectorAttribute._wiki != (Type) null && BqlCommand.GetItemType(selectorAttribute._wiki) != (Type) null)
    {
      PXCache cach = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute._wiki)];
      Guid? nullable = wikiId;
      if (!nullable.HasValue && selectorAttribute._wiki != (Type) null)
      {
        object obj = cach.GetValue(cach.Current, selectorAttribute._wiki.Name);
        if (obj != null)
          nullable = GUID.CreateGuid(obj.ToString());
      }
      if (nullable.HasValue)
      {
        PXGraph graph = selectorAttribute._Graph;
        object[] objArray = new object[1]
        {
          (object) nullable
        };
        foreach (PXResult<WikiPageSimple> pxResult in PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.wikiID, Equal<Required<WikiPageSimple.wikiID>>>>.Config>.Select(graph, objArray))
        {
          WikiPageSimple record = PXResult<WikiPageSimple>.op_Implicit(pxResult);
          if (PXSiteMap.WikiProvider.GetAccessRights(((WikiPage) record).PageID.Value) >= 1)
          {
            PXSiteMapNode siteMapNodeFromKey = ((PXSiteMapProvider) PXSiteMap.WikiProvider).FindSiteMapNodeFromKey(((WikiPage) record).PageID.Value);
            ((WikiPage) record).Title = siteMapNodeFromKey == null || string.IsNullOrEmpty(siteMapNodeFromKey.Title) ? ((WikiPage) record).Name : siteMapNodeFromKey.Title;
            yield return (object) record;
          }
        }
      }
    }
  }
}
