// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXTxtLinkRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>Represents a class for PXLinkElement txt rendering.</summary>
internal class PXTxtLinkRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXLinkElement e = (PXLinkElement) elem;
    PXTxtRenderContext resultTxt1 = new PXTxtRenderContext();
    PXTxtRenderContext resultTxt2 = new PXTxtRenderContext();
    foreach (PXElement linkElement in e.GetLinkElements())
      this.DoRender(linkElement, resultTxt1);
    foreach (PXElement captionElement in e.GetCaptionElements())
      this.DoRender(captionElement, resultTxt2);
    LinkInfo linkInfo = e.IsFileLink ? resultTxt.Settings.CreateFileLink(resultTxt1.Result, e) : resultTxt.Settings.CreateArticleLink(resultTxt1.Result);
    string str = linkInfo == null || linkInfo.IsInvalid ? resultTxt2.Result : linkInfo.DefaultCaption;
    if (string.IsNullOrEmpty(str))
      str = resultTxt2.Result;
    if (string.IsNullOrEmpty(str))
      str = resultTxt1.Result;
    resultTxt.Append(str);
  }
}
