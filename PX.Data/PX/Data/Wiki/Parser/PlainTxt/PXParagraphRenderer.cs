// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXParagraphRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

internal class PXParagraphRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXParagraphElement paragraphElement = (PXParagraphElement) elem;
    if (paragraphElement.Children.Length == 0)
      return;
    if (resultTxt.Result.Length != 0 && !resultTxt.Result.EndsWith(Environment.NewLine))
      resultTxt.Append(Environment.NewLine);
    foreach (PXElement child in paragraphElement.Children)
      this.DoRender(child, resultTxt);
    resultTxt.Append(Environment.NewLine);
  }
}
