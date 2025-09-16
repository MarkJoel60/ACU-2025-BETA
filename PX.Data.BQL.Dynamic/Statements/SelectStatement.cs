// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.SelectStatement
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
public class SelectStatement
{
  private static readonly ReadOnlyBiDictionary<SelectStatement.SelectClassification, Type> Selects = ReadOnlyBiDictionaryExt.ToBiDictionary<SelectStatement.SelectClassification, Type>((IReadOnlyDictionary<SelectStatement.SelectClassification, Type>) new Dictionary<SelectStatement.SelectClassification, Type>()
  {
    [new SelectStatement.SelectClassification(false, false, false, false, false)] = typeof (Select<>),
    [new SelectStatement.SelectClassification(false, false, false, false, true)] = typeof (Select3<,>),
    [new SelectStatement.SelectClassification(false, false, false, true, false)] = typeof (Select4<,>),
    [new SelectStatement.SelectClassification(false, false, false, true, true)] = typeof (Select6<,,>),
    [new SelectStatement.SelectClassification(false, false, true, false, false)] = typeof (Select<,>),
    [new SelectStatement.SelectClassification(false, false, true, false, true)] = typeof (Select<,,>),
    [new SelectStatement.SelectClassification(false, false, true, true, false)] = typeof (Select4<,,>),
    [new SelectStatement.SelectClassification(false, false, true, true, true)] = typeof (Select4<,,,>),
    [new SelectStatement.SelectClassification(false, true, false, false, false)] = typeof (Select2<,>),
    [new SelectStatement.SelectClassification(false, true, false, false, true)] = typeof (Select3<,,>),
    [new SelectStatement.SelectClassification(false, true, false, true, false)] = typeof (Select5<,,>),
    [new SelectStatement.SelectClassification(false, true, false, true, true)] = typeof (Select6<,,,>),
    [new SelectStatement.SelectClassification(false, true, true, false, false)] = typeof (Select2<,,>),
    [new SelectStatement.SelectClassification(false, true, true, false, true)] = typeof (Select2<,,,>),
    [new SelectStatement.SelectClassification(false, true, true, true, false)] = typeof (Select5<,,,>),
    [new SelectStatement.SelectClassification(false, true, true, true, true)] = typeof (Select5<,,,,>),
    [new SelectStatement.SelectClassification(true, false, false, false, false)] = typeof (Search<>),
    [new SelectStatement.SelectClassification(true, false, false, false, true)] = typeof (Search3<,>),
    [new SelectStatement.SelectClassification(true, false, false, true, false)] = typeof (Search4<,>),
    [new SelectStatement.SelectClassification(true, false, false, true, true)] = typeof (Search6<,,>),
    [new SelectStatement.SelectClassification(true, false, true, false, false)] = typeof (Search<,>),
    [new SelectStatement.SelectClassification(true, false, true, false, true)] = typeof (Search<,,>),
    [new SelectStatement.SelectClassification(true, false, true, true, false)] = typeof (Search4<,,>),
    [new SelectStatement.SelectClassification(true, false, true, true, true)] = typeof (Search4<,,,>),
    [new SelectStatement.SelectClassification(true, true, false, false, false)] = typeof (Search2<,>),
    [new SelectStatement.SelectClassification(true, true, false, false, true)] = typeof (Search3<,,>),
    [new SelectStatement.SelectClassification(true, true, false, true, false)] = typeof (Search5<,,>),
    [new SelectStatement.SelectClassification(true, true, false, true, true)] = typeof (Search6<,,,>),
    [new SelectStatement.SelectClassification(true, true, true, false, false)] = typeof (Search2<,,>),
    [new SelectStatement.SelectClassification(true, true, true, false, true)] = typeof (Search2<,,,>),
    [new SelectStatement.SelectClassification(true, true, true, true, false)] = typeof (Search5<,,,>),
    [new SelectStatement.SelectClassification(true, true, true, true, true)] = typeof (Search5<,,,,>)
  });

  internal bool IsSearch { get; }

  internal Type Table { get; }

  internal Type Field { get; }

  internal SelectStatement(Type source, bool isSearch)
  {
    this.IsSearch = isSearch;
    if (isSearch)
    {
      source.VerifyRawStatement<IBqlField>(nameof (source));
      this.Field = source;
      this.Table = BqlCommand.GetItemType(source);
    }
    else
    {
      source.VerifyRawStatement<IBqlTable>(nameof (source));
      this.Table = source;
    }
  }

  internal Type Eval(Type join, Type where, Type aggregate, Type orderBy)
  {
    SelectStatement.SelectClassification selectClassification = new SelectStatement.SelectClassification(this.IsSearch, join != (Type) null, where != (Type) null, aggregate != (Type) null, orderBy != (Type) null);
    Type[] array = EnumerableExtensions.WhereNotNull<Type>((IEnumerable<Type>) new Type[5]
    {
      this.IsSearch ? this.Field : this.Table,
      join,
      where,
      aggregate,
      orderBy
    }).ToArray<Type>();
    return SelectStatement.Selects[selectClassification].MakeGenericType(array);
  }

  internal static BqlBuilder FromRawStatement(Type commandType)
  {
    commandType.VerifyRawStatement<BqlCommand>(nameof (commandType));
    commandType = BqlCommandDecorator.Unwrap(commandType);
    Type genericTypeDefinition = commandType.GetGenericTypeDefinition();
    Type[] genericArguments = commandType.GetGenericArguments();
    if (!SelectStatement.Selects.ContainsValue(genericTypeDefinition))
      throw new NotSupportedException();
    int num = 0;
    Type[] typeArray = genericArguments;
    int index1 = num;
    int index2 = index1 + 1;
    BqlBuilder bqlBuilder = new BqlBuilder(new SelectStatement(typeArray[index1], SelectStatement.Selects[genericTypeDefinition].IsSearch));
    if (SelectStatement.Selects[genericTypeDefinition].HasJoin)
      bqlBuilder.Append(JoinStatement.FromRawStatement(genericArguments[index2++]));
    if (SelectStatement.Selects[genericTypeDefinition].HasWhere)
      bqlBuilder.Set(WhereStatement.FromRawStatement(genericArguments[index2++], true));
    if (SelectStatement.Selects[genericTypeDefinition].HasAggregate)
      bqlBuilder.Append(AggregateStatement.FromRawStatement(genericArguments[index2++]));
    if (SelectStatement.Selects[genericTypeDefinition].HasOrderBy)
      bqlBuilder.Append(OrderByStatement.FromRawStatement(genericArguments[index2]));
    return bqlBuilder;
  }

  private class SelectClassification(
    bool isSearch,
    bool hasJoin,
    bool hasWhere,
    bool hasAggregate,
    bool hasOrderBy) : Tuple<bool, bool, bool, bool, bool>(isSearch, hasJoin, hasWhere, hasAggregate, hasOrderBy)
  {
    public bool IsSearch => this.Item1;

    public bool HasJoin => this.Item2;

    public bool HasWhere => this.Item3;

    public bool HasAggregate => this.Item4;

    public bool HasOrderBy => this.Item5;
  }
}
