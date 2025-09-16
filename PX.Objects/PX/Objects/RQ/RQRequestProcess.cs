// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Objects.RQ;

[TableAndChartDashboardType]
[Serializable]
public class RQRequestProcess : PXGraph<RQRequestProcess>
{
  public PXCancel<RQRequestSelection> Cancel;
  public PXFilter<RQRequestSelection> Filter;
  public PXFilter<PX.Objects.CR.BAccount> bAccount;
  public PXFilter<PX.Objects.AP.Vendor> Vendor;
  public PXSetup<RQSetup> Setup;
  [PXFilterable(new System.Type[] {})]
  public RQRequestProcess.RQRequestProcessing Records;

  public RQRequestProcess()
  {
    ((PXProcessingBase<RQRequestLineOwned>) this.Records).SetSelected<RQRequestLine.selected>();
  }

  public virtual void RQRequestSelection_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<RQRequestLineOwned>) this.Records).SetProcessDelegate(new PXProcessingBase<RQRequestLineOwned>.ProcessListDelegate((object) new RQRequestProcess.\u003C\u003Ec__DisplayClass7_0()
    {
      filter = ((PXSelectBase<RQRequestSelection>) this.Filter).Current
    }, __methodptr(\u003CRQRequestSelection_RowSelected\u003Eb__0)));
  }

  private static void GenerateRequisition(RQRequestSelection filter, List<RQRequestLineOwned> lines)
  {
    RQRequisitionEntry instance = PXGraph.CreateInstance<RQRequisitionEntry>();
    ((PXGraph) instance).Clear();
    try
    {
      RQRequestProcess.GenerateRequisition(filter, lines, instance);
    }
    catch (PXBaseRedirectException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
      for (int index = 0; index < lines.Count; ++index)
        PXProcessing<RQRequestLine>.SetError(index, ex);
      return;
    }
    for (int index = 0; index < lines.Count; ++index)
      PXProcessing<RQRequestLine>.SetInfo(index, PXMessages.LocalizeFormatNoPrefixNLA("Requisition '{0}' created.", new object[1]
      {
        (object) ((PXSelectBase<RQRequisition>) instance.Document).Current.ReqNbr
      }));
  }

  private static void GenerateRequisition(
    RQRequestSelection filter,
    List<RQRequestLineOwned> lines,
    RQRequisitionEntry graph)
  {
    RQRequisition rqRequisition1 = (RQRequisition) ((PXSelectBase) graph.Document).Cache.CreateInstance();
    rqRequisition1.SkipValidateWithVendorCuryOrRate = new bool?(lines.Count > 1 && lines.GroupBy<RQRequestLineOwned, int?>((Func<RQRequestLineOwned, int?>) (e => e.VendorID)).Count<IGrouping<int?, RQRequestLineOwned>>() > 1);
    ((PXSelectBase<RQRequisition>) graph.Document).Insert(rqRequisition1);
    rqRequisition1.ShipDestType = (string) null;
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = true;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    HashSet<RQRequestProcess.VendorRef> vendorRefSet = new HashSet<RQRequestProcess.VendorRef>();
    foreach (RQRequestLine line1 in lines)
    {
      PXResult<RQRequest, RQRequestClass> pxResult = (PXResult<RQRequest, RQRequestClass>) PXResultset<RQRequest>.op_Implicit(PXSelectBase<RQRequest, PXSelectJoin<RQRequest, InnerJoin<RQRequestClass, On<RQRequestClass.reqClassID, Equal<RQRequest.reqClassID>>>, Where<RQRequest.orderNbr, Equal<Required<RQRequest.orderNbr>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) line1.OrderNbr
      }));
      RQRequest rqRequest = PXResult<RQRequest, RQRequestClass>.op_Implicit(pxResult);
      RQRequestClass rqRequestClass = PXResult<RQRequest, RQRequestClass>.op_Implicit(pxResult);
      rqRequisition1 = PXCache<RQRequisition>.CreateCopy(((PXSelectBase<RQRequisition>) graph.Document).Current);
      bool? nullable3 = rqRequestClass.CustomerRequest;
      int? nullable4;
      int? nullable5;
      if (nullable3.GetValueOrDefault() & flag1)
      {
        nullable4 = rqRequisition1.CustomerID;
        if (!nullable4.HasValue)
        {
          rqRequisition1.CustomerID = rqRequest.EmployeeID;
          rqRequisition1.CustomerLocationID = rqRequest.LocationID;
        }
        else
        {
          nullable4 = rqRequisition1.CustomerID;
          nullable5 = rqRequest.EmployeeID;
          if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
          {
            nullable5 = rqRequisition1.CustomerLocationID;
            nullable4 = rqRequest.LocationID;
            if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
              goto label_9;
          }
          flag1 = false;
        }
      }
      else
        flag1 = false;
