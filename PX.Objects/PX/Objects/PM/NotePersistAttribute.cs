// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.NotePersistAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>Persist record in Note table with given NoteID</summary>
/// <exclude />
public class NotePersistAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber
{
  protected Type _NoteID;

  public NotePersistAttribute(Type NoteID) => this._NoteID = NoteID;

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PXNoteAttribute.GetNoteID(sender, e.Row, this._NoteID.Name);
    sender.Graph.Caches[typeof (Note)].IsDirty = false;
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PXNoteAttribute.GetNoteID(sender, e.Row, this._NoteID.Name);
  }
}
