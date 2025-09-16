// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BQL.ActivityCalc.LastActivityCalc`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR.BQL.ActivityCalc;

/// <summary>
/// The class that is used to calculate the fields of the last activity in <see cref="T:PX.Objects.CR.CRActivityStatistics">activity statistics</see>.
/// </summary>
/// <typeparam name="TargetField">The target field of <see cref="T:PX.Objects.CR.CRActivityStatistics" /> to set the value</typeparam>
/// <typeparam name="ReturnField">The source field of <see cref="T:PX.Objects.CR.CRActivity" /> to be set as <typeparamref name="TargetField" /></typeparam>
public class LastActivityCalc<TargetField, ReturnField> : ActivityCalcBase<
#nullable disable
TargetField, ReturnField>
  where TargetField : IBqlField
  where ReturnField : IBqlField
{
  protected override IEnumerable<CRActivity> FilterActivities(
    PXCache activityCache,
    IBqlCreator formula,
    IEnumerable<CRActivity> records)
  {
    return (IEnumerable<CRActivity>) base.FilterActivities(activityCache, formula, records).OrderBy<CRActivity, DateTime?>((Func<CRActivity, DateTime?>) (a => a.CreatedDateTime));
  }

  protected override 
  #nullable enable
  CRActivity? TryFindActivity(
    #nullable disable
    PXCache activityCache,
    IBqlCreator formula,
    IEnumerable<CRActivity> records)
  {
    return records.LastOrDefault<CRActivity>();
  }
}
