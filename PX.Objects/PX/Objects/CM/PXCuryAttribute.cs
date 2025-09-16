// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXCuryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM;

/// <summary>
/// Extends <see cref="T:PX.Data.PXDecimalAttribute" /> by defaulting the precision property.
/// Precision is taken from given Currency.
/// </summary>
/// <remarks>This is a NON-DB attribute. Use it for calculated fields that are not storred in database.</remarks>
public class PXCuryAttribute : PXDecimalAttribute
{
  protected readonly Type sourceCuryID;

  public PXCuryAttribute(Type CuryIDType)
    : base(CurrencyCollection.CombiteSearchPrecision(CuryIDType))
  {
    this.sourceCuryID = CuryIDType;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    base.CacheAttached(sender);
  }

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    short? decimalPlaces = (short?) CurrencyCollection.GetCurrency(sender, this.sourceCuryID, row)?.DecimalPlaces;
    this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
    if (!this._Precision.HasValue)
      base._ensurePrecision(sender, row);
    if (this._Precision.HasValue)
      return;
    this._Precision = new int?(2);
  }
}
