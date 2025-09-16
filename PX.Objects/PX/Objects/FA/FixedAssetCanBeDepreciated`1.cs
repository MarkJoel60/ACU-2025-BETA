// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FixedAssetCanBeDepreciated`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FA;

public class FixedAssetCanBeDepreciated<TFieldAssetID> : 
  IBqlWhere,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where TFieldAssetID : 
  #nullable disable
  BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TFieldAssetID>
{
  private readonly IBqlCreator whereCanBeDepreciated;
  private readonly Type assetIDCacheType;

  public FixedAssetCanBeDepreciated()
  {
    this.assetIDCacheType = BqlCommand.GetItemType(typeof (TFieldAssetID));
    this.whereCanBeDepreciated = (IBqlCreator) new Where<FixedAsset.depreciable, Equal<True>, And<FixedAsset.underConstruction, NotEqual<True>>>();
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.whereCanBeDepreciated.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    Type type = item.GetType();
    if (!this.assetIDCacheType.IsAssignableFrom(type))
      throw new PXException("The FixedAssetCanBeDepreciated operator must use a field from the {0} DAC in which it was used, but it uses a field from the {1} DAC.", new object[2]
      {
        (object) type.FullName,
        (object) this.assetIDCacheType.FullName
      });
    if (typeof (FixedAsset).IsAssignableFrom(type) || cache.GetValue<TFieldAssetID>(item) == null)
      ((IBqlVerifier) this.whereCanBeDepreciated).Verify(cache, item, pars, ref result, ref value);
    else
      ((IBqlVerifier) this.whereCanBeDepreciated).Verify(cache.Graph.Caches[typeof (FixedAsset)], (object) PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(cache.Graph, new object[1]
      {
        (object) (int) cache.GetValue<TFieldAssetID>(item)
      })), pars, ref result, ref value);
  }
}
