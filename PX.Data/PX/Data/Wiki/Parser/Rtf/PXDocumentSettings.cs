// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXDocumentSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXDocumentSettings
{
  private List<string> fontTable = new List<string>();
  private List<Color> colorTable = new List<Color>();
  private List<PXListDefinition> listsTable = new List<PXListDefinition>();
  public readonly int PageWidth = 11907;
  public readonly int PageHeight = 16840;
  public readonly int MarginTop = 1440;
  public readonly int MarginBottom = 1440;
  public readonly int MarginLeft = 1700;
  public readonly int MarginRight = 850;
  public readonly int FontSize = 8;

  public List<string> FontTable => this.fontTable;

  public List<Color> ColorTable => this.colorTable;

  public List<PXListDefinition> ListsTable => this.listsTable;
}
