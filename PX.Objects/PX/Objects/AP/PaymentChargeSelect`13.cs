// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentChargeSelect`13
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.AP;

public class PaymentChargeSelect<PaymentTable, PaymentMethodID, CashAccountID, DocDate, TranPeriodID, ChargeTable, EntryTypeID, ChargeDocType, ChargeRefNbr, ChargeCashAccount, ChargeFinPeriodID, ChargeTranDate, WhereSelect> : 
  PXSelect<ChargeTable, WhereSelect>
  where PaymentTable : class, IBqlTable, new()
  where PaymentMethodID : IBqlField
  where CashAccountID : IBqlField
  where DocDate : IBqlField
  where TranPeriodID : IBqlField
  where ChargeTable : class, IBqlTable, IPaymentCharge, new()
  where EntryTypeID : IBqlField
  where ChargeDocType : IBqlField
  where ChargeRefNbr : IBqlField
  where ChargeCashAccount : IBqlField
  where ChargeFinPeriodID : IBqlField
  where ChargeTranDate : IBqlField
  where WhereSelect : IBqlWhere, new()
{
  public PaymentChargeSelect(PXGraph graph)
    : base(graph)
  {
    graph.FieldUpdating.AddHandler<PaymentMethodID>(new PXFieldUpdating(this.PaymentMethodID_FieldUpdating));
    graph.FieldUpdating.AddHandler<CashAccountID>(new PXFieldUpdating(this.CashAccountID_FieldUpdating));
    graph.RowPersisting.AddHandler<ChargeTable>(new PXRowPersisting(this.ChargeTable_RowPersisting));
  }

  public virtual void UpdateChangesFromPayment(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<DocDate, TranPeriodID, CashAccountID>(e.Row, e.OldRow))
      return;
    foreach (ChargeTable chargeTable in this.View.SelectMulti())
    {
      if (!sender.ObjectsEqual<CashAccountID>(e.Row, e.OldRow))
        this.View.Cache.SetDefaultExt<ChargeCashAccount>((object) chargeTable);
      if (!sender.ObjectsEqual<DocDate>(e.Row, e.OldRow))
        this.View.Cache.SetDefaultExt<ChargeTranDate>((object) chargeTable);
      if (!sender.ObjectsEqual<TranPeriodID>(e.Row, e.OldRow))
        FinPeriodIDAttribute.DefaultPeriods<ChargeFinPeriodID>(this.View.Cache, (object) chargeTable);
      this.View.Cache.MarkUpdated((object) chargeTable);
    }
  }

  protected virtual void PaymentMethodID_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    PaymentTable row = (PaymentTable) e.Row;
    if ((object) row == null)
      return;
    object objA = sender.GetValue<PaymentMethodID>((object) row);
    if (object.Equals(objA, e.NewValue))
      return;
    PaymentTable copy = (PaymentTable) sender.CreateCopy((object) row);
    sender.SetValue<PaymentMethodID>((object) copy, e.NewValue);
    this.RelatedFieldsDefaulting(sender, copy);
    object newValue;
    sender.RaiseFieldDefaulting<CashAccountID>((object) copy, out newValue);
    sender.SetValue<CashAccountID>((object) copy, newValue);
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CashAccountID>(sender, (object) copy);
    if (object.Equals(sender.GetValue<CashAccountID>((object) row), newValue) || this.CheckPaymentCharge(sender.Graph, (object) cashAccount?.CashAccountCD, "Some finance charges cannot be recorded to the cash account associated with the specified payment method. Do you want to delete these finance charges and use the selected payment method?"))
      return;
    e.NewValue = objA;
  }

  protected virtual void RelatedFieldsDefaulting(PXCache sender, PaymentTable payment)
  {
  }

  protected virtual void CashAccountID_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    PaymentTable row = (PaymentTable) e.Row;
    if ((object) row == null || this.CheckPaymentCharge(sender.Graph, e.NewValue, "Some finance charges cannot be recorded to the specified cash account. Do you want to delete these finance charges and use the selected cash account?"))
      return;
    e.NewValue = sender.GetValue<CashAccountID>((object) row);
  }

  protected virtual bool CheckPaymentCharge(PXGraph graph, object cashAccountCD, string message)
  {
    bool flag1 = true;
    bool flag2 = false;
    foreach (ChargeTable data in this.View.SelectMulti())
    {
      object obj = this.View.Cache.GetValue<EntryTypeID>((object) data);
      if ((CashAccountETDetail) PXSelectBase<CashAccountETDetail, PXSelectJoin<CashAccountETDetail, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CashAccountETDetail.cashAccountID>>>, Where<PX.Objects.CA.CashAccount.cashAccountCD, Equal<Required<PX.Objects.CA.CashAccount.cashAccountCD>>, And<CashAccountETDetail.entryTypeID, Equal<Required<EntryTypeID>>>>>.Config>.Select(graph, cashAccountCD, obj) == null)
      {
        if (!flag2)
        {
          if (this.View.Ask("Warning", message, MessageButtons.YesNo) == WebDialogResult.No)
          {
            flag1 = false;
            break;
          }
          flag2 = true;
        }
        this.View.Cache.Delete((object) data);
      }
    }
    return flag1;
  }

  protected virtual void ReverseCharges(PX.Objects.CM.IRegister oldDoc, PX.Objects.CM.IRegister newDoc, bool reverseSign)
  {
    foreach (PXResult<ChargeTable, CashAccountETDetail> pxResult in PXSelectBase<ChargeTable, PXSelectJoin<ChargeTable, LeftJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<EntryTypeID>, And<CashAccountETDetail.cashAccountID, Equal<PX.Data.Current<CashAccountID>>>>>, Where<ChargeDocType, Equal<Required<ChargeDocType>>, And<ChargeRefNbr, Equal<Required<ChargeRefNbr>>>>>.Config>.Select(this._Graph, (object) oldDoc.DocType, (object) oldDoc.RefNbr))
    {
      ChargeTable chargeTable = pxResult.GetItem<ChargeTable>();
      if (pxResult.GetItem<CashAccountETDetail>().EntryTypeID == null)
      {
        PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find(this._Graph, (int?) this._Graph.Caches[typeof (PaymentTable)].GetValue<CashAccountID>(this._Graph.Caches[typeof (PaymentTable)].Current));
        throw new PXException("The charge cannot be reversed. The {0} entry type is not configured for the {1} cash account. To proceed, add the entry type for the cash account on the Cash Accounts (CA202000) form.", new object[2]
        {
          (object) chargeTable.EntryTypeID,
          (object) cashAccount.CashAccountCD
        });
      }
      ChargeTable copy = PXCache<ChargeTable>.CreateCopy(chargeTable);
      copy.DocType = newDoc.DocType;
      copy.RefNbr = (string) null;
      // ISSUE: variable of a boxed type
      __Boxed<ChargeTable> local = (object) copy;
      Decimal num = (Decimal) (reverseSign ? -1 : 1);
      Decimal? curyTranAmt = copy.CuryTranAmt;
      Decimal? nullable = curyTranAmt.HasValue ? new Decimal?(num * curyTranAmt.GetValueOrDefault()) : new Decimal?();
      local.CuryTranAmt = nullable;
      copy.Released = new bool?(false);
      copy.CashTranID = new long?();
      copy.CuryInfoID = newDoc.CuryInfoID;
      copy.TranDate = newDoc.DocDate;
      this.Insert(copy);
    }
  }

  /// <summary>
  /// <typeparamref name="ChargeTable" /> row persisting event. Implements logic for <see cref="P:PX.Objects.AP.IPaymentCharge.CuryTranAmt" /> check.
  /// </summary>
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
    this.Cache.RaiseExceptionHandling("CuryTranAmt", (object) row, (object) row.CuryTranAmt, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
    {
      (object) "0"
    }));
  }

  /// <summary>
  /// Check if the negative sign of the <see cref="P:PX.Objects.AP.IPaymentCharge.CuryTranAmt" /> of the charge is allowed by document type. Default implementation simply returns <c>false</c>.
  /// </summary>
  /// <param name="charge">The charge.</param>
  /// <returns />
  protected virtual bool IsAllowedNegativeSign(ChargeTable charge) => false;
}
