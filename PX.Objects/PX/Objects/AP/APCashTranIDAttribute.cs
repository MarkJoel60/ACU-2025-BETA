// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Specialized for the APPayment version of the <see cref="T:PX.Objects.CA.CashTranIDAttribute" /><br />
/// Since CATran created from the source row, it may be used only the fields <br />
/// of APPayment compatible DAC. <br />
/// The main purpuse of the attribute - to create CATran <br />
/// for the source row and provide CATran and source synchronization on persisting.<br />
/// CATran cache must exists in the calling Graph.<br />
/// </summary>
public class APCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  [Obsolete("This constructor has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public APCashTranIDAttribute(System.Type isMigrationModeEnabledSetupField)
  {
  }

  public APCashTranIDAttribute()
  {
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    APPayment apPayment1 = (APPayment) orig_Row;
    if (!(apPayment1.DocType == "PPI") && apPayment1.CashAccountID.HasValue)
    {
      bool? nullable1 = apPayment1.Released;
      long? nullable2;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = catran_Row.TranID;
        if (nullable2.HasValue)
          goto label_6;
      }
      Decimal? nullable3 = apPayment1.CuryOrigDocAmt;
      if (nullable3.HasValue)
      {
        nullable1 = apPayment1.IsMigratedRecord;
        if (!nullable1.GetValueOrDefault())
        {
          nullable3 = apPayment1.CuryOrigDocAmt;
          Decimal num1 = 0M;
          if (!(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue))
          {
            catran_Row.OrigModule = "AP";
            catran_Row.OrigTranType = apPayment1.DocType;
            catran_Row.OrigRefNbr = apPayment1.RefNbr;
            catran_Row.ExtRefNbr = apPayment1.ExtRefNbr;
            catran_Row.CashAccountID = apPayment1.CashAccountID;
            catran_Row.CuryInfoID = apPayment1.CuryInfoID;
            catran_Row.CuryID = apPayment1.CuryID;
            CATran caTran1 = catran_Row;
            Decimal num2 = (Decimal) (APPaymentType.DrCr(apPayment1.DocType) == "C" ? -1 : 1);
            nullable3 = apPayment1.CuryOrigDocAmt;
            Decimal? nullable4 = nullable3.HasValue ? new Decimal?(num2 * nullable3.GetValueOrDefault()) : new Decimal?();
            caTran1.CuryTranAmt = nullable4;
            nullable3 = catran_Row.CuryTranAmt;
            Decimal num3 = 0M;
            catran_Row.DrCr = !(nullable3.GetValueOrDefault() < num3 & nullable3.HasValue) ? "D" : "C";
            string[] voidedApDocType = APPaymentType.GetVoidedAPDocType(apPayment1.DocType);
            catran_Row.TranDate = apPayment1.DocDate;
            catran_Row.TranDesc = apPayment1.DocDesc;
            CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, apPayment1.TranPeriodID);
            catran_Row.ReferenceID = apPayment1.VendorID;
            catran_Row.Released = apPayment1.Released;
            CATran caTran2 = catran_Row;
            nullable1 = apPayment1.DontApprove;
            bool flag1 = false;
            int num4;
            if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
            {
              nullable1 = apPayment1.Approved;
              bool flag2 = false;
              num4 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
            }
            else
              num4 = 0;
            bool? nullable5 = new bool?(num4 != 0);
            caTran2.PendingApproval = nullable5;
            catran_Row.Hold = apPayment1.Hold;
            catran_Row.Cleared = apPayment1.Cleared;
            catran_Row.ClearDate = apPayment1.ClearDate;
            nullable2 = apPayment1.CARefTranID;
            if (nullable2.HasValue)
            {
              catran_Row.RefTranAccountID = apPayment1.CARefTranAccountID;
              catran_Row.RefTranID = apPayment1.CARefTranID;
              catran_Row.RefSplitLineNbr = apPayment1.CARefSplitLineNbr;
            }
            foreach (string str in voidedApDocType)
            {
              APPayment apPayment2 = (APPayment) PXSelectBase<APPayment, PXSelectJoin<APPayment, InnerJoin<CATran, On<CATran.tranID, Equal<APPayment.cATranID>>>, Where<APPayment.refNbr, Equal<Required<APPayment.refNbr>>, And<APPayment.docType, Equal<Required<APPayment.docType>>>>>.Config>.Select(sender.Graph, (object) apPayment1.RefNbr, (object) str);
              if (apPayment2 != null)
              {
                catran_Row.VoidedTranID = apPayment2.CATranID;
                break;
              }
            }
            CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
            return catran_Row;
          }
        }
      }
    }
label_6:
    return (CATran) null;
  }

  protected override bool NeedPreventCashTransactionCreation(PXCache sender, object row)
  {
    return row is APPayment apPayment && !apPayment.CashAccountID.HasValue || base.NeedPreventCashTransactionCreation(sender, row);
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is APCashTranIDAttribute)
      {
        ((APCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
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
