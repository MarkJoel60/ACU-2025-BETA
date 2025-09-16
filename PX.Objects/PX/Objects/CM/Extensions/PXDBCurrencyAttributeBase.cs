// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXDBCurrencyAttributeBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public abstract class PXDBCurrencyAttributeBase : PXDBDecimalAttribute, ICurrencyAttribute
{
  public Type ResultField { get; }

  public Type KeyField { get; }

  public virtual bool BaseCalc { get; set; } = true;

  public virtual bool ShouldShowBaseIfCuryViewState { get; set; } = true;

  public virtual int? CustomPrecision => new int?();

  public PXDBCurrencyAttributeBase(Type keyField, Type resultField)
  {
    this.ResultField = resultField;
    this.KeyField = keyField;
  }

  public PXDBCurrencyAttributeBase(Type precision, Type keyField, Type resultField)
    : base(precision)
  {
    this.ResultField = resultField;
    this.KeyField = keyField;
  }

  public PXDBCurrencyAttributeBase(int precision, Type keyField, Type resultField)
    : base(precision)
  {
    this.ResultField = resultField;
    this.KeyField = keyField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PXDBCurrencyAttributeBase.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new PXDBCurrencyAttributeBase.\u003C\u003Ec__DisplayClass19_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    base.CacheAttached(cDisplayClass190.sender);
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass190.sender.Graph.IsInitializing)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass190.sender.Graph.Initialized += new PXGraphInitializedDelegate((object) cDisplayClass190, __methodptr(\u003CCacheAttached\u003Eg__subscribeToEvents\u007C0));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      cDisplayClass190.\u003CCacheAttached\u003Eg__subscribeToEvents\u007C0(cDisplayClass190.sender.Graph);
    }
  }

  protected virtual void CuryFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    CuryField curyField)
  {
    if (!sender.Graph.Accessinfo.CuryViewState || !this.ShouldShowBaseIfCuryViewState || string.IsNullOrEmpty(curyField.BaseName))
      return;
    curyField.SetBaseCuryValueAsReturnState(sender, e);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CuryField curyField = new CuryField((ICurrencyAttribute) this);
    Decimal? objA = (Decimal?) sender.GetValue(e.Row, curyField.CuryName);
    if (objA.HasValue && ((e.Operation & 3) == 1 && !object.Equals((object) objA, sender.GetValueOriginal(e.Row, ((PXEventSubscriberAttribute) this)._FieldName)) || (e.Operation & 3) == 2))
    {
      int? customPrecision = curyField.CustomPrecision;
      if (!customPrecision.HasValue)
      {
        CurrencyInfo currencyInfo = sender.Graph.FindImplementation<ICurrencyHost>()?.GetCurrencyInfo(sender, e.Row, curyField.CuryInfoIDName);
        sender.SetValue(e.Row, curyField.CuryName, (object) (currencyInfo != null ? currencyInfo.RoundCury(objA.Value) : objA.Value));
      }
      else
      {
        PXCache pxCache = sender;
        object row = e.Row;
        string curyName = curyField.CuryName;
        Decimal d = objA.Value;
        customPrecision = curyField.CustomPrecision;
        int decimals = customPrecision.Value;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal> local = (ValueType) Math.Round(d, decimals, MidpointRounding.AwayFromZero);
        pxCache.SetValue(row, curyName, (object) local);
      }
    }
    base.RowPersisting(sender, e);
  }

  protected virtual void CuryRowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e,
    CuryField curyField)
  {
    CurrencyInfo currencyInfo = sender.Graph.FindImplementation<ICurrencyHost>().GetCurrencyInfo(sender, e.Row, curyField.CuryInfoIDName);
    Decimal? curyValue = (Decimal?) sender.GetValue(e.Row, curyField.CuryName);
    curyField.RecalculateFieldBaseValue(sender, e.Row, (object) curyValue, currencyInfo);
  }
}
