// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.OpenLineCalc`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

[Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
public sealed class OpenLineCalc<Field> : IBqlAggregateCalculator where Field : IBqlField
{
  public object Calculate(PXCache cache, object row, object oldrow, int fieldordinal, int digit)
  {
    if (row == oldrow)
      return (object) null;
    bool? nullable1 = (bool?) cache.GetValue<SOLine.openLine>(row);
    bool? nullable2 = (bool?) cache.GetValue<SOLine.openLine>(oldrow);
    bool? nullable3 = (bool?) cache.GetValue<SOLine.isFree>(row);
    bool? nullable4 = (bool?) cache.GetValue<SOLine.isFree>(oldrow);
    bool? nullable5 = (bool?) cache.GetValue<SOLine.manualDisc>(row);
    bool? nullable6 = (bool?) cache.GetValue<SOLine.manualDisc>(oldrow);
    bool? nullable7;
    if (row != null && nullable1.GetValueOrDefault() && (!nullable3.GetValueOrDefault() || nullable5.GetValueOrDefault()))
    {
      if (oldrow != null && nullable2.GetValueOrDefault())
      {
        if (nullable4.GetValueOrDefault())
        {
          nullable7 = nullable6;
          bool flag = false;
          if (!(nullable7.GetValueOrDefault() == flag & nullable7.HasValue))
            goto label_7;
        }
        else
          goto label_7;
      }
      return (object) 1;
    }
label_7:
    if (oldrow != null && nullable2.GetValueOrDefault() && (!nullable4.GetValueOrDefault() || nullable6.GetValueOrDefault()))
    {
      if (row != null && nullable1.GetValueOrDefault())
      {
        if (nullable3.GetValueOrDefault())
        {
          nullable7 = nullable5;
          bool flag = false;
          if (!(nullable7.GetValueOrDefault() == flag & nullable7.HasValue))
            goto label_12;
        }
        else
          goto label_12;
      }
      return (object) -1;
    }
label_12:
    return (object) 0;
  }

  public object Calculate(
    PXCache cache,
    object row,
    int fieldordinal,
    object[] records,
    int digit)
  {
    return (object) (short) ((IEnumerable<object>) records).Count<object>((Func<object, bool>) (r =>
    {
      if (!((bool?) cache.GetValue<SOLine.openLine>(r)).GetValueOrDefault())
        return false;
      return !((bool?) cache.GetValue<SOLine.isFree>(r)).GetValueOrDefault() || ((bool?) cache.GetValue<SOLine.manualDisc>(r)).GetValueOrDefault();
    }));
  }
}
