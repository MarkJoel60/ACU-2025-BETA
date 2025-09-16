// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BQL.ActivityCalc.ActivityCalcBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR.BQL.ActivityCalc;

/// <summary>
/// The base class that is used to calculate <see cref="T:PX.Objects.CR.CRActivityStatistics">activity statistics</see>.
/// </summary>
/// <typeparam name="TargetField">The target field of <see cref="T:PX.Objects.CR.CRActivityStatistics" /> to set value</typeparam>
/// <typeparam name="ReturnField">The source field of <see cref="T:PX.Objects.CR.CRActivity" /> to be set as <typeparamref name="TargetField" /></typeparam>
public abstract class ActivityCalcBase<TargetField, ReturnField> : IBqlUnboundAggregateCalculator
  where TargetField : IBqlField
  where ReturnField : IBqlField
{
  protected virtual object? Calculate(
    PXCache cache,
    PXCache activityCache,
    CRActivity? row,
    IBqlCreator formula,
    IReadOnlyCollection<CRActivity> records,
    int digit)
  {
    if (cache.Cached.Count() < 1L)
      return (object) null;
    if (row != null)
    {
      CRActivity row1 = row;
      if (this.ActivityRowSatisfiesFormula(activityCache, formula, row1))
        return activityCache.GetValue<ReturnField>((object) row);
    }
    CRActivity activity = this.TryFindActivity(activityCache, formula, this.FilterActivities(activityCache, formula, (IEnumerable<CRActivity>) records));
    return activity != null ? activityCache.GetValue<ReturnField>((object) activity) : (object) null;
  }

  /// <exclude />
  protected virtual bool ActivitySatisfiesFormula(
    PXCache cache,
    IBqlCreator formula,
    CRActivity item)
  {
    object obj = (object) null;
    bool? nullable = new bool?();
    BqlFormula.Verify(cache, (object) item, formula, ref nullable, ref obj);
    return obj is bool flag && flag;
  }

  /// <exclude />
  protected virtual bool ActivityRowSatisfiesFormula(
    PXCache cache,
    IBqlCreator formula,
    CRActivity row)
  {
    return this.ActivitySatisfiesFormula(cache, formula, row);
  }

  /// <exclude />
  protected virtual IEnumerable<CRActivity> FilterActivities(
    PXCache activityCache,
    IBqlCreator formula,
    IEnumerable<CRActivity> records)
  {
    return records.Where<CRActivity>((Func<CRActivity, bool>) (a => this.ActivitySatisfiesFormula(activityCache, formula, a)));
  }

  /// <exclude />
  protected abstract CRActivity? TryFindActivity(
    PXCache activityCache,
    IBqlCreator formula,
    IEnumerable<CRActivity> records);

  /// <exclude />
  object? IBqlUnboundAggregateCalculator.Calculate(
    PXCache cache,
    object row,
    IBqlCreator formula,
    object[] records,
    int digit)
  {
    List<CRActivity> list = records.OfType<CRActivity>().ToList<CRActivity>();
    if (list != null)
    {
      PXCache cach = cache.Graph.Caches[typeof (CRActivity)];
      if (cach != null)
        return this.Calculate(cache, cach, row as CRActivity, formula, (IReadOnlyCollection<CRActivity>) list, digit);
    }
    return (object) null;
  }

  /// <exclude />
  object? IBqlUnboundAggregateCalculator.Calculate(
    PXCache cache,
    object row,
    object oldrow,
    IBqlCreator formula,
    int digit)
  {
    return (object) null;
  }
}
