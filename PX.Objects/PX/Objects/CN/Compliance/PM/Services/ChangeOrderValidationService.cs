// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.Services.ChangeOrderValidationService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.Common.Extensions;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.Services;

public class ChangeOrderValidationService
{
  private readonly ChangeOrderEntry graph;
  private readonly PXSelectBase<ComplianceDocument> complianceDocumentView;

  public ChangeOrderValidationService(
    ChangeOrderEntry graph,
    PXSelectBase<ComplianceDocument> complianceDocumentView)
  {
    this.graph = graph;
    this.complianceDocumentView = complianceDocumentView;
  }

  public void ValidateComplianceFields(ComplianceDocument complianceDocument)
  {
    this.ValidateFieldOnLinkedToChangeOrder<ComplianceDocument.costTaskID>(complianceDocument, new Func<int?, bool>(this.IsTaskLinkedToChangeOrder), complianceDocument.CostTaskID, "Cost Task is not linked to the selected Change Order");
    this.ValidateFieldOnLinkedToChangeOrder<ComplianceDocument.revenueTaskID>(complianceDocument, new Func<int?, bool>(this.IsTaskLinkedToChangeOrder), complianceDocument.RevenueTaskID, "Revenue Task is not linked to the selected Change Order");
    this.ValidateFieldOnLinkedToChangeOrder<ComplianceDocument.costCodeID>(complianceDocument, new Func<int?, bool>(this.IsCostCodeLinkedToChangeOrder), complianceDocument.CostCodeID, "Cost Code is not linked to the selected Change Order");
  }

  private void ValidateFieldOnLinkedToChangeOrder<TField>(
    ComplianceDocument complianceDocument,
    Func<int?, bool> isFieldLinkedToChangeOrder,
    int? fieldId,
    string warningMessage)
    where TField : IBqlField
  {
    if (isFieldLinkedToChangeOrder(fieldId))
      ChangeOrderValidationService.RaiseExceptionForComplianceFields<TField>(((PXSelectBase) this.complianceDocumentView).Cache, complianceDocument, (object) fieldId, warningMessage);
    else
      ChangeOrderValidationService.RemoveErrorWarning<TField>(((PXSelectBase) this.complianceDocumentView).Cache, (object) complianceDocument, warningMessage);
  }

  private bool IsTaskLinkedToChangeOrder(int? taskId)
  {
    IEnumerable<PMChangeOrderBudget> orderBudgetLines = this.GetChangeOrderBudgetLines();
    IEnumerable<PMChangeOrderLine> changeOrderLines = this.GetChangeOrderLines();
    return taskId.HasValue && orderBudgetLines.All<PMChangeOrderBudget>((Func<PMChangeOrderBudget, bool>) (s =>
    {
      int? taskId1 = s.TaskID;
      int? nullable = taskId;
      return !(taskId1.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId1.HasValue == nullable.HasValue);
    })) && changeOrderLines.All<PMChangeOrderLine>((Func<PMChangeOrderLine, bool>) (s =>
    {
      int? taskId2 = s.TaskID;
      int? nullable = taskId;
      return !(taskId2.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId2.HasValue == nullable.HasValue);
    }));
  }

  private bool IsCostCodeLinkedToChangeOrder(int? costCodeId)
  {
    IEnumerable<PMChangeOrderBudget> orderBudgetLines = this.GetChangeOrderBudgetLines();
    IEnumerable<PMChangeOrderLine> changeOrderLines = this.GetChangeOrderLines();
    return costCodeId.HasValue && orderBudgetLines.All<PMChangeOrderBudget>((Func<PMChangeOrderBudget, bool>) (s =>
    {
      int? costCodeId1 = s.CostCodeID;
      int? nullable = costCodeId;
      return !(costCodeId1.GetValueOrDefault() == nullable.GetValueOrDefault() & costCodeId1.HasValue == nullable.HasValue);
    })) && changeOrderLines.All<PMChangeOrderLine>((Func<PMChangeOrderLine, bool>) (s =>
    {
      int? costCodeId2 = s.CostCodeID;
      int? nullable = costCodeId;
      return !(costCodeId2.GetValueOrDefault() == nullable.GetValueOrDefault() & costCodeId2.HasValue == nullable.HasValue);
    }));
  }

  private IEnumerable<PMChangeOrderBudget> GetChangeOrderBudgetLines()
  {
    return ((PXSelectBase<PMChangeOrderBudget>) new PXSelect<PMChangeOrderBudget, Where<PMChangeOrderBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>>>((PXGraph) this.graph)).Select(Array.Empty<object>()).FirstTableItems;
  }

  private IEnumerable<PMChangeOrderLine> GetChangeOrderLines()
  {
    return ((PXSelectBase<PMChangeOrderLine>) new PXSelect<PMChangeOrderLine, Where<PMChangeOrderLine.refNbr, Equal<Current<PMChangeOrder.refNbr>>>>((PXGraph) this.graph)).Select(Array.Empty<object>()).FirstTableItems;
  }

  private static void RaiseExceptionForComplianceFields<TField>(
    PXCache cache,
    ComplianceDocument document,
    object fieldValue,
    string warningMessage)
    where TField : IBqlField
  {
    PXSetPropertyException<TField> propertyException = new PXSetPropertyException<TField>(warningMessage, (PXErrorLevel) 2);
    cache.RaiseExceptionHandling<TField>((object) document, fieldValue, (Exception) propertyException);
  }

  private static void RemoveErrorWarning<TField>(PXCache cache, object entity, string errorMessage) where TField : IBqlField
  {
    string field = cache.GetField(typeof (TField));
    if (!cache.GetAttributes(entity, field).OfType<IPXInterfaceField>().Any<IPXInterfaceField>((Func<IPXInterfaceField, bool>) (x => x.ErrorText == errorMessage)))
      return;
    cache.ClearFieldErrors<TField>(entity);
  }
}
