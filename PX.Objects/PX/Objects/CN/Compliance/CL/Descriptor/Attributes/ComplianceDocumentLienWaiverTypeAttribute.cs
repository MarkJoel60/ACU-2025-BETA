// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentLienWaiverTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.PM;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentLienWaiverTypeAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber
{
  public void RowPersisting(PXCache cache, PXRowPersistingEventArgs args)
  {
    if (!(args.Row is ComplianceDocument row) || args.Operation == 3)
      return;
    int? waiverDocumentTypeId = ComplianceDocumentLienWaiverTypeAttribute.GetLienWaiverDocumentTypeId(cache.Graph);
    int? nullable1;
    if (waiverDocumentTypeId.HasValue)
    {
      nullable1 = row.DocumentType;
      int? nullable2 = waiverDocumentTypeId;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return;
    }
    int? nullable3 = row.ProjectID;
    if (!nullable3.HasValue)
      cache.RaiseExceptionHandling<ComplianceDocument.projectID>((object) row, (object) row.ProjectID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<ComplianceDocument.projectID>(cache)
      }));
    nullable3 = row.VendorID;
    if (!nullable3.HasValue)
      cache.RaiseExceptionHandling<ComplianceDocument.vendorID>((object) row, (object) row.VendorID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<ComplianceDocument.vendorID>(cache)
      }));
    nullable3 = row.DocumentTypeValue;
    if (!nullable3.HasValue)
      cache.RaiseExceptionHandling<ComplianceDocument.documentTypeValue>((object) row, (object) row.DocumentTypeValue, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<ComplianceDocument.documentTypeValue>(cache)
      }));
    if (!(cache.GetOriginal(args.Row) is ComplianceDocument original))
      return;
    nullable3 = original.ProjectID;
    nullable1 = row.ProjectID;
    if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
    {
      nullable1 = original.CostTaskID;
      nullable3 = row.CostTaskID;
      if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
      {
        nullable3 = original.DocumentTypeValue;
        nullable1 = row.DocumentTypeValue;
        if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue && !(original.Subcontract != row.Subcontract))
        {
          Guid? purchaseOrder1 = original.PurchaseOrder;
          Guid? purchaseOrder2 = row.PurchaseOrder;
          if ((purchaseOrder1.HasValue == purchaseOrder2.HasValue ? (purchaseOrder1.HasValue ? (purchaseOrder1.GetValueOrDefault() != purchaseOrder2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            return;
        }
      }
    }
    ComplianceDocumentLienWaiverTypeAttribute.ValidateNoFinalLienWaiverExists(cache, row);
  }

  private static POOrder GetPurchaseOrder(PXCache cache, Guid? complianceDocumentRefId)
  {
    ComplianceDocumentReference documentReference = ((PXSelectBase<ComplianceDocumentReference>) new FbqlSelect<SelectFromBase<ComplianceDocumentReference, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ComplianceDocumentReference.complianceDocumentReferenceId, IBqlGuid>.IsEqual<P.AsGuid>>, ComplianceDocumentReference>.View(cache.Graph)).SelectSingle(new object[1]
    {
      (object) complianceDocumentRefId
    });
    return ((PXSelectBase<POOrder>) new FbqlSelect<SelectFromBase<POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrder.orderType, Equal<P.AsString>>>>>.And<BqlOperand<POOrder.orderNbr, IBqlString>.IsEqual<P.AsString>>>, POOrder>.View(cache.Graph)).SelectSingle(new object[2]
    {
      (object) documentReference.Type,
      (object) documentReference.ReferenceNumber
    });
  }

  /// <summary>
  /// Validates that no final lien waiver exists under the current grouping condition.
  /// </summary>
  /// <param name="cache">A cache for execution of select queries.</param>
  /// <param name="complianceDocument">A lien waiver to validate.</param>
  /// <exception cref="T:PX.Data.PXSetPropertyException`1">An exception that shows that a final lien waiver exists for the current grouping condition.</exception>
  public static void ValidateNoFinalLienWaiverExists(
    PXCache cache,
    ComplianceDocument complianceDocument)
  {
    int? waiverDocumentTypeId = ComplianceDocumentLienWaiverTypeAttribute.GetLienWaiverDocumentTypeId(cache.Graph);
    int? conditionalPartialTypeId = ComplianceDocumentLienWaiverTypeAttribute.GetLienWaiverConditionalPartialTypeId(cache.Graph);
    int? conditionalFinalTypeId = ComplianceDocumentLienWaiverTypeAttribute.GetLienWaiverConditionalFinalTypeId(cache.Graph);
    int? unconditionalPartialTypeId = ComplianceDocumentLienWaiverTypeAttribute.GetLienWaiverUnconditionalPartialTypeId(cache.Graph);
    int? unconditionalFinalTypeId = ComplianceDocumentLienWaiverTypeAttribute.GetLienWaiverUnconditionalFinalTypeId(cache.Graph);
    LienWaiverSetup lienWaiverSetup = ((PXSelectBase<LienWaiverSetup>) new PXViewOf<LienWaiverSetup>.BasedOn<SelectFromBase<LienWaiverSetup, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly(cache.Graph)).SelectSingle(Array.Empty<object>());
    int? documentTypeValue = complianceDocument.DocumentTypeValue;
    int? nullable1 = conditionalPartialTypeId;
    string str;
    int? nullable2;
    if (!(documentTypeValue.GetValueOrDefault() == nullable1.GetValueOrDefault() & documentTypeValue.HasValue == nullable1.HasValue))
    {
      int? nullable3 = complianceDocument.DocumentTypeValue;
      int? nullable4 = conditionalFinalTypeId;
      if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
      {
        int? nullable5 = complianceDocument.DocumentTypeValue;
        nullable3 = unconditionalPartialTypeId;
        if (!(nullable5.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable5.HasValue == nullable3.HasValue))
        {
          nullable3 = complianceDocument.DocumentTypeValue;
          nullable5 = unconditionalFinalTypeId;
          if (!(nullable3.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable3.HasValue == nullable5.HasValue))
            return;
        }
        str = lienWaiverSetup.GroupByUnconditional;
        nullable2 = unconditionalFinalTypeId;
        goto label_7;
      }
    }
    str = lienWaiverSetup.GroupByConditional;
    nullable2 = conditionalFinalTypeId;
label_7:
    FbqlSelect<SelectFromBase<ComplianceDocument, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceDocument.documentType, Equal<P.AsInt>>>>, And<BqlOperand<ComplianceDocument.documentTypeValue, IBqlInt>.IsEqual<P.AsInt>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofGuid>, IsNull>>>>.Or<BqlOperand<ComplianceDocument.noteID, IBqlGuid>.IsNotEqual<P.AsGuid>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceDocument.isVoided, Equal<False>>>>>.Or<BqlOperand<ComplianceDocument.isVoided, IBqlBool>.IsNull>>>>>.And<BqlOperand<ComplianceDocument.projectID, IBqlInt>.IsEqual<P.AsInt>>>, ComplianceDocument>.View view = new FbqlSelect<SelectFromBase<ComplianceDocument, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceDocument.documentType, Equal<P.AsInt>>>>, And<BqlOperand<ComplianceDocument.documentTypeValue, IBqlInt>.IsEqual<P.AsInt>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofGuid>, IsNull>>>>.Or<BqlOperand<ComplianceDocument.noteID, IBqlGuid>.IsNotEqual<P.AsGuid>>>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceDocument.isVoided, Equal<False>>>>>.Or<BqlOperand<ComplianceDocument.isVoided, IBqlBool>.IsNull>>>>>.And<BqlOperand<ComplianceDocument.projectID, IBqlInt>.IsEqual<P.AsInt>>>, ComplianceDocument>.View(cache.Graph);
    switch (str)
    {
      case "P":
        if (((PXSelectBase<ComplianceDocument>) view).SelectSingle(new object[5]
        {
          (object) waiverDocumentTypeId,
          (object) nullable2,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.ProjectID
        }) == null)
          break;
        PXTrace.WriteError("The final lien waiver already exists for the combination: {0}, {1}, {2}.", new object[3]
        {
          (object) PMProject.PK.Find(cache.Graph, complianceDocument.ProjectID).ContractCD,
          (object) string.Empty,
          (object) string.Empty
        });
        throw new PXSetPropertyException<ComplianceDocument.documentTypeValue>("The final lien waiver already exists for the following combination of calculation parameters: {0}. For details, see the trace log.", new object[1]
        {
          (object) "Project"
        });
      case "PT":
        ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.costTaskID, IBqlInt>.IsEqual<P.AsInt>>>();
        if (((PXSelectBase<ComplianceDocument>) view).SelectSingle(new object[6]
        {
          (object) waiverDocumentTypeId,
          (object) nullable2,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.ProjectID,
          (object) complianceDocument.CostTaskID
        }) == null)
          break;
        PXTrace.WriteError("The final lien waiver already exists for the combination: {0}, {1}, {2}.", new object[3]
        {
          (object) PMProject.PK.Find(cache.Graph, complianceDocument.ProjectID).ContractCD,
          (object) PMTask.PK.Find(cache.Graph, complianceDocument.ProjectID, complianceDocument.CostTaskID).TaskCD,
          (object) string.Empty
        });
        throw new PXSetPropertyException<ComplianceDocument.documentTypeValue>("The final lien waiver already exists for the following combination of calculation parameters: {0}. For details, see the trace log.", new object[1]
        {
          (object) "Project, Project Task"
        });
      case "CP":
        if (complianceDocument.PurchaseOrder.HasValue)
        {
          POOrder purchaseOrder = ComplianceDocumentLienWaiverTypeAttribute.GetPurchaseOrder(cache, complianceDocument.PurchaseOrder);
          ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<Exists<SelectFromBase<ComplianceDocumentReference, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.purchaseOrder>>>>, And<BqlOperand<ComplianceDocumentReference.type, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<ComplianceDocumentReference.referenceNumber, IBqlString>.IsEqual<P.AsString>>>>>>();
          if (((PXSelectBase<ComplianceDocument>) view).SelectSingle(new object[7]
          {
            (object) waiverDocumentTypeId,
            (object) nullable2,
            (object) complianceDocument.NoteID,
            (object) complianceDocument.NoteID,
            (object) complianceDocument.ProjectID,
            (object) purchaseOrder.OrderType,
            (object) purchaseOrder.OrderNbr
          }) == null)
            break;
          PMProject pmProject = PMProject.PK.Find(cache.Graph, complianceDocument.ProjectID);
          PXTrace.WriteError("The final lien waiver already exists for the combination: {0}, {1}, {2}.", new object[3]
          {
            (object) purchaseOrder.OrderNbr,
            (object) pmProject.ContractCD,
            (object) string.Empty
          });
          throw new PXSetPropertyException<ComplianceDocument.documentTypeValue>("The final lien waiver already exists for the following combination of calculation parameters: {0}. For details, see the trace log.", new object[1]
          {
            (object) "Commitment, Project"
          });
        }
        if (complianceDocument.Subcontract == null)
          break;
        ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.subcontract, IBqlString>.IsEqual<P.AsString>>>();
        if (((PXSelectBase<ComplianceDocument>) view).SelectSingle(new object[6]
        {
          (object) waiverDocumentTypeId,
          (object) nullable2,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.ProjectID,
          (object) complianceDocument.Subcontract
        }) == null)
          break;
        PXTrace.WriteError("The final lien waiver already exists for the combination: {0}, {1}, {2}.", new object[3]
        {
          (object) POOrder.PK.Find(cache.Graph, "RS", complianceDocument.Subcontract).OrderNbr,
          (object) PMProject.PK.Find(cache.Graph, complianceDocument.ProjectID).ContractCD,
          (object) string.Empty
        });
        throw new PXSetPropertyException<ComplianceDocument.documentTypeValue>("The final lien waiver already exists for the following combination of calculation parameters: {0}. For details, see the trace log.", new object[1]
        {
          (object) "Commitment, Project"
        });
      case "CPT":
        if (complianceDocument.PurchaseOrder.HasValue)
        {
          POOrder purchaseOrder = ComplianceDocumentLienWaiverTypeAttribute.GetPurchaseOrder(cache, complianceDocument.PurchaseOrder);
          ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.costTaskID, IBqlInt>.IsEqual<P.AsInt>>>();
          ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<Exists<SelectFromBase<ComplianceDocumentReference, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.purchaseOrder>>>>, And<BqlOperand<ComplianceDocumentReference.type, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<ComplianceDocumentReference.referenceNumber, IBqlString>.IsEqual<P.AsString>>>>>>();
          if (((PXSelectBase<ComplianceDocument>) view).SelectSingle(new object[8]
          {
            (object) waiverDocumentTypeId,
            (object) nullable2,
            (object) complianceDocument.NoteID,
            (object) complianceDocument.NoteID,
            (object) complianceDocument.ProjectID,
            (object) complianceDocument.CostTaskID,
            (object) purchaseOrder.OrderType,
            (object) purchaseOrder.OrderNbr
          }) == null)
            break;
          PMProject pmProject = PMProject.PK.Find(cache.Graph, complianceDocument.ProjectID);
          PMTask pmTask = PMTask.PK.Find(cache.Graph, complianceDocument.ProjectID, complianceDocument.CostTaskID);
          PXTrace.WriteError("The final lien waiver already exists for the combination: {0}, {1}, {2}.", new object[3]
          {
            (object) purchaseOrder.OrderNbr,
            (object) pmProject.ContractCD,
            (object) pmTask.TaskCD
          });
          throw new PXSetPropertyException<ComplianceDocument.documentTypeValue>("The final lien waiver already exists for the following combination of calculation parameters: {0}. For details, see the trace log.", new object[1]
          {
            (object) "Commitment, Project, Project Task"
          });
        }
        if (complianceDocument.Subcontract == null)
          break;
        ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.costTaskID, IBqlInt>.IsEqual<P.AsInt>>>();
        ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.subcontract, IBqlString>.IsEqual<P.AsString>>>();
        if (((PXSelectBase<ComplianceDocument>) view).SelectSingle(new object[7]
        {
          (object) waiverDocumentTypeId,
          (object) nullable2,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.NoteID,
          (object) complianceDocument.ProjectID,
          (object) complianceDocument.CostTaskID,
          (object) complianceDocument.Subcontract
        }) == null)
          break;
        PXTrace.WriteError("The final lien waiver already exists for the combination: {0}, {1}, {2}.", new object[3]
        {
          (object) POOrder.PK.Find(cache.Graph, "RS", complianceDocument.Subcontract).OrderNbr,
          (object) PMProject.PK.Find(cache.Graph, complianceDocument.ProjectID).ContractCD,
          (object) PMTask.PK.Find(cache.Graph, complianceDocument.ProjectID, complianceDocument.CostTaskID).TaskCD
        });
        throw new PXSetPropertyException<ComplianceDocument.documentTypeValue>("The final lien waiver already exists for the following combination of calculation parameters: {0}. For details, see the trace log.", new object[1]
        {
          (object) "Commitment, Project, Project Task"
        });
    }
  }

  private static int? GetComplianceDocumentTypeId(PXGraph graph, string type)
  {
    return ((PXSelectBase<ComplianceAttributeType>) new FbqlSelect<SelectFromBase<ComplianceAttributeType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ComplianceAttributeType.type, IBqlString>.IsEqual<P.AsString>>, ComplianceAttributeType>.View(graph)).SelectSingle(new object[1]
    {
      (object) type
    })?.ComplianceAttributeTypeID;
  }

  private static int? GetComplianceDocumentTypeValueId(
    PXGraph graph,
    string type,
    string typeValue)
  {
    return ((PXSelectBase<ComplianceAttribute>) new FbqlSelect<SelectFromBase<ComplianceAttribute, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ComplianceAttributeType>.On<BqlOperand<ComplianceAttributeType.complianceAttributeTypeID, IBqlInt>.IsEqual<ComplianceAttribute.type>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceAttributeType.type, Equal<P.AsString>>>>>.And<BqlOperand<ComplianceAttribute.value, IBqlString>.IsEqual<P.AsString>>>, ComplianceAttribute>.View(graph)).SelectSingle(new object[2]
    {
      (object) type,
      (object) typeValue
    })?.AttributeId;
  }

  private static int? GetLienWaiverDocumentTypeId(PXGraph graph)
  {
    return ComplianceDocumentLienWaiverTypeAttribute.GetComplianceDocumentTypeId(graph, "Lien Waiver");
  }

  private static int? GetLienWaiverConditionalPartialTypeId(PXGraph graph)
  {
    return ComplianceDocumentLienWaiverTypeAttribute.GetComplianceDocumentTypeValueId(graph, "Lien Waiver", "Conditional Partial");
  }

  private static int? GetLienWaiverUnconditionalPartialTypeId(PXGraph graph)
  {
    return ComplianceDocumentLienWaiverTypeAttribute.GetComplianceDocumentTypeValueId(graph, "Lien Waiver", "Unconditional Partial");
  }

  private static int? GetLienWaiverConditionalFinalTypeId(PXGraph graph)
  {
    return ComplianceDocumentLienWaiverTypeAttribute.GetComplianceDocumentTypeValueId(graph, "Lien Waiver", "Conditional Final");
  }

  private static int? GetLienWaiverUnconditionalFinalTypeId(PXGraph graph)
  {
    return ComplianceDocumentLienWaiverTypeAttribute.GetComplianceDocumentTypeValueId(graph, "Lien Waiver", "Unconditional Final");
  }
}
