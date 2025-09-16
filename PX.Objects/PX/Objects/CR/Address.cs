// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Address
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Represents an address of the <see cref="T:PX.Objects.CR.BAccount">business account</see>, the <see cref="T:PX.Objects.CR.Contact">contact</see> or the <see cref="T:PX.Objects.CR.CRLead">lead</see>.
/// </summary>
/// <remarks>
/// The records of this type are created and edited on the <i>Business Accounts (CR303000)</i>, <i>Contacts (CR302000)</i>, or <i>Leads (CR301000)</i> form,
/// which correspond to the <see cref="T:PX.Objects.CR.BusinessAccountMaint" />, <see cref="T:PX.Objects.CR.ContactMaint" /> or <see cref="T:PX.Objects.CR.LeadMaint" /> graph respectively.
/// </remarks>
[PXCacheName("Address")]
[PXPersonalDataTable(typeof (Select<Address, Where<Address.addressID, Equal<Current<Contact.defAddressID>>>>))]
[DebuggerDisplay("Address: AddressID = {AddressID,nq}, DisplayName = {DisplayName}")]
[Serializable]
public class Address : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAddressBase,
  IValidatedAddress,
  IPXSelectable,
  INotable,
  IAddressLocation,
  IAddressISO20022
{
  protected int? _AddressID;
  protected int? _BAccountID;
  protected int? _RevisionID;
  protected 
  #nullable disable
  string _AddressType;
  protected string _AddressLine1;
  protected string _AddressLine2;
  protected string _AddressLine3;
  protected string _City;
  protected string _CountryID;
  protected string _State;
  protected string _PostalCode;
  protected Guid? _NoteID;
  protected bool? _IsValidated;
  protected string _TaxLocationCode;
  protected string _TaxLocationCodeFull;
  protected string _TaxMunicipalCode;
  protected string _TaxSchoolCode;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  /// <summary>
  /// The unique integer identifier of the record.
  /// This field is the key field.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.BAccount" /> record that is specified in the business account to which the address belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt(IsKey = false)]
  [PXDBDefault(typeof (BAccount.bAccountID))]
  [PXUIField]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>The revision ID of the original record.</summary>
  [PXDBInt]
  [PXDefault(0)]
  [AddressRevisionID]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>The type of the address.</summary>
  /// <value>
  /// The field can have one of the values described in the <see cref="T:PX.Objects.CR.Address.AddressTypes" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.Address.AddressTypes.BusinessAddress" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("BS")]
  [Address.AddressTypes.List]
  [PXUIField]
  public virtual string AddressType
  {
    get => this._AddressType;
    set => this._AddressType = value;
  }

  /// <summary>
  /// The display name of the address as a concatenation of address lines (<see cref="P:PX.Objects.CR.Address.AddressLine1" />, <see cref="P:PX.Objects.CR.Address.AddressLine2" /> and <see cref="P:PX.Objects.CR.Address.AddressLine2" /> fields).
  /// </summary>
  [PXString]
  [PXUIField]
  [PXDBCalced(typeof (Switch<Case<Where<Address.addressLine1, IsNotNull, And<Address.addressLine2, IsNotNull, And<Address.addressLine3, IsNotNull>>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Address.addressLine1, Space>>, Address.addressLine2>>, Space>>, IBqlString>.Concat<Address.addressLine3>, Case<Where<Address.addressLine1, IsNotNull, And<Address.addressLine2, IsNotNull, And<Address.addressLine3, IsNull>>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Address.addressLine1, Space>>, IBqlString>.Concat<Address.addressLine2>, Case<Where<Address.addressLine1, IsNotNull, And<Address.addressLine2, IsNull, And<Address.addressLine3, IsNotNull>>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Address.addressLine1, Space>>, IBqlString>.Concat<Address.addressLine3>, Case<Where<Address.addressLine1, IsNull, And<Address.addressLine2, IsNotNull, And<Address.addressLine3, IsNotNull>>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<Address.addressLine2, Space>>, IBqlString>.Concat<Address.addressLine3>, Case<Where<Address.addressLine1, IsNull, And<Address.addressLine2, IsNull, And<Address.addressLine3, IsNotNull>>>, Address.addressLine3, Case<Where<Address.addressLine1, IsNull, And<Address.addressLine2, IsNotNull, And<Address.addressLine3, IsNull>>>, Address.addressLine2, Case<Where<Address.addressLine1, IsNotNull, And<Address.addressLine2, IsNull, And<Address.addressLine3, IsNull>>>, Address.addressLine1, Case<Where<Address.addressLine1, IsNull, And<Address.addressLine2, IsNull, And<Address.addressLine3, IsNull>>>, Empty>>>>>>>>>), typeof (string))]
  public virtual string DisplayName { get; set; }

  /// <summary>The first line of the street address.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField]
  [PXMassMergableField]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string AddressLine1
  {
    get => this._AddressLine1;
    set => this._AddressLine1 = value;
  }

  /// <summary>The second line of the street address.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXMassMergableField]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string AddressLine2
  {
    get => this._AddressLine2;
    set => this._AddressLine2 = value;
  }

  /// <summary>The third line of the street address.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 3")]
  [PXMassMergableField]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string AddressLine3
  {
    get => this._AddressLine3;
    set => this._AddressLine3 = value;
  }

  /// <summary>The name of the city or inhabited locality.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string City
  {
    get => this._City;
    set => this._City = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Country" /> record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Country.CountryID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXContactInfoField]
  [PXForeignReference(typeof (Address.FK.Country))]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  /// <summary>
  /// The string identifier of the state or province part of the address.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.State.StateID" /> field.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (Address.countryID))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXContactInfoField]
  [PXForeignReference(typeof (Address.FK.State))]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <summary>The postal code part of the address.</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (Address.countryID))]
  [PXMassMergableField]
  [PXDeduplicationSearchField(false, true)]
  [PXPersonalDataField]
  [PXContactInfoField]
  public virtual string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  /// <summary>
  /// Identification of a division of a large organization or building (ISO 20022).
  /// </summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Department")]
  [PXPersonalDataField]
  public virtual string Department { get; set; }

  /// <summary>
  /// Identification of a sub-division of a large organization or building (ISO 20022).
  /// </summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Subdepartment")]
  [PXPersonalDataField]
  public virtual string SubDepartment { get; set; }

  /// <summary>Name of a street or thoroughfare (ISO 20022).</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Street Name")]
  [PXPersonalDataField]
  public virtual string StreetName { get; set; }

  /// <summary>
  /// Number that identifies the position of a building on a street (ISO 20022).
  /// </summary>
  [PXDBString(16 /*0x10*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Building Number")]
  [PXPersonalDataField]
  public virtual string BuildingNumber { get; set; }

  /// <summary>Name of the building or house (ISO 20022).</summary>
  [PXDBString(35, IsUnicode = true)]
  [PXUIField(DisplayName = "Building Name")]
  [PXPersonalDataField]
  public virtual string BuildingName { get; set; }

  /// <summary>Floor or storey within a building (ISO 20022).</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Floor")]
  [PXPersonalDataField]
  public virtual string Floor { get; set; }

  /// <summary>
  /// Identifies a flat or dwelling within the building (ISO 20022).
  /// </summary>
  [PXDBString(16 /*0x10*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Unit Number")]
  [PXPersonalDataField]
  public virtual string UnitNumber { get; set; }

  /// <summary>
  /// Numbered box in a post office, assigned to a person or organisation, where letters are kept until called for (ISO 20022).
  /// </summary>
  [PXDBString(16 /*0x10*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Post Box")]
  [PXPersonalDataField]
  public virtual string PostBox { get; set; }

  /// <summary>Floor or storey within a building (ISO 20022).</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Room")]
  [PXPersonalDataField]
  public virtual string Room { get; set; }

  /// <summary>Specific location name within the town (ISO 20022).</summary>
  [PXDBString(35, IsUnicode = true)]
  [PXUIField(DisplayName = "Town Location Name")]
  [PXPersonalDataField]
  public virtual string TownLocationName { get; set; }

  /// <summary>
  /// Identifies a subdivision within a country sub-division (ISO 20022).
  /// </summary>
  [PXDBString(35, IsUnicode = true)]
  [PXUIField(DisplayName = "District Name")]
  [PXPersonalDataField]
  public virtual string DistrictName { get; set; }

  [PXUniqueNote(DescriptionField = typeof (Address.displayName))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// If set to <see langword="true" />, this field indicates that the address has been successfully validated by Acumatica ERP.
  /// </summary>
  [PXDefault(false)]
  [PXDBBool]
  [ValidatedAddress]
  [PXUIField(DisplayName = "Validated", Enabled = false, FieldClass = "Validate Address")]
  public virtual bool? IsValidated
  {
    get => this._IsValidated;
    set => this._IsValidated = value;
  }

  /// <summary>The code of the location tax that is used in reports.</summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Location Code")]
  public virtual string TaxLocationCode
  {
    get => this._TaxLocationCode;
    set => this._TaxLocationCode = value;
  }

  /// <summary>
  /// The tax location code according to Symmetry Location Service specifications.
  /// </summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Full Tax Location Code")]
  public virtual string TaxLocationCodeFull
  {
    get => this._TaxLocationCodeFull;
    set => this._TaxLocationCodeFull = value;
  }

  /// <summary>
  /// The code of the municipal tax that is used in reports.
  /// </summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Municipal Code")]
  public virtual string TaxMunicipalCode
  {
    get => this._TaxMunicipalCode;
    set => this._TaxMunicipalCode = value;
  }

  /// <summary>The code of the school tax that is used in reports.</summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax School Code")]
  public virtual string TaxSchoolCode
  {
    get => this._TaxSchoolCode;
    set => this._TaxSchoolCode = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>
  /// The latitude coordinates that are entered for a location if the location does not contain the postal address or the postal address cannot be validated.
  /// The field value is filled using the Google or Bing lookup.
  /// </summary>
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Visible = false)]
  public virtual Decimal? Latitude { get; set; }

  /// <summary>
  /// The longitude coordinates that are entered for a location if the location does not contain the postal address or the postal address cannot be validated.
  /// The field value is filled using the Google or Bing lookup.
  /// </summary>
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Visible = false)]
  public virtual Decimal? Longitude { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<Address>.By<Address.addressID>
  {
    public static Address Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (Address) PrimaryKeyOf<Address>.By<Address.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Country</summary>
    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<Address>.By<Address.countryID>
    {
    }

    /// <summary>State</summary>
    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<Address>.By<Address.countryID, Address.state>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Address.selected>
  {
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Address.addressID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Address.bAccountID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Address.revisionID>
  {
  }

  public abstract class addressType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.addressType>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.displayName>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.addressLine2>
  {
  }

  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.department>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.districtName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Address.noteID>
  {
  }

  public abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Address.isValidated>
  {
  }

  public abstract class taxLocationCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.taxLocationCode>
  {
  }

  public abstract class taxLocationCodeFull : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address.taxLocationCodeFull>
  {
  }

  public abstract class taxMunicipalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address.taxMunicipalCode>
  {
  }

  public abstract class taxSchoolCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.taxSchoolCode>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Address.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Address.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Address.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Address.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Address.lastModifiedDateTime>
  {
  }

  public class AddressTypes
  {
    public const string BusinessAddress = "BS";
    public const string HomeAddress = "HM";
    public const string OtherAddress = "UN";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "BS", "HM", "UN" }, new string[3]
        {
          "Business",
          "Home",
          "Other"
        })
      {
      }
    }

    public class businessAddress : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      Address.AddressTypes.businessAddress>
    {
      public businessAddress()
        : base("BS")
      {
      }
    }

    public class homeAddress : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Address.AddressTypes.homeAddress>
    {
      public homeAddress()
        : base("HM")
      {
      }
    }

    public class otherAddress : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      Address.AddressTypes.otherAddress>
    {
      public otherAddress()
        : base("UN")
      {
      }
    }
  }

  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Address.latitude>
  {
  }

  public abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Address.longitude>
  {
  }
}
