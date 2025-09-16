// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.DropShipLinksExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common.DAC;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.PO.DAC.Unbound;
using PX.Objects.SO.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class DropShipLinksExt : PXGraphExtension<POOrderEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<DropShipLink, Where<DropShipLink.pOOrderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<DropShipLink.pOOrderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<DropShipLink.pOLineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>> DropShipLinks;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.PO.DemandSOOrder, Where<PX.Objects.PO.DemandSOOrder.orderType, Equal<Required<PX.Objects.PO.DemandSOOrder.orderType>>, And<PX.Objects.PO.DemandSOOrder.orderNbr, Equal<Required<PX.Objects.PO.DemandSOOrder.orderNbr>>>>> DemandSOOrders;
  public PXFilter<CreateSOOrderFilter> CreateSOFilter;
  public PXAction<PX.Objects.PO.POOrder> unlinkFromSO;
  public PXAction<PX.Objects.PO.POOrder> convertToNormal;
  public PXAction<PX.Objects.PO.POOrder> createSalesOrder;
  protected bool prefetching;
  protected HashSet<string> prefetched = new HashSet<string>();

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  [PXUIField]
  [PXButton(CommitChanges = true)]
  protected virtual IEnumerable UnlinkFromSO(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DropShipLinksExt.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new DropShipLinksExt.\u003C\u003Ec__DisplayClass5_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.order = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass50.order == null || cDisplayClass50.order.OrderType != "DP" || cDisplayClass50.order.IsLegacyDropShip.GetValueOrDefault() || EnumerableExtensions.IsIn<string>(cDisplayClass50.order.Status, "M", "C", "L"))
      return adapter.Get();
    if (PXResultset<PX.Objects.PO.POOrderReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrderReceipt, PXSelectJoin<PX.Objects.PO.POOrderReceipt, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POOrderReceipt.FK.Receipt>>, Where<PX.Objects.PO.POOrderReceipt.pOType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrderReceipt.pONbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>, OrderBy<Desc<PX.Objects.PO.POReceipt.released>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>())) != null)
    {
      // ISSUE: reference to a compiler-generated field
      throw new PXException("The {0} purchase order cannot be unlinked because one or multiple purchase receipts have been generated for it.", new object[1]
      {
        (object) cDisplayClass50.order.OrderNbr
      });
    }
    // ISSUE: reference to a compiler-generated field
    if (string.IsNullOrEmpty(cDisplayClass50.order.SOOrderNbr))
      throw new PXException("There are no links to sales orders in this document.");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (((PXSelectBase) this.Base.Transactions).View.Ask(PXMessages.LocalizeFormatNoPrefixNLA("The {0} purchase order has a link to the {1} sales order. Do you want to remove the link?", new object[2]
    {
      (object) cDisplayClass50.order.OrderNbr,
      (object) cDisplayClass50.order.SOOrderNbr
    }), (MessageButtons) 1) == 1)
    {
      ((PXAction) this.Base.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass50, __methodptr(\u003CUnlinkFromSO\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable ConvertToNormal(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current != null && !(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderType != "DP"))
    {
      bool? nullable1 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IsLegacyDropShip;
      if (!nullable1.GetValueOrDefault())
      {
        POOrderPrepayment poOrderPrepayment = PXResultset<POOrderPrepayment>.op_Implicit(PXSelectBase<POOrderPrepayment, PXSelectJoin<POOrderPrepayment, InnerJoin<PX.Objects.AP.APRegister, On<PX.Objects.AP.APRegister.docType, Equal<POOrderPrepayment.aPDocType>, And<PX.Objects.AP.APRegister.refNbr, Equal<POOrderPrepayment.aPRefNbr>>>>, Where<POOrderPrepayment.orderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>()));
        if (poOrderPrepayment != null)
          throw new PXException("The {0} prepayment request is applied to the {1} drop-ship purchase order. To convert the {1} purchase order to the Normal type, remove all applications.", new object[2]
          {
            (object) poOrderPrepayment.APRefNbr,
            (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderNbr
          });
        PX.Objects.AP.APTran apTran = PXResultset<PX.Objects.AP.APTran>.op_Implicit(PXSelectBase<PX.Objects.AP.APTran, PXSelect<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.pOOrderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.AP.APTran.pONbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>()));
        if (apTran != null)
          throw new PXException("The {0} AP bill is applied to the {1} drop-ship purchase order. To convert the {1} purchase order to the Normal type, remove all applications.", new object[2]
          {
            (object) apTran.RefNbr,
            (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderNbr
          });
        PX.Objects.PO.POOrderReceipt poOrderReceipt = PXResultset<PX.Objects.PO.POOrderReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrderReceipt, PXSelectJoin<PX.Objects.PO.POOrderReceipt, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POOrderReceipt.FK.Receipt>>, Where<PX.Objects.PO.POOrderReceipt.pOType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrderReceipt.pONbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>()));
        if (poOrderReceipt != null)
          throw new PXException("The {0} purchase receipt is applied to the {1} drop-ship purchase order. To convert the {1} purchase order to the Normal type, remove all applications.", new object[2]
          {
            (object) poOrderReceipt.ReceiptNbr,
            (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderNbr
          });
        nullable1 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IsIntercompany;
        if (nullable1.GetValueOrDefault())
        {
          PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrder.intercompanyPOType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.SO.SOOrder.intercompanyPONbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>, PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
          if (soOrder != null)
          {
            int? shipmentCntr = soOrder.ShipmentCntr;
            int num = 0;
            if (!(shipmentCntr.GetValueOrDefault() == num & shipmentCntr.HasValue))
              throw new PXException("The drop-ship purchase order cannot be converted to the Normal type because a shipment has been prepared for the related {0} sales order.", new object[1]
              {
                (object) soOrder.OrderNbr
              });
          }
        }
        string str;
        if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.SOOrderNbr == null)
          str = PXMessages.LocalizeNoPrefix("The purchase order will be canceled. A new purchase order of the Normal type will be created. The value of the Shipping Destination Type box and the Vendor Tax Zone in the created purchase order will be set to the default value for a purchase order of the Normal type.");
        else
          str = PXMessages.LocalizeFormatNoPrefixNLA("The link to the {0} sales order will be removed. The purchase order will be canceled. A new purchase order of the Normal type will be created. The value of the Shipping Destination Type box and the Vendor Tax Zone in the created purchase order will be set to the default value for a purchase order of the Normal type.", new object[1]
          {
            (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.SOOrderNbr
          });
        if (((PXSelectBase) this.Base.Transactions).View.Ask(str, (MessageButtons) 1) == 1)
        {
          ((PXAction) this.Base.Save).Press();
          PX.Objects.PO.POOrder copy1 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
          PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
          POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) instance).GetExtension<POOrderEntry.MultiCurrency>().CloneCurrencyInfo(defaultCurrencyInfo);
          PX.Objects.PO.POOrder poOrder1 = ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Insert(new PX.Objects.PO.POOrder()
          {
            OrderType = "RO",
            CuryID = currencyInfo.CuryID,
            CuryInfoID = currencyInfo.CuryInfoID
          });
          PX.Objects.PO.POOrder copy2 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(copy1);
          copy2.OrderType = poOrder1.OrderType;
          copy2.OrderNbr = poOrder1.OrderNbr;
          copy2.CuryDiscTot = poOrder1.CuryDiscTot;
          copy2.CuryLineRetainageTotal = poOrder1.CuryLineRetainageTotal;
          copy2.CuryLineTotal = poOrder1.CuryLineTotal;
          copy2.CuryOrderTotal = poOrder1.CuryOrderTotal;
          copy2.CuryPrepaidTotal = poOrder1.CuryPrepaidTotal;
          copy2.CuryRetainageTotal = poOrder1.CuryRetainageTotal;
          copy2.CuryRetainedDiscTotal = poOrder1.CuryRetainedDiscTotal;
          copy2.CuryRetainedTaxTotal = poOrder1.CuryRetainedTaxTotal;
          copy2.CuryTaxTotal = poOrder1.CuryTaxTotal;
          copy2.CuryUnbilledLineTotal = poOrder1.CuryUnbilledLineTotal;
          copy2.CuryUnbilledOrderTotal = poOrder1.CuryUnbilledOrderTotal;
          copy2.CuryUnbilledTaxTotal = poOrder1.CuryUnbilledTaxTotal;
          copy2.CuryUnprepaidTotal = poOrder1.CuryUnprepaidTotal;
          copy2.CuryVatExemptTotal = poOrder1.CuryVatExemptTotal;
          copy2.CuryVatTaxableTotal = poOrder1.CuryVatTaxableTotal;
          copy2.OrderQty = poOrder1.OrderQty;
          copy2.OpenOrderQty = poOrder1.OpenOrderQty;
          copy2.UnbilledOrderQty = poOrder1.UnbilledOrderQty;
          copy2.OrderWeight = poOrder1.OrderWeight;
          copy2.OrderVolume = poOrder1.OrderVolume;
          ((PXSelectBase<PX.Objects.PO.POOrder>) instance.CurrentDocument).SetValueExt<PX.Objects.PO.POOrder.shipDestType>(copy2, (object) poOrder1.ShipDestType);
          ((PXSelectBase<PX.Objects.PO.POOrder>) instance.CurrentDocument).SetValueExt<PX.Objects.PO.POOrder.shipToBAccountID>(copy2, (object) poOrder1.ShipToBAccountID);
          ((PXSelectBase<PX.Objects.PO.POOrder>) instance.CurrentDocument).SetValueExt<PX.Objects.PO.POOrder.shipToLocationID>(copy2, (object) poOrder1.ShipToLocationID);
          copy2.LineCntr = new int?(0);
          copy2.LinesToCloseCntr = new int?(0);
          copy2.LinesToCompleteCntr = new int?(0);
          copy2.DropShipLinesCount = new int?(0);
          copy2.DropShipLinkedLinesCount = new int?(0);
          copy2.DropShipActiveLinksCount = new int?(0);
          copy2.DropShipOpenLinesCntr = new int?(0);
          copy2.DropShipNotLinkedLinesCntr = new int?(0);
          copy2.CuryInfoID = poOrder1.CuryInfoID;
          copy2.Status = poOrder1.Status;
          copy2.Hold = poOrder1.Hold;
          copy2.Approved = poOrder1.Approved;
          copy2.Rejected = poOrder1.Rejected;
          copy2.RequestApproval = poOrder1.RequestApproval;
          copy2.Cancelled = poOrder1.Cancelled;
          copy2.TaxZoneID = poOrder1.TaxZoneID;
          copy2.IsTaxValid = poOrder1.IsTaxValid;
          copy2.IsUnbilledTaxValid = poOrder1.IsUnbilledTaxValid;
          copy2.SOOrderType = poOrder1.SOOrderType;
          copy2.SOOrderNbr = poOrder1.SOOrderNbr;
          copy2.DontEmail = poOrder1.DontEmail;
          copy2.DontPrint = poOrder1.DontPrint;
          copy2.NoteID = poOrder1.NoteID;
          copy2.CreatedByID = poOrder1.CreatedByID;
          copy2.CreatedByScreenID = poOrder1.CreatedByScreenID;
          copy2.CreatedDateTime = poOrder1.CreatedDateTime;
          copy2.LastModifiedByID = poOrder1.LastModifiedByID;
          copy2.LastModifiedByScreenID = poOrder1.LastModifiedByScreenID;
          copy2.LastModifiedDateTime = poOrder1.LastModifiedDateTime;
          copy2.tstamp = poOrder1.tstamp;
          copy2.OriginalPOType = copy1.OrderType;
          copy2.OriginalPONbr = copy1.OrderNbr;
          copy2.IntercompanySOCancelled = poOrder1.IntercompanySOCancelled;
          copy2.IntercompanySONbr = poOrder1.IntercompanySONbr;
          copy2.IntercompanySOType = poOrder1.IntercompanySOType;
          copy2.IntercompanySOWithEmptyInventory = poOrder1.IntercompanySOWithEmptyInventory;
          copy2.IsIntercompanySOCreated = poOrder1.IsIntercompanySOCreated;
          copy2.IsIntercompany = copy1.IsIntercompany;
          nullable1 = copy1.IsIntercompany;
          if (nullable1.GetValueOrDefault())
            copy2.VendorRefNbr = poOrder1.VendorRefNbr;
          PX.Objects.PO.POOrder poOrder2 = ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy2);
          int? branchId = poOrder2.BranchID;
          int? nullable2 = copy1.BranchID;
          if (!(branchId.GetValueOrDefault() == nullable2.GetValueOrDefault() & branchId.HasValue == nullable2.HasValue))
          {
            poOrder2.BranchID = copy1.BranchID;
            ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy2);
          }
          foreach (PXResult<PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Select(Array.Empty<object>()))
          {
            PX.Objects.PO.POLine poLine1 = PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult);
            PX.Objects.PO.POLine poLine2 = ((PXSelectBase<PX.Objects.PO.POLine>) instance.Transactions).Insert();
            PX.Objects.PO.POLine copy3 = PXCache<PX.Objects.PO.POLine>.CreateCopy(poLine2);
            PXCache<PX.Objects.PO.POLine>.RestoreCopy(copy3, poLine1);
            copy3.OrderType = poLine2.OrderType;
            copy3.OrderNbr = poLine2.OrderNbr;
            copy3.LineNbr = poLine2.LineNbr;
            copy3.PlanID = new long?();
            copy3.LineType = POLineType.IsService(poLine1.LineType) ? "SV" : (POLineType.IsStock(poLine1.LineType) ? "GI" : (POLineType.IsNonStock(poLine1.LineType) ? "NS" : "FT"));
            copy3.SOOrderType = poLine2.SOOrderType;
            copy3.SOOrderNbr = poLine2.SOOrderNbr;
            copy3.SOLinkActive = poLine2.SOLinkActive;
            copy3.OrderNoteID = poLine2.OrderNoteID;
            copy3.NoteID = poLine2.NoteID;
            copy3.Completed = new bool?(false);
            copy3.Cancelled = new bool?(false);
            copy3.Closed = new bool?(false);
            copy3.CreatedByID = poLine2.CreatedByID;
            copy3.CreatedByScreenID = poLine2.CreatedByScreenID;
            copy3.CreatedDateTime = poLine2.CreatedDateTime;
            copy3.LastModifiedByID = poLine2.LastModifiedByID;
            copy3.LastModifiedByScreenID = poLine2.LastModifiedByScreenID;
            copy3.LastModifiedDateTime = poLine2.LastModifiedDateTime;
            copy3.tstamp = poLine2.tstamp;
            PX.Objects.PO.POLine poLine3 = ((PXSelectBase<PX.Objects.PO.POLine>) instance.Transactions).Update(copy3);
            nullable2 = poLine3.ProjectID;
            if (!nullable2.HasValue)
            {
              nullable2 = copy3.ProjectID;
              if (nullable2.HasValue)
              {
                poLine3.ProjectID = copy3.ProjectID;
                ((PXSelectBase<PX.Objects.PO.POLine>) instance.Transactions).Update(poLine3);
              }
            }
          }
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            ((PXAction) instance.Save).Press();
            ((PXSelectBase) this.Base.Document).View.SetAnswer((string) null, (WebDialogResult) 1);
            ((PXAction) this.Base.cancelOrder).Press();
            transactionScope.Complete();
          }
          throw new PXRedirectRequiredException((PXGraph) instance, "Redirect to Normal PO");
        }
        return adapter.Get();
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable CreateSalesOrder(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DropShipLinksExt.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new DropShipLinksExt.\u003C\u003Ec__DisplayClass9_0();
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current?.OrderType != "DP")
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.isLegacy = current.IsLegacyDropShip.GetValueOrDefault();
    // ISSUE: reference to a compiler-generated field
    if (current.SOOrderNbr != null && !cDisplayClass90.isLegacy)
      throw new PXException("A sales order cannot be created because the {0} purchase order has already linked to the {1} sales order.", new object[2]
      {
        (object) current.OrderNbr,
        (object) current.SOOrderNbr
      });
    PXSelectJoin<PX.Objects.PO.POLine, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.FK.POLine>>, Where<PX.Objects.SO.SOLineSplit.orderNbr, IsNull, And<PX.Objects.PO.POLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<PX.Objects.PO.POLine.cancelled, NotEqual<True>, And<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>>>> pxSelectJoin = new PXSelectJoin<PX.Objects.PO.POLine, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.FK.POLine>>, Where<PX.Objects.SO.SOLineSplit.orderNbr, IsNull, And<PX.Objects.PO.POLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<PX.Objects.PO.POLine.cancelled, NotEqual<True>, And<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>>>>((PXGraph) this.Base);
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.isLegacy)
    {
      if (!((IQueryable<PXResult<PX.Objects.PO.POLine>>) ((PXSelectBase<PX.Objects.PO.POLine>) pxSelectJoin).Select(new object[2]
      {
        (object) current.OrderType,
        (object) current.OrderNbr
      })).Any<PXResult<PX.Objects.PO.POLine>>())
        throw new PXException("The sales order cannot be created because all lines of the {0} purchase order with stock or non-stock items are linked to another sales order, or the {0} purchase order does not have lines with stock or non-stock items.", new object[1]
        {
          (object) current.OrderNbr
        });
    }
    PXFilter<CreateSOOrderFilter> createSoFilter = this.CreateSOFilter;
    DropShipLinksExt dropShipLinksExt = this;
    // ISSUE: virtual method pointer
    PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) dropShipLinksExt, __vmethodptr(dropShipLinksExt, LineMessagesDialogInitializer));
    if (((PXSelectBase<CreateSOOrderFilter>) createSoFilter).AskExt(initializePanel, true) == 1 && this.CreateSOFilter.VerifyRequired())
    {
      ((PXAction) this.Base.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.lines = (List<PX.Objects.PO.POLine>) null;
      // ISSUE: reference to a compiler-generated field
      if (!cDisplayClass90.isLegacy)
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass90.lines = GraphHelper.RowCast<PX.Objects.PO.POLine>((IEnumerable) ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Select(Array.Empty<object>())).Where<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (l => POLineType.IsDropShip(l.LineType) && !l.Completed.GetValueOrDefault())).ToList<PX.Objects.PO.POLine>();
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass90.lines = GraphHelper.RowCast<PX.Objects.PO.POLine>((IEnumerable) ((PXSelectBase<PX.Objects.PO.POLine>) pxSelectJoin).Select(new object[2]
        {
          (object) current.OrderType,
          (object) current.OrderNbr
        })).ToList<PX.Objects.PO.POLine>();
      }
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.currentFilter = ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.currentDoc = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.currentContact = ((PXSelectBase<POShipContact>) this.Base.Shipping_Contact).Current;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.currentAddress = ((PXSelectBase<POShipAddress>) this.Base.Shipping_Address).Current;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass90, __methodptr(\u003CCreateSalesOrder\u003Eb__1)));
    }
    return adapter.Get();
  }

  public virtual void LineMessagesDialogInitializer(PXGraph graph, string viewName)
  {
    ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current.OrderType = (string) null;
    ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current.OrderNbr = (string) null;
    ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current.CustomerOrderNbr = (string) null;
    CreateSOOrderFilter current1 = ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current;
    int? nullable1;
    int num;
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.ShipDestType == "C")
    {
      nullable1 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.ShipToBAccountID;
      num = nullable1.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool? nullable2 = new bool?(num != 0);
    current1.FixedCustomer = nullable2;
    CreateSOOrderFilter current2 = ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current;
    bool? fixedCustomer = ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current.FixedCustomer;
    int? nullable3;
    if (!fixedCustomer.GetValueOrDefault())
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    else
      nullable3 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.ShipToBAccountID;
    current2.CustomerID = nullable3;
    CreateSOOrderFilter current3 = ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current;
    fixedCustomer = ((PXSelectBase<CreateSOOrderFilter>) this.CreateSOFilter).Current.FixedCustomer;
    int? nullable4;
    if (!fixedCustomer.GetValueOrDefault())
    {
      nullable1 = new int?();
      nullable4 = nullable1;
    }
    else
      nullable4 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.ShipToLocationID;
    current3.CustomerLocationID = nullable4;
  }

  public virtual void _(PX.Data.Events.RowSelected<CreateSOOrderFilter> e)
  {
    if (e.Row == null || ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null)
      return;
    bool flag1 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.ShipDestType == "C";
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.accountLocations>();
    PXUIFieldAttribute.SetEnabled<CreateSOOrderFilter.customerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateSOOrderFilter>>) e).Cache, (object) e.Row, !flag1);
    PXUIFieldAttribute.SetEnabled<CreateSOOrderFilter.customerLocationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateSOOrderFilter>>) e).Cache, (object) e.Row, !flag1 & flag2);
  }

  [CustomerOrderNbrLight]
  [PXMergeAttributes]
  public virtual void _(
    PX.Data.Events.CacheAttached<CreateSOOrderFilter.customerOrderNbr> e)
  {
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<CreateSOOrderFilter.orderType> e)
  {
    if (e.Row == null || ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CreateSOOrderFilter.orderType>>) e).Cache.SetValueExt<CreateSOOrderFilter.orderNbr>(e.Row, (object) null);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<CreateSOOrderFilter.customerID> e)
  {
    if (e.Row == null || ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CreateSOOrderFilter.customerID>>) e).Cache.SetDefaultExt<CreateSOOrderFilter.customerLocationID>(e.Row);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<CreateSOOrderFilter, CreateSOOrderFilter.orderNbr> e)
  {
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CreateSOOrderFilter, CreateSOOrderFilter.orderNbr>, CreateSOOrderFilter, object>) e).NewValue;
    if (e.Row != null && ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current != null && e.Row.OrderType != null && newValue != null && PX.Objects.SO.SOOrder.PK.Find((PXGraph) this.Base, e.Row.OrderType, newValue) != null)
      throw new PXSetPropertyException("A sales order with this number already exists. Enter another number.");
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOOrderType> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOOrderNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBIntAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOLineNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOOrderType> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (DropShipLink.FK.POOrder))]
  [PXFormula(null, typeof (CountCalc<PX.Objects.PO.POOrder.dropShipLinkedLinesCount>))]
  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOOrderNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBIntAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOLineNbr> e)
  {
  }

  [PXUnboundFormula(typeof (Switch<Case<Where<PX.Objects.PO.POLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<PX.Objects.PO.POLine.completed, Equal<False>>>, int1>, int0>), typeof (SumCalc<PX.Objects.PO.POOrder.dropShipLinesCount>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<PX.Objects.PO.POLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<PX.Objects.PO.POLine.completed, Equal<False>, And<PX.Objects.PO.POLine.sOLinkActive, NotEqual<True>>>>, int1>, int0>), typeof (SumCalc<PX.Objects.PO.POOrder.dropShipNotLinkedLinesCntr>), ValidateAggregateCalculation = true)]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.lineType> e)
  {
  }

  [PXUnboundFormula(typeof (Switch<Case<Where<DropShipLink.active, Equal<True>, And<DropShipLink.poCompleted, Equal<False>>>, int1>, int0>), typeof (SumCalc<PX.Objects.PO.POOrder.dropShipActiveLinksCount>))]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.active> e)
  {
  }

  public virtual void _(
    PX.Data.Events.FieldSelecting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOOrderStatus> e)
  {
    PX.Objects.PO.DemandSOOrder demandOrder = this.GetDemandOrder(e.Row);
    if (e.Row != null && demandOrder != null && e.Row.SOOrderStatus != demandOrder.Status)
      e.Row.SOOrderStatus = demandOrder.Status;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOOrderStatus>>) e).ReturnState = (object) demandOrder?.Status;
  }

  [Obsolete("Event handler is kept to avoid breaking changes.")]
  public virtual void _(PX.Data.Events.FieldSelecting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOOrderNbr> e)
  {
  }

  [Obsolete("Event handler is kept to avoid breaking changes.")]
  public virtual void _(PX.Data.Events.FieldSelecting<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOLineNbr> e)
  {
  }

  public virtual void _(PX.Data.Events.RowSelecting<PX.Objects.PO.POLine> e)
  {
    if (this.prefetching)
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    if (e.Row == null || dropShipLink == null)
      return;
    e.Row.SOLinkActive = dropShipLink.Active;
    e.Row.SOOrderNbr = dropShipLink.SOOrderNbr;
    e.Row.SOOrderType = dropShipLink.SOOrderType;
    e.Row.SOLineNbr = dropShipLink.SOLineNbr;
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || e.Row == null || !POLineType.IsDropShip(e.Row.LineType))
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    if (dropShipLink != null && dropShipLink.Active.GetValueOrDefault())
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID>, PX.Objects.PO.POLine, object>) e).NewValue);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID>, PX.Objects.PO.POLine, object>) e).NewValue = (object) inventoryItem?.InventoryCD;
      throw new PXSetPropertyException("The line has an active link to a line of the {0} sales order. To make changes, clear the SO Linked check box.", new object[1]
      {
        (object) dropShipLink.SOOrderNbr
      });
    }
    PX.Objects.IN.InventoryItem inventoryItem1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID>, PX.Objects.PO.POLine, object>) e).NewValue);
    if (!inventoryItem1.StkItem.GetValueOrDefault() && (!inventoryItem1.NonStockReceipt.GetValueOrDefault() || !inventoryItem1.NonStockShip.GetValueOrDefault()))
    {
      PX.Objects.IN.InventoryItem inventoryItem2 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID>, PX.Objects.PO.POLine, object>) e).NewValue);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID>, PX.Objects.PO.POLine, object>) e).NewValue = (object) inventoryItem2?.InventoryCD;
      throw new PXSetPropertyException("Only items for which both 'Require Receipt' and 'Require Shipment' are selected can be drop shipped.");
    }
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.siteID> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || e.Row == null || !POLineType.IsDropShip(e.Row.LineType) || object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.siteID>, PX.Objects.PO.POLine, object>) e).NewValue))
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    if (dropShipLink != null && dropShipLink.Active.GetValueOrDefault())
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.siteID>, PX.Objects.PO.POLine, object>) e).NewValue);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.siteID>, PX.Objects.PO.POLine, object>) e).NewValue = (object) inSite?.SiteCD;
      throw new PXSetPropertyException("The line has an active link to a line of the {0} sales order. To make changes, clear the SO Linked check box.", new object[1]
      {
        (object) dropShipLink.SOOrderNbr
      });
    }
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderQty> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || e.Row == null || !POLineType.IsDropShip(e.Row.LineType))
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    if (dropShipLink == null)
      return;
    Decimal? nullable1 = !dropShipLink.Active.GetValueOrDefault() ? dropShipLink.BaseReceivedQty : throw new PXSetPropertyException("The line has an active link to a line of the {0} sales order. To make changes, clear the SO Linked check box.", new object[1]
    {
      (object) dropShipLink.SOOrderNbr
    });
    Decimal num1 = 0M;
    if (!(nullable1.GetValueOrDefault() > num1 & nullable1.HasValue))
      return;
    Decimal? nullable2 = new Decimal?(0M);
    nullable1 = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderQty>, PX.Objects.PO.POLine, object>) e).NewValue;
    Decimal num2 = 0M;
    if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
      nullable2 = new Decimal?(INUnitAttribute.ConvertToBase<PX.Objects.PO.POLine.inventoryID, PX.Objects.PO.POLine.uOM>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderQty>>) e).Cache, (object) e.Row, (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderQty>, PX.Objects.PO.POLine, object>) e).NewValue, INPrecision.QUANTITY));
    if (dropShipLink.Active.GetValueOrDefault())
      return;
    nullable1 = nullable2;
    Decimal? baseReceivedQty = dropShipLink.BaseReceivedQty;
    if (nullable1.GetValueOrDefault() < baseReceivedQty.GetValueOrDefault() & nullable1.HasValue & baseReceivedQty.HasValue)
    {
      PXCache cache = ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderQty>>) e).Cache;
      PX.Objects.PO.POLine row = e.Row;
      baseReceivedQty = dropShipLink.BaseReceivedQty;
      Decimal num3 = baseReceivedQty.Value;
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) INUnitAttribute.ConvertFromBase<PX.Objects.PO.POLine.inventoryID, PX.Objects.PO.POLine.uOM>(cache, (object) row, num3, INPrecision.QUANTITY)
      });
    }
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.uOM> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || e.Row == null || !POLineType.IsDropShip(e.Row.LineType))
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    if (dropShipLink != null && dropShipLink.Active.GetValueOrDefault())
      throw new PXSetPropertyException("The line has an active link to a line of the {0} sales order. To make changes, clear the SO Linked check box.", new object[1]
      {
        (object) dropShipLink.SOOrderNbr
      });
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOLinkActive> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || e.Row == null || !POLineType.IsDropShip(e.Row.LineType))
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    if (dropShipLink == null || dropShipLink.Active.GetValueOrDefault() || !((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOLinkActive>, PX.Objects.PO.POLine, object>) e).NewValue).GetValueOrDefault())
      return;
    string str = (string) null;
    int? nullable = e.Row.InventoryID;
    int? soInventoryId = dropShipLink.SOInventoryID;
    if (!(nullable.GetValueOrDefault() == soInventoryId.GetValueOrDefault() & nullable.HasValue == soInventoryId.HasValue))
    {
      str = PXUIFieldAttribute.GetDisplayName<PX.Objects.PO.POLine.inventoryID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOLinkActive>>) e).Cache);
    }
    else
    {
      int? siteId = e.Row.SiteID;
      nullable = dropShipLink.SOSiteID;
      if (!(siteId.GetValueOrDefault() == nullable.GetValueOrDefault() & siteId.HasValue == nullable.HasValue))
      {
        str = PXUIFieldAttribute.GetDisplayName<PX.Objects.PO.POLine.siteID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOLinkActive>>) e).Cache);
      }
      else
      {
        Decimal? baseOrderQty = e.Row.BaseOrderQty;
        Decimal? soBaseOrderQty = dropShipLink.SOBaseOrderQty;
        if (!(baseOrderQty.GetValueOrDefault() == soBaseOrderQty.GetValueOrDefault() & baseOrderQty.HasValue == soBaseOrderQty.HasValue))
          str = PXUIFieldAttribute.GetDisplayName<PX.Objects.PO.POLine.orderQty>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.sOLinkActive>>) e).Cache);
      }
    }
    if (str != null)
      throw new PXSetPropertyException("The {0} column value in the line does not match the {0} column value in the linked line of the {1} sales order.", new object[2]
      {
        (object) str,
        (object) dropShipLink.SOOrderNbr
      });
  }

  public virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POLine> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || !POLineType.IsDropShip(e.Row.LineType))
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    bool? nullable;
    if (dropShipLink != null)
    {
      nullable = e.Row.Cancelled;
      if (nullable.GetValueOrDefault())
      {
        ((PXSelectBase<DropShipLink>) this.DropShipLinks).Delete(dropShipLink);
        e.Row.SOLinkActive = new bool?(false);
        e.Row.SOOrderNbr = (string) null;
        e.Row.SOOrderType = (string) null;
        e.Row.SOLineNbr = new int?();
        return;
      }
    }
    if (dropShipLink == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.POLine>>) e).Cache.ObjectsEqual<PX.Objects.PO.POLine.sOLinkActive, PX.Objects.PO.POLine.inventoryID, PX.Objects.PO.POLine.siteID, PX.Objects.PO.POLine.baseOrderQty, PX.Objects.PO.POLine.completed>((object) e.Row, (object) e.OldRow))
      return;
    dropShipLink.Active = e.Row.SOLinkActive;
    dropShipLink.POInventoryID = e.Row.InventoryID;
    dropShipLink.POSiteID = e.Row.SiteID;
    dropShipLink.POBaseOrderQty = e.Row.BaseOrderQty;
    dropShipLink.POCompleted = e.Row.Completed;
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).Update(dropShipLink);
    nullable = e.Row.SOLinkActive;
    bool? soLinkActive = e.OldRow.SOLinkActive;
    if (nullable.GetValueOrDefault() == soLinkActive.GetValueOrDefault() & nullable.HasValue == soLinkActive.HasValue)
      return;
    ((PXSelectBase) this.Base.Transactions).View.RequestRefresh();
  }

  public virtual void _(PX.Data.Events.RowDeleting<PX.Objects.PO.POLine> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || e.Row == null || !POLineType.IsDropShip(e.Row.LineType))
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    if (dropShipLink != null && dropShipLink.Active.GetValueOrDefault() && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.Base.Document).Cache.GetStatus((object) current), (PXEntryStatus) 3, (PXEntryStatus) 4))
    {
      e.Cancel = true;
      throw new PXException("The line cannot be deleted because there is an active link to a line of the {0} sales order. To delete the line, clear the SO Linked check box.", new object[1]
      {
        (object) dropShipLink.SOOrderNbr
      });
    }
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POLine> e)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    if (current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || e.Row == null || !POLineType.IsDropShip(e.Row.LineType))
      return;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POLine.sOOrderNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POLine.sOOrderStatus>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POLine.sOLineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row, false);
    DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
    bool? nullable;
    int num1;
    if (!this.CanReopenPOLine(dropShipLink))
    {
      nullable = e.Row.Completed;
      if (nullable.GetValueOrDefault())
      {
        num1 = 0;
        goto label_6;
      }
    }
    num1 = EnumerableExtensions.IsNotIn<string>(current.Status, "M", "L", "C") ? 1 : 0;
