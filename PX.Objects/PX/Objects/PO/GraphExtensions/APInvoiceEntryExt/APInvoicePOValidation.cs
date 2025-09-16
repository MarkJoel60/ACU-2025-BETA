// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceEntryExt.APInvoicePOValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceEntryExt;

public class APInvoicePOValidation : PXGraphExtension<APInvoiceEntry>
{
  private APInvoicePOValidationService _validationService;
  public PXSelectReadonly2<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>> POLineQuery;
  public PXSelectReadonly<POLineBillingRevision, Where<POLineBillingRevision.apDocType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<POLineBillingRevision.apRefNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<POLineBillingRevision.orderType, Equal<Required<PX.Objects.AP.APTran.pOOrderType>>, And<POLineBillingRevision.orderNbr, Equal<Required<PX.Objects.AP.APTran.pONbr>>, And<POLineBillingRevision.orderLineNbr, Equal<Required<PX.Objects.AP.APTran.pOLineNbr>>>>>>>> POLineRevisionQuery;
  protected string _prefetchedDoc;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual APInvoicePOValidationService GetValidationService(Lazy<POSetup> poSetup)
  {
    if (this._validationService == null)
      this._validationService = new APInvoicePOValidationService(poSetup);
    return this._validationService;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AP.APTran> e)
  {
    this.Validate(e.Row, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AP.APTran>>) e).Cache);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APTran> e)
  {
    this.Validate(e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache);
  }

  protected virtual void Validate(PX.Objects.AP.APTran tran, PXCache cache)
  {
    APInvoicePOValidationService validationService = this.GetValidationService(Lazy.By<POSetup>((Func<POSetup>) (() => ((PXSelectBase<POSetup>) this.Base.posetup).Current)));
    if (!validationService.IsLineValidationRequired(tran) || ((PXGraph) this.Base).IsImport || ((PXGraph) this.Base).IsExport)
      return;
    this.Prefetch(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current);
    APInvoicePOValidation.POLineDTO poLine = this.ReadLinkedPOLine(tran);
    if (poLine == null)
      return;
    bool flag1 = false;
    string message;
    if (tran.LineType != "FT")
    {
      Decimal? receivedQty = poLine.ReceivedQty;
      Decimal? orderQty = poLine.OrderQty;
      Decimal? rcptQtyMax = poLine.RcptQtyMax;
      Decimal? nullable = orderQty.HasValue & rcptQtyMax.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * rcptQtyMax.GetValueOrDefault() / 100M) : new Decimal?();
      bool flag2 = receivedQty.GetValueOrDefault() > nullable.GetValueOrDefault() & receivedQty.HasValue & nullable.HasValue;
      message = poLine.OrderType == "RS" ? (flag2 ? "Related subcontract line:~Unbilled Quantity: {2} {0}, Unit Cost: {3} {1} per {0}, Unbilled Amount: {4} {1}.~Received quantity exceeds the ordered quantity." : "Related subcontract line:~Unbilled Quantity: {2} {0}, Unit Cost: {3} {1} per {0}, Unbilled Amount: {4} {1}.") : (flag2 ? "Related purchase order line:~Unbilled Quantity: {2} {0}, Unit Cost: {3} {1} per {0}, Unbilled Amount: {4} {1}.~Received quantity exceeds the ordered quantity." : "Related purchase order line:~Unbilled Quantity: {2} {0}, Unit Cost: {3} {1} per {0}, Unbilled Amount: {4} {1}.");
      bool flag3 = PXUIFieldAttribute.GetErrorOnly<PX.Objects.AP.APTran.qty>(cache, (object) tran) != null;
      if (validationService.IsAPTranQtyExceedsPOLineUnbilledQty(tran, poLine) && !flag3)
      {
        this.ShowWarningWithErrorFallback<PX.Objects.AP.APTran.qty>(cache, message, tran, poLine);
        flag1 = true;
      }
      else if (!validationService.IsAPTranQtyExceedsPOLineUnbilledQty(tran, poLine) && !flag3)
        PXUIFieldAttribute.SetWarning<PX.Objects.AP.APTran.qty>(cache, (object) tran, (string) null);
      bool flag4 = PXUIFieldAttribute.GetErrorOnly<PX.Objects.AP.APTran.curyUnitCost>(cache, (object) tran) != null;
      try
      {
        if (validationService.IsAPTranUnitCostExceedsPOLineUnitCost(cache, tran, ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.CuryID, poLine) && !flag4)
        {
          this.ShowWarningWithErrorFallback<PX.Objects.AP.APTran.curyUnitCost>(cache, message, tran, poLine);
          flag1 = true;
        }
        else if (!validationService.IsAPTranUnitCostExceedsPOLineUnitCost(cache, tran, ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.CuryID, poLine))
        {
          if (!flag4)
            PXUIFieldAttribute.SetWarning<PX.Objects.AP.APTran.curyUnitCost>(cache, (object) tran, (string) null);
        }
      }
      catch (Exception ex)
      {
        PXUIFieldAttribute.SetError<PX.Objects.AP.APTran.uOM>(cache, (object) tran, ex.Message);
      }
    }
    else
      message = "Related purchase order line:~Unbilled Amount: {4} {1}.";
    bool flag5 = PXUIFieldAttribute.GetErrorOnly<PX.Objects.AP.APTran.curyTranAmt>(cache, (object) tran) != null;
    if (validationService.IsAPTranAmountExceedsPOLineUnbilledAmount(tran, ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.CuryID, poLine) && !flag5)
    {
      this.ShowWarningWithErrorFallback<PX.Objects.AP.APTran.curyTranAmt>(cache, message, tran, poLine);
      flag1 = true;
    }
    else if (!validationService.IsAPTranAmountExceedsPOLineUnbilledAmount(tran, ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.CuryID, poLine) && !flag5)
      PXUIFieldAttribute.SetWarning<PX.Objects.AP.APTran.curyTranAmt>(cache, (object) tran, (string) null);
    if (flag1)
    {
      string str = poLine.OrderType == "RS" ? "Some of the bill lines differ from the corresponding lines of the related subcontract. For details, review warnings in the lines." : "Some of the bill lines differ from the corresponding lines of the related purchase order. For details, review warnings in the lines.";
      cache.RaiseExceptionHandling<PX.Objects.AP.APTran.inventoryID>((object) tran, (object) tran.InventoryID, (Exception) new PXSetPropertyKeepPreviousException(str, (PXErrorLevel) 3));
    }
    else
      PXUIFieldAttribute.SetWarning<PX.Objects.AP.APTran.inventoryID>(cache, (object) tran, (string) null);
  }

  protected virtual Type[] GetPOLinesQueryFieldScope()
  {
    return new Type[2]
    {
      typeof (PX.Objects.PO.POLine),
      typeof (PX.Objects.PO.POOrder.curyID)
    };
  }

  protected virtual void Prefetch(PX.Objects.AP.APInvoice doc)
  {
    string a = $"{doc.DocType}-{doc.RefNbr}-{doc.Released}";
    if (string.Equals(a, this._prefetchedDoc, StringComparison.Ordinal))
      return;
    if (!doc.Released.GetValueOrDefault())
    {
      PXSelectReadonly2<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>, InnerJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.pOOrderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.AP.APTran.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.AP.APTran.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>>>, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>> pxSelectReadonly2 = new PXSelectReadonly2<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>, InnerJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.pOOrderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.AP.APTran.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.AP.APTran.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>>>, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>((PXGraph) this.Base);
      using (new PXReadBranchRestrictedScope())
      {
        using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, this.GetPOLinesQueryFieldScope()))
        {
          PXView view = ((PXSelectBase) pxSelectReadonly2).View;
          object[] objArray = new object[2]
          {
            (object) doc.DocType,
            (object) doc.RefNbr
          };
          foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran> pxResult in view.SelectMulti(objArray))
          {
            PX.Objects.PO.POLine poLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran>.op_Implicit(pxResult);
            PXSelectReadonly2<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>> poLineQuery = this.POLineQuery;
            List<object> objectList = new List<object>(1);
            objectList.Add((object) new PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>(poLine, PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.AP.APTran>.op_Implicit(pxResult)));
            PXQueryParameters fromRecord = PXQueryParameters.ExtractFromRecord((IBqlTable) poLine);
            ((PXSelectBase<PX.Objects.PO.POLine>) poLineQuery).StoreResult(objectList, fromRecord);
          }
        }
      }
    }
    else
    {
      foreach (PXResult<PX.Objects.AP.APTran, POLineBillingRevision> pxResult in PXSelectBase<PX.Objects.AP.APTran, PXSelectReadonly2<PX.Objects.AP.APTran, LeftJoin<POLineBillingRevision, On<POLineBillingRevision.apDocType, Equal<PX.Objects.AP.APTran.tranType>, And<POLineBillingRevision.apRefNbr, Equal<PX.Objects.AP.APTran.refNbr>, And<POLineBillingRevision.orderType, Equal<PX.Objects.AP.APTran.pOOrderType>, And<POLineBillingRevision.orderNbr, Equal<PX.Objects.AP.APTran.pONbr>, And<POLineBillingRevision.orderLineNbr, Equal<PX.Objects.AP.APTran.pOLineNbr>>>>>>>, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>, And<PX.Objects.AP.APTran.pONbr, IsNotNull>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }))
      {
        PX.Objects.AP.APTran apTran = PXResult<PX.Objects.AP.APTran, POLineBillingRevision>.op_Implicit(pxResult);
        POLineBillingRevision lineBillingRevision = PXResult<PX.Objects.AP.APTran, POLineBillingRevision>.op_Implicit(pxResult);
        PXSelectReadonly<POLineBillingRevision, Where<POLineBillingRevision.apDocType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<POLineBillingRevision.apRefNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<POLineBillingRevision.orderType, Equal<Required<PX.Objects.AP.APTran.pOOrderType>>, And<POLineBillingRevision.orderNbr, Equal<Required<PX.Objects.AP.APTran.pONbr>>, And<POLineBillingRevision.orderLineNbr, Equal<Required<PX.Objects.AP.APTran.pOLineNbr>>>>>>>> lineRevisionQuery = this.POLineRevisionQuery;
        List<object> objectList = new List<object>(1);
        objectList.Add(lineBillingRevision.OrderNbr != null ? (object) new PXResult<POLineBillingRevision>(lineBillingRevision) : (object) (PXResult<POLineBillingRevision>) null);
        PXQueryParameters pxQueryParameters = PXQueryParameters.ExplicitParameters(new object[5]
        {
          (object) apTran.TranType,
          (object) apTran.RefNbr,
          (object) apTran.POOrderType,
          (object) apTran.PONbr,
          (object) apTran.POLineNbr
        });
        ((PXSelectBase<POLineBillingRevision>) lineRevisionQuery).StoreResult(objectList, pxQueryParameters);
      }
    }
    this._prefetchedDoc = a;
  }

  public virtual APInvoicePOValidation.POLineDTO ReadLinkedPOLine(PX.Objects.AP.APTran tran)
  {
    if (!tran.Released.GetValueOrDefault())
    {
      using (new PXReadBranchRestrictedScope())
      {
        using (new PXFieldScope(((PXSelectBase) this.POLineQuery).View, this.GetPOLinesQueryFieldScope()))
        {
          PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder> pxResult = (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>) ((PXSelectBase) this.POLineQuery).View.SelectSingle(new object[3]
          {
            (object) tran.POOrderType,
            (object) tran.PONbr,
            (object) tran.POLineNbr
          });
          return pxResult == null ? (APInvoicePOValidation.POLineDTO) null : new APInvoicePOValidation.POLineDTO(PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), ((PXResult) pxResult).GetItem<PX.Objects.PO.POOrder>().CuryID);
        }
      }
    }
    POLineBillingRevision poLineRevision = PXResultset<POLineBillingRevision>.op_Implicit(((PXSelectBase<POLineBillingRevision>) this.POLineRevisionQuery).SelectWindowed(0, 1, new object[5]
    {
      (object) tran.TranType,
      (object) tran.RefNbr,
      (object) tran.POOrderType,
      (object) tran.PONbr,
      (object) tran.POLineNbr
    }));
    return poLineRevision == null ? (APInvoicePOValidation.POLineDTO) null : new APInvoicePOValidation.POLineDTO(poLineRevision);
  }

  public virtual void ShowWarningWithErrorFallback<TField>(
    PXCache cache,
    string message,
    PX.Objects.AP.APTran tran,
    APInvoicePOValidation.POLineDTO poLine)
    where TField : IBqlField
  {
    Decimal num1 = PXDBQuantityAttribute.Round(poLine.UnbilledQty);
    Decimal num2 = PXDBPriceCostAttribute.Round(poLine.CuryUnitCost.GetValueOrDefault());
    Decimal num3 = PXDBCurrencyAttribute.BaseRound((PXGraph) this.Base, poLine.CuryUnbilledAmt.GetValueOrDefault());
    if (cache.RaiseExceptionHandling<TField>((object) tran, cache.GetValueExt<TField>((object) tran), (Exception) new PXSetPropertyException<TField>(message, (PXErrorLevel) 2, new object[5]
    {
      (object) poLine.UOM,
      (object) poLine.CuryID,
      (object) num1,
      (object) num2,
      (object) num3
    })))
      throw new PXSetPropertyException<TField>(message, new object[5]
      {
        (object) poLine.UOM,
        (object) poLine.CuryID,
        (object) num1,
        (object) num2,
        (object) num3
      });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AP.APTran> e)
  {
    PX.Objects.AP.APTran row = e.Row;
    if (row == null || row.Released.GetValueOrDefault() || EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    string lineType = (string) null;
    Decimal? nullable1 = row.Qty;
    Decimal? nullable2;
    switch (row.POAccrualType)
    {
      case "R":
        PX.Objects.PO.POReceiptLine poReceiptLine = PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) this.Base, row.ReceiptType, row.ReceiptNbr, row.ReceiptLineNbr);
        lineType = poReceiptLine.LineType;
        Decimal? nullable3 = poReceiptLine.UnbilledQty;
        if (row.UOM != poReceiptLine.UOM)
        {
          nullable1 = row.BaseQty;
          nullable3 = poReceiptLine.BaseUnbilledQty;
        }
        nullable2 = nullable1;
        Decimal? nullable4 = nullable3;
        if (nullable2.GetValueOrDefault() > nullable4.GetValueOrDefault() & nullable2.HasValue & nullable4.HasValue)
        {
          Decimal sign = row.Sign;
          short? invtMult = poReceiptLine.InvtMult;
          int? nullable5 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          int num1 = 0;
          Decimal num2 = nullable5.GetValueOrDefault() < num1 & nullable5.HasValue ? -1M : 1M;
          if (sign == num2 && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.qty>((object) row, (object) row.Qty, (Exception) new PXSetPropertyException("Quantity billed is greater than the quantity in the original PO Receipt for this row", (PXErrorLevel) 4)))
            throw new PXSetPropertyException("Quantity billed is greater than the quantity in the original PO Receipt for this row");
          break;
        }
        break;
      case "O":
        PX.Objects.PO.POLine poLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXSelectReadonly<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[3]
        {
          (object) row.POOrderType,
          (object) row.PONbr,
          (object) row.POLineNbr
        }));
        lineType = poLine.LineType;
        if (!(row.Sign < 0M))
        {
          Decimal? nullable6 = poLine.BilledQty;
          Decimal? orderQty = poLine.OrderQty;
          nullable2 = poLine.RcptQtyMax;
          Decimal? nullable7 = orderQty.HasValue & nullable2.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * nullable2.GetValueOrDefault() / 100M) : new Decimal?();
          Decimal? nullable8;
          if (row.UOM != poLine.UOM)
          {
            nullable1 = row.BaseQty;
            nullable6 = poLine.BaseBilledQty;
            nullable2 = poLine.BaseOrderQty;
            nullable8 = poLine.RcptQtyMax;
            nullable7 = nullable2.HasValue & nullable8.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable8.GetValueOrDefault() / 100M) : new Decimal?();
          }
          if (EnumerableExtensions.IsIn<string>(poLine.RcptQtyAction, "R", "W"))
          {
            Decimal? nullable9 = nullable1;
            Decimal? nullable10 = nullable6;
            nullable8 = nullable9.HasValue & nullable10.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new Decimal?();
            nullable2 = nullable7;
            if (nullable8.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable8.HasValue & nullable2.HasValue)
            {
              PXErrorLevel pxErrorLevel = poLine.RcptQtyAction == "R" ? (PXErrorLevel) 4 : (PXErrorLevel) 2;
              if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.qty>((object) row, (object) row.Qty, (Exception) new PXSetPropertyException("The billed quantity is greater than the quantity in the original purchase order for this row.", pxErrorLevel)) && pxErrorLevel == 4)
                throw new PXSetPropertyException("The billed quantity is greater than the quantity in the original purchase order for this row.");
              break;
            }
            break;
          }
          break;
        }
        break;
    }
    nullable2 = row.Qty;
    Decimal num3 = 0M;
    if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
      return;
    nullable2 = row.CuryTranAmt;
    Decimal num4 = 0M;
    if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue || !POLineType.UsePOAccrual(lineType))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APTran>>) e).Cache.RaiseExceptionHandling<PX.Objects.AP.APTran.qty>((object) row, (object) row.Qty, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must not be equal to {0}.", (PXErrorLevel) 4, new object[1]
    {
      (object) 0M
    }));
  }

  public class POLineDTO
  {
    protected PX.Objects.PO.POLine _poLine;
    protected string _poLineCuryID;
    protected POLineBillingRevision _poLineRevision;

    public virtual string OrderType
    {
      get => this._poLineRevision != null ? this._poLineRevision.OrderType : this._poLine.OrderType;
    }

    public virtual string OrderNbr
    {
      get => this._poLineRevision != null ? this._poLineRevision.OrderNbr : this._poLine.OrderNbr;
    }

    public virtual int? OrderLineNbr
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.OrderLineNbr : this._poLine.LineNbr;
      }
    }

    public virtual string CuryID
    {
      get => this._poLineRevision != null ? this._poLineRevision.CuryID : this._poLineCuryID;
    }

    public virtual string UOM
    {
      get => this._poLineRevision != null ? this._poLineRevision.UOM : this._poLine.UOM;
    }

    public virtual int? InventoryID
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.InventoryID : this._poLine.InventoryID;
      }
    }

    public virtual Decimal? OrderQty
    {
      get => this._poLineRevision != null ? this._poLineRevision.OrderQty : this._poLine.OrderQty;
    }

    public virtual Decimal? BaseOrderQty
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.BaseOrderQty : this._poLine.BaseOrderQty;
      }
    }

    public virtual Decimal? ReceivedQty
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.ReceivedQty : this._poLine.ReceivedQty;
      }
    }

    public virtual Decimal? BaseReceivedQty
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.BaseReceivedQty : this._poLine.BaseReceivedQty;
      }
    }

    public virtual Decimal? RcptQtyMax
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.RcptQtyMax : this._poLine.RcptQtyMax;
      }
    }

    public virtual Decimal? UnbilledQty
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.UnbilledQty : this._poLine.UnbilledQty;
      }
    }

    public virtual Decimal? BaseUnbilledQty
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.BaseUnbilledQty : this._poLine.BaseUnbilledQty;
      }
    }

    public virtual Decimal? CuryUnbilledAmt
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.CuryUnbilledAmt : this._poLine.CuryUnbilledAmt;
      }
    }

    public virtual Decimal? UnbilledAmt
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.UnbilledAmt : this._poLine.UnbilledAmt;
      }
    }

    public virtual Decimal? CuryUnitCost
    {
      get
      {
        return this._poLineRevision != null ? this._poLineRevision.CuryUnitCost : this._poLine.CuryUnitCost;
      }
    }

    public virtual Decimal? UnitCost
    {
      get => this._poLineRevision != null ? this._poLineRevision.UnitCost : this._poLine.UnitCost;
    }

    public virtual string LineType
    {
      get => this._poLineRevision != null ? this._poLineRevision.LineType : this._poLine.LineType;
    }

    public POLineDTO(PX.Objects.PO.POLine poLine, string curyID)
    {
      this._poLine = poLine;
      this._poLineCuryID = curyID;
    }

    public POLineDTO(POLineBillingRevision poLineRevision) => this._poLineRevision = poLineRevision;
  }
}
