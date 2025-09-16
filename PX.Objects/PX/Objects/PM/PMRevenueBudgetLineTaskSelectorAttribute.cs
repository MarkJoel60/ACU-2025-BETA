// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRevenueBudgetLineTaskSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[Serializable]
public class PMRevenueBudgetLineTaskSelectorAttribute : PXCustomSelectorAttribute
{
  public PMRevenueBudgetLineTaskSelectorAttribute()
    : base(typeof (PMTask.taskID), new Type[2]
    {
      typeof (PMTask.taskCD),
      typeof (PMTask.description)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (PMTask.taskCD);
    ((PXSelectorAttribute) this).DescriptionField = typeof (PMTask.description);
  }

  protected virtual IEnumerable GetRecords()
  {
    PMRevenueBudgetLineTaskSelectorAttribute selectorAttribute = this;
    PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>> pxSelect = new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>>(selectorAttribute._Graph);
    HashSet<int> budgetedTasks = new HashSet<int>();
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) pxSelect).Select(objArray))
      budgetedTasks.Add(PXResult<PMRevenueBudget>.op_Implicit(pxResult).TaskID.Value);
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) new PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>>>(selectorAttribute._Graph)).Select(Array.Empty<object>()))
    {
      PMTask record = PXResult<PMTask>.op_Implicit(pxResult);
      if (budgetedTasks.Contains(record.TaskID.Value))
        yield return (object) record;
    }
  }
}
