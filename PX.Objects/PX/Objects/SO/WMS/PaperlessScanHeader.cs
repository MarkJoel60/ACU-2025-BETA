// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.PaperlessScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO.WMS;

public sealed class PaperlessScanHeader : 
  PXCacheExtension<
  #nullable disable
  WorksheetScanHeader, WMSScanHeader, QtyScanHeader, ScanHeader>
{
  public static bool IsActive() => PaperlessPicking.IsActive();

  [Location(typeof (WMSScanHeader.siteID), Enabled = false, DisplayName = "Current Location", KeepEntry = false, ResetEntry = false)]
  [PXUIVisible(typeof (BqlOperand<PaperlessScanHeader.lastVisitedLocationID, IBqlInt>.IsNotNull))]
  public int? LastVisitedLocationID { get; set; }

  [PXBool]
  public bool? PathInversedDirection { get; set; }

  [PXInt]
  public int? WantedLineNbr { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  [PXSelector(typeof (PX.Objects.SO.SOShipment.shipmentNbr))]
  public string SingleShipmentNbr { get; set; }

  public HashSet<int> IgnoredPickingJobs { get; set; } = new HashSet<int>();

  public abstract class lastVisitedLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PaperlessScanHeader.lastVisitedLocationID>
  {
  }

  public abstract class pathInversedDirection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaperlessScanHeader.pathInversedDirection>
  {
  }

  public abstract class wantedLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PaperlessScanHeader.wantedLineNbr>
  {
  }

  public abstract class singleShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaperlessScanHeader.singleShipmentNbr>
  {
  }

  public abstract class ignoredPickingJobs : IBqlField, IBqlOperand
  {
  }
}
