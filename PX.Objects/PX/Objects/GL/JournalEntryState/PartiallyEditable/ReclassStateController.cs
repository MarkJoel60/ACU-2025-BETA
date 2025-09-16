// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntryState.PartiallyEditable.ReclassStateController
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL.JournalEntryState.PartiallyEditable;

public class ReclassStateController(JournalEntry journalEntry) : PartiallyEditableStateControllerBase(journalEntry)
{
  public override void Batch_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Batch row = e.Row as Batch;
    base.Batch_RowSelected(cache, e);
    PXUIFieldAttribute.SetEnabled<Batch.dateEntered>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<Batch.description>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<Batch.branchID>(cache, (object) row, true);
    PXUIFieldAttribute.SetVisible<GLTran.origBatchNbr>((PXCache) this.GLTranCache, (object) null, true);
    ((PXAction) this.JournalEntry.editReclassBatch).SetVisible(true);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
  public static bool HasRamainingAmount(bool? hasRemainingAmount, GLTran tran)
  {
    return hasRemainingAmount.GetValueOrDefault() || tran.CuryReclassRemainingAmt.GetValueOrDefault() != 0M;
  }
}
