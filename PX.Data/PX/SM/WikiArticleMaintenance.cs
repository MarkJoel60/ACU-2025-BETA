// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiArticleMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden(ServiceVisible = true)]
public class WikiArticleMaintenance : 
  WikiPageMaint<WikiArticle, WikiArticle, Where<WikiArticle.articleType, Equal<WikiArticleType.article>>>
{
  public WikiArticleMaintenance()
  {
    PXUIFieldAttribute.SetDisplayName<WikiPage.name>(this.Pages.Cache, "Article ID");
  }
}
