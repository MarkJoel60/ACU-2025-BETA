// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.MassConvertStockNonStock
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.DAC.Unbound;
using PX.Objects.IN.GraphExtensions.InventoryItemMaintExt;
using PX.Objects.IN.GraphExtensions.NonStockItemMaintExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class MassConvertStockNonStock : 
  PXGraph<MassConvertStockNonStock>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXSetup<INSetup> insetup;
  public PXFilter<MassConvertStockNonStockFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<InventoryItem, MassConvertStockNonStockFilter, Where<InventoryItem.stkItem, Equal<Current<MassConvertStockNonStockFilter.stkItem>>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.templateItemID, IsNull, And<InventoryItem.kitItem, Equal<False>, And2<Where<InventoryItem.stkItem, Equal<True>, Or<InventoryItem.nonStockReceipt, Equal<True>, And<InventoryItem.nonStockShip, Equal<True>, And<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>>>>>, And2<Where<Current<MassConvertStockNonStockFilter.itemClassID>, IsNull, Or<InventoryItem.itemClassID, Equal<Current<MassConvertStockNonStockFilter.itemClassID>>>>, And<MatchUser>>>>>>>>> ItemList;
  public PXCancel<MassConvertStockNonStockFilter> Cancel;

  public IEnumerable itemList() => this.GetItemsForProcessing();

  public MassConvertStockNonStock()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    ((PXProcessingBase<InventoryItem>) this.ItemList).SetSelected<InventoryItem.selected>();
    ((PXProcessing<InventoryItem>) this.ItemList).SetProcessCaption("Process");
    ((PXProcessing<InventoryItem>) this.ItemList).SetProcessAllCaption("Process All");
    ((PXGraph) this).Actions["Schedule"].SetVisible(false);
  }

  protected virtual IEnumerable GetItemsForProcessing()
  {
    BqlCommand bqlSelect = ((PXSelectBase) this.ItemList).View.BqlSelect;
    List<object> parameters = new List<object>();
    BqlCommand command = this.AppendFilter(bqlSelect, (IList<object>) parameters, ((PXSelectBase<MassConvertStockNonStockFilter>) this.Filter).Current);
    int startRow = PXView.StartRow;
    int num = 0;
    PXDelegateResult itemsForProcessing = new PXDelegateResult();
    itemsForProcessing.IsResultFiltered = true;
    itemsForProcessing.IsResultSorted = true;
    itemsForProcessing.IsResultTruncated = true;
    ((List<object>) itemsForProcessing).AddRange((IEnumerable<object>) command.CreateView((PXGraph) this, mergeCache: true).Select(PXView.Currents, parameters.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num));
    PXView.StartRow = 0;
    return (IEnumerable) itemsForProcessing;
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<MassConvertStockNonStockFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    MassConvertStockNonStock.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new MassConvertStockNonStock.\u003C\u003Ec__DisplayClass7_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.e = e;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass70.e.Row == null)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    // ISSUE: reference to a compiler-generated method
    // ISSUE: reference to a compiler-generated method
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<MassConvertStockNonStockFilter>>) cDisplayClass70.e).Cache, (object) null).For<MassConvertStockNonStockFilter.nonStockItemClassID>(new Action<PXUIFieldAttribute>(cDisplayClass70.\u003C_\u003Eb__0)).For<MassConvertStockNonStockFilter.nonStockPostClassID>(new Action<PXUIFieldAttribute>(cDisplayClass70.\u003C_\u003Eb__1)).For<MassConvertStockNonStockFilter.stockItemClassID>(new Action<PXUIFieldAttribute>(cDisplayClass70.\u003C_\u003Eb__2)).SameFor<MassConvertStockNonStockFilter.stockPostClassID>();
    chained = chained.SameFor<MassConvertStockNonStockFilter.stockValMethod>();
    chained.SameFor<MassConvertStockNonStockFilter.stockLotSerClassID>();
    PXFilteredProcessing<InventoryItem, MassConvertStockNonStockFilter, Where<InventoryItem.stkItem, Equal<Current<MassConvertStockNonStockFilter.stkItem>>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.templateItemID, IsNull, And<InventoryItem.kitItem, Equal<False>, And2<Where<InventoryItem.stkItem, Equal<True>, Or<InventoryItem.nonStockReceipt, Equal<True>, And<InventoryItem.nonStockShip, Equal<True>, And<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>>>>>, And2<Where<Current<MassConvertStockNonStockFilter.itemClassID>, IsNull, Or<InventoryItem.itemClassID, Equal<Current<MassConvertStockNonStockFilter.itemClassID>>>>, And<MatchUser>>>>>>>>> itemList1 = this.ItemList;
    MassConvertStockNonStock convertStockNonStock = this;
    // ISSUE: virtual method pointer
    PXProcessingBase<InventoryItem>.ParametersDelegate parametersDelegate = new PXProcessingBase<InventoryItem>.ParametersDelegate((object) convertStockNonStock, __vmethodptr(convertStockNonStock, ValidateParameters));
    ((PXProcessingBase<InventoryItem>) itemList1).SetParametersDelegate(parametersDelegate);
    // ISSUE: reference to a compiler-generated field
    bool? stkItem = cDisplayClass70.e.Row.StkItem;
    if (stkItem.HasValue)
    {
      if (stkItem.GetValueOrDefault())
      {
        // ISSUE: method pointer
        ((PXProcessingBase<InventoryItem>) this.ItemList).SetProcessDelegate<InventoryItemMaint>(new PXProcessingBase<InventoryItem>.ProcessItemDelegate<InventoryItemMaint>((object) cDisplayClass70, __methodptr(\u003C_\u003Eb__3)));
      }
      else
      {
        // ISSUE: method pointer
        ((PXProcessingBase<InventoryItem>) this.ItemList).SetProcessDelegate<NonStockItemMaint>(new PXProcessingBase<InventoryItem>.ProcessItemDelegate<NonStockItemMaint>((object) cDisplayClass70, __methodptr(\u003C_\u003Eb__4)));
      }
    }
    PXFilteredProcessing<InventoryItem, MassConvertStockNonStockFilter, Where<InventoryItem.stkItem, Equal<Current<MassConvertStockNonStockFilter.stkItem>>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.templateItemID, IsNull, And<InventoryItem.kitItem, Equal<False>, And2<Where<InventoryItem.stkItem, Equal<True>, Or<InventoryItem.nonStockReceipt, Equal<True>, And<InventoryItem.nonStockShip, Equal<True>, And<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>>>>>, And2<Where<Current<MassConvertStockNonStockFilter.itemClassID>, IsNull, Or<InventoryItem.itemClassID, Equal<Current<MassConvertStockNonStockFilter.itemClassID>>>>, And<MatchUser>>>>>>>>> itemList2 = this.ItemList;
    // ISSUE: reference to a compiler-generated field
    stkItem = cDisplayClass70.e.Row.StkItem;
    int num1 = stkItem.HasValue ? 1 : 0;
    ((PXProcessing<InventoryItem>) itemList2).SetProcessEnabled(num1 != 0);
    PXFilteredProcessing<InventoryItem, MassConvertStockNonStockFilter, Where<InventoryItem.stkItem, Equal<Current<MassConvertStockNonStockFilter.stkItem>>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.templateItemID, IsNull, And<InventoryItem.kitItem, Equal<False>, And2<Where<InventoryItem.stkItem, Equal<True>, Or<InventoryItem.nonStockReceipt, Equal<True>, And<InventoryItem.nonStockShip, Equal<True>, And<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>>>>>, And2<Where<Current<MassConvertStockNonStockFilter.itemClassID>, IsNull, Or<InventoryItem.itemClassID, Equal<Current<MassConvertStockNonStockFilter.itemClassID>>>>, And<MatchUser>>>>>>>>> itemList3 = this.ItemList;
    // ISSUE: reference to a compiler-generated field
    stkItem = cDisplayClass70.e.Row.StkItem;
    int num2 = stkItem.HasValue ? 1 : 0;
    ((PXProcessing<InventoryItem>) itemList3).SetProcessAllEnabled(num2 != 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID> e)
  {
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID>>) e).ExternalCall || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID>, object, object>) e).NewValue is int) || (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID>>) e).Cache.GetValuePending<MassConvertStockNonStockFilter.action>(e.Row) ?? PXCache.NotSetValue) == PXCache.NotSetValue || (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID>>) e).Cache.GetValuePending<MassConvertStockNonStockFilter.stkItem>(e.Row) ?? PXCache.NotSetValue) == PXCache.NotSetValue || (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID>>) e).Cache.GetValuePending<MassConvertStockNonStockFilter.stockPostClassID>(e.Row) ?? PXCache.NotSetValue) == PXCache.NotSetValue && (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID>>) e).Cache.GetValuePending<MassConvertStockNonStockFilter.nonStockPostClassID>(e.Row) ?? PXCache.NotSetValue) == PXCache.NotSetValue)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<MassConvertStockNonStockFilter.inventoryID>, object, object>) e).NewValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InventoryItem, InventoryItem.selected> e)
  {
    bool? stkItem1 = e.Row.StkItem;
    bool? stkItem2 = ((PXSelectBase<MassConvertStockNonStockFilter>) this.Filter).Current.StkItem;
    if (stkItem1.GetValueOrDefault() == stkItem2.GetValueOrDefault() & stkItem1.HasValue == stkItem2.HasValue)
      return;
    e.Row.Selected = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<MassConvertStockNonStockFilter> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<MassConvertStockNonStockFilter>>) e).Cache.ObjectsEqual<MassConvertStockNonStockFilter.stkItem>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase) this.ItemList).Cache.Clear();
    ((PXSelectBase) this.ItemList).Cache.ClearQueryCache();
  }

  protected virtual bool ValidateParameters(List<InventoryItem> list)
  {
    if (!((PXSelectBase<MassConvertStockNonStockFilter>) this.Filter).Current.StkItem.HasValue)
      throw new PXInvalidOperationException();
    this.ValidateNonEmptyRequiredField<MassConvertStockNonStockFilter.nonStockPostClassID>(true);
    this.ValidateNonEmptyRequiredField<MassConvertStockNonStockFilter.stockItemClassID>(false);
    this.ValidateNonEmptyRequiredField<MassConvertStockNonStockFilter.stockPostClassID>(false);
    this.ValidateNonEmptyRequiredField<MassConvertStockNonStockFilter.stockValMethod>(false);
    if (PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>())
      this.ValidateNonEmptyRequiredField<MassConvertStockNonStockFilter.stockLotSerClassID>(false);
    Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(((PXSelectBase) this.Filter).Cache, (object) ((PXSelectBase<MassConvertStockNonStockFilter>) this.Filter).Current, new PXErrorLevel[1]
    {
      (PXErrorLevel) 4
    });
    if (errors.Any<KeyValuePair<string, string>>())
      throw new PXException(string.Join(Environment.NewLine, (IEnumerable<string>) errors.Values));
    return true;
  }

  private string ValidateNonEmptyRequiredField<TField>(bool stkField) where TField : IBqlField
  {
    string str1;
    if (((PXSelectBase) this.Filter).Cache.GetValue<TField>((object) ((PXSelectBase<MassConvertStockNonStockFilter>) this.Filter).Current) == null)
    {
      int num1 = stkField ? 1 : 0;
      bool? stkItem = ((PXSelectBase<MassConvertStockNonStockFilter>) this.Filter).Current.StkItem;
      int num2 = stkItem.GetValueOrDefault() ? 1 : 0;
      if (num1 == num2 & stkItem.HasValue)
      {
        str1 = PXMessages.LocalizeFormat("'{0}' cannot be empty.", new object[1]
        {
          (object) $"[{typeof (TField).Name}]"
        });
        goto label_4;
      }
    }
    str1 = (string) null;
label_4:
    string str2 = str1;
    ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<TField>((object) ((PXSelectBase<MassConvertStockNonStockFilter>) this.Filter).Current, (object) null, str2 != null ? (Exception) new PXSetPropertyException(str2, (PXErrorLevel) 4) : (Exception) null);
    return str2;
  }

  private static void ConvertStockToNonStock(
    InventoryItemMaint graph,
    InventoryItem item,
    MassConvertStockNonStockFilter filter)
  {
    ((PXGraph) graph).Clear();
    ConvertStockToNonStockExt implementation = ((PXGraph) graph).FindImplementation<ConvertStockToNonStockExt>();
    ((PXSelectBase<InventoryItem>) graph.Item).Current = PXResultset<InventoryItem>.op_Implicit(((PXSelectBase<InventoryItem>) graph.Item).Search<InventoryItem.inventoryID>((object) item.InventoryID, Array.Empty<object>()));
    try
    {
      ((PXAction) implementation.convert).Press();
    }
    catch (PXRedirectRequiredException ex)
    {
      NonStockItemMaint graph1 = (NonStockItemMaint) ex.Graph;
      graph1.Answers.ForceValidationInUnattendedMode = true;
      InventoryItem copy1 = PXCache<InventoryItem>.CreateCopy(((PXSelectBase<InventoryItem>) graph1.Item).Current);
      copy1.ItemClassID = filter.NonStockItemClassID;
      InventoryItem copy2 = PXCache<InventoryItem>.CreateCopy(((PXSelectBase<InventoryItem>) graph1.Item).Update(copy1));
      copy2.PostClassID = filter.NonStockPostClassID;
      ((PXSelectBase<InventoryItem>) graph1.Item).Update(copy2);
      ((PXAction) graph1.Save).Press();
    }
  }

  private static void ConvertNonStockToStock(
    NonStockItemMaint graph,
    InventoryItem item,
    MassConvertStockNonStockFilter filter)
  {
    ((PXGraph) graph).Clear();
    ConvertNonStockToStockExt implementation = ((PXGraph) graph).FindImplementation<ConvertNonStockToStockExt>();
    ((PXSelectBase<InventoryItem>) graph.Item).Current = PXResultset<InventoryItem>.op_Implicit(((PXSelectBase<InventoryItem>) graph.Item).Search<InventoryItem.inventoryID>((object) item.InventoryID, Array.Empty<object>()));
    try
    {
      ((PXAction) implementation.convert).Press();
    }
    catch (PXRedirectRequiredException ex)
    {
      InventoryItemMaint graph1 = (InventoryItemMaint) ex.Graph;
      graph1.Answers.ForceValidationInUnattendedMode = true;
      InventoryItem copy1 = PXCache<InventoryItem>.CreateCopy(((PXSelectBase<InventoryItem>) graph1.Item).Current);
      copy1.ItemClassID = filter.StockItemClassID;
      InventoryItem copy2 = PXCache<InventoryItem>.CreateCopy(((PXSelectBase<InventoryItem>) graph1.Item).Update(copy1));
      copy2.PostClassID = filter.StockPostClassID;
      copy2.ValMethod = filter.StockValMethod;
      copy2.LotSerClassID = filter.StockLotSerClassID;
      ((PXSelectBase<InventoryItem>) graph1.Item).Update(copy2);
      ((PXAction) graph1.Save).Press();
    }
  }

  public virtual BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    MassConvertStockNonStockFilter filter)
  {
    return cmd;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
  }
}
