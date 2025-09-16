// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BusinessAccountMaint_Extensions.BusinessAccountMaint_SalesTerritoryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.CR.BusinessAccountMaint_Extensions;

/// <exclude />
public class BusinessAccountMaint_SalesTerritoryExt : 
  SalesTerritoryExt<BusinessAccountMaint, BAccount, BAccount.defAddressID, BAccount.overrideSalesTerritory, BAccount.salesTerritoryID, Address, Address.addressID, Address.countryID, Address.state>
{
  public static bool IsActive()
  {
    return SalesTerritoryExt<BusinessAccountMaint, BAccount, BAccount.defAddressID, BAccount.overrideSalesTerritory, BAccount.salesTerritoryID, Address, Address.addressID, Address.countryID, Address.state>.IsExtensionActive();
  }

  protected override IAddressBase CurrentAddress
  {
    get
    {
      BusinessAccountMaint.DefContactAddressExt extension = ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.DefContactAddressExt>();
      if (extension == null)
        return (IAddressBase) null;
      PXSelect<Address, Where<Address.bAccountID, Equal<Current<BAccount.bAccountID>>, And<Address.addressID, Equal<Current<BAccount.defAddressID>>>>> defAddress = extension.DefAddress;
      return defAddress == null ? (IAddressBase) null : (IAddressBase) ((PXSelectBase<Address>) defAddress).SelectSingle(Array.Empty<object>());
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
    BAccount baccount = ((PXSelectBase<BAccount>) this.Base.CurrentBAccount).SelectSingle(Array.Empty<object>());
    if (baccount == null)
      return;
    addressId = address1.AddressID;
    int? defAddressId = baccount.DefAddressID;
    if (!(addressId.GetValueOrDefault() == defAddressId.GetValueOrDefault() & addressId.HasValue == defAddressId.HasValue))
      return;
    string salesTerritory = this.GetSalesTerritory(address);
    this.UpdateRelatedContacts(baccount.DefContactID, address1.AddressID, salesTerritory);
    foreach (PXResult<Address> pxResult in ((PXSelectBase<Address>) ((PXGraph) this.Base).GetExtension<BusinessAccountMaint.UpdateRelatedContactInfoFromAccountGraphExt>().BAccountRelatedAddresses).Select(new object[1]
    {
      (object) baccount.BAccountID
    }))
    {
      Address address2 = PXResult<Address>.op_Implicit(pxResult);
      this.UpdateRelatedContacts(baccount.DefContactID, address2.AddressID, salesTerritory);
    }
  }
}
