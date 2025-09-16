// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.JoinStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common.Collection;
using PX.Data.BQL.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class JoinStatement : IChainedStatement, IStatement
{
  internal readonly Type Table;
  private readonly JoinType _joinType;
  private readonly OnStatement _onStatement;
  private readonly bool _isSingleTable;
  private static readonly ReadOnlyBiDictionary<JoinStatement.JoinClassification, Type> Joins = ReadOnlyBiDictionaryExt.ToBiDictionary<JoinStatement.JoinClassification, Type>((IReadOnlyDictionary<JoinStatement.JoinClassification, Type>) new Dictionary<JoinStatement.JoinClassification, Type>()
  {
    [new JoinStatement.JoinClassification(JoinType.Inner, false, false)] = typeof (InnerJoin<,>),
    [new JoinStatement.JoinClassification(JoinType.Left, false, false)] = typeof (LeftJoin<,>),
    [new JoinStatement.JoinClassification(JoinType.Right, false, false)] = typeof (RightJoin<,>),
    [new JoinStatement.JoinClassification(JoinType.Full, false, false)] = typeof (FullJoin<,>),
    [new JoinStatement.JoinClassification(JoinType.Cross, false, false)] = typeof (CrossJoin<>),
    [new JoinStatement.JoinClassification(JoinType.Inner, false, true)] = typeof (InnerJoin<,,>),
    [new JoinStatement.JoinClassification(JoinType.Left, false, true)] = typeof (LeftJoin<,,>),
    [new JoinStatement.JoinClassification(JoinType.Right, false, true)] = typeof (RightJoin<,,>),
    [new JoinStatement.JoinClassification(JoinType.Full, false, true)] = typeof (FullJoin<,,>),
    [new JoinStatement.JoinClassification(JoinType.Cross, false, true)] = typeof (CrossJoin<,>),
    [new JoinStatement.JoinClassification(JoinType.Inner, true, false)] = typeof (InnerJoinSingleTable<,>),
    [new JoinStatement.JoinClassification(JoinType.Left, true, false)] = typeof (LeftJoinSingleTable<,>),
    [new JoinStatement.JoinClassification(JoinType.Right, true, false)] = typeof (RightJoinSingleTable<,>),
    [new JoinStatement.JoinClassification(JoinType.Full, true, false)] = typeof (FullJoinSingleTable<,>),
    [new JoinStatement.JoinClassification(JoinType.Cross, true, false)] = typeof (CrossJoinSingleTable<>),
    [new JoinStatement.JoinClassification(JoinType.Inner, true, true)] = typeof (InnerJoinSingleTable<,,>),
    [new JoinStatement.JoinClassification(JoinType.Left, true, true)] = typeof (LeftJoinSingleTable<,,>),
    [new JoinStatement.JoinClassification(JoinType.Right, true, true)] = typeof (RightJoinSingleTable<,,>),
    [new JoinStatement.JoinClassification(JoinType.Full, true, true)] = typeof (FullJoinSingleTable<,,>),
    [new JoinStatement.JoinClassification(JoinType.Cross, true, true)] = typeof (CrossJoinSingleTable<,>)
  });

  internal JoinStatement(JoinType type, Type table, OnStatement joinCondition, bool isSingleTable = false)
  {
    table.VerifyRawStatement<IBqlTable>(nameof (table));
    if (joinCondition == null && type != JoinType.Cross)
      throw new ArgumentNullException(nameof (joinCondition));
    this._isSingleTable = isSingleTable;
    this._joinType = type;
    this.Table = table;
    this._onStatement = joinCondition;
  }

  public JoinStatement SingleTable()
  {
    return new JoinStatement(this._joinType, this.Table, this._onStatement, true);
  }

  public static implicit operator Func<JoinBuilder, JoinStatement>(JoinStatement join)
  {
    return (Func<JoinBuilder, JoinStatement>) (jb => join);
  }

  public Type Eval()
  {
    return this._joinType != JoinType.Cross ? JoinStatement.Joins[new JoinStatement.JoinClassification(this._joinType, this._isSingleTable, false)].MakeGenericType(this.Table, this._onStatement.Eval()) : JoinStatement.Joins[new JoinStatement.JoinClassification(JoinType.Cross, this._isSingleTable, false)].MakeGenericType(this.Table);
  }

  public Type Eval(Type next)
  {
    return this._joinType != JoinType.Cross ? JoinStatement.Joins[new JoinStatement.JoinClassification(this._joinType, this._isSingleTable, true)].MakeGenericType(this.Table, this._onStatement.Eval(), next) : JoinStatement.Joins[new JoinStatement.JoinClassification(JoinType.Cross, this._isSingleTable, true)].MakeGenericType(this.Table, next);
  }

  public static IEnumerable<JoinStatement> FromRawStatement(Type rawStatement)
  {
    rawStatement.VerifyRawStatement<IBqlJoin>(nameof (rawStatement));
    List<JoinStatement> joinStatementList = new List<JoinStatement>();
    while (rawStatement != (Type) null)
    {
      JoinStatement joinStatement;
      rawStatement = JoinStatement.DeconstructChain(rawStatement, out joinStatement);
      joinStatementList.Add(joinStatement);
    }
    return (IEnumerable<JoinStatement>) joinStatementList;
  }

  private static Type DeconstructChain(Type chain, out JoinStatement joinStatement)
  {
    Type genericTypeDefinition = chain.GetGenericTypeDefinition();
    Type[] genericArguments = chain.GetGenericArguments();
    if (!JoinStatement.Joins.ContainsValue(genericTypeDefinition))
      throw new NotSupportedException();
    bool flag = JoinStatement.Joins[genericTypeDefinition].JoinType == JoinType.Cross;
    joinStatement = new JoinStatement(JoinStatement.Joins[genericTypeDefinition].JoinType, ((IEnumerable<Type>) genericArguments).First<Type>(), flag ? (OnStatement) null : OnStatement.FromRawStatement(genericArguments[1]), JoinStatement.Joins[genericTypeDefinition].IsSingleTable);
    return !JoinStatement.Joins[genericTypeDefinition].IsChained ? (Type) null : genericArguments[flag ? 1 : 2];
  }

  internal JoinStatement AppendBinary(BinaryStatement binary)
  {
    if (this._joinType == JoinType.Cross)
      throw new InvalidOperationException();
    return new JoinStatement(this._joinType, this.Table, this._onStatement.AppendBinary(binary));
  }

  private class JoinClassification(JoinType joinType, bool isSingleTable, bool isChained) : 
    Tuple<JoinType, bool, bool>(joinType, isSingleTable, isChained)
  {
    public JoinType JoinType => this.Item1;

    public bool IsSingleTable => this.Item2;

    public bool IsChained => this.Item3;
  }
}
