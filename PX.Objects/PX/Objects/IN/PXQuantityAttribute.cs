// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXQuantityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXQuantityAttribute : 
  PXDecimalAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowInsertingSubscriber,
  IPXRowPersistingSubscriber
{
  protected int _ResultOrdinal;
  protected int _KeyOrdinal;
  protected Type _KeyField;
  protected Type _ResultField;
  protected bool _HandleEmptyKey;
  protected int? _OverridePrecision;

  public PXQuantityAttribute()
  {
  }

  public PXQuantityAttribute(byte precision) => this._OverridePrecision = new int?((int) precision);

  public PXQuantityAttribute(Type keyField, Type resultField)
  {
    this._KeyField = keyField;
    this._ResultField = resultField;
  }

  public bool HandleEmptyKey
  {
    set => this._HandleEmptyKey = value;
    get => this._HandleEmptyKey;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Precision = new int?(this._OverridePrecision ?? CommonSetupDecPl.Qty);
    if (this._ResultField != (Type) null)
      this._ResultOrdinal = sender.GetFieldOrdinal(this._ResultField.Name);
    if (!(this._KeyField != (Type) null))
      return;
    this._KeyOrdinal = sender.GetFieldOrdinal(this._KeyField.Name);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = BqlCommand.GetItemType(this._KeyField);
    string name = this._KeyField.Name;
    PXQuantityAttribute quantityAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) quantityAttribute, __vmethodptr(quantityAttribute, KeyFieldUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
  }

  internal virtual INUnit ReadConversion(PXCache cache, object data)
  {
    return cache.GetAttributesOfType<INUnitAttribute>(data, this._KeyField.Name).FirstOrDefault<INUnitAttribute>()?.ReadConversion(cache, data, (string) cache.GetValue(data, this._KeyField.Name));
  }

  protected virtual void CalcBaseQty(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    Decimal? nullable = new Decimal?();
    if (!(this._ResultField != (Type) null))
      return;
    if (e.NewValue != null)
    {
      bool flag = false;
      if (this._HandleEmptyKey && string.IsNullOrEmpty((string) sender.GetValue(e.Row, this._KeyField.Name)))
      {
        nullable = new Decimal?((Decimal) e.NewValue);
        flag = true;
      }
      if (!flag)
      {
        INUnit inUnit = this.ReadConversion(sender, e.Row);
        if (inUnit != null)
        {
          if (inUnit.FromUnit == inUnit.ToUnit)
          {
            this._ensurePrecision(sender, e.Row);
            nullable = new Decimal?(Math.Round((Decimal) e.NewValue, this._Precision.Value, MidpointRounding.AwayFromZero));
          }
          else
          {
            Decimal? unitRate = inUnit.UnitRate;
            Decimal num1 = 0M;
            if (!(unitRate.GetValueOrDefault() == num1 & unitRate.HasValue))
            {
              this._ensurePrecision(sender, e.Row);
              ref Decimal? local = ref nullable;
              Decimal newValue = (Decimal) e.NewValue;
              Decimal num2;
              if (!(inUnit.UnitMultDiv == "M"))
              {
                unitRate = inUnit.UnitRate;
                num2 = 1M / unitRate.Value;
              }
              else
              {
                unitRate = inUnit.UnitRate;
                num2 = unitRate.Value;
              }
              Decimal num3 = Math.Round(newValue * num2, this._Precision.Value, MidpointRounding.AwayFromZero);
              local = new Decimal?(num3);
            }
          }
        }
        else if (!e.ExternalCall)
          throw new PXUnitConversionException((string) sender.GetValue(e.Row, this._KeyField.Name));
      }
    }
    sender.SetValue(e.Row, this._ResultOrdinal, (object) nullable);
  }

  protected virtual void CalcBaseQty(PXCache sender, object data)
  {
    object obj = sender.GetValue(data, ((PXEventSubscriberAttribute) this)._FieldName);
    try
    {
      this.CalcBaseQty(sender, new PXFieldVerifyingEventArgs(data, obj, false));
    }
    catch (PXUnitConversionException ex)
    {
      sender.SetValue(data, this._ResultField.Name, (object) null);
    }
  }

  protected virtual void CalcTranQty(PXCache sender, object data)
  {
    Decimal? nullable = new Decimal?();
    if (!(this._ResultField != (Type) null))
      return;
    object d = sender.GetValue(data, this._ResultOrdinal);
    if (d != null)
    {
      INUnit inUnit = this.ReadConversion(sender, data);
      if (inUnit != null)
      {
        if (inUnit.FromUnit == inUnit.ToUnit)
        {
          this._ensurePrecision(sender, data);
          nullable = new Decimal?(Math.Round((Decimal) d, this._Precision.Value, MidpointRounding.AwayFromZero));
        }
        else
        {
          Decimal? unitRate = inUnit.UnitRate;
          Decimal num = 0M;
          if (!(unitRate.GetValueOrDefault() == num & unitRate.HasValue))
          {
            this._ensurePrecision(sender, data);
            nullable = new Decimal?(Math.Round((Decimal) d * (inUnit.UnitMultDiv == "M" ? 1M / inUnit.UnitRate.Value : inUnit.UnitRate.Value), this._Precision.Value, MidpointRounding.AwayFromZero));
          }
        }
      }
    }
    sender.SetValue(data, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) nullable);
  }

  public static void CalcBaseQty<TField>(PXCache cache, object data) where TField : class, IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<TField>(data))
    {
      if (attribute is PXDBQuantityAttribute)
      {
        ((PXQuantityAttribute) attribute).CalcBaseQty(cache, data);
        break;
      }
    }
  }

  public static void CalcTranQty<TField>(PXCache cache, object data) where TField : class, IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<TField>(data))
    {
      if (attribute is PXQuantityAttribute)
      {
        ((PXQuantityAttribute) attribute).CalcTranQty(cache, data);
        break;
      }
    }
  }

  public virtual void KeyFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.CalcBaseQty(sender, e.Row);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    this.CalcBaseQty(sender, new PXFieldVerifyingEventArgs(e.Row, obj, false));
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.CalcBaseQty(sender, e.Row);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    this.CalcBaseQty(sender, e);
  }
}
