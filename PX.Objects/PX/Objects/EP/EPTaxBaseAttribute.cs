// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTaxBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public abstract class EPTaxBaseAttribute : TaxAttribute
{
  public abstract bool IsTaxTipAttribute();

  protected override bool CalcGrossOnDocumentLevel
  {
    get => true;
    set => base.CalcGrossOnDocumentLevel = value;
  }

  protected override bool AskRecalculationOnCalcModeChange
  {
    get => true;
    set => base.AskRecalculationOnCalcModeChange = value;
  }

  public EPTaxBaseAttribute()
    : base(typeof (EPExpenseClaimDetails), typeof (EPTax), typeof (EPTaxTran), typeof (EPExpenseClaimDetails.taxCalcMode))
  {
  }

  public EPTaxBaseAttribute(Type ParentType, Type TaxType, Type TaxSumType, Type CalcMode)
    : base(ParentType, TaxType, TaxSumType, CalcMode)
  {
    this.Init();
  }

  private void Init() => this._LineNbr = "ClaimDetailID";

  protected override object InitializeTaxDet(object data)
  {
    data = base.InitializeTaxDet(data);
    switch (data)
    {
      case EPTax _:
        ((EPTax) data).IsTipTax = new bool?(this.IsTaxTipAttribute());
        break;
      case EPTaxTran _:
        ((EPTaxTran) data).IsTipTax = new bool?(this.IsTaxTipAttribute());
        break;
    }
    return data;
  }

  protected override bool AskRecalculate(PXCache sender, PXCache detailCache, object detail)
  {
    return false;
  }

  protected override List<object> ChildSelect(PXCache cache, object data)
  {
    return new List<object>() { cache.Current };
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    return ((Decimal?) this.ParentGetValue(sender.Graph, this._CuryLineTotal)).GetValueOrDefault();
  }

  protected override Decimal? GetTaxableAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<EPExpenseClaimDetails.curyTaxableAmt>(row);
  }

  protected override Decimal? GetTaxAmt(PXCache sender, object row)
  {
    return (Decimal?) sender.GetValue<EPExpenseClaimDetails.curyTaxTotal>(row);
  }

  protected virtual string GetRefNbr(PXCache sender, object row)
  {
    return (string) sender.GetValue<EPExpenseClaimDetails.refNbr>(row);
  }

  protected virtual string GetTaxZoneLocal(PXCache sender, object row)
  {
    return (string) sender.GetValue(row, this._TaxZoneID);
  }

  protected override string GetExtCostLabel(PXCache sender, object row)
  {
    return ((PXFieldState) sender.GetValueExt<EPExpenseClaimDetails.curyExtCost>(row)).DisplayName;
  }

  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    if (row.LegacyReceipt.GetValueOrDefault() && (!object.Equals((object) this.GetTaxCategory(sender, e.OldRow), (object) this.GetTaxCategory(sender, e.Row)) || !object.Equals((object) this.GetCuryTranAmt(sender, e.OldRow), (object) this.GetCuryTranAmt(sender, e.Row)) || !object.Equals((object) this.GetTaxZoneLocal(sender, e.OldRow), (object) this.GetTaxZoneLocal(sender, e.Row)) || !object.Equals((object) this.GetRefNbr(sender, e.OldRow), (object) this.GetRefNbr(sender, e.Row)) || !object.Equals((object) this.GetTaxID(sender, e.OldRow), (object) this.GetTaxID(sender, e.Row))))
    {
      ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.DeleteLegacyTaxRows(sender.Graph, row.RefNbr);
      PXCache cach = sender.Graph.Caches[this._ChildType];
      this.Preload(sender);
      List<object> details = this.ChildSelect(cach, e.Row);
      this.ReDefaultTaxes(cach, details);
      this._ParentRow = e.Row;
      this.CalcTaxes(cach, (object) null);
      this._ParentRow = (object) null;
      row.LegacyReceipt = new bool?(false);
    }
    this._ParentRow = e.Row;
    base.RowUpdated(sender, e);
    this._ParentRow = (object) null;
  }

  protected override void Tax_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    base.Tax_RowUpdated(sender, e);
    TaxDetail row = e.Row as TaxDetail;
    TaxDetail oldRow = e.OldRow as TaxDetail;
    if (!e.ExternalCall || sender.ObjectsEqual<TaxAttribute.curyTaxAmt>((object) row, (object) oldRow) && sender.ObjectsEqual<TaxAttribute.curyExpenseAmt>((object) row, (object) oldRow) || this.ParentCache(sender.Graph).Current == null)
      return;
    this.ParentUpdate(sender.Graph);
  }

  protected virtual void ParentUpdate(PXGraph graph)
  {
    PXCache pxCache = this.ParentCache(graph);
    if (this._ParentRow == null)
      pxCache.Update(pxCache.Current);
    else
      pxCache.Update(this._ParentRow);
  }

  protected override void ParentSetValue(PXGraph graph, string fieldname, object value)
  {
    PXCache pxCache = this.ParentCache(graph);
    if (this._ParentRow == null)
    {
      pxCache.CreateCopy(pxCache.Current);
      pxCache.SetValueExt(pxCache.Current, fieldname, value);
    }
    else
      pxCache.SetValueExt(this._ParentRow, fieldname, value);
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    List<object> taxList = new List<object>();
    BqlCommand select = (BqlCommand) new Select2<Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<Tax.reverseTax, Equal<boolFalse>, Or<TaxRev.taxType, Equal<TaxType.sales>, And<Tax.reverseTax, Equal<boolTrue>, Or<Tax.taxType, Equal<CSTaxType.use>, Or<Tax.taxType, Equal<CSTaxType.withholding>>>>>>>, And<Current<EPExpenseClaimDetails.expenseDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>();
    object[] currents = new object[1]{ row };
    switch (taxchk)
    {
      case PXTaxCheck.Line:
      case PXTaxCheck.RecalcLine:
        PXSelectBase<EPTax> pxSelectBase1 = (PXSelectBase<EPTax>) new PXSelect<EPTax, Where<EPTax.claimDetailID, Equal<Current<EPExpenseClaimDetails.claimDetailID>>>>(graph);
        if (this.IsTaxTipAttribute())
          pxSelectBase1.WhereAnd<Where<EPTax.isTipTax, Equal<True>>>();
        else
          pxSelectBase1.WhereAnd<Where<EPTax.isTipTax, Equal<False>>>();
        IList<ITaxDetail> list1 = (IList<ITaxDetail>) ((IEnumerable<ITaxDetail>) ((PXSelectBase) pxSelectBase1).View.SelectMultiBound(currents, Array.Empty<object>()).Cast<EPTax>()).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list1, select, currents, parameters);
        using (IEnumerator<ITaxDetail> enumerator = list1.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            EPTax current = (EPTax) enumerator.Current;
            this.InsertTax<EPTax>(graph, taxchk, current, tails1, taxList);
          }
          break;
        }
      case PXTaxCheck.RecalcTotals:
        PXSelectBase<EPTaxTran> pxSelectBase2 = (PXSelectBase<EPTaxTran>) new PXSelect<EPTaxTran, Where<EPTaxTran.claimDetailID, Equal<Current<EPExpenseClaimDetails.claimDetailID>>>>(graph);
        if (this.IsTaxTipAttribute())
          pxSelectBase2.WhereAnd<Where<EPTaxTran.isTipTax, Equal<True>>>();
        else
          pxSelectBase2.WhereAnd<Where<EPTaxTran.isTipTax, Equal<False>>>();
        IList<ITaxDetail> list2 = (IList<ITaxDetail>) ((IEnumerable<ITaxDetail>) ((PXSelectBase) pxSelectBase2).View.SelectMultiBound(currents, Array.Empty<object>()).Cast<EPTaxTran>()).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        IDictionary<string, PXResult<Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list2, select, currents, parameters);
        using (IEnumerator<ITaxDetail> enumerator = list2.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            EPTaxTran current = (EPTaxTran) enumerator.Current;
            this.InsertTax<EPTaxTran>(graph, taxchk, current, tails2, taxList);
          }
          break;
        }
    }
    return taxList;
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    if (row == null)
      row = this.ParentRow(graph);
    if (row is EPExpenseClaimDetails)
      return new List<object>(1) { row };
    return GraphHelper.RowCast<EPExpenseClaimDetails>((IEnumerable) PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.refNbr, Equal<Current<EPExpenseClaim.refNbr>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      row
    }, Array.Empty<object>())).Select<EPExpenseClaimDetails, object>((Func<EPExpenseClaimDetails, object>) (_ => (object) _)).ToList<object>();
  }
}
