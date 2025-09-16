// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.GraphExtensions.RegisterEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.GraphExtensions;

public class RegisterEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<RegisterEntry, PMRegister>, RegisterEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.projectTransactionID>>>, Where<ComplianceDocumentReference.type, Equal<Current<PMRegister.module>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<PMRegister.refNbr>>>>> ComplianceDocuments;
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
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<RegisterEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForProjectTransactionsForm(((PXSelectBase) this.ComplianceDocuments).Cache);
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

  public virtual void _(Events.RowUpdated<ComplianceDocument> args)
  {
    ((PXSelectBase) this.ComplianceDocuments).View.RequestRefresh();
  }

  protected virtual void _(Events.RowPersisting<PMRegister> args)
  {
    PMRegister row = args.Row;
    if ((row != null ? (row.Released.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    args.Cancel = true;
  }

  protected virtual void PmRegister_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs arguments,
    PXRowSelected baseHandler)
  {
    if (!(arguments.Row is PMRegister))
      return;
    baseHandler.Invoke(cache, arguments);
    ((PXSelectBase) ((PXGraphExtension<RegisterEntry>) this).Base.Document).Cache.AllowUpdate = true;
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<RegisterEntry>) this).Base.Document).Cache.Inserted);
  }

  protected virtual void PmRegister_RowSelecting(PXCache cache, PXRowSelectingEventArgs arguments)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void PmRegister_RowDeleted(PXCache cache, PXRowDeletedEventArgs arguments)
  {
    if (!(arguments.Row is PMRegister))
      return;
    foreach (ComplianceDocument complianceDocument in this.GetComplianceDocuments())
    {
      complianceDocument.ProjectTransactionID = new Guid?();
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
    }
  }

  protected virtual void ComplianceDocument_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs arguments)
  {
    this.service.UpdateExpirationIndicator(arguments.Row as ComplianceDocument);
  }

  protected virtual void _(Events.RowInserting<ComplianceDocument> args)
  {
    PMRegister current = ((PXSelectBase<PMRegister>) ((PXGraphExtension<RegisterEntry>) this).Base.Document).Current;
    if (current == null)
      return;
    ComplianceDocument row = args.Row;
    if (row == null)
      return;
    row.ProjectTransactionID = this.CreateComplianceDocumentReference(current).ComplianceDocumentReferenceId;
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PMRegister>) ((PXGraphExtension<RegisterEntry>) this).Base.Document).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.projectTransactionID>>, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>>, Where<ComplianceDocumentReference.type, Equal<Current<PMRegister.module>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<PMRegister.refNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>>((PXGraph) ((PXGraphExtension<RegisterEntry>) this).Base)).Select(new object[2]
      {
        (object) ((PXSelectBase<PMRegister>) ((PXGraphExtension<RegisterEntry>) this).Base.Document).Current.Module,
        (object) ((PXSelectBase<PMRegister>) ((PXGraphExtension<RegisterEntry>) this).Base.Document).Current.RefNbr
      }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private ComplianceDocumentReference CreateComplianceDocumentReference(PMRegister register)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) this.DocumentReference).Insert(new ComplianceDocumentReference()
    {
      ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid()),
      Type = register.Module,
      ReferenceNumber = register.RefNbr,
      RefNoteId = register.NoteID
    });
  }
}
