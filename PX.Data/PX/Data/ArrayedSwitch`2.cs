// Decompiled with JetBrains decompiler
// Type: PX.Data.ArrayedSwitch`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public class ArrayedSwitch<TCases, TDefault> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  ISwitch
  where TCases : ITypeArrayOf<IBqlCase>
  where TDefault : IBqlOperand
{
  private static readonly Lazy<System.Type> SwitchType = Lazy.By<System.Type>((Func<System.Type>) (() => ArrayedSwitch<TCases, TDefault>.ConvertArraySwitchToChainSwitch(typeof (TCases))));
  private readonly IBqlCreator _chainedSwitch;

  public ArrayedSwitch()
  {
    this._chainedSwitch = (IBqlCreator) Activator.CreateInstance(ArrayedSwitch<TCases, TDefault>.SwitchType.Value);
  }

  private static System.Type ConvertArraySwitchToChainSwitch(System.Type arraySwitch)
  {
    System.Type[] source = ArrayedSwitch<TCases, TDefault>.EnflatCases(TypeArrayOf<IBqlCase>.CheckAndExtract(arraySwitch, (string) null));
    System.Type type1 = ((IEnumerable<System.Type>) source).Last<System.Type>();
    System.Type[] genericArguments1 = type1.GetGenericArguments();
    System.Type type2 = type1.GetGenericTypeDefinition().MakeGenericType(ConditionConvertor.TryConvert(genericArguments1[0], true), ConditionConvertor.TryConvert(genericArguments1[1]));
    foreach (System.Type type3 in ((IEnumerable<System.Type>) source).Reverse<System.Type>().Skip<System.Type>(1))
    {
      System.Type genericTypeDefinition = type3.GetGenericTypeDefinition();
      System.Type[] genericArguments2 = type3.GetGenericArguments();
      if (genericTypeDefinition == typeof (Case<,>))
      {
        type2 = typeof (Case<,,>).MakeGenericType(ConditionConvertor.TryConvert(genericArguments2[0], true), ConditionConvertor.TryConvert(genericArguments2[1]), type2);
      }
      else
      {
        if (!(genericTypeDefinition == typeof (Case2<,>)))
          throw new NotSupportedException();
        type2 = typeof (Case2<,,>).MakeGenericType(ConditionConvertor.TryConvert(genericArguments2[0], true), ConditionConvertor.TryConvert(genericArguments2[1]), type2);
      }
    }
    return typeof (TDefault) == typeof (BqlNone) ? typeof (Switch<>).MakeGenericType(type2) : typeof (Switch<,>).MakeGenericType(type2, typeof (TDefault));
  }

  private static System.Type[] EnflatCases(System.Type[] cases)
  {
    List<System.Type> typeList = new List<System.Type>();
    foreach (System.Type type1 in cases)
    {
      System.Type type2 = type1;
      for (System.Type type3 = type1.IsGenericType ? type1.GetGenericTypeDefinition() : type1; EnumerableExtensions.IsIn<System.Type>(type3, typeof (Case<,,>), typeof (Case2<,,>)); type3 = type2.IsGenericType ? type2.GetGenericTypeDefinition() : type2)
      {
        System.Type[] genericArguments = type2.GetGenericArguments();
        if (type3 == typeof (Case<,,>))
        {
          typeList.Add(typeof (Case<,>).MakeGenericType(genericArguments[0], genericArguments[1]));
          type2 = genericArguments[2];
        }
        else
        {
          if (!(type3 == typeof (Case2<,,>)))
            throw new NotSupportedException();
          typeList.Add(typeof (Case2<,>).MakeGenericType(genericArguments[0], genericArguments[1]));
          type2 = genericArguments[2];
        }
      }
      typeList.Add(type2);
    }
    return typeList.ToArray();
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._chainedSwitch.Verify(cache, item, pars, ref result, ref value);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._chainedSwitch.AppendExpression(ref exp, graph, info, selection);
  }

  public System.Type OuterField
  {
    get => !(this._chainedSwitch is ISwitch chainedSwitch) ? (System.Type) null : chainedSwitch.OuterField;
    set
    {
      if (!(this._chainedSwitch is ISwitch chainedSwitch))
        return;
      chainedSwitch.OuterField = value;
    }
  }
}
