// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.GraphExtensions.ProjectEntryBaseExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.GraphExtensions;

public abstract class ProjectEntryBaseExt<TProjectEntryGraph> : 
  PXGraphExtension<ComplianceViewEntityExtension<TProjectEntryGraph, PMProject>, TProjectEntryGraph>
  where TProjectEntryGraph : ProjectEntryBase<TProjectEntryGraph>
{
  [PXFilterable(new Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocument, Where<ComplianceDocument.projectID, Equal<Current<PMProject.contractID>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService service;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.service = new ComplianceDocumentService((PXGraph) (object) ((PXGraphExtension<TProjectEntryGraph>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForProject(((PXSelectBase) this.ComplianceDocuments).Cache);
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

  protected virtual void PmProject_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs args,
    PXRowSelected baseHandler)
  {
    if (!(args.Row is PMProject))
      return;
    baseHandler.Invoke(cache, args);
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    bool flag = ((Contract) args.Row).Status == "L";
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<TProjectEntryGraph>) this).Base.Project).Cache.Inserted) && !flag;
    ((PXSelectBase) this.ComplianceDocuments).AllowUpdate = !flag;
    ((PXSelectBase) this.ComplianceDocuments).AllowDelete = !flag;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PMProject> args)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service?.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PMProject>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMProject> args)
  {
    if (args.Row == null)
      return;
    foreach (ComplianceDocument complianceDocument in this.GetComplianceDocuments())
    {
      complianceDocument.ProjectID = new int?();
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    this.service.UpdateExpirationIndicator(args.Row);
    if (args.Row == null)
      return;
    ComplianceDocument row = args.Row;
    ComplianceAttributeType complianceAttributeType = PXResultset<ComplianceAttributeType>.op_Implicit(PXSelectBase<ComplianceAttributeType, PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<Required<ComplianceAttributeType.type>>>>.Config>.Select((PXGraph) (object) ((PXGraphExtension<TProjectEntryGraph>) this).Base, new object[1]
    {
      (object) "Lien Waiver"
    }));
    int? documentType = row.DocumentType;
    int? complianceAttributeTypeId = complianceAttributeType.ComplianceAttributeTypeID;
    if (!(documentType.GetValueOrDefault() == complianceAttributeTypeId.GetValueOrDefault() & documentType.HasValue == complianceAttributeTypeId.HasValue) || !row.ThroughDate.HasValue)
      return;
    DateTime? throughDate = row.ThroughDate;
    DateTime? businessDate = ((PXGraph) (object) ((PXGraphExtension<TProjectEntryGraph>) this).Base).Accessinfo.BusinessDate;
    if ((throughDate.HasValue & businessDate.HasValue ? (throughDate.GetValueOrDefault() < businessDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 || row.Received.GetValueOrDefault())
      return;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) (object) ((PXGraphExtension<TProjectEntryGraph>) this).Base, new object[1]
    {
      (object) row.VendorID
    }));
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ComplianceDocument>>) args).Cache.RaiseExceptionHandling<ComplianceDocument.throughDate>((object) row, (object) row.ThroughDate, (Exception) new PXSetPropertyException<ComplianceDocument.throughDate>("There is an outstanding lien waiver for the {0} vendor.", (PXErrorLevel) 2, new object[1]
    {
      (object) vendor.AcctName
    }));
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    PMProject current = ((PXSelectBase<PMProject>) ((PXGraphExtension<TProjectEntryGraph>) this).Base.Project).Current;
    ComplianceDocument row = args.Row;
    if (current == null || row == null)
      return;
    this.FillProjectInfo(row, current);
  }

  private void FillProjectInfo(ComplianceDocument complianceDocument, PMProject project)
  {
    complianceDocument.ProjectID = project.ContractID;
    complianceDocument.CustomerID = project.CustomerID;
    complianceDocument.CustomerName = this.GetCustomerName(complianceDocument.CustomerID);
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PMProject>) ((PXGraphExtension<TProjectEntryGraph>) this).Base.Project).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelect<ComplianceDocument, Where<ComplianceDocument.projectID, Equal<Required<PMProject.contractID>>>>((PXGraph) (object) ((PXGraphExtension<TProjectEntryGraph>) this).Base)).Select(new object[1]
      {
        (object) ((PXSelectBase<PMProject>) ((PXGraphExtension<TProjectEntryGraph>) this).Base.Project).Current.ContractID
      }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private string GetCustomerName(int? customerId)
  {
    if (!customerId.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) (object) ((PXGraphExtension<TProjectEntryGraph>) this).Base, new object[1]
    {
      (object) customerId
    }))?.AcctName;
  }
}
