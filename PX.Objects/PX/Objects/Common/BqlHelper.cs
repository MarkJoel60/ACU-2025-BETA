// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.BqlHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public static class BqlHelper
{
  private static readonly ConcurrentDictionary<Tuple<Type, Type>, bool> ParametersEqualityCache = new ConcurrentDictionary<Tuple<Type, Type>, bool>();
  public static readonly Dictionary<Type, Type> SelectToSearch = new Dictionary<Type, Type>()
  {
    {
      typeof (Select<>),
      typeof (Search<>)
    },
    {
      typeof (Select<,>),
      typeof (Search<,>)
    },
    {
      typeof (Select<,,>),
      typeof (Search<,,>)
    },
    {
      typeof (Select2<,>),
      typeof (Search2<,>)
    },
    {
      typeof (Select2<,,>),
      typeof (Search2<,,>)
    },
    {
      typeof (Select2<,,,>),
      typeof (Search2<,,,>)
    },
    {
      typeof (Select3<,>),
      typeof (Search3<,>)
    },
    {
      typeof (Select3<,,>),
      typeof (Search3<,,>)
    },
    {
      typeof (Select4<,>),
      typeof (Search4<,>)
    },
    {
      typeof (Select4<,,>),
      typeof (Search4<,,>)
    },
    {
      typeof (Select4<,,,>),
      typeof (Search4<,,,>)
    },
    {
      typeof (Select5<,,>),
      typeof (Search5<,,>)
    },
    {
      typeof (Select5<,,,>),
      typeof (Search5<,,,>)
    },
    {
      typeof (Select5<,,,,>),
      typeof (Search5<,,,,>)
    },
    {
      typeof (Select6<,,>),
      typeof (Search6<,,>)
    },
    {
      typeof (Select6<,,,>),
      typeof (Search6<,,,>)
    }
  };

  /// <summary>
  /// Ensures that the first command's parameters have the same type as
  /// the second command's parameters. Can be helpful to keep graph
  /// views and selects inside their delegates synchronized in terms
  /// of BQL parameters.
  /// </summary>
  public static void EnsureParametersEqual(this BqlCommand firstCommand, BqlCommand secondCommand)
  {
    if (firstCommand == null)
      throw new ArgumentNullException(nameof (firstCommand));
    Tuple<Type, Type> key1 = secondCommand != null ? Tuple.Create<Type, Type>(firstCommand.GetSelectType(), secondCommand.GetSelectType()) : throw new ArgumentNullException(nameof (secondCommand));
    Tuple<Type, Type> key2 = Tuple.Create<Type, Type>(key1.Item2, key1.Item1);
    bool flag;
    if (!BqlHelper.ParametersEqualityCache.ContainsKey(key1) && !BqlHelper.ParametersEqualityCache.ContainsKey(key2))
    {
      IBqlParameter[] parameters1 = firstCommand.GetParameters();
      IBqlParameter[] parameters2 = secondCommand.GetParameters();
      flag = parameters1.Length == parameters2.Length && ((IEnumerable<IBqlParameter>) parameters1).Zip<IBqlParameter, IBqlParameter, bool>((IEnumerable<IBqlParameter>) parameters2, (Func<IBqlParameter, IBqlParameter, bool>) ((x, y) => x.GetType() == y.GetType())).All<bool>((Func<bool, bool>) (x => x));
      BqlHelper.ParametersEqualityCache[key1] = flag;
    }
    else
      flag = BqlHelper.ParametersEqualityCache[key1];
    if (!flag)
      throw new PXException("The BQL commands have different parameters.");
  }

  public static IEnumerable<Type> GetDecimalFieldsAggregate<Table>(PXGraph graph) where Table : IBqlTable
  {
    return BqlHelper.GetDecimalFieldsAggregate<Table>(graph, false);
  }

  public static IEnumerable<Type> GetDecimalFieldsAggregate<Table>(PXGraph graph, bool closing) where Table : IBqlTable
  {
    List<Type> decimalFieldsAggregate = new List<Type>();
    PXCache cache = graph.Caches[typeof (Table)];
    if (cache == null)
      throw new PXException("Cache of the {0} type was not found in the graph. Please contact Acumatica support.", new object[1]
      {
        (object) typeof (Table).FullName
      });
    List<Type> list = cache.BqlFields.Where<Type>((Func<Type, bool>) (fieldType => cache.GetAttributesReadonly(cache.GetField(fieldType)).OfType<PXDBDecimalAttribute>().Any<PXDBDecimalAttribute>() || cache.GetAttributesReadonly(cache.GetField(fieldType)).OfType<PXDBCalcedAttribute>().Any<PXDBCalcedAttribute>())).ToList<Type>();
    for (int index = 0; index < list.Count; ++index)
    {
      bool flag = index + 1 == list.Count;
      Type type = list[index];
      decimalFieldsAggregate.Add(closing & flag ? typeof (Sum<>) : typeof (Sum<,>));
      decimalFieldsAggregate.Add(type);
    }
    return (IEnumerable<Type>) decimalFieldsAggregate;
  }

  public static object GetOperandValue(PXCache cache, object item, Type sourceType)
  {
    if (item == null)
      return (object) null;
    if (typeof (IBqlField).IsAssignableFrom(sourceType) && BqlCommand.GetItemType(sourceType).IsAssignableFrom(cache.GetItemType()))
      return cache.GetValue(item, sourceType.Name);
    throw new NotImplementedException();
  }

  public static object GetOperandValue(PXGraph graph, object item, Type sourceType)
  {
    if (item == null)
      return (object) null;
    Type type = typeof (IBqlField).IsAssignableFrom(sourceType) ? BqlCommand.GetItemType(sourceType) : throw new NotImplementedException();
    return BqlHelper.GetOperandValue(graph.Caches[type], item, sourceType);
  }

  public static object GetOperandValue<TOperand>(PXCache cache, object item) where TOperand : IBqlOperand
  {
    return BqlHelper.GetOperandValue(cache, item, typeof (TOperand));
  }

  public static object GetCurrentValue(PXGraph graph, Type sourceType, object row = null)
  {
    Type type = typeof (IBqlField).IsAssignableFrom(sourceType) ? BqlCommand.GetItemType(sourceType) : throw new NotImplementedException();
    PXCache cach = graph.Caches[type];
    return BqlHelper.GetOperandValue(cach, row ?? cach.Current, sourceType);
  }

  public static object GetValuePendingOrRow<TField>(PXCache cache, object row) where TField : IBqlField
  {
    object valuePending = cache.GetValuePending(row, typeof (TField).Name);
    if (valuePending == PXCache.NotSetValue)
      return BqlHelper.GetOperandValue(cache, row, typeof (TField));
    cache.RaiseFieldUpdating<TField>(row, ref valuePending);
    return valuePending;
  }

  public static object GetValuePendingOrCurrent(PXCache cache, Type sourceType, object row)
  {
    object valuePending = cache.GetValuePending(row, sourceType.Name);
    return valuePending != PXCache.NotSetValue ? valuePending : BqlHelper.GetOperandValue(cache, row ?? cache.Current, sourceType);
  }

  public static object GetCurrentValue<TOperand>(PXGraph graph) where TOperand : IBqlOperand
  {
    return BqlHelper.GetCurrentValue(graph, typeof (TOperand));
  }

  public static object GetParameterValue(PXGraph graph, IBqlParameter parameter)
  {
    if (parameter.HasDefault)
    {
      Type referencedType = parameter.GetReferencedType();
      if (referencedType.IsNested)
      {
        Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
          return cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    return (object) null;
  }

  public static Type GetTypeNotStub(Type type)
  {
    return type == typeof (BqlNone) || type == typeof (BqlHelper.fieldStub) ? (Type) null : type;
  }

  public static Type GetTypeNotStub<T>() => BqlHelper.GetTypeNotStub(typeof (T));

  public static void OrderByNew(this PXSelectBase query, IEnumerable<IBqlSortColumn> sortingFields)
  {
    Stack<IBqlSortColumn> source = new Stack<IBqlSortColumn>(sortingFields);
    Type type1 = (Type) null;
    while (source.Any<IBqlSortColumn>())
    {
      IBqlSortColumn ibqlSortColumn = source.Pop();
      if (type1 == (Type) null)
        type1 = ibqlSortColumn.GetType();
      else if (ibqlSortColumn.IsDescending)
        type1 = BqlCommand.Compose(new Type[3]
        {
          typeof (Desc<,>),
          ibqlSortColumn.GetReferencedType(),
          type1
        });
      else
        type1 = BqlCommand.Compose(new Type[3]
        {
          typeof (Asc<,>),
          ibqlSortColumn.GetReferencedType(),
          type1
        });
    }
    Type type2 = BqlCommand.Compose(new Type[2]
    {
      typeof (OrderBy<>),
      type1
    });
    query.View.OrderByNew(type2);
  }

  public abstract class fieldStub : IBqlField, IBqlOperand
  {
  }
}
