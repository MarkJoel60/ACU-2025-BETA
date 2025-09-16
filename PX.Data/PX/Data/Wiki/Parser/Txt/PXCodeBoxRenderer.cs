// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXCodeBoxRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>
/// Represents a class for rendering PXCodeBoxElement to txt.
/// </summary>
internal class PXCodeBoxRenderer : PXBoxRenderer
{
  protected override string GetCaption(PXElement elem) => "Code Box";

  protected override void RenderInner(PXElement box, PXTxtRenderContext resultTxt)
  {
    bool allowJustify = resultTxt.AllowJustify;
    resultTxt.AllowJustify = false;
    foreach (PXElement child in ((PXContainerElement) box).Children)
    {
      if (child is PXTextElement)
      {
        string[] strArray = ((PXTextElement) child).Value.TrimStart().Split(new string[1]
        {
          Environment.NewLine
        }, StringSplitOptions.RemoveEmptyEntries);
        for (int index = 0; index < strArray.Length; ++index)
        {
          resultTxt.Append(HttpUtility.HtmlDecode(strArray[index].Replace("\t", "     ")));
          if (index != strArray.Length - 1)
            resultTxt.NewLine();
        }
      }
    }
    resultTxt.AllowJustify = allowJustify;
  }
}
