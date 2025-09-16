// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranClassExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AP;

public static class APTranClassExtension
{
  public static void ClearInvoiceDetailsBalance(this APTran tran)
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

  public static void RecoverInvoiceDetailsBalance(this APTran tran)
  {
    tran.CuryRetainageBal = tran.CuryOrigRetainageAmt;
    tran.RetainageBal = tran.OrigRetainageAmt;
    tran.CuryTranBal = tran.CuryOrigTranAmt;
    tran.TranBal = tran.OrigTranAmt;
  }
}
