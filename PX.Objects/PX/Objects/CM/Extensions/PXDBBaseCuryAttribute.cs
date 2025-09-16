// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXDBBaseCuryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Extends <see cref="T:PX.Data.PXDBDecimalAttribute" /> by defaulting the precision property.
/// Precision is taken from Base Currency that is configured on the Company level.
/// </summary>
public class PXDBBaseCuryAttribute : PXDBDecimalAttribute
{
  private string _BranchID = "BranchID";
  protected Type branchID;

  public virtual bool TrySetPrecisionWithBranch { get; set; } = true;

  public PXDBBaseCuryAttribute()
  {
  }

  public PXDBBaseCuryAttribute(Type branchID) => this.branchID = branchID;

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    short? decimalPlaces;
    if (this.branchID != (Type) null)
    {
      decimalPlaces = (short?) CurrencyCollection.GetCurrency(PXAccess.GetBranch((int?) this.GetSourceID(sender, row, this.branchID))?.BaseCuryID)?.DecimalPlaces;
      this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
    }
    if (this.branchID == (Type) null && this.TrySetPrecisionWithBranch)
    {
      int? nullable = sender.GetValue(row, this._BranchID) as int?;
      if (nullable.HasValue)
      {
        decimalPlaces = (short?) CurrencyCollection.GetCurrency(PXAccess.GetBranch(nullable)?.BaseCuryID)?.DecimalPlaces;
        this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
      }
      if (!nullable.HasValue || !this._Precision.HasValue)
      {
        decimalPlaces = (short?) CurrencyCollection.GetCurrency(sender.Graph.Accessinfo.BaseCuryID)?.DecimalPlaces;
        this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
      }
    }
    if (this._Precision.HasValue && (!(this.branchID == (Type) null) || this.TrySetPrecisionWithBranch))
      return;
    this._Precision = new int?(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(sender.Graph).BaseDecimalPlaces());
  }

  public virtual void CacheAttached(PXCache sender)
  {
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    base.CacheAttached(sender);
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
}
