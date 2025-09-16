// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.GetBonusRate`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class GetBonusRate<Date, BonusID> : BqlFormulaEvaluator<Date, BonusID>
  where Date : IBqlOperand
  where BonusID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    DateTime? par1 = (DateTime?) pars[typeof (Date)];
    int? par2 = (int?) pars[typeof (BonusID)];
    if (!par2.HasValue || !par1.HasValue)
      return (object) 0M;
    FABonusDetails faBonusDetails = PXResultset<FABonusDetails>.op_Implicit(PXSelectBase<FABonusDetails, PXSelect<FABonusDetails, Where<FABonusDetails.bonusID, Equal<Required<FABonus.bonusID>>, And<FABonusDetails.startDate, LessEqual<Required<FABookBalance.deprFromDate>>, And<FABonusDetails.endDate, GreaterEqual<Required<FABookBalance.deprFromDate>>>>>>.Config>.Select(cache.Graph, new object[3]
    {
      (object) par2,
      (object) par1,
      (object) par1
    }));
    return (object) (faBonusDetails == null ? new Decimal?(0M) : faBonusDetails.BonusPercent);
  }
}
