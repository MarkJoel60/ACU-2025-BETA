// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXStyledTextElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents styled text in memory.</summary>
internal class PXStyledTextElement : PXContainerElement
{
  protected TextStyle style;

  /// <summary>
  /// Initializes a new instance of PXStyledTextElement class.
  /// </summary>
  /// <param name="style"></param>
  public PXStyledTextElement(TextStyle style) => this.style = style;

  /// <summary>Gets a TextStyle value for this styled text.</summary>
  public TextStyle Style => this.style;
}
