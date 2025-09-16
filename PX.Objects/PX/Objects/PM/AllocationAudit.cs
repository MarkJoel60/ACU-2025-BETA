// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AllocationAudit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class AllocationAudit : PXGraph<AllocationAudit>
{
  public PXFilter<AllocationPMTran> destantion;
  [PXHidden]
  public PXSelect<PMAllocationSourceTran> allocationSourceTran;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<PMTran, LeftJoin<PMAllocationSourceTran, On<PMTran.tranID, Equal<PMAllocationSourceTran.tranID>>>> source;
  public PXAction<PMTran> viewBatch;
  public PXAction<PMTran> viewAllocationRule;

  public IEnumerable Source(PXAdapter adapter)
  {
    return this.GetSources(((PXSelectBase<AllocationPMTran>) this.destantion).Current.TranID);
  }

  public IEnumerable GetSources(long? dst)
  {
    AllocationAudit allocationAudit = this;
    if (dst.HasValue)
    {
      PXSelect<PMAllocationAuditTran, Where<PMAllocationAuditTran.tranID, Equal<Required<PMAllocationAuditTran.tranID>>>> pxSelect1 = new PXSelect<PMAllocationAuditTran, Where<PMAllocationAuditTran.tranID, Equal<Required<PMAllocationAuditTran.tranID>>>>((PXGraph) allocationAudit);
      PXSelectJoin<PMTran, LeftJoin<PMAllocationSourceTran, On<PMTran.tranID, Equal<PMAllocationSourceTran.tranID>>>, Where<PMAllocationSourceTran.tranID, Equal<Required<PMAllocationSourceTran.tranID>>>> selectSource = new PXSelectJoin<PMTran, LeftJoin<PMAllocationSourceTran, On<PMTran.tranID, Equal<PMAllocationSourceTran.tranID>>>, Where<PMAllocationSourceTran.tranID, Equal<Required<PMAllocationSourceTran.tranID>>>>((PXGraph) allocationAudit);
      PXSelect<PMAllocationAuditTran, Where<PMAllocationAuditTran.tranID, Equal<Required<PMAllocationAuditTran.tranID>>>> pxSelect2 = pxSelect1;
      object[] objArray1 = new object[1]{ (object) dst };
      foreach (PXResult<PMAllocationAuditTran> pxResult in ((PXSelectBase<PMAllocationAuditTran>) pxSelect2).Select(objArray1))
      {
        PMAllocationAuditTran allocationAuditTran = PXResult<PMAllocationAuditTran>.op_Implicit(pxResult);
        PXSelectJoin<PMTran, LeftJoin<PMAllocationSourceTran, On<PMTran.tranID, Equal<PMAllocationSourceTran.tranID>>>, Where<PMAllocationSourceTran.tranID, Equal<Required<PMAllocationSourceTran.tranID>>>> pxSelectJoin = selectSource;
        object[] objArray2 = new object[1]
        {
          (object) allocationAuditTran.SourceTranID
        };
        foreach (PXResult<PMTran, PMAllocationSourceTran> source in ((PXSelectBase<PMTran>) pxSelectJoin).Select(objArray2))
          yield return (object) source;
      }
    }
  }

  public AllocationAudit()
  {
    ((PXSelectBase) this.source).Cache.AllowDelete = false;
    ((PXSelectBase) this.source).Cache.AllowInsert = false;
    ((PXSelectBase) this.source).Cache.AllowUpdate = false;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.refNbr, Equal<Current<PMTran.refNbr>>, And<PMRegister.module, Equal<Current<PMTran.tranType>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())), (PXRedirectHelper.WindowMode) 1);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewAllocationRule(PXAdapter adapter)
  {
    AllocationMaint instance = PXGraph.CreateInstance<AllocationMaint>();
    PMAllocation pmAllocation = PXResultset<PMAllocation>.op_Implicit(PXSelectBase<PMAllocation, PXSelectJoin<PMAllocation, InnerJoin<PMAllocationSourceTran, On<PMAllocation.allocationID, Equal<PMAllocationSourceTran.allocationID>>>, Where<PMAllocationSourceTran.tranID, Equal<Required<PMTran.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<PMTran>) this.source).Current.TranID
    }));
    ((PXSelectBase<PMAllocation>) instance.Allocations).Current = pmAllocation;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Allocation Step")]
  [PXDBInt(IsKey = true)]
  public void _(
    Events.CacheAttached<PMAllocationSourceTran.stepID> e)
  {
  }
}
