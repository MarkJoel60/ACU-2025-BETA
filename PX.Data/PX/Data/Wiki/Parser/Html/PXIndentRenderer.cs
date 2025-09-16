// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXIndentRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXIndentElement HTML rendering.
/// </summary>
internal class PXIndentRenderer : PXHtmlRenderer
{
  private const string NumberedListClass = "wikinumlist";
  private const string NumberedItemClass = "wikibullet";
  private const string BulletListClass = "wikibulletlist";
  private const string BulletItemClass = "wikibullet";
  private const string DefinitionClass = "wikidefn";

  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXIndentContainer container = (PXIndentContainer) elem;
    this.StartList(container, resultHtml, settings);
    foreach (PXElement child1 in container.Children)
    {
      switch (child1)
      {
        case PXIndentContainer _:
          this.DoRender(child1, resultHtml, settings);
          break;
        case PXIndentElement _:
          this.StartListItem(container, resultHtml, settings);
          foreach (PXElement child2 in ((PXContainerElement) child1).Children)
            this.DoRender(child2, resultHtml, settings);
          this.EndListItem(container, resultHtml);
          break;
      }
    }
    this.EndList(container, resultHtml, settings);
  }

  private void StartList(
    PXIndentContainer container,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    switch (container.Type)
    {
      case '#':
        ++settings.Listlevel;
        if (settings.Listlevel % 2 == 1)
        {
          resultHtml.Append($"<ol {this.GetWikiClass("wikinumlist", settings)}>");
          break;
        }
        resultHtml.Append($"<ol {this.GetWikiClass("wikinumlist", settings)}type=\"a\">");
        break;
      case '*':
        resultHtml.Append($"<ul {this.GetWikiClass("wikibulletlist", settings)}>");
        break;
      case ':':
      case ';':
        resultHtml.Append($"<dl {this.GetWikiClass("wikidefn", settings)}>");
        break;
    }
    resultHtml.Append(Environment.NewLine);
  }

  private void StartListItem(
    PXIndentContainer container,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    switch (container.Type)
    {
      case '#':
        resultHtml.Append($"<li {this.GetWikiClass("wikibullet", settings)}>");
        break;
      case '*':
        resultHtml.Append($"<li {this.GetWikiClass("wikibullet", settings)}>");
        break;
      case ':':
        resultHtml.Append($"<dd {this.GetWikiClass("wikidefn", settings)}>");
        break;
      case ';':
        resultHtml.Append($"<dt {this.GetWikiClass("wikidefn", settings)}>");
        break;
    }
  }

  private void EndListItem(PXIndentContainer container, StringBuilder resultHtml)
  {
    switch (container.Type)
    {
      case '#':
      case '*':
        resultHtml.Append("</li>");
        break;
      case ':':
        resultHtml.Append("</dd>");
        break;
      case ';':
        resultHtml.Append("</dt>");
        break;
    }
  }

  private void EndList(
    PXIndentContainer container,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    resultHtml.Append(Environment.NewLine);
    switch (container.Type)
    {
      case '#':
        ++settings.Listlevel;
        resultHtml.Append("</ol>");
        break;
      case '*':
        resultHtml.Append("</ul>");
        break;
      case ':':
      case ';':
        resultHtml.Append("</dl>");
        break;
    }
  }
}
