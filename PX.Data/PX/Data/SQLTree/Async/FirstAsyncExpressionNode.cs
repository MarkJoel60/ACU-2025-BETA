// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.FirstAsyncExpressionNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.SQLTree.Async;

/// <inheritdoc />
internal class FirstAsyncExpressionNode(
  MethodCallExpressionParseInfo parseInfo,
  LambdaExpression optionalPredicate) : ResultOperatorExpressionNodeBase(parseInfo, optionalPredicate, (LambdaExpression) null)
{
  /// <inheritdoc />
  public virtual Expression Resolve(
    ParameterExpression inputParameter,
    Expression expressionToBeResolved,
    ClauseGenerationContext clauseGenerationContext)
  {
    if (inputParameter == null)
      throw new ArgumentNullException(nameof (inputParameter));
    if (expressionToBeResolved == null)
      throw new ArgumentNullException(nameof (expressionToBeResolved));
    throw ((MethodCallExpressionNodeBase) this).CreateResolveNotSupportedException();
  }

  /// <inheritdoc />
  protected virtual ResultOperatorBase CreateResultOperator(
    ClauseGenerationContext clauseGenerationContext)
  {
    return (ResultOperatorBase) new FirstAsyncResultOperator(this.ParsedExpression.Method.Name.EndsWith("OrDefaultAsync"));
  }
}
