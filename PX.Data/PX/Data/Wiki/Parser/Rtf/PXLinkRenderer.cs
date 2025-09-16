// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXLinkRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Drawing;
using System.IO;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXLinkElement HTML rendering.</summary>
internal class PXLinkRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXLinkElement e = (PXLinkElement) elem;
    PXRtfBuilder rtf1 = new PXRtfBuilder(rtf.Document.Settings);
    PXRtfBuilder rtf2 = new PXRtfBuilder(rtf.Document.Settings);
    rtf1.Settings = rtf2.Settings = rtf.Settings;
    foreach (PXElement linkElement in e.GetLinkElements())
      this.DoRender(linkElement, rtf1);
    foreach (PXElement captionElement in e.GetCaptionElements())
      this.DoRender(captionElement, rtf2);
    LinkInfo linkInfo = this.RenderHref(e, rtf1.Document.Content.ToString().Trim(), rtf2.Document.Content.ToString().Trim(), e.Props, rtf);
    if (linkInfo == null)
      return;
    rtf.AddString(PXHtmlFormatter.GetFileInfoLink(linkInfo.FileInfoPath, rtf.Settings));
  }

  private LinkInfo RenderHref(
    PXLinkElement e,
    string link,
    string caption,
    string props,
    PXRtfBuilder rtf)
  {
    if (string.IsNullOrEmpty(caption))
    {
      int num = link.IndexOf("\\");
      caption = num > 0 ? link.Substring(num + 1) : link;
    }
    if (string.Compare(link, "anchor", true) == 0)
    {
      caption = caption.Length > 1 ? caption.Substring(1) : caption;
      rtf.AddRtfElement((PXRtfElement) new PXBookmark(rtf.Document, caption));
      return (LinkInfo) null;
    }
    if (link.Length > 0 && link[0] == '^')
    {
      caption = caption == link ? caption.Remove(0, 1) : caption;
      link = link.Remove(0, 1);
    }
    if (link.IndexOf('@') != -1)
    {
      rtf.AddRtfElement((PXRtfElement) new PXHyperlink(rtf.Document, "mailto:" + link)
      {
        Caption = caption
      });
      return (LinkInfo) null;
    }
    if (e.IsFileLink)
    {
      string imageSource = this.GetImageSource(link, e, rtf);
      LinkInfo fileLink = rtf.Settings.CreateFileLink(link, e);
      if (rtf.Settings.HideBrokenLink && (fileLink == null || !fileLink.IsExisting))
        return (LinkInfo) null;
      if (fileLink != null)
      {
        caption = !(caption == link) || string.IsNullOrEmpty(fileLink.DefaultCaption) ? caption : fileLink.DefaultCaption;
        link = fileLink.Url;
        imageSource = this.GetImageSource(fileLink.Extension, e, rtf);
      }
      PXHyperlink elem = new PXHyperlink(rtf.Document, link);
      Image icon = PXLinkRenderer.GetIcon(imageSource);
      if (icon != null)
        rtf.AddImage(icon);
      elem.CaptionColor = fileLink.IsExisting ? Color.Blue : Color.FromArgb(153, 0, 0);
      elem.Caption = caption;
      rtf.AddRtfElement((PXRtfElement) elem);
      return fileLink;
    }
    if (this.IsExternalLink(ref link))
    {
      rtf.AddRtfElement((PXRtfElement) new PXHyperlink(rtf.Document, link)
      {
        Caption = caption
      });
      return (LinkInfo) null;
    }
    this.RenderInternalLink(link, caption, rtf);
    return (LinkInfo) null;
  }

  private void RenderInternalLink(string link, string caption, PXRtfBuilder rtf)
  {
    if (link.Length > 0 && link[0] == '~' && !string.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath))
    {
      rtf.AddRtfElement((PXRtfElement) new PXHyperlink(rtf.Document, PXLinkRenderer.ToAbsolute(link))
      {
        Caption = caption
      });
    }
    else
    {
      LinkInfo articleLink = rtf.Settings.CreateArticleLink(link);
      if (rtf.Settings.HideBrokenLink && (articleLink == null || !articleLink.IsExisting) || HttpContext.Current == null)
        return;
      if (articleLink != null && articleLink.IsInvalid)
      {
        rtf.SetTextColor(Color.Red);
        rtf.AddString(PXMessages.Localize("(invalid link)", out string _));
        rtf.AddString(string.IsNullOrEmpty(caption) || caption == link ? "" : ": " + caption);
        rtf.DisableColor();
      }
      else
      {
        if (articleLink != null)
        {
          caption = !(caption == link) || string.IsNullOrEmpty(articleLink.DefaultCaption) ? caption : articleLink.DefaultCaption;
          link = articleLink.Url;
        }
        rtf.AddRtfElement((PXRtfElement) new PXHyperlink(rtf.Document, link)
        {
          CaptionColor = (articleLink.IsExisting ? Color.Blue : Color.FromArgb(153, 0, 0)),
          Caption = caption
        });
      }
    }
  }

  private string GetImageSource(string url, PXLinkElement e, PXRtfBuilder rtf)
  {
    if (string.IsNullOrEmpty(url) || e.NoIcon)
      return "";
    string extension = Path.GetExtension(url);
    if (string.IsNullOrEmpty(extension))
      return "";
    string defaultExtensionImage;
    if (!rtf.Settings.Settings.ExtensionsImages.TryGetValue(extension.Trim().ToLower(), out defaultExtensionImage))
    {
      defaultExtensionImage = rtf.Settings.Settings.DefaultExtensionImage;
      if (string.IsNullOrEmpty(defaultExtensionImage))
        return "";
    }
    return defaultExtensionImage;
  }

  public static string ToAbsolute(string link)
  {
    int num = link.IndexOf('?');
    link = num == -1 ? VirtualPathUtility.ToAbsolute(link) : VirtualPathUtility.ToAbsolute(link.Substring(0, num)) + link.Substring(num);
    return link;
  }

  private bool IsExternalLink(ref string link)
  {
    link = link.ToLower();
    if (link.StartsWith("http://"))
      return true;
    if (link.StartsWith("www."))
    {
      link = "http://" + link;
      return true;
    }
    return link.StartsWith("https://") || link.StartsWith("ftp://") || link.StartsWith("irc://");
  }

  public static Image GetIcon(string imgUrl)
  {
    if (HttpContext.Current == null)
      return (Image) null;
    try
    {
      imgUrl = new Uri(imgUrl).GetComponents(UriComponents.PathAndQuery | UriComponents.Scheme | UriComponents.Host, UriFormat.UriEscaped);
      string components = new Uri(PXUrl.SiteUrlWithPath()).GetComponents(UriComponents.PathAndQuery | UriComponents.Scheme | UriComponents.Host, UriFormat.UriEscaped);
      string str = components.EndsWith("/") ? components : components + "/";
      imgUrl = HttpContext.Current.Request.PhysicalApplicationPath + imgUrl.Substring(str.Length).Replace('/', '\\');
      return Image.FromFile(imgUrl);
    }
    catch
    {
      return (Image) null;
    }
  }
}
