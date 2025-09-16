// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXDBCurrencyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBCurrencyAttribute : 
  PXDBDecimalAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowInsertingSubscriber
{
  internal PXCurrencyAttribute.PXCurrencyHelper _helper;
  private bool FixedPrec;

  protected virtual int? Precision => new int?(this._helper.Precision);

  /// <summary>Constructor</summary>
  /// <param name="keyField">Field in this table used as a key for CurrencyInfo
  /// table. If 'null' is passed then the constructor will try to find field
  /// in this table named 'CuryInfoID'.</param>
  /// <param name="resultField">Field in this table to store the result of
  /// currency conversion. If 'null' is passed then the constructor will try
  /// to find field in this table name of which start with 'base'.</param>
  public PXDBCurrencyAttribute(Type keyField, Type resultField)
  {
    this._helper = new PXCurrencyAttribute.PXCurrencyHelper(keyField, resultField, ((PXEventSubscriberAttribute) this)._FieldName, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._Precision, ((PXEventSubscriberAttribute) this)._AttributeLevel);
  }

  public PXDBCurrencyAttribute(Type precision, Type keyField, Type resultField)
    : base(precision)
  {
    this._helper = new PXCurrencyAttribute.PXCurrencyHelper(keyField, resultField, ((PXEventSubscriberAttribute) this)._FieldName, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._Precision, ((PXEventSubscriberAttribute) this)._AttributeLevel);
    this.FixedPrec = true;
  }

  /// <summary>Constructor</summary>
  /// <param name="precision">Precision for value of 'decimal' type</param>
  /// <param name="keyField">Field in this table used as a key for CurrencyInfo
  /// table. If 'null' is passed then the constructor will try to find field
  /// in this table named 'CuryInfoID'.</param>
  /// <param name="resultField">Field in this table to store the result of
  /// currency conversion. If 'null' is passed then the constructor will try
  /// to find field in this table name of which start with 'base'.</param>
  public PXDBCurrencyAttribute(int precision, Type keyField, Type resultField)
    : base(precision)
  {
    this._helper = new PXCurrencyAttribute.PXCurrencyHelper(keyField, resultField, ((PXEventSubscriberAttribute) this)._FieldName, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._Precision, ((PXEventSubscriberAttribute) this)._AttributeLevel);
    this.FixedPrec = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    this._helper.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    Type itemType1 = BqlCommand.GetItemType(this._helper.ResultField);
    string name1 = this._helper.ResultField.Name;
    PXDBCurrencyAttribute currencyAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) currencyAttribute1, __vmethodptr(currencyAttribute1, ResultFieldUpdating));
    fieldUpdating.AddHandler(itemType1, name1, pxFieldUpdating);
    if (this.FixedPrec)
      this._ensurePrecision(sender, (object) null);
    PXDBDecimalAttribute.SetPrecision(sender, this._helper.ResultField.Name, this.FixedPrec ? this._Precision : new int?());
    if (this.FixedPrec)
      return;
    sender.SetAltered(this._helper.ResultField.Name, true);
    PXGraph.FieldSelectingEvents fieldSelecting = sender.Graph.FieldSelecting;
    Type itemType2 = BqlCommand.GetItemType(this._helper.ResultField);
    string name2 = this._helper.ResultField.Name;
    PXDBCurrencyAttribute currencyAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) currencyAttribute2, __vmethodptr(currencyAttribute2, ResultFieldSelecting));
    fieldSelecting.AddHandler(itemType2, name2, pxFieldSelecting);
  }

  public virtual bool BaseCalc
  {
    get => this._helper.BaseCalc;
    set => this._helper.BaseCalc = value;
  }

  public virtual int FieldOrdinal
  {
    get => ((PXEventSubscriberAttribute) this).FieldOrdinal;
    set
    {
      ((PXEventSubscriberAttribute) this).FieldOrdinal = value;
      this._helper.FieldOrdinal = value;
    }
  }

  public virtual string FieldName
  {
    get => ((PXEventSubscriberAttribute) this).FieldName;
    set
    {
      ((PXEventSubscriberAttribute) this).FieldName = value;
      this._helper.FieldName = value;
    }
  }

  public bool KeepResultValue
  {
    get => this._helper.KeepResultValue;
    set => this._helper.KeepResultValue = value;
  }

  public virtual PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
  {
    PXDBCurrencyAttribute currencyAttribute = (PXDBCurrencyAttribute) ((PXEventSubscriberAttribute) this).Clone(attributeLevel);
    currencyAttribute._helper = this._helper.Clone(attributeLevel);
    return (PXEventSubscriberAttribute) currencyAttribute;
  }

  /// <summary>Converts from amount from Base Currency to another</summary>
  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval)
  {
    PXCurrencyAttribute.CuryConvCury(sender, row, baseval, out curyval);
  }

  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    bool skipRounding)
  {
    PXCurrencyAttribute.CuryConvCury(sender, row, baseval, out curyval, skipRounding);
  }

  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    int precision)
  {
    PXCurrencyAttribute.CuryConvCury(sender, row, baseval, out curyval, precision);
  }

  /// <summary>Converts from amount from Base Currency to another</summary>
  public static void CuryConvCury<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.CuryConvCury<InfoKeyField>(sender, row, baseval, out curyval);
  }

  public static void CuryConvCury<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    bool skipRounding)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.CuryConvCury<InfoKeyField>(sender, row, baseval, out curyval, skipRounding);
  }

  /// <summary>Converts amount to Base Currency</summary>
  public static void CuryConvCury<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval,
    int precision)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.CuryConvCury<InfoKeyField>(sender, row, curyval, out baseval, precision);
  }

  /// <summary>Converts from amount from Base Currency to another</summary>
  public static void CuryConvCury(
    PXCache cache,
    CurrencyInfo info,
    Decimal baseval,
    out Decimal curyval)
  {
    PXCurrencyAttribute.CuryConvCury(cache, info, baseval, out curyval);
  }

  public static void CuryConvCury(
    PXCache cache,
    CurrencyInfo info,
    Decimal baseval,
    out Decimal curyval,
    bool skipRounding)
  {
    PXCurrencyAttribute.CuryConvCury(cache, info, baseval, out curyval, skipRounding);
  }

  /// <summary>Converts amount to Base Currency</summary>
  public static void CuryConvBase(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval)
  {
    PXCurrencyAttribute.CuryConvBase(sender, row, curyval, out baseval);
  }

  public static void CuryConvBase(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval,
    bool skipRounding)
  {
    PXCurrencyAttribute.CuryConvBase(sender, row, curyval, out baseval, skipRounding);
  }

  public static void CuryConvBase(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    int precision)
  {
    PXCurrencyAttribute.CuryConvBase(sender, row, baseval, out curyval, precision);
  }

  /// <summary>Converts amount to Base Currency</summary>
  public static void CuryConvBase<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.CuryConvBase<InfoKeyField>(sender, row, curyval, out baseval);
  }

  /// <summary>Converts amount to Base Currency</summary>
  public static void CuryConvBase(
    PXCache cache,
    CurrencyInfo info,
    Decimal curyval,
    out Decimal baseval)
  {
    PXCurrencyAttribute.CuryConvBase(cache, info, curyval, out baseval);
  }

  public static void CuryConvBase(
    PXCache cache,
    CurrencyInfo info,
    Decimal curyval,
    out Decimal baseval,
    bool skipRounding)
  {
    PXCurrencyAttribute.CuryConvBase(cache, info, curyval, out baseval, skipRounding);
  }

  /// <summary>Converts amount to Base Currency</summary>
  public static void CuryConvBase<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval,
    int precision)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.CuryConvBase<InfoKeyField>(sender, row, curyval, out baseval, precision);
  }

  /// <summary>Rounds amount according to Base Currency rules.</summary>
  public static Decimal BaseRound(PXGraph graph, Decimal value)
  {
    return PXCurrencyAttribute.BaseRound(graph, value);
  }

  /// <summary>
  /// Rounds given amount either according to Base Currency or current Currency rules.
  /// </summary>
  public static Decimal Round(PXCache sender, object row, Decimal val, CMPrecision prec)
  {
    return PXCurrencyAttribute.Round(sender, row, val, prec);
  }

  /// <summary>
  /// Rounds given amount either according to Base Currency or current Currency rules.
  /// </summary>
  public static Decimal Round<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal val,
    CMPrecision prec)
    where InfoKeyField : IBqlField
  {
    return PXCurrencyAttribute.Round<InfoKeyField>(sender, row, val, prec);
  }

  /// <summary>
  /// Rounds given amount according to current Currency rules.
  /// </summary>
  public static Decimal RoundCury(PXCache sender, object row, Decimal val)
  {
    return PXCurrencyAttribute.Round(sender, row, val, CMPrecision.TRANCURY);
  }

  /// <summary>
  /// Rounds given amount according to current Currency rules or using the custom precision.
  /// </summary>
  public static Decimal RoundCury(PXCache sender, object row, Decimal val, int? customPrecision)
  {
    return !customPrecision.HasValue ? PXDBCurrencyAttribute.RoundCury(sender, row, val) : PXCurrencyAttribute.Round(sender, row, val, CMPrecision.CUSTOM, customPrecision.Value);
  }

  /// <summary>
  /// Rounds given amount according to current Currency rules.
  /// </summary>
  public static Decimal RoundCury<InfoKeyField>(PXCache sender, object row, Decimal val) where InfoKeyField : IBqlField
  {
    return PXCurrencyAttribute.Round<InfoKeyField>(sender, row, val, CMPrecision.TRANCURY);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (obj != null && ((e.Operation & 3) == 1 && !object.Equals(obj, sender.GetValueOriginal(e.Row, ((PXEventSubscriberAttribute) this)._FieldName)) || (e.Operation & 3) == 2))
    {
      int currencyInfoPrecision;
      if (this.FixedPrec)
      {
        this._ensurePrecision(sender, e.Row);
        currencyInfoPrecision = this._Precision.Value;
      }
      else
        currencyInfoPrecision = this._helper.GetCurrencyInfoPrecision(sender, e.Row);
      obj = (object) Math.Round((Decimal) obj, currencyInfoPrecision, MidpointRounding.AwayFromZero);
    }
    this._helper.CalcBaseValues(sender, new PXFieldVerifyingEventArgs(e.Row, obj, false));
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    this._helper.CalcBaseValues(sender, new PXFieldVerifyingEventArgs(e.Row, obj, e.ExternalCall));
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null)
    {
      base.FieldVerifying(sender, e);
      this._ensurePrecision(sender, e.Row);
      int decimals = this.FixedPrec ? this._Precision.Value : this._helper.GetCurrencyInfoPrecision(sender, e.Row);
      e.NewValue = (object) Math.Round((Decimal) e.NewValue, decimals, MidpointRounding.AwayFromZero);
    }
    this._helper.CalcBaseValues(sender, e);
  }

  /// <summary>
  /// Automaticaly Converts and updates Base Currency field with the current value.
  /// BaseCurrency field is supplied through Field
  /// </summary>
  public static void CalcBaseValues<Field>(PXCache cache, object data) where Field : IBqlField
  {
    PXCurrencyAttribute.CalcBaseValues<Field>(cache, data);
  }

  /// <summary>
  /// Automaticaly Converts and updates Base Currency field with the current value.
  /// </summary>
  public static void CalcBaseValues(PXCache cache, object data, string name)
  {
    PXCurrencyAttribute.CalcBaseValues(cache, data, name);
  }

  public static void SetBaseCalc<Field>(PXCache cache, object data, bool isBaseCalc) where Field : IBqlField
  {
    PXCurrencyAttribute.SetBaseCalc<Field>(cache, data, isBaseCalc);
  }

  public static void SetBaseCalc(PXCache cache, object data, string name, bool isBaseCalc)
  {
    PXCurrencyAttribute.SetBaseCalc(cache, data, name, isBaseCalc);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    this._helper.FieldSelecting(sender, e, this.FixedPrec);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (sender.Graph.Accessinfo.CuryViewState && e.Row != null && e.NewValue != null && sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) == e.NewValue)
      e.NewValue = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    else if (this.FixedPrec)
      base.FieldUpdating(sender, e);
    else
      this._helper.FieldUpdating(sender, e);
  }

  public virtual void ResultFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    this._helper.ResultFieldSelecting(sender, e, this.FixedPrec);
  }

  public virtual void ResultFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (this.FixedPrec)
      base.FieldUpdating(sender, e);
    else
      this._helper.ResultFieldUpdating(sender, e);
  }
}
