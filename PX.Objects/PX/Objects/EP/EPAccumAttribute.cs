// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPAccumAttribute : PXAccumulatorAttribute
{
  public EPAccumAttribute()
    : base(new Type[2]
    {
      typeof (EPHistory.finYtdClaimed),
      typeof (EPHistory.tranYtdClaimed)
    }, new Type[2]
    {
      typeof (EPHistory.finYtdClaimed),
      typeof (EPHistory.tranYtdClaimed)
    })
  {
  }

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    EPHistory epHistory = (EPHistory) row;
    columns.RestrictPast<EPHistory.finPeriodID>((PXComp) 3, (object) (epHistory.FinPeriodID.Substring(0, 4) + "01"));
    columns.RestrictFuture<EPHistory.finPeriodID>((PXComp) 5, (object) (epHistory.FinPeriodID.Substring(0, 4) + "99"));
    return true;
  }
}
