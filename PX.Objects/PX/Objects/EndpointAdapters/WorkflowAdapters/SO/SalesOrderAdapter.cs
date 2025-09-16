// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.SO.SalesOrderAdapter
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
internal class SalesOrderAdapter
{
  [FieldsProcessed(new string[] {"OrderType", "OrderNbr", "Hold", "CreditHold"})]
  protected virtual void SalesOrder_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOOrderEntry graph1 = (SOOrderEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderType")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderNbr")) as EntityValueField;
    EntityValueField holdField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    EntityValueField holdField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CreditHold")) as EntityValueField;
    SOOrder instance = (SOOrder) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (entityValueField1 != null)
      instance.OrderType = entityValueField1.Value;
    if (entityValueField2 != null)
      instance.OrderNbr = entityValueField2.Value;
    ((PXSelectBase<SOOrder>) graph1.Document).Current = ((PXSelectBase<SOOrder>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<SOOrder>(holdField1, graph1.putOnHold, graph1.releaseFromHold);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<SOOrder>(holdField2, (PXAction<SOOrder>) null, graph1.releaseFromCreditHold);
  }

  [FieldsProcessed(new string[] {"Hold", "CreditHold"})]
  protected virtual void SalesOrder_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOOrderEntry graph1 = (SOOrderEntry) graph;
    ((PXSelectBase) graph1.Document).View.Answer = (WebDialogResult) 6;
    EntityValueField holdField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    EntityValueField holdField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CreditHold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<SOOrder>(holdField1, graph1.putOnHold, graph1.releaseFromHold);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<SOOrder>(holdField2, (PXAction<SOOrder>) null, graph1.releaseFromCreditHold);
  }
}
