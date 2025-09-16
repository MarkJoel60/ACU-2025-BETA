// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXParagraphRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

internal class PXParagraphRenderer : PXRtfRenderer
{
  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    PXElement[] pxElementArray = this.RemoveEmpty(((PXContainerElement) elem).Children);
    if (pxElementArray.Length == 0)
      return;
    PXRtfBuilder rtf1 = new PXRtfBuilder(rtf.Document.Settings);
    rtf1.Settings = rtf.Settings;
    rtf1.CurrentTableLevel = rtf.CurrentTableLevel;
    PXParagraph elem1 = new PXParagraph(rtf.Document);
    for (int index = 0; index < pxElementArray.Length; ++index)
    {
      rtf1.AddString(Environment.NewLine);
      if (index == 0 && pxElementArray[index] is PXTextElement)
        rtf1.AddString(((PXTextElement) pxElementArray[index]).Value.TrimStart());
      else
        this.DoRender(pxElementArray[index], rtf1);
    }
    elem1.Children.Add((PXRtfElement) new PXRawText(rtf.Document, rtf1.Document.Content.ToString()));
    rtf.AddRtfElement((PXRtfElement) elem1);
  }

  private PXElement[] RemoveEmpty(PXElement[] children)
  {
    List<PXElement> pxElementList = new List<PXElement>();
    foreach (PXElement child in children)
    {
      if (!(child is PXTextElement) || !string.IsNullOrEmpty(child.ToString().Trim()))
        pxElementList.Add(child);
    }
    return pxElementList.ToArray();
  }
}
