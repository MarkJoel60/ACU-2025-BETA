// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Services.INRegisterEntryFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.SO.Services;

public class INRegisterEntryFactory
{
  protected SOShipmentEntry _shipGraph;
  protected string _lastShipmentType;
  protected INRegisterEntryBase _lastCreatedGraph;

  public INRegisterEntryFactory(SOShipmentEntry shipGraph) => this._shipGraph = shipGraph;

  public virtual INRegisterEntryBase GetOrCreateINRegisterEntry(PX.Objects.SO.SOShipment shipment)
  {
    if (shipment.ShipmentType == this._lastShipmentType)
    {
      ((PXGraph) this._lastCreatedGraph).Clear();
      return this._lastCreatedGraph;
    }
    INRegisterEntryBase graph = shipment.ShipmentType == "T" ? (INRegisterEntryBase) PXGraph.CreateInstance<INTransferEntry>() : (INRegisterEntryBase) PXGraph.CreateInstance<INIssueEntry>();
    this._shipGraph.MergeCachesWithINRegisterEntry(graph);
    this._lastShipmentType = shipment.ShipmentType;
    this._lastCreatedGraph = graph;
    return graph;
  }
}
