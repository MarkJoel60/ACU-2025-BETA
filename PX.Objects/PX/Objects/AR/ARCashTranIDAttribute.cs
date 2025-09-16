// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Specialized for the ARPayment  version of the <see cref="T:PX.Objects.CA.CashTranIDAttribute" /><br />
/// Since CATran created from the source row, it may be used only the fields <br />
/// of ARPayment compatible DAC. <br />
/// The main purpuse of the attribute - to create CATran <br />
/// for the source row and provide CATran and source synchronization on persisting.<br />
/// CATran cache must exists in the calling Graph.<br />
/// </summary>
public class ARCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  [Obsolete("This constructor has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public ARCashTranIDAttribute(Type isMigrationModeEnabledSetupField)
  {
  }

  public ARCashTranIDAttribute()
  {
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    return ARCashTranIDAttribute.DefaultValues(sender, catran_Row, (ARPayment) orig_Row);
  }

  public static CATran DefaultValues(PXCache sender, CATran catran_Row, ARPayment orig_Row)
  {
    ARPayment arPayment1 = orig_Row;
    if (!(arPayment1.DocType == "CRM") && !(arPayment1.DocType == "SMB") && !(arPayment1.DocType == "PPI"))
    {
      bool? nullable1 = arPayment1.Released;
      long? nullable2;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = catran_Row.TranID;
        if (nullable2.HasValue)
          goto label_8;
      }
      Decimal? nullable3 = arPayment1.CuryOrigDocAmt;
      if (nullable3.HasValue)
      {
        nullable1 = arPayment1.IsMigratedRecord;
        if (!nullable1.GetValueOrDefault())
        {
          nullable3 = arPayment1.CuryOrigDocAmt;
          Decimal num1 = 0M;
          if (!(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue))
          {
            nullable1 = arPayment1.Released;
            bool flag1 = false;
            if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
            {
              nullable1 = arPayment1.Voided;
              if (nullable1.GetValueOrDefault())
                goto label_8;
            }
            catran_Row.OrigModule = "AR";
            catran_Row.OrigTranType = arPayment1.DocType;
            catran_Row.OrigRefNbr = arPayment1.RefNbr;
            catran_Row.ExtRefNbr = arPayment1.ExtRefNbr;
            catran_Row.CashAccountID = arPayment1.CashAccountID;
            catran_Row.CuryInfoID = arPayment1.CuryInfoID;
            catran_Row.CuryID = arPayment1.CuryID;
            CATran caTran1 = catran_Row;
            Decimal num2 = (Decimal) (ARPaymentType.DrCr(arPayment1.DocType) == "C" ? -1 : 1);
            Decimal? nullable4 = arPayment1.CuryOrigDocAmt;
            nullable3 = nullable4.HasValue ? new Decimal?(num2 * nullable4.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable5 = arPayment1.CuryConsolidateChargeTotal;
            Decimal? nullable6;
            if (!(nullable3.HasValue & nullable5.HasValue))
            {
              nullable4 = new Decimal?();
              nullable6 = nullable4;
            }
            else
              nullable6 = new Decimal?(nullable3.GetValueOrDefault() - nullable5.GetValueOrDefault());
            caTran1.CuryTranAmt = nullable6;
            nullable5 = catran_Row.CuryTranAmt;
            Decimal num3 = 0M;
            catran_Row.DrCr = !(nullable5.GetValueOrDefault() < num3 & nullable5.HasValue) ? "D" : "C";
            string[] voidedArDocType = ARPaymentType.GetVoidedARDocType(arPayment1.DocType);
            catran_Row.TranDate = arPayment1.DocDate;
            catran_Row.TranDesc = arPayment1.DocDesc;
            CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, arPayment1.TranPeriodID);
            catran_Row.ReferenceID = arPayment1.CustomerID;
            catran_Row.Released = arPayment1.Released;
            catran_Row.Hold = arPayment1.Hold;
            CATran caTran2 = catran_Row;
            nullable1 = arPayment1.DontApprove;
            bool flag2 = false;
            int num4;
            if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
            {
              nullable1 = arPayment1.Approved;
              bool flag3 = false;
              num4 = nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue ? 1 : 0;
            }
            else
              num4 = 0;
            bool? nullable7 = new bool?(num4 != 0);
            caTran2.PendingApproval = nullable7;
            catran_Row.Cleared = arPayment1.Cleared;
            catran_Row.ClearDate = arPayment1.ClearDate;
            nullable2 = arPayment1.CARefTranID;
            if (nullable2.HasValue)
            {
              catran_Row.RefTranAccountID = arPayment1.CARefTranAccountID;
              catran_Row.RefTranID = arPayment1.CARefTranID;
              catran_Row.RefSplitLineNbr = arPayment1.CARefSplitLineNbr;
            }
            foreach (string str in voidedArDocType)
            {
              ARPayment arPayment2 = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelectJoin<ARPayment, InnerJoin<CATran, On<CATran.tranID, Equal<ARPayment.cATranID>>>, Where<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>, And<ARPayment.docType, Equal<Required<ARPayment.docType>>>>>.Config>.Select(sender.Graph, new object[2]
              {
                (object) arPayment1.RefNbr,
                (object) str
              }));
              if (arPayment2 != null)
              {
                catran_Row.VoidedTranID = arPayment2.CATranID;
                break;
              }
            }
            CashTranIDAttribute.SetCleared(catran_Row, CashTranIDAttribute.GetCashAccount(catran_Row, sender.Graph));
            return catran_Row;
          }
        }
      }
    }
label_8:
    return (CATran) null;
  }

  protected override bool NeedPreventCashTransactionCreation(PXCache sender, object row)
  {
    return row is ARPayment arPayment && !arPayment.CashAccountID.HasValue || base.NeedPreventCashTransactionCreation(sender, row);
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is ARCashTranIDAttribute)
      {
        ((ARCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
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
