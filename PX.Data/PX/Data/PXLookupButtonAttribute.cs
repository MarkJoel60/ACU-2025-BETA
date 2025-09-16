// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLookupButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Sets up a button with the properties of the lookup
/// button.</summary>
/// <example>
/// <code>
/// public PXAction&lt;APInvoice&gt; newVendor;
/// 
/// [PXUIField(DisplayName = "New Vendor",
///            MapEnableRights = PXCacheRights.Select,
///            MapViewRights = PXCacheRights.Select)]
/// [PXLookupButton]
/// public virtual IEnumerable NewVendor(PXAdapter adapter) { ... }
/// </code>
/// </example>
public class PXLookupButtonAttribute : PXButtonAttribute
{
  /// <summary>
  /// Creates an instance of the attribute, setting the image.
  /// </summary>
  public PXLookupButtonAttribute() => this.ImageKey = "Search";
}
