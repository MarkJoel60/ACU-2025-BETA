// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSaveButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the <b>Save</b>
/// button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;INPIHeader&gt; save;
/// 
/// [PXSaveButton]
/// protected virtual IEnumerable Save(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXSaveButtonAttribute : PXButtonAttribute
{
  /// <summary>
  ///   <para>Creates an instance of the attribute and sets the following properties of the <tt>PXButton</tt> attribute:</para>
  ///   <list type="bullet">
  ///     <item><description><tt>CommitChanges</tt> to <tt>true</tt></description></item>
  ///     <item><description>
  ///       <tt>SpecialType</tt> to <tt>PXSpecialButtonType.Save</tt></description></item>
  ///     <item><description>
  ///       <i>Ctrl + S</i> as the keyboard shortcut</description></item>
  ///   </list>
  ///   <para>Also the constructor sets the image and the tooltip.</para>
  /// </summary>
  public PXSaveButtonAttribute()
  {
    this.ImageKey = "Save";
    this.Tooltip = "Save (Ctrl+S).";
    this.ShortcutChar = 'S';
    this.ShortcutCtrl = true;
    this.CommitChanges = true;
    this.SpecialType = PXSpecialButtonType.Save;
    this.PopupVisible = true;
    this.IsLockedOnToolbar = true;
  }
}
