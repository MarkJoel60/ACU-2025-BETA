// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.POCreateSalesOrderProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class POCreateSalesOrderProcess : PXGraph<POCreateSalesOrderProcess>
{
  public virtual void GenerateSalesOrdersFromPurchaseOrders(
    List<POForSalesOrderDocument> itemsList,
    POForSalesOrderFilter filter)
  {
    POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
    foreach (POForSalesOrderDocument items in itemsList)
      POCreateSalesOrderProcess.SetProcessingResult(this.GenerateSalesOrderFromPurchaseOrder(instance, items, filter));
  }

  public virtual void GenerateSalesOrdersFromPurchaseReturns(
    List<POForSalesOrderDocument> itemsList,
    POForSalesOrderFilter filter)
  {
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    foreach (POForSalesOrderDocument items in itemsList)
      POCreateSalesOrderProcess.SetProcessingResult(this.GenerateSalesOrderFromPurchaseReturn(instance, items, filter));
  }

  public virtual ProcessingResult GenerateSalesOrderFromPurchaseOrder(
    POOrderEntry orderEntry,
    POForSalesOrderDocument item,
    POForSalesOrderFilter filter)
  {
    ProcessingResult fromPurchaseOrder = new ProcessingResult();
    PXProcessing<POForSalesOrderDocument>.SetCurrentItem((object) item);
    ((PXGraph) orderEntry).Clear();
    ((PXSelectBase<PX.Objects.PO.POOrder>) orderEntry.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) orderEntry.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) item.DocNbr, new object[1]
    {
      (object) item.DocType
    }));
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) orderEntry.Document).Current;
    if (item.Excluded.GetValueOrDefault())
    {
      try
      {
        current.ExcludeFromIntercompanyProc = new bool?(true);
        ((PXSelectBase<PX.Objects.PO.POOrder>) orderEntry.Document).Update(current);
        ((PXAction) orderEntry.Save).Press();
      }
      catch (Exception ex)
      {
        fromPurchaseOrder.AddErrorMessage(ex.Message);
      }
      return fromPurchaseOrder;
    }
    List<PX.Objects.PO.POLine> list1 = GraphHelper.RowCast<PX.Objects.PO.POLine>((IEnumerable) ((PXSelectBase<PX.Objects.PO.POLine>) orderEntry.Transactions).Select(Array.Empty<object>())).ToList<PX.Objects.PO.POLine>();
    bool? copyProjectDetails = filter.CopyProjectDetails;
    if (copyProjectDetails.GetValueOrDefault())
    {
      int? nullable1 = new int?();
      foreach (PX.Objects.PO.POLine poLine in list1)
      {
        if (!nullable1.HasValue)
        {
          nullable1 = poLine.ProjectID;
        }
        else
        {
          int? nullable2 = nullable1;
          int? projectId = poLine.ProjectID;
          if (!(nullable2.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable2.HasValue == projectId.HasValue))
          {
            fromPurchaseOrder.AddErrorMessage("A sales order has not been generated because different projects are specified in the purchase order lines. To create a sales order and copy project details from the related purchase order, create a separate purchase order for each project. If you do not need to copy project details, clear the Copy Project Details to Generated Sales Orders check box and generate a sales order again.");
            return fromPurchaseOrder;
          }
        }
      }
    }
    POShipAddress poShipAddress = PXResultset<POShipAddress>.op_Implicit(((PXSelectBase<POShipAddress>) orderEntry.Shipping_Address).Select(Array.Empty<object>()));
    POShipContact poShipContact = PXResultset<POShipContact>.op_Implicit(((PXSelectBase<POShipContact>) orderEntry.Shipping_Contact).Select(Array.Empty<object>()));
    List<POOrderDiscountDetail> list2 = GraphHelper.RowCast<POOrderDiscountDetail>((IEnumerable) ((PXSelectBase<POOrderDiscountDetail>) orderEntry.DiscountDetails).Select(Array.Empty<object>())).ToList<POOrderDiscountDetail>();
    try
    {
      PX.Objects.PO.GraphExtensions.POOrderEntryExt.Intercompany extension = ((PXGraph) orderEntry).GetExtension<PX.Objects.PO.GraphExtensions.POOrderEntryExt.Intercompany>();
      PX.Objects.PO.POOrder po = current;
      POShipAddress shipAddress = poShipAddress;
      POShipContact shipContact = poShipContact;
      List<PX.Objects.PO.POLine> lines = list1;
      List<POOrderDiscountDetail> discountLines = list2;
      string intercompanyOrderType = filter.IntercompanyOrderType;
      copyProjectDetails = filter.CopyProjectDetails;
      int num = copyProjectDetails.GetValueOrDefault() ? 1 : 0;
      SOOrder intercompanySalesOrder = extension.GenerateIntercompanySalesOrder(po, shipAddress, shipContact, (IEnumerable<PX.Objects.PO.POLine>) lines, (IEnumerable<POOrderDiscountDetail>) discountLines, intercompanyOrderType, num != 0);
      item.OrderType = intercompanySalesOrder.OrderType;
      item.OrderNbr = intercompanySalesOrder.OrderNbr;
      fromPurchaseOrder = ProcessingResultBase<ProcessingResult, object, ProcessingResultMessage>.CreateSuccess((object) intercompanySalesOrder);
      fromPurchaseOrder.AddMessage((PXErrorLevel) 1, "The {1} sales order of the {0} type has been created successfully.", (object) intercompanySalesOrder.OrderType, (object) intercompanySalesOrder.OrderNbr);
      Decimal? nullable = intercompanySalesOrder.CuryTaxTotal;
      Decimal? curyTaxTotal = item.CuryTaxTotal;
      if (!(nullable.GetValueOrDefault() == curyTaxTotal.GetValueOrDefault() & nullable.HasValue == curyTaxTotal.HasValue))
        fromPurchaseOrder.AddMessage((PXErrorLevel) 3, "The sales order tax total differs from the tax total in the related purchase order.");
      Decimal? curyOrderTotal = intercompanySalesOrder.CuryOrderTotal;
      nullable = item.CuryDocTotal;
      if (!(curyOrderTotal.GetValueOrDefault() == nullable.GetValueOrDefault() & curyOrderTotal.HasValue == nullable.HasValue))
        fromPurchaseOrder.AddMessage((PXErrorLevel) 3, "The sales order total differs from the order total in the related purchase order.");
    }
    catch (Exception ex)
    {
      fromPurchaseOrder.AddErrorMessage(ex.Message);
    }
    return fromPurchaseOrder;
  }

  public virtual ProcessingResult GenerateSalesOrderFromPurchaseReturn(
    POReceiptEntry receiptEntry,
    POForSalesOrderDocument item,
    POForSalesOrderFilter filter)
  {
    ProcessingResult fromPurchaseReturn = new ProcessingResult();
    PXProcessing<POForSalesOrderDocument>.SetCurrentItem((object) item);
    ((PXGraph) receiptEntry).Clear();
    ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Search<PX.Objects.PO.POReceipt.receiptNbr>((object) item.DocNbr, new object[1]
    {
      (object) "RN"
    }));
    PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current;
    if (item.Excluded.GetValueOrDefault())
    {
      try
      {
        current.ExcludeFromIntercompanyProc = new bool?(true);
        ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Update(current);
        ((PXAction) receiptEntry.Save).Press();
      }
      catch (Exception ex)
      {
        fromPurchaseReturn.AddErrorMessage(ex.Message);
      }
      return fromPurchaseReturn;
    }
    try
    {
      SOOrder intercompanySoReturn = ((PXGraph) receiptEntry).GetExtension<PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Intercompany>().GenerateIntercompanySOReturn(current, filter.IntercompanyOrderType);
      item.OrderType = intercompanySoReturn.OrderType;
      item.OrderNbr = intercompanySoReturn.OrderNbr;
      fromPurchaseReturn = ProcessingResultBase<ProcessingResult, object, ProcessingResultMessage>.CreateSuccess((object) intercompanySoReturn);
      fromPurchaseReturn.AddMessage((PXErrorLevel) 1, "The {1} sales order of the {0} type has been created successfully.", (object) intercompanySoReturn.OrderType, (object) intercompanySoReturn.OrderNbr);
    }
    catch (Exception ex)
    {
      fromPurchaseReturn.AddErrorMessage(ex.Message);
    }
    return fromPurchaseReturn;
  }

  private static void SetProcessingResult(ProcessingResult result)
  {
    if (!result.IsSuccess)
      PXProcessing<POForSalesOrderDocument>.SetError(result.GeneralMessage);
    else if (result.HasWarning)
    {
      PXProcessing<POForSalesOrderDocument>.SetWarning(result.GeneralMessage);
    }
    else
    {
      PXProcessing<POForSalesOrderDocument>.SetProcessed();
      PXProcessing<POForSalesOrderDocument>.SetInfo(result.GeneralMessage);
    }
  }
}
