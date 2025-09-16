// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranSplitCostComparer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class INTranSplitCostComparer : IEqualityComparer<INTranSplit>
{
  public bool Equals(INTranSplit x, INTranSplit y)
  {
    if (x.DocType == y.DocType && x.RefNbr == y.RefNbr)
    {
      int? nullable1 = x.LineNbr;
      int? lineNbr = y.LineNbr;
      if (nullable1.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable1.HasValue == lineNbr.HasValue)
      {
        int? nullable2 = x.CostSiteID;
        nullable1 = y.CostSiteID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = x.CostSubItemID;
          nullable2 = y.CostSubItemID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            return x.ValMethod != "S" || string.Equals(x.LotSerialNbr, y.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
        }
      }
    }
    return false;
  }

  public int GetHashCode(INTranSplit obj)
  {
    return ((((17 * 23 + obj.DocType.GetHashCode()) * 23 + obj.RefNbr.GetHashCode()) * 23 + obj.LineNbr.GetHashCode()) * 23 + obj.CostSiteID.GetHashCode()) * 23 + obj.CostSubItemID.GetHashCode();
  }
}
