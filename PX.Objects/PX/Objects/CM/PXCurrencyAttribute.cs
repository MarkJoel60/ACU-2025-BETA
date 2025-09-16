// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXCurrencyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Dashboards;
using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.CM;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXCurrencyAttribute : 
  PXDecimalAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowInsertingSubscriber
{
  internal PXCurrencyAttribute.PXCurrencyHelper _helper;
  protected bool _FixedPrec;

  protected virtual int? Precision => new int?(this._helper.Precision);

  /// <summary>Constructor</summary>
  /// <param name="keyField">Field in this table used as a key for CurrencyInfo
  /// table. If 'null' is passed then the constructor will try to find field
  /// in this table named 'CuryInfoID'.</param>
  /// <param name="resultField">Field in this table to store the result of
  /// currency conversion. If 'null' is passed then the constructor will try
  /// to find field in this table name of which start with 'base'.</param>
  public PXCurrencyAttribute(Type keyField, Type resultField)
  {
    this._helper = new PXCurrencyAttribute.PXCurrencyHelper(keyField, resultField, ((PXEventSubscriberAttribute) this)._FieldName, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._Precision, ((PXEventSubscriberAttribute) this)._AttributeLevel);
  }

  /// <summary>Constructor</summary>
  /// <param name="precision">Precision for value of 'decimal' type</param>
  /// <param name="keyField">Field in this table used as a key for CurrencyInfo
  /// table. If 'null' is passed then the constructor will try to find field
  /// in this table named 'CuryInfoID'.</param>
  /// <param name="resultField">Field in this table to store the result of
  /// currency conversion. If 'null' is passed then the constructor will try
  /// to find field in this table name of which start with 'base'.</param>
  public PXCurrencyAttribute(int precision, Type keyField, Type resultField)
    : base(precision)
  {
    this._helper = new PXCurrencyAttribute.PXCurrencyHelper(keyField, resultField, ((PXEventSubscriberAttribute) this)._FieldName, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._Precision, ((PXEventSubscriberAttribute) this)._AttributeLevel);
  }

  public PXCurrencyAttribute(Type precision, Type keyField, Type resultField)
    : base(precision)
  {
    this._helper = new PXCurrencyAttribute.PXCurrencyHelper(keyField, resultField, ((PXEventSubscriberAttribute) this)._FieldName, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._Precision, ((PXEventSubscriberAttribute) this)._AttributeLevel);
    this._FixedPrec = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    this._helper.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    Type itemType1 = BqlCommand.GetItemType(this._helper.ResultField);
    string name1 = this._helper.ResultField.Name;
    PXCurrencyAttribute currencyAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) currencyAttribute1, __vmethodptr(currencyAttribute1, ResultFieldUpdating));
    fieldUpdating.AddHandler(itemType1, name1, pxFieldUpdating);
    PXDecimalAttribute.SetPrecision(sender, this._helper.ResultField.Name, this.FixedPrec ? this._Precision : new int?());
    if (this.FixedPrec)
      return;
    sender.SetAltered(this._helper.ResultField.Name, true);
    PXGraph.FieldSelectingEvents fieldSelecting = sender.Graph.FieldSelecting;
    Type itemType2 = BqlCommand.GetItemType(this._helper.ResultField);
    string name2 = this._helper.ResultField.Name;
    PXCurrencyAttribute currencyAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) currencyAttribute2, __vmethodptr(currencyAttribute2, ResultFieldSelecting));
    fieldSelecting.AddHandler(itemType2, name2, pxFieldSelecting);
  }

  public bool FixedPrec => this._FixedPrec;

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

  public virtual PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
  {
    PXCurrencyAttribute currencyAttribute = (PXCurrencyAttribute) ((PXEventSubscriberAttribute) this).Clone(attributeLevel);
    currencyAttribute._helper = this._helper.Clone(attributeLevel);
    return (PXEventSubscriberAttribute) currencyAttribute;
  }

  public static bool IsNullOrEmpty(Decimal? val)
  {
    if (!val.HasValue)
      return true;
    Decimal? nullable = val;
    Decimal num = 0M;
    return nullable.GetValueOrDefault() == num & nullable.HasValue;
  }

  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(sender, row, baseval, out curyval);
  }

  public static void CuryConvCury<CuryField>(PXCache sender, object row) where CuryField : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury<CuryField>(sender, row);
  }

  public static void CuryConvCury<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    bool skipRounding)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury<InfoKeyField>(sender, row, baseval, out curyval, skipRounding);
  }

  public static void CuryConvCury<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury<InfoKeyField>(sender, row, baseval, out curyval);
  }

  public static void CuryConvCury<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    int precision)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury<InfoKeyField>(sender, row, baseval, out curyval, precision);
  }

  public static void CuryConvCury(
    PXCache cache,
    CurrencyInfo info,
    Decimal baseval,
    out Decimal curyval)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(cache, info, baseval, out curyval);
  }

  public static void CuryConvCury(
    PXCache cache,
    CurrencyInfo info,
    Decimal baseval,
    out Decimal curyval,
    bool skipRounding)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(cache, info, baseval, out curyval, skipRounding);
  }

  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    bool skipRounding)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(sender, row, baseval, out curyval, skipRounding);
  }

  public static void CuryConvCury(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    int precision)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(sender, row, baseval, out curyval, precision);
  }

  public static void CuryConvBase(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(sender, row, curyval, out baseval);
  }

  public static void CuryConvBase<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase<InfoKeyField>(sender, row, curyval, out baseval);
  }

  public static void CuryConvBase<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval,
    int precision)
    where InfoKeyField : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase<InfoKeyField>(sender, row, curyval, out baseval, precision);
  }

  public static void CuryConvBase(
    PXCache cache,
    CurrencyInfo info,
    Decimal curyval,
    out Decimal baseval)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(cache, info, curyval, out baseval);
  }

  public static void CuryConvBase(
    PXCache cache,
    CurrencyInfo info,
    Decimal curyval,
    out Decimal baseval,
    bool skipRounding)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(cache, info, curyval, out baseval, skipRounding);
  }

  public static void CuryConvBase(
    PXCache sender,
    object row,
    Decimal curyval,
    out Decimal baseval,
    bool skipRounding)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(sender, row, curyval, out baseval, skipRounding);
  }

  public static void CuryConvBase(
    PXCache sender,
    object row,
    Decimal baseval,
    out Decimal curyval,
    int precision)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(sender, row, baseval, out curyval, precision);
  }

  public static Decimal BaseRound(PXGraph graph, Decimal value)
  {
    if (string.IsNullOrEmpty(graph.Accessinfo.BaseCuryID))
      LoggerExtensions.WithStack(PXTelemetryLogger.ForTelemetry(PXTrace.Logger, nameof (PXCurrencyAttribute), nameof (BaseRound))).Warning("Accessinfo.BaseCuryID is empty");
    return PXCurrencyAttribute.PXCurrencyHelper.BaseRound(graph, value);
  }

  public static Decimal BaseRound(PXGraph graph, Decimal? value)
  {
    return PXCurrencyAttribute.BaseRound(graph, value.Value);
  }

  public static Decimal Round(PXCache sender, object row, Decimal val, CMPrecision prec)
  {
    return PXCurrencyAttribute.PXCurrencyHelper.Round(sender, row, val, prec);
  }

  public static Decimal Round(
    PXCache sender,
    object row,
    Decimal val,
    CMPrecision prec,
    int customPrecision)
  {
    return PXCurrencyAttribute.PXCurrencyHelper.Round(val, customPrecision);
  }

  public static Decimal Round<InfoKeyField>(
    PXCache sender,
    object row,
    Decimal val,
    CMPrecision prec)
    where InfoKeyField : IBqlField
  {
    return PXCurrencyAttribute.PXCurrencyHelper.Round<InfoKeyField>(sender, row, val, prec);
  }

  public static Decimal RoundCury(PXCache sender, object row, Decimal val)
  {
    return PXCurrencyAttribute.PXCurrencyHelper.Round(sender, row, val, CMPrecision.TRANCURY);
  }

  public static Decimal RoundCury<InfoKeyField>(PXCache sender, object row, Decimal val) where InfoKeyField : IBqlField
  {
    return PXCurrencyAttribute.PXCurrencyHelper.Round<InfoKeyField>(sender, row, val, CMPrecision.TRANCURY);
  }

  public static Decimal RoundCury<InfoKeyField>(PXCache sender, object row, Decimal? val) where InfoKeyField : IBqlField
  {
    return PXCurrencyAttribute.RoundCury<InfoKeyField>(sender, row, val.Value);
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    this._helper.CalcBaseValues(sender, new PXFieldVerifyingEventArgs(e.Row, obj, e.ExternalCall));
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    this._helper.CalcBaseValues(sender, e);
  }

  public static void CalcBaseValues<Field>(PXCache cache, object data) where Field : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.CalcBaseValues<Field>(cache, data);
  }

  public static void CalcBaseValues(PXCache cache, object data, string name)
  {
    PXCurrencyAttribute.PXCurrencyHelper.CalcBaseValues(cache, data, name);
  }

  public static void SetBaseCalc<Field>(PXCache cache, object data, bool isBaseCalc) where Field : IBqlField
  {
    PXCurrencyAttribute.PXCurrencyHelper.SetBaseCalc<Field>(cache, data, isBaseCalc);
  }

  public static void SetBaseCalc(PXCache cache, object data, string name, bool isBaseCalc)
  {
    PXCurrencyAttribute.PXCurrencyHelper.SetBaseCalc(cache, data, name, isBaseCalc);
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

  public class PXCurrencyHelper
  {
    private const int UseNoPrecision = -1;
    private const int UseCuryPrecision = -2;
    protected Type _KeyField;
    protected Type _ResultField;
    protected int _ResultOrdinal;
    protected Type _ClassType;
    protected bool _BaseCalc = true;
    protected bool _KeepResultValue;
    private string _FieldName;
    private int _FieldOrdinal;
    private int? _Precision;
    private PXAttributeLevel _AttributeLevel;

    public Type ResultField => this._ResultField;

    internal int Precision => this._Precision ?? 2;

    public bool BaseCalc
    {
      get => this._BaseCalc;
      set => this._BaseCalc = value;
    }

    public int FieldOrdinal
    {
      get => this._FieldOrdinal;
      set => this._FieldOrdinal = value;
    }

    public string FieldName
    {
      get => this._FieldName;
      set => this._FieldName = value;
    }

    public bool KeepResultValue
    {
      get => this._KeepResultValue;
      set => this._KeepResultValue = value;
    }

    public PXCurrencyAttribute.PXCurrencyHelper Clone(PXAttributeLevel attributeLevel)
    {
      if (attributeLevel == 2)
        return this;
      PXCurrencyAttribute.PXCurrencyHelper pxCurrencyHelper = (PXCurrencyAttribute.PXCurrencyHelper) this.MemberwiseClone();
      pxCurrencyHelper._AttributeLevel = attributeLevel;
      return pxCurrencyHelper;
    }

    public PXCurrencyHelper(
      Type keyField,
      Type resultField,
      string FieldName,
      int FieldOrdinal,
      int? Precision,
      PXAttributeLevel AttributeLevel)
    {
      if (keyField != (Type) null && !typeof (IBqlField).IsAssignableFrom(keyField))
        throw new PXArgumentException(nameof (keyField), "Invalid field: '{0}'.", new object[1]
        {
          (object) keyField
        });
      if (resultField != (Type) null && !typeof (IBqlField).IsAssignableFrom(resultField))
        throw new PXArgumentException(nameof (resultField), "Invalid field: '{0}'.", new object[1]
        {
          (object) resultField
        });
      this._KeyField = keyField;
      this._ResultField = resultField;
      this._FieldName = FieldName;
      this._FieldOrdinal = FieldOrdinal;
      this._Precision = Precision;
      this._AttributeLevel = AttributeLevel;
    }

    public virtual void CacheAttached(PXCache sender)
    {
      this._ClassType = sender.GetItemType();
      if (this._KeyField == (Type) null)
        this._KeyField = PXCurrencyAttribute.PXCurrencyHelper.SearchKeyField(sender);
      if (this._ResultField == (Type) null)
        this._ResultField = this.SearchResultField(sender);
      if (sender.HasAttribute<CurrencyInfoAttribute>())
      {
        PXGraph.RowUpdatingEvents rowUpdating = sender.Graph.RowUpdating;
        PXCurrencyAttribute.PXCurrencyHelper pxCurrencyHelper1 = this;
        // ISSUE: virtual method pointer
        PXRowUpdating pxRowUpdating = new PXRowUpdating((object) pxCurrencyHelper1, __vmethodptr(pxCurrencyHelper1, currencyInfoRowUpdating));
        rowUpdating.AddHandler<CurrencyInfo>(pxRowUpdating);
        PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
        PXCurrencyAttribute.PXCurrencyHelper pxCurrencyHelper2 = this;
        // ISSUE: virtual method pointer
        PXRowUpdated pxRowUpdated = new PXRowUpdated((object) pxCurrencyHelper2, __vmethodptr(pxCurrencyHelper2, currencyInfoRowUpdated));
        rowUpdated.AddHandler<CurrencyInfo>(pxRowUpdated);
      }
      this._ResultOrdinal = sender.GetFieldOrdinal(this._ResultField.Name);
    }

    private static Type SearchKeyField(PXCache sender)
    {
      for (int index = 0; index < sender.BqlFields.Count; ++index)
      {
        if (string.Compare(sender.BqlFields[index].Name, "CuryInfoID", true) == 0)
          return sender.BqlFields[index];
      }
      throw new PXArgumentException("_KeyField", "Invalid field: '{0}'.", new object[1]
      {
        (object) "CuryInfoID"
      });
    }

    private Type SearchResultField(PXCache sender)
    {
      string strB = "base" + this._FieldName;
      for (int index = 0; index < sender.BqlFields.Count; ++index)
      {
        if (string.Compare(sender.BqlFields[index].Name, strB, true) == 0)
          return sender.BqlFields[index];
      }
      throw new PXArgumentException("_ResultField", "Invalid field: '{0}'.", new object[1]
      {
        (object) this._ResultField
      });
    }

    private static CurrencyInfo GetCurrencyInfo(PXCache sender, object row, Type _KeyField)
    {
      IPXCurrencyHelper implementation = sender.Graph.FindImplementation<IPXCurrencyHelper>();
      if (implementation != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = implementation.GetCurrencyInfo(sender.GetValue(row, _KeyField.Name) as long?);
        if (currencyInfo != null)
          return currencyInfo.GetCM();
      }
      Type bqlTable = sender.BqlTable;
      PXView view;
      if (PXCurrencyAttribute.PXCurrencyHelper.GetView(sender.Graph, bqlTable, _KeyField, out view))
      {
        if (view.SelectSingleBound(new object[1]{ row }, Array.Empty<object>()) is CurrencyInfo info)
          PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision(view.Cache, info);
        return info;
      }
      throw new PXArgumentException(nameof (sender), "Missing CurrencyInfoAttribute for the cache: '{0}'.", new object[1]
      {
        (object) sender.GetItemType().Name
      });
    }

    private static CurrencyInfo GetCurrencyInfo(PXCache sender, object row)
    {
      Type _KeyField = PXCurrencyAttribute.PXCurrencyHelper.SearchKeyField(sender);
      return PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row, _KeyField);
    }

    private static CurrencyInfo GetCurrencyInfo<InfoKeyField>(PXCache sender, object row) where InfoKeyField : IBqlField
    {
      return PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row, typeof (InfoKeyField));
    }

    public static void CuryConvCury(
      PXCache sender,
      object row,
      Decimal baseval,
      out Decimal curyval)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row), baseval, out curyval);
    }

    public static void CuryConvCury(
      PXCache sender,
      object row,
      Decimal baseval,
      out Decimal curyval,
      int precision)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row), baseval, out curyval, precision);
    }

    public static void CuryConvCury(
      PXCache sender,
      object row,
      Decimal baseval,
      out Decimal curyval,
      bool SkipRounding)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row), baseval, out curyval, SkipRounding ? -1 : -2);
    }

    public static void CuryConvCury<CuryField>(PXCache sender, object row) where CuryField : IBqlField
    {
      foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly<CuryField>())
      {
        if (subscriberAttribute.AttributeLevel == 1 && subscriberAttribute is PXDBCurrencyAttribute)
        {
          Type keyField = ((PXDBCurrencyAttribute) subscriberAttribute)._helper._KeyField;
          int resultOrdinal = ((PXDBCurrencyAttribute) subscriberAttribute)._helper._ResultOrdinal;
          string fieldName = ((PXDBCurrencyAttribute) subscriberAttribute)._helper.FieldName;
          Decimal curyval;
          PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row, keyField), ((Decimal?) sender.GetValue(row, resultOrdinal)).GetValueOrDefault(), out curyval);
          sender.SetValueExt(row, fieldName, (object) curyval);
          break;
        }
      }
    }

    public static void CuryConvCury<InfoKeyField>(
      PXCache sender,
      object row,
      Decimal baseval,
      out Decimal curyval)
      where InfoKeyField : IBqlField
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo<InfoKeyField>(sender, row), baseval, out curyval, false);
    }

    public static void CuryConvCury<InfoKeyField>(
      PXCache sender,
      object row,
      Decimal baseval,
      out Decimal curyval,
      bool skipRounding)
      where InfoKeyField : IBqlField
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo<InfoKeyField>(sender, row), baseval, out curyval, skipRounding);
    }

    public static void CuryConvCury<InfoKeyField>(
      PXCache sender,
      object row,
      Decimal curyval,
      out Decimal baseval,
      int precision)
      where InfoKeyField : IBqlField
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo<InfoKeyField>(sender, row), curyval, out baseval, precision);
    }

    protected static void CuryConvCury(CurrencyInfo info, Decimal baseval, out Decimal curyval)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(info, baseval, out curyval, false);
    }

    protected static void CuryConvCury(
      CurrencyInfo info,
      Decimal baseval,
      out Decimal curyval,
      bool skipRounding)
    {
      if (info != null)
      {
        int num = (int) info.CuryPrecision.Value;
      }
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(info, baseval, out curyval, skipRounding ? -1 : -2);
    }

    protected static void CuryConvCury(
      CurrencyInfo info,
      Decimal baseval,
      out Decimal curyval,
      int precision)
    {
      if (info != null)
      {
        Decimal num;
        try
        {
          num = info.CuryRate.Value;
        }
        catch (InvalidOperationException ex)
        {
          throw new PXRateNotFoundException();
        }
        if (num == 0.0M)
          num = 1.0M;
        bool flag = info.CuryMultDiv == "D";
        curyval = flag ? baseval * num : baseval / num;
        if (precision == -2 && info.CuryPrecision.HasValue)
        {
          curyval = Math.Round(curyval, (int) info.CuryPrecision.Value, MidpointRounding.AwayFromZero);
        }
        else
        {
          if (!EnumerableExtensions.IsNotIn<int>(precision, -1, -2))
            return;
          curyval = Math.Round(curyval, precision, MidpointRounding.AwayFromZero);
        }
      }
      else
        curyval = baseval;
    }

    public static void CuryConvCury(
      PXCache sender,
      CurrencyInfo info,
      Decimal baseval,
      out Decimal curyval)
    {
      PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision(sender.Graph.Caches[typeof (CurrencyInfo)], info);
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(info, baseval, out curyval, false);
    }

    public static void CuryConvCury(
      PXCache sender,
      CurrencyInfo info,
      Decimal baseval,
      out Decimal curyval,
      bool skipRounding)
    {
      PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision(sender.Graph.Caches[typeof (CurrencyInfo)], info);
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(info, baseval, out curyval, skipRounding);
    }

    public static void CuryConvBase(
      PXCache sender,
      object row,
      Decimal curyval,
      out Decimal baseval)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row), curyval, out baseval);
    }

    public static void CuryConvBase(
      PXCache sender,
      object row,
      Decimal curyval,
      out Decimal baseval,
      bool skipRounding)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row), curyval, out baseval, skipRounding);
    }

    public static void CuryConvBase<InfoKeyField>(
      PXCache sender,
      object row,
      Decimal curyval,
      out Decimal baseval)
      where InfoKeyField : IBqlField
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo<InfoKeyField>(sender, row), curyval, out baseval);
    }

    public static void CuryConvBase<InfoKeyField>(
      PXCache sender,
      object row,
      Decimal curyval,
      out Decimal baseval,
      int precision)
      where InfoKeyField : IBqlField
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo<InfoKeyField>(sender, row), curyval, out baseval, precision);
    }

    protected static void CuryConvBase(CurrencyInfo info, Decimal curyval, out Decimal baseval)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(info, curyval, out baseval, false);
    }

    protected static void CuryConvBase(
      CurrencyInfo info,
      Decimal curyval,
      out Decimal baseval,
      bool skipRounding)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(info, curyval, out baseval, skipRounding ? -1 : -2);
    }

    protected static void CuryConvBase(
      CurrencyInfo info,
      Decimal curyval,
      out Decimal baseval,
      int precision)
    {
      if (info != null)
      {
        Decimal num;
        try
        {
          num = info.CuryRate.Value;
        }
        catch (InvalidOperationException ex)
        {
          throw new PXRateNotFoundException();
        }
        if (num == 0.0M)
          num = 1.0M;
        bool flag = info.CuryMultDiv != "D";
        baseval = flag ? curyval * num : curyval / num;
        if (precision == -2 && info.BasePrecision.HasValue)
        {
          baseval = Math.Round(baseval, (int) info.BasePrecision.Value, MidpointRounding.AwayFromZero);
        }
        else
        {
          if (!EnumerableExtensions.IsNotIn<int>(precision, -1, -2))
            return;
          baseval = Math.Round(baseval, precision, MidpointRounding.AwayFromZero);
        }
      }
      else
        baseval = curyval;
    }

    public static void CuryConvBase(
      PXCache sender,
      CurrencyInfo info,
      Decimal curyval,
      out Decimal baseval)
    {
      PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision(sender.Graph.Caches[typeof (CurrencyInfo)], info);
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(info, curyval, out baseval, false);
    }

    public static void CuryConvBase(
      PXCache sender,
      CurrencyInfo info,
      Decimal curyval,
      out Decimal baseval,
      bool skipRounding)
    {
      PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision(sender.Graph.Caches[typeof (CurrencyInfo)], info);
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(info, curyval, out baseval, skipRounding);
    }

    public static void CuryConvBase(
      PXCache sender,
      object row,
      Decimal baseval,
      out Decimal curyval,
      int precision)
    {
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvBase(PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row), baseval, out curyval, precision);
    }

    public static Decimal Round(PXCache sender, object row, Decimal val, CMPrecision prec)
    {
      CurrencyInfo currencyInfo = PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo(sender, row);
      if (currencyInfo != null)
      {
        if (prec == CMPrecision.TRANCURY)
          return Math.Round(val, (int) currencyInfo.CuryPrecision.Value, MidpointRounding.AwayFromZero);
        if (prec == CMPrecision.BASECURY)
          return Math.Round(val, (int) currencyInfo.BasePrecision.Value, MidpointRounding.AwayFromZero);
      }
      return val;
    }

    public static Decimal Round(Decimal val, int customPrecision)
    {
      return Math.Round(val, customPrecision, MidpointRounding.AwayFromZero);
    }

    public static Decimal Round<InfoKeyField>(
      PXCache sender,
      object row,
      Decimal val,
      CMPrecision prec)
      where InfoKeyField : IBqlField
    {
      CurrencyInfo currencyInfo = PXCurrencyAttribute.PXCurrencyHelper.GetCurrencyInfo<InfoKeyField>(sender, row);
      if (currencyInfo != null)
      {
        if (prec == CMPrecision.TRANCURY)
          return Math.Round(val, (int) currencyInfo.CuryPrecision.Value, MidpointRounding.AwayFromZero);
        if (prec == CMPrecision.BASECURY)
          return Math.Round(val, (int) currencyInfo.BasePrecision.Value, MidpointRounding.AwayFromZero);
      }
      return val;
    }

    public static void SetBaseCalc<Field>(PXCache cache, object data, bool isBaseCalc) where Field : IBqlField
    {
      if (data == null)
        cache.SetAltered<Field>(true);
      foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly<Field>())
      {
        if (subscriberAttribute is PXCurrencyAttribute)
          ((PXCurrencyAttribute) subscriberAttribute).BaseCalc = isBaseCalc;
        if (subscriberAttribute is PXDBCurrencyAttribute)
          ((PXDBCurrencyAttribute) subscriberAttribute).BaseCalc = isBaseCalc;
      }
    }

    public static void SetBaseCalc(PXCache cache, object data, string name, bool isBaseCalc)
    {
      if (data == null)
        cache.SetAltered(name, true);
      foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(name))
      {
        if (subscriberAttribute is PXCurrencyAttribute)
          ((PXCurrencyAttribute) subscriberAttribute).BaseCalc = isBaseCalc;
        if (subscriberAttribute is PXDBCurrencyAttribute)
          ((PXDBCurrencyAttribute) subscriberAttribute).BaseCalc = isBaseCalc;
      }
    }

    public static void CalcBaseValues<Field>(PXCache cache, object data) where Field : IBqlField
    {
      if (data == null)
        cache.SetAltered<Field>(true);
      foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly<Field>())
      {
        if (subscriberAttribute is PXCurrencyAttribute)
        {
          object obj = cache.GetValue(data, subscriberAttribute.FieldOrdinal);
          ((PXCurrencyAttribute) subscriberAttribute)._helper.CalcBaseValues(cache, new PXFieldVerifyingEventArgs(data, obj, false));
        }
        if (subscriberAttribute is PXDBCurrencyAttribute)
        {
          object obj = cache.GetValue(data, subscriberAttribute.FieldOrdinal);
          ((PXDBCurrencyAttribute) subscriberAttribute)._helper.CalcBaseValues(cache, new PXFieldVerifyingEventArgs(data, obj, false));
        }
      }
    }

    public static void CalcBaseValues(PXCache cache, object data, string name)
    {
      if (data == null)
        cache.SetAltered(name, true);
      foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(name))
      {
        if (subscriberAttribute is PXCurrencyAttribute)
        {
          object obj = cache.GetValue(data, subscriberAttribute.FieldOrdinal);
          ((PXCurrencyAttribute) subscriberAttribute)._helper.CalcBaseValues(cache, new PXFieldVerifyingEventArgs(data, obj, false));
        }
        if (subscriberAttribute is PXDBCurrencyAttribute)
        {
          object obj = cache.GetValue(data, subscriberAttribute.FieldOrdinal);
          ((PXDBCurrencyAttribute) subscriberAttribute)._helper.CalcBaseValues(cache, new PXFieldVerifyingEventArgs(data, obj, false));
        }
      }
    }

    internal void CalcBaseValues(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (!this._BaseCalc)
        return;
      CurrencyInfo currencyInfo = (CurrencyInfo) null;
      bool? baseCalc;
      if (e.NewValue != null && (currencyInfo = this.getInfoInt(sender, e.Row)) != null)
      {
        Decimal? curyRate = currencyInfo.CuryRate;
        if (curyRate.HasValue)
        {
          baseCalc = currencyInfo.BaseCalc;
          if (baseCalc.GetValueOrDefault())
          {
            curyRate = currencyInfo.CuryRate;
            Decimal num1 = curyRate.Value;
            if (num1 == 0.0M)
              num1 = 1.0M;
            int num2 = currencyInfo.CuryMultDiv != "D" ? 1 : 0;
            Decimal newValue = (Decimal) e.NewValue;
            object obj = (object) (num2 != 0 ? newValue * num1 : newValue / num1);
            sender.RaiseFieldUpdating(this._ResultField.Name, e.Row, ref obj);
            sender.SetValue(e.Row, this._ResultOrdinal, obj);
            return;
          }
        }
      }
      if (currencyInfo != null)
      {
        baseCalc = currencyInfo.BaseCalc;
        if (!baseCalc.GetValueOrDefault())
          return;
      }
      object newValue1 = e.NewValue;
      sender.RaiseFieldUpdating(this._ResultField.Name, e.Row, ref newValue1);
      sender.SetValue(e.Row, this._ResultOrdinal, newValue1);
    }

    public virtual void currencyInfoRowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
    {
      if (!(e.Row is CurrencyInfo row))
        return;
      long? curyInfoId1 = row.CuryInfoID;
      long num = 0;
      if (!(curyInfoId1.GetValueOrDefault() > num & curyInfoId1.HasValue))
        return;
      long? curyInfoId2 = row.CuryInfoID;
      long? curyInfoId3 = CurrencyCollection.GetCurrency(row?.BaseCuryID).CuryInfoID;
      if (!(curyInfoId2.GetValueOrDefault() == curyInfoId3.GetValueOrDefault() & curyInfoId2.HasValue == curyInfoId3.HasValue))
        return;
      ((CancelEventArgs) e).Cancel = true;
    }

    public virtual void currencyInfoRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      if (!this._BaseCalc && !this.KeepResultValue)
        return;
      CurrencyInfo row = e.Row as CurrencyInfo;
      CurrencyInfo oldRow = e.OldRow as CurrencyInfo;
      if (row == null)
        return;
      if (oldRow != null)
      {
        Decimal? curyRate1 = row.CuryRate;
        Decimal? curyRate2 = oldRow.CuryRate;
        if (curyRate1.GetValueOrDefault() == curyRate2.GetValueOrDefault() & curyRate1.HasValue == curyRate2.HasValue && !(row.CuryMultDiv != oldRow.CuryMultDiv))
          return;
      }
      PXView view = CurrencyInfoAttribute.GetView(sender.Graph, this._ClassType, this._KeyField);
      if (view == null || !row.CuryRate.HasValue)
        return;
      Decimal rate = row.CuryRate.Value;
      if (rate == 0.0M)
        rate = 1.0M;
      bool mult = row.CuryMultDiv != "D";
      PXCache cache = view.Cache;
      foreach (object obj1 in view.SelectMultiBound(new object[1]
      {
        e.Row
      }, Array.Empty<object>()))
      {
        object obj2 = obj1 is PXResult ? ((PXResult) obj1)[0] : obj1;
        if (this._KeepResultValue)
          this.RecalculateFieldValue(cache, obj2, rate, mult);
        else
          this.RecalculateResultFieldValue(cache, obj2, rate, mult);
      }
    }

    protected virtual void RecalculateResultFieldValue(
      PXCache cache,
      object item,
      Decimal rate,
      bool mult)
    {
      if (cache.GetValue(item, this._FieldOrdinal) == null)
        return;
      Decimal num = (Decimal) cache.GetValue(item, this._FieldOrdinal);
      object obj = (object) (mult ? num * rate : num / rate);
      cache.RaiseFieldUpdating(this._ResultField.Name, item, ref obj);
      cache.SetValue(item, this._ResultOrdinal, obj);
      GraphHelper.MarkUpdated(cache, item);
    }

    protected virtual void RecalculateFieldValue(
      PXCache cache,
      object item,
      Decimal rate,
      bool mult)
    {
      object obj1 = (object) null;
      Decimal? nullable = (Decimal?) cache.GetValue(item, this._ResultOrdinal);
      if (nullable.HasValue)
        obj1 = (object) (mult ? nullable.Value / rate : nullable.Value * rate);
      cache.RaiseFieldUpdating(this.FieldName, item, ref obj1);
      object obj2 = cache.GetValue(item, this._FieldOrdinal);
      if (obj2 == obj1)
        return;
      object copy = cache.CreateCopy(item);
      cache.SetValue(item, this._FieldOrdinal, obj1);
      cache.RaiseFieldUpdated(this.FieldName, item, obj2);
      cache.RaiseRowUpdated(item, copy);
    }

    private static short GetBasePrecision(PXGraph graph)
    {
      CurrencyInfo currencyInfo = new CurrencyInfo();
      PXCache cach = graph.Caches[typeof (CurrencyInfo)];
      object obj;
      cach.RaiseFieldDefaulting<CurrencyInfo.baseCuryID>((object) currencyInfo, ref obj);
      currencyInfo.BaseCuryID = (string) obj;
      currencyInfo.CuryPrecision = new short?((short) 4);
      object basePrecision;
      cach.RaiseFieldDefaulting<CurrencyInfo.basePrecision>((object) currencyInfo, ref basePrecision);
      return (short) basePrecision;
    }

    public static Decimal BaseRound(PXGraph graph, Decimal value)
    {
      short basePrecision = PXCurrencyAttribute.PXCurrencyHelper.GetBasePrecision(graph);
      return Math.Round(value, (int) basePrecision, MidpointRounding.AwayFromZero);
    }

    public static void PopulatePrecision(PXCache cache, CurrencyInfo info)
    {
      if (info == null)
        return;
      if (!info.CuryPrecision.HasValue)
      {
        object obj;
        cache.RaiseFieldDefaulting<CurrencyInfo.curyPrecision>((object) info, ref obj);
        info.CuryPrecision = (short?) obj;
        GraphHelper.Hold(cache, (object) info);
      }
      if (info.BasePrecision.HasValue)
        return;
      object obj1;
      cache.RaiseFieldDefaulting<CurrencyInfo.basePrecision>((object) info, ref obj1);
      info.BasePrecision = (short?) obj1;
      GraphHelper.Hold(cache, (object) info);
    }

    public int GetCurrencyInfoPrecision(PXCache sender, object row)
    {
      return (int) this.getInfo(sender, row)?.CuryPrecision ?? this.Precision;
    }

    public virtual void FieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      bool fixedPrecision)
    {
      bool curyViewState = sender.Graph.Accessinfo.CuryViewState;
      int? nullable = new int?();
      if (!fixedPrecision)
      {
        CurrencyInfo info = this.getInfo(sender, e.Row);
        if (info != null)
        {
          PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision((PXCache) GraphHelper.Caches<CurrencyInfo>(sender.Graph), info);
          this._Precision = new int?(!curyViewState ? (int) (info.CuryPrecision ?? (short) 2) : (int) (info.BasePrecision ?? (short) 2));
          nullable = this._Precision;
        }
      }
      if (curyViewState)
      {
        object obj = sender.GetValue(e.Row, this._FieldOrdinal);
        this.CalcBaseValues(sender, new PXFieldVerifyingEventArgs(e.Row, obj, false));
        e.ReturnValue = sender.GetValue(e.Row, this._ResultOrdinal);
      }
      if (this._AttributeLevel != 2 && !e.IsAltered)
        return;
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), nullable, new int?(), (object) null, this._FieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, curyViewState ? new bool?(false) : new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    }

    private CurrencyInfo LocateInfo(PXCache cache, CurrencyInfo info)
    {
      foreach (CurrencyInfo currencyInfo in cache.Inserted)
      {
        if (object.Equals((object) currencyInfo.CuryInfoID, (object) info.CuryInfoID))
          return currencyInfo;
      }
      return cache.Locate((object) info) as CurrencyInfo;
    }

    private static bool GetView(PXGraph graph, Type classType, Type keyField, out PXView view)
    {
      string str = keyField.Name.With<string, string>((Func<string, string>) (_ => char.ToUpper(_[0]).ToString() + _.Substring(1)));
      string key1 = $"_{classType.Name}_CurrencyInfo_{(str == "CuryInfoID" ? "" : str + "_")}";
      if (!((Dictionary<string, PXView>) graph.Views).TryGetValue(key1, out view))
      {
        string key2 = $"_CurrencyInfo_{classType.FullName}.{str}_";
        if (!((Dictionary<string, PXView>) graph.Views).TryGetValue(key2, out view))
        {
          BqlCommand instance = BqlCommand.CreateInstance(new Type[7]
          {
            typeof (Select<,>),
            typeof (CurrencyInfo),
            typeof (Where<,>),
            typeof (CurrencyInfo.curyInfoID),
            typeof (Equal<>),
            typeof (Optional<>),
            keyField
          });
          graph.Views[key2] = view = new PXView(graph, false, instance);
        }
      }
      return true;
    }

    private CurrencyInfo getInfo(PXCache sender, object row)
    {
      if (!(sender.Graph.GetType() == typeof (PXGenericInqGrph)) && !(sender.Graph.GetType() == typeof (LayoutMaint)))
      {
        if (!(sender.Graph.GetType() == typeof (PXGraph)))
        {
          try
          {
            return this.getInfoInt(sender, row);
          }
          catch (InvalidCastException ex)
          {
            throw new Exception($"Looks like {string.Join(" ", ((IEnumerable<Type>) sender.GetType().GenericTypeArguments).Select<Type, string>((Func<Type, string>) (_ => _.FullName)))} stil uses obsolete attributtes from 'PX.Objects.CM' while some code expects it to have 'PX.Objects.CM.Extensions'", (Exception) ex);
          }
        }
      }
      CurrencyInfo info = new CurrencyInfo();
      info.CuryInfoID = (long?) sender.GetValue(row, this._KeyField.Name);
      PXCache pxCache = (PXCache) GraphHelper.Caches<CurrencyInfo>(sender.Graph);
      object obj1;
      if ((obj1 = pxCache.Locate((object) info)) == null)
      {
        info.CuryID = CurrencyInfoAttribute.GetCuryID(sender, row, this._KeyField.Name);
        object obj2;
        if (pxCache.RaiseFieldDefaulting<CurrencyInfo.baseCuryID>((object) info, ref obj2))
          pxCache.RaiseFieldUpdating<CurrencyInfo.baseCuryID>((object) info, ref obj2);
        pxCache.SetValue<CurrencyInfo.baseCuryID>((object) info, obj2);
      }
      else
        info = obj1 as CurrencyInfo;
      return info;
    }

    private CurrencyInfo getInfoInt(PXCache sender, object row)
    {
      PXView view;
      if (!PXCurrencyAttribute.PXCurrencyHelper.GetView(sender.Graph, this._ClassType, this._KeyField, out view))
        return (CurrencyInfo) null;
      if (view.Cache.Current is CurrencyInfo infoInt)
      {
        long? objB = (long?) sender.GetValue(row, this._KeyField.Name);
        if (row == null || !object.Equals((object) infoInt.CuryInfoID, (object) objB))
        {
          infoInt = this.LocateInfo(view.Cache, new CurrencyInfo()
          {
            CuryInfoID = objB
          });
          if (infoInt == null)
          {
            if (!objB.HasValue)
            {
              object obj;
              if (sender.RaiseFieldDefaulting(this._KeyField.Name, (object) null, ref obj))
                sender.RaiseFieldUpdating(this._KeyField.Name, (object) null, ref obj);
              if (obj != null)
                infoInt = view.Cache.Locate((object) new CurrencyInfo()
                {
                  CuryInfoID = new long?(Convert.ToInt64(obj))
                }) as CurrencyInfo;
            }
            if (!objB.HasValue && infoInt == null)
            {
              object obj = sender.GetValue(sender.Current, this._KeyField.Name);
              if (obj != null)
                infoInt = this.LocateInfo(view.Cache, new CurrencyInfo()
                {
                  CuryInfoID = new long?(Convert.ToInt64(obj))
                });
            }
            if (infoInt == null)
            {
              using (new CurrencyInfoAttribute.SkipReadOnly(view))
              {
                CurrencyInfo currencyInfo = CurrencyInfoAttribute.GetCurrencyInfo(sender, this._KeyField, row);
                if (currencyInfo == null)
                  currencyInfo = view.SelectSingleBound(new object[1]
                  {
                    row
                  }, Array.Empty<object>()) as CurrencyInfo;
                infoInt = currencyInfo;
              }
            }
          }
        }
      }
      else
      {
        infoInt = this.LocateInfo(view.Cache, new CurrencyInfo()
        {
          CuryInfoID = (long?) sender.GetValue(row, this._KeyField.Name)
        });
        if (infoInt == null)
        {
          using (new CurrencyInfoAttribute.SkipReadOnly(view))
          {
            CurrencyInfo currencyInfo = CurrencyInfoAttribute.GetCurrencyInfo(sender, this._KeyField, row);
            if (currencyInfo == null)
              currencyInfo = view.SelectSingleBound(new object[1]
              {
                row
              }, Array.Empty<object>()) as CurrencyInfo;
            infoInt = currencyInfo;
          }
        }
      }
      return infoInt;
    }

    public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (!PXCurrencyAttribute.PXCurrencyHelper.FormatValue(e, sender.Graph.Culture))
        return;
      CurrencyInfo info = this.getInfo(sender, e.Row);
      if (info == null)
        return;
      PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision((PXCache) GraphHelper.Caches<CurrencyInfo>(sender.Graph), info);
      Decimal d = Convert.ToDecimal(e.NewValue);
      e.NewValue = (object) Math.Round(d, (int) (info.CuryPrecision ?? (short) 2), MidpointRounding.AwayFromZero);
    }

    public virtual void ResultFieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      bool fixedPrecision)
    {
      int num = sender.Graph.Accessinfo.CuryViewState ? 1 : 0;
      int? nullable = new int?();
      if (!fixedPrecision)
      {
        CurrencyInfo info = this.getInfo(sender, e.Row);
        if (info != null)
        {
          PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision((PXCache) GraphHelper.Caches<CurrencyInfo>(sender.Graph), info);
          nullable = new int?((int) (info.BasePrecision ?? (short) 2));
        }
      }
      if (this._AttributeLevel != 2 && !e.IsAltered)
        return;
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (Type) null, new bool?(), new bool?(), new int?(), nullable, new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    }

    public virtual void ResultFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (!PXCurrencyAttribute.PXCurrencyHelper.FormatValue(e, sender.Graph.Culture))
        return;
      CurrencyInfo info = this.getInfo(sender, e.Row);
      if (info == null)
        return;
      PXCurrencyAttribute.PXCurrencyHelper.PopulatePrecision((PXCache) GraphHelper.Caches<CurrencyInfo>(sender.Graph), info);
      e.NewValue = (object) Math.Round((Decimal) e.NewValue, (int) (info.BasePrecision ?? (short) 2), MidpointRounding.AwayFromZero);
    }

    private static bool FormatValue(PXFieldUpdatingEventArgs e, CultureInfo culture)
    {
      if (e.NewValue is string)
      {
        Decimal result;
        e.NewValue = !Decimal.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) culture, out result) ? (object) null : (object) result;
      }
      return e.NewValue != null;
    }
  }
}
