// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AdjCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

public class AdjCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is AdjCashTranIDAttribute)
      {
        ((AdjCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
        return ((CashTranIDAttribute) attribute).DefaultValues(sender, new CATran(), data);
      }
    }
    return (CATran) null;
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    CAAdj caAdj = (CAAdj) orig_Row;
    if (caAdj.Released.GetValueOrDefault() && catran_Row.TranID.HasValue)
      return (CATran) null;
    if (catran_Row.TranID.HasValue)
    {
      long? tranId = catran_Row.TranID;
      long num = 0;
      if (!(tranId.GetValueOrDefault() < num & tranId.HasValue))
        goto label_5;
    }
    catran_Row.OrigModule = "CA";
    catran_Row.OrigTranType = caAdj.AdjTranType;
    catran_Row.OrigRefNbr = caAdj.TransferNbr != null ? caAdj.TransferNbr : caAdj.AdjRefNbr;
label_5:
    catran_Row.CashAccountID = caAdj.CashAccountID;
    catran_Row.ExtRefNbr = caAdj.ExtRefNbr;
    catran_Row.CuryID = caAdj.CuryID;
    catran_Row.CuryInfoID = caAdj.CuryInfoID;
    CATran caTran1 = catran_Row;
    Decimal? nullable1 = caAdj.CuryTranAmt;
    Decimal num1 = (Decimal) (caAdj.DrCr == "D" ? 1 : -1);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num1) : new Decimal?();
    caTran1.CuryTranAmt = nullable2;
    CATran caTran2 = catran_Row;
    nullable1 = caAdj.TranAmt;
    Decimal num2 = (Decimal) (caAdj.DrCr == "D" ? 1 : -1);
    Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num2) : new Decimal?();
    caTran2.TranAmt = nullable3;
    catran_Row.DrCr = caAdj.DrCr;
    catran_Row.TranDate = caAdj.TranDate;
    catran_Row.TranDesc = caAdj.TranDesc;
    catran_Row.ReferenceID = new int?();
    catran_Row.Released = caAdj.Released;
    catran_Row.Hold = caAdj.Hold;
    CATran caTran3 = catran_Row;
    bool? nullable4 = caAdj.DontApprove;
    bool flag1 = false;
    int num3;
    if (nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue)
    {
      nullable4 = caAdj.Approved;
      bool flag2 = false;
      num3 = nullable4.GetValueOrDefault() == flag2 & nullable4.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool? nullable5 = new bool?(num3 != 0);
    caTran3.PendingApproval = nullable5;
    CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, caAdj.TranPeriodID);
    catran_Row.Cleared = caAdj.Cleared;
    catran_Row.ClearDate = caAdj.ClearDate;
    CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
    return catran_Row;
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
