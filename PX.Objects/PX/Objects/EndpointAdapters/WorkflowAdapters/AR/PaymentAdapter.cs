// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.AR.PaymentAdapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters.WorkflowAdapters.AR;

[PXVersion("20.200.001", "Default")]
[PXVersion("22.200.001", "Default")]
[PXVersion("23.200.001", "Default")]
[PXVersion("24.200.001", "Default")]
[PXVersion("25.200.001", "Default")]
internal class PaymentAdapter
{
  /// <summary>Makes Branch value to be set first on AR Payment</summary>
  [FieldsProcessed(new string[] {"Type", "ReferenceNbr", "Branch", "Hold"})]
  protected void Payment_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    ARPaymentEntry graph1 = (ARPaymentEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    EntityValueField entityValueField3 = targetEntity.Fields.OfType<EntityValueField>().SingleOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "Branch"));
    ARPayment instance = (ARPayment) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (entityValueField1 == null)
      ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<ARRegister.docType>((object) instance);
    else
      ((PXGraph) graph1).SetDropDownValue<ARPayment.docType, ARPayment>(entityValueField1.Value, (object) instance);
    if (entityValueField2 == null)
      ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<ARPayment.refNbr>((object) instance);
    else
      ((PXSelectBase) graph1.Document).Cache.SetValueExt<ARPayment.refNbr>((object) instance, (object) entityValueField2.Value);
    if (entityValueField3 == null || entityValueField3.Value == null)
      ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<ARPayment.branchID>((object) instance);
    else
      ((PXSelectBase<ARPayment>) graph1.Document).SetValueExt<ARPayment.branchID>(instance, (object) entityValueField3.Value);
    ((PXSelectBase<ARPayment>) graph1.Document).Current = ((PXSelectBase<ARPayment>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<ARPayment>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected void Payment_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    ARPaymentEntry graph1 = (ARPaymentEntry) graph;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<ARPayment>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  protected void Action_ReleasePayment(PXGraph graph, ActionImpl action)
  {
    ARPaymentEntry arPaymentEntry = (ARPaymentEntry) graph;
    ((PXAction) arPaymentEntry.Save).Press();
    ((PXAction) arPaymentEntry.release).Press();
  }
}
