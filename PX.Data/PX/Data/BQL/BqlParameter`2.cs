// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlParameter`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

/// <exclude />
public abstract class BqlParameter<TParameter, TBqlType> : 
  BqlOperand<TParameter, TBqlType>,
  IBqlParameter,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IBqlAggregatedOperand
  where TParameter : IBqlParameter, new()
  where TBqlType : class, IBqlDataType
{
  private readonly Lazy<TParameter> _lazyParameter = new Lazy<TParameter>();

  System.Type IBqlParameter.GetReferencedType() => this._lazyParameter.Value.GetReferencedType();

  bool IBqlParameter.TryDefault => this._lazyParameter.Value.TryDefault;

  bool IBqlParameter.HasDefault => this._lazyParameter.Value.HasDefault;

  bool IBqlParameter.IsVisible => this._lazyParameter.Value.IsVisible;

  bool IBqlParameter.IsArgument => this._lazyParameter.Value.IsArgument;

  System.Type IBqlParameter.MaskedType
  {
    get => this._lazyParameter.Value.MaskedType;
    set => this._lazyParameter.Value.MaskedType = value;
  }

  bool IBqlParameter.NullAllowed
  {
    get => this._lazyParameter.Value.NullAllowed;
    set => this._lazyParameter.Value.NullAllowed = value;
  }

  bool IBqlCreator.AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._lazyParameter.Value.AppendExpression(ref exp, graph, info, selection);
  }

  void IBqlVerifier.Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._lazyParameter.Value.Verify(cache, item, pars, ref result, ref value);
  }
}
