// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt.ComponentTranSplitPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt;

public class ComponentTranSplitPlan : 
  INTranSplitPlanBase<KitAssemblyEntry, INKitRegister, INComponentTranSplit>
{
  protected override void PrefetchDocumentPlansToCache()
  {
    EnumerableExtensions.Consume<PXResult<INItemPlan>>((IEnumerable<PXResult<INItemPlan>>) PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<INKitRegister.noteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override IEnumerable<INComponentTranSplit> GetDocumentSplits()
  {
    return GraphHelper.RowCast<INComponentTranSplit>((IEnumerable) PXSelectBase<INComponentTranSplit, PXSelect<INComponentTranSplit, Where<INComponentTranSplit.docType, Equal<Current<INKitRegister.docType>>, And<INComponentTranSplit.refNbr, Equal<Current<INKitRegister.refNbr>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  public override INItemPlan DefaultValues(INItemPlan planRow, INComponentTranSplit origRow)
  {
    planRow = base.DefaultValues(planRow, origRow);
    if (planRow == null)
      return (INItemPlan) null;
    INComponentTran inComponentTran = PXParentAttribute.SelectParent<INComponentTran>((PXCache) this.ItemPlanSourceCache, (object) origRow);
    INKitRegister current = ((PXSelectBase<INKitRegister>) this.Base.Document).Current;
    planRow.ProjectID = inComponentTran.ProjectID;
    planRow.TaskID = inComponentTran.TaskID;
    planRow.CostCenterID = inComponentTran.CostCenterID;
    planRow.UOM = inComponentTran.UOM;
    planRow.BAccountID = inComponentTran.BAccountID;
    planRow.PlanDate = current != null ? current.KitRequestDate : planRow.PlanDate;
    return planRow;
  }
}
