// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.FieldSortStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common;
using PX.Common.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class FieldSortStatement : IChainedStatement, IStatement
{
  private readonly bool _isFieldNameSort;
  private readonly bool _isDesc;
  private readonly Type _field;
  private static readonly ReadOnlyBiDictionary<FieldSortStatement.SortClassification, Type> Sorts = ReadOnlyBiDictionaryExt.ToBiDictionary<FieldSortStatement.SortClassification, Type>((IReadOnlyDictionary<FieldSortStatement.SortClassification, Type>) new Dictionary<FieldSortStatement.SortClassification, Type>()
  {
    [new FieldSortStatement.SortClassification(false, false)] = typeof (Asc<>),
    [new FieldSortStatement.SortClassification(false, true)] = typeof (Asc<,>),
    [new FieldSortStatement.SortClassification(true, false)] = typeof (Desc<>),
    [new FieldSortStatement.SortClassification(true, true)] = typeof (Desc<,>)
  });
  private static readonly ReadOnlyBiDictionary<FieldSortStatement.SortClassification, Type> FieldNameSorts = ReadOnlyBiDictionaryExt.ToBiDictionary<FieldSortStatement.SortClassification, Type>((IReadOnlyDictionary<FieldSortStatement.SortClassification, Type>) new Dictionary<FieldSortStatement.SortClassification, Type>()
  {
    [new FieldSortStatement.SortClassification(false, false)] = typeof (FieldNameAsc),
    [new FieldSortStatement.SortClassification(false, true)] = typeof (FieldNameAsc<>),
    [new FieldSortStatement.SortClassification(true, false)] = typeof (FieldNameDesc),
    [new FieldSortStatement.SortClassification(true, true)] = typeof (FieldNameDesc<>)
  });

  internal FieldSortStatement(bool fieldNameSort, bool isDesc, Type field)
  {
    if (!fieldNameSort)
      field.VerifyRawStatement<IBqlField>(nameof (field));
    this._isFieldNameSort = fieldNameSort;
    this._isDesc = isDesc;
    this._field = field;
  }

  public Type Eval()
  {
    if (this._isFieldNameSort)
      return FieldSortStatement.FieldNameSorts[new FieldSortStatement.SortClassification(this._isDesc, false)];
    return FieldSortStatement.Sorts[new FieldSortStatement.SortClassification(this._isDesc, false)].MakeGenericType(this._field);
  }

  public Type Eval(Type next)
  {
    return !this._isFieldNameSort ? FieldSortStatement.Sorts[new FieldSortStatement.SortClassification(this._isDesc, true)].MakeGenericType(this._field, next) : FieldSortStatement.FieldNameSorts[new FieldSortStatement.SortClassification(this._isDesc, true)].MakeGenericType(next);
  }

  internal static Type DeconstructChain(Type chain, out FieldSortStatement sort)
  {
    if (EnumerableExtensions.IsIn<Type>(chain, typeof (FieldNameAsc), typeof (FieldNameDesc)))
    {
      sort = new FieldSortStatement(true, chain == typeof (FieldNameDesc), (Type) null);
      return (Type) null;
    }
    Type genericTypeDefinition = chain.GetGenericTypeDefinition();
    Type[] genericArguments = chain.GetGenericArguments();
    if (FieldSortStatement.FieldNameSorts.ContainsValue(genericTypeDefinition))
    {
      sort = new FieldSortStatement(true, FieldSortStatement.FieldNameSorts[genericTypeDefinition].IsDesc, (Type) null);
      return !FieldSortStatement.FieldNameSorts[genericTypeDefinition].IsChained ? (Type) null : genericArguments[0];
    }
    if (!FieldSortStatement.Sorts.ContainsValue(genericTypeDefinition))
      throw new NotSupportedException();
    sort = new FieldSortStatement(false, FieldSortStatement.Sorts[genericTypeDefinition].IsDesc, ((IEnumerable<Type>) genericArguments).First<Type>());
    return !FieldSortStatement.Sorts[genericTypeDefinition].IsChained ? (Type) null : genericArguments[1];
  }

  private class SortClassification(bool isDesc, bool isChained) : Tuple<bool, bool>(isDesc, isChained)
  {
    public bool IsDesc => this.Item1;

    public bool IsChained => this.Item2;
  }
}
