// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXDBBaseCuryMaxPrecisionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CM;

/// <summary>
/// Extends <see cref="T:PX.Data.PXDBDecimalAttribute" /> by defaulting the precision property.
/// If BranchID or CuryID is supplied than Precision is taken from the currency, corresponded to the Branch.BaseCuryID or Currency.CuryID.
/// Otherwise system searchef for the max Precision for base currencies, set for Companies.
/// </summary>
public class PXDBBaseCuryMaxPrecisionAttribute : PXDBDecimalAttribute
{
  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    short? nullable = CurrencyCollection.GetBaseCurrencies().Select<string, Currency>((Func<string, Currency>) (_ => CurrencyCollection.GetCurrency(_))).Max<Currency, short?>((Func<Currency, short?>) (_ => _.DecimalPlaces));
    this._Precision = nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?();
    if (this._Precision.HasValue)
      return;
    this._Precision = new int?(2);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    base.CacheAttached(sender);
    if (this._Precision.HasValue || !PXGraph.ProxyIsActive)
      return;
    this._Precision = new int?(2);
  }
}
