// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXLinkRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>Represents a class for PXLinkElement HTML rendering.</summary>
public class PXLinkRenderer : PXHtmlRenderer
{
  private readonly bool _paramsToUrl;

  public PXLinkRenderer(bool paramsToUrl) => this._paramsToUrl = paramsToUrl;

  public PXLinkRenderer()
    : this(false)
  {
  }

  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXLinkElement e = (PXLinkElement) elem;
    StringBuilder resultHtml1 = new StringBuilder();
    StringBuilder resultHtml2 = new StringBuilder();
    StringBuilder stringBuilder = new StringBuilder();
    foreach (PXElement linkElement in e.GetLinkElements())
      this.DoRender(linkElement, resultHtml1, settings);
    foreach (PXElement captionElement in e.GetCaptionElements())
      this.DoRender(captionElement, resultHtml2, settings);
    LinkInfo linkInfo;
    if (e.IsPopup && e.IsFileLink)
    {
      resultHtml.Append("<span id=\"clickableLink");
      resultHtml.Append(settings.IDSuffix);
      resultHtml.Append("\" name=\"clickableLink");
      resultHtml.Append(settings.IDSuffix);
      resultHtml.Append("\" frWidth=\"");
      resultHtml.Append(e.Width > 0 ? e.Width.ToString() : "640");
      resultHtml.Append("\"");
      resultHtml.Append(" frHeight=\"");
      resultHtml.Append(e.Width <= 0 || e.Height <= 0 ? "480" : e.Height.ToString());
      resultHtml.Append("\">");
      linkInfo = this.RenderHref(e, resultHtml1.ToString(), resultHtml2.ToString(), e.Props, resultHtml, settings);
      resultHtml.Append("</span>");
    }
    else
      linkInfo = this.RenderHref(e, resultHtml1.ToString().Trim(), resultHtml2.ToString(), e.Props, resultHtml, settings);
    if (linkInfo == null)
      return;
    resultHtml.Append(PXHtmlFormatter.GetFileInfoLink(linkInfo.FileInfoPath, settings));
  }

  private LinkInfo RenderHref(
    PXLinkElement e,
    string link,
    string caption,
    string props,
    StringBuilder result,
    PXWikiParserContext settings)
  {
    string wikiTag = this.GetWikiTag(e, link);
    if (string.IsNullOrEmpty(caption))
    {
      int num = link.IndexOf("\\");
      caption = num > 0 ? link.Substring(num + 1) : link;
    }
    if (string.Compare(link, "anchor", true) == 0)
    {
      caption = caption.Length > 1 ? caption.Substring(1) : caption;
      props = StyleBuilder.MergeCssClasses($"{this.GetWikiClass("wikilink anchorlink", settings)} {props}");
      result.Append($"<a {wikiTag}id=\"{caption}\" {props}></a>");
      return (LinkInfo) null;
    }
    string target = "";
    if (e.IsInNewWindow)
      target = " target=\"_blank\"";
    if (link.IndexOf('@') != -1)
    {
      props = StyleBuilder.MergeCssClasses($"{this.GetWikiClass("wikilink emaillink", settings)} {props}");
      result.Append($"<a {wikiTag}href=\"mailto:{link}\" {props}>{caption}</a>");
      return (LinkInfo) null;
    }
    if (e.IsFileLink)
    {
      string str = "wikilink pagelink " + settings.Settings.SearchLink;
      string imageSource = this.GetImageSource(link, e, settings);
      LinkInfo fileLink = settings.CreateFileLink(link, e);
      string wkclass = fileLink == null || !fileLink.IsExisting ? "unknownlink" : str;
      props = StyleBuilder.MergeCssClasses($"{this.GetWikiClass(wkclass, settings)} {props}");
      if (settings.HideBrokenLink && (fileLink == null || !fileLink.IsExisting))
        return (LinkInfo) null;
      if (fileLink == null)
      {
        props = StyleBuilder.MergeCssClasses($"{this.GetWikiClass(wkclass, settings)} {props}");
      }
      else
      {
        caption = !(caption == link) || string.IsNullOrEmpty(fileLink.DefaultCaption) ? caption : fileLink.DefaultCaption;
        link = fileLink.Url;
        imageSource = this.GetImageSource(fileLink.Extension, e, settings);
      }
      if (!string.IsNullOrEmpty(imageSource))
        result.Append($"<a href=\"{link}\" wikitag=\"\">{imageSource}</a>");
      result.Append($"<a {wikiTag}href=\"{link}\" {props}{target}>{caption}</a> ");
      return fileLink;
    }
    if (this.IsExternalLink(ref link))
    {
      if (string.IsNullOrEmpty(target))
        target = " target=\"_blank\"";
      props = StyleBuilder.MergeCssClasses($"{this.GetWikiClass("wikilink externallink", settings)} {props}");
      result.Append($"<a {wikiTag}href=\"{link}\"{target} {props}>{caption}</a>");
      return (LinkInfo) null;
    }
    this.RenderInternalLink(link, caption, props, target, wikiTag, e.IsPopup, result, settings);
    return (LinkInfo) null;
  }

  private void RenderInternalLink(
    string link,
    string caption,
    string props,
    string target,
    string wikiTag,
    StringBuilder result,
    PXWikiParserContext settings)
  {
    this.RenderInternalLink(link, caption, props, target, wikiTag, false, result, settings);
  }

  private void RenderInternalLink(
    string link,
    string caption,
    string props,
    string target,
    string wikiTag,
    bool popup,
    StringBuilder result,
    PXWikiParserContext settings)
  {
    string wkclass1 = "wikilink pagelink " + settings.Settings.SearchLink;
    if (link.Length > 0 && link[0] == '#')
      result.Append($"<a {wikiTag}href=\"{link}\"{target} {StyleBuilder.MergeCssClasses($"{this.GetWikiClass(wkclass1, settings)} {props}")}>{caption}</a>");
    else if (link.Length > 0 && link[0] == '~' && !string.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath))
    {
      string str1 = settings.Settings.RootUrl + link.Substring(1);
      caption = caption == link ? str1 : caption;
      if (popup)
      {
        string str2 = $"javascript:window.open('{str1}');";
        result.Append($"<a {wikiTag}onclick=\"{str2}\" href=\"\" {StyleBuilder.MergeCssClasses($"{this.GetWikiClass(wkclass1, settings)} {props}")}>{caption}</a>");
      }
      else
      {
        if (link.StartsWith("~/?ScreenId"))
          target = " target = \"_top\" ";
        result.Append($"<a {wikiTag}href=\"{str1}\"{target} {StyleBuilder.MergeCssClasses($"{this.GetWikiClass(wkclass1, settings)} {props}")}>{caption}</a>");
      }
    }
    else
    {
      string str = (string) null;
      int num = link.IndexOf('#');
      if (link.Length > 0 && link.IndexOf('#') > -1)
      {
        str = link.Substring(num, link.Length - num);
        link = link.Substring(0, num);
      }
      link = this.SupportLink(link);
      LinkInfo articleLink = settings.CreateArticleLink(link);
      string wkclass2 = articleLink == null || !articleLink.IsExisting ? "unknownlink" + settings.Settings.SearchUnknownLink : wkclass1 + settings.Settings.SearchUnknownLink;
      if (settings.HideBrokenLink && caption == link && (articleLink == null || articleLink.IsInvalid))
        return;
      if (articleLink != null && articleLink.IsInvalid && caption == link)
      {
        result.Append("<span style=\"color: red;\">" + PXMessages.Localize("(invalid link)", out string _));
        result.Append(string.IsNullOrEmpty(caption) || caption == link ? "" : ": " + caption);
        result.Append("</span>");
      }
      else
      {
        if (articleLink != null && articleLink.IsInvalid && caption != link)
          articleLink = settings.CreateArticleLink(caption);
        if (articleLink != null)
        {
          caption = !(caption == link) || string.IsNullOrEmpty(articleLink.DefaultCaption) ? caption : articleLink.DefaultCaption;
          link = articleLink.Url;
        }
        if (this._paramsToUrl && !string.IsNullOrEmpty(props))
          result.Append($"<a {wikiTag}href=\"{link}{str}&{props}\"{target} {StyleBuilder.MergeCssClasses(this.GetWikiClass(wkclass2, settings))} >{caption}</a>");
        else
          result.Append($"<a {wikiTag}href=\"{link}{str}\"{target} {StyleBuilder.MergeCssClasses($"{this.GetWikiClass(wkclass2, settings)} {props}")}>{caption}</a>");
      }
    }
  }

  private string GetImageSource(string url, PXLinkElement e, PXWikiParserContext settings)
  {
    if (string.IsNullOrEmpty(url) || e.NoIcon)
      return "";
    string str = "";
    try
    {
      str = Path.GetExtension(url);
    }
    catch (ArgumentException ex)
    {
    }
    if (string.IsNullOrEmpty(str))
      return "";
    string defaultExtensionImage;
    if (!settings.Settings.ExtensionsImages.TryGetValue(str.Trim().ToLower(), out defaultExtensionImage))
    {
      defaultExtensionImage = settings.Settings.DefaultExtensionImage;
      if (string.IsNullOrEmpty(defaultExtensionImage))
        return "";
    }
    return $"<img wikitag=\"\" src=\"{defaultExtensionImage}\" {this.GetWikiClass("fileimg", settings)} />";
  }

  private string MoveBarsToEnd(string link)
  {
    int num = link.IndexOf('#');
    if (num == -1 || num == link.Length - 1)
      return link;
    int startIndex = link.IndexOf('&', num + 1);
    return startIndex == -1 ? link : link.Substring(0, num) + link.Substring(startIndex) + link.Substring(num, startIndex - num);
  }

  private string GetWikiTag(PXLinkElement e, string link)
  {
    string wikiTag = PXHtmlFormatter.GetWikiTag((PXElement) e);
    return string.IsNullOrEmpty(wikiTag) ? wikiTag : $"{wikiTag}wikitext=\"{HttpUtility.HtmlEncode(e.WikiText)}\" ";
  }

  public static string ToAbsolute(string link)
  {
    int num = link.IndexOf('?');
    link = num == -1 ? VirtualPathUtility.ToAbsolute(link) : VirtualPathUtility.ToAbsolute(link.Substring(0, num)) + link.Substring(num);
    return link;
  }

  private bool IsExternalLink(ref string link)
  {
    if (link.Length >= 7 && link.Substring(0, 7) == "http://")
      return true;
    if (link.Length >= 4 && link.Substring(0, 4) == "www.")
    {
      link = "http://" + link;
      return true;
    }
    return link.Length >= 8 && link.Substring(0, 8) == "https://" || link.Length >= 6 && link.Substring(0, 6) == "ftp://" || link.Length >= 6 && link.Substring(0, 6) == "irc://";
  }

  public virtual string SupportLink(string link) => link;
}
