// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentPurchaseOrderLineSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

/// <summary>
/// Needed as DirtyRead for the PXSelectorAttribute with type = Search2&lt;Joins&gt; didn`t read cached values.
/// </summary>
public class ComplianceDocumentPurchaseOrderLineSelectorAttribute : PXCustomSelectorAttribute
{
  private static readonly Type[] Fields = new Type[7]
  {
    typeof (POLine.lineNbr),
    typeof (POLine.branchID),
    typeof (POLine.inventoryID),
    typeof (POLine.lineType),
    typeof (POLine.tranDesc),
    typeof (POLine.orderQty),
    typeof (POLine.curyUnitCost)
  };

  public ComplianceDocumentPurchaseOrderLineSelectorAttribute()
    : base(typeof (POLine.lineNbr), ComplianceDocumentPurchaseOrderLineSelectorAttribute.Fields)
  {
  }

  public IEnumerable GetRecords()
  {
    ComplianceDocument cache = this._Graph.Caches[typeof (ComplianceDocument)].Current as ComplianceDocument;
    return cache != null ? (IEnumerable) this.GetPurchaseOrdersLines(EnumerableEx.Select<ComplianceDocumentReference>(this._Graph.Caches[typeof (ComplianceDocumentReference)].Cached).Single<ComplianceDocumentReference>((Func<ComplianceDocumentReference, bool>) (reference =>
    {
      Guid? documentReferenceId = reference.ComplianceDocumentReferenceId;
      Guid? purchaseOrder = cache.PurchaseOrder;
      if (documentReferenceId.HasValue != purchaseOrder.HasValue)
        return false;
      return !documentReferenceId.HasValue || documentReferenceId.GetValueOrDefault() == purchaseOrder.GetValueOrDefault();
    }))) : (IEnumerable) Enumerable.Empty<POLine>();
  }

  private PXResultset<POLine> GetPurchaseOrdersLines(ComplianceDocumentReference reference)
  {
    return ((PXSelectBase<POLine>) new PXSelect<POLine, Where<POLine.orderNbr, Equal<Required<POLine.orderNbr>>, And<POLine.orderType, Equal<Required<POLine.orderType>>>>>(this._Graph)).Select(new object[2]
    {
      (object) reference.ReferenceNumber,
      (object) reference.Type
    });
  }
}
