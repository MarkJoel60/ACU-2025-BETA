// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAChargeSelect`7
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CAChargeSelect<DocumentTable, DocDate, FinPeriodID, ChargeTable, EntryTypeID, ChargeRefNbr, WhereSelect> : 
  PXSelect<ChargeTable, WhereSelect>
  where DocumentTable : class, ICADocument, IBqlTable, new()
  where DocDate : IBqlField
  where FinPeriodID : IBqlField
  where ChargeTable : class, IBqlTable, IPaymentCharge, new()
  where EntryTypeID : IBqlField
  where ChargeRefNbr : IBqlField
  where WhereSelect : IBqlWhere, new()
{
  public CAChargeSelect(PXGraph graph)
    : base(graph)
  {
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    CAChargeSelect<DocumentTable, DocDate, FinPeriodID, ChargeTable, EntryTypeID, ChargeRefNbr, WhereSelect> caChargeSelect1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) caChargeSelect1, __vmethodptr(caChargeSelect1, PaymentRowUpdated));
    rowUpdated.AddHandler<DocumentTable>(pxRowUpdated);
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    CAChargeSelect<DocumentTable, DocDate, FinPeriodID, ChargeTable, EntryTypeID, ChargeRefNbr, WhereSelect> caChargeSelect2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) caChargeSelect2, __vmethodptr(caChargeSelect2, ChargeTable_RowPersisting));
    rowPersisting.AddHandler<ChargeTable>(pxRowPersisting);
  }

  protected virtual void PaymentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<DocDate, FinPeriodID>(e.Row, e.OldRow))
      return;
    foreach (ChargeTable chargeTable in ((PXSelectBase) this).View.SelectMulti(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this).View.Cache, (object) chargeTable);
  }

  protected virtual void ChargeTable_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ChargeTable row = (ChargeTable) e.Row;
    Decimal? curyTranAmt1 = row.CuryTranAmt;
    Decimal num1 = 0M;
    if (!(curyTranAmt1.GetValueOrDefault() == num1 & curyTranAmt1.HasValue))
    {
      Decimal? curyTranAmt2 = row.CuryTranAmt;
      Decimal num2 = 0M;
      if (!(curyTranAmt2.GetValueOrDefault() < num2 & curyTranAmt2.HasValue) || this.IsAllowedNegativeSign(row))
        return;
    }
    ((PXSelectBase) this).Cache.RaiseExceptionHandling("CuryTranAmt", (object) row, (object) row.CuryTranAmt, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
    {
      (object) "0"
    }));
  }

  public void ReverseExpenses(ICADocument oldDoc, ICADocument newDoc)
  {
    this.ReverseCharges(oldDoc, newDoc, true);
  }

  protected virtual void ReverseCharges(ICADocument oldDoc, ICADocument newDoc, bool reverseSign)
  {
    foreach (PXResult<ChargeTable> pxResult in PXSelectBase<ChargeTable, PXSelect<ChargeTable, Where<ChargeRefNbr, Equal<Required<ChargeRefNbr>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) oldDoc.RefNbr
    }))
      ((PXSelectBase<ChargeTable>) this).Insert(this.ReverseCharge(PXResult<ChargeTable>.op_Implicit(pxResult), reverseSign));
  }

  public virtual ChargeTable ReverseCharge(ChargeTable oldCharge, bool reverseSign)
  {
    ChargeTable copy = PXCache<ChargeTable>.CreateCopy(oldCharge);
    copy.DocType = "CTE";
    copy.RefNbr = (string) null;
    // ISSUE: variable of a boxed type
    __Boxed<ChargeTable> local = (object) copy;
    Decimal num = (Decimal) (reverseSign ? -1 : 1);
    Decimal? curyTranAmt = copy.CuryTranAmt;
    Decimal? nullable = curyTranAmt.HasValue ? new Decimal?(num * curyTranAmt.GetValueOrDefault()) : new Decimal?();
    local.CuryTranAmt = nullable;
    copy.Released = new bool?(false);
    copy.CashTranID = new long?();
    ((PXSelectBase) this).Cache.SetValueExt((object) copy, "NoteID", (object) null);
    return copy;
  }

  protected virtual bool IsAllowedNegativeSign(ChargeTable charge) => true;
}
