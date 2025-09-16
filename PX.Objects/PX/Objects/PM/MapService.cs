// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.MapService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

public class MapService
{
  private readonly PXGraph graph;

  public MapService(PXGraph graph) => this.graph = graph;

  public virtual void viewAddressOnMap(IAddressLocation location)
  {
    MapRedirector currentMapRedirector = SitePolicy.CurrentMapRedirector;
    if (currentMapRedirector == null)
      return;
    Decimal? nullable = location.Latitude;
    if (!nullable.HasValue)
    {
      nullable = location.Longitude;
      if (!nullable.HasValue)
      {
        this.ShowLocationByAddress(currentMapRedirector, location, ((IAddressBase) location).AddressLine1);
        return;
      }
    }
    MapService.ShowLocationByCoordinates(location.Latitude, location.Longitude);
  }

  private void ShowLocationByAddress(
    MapRedirector mapRedirector,
    IAddressLocation location,
    string siteAddress)
  {
    string str = this.GetCountry(((IAddressBase) location).CountryID)?.Description ?? ((IAddressBase) location).CountryID;
    mapRedirector.ShowAddress(str, ((IAddressBase) location).State, ((IAddressBase) location).City, ((IAddressBase) location).PostalCode, siteAddress, (string) null, (string) null);
  }

  private Country GetCountry(string code)
  {
    return PXResultset<Country>.op_Implicit(PXSelectBase<Country, PXViewOf<Country>.BasedOn<SelectFromBase<Country, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Country.countryID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(this.graph, new object[1]
    {
      (object) code
    }));
  }

  private static void ShowLocationByCoordinates(Decimal? latitude, Decimal? longitude)
  {
    new MapService.GoogleMapLatLongRedirector().ShowAddressByLocation(latitude, longitude);
  }

  public class GoogleMapLatLongRedirector : GoogleMapRedirector
  {
    public void ShowAddressByLocation(Decimal? latitude, Decimal? longitude)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string empty5 = string.Empty;
      string empty6 = string.Empty;
      string empty7 = string.Empty;
      if (!latitude.HasValue)
        latitude = new Decimal?(0M);
      if (!longitude.HasValue)
        longitude = new Decimal?(0M);
      string str = $"{empty1}{Convert.ToString((object) latitude)},{Convert.ToString((object) longitude)}";
      ((MapRedirector) this).ShowAddress(empty2, empty3, empty4, empty5, empty6, empty7, str);
    }
  }
}
