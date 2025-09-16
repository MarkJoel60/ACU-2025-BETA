// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCancelButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Cancel</b>
/// button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;CashAccount&gt; cancel;
/// 
/// [PXUIField(DisplayName = ActionsMessages.Cancel, MapEnableRights = PXCacheRights.Select)]
/// [PXCancelButton]
/// protected virtual IEnumerable Cancel(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXCancelButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute, setting the properties of the
  /// <tt>PXButton</tt> attribute: <tt>ClosePopup</tt> to <tt>false</tt>;
  /// <tt>SpecialType</tt> to <tt>PXSpecialButtonType.Cancel</tt>; <tt>ConfirmationType</tt> to
  /// <tt>PXConfirmationType.IfDirty</tt>; <i>Ctrl + -</i> as the keyboard shortcut.
  /// Also sets the image, the tooltip, and the confirmation message.
  /// </summary>
  public PXCancelButtonAttribute()
  {
    this.ImageKey = "Cancel";
    this.Tooltip = "Cancel (Esc)";
    this.ConfirmationType = PXConfirmationType.IfDirty;
    this.ConfirmationMessage = "Any unsaved changes will be discarded.";
    this.ShortcutChar = '\u001B';
    this.SpecialType = PXSpecialButtonType.Cancel;
    this.ClosePopup = false;
    this.CommitChanges = false;
    this.PopupVisible = true;
    this.IgnoresArchiveDisabling = true;
  }
}
