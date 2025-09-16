// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.IN.InventoryAdjustmentAdapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.EndpointAdapters.WorkflowAdapters.IN;

[PXVersion("20.200.001", "Default")]
[PXVersion("22.200.001", "Default")]
[PXVersion("23.200.001", "Default")]
[PXVersion("24.200.001", "Default")]
[PXVersion("25.200.001", "Default")]
internal class InventoryAdjustmentAdapter : InventoryRegisterAdapterBase
{
  [FieldsProcessed(new string[] {"ReferenceNbr", "Hold"})]
  protected virtual void InventoryAdjustment_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    this.INRegisterInsert((INRegisterEntryBase) graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected virtual void InventoryAdjustment_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    this.INRegisterUpdate((INRegisterEntryBase) graph, entity, targetEntity);
  }
}
