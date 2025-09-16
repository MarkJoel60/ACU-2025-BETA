// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction.CostSiteIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction;

public class CostSiteIDAttribute : PXForeignSelectorAttribute
{
  public CostSiteIDAttribute()
    : base(typeof (INTran.locationID))
  {
  }

  protected override object GetValueExt(PXCache cache, object row)
  {
    int? nullable1 = (int?) cache.GetValue(row, this._FieldOrdinal);
    string valueExt = string.Empty;
    INLocation inLocation = INLocation.PK.Find(cache.Graph, nullable1);
    int? nullable2;
    int? nullable3;
    INSite inSite;
    if (inLocation == null)
    {
      inLocation = PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXSelectReadonly<INLocation, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[1]
      {
        (object) nullable1
      }));
      if (inLocation == null)
      {
        nullable2 = PXResultset<INSetup>.op_Implicit(PXSelectBase<INSetup, PXSelectReadonly<INSetup>.Config>.SelectWindowed(cache.Graph, 0, 1, Array.Empty<object>())).TransitSiteID;
        nullable3 = nullable1;
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
          return (object) INSite.PK.Find(cache.Graph, nullable1).SiteCD;
        INCostCenter inCostCenter = INCostCenter.PK.Find(cache.Graph, nullable1);
        if (inCostCenter == null)
        {
          IBqlTable ibqlTable = (IBqlTable) row;
          object[] objArray = new object[2]
          {
            (object) "IN Location",
            null
          };
          nullable3 = nullable1;
          objArray[1] = (object) ("siteID = " + nullable3.ToString());
          string str = PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system.", objArray);
          throw new PXSetPropertyException(ibqlTable, str);
        }
        inSite = INSite.PK.Find(cache.Graph, inCostCenter.SiteID);
        if (inSite == null)
          throw new RowNotFoundException(cache.Graph.Caches[typeof (INSite)], new object[1]
          {
            (object) inCostCenter.SiteID
          });
        valueExt = inCostCenter.CostCenterCD + " ";
      }
      else
        inSite = INSite.PK.Find(cache.Graph, inLocation.SiteID);
    }
    else
      inSite = INSite.PK.Find(cache.Graph, inLocation.SiteID);
    object siteCd = (object) inSite.SiteCD;
    ((PXCache) GraphHelper.Caches<INSite>(cache.Graph)).RaiseFieldSelecting<INSite.siteCD>((object) inLocation, ref siteCd, true);
    if (siteCd is PXStringState pxStringState1 && !string.IsNullOrEmpty(pxStringState1.InputMask))
      valueExt += Mask.Format(pxStringState1.InputMask, (string) ((PXFieldState) pxStringState1).Value);
    else if (siteCd is PXFieldState pxFieldState1 && pxFieldState1.Value is string)
      valueExt += (string) pxFieldState1.Value;
    int num;
    if (inLocation == null)
    {
      num = !nullable1.HasValue ? 1 : 0;
    }
    else
    {
      nullable3 = inLocation.LocationID;
      nullable2 = nullable1;
      num = nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      object locationCd = (object) inLocation.LocationCD;
      ((PXCache) GraphHelper.Caches<INLocation>(cache.Graph)).RaiseFieldSelecting<INLocation.locationCD>((object) inLocation, ref locationCd, true);
      switch (locationCd)
      {
        case PXStringState pxStringState2 when !string.IsNullOrEmpty(pxStringState2.InputMask):
          valueExt = valueExt + " " + Mask.Format(pxStringState2.InputMask, (string) ((PXFieldState) pxStringState2).Value);
          break;
        case PXFieldState pxFieldState2 when pxFieldState2.Value is string:
          valueExt = valueExt + " " + (string) pxFieldState2.Value;
          break;
      }
    }
    return (object) valueExt;
  }
}
