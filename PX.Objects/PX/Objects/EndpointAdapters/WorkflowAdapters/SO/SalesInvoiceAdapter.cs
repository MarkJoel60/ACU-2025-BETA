// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.WorkflowAdapters.SO.SalesInvoiceAdapter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.AR;
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
internal class SalesInvoiceAdapter
{
  [FieldsProcessed(new string[] {"Type", "ReferenceNbr", "Hold", "CreditHold"})]
  protected virtual void SalesInvoice_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOInvoiceEntry graph1 = (SOInvoiceEntry) graph;
    EntityValueField typeField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Type")) as EntityValueField;
    EntityValueField entityValueField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) as EntityValueField;
    EntityValueField holdField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    EntityValueField holdField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CreditHold")) as EntityValueField;
    PX.Objects.AR.ARInvoice instance = (PX.Objects.AR.ARInvoice) ((PXSelectBase) graph1.Document).Cache.CreateInstance();
    if (typeField != null)
    {
      ARInvoiceType.ListAttribute listAttribute = new ARInvoiceType.ListAttribute();
      if (listAttribute.ValueLabelDic.ContainsValue(typeField.Value))
        instance.DocType = listAttribute.ValueLabelDic.First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Value == typeField.Value)).Key;
      else
        instance.DocType = typeField.Value;
    }
    if (entityValueField != null)
    {
      object obj = (object) entityValueField.Value;
      if (((PXSelectBase) graph1.Document).Cache.GetStateExt<PX.Objects.AR.ARInvoice.refNbr>((object) null) as PXFieldState is PXStringState stateExt)
      {
        string inputMask = stateExt.InputMask;
        if ((inputMask != null ? (inputMask.Length > 0 ? 1 : 0) : 0) != 0)
        {
          if (stateExt.InputMask.StartsWith(">"))
            obj = (object) entityValueField.Value.ToUpper();
          else if (stateExt.InputMask.StartsWith("<"))
            obj = (object) entityValueField.Value.ToLower();
        }
      }
      ((PXSelectBase) graph1.Document).Cache.RaiseFieldUpdating<PX.Objects.AR.ARInvoice.refNbr>((object) instance, ref obj);
      instance.RefNbr = (string) obj;
    }
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph1.Document).Current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph1.Document).Insert(instance);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<PX.Objects.AR.ARInvoice>(holdField1, graph1.putOnHold, graph1.releaseFromHold);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<PX.Objects.AR.ARInvoice>(holdField2, graph1.putOnCreditHold, graph1.releaseFromCreditHold);
  }

  [FieldsProcessed(new string[] {"Hold", "CreditHold"})]
  protected virtual void SalesInvoice_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOInvoiceEntry graph1 = (SOInvoiceEntry) graph;
    ((PXSelectBase) graph1.Document).View.Answer = (WebDialogResult) 6;
    EntityValueField holdField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Hold")) as EntityValueField;
    EntityValueField holdField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CreditHold")) as EntityValueField;
    if (holdField1 != null && holdField1.Value != null)
    {
      ((PXSelectBase) graph1.Document).Cache.Update((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph1.Document).Current);
      ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<PX.Objects.AR.ARInvoice>(holdField1, graph1.putOnHold, graph1.releaseFromHold);
    }
    if (holdField2 == null || holdField2.Value == null)
      return;
    ((PXSelectBase) graph1.Document).Cache.Update((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph1.Document).Current);
    ((PXGraph) graph1).SubscribeToPersistDependingOnBoolField<PX.Objects.AR.ARInvoice>(holdField2, graph1.putOnCreditHold, graph1.releaseFromCreditHold);
  }
}
