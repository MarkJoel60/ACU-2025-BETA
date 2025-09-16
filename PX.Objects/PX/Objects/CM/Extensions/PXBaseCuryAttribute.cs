// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.PXBaseCuryAttribute
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
/// Extends <see cref="T:PX.Data.PXDecimalAttribute" /> by defaulting the precision property.
/// Precision is taken from Base Currency that is configured on the Company level.
/// </summary>
/// <remarks>This is a NON-DB attribute. Use it for calculated fields that are not storred in database.</remarks>
public class PXBaseCuryAttribute : PXDecimalAttribute
{
  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    this._Precision = new int?(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(sender.Graph).BaseDecimalPlaces());
  }

  public virtual void CacheAttached(PXCache sender)
  {
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
    base.CacheAttached(sender);
  }
}
