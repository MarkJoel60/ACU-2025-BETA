// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryState.CommonTypeStateController
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL.JournalEntryState;

public class CommonTypeStateController(JournalEntry journalEntry) : EditableStateControllerBase(journalEntry)
{
  public override void Batch_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (((PXGraph) this.JournalEntry).UnattendedMode)
      return;
    Batch row = e.Row as Batch;
    base.Batch_RowSelected(cache, e);
    PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<Batch.status>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<Batch.curyCreditTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<Batch.curyDebitTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<Batch.origBatchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<Batch.autoReverse>(cache, (object) row, !row.AutoReverseCopy.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<Batch.autoReverseCopy>(cache, (object) row, false);
    ((PXCache) this.GLTranCache).AllowInsert = true;
    ((PXCache) this.GLTranCache).AllowDelete = true;
    bool flag1 = row.AutoReverseCopy.GetValueOrDefault() || row.AutoReverse.GetValueOrDefault();
    bool flag2 = this.IsTaxTranCreationAllowed(row) && !flag1;
    PXUIFieldAttribute.SetEnabled<Batch.createTaxTrans>(cache, (object) row, flag2);
  }
}
