// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.ComplianceDocumentService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.Compliance.AP.CacheExtensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

internal class ComplianceDocumentService
{
  private readonly PXGraph graph;
  private readonly CommonAttributeColumnCreator columnCreator;
  private readonly PXSelectBase<ComplianceDocument> complianceDocuments;
  private readonly string complianceDocumentsViewName;

  public ComplianceDocumentService(
    PXGraph graph,
    PXSelectBase<CSAttributeGroup> attributeGroups,
    PXSelectBase<ComplianceDocument> complianceDocuments,
    string complianceDocumentsViewName)
  {
    this.graph = graph;
    this.complianceDocuments = complianceDocuments;
    this.complianceDocumentsViewName = complianceDocumentsViewName;
    this.columnCreator = new CommonAttributeColumnCreator(graph, attributeGroups);
  }

  public void GenerateColumns(PXCache cache, string documentAnswerView)
  {
    this.columnCreator.GenerateColumns(cache, this.complianceDocumentsViewName, documentAnswerView);
  }

  public void AddExpirationDateEventHandlers()
  {
    // ISSUE: method pointer
    this.graph.FieldSelecting.AddHandler<ComplianceDocument.expirationDate>(new PXFieldSelecting((object) this, __methodptr(\u003CAddExpirationDateEventHandlers\u003Eb__6_0)));
    // ISSUE: method pointer
    this.graph.FieldVerifying.AddHandler<ComplianceDocument.expirationDate>(new PXFieldVerifying((object) this, __methodptr(\u003CAddExpirationDateEventHandlers\u003Eb__6_1)));
  }

  public void UpdateExpirationIndicator(ComplianceDocument document)
  {
    if (document == null)
      return;
    ComplianceDocument complianceDocument = document;
    DateTime? expirationDate = document.ExpirationDate;
    DateTime? businessDate = this.graph.Accessinfo.BusinessDate;
    bool? nullable = new bool?(expirationDate.HasValue & businessDate.HasValue && expirationDate.GetValueOrDefault() < businessDate.GetValueOrDefault());
    complianceDocument.IsExpired = nullable;
  }

  public void ValidateComplianceDocuments(
    PXCache eventCache,
    IEnumerable<ComplianceDocument> documents,
    PXCache documentsCache)
  {
    if (eventCache != null && NonGenericIEnumerableExtensions.Any_(eventCache.Updated))
      return;
    EnumerableExtensions.ForEach<ComplianceDocument>(documents.Where<ComplianceDocument>((Func<ComplianceDocument, bool>) (d =>
    {
      DateTime? expirationDate = d.ExpirationDate;
      DateTime? businessDate = this.graph.Accessinfo.BusinessDate;
      return expirationDate.HasValue & businessDate.HasValue && expirationDate.GetValueOrDefault() < businessDate.GetValueOrDefault();
    })), (Action<ComplianceDocument>) (d => ComplianceDocumentService.RaiseComplianceDocumentIsExpiredException(documentsCache, d, d.ExpirationDate)));
  }

  public IEnumerable<ComplianceDocument> GetComplianceDocuments<TField>(object value) where TField : IBqlField
  {
    return ((PXSelectBase<ComplianceDocument>) new PXSelect<ComplianceDocument, Where<TField, Equal<Required<TField>>>>(this.graph)).Select(new object[1]
    {
      value
    }).FirstTableItems;
  }

  public void ValidateApAdjustment<TField>(APAdjust adjustment) where TField : IBqlField
  {
    if (adjustment.IsSelfAdjustment())
      return;
    APInvoice apInvoice = PXResultset<APInvoice>.op_Implicit(PXSelectBase<APInvoice, PXViewOf<APInvoice>.BasedOn<SelectFromBase<APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APInvoice.refNbr, Equal<P.AsString>>>>>.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(this.graph, new object[2]
    {
      (object) adjustment.AdjdDocType,
      (object) adjustment.AdjdRefNbr
    }));
    if (apInvoice == null)
      return;
    Guid? documentReferenceId = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReferenceId(this.graph, apInvoice);
    if (!documentReferenceId.HasValue)
      return;
    bool rowHasExpiredCompliance = this.ValidateRelatedField<APAdjust, ComplianceDocument.billID, TField>(adjustment, (object) documentReferenceId);
    this.ValidateRelatedRow<APAdjust, ApAdjustExt.hasExpiredComplianceDocuments>(adjustment, rowHasExpiredCompliance);
  }

