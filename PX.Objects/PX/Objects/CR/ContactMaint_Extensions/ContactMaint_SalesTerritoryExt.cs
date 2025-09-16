// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ContactMaint_Extensions.ContactMaint_SalesTerritoryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.CR.ContactMaint_Extensions;

/// <exclude />
public class ContactMaint_SalesTerritoryExt : 
  SalesTerritoryExt<ContactMaint, Contact, Contact.defAddressID, Contact.overrideSalesTerritory, Contact.salesTerritoryID, Address, Address.addressID, Address.countryID, Address.state>
{
  public static bool IsActive()
  {
    return SalesTerritoryExt<ContactMaint, Contact, Contact.defAddressID, Contact.overrideSalesTerritory, Contact.salesTerritoryID, Address, Address.addressID, Address.countryID, Address.state>.IsExtensionActive();
  }

  protected override IAddressBase CurrentAddress
  {
    get
    {
      return (IAddressBase) ((PXSelectBase<Address>) this.Base.AddressCurrent).SelectSingle(Array.Empty<object>());
    }
  }

  protected override void AssignDefaultSalesTerritory(IAddressBase address)
  {
    base.AssignDefaultSalesTerritory(address);
    if (!(address is Address address1))
      return;
    int? addressId = address1.AddressID;
    int num = 0;
    if (!(addressId.GetValueOrDefault() > num & addressId.HasValue))
      return;
    Contact contact1 = ((PXSelectBase<Contact>) this.Base.Contact).Current;
    if (contact1 == null)
      contact1 = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.defAddressID, Equal<P.AsInt>, And<Contact.contactType, Equal<ContactTypesAttribute.person>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) address1.AddressID
      }));
    Contact contact2 = contact1;
    if (contact2 == null)
      return;
    string salesTerritory = this.GetSalesTerritory(address);
    this.UpdateRelatedContacts(contact2.ContactID, address1.AddressID, salesTerritory);
    this.UpdateRelatedBAccount(address1.AddressID, salesTerritory);
    foreach (PXResult<Address> pxResult in ((PXSelectBase<Address>) ((PXGraph) this.Base).GetExtension<ContactMaint.UpdateRelatedContactInfoFromContactGraphExt>().ContactRelatedAddresses).Select(new object[1]
    {
      (object) contact2.ContactID
    }))
    {
      Address address2 = PXResult<Address>.op_Implicit(pxResult);
      this.UpdateRelatedContacts(contact2.ContactID, address2.AddressID, salesTerritory);
      this.UpdateRelatedBAccount(address2.AddressID, salesTerritory);
    }
  }
}
