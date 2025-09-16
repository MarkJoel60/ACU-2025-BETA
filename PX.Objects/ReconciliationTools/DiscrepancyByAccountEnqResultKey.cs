// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.DiscrepancyByAccountEnqResultKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;

#nullable disable
namespace ReconciliationTools;

public class DiscrepancyByAccountEnqResultKey
{
  public string FinPeriodID;
  public int? AccountID;
  public int? SubID;

  public DiscrepancyByAccountEnqResultKey(GLTran tran)
  {
    this.FinPeriodID = tran.FinPeriodID;
    this.AccountID = tran.AccountID;
    this.SubID = tran.SubID;
  }

  public override int GetHashCode()
  {
    return !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.subAccount>() ? Tuple.Create<string, int?>(this.FinPeriodID, this.AccountID).GetHashCode() : Tuple.Create<string, int?, int?>(this.FinPeriodID, this.AccountID, this.SubID).GetHashCode();
  }

  public override bool Equals(object obj)
  {
    DiscrepancyByAccountEnqResultKey accountEnqResultKey = (DiscrepancyByAccountEnqResultKey) obj;
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.subAccount>())
    {
      if (!(this.FinPeriodID == accountEnqResultKey.FinPeriodID))
        return false;
      int? accountId1 = this.AccountID;
      int? accountId2 = accountEnqResultKey.AccountID;
      return accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue;
    }
    if (this.FinPeriodID == accountEnqResultKey.FinPeriodID)
    {
      int? accountId = this.AccountID;
      int? nullable = accountEnqResultKey.AccountID;
      if (accountId.GetValueOrDefault() == nullable.GetValueOrDefault() & accountId.HasValue == nullable.HasValue)
      {
        nullable = this.SubID;
        int? subId = accountEnqResultKey.SubID;
        return nullable.GetValueOrDefault() == subId.GetValueOrDefault() & nullable.HasValue == subId.HasValue;
      }
    }
    return false;
  }
}
