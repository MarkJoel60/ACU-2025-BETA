// Decompiled with JetBrains decompiler
// Type: PX.Data.Concat`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public sealed class Concat<TOperands> : IBqlCreator, IBqlVerifier, IBqlOperand where TOperands : ITypeArrayOf<IBqlOperand>, TypeArray.IsNotEmpty
{
  private readonly System.Type[] _operands = TypeArrayOf<IBqlOperand>.CheckAndExtract(typeof (TOperands), (string) null);

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    foreach (System.Type operand in this._operands)
    {
      IBqlCreator bqlCreator = (IBqlCreator) null;
      ref IBqlCreator local1 = ref bqlCreator;
      PXCache cache1 = cache;
      object obj1 = item;
      List<object> pars1 = pars;
      ref bool? local2 = ref result;
      object obj2;
      ref object local3 = ref obj2;
      if (BqlFunction.getValue(operand, ref local1, cache1, obj1, pars1, ref local2, out local3))
      {
        result = new bool?(true);
        value = (object) (value?.ToString() + (obj2 as string));
      }
    }
    if (result.HasValue)
      return;
    value = (object) null;
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool status = true;
    SQLExpression[] array = ((IEnumerable<System.Type>) this._operands).Select<System.Type, SQLExpression>((Func<System.Type, SQLExpression>) (o =>
    {
      SQLExpression exp1 = (SQLExpression) null;
      if (!typeof (IBqlCreator).IsAssignableFrom(o))
      {
        if (info.BuildExpression)
          exp1 = BqlCommand.GetSingleExpression(o, graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      }
      else
        status &= ((IBqlCreator) null).createOperand(o).AppendExpression(ref exp1, graph, info, selection);
      return exp1;
    })).ToArray<SQLExpression>();
    if (info.BuildExpression)
    {
      PXDbType type = SqlDbTypedExpressionHelper.SetExpressionDbType(((IEnumerable<SQLExpression>) array).Select<SQLExpression, (PXDbType, ISQLDBTypedExpression)>((Func<SQLExpression, (PXDbType, ISQLDBTypedExpression)>) (e => (e.GetDBTypeOrDefault(), e as ISQLDBTypedExpression))).ToArray<(PXDbType, ISQLDBTypedExpression)>());
      exp = ((IEnumerable<SQLExpression>) array).Aggregate<SQLExpression>((Func<SQLExpression, SQLExpression, SQLExpression>) ((acc, ex) => acc.Concat(ex)));
      if (exp is ISQLDBTypedExpression sqldbTypedExpression)
        sqldbTypedExpression.SetDBType(type);
    }
    return status;
  }
}
