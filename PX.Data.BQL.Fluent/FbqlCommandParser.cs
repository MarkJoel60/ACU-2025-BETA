// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlCommandParser
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;
using PX.Common.Collection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <summary>
/// Is used by FBQL-commands to build a corresponding BQL-command
/// </summary>
internal static class FbqlCommandParser
{
  private static readonly ReadOnlyBiDictionary<(bool hasJoin, bool hasWhere, bool hasAggregate, bool hasOrder), Type> FeatureSetToCommandTypeMap = ReadOnlyBiDictionaryExt.ToBiDictionary<(bool, bool, bool, bool), Type>((IReadOnlyDictionary<(bool, bool, bool, bool), Type>) new Dictionary<(bool, bool, bool, bool), Type>()
  {
    [(false, false, false, false)] = typeof (Select<>),
    [(false, false, false, true)] = typeof (Select3<,>),
    [(false, false, true, false)] = typeof (Select4<,>),
    [(false, false, true, true)] = typeof (Select6<,,>),
    [(false, true, false, false)] = typeof (Select<,>),
    [(false, true, false, true)] = typeof (Select<,,>),
    [(false, true, true, false)] = typeof (Select4<,,>),
    [(false, true, true, true)] = typeof (Select4<,,,>),
    [(true, false, false, false)] = typeof (Select2<,>),
    [(true, false, false, true)] = typeof (Select3<,,>),
    [(true, false, true, false)] = typeof (Select5<,,>),
    [(true, false, true, true)] = typeof (Select6<,,,>),
    [(true, true, false, false)] = typeof (Select2<,,>),
    [(true, true, false, true)] = typeof (Select2<,,,>),
    [(true, true, true, false)] = typeof (Select5<,,,>),
    [(true, true, true, true)] = typeof (Select5<,,,,>)
  });
  private static readonly ReadOnlyBiDictionary<Type, Type> SelectToSearchTypesMap = ReadOnlyBiDictionaryExt.ToBiDictionary<Type, Type>((IReadOnlyDictionary<Type, Type>) new Dictionary<Type, Type>()
  {
    [typeof (Select<>)] = typeof (Search<>),
    [typeof (Select3<,>)] = typeof (Search3<,>),
    [typeof (Select4<,>)] = typeof (Search4<,>),
    [typeof (Select6<,,>)] = typeof (Search6<,,>),
    [typeof (Select<,>)] = typeof (Search<,>),
    [typeof (Select<,,>)] = typeof (Search<,,>),
    [typeof (Select4<,,>)] = typeof (Search4<,,>),
    [typeof (Select4<,,,>)] = typeof (Search4<,,,>),
    [typeof (Select2<,>)] = typeof (Search2<,>),
    [typeof (Select3<,,>)] = typeof (Search3<,,>),
    [typeof (Select5<,,>)] = typeof (Search5<,,>),
    [typeof (Select6<,,,>)] = typeof (Search6<,,,>),
    [typeof (Select2<,,>)] = typeof (Search2<,,>),
    [typeof (Select2<,,,>)] = typeof (Search2<,,,>),
    [typeof (Select5<,,,>)] = typeof (Search5<,,,>),
    [typeof (Select5<,,,,>)] = typeof (Search5<,,,,>)
  });
  private static readonly ReadOnlyBiDictionary<Type, Type> WhereToOnTypesMap = ReadOnlyBiDictionaryExt.ToBiDictionary<Type, Type>((IReadOnlyDictionary<Type, Type>) new Dictionary<Type, Type>()
  {
    [typeof (Where<>)] = typeof (On<>),
    [typeof (Where<,>)] = typeof (On<,>),
    [typeof (Where<,,>)] = typeof (On<,,>),
    [typeof (Where2<,>)] = typeof (On2<,>)
  });
  private static readonly ReadOnlyBiDictionary<Type, Type> AggregatesMap = ReadOnlyBiDictionaryExt.ToBiDictionary<Type, Type>((IReadOnlyDictionary<Type, Type>) new Dictionary<Type, Type>()
  {
    [typeof (GroupBy<>)] = typeof (GroupBy<,>),
    [typeof (Avg<>)] = typeof (Avg<,>),
    [typeof (Sum<>)] = typeof (Sum<,>),
    [typeof (Min<>)] = typeof (Min<,>),
    [typeof (Max<>)] = typeof (Max<,>),
    [typeof (AggConcat<,>)] = typeof (AggConcat<,,>)
  });
  private static readonly ConcurrentDictionary<FbqlCommandParser.FbqlKey, FbqlParseResult> FbqlToBqlCache = new ConcurrentDictionary<FbqlCommandParser.FbqlKey, FbqlParseResult>();

