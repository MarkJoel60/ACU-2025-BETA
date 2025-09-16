// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXWikiParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Wiki.Parser.Render.ScreenRelatedInfo;
using PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

public static class PXWikiParser
{
  /// <summary>Parses wiki mark-up and translates it into HTML.</summary>
  /// <param name="wikiText">Wiki-markup to translate.</param>
  /// <returns>Translated HTML or null if wikiText is null or empty.</returns>
  public static string Parse(string wikiText)
  {
    return PXWikiParser.Parse(wikiText, new PXWikiParserContext());
  }

  /// <summary>Parses wiki mark-up and translates it into HTML.</summary>
  /// <param name="wikiText">Wiki-markup to translate.</param>
  /// <param name="context">Settings which define how wikiText should be parsed and presented to user.</param>
  /// <returns>Translated HTML or null if wikiText is null or empty.</returns>
  public static string Parse(string wikiText, PXWikiParserContext context)
  {
    return PXWikiParser.Parse(wikiText, context, (PXGraph) null, (System.Type) null, (object[]) null);
  }

  /// <summary>
  /// Parses wiki mark-up and returns it in the format of the screen related information.
  /// </summary>
  /// <param name="wikiText">Wiki-markup to translate.</param>
  /// <param name="context">Settings which define how wikiText should be parsed and presented to user.</param>
  /// <returns>Translated HTML or null if wikiText is null or empty.</returns>
  [PXInternalUseOnly]
  public static IEnumerable<PXScreenRelatedInfoSection> ParseRelatedInformation(
    string wikiText,
    PXWikiParserContext context)
  {
    WikiArticle hierarchy = PXWikiParser.GetHierarchy(wikiText, context, (PXGraph) null, (System.Type) null, (object[]) null, out PXBlockParser.ParseContext _);
    if (hierarchy == null)
      return (IEnumerable<PXScreenRelatedInfoSection>) null;
    PXScreenRelatedInfoRenderer relatedInfoRenderer = new PXScreenRelatedInfoRenderer(context);
    relatedInfoRenderer.Render(hierarchy);
    return relatedInfoRenderer.GetResultingObject();
  }

  public static string TemplateParse(
    string wikiText,
    PXWikiParserContext context,
    PXGraph graph,
    System.Type entityType,
    object[] keys)
  {
    return PXWikiParser.PrepareWikiText(wikiText, context, graph, entityType, keys);
  }

  /// <summary>
  /// Format a wiki article, subsituting parameters from the graph type
  /// </summary>
  /// <param name="wikiText">Wiki-markup to translate.</param>
  /// <param name="context">Settings which define how wikiText should be parsed and presented to user.</param>
  /// <param name="graphType">Graph type.</param>
  /// <returns></returns>
  public static string Parse(
    string wikiText,
    PXWikiParserContext context,
    PXGraph graph,
    System.Type entityType,
    object[] keys)
  {
    context.SectionCount = 1;
    PXBlockParser.ParseContext parseContext;
    WikiArticle hierarchy = PXWikiParser.GetHierarchy(wikiText, context, graph, entityType, keys, out parseContext);
    if (hierarchy == null)
      return string.Empty;
    context.Renderer.Render(hierarchy);
    return context.Renderer.GetResultingString(parseContext);
  }

  public static WikiArticle ParseHierarchy(PXBlockParser.ParseContext context)
  {
    WikiArticle result = new WikiArticle();
    new PXBlockParser().Parse(context, result);
    return result;
  }

  public static WikiArticle ParseHierarchy(string wikiText, PXWikiParserContext context)
  {
    WikiArticle result = new WikiArticle();
    new PXBlockParser().Parse(new PXBlockParser.ParseContext(wikiText, 0, context), result);
    return result;
  }

  public static string PrepareWikiText(
    string wikiText,
    PXWikiParserContext context,
    PXGraph graph,
    System.Type entityType,
    object[] keys)
  {
    if (wikiText == null)
      return (string) null;
    string templateText = PXTemplatePreprocessor<PXTemplatePreprocessor>.Process(PXTemplatePreprocessor<PXGenericTemplatePreprocessor>.Process(PXTemplatePreprocessor<PXContainerTemplatePreprocessor>.Process(wikiText.Replace("\n", "\r\n").Replace("\r\r", "\r").TrimStart(), context), context), context);
    if (entityType != (System.Type) null)
      templateText = PXTemplateContentParser.Instance.Process(templateText, graph, entityType, keys);
    return templateText;
  }

