// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.ListLabelOf`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Bql;

public class ListLabelOf<TStringField> : BqlFunction<ListLabelOf<TStringField>.Evaluator, IBqlString>
  where TStringField : IBqlField, IImplement<IBqlString>
{
  public class Evaluator : BqlFormulaEvaluator, IBqlOperand
  {
    public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> parameters)
    {
      return (object) PXStringListAttribute.GetLocalizedLabel<TStringField>(cache, item);
    }
  }
}
