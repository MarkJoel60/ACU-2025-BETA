// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXDBBaseCuryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM;

/// <summary>
/// Extends <see cref="T:PX.Data.PXDBDecimalAttribute" /> by defaulting the precision property.
/// If LedgerID is supplied than Precision is taken from the Ledger's base currency.
/// If BranchID or CuryID is supplied than Precision is taken from the currency, corresponded to the Branch.BaseCuryID or Currency.CuryID.
/// Otherwise Precision is taken from Currency that is configured for the currently selected Branch, that user works with (AccessInfo.BaseCuryID).
/// </summary>
public class PXDBBaseCuryAttribute : PXDBDecimalAttribute
{
  private readonly Type curyID;
  private readonly Type branchID;

  public PXDBBaseCuryAttribute(Type LedgerIDType)
    : base(BqlCommand.Compose(new Type[8]
    {
      typeof (Search2<,,>),
      typeof (Currency.decimalPlaces),
      typeof (InnerJoin<PX.Objects.GL.Ledger, On<PX.Objects.GL.Ledger.baseCuryID, Equal<Currency.curyID>>>),
      typeof (Where<,>),
      typeof (PX.Objects.GL.Ledger.ledgerID),
      typeof (Equal<>),
      typeof (Current<>),
      LedgerIDType
    }))
  {
  }

  public PXDBBaseCuryAttribute(Type branchID = null, Type curyID = null)
  {
    this.branchID = branchID;
    this.curyID = curyID;
  }

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    if (this._Type != (Type) null)
    {
      short? decimalPlaces = (short?) CurrencyCollection.GetCurrency(sender, this._Type, row)?.DecimalPlaces;
      this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
      if (!this._Precision.HasValue)
        base._ensurePrecision(sender, row);
    }
    else if (this.branchID != (Type) null)
    {
      short? decimalPlaces = (short?) CurrencyCollection.GetCurrency(PXAccess.GetBranch((int?) this.GetSourceID(sender, row, this.branchID))?.BaseCuryID)?.DecimalPlaces;
      this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
    }
    else if (this.curyID != (Type) null)
    {
      short? decimalPlaces = (short?) CurrencyCollection.GetCurrency((string) this.GetSourceID(sender, row, this.curyID))?.DecimalPlaces;
      this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
    }
    else
    {
      short? decimalPlaces = (short?) CurrencyCollection.GetCurrency(sender.Graph.Accessinfo.BaseCuryID)?.DecimalPlaces;
      this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
    }
    if (this._Precision.HasValue)
      return;
    this._Precision = new int?(2);
  }

  private object GetSourceID(PXCache sender, object row, Type field)
  {
    if (field == (Type) null)
      return (object) null;
    if (field.DeclaringType == sender.GetItemType())
      return sender.GetValue(row, field.Name);
    PXCache cach = sender.Graph.Caches[field.DeclaringType];
    return cach.GetValue(cach.Current, field.Name);
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
