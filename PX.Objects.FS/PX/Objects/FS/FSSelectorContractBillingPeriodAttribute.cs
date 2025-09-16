// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorContractBillingPeriodAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorContractBillingPeriodAttribute : PXCustomSelectorAttribute
{
  public FSSelectorContractBillingPeriodAttribute()
    : base(typeof (FSContractPeriod.contractPeriodID), new Type[3]
    {
      typeof (FSContractPeriod.billingPeriod),
      typeof (FSContractPeriod.status),
      typeof (FSContractPeriod.invoiced)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (FSContractPeriod.billingPeriod);
    ((PXSelectorAttribute) this).ValidateValue = false;
  }

  protected virtual IEnumerable GetRecords()
  {
    FSSelectorContractBillingPeriodAttribute billingPeriodAttribute = this;
    FSContractPeriod record1 = (FSContractPeriod) null;
    PXCache cach = billingPeriodAttribute._Graph.Caches[typeof (FSContractPeriod)];
    foreach (object current in PXView.Currents)
    {
      if (current != null && current.GetType() == typeof (FSContractPeriod))
      {
        record1 = (FSContractPeriod) current;
        break;
      }
    }
    if (record1 == null)
      record1 = (FSContractPeriod) cach.Current;
    if (record1 != null)
    {
      int? contractPeriodId = record1.ContractPeriodID;
      int num = 0;
      if (contractPeriodId.GetValueOrDefault() > num & contractPeriodId.HasValue)
      {
        PXResultset<FSContractPeriod> pxResultset = PXSelectBase<FSContractPeriod, PXSelect<FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>, OrderBy<Desc<FSContractPeriod.endPeriodDate>>>.Config>.Select(billingPeriodAttribute._Graph, Array.Empty<object>());
        string actionType = "SBP";
        FSContractPeriodFilter current = (FSContractPeriodFilter) billingPeriodAttribute._Graph.Caches[typeof (FSContractPeriodFilter)].Current;
        if (current != null)
          actionType = current.Actions;
        if (pxResultset.Count > 0)
        {
          foreach (PXResult<FSContractPeriod> pxResult in pxResultset)
          {
            FSContractPeriod record2 = PXResult<FSContractPeriod>.op_Implicit(pxResult);
            if (actionType == "MBP" && record2.Status == "I")
              yield return (object) record2;
            else if (actionType == "SBP" && (record2.Status != "I" || record2.Invoiced.GetValueOrDefault()))
              yield return (object) record2;
          }
        }
        actionType = (string) null;
      }
      else
        yield return (object) record1;
    }
  }
}
