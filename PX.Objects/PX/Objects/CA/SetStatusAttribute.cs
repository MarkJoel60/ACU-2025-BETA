// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.SetStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class SetStatusAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber,
  IPXRowInsertingSubscriber,
  IPXRowUpdatingSubscriber,
  IPXRowSelectedSubscriber
{
  public static void UpdateStatus(PXCache cache, CAAdj row)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<CAAdj.status>((object) row))
    {
      if (attribute is SetStatusAttribute)
        (attribute as SetStatusAttribute).UpdateStatus(cache, row, row.Hold);
    }
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    // ISSUE: method pointer
    sender.Graph.FieldUpdating.AddHandler<CAAdj.hold>(new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__1_0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    sender.Graph.FieldVerifying.AddHandler<CAAdj.status>(SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9__1_1 ?? (SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9__1_1 = new PXFieldVerifying((object) SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCacheAttached\u003Eb__1_1))));
  }

  public void UpdateStatus(PXCache cache, CAAdj row, bool? isHold)
  {
    if (isHold.GetValueOrDefault())
      row.Status = "H";
    else if (row.Released.GetValueOrDefault())
      row.Status = "R";
    else if (row.Rejected.GetValueOrDefault())
      row.Status = "J";
    else if (!row.Approved.GetValueOrDefault() && !row.DontApprove.GetValueOrDefault())
      row.Status = "P";
    else
      row.Status = "B";
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    if (row == null)
      return;
    this.UpdateStatus(sender, row, row.Hold);
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    this.UpdateStatus(sender, row, row.Hold);
  }

  public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CAAdj newRow = (CAAdj) e.NewRow;
    this.UpdateStatus(sender, newRow, newRow.Hold);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CAAdj row = (CAAdj) e.Row;
    if (row == null)
      return;
    this.UpdateStatus(sender, row, row.Hold);
  }
}
