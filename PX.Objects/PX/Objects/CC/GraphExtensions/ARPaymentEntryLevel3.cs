// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.ARPaymentEntryLevel3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.CC.Utility;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class ARPaymentEntryLevel3 : Level3Graph<ARPaymentEntry, ARPayment, ARPaymentEntryLevel3>
{
  public PXAction<ARPayment> updateL3Data;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable UpdateL3Data(PXAdapter adapter)
  {
    return ((PXSelectBase<ARPayment>) this.Base.Document).Current != null && ((PXSelectBase<ARPayment>) this.Base.Document).Current.IsCCPayment.GetValueOrDefault() && this.CollectL3Data(((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) this.PaymentDoc).Current) ? this.UpdateLevel3DataCCPayment(adapter) : adapter.Get();
  }

  public virtual bool CollectL3Data(PX.Objects.Extensions.PaymentTransaction.Payment payment)
  {
    payment.L3Data = new TranProcessingL3DataInput()
    {
      LineItems = new List<TranProcessingL3DataLineItemInput>()
    };
    Decimal paymentTax = 0M;
    Decimal freighAmount = 0M;
    string currentDocRefNbr = string.Empty;
    EnumerableExtensions.ForEach<PXResult<ARAdjust>>((IEnumerable<PXResult<ARAdjust>>) PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>, InnerJoin<ARTran, On<ARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>, And<ARTran.refNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>>>, Where<ARAdjust.adjgDocType, Equal<Required<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARPayment.refNbr>>, And<ARAdjust.adjdDocType, In3<ARDocType.invoice, ARDocType.finCharge, ARDocType.debitMemo, ARDocType.creditMemo>, And<ARAdjust.voided, NotEqual<True>, And<PX.Objects.AR.ARInvoice.paymentsByLinesAllowed, NotEqual<True>, And<ARTran.curyTranAmt, Greater<decimal0>, And<ARTran.inventoryID, IsNotNull>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) payment.DocType,
      (object) payment.RefNbr
    }), (Action<PXResult<ARAdjust>>) (arApplication =>
    {
      ARAdjust arAdjust = PXResult.Unwrap<ARAdjust>((object) arApplication);
      PX.Objects.AR.ARInvoice arInvoice = PXResult.Unwrap<PX.Objects.AR.ARInvoice>((object) arApplication);
      ARTran arTran = PXResult.Unwrap<ARTran>((object) arApplication);
      Decimal num1 = arAdjust.AdjdCuryRate ?? 1M;
      if (currentDocRefNbr != arInvoice.RefNbr)
      {
        freighAmount += arInvoice.CuryFreightTot.GetValueOrDefault() * num1;
        paymentTax += arInvoice.CuryTaxTotal.GetValueOrDefault() * num1;
        currentDocRefNbr = arInvoice.RefNbr;
      }
      TranProcessingL3DataLineItemInput dataLineItemInput1 = new TranProcessingL3DataLineItemInput();
      dataLineItemInput1.Description = $"{arTran.LineNbr} {arTran.TranDesc}";
      dataLineItemInput1.UnitCost = arTran.CuryUnitPrice.GetValueOrDefault() * num1;
      dataLineItemInput1.Quantity = arTran.Qty;
      dataLineItemInput1.UnitCode = this.GetL3Code(arTran.UOM);
      Decimal? curyDiscAmt = arTran.CuryDiscAmt;
      Decimal num2 = num1;
      dataLineItemInput1.DiscountAmount = curyDiscAmt.HasValue ? new Decimal?(curyDiscAmt.GetValueOrDefault() * num2) : new Decimal?();
      dataLineItemInput1.DiscountRate = arTran.DiscPct;
      dataLineItemInput1.DebitCredit = "D";
      TranProcessingL3DataLineItemInput dataLineItemInput2 = dataLineItemInput1;
      Level3Helper.RetriveInventoryInfo((PXGraph) this.Base, arTran.InventoryID, dataLineItemInput2);
      payment.L3Data.LineItems.Add(dataLineItemInput2);
    }));
    EnumerableExtensions.ForEach<PXResult<SOAdjust>>((IEnumerable<PXResult<SOAdjust>>) PXSelectBase<SOAdjust, PXSelectJoin<SOAdjust, InnerJoin<PX.Objects.SO.SOOrder, On<SOAdjust.FK.Order>, InnerJoin<SOLine, On<SOLine.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<SOLine.orderNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>>>>>, Where<SOAdjust.adjgDocType, Equal<Required<ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Required<ARPayment.refNbr>>, And<SOAdjust.voided, NotEqual<True>, And<SOLine.operation, Equal<SOOperation.issue>, And<PX.Objects.SO.SOOrder.hold, NotEqual<True>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) payment.DocType,
      (object) payment.RefNbr
    }), (Action<PXResult<SOAdjust>>) (soApplication =>
    {
      SOAdjust row = PXResult.Unwrap<SOAdjust>((object) soApplication);
      PX.Objects.SO.SOOrder soOrder = PXResult.Unwrap<PX.Objects.SO.SOOrder>((object) soApplication);
      SOLine soLine = PXResult.Unwrap<SOLine>((object) soApplication);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = MultiCurrencyCalculator.GetCurrencyInfo<SOAdjust.adjdOrigCuryInfoID>((PXGraph) this.Base, (object) row);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = MultiCurrencyCalculator.GetCurrencyInfo<SOAdjust.adjgCuryInfoID>((PXGraph) this.Base, (object) row);
      Decimal? curyRate = currencyInfo1.CuryRate;
      Decimal num3 = curyRate ?? 1M;
      curyRate = currencyInfo2.CuryRate;
      Decimal num4 = curyRate ?? 1M;
      Decimal num5 = num3 / num4;
      if (currentDocRefNbr != soOrder.OrderNbr)
      {
        freighAmount += soOrder.CuryFreightTot.GetValueOrDefault() * num5;
        paymentTax += soOrder.CuryTaxTotal.GetValueOrDefault() * num5;
        currentDocRefNbr = soOrder.OrderNbr;
      }
      TranProcessingL3DataLineItemInput dataLineItemInput3 = new TranProcessingL3DataLineItemInput();
      dataLineItemInput3.Description = $"{soLine.LineNbr} {soLine.TranDesc}";
      dataLineItemInput3.UnitCost = soLine.CuryUnitPrice.GetValueOrDefault() * num5;
      dataLineItemInput3.Quantity = soLine.Qty;
      dataLineItemInput3.UnitCode = this.GetL3Code(soLine.UOM);
      Decimal? curyDiscAmt = soLine.CuryDiscAmt;
      Decimal num6 = num5;
      dataLineItemInput3.DiscountAmount = curyDiscAmt.HasValue ? new Decimal?(curyDiscAmt.GetValueOrDefault() * num6) : new Decimal?();
      dataLineItemInput3.DiscountRate = soLine.DiscPct;
      dataLineItemInput3.DebitCredit = "D";
      TranProcessingL3DataLineItemInput dataLineItemInput4 = dataLineItemInput3;
      Level3Helper.RetriveInventoryInfo((PXGraph) this.Base, soLine.InventoryID, dataLineItemInput4);
      payment.L3Data.LineItems.Add(dataLineItemInput4);
    }));
    if (payment.L3Data.LineItems.Count == 0)
      return false;
    payment.L3Data.FreightAmount = new Decimal?(freighAmount);
    Level3Helper.FillL3Header(this.Base, payment, paymentTax);
    return true;
  }

  protected override PX.Objects.CC.Utility.PaymentMapping GetPaymentMapping()
  {
    return new PX.Objects.CC.Utility.PaymentMapping(typeof (ARPayment));
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
