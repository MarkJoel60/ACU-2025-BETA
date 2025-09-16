// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashTransferEntryTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.Extensions.SalesTax;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CashTransferEntryTax : TaxGraph<CashTransferEntry, CATransfer>
{
  protected Type DocumentTypeCuryDocBal = typeof (CAExpense.curyTranAmt);
  public PXAction<CATransfer> ViewExpenseTaxes;

  protected override PXView DocumentDetailsView => ((PXSelectBase) this.Base.Expenses).View;

  protected IEnumerable details()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    CashTransferEntryTax transferEntryTax = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) ((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) transferEntryTax.Documents).Current;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  protected override TaxBaseGraph<CashTransferEntry, CATransfer>.DocumentMapping GetDocumentMapping()
  {
    return new TaxBaseGraph<CashTransferEntry, CATransfer>.DocumentMapping(typeof (CAExpense))
    {
      DocumentDate = typeof (CAExpense.tranDate),
      CuryDocBal = typeof (CAExpense.curyTranAmt),
      CuryTaxTotal = typeof (CAExpense.curyTaxTotal),
      CuryLinetotal = typeof (CAExpense.curyTaxableAmt),
      BranchID = typeof (CAExpense.branchID),
      CuryID = typeof (CAExpense.curyID),
      CuryInfoID = typeof (CAExpense.curyInfoID),
      CuryTaxRoundDiff = typeof (CAExpense.curyTaxRoundDiff),
      TaxRoundDiff = typeof (CAExpense.taxRoundDiff),
      TaxZoneID = typeof (CAExpense.taxZoneID),
      FinPeriodID = typeof (CAExpense.finPeriodID),
      TaxCalcMode = typeof (CAExpense.taxCalcMode),
      CuryExtPriceTotal = typeof (CAExpense.curyTaxableAmt),
      CuryOrigWhTaxAmt = typeof (CAExpense.curyTaxAmt)
    };
  }

  protected override TaxBaseGraph<CashTransferEntry, CATransfer>.DetailMapping GetDetailMapping()
  {
    return new TaxBaseGraph<CashTransferEntry, CATransfer>.DetailMapping(typeof (CAExpense))
    {
      CuryTranAmt = typeof (CAExpense.curyTaxableAmt),
      CuryTranExtPrice = typeof (CAExpense.curyTaxableAmt),
      CuryInfoID = typeof (CAExpense.curyInfoID),
      TaxCategoryID = typeof (CAExpense.taxCategoryID)
    };
  }

  protected override TaxBaseGraph<CashTransferEntry, CATransfer>.TaxDetailMapping GetTaxDetailMapping()
  {
    return new TaxBaseGraph<CashTransferEntry, CATransfer>.TaxDetailMapping(typeof (CAExpenseTax), typeof (CAExpenseTax.taxID));
  }

  protected override TaxBaseGraph<CashTransferEntry, CATransfer>.TaxTotalMapping GetTaxTotalMapping()
  {
    return new TaxBaseGraph<CashTransferEntry, CATransfer>.TaxTotalMapping(typeof (CAExpenseTaxTran), typeof (CAExpenseTaxTran.taxID));
  }

  protected override bool AskConfirmationToRecalculateExtCost => false;

  protected override PXResultset<Detail> SelectDetails()
  {
    PXResultset<Detail> pxResultset = new PXResultset<Detail>();
    pxResultset.Add(new PXResult<Detail>(((PXSelectBase) this.Documents).Cache.GetExtension<Detail>((object) ((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current)));
    return pxResultset;
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> dictionary = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
    object[] objArray = new object[1]{ row };
    foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, Or<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolTrue>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>>>>, And<Current<CAExpense.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
    {
      PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult));
      dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = new PXResult<PX.Objects.TX.Tax, TaxRev>(tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult));
    }
    List<object> objectList = new List<object>();
    switch (taxchk)
    {
      case PXTaxCheck.Line:
      case PXTaxCheck.RecalcLine:
        using (List<object>.Enumerator enumerator = ((PXSelectBase) new PXSelect<CAExpenseTax, Where<CAExpenseTax.refNbr, Equal<Current<CAExpense.refNbr>>, And<CAExpenseTax.lineNbr, Equal<Current<CAExpense.lineNbr>>>>>(graph)).View.SelectMultiBound(objArray, Array.Empty<object>()).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            CAExpenseTax current = (CAExpenseTax) enumerator.Current;
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult;
            if (dictionary.TryGetValue(current.TaxID, out pxResult))
            {
              int count = objectList.Count;
              while (count > 0 && string.Compare(PXResult<CAExpenseTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<CAExpenseTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]).TaxCalcLevel, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxCalcLevel) > 0)
                --count;
              objectList.Insert(count, (object) new PXResult<CAExpenseTax, PX.Objects.TX.Tax, TaxRev>(current, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult)));
            }
          }
          break;
        }
      case PXTaxCheck.RecalcTotals:
        using (List<object>.Enumerator enumerator = ((PXSelectBase) new PXSelect<CAExpenseTaxTran, Where<CAExpenseTaxTran.refNbr, Equal<Current<CAExpense.refNbr>>, And<CAExpenseTaxTran.lineNbr, Equal<Current<CAExpense.lineNbr>>>>>(graph)).View.SelectMultiBound(objArray, Array.Empty<object>()).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            CAExpenseTaxTran current = (CAExpenseTaxTran) enumerator.Current;
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult;
            if (!string.IsNullOrEmpty(current.TaxID) && dictionary.TryGetValue(current.TaxID, out pxResult))
            {
              int count = objectList.Count;
              while (count > 0 && string.Compare(PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]).TaxCalcLevel, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxCalcLevel) > 0)
                --count;
              objectList.Insert(count, (object) new PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax, TaxRev>(current, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult)));
            }
          }
          break;
        }
    }
    return objectList;
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return GraphHelper.RowCast<CAExpense>((IEnumerable) PXSelectBase<CAExpense, PXSelect<CAExpense, Where<CAExpense.refNbr, Equal<Current<CAExpense.refNbr>>, And<CAExpense.lineNbr, Equal<Current<CAExpense.lineNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>())).Select<CAExpense, object>((Func<CAExpense, object>) (_ => (object) _)).ToList<object>();
  }

  protected override List<object> ChildSelect(PXCache cache, object data)
  {
    return new List<object>() { cache.Current };
  }

  protected virtual string GetRefNbr(PXCache sender, object row)
  {
    return (string) sender.GetValue<CAExpense.refNbr>(row);
  }

  protected virtual int? GetLineNbr(PXCache sender, object row)
  {
    return (int?) sender.GetValue<CAExpense.lineNbr>(row);
  }

  protected virtual string GetEntryTypeID(PXCache sender, object row)
  {
    return (string) sender.GetValue<CAExpense.entryTypeID>(row);
  }

  protected virtual string GetTaxZoneLocal(PXCache sender, object row)
  {
    return (string) sender.GetValue<CAExpense.taxZoneID>(row);
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    return ((PXFieldState) sender.GetValueExt<CAExpense.curyTranAmt>(row)).DisplayName;
  }

  protected override Decimal? GetCuryTranAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<CAExpense.curyTranAmt>(row);
  }

  protected override void CalcDocTotals(
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal)
  {
    Decimal curyLineTotal = 0M;
    Decimal discountLineTotal = 0M;
    Decimal tranExtPriceTotal = 0M;
    this.CalcSingleLineTotals(row, ((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current, out curyLineTotal, out discountLineTotal, out tranExtPriceTotal);
    Decimal objA = curyLineTotal + CuryTaxTotal - CuryInclTaxTotal;
    Decimal valueOrDefault1 = this.CurrentDocument.CuryLineTotal.GetValueOrDefault();
    Decimal valueOrDefault2 = this.CurrentDocument.CuryTaxTotal.GetValueOrDefault();
    if (!object.Equals((object) curyLineTotal, (object) valueOrDefault1) || !object.Equals((object) CuryTaxTotal, (object) valueOrDefault2))
    {
      this.ParentSetValue<PX.Objects.Extensions.SalesTax.Document.curyLineTotal>((object) curyLineTotal);
      this.ParentSetValue<PX.Objects.Extensions.SalesTax.Document.curyDiscountLineTotal>((object) discountLineTotal);
      this.ParentSetValue<PX.Objects.Extensions.SalesTax.Document.curyExtPriceTotal>((object) tranExtPriceTotal);
      this.ParentSetValue<PX.Objects.Extensions.SalesTax.Document.curyTaxTotal>((object) CuryTaxTotal);
      if (this.GetDocumentMapping().CuryDocBal != typeof (PX.Objects.Extensions.SalesTax.Document.curyDocBal))
      {
        this.ParentSetValue<PX.Objects.Extensions.SalesTax.Document.curyDocBal>((object) objA);
        return;
      }
    }
    if (!(this.GetDocumentMapping().CuryDocBal != typeof (PX.Objects.Extensions.SalesTax.Document.curyDocBal)))
      return;
    Decimal valueOrDefault3 = this.CurrentDocument.CuryDocBal.GetValueOrDefault();
    if (object.Equals((object) objA, (object) valueOrDefault3))
      return;
    this.ParentSetValue<PX.Objects.Extensions.SalesTax.Document.curyDocBal>((object) objA);
  }

  protected virtual void CalcSingleLineTotals(
    object row,
    PX.Objects.Extensions.SalesTax.Document document,
    out Decimal curyLineTotal,
    out Decimal discountLineTotal,
    out Decimal tranExtPriceTotal)
  {
    Detail extension = ((PXSelectBase) this.Details).Cache.GetExtension<Detail>((object) document);
    curyLineTotal = ((Decimal?) extension?.CuryTranAmt).GetValueOrDefault();
    discountLineTotal = ((Decimal?) extension?.CuryTranDiscount).GetValueOrDefault();
    tranExtPriceTotal = ((Decimal?) extension?.CuryTranExtPrice).GetValueOrDefault();
  }

  public override void Detail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CAExpense row = (CAExpense) this.CurrentDocument.Base;
    // ISSUE: explicit non-virtual call
    if ((row != null ? (!__nonvirtual (row.CashAccountID).HasValue ? 1 : 0) : 1) != 0 || row.EntryTypeID == null)
      return;
    if (!object.Equals((object) this.GetTaxCategory(sender, e.OldRow), (object) this.GetTaxCategory(sender, e.Row)) || !object.Equals((object) this.GetCuryTranAmt(sender, e.OldRow), (object) this.GetCuryTranAmt(sender, e.Row)) || !object.Equals((object) this.GetTaxZoneLocal(sender, e.OldRow), (object) this.GetTaxZoneLocal(sender, e.Row)) || !object.Equals((object) this.GetRefNbr(sender, e.OldRow), (object) this.GetRefNbr(sender, e.Row)) || !object.Equals((object) this.GetLineNbr(sender, e.OldRow), (object) this.GetLineNbr(sender, e.Row)) || !object.Equals((object) this.GetEntryTypeID(sender, e.OldRow), (object) this.GetEntryTypeID(sender, e.Row)))
    {
      PXCache cach = sender.Graph.Caches[typeof (CAExpense)];
      this.Preload();
      this.ReDefaultTaxes(this.SelectTheDetail(row));
      this._ParentRow = sender.GetExtension<PX.Objects.Extensions.SalesTax.Document>(e.Row);
      this.CalcTaxes((object) null);
      this._ParentRow = (PX.Objects.Extensions.SalesTax.Document) null;
    }
    base.Detail_RowUpdated(sender, e);
  }

  /// <summary>
  /// The method converts the current row to <see cref="T:PX.Data.PXResultset`1" />.
  /// The method replaces the base Details.Select() call to avoid the selection of all details according to the feature that the expense is a document and line 2-in-1.
  /// </summary>
  private PXResultset<Detail> SelectTheDetail(CAExpense row)
  {
    Detail extension = ((PXSelectBase) this.Details).Cache.GetExtension<Detail>((object) row);
    PXResultset<Detail> pxResultset = new PXResultset<Detail>();
    pxResultset.Add(new PXResult<Detail>(extension));
    return pxResultset;
  }

  public override void Detail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this.CurrentDocument == null)
      return;
    base.Detail_RowDeleted(sender, e);
  }

  protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
  {
    if (!(child is CAExpense caExpense))
      return;
    caExpense.CuryTaxableAmt = value;
    sender.Update((object) caExpense);
  }

  protected override void DefaultTaxes(Detail row, bool DefaultExisting)
  {
    if (this.CurrentDocument == null)
      return;
    base.DefaultTaxes(row, DefaultExisting);
  }

  protected virtual void CAExpenseTaxTran_TaxType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || ((PXSelectBase<CAExpense>) this.Base.Expenses).Current == null)
      return;
    if (((PXSelectBase<CAExpense>) this.Base.Expenses).Current.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((PX.Objects.TX.TaxDetail) e.Row).TaxID
      }));
      if (purchaseTax == null)
        return;
      e.NewValue = (object) purchaseTax.TranTaxType;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((PX.Objects.TX.TaxDetail) e.Row).TaxID
      }));
      if (salesTax == null)
        return;
      e.NewValue = (object) salesTax.TranTaxType;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CAExpenseTaxTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || ((PXSelectBase<CAExpense>) this.Base.Expenses).Current == null)
      return;
    if (((PXSelectBase<CAExpense>) this.Base.Expenses).Current.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((PX.Objects.TX.TaxDetail) e.Row).TaxID
      }));
      if (purchaseTax == null)
        return;
      e.NewValue = (object) purchaseTax.HistTaxAcctID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((PX.Objects.TX.TaxDetail) e.Row).TaxID
      }));
      if (salesTax == null)
        return;
      e.NewValue = (object) salesTax.HistTaxAcctID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CAExpenseTaxTran_SubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || ((PXSelectBase<CAExpense>) this.Base.Expenses).Current == null)
      return;
    if (((PXSelectBase<CAExpense>) this.Base.Expenses).Current.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((PX.Objects.TX.TaxDetail) e.Row).TaxID
      }));
      if (purchaseTax == null)
        return;
      e.NewValue = (object) purchaseTax.HistTaxSubID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((PX.Objects.TX.TaxDetail) e.Row).TaxID
      }));
      if (salesTax == null)
        return;
      e.NewValue = (object) salesTax.HistTaxSubID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewExpenseTaxes(PXAdapter adapter)
  {
    ((PXSelectBase<CAExpenseTaxTran>) this.Base.ExpenseTaxTrans).AskExt(true);
    return adapter.Get();
  }
}
