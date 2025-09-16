// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaint_Extensions.OpportunityMaint_SalesTerritoryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CR.Extensions;
using System;

#nullable disable
namespace PX.Objects.CR.OpportunityMaint_Extensions;

/// <exclude />
public class OpportunityMaint_SalesTerritoryExt : 
  SalesTerritoryExt<OpportunityMaint, CROpportunity, CROpportunity.opportunityAddressID, CROpportunity.overrideSalesTerritory, CROpportunity.salesTerritoryID, CRAddress, CRAddress.addressID, CRAddress.countryID, CRAddress.state>
{
  public static bool IsActive()
  {
    return SalesTerritoryExt<OpportunityMaint, CROpportunity, CROpportunity.opportunityAddressID, CROpportunity.overrideSalesTerritory, CROpportunity.salesTerritoryID, CRAddress, CRAddress.addressID, CRAddress.countryID, CRAddress.state>.IsExtensionActive();
  }

  protected override IAddressBase CurrentAddress
  {
    get
    {
      return (IAddressBase) ((PXSelectBase<CRAddress>) this.Base.Opportunity_Address).SelectSingle(Array.Empty<object>());
    }
  }
}
