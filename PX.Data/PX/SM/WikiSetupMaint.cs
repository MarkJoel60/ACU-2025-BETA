// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiSetupMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Wiki.DITA;
using PX.Web.UI;
using System;
using System.Collections;
using System.Web;

#nullable enable
namespace PX.SM;

/// <exclude />
public class WikiSetupMaint : WikiMaintenance
{
  private const 
  #nullable disable
  string _SLOT_KEY_PREFIX = "WikiSetup_";
  [PXHidden]
  public PXSelect<KBResponseMark> KBResponseMarks;
  [PXHidden]
  public PXSelect<WikiPageLanguage> KBWikiPageTree;
  public PXAction<WikiDescriptor> DITA;
  public PXAction<WikiDescriptor> ExportToDITA;
  public PXAction<WikiDescriptor> ExportToDITAPublished;
  public PXAction<WikiDescriptor> ImportToDITA;

  public WikiSetupMaint()
  {
    PXUIFieldAttribute.SetDisplayName(this.Caches[typeof (SimpleWikiPage)], "Name", "ID");
    PXUIFieldAttribute.SetDisplayName(this.Caches[typeof (WikiPageLanguage)], "Title", "Title");
    this.DITA.SetVisible(false);
    this.DITA.AddMenuAction((PXAction) this.ImportToDITA);
    this.DITA.AddMenuAction((PXAction) this.ExportToDITA);
    this.DITA.AddMenuAction((PXAction) this.ExportToDITAPublished);
  }

  public virtual IEnumerable kbWikiPageTree([PXGuid] Guid? pageID)
  {
    return (IEnumerable) PXSelectBase<WikiPageLanguage, PXSelectJoin<WikiPageLanguage, InnerJoin<SimpleWikiPage, On<SimpleWikiPage.pageID, Equal<WikiPageLanguage.pageID>>>, Where<SimpleWikiPage.wikiID, Equal<Current<WikiDescriptor.pageID>>, And<SimpleWikiPage.name, NotLike<TemplateLeftLike>, And<SimpleWikiPage.name, NotLike<GenTemplateLeftLike>, And<SimpleWikiPage.name, NotLike<ContainerTemplateLeftLike>, PX.Data.And<Where<Required<SimpleWikiPage.parentUID>, PX.Data.IsNull, And<SimpleWikiPage.parentUID, Equal<Current<WikiDescriptor.pageID>>, Or<SimpleWikiPage.parentUID, Equal<Required<SimpleWikiPage.parentUID>>>>>>>>>>>.Config>.Select((PXGraph) this, (object) pageID, (object) pageID);
  }

  protected override string GetArticleUrlEdit(int wikiArticleType)
  {
    return wikiArticleType != WikiArticleTypeAttribute._KB_ARTICLE_TYPE ? base.GetArticleUrlEdit(wikiArticleType) : "~/Wiki/KBEdit.aspx";
  }

  protected override string GetSiteMapIcon(int wikiArticleType)
  {
    return wikiArticleType != WikiArticleTypeAttribute._KB_ARTICLE_TYPE ? base.GetSiteMapIcon(wikiArticleType) : Sprite.Main.GetFullUrl("Info");
  }

  protected override string GetSearchUrl(int? wikiArticleType, Guid? wikiId)
  {
    int? nullable = wikiArticleType;
    int kbArticleType = WikiArticleTypeAttribute._KB_ARTICLE_TYPE;
    return !(nullable.GetValueOrDefault() == kbArticleType & nullable.HasValue) ? base.GetSearchUrl(wikiArticleType, wikiId) : "~/Search/KB.aspx?WikiID=" + wikiId.ToString();
  }

  protected override System.Type GetGraphType(int? wikiArticleType)
  {
    int? nullable = wikiArticleType;
    int kbArticleType = WikiArticleTypeAttribute._KB_ARTICLE_TYPE;
    return !(nullable.GetValueOrDefault() == kbArticleType & nullable.HasValue) ? base.GetGraphType(wikiArticleType) : typeof (KBArticleMaint);
  }

  protected void WikiDescriptor_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    WikiDescriptor row = e.Row as WikiDescriptor;
    this.ImportToDITA.SetEnabled(false);
    this.ExportToDITA.SetEnabled(false);
    this.ExportToDITAPublished.SetEnabled(false);
    if (row == null || row.Name == null)
      return;
    this.ExportToDITA.SetEnabled(true);
    this.ExportToDITAPublished.SetEnabled(true);
  }

  [PXButton(Tooltip = "DITAOperation")]
  [PXUIField(DisplayName = "DITA")]
  protected virtual IEnumerable dITA(PXAdapter adapter) => adapter.Get();

  [PXButton(Tooltip = "Export wiki help to the DITA format.")]
  [PXUIField(DisplayName = "Export")]
  protected virtual IEnumerable exportToDITA(PXAdapter adapter)
  {
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate(this.exportToDITAlong));
    return adapter.Get();
  }

  protected virtual void exportToDITAlong()
  {
    if (this.Wikis.Current != null)
      throw new PXRedirectToUrlException($"~/ExportDita.axd?DITAConversionType=ConversionType1&pageid={this.Wikis.Current.PageID.ToString()}&lang={this.Wikis.Current.Language}&name={HttpUtility.HtmlEncode(this.Wikis.Current.Title)}&type=last", "");
  }

  [PXButton(Tooltip = "Export published wiki help to the DITA format.")]
  [PXUIField(DisplayName = "Export (published)")]
  protected virtual IEnumerable exportToDITAPublished(PXAdapter adapter)
  {
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate(this.exportToDITAPublishedlong));
    return adapter.Get();
  }

  protected virtual void exportToDITAPublishedlong()
  {
    if (this.Wikis.Current != null)
      throw new PXRedirectToUrlException($"~/ExportDita.axd?DITAConversionType=ConversionType1&pageid={this.Wikis.Current.PageID.ToString()}&lang={this.Wikis.Current.Language}&name={HttpUtility.HtmlEncode(this.Wikis.Current.Title)}&type=published", "");
  }

  [PXButton(Tooltip = "Import DITA format to wiki help.")]
  [PXUIField(DisplayName = "Import")]
  protected virtual IEnumerable importToDITA(PXAdapter adapter)
  {
    if (adapter != null && adapter.Parameters != null && this.Wikis.Current != null)
      new Package().Read((byte[]) adapter.Parameters[0], new Guid("F8B90F18-CC61-40F0-BBBD-93EF20AF3CC2"));
    return adapter.Get();
  }

  private WikiPage GetWikiPage(Guid? pageID)
  {
    return (WikiPage) (SimpleWikiPage) PXSelectBase<SimpleWikiPage, PXSelect<SimpleWikiPage, Where<SimpleWikiPage.pageID, Equal<Required<SimpleWikiPage.pageID>>>>.Config>.Select((PXGraph) this, (object) pageID) ?? (WikiPage) new SimpleWikiPage();
  }

  /// <exclude />
  [PXHidden]
  [Serializable]
  public class TemplateWikiPage : SimpleWikiPage
  {
    /// <exclude />
    public new abstract class pageID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      WikiSetupMaint.TemplateWikiPage.pageID>
    {
    }
  }
}
