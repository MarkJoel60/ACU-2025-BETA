// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiActions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

/// <exclude />
public static class WikiActions
{
  public static void Insert(WikiDescriptor wiki, WikiPage current, string name)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    WikiActions.Insert(wiki, current, name, WikiActions.\u003C\u003EO.\u003C0\u003E__GraphType ?? (WikiActions.\u003C\u003EO.\u003C0\u003E__GraphType = new WikiActions.GetArticleEditGraph(Wiki.GraphType)));
  }

  public static void Insert(
    WikiDescriptor wiki,
    WikiPage current,
    string name,
    WikiActions.GetArticleEditGraph getGraphTypeHandler)
  {
    Guid? nullable1;
    if (current != null)
    {
      Guid? pageId = wiki.PageID;
      nullable1 = current.WikiID;
      if ((pageId.HasValue == nullable1.HasValue ? (pageId.HasValue ? (pageId.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        current = (WikiPage) null;
    }
    Guid? nullable2;
    if (current != null)
    {
      bool? folder = current.Folder;
      bool flag = true;
      nullable2 = folder.GetValueOrDefault() == flag & folder.HasValue ? current.PageID : current.ParentUID;
    }
    else
      nullable2 = wiki.PageID;
    Guid? nullable3 = nullable2;
    System.Type graphType = getGraphTypeHandler(wiki.WikiArticleType);
    if (graphType == (System.Type) null)
      return;
    PXGraph instance = PXGraph.CreateInstance(graphType);
    if (instance != null)
    {
      string[] strArray = new string[7];
      strArray[0] = wiki.UrlEdit;
      strArray[1] = "?wiki=";
      nullable1 = wiki.PageID;
      strArray[2] = nullable1.ToString();
      strArray[3] = "&parent=";
      strArray[4] = nullable3.ToString();
      strArray[5] = "&name=";
      strArray[6] = name;
      throw new PXRedirectRequiredException(string.Concat(strArray), instance, "New Page");
    }
  }

  public static void Edit(WikiDescriptor wiki, WikiPage current)
  {
    if (wiki != null && current != null)
      throw new PXRedirectToUrlException(Wiki.Url(wiki.UrlEdit, new Guid?(current.PageID.Value)), "Edit Page");
  }

  public static bool Delete(WikiPage current)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return WikiActions.Delete(current, WikiActions.\u003C\u003EO.\u003C0\u003E__GraphType ?? (WikiActions.\u003C\u003EO.\u003C0\u003E__GraphType = new WikiActions.GetArticleEditGraph(Wiki.GraphType)));
  }

  public static bool Delete(
    WikiPage current,
    WikiActions.GetArticleEditGraph getGraphTypeHandler)
  {
    if (current == null)
      return false;
    System.Type graphType = getGraphTypeHandler(current.ArticleType);
    if (graphType == (System.Type) null)
      return false;
    PXGraph instance = PXGraph.CreateInstance(graphType);
    if (instance == null)
      return false;
    ((WikiPageFilter) instance.Views["Filter"].Cache.Current).PageID = current.PageID;
    PXView view = instance.Views["Pages"];
    WikiPage wikiPage = (WikiPage) view.SelectSingle();
    int? statusId = wikiPage.StatusID;
    int num1 = 4;
    int num2 = statusId.GetValueOrDefault() == num1 & statusId.HasValue ? 1 : 0;
    view.Cache.Delete((object) wikiPage);
    instance.Actions.PressSave();
    if (num2 != 0)
      WikiActions.View(wikiPage.ParentUID);
    return true;
  }

  internal static void RemoveArticleForever(Guid? pageID)
  {
    PXDatabase.Delete<WikiRevisionTag>(new PXDataFieldRestrict(typeof (WikiFileInPage.pageID).Name, PXDbType.UniqueIdentifier, (object) pageID));
    PXDatabase.Delete<WikiRevision>(new PXDataFieldRestrict(typeof (WikiRevision.pageID).Name, PXDbType.UniqueIdentifier, (object) pageID));
    PXDatabase.Delete<WikiPageLanguage>(new PXDataFieldRestrict(typeof (WikiPageLanguage.pageID).Name, PXDbType.UniqueIdentifier, (object) pageID));
    PXDatabase.Update<UploadFile>((PXDataFieldParam) new PXDataFieldAssign(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, (object) null), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, (object) pageID));
  }

  public static void View(WikiPage current)
  {
    if (current != null)
      throw new PXRedirectToUrlException(Wiki.Url(new Guid?(current.PageID.Value)), "View Page");
  }

  public static void View(Guid? current)
  {
    if (current.HasValue)
      throw new PXRedirectToUrlException(Wiki.Url(new Guid?(current.Value)), "View Page");
  }

  /// <exclude />
  public delegate System.Type GetArticleEditGraph(int? articleType);
}
