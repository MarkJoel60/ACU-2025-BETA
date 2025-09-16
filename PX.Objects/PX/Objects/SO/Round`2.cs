// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Round`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class Round<Field, CuryKeyField> : BqlFormulaEvaluator<Field, CuryKeyField>, IBqlOperand
  where Field : IBqlOperand
  where CuryKeyField : IBqlField
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    Decimal? par = (Decimal?) pars[typeof (Field)];
    return !par.HasValue ? (object) null : (object) PXDBCurrencyAttribute.RoundCury<CuryKeyField>(cache, item, par.Value);
  }
}
