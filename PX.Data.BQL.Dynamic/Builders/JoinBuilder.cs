// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Builders.JoinBuilder
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Data.BQL.Dynamic.Statements;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Builders;

[ImmutableObject(true)]
public class JoinBuilder
{
  public JoinStatement InnerJoin<TTable>(Func<ConditionBuilder, OnStatement> on) where TTable : IBqlTable
  {
    return this.InnerJoin(typeof (TTable), on);
  }

  public JoinStatement InnerJoin(Type table, Func<ConditionBuilder, OnStatement> on)
  {
    return this.InnerJoin(table, on(BqlBuilder.ConditionBuilder));
  }

  public JoinStatement InnerJoin<TTable>(OnStatement on) where TTable : IBqlTable
  {
    return this.InnerJoin(typeof (TTable), on);
  }

  public JoinStatement InnerJoin(Type table, OnStatement on)
  {
    return new JoinStatement(JoinType.Inner, table, on);
  }

  public JoinStatement LeftJoin<TTable>(Func<ConditionBuilder, OnStatement> on) where TTable : IBqlTable
  {
    return this.LeftJoin(typeof (TTable), on);
  }

  public JoinStatement LeftJoin(Type table, Func<ConditionBuilder, OnStatement> on)
  {
    return this.LeftJoin(table, on(BqlBuilder.ConditionBuilder));
  }

  public JoinStatement LeftJoin<TTable>(OnStatement on) where TTable : IBqlTable
  {
    return this.LeftJoin(typeof (TTable), on);
  }

  public JoinStatement LeftJoin(Type table, OnStatement on)
  {
    return new JoinStatement(JoinType.Left, table, on);
  }

  public JoinStatement RightJoin<TTable>(Func<ConditionBuilder, OnStatement> on) where TTable : IBqlTable
  {
    return this.RightJoin(typeof (TTable), on);
  }

  public JoinStatement RightJoin(Type table, Func<ConditionBuilder, OnStatement> on)
  {
    return this.RightJoin(table, on(BqlBuilder.ConditionBuilder));
  }

  public JoinStatement RightJoin<TTable>(OnStatement on) where TTable : IBqlTable
  {
    return this.RightJoin(typeof (TTable), on);
  }

  public JoinStatement RightJoin(Type table, OnStatement on)
  {
    return new JoinStatement(JoinType.Right, table, on);
  }

  public JoinStatement FullJoin<TTable>(Func<ConditionBuilder, OnStatement> on) where TTable : IBqlTable
  {
    return this.FullJoin(typeof (TTable), on);
  }

  public JoinStatement FullJoin(Type table, Func<ConditionBuilder, OnStatement> on)
  {
    return this.FullJoin(table, on(BqlBuilder.ConditionBuilder));
  }

  public JoinStatement FullJoin<TTable>(OnStatement on) where TTable : IBqlTable
  {
    return this.FullJoin(typeof (TTable), on);
  }

  public JoinStatement FullJoin(Type table, OnStatement on)
  {
    return new JoinStatement(JoinType.Full, table, on);
  }

  public JoinStatement CrossJoin<TTable>() where TTable : IBqlTable
  {
    return this.CrossJoin(typeof (TTable));
  }

  public JoinStatement CrossJoin(Type table)
  {
    return new JoinStatement(JoinType.Cross, table, (OnStatement) null);
  }
}
