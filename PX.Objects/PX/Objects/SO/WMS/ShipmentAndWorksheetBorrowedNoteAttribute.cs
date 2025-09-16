// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.ShipmentAndWorksheetBorrowedNoteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.WMS;

public class ShipmentAndWorksheetBorrowedNoteAttribute : PXNoteAttribute
{
  protected virtual string GetEntityType(PXCache cache, Guid? noteId)
  {
    if (!(cache.Graph is PickPackShip.Host graph))
      return base.GetEntityType(cache, noteId);
    return !this.IsWorksheet(graph, noteId) ? typeof (SOShipment).FullName : typeof (SOPickingWorksheet).FullName;
  }

  protected virtual string GetGraphType(PXGraph graph)
  {
    if (!(graph is PickPackShip.Host pps))
      return base.GetGraphType(graph);
    return !this.IsWorksheet(pps) ? typeof (SOShipmentEntry).FullName : typeof (SOPickingWorksheetReview).FullName;
  }

  protected virtual bool IsWorksheet(PickPackShip.Host pps)
  {
    return WorksheetPicking.IsActive() && pps.WMS.Get<WorksheetPicking>().IsWorksheetMode(pps.WMS.CurrentMode.Code);
  }

  protected virtual bool IsWorksheet(PickPackShip.Host pps, Guid? noteID)
  {
    if (!WorksheetPicking.IsActive() || !noteID.HasValue)
      return false;
    return ((IEnumerable<PXResult<SOPickingWorksheet>>) PXSelectBase<SOPickingWorksheet, PXViewOf<SOPickingWorksheet>.BasedOn<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetType, In3<SOPickingWorksheet.worksheetType.wave, SOPickingWorksheet.worksheetType.batch>>>>>.And<BqlOperand<SOPickingWorksheet.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>>.Config>.Select((PXGraph) pps, new object[1]
    {
      (object) noteID
    })).AsEnumerable<PXResult<SOPickingWorksheet>>().Any<PXResult<SOPickingWorksheet>>();
  }
}
