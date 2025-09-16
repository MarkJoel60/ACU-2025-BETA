// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXHeaderRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>Represents a class for PXHeaderElement txt rendering.</summary>
internal class PXHeaderRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXHeaderElement e = (PXHeaderElement) elem;
    if (resultTxt.Result.Length != 0 && !resultTxt.Result.EndsWith(Environment.NewLine))
      resultTxt.NewLine();
    if (resultTxt.Result.Length != 0)
      resultTxt.NewLine();
    if (e.IsError)
    {
      this.RenderError(e, resultTxt);
    }
    else
    {
      int charsSpace = resultTxt.CharsSpace;
      int num1 = 0;
      string upper = this.GetValue(e, resultTxt);
      switch (e.Level)
      {
        case SectionLevel.H1:
          upper = upper.ToUpper();
          num1 = resultTxt.CharsSpace = 1;
          break;
        case SectionLevel.H2:
          upper = upper.ToUpper();
          break;
      }
      resultTxt.Append(upper);
      resultTxt.NewLine();
      resultTxt.CharsSpace = charsSpace;
      if (e.Level == SectionLevel.H4)
        return;
      for (int index = 0; index < upper.Length; ++index)
      {
        int num2 = index * (num1 + 1);
        if ((num2 < upper.Length * num1 || resultTxt.ColsCount != 0) && (num2 < resultTxt.ColsCount || resultTxt.ColsCount == 0))
        {
          resultTxt.Append("-");
          if (e.Level == SectionLevel.H1)
            resultTxt.Append("-");
        }
        else
          break;
      }
      resultTxt.NewLine();
    }
  }

  private void RenderError(PXHeaderElement e, PXTxtRenderContext resultTxt)
  {
    resultTxt.Append("Unclosed header: ");
    resultTxt.Append(e.Name);
    resultTxt.Append(", cannot render section!");
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