  /// <summary>
  /// Returns specified section of wiki-markup (each section is a block of wiki-markup between two headers).
  /// </summary>
  /// <param name="wikiText">Wiki-markup to get specified section from.</param>
  /// <param name="nSection">Zero-based index of section to retrieve.</param>
  /// <returns>Text of the section (wiki-markup) or null if section could not be found.</returns>
  public static string GetSection(string wikiText, int nSection)
  {
    return PXWikiParser.GetSection(wikiText, nSection, out string _, out string _);
  }

  /// <summary>
  /// Returns specified section of wiki-markup (each section is a block of wiki-markup between two headers).
  /// </summary>
  /// <param name="wikitext">Wiki-markup to get specified section from.</param>
  /// <param name="nSection">Zero-based index of section to retrieve.</param>
  /// <param name="prevtext">After method finishes will contain wiki text preceding specified section.</param>
  /// <param name="nexttext">After method finishes will contain wiki text following specified section.</param>
  /// <returns>Text of the section (wiki-markup) or null if section could not be found.</returns>
  public static string GetSection(
    string wikitext,
    int nSection,
    out string prevtext,
    out string nexttext)
  {
    prevtext = nexttext = (string) null;
    if (string.IsNullOrEmpty(wikitext))
      return string.Empty;
    wikitext = wikitext.Replace("\n", "\r\n");
    wikitext = wikitext.Replace("\r\r", "\r");
    if (nSection == 0)
      return wikitext;
    WikiArticle result = new WikiArticle();
    new PXBlockParser().Parse(new PXBlockParser.ParseContext(wikitext, 0, new PXWikiParserContext()), result);
    int curSection = 1;
    PXSectionElement section = PXWikiParser.FindSection(result.GetAllElements().ToArray(), nSection, ref curSection);
    if (section == null)
      return (string) null;
    prevtext = wikitext.Substring(0, section.StartIndex);
    nexttext = wikitext.Substring(section.EndIndex);
    return wikitext.Substring(section.StartIndex, section.EndIndex - section.StartIndex);
  }

  private static PXSectionElement FindSection(PXElement[] elems, int nSection, ref int curSection)
  {
    foreach (PXElement elem in elems)
    {
      switch (elem)
      {
        case PXSectionElement _ when curSection == nSection:
          return (PXSectionElement) elem;
        case PXSectionElement _:
          ++curSection;
          PXSectionElement section = PXWikiParser.FindSection(((PXContainerElement) elem).Children, nSection, ref curSection);
          if (section != null)
            return section;
          break;
      }
    }
    return (PXSectionElement) null;
  }

  private static WikiArticle GetHierarchy(
    string wikiText,
    PXWikiParserContext context,
    PXGraph graph,
    System.Type entityType,
    object[] keys,
    out PXBlockParser.ParseContext parseContext)
  {
    string wikiText1 = PXWikiParser.PrepareWikiText(wikiText, context, graph, entityType, keys);
    parseContext = new PXBlockParser.ParseContext(wikiText1, 0, context);
    return PXWikiParser.ParseHierarchy(parseContext);
  }

  public static string EncodeSpecialChars(string text)
  {
    return PXBlockParser.EncodeSpecialChars(PXBlockParser.EncodeSpecialChars(PXBlockParser.EncodeSpecialChars(PXBlockParser.EncodeSpecialChars(PXBlockParser.EncodeSpecialChars(text, (PXBlockParser) new PXContainerTemplatePreprocessor()), (PXBlockParser) new PXGenericTemplatePreprocessor()), (PXBlockParser) new PXTemplatePreprocessor()), (PXBlockParser) PXTemplateContentParser.ScriptInstance), new PXBlockParser((IEnumerable<TokenLexems>) PXLexemsTable.ReservedLexems));
  }
}
