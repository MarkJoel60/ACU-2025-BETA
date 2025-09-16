// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.ARGLDiscrepancyByDocumentEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace ReconciliationTools;

[TableAndChartDashboardType]
public class ARGLDiscrepancyByDocumentEnq : 
  ARGLDiscrepancyEnqGraphBase<ARGLDiscrepancyByAccountEnq, ARGLDiscrepancyByCustomerEnqFilter, ARGLDiscrepancyByDocumentEnqResult>
{
  public PXAction<ARGLDiscrepancyByCustomerEnqFilter> viewDocument;

  public ARGLDiscrepancyByDocumentEnq()
  {
    PXUIFieldAttribute.SetRequired<ARDocumentEnq.ARDocumentResult.refNbr>(this.Caches[typeof (ARGLDiscrepancyByDocumentEnqResult)], false);
    PXUIFieldAttribute.SetRequired<ARDocumentEnq.ARDocumentResult.finPeriodID>(this.Caches[typeof (ARGLDiscrepancyByDocumentEnqResult)], false);
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Financial Period")]
  protected virtual void ARGLDiscrepancyByCustomerEnqFilter_PeriodFrom_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault]
  protected virtual void ARGLDiscrepancyByCustomerEnqFilter_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Original Amount")]
  protected virtual void ARGLDiscrepancyByDocumentEnqResult_OrigDocAmt_CacheAttached(PXCache sender)
  {
  }

  protected override List<ARGLDiscrepancyByDocumentEnqResult> SelectDetails()
  {
    List<ARGLDiscrepancyByDocumentEnqResult> documentEnqResultList = new List<ARGLDiscrepancyByDocumentEnqResult>();
    ARGLDiscrepancyByCustomerEnqFilter current = this.Filter.Current;
    if (current == null || !current.BranchID.HasValue || current.PeriodFrom == null || !current.CustomerID.HasValue)
      return documentEnqResultList;
    ARDocumentEnq instance = PXGraph.CreateInstance<ARDocumentEnq>();
    ARDocumentEnq.ARDocumentFilter copy1 = PXCache<ARDocumentEnq.ARDocumentFilter>.CreateCopy(instance.Filter.Current);
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(current.BranchID);
    copy1.OrgBAccountID = branch?.BAccountID;
    copy1.OrganizationID = (int?) branch?.Organization?.OrganizationID;
    copy1.BranchID = current.BranchID;
    copy1.CustomerID = current.CustomerID;
    copy1.Period = current.PeriodFrom;
    copy1.ARAcctID = current.AccountID;
    copy1.SubCD = current.SubCD;
    copy1.IncludeChildAccounts = new bool?(false);
    copy1.IncludeGLTurnover = new bool?(true);
    instance.Filter.Update(copy1);
    foreach (PXResult<ARDocumentEnq.ARDocumentResult> pxResult in instance.Documents.Select())
    {
      ARDocumentEnq.ARDocumentResult copy2 = (ARDocumentEnq.ARDocumentResult) pxResult;
      ARGLDiscrepancyByDocumentEnqResult documentEnqResult1 = new ARGLDiscrepancyByDocumentEnqResult();
      Decimal? nullable = copy2.ARTurnover;
      documentEnqResult1.XXTurnover = new Decimal?(nullable.GetValueOrDefault());
      ARGLDiscrepancyByDocumentEnqResult documentEnqResult2 = documentEnqResult1;
      PXCache<ARDocumentEnq.ARDocumentResult>.RestoreCopy((ARDocumentEnq.ARDocumentResult) documentEnqResult2, copy2);
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

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (this.Rows.Current != null)
      PXRedirectHelper.TryRedirect(this.Rows.Cache, (object) this.Rows.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return (IEnumerable) this.Filter.Select();
  }
}
