// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.CuryField
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM.Extensions;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency;

public class CuryField
{
  private readonly ICurrencyAttribute PXCurrencyAttr;
  private bool _ForceBaseCalc;

  public string CuryName => this.PXCurrencyAttr.FieldName;

  public string BaseName => this.PXCurrencyAttr.ResultField?.Name;

  public bool BaseCalc
  {
    get => this._ForceBaseCalc || this.PXCurrencyAttr.BaseCalc;
    set => this._ForceBaseCalc = value;
  }

  public string CuryInfoIDName => this.PXCurrencyAttr.KeyField?.Name;

  public int? CustomPrecision => this.PXCurrencyAttr.CustomPrecision;

  public CuryField(ICurrencyAttribute PXCurrencyAttr) => this.PXCurrencyAttr = PXCurrencyAttr;

  public override string ToString()
  {
    return string.Join(" : ", this.CuryName, this.BaseName, this.CuryInfoIDName);
  }

  public virtual void RecalculateFieldBaseValue(
    PXCache sender,
    object row,
    object curyValue,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo,
    bool? baseCalc = null)
  {
    if (string.IsNullOrEmpty(this.BaseName))
      return;
    bool? nullable = baseCalc;
    if (((int) nullable ?? (this.BaseCalc ? 1 : 0)) == 0)
      return;
    if (curyValue == null)
    {
      sender.SetValue(row, this.BaseName, curyValue);
    }
    else
    {
      if (curyInfo != null && curyInfo.CuryRate.HasValue)
      {
        nullable = curyInfo.BaseCalc;
        if (nullable.GetValueOrDefault())
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = curyInfo;
          Decimal curyval = (Decimal) curyValue;
          int? customPrecision = this.CustomPrecision;
          int? precision;
          if (!customPrecision.HasValue)
          {
            short? basePrecision = curyInfo.BasePrecision;
            precision = basePrecision.HasValue ? new int?((int) basePrecision.GetValueOrDefault()) : new int?();
          }
          else
            precision = customPrecision;
          curyValue = (object) currencyInfo.CuryConvBase(curyval, precision);
          sender.RaiseFieldUpdating(this.BaseName, row, ref curyValue);
          sender.SetValue(row, this.BaseName, curyValue);
          return;
        }
      }
      if (curyInfo != null)
      {
        nullable = curyInfo.BaseCalc;
        if (!nullable.GetValueOrDefault())
          return;
      }
      sender.RaiseFieldUpdating(this.BaseName, row, ref curyValue);
      sender.SetValue(row, this.BaseName, curyValue);
    }
  }

  public static void SubscribeSimpleCopying(PXCache cache)
  {
    foreach (CuryField curyField in cache.GetAttributesReadonly((string) null).OfType<ICurrencyAttribute>().Select<ICurrencyAttribute, CuryField>((Func<ICurrencyAttribute, CuryField>) (_ => new CuryField(_))))
    {
      cache.Graph.FieldDefaulting.AddHandler(cache.GetItemType(), curyField.BaseName, (PXFieldDefaulting) ((s, e) => e.NewValue = (object) 0M));
      cache.Graph.FieldVerifying.AddHandler(cache.GetItemType(), curyField.CuryName, new PXFieldVerifying(curyField.SimpleCopying));
    }
  }

  private void SimpleCopying(PXCache pXCache, PXFieldVerifyingEventArgs e)
  {
    pXCache.SetValue(e.Row, this.BaseName, e.NewValue);
  }

  internal void SetBaseCuryValueAsReturnState(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnValue = sender.GetValue(e.Row, this.BaseName);
    ICurrencyHost implementation = sender.Graph.FindImplementation<ICurrencyHost>();
    if (PX.Objects.CM.PXCurrencyAttribute.IsNullOrEmpty(e.ReturnValue as Decimal?) && implementation != null)
    {
      object curyValue = sender.GetValue(e.Row, this.CuryName);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = implementation.GetCurrencyInfo(sender, e.Row, this.CuryInfoIDName);
      this.RecalculateFieldBaseValue(sender, e.Row, curyValue, currencyInfo, new bool?(true));
      e.ReturnValue = sender.GetValue(e.Row, this.BaseName);
    }
    if (!e.IsAltered)
      return;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    bool? nullable1 = new bool?(false);
    bool? isKey = new bool?();
    bool? nullable2 = new bool?();
    int? required = new int?();
    int? precision = new int?();
    int? length = new int?();
    bool? enabled = nullable1;
    bool? visible = new bool?();
    bool? readOnly = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnState, (System.Type) null, isKey, nullable2, required, precision, length, enabled: enabled, visible: visible, readOnly: readOnly);
    selectingEventArgs.ReturnState = (object) instance;
  }
}
