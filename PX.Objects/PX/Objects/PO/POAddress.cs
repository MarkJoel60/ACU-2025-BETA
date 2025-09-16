// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAddress
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
namespace PX.Objects.PO;

/// <summary>
/// Represents an address that is used in purchase order management.
/// </summary>
/// <remarks>
/// An address state is frozen at the moment of creation of a purchase order.
/// Each modification to the original address leads to the generation of a new <see cref="T:PX.Objects.PO.POAddress.revisionID">revision</see> of the address, which is used in the new purchase order or in the overridden address in an existed purchase order.
/// If the <see cref="T:PX.Objects.PO.POAddress.isDefaultAddress" /> field is <see langword="false" />, the address has been overridden or the original address has been copied with the <see cref="T:PX.Objects.PO.POAddress.revisionID">revision</see> related to the moment of creation.
/// Also this is the base class for the following derived DACs:
/// <list type="bullet">
/// <item><description><see cref="T:PX.Objects.PO.POShipAddress" />, which contains the information related to shipping of the ordered items</description></item>
/// <item><description><see cref="T:PX.Objects.PO.PORemitAddress" />, which contains the information about the vendor to supply the ordered items</description></item>
/// </list>
/// The records of this type are created and edited on the <i>Purchase Orders (PO301000)</i> form
/// (which corresponds to the <see cref="T:PX.Objects.PO.POOrderEntry" /> graph).
/// </remarks>
[PXCacheName("PO Address")]
[Serializable]
public class POAddress : 
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

  /// <summary>The identifier of the address.</summary>
  [PXDBIdentity(IsKey = true)]
  public virtual int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.BAccount">business account</see>.
  /// The field is included in <see cref="T:PX.Objects.PO.POAddress.FK.BAccount" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.BAccount.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Address">business account address</see>.
  /// The field is included in <see cref="T:PX.Objects.PO.POAddress.FK.BAccountAddress" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.Address.addressID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? BAccountAddressID
  {
    get => this._BAccountAddressID;
    set => this._BAccountAddressID = value;
  }

  /// <summary>
  /// If the value is <see langword="false" />, the address has been overridden or the original address has been copied with the revision related to the moment of creation.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Is Default Address")]
  [PXExtraKey]
  public virtual bool? IsDefaultAddress
  {
    get => this._IsDefaultAddress;
    set => this._IsDefaultAddress = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the address is overriden.
  /// </summary>
  /// <value>
  /// If the value of the <see cref="T:PX.Objects.PO.POAddress.isDefaultAddress" /> field is <see langword="null" />, the value of this field is <see langword="null" />.
  /// If the value of the <see cref="T:PX.Objects.PO.POAddress.isDefaultAddress" /> field is <see langword="true" />, the value of this field is <see langword="false" />.
  /// If the value of the <see cref="T:PX.Objects.PO.POAddress.isDefaultAddress" /> field is <see langword="false" />, the value of this field is <see langword="true" />.
  /// </value>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideAddress
  {
    [PXDependsOnFields(new System.Type[] {typeof (POAddress.isDefaultAddress)})] get
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

  /// <summary>The identifier of the revision address.</summary>
  /// <remarks>
  /// Each modification to the original address leads to the generation of a new revision of the address, which is used in the new purchase order or in the overridden address in an existed purchase order.
  /// </remarks>
  [PXDefault]
  [PXDBInt]
  [PXUIField]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>The first line of the address.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string AddressLine1
  {
    get => this._AddressLine1;
    set => this._AddressLine1 = value;
  }

  /// <summary>The second line of the address.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXPersonalDataField]
  public virtual string AddressLine2
  {
    get => this._AddressLine2;
    set => this._AddressLine2 = value;
  }

  /// <summary>The third line of the address.</summary>
  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 3")]
  [PXPersonalDataField]
  public virtual string AddressLine3
  {
    get => this._AddressLine3;
    set => this._AddressLine3 = value;
  }

  /// <summary>The city of the address.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string City
  {
    get => this._City;
    set => this._City = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Country">country</see>.
  /// The field is included in <see cref="T:PX.Objects.PO.POAddress.FK.Country" /> and <see cref="T:PX.Objects.PO.POAddress.FK.State" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.Country.countryID" /> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="P:PX.Objects.PO.POAddress.State">state</see>.
  /// The field is included in <see cref="T:PX.Objects.PO.POAddress.FK.State" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.State.stateID" /> field.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (POAddress.countryID))]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <summary>The postal code of the address.</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (POAddress.countryID))]
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
  /// Specifies (if set to <see langword="true" />) that the address has been validated with a third-party specialized software or service.
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

  /// <summary>The latitude of the address location.</summary>
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Visible = false)]
  public virtual Decimal? Latitude { get; set; }

  /// <summary>The longitude of the address location.</summary>
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Visible = false)]
  public virtual Decimal? Longitude { get; set; }

  public class PK : PrimaryKeyOf<POAddress>.By<POAddress.addressID>
  {
    public static POAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (POAddress) PrimaryKeyOf<POAddress>.By<POAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public static class FK
  {
    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<POAddress>.By<POAddress.bAccountID>
    {
    }

    public class BAccountAddress : 
      PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<POAddress>.By<POAddress.bAccountAddressID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<POAddress>.By<POAddress.countryID>
    {
    }

    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<POAddress>.By<POAddress.countryID, POAddress.state>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.AddressID" />
  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAddress.addressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.BAccountID" />
  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAddress.bAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.BAccountAddressID" />
  public abstract class bAccountAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAddress.bAccountAddressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.IsDefaultAddress" />
  public abstract class isDefaultAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAddress.isDefaultAddress>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.OverrideAddress" />
  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAddress.overrideAddress>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.RevisionID" />
  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAddress.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.AddressLine1" />
  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.addressLine1>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.AddressLine2" />
  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.addressLine2>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.AddressLine3" />
  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.addressLine3>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.City" />
  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.city>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.CountryID" />
  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.countryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.State" />
  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.state>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.PostalCode" />
  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.department>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAddress.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAddress.districtName>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.NoteID" />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAddress.noteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.IsValidated" />
  public abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAddress.isValidated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POAddress.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAddress.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAddress.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAddress.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAddress.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAddress.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAddress.lastModifiedDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.Latitude" />
  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAddress.latitude>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAddress.Longitude" />
  public abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAddress.longitude>
  {
  }
}
