// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectOverview_ChangeOrderExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.PM;

public class ProjectOverview_ChangeOrderExt : ProjectOverviewExtension
{
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  [PXViewName("Change Order")]
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMChangeOrder.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMProject.contractID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMChangeOrder>.View ChangeOrders;
  public PXAction<PMProject> openChangeOrders;
  public PXAction<PMProject> createChangeOrder;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.changeOrder>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXAction) this.createChangeOrder).SetVisible(!this.Base.IsUserNumberingOn(((PXSelectBase<PMSetup>) this.Base.Setup).Current?.ChangeOrderNumbering));
  }

  public virtual IEnumerable changeOrders()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = false,
      IsResultTruncated = false,
      IsResultSorted = false
    };
    PXResultset<PMChangeOrder> pxResultset = PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMChangeOrder.projectID, IBqlInt>.IsEqual<BqlField<PMProject.contractID, IBqlInt>.FromCurrent>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>());
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) pxResultset))
      return (IEnumerable) pxDelegateResult;
    PMChangeOrder pmChangeOrder1 = new PMChangeOrder();
    ((List<object>) pxDelegateResult).Add((object) pmChangeOrder1);
    foreach (PXResult<PMChangeOrder> pxResult in pxResultset)
    {
      PMChangeOrder pmChangeOrder2 = PXResult<PMChangeOrder>.op_Implicit(pxResult);
      ((List<object>) pxDelegateResult).Add((object) pmChangeOrder2);
      PMChangeOrder pmChangeOrder3 = pmChangeOrder1;
      Decimal? nullable1 = pmChangeOrder1.RevenueTotal;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = pmChangeOrder2.RevenueTotal;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
      pmChangeOrder3.RevenueTotal = nullable2;
      PMChangeOrder pmChangeOrder4 = pmChangeOrder1;
      nullable1 = pmChangeOrder1.CostTotal;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = pmChangeOrder2.CostTotal;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal? nullable3 = new Decimal?(valueOrDefault3 + valueOrDefault4);
      pmChangeOrder4.CostTotal = nullable3;
      PMChangeOrder pmChangeOrder5 = pmChangeOrder1;
      nullable1 = pmChangeOrder1.CommitmentTotal;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      nullable1 = pmChangeOrder2.CommitmentTotal;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(valueOrDefault5 + valueOrDefault6);
      pmChangeOrder5.CommitmentTotal = nullable4;
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual void _(Events.RowSelected<PMChangeOrder> e)
  {
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMChangeOrder>>) e).Cache.AllowInsert = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMChangeOrder>>) e).Cache.AllowUpdate = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMChangeOrder>>) e).Cache.AllowDelete = false;
  }

  protected virtual void _(Events.RowSelected<PMProject> e)
  {
    ((PXAction) this.createChangeOrder).SetEnabled(!this.Base.IsNewProject() && !this.Base.ProjectIsInactive());
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable OpenChangeOrders(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM3080PL", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Base.Project).Current?.ContractCD));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CreateChangeOrder(PXAdapter adapter)
  {
    ChangeOrderEntry instance = PXGraph.CreateInstance<ChangeOrderEntry>();
    ((PXSelectBase<PMChangeOrder>) instance.Document).Insert();
    ((PXSelectBase<PMChangeOrder>) instance.Document).SetValueExt<PMChangeOrder.projectID>(((PXSelectBase<PMChangeOrder>) instance.Document).Current, (object) (int?) ((PXSelectBase<PMProject>) this.Base.Project).Current?.ContractID);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }
}
