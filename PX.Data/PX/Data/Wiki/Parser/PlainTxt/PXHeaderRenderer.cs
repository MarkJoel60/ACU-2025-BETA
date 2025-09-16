// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXHeaderRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

/// <summary>Represents a class for PXHeaderElement txt rendering.</summary>
internal class PXHeaderRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    string str = this.GetValue((PXHeaderElement) elem, resultTxt);
    resultTxt.Append(str);
    resultTxt.Append(Environment.NewLine);
  }

  private string GetValue(PXHeaderElement e, PXTxtRenderContext resultContext)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (PXElement child in e.Children)
    {
      switch (child)
      {
        case PXTextElement _:
          stringBuilder.Append(((PXTextElement) child).Value);
          break;
        case PXLinkElement _:
          PXTxtRenderContext resultTxt = new PXTxtRenderContext();
          resultTxt.Settings = resultContext.Settings;
          this.DoRender(child, resultTxt);
          stringBuilder.Append(resultTxt.Result);
          break;
      }
    }
    return stringBuilder.ToString();
  }
}
