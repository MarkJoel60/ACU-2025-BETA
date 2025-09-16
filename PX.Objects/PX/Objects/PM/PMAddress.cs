// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Represents a billing address that is specified in the <see cref="T:PX.Objects.PM.PMProject">project</see> for billing purposes. These settings are initially populated with the information
/// specified on the <strong>Billing Settings</strong> tab of the Customers (AR303000) form (as a copy of the default customer location's <see cref="T:PX.Objects.CR.Address">address</see>), but
/// you can override any of the default settings. The record is independent of changes to the original <see cref="T:PX.Objects.CR.Address">address</see> record. The entities of this type are
/// created and edited on the Projects (PM301000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProjectEntry" /> graph)</summary>
[PXCacheName("PM Address")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMAddress : 
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
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

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
  /// An alias for <see cref="P:PX.Objects.PM.PMAddress.CustomerAddressID" />,
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
  /// record, which is referenced by <see cref="P:PX.Objects.PM.PMAddress.CustomerAddressID" />.
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
  /// An alias for <see cref="P:PX.Objects.PM.PMAddress.IsDefaultBillAddress" />,
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
  /// referenced by <see cref="P:PX.Objects.PM.PMAddress.CustomerAddressID" />. This field
  /// is the inverse of <see cref="P:PX.Objects.PM.PMAddress.IsDefaultBillAddress" />.
  /// </summary>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideAddress
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMAddress.isDefaultBillAddress)})] get
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

  /// <summary>
  /// The revision ID of the original <see cref="T:PX.Objects.CR.Address" /> record
  /// from which the record originates.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Address.RevisionID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>The first address line.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string AddressLine1
  {
    get => this._AddressLine1;
    set => this._AddressLine1 = value;
  }

  /// <summary>The second address line.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXPersonalDataField]
  public virtual string AddressLine2
  {
    get => this._AddressLine2;
    set => this._AddressLine2 = value;
  }

  /// <summary>The third address line.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 3")]
  [PXPersonalDataField]
  public virtual string AddressLine3
  {
    get => this._AddressLine3;
    set => this._AddressLine3 = value;
  }

  /// <summary>The name of the city or inhabited locality.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
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
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Country")]
  [PXSelector(typeof (Search<PX.Objects.CS.Country.countryID>), DescriptionField = typeof (PX.Objects.CS.Country.description), CacheGlobal = true)]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  /// <summary>The name of the state.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (PMAddress.countryID), DescriptionField = typeof (PX.Objects.CS.State.name))]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <summary>The postal code.</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (PMAddress.countryID))]
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

  /// <summary>The latitude of the address.</summary>
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Visible = false)]
  public virtual Decimal? Latitude { get; set; }

  /// <summary>The longitude of the address.</summary>
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Visible = false)]
  public virtual Decimal? Longitude { get; set; }

  public class PK : PrimaryKeyOf<PMAddress>.By<PMAddress.addressID>
  {
    public static PMAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (PMAddress) PrimaryKeyOf<PMAddress>.By<PMAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAddress.addressID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAddress.customerID>
  {
  }

  public abstract class customerAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAddress.customerAddressID>
  {
  }

  public abstract class isDefaultBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMAddress.isDefaultBillAddress>
  {
  }

  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAddress.overrideAddress>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMAddress.revisionID>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.addressLine2>
  {
  }

  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.department>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAddress.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMAddress.districtName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMAddress.noteID>
  {
  }

  public abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMAddress.isValidated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMAddress.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMAddress.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAddress.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAddress.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMAddress.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMAddress.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMAddress.lastModifiedDateTime>
  {
  }

  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMAddress.latitude>
  {
  }

  public abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMAddress.longitude>
  {
  }
}
