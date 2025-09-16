// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ASC606Helper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.DR;

internal class ASC606Helper
{
  public static ARReleaseProcess.Amount CalculateSalesPostingAmount(
    PXGraph graph,
    DRSchedule customSchedule)
  {
    ARReleaseProcess.Amount salesPostingAmount1 = new ARReleaseProcess.Amount(new Decimal?(0M), new Decimal?(0M));
    HashSet<ARTran> arTranSet = new HashSet<ARTran>();
    foreach (PXResult<ARTran, PX.Objects.AR.ARRegister, ARTax, PX.Objects.TX.Tax> pxResult in PXSelectBase<ARTran, PXSelectJoin<ARTran, InnerJoin<PX.Objects.AR.ARRegister, On<ARTran.tranType, Equal<PX.Objects.AR.ARRegister.docType>, And<ARTran.refNbr, Equal<PX.Objects.AR.ARRegister.refNbr>>>, LeftJoin<ARTax, On<ARTran.tranType, Equal<ARTax.tranType>, And<ARTran.refNbr, Equal<ARTax.refNbr>, And<ARTran.lineNbr, Equal<ARTax.lineNbr>>>>, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTax.taxID>>>>>, Where<ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<ARTran.deferredCode, IsNull, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) customSchedule.DocType,
      (object) customSchedule.RefNbr
    }))
    {
      ARTran line = PXResult<ARTran, PX.Objects.AR.ARRegister, ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      PX.Objects.AR.ARRegister document = PXResult<ARTran, PX.Objects.AR.ARRegister, ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      ARTax lineTax = PXResult<ARTran, PX.Objects.AR.ARRegister, ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      PX.Objects.TX.Tax salesTax = PXResult<ARTran, PX.Objects.AR.ARRegister, ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      if (!arTranSet.Contains(line))
      {
        ARReleaseProcess.Amount salesPostingAmount2 = ARReleaseProcess.GetSalesPostingAmount(graph, document, line, lineTax, salesTax, (Func<Decimal, Decimal>) (amnt => PXDBCurrencyAttribute.Round(graph.Caches[typeof (ARTran)], (object) line, amnt, CMPrecision.TRANCURY)));
        salesPostingAmount1 += salesPostingAmount2;
        arTranSet.Add(line);
      }
    }
    return salesPostingAmount1;
  }

  public static ARReleaseProcess.Amount CalculateNetAmount(PXGraph graph, PX.Objects.AR.ARRegister document)
  {
    return ASC606Helper.CalculateNetAmountForDeferredLines(graph, document.DocType, document.RefNbr);
  }

  public static ARReleaseProcess.Amount CalculateNetAmount(PXGraph graph, DRSchedule document)
  {
    return ASC606Helper.CalculateNetAmountForDeferredLines(graph, document.DocType, document.RefNbr);
  }

  public static ARReleaseProcess.Amount CalculateNetAmount(
    PXGraph graph,
    PX.Objects.AR.ARInvoice document,
    out Decimal deferredNetDiscountRate,
    out int? defScheduleID)
  {
    return ASC606Helper.CalculateNetAmountForDeferredLines(graph, document.DocType, document.RefNbr, out deferredNetDiscountRate, out defScheduleID, document.CuryDiscTot);
  }

  private static ARReleaseProcess.Amount CalculateNetAmountForDeferredLines(
    PXGraph graph,
    string docType,
    string refNbr)
  {
    Decimal deferredNetDiscountRate = 0M;
    int? defScheduleID = new int?();
    return ASC606Helper.CalculateNetAmountForDeferredLines(graph, docType, refNbr, out deferredNetDiscountRate, out defScheduleID);
  }

  private static ARReleaseProcess.Amount CalculateNetAmountForDeferredLines(
    PXGraph graph,
    string docType,
    string refNbr,
    out Decimal deferredNetDiscountRate,
    out int? defScheduleID,
    Decimal? invoiceCuryDiscTotal = null)
  {
    ARReleaseProcess.Amount forDeferredLines = new ARReleaseProcess.Amount(new Decimal?(0M), new Decimal?(0M));
    Decimal num1 = 0M;
    deferredNetDiscountRate = 0M;
    ARTran arTran = PXResultset<ARTran>.op_Implicit(((PXSelectBase<ARTran>) new PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.defScheduleID, IsNotNull>>>>(graph)).SelectWindowed(0, 1, new object[2]
    {
      (object) docType,
      (object) refNbr
    }));
    defScheduleID = (int?) arTran?.DefScheduleID;
    foreach (PXResult<ARTran> pxResult in PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<ARTran.inventoryID, IsNotNull, And<ARTran.deferredCode, IsNotNull, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) docType,
      (object) refNbr
    }))
    {
      ARTran line = PXResult<ARTran>.op_Implicit(pxResult);
      Func<Decimal, Decimal> func1 = (Func<Decimal, Decimal>) (amount => PXDBCurrencyAttribute.Round(graph.Caches[typeof (ARTran)], (object) line, amount, CMPrecision.TRANCURY));
      Func<Decimal, Decimal> func2 = func1;
      Decimal? nullable1 = line.CuryTranAmt;
      Decimal? nullable2 = line.OrigGroupDiscountRate;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? documentDiscountRate1 = line.OrigDocumentDiscountRate;
      Decimal? nullable4;
      if (!(nullable3.HasValue & documentDiscountRate1.HasValue))
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable3.GetValueOrDefault() * documentDiscountRate1.GetValueOrDefault());
      nullable2 = nullable4;
      Decimal num2 = nullable2.Value;
      Decimal num3 = func2(num2);
      Func<Decimal, Decimal> func3 = func1;
      nullable2 = line.TranAmt;
      nullable1 = line.OrigGroupDiscountRate;
      nullable3 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? documentDiscountRate2 = line.OrigDocumentDiscountRate;
      Decimal? nullable5;
      if (!(nullable3.HasValue & documentDiscountRate2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable5 = nullable1;
      }
      else
        nullable5 = new Decimal?(nullable3.GetValueOrDefault() * documentDiscountRate2.GetValueOrDefault());
      nullable1 = nullable5;
      Decimal num4 = nullable1.Value;
      Decimal num5 = func3(num4);
      Func<Decimal, Decimal> func4 = func1;
      nullable1 = line.CuryTranAmt;
      nullable2 = line.DocumentDiscountRate;
      nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? groupDiscountRate1 = line.GroupDiscountRate;
      Decimal? nullable6;
      if (!(nullable3.HasValue & groupDiscountRate1.HasValue))
      {
        nullable2 = new Decimal?();
        nullable6 = nullable2;
      }
      else
        nullable6 = new Decimal?(nullable3.GetValueOrDefault() * groupDiscountRate1.GetValueOrDefault());
      nullable2 = nullable6;
      Decimal num6 = nullable2.Value;
      Decimal num7 = func4(num6);
      Func<Decimal, Decimal> func5 = func1;
      nullable2 = line.TranAmt;
      nullable1 = line.DocumentDiscountRate;
      nullable3 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? groupDiscountRate2 = line.GroupDiscountRate;
      Decimal? nullable7;
      if (!(nullable3.HasValue & groupDiscountRate2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable7 = nullable1;
      }
      else
        nullable7 = new Decimal?(nullable3.GetValueOrDefault() * groupDiscountRate2.GetValueOrDefault());
      nullable1 = nullable7;
      Decimal num8 = nullable1.Value;
      Decimal num9 = func5(num8);
      nullable3 = line.CuryTaxableAmt;
      Decimal num10 = 0M;
      Decimal? nullable8;
      if (!(nullable3.GetValueOrDefault() == num10 & nullable3.HasValue))
      {
        nullable8 = line.CuryTaxableAmt;
      }
      else
      {
        Decimal num11 = num7 + num3;
        Func<Decimal, Decimal> func6 = func1;
        nullable3 = line.CuryTranAmt;
        Decimal num12 = nullable3.Value;
        Decimal num13 = func6(num12);
        nullable8 = new Decimal?(num11 - num13);
      }
      Decimal? cury = nullable8;
      nullable3 = line.TaxableAmt;
      Decimal num14 = 0M;
      Decimal? nullable9;
      if (!(nullable3.GetValueOrDefault() == num14 & nullable3.HasValue))
      {
        nullable9 = line.TaxableAmt;
      }
      else
      {
        Decimal num15 = num9 + num5;
        Func<Decimal, Decimal> func7 = func1;
        nullable3 = line.TranAmt;
        Decimal num16 = nullable3.Value;
        Decimal num17 = func7(num16);
        nullable9 = new Decimal?(num15 - num17);
      }
      Decimal? baaase = nullable9;
      forDeferredLines += new ARReleaseProcess.Amount(cury, baaase);
      nullable2 = line.CuryTranAmt;
      Decimal num18 = num3;
      Decimal? nullable10 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - num18) : new Decimal?();
      nullable1 = line.CuryTranAmt;
      Decimal? nullable11;
      if (!(nullable10.HasValue & nullable1.HasValue))
      {
        nullable2 = new Decimal?();
        nullable11 = nullable2;
      }
      else
        nullable11 = new Decimal?(nullable10.GetValueOrDefault() + nullable1.GetValueOrDefault());
      nullable3 = nullable11;
      Decimal num19 = num7;
      Decimal? nullable12;
      if (!nullable3.HasValue)
      {
        nullable1 = new Decimal?();
        nullable12 = nullable1;
      }
      else
        nullable12 = new Decimal?(nullable3.GetValueOrDefault() - num19);
      Decimal? nullable13 = nullable12;
      num1 += nullable13.GetValueOrDefault();
    }
    if (invoiceCuryDiscTotal.HasValue)
    {
      Decimal? nullable = invoiceCuryDiscTotal;
      Decimal num20 = 0M;
      if (nullable.GetValueOrDefault() > num20 & nullable.HasValue)
        deferredNetDiscountRate = num1 / invoiceCuryDiscTotal.Value;
    }
    return forDeferredLines;
  }
}
