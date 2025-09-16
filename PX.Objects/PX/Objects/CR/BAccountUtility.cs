// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountUtility
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR;

public static class BAccountUtility
{
  public static BAccount FindAccount(PXGraph graph, int? aBAccountID)
  {
    BAccount account = (BAccount) null;
    if (aBAccountID.HasValue)
      account = (BAccount) ((PXSelectBase) new PXSelectReadonly<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>(graph)).View.SelectSingle(new object[1]
      {
        (object) aBAccountID
      });
    return account;
  }

  public static void ViewOnMap(Address aAddr)
  {
    MapRedirector currentMapRedirector = SitePolicy.CurrentMapRedirector;
    if (currentMapRedirector == null || aAddr == null)
      return;
    PX.Objects.CS.Country country = PXSelectorAttribute.Select<Address.countryID>(new PXGraph().Caches[typeof (Address)], (object) aAddr) as PX.Objects.CS.Country;
    currentMapRedirector.ShowAddress(country != null ? country.Description : aAddr.CountryID, aAddr.State, aAddr.City, aAddr.PostalCode, aAddr.AddressLine1, aAddr.AddressLine2, aAddr.AddressLine3);
  }

  public static void ViewOnMap(CRAddress aAddr)
  {
    MapRedirector currentMapRedirector = SitePolicy.CurrentMapRedirector;
    if (currentMapRedirector == null || aAddr == null)
      return;
    PX.Objects.CS.Country country = PXSelectorAttribute.Select<CRAddress.countryID>(new PXGraph().Caches[typeof (CRAddress)], (object) aAddr) as PX.Objects.CS.Country;
    currentMapRedirector.ShowAddress(country != null ? country.Description : aAddr.CountryID, aAddr.State, aAddr.City, aAddr.PostalCode, aAddr.AddressLine1, aAddr.AddressLine2, (string) null);
  }

  public static void ViewOnMap<TAddress, FCountryID>(IAddress aAddr)
    where TAddress : class, IBqlTable, IAddress, new()
    where FCountryID : IBqlField
  {
    MapRedirector currentMapRedirector = SitePolicy.CurrentMapRedirector;
    if (currentMapRedirector == null || aAddr == null)
      return;
    PX.Objects.CS.Country country = PXSelectorAttribute.Select<FCountryID>(new PXGraph().Caches[typeof (TAddress)], (object) aAddr) as PX.Objects.CS.Country;
    currentMapRedirector.ShowAddress(country != null ? country.Description : ((IAddressBase) aAddr).CountryID, ((IAddressBase) aAddr).State, ((IAddressBase) aAddr).City, ((IAddressBase) aAddr).PostalCode, ((IAddressBase) aAddr).AddressLine1, ((IAddressBase) aAddr).AddressLine2, (string) null);
  }
}
