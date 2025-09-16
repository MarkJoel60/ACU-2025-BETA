// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXCurrencyPriceCostAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXCurrencyPriceCostAttribute : PXDecimalAttribute, ICurrencyAttribute
{
  protected internal Type ResultField;
  protected internal Type KeyField;

  public virtual bool BaseCalc { get; set; } = true;

  int? ICurrencyAttribute.CustomPrecision => this._Precision;

  Type ICurrencyAttribute.ResultField => this.ResultField;

  Type ICurrencyAttribute.KeyField => this.KeyField;

  /// <summary>Constructor</summary>
  /// <param name="keyField">Field in this table used as a key for CurrencyInfo table.</param>
  /// <param name="resultField">Field in this table to store the result of currency conversion.</param>
  public PXCurrencyPriceCostAttribute(Type keyField, Type resultField)
    : base(typeof (Search<CommonSetup.decPlPrcCst>))
  {
    this.ResultField = resultField;
    this.KeyField = keyField;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    base._ensurePrecision(sender, (object) null);
  }

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    this._Precision = new int?(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(sender.Graph).PriceCostDecimalPlaces());
  }
}
