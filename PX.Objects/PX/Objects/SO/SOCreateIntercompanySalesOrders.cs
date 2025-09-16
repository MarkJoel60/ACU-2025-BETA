// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOCreateIntercompanySalesOrders
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
public class SOCreateIntercompanySalesOrders : PXGraph<SOCreateIntercompanySalesOrders>
{
  public PXCancel<POForSalesOrderFilter> Cancel;
  public PXFilter<POForSalesOrderFilter> Filter;
  public PXSetup<PX.Objects.SO.SOSetup> SOSetup;
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  [PXFilterable(new System.Type[] {})]
  [PXVirtualDAC]
  public PXFilteredProcessingOrderBy<POForSalesOrderDocument, POForSalesOrderFilter, OrderBy<Asc<POForSalesOrderDocument.docNbr>>> Documents;
  private readonly Dictionary<string, (string, string)> FieldsDictionary = new Dictionary<string, (string, string)>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    ["DocType"] = ("OrderType", "ReceiptType"),
    ["DocNbr"] = ("OrderNbr", "ReceiptNbr"),
    ["VendorID"] = ("VendorID", "VendorID"),
    ["BranchID"] = ("BranchID", "BranchID"),
    ["CuryID"] = ("CuryID", "CuryID"),
    ["CuryDocTotal"] = ("CuryOrderTotal", "CuryOrderTotal"),
    ["CuryDiscTot"] = ("CuryDiscTot", (string) null),
    ["CuryTaxTotal"] = ("CuryTaxTotal", (string) null),
    ["ExpectedDate"] = ("ExpectedDate", (string) null),
    ["DocDate"] = ("OrderDate", "ReceiptDate"),
    ["OwnerID"] = ("OwnerID", "OwnerID"),
    ["DocDesc"] = ("OrderDesc", (string) null),
    ["DocQty"] = ((string) null, "OrderQty"),
    ["WorkgroupID"] = ((string) null, "WorkgroupID"),
    ["FinPeriodID"] = ((string) null, "FinPeriodID")
  };
  public PXAction<POForSalesOrderFilter> viewPODocument;
  public PXAction<POForSalesOrderFilter> viewSOOrder;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  public SOCreateIntercompanySalesOrders()
  {
    PX.Objects.SO.SOSetup current1 = ((PXSelectBase<PX.Objects.SO.SOSetup>) this.SOSetup).Current;
    PX.Objects.IN.INSetup current2 = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
    ((PXProcessingBase<POForSalesOrderDocument>) this.Documents).SetSelected<POForSalesOrderDocument.selected>();
    ((PXProcessing<POForSalesOrderDocument>) this.Documents).SetProcessCaption("Process");
    ((PXProcessing<POForSalesOrderDocument>) this.Documents).SetProcessAllCaption("Process All");
    PXUIFieldAttribute.SetEnabled<POForSalesOrderDocument.excluded>(((PXSelectBase) this.Documents).Cache, (object) null, PXLongOperation.GetStatus(((PXGraph) this).UID) == 0);
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (PX.Objects.CR.BAccount), typeof (PX.Objects.CR.BAccount));
  }

  protected virtual IEnumerable documents()
  {
    List<POForSalesOrderDocument> salesOrderDocumentList = new List<POForSalesOrderDocument>();
    int startRow = PXView.StartRow;
    int num = 0;
    if (((PXSelectBase<POForSalesOrderFilter>) this.Filter).Current != null)
    {
      using (new PXReadBranchRestrictedScope())
      {
        if (((PXSelectBase<POForSalesOrderFilter>) this.Filter).Current.PODocType == "PO")
        {
          foreach (PXResult<PX.Objects.PO.POOrder, PX.Objects.GL.Branch, PX.Objects.CR.BAccount, SOOrder> pxResult in ((PXSelectBase) new PXSelectJoin<PX.Objects.PO.POOrder, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.PO.POOrder.branchID>>, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>, And<PX.Objects.CR.BAccount.isBranch, Equal<True>>>, LeftJoin<SOOrder, On<SOOrder.FK.IntercompanyPOOrder>>>>, Where2<Where<PX.Objects.PO.POOrder.orderDate, LessEqual<Current<POForSalesOrderFilter.docDate>>, Or<Current<POForSalesOrderFilter.docDate>, IsNull>>, And2<Where<PX.Objects.PO.POOrder.vendorID, Equal<Current<POForSalesOrderFilter.sellingCompany>>, Or<Current<POForSalesOrderFilter.sellingCompany>, IsNull>>, And2<Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<POForSalesOrderFilter.purchasingCompany>>, Or<Current<POForSalesOrderFilter.purchasingCompany>, IsNull>>, And<PX.Objects.PO.POOrder.isIntercompany, Equal<True>, And<PX.Objects.PO.POOrder.orderType, In3<POOrderType.regularOrder, POOrderType.dropShip>, And<PX.Objects.PO.POOrder.status, Equal<POOrderStatus.open>, And<PX.Objects.PO.POOrder.excludeFromIntercompanyProc, Equal<False>, And<SOOrder.orderNbr, IsNull>>>>>>>>>((PXGraph) this)).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, this.GetSortColumnsWithOriginalNames(true), PXView.Descendings, this.GetFiltersWithOriginalNames(true), ref startRow, PXView.MaximumRows, ref num))
          {
            PX.Objects.PO.POOrder poOrder = PXResult<PX.Objects.PO.POOrder, PX.Objects.GL.Branch, PX.Objects.CR.BAccount, SOOrder>.op_Implicit(pxResult);
            POForSalesOrderDocument salesOrderDocument1 = new POForSalesOrderDocument()
            {
              DocType = poOrder.OrderType,
              DocNbr = poOrder.OrderNbr,
              VendorID = poOrder.VendorID,
              BranchID = poOrder.BranchID,
              CuryID = poOrder.CuryID,
              CuryDocTotal = poOrder.CuryOrderTotal,
              CuryDiscTot = poOrder.CuryDiscTot,
              CuryTaxTotal = poOrder.CuryTaxTotal,
              ExpectedDate = poOrder.ExpectedDate,
              DocDate = poOrder.OrderDate,
              OwnerID = poOrder.OwnerID,
              DocDesc = poOrder.OrderDesc
            };
            POForSalesOrderDocument salesOrderDocument2 = ((PXSelectBase<POForSalesOrderDocument>) this.Documents).Locate(salesOrderDocument1);
            if (salesOrderDocument2 != null)
            {
              salesOrderDocument1.Selected = salesOrderDocument2.Selected;
              salesOrderDocument1.Excluded = salesOrderDocument2.Excluded;
            }
            salesOrderDocumentList.Add(((PXSelectBase<POForSalesOrderDocument>) this.Documents).Update(salesOrderDocument1));
          }
        }
        else if (((PXSelectBase<POForSalesOrderFilter>) this.Filter).Current.PODocType == "PR")
        {
          foreach (PXResult<PX.Objects.PO.POReceipt, PX.Objects.GL.Branch, PX.Objects.CR.BAccount, SOOrder> pxResult in ((PXSelectBase) new PXSelectJoin<PX.Objects.PO.POReceipt, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.PO.POReceipt.branchID>>, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>, And<PX.Objects.CR.BAccount.isBranch, Equal<True>>>, LeftJoin<SOOrder, On<SOOrder.intercompanyPOReturnNbr, Equal<PX.Objects.PO.POReceipt.receiptNbr>>>>>, Where2<Where<PX.Objects.PO.POReceipt.receiptDate, LessEqual<Current<POForSalesOrderFilter.docDate>>, Or<Current<POForSalesOrderFilter.docDate>, IsNull>>, And2<Where<PX.Objects.PO.POReceipt.vendorID, Equal<Current<POForSalesOrderFilter.sellingCompany>>, Or<Current<POForSalesOrderFilter.sellingCompany>, IsNull>>, And2<Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<POForSalesOrderFilter.purchasingCompany>>, Or<Current<POForSalesOrderFilter.purchasingCompany>, IsNull>>, And<PX.Objects.PO.POReceipt.isIntercompany, Equal<True>, And<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreturn>, And<PX.Objects.PO.POReceipt.released, Equal<True>, And<PX.Objects.PO.POReceipt.excludeFromIntercompanyProc, Equal<False>, And<SOOrder.orderNbr, IsNull>>>>>>>>>((PXGraph) this)).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, this.GetSortColumnsWithOriginalNames(false), PXView.Descendings, this.GetFiltersWithOriginalNames(false), ref startRow, PXView.MaximumRows, ref num))
          {
            PX.Objects.PO.POReceipt poReceipt = PXResult<PX.Objects.PO.POReceipt, PX.Objects.GL.Branch, PX.Objects.CR.BAccount, SOOrder>.op_Implicit(pxResult);
            POForSalesOrderDocument salesOrderDocument3 = new POForSalesOrderDocument()
            {
              DocType = poReceipt.ReceiptType,
              DocNbr = poReceipt.ReceiptNbr,
              VendorID = poReceipt.VendorID,
              BranchID = poReceipt.BranchID,
              CuryID = poReceipt.CuryID,
              CuryDocTotal = poReceipt.CuryOrderTotal,
              DocQty = poReceipt.OrderQty,
              DocDate = poReceipt.ReceiptDate,
              OwnerID = poReceipt.OwnerID,
              WorkgroupID = poReceipt.WorkgroupID,
              FinPeriodID = poReceipt.FinPeriodID
            };
            POForSalesOrderDocument salesOrderDocument4 = ((PXSelectBase<POForSalesOrderDocument>) this.Documents).Locate(salesOrderDocument3);
            if (salesOrderDocument4 != null)
            {
              salesOrderDocument3.Selected = salesOrderDocument4.Selected;
              salesOrderDocument3.Excluded = salesOrderDocument4.Excluded;
            }
            salesOrderDocumentList.Add(((PXSelectBase<POForSalesOrderDocument>) this.Documents).Update(salesOrderDocument3));
          }
        }
      }
    }
    PXView.StartRow = 0;
    return (IEnumerable) salesOrderDocumentList;
  }

  private string GetOriginalFieldName(string fieldName, bool isPurchaseOrder)
  {
    string str1 = isPurchaseOrder ? "POOrder" : "POReceipt";
    (string, string) tuple;
    if (!this.FieldsDictionary.TryGetValue(fieldName, out tuple))
      return fieldName;
    string str2 = isPurchaseOrder ? tuple.Item1 : tuple.Item2;
    return str2 == null ? fieldName : $"{str1}__{str2}";
  }

  private string[] GetSortColumnsWithOriginalNames(bool isPurchaseOrder)
  {
    return ((IEnumerable<string>) PXView.SortColumns).Select<string, string>((Func<string, string>) (col => this.GetOriginalFieldName(col, isPurchaseOrder))).ToArray<string>();
  }

  private PXFilterRow[] GetFiltersWithOriginalNames(bool isPurchaseOrder)
  {
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (PXFilterRow filter in PXView.Filters)
      pxFilterRowList.Add(new PXFilterRow(filter)
      {
        DataField = this.GetOriginalFieldName(filter.DataField, isPurchaseOrder)
      });
    return pxFilterRowList.ToArray();
  }

  public virtual void _(PX.Data.Events.RowSelected<POForSalesOrderFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOCreateIntercompanySalesOrders.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new SOCreateIntercompanySalesOrders.\u003C\u003Ec__DisplayClass13_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.filter = e.Row;
    // ISSUE: method pointer
    ((PXProcessingBase<POForSalesOrderDocument>) this.Documents).SetProcessDelegate(new PXProcessingBase<POForSalesOrderDocument>.ProcessListDelegate((object) cDisplayClass130, __methodptr(\u003C_\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass130.filter == null)
      return;
    // ISSUE: reference to a compiler-generated field
    bool flag = cDisplayClass130.filter.PODocType == "PO";
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetVisible<POForSalesOrderFilter.copyProjectDetails>(((PXSelectBase) this.Filter).Cache, (object) cDisplayClass130.filter, flag && PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>());
    PXUIFieldAttribute.SetVisible<POForSalesOrderDocument.expectedDate>(((PXSelectBase) this.Documents).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<POForSalesOrderDocument.curyID>(((PXSelectBase) this.Documents).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<POForSalesOrderDocument.docDesc>(((PXSelectBase) this.Documents).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<POForSalesOrderDocument.docQty>(((PXSelectBase) this.Documents).Cache, (object) null, !flag);
  }

  public virtual void _(PX.Data.Events.RowUpdated<POForSalesOrderFilter> e)
  {
    POForSalesOrderFilter row = e.Row;
    POForSalesOrderFilter oldRow = e.OldRow;
    if (row == null || oldRow == null || ((PXSelectBase) this.Filter).Cache.ObjectsEqual<POForSalesOrderFilter.docDate, POForSalesOrderFilter.pODocType, POForSalesOrderFilter.purchasingCompany, POForSalesOrderFilter.sellingCompany>((object) row, (object) oldRow))
      return;
    ((PXSelectBase) this.Documents).Cache.Clear();
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<POForSalesOrderFilter, POForSalesOrderFilter.pODocType> e)
  {
    POForSalesOrderFilter row = e.Row;
    if (row == null)
      return;
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<POForSalesOrderFilter.intercompanyOrderType>((object) row);
    if (!(row.PODocType == "PR"))
      return;
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<POForSalesOrderFilter.copyProjectDetails>((object) row);
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<POForSalesOrderFilter, POForSalesOrderFilter.sellingCompany> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXGraph) this).Accessinfo.BranchID
    }));
    if (branch == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POForSalesOrderFilter, POForSalesOrderFilter.sellingCompany>, POForSalesOrderFilter, object>) e).NewValue = (object) branch.BAccountID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POForSalesOrderFilter, POForSalesOrderFilter.sellingCompany>>) e).Cancel = true;
  }

  public static void GenerateSalesOrder(
    List<POForSalesOrderDocument> itemsList,
    POForSalesOrderFilter filter)
  {
    if (filter == null)
      return;
    POCreateSalesOrderProcess instance = PXGraph.CreateInstance<POCreateSalesOrderProcess>();
    if (filter.PODocType == "PO")
    {
      instance.GenerateSalesOrdersFromPurchaseOrders(itemsList, filter);
    }
    else
    {
      if (!(filter.PODocType == "PR"))
        return;
      instance.GenerateSalesOrdersFromPurchaseReturns(itemsList, filter);
    }
  }

  [PXUIField]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewPODocument(PXAdapter adapter)
  {
    POForSalesOrderDocument current1 = ((PXSelectBase<POForSalesOrderDocument>) this.Documents).Current;
    POForSalesOrderFilter current2 = ((PXSelectBase<POForSalesOrderFilter>) this.Filter).Current;
    if (current1 != null && current2 != null)
    {
      if (current2.PODocType == "PO")
      {
        POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
        ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = PX.Objects.PO.POOrder.PK.Find((PXGraph) instance, current1.DocType, current1.DocNbr, (PKFindOptions) 1);
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
      }
      else if (current2.PODocType == "PR")
      {
        POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
        ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Current = PX.Objects.PO.POReceipt.PK.Find((PXGraph) instance, "RN", current1.DocNbr, (PKFindOptions) 1);
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
      }
    }
    return (IEnumerable) ((PXSelectBase<POForSalesOrderFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewSOOrder(PXAdapter adapter)
  {
    POForSalesOrderDocument current = ((PXSelectBase<POForSalesOrderDocument>) this.Documents).Current;
    if (current != null)
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<SOOrder>) instance.Document).Current = SOOrder.PK.Find((PXGraph) instance, current.OrderType, current.OrderNbr, (PKFindOptions) 1);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return (IEnumerable) ((PXSelectBase<POForSalesOrderFilter>) this.Filter).Select(Array.Empty<object>());
  }

  public virtual bool IsDirty => false;
}
