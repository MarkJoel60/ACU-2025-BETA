// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.InvoiceExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

[PXProtectedAccess(typeof (SOShipmentEntry))]
public abstract class InvoiceExtension : PXGraphExtension<SOShipmentEntry>
{
  public PXAction<PX.Objects.SO.SOShipment> createInvoice;
  public PXAction<PX.Objects.SO.SOShipment> createDropshipInvoice;

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CreateInvoice(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    InvoiceExtension.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new InvoiceExtension.\u003C\u003Ec__DisplayClass1_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.shipments = adapter.Get<PX.Objects.SO.SOShipment>().ToList<PX.Objects.SO.SOShipment>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.adapterSlice = (adapter.MassProcess, adapter.AllowRedirect, adapter.QuickProcessFlow);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.redirectRequired = !((PXGraph) this.Base).IsImport;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!adapter.Arguments.TryGetValue("InvoiceDate", out cDisplayClass10.invoiceDate) || cDisplayClass10.invoiceDate == null)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.invoiceDate = (object) ((PXGraph) this.Base).Accessinfo.BusinessDate;
    }
    ((PXAction) this.Base.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation<SOShipmentEntry>((PXGraphExtension<SOShipmentEntry>) this, new PXToggleAsyncDelegate((object) cDisplayClass10, __methodptr(\u003CCreateInvoice\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass10.shipments;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CreateDropshipInvoice(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    InvoiceExtension.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new InvoiceExtension.\u003C\u003Ec__DisplayClass3_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.list = adapter.Get<PX.Objects.SO.SOShipment>().ToList<PX.Objects.SO.SOShipment>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.adapterSlice = (adapter.MassProcess, adapter.Arguments);
    // ISSUE: method pointer
    PXLongOperation.StartOperation<SOShipmentEntry>((PXGraphExtension<SOShipmentEntry>) this, new PXToggleAsyncDelegate((object) cDisplayClass30, __methodptr(\u003CCreateDropshipInvoice\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass30.list;
  }

  public virtual void InvoiceShipment(
    SOInvoiceEntry docgraph,
    PX.Objects.SO.SOShipment shiporder,
    DateTime invoiceDate,
    InvoiceList list,
    PXQuickProcess.ActionFlow quickProcessFlow)
  {
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) shiporder.ShipmentNbr, Array.Empty<object>()));
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<PX.Objects.SO.SOShipment>((PXGraph) this.Base, "createInvoice", ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.createInvoice).GetCaption(),
        (object) ((PXSelectBase) this.Base.Document).Cache.GetRowDescription((object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current)
      });
    ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.Status = shiporder.Status;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current, true);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXAction) this.Base.Save).Press();
      foreach (PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.SO.SOOrderType> order in PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelectJoin<PX.Objects.SO.SOOrderShipment, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<PX.Objects.SO.SOOrderShipment.orderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<PX.Objects.SO.SOOrderShipment.orderNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.SO.SOOrder.curyInfoID>>, InnerJoin<SOAddress, On<SOAddress.addressID, Equal<PX.Objects.SO.SOOrder.billAddressID>>, InnerJoin<SOContact, On<SOContact.contactID, Equal<PX.Objects.SO.SOOrder.billContactID>>, InnerJoin<PX.Objects.SO.SOOrderType, On<PX.Objects.SO.SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.orderType, Equal<PX.Objects.SO.SOOrderType.orderType>, And<SOOrderTypeOperation.operation, Equal<PX.Objects.SO.SOOrderType.defaultOperation>>>>>>>>>, Where<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<Current<PX.Objects.SO.SOShipment.shipmentType>>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Current<PX.Objects.SO.SOShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.invoiceNbr, IsNull>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
      {
        PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.SO.SOOrderType>.op_Implicit(order).BillShipmentSeparately = shiporder.BillSeparately;
        ((PXGraph) docgraph).Clear();
        ((PXGraph) docgraph).Clear((PXClearOption) 4);
        ((PXSelectBase<ARSetup>) docgraph.ARSetup).Current.RequireControlTotal = new bool?(false);
        if (list is ShipmentInvoices shipmentInvoices)
        {
          PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find((PXGraph) this.Base, PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.SO.SOOrderType>.op_Implicit(order).OrderType);
          string invoiceDocType = docgraph.GetInvoiceDocType(soOrderType, PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.SO.SOOrderType>.op_Implicit(order), PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.SO.SOOrderType>.op_Implicit(order).Operation);
          InvoiceList invoiceList = new InvoiceList((PXGraph) docgraph);
          invoiceList.AddRange(shipmentInvoices.GetInvoices(invoiceDocType));
          int count = invoiceList.Count;
          docgraph.InvoiceOrder(new InvoiceOrderArgs((PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>) order)
          {
            InvoiceDate = invoiceDate,
            Customer = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current,
            List = invoiceList,
            QuickProcessFlow = quickProcessFlow,
            OptimizeExternalTaxCalc = true
          });
          if (invoiceList.Count > count)
            list.Add(PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(invoiceList[count]), PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(invoiceList[count]), (PX.Objects.CM.Extensions.CurrencyInfo) ((PXResult) invoiceList[count])[typeof (PX.Objects.CM.Extensions.CurrencyInfo)]);
        }
        else
          docgraph.InvoiceOrder(new InvoiceOrderArgs((PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>) order)
          {
            InvoiceDate = invoiceDate,
            Customer = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current,
            List = list,
            QuickProcessFlow = quickProcessFlow,
            OptimizeExternalTaxCalc = true
          });
      }
      transactionScope.Complete();
    }
  }

  public static void InvoiceReceipt(
    Dictionary<string, object> parameters,
    List<PX.Objects.SO.SOShipment> list,
    InvoiceList created,
    bool isMassProcess = false)
  {
    SOShipmentEntry docgraph = PXGraph.CreateInstance<SOShipmentEntry>();
    SOInvoiceEntry invoiceEntry = PXGraph.CreateInstance<SOInvoiceEntry>();
    list.Sort((Comparison<PX.Objects.SO.SOShipment>) ((x, y) => x.ShipmentNbr.CompareTo(y.ShipmentNbr)));
    PXProcessing<PX.Objects.SO.SOShipment>.ProcessRecords((IEnumerable<PX.Objects.SO.SOShipment>) list, isMassProcess, (Action<PX.Objects.SO.SOShipment>) (poreceipt =>
    {
      ((PXGraph) invoiceEntry).Clear();
      ((PXGraph) invoiceEntry).Clear((PXClearOption) 4);
      ((PXSelectBase<ARSetup>) invoiceEntry.ARSetup).Current.RequireControlTotal = new bool?(false);
      char[] charArray = typeof (SOShipmentFilter.invoiceDate).Name.ToCharArray();
      charArray[0] = char.ToUpper(charArray[0]);
      object businessDate;
      if (!parameters.TryGetValue(new string(charArray), out businessDate))
        businessDate = (object) ((PXGraph) invoiceEntry).Accessinfo.BusinessDate;
      foreach (PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact> pxResult in PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelectJoin<PX.Objects.SO.SOOrderShipment, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<PX.Objects.SO.SOOrderShipment.orderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<PX.Objects.SO.SOOrderShipment.orderNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.SO.SOOrder.curyInfoID>>, InnerJoin<SOAddress, On<SOAddress.addressID, Equal<PX.Objects.SO.SOOrder.billAddressID>>, InnerJoin<SOContact, On<SOContact.contactID, Equal<PX.Objects.SO.SOOrder.billContactID>>>>>>, Where<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<INDocType.dropShip>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.invoiceNbr, IsNull>>>>.Config>.Select((PXGraph) docgraph, new object[1]
      {
        (object) poreceipt.ShipmentNbr
      }))
      {
        PX.Objects.SO.SOOrderShipment shipment = PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(pxResult);
        shipment.BillShipmentSeparately = poreceipt.BillSeparately;
        PX.Objects.SO.SOOrder order1 = PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(pxResult);
        PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact> order2 = new PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>(shipment, order1, PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(pxResult), PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(pxResult), PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(pxResult));
        PXResultset<SOShipLine, PX.Objects.SO.SOLine> pxResultset = new PXResultset<SOShipLine, PX.Objects.SO.SOLine>();
        ((PXResultset<SOShipLine>) pxResultset).AddRange((IEnumerable<PXResult<SOShipLine>>) docgraph.CollectDropshipDetails(shipment));
        if (created is ShipmentInvoices shipmentInvoices2)
        {
          string invoiceDocType = invoiceEntry.GetInvoiceDocType(PX.Objects.SO.SOOrderType.PK.Find((PXGraph) docgraph, order1.OrderType), order1, shipment.Operation);
          InvoiceList invoiceList = new InvoiceList((PXGraph) docgraph);
          invoiceList.AddRange(shipmentInvoices2.GetInvoices(invoiceDocType));
          int count = invoiceList.Count;
          invoiceEntry.InvoiceOrder(new InvoiceOrderArgs(order2)
          {
            InvoiceDate = (DateTime) businessDate,
            Details = pxResultset,
            List = invoiceList,
            OptimizeExternalTaxCalc = true
          });
          if (invoiceList.Count > count)
            created.Add(PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(invoiceList[count]), PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(invoiceList[count]), (PX.Objects.CM.Extensions.CurrencyInfo) ((PXResult) invoiceList[count])[typeof (PX.Objects.CM.Extensions.CurrencyInfo)]);
        }
        else
          invoiceEntry.InvoiceOrder(new InvoiceOrderArgs(order2)
          {
            InvoiceDate = (DateTime) businessDate,
            Details = pxResultset,
            List = created,
            OptimizeExternalTaxCalc = true
          });
        if (((Dictionary<Type, PXCache>) ((PXGraph) invoiceEntry).Caches).ContainsKey(typeof (PX.Objects.SO.SOOrder)) && PXTimeStampScope.GetPersisted(((PXGraph) invoiceEntry).Caches[typeof (PX.Objects.SO.SOOrder)], (object) order1) != null)
          PXTimeStampScope.PutPersisted(((PXGraph) invoiceEntry).Caches[typeof (PX.Objects.SO.SOOrder)], (object) order1, new object[1]
          {
            (object) ((PXGraph) invoiceEntry).TimeStamp
          });
      }
    }), (Action<PX.Objects.SO.SOShipment>) null, (Func<PX.Objects.SO.SOShipment, Exception, bool, bool?>) null, (Action<PX.Objects.SO.SOShipment>) null, (Action<PX.Objects.SO.SOShipment>) null);
    invoiceEntry.CompleteProcessingImpl(created);
  }
}
