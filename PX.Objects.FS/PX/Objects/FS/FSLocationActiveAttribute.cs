// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLocationActiveAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FS;

public class FSLocationActiveAttribute(System.Type WhereType) : LocationActiveAttribute(WhereType, typeof (LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PX.Objects.CS.Country, On<PX.Objects.CS.Country.countryID, Equal<PX.Objects.CR.Address.countryID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>>>))
{
  protected override PXDimensionSelectorAttribute GetSelectorAttribute(System.Type searchType)
  {
    return new PXDimensionSelectorAttribute("LOCATION", searchType, typeof (PX.Objects.CR.Location.locationCD), new System.Type[9]
    {
      typeof (PX.Objects.CR.Location.locationCD),
      typeof (PX.Objects.CR.Location.descr),
      typeof (PX.Objects.CR.Address.addressLine1),
      typeof (PX.Objects.CR.Address.addressLine2),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.state),
      typeof (PX.Objects.CR.Address.postalCode),
      typeof (PX.Objects.CS.Country.description),
      typeof (PX.Objects.CR.Contact.phone1)
    });
  }
}
