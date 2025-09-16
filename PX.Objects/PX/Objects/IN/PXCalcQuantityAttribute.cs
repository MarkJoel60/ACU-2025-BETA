// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXCalcQuantityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class PXCalcQuantityAttribute : PXDecimalAttribute
{
  protected int _SourceOrdinal;
  protected int _KeyOrdinal;
  protected Type _KeyField;
  protected Type _SourceField;
  protected bool _LegacyBehavior;

  public PXCalcQuantityAttribute()
  {
  }

  /// <summary>
  /// Calculates TranQty using BaseQty and UOM. TranQty will be calculated on RowSelected event only.
  /// </summary>
  /// <param name="keyField">UOM field</param>
  /// <param name="sourceField">BaseQty field</param>
  public PXCalcQuantityAttribute(Type keyField, Type sourceField)
    : this(keyField, sourceField, true)
  {
  }

  /// <summary>Calculates TranQty using BaseQty and UOM.</summary>
  /// <param name="keyField">UOM field</param>
  /// <param name="sourceField">BaseQty field</param>
  /// <param name="legacyBehavior">When set to True, TranQty will be calculated on RowSelected and on FieldSelecting (when needed) events.</param>
  public PXCalcQuantityAttribute(Type keyField, Type sourceField, bool legacyBehavior)
  {
    this._KeyField = keyField;
    this._SourceField = sourceField;
    this._LegacyBehavior = legacyBehavior;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Precision = new int?(CommonSetupDecPl.Qty);
    if (this._SourceField != (Type) null)
      this._SourceOrdinal = sender.GetFieldOrdinal(this._SourceField.Name);
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    Type itemType1 = sender.GetItemType();
    PXCalcQuantityAttribute quantityAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) quantityAttribute1, __vmethodptr(quantityAttribute1, RowSelected));
    rowSelected.AddHandler(itemType1, pxRowSelected);
    if (!this._LegacyBehavior)
    {
      PXGraph.FieldSelectingEvents fieldSelecting = sender.Graph.FieldSelecting;
      Type itemType2 = sender.GetItemType();
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
      PXCalcQuantityAttribute quantityAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) quantityAttribute2, __vmethodptr(quantityAttribute2, TranQtyFieldSelecting));
      fieldSelecting.AddHandler(itemType2, fieldName, pxFieldSelecting);
    }
    if (!(this._KeyField != (Type) null))
      return;
    this._KeyOrdinal = sender.GetFieldOrdinal(this._KeyField.Name);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType3 = BqlCommand.GetItemType(this._KeyField);
    string name = this._KeyField.Name;
    PXCalcQuantityAttribute quantityAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) quantityAttribute3, __vmethodptr(quantityAttribute3, KeyFieldUpdated));
    fieldUpdated.AddHandler(itemType3, name, pxFieldUpdated);
  }

  internal virtual INUnit ReadConversion(PXCache cache, object data)
  {
    return cache.GetAttributesOfType<INUnitAttribute>(data, this._KeyField.Name).FirstOrDefault<INUnitAttribute>()?.ReadConversion(cache, data, (string) cache.GetValue(data, this._KeyField.Name));
  }

  protected virtual void CalcTranQty(PXCache sender, object data)
  {
    Decimal? tranQty = this.GetTranQty(sender, data);
    sender.SetValue(data, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) tranQty.GetValueOrDefault());
  }

  protected virtual Decimal? GetTranQty(PXCache sender, object data)
  {
    Decimal? tranQty = new Decimal?();
    if (this._SourceField != (Type) null)
    {
      object d = sender.GetValue(data, this._SourceOrdinal);
      if (d != null)
      {
        INUnit inUnit = this.ReadConversion(sender, data);
        if (inUnit != null)
        {
          if (inUnit.FromUnit == inUnit.ToUnit)
          {
            this._ensurePrecision(sender, data);
            tranQty = new Decimal?(Math.Round((Decimal) d, this._Precision.Value, MidpointRounding.AwayFromZero));
          }
          else
          {
            Decimal? unitRate = inUnit.UnitRate;
            Decimal num = 0M;
            if (!(unitRate.GetValueOrDefault() == num & unitRate.HasValue))
            {
              this._ensurePrecision(sender, data);
              tranQty = new Decimal?(Math.Round((Decimal) d * (inUnit.UnitMultDiv == "M" ? 1M / inUnit.UnitRate.Value : inUnit.UnitRate.Value), this._Precision.Value, MidpointRounding.AwayFromZero));
            }
          }
        }
      }
    }
    return tranQty;
  }

  public virtual void KeyFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.CalcTranQty(sender, e.Row);
  }

  protected virtual void TranQtyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object row = e.Row;
    if (e.Row == null || e.ReturnValue != null)
      return;
    e.ReturnValue = (object) this.GetTranQty(sender, e.Row);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
      return;
    this.CalcTranQty(sender, e.Row);
  }
}
