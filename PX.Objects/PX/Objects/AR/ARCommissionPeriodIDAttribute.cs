// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCommissionPeriodIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

public class ARCommissionPeriodIDAttribute : PeriodIDAttribute, IPXFieldVerifyingSubscriber
{
  protected int _SelAttrIndex = -1;

  public ARCommissionPeriodIDAttribute()
    : base(searchType: typeof (Search<ARSPCommissionPeriod.commnPeriodID>))
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(typeof (Search<ARSPCommissionPeriod.commnPeriodID>))
    {
      SelectorMode = (PXSelectorMode) 1
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) this._Attributes).Count - 1;
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber))
      subscribers.Add(this as ISubscriber);
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      if (this._SelAttrIndex == -1)
        return;
      ((IPXFieldVerifyingSubscriber) this._Attributes[this._SelAttrIndex]).FieldVerifying(sender, e);
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatPeriod((string) e.NewValue);
      throw;
    }
  }
}
