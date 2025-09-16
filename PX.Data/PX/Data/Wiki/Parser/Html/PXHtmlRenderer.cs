// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXHtmlRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a base class for all HTML renderers of wiki elements.
/// </summary>
public class PXHtmlRenderer : PXRenderer<PXHtmlRenderer>
{
  private StringBuilder resultHtml = new StringBuilder();
  protected WikiArticle model;

  /// <summary>Initializes a new instance of PXHtmlRenderer class.</summary>
  public PXHtmlRenderer()
  {
  }

  /// <summary>Initializes a new instance of PXHtmlRenderer class.</summary>
  /// <param name="settings">Formatting settings.</param>
  public PXHtmlRenderer(PXWikiParserContext settings) => this.Context = settings;

  /// <summary>Gets resulting HTML containing rendered elements.</summary>
  public StringBuilder ResultHtml => this.resultHtml;

  /// <summary>
  /// Registers custom HTML renderers for PXElement-derived objects.
  /// Override this method to provide rendering of your custom objects.
  /// </summary>
  protected override void OnRegisterCustomRenderers()
  {
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXEmptySpace), (PXHtmlRenderer) new PXEmptyRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXTextElement), (PXHtmlRenderer) new PXTextRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXStyledTextElement), (PXHtmlRenderer) new PXStyledTextRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXSectionElement), (PXHtmlRenderer) new PXSectionRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXHeaderElement), (PXHtmlRenderer) new PXHeaderRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXBoxElement), (PXHtmlRenderer) new PXBoxRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXCodeBoxElement), (PXHtmlRenderer) new PXCodeBoxRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXHorLineElement), (PXHtmlRenderer) new PXHorLineRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXHtmlTagElement), (PXHtmlRenderer) new PXHtmlTagRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXIndentContainer), (PXHtmlRenderer) new PXIndentRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXCommonSpecialTagElement), (PXHtmlRenderer) new PXSpecialTagRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXFileListTagElement), (PXHtmlRenderer) new PXFileListTagRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXLinkElement), (PXHtmlRenderer) new PXLinkRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXImageElement), (PXHtmlRenderer) new PXImageRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXEmbeddedVideoElement), (PXHtmlRenderer) new PXEmbeddedVideoRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXTableElement), (PXHtmlRenderer) new PXTableRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXTableRow), (PXHtmlRenderer) new PXTableRowRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXTableCell), (PXHtmlRenderer) new PXTableCellRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (SourceElement), (PXHtmlRenderer) new PXSourceRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (NowikiElement), (PXHtmlRenderer) new NowikiTagRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXParagraphElement), (PXHtmlRenderer) new PXParagraphRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXHiddenElement), (PXHtmlRenderer) new PXHiddenRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXRssLink), (PXHtmlRenderer) new PXRssRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXRssArticleLink), (PXHtmlRenderer) new PXRssArticleRenderer());
    PXRenderer<PXHtmlRenderer>.RegisterRenderer(typeof (PXHtmlContentElement), (PXHtmlRenderer) new PXHtmlTagRenderer());
  }

  /// <summary>
  /// Performs rendering of the given element if its renderer is available.
  /// </summary>
  /// <param name="el">An element to render.</param>
  /// <param name="resultHtml">A StringBuilder object which will contain rendered result.</param>
  protected void DoRender(PXElement el, StringBuilder resultHtml, PXWikiParserContext settings)
  {
    PXHtmlRenderer renderer = this.GetRenderer(el);
    if (renderer == null)
      return;
    if (el.NeedClear)
      resultHtml.Append(PXHtmlFormatter.GetClearDiv());
    renderer.model = this.model;
    renderer.Render(el, resultHtml, settings);
  }

  protected string GetWikiClass(string wkclass, PXWikiParserContext settings)
  {
    if (!settings.IsDesignMode)
      return $"class=\"{wkclass}\"";
    return $"class=\"{wkclass}\" wikicl=\"{wkclass}\"";
  }

  /// <summary>
  /// Renders specified element. The default implementation does nothing.
  /// Override this method and provide rendering logic in your custom renderers.
  /// </summary>
  /// <param name="elem">An element to render.</param>
  /// <param name="resultHtml">A StringBuilder object which will contain rendered result.</param>
  public virtual void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
  }

  public override void Render(WikiArticle output)
  {
    List<PXElement> elems = output.GetAllElements();
    this.model = output;
    this.RegisterCustomRenderers();
    this.CheckForRedirect(elems);
    this.CheckForStyleElement(elems);
    this.resultHtml.Length = 0;
    if (elems.Count != 0 && !(elems[0] is PXParagraphElement) && this.Context.IsDesignMode)
      this.resultHtml.AppendLine("<p>&nbsp;</p>");
    if (elems.Count == 1 && elems[0] is PXParagraphElement paragraphElement)
      elems = new List<PXElement>((IEnumerable<PXElement>) paragraphElement.Children);
    foreach (PXElement el in elems)
      this.DoRender(el, this.resultHtml, this.Context);
  }

  public override string GetResultingString(PXBlockParser.ParseContext parseContext)
  {
    return parseContext.Settings.IsSimpleRender ? this.resultHtml.ToString() : PXHtmlFormatter.GetResultingPage(this.ResultHtml.ToString(), this.Context, parseContext);
  }

  private void CheckForRedirect(List<PXElement> elems)
  {
    if (elems.Count == 0 || !(elems[0] is PXRedirectElement))
      return;
    PXLinkElement redirectLink = ((PXRedirectElement) elems[0]).RedirectLink;
    StringBuilder resultHtml = new StringBuilder();
    foreach (PXElement linkElement in redirectLink.GetLinkElements())
      this.DoRender(linkElement, resultHtml, this.Context);
    string absolute = resultHtml.ToString();
    if (resultHtml.Length > 0 && resultHtml[0] == '~')
      absolute = PXLinkRenderer.ToAbsolute(absolute);
    if (!this.Context.RedirectEnable)
      return;
    this.Context.Redirect(absolute);
  }

  private void CheckForStyleElement(List<PXElement> elems)
  {
    string str1 = "style";
    if (elems.Count <= 0 || !(elems[0] is PXTextElement))
      return;
    PXTextElement elem = (PXTextElement) elems[0];
    string str2 = elem.Value != null ? elem.Value.TrimStart() : (string) null;
    if (str2 == null || !str2.StartsWith(str1))
      return;
    string[] strArray = elem.Value.Split('=');
    if (strArray.Length <= 1)
      return;
    strArray[1] = strArray[1].TrimStart(' ', '\t');
    int index1 = 0;
    if (strArray[1][index1] != '"')
      return;
    int index2 = index1 + 1;
    StringBuilder stringBuilder = new StringBuilder();
    for (; index2 < strArray[1].Length && strArray[1][index2] != '"'; ++index2)
      stringBuilder.Append(strArray[1][index2]);
    if (stringBuilder.Length <= 0)
      return;
    this.Context.StylesPath = stringBuilder.ToString();
    elem.Value = index2 + 1 >= strArray[1].Length ? "" : strArray[1].Substring(index2 + 1);
  }
}
