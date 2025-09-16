// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnitAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
[PXUIField]
public class INUnitAttribute : 
  PXEntityAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowSelectedSubscriber,
  IPXRowPersistingSubscriber
{
  private readonly INUnitAttribute.VerifyingMode _verifyingMode;
  protected Type InventoryType;
  protected Type BaseUnitType;
  private string _AccountIDField;
  private string _AccountRequireUnitsField;
  private PXSelectorAttribute _selectorWithAggregate;
  private PXSelectorAttribute _selectorNoAggregate;
  /// <summary>run verifying process if inventory was setted</summary>
  private readonly bool _verifyOnSettedInventory = true;

  public virtual bool DirtyRead
  {
    get => base.DirtyRead;
    set
    {
      if (value == base.DirtyRead || ((PXEventSubscriberAttribute) this).AttributeLevel != null)
        return;
      ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = value ? (PXEventSubscriberAttribute) this._selectorNoAggregate : (PXEventSubscriberAttribute) this._selectorWithAggregate;
      base.DirtyRead = value;
    }
  }

  public bool VerifyOnCopyPaste { get; set; } = true;

  public bool SupportNAInventory { get; set; }

  protected INUnitAttribute(INUnitAttribute.VerifyingMode verifyingMode)
  {
    this._verifyingMode = verifyingMode;
  }

  public INUnitAttribute()
    : this(INUnitAttribute.VerifyingMode.UnitCatalog)
  {
    this.Init(typeof (Search4<INUnit.fromUnit, Where<INUnit.unitType, Equal<INUnitType.global>>, Aggregate<GroupBy<INUnit.fromUnit>>>));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dummy">Is dummy parameter. Was created for constructor type identification</param>
  /// <param name="BaseUnitType"></param>
  public INUnitAttribute(Type dummy, Type BaseUnitType)
    : this(INUnitAttribute.VerifyingMode.GlobalUnitConversion)
  {
    this.BaseUnitType = BaseUnitType;
    this.Init(((IBqlTemplate) BqlTemplate.OfCommand<Search<INUnit.fromUnit, Where<INUnit.unitType, Equal<INUnitType.global>, And<INUnit.toUnit, Equal<Optional<BqlPlaceholder.A>>>>>>.Replace<BqlPlaceholder.A>(BaseUnitType)).ToType());
  }

  public INUnitAttribute(Type InventoryType, Type AccountIDType, Type AccountRequireUnitsType)
    : this()
  {
    this.InventoryType = InventoryType;
    this._AccountIDField = AccountIDType.Name;
    this._AccountRequireUnitsField = AccountRequireUnitsType.Name;
    this._verifyOnSettedInventory = false;
  }

  public INUnitAttribute(Type InventoryType)
    : this(INUnitAttribute.VerifyingMode.InventoryUnitConversion)
  {
    this.InventoryType = InventoryType;
    this.Init(((IBqlTemplate) BqlTemplate.OfCommand<Search<INUnit.fromUnit, Where<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.inventoryID, Equal<Optional<BqlPlaceholder.A>>, Or<INUnit.unitType, Equal<INUnitType.global>, And<Optional<BqlPlaceholder.A>, IsNull>>>>>>.Replace<BqlPlaceholder.A>(InventoryType)).ToType());
  }

  private void Init(Type searchType)
  {
    this.Init(searchType, BqlCommand.CreateInstance(new Type[1]
    {
      searchType
    }).AggregateNew<Aggregate<GroupBy<INUnit.fromUnit>>>().GetType());
  }

  protected void Init(Type searchNoAggregate, Type searchWithAggregate)
  {
    this._selectorNoAggregate = new PXSelectorAttribute(searchNoAggregate);
    this._selectorWithAggregate = searchNoAggregate == searchWithAggregate ? this._selectorNoAggregate : new PXSelectorAttribute(searchWithAggregate);
    ((PXAggregateAttribute) this)._Attributes.Add(base.DirtyRead ? (PXEventSubscriberAttribute) this._selectorNoAggregate : (PXEventSubscriberAttribute) this._selectorWithAggregate);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber))
      subscribers.Add(this as ISubscriber);
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || string.IsNullOrEmpty(this._AccountIDField) || string.IsNullOrEmpty(this._AccountRequireUnitsField))
      return;
    object accountID = sender.GetValue(e.Row, this._AccountIDField);
    if (sender.GetValue(e.Row, this._AccountRequireUnitsField) != null)
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find(sender.Graph, accountID as int?);
    if (account != null)
      sender.SetValue(e.Row, this._AccountRequireUnitsField, (object) account.RequireUnits);
    else
      sender.SetValue(e.Row, this._AccountRequireUnitsField, (object) null);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (string.IsNullOrEmpty(this._AccountRequireUnitsField) || (e.Operation & 3) == 3)
      return;
    object obj1 = sender.GetValue(e.Row, this._AccountRequireUnitsField);
    string str = (string) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (obj1 == null || !(bool) obj1 || !string.IsNullOrEmpty(str))
      return;
    object obj2 = sender.GetValue(e.Row, this._AccountIDField);
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      obj2
    }));
    if (account == null)
      throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) ((PXEventSubscriberAttribute) this)._FieldName
      });
    throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) null, "{0} may not be empty for Account '{1}'", new object[2]
    {
      (object) ((PXEventSubscriberAttribute) this)._FieldName,
      (object) account.AccountCD
    });
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null || EnumerableExtensions.IsIn<string>(((PXEventSubscriberAttribute) this)._FieldName, "FromUnit", "ToUnit") || !this.VerifyOnCopyPaste && sender.Graph.IsCopyPasteContext || !this._verifyOnSettedInventory && sender.GetValue(e.Row, this.InventoryType.Name) != null)
      return;
    INUnit unit = this.ReadUnit(sender, e.Row, (string) e.NewValue);
    this.UnitVerifying(sender, e, unit);
  }

  protected virtual void UnitVerifying(PXCache cache, PXFieldVerifyingEventArgs e, INUnit unit)
  {
    if (unit != null)
      return;
    if (e.ExternalCall)
      throw new PXSetPropertyException(this._verifyingMode == INUnitAttribute.VerifyingMode.UnitCatalog ? "'{0}' cannot be found in the system." : "'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
      {
        (object) ((PXEventSubscriberAttribute) this)._FieldName
      });
    if (this._verifyingMode == INUnitAttribute.VerifyingMode.UnitCatalog)
      throw new PXSetPropertyException("{0} '{1}' cannot be found in the system.", new object[2]
      {
        (object) ((PXEventSubscriberAttribute) this)._FieldName,
        e.NewValue
      });
    InventoryItem inventoryItem = this.ReadInventoryItem(cache, e.Row);
    if (inventoryItem != null)
      throw new PXSetPropertyException("The {0} UOM is not specified for the {1} item. Select another UOM in the line with this item, or specify the {0} UOM in the settings of the item.", new object[2]
      {
        e.NewValue,
        (object) inventoryItem.InventoryCD
      });
    throw new PXSetPropertyException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
    {
      (object) ((PXEventSubscriberAttribute) this)._FieldName,
      e.NewValue
    });
  }

  protected virtual INUnit ReadUnit(PXCache sender, object data, string fromUnitID)
  {
    INUnit inUnit = (INUnit) null;
    switch (this._verifyingMode)
    {
      case INUnitAttribute.VerifyingMode.UnitCatalog:
        inUnit = INUnitAttribute.ReadGlobalUnit(sender, fromUnitID);
        break;
      case INUnitAttribute.VerifyingMode.GlobalUnitConversion:
        string baseUnit = this.GetBaseUnit(sender, data);
        inUnit = INUnit.UK.ByGlobal.Find(sender.Graph, fromUnitID, baseUnit);
        break;
      case INUnitAttribute.VerifyingMode.InventoryUnitConversion:
        InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, (int?) this.GetSelectorParameterValue(sender, data, this.InventoryType));
        if (inventoryItem != null)
        {
          if (this.SupportNAInventory)
          {
            int? inventoryId = (int?) inventoryItem?.InventoryID;
            int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
            if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
              goto label_6;
          }
          inUnit = INUnit.UK.ByInventory.Find(sender.Graph, inventoryItem.InventoryID, fromUnitID);
          break;
        }
label_6:
        inUnit = INUnitAttribute.ReadGlobalUnit(sender, fromUnitID);
        break;
    }
    return inUnit;
  }

  public virtual INUnit ReadConversion(PXCache cache, object data, string fromUnitID)
  {
    return this.ReadConversionInfo(cache, data, fromUnitID)?.Conversion;
  }

  public virtual InventoryItem ReadInventoryItem(PXCache cache, object data)
  {
    return this._verifyingMode == INUnitAttribute.VerifyingMode.InventoryUnitConversion ? InventoryItem.PK.Find(cache.Graph, (int?) this.GetSelectorParameterValue(cache, data, this.InventoryType)) : (InventoryItem) null;
  }

  public virtual ConversionInfo ReadConversionInfo(PXCache cache, object data, string fromUnitID)
  {
    if (string.IsNullOrEmpty(fromUnitID))
      return (ConversionInfo) null;
    InventoryItem inventory = (InventoryItem) null;
    INUnit conversion;
    switch (this._verifyingMode)
    {
      case INUnitAttribute.VerifyingMode.Custom:
        return (ConversionInfo) null;
      case INUnitAttribute.VerifyingMode.UnitCatalog:
        conversion = this.EmptyConversion(fromUnitID);
        break;
      case INUnitAttribute.VerifyingMode.GlobalUnitConversion:
        string baseUnit = this.GetBaseUnit(cache, data);
        conversion = fromUnitID == baseUnit ? this.EmptyConversion(fromUnitID) : INUnit.UK.ByGlobal.Find(cache.Graph, fromUnitID, baseUnit);
        break;
      case INUnitAttribute.VerifyingMode.InventoryUnitConversion:
        inventory = InventoryItem.PK.Find(cache.Graph, (int?) this.GetSelectorParameterValue(cache, data, this.InventoryType));
        if (inventory != null)
        {
          if (this.SupportNAInventory)
          {
            int? inventoryId = (int?) inventory?.InventoryID;
            int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
            if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue)
              goto label_9;
          }
          if (fromUnitID == inventory.BaseUnit)
          {
            conversion = this.EmptyConversion(fromUnitID);
            conversion.UnitType = new short?((short) 1);
            conversion.InventoryID = inventory.InventoryID;
            break;
          }
          conversion = INUnit.UK.ByInventory.Find(cache.Graph, inventory.InventoryID, fromUnitID);
          break;
        }
label_9:
        conversion = this.EmptyConversion(fromUnitID);
        break;
      default:
        throw new ArgumentOutOfRangeException("_verifyingMode");
    }
    return new ConversionInfo(conversion, inventory);
  }

  private INUnit EmptyConversion(string unit)
  {
    return new INUnit()
    {
      UnitType = new short?((short) 3),
      FromUnit = unit,
      ToUnit = unit,
      UnitRate = new Decimal?((Decimal) 1),
      PriceAdjustmentMultiplier = new Decimal?((Decimal) 1),
      UnitMultDiv = "M"
    };
  }

  protected virtual string GetBaseUnit(PXCache cache, object data)
  {
    if (this.BaseUnitType == (Type) null)
      return (string) null;
    string selectorParameterValue = (string) this.GetSelectorParameterValue(cache, data, this.BaseUnitType);
    if (selectorParameterValue == null)
    {
      Type itemType = BqlCommand.GetItemType(this.BaseUnitType);
      PXCache cach = cache.Graph.Caches[itemType];
      if (cach.Keys.Count == 0)
      {
        BqlCommand command = BqlTemplate.OfCommand<Select<BqlPlaceholder.A>>.Replace<BqlPlaceholder.A>(itemType).ToCommand();
        object obj = cache.Graph.TypedViews.GetView(command, true).SelectSingle(Array.Empty<object>());
        selectorParameterValue = (string) cach.GetValue(obj, this.BaseUnitType.Name);
      }
    }
    return selectorParameterValue;
  }

  protected virtual object GetSelectorParameterValue(
    PXCache cache,
    object data,
    Type parameterFieldType)
  {
    object[] objArray = cache.Graph.TypedViews.GetView(this.NonDimensionSelectorAttribute.GetSelect(), !this.NonDimensionSelectorAttribute.DirtyRead).PrepareParameters(new object[1]
    {
      data
    }, (object[]) null);
    return objArray != null && objArray.Length != 0 ? objArray[0] : (object) null;
  }

  private static INUnit ReadGlobalUnit(PXCache sender, string fromUnitID)
  {
    return PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXSelectReadonly<INUnit, Where<INUnit.unitType, Equal<INUnitType.global>, And<INUnit.fromUnit, Equal<Required<INUnit.fromUnit>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
    {
      (object) fromUnitID
    }));
  }

  public static Decimal ConvertFromBase<InventoryIDField, UOMField>(
    PXCache sender,
    object Row,
    Decimal value,
    INPrecision prec)
    where InventoryIDField : IBqlField
    where UOMField : IBqlField
  {
    if (value == 0M)
      return 0M;
    string ToUnit = (string) sender.GetValue<UOMField>(Row);
    try
    {
      return INUnitAttribute.ConvertFromBase<InventoryIDField>(sender, Row, ToUnit, value, prec);
    }
    catch (PXUnitConversionException ex)
    {
      sender.RaiseExceptionHandling<UOMField>(Row, (object) ToUnit, (Exception) ex);
    }
    return 0M;
  }

  public static Decimal ConvertFromBase<InventoryIDField>(
    PXCache sender,
    object Row,
    string ToUnit,
    Decimal value,
    INPrecision prec)
    where InventoryIDField : IBqlField
  {
    return INUnitAttribute.Convert<InventoryIDField>(sender, Row, ToUnit, value, prec, true);
  }

  public static Decimal ConvertFromBase<InventoryIDField>(
    PXCache sender,
    object Row,
    string ToUnit,
    Decimal value,
    INPrecision prec,
    INMidpointRounding rounding)
    where InventoryIDField : IBqlField
  {
    return INUnitAttribute.Convert<InventoryIDField>(sender, Row, ToUnit, value, prec, true, rounding);
  }

  public static Decimal ConvertToBase<InventoryIDField, UOMField>(
    PXCache sender,
    object Row,
    Decimal value,
    INPrecision prec)
    where InventoryIDField : IBqlField
    where UOMField : IBqlField
  {
    if (value == 0M)
      return 0M;
    string FromUnit = (string) sender.GetValue<UOMField>(Row);
    try
    {
      return INUnitAttribute.ConvertToBase<InventoryIDField>(sender, Row, FromUnit, value, prec);
    }
    catch (PXUnitConversionException ex)
    {
      sender.RaiseExceptionHandling<UOMField>(Row, (object) FromUnit, (Exception) ex);
    }
    return 0M;
  }

  public static Decimal ConvertToBase<InventoryIDField>(
    PXCache sender,
    object Row,
    string FromUnit,
    Decimal value,
    INPrecision prec)
    where InventoryIDField : IBqlField
  {
    return INUnitAttribute.Convert<InventoryIDField>(sender, Row, FromUnit, value, prec, false);
  }

  public static Decimal ConvertFromTo<InventoryIDField>(
    PXCache sender,
    object Row,
    string FromUnit,
    string ToUnit,
    Decimal value,
    INPrecision prec)
    where InventoryIDField : IBqlField
  {
    if (string.Equals(FromUnit, ToUnit))
      return value;
    Decimal num = INUnitAttribute.ConvertToBase<InventoryIDField>(sender, Row, FromUnit, value, prec);
    return INUnitAttribute.ConvertFromBase<InventoryIDField>(sender, Row, ToUnit, num, prec);
  }

  private static Decimal Convert<InventoryIDField>(
    PXCache sender,
    object Row,
    string FromUnit,
    Decimal value,
    INPrecision prec,
    bool ViceVersa,
    INMidpointRounding rounding)
    where InventoryIDField : IBqlField
  {
    if (value == 0M || FromUnit == null)
      return value;
    object inventoryID = sender.GetValue<InventoryIDField>(Row);
    if (inventoryID == null)
      return value;
    InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, (int?) inventoryID);
    if (inventoryItem == null)
    {
      PXTrace.WriteError("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        (object) "Inventory Item",
        inventoryID
      });
      throw new PXUnitConversionException();
    }
    if (FromUnit == inventoryItem.BaseUnit)
      return INUnitAttribute.Round(value, prec, rounding);
    return INUnitAttribute.ConvertValue(value, INUnit.UK.ByInventory.Find(sender.Graph, inventoryItem.InventoryID, FromUnit) ?? throw new PXUnitConversionException(), prec, ViceVersa, INUnitAttribute.UsePriceAdjustmentMultiplier(sender.Graph));
  }

  private static Decimal Convert<InventoryIDField>(
    PXCache sender,
    object Row,
    string FromUnit,
    Decimal value,
    INPrecision prec,
    bool ViceVersa)
    where InventoryIDField : IBqlField
  {
    return INUnitAttribute.Convert<InventoryIDField>(sender, Row, FromUnit, value, prec, ViceVersa, INMidpointRounding.ROUND);
  }

  public static Decimal ConvertFromBase(
    PXCache sender,
    int? InventoryID,
    string ToUnit,
    Decimal value,
    INPrecision prec)
  {
    return INUnitAttribute.Convert(sender, InventoryID, ToUnit, value, prec, true);
  }

  public static Decimal ConvertToBase(
    PXCache sender,
    int? InventoryID,
    string FromUnit,
    Decimal value,
    INPrecision prec)
  {
    return INUnitAttribute.Convert(sender, InventoryID, FromUnit, value, prec, false);
  }

  public static Decimal ConvertToBase(
    PXCache sender,
    int? InventoryID,
    string FromUnit,
    Decimal value,
    Decimal? baseValue,
    INPrecision prec)
  {
    return INUnitAttribute.Convert(sender, InventoryID, FromUnit, value, baseValue, prec, false);
  }

  private static Decimal Convert(
    PXCache sender,
    int? InventoryID,
    string FromUnit,
    Decimal value,
    INPrecision prec,
    bool ViceVersa)
  {
    return INUnitAttribute.Convert(sender, InventoryID, FromUnit, value, new Decimal?(), prec, ViceVersa);
  }

  private static Decimal Convert(
    PXCache sender,
    int? InventoryID,
    string FromUnit,
    Decimal value,
    Decimal? baseValue,
    INPrecision prec,
    bool ViceVersa)
  {
    if (value == 0M || FromUnit == null)
      return value;
    InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, InventoryID);
    if (inventoryItem == null)
    {
      PXTrace.WriteError("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        (object) "Inventory Item",
        (object) InventoryID
      });
      throw new PXUnitConversionException();
    }
    if (FromUnit == inventoryItem.BaseUnit)
      return baseValue.HasValue && INUnitAttribute.Round(baseValue.Value, prec) == value ? baseValue.Value : INUnitAttribute.Round(value, prec);
    INUnit unit = INUnit.UK.ByInventory.Find(sender.Graph, inventoryItem.InventoryID, FromUnit);
    if (unit == null)
      throw new PXUnitConversionException();
    bool usePriceAdjustmentMultiplier = INUnitAttribute.UsePriceAdjustmentMultiplier(sender.Graph);
    return baseValue.HasValue && INUnitAttribute.ConvertValue(baseValue.Value, unit, prec, !ViceVersa, usePriceAdjustmentMultiplier) == value ? baseValue.Value : INUnitAttribute.ConvertValue(value, unit, prec, ViceVersa, usePriceAdjustmentMultiplier);
  }

  internal static Decimal Convert(
    PXCache sender,
    INUnit unit,
    Decimal value,
    INPrecision prec,
    bool ViceVersa)
  {
    if (value == 0M)
      return 0M;
    return unit == null ? value : INUnitAttribute.ConvertValue(value, unit, prec, ViceVersa, INUnitAttribute.UsePriceAdjustmentMultiplier(sender.Graph));
  }

  public static Decimal ConvertFromBase(
    PXCache sender,
    INUnit unit,
    Decimal value,
    INPrecision prec)
  {
    return INUnitAttribute.Convert(sender, unit, value, prec, true);
  }

  public static Decimal ConvertToBase(
    PXCache sender,
    INUnit unit,
    Decimal value,
    INPrecision prec)
  {
    return INUnitAttribute.Convert(sender, unit, value, prec, false);
  }

  public static bool IsFractional(INUnit conv)
  {
    if (conv == null)
      return false;
    Decimal? unitRate;
    if (conv.UnitMultDiv == "D")
    {
      unitRate = conv.UnitRate;
      Decimal num = 1M;
      if (!(unitRate.GetValueOrDefault() == num & unitRate.HasValue))
        return true;
    }
    unitRate = conv.UnitRate;
    return Decimal.Remainder(unitRate.Value, 1M) != 0M;
  }

  /// <summary>Converts units using Global converion Table.</summary>
  /// <exception cref="T:PX.Data.PXException">Is thrown if converion is not found in the table.</exception>
  [Obsolete]
  public static Decimal ConvertGlobalUnits(
    PXGraph graph,
    string from,
    string to,
    Decimal value,
    INPrecision prec)
  {
    Decimal result = 0M;
    if (INUnitAttribute.TryConvertGlobalUnits(graph, from, to, value, prec, out result))
      return result;
    throw new PXException("Unit Conversion is not setup on 'Units Of Measure' screen. Please setup Unit Conversion FROM {0} TO {1}.", new object[2]
    {
      (object) from,
      (object) to
    });
  }

  /// <summary>Converts units using Global converion Table.</summary>
  /// <exception cref="T:PX.Objects.IN.PXUnitConversionException">Is thrown if converion is not found in the table.</exception>
  public static Decimal ConvertGlobal(
    PXGraph graph,
    string from,
    string to,
    Decimal value,
    INPrecision prec)
  {
    Decimal result = 0M;
    if (INUnitAttribute.TryConvertGlobalUnits(graph, from, to, value, prec, out result))
      return result;
    throw new PXUnitConversionException(from, to);
  }

  public static bool TryConvertGlobalUnits(
    PXGraph graph,
    string from,
    string to,
    Decimal value,
    INPrecision prec,
    out Decimal result)
  {
    if (value == 0M)
    {
      result = 0M;
      return true;
    }
    result = 0M;
    if (from == to)
    {
      result = value;
      return true;
    }
    INUnit unit = INUnit.UK.ByGlobal.Find(graph, from, to);
    if (unit == null)
      return false;
    result = INUnitAttribute.ConvertValue(value, unit, prec);
    return true;
  }

  public static Decimal ConvertValue(
    Decimal value,
    INUnit unit,
    INPrecision prec,
    bool viceVersa,
    bool usePriceAdjustmentMultiplier,
    INMidpointRounding rounding)
  {
    Decimal? nullable;
    if (unit.UnitMultDiv == "M" && !viceVersa || unit.UnitMultDiv == "D" & viceVersa)
    {
      Decimal num1 = value;
      nullable = unit.UnitRate;
      Decimal num2 = nullable.Value;
      value = num1 * num2;
    }
    else
    {
      Decimal num3 = value;
      nullable = unit.UnitRate;
      Decimal num4 = nullable.Value;
      value = num3 / num4;
    }
    if (usePriceAdjustmentMultiplier && prec == INPrecision.UNITCOST)
    {
      if (viceVersa)
      {
        Decimal num5 = value;
        nullable = unit.PriceAdjustmentMultiplier;
        Decimal num6 = nullable.Value;
        value = num5 / num6;
      }
      else
      {
        Decimal num7 = value;
        nullable = unit.PriceAdjustmentMultiplier;
        Decimal num8 = nullable.Value;
        value = num7 * num8;
      }
    }
    return INUnitAttribute.Round(value, prec, rounding);
  }

  public static Decimal ConvertValue(
    Decimal value,
    INUnit unit,
    INPrecision prec,
    bool viceVersa = false,
    bool usePriceAdjustmentMultiplier = false)
  {
    return INUnitAttribute.ConvertValue(value, unit, prec, viceVersa, usePriceAdjustmentMultiplier, INMidpointRounding.ROUND);
  }

  private static Decimal Round(Decimal value, INPrecision prec)
  {
    return INUnitAttribute.Round(value, prec, INMidpointRounding.ROUND);
  }

  private static Decimal Round(Decimal value, INPrecision prec, INMidpointRounding rounding)
  {
    if (prec == INPrecision.NOROUND)
      return value;
    int num = INUnitAttribute.DefinePrecision(prec);
    if (rounding == INMidpointRounding.ROUND)
      return Math.Round(value, num, MidpointRounding.AwayFromZero);
    return !(value > 0M) ? Math.Ceiling(value * (Decimal) (long) Math.Pow(10.0, (double) num)) / (Decimal) (long) Math.Pow(10.0, (double) num) : Math.Floor(value * (Decimal) (long) Math.Pow(10.0, (double) num)) / (Decimal) (long) Math.Pow(10.0, (double) num);
  }

  private static int DefinePrecision(INPrecision prec)
  {
    int num = 6;
    switch (prec)
    {
      case INPrecision.QUANTITY:
        num = CommonSetupDecPl.Qty;
        break;
      case INPrecision.UNITCOST:
        num = CommonSetupDecPl.PrcCst;
        break;
    }
    return num;
  }

  public static bool UsePriceAdjustmentMultiplier(PXGraph graph)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
      return false;
    switch (graph)
    {
      case ARInvoiceEntry _:
      case SOOrderEntry _:
      case ARCashSaleEntry _:
        return ((bool?) PXResultset<SOSetup>.op_Implicit(PXSelectBase<SOSetup, PXSelect<SOSetup>.Config>.SelectSingleBound(graph, (object[]) null, Array.Empty<object>()))?.UsePriceAdjustmentMultiplier).GetValueOrDefault();
      default:
        return false;
    }
  }

  public enum VerifyingMode
  {
    Custom,
    UnitCatalog,
    GlobalUnitConversion,
    InventoryUnitConversion,
  }
}
