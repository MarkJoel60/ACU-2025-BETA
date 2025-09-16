// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.APGLDiscrepancyByDocumentEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace ReconciliationTools;

[TableAndChartDashboardType]
public class APGLDiscrepancyByDocumentEnq : 
  APGLDiscrepancyEnqGraphBase<APGLDiscrepancyByAccountEnq, APGLDiscrepancyByVendorEnqFilter, APGLDiscrepancyByDocumentEnqResult>
{
  public PXAction<APGLDiscrepancyByVendorEnqFilter> viewDocument;

  public APGLDiscrepancyByDocumentEnq()
  {
    PXUIFieldAttribute.SetRequired<APDocumentEnq.APDocumentResult.refNbr>(this.Caches[typeof (APGLDiscrepancyByDocumentEnqResult)], false);
    PXUIFieldAttribute.SetRequired<APDocumentEnq.APDocumentResult.finPeriodID>(this.Caches[typeof (APGLDiscrepancyByDocumentEnqResult)], false);
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Financial Period")]
  protected virtual void APGLDiscrepancyByVendorEnqFilter_PeriodFrom_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault]
  protected virtual void APGLDiscrepancyByVendorEnqFilter_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Original Amount")]
  protected virtual void APGLDiscrepancyByDocumentEnqResult_OrigDocAmt_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (this.Rows.Current != null)
      PXRedirectHelper.TryRedirect(this.Rows.Cache, (object) this.Rows.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return (IEnumerable) this.Filter.Select();
  }

  protected override List<APGLDiscrepancyByDocumentEnqResult> SelectDetails()
  {
    List<APGLDiscrepancyByDocumentEnqResult> documentEnqResultList = new List<APGLDiscrepancyByDocumentEnqResult>();
    APGLDiscrepancyByVendorEnqFilter current = this.Filter.Current;
    if (current == null || !current.BranchID.HasValue || current.PeriodFrom == null || !current.VendorID.HasValue)
      return documentEnqResultList;
    APDocumentEnq instance = PXGraph.CreateInstance<APDocumentEnq>();
    APDocumentEnq.APDocumentFilter copy1 = PXCache<APDocumentEnq.APDocumentFilter>.CreateCopy(instance.Filter.Current);
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(current.BranchID);
    copy1.OrgBAccountID = branch?.BAccountID;
    copy1.OrganizationID = (int?) branch?.Organization?.OrganizationID;
    copy1.BranchID = current.BranchID;
    copy1.VendorID = current.VendorID;
    copy1.FinPeriodID = current.PeriodFrom;
    copy1.AccountID = current.AccountID;
    copy1.SubCD = current.SubCD;
    copy1.IncludeGLTurnover = new bool?(true);
    instance.Filter.Update(copy1);
    Dictionary<ARDocKey, ARGLDiscrepancyByDocumentEnqResult> dictionary = new Dictionary<ARDocKey, ARGLDiscrepancyByDocumentEnqResult>();
    foreach (PXResult<APDocumentEnq.APDocumentResult> pxResult in instance.Documents.Select())
    {
      APDocumentEnq.APDocumentResult copy2 = (APDocumentEnq.APDocumentResult) pxResult;
      APGLDiscrepancyByDocumentEnqResult documentEnqResult1 = new APGLDiscrepancyByDocumentEnqResult();
      Decimal? nullable = copy2.APTurnover;
      documentEnqResult1.XXTurnover = new Decimal?(nullable.GetValueOrDefault());
      APGLDiscrepancyByDocumentEnqResult documentEnqResult2 = documentEnqResult1;
      PXCache<APDocumentEnq.APDocumentResult>.RestoreCopy((APDocumentEnq.APDocumentResult) documentEnqResult2, copy2);
      if (current.ShowOnlyWithDiscrepancy.GetValueOrDefault())
      {
        nullable = documentEnqResult2.Discrepancy;
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          continue;
      }
      documentEnqResultList.Add(documentEnqResult2);
    }
    return documentEnqResultList;
  }
}
