// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.ARCashSaleEntryLevel3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Standalone;
using PX.Objects.CC.Utility;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class ARCashSaleEntryLevel3 : Level3Graph<ARCashSaleEntry, ARCashSale, ARCashSaleEntryLevel3>
{
  public PXAction<ARCashSale> updateL3Data;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable UpdateL3Data(PXAdapter adapter)
  {
    return ((PXSelectBase<ARCashSale>) this.Base.Document).Current != null && ((PXSelectBase<ARCashSale>) this.Base.Document).Current.IsCCPayment.GetValueOrDefault() && this.CollectL3Data(((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current) ? this.UpdateLevel3DataCCPayment(adapter) : adapter.Get();
  }

  public virtual bool CollectL3Data(PX.Objects.Extensions.PaymentTransaction.Payment payment)
  {
    payment.L3Data = new TranProcessingL3DataInput()
    {
      LineItems = new List<TranProcessingL3DataLineItemInput>()
    };
    payment.L3Data.TransactionId = ExternalTranHelper.GetActiveTransaction(this.GetExtTrans())?.TranNumber ?? this.GetExtTrans().First<IExternalTransaction>().TranNumber;
    foreach (PXResult<ARTran> pxResult in ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
      if (arTran.LineType != "DS")
      {
        TranProcessingL3DataLineItemInput dataLineItemInput = new TranProcessingL3DataLineItemInput()
        {
          Description = $"{arTran.LineNbr} {arTran.TranDesc}",
          UnitCost = arTran.CuryUnitPrice.GetValueOrDefault(),
          Quantity = arTran.Qty,
          UnitCode = this.GetL3Code(arTran.UOM),
          DiscountAmount = arTran.CuryDiscAmt,
          DiscountRate = arTran.DiscPct,
          DebitCredit = "D"
        };
        Level3Helper.RetriveInventoryInfo((PXGraph) this.Base, arTran.InventoryID, dataLineItemInput);
        payment.L3Data.LineItems.Add(dataLineItemInput);
      }
    }
    if (payment.L3Data.LineItems.Count == 0)
      return false;
    ARCashSale arCashSale = ARCashSale.PK.Find((PXGraph) this.Base, payment.DocType, payment.RefNbr);
    Level3Helper.FillL3Header(this.Base, payment, arCashSale.CuryTaxTotal.GetValueOrDefault());
    return true;
  }

  protected override PX.Objects.CC.Utility.PaymentMapping GetPaymentMapping()
  {
    return new PX.Objects.CC.Utility.PaymentMapping(typeof (ARCashSale));
  }

  protected override PX.Objects.CC.Utility.ExternalTransactionDetailMapping GetExternalTransactionMapping()
  {
    return new PX.Objects.CC.Utility.ExternalTransactionDetailMapping(typeof (PX.Objects.AR.ExternalTransaction));
  }

  public virtual string GetL3Code(string uomName)
  {
    string str = uomName?.Trim();
    if (str != null)
    {
      switch (str.Length)
      {
        case 2:
          if (str == "KG")
            return "KGM";
          break;
        case 4:
          switch (str[0])
          {
            case 'H':
              if (str == "HOUR")
                return "HUR";
              break;
            case 'P':
              if (str == "PACK")
                return "NMP";
              break;
          }
          break;
        case 5:
          switch (str[0])
          {
            case 'L':
              if (str == "LITER")
                return "LTR";
              break;
            case 'M':
              if (str == "METER")
                return "MTR";
              break;
            case 'P':
              if (str == "PIECE")
                return "PCB";
              break;
          }
          break;
        case 6:
          if (str == "MINUTE")
            return "MIN";
          break;
      }
    }
    return "EA ";
  }
}
