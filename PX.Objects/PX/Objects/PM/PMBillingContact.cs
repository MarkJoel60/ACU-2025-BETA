// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a contact that is specified in the <see cref="T:PX.Objects.PM.PMProject">project</see>.
/// A <see cref="T:PX.Objects.PM.PMContact" /> record is initially populated with the customer location's
/// <see cref="T:PX.Objects.CR.Contact" /> and can be used to override the contact at the document level.
/// This record reflects changes made to the original <see cref="T:PX.Objects.CR.Contact" /> record.
/// The entities of this type are created and edited on the Projects (PM301000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProjectEntry" /> graph).
/// </summary>
[PXCacheName("PM Billing Contact")]
[PXBreakInheritance]
[PXProjection(typeof (MappedSelect<PMBillingContact, From<BqlTableMapper<PMProjectBillingContact, PMProjectBillingContactMapped>, Union<BqlTableMapper<PMCustomerBillingContact, PMCustomerBillingContactMapped>>>>))]
[Serializable]
public class PMBillingContact : PMContact
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  PMBillingContact>.By<PMBillingContact.contactID>
  {
    public static PMContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (PMContact) PrimaryKeyOf<PMBillingContact>.By<PMBillingContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingContact.contactID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingContact.customerID>
  {
  }

  public new abstract class customerContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMBillingContact.customerContactID>
  {
  }

  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingContact.isDefaultContact>
  {
  }

  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingContact.overrideContact>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingContact.revisionID>
  {
  }

  public new abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.title>
  {
  }

  public new abstract class salutation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingContact.salutation>
  {
  }

  public new abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.attention>
  {
  }

  public new abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.fullName>
  {
  }

  public new abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.email>
  {
  }

  public new abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.fax>
  {
  }

  public new abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.faxType>
  {
  }

  public new abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.phone1>
  {
  }

  public new abstract class phone1Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingContact.phone1Type>
  {
  }

  public new abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.phone2>
  {
  }

  public new abstract class phone2Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingContact.phone2Type>
  {
  }

  public new abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingContact.phone3>
  {
  }

  public new abstract class phone3Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingContact.phone3Type>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBillingContact.noteID>
  {
  }
}
