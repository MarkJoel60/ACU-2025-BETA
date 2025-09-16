// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.OrderSiteSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public class OrderSiteSelectorAttribute : PXSelectorAttribute
{
  protected string _InputMask;

  public OrderSiteSelectorAttribute()
    : base(typeof (Search2<SOOrderSite.siteID, InnerJoin<PX.Objects.IN.INSite, On<SOOrderSite.FK.Site>>, Where<SOOrderSite.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>), new Type[3]
    {
      typeof (PX.Objects.IN.INSite.siteCD),
      typeof (PX.Objects.IN.INSite.descr),
      typeof (PX.Objects.IN.INSite.replenishmentClassID)
    })
  {
    this.DirtyRead = true;
    this.SubstituteKey = typeof (PX.Objects.IN.INSite.siteCD);
    this.DescriptionField = typeof (PX.Objects.IN.INSite.descr);
    this._UnconditionalSelect = BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Search<PX.Objects.IN.INSite.siteID, Where<PX.Objects.IN.INSite.siteID, Equal<Required<PX.Objects.IN.INSite.siteID>>>>)
    });
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXDimensionAttribute dimensionAttribute = new PXDimensionAttribute("INSITE");
    ((PXEventSubscriberAttribute) dimensionAttribute).CacheAttached(sender);
    ((PXEventSubscriberAttribute) dimensionAttribute).FieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    PXFieldSelectingEventArgs selectingEventArgs = new PXFieldSelectingEventArgs((object) null, (object) null, true, false);
    dimensionAttribute.FieldSelecting(sender, selectingEventArgs);
    this._InputMask = ((PXStringState) selectingEventArgs.ReturnState).InputMask;
  }

  public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.SubstituteKeyFieldSelecting(sender, e);
    if (((PXEventSubscriberAttribute) this)._AttributeLevel != 2 && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), (string) null, new bool?(), new int?(), this._InputMask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
  }
}
