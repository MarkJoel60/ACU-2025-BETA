// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DriverSelectionFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class DriverSelectionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Route Document ID", Enabled = false)]
  [PXSelector(typeof (FSRouteDocument.routeDocumentID), SubstituteKey = typeof (FSRouteDocument.refNbr))]
  public virtual int? RouteDocumentID { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Show Available Drivers for this Route only")]
  public virtual bool? ShowUnassignedDrivers { get; set; }

  [PXInt]
  public virtual int? VehicleTypeID { get; set; }

  public abstract class routeDocumentID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    DriverSelectionFilter.routeDocumentID>
  {
  }

  public abstract class showUnassignedDrivers : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DriverSelectionFilter.showUnassignedDrivers>
  {
  }

  public abstract class vehicleTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DriverSelectionFilter.vehicleTypeID>
  {
  }
}
