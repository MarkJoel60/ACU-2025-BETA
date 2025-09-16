// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SalesPersonMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class SalesPersonMaint : PXGraph<SalesPersonMaint, SalesPerson>
{
  public PXSelect<SalesPerson> Salesperson;
  public PXSelect<SalesPerson, Where<SalesPerson.salesPersonID, Equal<Current<SalesPerson.salesPersonID>>>> SalespersonCurrent;
  public PXSelectJoin<CustSalesPeople, InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<CustSalesPeople.bAccountID>>>, Where<CustSalesPeople.salesPersonID, Equal<Current<SalesPerson.salesPersonID>>, And<Match<Customer, Current<AccessInfo.userName>>>>> SPCustomers;
  public PXSelectGroupBy<ARSPCommnHistory, Where<ARSPCommnHistory.salesPersonID, Equal<Current<SalesPerson.salesPersonID>>>, Aggregate<Sum<ARSPCommnHistory.commnAmt, Sum<ARSPCommnHistory.commnblAmt, GroupBy<ARSPCommnHistory.commnPeriod, GroupBy<ARSPCommnHistory.baseCuryID, Max<ARSPCommnHistory.pRProcessedDate>>>>>>> CommissionsHistory;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXAction<SalesPerson> viewDetails;

  public SalesPersonMaint()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.CommissionsHistory).Cache.AllowInsert = false;
    ((PXSelectBase) this.CommissionsHistory).Cache.AllowDelete = false;
    ((PXSelectBase) this.CommissionsHistory).Cache.AllowUpdate = false;
    PXUIFieldAttribute.SetEnabled<CustSalesPeople.locationID>(((PXSelectBase) this.SPCustomers).Cache, (object) null, false);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.salutation>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], "Attention");
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<ARSPCommnHistory>) this.CommissionsHistory).Current != null)
    {
      ARSPCommnHistory current1 = ((PXSelectBase<ARSPCommnHistory>) this.CommissionsHistory).Current;
      ARSPCommissionDocEnq instance = PXGraph.CreateInstance<ARSPCommissionDocEnq>();
      SPDocFilter current2 = ((PXSelectBase<SPDocFilter>) instance.Filter).Current;
      current2.SalesPersonID = current1.SalesPersonID;
      current2.CommnPeriod = current1.CommnPeriod;
      ((PXSelectBase<SPDocFilter>) instance.Filter).Update(current2);
      throw new PXRedirectRequiredException((PXGraph) instance, "Document");
    }
    return adapter.Get();
  }

  protected virtual void SalesPerson_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if ((ARSalesPerTran) ((PXSelectBase) new PXSelect<ARSalesPerTran, Where<ARSalesPerTran.salespersonID, Equal<Required<ARSalesPerTran.salespersonID>>>>((PXGraph) this)).View.SelectSingle(new object[1]
    {
      (object) ((SalesPerson) e.Row).SalesPersonID
    }) != null)
      throw new PXException("One or more AR transactions exists for the selected Sales Person. This record cannot be deleted.");
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void CustSalesPeople_IsDefault_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Currency", Required = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARSPCommnHistory.baseCuryID> e)
  {
  }

  protected virtual void CustSalesPeople_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    CustSalesPeople row = (CustSalesPeople) e.Row;
    if (row == null)
      return;
    int? nullable = row.BAccountID;
    if (!nullable.HasValue)
      return;
    List<CustSalesPeople> custSalesPeopleList = new List<CustSalesPeople>();
    bool flag = false;
    foreach (PXResult<CustSalesPeople> pxResult in ((PXSelectBase<CustSalesPeople>) this.SPCustomers).Select(Array.Empty<object>()))
    {
      CustSalesPeople custSalesPeople = PXResult<CustSalesPeople>.op_Implicit(pxResult);
      nullable = row.BAccountID;
      int? baccountId = custSalesPeople.BAccountID;
      if (nullable.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable.HasValue == baccountId.HasValue)
      {
        custSalesPeopleList.Add(custSalesPeople);
        int? locationId = row.LocationID;
        nullable = custSalesPeople.LocationID;
        if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
          flag = true;
      }
    }
    if (!flag)
      return;
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) null;
    foreach (PXResult<PX.Objects.CR.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Location>) new PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) row.BAccountID
    }))
    {
      PX.Objects.CR.Location iLoc = PXResult<PX.Objects.CR.Location>.op_Implicit(pxResult);
      if (!custSalesPeopleList.Exists((Predicate<CustSalesPeople>) (op =>
      {
        int? locationId1 = op.LocationID;
        int? locationId2 = iLoc.LocationID;
        return locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue;
      })))
      {
        location = iLoc;
        break;
      }
    }
    row.LocationID = location != null ? location.LocationID : throw new PXException("All Customer locations has been added already.");
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (False))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARSPCommnHistory.commnPeriod> e)
  {
  }
}
