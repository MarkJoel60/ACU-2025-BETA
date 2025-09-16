// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.Async.AsyncEnumerableQuery`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree.Async;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.SQLTree.Linq.Async;

internal class AsyncEnumerableQuery<T> : 
  AsyncEnumerableQuery,
  IAsyncQueryable<T>,
  IAsyncEnumerable<T>,
  IAsyncQueryable,
  IQueryable<T>,
  IEnumerable<T>,
  IEnumerable,
  IQueryable,
  IAsyncQueryProvider,
  IQueryProvider
{
  private readonly Expression _asyncExpression;
  private IAsyncEnumerable<T> _asyncEnumerable;
  private readonly Expression _expression;
  private IEnumerable<T> _enumerable;

  public AsyncEnumerableQuery()
    : this((IEnumerable<T>) new T[0])
  {
  }

  /// <summary>
  /// Creates a new asynchronous enumerable sequence represented by the specified expression tree.
  /// </summary>
  /// <param name="expression">The expression tree representing the asynchronous enumerable sequence.</param>
  public AsyncEnumerableQuery(Expression expression)
  {
    this._expression = expression;
    this._asyncExpression = expression;
  }

  public AsyncEnumerableQuery(IAsyncEnumerable<T> enumerable)
  {
    this._asyncEnumerable = enumerable;
    this._asyncExpression = (Expression) Expression.Constant((object) this);
  }

  /// <summary>
  /// Creates a new asynchronous enumerable sequence by wrapping the specified sequence in an expression tree representation.
  /// </summary>
  /// <param name="enumerable">The asynchronous enumerable sequence to represent using an expression tree.</param>
  public AsyncEnumerableQuery(IEnumerable enumerable)
    : this(enumerable.Cast<T>())
  {
  }

  /// <summary>
  /// Creates a new asynchronous enumerable sequence by wrapping the specified sequence in an expression tree representation.
  /// </summary>
  /// <param name="enumerable">The asynchronous enumerable sequence to represent using an expression tree.</param>
  public AsyncEnumerableQuery(IEnumerable<T> enumerable)
  {
    this._enumerable = enumerable;
    this._expression = (Expression) Expression.Constant((object) AsyncEnumerableQuery<T>.CreateQuery(typeof (T), (Expression) Expression.Constant((object) enumerable)));
  }

  /// <summary>Gets the type of the elements in the sequence.</summary>
  System.Type IAsyncQueryable.ElementType => typeof (T);

  /// <inheritdoc />
  IQueryProvider IQueryable.Provider => (IQueryProvider) this;

  /// <inheritdoc />
  System.Type IQueryable.ElementType => typeof (T);

  /// <summary>Gets the expression representing the sequence.</summary>
  Expression IQueryable.Expression => this._expression;

  /// <summary>Gets the expression representing the sequence.</summary>
  Expression IAsyncQueryable.Expression => this._asyncExpression;

  /// <summary>Gets the query provider used to execute the sequence.</summary>
  IAsyncQueryProvider IAsyncQueryable.Provider => (IAsyncQueryProvider) this;

  /// <summary>
  /// Gets the enumerable sequence obtained from evaluating the expression tree.
  /// </summary>
  internal override object Enumerable => (object) this._asyncEnumerable;

  /// <summary>
  /// Gets the expression tree representing the asynchronous enumerable sequence.
  /// </summary>
  internal override Expression Expression => this._asyncExpression;

  /// <inheritdoc />
  public IAsyncEnumerable<TResult> ExecuteCollectionAsync<TResult>(
    Expression expression,
    CancellationToken cancellationToken)
  {
    return Expression.Lambda<Func<IAsyncEnumerable<TResult>>>(new AsyncEnumerableRewriter().Visit(expression), (ParameterExpression[]) null).Compile()();
  }

  private static IQueryable CreateQuery(System.Type elementType, Expression expression)
  {
    return (IQueryable) Activator.CreateInstance(typeof (EnumerableQuery<>).MakeGenericType(elementType), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new object[1]
    {
      (object) expression
    }, (CultureInfo) null);
  }

  /// <inheritdoc />
  public IQueryable CreateQuery(Expression expression)
  {
    return AsyncEnumerableQuery<T>.CreateQuery(expression.Type, expression);
  }

  /// <inheritdoc />
  public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
  {
    return (IQueryable<TElement>) new EnumerableQuery<TElement>(expression);
  }

  private static EnumerableExecutor CreateExecuter(Expression expression)
  {
    return (EnumerableExecutor) Activator.CreateInstance(typeof (EnumerableExecutor<>).MakeGenericType(expression.Type), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new object[1]
    {
      (object) expression
    }, (CultureInfo) null);
  }

  private static Func<EnumerableExecutor, object> ExecuteBoxed { get; } = AsyncEnumerableQuery<T>.GetExecuteBoxed();

  private static Func<EnumerableExecutor, object> GetExecuteBoxed()
  {
    MethodInfo method = typeof (EnumerableExecutor).GetMethod("ExecuteBoxed", BindingFlags.Instance | BindingFlags.NonPublic);
    ExceptionExtensions.ThrowOnNull<MethodInfo>(method, "methodInfo", (string) null);
    return ((Expression<Func<EnumerableExecutor, object>>) (enumerableExecutor => Expression.Call(enumerableExecutor, method))).Compile();
  }

  /// <inheritdoc />
  object IQueryProvider.Execute(Expression expression)
  {
    ExceptionExtensions.ThrowOnNull<Expression>(expression, nameof (expression), (string) null);
    return AsyncEnumerableQuery<T>.ExecuteBoxed(AsyncEnumerableQuery<T>.CreateExecuter(expression));
  }

  /// <inheritdoc />
  TResult IQueryProvider.Execute<TResult>(Expression expression)
  {
    return (TResult) ((IQueryProvider) this).Execute(expression);
  }

  /// <summary>
  /// Creates a new asynchronous enumerable sequence represented by an expression tree.
  /// </summary>
  /// <typeparam name="TElement">The type of the elements in the sequence.</typeparam>
  /// <param name="expression">The expression tree representing the asynchronous enumerable sequence.</param>
  /// <returns>Asynchronous enumerable sequence represented by the specified expression tree.</returns>
  IAsyncQueryable<TElement> IAsyncQueryProvider.CreateAsyncQuery<TElement>(Expression expression)
  {
    return (IAsyncQueryable<TElement>) new AsyncEnumerableQuery<TElement>(expression);
  }

  /// <summary>
  /// Executes an expression tree representing a computation over asynchronous enumerable sequences.
  /// </summary>
  /// <typeparam name="TResult">The type of the result of evaluating the expression tree.</typeparam>
  /// <param name="expression">The expression tree to evaluate.</param>
  /// <param name="token">Cancellation token used to cancel the evaluation.</param>
  /// <returns>Task representing the result of evaluating the specified expression tree.</returns>
  async ValueTask<object> IAsyncQueryProvider.ExecuteAsync<TResult>(
    Expression expression,
    CancellationToken token)
  {
    ExceptionExtensions.ThrowOnNull<Expression>(expression, nameof (expression), (string) null);
    if (!typeof (ValueTask<TResult>).IsAssignableFrom(expression.Type))
      throw new ArgumentException("The specified expression is not assignable to the result type.", nameof (expression));
    return (object) await new AsyncEnumerableExecutor<TResult>(expression).ExecuteAsync(token);
  }

  /// <summary>
  /// Gets an enumerator to enumerate the elements in the sequence.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token used to cancel the enumeration.</param>
  /// <returns>A new enumerator instance used to enumerate the elements in the sequence.</returns>
  public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    if (this._asyncEnumerable == null)
      this._asyncEnumerable = this._enumerable == null ? Expression.Lambda<Func<IAsyncEnumerable<T>>>(new AsyncEnumerableRewriter().Visit(this._asyncExpression ?? this._expression), (ParameterExpression[]) null).Compile()() : AsyncEnumerable.ToAsyncEnumerable<T>(this._enumerable);
    return this._asyncEnumerable.GetAsyncEnumerator(cancellationToken);
  }

  /// <inheritdoc />
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    if (this._enumerable == null)
    {
      if (this._asyncEnumerable != null)
      {
        this._enumerable = AsyncEnumerable.ToEnumerable<T>(this._asyncEnumerable);
      }
      else
      {
        Expression body = new AsyncEnumerableRewriter().Visit(this._expression ?? this._asyncExpression);
        if (body.Type == typeof (IAsyncEnumerable<T>))
        {
          this._asyncEnumerable = Expression.Lambda<Func<IAsyncEnumerable<T>>>(body, (ParameterExpression[]) null).Compile()();
          this._enumerable = AsyncEnumerable.ToEnumerable<T>(this._asyncEnumerable);
        }
        else
          this._enumerable = Expression.Lambda<Func<IEnumerable<T>>>(body, (ParameterExpression[]) null).Compile()();
      }
    }
    return this._enumerable.GetEnumerator();
  }

  /// <inheritdoc />
  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) ((IEnumerable<T>) this).GetEnumerator();

  /// <summary>
  /// Gets a string representation of the enumerable sequence.
  /// </summary>
  /// <returns>String representation of the enumerable sequence.</returns>
  public override string ToString()
  {
    if (!(this._expression is ConstantExpression expression) || expression.Value != this)
      return this._expression.ToString();
    return this._enumerable != null ? this._enumerable.ToString() : "null";
  }
}
