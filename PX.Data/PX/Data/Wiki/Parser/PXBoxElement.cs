// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXBoxElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a gray box in memory.</summary>
internal class PXBoxElement : PXContainerElement
{
  private BoxType type;

  /// <summary>
  /// Gets or sets value indicating whether this is a box with information icon.
  /// </summary>
  public bool IsHintBox
  {
    get => this.type == BoxType.Hint;
    set => this.type = value ? BoxType.Hint : BoxType.Warn;
  }

  /// <summary>
  /// Gets or sets value indicating whether this is a box with warning icon.
  /// </summary>
  public bool IsWarnBox
  {
    get => this.type == BoxType.Warn;
    set => this.type = value ? BoxType.Warn : BoxType.Hint;
  }

  /// <summary>
  /// Gets or sets value indicating whether this is a box with danger icon.
  /// </summary>
  public bool IsDangerBox
  {
    get => this.type == BoxType.Danger;
    set => this.type = value ? BoxType.Danger : BoxType.Hint;
  }

  /// <summary>
  /// Gets or sets value indicating whether this is a box with good practice icon.
  /// </summary>
  public bool IsGoodPracticeBox
  {
    get => this.type == BoxType.GoodPractice;
    set => this.type = value ? BoxType.GoodPractice : BoxType.Hint;
  }
}
