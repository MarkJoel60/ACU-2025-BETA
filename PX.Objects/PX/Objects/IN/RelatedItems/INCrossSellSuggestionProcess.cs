// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.INCrossSellSuggestionProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.IN.RelatedItems.DAC.Projections;
using PX.Objects.IN.RelatedItems.DAC.Unbound;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

[TableAndChartDashboardType]
public class INCrossSellSuggestionProcess : PXGraph<
#nullable disable
INCrossSellSuggestionProcess>
{
  public PXCancel<INCrossSellSuggestionProcessFilter> Cancel;
  public PXFilter<INCrossSellSuggestionProcessFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<INCrossSellSuggestionProcessResult, INCrossSellSuggestionProcessFilter, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<
  #nullable enable
  INCrossSellSuggestionProcessFilter.originalItemClassID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<Current<
  #nullable enable
  INCrossSellSuggestionProcessFilter.originalItemClassID>, IBqlInt>.IsEqual<
  #nullable disable
  INCrossSellSuggestionProcessResult.originalItemClassID>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<
  #nullable enable
  INCrossSellSuggestionProcessFilter.originalInventoryID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<Current<
  #nullable enable
  INCrossSellSuggestionProcessFilter.originalInventoryID>, IBqlInt>.IsEqual<
  #nullable disable
  INCrossSellSuggestionProcessResult.originalInventoryID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<
  #nullable enable
  INCrossSellSuggestionProcessFilter.crossSellInventoryID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<Current<
  #nullable enable
  INCrossSellSuggestionProcessFilter.crossSellInventoryID>, IBqlInt>.IsEqual<
  #nullable disable
  INCrossSellSuggestionProcessResult.crossSellInventoryID>>>>, OrderBy<Asc<INRelatedInventory.inventoryID, Asc<INRelatedInventory.rank>>>> MLCrossSellSuggestions;

  public virtual void _(
    PX.Data.Events.RowSelected<INCrossSellSuggestionProcessFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INCrossSellSuggestionProcess.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new INCrossSellSuggestionProcess.\u003C\u003Ec__DisplayClass3_0();
    if (e.Row == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.filter = e.Row;
    // ISSUE: reference to a compiler-generated field
    bool flag = cDisplayClass30.filter.Action == "ApproveCrossSellSuggestion";
    PXUIFieldAttribute.SetEnabled<INCrossSellSuggestionProcessResult.uom>(((PXSelectBase) this.MLCrossSellSuggestions).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<INCrossSellSuggestionProcessResult.qty>(((PXSelectBase) this.MLCrossSellSuggestions).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<INCrossSellSuggestionProcessResult.effectiveDate>(((PXSelectBase) this.MLCrossSellSuggestions).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<INCrossSellSuggestionProcessResult.expirationDate>(((PXSelectBase) this.MLCrossSellSuggestions).Cache, (object) null, flag);
    // ISSUE: method pointer
    ((PXProcessingBase<INCrossSellSuggestionProcessResult>) this.MLCrossSellSuggestions).SetProcessDelegate<INCrossSellSuggestionProcess>(new PXProcessingBase<INCrossSellSuggestionProcessResult>.ProcessItemDelegate<INCrossSellSuggestionProcess>((object) cDisplayClass30, __methodptr(\u003C_\u003Eb__0)));
  }

  protected virtual void ProcessCrossSellMLSuggestions(
    INCrossSellSuggestionProcessResult item,
    string action)
  {
    switch (action)
    {
      case "ApproveCrossSellSuggestion":
        this.ApproveCrossSellSuggestion(item);
        break;
      case "DeleteCrossSellSuggestion":
        this.DeleteCrossSellSuggestion(item);
        break;
      default:
        throw new NotImplementedException();
    }
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<INCrossSellSuggestionProcessFilter.originalItemClassID> e)
  {
    if (e.Row == null && e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<INCrossSellSuggestionProcessFilter.originalItemClassID>, object, object>) e).OldValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INCrossSellSuggestionProcessFilter.originalItemClassID>>) e).Cache.SetValueExt<INCrossSellSuggestionProcessFilter.originalInventoryID>(e.Row, (object) null);
  }

  protected virtual void ApproveCrossSellSuggestion(INCrossSellSuggestionProcessResult item)
  {
    InventoryItemMaintBase inventoryItemMaintBase = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, item.OriginalInventoryID).StkItem.GetValueOrDefault() ? (InventoryItemMaintBase) PXGraph.CreateInstance<InventoryItemMaint>() : (InventoryItemMaintBase) PXGraph.CreateInstance<NonStockItemMaint>();
    RelatedItemsTab<InventoryItemMaintBase> implementation = ((PXGraph) inventoryItemMaintBase).FindImplementation<RelatedItemsTab<InventoryItemMaintBase>>();
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) inventoryItemMaintBase.Item).Current = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) inventoryItemMaintBase.Item).Search<PX.Objects.IN.InventoryItem.inventoryID>((object) item.OriginalInventoryID, Array.Empty<object>()));
    INRelatedInventory relatedInventory = PXResultset<INRelatedInventory>.op_Implicit(((PXSelectBase<INRelatedInventory>) implementation.RelatedItems).Search<INRelatedInventory.inventoryID, INRelatedInventory.lineID>((object) item.OriginalInventoryID, (object) item.LineID, Array.Empty<object>()));
    ((PXSelectBase<INRelatedInventory>) implementation.RelatedItems).Current = relatedInventory;
    relatedInventory.UOM = item.UOM;
    relatedInventory.Qty = item.Qty;
    relatedInventory.AcceptedMLSuggestion = new bool?(true);
    ((PXSelectBase<INRelatedInventory>) implementation.RelatedItems).Update(relatedInventory);
    relatedInventory.EffectiveDate = item.EffectiveDate;
    relatedInventory.ExpirationDate = item.ExpirationDate;
    ((PXSelectBase<INRelatedInventory>) implementation.RelatedItems).Update(relatedInventory);
    ((PXAction) inventoryItemMaintBase.Save).Press();
  }

  protected virtual void DeleteCrossSellSuggestion(INCrossSellSuggestionProcessResult item)
  {
    InventoryItemMaintBase inventoryItemMaintBase = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, item.OriginalInventoryID).StkItem.GetValueOrDefault() ? (InventoryItemMaintBase) PXGraph.CreateInstance<InventoryItemMaint>() : (InventoryItemMaintBase) PXGraph.CreateInstance<NonStockItemMaint>();
    RelatedItemsTab<InventoryItemMaintBase> implementation = ((PXGraph) inventoryItemMaintBase).FindImplementation<RelatedItemsTab<InventoryItemMaintBase>>();
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) inventoryItemMaintBase.Item).Current = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) inventoryItemMaintBase.Item).Search<PX.Objects.IN.InventoryItem.inventoryID>((object) item.OriginalInventoryID, Array.Empty<object>()));
    INRelatedInventory relatedInventory = PXResultset<INRelatedInventory>.op_Implicit(((PXSelectBase<INRelatedInventory>) implementation.RelatedItems).Search<INRelatedInventory.inventoryID, INRelatedInventory.lineID>((object) item.OriginalInventoryID, (object) item.LineID, Array.Empty<object>()));
    ((PXSelectBase<INRelatedInventory>) implementation.RelatedItems).Current = relatedInventory;
    ((PXSelectBase<INRelatedInventory>) implementation.RelatedItems).Delete(relatedInventory);
    ((PXAction) inventoryItemMaintBase.Save).Press();
  }
}
