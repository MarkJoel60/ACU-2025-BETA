// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.SPWikiProductMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Search;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.SM;

[Serializable]
public class SPWikiProductMaint : PXGraph<SPWikiProductMaint>
{
  public PXSelect<SPWikiProduct> WikiProduct;
  public PXSelect<SPWikiProductTags, Where<SPWikiProductTags.productID, Equal<Current<SPWikiProduct.productID>>>> WikiProductDetails;
  public SPWikiCategoryMaint.PXSelectWikiFoldersTree Folders;
  public PXSave<SPWikiProduct> Save;
  public PXCancel<SPWikiProduct> Cancel;
  public PXInsert<SPWikiProduct> Insert;
  public PXDelete<SPWikiProduct> Delete;
  public PXFirst<SPWikiProduct> First;
  public PXPrevious<SPWikiProduct> Previous;
  public PXNext<SPWikiProduct> Next;
  public PXLast<SPWikiProduct> Last;

  public SPWikiProductMaint() => Wiki.BlockIfOnlineHelpIsOn();

  protected virtual void SPWikiProductTags_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is SPWikiProductTags row))
      return;
    row.ProductID = ((PXSelectBase<SPWikiProduct>) this.WikiProduct).Current.ProductID;
    if (row == null || row.PageName == null)
      return;
    PXResult<WikiPage, WikiPageLanguage> pxResult = (PXResult<WikiPage, WikiPageLanguage>) PXResultset<WikiPage>.op_Implicit(PXSelectBase<WikiPage, PXSelectJoin<WikiPage, LeftJoin<WikiPageLanguage, On<WikiPage.pageID, Equal<WikiPageLanguage.pageID>>>, Where<WikiPage.name, Equal<Required<WikiPage.name>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.PageName
    }));
    if (pxResult == null)
      return;
    WikiPage wikiPage = ((PXResult) pxResult)[typeof (WikiPage)] as WikiPage;
    WikiPageLanguage wikiPageLanguage = ((PXResult) pxResult)[typeof (WikiPageLanguage)] as WikiPageLanguage;
    if (wikiPage != null)
    {
      row.PageName = wikiPage.Name;
      row.PageID = wikiPage.PageID;
    }
    if (wikiPageLanguage != null)
      row.PageTitle = wikiPageLanguage.Title;
    else
      row.PageTitle = row.PageName;
  }

  protected virtual void SPWikiProductTags_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is SPWikiProductTags row) || row.PageName == null)
      return;
    PXResult<WikiPage, WikiPageLanguage> pxResult = (PXResult<WikiPage, WikiPageLanguage>) PXResultset<WikiPage>.op_Implicit(PXSelectBase<WikiPage, PXSelectJoin<WikiPage, LeftJoin<WikiPageLanguage, On<WikiPage.pageID, Equal<WikiPageLanguage.pageID>>>, Where<WikiPage.name, Equal<Required<WikiPage.name>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.PageName
    }));
    if (pxResult == null)
      return;
    WikiPage wikiPage = ((PXResult) pxResult)[typeof (WikiPage)] as WikiPage;
    WikiPageLanguage wikiPageLanguage = ((PXResult) pxResult)[typeof (WikiPageLanguage)] as WikiPageLanguage;
    if (wikiPage != null)
    {
      row.PageName = wikiPage.Name;
      row.PageID = wikiPage.PageID;
    }
    if (wikiPageLanguage != null)
      row.PageTitle = wikiPageLanguage.Title;
    else
      row.PageTitle = row.PageName;
  }
}
