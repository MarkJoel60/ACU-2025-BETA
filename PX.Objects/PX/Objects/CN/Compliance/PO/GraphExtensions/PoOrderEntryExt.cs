// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PO.GraphExtensions.PoOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CN.Compliance.PO.CacheExtensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.PO.GraphExtensions;

public class PoOrderEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<POOrderEntry, PX.Objects.PO.POOrder>, POOrderEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.purchaseOrder>>>, Where<ComplianceDocumentReference.type, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>, Or<ComplianceDocument.subcontract, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>, And<Current<PX.Objects.PO.POOrder.orderType>, Equal<POOrderType.regularSubcontract>>>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  public PXSelect<ComplianceDocumentReference> DocumentReference;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  public PXSetup<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> LienWaiverSetup;
  private ComplianceDocumentService service;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.ValidateComplianceSetup();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    this.UpdateFieldsVisibilityForComplianceDocuments();
  }

  private void ValidateComplianceSetup()
  {
    if (((PXSelectBase<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>) this.LienWaiverSetup).Current == null)
      throw new PXSetupNotEnteredException<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>();
  }

  public IEnumerable complianceDocuments()
  {
    List<ComplianceDocument> list = this.GetComplianceDocuments().ToList<ComplianceDocument>();
    this.service.ValidateComplianceDocuments((PXCache) null, (IEnumerable<ComplianceDocument>) list, ((PXSelectBase) this.ComplianceDocuments).Cache);
    return (IEnumerable) list;
  }

  public virtual void _(PX.Data.Events.RowUpdated<ComplianceDocument> args)
  {
    ((PXSelectBase) this.ComplianceDocuments).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POLine> args)
  {
    if (args.Row == null)
      return;
    this.ValidatePoLine(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> args)
  {
    if (args.Row == null)
      return;
    this.service.ValidateRelatedField<PX.Objects.PO.POOrder, ComplianceDocument.vendorID, PX.Objects.PO.POOrder.vendorID>(args.Row, (object) args.Row.VendorID);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    if (args.Row == null)
      return;
    this.service.UpdateExpirationIndicator(args.Row);
  }

  protected virtual void PoOrder_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs arguments,
    PXRowSelected baseHandler)
  {
    if (!(arguments.Row is PX.Objects.PO.POOrder))
      return;
    baseHandler.Invoke(cache, arguments);
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.PO.POOrder> args)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PX.Objects.PO.POOrder>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current;
    ComplianceDocument row = args.Row;
    if (current == null || row == null)
      return;
    PX.Objects.PO.POLine poLine = ((PXSelectBase<PX.Objects.PO.POLine>) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).SelectSingle(Array.Empty<object>());
    this.FillPurchaseOrderInfo(row, current, poLine);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.PO.POOrder> args)
  {
    if (args.Row == null)
      return;
    foreach (ComplianceDocument complianceDocument in this.GetComplianceDocuments())
      this.RemoveComplianceReference(complianceDocument);
  }

  private void RemoveComplianceReference(ComplianceDocument document)
  {
    if (this.IsSubcontractScreen())
      document.Subcontract = (string) null;
    else
      document.PurchaseOrder = new Guid?();
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(document);
  }

  private void FillPurchaseOrderInfo(
    ComplianceDocument complianceDocument,
    PX.Objects.PO.POOrder poOrder,
    PX.Objects.PO.POLine poLine)
  {
    complianceDocument.VendorID = poOrder.VendorID;
    complianceDocument.VendorName = this.GetVendorName(complianceDocument.VendorID);
    complianceDocument.ProjectID = (int?) poLine?.ProjectID;
    complianceDocument.CostTaskID = (int?) poLine?.TaskID;
    complianceDocument.AccountID = (int?) poLine?.ExpenseAcctID;
    complianceDocument.CostCodeID = (int?) poLine?.CostCodeID;
    this.SetComplianceDocumentReference(complianceDocument, poOrder);
  }

  private void SetComplianceDocumentReference(
    ComplianceDocument complianceDocument,
    PX.Objects.PO.POOrder poOrder)
  {
    if (this.IsSubcontractScreen())
      complianceDocument.Subcontract = poOrder.OrderNbr;
    else
      complianceDocument.PurchaseOrder = this.CreateComplianceDocumentReference(poOrder).ComplianceDocumentReferenceId;
  }

  private void ValidatePoLine(PX.Objects.PO.POLine poLine)
  {
    bool flag1 = this.service.ValidateProjectVendorRelatedField<PX.Objects.PO.POLine, PX.Objects.PO.POLine.projectID>(poLine, poLine.ProjectID, poLine.VendorID);
    bool flag2 = this.service.ValidateRelatedField<PX.Objects.PO.POLine, ComplianceDocument.costTaskID, PX.Objects.PO.POLine.taskID>(poLine, (object) poLine.TaskID);
    this.service.ValidateRelatedRow<PX.Objects.PO.POLine, PoLineExt.hasExpiredComplianceDocuments>(poLine, flag1 | flag2);
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return this.IsSubcontractScreen() ? this.GetCompliancesForSubcontract() : this.GetCompliancesForPurchaseOrder();
  }

  private IEnumerable<ComplianceDocument> GetCompliancesForPurchaseOrder()
  {
    return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.purchaseOrder>>, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>>, Where<ComplianceDocumentReference.type, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base)).Select(new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.OrderType,
      (object) ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.OrderNbr
    }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private IEnumerable<ComplianceDocument> GetCompliancesForSubcontract()
  {
    return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>, Where<ComplianceDocument.subcontract, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base)).Select(new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.OrderNbr
    }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private string GetVendorName(int? vendorId)
  {
    if (!vendorId.HasValue)
      return (string) null;
    return ((PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base)).SelectSingle(new object[1]
    {
      (object) vendorId
    })?.AcctName;
  }

  private bool IsSubcontractScreen()
  {
    return ((object) ((PXGraphExtension<POOrderEntry>) this).Base).GetType() == typeof (SubcontractEntry) || ((object) ((PXGraphExtension<POOrderEntry>) this).Base).GetType().BaseType == typeof (SubcontractEntry);
  }

  private void UpdateFieldsVisibilityForComplianceDocuments()
  {
    if (this.IsSubcontractScreen())
      ComplianceDocumentFieldVisibilitySetter.HideFieldsForSubcontract(((PXSelectBase) this.ComplianceDocuments).Cache);
    else
      ComplianceDocumentFieldVisibilitySetter.HideFieldsForPurchaseOrder(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  private ComplianceDocumentReference CreateComplianceDocumentReference(PX.Objects.PO.POOrder pOrder)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) this.DocumentReference).Insert(new ComplianceDocumentReference()
    {
      ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid()),
      Type = pOrder.OrderType,
      ReferenceNumber = pOrder.OrderNbr,
      RefNoteId = pOrder.NoteID
    });
  }
}
