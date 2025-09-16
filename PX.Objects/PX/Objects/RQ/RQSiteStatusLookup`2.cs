// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQSiteStatusLookup`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.RQ;

[Obsolete("This class is obsolete. Use RQSiteStatusLookupBaseExt instead.")]
public class RQSiteStatusLookup<Status, StatusFilter> : INSiteStatusLookup<Status, StatusFilter>
  where Status : class, IBqlTable, new()
  where StatusFilter : class, IBqlTable, new()
{
  public RQSiteStatusLookup(PXGraph graph)
    : base(graph)
  {
  }

  public RQSiteStatusLookup(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override PXView CreateIntView(PXGraph graph)
  {
    return graph.Caches[typeof (RQRequestClass)].Current is RQRequestClass current && current.RestrictItemList.GetValueOrDefault() ? RQSiteStatusLookup<Status, StatusFilter>.CreateRestictedIntView(graph) : base.CreateIntView(graph);
  }

  private static PXView CreateRestictedIntView(PXGraph graph)
  {
    Type type = BqlCommand.Compose(new Type[4]
    {
      typeof (Select2<,,>),
      typeof (Status),
      BqlCommand.Compose(new Type[7]
      {
        typeof (InnerJoin<,>),
        typeof (RQRequestClassItem),
        typeof (On<,,>),
        typeof (RQRequestClassItem.inventoryID),
        typeof (Equal<>),
        INSiteStatusLookup<Status, StatusFilter>.GetTypeField<Status>(typeof (INSiteStatusByCostCenter.inventoryID).Name),
        typeof (And<RQRequestClassItem.reqClassID, Equal<Current<RQRequestClass.reqClassID>>>)
      }),
      INSiteStatusLookup<Status, StatusFilter>.CreateWhere(graph)
    });
    return (PXView) new INSiteStatusLookup<Status, StatusFilter>.LookupView(graph, BqlCommand.CreateInstance(new Type[1]
    {
      type
    }));
  }
}
