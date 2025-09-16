// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCancelCloseButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Cancel and
/// Close</b> button.</summary>
public class PXCancelCloseButtonAttribute : PXCancelButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute. In addition to properties
  /// that are set by the base <see cref="T:PX.Data.PXCancelButtonAttribute">PXCancelButton</see>
  /// attribute, sets the different image, tooltip and shortcut.
  /// </summary>
  public PXCancelCloseButtonAttribute()
  {
    this.Tooltip = "Discard Changes and Close";
    this.ImageKey = "CancelClose";
    this.IgnoresArchiveDisabling = true;
  }
}
