// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimecardWeekStartDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class TimecardWeekStartDate : 
  PXDateAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowSelectingSubscriber
{
  private Type weekID;

  public TimecardWeekStartDate(Type weekID) => this.weekID = weekID;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType1 = sender.GetItemType();
    string name = this.weekID.Name;
    TimecardWeekStartDate timecardWeekStartDate1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) timecardWeekStartDate1, __vmethodptr(timecardWeekStartDate1, OnWeekIdUpdated));
    fieldUpdated.AddHandler(itemType1, name, pxFieldUpdated);
    if (!(sender.Graph.GetType() == typeof (PXGraph)))
      return;
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    Type itemType2 = sender.GetItemType();
    TimecardWeekStartDate timecardWeekStartDate2 = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) timecardWeekStartDate2, __vmethodptr(timecardWeekStartDate2, RowSelecting));
    rowSelecting.AddHandler(itemType2, pxRowSelecting);
  }

  protected virtual DateTime? GetWeekStartDate(PXCache sender, object row)
  {
    DateTime? weekStartDate = new DateTime?();
    if (this.weekID != (Type) null)
    {
      int? nullable = (int?) sender.GetValue(row, this.weekID.Name);
      if (nullable.HasValue)
        weekStartDate = new DateTime?(PXWeekSelector2Attribute.GetWeekStartDate(sender.Graph, nullable.Value));
    }
    return weekStartDate;
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    DateTime? weekStartDate = this.GetWeekStartDate(sender, e.Row);
    e.NewValue = (object) weekStartDate;
  }

  protected virtual void OnWeekIdUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    DateTime? weekStartDate = this.GetWeekStartDate(sender, e.Row);
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName, (object) weekStartDate);
  }

  public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    int? nullable = (int?) sender.GetValue(e.Row, this.weekID.Name);
    if (e.Row == null || !nullable.HasValue)
      return;
    using (new PXConnectionScope())
    {
      DateTime? weekStartDate = this.GetWeekStartDate(sender, e.Row);
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName, (object) weekStartDate);
    }
  }
}
