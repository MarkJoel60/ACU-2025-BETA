// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class GLTaxAttribute : TaxAttribute
{
  protected string _CuryInclTaxTotal = "CuryTaxTotal";
  protected string _CuryTranTotal = nameof (CuryTranTotal);
  protected bool _InternalCall;

  public Type CuryInclTaxTotal
  {
    set => this._CuryInclTaxTotal = value.Name;
    get => (Type) null;
  }

  public Type CuryTranTotal
  {
    set => this._CuryTranTotal = value.Name;
    get => (Type) null;
  }

  public GLTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this.CuryDocBal = (Type) null;
    this.CuryLineTotal = typeof (GLTranDoc.curyTranAmt);
    this.CuryTaxTotal = typeof (GLTranDoc.curyTaxAmt);
    this.CuryInclTaxTotal = typeof (GLTranDoc.curyInclTaxAmt);
    this.DocDate = typeof (GLTranDoc.tranDate);
    this.CuryTranAmt = typeof (GLTranDoc.curyTranAmt);
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<GLTranDoc.debitAccountID, IsNotNull>, GLTranDoc.curyTranTotal>, Sub<GLTranDoc.curyTaxAmt, GLTranDoc.curyInclTaxAmt>>), typeof (SumCalc<GLDocBatch.curyDebitTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<GLTranDoc.creditAccountID, IsNotNull>, GLTranDoc.curyTranTotal>, Sub<GLTranDoc.curyTaxAmt, GLTranDoc.curyInclTaxAmt>>), typeof (SumCalc<GLDocBatch.curyCreditTotal>)));
  }

  protected override Decimal CalcLineTotal(PXCache sender, object row)
  {
    return ((Decimal?) this.ParentGetValue(sender.Graph, this._CuryLineTotal)).GetValueOrDefault();
  }

  protected virtual BqlCommand GetSelectTaxesCommand(object row)
  {
    BqlCommand selectTaxesCommand = (BqlCommand) null;
    GLTranDoc glTranDoc = (GLTranDoc) (this._ParentRow ?? row);
    if (glTranDoc != null && glTranDoc.TranModule == "AP")
      selectTaxesCommand = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, Or<TaxRev.taxType, Equal<TaxType.sales>, And<Where<PX.Objects.TX.Tax.reverseTax, Equal<boolTrue>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>, Or<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>>>>>, And<Current<GLTranDoc.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>();
    if (glTranDoc != null && glTranDoc.TranModule == "AR")
      selectTaxesCommand = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<Current<GLTranDoc.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>>();
    if (glTranDoc != null && glTranDoc.TranModule == "CA")
      selectTaxesCommand = (BqlCommand) new Select2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And2<Where<Current<GLTranDoc.cADrCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.purchase>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, Or<Current<GLTranDoc.cADrCr>, Equal<CADrCr.cACredit>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolTrue>, Or<Current<GLTranDoc.cADrCr>, Equal<CADrCr.cADebit>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>>>>>>>>>>>>, And<Current<GLTranDoc.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>();
    return selectTaxesCommand;
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    List<object> taxList = new List<object>();
    BqlCommand selectTaxesCommand = this.GetSelectTaxesCommand(row);
    object[] currents = new object[1]{ row };
    switch (taxchk)
    {
      case PXTaxCheck.Line:
        List<ITaxDetail> list1 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<GLTax>((IEnumerable) PXSelectBase<GLTax, PXSelect<GLTax, Where<GLTax.module, Equal<Current<GLTranDoc.module>>, And<GLTax.batchNbr, Equal<Current<GLTranDoc.batchNbr>>, And<GLTax.detailType, Equal<GLTaxDetailType.lineTax>, And<GLTax.lineNbr, Equal<Current<GLTranDoc.lineNbr>>>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list1 == null || list1.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails1 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list1, selectTaxesCommand, currents, parameters);
        foreach (GLTax record in list1)
          this.InsertTax<GLTax>(graph, taxchk, record, tails1, taxList);
        return taxList;
      case PXTaxCheck.RecalcLine:
        List<ITaxDetail> list2 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<GLTax>((IEnumerable) PXSelectBase<GLTax, PXSelect<GLTax, Where<GLTax.module, Equal<Current<GLTranDoc.module>>, And<GLTax.batchNbr, Equal<Current<GLTranDoc.batchNbr>>, And<GLTax.detailType, Equal<GLTaxDetailType.lineTax>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          row
        }, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list2 == null || list2.Count == 0)
          return taxList;
        HashSet<int?> nullableSet = new HashSet<int?>((IEnumerable<int?>) Array.ConvertAll<object, int?>((object[]) ((IEnumerable<PXResult<GLTranDoc>>) PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.module, Equal<Current<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLTranDoc.batchNbr>>, And<GLTranDoc.parentLineNbr, Equal<Current<GLTranDoc.lineNbr>>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          this._ParentRow
        }, Array.Empty<object>())).ToArray<PXResult<GLTranDoc>>(), (Converter<object, int?>) (a => PXResult.Unwrap<GLTranDoc>(a).LineNbr)));
        if (row == null && this._ParentRow == null)
          return taxList;
        nullableSet.Add(((GLTranDoc) row ?? (GLTranDoc) this._ParentRow).LineNbr);
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails2 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list2, selectTaxesCommand, currents, parameters);
        foreach (GLTax record in list2)
        {
          if (nullableSet.Contains(record.LineNbr))
            this.InsertTax<GLTax>(graph, taxchk, record, tails2, taxList);
        }
        return taxList;
      case PXTaxCheck.RecalcTotals:
        GLTranDoc parentRow = (GLTranDoc) this._ParentRow;
        if (parentRow == null)
          return taxList;
        List<ITaxDetail> list3 = ((IEnumerable<ITaxDetail>) GraphHelper.RowCast<GLTaxTran>((IEnumerable) PXSelectBase<GLTaxTran, PXSelect<GLTaxTran, Where<GLTaxTran.module, Equal<Current<GLTranDoc.module>>, And<GLTaxTran.batchNbr, Equal<Current<GLTranDoc.batchNbr>>, And<GLTaxTran.lineNbr, Equal<Current<GLTranDoc.lineNbr>>>>>>.Config>.SelectMultiBound(graph, new object[1]
        {
          (object) parentRow
        }, Array.Empty<object>()))).ToList<ITaxDetail>();
        if (list3 == null || list3.Count == 0)
          return taxList;
        IDictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> tails3 = this.CollectInvolvedTaxes<Where>(graph, (IEnumerable<ITaxDetail>) list3, selectTaxesCommand, currents, parameters);
        foreach (GLTaxTran record in list3)
          this.InsertTax<GLTaxTran>(graph, taxchk, record, tails3, taxList);
        return taxList;
      default:
        return taxList;
    }
  }

  protected override IEnumerable<ITaxDetail> ManualTaxes(PXCache sender, object row)
  {
    List<ITaxDetail> taxDetailList = new List<ITaxDetail>();
    foreach (PXResult selectTax in this.SelectTaxes(sender, row, PXTaxCheck.RecalcTotals))
      taxDetailList.Add((ITaxDetail) selectTax[0]);
    return (IEnumerable<ITaxDetail>) taxDetailList;
  }

  public override void CacheAttached(PXCache sender)
  {
    if (this.EnableTaxCalcOn(sender.Graph))
    {
      base.CacheAttached(sender);
      PXGraph.RowInsertingEvents rowInserting = sender.Graph.RowInserting;
      Type taxSumType = this._TaxSumType;
      GLTaxAttribute glTaxAttribute = this;
      // ISSUE: virtual method pointer
      PXRowInserting pxRowInserting = new PXRowInserting((object) glTaxAttribute, __vmethodptr(glTaxAttribute, Tax_RowInserting));
      rowInserting.AddHandler(taxSumType, pxRowInserting);
    }
    else
      this.TaxCalc = TaxCalc.NoCalc;
  }

  protected virtual bool EnableTaxCalcOn(PXGraph aGraph) => aGraph is JournalWithSubEntry;

  protected override void Tax_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if ((this._TaxCalc == TaxCalc.NoCalc || !e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc || sender.ObjectsEqual<GLTaxTran.curyTaxAmt>(e.OldRow, e.Row))
      return;
    this._ParentRow = PXParentAttribute.SelectParent(sender, e.Row, typeof (GLTranDoc));
    if (this._ParentRow != null)
      this._ParentRow = (object) PXCache<GLTranDoc>.CreateCopy((GLTranDoc) this._ParentRow);
    this.CalcTotals(sender.Graph.Caches[this._ChildType], (object) null, false);
    sender.Graph.Caches[this._ParentType].Update(this._ParentRow);
  }

  protected virtual void Tax_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (e.ExternalCall)
      throw new PXSetPropertyException("Tax detail can not be inserted manually");
  }

  protected override void Tax_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    bool flag = false;
    if ((this._TaxCalc == TaxCalc.NoCalc || !e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    PXCache cach2 = sender.Graph.Caches[this._TaxType];
    foreach (object obj in TaxParentAttribute.ChildSelect(cach1, e.Row, this._ParentType))
    {
      ITaxDetail taxitem = this.MatchesCategory(cach1, obj, (ITaxDetail) e.Row);
      this.AddOneTax(cach2, obj, taxitem);
      if (taxitem != null)
        flag = true;
    }
    this._NoSumTotals = this._TaxCalc == TaxCalc.ManualCalc && !e.ExternalCall;
    this.CalcTaxes(cach1, (object) null);
    this._NoSumTotals = false;
    if (flag)
      return;
    sender.RaiseExceptionHandling("TaxID", e.Row, (object) ((TaxDetail) e.Row).TaxID, (Exception) new PXSetPropertyException("The {0} tax cannot be applied to the document because there are no document lines whose tax category contains the {0} tax.", (PXErrorLevel) 5, new object[1]
    {
      (object) ((TaxDetail) e.Row).TaxID
    }));
  }

  protected override void Tax_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if ((this._TaxCalc == TaxCalc.NoCalc || !e.ExternalCall) && this._TaxCalc != TaxCalc.ManualCalc)
      return;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    PXCache cach2 = sender.Graph.Caches[this._TaxType];
    foreach (object detrow in TaxParentAttribute.ChildSelect(cach1, e.Row, this._ParentType))
      this.DelOneTax(cach2, detrow, e.Row);
    this.CalcTaxes(cach1, (object) null);
  }

  protected override void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    base.CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
    if (this.ParentGetStatus(sender.Graph) == 3)
      return;
    Decimal objB1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryWhTaxTotal) ?? (object) 0M);
    if (!object.Equals((object) CuryWhTaxTotal, (object) objB1))
      this.ParentSetValue(sender.Graph, this._CuryWhTaxTotal, (object) CuryWhTaxTotal);
    Decimal objB2 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryInclTaxTotal) ?? (object) 0M);
    if (object.Equals((object) CuryInclTaxTotal, (object) objB2))
      return;
    this.ParentSetValue(sender.Graph, this._CuryInclTaxTotal, (object) CuryInclTaxTotal);
  }

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row.ParentLineNbr.HasValue)
    {
      this._ParentRow = PXParentAttribute.SelectParent(sender, e.Row, typeof (GLTranDoc));
      if (this._ParentRow != null)
        this._ParentRow = (object) PXCache<GLTranDoc>.CreateCopy((GLTranDoc) this._ParentRow);
      Decimal? curyTranTotal = row.CuryTranTotal;
      Decimal? curyTranAmt = row.CuryTranAmt;
      if (!(curyTranTotal.GetValueOrDefault() == curyTranAmt.GetValueOrDefault() & curyTranTotal.HasValue == curyTranAmt.HasValue))
        row.CuryTranAmt = row.CuryTranTotal;
      base.RowInserted(sender, e);
      object current = sender.Current;
      sender.Update(this._ParentRow);
      sender.Current = current;
    }
    else
    {
      this._ParentRow = (object) row;
      base.RowInserted(sender, e);
      if (row.IsChildTran || row.TranModule == "GL")
      {
        row.CuryTranAmt = new Decimal?(this.CalcTaxableFromTotal(sender, (object) row, row.CuryTranTotal.Value));
      }
      else
      {
        bool? split = row.Split;
        bool flag = false;
        if (!(split.GetValueOrDefault() == flag & split.HasValue))
          return;
        row.CuryTranAmt = new Decimal?(this.CalcTaxableFromTotal(sender, (object) row, row.CuryTranTotal.Value));
        this.CalcTaxes(sender, (object) row, PXTaxCheck.Line);
      }
    }
  }

  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row.ParentLineNbr.HasValue)
    {
      this._ParentRow = PXParentAttribute.SelectParent(sender, e.Row, typeof (GLTranDoc));
      if (this._ParentRow != null)
        this._ParentRow = (object) PXCache<GLTranDoc>.CreateCopy((GLTranDoc) this._ParentRow);
      Decimal? curyTranTotal1 = row.CuryTranTotal;
      Decimal? curyTranTotal2 = ((GLTranDoc) e.OldRow).CuryTranTotal;
      if (!(curyTranTotal1.GetValueOrDefault() == curyTranTotal2.GetValueOrDefault() & curyTranTotal1.HasValue == curyTranTotal2.HasValue))
        row.CuryTranAmt = row.CuryTranTotal;
      base.RowUpdated(sender, e);
      object current = sender.Current;
      sender.Update(this._ParentRow);
      sender.Current = current;
    }
    else
    {
      this._ParentRow = (object) row;
      GLTranDoc oldRow = (GLTranDoc) e.OldRow;
      bool? split1 = row.Split;
      bool flag1 = false;
      if (split1.GetValueOrDefault() == flag1 & split1.HasValue)
      {
        Decimal? curyTranTotal3 = row.CuryTranTotal;
        Decimal? curyTranTotal4 = oldRow.CuryTranTotal;
        if (!(curyTranTotal3.GetValueOrDefault() == curyTranTotal4.GetValueOrDefault() & curyTranTotal3.HasValue == curyTranTotal4.HasValue))
          row.CuryTranAmt = new Decimal?(this.CalcTaxableFromTotal(sender, (object) row, row.CuryTranTotal.Value));
      }
      GLTranDoc copy = PXCache<GLTranDoc>.CreateCopy((GLTranDoc) this._ParentRow);
      base.RowUpdated(sender, e);
      if (!(row.TaxCategoryID != oldRow.TaxCategoryID) && !(row.TaxZoneID != oldRow.TaxZoneID))
      {
        DateTime? tranDate1 = row.TranDate;
        DateTime? tranDate2 = oldRow.TranDate;
        if ((tranDate1.HasValue == tranDate2.HasValue ? (tranDate1.HasValue ? (tranDate1.GetValueOrDefault() != tranDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          bool? split2 = row.Split;
          bool? split3 = oldRow.Split;
          if (split2.GetValueOrDefault() == split3.GetValueOrDefault() & split2.HasValue == split3.HasValue && !(row.TermsID != oldRow.TermsID))
            goto label_14;
        }
      }
      bool? split4 = row.Split;
      bool flag2 = false;
      if (split4.GetValueOrDefault() == flag2 & split4.HasValue)
      {
        row.CuryTranAmt = new Decimal?(this.CalcTaxableFromTotal(sender, (object) row, row.CuryTranTotal.Value));
        this.CalcTaxes(sender, (object) row, PXTaxCheck.Line);
      }
label_14:
      if (this._TaxCalc == TaxCalc.NoCalc || this._TaxCalc == TaxCalc.ManualLineCalc)
        return;
      PXRowUpdatedEventArgs updatedEventArgs = new PXRowUpdatedEventArgs(e.Row, (object) copy, e.ExternalCall);
      for (int index = 0; index < ((List<PXEventSubscriberAttribute>) this._Attributes).Count; ++index)
      {
        if (this._Attributes[index] is IPXRowUpdatedSubscriber)
          ((IPXRowUpdatedSubscriber) this._Attributes[index]).RowUpdated(sender, updatedEventArgs);
      }
    }
  }

  public override void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row.ParentLineNbr.HasValue)
    {
      this._ParentRow = PXParentAttribute.SelectParent(sender, e.Row, typeof (GLTranDoc));
      if (this._ParentRow != null)
        this._ParentRow = (object) PXCache<GLTranDoc>.CreateCopy((GLTranDoc) this._ParentRow);
      base.RowDeleted(sender, e);
      object current = sender.Current;
      sender.Update(this._ParentRow);
      sender.Current = current;
    }
    else
    {
      this._ParentRow = (object) row;
      base.RowDeleted(sender, e);
    }
  }

  protected override List<object> ChildSelect(PXCache cache, object data)
  {
    GLTranDoc glTranDoc = (GLTranDoc) data;
    bool? split = glTranDoc.Split;
    bool flag = false;
    if (!(split.GetValueOrDefault() == flag & split.HasValue) || glTranDoc.ParentLineNbr.HasValue)
      return base.ChildSelect(cache, data);
    return new List<object>() { data };
  }

  public Decimal CalcTaxableFromTotal(PXCache cache, object row, Decimal aCuryTotal)
  {
    List<object> objectList = this.SelectTaxes<Where<True, Equal<True>>>(cache.Graph, row, PXTaxCheck.Line);
    PX.Objects.CS.Terms terms = this.SelectTerms(cache.Graph);
    Decimal num1 = 0M;
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult<GLTax, PX.Objects.TX.Tax, TaxRev> pxResult in objectList)
    {
      PX.Objects.TX.Tax tax = PXResult<GLTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult);
      TaxRev taxRev = PXResult<GLTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult);
      Decimal num2 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
      if (!(tax.TaxCalcLevel == "0"))
      {
        if (tax.TaxCalcLevel == "1")
        {
          flag1 = true;
          Decimal num3 = 0M;
          if (tax.TaxApplyTermsDisc == "X")
            num3 = terms.DiscPercent.GetValueOrDefault() / 100M;
          num1 += num2 * taxRev.TaxRate.Value * (1M - num3);
        }
        if (tax.TaxCalcLevel == "2" & flag1)
        {
          flag2 = true;
          break;
        }
      }
    }
    if (flag2)
      throw new PXException("Taxable amount can not be calculated - tax on taxes are defined");
    Decimal num4 = PXDBCurrencyAttribute.RoundCury(cache, row, aCuryTotal / (1M + num1 / 100M));
    Decimal num5 = 0M;
    foreach (PXResult<GLTax, PX.Objects.TX.Tax, TaxRev> pxResult in objectList)
    {
      PX.Objects.TX.Tax tax = PXResult<GLTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult);
      TaxRev taxRev = PXResult<GLTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult);
      Decimal num6 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
      if (!(tax.TaxCalcLevel == "0") && tax.TaxCalcLevel == "1")
      {
        Decimal num7 = 0M;
        Decimal? nullable1;
        if (tax.TaxApplyTermsDisc == "X")
        {
          nullable1 = terms.DiscPercent;
          num7 = nullable1.GetValueOrDefault() / 100M;
        }
        Decimal num8 = num6;
        Decimal num9 = num4 * (1M - num7);
        Decimal? nullable2 = taxRev.TaxRate;
        nullable1 = nullable2.HasValue ? new Decimal?(num9 * nullable2.GetValueOrDefault() / 100M) : new Decimal?();
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(num8 * nullable1.GetValueOrDefault());
        nullable2 = nullable3;
        Decimal val = nullable2.GetValueOrDefault();
        if (tax.TaxApplyTermsDisc == "T")
        {
          Decimal num10 = val;
          nullable1 = terms.DiscPercent;
          Decimal num11 = 1M - nullable1.GetValueOrDefault() / 100M;
          val = num10 * num11;
        }
        if (tax.TaxType != "P")
          num5 += PXDBCurrencyAttribute.RoundCury(cache, row, val);
      }
    }
    return aCuryTotal - num5;
  }
}