label_9:
      if (flag3)
      {
        if (!nullable1.HasValue && !nullable2.HasValue)
        {
          rqRequisition1.ShipDestType = rqRequest.ShipDestType;
          rqRequisition1.SiteID = rqRequest.SiteID;
          rqRequisition1.ShipToBAccountID = rqRequest.ShipToBAccountID;
          rqRequisition1.ShipToLocationID = rqRequest.ShipToLocationID;
          nullable1 = rqRequest.ShipContactID;
          nullable2 = rqRequest.ShipAddressID;
        }
        else
        {
          if (!(rqRequisition1.ShipDestType != rqRequest.ShipDestType))
          {
            if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
            {
              nullable4 = rqRequisition1.SiteID;
              nullable5 = rqRequest.SiteID;
              if (!(nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue))
                goto label_17;
            }
            nullable5 = rqRequisition1.ShipToBAccountID;
            nullable4 = rqRequest.ShipToBAccountID;
            if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
            {
              nullable4 = rqRequisition1.ShipToLocationID;
              nullable5 = rqRequest.ShipToLocationID;
              if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
                goto label_18;
            }
          }
label_17:
          flag3 = false;
        }
      }
label_18:
      nullable5 = line1.VendorID;
      if (nullable5.HasValue)
      {
        nullable5 = line1.VendorLocationID;
        if (nullable5.HasValue)
        {
          RQRequestProcess.VendorRef vendorRef1 = new RQRequestProcess.VendorRef();
          ref RQRequestProcess.VendorRef local1 = ref vendorRef1;
          nullable5 = line1.VendorID;
          int num1 = nullable5.Value;
          local1.VendorID = num1;
          ref RQRequestProcess.VendorRef local2 = ref vendorRef1;
          nullable5 = line1.VendorLocationID;
          int num2 = nullable5.Value;
          local2.LocationID = num2;
          RQRequestProcess.VendorRef vendorRef2 = vendorRef1;
          vendorRefSet.Add(vendorRef2);
          if (flag2)
          {
            nullable5 = rqRequisition1.VendorID;
            if (!nullable5.HasValue)
            {
              rqRequisition1.VendorID = line1.VendorID;
              rqRequisition1.VendorLocationID = line1.VendorLocationID;
              goto label_27;
            }
            nullable5 = rqRequisition1.VendorID;
            nullable4 = line1.VendorID;
            if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
            {
              nullable4 = rqRequisition1.VendorLocationID;
              nullable5 = line1.VendorLocationID;
              if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
                goto label_27;
            }
            flag2 = false;
            goto label_27;
          }
          goto label_27;
        }
      }
      flag2 = false;
