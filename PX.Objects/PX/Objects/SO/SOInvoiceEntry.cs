// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Async;
using PX.Common;
using PX.Common.Collection;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.AR.MigrationMode;
using PX.Objects.AR.Standalone;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.Exceptions;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using PX.Objects.SO.Models;
using PX.Objects.SO.Services;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

#nullable enable
namespace PX.Objects.SO;

public class SOInvoiceEntry : ARInvoiceEntry
{
  public 
  #nullable disable
  PXAction<PX.Objects.AR.ARInvoice> selectShipment;
  public PXAction<PX.Objects.AR.ARInvoice> addShipment;
  public PXAction<PX.Objects.AR.ARInvoice> addShipmentCancel;
  private bool forceDiscountCalculation;
  public PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.lineType, Equal<SOLineType.freight>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>> Freight;
  public PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.lineType, Equal<SOLineType.discount>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>> Discount;
  public PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<ARSalesPerTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>> commisionlist;
  [PXCopyPasteEmptyFields(new System.Type[] {typeof (PX.Objects.SO.SOInvoice.paymentMethodID), typeof (PX.Objects.SO.SOInvoice.cashAccountID)})]
  public PXSelect<PX.Objects.SO.SOInvoice, Where<PX.Objects.SO.SOInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.SO.SOInvoice.refNbr, Equal<Optional<PX.Objects.AR.ARInvoice.refNbr>>>>> SODocument;
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<SOOrderShipment, OrderBy<Asc<SOOrderShipment.orderType, Asc<SOOrderShipment.orderNbr, Asc<SOOrderShipment.shipmentNbr, Asc<SOOrderShipment.shipmentType>>>>>> shipmentlist;
  public PXSelect<SOShipment> shipments;
  [PXCopyPasteHiddenView]
  public PXSelect<SOFreightDetail, Where<SOFreightDetail.docType, Equal<Optional2<PX.Objects.AR.ARInvoice.docType>>, And<SOFreightDetail.refNbr, Equal<Optional2<PX.Objects.AR.ARInvoice.refNbr>>>>> FreightDetails;
  public PXSelect<SOAdjust> soadjustments;
  public PXSelect<INTran> inTran;
  public PXSetup<SOOrderType, Where<SOOrderType.orderType, Equal<Optional<SOOrder.orderType>>>> soordertype;
  public PXSelect<PX.Objects.AR.ARInvoice> invoiceview;
  public PXSelect<SOAddress> SOAddressView;
  public PXSelectOrderBy<PX.Objects.AR.ExternalTransaction, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.transactionID>>> ExternalTran;
  public PXSelectOrderBy<CCProcTran, OrderBy<Desc<CCProcTran.tranNbr>>> ccProcTran;
  public PXSetup<SOSetup> sosetup;
  public PXSetup<PX.Objects.AR.ARSetup> arsetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSelect<SOOrder, Where<SOOrder.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrder.orderNbr, Equal<Required<SOOrder.orderNbr>>>>> soorder;
  public FbqlSelect<SelectFromBase<SOSetupInvoiceApproval, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SOSetupInvoiceApproval.docType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.AR.ARInvoice.docType, IBqlString>.FromCurrent>>, 
  #nullable disable
  SOSetupInvoiceApproval>.View SetupInvoiceApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomationWithoutHoldDefaulting<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARRegister.approved, PX.Objects.AR.ARRegister.rejected, PX.Objects.AR.ARInvoice.hold, SOSetupInvoiceApproval> SOApproval;
  public new PXInitializeState<PX.Objects.AR.ARInvoice> initializeState;
  public PXAction<PX.Objects.AR.ARInvoice> completeProcessing;
  public new PXAction<PX.Objects.AR.ARInvoice> putOnHold;
  public new PXAction<PX.Objects.AR.ARInvoice> releaseFromHold;
  public PXAction<PX.Objects.AR.ARInvoice> post;
  public new PXAction<PX.Objects.AR.ARInvoice> printInvoice;
  public PXAction<PX.Objects.AR.ARInvoice> arEdit;
  public bool TransferApplicationFromSalesOrder;
  protected HashSet<string> prefetched = new HashSet<string>();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable SelectShipment(PXAdapter adapter)
  {
    if (((PXSelectBase) this.Transactions).AllowInsert && ((PXSelectBase<SOOrderShipment>) this.shipmentlist).AskExt() == 1)
      this.AddShipment(adapter);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddShipment(PXAdapter adapter)
  {
    \u003C\u003Ef__AnonymousType103<SOOrderShipment, PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>>[] array = ((PXSelectBase) this.shipmentlist).Cache.Updated.Cast<SOOrderShipment>().Where<SOOrderShipment>((Func<SOOrderShipment, bool>) (sho => sho.Selected.GetValueOrDefault())).SelectMany(sho => ((IEnumerable<PXResult<SOOrderShipment>>) PXSelectBase<SOOrderShipment, PXSelectJoin<SOOrderShipment, InnerJoin<SOOrder, On<SOOrderShipment.FK.Order>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<SOOrder.curyInfoID>>, InnerJoin<SOAddress, On<SOAddress.addressID, Equal<SOOrder.billAddressID>>, InnerJoin<SOContact, On<SOContact.contactID, Equal<SOOrder.billContactID>>>>>>, Where<SOOrderShipment.shipmentNbr, Equal<Current<SOOrderShipment.shipmentNbr>>, And<SOOrderShipment.shipmentType, Equal<Current<SOOrderShipment.shipmentType>>, And<SOOrderShipment.orderType, Equal<Current<SOOrderShipment.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrderShipment.orderNbr>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) sho
    }, Array.Empty<object>())).AsEnumerable<PXResult<SOOrderShipment>>().Cast<PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>>().Select(row => new
    {
      Shipment = sho,
      Row = row
    })).ToArray();
    HashSet<\u003C\u003Ef__AnonymousType104<string, string>> linkedOrdersKeys = GraphHelper.RowCast<SOOrderShipment>((IEnumerable) ((IEnumerable<PXResult<SOOrderShipment>>) PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AsEnumerable<PXResult<SOOrderShipment>>()).Select(r => new
    {
      Type = r.OrderType,
      Nbr = r.OrderNbr
    }).ToHashSet();
    IEnumerable<SOOrder> soOrders1;
    if (!linkedOrdersKeys.Any())
      soOrders1 = Enumerable.Empty<SOOrder>();
    else
      soOrders1 = (IEnumerable<SOOrder>) GraphHelper.RowCast<SOOrder>((IEnumerable) ((IEnumerable<PXResult<SOOrder>>) PXSelectBase<SOOrder, PXSelectReadonly<SOOrder, Where<SOOrder.orderType, In<Required<SOOrder.orderType>>, And<SOOrder.orderNbr, In<Required<SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) linkedOrdersKeys.Select(k => k.Type).ToArray<string>(),
        (object) linkedOrdersKeys.Select(k => k.Nbr).ToArray<string>()
      })).AsEnumerable<PXResult<SOOrder>>()).Where<SOOrder>((Func<SOOrder, bool>) (so => linkedOrdersKeys.Contains(new
      {
        Type = so.OrderType,
        Nbr = so.OrderNbr
      }))).ToArray<SOOrder>();
    IEnumerable<SOOrder> soOrders2 = soOrders1;
    ILookup<string, SOOrder> lookup1 = array.Select(r => ((PXResult) r.Row).GetItem<SOOrder>()).Concat<SOOrder>(soOrders2).ToLookup<SOOrder, string>((Func<SOOrder, string>) (s => s.TaxZoneID));
    string str1 = lookup1.Any<IGrouping<string, SOOrder>>() ? ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current?.TaxZoneID ?? soOrders2.FirstOrDefault<SOOrder>()?.TaxZoneID ?? lookup1.First<IGrouping<string, SOOrder>>().Key : (string) null;
    ILookup<string, SOOrder> lookup2 = array.Select(r => ((PXResult) r.Row).GetItem<SOOrder>()).Concat<SOOrder>(soOrders2).ToLookup<SOOrder, string>((Func<SOOrder, string>) (s => s.TaxCalcMode));
    string str2 = lookup2.Any<IGrouping<string, SOOrder>>() ? ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current?.TaxCalcMode ?? soOrders2.FirstOrDefault<SOOrder>()?.TaxCalcMode ?? lookup2.First<IGrouping<string, SOOrder>>().Key : (string) null;
    ILookup<bool?, SOOrder> lookup3 = array.Select(r => ((PXResult) r.Row).GetItem<SOOrder>()).Concat<SOOrder>(soOrders2).ToLookup<SOOrder, bool?>((Func<SOOrder, bool?>) (s => s.DisableAutomaticTaxCalculation));
    bool? nullable1;
    bool? nullable2;
    bool? nullable3;
    if (!lookup3.Any<IGrouping<bool?, SOOrder>>())
    {
      nullable3 = new bool?();
    }
    else
    {
      nullable1 = (bool?) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current?.DisableAutomaticTaxCalculation;
      if (!nullable1.HasValue)
      {
        nullable2 = (bool?) soOrders2.FirstOrDefault<SOOrder>()?.DisableAutomaticTaxCalculation;
        nullable3 = nullable2 ?? lookup3.First<IGrouping<bool?, SOOrder>>().Key;
      }
      else
        nullable3 = nullable1;
    }
    bool? nullable4 = nullable3;
    nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    List<SOOrder> source1 = new List<SOOrder>();
    List<SOOrder> source2 = new List<SOOrder>();
    List<SOOrder> source3 = new List<SOOrder>();
    foreach (var data in array)
    {
      if (((PXResult) data.Row).GetItem<SOOrder>().TaxZoneID == str1 && ((PXResult) data.Row).GetItem<SOOrder>().TaxCalcMode == str2)
      {
        nullable1 = ((PXResult) data.Row).GetItem<SOOrder>().DisableAutomaticTaxCalculation;
        nullable2 = nullable4;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
          int num;
          if (current == null)
          {
            num = 0;
          }
          else
          {
            nullable2 = current.DisableAutomaticTaxCalculation;
            num = nullable2.HasValue ? 1 : 0;
          }
          if (num != 0)
            goto label_19;
        }
        PX.Objects.AR.ARInvoice current1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
        int num1;
        if (current1 == null)
        {
          num1 = 1;
        }
        else
        {
          nullable2 = current1.DisableAutomaticTaxCalculation;
          num1 = !nullable2.HasValue ? 1 : 0;
        }
        if (num1 == 0 || soOrders2.Any<SOOrder>())
          goto label_30;
label_19:
        SOOrderShipment soOrderShipment1 = ((PXResult) data.Row).GetItem<SOOrderShipment>();
        PXResultset<SOShipLine, SOLine> pxResultset = new PXResultset<SOShipLine, SOLine>();
        ((PXResultset<SOShipLine>) pxResultset).AddRange((IEnumerable<PXResult<SOShipLine>>) ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>) PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectJoin<PX.Objects.PO.POReceiptLine, InnerJoin<SOLineSplit, On<SOLineSplit.pOType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<SOLineSplit.pONbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<SOLineSplit.pOLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>, InnerJoin<SOLine, On<SOLine.orderType, Equal<SOLineSplit.orderType>, And<SOLine.orderNbr, Equal<SOLineSplit.orderNbr>, And<SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>>>, Where<PX.Objects.PO.POReceiptLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<INDocType.dropShip, Equal<Current<SOOrderShipment.shipmentType>>, And<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<SOLine.orderType, Equal<Current<SOOrderShipment.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrderShipment.orderNbr>>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) soOrderShipment1
        }, new object[2]
        {
          soOrderShipment1.Operation == "R" ? (object) "RN" : (object) "RT",
          (object) soOrderShipment1.ShipmentNbr
        })).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>().Cast<PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>>().Select<PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>, PXResult<SOShipLine, SOLine>>((Func<PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>, PXResult<SOShipLine, SOLine>>) (line => new PXResult<SOShipLine, SOLine>(SOShipLine.FromDropShip(PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>.op_Implicit(line), PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>.op_Implicit(line)), PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>.op_Implicit(line)))));
        ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal = new bool?(false);
        try
        {
          using (new SOInvoiceAddOrderPaymentsScope())
            this.InvoiceOrder(new InvoiceOrderArgs(data.Row)
            {
              InvoiceDate = ((PXGraph) this).Accessinfo.BusinessDate.Value,
              Details = pxResultset
            });
        }
        catch (Exception ex)
        {
          SOOrderShipment soOrderShipment2 = ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Locate(data.Shipment);
          if (soOrderShipment2?.InvoiceType == ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType && soOrderShipment2.InvoiceNbr == ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.RefNbr)
          {
            soOrderShipment2.HasUnhandledErrors = new bool?(true);
            ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(soOrderShipment2);
          }
          throw;
        }
        finally
        {
          ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal = new bool?(valueOrDefault);
        }
        data.Shipment.HasDetailDeleted = new bool?(false);
        data.Shipment.IsPartialInvoiceConstraintViolated = new bool?(false);
        ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(data.Shipment);
        continue;
      }
label_30:
      if (((PXResult) data.Row).GetItem<SOOrder>().TaxZoneID != str1)
      {
        source1.Add(PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(data.Row));
      }
      else
      {
        nullable2 = ((PXResult) data.Row).GetItem<SOOrder>().DisableAutomaticTaxCalculation;
        nullable1 = nullable4;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          source3.Add(PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(data.Row));
        else
          source2.Add(PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.Extensions.CurrencyInfo, SOAddress, SOContact>.op_Implicit(data.Row));
      }
    }
    ((PXSelectBase) this.shipmentlist).View.Clear();
    if (source1.Any<SOOrder>())
      throw new PXInvalidOperationException("The following sales orders were not added to the invoice because they have tax zones other than {0}: {1}.", new object[2]
      {
        (object) str1,
        (object) string.Join(",", source1.Select<SOOrder, string>((Func<SOOrder, string>) (s => s.OrderNbr)))
      });
    if (source2.Any<SOOrder>())
      throw new PXInvalidOperationException("The following sales orders were not added to the invoice because they have tax calculation mode other than {0}: {1}.", new object[2]
      {
        (object) PXStringListAttribute.GetLocalizedLabel<SOOrder.taxCalcMode>(((PXSelectBase) this.Document).Cache, (object) null, str2),
        (object) string.Join(",", source2.Select<SOOrder, string>((Func<SOOrder, string>) (s => s.OrderNbr)))
      });
    if (source3.Any<SOOrder>())
      throw new PXInvalidOperationException("The following sales orders cannot be added to the invoice because of inconsistencies in the states of the Disable Automatic Tax Calculation check box across the documents: {0}.", new object[1]
      {
        (object) string.Join(",", source3.Select<SOOrder, string>((Func<SOOrder, string>) (s => s.OrderNbr)))
      });
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddShipmentCancel(PXAdapter adapter)
  {
    foreach (SOOrderShipment soOrderShipment in ((PXSelectBase) this.shipmentlist).Cache.Updated)
    {
      if (soOrderShipment.InvoiceNbr == null)
        soOrderShipment.Selected = new bool?(false);
    }
    ((PXSelectBase) this.shipmentlist).View.Clear();
    return adapter.Get();
  }

  public bool cancelUnitPriceCalculation { get; set; }

  [PXDBString(2, IsFixed = true)]
  [SOLineType.List]
  [PXUIField(DisplayName = "Line Type", Visible = false, Enabled = false)]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.inventoryID, IsNull>, SOLineType.nonInventory, Case<Where<PX.Objects.AR.ARTran.sOShipmentNbr, IsNull>, Selector<PX.Objects.AR.ARTran.inventoryID, Switch<Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>, SOLineType.inventory, Case<Where<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>, SOLineType.nonInventory>>, SOLineType.miscCharge>>>>, PX.Objects.AR.ARTran.lineType>))]
  protected virtual void ARTran_LineType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [SOInvoiceTax(Inventory = typeof (PX.Objects.AR.ARTran.inventoryID), UOM = typeof (PX.Objects.AR.ARTran.uOM), LineQty = typeof (PX.Objects.AR.ARTran.qty))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Selector<PX.Objects.AR.ARTran.inventoryID, PX.Objects.IN.InventoryItem.taxCategoryID>))]
  protected override void ARTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price", Visible = true)]
  protected virtual void ARTran_ManualPrice_CacheAttached(PXCache sender)
  {
  }

  [PopupMessage]
  [PXRemoveBaseAttribute(typeof (ARTranInventoryItemAttribute))]
  [PXMergeAttributes]
  [NonStockNonKitCrossItem(INPrimaryAlternateType.CPN, "A non-stock kit cannot be added to a document manually. Use the Sales Orders (SO301000) form to prepare an invoice for the corresponding sales order.", typeof (PX.Objects.AR.ARTran.sOOrderNbr), typeof (FeaturesSet.advancedSOInvoices), Filterable = true)]
  protected override void ARTran_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [SiteAvail(typeof (PX.Objects.AR.ARTran.inventoryID), typeof (PX.Objects.AR.ARTran.subItemID), typeof (CostCenter.freeStock), DocumentBranchType = typeof (PX.Objects.AR.ARInvoice.branchID))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<PX.Objects.AR.ARInvoice.branchID>>>))]
  protected override void ARTran_SiteID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  protected override void ARTran_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBQuantity(typeof (PX.Objects.AR.ARTran.uOM), typeof (PX.Objects.AR.ARTran.baseQty), InventoryUnitType.BaseUnit | InventoryUnitType.SalesUnit, HandleEmptyKey = true)]
  protected virtual void ARTran_Qty_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXUIFieldAttribute))]
  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  [SOInvoiceLinesSorting]
  protected virtual void ARTran_SortOrder_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ARDocType.SOEntryList]
  [PXUIField]
  protected virtual void ARInvoice_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARInvoiceType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<PX.Objects.AR.Customer, On<ARRegisterAlias.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<ARRegisterAlias.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And<ARRegisterAlias.origModule, Equal<BatchModule.moduleSO>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>), Filterable = true, IsPrimaryViewCompatible = true)]
  [ARInvoiceType.Numbering]
  [ARInvoiceNbr]
  protected virtual void ARInvoice_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [SOOpenPeriod(typeof (PX.Objects.AR.ARRegister.docDate), typeof (PX.Objects.AR.ARRegister.branchID), null, null, null, null, true, false, typeof (PX.Objects.AR.ARRegister.tranPeriodID), IsHeader = true)]
  [PXMergeAttributes]
  protected virtual void ARInvoice_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  [PXFormula(typeof (IIf<Where<ExternalCall, Equal<True>, Or<PendingValue<PX.Objects.AR.ARInvoice.termsID>, IsNull>>, IIf<Where<Current<PX.Objects.AR.ARInvoice.docType>, NotEqual<ARDocType.creditMemo>, Or<Current<PX.Objects.AR.ARSetup.termsInCreditMemos>, Equal<True>>>, Selector<PX.Objects.AR.ARInvoice.customerID, PX.Objects.AR.Customer.termsID>, Null>, PX.Objects.AR.ARInvoice.termsID>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  [SOInvoiceTerms]
  protected override void ARInvoice_TermsID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<boolTrue>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<boolTrue>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.paymentMethodID> e)
  {
  }

  [PXDBDate]
  [PXUIField]
  protected virtual void ARInvoice_DueDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField]
  protected virtual void ARInvoice_DiscDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.origDocAmt))]
  [PXUIField]
  protected virtual void ARInvoice_CuryOrigDocAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.docBal), BaseCalc = false)]
  [PXUIField]
  protected virtual void ARInvoice_CuryDocBal_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.origDiscAmt))]
  [PXUIField]
  protected virtual void ARInvoice_CuryOrigDiscAmt_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Line Total")]
  protected virtual void ARInvoice_CuryGoodsTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<PX.Objects.AR.Standalone.ARRegister, Where<PX.Objects.AR.ARRegister.noteID, Equal<Current<PX.Objects.AR.ARAdjust.invoiceID>>, And<Current<PX.Objects.AR.ARAdjust.adjgDocType>, Equal<ARDocType.creditMemo>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARAdjust.invoiceID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyVatExemptTotal> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.AR.ARInvoice.curyVatTaxableTotal> e)
  {
  }

  protected override void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.branchID> e)
  {
    base._(e);
    PX.Objects.AR.ARInvoice row = e.Row;
    if (row == null)
      return;
    PX.Objects.AR.ARInvoice.paymentMethodID parent = PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.paymentMethodID>.FindParent((PXGraph) this, (PX.Objects.AR.ARInvoice.paymentMethodID) row, (PKFindOptions) 0);
    if ((parent != null ? (EnumerableExtensions.IsIn<string>(((PX.Objects.CA.PaymentMethod) parent).PaymentType, "CCD", "EFT") ? 1 : 0) : 0) == 0)
      return;
    PX.Objects.AR.ARInvoice copy = (PX.Objects.AR.ARInvoice) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.branchID>>) e).Cache.CreateCopy((object) row);
    copy.BranchID = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.branchID>, PX.Objects.AR.ARInvoice, object>) e).OldValue;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice.branchID>>) e).Cache.RaiseFieldDefaulting<PX.Objects.AR.ARInvoice.pMInstanceID>((object) copy, ref obj);
    int? nullable = (int?) obj;
    int? pmInstanceId = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.PMInstanceID;
    if (!(nullable.GetValueOrDefault() == pmInstanceId.GetValueOrDefault() & nullable.HasValue == pmInstanceId.HasValue))
      return;
    ((PXSelectBase) this.SODocument).Cache.SetDefaultExt<PX.Objects.SO.SOInvoice.pMInstanceID>((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current);
  }

  [PXDBInt]
  [PXDBDefault(typeof (SOAddress.addressID), DefaultForInsert = false, DefaultForUpdate = false)]
  protected virtual void SOOrderShipment_ShipAddressID_CacheAttached(PXCache sender)
  {
  }

  [CustomerActive]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.customerID))]
  protected virtual void SOInvoice_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault]
  protected virtual void SOAdjust_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  protected virtual void SOAdjust_AdjdOrderType_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXRemoveBaseAttribute(typeof (PXRestrictorAttribute))]
  [PXRemoveBaseAttribute(typeof (PXUnboundFormulaAttribute))]
  [PXMergeAttributes]
  protected virtual void SOAdjust_AdjdOrderNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault]
  protected virtual void SOAdjust_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXParent(typeof (Select<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<SOAdjust.adjgDocType>>, And<ARPaymentTotals.refNbr, Equal<Current<SOAdjust.adjgRefNbr>>>>>), ParentCreate = true)]
  protected virtual void SOAdjust_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDBDecimalAttribute))]
  [PXMergeAttributes]
  [PXDBCurrency(typeof (SOAdjust.adjdCuryInfoID), typeof (SOAdjust.adjAmt))]
  [PXUIField(DisplayName = "Applied To Order")]
  protected virtual void SOAdjust_CuryAdjdAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(4)]
  [PXFormula(typeof (Maximum<Sub<SOAdjust.origAdjAmt, SOAdjust.adjBilledAmt>, decimal0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void SOAdjust_AdjAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(4)]
  [PXFormula(typeof (Maximum<Sub<SOAdjust.curyOrigAdjgAmt, SOAdjust.curyAdjgBilledAmt>, decimal0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void SOAdjust_CuryAdjgAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo]
  protected virtual void SOAdjust_AdjdOrigCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo]
  protected virtual void SOAdjust_AdjgCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo]
  protected virtual void SOAdjust_AdjdCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIEnabled(typeof (Where<ARInvoiceDiscountDetail.type, NotEqual<DiscountType.ExternalDocumentDiscount>, And<ARInvoiceDiscountDetail.orderNbr, IsNull>>))]
  [PXUIField(DisplayName = "Discount Code", Required = false)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>>>))]
  protected virtual void ARInvoiceDiscountDetail_DiscountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDecimalAttribute))]
  [PXDBCurrency(typeof (PX.Objects.AR.ARAdjust.adjdCuryInfoID), typeof (PX.Objects.AR.ARAdjust.adjAmt), BaseCalc = false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<PX.Objects.AR.ARAdjust.voided, Equal<False>>, PX.Objects.AR.ARAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyPaymentTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<PX.Objects.AR.ARAdjust.voided, Equal<False>>, PX.Objects.AR.ARAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<ARInvoiceAdjusted.curyPaymentTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<PX.Objects.AR.ARAdjust.voided, Equal<False>, And<PX.Objects.AR.ARAdjust.released, Equal<False>, And<Where<PX.Objects.AR.ARAdjust.paymentID, IsNotNull, And<PX.Objects.AR.ARAdjust.paymentReleased, NotEqual<True>, Or<PX.Objects.AR.ARAdjust.invoiceID, IsNotNull, And<Parent<PX.Objects.AR.Standalone.ARRegister.released>, NotEqual<True>>>>>>>>, PX.Objects.AR.ARAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyUnreleasedPaymentAmt>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<PX.Objects.AR.ARAdjust.voided, Equal<False>, And<PX.Objects.AR.ARAdjust.released, Equal<False>, And<Where<PX.Objects.AR.ARAdjust.paymentID, IsNotNull, And<PX.Objects.AR.ARAdjust.paymentReleased, Equal<True>, Or<PX.Objects.AR.ARAdjust.invoiceID, IsNotNull, And<Parent<PX.Objects.AR.Standalone.ARRegister.released>, Equal<True>>>>>>>>, PX.Objects.AR.ARAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<PX.Objects.AR.ARInvoice.curyPaidAmt>))]
  protected override void _(PX.Data.Events.CacheAttached<PX.Objects.AR.ARAdjust.curyAdjdAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [ARPaymentType.List]
  [PXDefault]
  [PXUIField(DisplayName = "Doc. Type")]
  protected new virtual void ARAdjust2_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [ARPaymentType.AdjgRefNbr(typeof (Search<PX.Objects.AR.ARPayment.refNbr, Where<PX.Objects.AR.ARPayment.docType, Equal<Optional<ARAdjust2.adjgDocType>>>>), Filterable = true)]
  protected override void ARAdjust2_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PX.Objects.CM.Extensions.PXDBCurrencyAttribute), "MinValue", -7.9228162514264338E+28)]
  protected virtual void ARAdjust2_CuryAdjdAmt_CacheAttached(PXCache sender)
  {
  }

  public virtual IEnumerable externalTran()
  {
    SOInvoiceEntry soInvoiceEntry = this;
    foreach (PXResult<PX.Objects.AR.ExternalTransaction> pxResult in PXSelectBase<PX.Objects.AR.ExternalTransaction, PXSelectReadonly<PX.Objects.AR.ExternalTransaction, Where<PX.Objects.AR.ExternalTransaction.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ExternalTransaction.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>>>, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.transactionID>>>.Config>.SelectMultiBound((PXGraph) soInvoiceEntry, PXView.Currents, Array.Empty<object>()))
      yield return (object) PXResult<PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
    foreach (PXResult<PX.Objects.AR.ExternalTransaction> pxResult in PXSelectBase<PX.Objects.AR.ExternalTransaction, PXSelectReadonly2<PX.Objects.AR.ExternalTransaction, InnerJoin<SOOrderShipment, On<SOOrderShipment.orderNbr, Equal<PX.Objects.AR.ExternalTransaction.origRefNbr>, And<SOOrderShipment.orderType, Equal<PX.Objects.AR.ExternalTransaction.origDocType>>>>, Where<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ExternalTransaction.refNbr, IsNull>>>, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.transactionID>>>.Config>.SelectMultiBound((PXGraph) soInvoiceEntry, PXView.Currents, Array.Empty<object>()))
      yield return (object) PXResult<PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
  }

  public virtual IEnumerable ccproctran()
  {
    SOInvoiceEntry soInvoiceEntry = this;
    PXResultset<PX.Objects.AR.ExternalTransaction> pxResultset = ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) soInvoiceEntry.ExternalTran).Select(Array.Empty<object>());
    PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>> query = new PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>>((PXGraph) soInvoiceEntry);
    foreach (PXResult<PX.Objects.AR.ExternalTransaction> pxResult1 in pxResultset)
    {
      PX.Objects.AR.ExternalTransaction externalTransaction = PXResult<PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult1);
      PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>> pxSelect = query;
      object[] objArray = new object[1]
      {
        (object) externalTransaction.TransactionID
      };
      foreach (PXResult<CCProcTran> pxResult2 in ((PXSelectBase<CCProcTran>) pxSelect).Select(objArray))
        yield return (object) PXResult<CCProcTran>.op_Implicit(pxResult2);
    }
  }

  public SOInvoiceLineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<SOInvoiceLineSplittingExtension>();
  }

  public SOInvoiceItemAvailabilityExtension ItemAvailabilityExt
  {
    get => ((PXGraph) this).FindImplementation<SOInvoiceItemAvailabilityExtension>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Complete Processing")]
  protected virtual IEnumerable CompleteProcessing(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOInvoiceEntry.\u003C\u003Ec__DisplayClass80_0 cDisplayClass800 = new SOInvoiceEntry.\u003C\u003Ec__DisplayClass80_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass800.isMassProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass800.invoices = adapter.Get<PX.Objects.AR.ARInvoice>().ToList<PX.Objects.AR.ARInvoice>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass800, __methodptr(\u003CCompleteProcessing\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass800.invoices;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected override IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected override IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public override IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOInvoiceEntry.\u003C\u003Ec__DisplayClass85_0 cDisplayClass850 = new SOInvoiceEntry.\u003C\u003Ec__DisplayClass85_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass850.isMassProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass850.list = new List<PX.Objects.AR.ARRegister>();
    foreach (PX.Objects.AR.ARInvoice doc in adapter.Get<PX.Objects.AR.ARInvoice>())
    {
      this.OnBeforeRelease((PX.Objects.AR.ARRegister) doc);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) doc, true);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass850.list.Add((PX.Objects.AR.ARRegister) doc);
    }
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass850, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass850.list;
  }

  public virtual void ReleaseInvoiceProc(List<PX.Objects.AR.ARRegister> list, bool isMassProcess)
  {
    this.ReleaseInvoiceProcImpl(list, isMassProcess);
  }

  public void ReleaseInvoiceProcImpl(List<PX.Objects.AR.ARRegister> list, bool isMassProcess)
  {
    PXTimeStampScope.SetRecordComesFirst(typeof (PX.Objects.AR.ARInvoice), true);
    PXNoteAttribute.ForcePassThrow<PX.Objects.AR.ARTran.noteID>(((PXSelectBase) this.Freight).Cache);
    SOOrderShipmentProcess docgraph = PXGraph.CreateInstance<SOOrderShipmentProcess>();
    InvoicePostingContext invoicePostingContext = this.GetInvoicePostingContext();
    HashSet<PX.Objects.IN.INRegister> createdIssuesToRelease = new HashSet<PX.Objects.IN.INRegister>();
    HashSet<object> processedInvoices = new HashSet<object>();
    try
    {
      ARDocumentRelease.ReleaseDoc(list, isMassProcess, (List<PX.Objects.GL.Batch>) null, (ARDocumentRelease.ARMassProcessDelegate) null, (ARDocumentRelease.ARMassProcessReleaseTransactionScopeDelegate) (ardoc =>
      {
        ((PXGraph) docgraph).Clear();
        List<PXResult<SOOrderShipment, SOOrder>> orderShipments = docgraph.UpdateOrderShipments(ardoc, processedInvoices);
        List<SOOrderShipment> soOrderShipmentList = new List<SOOrderShipment>();
        docgraph.CompleteMiscLines(ardoc, soOrderShipmentList);
        docgraph.UpdateApplications(ardoc, GraphHelper.RowCast<SOOrderShipment>((IEnumerable) orderShipments).Union<SOOrderShipment>((IEnumerable<SOOrderShipment>) soOrderShipmentList));
        ((SelectedEntityEvent<PX.Objects.SO.SOInvoice>) PXEntityEventBase<PX.Objects.SO.SOInvoice>.Container<PX.Objects.SO.SOInvoice.Events>.Select((Expression<Func<PX.Objects.SO.SOInvoice.Events, PXEntityEvent<PX.Objects.SO.SOInvoice.Events>>>) (e => e.InvoiceReleased))).FireOn((PXGraph) docgraph, PX.Objects.SO.SOInvoice.PK.Find((PXGraph) docgraph, ardoc.DocType, ardoc.RefNbr));
        ((PXAction) docgraph.Save).Press();
        DocumentList<PX.Objects.IN.INRegister> documentList = new DocumentList<PX.Objects.IN.INRegister>((PXGraph) invoicePostingContext.IssueEntry);
        ((PXGraph) this).Clear((PXClearOption) 3);
        this.PostInvoice(invoicePostingContext, ardoc as PX.Objects.AR.ARInvoice, documentList, soOrderShipmentList);
        if (((PXSelectBase<SOSetup>) this.sosetup).Current.AutoReleaseIN.GetValueOrDefault() && documentList.Count > 0 && documentList.All<PX.Objects.IN.INRegister>((Func<PX.Objects.IN.INRegister, bool>) (issue =>
        {
          bool? hold = issue.Hold;
          bool flag = false;
          return hold.GetValueOrDefault() == flag & hold.HasValue;
        })))
          EnumerableExtensions.AddRange<PX.Objects.IN.INRegister>((ISet<PX.Objects.IN.INRegister>) createdIssuesToRelease, (IEnumerable<PX.Objects.IN.INRegister>) documentList);
        this.CreateDirectShipments(soOrderShipmentList, GraphHelper.RowCast<SOOrderShipment>((IEnumerable) orderShipments).ToList<SOOrderShipment>());
        docgraph.OnInvoiceReleased(ardoc, orderShipments);
        this.CopyFreightNotesAndFilesToARTran(ardoc);
      }));
    }
    finally
    {
      if (createdIssuesToRelease.Any<PX.Objects.IN.INRegister>())
        this.ReleaseCreatedIssues(invoicePostingContext.IssueEntry, (IEnumerable<PX.Objects.IN.INRegister>) createdIssuesToRelease);
    }
  }

  protected virtual void ReleaseCreatedIssues(
    INIssueEntry issueEntry,
    IEnumerable<PX.Objects.IN.INRegister> createdIssues)
  {
    ((PXGraph) issueEntry).Clear();
    EnumerableExtensions.Consume(issueEntry.release.Press<PX.Objects.IN.INRegister>(createdIssues));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Post Invoice to IN", Visible = false)]
  [Obsolete("The action is obsolete as Posting to IN became a part of the Release action.")]
  protected virtual IEnumerable Post(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOInvoiceEntry.\u003C\u003Ec__DisplayClass90_0 cDisplayClass900 = new SOInvoiceEntry.\u003C\u003Ec__DisplayClass90_0();
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass900.isMassProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass900.list = new List<PX.Objects.AR.ARRegister>();
    foreach (PX.Objects.AR.ARInvoice arInvoice in adapter.Get<PX.Objects.AR.ARInvoice>())
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass900.list.Add((PX.Objects.AR.ARRegister) arInvoice);
    }
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass900, __methodptr(\u003CPost\u003Eb__0)));
    return adapter.Get();
  }

  private void PostInvoiceToINProc(List<PX.Objects.AR.ARRegister> list, bool isMassProcess)
  {
    InvoicePostingContext invoicePostingContext = this.GetInvoicePostingContext();
    DocumentList<PX.Objects.IN.INRegister> inlist = new DocumentList<PX.Objects.IN.INRegister>((PXGraph) invoicePostingContext.IssueEntry);
    int num = PXProcessing<PX.Objects.AR.ARInvoice>.ProcessRecords(list.Cast<PX.Objects.AR.ARInvoice>(), isMassProcess, (System.Action<PX.Objects.AR.ARInvoice>) (ardoc =>
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        List<SOOrderShipment> directShipmentsToCreate = new List<SOOrderShipment>();
        this.PostInvoice(invoicePostingContext, ardoc, inlist, directShipmentsToCreate);
        this.CreateDirectShipments(directShipmentsToCreate, new List<SOOrderShipment>());
        transactionScope.Complete();
      }
    }), (System.Action<PX.Objects.AR.ARInvoice>) null, (Func<PX.Objects.AR.ARInvoice, Exception, bool, bool?>) null, (System.Action<PX.Objects.AR.ARInvoice>) null, (System.Action<PX.Objects.AR.ARInvoice>) null);
    if (((PXSelectBase<SOSetup>) this.sosetup).Current.AutoReleaseIN.GetValueOrDefault() && inlist.Count > 0)
    {
      bool? hold = inlist[0].Hold;
      bool flag = false;
      if (hold.GetValueOrDefault() == flag & hold.HasValue)
        INDocumentRelease.ReleaseDoc((List<PX.Objects.IN.INRegister>) inlist, false);
    }
    if (num > 0)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
  }

  [PXUIField]
  [PXButton]
  protected override IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<PX.Objects.AR.ARInvoice> list = adapter.Get<PX.Objects.AR.ARInvoice>().ToList<PX.Objects.AR.ARInvoice>();
    if (!string.IsNullOrEmpty(reportID))
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      string actualReportID = (string) null;
      PXReportRequiredException ex = (PXReportRequiredException) null;
      Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
      PXProcessing<PX.Objects.AR.ARInvoice>.ProcessRecords((IEnumerable<PX.Objects.AR.ARInvoice>) list, adapter.MassProcess, (System.Action<PX.Objects.AR.ARInvoice>) (doc =>
      {
        parameters = new Dictionary<string, string>();
        parameters["ARInvoice.DocType"] = doc.DocType;
        parameters["ARInvoice.RefNbr"] = doc.RefNbr;
        actualReportID = new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, doc.CustomerID, doc.BranchID);
        ex = PXReportRequiredException.CombineReport(ex, actualReportID, parameters, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
        ((PXBaseRedirectException) ex).Mode = (PXBaseRedirectException.WindowMode) 2;
        reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, parameters, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, actualReportID, doc.BranchID, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
      }), (System.Action<PX.Objects.AR.ARInvoice>) null, (Func<PX.Objects.AR.ARInvoice, Exception, bool, bool?>) null, (System.Action<PX.Objects.AR.ARInvoice>) null, (System.Action<PX.Objects.AR.ARInvoice>) null);
      if (ex != null)
      {
        int num;
        ((ILongOperationManager) ((PXGraph) this).LongOperationManager).StartAsyncOperation((object) Guid.NewGuid(), (Func<CancellationToken, System.Threading.Tasks.Task>) (async ct => num = await SMPrintJobMaint.CreatePrintJobGroups(reportsToPrint, ct) ? 1 : 0));
        throw ex;
      }
    }
    return (IEnumerable) list;
  }

  [PXButton]
  [PXUIField]
  public override IEnumerable PrintInvoice(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter.Apply<PXAdapter>((System.Action<PXAdapter>) (it => it.Menu = "Print Invoice")), reportID ?? "SO643000");
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable AREdit(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter.Apply<PXAdapter>((System.Action<PXAdapter>) (it => it.Menu = "AR Edit")), reportID ?? "AR610500");
  }

  [PXButton]
  [PXUIField]
  public override IEnumerable EmailInvoice(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "SO INVOICE");
  }

  [PXButton]
  [PXUIField]
  public override IEnumerable ReverseInvoice(PXAdapter adapter)
  {
    throw new PXInvalidOperationException();
  }

  public SOInvoiceEntry()
  {
    SOSetup current = ((PXSelectBase<SOSetup>) this.sosetup).Current;
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXSelectBase) this.Document).View = new PXView((PXGraph) this, false, (BqlCommand) new Select2<PX.Objects.AR.ARInvoice, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.ARInvoice.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>>());
    ((PXGraph) this).Views["Document"] = ((PXSelectBase) this.Document).View;
    BqlCommand bqlCommand1 = ((PXSelectBase) this.Transactions).View.BqlSelect.WhereNew<Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.lineType, NotEqual<SOLineType.discount>, And<PX.Objects.AR.ARTran.lineType, NotEqual<SOLineType.freight>>>>>>();
    PXOrderedSelect<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<Where<PX.Objects.AR.ARTran.lineType, IsNull, Or<PX.Objects.AR.ARTran.lineType, NotEqual<SOLineType.discount>>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.sortOrder, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>> transactions = this.Transactions;
    BqlCommand bqlCommand2 = bqlCommand1;
    SOInvoiceEntry soInvoiceEntry = this;
    // ISSUE: virtual method pointer
    PXSelectDelegate pxSelectDelegate = new PXSelectDelegate((object) soInvoiceEntry, __vmethodptr(soInvoiceEntry, transactions));
    PXView pxView = new PXView((PXGraph) this, false, bqlCommand2, (Delegate) pxSelectDelegate);
    ((PXSelectBase) transactions).View = pxView;
    ((PXGraph) this).Views["Transactions"] = ((PXSelectBase) this.Transactions).View;
    PXUIFieldAttribute.SetVisible<SOOrderShipment.orderType>(((PXSelectBase) this.shipmentlist).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<SOOrderShipment.orderNbr>(((PXSelectBase) this.shipmentlist).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<SOOrderShipment.shipmentNbr>(((PXSelectBase) this.shipmentlist).Cache, (object) null, true);
    PXDBDefaultAttribute.SetDefaultForInsert<SOOrderShipment.invoiceNbr>(((PXSelectBase) this.shipmentlist).Cache, (object) null, true);
    PXDBDefaultAttribute.SetDefaultForUpdate<SOOrderShipment.invoiceNbr>(((PXSelectBase) this.shipmentlist).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<ARAdjust2.curyAdjgDiscAmt>(((PXSelectBase) this.Adjustments).Cache, (object) null, false);
    TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
    ((PXOrderedSelectBase<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>) this.Transactions).CustomComparer = (IComparer<PXResult>) Comparer<PXResult>.Create((Comparison<PXResult>) ((a, b) =>
    {
      PX.Objects.AR.ARTran arTran1 = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) a);
      PX.Objects.AR.ARTran arTran2 = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) b);
      return string.Compare($"{arTran1.SOOrderType}.{arTran1.SOOrderNbr}.{arTran1.SOOrderSortOrder:D7}.{arTran1.SOShipmentNbr}.{arTran1.SOShipmentLineGroupNbr:D7}", $"{arTran2.SOOrderType}.{arTran2.SOOrderNbr}.{arTran2.SOOrderSortOrder:D7}.{arTran2.SOShipmentNbr}.{arTran2.SOShipmentLineGroupNbr:D7}");
    }));
    PXAction action = ((PXGraph) this).Actions["ReleaseRetainage"];
    if (action != null)
    {
      ((PXAction) this.action).AddMenuAction(action);
      ((PXAction) this.action).SetVisible("ReleaseRetainage", false);
    }
    this.Approval.SuppressInFull = true;
    ((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).OrderByNew<OrderBy<Asc<ARInvoiceDiscountDetail.orderType, Asc<ARInvoiceDiscountDetail.orderNbr, Asc<ARInvoiceDiscountDetail.lineNbr>>>>>();
    ((PXGraph) this).Views.Caches.Add(typeof (NoteDoc));
  }

  protected virtual InvoicePostingContext GetInvoicePostingContext()
  {
    return new InvoicePostingContext(this.FinPeriodUtils);
  }

  protected override void RecalculateDiscountsProc(bool redirect)
  {
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
    try
    {
      base.RecalculateDiscountsProc(redirect);
    }
    finally
    {
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
    }
  }

  protected virtual void RecalculatePricesAndDiscountsOnPersist(IEnumerable<PX.Objects.AR.ARInvoice> docs)
  {
    foreach (PX.Objects.AR.ARInvoice doc in docs.Where<PX.Objects.AR.ARInvoice>((Func<PX.Objects.AR.ARInvoice, bool>) (doc =>
    {
      if (!doc.DeferPriceDiscountRecalculation.GetValueOrDefault())
        return false;
      bool? andDiscountsValid = doc.IsPriceAndDiscountsValid;
      bool flag = false;
      return andDiscountsValid.GetValueOrDefault() == flag & andDiscountsValid.HasValue;
    })))
    {
      if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current != doc)
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current = doc;
      TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc | TaxCalc.RecalculateAlways);
      doc.DeferPriceDiscountRecalculation = new bool?(false);
      try
      {
        this.ARDiscountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions, (PX.Objects.AR.ARTran) null, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocDate, this.GetDefaultARDiscountCalculationOptions(doc, true));
        doc.IsPriceAndDiscountsValid = new bool?(true);
      }
      finally
      {
        doc.DeferPriceDiscountRecalculation = new bool?(true);
      }
    }
  }

  public override void Persist()
  {
    this.CopyFreightNotesAndFilesToARTran();
    if (((PXGraph) this).Caches[typeof (SOOrderShipment)] != null)
    {
      foreach (SOOrderShipment soOrderShipment in ((PXGraph) this).Caches[typeof (SOOrderShipment)].Cached)
      {
        bool? nullable = soOrderShipment.HasUnhandledErrors;
        nullable = !nullable.GetValueOrDefault() ? soOrderShipment.IsPartialInvoiceConstraintViolated : throw new PXException("The document cannot be saved because one or more errors have occurred during adding the line. Cancel the changes and verify the details of the sales order {0} and shipment {1}.", new object[2]
        {
          (object) soOrderShipment.OrderNbr,
          (object) soOrderShipment.ShipmentNbr
        });
        if (nullable.GetValueOrDefault())
          throw new PXException("Sales Order/Shipment cannot be invoiced partially.");
      }
    }
    this.RecalculatePricesAndDiscountsOnPersist(NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Document).Cache.Inserted, ((PXSelectBase) this.Document).Cache.Updated).Cast<PX.Objects.AR.ARInvoice>());
    PXCache cach = ((PXGraph) this).Caches[typeof (SOLine2)];
    foreach (SOLine2 soLine2 in cach.Updated)
      PXTimeStampScope.DuplicatePersisted(cach, (object) soLine2, typeof (SOLine));
    this.DeleteZeroAdjustments();
    this.VerifyDocumentBalanceAgainstAdjustmentBalance();
    ((PXOrderedSelectBase<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>) this.Transactions).RenumberAllBeforePersist = LinesSortingAttribute.AllowSorting<PX.Objects.SO.SOInvoice>(((PXSelectBase) this.Transactions).Cache, ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current);
    base.Persist();
  }

  protected virtual void DeleteZeroAdjustments()
  {
    foreach (ARAdjust2 arAdjust2 in ((PXSelectBase) this.Adjustments).Cache.Inserted)
    {
      Decimal? curyAdjdAmt = arAdjust2.CuryAdjdAmt;
      Decimal num = 0M;
      if (curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue && !arAdjust2.Recalculatable.GetValueOrDefault())
        ((PXSelectBase) this.Adjustments).Cache.SetStatus((object) arAdjust2, (PXEntryStatus) 4);
    }
    foreach (object obj in GraphHelper.RowCast<ARAdjust2>(((PXSelectBase) this.Adjustments).Cache.Updated).Where<ARAdjust2>((Func<ARAdjust2, bool>) (adj =>
    {
      Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
      Decimal num = 0M;
      return curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue && !adj.Recalculatable.GetValueOrDefault();
    })))
      ((PXSelectBase) this.Adjustments).Cache.SetStatus(obj, (PXEntryStatus) 3);
  }

  protected virtual void VerifyDocumentBalanceAgainstAdjustmentBalance()
  {
    foreach (PX.Objects.AR.ARInvoice arInvoice in ((PXSelectBase) this.Document).Cache.Cached.Cast<PX.Objects.AR.ARInvoice>().Where<PX.Objects.AR.ARInvoice>((Func<PX.Objects.AR.ARInvoice, bool>) (ardoc =>
    {
      if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Document).Cache.GetStatus((object) ardoc), (PXEntryStatus) 2, (PXEntryStatus) 1) && ardoc.DocType == "INV")
      {
        bool? nullable = ardoc.Released;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          nullable = ardoc.ApplyPaymentWhenTaxAvailable;
          return !nullable.GetValueOrDefault();
        }
      }
      return false;
    })))
    {
      PX.Objects.AR.ARInvoice arDoc = arInvoice;
      PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(new object[2]
      {
        (object) arDoc.DocType,
        (object) arDoc.RefNbr
      }));
      Decimal? curyDocBal1 = arDoc.CuryDocBal;
      Decimal? nullable1 = arDoc.CuryBalanceWOTotal;
      Decimal? nullable2 = curyDocBal1.HasValue & nullable1.HasValue ? new Decimal?(curyDocBal1.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? curyPaymentTotal = arDoc.CuryPaymentTotal;
      Decimal? nullable3;
      if (!(nullable2.HasValue & curyPaymentTotal.HasValue))
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(nullable2.GetValueOrDefault() - curyPaymentTotal.GetValueOrDefault());
      Decimal? nullable4 = nullable3;
      Decimal num = 0M;
      if (nullable4.GetValueOrDefault() < num & nullable4.HasValue)
      {
        using (IEnumerator<ARAdjust2> enumerator = GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase) this.Adjustments_Inv).View.SelectMultiBound(new object[1]
        {
          (object) arDoc
        }, Array.Empty<object>())).Where<ARAdjust2>((Func<ARAdjust2, bool>) (adj =>
        {
          if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Adjustments).Cache.GetStatus((object) adj), (PXEntryStatus) 2, (PXEntryStatus) 1))
            return true;
          Decimal? valueOriginal = (Decimal?) ((PXSelectBase) this.Document).Cache.GetValueOriginal<PX.Objects.AR.ARInvoice.curyDocBal>((object) arDoc);
          Decimal? curyDocBal2 = arDoc.CuryDocBal;
          return !(valueOriginal.GetValueOrDefault() == curyDocBal2.GetValueOrDefault() & valueOriginal.HasValue == curyDocBal2.HasValue);
        })).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            ARAdjust2 current = enumerator.Current;
            GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments).Cache, (object) current, true);
            ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust2.curyAdjdAmt>((object) current, (object) current.CuryAdjdAmt, (Exception) new PXSetPropertyException("The total application amount must not exceed the document amount."));
            throw new PXException("The total application amount must not exceed the document amount.");
          }
        }
      }
    }
  }

  public override PX.Objects.AR.ARInvoice InsertReversalARInvoice(PX.Objects.AR.ARInvoice arInvoice)
  {
    if (arInvoice.DocType != "INV" && arInvoice.RefNbr == null)
      arInvoice.RefNbr = arInvoice.OrigRefNbr;
    return base.InsertReversalARInvoice(arInvoice);
  }

  public override PX.Objects.AR.ARInvoice SetRefNumber(PX.Objects.AR.ARInvoice arInvoice, string refNbr)
  {
    PX.Objects.SO.SOInvoice current = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current;
    current.RefNbr = refNbr;
    ((PXSelectBase) this.SODocument).Cache.Normalize();
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Update(current);
    return base.SetRefNumber(arInvoice, refNbr);
  }

  public override void RecalcUnbilledTax()
  {
    IEnumerable<\u003C\u003Ef__AnonymousType105<string, string>> datas = ((IEnumerable<PX.Objects.AR.ARTran>) ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).SelectMain(Array.Empty<object>())).Where<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (o => o.SOOrderType != null && o.SOOrderNbr != null)).Select(o => new
    {
      SOOrderType = o.SOOrderType,
      SOOrderNbr = o.SOOrderNbr
    }).Distinct();
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    foreach (var data in datas)
    {
      ((PXGraph) instance).Clear((PXClearOption) 3);
      ((PXSelectBase<SOOrder>) instance.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) instance.Document).Search<SOOrder.orderNbr>((object) data.SOOrderNbr, new object[1]
      {
        (object) data.SOOrderType
      }));
      if (!((PXSelectBase<SOOrder>) instance.Document).Current.IsUnbilledTaxValid.GetValueOrDefault())
      {
        if (this.IsExternalTax(((PXSelectBase<SOOrder>) instance.Document).Current.TaxZoneID))
          instance.CalculateExternalTax(((PXSelectBase<SOOrder>) instance.Document).Current);
        ((PXGraph) instance).Persist();
      }
    }
  }

  protected override void ARInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) e.Row;
    if (e.Operation != 3 && (row.DocType == "CSL" || row.DocType == "RCS"))
      this.ValidateTaxConfiguration(sender, row);
    if ((e.Operation & 3) == 2)
    {
      SOOrderShipment soOrderShipment = PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.invoiceType, Equal<Required<SOOrderShipment.invoiceType>>, And<SOOrderShipment.invoiceNbr, Equal<Required<SOOrderShipment.invoiceNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
      {
        (object) row.DocType,
        (object) row.RefNbr
      }));
      if (soOrderShipment != null)
      {
        SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this, soOrderShipment.OrderType);
        if (soOrderType != null)
        {
          bool? nullable;
          if (string.IsNullOrEmpty(row.RefNbr))
          {
            nullable = soOrderType.UserInvoiceNumbering;
            if (nullable.GetValueOrDefault())
              throw new PXException("'{0}' cannot be empty.", new object[1]
              {
                (object) PXUIFieldAttribute.GetDisplayName<SOOrder.invoiceNbr>(((PXSelectBase) this.soorder).Cache)
              });
          }
          nullable = soOrderType.MarkInvoicePrinted;
          if (nullable.GetValueOrDefault())
            row.Printed = new bool?(true);
          nullable = soOrderType.MarkInvoiceEmailed;
          if (nullable.GetValueOrDefault())
            row.Emailed = new bool?(true);
          AutoNumberAttribute.SetNumberingId<PX.Objects.AR.ARInvoice.refNbr>(((PXSelectBase) this.Document).Cache, soOrderType.ARDocType, soOrderType.InvoiceNumberingID);
        }
      }
    }
    if (e.Operation == 2 || e.Operation == 1)
    {
      Decimal? nullable = row.CuryDiscTot;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = row.CuryGoodsTotal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      nullable = row.CuryMiscTot;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      Decimal num = Math.Abs(valueOrDefault2 + valueOrDefault3);
      if (valueOrDefault1 > num && sender.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.curyDiscTot>(e.Row, (object) row.CuryDiscTot, (Exception) new PXSetPropertyException("The total amount of line and document discounts cannot exceed the Detail Total amount.", (PXErrorLevel) 4)))
        throw new PXRowPersistingException(typeof (PX.Objects.AR.ARInvoice.curyDiscTot).Name, (object) null, "The total amount of line and document discounts cannot exceed the Detail Total amount.");
    }
    base.ARInvoice_RowPersisting(sender, e);
  }

  protected virtual void ARInvoice_OrigModule_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "SO";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected override void ARInvoice_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    base.ARInvoice_RowInserted(sender, e);
    PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) e.Row;
    ((PXSelectBase) this.SODocument).Cache.Insert();
    ((PXSelectBase) this.SODocument).Cache.IsDirty = false;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.AdjDate = row.DocDate;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.AdjFinPeriodID = row.FinPeriodID;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.AdjTranPeriodID = row.TranPeriodID;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.NoteID = row.NoteID;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.CuryID = row.CuryID;
  }

  protected override void ARTran_CuryUnitPrice_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this.cancelUnitPriceCalculation)
      return;
    base.ARTran_CuryUnitPrice_FieldVerifying(sender, e);
  }

  protected override void ARTran_CuryUnitPrice_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    if (row != null && row.InventoryID.HasValue && row.UOM != null)
    {
      bool? nullable1 = row.IsFree;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.ManualPrice;
        if (!nullable1.GetValueOrDefault() && !this.cancelUnitPriceCalculation)
        {
          string custPriceClass = "BASE";
          PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
          if (!string.IsNullOrEmpty(location?.CPriceClassID))
            custPriceClass = location.CPriceClassID;
          DateTime? nullable2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocDate;
          DateTime date = nullable2.Value;
          string taxCalcMode = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxCalcMode;
          if (row.TranType == "CRM")
          {
            nullable2 = row.OrigInvoiceDate;
            if (nullable2.HasValue)
            {
              nullable2 = row.OrigInvoiceDate;
              date = nullable2.Value;
            }
          }
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
          (ARSalesPriceMaint.SalesPriceItem salesPriceItem, Decimal? newValue) = ARSalesPriceMaint.SingleARSalesPriceMaint.GetSalesPriceItemAndCalculatedPrice(cache, custPriceClass, row.CustomerID, row.InventoryID, row.LotSerialNbr, row.SiteID, currencyInfo.GetCM(), row.UOM, row.Qty, date, row.CuryUnitPrice, taxCalcMode);
          PX.Objects.AR.ARTran arTran = row;
          bool? nullable3;
          if (salesPriceItem == null)
          {
            nullable1 = new bool?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new bool?(salesPriceItem.SkipLineDiscounts);
          arTran.SkipLineDiscountsBuffer = nullable3;
          e.NewValue = (object) newValue;
          ARSalesPriceMaint.CheckNewUnitPrice<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.curyUnitPrice>(cache, row, (object) newValue);
          return;
        }
      }
    }
    Decimal? curyUnitPrice = row.CuryUnitPrice;
    e.NewValue = (object) curyUnitPrice.GetValueOrDefault();
    ((CancelEventArgs) e).Cancel = curyUnitPrice.HasValue;
  }

  private (ARSalesPriceMaint.SalesPriceItem, Decimal?) GetSalesPriceItemAndCalculatedPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode)
  {
    ARSalesPriceMaint arSalesPriceMaint = ARSalesPriceMaint.SingleARSalesPriceMaint;
    bool baseCurrencySetting = arSalesPriceMaint.GetAlwaysFromBaseCurrencySetting(sender);
    ARSalesPriceMaint.SalesPriceItem salesPriceItem = arSalesPriceMaint.CalculateSalesPriceItem(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), UOM, date, baseCurrencySetting, false, taxCalcMode);
    Decimal? nullable = arSalesPriceMaint.AdjustSalesPrice(sender, salesPriceItem, inventoryID, currencyinfo, UOM);
    return (salesPriceItem, new Decimal?(nullable.GetValueOrDefault()));
  }

  [PXFormula(typeof (Current<SOSetup.deferPriceDiscountRecalculation>))]
  [PXMergeAttributes]
  public void ARInvoice_DeferPriceDiscountRecalculation_CacheAttached(PXCache sender)
  {
  }

  protected override void ARInvoice_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    bool? requireControlTotal = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal;
    PX.Objects.AR.ARInvoice row = e.Row as PX.Objects.AR.ARInvoice;
    PX.Objects.AR.ARInvoice oldRow = e.OldRow as PX.Objects.AR.ARInvoice;
    if (row.DocType == "CSL" || row.DocType == "RCS")
      ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal = new bool?(true);
    bool flag = !sender.ObjectsEqual<PX.Objects.AR.ARInvoice.docDate>((object) oldRow, (object) row);
    try
    {
      if (flag)
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DisableAutomaticDiscountCalculation = new bool?(true);
      base.ARInvoice_RowUpdated(sender, e);
    }
    finally
    {
      if (flag)
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DisableAutomaticDiscountCalculation = new bool?(false);
      ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal = requireControlTotal;
    }
    if (row != null && row.RefNbr == null)
      return;
    if (row.DeferPriceDiscountRecalculation.GetValueOrDefault() && !sender.ObjectsEqual<PX.Objects.AR.ARInvoice.taxZoneID>(e.OldRow, (object) row))
      row.IsPriceAndDiscountsValid = new bool?(false);
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    if ((row.DocType == "CSL" || row.DocType == "RCS") && !row.Released.GetValueOrDefault())
    {
      if (!sender.ObjectsEqual<PX.Objects.AR.ARInvoice.curyDocBal, PX.Objects.AR.ARInvoice.curyOrigDiscAmt>(e.Row, e.OldRow))
      {
        Decimal? curyDocBal1 = row.CuryDocBal;
        nullable1 = row.CuryOrigDiscAmt;
        nullable2 = curyDocBal1.HasValue & nullable1.HasValue ? new Decimal?(curyDocBal1.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal? curyOrigDocAmt = row.CuryOrigDocAmt;
        if (!(nullable2.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & nullable2.HasValue == curyOrigDocAmt.HasValue))
        {
          if (row.CuryDocBal.HasValue && row.CuryOrigDiscAmt.HasValue)
          {
            Decimal? curyDocBal2 = row.CuryDocBal;
            Decimal num = 0M;
            if (!(curyDocBal2.GetValueOrDefault() == num & curyDocBal2.HasValue))
            {
              PXCache pxCache = sender;
              PX.Objects.AR.ARInvoice arInvoice = row;
              nullable3 = row.CuryDocBal;
              nullable2 = row.CuryOrigDiscAmt;
              Decimal? nullable4;
              if (!(nullable3.HasValue & nullable2.HasValue))
              {
                nullable1 = new Decimal?();
                nullable4 = nullable1;
              }
              else
                nullable4 = new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault());
              // ISSUE: variable of a boxed type
              __Boxed<Decimal?> local = (ValueType) nullable4;
              pxCache.SetValueExt<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) arInvoice, (object) local);
              goto label_32;
            }
          }
          sender.SetValueExt<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) row, (object) 0M);
          goto label_32;
        }
      }
      if (!sender.ObjectsEqual<PX.Objects.AR.ARInvoice.curyOrigDocAmt>(e.Row, e.OldRow))
      {
        nullable2 = row.CuryDocBal;
        if (nullable2.HasValue)
        {
          nullable2 = row.CuryOrigDocAmt;
          if (nullable2.HasValue)
          {
            nullable2 = row.CuryDocBal;
            Decimal num = 0M;
            if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
            {
              PXCache pxCache = sender;
              PX.Objects.AR.ARInvoice arInvoice = row;
              nullable2 = row.CuryDocBal;
              Decimal? curyOrigDocAmt = row.CuryOrigDocAmt;
              Decimal? nullable5;
              if (!(nullable2.HasValue & curyOrigDocAmt.HasValue))
              {
                nullable1 = new Decimal?();
                nullable5 = nullable1;
              }
              else
                nullable5 = new Decimal?(nullable2.GetValueOrDefault() - curyOrigDocAmt.GetValueOrDefault());
              // ISSUE: variable of a boxed type
              __Boxed<Decimal?> local = (ValueType) nullable5;
              pxCache.SetValueExt<PX.Objects.AR.ARInvoice.curyOrigDiscAmt>((object) arInvoice, (object) local);
              goto label_32;
            }
          }
        }
        sender.SetValueExt<PX.Objects.AR.ARInvoice.curyOrigDiscAmt>((object) row, (object) 0M);
      }
    }
label_32:
    if (row != null)
    {
      nullable3 = row.CuryDocBal;
      if (nullable3.HasValue && !row.Hold.GetValueOrDefault())
      {
        nullable3 = row.CuryDocBal;
        Decimal num1 = 0M;
        if (nullable3.GetValueOrDefault() < num1 & nullable3.HasValue && ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current != null)
        {
          nullable3 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryPremiumFreightAmt;
          Decimal num2 = 0M;
          if (nullable3.GetValueOrDefault() < num2 & nullable3.HasValue)
          {
            nullable2 = row.CuryDocBal;
            nullable1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryPremiumFreightAmt;
            nullable3 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
            Decimal num3 = 0M;
            if (nullable3.GetValueOrDefault() >= num3 & nullable3.HasValue)
              sender.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.curyDocBal>((object) row, (object) row.CuryDocBal, (Exception) new PXSetPropertyException("Negative Premium Freight is greater than Document Balance. Document Balance will go negative."));
          }
        }
      }
    }
    if ((row.DocType == "CSL" || row.DocType == "RCS") && !row.Released.GetValueOrDefault() && !row.Hold.GetValueOrDefault())
    {
      nullable3 = row.CuryDocBal;
      nullable1 = row.CuryOrigDocAmt;
      if (nullable3.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable3.HasValue & nullable1.HasValue)
        sender.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The payment amount should be less than or equal to the invoice amount."));
      else
        sender.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) null);
    }
    Decimal? nullable6;
    if (!sender.ObjectsEqual<PX.Objects.AR.ARInvoice.customerID, PX.Objects.AR.ARInvoice.docDate, PX.Objects.AR.ARInvoice.finPeriodID, PX.Objects.AR.ARInvoice.curyTaxTotal, PX.Objects.AR.ARInvoice.curyOrigDocAmt, PX.Objects.AR.ARInvoice.docDesc, PX.Objects.AR.ARInvoice.curyOrigDiscAmt, PX.Objects.AR.ARInvoice.hold>(e.Row, e.OldRow))
    {
      PX.Objects.SO.SOInvoice soInvoice = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(Array.Empty<object>()));
      if (((PXGraph) this).IsImport && soInvoice == null && ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current != null && sender.Current is PX.Objects.AR.ARInvoice && (((PX.Objects.AR.ARRegister) sender.Current).DocType != ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.DocType || ((PX.Objects.AR.ARRegister) sender.Current).RefNbr != ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.RefNbr) && ((PXSelectBase) this.SODocument).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current) == 2)
      {
        PXCache pxCache = sender;
        PX.Objects.AR.ARInvoice arInvoice = new PX.Objects.AR.ARInvoice();
        arInvoice.DocType = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.DocType;
        arInvoice.RefNbr = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.RefNbr;
        if (pxCache.Locate((object) arInvoice) == null)
          ((PXSelectBase) this.SODocument).Cache.Delete((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current);
      }
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current = soInvoice ?? (PX.Objects.SO.SOInvoice) ((PXSelectBase) this.SODocument).Cache.Insert();
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.CustomerID = row.CustomerID;
      if (EnumerableExtensions.IsIn<string>(row.DocType, "CSL", "RCS", "INV", "CRM") && !sender.ObjectsEqual<PX.Objects.AR.ARInvoice.customerID>(e.Row, e.OldRow))
      {
        ((PXSelectBase) this.SODocument).Cache.SetDefaultExt<PX.Objects.SO.SOInvoice.paymentMethodID>((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current);
        ((PXSelectBase) this.SODocument).Cache.SetDefaultExt<PX.Objects.SO.SOInvoice.pMInstanceID>((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current);
      }
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.AdjDate = row.DocDate;
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.DepositAfter = row.DocDate;
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.AdjFinPeriodID = row.FinPeriodID;
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.AdjTranPeriodID = row.TranPeriodID;
      PX.Objects.SO.SOInvoice current = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current;
      nullable2 = row.CuryOrigDocAmt;
      nullable6 = row.CuryOrigDiscAmt;
      nullable1 = nullable2.HasValue & nullable6.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
      nullable3 = row.CuryPaymentTotal;
      Decimal? nullable7;
      if (!(nullable1.HasValue & nullable3.HasValue))
      {
        nullable6 = new Decimal?();
        nullable7 = nullable6;
      }
      else
        nullable7 = new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault());
      current.CuryPaymentAmt = nullable7;
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.DocDesc = row.DocDesc;
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.CuryID = row.CuryID;
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.PaymentProjectID = ProjectDefaultAttribute.NonProject();
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.Hold = row.Hold;
      GraphHelper.MarkUpdated(((PXSelectBase) this.SODocument).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, true);
    }
    if (!sender.ObjectsEqual<PX.Objects.AR.ARInvoice.curyPaymentTotal>(e.OldRow, e.Row))
    {
      PX.Objects.SO.SOInvoice soInvoice = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(Array.Empty<object>()));
      if (soInvoice != null)
      {
        ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current = soInvoice;
        PX.Objects.SO.SOInvoice current = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current;
        nullable6 = row.CuryOrigDocAmt;
        nullable2 = row.CuryOrigDiscAmt;
        nullable3 = nullable6.HasValue & nullable2.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        nullable1 = row.CuryPaymentTotal;
        Decimal? nullable8;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable2 = new Decimal?();
          nullable8 = nullable2;
        }
        else
          nullable8 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
        current.CuryPaymentAmt = nullable8;
      }
    }
    if (!e.ExternalCall || sender.GetStatus((object) row) == 3 || sender.ObjectsEqual<PX.Objects.AR.ARInvoice.curyDiscTot>(e.OldRow, e.Row))
      return;
    this.ARDiscountEngine.SetTotalDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryDiscTot, DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation);
    this.RecalculateTotalDiscount();
  }

  protected override void ApplyDocumentState(PXCache cache, PX.Objects.AR.ARInvoice doc, ARInvoiceState state)
  {
    base.ApplyDocumentState(cache, doc, state);
    if (((PXSelectBase<SOSetup>) this.sosetup).Current.DeferPriceDiscountRecalculation.GetValueOrDefault())
      TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc | TaxCalc.RedefaultAlways);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARTran.taskID>(((PXSelectBase) this.Transactions).Cache, (object) null, ProjectAttribute.IsPMVisible("SO") || ProjectAttribute.IsPMVisible("AR"));
    ((PXAction) this.selectShipment).SetEnabled(((PXSelectBase) this.Transactions).AllowInsert);
    if (doc == null)
      return;
    bool flag1 = doc != null && EnumerableExtensions.IsIn<string>(doc.DocType, "CSL", "RCS");
    cache.Graph.Actions["Processing Category"]?.SetVisible("PayInvoice", !flag1);
    cache.Graph.Actions["Corrections Category"]?.SetVisible("WriteOff", !flag1);
    if (flag1)
      PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARInvoice.curyOrigDocAmt>(cache, (object) doc);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) null).For<PX.Objects.AR.ARInvoice.curyPaymentTotal>((System.Action<PXUIFieldAttribute>) (a => a.Visible = EnumerableExtensions.IsNotIn<string>(doc.DocType, "RCS", "CSL")));
    chained = chained.SameFor<PX.Objects.AR.ARInvoice.curyUnreleasedPaymentAmt>();
    chained = chained.SameFor<PX.Objects.AR.ARInvoice.curyPaidAmt>();
    chained = chained.SameFor<PX.Objects.AR.ARInvoice.curyUnpaidBalance>();
    chained = chained.SameFor<PX.Objects.AR.ARInvoice.curyBalanceWOTotal>();
    chained.For<PX.Objects.AR.ARInvoice.curyCCAuthorizedAmt>((System.Action<PXUIFieldAttribute>) (a => a.Visible = EnumerableExtensions.IsNotIn<string>(doc.DocType, "CRM", "RCS", "CSL")));
    ((PXSelectBase) this.SODocument).Cache.AllowUpdate = ((PXSelectBase) this.Document).Cache.AllowUpdate;
    ((PXSelectBase) this.FreightDetails).Cache.AllowUpdate = ((PXSelectBase) this.Document).Cache.AllowUpdate && ((PXSelectBase) this.Transactions).Cache.AllowUpdate;
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState(cache.Graph, (PXSelectBase<PX.Objects.AR.ExternalTransaction>) this.ExternalTran);
    bool isCaptured = transactionState.IsCaptured;
    bool isRefunded = transactionState.IsRefunded;
    bool isPreAuthorized = transactionState.IsPreAuthorized;
    bool flag2 = state.IsDocumentRejectedOrPendingApproval || state.IsDocumentApprovedBalanced;
    bool flag3 = doc.DocType == "CSL" && isPreAuthorized | isCaptured;
    bool flag4 = doc.DocType == "RCS" & isRefunded;
    PXCache cache1 = ((PXSelectBase) this.Transactions).Cache;
    cache1.AllowDelete = ((cache1.AllowDelete ? 1 : 0) & (flag3 ? 0 : (!flag4 ? 1 : 0))) != 0;
    PXCache cache2 = ((PXSelectBase) this.Transactions).Cache;
    cache2.AllowUpdate = ((cache2.AllowUpdate ? 1 : 0) & (flag3 ? 0 : (!flag4 ? 1 : 0))) != 0;
    PXCache cache3 = ((PXSelectBase) this.Transactions).Cache;
    cache3.AllowInsert = ((cache3.AllowInsert ? 1 : 0) & (flag3 ? 0 : (!flag4 ? 1 : 0))) != 0;
    PXCache pxCache1 = cache;
    PX.Objects.AR.ARInvoice arInvoice1 = doc;
    bool? nullable = doc.Released;
    bool flag5 = false;
    int num1 = !(nullable.GetValueOrDefault() == flag5 & nullable.HasValue) || flag2 || flag3 ? 0 : (!flag4 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARInvoice.curyOrigDocAmt>(pxCache1, (object) arInvoice1, num1 != 0);
    PXCache pxCache2 = cache;
    PX.Objects.AR.ARInvoice arInvoice2 = doc;
    nullable = doc.Released;
    bool flag6 = false;
    int num2;
    if (nullable.GetValueOrDefault() == flag6 & nullable.HasValue && !flag2)
    {
      if (!(doc.DocType != "CRM"))
      {
        if (doc.DocType == "CRM")
        {
          nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.arsetup).Current.TermsInCreditMemos;
          if (!nullable.GetValueOrDefault())
            goto label_16;
        }
        else
          goto label_16;
      }
      if (!flag3)
      {
        num2 = !flag4 ? 1 : 0;
        goto label_17;
      }
    }
label_16:
    num2 = 0;
label_17:
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARInvoice.curyOrigDiscAmt>(pxCache2, (object) arInvoice2, num2 != 0);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARTran.sOOrderNbr>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARTran.sOOrderType>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    ((PXSelectBase) this.Adjustments).Cache.AllowSelect = !flag1;
    nullable = doc.DisableAutomaticTaxCalculation;
    if (!nullable.GetValueOrDefault())
      return;
    TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
  }

  protected override void DisableCreditHoldActions(PXCache cache, PX.Objects.AR.ARInvoice doc)
  {
    ((PXAction) this.putOnCreditHold).SetEnabled(EnumerableExtensions.IsNotIn<string>(doc.DocType, "CRM", "CSL", "RCS") && !this.SOApproval.GetAssignedMaps(doc, cache).Any<PX.Objects.EP.ApprovalMap>());
  }

  public override bool IsApprovalRequired(PX.Objects.AR.ARInvoice doc)
  {
    return EPApprovalSettings<SOSetupInvoiceApproval, SOSetupInvoiceApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.ApprovableDocTypes.Contains(doc.DocType);
  }

  protected override bool IsWarehouseVisible(PX.Objects.AR.ARInvoice doc) => true;

  public static bool IsDocTypeSuitableForCC(string docType)
  {
    return docType == "INV" || docType == "RCS" || docType == "CSL" || docType == "REF";
  }

  protected override void ARInvoice_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    string creditRule = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current?.CreditRule;
    try
    {
      base.ARInvoice_CustomerID_FieldUpdated(sender, e);
    }
    finally
    {
      if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null)
        ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CreditRule = creditRule;
    }
  }

  protected virtual void SOInvoice_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOInvoice row1 = (PX.Objects.SO.SOInvoice) e.Row;
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
    row1.PaymentProjectID = ProjectDefaultAttribute.NonProject();
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARInvoice.curyDiscTot>(((PXSelectBase) this.SODocument).Cache, e.Row, ((PXSelectBase) this.Document).Cache.AllowUpdate);
    PXCache cache1 = ((PXSelectBase) this.SODocument).Cache;
    object row2 = e.Row;
    bool? isCancellation;
    int num1;
    if (((PXSelectBase) this.Document).Cache.AllowUpdate)
    {
      if (current != null)
      {
        isCancellation = current.IsCancellation;
        if (isCancellation.GetValueOrDefault())
          goto label_5;
      }
      num1 = ((PX.Objects.SO.SOInvoice) e.Row).PMInstanceID.HasValue ? 1 : (!string.IsNullOrEmpty(row1.PaymentMethodID) ? 1 : 0);
      goto label_6;
    }
label_5:
    num1 = 0;
label_6:
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOInvoice.cashAccountID>(cache1, row2, num1 != 0);
    PXCache cache2 = ((PXSelectBase) this.SODocument).Cache;
    object row3 = e.Row;
    int num2;
    if (((PXSelectBase) this.Document).Cache.AllowUpdate)
    {
      if (current != null)
      {
        isCancellation = current.IsCancellation;
        if (isCancellation.GetValueOrDefault())
          goto label_10;
      }
      num2 = ((PX.Objects.SO.SOInvoice) e.Row).PMInstanceID.HasValue ? 1 : (!string.IsNullOrEmpty(row1.PaymentMethodID) ? 1 : 0);
      goto label_11;
    }
label_10:
    num2 = 0;
label_11:
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOInvoice.extRefNbr>(cache2, row3, num2 != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOInvoice.cleared>(((PXSelectBase) this.SODocument).Cache, e.Row, ((PXSelectBase) this.Document).Cache.AllowUpdate && (((PX.Objects.SO.SOInvoice) e.Row).PMInstanceID.HasValue || !string.IsNullOrEmpty(row1.PaymentMethodID)) && (((PX.Objects.SO.SOInvoice) e.Row).DocType == "CSL" || ((PX.Objects.SO.SOInvoice) e.Row).DocType == "RCS"));
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOInvoice.clearDate>(((PXSelectBase) this.SODocument).Cache, e.Row, ((PXSelectBase) this.Document).Cache.AllowUpdate && (((PX.Objects.SO.SOInvoice) e.Row).PMInstanceID.HasValue || !string.IsNullOrEmpty(row1.PaymentMethodID)) && (((PX.Objects.SO.SOInvoice) e.Row).DocType == "CSL" || ((PX.Objects.SO.SOInvoice) e.Row).DocType == "RCS"));
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOInvoice.extRefNbr>(((PXSelectBase) this.SODocument).Cache, e.Row, row1.DocType == "CSL" || row1.DocType == "RCS");
  }

  protected virtual void SOInvoice_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<PX.Objects.SO.SOInvoice.pMInstanceID>(e.Row);
    sender.SetDefaultExt<PX.Objects.SO.SOInvoice.cashAccountID>(e.Row);
  }

  protected virtual void SOInvoice_PMInstanceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<PX.Objects.SO.SOInvoice.cashAccountID>(e.Row);
  }

  protected virtual void SOInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2 && e.Operation != 1)
      return;
    PX.Objects.SO.SOInvoice row = (PX.Objects.SO.SOInvoice) e.Row;
    if (row.DocType == "CSL" || row.DocType == "RCS")
    {
      if (string.IsNullOrEmpty(row.PaymentMethodID))
      {
        if (sender.RaiseExceptionHandling<PX.Objects.SO.SOInvoice.pMInstanceID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[pMInstanceID]"
        })))
          throw new PXRowPersistingException("pMInstanceID", (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) "pMInstanceID"
          });
      }
      else if (PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PaymentMethodID
      })).IsAccountNumberRequired.GetValueOrDefault() && !row.PMInstanceID.HasValue)
      {
        if (sender.RaiseExceptionHandling<PX.Objects.SO.SOInvoice.pMInstanceID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[pMInstanceID]"
        })))
          throw new PXRowPersistingException("pMInstanceID", (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) "pMInstanceID"
          });
      }
    }
    if ((row.DocType == "CSL" ? 1 : (row.DocType == "RCS" ? 1 : 0)) != 0 && ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).GetValueExt<PX.Objects.SO.SOInvoice.cashAccountID>((PX.Objects.SO.SOInvoice) e.Row) == null)
    {
      if (sender.RaiseExceptionHandling<PX.Objects.SO.SOInvoice.cashAccountID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[cashAccountID]"
      })))
        throw new PXRowPersistingException("cashAccountID", (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "cashAccountID"
        });
    }
    object valueExt;
    if ((valueExt = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).GetValueExt<PX.Objects.SO.SOInvoice.cashAccountID>((PX.Objects.SO.SOInvoice) e.Row)) == null || sender.GetValue<PX.Objects.SO.SOInvoice.cashAccountID>(e.Row) != null)
      return;
    sender.RaiseExceptionHandling<PX.Objects.SO.SOInvoice.cashAccountID>(e.Row, (object) null, (Exception) null);
    sender.SetValueExt<PX.Objects.SO.SOInvoice.cashAccountID>(e.Row, valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt);
  }

  private void ValidateTaxConfiguration(PXCache cache, PX.Objects.AR.ARInvoice cashSale)
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult<ARTax, PX.Objects.TX.Tax> pxResult in PXSelectBase<ARTax, PXSelectJoin<ARTax, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTax.taxID>>>, Where<ARTax.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<ARTax.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PX.Objects.TX.Tax tax = PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      if (tax.TaxApplyTermsDisc == "P")
        flag1 = true;
      if (tax.TaxApplyTermsDisc == "X")
        flag2 = true;
      if (flag1 & flag2)
        cache.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.taxZoneID>((object) cashSale, (object) cashSale.TaxZoneID, (Exception) new PXSetPropertyException("Tax configuration is invalid. A document cannot contain both Reduce Taxable Amount and Reduce Taxable Amount On Early Payment taxes."));
    }
  }

  protected virtual void SOInvoice_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<PX.Objects.SO.SOInvoice.pMInstanceID, PX.Objects.SO.SOInvoice.paymentMethodID, PX.Objects.SO.SOInvoice.cashAccountID>(e.Row, e.OldRow))
      return;
    PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) ((PX.Objects.SO.SOInvoice) e.Row).RefNbr, new object[1]
    {
      (object) ((PX.Objects.SO.SOInvoice) e.Row).DocType
    }));
    if (arInvoice == null)
      return;
    arInvoice.PMInstanceID = ((PX.Objects.SO.SOInvoice) e.Row).PMInstanceID;
    arInvoice.PaymentMethodID = ((PX.Objects.SO.SOInvoice) e.Row).PaymentMethodID;
    arInvoice.CashAccountID = ((PX.Objects.SO.SOInvoice) e.Row).CashAccountID;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) arInvoice, true);
  }

  protected override void ARAdjust2_CuryAdjdAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARAdjust2 adj = !((Decimal) e.NewValue < 0M) ? (ARAdjust2) e.Row : throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
    {
      (object) 0.ToString()
    });
    PX.Objects.CS.Terms terms = PXResultset<PX.Objects.CS.Terms>.op_Implicit(PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Current<PX.Objects.AR.ARInvoice.termsID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (terms != null && terms.InstallmentType != "S" && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("No applications can be created for documents with multiple installment credit terms specified.");
    if (!adj.CuryDocBal.HasValue)
      this.CalcBalancesFromInvoiceSide(adj, false, false);
    if (adj.CuryDocBal.Value + adj.CuryAdjdAmt.Value - (Decimal) e.NewValue < 0M)
    {
      object[] objArray = new object[1];
      Decimal? nullable = adj.CuryDocBal;
      Decimal num1 = nullable.Value;
      nullable = adj.CuryAdjdAmt;
      Decimal num2 = nullable.Value;
      objArray[0] = (object) (num1 + num2).ToString();
      throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
    }
  }

  protected virtual void ARAdjust2_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARAdjust2 row = (ARAdjust2) e.Row;
    if (!e.ExternalCall || !row.Recalculatable.GetValueOrDefault() || sender.ObjectsEqual<ARAdjust2.curyAdjdAmt>(e.Row, e.OldRow))
      return;
    row.Recalculatable = new bool?(false);
  }

  protected override void ARTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    if (row == null)
      return;
    if (!row.SortOrder.HasValue)
      row.SortOrder = row.LineNbr;
    if (e.ExternalCall || this.forceDiscountCalculation)
      this.RecalculateDiscounts(sender, (PX.Objects.AR.ARTran) e.Row);
    TaxBaseAttribute.Calculate<PX.Objects.AR.ARTran.taxCategoryID>(sender, e);
    if (((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current != null)
    {
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.IsTaxValid = new bool?(false);
      GraphHelper.MarkUpdated(((PXSelectBase) this.SODocument).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, true);
    }
    if (row.LineType == "GI" && row.InvtMult.GetValueOrDefault() != (short) 0)
      this.UpdateCreateINDocValue(true);
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current == null)
      return;
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.IsTaxValid = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.SODocument).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, true);
  }

  protected override void ARTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    base.ARTran_RowDeleted(sender, e);
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    if (row.LineType == "FR")
      return;
    bool flag = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current != null && EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current), (PXEntryStatus) 3, (PXEntryStatus) 4);
    List<PX.Objects.AR.ARTran> arTranList;
    if (flag)
      arTranList = new List<PX.Objects.AR.ARTran>();
    else
      arTranList = GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.sOOrderType, Equal<Required<PX.Objects.AR.ARTran.sOOrderType>>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<Required<PX.Objects.AR.ARTran.sOOrderNbr>>, And<PX.Objects.AR.ARTran.sOShipmentType, Equal<Required<PX.Objects.AR.ARTran.sOShipmentType>>, And<PX.Objects.AR.ARTran.sOShipmentNbr, Equal<Required<PX.Objects.AR.ARTran.sOShipmentNbr>>, And<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, new object[6]
      {
        (object) row.SOOrderType,
        (object) row.SOOrderNbr,
        (object) row.SOShipmentType,
        (object) row.SOShipmentNbr,
        (object) row.TranType,
        (object) row.RefNbr
      })).ToList<PX.Objects.AR.ARTran>();
    if (arTranList.Count == 1 && arTranList[0].LineType == "FR")
    {
      ((PXSelectBase<PX.Objects.AR.ARTran>) this.Freight).Delete(arTranList[0]);
      arTranList.Clear();
    }
    SOOrderShipment soOrderShipment1 = PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Required<SOOrderShipment.orderType>>, And<SOOrderShipment.orderNbr, Equal<Required<SOOrderShipment.orderNbr>>, And<SOOrderShipment.shipmentType, Equal<Required<SOOrderShipment.shipmentType>>, And<SOOrderShipment.shipmentNbr, Equal<Required<SOOrderShipment.shipmentNbr>>, And<SOOrderShipment.invoiceType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[6]
    {
      (object) row.SOOrderType,
      (object) row.SOOrderNbr,
      (object) row.SOShipmentType,
      (object) row.SOShipmentNbr,
      (object) row.TranType,
      (object) row.RefNbr
    }));
    if (arTranList.Count == 0)
    {
      if (soOrderShipment1 != null)
      {
        soOrderShipment1.HasDetailDeleted = new bool?(false);
        soOrderShipment1.IsPartialInvoiceConstraintViolated = new bool?(false);
        PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
        bool? nullable;
        int num;
        if (current == null)
        {
          num = 0;
        }
        else
        {
          nullable = current.DisableAutomaticTaxCalculation;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0)
        {
          nullable = soOrderShipment1.OrderTaxAllocated;
          if (nullable.GetValueOrDefault())
            this.DeductTaxAmountsOfDeletedOrdersFromARTaxTranDetails(soOrderShipment1);
        }
        SOOrderShipment soOrderShipment2 = soOrderShipment1.UnlinkInvoice((PXGraph) this);
        if (!flag)
        {
          nullable = soOrderShipment2.CreateINDoc;
          if (nullable.GetValueOrDefault() && soOrderShipment2.InvtRefNbr == null)
            this.UpdateCreateINDocValue(false);
        }
        if (!string.Equals(soOrderShipment2.ShipmentNbr, "<NEW>") && soOrderShipment2.ShipmentNbr != null && soOrderShipment2.ShipmentType != null)
        {
          soOrderShipment2.OrderFreightAllocated = new bool?(false);
          ((PXSelectBase) this.shipmentlist).Cache.Update((object) soOrderShipment2);
        }
        else
          ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Delete(soOrderShipment2);
        if (!flag && PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) == null)
        {
          if (((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current == null)
            ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(Array.Empty<object>()));
          if (((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current == null)
            throw new ArgumentNullException(typeof (PX.Objects.SO.SOInvoice).Name);
          ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.InitialSOBehavior = (string) null;
          GraphHelper.MarkUpdated(((PXSelectBase) this.SODocument).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current);
        }
      }
      if (!flag)
      {
        SOFreightDetail soFreightDetail = GraphHelper.RowCast<SOFreightDetail>((IEnumerable) ((IEnumerable<PXResult<SOFreightDetail>>) ((PXSelectBase<SOFreightDetail>) this.FreightDetails).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOFreightDetail>>()).Where<SOFreightDetail>((Func<SOFreightDetail, bool>) (d => d.ShipmentType == row.SOShipmentType && d.ShipmentNbr == row.SOShipmentNbr && d.OrderType == row.SOOrderType && d.OrderNbr == row.SOOrderNbr)).FirstOrDefault<SOFreightDetail>();
        if (soFreightDetail != null)
          ((PXSelectBase<SOFreightDetail>) this.FreightDetails).Delete(soFreightDetail);
        SOOrder soOrder = SOOrder.PK.Find((PXGraph) this, row.SOOrderType, row.SOOrderNbr);
        if (soOrder != null)
        {
          Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXGraph) this).Caches[typeof (SOOrder)], (object) soOrder);
          foreach (PXResult<NoteDoc> pxResult in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.NoteID
          }))
          {
            NoteDoc noteDoc = PXResult<NoteDoc>.op_Implicit(pxResult);
            if (((IEnumerable<Guid>) fileNotes).Contains<Guid>(noteDoc.FileID ?? Guid.Empty))
              ((PXGraph) this).Caches[typeof (NoteDoc)].Delete((object) noteDoc);
          }
        }
      }
    }
    else if (soOrderShipment1 != null)
    {
      soOrderShipment1.HasDetailDeleted = new bool?(true);
      SOOrderShipment soOrderShipment3 = soOrderShipment1;
      bool? constraintViolated = soOrderShipment3.IsPartialInvoiceConstraintViolated;
      soOrderShipment3.IsPartialInvoiceConstraintViolated = (row.LineType != "MI" ? 1 : (!SOOrderType.PK.Find((PXGraph) this, soOrderShipment1.OrderType).RequireShipping.GetValueOrDefault() ? 1 : 0)) != 0 ? new bool?(true) : constraintViolated;
      ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(soOrderShipment1);
    }
    if (flag)
      return;
    if (((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current != null)
    {
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.IsTaxValid = new bool?(false);
      GraphHelper.MarkUpdated(((PXSelectBase) this.SODocument).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, true);
    }
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current != null)
    {
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.IsTaxValid = new bool?(false);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current, true);
    }
    if (!(row.LineType == "GI"))
      return;
    short? invtMult = row.InvtMult;
    int? nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return;
    this.UpdateCreateINDocValue(false);
  }

  public virtual void DeductTaxAmountsOfDeletedOrdersFromARTaxTranDetails(
    SOOrderShipment ordershipment)
  {
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
    if ((current != null ? (!current.DisableAutomaticTaxCalculation.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    foreach (PXResult<SOTaxTran, PX.Objects.TX.Tax> pxResult1 in PXSelectBase<SOTaxTran, PXSelectJoin<SOTaxTran, InnerJoin<PX.Objects.TX.Tax, On<SOTaxTran.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<SOTaxTran.orderType, Equal<Required<SOTaxTran.orderType>>, And<SOTaxTran.orderNbr, Equal<Required<SOTaxTran.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ordershipment.OrderType,
      (object) ordershipment.OrderNbr
    }))
    {
      SOTaxTran tax = PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult1);
      PXResultset<ARTaxTran> source = ((PXSelectBase<ARTaxTran>) this.Taxes).Select(Array.Empty<object>());
      Expression<Func<PXResult<ARTaxTran>, bool>> predicate = (Expression<Func<PXResult<ARTaxTran>, bool>>) (a => ((ARTaxTran) a).TaxID == tax.TaxID && ((ARTaxTran) a).JurisType == tax.JurisType && ((ARTaxTran) a).JurisName == tax.JurisName);
      foreach (PXResult<ARTaxTran> pxResult2 in (IEnumerable<PXResult<ARTaxTran>>) ((IQueryable<PXResult<ARTaxTran>>) source).Where<PXResult<ARTaxTran>>(predicate))
      {
        ARTaxTran arTaxTran1 = (ARTaxTran) ((PXSelectBase) this.Taxes).Cache.CreateCopy((object) PXResult<ARTaxTran>.op_Implicit(pxResult2));
        ARTaxTran arTaxTran2 = arTaxTran1;
        Decimal? curyTaxableAmt1 = arTaxTran2.CuryTaxableAmt;
        Decimal? curyTaxableAmt2 = tax.CuryTaxableAmt;
        arTaxTran2.CuryTaxableAmt = curyTaxableAmt1.HasValue & curyTaxableAmt2.HasValue ? new Decimal?(curyTaxableAmt1.GetValueOrDefault() - curyTaxableAmt2.GetValueOrDefault()) : new Decimal?();
        ARTaxTran arTaxTran3 = arTaxTran1;
        Decimal? curyTaxAmt1 = arTaxTran3.CuryTaxAmt;
        Decimal? curyTaxAmt2 = tax.CuryTaxAmt;
        arTaxTran3.CuryTaxAmt = curyTaxAmt1.HasValue & curyTaxAmt2.HasValue ? new Decimal?(curyTaxAmt1.GetValueOrDefault() - curyTaxAmt2.GetValueOrDefault()) : new Decimal?();
        Decimal? curyTaxAmt3 = arTaxTran1.CuryTaxAmt;
        Decimal num1 = 0M;
        if (curyTaxAmt3.GetValueOrDefault() < num1 & curyTaxAmt3.HasValue)
        {
          arTaxTran1.CuryTaxableAmt = new Decimal?(0M);
          arTaxTran1.CuryTaxAmt = new Decimal?(0M);
        }
        curyTaxAmt3 = arTaxTran1.CuryTaxAmt;
        Decimal num2 = 0M;
        if (curyTaxAmt3.GetValueOrDefault() >= num2 & curyTaxAmt3.HasValue)
          arTaxTran1 = ((PXSelectBase<ARTaxTran>) this.Taxes).Update(arTaxTran1);
        curyTaxAmt3 = arTaxTran1.CuryTaxAmt;
        Decimal num3 = 0M;
        if (curyTaxAmt3.GetValueOrDefault() == num3 & curyTaxAmt3.HasValue)
          ((PXSelectBase<ARTaxTran>) this.Taxes).Delete(arTaxTran1);
      }
    }
  }

  protected override void ARTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    PX.Objects.AR.ARTran oldRow = (PX.Objects.AR.ARTran) e.OldRow;
    if (row == null)
      return;
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation.GetValueOrDefault() && !sender.ObjectsEqual<PX.Objects.AR.ARTran.taxCategoryID>((object) oldRow, (object) row))
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.IsPriceAndDiscountsValid = new bool?(false);
    bool? nullable1 = row.SkipLineDiscountsBuffer;
    if (nullable1.HasValue)
    {
      row.SkipLineDiscounts = row.SkipLineDiscountsBuffer;
      PX.Objects.AR.ARTran arTran = row;
      nullable1 = new bool?();
      bool? nullable2 = nullable1;
      arTran.SkipLineDiscountsBuffer = nullable2;
    }
    if ((e.ExternalCall || sender.Graph.IsImport) && sender.ObjectsEqual<PX.Objects.AR.ARTran.inventoryID>(e.Row, e.OldRow) && sender.ObjectsEqual<PX.Objects.AR.ARTran.uOM>(e.Row, e.OldRow) && sender.ObjectsEqual<PX.Objects.AR.ARTran.qty>(e.Row, e.OldRow) && sender.ObjectsEqual<PX.Objects.AR.ARTran.branchID>(e.Row, e.OldRow) && sender.ObjectsEqual<PX.Objects.AR.ARTran.siteID>(e.Row, e.OldRow) && sender.ObjectsEqual<PX.Objects.AR.ARTran.manualPrice>(e.Row, e.OldRow) && sender.ObjectsEqual<PX.Objects.AR.ARTran.lotSerialNbr>(e.Row, e.OldRow) && (!sender.ObjectsEqual<PX.Objects.AR.ARTran.curyUnitPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.curyExtPrice>(e.Row, e.OldRow)))
    {
      nullable1 = row.ManualPrice;
      bool? manualPrice = oldRow.ManualPrice;
      if (nullable1.GetValueOrDefault() == manualPrice.GetValueOrDefault() & nullable1.HasValue == manualPrice.HasValue)
      {
        row.ManualPrice = new bool?(true);
        row.SkipLineDiscounts = new bool?(false);
        row.SkipLineDiscountsBuffer = new bool?(false);
      }
    }
    if (!sender.ObjectsEqual<PX.Objects.AR.ARTran.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.qty>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.curyUnitPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.curyTranAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.curyExtPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.curyDiscAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.discPct>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.manualDisc>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.discountID>(e.Row, e.OldRow) || !sender.ObjectsEqual<PX.Objects.AR.ARTran.skipLineDiscounts>(e.Row, e.OldRow))
      this.RecalculateDiscounts(sender, row);
    bool? nullable3 = row.ManualDisc;
    if (!nullable3.GetValueOrDefault())
    {
      ARDiscount arDiscount = (ARDiscount) PXSelectorAttribute.Select<PX.Objects.AR.ARTran.discountID>(sender, (object) row);
      PX.Objects.AR.ARTran arTran = row;
      Decimal? nullable4;
      if (arDiscount != null)
      {
        nullable3 = arDiscount.IsAppliedToDR;
        if (nullable3.GetValueOrDefault())
        {
          nullable4 = row.DiscPct;
          goto label_15;
        }
      }
      nullable4 = new Decimal?(0.0M);
label_15:
      arTran.DiscPctDR = nullable4;
    }
    nullable3 = row.ManualPrice;
    if (!nullable3.GetValueOrDefault())
      row.CuryUnitPriceDR = row.CuryUnitPrice;
    short? invtMult;
    int num1;
    if (oldRow.LineType == "GI")
    {
      invtMult = oldRow.InvtMult;
      num1 = invtMult.GetValueOrDefault() != (short) 0 ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    int num2;
    if (row.LineType == "GI")
    {
      invtMult = row.InvtMult;
      num2 = invtMult.GetValueOrDefault() != (short) 0 ? 1 : 0;
    }
    else
      num2 = 0;
    bool presumptiveNewValue = num2 != 0;
    if (presumptiveNewValue != flag)
      this.UpdateCreateINDocValue(presumptiveNewValue);
    TaxBaseAttribute.Calculate<PX.Objects.AR.ARTran.taxCategoryID>(sender, e);
  }

  protected override void ARTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.ARTran_RowSelected(sender, e);
    if (!(e.Row is PX.Objects.AR.ARTran row))
      return;
    short? invtMult = row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    bool flag = !(nullable.GetValueOrDefault() == num & nullable.HasValue) && row.LineType != "MI";
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARTran.inventoryID>(sender, (object) row, row.SOOrderNbr == null);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARTran.qty>(sender, (object) row, row.SOOrderNbr == null | flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARTran.uOM>(sender, (object) row, row.SOOrderNbr == null | flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARTran.skipLineDiscounts>(sender, e.Row, ((PXGraph) this).IsCopyPasteContext);
  }

  protected override void ARTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    base.ARTran_RowPersisting(sender, e);
    if (!(e.Row is PX.Objects.AR.ARTran row) || (e.Operation & 3) != 2)
      return;
    this.ThrowIfSOInvoiceHasRelatedCorrectionReceipt(row);
  }

  protected override void ARTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (((PX.Objects.AR.ARTran) e.Row).SOShipmentNbr == null)
      sender.SetDefaultExt<PX.Objects.AR.ARTran.invtMult>(e.Row);
    base.ARTran_InventoryID_FieldUpdated(sender, e);
  }

  protected virtual void ARTran_InvtMult_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) (row.SOShipmentNbr != null || row.LineType == "DS" ? new short?((short) 0) : INTranType.InvtMultFromInvoiceType(row.TranType));
  }

  protected override void ARTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<PX.Objects.AR.ARTran.curyUnitPrice>(e.Row);
  }

  protected virtual void ARTran_SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    sender.SetDefaultExt<PX.Objects.AR.ARTran.curyUnitPrice>((object) row);
    if (row.InventoryID.HasValue)
    {
      bool? nullable = row.IsStockItem;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.AccrueCost;
        if (nullable.GetValueOrDefault())
        {
          sender.SetDefaultExt<PX.Objects.AR.ARTran.expenseAccrualAccountID>(e.Row);
          try
          {
            sender.SetDefaultExt<PX.Objects.AR.ARTran.expenseAccrualSubID>(e.Row);
          }
          catch (PXSetPropertyException ex)
          {
            sender.SetValue<PX.Objects.AR.ARTran.expenseAccrualSubID>(e.Row, (object) null);
          }
          sender.SetDefaultExt<PX.Objects.AR.ARTran.expenseAccountID>(e.Row);
          try
          {
            sender.SetDefaultExt<PX.Objects.AR.ARTran.expenseSubID>(e.Row);
            return;
          }
          catch (PXSetPropertyException ex)
          {
            sender.SetValue<PX.Objects.AR.ARTran.expenseSubID>(e.Row, (object) null);
            return;
          }
        }
      }
    }
    row.ExpenseAccrualAccountID = new int?();
    row.ExpenseAccrualSubID = new int?();
    row.ExpenseAccountID = new int?();
    row.ExpenseSubID = new int?();
  }

  protected override void ARTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || string.IsNullOrEmpty(((PX.Objects.AR.ARTran) e.Row).SOOrderNbr))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_SalesPersonID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || string.IsNullOrEmpty(((PX.Objects.AR.ARTran) e.Row).SOOrderNbr))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected override void ARTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row != null && !string.IsNullOrEmpty(((PX.Objects.AR.ARTran) e.Row).SOOrderType))
    {
      PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
      if (row == null)
        return;
      PX.Objects.IN.InventoryItem data1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
      if (data1 == null)
        return;
      switch (SOOrderType.PK.Find((PXGraph) this, row.SOOrderType).SalesAcctDefault)
      {
        case "I":
          e.NewValue = this.GetValue<PX.Objects.IN.InventoryItem.salesAcctID>((object) data1);
          ((CancelEventArgs) e).Cancel = true;
          break;
        case "W":
          PX.Objects.IN.INSite data2 = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
          e.NewValue = this.GetValue<PX.Objects.IN.INSite.salesAcctID>((object) data2);
          ((CancelEventArgs) e).Cancel = true;
          break;
        case "P":
          INPostClass data3 = INPostClass.PK.Find((PXGraph) this, data1.PostClassID) ?? new INPostClass();
          e.NewValue = this.GetValue<INPostClass.salesAcctID>((object) data3);
          ((CancelEventArgs) e).Cancel = true;
          break;
        case "L":
          PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
          e.NewValue = this.GetValue<PX.Objects.CR.Location.cSalesAcctID>((object) current);
          ((CancelEventArgs) e).Cancel = true;
          break;
        case "R":
          PX.Objects.CS.ReasonCode data4 = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, row.ReasonCode);
          e.NewValue = this.GetValue<PX.Objects.CS.ReasonCode.salesAcctID>((object) data4);
          ((CancelEventArgs) e).Cancel = true;
          break;
      }
    }
    else
      base.ARTran_AccountID_FieldDefaulting(sender, e);
  }

  protected override void ARTran_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row != null && !string.IsNullOrEmpty(((PX.Objects.AR.ARTran) e.Row).SOOrderType))
    {
      PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
      if (row == null || !row.AccountID.HasValue)
        return;
      PX.Objects.IN.InventoryItem data1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
      PX.Objects.IN.INSite data2 = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
      PX.Objects.CS.ReasonCode data3 = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, row.ReasonCode);
      SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this, row.SOOrderType);
      PX.Objects.AR.SalesPerson data4 = (PX.Objects.AR.SalesPerson) PXSelectorAttribute.Select<PX.Objects.AR.ARTran.salesPersonID>(sender, e.Row);
      INPostClass data5 = INPostClass.PK.Find((PXGraph) this, data1?.PostClassID) ?? new INPostClass();
      PX.Objects.EP.EPEmployee data6 = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelectJoin<PX.Objects.EP.EPEmployee, InnerJoin<SOOrder, On<PX.Objects.EP.EPEmployee.defContactID, Equal<SOOrder.ownerID>>>, Where<SOOrder.orderType, Equal<Required<PX.Objects.AR.ARTran.sOOrderType>>, And<SOOrder.orderNbr, Equal<Required<PX.Objects.AR.ARTran.sOOrderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.SOOrderType,
        (object) row.SOOrderNbr
      }));
      PX.Objects.CR.Standalone.Location data7 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.AR.ARTran.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.BranchID
      }));
      PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
      object obj1 = this.GetValue<PX.Objects.IN.InventoryItem.salesSubID>((object) data1);
      object obj2 = this.GetValue<PX.Objects.IN.INSite.salesSubID>((object) data2);
      object obj3 = this.GetValue<INPostClass.salesSubID>((object) data5);
      object obj4 = this.GetValue<PX.Objects.CR.Location.cSalesSubID>((object) current);
      object obj5 = this.GetValue<PX.Objects.EP.EPEmployee.salesSubID>((object) data6);
      object obj6 = this.GetValue<PX.Objects.CR.Standalone.Location.cMPSalesSubID>((object) data7);
      object obj7 = this.GetValue<PX.Objects.AR.SalesPerson.salesSubID>((object) data4);
      object obj8 = this.GetValue<PX.Objects.CS.ReasonCode.salesSubID>((object) data3);
      object obj9 = (object) null;
      try
      {
        obj9 = (object) SOSalesSubAccountMaskAttribute.MakeSub<SOOrderType.salesSubMask>((PXGraph) this, soOrderType.SalesSubMask, new object[8]
        {
          obj1,
          obj2,
          obj3,
          obj4,
          obj5,
          obj6,
          obj7,
          obj8
        }, new System.Type[8]
        {
          typeof (PX.Objects.IN.InventoryItem.salesSubID),
          typeof (PX.Objects.IN.INSite.salesSubID),
          typeof (INPostClass.salesSubID),
          typeof (PX.Objects.CR.Location.cSalesSubID),
          typeof (PX.Objects.EP.EPEmployee.salesSubID),
          typeof (PX.Objects.CR.Location.cMPSalesSubID),
          typeof (PX.Objects.AR.SalesPerson.salesSubID),
          typeof (PX.Objects.CS.ReasonCode.subID)
        });
        sender.RaiseFieldUpdating<PX.Objects.AR.ARTran.subID>((object) row, ref obj9);
      }
      catch (PXMaskArgumentException ex)
      {
        sender.RaiseExceptionHandling<PX.Objects.AR.ARTran.subID>(e.Row, (object) null, (Exception) new PXSetPropertyException(((Exception) ex).Message));
        obj9 = (object) null;
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling<PX.Objects.AR.ARTran.subID>(e.Row, obj9, (Exception) ex);
        obj9 = (object) null;
      }
      e.NewValue = (object) (int?) obj9;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
      base.ARTran_SubID_FieldDefaulting(sender, e);
  }

  protected override void ARInvoiceDiscountDetail_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    base.ARInvoiceDiscountDetail_RowSelected(sender, e);
    ARInvoiceDiscountDetail row = (ARInvoiceDiscountDetail) e.Row;
    if (row == null || row.OrderNbr != null || row.DiscountID == null)
      return;
    bool flag = false;
    foreach (PX.Objects.AR.ARTran arTran in ((PXSelectBase) this.Transactions).Cache.Cached)
    {
      if (EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.Transactions).Cache.GetStatus((object) arTran), (PXEntryStatus) 3, (PXEntryStatus) 4))
      {
        Decimal? groupDiscountRate = arTran.OrigGroupDiscountRate;
        Decimal num1 = (Decimal) 1;
        if (groupDiscountRate.GetValueOrDefault() == num1 & groupDiscountRate.HasValue)
        {
          Decimal? documentDiscountRate = arTran.OrigDocumentDiscountRate;
          Decimal num2 = (Decimal) 1;
          if (documentDiscountRate.GetValueOrDefault() == num2 & documentDiscountRate.HasValue)
            continue;
        }
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    sender.RaiseExceptionHandling<ARInvoiceDiscountDetail.discountID>((object) row, (object) row.DiscountID, (Exception) new PXSetPropertyException("Group and document discounts were recalculated based on all the document lines. Discounts inherited from the sales orders were not recalculated, however. Please verify all discounts and delete any unnecessary ones.", (PXErrorLevel) 2));
  }

  protected virtual void SOFreightDetail_AccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOFreightDetail row) || row.TaskID.HasValue)
      return;
    sender.SetDefaultExt<SOFreightDetail.taskID>(e.Row);
  }

  protected virtual void SOFreightDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOFreightDetail row = (SOFreightDetail) e.Row;
    if (row == null)
      return;
    this.UpdateFreightTransaction(row, false);
  }

  protected virtual void SOFreightDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    SOFreightDetail row = (SOFreightDetail) e.Row;
    if (row == null)
      return;
    this.UpdateFreightTransaction(row, true);
  }

  public virtual int ExecuteInsert(string viewName, IDictionary values, params object[] parameters)
  {
    switch (viewName)
    {
      case "Freight":
        values[(object) PXDataUtils.FieldName<PX.Objects.AR.ARTran.lineType>()] = (object) "FR";
        break;
      case "Discount":
        values[(object) PXDataUtils.FieldName<PX.Objects.AR.ARTran.lineType>()] = (object) "DS";
        break;
    }
    return ((PXGraph) this).ExecuteInsert(viewName, values, parameters);
  }

  public virtual IEnumerable sHipmentlist()
  {
    SOInvoiceEntry graph = this;
    PXSelectBase<PX.Objects.AR.ARTran> cmd = (PXSelectBase<PX.Objects.AR.ARTran>) new PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.sOShipmentNbr, Equal<Current<SOOrderShipment.shipmentNbr>>, And<PX.Objects.AR.ARTran.sOShipmentType, Equal<Current<SOOrderShipment.shipmentType>>, And<PX.Objects.AR.ARTran.sOOrderType, Equal<Current<SOOrderShipment.orderType>>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<Current<SOOrderShipment.orderNbr>>, And<PX.Objects.AR.ARTran.sOOrderLineNbr, IsNotNull, And<PX.Objects.AR.ARTran.canceled, NotEqual<True>, And<PX.Objects.AR.ARTran.isCancellation, NotEqual<True>>>>>>>>>((PXGraph) graph);
    InvoiceList list = new InvoiceList((PXGraph) graph);
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) graph.currencyinfo).Select(Array.Empty<object>()));
    list.Add(((PXSelectBase<PX.Objects.AR.ARInvoice>) graph.Document).Current, PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) graph.SODocument).Select(Array.Empty<object>())), curyInfo);
    bool newInvoice = ((PXSelectBase<PX.Objects.AR.ARTran>) graph.Transactions).SelectSingle(Array.Empty<object>()) == null;
    bool curyRateNotDefined = curyInfo.CuryRate.GetValueOrDefault() == 0M;
    List<FieldLookup> invoiceSearchValues = new List<FieldLookup>();
    HashSet<SOOrderShipment> selectedShipments = new HashSet<SOOrderShipment>((IEqualityComparer<SOOrderShipment>) PXCacheEx.GetComparer(((PXSelectBase) graph.shipmentlist).Cache));
    foreach (SOOrderShipment soOrderShipment in ((PXSelectBase) graph.shipmentlist).Cache.Updated)
      selectedShipments.Add(soOrderShipment);
    foreach (PXResult<SOOrderShipment, SOOrder, SOOrderType> pxResult in ((IEnumerable<PXResult<SOOrderShipment>>) PXSelectBase<SOOrderShipment, PXSelectJoin<SOOrderShipment, InnerJoin<SOOrder, On<SOOrderShipment.FK.Order>, InnerJoin<SOOrderType, On<SOOrderShipment.FK.OrderType>, InnerJoin<SOShipment, On<SOShipment.shipmentNbr, Equal<SOOrderShipment.shipmentNbr>, And<SOShipment.shipmentType, Equal<SOOrderShipment.shipmentType>>>>>>, Where<SOOrderShipment.customerID, Equal<Current<PX.Objects.AR.ARInvoice.customerID>>, And<SOOrderShipment.hold, Equal<boolFalse>, And<SOOrderShipment.confirmed, Equal<boolTrue>, And<SOOrderType.aRDocType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<Where<SOOrderShipment.invoiceNbr, IsNull, Or<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>>>>>>.Config>.Select((PXGraph) graph, Array.Empty<object>())).AsEnumerable<PXResult<SOOrderShipment>>().Concat<PXResult<SOOrderShipment>>((IEnumerable<PXResult<SOOrderShipment>>) PXSelectBase<SOOrderShipment, PXSelectJoin<SOOrderShipment, InnerJoin<SOOrder, On<SOOrderShipment.FK.Order>, InnerJoin<SOOrderType, On<SOOrderShipment.FK.OrderType>, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceipt.receiptNbr, Equal<SOOrderShipment.shipmentNbr>, And<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreceipt>>>>>>, Where<SOOrderShipment.shipmentType, Equal<INDocType.dropShip>, And<SOOrderShipment.customerID, Equal<Current<PX.Objects.AR.ARInvoice.customerID>>, And<SOOrderType.aRDocType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And2<Where<SOOrderShipment.invoiceNbr, IsNull, Or<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>, And<PX.Objects.PO.POReceipt.isUnderCorrection, Equal<False>, And<PX.Objects.PO.POReceipt.canceled, Equal<False>>>>>>>>.Config>.Select((PXGraph) graph, Array.Empty<object>())))
    {
      SOOrderShipment soOrderShipment = PXResult<SOOrderShipment, SOOrder, SOOrderType>.op_Implicit(pxResult);
      if (((PXSelectBase) cmd).View.SelectSingleBound(new object[1]
      {
        (object) soOrderShipment
      }, Array.Empty<object>()) == null)
      {
        SOOrder soOrder = PXResult<SOOrderShipment, SOOrder, SOOrderType>.op_Implicit(pxResult);
        SOOrderType soOrderType = PXResult<SOOrderShipment, SOOrder, SOOrderType>.op_Implicit(pxResult);
        bool? nullable;
        int num1;
        if (!curyRateNotDefined)
        {
          if (newInvoice)
          {
            nullable = soOrderType.UseCuryRateFromSO;
            num1 = nullable.GetValueOrDefault() ? 1 : 0;
          }
          else
            num1 = 0;
        }
        else
          num1 = 1;
        if (!newInvoice)
        {
          invoiceSearchValues.Add((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.customerID>((object) soOrder.CustomerID));
          invoiceSearchValues.Add((FieldLookup) new FieldLookup<PX.Objects.SO.SOInvoice.billAddressID>((object) soOrder.BillAddressID));
          invoiceSearchValues.Add((FieldLookup) new FieldLookup<PX.Objects.SO.SOInvoice.billContactID>((object) soOrder.BillContactID));
          invoiceSearchValues.Add((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.termsID>((object) soOrder.TermsID));
          PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) graph.Document).Current;
          int num2;
          if (current == null)
          {
            num2 = 0;
          }
          else
          {
            nullable = current.DisableAutomaticDiscountCalculation;
            num2 = nullable.HasValue ? 1 : 0;
          }
          if (num2 != 0)
            invoiceSearchValues.Add((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.disableAutomaticTaxCalculation>((object) soOrder.DisableAutomaticTaxCalculation));
          invoiceSearchValues.Add((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hidden>((object) false));
        }
        if (num1 == 0)
          invoiceSearchValues.Add((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.curyID>((object) soOrder.CuryID));
        if (list.Find(invoiceSearchValues.ToArray()) != null)
        {
          selectedShipments.Remove(soOrderShipment);
          yield return (object) soOrderShipment;
        }
        invoiceSearchValues.Clear();
      }
    }
    foreach (SOOrderShipment soOrderShipment in selectedShipments)
      soOrderShipment.Selected = new bool?(false);
  }

  protected virtual void SOOrderShipment_ShipmentNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void UpdateCreateINDocValue(bool presumptiveNewValue)
  {
    if (((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current == null)
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(Array.Empty<object>()));
    if (((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current == null)
      throw new ArgumentNullException(typeof (PX.Objects.SO.SOInvoice).Name);
    bool? createInDoc = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.CreateINDoc;
    bool flag = presumptiveNewValue;
    if (createInDoc.GetValueOrDefault() == flag & createInDoc.HasValue || !presumptiveNewValue && (PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<SOOrderShipment.createINDoc, Equal<boolTrue>, And<SOOrderShipment.invtRefNbr, IsNull>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, Array.Empty<object>()).Count > 0 || PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.lineType, Equal<SOLineType.inventory>, And<PX.Objects.AR.ARTran.invtMult, NotEqual<short0>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()).Count > 0))
      return;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.CreateINDoc = new bool?(presumptiveNewValue);
    if (((PXSelectBase) this.SODocument).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current) != null)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.SODocument).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, true);
  }

  protected virtual void SOOrderShipment_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is SOOrderShipment row) || !string.Equals(row.ShipmentNbr, "<NEW>"))
      return;
    SOOrder soOrder1 = ((PXSelectBase<SOOrder>) this.soorder).Locate(new SOOrder()
    {
      OrderType = row.OrderType,
      OrderNbr = row.OrderNbr
    });
    if (soOrder1 == null)
      return;
    SOOrder soOrder2 = soOrder1;
    int? shipmentCntr = soOrder2.ShipmentCntr;
    soOrder2.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() - 1) : new int?();
    ((PXSelectBase<SOOrder>) this.soorder).Update(soOrder1);
    ((SelectedEntityEvent<SOOrderShipment, SOShipment>) PXEntityEventBase<SOOrderShipment>.Container<SOOrderShipment.Events>.Select<SOShipment>((Expression<Func<SOOrderShipment.Events, PXEntityEvent<SOOrderShipment.Events, SOShipment>>>) (ev => ev.ShipmentUnlinked))).FireOn((PXGraph) this, row, new SOShipment()
    {
      ShipmentNbr = "<NEW>"
    });
  }

  protected virtual void SOOrderShipment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<SOOrderShipment.selected>(sender, e.Row, true);
  }

  protected virtual void SOOrder_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    if (e.Operation == 1)
    {
      int? shipmentCntr1 = row.ShipmentCntr;
      int num1 = 0;
      if (!(shipmentCntr1.GetValueOrDefault() < num1 & shipmentCntr1.HasValue))
      {
        int? openShipmentCntr = row.OpenShipmentCntr;
        int num2 = 0;
        if (!(openShipmentCntr.GetValueOrDefault() < num2 & openShipmentCntr.HasValue))
        {
          int? shipmentCntr2 = row.ShipmentCntr;
          int? billedCntr = row.BilledCntr;
          int? releasedCntr = row.ReleasedCntr;
          int? nullable = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
          if (!(shipmentCntr2.GetValueOrDefault() < nullable.GetValueOrDefault() & shipmentCntr2.HasValue & nullable.HasValue) || !(row.Behavior == "SO"))
            return;
        }
      }
      throw new InvalidShipmentCountersException();
    }
  }

  protected virtual void SOOrder_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    SOOrder oldRow1 = e.OldRow as SOOrder;
    if (row != null && oldRow1 != null && !((PXSelectBase) this.soorder).Cache.ObjectsEqual<SOOrder.unbilledOrderQty, SOOrder.curyUnbilledLineTotal, SOOrder.curyUnbilledMiscTot, SOOrder.curyUnbilledFreightTot, SOOrder.curyUnbilledDiscTotal>((object) row, (object) oldRow1))
      row.IsUnbilledTaxValid = new bool?(false);
    if (e.OldRow != null)
    {
      SOOrder oldRow2 = (SOOrder) e.OldRow;
      Decimal? nullable = ((SOOrder) e.OldRow).UnbilledOrderTotal;
      Decimal? UnbilledAmount = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
      nullable = ((SOOrder) e.Row).OpenOrderTotal;
      Decimal? UnshippedAmount = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
      ARReleaseProcess.UpdateARBalances((PXGraph) this, oldRow2, UnbilledAmount, UnshippedAmount);
    }
    ARReleaseProcess.UpdateARBalances((PXGraph) this, (SOOrder) e.Row, ((SOOrder) e.Row).UnbilledOrderTotal, ((SOOrder) e.Row).OpenOrderTotal);
  }

  public override IEnumerable adjustments()
  {
    SOInvoiceEntry graph = this;
    ((PXSelectBase) graph.Adjustments_Inv).View.Clear();
    int applcount = 0;
    foreach (PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction> pxResult in ((PXSelectBase<ARAdjust2>) graph.Adjustments_Inv).Select(Array.Empty<object>()))
    {
      PX.Objects.AR.ARPayment payment = PX.Objects.Common.Utilities.Clone<PX.Objects.AR.Standalone.ARPayment, PX.Objects.AR.ARPayment>((PXGraph) graph, PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult));
      ARAdjust2 adj = PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
      PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult);
      PXCache<PX.Objects.AR.ARRegister>.RestoreCopy((PX.Objects.AR.ARRegister) payment, (PX.Objects.AR.ARRegister) PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult));
      PX.Objects.AR.ARPayment copy = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(payment);
      if (adj != null)
      {
        if (!adj.CuryDocBal.HasValue)
          graph.CalcBalancesFromInvoiceSide(adj, payment, true, true);
        yield return (object) new PXResult<ARAdjust2, PX.Objects.AR.ARPayment, PX.Objects.AR.ExternalTransaction>(adj, copy, PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult));
        ++applcount;
      }
    }
    if ((!((PXGraph) graph).UnattendedMode || graph.TransferApplicationFromSalesOrder) && (((PXGraph) graph).IsContractBasedAPI || ((PXGraph) graph).UnattendedMode || FlaggedModeScopeBase<SOInvoiceAddOrderPaymentsScope>.IsActive))
    {
      foreach (PXResult<ARAdjust2, PX.Objects.AR.ARPayment> pxResult in (PXResultset<ARAdjust2>) graph.LoadDocumentsProc(applcount))
        yield return (object) pxResult;
    }
  }

  public override IEnumerable adjustments_1()
  {
    SOInvoiceEntry soInvoiceEntry = this;
    // ISSUE: reference to a compiler-generated method
    foreach (object obj in soInvoiceEntry.\u003C\u003En__0())
      yield return obj;
    if (soInvoiceEntry.TransferApplicationFromSalesOrder && ((PXSelectBase<PX.Objects.AR.ARInvoice>) soInvoiceEntry.Document).Current?.DocType == "CRM")
    {
      using (new ReadOnlyScope(new PXCache[6]
      {
        ((PXSelectBase) soInvoiceEntry.Adjustments).Cache,
        ((PXSelectBase) soInvoiceEntry.Adjustments_1).Cache,
        ((PXSelectBase) soInvoiceEntry.Document).Cache,
        ((PXSelectBase) soInvoiceEntry.arbalances).Cache,
        ((PXSelectBase) soInvoiceEntry.SODocument).Cache,
        ((PXSelectBase) soInvoiceEntry.PaymentTotalsUpd).Cache
      }))
      {
        foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction> pxResult in soInvoiceEntry.CollectPaymentsToApply(0))
        {
          PX.Objects.AR.ARAdjust applicationFromPayment = soInvoiceEntry.CreateApplicationFromPayment(((PXSelectBase) soInvoiceEntry.Adjustments_1).Cache, PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult));
          if (applicationFromPayment != null)
            yield return (object) new PXResult<PX.Objects.AR.ARAdjust, PX.Objects.AR.ARPayment, PX.Objects.AR.ExternalTransaction>(applicationFromPayment, PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult), PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult));
        }
      }
    }
  }

  /// <summary>
  /// The method to calculate application
  /// balances in Invoice currency. Only
  /// payment document should be set.
  /// </summary>
  protected override void CalcBalancesFromInvoiceSide(
    ARAdjust2 adj,
    PX.Objects.AR.ARPayment payment,
    bool isCalcRGOL,
    bool DiscOnDiscDate)
  {
    PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.ARInvoice_CustomerID_DocType_RefNbr).Select(new object[3]
    {
      (object) adj.AdjdCustomerID,
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }));
    ARAdjust2 ofPaymentGrouped = this.GetOtherAppsOfPaymentGrouped(adj);
    new ARInvoiceBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>(), (PXGraph) this).CalcBalancesFromInvoiceSide((PX.Objects.AR.ARAdjust) adj, (IInvoice) arInvoice, payment, isCalcRGOL, DiscOnDiscDate, (PX.Objects.AR.ARAdjust) ofPaymentGrouped);
  }

  protected virtual ARAdjust2 GetOtherAppsOfPaymentGrouped(ARAdjust2 adj)
  {
    ARAdjust2 ofPaymentGrouped = PXResultset<ARAdjust2>.op_Implicit(PXSelectBase<ARAdjust2, PXSelectGroupBy<ARAdjust2, Where<ARAdjust2.adjgDocType, Equal<Required<ARAdjust2.adjgDocType>>, And<ARAdjust2.adjgRefNbr, Equal<Required<ARAdjust2.adjgRefNbr>>, And<ARAdjust2.released, Equal<False>, And<Where<ARAdjust2.adjdDocType, NotEqual<Required<ARAdjust2.adjdDocType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Required<ARAdjust2.adjdRefNbr>>>>>>>>, Aggregate<GroupBy<ARAdjust2.adjgDocType, GroupBy<ARAdjust2.adjgRefNbr, Sum<ARAdjust2.curyAdjgSignedAmt, Sum<ARAdjust2.adjSignedAmt>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr,
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }));
    if (ofPaymentGrouped != null)
    {
      Decimal? nullable1 = ARDocType.SignBalance(adj.AdjdDocType);
      ARAdjust2 arAdjust2_1 = ofPaymentGrouped;
      Decimal? nullable2 = ofPaymentGrouped.CuryAdjgSignedAmt;
      Decimal? nullable3 = nullable1;
      Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
      arAdjust2_1.CuryAdjgAmt = nullable4;
      ARAdjust2 arAdjust2_2 = ofPaymentGrouped;
      nullable3 = ofPaymentGrouped.AdjSignedAmt;
      nullable2 = nullable1;
      Decimal? nullable5 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      arAdjust2_2.AdjAmt = nullable5;
    }
    return ofPaymentGrouped;
  }

  public override PXResultset<ARAdjust2, PX.Objects.AR.ARPayment, PX.Objects.AR.ExternalTransaction> LoadDocumentsProc()
  {
    PXResultset<SOOrderShipment> pxResultset = PXSelectBase<SOOrderShipment, PXViewOf<SOOrderShipment>.BasedOn<SelectFromBase<SOOrderShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderShipment.invoiceType, Equal<BqlField<PX.Objects.AR.ARInvoice.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOOrderShipment.invoiceNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.FromCurrent>>>>.And<Exists<SelectFromBase<SOAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjdOrderType, Equal<SOOrderShipment.orderType>>>>>.And<BqlOperand<SOAdjust.adjdOrderNbr, IBqlString>.IsEqual<SOOrderShipment.orderNbr>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    using (new SOInvoiceAddOrderPaymentsScope())
    {
      foreach (PXResult<SOOrderShipment> pxResult in pxResultset)
        this.InsertApplications(PXResult<SOOrderShipment>.op_Implicit(pxResult));
    }
    return this.LoadDocumentsProc(0);
  }

  public virtual void NonTransferApplicationQuery(PXSelectBase<PX.Objects.AR.ARPayment> cmd)
  {
    cmd.Join<LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>, And<ARAdjust2.adjgRefNbr, Equal<PX.Objects.AR.ARPayment.refNbr>, And<ARAdjust2.adjNbr, Equal<PX.Objects.AR.ARPayment.adjCntr>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.hold, Equal<True>, And<ARAdjust2.voided, Equal<False>, And<Where<ARAdjust2.adjdDocType, NotEqual<Current<PX.Objects.AR.ARInvoice.docType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>>>>>>>>>();
    cmd.Join<LeftJoin<SOAdjust, On<SOAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>, And<SOAdjust.adjgRefNbr, Equal<PX.Objects.AR.ARPayment.refNbr>, And<SOAdjust.adjAmt, Greater<decimal0>>>>>>();
    cmd.WhereAnd<Where<PX.Objects.AR.ARPayment.finPeriodID, LessEqual<Current<PX.Objects.AR.ARInvoice.finPeriodID>>, And<PX.Objects.AR.ARPayment.released, Equal<True>, And<ARAdjust2.adjdRefNbr, IsNull, And<SOAdjust.adjgRefNbr, IsNull>>>>>();
  }

  public virtual PXResultset<ARAdjust2, PX.Objects.AR.ARPayment, PX.Objects.AR.ExternalTransaction> LoadDocumentsProc(
    int applcount)
  {
    PXResultset<ARAdjust2, PX.Objects.AR.ARPayment, PX.Objects.AR.ExternalTransaction> pxResultset = new PXResultset<ARAdjust2, PX.Objects.AR.ARPayment, PX.Objects.AR.ExternalTransaction>();
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      bool? released = current.Released;
      bool flag = false;
      num = released.GetValueOrDefault() == flag & released.HasValue ? 1 : 0;
    }
    if (num != 0 && EnumerableExtensions.IsIn<string>(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType, "INV", "DRM"))
    {
      using (new ReadOnlyScope(new PXCache[6]
      {
        ((PXSelectBase) this.Adjustments).Cache,
        ((PXSelectBase) this.Adjustments_1).Cache,
        ((PXSelectBase) this.Document).Cache,
        ((PXSelectBase) this.arbalances).Cache,
        ((PXSelectBase) this.SODocument).Cache,
        ((PXSelectBase) this.PaymentTotalsUpd).Cache
      }))
      {
        foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction> pxResult in this.CollectPaymentsToApply(applcount))
        {
          ARAdjust2 applicationFromPayment = (ARAdjust2) this.CreateApplicationFromPayment(((PXSelectBase) this.Adjustments).Cache, PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult));
          if (applicationFromPayment != null)
            ((PXResultset<ARAdjust2>) pxResultset).Add((PXResult<ARAdjust2>) new PXResult<ARAdjust2, PX.Objects.AR.ARPayment, PX.Objects.AR.ExternalTransaction>(applicationFromPayment, PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult), PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult)));
        }
      }
    }
    return pxResultset;
  }

  protected virtual List<PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>> CollectPaymentsToApply(
    int applcount)
  {
    List<PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>> apply = new List<PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>>();
    PXSelectBase<PX.Objects.AR.ARPayment> cmd = (PXSelectBase<PX.Objects.AR.ARPayment>) new PXSelectReadonly2<PX.Objects.AR.ARPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARPayment.curyInfoID>>, LeftJoin<PX.Objects.AR.ExternalTransaction, On<PX.Objects.AR.ExternalTransaction.transactionID, Equal<PX.Objects.AR.ARPayment.cCActualExternalTransactionID>>>>, Where<PX.Objects.AR.ARPayment.customerID, In3<Current<PX.Objects.AR.ARInvoice.customerID>, Current<PX.Objects.AR.Customer.consolidatingBAccountID>>, And<PX.Objects.AR.ARPayment.openDoc, Equal<True>>>, OrderBy<Asc<PX.Objects.AR.ARPayment.docType, Asc<PX.Objects.AR.ARPayment.refNbr>>>>((PXGraph) this);
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current?.DocType == "CRM")
    {
      cmd.WhereAnd<Where<PX.Objects.AR.ARPayment.docType, Equal<ARDocType.refund>>>();
    }
    else
    {
      cmd.WhereAnd<Where<PX.Objects.AR.ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.creditMemo, ARDocType.prepaymentInvoice>>>();
      if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
        cmd.WhereAnd<Where<PX.Objects.AR.ARPayment.docType, NotEqual<ARDocType.prepaymentInvoice>, Or<PX.Objects.AR.ARPayment.pendingPayment, Equal<False>, And<PX.Objects.AR.ARPayment.released, Equal<True>>>>>();
    }
    if (!this.TransferApplicationFromSalesOrder)
    {
      this.NonTransferApplicationQuery(cmd);
      int num = 200 - applcount;
      if (num > 0)
      {
        foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction> pxResult in cmd.SelectWindowed(0, num, Array.Empty<object>()))
          apply.Add(pxResult);
      }
    }
    else
    {
      cmd.Join<InnerJoin<SOAdjust, On<SOAdjust.adjgDocType, Equal<PX.Objects.AR.ARPayment.docType>, And<SOAdjust.adjgRefNbr, Equal<PX.Objects.AR.ARPayment.refNbr>>>>>();
      cmd.WhereAnd<Where<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>>>>();
      HashSet<string> stringSet = new HashSet<string>();
      foreach (PXResult<PX.Objects.AR.ARTran> pxResult1 in ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Select(Array.Empty<object>()))
      {
        PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult1);
        if (!string.IsNullOrEmpty(arTran.SOOrderType) && !string.IsNullOrEmpty(arTran.SOOrderNbr))
        {
          string str = $"{arTran.SOOrderType}.{arTran.SOOrderNbr}";
          if (!stringSet.Contains(str))
          {
            stringSet.Add(str);
            foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction, SOAdjust> pxResult2 in cmd.Select(new object[2]
            {
              (object) arTran.SOOrderType,
              (object) arTran.SOOrderNbr
            }))
            {
              SOAdjust soAdjust1 = PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction, SOAdjust>.op_Implicit(pxResult2);
              SOAdjust soAdjust2 = ((PXSelectBase<SOAdjust>) this.soadjustments).Locate(soAdjust1);
              if (soAdjust2 != null && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.soadjustments).Cache.GetStatus((object) soAdjust2), (PXEntryStatus) 3, (PXEntryStatus) 4))
                soAdjust1 = soAdjust2;
              else if (soAdjust2 != null)
                soAdjust1 = (SOAdjust) null;
              if (soAdjust1 != null)
              {
                Decimal? adjAmt = soAdjust1.AdjAmt;
                Decimal num = 0M;
                if (adjAmt.GetValueOrDefault() > num & adjAmt.HasValue)
                  apply.Add((PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.AR.ExternalTransaction>) pxResult2);
              }
            }
          }
        }
      }
    }
    return apply;
  }

  protected virtual PX.Objects.AR.ARAdjust CreateApplicationFromPayment(
    PXCache adjCache,
    PX.Objects.AR.ARPayment payment)
  {
    bool flag1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current?.DocType == "CRM";
    bool flag2 = payment.DocType == "CRM";
    PX.Objects.AR.ARAdjust instance = (PX.Objects.AR.ARAdjust) adjCache.CreateInstance();
    instance.CustomerID = payment.CustomerID;
    instance.AdjdCustomerID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CustomerID;
    instance.AdjdDocType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType;
    instance.AdjdRefNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.RefNbr;
    instance.AdjdLineNbr = new int?(0);
    instance.AdjdBranchID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.BranchID;
    instance.AdjdFinPeriodID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.FinPeriodID;
    instance.AdjgDocType = payment.DocType;
    instance.AdjgRefNbr = payment.RefNbr;
    instance.AdjgBranchID = payment.BranchID;
    instance.AdjgFinPeriodID = payment.FinPeriodID;
    instance.AdjNbr = payment.AdjCntr;
    instance.InvoiceID = flag1 ? new Guid?() : ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.NoteID;
    instance.PaymentID = flag1 || !flag2 ? payment.NoteID : new Guid?();
    instance.MemoID = flag1 ? ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.NoteID : (flag2 ? payment.NoteID : new Guid?());
    instance.Recalculatable = new bool?(!((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.IsTaxValid.GetValueOrDefault() && this.IsExternalTax(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxZoneID));
    PX.Objects.AR.ARAdjust arAdjust1 = (PX.Objects.AR.ARAdjust) adjCache.Locate((object) instance);
    if (arAdjust1 != null && !EnumerableExtensions.IsIn<PXEntryStatus>(adjCache.GetStatus((object) arAdjust1), (PXEntryStatus) 4, (PXEntryStatus) 3))
      return (PX.Objects.AR.ARAdjust) null;
    instance.AdjgCuryInfoID = payment.CuryInfoID;
    instance.AdjdOrigCuryInfoID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryInfoID;
    instance.AdjdCuryInfoID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryInfoID;
    PXSelectorAttribute.StoreCached<ARAdjust2.adjgRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) instance, (object) payment);
    this.CalcBalancesFromInvoiceSide(instance, payment, false, false);
    try
    {
      return (PX.Objects.AR.ARAdjust) adjCache.Insert((object) instance);
    }
    catch (PXException ex) when (flag1)
    {
      PX.Objects.AR.ARAdjust arAdjust2 = PXResultset<PX.Objects.AR.ARAdjust>.op_Implicit(PXSelectBase<PX.Objects.AR.ARAdjust, PXSelectReadonly<PX.Objects.AR.ARAdjust, Where<PX.Objects.AR.ARAdjust.adjgDocType, Equal<Current<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, Equal<Current<PX.Objects.AR.ARPayment.refNbr>>, And<PX.Objects.AR.ARAdjust.released, NotEqual<True>, And<PX.Objects.AR.ARAdjust.voided, NotEqual<True>, And<Where<PX.Objects.AR.ARAdjust.adjdDocType, NotEqual<Current<PX.Objects.AR.ARInvoice.docType>>, Or<PX.Objects.AR.ARAdjust.adjdRefNbr, NotEqual<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
      {
        (object) payment,
        (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current
      }, Array.Empty<object>()));
      if (arAdjust2 != null)
        throw new PXException("Cannot create a credit memo because the {0} customer refund has already been applied to an unreleased credit memo. To create another credit memo, release the {1} credit memo first.", new object[2]
        {
          (object) payment.RefNbr,
          (object) arAdjust2.AdjdRefNbr
        });
      throw;
    }
  }

  protected virtual void InvoiceCreated(PX.Objects.AR.ARInvoice invoice, InvoiceOrderArgs args)
  {
  }

  public virtual string GetInvoiceDocType(
    SOOrderType soOrderType,
    SOOrder order,
    string shipmentOperation)
  {
    switch (soOrderType.ARDocType)
    {
      case "INC":
        Decimal? curyOrderTotal1 = order.CuryOrderTotal;
        Decimal num1 = 0M;
        if (!(curyOrderTotal1.GetValueOrDefault() >= num1 & curyOrderTotal1.HasValue) || !(order.DefaultOperation == "I"))
        {
          Decimal? curyOrderTotal2 = order.CuryOrderTotal;
          Decimal num2 = 0M;
          if (!(curyOrderTotal2.GetValueOrDefault() < num2 & curyOrderTotal2.HasValue) || !(order.DefaultOperation == "R"))
            return "CRM";
        }
        return "INV";
      case "CSR":
        Decimal? curyOrderTotal3 = order.CuryOrderTotal;
        Decimal num3 = 0M;
        if (!(curyOrderTotal3.GetValueOrDefault() >= num3 & curyOrderTotal3.HasValue) || !(order.DefaultOperation == "I"))
        {
          Decimal? curyOrderTotal4 = order.CuryOrderTotal;
          Decimal num4 = 0M;
          if (!(curyOrderTotal4.GetValueOrDefault() < num4 & curyOrderTotal4.HasValue) || !(order.DefaultOperation == "R"))
            return "RCS";
        }
        return "CSL";
      default:
        if ((!(ARInvoiceType.DrCr(soOrderType.ARDocType) == "C") || !(shipmentOperation == "R")) && (!(ARInvoiceType.DrCr(soOrderType.ARDocType) == "D") || !(shipmentOperation == "I")))
          return soOrderType.ARDocType;
        if (EnumerableExtensions.IsIn<string>(soOrderType.ARDocType, "INV", "DRM"))
          return "CRM";
        if (soOrderType.ARDocType == "CRM")
          return "INV";
        if (soOrderType.ARDocType == "CSL")
          return "RCS";
        return !(soOrderType.ARDocType == "RCS") ? (string) null : "CSL";
    }
  }

  public virtual bool HasOrderShipmentTransactions(SOOrderShipment orderShipment)
  {
    return ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Search<PX.Objects.AR.ARTran.sOOrderType, PX.Objects.AR.ARTran.sOOrderNbr, PX.Objects.AR.ARTran.sOShipmentType, PX.Objects.AR.ARTran.sOShipmentNbr>((object) orderShipment.OrderType, (object) orderShipment.OrderNbr, (object) orderShipment.ShipmentType, (object) orderShipment.ShipmentNbr, Array.Empty<object>()).Count > 0;
  }

  public virtual PXResultset<SOShipLine> SelectLinesToInvoice(SOOrderShipment orderShipment)
  {
    return PXSelectBase<SOShipLine, PXSelectJoin<SOShipLine, InnerJoin<SOLine, On<SOLine.orderType, Equal<SOShipLine.origOrderType>, And<SOLine.orderNbr, Equal<SOShipLine.origOrderNbr>, And<SOLine.lineNbr, Equal<SOShipLine.origLineNbr>>>>, LeftJoin<SOSalesPerTran, On<SOLine.orderType, Equal<SOSalesPerTran.orderType>, And<SOLine.orderNbr, Equal<SOSalesPerTran.orderNbr>, And<SOLine.salesPersonID, Equal<SOSalesPerTran.salespersonID>>>>, LeftJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.sOShipmentNbr, Equal<SOShipLine.shipmentNbr>, And<PX.Objects.AR.ARTran.sOShipmentType, Equal<SOShipLine.shipmentType>, And<PX.Objects.AR.ARTran.sOShipmentLineGroupNbr, Equal<SOShipLine.invoiceGroupNbr>, And<PX.Objects.AR.ARTran.sOOrderType, Equal<SOShipLine.origOrderType>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<SOShipLine.origOrderNbr>, And<PX.Objects.AR.ARTran.sOOrderLineNbr, Equal<SOShipLine.origLineNbr>, And<PX.Objects.AR.ARTran.canceled, NotEqual<True>, And<PX.Objects.AR.ARTran.isCancellation, NotEqual<True>>>>>>>>>, LeftJoin<ARTranAccrueCost, On<ARTranAccrueCost.tranType, Equal<SOLine.invoiceType>, And<ARTranAccrueCost.refNbr, Equal<SOLine.invoiceNbr>, And<ARTranAccrueCost.lineNbr, Equal<SOLine.invoiceLineNbr>>>>>>>>, Where<SOShipLine.shipmentNbr, Equal<Required<SOShipLine.shipmentNbr>>, And<SOShipLine.shipmentType, Equal<Required<SOShipLine.shipmentType>>, And<SOShipLine.origOrderType, Equal<Required<SOShipLine.origOrderType>>, And<SOShipLine.origOrderNbr, Equal<Required<SOShipLine.origOrderNbr>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) orderShipment.ShipmentNbr,
      (object) orderShipment.ShipmentType,
      (object) orderShipment.OrderType,
      (object) orderShipment.OrderNbr
    });
  }

  private void ClearCache()
  {
    PXCache<SOOrder> pxCache1 = GraphHelper.Caches<SOOrder>((PXGraph) this);
    ((PXCache) pxCache1).ClearQueryCache();
    ((PXCache) pxCache1).Clear();
    PXCache<SOTaxTran> pxCache2 = GraphHelper.Caches<SOTaxTran>((PXGraph) this);
    ((PXCache) pxCache2).ClearQueryCache();
    ((PXCache) pxCache2).Clear();
  }

  private void InvoiceOrderImpl(InvoiceOrderArgs args)
  {
    SOOrderShipment soOrderShipment1 = args.OrderShipment;
    SOOrder soOrder1 = args.SOOrder;
    PX.Objects.CM.Extensions.CurrencyInfo soCuryInfo = args.SoCuryInfo;
    SOAddress soBillAddress = args.SoBillAddress;
    SOContact soBillContact = args.SoBillContact;
    SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this, soOrder1.OrderType);
    SOShipment shipment = this.GetShipment(soOrderShipment1);
    OpenPeriodAttribute.SetValidatePeriod<PX.Objects.AR.ARInvoice.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.Nothing);
    PX.Objects.AR.ARInvoice arInvoice1;
    bool? nullable1;
    int? nullable2;
    int? nullable3;
    if (args.List != null)
    {
      DateTime orderInvoiceDate = this.GetOrderInvoiceDate(args.InvoiceDate, soOrder1, soOrderShipment1);
      PX.Objects.AR.ARInvoice orCreateInvoice = this.FindOrCreateInvoice(orderInvoiceDate, args);
      if (orCreateInvoice.RefNbr != null)
      {
        PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And2<Where<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>> document1 = this.Document;
        PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.ARInvoice.customerID>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And2<Where<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>> document2 = this.Document;
        string refNbr = orCreateInvoice.RefNbr;
        object[] objArray = new object[1]
        {
          (object) orCreateInvoice.DocType
        };
        PX.Objects.AR.ARInvoice arInvoice2;
        arInvoice1 = arInvoice2 = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) document2).Search<PX.Objects.AR.ARInvoice.refNbr>((object) refNbr, objArray));
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) document1).Current = arInvoice2;
        nullable1 = arInvoice1.Released;
        if (nullable1.GetValueOrDefault())
          throw new PXInvalidOperationException("Document Status is invalid for processing.");
      }
      else
      {
        ((PXGraph) this).Clear();
        orCreateInvoice.DocType = this.GetInvoiceDocType(soOrderType, soOrder1, soOrderShipment1.Operation);
        orCreateInvoice.Hold = new bool?(args.QuickProcessFlow != 2 && soOrderType.InvoiceHoldEntry.GetValueOrDefault() || EPApprovalSettings<SOSetupInvoiceApproval, SOSetupInvoiceApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.ApprovableDocTypes.Contains(orCreateInvoice.DocType));
        orCreateInvoice.DocDate = new DateTime?(orderInvoiceDate);
        orCreateInvoice.BranchID = soOrder1.BranchID;
        orCreateInvoice.IsPaymentsTransferred = new bool?(!args.OptimizeExternalTaxCalc || !this.IsExternalTax(soOrder1.TaxZoneID));
        if (!string.IsNullOrEmpty(soOrder1.FinPeriodID))
          orCreateInvoice.FinPeriodID = soOrder1.FinPeriodID;
        if (soOrder1.InvoiceNbr != null)
        {
          orCreateInvoice.RefNbr = soOrder1.InvoiceNbr;
          orCreateInvoice.RefNoteID = soOrder1.NoteID;
        }
        if (soOrderType.UserInvoiceNumbering.GetValueOrDefault() && string.IsNullOrEmpty(orCreateInvoice.RefNbr))
          throw new PXException("'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<SOOrder.invoiceNbr>(((PXSelectBase) this.soorder).Cache)
          });
        bool? nullable4 = soOrderType.UserInvoiceNumbering;
        bool flag1 = false;
        if (nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue && !string.IsNullOrEmpty(orCreateInvoice.RefNbr))
          throw new PXException("Invoice Number is specified, Manual Numbering should be activated for '{0}'.", new object[1]
          {
            (object) soOrderType.InvoiceNumberingID
          });
        AutoNumberAttribute.SetNumberingId<PX.Objects.AR.ARInvoice.refNbr>(((PXSelectBase) this.Document).Cache, orCreateInvoice.DocType, soOrderType.InvoiceNumberingID);
        arInvoice1 = (PX.Objects.AR.ARInvoice) ((PXSelectBase) this.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Insert(orCreateInvoice));
        arInvoice1.CustomerID = soOrder1.CustomerID;
        arInvoice1.CustomerLocationID = soOrder1.CustomerLocationID;
        if (!(arInvoice1.DocType != "CRM"))
        {
          if (arInvoice1.DocType == "CRM")
          {
            nullable4 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.arsetup).Current.TermsInCreditMemos;
            if (!nullable4.GetValueOrDefault())
              goto label_16;
          }
          else
            goto label_16;
        }
        arInvoice1.TermsID = soOrder1.TermsID;
        arInvoice1.DiscDate = soOrder1.DiscDate;
        arInvoice1.DueDate = soOrder1.DueDate;
label_16:
        arInvoice1.TaxZoneID = soOrder1.TaxZoneID;
        arInvoice1.TaxCalcMode = soOrder1.TaxCalcMode;
        arInvoice1.ExternalTaxExemptionNumber = soOrder1.ExternalTaxExemptionNumber;
        arInvoice1.AvalaraCustomerUsageType = soOrder1.AvalaraCustomerUsageType;
        arInvoice1.SalesPersonID = soOrder1.SalesPersonID;
        arInvoice1.DocDesc = soOrder1.OrderDesc;
        arInvoice1.InvoiceNbr = soOrder1.CustomerOrderNbr;
        arInvoice1.CuryID = soOrder1.CuryID;
        PX.Objects.AR.ARInvoice arInvoice3 = arInvoice1;
        int? nullable5 = soOrder1.ProjectID;
        int? nullable6 = nullable5 ?? ProjectDefaultAttribute.NonProject();
        arInvoice3.ProjectID = nullable6;
        arInvoice1.DisableAutomaticTaxCalculation = soOrder1.DisableAutomaticTaxCalculation;
        nullable4 = soOrderType.MarkInvoicePrinted;
        if (nullable4.GetValueOrDefault())
          arInvoice1.Printed = new bool?(true);
        nullable4 = soOrderType.MarkInvoiceEmailed;
        if (nullable4.GetValueOrDefault())
          arInvoice1.Emailed = new bool?(true);
        nullable5 = soOrder1.PMInstanceID;
        if (nullable5.HasValue || !string.IsNullOrEmpty(soOrder1.PaymentMethodID))
        {
          PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this, soOrder1.PMInstanceID);
          int num;
          if (customerPaymentMethod == null)
          {
            num = 0;
          }
          else
          {
            nullable4 = customerPaymentMethod.IsActive;
            num = nullable4.GetValueOrDefault() ? 1 : 0;
          }
          if (num != 0)
            arInvoice1.PMInstanceID = soOrder1.PMInstanceID;
          arInvoice1.PaymentMethodID = soOrder1.PaymentMethodID;
          arInvoice1.CashAccountID = soOrder1.CashAccountID;
        }
        List<(System.Type, string, PXFieldDefaulting)> valueTupleList = new List<(System.Type, string, PXFieldDefaulting)>()
        {
          this.CancelDefaulting<PX.Objects.AR.ARInvoice.branchID>(),
          this.CancelDefaulting<PX.Objects.AR.ARInvoice.taxZoneID>()
        };
        if (arInvoice1.DocType != "CRM")
        {
          valueTupleList.Add(this.CancelDefaulting<PX.Objects.SO.SOInvoice.paymentMethodID>());
          valueTupleList.Add(this.CancelDefaulting<PX.Objects.SO.SOInvoice.pMInstanceID>());
        }
        try
        {
          arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Update(arInvoice1);
          bool flag2 = false;
          if (arInvoice1.AvalaraCustomerUsageType != soOrder1.AvalaraCustomerUsageType)
          {
            arInvoice1.AvalaraCustomerUsageType = soOrder1.AvalaraCustomerUsageType;
            flag2 = true;
          }
          if (arInvoice1.TaxCalcMode != soOrder1.TaxCalcMode)
          {
            arInvoice1.TaxCalcMode = soOrder1.TaxCalcMode;
            flag2 = true;
          }
          if (flag2)
            arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Update(arInvoice1);
        }
        finally
        {
          foreach ((System.Type, string, PXFieldDefaulting) valueTuple in valueTupleList)
            ((PXGraph) this).FieldDefaulting.RemoveHandler(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
        }
        if (soOrder1.PMInstanceID.HasValue || !string.IsNullOrEmpty(soOrder1.PaymentMethodID))
        {
          PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PX.Objects.AR.CustomerPaymentMethod.PK.Find((PXGraph) this, soOrder1.PMInstanceID);
          int num;
          if (customerPaymentMethod == null)
          {
            num = 0;
          }
          else
          {
            nullable1 = customerPaymentMethod.IsActive;
            num = nullable1.GetValueOrDefault() ? 1 : 0;
          }
          if (num != 0)
            ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.PMInstanceID = soOrder1.PMInstanceID;
          else
            arInvoice1.PMInstanceID = new int?();
          ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.PaymentMethodID = soOrder1.PaymentMethodID;
          nullable2 = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.CashAccountID;
          int? cashAccountId = soOrder1.CashAccountID;
          if (!(nullable2.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable2.HasValue == cashAccountId.HasValue))
            ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).SetValueExt<PX.Objects.SO.SOInvoice.cashAccountID>(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, (object) soOrder1.CashAccountID);
          nullable3 = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.CashAccountID;
          if (!nullable3.HasValue)
            ((PXSelectBase) this.SODocument).Cache.SetDefaultExt<PX.Objects.SO.SOInvoice.cashAccountID>((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current);
          nullable1 = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.ARPaymentDepositAsBatch;
          if (nullable1.GetValueOrDefault() && !((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.DepositAfter.HasValue)
            ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.DepositAfter = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.AdjDate;
          ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.ExtRefNbr = soOrder1.ExtRefNbr;
          ((PXSelectBase) this.SODocument).Cache.RaiseExceptionHandling<PX.Objects.SO.SOInvoice.cashAccountID>((object) ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, (object) null, (Exception) null);
        }
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
        if (!(currencyInfo.CuryRate.GetValueOrDefault() == 0M))
        {
          nullable1 = soOrderType.UseCuryRateFromSO;
          if (!nullable1.GetValueOrDefault())
          {
            currencyInfo.CuryRateTypeID = soCuryInfo.CuryRateTypeID;
            ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo);
            goto label_60;
          }
        }
        PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.RestoreCopy(currencyInfo, soCuryInfo);
        currencyInfo.CuryInfoID = arInvoice1.CuryInfoID;
label_60:
        SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.billAddressID>(((PXSelectBase) this.Document).Cache, (object) arInvoice1, (object) soBillAddress, true);
        if (soBillAddress != null)
        {
          nullable1 = soBillAddress.IsValidated;
          if (nullable1.GetValueOrDefault() && ((PXSelectBase<ARAddress>) this.Billing_Address).Current != null)
            ((PXSelectBase<ARAddress>) this.Billing_Address).Current.IsValidated = new bool?(true);
        }
        SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.billContactID>(((PXSelectBase) this.Document).Cache, (object) arInvoice1, (object) soBillContact, true);
        SOContact source = SOContact.PK.Find((PXGraph) this, soOrderShipment1.ShipContactID);
        SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.shipContactID>(((PXSelectBase) this.Document).Cache, (object) arInvoice1, (object) source, true);
      }
    }
    else
    {
      PX.Objects.AR.ARInvoice copy = (PX.Objects.AR.ARInvoice) ((PXSelectBase) this.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current);
      bool flag = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).SelectSingle(Array.Empty<object>()) == null;
      if (flag)
      {
        copy.CustomerID = soOrder1.CustomerID;
        copy.ProjectID = soOrder1.ProjectID;
        copy.CustomerLocationID = soOrder1.CustomerLocationID;
        copy.SalesPersonID = soOrder1.SalesPersonID;
        copy.TaxZoneID = soOrder1.TaxZoneID;
        copy.TaxCalcMode = soOrder1.TaxCalcMode;
        copy.ExternalTaxExemptionNumber = soOrder1.ExternalTaxExemptionNumber;
        copy.AvalaraCustomerUsageType = soOrder1.AvalaraCustomerUsageType;
        copy.DocDesc = soOrder1.OrderDesc;
        copy.InvoiceNbr = soOrder1.CustomerOrderNbr;
        copy.TermsID = soOrder1.TermsID;
        copy.DisableAutomaticTaxCalculation = soOrder1.DisableAutomaticTaxCalculation;
      }
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
      if ((currencyInfo.CuryRate.GetValueOrDefault() == 0M ? 1 : (!flag ? 0 : (soOrderType.UseCuryRateFromSO.GetValueOrDefault() ? 1 : 0))) != 0)
      {
        PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.RestoreCopy(currencyInfo, soCuryInfo);
        currencyInfo.CuryInfoID = copy.CuryInfoID;
        ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo);
        copy.CuryID = currencyInfo.CuryID;
      }
      else if (!((PXSelectBase) this.currencyinfo).Cache.ObjectsEqual<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((object) currencyInfo, (object) soCuryInfo) || !((PXSelectBase) this.currencyinfo).Cache.ObjectsEqual<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID, PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv, PX.Objects.CM.Extensions.CurrencyInfo.curyRate>((object) currencyInfo, (object) soCuryInfo) && soOrderType.UseCuryRateFromSO.GetValueOrDefault())
        throw new PXException("The sales order {0} cannot be added to the invoice because the currency rate in the sales order differs from the currency rate in the invoice.", new object[1]
        {
          (object) soOrder1.OrderNbr
        });
      arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Update(copy);
      SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.billAddressID>(((PXSelectBase) this.Document).Cache, (object) arInvoice1, (object) soBillAddress, true);
      if (soBillAddress != null && soBillAddress.IsValidated.GetValueOrDefault() && ((PXSelectBase<ARAddress>) this.Billing_Address).Current != null)
        ((PXSelectBase<ARAddress>) this.Billing_Address).Current.IsValidated = new bool?(true);
      SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.billContactID>(((PXSelectBase) this.Document).Cache, (object) arInvoice1, (object) soBillContact, true);
      SOContact source = SOContact.PK.Find((PXGraph) this, soOrderShipment1.ShipContactID);
      SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.shipContactID>(((PXSelectBase) this.Document).Cache, (object) arInvoice1, (object) source, true);
    }
    nullable1 = arInvoice1.DisableAutomaticTaxCalculation;
    if (!nullable1.HasValue)
      arInvoice1.DisableAutomaticTaxCalculation = soOrder1.DisableAutomaticTaxCalculation;
    this.CopyOrderHeaderNoteAndFiles(soOrder1, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current, soOrderType);
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(Array.Empty<object>())) ?? (PX.Objects.SO.SOInvoice) ((PXSelectBase) this.SODocument).Cache.Insert();
    if (soOrderShipment1.ShipmentType == "H")
      soOrderShipment1 = this.UpdateDropShipmentFromPOOrder(soOrderShipment1, soOrder1);
    nullable3 = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.ShipAddressID;
    if (!nullable3.HasValue)
    {
      this.DefaultShippingAddress(arInvoice1, soOrderShipment1, shipment);
    }
    else
    {
      nullable3 = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.ShipAddressID;
      nullable2 = soOrderShipment1.ShipAddressID;
      if (!(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue))
      {
        nullable1 = arInvoice1.MultiShipAddress;
        if (!nullable1.GetValueOrDefault())
        {
          arInvoice1.MultiShipAddress = new bool?(true);
          SharedRecordAttribute.DefaultRecord<PX.Objects.AR.ARInvoice.shipAddressID>(((PXSelectBase) this.Document).Cache, (object) arInvoice1);
        }
      }
    }
    nullable1 = arInvoice1.Hold;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    nullable1 = arInvoice1.Hold;
    if (!nullable1.GetValueOrDefault())
    {
      arInvoice1.Hold = new bool?(true);
      arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Update(arInvoice1);
    }
    this.InvoiceCreated(arInvoice1, args);
    foreach (PXResult<ARInvoiceDiscountDetail> pxResult in ((PXSelectBase<ARInvoiceDiscountDetail>) new PXSelect<ARInvoiceDiscountDetail, Where<ARInvoiceDiscountDetail.docType, Equal<Current<PX.Objects.SO.SOInvoice.docType>>, And<ARInvoiceDiscountDetail.refNbr, Equal<Current<PX.Objects.SO.SOInvoice.refNbr>>, And<ARInvoiceDiscountDetail.orderType, Equal<Required<ARInvoiceDiscountDetail.orderType>>, And<ARInvoiceDiscountDetail.orderNbr, Equal<Required<ARInvoiceDiscountDetail.orderNbr>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) soOrderShipment1.OrderType,
      (object) soOrderShipment1.OrderNbr
    }))
      this.ARDiscountEngine.DeleteDiscountDetail(((PXSelectBase) this.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult));
    TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    this.InsertSOShipLines(args);
    this.ValidateDropShipSOPOLinks(args, soOrderShipment1);
    SOOrderShipment copy1 = PXCache<SOOrderShipment>.CreateCopy(soOrderShipment1);
    copy1.InvoiceType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType;
    copy1.InvoiceNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.RefNbr;
    if (string.Equals(copy1.ShipmentNbr, "<NEW>"))
      copy1.ShippingRefNoteID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.NoteID;
    SOOrderShipment soOrderShipment2 = ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(copy1);
    (HashSet<PX.Objects.AR.ARTran> arTranSet, Dictionary<int, SOSalesPerTran> dctCommissions, DateTime? origInvoiceDate, bool updateINRequired, bool lineAdded) invoiceDetails = this.CreateInvoiceDetails(arInvoice1, args, soOrderShipment2, soOrderType);
    HashSet<PX.Objects.AR.ARTran> arTranSet = invoiceDetails.arTranSet;
    Dictionary<int, SOSalesPerTran> dctCommissions = invoiceDetails.dctCommissions;
    DateTime? origInvoiceDate = invoiceDetails.origInvoiceDate;
    bool updateInRequired = invoiceDetails.updateINRequired;
    HashSet<PX.Objects.AR.ARTran> detailsForMiscLines = this.CreateInvoiceDetailsForMiscLines(arInvoice1, args, soOrderShipment2, soOrderType, dctCommissions);
    EnumerableExtensions.AddRange<PX.Objects.AR.ARTran>((ISet<PX.Objects.AR.ARTran>) arTranSet, (IEnumerable<PX.Objects.AR.ARTran>) detailsForMiscLines);
    this.ValidateLinesAdded(invoiceDetails.lineAdded || detailsForMiscLines.Any<PX.Objects.AR.ARTran>());
    this.ProcessSalespersonCommissions(arInvoice1, args, dctCommissions);
    if (LinesSortingAttribute.AllowSorting<PX.Objects.SO.SOInvoice>(((PXSelectBase) this.Transactions).Cache, ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current))
      this.ResortTransactions(arTranSet, ((PXGraph) this).UnattendedMode);
    PX.Objects.SO.SOInvoice current1 = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current;
    if (current1.InitialSOBehavior == null)
    {
      string behavior;
      current1.InitialSOBehavior = behavior = soOrder1.Behavior;
    }
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.BillAddressID = soOrder1.BillAddressID;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.BillContactID = soOrder1.BillContactID;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.ShipAddressID = soOrderShipment2.ShipAddressID;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.ShipContactID = soOrderShipment2.ShipContactID;
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current.PaymentProjectID = ProjectDefaultAttribute.NonProject();
    PX.Objects.SO.SOInvoice current2 = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current;
    bool? createInDoc = current2.CreateINDoc;
    current2.CreateINDoc = (!updateInRequired ? 0 : (soOrderShipment2.InvtRefNbr == null ? 1 : 0)) != 0 ? new bool?(true) : createInDoc;
    SOFreightDetail soFreightDetail = this.FillFreightDetails(soOrder1, soOrderShipment2);
    SOOrderShipment copy2 = PXCache<SOOrderShipment>.CreateCopy(soOrderShipment2);
    copy2.CreateINDoc = new bool?(updateInRequired);
    SOOrderShipment soOrderShipment3 = ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(copy2).LinkInvoice(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, (PXGraph) this, false);
    if (string.Equals(soOrderShipment3.ShipmentNbr, "<NEW>"))
    {
      SOOrder order = ((PXSelectBase<SOOrder>) this.soorder).Locate(soOrder1);
      if (order != null)
      {
        int? nullable7;
        if (EnumerableExtensions.IsIn<string>(order.Behavior, "SO", "RM"))
        {
          nullable7 = order.OpenLineCntr;
          int num = 0;
          if (nullable7.GetValueOrDefault() == num & nullable7.HasValue)
            order.MarkCompleted();
        }
        SOOrder soOrder2 = order;
        nullable7 = soOrder2.ShipmentCntr;
        int? nullable8 = nullable7;
        soOrder2.ShipmentCntr = nullable8.HasValue ? new int?(nullable8.GetValueOrDefault() + 1) : new int?();
        ((PXSelectBase<SOOrder>) this.soorder).Update(order);
      }
    }
    this.ProcessGroupAndDocumentDiscounts(arInvoice1, args, soOrderShipment3, soOrderType);
    arInvoice1.IsPriceAndDiscountsValid = new bool?(true);
    this.AddOrderTaxes(soOrderShipment3);
    if (!this.IsExternalTax(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxZoneID) && shipment != null && shipment.TaxCategoryID != soOrder1.FreightTaxCategoryID)
    {
      TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
      try
      {
        soFreightDetail.TaxCategoryID = shipment.TaxCategoryID;
        ((PXSelectBase<SOFreightDetail>) this.FreightDetails).Update(soFreightDetail);
      }
      finally
      {
        TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      }
    }
    if (!this.IsExternalTax(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxZoneID) || FlaggedModeScopeBase<SOInvoiceAddOrderPaymentsScope>.IsActive)
      this.InsertApplications(soOrderShipment3);
    PX.Objects.AR.ARInvoice copy3 = (PX.Objects.AR.ARInvoice) ((PXSelectBase) this.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current);
    copy3.OrigDocDate = origInvoiceDate;
    if (copy3.DocType == "CRM")
    {
      PXFormulaAttribute.CalcAggregate<PX.Objects.AR.ARAdjust.curyAdjdAmt>(((PXSelectBase) this.Adjustments_1).Cache, (object) copy3, false);
    }
    else
    {
      PXFormulaAttribute.CalcAggregate<ARAdjust2.adjdRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) copy3, false);
      PXFormulaAttribute.CalcAggregate<ARAdjust2.curyAdjdAmt>(((PXSelectBase) this.Adjustments).Cache, (object) copy3, false);
      PXFormulaAttribute.CalcAggregate<ARAdjust2.curyAdjdWOAmt>(((PXSelectBase) this.Adjustments).Cache, (object) copy3, false);
    }
    PXResultset<SOOrderShipment> invoiceOrderShipments = PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    this.UpdateInvoiceIfItContainsSeveralShipments(copy3, args, invoiceOrderShipments);
    this.ProcessFreeItemDiscountsFromSeveralShipments(copy3, args, invoiceOrderShipments, true);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Update(copy3);
    OpenPeriodAttribute.SetValidatePeriod<PX.Objects.AR.ARInvoice.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    if (args.List == null)
      RestoreHold(valueOrDefault);
    else if (this.HasOrderShipmentTransactions(soOrderShipment3))
    {
      ARInvoiceEntryExternalTax extension = ((PXGraph) this).GetExtension<ARInvoiceEntryExternalTax>();
      try
      {
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.ApplyPaymentWhenTaxAvailable = new bool?(true);
        bool? nullable9;
        if (extension != null)
        {
          nullable9 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.IsPaymentsTransferred;
          if (nullable9.GetValueOrDefault())
            extension.forceTaxCalcOnHold = true;
        }
        if (!this.IsExternalTax(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxZoneID))
        {
          nullable9 = soOrderType.AutoWriteOff;
          if (nullable9.GetValueOrDefault())
            this.AutoWriteOffBalance(args.Customer);
          RestoreHold(valueOrDefault);
        }
        ((PXAction) this.Save).Press();
        if (this.IsExternalTax(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxZoneID))
        {
          nullable9 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.IsPaymentsTransferred;
          if (nullable9.GetValueOrDefault())
          {
            this.ClearCache();
            this.InsertApplications(soOrderShipment3);
            nullable9 = soOrderType.AutoWriteOff;
            if (nullable9.GetValueOrDefault())
              this.AutoWriteOffBalance(args.Customer);
            RestoreHold(valueOrDefault);
            ((PXAction) this.Save).Press();
          }
        }
      }
      finally
      {
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.ApplyPaymentWhenTaxAvailable = new bool?(false);
        if (extension != null && ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.IsPaymentsTransferred.GetValueOrDefault())
          extension.forceTaxCalcOnHold = false;
      }
      if (args.List.Find((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current) != null)
        return;
      PX.Objects.CM.Extensions.CurrencyInfo curyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
      args.List.Add(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current, ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current, curyInfo);
    }
    else
      ((PXGraph) this).Clear();

    void RestoreHold(bool toValue)
    {
      PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
      Decimal? curyDocBal = current.CuryDocBal;
      Decimal num = 0M;
      if (!(curyDocBal.GetValueOrDefault() >= num & curyDocBal.HasValue))
        return;
      bool? hold = current.Hold;
      bool flag = toValue;
      if (hold.GetValueOrDefault() == flag & hold.HasValue)
        return;
      current.Hold = new bool?(toValue);
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Update(current);
    }
  }

  protected virtual void CompleteProcessingImpl(PX.Objects.AR.ARInvoice invoice)
  {
    if (invoice.IsPaymentsTransferred.GetValueOrDefault())
      return;
    ((PXGraph) this).Clear();
    ((PXGraph) this).Clear((PXClearOption) 4);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) invoice.RefNbr, new object[1]
    {
      (object) invoice.DocType
    }));
    ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Current = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(Array.Empty<object>()));
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<SOInvoiceEntry, PX.Objects.AR.ARInvoice>(this, (Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.completeProcessing), ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.completeProcessing).GetCaption(),
        (object) ((PXSelectBase) this.Document).Cache.GetRowDescription((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current)
      });
    if (PXTimeStampScope.GetPersistedRecord(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current) != ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current)
      PXTimeStampScope.PutPersisted(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.tstamp
      });
    if (this.IsExternalTax(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxZoneID))
    {
      ARInvoiceEntryExternalTax extension = ((PXGraph) this).GetExtension<ARInvoiceEntryExternalTax>();
      try
      {
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.ApplyPaymentWhenTaxAvailable = new bool?(true);
        if (extension != null)
          extension.forceTaxCalcOnHold = true;
        ((PXAction) this.Save).Press();
      }
      finally
      {
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.ApplyPaymentWhenTaxAvailable = new bool?(false);
        if (extension != null)
          extension.forceTaxCalcOnHold = false;
      }
    }
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) null;
    SOOrderType soOrderType = (SOOrderType) null;
    foreach (PXResult<SOOrderShipment> pxResult in PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      SOOrderShipment orderShipment = PXResult<SOOrderShipment>.op_Implicit(pxResult);
      this.InsertApplications(orderShipment);
      if (soOrderType == null)
      {
        soOrderType = SOOrderType.PK.Find((PXGraph) this, orderShipment.OrderType);
        customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this, orderShipment.CustomerID);
      }
    }
    if (soOrderType.AutoWriteOff.GetValueOrDefault())
      this.AutoWriteOffBalance(customer);
    ((SelectedEntityEvent<PX.Objects.AR.ARInvoice>) PXEntityEventBase<PX.Objects.AR.ARInvoice>.Container<PX.Objects.AR.ARInvoice.Events>.Select((Expression<Func<PX.Objects.AR.ARInvoice.Events, PXEntityEvent<PX.Objects.AR.ARInvoice.Events>>>) (ev => ev.ProcessingCompleted))).FireOn((PXGraph) this, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.Hold = new bool?(soOrderType != null && soOrderType.InvoiceHoldEntry.GetValueOrDefault() || EPApprovalSettings<SOSetupInvoiceApproval, SOSetupInvoiceApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.ApprovableDocTypes.Contains(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType));
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).UpdateCurrent();
    ((PXAction) this.Save).Press();
  }

  public virtual void CompleteProcessingImpl(InvoiceList createdInvoices)
  {
    bool flag = false;
    string str = (string) null;
    foreach (PX.Objects.AR.ARInvoice invoice in GraphHelper.RowCast<PX.Objects.AR.ARInvoice>((IEnumerable) createdInvoices))
    {
      try
      {
        this.CompleteProcessingImpl(invoice);
      }
      catch (Exception ex)
      {
        PXTrace.WriteWarning(ex);
        PXTrace.WriteWarning("The {0} invoice has been created with the Incomplete status because the external tax provider failed to calculate the taxes. To finish processing, click Complete Processing on the Invoices (SO303000) form or use the Complete Processing action on the Process Invoices and Memos (SO505000) form.", new object[1]
        {
          (object) invoice.RefNbr
        });
        if (str != null)
          flag = true;
        str = invoice.RefNbr;
      }
    }
    if (flag)
      throw new PXOperationCompletedWithWarningException("Some invoices have been created with the Incomplete status because the external tax provider failed to calculate the taxes. To finish processing, click Complete Processing on the Invoices (SO303000) form or use the Complete Processing action on the Process Invoices and Memos (SO505000) form.");
    if (str != null)
      throw new PXOperationCompletedWithWarningException("The {0} invoice has been created with the Incomplete status because the external tax provider failed to calculate the taxes. To finish processing, click Complete Processing on the Invoices (SO303000) form or use the Complete Processing action on the Process Invoices and Memos (SO505000) form.", new object[1]
      {
        (object) str
      });
  }

  private PXRowUpdated CreateApprovedBalanceCollectorDelegate(InvoiceOrderArgs args)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    return new PXRowUpdated((object) new SOInvoiceEntry.\u003C\u003Ec__DisplayClass177_0()
    {
      soOrder = args.SOOrder,
      ApprovedBalance = 0M,
      accountedForOrders = new HashSet<SOOrder>((IEqualityComparer<SOOrder>) PXCacheEx.GetComparer(((PXSelectBase) this.soorder).Cache))
    }, __methodptr(\u003CCreateApprovedBalanceCollectorDelegate\u003Eb__0));
  }

  public virtual void InvoiceOrder(InvoiceOrderArgs args)
  {
    this.InvoicePreProcessingValidations(args);
    PXRowUpdated collectorDelegate = this.CreateApprovedBalanceCollectorDelegate(args);
    ARInvoiceEntry_ARInvoiceCustomerCreditExtension implementation = ((PXGraph) this).FindImplementation<ARInvoiceEntry_ARInvoiceCustomerCreditExtension>();
    implementation?.AppendPreUpdatedEvent(typeof (PX.Objects.AR.ARInvoice), collectorDelegate);
    try
    {
      this.InvoiceOrderImpl(args);
    }
    finally
    {
      implementation?.RemovePreUpdatedEvent(typeof (PX.Objects.AR.ARInvoice), collectorDelegate);
      TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
    }
  }

  /// <summary>Copies notes and files from Sales Order to SO Invoice</summary>
  public virtual void CopyOrderHeaderNoteAndFiles(
    SOOrder srcOrder,
    PX.Objects.AR.ARInvoice dstInvoice,
    SOOrderType orderType)
  {
    bool flag = PXNoteAttribute.GetNote(((PXSelectBase) this.Document).Cache, (object) dstInvoice) == null && orderType.CopyHeaderNotesToInvoice.GetValueOrDefault();
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (SOOrder)], (object) srcOrder, ((PXSelectBase) this.Document).Cache, (object) dstInvoice, new bool?(flag), orderType.CopyHeaderFilesToInvoice);
  }

  /// <summary>
  /// This method can be used to add validations that should be performed right before the start of the SO Invoice creation procedure.
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  public virtual void InvoicePreProcessingValidations(InvoiceOrderArgs args)
  {
    SOOrderShipment orderShipment = args.OrderShipment;
    if (orderShipment.ShipmentNbr != "<NEW>" && orderShipment.ShipmentType != "H" && !orderShipment.Confirmed.GetValueOrDefault())
      throw new PXException("The system cannot process the unconfirmed shipment {0}.", new object[1]
      {
        (object) orderShipment.ShipmentNbr
      });
    if (orderShipment.InvoiceNbr != null)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.selectShipment).GetCaption(),
        (object) ((PXSelectBase) this.shipmentlist).Cache.GetRowDescription((object) orderShipment)
      });
  }

  public virtual void InsertSOShipLines(InvoiceOrderArgs args)
  {
    if (args.Details == null)
      return;
    PXCache cach = ((PXGraph) this).Caches[typeof (SOShipLine)];
    foreach (PXResult<SOShipLine, SOLine> detail in (PXResultset<SOShipLine>) args.Details)
    {
      SOShipLine soShipLine1 = PXResult<SOShipLine, SOLine>.op_Implicit(detail);
      SOLine soLine = PXResult<SOShipLine, SOLine>.op_Implicit(detail);
      Decimal? nullable1 = soLine.BaseQty;
      if (Math.Abs(nullable1.Value) >= 0.0000005M)
      {
        nullable1 = soLine.UnassignedQty;
        Decimal num1 = 0.0000005M;
        if (!(nullable1.GetValueOrDefault() >= num1 & nullable1.HasValue))
        {
          nullable1 = soLine.UnassignedQty;
          Decimal num2 = -0.0000005M;
          if (!(nullable1.GetValueOrDefault() <= num2 & nullable1.HasValue))
            goto label_7;
        }
        throw new PXException("One or more lines have unassigned Location and/or Lot/Serial Number");
      }
label_7:
      SOShipLine soShipLine2 = (SOShipLine) cach.Insert((object) soShipLine1);
      if (soShipLine2 != null)
      {
        if (soShipLine2.LineType == "GI")
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, soShipLine2.InventoryID);
          bool? nullable2 = inventoryItem.StkItem;
          bool flag = false;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          {
            nullable2 = inventoryItem.KitItem;
            if (nullable2.GetValueOrDefault())
            {
              soShipLine2.RequireINUpdate = new bool?(PXResultset<SOLineSplit>.op_Implicit(PXSelectBase<SOLineSplit, PXSelectJoin<SOLineSplit, InnerJoin<PX.Objects.IN.InventoryItem, On2<SOLineSplit.FK.InventoryItem, And<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>>, Where<SOLineSplit.orderType, Equal<Current<SOLine.orderType>>, And<SOLineSplit.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOLineSplit.lineNbr, Equal<Current<SOLine.lineNbr>>, And<SOLineSplit.qty, Greater<Zero>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
              {
                (object) soLine
              }, Array.Empty<object>())) != null);
              continue;
            }
          }
          soShipLine2.RequireINUpdate = inventoryItem.StkItem;
        }
        else
          soShipLine2.RequireINUpdate = new bool?(false);
      }
    }
  }

  /// <summary>
  /// Validates that drop-ship PO has no lines that are not linked with SO
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  /// <param name="currentOrderShipment">Current SOOrderShipment. It can potentially be different from its original value that is stored in the args</param>
  public virtual void ValidateDropShipSOPOLinks(
    InvoiceOrderArgs args,
    SOOrderShipment currentOrderShipment)
  {
    if (!(currentOrderShipment.ShipmentType == "H"))
      return;
    PXResultset<PX.Objects.PO.POReceiptLine> pxResultset = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) new PXSelectJoin<PX.Objects.PO.POReceiptLine, InnerJoin<PX.Objects.PO.POLine, On<PX.Objects.PO.POLine.orderType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<PX.Objects.PO.POLine.orderNbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<PX.Objects.PO.POLine.lineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>, LeftJoin<SOLineSplit, On<SOLineSplit.pOType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<SOLineSplit.pONbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<SOLineSplit.pOLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<POReceiptType.poreceipt>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<SOLineSplit.pOLineNbr, IsNull, And<Where<PX.Objects.PO.POReceiptLine.lineType, Equal<POLineType.goodsForDropShip>, Or<PX.Objects.PO.POReceiptLine.lineType, Equal<POLineType.nonStockForDropShip>>>>>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) currentOrderShipment.ShipmentNbr
    });
    if (pxResultset.Count > 0)
    {
      foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult in pxResultset)
      {
        PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, poReceiptLine.InventoryID);
        PXTrace.WriteError("In the purchase order '{0}', a drop-ship line with the item '{1}' is not linked to any sales order line.", new object[2]
        {
          (object) poReceiptLine.PONbr,
          (object) inventoryItem?.InventoryCD
        });
      }
      throw new PXException("Some of drop-ship lines in the purchase orders are not linked to any sales order lines. Check Trace for more details.");
    }
  }

  /// <summary>
  /// this method copies salesperson commissions from SO to AR
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  public virtual void ProcessSalespersonCommissions(
    PX.Objects.AR.ARInvoice newdoc,
    InvoiceOrderArgs args,
    Dictionary<int, SOSalesPerTran> dctCommissions)
  {
    foreach (SOSalesPerTran soSalesPerTran in dctCommissions.Values)
    {
      ARSalesPerTran arSalesPerTran1 = new ARSalesPerTran();
      arSalesPerTran1.DocType = newdoc.DocType;
      arSalesPerTran1.RefNbr = newdoc.RefNbr;
      arSalesPerTran1.SalespersonID = soSalesPerTran.SalespersonID;
      ((PXSelectBase) this.commisionlist).Cache.SetDefaultExt<ARSalesPerTran.adjNbr>((object) arSalesPerTran1);
      ((PXSelectBase) this.commisionlist).Cache.SetDefaultExt<ARSalesPerTran.adjdRefNbr>((object) arSalesPerTran1);
      ((PXSelectBase) this.commisionlist).Cache.SetDefaultExt<ARSalesPerTran.adjdDocType>((object) arSalesPerTran1);
      ARSalesPerTran arSalesPerTran2 = ((PXSelectBase<ARSalesPerTran>) this.commisionlist).Locate(arSalesPerTran1);
      if (arSalesPerTran2 != null)
      {
        Decimal? commnPct1 = arSalesPerTran2.CommnPct;
        Decimal? commnPct2 = soSalesPerTran.CommnPct;
        if (!(commnPct1.GetValueOrDefault() == commnPct2.GetValueOrDefault() & commnPct1.HasValue == commnPct2.HasValue))
        {
          arSalesPerTran2.CommnPct = soSalesPerTran.CommnPct;
          ((PXSelectBase<ARSalesPerTran>) this.commisionlist).Update(arSalesPerTran2);
        }
      }
    }
  }

  /// <summary>
  /// Creates ARTran (Invoice details) records from sales order and shipment lines
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  /// <param name="currentOrderShipment">Current SOOrderShipment. It can potentially be different from its original value that is stored in the args</param>
  public virtual (HashSet<PX.Objects.AR.ARTran> arTranSet, Dictionary<int, SOSalesPerTran> dctCommissions, DateTime? origInvoiceDate, bool updateINRequired, bool lineAdded) CreateInvoiceDetails(
    PX.Objects.AR.ARInvoice newdoc,
    InvoiceOrderArgs args,
    SOOrderShipment currentOrderShipment,
    SOOrderType soOrderType)
  {
    DateTime? nullable1 = new DateTime?();
    bool flag1 = currentOrderShipment.ShipmentType == "H";
    HashSet<PX.Objects.AR.ARTran> arTranSet = new HashSet<PX.Objects.AR.ARTran>((IEqualityComparer<PX.Objects.AR.ARTran>) PXCacheEx.GetComparer(((PXSelectBase) this.Transactions).Cache));
    Dictionary<int, SOSalesPerTran> dictionary1 = new Dictionary<int, SOSalesPerTran>();
    Lazy<ILookup<(int?, int?), PX.Objects.AR.ARTran>> lazy = Lazy.By<ILookup<(int?, int?), PX.Objects.AR.ARTran>>((Func<ILookup<(int?, int?), PX.Objects.AR.ARTran>>) (() => NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Transactions).Cache.Inserted, ((PXSelectBase) this.Transactions).Cache.Updated).OfType<PX.Objects.AR.ARTran>().Where<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (t => string.Equals(t.SOShipmentNbr, currentOrderShipment.ShipmentNbr, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SOOrderNbr, currentOrderShipment.OrderNbr, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SOShipmentType, currentOrderShipment.ShipmentType, StringComparison.OrdinalIgnoreCase) && string.Equals(t.SOOrderType, currentOrderShipment.OrderType, StringComparison.OrdinalIgnoreCase))).ToLookup<PX.Objects.AR.ARTran, (int?, int?)>((Func<PX.Objects.AR.ARTran, (int?, int?)>) (t => (t.SOOrderLineNbr, t.SOShipmentLineGroupNbr)))));
    bool flag2 = false;
    foreach (IGrouping<(int?, int?), PXResult<SOShipLine>> source in ((IEnumerable<PXResult<SOShipLine>>) this.SelectLinesToInvoice(currentOrderShipment)).AsEnumerable<PXResult<SOShipLine>>().GroupBy<PXResult<SOShipLine>, (int?, int?)>((Func<PXResult<SOShipLine>, (int?, int?)>) (r => (PXResult<SOShipLine>.op_Implicit(r).OrigLineNbr, PXResult<SOShipLine>.op_Implicit(r).InvoiceGroupNbr))))
    {
      PX.Objects.AR.ARTran arTran1 = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) source.First<PXResult<SOShipLine>>());
      ARTranAccrueCost arTranAccrueCost = PXResult.Unwrap<ARTranAccrueCost>((object) source.First<PXResult<SOShipLine>>());
      SOLine orderline = PXResult.Unwrap<SOLine>((object) source.First<PXResult<SOShipLine>>());
      SOSalesPerTran soSalesPerTran1 = PXResult.Unwrap<SOSalesPerTran>((object) source.First<PXResult<SOShipLine>>());
      if (soSalesPerTran1 != null)
      {
        int? salespersonId = soSalesPerTran1.SalespersonID;
        if (salespersonId.HasValue)
        {
          Dictionary<int, SOSalesPerTran> dictionary2 = dictionary1;
          salespersonId = soSalesPerTran1.SalespersonID;
          int key1 = salespersonId.Value;
          if (!dictionary2.ContainsKey(key1))
          {
            Dictionary<int, SOSalesPerTran> dictionary3 = dictionary1;
            salespersonId = soSalesPerTran1.SalespersonID;
            int key2 = salespersonId.Value;
            SOSalesPerTran soSalesPerTran2 = soSalesPerTran1;
            dictionary3[key2] = soSalesPerTran2;
          }
        }
      }
      if ((arTran1.RefNbr == null || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Transactions).Cache.GetStatus((object) arTran1), (PXEntryStatus) 3, (PXEntryStatus) 1)) && !lazy.Value.Contains(source.Key))
      {
        SOShipLine[] array = GraphHelper.RowCast<SOShipLine>((IEnumerable) source).Where<SOShipLine>((Func<SOShipLine, bool>) (shipline =>
        {
          Decimal? nullable2;
          if (shipline.ShipmentNbr != null && currentOrderShipment.ShipmentType != "H" && currentOrderShipment.ShipmentNbr != "<NEW>")
          {
            if (shipline.Confirmed.GetValueOrDefault())
            {
              nullable2 = shipline.UnassignedQty;
              Decimal num = 0M;
              if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
                goto label_4;
            }
            throw new PXException("The system cannot process the unconfirmed shipment {0}.", new object[1]
            {
              (object) shipline.ShipmentNbr
            });
          }
label_4:
          nullable2 = shipline.BaseShippedQty;
          return Math.Abs(nullable2.Value) >= 0.0000005M || string.Equals(shipline.ShipmentNbr, "<NEW>");
        })).ToArray<SOShipLine>();
        if (((IEnumerable<SOShipLine>) array).Any<SOShipLine>())
        {
          if (!nullable1.HasValue)
            nullable1 = orderline.InvoiceDate;
          flag2 = true;
          SOShipLine shipline = this.CombineShipLines(orderline, array);
          PX.Objects.AR.ARTran arTran2 = this.CreateTranFromShipLine(newdoc, soOrderType, orderline.Operation, orderline, ref shipline);
          bool? nullable3;
          Decimal? nullable4;
          if (arTranAccrueCost != null)
          {
            nullable3 = arTranAccrueCost.AccrueCost;
            if (nullable3.GetValueOrDefault())
            {
              arTran2.AccrueCost = arTranAccrueCost.AccrueCost;
              arTran2.CostBasis = arTranAccrueCost.CostBasis;
              arTran2.ExpenseAccrualAccountID = arTranAccrueCost.ExpenseAccrualAccountID;
              arTran2.ExpenseAccrualSubID = arTranAccrueCost.ExpenseAccrualSubID;
              arTran2.ExpenseAccountID = arTranAccrueCost.ExpenseAccountID;
              arTran2.ExpenseSubID = arTranAccrueCost.ExpenseSubID;
              nullable4 = arTran2.Qty;
              Decimal num1 = 0M;
              if (!(nullable4.GetValueOrDefault() == num1 & nullable4.HasValue))
              {
                nullable4 = arTranAccrueCost.Qty;
                Decimal num2 = 0M;
                if (!(nullable4.GetValueOrDefault() == num2 & nullable4.HasValue))
                {
                  PX.Objects.AR.ARTran arTran3 = arTran2;
                  nullable4 = arTranAccrueCost.AccruedCost;
                  Decimal? nullable5;
                  Decimal? nullable6;
                  if (!nullable4.HasValue)
                  {
                    Decimal? qty1 = arTran2.Qty;
                    Decimal? qty2 = arTranAccrueCost.Qty;
                    nullable5 = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() / qty2.GetValueOrDefault()) : new Decimal?();
                    nullable6 = nullable5.HasValue ? new Decimal?(0M * nullable5.GetValueOrDefault()) : new Decimal?();
                  }
                  else
                    nullable6 = nullable4;
                  nullable5 = nullable6;
                  Decimal? nullable7 = new Decimal?(PXPriceCostAttribute.Round(nullable5.Value));
                  arTran3.AccruedCost = nullable7;
                }
              }
            }
          }
          try
          {
            this.cancelUnitPriceCalculation = true;
            arTran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Insert(arTran2);
            arTranSet.Add(arTran2);
          }
          catch (PXSetPropertyException ex)
          {
            SOLine entity = new SOLine()
            {
              OrderType = arTran2.SOOrderType,
              OrderNbr = arTran2.SOOrderNbr,
              LineNbr = arTran2.SOOrderLineNbr
            };
            throw new ErrorProcessingEntityException(((PXGraph) this).Caches[((object) entity).GetType()], (IBqlTable) entity, (PXException) ex);
          }
          finally
          {
            this.cancelUnitPriceCalculation = false;
          }
          PXCache cach1 = ((PXGraph) this).Caches[typeof (SOLine)];
          SOLine soLine = orderline;
          PXCache cach2 = ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARTran)];
          PX.Objects.AR.ARTran arTran4 = arTran2;
          nullable3 = soOrderType.CopyLineNotesToInvoice;
          int num3;
          if (nullable3.GetValueOrDefault())
          {
            nullable3 = soOrderType.CopyLineNotesToInvoiceOnlyNS;
            bool flag3 = false;
            num3 = nullable3.GetValueOrDefault() == flag3 & nullable3.HasValue ? 1 : (orderline.LineType == "GN" ? 1 : 0);
          }
          else
            num3 = 0;
          bool? nullable8 = new bool?(num3 != 0);
          nullable3 = soOrderType.CopyLineFilesToInvoice;
          int num4;
          if (nullable3.GetValueOrDefault())
          {
            nullable3 = soOrderType.CopyLineFilesToInvoiceOnlyNS;
            bool flag4 = false;
            num4 = nullable3.GetValueOrDefault() == flag4 & nullable3.HasValue ? 1 : (orderline.LineType == "GN" ? 1 : 0);
          }
          else
            num4 = 0;
          bool? nullable9 = new bool?(num4 != 0);
          PXNoteAttribute.CopyNoteAndFiles(cach1, (object) soLine, cach2, (object) arTran4, nullable8, nullable9);
          nullable3 = arTran2.RequireINUpdate;
          if (nullable3.GetValueOrDefault())
          {
            nullable4 = arTran2.Qty;
            Decimal num5 = 0M;
            if (!(nullable4.GetValueOrDefault() == num5 & nullable4.HasValue))
              flag1 = true;
          }
        }
      }
    }
    return (arTranSet, dictionary1, nullable1, flag1, flag2);
  }

  /// <summary>
  /// Creates Invoice details for Misc. sales order lines only
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  /// <param name="currentOrderShipment">Current SOOrderShipment. It can potentially be different from its original value that is stored in the args</param>
  public virtual HashSet<PX.Objects.AR.ARTran> CreateInvoiceDetailsForMiscLines(
    PX.Objects.AR.ARInvoice newdoc,
    InvoiceOrderArgs args,
    SOOrderShipment currentOrderShipment,
    SOOrderType soOrderType,
    Dictionary<int, SOSalesPerTran> dctCommissions)
  {
    HashSet<PX.Objects.AR.ARTran> detailsForMiscLines = new HashSet<PX.Objects.AR.ARTran>((IEqualityComparer<PX.Objects.AR.ARTran>) PXCacheEx.GetComparer(((PXSelectBase) this.Transactions).Cache));
    PXSelectBase<PX.Objects.AR.ARTran> pxSelectBase = (PXSelectBase<PX.Objects.AR.ARTran>) new PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.sOOrderType, Equal<Current<SOMiscLine2.orderType>>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<Current<SOMiscLine2.orderNbr>>, And<PX.Objects.AR.ARTran.sOOrderLineNbr, Equal<Current<SOMiscLine2.lineNbr>>>>>>>>((PXGraph) this);
    foreach (PXResult<SOMiscLine2, SOSalesPerTran> pxResult in PXSelectBase<SOMiscLine2, PXSelectJoin<SOMiscLine2, LeftJoin<SOSalesPerTran, On<SOMiscLine2.orderType, Equal<SOSalesPerTran.orderType>, And<SOMiscLine2.orderNbr, Equal<SOSalesPerTran.orderNbr>, And<SOMiscLine2.salesPersonID, Equal<SOSalesPerTran.salespersonID>>>>>, Where<SOMiscLine2.orderType, Equal<Required<SOMiscLine2.orderType>>, And<SOMiscLine2.orderNbr, Equal<Required<SOMiscLine2.orderNbr>>, And<Where2<Where<SOMiscLine2.curyUnbilledAmt, Greater<decimal0>, And<SOMiscLine2.curyLineAmt, Greater<decimal0>>>, Or2<Where<SOMiscLine2.curyUnbilledAmt, Less<decimal0>, And<SOMiscLine2.curyLineAmt, Less<decimal0>>>, Or2<Where<SOMiscLine2.curyLineAmt, Equal<decimal0>, And<SOMiscLine2.unbilledQty, Greater<decimal0>, And<SOMiscLine2.orderQty, Greater<decimal0>>>>, Or<Where<SOMiscLine2.curyLineAmt, Equal<decimal0>, And<SOMiscLine2.unbilledQty, Less<decimal0>, And<SOMiscLine2.orderQty, Less<decimal0>>>>>>>>>>>, OrderBy<Desc<Switch<Case<Where<SOMiscLine2.curyUnbilledAmt, GreaterEqual<decimal0>>, decimal1>, decimal0>, Asc<SOMiscLine2.lineNbr>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) currentOrderShipment.OrderType,
      (object) currentOrderShipment.OrderNbr
    }))
    {
      SOMiscLine2 orderline = PXResult<SOMiscLine2, SOSalesPerTran>.op_Implicit(pxResult);
      SOSalesPerTran soSalesPerTran = PXResult<SOMiscLine2, SOSalesPerTran>.op_Implicit(pxResult);
      if (soSalesPerTran != null && soSalesPerTran.SalespersonID.HasValue && !dctCommissions.ContainsKey(soSalesPerTran.SalespersonID.Value))
        dctCommissions[soSalesPerTran.SalespersonID.Value] = soSalesPerTran;
      if (((PXSelectBase) pxSelectBase).View.SelectSingleBound(new object[2]
      {
        (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current,
        (object) orderline
      }, Array.Empty<object>()) == null)
      {
        PX.Objects.AR.ARTran tranFromMiscLine = this.CreateTranFromMiscLine(currentOrderShipment, orderline);
        this.ChangeBalanceSign(tranFromMiscLine, newdoc, orderline.DefaultOperation);
        PX.Objects.AR.ARTran arTran = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Insert(tranFromMiscLine);
        detailsForMiscLines.Add(arTran);
        PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (SOMiscLine2)], (object) orderline, ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARTran)], (object) arTran, soOrderType.CopyLineNotesToInvoice, soOrderType.CopyLineFilesToInvoice);
      }
    }
    return detailsForMiscLines;
  }

  /// <summary>
  /// This method prepares group and document discounts for SO invoice. Discounts are either prorated from the originating SO to the AR document, or recalculated on the AR level.
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  /// <param name="currentOrderShipment">Current SOOrderShipment. It can potentially be different from its original value that is stored in the args</param>
  public virtual void ProcessGroupAndDocumentDiscounts(
    PX.Objects.AR.ARInvoice newdoc,
    InvoiceOrderArgs args,
    SOOrderShipment currentOrderShipment,
    SOOrderType soOrderType)
  {
    SOOrder soOrder = args.SOOrder;
    PXSelectBase<SOLine> transactions = (PXSelectBase<SOLine>) new PXSelect<SOLine, Where<SOLine.orderType, Equal<Required<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Required<SOOrder.orderNbr>>>>>((PXGraph) this);
    PXSelectBase<SOOrderDiscountDetail> discountdetail = (PXSelectBase<SOOrderDiscountDetail>) new PXSelect<SOOrderDiscountDetail, Where<SOOrderDiscountDetail.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Required<SOOrder.orderNbr>>>>>((PXGraph) this);
    Lazy<bool> lazy1 = new Lazy<bool>((Func<bool>) (() => this.IsFullOrderInvoicing(soOrder, soOrderType, transactions)));
    Lazy<TwoWayLookup<SOOrderDiscountDetail, SOLine>> discountCodesWithApplicableSOLines = new Lazy<TwoWayLookup<SOOrderDiscountDetail, SOLine>>((Func<TwoWayLookup<SOOrderDiscountDetail, SOLine>>) (() => DiscountEngineProvider.GetEngineFor<SOLine, SOOrderDiscountDetail>().GetListOfLinksBetweenDiscountsAndDocumentLines(((PXGraph) this).Caches[typeof (SOLine)], transactions, discountdetail, (object[]) new string[2]
    {
      soOrder.OrderType,
      soOrder.OrderNbr
    }, (object[]) new string[2]
    {
      soOrder.OrderType,
      soOrder.OrderNbr
    })));
    Lazy<bool> lazy2 = new Lazy<bool>((Func<bool>) (() => discountCodesWithApplicableSOLines.Value.LeftValues.Any<SOOrderDiscountDetail>((Func<SOOrderDiscountDetail, bool>) (dd => dd.IsManual.GetValueOrDefault()))));
    Lazy<bool> lazy3 = new Lazy<bool>((Func<bool>) (() => discountCodesWithApplicableSOLines.Value.LeftValues.Any<SOOrderDiscountDetail>((Func<SOOrderDiscountDetail, bool>) (dd => dd.Type == "B"))));
    if (!soOrderType.RecalculateDiscOnPartialShipment.GetValueOrDefault() || lazy3.Value || lazy1.Value && lazy2.Value)
    {
      Decimal? nullable1 = new Decimal?(1M);
      Decimal? lineTotal1 = soOrder.LineTotal;
      Decimal num1 = 0M;
      if (lineTotal1.GetValueOrDefault() > num1 & lineTotal1.HasValue)
      {
        Decimal? lineTotal2 = currentOrderShipment.LineTotal;
        Decimal? lineTotal3 = soOrder.LineTotal;
        nullable1 = lineTotal2.HasValue & lineTotal3.HasValue ? new Decimal?(lineTotal2.GetValueOrDefault() / lineTotal3.GetValueOrDefault()) : new Decimal?();
      }
      TwoWayLookup<ARInvoiceDiscountDetail, PX.Objects.AR.ARTran> twoWayLookup = new TwoWayLookup<ARInvoiceDiscountDetail, PX.Objects.AR.ARTran>((IEqualityComparer<ARInvoiceDiscountDetail>) new ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparer(), (IEqualityComparer<PX.Objects.AR.ARTran>) null, (Func<ARInvoiceDiscountDetail, PX.Objects.AR.ARTran, bool>) null);
      foreach (SOOrderDiscountDetail leftValue1 in discountCodesWithApplicableSOLines.Value.LeftValues)
      {
        bool? nullable2 = soOrderType.RecalculateDiscOnPartialShipment;
        if (nullable2.GetValueOrDefault())
        {
          nullable2 = leftValue1.IsManual;
          if (!nullable2.GetValueOrDefault())
            continue;
        }
        if (leftValue1.Type == "B")
        {
          nullable2 = leftValue1.SkipDiscount;
          if (nullable2.GetValueOrDefault())
            continue;
        }
        bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
        ARInvoiceDiscountDetail invoiceDiscountDetail1 = new ARInvoiceDiscountDetail()
        {
          SkipDiscount = leftValue1.SkipDiscount,
          Type = leftValue1.Type,
          DiscountID = leftValue1.DiscountID,
          DiscountSequenceID = leftValue1.DiscountSequenceID,
          OrderType = flag1 ? leftValue1.OrderType : (string) null,
          OrderNbr = flag1 ? leftValue1.OrderNbr : (string) null,
          DocType = newdoc.DocType,
          RefNbr = newdoc.RefNbr,
          IsManual = leftValue1.IsManual,
          DiscountPct = leftValue1.DiscountPct,
          FreeItemID = leftValue1.FreeItemID,
          FreeItemQty = leftValue1.FreeItemQty,
          ExtDiscCode = leftValue1.ExtDiscCode,
          Description = leftValue1.Description,
          CuryInfoID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryInfoID
        };
        Decimal? rate = nullable1;
        Decimal num2 = 0M;
        Decimal num3 = 0M;
        foreach (SOLine soLine in discountCodesWithApplicableSOLines.Value.RightsFor(leftValue1))
        {
          foreach (PXResult<PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Select(Array.Empty<object>()))
          {
            PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
            int num4;
            if (soLine.OrderType == arTran.SOOrderType && soLine.OrderNbr == arTran.SOOrderNbr)
            {
              int? lineNbr = soLine.LineNbr;
              int? soOrderLineNbr = arTran.SOOrderLineNbr;
              if (lineNbr.GetValueOrDefault() == soOrderLineNbr.GetValueOrDefault() & lineNbr.HasValue == soOrderLineNbr.HasValue)
              {
                num4 = flag1 ? 1 : (currentOrderShipment.ShipmentNbr.Trim() == arTran.SOShipmentNbr.Trim() ? 1 : 0);
                goto label_17;
              }
            }
            num4 = 0;
label_17:
            if (num4 != 0)
            {
              bool flag2 = false;
              if (arTran.DiscountID != null)
              {
                ARDiscount arDiscount = ARDiscount.PK.Find((PXGraph) this, arTran.DiscountID);
                int num5;
                if (arDiscount == null)
                {
                  num5 = 0;
                }
                else
                {
                  nullable2 = arDiscount.ExcludeFromDiscountableAmt;
                  num5 = nullable2.GetValueOrDefault() ? 1 : 0;
                }
                if (num5 != 0)
                  flag2 = true;
              }
              if (!flag2)
              {
                if (leftValue1.Type == "G")
                  num2 += arTran.CuryTranAmt.GetValueOrDefault();
                if (EnumerableExtensions.IsIn<string>(leftValue1.Type, "D", "B"))
                {
                  Decimal num6 = num3;
                  Decimal? nullable3 = arTran.CuryTranAmt;
                  Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
                  nullable3 = arTran.CuryTranAmt;
                  Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
                  nullable3 = soLine.GroupDiscountRate;
                  Decimal num7 = 1M - (nullable3 ?? 1M);
                  Decimal num8 = valueOrDefault2 * num7;
                  Decimal num9 = valueOrDefault1 - num8;
                  num3 = num6 + num9;
                }
              }
            }
            if (num4 != 0 || !flag1)
              twoWayLookup.Link(invoiceDiscountDetail1, arTran);
          }
        }
        bool flag3 = lazy1.Value && leftValue1.Type == "D";
        Decimal? nullable4;
        if (flag3)
        {
          rate = new Decimal?(1M);
        }
        else
        {
          Decimal? curyDiscountableAmt = leftValue1.CuryDiscountableAmt;
          Decimal num10 = 0M;
          if (curyDiscountableAmt.GetValueOrDefault() > num10 & curyDiscountableAmt.HasValue)
          {
            if (leftValue1.Type == "G")
            {
              Decimal num11 = num2;
              nullable4 = leftValue1.CuryDiscountableAmt;
              rate = nullable4.HasValue ? new Decimal?(num11 / nullable4.GetValueOrDefault()) : new Decimal?();
            }
            else
            {
              Decimal? lineTotal4 = soOrder.LineTotal;
              Decimal num12 = 0M;
              if (lineTotal4.GetValueOrDefault() == num12 & lineTotal4.HasValue)
              {
                Decimal? miscTot = soOrder.MiscTot;
                Decimal num13 = 0M;
                if (miscTot.GetValueOrDefault() == num13 & miscTot.HasValue)
                  goto label_47;
              }
              Decimal num14 = num3;
              nullable4 = leftValue1.CuryDiscountableAmt;
              rate = nullable4.HasValue ? new Decimal?(num14 / nullable4.GetValueOrDefault()) : new Decimal?();
            }
          }
        }
label_47:
        ARInvoiceDiscountDetail invoiceDiscountDetail2 = ((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).Locate(invoiceDiscountDetail1);
        if (invoiceDiscountDetail2 == null)
        {
          List<ARInvoiceDiscountDetail> invoiceDiscountDetailList = new List<ARInvoiceDiscountDetail>();
          if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
          {
            foreach (ARInvoiceDiscountDetail invoiceDiscountDetail3 in ((PXSelectBase) this.ARDiscountDetails).Cache.Cached)
            {
              if (EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.ARDiscountDetails).Cache.GetStatus((object) invoiceDiscountDetail3), (PXEntryStatus) 3, (PXEntryStatus) 4))
                invoiceDiscountDetailList.Add(invoiceDiscountDetail3);
            }
          }
          else
          {
            foreach (PXResult<ARInvoiceDiscountDetail> pxResult in ((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).Select(Array.Empty<object>()))
            {
              ARInvoiceDiscountDetail invoiceDiscountDetail4 = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult);
              invoiceDiscountDetailList.Add(invoiceDiscountDetail4);
            }
          }
          foreach (ARInvoiceDiscountDetail invoiceDiscountDetail5 in invoiceDiscountDetailList)
          {
            if (invoiceDiscountDetail5.DiscountID == invoiceDiscountDetail1.DiscountID && invoiceDiscountDetail5.DiscountSequenceID == invoiceDiscountDetail1.DiscountSequenceID && invoiceDiscountDetail5.OrderType == invoiceDiscountDetail1.OrderType && invoiceDiscountDetail5.OrderNbr == invoiceDiscountDetail1.OrderNbr && invoiceDiscountDetail5.DocType == invoiceDiscountDetail1.DocType && invoiceDiscountDetail5.RefNbr == invoiceDiscountDetail1.RefNbr && invoiceDiscountDetail5.Type == invoiceDiscountDetail1.Type)
              invoiceDiscountDetail2 = invoiceDiscountDetail5;
          }
        }
        ARInvoiceDiscountDetail discountDetail2;
        if (invoiceDiscountDetail2 != null)
        {
          if (leftValue1.Type == "G" | flag3)
          {
            this.UpdateDiscountDetail(invoiceDiscountDetail2, leftValue1, rate);
          }
          else
          {
            PXCache cache1 = ((PXSelectBase) this.ARDiscountDetails).Cache;
            ARInvoiceDiscountDetail invoiceDiscountDetail6 = invoiceDiscountDetail2;
            nullable4 = invoiceDiscountDetail2.DiscountAmt;
            Decimal? discountAmt = leftValue1.DiscountAmt;
            Decimal? nullable5 = rate;
            Decimal? nullable6 = discountAmt.HasValue & nullable5.HasValue ? new Decimal?(discountAmt.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local1 = (ValueType) (nullable4.HasValue & nullable6.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?());
            cache1.SetValueExt<ARInvoiceDiscountDetail.discountAmt>((object) invoiceDiscountDetail6, (object) local1);
            PXCache cache2 = ((PXSelectBase) this.ARDiscountDetails).Cache;
            ARInvoiceDiscountDetail invoiceDiscountDetail7 = invoiceDiscountDetail2;
            Decimal? nullable7 = invoiceDiscountDetail2.CuryDiscountAmt;
            Decimal? curyDiscountAmt = leftValue1.CuryDiscountAmt;
            Decimal? nullable8 = rate;
            nullable4 = curyDiscountAmt.HasValue & nullable8.HasValue ? new Decimal?(curyDiscountAmt.GetValueOrDefault() * nullable8.GetValueOrDefault()) : new Decimal?();
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local2 = (ValueType) (nullable7.HasValue & nullable4.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?());
            cache2.SetValueExt<ARInvoiceDiscountDetail.curyDiscountAmt>((object) invoiceDiscountDetail7, (object) local2);
            PXCache cache3 = ((PXSelectBase) this.ARDiscountDetails).Cache;
            ARInvoiceDiscountDetail invoiceDiscountDetail8 = invoiceDiscountDetail2;
            nullable4 = invoiceDiscountDetail2.DiscountableAmt;
            Decimal? discountableAmt = leftValue1.DiscountableAmt;
            Decimal? nullable9 = rate;
            nullable7 = discountableAmt.HasValue & nullable9.HasValue ? new Decimal?(discountableAmt.GetValueOrDefault() * nullable9.GetValueOrDefault()) : new Decimal?();
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local3 = (ValueType) (nullable4.HasValue & nullable7.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?());
            cache3.SetValueExt<ARInvoiceDiscountDetail.discountableAmt>((object) invoiceDiscountDetail8, (object) local3);
            PXCache cache4 = ((PXSelectBase) this.ARDiscountDetails).Cache;
            ARInvoiceDiscountDetail invoiceDiscountDetail9 = invoiceDiscountDetail2;
            nullable7 = invoiceDiscountDetail2.CuryDiscountableAmt;
            Decimal? curyDiscountableAmt = leftValue1.CuryDiscountableAmt;
            Decimal? nullable10 = rate;
            nullable4 = curyDiscountableAmt.HasValue & nullable10.HasValue ? new Decimal?(curyDiscountableAmt.GetValueOrDefault() * nullable10.GetValueOrDefault()) : new Decimal?();
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local4 = (ValueType) (nullable7.HasValue & nullable4.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?());
            cache4.SetValueExt<ARInvoiceDiscountDetail.curyDiscountableAmt>((object) invoiceDiscountDetail9, (object) local4);
            PXCache cache5 = ((PXSelectBase) this.ARDiscountDetails).Cache;
            ARInvoiceDiscountDetail invoiceDiscountDetail10 = invoiceDiscountDetail2;
            nullable4 = invoiceDiscountDetail2.DiscountableQty;
            Decimal? discountableQty = leftValue1.DiscountableQty;
            Decimal? nullable11 = rate;
            nullable7 = discountableQty.HasValue & nullable11.HasValue ? new Decimal?(discountableQty.GetValueOrDefault() * nullable11.GetValueOrDefault()) : new Decimal?();
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local5 = (ValueType) (nullable4.HasValue & nullable7.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?());
            cache5.SetValueExt<ARInvoiceDiscountDetail.discountableQty>((object) invoiceDiscountDetail10, (object) local5);
          }
          discountDetail2 = ((PXSelectBase) this.ARDiscountDetails).Cache.GetStatus((object) invoiceDiscountDetail2) != 3 ? this.ARDiscountEngine.UpdateDiscountDetail(((PXSelectBase) this.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, invoiceDiscountDetail2) : this.ARDiscountEngine.InsertDiscountDetail(((PXSelectBase) this.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, invoiceDiscountDetail2);
        }
        else
        {
          this.UpdateDiscountDetail(invoiceDiscountDetail1, leftValue1, rate);
          discountDetail2 = this.ARDiscountEngine.InsertDiscountDetail(((PXSelectBase) this.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, invoiceDiscountDetail1);
        }
        ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparer discountDetailComparer = new ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparer();
        foreach (ARInvoiceDiscountDetail leftValue2 in twoWayLookup.LeftValues)
        {
          if (discountDetailComparer.Equals(leftValue2, discountDetail2))
          {
            leftValue2.DiscountAmt = discountDetail2.DiscountAmt;
            leftValue2.CuryDiscountableAmt = discountDetail2.CuryDiscountableAmt;
            leftValue2.CuryDiscountAmt = discountDetail2.CuryDiscountAmt;
            leftValue2.DiscountableQty = discountDetail2.DiscountableQty;
            leftValue2.DiscountableAmt = discountDetail2.DiscountableAmt;
            leftValue2.IsOrigDocDiscount = discountDetail2.IsOrigDocDiscount;
            leftValue2.LineNbr = discountDetail2.LineNbr;
          }
        }
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
        this.RecalculateTotalDiscount();
      this.ARDiscountEngine.CalculateGroupDiscountRate(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<PX.Objects.AR.ARTran>) new PXSelectJoin<PX.Objects.AR.ARTran, LeftJoin<SOLine, On<SOLine.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<SOLine.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<SOLine.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.sOOrderType, Equal<Required<SOOrder.orderType>>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<Required<SOOrder.orderNbr>>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>((PXGraph) this), (PX.Objects.AR.ARTran) null, twoWayLookup, true, false, (object[]) new string[2]
      {
        soOrder.OrderType,
        soOrder.OrderNbr
      }, forceFormulaCalculation: true, calculateOrigRate: true);
      this.ARDiscountEngine.CalculateDocumentDiscountRate(((PXSelectBase) this.Transactions).Cache, twoWayLookup, (PX.Objects.AR.ARTran) null, (PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions, true, true);
      if (!PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
        this.RecalculateTotalDiscount();
    }
    if (!soOrderType.RecalculateDiscOnPartialShipment.GetValueOrDefault())
      return;
    PX.Objects.AR.ARTran line1 = (PX.Objects.AR.ARTran) null;
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.AR.ARTran line2 = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      if (line1 == null)
        line1 = line2;
      this.RecalculateDiscounts(((PXSelectBase) this.Transactions).Cache, line2, true);
      ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Update(line2);
    }
    if (line1 == null)
      return;
    this.RecalculateDiscounts(((PXSelectBase) this.Transactions).Cache, line1, false, true);
  }

  public virtual void ProcessFreeItemDiscountsFromSeveralShipments(
    PX.Objects.AR.ARInvoice newdoc,
    InvoiceOrderArgs args,
    PXResultset<SOOrderShipment> invoiceOrderShipments,
    bool useLegacyBehavior)
  {
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<SOOrderShipment> invoiceOrderShipment in invoiceOrderShipments)
    {
      SOOrderShipment shipment = PXResult<SOOrderShipment>.op_Implicit(invoiceOrderShipment);
      if (useLegacyBehavior)
      {
        stringSet.Add($"{shipment.OrderType}|{shipment.OrderNbr}");
        if (args.List != null && stringSet.Count > 1)
          break;
      }
      this.ProcessFreeItemDiscounts(newdoc, args, shipment);
    }
  }

  /// <summary>Copies free item discounts from SO to AR</summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  public virtual void ProcessFreeItemDiscounts(
    PX.Objects.AR.ARInvoice newdoc,
    InvoiceOrderArgs args,
    SOOrderShipment shipment)
  {
    foreach (PXResult<SOShipmentDiscountDetail> pxResult1 in ((PXSelectBase<SOShipmentDiscountDetail>) new PXSelect<SOShipmentDiscountDetail, Where<SOShipmentDiscountDetail.orderType, Equal<Required<SOShipmentDiscountDetail.orderType>>, And<SOShipmentDiscountDetail.orderNbr, Equal<Required<SOShipmentDiscountDetail.orderNbr>>, And<SOShipmentDiscountDetail.shipmentNbr, Equal<Required<SOShipmentDiscountDetail.shipmentNbr>>>>>>((PXGraph) this)).Select(new object[3]
    {
      (object) shipment.OrderType,
      (object) shipment.OrderNbr,
      (object) shipment.ShipmentNbr
    }))
    {
      SOShipmentDiscountDetail shipmentDiscountDetail = PXResult<SOShipmentDiscountDetail>.op_Implicit(pxResult1);
      bool flag = false;
      foreach (PXResult<ARInvoiceDiscountDetail> pxResult2 in ((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).Select(Array.Empty<object>()))
      {
        ARInvoiceDiscountDetail invoiceDiscountDetail = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult2);
        if (invoiceDiscountDetail.DocType == newdoc.DocType && invoiceDiscountDetail.RefNbr == newdoc.RefNbr && invoiceDiscountDetail.OrderType == shipment.OrderType && invoiceDiscountDetail.OrderNbr == shipment.OrderNbr && invoiceDiscountDetail.DiscountID == shipmentDiscountDetail.DiscountID && invoiceDiscountDetail.DiscountSequenceID == shipmentDiscountDetail.DiscountSequenceID)
        {
          flag = true;
          if (!invoiceDiscountDetail.FreeItemID.HasValue)
          {
            invoiceDiscountDetail.FreeItemID = shipmentDiscountDetail.FreeItemID;
            invoiceDiscountDetail.FreeItemQty = shipmentDiscountDetail.FreeItemQty;
          }
          else
            invoiceDiscountDetail.FreeItemQty = shipmentDiscountDetail.FreeItemQty;
        }
      }
      if (!flag)
        this.ARDiscountEngine.InsertDiscountDetail(((PXSelectBase) this.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, new ARInvoiceDiscountDetail()
        {
          Type = "G",
          DocType = newdoc.DocType,
          RefNbr = newdoc.RefNbr,
          OrderType = shipmentDiscountDetail.OrderType,
          OrderNbr = shipmentDiscountDetail.OrderNbr,
          DiscountID = shipmentDiscountDetail.DiscountID,
          DiscountSequenceID = shipmentDiscountDetail.DiscountSequenceID,
          FreeItemID = shipmentDiscountDetail.FreeItemID,
          FreeItemQty = shipmentDiscountDetail.FreeItemQty
        });
    }
  }

  public virtual void UpdateInvoiceIfItContainsSeveralShipments(
    PX.Objects.AR.ARInvoice newdoc,
    InvoiceOrderArgs args,
    PXResultset<SOOrderShipment> invoiceOrderShipments)
  {
    List<string> stringList = new List<string>();
    foreach (PXResult<SOOrderShipment> invoiceOrderShipment in invoiceOrderShipments)
    {
      SOOrderShipment soOrderShipment = PXResult<SOOrderShipment>.op_Implicit(invoiceOrderShipment);
      string str = $"{soOrderShipment.OrderType}|{soOrderShipment.OrderNbr}";
      if (!stringList.Contains(str))
        stringList.Add(str);
      if (args.List != null && stringList.Count > 1)
      {
        newdoc.InvoiceNbr = (string) null;
        newdoc.SalesPersonID = new int?();
        newdoc.DocDesc = (string) null;
        break;
      }
    }
  }

  protected virtual (System.Type itemType, string fieldName, PXFieldDefaulting handler) CancelDefaulting<TField>() where TField : class, IBqlField
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOInvoiceEntry.\u003C\u003Ec__DisplayClass190_0<TField> displayClass1900 = new SOInvoiceEntry.\u003C\u003Ec__DisplayClass190_0<TField>();
    System.Type type = typeof (TField);
    System.Type itemType = BqlCommand.GetItemType(type);
    // ISSUE: reference to a compiler-generated field
    displayClass1900.fieldName = type.Name;
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) displayClass1900, __methodptr(\u003CCancelDefaulting\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    ((PXGraph) this).FieldDefaulting.AddHandler(itemType, displayClass1900.fieldName, pxFieldDefaulting);
    // ISSUE: reference to a compiler-generated field
    return (itemType, displayClass1900.fieldName, pxFieldDefaulting);
  }

  protected virtual void ValidateLinesAdded(bool lineAdded)
  {
    if (!lineAdded)
      throw new PXInvalidOperationException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) ((PXSelectBase) this.Transactions).Cache.DisplayName
      });
  }

  private void UpdateDiscountDetail(
    ARInvoiceDiscountDetail dd,
    SOOrderDiscountDetail docGroupDisc,
    Decimal? rate)
  {
    PXCache cache1 = ((PXSelectBase) this.ARDiscountDetails).Cache;
    ARInvoiceDiscountDetail invoiceDiscountDetail1 = dd;
    Decimal? nullable1 = docGroupDisc.DiscountAmt;
    Decimal? nullable2 = rate;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local1 = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
    cache1.SetValueExt<ARInvoiceDiscountDetail.discountAmt>((object) invoiceDiscountDetail1, (object) local1);
    PXCache cache2 = ((PXSelectBase) this.ARDiscountDetails).Cache;
    ARInvoiceDiscountDetail invoiceDiscountDetail2 = dd;
    nullable2 = docGroupDisc.CuryDiscountAmt;
    nullable1 = rate;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local2 = (ValueType) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?());
    cache2.SetValueExt<ARInvoiceDiscountDetail.curyDiscountAmt>((object) invoiceDiscountDetail2, (object) local2);
    PXCache cache3 = ((PXSelectBase) this.ARDiscountDetails).Cache;
    ARInvoiceDiscountDetail invoiceDiscountDetail3 = dd;
    nullable1 = docGroupDisc.DiscountableAmt;
    nullable2 = rate;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local3 = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
    cache3.SetValueExt<ARInvoiceDiscountDetail.discountableAmt>((object) invoiceDiscountDetail3, (object) local3);
    PXCache cache4 = ((PXSelectBase) this.ARDiscountDetails).Cache;
    ARInvoiceDiscountDetail invoiceDiscountDetail4 = dd;
    nullable2 = docGroupDisc.CuryDiscountableAmt;
    nullable1 = rate;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local4 = (ValueType) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?());
    cache4.SetValueExt<ARInvoiceDiscountDetail.curyDiscountableAmt>((object) invoiceDiscountDetail4, (object) local4);
    PXCache cache5 = ((PXSelectBase) this.ARDiscountDetails).Cache;
    ARInvoiceDiscountDetail invoiceDiscountDetail5 = dd;
    nullable1 = docGroupDisc.DiscountableQty;
    nullable2 = rate;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local5 = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
    cache5.SetValueExt<ARInvoiceDiscountDetail.discountableQty>((object) invoiceDiscountDetail5, (object) local5);
  }

  protected virtual void ResortTransactions(HashSet<PX.Objects.AR.ARTran> set, bool fullResort)
  {
    List<Tuple<string, PX.Objects.AR.ARTran>> tupleList = new List<Tuple<string, PX.Objects.AR.ARTran>>();
    int val1 = 0;
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      if (fullResort || set.Contains(arTran))
      {
        string str = $"{arTran.SOOrderType}.{arTran.SOOrderNbr}.{arTran.SOOrderSortOrder:D7}.{arTran.SOShipmentNbr}.{arTran.SOShipmentLineGroupNbr:D7}";
        tupleList.Add(new Tuple<string, PX.Objects.AR.ARTran>(str, arTran));
      }
      else
        val1 = Math.Max(val1, arTran.SortOrder.GetValueOrDefault());
    }
    tupleList.Sort((Comparison<Tuple<string, PX.Objects.AR.ARTran>>) ((x, y) => x.Item1.CompareTo(y.Item1)));
    for (int index = 0; index < tupleList.Count; ++index)
    {
      ++val1;
      int? sortOrder = tupleList[index].Item2.SortOrder;
      int num = val1;
      if (!(sortOrder.GetValueOrDefault() == num & sortOrder.HasValue))
      {
        tupleList[index].Item2.SortOrder = new int?(val1);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) tupleList[index].Item2, true);
      }
    }
    ((PXSelectBase) this.Transactions).Cache.ClearQueryCache();
  }

  public virtual void InsertApplications(SOOrderShipment orderShipment)
  {
    bool flag1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current?.DocType == "CRM";
    PXCache pxCache = flag1 ? ((PXSelectBase) this.Adjustments_1).Cache : ((PXSelectBase) this.Adjustments).Cache;
    Decimal? nullable1 = new Decimal?(0M);
    bool flag2 = false;
    foreach (PXResult<SOAdjust> pxResult in PXSelectBase<SOAdjust, PXSelectJoin<SOAdjust, InnerJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ARPayment.docType, Equal<SOAdjust.adjgDocType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<SOAdjust.adjgRefNbr>>>>, Where<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<PX.Objects.AR.ARPayment.openDoc, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) orderShipment.OrderType,
      (object) orderShipment.OrderNbr
    }))
    {
      SOAdjust soAdjust = PXResult<SOAdjust>.op_Implicit(pxResult);
      PX.Objects.AR.ARAdjust arAdjust1 = (PX.Objects.AR.ARAdjust) null;
      List<PX.Objects.AR.ARAdjust> arAdjustList = (List<PX.Objects.AR.ARAdjust>) null;
      try
      {
        this.TransferApplicationFromSalesOrder = true;
        arAdjustList = flag1 ? GraphHelper.RowCast<PX.Objects.AR.ARAdjust>((IEnumerable) ((PXSelectBase<PX.Objects.AR.ARAdjust>) this.Adjustments_1).Select(Array.Empty<object>())).ToList<PX.Objects.AR.ARAdjust>() : GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase<ARAdjust2>) this.Adjustments).Select(Array.Empty<object>())).Cast<PX.Objects.AR.ARAdjust>().ToList<PX.Objects.AR.ARAdjust>();
      }
      finally
      {
        this.TransferApplicationFromSalesOrder = false;
      }
      foreach (PX.Objects.AR.ARAdjust arAdjust2 in arAdjustList)
      {
        if (flag2)
        {
          Decimal? nullable2 = nullable1;
          Decimal? curyAdjdAmt = arAdjust2.CuryAdjdAmt;
          nullable1 = nullable2.HasValue & curyAdjdAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
        }
        if (string.Equals(arAdjust2.AdjgDocType, soAdjust.AdjgDocType) && string.Equals(arAdjust2.AdjgRefNbr, soAdjust.AdjgRefNbr))
        {
          Decimal? curyAdjdAmt1 = soAdjust.CuryAdjdAmt;
          Decimal num = 0M;
          if (curyAdjdAmt1.GetValueOrDefault() > num & curyAdjdAmt1.HasValue)
          {
            PX.Objects.AR.ARAdjust copy = (PX.Objects.AR.ARAdjust) pxCache.CreateCopy((object) arAdjust2);
            PX.Objects.AR.ARAdjust arAdjust3 = copy;
            curyAdjdAmt1 = arAdjust3.CuryAdjdAmt;
            Decimal? curyAdjdAmt2 = soAdjust.CuryAdjdAmt;
            Decimal? nullable3 = arAdjust2.CuryDocBal;
            Decimal? nullable4 = curyAdjdAmt2.GetValueOrDefault() > nullable3.GetValueOrDefault() & curyAdjdAmt2.HasValue & nullable3.HasValue ? arAdjust2.CuryDocBal : soAdjust.CuryAdjdAmt;
            Decimal? nullable5;
            if (!(curyAdjdAmt1.HasValue & nullable4.HasValue))
            {
              nullable3 = new Decimal?();
              nullable5 = nullable3;
            }
            else
              nullable5 = new Decimal?(curyAdjdAmt1.GetValueOrDefault() + nullable4.GetValueOrDefault());
            arAdjust3.CuryAdjdAmt = nullable5;
            copy.CuryAdjdOrigAmt = copy.CuryAdjdAmt;
            copy.AdjdOrderType = soAdjust.AdjdOrderType;
            copy.AdjdOrderNbr = soAdjust.AdjdOrderNbr;
            arAdjust1 = (PX.Objects.AR.ARAdjust) pxCache.Update((object) copy);
          }
          if (flag2)
          {
            Decimal? nullable6 = nullable1;
            curyAdjdAmt1 = arAdjust2.CuryAdjdAmt;
            nullable1 = nullable6.HasValue & curyAdjdAmt1.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + curyAdjdAmt1.GetValueOrDefault()) : new Decimal?();
            break;
          }
        }
        Decimal? nullable7 = nullable1;
        Decimal? curyAdjdAmt3 = arAdjust2.CuryAdjdAmt;
        nullable1 = nullable7.HasValue & curyAdjdAmt3.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + curyAdjdAmt3.GetValueOrDefault()) : new Decimal?();
      }
      flag2 = true;
      if (arAdjust1 != null)
      {
        PX.Objects.AR.ARAdjust copy = (PX.Objects.AR.ARAdjust) pxCache.CreateCopy((object) arAdjust1);
        Decimal valueOrDefault = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryDocBal.GetValueOrDefault();
        Decimal num1 = nullable1.GetValueOrDefault() - valueOrDefault;
        Decimal? nullable8 = nullable1;
        Decimal num2 = valueOrDefault;
        if (nullable8.GetValueOrDefault() > num2 & nullable8.HasValue)
        {
          nullable8 = copy.CuryAdjdAmt;
          Decimal num3 = num1;
          if (nullable8.GetValueOrDefault() > num3 & nullable8.HasValue)
          {
            PX.Objects.AR.ARAdjust arAdjust4 = copy;
            nullable8 = arAdjust4.CuryAdjdAmt;
            Decimal num4 = num1;
            arAdjust4.CuryAdjdAmt = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - num4) : new Decimal?();
            nullable1 = new Decimal?(valueOrDefault);
          }
          else
          {
            nullable8 = nullable1;
            Decimal? curyAdjdAmt = copy.CuryAdjdAmt;
            nullable1 = nullable8.HasValue & curyAdjdAmt.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
            copy.CuryAdjdAmt = new Decimal?(0M);
          }
          PX.Objects.AR.ARAdjust arAdjust5 = (PX.Objects.AR.ARAdjust) pxCache.Update((object) copy);
        }
      }
    }
    this.AfterInsertApplication(orderShipment);
  }

  protected virtual void AfterInsertApplication(SOOrderShipment orderShipment)
  {
  }

  protected virtual void DefaultShippingAddress(
    PX.Objects.AR.ARInvoice newdoc,
    SOOrderShipment orderShipment,
    SOShipment soShipment)
  {
    SOAddress soAddress1 = SOAddress.PK.Find((PXGraph) this, orderShipment.ShipAddressID, (PKFindOptions) 1);
    if (!ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this, newdoc.TaxZoneID) || !ExternalTaxBase<PXGraph>.IsEmptyAddress((IAddressLocation) soAddress1))
    {
      SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.shipAddressID>(((PXSelectBase) this.Document).Cache, (object) newdoc, (object) soAddress1, true);
      if (soAddress1 == null || !soAddress1.IsValidated.GetValueOrDefault() || ((PXSelectBase<ARShippingAddress>) this.Shipping_Address).Current == null)
        return;
      ((PXSelectBase<ARShippingAddress>) this.Shipping_Address).Current.IsValidated = new bool?(true);
    }
    else
    {
      if (soShipment != null && soShipment.WillCall.GetValueOrDefault())
      {
        PX.Objects.CR.Address address = PX.Objects.CR.Address.PK.Find((PXGraph) this, PX.Objects.IN.INSite.PK.Find((PXGraph) this, soShipment.SiteID).AddressID);
        if (!ExternalTaxBase<PXGraph>.IsEmptyAddress((IAddressLocation) address))
        {
          AddressAttribute.DefaultAddress<ARShippingAddress, ARShippingAddress.addressID>(((PXSelectBase) this.Document).Cache, "shipAddressID", (object) newdoc, (object) null, (object) new PXResult<PX.Objects.CR.Address, ARShippingAddress>(address, new ARShippingAddress()));
          return;
        }
      }
      SOAddress soAddress2 = SOAddress.PK.Find((PXGraph) this, (int?) KeysRelation<CompositeKey<Field<SOOrderShipment.orderType>.IsRelatedTo<SOOrder.orderType>, Field<SOOrderShipment.orderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOOrderShipment>, SOOrder, SOOrderShipment>.FindParent((PXGraph) this, orderShipment, (PKFindOptions) 0)?.ShipAddressID);
      if (ExternalTaxBase<PXGraph>.IsEmptyAddress((IAddressLocation) soAddress2))
        return;
      SharedRecordAttribute.CopyRecord<PX.Objects.AR.ARInvoice.shipAddressID>(((PXSelectBase) this.Document).Cache, (object) newdoc, (object) soAddress2, true);
      if (soAddress2 == null || !soAddress2.IsValidated.GetValueOrDefault() || ((PXSelectBase<ARShippingAddress>) this.Shipping_Address).Current == null)
        return;
      ((PXSelectBase<ARShippingAddress>) this.Shipping_Address).Current.IsValidated = new bool?(true);
    }
  }

  public virtual bool IsFullOrderInvoicing(
    SOOrder soOrder,
    SOOrderType soOrderType,
    PXSelectBase<SOLine> transactions)
  {
    bool flag = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() && soOrderType.RequireShipping.GetValueOrDefault())
    {
      int? openLineCntr = soOrder.OpenLineCntr;
      int num = 0;
      if (openLineCntr.GetValueOrDefault() == num & openLineCntr.HasValue)
      {
        if (GraphHelper.RowCast<SOLine>((IEnumerable) ((IEnumerable<PXResult<SOLine>>) transactions.Select(new object[2]
        {
          (object) soOrder.OrderType,
          (object) soOrder.OrderNbr
        })).AsEnumerable<PXResult<SOLine>>()).All<SOLine>((Func<SOLine, bool>) (l =>
        {
          Decimal? shippedQty = l.ShippedQty;
          Decimal? orderQty = l.OrderQty;
          return shippedQty.GetValueOrDefault() == orderQty.GetValueOrDefault() & shippedQty.HasValue == orderQty.HasValue || l.LineType == "MI";
        })))
          flag = PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrderShipment.orderNbr, Equal<Required<SOOrder.orderNbr>>, And<Where<SOOrderShipment.invoiceNbr, IsNull, Or<SOOrderShipment.invoiceType, NotEqual<Current<PX.Objects.AR.ARInvoice.docType>>, Or<SOOrderShipment.invoiceNbr, NotEqual<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
          {
            (object) soOrder.OrderType,
            (object) soOrder.OrderNbr
          })) == null;
      }
    }
    return flag;
  }

  /// <summary>
  /// Automatically writes-off the difference between original Amount Paid in Sales Order and Amount Paid in SO Invoice
  /// </summary>
  /// <param name="customer"></param>
  protected virtual void AutoWriteOffBalance(PX.Objects.AR.Customer customer)
  {
    foreach (PXResult<ARAdjust2> pxResult in ((PXSelectBase<ARAdjust2>) this.Adjustments_Inv).Select(Array.Empty<object>()))
    {
      ARAdjust2 arAdjust2 = PXResult<ARAdjust2>.op_Implicit(pxResult);
      Decimal num = arAdjust2.CuryAdjdAmt.GetValueOrDefault() - arAdjust2.CuryAdjdOrigAmt.GetValueOrDefault();
      if (customer != null && customer.SmallBalanceAllow.GetValueOrDefault() && num != 0M && Math.Abs(customer.SmallBalanceLimit.GetValueOrDefault()) >= Math.Abs(num))
      {
        ARAdjust2 copy = PXCache<ARAdjust2>.CreateCopy(arAdjust2);
        copy.CuryAdjdAmt = copy.CuryAdjdOrigAmt;
        copy.CuryAdjdWOAmt = new Decimal?(num);
        ((PXSelectBase<ARAdjust2>) this.Adjustments).Update(copy);
      }
    }
    Decimal? applicationBalance = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryApplicationBalance;
    Decimal num1 = 0M;
    if (applicationBalance.GetValueOrDefault() == num1 & applicationBalance.HasValue)
      return;
    ARAdjust2 arAdjust2_1 = ((PXSelectBase<ARAdjust2>) this.Adjustments_Inv).SelectSingle(Array.Empty<object>());
    if (arAdjust2_1 == null)
      return;
    ARAdjust2 copy1 = PXCache<ARAdjust2>.CreateCopy(arAdjust2_1);
    Decimal valueOrDefault = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryApplicationBalance.GetValueOrDefault();
    if (customer == null || !customer.SmallBalanceAllow.GetValueOrDefault() || !(Math.Abs(customer.SmallBalanceLimit.GetValueOrDefault()) >= Math.Abs(valueOrDefault)))
      return;
    copy1.CuryAdjdWOAmt = new Decimal?(-valueOrDefault);
    ((PXSelectBase<ARAdjust2>) this.Adjustments).Update(copy1);
  }

  protected Sign GetTaxSign(PX.Objects.AR.ARInvoice newInvoice, SOOrderShipment orderShipment)
  {
    SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this, orderShipment.OrderType);
    return newInvoice.DrCr == "C" && soOrderType.DefaultOperation == "R" || newInvoice.DrCr == "D" && soOrderType.DefaultOperation == "I" ? Sign.Minus : Sign.Plus;
  }

  public virtual void AddOrderTaxes(SOOrderShipment orderShipment)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current == null)
      return;
    bool? automaticTaxCalculation;
    if (this.IsExternalTax(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.TaxZoneID))
    {
      automaticTaxCalculation = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DisableAutomaticTaxCalculation;
      if (!automaticTaxCalculation.GetValueOrDefault())
        return;
    }
    automaticTaxCalculation = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DisableAutomaticTaxCalculation;
    if (automaticTaxCalculation.GetValueOrDefault())
    {
      if (PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Current<SOOrderShipment.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrderShipment.orderNbr>>, And<SOOrderShipment.invoiceNbr, IsNotNull, And<SOOrderShipment.orderTaxAllocated, Equal<True>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) orderShipment
      }, Array.Empty<object>())) != null)
        return;
      orderShipment.OrderTaxAllocated = new bool?(true);
      orderShipment = ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(orderShipment);
    }
    ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Current = orderShipment;
    ParameterExpression instance;
    // ISSUE: method reference
    // ISSUE: type reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: type reference
    // ISSUE: method reference
    // ISSUE: type reference
    IQueryable<\u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>> queryable = ((IQueryable<PXResult<SOTaxTran>>) PXSelectBase<SOTaxTran, PXSelectJoin<SOTaxTran, InnerJoin<PX.Objects.TX.Tax, On<SOTaxTran.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<SOTaxTran.orderType, Equal<Required<SOTaxTran.orderType>>, And<SOTaxTran.orderNbr, Equal<Required<SOTaxTran.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) orderShipment.OrderType,
      (object) orderShipment.OrderNbr
    })).Select(Expression.Lambda<Func<PXResult<SOTaxTran>, \u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>>>((Expression) Expression.New((ConstructorInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>.\u002Ector), __typeref (\u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>)), (IEnumerable<Expression>) new Expression[2]
    {
      (Expression) Expression.Call(m, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()),
      (Expression) Expression.Call((Expression) instance, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>())
    }, (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>.get_TaxTran), __typeref (\u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>)), (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>.get_Tax), __typeref (\u003C\u003Ef__AnonymousType86<SOTaxTran, PX.Objects.TX.Tax>))), instance));
    foreach (var data in queryable)
    {
      ARTaxTran arTaxTran1 = new ARTaxTran();
      arTaxTran1.Module = "AR";
      ARTaxTran newtax = arTaxTran1;
      ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<TaxTran.origTranType>((object) newtax);
      ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<TaxTran.origRefNbr>((object) newtax);
      ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<TaxTran.lineRefNbr>((object) newtax);
      newtax.TranType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType;
      newtax.RefNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.RefNbr;
      newtax.TaxID = data.TaxTran.TaxID;
      newtax.TaxRate = new Decimal?(0M);
      newtax.CuryID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryID;
      if (data.Tax?.TaxType == "Q")
        newtax.TaxableQty = data.TaxTran.TaxableQty;
      PX.Objects.AR.ARInvoice current1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        automaticTaxCalculation = current1.DisableAutomaticTaxCalculation;
        num1 = automaticTaxCalculation.GetValueOrDefault() ? 1 : 0;
      }
      Decimal? nullable1;
      if (num1 != 0)
      {
        Sign taxSign = this.GetTaxSign(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current, orderShipment);
        ARTaxTran arTaxTran2 = newtax;
        nullable1 = data.TaxTran.CuryTaxableAmt;
        Sign sign1 = taxSign;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?(Sign.op_Multiply(nullable1.GetValueOrDefault(), sign1)) : new Decimal?();
        arTaxTran2.CuryTaxableAmt = nullable2;
        ARTaxTran arTaxTran3 = newtax;
        nullable1 = data.TaxTran.CuryTaxAmt;
        Sign sign2 = taxSign;
        Decimal? nullable3 = nullable1.HasValue ? new Decimal?(Sign.op_Multiply(nullable1.GetValueOrDefault(), sign2)) : new Decimal?();
        arTaxTran3.CuryTaxAmt = nullable3;
        newtax.TaxRate = data.TaxTran.TaxRate;
        newtax.JurisName = data.TaxTran.JurisName;
        newtax.JurisType = data.TaxTran.JurisType;
        newtax.TaxBucketID = new int?(0);
      }
      EnumerableExtensions.Consume<PXResult<ARTaxTran>>((IEnumerable<PXResult<ARTaxTran>>) ((PXSelectBase<ARTaxTran>) this.Taxes).Select(Array.Empty<object>()));
      bool flag = true;
      PX.Objects.AR.ARInvoice current2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
      int num2;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        automaticTaxCalculation = current2.DisableAutomaticTaxCalculation;
        num2 = automaticTaxCalculation.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 != 0)
      {
        using (IEnumerator<ARTaxTran> enumerator = GraphHelper.RowCast<ARTaxTran>(((PXSelectBase) this.Taxes).Cache.Cached).Where<ARTaxTran>((Func<ARTaxTran, bool>) (a => !EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Taxes).Cache.GetStatus((object) a), (PXEntryStatus) 3, (PXEntryStatus) 4) && ((PXSelectBase) this.Taxes).Cache.ObjectsEqual<ARTaxTran.module, ARTaxTran.refNbr, ARTaxTran.tranType, ARTaxTran.taxID, TaxTran.jurisType, TaxTran.jurisName>((object) newtax, (object) a))).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            ARTaxTran copy = (ARTaxTran) ((PXSelectBase) this.Taxes).Cache.CreateCopy((object) enumerator.Current);
            ARTaxTran arTaxTran4 = copy;
            nullable1 = arTaxTran4.CuryTaxableAmt;
            Decimal? nullable4 = newtax.CuryTaxableAmt;
            arTaxTran4.CuryTaxableAmt = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            ARTaxTran arTaxTran5 = copy;
            nullable4 = arTaxTran5.CuryTaxAmt;
            nullable1 = newtax.CuryTaxAmt;
            arTaxTran5.CuryTaxAmt = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
            if (data.Tax?.TaxType == "Q")
            {
              ARTaxTran arTaxTran6 = copy;
              nullable1 = arTaxTran6.TaxableQty;
              nullable4 = newtax.TaxableQty;
              arTaxTran6.TaxableQty = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            }
            ((PXSelectBase<ARTaxTran>) this.Taxes).Update(copy);
            flag = false;
          }
        }
      }
      else
      {
        foreach (ARTaxTran arTaxTran7 in GraphHelper.RowCast<ARTaxTran>(((PXSelectBase) this.Taxes).Cache.Cached).Where<ARTaxTran>((Func<ARTaxTran, bool>) (a => !EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Taxes).Cache.GetStatus((object) a), (PXEntryStatus) 3, (PXEntryStatus) 4) && ((PXSelectBase) this.Taxes).Cache.ObjectsEqual<ARTaxTran.module, ARTaxTran.refNbr, ARTaxTran.tranType, ARTaxTran.taxID>((object) newtax, (object) a))))
        {
          if (data.Tax?.TaxType == "Q")
          {
            ARTaxTran arTaxTran8 = newtax;
            Decimal? taxableQty = arTaxTran8.TaxableQty;
            nullable1 = arTaxTran7.TaxableQty;
            arTaxTran8.TaxableQty = taxableQty.HasValue & nullable1.HasValue ? new Decimal?(taxableQty.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          }
          ((PXSelectBase<ARTaxTran>) this.Taxes).Delete(arTaxTran7);
        }
      }
      if (flag)
        newtax = ((PXSelectBase<ARTaxTran>) this.Taxes).Insert(newtax);
    }
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current;
    if ((current != null ? (current.DisableAutomaticTaxCalculation.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    SOOrder soOrder1 = GraphHelper.Caches<SOOrder>((PXGraph) this).Locate(new SOOrder()
    {
      OrderType = orderShipment.OrderType,
      OrderNbr = orderShipment.OrderNbr
    });
    SOOrder copy1 = PXCache<SOOrder>.CreateCopy(soOrder1);
    foreach (var data in queryable)
    {
      data.TaxTran.CuryUnbilledTaxableAmt = new Decimal?(0M);
      data.TaxTran.CuryUnbilledTaxAmt = new Decimal?(0M);
      data.TaxTran.CuryUnshippedTaxableAmt = new Decimal?(0M);
      data.TaxTran.CuryUnshippedTaxAmt = new Decimal?(0M);
      GraphHelper.Caches<SOTaxTran>((PXGraph) this).Update(data.TaxTran);
    }
    soOrder1.CuryOpenTaxTotal = new Decimal?(0M);
    soOrder1.CuryUnbilledTaxTotal = new Decimal?(0M);
    SOOrder soOrder2 = soOrder1;
    Decimal? nullable = soOrder2.CuryOpenOrderTotal;
    Decimal? curyOpenTaxTotal = copy1.CuryOpenTaxTotal;
    soOrder2.CuryOpenOrderTotal = nullable.HasValue & curyOpenTaxTotal.HasValue ? new Decimal?(nullable.GetValueOrDefault() - curyOpenTaxTotal.GetValueOrDefault()) : new Decimal?();
    SOOrder soOrder3 = soOrder1;
    Decimal? unbilledOrderTotal = soOrder3.CuryUnbilledOrderTotal;
    nullable = copy1.CuryUnbilledTaxTotal;
    soOrder3.CuryUnbilledOrderTotal = unbilledOrderTotal.HasValue & nullable.HasValue ? new Decimal?(unbilledOrderTotal.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
    GraphHelper.Caches<SOOrder>((PXGraph) this).Update(soOrder1);
  }

  public virtual DateTime GetOrderInvoiceDate(
    DateTime invoiceDate,
    SOOrder soOrder,
    SOOrderShipment orderShipment)
  {
    return (!((PXSelectBase<SOSetup>) this.sosetup).Current.UseShipDateForInvoiceDate.GetValueOrDefault() || soOrder.InvoiceDate.HasValue ? soOrder.InvoiceDate : orderShipment.ShipDate) ?? invoiceDate;
  }

  public virtual bool IsCreditCardProcessing(SOOrder soOrder)
  {
    return PXSelectBase<PX.Objects.AR.ExternalTransaction, PXSelectReadonly<PX.Objects.AR.ExternalTransaction, Where<PX.Objects.AR.ExternalTransaction.origDocType, Equal<Required<PX.Objects.AR.ExternalTransaction.origDocType>>, And<PX.Objects.AR.ExternalTransaction.origRefNbr, Equal<Required<PX.Objects.AR.ExternalTransaction.origRefNbr>>, And<PX.Objects.AR.ExternalTransaction.refNbr, IsNull>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) soOrder.OrderType,
      (object) soOrder.OrderNbr
    }).Count > 0;
  }

  public virtual PX.Objects.AR.ARInvoice FindOrCreateInvoice(
    DateTime orderInvoiceDate,
    InvoiceOrderArgs args)
  {
    SOOrderShipment orderShipment = args.OrderShipment;
    SOOrder soOrder = args.SOOrder;
    SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) this, soOrder.OrderType);
    string invoiceDocType = this.GetInvoiceDocType(soOrderType, soOrder, args.GroupByDefaultOperation ? soOrderType.DefaultOperation : orderShipment.Operation);
    string termsId = invoiceDocType == "CRM" ? (string) null : soOrder.TermsID;
    if (orderShipment.BillShipmentSeparately.GetValueOrDefault())
    {
      PX.Objects.AR.ARInvoice orCreateInvoice = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(args.List.Find((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hidden>((object) false), (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hiddenByShipment>((object) true), (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hiddenShipmentType>((object) orderShipment.ShipmentType), (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hiddenShipmentNbr>((object) orderShipment.ShipmentNbr), (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.taxCalcMode>((object) soOrder.TaxCalcMode)));
      if (orCreateInvoice != null)
        return orCreateInvoice;
      return new PX.Objects.AR.ARInvoice()
      {
        HiddenShipmentType = orderShipment.ShipmentType,
        HiddenShipmentNbr = orderShipment.ShipmentNbr,
        HiddenByShipment = new bool?(true)
      };
    }
    int? nullable = soOrder.PaymentCntr;
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue) || soOrder.BillSeparately.GetValueOrDefault() || this.IsCreditCardProcessing(soOrder))
    {
      PX.Objects.AR.ARInvoice orCreateInvoice = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(args.List.Find((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hidden>((object) true), (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hiddenByShipment>((object) false), (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hiddenOrderType>((object) soOrder.OrderType), (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hiddenOrderNbr>((object) soOrder.OrderNbr)));
      if (orCreateInvoice != null)
        return orCreateInvoice;
      return new PX.Objects.AR.ARInvoice()
      {
        HiddenOrderType = soOrder.OrderType,
        HiddenOrderNbr = soOrder.OrderNbr,
        Hidden = new bool?(true)
      };
    }
    List<FieldLookup> fieldLookupList = new List<FieldLookup>()
    {
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hidden>((object) false),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.hiddenByShipment>((object) false),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.docType>((object) invoiceDocType),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.docDate>((object) orderInvoiceDate),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.branchID>((object) soOrder.BranchID),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.customerID>((object) soOrder.CustomerID),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.customerLocationID>((object) soOrder.CustomerLocationID),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.taxZoneID>((object) soOrder.TaxZoneID),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.taxCalcMode>((object) soOrder.TaxCalcMode),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.curyID>((object) soOrder.CuryID),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.termsID>((object) termsId),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOInvoice.billAddressID>((object) soOrder.BillAddressID),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOInvoice.billContactID>((object) soOrder.BillContactID),
      (FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.disableAutomaticTaxCalculation>((object) soOrder.DisableAutomaticTaxCalculation)
    };
    if (args.GroupByCustomerOrderNumber)
      fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.AR.ARInvoice.invoiceNbr>((object) soOrder.CustomerOrderNbr));
    fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.SO.SOInvoice.extRefNbr>((object) soOrder.ExtRefNbr));
    fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.SO.SOInvoice.pMInstanceID>((object) soOrder.PMInstanceID));
    nullable = soOrder.CashAccountID;
    if (nullable.HasValue)
      fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.SO.SOInvoice.cashAccountID>((object) soOrder.CashAccountID));
    PX.Objects.CM.Extensions.CurrencyInfo soCuryInfo = args.SoCuryInfo;
    fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>((object) soCuryInfo.CuryRateTypeID));
    if (soOrderType.UseCuryRateFromSO.GetValueOrDefault())
    {
      fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv>((object) soCuryInfo.CuryMultDiv));
      fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.CM.Extensions.CurrencyInfo.curyRate>((object) soCuryInfo.CuryRate));
    }
    PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice> pxResult = args.List.Find(fieldLookupList.ToArray());
    return pxResult == null ? new PX.Objects.AR.ARInvoice() : PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(pxResult);
  }

  public virtual PX.Objects.AR.ARTran CreateTranFromMiscLine(
    SOOrderShipment orderShipment,
    SOMiscLine2 orderline)
  {
    return new PX.Objects.AR.ARTran()
    {
      BranchID = orderline.BranchID,
      AccountID = orderline.SalesAcctID,
      SubID = orderline.SalesSubID,
      SOOrderType = orderline.OrderType,
      SOOrderNbr = orderline.OrderNbr,
      SOOrderLineNbr = orderline.LineNbr,
      SOOrderLineOperation = orderline.Operation,
      SOOrderSortOrder = orderline.SortOrder,
      SOOrderLineSign = orderline.LineSign,
      SOShipmentNbr = orderShipment.ShipmentNbr,
      SOShipmentLineGroupNbr = new int?(),
      SOShipmentLineNbr = new int?(),
      SOShipmentType = orderShipment.ShipmentType,
      LineType = "MI",
      InventoryID = orderline.InventoryID,
      SiteID = orderline.SiteID,
      ProjectID = orderline.ProjectID,
      TaskID = orderline.TaskID,
      SalesPersonID = orderline.SalesPersonID,
      Commissionable = orderline.Commissionable,
      UOM = orderline.UOM,
      Qty = orderline.UnbilledQty,
      BaseQty = orderline.BaseUnbilledQty,
      CuryUnitPrice = orderline.CuryUnitPrice,
      CuryExtPrice = orderline.CuryExtPrice,
      CuryDiscAmt = orderline.CuryDiscAmt,
      CuryTranAmt = orderline.CuryUnbilledAmt,
      TranDesc = orderline.TranDesc,
      TaxCategoryID = orderline.TaxCategoryID,
      DiscPct = orderline.DiscPct,
      IsFree = orderline.IsFree,
      ManualPrice = new bool?(true),
      ManualDisc = new bool?(orderline.ManualDisc.GetValueOrDefault() || orderline.IsFree.GetValueOrDefault()),
      FreezeManualDisc = new bool?(true),
      DiscountID = orderline.DiscountID,
      DiscountSequenceID = orderline.DiscountSequenceID,
      DRTermStartDate = orderline.DRTermStartDate,
      DRTermEndDate = orderline.DRTermEndDate,
      CuryUnitPriceDR = orderline.CuryUnitPriceDR,
      DiscPctDR = orderline.DiscPctDR,
      DefScheduleID = orderline.DefScheduleID,
      SortOrder = orderline.SortOrder,
      OrigInvoiceType = orderline.InvoiceType,
      OrigInvoiceNbr = orderline.InvoiceNbr,
      OrigInvoiceLineNbr = orderline.InvoiceLineNbr,
      OrigInvoiceDate = orderline.InvoiceDate,
      CostCodeID = orderline.CostCodeID,
      BlanketType = orderline.BlanketType,
      BlanketNbr = orderline.BlanketNbr,
      BlanketLineNbr = orderline.BlanketLineNbr,
      BlanketSplitLineNbr = orderline.BlanketSplitLineNbr
    };
  }

  protected virtual SOShipLine CombineShipLines(SOLine orderline, SOShipLine[] shipLines)
  {
    if (shipLines == null || shipLines.Length == 0)
      throw new PXArgumentException(nameof (shipLines));
    if (shipLines.Length == 1 && string.Equals(orderline.UOM, shipLines[0].UOM, StringComparison.OrdinalIgnoreCase))
      return shipLines[0];
    SOShipLine data = (SOShipLine) null;
    bool flag1 = false;
    bool flag2 = false;
    foreach (SOShipLine shipLine in shipLines)
    {
      flag2 |= !string.Equals(orderline.UOM, shipLine.UOM, StringComparison.OrdinalIgnoreCase);
      if (data == null)
      {
        data = PXCache<SOShipLine>.CreateCopy(shipLine);
      }
      else
      {
        flag1 |= !string.Equals(data.UOM, shipLine.UOM, StringComparison.OrdinalIgnoreCase);
        SOShipLine soShipLine1 = data;
        Decimal? shippedQty1 = soShipLine1.ShippedQty;
        Decimal? shippedQty2 = shipLine.ShippedQty;
        soShipLine1.ShippedQty = shippedQty1.HasValue & shippedQty2.HasValue ? new Decimal?(shippedQty1.GetValueOrDefault() + shippedQty2.GetValueOrDefault()) : new Decimal?();
        SOShipLine soShipLine2 = data;
        Decimal? baseShippedQty1 = soShipLine2.BaseShippedQty;
        Decimal? baseShippedQty2 = shipLine.BaseShippedQty;
        soShipLine2.BaseShippedQty = baseShippedQty1.HasValue & baseShippedQty2.HasValue ? new Decimal?(baseShippedQty1.GetValueOrDefault() + baseShippedQty2.GetValueOrDefault()) : new Decimal?();
        data.LineNbr = new int?();
        if (!string.Equals(data.LotSerialNbr, shipLine.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        {
          data.LotSerialNbr = (string) null;
          data.ExpireDate = new DateTime?();
        }
      }
    }
    if (flag2)
    {
      Decimal? baseShippedQty = data.BaseShippedQty;
      Decimal? nullable = orderline.BaseOrderQty;
      if (baseShippedQty.GetValueOrDefault() == nullable.GetValueOrDefault() & baseShippedQty.HasValue == nullable.HasValue)
      {
        data.UOM = orderline.UOM;
        data.ShippedQty = orderline.OrderQty;
      }
      else
      {
        PXCache cach = ((PXGraph) this).Caches[typeof (SOShipLine)];
        int? inventoryId = data.InventoryID;
        string uom = orderline.UOM;
        nullable = data.BaseShippedQty;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        Decimal num = INUnitAttribute.ConvertFromBase(cach, inventoryId, uom, valueOrDefault, INPrecision.NOROUND);
        if (num % 1M == 0M)
        {
          data.UOM = orderline.UOM;
          data.ShippedQty = new Decimal?(num);
        }
      }
    }
    if (flag1)
    {
      data.UOM = orderline.UOM;
      PXDBQuantityAttribute.CalcTranQty<SOShipLine.shippedQty>(((PXGraph) this).Caches[typeof (SOShipLine)], (object) data);
    }
    return data;
  }

  public virtual PX.Objects.AR.ARTran CreateTranFromShipLine(
    PX.Objects.AR.ARInvoice newdoc,
    SOOrderType ordertype,
    string operation,
    SOLine orderline,
    ref SOShipLine shipline)
  {
    PX.Objects.AR.ARTran tranFromShipLine = new PX.Objects.AR.ARTran();
    tranFromShipLine.SOOrderType = shipline.OrigOrderType;
    tranFromShipLine.SOOrderNbr = shipline.OrigOrderNbr;
    tranFromShipLine.SOOrderLineNbr = shipline.OrigLineNbr;
    tranFromShipLine.SOShipmentNbr = shipline.ShipmentNbr;
    tranFromShipLine.SOShipmentType = shipline.ShipmentType;
    tranFromShipLine.SOShipmentLineGroupNbr = shipline.InvoiceGroupNbr;
    tranFromShipLine.SOShipmentLineNbr = shipline.LineNbr;
    tranFromShipLine.RequireINUpdate = shipline.RequireINUpdate;
    tranFromShipLine.LineType = orderline.LineType;
    tranFromShipLine.InventoryID = shipline.InventoryID;
    tranFromShipLine.SiteID = orderline.SiteID;
    tranFromShipLine.SubItemID = shipline.SubItemID;
    tranFromShipLine.LocationID = shipline.ShipmentType == "H" || shipline.ShipmentNbr == "<NEW>" ? orderline.LocationID : shipline.LocationID;
    tranFromShipLine.LotSerialNbr = shipline.LotSerialNbr;
    tranFromShipLine.ExpireDate = shipline.ExpireDate;
    tranFromShipLine.CostCodeID = shipline.CostCodeID;
    bool useLineDiscPct;
    this.CopyTranFieldsFromSOLine(tranFromShipLine, ordertype, orderline, out useLineDiscPct);
    tranFromShipLine.UOM = shipline.UOM;
    tranFromShipLine.Qty = shipline.ShippedQty;
    tranFromShipLine.BaseQty = shipline.BaseShippedQty;
    bool flag1 = !string.Equals(shipline.UOM, orderline.UOM, StringComparison.OrdinalIgnoreCase);
    Decimal num1;
    Decimal num2;
    if (flag1)
    {
      try
      {
        Decimal num3 = INUnitAttribute.ConvertToBase(((PXSelectBase) this.Transactions).Cache, tranFromShipLine.InventoryID, shipline.UOM, shipline.ShippedQty.Value, INPrecision.NOROUND);
        num1 = INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Transactions).Cache, tranFromShipLine.InventoryID, orderline.UOM, num3, INPrecision.NOROUND);
        num2 = PXDBQuantityAttribute.Round(new Decimal?(num1));
      }
      catch (PXSetPropertyException ex)
      {
        throw new ErrorProcessingEntityException(((PXGraph) this).Caches[((object) orderline).GetType()], (IBqlTable) orderline, (PXException) ex);
      }
    }
    else
    {
      Decimal? shippedQty = shipline.ShippedQty;
      num2 = num1 = shippedQty.Value;
    }
    ARInvoiceEntry.MultiCurrency extension = ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>();
    Decimal num4 = num2;
    Decimal? orderQty1 = orderline.OrderQty;
    Decimal valueOrDefault1 = orderQty1.GetValueOrDefault();
    if (!(num4 == valueOrDefault1 & orderQty1.HasValue) | flag1)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = extension.GetCurrencyInfo(orderline.CuryInfoID);
      Decimal? nullable1 = orderline.OrderQty;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      nullable1 = orderline.CuryUnitPrice;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal val = valueOrDefault2 * valueOrDefault3;
      bool flag2 = currencyInfo.RoundCury(val) - orderline.CuryExtPrice.GetValueOrDefault() != 0M;
      Decimal num5 = 0M;
      Decimal? nullable2;
      if (flag2)
      {
        Decimal? curyExtPrice = orderline.CuryExtPrice;
        Decimal num6 = 0M;
        if (!(curyExtPrice.GetValueOrDefault() == num6 & curyExtPrice.HasValue))
        {
          Decimal? orderQty2 = orderline.OrderQty;
          Decimal num7 = 0M;
          if (!(orderQty2.GetValueOrDefault() == num7 & orderQty2.HasValue))
            num5 = PXPriceCostAttribute.Round(orderline.CuryExtPrice.Value / orderline.OrderQty.Value);
        }
      }
      else
      {
        nullable2 = orderline.CuryUnitPrice;
        num5 = nullable2.Value;
      }
      Decimal num8;
      if (flag1)
      {
        Decimal num9 = INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Transactions).Cache, tranFromShipLine.InventoryID, orderline.UOM, num5, INPrecision.UNITCOST);
        num8 = INUnitAttribute.ConvertToBase(((PXSelectBase) this.Transactions).Cache, tranFromShipLine.InventoryID, shipline.UOM, num9, INPrecision.UNITCOST);
      }
      else
        num8 = num5;
      nullable2 = orderline.CuryUnitPrice;
      Decimal num10 = 0M;
      if (!(nullable2.GetValueOrDefault() == num10 & nullable2.HasValue))
        tranFromShipLine.CuryUnitPrice = new Decimal?(num8);
      Decimal num11 = num2;
      nullable2 = orderline.OrderQty;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      Decimal? nullable3;
      if (!(num11 == valueOrDefault4 & nullable2.HasValue))
      {
        nullable2 = orderline.OrderQty;
        Decimal num12 = 0M;
        if (!(nullable2.GetValueOrDefault() == num12 & nullable2.HasValue))
        {
          Decimal? nullable4;
          if (!flag2)
          {
            Decimal num13 = num1;
            nullable2 = orderline.CuryUnitPrice;
            nullable4 = nullable2.HasValue ? new Decimal?(num13 * nullable2.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            Decimal? curyExtPrice = orderline.CuryExtPrice;
            Decimal num14 = num1;
            nullable2 = curyExtPrice.HasValue ? new Decimal?(curyExtPrice.GetValueOrDefault() * num14) : new Decimal?();
            nullable3 = orderline.OrderQty;
            nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable3.GetValueOrDefault()) : new Decimal?();
          }
          Decimal? nullable5 = nullable4;
          tranFromShipLine.CuryExtPrice = new Decimal?(extension.GetDefaultCurrencyInfo().RoundCury(nullable5.GetValueOrDefault()));
          goto label_22;
        }
      }
      tranFromShipLine.CuryExtPrice = orderline.CuryExtPrice;
label_22:
      nullable3 = orderline.DiscPct;
      Decimal num15 = 0M;
      if (nullable3.GetValueOrDefault() == num15 & nullable3.HasValue)
      {
        nullable3 = orderline.CuryDiscAmt;
        Decimal num16 = 0M;
        if (nullable3.GetValueOrDefault() == num16 & nullable3.HasValue)
        {
          tranFromShipLine.CuryTranAmt = tranFromShipLine.CuryExtPrice;
          tranFromShipLine.CuryDiscAmt = new Decimal?(0M);
          goto label_43;
        }
      }
      Decimal num17 = num8;
      Decimal? nullable6;
      Decimal? nullable7;
      if (!useLineDiscPct)
      {
        nullable7 = new Decimal?(1M);
      }
      else
      {
        Decimal num18 = 1M;
        nullable6 = orderline.DiscPct;
        Decimal num19 = 100M;
        nullable2 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / num19) : new Decimal?();
        if (!nullable2.HasValue)
        {
          nullable6 = new Decimal?();
          nullable7 = nullable6;
        }
        else
          nullable7 = new Decimal?(num18 - nullable2.GetValueOrDefault());
      }
      nullable3 = nullable7;
      Decimal? nullable8;
      if (!nullable3.HasValue)
      {
        nullable2 = new Decimal?();
        nullable8 = nullable2;
      }
      else
        nullable8 = new Decimal?(num17 * nullable3.GetValueOrDefault());
      Decimal? nullable9 = nullable8;
      if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.arsetup).Current.LineDiscountTarget == "S")
        nullable9 = new Decimal?(PXPriceCostAttribute.Round(nullable9.GetValueOrDefault()));
      nullable3 = shipline.ShippedQty;
      nullable2 = nullable9;
      Decimal? nullable10;
      if (!(nullable3.HasValue & nullable2.HasValue))
      {
        nullable6 = new Decimal?();
        nullable10 = nullable6;
      }
      else
        nullable10 = new Decimal?(nullable3.GetValueOrDefault() * nullable2.GetValueOrDefault());
      Decimal? nullable11 = nullable10;
      tranFromShipLine.CuryTranAmt = new Decimal?(extension.GetDefaultCurrencyInfo().RoundCury(nullable11.GetValueOrDefault()));
      PX.Objects.AR.ARTran arTran = tranFromShipLine;
      nullable2 = tranFromShipLine.CuryExtPrice;
      nullable3 = tranFromShipLine.CuryTranAmt;
      Decimal? nullable12;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable6 = new Decimal?();
        nullable12 = nullable6;
      }
      else
        nullable12 = new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
      arTran.CuryDiscAmt = nullable12;
    }
    else
    {
      tranFromShipLine.CuryUnitPrice = orderline.CuryUnitPrice;
      tranFromShipLine.CuryExtPrice = orderline.CuryExtPrice;
      tranFromShipLine.CuryTranAmt = orderline.CuryLineAmt;
      tranFromShipLine.CuryDiscAmt = orderline.CuryDiscAmt;
    }
label_43:
    this.ChangeBalanceSign(tranFromShipLine, newdoc, orderline.Operation);
    return tranFromShipLine;
  }

  protected virtual void ChangeBalanceSign(PX.Objects.AR.ARTran tran, PX.Objects.AR.ARInvoice newdoc, string soLineOperation)
  {
    if ((!(newdoc.DrCr == "C") || !(soLineOperation == "R")) && (!(newdoc.DrCr == "D") || !(soLineOperation == "I")))
      return;
    PX.Objects.AR.ARTran arTran1 = tran;
    Decimal? nullable1 = tran.Qty;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    arTran1.Qty = nullable2;
    PX.Objects.AR.ARTran arTran2 = tran;
    nullable1 = tran.CuryDiscAmt;
    Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    arTran2.CuryDiscAmt = nullable3;
    PX.Objects.AR.ARTran arTran3 = tran;
    nullable1 = tran.CuryTranAmt;
    Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    arTran3.CuryTranAmt = nullable4;
    PX.Objects.AR.ARTran arTran4 = tran;
    nullable1 = tran.CuryExtPrice;
    Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    arTran4.CuryExtPrice = nullable5;
  }

  protected virtual void CopyTranFieldsFromOrigTran(PX.Objects.AR.ARTran newtran, PX.Objects.AR.ARTran origTran)
  {
    newtran.IsFree = origTran.IsFree;
    newtran.ManualPrice = new bool?(true);
    PX.Objects.AR.ARTran arTran = newtran;
    bool? nullable1;
    int num;
    if (!origTran.ManualDisc.GetValueOrDefault())
    {
      nullable1 = origTran.IsFree;
      num = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 1;
    bool? nullable2 = new bool?(num != 0);
    arTran.ManualDisc = nullable2;
    nullable1 = origTran.ManualDisc;
    if (nullable1.GetValueOrDefault())
      newtran.DiscPct = origTran.DiscPct;
    newtran.AvalaraCustomerUsageType = origTran.AvalaraCustomerUsageType;
    newtran.TaxCategoryID = origTran.TaxCategoryID;
  }

  protected virtual void CopyTranFieldsFromSOLine(
    PX.Objects.AR.ARTran newtran,
    SOOrderType ordertype,
    SOLine orderline,
    out bool useLineDiscPct)
  {
    ref bool local = ref useLineDiscPct;
    bool? nullable1;
    int num1;
    if (ordertype == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = ordertype.RecalculateDiscOnPartialShipment;
      num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num2;
    if (num1 == 0)
    {
      nullable1 = orderline.ManualDisc;
      if (!nullable1.GetValueOrDefault())
      {
        num2 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.arsetup).Current.LineDiscountTarget == "S" ? 1 : 0;
        goto label_7;
      }
    }
    num2 = 1;
label_7:
    local = num2 != 0;
    newtran.BranchID = orderline.BranchID;
    newtran.AccountID = orderline.SalesAcctID;
    newtran.SubID = orderline.SalesSubID;
    newtran.ReasonCode = orderline.ReasonCode;
    newtran.DRTermStartDate = orderline.DRTermStartDate;
    newtran.DRTermEndDate = orderline.DRTermEndDate;
    newtran.CuryUnitPriceDR = orderline.CuryUnitPriceDR;
    newtran.DiscPctDR = orderline.DiscPctDR;
    newtran.DefScheduleID = orderline.DefScheduleID;
    newtran.Commissionable = orderline.Commissionable;
    newtran.ProjectID = orderline.ProjectID;
    newtran.TaskID = orderline.TaskID;
    newtran.CostCodeID = orderline.CostCodeID;
    newtran.TranDesc = orderline.TranDesc;
    newtran.SalesPersonID = orderline.SalesPersonID;
    newtran.TaxCategoryID = orderline.TaxCategoryID;
    newtran.DiscPct = useLineDiscPct ? orderline.DiscPct : new Decimal?(0M);
    newtran.IsFree = orderline.IsFree;
    newtran.ManualPrice = new bool?(true);
    PX.Objects.AR.ARTran arTran = newtran;
    nullable1 = orderline.ManualDisc;
    int num3;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = orderline.IsFree;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 1;
    bool? nullable2 = new bool?(num3 != 0);
    arTran.ManualDisc = nullable2;
    newtran.FreezeManualDisc = new bool?(true);
    newtran.SkipLineDiscounts = orderline.SkipLineDiscounts;
    newtran.DiscountID = orderline.DiscountID;
    newtran.DiscountSequenceID = orderline.DiscountSequenceID;
    newtran.DisableAutomaticTaxCalculation = orderline.DisableAutomaticTaxCalculation;
    newtran.SortOrder = orderline.SortOrder;
    newtran.OrigInvoiceType = orderline.InvoiceType;
    newtran.OrigInvoiceNbr = orderline.InvoiceNbr;
    newtran.OrigInvoiceLineNbr = orderline.InvoiceLineNbr;
    newtran.OrigInvoiceDate = orderline.InvoiceDate;
    newtran.SOOrderLineOperation = orderline.Operation;
    newtran.SOOrderSortOrder = orderline.SortOrder;
    newtran.SOOrderLineSign = orderline.LineSign;
    newtran.AvalaraCustomerUsageType = orderline.AvalaraCustomerUsageType;
    newtran.BlanketType = orderline.BlanketType;
    newtran.BlanketNbr = orderline.BlanketNbr;
    newtran.BlanketLineNbr = orderline.BlanketLineNbr;
    newtran.BlanketSplitLineNbr = orderline.BlanketSplitLineNbr;
  }

  public virtual void PostInvoice(
    InvoicePostingContext context,
    PX.Objects.AR.ARInvoice invoice,
    DocumentList<PX.Objects.IN.INRegister> list,
    List<SOOrderShipment> directShipmentsToCreate)
  {
    PX.Objects.SO.SOInvoice soInvoice = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(new object[2]
    {
      (object) invoice.DocType,
      (object) invoice.RefNbr
    }));
    if ((soInvoice != null ? (soInvoice.CreateINDoc.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      foreach (PXResult<SOOrderShipment, SOOrder> pxResult in PXSelectBase<SOOrderShipment, PXSelectJoin<SOOrderShipment, InnerJoin<SOOrder, On<SOOrder.orderType, Equal<SOOrderShipment.orderType>, And<SOOrder.orderNbr, Equal<SOOrderShipment.orderNbr>>>>, Where<SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<SOOrderShipment.createINDoc, Equal<True>, And<SOOrderShipment.invtRefNbr, IsNull>>>>, OrderBy<Asc<SOOrderShipment.shipmentNbr>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) invoice
      }, Array.Empty<object>()))
      {
        if (PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult).ShipmentType == "H")
          context.GetClearPOReceiptEntry().PostReceipt(new PostReceiptArgs(context.IssueEntry, PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult), PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult), invoice, list));
        else if (string.Equals(PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult).ShipmentNbr, "<NEW>"))
          context.GetClearOrderEntry().PostOrder(context.IssueEntry, PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult), list, PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult));
        else
          ((PXGraph) context.GetClearShipmentEntry()).GetExtension<UpdateInventoryExtension>().PostShipment(new PostShipmentArgs((INRegisterEntryBase) context.IssueEntry, PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult), PXResult<SOOrderShipment, SOOrder>.op_Implicit(pxResult), list, invoice));
      }
    }
    this.PostInvoiceDirectLines(context.IssueEntry, invoice, list, directShipmentsToCreate);
  }

  public virtual PX.Objects.AR.ARTran InsertInvoiceDirectLine(PX.Objects.AR.ARTran tran)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current == null)
      return (PX.Objects.AR.ARTran) null;
    int? nullable1 = tran.SOOrderLineNbr;
    Decimal? nullable2;
    if (nullable1.HasValue)
    {
      SOLine orderline = SOLine.PK.Find((PXGraph) this, tran.SOOrderType, tran.SOOrderNbr, tran.SOOrderLineNbr);
      if (orderline != null)
      {
        PX.Objects.AR.ARTran arTran1 = tran;
        nullable1 = tran.InventoryID;
        bool? nullable3 = nullable1.HasValue ? tran.IsStockItem : orderline.IsStockItem;
        arTran1.IsStockItem = nullable3;
        tran.LineType = orderline.LineType;
        PX.Objects.AR.ARTran arTran2 = tran;
        nullable1 = tran.InventoryID;
        int? nullable4 = nullable1 ?? orderline.InventoryID;
        arTran2.InventoryID = nullable4;
        PX.Objects.AR.ARTran arTran3 = tran;
        nullable1 = tran.SubItemID;
        int? nullable5 = nullable1 ?? orderline.SubItemID;
        arTran3.SubItemID = nullable5;
        PX.Objects.AR.ARTran arTran4 = tran;
        nullable1 = tran.SiteID;
        int? nullable6 = nullable1 ?? orderline.SiteID;
        arTran4.SiteID = nullable6;
        PX.Objects.AR.ARTran arTran5 = tran;
        nullable1 = tran.LocationID;
        int? nullable7 = nullable1 ?? orderline.LocationID;
        arTran5.LocationID = nullable7;
        tran.UOM = tran.UOM ?? orderline.UOM;
        tran.LotSerialNbr = tran.LotSerialNbr ?? orderline.LotSerialNbr;
        tran.ExpireDate = tran.ExpireDate ?? orderline.ExpireDate;
        PX.Objects.AR.ARTran arTran6 = tran;
        Decimal? nullable8 = tran.CuryUnitPrice;
        Decimal? nullable9 = nullable8 ?? orderline.CuryUnitPrice;
        arTran6.CuryUnitPrice = nullable9;
        nullable8 = tran.Qty;
        if (!nullable8.HasValue)
        {
          short num1 = orderline.DefaultOperation == "R" ? (short) 1 : (short) -1;
          short? nullable10 = INTranType.InvtMultFromInvoiceType(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType);
          PX.Objects.AR.ARTran arTran7 = tran;
          int num2 = (int) num1;
          short? nullable11 = nullable10;
          nullable1 = nullable11.HasValue ? new int?((int) nullable11.GetValueOrDefault()) : new int?();
          nullable8 = nullable1.HasValue ? new Decimal?((Decimal) (num2 * nullable1.GetValueOrDefault())) : new Decimal?();
          Decimal? orderQty = orderline.OrderQty;
          Decimal? shippedQty = orderline.ShippedQty;
          nullable2 = orderQty.HasValue & shippedQty.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - shippedQty.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable12 = nullable8.HasValue & nullable2.HasValue ? new Decimal?(nullable8.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
          arTran7.Qty = nullable12;
        }
        SOOrderType ordertype = SOOrderType.PK.Find((PXGraph) this, orderline.OrderType);
        this.CopyTranFieldsFromSOLine(tran, ordertype, orderline, out bool _);
        tran.FreezeManualDisc = new bool?(false);
      }
    }
    else if (tran.OrigInvoiceNbr != null)
    {
      nullable1 = tran.OrigInvoiceLineNbr;
      if (nullable1.HasValue)
      {
        PX.Objects.AR.ARTran origTran = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelectReadonly<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) tran.OrigInvoiceType,
          (object) tran.OrigInvoiceNbr,
          (object) tran.OrigInvoiceLineNbr
        }));
        if (origTran != null)
        {
          PX.Objects.AR.ARTran arTran8 = tran;
          nullable1 = tran.InventoryID;
          bool? nullable13 = nullable1.HasValue ? tran.IsStockItem : origTran.IsStockItem;
          arTran8.IsStockItem = nullable13;
          tran.LineType = origTran.LineType;
          PX.Objects.AR.ARTran arTran9 = tran;
          nullable1 = tran.InventoryID;
          int? nullable14 = nullable1 ?? origTran.InventoryID;
          arTran9.InventoryID = nullable14;
          PX.Objects.AR.ARTran arTran10 = tran;
          nullable1 = tran.SubItemID;
          int? nullable15 = nullable1 ?? origTran.SubItemID;
          arTran10.SubItemID = nullable15;
          PX.Objects.AR.ARTran arTran11 = tran;
          nullable1 = tran.SiteID;
          int? nullable16 = nullable1 ?? origTran.SiteID;
          arTran11.SiteID = nullable16;
          PX.Objects.AR.ARTran arTran12 = tran;
          nullable1 = tran.LocationID;
          int? nullable17 = nullable1 ?? origTran.LocationID;
          arTran12.LocationID = nullable17;
          tran.UOM = tran.UOM ?? origTran.UOM;
          tran.LotSerialNbr = tran.LotSerialNbr ?? origTran.LotSerialNbr;
          tran.ExpireDate = tran.ExpireDate ?? origTran.ExpireDate;
          PX.Objects.AR.ARTran arTran13 = tran;
          nullable2 = tran.CuryUnitPrice;
          Decimal? nullable18 = nullable2 ?? origTran.CuryUnitPrice;
          arTran13.CuryUnitPrice = nullable18;
          PX.Objects.AR.ARTran arTran14 = tran;
          nullable1 = tran.AccountID;
          int? nullable19 = nullable1 ?? origTran.AccountID;
          arTran14.AccountID = nullable19;
          PX.Objects.AR.ARTran arTran15 = tran;
          nullable1 = tran.SubID;
          int? nullable20 = nullable1 ?? origTran.SubID;
          arTran15.SubID = nullable20;
          nullable2 = tran.Qty;
          if (!nullable2.HasValue)
          {
            short? nullable21 = INTranType.InvtMultFromInvoiceType(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType);
            PX.Objects.AR.ARTran arTran16 = tran;
            short? nullable22 = nullable21;
            nullable2 = nullable22.HasValue ? new Decimal?((Decimal) nullable22.GetValueOrDefault()) : new Decimal?();
            Decimal num = Math.Abs(origTran.Qty.GetValueOrDefault());
            Decimal? nullable23 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num) : new Decimal?();
            arTran16.Qty = nullable23;
          }
          this.CopyTranFieldsFromOrigTran(tran, origTran);
        }
      }
    }
    nullable2 = tran.CuryUnitPrice;
    if (nullable2.HasValue)
      this.cancelUnitPriceCalculation = true;
    this.forceDiscountCalculation = true;
    try
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, tran.InventoryID);
      if (tran.SOOrderNbr == null)
      {
        bool? nullable24;
        int num;
        if (inventoryItem == null)
        {
          num = 1;
        }
        else
        {
          nullable24 = inventoryItem.StkItem;
          num = !nullable24.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0 && inventoryItem != null)
        {
          nullable24 = inventoryItem.KitItem;
          if (nullable24.GetValueOrDefault())
            throw new PXException("A non-stock kit cannot be added to a document manually. Use the Sales Orders (SO301000) form to prepare an invoice for the corresponding sales order.");
        }
      }
      if (tran.LineType != null)
        PXCacheEx.Adjust<PXFormulaAttribute>(((PXSelectBase) this.Transactions).Cache, (object) null).For<PX.Objects.AR.ARTran.lineType>((System.Action<PXFormulaAttribute>) (f => f.CancelCalculation = true));
      return ((PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions).Insert(tran);
    }
    finally
    {
      this.cancelUnitPriceCalculation = false;
      this.forceDiscountCalculation = false;
      PXCacheEx.Adjust<PXFormulaAttribute>(((PXSelectBase) this.Transactions).Cache, (object) null).For<PX.Objects.AR.ARTran.lineType>((System.Action<PXFormulaAttribute>) (f => f.CancelCalculation = false));
    }
  }

  protected virtual void PostInvoiceDirectLines(
    INIssueEntry docgraph,
    PX.Objects.AR.ARInvoice invoice,
    DocumentList<PX.Objects.IN.INRegister> list,
    List<SOOrderShipment> directShipmentsToCreate)
  {
    List<PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>> list1 = ((IEnumerable<PXResult<PX.Objects.AR.ARTran>>) PXSelectBase<PX.Objects.AR.ARTran, PXSelectJoin<PX.Objects.AR.ARTran, LeftJoin<SOLine, On<PX.Objects.AR.ARTran.FK.SOOrderLine>, LeftJoin<INItemPlan, On<INItemPlan.planID, Equal<PX.Objects.AR.ARTran.planID>>>>, Where2<KeysRelation<CompositeKey<Field<PX.Objects.AR.ARTran.tranType>.IsRelatedTo<PX.Objects.AR.ARInvoice.docType>, Field<PX.Objects.AR.ARTran.refNbr>.IsRelatedTo<PX.Objects.AR.ARInvoice.refNbr>>.WithTablesOf<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>.SameAsCurrent, And<PX.Objects.AR.ARTran.lineType, NotEqual<SOLineType.miscCharge>, And<PX.Objects.AR.ARTran.invtRefNbr, IsNull, And<PX.Objects.AR.ARTran.invtMult, NotEqual<short0>>>>>, OrderBy<Desc<PX.Objects.AR.ARTran.sOOrderType, Asc<PX.Objects.AR.ARTran.sOOrderNbr, Asc<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) invoice
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.AR.ARTran>>().Select<PXResult<PX.Objects.AR.ARTran>, PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>>((Func<PXResult<PX.Objects.AR.ARTran>, PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>>) (r => (PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>) r)).ToList<PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>>();
    if (!list1.Any<PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>>())
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) invoice.RefNbr, new object[1]
      {
        (object) invoice.DocType
      }));
      List<PX.Objects.AR.ARTran> source = new List<PX.Objects.AR.ARTran>();
      PXResultset<PX.Objects.AR.ARTran, SOLine> orderARTranSet = new PXResultset<PX.Objects.AR.ARTran, SOLine>();
      Lazy<SOOrderEntry> lazy = new Lazy<SOOrderEntry>((Func<SOOrderEntry>) (() => PXGraph.CreateInstance<SOOrderEntry>()));
      List<SOOrderShipment> soOrderShipmentList = new List<SOOrderShipment>();
      for (int index = 0; index < list1.Count; ++index)
      {
        PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan> pxResult = list1[index];
        PX.Objects.AR.ARTran arTran1 = PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>.op_Implicit(pxResult);
        INItemPlan inItemPlan = PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>.op_Implicit(pxResult);
        long? nullable1 = inItemPlan.PlanID;
        if (nullable1.HasValue)
          ((PXGraph) this).Caches[typeof (INItemPlan)].SetStatus((object) inItemPlan, (PXEntryStatus) 0);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) arTran1, true);
        PX.Objects.AR.ARTran tran1 = (PX.Objects.AR.ARTran) ((PXSelectBase) this.Transactions).Cache.Locate((object) arTran1);
        PX.Objects.AR.ARTran arTran2 = tran1;
        nullable1 = new long?();
        long? nullable2 = nullable1;
        arTran2.PlanID = nullable2;
        ((PXSelectBase) this.Transactions).Cache.IsDirty = true;
        Decimal? nullable3 = tran1.Qty;
        Decimal num1 = 0M;
        if (nullable3.GetValueOrDefault() == num1 & nullable3.HasValue)
        {
          nullable1 = inItemPlan.PlanID;
          if (nullable1.HasValue)
            ((PXGraph) this).Caches[typeof (INItemPlan)].Delete((object) inItemPlan);
        }
        else
        {
          Decimal? nullable4;
          Decimal? nullable5;
          int? nullable6;
          if (tran1.LineType == "GI")
          {
            if (!source.Any<PX.Objects.AR.ARTran>())
            {
              ((PXSelectBase<INSetup>) docgraph.insetup).Current.HoldEntry = new bool?(false);
              ((PXSelectBase<INSetup>) docgraph.insetup).Current.RequireControlTotal = new bool?(false);
              PX.Objects.IN.INRegister inRegister = new PX.Objects.IN.INRegister()
              {
                BranchID = invoice.BranchID,
                DocType = "I",
                TranDate = invoice.DocDate,
                OrigModule = "SO",
                SrcDocType = invoice.DocType,
                SrcRefNbr = invoice.RefNbr,
                FinPeriodID = invoice.FinPeriodID
              };
              ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Insert(inRegister);
            }
            INTran tran2 = new INTran()
            {
              BranchID = tran1.BranchID,
              DocType = "I",
              TranType = INTranType.TranTypeFromInvoiceType(tran1.TranType, tran1.Qty),
              SOShipmentNbr = tran1.SOShipmentNbr,
              SOShipmentType = tran1.SOShipmentType,
              SOShipmentLineNbr = tran1.SOShipmentLineNbr,
              SOOrderType = tran1.SOOrderType,
              SOOrderNbr = tran1.SOOrderNbr,
              SOOrderLineNbr = tran1.SOOrderLineNbr,
              SOLineType = "GI",
              ARDocType = tran1.TranType,
              ARRefNbr = tran1.RefNbr,
              ARLineNbr = tran1.LineNbr,
              BAccountID = tran1.CustomerID,
              AcctID = tran1.AccountID,
              SubID = tran1.SubID,
              ProjectID = tran1.ProjectID,
              TaskID = tran1.TaskID,
              CostCodeID = tran1.CostCodeID,
              IsStockItem = tran1.IsStockItem,
              InventoryID = tran1.InventoryID,
              SiteID = tran1.SiteID,
              Qty = new Decimal?(0M),
              SubItemID = tran1.SubItemID,
              UOM = tran1.UOM,
              UnitPrice = tran1.UnitPrice,
              TranDesc = tran1.TranDesc,
              ReasonCode = tran1.ReasonCode,
              LocationID = tran1.LocationID
            };
            if (tran1.OrigInvoiceNbr != null)
            {
              short? invtMult = tran1.InvtMult;
              nullable4 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
              nullable5 = tran1.Qty;
              nullable3 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
              Decimal num2 = 0M;
              if (nullable3.GetValueOrDefault() > num2 & nullable3.HasValue)
                tran2.UnitCost = this.CalculateUnitCostForReturnDirectLine(tran1);
            }
            tran2.InvtMult = INTranType.InvtMult(tran2.TranType);
            docgraph.CostCenterDispatcherExt?.SetInventorySource(tran2);
            INTranSplit inTranSplit1 = INTranSplit.FromINTran(docgraph.LineSplittingExt.lsselect.Insert(tran2));
            INTranSplit inTranSplit2 = inTranSplit1;
            nullable6 = new int?();
            int? nullable7 = nullable6;
            inTranSplit2.SplitLineNbr = nullable7;
            inTranSplit1.SubItemID = tran1.SubItemID;
            inTranSplit1.LocationID = tran1.LocationID;
            inTranSplit1.LotSerialNbr = tran1.LotSerialNbr;
            inTranSplit1.ExpireDate = tran1.ExpireDate;
            inTranSplit1.UOM = tran1.UOM;
            INTranSplit inTranSplit3 = inTranSplit1;
            nullable3 = tran1.Qty;
            Decimal? nullable8 = new Decimal?(Math.Abs(nullable3.GetValueOrDefault()));
            inTranSplit3.Qty = nullable8;
            INTranSplit inTranSplit4 = inTranSplit1;
            nullable3 = new Decimal?();
            Decimal? nullable9 = nullable3;
            inTranSplit4.BaseQty = nullable9;
            inTranSplit1.PlanID = inItemPlan.PlanID;
            ((PXSelectBase<INTranSplit>) docgraph.splits).Insert(inTranSplit1);
            source.Add(tran1);
          }
          if (tran1.SOOrderNbr != null)
          {
            ((PXResultset<PX.Objects.AR.ARTran>) orderARTranSet).Add((PXResult<PX.Objects.AR.ARTran>) pxResult);
            if (index + 1 >= list1.Count || !((PXSelectBase) this.Transactions).Cache.ObjectsEqual<PX.Objects.AR.ARTran.sOOrderType, PX.Objects.AR.ARTran.sOOrderNbr>((object) tran1, (object) PXResult<PX.Objects.AR.ARTran, SOLine, INItemPlan>.op_Implicit(list1[index + 1])))
            {
              SOOrderShipment orderShipment = this.UpdateSalesOrderInvoicedDirectly(lazy.Value, orderARTranSet);
              if (orderShipment != null)
              {
                SOOrderShipment soOrderShipment1 = directShipmentsToCreate.FirstOrDefault<SOOrderShipment>((Func<SOOrderShipment, bool>) (s =>
                {
                  if (!(s.OrderType == orderShipment.OrderType) || !(s.OrderNbr == orderShipment.OrderNbr))
                    return false;
                  Guid? shippingRefNoteId1 = s.ShippingRefNoteID;
                  Guid? shippingRefNoteId2 = orderShipment.ShippingRefNoteID;
                  if (shippingRefNoteId1.HasValue != shippingRefNoteId2.HasValue)
                    return false;
                  return !shippingRefNoteId1.HasValue || shippingRefNoteId1.GetValueOrDefault() == shippingRefNoteId2.GetValueOrDefault();
                }));
                if (soOrderShipment1 != null)
                {
                  SOOrderShipment soOrderShipment2 = soOrderShipment1;
                  nullable6 = soOrderShipment2.LineCntr;
                  int? lineCntr = orderShipment.LineCntr;
                  soOrderShipment2.LineCntr = nullable6.HasValue & lineCntr.HasValue ? new int?(nullable6.GetValueOrDefault() + lineCntr.GetValueOrDefault()) : new int?();
                  SOOrderShipment soOrderShipment3 = soOrderShipment1;
                  nullable3 = soOrderShipment3.ShipmentQty;
                  nullable5 = orderShipment.ShipmentQty;
                  Decimal? nullable10;
                  if (!(nullable3.HasValue & nullable5.HasValue))
                  {
                    nullable4 = new Decimal?();
                    nullable10 = nullable4;
                  }
                  else
                    nullable10 = new Decimal?(nullable3.GetValueOrDefault() + nullable5.GetValueOrDefault());
                  soOrderShipment3.ShipmentQty = nullable10;
                  SOOrderShipment soOrderShipment4 = soOrderShipment1;
                  nullable5 = soOrderShipment4.LineTotal;
                  nullable3 = orderShipment.LineTotal;
                  Decimal? nullable11;
                  if (!(nullable5.HasValue & nullable3.HasValue))
                  {
                    nullable4 = new Decimal?();
                    nullable11 = nullable4;
                  }
                  else
                    nullable11 = new Decimal?(nullable5.GetValueOrDefault() + nullable3.GetValueOrDefault());
                  soOrderShipment4.LineTotal = nullable11;
                  SOOrderShipment soOrderShipment5 = soOrderShipment1;
                  bool? createInDoc1 = soOrderShipment5.CreateINDoc;
                  bool? createInDoc2 = orderShipment.CreateINDoc;
                  soOrderShipment5.CreateINDoc = createInDoc1.GetValueOrDefault() || !createInDoc2.GetValueOrDefault() && !createInDoc1.HasValue ? createInDoc1 : createInDoc2;
                }
                else
                {
                  directShipmentsToCreate.Add(orderShipment);
                  soOrderShipment1 = orderShipment;
                }
                soOrderShipmentList.Add(soOrderShipment1);
              }
              ((PXResultset<PX.Objects.AR.ARTran>) orderARTranSet).Clear();
            }
          }
        }
      }
      bool flag = source.Any<PX.Objects.AR.ARTran>();
      if (flag)
      {
        PX.Objects.IN.INRegister copy = PXCache<PX.Objects.IN.INRegister>.CreateCopy(((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current);
        PXFormulaAttribute.CalcAggregate<INTran.qty>(((PXSelectBase) docgraph.transactions).Cache, (object) copy);
        PXFormulaAttribute.CalcAggregate<INTran.tranAmt>(((PXSelectBase) docgraph.transactions).Cache, (object) copy);
        PXFormulaAttribute.CalcAggregate<INTran.tranCost>(((PXSelectBase) docgraph.transactions).Cache, (object) copy);
        ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Update(copy);
      }
      try
      {
        if (flag)
        {
          ((PXAction) docgraph.Save).Press();
          foreach (PX.Objects.AR.ARTran arTran in source)
          {
            arTran.InvtDocType = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.DocType;
            arTran.InvtRefNbr = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.RefNbr;
          }
          foreach (SOOrderShipment soOrderShipment in soOrderShipmentList)
          {
            soOrderShipment.InvtDocType = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.DocType;
            soOrderShipment.InvtRefNbr = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.RefNbr;
            soOrderShipment.InvtNoteID = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.NoteID;
          }
        }
        ((PXAction) this.Save).Press();
      }
      catch
      {
        throw;
      }
      if (flag)
        list.Add(((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current);
      transactionScope.Complete();
    }
  }

  protected virtual SOOrderShipment UpdateSalesOrderInvoicedDirectly(
    SOOrderEntry orderEntry,
    PXResultset<PX.Objects.AR.ARTran, SOLine> orderARTranSet)
  {
    List<PXResult<PX.Objects.AR.ARTran, SOLine>> list1 = ((IQueryable<PXResult<PX.Objects.AR.ARTran>>) orderARTranSet).Select<PXResult<PX.Objects.AR.ARTran>, PXResult<PX.Objects.AR.ARTran, SOLine>>((Expression<Func<PXResult<PX.Objects.AR.ARTran>, PXResult<PX.Objects.AR.ARTran, SOLine>>>) (r => (PXResult<PX.Objects.AR.ARTran, SOLine>) r)).ToList<PXResult<PX.Objects.AR.ARTran, SOLine>>();
    if (list1.Any<PXResult<PX.Objects.AR.ARTran, SOLine>>((Func<PXResult<PX.Objects.AR.ARTran, SOLine>, bool>) (r => !PXResult<PX.Objects.AR.ARTran, SOLine>.op_Implicit(r).LineNbr.HasValue)))
      throw new PXException("Sales Order line was not found.");
    int num1 = list1.GroupBy(r => new
    {
      OrderType = PXResult<PX.Objects.AR.ARTran, SOLine>.op_Implicit(r).OrderType,
      OrderNbr = PXResult<PX.Objects.AR.ARTran, SOLine>.op_Implicit(r).OrderNbr
    }).Count<IGrouping<\u003C\u003Ef__AnonymousType82<string, string>, PXResult<PX.Objects.AR.ARTran, SOLine>>>();
    if (num1 > 1)
      throw new PXArgumentException("orderARTrans");
    if (num1 == 0)
      return (SOOrderShipment) null;
    PXCache cache1 = ((PXSelectBase) this.Transactions).Cache;
    SOLine soLine1 = PXResult<PX.Objects.AR.ARTran, SOLine>.op_Implicit(list1.First<PXResult<PX.Objects.AR.ARTran, SOLine>>());
    ((PXGraph) orderEntry).Clear();
    ((PXSelectBase<SOOrder>) orderEntry.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) orderEntry.Document).Search<SOOrder.orderNbr>((object) soLine1.OrderNbr, new object[1]
    {
      (object) soLine1.OrderType
    }));
    GraphHelper.MarkUpdated(((PXSelectBase) orderEntry.Document).Cache, (object) ((PXSelectBase<SOOrder>) orderEntry.Document).Current, true);
    ((PXSelectBase<SOOrderType>) orderEntry.soordertype).Current.RequireControlTotal = new bool?(false);
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    bool flag1 = false;
    foreach (IGrouping<int?, PXResult<PX.Objects.AR.ARTran, SOLine>> source1 in list1.GroupBy<PXResult<PX.Objects.AR.ARTran, SOLine>, int?>((Func<PXResult<PX.Objects.AR.ARTran, SOLine>, int?>) (r => PXResult<PX.Objects.AR.ARTran, SOLine>.op_Implicit(r).LineNbr)))
    {
      SOLine soLine2 = PXResult<PX.Objects.AR.ARTran, SOLine>.op_Implicit(source1.First<PXResult<PX.Objects.AR.ARTran, SOLine>>());
      if (soLine2.BlanketNbr != null)
      {
        BlanketSOOrder blanketSoOrder1 = PXResultset<BlanketSOOrder>.op_Implicit(PXSelectBase<BlanketSOOrder, PXViewOf<BlanketSOOrder>.BasedOn<SelectFromBase<BlanketSOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOOrder.orderType, Equal<BqlField<SOLine.blanketType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<BlanketSOOrder.orderNbr, IBqlString>.IsEqual<BqlField<SOLine.blanketNbr, IBqlString>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) soLine2
        }, Array.Empty<object>()));
        if (blanketSoOrder1 != null && !blanketSoOrder1.ShipmentCntrUpdated.GetValueOrDefault())
        {
          BlanketSOOrder blanketSoOrder2 = blanketSoOrder1;
          int? shipmentCntr = blanketSoOrder2.ShipmentCntr;
          blanketSoOrder2.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() + 1) : new int?();
          blanketSoOrder1.ShipmentCntrUpdated = new bool?(true);
          GraphHelper.Caches<BlanketSOOrder>((PXGraph) orderEntry).Update(blanketSoOrder1);
        }
      }
      IEnumerable<PX.Objects.AR.ARTran> source2 = source1.Select<PXResult<PX.Objects.AR.ARTran, SOLine>, PX.Objects.AR.ARTran>((Func<PXResult<PX.Objects.AR.ARTran, SOLine>, PX.Objects.AR.ARTran>) (r => PXResult<PX.Objects.AR.ARTran, SOLine>.op_Implicit(r)));
      PX.Objects.AR.ARTran arTran = source2.First<PX.Objects.AR.ARTran>();
      Decimal num4 = source2.Sum<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, Decimal>) (t => Math.Abs(t.Qty.GetValueOrDefault())));
      Decimal num5 = source2.Sum<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, Decimal>) (t => Math.Abs(t.BaseQty.GetValueOrDefault())));
      num2 += num4;
      num3 += source2.Sum<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, Decimal?>) (t => t.TranAmt)).GetValueOrDefault();
      flag1 |= source2.Any<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (t => t.LineType == "GI"));
      foreach (PX.Objects.AR.ARTran line in source2)
        this.ItemAvailabilityExt.OrderCheck(line);
      short? lineSign1 = soLine2.LineSign;
      Decimal? nullable1 = lineSign1.HasValue ? new Decimal?((Decimal) lineSign1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = soLine2.BaseOrderQty;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      lineSign1 = soLine2.LineSign;
      nullable2 = lineSign1.HasValue ? new Decimal?((Decimal) lineSign1.GetValueOrDefault()) : new Decimal?();
      nullable1 = soLine2.BaseShippedQty;
      Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable5 = nullable3;
      Decimal? completeQtyMin = soLine2.CompleteQtyMin;
      nullable2 = nullable5.HasValue & completeQtyMin.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M) : new Decimal?();
      Decimal? nullable6 = nullable4;
      nullable1 = nullable2.HasValue & nullable6.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
      Decimal num6 = num5;
      Decimal? nullable7;
      if (!nullable1.HasValue)
      {
        nullable6 = new Decimal?();
        nullable7 = nullable6;
      }
      else
        nullable7 = new Decimal?(nullable1.GetValueOrDefault() - num6);
      nullable6 = nullable7;
      bool flag2 = PXDBQuantityAttribute.Round(new Decimal?(nullable6.Value)) <= 0M;
      if (soLine2.ShipComplete == "C" && !flag2)
        throw new PXException("Shipment cannot be confirmed because the quantity of the item for the line '{0}' with the Ship Complete setting is less than the ordered quantity.", new object[1]
        {
          cache1.GetValueExt<PX.Objects.AR.ARTran.inventoryID>((object) arTran)
        });
      bool flag3 = flag2 || soLine2.ShipComplete == "L";
      Decimal? nullable8 = nullable3;
      nullable5 = soLine2.CompleteQtyMax;
      nullable6 = nullable8.HasValue & nullable5.HasValue ? new Decimal?(nullable8.GetValueOrDefault() * nullable5.GetValueOrDefault() / 100M) : new Decimal?();
      nullable2 = nullable4;
      Decimal? nullable9;
      if (!(nullable6.HasValue & nullable2.HasValue))
      {
        nullable5 = new Decimal?();
        nullable9 = nullable5;
      }
      else
        nullable9 = new Decimal?(nullable6.GetValueOrDefault() - nullable2.GetValueOrDefault());
      nullable1 = nullable9;
      Decimal num7 = num5;
      Decimal? nullable10;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable10 = nullable2;
      }
      else
        nullable10 = new Decimal?(nullable1.GetValueOrDefault() - num7);
      nullable2 = nullable10;
      if (PXDBQuantityAttribute.Round(new Decimal?(nullable2.Value)) < 0M)
        throw new PXException("Item '{0} {1}' in order '{2} {3}' quantity shipped is greater than quantity ordered.", new object[4]
        {
          cache1.GetValueExt<PX.Objects.AR.ARTran.inventoryID>((object) arTran),
          cache1.GetValueExt<PX.Objects.AR.ARTran.subItemID>((object) arTran),
          cache1.GetValueExt<PX.Objects.AR.ARTran.sOOrderType>((object) arTran),
          cache1.GetValueExt<PX.Objects.AR.ARTran.sOOrderNbr>((object) arTran)
        });
      SOLine copy1 = (SOLine) ((PXSelectBase) orderEntry.Transactions).Cache.CreateCopy((object) soLine2);
      ((PXSelectBase<SOLine>) orderEntry.Transactions).Current = copy1;
      List<PXResult<SOLineSplit, INItemPlan>> list2 = ((IQueryable<PXResult<SOLineSplit>>) PXSelectBase<SOLineSplit, PXSelectJoin<SOLineSplit, InnerJoin<INItemPlan, On<INItemPlan.planID, Equal<SOLineSplit.planID>>>, Where<SOLineSplit.orderType, Equal<Required<SOLineSplit.orderType>>, And<SOLineSplit.orderNbr, Equal<Required<SOLineSplit.orderNbr>>, And<SOLineSplit.lineNbr, Equal<Required<SOLineSplit.lineNbr>>, And<SOLineSplit.completed, Equal<boolFalse>>>>>>.Config>.Select((PXGraph) orderEntry, new object[3]
      {
        (object) copy1.OrderType,
        (object) copy1.OrderNbr,
        (object) copy1.LineNbr
      })).Select<PXResult<SOLineSplit>, PXResult<SOLineSplit, INItemPlan>>((Expression<Func<PXResult<SOLineSplit>, PXResult<SOLineSplit, INItemPlan>>>) (r => (PXResult<SOLineSplit, INItemPlan>) r)).ToList<PXResult<SOLineSplit, INItemPlan>>();
      List<SOLineSplit> list3 = list2.Select<PXResult<SOLineSplit, INItemPlan>, SOLineSplit>((Func<PXResult<SOLineSplit, INItemPlan>, SOLineSplit>) (s => PXResult<SOLineSplit, INItemPlan>.op_Implicit(s))).ToList<SOLineSplit>();
      HashSet<int?> nullableSet = new HashSet<int?>();
      SOLineSplit soLineSplit1 = (SOLineSplit) null;
      using (IEnumerator<PX.Objects.AR.ARTran> enumerator = source2.GetEnumerator())
      {
label_45:
        while (enumerator.MoveNext())
        {
          PX.Objects.AR.ARTran tran = enumerator.Current;
          list3.Where<SOLineSplit>((Func<SOLineSplit, bool>) (s => !s.Completed.GetValueOrDefault())).ToList<SOLineSplit>().Sort((Comparison<SOLineSplit>) ((s1, s2) =>
          {
            if (!string.IsNullOrEmpty(tran.LotSerialNbr) && !string.Equals(s1.LotSerialNbr, s2.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))
            {
              if (string.Equals(s1.LotSerialNbr, tran.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))
                return -1;
              if (string.Equals(s2.LotSerialNbr, tran.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))
                return 1;
            }
            int? locationId = s1.LocationID;
            int? nullable11 = s2.LocationID;
            if (!(locationId.GetValueOrDefault() == nullable11.GetValueOrDefault() & locationId.HasValue == nullable11.HasValue))
            {
              nullable11 = tran.LocationID;
              locationId = s1.LocationID;
              if (nullable11.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable11.HasValue == locationId.HasValue)
                return -1;
              locationId = tran.LocationID;
              nullable11 = s2.LocationID;
              if (locationId.GetValueOrDefault() == nullable11.GetValueOrDefault() & locationId.HasValue == nullable11.HasValue)
                return 1;
            }
            nullable11 = s1.SplitLineNbr;
            int valueOrDefault1 = nullable11.GetValueOrDefault();
            ref int local = ref valueOrDefault1;
            nullable11 = s2.SplitLineNbr;
            int valueOrDefault2 = nullable11.GetValueOrDefault();
            return local.CompareTo(valueOrDefault2);
          }));
          nullable1 = tran.BaseQty;
          Decimal num8 = Math.Abs(nullable1.GetValueOrDefault());
          int index = 0;
          while (true)
          {
            if (index < list3.Count && !(num8 <= 0M))
            {
              SOLineSplit soLineSplit2 = list3[index];
              int num9 = index + 1 >= list3.Count ? 1 : 0;
              nullable1 = soLineSplit2.BaseQty;
              nullable2 = soLineSplit2.BaseShippedQty;
              Decimal? nullable12;
              if (!(nullable1.HasValue & nullable2.HasValue))
              {
                nullable6 = new Decimal?();
                nullable12 = nullable6;
              }
              else
                nullable12 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
              nullable6 = nullable12;
              Decimal num10 = nullable6.Value;
              if (num9 != 0 || num10 >= num8)
              {
                SOLineSplit soLineSplit3 = soLineSplit2;
                nullable1 = soLineSplit3.BaseShippedQty;
                Decimal num11 = num8;
                Decimal? nullable13;
                if (!nullable1.HasValue)
                {
                  nullable2 = new Decimal?();
                  nullable13 = nullable2;
                }
                else
                  nullable13 = new Decimal?(nullable1.GetValueOrDefault() + num11);
                soLineSplit3.BaseShippedQty = nullable13;
                SOLineSplit soLineSplit4 = soLineSplit2;
                PXCache cache2 = ((PXSelectBase) orderEntry.splits).Cache;
                int? inventoryId = soLineSplit2.InventoryID;
                string uom = soLineSplit2.UOM;
                nullable1 = soLineSplit2.BaseShippedQty;
                Decimal num12 = nullable1.Value;
                Decimal? nullable14 = new Decimal?(INUnitAttribute.ConvertFromBase(cache2, inventoryId, uom, num12, INPrecision.QUANTITY));
                soLineSplit4.ShippedQty = nullable14;
                num8 = 0M;
              }
              else
              {
                soLineSplit2.BaseShippedQty = soLineSplit2.BaseQty;
                soLineSplit2.ShippedQty = soLineSplit2.Qty;
                num8 -= num10;
                soLineSplit2.Completed = new bool?(true);
              }
              nullableSet.Add(soLineSplit2.SplitLineNbr);
              soLineSplit1 = soLineSplit2;
              ++index;
            }
            else
              goto label_45;
          }
        }
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXRowUpdating pxRowUpdating = SOInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__213_11 ?? (SOInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__213_11 = new PXRowUpdating((object) SOInvoiceEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateSalesOrderInvoicedDirectly\u003Eb__213_11)));
      ((PXGraph) orderEntry).RowUpdating.AddHandler<SOLine>(pxRowUpdating);
      foreach (PXResult<SOLineSplit, INItemPlan> pxResult in list2)
      {
        SOLineSplit soLineSplit5 = PXResult<SOLineSplit, INItemPlan>.op_Implicit(pxResult);
        if (nullableSet.Contains(soLineSplit5.SplitLineNbr))
        {
          soLineSplit5.Completed = new bool?(true);
          soLineSplit5.ShipComplete = copy1.ShipComplete;
          soLineSplit5.PlanID = new long?();
          soLineSplit5.RefNoteID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.NoteID;
          ((PXSelectBase) orderEntry.splits).Cache.Update((object) soLineSplit5);
          ((PXGraph) orderEntry).Caches[typeof (INItemPlan)].Delete((object) PXResult<SOLineSplit, INItemPlan>.op_Implicit(pxResult));
        }
      }
      Decimal? nullable15;
      Decimal? nullable16;
      Decimal? nullable17;
      if (!flag3)
      {
        SOLineSplit copy2 = PXCache<SOLineSplit>.CreateCopy(soLineSplit1);
        copy2.PlanID = new long?();
        copy2.PlanType = copy2.BackOrderPlanType;
        copy2.ParentSplitLineNbr = copy2.SplitLineNbr;
        copy2.SplitLineNbr = new int?();
        copy2.IsAllocated = new bool?(false);
        copy2.Completed = new bool?(false);
        copy2.ShipmentNbr = (string) null;
        copy2.LotSerialNbr = (string) null;
        copy2.VendorID = new int?();
        copy2.ClearPOFlags();
        copy2.ClearPOReferences();
        copy2.ClearSOReferences();
        copy2.RefNoteID = new Guid?();
        copy2.BaseReceivedQty = new Decimal?(0M);
        copy2.ReceivedQty = new Decimal?(0M);
        copy2.BaseShippedQty = new Decimal?(0M);
        copy2.ShippedQty = new Decimal?(0M);
        SOLineSplit soLineSplit6 = copy2;
        nullable15 = copy1.BaseOrderQty;
        nullable16 = copy1.BaseShippedQty;
        nullable17 = nullable15.HasValue & nullable16.HasValue ? new Decimal?(nullable15.GetValueOrDefault() - nullable16.GetValueOrDefault()) : new Decimal?();
        Decimal num13 = num5;
        Decimal? nullable18;
        if (!nullable17.HasValue)
        {
          nullable16 = new Decimal?();
          nullable18 = nullable16;
        }
        else
          nullable18 = new Decimal?(nullable17.GetValueOrDefault() - num13);
        soLineSplit6.BaseQty = nullable18;
        SOLineSplit soLineSplit7 = copy2;
        PXCache cache3 = ((PXSelectBase) orderEntry.splits).Cache;
        int? inventoryId = copy2.InventoryID;
        string uom = copy2.UOM;
        nullable17 = copy2.BaseQty;
        Decimal num14 = nullable17.Value;
        Decimal? nullable19 = new Decimal?(INUnitAttribute.ConvertFromBase(cache3, inventoryId, uom, num14, INPrecision.QUANTITY));
        soLineSplit7.Qty = nullable19;
        ((PXSelectBase<SOLineSplit>) orderEntry.splits).Insert(copy2);
      }
      ((PXGraph) orderEntry).RowUpdating.RemoveHandler<SOLine>(pxRowUpdating);
      using (orderEntry.LineSplittingExt.SuppressedModeScope(true))
      {
        SOLine soLine3 = copy1;
        nullable17 = soLine3.ShippedQty;
        short? lineSign2 = copy1.LineSign;
        nullable15 = lineSign2.HasValue ? new Decimal?((Decimal) lineSign2.GetValueOrDefault()) : new Decimal?();
        Decimal num15 = num4;
        nullable16 = nullable15.HasValue ? new Decimal?(nullable15.GetValueOrDefault() * num15) : new Decimal?();
        Decimal? nullable20;
        if (!(nullable17.HasValue & nullable16.HasValue))
        {
          nullable15 = new Decimal?();
          nullable20 = nullable15;
        }
        else
          nullable20 = new Decimal?(nullable17.GetValueOrDefault() + nullable16.GetValueOrDefault());
        soLine3.ShippedQty = nullable20;
        SOLine soLine4 = copy1;
        nullable16 = soLine4.BaseShippedQty;
        lineSign2 = copy1.LineSign;
        nullable15 = lineSign2.HasValue ? new Decimal?((Decimal) lineSign2.GetValueOrDefault()) : new Decimal?();
        Decimal num16 = num5;
        nullable17 = nullable15.HasValue ? new Decimal?(nullable15.GetValueOrDefault() * num16) : new Decimal?();
        Decimal? nullable21;
        if (!(nullable16.HasValue & nullable17.HasValue))
        {
          nullable15 = new Decimal?();
          nullable21 = nullable15;
        }
        else
          nullable21 = new Decimal?(nullable16.GetValueOrDefault() + nullable17.GetValueOrDefault());
        soLine4.BaseShippedQty = nullable21;
        if (flag3)
        {
          copy1.OpenQty = new Decimal?(0M);
          copy1.ClosedQty = copy1.OrderQty;
          copy1.BaseClosedQty = copy1.BaseOrderQty;
          copy1.OpenLine = new bool?(false);
          copy1.Completed = new bool?(true);
          SOLine soLine5 = copy1;
          nullable17 = soLine5.UnbilledQty;
          nullable15 = copy1.OrderQty;
          Decimal? shippedQty = copy1.ShippedQty;
          nullable16 = nullable15.HasValue & shippedQty.HasValue ? new Decimal?(nullable15.GetValueOrDefault() - shippedQty.GetValueOrDefault()) : new Decimal?();
          soLine5.UnbilledQty = nullable17.HasValue & nullable16.HasValue ? new Decimal?(nullable17.GetValueOrDefault() - nullable16.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          SOLine soLine6 = copy1;
          nullable16 = copy1.OrderQty;
          nullable17 = copy1.ShippedQty;
          Decimal? nullable22 = nullable16.HasValue & nullable17.HasValue ? new Decimal?(nullable16.GetValueOrDefault() - nullable17.GetValueOrDefault()) : new Decimal?();
          soLine6.OpenQty = nullable22;
          SOLine soLine7 = copy1;
          nullable17 = copy1.BaseOrderQty;
          nullable16 = copy1.BaseShippedQty;
          Decimal? nullable23 = nullable17.HasValue & nullable16.HasValue ? new Decimal?(nullable17.GetValueOrDefault() - nullable16.GetValueOrDefault()) : new Decimal?();
          soLine7.BaseOpenQty = nullable23;
          copy1.ClosedQty = copy1.ShippedQty;
          copy1.BaseClosedQty = copy1.BaseShippedQty;
        }
        ((PXSelectBase) orderEntry.Transactions).Cache.Update((object) copy1);
      }
    }
    SOOrderShipment soOrderShipment = SOOrderShipment.FromDirectInvoice((PX.Objects.AR.ARRegister) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current, ((PXSelectBase<SOOrder>) orderEntry.Document).Current.OrderType, ((PXSelectBase<SOOrder>) orderEntry.Document).Current.OrderNbr);
    soOrderShipment.LineCntr = new int?(list1.Count);
    soOrderShipment.ShipmentQty = new Decimal?(num2);
    soOrderShipment.LineTotal = new Decimal?(num3);
    soOrderShipment.CreateINDoc = new bool?(flag1);
    int? openLineCntr = ((PXSelectBase<SOOrder>) orderEntry.Document).Current.OpenLineCntr;
    int num17 = 0;
    if (openLineCntr.GetValueOrDefault() <= num17 & openLineCntr.HasValue)
      ((PXSelectBase<SOOrder>) orderEntry.Document).Current.MarkCompleted();
    ((PXAction) orderEntry.Save).Press();
    return soOrderShipment;
  }

  protected virtual void CreateDirectShipments(
    List<SOOrderShipment> directShipmentsToCreate,
    List<SOOrderShipment> existingShipments)
  {
    foreach (SOOrderShipment soOrderShipment1 in directShipmentsToCreate)
    {
      SOOrderShipment orderShipment = soOrderShipment1;
      SOOrderShipment soOrderShipment2 = existingShipments.FirstOrDefault<SOOrderShipment>((Func<SOOrderShipment, bool>) (s =>
      {
        if (!(s.OrderType == orderShipment.OrderType) || !(s.OrderNbr == orderShipment.OrderNbr))
          return false;
        Guid? shippingRefNoteId1 = s.ShippingRefNoteID;
        Guid? shippingRefNoteId2 = orderShipment.ShippingRefNoteID;
        if (shippingRefNoteId1.HasValue != shippingRefNoteId2.HasValue)
          return false;
        return !shippingRefNoteId1.HasValue || shippingRefNoteId1.GetValueOrDefault() == shippingRefNoteId2.GetValueOrDefault();
      }));
      SOOrder order = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) this.soorder).Select(new object[2]
      {
        (object) orderShipment.OrderType,
        (object) orderShipment.OrderNbr
      }));
      int? nullable1;
      int? nullable2;
      Decimal? nullable3;
      if (soOrderShipment2 != null)
      {
        SOOrderShipment soOrderShipment3 = (SOOrderShipment) PrimaryKeyOf<SOOrderShipment>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SOOrderShipment.orderType, SOOrderShipment.orderNbr, SOOrderShipment.shippingRefNoteID>>.Find((PXGraph) this, (TypeArrayOf<IBqlField>.IFilledWith<SOOrderShipment.orderType, SOOrderShipment.orderNbr, SOOrderShipment.shippingRefNoteID>) soOrderShipment2, (PKFindOptions) 1);
        SOOrderShipment soOrderShipment4 = soOrderShipment3;
        nullable1 = soOrderShipment4.LineCntr;
        nullable2 = orderShipment.LineCntr;
        soOrderShipment4.LineCntr = nullable1.HasValue & nullable2.HasValue ? new int?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new int?();
        SOOrderShipment soOrderShipment5 = soOrderShipment3;
        nullable3 = soOrderShipment5.ShipmentQty;
        Decimal? nullable4 = orderShipment.ShipmentQty;
        soOrderShipment5.ShipmentQty = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        SOOrderShipment soOrderShipment6 = soOrderShipment3;
        nullable4 = soOrderShipment6.LineTotal;
        nullable3 = orderShipment.LineTotal;
        soOrderShipment6.LineTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        SOOrderShipment soOrderShipment7 = soOrderShipment3;
        bool? createInDoc1 = soOrderShipment7.CreateINDoc;
        bool? createInDoc2 = orderShipment.CreateINDoc;
        soOrderShipment7.CreateINDoc = createInDoc1.GetValueOrDefault() || !createInDoc2.GetValueOrDefault() && !createInDoc1.HasValue ? createInDoc1 : createInDoc2;
        if (soOrderShipment3.InvtRefNbr == null && orderShipment.InvtRefNbr != null)
        {
          soOrderShipment3.InvtDocType = orderShipment.InvtDocType;
          soOrderShipment3.InvtRefNbr = orderShipment.InvtRefNbr;
          soOrderShipment3.InvtNoteID = orderShipment.InvtNoteID;
        }
        ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(soOrderShipment3);
      }
      else
      {
        orderShipment.Operation = order.DefaultOperation;
        orderShipment.OrderNoteID = order.NoteID;
        ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Insert(orderShipment);
        SOOrder soOrder = order;
        nullable2 = soOrder.ShipmentCntr;
        nullable1 = nullable2;
        soOrder.ShipmentCntr = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
      }
      nullable2 = order.OpenLineCntr;
      int num1 = 0;
      if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      {
        nullable3 = order.UnbilledMiscTot;
        Decimal num2 = 0M;
        if (nullable3.GetValueOrDefault() == num2 & nullable3.HasValue)
        {
          nullable3 = order.UnbilledOrderQty;
          Decimal num3 = 0M;
          if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
            order.MarkCompleted();
        }
      }
      ((PXSelectBase<SOOrder>) this.soorder).Update(order);
    }
    if (!((PXSelectBase) this.shipmentlist).Cache.IsInsertedUpdatedDeleted)
      return;
    ((PXAction) this.Save).Press();
  }

  protected virtual Decimal? CalculateUnitCostForReturnDirectLine(PX.Objects.AR.ARTran tran)
  {
    PXSelectReadonly<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARTran.origInvoiceType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARTran.origInvoiceNbr>>, And<PX.Objects.AR.ARTran.inventoryID, Equal<Current<PX.Objects.AR.ARTran.inventoryID>>, And<PX.Objects.AR.ARTran.subItemID, Equal<Current<PX.Objects.AR.ARTran.subItemID>>, And<Mult<PX.Objects.AR.ARTran.qty, PX.Objects.AR.ARTran.invtMult>, LessEqual<decimal0>>>>>>> pxSelectReadonly = new PXSelectReadonly<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARTran.origInvoiceType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARTran.origInvoiceNbr>>, And<PX.Objects.AR.ARTran.inventoryID, Equal<Current<PX.Objects.AR.ARTran.inventoryID>>, And<PX.Objects.AR.ARTran.subItemID, Equal<Current<PX.Objects.AR.ARTran.subItemID>>, And<Mult<PX.Objects.AR.ARTran.qty, PX.Objects.AR.ARTran.invtMult>, LessEqual<decimal0>>>>>>>((PXGraph) this);
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    PXView view = ((PXSelectBase) pxSelectReadonly).View;
    object[] objArray1 = (object[]) new PX.Objects.AR.ARTran[1]{ tran };
    object[] objArray2 = Array.Empty<object>();
    foreach (PX.Objects.AR.ARTran arTran in view.SelectMultiBound(objArray1, objArray2))
    {
      short? nullable1 = INTranType.InvtMultFromInvoiceType(arTran.TranType);
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? qty = arTran.Qty;
      Decimal? nullable3 = nullable2.HasValue & qty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * qty.GetValueOrDefault()) : new Decimal?();
      Decimal num3 = 0M;
      if (nullable3.GetValueOrDefault() < num3 & nullable3.HasValue)
      {
        Decimal num4 = num1;
        nullable3 = arTran.BaseQty;
        Decimal num5 = Math.Abs(nullable3.GetValueOrDefault());
        num1 = num4 + num5;
        Decimal num6 = num2;
        nullable3 = arTran.TranCost;
        Decimal num7 = Math.Abs(nullable3.GetValueOrDefault());
        num2 = num6 + num7;
      }
    }
    return !(num1 == 0M) ? new Decimal?(INUnitAttribute.ConvertToBase(((PXSelectBase) this.Transactions).Cache, tran.InventoryID, tran.UOM, num2 / num1, INPrecision.UNITCOST)) : new Decimal?();
  }

  public override void DefaultDiscountAccountAndSubAccount(PX.Objects.AR.ARTran tran)
  {
    PX.Objects.AR.ARTran arTran = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.SO.SOInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.SO.SOInvoice.refNbr>>, And<PX.Objects.AR.ARTran.sOOrderType, IsNotNull>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (arTran != null)
    {
      SOOrderType data1 = PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.soordertype).SelectWindowed(0, 1, new object[1]
      {
        (object) arTran.SOOrderType
      }));
      if (data1 == null)
        return;
      PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
      PX.Objects.CR.Standalone.Location data2 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<PX.Objects.AR.ARRegister.branchID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      switch (data1.DiscAcctDefault)
      {
        case "T":
          tran.AccountID = (int?) this.GetValue<SOOrderType.discountAcctID>((object) data1);
          break;
        case "L":
          tran.AccountID = (int?) this.GetValue<PX.Objects.CR.Location.cDiscountAcctID>((object) current);
          break;
      }
      if (!tran.AccountID.HasValue)
        tran.AccountID = data1.DiscountAcctID;
      ((PXSelectBase) this.Discount).Cache.RaiseFieldUpdated<PX.Objects.AR.ARTran.accountID>((object) tran, (object) null);
      if (!tran.AccountID.HasValue)
        return;
      object obj1 = this.GetValue<SOOrderType.discountSubID>((object) data1);
      object obj2 = this.GetValue<PX.Objects.CR.Location.cDiscountSubID>((object) current);
      object obj3 = this.GetValue<PX.Objects.CR.Standalone.Location.cMPDiscountSubID>((object) data2);
      object obj4 = (object) SODiscSubAccountMaskAttribute.MakeSub<SOOrderType.discSubMask>((PXGraph) this, data1.DiscSubMask, new object[3]
      {
        obj1,
        obj2,
        obj3
      }, new System.Type[3]
      {
        typeof (SOOrderType.discountSubID),
        typeof (PX.Objects.CR.Location.cDiscountSubID),
        typeof (PX.Objects.CR.Location.cMPDiscountSubID)
      });
      ((PXSelectBase) this.Discount).Cache.RaiseFieldUpdating<PX.Objects.AR.ARTran.subID>((object) tran, ref obj4);
      tran.SubID = (int?) obj4;
    }
    else
      base.DefaultDiscountAccountAndSubAccount(tran);
  }

  public override ARInvoiceState GetDocumentState(PXCache cache, PX.Objects.AR.ARInvoice doc)
  {
    ARInvoiceState documentState = base.GetDocumentState(cache, doc);
    PX.Objects.SO.SOInvoice soInvoice = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.SODocument).Select(new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    documentState.AllowUpdateAdjustments &= soInvoice?.InitialSOBehavior != "MO";
    documentState.AllowDeleteAdjustments &= soInvoice?.InitialSOBehavior != "MO";
    documentState.AllowUpdateCMAdjustments &= soInvoice?.InitialSOBehavior != "MO";
    documentState.LoadDocumentsEnabled &= soInvoice?.InitialSOBehavior != "MO";
    documentState.AutoApplyEnabled &= soInvoice?.InitialSOBehavior != "MO";
    return documentState;
  }

  public virtual SOFreightDetail FillFreightDetails(SOOrder order, SOOrderShipment ordershipment)
  {
    return !string.Equals(ordershipment.ShipmentNbr, "<NEW>") ? this.FillFreightDetailsForShipment(order, ordershipment) : this.FillFreightDetailsForNonShipment(order, ordershipment);
  }

  public virtual SOFreightDetail FillFreightDetailsForNonShipment(
    SOOrder order,
    SOOrderShipment orderShipment)
  {
    SOFreightDetail freightDet = new SOFreightDetail()
    {
      CuryInfoID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryInfoID,
      ShipmentNbr = orderShipment.ShipmentNbr,
      ShipmentType = orderShipment.ShipmentType,
      OrderType = orderShipment.OrderType,
      OrderNbr = orderShipment.OrderNbr,
      ProjectID = order.ProjectID,
      ShipTermsID = order.ShipTermsID,
      ShipVia = order.ShipVia,
      ShipZoneID = order.ShipZoneID,
      TaxCategoryID = order.FreightTaxCategoryID,
      Weight = order.OrderWeight,
      Volume = order.OrderVolume,
      CuryLineTotal = order.CuryLineTotal,
      CuryFreightCost = order.CuryFreightCost,
      CuryFreightAmt = order.CuryFreightAmt,
      CuryPremiumFreightAmt = order.CuryPremiumFreightAmt
    };
    this.PopulateFreightAccountAndSubAccount(freightDet, order, orderShipment);
    return ((PXSelectBase<SOFreightDetail>) this.FreightDetails).Insert(freightDet);
  }

  public virtual SOShipment GetShipment(SOOrderShipment orderShipment)
  {
    return this.GetShipment(orderShipment.ShipmentType, orderShipment.ShipmentNbr);
  }

  protected virtual SOShipment GetShipment(string shipmentType, string shipmentNbr)
  {
    if (string.Equals(shipmentNbr, "<NEW>") || shipmentType == "H")
      return (SOShipment) null;
    return PXResultset<SOShipment>.op_Implicit(PXSelectBase<SOShipment, PXSelect<SOShipment, Where<SOShipment.shipmentType, Equal<Required<SOShipment.shipmentType>>, And<SOShipment.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) shipmentType,
      (object) shipmentNbr
    }));
  }

  public virtual SOFreightDetail FillFreightDetailsForShipment(
    SOOrder order,
    SOOrderShipment orderShipment)
  {
    bool isDropship = orderShipment.ShipmentType == "H";
    SOShipment shipment = this.GetShipment(orderShipment);
    if (!isDropship && shipment == null)
      return (SOFreightDetail) null;
    bool isOrderBased = (shipment?.FreightAmountSource ?? order.FreightAmountSource) == "O";
    Decimal? lineTotal1 = orderShipment.LineTotal;
    Decimal? lineTotal2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.LineTotal;
    Decimal? nullable1;
    if (!(lineTotal1.GetValueOrDefault() == lineTotal2.GetValueOrDefault() & lineTotal1.HasValue == lineTotal2.HasValue))
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().GetCurrencyInfo(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryInfoID);
      lineTotal2 = orderShipment.LineTotal;
      Decimal baseval = lineTotal2.Value;
      nullable1 = new Decimal?(currencyInfo.CuryConvCury(baseval));
    }
    else
      nullable1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryLineTotal;
    Decimal? nullable2 = nullable1;
    SOFreightDetail freightDet = new SOFreightDetail()
    {
      CuryInfoID = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryInfoID,
      ShipmentNbr = orderShipment.ShipmentNbr,
      ShipmentType = orderShipment.ShipmentType,
      OrderType = orderShipment.OrderType,
      OrderNbr = orderShipment.OrderNbr,
      ProjectID = order.ProjectID,
      ShipTermsID = isDropship | isOrderBased ? order.ShipTermsID : shipment.ShipTermsID,
      ShipVia = isDropship ? order.ShipVia : shipment.ShipVia,
      ShipZoneID = isDropship ? order.ShipZoneID : shipment.ShipZoneID,
      TaxCategoryID = order.FreightTaxCategoryID,
      Weight = orderShipment.ShipmentWeight,
      Volume = orderShipment.ShipmentVolume,
      LineTotal = orderShipment.LineTotal,
      CuryLineTotal = nullable2,
      CuryFreightCost = new Decimal?(0M),
      CuryFreightAmt = new Decimal?(0M),
      CuryPremiumFreightAmt = new Decimal?(0M)
    };
    bool fullOrderAllocation = this.IsFullOrderFreightAmountFirstTime(order);
    this.CalcOrderBasedFreight(freightDet, order, orderShipment, isOrderBased, fullOrderAllocation, isDropship);
    this.CalcShipmentBasedFreight(freightDet, orderShipment, shipment, isOrderBased, isDropship);
    this.PopulateFreightAccountAndSubAccount(freightDet, order, orderShipment);
    return this.FillFreightDetailRoundingDiffByShipment(this.FillFreightDetailRoundingDiffByOrder(((PXSelectBase<SOFreightDetail>) this.FreightDetails).Insert(freightDet), order, orderShipment, isOrderBased, fullOrderAllocation), orderShipment, shipment, isOrderBased, isDropship);
  }

  public virtual void CalcOrderBasedFreight(
    SOFreightDetail freightDet,
    SOOrder order,
    SOOrderShipment orderShipment,
    bool isOrderBased,
    bool fullOrderAllocation,
    bool isDropship)
  {
    if (order.DefaultOperation != orderShipment.Operation)
      return;
    Lazy<Decimal> lazy = new Lazy<Decimal>((Func<Decimal>) (() => this.CalcOrderFreightRatio(order, orderShipment)));
    if (fullOrderAllocation)
    {
      if (PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Current<SOOrderShipment.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrderShipment.orderNbr>>, And<SOOrderShipment.invoiceNbr, IsNotNull, And<SOOrderShipment.orderFreightAllocated, Equal<True>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) orderShipment
      }, Array.Empty<object>())) == null)
      {
        freightDet.CuryPremiumFreightAmt = order.CuryPremiumFreightAmt;
        if (isOrderBased)
          freightDet.CuryFreightAmt = order.CuryFreightAmt;
        orderShipment.OrderFreightAllocated = new bool?(true);
      }
    }
    else if (((PXSelectBase<SOSetup>) this.sosetup).Current.FreightAllocation == "P")
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().GetDefaultCurrencyInfo();
      freightDet.CuryPremiumFreightAmt = new Decimal?(defaultCurrencyInfo.RoundCury(lazy.Value * order.CuryPremiumFreightAmt.GetValueOrDefault()));
      if (isOrderBased)
        freightDet.CuryFreightAmt = new Decimal?(defaultCurrencyInfo.RoundCury(lazy.Value * order.CuryFreightAmt.GetValueOrDefault()));
    }
    if (!isDropship)
      return;
    freightDet.CuryFreightCost = new Decimal?(((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().GetDefaultCurrencyInfo().RoundCury(lazy.Value * order.CuryFreightCost.GetValueOrDefault()));
  }

  public virtual void CalcShipmentBasedFreight(
    SOFreightDetail freightDet,
    SOOrderShipment orderShipment,
    SOShipment shipment,
    bool isOrderBased,
    bool isDropship)
  {
    if (isDropship)
      return;
    Decimal num = this.CalcShipmentFreightRatio(orderShipment, shipment);
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().GetDefaultCurrencyInfo();
    if (string.Equals(shipment.CuryID, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryID, StringComparison.OrdinalIgnoreCase))
    {
      freightDet.CuryFreightCost = new Decimal?(defaultCurrencyInfo.RoundCury(num * shipment.CuryFreightCost.GetValueOrDefault()));
      if (isOrderBased)
        return;
      freightDet.CuryFreightAmt = new Decimal?(defaultCurrencyInfo.RoundCury(num * shipment.CuryFreightAmt.GetValueOrDefault()));
    }
    else
    {
      freightDet.FreightCost = new Decimal?(num * shipment.FreightCost.GetValueOrDefault());
      freightDet.CuryFreightCost = new Decimal?(defaultCurrencyInfo.CuryConvCury(freightDet.FreightCost.Value));
      if (isOrderBased)
        return;
      freightDet.FreightAmt = new Decimal?(num * shipment.FreightAmt.GetValueOrDefault());
      freightDet.CuryFreightAmt = new Decimal?(defaultCurrencyInfo.CuryConvCury(freightDet.FreightAmt.Value));
    }
  }

  public virtual bool IsFullOrderFreightAmountFirstTime(SOOrder order)
  {
    if (!(((PXSelectBase<SOSetup>) this.sosetup).Current.FreightAllocation == "A"))
    {
      Decimal? lineTotal = order.LineTotal;
      Decimal num = 0M;
      if (!(lineTotal.GetValueOrDefault() <= num & lineTotal.HasValue))
      {
        if (order.Behavior != "RM")
          return false;
        return PXResultset<SOOrderTypeOperation>.op_Implicit(PXSelectBase<SOOrderTypeOperation, PXSelectReadonly<SOOrderTypeOperation, Where<SOOrderTypeOperation.orderType, Equal<Required<SOOrderTypeOperation.orderType>>, And<SOOrderTypeOperation.operation, NotEqual<Required<SOOrderTypeOperation.operation>>, And<SOOrderTypeOperation.active, Equal<True>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
        {
          (object) order.OrderType,
          (object) order.DefaultOperation
        })) != null;
      }
    }
    return true;
  }

  public virtual Decimal CalcOrderFreightRatio(SOOrder order, SOOrderShipment orderShipment)
  {
    Decimal? nullable1;
    if (orderShipment.ShipmentType == "H")
    {
      nullable1 = orderShipment.LineTotal;
      Decimal num1 = 0M;
      if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      {
        nullable1 = order.CuryLineTotal;
        Decimal num2 = 0M;
        if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
          return 1M;
        Decimal num3 = 0M;
        foreach (PXResult<SOLine, SOLineSplit, PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine> pxResult in PXSelectBase<SOLine, PXSelectJoin<SOLine, InnerJoin<SOLineSplit, On<SOLineSplit.orderType, Equal<SOLine.orderType>, And<SOLineSplit.orderNbr, Equal<SOLine.orderNbr>, And<SOLineSplit.lineNbr, Equal<SOLine.lineNbr>>>>, InnerJoin<PX.Objects.PO.POLine, On<PX.Objects.PO.POLine.orderType, Equal<SOLineSplit.pOType>, And<PX.Objects.PO.POLine.orderNbr, Equal<SOLineSplit.pONbr>, And<PX.Objects.PO.POLine.lineNbr, Equal<SOLineSplit.pOLineNbr>>>>, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.PO.POReceiptLine.pOType, Equal<PX.Objects.PO.POLine.orderType>>>>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<SOLine.orderType, Equal<Required<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Required<SOLine.orderNbr>>>>>>>.Config>.Select((PXGraph) this, new object[4]
        {
          orderShipment.Operation == "R" ? (object) "RN" : (object) "RT",
          (object) orderShipment.ShipmentNbr,
          (object) orderShipment.OrderType,
          (object) orderShipment.OrderNbr
        }))
        {
          SOLine soLine = PXResult<SOLine, SOLineSplit, PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
          PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<SOLine, SOLineSplit, PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
          nullable1 = soLine.BaseOrderQty;
          Decimal num4;
          if (!(nullable1.GetValueOrDefault() > 0M))
          {
            num4 = 1M;
          }
          else
          {
            nullable1 = poReceiptLine.BaseReceiptQty;
            Decimal? baseOrderQty = soLine.BaseOrderQty;
            Decimal? nullable2;
            Decimal? nullable3;
            if (!(nullable1.HasValue & baseOrderQty.HasValue))
            {
              nullable2 = new Decimal?();
              nullable3 = nullable2;
            }
            else
              nullable3 = new Decimal?(nullable1.GetValueOrDefault() / baseOrderQty.GetValueOrDefault());
            nullable2 = nullable3;
            num4 = nullable2.Value;
          }
          Decimal num5 = num4;
          Decimal num6 = num3;
          nullable1 = soLine.CuryLineAmt;
          Decimal num7 = nullable1.GetValueOrDefault() * num5;
          num3 = num6 + num7;
        }
        return Math.Min(1M, num3 / order.CuryLineTotal.Value);
      }
    }
    nullable1 = order.LineTotal;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return 1M;
    nullable1 = orderShipment.LineTotal;
    Decimal? lineTotal = order.LineTotal;
    return Math.Min(1M, (nullable1.HasValue & lineTotal.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / lineTotal.GetValueOrDefault()) : new Decimal?()).Value);
  }

  public virtual Decimal CalcShipmentFreightRatio(
    SOOrderShipment orderShipment,
    SOShipment shipment)
  {
    Decimal? lineTotal1 = shipment.LineTotal;
    Decimal num = 0M;
    if (lineTotal1.GetValueOrDefault() == num & lineTotal1.HasValue)
      return 1M;
    Decimal? lineTotal2 = orderShipment.LineTotal;
    Decimal? lineTotal3 = shipment.LineTotal;
    return Math.Min(1M, (lineTotal2.HasValue & lineTotal3.HasValue ? new Decimal?(lineTotal2.GetValueOrDefault() / lineTotal3.GetValueOrDefault()) : new Decimal?()).Value);
  }

  public virtual SOFreightDetail FillFreightDetailRoundingDiffByShipment(
    SOFreightDetail freightDet,
    SOOrderShipment orderShipment,
    SOShipment shipment,
    bool isOrderBased,
    bool isDropship)
  {
    if (isDropship || !string.Equals(shipment.CuryID, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryID, StringComparison.OrdinalIgnoreCase))
      return freightDet;
    PXResultset<SOFreightDetail> pxResultset1 = PXSelectBase<SOFreightDetail, PXSelect<SOFreightDetail, Where<SOFreightDetail.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOFreightDetail.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<SOFreightDetail.shipmentType, Equal<Required<SOFreightDetail.shipmentType>>, And<SOFreightDetail.shipmentNbr, Equal<Required<SOFreightDetail.shipmentNbr>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) orderShipment.ShipmentType,
      (object) orderShipment.ShipmentNbr
    });
    if (pxResultset1.Count <= 1)
      return freightDet;
    PXResultset<SOOrderShipment> pxResultset2 = PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.shipmentType, Equal<Required<SOOrderShipment.shipmentType>>, And<SOOrderShipment.shipmentNbr, Equal<Required<SOOrderShipment.shipmentNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) orderShipment.ShipmentType,
      (object) orderShipment.ShipmentNbr
    });
    if (pxResultset1.Count != pxResultset2.Count)
      return freightDet;
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    foreach (PXResult<SOFreightDetail> pxResult in pxResultset1)
    {
      SOFreightDetail soFreightDetail = PXResult<SOFreightDetail>.op_Implicit(pxResult);
      Decimal num3 = num1;
      Decimal? nullable = soFreightDetail.CuryFreightCost;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      num1 = num3 + valueOrDefault1;
      Decimal num4 = num2;
      nullable = soFreightDetail.CuryFreightAmt;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      num2 = num4 + valueOrDefault2;
    }
    Decimal? nullable1 = shipment.CuryFreightCost;
    Decimal num5 = nullable1.GetValueOrDefault() - num1;
    nullable1 = shipment.CuryFreightAmt;
    Decimal num6 = nullable1.GetValueOrDefault() - num2;
    if (!(num5 != 0M) && (isOrderBased || !(num6 != 0M)))
      return freightDet;
    SOFreightDetail soFreightDetail1 = freightDet;
    nullable1 = soFreightDetail1.CuryFreightCost;
    Decimal num7 = num5;
    soFreightDetail1.CuryFreightCost = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num7) : new Decimal?();
    nullable1 = freightDet.CuryFreightCost;
    Decimal num8 = 0M;
    if (nullable1.GetValueOrDefault() < num8 & nullable1.HasValue)
      freightDet.CuryFreightCost = new Decimal?(0M);
    if (!isOrderBased)
    {
      SOFreightDetail soFreightDetail2 = freightDet;
      nullable1 = soFreightDetail2.CuryFreightAmt;
      Decimal num9 = num6;
      soFreightDetail2.CuryFreightAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num9) : new Decimal?();
      nullable1 = freightDet.CuryFreightAmt;
      Decimal num10 = 0M;
      if (nullable1.GetValueOrDefault() < num10 & nullable1.HasValue)
        freightDet.CuryFreightAmt = new Decimal?(0M);
    }
    return ((PXSelectBase<SOFreightDetail>) this.FreightDetails).Update(freightDet);
  }

  public virtual SOFreightDetail FillFreightDetailRoundingDiffByOrder(
    SOFreightDetail freightDet,
    SOOrder order,
    SOOrderShipment orderShipment,
    bool isOrderBased,
    bool fullOrderAllocation)
  {
    int? openLineCntr = order.OpenLineCntr;
    int num1 = 0;
    if (!(openLineCntr.GetValueOrDefault() == num1 & openLineCntr.HasValue) | fullOrderAllocation)
      return freightDet;
    PXResultset<SOFreightDetail> pxResultset1 = PXSelectBase<SOFreightDetail, PXSelect<SOFreightDetail, Where<SOFreightDetail.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<SOFreightDetail.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<SOFreightDetail.orderType, Equal<Required<SOFreightDetail.orderType>>, And<SOFreightDetail.orderNbr, Equal<Required<SOFreightDetail.orderNbr>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    });
    if (pxResultset1.Count <= 1)
      return freightDet;
    PXResultset<SOOrderShipment> pxResultset2 = PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Required<SOOrderShipment.orderType>>, And<SOOrderShipment.orderNbr, Equal<Required<SOOrderShipment.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    });
    if (pxResultset1.Count != pxResultset2.Count)
      return freightDet;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    foreach (PXResult<SOFreightDetail> pxResult in pxResultset1)
    {
      SOFreightDetail soFreightDetail = PXResult<SOFreightDetail>.op_Implicit(pxResult);
      Decimal num4 = num2;
      Decimal? nullable = soFreightDetail.CuryFreightAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      num2 = num4 + valueOrDefault1;
      Decimal num5 = num3;
      nullable = soFreightDetail.CuryPremiumFreightAmt;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      num3 = num5 + valueOrDefault2;
    }
    Decimal num6 = order.CuryFreightAmt.GetValueOrDefault() - num2;
    Decimal num7 = order.CuryPremiumFreightAmt.GetValueOrDefault() - num3;
    if ((!isOrderBased || !(num6 != 0M)) && !(num7 != 0M))
      return freightDet;
    Decimal? nullable1;
    if (isOrderBased)
    {
      SOFreightDetail soFreightDetail = freightDet;
      nullable1 = soFreightDetail.CuryFreightAmt;
      Decimal num8 = num6;
      soFreightDetail.CuryFreightAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num8) : new Decimal?();
      nullable1 = freightDet.CuryFreightAmt;
      Decimal num9 = 0M;
      if (nullable1.GetValueOrDefault() < num9 & nullable1.HasValue)
        freightDet.CuryFreightAmt = new Decimal?(0M);
    }
    SOFreightDetail soFreightDetail1 = freightDet;
    nullable1 = soFreightDetail1.CuryPremiumFreightAmt;
    Decimal num10 = num7;
    soFreightDetail1.CuryPremiumFreightAmt = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num10) : new Decimal?();
    return ((PXSelectBase<SOFreightDetail>) this.FreightDetails).Update(freightDet);
  }

  public virtual PX.Objects.AR.ARTran UpdateFreightTransaction(
    SOFreightDetail fd,
    bool newFreightDetail)
  {
    PX.Objects.AR.ARTran arTran1 = (PX.Objects.AR.ARTran) null;
    if (!newFreightDetail && ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current != null)
      arTran1 = this.GetFreightTran(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.RefNbr, fd);
    Decimal? nullable = fd.CuryFreightAmt;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
    {
      nullable = fd.CuryPremiumFreightAmt;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      {
        nullable = fd.CuryFreightCost;
        Decimal num3 = 0M;
        if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
        {
          if (arTran1 != null)
            ((PXSelectBase<PX.Objects.AR.ARTran>) this.Freight).Delete(arTran1);
          return (PX.Objects.AR.ARTran) null;
        }
      }
    }
    bool flag = arTran1 == null;
    PX.Objects.AR.ARTran arTran2 = arTran1 ?? new PX.Objects.AR.ARTran();
    arTran2.SOShipmentNbr = fd.ShipmentNbr;
    arTran2.SOShipmentType = fd.ShipmentType ?? "I";
    arTran2.SOOrderType = fd.OrderType;
    arTran2.SOOrderNbr = fd.OrderNbr;
    arTran2.LineType = "FR";
    arTran2.Qty = new Decimal?((Decimal) 1);
    arTran2.CuryUnitPrice = fd.CuryTotalFreightAmt;
    arTran2.CuryUnitPriceDR = fd.CuryTotalFreightAmt;
    arTran2.CuryTranAmt = fd.CuryTotalFreightAmt;
    arTran2.CuryExtPrice = fd.CuryTotalFreightAmt;
    arTran2.TranCostOrig = fd.FreightCost;
    arTran2.TaxCategoryID = fd.TaxCategoryID;
    arTran2.AccountID = fd.AccountID;
    arTran2.SubID = fd.SubID;
    arTran2.ProjectID = fd.ProjectID;
    arTran2.TaskID = fd.TaskID;
    arTran2.Commissionable = new bool?(false);
    if (CostCodeAttribute.UseCostCode())
      arTran2.CostCodeID = CostCodeAttribute.DefaultCostCode;
    using (new PXLocaleScope(((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.LocaleName))
      arTran2.TranDesc = PXMessages.LocalizeFormatNoPrefix("Freight ShipVia {0}", new object[1]
      {
        (object) fd.ShipVia
      });
    PX.Objects.AR.ARTran arTran3 = flag ? ((PXSelectBase<PX.Objects.AR.ARTran>) this.Freight).Insert(arTran2) : ((PXSelectBase<PX.Objects.AR.ARTran>) this.Freight).Update(arTran2);
    if (!arTran3.TaskID.HasValue && !ProjectDefaultAttribute.IsNonProject(arTran3.ProjectID))
      throw new PXException("Failed to automatically assign Project Task to the Transaction. Please check that the Account-Task mapping exists for the given account '{0}' and Task is Active and Visible in the given Module.", new object[1]
      {
        (object) PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) arTran3.AccountID
        })).AccountCD
      });
    return arTran3;
  }

  public virtual PX.Objects.AR.ARTran GetFreightTran(
    string docType,
    string refNbr,
    SOFreightDetail fd)
  {
    return PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.lineType, Equal<SOLineType.freight>, And<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.sOShipmentType, Equal<Required<PX.Objects.AR.ARTran.sOShipmentType>>, And<PX.Objects.AR.ARTran.sOShipmentNbr, Equal<Required<PX.Objects.AR.ARTran.sOShipmentNbr>>, And<PX.Objects.AR.ARTran.sOOrderType, Equal<Required<PX.Objects.AR.ARTran.sOOrderType>>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<Required<PX.Objects.AR.ARTran.sOOrderNbr>>>>>>>>>>.Config>.Select((PXGraph) this, new object[6]
    {
      (object) docType,
      (object) refNbr,
      (object) fd.ShipmentType,
      (object) fd.ShipmentNbr,
      (object) fd.OrderType,
      (object) fd.OrderNbr
    }));
  }

  public virtual void CopyFreightNotesAndFilesToARTran()
  {
    this.CopyFreightNotesAndFilesToARTran((PX.Objects.AR.ARRegister) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current);
  }

  public virtual void CopyFreightNotesAndFilesToARTran(PX.Objects.AR.ARRegister doc)
  {
    if (doc == null || !doc.Released.GetValueOrDefault())
      return;
    IEnumerable<SOFreightDetail> source = GraphHelper.RowCast<SOFreightDetail>((IEnumerable) ((PXSelectBase<SOFreightDetail>) this.FreightDetails).Select(new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    if (!source.Any<SOFreightDetail>())
      return;
    ILookup<Composite<string, string, string, string>, PX.Objects.AR.ARTran> lookup = ((IEnumerable<PX.Objects.AR.ARTran>) ((PXSelectBase<PX.Objects.AR.ARTran>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.tranType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.refNbr, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.lineType, IBqlString>.IsEqual<SOLineType.freight>>>>, PX.Objects.AR.ARTran>.View((PXGraph) this)).SelectMain(new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    })).ToLookup<PX.Objects.AR.ARTran, Composite<string, string, string, string>>((Func<PX.Objects.AR.ARTran, Composite<string, string, string, string>>) (x => Composite.Create<string, string, string, string>(x.SOOrderType, x.SOOrderNbr, x.SOShipmentType, x.SOShipmentNbr)));
    foreach (SOFreightDetail soFreightDetail in source)
    {
      foreach (PX.Objects.AR.ARTran arTran in lookup[Composite.Create<string, string, string, string>(soFreightDetail.OrderType, soFreightDetail.OrderNbr, soFreightDetail.ShipmentType, soFreightDetail.ShipmentNbr)])
        PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.FreightDetails).Cache, (object) soFreightDetail, ((PXSelectBase) this.Freight).Cache, (object) arTran, (PXNoteAttribute.IPXCopySettings) null);
    }
  }

  public virtual void PopulateFreightAccountAndSubAccount(
    SOFreightDetail freightDet,
    SOOrder order,
    SOOrderShipment orderShipment)
  {
    int? accountID;
    object subID;
    this.GetFreightAccountAndSubAccount(order, freightDet.ShipVia, order.OwnerID, out accountID, out subID);
    freightDet.AccountID = accountID;
    ((PXSelectBase) this.FreightDetails).Cache.RaiseFieldUpdating<SOFreightDetail.subID>((object) freightDet, ref subID);
    freightDet.SubID = (int?) subID;
  }

  public virtual void GetFreightAccountAndSubAccount(
    SOOrder order,
    string ShipVia,
    int? ownerID,
    out int? accountID,
    out object subID)
  {
    accountID = new int?();
    subID = (object) null;
    SOOrderType data1 = PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.soordertype).SelectWindowed(0, 1, new object[1]
    {
      (object) order.OrderType
    }));
    if (data1 == null)
      return;
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
    PX.Objects.CS.Carrier data2 = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ShipVia);
    PX.Objects.CR.Standalone.Location data3 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<PX.Objects.AR.ARRegister.branchID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PX.Objects.EP.EPEmployee data4 = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<SOOrder.ownerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ownerID
    }));
    switch (data1.FreightAcctDefault)
    {
      case "T":
        accountID = (int?) this.GetValue<SOOrderType.freightAcctID>((object) data1);
        break;
      case "L":
        accountID = (int?) this.GetValue<PX.Objects.CR.Location.cFreightAcctID>((object) current);
        break;
      case "V":
        accountID = (int?) this.GetValue<PX.Objects.CS.Carrier.freightSalesAcctID>((object) data2);
        break;
    }
    if (!accountID.HasValue)
    {
      accountID = data1.FreightAcctID;
      if (!accountID.HasValue)
        throw new PXException("Freight Account is required. Order Type is not properly setup.");
    }
    if (!accountID.HasValue)
      return;
    object obj1 = this.GetValue<SOOrderType.freightSubID>((object) data1);
    object obj2 = this.GetValue<PX.Objects.CR.Location.cFreightSubID>((object) current);
    object obj3 = this.GetValue<PX.Objects.CS.Carrier.freightSalesSubID>((object) data2);
    object obj4 = this.GetValue<PX.Objects.CR.Standalone.Location.cMPFreightSubID>((object) data3);
    object obj5 = this.GetValue<PX.Objects.EP.EPEmployee.salesSubID>((object) data4);
    if (obj5 != null)
      subID = (object) SOFreightSubAccountMaskAttribute.MakeSub<SOOrderType.freightSubMask>((PXGraph) this, data1.FreightSubMask, new object[5]
      {
        obj1,
        obj2,
        obj3,
        obj4,
        obj5
      }, new System.Type[5]
      {
        typeof (SOOrderType.freightSubID),
        typeof (PX.Objects.CR.Location.cFreightSubID),
        typeof (PX.Objects.CS.Carrier.freightSalesSubID),
        typeof (PX.Objects.CR.Location.cMPFreightSubID),
        typeof (PX.Objects.EP.EPEmployee.salesSubID)
      });
    else
      subID = (object) SOFreightSubAccountMaskAttribute.MakeSub<SOOrderType.freightSubMask>((PXGraph) this, data1.FreightSubMask, new object[5]
      {
        obj1,
        obj2,
        obj3,
        obj4,
        obj2
      }, new System.Type[5]
      {
        typeof (SOOrderType.freightSubID),
        typeof (PX.Objects.CR.Location.cFreightSubID),
        typeof (PX.Objects.CS.Carrier.freightSalesSubID),
        typeof (PX.Objects.CR.Location.cMPFreightSubID),
        typeof (PX.Objects.CR.Location.cFreightSubID)
      });
  }

  public override void RecalculateDiscounts(PXCache sender, PX.Objects.AR.ARTran line)
  {
    this.RecalculateDiscounts(sender, line, false);
  }

  public virtual void RecalculateDiscounts(
    PXCache sender,
    PX.Objects.AR.ARTran line,
    bool disableGroupAndDocumentDiscountsCalculation)
  {
    this.RecalculateDiscounts(sender, line, disableGroupAndDocumentDiscountsCalculation, false);
  }

  public virtual void RecalculateDiscounts(
    PXCache sender,
    PX.Objects.AR.ARTran line,
    bool disableGroupAndDocumentDiscountsCalculation,
    bool forceFormulaRecalculation)
  {
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions = this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current) | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation;
    if (line.CalculateDiscountsOnImport.GetValueOrDefault())
      discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport;
    if (disableGroupAndDocumentDiscountsCalculation)
      discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.DisableGroupAndDocumentDiscounts;
    if (forceFormulaRecalculation)
      discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.ForceFormulaRecalculation;
    if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() && line.InventoryID.HasValue && line.Qty.HasValue && line.CuryTranAmt.HasValue && !line.IsFree.GetValueOrDefault())
    {
      object copy = sender.CreateCopy(sender.Current);
      DateTime? docDate = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocDate;
      int? customerLocationId = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CustomerLocationID;
      this.ARDiscountEngine.SetDiscounts(sender, (PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions, line, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.BranchID, customerLocationId, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.CuryID, docDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, discountCalculationOptions);
      this.RecalculateTotalDiscount();
      if (!sender.Graph.IsMobile && !sender.Graph.IsContractBasedAPI)
        return;
      sender.Current = copy;
    }
    else
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() || ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current == null)
        return;
      this.ARDiscountEngine.CalculateDocumentDiscountRate(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<PX.Objects.AR.ARTran>) this.Transactions, line, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, discountCalculationOptions);
    }
  }

  public bool ProrateDiscount
  {
    get
    {
      SOSetup soSetup = PXResultset<SOSetup>.op_Implicit(PXSelectBase<SOSetup, PXSelect<SOSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      return soSetup == null || !soSetup.ProrateDiscounts.HasValue || soSetup.ProrateDiscounts.GetValueOrDefault();
    }
  }

  protected override bool AskUserApprovalIfInvoiceIsLinkedToShipment(PX.Objects.AR.ARInvoice origDoc)
  {
    return true;
  }

  public override void PrefetchWithDetails() => this.LoadEntityDiscounts();

  private void ThrowIfSOInvoiceHasRelatedCorrectionReceipt(PX.Objects.AR.ARTran line)
  {
    if (line.SOShipmentType != "H")
      return;
    PX.Objects.PO.POReceipt poReceipt = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderShipment>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<SOOrderShipment.shipmentNbr>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<POReceiptType.poreceipt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.isUnderCorrection, Equal<True>>>>>.Or<BqlOperand<PX.Objects.PO.POReceipt.canceled, IBqlBool>.IsEqual<True>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderShipment.shipmentType, Equal<BqlField<PX.Objects.AR.ARTran.sOShipmentType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOOrderShipment.shipmentNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOShipmentNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOOrderShipment.orderType, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOOrderType, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOOrderShipment.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOOrderNbr, IBqlString>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) new PX.Objects.AR.ARTran[1]
    {
      line
    }, Array.Empty<object>()));
    if (poReceipt != null)
      throw new PXException("The invoice cannot be created because the {0} drop shipment is under correction.", new object[1]
      {
        (object) poReceipt.ReceiptNbr
      });
  }

  protected virtual SOOrderShipment UpdateDropShipmentFromPOOrder(
    SOOrderShipment shipment,
    SOOrder soOrder)
  {
    PXResultset<SOLineSplit> pxResultset = PXSelectBase<SOLineSplit, PXViewOf<SOLineSplit>.BasedOn<SelectFromBase<SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineSplit.pOReceiptNbr, Equal<BqlField<SOOrderShipment.shipmentNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLineSplit.pOReceiptType, IBqlString>.IsEqual<POReceiptType.poreceipt>>>, And<BqlOperand<SOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<SOOrderShipment.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOLineSplit.orderType, IBqlString>.IsEqual<BqlField<SOOrderShipment.orderType, IBqlString>.FromCurrent>>>.Aggregate<To<GroupBy<SOLineSplit.pONbr>, GroupBy<SOLineSplit.pOType>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this, (object[]) new SOOrderShipment[1]
    {
      shipment
    }, Array.Empty<object>());
    POAddress poAddress1 = (POAddress) null;
    SOAddress soAddress1 = SOAddress.PK.Find((PXGraph) this, shipment.ShipAddressID);
    foreach (PXResult<SOLineSplit> pxResult in pxResultset)
    {
      SOLineSplit soLineSplit = PXResult<SOLineSplit>.op_Implicit(pxResult);
      POAddress poAddress2 = POAddress.PK.Find((PXGraph) this, PX.Objects.PO.POOrder.PK.Find((PXGraph) this, soLineSplit.POType, soLineSplit.PONbr).ShipAddressID);
      if (poAddress2 != null)
      {
        bool? isDefaultAddress = poAddress2.IsDefaultAddress;
        bool flag = false;
        if (isDefaultAddress.GetValueOrDefault() == flag & isDefaultAddress.HasValue)
        {
          if (SOAddress.IsEquivalentAddress(soAddress1, poAddress2))
            return shipment;
          if (poAddress1 != null)
          {
            DateTime? modifiedDateTime1 = poAddress1.LastModifiedDateTime;
            DateTime? modifiedDateTime2 = poAddress2.LastModifiedDateTime;
            if ((modifiedDateTime1.HasValue & modifiedDateTime2.HasValue ? (modifiedDateTime1.GetValueOrDefault() < modifiedDateTime2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
              continue;
          }
          poAddress1 = poAddress2;
        }
      }
    }
    if (poAddress1 == null)
    {
      int? shipAddressId1 = shipment.ShipAddressID;
      int? shipAddressId2 = soOrder.ShipAddressID;
      if (shipAddressId1.GetValueOrDefault() == shipAddressId2.GetValueOrDefault() & shipAddressId1.HasValue == shipAddressId2.HasValue)
        return shipment;
      shipment.ShipAddressID = soOrder.ShipAddressID;
      return ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(shipment);
    }
    SOAddress soAddress2 = ((PXSelectBase<SOAddress>) this.SOAddressView).Insert(SOAddress.CreateFromPOAddress(poAddress1));
    shipment.ShipAddressID = soAddress2.AddressID;
    return ((PXSelectBase<SOOrderShipment>) this.shipmentlist).Update(shipment);
  }

  protected virtual void LoadEntityDiscounts()
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current?.RefNbr == null || !PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() || this.prefetched.Contains(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType + ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.RefNbr) || PXView.MaximumRows == 1)
      return;
    PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DiscountItem>.On<BqlOperand<DiscountItem.inventoryID, IBqlInt>.IsEqual<PX.Objects.AR.ARTran.inventoryID>>>, FbqlJoins.Left<PX.Objects.AR.DiscountSequence>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.DiscountSequence.isActive, Equal<True>>>>>.And<DiscountItem.FK.DiscountSequence>>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.AR.ARTran.tranType>.IsRelatedTo<PX.Objects.AR.ARInvoice.docType>, Field<PX.Objects.AR.ARTran.refNbr>.IsRelatedTo<PX.Objects.AR.ARInvoice.refNbr>>.WithTablesOf<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>.SameAsCurrent>>.ReadOnly readOnly = new PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DiscountItem>.On<BqlOperand<DiscountItem.inventoryID, IBqlInt>.IsEqual<PX.Objects.AR.ARTran.inventoryID>>>, FbqlJoins.Left<PX.Objects.AR.DiscountSequence>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.DiscountSequence.isActive, Equal<True>>>>>.And<DiscountItem.FK.DiscountSequence>>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.AR.ARTran.tranType>.IsRelatedTo<PX.Objects.AR.ARInvoice.docType>, Field<PX.Objects.AR.ARTran.refNbr>.IsRelatedTo<PX.Objects.AR.ARInvoice.refNbr>>.WithTablesOf<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>.SameAsCurrent>>.ReadOnly((PXGraph) this);
    Dictionary<int, HashSet<DiscountSequenceKey>> items = new Dictionary<int, HashSet<DiscountSequenceKey>>();
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new System.Type[3]
    {
      typeof (PX.Objects.AR.ARTran.inventoryID),
      typeof (PX.Objects.AR.DiscountSequence.discountID),
      typeof (PX.Objects.AR.DiscountSequence.discountSequenceID)
    }))
    {
      foreach (PXResult<PX.Objects.AR.ARTran, DiscountItem, PX.Objects.AR.DiscountSequence> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) readOnly).Select(Array.Empty<object>()))
      {
        PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, DiscountItem, PX.Objects.AR.DiscountSequence>.op_Implicit(pxResult);
        PX.Objects.AR.DiscountSequence discountSequence = PXResult<PX.Objects.AR.ARTran, DiscountItem, PX.Objects.AR.DiscountSequence>.op_Implicit(pxResult);
        int? inventoryId = arTran.InventoryID;
        if (inventoryId.HasValue)
        {
          Dictionary<int, HashSet<DiscountSequenceKey>> dictionary1 = items;
          inventoryId = arTran.InventoryID;
          int key1 = inventoryId.Value;
          HashSet<DiscountSequenceKey> discountSequenceKeySet1;
          ref HashSet<DiscountSequenceKey> local = ref discountSequenceKeySet1;
          if (!dictionary1.TryGetValue(key1, out local))
          {
            Dictionary<int, HashSet<DiscountSequenceKey>> dictionary2 = items;
            inventoryId = arTran.InventoryID;
            int key2 = inventoryId.Value;
            HashSet<DiscountSequenceKey> discountSequenceKeySet2;
            discountSequenceKeySet1 = discountSequenceKeySet2 = new HashSet<DiscountSequenceKey>();
            dictionary2.Add(key2, discountSequenceKeySet2);
          }
          if (discountSequence.DiscountID != null && discountSequence.DiscountSequenceID != null)
            discountSequenceKeySet1.Add(new DiscountSequenceKey(discountSequence.DiscountID, discountSequence.DiscountSequenceID));
        }
      }
    }
    DiscountEngine.UpdateEntityCache();
    DiscountEngine.PutEntityDiscountsToSlot<DiscountItem, int>((ICollection<KeyValuePair<int, HashSet<DiscountSequenceKey>>>) items);
    this.prefetched.Add(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.DocType + ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Document).Current.RefNbr);
  }

  public class Alter_ARInvoiceEntryDocumentExtension : 
    PXGraphExtension<ARInvoiceEntry.ARInvoiceEntryDocumentExtension, ARInvoiceEntry>
  {
    [PXOverride]
    public void SuppressApproval(System.Action base_SuppressApproval)
    {
      base_SuppressApproval();
      if (!(((PXGraphExtension<ARInvoiceEntry>) this).Base is SOInvoiceEntry soInvoiceEntry))
        return;
      soInvoiceEntry.SOApproval.SuppressApproval = true;
    }
  }

  public delegate void InvoiceCreatedDelegate(PX.Objects.AR.ARInvoice invoice, InvoiceOrderArgs args);

  /// <exclude />
  public class SOInvoiceEntryAddressLookupExtension : 
    AddressLookupExtension<SOInvoiceEntry, PX.Objects.AR.ARInvoice, ARAddress>
  {
    protected override string AddressView => "Billing_Address";
  }

  /// <exclude />
  public class SOInvoiceEntryShippingAddressLookupExtension : 
    AddressLookupExtension<SOInvoiceEntry, PX.Objects.AR.ARInvoice, ARShippingAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }
}
