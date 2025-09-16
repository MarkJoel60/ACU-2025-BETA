// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.CuryConvert`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.TX;

public class CuryConvert<CuryAmount, CuryInfoID, ToCuryInfoID> : 
  BqlFormulaEvaluator<CuryAmount, CuryInfoID, ToCuryInfoID>,
  IBqlOperand
  where CuryAmount : IBqlOperand
  where CuryInfoID : IBqlOperand
  where ToCuryInfoID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    Decimal? par1 = (Decimal?) pars[typeof (CuryAmount)];
    long? par2 = (long?) pars[typeof (CuryInfoID)];
    long? par3 = (long?) pars[typeof (ToCuryInfoID)];
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) par2
    }));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) par3
    }));
    if (!par1.HasValue || currencyInfo2 == null || currencyInfo1 == null)
      return (object) 0M;
    if (string.Equals(currencyInfo2.CuryID, currencyInfo1.CuryID))
      return (object) par1;
    Decimal baseval = currencyInfo1.CuryConvBase(par1.Value);
    return (object) currencyInfo2.CuryConvCury(baseval);
  }
}