label_6:
    bool flag = num1 != 0;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache;
    PX.Objects.PO.POLine row1 = e.Row;
    int num2;
    if (dropShipLink != null && !this.IsFullQtyReceived(dropShipLink))
    {
      nullable = current.Hold;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POLine.sOLinkActive>(cache1, (object) row1, num2 != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POLine.completed>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POLine.cancelled>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row, flag);
    if (PXUIFieldAttribute.GetErrorOnly<PX.Objects.PO.POLine.completed>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row) == null)
    {
      PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache;
      PX.Objects.PO.POLine row2 = e.Row;
      // ISSUE: variable of a boxed type
      __Boxed<bool?> completed = (ValueType) e.Row.Completed;
      PXSetPropertyException<PX.Objects.PO.POLine.completed> propertyException;
      if (dropShipLink != null)
      {
        nullable = dropShipLink.SOCompleted;
        if (nullable.GetValueOrDefault())
        {
          nullable = e.Row.Completed;
          if (nullable.GetValueOrDefault())
          {
            propertyException = new PXSetPropertyException<PX.Objects.PO.POLine.completed>("The line cannot be reopened because the linked sales order line has been completed.", (PXErrorLevel) 3);
            goto label_15;
          }
        }
      }
      propertyException = (PXSetPropertyException<PX.Objects.PO.POLine.completed>) null;
label_15:
      cache2.RaiseExceptionHandling<PX.Objects.PO.POLine.completed>((object) row2, (object) completed, (Exception) propertyException);
    }
    if (PXUIFieldAttribute.GetErrorOnly<PX.Objects.PO.POLine.sOLinkActive>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache, (object) e.Row) != null)
      return;
    if (dropShipLink != null)
    {
      nullable = dropShipLink.SOCompleted;
      if (nullable.GetValueOrDefault())
      {
        nullable = e.Row.Completed;
        if (!nullable.GetValueOrDefault())
        {
          Decimal? baseReceivedQty = dropShipLink.BaseReceivedQty;
          Decimal? soBaseOrderQty = dropShipLink.SOBaseOrderQty;
          if (baseReceivedQty.GetValueOrDefault() < soBaseOrderQty.GetValueOrDefault() & baseReceivedQty.HasValue & soBaseOrderQty.HasValue)
          {
            ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.sOLinkActive>((object) e.Row, (object) e.Row.SOLinkActive, (Exception) new PXSetPropertyException<PX.Objects.PO.POLine.sOLinkActive>("The linked {0} sales order line has been completed.", (PXErrorLevel) 2, new object[1]
            {
              (object) dropShipLink.SOOrderNbr
            }));
            return;
          }
        }
      }
    }
    int num3;
    if (dropShipLink == null)
    {
      num3 = 1;
    }
    else
    {
      nullable = dropShipLink.Active;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num3 != 0)
    {
      nullable = e.Row.Completed;
      if (!nullable.GetValueOrDefault())
      {
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.sOLinkActive>((object) e.Row, (object) e.Row.SOLinkActive, (Exception) new PXSetPropertyException<PX.Objects.PO.POLine.sOLinkActive>("The purchase order line has no active link to a line of a sales order.", (PXErrorLevel) 2));
        return;
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.sOLinkActive>((object) e.Row, (object) e.Row.SOLinkActive, (Exception) null);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POOrderEntry.CanReopenPOLine(PX.Objects.PO.POLine)" />
  /// </summary>
  [PXOverride]
  public virtual bool CanReopenPOLine(PX.Objects.PO.POLine line, Func<PX.Objects.PO.POLine, bool> baseMethod)
  {
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    return current == null || current.IsLegacyDropShip.GetValueOrDefault() || current.OrderType != "DP" || line == null || EnumerableExtensions.IsNotIn<string>(line.LineType, "GP", "NP") ? baseMethod(line) : this.CanReopenPOLine(this.GetDropShipLink(line));
  }

  protected virtual bool CanReopenPOLine(DropShipLink link)
  {
    if (this.IsFullQtyReceived(link))
      return false;
    return link == null || !link.SOCompleted.GetValueOrDefault();
  }

  protected virtual bool IsFullQtyReceived(DropShipLink link)
  {
    if (link == null || !link.Active.GetValueOrDefault())
      return false;
    Decimal? baseReceivedQty = link.BaseReceivedQty;
    Decimal? poBaseOrderQty = link.POBaseOrderQty;
    return baseReceivedQty.GetValueOrDefault() == poBaseOrderQty.GetValueOrDefault() & baseReceivedQty.HasValue == poBaseOrderQty.HasValue;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POOrderEntry.POOrder_RowDeleting(PX.Data.PXCache,PX.Data.PXRowDeletingEventArgs)" />
  /// </summary>
  [PXOverride]
  public virtual void POOrder_RowDeleting(
    PXCache sender,
    PXRowDeletingEventArgs e,
    PXRowDeleting baseMethod)
  {
    baseMethod.Invoke(sender, e);
    PX.Objects.PO.POOrder row = (PX.Objects.PO.POOrder) e.Row;
    if (row.OrderType != "DP" || row.IsLegacyDropShip.GetValueOrDefault() || row.SOOrderNbr == null)
      return;
    if (((PXSelectBase) this.Base.Document).View.Ask(PXMessages.LocalizeFormatNoPrefixNLA("The {0} purchase order has a link to a sales order. Do you want to remove the link and delete the {0} purchase order?", new object[1]
    {
      (object) row.OrderNbr
    }), (MessageButtons) 1) != 2)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.PO.POOrder> e)
  {
    if (!(e.Row?.OrderType == "DP"))
      return;
    bool? nullable = e.Row.IsLegacyDropShip;
    if (nullable.GetValueOrDefault())
      return;
    nullable = e.Row.Cancelled;
    if (!nullable.GetValueOrDefault())
      return;
    PX.Objects.PO.POOrder poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXSelectReadonly<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.originalPOType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.originalPONbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POOrder[1]
    {
      e.Row
    }, Array.Empty<object>()));
    e.Row.SuccessorPONbr = poOrder?.OrderNbr;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POOrder.originalPONbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) e).Cache, (object) e.Row, e.Row.OrderType == "RO");
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POOrder.successorPONbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) e).Cache, (object) e.Row, e.Row.OrderType == "DP");
    if (e.Row.OrderType != "DP" || this.Base.BlockUIUpdate)
      return;
    bool? nullable1 = e.Row.IsLegacyDropShip;
    bool flag1 = !nullable1.GetValueOrDefault() && e.Row.Status == "A";
    nullable1 = e.Row.IsLegacyDropShip;
    bool flag2 = nullable1.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(e.Row.Status, "N", "M", "C");
    ((PXAction) this.createSalesOrder).SetEnabled(flag1 | flag2);
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) e).Cache;
    PX.Objects.PO.POOrder row1 = e.Row;
    nullable1 = e.Row.IsLegacyDropShip;
    int num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POOrder.sOOrderType>(cache1, (object) row1, num1 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) e).Cache;
    PX.Objects.PO.POOrder row2 = e.Row;
    nullable1 = e.Row.IsLegacyDropShip;
    int num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POOrder.sOOrderNbr>(cache2, (object) row2, num2 != 0);
    nullable1 = e.Row.Cancelled;
    if (nullable1.GetValueOrDefault() || !EnumerableExtensions.IsNotIn<string>(e.Row.Status, "M", "C"))
      return;
    PXException pxException = (PXException) null;
    if ((string.IsNullOrEmpty(e.Row.SOOrderType) ? 0 : (!string.IsNullOrEmpty(e.Row.SOOrderNbr) ? 1 : 0)) != 0)
    {
      PX.Objects.SO.SOOrder soOrder = (PX.Objects.SO.SOOrder) PXSelectorAttribute.Select<PX.Objects.PO.POOrder.sOOrderNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) e).Cache, (object) e.Row);
      if (soOrder != null)
      {
        DateTime? requestDate = soOrder.RequestDate;
        DateTime? nullable2 = e.Row.ExpectedDate;
        if ((requestDate.HasValue & nullable2.HasValue ? (requestDate.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          object[] objArray = new object[2]
          {
            (object) soOrder.OrderNbr,
            null
          };
          nullable2 = soOrder.RequestDate;
          ref DateTime? local = ref nullable2;
          objArray[1] = (object) (local.HasValue ? local.GetValueOrDefault().ToShortDateString() : (string) null);
          pxException = (PXException) new PXSetPropertyException("The Requested On date in the corresponding sales order {0} is {1}.", (PXErrorLevel) 2, objArray);
        }
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POOrder.expectedDate>((object) e.Row, (object) e.Row.ExpectedDate, (Exception) pxException);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POOrderEntry.POOrder_Cancelled_FieldVerifying(PX.Data.PXCache,PX.Data.PXFieldVerifyingEventArgs)" />
  /// </summary>
  [PXOverride]
  public virtual void POOrder_Cancelled_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    PXFieldVerifying baseMethod)
  {
    baseMethod.Invoke(sender, e);
    PX.Objects.PO.POOrder row = (PX.Objects.PO.POOrder) e.Row;
    if (row == null || row.OrderType != "DP")
      return;
    bool? nullable = row.IsLegacyDropShip;
    if (nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.NewValue;
    if (!nullable.GetValueOrDefault() && row.SuccessorPONbr != null)
      throw new PXException("The {0} purchase order cannot be reopened. The {1} purchase order of the Normal type is linked to the {0} purchase order.", new object[2]
      {
        (object) row.OrderNbr,
        (object) row.SuccessorPONbr
      });
    nullable = (bool?) e.NewValue;
    if (!nullable.GetValueOrDefault() || row.SOOrderNbr == null)
      return;
    if (((PXSelectBase) this.Base.Document).View.Ask(PXMessages.LocalizeFormatNoPrefixNLA("The {0} purchase order has a link to a sales order. Do you want to remove the link and cancel the {0} purchase order?", new object[1]
    {
      (object) row.OrderNbr
    }), (MessageButtons) 1) != 2)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.dropShipLinkedLinesCount> e)
  {
    int? oldValue = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder.dropShipLinkedLinesCount>, PX.Objects.PO.POOrder, object>) e).OldValue;
    if (e.Row == null)
      return;
    int? linkedLinesCount = e.Row.DropShipLinkedLinesCount;
    int? nullable = oldValue;
    if (linkedLinesCount.GetValueOrDefault() == nullable.GetValueOrDefault() & linkedLinesCount.HasValue == nullable.HasValue || e.Row.OrderType != "DP" || e.Row.IsLegacyDropShip.GetValueOrDefault())
      return;
    nullable = e.Row.DropShipLinkedLinesCount;
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    ((PXSelectBase) this.Base.Document).Cache.SetValue<PX.Objects.PO.POOrder.sOOrderType>((object) e.Row, (object) null);
    ((PXSelectBase) this.Base.Document).Cache.SetValue<PX.Objects.PO.POOrder.sOOrderNbr>((object) e.Row, (object) null);
  }

  public virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POOrder> e)
  {
    if (e.Row.OrderType != "DP" || e.Row.IsLegacyDropShip.GetValueOrDefault() || ((PXCache) GraphHelper.Caches<PX.Objects.PO.POOrder>((PXGraph) this.Base)).ObjectsEqual<PX.Objects.PO.POOrder.dropShipOpenLinesCntr, PX.Objects.PO.POOrder.dropShipNotLinkedLinesCntr>((object) e.Row, (object) e.OldRow))
      return;
    this.UpdateDocumentState(e.Row);
  }

  public virtual void _(PX.Data.Events.RowDeleted<DropShipLink> e)
  {
    POOrderEntry.SOLineSplit3 split = PXResultset<POOrderEntry.SOLineSplit3>.op_Implicit(((PXSelectBase<POOrderEntry.SOLineSplit3>) new PXSelect<POOrderEntry.SOLineSplit3, Where<POOrderEntry.SOLineSplit3.orderType, Equal<Required<DropShipLink.sOOrderType>>, And<POOrderEntry.SOLineSplit3.orderNbr, Equal<Required<DropShipLink.sOOrderNbr>>, And<POOrderEntry.SOLineSplit3.lineNbr, Equal<Required<DropShipLink.sOLineNbr>>, And<POOrderEntry.SOLineSplit3.pOType, Equal<Required<DropShipLink.pOOrderType>>, And<POOrderEntry.SOLineSplit3.pONbr, Equal<Required<DropShipLink.pOOrderNbr>>, And<POOrderEntry.SOLineSplit3.pOLineNbr, Equal<Required<DropShipLink.pOLineNbr>>>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[6]
    {
      (object) e.Row.SOOrderType,
      (object) e.Row.SOOrderNbr,
      (object) e.Row.SOLineNbr,
      (object) e.Row.POOrderType,
      (object) e.Row.POOrderNbr,
      (object) e.Row.POLineNbr
    }));
    if (split == null)
      return;
    split.POType = (string) null;
    split.PONbr = (string) null;
    split.POLineNbr = new int?();
    split.RefNoteID = new Guid?();
    this.Base.UpdateSOLine(split, split.VendorID, false);
    ((PXSelectBase<POOrderEntry.SOLineSplit3>) this.Base.FixedDemand).Update(split);
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) split.PlanID
    }));
    if (inItemPlan == null)
      return;
    inItemPlan.SupplyPlanID = new long?();
    ((PXGraph) this.Base).Caches[typeof (INItemPlan)].Update((object) inItemPlan);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POOrderEntry.PrefetchWithDetails" />
  /// </summary>
  [PXOverride]
  public virtual void PrefetchWithDetails()
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null || ((PXSelectBase) this.DropShipLinks).Cache.IsDirty || this.prefetched.Contains(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderType + ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderNbr))
      return;
    if (PXView.MaximumRows == 1)
      return;
    try
    {
      this.prefetching = true;
      PXSelectReadonly2<PX.Objects.PO.POLine, LeftJoin<DropShipLink, On<DropShipLink.FK.POLine>, LeftJoin<PX.Objects.PO.DemandSOOrder, On<DropShipLink.FK.DemandSOOrder>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>> pxSelectReadonly2 = new PXSelectReadonly2<PX.Objects.PO.POLine, LeftJoin<DropShipLink, On<DropShipLink.FK.POLine>, LeftJoin<PX.Objects.PO.DemandSOOrder, On<DropShipLink.FK.DemandSOOrder>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Current<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Current<PX.Objects.PO.POOrder.orderNbr>>>>>((PXGraph) this.Base);
      Type[] typeArray = new Type[5]
      {
        typeof (PX.Objects.PO.POLine.orderType),
        typeof (PX.Objects.PO.POLine.orderNbr),
        typeof (PX.Objects.PO.POLine.lineNbr),
        typeof (DropShipLink),
        typeof (PX.Objects.PO.DemandSOOrder)
      };
      using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, typeArray))
      {
        int startRow = PXView.StartRow;
        int num = 0;
        foreach (PXResult<PX.Objects.PO.POLine, DropShipLink, PX.Objects.PO.DemandSOOrder> pxResult in ((PXSelectBase) pxSelectReadonly2).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
        {
          PX.Objects.PO.POLine line = PXResult<PX.Objects.PO.POLine, DropShipLink, PX.Objects.PO.DemandSOOrder>.op_Implicit(pxResult);
          DropShipLink link = PXResult<PX.Objects.PO.POLine, DropShipLink, PX.Objects.PO.DemandSOOrder>.op_Implicit(pxResult);
          PX.Objects.PO.DemandSOOrder order = PXResult<PX.Objects.PO.POLine, DropShipLink, PX.Objects.PO.DemandSOOrder>.op_Implicit(pxResult);
          this.DropShipLinkStoreCached(link, line);
          if (order != null && order.OrderNbr != null)
            this.DemandOrderStoreCached(order);
        }
      }
      this.prefetched.Add(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderType + ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderNbr);
    }
    finally
    {
      this.prefetching = false;
    }
  }

  public virtual void DropShipLinkStoreCached(DropShipLink link, PX.Objects.PO.POLine line)
  {
    List<object> objectList = new List<object>(1);
    if (link != null && link.POOrderType != null)
      objectList.Add((object) link);
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).StoreResult(objectList, PXQueryParameters.ExplicitParameters(new object[3]
    {
      (object) line.OrderType,
      (object) line.OrderNbr,
      (object) line.LineNbr
    }));
  }

  public virtual DropShipLink GetDropShipLink(PX.Objects.PO.POLine line)
  {
    if (line == null || !POLineType.IsDropShip(line.LineType))
      return (DropShipLink) null;
    return PXResultset<DropShipLink>.op_Implicit(((PXSelectBase<DropShipLink>) this.DropShipLinks).SelectWindowed(0, 1, new object[3]
    {
      (object) line.OrderType,
      (object) line.OrderNbr,
      (object) line.LineNbr
    }));
  }

  public virtual void DemandOrderStoreCached(PX.Objects.PO.DemandSOOrder order)
  {
    ((PXSelectBase<PX.Objects.PO.DemandSOOrder>) this.DemandSOOrders).StoreResult((IBqlTable) order);
  }

  public virtual PX.Objects.PO.DemandSOOrder GetDemandOrder(PX.Objects.PO.POLine line)
  {
    if (line == null || !POLineType.IsDropShip(line.LineType))
      return (PX.Objects.PO.DemandSOOrder) null;
    DropShipLink dropShipLink = this.GetDropShipLink(line);
    if (dropShipLink == null)
      return (PX.Objects.PO.DemandSOOrder) null;
    return PXResultset<PX.Objects.PO.DemandSOOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.DemandSOOrder>) this.DemandSOOrders).SelectWindowed(0, 1, new object[2]
    {
      (object) dropShipLink.SOOrderType,
      (object) dropShipLink.SOOrderNbr
    }));
  }

  public virtual void UpdateDocumentState(PX.Objects.PO.POOrder order)
  {
    PXSelectJoin<PX.Objects.PO.POOrder, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POOrder.vendorID>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<PX.Objects.PO.POOrder.orderType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> document1 = this.Base.Document;
    PXSelectJoin<PX.Objects.PO.POOrder, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POOrder.vendorID>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<PX.Objects.PO.POOrder.orderType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> document2 = this.Base.Document;
    string orderNbr = order.OrderNbr;
    object[] objArray = new object[1]
    {
      (object) order.OrderType
    };
    PX.Objects.PO.POOrder poOrder1;
    PX.Objects.PO.POOrder poOrder2 = poOrder1 = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) document2).Search<PX.Objects.PO.POOrder.orderNbr>((object) orderNbr, objArray));
    ((PXSelectBase<PX.Objects.PO.POOrder>) document1).Current = poOrder1;
    order = poOrder2;
    if (order.OrderType != "DP")
      return;
    bool? nullable1 = order.IsLegacyDropShip;
    if (nullable1.GetValueOrDefault())
      return;
    nullable1 = order.Hold;
    if (nullable1.GetValueOrDefault())
      return;
    nullable1 = order.Approved;
    if (!nullable1.GetValueOrDefault())
      return;
    int? nullable2 = order.DropShipOpenLinesCntr;
    int num1 = 0;
    int num2;
    if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
    {
      nullable2 = order.DropShipNotLinkedLinesCntr;
      int num3 = 0;
      num2 = nullable2.GetValueOrDefault() == num3 & nullable2.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    if (num2 != 0)
    {
      ((SelectedEntityEvent<PX.Objects.PO.POOrder>) PXEntityEventBase<PX.Objects.PO.POOrder>.Container<PX.Objects.PO.POOrder.Events>.Select((Expression<Func<PX.Objects.PO.POOrder.Events, PXEntityEvent<PX.Objects.PO.POOrder.Events>>>) (e => e.LinesLinked))).FireOn((PXGraph) this.Base, order);
    }
    else
    {
      nullable2 = order.DropShipNotLinkedLinesCntr;
      int num4 = 0;
      if (!(nullable2.GetValueOrDefault() > num4 & nullable2.HasValue))
        return;
      ((SelectedEntityEvent<PX.Objects.PO.POOrder>) PXEntityEventBase<PX.Objects.PO.POOrder>.Container<PX.Objects.PO.POOrder.Events>.Select((Expression<Func<PX.Objects.PO.POOrder.Events, PXEntityEvent<PX.Objects.PO.POOrder.Events>>>) (e => e.LinesUnlinked))).FireOn((PXGraph) this.Base, order);
    }
  }

  public virtual PX.Objects.PO.POLine InsertDropShipLink(
    PX.Objects.PO.POLine line,
    POOrderEntry.SOLineSplit3 soline)
  {
    if (soline != null && POLineType.IsDropShip(line.LineType))
    {
      ((PXSelectBase<DropShipLink>) this.DropShipLinks).Insert(new DropShipLink()
      {
        POOrderType = line.OrderType,
        POOrderNbr = line.OrderNbr,
        POLineNbr = line.LineNbr,
        SOOrderType = soline.OrderType,
        SOOrderNbr = soline.OrderNbr,
        SOLineNbr = soline.LineNbr,
        POInventoryID = soline.InventoryID,
        POSiteID = soline.SiteID,
        POBaseOrderQty = soline.BaseOrderQty,
        SOInventoryID = soline.InventoryID,
        SOSiteID = soline.SiteID,
        SOBaseOrderQty = soline.BaseOrderQty
      });
      line.SOLinkActive = new bool?(true);
      line = ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Update(line);
    }
    return line;
  }
}
