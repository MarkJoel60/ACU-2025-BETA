// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AP.GraphExtensions.VendorMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.AP.GraphExtensions;

public class VendorMaintExt : 
  PXGraphExtension<ComplianceViewEntityExtension<VendorMaint, PX.Objects.AP.Vendor>, VendorMaint>
{
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocument, Where<ComplianceDocument.vendorID, Equal<Current<VendorR.bAccountID>>, Or<ComplianceDocument.secondaryVendorID, Equal<Current<VendorR.bAccountID>>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService service;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<VendorMaint>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForVendor(((PXSelectBase) this.ComplianceDocuments).Cache);
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
    VendorR current = ((PXSelectBase<VendorR>) ((PXGraphExtension<VendorMaint>) this).Base.BAccount).Current;
    if (current == null)
      return;
    ComplianceDocument row = args.Row;
    if (row == null)
      return;
    row.VendorID = current.BAccountID;
    row.VendorName = current.AcctName;
  }

  protected virtual void Vendor_RowSelected(PXCache cache, PXRowSelectedEventArgs arguments)
  {
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<VendorMaint>) this).Base.BAccount).Cache.Inserted);
  }

  protected virtual void Vendor_RowSelecting(PXCache cache, PXRowSelectingEventArgs arguments)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void Vendor_RowDeleted(PXCache cache, PXRowDeletedEventArgs arguments)
  {
    if (!(arguments.Row is PX.Objects.AP.Vendor row))
      return;
    this.UpdateDeletedVendorComplianceDocuments(row.BAccountID);
  }

  private void UpdateDeletedVendorComplianceDocuments(int? vendorId)
  {
    foreach (ComplianceDocument complianceDocument in this.GetComplianceDocuments())
      this.UpdateVendorReference(complianceDocument, vendorId);
  }

  private void UpdateVendorReference(ComplianceDocument document, int? vendorId)
  {
    int? vendorId1 = document.VendorID;
    int? nullable1 = vendorId;
    if (vendorId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & vendorId1.HasValue == nullable1.HasValue)
      document.VendorID = new int?();
    int? secondaryVendorId = document.SecondaryVendorID;
    int? nullable2 = vendorId;
    if (secondaryVendorId.GetValueOrDefault() == nullable2.GetValueOrDefault() & secondaryVendorId.HasValue == nullable2.HasValue)
      document.SecondaryVendorID = new int?();
    int? vendorInternalId = document.JointVendorInternalId;
    int? nullable3 = vendorId;
    if (vendorInternalId.GetValueOrDefault() == nullable3.GetValueOrDefault() & vendorInternalId.HasValue == nullable3.HasValue)
      document.JointVendorInternalId = new int?();
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(document);
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<VendorMaint>) this).Base.CurrentVendor).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>, Where2<Where<ComplianceDocument.vendorID, Equal<Required<VendorR.bAccountID>>, Or<ComplianceDocument.secondaryVendorID, Equal<Required<VendorR.bAccountID>>, Or<ComplianceDocument.jointVendorInternalId, Equal<Required<VendorR.bAccountID>>>>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>((PXGraph) ((PXGraphExtension<VendorMaint>) this).Base)).Select(new object[3]
      {
        (object) ((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<VendorMaint>) this).Base.CurrentVendor).Current.BAccountID,
        (object) ((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<VendorMaint>) this).Base.CurrentVendor).Current.BAccountID,
        (object) ((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<VendorMaint>) this).Base.CurrentVendor).Current.BAccountID
      }).FirstTableItems.ToList<ComplianceDocument>();
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();
}
