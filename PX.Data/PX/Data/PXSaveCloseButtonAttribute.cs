// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSaveCloseButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Save and
/// Close</b> button.</summary>
public class PXSaveCloseButtonAttribute : PXSaveButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute. In addition to properties that
  /// are set by the base <see cref="T:PX.Data.PXSaveButtonAttribute">PXSaveButton</see>
  /// attribute, extends the keyboard shortcut with <i>Shift</i> ans sets the
  /// different tooltip.
  /// </summary>
  public PXSaveCloseButtonAttribute()
  {
    this.ImageKey = "SaveClose";
    this.ShortcutShift = true;
    this.Tooltip = "Save the current record and close the screen (Ctrl+Shift+S).";
  }
}
