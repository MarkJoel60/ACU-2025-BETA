// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PlainTxt.PXTxtRenderContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.PlainTxt;

public class PXTxtRenderContext
{
  private StringBuilder resultTxt = new StringBuilder();
  private int charsSpace;
  private int colsCount;
  private string linePrefix = "";
  private string lineSuffix = "";
  private bool fillEachLine;
  private bool allowJustify = true;
  public PXWikiParserContext Settings;

  /// <summary>Gets resulting text string.</summary>
  public string Result => this.resultTxt.ToString();

  /// <summary>Gets or sets space between two sibling characters.</summary>
  public int CharsSpace
  {
    get => this.charsSpace;
    set => this.charsSpace = value;
  }

  /// <summary>
  /// Gets or sets maximum number of columns in each line of resulting txt document.
  /// </summary>
  public int ColsCount
  {
    get => this.colsCount;
    set => this.colsCount = value;
  }

  /// <summary>
  /// Gets or sets prefix which is displayed in the beginning of each line.
  /// </summary>
  public string LinePrefix
  {
    get => this.linePrefix;
    set => this.linePrefix = value;
  }

  /// <summary>
  /// Gets or sets suffix which is displayed in the end of each line.
  /// </summary>
  public string LineSuffix
  {
    get => this.lineSuffix;
    set => this.lineSuffix = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether each line of text will occupy ColsCount space.
  /// If the line is shorter than ColsCount it will be expanded by filling with whitespaces.
  /// </summary>
  public bool FillEachLine
  {
    get => this.fillEachLine;
    set => this.fillEachLine = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether added text should be justified.
  /// </summary>
  public bool AllowJustify
  {
    get => this.allowJustify;
    set => this.allowJustify = value;
  }

  /// <summary>Appends given value to text result.</summary>
  /// <param name="value">Value to add.</param>
  public void Append(string value)
  {
    if (string.IsNullOrEmpty(value))
      return;
    if (value == Environment.NewLine)
      this.resultTxt.Append(Environment.NewLine);
    else
      this.resultTxt.Append(value);
  }
}
