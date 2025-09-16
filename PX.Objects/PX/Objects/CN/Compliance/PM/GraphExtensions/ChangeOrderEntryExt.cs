// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.GraphExtensions.ChangeOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CN.Compliance.PM.CacheExtensions;
using PX.Objects.CN.Compliance.PM.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.GraphExtensions;

public class ChangeOrderEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<ChangeOrderEntry, PMChangeOrder>, ChangeOrderEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocument, Where<ComplianceDocument.changeOrderNumber, Equal<Current<PMChangeOrder.refNbr>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  public PXSetup<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> LienWaiverSetup;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService complianceDocumentService;
  private ChangeOrderValidationService changeOrderValidationService;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize()
  {
    this.ValidateComplianceSetup();
    this.complianceDocumentService = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<ChangeOrderEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.complianceDocumentService.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.complianceDocumentService.AddExpirationDateEventHandlers();
    this.changeOrderValidationService = new ChangeOrderValidationService(((PXGraphExtension<ChangeOrderEntry>) this).Base, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments);
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForChangeOrder(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  private void ValidateComplianceSetup()
  {
    if (((PXSelectBase<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>) this.LienWaiverSetup).Current == null)
      throw new PXSetupNotEnteredException<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>();
  }

  public IEnumerable complianceDocuments()
  {
    List<ComplianceDocument> list = this.GetComplianceDocuments().ToList<ComplianceDocument>();
    this.complianceDocumentService.ValidateComplianceDocuments((PXCache) null, (IEnumerable<ComplianceDocument>) list, ((PXSelectBase) this.ComplianceDocuments).Cache);
    return (IEnumerable) list;
  }

  public virtual void _(PX.Data.Events.RowUpdated<ComplianceDocument> args)
  {
    ((PXSelectBase) this.ComplianceDocuments).View.RequestRefresh();
  }

  [PXMergeAttributes]
  [PXDBInt]
  [PXDimensionSelector("PROTASK", typeof (Search5<PMTask.taskID, LeftJoin<PMChangeOrderLine, On<PMChangeOrderLine.projectID, Equal<PMTask.projectID>, And<PMChangeOrderLine.taskID, Equal<PMTask.taskID>>>, LeftJoin<PMChangeOrderBudget, On<PMChangeOrderBudget.projectID, Equal<PMTask.projectID>, And<PMChangeOrderBudget.projectTaskID, Equal<PMTask.taskID>>>>>, Where2<Where<PMChangeOrderLine.refNbr, Equal<Current<PMChangeOrder.refNbr>>, Or<PMChangeOrderBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>>>, And<PMTask.type, NotEqual<ProjectTaskType.revenue>>>, Aggregate<GroupBy<PMTask.taskID>>>), typeof (PMTask.taskCD), DescriptionField = typeof (PMTask.description))]
  [PXUIField]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ComplianceDocument.costTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [PXDimensionSelector("PROTASK", typeof (Search5<PMTask.taskID, LeftJoin<PMChangeOrderLine, On<PMChangeOrderLine.projectID, Equal<PMTask.projectID>, And<PMChangeOrderLine.taskID, Equal<PMTask.taskID>>>, LeftJoin<PMChangeOrderBudget, On<PMChangeOrderBudget.projectID, Equal<PMTask.projectID>, And<PMChangeOrderBudget.projectTaskID, Equal<PMTask.taskID>>>>>, Where2<Where<PMChangeOrderLine.refNbr, Equal<Current<PMChangeOrder.refNbr>>, Or<PMChangeOrderBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>>>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>, Aggregate<GroupBy<PMTask.taskID>>>), typeof (PMTask.taskCD), DescriptionField = typeof (PMTask.description))]
  [PXUIField]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ComplianceDocument.revenueTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [CostCodeDimensionSelector(typeof (Search5<PMCostCode.costCodeID, LeftJoin<PMChangeOrderLine, On<PMChangeOrderLine.costCodeID, Equal<PMCostCode.costCodeID>>, LeftJoin<PMChangeOrderBudget, On<PMChangeOrderBudget.costCodeID, Equal<PMCostCode.costCodeID>>>>, Where<PMChangeOrderLine.refNbr, Equal<Current<PMChangeOrder.refNbr>>, Or<PMChangeOrderBudget.refNbr, Equal<Current<PMChangeOrder.refNbr>>>>, Aggregate<GroupBy<PMCostCode.costCodeID>>>))]
  [PXUIField]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ComplianceDocument.costCodeID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrder> args)
  {
    PMChangeOrder row = args.Row;
    if (row == null)
      return;
    this.complianceDocumentService.ValidateRelatedProjectField<PMChangeOrder, PMChangeOrder.projectID>(row, (object) row.ProjectID);
  }

  protected virtual void PMChangeOrder_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs args,
    PXRowSelected baseHandler)
  {
    if (!(args.Row is PMChangeOrder))
      return;
    baseHandler.Invoke(cache, args);
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<ChangeOrderEntry>) this).Base.Document).Cache.Inserted);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    ComplianceDocument row = args.Row;
    if (row == null)
      return;
    this.complianceDocumentService.UpdateExpirationIndicator(args.Row);
    this.changeOrderValidationService.ValidateComplianceFields(row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrderLine> args)
  {
    if (args.Row == null)
      return;
    this.ValidateChangeOrderLine(args.Row);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<PMChangeOrderRevenueBudget> args)
  {
    if (args.Row == null)
      return;
    this.ValidateChangeOrderRevenueBudget(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrderCostBudget> args)
  {
    if (args.Row == null)
      return;
    this.ValidateChangeOrderCostBudget(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    PMChangeOrder current = ((PXSelectBase<PMChangeOrder>) ((PXGraphExtension<ChangeOrderEntry>) this).Base.Document).Current;
    ComplianceDocument row = args.Row;
    if (current == null || row == null)
      return;
    this.FillChangeOrderInfo(row, current);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PMChangeOrder> args)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.complianceDocumentService.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PMChangeOrder>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMChangeOrder> args)
  {
    if (args.Row == null)
      return;
    EnumerableExtensions.ForEach<ComplianceDocument>(this.GetComplianceDocuments(), new Action<ComplianceDocument>(this.RemoveComplianceReference));
  }

  private void RemoveComplianceReference(ComplianceDocument document)
  {
    document.ChangeOrderNumber = (string) null;
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(document);
  }

  private void FillChangeOrderInfo(ComplianceDocument complianceDocument, PMChangeOrder changeOrder)
  {
    complianceDocument.ProjectID = changeOrder.ProjectID;
    complianceDocument.CustomerID = changeOrder.CustomerID;
    complianceDocument.CustomerName = this.GetCustomerName(changeOrder.CustomerID);
    complianceDocument.ChangeOrderNumber = changeOrder.RefNbr;
    this.FillAdditionalChangeOrderInfo(complianceDocument);
  }

  private void FillAdditionalChangeOrderInfo(ComplianceDocument complianceDocument)
  {
    List<PMChangeOrderRevenueBudget> list1 = ((PXSelectBase<PMChangeOrderRevenueBudget>) ((PXGraphExtension<ChangeOrderEntry>) this).Base.RevenueBudget).Select(Array.Empty<object>()).FirstTableItems.ToList<PMChangeOrderRevenueBudget>();
    List<PMChangeOrderLine> list2 = ((PXSelectBase<PMChangeOrderLine>) ((PXGraphExtension<ChangeOrderEntry>) this).Base.Details).Select(Array.Empty<object>()).FirstTableItems.ToList<PMChangeOrderLine>();
    List<PMChangeOrderCostBudget> list3 = ((PXSelectBase<PMChangeOrderCostBudget>) ((PXGraphExtension<ChangeOrderEntry>) this).Base.CostBudget).Select(Array.Empty<object>()).FirstTableItems.ToList<PMChangeOrderCostBudget>();
    complianceDocument.VendorID = ChangeOrderEntryExt.GetVendorIfSingle((IReadOnlyCollection<PMChangeOrderLine>) list2);
    complianceDocument.VendorName = this.GetVendorName(complianceDocument.VendorID);
    complianceDocument.CostCodeID = ChangeOrderEntryExt.GetCostCodeIfSingle((IReadOnlyCollection<PMChangeOrderLine>) list2, (IReadOnlyCollection<PMChangeOrderCostBudget>) list3);
    complianceDocument.CostTaskID = ChangeOrderEntryExt.GetCostTaskIfSingle((IReadOnlyCollection<PMChangeOrderLine>) list2, (IReadOnlyCollection<PMChangeOrderCostBudget>) list3);
    complianceDocument.RevenueTaskID = ChangeOrderEntryExt.GetRevenueTaskIfSingle((IReadOnlyCollection<PMChangeOrderRevenueBudget>) list1);
  }

  private static int? GetCostTaskIfSingle(
    IReadOnlyCollection<PMChangeOrderLine> commitments,
    IReadOnlyCollection<PMChangeOrderCostBudget> costBudgets)
  {
    int? costTaskIfSingle = (int?) commitments.FirstOrDefault<PMChangeOrderLine>()?.TaskID;
    int? firstCostTask = (int?) (costTaskIfSingle ?? costBudgets.FirstOrDefault<PMChangeOrderCostBudget>()?.ProjectTaskID);
    if (!commitments.Any<PMChangeOrderLine>((Func<PMChangeOrderLine, bool>) (c =>
    {
      int? taskId = c.TaskID;
      int? nullable = firstCostTask;
      return !(taskId.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId.HasValue == nullable.HasValue);
    })) && !costBudgets.Any<PMChangeOrderCostBudget>((Func<PMChangeOrderCostBudget, bool>) (c =>
    {
      int? taskId = c.TaskID;
      int? nullable = firstCostTask;
      return !(taskId.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId.HasValue == nullable.HasValue);
    })))
      return firstCostTask;
    costTaskIfSingle = new int?();
    return costTaskIfSingle;
  }

  private static int? GetCostCodeIfSingle(
    IReadOnlyCollection<PMChangeOrderLine> commitments,
    IReadOnlyCollection<PMChangeOrderCostBudget> costBudgets)
  {
    int? costCodeIfSingle = (int?) commitments.FirstOrDefault<PMChangeOrderLine>()?.CostCodeID;
    int? firstCostCodeId = (int?) (costCodeIfSingle ?? costBudgets.FirstOrDefault<PMChangeOrderCostBudget>()?.CostCodeID);
    if (!commitments.Any<PMChangeOrderLine>((Func<PMChangeOrderLine, bool>) (c =>
    {
      int? costCodeId = c.CostCodeID;
      int? nullable = firstCostCodeId;
      return !(costCodeId.GetValueOrDefault() == nullable.GetValueOrDefault() & costCodeId.HasValue == nullable.HasValue);
    })) && !costBudgets.Any<PMChangeOrderCostBudget>((Func<PMChangeOrderCostBudget, bool>) (c =>
    {
      int? costCodeId = c.CostCodeID;
      int? nullable = firstCostCodeId;
      return !(costCodeId.GetValueOrDefault() == nullable.GetValueOrDefault() & costCodeId.HasValue == nullable.HasValue);
    })))
      return firstCostCodeId;
    costCodeIfSingle = new int?();
    return costCodeIfSingle;
  }

  private static int? GetVendorIfSingle(IReadOnlyCollection<PMChangeOrderLine> commitments)
  {
    int? firstVendorId = (int?) commitments.FirstOrDefault<PMChangeOrderLine>()?.VendorID;
    return !commitments.Any<PMChangeOrderLine>((Func<PMChangeOrderLine, bool>) (c =>
    {
      int? vendorId = c.VendorID;
      int? nullable = firstVendorId;
      return !(vendorId.GetValueOrDefault() == nullable.GetValueOrDefault() & vendorId.HasValue == nullable.HasValue);
    })) ? firstVendorId : new int?();
  }

  private static int? GetRevenueTaskIfSingle(
    IReadOnlyCollection<PMChangeOrderRevenueBudget> revenueBudgets)
  {
    int? revenueTask = (int?) revenueBudgets.FirstOrDefault<PMChangeOrderRevenueBudget>()?.ProjectTaskID;
    return !revenueBudgets.Any<PMChangeOrderRevenueBudget>((Func<PMChangeOrderRevenueBudget, bool>) (c =>
    {
      int? projectTaskId = c.ProjectTaskID;
      int? nullable = revenueTask;
      return !(projectTaskId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectTaskId.HasValue == nullable.HasValue);
    })) ? revenueTask : new int?();
  }

  private void ValidateChangeOrderLine(PMChangeOrderLine changeOrderLine)
  {
    bool flag1 = this.complianceDocumentService.ValidateRelatedField<PMChangeOrderLine, ComplianceDocument.costTaskID, PMChangeOrderLine.taskID>(changeOrderLine, (object) changeOrderLine.TaskID);
    bool flag2 = this.complianceDocumentService.ValidateRelatedField<PMChangeOrderLine, ComplianceDocument.vendorID, PMChangeOrderLine.vendorID>(changeOrderLine, (object) changeOrderLine.VendorID);
    this.complianceDocumentService.ValidateRelatedRow<PMChangeOrderLine, PmChangeOrderLineExt.hasExpiredComplianceDocuments>(changeOrderLine, flag1 | flag2);
  }

  private void ValidateChangeOrderRevenueBudget(PMChangeOrderRevenueBudget revenueBudgetLine)
  {
    bool rowHasExpiredCompliance = this.complianceDocumentService.ValidateRelatedField<PMChangeOrderRevenueBudget, ComplianceDocument.revenueTaskID, PMChangeOrderRevenueBudget.projectTaskID>(revenueBudgetLine, (object) revenueBudgetLine.ProjectTaskID);
    this.complianceDocumentService.ValidateRelatedRow<PMChangeOrderRevenueBudget, PmChangeOrderRevenueBudgetExt.hasExpiredComplianceDocuments>(revenueBudgetLine, rowHasExpiredCompliance);
  }

  private void ValidateChangeOrderCostBudget(PMChangeOrderCostBudget costBudgetLine)
  {
    bool rowHasExpiredCompliance = this.complianceDocumentService.ValidateRelatedField<PMChangeOrderCostBudget, ComplianceDocument.costTaskID, PMChangeOrderCostBudget.projectTaskID>(costBudgetLine, (object) costBudgetLine.ProjectTaskID);
    this.complianceDocumentService.ValidateRelatedRow<PMChangeOrderCostBudget, PmChangeOrderCostBudgetExt.hasExpiredComplianceDocuments>(costBudgetLine, rowHasExpiredCompliance);
  }

  private string GetVendorName(int? vendorId)
  {
    if (!vendorId.HasValue)
      return (string) null;
    return ((PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>((PXGraph) ((PXGraphExtension<ChangeOrderEntry>) this).Base)).SelectSingle(new object[1]
    {
      (object) vendorId
    })?.AcctName;
  }

  private string GetCustomerName(int? customerId)
  {
    if (!customerId.HasValue)
      return (string) null;
    return ((PXSelectBase<PX.Objects.AR.Customer>) new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>((PXGraph) ((PXGraphExtension<ChangeOrderEntry>) this).Base)).SelectSingle(new object[1]
    {
      (object) customerId
    })?.AcctName;
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PMChangeOrder>) ((PXGraphExtension<ChangeOrderEntry>) this).Base.Document).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>, Where<ComplianceDocument.changeOrderNumber, Equal<Required<PMChangeOrder.refNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>((PXGraph) ((PXGraphExtension<ChangeOrderEntry>) this).Base)).Select(new object[1]
      {
        (object) ((PXSelectBase<PMChangeOrder>) ((PXGraphExtension<ChangeOrderEntry>) this).Base.Document).Current.RefNbr
      }).FirstTableItems.ToList<ComplianceDocument>();
  }
}
