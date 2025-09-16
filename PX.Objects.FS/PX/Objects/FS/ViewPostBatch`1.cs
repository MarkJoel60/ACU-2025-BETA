// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ViewPostBatch`1
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class ViewPostBatch<TNode>(PXGraph graph, string name) : PXAction<TNode>(graph, name) where TNode : class, IBqlTable, new()
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
        FSBillHistory current = (FSBillHistory) adapter.View.Graph.Caches[typeof (FSBillHistory)].Current;
        if (current != null)
        {
          if (string.IsNullOrEmpty(current.ServiceContractRefNbr))
          {
            PostBatchMaint instance = PXGraph.CreateInstance<PostBatchMaint>();
            ((PXSelectBase<FSPostBatch>) instance.BatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) instance.BatchRecords).Search<FSPostBatch.batchID>((object) current.BatchID, Array.Empty<object>()));
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          ContractPostBatchMaint instance1 = PXGraph.CreateInstance<ContractPostBatchMaint>();
          ((PXSelectBase<FSContractPostBatch>) instance1.ContractBatchRecords).Current = PXResultset<FSContractPostBatch>.op_Implicit(((PXSelectBase<FSContractPostBatch>) instance1.ContractBatchRecords).Search<FSContractPostBatch.contractPostBatchID>((object) current.BatchID, Array.Empty<object>()));
          PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
          ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException1;
        }
      }
      yield return obj;
    }
  }
}
