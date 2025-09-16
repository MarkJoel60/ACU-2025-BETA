// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXHtmlFormatter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SP;
using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXHtmlFormatter
{
  public static string GetFileInfoLink(string url, PXWikiParserContext context)
  {
    if (!context.RenderSectionLink || string.IsNullOrEmpty(url))
      return string.Empty;
    return $"<a href=\"{url}\" alt=\"Show file revisions\" class=\"wikilink\" style=\"font-size: 12px;\">[{context.Settings.EditLinkText}]</a>";
  }

  public static string GetPageStyle(PXWikiParserContext context)
  {
    return string.IsNullOrEmpty(context.StylesPath) ? string.Empty : $"<link href=\"{context.StylesPath}\" type=\"text/css\" rel=\"stylesheet\" />{Environment.NewLine}";
  }

  public static string GetContentSize(PXWikiParserContext context)
  {
    string str = " style=\"";
    if (context.ContentWidth > 0)
      str = $" width: {context.ContentWidth}px;";
    if (context.ContentHeight > 0)
      str += $" height: {context.ContentHeight}px; overflow-y: auto;";
    return str + "\"";
  }

  public static string GetPageTitle(PXWikiParserContext context)
  {
    string str = "";
    if (context.RenderSectionLink && !context.IsDesignMode && !PortalHelper.IsPortalContext())
      str = $"{$"{str}<a id=\"sectionEditLink{context.IDSuffix}\" name=\"sectionEditLink{context.IDSuffix}\" sectionForEdit=\"0\" editText=\"{context.Settings.EditLinkText}\" closeText=\"{context.Settings.CloseLinkText}\" href=\"#\" class=\"wikilink editsectionlink\">{context.Settings.EditLinkText}</a>"}<a id=\"Section0{context.IDSuffix}\"></a>";
    if (!string.IsNullOrEmpty(context.PageTitle))
      str = $"{str}<h1 class=\"pagetitle\">{context.PageTitle}</h1>" + "<br/>";
    return str + Environment.NewLine;
  }

  public static string GetPageAuthor(PXWikiParserContext context)
  {
    return !string.IsNullOrEmpty(context.CreatedByLogin) ? $"<div id=\"PageInfoDiv\">Created by {context.CreatedByLogin}</div>" : "";
  }

  public static string GetLastModificationInfo(PXWikiParserContext context)
  {
    string str = "";
    if (context.LastModified.HasValue)
    {
      str = "<div id=\"PageInfoDiv\" class=\"legend\">Modified: " + context.LastModified.Value.ToString("g");
      if (!string.IsNullOrEmpty(context.LastModifiedByLogin))
        str = $"{str} by {context.LastModifiedByLogin}";
    }
    else if (!string.IsNullOrEmpty(context.LastModifiedByLogin))
      str = "<div id=\"PageInfoDiv\">Modified by " + context.LastModifiedByLogin;
    return !(str == "") ? str + "</div>" : str;
  }

  public static string GetWikiTag(PXElement e)
  {
    return string.IsNullOrEmpty(e.WikiTag) ? "" : $"wikitag=\"{e.WikiTag}\" ";
  }

  public static string GetTOC(PXWikiParserContext context, WikiArticle model, bool isQuickGuide = false)
  {
    if (model.TocItems.Count == 0 || context.IsSimpleRender)
      return "";
    StringBuilder stringBuilder = new StringBuilder("<br /><table class=\"wiki TocBox\" ");
    if (context.IsDesignMode)
      stringBuilder.Append(" wikitag=\"toc\" ");
    stringBuilder.Append(" cellpadding=\"0px\" cellspacing=\"0px\"><tr><td>");
    string str1 = PXLocalizer.Localize(isQuickGuide ? "Steps" : "In This Topic");
    string str2 = PXLocalizer.Localize("Hide/Show");
    if (context.ExpandableTOC)
      stringBuilder.Append($"<span class=\"small\"><b>{str1}</b><span> [<a id=\"tocHideShow{context.IDSuffix}\" name=\"tocHideShow{context.IDSuffix}\" href=\"javascript:void 0;\">{str2}</a>]</span></span></td></tr>");
    else
      stringBuilder.Append($"<span class=\"small\"><b>{str1}</b></span></td></tr>");
    stringBuilder.Append("<tr><td><div id=\"Toc\">");
    for (int index = 0; index < model.TocItems.Count; ++index)
    {
      TOCItem tocItem = model.TocItems[index];
      string str3 = Convert.ToString(index + 1) + context.IDSuffix;
      switch (tocItem.level)
      {
        case 1:
          stringBuilder.Append($"<b><a href=\"#Section{str3}\" class=\"anchorlink\">{tocItem.header}</a></b><br />");
          break;
        case 2:
          stringBuilder.Append($"&nbsp;&nbsp;&nbsp;<a href=\"#Section{str3}\" class=\"anchorlink\">{tocItem.header}</a><br />");
          break;
        case 3:
          stringBuilder.Append($"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"#Section{str3}\" class=\"anchorlink\">{tocItem.header}</a><br />");
          break;
        case 4:
          stringBuilder.Append($"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<small><a href=\"#Section{str3}\" class=\"anchorlink\">{tocItem.header}</a></small><br />");
          break;
      }
    }
    stringBuilder.Append("</div></td></tr></table>");
    return stringBuilder.ToString();
  }

  public static string GetClearDiv() => "<div class=\"clear\"></div>";

  public static string GetResultingPage(
    string htmlText,
    PXWikiParserContext context,
    PXBlockParser.ParseContext parseContext)
  {
    return $"{(context.IsDesignMode ? "" : "<a id=\"PageTop\"></a>") + Environment.NewLine + (context.IsDesignMode ? "" : $"<div class=\"wiki\"{PXHtmlFormatter.GetContentSize(context)}>")}{Environment.NewLine}{PXHtmlFormatter.GetPageTitle(context) + (context.IsDesignMode ? "" : "<div id=\"Sec0\">")}{PXHtmlFormatter.GetPageAuthor(context)}{PXHtmlFormatter.GetLastModificationInfo(context)}{htmlText}{Environment.NewLine}{(context.IsDesignMode ? (object) "" : (object) "</div></div>")}";
  }
}
