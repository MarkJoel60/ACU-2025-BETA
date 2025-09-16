// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.TemplateInventorySubAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Attributes;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.IN.Matrix.Attributes;

public class TemplateInventorySubAccountAttribute : SubAccountAttribute
{
  protected bool _expectedStockItemValue;
  protected Type _databaseField;

  public TemplateInventorySubAccountAttribute(
    bool expectedStockItemValue,
    Type databaseField,
    Type accountType)
    : base(accountType)
  {
    this._expectedStockItemValue = expectedStockItemValue;
    this._databaseField = databaseField;
    if (this._DBAttrIndex == -1)
      return;
    ((PXAggregateAttribute) this)._Attributes[this._DBAttrIndex] = (PXEventSubscriberAttribute) new DBIntConditionAttribute(typeof (PX.Objects.IN.InventoryItem.stkItem), (object) expectedStockItemValue, databaseField);
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!this.IsCurrentStockItemValueEqualsExpectedValue(e.Row as PX.Objects.IN.InventoryItem))
      return;
    if (string.Compare(this.FieldName, this._databaseField.Name, true) != 0)
    {
      object obj = sender.GetValue(e.Row, this.FieldName);
      sender.SetValue(e.Row, this._databaseField.Name, obj);
    }
    base.RowPersisting(sender, e);
  }

  protected virtual bool IsCurrentStockItemValueEqualsExpectedValue(PX.Objects.IN.InventoryItem currentRow)
  {
    bool? stkItem = (bool?) currentRow?.StkItem;
    bool expectedStockItemValue = this._expectedStockItemValue;
    return stkItem.GetValueOrDefault() == expectedStockItemValue & stkItem.HasValue;
  }
}
