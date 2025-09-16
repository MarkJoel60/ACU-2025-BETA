// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.ConditionConvertor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL;

public static class ConditionConvertor
{
  private static readonly IReadOnlyDictionary<System.Type, System.Type> SimpleBinaryToChainedBinaryMap = (IReadOnlyDictionary<System.Type, System.Type>) new Dictionary<System.Type, System.Type>()
  {
    [typeof (PX.Data.And<>)] = typeof (And2<,>),
    [typeof (PX.Data.Or<>)] = typeof (Or2<,>)
  };
  private static readonly IReadOnlyDictionary<System.Type, System.Type> UnaryToComparisonMap = (IReadOnlyDictionary<System.Type, System.Type>) new Dictionary<System.Type, System.Type>()
  {
    [typeof (Where<>)] = typeof (Where<,>),
    [typeof (PX.Data.And<>)] = typeof (And<,>),
    [typeof (PX.Data.Or<>)] = typeof (Or<,>),
    [typeof (On<>)] = typeof (On<,>),
    [typeof (Where2<,>)] = typeof (Where<,,>),
    [typeof (Where2Np<,>)] = typeof (WhereNp<,,>),
    [typeof (And2<,>)] = typeof (And<,,>),
    [typeof (Or2<,>)] = typeof (Or<,,>),
    [typeof (On2<,>)] = typeof (On<,,>)
  };

