// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.CanBeDepreciated`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FA;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
public class CanBeDepreciated<TFieldDepreciated, TFieldUnderConstruction> : 
  IBqlWhere,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TFieldDepreciated : 
  #nullable disable
  BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TFieldDepreciated>
  where TFieldUnderConstruction : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TFieldUnderConstruction>
{
  private readonly IBqlCreator whereEqualNotNull;
  private readonly Type cacheType;

  public CanBeDepreciated()
  {
    this.cacheType = BqlCommand.GetItemType(typeof (TFieldDepreciated));
    this.whereEqualNotNull = (IBqlCreator) new Where<TFieldDepreciated, Equal<True>, And<TFieldUnderConstruction, NotEqual<True>>>();
    if (this.cacheType != BqlCommand.GetItemType(typeof (TFieldUnderConstruction)))
      throw new PXArgumentException();
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.whereEqualNotNull.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    object obj = item;
    if (!this.cacheType.IsAssignableFrom(item.GetType()))
      obj = cache.Graph.Caches[this.cacheType].Current;
    ((IBqlVerifier) this.whereEqualNotNull).Verify(cache, obj, pars, ref result, ref value);
  }
}
