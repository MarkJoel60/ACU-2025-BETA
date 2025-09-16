// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXParagraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXParagraph(PXDocument document) : PXRtfElement(document)
{
  private List<PXRtfElement> children = new List<PXRtfElement>();
  public int SpaceAfter = 120;
  public int LineIndent;
  public TextAlign Align;
  public PXBorderSettings left = new PXBorderSettings();
  public PXBorderSettings top = new PXBorderSettings();
  public PXBorderSettings right = new PXBorderSettings();
  public PXBorderSettings bottom = new PXBorderSettings();
  public int TableLevel;
  public bool EndsWithNewLine = true;

  public List<PXRtfElement> Children => this.children;

  public override void Render(StringBuilder result)
  {
    if (this.TableLevel > 1)
    {
      result.Append("\\intbl\\itap");
      result.Append(this.TableLevel);
    }
    result.Append("\\sa");
    result.Append(this.SpaceAfter);
    result.Append("\\li");
    result.Append(this.LineIndent);
    result.Append(Environment.NewLine);
    result.Append("\\");
    result.AppendLine(PXParagraph.DecryptAlign(this.Align));
    if (this.left != null && this.left.type != BorderType.None)
    {
      result.Append("\\brdrl");
      result.Append(this.left.ToString());
      if (this.left.color.HasValue)
      {
        result.Append("\\brdrcf");
        result.AppendLine(this.document.GetColorCode(this.left.color.Value).ToString());
      }
    }
    if (this.top != null && this.top.type != BorderType.None)
    {
      result.Append("\\brdrt");
      result.Append(this.top.ToString());
      if (this.top.color.HasValue)
      {
        result.Append("\\brdrcf");
        result.AppendLine(this.document.GetColorCode(this.top.color.Value).ToString());
      }
    }
    if (this.right != null && this.right.type != BorderType.None)
    {
      result.Append("\\brdrr");
      result.Append(this.right.ToString());
      if (this.right.color.HasValue)
      {
        result.Append("\\brdrcf");
        result.AppendLine(this.document.GetColorCode(this.right.color.Value).ToString());
      }
    }
    if (this.bottom != null && this.bottom.type != BorderType.None)
    {
      result.Append("\\brdrb");
      result.Append(this.bottom.ToString());
      if (this.bottom.color.HasValue)
      {
        result.Append("\\brdrcf");
        result.AppendLine(this.document.GetColorCode(this.bottom.color.Value).ToString());
      }
    }
    foreach (PXRtfElement child in this.children)
    {
      child.Render(result);
      result.Append(Environment.NewLine);
    }
    if (!this.EndsWithNewLine)
      return;
    result.Append("\\par");
    result.Append("\\pard");
  }

  internal static string DecryptAlign(TextAlign align)
  {
    switch (align)
    {
      case TextAlign.Left:
        return "ql";
      case TextAlign.Center:
        return "qc";
      case TextAlign.Right:
        return "qr";
      case TextAlign.Justify:
        return "qj";
      default:
        return "";
    }
  }
}
