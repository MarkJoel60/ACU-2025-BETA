// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlFormulaEvaluator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// An abstract class that is used to derive custom BQL functions.
/// </summary>
public abstract class BqlFormulaEvaluator : BqlFormula, IBqlCreator, IBqlVerifier, IBqlOperand
{
  protected bool IsExternalCall;

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return true;
  }

  /// <exclude />
  public virtual void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (item is BqlFormula.ItemContainer itemContainer)
      this.IsExternalCall = itemContainer.IsExternalCall;
    Dictionary<System.Type, object> parameters = new Dictionary<System.Type, object>();
    value = this.Evaluate(cache, BqlFormula.ItemContainer.Unwrap(item), parameters);
  }

  /// <exclude />
  public abstract object Evaluate(PXCache cache, object item, Dictionary<System.Type, object> parameters);
}
