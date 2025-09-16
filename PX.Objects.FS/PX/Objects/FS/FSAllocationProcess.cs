// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAllocationProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.FS;

public class FSAllocationProcess : PXGraph<FSAllocationProcess>
{
  public static FSAllocationProcess SingleFSDeallocateProcess
  {
    get
    {
      return PXContext.GetSlot<FSAllocationProcess>() ?? PXContext.SetSlot<FSAllocationProcess>(PXGraph.CreateInstance<FSAllocationProcess>());
    }
  }

  public static void DeallocateServiceOrderSplits(
    ServiceOrderEntry docgraph,
    List<FSSODetSplit> splitsToDeallocate,
    bool calledFromServiceOrder)
  {
    FSAllocationProcess.SingleFSDeallocateProcess.DeallocateServiceOrderSplitsInt(docgraph, splitsToDeallocate, calledFromServiceOrder);
  }

  public static void ReallocateServiceOrderSplits(
    List<AllocationHelper.AllocationInfo> requiredAllocationList)
  {
    FSAllocationProcess.SingleFSDeallocateProcess.ReallocateServiceOrderSplitsInt(requiredAllocationList);
  }

  public virtual void DeallocateServiceOrderSplitsInt(
    ServiceOrderEntry docgraph,
    List<FSSODetSplit> splitsToDeallocate,
    bool calledFromServiceOrder)
  {
    foreach (IGrouping<(string, string), FSSODetSplit> source1 in splitsToDeallocate.GroupBy<FSSODetSplit, (string, string)>((Func<FSSODetSplit, (string, string)>) (x => (x.SrvOrdType, x.RefNbr))))
    {
      FSSODetSplit fssoDetSplit1 = source1.First<FSSODetSplit>();
      FSServiceOrder fsServiceOrder1;
      if (!calledFromServiceOrder)
      {
        ((PXGraph) docgraph).Clear();
        PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords1 = docgraph.ServiceOrderRecords;
        PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords2 = docgraph.ServiceOrderRecords;
        string refNbr = fssoDetSplit1.RefNbr;
        object[] objArray = new object[1]
        {
          (object) fssoDetSplit1.SrvOrdType
        };
        FSServiceOrder fsServiceOrder2;
        FSServiceOrder fsServiceOrder3 = fsServiceOrder2 = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderRecords2).Search<FSServiceOrder.refNbr>((object) refNbr, objArray));
        ((PXSelectBase<FSServiceOrder>) serviceOrderRecords1).Current = fsServiceOrder2;
        fsServiceOrder1 = fsServiceOrder3;
      }
      else
        fsServiceOrder1 = ((PXSelectBase<FSServiceOrder>) docgraph.ServiceOrderRecords).Current;
      if (fsServiceOrder1.SrvOrdType != fssoDetSplit1.SrvOrdType || fsServiceOrder1.RefNbr != fssoDetSplit1.RefNbr)
        throw new PXException("The service order was not found.");
      foreach (IGrouping<(string, string, int?), FSSODetSplit> source2 in source1.GroupBy<FSSODetSplit, (string, string, int?)>((Func<FSSODetSplit, (string, string, int?)>) (x => (x.SrvOrdType, x.RefNbr, x.LineNbr))))
      {
        FSSODetSplit fssoDetSplit2 = source2.First<FSSODetSplit>();
        FSSODet line = ((PXSelectBase<FSSODet>) docgraph.ServiceOrderDetails).Current = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) docgraph.ServiceOrderDetails).Search<FSSODet.lineNbr>((object) fssoDetSplit2.LineNbr, Array.Empty<object>()));
        int? lineNbr1 = (int?) line?.LineNbr;
        int? lineNbr2 = fssoDetSplit2.LineNbr;
        if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
          throw new PXException("The {0} record was not found.", new object[1]
          {
            (object) DACHelper.GetDisplayName(typeof (FSSODet))
          });
        foreach (FSSODetSplit forSplit in (IEnumerable<FSSODetSplit>) source2.OrderByDescending<FSSODetSplit, bool?>((Func<FSSODetSplit, bool?>) (x => x.IsAllocated)))
        {
          FSSODetSplit fssoDetSplit3 = ((PXSelectBase<FSSODetSplit>) docgraph.Splits).Current = PXResultset<FSSODetSplit>.op_Implicit(((PXSelectBase<FSSODetSplit>) docgraph.Splits).Search<FSSODetSplit.splitLineNbr>((object) forSplit.SplitLineNbr, Array.Empty<object>()));
          if (fssoDetSplit3 != null)
          {
            int? splitLineNbr1 = forSplit.SplitLineNbr;
            int? splitLineNbr2 = fssoDetSplit3.SplitLineNbr;
            if (splitLineNbr1.GetValueOrDefault() == splitLineNbr2.GetValueOrDefault() & splitLineNbr1.HasValue == splitLineNbr2.HasValue)
            {
              if (this.CanUpdateRealSplit(fssoDetSplit3, forSplit))
              {
                Decimal? baseQty1 = fssoDetSplit3.BaseQty;
                Decimal? baseQty2 = forSplit.BaseQty;
                Decimal? baseDeallocationQty = baseQty1.HasValue & baseQty2.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() - baseQty2.GetValueOrDefault()) : new Decimal?();
                this.DeallocateSplitLine(docgraph, line, baseDeallocationQty, fssoDetSplit3);
                continue;
              }
              continue;
            }
          }
          throw new PXException("The {0} record was not found.", new object[1]
          {
            (object) DACHelper.GetDisplayName(typeof (FSSODetSplit))
          });
        }
      }
      if (!calledFromServiceOrder)
        docgraph.SkipTaxCalcAndSave();
    }
  }

  public virtual void ReallocateServiceOrderSplitsInt(
    List<AllocationHelper.AllocationInfo> requiredAllocationList)
  {
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXRowUpdating pxRowUpdating = FSAllocationProcess.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (FSAllocationProcess.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXRowUpdating((object) FSAllocationProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReallocateServiceOrderSplitsInt\u003Eb__5_0)));
    foreach (IGrouping<(string, string), AllocationHelper.AllocationInfo> source1 in requiredAllocationList.GroupBy<AllocationHelper.AllocationInfo, (string, string)>((Func<AllocationHelper.AllocationInfo, (string, string)>) (x => (x.SrvOrdType, x.RefNbr))))
    {
      AllocationHelper.AllocationInfo allocationInfo1 = source1.First<AllocationHelper.AllocationInfo>();
      ((PXGraph) instance).Clear();
      PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords1 = instance.ServiceOrderRecords;
      PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords2 = instance.ServiceOrderRecords;
      string refNbr = allocationInfo1.RefNbr;
      object[] objArray = new object[1]
      {
        (object) allocationInfo1.SrvOrdType
      };
      FSServiceOrder fsServiceOrder1;
      FSServiceOrder fsServiceOrder2 = fsServiceOrder1 = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderRecords2).Search<FSServiceOrder.refNbr>((object) refNbr, objArray));
      ((PXSelectBase<FSServiceOrder>) serviceOrderRecords1).Current = fsServiceOrder1;
      FSServiceOrder fsServiceOrder3 = fsServiceOrder2;
      if (fsServiceOrder3.SrvOrdType != allocationInfo1.SrvOrdType || fsServiceOrder3.RefNbr != allocationInfo1.RefNbr)
        throw new PXException("The service order was not found.");
      IEnumerable<IGrouping<(string, string, int), AllocationHelper.AllocationInfo>> groupings = source1.GroupBy<AllocationHelper.AllocationInfo, (string, string, int)>((Func<AllocationHelper.AllocationInfo, (string, string, int)>) (x => (x.SrvOrdType, x.RefNbr, x.LineNbr)));
      using (instance.LineSplittingExt.SuppressedModeScope(true))
      {
        foreach (IGrouping<(string, string, int), AllocationHelper.AllocationInfo> source2 in groupings)
        {
          AllocationHelper.AllocationInfo allocationInfo2 = source2.First<AllocationHelper.AllocationInfo>();
          int? lineNbr1 = (((PXSelectBase<FSSODet>) instance.ServiceOrderDetails).Current = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) instance.ServiceOrderDetails).Search<FSSODet.lineNbr>((object) allocationInfo2.LineNbr, Array.Empty<object>()))).LineNbr;
          int lineNbr2 = allocationInfo2.LineNbr;
          if (!(lineNbr1.GetValueOrDefault() == lineNbr2 & lineNbr1.HasValue))
            throw new PXException("The {0} record was not found.", new object[1]
            {
              (object) DACHelper.GetDisplayName(typeof (FSSODet))
            });
          ((PXGraph) instance).RowUpdating.AddHandler<FSSODet>(pxRowUpdating);
          try
          {
            foreach (AllocationHelper.AllocationInfo allocationInfo3 in (IEnumerable<AllocationHelper.AllocationInfo>) source2)
              this.ReallocateItemIntoCurrentLineSplit(instance, allocationInfo3);
            this.MergeEqualSplitsForCurrentLine(instance, (Func<FSSODetSplit, bool>) (s => s.Completed.GetValueOrDefault() && !s.PlanID.HasValue));
            this.MergeEqualSplitsForCurrentLine(instance, (Func<FSSODetSplit, bool>) (s =>
            {
              bool? completed = s.Completed;
              bool flag = false;
              return completed.GetValueOrDefault() == flag & completed.HasValue;
            }));
          }
          finally
          {
            ((PXGraph) instance).RowUpdating.RemoveHandler<FSSODet>(pxRowUpdating);
          }
          this.UpdateCurrentLineBasedOnSplit(instance);
        }
      }
      ((PXAction) instance.Save).Press();
    }
  }

  public virtual void ReallocateItemIntoCurrentLineSplit(
    ServiceOrderEntry graph,
    AllocationHelper.AllocationInfo allocationInfo,
    int recursionLevel = 0)
  {
    if (allocationInfo.Qty <= 0M)
      return;
    FSSODetSplit fssoDetSplit1 = GraphHelper.RowCast<FSSODetSplit>((IEnumerable) ((PXSelectBase<FSSODetSplit>) graph.Splits).Select(Array.Empty<object>())).Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (s =>
    {
      if (s.Completed.GetValueOrDefault())
      {
        bool? poCreate = s.POCreate;
        bool flag = false;
        if (poCreate.GetValueOrDefault() == flag & poCreate.HasValue)
        {
          Decimal? shippedQty = s.ShippedQty;
          Decimal num = 0M;
          if (shippedQty.GetValueOrDefault() > num & shippedQty.HasValue)
            return string.IsNullOrEmpty(s.LotSerialNbr) && string.IsNullOrEmpty(allocationInfo.LotSerialNbr) || s.LotSerialNbr == allocationInfo.LotSerialNbr;
        }
      }
      return false;
    })).OrderBy<FSSODetSplit, Decimal?>((Func<FSSODetSplit, Decimal?>) (s => s.ShippedQty)).ThenBy<FSSODetSplit, int?>((Func<FSSODetSplit, int?>) (s => s.SplitLineNbr)).FirstOrDefault<FSSODetSplit>();
    if (fssoDetSplit1 == null)
    {
      FSSODet fssoDet = GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) graph.ServiceOrderDetails).Select(Array.Empty<object>())).Where<FSSODet>((Func<FSSODet, bool>) (d =>
      {
        if (!(d.RefNbr == allocationInfo.RefNbr) || !(d.SrvOrdType == allocationInfo.SrvOrdType))
          return false;
        int? soDetId1 = d.SODetID;
        int soDetId2 = allocationInfo.SODetID;
        return soDetId1.GetValueOrDefault() == soDetId2 & soDetId1.HasValue;
      })).FirstOrDefault<FSSODet>();
      if ((recursionLevel != 0 || allocationInfo.LotSerialNbr != null) && (fssoDet == null || fssoDet.IsInventoryItem))
        throw new PXException("The {0} record was not found.", new object[1]
        {
          (object) DACHelper.GetDisplayName(typeof (FSSODetSplit))
        });
    }
    else
    {
      Decimal qty = allocationInfo.Qty;
      Decimal num1 = qty;
      Decimal? nullable1 = fssoDetSplit1.ShippedQty;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      if (num1 > valueOrDefault1 & nullable1.HasValue)
      {
        nullable1 = fssoDetSplit1.ShippedQty;
        qty = nullable1.Value;
      }
      FSSODetSplit fssoDetSplit2 = (FSSODetSplit) null;
      Decimal? nullable2 = new Decimal?();
      Decimal? nullable3 = new Decimal?();
      Decimal num2 = qty;
      nullable1 = fssoDetSplit1.ShippedQty;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      FSSODetSplit openSplit;
      if (num2 < valueOrDefault2 & nullable1.HasValue)
      {
        fssoDetSplit2 = fssoDetSplit1;
        nullable1 = fssoDetSplit1.ShippedQty;
        Decimal num3 = qty;
        nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num3) : new Decimal?();
        nullable3 = new Decimal?(qty);
        FSSODetSplit copy = (FSSODetSplit) ((PXSelectBase) graph.Splits).Cache.CreateCopy((object) fssoDetSplit1);
        copy.ParentSplitLineNbr = fssoDetSplit1.SplitLineNbr;
        copy.SplitLineNbr = new int?();
        copy.PlanID = new long?();
        openSplit = ((PXSelectBase<FSSODetSplit>) graph.Splits).Insert(copy);
      }
      else
      {
        openSplit = fssoDetSplit1;
        nullable3 = fssoDetSplit1.Qty;
      }
      if (fssoDetSplit2 != null)
      {
        nullable1 = fssoDetSplit2.OpenQty;
        Decimal num4 = 0M;
        if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
        {
          nullable1 = fssoDetSplit2.BaseOpenQty;
          Decimal num5 = 0M;
          if (nullable1.GetValueOrDefault() == num5 & nullable1.HasValue)
          {
            fssoDetSplit2.Qty = nullable2;
            FSSODetSplit fssoDetSplit3 = fssoDetSplit2;
            PXCache cache = ((PXSelectBase) graph.Splits).Cache;
            int? inventoryId = fssoDetSplit2.InventoryID;
            string uom = fssoDetSplit2.UOM;
            nullable1 = fssoDetSplit2.Qty;
            Decimal num6 = nullable1.Value;
            Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertToBase(cache, inventoryId, uom, num6, INPrecision.QUANTITY));
            fssoDetSplit3.BaseQty = nullable4;
            fssoDetSplit2.ShippedQty = fssoDetSplit2.Qty;
            fssoDetSplit2.BaseShippedQty = fssoDetSplit2.BaseQty;
            ((PXSelectBase<FSSODetSplit>) graph.Splits).Update(fssoDetSplit2);
            goto label_16;
          }
        }
        throw new PXInvalidOperationException();
      }
