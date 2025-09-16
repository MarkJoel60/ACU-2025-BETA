// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNextButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Go to Next
/// Record</b> button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;APDocumentFilter&gt; nextPeriod;
/// 
/// [PXUIField(DisplayName = "Next",
///            MapEnableRights = PXCacheRights.Select,
///            MapViewRights = PXCacheRights.Select)]
/// [PXNextButton]
/// public virtual IEnumerable NextPeriod(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXNextButtonAttribute : PXButtonAttribute
{
  /// <summary>
  ///   <para>Creates an instance of the attribute and sets the following properties of the <tt>PXButton</tt> attribute:</para>
  ///   <list type="bullet">
  ///     <item><description>PopupVisible to <tt>false</tt></description></item>
  ///     <item><description>
  ///       <i>"</i> as the keyboard shortcut</description></item>
  ///   </list>
  ///   <para>Also the constructor sets the image, the tooltip, and the confirmation message.</para>
  /// </summary>
  public PXNextButtonAttribute()
  {
    this.ImageKey = "PageNext";
    this.Tooltip = "Go to Next Record (PgDn)";
    this.ConfirmationMessage = "Any unsaved changes will be discarded.";
    this.ShortcutChar = '"';
    this.PopupVisible = true;
    this.SpecialType = PXSpecialButtonType.Next;
    this.IsLockedOnToolbar = true;
    this.IgnoresArchiveDisabling = true;
  }
}
