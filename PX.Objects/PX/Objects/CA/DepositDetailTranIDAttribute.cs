// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.DepositDetailTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

public class DepositDetailTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  public static CATran DefaultValues(PXCache sender, CATran catran_Row, CADepositDetail orig_Row)
  {
    CADepositDetail caDepositDetail1 = orig_Row;
    CADeposit caDeposit = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.tranType, Equal<Required<CADeposit.tranType>>, And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) caDepositDetail1.TranType,
      (object) caDepositDetail1.RefNbr
    }));
    if (!caDeposit.Released.GetValueOrDefault() || !catran_Row.TranID.HasValue)
    {
      Decimal? nullable1 = caDepositDetail1.CuryTranAmt;
      if (nullable1.HasValue)
      {
        nullable1 = caDepositDetail1.CuryTranAmt;
        Decimal num1 = 0M;
        if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
        {
          catran_Row.OrigModule = "CA";
          catran_Row.OrigTranType = caDepositDetail1.TranType;
          catran_Row.OrigRefNbr = caDepositDetail1.RefNbr;
          catran_Row.OrigLineNbr = caDepositDetail1.LineNbr;
          catran_Row.CashAccountID = caDepositDetail1.CashAccountID;
          catran_Row.CuryID = caDepositDetail1.OrigCuryID;
          catran_Row.CuryInfoID = caDepositDetail1.CuryInfoID;
          CATran caTran1 = catran_Row;
          nullable1 = caDepositDetail1.CuryOrigAmtSigned;
          Decimal num2 = (Decimal) (caDepositDetail1.DrCr == "D" ? 1 : -1);
          Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num2) : new Decimal?();
          caTran1.CuryTranAmt = nullable2;
          CATran caTran2 = catran_Row;
          nullable1 = caDepositDetail1.OrigAmtSigned;
          Decimal num3 = (Decimal) (caDepositDetail1.DrCr == "D" ? 1 : -1);
          Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num3) : new Decimal?();
          caTran2.TranAmt = nullable3;
          catran_Row.DrCr = caDepositDetail1.DrCr;
          catran_Row.TranDesc = caDepositDetail1.TranDesc;
          catran_Row.ReferenceID = new int?();
          catran_Row.Released = caDeposit.Released;
          catran_Row.Voided = caDeposit.Voided;
          if (caDepositDetail1.DetailType == "CHD" || caDepositDetail1.DetailType == "VCD")
            catran_Row.TranDesc = $"{caDepositDetail1.OrigDocType}-{caDepositDetail1.OrigRefNbr}";
          if (caDeposit != null)
          {
            catran_Row.Hold = caDeposit.Hold;
            CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, caDeposit.TranPeriodID);
            catran_Row.ExtRefNbr = caDeposit.ExtRefNbr;
            catran_Row.TranDate = caDeposit.TranDate;
            catran_Row.Cleared = caDeposit.Cleared;
            catran_Row.ClearDate = caDeposit.ClearDate;
          }
          if (caDepositDetail1.DetailType == "VCD")
          {
            CADepositDetail caDepositDetail2 = PXResultset<CADepositDetail>.op_Implicit(PXSelectBase<CADepositDetail, PXSelectJoin<CADepositDetail, InnerJoin<CATran, On<CATran.tranID, Equal<CADepositDetail.tranID>>>, Where<CADepositDetail.refNbr, Equal<Required<CADepositDetail.refNbr>>, And<CADepositDetail.tranType, Equal<Required<CADepositDetail.tranType>>, And<CADepositDetail.lineNbr, Equal<Required<CADepositDetail.lineNbr>>>>>>.Config>.Select(sender.Graph, new object[3]
            {
              (object) caDepositDetail1.RefNbr,
              (object) "CDT",
              (object) caDepositDetail1.LineNbr
            }));
            if (caDepositDetail2 != null)
              catran_Row.VoidedTranID = caDepositDetail2.TranID;
          }
          CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
          return catran_Row;
        }
      }
    }
    return (CATran) null;
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is DepositDetailTranIDAttribute)
      {
        ((DepositDetailTranIDAttribute) attribute)._IsIntegrityCheck = true;
        return ((CashTranIDAttribute) attribute).DefaultValues(sender, new CATran(), data);
      }
    }
    return (CATran) null;
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    return DepositDetailTranIDAttribute.DefaultValues(sender, catran_Row, (CADepositDetail) orig_Row);
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
