// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.Overrides.AssetProcess.FABookHistAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA.Overrides.AssetProcess;

public class FABookHistAccumAttribute : PXAccumulatorAttribute
{
  public FABookHistAccumAttribute()
    : base(new Type[20]
    {
      typeof (FABookHistory.ytdCalculated),
      typeof (FABookHistory.ytdBal),
      typeof (FABookHistory.ytdBal),
      typeof (FABookHistory.ytdDeprBase),
      typeof (FABookHistory.ytdDeprBase),
      typeof (FABookHistory.ytdBonus),
      typeof (FABookHistory.ytdBonusTaken),
      typeof (FABookHistory.ytdBonusCalculated),
      typeof (FABookHistory.ytdBonusRecap),
      typeof (FABookHistory.ytdTax179),
      typeof (FABookHistory.ytdTax179Taken),
      typeof (FABookHistory.ytdTax179Calculated),
      typeof (FABookHistory.ytdTax179Recap),
      typeof (FABookHistory.ytdAcquired),
      typeof (FABookHistory.ytdDepreciated),
      typeof (FABookHistory.ytdDisposalAmount),
      typeof (FABookHistory.ytdRGOL),
      typeof (FABookHistory.ytdSuspended),
      typeof (FABookHistory.ytdReversed),
      typeof (FABookHistory.ytdReconciled)
    }, new Type[20]
    {
      typeof (FABookHistory.ytdCalculated),
      typeof (FABookHistory.begBal),
      typeof (FABookHistory.ytdBal),
      typeof (FABookHistory.begDeprBase),
      typeof (FABookHistory.ytdDeprBase),
      typeof (FABookHistory.ytdBonus),
      typeof (FABookHistory.ytdBonusTaken),
      typeof (FABookHistory.ytdBonusCalculated),
      typeof (FABookHistory.ytdBonusRecap),
      typeof (FABookHistory.ytdTax179),
      typeof (FABookHistory.ytdTax179Taken),
      typeof (FABookHistory.ytdTax179Calculated),
      typeof (FABookHistory.ytdTax179Recap),
      typeof (FABookHistory.ytdAcquired),
      typeof (FABookHistory.ytdDepreciated),
      typeof (FABookHistory.ytdDisposalAmount),
      typeof (FABookHistory.ytdRGOL),
      typeof (FABookHistory.ytdSuspended),
      typeof (FABookHistory.ytdReversed),
      typeof (FABookHistory.ytdReconciled)
    })
  {
  }

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    FABookHist faBookHist = (FABookHist) row;
    PXAccumulatorCollection accumulatorCollection1 = columns;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> closed = (ValueType) faBookHist.Closed;
    bool? nullable = faBookHist.Closed;
    int num1;
    if (!nullable.GetValueOrDefault())
    {
      nullable = faBookHist.Reopen;
      if (!nullable.GetValueOrDefault())
      {
        num1 = 4;
        goto label_6;
      }
    }
    num1 = 0;
label_6:
    accumulatorCollection1.Update<FABookHistory.closed>((object) closed, (PXDataFieldAssign.AssignBehavior) num1);
    PXAccumulatorCollection accumulatorCollection2 = columns;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> suspended = (ValueType) faBookHist.Suspended;
    nullable = faBookHist.Suspended;
    int num2 = nullable.GetValueOrDefault() ? 0 : 4;
    accumulatorCollection2.Update<FABookHistory.suspended>((object) suspended, (PXDataFieldAssign.AssignBehavior) num2);
    columns.Update<FABookHistory.ytdSuspended>((object) faBookHist.YtdSuspended, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<FABookHistory.ytdReversed>((object) faBookHist.YtdReversed, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<FABookHistory.createdByID>((object) faBookHist.CreatedByID, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<FABookHistory.createdDateTime>((object) faBookHist.CreatedDateTime, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<FABookHistory.createdByScreenID>((object) faBookHist.CreatedByScreenID, (PXDataFieldAssign.AssignBehavior) 4);
    columns.Update<FABookHistory.lastModifiedByID>((object) faBookHist.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
    columns.Update<FABookHistory.lastModifiedDateTime>((object) faBookHist.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
    columns.Update<FABookHistory.lastModifiedByScreenID>((object) faBookHist.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
    return true;
  }
}