  public static BqlCommand ConvertSelectToSearch<TField>(Type bqlSelectType) where TField : IBqlField
  {
    Type genericTypeDefinition = bqlSelectType.GetGenericTypeDefinition();
    Type[] genericArguments = bqlSelectType.GetGenericArguments();
    genericArguments[0] = typeof (TField);
    return BqlCommand.CreateInstance(new Type[1]
    {
      FbqlCommandParser.SelectToSearchTypesMap.GetValue(genericTypeDefinition).MakeGenericType(genericArguments)
    });
  }

  /// <summary>
  /// Create a BqlCommand that corresponds to the input configuration.
  /// </summary>
  /// <param name="queryPattern">Pattern of the FBQL query</param>
  /// <param name="table">First table</param>
  /// <param name="joinArray"><see cref="T:PX.Common.TypeArray" /> of <see cref="T:PX.Data.BQL.Fluent.IFbqlJoin" />s</param>
  /// <param name="unaryCondition"><see cref="T:PX.Data.IBqlUnary" /> implementor</param>
  /// <param name="aggregateFunctionArray"><see cref="T:PX.Common.TypeArray" /> of <see cref="T:PX.Data.IBqlFunction" />s</param>
  /// <param name="groupCondition"><see cref="T:PX.Data.IBqlHaving" /> implementor</param>
  /// <param name="sortColumnArray"><see cref="T:PX.Common.TypeArray" /> of <see cref="T:PX.Data.IBqlSortColumn" />s</param>
  public static FbqlParseResult CreateBqlCommand(
    Type queryPattern,
    Type table,
    Type joinArray,
    Type unaryCondition,
    Type aggregateFunctionArray,
    Type groupCondition,
    Type sortColumnArray)
  {
    return FbqlCommandParser.FbqlToBqlCache.GetOrAdd(new FbqlCommandParser.FbqlKey(queryPattern, table, joinArray, unaryCondition, aggregateFunctionArray, groupCondition, sortColumnArray), new Func<FbqlCommandParser.FbqlKey, FbqlParseResult>(FbqlCommandParser.CreateBqlCommand));
  }

  private static FbqlParseResult CreateBqlCommand(FbqlCommandParser.FbqlKey key)
  {
    List<Type> typeList = new List<Type>()
    {
      key.QueryPattern,
      key.Table
    };
    Type[] typeArray1 = TypeArrayOf<IFbqlJoin>.CheckAndExtract(key.JoinArray, (string) null);
    bool hasJoin = ((IEnumerable<Type>) typeArray1).Any<Type>();
    if (hasJoin)
      typeList.Add(FbqlCommandParser.ComposeJoin(typeArray1));
    bool hasWhere = key.Condition != typeof (BqlNone);
    if (hasWhere)
      typeList.Add(FbqlCommandParser.ComposeWhere(key.Condition));
    Type[] typeArray2 = TypeArrayOf<IBqlFunction>.CheckAndExtract(key.GroupByArray, (string) null);
    bool hasAggregate = ((IEnumerable<Type>) typeArray2).Any<Type>();
    if (hasAggregate)
      typeList.Add(FbqlCommandParser.ComposeAggregate(typeArray2, key.GroupCondition));
    Type[] typeArray3 = TypeArrayOf<IBqlSortColumn>.CheckAndExtract(key.OrderByArray, (string) null);
    bool hasOrder = ((IEnumerable<Type>) typeArray3).Any<Type>();
    if (hasOrder)
      typeList.Add(FbqlCommandParser.ComposeOrderBy(typeArray3));
    typeList[0] = FbqlCommandParser.FeatureSetToCommandTypeMap[(hasJoin, hasWhere, hasAggregate, hasOrder)];
    return new FbqlParseResult(BqlCommand.Compose(typeList.ToArray()), hasJoin, hasWhere, hasAggregate, hasOrder);
  }

