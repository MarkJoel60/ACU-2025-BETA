// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.PO.PurchaseReceiptAdapter
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
internal class PurchaseReceiptAdapter
{
  [FieldsProcessed(new string[] {"Type", "ReceiptNbr", "Hold"})]
  protected virtual void PurchaseReceipt_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    POReceiptEntry graph1 = (POReceiptEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReceiptNbr")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    POReceipt instance = (POReceipt) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (!string.IsNullOrEmpty(entityValueField1?.Value))
    {
      string str;
      if (!new POReceiptType.ListAttribute().TryGetValue(entityValueField1.Value, out str))
        str = entityValueField1?.Value;
      ((PXSelectBase) graph1.Document).Cache.SetValueExt<POReceipt.receiptType>((object) instance, (object) str);
    }
    if (entityValueField2 != null)
      instance.ReceiptNbr = entityValueField2.Value;
    ((PXSelectBase<POReceipt>) graph1.Document).Current = ((PXSelectBase<POReceipt>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<POReceipt>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected virtual void PurchaseReceipt_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    POReceiptEntry graph1 = (POReceiptEntry) graph;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<POReceipt>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }
}
