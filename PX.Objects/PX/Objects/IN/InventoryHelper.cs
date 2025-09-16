// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Description;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class InventoryHelper
{
  public static void CheckZeroDefaultTerm<DeferralCode, DefaultTerm>(PXCache sender, object row)
    where DeferralCode : IBqlField
    where DefaultTerm : IBqlField
  {
    string str = sender.GetValue<DeferralCode>(row) as string;
    Decimal? nullable1 = sender.GetValue<DefaultTerm>(row) as Decimal?;
    bool flag = false;
    if (str != null)
    {
      DRDeferredCode code = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) str
      }));
      if (code != null && DeferredMethodType.RequiresTerms(code) && nullable1.HasValue)
      {
        Decimal? nullable2 = nullable1;
        Decimal num = 0M;
        if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
          flag = true;
      }
    }
    sender.RaiseExceptionHandling(typeof (DefaultTerm).Name, row, (object) nullable1, flag ? (Exception) new PXSetPropertyException("For items with no Default Term, the system cannot calculate Term End Date.", (PXErrorLevel) 2) : (Exception) null);
  }

  public static List<CacheEntityItem> TemplateEntity(
    PXGraph graph,
    string parent,
    PXSiteMap.ScreenInfo _info)
  {
    List<CacheEntityItem> cacheEntityItemList = new List<CacheEntityItem>();
    if (parent == null)
    {
      int num = 0;
      if (_info.GraphName != null)
      {
        foreach (PXViewDescription pxViewDescription in _info.Containers.Values)
          cacheEntityItemList.Add(new CacheEntityItem()
          {
            Key = pxViewDescription.ViewName,
            SubKey = pxViewDescription.ViewName,
            Path = (string) null,
            Name = pxViewDescription.DisplayName,
            Number = new int?(num++),
            Icon = Sprite.Main.GetFullUrl("Box")
          });
      }
    }
    else
    {
      string[] strArray = (string[]) null;
      _info.Views.TryGetValue(parent, out strArray);
      if (strArray != null)
      {
        int num = 0;
        PXGraph instance = PXGraph.CreateInstance(GraphHelper.GetType(_info.GraphName));
        if (!((Dictionary<string, PXView>) instance.Views).ContainsKey(parent))
          return (List<CacheEntityItem>) null;
        foreach (PXFieldState field in PXFieldState.GetFields(graph, instance.Views[parent].BqlSelect.GetTables(), false))
          cacheEntityItemList.Add(new CacheEntityItem()
          {
            Key = $"{parent}.{field.Name}",
            SubKey = field.Name,
            Path = $"(({(string.IsNullOrEmpty(parent) ? field.Name : $"{parent}.{field.Name}")}))",
            Name = field.DisplayName,
            Number = new int?(num++),
            Icon = Sprite.Main.GetFullUrl("BoxIn")
          });
      }
    }
    return cacheEntityItemList;
  }

  public static bool CanCreateStockItem(PXGraph graph)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() && GraphHelper.RowCast<INSetup>((IEnumerable) PXSelectBase<INSetup, PXSelect<INSetup>.Config>.Select(graph, Array.Empty<object>())).Any<INSetup>();
  }
}
