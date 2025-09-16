// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.AddItemLookup.AlternateIDLookupExt`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Interfaces;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.AddItemLookup;

public abstract class AlternateIDLookupExt<TGraph, TDocument, TLine, TItemInfo, TItemFilter, TUnitField> : 
  SiteStatusLookupExt<TGraph, TDocument, TLine, TItemInfo, TItemFilter>
  where TGraph : PXGraph
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, new()
  where TItemInfo : class, IQtySelectable, IPXSelectable, IBqlTable, IAlternateSelectable, new()
  where TItemFilter : INSiteStatusFilter, IBqlTable, new()
  where TUnitField : class, IBqlField, IImplement<IBqlString>
{
  public PXSetup<PX.Objects.IN.INSetup> INSetup;

  protected override void PrepareSelectedRecord(TItemInfo line)
  {
    bool? barcodesInOrderLines = this.INSetup.Current.ShowBarcodesInOrderLines;
    bool flag = false;
    if (!(barcodesInOrderLines.GetValueOrDefault() == flag & barcodesInOrderLines.HasValue) || !(line.AlternateType == "BAR"))
      return;
    line.AlternateID = (string) null;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<TItemInfo> e)
  {
    TItemFilter current = (TItemFilter) this.Base.Caches<TItemFilter>().Current;
    this.FillAlternateFields(e.Row, current);
  }

  protected virtual void FillAlternateFields(TItemInfo itemInfo, TItemFilter filter)
  {
    if (!string.IsNullOrEmpty(filter.Inventory))
      this.FillAlternateFieldsForInventory(itemInfo);
    else if (!string.IsNullOrEmpty(filter.BarCode))
    {
      this.FillAlternateFieldsForBarCode(itemInfo);
    }
    else
    {
      if (!string.IsNullOrEmpty(itemInfo.AlternateID))
        return;
      this.FillAlternateFieldsForEmptyFilter(itemInfo);
    }
  }

  protected virtual void FillAlternateFieldsForBarCode(TItemInfo itemInfo)
  {
    if (itemInfo.BarCode == null)
      return;
    itemInfo.AlternateType = itemInfo.BarCodeType;
    itemInfo.AlternateID = itemInfo.BarCode;
    itemInfo.AlternateDescr = itemInfo.BarCodeDescr;
  }

  protected virtual void FillAlternateFieldsForInventory(TItemInfo itemInfo)
  {
    if (itemInfo.InventoryAlternateID == null)
      return;
    itemInfo.AlternateType = itemInfo.InventoryAlternateType;
    itemInfo.AlternateID = itemInfo.InventoryAlternateID;
    itemInfo.AlternateDescr = itemInfo.InventoryAlternateDescr;
  }

  protected virtual void FillAlternateFieldsForEmptyFilter(TItemInfo itemInfo)
  {
    INItemXRef inItemXref = PXSelectBase<INItemXRef, PXViewOf<INItemXRef>.BasedOn<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<INItemXRef.inventoryID, Equal<P.AsInt>>>>>.And<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<INItemXRef.uOM, Equal<P.AsString>>>>>.Or<BqlOperand<INItemXRef.uOM, IBqlString>.IsNull>>>>>.Config>.Select((PXGraph) this.Base, (object) itemInfo.InventoryID, this.Base.Caches<TItemInfo>().GetValue<TUnitField>((object) itemInfo)).RowCast<INItemXRef>().AsEnumerable<INItemXRef>().Where<INItemXRef>(new Func<INItemXRef, bool>(this.ScreenSpecificFilter)).OrderBy<INItemXRef, (string, string)>((Func<INItemXRef, (string, string)>) (_ => (_.UOM, _.AlternateType)), (IComparer<(string, string)>) new AlternateIDLookupExt<TGraph, TDocument, TLine, TItemInfo, TItemFilter, TUnitField>.AlternateTypeComparer(this.GetAlternateTypePriority())).FirstOrDefault<INItemXRef>();
    if (inItemXref == null)
      return;
    itemInfo.AlternateType = inItemXref.AlternateType;
    itemInfo.AlternateID = inItemXref.AlternateID;
    itemInfo.AlternateDescr = inItemXref.Descr;
  }

  protected abstract bool ScreenSpecificFilter(INItemXRef xRef);

  protected abstract Dictionary<string, int> GetAlternateTypePriority();

  protected class AlternateTypeComparer : IComparer<(string, string)>
  {
    protected Dictionary<string, int> _alternateTypePriority;

    public AlternateTypeComparer(Dictionary<string, int> alternateTypePriority)
    {
      this._alternateTypePriority = alternateTypePriority;
    }

    public int Compare((string, string) x, (string, string) y)
    {
      int num1;
      int num2;
      int num3 = (this._alternateTypePriority.TryGetValue(x.Item2, out num1) ? num1 : int.MaxValue) - (this._alternateTypePriority.TryGetValue(y.Item2, out num2) ? num2 : int.MaxValue);
      if (num3 != 0)
        return num3;
      bool flag1 = !string.IsNullOrEmpty(x.Item1);
      bool flag2 = !string.IsNullOrEmpty(y.Item1);
      if (!(flag1 ^ flag2))
        return 0;
      return !flag1 ? 1 : -1;
    }
  }
}
