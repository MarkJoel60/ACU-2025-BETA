// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.SelectDepreciationMethod`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class SelectDepreciationMethod<DeprDate, ClassID, BookID, AssetID> : 
  BqlFormulaEvaluator<DeprDate, ClassID, BookID, AssetID>
  where DeprDate : IBqlOperand
  where ClassID : IBqlOperand
  where BookID : IBqlOperand
  where AssetID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    DateTime? par1 = (DateTime?) pars[typeof (DeprDate)];
    int? par2 = (int?) pars[typeof (BookID)];
    int? par3 = (int?) pars[typeof (AssetID)];
    int? par4 = (int?) pars[typeof (ClassID)];
    FADepreciationMethod classMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelectJoin<FADepreciationMethod, LeftJoin<FABookSettings, On<FADepreciationMethod.methodID, Equal<FABookSettings.depreciationMethodID>>>, Where<FABookSettings.assetID, Equal<Required<FABookBalance.classID>>, And<FABookSettings.bookID, Equal<Required<FABookBalance.bookID>>>>>.Config>.Select(cache.Graph, new object[2]
    {
      (object) par4,
      (object) par2
    }));
    if (classMethod != null && classMethod.RecordType == "B")
      return (object) classMethod.MethodCD;
    if (classMethod != null && classMethod.RecordType == "C" && par1.HasValue)
    {
      FADepreciationMethod depreciationMethod = AssetMaint.GetSuitableDepreciationMethod(cache.Graph, classMethod, par1, par2, par3);
      if (depreciationMethod != null)
        return (object) depreciationMethod.MethodCD;
    }
    return (object) null;
  }
}
