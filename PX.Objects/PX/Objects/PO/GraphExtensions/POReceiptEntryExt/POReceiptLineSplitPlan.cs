// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptLineSplitPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class POReceiptLineSplitPlan : POReceiptLineSplitPlanBase<POReceiptLineSplit>
{
  public override void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POReceipt> e)
  {
    base._(e);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.POReceipt>>) e).Cache.ObjectsEqual<PX.Objects.PO.POReceipt.receiptDate, PX.Objects.PO.POReceipt.hold>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.refNoteID, IBqlGuid>.IsEqual<BqlField<PX.Objects.PO.POReceipt.noteID, IBqlGuid>.FromCurrent>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      INItemPlan plan = PXResult<INItemPlan>.op_Implicit(pxResult);
      this.DefaultValuesFromReceipt(plan, e.Row);
      GraphHelper.MarkUpdated((PXCache) this.PlanCache, (object) plan, true);
    }
  }

  protected override HashSet<long?> CollectReceiptPlans(PX.Objects.PO.POReceipt receipt)
  {
    return new HashSet<long?>(((IEnumerable<PXResult<POReceiptLineSplit>>) PXSelectBase<POReceiptLineSplit, PXViewOf<POReceiptLineSplit>.BasedOn<SelectFromBase<POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptLineSplit>, PX.Objects.PO.POReceipt, POReceiptLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>())).AsEnumerable<PXResult<POReceiptLineSplit>>().Select<PXResult<POReceiptLineSplit>, long?>((Func<PXResult<POReceiptLineSplit>, long?>) (s => PXResult<POReceiptLineSplit>.op_Implicit(s).PlanID)));
  }
}
