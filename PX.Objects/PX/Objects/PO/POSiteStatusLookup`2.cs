// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POSiteStatusLookup`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO;

[Obsolete("This class is obsolete. Use POOrderSiteStatusLookupExt instead.")]
public class POSiteStatusLookup<Status, StatusFilter> : INSiteStatusLookup<Status, StatusFilter>
  where Status : class, IBqlTable, new()
  where StatusFilter : INSiteStatusFilter, new()
{
  public POSiteStatusLookup(PXGraph graph)
    : base(graph)
  {
  }

  public POSiteStatusLookup(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override PXView CreateIntView(PXGraph graph)
  {
    PXCache cach1 = graph.Caches[typeof (Status)];
    PXCache cach2 = graph.Caches[typeof (StatusFilter)];
    List<Type> typeList = new List<Type>();
    if (cach2.Current == null || !((StatusFilter) cach2.Current).OnlyAvailable.GetValueOrDefault())
      return base.CreateIntView(graph);
    typeList.Add(typeof (Select2<,,>));
    typeList.Add(typeof (Status));
    typeList.Add(typeof (InnerJoin<POVendorInventoryOnly, On<POVendorInventoryOnly.inventoryID, Equal<POSiteStatusSelected.inventoryID>, And<POVendorInventoryOnly.vendorID, Equal<Current<POSiteStatusFilter.vendorID>>, And<Where<POVendorInventoryOnly.subItemID, Equal<POSiteStatusSelected.subItemID>, Or<POSiteStatusSelected.subItemID, IsNull>>>>>>));
    typeList.Add(INSiteStatusLookup<Status, StatusFilter>.CreateWhere(graph));
    Type type = BqlCommand.Compose(typeList.ToArray());
    return (PXView) new INSiteStatusLookup<Status, StatusFilter>.LookupView(graph, BqlCommand.CreateInstance(new Type[1]
    {
      type
    }));
  }
}
