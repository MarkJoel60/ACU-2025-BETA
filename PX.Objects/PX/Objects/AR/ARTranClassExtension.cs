// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranClassExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR;

public static class ARTranClassExtension
{
  public static void ClearInvoiceDetailsBalance(this ARTran tran)
  {
    tran.CuryCashDiscBal = new Decimal?(0M);
    tran.CashDiscBal = new Decimal?(0M);
    tran.CuryRetainedTaxableAmt = new Decimal?(0M);
    tran.RetainedTaxableAmt = new Decimal?(0M);
    tran.CuryRetainedTaxAmt = new Decimal?(0M);
    tran.RetainedTaxAmt = new Decimal?(0M);
    tran.CuryRetainageBal = new Decimal?(0M);
    tran.RetainageBal = new Decimal?(0M);
    tran.CuryOrigRetainageAmt = new Decimal?(0M);
    tran.OrigRetainageAmt = new Decimal?(0M);
    tran.CuryOrigTranAmt = new Decimal?(0M);
    tran.OrigTranAmt = new Decimal?(0M);
    tran.CuryTranBal = new Decimal?(0M);
    tran.TranBal = new Decimal?(0M);
    tran.CuryOrigTaxableAmt = new Decimal?(0M);
    tran.OrigTaxableAmt = new Decimal?(0M);
    tran.CuryOrigTaxAmt = new Decimal?(0M);
    tran.OrigTaxAmt = new Decimal?(0M);
  }

  public static void RecoverInvoiceDetailsBalance(this ARTran tran)
  {
    tran.CuryRetainageBal = tran.CuryOrigRetainageAmt;
    tran.RetainageBal = tran.OrigRetainageAmt;
    tran.CuryTranBal = tran.CuryOrigTranAmt;
    tran.TranBal = tran.OrigTranAmt;
  }

  public static void ClearInvoiceDetailsDiscounts(this ARTran tran)
  {
    tran.CalculateDiscountsOnImport = new bool?(false);
    tran.DiscAmt = new Decimal?(0M);
    tran.CuryDiscAmt = new Decimal?(0M);
    tran.DocumentDiscountAmount = new Decimal?(0M);
    tran.DocumentDiscountRate = new Decimal?((Decimal) 1);
    tran.GroupDiscountAmount = new Decimal?(0M);
    tran.GroupDiscountRate = new Decimal?((Decimal) 1);
    tran.DiscPct = new Decimal?(0M);
    tran.CuryCashDiscBal = new Decimal?(0M);
  }
}