  private static Type[] EnflatSorts(Type[] sorts)
  {
    List<Type> typeList = new List<Type>();
    foreach (Type sort in sorts)
    {
      if (typeof (IBqlSortColumnDirected).IsAssignableFrom(sort))
      {
        if (typeof (IBqlSortColumnAscending).IsAssignableFrom(sort))
        {
          typeList.Add(typeof (Asc<>).MakeGenericType(sort.BaseType.GetGenericArguments()[0]));
        }
        else
        {
          if (!typeof (IBqlSortColumnDescending).IsAssignableFrom(sort))
            throw new NotSupportedException();
          typeList.Add(typeof (Desc<>).MakeGenericType(sort.BaseType.GetGenericArguments()[0]));
        }
      }
      else
      {
        Type type1 = sort;
        for (Type type2 = sort.IsGenericType ? sort.GetGenericTypeDefinition() : sort; EnumerableExtensions.IsIn<Type>(type2, typeof (Asc<,>), typeof (Desc<,>), typeof (FieldNameAsc<>), typeof (FieldNameDesc<>)); type2 = type1.IsGenericType ? type1.GetGenericTypeDefinition() : type1)
        {
          Type[] genericArguments = type1.GetGenericArguments();
          if (type2 == typeof (Asc<,>))
          {
            typeList.Add(typeof (Asc<>).MakeGenericType(type1.GetGenericArguments()[0]));
            type1 = genericArguments[1];
          }
          else if (type2 == typeof (Desc<,>))
          {
            typeList.Add(typeof (Desc<>).MakeGenericType(type1.GetGenericArguments()[0]));
            type1 = genericArguments[1];
          }
          else if (type2 == typeof (FieldNameAsc<>))
          {
            typeList.Add(typeof (FieldNameAsc));
            type1 = genericArguments[0];
          }
          else
          {
            if (!(type2 == typeof (FieldNameDesc<>)))
              throw new NotSupportedException();
            typeList.Add(typeof (FieldNameDesc));
            type1 = genericArguments[0];
          }
        }
        typeList.Add(type1);
      }
    }
    return typeList.ToArray();
  }

  private static Type ComposeOrderBy(Type[] sorts)
  {
    sorts = FbqlCommandParser.EnflatSorts(sorts);
    if (sorts.Length == 1)
      return typeof (OrderBy<>).MakeGenericType(sorts[0]);
    Type type1 = ((IEnumerable<Type>) sorts).Last<Type>();
    foreach (Type type2 in ((IEnumerable<Type>) sorts).Reverse<Type>().Skip<Type>(1))
    {
      Type type3 = type2.IsGenericType ? type2.GetGenericTypeDefinition() : type2;
      if (type3 == typeof (Asc<>))
        type1 = typeof (Asc<,>).MakeGenericType(type2.GetGenericArguments()[0], type1);
      else if (type3 == typeof (Desc<>))
        type1 = typeof (Desc<,>).MakeGenericType(type2.GetGenericArguments()[0], type1);
      else if (type3 == typeof (FieldNameAsc))
      {
        type1 = typeof (FieldNameAsc<>).MakeGenericType(type1);
      }
      else
      {
        if (!(type3 == typeof (FieldNameDesc)))
          throw new NotSupportedException();
        type1 = typeof (FieldNameDesc<>).MakeGenericType(type1);
      }
    }
    return typeof (OrderBy<>).MakeGenericType(type1);
  }

