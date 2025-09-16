// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMShippingContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a shipping contact that is specified in the <see cref="T:PX.Objects.PM.PMProforma">pro forma invoice</see>.
/// A <see cref="T:PX.Objects.PM.PMShippingContact" /> record is a copy of customer location's
/// <see cref="T:PX.Objects.CR.Contact" /> and can be used to override the shipping contact at the document level.
/// The record is independent of changes to the original <see cref="T:PX.Objects.CR.Contact" /> record.
/// The entities of this type are created and edited on the Pro Forma Invoices (PM307000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProformaEntry" /> graph).
/// </summary>
[PXCacheName("Project Contact")]
[Serializable]
public class PMShippingContact : PMContact
{
  public new abstract class contactID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMShippingContact.contactID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMShippingContact.customerID>
  {
  }

  public new abstract class customerContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMShippingContact.customerContactID>
  {
  }

  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMShippingContact.isDefaultContact>
  {
  }

  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMShippingContact.overrideContact>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMShippingContact.revisionID>
  {
  }

  public new abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.title>
  {
  }

  public new abstract class salutation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingContact.salutation>
  {
  }

  public new abstract class attention : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingContact.attention>
  {
  }

  public new abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.fullName>
  {
  }

  public new abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.email>
  {
  }

  public new abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.fax>
  {
  }

  public new abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.faxType>
  {
  }

  public new abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.phone1>
  {
  }

  public new abstract class phone1Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingContact.phone1Type>
  {
  }

  public new abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.phone2>
  {
  }

  public new abstract class phone2Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingContact.phone2Type>
  {
  }

  public new abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingContact.phone3>
  {
  }

  public new abstract class phone3Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingContact.phone3Type>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMShippingContact.noteID>
  {
  }
}
