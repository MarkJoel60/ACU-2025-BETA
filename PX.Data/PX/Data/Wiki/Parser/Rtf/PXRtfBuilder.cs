// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXRtfBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Drawing;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXRtfBuilder
{
  private PXDocument resultRtf = new PXDocument();
  private int currentIndent;
  private int currentTableLevel;
  public PXWikiParserContext Settings;

  public PXRtfBuilder()
  {
  }

  public PXRtfBuilder(PXDocumentSettings settings) => this.resultRtf = new PXDocument(settings);

  public PXDocument Document => this.resultRtf;

  public string Result => this.resultRtf.ToString();

  public int CurrentIndent
  {
    get => this.currentIndent;
    set => this.currentIndent = value;
  }

  public int CurrentTableLevel
  {
    get => this.currentTableLevel;
    set => this.currentTableLevel = value;
  }

  public void AddString(string s) => this.AddString(s, false);

  public void AddString(string s, bool preserveNewLines)
  {
    if (string.IsNullOrEmpty(s))
      return;
    string s1 = s.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}").Replace("&nbsp;", " ");
    if (preserveNewLines)
      s1 = s1.Replace(Environment.NewLine, Environment.NewLine + "\\line ");
    this.resultRtf.Content.Append(HttpUtility.HtmlDecode(s1));
  }

  public void AddImage(Image img)
  {
    this.AddRtfElement((PXRtfElement) new PXPicture(this.Document, img));
  }

  public void AddRtfElement(PXRtfElement elem)
  {
    if (elem is PXParagraph && this.CurrentTableLevel > 0)
    {
      this.resultRtf.Content.Append(Environment.NewLine);
      this.resultRtf.Content.Append("\\intbl\\itap");
      this.resultRtf.Content.Append(this.CurrentTableLevel);
    }
    this.resultRtf.Content.Append(Environment.NewLine);
    elem.Render();
    this.resultRtf.Content.Append(Environment.NewLine);
  }

  public void Paragraph() => this.AddRtfElement((PXRtfElement) new PXParagraph(this.Document));

  public void NewLine()
  {
    this.resultRtf.Content.Append(Environment.NewLine);
    this.resultRtf.Content.AppendLine("\\line");
  }

  public void PageBreak() => this.resultRtf.Content.AppendLine("\\page");

  public void InTable()
  {
    this.resultRtf.Content.Append(Environment.NewLine);
    this.resultRtf.Content.Append("\\intbl\\itap");
    this.resultRtf.Content.AppendLine(this.CurrentTableLevel.ToString());
  }

  public void SetTextAlign(TextAlign align)
  {
    this.resultRtf.Content.Append("\\");
    this.resultRtf.Content.AppendLine(PXParagraph.DecryptAlign(align));
  }

  public void SetTextColor(Color color)
  {
    int colorCode = this.Document.GetColorCode(color);
    this.resultRtf.Content.Append("\\cf");
    this.resultRtf.Content.AppendLine(colorCode.ToString());
  }

  public void DisableColor() => this.resultRtf.Content.AppendLine("\\cf0");

  public void SetHighlightColor(Color color)
  {
    int colorCode = this.Document.GetColorCode(color);
    this.resultRtf.Content.Append("\\highlight");
    this.resultRtf.Content.AppendLine(colorCode.ToString());
  }

  public void DisableHighlight() => this.resultRtf.Content.AppendLine("\\highlight0");

  public void SetTextFont(string fontName)
  {
    int fontCode = this.Document.GetFontCode(fontName);
    this.resultRtf.Content.Append("\\f");
    this.resultRtf.Content.AppendLine(fontCode.ToString());
  }

  public void DisableTextFont() => this.resultRtf.Content.AppendLine("\\f0");

  public void SetTextStyle(TextStyle style)
  {
    switch (style)
    {
      case TextStyle.Bold:
        this.resultRtf.Content.Append("\\b ");
        break;
      case TextStyle.Italic:
        this.resultRtf.Content.Append("\\i ");
        break;
      case TextStyle.Bold | TextStyle.Italic:
        this.resultRtf.Content.Append("\\b\\i ");
        break;
      case TextStyle.Underlined:
        this.resultRtf.Content.Append("\\ul ");
        break;
      case TextStyle.Striked:
        this.resultRtf.Content.Append("\\strike ");
        break;
      case TextStyle.Monotype:
        this.SetTextFont("Courier New");
        break;
      case TextStyle.Subscript:
        this.resultRtf.Content.Append("\\sub ");
        break;
      case TextStyle.Superscript:
        this.resultRtf.Content.Append("\\super ");
        break;
    }
  }

  public void DisableTextStyle(TextStyle style)
  {
    switch (style)
    {
      case TextStyle.Bold:
        this.resultRtf.Content.Append("\\b0 ");
        break;
      case TextStyle.Italic:
        this.resultRtf.Content.Append("\\i0 ");
        break;
      case TextStyle.Bold | TextStyle.Italic:
        this.resultRtf.Content.Append("\\i0\\b0 ");
        break;
      case TextStyle.Underlined:
        this.resultRtf.Content.Append("\\ul0 ");
        break;
      case TextStyle.Striked:
        this.resultRtf.Content.Append("\\strike0 ");
        break;
      case TextStyle.Monotype:
        this.DisableTextFont();
        break;
      case TextStyle.Subscript:
      case TextStyle.Superscript:
        this.resultRtf.Content.Append("\\nosupersub ");
        break;
    }
  }

  public void SetTextSize(int ptSize)
  {
    this.resultRtf.Content.Append(Environment.NewLine);
    this.resultRtf.Content.Append("\\fs");
    this.resultRtf.Content.AppendLine((ptSize * 2).ToString());
  }
}
