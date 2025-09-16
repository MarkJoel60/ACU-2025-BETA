// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPOReceiptProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class FSPOReceiptProcess : PXGraph<FSPOReceiptProcess>
{
  public static FSPOReceiptProcess SingleFSProcessPOReceipt
  {
    get
    {
      return PXContext.GetSlot<FSPOReceiptProcess>() ?? PXContext.SetSlot<FSPOReceiptProcess>(PXGraph.CreateInstance<FSPOReceiptProcess>());
    }
  }

  public static List<PXResult<INItemPlan, INPlanType>> ProcessPOReceipt(
    PXGraph graph,
    IEnumerable<PXResult<INItemPlan, INPlanType>> list,
    string POReceiptType,
    string POReceiptNbr,
    bool stockItemProcessing)
  {
    return FSPOReceiptProcess.SingleFSProcessPOReceipt.ProcessPOReceiptInt(graph, list, POReceiptType, POReceiptNbr, stockItemProcessing);
  }

  public virtual List<PXResult<INItemPlan, INPlanType>> ProcessPOReceiptInt(
    PXGraph graph,
    IEnumerable<PXResult<INItemPlan, INPlanType>> list,
    string POReceiptType,
    string POReceiptNbr,
    bool stockItemProcessing)
  {
    PXSelect<FSServiceOrder> fsServiceOrder = new PXSelect<FSServiceOrder>(graph);
    if (!graph.Views.Caches.Contains(typeof (FSServiceOrder)))
      graph.Views.Caches.Add(typeof (FSServiceOrder));
    PXSelect<FSSODetSplit> pxSelect = new PXSelect<FSSODetSplit>(graph);
    if (!graph.Views.Caches.Contains(typeof (FSSODetSplit)))
      graph.Views.Caches.Add(typeof (FSSODetSplit));
    PXSelect<INItemPlan> initemplan = new PXSelect<INItemPlan>(graph);
    List<PXResult<INItemPlan, INPlanType>> pxResultList = new List<PXResult<INItemPlan, INPlanType>>();
    List<FSSODetSplit> fssoDetSplitList1 = new List<FSSODetSplit>();
    List<FSSODetSplit> fssoDetSplitList2 = new List<FSSODetSplit>();
    List<INItemPlan> source = new List<INItemPlan>();
    PXSelect<FSSODet> soDetView = new PXSelect<FSSODet>(graph);
    if (!graph.Views.Caches.Contains(typeof (FSSODet)))
      graph.Views.Caches.Add(typeof (FSSODet));
    PXSelect<FSAppointment> appointmentView = new PXSelect<FSAppointment>(graph);
    if (!graph.Views.Caches.Contains(typeof (FSAppointment)))
      graph.Views.Caches.Add(typeof (FSAppointment));
    PXSelect<FSAppointmentDet> apptLineView = new PXSelect<FSAppointmentDet>(graph);
    if (!graph.Views.Caches.Contains(typeof (FSAppointmentDet)))
      graph.Views.Caches.Add(typeof (FSAppointmentDet));
    PXSelect<FSApptLineSplit> apptLineSplitView = new PXSelect<FSApptLineSplit>(graph);
    if (!graph.Views.Caches.Contains(typeof (FSApptLineSplit)))
      graph.Views.Caches.Add(typeof (FSApptLineSplit));
    List<FSPOReceiptProcess.SrvOrdLineWithSplits> ordLineWithSplitsList = new List<FSPOReceiptProcess.SrvOrdLineWithSplits>();
    FSSODet srvOrdLine = (FSSODet) null;
    foreach (PXResult<INItemPlan, INPlanType> pxResult1 in list)
    {
      bool flag1 = true;
      INItemPlan copy1 = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan, INPlanType>.op_Implicit(pxResult1));
      INPlanType inPlanType = PXResult<INItemPlan, INPlanType>.op_Implicit(pxResult1);
      if (((PXSelectBase) initemplan).Cache.GetStatus((object) copy1) != 2)
        ((PXSelectBase) initemplan).Cache.SetStatus((object) copy1, (PXEntryStatus) 0);
      FSSODetSplit fssoDetSplit1 = PXResultset<FSSODetSplit>.op_Implicit(PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.planID, Equal<Required<FSSODetSplit.planID>>>>.Config>.Select(graph, new object[1]
      {
        (object) copy1.DemandPlanID
      }));
      if (fssoDetSplit1 != null)
      {
        bool? completed = fssoDetSplit1.Completed;
        bool flag2 = false;
        if (completed.GetValueOrDefault() == flag2 & completed.HasValue || ((PXSelectBase) pxSelect).Cache.GetStatus((object) fssoDetSplit1) == 1)
        {
          flag1 = false;
          FSSODetSplit copy2 = PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit1);
          FSSODetSplit fssoDetSplit2 = copy2;
          Decimal? nullable1 = fssoDetSplit2.BaseReceivedQty;
          Decimal? nullable2 = copy1.PlanQty;
          fssoDetSplit2.BaseReceivedQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          FSSODetSplit fssoDetSplit3 = copy2;
          PXCache cache1 = ((PXSelectBase) pxSelect).Cache;
          int? inventoryId1 = copy2.InventoryID;
          string uom1 = copy2.UOM;
          nullable2 = copy2.BaseReceivedQty;
          Decimal num1 = nullable2.Value;
          Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromBase(cache1, inventoryId1, uom1, num1, INPrecision.QUANTITY));
          fssoDetSplit3.ReceivedQty = nullable3;
          FSSODetSplit fssoDetSplit4 = (FSSODetSplit) ((PXSelectBase) pxSelect).Cache.Update((object) copy2);
          INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select(graph, new object[1]
          {
            (object) copy1.DemandPlanID
          }));
          if (inItemPlan1 != null)
          {
            INItemPlan inItemPlan2 = inItemPlan1;
            nullable2 = fssoDetSplit4.BaseQty;
            nullable1 = fssoDetSplit4.BaseReceivedQty;
            Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
            inItemPlan2.PlanQty = nullable4;
            ((PXSelectBase) initemplan).Cache.Update((object) inItemPlan1);
          }
          PXSelectBase<INItemPlan> pxSelectBase = (PXSelectBase<INItemPlan>) new PXSelectJoin<INItemPlan, InnerJoin<FSSODetSplit, On<FSSODetSplit.planID, Equal<INItemPlan.planID>>>, Where<INItemPlan.demandPlanID, Equal<Required<INItemPlan.demandPlanID>>, And<FSSODetSplit.isAllocated, Equal<True>, And<FSSODetSplit.siteID, Equal<Required<FSSODetSplit.siteID>>>>>>(graph);
          if (!string.IsNullOrEmpty(copy1.LotSerialNbr))
            pxSelectBase.WhereAnd<Where<INItemPlan.lotSerialNbr, Equal<Required<INItemPlan.lotSerialNbr>>>>();
          PXResult<INItemPlan> pxResult2 = PXResultset<INItemPlan>.op_Implicit(pxSelectBase.Select(new object[3]
          {
            (object) copy1.DemandPlanID,
            (object) copy1.SiteID,
            (object) copy1.LotSerialNbr
          }));
          if (pxResult2 != null)
          {
            FSSODetSplit fssoDetSplit5 = PXResult.Unwrap<FSSODetSplit>((object) pxResult2);
            ((PXSelectBase) pxSelect).Cache.SetStatus((object) fssoDetSplit5, (PXEntryStatus) 0);
            FSSODetSplit copy3 = PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit5);
            FSSODetSplit fssoDetSplit6 = copy3;
            nullable1 = fssoDetSplit6.BaseQty;
            nullable2 = copy1.PlanQty;
            fssoDetSplit6.BaseQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
            FSSODetSplit fssoDetSplit7 = copy3;
            PXCache cache2 = ((PXSelectBase) pxSelect).Cache;
            int? inventoryId2 = copy3.InventoryID;
            string uom2 = copy3.UOM;
            nullable2 = copy3.BaseQty;
            Decimal num2 = nullable2.Value;
            Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertFromBase(cache2, inventoryId2, uom2, num2, INPrecision.QUANTITY));
            fssoDetSplit7.Qty = nullable5;
            copy3.POReceiptType = POReceiptType;
            copy3.POReceiptNbr = POReceiptNbr;
            FSSODetSplit split = (FSSODetSplit) ((PXSelectBase) pxSelect).Cache.Update((object) copy3);
            INItemPlan copy4 = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan, INPlanType>.op_Implicit(pxResult1));
            INItemPlan inItemPlan3 = copy4;
            nullable2 = inItemPlan3.PlanQty;
            nullable1 = copy1.PlanQty;
            inItemPlan3.PlanQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
            ((PXSelectBase) initemplan).Cache.Update((object) copy4);
            inPlanType = PXCache<INPlanType>.CreateCopy(inPlanType);
            inPlanType.ReplanOnEvent = (string) null;
            inPlanType.DeleteOnEvent = new bool?(true);
            srvOrdLine = (FSSODet) PXParentAttribute.SelectParent(((PXSelectBase) pxSelect).Cache, (object) split, typeof (FSSODet));
            if (srvOrdLine == null)
              throw new PXException("The {0} record was not found.", new object[1]
              {
                (object) DACHelper.GetDisplayName(typeof (FSSODet))
              });
            FSPOReceiptProcess.SrvOrdLineWithSplits ordLineWithSplits = ordLineWithSplitsList.Find((Predicate<FSPOReceiptProcess.SrvOrdLineWithSplits>) (e =>
            {
              int? soDetId1 = e.SrvOrdLine.SODetID;
              int? soDetId2 = srvOrdLine.SODetID;
              return soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue;
            }));
            if (ordLineWithSplits == null)
            {
              ordLineWithSplitsList.Add(new FSPOReceiptProcess.SrvOrdLineWithSplits(srvOrdLine, split, copy1.PlanQty));
              goto label_55;
            }
            ordLineWithSplits.AddUpdateSplit(split, copy1.PlanQty);
            goto label_55;
          }
          ((PXSelectBase<FSServiceOrder>) fsServiceOrder).Current = (FSServiceOrder) PXParentAttribute.SelectParent(((PXSelectBase) pxSelect).Cache, (object) fssoDetSplit4, typeof (FSServiceOrder));
          FSSODetSplit copy5 = PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit4);
          long? planId = copy5.PlanID;
          FSPOReceiptProcess.ClearScheduleReferences(ref copy5);
          if (stockItemProcessing)
          {
            copy5.IsAllocated = new bool?(inPlanType.ReplanOnEvent != "60");
          }
          else
          {
            inPlanType.ReplanOnEvent = "F1";
            copy5.IsAllocated = new bool?(false);
          }
          copy5.LotSerialNbr = copy1.LotSerialNbr;
          copy5.POCreate = new bool?(false);
          copy5.POSource = (string) null;
          copy5.POReceiptType = POReceiptType;
          copy5.POReceiptNbr = POReceiptNbr;
          copy5.SiteID = copy1.SiteID;
          if (copy1.LocationID.HasValue)
          {
            int? locationId1 = copy1.LocationID;
            int? locationId2 = copy5.LocationID;
            if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
              copy5.LocationID = copy1.LocationID;
          }
          copy5.VendorID = new int?();
          copy5.BaseReceivedQty = new Decimal?(0M);
          copy5.ReceivedQty = new Decimal?(0M);
          copy5.BaseQty = copy1.PlanQty;
          FSSODetSplit fssoDetSplit8 = copy5;
          PXCache cache3 = ((PXSelectBase) pxSelect).Cache;
          int? inventoryId3 = copy5.InventoryID;
          string uom3 = copy5.UOM;
          nullable1 = copy5.BaseQty;
          Decimal num3 = nullable1.Value;
          Decimal? nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase(cache3, inventoryId3, uom3, num3, INPrecision.QUANTITY));
          fssoDetSplit8.Qty = nullable6;
          foreach (PXResult<INItemPlan> pxResult3 in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select(graph, new object[1]
          {
            (object) planId
          }))
          {
            INItemPlan copy6 = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan>.op_Implicit(pxResult3));
            ((PXSelectBase) initemplan).Cache.SetStatus((object) copy6, (PXEntryStatus) 0);
            copy6.SupplyPlanID = copy1.PlanID;
            ((PXSelectBase) initemplan).Cache.Update((object) copy6);
          }
          copy5.PlanID = copy1.PlanID;
          FSSODetSplit split1 = (FSSODetSplit) ((PXSelectBase) pxSelect).Cache.Insert((object) copy5);
          fssoDetSplitList2.Add(split1);
          srvOrdLine = (FSSODet) PXParentAttribute.SelectParent(((PXSelectBase) pxSelect).Cache, (object) split1, typeof (FSSODet));
          if (srvOrdLine == null)
            throw new PXException("The {0} record was not found.", new object[1]
            {
              (object) DACHelper.GetDisplayName(typeof (FSSODet))
            });
          FSPOReceiptProcess.SrvOrdLineWithSplits ordLineWithSplits1 = ordLineWithSplitsList.Find((Predicate<FSPOReceiptProcess.SrvOrdLineWithSplits>) (e =>
          {
            int? soDetId3 = e.SrvOrdLine.SODetID;
            int? soDetId4 = srvOrdLine.SODetID;
            return soDetId3.GetValueOrDefault() == soDetId4.GetValueOrDefault() & soDetId3.HasValue == soDetId4.HasValue;
          }));
          if (ordLineWithSplits1 == null)
            ordLineWithSplitsList.Add(new FSPOReceiptProcess.SrvOrdLineWithSplits(srvOrdLine, split1, split1.BaseQty));
          else
            ordLineWithSplits1.AddUpdateSplit(split1, split1.BaseQty);
          FSPOReceiptProcess.UpdateServiceOrderLineSiteAndLocation(soDetView, srvOrdLine, copy1);
          goto label_55;
        }
      }
      if (!copy1.DemandPlanID.HasValue)
      {
        FSSODetSplit fssoDetSplit9 = PXResultset<FSSODetSplit>.op_Implicit(PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.planID, Equal<Required<FSSODetSplit.planID>>, And<FSSODetSplit.completed, Equal<False>>>>.Config>.Select(graph, new object[1]
        {
          (object) copy1.PlanID
        }));
        if (fssoDetSplit9 != null)
        {
          flag1 = false;
          ((PXSelectBase) pxSelect).Cache.SetStatus((object) fssoDetSplit9, (PXEntryStatus) 0);
          FSSODetSplit copy7 = PXCache<FSSODetSplit>.CreateCopy(fssoDetSplit9);
          copy7.Completed = new bool?(true);
          copy7.POCompleted = new bool?(true);
          fssoDetSplitList1.Add(copy7);
          FSSODetSplit split = (FSSODetSplit) ((PXSelectBase) pxSelect).Cache.Update((object) copy7);
          srvOrdLine = (FSSODet) PXParentAttribute.SelectParent(((PXSelectBase) pxSelect).Cache, (object) split, typeof (FSSODet));
          if (srvOrdLine == null)
            throw new PXException("The {0} record was not found.", new object[1]
            {
              (object) DACHelper.GetDisplayName(typeof (FSSODet))
            });
          FSPOReceiptProcess.SrvOrdLineWithSplits ordLineWithSplits = ordLineWithSplitsList.Find((Predicate<FSPOReceiptProcess.SrvOrdLineWithSplits>) (e =>
          {
            int? soDetId5 = e.SrvOrdLine.SODetID;
            int? soDetId6 = srvOrdLine.SODetID;
            return soDetId5.GetValueOrDefault() == soDetId6.GetValueOrDefault() & soDetId5.HasValue == soDetId6.HasValue;
          }));
          if (ordLineWithSplits == null)
            ordLineWithSplitsList.Add(new FSPOReceiptProcess.SrvOrdLineWithSplits(srvOrdLine, split, new Decimal?(0M)));
          else
            ordLineWithSplits.AddUpdateSplit(split, new Decimal?(0M));
          INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select(graph, new object[1]
          {
            (object) copy1.PlanID
          }));
          source.Add(inItemPlan);
          ((PXSelectBase) initemplan).Cache.Delete((object) inItemPlan);
        }
      }
