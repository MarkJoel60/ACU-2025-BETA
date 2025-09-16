// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreatePurchaseOrderProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.PO;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class CreatePurchaseOrderProcess : PXGraph<CreatePurchaseOrderProcess>
{
  public PXFilter<CreatePOFilter> Filter;
  public PXCancel<CreatePOFilter> Cancel;
  public PXAction<CreatePOFilter> viewDocument;
  public PXSetup<POSetup> poSetup;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (CreatePOFilter))]
  public PXFilteredProcessing<POEnabledFSSODet, CreatePOFilter, Where2<Where<CurrentValue<CreatePOFilter.inventoryID>, IsNull, Or<POEnabledFSSODet.inventoryID, Equal<CurrentValue<CreatePOFilter.inventoryID>>>>, And2<Where<CurrentValue<CreatePOFilter.itemClassID>, IsNull, Or<POEnabledFSSODet.inventoryItemClassID, Equal<CurrentValue<CreatePOFilter.itemClassID>>>>, And2<Where<CurrentValue<CreatePOFilter.siteID>, IsNull, Or<POEnabledFSSODet.siteID, Equal<CurrentValue<CreatePOFilter.siteID>>>>, And2<Where<CurrentValue<CreatePOFilter.poVendorID>, IsNull, Or<POEnabledFSSODet.poVendorID, Equal<CurrentValue<CreatePOFilter.poVendorID>>>>, And2<Where<CurrentValue<CreatePOFilter.customerID>, IsNull, Or<POEnabledFSSODet.srvCustomerID, Equal<CurrentValue<CreatePOFilter.customerID>>>>, And2<Where<CurrentValue<CreatePOFilter.srvOrdType>, IsNull, Or<POEnabledFSSODet.srvOrdType, Equal<CurrentValue<CreatePOFilter.srvOrdType>>>>, And2<Where<CurrentValue<CreatePOFilter.sORefNbr>, IsNull, Or<POEnabledFSSODet.refNbr, Equal<CurrentValue<CreatePOFilter.sORefNbr>>>>, And<POEnabledFSSODet.orderDate, LessEqual<CurrentValue<CreatePOFilter.upToDate>>>>>>>>>>, OrderBy<Asc<POEnabledFSSODet.poVendorID>>> LinesToPO;

  public CreatePurchaseOrderProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CreatePurchaseOrderProcess.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new CreatePurchaseOrderProcess.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.\u003C\u003E4__this = this;
    PXUIFieldAttribute.SetEnabled<POEnabledFSSODet.poVendorID>(((PXSelectBase) this.LinesToPO).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<FSSODet.poVendorLocationID>(((PXSelectBase) this.LinesToPO).Cache, (object) null, true);
    // ISSUE: method pointer
    ((PXProcessingBase<POEnabledFSSODet>) this.LinesToPO).SetProcessDelegate(new PXProcessingBase<POEnabledFSSODet>.ProcessListDelegate((object) cDisplayClass00, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    ((PXSelectBase) this.LinesToPO).Cache.IsDirty = false;
    POEnabledFSSODet current = ((PXSelectBase<POEnabledFSSODet>) this.LinesToPO).Current;
    if (current == null || current.SrvOrdType == null || current.RefNbr == null)
      return adapter.Get();
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) current.RefNbr, new object[1]
    {
      (object) current.SrvOrdType
    }));
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 2);
    return adapter.Get();
  }

  public virtual void processLines(
    List<POEnabledFSSODet> originalItemList,
    PXGraph<CreatePurchaseOrderProcess> graphCreatePurchaseOrderByServiceOrder,
    List<List<POEnabledFSSODet>> groupedFSSODetRows)
  {
    if (groupedFSSODetRows.Count == 0)
      return;
    PX.Objects.PO.POOrder poOrderRow = (PX.Objects.PO.POOrder) null;
    PX.Objects.PO.POLine poLineRow = (PX.Objects.PO.POLine) null;
    POEnabledFSSODet firstPOEnabledFSSODetRow = (POEnabledFSSODet) null;
    PXGraph pxGraph = new PXGraph();
    POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
    foreach (List<POEnabledFSSODet> groupedFssoDetRow in groupedFSSODetRows)
    {
      if (this.IsThisItemGroupValid(groupedFssoDetRow, originalItemList))
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          try
          {
            pxGraph.Clear((PXClearOption) 3);
            ((PXGraph) instance).Clear((PXClearOption) 3);
            this.CreatePOOrderDocument(instance, groupedFssoDetRow, poOrderRow, poLineRow, firstPOEnabledFSSODetRow);
            foreach (POEnabledFSSODet poEnabledFssoDet in groupedFssoDetRow)
            {
              PXUpdate<Set<FSSODet.poType, Required<FSSODet.poType>, Set<FSSODet.poNbr, Required<FSSODet.poNbr>, Set<FSSODet.poLineNbr, Required<FSSODet.poLineNbr>, Set<FSSODet.poStatus, Required<FSSODet.poStatus>, Set<FSSODet.poCompleted, Required<FSSODet.poCompleted>, Set<FSSODet.poVendorID, Required<FSSODet.poVendorID>, Set<FSSODet.poVendorLocationID, Required<FSSODet.poVendorLocationID>>>>>>>>, FSSODet, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>, And<FSSODet.poNbr, IsNull>>>.Update(pxGraph, new object[8]
              {
                (object) poEnabledFssoDet.POType,
                (object) ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current.OrderNbr,
                (object) poEnabledFssoDet.POLineNbr,
                (object) ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current.Status,
                (object) poEnabledFssoDet.POCompleted,
                (object) poEnabledFssoDet.POVendorID,
                (object) poEnabledFssoDet.POVendorLocationID,
                (object) poEnabledFssoDet.SODetID
              });
              PXProcessing<POEnabledFSSODet>.SetInfo(originalItemList.IndexOf(poEnabledFssoDet), "Record processed successfully.");
              poEnabledFssoDet.PONbrCreated = ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current.OrderNbr;
            }
            transactionScope.Complete();
          }
          catch (Exception ex)
          {
            transactionScope.Dispose();
            foreach (POEnabledFSSODet poEnabledFssoDet in groupedFssoDetRow)
              PXProcessing<POEnabledFSSODet>.SetError(originalItemList.IndexOf(poEnabledFssoDet), ex);
          }
        }
      }
    }
  }

  public virtual bool IsThisItemGroupValid(
    List<POEnabledFSSODet> itemGroup,
    List<POEnabledFSSODet> originalItemList)
  {
    if (itemGroup[0].POVendorID.HasValue && itemGroup[0].POVendorLocationID.HasValue)
      return true;
    foreach (POEnabledFSSODet poEnabledFssoDet in itemGroup)
      PXProcessing<POEnabledFSSODet>.SetWarning(originalItemList.IndexOf(poEnabledFssoDet), "A vendor and vendor location must be defined.");
    return false;
  }

  public virtual void CreatePOOrderDocument(
    POOrderEntry graphPOOrderEntry,
    List<POEnabledFSSODet> itemGroup,
    PX.Objects.PO.POOrder poOrderRow,
    PX.Objects.PO.POLine poLineRow,
    POEnabledFSSODet firstPOEnabledFSSODetRow)
  {
    firstPOEnabledFSSODetRow = itemGroup[0];
    this.InitializePOOrderDocument(graphPOOrderEntry, poOrderRow, firstPOEnabledFSSODetRow);
    foreach (POEnabledFSSODet poEnabledFSSODetRow in itemGroup)
      this.InsertPOLine(graphPOOrderEntry, poLineRow, poEnabledFSSODetRow);
    ((PXAction) graphPOOrderEntry.Save).Press();
  }

  public virtual void InitializePOOrderDocument(
    POOrderEntry graphPOOrderEntry,
    PX.Objects.PO.POOrder poOrderRow,
    POEnabledFSSODet firstPOEnabledFSSODetRow)
  {
    poOrderRow = new PX.Objects.PO.POOrder();
    poOrderRow = ((PXSelectBase<PX.Objects.PO.POOrder>) graphPOOrderEntry.Document).Current = ((PXSelectBase<PX.Objects.PO.POOrder>) graphPOOrderEntry.Document).Insert(poOrderRow);
    poOrderRow.OrderType = "RO";
    poOrderRow.VendorID = firstPOEnabledFSSODetRow.POVendorID;
    poOrderRow.VendorLocationID = firstPOEnabledFSSODetRow.POVendorLocationID;
    ((PXSelectBase<PX.Objects.PO.POOrder>) graphPOOrderEntry.Document).Update(poOrderRow);
  }

  public virtual void InsertPOLine(
    POOrderEntry graphPOOrderEntry,
    PX.Objects.PO.POLine poLineRow,
    POEnabledFSSODet poEnabledFSSODetRow)
  {
    poLineRow = new PX.Objects.PO.POLine()
    {
      BranchID = poEnabledFSSODetRow.BranchID
    };
    poLineRow = ((PXSelectBase<PX.Objects.PO.POLine>) graphPOOrderEntry.Transactions).Current = ((PXSelectBase<PX.Objects.PO.POLine>) graphPOOrderEntry.Transactions).Insert(poLineRow);
    poLineRow.InventoryID = poEnabledFSSODetRow.InventoryID;
    poLineRow.SiteID = poEnabledFSSODetRow.SiteID;
    poLineRow.OrderQty = poEnabledFSSODetRow.EstimatedQty;
    poLineRow.ProjectID = poEnabledFSSODetRow.ProjectID;
    poLineRow.TaskID = poEnabledFSSODetRow.ProjectTaskID;
    poLineRow.CuryUnitCost = poEnabledFSSODetRow.UnitCost;
    poLineRow = ((PXSelectBase<PX.Objects.PO.POLine>) graphPOOrderEntry.Transactions).Update(poLineRow);
    poEnabledFSSODetRow.POType = poLineRow.OrderType;
    poEnabledFSSODetRow.POLineNbr = poLineRow.LineNbr;
    poEnabledFSSODetRow.POCompleted = poLineRow.Completed;
    this.CopyNotesAndAttachments(((PXSelectBase) graphPOOrderEntry.Transactions).Cache, poLineRow, poEnabledFSSODetRow);
  }

  public virtual void CopyNotesAndAttachments(
    PXCache cache,
    PX.Objects.PO.POLine poLineRow,
    POEnabledFSSODet poEnabledFSSODetRow)
  {
    if (((PXSelectBase<POSetup>) this.poSetup).Current == null)
      return;
    bool? fromServiceOrder = ((PXSelectBase<POSetup>) this.poSetup).Current.CopyLineNotesFromServiceOrder;
    if (!fromServiceOrder.GetValueOrDefault())
    {
      fromServiceOrder = ((PXSelectBase<POSetup>) this.poSetup).Current.CopyLineAttachmentsFromServiceOrder;
      if (!fromServiceOrder.GetValueOrDefault())
        return;
    }
    SharedFunctions.CopyNotesAndFiles((PXCache) new PXCache<FSSODet>(cache.Graph), cache, (object) poEnabledFSSODetRow, (object) poLineRow, ((PXSelectBase<POSetup>) this.poSetup).Current.CopyLineNotesFromServiceOrder, ((PXSelectBase<POSetup>) this.poSetup).Current.CopyLineAttachmentsFromServiceOrder);
  }
}
