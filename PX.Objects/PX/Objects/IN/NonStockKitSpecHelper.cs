// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NonStockKitSpecHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class NonStockKitSpecHelper
{
  private readonly Dictionary<int, Dictionary<int, Decimal>> _nonStockKitSpecs = new Dictionary<int, Dictionary<int, Decimal>>();
  private readonly PXGraph _graph;

  public NonStockKitSpecHelper(PXGraph graph) => this._graph = graph;

  public bool IsNonStockKit(int? inventoryID)
  {
    return InventoryItem.PK.Find(this._graph, inventoryID).With<InventoryItem, bool>((Func<InventoryItem, bool>) (i =>
    {
      bool? stkItem = i.StkItem;
      bool flag = false;
      return stkItem.GetValueOrDefault() == flag & stkItem.HasValue && i.KitItem.GetValueOrDefault();
    }));
  }

  public IReadOnlyDictionary<int, Decimal> GetNonStockKitSpec(int kitInventoryID)
  {
    Dictionary<int, Decimal> nonStockKitSpec;
    if (!this._nonStockKitSpecs.TryGetValue(kitInventoryID, out nonStockKitSpec))
    {
      IEnumerable<(int?, Decimal)> first = GraphHelper.RowCast<INKitSpecStkDet>((IEnumerable) ((IEnumerable<PXResult<INKitSpecStkDet>>) PXSelectBase<INKitSpecStkDet, PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INKitSpecStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(this._graph, new object[1]
      {
        (object) kitInventoryID
      })).AsEnumerable<PXResult<INKitSpecStkDet>>()).Select<INKitSpecStkDet, (int?, Decimal)>((Func<INKitSpecStkDet, (int?, Decimal)>) (r => (r.CompInventoryID, this.ConvertToBaseQty(r.CompInventoryID, r.UOM, r.DfltCompQty))));
      IEnumerable<(int?, Decimal)> second = GraphHelper.RowCast<INKitSpecNonStkDet>((IEnumerable) ((IEnumerable<PXResult<INKitSpecNonStkDet>>) PXSelectBase<INKitSpecNonStkDet, PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INKitSpecNonStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(this._graph, new object[1]
      {
        (object) kitInventoryID
      })).AsEnumerable<PXResult<INKitSpecNonStkDet>>()).Select<INKitSpecNonStkDet, (int?, Decimal)>((Func<INKitSpecNonStkDet, (int?, Decimal)>) (r => (r.CompInventoryID, this.ConvertToBaseQty(r.CompInventoryID, r.UOM, r.DfltCompQty))));
      Dictionary<int, Dictionary<int, Decimal>> nonStockKitSpecs = this._nonStockKitSpecs;
      int key = kitInventoryID;
      IEnumerable<IGrouping<int, (int?, Decimal)>> source = first.Concat<(int?, Decimal)>(second).GroupBy<(int?, Decimal), int>((Func<(int?, Decimal), int>) (r => r.item.Value));
      Dictionary<int, Decimal> dictionary;
      nonStockKitSpec = dictionary = source.ToDictionary<IGrouping<int, (int?, Decimal)>, int, Decimal>((Func<IGrouping<int, (int?, Decimal)>, int>) (g => g.Key), (Func<IGrouping<int, (int?, Decimal)>, Decimal>) (g => g.Sum<(int?, Decimal)>((Func<(int?, Decimal), Decimal>) (r => r.qty))));
      nonStockKitSpecs[key] = dictionary;
    }
    return (IReadOnlyDictionary<int, Decimal>) nonStockKitSpec;
  }

  protected Decimal ConvertToBaseQty(int? inventoryID, string uom, Decimal? qty)
  {
    return INUnitAttribute.ConvertToBase(((IEnumerable<KeyValuePair<Type, PXCache>>) this._graph.Caches).Select<KeyValuePair<Type, PXCache>, PXCache>((Func<KeyValuePair<Type, PXCache>, PXCache>) (p => p.Value)).First<PXCache>(), inventoryID, uom, qty.GetValueOrDefault(), INPrecision.NOROUND);
  }
}
