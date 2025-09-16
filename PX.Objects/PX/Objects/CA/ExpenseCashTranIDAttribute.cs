// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ExpenseCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

public class ExpenseCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    CAExpense caExpense = (CAExpense) orig_Row;
    if (caExpense.Released.GetValueOrDefault() && catran_Row.TranID.HasValue)
      return (CATran) null;
    catran_Row.OrigModule = "CA";
    catran_Row.OrigTranType = caExpense.DocType;
    catran_Row.OrigRefNbr = caExpense.RefNbr;
    catran_Row.OrigLineNbr = caExpense.LineNbr;
    catran_Row.CashAccountID = caExpense.CashAccountID;
    catran_Row.ExtRefNbr = caExpense.ExtRefNbr;
    catran_Row.CuryID = caExpense.CuryID;
    catran_Row.CuryInfoID = caExpense.CuryInfoID;
    CATran caTran1 = catran_Row;
    Decimal? nullable1 = caExpense.CuryTranAmt;
    Decimal num1 = (Decimal) (caExpense.DrCr == "D" ? 1 : -1);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num1) : new Decimal?();
    caTran1.CuryTranAmt = nullable2;
    CATran caTran2 = catran_Row;
    nullable1 = caExpense.TranAmt;
    Decimal num2 = (Decimal) (caExpense.DrCr == "D" ? 1 : -1);
    Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num2) : new Decimal?();
    caTran2.TranAmt = nullable3;
    catran_Row.DrCr = caExpense.DrCr;
    catran_Row.TranDate = caExpense.TranDate;
    catran_Row.TranDesc = caExpense.TranDesc;
    catran_Row.ReferenceID = new int?();
    catran_Row.Released = caExpense.Released;
    catran_Row.Hold = new bool?(false);
    CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, caExpense.TranPeriodID);
    catran_Row.Cleared = caExpense.Cleared;
    catran_Row.ClearDate = caExpense.ClearDate;
    CashAccount cashAccount = (CashAccount) ((PXSelectBase) new PXSelectReadonly<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>(sender.Graph)).View.SelectSingle(new object[1]
    {
      (object) catran_Row.CashAccountID
    });
    if (cashAccount != null)
    {
      bool? nullable4 = cashAccount.Reconcile;
      bool flag = false;
      if (nullable4.GetValueOrDefault() == flag & nullable4.HasValue)
      {
        nullable4 = catran_Row.Cleared;
        if (!nullable4.GetValueOrDefault() || !catran_Row.TranDate.HasValue)
        {
          catran_Row.Cleared = new bool?(true);
          catran_Row.ClearDate = catran_Row.TranDate;
        }
      }
    }
    return catran_Row;
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is ExpenseCashTranIDAttribute)
      {
        ((ExpenseCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
        return ((CashTranIDAttribute) attribute).DefaultValues(sender, new CATran(), data);
      }
    }
    return (CATran) null;
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this._IsIntegrityCheck)
      return;
    base.RowPersisting(sender, e);
  }

  public override void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (this._IsIntegrityCheck)
      return;
    base.RowPersisted(sender, e);
  }
}