  private static Type[] EnflatAggregates(Type[] aggregates)
  {
    List<Type> typeList = new List<Type>();
    foreach (Type aggregate in aggregates)
    {
      if (aggregate == typeof (Count))
      {
        typeList.Add(aggregate);
      }
      else
      {
        Type type1 = aggregate;
        for (Type type2 = aggregate.IsGenericType ? aggregate.GetGenericTypeDefinition() : aggregate; EnumerableExtensions.IsIn<Type>(type2, typeof (Avg<,>), typeof (Sum<,>), typeof (Min<,>), typeof (Max<,>), typeof (GroupBy<,>), Array.Empty<Type>()); type2 = type1.IsGenericType ? type1.GetGenericTypeDefinition() : type1)
        {
          Type[] genericArguments = type1.GetGenericArguments();
          typeList.Add(FbqlCommandParser.AggregatesMap.GetKey(type2).MakeGenericType(genericArguments[0]));
          type1 = genericArguments[1];
        }
        typeList.Add(type1);
      }
    }
    return typeList.ToArray();
  }

  private static Type ComposeAggregate(Type[] aggregates, Type groupCondition)
  {
    aggregates = FbqlCommandParser.EnflatAggregates(aggregates);
    if (aggregates.Length == 1)
      return typeof (Aggregate<>).MakeGenericType(aggregates[0]);
    Func<Type, bool> isCount = (Func<Type, bool>) (a => a == typeof (Count) || a.GetGenericTypeDefinition() == typeof (Count<>));
    Type type1 = EnumerableExtensions.FirstOrAny<Type>((IEnumerable<Type>) aggregates, isCount, (Type) null);
    foreach (Type type2 in ((IEnumerable<Type>) aggregates).Reverse<Type>().Skip<Type>(!isCount(type1) ? 1 : 0).Where<Type>((Func<Type, bool>) (a => !isCount(a))))
    {
      Type genericTypeDefinition = type2.GetGenericTypeDefinition();
      Type[] genericArguments = type2.GetGenericArguments();
      switch (genericArguments.Length)
      {
        case 0:
          type1 = FbqlCommandParser.AggregatesMap.GetValue(genericTypeDefinition).MakeGenericType(type1);
          continue;
        case 1:
          type1 = FbqlCommandParser.AggregatesMap.GetValue(genericTypeDefinition).MakeGenericType(genericArguments[0], type1);
          continue;
        case 2:
          type1 = FbqlCommandParser.AggregatesMap.GetValue(genericTypeDefinition).MakeGenericType(genericArguments[0], genericArguments[1], type1);
          continue;
        default:
          continue;
      }
    }
    return groupCondition != typeof (BqlNone) ? typeof (Aggregate<,>).MakeGenericType(type1, groupCondition) : typeof (Aggregate<>).MakeGenericType(type1);
  }

  private static Type ComposeJoin(Type[] fbqlJoins)
  {
    IFbqlJoin[] array = ((IEnumerable<Type>) fbqlJoins).Select<Type, object>(new Func<Type, object>(Activator.CreateInstance)).Cast<IFbqlJoin>().ToArray<IFbqlJoin>();
    if (array.Length == 1)
      return MakeSimpleJoin(array[0]);
    Type type1 = MakeSimpleJoin(((IEnumerable<IFbqlJoin>) array).Last<IFbqlJoin>());
    foreach (IFbqlJoin fbqlJoin in ((IEnumerable<IFbqlJoin>) array).Reverse<IFbqlJoin>().Skip<IFbqlJoin>(1))
    {
      Type type2;
      if (!(fbqlJoin.Condition == (Type) null))
        type2 = fbqlJoin.ChainedJoin.MakeGenericType(fbqlJoin.Table, FbqlCommandParser.ComposeOn(fbqlJoin.Condition), type1);
      else
        type2 = fbqlJoin.ChainedJoin.MakeGenericType(fbqlJoin.Table, type1);
      type1 = type2;
    }
    return type1;

    static Type MakeSimpleJoin(IFbqlJoin join)
    {
      if (join.Table == (Type) null && join.Condition == (Type) null)
        return join.SimpleJoin;
      return !(join.Condition == (Type) null) ? join.SimpleJoin.MakeGenericType(join.Table, FbqlCommandParser.ComposeOn(join.Condition)) : join.SimpleJoin.MakeGenericType(join.Table);
    }
  }

