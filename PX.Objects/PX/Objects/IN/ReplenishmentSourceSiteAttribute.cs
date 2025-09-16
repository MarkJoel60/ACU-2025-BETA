// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ReplenishmentSourceSiteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class ReplenishmentSourceSiteAttribute : SiteAttribute
{
  private Type source;

  public ReplenishmentSourceSiteAttribute(Type replenishmentSource)
  {
    this.DescriptionField = typeof (INSite.descr);
    this.source = replenishmentSource;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    Type itemType1 = sender.GetItemType();
    ReplenishmentSourceSiteAttribute sourceSiteAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) sourceSiteAttribute1, __vmethodptr(sourceSiteAttribute1, OnRowSelected));
    rowSelected.AddHandler(itemType1, pxRowSelected);
    PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
    Type itemType2 = sender.GetItemType();
    ReplenishmentSourceSiteAttribute sourceSiteAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) sourceSiteAttribute2, __vmethodptr(sourceSiteAttribute2, OnRowUpdated));
    rowUpdated.AddHandler(itemType2, pxRowUpdated);
  }

  protected virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, e.Row != null && INReplenishmentSource.IsTransfer((string) sender.GetValue(e.Row, this.source.Name)));
  }

  protected virtual void OnRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null || INReplenishmentSource.IsTransfer((string) sender.GetValue(e.Row, this.source.Name)))
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }
}
