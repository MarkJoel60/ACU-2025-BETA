// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaint_Extensions.QuoteMaint_CRConvertBAccountToCustomerExt
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
public class QuoteMaint_CRConvertBAccountToCustomerExt : 
  CRConvertBAccountToCustomerExt<QuoteMaint, CRQuote>
{
  public override void Initialize()
  {
    base.Initialize();
    this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.Quote_Address);
    this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.Quote_Contact);
  }

  protected override CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentMapping GetDocumentMapping()
  {
    return new CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentMapping(typeof (CRQuote))
    {
      RefContactID = typeof (CRQuote.contactID)
    };
  }

  protected override CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentContactMapping GetDocumentContactMapping()
  {
    return new CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentContactMapping(typeof (CRContact));
  }

  protected override CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentAddressMapping GetDocumentAddressMapping()
  {
    return new CRCreateActionBaseInit<QuoteMaint, CRQuote>.DocumentAddressMapping(typeof (CRAddress));
  }
}
