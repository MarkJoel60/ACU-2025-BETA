// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.AP.CheckAdapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.AP;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters.WorkflowAdapters.AP;

[PXVersion("20.200.001", "Default")]
[PXVersion("22.200.001", "Default")]
[PXVersion("23.200.001", "Default")]
[PXVersion("24.200.001", "Default")]
[PXVersion("25.200.001", "Default")]
internal class CheckAdapter
{
  [FieldsProcessed(new string[] {"Type", "ReferenceNbr", "Hold"})]
  protected void Check_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    APPaymentEntry graph1 = (APPaymentEntry) graph;
    ((PXSelectBase) graph1.Document).Cache.Current = (object) null;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    APPayment instance = (APPayment) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (entityValueField1 == null)
      ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<APRegister.docType>((object) instance);
    else
      ((PXGraph) graph1).SetDropDownValue<APPayment.docType, APPayment>(entityValueField1.Value, (object) instance);
    if (entityValueField2 == null)
      ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<APPayment.refNbr>((object) instance);
    else
      ((PXSelectBase) graph1.Document).Cache.SetValueExt<APPayment.refNbr>((object) instance, (object) entityValueField2.Value);
    ((PXSelectBase<APPayment>) graph1.Document).Current = ((PXSelectBase<APPayment>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<APPayment>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected void Check_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    APPaymentEntry graph1 = (APPaymentEntry) graph;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<APPayment>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  protected void Action_ReleaseCheck(PXGraph graph, ActionImpl action)
  {
    APPaymentEntry apPaymentEntry = (APPaymentEntry) graph;
    ((PXGraph) apPaymentEntry).Views[((PXGraph) apPaymentEntry).PrimaryView].Answer = (WebDialogResult) 6;
    ((PXAction) apPaymentEntry.Save).Press();
    ((PXAction) apPaymentEntry.release).Press();
  }

  protected void Action_VoidCheck(PXGraph graph, ActionImpl action)
  {
    APPaymentEntry apPaymentEntry = (APPaymentEntry) graph;
    ((PXAction) apPaymentEntry.Save).Press();
    ((PXAction) apPaymentEntry.voidCheck).Press();
  }
}
