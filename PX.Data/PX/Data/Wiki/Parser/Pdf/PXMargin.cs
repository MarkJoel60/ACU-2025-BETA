// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Pdf.PXMargin
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Pdf;

public class PXMargin
{
  private int y;
  private int width;
  private int height;
  private ElementFloat floating;

  public PXMargin(int y, int width, int height, ElementFloat floating)
  {
    this.y = y;
    this.width = width;
    this.height = height;
    this.floating = floating;
  }

  public int Y => this.y;

  public int Width => this.width;

  public int Height => this.height;

  public ElementFloat Float => this.floating;

  public bool Contains(int y) => y >= this.Y && y <= this.Y + this.Height;
}
