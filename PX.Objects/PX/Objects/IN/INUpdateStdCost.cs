// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUpdateStdCost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INUpdateStdCost : PXGraph<INUpdateStdCost>
{
  public PXCancel<INSiteFilter> Cancel;
  public PXFilter<INSiteFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<INUpdateStdCostRecord, INSiteFilter, Where2<Match<INUpdateStdCostRecord, Current<AccessInfo.userName>>, And<Where2<Where<Current<INSiteFilter.siteID>, IsNull, And<Where<INUpdateStdCostRecord.pendingStdCostDate, LessEqual<Current<INSiteFilter.pendingStdCostDate>>, Or<INUpdateStdCostRecord.pendingStdCostReset, Equal<boolTrue>>>>>, Or<Current<INSiteFilter.siteID>, Equal<INUpdateStdCostRecord.siteID>, And<Where<INUpdateStdCostRecord.pendingStdCostDate, LessEqual<Current<INSiteFilter.pendingStdCostDate>>, Or<Current<INSiteFilter.revalueInventory>, Equal<boolTrue>, Or<INUpdateStdCostRecord.pendingStdCostReset, Equal<boolTrue>>>>>>>>>> INItemList;
  public PXSetup<INSetup> insetup;

  public virtual bool IsDirty => false;

  public INUpdateStdCost()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    ((PXProcessing<INUpdateStdCostRecord>) this.INItemList).SetProcessCaption("Process");
    ((PXProcessing<INUpdateStdCostRecord>) this.INItemList).SetProcessAllCaption("Process All");
  }

  protected virtual void INSiteFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (((INSiteFilter) e.Row).RevalueInventory.GetValueOrDefault() && ((INSiteFilter) e.Row).SiteID.HasValue)
    {
      // ISSUE: method pointer
      // ISSUE: method pointer
      ((PXProcessingBase<INUpdateStdCostRecord>) this.INItemList).SetProcessDelegate<INUpdateStdCostProcess>(new PXProcessingBase<INUpdateStdCostRecord>.ProcessItemDelegate<INUpdateStdCostProcess>((object) null, __methodptr(RevalueInventory)), new PXProcessingBase<INUpdateStdCostRecord>.FinallyProcessDelegate<INUpdateStdCostProcess>((object) null, __methodptr(ReleaseAdjustment)));
    }
    else
    {
      // ISSUE: method pointer
      // ISSUE: method pointer
      ((PXProcessingBase<INUpdateStdCostRecord>) this.INItemList).SetProcessDelegate<INUpdateStdCostProcess>(new PXProcessingBase<INUpdateStdCostRecord>.ProcessItemDelegate<INUpdateStdCostProcess>((object) null, __methodptr(UpdateStdCost)), new PXProcessingBase<INUpdateStdCostRecord>.FinallyProcessDelegate<INUpdateStdCostProcess>((object) null, __methodptr(ReleaseAdjustment)));
    }
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  protected virtual IEnumerable initemlist() => (IEnumerable) null;

  protected virtual void INSiteFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.INItemList).Cache.Clear();
  }

  public static void UpdateStdCost(INUpdateStdCostProcess graph, INUpdateStdCostRecord itemsite)
  {
    graph.UpdateStdCost(itemsite);
  }

  public static void RevalueInventory(INUpdateStdCostProcess graph, INUpdateStdCostRecord itemsite)
  {
    graph.RevalueInventory(itemsite);
  }

  public static void ReleaseAdjustment(INUpdateStdCostProcess graph) => graph.ReleaseAdjustment();
}
