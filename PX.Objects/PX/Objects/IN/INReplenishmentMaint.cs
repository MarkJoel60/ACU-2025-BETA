// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN;

[PXHidden]
public class INReplenishmentMaint : PXGraph<INReplenishmentMaint, INReplenishmentOrder>
{
  public PXSelect<INReplenishmentOrder> Document;
  public PXSelect<PX.Objects.PO.POLine> planRelease;
  public PXSetup<INSetup> setup;

  public class POLinePlan : PX.Objects.PO.GraphExtensions.POOrderEntryExt.POLinePlan<INReplenishmentMaint>
  {
    protected virtual void _(PX.Data.Events.RowInserted<INReplenishmentOrder> e)
    {
      PXNoteAttribute.GetNoteID<INReplenishmentOrder.noteID>(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<INReplenishmentOrder>>) e).Cache, (object) e.Row);
    }

    public override void _(PX.Data.Events.RowPersisting<INItemPlan> e)
    {
      if (PXDBOperationExt.Command(e.Operation) != 3)
      {
        e.Row.RefNoteID = PXNoteAttribute.GetNoteID<INReplenishmentOrder.noteID>(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<INReplenishmentOrder>) this.Base.Document).Current);
        e.Row.RefEntityType = typeof (INReplenishmentOrder).FullName;
      }
      base._(e);
    }
  }
}
