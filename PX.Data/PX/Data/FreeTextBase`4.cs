// Decompiled with JetBrains decompiler
// Type: PX.Data.FreeTextBase`4
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
public abstract class FreeTextBase<Field, Operand, FieldKey, TopCount> : 
  IBqlUnaryFullText,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier,
  IDoNotParametrize
  where Field : IBqlField
  where Operand : IBqlOperand
  where FieldKey : IBqlField
  where TopCount : IBqlOperand
{
  protected IBqlCreator _operand;
  protected IBqlCreator _top;

  protected abstract string FreeTextOperator { get; }

  protected IBqlCreator ensureOperand()
  {
    return this._operand ?? (this._operand = this._operand.createOperand<Operand>());
  }

  protected IBqlCreator ensureTop()
  {
    IBqlCreator top = this._top;
    if (top != null)
      return top;
    return !(typeof (TopCount) == typeof (BqlNone)) ? (this._top = this._top.createOperand<TopCount>()) : (IBqlCreator) null;
  }

  public System.Type FullTextField => typeof (Field);

  public System.Type OperandValue => typeof (Operand);

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  public IBqlUnary getLikeCounterpart()
  {
    return Activator.CreateInstance(BqlCommand.Compose(typeof (Where<,>), this.FullTextField, typeof (LikeFullTextSubst<,>), this.OperandValue, typeof (TopCount))) as IBqlUnary;
  }

  public IBqlUnary getMatchAgainstCounterpart()
  {
    return Activator.CreateInstance(BqlCommand.Compose(typeof (Where<>), typeof (FullTextMatchAgainst<,,>), this.FullTextField, this.OperandValue, typeof (TopCount))) as IBqlUnary;
  }

  public BqlFullTextRenderingMethod getFullTextSupportMode(
    PXDatabaseProvider pXDatabaseProvider,
    PXGraph graph,
    List<System.Type> tables,
    BqlCommand.Selection selection)
  {
    return BqlUnaryFullTextExtensions.getFullTextSupportMode(this.FullTextField, pXDatabaseProvider, graph, tables, selection);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    string name = typeof (Field).Name;
    BqlFullTextRenderingMethod textRenderingMethod = PXDatabase.Provider.getFullTextRenderingMethod(BqlCommand.FindRealTableForType(info.Tables, BqlCommand.GetItemType(typeof (Field))).Name, name);
    bool flag = true;
    if (BqlFullTextRenderingMethod.NeutralLike == textRenderingMethod)
    {
      SQLExpression sqlExpression = (SQLExpression) null;
      if (info.BuildExpression)
        sqlExpression = BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      SQLExpression exp1 = (SQLExpression) null;
      if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
      {
        if (info.BuildExpression)
          exp1 = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
        if (info.Fields != null)
        {
          info.Fields.Add(typeof (Operand));
          if (info.Fields is BqlCommand.EqualityList fields)
            fields.NonStrict = true;
        }
      }
      else
      {
        this._operand = this._operand.createOperand<Operand>();
        flag &= this._operand.AppendExpression(ref exp1, graph, info, selection);
      }
      if (info.BuildExpression)
        exp = sqlExpression.Like(exp1);
    }
    else
    {
      SQLFullTextSearch sqlFullTextSearch = (SQLFullTextSearch) null;
      if (info.BuildExpression)
        sqlFullTextSearch = new SQLFullTextSearch(this.FreeTextOperator == "FREETEXTTABLE");
      exp = (SQLExpression) sqlFullTextSearch;
      if (info.BuildExpression)
        sqlFullTextSearch.SetSearchField((Column) BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition));
      if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
      {
        if (info.BuildExpression)
          sqlFullTextSearch.SearchValue = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
        if (info.Fields != null)
        {
          info.Fields.Add(typeof (Operand));
          if (info.Fields is BqlCommand.EqualityList fields)
            fields.NonStrict = true;
        }
      }
      else
      {
        this._operand = this._operand.createOperand<Operand>();
        SQLExpression exp2 = (SQLExpression) null;
        flag &= this._operand.AppendExpression(ref exp2, graph, info, selection);
        if (info.BuildExpression)
          sqlFullTextSearch.SearchValue = exp2;
      }
      if (info.BuildExpression)
        sqlFullTextSearch.SetKeyField(BqlCommand.GetSingleExpression(typeof (FieldKey), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition));
      if (this.ensureTop() != null)
      {
        SQLExpression exp3 = (SQLExpression) null;
        this._top.AppendExpression(ref exp3, graph, info, selection);
        if (info.BuildExpression)
          sqlFullTextSearch.SetLimit(exp3);
      }
    }
    return flag;
  }
}
