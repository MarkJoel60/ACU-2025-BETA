// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARInvoiceEntryExt.Intercompany
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARInvoiceEntryExt;

public class Intercompany : 
  PXGraphExtension<GenerateIntercompanyBillExtension, ARInvoiceEntry.CostAccrual, ARInvoiceEntry>
{
  private Dictionary<PX.Objects.AR.ARTran, IAPTranSource> _poSources;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [PXOverride]
  public virtual PX.Objects.AP.APTran GenerateIntercompanyAPTran(
    APInvoiceEntry apInvoiceEntryGraph,
    PX.Objects.AR.ARTran arTran,
    Func<APInvoiceEntry, PX.Objects.AR.ARTran, PX.Objects.AP.APTran> baseFunc)
  {
    PX.Objects.AP.APTran apTran1 = ((PXSelectBase<PX.Objects.AP.APTran>) apInvoiceEntryGraph.Transactions).Insert(new PX.Objects.AP.APTran()
    {
      ManualPrice = arTran.ManualPrice
    });
    IAPTranSource apTranSource = this.GetAPTranSource(arTran);
    apTranSource?.SetReferenceKeyTo(apTran1);
    this.Base2.SetAPTranFields(((PXSelectBase) apInvoiceEntryGraph.Transactions).Cache, apTran1, arTran);
    if (apTranSource != null)
    {
      apTran1.LineType = apTranSource.LineType;
      PX.Objects.AP.APTran apTran2 = apTran1;
      int? nullable1 = apTranSource.POAccrualAcctID;
      int? nullable2 = nullable1 ?? apTranSource.ExpenseAcctID;
      apTran2.AccountID = nullable2;
      apTran1 = ((PXSelectBase<PX.Objects.AP.APTran>) apInvoiceEntryGraph.Transactions).Update(apTran1);
      PX.Objects.AP.APTran apTran3 = apTran1;
      nullable1 = apTranSource.POAccrualSubID;
      int? nullable3 = nullable1 ?? apTranSource.ExpenseSubID;
      apTran3.SubID = nullable3;
      apTran1.SiteID = apTranSource.SiteID;
    }
    return ((PXSelectBase<PX.Objects.AP.APTran>) apInvoiceEntryGraph.Transactions).Update(apTran1);
  }

  [PXOverride]
  public virtual int? GetAPTranProjectID(
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters,
    PX.Objects.AR.ARInvoice arInvoice,
    PX.Objects.AR.ARTran arTran,
    Func<GenerateIntercompanyBillExtension.GenerateBillParameters, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, int?> baseFunc)
  {
    IAPTranSource apTranSource = this.GetAPTranSource(arTran);
    return apTranSource == null ? baseFunc(parameters, arInvoice, arTran) : apTranSource.ProjectID;
  }

  [PXOverride]
  public virtual int? GetAPTranTaskID(
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters,
    PX.Objects.AR.ARInvoice arInvoice,
    PX.Objects.AR.ARTran arTran,
    Func<GenerateIntercompanyBillExtension.GenerateBillParameters, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, int?> baseFunc)
  {
    IAPTranSource apTranSource = this.GetAPTranSource(arTran);
    return apTranSource == null ? baseFunc(parameters, arInvoice, arTran) : apTranSource.TaskID;
  }

  [PXOverride]
  public virtual int? GetAPTranCostCodeID(
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters,
    PX.Objects.AR.ARInvoice arInvoice,
    PX.Objects.AR.ARTran arTran,
    Func<GenerateIntercompanyBillExtension.GenerateBillParameters, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, int?> baseFunc)
  {
    IAPTranSource apTranSource = this.GetAPTranSource(arTran);
    return apTranSource == null ? baseFunc(parameters, arInvoice, arTran) : apTranSource.CostCodeID;
  }

  public virtual IAPTranSource GetAPTranSource(PX.Objects.AR.ARTran arTran)
  {
    IAPTranSource apTranSource = (IAPTranSource) null;
    Dictionary<PX.Objects.AR.ARTran, IAPTranSource> poSources = this._poSources;
    // ISSUE: explicit non-virtual call
    if ((poSources != null ? (__nonvirtual (poSources.TryGetValue(arTran, out apTranSource)) ? 1 : 0) : 0) != 0)
      return apTranSource;
    (PX.Objects.PO.POReceipt poReceipt, PX.Objects.PO.POReceiptLine poReceiptLine) = this.GetReceiptData(arTran);
    bool? nullable;
    if (poReceipt != null && poReceipt.POType == "DP")
    {
      nullable = poReceipt.IsUnderCorrection;
      if (nullable.GetValueOrDefault())
        throw new PXException("The AP bill cannot be created because {0} purchase receipt has the Under Correction status.", new object[1]
        {
          (object) poReceipt.ReceiptNbr
        });
    }
    if (poReceiptLine != null)
    {
      nullable = poReceiptLine.Released;
      if (nullable.GetValueOrDefault())
      {
        apTranSource = (IAPTranSource) POReceiptLineS.PK.Find((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, poReceiptLine.ReceiptType, poReceiptLine.ReceiptNbr, poReceiptLine.LineNbr);
        goto label_11;
      }
    }
    if (arTran.SOOrderLineOperation == "I")
    {
      PX.Objects.PO.POLine poLine = this.GetOrderData(arTran).Item2;
      if (poLine?.POAccrualType == "O")
        apTranSource = (IAPTranSource) POLineS.PK.Find((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, poLine.OrderType, poLine.OrderNbr, poLine.LineNbr);
    }
label_11:
    if (this._poSources == null)
      this._poSources = new Dictionary<PX.Objects.AR.ARTran, IAPTranSource>((IEqualityComparer<PX.Objects.AR.ARTran>) PXCacheEx.GetComparer(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache));
    this._poSources.Add(arTran, apTranSource);
    return apTranSource;
  }

  protected virtual (PX.Objects.PO.POOrder, PX.Objects.PO.POLine, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine) GetOrderData(
    PX.Objects.AR.ARTran arTran)
  {
    if (string.IsNullOrEmpty(arTran.SOOrderNbr))
      return ((PX.Objects.PO.POOrder) null, (PX.Objects.PO.POLine) null, (PX.Objects.SO.SOOrder) null, (PX.Objects.SO.SOLine) null);
    PXResult<PX.Objects.SO.SOLine> pxResult = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>, FbqlJoins.Left<PX.Objects.PO.POOrder>.On<PX.Objects.SO.SOOrder.FK.IntercompanyPOOrder>>, FbqlJoins.Left<PX.Objects.PO.POLine>.On<KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.orderType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POLine.orderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POLine>, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>.And<BqlOperand<PX.Objects.SO.SOLine.intercompanyPOLineNbr, IBqlInt>.IsEqual<PX.Objects.PO.POLine.lineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.orderType, Equal<BqlField<PX.Objects.AR.ARTran.sOOrderType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.orderNbr, Equal<BqlField<PX.Objects.AR.ARTran.sOOrderNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.SO.SOLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOOrderLineNbr, IBqlInt>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, (object[]) new PX.Objects.AR.ARTran[1]
    {
      arTran
    }, Array.Empty<object>()));
    return (PXResult.Unwrap<PX.Objects.PO.POOrder>((object) pxResult), PXResult.Unwrap<PX.Objects.PO.POLine>((object) pxResult), PXResult.Unwrap<PX.Objects.SO.SOOrder>((object) pxResult), PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult));
  }

  protected virtual (PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine) GetReceiptData(
    PX.Objects.AR.ARTran arTran)
  {
    if (string.IsNullOrEmpty(arTran.SOOrderNbr))
      return ((PX.Objects.PO.POReceipt) null, (PX.Objects.PO.POReceiptLine) null);
    PXResult pxResult;
    if (arTran.SOOrderLineOperation == "I")
      pxResult = (PXResult) PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<SOShipLine>.On<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, SOShipLine>, PX.Objects.SO.SOShipment, SOShipLine>.And<BqlOperand<SOShipLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOShipmentLineNbr, IBqlInt>.FromCurrent>>>>, FbqlJoins.Left<PX.Objects.PO.POReceipt>.On<KeysRelation<Field<PX.Objects.PO.POReceipt.intercompanyShipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>, PX.Objects.SO.SOShipment, PX.Objects.PO.POReceipt>.And<BqlOperand<PX.Objects.PO.POReceipt.canceled, IBqlBool>.IsEqual<False>>>>, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.And<BqlOperand<PX.Objects.PO.POReceiptLine.intercompanyShipmentLineNbr, IBqlInt>.IsEqual<SOShipLine.lineNbr>>>>>.Where<BqlOperand<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOShipmentNbr, IBqlString>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, (object[]) new PX.Objects.AR.ARTran[1]
      {
        arTran
      }, Array.Empty<object>()));
    else
      pxResult = (PXResult) PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>, FbqlJoins.Left<PX.Objects.PO.POReceipt>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreturn>>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrder.intercompanyPOReturnNbr>>>>, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<PX.Objects.SO.SOLine.intercompanyPOLineNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.orderType, Equal<BqlField<PX.Objects.AR.ARTran.sOOrderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLine.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOOrderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.AR.ARTran.sOOrderLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, (object[]) new PX.Objects.AR.ARTran[1]
      {
        arTran
      }, Array.Empty<object>()));
    return (PXResult.Unwrap<PX.Objects.PO.POReceipt>((object) pxResult), PXResult.Unwrap<PX.Objects.PO.POReceiptLine>((object) pxResult));
  }

  protected virtual void ARTran_ExpenseAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    PXFieldDefaulting baseFunc)
  {
    PX.Objects.AR.ARTran row = (PX.Objects.AR.ARTran) e.Row;
    if (row != null)
    {
      bool? nullable = row.IsStockItem;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.AccrueCost;
        if (nullable.GetValueOrDefault())
        {
          PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Current;
          int num;
          if (current == null)
          {
            num = 0;
          }
          else
          {
            nullable = current.IsBranch;
            num = nullable.GetValueOrDefault() ? 1 : 0;
          }
          if (num != 0 && row.SOOrderNbr != null)
          {
            PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find(sender.Graph, row.SOOrderType);
            if (soOrderType == null)
              return;
            switch (soOrderType.IntercompanyCOGSAcctDefault)
            {
              case "I":
                PX.Objects.IN.InventoryItem data = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, row.InventoryID);
                if (data == null)
                  return;
                e.NewValue = ((PXGraphExtension<ARInvoiceEntry>) this).Base.GetValue<PX.Objects.IN.InventoryItem.cOGSAcctID>((object) data);
                ((CancelEventArgs) e).Cancel = true;
                return;
              case "L":
                if (((PXSelectBase<PX.Objects.AR.Customer>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Current == null)
                  return;
                e.NewValue = ((PXGraphExtension<ARInvoiceEntry>) this).Base.GetValue<PX.Objects.AR.Customer.cOGSAcctID>((object) ((PXSelectBase<PX.Objects.AR.Customer>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.customer).Current);
                ((CancelEventArgs) e).Cancel = true;
                return;
              default:
                return;
            }
          }
        }
      }
    }
    baseFunc.Invoke(sender, e);
  }
}