label_27:
      if (!flag1)
      {
        RQRequisition rqRequisition2 = rqRequisition1;
        nullable5 = new int?();
        int? nullable6 = nullable5;
        rqRequisition2.CustomerID = nullable6;
        RQRequisition rqRequisition3 = rqRequisition1;
        nullable5 = new int?();
        int? nullable7 = nullable5;
        rqRequisition3.CustomerLocationID = nullable7;
      }
      if (!flag2)
      {
        RQRequisition rqRequisition4 = rqRequisition1;
        nullable5 = new int?();
        int? nullable8 = nullable5;
        rqRequisition4.VendorID = nullable8;
        RQRequisition rqRequisition5 = rqRequisition1;
        nullable5 = new int?();
        int? nullable9 = nullable5;
        rqRequisition5.VendorLocationID = nullable9;
        RQRequisition rqRequisition6 = rqRequisition1;
        nullable5 = new int?();
        int? nullable10 = nullable5;
        rqRequisition6.RemitAddressID = nullable10;
        RQRequisition rqRequisition7 = rqRequisition1;
        nullable5 = new int?();
        int? nullable11 = nullable5;
        rqRequisition7.RemitContactID = nullable11;
      }
      else
      {
        nullable5 = rqRequisition1.VendorID;
        nullable4 = rqRequest.VendorID;
        if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
        {
          nullable4 = rqRequisition1.VendorLocationID;
          nullable5 = rqRequest.VendorLocationID;
          if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
          {
            rqRequisition1.RemitAddressID = rqRequest.RemitAddressID;
            rqRequisition1.RemitContactID = rqRequest.RemitContactID;
          }
        }
      }
      if (!flag3)
      {
        rqRequisition1.ShipDestType = "L";
        ((PXSelectBase) graph.Document).Cache.SetDefaultExt<RQRequisition.shipToBAccountID>((object) rqRequisition1);
      }
      ((PXSelectBase<RQRequisition>) graph.Document).Update(rqRequisition1);
      Decimal? openQty = line1.OpenQty;
      Decimal num3 = 0M;
      if (openQty.GetValueOrDefault() > num3 & openQty.HasValue)
      {
        if (!((PXSelectBase) graph.Lines).Cache.IsDirty && rqRequest.CuryID != rqRequisition1.CuryID)
        {
          rqRequisition1 = PXCache<RQRequisition>.CreateCopy(((PXSelectBase<RQRequisition>) graph.Document).Current);
          PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) graph.currencyinfo).Select(Array.Empty<object>()));
          PXCache<PX.Objects.CM.CurrencyInfo>.RestoreCopy(currencyInfo, PX.Objects.CM.CurrencyInfo.PK.Find((PXGraph) graph, rqRequest.CuryInfoID));
          currencyInfo.CuryInfoID = rqRequisition1.CuryInfoID;
          currencyInfo.SetCuryEffDate(((PXSelectBase) graph.currencyinfo).Cache, (object) rqRequisition1.OrderDate);
          rqRequisition1.CuryID = currencyInfo.CuryID;
          ((PXSelectBase<RQRequisition>) graph.Document).Update(rqRequisition1);
        }
        RQRequisitionEntry requisitionEntry = graph;
        RQRequestLine line2 = line1;
        openQty = line1.OpenQty;
        Decimal valueOrDefault = openQty.GetValueOrDefault();
        nullable3 = filter.AddExists;
        int num4 = nullable3.GetValueOrDefault() ? 1 : 0;
        requisitionEntry.InsertRequestLine(line2, valueOrDefault, num4 != 0);
      }
    }
    if (flag3)
    {
      foreach (PXResult<POAddress, POContact> pxResult in PXSelectBase<POAddress, PXSelectJoin<POAddress, CrossJoin<POContact>, Where<POAddress.addressID, Equal<Required<RQRequisition.shipAddressID>>, And<POContact.contactID, Equal<Required<RQRequisition.shipContactID>>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) nullable2,
        (object) nullable1
      }))
      {
        SharedRecordAttribute.CopyRecord<RQRequisition.shipAddressID>(((PXSelectBase) graph.Document).Cache, (object) ((PXSelectBase<RQRequisition>) graph.Document).Current, (object) PXResult<POAddress, POContact>.op_Implicit(pxResult), true);
        SharedRecordAttribute.CopyRecord<RQRequisition.shipContactID>(((PXSelectBase) graph.Document).Cache, (object) ((PXSelectBase<RQRequisition>) graph.Document).Current, (object) PXResult<POAddress, POContact>.op_Implicit(pxResult), true);
      }
    }
    if (!rqRequisition1.VendorID.HasValue && vendorRefSet.Count > 0)
    {
      foreach (RQRequestProcess.VendorRef vendorRef in vendorRefSet)
      {
        RQBiddingVendor copy = PXCache<RQBiddingVendor>.CreateCopy(((PXSelectBase<RQBiddingVendor>) graph.Vendors).Insert());
        copy.VendorID = new int?(vendorRef.VendorID);
        copy.VendorLocationID = new int?(vendorRef.LocationID);
        ((PXSelectBase<RQBiddingVendor>) graph.Vendors).Update(copy);
      }
    }
    if (((PXSelectBase) graph.Lines).Cache.IsDirty)
    {
      ((PXAction) graph.Save).Press();
      throw new PXRedirectRequiredException((PXGraph) graph, $"Requisition '{((PXSelectBase<RQRequisition>) graph.Document).Current.ReqNbr}' created.");
    }
  }

  public class RQRequestProcessing : 
    PXFilteredProcessingJoin<RQRequestLineOwned, RQRequestSelection, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<RQRequestLineOwned.employeeID>>>, Where2<Where<Current<RQRequestSelection.selectedPriority>, Equal<AllPriority>, Or<RQRequestLineOwned.priority, Equal<Current<RQRequestSelection.selectedPriority>>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<RQRequestLineOwned.priority, Asc<RQRequestLineOwned.orderNbr, Asc<RQRequestLineOwned.lineNbr>>>>>
  {
    public RQRequestProcessing(PXGraph graph)
      : base(graph)
    {
      ((PXProcessingBase<RQRequestLineOwned>) this)._OuterView.WhereAndCurrent<RQRequestSelection>(typeof (RQRequestSelection.workGroupID).Name, typeof (RQRequestSelection.ownerID).Name);
    }

    public RQRequestProcessing(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
      ((PXProcessingBase<RQRequestLineOwned>) this)._OuterView.WhereAndCurrent<RQRequestSelection>(typeof (RQRequestSelection.workGroupID).Name, typeof (RQRequestSelection.ownerID).Name);
    }

    [PXUIField]
    [PXProcessButton]
    protected virtual IEnumerable Process(PXAdapter adapter)
    {
      return this.CheckCustomer(adapter, true) ? ((PXProcessing<RQRequestLineOwned>) this).Process(adapter) : adapter.Get();
    }

    [PXUIField]
    [PXProcessButton]
    protected virtual IEnumerable ProcessAll(PXAdapter adapter)
    {
      return this.CheckCustomer(adapter, false) ? ((PXFilteredProcessingBase<RQRequestLineOwned, RQRequestSelection>) this).ProcessAll(adapter) : adapter.Get();
    }

    private bool CheckCustomer(PXAdapter adapter, bool onlySelected)
    {
      try
      {
        if (HttpContext.Current == null)
          return true;
      }
      catch (Exception ex)
      {
        return true;
      }
      RQRequestLineOwned requestLineOwned1 = (RQRequestLineOwned) null;
      bool flag = false;
      foreach (RQRequestLineOwned requestLineOwned2 in onlySelected ? (IEnumerable<RQRequestLineOwned>) ((PXProcessingBase<RQRequestLineOwned>) this).GetSelectedItems(((PXSelectBase) this).View.Cache, ((PXSelectBase) this).View.Cache.Cached) : GraphHelper.RowCast<RQRequestLineOwned>((IEnumerable) ((PXSelectBase) this).View.SelectMulti(Array.Empty<object>())))
      {
        if (requestLineOwned1 != null)
        {
          bool? customerRequest1 = requestLineOwned1.CustomerRequest;
          if (customerRequest1.GetValueOrDefault())
          {
            int? nullable1 = requestLineOwned1.EmployeeID;
            int? nullable2 = requestLineOwned2.EmployeeID;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            {
              nullable2 = requestLineOwned1.LocationID;
              nullable1 = requestLineOwned2.LocationID;
              if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                goto label_12;
            }
          }
          customerRequest1 = requestLineOwned1.CustomerRequest;
          bool? customerRequest2 = requestLineOwned2.CustomerRequest;
          if (customerRequest1.GetValueOrDefault() == customerRequest2.GetValueOrDefault() & customerRequest1.HasValue == customerRequest2.HasValue)
          {
            customerRequest2 = requestLineOwned2.CustomerRequest;
            if (!customerRequest2.GetValueOrDefault())
              goto label_12;
          }
          flag = true;
          break;
        }
label_12:
        requestLineOwned1 = requestLineOwned2;
      }
      return !flag || ((PXSelectBase<RQRequestLineOwned>) this).Ask("Confirmation", "Some of processed request items have different data (Customers, Location etc). Continue to create requisition without customer specified?", (MessageButtons) 4) == 6;
    }
  }

  public struct VendorRef
  {
    public int VendorID;
    public int LocationID;
  }
}
