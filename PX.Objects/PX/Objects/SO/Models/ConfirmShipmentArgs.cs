// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.ConfirmShipmentArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.SO.Models;

/// <exclude />
public class ConfirmShipmentArgs
{
  public ConfirmShipmentArgs(
    SOShipment shipment,
    PXGraph origDocumentGraph,
    bool isShipmentReadyForConfirmation = false)
  {
    this.Shipment = shipment;
    this.OrigDocumentGraph = origDocumentGraph;
    this.IsShipmentReadyForConfirmation = isShipmentReadyForConfirmation;
  }

  /// <summary>
  /// Gets flag that indicates that the shipment that should be confirmed is already prepared for confirmation
  /// (<see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ConfirmShipmentExtension.PrepareShipmentForConfirmation(PX.Objects.SO.SOShipment)" /> is executed and shipment is validated).
  /// </summary>
  public bool IsShipmentReadyForConfirmation { get; }

  /// <summary>
  /// Gets graph object that should be used to process shipment original document.
  /// </summary>
  public PXGraph OrigDocumentGraph { get; }

  public SOShipment Shipment { get; }
}
