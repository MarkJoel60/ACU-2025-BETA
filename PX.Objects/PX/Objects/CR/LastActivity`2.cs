// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LastActivity`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

[Obsolete]
public sealed class LastActivity<TargetField, ReturnField> : IBqlUnboundAggregateCalculator
  where TargetField : IBqlField
  where ReturnField : IBqlField
{
  private static object CalcFormula(IBqlCreator formula, PXCache cache, object item)
  {
    object obj = (object) null;
    bool? nullable = new bool?();
    BqlFormula.Verify(cache, item, formula, ref nullable, ref obj);
    return obj;
  }

  public object Calculate(
    PXCache cache,
    object row,
    IBqlCreator formula,
    object[] records,
    int digit)
  {
    if (cache.Cached.Count() < 1L)
      return (object) null;
    bool flag = typeof (TargetField) == typeof (CRActivityStatistics.initialOutgoingActivityCompletedAtDate) || typeof (TargetField) == typeof (CRActivityStatistics.initialOutgoingActivityCompletedAtNoteID);
    List<object> objectList = new List<object>((IEnumerable<object>) records);
    if (row != null && row is CRActivity && !NonGenericIEnumerableExtensions.Any_((IEnumerable) objectList.Where<object>((Func<object, bool>) (a => ((CRActivity) a).NoteID.Equals((object) ((CRActivity) row).NoteID)))))
      objectList.Add(row);
    PXCache crActivityCache = cache.Graph.Caches[typeof (CRActivity)];
    if (row is CRActivity && ((bool?) LastActivity<TargetField, ReturnField>.CalcFormula(formula, cache, row)).GetValueOrDefault() && !flag)
      return crActivityCache == null ? cache.GetValue<ReturnField>(row) : this.getLastValue(crActivityCache, formula, objectList);
    if (records.Length < 1 || !(records[0] is CRActivity))
      return (object) null;
    if (crActivityCache == null)
      return (object) null;
    return flag ? crActivityCache.GetValue<ReturnField>(objectList.Where<object>((Func<object, bool>) (a => ((bool?) LastActivity<TargetField, ReturnField>.CalcFormula(formula, crActivityCache, a)).GetValueOrDefault())).OrderBy<object, DateTime?>((Func<object, DateTime?>) (a => ((CRActivity) a).CompletedDate)).FirstOrDefault<object>()) : this.getLastValue(crActivityCache, formula, objectList);
  }

  private object getLastValue(PXCache crActivityCache, IBqlCreator formula, List<object> records)
  {
    return crActivityCache.GetValue<ReturnField>(records.Where<object>((Func<object, bool>) (a => ((bool?) LastActivity<TargetField, ReturnField>.CalcFormula(formula, crActivityCache, a)).GetValueOrDefault())).OrderBy<object, DateTime?>((Func<object, DateTime?>) (a => ((CRActivity) a).CreatedDateTime)).LastOrDefault<object>());
  }

  public object Calculate(
    PXCache cache,
    object row,
    object oldrow,
    IBqlCreator formula,
    int digit)
  {
    return (object) null;
  }
}
