// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashSaleCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Specialized for the ARCashSale  version of the <see cref="T:PX.Objects.CA.CashTranIDAttribute" /><br />
/// Since CATran created from the source row, it may be used only the fields <br />
/// of ARPayment compatible DAC. <br />
/// The main purpuse of the attribute - to create CATran <br />
/// for the source row and provide CATran and source synchronization on persisting.<br />
/// CATran cache must exists in the calling Graph.<br />
/// </summary>
public class ARCashSaleCashTranIDAttribute : CashTranIDAttribute
{
  protected bool _IsIntegrityCheck;

  [Obsolete("This constructor has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public ARCashSaleCashTranIDAttribute(Type isMigrationModeEnabledSetupField)
  {
  }

  public ARCashSaleCashTranIDAttribute()
  {
  }

  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    ARCashSale arCashSale = (ARCashSale) orig_Row;
    if (!arCashSale.Released.GetValueOrDefault() && arCashSale.CuryOrigDocAmt.HasValue && !arCashSale.IsMigratedRecord.GetValueOrDefault())
    {
      Decimal? nullable1 = arCashSale.CuryOrigDocAmt;
      Decimal num1 = 0M;
      if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
      {
        catran_Row.OrigModule = "AR";
        catran_Row.OrigTranType = arCashSale.DocType;
        catran_Row.OrigRefNbr = arCashSale.RefNbr;
        catran_Row.ExtRefNbr = arCashSale.ExtRefNbr;
        catran_Row.CashAccountID = arCashSale.CashAccountID;
        catran_Row.CuryInfoID = arCashSale.CuryInfoID;
        catran_Row.CuryID = arCashSale.CuryID;
        switch (arCashSale.DocType)
        {
          case "CSL":
            CATran caTran1 = catran_Row;
            nullable1 = arCashSale.CuryOrigDocAmt;
            Decimal? consolidateChargeTotal = arCashSale.CuryConsolidateChargeTotal;
            Decimal? nullable2 = nullable1.HasValue & consolidateChargeTotal.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - consolidateChargeTotal.GetValueOrDefault()) : new Decimal?();
            caTran1.CuryTranAmt = nullable2;
            catran_Row.DrCr = "D";
            break;
          case "RCS":
            CATran caTran2 = catran_Row;
            Decimal? curyOrigDocAmt = arCashSale.CuryOrigDocAmt;
            Decimal? nullable3 = curyOrigDocAmt.HasValue ? new Decimal?(-curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
            nullable1 = arCashSale.CuryConsolidateChargeTotal;
            Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
            caTran2.CuryTranAmt = nullable4;
            catran_Row.DrCr = "C";
            break;
          default:
            throw new PXException();
        }
        catran_Row.TranDate = arCashSale.DocDate;
        catran_Row.TranDesc = arCashSale.DocDesc;
        CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, arCashSale.TranPeriodID);
        catran_Row.ReferenceID = arCashSale.CustomerID;
        catran_Row.Released = arCashSale.Released;
        catran_Row.Hold = arCashSale.Hold;
        CATran caTran3 = catran_Row;
        bool? nullable5 = arCashSale.DontApprove;
        bool flag1 = false;
        int num2;
        if (nullable5.GetValueOrDefault() == flag1 & nullable5.HasValue)
        {
          nullable5 = arCashSale.Approved;
          bool flag2 = false;
          num2 = nullable5.GetValueOrDefault() == flag2 & nullable5.HasValue ? 1 : 0;
        }
        else
          num2 = 0;
        bool? nullable6 = new bool?(num2 != 0);
        caTran3.PendingApproval = nullable6;
        catran_Row.Cleared = arCashSale.Cleared;
        catran_Row.ClearDate = arCashSale.ClearDate;
        return catran_Row;
      }
    }
    return (CATran) null;
  }

  public static CATran DefaultValues<Field>(PXCache sender, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes<Field>(data))
    {
      if (attribute is ARCashSaleCashTranIDAttribute)
      {
        ((ARCashSaleCashTranIDAttribute) attribute)._IsIntegrityCheck = true;
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
