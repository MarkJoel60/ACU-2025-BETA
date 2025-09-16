// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOShipLineSplitPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SOShipLineSplitPlan : SOShipLineSplitPlanBase<SOShipLineSplit>
{
  protected Dictionary<object[], HashSet<Guid?>> _processingSets;

  public override void Initialize()
  {
    base.Initialize();
    this._processingSets = new Dictionary<object[], HashSet<Guid?>>();
  }

  protected override void UpdatePlansOnParentUpdated(PX.Objects.SO.SOShipment parent)
  {
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<PX.Objects.SO.SOShipment.noteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      INItemPlan inItemPlan = PXResult<INItemPlan>.op_Implicit(pxResult);
      inItemPlan.Hold = parent.Hold;
      inItemPlan.PlanDate = parent.ShipDate;
      GraphHelper.MarkUpdated((PXCache) this.PlanCache, (object) inItemPlan, true);
    }
  }

  protected override HashSet<long?> CollectShipmentPlans(string shipmentNbr = null)
  {
    object[] objArray;
    if (shipmentNbr != null)
      objArray = new object[1]{ (object) shipmentNbr };
    else
      objArray = new object[0];
    return new HashSet<long?>((IEnumerable<long?>) ((IQueryable<PXResult<SOShipLineSplit>>) PXSelectBase<SOShipLineSplit, PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Optional<PX.Objects.SO.SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this.Base, objArray)).Select<PXResult<SOShipLineSplit>, long?>((Expression<Func<PXResult<SOShipLineSplit>, long?>>) (s => ((SOShipLineSplit) s).PlanID)));
  }

  public override void _(PX.Data.Events.RowPersisting<INItemPlan> e)
  {
    if (e.Operation == 1)
    {
      PXCache pxCache = (PXCache) GraphHelper.Caches<PX.Objects.SO.SOShipment>((PXGraph) this.Base);
      INItemPlan row = e.Row;
      if (row != null && pxCache.Current is PX.Objects.SO.SOShipment current)
      {
        Guid? refNoteId = row.RefNoteID;
        Guid? noteId = current.NoteID;
        object[] objArray;
        if ((refNoteId.HasValue == noteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != noteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && PXLongOperation.GetCustomInfo(((PXGraph) this.Base).UID, "PXProcessingState", ref objArray) != null && objArray != null)
        {
          HashSet<Guid?> hashSet;
          if (!this._processingSets.TryGetValue(objArray, out hashSet))
          {
            hashSet = ((IEnumerable<object>) objArray).Select<object, Guid?>((Func<object, Guid?>) (x => ((PX.Objects.SO.SOShipment) x).NoteID)).ToHashSet<Guid?>();
            this._processingSets[objArray] = hashSet;
          }
          if (hashSet.Contains(row.RefNoteID))
            e.Cancel = true;
        }
      }
    }
    base._(e);
  }
}
