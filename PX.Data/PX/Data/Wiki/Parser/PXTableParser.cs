// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXTableParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXTableParser : PXBlockParser
{
  protected override void DoParse(PXBlockParser.ParseContext context, WikiArticle result)
  {
    string props1 = "";
    if (context.StartIndex != 2 && context.WikiText[context.StartIndex - 3] != '\n' && !(result.Current is PXIndentElement))
    {
      this.AddText("{|", context, result);
    }
    else
    {
      for (; context.StartIndex < context.WikiText.Length && context.WikiText[context.StartIndex] != '\r' && context.WikiText[context.StartIndex] != '\n'; ++context.StartIndex)
        props1 += context.WikiText[context.StartIndex].ToString();
      string props2 = this.SanitizeAttributes(props1, context.Settings);
      string nextTableRow = this.GetNextTableRow(context);
      PXTableElement elem = new PXTableElement(props2);
      elem.Attributes = this.ParseProps(props2, context);
      result.AddElement((PXElement) elem);
      string str;
      if (nextTableRow.Length > 2 && nextTableRow.Substring(0, 2) == "|+")
      {
        elem.Caption = nextTableRow.Substring(2);
        str = this.GetNextTableRow(context);
      }
      else
      {
        if (nextTableRow == "|}")
          return;
        str = nextTableRow;
      }
      PXTableRow row = new PXTableRow();
      elem.AddRow(row);
      if (str.Length > 2 && str.Substring(0, 2) == "|-")
      {
        row.Props = this.SanitizeAttributes(str.Substring(2), context.Settings);
        row.Attributes = this.ParseProps(row.Props, context);
        str = this.GetNextTableRow(context);
      }
      while (str != "|}" && context.StartIndex < context.WikiText.Length)
      {
        if (string.IsNullOrEmpty(str) || str.Length < 2 || str[0] != '|' && str[0] != '!')
          str = this.GetNextTableRow(context);
        else if (str.Length >= 2 && str.Substring(0, 2) == "|-")
        {
          row = new PXTableRow(str.Substring(2));
          if (str.Length > 2)
          {
            row.Props = this.SanitizeAttributes(str.Substring(2), context.Settings);
            row.Attributes = this.ParseProps(row.Props, context);
          }
          elem.AddRow(row);
          str = this.GetNextTableRow(context);
        }
        else
        {
          if (str[0] == '|')
            this.ParseCells(str.Substring(1), context, row, false);
          else
            this.ParseCells(str.Substring(1), context, row, true);
          str = this.GetNextTableRow(context);
        }
      }
    }
  }

  private void ParseCells(
    string line,
    PXBlockParser.ParseContext context,
    PXTableRow row,
    bool isHeaderCells)
  {
    string separator1 = isHeaderCells ? " !! " : " || ";
    string separator2 = isHeaderCells ? " ! " : " | ";
    string[] strArray1 = this.SplitLine(line, separator1);
    row.IsSingleLine = strArray1.Length > 1;
    foreach (string str in strArray1)
    {
      string[] strArray2 = this.SplitLine(str, separator2);
      PXTableCell pxTableCell = new PXTableCell(isHeaderCells);
      row.AddCell(pxTableCell);
      WikiArticle result = new WikiArticle();
      string cellFirstLine;
      if (strArray2.Length > 1 && !strArray2[0].Trim().StartsWith("["))
      {
        pxTableCell.Props = this.SanitizeAttributes(strArray2[0], context.Settings);
        pxTableCell.Attributes = this.ParseProps(pxTableCell.Props, context);
        cellFirstLine = this.ParseCellFirstLine(str.Substring(strArray2[0].Length + separator2.Length), pxTableCell, context.Settings);
      }
      else
        cellFirstLine = this.ParseCellFirstLine(str, pxTableCell, context.Settings);
      base.DoParse(new PXBlockParser.ParseContext(cellFirstLine, 0, context.Settings), result);
      pxTableCell.AddChildren(result.GetAllElements());
    }
  }

  private string[] SplitLine(string line, string separator)
  {
    int num = line.IndexOf(Environment.NewLine);
    string str;
    if (num == -1 || num + Environment.NewLine.Length == line.Length)
    {
      str = line;
    }
    else
    {
      if (num == 0)
        return new string[1]{ line };
      str = line.Substring(0, num);
    }
    string[] strArray = str.Split(new string[1]{ separator }, StringSplitOptions.None);
    if (num != -1)
    {
      // ISSUE: explicit reference operation
      ^ref strArray[strArray.Length - 1] += line.Substring(num);
    }
    return strArray;
  }

  private string GetNextTableRow(PXBlockParser.ParseContext context)
  {
    StringBuilder stringBuilder = new StringBuilder();
    while (context.StartIndex < context.WikiText.Length && (context.WikiText[context.StartIndex] == '\r' || context.WikiText[context.StartIndex] == '\n'))
      ++context.StartIndex;
    while (context.StartIndex < context.WikiText.Length && context.WikiText[context.StartIndex] != '|' && context.WikiText[context.StartIndex] != '!')
      stringBuilder.Append(context.WikiText[context.StartIndex++]);
    if (stringBuilder.Length != 0 || context.StartIndex >= context.WikiText.Length)
      return "|";
    stringBuilder.Append(context.WikiText[context.StartIndex]);
    ++context.StartIndex;
    while (context.StartIndex < context.WikiText.Length)
    {
      char ch1 = context.WikiText[context.StartIndex];
      char ch2 = context.StartIndex == 0 ? context.WikiText[context.StartIndex] : context.WikiText[context.StartIndex - 1];
      if (ch1 != '|' && ch1 != '!' || ch2 != '\n')
      {
        stringBuilder.Append(ch1);
        ++context.StartIndex;
        if (stringBuilder.ToString() == "|}")
          break;
      }
      else
        break;
    }
    return stringBuilder.ToString().Trim('\t', '\r', '\n');
  }

  private string ParseCellFirstLine(string cell, PXTableCell elem, PXWikiParserContext settings)
  {
    string cellFirstLine1 = cell;
    int num1 = cell.Length;
    string str = Environment.NewLine + Environment.NewLine;
    foreach (TokenLexems reservedLexem in PXLexemsTable.ReservedLexems)
    {
      string tkLexem = reservedLexem.tkLexem;
      int num2 = cell.IndexOf(tkLexem);
      if (num2 > -1 && num2 < num1)
        num1 = num2;
    }
    if (string.IsNullOrEmpty(cell))
      return cellFirstLine1;
    int length = cell.IndexOf(str);
    string cellFirstLine2;
    if (length == -1 || length + str.Length >= cell.Length || num1 < length)
    {
      cellFirstLine2 = "";
    }
    else
    {
      cellFirstLine2 = cell.Substring(length + str.Length);
      cell = cell.Substring(0, length);
    }
    PXBlockParser.ParseContext context = new PXBlockParser.ParseContext(cell, 0, settings);
    context.AllowParagraph = false;
    WikiArticle result = new WikiArticle();
    base.DoParse(context, result);
    elem.AddChildren(result.GetAllElements());
    return cellFirstLine2;
  }

  private Dictionary<string, string> ParseProps(string props, PXBlockParser.ParseContext context)
  {
    Dictionary<string, string> props1 = new Dictionary<string, string>();
    if (string.IsNullOrWhiteSpace(props) || !this.allowedBlocks.ContainsKey(Token.htmlstart))
      return props1;
    PXBlockParser.ParseContext context1 = new PXBlockParser.ParseContext(props, 0, context.Settings);
    foreach (PXHtmlAttribute readTagAttribute in ((PXHtmlParser) this.allowedBlocks[Token.htmlstart]).ReadTagAttributes(context1))
      props1[readTagAttribute.name.ToLower()] = readTagAttribute.value;
    return props1;
  }
}
