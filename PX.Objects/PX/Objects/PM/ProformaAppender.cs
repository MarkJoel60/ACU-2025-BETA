// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaAppender
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProformaAppender : PMBillEngine
{
  public void SetProformaEntry(ProformaEntry proformaEntry) => this.proformaEntry = proformaEntry;

  public override List<PMTran> SelectBillingBase(
    int? projectID,
    int? taskID,
    int? accountGroupID,
    bool includeNonBillable)
  {
    List<PMTran> pmTranList = new List<PMTran>();
    foreach (PMTran pmTran in ((PXSelectBase) this.proformaEntry.Unbilled).Cache.Updated)
    {
      bool? nullable1 = pmTran.Selected;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = pmTran.Billed;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = pmTran.ExcludedFromBilling;
          if (!nullable1.GetValueOrDefault())
          {
            int? taskId = pmTran.TaskID;
            int? nullable2 = taskID;
            if (taskId.GetValueOrDefault() == nullable2.GetValueOrDefault() & taskId.HasValue == nullable2.HasValue)
            {
              int? accountGroupId = pmTran.AccountGroupID;
              int? nullable3 = accountGroupID;
              if (accountGroupId.GetValueOrDefault() == nullable3.GetValueOrDefault() & accountGroupId.HasValue == nullable3.HasValue)
                pmTranList.Add(pmTran);
            }
          }
        }
      }
    }
    return pmTranList;
  }

  public void InitRateEngine(IList<string> distinctRateTables, IList<string> distinctRateTypes)
  {
    this.rateEngine = this.CreateRateEngineV2(distinctRateTables, distinctRateTypes);
  }
}