label_55:
      if (flag1)
        pxResultList.Add(pxResult1);
      else if (inPlanType.ReplanOnEvent != null)
      {
        copy1.PlanType = inPlanType.ReplanOnEvent;
        copy1.SupplyPlanID = new long?();
        copy1.DemandPlanID = new long?();
        ((PXSelectBase) initemplan).Cache.Update((object) copy1);
      }
      else if (inPlanType.DeleteOnEvent.GetValueOrDefault())
        ((PXSelectBase<INItemPlan>) initemplan).Delete(copy1);
    }
    FSSODetSplit fssoDetSplit10 = (FSSODetSplit) null;
    foreach (FSSODetSplit fssoDetSplit11 in fssoDetSplitList2)
    {
      if (fssoDetSplit10 != null && fssoDetSplit10.SrvOrdType == fssoDetSplit11.SrvOrdType && fssoDetSplit10.RefNbr == fssoDetSplit11.RefNbr)
      {
        int? nullable7 = fssoDetSplit10.LineNbr;
        int? nullable8 = fssoDetSplit11.LineNbr;
        if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue)
        {
          nullable8 = fssoDetSplit10.InventoryID;
          nullable7 = fssoDetSplit11.InventoryID;
          if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue)
          {
            nullable7 = fssoDetSplit10.SubItemID;
            nullable8 = fssoDetSplit11.SubItemID;
            if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue)
            {
              nullable8 = fssoDetSplit10.ParentSplitLineNbr;
              nullable7 = fssoDetSplit11.ParentSplitLineNbr;
              if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue && fssoDetSplit10.LotSerialNbr != null && fssoDetSplit11.LotSerialNbr != null)
                continue;
            }
          }
        }
      }
      FSSODetSplit parentschedule = PXResultset<FSSODetSplit>.op_Implicit(PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>, And<FSSODetSplit.splitLineNbr, Equal<Required<FSSODetSplit.parentSplitLineNbr>>>>>>>.Config>.Select(graph, new object[4]
      {
        (object) fssoDetSplit11.SrvOrdType,
        (object) fssoDetSplit11.RefNbr,
        (object) fssoDetSplit11.LineNbr,
        (object) fssoDetSplit11.ParentSplitLineNbr
      }));
      if (parentschedule != null)
      {
        bool? nullable = parentschedule.Completed;
        if (nullable.GetValueOrDefault())
        {
          nullable = parentschedule.POCompleted;
          if (nullable.GetValueOrDefault())
          {
            Decimal? baseQty = parentschedule.BaseQty;
            Decimal? baseReceivedQty = parentschedule.BaseReceivedQty;
            if (baseQty.GetValueOrDefault() > baseReceivedQty.GetValueOrDefault() & baseQty.HasValue & baseReceivedQty.HasValue && source.Exists((Predicate<INItemPlan>) (x =>
            {
              long? planId3 = x.PlanID;
              long? planId4 = parentschedule.PlanID;
              return planId3.GetValueOrDefault() == planId4.GetValueOrDefault() & planId3.HasValue == planId4.HasValue;
            })))
            {
              ((PXSelectBase<FSServiceOrder>) fsServiceOrder).Current = (FSServiceOrder) PXParentAttribute.SelectParent(((PXSelectBase) pxSelect).Cache, (object) parentschedule, typeof (FSServiceOrder));
              parentschedule = PXCache<FSSODetSplit>.CreateCopy(parentschedule);
              INItemPlan copy = PXCache<INItemPlan>.CreateCopy(source.First<INItemPlan>((Func<INItemPlan, bool>) (x =>
              {
                long? planId1 = x.PlanID;
                long? planId2 = parentschedule.PlanID;
                return planId1.GetValueOrDefault() == planId2.GetValueOrDefault() & planId1.HasValue == planId2.HasValue;
              })));
              this.UpdateSchedulesFromCompletedPO(graph, pxSelect, initemplan, parentschedule, fsServiceOrder, copy);
            }
          }
        }
      }
      fssoDetSplit10 = fssoDetSplit11;
    }
    foreach (FSPOReceiptProcess.SrvOrdLineWithSplits srvOrdLineExt in ordLineWithSplitsList)
      this.UpdatePOReceiptInfoInAppointments(graph, srvOrdLineExt, pxSelect, appointmentView, apptLineView, apptLineSplitView, false);
    foreach (FSSODetSplit fssoDetSplit12 in fssoDetSplitList1)
    {
      FSSODetSplit fssoDetSplit13 = (FSSODetSplit) ((PXSelectBase) pxSelect).Cache.Locate((object) fssoDetSplit12);
      if (fssoDetSplit13 != null)
      {
        fssoDetSplit13.PlanID = new long?();
        ((PXSelectBase) pxSelect).Cache.Update((object) fssoDetSplit13);
      }
    }
    return pxResultList;
  }

  private static void UpdateServiceOrderLineSiteAndLocation(
    PXSelect<FSSODet> soDetView,
    FSSODet srvOrdLine,
    INItemPlan plan)
  {
    int? siteId1 = srvOrdLine.SiteID;
    int? siteId2 = plan.SiteID;
    if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
    {
      int? locationId1 = srvOrdLine.LocationID;
      int? locationId2 = plan.LocationID;
      if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue)
        return;
    }
    FSSODet copy = (FSSODet) ((PXSelectBase) soDetView).Cache.CreateCopy((object) srvOrdLine);
    copy.SiteID = plan.SiteID;
    copy.LocationID = plan.LocationID;
    ((PXSelectBase<FSSODet>) soDetView).Update(copy);
  }

  public static void UpdatePOReceiptInfoInAppointmentsStatic(
    PXGraph graph,
    FSPOReceiptProcess.SrvOrdLineWithSplits srvOrdLineExt,
    PXSelect<FSSODetSplit> soDetSplitView,
    PXSelect<FSAppointment> appointmentView,
    PXSelect<FSAppointmentDet> apptLineView,
    PXSelect<FSApptLineSplit> apptLineSplitView,
    bool ignoreCurrentAppointmentAllocation)
  {
    FSPOReceiptProcess.SingleFSProcessPOReceipt.UpdatePOReceiptInfoInAppointments(graph, srvOrdLineExt, soDetSplitView, appointmentView, apptLineView, apptLineSplitView, ignoreCurrentAppointmentAllocation);
  }

  public virtual void UpdatePOReceiptInfoInAppointments(
    PXGraph graph,
    FSPOReceiptProcess.SrvOrdLineWithSplits srvOrdLineExt,
    PXSelect<FSSODetSplit> soDetSplitView,
    PXSelect<FSAppointment> appointmentView,
    PXSelect<FSAppointmentDet> apptLineView,
    PXSelect<FSApptLineSplit> apptLineSplitView,
    bool ignoreCurrentAppointmentAllocation)
  {
    PXCache cach1 = graph.Caches[typeof (FSServiceOrder)];
    PXCache cach2 = graph.Caches[typeof (FSSODet)];
    if (FSSrvOrdType.PK.Find(graph, srvOrdLineExt.SrvOrdLine.SrvOrdType) == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSrvOrdType))
      });
    bool flag1 = true;
    if (PX.Objects.IN.InventoryItem.PK.Find(graph, srvOrdLineExt.SrvOrdLine.InventoryID) == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (PX.Objects.IN.InventoryItem))
      });
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    List<FSSODetSplit> fssoDetSplitList = new List<FSSODetSplit>();
    Decimal? nullable1;
    foreach (FSSODetSplit fssoDetSplit in (IEnumerable<FSSODetSplit>) GraphHelper.RowCast<FSSODetSplit>((IEnumerable) PXParentAttribute.SelectChildren(((PXSelectBase) soDetSplitView).Cache, (object) srvOrdLineExt.SrvOrdLine, typeof (FSSODet))).OrderBy<FSSODetSplit, int?>((Func<FSSODetSplit, int?>) (e => e.SplitLineNbr)))
    {
      if (fssoDetSplit.POCreate.GetValueOrDefault())
      {
        Decimal num3 = num1;
        nullable1 = fssoDetSplit.BaseQty;
        Decimal num4 = nullable1.Value;
        num1 = num3 + num4;
        Decimal num5 = num2;
        nullable1 = fssoDetSplit.BaseReceivedQty;
        Decimal num6 = nullable1.Value;
        nullable1 = fssoDetSplit.BaseShippedQty;
        Decimal num7 = nullable1.Value;
        Decimal num8 = num6 + num7;
        num2 = num5 + num8;
      }
      else if (ignoreCurrentAppointmentAllocation)
      {
        nullable1 = fssoDetSplit.BaseQty;
        Decimal num9 = 0M;
        if (nullable1.GetValueOrDefault() > num9 & nullable1.HasValue)
          fssoDetSplitList.Add((FSSODetSplit) ((PXSelectBase) soDetSplitView).Cache.CreateCopy((object) fssoDetSplit));
      }
    }
    Decimal baseQtyBalanceToAllocate;
    if (!ignoreCurrentAppointmentAllocation)
    {
      foreach (FSPOReceiptProcess.SplitWithAdjustQty splitWithAdjustQty in (IEnumerable<FSPOReceiptProcess.SplitWithAdjustQty>) srvOrdLineExt.Splits.OrderBy<FSPOReceiptProcess.SplitWithAdjustQty, int?>((Func<FSPOReceiptProcess.SplitWithAdjustQty, int?>) (e => e.Split.SplitLineNbr)))
      {
        bool? poCreate = splitWithAdjustQty.Split.POCreate;
        bool flag2 = false;
        if (poCreate.GetValueOrDefault() == flag2 & poCreate.HasValue)
          fssoDetSplitList.Add((FSSODetSplit) ((PXSelectBase) soDetSplitView).Cache.CreateCopy((object) splitWithAdjustQty.Split));
      }
      baseQtyBalanceToAllocate = srvOrdLineExt.BaseNewReceivedShippedQty;
    }
    else
      baseQtyBalanceToAllocate = num2;
    foreach (FSAppointmentDet fsAppointmentDet1 in (IEnumerable<FSAppointmentDet>) this.GetRelatedApptLines(graph, srvOrdLineExt.SrvOrdLine.SODetID, false, new int?(), true, true).OrderBy<FSAppointmentDet, DateTime?>((Func<FSAppointmentDet, DateTime?>) (x => x.TranDate)).ThenBy<FSAppointmentDet, int?>((Func<FSAppointmentDet, int?>) (x => x.AppointmentID)).ThenBy<FSAppointmentDet, int?>((Func<FSAppointmentDet, int?>) (x => x.AppDetID)))
    {
      ((PXSelectBase<FSAppointmentDet>) apptLineView).Current = fsAppointmentDet1;
      ((PXSelectBase<FSAppointment>) appointmentView).Current = (FSAppointment) PXParentAttribute.SelectParent(((PXSelectBase) apptLineView).Cache, (object) fsAppointmentDet1, typeof (FSAppointment));
      if (((PXSelectBase<FSAppointment>) appointmentView).Current == null)
        throw new PXException("The {0} record was not found.", new object[1]
        {
          (object) DACHelper.GetDisplayName(typeof (FSAppointment))
        });
      if (!((PXSelectBase<FSAppointment>) appointmentView).Current.Canceled.GetValueOrDefault())
      {
        FSAppointmentDet copy1 = (FSAppointmentDet) ((PXSelectBase) apptLineView).Cache.CreateCopy((object) ((PXSelectBase<FSAppointmentDet>) apptLineView).Current);
        List<FSApptLineSplit> list1 = GraphHelper.RowCast<FSApptLineSplit>((IEnumerable) PXParentAttribute.SelectChildren(((PXSelectBase) apptLineSplitView).Cache, (object) ((PXSelectBase<FSAppointmentDet>) apptLineView).Current, typeof (FSAppointmentDet))).ToList<FSApptLineSplit>();
        int num10 = list1.Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (x => !string.IsNullOrEmpty(x.LotSerialNbr))).Count<FSApptLineSplit>();
        Decimal? nullable2;
        if (!ignoreCurrentAppointmentAllocation)
        {
          if (((PXSelectBase<FSAppointment>) appointmentView).Current.Status == "Z")
            continue;
        }
        else
        {
          switch ("O")
          {
            case "O":
              List<FSApptLineSplit> list2 = GraphHelper.RowCast<FSApptLineSplit>((IEnumerable) PXParentAttribute.SelectChildren(((PXSelectBase) apptLineSplitView).Cache, (object) srvOrdLineExt.SrvOrdLine, typeof (FSAppointmentDet))).OrderBy<FSApptLineSplit, int?>((Func<FSApptLineSplit, int?>) (e => e.SplitLineNbr)).ToList<FSApptLineSplit>();
              this.UpdateAvailableQtyOfUsedSplits(fssoDetSplitList, list2, ref baseQtyBalanceToAllocate);
              break;
            case "D":
              Decimal num11 = baseQtyBalanceToAllocate;
              nullable2 = copy1.BaseAllocatedFromSrvOrdPOQty;
              Decimal num12 = nullable2.Value;
              baseQtyBalanceToAllocate = num11 - num12;
              break;
            default:
              throw new NotImplementedException();
          }
          if (!EnumerableExtensions.IsIn<string>(((PXSelectBase<FSAppointment>) appointmentView).Current.Status, "C", "Z"))
          {
            copy1.BaseAllocatedFromSrvOrdPOQty = new Decimal?(0M);
            foreach (FSApptLineSplit fsApptLineSplit in list1)
            {
              FSAppointmentDet fsAppointmentDet2 = copy1;
              nullable2 = fsAppointmentDet2.BaseAllocatedFromSrvOrdPOQty;
              Decimal? baseQty = fsApptLineSplit.BaseQty;
              fsAppointmentDet2.BaseAllocatedFromSrvOrdPOQty = nullable2.HasValue & baseQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
            }
            copy1.AllocatedFromSrvOrdPOQty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) apptLineView).Cache, copy1.InventoryID, copy1.UOM, copy1.BaseAllocatedFromSrvOrdPOQty.Value, INPrecision.QUANTITY));
            Decimal? baseEffTranQty = copy1.BaseEffTranQty;
            nullable2 = copy1.BaseAllocatedFromSrvOrdPOQty;
            if (baseEffTranQty.GetValueOrDefault() < nullable2.GetValueOrDefault() & baseEffTranQty.HasValue & nullable2.HasValue)
              copy1.Status = "WP";
            FSAppointmentDet fsAppointmentDet3 = ((PXSelectBase<FSAppointmentDet>) apptLineView).Update(copy1);
            copy1 = (FSAppointmentDet) ((PXSelectBase) apptLineView).Cache.CreateCopy((object) fsAppointmentDet3);
          }
          else
            continue;
        }
        nullable2 = copy1.BaseEffTranQty;
        Decimal num13 = nullable2.Value;
        nullable2 = copy1.BaseAllocatedFromSrvOrdPOQty;
        Decimal num14 = nullable2.Value;
        Decimal num15 = num13 - num14;
        Decimal num16;
        if (baseQtyBalanceToAllocate > num15)
        {
          num16 = num15;
          baseQtyBalanceToAllocate -= num16;
        }
        else
        {
          num16 = baseQtyBalanceToAllocate;
          baseQtyBalanceToAllocate = 0M;
        }
        string str = (string) null;
        Decimal num17 = num16;
        if (flag1 && num16 > 0M)
        {
          foreach (FSSODetSplit soDetSplit in fssoDetSplitList.Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (e =>
          {
            Decimal? baseQty = e.BaseQty;
            Decimal num23 = 0M;
            return baseQty.GetValueOrDefault() > num23 & baseQty.HasValue;
          })))
          {
            FSApptLineSplit fsApptLineSplit1 = FSAppointmentLineSplittingExtension.StaticConvert(copy1);
            fsApptLineSplit1.BaseQty = new Decimal?(0M);
            FSApptLineSplit fsApptLineSplit2 = (FSApptLineSplit) ((PXSelectBase) apptLineSplitView).Cache.Insert((object) fsApptLineSplit1);
            PXDefaultAttribute.SetPersistingCheck<FSApptLineSplit.locationID>(((PXSelectBase) apptLineSplitView).Cache, (object) fsApptLineSplit2, copy1.LineType == "NSTKI" ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
            FSApptLineSplit copy2 = (FSApptLineSplit) ((PXSelectBase) apptLineSplitView).Cache.CreateCopy((object) fsApptLineSplit2);
            this.FillLotSerialAndPOFields(copy2, soDetSplit);
            FSPOReceiptProcess.FillAppointmentLineSiteAndLocation(copy1, soDetSplit);
            if (!string.IsNullOrEmpty(copy2.LotSerialNbr))
            {
              ++num10;
              str = copy2.LotSerialNbr;
            }
            PX.Objects.PO.POLine poLine = PX.Objects.PO.POLine.PK.Find(graph, copy1.POType, copy1.PONbr, copy1.POLineNbr);
            PX.Objects.PO.POReceiptLine topFirst = PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[3]
            {
              (object) copy2.POReceiptNbr,
              (object) copy2.POReceiptType,
              (object) poLine.LineNbr
            }).TopFirst;
            copy2.ExpireDate = topFirst.ExpireDate;
            FSApptLineSplit fsApptLineSplit3 = copy2;
            Decimal num18 = num16;
            nullable2 = soDetSplit.BaseQty;
            Decimal valueOrDefault = nullable2.GetValueOrDefault();
            Decimal? nullable3 = num18 < valueOrDefault & nullable2.HasValue ? new Decimal?(num16) : soDetSplit.BaseQty;
            fsApptLineSplit3.BaseQty = nullable3;
            FSApptLineSplit fsApptLineSplit4 = copy2;
            PXCache cache = ((PXSelectBase) apptLineSplitView).Cache;
            int? inventoryId = copy2.InventoryID;
            string uom = copy2.UOM;
            nullable2 = copy2.BaseQty;
            Decimal num19 = nullable2.Value;
            Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num19, INPrecision.QUANTITY));
            fsApptLineSplit4.Qty = nullable4;
            FSApptLineSplit fsApptLineSplit5 = (FSApptLineSplit) ((PXSelectBase) apptLineSplitView).Cache.Update((object) copy2);
            FSSODetSplit fssoDetSplit = soDetSplit;
            nullable2 = fssoDetSplit.BaseQty;
            Decimal num20 = fsApptLineSplit5.BaseQty.Value;
            fssoDetSplit.BaseQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num20) : new Decimal?();
            Decimal num21 = num16;
            nullable2 = fsApptLineSplit5.BaseQty;
            Decimal num22 = nullable2.Value;
            num16 = num21 - num22;
            if (num16 <= 0M)
              break;
          }
        }
        FSAppointmentDet fsAppointmentDet4 = copy1;
        nullable2 = fsAppointmentDet4.BaseAllocatedFromSrvOrdPOQty;
        Decimal num24 = num17;
        fsAppointmentDet4.BaseAllocatedFromSrvOrdPOQty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num24) : new Decimal?();
        FSAppointmentDet fsAppointmentDet5 = copy1;
        PXCache cache1 = ((PXSelectBase) apptLineView).Cache;
        int? inventoryId1 = copy1.InventoryID;
        string uom1 = copy1.UOM;
        nullable2 = copy1.BaseAllocatedFromSrvOrdPOQty;
        Decimal num25 = nullable2.Value;
        Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertFromBase(cache1, inventoryId1, uom1, num25, INPrecision.QUANTITY));
        fsAppointmentDet5.AllocatedFromSrvOrdPOQty = nullable5;
        string status = copy1.Status;
        this.UpdateLineStatusBasedOnReceivedPurchaseItems(((PXSelectBase<FSAppointment>) appointmentView).Current, ((PXSelectBase) apptLineView).Cache, copy1, this.MustHaveRequestPOStatus(copy1), (List<FSApptLineSplit>) null, copy1.BaseAllocatedFromSrvOrdPOQty, copy1.AllocatedFromSrvOrdPOQty, false);
        copy1.LotSerialNbr = num10 != 1 ? (string) null : str;
        FSAppointmentDet apptDet = (FSAppointmentDet) ((PXSelectBase) apptLineView).Cache.Update((object) copy1);
        if (!(graph is POOrderEntry))
          ((PXSelectBase) apptLineView).Cache.Persist((object) apptDet, (PXDBOperation) 1);
        FSServiceOrder serviceOrder = (FSServiceOrder) null;
        FSSODet soDet = (FSSODet) null;
        if (FSPOReceiptProcess.UpdateFSSODetStatus(apptDet, status, cach1, cach2, out serviceOrder, out soDet) && !(graph is POOrderEntry))
          cach2.Persist((object) soDet, (PXDBOperation) 1);
      }
    }
  }

  private static void FillAppointmentLineSiteAndLocation(
    FSAppointmentDet apptLineCopy,
    FSSODetSplit soDetSplit)
  {
    apptLineCopy.SiteID = soDetSplit.SiteID;
    apptLineCopy.LocationID = soDetSplit.LocationID;
  }

  public static bool UpdateFSSODetStatus(
    FSAppointmentDet apptDet,
    string oldApptDetStatus,
    PXCache serviceOrderCache,
    PXCache soDetCache,
    out FSServiceOrder serviceOrder,
    out FSSODet soDet)
  {
    soDet = (FSSODet) null;
    serviceOrder = (FSServiceOrder) null;
    if (!(apptDet.Status != oldApptDetStatus) || !(apptDet.Status == "CP") || !apptDet.EnablePO.GetValueOrDefault() || !(apptDet.POSource == "A"))
      return false;
    soDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.srvOrdType, Equal<Required<FSSODet.srvOrdType>>, And<FSSODet.refNbr, Equal<Required<FSSODet.refNbr>>, And<FSSODet.lineNbr, Equal<Required<FSSODet.lineNbr>>>>>>.Config>.Select(soDetCache.Graph, new object[3]
    {
      (object) apptDet.SrvOrdType,
      (object) apptDet.OrigSrvOrdNbr,
      (object) apptDet.OrigLineNbr
    }));
    if (soDet == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSODet))
      });
    serviceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select(serviceOrderCache.Graph, new object[2]
    {
      (object) soDet.SrvOrdType,
      (object) soDet.RefNbr
    }));
    if (serviceOrder == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSServiceOrder))
      });
    serviceOrderCache.Current = (object) serviceOrder;
    soDet = (FSSODet) soDetCache.CreateCopy((object) soDet);
    soDet.Status = "CP";
    soDet = (FSSODet) soDetCache.Update((object) soDet);
    return true;
  }

  protected virtual void UpdateAvailableQtyOfUsedSplits(
    List<FSSODetSplit> srvOrdLineSplitCopyList,
    List<FSApptLineSplit> usedApptSplits,
    ref Decimal baseQtyBalanceToAllocate)
  {
    foreach (FSApptLineSplit usedApptSplit1 in usedApptSplits)
    {
      FSApptLineSplit usedApptSplit = usedApptSplit1;
      int index = srvOrdLineSplitCopyList.FindIndex((Predicate<FSSODetSplit>) (e =>
      {
        if (usedApptSplit.POReceiptType != null && usedApptSplit.POReceiptNbr != null && e.POReceiptType == usedApptSplit.POReceiptType && e.POReceiptNbr == usedApptSplit.POReceiptNbr && string.Equals(e.LotSerialNbr, usedApptSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
          return true;
        return usedApptSplit.POReceiptType == null && usedApptSplit.POReceiptNbr == null && !string.IsNullOrEmpty(e.LotSerialNbr) && string.Equals(e.LotSerialNbr, usedApptSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
      }));
      if (index > -1)
      {
        FSSODetSplit ordLineSplitCopy = srvOrdLineSplitCopyList[index];
        Decimal? baseQty1 = ordLineSplitCopy.BaseQty;
        Decimal? baseQty2 = usedApptSplit.BaseQty;
        if (baseQty1.GetValueOrDefault() > baseQty2.GetValueOrDefault() & baseQty1.HasValue & baseQty2.HasValue)
        {
          ref Decimal local = ref baseQtyBalanceToAllocate;
          Decimal num1 = baseQtyBalanceToAllocate;
          baseQty2 = ordLineSplitCopy.BaseQty;
          Decimal num2 = baseQty2.Value;
          baseQty2 = usedApptSplit.BaseQty;
          Decimal num3 = baseQty2.Value;
          Decimal num4 = num2 - num3;
          Decimal num5 = num1 - num4;
          local = num5;
          FSSODetSplit fssoDetSplit = ordLineSplitCopy;
          baseQty2 = fssoDetSplit.BaseQty;
          baseQty1 = usedApptSplit.BaseQty;
          fssoDetSplit.BaseQty = baseQty2.HasValue & baseQty1.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() - baseQty1.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          ref Decimal local = ref baseQtyBalanceToAllocate;
          Decimal num6 = baseQtyBalanceToAllocate;
          baseQty1 = ordLineSplitCopy.BaseQty;
          Decimal num7 = baseQty1.Value;
          Decimal num8 = num6 - num7;
          local = num8;
          ordLineSplitCopy.BaseQty = new Decimal?(0M);
        }
        baseQty1 = ordLineSplitCopy.BaseQty;
        Decimal num = 0M;
        if (baseQty1.GetValueOrDefault() <= num & baseQty1.HasValue)
          srvOrdLineSplitCopyList.RemoveAt(index);
      }
    }
  }

  public virtual void FillLotSerialAndPOFields(FSApptLineSplit split, FSSODetSplit soDetSplit)
  {
    FSPOReceiptProcess.FillLotSerialAndPOFieldsStatic(split, soDetSplit);
    split.POSource = (string) null;
    split.SiteID = soDetSplit.SiteID;
    split.LocationID = soDetSplit.LocationID;
    split.VendorID = new int?();
  }

  public static void FillLotSerialAndPOFieldsStatic(FSApptLineSplit split, FSSODetSplit soDetSplit)
  {
    split.LotSerialNbr = soDetSplit.LotSerialNbr;
    split.POCreate = new bool?(false);
    split.POReceiptType = soDetSplit.POReceiptType;
    split.POReceiptNbr = soDetSplit.POReceiptNbr;
    split.OrigSrvOrdType = soDetSplit.SrvOrdType;
    split.OrigSrvOrdNbr = soDetSplit.RefNbr;
    split.OrigLineNbr = soDetSplit.LineNbr;
    split.OrigSplitLineNbr = soDetSplit.SplitLineNbr;
  }

  public static void UpdateSchedulesFromCompletedPOStatic(
    PXGraph graph,
    PXSelect<FSSODetSplit> fsSODetSplit,
    PXSelect<INItemPlan> initemplan,
    FSSODetSplit parentschedule,
    PXSelect<FSServiceOrder> fsServiceOrder,
    INItemPlan demand)
  {
    FSPOReceiptProcess.SingleFSProcessPOReceipt.UpdateSchedulesFromCompletedPO(graph, fsSODetSplit, initemplan, parentschedule, fsServiceOrder, demand);
  }

  public virtual void UpdateSchedulesFromCompletedPO(
    PXGraph graph,
    PXSelect<FSSODetSplit> fsSODetSplit,
    PXSelect<INItemPlan> initemplan,
    FSSODetSplit parentschedule,
    PXSelect<FSServiceOrder> fsServiceOrder,
    INItemPlan demand)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    graph.FieldDefaulting.AddHandler<FSSODetSplit.locationID>(FSPOReceiptProcess.\u003C\u003Ec.\u003C\u003E9__13_0 ?? (FSPOReceiptProcess.\u003C\u003Ec.\u003C\u003E9__13_0 = new PXFieldDefaulting((object) FSPOReceiptProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateSchedulesFromCompletedPO\u003Eb__13_0))));
    FSSODetSplit copy1 = PXCache<FSSODetSplit>.CreateCopy(parentschedule);
    FSPOReceiptProcess.ClearScheduleReferences(ref copy1);
    copy1.LotSerialNbr = demand.LotSerialNbr;
    copy1.SiteID = demand.SiteID;
    FSSODetSplit fssoDetSplit = copy1;
    Decimal? baseQty = parentschedule.BaseQty;
    Decimal? baseReceivedQty = parentschedule.BaseReceivedQty;
    Decimal? nullable = baseQty.HasValue & baseReceivedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - baseReceivedQty.GetValueOrDefault()) : new Decimal?();
    fssoDetSplit.BaseQty = nullable;
    copy1.Qty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) fsSODetSplit).Cache, copy1.InventoryID, copy1.UOM, copy1.BaseQty.Value, INPrecision.QUANTITY));
    copy1.BaseReceivedQty = new Decimal?(0M);
    copy1.ReceivedQty = new Decimal?(0M);
    INItemPlan copy2 = PXCache<INItemPlan>.CreateCopy(demand);
    copy2.PlanID = new long?();
    copy2.SupplyPlanID = new long?();
    copy2.DemandPlanID = new long?();
    copy2.PlanQty = copy1.BaseQty;
    copy2.VendorID = new int?();
    copy2.VendorLocationID = new int?();
    copy2.FixedSource = "N";
    copy2.PlanType = ((PXSelectBase<FSServiceOrder>) fsServiceOrder).Current == null || !((PXSelectBase<FSServiceOrder>) fsServiceOrder).Current.Hold.GetValueOrDefault() ? "F1" : "F0";
    INItemPlan inItemPlan = (INItemPlan) ((PXSelectBase) initemplan).Cache.Insert((object) copy2);
    copy1.PlanID = inItemPlan.PlanID;
    ((PXSelectBase) fsSODetSplit).Cache.Insert((object) copy1);
  }

  public static void ClearScheduleReferences(ref FSSODetSplit schedule)
  {
    schedule.ParentSplitLineNbr = schedule.SplitLineNbr;
    schedule.SplitLineNbr = new int?();
    schedule.Completed = new bool?(false);
    schedule.PlanID = new long?();
    FSPOReceiptProcess.ClearPOFlags(schedule);
    FSPOReceiptProcess.ClearPOReferences(schedule);
    schedule.POSource = "N";
    FSPOReceiptProcess.ClearSOReferences(schedule);
    schedule.RefNoteID = new Guid?();
  }

  public static void ClearPOFlags(FSSODetSplit split)
  {
    split.POCompleted = new bool?(false);
    split.POCancelled = new bool?(false);
    split.POCreate = new bool?(false);
    split.POSource = (string) null;
  }

  public static void ClearPOReferences(FSSODetSplit split)
  {
    split.POType = (string) null;
    split.PONbr = (string) null;
    split.POLineNbr = new int?();
    split.POReceiptType = (string) null;
    split.POReceiptNbr = (string) null;
  }

  public static void ClearSOReferences(FSSODetSplit split)
  {
    split.SOOrderType = (string) null;
    split.SOOrderNbr = (string) null;
    split.SOLineNbr = new int?();
    split.SOSplitLineNbr = new int?();
  }

  public static void UpdateSrvOrdLinePOStatus(PXGraph graph, PX.Objects.PO.POOrder poOrderRow)
  {
    FSPOReceiptProcess.SingleFSProcessPOReceipt.UpdateSrvOrdLinePOStatusInt(graph, poOrderRow);
  }

  public virtual void UpdateSrvOrdLinePOStatusInt(PXGraph graph, PX.Objects.PO.POOrder poOrderRow)
  {
    PXCache cach1 = graph.Caches[typeof (FSSODet)];
    PXCache cach2 = graph.Caches[typeof (FSAppointmentDet)];
    PXCache cach3 = graph.Caches[typeof (FSAppointment)];
    PXResultset<FSSODet> pxResultset1 = PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.poType, Equal<Required<FSSODet.poType>>, And<FSSODet.poNbr, Equal<Required<FSSODet.poNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) poOrderRow.OrderType,
      (object) poOrderRow.OrderNbr
    });
    PXResultset<PX.Objects.PO.POLine> pxResultset2 = PXSelectBase<PX.Objects.PO.POLine, PXSelect<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) poOrderRow.OrderType,
      (object) poOrderRow.OrderNbr
    });
    foreach (PXResult<FSSODet> pxResult in pxResultset1)
    {
      FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult);
      PX.Objects.PO.POLine poLine1 = PX.Objects.PO.POLine.PK.Find(graph, fssoDet.POType, fssoDet.PONbr, fssoDet.POLineNbr);
      PX.Objects.PO.POLine poLine2 = GraphHelper.Caches<PX.Objects.PO.POLine>(graph).Locate(poLine1);
      FSSODet soLineCopy = (FSSODet) cach1.CreateCopy((object) fssoDet);
      soLineCopy.POStatus = poOrderRow.Status;
      PX.Objects.PO.POLine poLine3 = pxResultset2.Cast<PX.Objects.PO.POLine>().SingleOrDefault<PX.Objects.PO.POLine>((Expression<Func<PX.Objects.PO.POLine, bool>>) (x => x.LineNbr == soLineCopy.LineNbr));
      if (poLine3 != null)
        soLineCopy.POCompleted = poLine3.Completed;
      soLineCopy = (FSSODet) cach1.Update((object) soLineCopy);
      List<FSAppointmentDet> relatedApptLines = this.GetRelatedApptLines(graph, soLineCopy.SODetID, false, new int?(), false, false);
      int? nullable1 = new int?();
      using (new PXTimeStampScope((byte[]) null))
      {
        foreach (FSAppointmentDet fsAppointmentDet1 in (IEnumerable<FSAppointmentDet>) relatedApptLines.OrderBy<FSAppointmentDet, int?>((Func<FSAppointmentDet, int?>) (x => x.AppointmentID)))
        {
          if (nullable1.HasValue)
          {
            int? nullable2 = nullable1;
            int? appointmentId = fsAppointmentDet1.AppointmentID;
            if (nullable2.GetValueOrDefault() == appointmentId.GetValueOrDefault() & nullable2.HasValue == appointmentId.HasValue)
              goto label_10;
          }
          cach3.Current = (object) (FSAppointment) PXParentAttribute.SelectParent(cach2, (object) fsAppointmentDet1, typeof (FSAppointment));
          nullable1 = fsAppointmentDet1.AppointmentID;
label_10:
          FSAppointmentDet copy = (FSAppointmentDet) cach2.CreateCopy((object) fsAppointmentDet1);
          copy.POStatus = poOrderRow.Status;
          if (poLine2 != null)
          {
            bool? completed1 = poLine2.Completed;
            bool? poCompleted = copy.POCompleted;
            if (!(completed1.GetValueOrDefault() == poCompleted.GetValueOrDefault() & completed1.HasValue == poCompleted.HasValue))
            {
              copy.POCompleted = poLine2.Completed;
              FSAppointment current = (FSAppointment) cach3.Current;
              if ((current != null ? (!current.Billed.GetValueOrDefault() ? 1 : 0) : 1) != 0)
              {
                bool? completed2 = poLine2.Completed;
                bool flag = false;
                if (completed2.GetValueOrDefault() == flag & completed2.HasValue && poOrderRow.Status == "H")
                {
                  copy.Status = "WP";
                }
                else
                {
                  completed2 = poLine2.Completed;
                  if (completed2.GetValueOrDefault() && (poOrderRow.Status == "M" || poOrderRow.Status == "L"))
                    copy.Status = "NS";
                }
              }
            }
          }
          FSAppointmentDet fsAppointmentDet2 = (FSAppointmentDet) cach2.Update((object) copy);
          fsAppointmentDet2.tstamp = PXDatabase.SelectTimeStamp();
          PXTimeStampScope.DuplicatePersisted(cach2, (object) fsAppointmentDet2, typeof (FSAppointmentDet));
          PXTimeStampScope.SetRecordComesFirst(typeof (FSAppointmentDet), true);
          cach2.ResetPersisted((object) fsAppointmentDet2);
          cach2.Persist((object) fsAppointmentDet2, (PXDBOperation) 1);
        }
        soLineCopy.tstamp = PXDatabase.SelectTimeStamp();
        PXTimeStampScope.DuplicatePersisted(cach1, (object) soLineCopy, typeof (FSSODet));
        PXTimeStampScope.SetRecordComesFirst(typeof (FSSODet), true);
        cach1.ResetPersisted((object) soLineCopy);
        cach1.Persist((object) soLineCopy, (PXDBOperation) 1);
      }
    }
  }

  public static void UpdateSrvOrdApptLineUnitCost(PXGraph graph, PX.Objects.PO.POOrder poOrderRow)
  {
    FSPOReceiptProcess.SingleFSProcessPOReceipt.UpdateSrvOrdApptLineUnitCostInt(graph, poOrderRow);
  }

  public virtual void UpdateSrvOrdApptLineUnitCostInt(PXGraph graph, PX.Objects.PO.POOrder poOrderRow)
  {
    List<\u003C\u003Ef__AnonymousType3<FSSODet, PX.Objects.PO.POLine, List<FSAppointmentDet>>> list = ((IEnumerable<PXResult<FSSODet>>) PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.PO.POLine>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<FSSODet.poType>>>>, And<BqlOperand<PX.Objects.PO.POLine.orderNbr, IBqlString>.IsEqual<FSSODet.poNbr>>>>.And<BqlOperand<PX.Objects.PO.POLine.lineNbr, IBqlInt>.IsEqual<FSSODet.poLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.poType, Equal<P.AsString>>>>, And<BqlOperand<FSSODet.poNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.PO.POLine.curyUnitCost, IBqlDecimal>.IsNotEqual<FSSODet.curyUnitCost>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.lineType, Equal<FSLineType.NonStockItem>>>>>.Or<BqlOperand<FSSODet.lineType, IBqlString>.IsEqual<FSLineType.Service>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) poOrderRow.OrderType,
      (object) poOrderRow.OrderNbr
    })).AsEnumerable<PXResult<FSSODet>>().Select(res =>
    {
      FSSODet fssoDet = ((PXResult) res).GetItem<FSSODet>();
      PX.Objects.PO.POLine poLine1 = ((PXResult) res).GetItem<PX.Objects.PO.POLine>();
      bool flag = poLine1?.OrderNbr != null;
      if (!flag)
        poLine1 = new PX.Objects.PO.POLine()
        {
          OrderType = fssoDet.POType,
          OrderNbr = fssoDet.PONbr,
          LineNbr = fssoDet.POLineNbr
        };
      PX.Objects.PO.POLine poLine2 = GraphHelper.Caches<PX.Objects.PO.POLine>(graph).Locate(poLine1);
      if (poLine2 != null)
        poLine1 = poLine2;
      else if (!flag)
        poLine1 = (PX.Objects.PO.POLine) null;
      return new
      {
        FSSODet = fssoDet,
        POLine = poLine1,
        AppointmentLines = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) PXSelectBase<FSAppointmentDet, PXViewOf<FSAppointmentDet>.BasedOn<SelectFromBase<FSAppointmentDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSAppointmentDet.sODetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
        {
          (object) fssoDet.SODetID
        })).ToList<FSAppointmentDet>()
      };
    }).Where(l => l.POLine != null).ToList();
    using (new PXTimeStampScope((byte[]) null))
    {
      PXCache cach1 = graph.Caches[typeof (FSSODet)];
      PXCache cach2 = graph.Caches[typeof (FSAppointmentDet)];
      foreach (var data in list)
      {
        FSSODet fssoDet1 = data.FSSODet;
        PX.Objects.PO.POLine poLine = data.POLine;
        FSSODet copy1 = (FSSODet) cach1.CreateCopy((object) fssoDet1);
        Decimal num = INUnitAttribute.ConvertFromBase(cach1, poLine.InventoryID, poLine.UOM, poLine.UnitCost.GetValueOrDefault(), INPrecision.UNITCOST);
        cach1.SetValueExt<FSSODet.curyUnitCost>((object) copy1, (object) num);
        cach1.SetValueExt<FSSODet.unitCost>((object) copy1, (object) num);
        FSSODet fssoDet2 = (FSSODet) cach1.Update((object) copy1);
        fssoDet2.tstamp = PXDatabase.SelectTimeStamp();
        PXTimeStampScope.DuplicatePersisted(cach1, (object) fssoDet2, typeof (FSSODet));
        PXTimeStampScope.SetRecordComesFirst(typeof (FSSODet), true);
        cach1.ResetPersisted((object) fssoDet2);
        cach1.Persist((object) fssoDet2, (PXDBOperation) 1);
        foreach (FSAppointmentDet appointmentLine in data.AppointmentLines)
        {
          FSAppointmentDet copy2 = (FSAppointmentDet) cach2.CreateCopy((object) appointmentLine);
          cach2.SetValueExt<FSAppointmentDet.curyUnitCost>((object) copy2, (object) num);
          cach2.SetValueExt<FSAppointmentDet.unitCost>((object) copy2, (object) num);
          FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) cach2.Update((object) copy2);
          fsAppointmentDet.tstamp = PXDatabase.SelectTimeStamp();
          PXTimeStampScope.DuplicatePersisted(cach2, (object) fsAppointmentDet, typeof (FSAppointmentDet));
          PXTimeStampScope.SetRecordComesFirst(typeof (FSAppointmentDet), true);
          cach2.ResetPersisted((object) fsAppointmentDet);
          cach2.Persist((object) fsAppointmentDet, (PXDBOperation) 1);
        }
      }
    }
  }

  public static void VerifyAndUpdateSrvOrdApptLineProjectTask(
    PXGraph graph,
    PX.Objects.PO.POLine poLineRow,
    PXSelect<FSSrvOrdType> fsSrvOrdTypeView,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSAppointmentDet> appointmentLineView)
  {
    FSPOReceiptProcess.SingleFSProcessPOReceipt.VerifyAndUpdateSrvOrdApptLineProjectTaskInt(graph, poLineRow, fsSrvOrdTypeView, fsSODetFixedDemand, appointmentLineView);
  }

  public virtual void VerifyAndUpdateSrvOrdApptLineProjectTaskInt(
    PXGraph graph,
    PX.Objects.PO.POLine poLineRow,
    PXSelect<FSSrvOrdType> fsSrvOrdTypeView,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSAppointmentDet> appointmentLineView)
  {
    List<\u003C\u003Ef__AnonymousType3<FSSODet, PX.Objects.PO.POLine, List<FSAppointmentDet>>> list = ((IEnumerable<PXResult<FSSODet>>) PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<FSSODet.poType>>>>, And<BqlOperand<PX.Objects.PO.POLine.orderNbr, IBqlString>.IsEqual<FSSODet.poNbr>>>>.And<BqlOperand<PX.Objects.PO.POLine.lineNbr, IBqlInt>.IsEqual<FSSODet.poLineNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.poType, Equal<P.AsString>>>>, And<BqlOperand<FSSODet.poNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.PO.POLine.taskID, IBqlInt>.IsNotEqual<FSSODet.projectTaskID>>>>.Config>.Select(graph, new object[2]
    {
      (object) poLineRow.OrderType,
      (object) poLineRow.OrderNbr
    })).AsEnumerable<PXResult<FSSODet>>().Select(res =>
    {
      FSSODet fssoDet = ((PXResult) res).GetItem<FSSODet>();
      PX.Objects.PO.POLine poLine1 = ((PXResult) res).GetItem<PX.Objects.PO.POLine>();
      bool flag = poLine1?.OrderNbr != null;
      if (!flag)
        poLine1 = new PX.Objects.PO.POLine()
        {
          OrderType = fssoDet.POType,
          OrderNbr = fssoDet.PONbr,
          LineNbr = fssoDet.POLineNbr
        };
      PX.Objects.PO.POLine poLine2 = GraphHelper.Caches<PX.Objects.PO.POLine>(graph).Locate(poLine1);
      if (poLine2 != null)
        poLine1 = poLine2;
      else if (!flag)
        poLine1 = (PX.Objects.PO.POLine) null;
      return new
      {
        FSSODet = fssoDet,
        POLine = poLine1,
        AppointmentLines = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) PXSelectBase<FSAppointmentDet, PXViewOf<FSAppointmentDet>.BasedOn<SelectFromBase<FSAppointmentDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSAppointmentDet.sODetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
        {
          (object) fssoDet.SODetID
        })).ToList<FSAppointmentDet>()
      };
    }).Where(l => l.POLine != null).ToList();
    using (new PXTimeStampScope((byte[]) null))
    {
      PXCache cach1 = graph.Caches[typeof (FSSODet)];
      PXCache cach2 = graph.Caches[typeof (FSAppointmentDet)];
      foreach (var data in list)
      {
        FSSODet fssoDet1 = data.FSSODet;
        ((PXSelectBase<FSSrvOrdType>) fsSrvOrdTypeView).Current = FSSrvOrdType.PK.Find(graph, fssoDet1.SrvOrdType);
        FSSODet copy1 = (FSSODet) cach1.CreateCopy((object) fssoDet1);
        copy1.TaskID = poLineRow.TaskID;
        FSSODet fssoDet2 = (FSSODet) cach1.Update((object) copy1);
        fssoDet2.tstamp = PXDatabase.SelectTimeStamp();
        PXTimeStampScope.DuplicatePersisted(cach1, (object) fssoDet2, typeof (FSSODet));
        PXTimeStampScope.SetRecordComesFirst(typeof (FSSODet), true);
        cach1.ResetPersisted((object) fssoDet2);
        cach1.Persist((object) fssoDet2, (PXDBOperation) 1);
        foreach (FSAppointmentDet appointmentLine in data.AppointmentLines)
        {
          FSAppointmentDet copy2 = (FSAppointmentDet) cach2.CreateCopy((object) appointmentLine);
          copy2.ProjectTaskID = poLineRow.TaskID;
          FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) cach2.Update((object) copy2);
          fsAppointmentDet.tstamp = PXDatabase.SelectTimeStamp();
          PXTimeStampScope.DuplicatePersisted(cach2, (object) fsAppointmentDet, typeof (FSAppointmentDet));
          PXTimeStampScope.SetRecordComesFirst(typeof (FSAppointmentDet), true);
          cach2.ResetPersisted((object) fsAppointmentDet);
          cach2.Persist((object) fsAppointmentDet, (PXDBOperation) 1);
        }
      }
    }
  }

  public static void RemovePOReferenceInSrvOrdLine(
    PXGraph graph,
    ServiceOrderEntry soEntrygraph,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSSODetSplit> fsSODetSplitFixedDemand,
    PXSelectBase<FSAppointment> appointmentView,
    PXSelectBase<FSAppointmentDet> appointmentLineView,
    PXSelectBase<INItemPlan> inItemPlans,
    FSSODet fsSODet,
    FSSODetSplit fsSODetSplit)
  {
    FSPOReceiptProcess.SingleFSProcessPOReceipt.RemovePOReferenceInSrvOrdLineInt(graph, soEntrygraph, fsSODetFixedDemand, fsSODetSplitFixedDemand, appointmentView, appointmentLineView, inItemPlans, fsSODet, fsSODetSplit);
  }

  public virtual void RemovePOReferenceInSrvOrdLineInt(
    PXGraph graph,
    ServiceOrderEntry soEntrygraph,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSSODetSplit> fsSODetSplitFixedDemand,
    PXSelectBase<FSAppointment> appointmentView,
    PXSelectBase<FSAppointmentDet> appointmentLineView,
    PXSelectBase<INItemPlan> inItemPlans,
    FSSODet fsSODet,
    FSSODetSplit fsSODetSplit)
  {
    if (fsSODetSplit == null)
      return;
    foreach (PXResult<INItemPlan, FSSODetSplit> pxResult in PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSSODetSplit>.On<BqlOperand<FSSODetSplit.planID, IBqlLong>.IsEqual<INItemPlan.planID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<FSSODetSplit.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FSSODetSplit.parentSplitLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INItemPlan.planType, IBqlString>.IsEqual<INPlanConstants.planF1>>>>.Config>.Select(graph, new object[4]
    {
      (object) fsSODetSplit.SrvOrdType,
      (object) fsSODetSplit.RefNbr,
      (object) fsSODetSplit.LineNbr,
      (object) fsSODetSplit.LineNbr
    }))
    {
      FSSODetSplit fssoDetSplit = PXResult<INItemPlan, FSSODetSplit>.op_Implicit(pxResult);
      INItemPlan inItemPlan = PXResult<INItemPlan, FSSODetSplit>.op_Implicit(pxResult);
      fsSODetSplitFixedDemand.Delete(fssoDetSplit);
      inItemPlans.Delete(inItemPlan);
    }
    FSSODet copy1 = (FSSODet) ((PXSelectBase) fsSODetFixedDemand).Cache.CreateCopy((object) fsSODet);
    copy1.POType = (string) null;
    copy1.PONbr = (string) null;
    copy1.POLineNbr = new int?();
    copy1.POStatus = (string) null;
    copy1.POCompleted = new bool?(false);
    fsSODetFixedDemand.Current = copy1;
    fsSODetFixedDemand.Update(copy1);
    FSSODetSplit copy2 = (FSSODetSplit) ((PXSelectBase) fsSODetSplitFixedDemand).Cache.CreateCopy((object) fsSODetSplit);
    copy2.Completed = new bool?(false);
    copy2.POCompleted = new bool?(false);
    copy2.POCancelled = new bool?(false);
    copy2.POLineNbr = new int?();
    copy2.POType = (string) null;
    copy2.PONbr = (string) null;
    copy2.POCreate = new bool?(true);
    fsSODetSplitFixedDemand.Current = copy2;
    INItemPlan inItemPlan1 = ((PXGraph) soEntrygraph).FindImplementation<FSSODetSplitPlan>().DefaultValues(copy2);
    if (inItemPlan1 != null)
    {
      INItemPlan inItemPlan2 = (INItemPlan) ((PXSelectBase) fsSODetSplitFixedDemand).Cache.Graph.Caches[typeof (INItemPlan)].Insert((object) inItemPlan1);
      copy2.PlanID = inItemPlan2.PlanID;
    }
    fsSODetSplitFixedDemand.Update(copy2);
    List<FSAppointmentDet> relatedApptLines = this.GetRelatedApptLines(graph, fsSODet.SODetID, false, new int?(), false, false);
    int? nullable1 = new int?();
    foreach (FSAppointmentDet fsAppointmentDet1 in (IEnumerable<FSAppointmentDet>) relatedApptLines.OrderBy<FSAppointmentDet, int?>((Func<FSAppointmentDet, int?>) (x => x.AppointmentID)))
    {
      if (nullable1.HasValue)
      {
        int? nullable2 = nullable1;
        int? appointmentId = fsAppointmentDet1.AppointmentID;
        if (nullable2.GetValueOrDefault() == appointmentId.GetValueOrDefault() & nullable2.HasValue == appointmentId.HasValue)
          goto label_16;
      }
      appointmentView.Current = (FSAppointment) PXParentAttribute.SelectParent(((PXSelectBase) appointmentLineView).Cache, (object) fsAppointmentDet1, typeof (FSAppointment));
      nullable1 = fsAppointmentDet1.AppointmentID;
label_16:
      FSAppointmentDet fsAppointmentDet2 = appointmentLineView.Current = (FSAppointmentDet) ((PXSelectBase) appointmentLineView).Cache.CreateCopy((object) fsAppointmentDet1);
      fsAppointmentDet2.POType = (string) null;
      fsAppointmentDet2.PONbr = (string) null;
      fsAppointmentDet2.POLineNbr = new int?();
      fsAppointmentDet2.POStatus = (string) null;
      fsAppointmentDet2.POCompleted = new bool?(false);
      fsAppointmentDet2.Status = "WP";
      appointmentLineView.Update(fsAppointmentDet2);
    }
  }

  public static void UpdatePOReferenceInSrvOrdLine(
    PXGraph graph,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSSODetSplit> fsSODetSplitFixedDemand,
    PXSelectBase<FSAppointment> appointmentView,
    PXSelectBase<FSAppointmentDet> appointmentLineView,
    FSSODet fsSODet,
    PX.Objects.PO.POOrder poOrder,
    int? poLineNbr,
    bool? poLineCompleted,
    PXCache inItemPlanCache,
    INItemPlan inItemPlan,
    bool clearPOReference)
  {
    FSPOReceiptProcess.UpdatePOReferenceInSrvOrdLine(graph, (PXSelectBase<FSServiceOrder>) null, fsSODetFixedDemand, fsSODetSplitFixedDemand, appointmentView, appointmentLineView, fsSODet, poOrder, poLineNbr, poLineCompleted, inItemPlanCache, inItemPlan, clearPOReference);
  }

  public static void UpdatePOReferenceInSrvOrdLine(
    PXGraph graph,
    PXSelectBase<FSServiceOrder> serviceOrderView,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSSODetSplit> fsSODetSplitFixedDemand,
    PXSelectBase<FSAppointment> appointmentView,
    PXSelectBase<FSAppointmentDet> appointmentLineView,
    FSSODet fsSODet,
    PX.Objects.PO.POOrder poOrder,
    int? poLineNbr,
    bool? poLineCompleted,
    PXCache inItemPlanCache,
    INItemPlan inItemPlan,
    bool clearPOReference)
  {
    FSPOReceiptProcess.UpdatePOReferenceInSrvOrdLine(graph, serviceOrderView, fsSODetFixedDemand, fsSODetSplitFixedDemand, appointmentView, appointmentLineView, fsSODet, poOrder, poLineNbr, poLineCompleted, inItemPlanCache, inItemPlan, clearPOReference, !(graph is POOrderEntry));
  }

  public static void UpdatePOReferenceInSrvOrdLine(
    PXGraph graph,
    PXSelectBase<FSServiceOrder> serviceOrderView,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSSODetSplit> fsSODetSplitFixedDemand,
    PXSelectBase<FSAppointment> appointmentView,
    PXSelectBase<FSAppointmentDet> appointmentLineView,
    FSSODet fsSODet,
    PX.Objects.PO.POOrder poOrder,
    int? poLineNbr,
    bool? poLineCompleted,
    PXCache inItemPlanCache,
    INItemPlan inItemPlan,
    bool clearPOReference,
    bool persistAppointmentLines)
  {
    FSPOReceiptProcess.SingleFSProcessPOReceipt.UpdatePOReferenceInSrvOrdLineInt(graph, serviceOrderView, fsSODetFixedDemand, fsSODetSplitFixedDemand, appointmentView, appointmentLineView, fsSODet, poOrder, poLineNbr, poLineCompleted, inItemPlanCache, inItemPlan, clearPOReference, persistAppointmentLines);
  }

  public virtual void UpdatePOReferenceInSrvOrdLineInt(
    PXGraph graph,
    PXSelectBase<FSServiceOrder> serviceOrderView,
    PXSelectBase<FSSODet> fsSODetFixedDemand,
    PXSelectBase<FSSODetSplit> fsSODetSplitFixedDemand,
    PXSelectBase<FSAppointment> appointmentView,
    PXSelectBase<FSAppointmentDet> appointmentLineView,
    FSSODet fsSODet,
    PX.Objects.PO.POOrder poOrder,
    int? poLineNbr,
    bool? poLineCompleted,
    PXCache inItemPlanCache,
    INItemPlan inItemPlan,
    bool clearPOReference,
    bool persistAppointmentLines)
  {
    if (fsSODet == null)
      return;
    string str1 = (string) null;
    string str2 = (string) null;
    int? nullable1 = new int?();
    string str3 = (string) null;
    int? nullable2 = new int?();
    int? nullable3 = new int?();
    bool? nullable4 = new bool?(false);
    if (!clearPOReference)
    {
      str1 = poOrder.OrderType;
      str2 = poOrder.OrderNbr;
      nullable1 = poLineNbr;
      str3 = poOrder.Status;
      nullable2 = poOrder.VendorID;
      nullable3 = poOrder.VendorLocationID;
      nullable4 = poLineCompleted;
    }
    else
    {
      INItemPlan copy = (INItemPlan) inItemPlanCache.CreateCopy((object) inItemPlan);
      copy.SupplyPlanID = new long?();
      INItemPlan inItemPlan1 = (INItemPlan) inItemPlanCache.Update((object) copy);
    }
    if (serviceOrderView != null)
      serviceOrderView.Current = (FSServiceOrder) PXParentAttribute.SelectParent(((PXSelectBase) fsSODetFixedDemand).Cache, (object) fsSODet, typeof (FSServiceOrder));
    FSSODet copy1 = (FSSODet) ((PXSelectBase) fsSODetFixedDemand).Cache.CreateCopy((object) fsSODet);
    int? nullable5;
    int? nullable6;
    bool? nullable7;
    bool? nullable8;
    if (!(copy1.POType != str1) && !(copy1.PONbr != str2))
    {
      nullable5 = copy1.POLineNbr;
      nullable6 = nullable1;
      if (nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue && !(copy1.POStatus != str3))
      {
        nullable7 = copy1.POCompleted;
        nullable8 = nullable4;
        if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue)
        {
          nullable6 = copy1.POVendorID;
          nullable5 = nullable2;
          if (nullable6.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable6.HasValue == nullable5.HasValue)
          {
            nullable5 = copy1.POVendorLocationID;
            nullable6 = nullable3;
            if (nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue)
              goto label_15;
          }
        }
      }
    }
    copy1.POType = str1;
    copy1.PONbr = str2;
    copy1.POLineNbr = nullable1;
    copy1.POStatus = str3;
    copy1.POCompleted = nullable4;
    if (!clearPOReference)
    {
      copy1.POVendorID = nullable2;
      copy1.POVendorLocationID = nullable3;
    }
    fsSODetFixedDemand.Update(copy1);
