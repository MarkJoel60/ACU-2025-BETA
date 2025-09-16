// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An address that is specified in a customer document, such as
/// <see cref="P:PX.Objects.AR.ARInvoice.BillAddressID">an invoice's billing address</see>.
/// An <see cref="T:PX.Objects.AR.ARAddress" /> record is as a copy of default customer location's
/// <see cref="T:PX.Objects.CR.Address" /> and can be used to override the document-level address;
/// the record is independent of changes to the original <see cref="T:PX.Objects.CR.Address" /> record.
/// The entities of this type are created and edited on the Invoices and Memos
/// (AR301000) form, which corresponds to the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> graph.
/// </summary>
[PXCacheName("AR Address")]
[Serializable]
public class ARAddress : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAddress,
  IAddressBase,
  IValidatedAddress,
  INotable,
  IAddressLocation,
  IAddressISO20022
{
  protected int? _AddressID;
  protected int? _CustomerID;
  protected int? _CustomerAddressID;
  protected bool? _IsDefaultBillAddress;
  protected int? _RevisionID;
  protected 
  #nullable disable
  string _AddressLine1;
  protected string _AddressLine2;
  protected string _AddressLine3;
  protected string _City;
  protected string _CountryID;
  protected string _State;
  protected string _PostalCode;
  protected bool? _IsValidated;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The unique integer identifier of the record.
  /// This field is the key field.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Address ID", Visible = false)]
  public virtual int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer" /> record,
  /// which is specified in the document to which the address belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Customer.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBDefault(typeof (ARRegister.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// An alias for <see cref="P:PX.Objects.AR.ARAddress.CustomerID" />, which exists
  /// for the purpose of implementing the <see cref="T:PX.Objects.CS.IAddress" /> interface.
  /// </summary>
  public virtual int? BAccountID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Address" /> record from which
  /// the address was originally created.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? CustomerAddressID
  {
    get => this._CustomerAddressID;
    set => this._CustomerAddressID = value;
  }

  /// <summary>
  /// An alias for <see cref="P:PX.Objects.AR.ARAddress.CustomerAddressID" />,
  /// which exists for the purpose of implementing the
  /// <see cref="T:PX.Objects.CS.IAddress" /> interface.
  /// </summary>
  public virtual int? BAccountAddressID
  {
    get => this._CustomerAddressID;
    set => this._CustomerAddressID = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the address record
  /// is identical to the original <see cref="T:PX.Objects.CR.Address" />
  /// record, which is referenced by <see cref="P:PX.Objects.AR.ARAddress.CustomerAddressID" />.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  [PXExtraKey]
  public virtual bool? IsDefaultBillAddress
  {
    get => this._IsDefaultBillAddress;
    set => this._IsDefaultBillAddress = value;
  }

  /// <summary>
  /// An alias for <see cref="P:PX.Objects.AR.ARAddress.IsDefaultBillAddress" />,
  /// which exists for the purpose of implementing the
  /// <see cref="T:PX.Objects.CS.IAddress" /> interface.
  /// </summary>
  public virtual bool? IsDefaultAddress
  {
    get => this._IsDefaultBillAddress;
    set => this._IsDefaultBillAddress = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the address
  /// overrides the default <see cref="T:PX.Objects.CR.Address" /> record, which is
  /// referenced by <see cref="P:PX.Objects.AR.ARAddress.CustomerAddressID" />. This field
  /// is the inverse of <see cref="P:PX.Objects.AR.ARAddress.IsDefaultBillAddress" />.
  /// </summary>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideAddress
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARAddress.isDefaultBillAddress)})] get
    {
      if (!this._IsDefaultBillAddress.HasValue)
        return this._IsDefaultBillAddress;
      bool? defaultBillAddress = this._IsDefaultBillAddress;
      bool flag = false;
      return new bool?(defaultBillAddress.GetValueOrDefault() == flag & defaultBillAddress.HasValue);
    }
    set
    {
      bool? nullable1;
      if (value.HasValue)
      {
        bool? nullable2 = value;
        bool flag = false;
        nullable1 = new bool?(nullable2.GetValueOrDefault() == flag & nullable2.HasValue);
      }
      else
        nullable1 = value;
      this._IsDefaultBillAddress = nullable1;
    }
  }

  [PXDBBool]
  public virtual bool? IsEncrypted { get; set; }

  /// <summary>
  /// The revision ID of the original <see cref="T:PX.Objects.CR.Address" /> record
  /// from which the record originates.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Address.RevisionID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine1" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string AddressLine1
  {
    get => this._AddressLine1;
    set => this._AddressLine1 = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine2" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXPersonalDataField]
  public virtual string AddressLine2
  {
    get => this._AddressLine2;
    set => this._AddressLine2 = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine3" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 3")]
  [PXPersonalDataField]
  public virtual string AddressLine3
  {
    get => this._AddressLine3;
    set => this._AddressLine3 = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.City" />
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string City
  {
    get => this._City;
    set => this._City = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.CountryID" />
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.State" />
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (ARAddress.countryID))]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostalCode" />
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (ARAddress.countryID))]
  [PXPersonalDataField]
  public virtual string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Department" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Department")]
  [PXPersonalDataField]
  public virtual string Department { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.SubDepartment" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Subdepartment")]
  [PXPersonalDataField]
  public virtual string SubDepartment { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.StreetName" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Street Name")]
  [PXPersonalDataField]
  public virtual string StreetName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingNumber" />
  [PXDBString(16 /*0x10*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Building Number")]
  [PXPersonalDataField]
  public virtual string BuildingNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingName" />
  [PXDBString(35, IsUnicode = true)]
  [PXUIField(DisplayName = "Building Name")]
  [PXPersonalDataField]
  public virtual string BuildingName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Floor" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Floor")]
  [PXPersonalDataField]
  public virtual string Floor { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.UnitNumber" />
  [PXDBString(16 /*0x10*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Unit Number")]
  [PXPersonalDataField]
  public virtual string UnitNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostBox" />
  [PXDBString(16 /*0x10*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Post Box")]
  [PXPersonalDataField]
  public virtual string PostBox { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Room" />
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Room")]
  [PXPersonalDataField]
  public virtual string Room { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.TownLocationName" />
  [PXDBString(35, IsUnicode = true)]
  [PXUIField(DisplayName = "Town Location Name")]
  [PXPersonalDataField]
  public virtual string TownLocationName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.DistrictName" />
  [PXDBString(35, IsUnicode = true)]
  [PXUIField(DisplayName = "District Name")]
  [PXPersonalDataField]
  public virtual string DistrictName { get; set; }

  [PXDBGuidNotNull]
  public virtual Guid? NoteID { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the address has been
  /// successfully validated by Acumatica.
  /// </summary>
  [PXDefault(false)]
  [PXDBBool]
  [ValidatedAddress]
  [PXUIField(DisplayName = "Validated", FieldClass = "Validate Address")]
  public virtual bool? IsValidated
  {
    get => this._IsValidated;
    set => this._IsValidated = value;
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

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Visible = false)]
  public virtual Decimal? Latitude { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Visible = false)]
  public virtual Decimal? Longitude { get; set; }

  public class PK : PrimaryKeyOf<ARAddress>.By<ARAddress.addressID>
  {
    public static ARAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (ARAddress) PrimaryKeyOf<ARAddress>.By<ARAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARAddress>.By<ARAddress.customerID>
    {
    }

    public class CustomerAddress : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<ARAddress>.By<ARAddress.customerAddressID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<ARAddress>.By<ARAddress.countryID>
    {
    }
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAddress.addressID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAddress.customerID>
  {
  }

  public abstract class customerAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAddress.customerAddressID>
  {
  }

  public abstract class isDefaultBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAddress.isDefaultBillAddress>
  {
  }

  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAddress.overrideAddress>
  {
  }

  /// <summary>
  /// Indicates if  Personally identifiable Information fields
  /// (example AddressLine1 , AddressLine2 etc.) are encrypted or not
  /// </summary>
  public abstract class isEncrypted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAddress.isEncrypted>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAddress.revisionID>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.addressLine2>
  {
  }

  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.department>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAddress.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAddress.districtName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAddress.noteID>
  {
  }

  public abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAddress.isValidated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARAddress.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAddress.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAddress.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAddress.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAddress.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAddress.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAddress.lastModifiedDateTime>
  {
  }

  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAddress.latitude>
  {
  }

  public abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAddress.longitude>
  {
  }
}
