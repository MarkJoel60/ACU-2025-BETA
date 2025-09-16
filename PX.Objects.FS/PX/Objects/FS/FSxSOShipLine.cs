// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxSOShipLine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;

#nullable enable
namespace PX.Objects.FS;

/// <summary>Extension for the shipment line</summary>
public sealed class FSxSOShipLine : PXCacheExtension<
#nullable disable
SOShipLine>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  /// <summary>
  /// Optimization flag indicating whether the location on the shipment line is different from location on the service document line
  /// </summary>
  [PXBool]
  public bool? IsLocationDifferent { get; set; }

  /// <summary>Related service document reference</summary>
  [PXString]
  public string ServiceDocRefNbr { get; set; }

  /// <summary>
  /// The user-friendly unique identifier of the Inventory Item
  /// </summary>
  [PXString]
  public string InventoryCD { get; set; }

  public abstract class isLocationDifferent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxSOShipLine.isLocationDifferent>
  {
  }

  public abstract class serviceDocRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOShipLine.serviceDocRefNbr>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxSOShipLine.inventoryCD>
  {
  }
}
