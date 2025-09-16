// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlCast`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

/// <exclude />
public abstract class BqlCast<TOperand, TBqlType> : 
  BqlOperand<TOperand, TBqlType>,
  IBqlCast,
  IBqlCreator,
  IBqlVerifier
  where TOperand : IBqlOperand
  where TBqlType : class, IBqlDataType
{
  private IBqlCreator _operand;

  void IBqlVerifier.Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    System.Type type = typeof (TOperand);
    value = (object) null;
    if (typeof (IBqlField).IsAssignableFrom(type))
    {
      if (!(cache.GetItemType() == BqlCommand.GetItemType(type)) && !BqlCommand.GetItemType(type).IsAssignableFrom(cache.GetItemType()))
        return;
      if (item is BqlFormula.ItemContainer itemContainer)
        itemContainer.InvolvedFields.Add(type);
      value = cache.GetValue(BqlFormula.ItemContainer.Unwrap(item), type.Name);
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<TOperand>();
      this._operand.Verify(cache, item, pars, ref result, ref value);
    }
  }

  bool IBqlCreator.AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    if (typeof (IBqlCreator).IsAssignableFrom(typeof (TOperand)))
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<TOperand>();
      flag &= this._operand.AppendExpression(ref exp, graph, info, selection);
    }
    else
    {
      if (info.BuildExpression)
        exp = BqlCommand.GetSingleExpression(typeof (TOperand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (TOperand));
    }
    return flag;
  }
}