  public bool ValidateRelatedField<TEntity, TComplianceDocumentField, TEntityField>(
    TEntity entity,
    object fieldValue)
    where TEntity : class, IBqlTable, new()
    where TComplianceDocumentField : IBqlField
    where TEntityField : IBqlField
  {
    PXCache<TEntity> cache = GraphHelper.Caches<TEntity>(this.graph);
    bool validationErrorNeeded = this.DoExpiredDocumentsExist<Where<TComplianceDocumentField, Equal<Required<TComplianceDocumentField>>>>(fieldValue);
    ComplianceDocumentService.RaiseOrClearExceptionForRelatedField<TEntityField>((PXCache) cache, (object) entity, validationErrorNeeded, (PXErrorLevel) 2);
    return validationErrorNeeded;
  }

  public bool ValidateRelatedProjectField<TEntity, TEntityField>(TEntity entity, object fieldValue)
    where TEntity : class, IBqlTable, new()
    where TEntityField : IBqlField
  {
    return this.ValidateProjectRelatedField<TEntity, TEntityField, BqlNone>(entity, fieldValue as int?);
  }

  public bool ValidateProjectVendorRelatedField<TEntity, TEntityField>(
    TEntity entity,
    int? projectID,
    int? vendorID)
    where TEntity : class, IBqlTable, new()
    where TEntityField : IBqlField
  {
    return this.ValidateProjectRelatedField<TEntity, TEntityField, Where<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, Or<Required<ComplianceDocument.vendorID>, IsNull>>>(entity, projectID, (object) vendorID, (object) vendorID);
  }

  public bool ValidateProjectRelatedField<TEntity, TEntityField, TWhere>(
    TEntity entity,
    int? projectID,
    params object[] args)
    where TEntity : class, IBqlTable, new()
    where TEntityField : IBqlField
    where TWhere : IBqlWhere, new()
  {
    List<object> objectList = new List<object>()
    {
      (object) projectID
    };
    objectList.AddRange((IEnumerable<object>) args);
    int num;
    if (!ProjectDefaultAttribute.IsNonProject(projectID))
    {
      if (!(typeof (TWhere) == typeof (BqlNone)))
        num = this.DoExpiredDocumentsExist<Where<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>, And<TWhere>>>(objectList.ToArray()) ? 1 : 0;
      else
        num = this.DoExpiredDocumentsExist<Where<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>>>((object) projectID) ? 1 : 0;
    }
    else
      num = 0;
    bool validationErrorNeeded = num != 0;
    if (validationErrorNeeded)
    {
      string contractCd = PMProject.PK.Find(this.graph, projectID)?.ContractCD;
      string customErrorMessage = $"The {contractCd} project has at least one expired compliance document.";
      ComplianceDocumentService.RaiseOrClearExceptionForRelatedField<TEntityField>((PXCache) GraphHelper.Caches<TEntity>(this.graph), (object) entity, (validationErrorNeeded ? 1 : 0) != 0, (PXErrorLevel) 2, customErrorMessage, (object) contractCd);
    }
    else
      ComplianceDocumentService.RaiseOrClearExceptionForRelatedField<TEntityField>((PXCache) GraphHelper.Caches<TEntity>(this.graph), (object) entity, validationErrorNeeded, (PXErrorLevel) 2);
    return validationErrorNeeded;
  }

  public void ValidateRelatedRow<TEntity, THasExpiredDocumentsField>(
    TEntity entity,
    bool rowHasExpiredCompliance)
    where TEntity : class, IBqlTable, new()
    where THasExpiredDocumentsField : IBqlField
  {
    PXCache<TEntity> cache = GraphHelper.Caches<TEntity>(this.graph);
    ((PXCache) cache).SetValue<THasExpiredDocumentsField>((object) entity, (object) rowHasExpiredCompliance);
    ComplianceDocumentService.RaiseOrClearExceptionForRelatedField<THasExpiredDocumentsField>((PXCache) cache, (object) entity, rowHasExpiredCompliance, (PXErrorLevel) 3);
  }

