// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POCreateIntercompanySalesOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO;

[TableAndChartDashboardType]
public class POCreateIntercompanySalesOrder : PXGraph<POCreateIntercompanySalesOrder>
{
  public PXCancel<SOForPurchaseReceiptFilter> Cancel;
  public PXFilter<SOForPurchaseReceiptFilter> Filter;
  public PXSetup<PX.Objects.PO.POSetup> POSetup;
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  public PXSelectReadonly<PX.Objects.SO.SOOrder, Where<True, Equal<False>>> Order;
  private readonly string IntercompanyPONbrFieldName = "SOOrder__IntercompanyPONbr";
  [PXFilterable(new System.Type[] {})]
  [PXVirtualDAC]
  public PXFilteredProcessingOrderBy<PX.Objects.SO.SOShipment, SOForPurchaseReceiptFilter, OrderBy<Asc<PX.Objects.SO.SOShipment.shipmentNbr>>> Documents;
  public PXAction<SOForPurchaseReceiptFilter> viewSODocument;
  public PXAction<POForSalesOrderFilter> viewPOReceipt;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [PXMergeAttributes]
  [PXDefault("RO")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.intercompanyPOType> eventArgs)
  {
  }

  public POCreateIntercompanySalesOrder()
  {
    PX.Objects.PO.POSetup current1 = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current;
    PX.Objects.IN.INSetup current2 = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
    ((PXProcessingBase<PX.Objects.SO.SOShipment>) this.Documents).SetSelected<PX.Objects.SO.SOShipment.selected>();
    ((PXProcessing<PX.Objects.SO.SOShipment>) this.Documents).SetProcessCaption("Process");
    ((PXProcessing<PX.Objects.SO.SOShipment>) this.Documents).SetProcessAllCaption("Process All");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.SO.SOShipment.customerID>(((PXSelectBase) this.Documents).Cache, "Purchasing Company");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.SO.SOOrder.branchID>(((PXSelectBase) this.Order).Cache, "Selling Company");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.SO.SOOrder.intercompanyPONbr>(((PXSelectBase) this.Order).Cache, "Related PO Nbr.");
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOShipment.workgroupID>(((PXSelectBase) this.Documents).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOShipment.shipmentWeight>(((PXSelectBase) this.Documents).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOShipment.shipmentVolume>(((PXSelectBase) this.Documents).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOShipment.packageCount>(((PXSelectBase) this.Documents).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOShipment.packageWeight>(((PXSelectBase) this.Documents).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOShipment.status>(((PXSelectBase) this.Documents).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOShipment.excluded>(((PXSelectBase) this.Documents).Cache, (object) null, PXLongOperation.GetStatus(((PXGraph) this).UID) == 0);
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (PX.Objects.CR.BAccount), typeof (PX.Objects.CR.BAccount));
  }

  protected virtual IEnumerable documents()
  {
    PXDelegateResult delegateResult = new PXDelegateResult();
    int totalRows = 0;
    bool filtersContainNonDbField = false;
    bool sortsContainNonDBField = false;
    if (((PXSelectBase<SOForPurchaseReceiptFilter>) this.Filter).Current != null)
    {
      bool[] newDescendings;
      string[] sortColumns = this.GetSortColumns(out sortsContainNonDBField, out newDescendings);
      PXFilterRow[] filters = this.GetFilters(out filtersContainNonDbField);
      int startRow = filtersContainNonDbField | sortsContainNonDBField ? 0 : PXView.StartRow;
      int maximumRows = filtersContainNonDbField | sortsContainNonDBField ? 0 : PXView.MaximumRows;
      using (new PXReadBranchRestrictedScope())
      {
        foreach (PXResult<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.GL.Branch, PX.Objects.CR.BAccount, POReceipt> pxResult in ((PXSelectBase) new PXSelectJoinGroupBy<PX.Objects.SO.SOShipment, InnerJoin<PX.Objects.SO.SOOrderShipment, On<PX.Objects.SO.SOOrderShipment.FK.Shipment>, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrderShipment.FK.Order>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<PX.Objects.SO.SOOrder.branchID>>, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>, And<PX.Objects.CR.BAccount.isBranch, Equal<True>>>, LeftJoin<POReceipt, On<Where2<POReceipt.FK.IntercompanyShipment, And<POReceipt.canceled, Equal<False>>>>>>>>>, Where2<Where<PX.Objects.SO.SOShipment.shipDate, LessEqual<Current<SOForPurchaseReceiptFilter.docDate>>, Or<Current<SOForPurchaseReceiptFilter.docDate>, IsNull>>, And2<Where<PX.Objects.SO.SOShipment.customerID, Equal<Current<SOForPurchaseReceiptFilter.purchasingCompany>>, Or<Current<SOForPurchaseReceiptFilter.purchasingCompany>, IsNull>>, And2<Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<SOForPurchaseReceiptFilter.sellingCompany>>, Or<Current<SOForPurchaseReceiptFilter.sellingCompany>, IsNull>>, And<PX.Objects.SO.SOShipment.shipmentType, Equal<INDocType.issue>, And<PX.Objects.SO.SOShipment.operation, Equal<SOOperation.issue>, And<POReceipt.intercompanyShipmentNbr, IsNull, And<PX.Objects.SO.SOShipment.isIntercompany, Equal<True>, And<PX.Objects.SO.SOShipment.excludeFromIntercompanyProc, Equal<False>, And<PX.Objects.SO.SOShipment.confirmed, Equal<True>>>>>>>>>>, Aggregate<GroupBy<PX.Objects.SO.SOShipment.shipmentNbr, GroupBy<PX.Objects.SO.SOShipment.shipmentType, Count<PX.Objects.SO.SOOrder.intercompanyPONbr>>>>>((PXGraph) this)).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, sortColumns, newDescendings, filters, ref startRow, maximumRows, ref totalRows))
        {
          PX.Objects.SO.SOShipment soShipment1 = PXResult.Unwrap<PX.Objects.SO.SOShipment>((object) pxResult);
          PX.Objects.SO.SOShipment soShipment2 = (PX.Objects.SO.SOShipment) ((PXSelectBase) this.Documents).Cache.Locate((object) soShipment1);
          if (soShipment2 == null)
          {
            ((PXSelectBase) this.Documents).Cache.SetStatus((object) soShipment1, (PXEntryStatus) 5);
          }
          else
          {
            soShipment1.Selected = soShipment2.Selected;
            soShipment1.Excluded = soShipment2.Excluded;
          }
          if (((PXResult) pxResult).RowCount.GetValueOrDefault() != 1)
            PXResult<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.GL.Branch, PX.Objects.CR.BAccount, POReceipt>.op_Implicit(pxResult).IntercompanyPONbr = (string) null;
          ((List<object>) delegateResult).Add((object) pxResult);
        }
      }
    }
    if (!filtersContainNonDbField && !sortsContainNonDBField)
      PXView.StartRow = 0;
    return (IEnumerable) this.CreateDelegateResult(delegateResult, totalRows, filtersContainNonDbField, sortsContainNonDBField);
  }

  protected virtual PXDelegateResult CreateDelegateResult(
    PXDelegateResult delegateResult,
    int totalRows,
    bool filtersContainNonDBField,
    bool sortsContainNonDBField)
  {
    delegateResult.IsResultFiltered = !filtersContainNonDBField;
    delegateResult.IsResultTruncated = totalRows > ((List<object>) delegateResult).Count;
    delegateResult.IsResultSorted = !sortsContainNonDBField && !PXView.ReverseOrder;
    return delegateResult;
  }

  protected virtual string[] GetSortColumns(
    out bool sortsContainNonDBField,
    out bool[] newDescendings)
  {
    int index1 = Array.FindIndex<string>(PXView.SortColumns, (Predicate<string>) (c => c.Equals(this.IntercompanyPONbrFieldName, StringComparison.OrdinalIgnoreCase)));
    if (index1 == -1)
    {
      newDescendings = ((IEnumerable<bool>) PXView.Descendings).ToArray<bool>();
      sortsContainNonDBField = false;
      return ((IEnumerable<string>) PXView.SortColumns).ToArray<string>();
    }
    sortsContainNonDBField = true;
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();
    for (int index2 = 0; index2 < PXView.SortColumns.Length; ++index2)
    {
      if (index2 != index1)
      {
        stringList.Add(PXView.SortColumns[index2]);
        boolList.Add(PXView.Descendings[index2]);
      }
    }
    newDescendings = boolList.ToArray();
    return stringList.ToArray();
  }

  protected virtual PXFilterRow[] GetFilters(out bool filtersContainNonDbField)
  {
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (PXFilterRow filter in PXView.Filters)
    {
      if (!filter.DataField.Equals(this.IntercompanyPONbrFieldName, StringComparison.OrdinalIgnoreCase))
        pxFilterRowList.Add(filter);
    }
    filtersContainNonDbField = pxFilterRowList.Count != PXView.Filters.Length;
    return pxFilterRowList.ToArray();
  }

  public virtual void _(PX.Data.Events.RowSelected<SOForPurchaseReceiptFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<PX.Objects.SO.SOShipment>) this.Documents).SetProcessDelegate(new PXProcessingBase<PX.Objects.SO.SOShipment>.ProcessListDelegate((object) new POCreateIntercompanySalesOrder.\u003C\u003Ec__DisplayClass15_0()
    {
      filter = e.Row
    }, __methodptr(\u003C_\u003Eb__0)));
  }

  public virtual void _(PX.Data.Events.RowUpdated<SOForPurchaseReceiptFilter> e)
  {
    SOForPurchaseReceiptFilter row = e.Row;
    SOForPurchaseReceiptFilter oldRow = e.OldRow;
    if (row == null || oldRow == null || ((PXSelectBase) this.Filter).Cache.ObjectsEqual<SOForPurchaseReceiptFilter.docDate, SOForPurchaseReceiptFilter.purchasingCompany, SOForPurchaseReceiptFilter.sellingCompany>((object) row, (object) oldRow))
      return;
    ((PXSelectBase) this.Documents).Cache.Clear();
    ((PXSelectBase) this.Documents).Cache.ClearQueryCache();
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<SOForPurchaseReceiptFilter, SOForPurchaseReceiptFilter.purchasingCompany> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXGraph) this).Accessinfo.BranchID
    }));
    if (branch == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOForPurchaseReceiptFilter, SOForPurchaseReceiptFilter.purchasingCompany>, SOForPurchaseReceiptFilter, object>) e).NewValue = (object) branch.BAccountID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOForPurchaseReceiptFilter, SOForPurchaseReceiptFilter.purchasingCompany>>) e).Cancel = true;
  }

  public static void GeneratePurchaseReceipt(
    List<PX.Objects.SO.SOShipment> itemsList,
    SOForPurchaseReceiptFilter filter)
  {
    if (filter == null)
      return;
    PXGraph.CreateInstance<SOCreatePurchaseReceiptProcess>().GeneratePurchaseReceiptsFromShipment(itemsList, filter);
  }

  [PXUIField]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewSODocument(PXAdapter adapter)
  {
    PX.Objects.SO.SOShipment current1 = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Documents).Current;
    SOForPurchaseReceiptFilter current2 = ((PXSelectBase<SOForPurchaseReceiptFilter>) this.Filter).Current;
    if (current1 != null && current2 != null)
    {
      SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
      ((PXSelectBase<PX.Objects.SO.SOShipment>) instance.Document).Current = PX.Objects.SO.SOShipment.PK.Find((PXGraph) instance, current1.ShipmentNbr, (PKFindOptions) 1);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return (IEnumerable) ((PXSelectBase<SOForPurchaseReceiptFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewPOReceipt(PXAdapter adapter)
  {
    PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Documents).Current;
    if (current != null)
    {
      POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
      ((PXSelectBase<POReceipt>) instance.Document).Current = POReceipt.PK.Find((PXGraph) instance, "RT", current.IntercompanyPOReceiptNbr, (PKFindOptions) 1);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return (IEnumerable) ((PXSelectBase<SOForPurchaseReceiptFilter>) this.Filter).Select(Array.Empty<object>());
  }

  public virtual bool IsDirty => false;
}
