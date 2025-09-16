// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.TransferScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.WMS;

public sealed class TransferScanHeader : 
  PXCacheExtension<
  #nullable disable
  RegisterScanHeader, WMSScanHeader, QtyScanHeader, ScanHeader>
{
  [Site(DisplayName = "To Warehouse")]
  [PXUIVisible(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TransferScanHeader.transferToSiteID, IsNotNull>>>>.And<BqlOperand<TransferScanHeader.transferToSiteID, IBqlInt>.IsNotEqual<WMSScanHeader.siteID>>))]
  public int? TransferToSiteID { get; set; }

  [Location(typeof (TransferScanHeader.transferToSiteID))]
  public int? TransferToLocationID { get; set; }

  [PXString]
  public string AmbiguousLocationCD { get; set; }

  [PXBool]
  public bool? AmbiguousSource { get; set; }

  /// <summary>Skip availability warning</summary>
  [PXBool]
  public bool? SkipAvailabilityWarning { get; set; }

  public abstract class transferToSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransferScanHeader.transferToSiteID>
  {
  }

  public abstract class transferToLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransferScanHeader.transferToLocationID>
  {
  }

  public abstract class ambiguousLocationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TransferScanHeader.ambiguousLocationCD>
  {
  }

  public abstract class ambiguousSource : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    TransferScanHeader.ambiguousSource>
  {
  }

  public abstract class skipAvailabilityWarning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    TransferScanHeader.skipAvailabilityWarning>
  {
  }
}
