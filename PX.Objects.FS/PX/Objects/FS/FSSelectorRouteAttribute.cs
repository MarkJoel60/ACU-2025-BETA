// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorRouteAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorRouteAttribute : PXSelectorAttribute
{
  public FSSelectorRouteAttribute()
    : base(typeof (Search<FSRouteDocument.routeDocumentID>), new Type[6]
    {
      typeof (FSRouteDocument.refNbr),
      typeof (FSRouteDocument.date),
      typeof (FSRouteDocument.driverID),
      typeof (FSRouteDocument.routeID),
      typeof (FSRouteDocument.status),
      typeof (FSRouteDocument.vehicleID)
    })
  {
    this.SubstituteKey = typeof (FSRouteDocument.refNbr);
  }
}
