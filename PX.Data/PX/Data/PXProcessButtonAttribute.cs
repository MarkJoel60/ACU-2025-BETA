// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of buttons that are used
/// on processing screens.</summary>
/// <example>
/// <code>
/// public PXAction&lt;APInvoice&gt; createSchedule;
/// 
/// [PXUIField(DisplayName = "Assign to Schedule",
///            MapEnableRights = PXCacheRights.Update,
///            MapViewRights = PXCacheRights.Update)]
/// [PXProcessButton(ImageKey = PX.Web.UI.Sprite.Main.Shedule)]
/// public virtual IEnumerable CreateSchedule(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXProcessButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute and sets the <tt>CommitChanges</tt>
  /// property of the <tt>PXButton</tt> attribute to true.
  /// </summary>
  public PXProcessButtonAttribute() => this.CommitChanges = true;
}