label_15:
    PXGraph graph1 = graph;
    int? soDetId = fsSODet.SODetID;
    nullable6 = new int?();
    int? apptDetID = nullable6;
    List<FSAppointmentDet> relatedApptLines = this.GetRelatedApptLines(graph1, soDetId, false, apptDetID, false, false);
    int? nullable9 = new int?();
    foreach (FSAppointmentDet fsAppointmentDet1 in (IEnumerable<FSAppointmentDet>) relatedApptLines.OrderBy<FSAppointmentDet, int?>((Func<FSAppointmentDet, int?>) (x => x.AppointmentID)))
    {
      if (nullable9.HasValue)
      {
        nullable6 = nullable9;
        nullable5 = fsAppointmentDet1.AppointmentID;
        if (nullable6.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable6.HasValue == nullable5.HasValue)
          goto label_20;
      }
      appointmentView.Current = (FSAppointment) PXParentAttribute.SelectParent(((PXSelectBase) appointmentLineView).Cache, (object) fsAppointmentDet1, typeof (FSAppointment));
      nullable9 = fsAppointmentDet1.AppointmentID;
label_20:
      FSAppointmentDet fsAppointmentDet2 = appointmentLineView.Current = (FSAppointmentDet) ((PXSelectBase) appointmentLineView).Cache.CreateCopy((object) fsAppointmentDet1);
      if (!(fsAppointmentDet2.POType != str1) && !(fsAppointmentDet2.PONbr != str2))
      {
        nullable5 = fsAppointmentDet2.POLineNbr;
        nullable6 = nullable1;
        if (nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue && !(fsAppointmentDet2.POStatus != str3))
        {
          nullable8 = fsAppointmentDet2.POCompleted;
          nullable7 = nullable4;
          if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue)
          {
            nullable6 = fsAppointmentDet2.POVendorID;
            nullable5 = nullable2;
            if (nullable6.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable6.HasValue == nullable5.HasValue)
            {
              nullable5 = fsAppointmentDet2.POVendorLocationID;
              nullable6 = nullable3;
              if (nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue)
                continue;
            }
          }
        }
      }
      fsAppointmentDet2.POType = str1;
      fsAppointmentDet2.PONbr = str2;
      fsAppointmentDet2.POLineNbr = nullable1;
      fsAppointmentDet2.POStatus = str3;
      fsAppointmentDet2.POCompleted = nullable4;
      fsAppointmentDet2.tstamp = PXDatabase.SelectTimeStamp();
      if (!clearPOReference)
      {
        fsAppointmentDet2.POVendorID = nullable2;
        fsAppointmentDet2.POVendorLocationID = nullable3;
      }
      FSAppointmentDet fsAppointmentDet3 = appointmentLineView.Update(fsAppointmentDet2);
      if (persistAppointmentLines)
      {
        ((PXSelectBase) appointmentLineView).Cache.ResetPersisted((object) fsAppointmentDet3);
        ((PXSelectBase) appointmentLineView).Cache.Persist((object) fsAppointmentDet3, (PXDBOperation) 1);
      }
    }
    if (inItemPlan == null)
      return;
    FSSODetSplit fssoDetSplit = PXResultset<FSSODetSplit>.op_Implicit(fsSODetSplitFixedDemand.Select(new object[1]
    {
      (object) inItemPlan.PlanID
    }));
    if (fssoDetSplit == null)
      return;
    FSSODetSplit copy2 = (FSSODetSplit) ((PXSelectBase) fsSODetSplitFixedDemand).Cache.CreateCopy((object) fssoDetSplit);
    if (!(copy2.POType != str1) && !(copy2.PONbr != str2))
    {
      int? poLineNbr1 = copy2.POLineNbr;
      int? nullable10 = nullable1;
      if (poLineNbr1.GetValueOrDefault() == nullable10.GetValueOrDefault() & poLineNbr1.HasValue == nullable10.HasValue)
      {
        bool? poCompleted = copy2.POCompleted;
        bool? nullable11 = nullable4;
        if (poCompleted.GetValueOrDefault() == nullable11.GetValueOrDefault() & poCompleted.HasValue == nullable11.HasValue)
          return;
      }
    }
    copy2.POType = str1;
    copy2.PONbr = str2;
    copy2.POLineNbr = nullable1;
    copy2.POCompleted = nullable4;
    fsSODetSplitFixedDemand.Update(copy2);
  }

  public virtual void UpdateLineStatusBasedOnReceivedPurchaseItems(
    FSAppointment appt,
    PXCache sender,
    FSAppointmentDet row,
    bool rowMustHaveRequestPOStatus,
    List<FSApptLineSplit> existingSplits,
    Decimal? baseExistingSplitTotalQty,
    Decimal? existingSplitTotalQty,
    bool runSetValueExt)
  {
    FSPOReceiptProcess.UpdateLineStatusBasedOnReceivedPurchaseItemsStatic(appt, sender, row, rowMustHaveRequestPOStatus, existingSplits, baseExistingSplitTotalQty, existingSplitTotalQty, runSetValueExt);
  }

  public static void UpdateLineStatusBasedOnReceivedPurchaseItemsStatic(
    FSAppointment appt,
    PXCache sender,
    FSAppointmentDet row,
    bool rowMustHaveRequestPOStatus,
    List<FSApptLineSplit> existingSplits,
    Decimal? baseExistingSplitTotalQty,
    Decimal? existingSplitTotalQty,
    bool runSetValueExt)
  {
    Decimal? nullable1 = row.AllocatedFromSrvOrdPOQty;
    FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(SharedFunctions.GetInventoryItemRow(sender.Graph, row.InventoryID));
    bool? nullable2;
    int num;
    if (extension == null)
    {
      num = 0;
    }
    else
    {
      nullable2 = extension.IsTravelItem;
      num = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    bool flag1 = (uint) num > 0U;
    Decimal? nullable3 = row.BaseAllocatedFromSrvOrdPOQty;
    string str1 = row.Status;
    nullable2 = row.EnablePO;
    bool flag2 = false;
    string str2;
    Decimal? nullable4;
    if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
    {
      nullable1 = new Decimal?(0M);
      nullable3 = new Decimal?(0M);
      str1 = "NS";
      nullable2 = appt.Completed;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = appt.ReopenActionRunning;
        bool flag3 = false;
        if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue && !flag1)
        {
          str2 = "CP";
          goto label_17;
        }
      }
      str2 = "NS";
    }
    else if (rowMustHaveRequestPOStatus)
    {
      nullable1 = new Decimal?(0M);
      nullable3 = new Decimal?(0M);
      str2 = "RP";
    }
    else
    {
      nullable1 = existingSplitTotalQty;
      nullable3 = baseExistingSplitTotalQty;
      Decimal? nullable5 = baseExistingSplitTotalQty;
      nullable4 = row.BaseEffTranQty;
      if (!(nullable5.GetValueOrDefault() >= nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue))
      {
        nullable2 = row.POCompleted;
        if (!nullable2.GetValueOrDefault())
        {
          str2 = "WP";
          goto label_17;
        }
      }
      nullable2 = appt.Completed;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = appt.ReopenActionRunning;
        bool flag4 = false;
        if (nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue && !flag1)
        {
          str2 = "CP";
          goto label_17;
        }
      }
      str2 = "NS";
    }
