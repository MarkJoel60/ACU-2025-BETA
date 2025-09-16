// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.WhereEqualNotNull`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class WhereEqualNotNull<TField, TFieldCurrent> : 
  IBqlWhere,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TField : IBqlOperand
  where TFieldCurrent : IBqlField
{
  private IBqlCreator whereEqual = (IBqlCreator) new Where<TField, Equal<Current2<TFieldCurrent>>>();
  private IBqlCreator whereNull = (IBqlCreator) new Where<Current2<TFieldCurrent>, IsNull>();
  private System.Type cacheType;

  public WhereEqualNotNull() => this.cacheType = typeof (TFieldCurrent).DeclaringType;

  private IBqlCreator GetWhereClause(PXCache cache)
  {
    return cache != null && cache.GetValue<TFieldCurrent>(cache.Current) != null ? this.whereEqual : this.whereNull;
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.GetWhereClause(graph?.Caches[this.cacheType]).AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    ((IBqlVerifier) this.GetWhereClause(cache?.Graph.Caches[this.cacheType])).Verify(cache, item, pars, ref result, ref value);
  }
}
