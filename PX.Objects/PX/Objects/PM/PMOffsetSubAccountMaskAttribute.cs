// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMOffsetSubAccountMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public sealed class PMOffsetSubAccountMaskAttribute : PMSubAccountMaskBaseAttribute
{
  public PMOffsetSubAccountMaskAttribute()
    : base("PMSETUPOFFSET", (PMAcctSubDefault.CustomListAttribute) new PMAcctSubDefault.OffsetSubListAttribute(), "S")
  {
  }

  public static void VerifyMask(
    PMAllocationDetail step,
    PMTran tran,
    PMProject project,
    PMTask task)
  {
    PMSubAccountMaskBaseAttribute.VerifyCommonMaskProperties(step, step.OffsetSubMask, project, task);
    if (step.OffsetSubMask.Contains("G") && !tran.OffsetSubID.HasValue)
      throw new PXException("Allocation rule is configured to use the source subaccount of transaction that is being allocated but the Subaccount is not set for the original transaction. Please correct your allocation step. Allocation Rule:{0} Step:{1}", new object[2]
      {
        (object) step.AllocationID,
        (object) step.StepID
      });
    if (step.OffsetSubMask.Contains("A") && !step.OffsetSubID.HasValue)
      throw new PXException("Allocation rule is configured to use the subaccount of allocation step but the subaccount is not set up. Please correct your allocation step. Allocation Rule:{0} Step:{1}", new object[2]
      {
        (object) step.AllocationID,
        (object) step.StepID
      });
  }

  public static string MakeSub(
    PXGraph graph,
    PMAllocationDetail step,
    PMTran tran,
    PMProject project,
    PMTask task)
  {
    return PMSubAccountMaskBaseAttribute.MakeSub<PMAllocationDetail.offsetSubMask, PMAcctSubDefault.OffsetSubListAttribute>(graph, step.OffsetSubMask, new object[7]
    {
      (object) tran.OffsetSubID,
      (object) step.OffsetSubID,
      (object) project.DefaultSalesSubID,
      (object) task.DefaultSalesSubID,
      (object) project.DefaultExpenseSubID,
      (object) task.DefaultExpenseSubID,
      (object) tran.SubID
    }, new Type[7]
    {
      typeof (PMTran.offsetSubID),
      typeof (PMAllocationDetail.offsetSubID),
      typeof (PMProject.defaultSalesSubID),
      typeof (PMTask.defaultSalesSubID),
      typeof (PMProject.defaultExpenseSubID),
      typeof (PMTask.defaultExpenseSubID),
      typeof (PMTran.subID)
    });
  }

  public static void VerifyMaskReversed(
    PMAllocationDetail step,
    PMTran tran,
    PMProject project,
    PMTask task)
  {
    PMSubAccountMaskBaseAttribute.VerifyCommonMaskProperties(step, step.OffsetSubMask, project, task);
    if (step.OffsetSubMask.Contains("S") && !tran.SubID.HasValue)
      throw new PXException("Allocation rule is configured to use the source subaccount of transaction that is being allocated but the Subaccount is not set for the original transaction. Please correct your allocation step. Allocation Rule:{0} Step:{1}", new object[2]
      {
        (object) step.AllocationID,
        (object) step.StepID
      });
    if (step.OffsetSubMask.Contains("A") && !step.SubID.HasValue)
      throw new PXException("Allocation rule is configured to use the subaccount of allocation step but the subaccount is not set up. Please correct your allocation step. Allocation Rule:{0} Step:{1}", new object[2]
      {
        (object) step.AllocationID,
        (object) step.StepID
      });
  }

  public static string MakeSubReversed(
    PXGraph graph,
    PMAllocationDetail step,
    PMTran tran,
    PMProject project,
    PMTask task)
  {
    return PMSubAccountMaskBaseAttribute.MakeSub<PMAllocationDetail.offsetSubMask, PMAcctSubDefault.OffsetSubListAttribute>(graph, step.OffsetSubMask, new object[7]
    {
      (object) tran.SubID,
      (object) step.SubID,
      (object) project.DefaultSalesSubID,
      (object) task.DefaultSalesSubID,
      (object) project.DefaultExpenseSubID,
      (object) task.DefaultExpenseSubID,
      (object) tran.OffsetSubID
    }, new Type[7]
    {
      typeof (PMTran.subID),
      typeof (PMAllocationDetail.subID),
      typeof (PMProject.defaultSalesSubID),
      typeof (PMTask.defaultSalesSubID),
      typeof (PMProject.defaultExpenseSubID),
      typeof (PMTask.defaultExpenseSubID),
      typeof (PMTran.offsetSubID)
    });
  }
}
