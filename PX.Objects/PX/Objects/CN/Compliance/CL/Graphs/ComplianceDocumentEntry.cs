// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Graphs.ComplianceDocumentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Common.Helpers;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.Graphs;

[DashboardType(new int[] {0})]
public class ComplianceDocumentEntry : PXGraph<
#nullable disable
ComplianceDocumentEntry>
{
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<ComplianceDocument, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceDocument.projectID, 
  #nullable disable
  IsNull>>>>.Or<Exists<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceDocument.projectID, 
  #nullable disable
  Equal<PMProject.contractID>>>>>.And<MatchUserFor<PMProject>>>>>>.Order<By<BqlField<
  #nullable enable
  ComplianceDocument.createdDateTime, IBqlDateTime>.Asc>>, 
  #nullable disable
  ComplianceDocument>.View Documents;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  public PXSelect<ComplianceDocumentReference> DocumentReference;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  public PXSave<ComplianceDocument> Save;
  public PXCancel<ComplianceDocument> Cancel;
  private ComplianceDocumentService service;
  public PXAction<ComplianceDocument> setAsFinal;

  public ComplianceDocumentEntry()
  {
    FeaturesSetHelper.CheckConstructionFeature();
    this.service = new ComplianceDocumentService((PXGraph) this, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.Documents, nameof (Documents));
    this.service.GenerateColumns(((PXSelectBase) this.Documents).Cache, nameof (ComplianceAnswers));
  }

  public virtual void Persist() => ((PXGraph) this).Persist();

  protected virtual void ComplianceDocument_DocumentType_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs arguments)
  {
    if (arguments.NewValue == null)
      throw new PXSetPropertyException<ComplianceDocument.documentType>("The field is required.");
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public IEnumerable documents()
  {
    IEnumerable<ComplianceDocument> complianceDocuments = GraphHelper.RowCast<ComplianceDocument>((IEnumerable) new PXView((PXGraph) this, false, ((PXSelectBase) this.Documents).View.BqlSelect).SelectWithViewContext());
    this.service.ValidateComplianceDocuments((PXCache) null, complianceDocuments, ((PXSelectBase) this.Documents).Cache);
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultTruncated = true;
    pxDelegateResult.IsResultSorted = true;
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) complianceDocuments);
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual void _(Events.RowSelected<ComplianceDocument> e)
  {
    if (e?.Row == null)
      return;
    int? lienWaiverType = (int?) ((PXGraph) this).GetExtension<ComplianceDocumentEntryComplianceDocumentTypeExt>().GetLienWaiverDocumentType()?.ComplianceAttributeTypeID;
    ((PXAction) this.setAsFinal).SetEnabled(((PXSelectBase) this.Documents).Cache.Cached.OfType<ComplianceDocument>().Any<ComplianceDocument>((Func<ComplianceDocument, bool>) (d =>
    {
      if (!d.Selected.GetValueOrDefault())
        return false;
      int? documentType = d.DocumentType;
      int? nullable = lienWaiverType;
      return documentType.GetValueOrDefault() == nullable.GetValueOrDefault() & documentType.HasValue == nullable.HasValue;
    })));
  }

  [PXUIField(DisplayName = "Set as Final")]
  [PXProcessButton]
  public virtual IEnumerable SetAsFinal(PXAdapter adapter)
  {
    this.UpdateSelectedLienWaiversAsFinal();
    return adapter.Get();
  }

  protected virtual void UpdateSelectedLienWaiversAsFinal()
  {
    ComplianceDocumentEntryComplianceDocumentTypeExt extension = ((PXGraph) this).GetExtension<ComplianceDocumentEntryComplianceDocumentTypeExt>();
    int? lienWaiverType = extension.GetLienWaiverDocumentType().ComplianceAttributeTypeID;
    List<ComplianceDocument> list = ((PXSelectBase) this.Documents).Cache.Cached.OfType<ComplianceDocument>().Where<ComplianceDocument>((Func<ComplianceDocument, bool>) (d =>
    {
      if (!d.Selected.GetValueOrDefault())
        return false;
      int? documentType = d.DocumentType;
      int? nullable = lienWaiverType;
      return documentType.GetValueOrDefault() == nullable.GetValueOrDefault() & documentType.HasValue == nullable.HasValue;
    })).ToList<ComplianceDocument>();
    ComplianceAttribute conditionalPartialType = extension.GetLienWaiverConditionalPartialType();
    ComplianceAttribute unconditionalPartialType = extension.GetLienWaiverUnconditionalPartialType();
    ComplianceAttribute conditionalFinalType = extension.GetLienWaiverConditionalFinalType();
    ComplianceAttribute unconditionalFinalType = extension.GetLienWaiverUnconditionalFinalType();
    foreach (ComplianceDocument lw in list)
    {
      int? documentTypeValue = lw.DocumentTypeValue;
      int? nullable1 = lw.DocumentTypeValue;
      string empty = string.Empty;
      int? nullable2 = lw.DocumentTypeValue;
      int? nullable3 = conditionalPartialType.AttributeId;
      if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
      {
        nullable1 = conditionalFinalType.AttributeId;
        empty = conditionalPartialType.Value;
      }
      else
      {
        nullable3 = lw.DocumentTypeValue;
        nullable2 = unconditionalPartialType.AttributeId;
        if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
        {
          nullable1 = unconditionalFinalType.AttributeId;
          empty = unconditionalPartialType.Value;
        }
        else
        {
          nullable2 = lw.DocumentTypeValue;
          nullable3 = conditionalFinalType.AttributeId;
          if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
          {
            empty = conditionalFinalType.Value;
          }
          else
          {
            nullable3 = lw.DocumentTypeValue;
            nullable2 = unconditionalFinalType.AttributeId;
            if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
              empty = unconditionalFinalType.Value;
          }
        }
      }
      try
      {
        APPaymentEntryLienWaiver.RecalculateLWAmount((PXGraph) this, lw);
        ((PXSelectBase<ComplianceDocument>) this.Documents).SetValueExt<ComplianceDocument.documentTypeValue>(lw, (object) nullable1);
        ((PXSelectBase) this.Documents).Cache.PersistUpdated((object) lw);
      }
      catch (Exception ex)
      {
        ((PXSelectBase<ComplianceDocument>) this.Documents).SetValueExt<ComplianceDocument.documentTypeValue>(lw, (object) documentTypeValue);
        ((PXSelectBase) this.Documents).Cache.RaiseExceptionHandling<ComplianceDocument.documentTypeValue>((object) lw, (object) empty, ex);
      }
    }
    ((PXAction) this.Save).Press();
  }
}
