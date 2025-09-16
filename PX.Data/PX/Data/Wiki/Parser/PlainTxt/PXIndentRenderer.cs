// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXIndentRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

/// <summary>Represents a class for PXIndentElement txt rendering.</summary>
internal class PXIndentRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    PXIndentContainer container = (PXIndentContainer) elem;
    int num = 1;
    foreach (PXElement child1 in container.Children)
    {
      string linePrefix = resultTxt.LinePrefix;
      switch (child1)
      {
        case PXIndentContainer _:
          resultTxt.LinePrefix += "  ";
          this.DoRender(child1, resultTxt);
          resultTxt.LinePrefix = linePrefix;
          break;
        case PXIndentElement _:
          string str = this.FormatNewItem(container, resultTxt, num);
          if (resultTxt.Result.Length != 0 && !resultTxt.Result.EndsWith(Environment.NewLine))
            resultTxt.Append(Environment.NewLine);
          resultTxt.Append(str);
          resultTxt.Append(" ");
          foreach (PXElement child2 in ((PXContainerElement) child1).Children)
            this.DoRender(child2, resultTxt);
          resultTxt.LinePrefix = linePrefix;
          ++num;
          break;
      }
    }
  }

  private string FormatNewItem(PXIndentContainer container, PXTxtRenderContext resultTxt, int num)
  {
    string str = "";
    switch (container.Type)
    {
      case '#':
        str = num.ToString() + ".";
        break;
      case '*':
        str = "";
        break;
      case ':':
      case ';':
        str = "";
        break;
    }
    return str;
  }
}
