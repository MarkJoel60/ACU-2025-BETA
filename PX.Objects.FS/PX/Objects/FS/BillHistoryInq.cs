// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.BillHistoryInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class BillHistoryInq : PXGraph<BillHistoryInq>
{
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<FSBillHistory, LeftJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<FSBillHistory.childDocType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<FSBillHistory.childRefNbr>>>, LeftJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<FSBillHistory.childDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<FSBillHistory.childRefNbr>>>, LeftJoin<PX.Objects.SO.SOInvoice, On<PX.Objects.SO.SOInvoice.docType, Equal<FSBillHistory.childDocType>, And<PX.Objects.SO.SOInvoice.refNbr, Equal<FSBillHistory.childRefNbr>>>, LeftJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<FSBillHistory.childDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<FSBillHistory.childRefNbr>>>, LeftJoin<PMRegister, On<PMRegister.module, Equal<FSBillHistory.childDocType>, And<PMRegister.refNbr, Equal<FSBillHistory.childRefNbr>>>, LeftJoin<PX.Objects.IN.INRegister, On<PX.Objects.IN.INRegister.docType, Equal<FSBillHistory.childDocType>, And<PX.Objects.IN.INRegister.refNbr, Equal<FSBillHistory.childRefNbr>>>, LeftJoin<FSContractPostDoc, On<FSContractPostDoc.postDocType, Equal<FSBillHistory.childDocType>, And<FSContractPostDoc.postRefNbr, Equal<FSBillHistory.childRefNbr>>>>>>>>>>> BillHistoryRecords;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXAction<FSBillHistory> runAppointmentBilling;
  public PXAction<FSBillHistory> runServiceOrderBilling;
  public PXAction<FSBillHistory> runServiceContractBilling;
  public ViewPostBatch<FSBillHistory> openPostBatch;

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Doc. Date")]
  protected virtual void FSBillHistory_ChildDocDate_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Doc. Status")]
  protected virtual void FSBillHistory_ChildDocStatus_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RunAppointmentBilling(PXAdapter adapter)
  {
    if (!adapter.MassProcess)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<CreateInvoiceByAppointmentPost>(), (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RunServiceOrderBilling(PXAdapter adapter)
  {
    if (!adapter.MassProcess)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<CreateInvoiceByServiceOrderPost>(), (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RunServiceContractBilling(PXAdapter adapter)
  {
    if (!adapter.MassProcess)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<CreateInvoiceByContractPost>(), (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public virtual IEnumerable billHistoryRecords()
  {
    PXView pxView = new PXView((PXGraph) this, true, ((PXSelectBase) this.BillHistoryRecords).View.BqlSelect);
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = pxView.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    PXCache cache = pxView.Cache;
    foreach (PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc> pxResult in objectList)
    {
      FSBillHistory fsBillHistory = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      PX.Objects.SO.SOOrder soOrder = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      PX.Objects.IN.INRegister inRegister = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      PX.Objects.AR.ARInvoice arInvoice = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      PX.Objects.SO.SOInvoice soInvoice = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      PX.Objects.AP.APInvoice apInvoice = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      PMRegister pmRegister = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      FSContractPostDoc fsContractPostDoc = PXResult<FSBillHistory, PX.Objects.SO.SOOrder, PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice, PX.Objects.AP.APInvoice, PMRegister, PX.Objects.IN.INRegister, FSContractPostDoc>.op_Implicit(pxResult);
      if (fsBillHistory.ChildEntityType == "PXSO")
      {
        if (soOrder != null && !string.IsNullOrEmpty(soOrder.OrderNbr))
        {
          fsBillHistory.ChildDocDate = soOrder.OrderDate;
          fsBillHistory.ChildDocDesc = soOrder.OrderDesc;
          fsBillHistory.ChildAmount = soOrder.CuryOrderTotal;
          fsBillHistory.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.SO.SOOrder.status>((PXCache) new PXCache<PX.Objects.SO.SOOrder>(cache.Graph), (object) soOrder, soOrder.Status);
        }
      }
      else if (fsBillHistory.ChildEntityType == "PXAR")
      {
        if (arInvoice != null && !string.IsNullOrEmpty(arInvoice.RefNbr))
        {
          fsBillHistory.ChildDocDate = arInvoice.DocDate;
          fsBillHistory.ChildDocDesc = arInvoice.DocDesc;
          fsBillHistory.ChildAmount = arInvoice.CuryOrigDocAmt;
          fsBillHistory.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.AR.ARInvoice.status>((PXCache) new PXCache<PX.Objects.AR.ARInvoice>(cache.Graph), (object) arInvoice, arInvoice.Status);
        }
      }
      else if (fsBillHistory.ChildEntityType == "PXSI" || fsBillHistory.ChildEntityType == "PXSM")
      {
        if (soInvoice != null && !string.IsNullOrEmpty(soInvoice.RefNbr))
        {
          fsBillHistory.ChildDocDate = soInvoice.DocDate;
          fsBillHistory.ChildDocDesc = soInvoice.DocDesc;
          fsBillHistory.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.SO.SOInvoice.status>((PXCache) new PXCache<PX.Objects.SO.SOInvoice>(cache.Graph), (object) soInvoice, soInvoice.Status);
        }
      }
      else if (fsBillHistory.ChildEntityType == "PXAP")
      {
        if (apInvoice != null && !string.IsNullOrEmpty(apInvoice.RefNbr))
        {
          fsBillHistory.ChildDocDate = apInvoice.DocDate;
          fsBillHistory.ChildDocDesc = apInvoice.DocDesc;
          fsBillHistory.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.AP.APInvoice.status>((PXCache) new PXCache<PX.Objects.AP.APInvoice>(cache.Graph), (object) apInvoice, apInvoice.Status);
        }
      }
      else if (fsBillHistory.ChildEntityType == "PXPM")
      {
        if (pmRegister != null && !string.IsNullOrEmpty(pmRegister.RefNbr))
        {
          fsBillHistory.ChildDocDesc = pmRegister.Description;
          fsBillHistory.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PMRegister.status>((PXCache) new PXCache<PMRegister>(cache.Graph), (object) pmRegister, pmRegister.Status);
        }
      }
      else if ((fsBillHistory.ChildEntityType == "PXIS" || fsBillHistory.ChildEntityType == "PXIR") && inRegister != null && !string.IsNullOrEmpty(inRegister.RefNbr))
      {
        fsBillHistory.ChildDocDate = inRegister.TranDate;
        fsBillHistory.ChildDocDesc = inRegister.TranDesc;
        fsBillHistory.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.IN.INRegister.status>((PXCache) new PXCache<PX.Objects.IN.INRegister>(cache.Graph), (object) inRegister, inRegister.Status);
      }
      fsBillHistory.ServiceContractPeriodID = fsContractPostDoc.ContractPeriodID;
      if (string.IsNullOrEmpty(fsBillHistory.ChildDocStatus))
      {
        fsBillHistory.ChildDocStatus = "Deleted";
        fsBillHistory.IsChildDocDeleted = new bool?(true);
      }
    }
    return (IEnumerable) objectList;
  }
}
