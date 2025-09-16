// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentChargeCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.AP;

public class APPaymentChargeCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  [Obsolete("This constructor has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public APPaymentChargeCashTranIDAttribute(System.Type isMigrationModeEnabledSetupField)
  {
  }

  public APPaymentChargeCashTranIDAttribute()
  {
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    APPaymentChargeTran paymentChargeTran1 = (APPaymentChargeTran) orig_Row;
    if (!paymentChargeTran1.Released.GetValueOrDefault() && paymentChargeTran1.CuryTranAmt.HasValue)
    {
      Decimal? curyTranAmt = paymentChargeTran1.CuryTranAmt;
      Decimal num1 = 0M;
      if (!(curyTranAmt.GetValueOrDefault() == num1 & curyTranAmt.HasValue) && !string.IsNullOrEmpty(paymentChargeTran1.EntryTypeID))
      {
        catran_Row.OrigModule = "AP";
        catran_Row.OrigTranType = paymentChargeTran1.DocType;
        catran_Row.OrigRefNbr = paymentChargeTran1.RefNbr;
        catran_Row.IsPaymentChargeTran = new bool?(true);
        catran_Row.OrigLineNbr = paymentChargeTran1.LineNbr;
        catran_Row.CashAccountID = paymentChargeTran1.CashAccountID;
        catran_Row.CuryInfoID = paymentChargeTran1.CuryInfoID;
        CATran caTran = catran_Row;
        Decimal caSign = (Decimal) paymentChargeTran1.GetCASign();
        curyTranAmt = paymentChargeTran1.CuryTranAmt;
        Decimal? nullable = curyTranAmt.HasValue ? new Decimal?(caSign * curyTranAmt.GetValueOrDefault()) : new Decimal?();
        caTran.CuryTranAmt = nullable;
        catran_Row.ExtRefNbr = paymentChargeTran1.ExtRefNbr;
        catran_Row.DrCr = paymentChargeTran1.DrCr;
        CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, paymentChargeTran1.TranPeriodID);
        APRegister apRegister = (APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.docType, Equal<Required<APPaymentChargeTran.docType>>, And<APRegister.refNbr, Equal<Required<APPaymentChargeTran.refNbr>>>>>.Config>.Select(sender.Graph, (object) paymentChargeTran1.DocType, (object) paymentChargeTran1.RefNbr);
        catran_Row.ReferenceID = apRegister.VendorID;
        catran_Row.TranDate = paymentChargeTran1.TranDate;
        catran_Row.TranDesc = paymentChargeTran1.TranDesc;
        catran_Row.Released = paymentChargeTran1.Released;
        catran_Row.Hold = apRegister.Hold;
        PX.Objects.CA.CashAccount cashAccount = CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph);
        catran_Row.CuryID = cashAccount.CuryID;
        CashTranIDAttribute.SetCleared(catran_Row, cashAccount);
        foreach (string DocType in APPaymentType.GetVoidedAPDocType(paymentChargeTran1.DocType))
        {
          int num2 = APPaymentType.DrCr(DocType) == APPaymentType.DrCr(paymentChargeTran1.DocType) ? -1 : 1;
          PXGraph graph = sender.Graph;
          object[] objArray = new object[4]
          {
            (object) apRegister.RefNbr,
            (object) DocType,
            (object) catran_Row.OrigLineNbr,
            null
          };
          Decimal num3 = (Decimal) num2;
          curyTranAmt = paymentChargeTran1.CuryTranAmt;
          objArray[3] = (object) (curyTranAmt.HasValue ? new Decimal?(num3 * curyTranAmt.GetValueOrDefault()) : new Decimal?());
          APPaymentChargeTran paymentChargeTran2 = (APPaymentChargeTran) PXSelectBase<APPaymentChargeTran, PXSelectJoin<APPaymentChargeTran, InnerJoin<CATran, On<CATran.tranID, Equal<APPaymentChargeTran.cashTranID>>>, Where<APPaymentChargeTran.refNbr, Equal<Required<APRegister.refNbr>>, And<APPaymentChargeTran.docType, Equal<Required<APRegister.docType>>, And<APPaymentChargeTran.lineNbr, Equal<Required<CATran.origLineNbr>>, And<APPaymentChargeTran.curyTranAmt, Equal<Required<APPaymentChargeTran.curyTranAmt>>>>>>>.Config>.Select(graph, objArray);
          if (paymentChargeTran2 != null)
          {
            catran_Row.VoidedTranID = paymentChargeTran2.CashTranID;
            break;
          }
        }
        return catran_Row;
      }
    }
    return (CATran) null;
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is APPaymentChargeCashTranIDAttribute)
      {
        ((APPaymentChargeCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
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
