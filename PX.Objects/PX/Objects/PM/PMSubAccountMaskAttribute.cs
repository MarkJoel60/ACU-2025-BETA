// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMSubAccountMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public sealed class PMSubAccountMaskAttribute : PMSubAccountMaskBaseAttribute
{
  public PMSubAccountMaskAttribute()
    : base("PMSETUP", (PMAcctSubDefault.CustomListAttribute) new PMAcctSubDefault.SubListAttribute(), "S")
  {
  }

  public static void VerifyMask(
    PMAllocationDetail step,
    PMTran tran,
    PMProject project,
    PMTask task)
  {
    PMSubAccountMaskBaseAttribute.VerifyCommonMaskProperties(step, step.SubMask, project, task);
    if (step.SubMask.Contains("S") && !tran.SubID.HasValue)
    {
      string message = PXLocalizer.LocalizeFormat("Allocation rule is configured to use the source subaccount of transaction that is being allocated but the Subaccount is not set for the original transaction. Please correct your allocation step. Allocation Rule:{0} Step:{1}", new object[2]
      {
        (object) step.AllocationID,
        (object) step.StepID
      });
      throw new PMAllocationException(tran.RefNbr, message);
    }
    if (step.SubMask.Contains("G") && !tran.OffsetSubID.HasValue)
    {
      string message = PXLocalizer.LocalizeFormat("Allocation rule is configured to use the source subaccount of transaction that is being allocated but the Subaccount is not set for the original transaction. Please correct your allocation step. Allocation Rule:{0} Step:{1}", new object[2]
      {
        (object) step.AllocationID,
        (object) step.StepID
      });
      throw new PMAllocationException(tran.RefNbr, message);
    }
    if (step.SubMask.Contains("A") && !step.SubID.HasValue)
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
    return PMSubAccountMaskBaseAttribute.MakeSub<PMAllocationDetail.subMask, PMAcctSubDefault.SubListAttribute>(graph, step.SubMask, new object[7]
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
