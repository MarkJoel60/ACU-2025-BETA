// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.OrderByStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class OrderByStatement : IStatement
{
  public IEnumerable<FieldSortStatement> FieldSorts { get; }

  internal OrderByStatement(FieldSortStatement field, FieldSortStatement[] fields)
    : this(((IEnumerable<FieldSortStatement>) new FieldSortStatement[1]
    {
      field
    }).Concat<FieldSortStatement>((IEnumerable<FieldSortStatement>) fields).ToArray<FieldSortStatement>())
  {
  }

  internal OrderByStatement(FieldSortStatement[] fields)
  {
    this.FieldSorts = (IEnumerable<FieldSortStatement>) ((IEnumerable<FieldSortStatement>) fields).ToArray<FieldSortStatement>();
  }

  internal static OrderByStatement FromRawStatement(Type rawStatement)
  {
    if (rawStatement == (Type) null)
      throw new ArgumentNullException(nameof (rawStatement));
    if (!rawStatement.IsGenericType)
      throw new ArgumentException();
    Type chain;
    if (typeof (OrderBy<>) == rawStatement.GetGenericTypeDefinition())
      chain = ((IEnumerable<Type>) rawStatement.GetGenericArguments()).First<Type>();
    else
      chain = typeof (IBqlFunction).IsAssignableFrom(rawStatement) ? rawStatement : throw new NotSupportedException();
    List<FieldSortStatement> fieldSortStatementList = new List<FieldSortStatement>();
    while (chain != (Type) null)
    {
      FieldSortStatement sort;
      chain = FieldSortStatement.DeconstructChain(chain, out sort);
      fieldSortStatementList.Add(sort);
    }
    return new OrderByStatement(fieldSortStatementList.ToArray());
  }

  public Type Eval()
  {
    return typeof (OrderBy<>).MakeGenericType(((IEnumerable<IChainedStatement>) this.FieldSorts).EvalToChain());
  }
}
