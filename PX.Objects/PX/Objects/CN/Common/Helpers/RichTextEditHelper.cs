// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Helpers.RichTextEditHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using HtmlAgilityPack;

#nullable disable
namespace PX.Objects.CN.Common.Helpers;

public static class RichTextEditHelper
{
  public static string GetInnerText(string htmlText)
  {
    if (string.IsNullOrWhiteSpace(htmlText))
      return (string) null;
    HtmlDocument htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(htmlText);
    HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//body");
    if (htmlNode == null)
      return htmlText;
    return htmlNode?.InnerText.Replace("&nbsp;", string.Empty).Replace(" ", string.Empty);
  }
}
