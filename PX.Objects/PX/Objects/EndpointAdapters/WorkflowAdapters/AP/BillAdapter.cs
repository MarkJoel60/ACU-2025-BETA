// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.AP.BillAdapter
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
internal class BillAdapter
{
  [FieldsProcessed(new string[] {"Type", "ReferenceNbr", "Hold"})]
  protected void Bill_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    APInvoiceEntry graph1 = (APInvoiceEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) as EntityValueField;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    APInvoice instance = (APInvoice) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (entityValueField1 == null)
      ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<APRegister.docType>((object) instance);
    else
      ((PXGraph) graph1).SetDropDownValue<APInvoice.docType, APInvoice>(entityValueField1.Value, (object) instance);
    if (entityValueField2 == null)
      ((PXSelectBase) graph1.Document).Cache.SetDefaultExt<APInvoice.refNbr>((object) instance);
    else
      ((PXSelectBase) graph1.Document).Cache.SetValueExt<APInvoice.refNbr>((object) instance, (object) entityValueField2.Value);
    ((PXSelectBase<APInvoice>) graph1.Document).Current = ((PXSelectBase<APInvoice>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<APInvoice>(holdField, graph1.putOnHold, graph1.releaseFromHold, new Action<PXCache<APInvoice>>(this.SupressErrors));
  }

  [FieldsProcessed(new string[] {"Hold"})]
  protected void Bill_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    APInvoiceEntry graph1 = (APInvoiceEntry) graph;
    EntityValueField holdField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<APInvoice>(holdField, graph1.putOnHold, graph1.releaseFromHold, new Action<PXCache<APInvoice>>(this.SupressErrors));
  }

  private void SupressErrors(PXCache<APInvoice> bill)
  {
    ((PXCache) bill).RaiseExceptionHandling<APRegister.curyOrigDocAmt>(((PXCache) bill).Current, (object) ((APRegister) ((PXCache) bill).Current).CuryOrigDocAmt, (Exception) null);
  }

  protected void Action_ReleaseBill(PXGraph graph, ActionImpl action)
  {
    APInvoiceEntry apInvoiceEntry = (APInvoiceEntry) graph;
    ((PXAction) apInvoiceEntry.Save).Press();
    ((PXAction) apInvoiceEntry.release).Press();
  }
}
