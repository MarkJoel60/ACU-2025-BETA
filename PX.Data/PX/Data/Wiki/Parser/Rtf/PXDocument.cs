// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXDocument
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXDocument
{
  private StringBuilder rtfContent = new StringBuilder();
  private PXDocumentSettings settings = new PXDocumentSettings();

  public PXDocument()
  {
    this.settings.FontTable.Add("Verdana");
    this.settings.FontTable.Add("Symbol");
    this.settings.ColorTable.Add(Color.White);
  }

  public PXDocument(PXDocumentSettings settings) => this.settings = settings;

  public PXDocumentSettings Settings => this.settings;

  public StringBuilder Content => this.rtfContent;

  /// <summary>Gets page width without its margins.</summary>
  public int ClientWidth
  {
    get => this.Settings.PageWidth - this.Settings.MarginLeft - this.Settings.MarginRight;
  }

  public int GetFontCode(string fontName)
  {
    for (int index = 0; index < this.Settings.FontTable.Count; ++index)
    {
      if (string.Compare(this.Settings.FontTable[index], fontName, true) == 0)
        return index;
    }
    this.Settings.FontTable.Add(fontName);
    return this.Settings.FontTable.Count - 1;
  }

  public int GetColorCode(Color color)
  {
    for (int index = 0; index < this.Settings.ColorTable.Count; ++index)
    {
      if ((int) this.Settings.ColorTable[index].R == (int) color.R && (int) this.Settings.ColorTable[index].G == (int) color.G && (int) this.Settings.ColorTable[index].B == (int) color.B)
        return index + 1;
    }
    this.Settings.ColorTable.Add(color);
    return this.Settings.ColorTable.Count;
  }

  public int GetListCode(int listId, ListNumberType numberType)
  {
    for (int index = 0; index < this.Settings.ListsTable.Count; ++index)
    {
      if (this.Settings.ListsTable[index].ID == (long) listId)
        return index + 1;
    }
    this.Settings.ListsTable.Add(new PXListDefinition(this, numberType, listId));
    return this.Settings.ListsTable.Count;
  }

  public Color ConvertCodeToColor(int code)
  {
    --code;
    return code < 0 || code >= this.Settings.ColorTable.Count ? Color.White : this.Settings.ColorTable[code];
  }

  public override string ToString()
  {
    return $"{this.GetHeader()}{Environment.NewLine}\\fs{(this.Settings.FontSize * 2).ToString()}{Environment.NewLine}{this.Content.ToString()}{Environment.NewLine}}}";
  }

  private string GetHeader()
  {
    return $"{{\\rtf1\\ansi\\ansicpg1251\\deff0\\deflang1049{Environment.NewLine}{this.GetFontsTable()}{Environment.NewLine}{this.GetDimensions()}{Environment.NewLine}{this.GetColorsTable()}{Environment.NewLine}{this.GetListsTable()}";
  }

  private string GetDimensions()
  {
    return $"\\paperw{this.Settings.PageWidth.ToString()}\\paperh{this.Settings.PageHeight.ToString()}\\margl{this.Settings.MarginLeft.ToString()}\\margr{this.Settings.MarginRight.ToString()}\\margt{this.Settings.MarginTop.ToString()}\\margb{this.Settings.MarginBottom.ToString()}";
  }

  private string GetFontsTable()
  {
    StringBuilder stringBuilder = new StringBuilder("{\\fonttbl");
    for (int index = 0; index < this.Settings.FontTable.Count; ++index)
      stringBuilder.AppendFormat("{0}{1}{2}{3}{4}{5}", (object) "{\\f", (object) index, (object) "\\fnil\\fcharset0 ", (object) this.Settings.FontTable[index], (object) ";}", (object) Environment.NewLine);
    stringBuilder.Append("}");
    return stringBuilder.ToString();
  }

  private string GetColorsTable()
  {
    StringBuilder stringBuilder = new StringBuilder("{\\colortbl ;");
    foreach (Color color in this.Settings.ColorTable)
      stringBuilder.AppendFormat("{0}{1}{2}{3}{4}{5}{6}", (object) "\\red", (object) color.R, (object) "\\green", (object) color.G, (object) "\\blue", (object) color.B, (object) ";");
    stringBuilder.Append("}");
    return stringBuilder.ToString();
  }

  private string GetListsTable()
  {
    if (this.settings.ListsTable.Count == 0)
      return "";
    StringBuilder result = new StringBuilder("{\\*\\listtable" + Environment.NewLine);
    List<long> longList = new List<long>();
    foreach (PXListDefinition pxListDefinition in this.settings.ListsTable)
    {
      result.Append(Environment.NewLine);
      pxListDefinition.Render(result);
      longList.Add(pxListDefinition.ID);
    }
    result.AppendFormat("{0}{1}", (object) "}", (object) Environment.NewLine);
    for (int index = 0; index < longList.Count; ++index)
    {
      result.AppendFormat("{0}{1}{2}{3}{4}", (object) "{\\*\\listoverridetable{\\listoverride\\listid", (object) longList[index], (object) "\\listoverridecount9\\ls", (object) (index + 1), (object) "}}");
      result.Append(Environment.NewLine);
    }
    return result.ToString();
  }
}
