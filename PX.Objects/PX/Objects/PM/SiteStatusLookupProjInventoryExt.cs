// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SiteStatusLookupProjInventoryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.Extensions.AddItemLookup;
using PX.Objects.IN;
using PX.Objects.PM.CacheExtensions;

#nullable disable
namespace PX.Objects.PM;

public class SiteStatusLookupProjInventoryExt : INSiteStatusLookupExt<INIssueEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();

  protected override bool IsAddItemEnabled(INRegister doc)
  {
    return ((PXSelectBase) this.LSSelect).AllowDelete;
  }

  protected override void _(PX.Data.Events.RowSelected<INSiteStatusSelected> e)
  {
    base._(e);
    PXCache pxCache = (PXCache) GraphHelper.Caches<INSiteStatusFilter>((PXGraph) this.Base);
    INSiteStatusFilterExt extension = pxCache?.GetExtension<INSiteStatusFilterExt>(pxCache.Current);
    bool flag = extension != null && extension.ProjectInformation.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<INSiteStatusSelectedExt.inventorySource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSiteStatusSelected>>) e).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<INSiteStatusSelectedExt.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSiteStatusSelected>>) e).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<INSiteStatusSelectedExt.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INSiteStatusSelected>>) e).Cache, (object) null, flag);
  }

  protected override INTran InitTran(INTran newTran, INSiteStatusSelected siteStatus)
  {
    INTran inTran = base.InitTran(newTran, siteStatus);
    INSiteStatusSelectedExt extension = PXCacheEx.GetExtension<INSiteStatusSelectedExt>((IBqlTable) siteStatus);
    if ((extension != null ? (!extension.ProjectID.HasValue ? 1 : 0) : 1) != 0)
      return inTran;
    inTran.ProjectID = extension.ProjectID;
    inTran.TaskID = extension.TaskID;
    inTran.InventorySource = extension.InventorySource;
    return inTran;
  }

  protected override PXView CreateItemInfoView()
  {
    PXCache<INSiteStatusFilter> pxCache = GraphHelper.Caches<INSiteStatusFilter>((PXGraph) this.Base);
    INSiteStatusFilterExt extension = ((PXCache) pxCache).GetExtension<INSiteStatusFilterExt>((object) (((PXCache) pxCache).Current as INSiteStatusFilter));
    return (extension != null ? (!extension.ProjectInformation.GetValueOrDefault() ? 1 : 0) : 1) != 0 ? base.CreateItemInfoView() : (PXView) new AddItemLookupBaseExt<INIssueEntry, INRegister, INSiteStatusSelected, INSiteStatusFilter>.LookupView((PXGraph) this.Base, ((BqlCommand) new SelectFromBase<INSiteStatusSelected, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusSelected.costCenterID, IsNull>>>, Or<BqlOperand<INSiteStatusSelected.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusSelectedExt.costLayerType, Equal<CostLayerType.normal>>>>>.Or<BqlOperand<INSiteStatusSelectedExt.costLayerType, IBqlString>.IsEqual<CostLayerType.project>>>>()).WhereAnd(this.CreateWhere()));
  }
}
