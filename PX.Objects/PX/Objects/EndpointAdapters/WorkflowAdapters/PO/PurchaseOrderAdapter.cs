// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.PO.PurchaseOrderAdapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters.WorkflowAdapters.PO;

[PXVersion("20.200.001", "Default")]
[PXVersion("22.200.001", "Default")]
[PXVersion("23.200.001", "Default")]
[PXVersion("24.200.001", "Default")]
[PXVersion("25.200.001", "Default")]
internal class PurchaseOrderAdapter
{
  [FieldsProcessed(new string[] {"Type", "OrderNbr", "Hold"})]
  protected virtual void PurchaseOrder_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    POOrderEntry graph1 = (POOrderEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderNbr")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    POOrder instance = (POOrder) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (!string.IsNullOrEmpty(entityValueField1?.Value))
    {
      string str;
      if (!new POOrderType.ListAttribute().TryGetValue(entityValueField1.Value, out str))
        str = entityValueField1?.Value;
      ((PXSelectBase) graph1.Document).Cache.SetValueExt<POOrder.orderType>((object) instance, (object) str);
    }
    if (entityValueField2 != null)
      instance.OrderNbr = entityValueField2.Value;
    ((PXSelectBase<POOrder>) graph1.Document).Current = ((PXSelectBase<POOrder>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<POOrder>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected virtual void PurchaseOrder_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    POOrderEntry graph1 = (POOrderEntry) graph;
    if (((PXSelectBase<POOrder>) graph1.Document).Current == null || ((PXSelectBase<POOrder>) graph1.Document).Current.Behavior == "C")
      return;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<POOrder>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }
}
