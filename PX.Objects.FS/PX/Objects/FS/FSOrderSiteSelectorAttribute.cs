// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSOrderSiteSelectorAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSOrderSiteSelectorAttribute : PXSelectorAttribute
{
  protected string _InputMask;

  public FSOrderSiteSelectorAttribute()
    : base(typeof (Search<INSite.siteID, Where<Match<INSite, Current<AccessInfo.userName>>>>), new Type[3]
    {
      typeof (INSite.siteCD),
      typeof (INSite.descr),
      typeof (INSite.replenishmentClassID)
    })
  {
    this.DirtyRead = true;
    this.SubstituteKey = typeof (INSite.siteCD);
    this.DescriptionField = typeof (INSite.descr);
    this._UnconditionalSelect = BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Search<INSite.siteID, Where<INSite.siteID, Equal<Required<INSite.siteID>>>>)
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
