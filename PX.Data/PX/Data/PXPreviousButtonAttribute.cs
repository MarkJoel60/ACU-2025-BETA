// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPreviousButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Go to Previous
/// Record</b> button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;APDocumentFilter&gt; previousPeriod;
/// 
/// [PXUIField(DisplayName = "Prev",
///            MapEnableRights = PXCacheRights.Select,
///            MapViewRights = PXCacheRights.Select)]
/// [PXPreviousButton]
/// public virtual IEnumerable PreviousPeriod(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXPreviousButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute and sets the following properties of the <tt>PXButton</tt> attribute:
  /// <list type="bullet">
  /// <item><description><tt>PopupVisible</tt> to <tt>false</tt></description></item>
  /// <item><description>
  /// <i>!</i> as the keyboard shortcut</description></item>
  /// </list>
  /// Also sets the image, the tooltip, and the confirmation message.
  /// </summary>
  public PXPreviousButtonAttribute()
  {
    this.ImageKey = "PagePrev";
    this.Tooltip = "Go to Previous Record (PgUp)";
    this.ConfirmationMessage = "Any unsaved changes will be discarded.";
    this.ShortcutChar = '!';
    this.PopupVisible = true;
    this.SpecialType = PXSpecialButtonType.Prev;
    this.IsLockedOnToolbar = true;
    this.IgnoresArchiveDisabling = true;
  }
}
