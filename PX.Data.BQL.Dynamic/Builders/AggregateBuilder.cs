// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Builders.AggregateBuilder
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Data.BQL.Dynamic.Statements;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Builders;

[ImmutableObject(true)]
public class AggregateBuilder
{
  public AggregateStatement Aggregate(
    FieldAggregateStatement field,
    params FieldAggregateStatement[] fields)
  {
    return new AggregateStatement((ChainedAggregateStatement) field, (ChainedAggregateStatement[]) fields);
  }

  public FieldAggregateStatement Group<TField>() => this.Group(typeof (TField));

  public FieldAggregateStatement Group(Type field)
  {
    return new FieldAggregateStatement(AggregateType.Group, field);
  }

  public FieldAggregateStatement Max<TField>() => this.Max(typeof (TField));

  public FieldAggregateStatement Max(Type field)
  {
    return new FieldAggregateStatement(AggregateType.Max, field);
  }

  public FieldAggregateStatement Min<TField>() => this.Min(typeof (TField));

  public FieldAggregateStatement Min(Type field)
  {
    return new FieldAggregateStatement(AggregateType.Min, field);
  }

  public FieldAggregateStatement Avg<TField>() => this.Avg(typeof (TField));

  public FieldAggregateStatement Avg(Type field)
  {
    return new FieldAggregateStatement(AggregateType.Avg, field);
  }

  public FieldAggregateStatement Sum<TField>() => this.Sum(typeof (TField));

  public FieldAggregateStatement Sum(Type field)
  {
    return new FieldAggregateStatement(AggregateType.Sum, field);
  }

  public FieldAggregateStatement Count<TField>() => this.Count(typeof (TField));

  public FieldAggregateStatement Count(Type field)
  {
    return new FieldAggregateStatement(AggregateType.Count, field);
  }

  public FieldAggregateStatement Count()
  {
    return new FieldAggregateStatement(AggregateType.Count, (Type) null);
  }
}
