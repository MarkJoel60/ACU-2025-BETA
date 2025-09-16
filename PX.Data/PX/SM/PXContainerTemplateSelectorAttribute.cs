// Decompiled with JetBrains decompiler
// Type: PX.SM.PXContainerTemplateSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXContainerTemplateSelectorAttribute : PXCustomSelectorAttribute
{
  public PXContainerTemplateSelectorAttribute(System.Type wikiIDSearch)
    : base(PXContainerTemplateSelectorAttribute.GenerateSearchCommand(wikiIDSearch), typeof (SimpleWikiPage.name))
  {
    this.DescriptionField = typeof (SimpleWikiPage.title);
  }

  private static System.Type GenerateSearchCommand(System.Type wikiIDSearch)
  {
    return typeof (IBqlOperand).IsAssignableFrom(wikiIDSearch) ? BqlCommand.Compose(typeof (Search<,>), typeof (SimpleWikiPage.pageID), typeof (Where<,,>), typeof (SimpleWikiPage.wikiID), typeof (Equal<>), wikiIDSearch, typeof (And<SimpleWikiPage.name, Like<ContainerTemplateLeftLike>>)) : throw new ArgumentException($"'{wikiIDSearch}' must implement IBqlOperand interface");
  }

  protected virtual IEnumerable GetRecords()
  {
    PXContainerTemplateSelectorAttribute selectorAttribute = this;
    foreach (SimpleWikiPage record in new PXView(selectorAttribute._Graph, !selectorAttribute._DirtyRead, selectorAttribute._Select).SelectMulti())
    {
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(record.PageID.Value);
      record.Title = siteMapNodeFromKey == null ? record.Name : siteMapNodeFromKey.Title;
      yield return (object) record;
    }
  }
}
