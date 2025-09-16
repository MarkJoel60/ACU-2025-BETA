// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.IsAcquiredAsset`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class IsAcquiredAsset<AssetID> : BqlFormulaEvaluator<AssetID>, IBqlOperand where AssetID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    int? par = pars[typeof (AssetID)] as int?;
    if (!par.HasValue)
      return (object) null;
    return (object) (PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Required<FixedAsset.assetID>>, And<FATran.released, Equal<True>>>>.Config>.SelectSingleBound(cache.Graph, new object[0], new object[1]
    {
      (object) par
    })) != null);
  }
}
