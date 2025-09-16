// Decompiled with JetBrains decompiler
// Type: PX.SM.PXWikiArticleSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXWikiArticleSelectorAttribute(System.Type keyType) : PXCustomSelectorAttribute(keyType)
{
  protected System.Type _Wiki;

  public System.Type Wiki
  {
    get => this._Wiki;
    set => this._Wiki = value;
  }

  public PXWikiArticleSelectorAttribute()
    : this(typeof (WikiPageSimple.pageID))
  {
  }

  public IEnumerable GetRecords()
  {
    PXWikiArticleSelectorAttribute selectorAttribute = this;
    if (selectorAttribute.Wiki != (System.Type) null && BqlCommand.GetItemType(selectorAttribute.Wiki) != (System.Type) null)
    {
      PXCache cach = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute.Wiki)];
      object obj = cach.GetValue(cach.Current, selectorAttribute.Wiki.Name);
      Guid? nullable = obj == null ? new Guid?() : GUID.CreateGuid(obj.ToString());
      if (nullable.HasValue)
      {
        PXGraph graph = selectorAttribute._Graph;
        object[] objArray = new object[1]
        {
          (object) nullable
        };
        foreach (PXResult<WikiPageSimple> pxResult in PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.wikiID, Equal<Required<WikiPageSimple.wikiID>>>>.Config>.Select(graph, objArray))
        {
          WikiPageSimple record = (WikiPageSimple) pxResult;
          if (PXSiteMap.WikiProvider.GetAccessRights(record.PageID.Value) >= PXWikiRights.Select)
            yield return (object) record;
        }
      }
    }
  }
}
