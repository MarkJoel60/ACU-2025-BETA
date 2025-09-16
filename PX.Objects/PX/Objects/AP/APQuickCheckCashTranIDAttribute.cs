// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APQuickCheckCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.Standalone;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.AP;

public class APQuickCheckCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  [Obsolete("This constructor has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public APQuickCheckCashTranIDAttribute(System.Type isMigrationModeEnabledSetupField)
  {
  }

  public APQuickCheckCashTranIDAttribute()
  {
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    APQuickCheck apQuickCheck = (APQuickCheck) orig_Row;
    if (apQuickCheck.CashAccountID.HasValue && (!apQuickCheck.Released.GetValueOrDefault() || !catran_Row.TranID.HasValue) && apQuickCheck.CuryOrigDocAmt.HasValue && !apQuickCheck.IsMigratedRecord.GetValueOrDefault())
    {
      Decimal? curyOrigDocAmt = apQuickCheck.CuryOrigDocAmt;
      Decimal num1 = 0M;
      if (!(curyOrigDocAmt.GetValueOrDefault() == num1 & curyOrigDocAmt.HasValue))
      {
        catran_Row.OrigModule = "AP";
        catran_Row.OrigTranType = apQuickCheck.DocType;
        catran_Row.OrigRefNbr = apQuickCheck.RefNbr;
        catran_Row.ExtRefNbr = apQuickCheck.ExtRefNbr;
        catran_Row.CashAccountID = apQuickCheck.CashAccountID;
        catran_Row.CuryInfoID = apQuickCheck.CuryInfoID;
        catran_Row.CuryID = apQuickCheck.CuryID;
        string str = string.Empty;
        switch (apQuickCheck.DocType)
        {
          case "QCK":
            CATran caTran1 = catran_Row;
            curyOrigDocAmt = apQuickCheck.CuryOrigDocAmt;
            Decimal? nullable1 = curyOrigDocAmt.HasValue ? new Decimal?(-curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
            caTran1.CuryTranAmt = nullable1;
            catran_Row.DrCr = "C";
            break;
          case "VQC":
            catran_Row.CuryTranAmt = apQuickCheck.CuryOrigDocAmt;
            catran_Row.DrCr = "D";
            str = "QCK";
            break;
          case "RQC":
            catran_Row.CuryTranAmt = apQuickCheck.CuryOrigDocAmt;
            catran_Row.DrCr = "D";
            break;
          default:
            throw new PXException();
        }
        catran_Row.TranDate = apQuickCheck.DocDate;
        catran_Row.TranDesc = apQuickCheck.DocDesc;
        CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, apQuickCheck.TranPeriodID);
        catran_Row.ReferenceID = apQuickCheck.VendorID;
        catran_Row.Released = new bool?(apQuickCheck.Released.GetValueOrDefault() || apQuickCheck.Prebooked.GetValueOrDefault());
        catran_Row.Hold = apQuickCheck.Hold;
        CATran caTran2 = catran_Row;
        bool? dontApprove = apQuickCheck.DontApprove;
        bool flag1 = false;
        int num2;
        if (dontApprove.GetValueOrDefault() == flag1 & dontApprove.HasValue)
        {
          bool? approved = apQuickCheck.Approved;
          bool flag2 = false;
          num2 = approved.GetValueOrDefault() == flag2 & approved.HasValue ? 1 : 0;
        }
        else
          num2 = 0;
        bool? nullable2 = new bool?(num2 != 0);
        caTran2.PendingApproval = nullable2;
        catran_Row.Cleared = apQuickCheck.Cleared;
        catran_Row.ClearDate = apQuickCheck.ClearDate;
        if (!string.IsNullOrEmpty(str))
        {
          APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelectJoin<APPayment, InnerJoin<CATran, On<CATran.tranID, Equal<APPayment.cATranID>>>, Where<APPayment.refNbr, Equal<Required<APPayment.refNbr>>, And<APPayment.docType, Equal<Required<APPayment.docType>>>>>.Config>.Select(sender.Graph, (object) apQuickCheck.RefNbr, (object) str);
          if (apPayment != null)
            catran_Row.VoidedTranID = apPayment.CATranID;
        }
        CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
        return catran_Row;
      }
    }
    return (CATran) null;
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is APQuickCheckCashTranIDAttribute)
      {
        ((APQuickCheckCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
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
