// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARShippingContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("AR Contact")]
[PXBreakInheritance]
[Serializable]
public class ARShippingContact : ARContact
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  ARShippingContact>.By<ARShippingContact.contactID>
  {
    public static ARShippingContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (ARShippingContact) PrimaryKeyOf<ARShippingContact>.By<ARShippingContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<ARShippingContact>.By<ARShippingContact.customerID>
    {
    }

    public class CustomerContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<ARShippingContact>.By<ARShippingContact.customerContactID>
    {
    }
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARShippingContact.contactID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARShippingContact.customerID>
  {
  }

  public new abstract class customerContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARShippingContact.customerContactID>
  {
  }

  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARShippingContact.isDefaultContact>
  {
  }

  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARShippingContact.overrideContact>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARShippingContact.revisionID>
  {
  }

  public new abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.title>
  {
  }

  public new abstract class salutation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingContact.salutation>
  {
  }

  public new abstract class attention : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingContact.attention>
  {
  }

  public new abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.fullName>
  {
  }

  public new abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.email>
  {
  }

  public new abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.fax>
  {
  }

  public new abstract class faxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.faxType>
  {
  }

  public new abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.phone1>
  {
  }

  public new abstract class phone1Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingContact.phone1Type>
  {
  }

  public new abstract class phone2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.phone2>
  {
  }

  public new abstract class phone2Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingContact.phone2Type>
  {
  }

  public new abstract class phone3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingContact.phone3>
  {
  }

  public new abstract class phone3Type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingContact.phone3Type>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARShippingContact.noteID>
  {
  }
}
