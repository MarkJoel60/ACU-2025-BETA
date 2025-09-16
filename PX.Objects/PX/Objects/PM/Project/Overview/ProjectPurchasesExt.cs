// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.ProjectPurchasesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.PM.Project.Overview;

/// <summary>
/// The 'Subcontracts and Purchases' section on the PM301500 screen.
/// </summary>
public class ProjectPurchasesExt : ProjectOverviewExtension
{
  public 
  #nullable disable
  PXSetup<PX.Objects.PO.POSetup> POSetup;
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<POOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ProjectPOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectPOLine.orderNbr, 
  #nullable disable
  Equal<POOrder.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  ProjectPOLine.orderType, IBqlString>.IsEqual<
  #nullable disable
  POOrder.orderType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectPOLine.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  POOrder.orderType, IBqlString>.IsNotEqual<
  #nullable disable
  POOrderType.regularSubcontract>>>.Order<By<BqlField<
  #nullable enable
  POOrder.createdDateTime, IBqlDateTime>.Desc>>, 
  #nullable disable
  POOrder>.View PurchaseOrders;
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<POOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ProjectPOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectPOLine.orderNbr, 
  #nullable disable
  Equal<POOrder.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  ProjectPOLine.orderType, IBqlString>.IsEqual<
  #nullable disable
  POOrder.orderType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ProjectPOLine.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  POOrder.orderType, IBqlString>.IsEqual<
  #nullable disable
  POOrderType.regularSubcontract>>>.Order<By<BqlField<
  #nullable enable
  POOrder.createdDateTime, IBqlDateTime>.Desc>>, 
  #nullable disable
  POOrder>.View Subcontracts;
  public PXAction<PMProject> openPurchaseOrders;
  public PXAction<PMProject> openSubcontracts;
  public PXAction<PMProject> createPurchaseOrder;
  public PXAction<PMProject> createSubcontract;
  public PXAction<PMProject> createDropShipOrder;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable OpenPurchaseOrders(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PO3010PL", DataViewHelper.DataViewFilter.Create<POOrder, POOrder.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.ContractCD));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable OpenSubcontracts(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("SC3010PL", DataViewHelper.DataViewFilter.Create<POOrder, POOrder.projectID>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.ContractCD));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CreatePurchaseOrder(PXAdapter adapter)
  {
    return this.CreatePOOrderBase<POOrderEntry>(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CreateSubcontract(PXAdapter adapter)
  {
    return this.CreatePOOrderBase<SubcontractEntry>(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CreateDropShipOrder(PXAdapter adapter)
  {
    return this.CreatePOOrderBase<POOrderEntry>(adapter, (Action<POOrderEntry, POOrder>) ((graph, order) => ((PXSelectBase) graph.Document).Cache.SetValueExt<POOrder.orderType>((object) order, (object) "PD")));
  }

  public virtual IEnumerable CreatePOOrderBase<TGraph>(
    PXAdapter adapter,
    Action<TGraph, POOrder> initializing = null)
    where TGraph : POOrderEntry, new()
  {
    TGraph instance = PXGraph.CreateInstance<TGraph>();
    POOrder poOrder = ((PXSelectBase<POOrder>) instance.Document).Insert();
    if (initializing != null)
      initializing(instance, poOrder);
    ((PXSelectBase) instance.Document).Cache.SetValueExt<POOrder.projectID>((object) poOrder, (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.ContractID);
    ProjectAccountingService.NavigateToScreen((PXGraph) (object) instance);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
  {
    bool flag1 = this.Base.IsNewProject();
    ((PXAction) this.createSubcontract).SetVisible(!flag1 && this.Base.CostCommitmentTrackingEnabled() && !this.Base.IsUserNumberingOn("SUBCONTR"));
    bool flag2 = !flag1 && this.Base.CostCommitmentTrackingEnabled() && !this.Base.IsUserNumberingOn(((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current?.RegularPONumberingID);
    ((PXAction) this.createPurchaseOrder).SetVisible(flag2);
    ((PXAction) this.createDropShipOrder).SetVisible(flag2);
  }

  protected virtual void _(PX.Data.Events.RowSelected<POOrder> e)
  {
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POOrder>>) e).Cache.AllowInsert = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POOrder>>) e).Cache.AllowUpdate = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POOrder>>) e).Cache.AllowDelete = false;
  }
}
