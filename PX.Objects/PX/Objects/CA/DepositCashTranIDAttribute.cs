// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.DepositCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// Creates the cash transaction on the <see cref="P:PX.Objects.CA.CADeposit.ExtraCashAccountID" /> Cash Drop Account.
/// </summary>
public class DepositCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  public static CATran DefaultValues(
    PXCache sender,
    CATran catran_Row,
    CADeposit parentDoc,
    string fieldName)
  {
    if (parentDoc.Released.GetValueOrDefault() && catran_Row.TranID.HasValue || !DepositCashTranIDAttribute.IsCreationNeeded(parentDoc))
      return (CATran) null;
    catran_Row.OrigModule = "CA";
    catran_Row.OrigTranType = parentDoc.TranType == "CVD" ? "CVX" : "CDX";
    catran_Row.OrigRefNbr = parentDoc.RefNbr;
    catran_Row.CashAccountID = parentDoc.ExtraCashAccountID;
    catran_Row.ExtRefNbr = parentDoc.ExtRefNbr;
    catran_Row.CuryID = parentDoc.CuryID;
    catran_Row.CuryInfoID = parentDoc.CuryInfoID;
    catran_Row.DrCr = parentDoc.DrCr == "D" ? "C" : "D";
    CATran caTran1 = catran_Row;
    Decimal? nullable1 = parentDoc.CuryExtraCashTotal;
    Decimal num1 = (Decimal) (catran_Row.DrCr == "D" ? 1 : -1);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num1) : new Decimal?();
    caTran1.CuryTranAmt = nullable2;
    CATran caTran2 = catran_Row;
    nullable1 = parentDoc.ExtraCashTotal;
    Decimal num2 = (Decimal) (catran_Row.DrCr == "D" ? 1 : -1);
    Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num2) : new Decimal?();
    caTran2.TranAmt = nullable3;
    catran_Row.TranDate = parentDoc.TranDate;
    catran_Row.TranDesc = parentDoc.TranDesc;
    catran_Row.ReferenceID = new int?();
    catran_Row.Released = parentDoc.Released;
    catran_Row.Hold = parentDoc.Hold;
    catran_Row.Voided = parentDoc.Voided;
    CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, parentDoc.TranPeriodID);
    catran_Row.Cleared = parentDoc.Cleared;
    catran_Row.ClearDate = parentDoc.ClearDate;
    if (parentDoc.DocType == "CVD")
    {
      CADeposit caDeposit = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelectJoin<CADeposit, InnerJoin<CATran, On<CATran.tranID, Equal<CADeposit.cashTranID>>>, Where<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>, And<CADeposit.tranType, Equal<Required<CADeposit.tranType>>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) parentDoc.RefNbr,
        (object) "CDT"
      }));
      if (caDeposit != null)
        catran_Row.VoidedTranID = (long?) sender.GetValue((object) caDeposit, fieldName);
    }
    CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
    return catran_Row;
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is DepositCashTranIDAttribute)
      {
        ((DepositCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
        return ((CashTranIDAttribute) attribute).DefaultValues(sender, new CATran(), data);
      }
    }
    return (CATran) null;
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    return DepositCashTranIDAttribute.DefaultValues(sender, catran_Row, (CADeposit) orig_Row, this._FieldName);
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

  protected static bool IsCreationNeeded(CADeposit parentDoc)
  {
    if (!parentDoc.ExtraCashAccountID.HasValue || !parentDoc.ExtraCashTotal.HasValue)
      return false;
    Decimal? extraCashTotal = parentDoc.ExtraCashTotal;
    Decimal num = 0M;
    return !(extraCashTotal.GetValueOrDefault() == num & extraCashTotal.HasValue);
  }
}
