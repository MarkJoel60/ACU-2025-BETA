// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AR.GraphExtensions.CustomerMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.AR.GraphExtensions;

public class CustomerMaintExt : 
  PXGraphExtension<ComplianceViewEntityExtension<CustomerMaint, PX.Objects.AR.Customer>, CustomerMaint>
{
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocument, Where<ComplianceDocument.customerID, Equal<Current<PX.Objects.AR.Customer.bAccountID>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService service;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForCustomer(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  public virtual void _(PX.Data.Events.RowUpdated<ComplianceDocument> args)
  {
    ((PXSelectBase) this.ComplianceDocuments).View.RequestRefresh();
  }

  public IEnumerable complianceDocuments()
  {
    List<ComplianceDocument> list = this.GetComplianceDocuments().ToList<ComplianceDocument>();
    this.service.ValidateComplianceDocuments((PXCache) null, (IEnumerable<ComplianceDocument>) list, ((PXSelectBase) this.ComplianceDocuments).Cache);
    return (IEnumerable) list;
  }

  protected virtual void ComplianceDocument_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs arguments)
  {
    this.service.UpdateExpirationIndicator(arguments.Row as ComplianceDocument);
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current;
    if (current == null)
      return;
    ComplianceDocument row = args.Row;
    if (row == null)
      return;
    row.CustomerID = current.BAccountID;
    row.CustomerName = current.AcctName;
    row.AccountID = (int?) ((PXSelectBase<CustomerClass>) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerClass).Current?.ARAcctID;
  }

  protected virtual void Customer_RowSelected(PXCache cache, PXRowSelectedEventArgs arguments)
  {
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Cache.Inserted);
  }

  protected virtual void Customer_RowSelecting(PXCache cache, PXRowSelectingEventArgs arguments)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.Customer> args)
  {
    if (args.Row == null)
      return;
    foreach (ComplianceDocument complianceDocument in this.GetComplianceDocuments())
    {
      complianceDocument.CustomerID = new int?();
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
    }
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PX.Objects.AR.Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>, Where<ComplianceDocument.customerID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base)).Select(new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.AR.Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current.BAccountID
      }).FirstTableItems.ToList<ComplianceDocument>();
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();
}
