// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryState.EditableStateControllerBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL.JournalEntryState;

public class EditableStateControllerBase : StateControllerBase
{
  protected EditableStateControllerBase(JournalEntry journalEntry)
    : base(journalEntry)
  {
  }

  public override void Batch_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Batch row = e.Row as Batch;
    base.Batch_RowSelected(cache, e);
    PXUIFieldAttribute.SetEnabled<Batch.hold>(cache, (object) row, !row.Scheduled.GetValueOrDefault());
    cache.AllowDelete = true;
    cache.AllowUpdate = true;
    ((PXCache) this.GLTranCache).AllowUpdate = true;
  }
}
