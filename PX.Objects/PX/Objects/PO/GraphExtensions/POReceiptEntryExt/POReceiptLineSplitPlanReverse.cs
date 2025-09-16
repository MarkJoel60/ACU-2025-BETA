// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptLineSplitPlanReverse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class POReceiptLineSplitPlanReverse : POReceiptLineSplitPlanBase<PX.Objects.PO.Reverse.POReceiptLineSplit>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() || PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>();
  }

  protected override HashSet<long?> CollectReceiptPlans(POReceipt receipt)
  {
    return new HashSet<long?>(((IEnumerable<PXResult<PX.Objects.PO.Reverse.POReceiptLineSplit>>) PXSelectBase<PX.Objects.PO.Reverse.POReceiptLineSplit, PXViewOf<PX.Objects.PO.Reverse.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.Reverse.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.Reverse.POReceiptLineSplit.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<PX.Objects.PO.Reverse.POReceiptLineSplit.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, PX.Objects.PO.Reverse.POReceiptLineSplit>, POReceipt, PX.Objects.PO.Reverse.POReceiptLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new POReceipt[1]
    {
      receipt
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.PO.Reverse.POReceiptLineSplit>>().Select<PXResult<PX.Objects.PO.Reverse.POReceiptLineSplit>, long?>((Func<PXResult<PX.Objects.PO.Reverse.POReceiptLineSplit>, long?>) (s => PXResult<PX.Objects.PO.Reverse.POReceiptLineSplit>.op_Implicit(s).PlanID)));
  }
}
