// Decompiled with JetBrains decompiler
// Type: PX.Data.InBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class InBase<Operand1> : IBqlComparison, IBqlCreator, IBqlVerifier where Operand1 : IBqlCreator
{
  protected IBqlCreator _operand1;

  protected virtual bool VerifyResultWhenItemIsInList => !this.IsNegative;

  [Obsolete]
  protected abstract string SqlOperator { get; }

  protected abstract bool IsNegative { get; }

  /// <exclude />
  public virtual void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    object obj1 = value;
    object obj2 = (object) null;
    if (typeof (IBqlField).IsAssignableFrom(typeof (Operand1)))
      throw new InvalidOperationException("BQL IN operator can be used only with the Required<T> operand.");
    if (this._operand1 == null)
      this._operand1 = this._operand1.createOperand<Operand1>();
    this._operand1.Verify(cache, item, pars, ref result, ref obj2);
    if (!(obj1 is IComparable))
    {
      result = obj1 == null ? new bool?(false) : new bool?();
    }
    else
    {
      if (!(obj2 is Array array))
        return;
      foreach (object obj3 in array)
      {
        if (obj1.Equals(obj3))
        {
          result = new bool?(this.VerifyResultWhenItemIsInList);
          return;
        }
      }
      result = new bool?(!this.VerifyResultWhenItemIsInList);
    }
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand1)))
      throw new InvalidOperationException("Bql IN operator may be used only with Required<T> operand");
    if (this._operand1 == null)
      this._operand1 = this._operand1.createOperand<Operand1>();
    if (this._operand1 is IBqlSearch operand1)
    {
      BqlCommand.Selection selection1 = new BqlCommand.Selection()
      {
        _Command = this._operand1 as BqlCommand,
        BqlMode = selection != null ? selection.BqlMode : BqlCommand.Selection.BqlParsingMode.Regular,
        ParamCounter = selection != null ? selection.ParamCounter : 0
      };
      selection1.BqlMode |= BqlCommand.Selection.BqlParsingMode.DiscardOrdersInPxSearch;
      BqlCommandInfo info1 = new BqlCommandInfo(false)
      {
        Tables = new List<System.Type>(),
        Parameters = info.Parameters
      };
      Query queryInternal = operand1.GetQueryInternal(graph, info1, selection1);
      if (selection != null)
        selection.ParamCounter = selection1.ParamCounter;
      if (graph != null && info.BuildExpression)
      {
        queryInternal.ClearSelection();
        queryInternal.Field(operand1.GetFieldExpression(graph));
        queryInternal.Limit(-1);
        exp1 = (SQLExpression) new SubQuery(queryInternal);
      }
    }
    else
    {
      flag &= this._operand1.AppendExpression(ref exp1, graph, info, selection);
      if (info.Parameters != null && info.Parameters.Count > 0)
        info.Parameters[info.Parameters.Count - 1].MaskedType = typeof (Array);
    }
    if (info.BuildExpression)
      exp = this.IsNegative ? exp.NotIn(exp1) : exp.In(exp1);
    return flag;
  }
}
