// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.PivotMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Olap;
using PX.Olap.Maintenance;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.SM;

public class PivotMaintExt : PXGraphExtension<PivotMaint>
{
  public static bool TryDetermineSortType(
    DataScreenBase dataScreen,
    string field,
    out SortType sortType)
  {
    if (PivotMaintExt.IsFinPeriod(dataScreen, field))
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref sortType = 1;
      return true;
    }
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ref sortType = 0;
    return false;
  }

  public static bool IsFinPeriod(DataScreenBase dataScreen, string field)
  {
    return !string.IsNullOrEmpty(field) && (dataScreen != null ? dataScreen.View.Cache.GetAttributesReadonly(field, true).OfType<FinPeriodIDFormattingAttribute>().FirstOrDefault<FinPeriodIDFormattingAttribute>() : (FinPeriodIDFormattingAttribute) null) != null;
  }

  [PXOverride]
  public virtual SortType DetermineSortType(string field, Func<string, SortType> del)
  {
    SortType sortType;
    return PivotMaintExt.TryDetermineSortType(this.Base.DataScreen, field, out sortType) ? sortType : del(field);
  }
}
