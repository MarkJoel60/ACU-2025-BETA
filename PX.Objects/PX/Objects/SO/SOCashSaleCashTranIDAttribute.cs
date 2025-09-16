// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOCashSaleCashTranIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOCashSaleCashTranIDAttribute : CashTranIDAttribute
{
  public override CATran DefaultValues(PXCache sender, CATran catran_Row, object orig_Row)
  {
    SOInvoice soInvoice = (SOInvoice) orig_Row;
    if (!soInvoice.Released.GetValueOrDefault() && soInvoice.CuryPaymentAmt.HasValue)
    {
      Decimal? curyPaymentAmt = soInvoice.CuryPaymentAmt;
      Decimal num = 0M;
      if (!(curyPaymentAmt.GetValueOrDefault() == num & curyPaymentAmt.HasValue))
      {
        catran_Row.OrigModule = "AR";
        catran_Row.OrigTranType = soInvoice.DocType;
        catran_Row.OrigRefNbr = soInvoice.RefNbr;
        catran_Row.ExtRefNbr = soInvoice.ExtRefNbr;
        catran_Row.CashAccountID = soInvoice.CashAccountID;
        catran_Row.CuryInfoID = soInvoice.CuryInfoID;
        catran_Row.CuryID = soInvoice.CuryID;
        switch (soInvoice.DocType)
        {
          case "CSL":
            catran_Row.CuryTranAmt = soInvoice.CuryPaymentAmt;
            catran_Row.DrCr = "D";
            break;
          case "RCS":
            CATran caTran = catran_Row;
            curyPaymentAmt = soInvoice.CuryPaymentAmt;
            Decimal? nullable = curyPaymentAmt.HasValue ? new Decimal?(-curyPaymentAmt.GetValueOrDefault()) : new Decimal?();
            caTran.CuryTranAmt = nullable;
            catran_Row.DrCr = "C";
            break;
          default:
            return (CATran) null;
        }
        catran_Row.TranDate = soInvoice.AdjDate;
        catran_Row.TranDesc = soInvoice.DocDesc;
        CashTranIDAttribute.SetPeriodsByMaster(sender, catran_Row, soInvoice.AdjTranPeriodID);
        catran_Row.ReferenceID = soInvoice.CustomerID;
        catran_Row.Released = new bool?(false);
        catran_Row.Hold = soInvoice.Hold;
        catran_Row.Cleared = soInvoice.Cleared;
        catran_Row.ClearDate = soInvoice.ClearDate;
        return catran_Row;
      }
    }
    return (CATran) null;
  }
}
