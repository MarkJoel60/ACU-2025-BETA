// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.OffsetBookDateToPeriod`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class OffsetBookDateToPeriod<BookDate, BookID, AssetID, DepreciationMethodID, AveragingConvention, UsefulLife> : 
  OffsetBookDate<BookDate, BookID, AssetID, DepreciationMethodID, AveragingConvention, UsefulLife>,
  IBqlOperand
  where BookDate : IBqlOperand
  where BookID : IBqlOperand
  where AssetID : IBqlOperand
  where DepreciationMethodID : IBqlOperand
  where AveragingConvention : IBqlOperand
  where UsefulLife : IBqlOperand
{
  public override object Evaluate(PXCache cache, object item, Dictionary<Type, object> pars)
  {
    int? par1 = (int?) pars[typeof (BookID)];
    int? par2 = (int?) pars[typeof (AssetID)];
    if (!par1.HasValue || !par2.HasValue)
      return (object) null;
    object obj = base.Evaluate(cache, item, pars);
    if (obj == null)
      return (object) null;
    DateTime? date = (DateTime?) obj;
    string str = cache.Graph.GetService<IFABookPeriodRepository>().GetFABookPeriodIDOfDate(date, par1, par2, false);
    FABookBalance faBookBalance = item as FABookBalance;
    if (!string.IsNullOrEmpty(str) && faBookBalance != null && faBookBalance.YtdSuspended.HasValue)
      str = cache.Graph.GetService<IFABookPeriodUtils>().PeriodPlusPeriodsCount(str, faBookBalance.YtdSuspended.Value, par1, par2);
    return (object) PeriodIDAttribute.FormatForDisplay(str);
  }
}
