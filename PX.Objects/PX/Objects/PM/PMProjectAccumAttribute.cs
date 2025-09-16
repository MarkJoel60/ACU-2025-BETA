// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class PMProjectAccumAttribute : PXAccumulatorAttribute
{
  public PMProjectAccumAttribute()
    : base(new Type[6]
    {
      typeof (PMHistory.finYTDQty),
      typeof (PMHistory.tranYTDQty),
      typeof (PMHistory.finYTDCuryAmount),
      typeof (PMHistory.finYTDAmount),
      typeof (PMHistory.tranYTDCuryAmount),
      typeof (PMHistory.tranYTDAmount)
    }, new Type[6]
    {
      typeof (PMHistory.finYTDQty),
      typeof (PMHistory.tranYTDQty),
      typeof (PMHistory.finYTDCuryAmount),
      typeof (PMHistory.finYTDAmount),
      typeof (PMHistory.tranYTDCuryAmount),
      typeof (PMHistory.tranYTDAmount)
    })
  {
  }

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    PMHistory pmHistory = (PMHistory) row;
    columns.RestrictPast<PMHistory.periodID>((PXComp) 3, (object) (pmHistory.PeriodID.Substring(0, 4) + "01"));
    columns.RestrictFuture<PMHistory.periodID>((PXComp) 5, (object) (pmHistory.PeriodID.Substring(0, 4) + "99"));
    return true;
  }
}
