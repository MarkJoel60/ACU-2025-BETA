// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectEMailTemplate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXSelectEMailTemplate : PXSelectBase<WikiTemplate>
{
  public System.Type SourceType;
  public System.Type DefaultWikiID;

  public PXSelectEMailTemplate(PXGraph graph)
  {
    this.View = new PXView(graph, false, (BqlCommand) new PX.Data.Select<WikiTemplate>(), (Delegate) new PXSelectDelegate(this.templateMail));
    this.InitHanlders(graph);
  }

  public PXSelectEMailTemplate(PXGraph graph, Delegate handler)
  {
    this.View = new PXView(graph, false, (BqlCommand) new PX.Data.Select<WikiTemplate>(), handler);
    this.InitHanlders(graph);
  }

  private void InitHanlders(PXGraph graph)
  {
    graph.RowSelected.AddHandler<WikiTemplate>(new PXRowSelected(this.TemplateSelected));
    graph.RowPersisting.AddHandler<WikiTemplate>(new PXRowPersisting(this.TemplatePersiting));
  }

  private IEnumerable templateMail()
  {
    PXSelectEMailTemplate selectEmailTemplate = this;
    if (selectEmailTemplate.Current != null)
    {
      yield return (object) selectEmailTemplate.Current;
    }
    else
    {
      WikiTemplate wikiTemplate = new WikiTemplate();
      yield return selectEmailTemplate.Cache.Insert((object) new WikiTemplate()) ?? selectEmailTemplate.Cache.Locate((object) wikiTemplate);
      selectEmailTemplate.Cache.IsDirty = false;
    }
  }

  private void TemplateSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    WikiTemplate row = (WikiTemplate) e.Row;
    row.SourceType = (string) this.GetValue(this.SourceType);
    Guid? nullable1;
    if (this.DefaultWikiID != (System.Type) null)
    {
      nullable1 = row.Wiki;
      if (!nullable1.HasValue)
        row.Wiki = (Guid?) this.GetValue(this.DefaultWikiID);
    }
    if (PXSelectorAttribute.Select<WikiTemplate.wiki>(cache, (object) row) is WikiDescriptor wikiDescriptor)
    {
      int? wikiArticleType = wikiDescriptor.WikiArticleType;
      int num = 12;
      if (wikiArticleType.GetValueOrDefault() == num & wikiArticleType.HasValue)
      {
        PXUIFieldAttribute.SetVisible<WikiTemplate.notificationID>(cache, (object) null, true);
        PXUIFieldAttribute.SetVisible<WikiTemplate.articleID>(cache, (object) null, false);
        WikiTemplate wikiTemplate = row;
        nullable1 = new Guid?();
        Guid? nullable2 = nullable1;
        wikiTemplate.ArticleID = nullable2;
        return;
      }
    }
    PXUIFieldAttribute.SetVisible<WikiTemplate.notificationID>(cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<WikiTemplate.articleID>(cache, (object) null, true);
    WikiTemplate wikiTemplate1 = row;
    nullable1 = new Guid?();
    Guid? nullable3 = nullable1;
    wikiTemplate1.NotificationID = nullable3;
  }

  private void TemplatePersiting(PXCache cache, PXRowPersistingEventArgs e) => e.Cancel = true;

  private object GetValue(System.Type sourceType)
  {
    System.Type field = sourceType;
    PXCache cach = this.View.Graph.Caches[BqlCommand.GetItemType(field)];
    return cach.Current != null ? cach.GetValue(cach.Current, field.Name) : (object) null;
  }
}
