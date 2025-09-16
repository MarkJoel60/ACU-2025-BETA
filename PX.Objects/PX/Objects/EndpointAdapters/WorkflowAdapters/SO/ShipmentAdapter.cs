// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.SO.ShipmentAdapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters.WorkflowAdapters.SO;

[PXVersion("20.200.001", "Default")]
[PXVersion("22.200.001", "Default")]
[PXVersion("23.200.001", "Default")]
[PXVersion("24.200.001", "Default")]
[PXVersion("25.200.001", "Default")]
internal class ShipmentAdapter
{
  [FieldsProcessed(new string[] {"ShipmentNbr", "Type", "Hold"})]
  protected virtual void Shipment_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    SOShipmentEntry graph1 = (SOShipmentEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ShipmentNbr")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    SOShipment instance = (SOShipment) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (entityValueField2 != null)
      ((PXSelectBase) graph1.Document).Cache.SetValueExt<SOShipment.shipmentType>((object) instance, (object) entityValueField2.Value);
    if (entityValueField1 != null)
      instance.ShipmentNbr = entityValueField1.Value;
    ((PXSelectBase<SOShipment>) graph1.Document).Current = ((PXSelectBase<SOShipment>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<SOShipment>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  [FieldsProcessed(new string[] {"Hold", "FreightAmount"})]
  protected virtual void Shipment_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    SOShipmentEntry graph1 = (SOShipmentEntry) graph;
    ((PXSelectBase) graph1.Document).View.Answer = (WebDialogResult) 6;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "FreightAmount")) is EntityValueField entityValueField)
    {
      SOShipmentEntry soShipmentEntry = (SOShipmentEntry) graph;
      SOShipment current = ((PXSelectBase<SOShipment>) soShipmentEntry.Document).Current;
      if (current.FreightAmountSource != "O")
      {
        ((PXSelectBase<SOShipment>) soShipmentEntry.Document).SetValueExt<SOShipment.overrideFreightAmount>(current, (object) true);
        ((PXSelectBase<SOShipment>) soShipmentEntry.Document).SetValueExt<SOShipment.curyFreightAmt>(current, (object) entityValueField.Value);
      }
    }
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<SOShipment>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }
}
