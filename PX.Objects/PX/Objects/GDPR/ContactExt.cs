// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.ContactExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<Contact, Where<Contact.contactID, Equal<Current<Contact.contactID>>>>))]
[PXPersonalDataTable(typeof (Select2<Contact, InnerJoin<PX.Objects.CR.Location, On<Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<BAccount.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<BAccount.defLocationID>>>>>))]
[PXPersonalDataTable(typeof (Select2<Contact, InnerJoin<PX.Objects.CR.Standalone.CRLead, On<PX.Objects.CR.Standalone.CRLead.contactID, Equal<Contact.contactID>>>, Where<PX.Objects.CR.Standalone.CRLead.refContactID, Equal<Current<Contact.contactID>>>>))]
[PXPersonalDataTable(typeof (Select<Contact, Where<Contact.contactType, Equal<ContactTypesAttribute.lead>, And<Contact.bAccountID, Equal<Current<BAccount.bAccountID>>>>>))]
[Serializable]
public sealed class ContactExt : PXCacheExtension<
#nullable disable
Contact>, IPseudonymizable, IPostPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public List<PXDataFieldParam> InterruptPseudonimyzationHandler(List<PXDataFieldParam> restricts)
  {
    return new List<PXDataFieldParam>()
    {
      (PXDataFieldParam) new PXDataFieldAssign<Contact.noCall>((object) true),
      (PXDataFieldParam) new PXDataFieldAssign<Contact.noEMail>((object) true),
      (PXDataFieldParam) new PXDataFieldAssign<Contact.noFax>((object) true),
      (PXDataFieldParam) new PXDataFieldAssign<Contact.noMail>((object) true),
      (PXDataFieldParam) new PXDataFieldAssign<Contact.noMarketing>((object) true),
      (PXDataFieldParam) new PXDataFieldAssign<Contact.noMassMail>((object) true),
      (PXDataFieldParam) new PXDataFieldAssign<Contact.isActive>((object) false),
      (PXDataFieldParam) new PXDataFieldAssign<Contact.status>((object) "I")
    };
  }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContactExt.pseudonymizationStatus>
  {
  }
}
