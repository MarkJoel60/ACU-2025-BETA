// Decompiled with JetBrains decompiler
// Type: PX.SM.PXTimeTagAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.SM;

public class PXTimeTagAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowDeletedSubscriber
{
  private const string _SynchronizationSlot_ = "_SyncScopeSlot_";

  public PXTimeTagAttribute(System.Type noteType)
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.Views.Caches.Add(typeof (SyncTimeTag));
  }

  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.UpdateTag(sender, e.Row);
  }

  public void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.UpdateTag(sender, e.Row);
  }

  public void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.UpdateTag(sender, e.Row);
  }

  protected void UpdateTag(PXCache sender, object row)
  {
    if (PXTimeTagAttribute.SyncScope.IsScoped())
      return;
    this.UpdateTag(sender, row, System.DateTime.UtcNow);
  }

  protected void UpdateTag(PXCache sender, object row, System.DateTime timetag)
  {
    Guid? noteIdReadonly = PXNoteAttribute.GetNoteIDReadonly(sender, row, (string) null);
    PXCache cach = sender.Graph.Caches[typeof (SyncTimeTag)];
    SyncTimeTag syncTimeTag = new SyncTimeTag()
    {
      NoteID = noteIdReadonly,
      TimeTag = new System.DateTime?(timetag)
    };
    using (new ReadOnlyScope(new PXCache[1]{ cach }))
      cach.Update((object) syncTimeTag);
  }

  public static void UpdateTag<Field>(PXCache cache, object row, System.DateTime tagTime) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXTimeTagAttribute)
        ((PXTimeTagAttribute) attribute).UpdateTag(cache, row, tagTime);
    }
  }

  public class SyncScope : IDisposable
  {
    private readonly bool prevState;

    public SyncScope()
    {
      this.prevState = PXTimeTagAttribute.SyncScope.IsScoped();
      PXContext.SetSlot<bool>("_SyncScopeSlot_", true);
    }

    public void Dispose() => PXContext.SetSlot<bool>("_SyncScopeSlot_", this.prevState);

    public static bool IsScoped() => PXContext.GetSlot<bool>("_SyncScopeSlot_");
  }
}