  private static Type ComposeOn(Type binaryArray)
  {
    Type c = ((IEnumerable<Type>) FbqlCommandParser.ComposeWhere(binaryArray).GetGenericArguments()).Single<Type>();
    if (typeof (IBqlWhere).IsAssignableFrom(c) && !typeof (IBqlPlaceholder).IsAssignableFrom(c))
      return FbqlCommandParser.WhereToOnTypesMap.GetValue(c.GetGenericTypeDefinition()).MakeGenericType(c.GetGenericArguments());
    if (!typeof (IBqlUnary).IsAssignableFrom(c))
      throw new NotSupportedException();
    return typeof (On<>).MakeGenericType(ConditionConvertor.TryConvert(c, true));
  }

  private static Type ComposeWhere(Type condition)
  {
    return typeof (Where<>).MakeGenericType(ConditionConvertor.TryConvert(condition, true));
  }

  private static class FbqlToBqlSelectCommandTypesMap
  {
    public static readonly Dictionary<Type, Type> NoJoin = new Dictionary<Type, Type>()
    {
      [typeof (SelectFromBase<,>)] = typeof (Select<>),
      [typeof (SelectFromBase<,>.Order<>)] = typeof (Select3<,>),
      [typeof (SelectFromBase<,>.Aggregate<>)] = typeof (Select4<,>),
      [typeof (SelectFromBase<,>.Aggregate<>.Order<>)] = typeof (Select6<,,>),
      [typeof (SelectFromBase<,>.Aggregate<>.Having<>)] = typeof (Select4<,>),
      [typeof (SelectFromBase<,>.Aggregate<>.Having<>.Order<>)] = typeof (Select6<,,>),
      [typeof (SelectFromBase<,>.Where<>)] = typeof (Select<,>),
      [typeof (SelectFromBase<,>.Where<>.Order<>)] = typeof (Select<,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>)] = typeof (Select4<,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>.Order<>)] = typeof (Select4<,,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>.Having<>)] = typeof (Select4<,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>.Having<>.Order<>)] = typeof (Select4<,,,>)
    };
    public static readonly Dictionary<Type, Type> HasJoin = new Dictionary<Type, Type>()
    {
      [typeof (SelectFromBase<,>)] = typeof (Select2<,>),
      [typeof (SelectFromBase<,>.Order<>)] = typeof (Select3<,,>),
      [typeof (SelectFromBase<,>.Aggregate<>)] = typeof (Select5<,,>),
      [typeof (SelectFromBase<,>.Aggregate<>.Order<>)] = typeof (Select6<,,,>),
      [typeof (SelectFromBase<,>.Aggregate<>.Having<>)] = typeof (Select5<,,>),
      [typeof (SelectFromBase<,>.Aggregate<>.Having<>.Order<>)] = typeof (Select6<,,,>),
      [typeof (SelectFromBase<,>.Where<>)] = typeof (Select2<,,>),
      [typeof (SelectFromBase<,>.Where<>.Order<>)] = typeof (Select2<,,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>)] = typeof (Select5<,,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>.Order<>)] = typeof (Select5<,,,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>.Having<>)] = typeof (Select5<,,,>),
      [typeof (SelectFromBase<,>.Where<>.Aggregate<>.Having<>.Order<>)] = typeof (Select5<,,,,>)
    };
  }

  private class FbqlKey(
    Type queryPattern,
    Type table,
    Type joinArray,
    Type whereBinaryArray,
    Type groupByArray,
    Type groupCondition,
    Type orderByArray) : Tuple<Type, Type, Type, Type, Type, Type, Type>(queryPattern, table, joinArray, whereBinaryArray, groupByArray, groupCondition, orderByArray)
  {
    public Type QueryPattern => this.Item1;

    public Type Table => this.Item2;

    public Type JoinArray => this.Item3;

    public Type Condition => this.Item4;

    public Type GroupByArray => this.Item5;

    public Type GroupCondition => this.Item6;

    public Type OrderByArray => this.Item7;
  }
}
