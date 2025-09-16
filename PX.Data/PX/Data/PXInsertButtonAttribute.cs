// Decompiled with JetBrains decompiler
// Type: PX.Data.PXInsertButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Add New
/// Record</b> button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;INPIHeader&gt; Insert;
/// 
/// [PXInsertButton]
/// protected virtual IEnumerable insert(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXInsertButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute, setting the properties of the <tt>PXButton</tt> attribute as follows:
  /// <list type="bullet">
  /// <item><description>PopupVisible to <tt>false</tt>;</description></item>
  /// <item><description>
  /// <i>Ctrl + -</i> as the keyboard shortcut.</description></item>
  /// </list>
  /// Also sets the image, the tooltip, and the confirmation message.
  /// </summary>
  public PXInsertButtonAttribute()
  {
    this.ImageKey = "AddNew";
    this.Tooltip = "Add New Record (Ctrl+Ins)";
    this.ConfirmationMessage = "Any unsaved changes will be discarded.";
    this.ShortcutChar = '-';
    this.ShortcutCtrl = true;
    this.PopupVisible = true;
    this.SpecialType = PXSpecialButtonType.Insert;
    this.IsLockedOnToolbar = true;
    this.IgnoresArchiveDisabling = true;
  }
}
