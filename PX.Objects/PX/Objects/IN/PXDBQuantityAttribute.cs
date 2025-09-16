// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXDBQuantityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBQuantityAttribute : 
  PXDBDecimalAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowInsertingSubscriber
{
  private Dictionary<object, PXDBQuantityAttribute> _rowAttributes;
  protected Type _KeyField;
  protected Type _ResultField;
  protected bool _HandleEmptyKey;
  protected int? _OverridePrecision;

  public Type KeyField => this._KeyField;

  public InventoryUnitType DecimalVerifyUnits { get; set; }

  public DecimalVerifyMode DecimalVerifyMode { get; set; }

  /// <summary>
  /// Enable conversion other units to specified units for decimal verifying(<see cref="P:PX.Objects.IN.PXDBQuantityAttribute.DecimalVerifyUnits" />)
  /// </summary>
  public bool ConvertToDecimalVerifyUnits { get; set; }

  public PXDBQuantityAttribute()
  {
    this.ConvertToDecimalVerifyUnits = true;
    this.DecimalVerifyMode = DecimalVerifyMode.Error;
  }

  public PXDBQuantityAttribute(byte precision)
    : this()
  {
    this._OverridePrecision = new int?((int) precision);
  }

  public PXDBQuantityAttribute(Type keyField, Type resultField)
    : this()
  {
    this._KeyField = keyField;
    this._ResultField = resultField;
  }

  public PXDBQuantityAttribute(
    Type keyField,
    Type resultField,
    InventoryUnitType decimalVerifyUnits)
    : this(keyField, resultField)
  {
    this.DecimalVerifyUnits = decimalVerifyUnits;
  }

  public PXDBQuantityAttribute(int precision, Type keyField, Type resultField)
    : this(keyField, resultField)
  {
    this._OverridePrecision = new int?(precision);
  }

  public PXDBQuantityAttribute(
    int precision,
    Type keyField,
    Type resultField,
    InventoryUnitType decimalVerifyUnits)
    : this(keyField, resultField, decimalVerifyUnits)
  {
    this._OverridePrecision = new int?(precision);
  }

  public bool HandleEmptyKey
  {
    set => this._HandleEmptyKey = value;
    get => this._HandleEmptyKey;
  }

  public static PXNotDecimalUnitException VerifyForDecimal(PXCache cache, object data)
  {
    PXNotDecimalUnitException ex = (PXNotDecimalUnitException) null;
    PXCacheEx.Adjust<PXDBQuantityAttribute>(cache, data).ForAllFields((Action<PXDBQuantityAttribute>) (a =>
    {
      PXNotDecimalUnitException decimalUnitException = a.VerifyForDecimalValue(cache, data);
      if (decimalUnitException == null || ex != null && decimalUnitException.ErrorLevel <= ex.ErrorLevel)
        return;
      ex = decimalUnitException;
    }));
    return ex;
  }

  public static PXNotDecimalUnitException VerifyForDecimal<TField>(PXCache cache, object data) where TField : IBqlField
  {
    PXNotDecimalUnitException error = (PXNotDecimalUnitException) null;
    PXCacheEx.Adjust<PXDBQuantityAttribute>(cache, data).For<TField>((Action<PXDBQuantityAttribute>) (a => error = a.VerifyForDecimalValue(cache, data)));
    return error;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Precision = new int?(this._OverridePrecision ?? CommonSetupDecPl.Qty);
    if (this._ResultField == (Type) null || !(this._KeyField != (Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = BqlCommand.GetItemType(this._KeyField);
    string name = this._KeyField.Name;
    PXDBQuantityAttribute quantityAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) quantityAttribute, __vmethodptr(quantityAttribute, KeyFieldUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    this._rowAttributes = new Dictionary<object, PXDBQuantityAttribute>(PXCacheEx.GetComparer(sender));
    this._rowAttributes.Add((object) 0, this);
  }

  protected string GetFromUnit(PXCache cache, object data)
  {
    return (string) cache.GetValue(data, this._KeyField.Name);
  }

  internal virtual ConversionInfo ReadConversionInfo(PXCache cache, object data)
  {
    return cache.GetAttributesOfType<INUnitAttribute>(data, this._KeyField.Name).FirstOrDefault<INUnitAttribute>()?.ReadConversionInfo(cache, data, this.GetFromUnit(cache, data));
  }

  internal virtual InventoryItem ReadInventoryItem(PXCache cache, object data)
  {
    return cache.GetAttributesOfType<INUnitAttribute>(data, this._KeyField.Name).FirstOrDefault<INUnitAttribute>()?.ReadInventoryItem(cache, data);
  }

  protected virtual void CalcBaseQty(PXCache sender, PXDBQuantityAttribute.QtyConversionArgs e)
  {
    if (!(this._ResultField != (Type) null))
      return;
    Decimal? nullable = this.CalcResultValue(sender, e);
    if (e.ExternalCall)
      sender.SetValueExt(e.Row, this._ResultField.Name, (object) nullable);
    else
      sender.SetValue(e.Row, this._ResultField.Name, (object) nullable);
  }

  protected virtual Decimal? CalcResultValue(
    PXCache sender,
    PXDBQuantityAttribute.QtyConversionArgs e)
  {
    Decimal? baseValue = new Decimal?();
    if (this._ResultField != (Type) null && e.NewValue != null)
    {
      bool flag = false;
      if (this._HandleEmptyKey && string.IsNullOrEmpty(this.GetFromUnit(sender, e.Row)))
      {
        baseValue = new Decimal?((Decimal) e.NewValue);
        flag = true;
      }
      if (!flag)
      {
        if ((Decimal) e.NewValue == 0M)
        {
          baseValue = new Decimal?(0M);
        }
        else
        {
          ConversionInfo conversionInfo = this.ReadConversionInfo(sender, e.Row);
          if (conversionInfo?.Conversion != null)
          {
            baseValue = this.ConvertValue(sender, e.Row, new Decimal?((Decimal) e.NewValue), conversionInfo.Conversion);
            PXNotDecimalUnitException decimalUnitException = this.VerifyForDecimalValue(sender, conversionInfo.Inventory, e.Row, new Decimal?((Decimal) e.NewValue), baseValue);
            if (decimalUnitException != null && decimalUnitException.ErrorLevel == 4 && e.ThrowNotDecimalUnitException && !decimalUnitException.IsLazyThrow)
              throw decimalUnitException;
          }
          else if (!e.ExternalCall)
            throw new PXUnitConversionException(this.GetFromUnit(sender, e.Row));
        }
      }
    }
    return baseValue;
  }

  protected Decimal? ConvertValue(PXCache cache, object row, Decimal? value, INUnit conv)
  {
    Decimal? nullable = new Decimal?();
    if (conv.FromUnit == conv.ToUnit)
    {
      nullable = this.ConvertValue(cache, row, value.Value, (Func<Decimal, bool, Decimal>) ((v, invert) => v));
    }
    else
    {
      Decimal? unitRate = conv.UnitRate;
      Decimal num = 0M;
      if (!(unitRate.GetValueOrDefault() == num & unitRate.HasValue))
        nullable = this.ConvertValue(cache, row, value.Value, (Func<Decimal, bool, Decimal>) ((v, invert) => conv.UnitMultDiv == "M" != invert ? v * conv.UnitRate.Value : v / conv.UnitRate.Value));
    }
    return nullable;
  }

  private Decimal? ConvertValue(
    PXCache cache,
    object row,
    Decimal value,
    Func<Decimal, bool, Decimal> converter)
  {
    this._ensurePrecision(cache, row);
    Decimal? nullable = (Decimal?) cache.GetValue(row, this._ResultField.Name);
    return nullable.HasValue && Math.Round(converter(nullable.GetValueOrDefault(), true), this._Precision.Value, MidpointRounding.AwayFromZero) == value ? nullable : new Decimal?(Math.Round(converter(value, false), this._Precision.Value, MidpointRounding.AwayFromZero));
  }

  protected virtual void CalcBaseQty(PXCache sender, object data)
  {
    object newValue = sender.GetValue(data, ((PXEventSubscriberAttribute) this)._FieldName);
    try
    {
      this.CalcBaseQty(sender, new PXDBQuantityAttribute.QtyConversionArgs(data, newValue, false));
    }
    catch (PXUnitConversionException ex)
    {
      sender.SetValue(data, this._ResultField.Name, (object) null);
    }
  }

  protected virtual void CalcTranQty(PXCache sender, object data)
  {
    Decimal? originalValue = new Decimal?();
    if (!(this._ResultField != (Type) null))
      return;
    object d = sender.GetValue(data, this._ResultField.Name);
    if (d != null)
    {
      bool flag = false;
      if (this._HandleEmptyKey && string.IsNullOrEmpty(this.GetFromUnit(sender, data)))
      {
        originalValue = new Decimal?((Decimal) d);
        flag = true;
      }
      if (!flag)
      {
        if ((Decimal) d == 0M)
        {
          originalValue = new Decimal?(0M);
        }
        else
        {
          ConversionInfo conversionInfo = this.ReadConversionInfo(sender, data);
          if (conversionInfo?.Conversion != null)
          {
            INUnit conversion = conversionInfo.Conversion;
            if (conversion.FromUnit == conversion.ToUnit)
            {
              this._ensurePrecision(sender, data);
              originalValue = new Decimal?(Math.Round((Decimal) d, this._Precision.Value, MidpointRounding.AwayFromZero));
            }
            else
            {
              Decimal? unitRate = conversion.UnitRate;
              Decimal num = 0M;
              if (!(unitRate.GetValueOrDefault() == num & unitRate.HasValue))
              {
                this._ensurePrecision(sender, data);
                originalValue = new Decimal?(Math.Round((Decimal) d * (conversion.UnitMultDiv == "M" ? 1M / conversion.UnitRate.Value : conversion.UnitRate.Value), this._Precision.Value, MidpointRounding.AwayFromZero));
              }
            }
            this.VerifyForDecimalValue(sender, conversionInfo.Inventory, data, originalValue, new Decimal?((Decimal) d));
          }
        }
      }
    }
    sender.SetValue(data, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) originalValue);
  }

  public static void CalcBaseQty<TField>(PXCache cache, object data) where TField : class, IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<TField>(data))
    {
      if (attribute is PXDBQuantityAttribute)
      {
        ((PXDBQuantityAttribute) attribute).CalcBaseQty(cache, data);
        break;
      }
    }
  }

  public static void CalcTranQty<TField>(PXCache cache, object data) where TField : class, IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<TField>(data))
    {
      if (attribute is PXDBQuantityAttribute)
      {
        ((PXDBQuantityAttribute) attribute).CalcTranQty(cache, data);
        break;
      }
    }
  }

  public static Decimal Round(Decimal? value)
  {
    return Math.Round(value.GetValueOrDefault(), CommonSetupDecPl.Qty, MidpointRounding.AwayFromZero);
  }

  public virtual void KeyFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.CalcBaseQty(sender, e.Row);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    object newValue = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    this.CalcBaseQty(sender, new PXDBQuantityAttribute.QtyConversionArgs(e.Row, newValue, false)
    {
      ThrowNotDecimalUnitException = true
    });
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.CalcBaseQty(sender, e.Row);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    base.FieldVerifying(sender, e);
    PXFieldUpdatingEventArgs updatingEventArgs = new PXFieldUpdatingEventArgs(e.Row, e.NewValue);
    if (!e.ExternalCall)
      this.FieldUpdating(sender, updatingEventArgs);
    this.CalcBaseQty(sender, new PXDBQuantityAttribute.QtyConversionArgs(updatingEventArgs.Row, updatingEventArgs.NewValue, true));
    e.NewValue = updatingEventArgs.NewValue;
  }

  public void SetDecimalVerifyMode(object row, DecimalVerifyMode mode)
  {
    if (((PXEventSubscriberAttribute) this).AttributeLevel == null)
      return;
    if (row == null)
    {
      if (((PXEventSubscriberAttribute) this).AttributeLevel != 1)
        return;
      this.DecimalVerifyMode = mode;
    }
    else
    {
      if (((PXEventSubscriberAttribute) this).AttributeLevel != 2)
        return;
      this.DecimalVerifyMode = mode;
      if (this._rowAttributes == null)
        return;
      this._rowAttributes.First<KeyValuePair<object, PXDBQuantityAttribute>>().Value._rowAttributes[row] = this;
    }
  }

  private DecimalVerifyMode GetDecimalVerifyMode(object data)
  {
    PXDBQuantityAttribute quantityAttribute;
    return ((PXEventSubscriberAttribute) this).AttributeLevel == 2 || this._rowAttributes == null || !this._rowAttributes.TryGetValue(data, out quantityAttribute) ? this.DecimalVerifyMode : quantityAttribute.DecimalVerifyMode;
  }

  public virtual PXNotDecimalUnitException VerifyForDecimalValue(PXCache cache, object data)
  {
    if (this._KeyField == (Type) null || data == null || this.DecimalVerifyUnits == InventoryUnitType.None || this.GetDecimalVerifyMode(data) == DecimalVerifyMode.Off)
      return (PXNotDecimalUnitException) null;
    Decimal? originalValue = (Decimal?) cache.GetValue(data, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (originalValue.GetValueOrDefault() == 0M)
      return (PXNotDecimalUnitException) null;
    InventoryItem inventory = this.ReadInventoryItem(cache, data);
    Decimal? baseValue = (Decimal?) cache.GetValue(data, this._ResultField.Name);
    return this.VerifyForDecimalValue(cache, inventory, data, originalValue, baseValue);
  }

  protected virtual PXNotDecimalUnitException VerifyForDecimalValue(
    PXCache cache,
    InventoryItem inventory,
    object data,
    Decimal? originalValue,
    Decimal? baseValue)
  {
    if (baseValue.GetValueOrDefault() == 0M || this.DecimalVerifyUnits == InventoryUnitType.None || inventory == null || originalValue.GetValueOrDefault() == 0M)
      return (PXNotDecimalUnitException) null;
    DecimalVerifyMode decimalVerifyMode = this.GetDecimalVerifyMode(data);
    if (decimalVerifyMode == DecimalVerifyMode.Off)
      return (PXNotDecimalUnitException) null;
    InventoryUnitType inventoryUnitType1 = PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>() ? inventory.ToIntegerUnits() : (inventory.DecimalBaseUnit.GetValueOrDefault() ? InventoryUnitType.None : InventoryUnitType.BaseUnit | InventoryUnitType.SalesUnit | InventoryUnitType.PurchaseUnit);
    if (inventoryUnitType1 == InventoryUnitType.None)
      return (PXNotDecimalUnitException) null;
    string fromUnit = this.GetFromUnit(cache, data);
    InventoryUnitType unitTypes = inventory.ToUnitTypes(fromUnit);
    foreach (InventoryUnitType inventoryUnitType2 in this.DecimalVerifyUnits.Split())
    {
      if ((inventoryUnitType2 & inventoryUnitType1) != InventoryUnitType.None)
      {
        bool flag = false;
        string str;
        Decimal num1;
        if (inventoryUnitType2 == InventoryUnitType.BaseUnit)
        {
          str = inventory.BaseUnit;
          num1 = baseValue.Value;
        }
        else if ((inventoryUnitType2 & unitTypes) > InventoryUnitType.None)
        {
          str = fromUnit;
          num1 = originalValue.Value;
        }
        else
        {
          str = inventory.GetUnitID(inventoryUnitType2);
          num1 = baseValue.Value;
          flag = true;
        }
        if (!(fromUnit != str) || this.ConvertToDecimalVerifyUnits)
        {
          if (flag)
          {
            INUnit inUnit = INUnit.UK.ByInventory.Find(cache.Graph, inventory.InventoryID, str);
            this._ensurePrecision(cache, data);
            Decimal num2 = num1;
            Decimal? unitRate;
            Decimal num3;
            if (!(inUnit.UnitMultDiv == "M"))
            {
              unitRate = inUnit.UnitRate;
              num3 = unitRate.Value;
            }
            else
            {
              unitRate = inUnit.UnitRate;
              num3 = 1M / unitRate.Value;
            }
            num1 = Math.Round(num2 * num3, this._Precision.Value, MidpointRounding.AwayFromZero);
          }
          if (num1 % 1M != 0M)
          {
            PXNotDecimalUnitException decimalUnitException = new PXNotDecimalUnitException(inventoryUnitType2, inventory.InventoryCD, str, decimalVerifyMode == DecimalVerifyMode.Error ? (PXErrorLevel) 4 : (PXErrorLevel) 2);
            cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this).FieldName, data, (object) originalValue, (Exception) decimalUnitException);
            return decimalUnitException;
          }
        }
      }
    }
    return (PXNotDecimalUnitException) null;
  }

  protected class QtyConversionArgs
  {
    public object Row { get; }

    public object NewValue { get; }

    public bool ExternalCall { get; }

    public bool ThrowNotDecimalUnitException { get; set; }

    public QtyConversionArgs(object row, object newValue, bool externalCall)
    {
      this.Row = row;
      this.NewValue = newValue;
      this.ExternalCall = externalCall;
    }
  }
}
