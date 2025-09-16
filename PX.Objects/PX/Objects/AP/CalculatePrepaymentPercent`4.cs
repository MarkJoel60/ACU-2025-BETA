// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.CalculatePrepaymentPercent`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class CalculatePrepaymentPercent<PrepaymentPct, CuryPrepaymentAmt, CuryLineAmt, CuryDiscAmt> : 
  BqlFormulaEvaluator<PrepaymentPct, CuryPrepaymentAmt, CuryLineAmt, CuryDiscAmt>
  where PrepaymentPct : IBqlOperand
  where CuryPrepaymentAmt : IBqlOperand
  where CuryLineAmt : IBqlOperand
  where CuryDiscAmt : IBqlOperand
{
  public override object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> pars)
  {
    Decimal? par1 = (Decimal?) pars[typeof (PrepaymentPct)];
    Decimal? par2 = (Decimal?) pars[typeof (CuryPrepaymentAmt)];
    Decimal? par3 = (Decimal?) pars[typeof (CuryLineAmt)];
    Decimal? par4 = (Decimal?) pars[typeof (CuryDiscAmt)];
    if (par2.HasValue && par3.HasValue && par4.HasValue)
    {
      Decimal? nullable1 = par3;
      Decimal? nullable2 = par4;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num1 = 0M;
      if (!(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue))
      {
        object valuePending = cache.GetValuePending<APTran.curyPrepaymentAmt>(cache.Current);
        if (valuePending == null || valuePending == PXCache.NotSetValue)
          return (object) par1;
        num1 = 100M;
        Decimal? nullable4 = par2;
        Decimal? nullable5 = nullable4.HasValue ? new Decimal?(num1 * nullable4.GetValueOrDefault()) : new Decimal?();
        nullable4 = par3;
        Decimal? nullable6 = par4;
        nullable2 = nullable4.HasValue & nullable6.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = Decimal.Round((nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?()).Value, 6, MidpointRounding.AwayFromZero);
        return (object) (!(0M <= num2) || !(num2 <= 100M) ? par1 : new Decimal?(num2));
      }
    }
    return (object) par1;
  }
}
