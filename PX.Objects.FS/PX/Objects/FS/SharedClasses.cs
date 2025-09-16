// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SharedClasses
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.FS.DAC;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.FS;

public class SharedClasses
{
  public class TransactionScopeException : Exception
  {
  }

  public class CpnyLocationDistance
  {
    public 
    #nullable disable
    FSBranchLocation fsBranchLocation;
    public string address;
    public int distance;

    public CpnyLocationDistance(FSBranchLocation fsBranchLocation, string address, int distance)
    {
      this.fsBranchLocation = fsBranchLocation;
      this.address = address;
      this.distance = distance;
    }
  }

  public class RouteSelected_view : 
    PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>, Where<FSRouteDocument.routeDocumentID, Equal<Current<FSRouteDocument.routeDocumentID>>>>
  {
    public RouteSelected_view(PXGraph graph)
      : base(graph)
    {
    }

    public RouteSelected_view(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class InvoiceGroup
  {
    public int Pivot;
    public string BatchNbr;
    public string DocumentType;
    public string DocumentRefNbr;
    public bool? PostToAppNegBalances;
    public int? BillingCycle;
    public Decimal? TotalSumAR_AP;
    public Decimal? TotalSumSO;
    public List<SharedClasses.InvoiceItem> ItemsToPost;

    public InvoiceGroup(
      int pivot,
      bool? postToAppNegBalances,
      List<SharedClasses.InvoiceItem> invoiceItemList)
    {
      this.BatchNbr = (string) null;
      this.DocumentType = (string) null;
      this.DocumentRefNbr = (string) null;
      this.Pivot = pivot;
      this.ItemsToPost = invoiceItemList;
      this.PostToAppNegBalances = postToAppNegBalances;
      this.BillingCycle = invoiceItemList[0] == null || invoiceItemList[0].FSAppointmentRow == null ? new int?() : invoiceItemList[0].FSAppointmentRow.BillingCycleID;
      if (!this.BillingCycle.HasValue)
        this.BillingCycle = invoiceItemList[0] == null || invoiceItemList[0].FSServiceOrderRow == null ? new int?() : invoiceItemList[0].FSServiceOrderRow.BillingCycleID;
      this.TotalSumAR_AP = invoiceItemList.Sum<SharedClasses.InvoiceItem>((Func<SharedClasses.InvoiceItem, Decimal?>) (x => x.FSPostingLineDetailsToPostInAR_AP.Sum<SharedClasses.PostingLine>((Func<SharedClasses.PostingLine, Decimal?>) (y => y.CuryTranAmt))));
      this.TotalSumSO = invoiceItemList.Sum<SharedClasses.InvoiceItem>((Func<SharedClasses.InvoiceItem, Decimal?>) (x => x.FSPostingLineDetailsToPostInSO.Sum<SharedClasses.PostingLine>((Func<SharedClasses.PostingLine, Decimal?>) (y => y.CuryTranAmt))));
    }
  }

  public class AppointmentInfo
  {
    public AppointmentToPost FSAppointmentRow;
    public ServiceOrderToPost FSServiceOrderRow;
    public FSSrvOrdType FSSrvOrdTypeRow;
    public PXResultset<FSAppointmentDet> FSAppointmentDetToPostInAR_AP;
    public PXResultset<FSAppointmentDet> FSAppointmentDetToPostInSO;
    public PXResultset<FSAppointmentDet> FSAppointmentInventoryItemToPostIn_AR_AP;
    public PXResultset<FSAppointmentDet> FSAppointmentInventoryItemToPostInSO;

    public AppointmentInfo()
    {
    }

    public AppointmentInfo(
      AppointmentToPost fsAppointmentRow,
      ServiceOrderToPost fsServiceOrderRow,
      FSSrvOrdType fsSrvOrdTypeRow,
      PXResultset<FSAppointmentDet> bqlResultSet_FSAppointmentDet_PostAR_AP,
      PXResultset<FSAppointmentDet> bqlResultSet_FSAppointmentDet_PostSO,
      PXResultset<FSAppointmentDet> bqlResultSet_FSAppointmentInventoryItem_PostAR_AP,
      PXResultset<FSAppointmentDet> bqlResultSet_FSAppointmentInventoryItem_PostSO)
    {
      this.FSAppointmentRow = fsAppointmentRow;
      this.FSServiceOrderRow = fsServiceOrderRow;
      this.FSSrvOrdTypeRow = fsSrvOrdTypeRow;
      this.FSAppointmentDetToPostInAR_AP = bqlResultSet_FSAppointmentDet_PostAR_AP;
      this.FSAppointmentDetToPostInSO = bqlResultSet_FSAppointmentDet_PostSO;
      this.FSAppointmentInventoryItemToPostIn_AR_AP = bqlResultSet_FSAppointmentInventoryItem_PostAR_AP;
      this.FSAppointmentInventoryItemToPostInSO = bqlResultSet_FSAppointmentInventoryItem_PostSO;
    }
  }

  public class ServiceOrderInfo
  {
    public ServiceOrderToPost FSServiceOrderRow;
    public FSSrvOrdType FSSrvOrdTypeRow;
    public PXResultset<FSSODet> FSSODetToPostInAR_AP;
    public PXResultset<FSSODet> FSSODetToPostInSO;

    public ServiceOrderInfo()
    {
    }

    public ServiceOrderInfo(
      ServiceOrderToPost fsServiceOrderRow,
      FSSrvOrdType fsSrvOrdTypeRow,
      PXResultset<FSSODet> bqlResultSet_FSSODet_PostAR_AP,
      PXResultset<FSSODet> bqlResultSet_FSSODet_PostSO)
    {
      this.FSServiceOrderRow = fsServiceOrderRow;
      this.FSSrvOrdTypeRow = fsSrvOrdTypeRow;
      this.FSSODetToPostInAR_AP = bqlResultSet_FSSODet_PostAR_AP;
      this.FSSODetToPostInSO = bqlResultSet_FSSODet_PostSO;
    }
  }

  public class PostingLine
  {
    public string TableSource;
    public string LineType;
    public int? LineID;
    public int? BranchID;
    public int? InventoryID;
    public string UOM;
    public int? AcctID;
    public int? SubID;
    public int? SiteID;
    public int? LocationID;
    public int? SubItemID;
    public string TranDesc;
    public int? ProjectID;
    public int? ProjectTaskID;
    public Decimal? CuryUnitPrice;
    public Decimal? Qty;
    public Decimal? CuryTranAmt;
    public int? PostID;
    public bool? IsFree;
  }

  public class GroupTransition
  {
    public int Index;
    public AppointmentToPost FSAppointmentRow;
    public ServiceOrderToPost FSServiceOrderRow;

    public GroupTransition(int index, AppointmentToPost fSAppointmentRow)
    {
      this.Index = index;
      this.FSAppointmentRow = fSAppointmentRow;
    }

    public GroupTransition(int index, ServiceOrderToPost fsServiceOrderRow)
    {
      this.Index = index;
      this.FSServiceOrderRow = fsServiceOrderRow;
    }
  }

  public class InvoiceItem
  {
    public AppointmentToPost FSAppointmentRow;
    public ServiceOrderToPost FSServiceOrderRow;
    public FSSrvOrdType FSSrvOrdTypeRow;
    public List<SharedClasses.PostingLine> FSPostingLineDetailsToPostInAR_AP;
    public List<SharedClasses.PostingLine> FSPostingLineDetailsToPostInSO;
    public int Index;
    public int? CustomerID;
    public int? SOID;
    public int? AppointmentID;
    public bool? PostToAPNegBalances;
    public int? CustomerLocationID;
    public string CustPORefNbr;
    public string CustWorkOrderRefNbr;

    private int? GetAcct(
      PXGraph graph,
      IFSSODetBase fsSODetBase,
      FSAppointmentDet fsAppointmentInventoryItem,
      FSServiceOrder fsServiceOrderRow,
      FSSrvOrdType fsSrvOrdTypeRow)
    {
      int? acct = new int?();
      if (fsSODetBase != null)
        acct = !fsSODetBase.AcctID.HasValue ? ServiceOrderEntry.Get_TranAcctID_DefaultValueInt(graph, fsSrvOrdTypeRow.SalesAcctSource, fsSODetBase.InventoryID, fsSODetBase.SiteID, fsServiceOrderRow) : fsSODetBase.AcctID;
      else if (fsAppointmentInventoryItem != null)
        acct = ServiceOrderEntry.Get_TranAcctID_DefaultValueInt(graph, fsSrvOrdTypeRow.SalesAcctSource, fsAppointmentInventoryItem.InventoryID, fsAppointmentInventoryItem.SiteID, fsServiceOrderRow);
      return acct;
    }

    /// <summary>
    /// Adds the line <c>fsAppointmentDetRow</c> or <c>fsAppointmentInventoryItem</c> to the List <c>fsAppointmentDetailsToPostInAR_AP</c> or <c>fsAppointmentDetailsToPostInSO</c> depending on <c>addToArApList</c> and <c>addToSoList</c> flags.
    /// </summary>
    private void AddAppointmentLineToList(
      PXGraph graph,
      FSAppointmentDet fsAppointmentDetRow,
      FSAppointmentDet fsAppointmentInventoryItem,
      SharedClasses.AppointmentInfo appointmentInfo,
      bool addToArApList,
      bool addToSoList)
    {
      SharedClasses.PostingLine postingLine = new SharedClasses.PostingLine();
      if (fsAppointmentDetRow != null)
      {
        int? inventoryId = fsAppointmentDetRow.InventoryID;
        postingLine.TableSource = "FSAppointmentDet";
        postingLine.LineType = fsAppointmentDetRow.LineType;
        postingLine.LineID = fsAppointmentDetRow.AppDetID;
        postingLine.BranchID = appointmentInfo.FSServiceOrderRow.BranchID;
        postingLine.InventoryID = inventoryId;
        postingLine.UOM = fsAppointmentDetRow.UOM;
        postingLine.AcctID = this.GetAcct(graph, (IFSSODetBase) fsAppointmentDetRow, (FSAppointmentDet) null, (FSServiceOrder) appointmentInfo.FSServiceOrderRow, appointmentInfo.FSSrvOrdTypeRow);
        postingLine.SubID = fsAppointmentDetRow.SubID;
        postingLine.SiteID = fsAppointmentDetRow.SiteID;
        postingLine.LocationID = fsAppointmentDetRow.SiteLocationID;
        postingLine.SubItemID = fsAppointmentDetRow.SubItemID;
        postingLine.TranDesc = fsAppointmentDetRow.TranDesc;
        postingLine.ProjectID = fsAppointmentDetRow.ProjectID;
        postingLine.ProjectTaskID = fsAppointmentDetRow.ProjectTaskID;
        postingLine.CuryUnitPrice = fsAppointmentDetRow.CuryUnitPrice;
        postingLine.Qty = fsAppointmentDetRow.ActualQty;
        postingLine.CuryTranAmt = fsAppointmentDetRow.TranAmt;
        postingLine.PostID = fsAppointmentDetRow.PostID;
        postingLine.IsFree = fsAppointmentDetRow.IsFree;
      }
      else if (fsAppointmentInventoryItem != null)
      {
        postingLine.TableSource = "FSAppointmentDet";
        postingLine.LineType = fsAppointmentInventoryItem.LineType;
        postingLine.LineID = fsAppointmentInventoryItem.AppDetID;
        postingLine.BranchID = appointmentInfo.FSServiceOrderRow.BranchID;
        postingLine.InventoryID = fsAppointmentInventoryItem.InventoryID;
        postingLine.UOM = fsAppointmentInventoryItem.UOM;
        postingLine.AcctID = this.GetAcct(graph, (IFSSODetBase) null, fsAppointmentInventoryItem, (FSServiceOrder) appointmentInfo.FSServiceOrderRow, appointmentInfo.FSSrvOrdTypeRow);
        postingLine.SubID = new int?();
        postingLine.SiteID = fsAppointmentInventoryItem.SiteID;
        postingLine.SubItemID = fsAppointmentInventoryItem.SubItemID;
        postingLine.TranDesc = fsAppointmentInventoryItem.TranDesc;
        postingLine.ProjectID = fsAppointmentInventoryItem.ProjectID;
        postingLine.ProjectTaskID = fsAppointmentInventoryItem.ProjectTaskID;
        postingLine.CuryUnitPrice = fsAppointmentInventoryItem.CuryUnitPrice;
        postingLine.Qty = fsAppointmentInventoryItem.ActualQty;
        postingLine.CuryTranAmt = fsAppointmentInventoryItem.TranAmt;
        postingLine.PostID = fsAppointmentInventoryItem.PostID;
        postingLine.IsFree = new bool?(false);
      }
      if (addToArApList)
      {
        this.FSPostingLineDetailsToPostInAR_AP.Add(postingLine);
      }
      else
      {
        if (!addToSoList)
          return;
        this.FSPostingLineDetailsToPostInSO.Add(postingLine);
      }
    }

    /// <summary>
    /// Adds the line <c>fsSODetRow</c> to the List <c>FSPostingLineDetailsToPostInAR_AP</c> or <c>FSPostingLineDetailsToPostInSO</c> depending on <c>addToArApList</c> and <c>addToSoList</c> flags.
    /// </summary>
    private void AddSOLineToList(
      PXGraph graph,
      FSSODet fsSODetRow,
      SharedClasses.ServiceOrderInfo serviceOrderInfo,
      bool addToArApList,
      bool addToSoList)
    {
      SharedClasses.PostingLine postingLine = new SharedClasses.PostingLine();
      if (fsSODetRow != null)
      {
        int? inventoryId = fsSODetRow.InventoryID;
        postingLine.TableSource = "FSSODet";
        postingLine.LineType = fsSODetRow.LineType;
        postingLine.LineID = fsSODetRow.SODetID;
        postingLine.BranchID = serviceOrderInfo.FSServiceOrderRow.BranchID;
        postingLine.InventoryID = inventoryId;
        postingLine.UOM = fsSODetRow.UOM;
        postingLine.AcctID = this.GetAcct(graph, (IFSSODetBase) fsSODetRow, (FSAppointmentDet) null, (FSServiceOrder) serviceOrderInfo.FSServiceOrderRow, serviceOrderInfo.FSSrvOrdTypeRow);
        postingLine.SubID = fsSODetRow.SubID;
        postingLine.SiteID = fsSODetRow.SiteID;
        postingLine.LocationID = fsSODetRow.SiteLocationID;
        postingLine.SubItemID = fsSODetRow.SubItemID;
        postingLine.TranDesc = fsSODetRow.TranDesc;
        postingLine.ProjectID = fsSODetRow.ProjectID;
        postingLine.ProjectTaskID = fsSODetRow.ProjectTaskID;
        postingLine.CuryUnitPrice = fsSODetRow.CuryUnitPrice;
        postingLine.Qty = fsSODetRow.EstimatedQty;
        postingLine.CuryTranAmt = fsSODetRow.CuryEstimatedTranAmt;
        postingLine.PostID = fsSODetRow.PostID;
        postingLine.IsFree = fsSODetRow.IsFree;
      }
      if (addToArApList)
      {
        this.FSPostingLineDetailsToPostInAR_AP.Add(postingLine);
      }
      else
      {
        if (!addToSoList)
          return;
        this.FSPostingLineDetailsToPostInSO.Add(postingLine);
      }
    }

    public InvoiceItem(PXGraph graph, SharedClasses.AppointmentInfo appointmentInfo, int index)
    {
      this.FSAppointmentRow = appointmentInfo.FSAppointmentRow;
      this.FSServiceOrderRow = appointmentInfo.FSServiceOrderRow;
      this.FSSrvOrdTypeRow = appointmentInfo.FSSrvOrdTypeRow;
      this.Index = index;
      this.CustomerID = appointmentInfo.FSServiceOrderRow.BillCustomerID;
      this.SOID = appointmentInfo.FSServiceOrderRow.SOID;
      this.AppointmentID = appointmentInfo.FSAppointmentRow.AppointmentID;
      this.CustomerLocationID = appointmentInfo.FSServiceOrderRow.BillLocationID;
      this.CustPORefNbr = appointmentInfo.FSServiceOrderRow.CustPORefNbr;
      this.CustWorkOrderRefNbr = appointmentInfo.FSServiceOrderRow.CustWorkOrderRefNbr;
      this.PostToAPNegBalances = appointmentInfo.FSSrvOrdTypeRow.PostNegBalanceToAP;
      this.FSPostingLineDetailsToPostInAR_AP = new List<SharedClasses.PostingLine>();
      this.FSPostingLineDetailsToPostInSO = new List<SharedClasses.PostingLine>();
      foreach (PXResult<FSAppointmentDet> pxResult in appointmentInfo.FSAppointmentDetToPostInAR_AP)
      {
        FSAppointmentDet fsAppointmentDetRow = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
        this.AddAppointmentLineToList(graph, fsAppointmentDetRow, (FSAppointmentDet) null, appointmentInfo, true, false);
      }
      foreach (PXResult<FSAppointmentDet> pxResult in appointmentInfo.FSAppointmentDetToPostInSO)
      {
        FSAppointmentDet fsAppointmentDetRow = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
        this.AddAppointmentLineToList(graph, fsAppointmentDetRow, (FSAppointmentDet) null, appointmentInfo, false, true);
      }
      foreach (PXResult<FSAppointmentDet> pxResult in appointmentInfo.FSAppointmentInventoryItemToPostIn_AR_AP)
      {
        FSAppointmentDet fsAppointmentInventoryItem = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
        this.AddAppointmentLineToList(graph, (FSAppointmentDet) null, fsAppointmentInventoryItem, appointmentInfo, true, false);
      }
      foreach (PXResult<FSAppointmentDet> pxResult in appointmentInfo.FSAppointmentInventoryItemToPostInSO)
      {
        FSAppointmentDet fsAppointmentInventoryItem = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
        this.AddAppointmentLineToList(graph, (FSAppointmentDet) null, fsAppointmentInventoryItem, appointmentInfo, false, true);
      }
    }

    public InvoiceItem(PXGraph graph, SharedClasses.ServiceOrderInfo serviceOrderInfo, int index)
    {
      this.FSServiceOrderRow = serviceOrderInfo.FSServiceOrderRow;
      this.FSSrvOrdTypeRow = serviceOrderInfo.FSSrvOrdTypeRow;
      this.Index = index;
      this.CustomerID = serviceOrderInfo.FSServiceOrderRow.BillCustomerID;
      this.SOID = serviceOrderInfo.FSServiceOrderRow.SOID;
      this.CustomerLocationID = serviceOrderInfo.FSServiceOrderRow.BillLocationID;
      this.CustPORefNbr = serviceOrderInfo.FSServiceOrderRow.CustPORefNbr;
      this.CustWorkOrderRefNbr = serviceOrderInfo.FSServiceOrderRow.CustWorkOrderRefNbr;
      this.PostToAPNegBalances = serviceOrderInfo.FSSrvOrdTypeRow.PostNegBalanceToAP;
      this.FSPostingLineDetailsToPostInAR_AP = new List<SharedClasses.PostingLine>();
      this.FSPostingLineDetailsToPostInSO = new List<SharedClasses.PostingLine>();
      foreach (PXResult<FSSODet> pxResult in serviceOrderInfo.FSSODetToPostInAR_AP)
      {
        FSSODet fsSODetRow = PXResult<FSSODet>.op_Implicit(pxResult);
        this.AddSOLineToList(graph, fsSODetRow, serviceOrderInfo, true, false);
      }
      foreach (PXResult<FSSODet> pxResult in serviceOrderInfo.FSSODetToPostInSO)
      {
        FSSODet fsSODetRow = PXResult<FSSODet>.op_Implicit(pxResult);
        this.AddSOLineToList(graph, fsSODetRow, serviceOrderInfo, false, true);
      }
    }
  }

  public class AppointmentInventoryItemInfo
  {
    public FSAppointment FSAppointmentRow;
    public FSServiceOrder FSServiceOrderRow;
    public FSSrvOrdType FSSrvOrdTypeRow;
    public FSAppointmentDet FSAppointmentDet;
    public FSAppointmentDet FSAppointmentInventoryItem;
    public int Index;
    public string ServiceType;
    public int? AppointmentID;

    public AppointmentInventoryItemInfo()
    {
    }

    public AppointmentInventoryItemInfo(
      FSAppointment fsAppointmentRow,
      FSServiceOrder fsServiceOrderRow,
      FSSrvOrdType fsSrvOrdTypeRow,
      FSAppointmentDet fsAppointmentDet,
      FSAppointmentDet fsAppointmentInventoryItemRow,
      int index)
    {
      this.FSAppointmentRow = fsAppointmentRow;
      this.FSServiceOrderRow = fsServiceOrderRow;
      this.FSSrvOrdTypeRow = fsSrvOrdTypeRow;
      this.FSAppointmentDet = fsAppointmentDet;
      this.FSAppointmentInventoryItem = fsAppointmentInventoryItemRow;
      this.AppointmentID = fsAppointmentRow.AppointmentID;
      this.ServiceType = fsAppointmentDet.ServiceType;
      this.Index = index;
    }
  }

  public class AppointmentInventoryItemGroup
  {
    public int Pivot;
    public string ServiceType;
    public string BatchNbr;
    public string DocumentType;
    public string DocumentRefNbr;
    public List<SharedClasses.AppointmentInventoryItemInfo> AppointmentInventoryItems;

    public AppointmentInventoryItemGroup(
      int pivot,
      string serviceType,
      List<SharedClasses.AppointmentInventoryItemInfo> appointmentInventoryItemList)
    {
      this.BatchNbr = (string) null;
      this.DocumentType = (string) null;
      this.DocumentRefNbr = (string) null;
      this.Pivot = pivot;
      this.ServiceType = serviceType;
      this.AppointmentInventoryItems = appointmentInventoryItemList;
    }
  }

  public class ItemList
  {
    public int? itemID;
    public List<object> list;

    public ItemList()
    {
    }

    public ItemList(int? itemID)
    {
      this.itemID = itemID;
      this.list = new List<object>();
    }
  }

  /// <summary>
  /// This class allows the use of a cero decimal in a BQL type declaration.
  /// </summary>
  public class decimal_0 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Constant<
  #nullable disable
  SharedClasses.decimal_0>
  {
    public decimal_0()
      : base(0M)
    {
    }
  }

  public class decimal_1 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Constant<
  #nullable disable
  SharedClasses.decimal_1>
  {
    public decimal_1()
      : base(1M)
    {
    }
  }

  public class decimal_100 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Constant<
  #nullable disable
  SharedClasses.decimal_100>
  {
    public decimal_100()
      : base(100M)
    {
    }
  }

  public class decimal_60 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Constant<
  #nullable disable
  SharedClasses.decimal_60>
  {
    public decimal_60()
      : base(60M)
    {
    }
  }

  public class int_0 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  SharedClasses.int_0>
  {
    public int_0()
      : base(0)
    {
    }
  }

  public class int_1 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  SharedClasses.int_1>
  {
    public int_1()
      : base(1)
    {
    }
  }

  public class SOARLineEquipmentComponent
  {
    public string equipmentAction;
    public int? componentID;
    public int? currentLineRef;
    public string sourceLineRef;
    public string sourceNewTargetEquipmentLineNbr;
    public FSxSOLine fsxSOLineRow;
    public FSxARTran fsxARTranRow;
    public ARTran arTranRow;

    public SOARLineEquipmentComponent(IDocLine docLine, SOLine sOLineRow, FSxSOLine fSxSOLineRow)
    {
      this.componentID = docLine.ComponentID;
      this.currentLineRef = sOLineRow.LineNbr;
      this.equipmentAction = docLine.EquipmentAction;
      this.sourceLineRef = docLine.LineRef;
      this.sourceNewTargetEquipmentLineNbr = docLine.NewTargetEquipmentLineNbr;
      this.fsxSOLineRow = fSxSOLineRow;
      this.fsxARTranRow = (FSxARTran) null;
    }

    public SOARLineEquipmentComponent(IDocLine docLine, ARTran arTranRow, FSxARTran fsxARTranRow)
    {
      this.arTranRow = arTranRow;
      this.componentID = docLine.ComponentID;
      this.currentLineRef = arTranRow.LineNbr;
      this.equipmentAction = docLine.EquipmentAction;
      this.sourceLineRef = docLine.LineRef;
      this.sourceNewTargetEquipmentLineNbr = docLine.NewTargetEquipmentLineNbr;
      this.fsxARTranRow = fsxARTranRow;
      this.fsxSOLineRow = (FSxSOLine) null;
    }

    public SOARLineEquipmentComponent(
      ContractInvoiceLine docLine,
      SOLine sOLineRow,
      FSxSOLine fSxSOLineRow)
    {
      this.componentID = docLine.ComponentID;
      this.currentLineRef = sOLineRow.LineNbr;
      this.equipmentAction = docLine.EquipmentAction;
      this.sourceLineRef = docLine.LineRef;
      this.sourceNewTargetEquipmentLineNbr = docLine.NewTargetEquipmentLineNbr;
      this.fsxSOLineRow = fSxSOLineRow;
      this.fsxARTranRow = (FSxARTran) null;
    }
  }

  public class SOPrepaymentBySO
  {
    public string SrvOrdType;
    public string ServiceOrderRefNbr;
    public Decimal? originalAmount;
    public Decimal? unpaidAmount;
    public List<FSxSOLine> fsxSOLineList;
    public List<FSxARTran> fsxARTranList;

    public SOPrepaymentBySO(SOLine soLineRow, FSxSOLine fsxSOLineRow, Decimal soTaxLine)
    {
      this.SrvOrdType = fsxSOLineRow.SrvOrdType;
      this.ServiceOrderRefNbr = fsxSOLineRow.ServiceOrderRefNbr;
      this.unpaidAmount = new Decimal?(0M);
      Decimal? unpaidAmount = this.unpaidAmount;
      Decimal num = soLineRow.CuryLineAmt.GetValueOrDefault() + soTaxLine;
      this.unpaidAmount = unpaidAmount.HasValue ? new Decimal?(unpaidAmount.GetValueOrDefault() + num) : new Decimal?();
      this.originalAmount = this.unpaidAmount;
      this.fsxSOLineList = new List<FSxSOLine>();
      this.fsxSOLineList.Add(fsxSOLineRow);
    }

    public SOPrepaymentBySO(ARTran arTranRow, FSxARTran fsxARTran, Decimal arTaxLine)
    {
      this.SrvOrdType = fsxARTran.SrvOrdType;
      this.ServiceOrderRefNbr = fsxARTran.ServiceOrderRefNbr;
      this.unpaidAmount = new Decimal?(0M);
      Decimal? unpaidAmount = this.unpaidAmount;
      Decimal num = arTranRow.CuryTranAmt.GetValueOrDefault() + arTaxLine;
      this.unpaidAmount = unpaidAmount.HasValue ? new Decimal?(unpaidAmount.GetValueOrDefault() + num) : new Decimal?();
      this.originalAmount = this.unpaidAmount;
      this.fsxARTranList = new List<FSxARTran>();
      this.fsxARTranList.Add(fsxARTran);
    }

    public PXResultset<ARPayment> GetPrepaymentBySO(PXGraph graph)
    {
      if (string.IsNullOrEmpty(this.ServiceOrderRefNbr))
        return (PXResultset<ARPayment>) null;
      return PXSelectBase<ARPayment, PXSelectReadonly2<ARPayment, InnerJoin<FSAdjust, On<ARPayment.docType, Equal<FSAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<FSAdjust.adjgRefNbr>>>, InnerJoin<FSServiceOrder, On<FSAdjust.adjdOrderType, Equal<FSServiceOrder.srvOrdType>, And<FSAdjust.adjdOrderNbr, Equal<FSServiceOrder.refNbr>>>>>, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select(graph, new object[2]
      {
        (object) this.SrvOrdType,
        (object) this.ServiceOrderRefNbr
      });
    }
  }

  public class SOPrepaymentHelper
  {
    public List<SharedClasses.SOPrepaymentBySO> SOPrepaymentList;

    public SOPrepaymentHelper()
    {
      this.SOPrepaymentList = new List<SharedClasses.SOPrepaymentBySO>();
    }

    public void Add(SOLine soLineRow, FSxSOLine fsxSOLineRow, Decimal soTaxLine)
    {
      if (fsxSOLineRow.ServiceOrderRefNbr == null)
        return;
      SharedClasses.SOPrepaymentBySO soPrepaymentBySo1 = this.SOPrepaymentList.Find((Predicate<SharedClasses.SOPrepaymentBySO>) (x => x.SrvOrdType.Equals(fsxSOLineRow.SrvOrdType) && x.ServiceOrderRefNbr.Equals(fsxSOLineRow.ServiceOrderRefNbr)));
      if (soPrepaymentBySo1 != null)
      {
        SharedClasses.SOPrepaymentBySO soPrepaymentBySo2 = soPrepaymentBySo1;
        Decimal? unpaidAmount = soPrepaymentBySo2.unpaidAmount;
        Decimal num = soLineRow.CuryLineAmt.GetValueOrDefault() + soTaxLine;
        soPrepaymentBySo2.unpaidAmount = unpaidAmount.HasValue ? new Decimal?(unpaidAmount.GetValueOrDefault() + num) : new Decimal?();
        soPrepaymentBySo1.fsxSOLineList.Add(fsxSOLineRow);
      }
      else
        this.SOPrepaymentList.Add(new SharedClasses.SOPrepaymentBySO(soLineRow, fsxSOLineRow, soTaxLine));
    }

    public void Add(ARTran arTranRow, FSxARTran fsxARTranRow, Decimal arTaxLine)
    {
      if (fsxARTranRow.ServiceOrderRefNbr == null)
        return;
      SharedClasses.SOPrepaymentBySO soPrepaymentBySo1 = this.SOPrepaymentList.Find((Predicate<SharedClasses.SOPrepaymentBySO>) (x => x.SrvOrdType.Equals(fsxARTranRow.SrvOrdType) && x.ServiceOrderRefNbr.Equals(fsxARTranRow.ServiceOrderRefNbr)));
      if (soPrepaymentBySo1 != null)
      {
        SharedClasses.SOPrepaymentBySO soPrepaymentBySo2 = soPrepaymentBySo1;
        Decimal? unpaidAmount = soPrepaymentBySo2.unpaidAmount;
        Decimal num = arTranRow.CuryTranAmt.GetValueOrDefault() + arTaxLine;
        soPrepaymentBySo2.unpaidAmount = unpaidAmount.HasValue ? new Decimal?(unpaidAmount.GetValueOrDefault() + num) : new Decimal?();
        soPrepaymentBySo1.fsxARTranList.Add(fsxARTranRow);
      }
      else
        this.SOPrepaymentList.Add(new SharedClasses.SOPrepaymentBySO(arTranRow, fsxARTranRow, arTaxLine));
    }
  }

  public class SubAccountIDTupla
  {
    public int? branchLocation_SubID;
    public int? branch_SubID;
    public int? inventoryItem_SubID;
    public int? customerLocation_SubID;
    public int? postingClass_SubID;
    public int? salesPerson_SubID;
    public int? srvOrdType_SubID;
    public int? warehouse_SubID;

    public SubAccountIDTupla(
      int? branchLocation_SubID,
      int? company_SubID,
      int? item_SubID,
      int? customer_SubID,
      int? postingClass_SubID,
      int? salesPerson_SubID,
      int? srvOrdType_SubID,
      int? warehouse_SubID)
    {
      this.branchLocation_SubID = branchLocation_SubID;
      this.branch_SubID = company_SubID;
      this.inventoryItem_SubID = item_SubID;
      this.customerLocation_SubID = customer_SubID;
      this.postingClass_SubID = postingClass_SubID;
      this.salesPerson_SubID = salesPerson_SubID;
      this.srvOrdType_SubID = srvOrdType_SubID;
      this.warehouse_SubID = warehouse_SubID;
    }
  }
}
