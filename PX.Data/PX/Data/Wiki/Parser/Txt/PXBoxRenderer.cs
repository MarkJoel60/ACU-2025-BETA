// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>Represents a class for rendering PXBoxElement to txt.</summary>
internal class PXBoxRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    string caption = this.GetCaption(elem);
    int num = resultTxt.ColsCount == 0 ? 80 /*0x50*/ : resultTxt.ColsCount;
    bool fillEachLine = resultTxt.FillEachLine;
    string linePrefix = resultTxt.LinePrefix;
    string lineSuffix = resultTxt.LineSuffix;
    resultTxt.NewLine();
    resultTxt.Append("┌─");
    resultTxt.Append(caption);
    for (int index = 2 + caption.Length + resultTxt.LinePrefix.Length; index < num - resultTxt.LineSuffix.Length - 1; ++index)
      resultTxt.Append("─");
    resultTxt.Append("┐");
    resultTxt.CompleteCurrentLine();
    resultTxt.LinePrefix += "│ ";
    resultTxt.LineSuffix = " │" + resultTxt.LineSuffix;
    resultTxt.FillEachLine = true;
    this.RenderInner(elem, resultTxt);
    resultTxt.CompleteCurrentLine();
    resultTxt.LinePrefix = linePrefix;
    resultTxt.LineSuffix = lineSuffix;
    resultTxt.FillEachLine = fillEachLine;
    resultTxt.NewLine();
    resultTxt.Append("└─");
    for (int index = 2 + resultTxt.LinePrefix.Length; index < num - resultTxt.LineSuffix.Length - 1; ++index)
      resultTxt.Append("─");
    resultTxt.Append("┘");
  }

  protected virtual string GetCaption(PXElement elem)
  {
    PXBoxElement pxBoxElement = (PXBoxElement) elem;
    if (pxBoxElement.IsHintBox)
      return "Hint";
    if (pxBoxElement.IsWarnBox)
      return "Warning!";
    if (pxBoxElement.IsDangerBox)
      return "Danger";
    return pxBoxElement.IsGoodPracticeBox ? "Good Practice" : "";
  }

  protected virtual void RenderInner(PXElement box, PXTxtRenderContext resultTxt)
  {
    foreach (PXElement child in ((PXContainerElement) box).Children)
      this.DoRender(child, resultTxt);
  }
}
