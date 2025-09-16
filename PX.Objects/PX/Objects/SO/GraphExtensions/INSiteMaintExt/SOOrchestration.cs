// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.INSiteMaintExt.SOOrchestration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.INSiteMaintExt;

public class SOOrchestration : PXGraphExtension<INSiteMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.orderOrchestration>();

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.IN.INSite, PX.Objects.IN.INSite.active> e)
  {
    if (e.Row == null || ((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.IN.INSite, PX.Objects.IN.INSite.active>, PX.Objects.IN.INSite, object>) e).NewValue).GetValueOrDefault())
      return;
    bool flag = ((IQueryable<PXResult<SOOrchestrationPlan>>) PXSelectBase<SOOrchestrationPlan, PXViewOf<SOOrchestrationPlan>.BasedOn<SelectFromBase<SOOrchestrationPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.sourceSiteID, Equal<P.AsInt>>>>, And<BqlOperand<SOOrchestrationPlan.strategy, IBqlString>.IsEqual<OrchestrationStrategies.warehousePriority>>>>.And<BqlOperand<SOOrchestrationPlan.isActive, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) e.Row.SiteID
    })).Any<PXResult<SOOrchestrationPlan>>();
    if (!flag)
      flag = ((IQueryable<PXResult<SOOrchestrationPlanLine>>) PXSelectBase<SOOrchestrationPlanLine, PXViewOf<SOOrchestrationPlanLine>.BasedOn<SelectFromBase<SOOrchestrationPlanLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrchestrationPlan>.On<BqlOperand<SOOrchestrationPlanLine.planID, IBqlString>.IsEqual<SOOrchestrationPlanLine.planID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.isActive, Equal<True>>>>>.And<BqlOperand<SOOrchestrationPlanLine.targetSiteID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) e.Row.SiteID
      })).Any<PXResult<SOOrchestrationPlanLine>>();
    if (flag)
      throw new PXSetPropertyException<PX.Objects.IN.INSite.active>("You cannot deactivate the warehouse because it is included in at least one active orchestration plan.", (PXErrorLevel) 4);
  }
}
