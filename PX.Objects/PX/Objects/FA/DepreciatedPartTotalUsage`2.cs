// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciatedPartTotalUsage`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class DepreciatedPartTotalUsage<Value, AssetID> : BqlFormulaEvaluator<Value, AssetID>
  where Value : IBqlOperand
  where AssetID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    Decimal? par1 = (Decimal?) pars[typeof (Value)];
    int? par2 = (int?) pars[typeof (AssetID)];
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) par2
    }));
    if (faDetails == null)
      return (object) null;
    Decimal? nullable = par1;
    Decimal? totalExpectedUsage = faDetails.TotalExpectedUsage;
    return (object) (nullable.HasValue & totalExpectedUsage.HasValue ? new Decimal?(nullable.GetValueOrDefault() / totalExpectedUsage.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
  }
}
