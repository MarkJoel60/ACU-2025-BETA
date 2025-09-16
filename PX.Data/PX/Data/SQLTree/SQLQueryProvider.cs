// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLQueryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Remotion.Linq;
using Remotion.Linq.Clauses.StreamedData;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLQueryProvider : QueryProviderBase
{
  public SQLQueryProvider(System.Type queryableType, IQueryParser queryParser, IQueryExecutor executor)
    : base(queryParser, executor)
  {
    this.CheckQueryableType(queryableType);
    this.QueryableType = queryableType;
  }

  private void CheckQueryableType(System.Type queryableType)
  {
    TypeInfo typeInfo = queryableType.GetTypeInfo();
    if (!typeInfo.IsGenericTypeDefinition)
      throw new ArgumentException($"Expected the generic type definition of an implementation of IQueryable<T>, but was '{queryableType}'.", nameof (queryableType));
    int length = typeInfo.GenericTypeParameters.Length;
    if (length != 1)
      throw new ArgumentException($"Expected the generic type definition of an implementation of IQueryable<T> with exactly one type argument, but found {length} arguments on '{queryableType}.", nameof (queryableType));
  }

  public System.Type QueryableType { get; }

  public virtual IQueryable<T> CreateQuery<T>(Expression expression)
  {
    return (IQueryable<T>) Activator.CreateInstance(this.QueryableType.MakeGenericType(typeof (T)), (object) this, (object) expression);
  }

  public virtual IStreamedData Execute(Expression expression)
  {
    if (this.Executor is SQLinqExecutor executor)
      executor.CurrentExpression = expression;
    return this.GenerateQueryModel(expression).Execute(this.Executor);
  }
}
