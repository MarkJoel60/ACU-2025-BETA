// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryState.PartiallyEditable.TrialBalanceStateController
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL.JournalEntryState.PartiallyEditable;

public class TrialBalanceStateController(JournalEntry journalEntry) : 
  PartiallyEditableStateControllerBase(journalEntry)
{
  public override void Batch_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Batch row = e.Row as Batch;
    base.Batch_RowSelected(cache, e);
    PXUIFieldAttribute.SetEnabled<Batch.description>(cache, (object) row, true);
  }

  public override void GLTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e, Batch batch)
  {
    GLTran row = e.Row as GLTran;
    base.GLTran_RowSelected(sender, e, batch);
    PXUIFieldAttribute.SetEnabled<GLTran.tranDesc>(sender, (object) row, true);
  }
}
