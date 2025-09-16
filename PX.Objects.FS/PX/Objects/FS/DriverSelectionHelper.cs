// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DriverSelectionHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class DriverSelectionHelper
{
  public static IEnumerable DriverRecordsDelegate(
    PXGraph graph,
    SharedClasses.RouteSelected_view routeSelected,
    PXFilter<DriverSelectionFilter> filter)
  {
    if (((PXSelectBase<FSRouteDocument>) routeSelected).Current != null)
    {
      List<object> objectList = new List<object>();
      PXSelectBase<EPEmployee> pxSelectBase = (PXSelectBase<EPEmployee>) new PXSelectJoinGroupBy<EPEmployee, InnerJoin<FSRouteEmployee, On<FSRouteEmployee.employeeID, Equal<EPEmployee.bAccountID>>, LeftJoin<FSRouteDocument, On<FSRouteDocument.driverID, Equal<FSRouteEmployee.employeeID>, And<FSRouteDocument.date, Equal<Required<FSRouteDocument.date>>>>>>, Where<FSRouteEmployee.routeID, Equal<Required<FSRouteEmployee.routeID>>, And<FSxEPEmployee.sDEnabled, Equal<True>, And<FSxEPEmployee.isDriver, Equal<True>>>>, Aggregate<GroupBy<EPEmployee.bAccountID>>, OrderBy<Asc<FSRouteEmployee.priorityPreference>>>(graph);
      objectList.Add((object) ((PXSelectBase<FSRouteDocument>) routeSelected).Current.Date);
      objectList.Add((object) ((PXSelectBase<FSRouteDocument>) routeSelected).Current.RouteID);
      if (((PXSelectBase<DriverSelectionFilter>) filter).Current.ShowUnassignedDrivers.GetValueOrDefault())
        pxSelectBase.WhereAnd<Where<FSRouteDocument.routeID, IsNull>>();
      foreach (PXResult<EPEmployee, FSRouteEmployee, FSRouteDocument> pxResult in pxSelectBase.Select(objectList.ToArray()))
      {
        EPEmployee epEmployee = PXResult<EPEmployee, FSRouteEmployee, FSRouteDocument>.op_Implicit(pxResult);
        PXResult<EPEmployee, FSRouteEmployee, FSRouteDocument>.op_Implicit(pxResult);
        FSRouteDocument fsRouteDocument = PXResult<EPEmployee, FSRouteEmployee, FSRouteDocument>.op_Implicit(pxResult);
        FSxEPEmployee extension = PXCache<EPEmployee>.GetExtension<FSxEPEmployee>(epEmployee);
        if (fsRouteDocument != null && fsRouteDocument.RouteID.HasValue)
          extension.Mem_UnassignedDriver = new bool?(true);
        yield return (object) pxResult;
      }
    }
  }

  public class DriverRecords_View : 
    PXSelectReadonly2<EPEmployee, InnerJoin<FSRouteEmployee, On<FSRouteEmployee.employeeID, Equal<EPEmployee.bAccountID>>>, Where<FSxEPEmployee.isDriver, Equal<True>, And<FSxEPEmployee.sDEnabled, Equal<True>>>, OrderBy<Asc<FSRouteEmployee.priorityPreference>>>
  {
    public DriverRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public DriverRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
