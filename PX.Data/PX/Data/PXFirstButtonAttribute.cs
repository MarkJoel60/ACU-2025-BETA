// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFirstButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Go to First
/// Record</b> button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;CuryRateFilter&gt; first;
/// 
/// [PXFirstButton]
/// [PXUIField]
/// protected virtual IEnumerable First(PXAdapter a) { ... }
/// </code>
/// </example>
public class PXFirstButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute, setting the properties of the <tt>PXButton</tt> attribute:
  /// <list type="bullet">
  /// <item><description>PopupVisible to <tt>false</tt>;</description></item>
  /// <item><description>
  /// <i>Ctrl + !</i> as the keyboard shortcut. A</description></item>
  /// </list>
  /// Also sets the image, the tooltip, and the confirmation message.
  /// </summary>
  public PXFirstButtonAttribute()
  {
    this.ImageKey = "PageFirst";
    this.Tooltip = "Go to First Record";
    this.ConfirmationMessage = "Any unsaved changes will be discarded.";
    this.PopupVisible = true;
    this.SpecialType = PXSpecialButtonType.First;
    this.IsLockedOnToolbar = true;
    this.IgnoresArchiveDisabling = true;
  }
}