  public static System.Type TryConvert(System.Type condition, bool topLevel = false)
  {
    if (typeof (BqlCommandDecorator).IsAssignableFrom(condition))
      return BqlCommandDecorator.Unwrap(condition);
    if (condition.IsGenericType && typeof (IShouldBeReplacedWith<>).GenericIsAssignableFrom(condition))
      condition = ((IEnumerable<System.Type>) ((IEnumerable<System.Type>) condition.GetInterfaces()).First<System.Type>((Func<System.Type, bool>) (i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IShouldBeReplacedWith<>))).GetGenericArguments()).First<System.Type>();
    while (typeof (IBqlCustomPredicate).IsAssignableFrom(condition) && !typeof (IDoNotConvert).IsAssignableFrom(condition) && !IsCompare(condition))
      condition = BqlCommand.UnwrapCustomPredicate(condition);
    if (typeof (IBqlChainableCondition).IsAssignableFrom(condition) || typeof (IBqlHavingCondition).IsAssignableFrom(condition))
    {
      System.Type[] source = TypeArrayOf<IBqlBinary>.CheckAndExtract(((IEnumerable<System.Type>) (typeof (IBqlChainableCondition).IsAssignableFrom(condition) ? condition.GetInheritanceChain().TakeWhile<System.Type>((Func<System.Type, bool>) (t => t != typeof (CustomPredicate))).Last<System.Type>() : condition.GetInheritanceChain().TakeWhile<System.Type>((Func<System.Type, bool>) (t => t != typeof (object))).Last<System.Type>()).GetGenericArguments()).Single<System.Type>(), (string) null);
      if (source == null || source.Length == 0 || ((IEnumerable<System.Type>) source).First<System.Type>().GetGenericTypeDefinition() != typeof (PX.Data.And<>))
        throw new InvalidOperationException();
      if (source.Length == 1)
      {
        System.Type type = ((IEnumerable<System.Type>) ((IEnumerable<System.Type>) source).Single<System.Type>().GetGenericArguments()).First<System.Type>();
        return topLevel && IsCompare(type) ? typeof (Where<,>).MakeGenericType(((IEnumerable<System.Type>) type.GetGenericArguments()).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t => ConditionConvertor.TryConvert(t))).ToArray<System.Type>()) : ConditionConvertor.TryConvert(type);
      }
      System.Type type1 = ((IEnumerable<System.Type>) source).Last<System.Type>();
      System.Type type2 = type1.GetGenericTypeDefinition().MakeGenericType(((IEnumerable<System.Type>) type1.GetGenericArguments()).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t => ConditionConvertor.TryConvert(t))).ToArray<System.Type>());
      foreach (System.Type type3 in ((IEnumerable<System.Type>) source).Skip<System.Type>(1).Reverse<System.Type>().Skip<System.Type>(1))
        type2 = ConditionConvertor.SimpleBinaryToChainedBinaryMap[type3.GetGenericTypeDefinition()].MakeGenericType(EnumerableExtensions.Append<System.Type>(((IEnumerable<System.Type>) type3.GetGenericArguments()).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t => ConditionConvertor.TryConvert(t))).ToArray<System.Type>(), type2));
      return typeof (Where2<,>).MakeGenericType(EnumerableExtensions.Append<System.Type>(((IEnumerable<System.Type>) ((IEnumerable<System.Type>) source).First<System.Type>().GetGenericArguments()).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t => ConditionConvertor.TryConvert(t))).ToArray<System.Type>(), type2));
    }
    if (!condition.IsGenericType || typeof (IDoNotConvert).IsAssignableFrom(condition))
      return condition;
    System.Type genericTypeDefinition1 = condition.GetGenericTypeDefinition();
    System.Type[] genericArguments = condition.GetGenericArguments();
    if (typeof (IBqlCast).IsAssignableFrom(condition) || genericTypeDefinition1 == typeof (HavingConditionWrapper<>) || genericTypeDefinition1 == typeof (FunctionWrapper<>) && typeof (IBqlOperand).IsAssignableFrom(genericArguments[0]))
      return ConditionConvertor.TryConvert(genericArguments[0]);
    if (EnumerableExtensions.IsIn<System.Type>(genericTypeDefinition1, typeof (Where<>), typeof (PX.Data.And<>), typeof (PX.Data.Or<>), typeof (On<>)))
    {
      System.Type condition1 = genericArguments[0];
      if (condition1.IsGenericType)
      {
        System.Type genericTypeDefinition2 = condition1.GetGenericTypeDefinition();
        if (genericTypeDefinition2 == typeof (Compare<,>))
          return ConditionConvertor.UnaryToComparisonMap[genericTypeDefinition1].MakeGenericType(((IEnumerable<System.Type>) condition1.GetGenericArguments()).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t => ConditionConvertor.TryConvert(t))).ToArray<System.Type>());
        if (genericTypeDefinition1 == typeof (Where<>) && EnumerableExtensions.IsIn<System.Type>(genericTypeDefinition2, typeof (Where<>), typeof (Where<,>), typeof (Where<,,>), typeof (Where2<,>)))
          return ConditionConvertor.TryConvert(condition1, topLevel);
      }
      return genericTypeDefinition1.MakeGenericType(ConditionConvertor.TryConvert(condition1));
    }
    if (EnumerableExtensions.IsIn<System.Type>(genericTypeDefinition1, typeof (Where<,,>), typeof (WhereNp<,,>), typeof (And<,,>), typeof (Or<,,>), typeof (On<,,>), Array.Empty<System.Type>()))
    {
      System.Type condition2 = genericArguments[0];
      System.Type condition3 = genericArguments[1];
      System.Type condition4 = genericArguments[2];
      return genericTypeDefinition1.MakeGenericType(ConditionConvertor.TryConvert(condition2), ConditionConvertor.TryConvert(condition3), ConditionConvertor.TryConvert(condition4));
    }
    if (!EnumerableExtensions.IsIn<System.Type>(genericTypeDefinition1, typeof (Where2<,>), typeof (Where2Np<,>), typeof (And2<,>), typeof (Or2<,>), typeof (On2<,>), Array.Empty<System.Type>()))
      return genericTypeDefinition1.MakeGenericType(((IEnumerable<System.Type>) genericArguments).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t => ConditionConvertor.TryConvert(t))).ToArray<System.Type>());
    System.Type type4 = genericArguments[0];
    System.Type condition5 = genericArguments[1];
    if (IsCompare(type4))
      return ConditionConvertor.UnaryToComparisonMap[genericTypeDefinition1].MakeGenericType(EnumerableExtensions.Append<System.Type>(((IEnumerable<System.Type>) type4.GetGenericArguments()).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t => ConditionConvertor.TryConvert(t))).ToArray<System.Type>(), ConditionConvertor.TryConvert(condition5)));
    return genericTypeDefinition1.MakeGenericType(ConditionConvertor.TryConvert(type4), ConditionConvertor.TryConvert(condition5));

    static bool IsCompare(System.Type c)
    {
      return c.IsGenericType && c.GetGenericTypeDefinition() == typeof (Compare<,>);
    }
  }
}
