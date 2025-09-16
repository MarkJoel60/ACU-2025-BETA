// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.ConvertedInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN.Exceptions;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class ConvertedInventoryItemAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
{
  protected Type _isStockItemField;
  protected bool? _isStockItemValue;

  public ConvertedInventoryItemAttribute(Type isStockItemField)
  {
    this._isStockItemField = isStockItemField;
  }

  public ConvertedInventoryItemAttribute(bool isStockItemValue)
  {
    this._isStockItemValue = new bool?(isStockItemValue);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    Type bqlTable = this.BqlTable;
    string fieldName = this.FieldName;
    ConvertedInventoryItemAttribute inventoryItemAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) inventoryItemAttribute, __vmethodptr(inventoryItemAttribute, FieldVerifying));
    fieldVerifying.AddHandler(bqlTable, fieldName, pxFieldVerifying);
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this._isStockItemField == (Type) null)
      return;
    int? inventoryID = (int?) sender.GetValue(e.Row, this._FieldName);
    bool? nullable = new bool?();
    if (inventoryID.HasValue)
      nullable = (bool?) InventoryItem.PK.Find(sender.Graph, inventoryID)?.StkItem;
    sender.SetValue(e.Row, this._isStockItemField.Name, (object) nullable);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    bool? nullable1 = new bool?();
    bool? nullable2 = !(this._isStockItemField != (Type) null) ? this._isStockItemValue : sender.GetValue(e.Row, this._isStockItemField.Name) as bool?;
    if (!nullable2.HasValue || ((int?) sender.GetValue(e.Row, this._FieldName)).HasValue)
      return;
    int? newValue = e.NewValue as int?;
    if (!newValue.HasValue)
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, newValue);
    if (inventoryItem == null)
      return;
    bool? nullable3 = inventoryItem.IsConverted;
    if (!nullable3.GetValueOrDefault())
      return;
    nullable3 = inventoryItem.StkItem;
    bool? nullable4 = nullable2;
    if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
      throw new ItemHasBeenConvertedException(inventoryItem?.InventoryCD);
  }

  public virtual void Validate(PXCache cache, object row)
  {
    bool? nullable1 = new bool?();
    bool? nullable2 = !(this._isStockItemField != (Type) null) ? this._isStockItemValue : cache.GetValue(row, this._isStockItemField.Name) as bool?;
    if (!nullable2.HasValue)
      return;
    int? inventoryID = cache.GetValue(row, this.FieldName) as int?;
    if (!inventoryID.HasValue)
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find(cache.Graph, inventoryID);
    if (inventoryItem == null)
      return;
    bool? nullable3 = inventoryItem.IsConverted;
    if (!nullable3.GetValueOrDefault())
      return;
    nullable3 = inventoryItem.StkItem;
    bool? nullable4 = nullable2;
    if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
      throw new ItemHasBeenConvertedException(inventoryItem.InventoryCD);
  }

  public static void ValidateRow(PXCache cache, object row)
  {
    EnumerableExtensions.ForEach<ConvertedInventoryItemAttribute>(cache.GetAttributesReadonly((string) null).OfType<ConvertedInventoryItemAttribute>(), (Action<ConvertedInventoryItemAttribute>) (a => a.Validate(cache, row)));
  }
}
