// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SubAccountRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Base Attribute for SubCD field. Aggregates PXFieldAttribute, PXUIFieldAttribute and PXDimensionAttribute.
/// PXDimensionAttribute selector has no restrictions and returns all records.
/// </summary>
[PXDBString(30, IsUnicode = true, InputMask = "", PadSpaced = true)]
[PXUIField]
public class SubAccountRawAttribute : PXEntityAttribute
{
  protected const string _DimensionName = "SUBACCOUNT";
  protected bool _SuppressValidation;

  public SubAccountRawAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute("SUBACCOUNT")
    {
      ValidComboRequired = false
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public SubAccountRawAttribute(DimensionLookupMode lookupMode)
  {
    PXDimensionValueLookupModeAttribute lookupModeAttribute = new PXDimensionValueLookupModeAttribute("SUBACCOUNT", lookupMode);
    lookupModeAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) lookupModeAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public bool SuppressValidation
  {
    get => this._SuppressValidation;
    set => this._SuppressValidation = value;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!this._SuppressValidation || !(typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber)) || this._SelAttrIndex < 0)
      return;
    subscribers.Remove(((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] as ISubscriber);
  }
}
