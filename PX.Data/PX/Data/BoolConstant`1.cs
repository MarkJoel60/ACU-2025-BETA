// Decompiled with JetBrains decompiler
// Type: PX.Data.BoolConstant`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data;

public abstract class BoolConstant<TSelf>(bool value) : 
  BqlType<IBqlBool, bool>.Constant<
  #nullable disable
  TSelf>(value),
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TSelf : BoolConstant<TSelf>, new()
{
  protected abstract Lazy<IBqlUnary> UnaryLazyImpl { get; }

  private protected override PXDbType DbType { get; } = PXDbType.Bit;

  void IBqlUnary.Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.UnaryLazyImpl.Value.Verify(cache, item, pars, ref result, ref value);
  }
}
