// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.SetOfConstants`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL;

public abstract class SetOfConstants<TConstantType, TOperator, TSetProvider> : IBqlConstants
  where TOperator : IBqlComparison
  where TSetProvider : SetOfConstants<TConstantType, TOperator, TSetProvider>.ISetProvider, new()
{
  private static readonly TSetProvider SetProvider = new TSetProvider();

  /// <summary>
  /// Gets the underlying set of <typeparamref name="TConstantType" /> constants.
  /// </summary>
  public static IEnumerable<TConstantType> Set
  {
    get
    {
      return ((IEnumerable<IConstant<TConstantType>>) SetOfConstants<TConstantType, TOperator, TSetProvider>.SetProvider.Constants).Select<IConstant<TConstantType>, TConstantType>((Func<IConstant<TConstantType>, TConstantType>) (t => t.Value)).Cast<TConstantType>().Distinct<TConstantType>();
    }
  }

  /// <summary>
  /// Indicates whether the passed value is present in the current set of constants.
  /// </summary>
  public static bool ContainsValue(TConstantType value)
  {
    return SetOfConstants<TConstantType, TOperator, TSetProvider>.Set.Contains<TConstantType>(value);
  }

  /// <summary>Gets the set of constants</summary>
  /// <returns></returns>
  IEnumerable<object> IBqlConstants.GetValues(PXGraph graph)
  {
    return SetOfConstants<TConstantType, TOperator, TSetProvider>.Set.Cast<object>();
  }

  /// <exclude />
  public interface ISetProvider
  {
    IConstant<TConstantType>[] Constants { get; }
  }

  /// <summary>
  /// Indicates whether the value of the passed field is present in the current set of constants.
  /// </summary>
  public class Contains<TOperand> : 
    BqlChainableConditionLite<SetOfConstants<TConstantType, TOperator, TSetProvider>.Contains<TOperand>>,
    IBqlUnary,
    IBqlCreator,
    IBqlVerifier,
    IBqlCustomPredicate
    where TOperand : IBqlOperand
  {
    private static readonly Lazy<System.Type> WhereType = new Lazy<System.Type>(new Func<System.Type>(SetOfConstants<TConstantType, TOperator, TSetProvider>.Contains<TOperand>.CreateWhereType));
    private readonly Lazy<IBqlUnary> _where = new Lazy<IBqlUnary>((Func<IBqlUnary>) (() => (IBqlUnary) Activator.CreateInstance(SetOfConstants<TConstantType, TOperator, TSetProvider>.Contains<TOperand>.WhereType.Value)));

    private static System.Type CreateWhereType()
    {
      System.Type[] array = ((IEnumerable<IConstant<TConstantType>>) SetOfConstants<TConstantType, TOperator, TSetProvider>.SetProvider.Constants).Select<IConstant<TConstantType>, System.Type>((Func<IConstant<TConstantType>, System.Type>) (c => c.GetType())).ToArray<System.Type>();
      if (!((IEnumerable<System.Type>) array).Any<System.Type>())
        return typeof (Where<True, Equal<False>>);
      if (!typeof (TOperator).IsGenericType || typeof (TOperator).GetGenericArguments().Length != 1)
        throw new InvalidOperationException();
      System.Type operatorDefinition = typeof (TOperator).GetGenericTypeDefinition();
      Func<System.Type, System.Type> func = (Func<System.Type, System.Type>) (constant => operatorDefinition.MakeGenericType(constant));
      if (array.Length == 1)
        return typeof (Where<,>).MakeGenericType(typeof (TOperand), func(((IEnumerable<System.Type>) array).Single<System.Type>()));
      System.Type type1 = typeof (Or<,>).MakeGenericType(typeof (TOperand), func(((IEnumerable<System.Type>) array).Last<System.Type>()));
      foreach (System.Type type2 in ((IEnumerable<System.Type>) array).Skip<System.Type>(1).Reverse<System.Type>().Skip<System.Type>(1))
        type1 = typeof (Or<,,>).MakeGenericType(typeof (TOperand), func(type2), type1);
      return typeof (Where<,,>).MakeGenericType(typeof (TOperand), func(((IEnumerable<System.Type>) array).First<System.Type>()), type1);
    }

    /// <exclude />
    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      this._where.Value.Verify(cache, item, pars, ref result, ref value);
    }

    /// <exclude />
    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return this._where.Value.AppendExpression(ref exp, graph, info, selection);
    }

    IBqlUnary IBqlCustomPredicate.Original => this._where.Value;
  }
}
