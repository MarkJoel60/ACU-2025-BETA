// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.IsBaseCurrency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CM;

public class IsBaseCurrency : IBqlComparison, IBqlCreator, IBqlVerifier
{
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(CurrencyCollection.IsBaseCurrency((string) value));
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph != null && info.BuildExpression)
    {
      IEnumerable<SQLConst> sqlConsts = CurrencyCollection.GetBaseCurrencies().Select<string, SQLConst>((Func<string, SQLConst>) (_ => new SQLConst((object) _)));
      exp = exp.In((IEnumerable<SQLExpression>) sqlConsts);
    }
    return true;
  }
}
