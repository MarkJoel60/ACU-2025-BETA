// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDeleteButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Delete</b>
/// button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;CARecon&gt; delete;
/// 
/// [PXDeleteButton]
/// [PXUIField]
/// protected virtual IEnumerable Delete(PXAdapter a) { ... }
/// </code>
/// </example>
public class PXDeleteButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute and sets the following properties of the <tt>PXButton</tt> attribute:
  /// <list type="bullet">
  /// <item><description><tt>ClosePopup</tt> to <tt>true</tt></description></item>
  /// <item><description>
  /// <tt>ConfirmationType</tt> to <tt>PXConfirmationType.Always</tt></description></item>
  /// <item><description>
  /// <i>Ctrl + .</i> as the keyboard shortcut</description></item>
  /// </list>
  /// Also the constructor sets the image, the tooltip, and the confirmation message.
  /// </summary>
  public PXDeleteButtonAttribute()
  {
    this.ImageKey = "Remove";
    this.Tooltip = "Delete (Ctrl+Del).";
    this.ConfirmationType = PXConfirmationType.Always;
    this.ConfirmationMessage = "The current record will be deleted.";
    this.ShortcutChar = '.';
    this.ShortcutCtrl = true;
    this.ClosePopup = true;
    this.PopupVisible = true;
    this.SpecialType = PXSpecialButtonType.Delete;
    this.IsLockedOnToolbar = true;
  }
}
