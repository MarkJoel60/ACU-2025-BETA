// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ViewLinkedDoc`2
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.EP;
using PX.Objects.SO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class ViewLinkedDoc<TNode, TDetail>(PXGraph graph, string name) : PXAction<TNode>(graph, name)
  where TNode : class, IBqlTable, new()
  where TDetail : class, IBqlTable, IFSSODetBase, new()
{
  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable Handler(PXAdapter adapter)
  {
    PXCache cache = adapter.View.Cache;
    if (adapter.View.Cache.IsDirty)
      adapter.View.Graph.GetSaveAction().Press();
    foreach (object obj in adapter.Get())
    {
      if ((object) (!(obj is PXResult) ? (TNode) obj : (TNode) ((PXResult) obj)[0]) != null)
      {
        TDetail current = (TDetail) adapter.View.Graph.Caches[typeof (TDetail)].Current;
        if ((object) current != null)
        {
          if (current.LinkedEntityType == "SO")
          {
            SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
            ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) current.LinkedDocRefNbr, new object[1]
            {
              (object) current.LinkedDocType
            }));
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Sales Order");
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          if (current.IsExpenseReceiptItem)
          {
            ExpenseClaimDetailEntry instance = PXGraph.CreateInstance<ExpenseClaimDetailEntry>();
            ((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Current = PXResultset<EPExpenseClaimDetails>.op_Implicit(((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Search<EPExpenseClaimDetails.claimDetailCD>((object) current.LinkedDocRefNbr, Array.Empty<object>()));
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Expense Receipt");
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          if (current.IsAPBillItem)
          {
            APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
            ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) current.LinkedDocRefNbr, new object[1]
            {
              (object) current.LinkedDocType
            }));
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "AP Bill");
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
        }
      }
      yield return obj;
    }
  }
}
