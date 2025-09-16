// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXRowSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

public class PXRowSettings
{
  /// <summary>
  /// Half the space between the cells of a table row in twips.
  /// </summary>
  public int CellSpacing = 108;
  /// <summary>
  /// Height of a table row in twips.
  /// When 0, the height is sufficient for all the text in the line;
  /// when positive, the height is guaranteed to be at least the specified height;
  /// when negative, the absolute value of the height is used, regardless of the height of the text in the line.
  /// </summary>
  public int MinRowHeight = 280;
  /// <summary>
  /// Position in twips of the leftmost edge of the table with respect to the left edge of its column.
  /// </summary>
  public int Offset = 36;
  /// <summary>
  /// Keep table row together. This row cannot be split by a page break.
  /// </summary>
  public bool KeepRow;
  /// <summary>Keep row in the same page as the following row.</summary>
  public bool KeepFollowingRow;
  public TextAlign Align;
  public int paddingLeft;
  public int paddingTop;
  public int paddingRight;
  public int paddingBottom;
}
