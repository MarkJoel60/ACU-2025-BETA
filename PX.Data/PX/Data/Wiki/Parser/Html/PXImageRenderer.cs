// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXImageRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>Represents a class for PXImageElement HTML rendering.</summary>
public class PXImageRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXImageElement e = (PXImageElement) elem;
    LinkInfo imageLink = settings.CreateImageLink(e, false);
    string image = this.GetImage(e, imageLink, settings);
    string imageDiv = this.GetImageDiv(e, image, imageLink, settings);
    if (e.Type != ImageType.None)
      resultHtml.Append(Environment.NewLine);
    resultHtml.Append(imageDiv);
  }

  private string GetImage(PXImageElement e, LinkInfo linfo, PXWikiParserContext settings)
  {
    string url = linfo.Url;
    StringBuilder resultHtml = new StringBuilder();
    if (e.IsClickable)
    {
      if (!string.IsNullOrEmpty(e.NavigateUrl))
      {
        resultHtml.Append("<a href=\"");
        resultHtml.Append(e.NavigateUrl.StartsWith("http://") || e.NavigateUrl.StartsWith("https://") ? e.NavigateUrl : PXUrl.SiteUrlWithPath() + e.NavigateUrl);
        resultHtml.Append($"\" title=\"{e.Caption}\">");
      }
      else if (!string.IsNullOrEmpty(e.InternalLink))
      {
        LinkInfo articleLink = settings.CreateArticleLink(e.InternalLink);
        if (articleLink != null && !articleLink.IsInvalid)
        {
          resultHtml.Append("<a href=\"" + articleLink.Url);
          resultHtml.Append($"\" title=\"{e.Caption}\">");
        }
      }
      else
      {
        resultHtml.Append("<a href=\"" + url);
        resultHtml.Append($"\" title=\"{e.Caption}\"");
        if (settings.EnableScript && e.Type == ImageType.Popup)
        {
          string str = $"'clickableImage{settings.IDSuffix}'";
          resultHtml.Append($" id={str} name={str}");
        }
        resultHtml.Append(">");
      }
    }
    this.AppendParameters(e, linfo, resultHtml, settings);
    if (e.IsClickable)
      resultHtml.Append("</a>");
    return resultHtml.ToString();
  }

  private string GetImageDiv(
    PXImageElement e,
    string image,
    LinkInfo linfo,
    PXWikiParserContext settings)
  {
    string wikiTag = this.GetWikiTag(e);
    switch (e.Location)
    {
      case ImageLocation.Left:
        image = $"<div {wikiTag}{this.GetWikiClass("imageleft", settings)}>{this.GetImageType(e, image, linfo, "", settings)}</div>";
        break;
      case ImageLocation.Right:
        image = $"<div {wikiTag}{this.GetWikiClass("imageright", settings)}>{this.GetImageType(e, image, linfo, "", settings)}</div>";
        break;
      case ImageLocation.Center:
        image = $"<div {wikiTag}{this.GetWikiClass("imagecenter", settings)}><table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" style=\"margin-left: auto; margin-right: auto;\"><tr><td style=\"text-align: center;\">{this.GetImageType(e, image, linfo, "", settings)}</td></tr></table></div>";
        break;
      default:
        image = this.GetImageType(e, image, linfo, wikiTag, settings);
        break;
    }
    return image;
  }

  private string GetImageType(
    PXImageElement e,
    string image,
    LinkInfo linfo,
    string wikiTag,
    PXWikiParserContext settings)
  {
    switch (e.Type)
    {
      case ImageType.Border:
        image = $"<table {wikiTag}{this.GetWikiClass("imageauto", settings)} cellpadding=\"0\" cellspacing=\"0\"><tr><td {this.GetWikiClass("imageauto", settings)} style=\"text-align: center;\">{image}</td></tr></table>{(!e.HasEditLink || settings.EnableScript ? "" : PXHtmlFormatter.GetFileInfoLink(linfo.FileInfoPath, settings))}";
        break;
      case ImageType.Frame:
      case ImageType.Thumb:
      case ImageType.Popup:
        image = $"<table {wikiTag}{this.GetWikiClass("imageauto", settings)} cellpadding=\"0\" cellspacing=\"0\"><tr><td {this.GetWikiClass("imageauto", settings)} style=\"text-align: center;\">{image}{this.GetImageDescription(e, linfo, settings)}</td></tr></table>";
        break;
      default:
        image = $"<span {wikiTag} style=\"white-space: nowrap;\">{image}{(!e.HasEditLink || settings.EnableScript ? "" : PXHtmlFormatter.GetFileInfoLink(linfo.FileInfoPath, settings))}</span>";
        break;
    }
    return image;
  }

  private string GetImageDescription(
    PXImageElement e,
    LinkInfo linfo,
    PXWikiParserContext settings)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append($"<div {this.GetWikiClass("imagedescription", settings)}>");
    if (!string.IsNullOrEmpty(settings.Settings.MagnifyImageUrl))
    {
      stringBuilder.Append("<div");
      if (e.IsClickable && e.Type == ImageType.Popup && settings.EnableScript)
      {
        string str = $"'clickableImageMagnify{settings.IDSuffix}'";
        stringBuilder.Append($" id={str} name={str}");
      }
      stringBuilder.Append(" style=\"float: right;\">");
      stringBuilder.Append($"<a href=\"{linfo.Url}\" title=\"{PXMessages.Localize("Enlarge")}\">");
      stringBuilder.Append($"<img data-wikiimage=1 src=\"{settings.Settings.MagnifyImageUrl}\" /></a></div>");
    }
    stringBuilder.Append(e.Caption + " ");
    stringBuilder.Append(!e.HasEditLink || settings.EnableScript ? "" : PXHtmlFormatter.GetFileInfoLink(linfo.FileInfoPath, settings));
    stringBuilder.Append("</div>");
    return stringBuilder.ToString();
  }

  private string GetWikiTag(PXImageElement e)
  {
    string wikiTag = PXHtmlFormatter.GetWikiTag((PXElement) e);
    return string.IsNullOrEmpty(wikiTag) ? wikiTag : $"{wikiTag}wikitext=\"{HttpUtility.HtmlEncode(e.WikiText)}\" ";
  }

  public virtual void AppendParameters(
    PXImageElement e,
    LinkInfo linfo,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    this.EmbedImage(e, linfo, resultHtml, settings);
  }

  public void EmbedImage(
    PXImageElement e,
    LinkInfo linfo,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    if (string.Compare(linfo.Extension, ".swf", true) == 0)
    {
      linfo.Url += "&second=1";
      int num = e.Width != -1 ? 1 : (e.Type == ImageType.Thumb ? 1 : 0);
      resultHtml.Append("<table id=\"VideoTable\" cellpadding=\"0\" cellspacing=\"0\">");
      resultHtml.Append("<tr>");
      resultHtml.Append("<td");
      resultHtml.Append(" frWidth=\"");
      resultHtml.Append(e.Width > 0 ? e.Width.ToString() : "640");
      resultHtml.Append("\"");
      resultHtml.Append(" frHeight=\"");
      resultHtml.Append(e.Width <= 0 || e.Height <= 0 ? "480" : e.Height.ToString());
      resultHtml.Append("\"");
      if (e.IsEmbedded)
      {
        resultHtml.Append(" style=\"text-align: center; vertical-align: center; width: ");
        resultHtml.Append(e.Width != -1 ? e.Width.ToString() : "180");
        resultHtml.Append("px; height: ");
        resultHtml.Append(e.Width == -1 || e.Height == -1 ? "180" : e.Height.ToString());
        resultHtml.Append("px;\">");
        resultHtml.AppendLine("<object id=\"Video\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" width=\"100%\" height=\"100%\">");
        resultHtml.AppendLine($"<param name=\"movie\" value=\"{linfo.Url}\" />");
        resultHtml.AppendLine("<param name=\"quality\" value=\"high\" />");
        resultHtml.AppendLine("<param name=\"scale\" value=\"exactfit\" />");
        resultHtml.AppendLine("<param name=\"wmode\" value=\"transparent\" />");
        resultHtml.AppendLine($"<embed id=\"Video\" wmode=\"transparent\" src=\"{linfo.Url}\" quality=\"high\" scale=\"exactfit\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"100%\" height=\"100%\"></embed>");
        resultHtml.Append("</object>");
      }
      else
      {
        resultHtml.Append(" style=\"text-align: center; vertical-align: center; width: 100px; height: 100px;\">");
        resultHtml.Append($"<img id=\"Video\" name=\"Video\" alt=\"{e.Caption}\" src=\"{settings.Settings.PlayFlashIconUrl}\" />");
      }
      resultHtml.Append("</td>");
      resultHtml.Append("</tr>");
      resultHtml.Append("</table>");
    }
    else
    {
      resultHtml.Append($"<img alt=\"{e.Caption}\" src=\"{linfo.Url}");
      if (e.Width != -1 && settings.ResizeImage(linfo, e.Width, e.Height))
      {
        resultHtml.Append("&amp;width=" + Convert.ToString(e.Width));
        if (e.Height != -1)
          resultHtml.Append("&amp;height=" + Convert.ToString(e.Height));
      }
      else if (e.Type == ImageType.Thumb && settings.ResizeImage(linfo, 180, -1))
        resultHtml.Append("&amp;width=180");
      resultHtml.Append("\" ");
      resultHtml.Append(e.Props);
      if (settings.EnableScript && settings.RenderSectionLink)
      {
        string str = $"'wikiImage{settings.IDSuffix}'";
        resultHtml.Append($" id={str} name={str}");
        resultHtml.Append(" editurl=\"");
        resultHtml.Append(linfo.FileInfoPath);
        resultHtml.Append("\"");
      }
      resultHtml.Append(" />");
    }
  }
}
