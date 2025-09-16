// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt.INTranSplitPlan`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;

public abstract class INTranSplitPlan<TGraph> : INTranSplitPlanBase<TGraph, INRegister, INTranSplit> where TGraph : PXGraph
{
  protected override void PrefetchDocumentPlansToCache()
  {
    EnumerableExtensions.Consume<PXResult<INItemPlan>>((IEnumerable<PXResult<INItemPlan>>) PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<INRegister.noteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override IEnumerable<INTranSplit> GetDocumentSplits()
  {
    return GraphHelper.RowCast<INTranSplit>((IEnumerable) PXSelectBase<INTranSplit, PXSelect<INTranSplit, Where<INTranSplit.docType, Equal<Current<INRegister.docType>>, And<INTranSplit.refNbr, Equal<Current<INRegister.refNbr>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  public override INItemPlan DefaultValues(INItemPlan planRow, INTranSplit origRow)
  {
    planRow = base.DefaultValues(planRow, origRow);
    if (planRow == null)
      return (INItemPlan) null;
    INTran inTran = PXParentAttribute.SelectParent<INTran>((PXCache) this.ItemPlanSourceCache, (object) origRow);
    planRow.ProjectID = inTran.ProjectID;
    planRow.TaskID = inTran.TaskID;
    planRow.CostCenterID = inTran.CostCenterID;
    planRow.UOM = inTran.UOM;
    planRow.BAccountID = inTran.BAccountID;
    if (inTran != null && inTran.OrigTranType == "TRX" && !planRow.OrigNoteID.HasValue)
    {
      planRow.OrigNoteID = inTran.OrigNoteID;
      planRow.OrigPlanLevel = new int?((inTran.OrigToLocationID.HasValue ? 1 : 0) | (inTran.OrigIsLotSerial.GetValueOrDefault() ? 2 : 0));
    }
    return planRow;
  }
}
