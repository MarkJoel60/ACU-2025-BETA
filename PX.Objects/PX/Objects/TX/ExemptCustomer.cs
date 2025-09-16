// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ExemptCustomer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// A projection over <see cref="T:PX.Objects.AR.Customer" /> that selects all active customer records with the parimary contact and default address information.
/// </summary>
[PXProjection(typeof (SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.BAccount>.On<BqlOperand<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<Customer.bAccountID>>>, FbqlJoins.Inner<PX.Objects.CR.Address>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Address.bAccountID, Equal<Customer.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.Address.addressID, IBqlInt>.IsEqual<PX.Objects.CR.BAccount.defAddressID>>>>, FbqlJoins.Left<PX.Objects.CR.Contact>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<PX.Objects.CR.BAccount.primaryContactID>>>>, FbqlJoins.Inner<PX.Objects.CR.Location>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Location.bAccountID, Equal<Customer.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<PX.Objects.CR.BAccount.defLocationID>>>>>.Where<BqlOperand<PX.Objects.CR.BAccount.status, IBqlString>.IsEqual<CustomerStatus.active>>), Persistent = true)]
[PXHidden]
[Serializable]
public class ExemptCustomer : Customer
{
  /// <summary>
  /// The human-readable identifier of the customer account.
  /// </summary>
  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.acctCD))]
  [PXUIField(DisplayName = "Customer ID")]
  public virtual 
  #nullable disable
  string CustomerID { get; set; }

  /// <summary>The full name of the customer.</summary>
  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField(DisplayName = "Customer Name")]
  public virtual string CustomerName { get; set; }

  /// <summary>The tax registration ID of the customer.</summary>
  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Location.taxRegistrationID))]
  [PXUIField(DisplayName = "Tax Registration ID")]
  public virtual string CustomerTaxRegistrationID { get; set; }

  /// <summary>The first line of the cutomer address.</summary>
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine1))]
  [PXUIField(DisplayName = "Address Line 1")]
  public virtual string AddressLine1 { get; set; }

  /// <summary>The second line of the customer address.</summary>
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  public virtual string AddressLine2 { get; set; }

  /// <summary>The city of the customer address.</summary>
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.city))]
  [PXUIField(DisplayName = "City")]
  public virtual string City { get; set; }

  /// <summary>The state of the customer address.</summary>
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.state))]
  [PXUIField(DisplayName = "State")]
  public virtual string State { get; set; }

  /// <summary>The postal code of the customer address.</summary>
  [PXDBString(20, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.postalCode))]
  [PXUIField(DisplayName = "Postal Code")]
  public virtual string PostalCode { get; set; }

  /// <summary>The country of the customer address.</summary>
  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.CR.Address.countryID))]
  [PXUIField(DisplayName = "Country")]
  public virtual string CountryID { get; set; }

  /// <summary>The primary contact of the customer.</summary>
  [PXDBString(BqlField = typeof (PX.Objects.CR.Contact.displayName))]
  [PXUIField(DisplayName = "Primary Contact")]
  public virtual string PrimaryContact { get; set; }

  /// <summary>The phone number of the customer account.</summary>
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.phone1))]
  [PXUIField(DisplayName = "Phone Number")]
  public virtual string PhoneNumber { get; set; }

  /// <summary>The email address of the customer account.</summary>
  [PXDBString(BqlField = typeof (PX.Objects.CR.Contact.eMail))]
  [PXUIField(DisplayName = "Account Email")]
  public virtual string Email { get; set; }

  /// <summary>The fax number of the customer account.</summary>
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.fax))]
  [PXUIField(DisplayName = "Fax")]
  public virtual string Fax { get; set; }

  /// <summary>
  /// The company code for which the customer is created in the exemption certificate management (ECM) system.
  /// </summary>
  [PXDBString(BqlField = typeof (Customer.eCMCompanyCode))]
  [PXUIField(DisplayName = "Company Code")]
  public override string ECMCompanyCode
  {
    get => base.ECMCompanyCode;
    set => base.ECMCompanyCode = value;
  }

  public new class PK : PrimaryKeyOf<ExemptCustomer>.By<ExemptCustomer.bAccountID>
  {
    public static ExemptCustomer Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (ExemptCustomer) PrimaryKeyOf<ExemptCustomer>.By<ExemptCustomer.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new static class FK
  {
    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<Customer>.By<ExemptCustomer.defAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<Customer>.By<Customer.defContactID>
    {
    }

    public class DefaultLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<Customer>.By<ExemptCustomer.bAccountID, ExemptCustomer.defLocationID>
    {
    }
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExemptCustomer.selected>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExemptCustomer.bAccountID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExemptCustomer.defAddressID>
  {
  }

  public new abstract class primaryContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ExemptCustomer.primaryContactID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExemptCustomer.defLocationID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ExemptCustomer.noteID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.customerID>
  {
  }

  public abstract class customerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.customerName>
  {
  }

  public abstract class customerTaxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExemptCustomer.customerTaxRegistrationID>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.addressLine2>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.city>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.postalCode>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.countryID>
  {
  }

  public abstract class primaryContact : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExemptCustomer.primaryContact>
  {
  }

  public abstract class phoneNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.phoneNumber>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.email>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomer.fax>
  {
  }

  public new abstract class eCMCompanyCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExemptCustomer.eCMCompanyCode>
  {
  }
}
