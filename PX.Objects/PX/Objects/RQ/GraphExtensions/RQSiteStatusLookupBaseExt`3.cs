// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.GraphExtensions.RQSiteStatusLookupBaseExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Extensions.AddItemLookup;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.RQ.GraphExtensions;

public abstract class RQSiteStatusLookupBaseExt<TGraph, TDocument, TLine> : 
  SiteStatusLookupExt<TGraph, TDocument, TLine, RQSiteStatusSelected, POSiteStatusFilter>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, new()
{
  protected override PXView CreateItemInfoView()
  {
    RQRequestClass current = (RQRequestClass) ((PXCache) GraphHelper.Caches<RQRequestClass>((PXGraph) this.Base)).Current;
    return current != null && current.RestrictItemList.GetValueOrDefault() ? this.CreateRestrictedIntView() : base.CreateItemInfoView();
  }

  private PXView CreateRestrictedIntView()
  {
    return (PXView) new AddItemLookupBaseExt<TGraph, TDocument, RQSiteStatusSelected, POSiteStatusFilter>.LookupView((PXGraph) this.Base, ((BqlCommand) new SelectFrom<RQSiteStatusSelected, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<RQRequestClassItem>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RQRequestClassItem.inventoryID, Equal<RQSiteStatusSelected.inventoryID>>>>>.And<BqlOperand<RQRequestClassItem.reqClassID, IBqlString>.IsEqual<BqlField<RQRequestClass.reqClassID, IBqlString>.FromCurrent>>>()).WhereAnd(this.CreateWhere()));
  }
}
