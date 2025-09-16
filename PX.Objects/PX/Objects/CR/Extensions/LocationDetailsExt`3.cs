// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.LocationDetailsExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <summary>Represents the Locations grid</summary>
public abstract class LocationDetailsExt<TGraph, TMaster, TBAccountID> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TMaster : PX.Objects.CR.BAccount, IBqlTable, new()
  where TBAccountID : class, IBqlField
{
  [PXViewName("Locations")]
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (PX.Objects.CR.BAccount))]
  public PXSelectJoin<PX.Objects.CR.Standalone.Location, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Standalone.Location.defAddressID>>>, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<TBAccountID>>>> Locations;
  public PXAction<TMaster> RefreshLocation;
  public PXAction<TMaster> NewLocation;
  public PXAction<TMaster> ViewLocation;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.Locations).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.accountLocations>();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void refreshLocation()
  {
    this.Base.SelectTimeStamp();
    ((PXCache) GraphHelper.Caches<PX.Objects.CR.Location>((PXGraph) this.Base)).ClearQueryCache();
    ((PXCache) GraphHelper.Caches<TMaster>((PXGraph) this.Base)).Clear();
  }

  [PXUIField(DisplayName = "Add Location")]
  [PXButton(ImageSet = "main", ImageKey = "AddNew", PopupCommand = "RefreshLocation", Tooltip = "Add New Location", DisplayOnMainToolbar = false)]
  public virtual void newLocation()
  {
    if (!(this.Base.Caches[typeof (TMaster)].Current is TMaster current) || !current.BAccountID.HasValue)
      return;
    LocationMaint locationsGraph = this.GetLocationsGraph(current);
    PX.Objects.CR.Standalone.Location location = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.Locations).SelectSingle(Array.Empty<object>());
    PX.Objects.CR.Location instance = (PX.Objects.CR.Location) ((PXSelectBase) locationsGraph.Location).Cache.CreateInstance();
    instance.BAccountID = current.BAccountID;
    string str;
    switch (current.Type)
    {
      case "VE":
        str = "VE";
        break;
      case "CU":
      case "EC":
        str = "CU";
        break;
      case "VC":
        str = "VC";
        break;
      default:
        str = location.LocType;
        break;
    }
    instance.LocType = str;
    ((PXSelectBase<PX.Objects.CR.Location>) locationsGraph.Location).Insert(instance);
    ((PXSelectBase) locationsGraph.Location).Cache.IsDirty = false;
    PXRedirectHelper.TryRedirect((PXGraph) locationsGraph, (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry", PopupCommand = "RefreshLocation", DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewLocation(PXAdapter adapter)
  {
    TMaster current1 = this.Base.Caches[typeof (TMaster)].Current as TMaster;
    if (((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.Locations).Current != null && (object) current1 != null && this.Base.Caches[typeof (TMaster)].GetStatus((object) current1) != 2)
    {
      PX.Objects.CR.Standalone.Location current2 = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.Locations).Current;
      LocationMaint locationsGraph = this.GetLocationsGraph(current1);
      ((PXSelectBase<PX.Objects.CR.Location>) locationsGraph.Location).Current = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) locationsGraph.Location).Search<PX.Objects.CR.Standalone.Location.locationID>((object) current2.LocationID, new object[1]
      {
        (object) current1.AcctCD
      }));
      PXRedirectHelper.TryRedirect((PXGraph) locationsGraph, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location> e)
  {
    PX.Objects.CR.Standalone.Location row = e.Row;
    if (row == null)
      return;
    bool flag = row.LocType == "CU" || row.LocType == "VC";
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Standalone.Location.cTaxZoneID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location>>) e).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location>>) e).Cache, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Standalone.Location.cBranchID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location>>) e).Cache, (object) null, flag);
  }

  protected virtual void _(PX.Data.Events.RowSelected<TMaster> e)
  {
    TMaster row = e.Row;
    if ((object) row == null)
      return;
    bool flag1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TMaster>>) e).Cache.GetStatus((object) row) != 2;
    bool flag2 = row.Type == "CU" || row.Type == "VC";
    bool flag3 = (row.Type == "CU" ? 1 : (row.Type == "PR" ? 1 : 0)) != 0 || row.Type == "VC";
    ((PXAction) this.NewLocation).SetEnabled(flag1);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Locations).Cache, (object) null, typeof (PX.Objects.CR.Standalone.Location.cPriceClassID).Name, flag3);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Locations).Cache, (object) null, typeof (PX.Objects.CR.Standalone.Location.cSalesAcctID).Name, flag2);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Locations).Cache, (object) null, typeof (PX.Objects.CR.Standalone.Location.cSalesSubID).Name, flag2);
  }

  public virtual LocationMaint GetLocationsGraph(TMaster master)
  {
    LocationMaint locationsGraph;
    switch (master.Type)
    {
      case "VE":
        locationsGraph = (LocationMaint) PXGraph.CreateInstance<VendorLocationMaint>();
        break;
      case "CU":
        locationsGraph = (LocationMaint) PXGraph.CreateInstance<CustomerLocationMaint>();
        break;
      default:
        locationsGraph = !typeof (Customer).IsAssignableFrom(this.Base.PrimaryItemType) ? (!typeof (PX.Objects.AP.Vendor).IsAssignableFrom(this.Base.PrimaryItemType) ? (LocationMaint) PXGraph.CreateInstance<AccountLocationMaint>() : (LocationMaint) PXGraph.CreateInstance<VendorLocationMaint>()) : (LocationMaint) PXGraph.CreateInstance<CustomerLocationMaint>();
        break;
    }
    return locationsGraph;
  }
}
