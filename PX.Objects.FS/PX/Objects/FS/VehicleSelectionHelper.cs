// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.VehicleSelectionHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class VehicleSelectionHelper
{
  public static IEnumerable VehicleRecordsDelegate(
    PXGraph graph,
    SharedClasses.RouteSelected_view routeSelected,
    PXFilter<VehicleSelectionFilter> filter)
  {
    if (((PXSelectBase<FSRouteDocument>) routeSelected).Current != null)
    {
      List<object> objectList = new List<object>();
      PXSelectBase<FSVehicle> pxSelectBase = (PXSelectBase<FSVehicle>) new PXSelectJoinGroupBy<FSVehicle, LeftJoin<FSRouteDocument, On<FSRouteDocument.vehicleID, Equal<FSVehicle.SMequipmentID>, And<FSRouteDocument.date, Equal<Required<FSRouteDocument.date>>>>>, Where<FSEquipment.isVehicle, Equal<True>>, Aggregate<GroupBy<FSVehicle.SMequipmentID>>, OrderBy<Asc<FSServiceVehicleType.priorityPreference>>>(graph);
      objectList.Add((object) ((PXSelectBase<FSRouteDocument>) routeSelected).Current.Date);
      if (((PXSelectBase<VehicleSelectionFilter>) filter).Current.ShowUnassignedVehicles.GetValueOrDefault())
        pxSelectBase.WhereAnd<Where<FSRouteDocument.routeID, IsNull>>();
      foreach (PXResult<FSVehicle, FSRouteDocument> pxResult in pxSelectBase.Select(objectList.ToArray()))
      {
        FSVehicle fsVehicle = PXResult<FSVehicle, FSRouteDocument>.op_Implicit(pxResult);
        FSRouteDocument fsRouteDocument = PXResult<FSVehicle, FSRouteDocument>.op_Implicit(pxResult);
        if (fsRouteDocument != null && fsRouteDocument.RouteID.HasValue)
          fsVehicle.Mem_UnassignedVehicle = new bool?(true);
        yield return (object) pxResult;
      }
    }
  }

  public class VehicleRecords_View : 
    PXSelectReadonly2<FSVehicle, LeftJoin<FSServiceVehicleType, On<FSServiceVehicleType.vehicleTypeID, Equal<FSEquipment.vehicleTypeID>>>, Where<FSEquipment.isVehicle, Equal<True>, And<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>>, OrderBy<Asc<FSServiceVehicleType.priorityPreference>>>
  {
    public VehicleRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public VehicleRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
