// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.CuryBilledAmtFormula
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.SO;

public class CuryBilledAmtFormula : 
  IIf<
  #nullable disable
  Where<ARTran.sOOrderLineOperation, Equal<SOOperation.receipt>, And<ARTran.drCr, Equal<DrCr.credit>, Or<ARTran.sOOrderLineOperation, Equal<SOOperation.issue>, And<ARTran.drCr, Equal<DrCr.debit>>>>>, Minus<BqlOperand<
  #nullable enable
  ARTran.curyTranAmt, IBqlDecimal>.Multiply<
  #nullable disable
  ARTran.sOOrderLineSign>>, BqlOperand<
  #nullable enable
  ARTran.curyTranAmt, IBqlDecimal>.Multiply<
  #nullable disable
  ARTran.sOOrderLineSign>>
{
}