label_17:
    nullable4 = nullable3;
    Decimal? allocatedFromSrvOrdPoQty = row.BaseAllocatedFromSrvOrdPOQty;
    if (!(nullable4.GetValueOrDefault() == allocatedFromSrvOrdPoQty.GetValueOrDefault() & nullable4.HasValue == allocatedFromSrvOrdPoQty.HasValue))
    {
      if (runSetValueExt)
        sender.SetValueExt<FSAppointmentDet.baseAllocatedFromSrvOrdPOQty>((object) row, (object) nullable3);
      else
        row.BaseAllocatedFromSrvOrdPOQty = nullable3;
    }
    Decimal? nullable6 = nullable1;
    nullable4 = row.AllocatedFromSrvOrdPOQty;
    if (!(nullable6.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable6.HasValue == nullable4.HasValue))
    {
      if (runSetValueExt)
        sender.SetValueExt<FSAppointmentDet.allocatedFromSrvOrdPOQty>((object) row, (object) nullable1);
      else
        row.AllocatedFromSrvOrdPOQty = nullable1;
    }
    if (!(str2 != row.Status) || str2 == "NS" && row.Status != "WP" && row.Status != "RP" || str2 == "CP" && row.Status == "NF")
      return;
    if (runSetValueExt)
    {
      sender.SetValueExt<FSAppointmentDet.status>((object) row, (object) str2);
      PXCache cach = sender.Graph.Caches[typeof (FSAppointment)];
      if (cach == null || cach.Current == null)
        return;
      int? pendingPoLineCntr = ((FSAppointment) cach.Current).PendingPOLineCntr;
      PXFormulaAttribute.CalcAggregate<FSAppointmentDet.status>(sender, cach.Current);
      cach.RaiseFieldUpdated<FSAppointment.pendingPOLineCntr>(cach.Current, (object) pendingPoLineCntr);
    }
    else
      row.Status = str2;
  }

  public virtual bool MustHaveRequestPOStatus(FSAppointmentDet apptLine)
  {
    return FSPOReceiptProcess.MustHaveRequestPOStatusStatic(apptLine);
  }

  public static bool MustHaveRequestPOStatusStatic(FSAppointmentDet apptLine)
  {
    return apptLine.EnablePO.GetValueOrDefault() && apptLine.POSource == "O" && apptLine.SODetCreate.GetValueOrDefault();
  }

  public virtual List<FSAppointmentDet> GetRelatedApptLines(
    PXGraph graph,
    int? soDetID,
    bool excludeSpecificApptLine,
    int? apptDetID,
    bool onlyMarkForPOLines,
    bool sortResult)
  {
    return AppointmentEntry.GetRelatedApptLinesInt(graph, soDetID, excludeSpecificApptLine, apptDetID, onlyMarkForPOLines, sortResult);
  }

  public class SrvOrdLineWithSplits
  {
    public FSSODet SrvOrdLine;
    public List<FSPOReceiptProcess.SplitWithAdjustQty> Splits;
    protected Decimal _baseNewReceivedShippedQty;

    public Decimal BaseNewReceivedShippedQty => this._baseNewReceivedShippedQty;

    public SrvOrdLineWithSplits(
      FSSODet srvOrdLine,
      FSSODetSplit split,
      Decimal? baseReceivedShippedAdjustQty)
    {
      this.SrvOrdLine = srvOrdLine;
      this.Splits = new List<FSPOReceiptProcess.SplitWithAdjustQty>();
      this.Splits.Add(new FSPOReceiptProcess.SplitWithAdjustQty(split, baseReceivedShippedAdjustQty));
      this.AccumulateQuantities(baseReceivedShippedAdjustQty);
    }

    public virtual void AddUpdateSplit(FSSODetSplit split, Decimal? baseReceivedShippedAdjustQty)
    {
      FSPOReceiptProcess.SplitWithAdjustQty splitWithAdjustQty = this.Splits.Find((Predicate<FSPOReceiptProcess.SplitWithAdjustQty>) (e =>
      {
        if (e.Split.SrvOrdType == split.SrvOrdType && e.Split.RefNbr == split.RefNbr)
        {
          int? lineNbr = e.Split.LineNbr;
          int? nullable = split.LineNbr;
          if (lineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & lineNbr.HasValue == nullable.HasValue)
          {
            nullable = e.Split.SplitLineNbr;
            int? splitLineNbr = split.SplitLineNbr;
            return nullable.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & nullable.HasValue == splitLineNbr.HasValue;
          }
        }
        return false;
      }));
      if (splitWithAdjustQty != null)
      {
        this.AccumulateQuantities(new Decimal?(-splitWithAdjustQty.AdjustQty));
        this.Splits.Remove(splitWithAdjustQty);
      }
      this.Splits.Add(new FSPOReceiptProcess.SplitWithAdjustQty(split, baseReceivedShippedAdjustQty));
      this.AccumulateQuantities(baseReceivedShippedAdjustQty);
    }

    protected virtual void AccumulateQuantities(Decimal? baseReceivedShippedAdjustQty)
    {
      this._baseNewReceivedShippedQty += baseReceivedShippedAdjustQty.Value;
    }
  }

  public class SplitWithAdjustQty
  {
    public FSSODetSplit Split;
    public Decimal AdjustQty;

    public SplitWithAdjustQty(FSSODetSplit split, Decimal? adjustQty)
    {
      this.Split = split;
      this.AdjustQty = adjustQty.Value;
    }
  }
}
