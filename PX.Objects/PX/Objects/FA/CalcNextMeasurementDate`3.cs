// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.CalcNextMeasurementDate`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class CalcNextMeasurementDate<LastDate, DateFrom, AssetID> : 
  BqlFormulaEvaluator<LastDate, DateFrom, AssetID>
  where LastDate : IBqlOperand
  where DateFrom : IBqlOperand
  where AssetID : IBqlOperand
{
  public virtual object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    DateTime? par1 = (DateTime?) pars[typeof (LastDate)];
    DateTime? par2 = (DateTime?) pars[typeof (DateFrom)];
    int? par3 = (int?) pars[typeof (AssetID)];
    DateTime? nullable = par1 ?? par2;
    if (!nullable.HasValue)
      return (object) null;
    FAUsageSchedule faUsageSchedule = PXResultset<FAUsageSchedule>.op_Implicit(PXSelectBase<FAUsageSchedule, PXSelectJoin<FAUsageSchedule, InnerJoin<FixedAsset, On<FixedAsset.usageScheduleID, Equal<FAUsageSchedule.scheduleID>>>, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) par3
    }));
    if (faUsageSchedule == null)
      return (object) null;
    switch (faUsageSchedule.ReadUsageEveryPeriod)
    {
      case "D":
        return (object) nullable.Value.AddDays((double) faUsageSchedule.ReadUsageEveryValue.GetValueOrDefault());
      case "W":
        return (object) nullable.Value.AddDays((double) (faUsageSchedule.ReadUsageEveryValue.GetValueOrDefault() * 7));
      case "M":
        return (object) nullable.Value.AddMonths(faUsageSchedule.ReadUsageEveryValue.GetValueOrDefault());
      case "Y":
        return (object) nullable.Value.AddYears(faUsageSchedule.ReadUsageEveryValue.GetValueOrDefault());
      default:
        return (object) null;
    }
  }
}
