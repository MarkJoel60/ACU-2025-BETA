// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadMaint_Extensions.LeadMaint_SalesTerritoryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CR.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.LeadMaint_Extensions;

/// <exclude />
public class LeadMaint_SalesTerritoryExt : 
  SalesTerritoryExt<LeadMaint, CRLead, CRLead.defAddressID, CRLead.overrideSalesTerritory, Contact.salesTerritoryID, Address, Address.addressID, Address.countryID, Address.state>
{
  public static bool IsActive()
  {
    return SalesTerritoryExt<LeadMaint, CRLead, CRLead.defAddressID, CRLead.overrideSalesTerritory, Contact.salesTerritoryID, Address, Address.addressID, Address.countryID, Address.state>.IsExtensionActive();
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
    CRLead crLead = ((PXSelectBase<CRLead>) this.Base.LeadCurrent).SelectSingle(Array.Empty<object>());
    string salesTerritory = this.GetSalesTerritory(address);
    if (address is Address address1)
    {
      int? addressId = address1.AddressID;
      int num = 0;
      if (addressId.GetValueOrDefault() > num & addressId.HasValue)
      {
        this.UpdateRelatedContacts(crLead.ContactID, address1.AddressID, salesTerritory);
        this.UpdateRelatedBAccount(address1.AddressID, salesTerritory);
      }
    }
    int? nullable;
    if (crLead != null)
    {
      nullable = crLead.RefContactID;
      if (nullable.HasValue)
      {
        using (IEnumerator<PXResult<Address>> enumerator = ((PXSelectBase<Address>) ((PXGraph) this.Base).GetExtension<LeadMaint.UpdateRelatedContactInfoFromLeadGraphExt>().RefContactRelatedAddresses).Select(new object[3]
        {
          (object) crLead.RefContactID,
          (object) crLead.RefContactID,
          (object) crLead.RefContactID
        }).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Address address2 = PXResult<Address>.op_Implicit(enumerator.Current);
            this.UpdateRelatedContacts(crLead.ContactID, address2.AddressID, salesTerritory);
            this.UpdateRelatedBAccount(address2.AddressID, salesTerritory);
          }
          return;
        }
      }
    }
    if (crLead == null)
      return;
    nullable = crLead.BAccountID;
    if (!nullable.HasValue)
      return;
    foreach (PXResult<Address> pxResult in ((PXSelectBase<Address>) ((PXGraph) this.Base).GetExtension<LeadMaint.UpdateRelatedContactInfoFromLeadGraphExt>().BAccountRelatedAddresses).Select(new object[2]
    {
      (object) crLead.BAccountID,
      (object) crLead.BAccountID
    }))
    {
      Address address3 = PXResult<Address>.op_Implicit(pxResult);
      this.UpdateRelatedContacts(crLead.ContactID, address3.AddressID, salesTerritory);
      this.UpdateRelatedBAccount(address3.AddressID, salesTerritory);
    }
  }
}
