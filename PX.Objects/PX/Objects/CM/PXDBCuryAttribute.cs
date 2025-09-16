// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXDBCuryAttribute
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
/// Precision is taken from given Currency.
/// </summary>
public class PXDBCuryAttribute : PXDBDecimalAttribute
{
  protected readonly Type sourceCuryID;
  protected readonly Type branchID;

  public PXDBCuryAttribute(Type CuryIDType)
    : base(CurrencyCollection.CombiteSearchPrecision(CuryIDType))
  {
    this.sourceCuryID = CuryIDType;
  }

  public PXDBCuryAttribute(Type CuryIDType, Type branchID)
    : base(CurrencyCollection.CombiteSearchPrecision(CuryIDType))
  {
    this.sourceCuryID = CuryIDType;
    this.branchID = branchID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    base.CacheAttached(sender);
  }

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    if (row == null)
    {
      base._ensurePrecision(sender, row);
    }
    else
    {
      if (this.sourceCuryID != (Type) null)
      {
        short? decimalPlaces = (short?) CurrencyCollection.GetCurrency(sender, this.sourceCuryID, row)?.DecimalPlaces;
        this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
      }
      else
      {
        short? decimalPlaces;
        if (this.branchID != (Type) null)
        {
          decimalPlaces = (short?) CurrencyCollection.GetCurrency(PXAccess.GetBranch((int?) this.GetSourceID(sender, row, this.branchID))?.BaseCuryID)?.DecimalPlaces;
          this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
        }
        if (this.branchID == (Type) null || !this._Precision.HasValue)
        {
          decimalPlaces = (short?) CurrencyCollection.GetCurrency(sender.Graph.Accessinfo.BaseCuryID)?.DecimalPlaces;
          this._Precision = decimalPlaces.HasValue ? new int?((int) decimalPlaces.GetValueOrDefault()) : new int?();
        }
      }
      if (this._Precision.HasValue)
        return;
      this._Precision = new int?(2);
    }
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