label_16:
      if (openSplit != null)
      {
        openSplit.Qty = nullable3;
        FSSODetSplit fssoDetSplit4 = openSplit;
        PXCache cache = ((PXSelectBase) graph.Splits).Cache;
        int? inventoryId = openSplit.InventoryID;
        string uom = openSplit.UOM;
        nullable1 = openSplit.Qty;
        Decimal num7 = nullable1.Value;
        Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertToBase(cache, inventoryId, uom, num7, INPrecision.QUANTITY));
        fssoDetSplit4.BaseQty = nullable5;
        openSplit.OpenQty = openSplit.Qty;
        openSplit.BaseOpenQty = openSplit.BaseQty;
        openSplit.ShippedQty = new Decimal?(0M);
        openSplit.BaseShippedQty = new Decimal?(0M);
        openSplit.Completed = new bool?(false);
        this.CreateAndAssignPlan(((PXSelectBase) graph.Splits).Cache, openSplit);
        ((PXSelectBase<FSSODetSplit>) graph.Splits).Update(openSplit);
      }
      allocationInfo.Qty -= qty;
      if (!(allocationInfo.Qty > 0M))
        return;
      this.ReallocateItemIntoCurrentLineSplit(graph, allocationInfo, recursionLevel + 1);
    }
  }

  public static List<AllocationHelper.AllocationInfo> GetRequiredAllocationList(
    PXGraph graph,
    object createdDoc)
  {
    return FSAllocationProcess.SingleFSDeallocateProcess.GetRequiredAllocationListInt(graph, createdDoc);
  }

  public virtual List<AllocationHelper.AllocationInfo> GetRequiredAllocationListInt(
    PXGraph graph,
    object createdDoc)
  {
    PXResultset<FSContractPostDet> pxResultset1 = (PXResultset<FSContractPostDet>) null;
    PXResultset<FSPostDet> pxResultset2;
    switch (createdDoc)
    {
      case PX.Objects.SO.SOOrder _:
        PX.Objects.SO.SOOrder soOrder = (PX.Objects.SO.SOOrder) createdDoc;
        pxResultset2 = PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, LeftJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>>>, Where<FSPostDet.sOOrderNbr, Equal<Required<FSPostDet.sOOrderNbr>>, And<FSPostDet.sOOrderType, Equal<Required<FSPostDet.sOOrderType>>>>>.Config>.Select(graph, new object[2]
        {
          (object) soOrder.OrderNbr,
          (object) soOrder.OrderType
        });
        if (pxResultset2 != null && pxResultset2.Count == 0)
        {
          pxResultset1 = PXSelectBase<FSContractPostDet, PXSelectJoin<FSContractPostDet, InnerJoin<FSContractPostDoc, On<FSContractPostDoc.contractPostDocID, Equal<FSContractPostDet.contractPostDocID>>, LeftJoin<FSSODet, On<FSSODet.sODetID, Equal<FSContractPostDet.sODetID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.appDetID, Equal<FSContractPostDet.appDetID>>>>>, Where<FSContractPostDoc.postedTO, Equal<Required<FSContractPostDoc.postedTO>>, And<FSContractPostDoc.postDocType, Equal<Required<FSContractPostDoc.postDocType>>, And<FSContractPostDoc.postRefNbr, Equal<Required<FSContractPostDoc.postRefNbr>>>>>>.Config>.Select(graph, new object[3]
          {
            (object) "SO",
            (object) soOrder.OrderType,
            (object) soOrder.OrderNbr
          });
          break;
        }
        break;
      case PX.Objects.SO.SOInvoice _:
        PX.Objects.SO.SOInvoice soInvoice = (PX.Objects.SO.SOInvoice) createdDoc;
        pxResultset2 = PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, LeftJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>>>, Where<FSPostDet.sOInvRefNbr, Equal<Required<FSPostDet.sOInvRefNbr>>, And<FSPostDet.sOInvDocType, Equal<Required<FSPostDet.sOInvDocType>>>>>.Config>.Select(graph, new object[2]
        {
          (object) soInvoice.RefNbr,
          (object) soInvoice.DocType
        });
        break;
      case PX.Objects.IN.INRegister _:
        PX.Objects.IN.INRegister inRegister = (PX.Objects.IN.INRegister) createdDoc;
        pxResultset2 = PXSelectBase<FSPostDet, PXSelectJoin<FSPostDet, LeftJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>>>, Where<FSPostDet.iNRefNbr, Equal<Required<FSPostDet.iNRefNbr>>, And<FSPostDet.iNDocType, Equal<Required<FSPostDet.iNDocType>>>>>.Config>.Select(graph, new object[2]
        {
          (object) inRegister.RefNbr,
          (object) inRegister.DocType
        });
        break;
      default:
        throw new NotImplementedException();
    }
    Dictionary<string, AllocationHelper.AllocationInfo> requiredAllocationList = new Dictionary<string, AllocationHelper.AllocationInfo>();
    foreach (PXResult<FSPostDet, FSSODet, FSAppointmentDet> pxResult in pxResultset2)
      this.AddRequiredAllocationToList(graph, requiredAllocationList, PXResult<FSPostDet, FSSODet, FSAppointmentDet>.op_Implicit(pxResult), PXResult<FSPostDet, FSSODet, FSAppointmentDet>.op_Implicit(pxResult));
    if (pxResultset1 != null)
    {
      foreach (PXResult<FSContractPostDet, FSContractPostDoc, FSSODet, FSAppointmentDet> pxResult in pxResultset1)
        this.AddRequiredAllocationToList(graph, requiredAllocationList, PXResult<FSContractPostDet, FSContractPostDoc, FSSODet, FSAppointmentDet>.op_Implicit(pxResult), PXResult<FSContractPostDet, FSContractPostDoc, FSSODet, FSAppointmentDet>.op_Implicit(pxResult));
    }
    return requiredAllocationList.Values.ToList<AllocationHelper.AllocationInfo>();
  }

  public virtual void AddRequiredAllocationToList(
    PXGraph graph,
    Dictionary<string, AllocationHelper.AllocationInfo> requiredAllocationList,
    FSSODet soDetLine,
    FSAppointmentDet apptLine)
  {
    if (apptLine == null || apptLine.RefNbr == null)
    {
      if (soDetLine == null)
        soDetLine = FSSODet.UK.Find(graph, apptLine.SODetID);
      foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>, And<FSSODetSplit.pOCreate, Equal<False>, And<FSSODetSplit.completed, Equal<True>>>>>>>.Config>.Select(graph, new object[3]
      {
        (object) soDetLine.SrvOrdType,
        (object) soDetLine.RefNbr,
        (object) soDetLine.LineNbr
      }))
      {
        AllocationHelper.AllocationInfo splitLine = new AllocationHelper.AllocationInfo(PXResult<FSSODetSplit>.op_Implicit(pxResult), soDetLine);
        requiredAllocationList.Add(splitLine);
      }
    }
    else
    {
      FSSODet srvOrdLine = FSSODet.UK.Find(graph, apptLine.SODetID);
      if (srvOrdLine == null)
        throw new PXException("The {0} record was not found.", new object[1]
        {
          (object) DACHelper.GetDisplayName(typeof (FSSODet))
        });
      PXResultset<FSApptLineSplit> pxResultset = PXSelectBase<FSApptLineSplit, PXSelect<FSApptLineSplit, Where<FSApptLineSplit.srvOrdType, Equal<Required<FSApptLineSplit.srvOrdType>>, And<FSApptLineSplit.apptNbr, Equal<Required<FSApptLineSplit.apptNbr>>, And<FSApptLineSplit.lineNbr, Equal<Required<FSApptLineSplit.lineNbr>>>>>>.Config>.Select(graph, new object[3]
      {
        (object) apptLine.SrvOrdType,
        (object) apptLine.RefNbr,
        (object) apptLine.LineNbr
      });
      if (pxResultset.Count > 0)
      {
        foreach (PXResult<FSApptLineSplit> pxResult in pxResultset)
        {
          AllocationHelper.AllocationInfo splitLine = new AllocationHelper.AllocationInfo(PXResult<FSApptLineSplit>.op_Implicit(pxResult), srvOrdLine);
          requiredAllocationList.Add(splitLine);
        }
      }
      else
      {
        AllocationHelper.AllocationInfo splitLine = new AllocationHelper.AllocationInfo(apptLine.BillableQty, srvOrdLine);
        requiredAllocationList.Add(splitLine);
      }
    }
  }

  public virtual bool CanUpdateRealSplit(FSSODetSplit realSplit, FSSODetSplit forSplit)
  {
    if (realSplit.Completed.GetValueOrDefault() || !string.IsNullOrEmpty(realSplit.LotSerialNbr) && realSplit.LotSerialNbr != forSplit.LotSerialNbr)
      return false;
    int? nullable;
    if (realSplit.SiteID.HasValue)
    {
      nullable = realSplit.SiteID;
      int? siteId = forSplit.SiteID;
      if (!(nullable.GetValueOrDefault() == siteId.GetValueOrDefault() & nullable.HasValue == siteId.HasValue))
        return false;
    }
    if (realSplit.LocationID.HasValue)
    {
      int? locationId = realSplit.LocationID;
      nullable = forSplit.LocationID;
      if (!(locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue))
        return false;
    }
    return true;
  }

  public virtual Decimal? DeallocateSplitLine(
    ServiceOrderEntry docgraph,
    FSSODet line,
    Decimal? baseDeallocationQty,
    FSSODetSplit split)
  {
    Decimal? nullable1 = baseDeallocationQty;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() <= num1 & nullable1.HasValue)
      return new Decimal?(0M);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXRowUpdating pxRowUpdating = FSAllocationProcess.\u003C\u003Ec.\u003C\u003E9__11_0 ?? (FSAllocationProcess.\u003C\u003Ec.\u003C\u003E9__11_0 = new PXRowUpdating((object) FSAllocationProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CDeallocateSplitLine\u003Eb__11_0)));
    ((PXGraph) docgraph).RowUpdating.AddHandler<FSSODet>(pxRowUpdating);
    PXCache cache = ((PXSelectBase) docgraph.Splits).Cache;
    Decimal? baseQty = split.BaseQty;
    Decimal? baseShippedQty1 = split.BaseShippedQty;
    Decimal? nullable2 = baseQty.HasValue & baseShippedQty1.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - baseShippedQty1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = nullable2;
    Decimal? nullable4 = baseDeallocationQty;
    Decimal? nullable5;
    Decimal? nullable6;
    if (nullable3.GetValueOrDefault() <= nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue)
    {
      nullable5 = new Decimal?(0M);
      FSSODetSplit fssoDetSplit = split;
      Decimal? baseShippedQty2 = fssoDetSplit.BaseShippedQty;
      nullable6 = nullable2;
      fssoDetSplit.BaseShippedQty = baseShippedQty2.HasValue & nullable6.HasValue ? new Decimal?(baseShippedQty2.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      nullable6 = baseDeallocationQty;
      Decimal? nullable7 = nullable2;
      baseDeallocationQty = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      Decimal? nullable8 = nullable2;
      Decimal? nullable9 = baseDeallocationQty;
      nullable5 = nullable8.HasValue & nullable9.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new Decimal?();
      split.BaseQty = baseDeallocationQty;
      split.Qty = new Decimal?(INUnitAttribute.ConvertFromBase(cache, split.InventoryID, split.UOM, split.BaseQty.Value, INPrecision.QUANTITY));
      split.BaseShippedQty = baseDeallocationQty;
      baseDeallocationQty = new Decimal?(0M);
    }
    FSSODetSplit fssoDetSplit1 = split;
    PXCache sender = cache;
    int? inventoryId = split.InventoryID;
    string uom = split.UOM;
    nullable6 = split.BaseShippedQty;
    Decimal num2 = nullable6.Value;
    Decimal? nullable10 = new Decimal?(INUnitAttribute.ConvertFromBase(sender, inventoryId, uom, num2, INPrecision.QUANTITY));
    fssoDetSplit1.ShippedQty = nullable10;
    split.Completed = new bool?(true);
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) docgraph, new object[1]
    {
      (object) split.PlanID
    }));
    if (inItemPlan != null)
    {
      ((PXGraph) docgraph).Caches[typeof (INItemPlan)].Delete((object) inItemPlan);
      split.PlanID = new long?();
    }
    using (docgraph.LineSplittingExt.SuppressedModeScope(true))
      split = ((PXSelectBase<FSSODetSplit>) docgraph.Splits).Update(split);
    Decimal? nullable11 = nullable5;
    Decimal num3 = 0M;
    if (nullable11.GetValueOrDefault() > num3 & nullable11.HasValue)
    {
      if (split.POCreate.GetValueOrDefault())
        throw new PXInvalidOperationException();
      FSSODetSplit copy1 = (FSSODetSplit) ((PXSelectBase) docgraph.Splits).Cache.CreateCopy((object) split);
      FSPOReceiptProcess.ClearScheduleReferences(ref copy1);
      copy1.POReceiptType = split.POReceiptType;
      copy1.POReceiptNbr = split.POReceiptNbr;
      FSSODetSplit copy2 = (FSSODetSplit) ((PXSelectBase) docgraph.Splits).Cache.CreateCopy((object) ((PXSelectBase<FSSODetSplit>) docgraph.Splits).Insert(copy1));
      copy2.PlanType = split.PlanType;
      copy2.IsAllocated = split.IsAllocated;
      copy2.ShipmentNbr = (string) null;
      copy2.LotSerialNbr = string.IsNullOrEmpty(split.LotSerialNbr) ? copy2.LotSerialNbr : (string) null;
      copy2.VendorID = new int?();
      copy2.BaseReceivedQty = new Decimal?(0M);
      copy2.ReceivedQty = new Decimal?(0M);
      copy2.BaseShippedQty = new Decimal?(0M);
      copy2.ShippedQty = new Decimal?(0M);
      copy2.BaseQty = nullable5;
      copy2.Qty = new Decimal?(INUnitAttribute.ConvertFromBase(cache, copy2.InventoryID, copy2.UOM, copy2.BaseQty.Value, INPrecision.QUANTITY));
      using (docgraph.LineSplittingExt.SuppressedModeScope(true))
        ((PXSelectBase<FSSODetSplit>) docgraph.Splits).Update(copy2);
    }
    ((PXGraph) docgraph).RowUpdating.RemoveHandler<FSSODet>(pxRowUpdating);
    this.ConfirmSingleLine(docgraph, split, line);
    return baseDeallocationQty;
  }

  public virtual void ConfirmSingleLine(
    ServiceOrderEntry docgraph,
    FSSODetSplit splitLine,
    FSSODet line)
  {
    using (docgraph.LineSplittingExt.SuppressedModeScope(true))
    {
      PXCache cache = ((PXSelectBase) docgraph.ServiceOrderDetails).Cache;
      FSSODet copy = (FSSODet) cache.CreateCopy((object) line);
      Decimal? nullable1 = copy.BaseShippedQty;
      Decimal? nullable2 = copy.BaseOrderQty;
      if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      {
        FSSODet fssoDet1 = copy;
        nullable2 = fssoDet1.BaseShippedQty;
        nullable1 = splitLine.BaseShippedQty;
        fssoDet1.BaseShippedQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        FSSODet fssoDet2 = copy;
        nullable1 = fssoDet2.ShippedQty;
        nullable2 = splitLine.ShippedQty;
        fssoDet2.ShippedQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        FSSODet fssoDet3 = copy;
        nullable2 = copy.OrderQty;
        nullable1 = copy.ShippedQty;
        Decimal? nullable3 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        fssoDet3.OpenQty = nullable3;
        FSSODet fssoDet4 = copy;
        nullable1 = copy.BaseOrderQty;
        nullable2 = copy.BaseShippedQty;
        Decimal? nullable4 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        fssoDet4.BaseOpenQty = nullable4;
        copy.ClosedQty = copy.ShippedQty;
        copy.BaseClosedQty = copy.BaseShippedQty;
        FSSODet fssoDet5 = copy;
        nullable2 = copy.OpenQty;
        Decimal num = 0M;
        bool? nullable5 = new bool?(nullable2.GetValueOrDefault() == num & nullable2.HasValue);
        fssoDet5.Completed = nullable5;
        cache.Update((object) copy);
      }
      else
      {
        copy.OpenQty = new Decimal?(0M);
        copy.ClosedQty = copy.OrderQty;
        FSSODet fssoDet6 = copy;
        nullable2 = fssoDet6.ShippedQty;
        nullable1 = splitLine.ShippedQty;
        fssoDet6.ShippedQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        FSSODet fssoDet7 = copy;
        nullable1 = copy.BaseOrderQty;
        nullable2 = copy.BaseShippedQty;
        Decimal? nullable6 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        fssoDet7.BaseOpenQty = nullable6;
        copy.BaseClosedQty = copy.BaseOrderQty;
        copy.Completed = new bool?(true);
        cache.Update((object) copy);
        docgraph.LineSplittingAllocatedExt.CompleteSchedules(line);
      }
    }
  }

  public virtual void UpdateCurrentLineBasedOnSplit(ServiceOrderEntry graph)
  {
    FSSODet current = ((PXSelectBase<FSSODet>) graph.ServiceOrderDetails).Current;
    current.OpenQty = current != null ? current.OrderQty : throw new ArgumentNullException();
    current.BaseOpenQty = current.BaseOrderQty;
    current.ShippedQty = new Decimal?(0M);
    current.BaseShippedQty = new Decimal?(0M);
    current.Completed = new bool?(false);
    foreach (PXResult<FSSODetSplit> pxResult in ((PXSelectBase<FSSODetSplit>) graph.Splits).Select(Array.Empty<object>()))
    {
      FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
      bool? nullable1 = fssoDetSplit.POCreate;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = fssoDetSplit.Completed;
        bool flag = false;
        if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
        {
          FSSODet fssoDet1 = current;
          Decimal? nullable2 = fssoDet1.OpenQty;
          Decimal? shippedQty1 = fssoDetSplit.ShippedQty;
          fssoDet1.OpenQty = nullable2.HasValue & shippedQty1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - shippedQty1.GetValueOrDefault()) : new Decimal?();
          FSSODet fssoDet2 = current;
          Decimal? baseOpenQty = fssoDet2.BaseOpenQty;
          nullable2 = fssoDetSplit.BaseShippedQty;
          fssoDet2.BaseOpenQty = baseOpenQty.HasValue & nullable2.HasValue ? new Decimal?(baseOpenQty.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          FSSODet fssoDet3 = current;
          nullable2 = fssoDet3.ShippedQty;
          Decimal? shippedQty2 = fssoDetSplit.ShippedQty;
          fssoDet3.ShippedQty = nullable2.HasValue & shippedQty2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + shippedQty2.GetValueOrDefault()) : new Decimal?();
          FSSODet fssoDet4 = current;
          Decimal? baseShippedQty = fssoDet4.BaseShippedQty;
          nullable2 = fssoDetSplit.BaseShippedQty;
          fssoDet4.BaseShippedQty = baseShippedQty.HasValue & nullable2.HasValue ? new Decimal?(baseShippedQty.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        }
      }
    }
    FSSODet fssoDet = current;
    Decimal? openQty = current.OpenQty;
    Decimal num = 0M;
    bool? nullable = new bool?(openQty.GetValueOrDefault() == num & openQty.HasValue);
    fssoDet.Completed = nullable;
    ((PXSelectBase<FSSODet>) graph.ServiceOrderDetails).Update(current);
  }

  public virtual void MergeEqualSplitsForCurrentLine(
    ServiceOrderEntry graph,
    Func<FSSODetSplit, bool> filter)
  {
    IEnumerable<IGrouping<FSAllocationProcess.SplitKey, FSSODetSplit>> groupings = GraphHelper.RowCast<FSSODetSplit>((IEnumerable) ((PXSelectBase<FSSODetSplit>) graph.Splits).Select(Array.Empty<object>())).Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (s =>
    {
      bool? poCreate = s.POCreate;
      bool flag = false;
      return poCreate.GetValueOrDefault() == flag & poCreate.HasValue && filter(s);
    })).OrderBy<FSSODetSplit, int?>((Func<FSSODetSplit, int?>) (s => s.SplitLineNbr)).ToList<FSSODetSplit>().GroupBy<FSSODetSplit, FSAllocationProcess.SplitKey>((Func<FSSODetSplit, FSAllocationProcess.SplitKey>) (s => new FSAllocationProcess.SplitKey(s)));
    PXCache cach = ((PXGraph) graph).Caches[typeof (INItemPlan)];
    foreach (IGrouping<FSAllocationProcess.SplitKey, FSSODetSplit> source in groupings)
    {
      if (source.Count<FSSODetSplit>() != 1)
      {
        FSSODetSplit fssoDetSplit1 = source.First<FSSODetSplit>();
        long? nullable1;
        foreach (FSSODetSplit split in (IEnumerable<FSSODetSplit>) source)
        {
          int? nullable2 = split.SplitLineNbr;
          int? splitLineNbr1 = fssoDetSplit1.SplitLineNbr;
          if (!(nullable2.GetValueOrDefault() == splitLineNbr1.GetValueOrDefault() & nullable2.HasValue == splitLineNbr1.HasValue))
          {
            if (split.PlanType != fssoDetSplit1.PlanType)
              throw new PXInvalidOperationException();
            int? splitLineNbr2 = split.SplitLineNbr;
            nullable2 = fssoDetSplit1.ParentSplitLineNbr;
            if (splitLineNbr2.GetValueOrDefault() == nullable2.GetValueOrDefault() & splitLineNbr2.HasValue == nullable2.HasValue)
              fssoDetSplit1.ParentSplitLineNbr = split.ParentSplitLineNbr;
            FSSODetSplit fssoDetSplit2 = fssoDetSplit1;
            Decimal? qty = fssoDetSplit2.Qty;
            Decimal? nullable3 = split.Qty;
            fssoDetSplit2.Qty = qty.HasValue & nullable3.HasValue ? new Decimal?(qty.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FSSODetSplit fssoDetSplit3 = fssoDetSplit1;
            nullable3 = fssoDetSplit3.BaseQty;
            Decimal? baseQty = split.BaseQty;
            fssoDetSplit3.BaseQty = nullable3.HasValue & baseQty.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
            FSSODetSplit fssoDetSplit4 = fssoDetSplit1;
            Decimal? nullable4 = fssoDetSplit4.OpenQty;
            nullable3 = split.OpenQty;
            fssoDetSplit4.OpenQty = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FSSODetSplit fssoDetSplit5 = fssoDetSplit1;
            nullable3 = fssoDetSplit5.BaseOpenQty;
            nullable4 = split.BaseOpenQty;
            fssoDetSplit5.BaseOpenQty = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            FSSODetSplit fssoDetSplit6 = fssoDetSplit1;
            nullable4 = fssoDetSplit6.ShippedQty;
            nullable3 = split.ShippedQty;
            fssoDetSplit6.ShippedQty = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            FSSODetSplit fssoDetSplit7 = fssoDetSplit1;
            nullable3 = fssoDetSplit7.BaseShippedQty;
            nullable4 = split.BaseShippedQty;
            fssoDetSplit7.BaseShippedQty = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            nullable1 = split.PlanID;
            if (nullable1.HasValue)
              this.DeletePlan(cach, split);
            ((PXSelectBase<FSSODetSplit>) graph.Splits).Delete(split);
          }
        }
        FSSODetSplit fssoDetSplit8 = ((PXSelectBase<FSSODetSplit>) graph.Splits).Update(fssoDetSplit1);
        nullable1 = fssoDetSplit8.PlanID;
        if (nullable1.HasValue)
        {
          long? planId1 = fssoDetSplit8.PlanID;
          this.DeletePlan(cach, fssoDetSplit8);
          this.CreateAndAssignPlan(((PXSelectBase) graph.Splits).Cache, fssoDetSplit8);
          long? planId2 = fssoDetSplit8.PlanID;
          nullable1 = planId2;
          long? nullable5 = planId1;
          if (nullable1.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable1.HasValue == nullable5.HasValue)
            throw new PXInvalidOperationException();
          long? planId3 = ((PXSelectBase<FSSODetSplit>) graph.Splits).Update(fssoDetSplit8).PlanID;
          nullable1 = planId2;
          if (!(planId3.GetValueOrDefault() == nullable1.GetValueOrDefault() & planId3.HasValue == nullable1.HasValue))
            throw new PXInvalidOperationException();
        }
      }
    }
  }

  public virtual void CreateAndAssignPlan(PXCache splitCache, FSSODetSplit openSplit)
  {
    if (openSplit.PlanID.HasValue)
      throw new PXInvalidOperationException();
    INItemPlan inItemPlan1 = splitCache.Graph.FindImplementation<FSSODetSplitPlan>().DefaultValues(openSplit);
    if (inItemPlan1 == null)
      return;
    INItemPlan inItemPlan2 = (INItemPlan) splitCache.Graph.Caches[typeof (INItemPlan)].Insert((object) inItemPlan1);
    openSplit.PlanID = inItemPlan2.PlanID;
  }

  public virtual void DeletePlan(PXCache planCache, FSSODetSplit split)
  {
    planCache.Delete((object) (PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select(planCache.Graph, new object[1]
    {
      (object) split.PlanID
    }) ?? throw new PXException("The {0} record was not found.", new object[1]
    {
      (object) DACHelper.GetDisplayName(typeof (INItemPlan))
    })));
    split.PlanID = new long?();
  }

  public class SplitKey : IEquatable<FSAllocationProcess.SplitKey>
  {
    public DateTime? ShipDate;
    public DateTime? ExpireDate;
    public bool? IsAllocated;
    public bool? Completed;
    public int? InventoryID;
    public string UOM;
    public int? SiteID;
    public int? LocationID;
    public int? CostCenterID;
    public string LotSerialNbr;
    public Guid? RefNoteID;
    public string POReceiptType;
    public string POReceiptNbr;
    public FSAllocationProcess.SplitKey.PlanStates PlanState;
    protected StringBuilder KeyBuilder;
    protected string Key;

    public SplitKey(FSSODetSplit split)
    {
      this.KeyBuilder = new StringBuilder();
      this.ShipDate = split.ShipDate;
      this.AddKeyPart((object) this.ShipDate);
      this.ExpireDate = split.ExpireDate;
      this.AddKeyPart((object) this.ExpireDate);
      this.IsAllocated = split.IsAllocated;
      this.AddKeyPart((object) this.IsAllocated);
      this.Completed = split.Completed;
      this.AddKeyPart((object) this.Completed);
      this.InventoryID = split.InventoryID;
      this.AddKeyPart((object) this.InventoryID);
      this.UOM = split.UOM;
      this.AddKeyPart((object) this.UOM);
      this.SiteID = split.SiteID;
      this.AddKeyPart((object) this.SiteID);
      this.LocationID = split.LocationID;
      this.AddKeyPart((object) this.LocationID);
      this.CostCenterID = split.CostCenterID;
      this.AddKeyPart((object) this.CostCenterID);
      this.LotSerialNbr = string.IsNullOrEmpty(split.LotSerialNbr) ? (string) null : split.LotSerialNbr;
      this.AddKeyPart((object) this.LotSerialNbr);
      this.RefNoteID = split.RefNoteID;
      this.AddKeyPart((object) this.RefNoteID);
      this.POReceiptType = split.POReceiptType;
      this.AddKeyPart((object) this.POReceiptType);
      this.POReceiptNbr = split.POReceiptNbr;
      this.AddKeyPart((object) this.POReceiptNbr);
      this.PlanState = split.PlanID.HasValue ? FSAllocationProcess.SplitKey.PlanStates.Created : FSAllocationProcess.SplitKey.PlanStates.Null;
      this.AddKeyPart((object) this.PlanState);
      this.Key = this.KeyBuilder.ToString();
    }

    public bool Equals(FSAllocationProcess.SplitKey other)
    {
      return other != null && (this == other || this.GetHashCode() == other.GetHashCode());
    }

    public override int GetHashCode() => this.Key.GetHashCode();

    protected virtual void AddKeyPart(object part)
    {
      if (part != null)
        this.KeyBuilder.Append(part.ToString());
      this.KeyBuilder.Append("|");
    }

    public enum PlanStates
    {
      Null,
      Created,
    }
  }
}
