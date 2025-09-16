// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

/// <summary>Represents a class for PXTextElement txt rendering.</summary>
internal class PXTextRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXTextElement pxTextElement = (PXTextElement) elem;
    resultTxt.Append(HttpUtility.HtmlDecode(pxTextElement.Value));
  }
}
