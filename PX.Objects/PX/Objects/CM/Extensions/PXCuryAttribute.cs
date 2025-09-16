// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXCuryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Extends <see cref="T:PX.Data.PXDecimalAttribute" /> by defaulting the precision property.
/// Precision is taken from given Currency.
/// </summary>
/// <remarks>This is a NON-DB attribute. Use it for calculated fields that are not storred in database.</remarks>
public class PXCuryAttribute : PXDecimalAttribute
{
  protected readonly Type sourceCuryID;
  protected Dictionary<long, string> _matches;

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
    this._Precision = new int?(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(sender.Graph).CuryDecimalPlaces(this.GetCuryID(sender, row)));
  }

  private string GetCuryID(PXCache sender, object row)
  {
    if (this.sourceCuryID.DeclaringType == (Type) null)
      return (string) null;
    int num = this.sourceCuryID.DeclaringType.IsAssignableFrom(sender.GetItemType()) ? 1 : 0;
    PXCache pxCache = num != 0 ? sender : sender.Graph.Caches[this.sourceCuryID.DeclaringType];
    object obj = num != 0 ? row : pxCache.Current;
    return (string) pxCache.GetValue(obj, this.sourceCuryID.Name);
  }
}
