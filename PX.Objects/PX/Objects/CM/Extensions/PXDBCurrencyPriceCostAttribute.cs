// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXDBCurrencyPriceCostAttribute
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

/// <summary>
/// Marks the field for processing by MultiCurrencyGraph. When attached to a Field that stores Amount in pair with BaseAmount Field
/// MultiCurrencyGraph handles conversion and rounding when this field is updated.
/// This Attribute forces the system to use precision specified for Price/Cost instead one comming from Currency
/// Use this Attribute for DB fields. See <see cref="T:PX.Objects.CM.Extensions.PXCurrencyAttribute" /> for Non-DB version.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
/// <summary>Constructor</summary>
/// <param name="keyField">Field in this table used as a key for CurrencyInfo table.</param>
/// <param name="resultField">Field in this table to store the result of currency conversion.</param>
public class PXDBCurrencyPriceCostAttribute(Type keyField, Type resultField) : 
  PXDBCurrencyAttributeBase(typeof (Search<CommonSetup.decPlPrcCst>), keyField, resultField)
{
  public override int? CustomPrecision => this._Precision;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    base._ensurePrecision(sender, (object) null);
  }

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    this._Precision = new int?(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(sender.Graph).PriceCostDecimalPlaces());
  }
}
