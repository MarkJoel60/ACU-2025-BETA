// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXTxtRenderContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

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
    {
      this.NewLine(true);
    }
    else
    {
      string[] strArray = this.SplitByLines(value);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (index == strArray.Length - 1 && this.AllowJustify && this.resultTxt.ToString().EndsWith(Environment.NewLine + this.LinePrefix))
          strArray[index] = strArray[index].TrimStart();
        if (strArray[index] != string.Empty)
          this.resultTxt.Append(strArray[index]);
        if (index < strArray.Length - 1)
          this.NewLine(true);
      }
    }
  }

  /// <summary>Safely adds a new line to the resulting text.</summary>
  public void NewLine() => this.NewLine(false);

  /// <summary>
  /// Splits given value by lines in such order that lines legth is less or equal to ColsCount property value.
  /// This method also determines left and right margins.
  /// </summary>
  /// <param name="value">Value to split.</param>
  /// <returns>An array of lines.</returns>
  public string[] SplitByLines(string value)
  {
    value = this.GetSpacedValue(value);
    int width1 = this.ColsCount - this.LinePrefix.Length - this.LineSuffix.Length;
    int num = this.resultTxt.ToString().LastIndexOf(Environment.NewLine) + Environment.NewLine.Length;
    int width2 = width1 - (this.resultTxt.Length - num - this.LinePrefix.Length);
    if (width2 <= 0)
    {
      this.NewLine();
      width2 = width1;
    }
    if (this.ColsCount == 0 || value.Length <= width2)
      return new string[1]{ value };
    string result;
    bool flag = this.SplitSingle(ref value, width2, out result);
    if (value.Length <= width1 & flag)
      return new string[2]{ result, value };
    if (value.Length <= width1)
      return new string[2]{ "", value };
    List<string> stringList = new List<string>();
    if (flag)
      stringList.Add(result);
    else
      stringList.Add("");
    string str = value;
    string[] separator = new string[1]
    {
      Environment.NewLine
    };
    foreach (string v in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      foreach (string splitByLine in this.SplitByLines(v, width1))
        stringList.Add(splitByLine);
    }
    return stringList.ToArray();
  }

  /// <summary>
  /// Fills current line with spaces and adds right container border if needed.
  /// </summary>
  public void CompleteCurrentLine()
  {
    int num1 = this.ColsCount - this.LinePrefix.Length - this.LineSuffix.Length;
    int num2 = this.resultTxt.ToString().LastIndexOf(Environment.NewLine) + Environment.NewLine.Length;
    int valuelen = this.resultTxt.Length - num2 - this.LinePrefix.Length;
    if (num2 == 1 || valuelen < 0 || valuelen > num1)
      return;
    this.FillLine(valuelen);
    this.resultTxt.Append(this.LineSuffix);
  }

  private string[] SplitByLines(string v, int width)
  {
    List<string> stringList = new List<string>();
    string result;
    do
    {
      this.SplitSingle(ref v, width, out result);
      stringList.Add(result);
    }
    while (!(result == v));
    return stringList.ToArray();
  }

  private bool SplitSingle(ref string v, int width, out string result)
  {
    result = v;
    if (width >= v.Length)
    {
      result = v.Substring(0);
    }
    else
    {
      int num = this.LocateNearestWhitespace(v, width);
      if (num == width && !char.IsWhiteSpace(v[num]))
        return false;
      result = v.Substring(0, num);
      int startIndex = this.SkipWhitespace(v, num);
      v = startIndex < v.Length ? v.Substring(startIndex) : v;
    }
    return true;
  }

  private string GetSpacedValue(string value)
  {
    if (this.charsSpace == 0)
      return value;
    StringBuilder stringBuilder = new StringBuilder(value.Length);
    foreach (char ch in value)
    {
      stringBuilder.Append(ch);
      if (ch != '\n' && ch != '\r')
      {
        for (int index = 0; index < this.CharsSpace; ++index)
          stringBuilder.Append(' ');
      }
    }
    return stringBuilder.ToString();
  }

  private void NewLine(bool shouldJustify)
  {
    if (this.ColsCount == 0)
    {
      this.resultTxt.Append(this.LineSuffix);
      this.resultTxt.Append(Environment.NewLine);
      this.resultTxt.Append(this.LinePrefix);
    }
    else
    {
      if (shouldJustify && this.AllowJustify)
        this.FormatLine();
      this.CompleteCurrentLine();
      this.resultTxt.Append(Environment.NewLine);
      this.resultTxt.Append(this.LinePrefix);
    }
  }

  private void FillLine(int valuelen)
  {
    if (this.ColsCount == 0 || !this.FillEachLine)
      return;
    for (int index = 0; index < this.ColsCount - this.LinePrefix.Length - this.LineSuffix.Length - valuelen; ++index)
      this.resultTxt.Append(' ');
  }

  private int LocateNearestWhitespace(string v, int index)
  {
    int num = index;
    while (index >= 0 && !char.IsWhiteSpace(v[index]))
      --index;
    if (index <= 0)
      return num;
    while (index >= 0 && char.IsWhiteSpace(v[index]))
      --index;
    return index <= 0 || index + 1 == v.Length ? num : index + 1;
  }

  private int SkipWhitespace(string v, int index)
  {
    int num = index;
    while (index < v.Length && char.IsWhiteSpace(v[index]))
      ++index;
    return index == v.Length ? num : index;
  }

  private void FormatLine()
  {
    int start;
    int count;
    string str = this.FormatLineJustify(this.GetLastLine(out start, out count));
    this.resultTxt = this.resultTxt.Remove(start, count);
    this.resultTxt = this.resultTxt.Insert(start, str);
  }

  private void TrimLine()
  {
    int start;
    int count;
    string lastLine = this.GetLastLine(out start, out count);
    if (lastLine.Length >= this.ColsCount - this.LinePrefix.Length)
      return;
    string str = lastLine.Trim();
    this.resultTxt = this.resultTxt.Remove(start, count);
    this.resultTxt = this.resultTxt.Insert(start, str);
  }

  private string GetLastLine(out int start, out int count)
  {
    string str = this.resultTxt.ToString();
    int num = str.LastIndexOf(Environment.NewLine) + Environment.NewLine.Length;
    int startIndex = (num == 1 ? 0 : num) + this.LinePrefix.Length;
    start = startIndex;
    count = this.ColsCount - this.LinePrefix.Length;
    count = startIndex + count >= str.Length ? str.Length - startIndex : count;
    return str.Substring(startIndex, count);
  }

  private string FormatLineJustify(string line)
  {
    line = line.Trim();
    int num1 = this.ColsCount - this.LinePrefix.Length - this.LineSuffix.Length - line.Length;
    if (num1 <= 0 || line.Length == 0)
      return line;
    string[] strArray = Str.SplitByWords(line);
    int num2 = num1 < strArray.Length ? num1 : strArray.Length;
    int num3 = num1 / num2;
    int num4 = num1 % num2;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index1 = 0; index1 < num2; ++index1)
    {
      stringBuilder.Append(strArray[index1]);
      for (int index2 = 0; index2 < num3; ++index2)
        stringBuilder.Append(' ');
      if (num4 != 0)
      {
        stringBuilder.Append(' ');
        --num4;
      }
    }
    for (int index = num2; index < strArray.Length; ++index)
      stringBuilder.Append(strArray[index]);
    return stringBuilder.ToString().TrimEnd();
  }
}
