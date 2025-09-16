// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaint_Extensions.OpportunityMaint_CRConvertBAccountToCustomerExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions.CRConvertLinkedEntityActions;
using PX.Objects.CR.Extensions.CRCreateActions;

#nullable disable
namespace PX.Objects.CR.OpportunityMaint_Extensions;

/// <inheritdoc />
public class OpportunityMaint_CRConvertBAccountToCustomerExt : 
  CRConvertBAccountToCustomerExt<OpportunityMaint, CROpportunity>
{
  public override void Initialize()
  {
    base.Initialize();
    this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.Opportunity_Address);
    this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.Opportunity_Contact);
  }

  protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
  {
    return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
    {
      RefContactID = typeof (CROpportunity.contactID)
    };
  }

  protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentContactMapping GetDocumentContactMapping()
  {
    return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentContactMapping(typeof (CRContact));
  }

  protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentAddressMapping GetDocumentAddressMapping()
  {
    return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentAddressMapping(typeof (CRAddress));
  }
}
