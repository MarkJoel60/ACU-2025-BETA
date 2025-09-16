// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRShippingContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Shipping Contact")]
[Serializable]
public class CRShippingContact : CRContact
{
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public override bool? IsDefaultContact
  {
    get => this._IsDefaultContact;
    set => this._IsDefaultContact = value;
  }

  [PXBool]
  [PXUIField]
  public override bool? OverrideContact
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRShippingContact.isDefaultContact)})] get
    {
      return base.OverrideContact;
    }
    set => base.OverrideContact = value;
  }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  CRShippingContact>.By<CRShippingContact.contactID>
  {
    public static CRShippingContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (CRShippingContact) PrimaryKeyOf<CRShippingContact>.By<CRShippingContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public new static class FK
  {
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRShippingContact>.By<CRShippingContact.bAccountID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<CRShippingContact>.By<CRShippingContact.bAccountContactID>
    {
    }

    public class Location : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<CRShippingContact>.By<CRShippingContact.bAccountID, CRShippingContact.bAccountLocationID>
    {
    }
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRShippingContact.contactID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRShippingContact.bAccountID>
  {
  }

  public new abstract class bAccountContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRShippingContact.bAccountContactID>
  {
  }

  public new abstract class bAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRShippingContact.bAccountLocationID>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRShippingContact.revisionID>
  {
  }

  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRShippingContact.isDefaultContact>
  {
  }

  public new abstract class overrideContact : IBqlField, IBqlOperand
  {
  }
}
