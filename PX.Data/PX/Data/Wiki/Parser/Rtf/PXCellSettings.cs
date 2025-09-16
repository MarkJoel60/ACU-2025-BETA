// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXCellSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXCellSettings
{
  public int width;
  public TextAlign align;
  public CellVerticalAlign valign;
  public Color? background;
  public PXBorderSettings left = new PXBorderSettings();
  public PXBorderSettings top = new PXBorderSettings();
  public PXBorderSettings right = new PXBorderSettings();
  public PXBorderSettings bottom = new PXBorderSettings();
}
