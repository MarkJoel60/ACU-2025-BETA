// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRAddress
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
using System.Diagnostics;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Represents an address that is specified in the document, such as
/// <see cref="P:PX.Objects.CR.CROpportunity.OpportunityAddressID"> an opportunity's contact address</see>.
/// A <see cref="T:PX.Objects.CR.CRAddress" /> record is as a copy of the default location <see cref="T:PX.Objects.CR.Address" /> of the business account
/// and can be used to override the address specified in the document.
/// A <see cref="T:PX.Objects.CR.CRAddress" /> record is independent from changes to the original <see cref="T:PX.Objects.CR.Address" /> record.
/// </summary>
[PXCacheName("Opportunity Address")]
[DebuggerDisplay("{GetType().Name,nq} ({IsDefaultAddress}): AddressID = {AddressID,nq}, AddressLine1 = {AddressLine1}")]
[Serializable]
public class CRAddress : 
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
  protected int? _BAccountID;
  protected int? _BAccountAddressID;
  protected bool? _IsDefaultAddress;
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

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressID" />
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Address ID", Visible = false)]
  public virtual int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BAccountID" />
  [PXDBInt]
  [PXDBDefault(typeof (CROpportunity.bAccountID))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Address" /> record from the referenced <see cref="P:PX.Objects.CR.CRAddress.BAccountID" /> record.
  /// </summary>
  /// <value>
  /// This field corresponds to the <see cref="P:PX.Objects.CR.BAccount.DefAddressID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? BAccountAddressID
  {
    get => this._BAccountAddressID;
    set => this._BAccountAddressID = value;
  }

  /// <summary>
  /// If set to <see langword="true" />, the field indicates that the address record is identical to
  /// the original <see cref="T:PX.Objects.CR.Address" /> record that is referenced by <see cref="P:PX.Objects.CR.CRAddress.BAccountAddressID" />.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? IsDefaultAddress
  {
    get => this._IsDefaultAddress;
    set => this._IsDefaultAddress = value;
  }

  /// <summary>
  /// If set to <see langword="true" />, the field indicates that the address overrides the default <see cref="T:PX.Objects.CR.Address" /> record,
  /// that is referenced by <see cref="P:PX.Objects.CR.CRAddress.BAccountAddressID" />.
  /// </summary>
  /// <value>
  /// This field is the inverse of <see cref="P:PX.Objects.CR.CRAddress.IsDefaultAddress" />.
  /// </value>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideAddress
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRAddress.isDefaultAddress)})] get
    {
      if (!this._IsDefaultAddress.HasValue)
        return this._IsDefaultAddress;
      bool? isDefaultAddress = this._IsDefaultAddress;
      bool flag = false;
      return new bool?(isDefaultAddress.GetValueOrDefault() == flag & isDefaultAddress.HasValue);
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
      this._IsDefaultAddress = nullable1;
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.RevisionID" />
  [PXDBInt]
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
  [PX.Objects.CR.State(typeof (CRAddress.countryID))]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostalCode" />
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (CRAddress.countryID))]
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
  /// If set to <see langword="true" />, the field indicates that the address has been successfully validated by Acumatica ERP.
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

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Latitude" />
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Visible = false)]
  public virtual Decimal? Latitude { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Longitude" />
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Visible = false)]
  public virtual Decimal? Longitude { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>
  {
    public static CRAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (CRAddress) PrimaryKeyOf<CRAddress>.By<CRAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Business Account</summary>
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRAddress>.By<CRAddress.bAccountID>
    {
    }

    /// <summary>Business Account Address</summary>
    public class BusinessAccountAddress : 
      PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<CRAddress>.By<CRAddress.bAccountAddressID>
    {
    }

    /// <summary>Country</summary>
    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<CRAddress>.By<CRAddress.countryID>
    {
    }

    /// <summary>State</summary>
    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<CRAddress>.By<CRAddress.countryID, CRAddress.state>
    {
    }
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRAddress.addressID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRAddress.bAccountID>
  {
  }

  public abstract class bAccountAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRAddress.bAccountAddressID>
  {
  }

  public abstract class isDefaultAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRAddress.isDefaultAddress>
  {
  }

  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRAddress.overrideAddress>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRAddress.revisionID>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.addressLine2>
  {
  }

  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.department>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRAddress.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRAddress.districtName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRAddress.noteID>
  {
  }

  public abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRAddress.isValidated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRAddress.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRAddress.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRAddress.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRAddress.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRAddress.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRAddress.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRAddress.lastModifiedDateTime>
  {
  }

  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRAddress.latitude>
  {
  }

  public abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRAddress.longitude>
  {
  }
}
