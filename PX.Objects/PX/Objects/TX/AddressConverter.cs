// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.AddressConverter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.TaxProvider;

#nullable disable
namespace PX.Objects.TX;

public static class AddressConverter
{
  public static TaxAddress ConvertTaxAddress(IAddressLocation address)
  {
    return new TaxAddress()
    {
      Country = ((IAddressBase) address).CountryID,
      Region = ((IAddressBase) address).State,
      City = ((IAddressBase) address).City,
      PostalCode = ((IAddressBase) address).PostalCode,
      AddressLine1 = ((IAddressBase) address).AddressLine1,
      AddressLine2 = ((IAddressBase) address).AddressLine2,
      AddressLine3 = ((IAddressBase) address).AddressLine3,
      Latitude = address.Latitude,
      Longitude = address.Longitude
    };
  }
}
