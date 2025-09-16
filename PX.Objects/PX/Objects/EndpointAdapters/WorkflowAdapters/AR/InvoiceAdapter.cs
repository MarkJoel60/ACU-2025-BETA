// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.AR.InvoiceAdapter
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
internal class InvoiceAdapter
{
  [FieldsProcessed(new string[] {"Type", "ReferenceNbr", "Hold"})]
  protected void Invoice_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    ARInvoiceEntry graph1 = (ARInvoiceEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    if (string.IsNullOrEmpty(entityValueField2?.Value) || ((PXSelectBase<ARInvoice>) graph1.Document).Current?.RefNbr != entityValueField2?.Value)
    {
      ARInvoice instance = (ARInvoice) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
      if (entityValueField1 == null)
        ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<ARRegister.docType>((object) instance);
      else
        ((PXGraph) graph1).SetDropDownValue<ARInvoice.docType, ARInvoice>(entityValueField1.Value, (object) instance);
      if (entityValueField2 == null)
        ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<ARInvoice.refNbr>((object) instance);
      else
        ((PXSelectBase) graph1.Document).Cache.SetValueExt<ARInvoice.refNbr>((object) instance, (object) entityValueField2.Value);
      ((PXSelectBase<ARInvoice>) graph1.Document).Current = ((PXSelectBase<ARInvoice>) graph1.Document).Insert(instance);
    }
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<ARInvoice>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected void Invoice_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    ARInvoiceEntry graph1 = (ARInvoiceEntry) graph;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<ARInvoice>(holdField, graph1.putOnHold, graph1.releaseFromHold);
  }

  protected void Action_ReleaseInvoice(PXGraph graph, ActionImpl action)
  {
    ARInvoiceEntry arInvoiceEntry = (ARInvoiceEntry) graph;
    ((PXAction) arInvoiceEntry.Save).Press();
    ((PXAction) arInvoiceEntry.release).Press();
  }
}
