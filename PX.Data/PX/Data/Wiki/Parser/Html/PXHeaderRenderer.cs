// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.PXHeaderRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SP;
using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

/// <summary>
/// Represents a class for PXHeaderElement HTML rendering.
/// </summary>
internal class PXHeaderRenderer : PXHtmlRenderer
{
  public override void Render(
    PXElement elem,
    StringBuilder resultHtml,
    PXWikiParserContext settings)
  {
    PXHeaderElement e = (PXHeaderElement) elem;
    this.AddEmptySpace(resultHtml, settings);
    if (e.IsError)
    {
      this.RenderError(e, resultHtml);
    }
    else
    {
      if (!settings.IsDesignMode && !settings.IsSimpleRender)
        resultHtml.Append($"<a id=\"Section{e.SectionID.ToString()}{settings.IDSuffix}\"></a>");
      int index;
      if (e.IsCollapsable)
      {
        int? nullable = e.parent is PXHeaderElement parent ? new int?(parent.SectionID) : new int?();
        string str1 = nullable.HasValue ? $"ParentSec='Sec{nullable.Value}'" : string.Empty;
        string wikiClass = this.GetWikiClass($"wiki{e.Name} separator collapsible{(e.IsDefaultExpanded ? string.Empty : " collapsed")}", settings);
        StringBuilder stringBuilder = resultHtml;
        string[] strArray = new string[8]
        {
          "<",
          e.Name,
          " ",
          wikiClass,
          " Collapse ='Sec",
          null,
          null,
          null
        };
        index = e.SectionID;
        strArray[5] = index.ToString();
        strArray[6] = "' ";
        strArray[7] = str1;
        string str2 = string.Concat(strArray);
        stringBuilder.Append(str2);
      }
      else
        resultHtml.Append($"<{e.Name} {this.GetWikiClass($"wiki{e.Name} separator", settings)}");
      if (settings.AllowSectionsExpand && settings.EnableScript && e.HasExpTag && !settings.IsDesignMode && !settings.IsSimpleRender)
        resultHtml.Append($" id='expandableSection{settings.IDSuffix}' name='expandableSection{settings.IDSuffix}'");
      resultHtml.Append("><div class='filler'></div>");
      bool flag1 = e.Level == SectionLevel.H1 && settings.EnableScript && !settings.IsDesignMode && !settings.IsSimpleRender;
      bool flag2 = settings.RenderSectionLink && settings.EnableScript && !settings.IsDesignMode && !settings.IsSimpleRender && !PortalHelper.IsPortalContext();
      if (flag1)
      {
        string str = flag2 ? "jumptopedit" : "jumptop";
        resultHtml.Append($"<a class='{str}' id=\"sectionEditLink1{settings.IDSuffix}\" href=\"#Sec0\" {this.GetWikiClass("wikilink editsectionlink", settings)}>{PXMessages.LocalizeNoPrefix("Back to Top")}</a>");
      }
      if (flag2)
      {
        string str3 = flag1 ? "editwikitop" : "editwiki";
        StringBuilder stringBuilder = resultHtml;
        string[] strArray = new string[17];
        strArray[0] = "<a class='";
        strArray[1] = str3;
        strArray[2] = "' id=\"sectionEditLink";
        strArray[3] = settings.IDSuffix;
        strArray[4] = "\" name=\"sectionEditLink";
        strArray[5] = settings.IDSuffix;
        strArray[6] = "\" href=\"#\" sectionForEdit=\"";
        index = e.SectionID;
        strArray[7] = index.ToString();
        strArray[8] = "\" editText=\"";
        strArray[9] = settings.Settings.EditLinkText;
        strArray[10] = "\" closeText=\"";
        strArray[11] = settings.Settings.CloseLinkText;
        strArray[12] = "\" ";
        strArray[13] = this.GetWikiClass("wikilink editsectionlink", settings);
        strArray[14] = ">";
        strArray[15] = settings.Settings.EditLinkText;
        strArray[16 /*0x10*/] = "</a>";
        string str4 = string.Concat(strArray);
        stringBuilder.Append(str4);
      }
      if (settings.IsDesignMode && e.HasExpTag)
      {
        resultHtml.Append("<span style=\"display: none;\">{exp}</span>");
        if (e.HasCollapsedTag)
          resultHtml.Append("<span style=\"display: none;\">{collapsed}</span>");
      }
      PXElement[] children = e.Children;
      for (index = 0; index < children.Length; ++index)
      {
        PXElement el = children[index];
        switch (el)
        {
          case PXTextElement _:
          case PXLinkElement _:
          case PXImageElement _:
          case PXRssLink _:
          case PXStyledTextElement _:
            this.DoRender(el, resultHtml, settings);
            break;
        }
      }
      if (e.IsCollapsable)
        resultHtml.Append($"<div class='fold-wrap'>&nbsp;<span class='fold-arrow {(!e.IsCollapsable || e.IsDefaultExpanded ? string.Empty : " tilt")}'><span></span></span></div>");
      resultHtml.Append($"</{e.Name}>");
    }
  }

  private void AddEmptySpace(StringBuilder resultHtml, PXWikiParserContext settings)
  {
    resultHtml.Append($"{Environment.NewLine}<div {this.GetWikiClass("emptyspace", settings)}></div>{Environment.NewLine}");
  }

  private void RenderError(PXHeaderElement e, StringBuilder resultHtml)
  {
    resultHtml.Append("<span style=\"color: red\">Unclosed header: ");
    resultHtml.Append(e.Name);
    resultHtml.Append(", cannot render section!</span>");
  }
}
