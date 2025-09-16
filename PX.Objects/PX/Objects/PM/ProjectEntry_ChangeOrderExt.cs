// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectEntry_ChangeOrderExt
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
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// This class implements graph extension to use change order extension
/// </summary>
public class ProjectEntry_ChangeOrderExt : ChangeOrderExt<ProjectEntry, PMProject>
{
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  [PXViewName("Change Order")]
  public PXSelect<PMChangeOrder, Where<PMChangeOrder.projectID, Equal<Current<PMProject.contractID>>>> ChangeOrders;
  public PXAction<PMProject> createChangeOrder;

  public new static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.changeOrder>();

  public override PXSelectBase<PMChangeOrder> ChangeOrder
  {
    get => (PXSelectBase<PMChangeOrder>) this.ChangeOrders;
  }

  public override PMChangeOrder CurrentChangeOrder
  {
    get => ((PXSelectBase<PMChangeOrder>) this.ChangeOrders).Current;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CreateChangeOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Base.Project).Current != null)
    {
      ChangeOrderEntry instance = PXGraph.CreateInstance<ChangeOrderEntry>();
      ((PXSelectBase<PMChangeOrder>) instance.Document).Insert();
      ((PXSelectBase<PMChangeOrder>) instance.Document).SetValueExt<PMChangeOrder.projectID>(((PXSelectBase<PMChangeOrder>) instance.Document).Current, (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.ContractID);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, false, "Change Order");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual void _(Events.RowSelected<PMProject> e)
  {
    bool flag = this.ChangeOrderEnabled();
    ((PXAction) this.createChangeOrder).SetVisible(this.ChangeOrderVisible());
    ((PXAction) this.createChangeOrder).SetEnabled(flag);
  }

  public override bool ChangeOrderEnabled()
  {
    return ((PXSelectBase<PMProject>) this.Base.Project).Current != null ? ((PXSelectBase<PMProject>) this.Base.Project).Current.ChangeOrderWorkflow.GetValueOrDefault() : this.ChangeOrderFeatureEnabled();
  }

  [PXOverride]
  public virtual List<string> ValidateProjectClosure(
    int? projectID,
    Func<int?, List<string>> baseMethod)
  {
    List<string> stringList = baseMethod(projectID);
    IEnumerable<PMChangeOrder> firstTableItems = PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMChangeOrder.released, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) projectID
    }).FirstTableItems;
    stringList.AddRange(firstTableItems.Select<PMChangeOrder, string>((Func<PMChangeOrder, string>) (x => PXMessages.LocalizeFormatNoPrefix("The project cannot be closed because the {0} change order related to the project is unreleased.", new object[1]
    {
      (object) x.RefNbr
    }))));
    return stringList;
  }
}
