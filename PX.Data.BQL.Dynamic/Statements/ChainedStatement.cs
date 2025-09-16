// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.ChainedStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

internal static class ChainedStatement
{
  public static Type EvalToChain(
    this IEnumerable<IChainedStatement> chainedStatements)
  {
    return chainedStatements == null || !chainedStatements.Any<IChainedStatement>() ? (Type) null : chainedStatements.Reverse<IChainedStatement>().Skip<IChainedStatement>(1).Aggregate<IChainedStatement, Type>(chainedStatements.Last<IChainedStatement>().Eval(), (Func<Type, IChainedStatement, Type>) ((current, binary) => binary.Eval(current)));
  }

  public static void VerifyRawStatement<TConstraint>(this Type rawStatement, string parameterName) where TConstraint : class
  {
    if (rawStatement == (Type) null)
      throw new ArgumentNullException(parameterName);
    if (!typeof (TConstraint).IsAssignableFrom(rawStatement))
      throw new ArgumentException("", parameterName);
  }
}
