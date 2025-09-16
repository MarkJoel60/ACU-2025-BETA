// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PrintPackageFilesArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class PrintPackageFilesArgs
{
  public List<SOShipment> Shipments { get; set; }

  public PXAdapter Adapter { get; set; }

  public PackageFileCategory Category { get; set; }

  public string PrintFormID { get; set; }

  public Dictionary<Guid, ShipmentRelatedReports> PrinterToReportsMap { get; set; }

  public PXReportRequiredException RedirectToReport { get; set; }
}
