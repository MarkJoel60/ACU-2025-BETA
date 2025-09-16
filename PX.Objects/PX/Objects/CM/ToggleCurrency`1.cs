// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.ToggleCurrency`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CM;

public class ToggleCurrency<TNode>(PXGraph graph, string name) : PXAction<TNode>(graph, name) where TNode : class, IBqlTable, new()
{
  [PXUIField]
  [PXButton(ImageKey = "Money", Tooltip = "Toggle Currency View", DisplayOnMainToolbar = false, CommitChanges = false)]
  protected virtual IEnumerable Handler(PXAdapter adapter)
  {
    ToggleCurrency<TNode> toggleCurrency = this;
    ((PXAction) toggleCurrency)._Graph.Accessinfo.CuryViewState = !((PXAction) toggleCurrency)._Graph.Accessinfo.CuryViewState;
    PXCache cache = adapter.View.Cache;
    bool anyDiff = !cache.IsDirty;
    foreach (object obj in adapter.Get())
    {
      if (!anyDiff)
      {
        TNode node = !(obj is PXResult) ? (TNode) obj : (TNode) ((PXResult) obj)[0];
        if ((object) node == null)
        {
          anyDiff = true;
        }
        else
        {
          TNode oldRow = CurrencyInfoAttribute.GetOldRow(cache, (object) node) as TNode;
          if ((object) node == null || (object) oldRow == null)
          {
            anyDiff = true;
          }
          else
          {
            foreach (string str in ((IEnumerable<string>) cache.Fields).Where<string>(new Func<string, bool>(PX.Objects.CM.Extensions.CurrencyInfoAttribute.IsDifferenceEssential)))
            {
              object objA = cache.GetValue((object) oldRow, str);
              object objB = cache.GetValue((object) node, str);
              if ((objA != null || objB != null) && !object.Equals(objA, objB) && (!(objA is DateTime) || !(objB is DateTime dateTime) || ((DateTime) objA).Date != dateTime.Date))
                anyDiff = true;
            }
          }
        }
      }
      yield return obj;
    }
    if (!anyDiff)
      cache.IsDirty = false;
  }
}
