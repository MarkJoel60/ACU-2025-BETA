// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommissionDocEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Standalone;
using PX.Objects.BQLConstants;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARSPCommissionDocEnq : PXGraph<ARSPCommissionDocEnq>
{
  public PXFilter<SPDocFilter> Filter;
  public PXCancel<SPDocFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelect<ARSPCommnDocResult> SPDocs;
  public PXAction<SPDocFilter> viewDocument;
  public PXAction<SPDocFilter> viewOrigDocument;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;

  public ARSPCommissionDocEnq()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.SPDocs).Cache.AllowDelete = false;
    ((PXSelectBase) this.SPDocs).Cache.AllowUpdate = false;
    ((PXSelectBase) this.SPDocs).Cache.AllowInsert = false;
  }

  public virtual bool IsDirty => false;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARSPCommnDocResult.branchID> e)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARSPCommnDocResult>) this.SPDocs).Current != null)
      this.RedirectToDoc(((PXSelectBase<ARSPCommnDocResult>) this.SPDocs).Current.DocType, ((PXSelectBase<ARSPCommnDocResult>) this.SPDocs).Current.RefNbr);
    return (IEnumerable) ((PXSelectBase<SPDocFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewOrigDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARSPCommnDocResult>) this.SPDocs).Current != null)
    {
      ARSPCommnDocResult current = ((PXSelectBase<ARSPCommnDocResult>) this.SPDocs).Current;
      if (current.DocType == "CRM" && string.IsNullOrEmpty(current.AdjdDocType))
        this.RedirectToDoc(current.DocType, current.RefNbr);
      if (!string.IsNullOrEmpty(current.AdjdDocType))
        this.RedirectToDoc(current.AdjdDocType, current.AdjdRefNbr);
      throw new PXException("Original document is not set.");
    }
    return (IEnumerable) ((PXSelectBase<SPDocFilter>) this.Filter).Select(Array.Empty<object>());
  }

  protected virtual IEnumerable spdocs()
  {
    List<ARSPCommnDocResult> arspCommnDocResultList = new List<ARSPCommnDocResult>();
    SPDocFilter current = ((PXSelectBase<SPDocFilter>) this.Filter).Current;
    if (current != null && current.CommnPeriod != null)
    {
      PXSelectBase<ARSalesPerTran> pxSelectBase = (PXSelectBase<ARSalesPerTran>) new PXSelectJoin<ARSalesPerTran, InnerJoin<ARRegister, On<ARSalesPerTran.docType, Equal<ARRegister.docType>, And<ARSalesPerTran.refNbr, Equal<ARRegister.refNbr>>>, InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARRegister.customerID>, And<Match<Customer, Current<AccessInfo.userName>>>>>>, Where<ARSalesPerTran.actuallyUsed, Equal<BitOn>>>((PXGraph) this);
      if (current.SalesPersonID.HasValue)
        pxSelectBase.WhereAnd<Where<ARSalesPerTran.salespersonID, Equal<Current<SPDocFilter.salesPersonID>>>>();
      if (current.CommnPeriod != null)
        pxSelectBase.WhereAnd<Where<ARSalesPerTran.commnPaymntPeriod, Equal<Current<SPDocFilter.commnPeriod>>>>();
      if (current.CustomerID.HasValue)
        pxSelectBase.WhereAnd<Where<ARRegister.customerID, Equal<Current<SPDocFilter.customerID>>>>();
      if (current.LocationID.HasValue)
        pxSelectBase.WhereAnd<Where<ARRegister.customerLocationID, Equal<Current<SPDocFilter.locationID>>>>();
      PXView view = ((PXSelectBase) pxSelectBase).View;
      int startRow = PXView.StartRow;
      int num = 0;
      object[] currents = PXView.Currents;
      object[] parameters = PXView.Parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> objectList = view.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      foreach (PXResult<ARSalesPerTran, ARRegister, Customer> pxResult in objectList)
      {
        ARSPCommnDocResult aDst = new ARSPCommnDocResult();
        this.Copy(aDst, PXResult<ARSalesPerTran, ARRegister, Customer>.op_Implicit(pxResult));
        this.Copy(aDst, PXResult<ARSalesPerTran, ARRegister, Customer>.op_Implicit(pxResult));
        arspCommnDocResultList.Add(aDst);
      }
    }
    return (IEnumerable) arspCommnDocResultList;
  }

  public virtual void SPDocFilter_CustomerID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    SPDocFilter row = (SPDocFilter) e.Row;
    cache.SetDefaultExt<SPDocFilter.locationID>(e.Row);
    cache.SetValuePending<SPDocFilter.locationID>(e.Row, (object) null);
  }

  public virtual void SPDocFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    SPDocFilter row = (SPDocFilter) e.Row;
  }

  protected virtual void Copy(ARSPCommnDocResult aDst, ARSalesPerTran aSrc)
  {
    aDst.RefNbr = aSrc.RefNbr;
    aDst.DocType = aSrc.DocType;
    aDst.AdjdDocType = aSrc.AdjdDocType != "UND" ? aSrc.AdjdDocType : string.Empty;
    aDst.AdjdRefNbr = aSrc.AdjdRefNbr;
    aDst.AdjNbr = aSrc.AdjNbr;
    aDst.CommnPct = aSrc.CommnPct;
    aDst.CommnAmt = aSrc.CommnAmt;
    aDst.CommnblAmt = aSrc.CommnblAmt;
    aDst.BaseCuryID = aSrc.BaseCuryID;
    aDst.BranchID = aSrc.BranchID;
  }

  protected virtual void Copy(ARSPCommnDocResult aDst, ARRegister aSrc)
  {
    aDst.CustomerID = aSrc.CustomerID;
    aDst.CustomerLocationID = aSrc.CustomerLocationID;
    aDst.OrigDocAmt = aSrc.OrigDocAmt;
  }

  protected virtual void RedirectToDoc(string aDocType, string aRefNbr)
  {
    Dictionary<string, string> valueLabelDic = new ARDocType.ListAttribute().ValueLabelDic;
    if (new ARInvoiceType.ListAttribute().ValueLabelDic.ContainsKey(aDocType))
    {
      ARInvoice doc = ARDocumentEnq.FindDoc<ARInvoice>((PXGraph) this, aDocType, aRefNbr);
      if (doc != null)
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<ARInvoice>) instance.Document).Current = doc;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      throw new PXException("Document {0} {1} cannot be found in the system.", new object[2]
      {
        (object) valueLabelDic[aDocType],
        (object) aRefNbr
      });
    }
    if (new ARPaymentType.ListAttribute().ValueLabelDic.ContainsKey(aDocType))
    {
      ARPayment doc = ARDocumentEnq.FindDoc<ARPayment>((PXGraph) this, aDocType, aRefNbr);
      if (doc != null)
      {
        ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
        ((PXSelectBase<ARPayment>) instance.Document).Current = doc;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      throw new PXException("Document {0} {1} cannot be found in the system.", new object[2]
      {
        (object) valueLabelDic[aDocType],
        (object) aRefNbr
      });
    }
    ARCashSale arCashSale = new ARCashSaleType.ListAttribute().ValueLabelDic.ContainsKey(aDocType) ? ARDocumentEnq.FindDoc<ARCashSale>((PXGraph) this, aDocType, aRefNbr) : throw new PXException("The document cannot be processed because the document type is unknown.");
    if (arCashSale != null)
    {
      ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
      ((PXSelectBase<ARCashSale>) instance.Document).Current = arCashSale;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    throw new PXException("Document {0} {1} cannot be found in the system.", new object[2]
    {
      (object) valueLabelDic[aDocType],
      (object) aRefNbr
    });
  }
}
