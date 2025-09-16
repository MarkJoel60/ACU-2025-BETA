// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.CorrectShipmentArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.Models;

/// <exclude />
public class CorrectShipmentArgs
{
  public CorrectShipmentArgs(SOShipment shipment, PXGraph origDocumentGraph)
  {
    this.Shipment = shipment;
    this.OrigDocumentGraph = origDocumentGraph;
    this.ShipLinesClearedSOAllocation = new HashSet<int?>();
  }

  /// <summary>
  /// Gets graph object that should be used to process shipment original document.
  /// </summary>
  public PXGraph OrigDocumentGraph { get; }

  public SOShipment Shipment { get; }

  public HashSet<int?> ShipLinesClearedSOAllocation { get; }
}