  private static void RaiseOrClearExceptionForRelatedField<TField>(
    PXCache cache,
    object entity,
    bool validationErrorNeeded,
    PXErrorLevel errorLevel,
    string customErrorMessage = null,
    params object[] args)
    where TField : IBqlField
  {
    string str = customErrorMessage ?? "Expired Compliance.";
    if (validationErrorNeeded)
      ComplianceDocumentService.RaiseCorrectExceptionForRelatedField<TField>(cache, entity, str, errorLevel, args);
    else
      cache.ClearFieldSpecificError<TField>(entity, str, args);
  }

  private bool DoExpiredDocumentsExist<TWhere>(params object[] args) where TWhere : IBqlWhere, new()
  {
    PXSelect<ComplianceDocument, Where<ComplianceDocument.expirationDate, Less<Required<ComplianceDocument.expirationDate>>>> pxSelect = new PXSelect<ComplianceDocument, Where<ComplianceDocument.expirationDate, Less<Required<ComplianceDocument.expirationDate>>>>(this.graph);
    ((PXSelectBase<ComplianceDocument>) pxSelect).WhereAnd<TWhere>();
    List<object> objectList = new List<object>()
    {
      (object) this.graph.Accessinfo.BusinessDate
    };
    objectList.AddRange((IEnumerable<object>) args);
    return ((IQueryable<PXResult<ComplianceDocument>>) ((PXSelectBase<ComplianceDocument>) pxSelect).Select(objectList.ToArray())).Any<PXResult<ComplianceDocument>>();
  }

  private static void RaiseCorrectExceptionForRelatedField<TField>(
    PXCache cache,
    object entity,
    string errorMessage,
    PXErrorLevel errorLevel,
    object[] args)
    where TField : IBqlField
  {
    if (PXUIFieldAttribute.GetError<TField>(cache, entity) != null)
      return;
    ComplianceDocumentService.RaiseExceptionForRelatedField<TField>(cache, entity, errorMessage, errorLevel, args);
  }

  private static void RaiseExceptionForRelatedField<TField>(
    PXCache cache,
    object entity,
    string errorMessage,
    PXErrorLevel errorLevel,
    params object[] args)
    where TField : IBqlField
  {
    PXSetPropertyException<TField> propertyException = new PXSetPropertyException<TField>(errorMessage, errorLevel, args);
    cache.RaiseExceptionHandling<TField>(entity, cache.GetValue<TField>(entity), (Exception) propertyException);
  }

  private void ValidateExpirationDateOnFieldSelecting(
    ComplianceDocument document,
    PXCache documentsCache)
  {
    if (document == null)
      return;
    DateTime? expirationDate = document.ExpirationDate;
    DateTime? businessDate = this.graph.Accessinfo.BusinessDate;
    if ((expirationDate.HasValue & businessDate.HasValue ? (expirationDate.GetValueOrDefault() < businessDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ComplianceDocumentService.RaiseComplianceDocumentIsExpiredException(documentsCache, document, document.ExpirationDate);
  }

  private void ValidateExpirationDateOnFieldVerifying(
    ComplianceDocument document,
    PXCache documentsCache,
    DateTime? expirationDate)
  {
    documentsCache.ClearItemAttributes();
    if (!expirationDate.HasValue)
      return;
    DateTime? nullable = expirationDate;
    DateTime? businessDate = this.graph.Accessinfo.BusinessDate;
    if ((nullable.HasValue & businessDate.HasValue ? (nullable.GetValueOrDefault() < businessDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ComplianceDocumentService.RaiseComplianceDocumentIsExpiredException(documentsCache, document, expirationDate);
  }

  private static void RaiseComplianceDocumentIsExpiredException(
    PXCache cache,
    ComplianceDocument document,
    DateTime? expirationDate)
  {
    ComplianceDocumentService.RaiseSingleIsExpiredException<ComplianceDocument.expirationDate>(cache, document, (object) expirationDate, (PXErrorLevel) 2);
    ComplianceDocumentService.RaiseSingleIsExpiredException<ComplianceDocument.isExpired>(cache, document, (object) document.IsExpired, (PXErrorLevel) 3);
  }

  private static void RaiseSingleIsExpiredException<TField>(
    PXCache cache,
    ComplianceDocument document,
    object fieldValue,
    PXErrorLevel errorLevel)
    where TField : IBqlField
  {
    PXSetPropertyException<TField> propertyException = new PXSetPropertyException<TField>("The compliance document has expired.", errorLevel);
    cache.RaiseExceptionHandling<TField>((object) document, fieldValue, (Exception) propertyException);
  }
}
