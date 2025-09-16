// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt.KitTranSplitPlan
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

public class KitTranSplitPlan : INTranSplitPlanBase<KitAssemblyEntry, INKitRegister, INKitTranSplit>
{
  protected override void PrefetchDocumentPlansToCache()
  {
    EnumerableExtensions.Consume<PXResult<INItemPlan>>((IEnumerable<PXResult<INItemPlan>>) PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<INKitRegister.noteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override IEnumerable<INKitTranSplit> GetDocumentSplits()
  {
    return GraphHelper.RowCast<INKitTranSplit>((IEnumerable) PXSelectBase<INKitTranSplit, PXSelect<INKitTranSplit, Where<INKitTranSplit.docType, Equal<Current<INKitRegister.docType>>, And<INKitTranSplit.refNbr, Equal<Current<INKitRegister.refNbr>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  public override void _(Events.RowUpdated<INKitRegister> e)
  {
    base._(e);
    DateTime? kitRequestDate1 = e.Row.KitRequestDate;
    DateTime? kitRequestDate2 = e.OldRow.KitRequestDate;
    if ((kitRequestDate1.HasValue == kitRequestDate2.HasValue ? (kitRequestDate1.HasValue ? (kitRequestDate1.GetValueOrDefault() != kitRequestDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    this.PrefetchDocumentPlansToCache();
    foreach (INKitTranSplit documentSplit in this.GetDocumentSplits())
    {
      foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Cached)
      {
        long? planId1 = inItemPlan.PlanID;
        long? planId2 = documentSplit.PlanID;
        if (planId1.GetValueOrDefault() == planId2.GetValueOrDefault() & planId1.HasValue == planId2.HasValue && EnumerableExtensions.IsNotIn<PXEntryStatus>(this.PlanCache.GetStatus(inItemPlan), (PXEntryStatus) 3, (PXEntryStatus) 4))
        {
          INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan);
          copy.PlanDate = e.Row.KitRequestDate;
          this.PlanCache.Update(copy);
        }
      }
    }
  }

  public override INItemPlan DefaultValues(INItemPlan planRow, INKitTranSplit origRow)
  {
    planRow = base.DefaultValues(planRow, origRow);
    if (planRow == null)
      return (INItemPlan) null;
    INKitRegister inKitRegister = PXParentAttribute.SelectParent<INKitRegister>((PXCache) this.ItemPlanSourceCache, (object) origRow);
    planRow.ProjectID = inKitRegister.ProjectID;
    planRow.TaskID = inKitRegister.TaskID;
    planRow.CostCenterID = inKitRegister.CostCenterID;
    planRow.UOM = inKitRegister.UOM;
    planRow.PlanDate = inKitRegister.KitRequestDate;
    return planRow;
  }
}
