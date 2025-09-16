// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentPolicyUniqueAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Compliance.CL.DAC;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentPolicyUniqueAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber
{
  public void RowPersisting(PXCache cache, PXRowPersistingEventArgs args)
  {
    if (args.Operation == 3 || !(args.Row is ComplianceDocument row))
      return;
    int? complianceAttributeTypeId = (int?) ((PXSelectBase<ComplianceAttributeType>) new PXSelectReadonly<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<ComplianceDocumentType.insurance>>>(cache.Graph)).SelectSingle(Array.Empty<object>())?.ComplianceAttributeTypeID;
    if (!complianceAttributeTypeId.HasValue)
      return;
    int? documentType = row.DocumentType;
    int? nullable = complianceAttributeTypeId;
    if (!(documentType.GetValueOrDefault() == nullable.GetValueOrDefault() & documentType.HasValue == nullable.HasValue) || !this.IsDuplicate(cache, row, complianceAttributeTypeId.Value))
      return;
    cache.RaiseExceptionHandling<ComplianceDocument.policy>(args.Row, (object) row.Policy, (Exception) new PXSetPropertyException("A compliance document with the same information (document category, policy, vendor, project, effective date, expiration date, and limit) already exists.", (PXErrorLevel) 5));
  }

  private bool IsDuplicate(
    PXCache cache,
    ComplianceDocument complianceDocument,
    int insuranceDocumentTypeId)
  {
    List<object> objectList = new List<object>()
    {
      (object) insuranceDocumentTypeId
    };
    FbqlSelect<SelectFromBase<ComplianceDocument, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ComplianceDocument.documentType, IBqlInt>.IsEqual<P.AsInt>>, ComplianceDocument>.View view = new FbqlSelect<SelectFromBase<ComplianceDocument, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ComplianceDocument.documentType, IBqlInt>.IsEqual<P.AsInt>>, ComplianceDocument>.View(cache.Graph);
    if (complianceDocument.Policy != null)
    {
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.policy, IBqlString>.IsEqual<P.AsString>>>();
      objectList.Add((object) complianceDocument.Policy);
    }
    else
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.policy, IBqlString>.IsNull>>();
    if (complianceDocument.VendorID.HasValue)
    {
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.vendorID, IBqlInt>.IsEqual<P.AsInt>>>();
      objectList.Add((object) complianceDocument.VendorID);
    }
    else
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.vendorID, IBqlInt>.IsNull>>();
    if (complianceDocument.ProjectID.HasValue)
    {
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.projectID, IBqlInt>.IsEqual<P.AsInt>>>();
      objectList.Add((object) complianceDocument.ProjectID);
    }
    else
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.projectID, IBqlInt>.IsNull>>();
    if (complianceDocument.EffectiveDate.HasValue)
    {
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.effectiveDate, IBqlDateTime>.IsEqual<P.AsDateTime>>>();
      objectList.Add((object) complianceDocument.EffectiveDate);
    }
    else
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.effectiveDate, IBqlDateTime>.IsNull>>();
    if (complianceDocument.ExpirationDate.HasValue)
    {
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.expirationDate, IBqlDateTime>.IsEqual<P.AsDateTime>>>();
      objectList.Add((object) complianceDocument.ExpirationDate);
    }
    else
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.expirationDate, IBqlDateTime>.IsNull>>();
    if (complianceDocument.Limit.HasValue)
    {
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.limit, IBqlDecimal>.IsEqual<P.AsDecimal>>>();
      objectList.Add((object) complianceDocument.Limit);
    }
    else
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.limit, IBqlDecimal>.IsNull>>();
    if (complianceDocument.DocumentTypeValue.HasValue)
    {
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.documentTypeValue, IBqlInt>.IsEqual<P.AsInt>>>();
      objectList.Add((object) complianceDocument.DocumentTypeValue);
    }
    else
      ((PXSelectBase<ComplianceDocument>) view).WhereAnd<Where<BqlOperand<ComplianceDocument.documentTypeValue, IBqlInt>.IsNull>>();
    return EnumerableExtensions.HasAtLeast<ComplianceDocument>(((PXSelectBase<ComplianceDocument>) view).Select(objectList.ToArray()).FirstTableItems, 2);
  }
}
