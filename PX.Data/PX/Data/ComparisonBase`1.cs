// Decompiled with JetBrains decompiler
// Type: PX.Data.ComparisonBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class ComparisonBase<Operand> : IBqlComparison, IBqlCreator, IBqlVerifier where Operand : IBqlOperand
{
  protected IBqlCreator _operand;
  private readonly string sqlOperator;
  private readonly bool setNonStrict;
  protected PXCollationComparer _collationComparer;

  protected abstract bool? verifyCore(object a, object b);

  protected abstract bool isBypass(object val);

  protected PXCollationComparer collationComparer
  {
    get
    {
      if (this._collationComparer == null)
        this._collationComparer = PXLocalesProvider.CollationComparer;
      return this._collationComparer;
    }
  }

  protected ComparisonBase(string sqlOperator, bool setNonStrict = false, IBqlCreator operand = null)
  {
    this.sqlOperator = sqlOperator;
    this.setNonStrict = setNonStrict;
    this._operand = operand;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    object obj = value;
    bool flag = this.isBypass(obj);
    if (BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out value) && !flag)
    {
      result = this.verifyCore(obj, value);
    }
    else
    {
      result = new bool?();
      value = (object) null;
    }
  }

  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    if (this.setNonStrict && info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
    {
      if (info.BuildExpression)
        exp1 = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (Operand));
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Operand>();
      flag &= this._operand.AppendExpression(ref exp1, graph, info, selection);
    }
    if (!info.BuildExpression)
      return flag;
    int num = (int) SqlDbTypedExpressionHelper.SetExpressionDbType(exp.GetDBType(), exp1 != null ? exp1.GetDBType() : PXDbType.Unspecified, exp as ISQLDBTypedExpression, exp1 as ISQLDBTypedExpression);
    string sqlOperator = this.sqlOperator;
    if (sqlOperator != null)
    {
      switch (sqlOperator.Length)
      {
        case 1:
          switch (sqlOperator[0])
          {
            case '<':
              exp = exp.LT(exp1);
              goto label_29;
            case '=':
              exp = SQLExpressionExt.EQ(exp, exp1);
              goto label_29;
            case '>':
              exp = exp.GT(exp1);
              goto label_29;
          }
          break;
        case 2:
          switch (sqlOperator[0])
          {
            case '<':
              switch (sqlOperator)
              {
                case "<>":
                  exp = SQLExpressionExt.NE(exp, exp1);
                  goto label_29;
                case "<=":
                  exp = exp.LE(exp1);
                  goto label_29;
              }
              break;
            case '>':
              if (sqlOperator == ">=")
              {
                exp = exp.GE(exp1);
                goto label_29;
              }
              break;
          }
          break;
        case 4:
          if (sqlOperator == "LIKE")
          {
            exp = exp.Like(exp1);
            goto label_29;
          }
          break;
        case 8:
          if (sqlOperator == "NOT LIKE")
          {
            exp = exp.NotLike(exp1);
            goto label_29;
          }
          break;
      }
    }
    flag = false;
label_29:
    return flag;
  }

  protected virtual void parseNonFieldOperand(
    PXGraph graph,
    StringBuilder text,
    System.Action parseOperand)
  {
    parseOperand();
  }
}
