// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// Represents an address that is used in sales order management.
/// </summary>
/// <remarks>
/// An address state is frozen at the moment of creation of a sales order.
/// Each modification to the original address leads to the generation of a new <see cref="T:PX.Objects.SO.SOAddress.revisionID">revision</see> of the address, which is used in the new sales order or in the overridden address in an existed sales order.
/// If the <see cref="T:PX.Objects.SO.SOAddress.isDefaultAddress" /> field is <see langword="false" />, the address has been overridden or the original address has been copied with the <see cref="T:PX.Objects.SO.SOAddress.revisionID">revision</see> related to the moment of creation.
/// Also this is the base class for the following derived DACs:
/// <list type="bullet">
/// <item><description><see cref="T:PX.Objects.SO.SOShipmentAddress" />, which contains the information related to shipping of the ordered items</description></item>
/// <item><description><see cref="T:PX.Objects.SO.SOShippingAddress" />, which contains the information related to shipping of the ordered items</description></item>
/// <item><description><see cref="T:PX.Objects.SO.SOBillingAddress" />, which contains the information related to billing of the ordered items</description></item>
/// </list>
/// The records of this type are created and edited on the following forms:
/// <list type="bullet">
/// <item><description><i>Invoices (SO303000)</i> (which corresponds to the <see cref="T:PX.Objects.SO.SOInvoiceEntry" /> graph)</description></item>
/// <item><description><i>Shipments (SO302000)</i> (which corresponds to the <see cref="T:PX.Objects.SO.SOShipmentEntry" /> graph)</description></item>
/// <item><description><i>Sales Orders (SO301000)</i> (which corresponds to the <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph)</description></item>
/// </list>
/// </remarks>
[PXCacheName("SO Address")]
[Serializable]
public class SOAddress : 
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
  [PXUIField(DisplayName = "Address ID", Visible = false)]
  public virtual int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer">customer</see>.
  /// The field is included in <see cref="T:PX.Objects.SO.SOAddress.FK.Customer" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.Customer.bAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBDefault(typeof (SOOrder.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.CustomerID" />
  public virtual int? BAccountID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.ARAddress">customer address</see>.
  /// The field is included in <see cref="T:PX.Objects.SO.SOAddress.FK.CustomerAddress" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.ARAddress.addressID" /> field.
  /// </value>
  [PXDBInt]
  public virtual int? CustomerAddressID
  {
    get => this._CustomerAddressID;
    set => this._CustomerAddressID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.CustomerAddressID" />
  public virtual int? BAccountAddressID
  {
    get => this._CustomerAddressID;
    set => this._CustomerAddressID = value;
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the address is default.
  /// If the value is <see langword="false" />, the address has been overridden or the original address has been copied with the revision related to the moment of creation.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
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
  /// If the value of the <see cref="T:PX.Objects.SO.SOAddress.isDefaultAddress" /> field is <see langword="null" />, the value of this field is <see langword="null" />.
  /// If the value of the <see cref="T:PX.Objects.SO.SOAddress.isDefaultAddress" /> field is <see langword="true" />, the value of this field is <see langword="false" />.
  /// If the value of the <see cref="T:PX.Objects.SO.SOAddress.isDefaultAddress" /> field is <see langword="false" />, the value of this field is <see langword="true" />.
  /// </value>
  [PXBool]
  [PXUIField]
  public virtual bool? OverrideAddress
  {
    [PXDependsOnFields(new System.Type[] {typeof (SOAddress.isDefaultAddress)})] get
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

  /// <summary>
  /// Indicates if  Personally identifiable Information fields
  /// (example AddressLine1 , AddressLine2 etc.) are encrypted or not.
  /// </summary>
  [PXDBBool]
  public virtual bool? IsEncrypted { get; set; }

  /// <summary>The identifier of the revision address.</summary>
  /// <remarks>
  /// Each modification to the original address leads to the generation of a new revision of the address, which is used in the new sales order or in the overridden address in an existed sales order.
  /// </remarks>
  [PXDBInt]
  [PXDefault]
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
  /// The field is included in <see cref="T:PX.Objects.SO.SOAddress.FK.Country" /> and <see cref="T:PX.Objects.SO.SOAddress.FK.State" />.
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
  /// The identifier of the <see cref="P:PX.Objects.SO.SOAddress.State">state</see>.
  /// The field is included in <see cref="T:PX.Objects.SO.SOAddress.FK.State" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.State.stateID" /> field.
  /// </value>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (SOAddress.countryID))]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <summary>The postal code of the address.</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (SOAddress.countryID))]
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

  public static bool IsEquivalentAddress(SOAddress soAddress, POAddress poAddress)
  {
    if (soAddress != null && poAddress != null)
    {
      int? nullable1 = poAddress.BAccountID;
      int? baccountId = soAddress.BAccountID;
      if (nullable1.GetValueOrDefault() == baccountId.GetValueOrDefault() & nullable1.HasValue == baccountId.HasValue)
      {
        int? nullable2 = poAddress.BAccountAddressID;
        nullable1 = soAddress.BAccountAddressID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = poAddress.RevisionID;
          nullable2 = soAddress.RevisionID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && poAddress.AddressLine1 == soAddress.AddressLine1 && poAddress.AddressLine2 == soAddress.AddressLine2 && poAddress.AddressLine3 == soAddress.AddressLine3 && poAddress.City == soAddress.City && poAddress.CountryID == soAddress.CountryID && poAddress.State == soAddress.State && poAddress.PostalCode == soAddress.PostalCode && poAddress.Department == soAddress.Department && poAddress.SubDepartment == soAddress.SubDepartment && poAddress.StreetName == soAddress.StreetName && poAddress.BuildingNumber == soAddress.BuildingNumber && poAddress.BuildingName == soAddress.BuildingName && poAddress.Floor == soAddress.Floor && poAddress.UnitNumber == soAddress.UnitNumber && poAddress.PostBox == soAddress.PostBox && poAddress.Room == soAddress.Room && poAddress.TownLocationName == soAddress.TownLocationName && poAddress.DistrictName == soAddress.DistrictName)
          {
            Decimal? nullable3 = poAddress.Latitude;
            Decimal? latitude = soAddress.Latitude;
            if (nullable3.GetValueOrDefault() == latitude.GetValueOrDefault() & nullable3.HasValue == latitude.HasValue)
            {
              Decimal? longitude = poAddress.Longitude;
              nullable3 = soAddress.Longitude;
              return longitude.GetValueOrDefault() == nullable3.GetValueOrDefault() & longitude.HasValue == nullable3.HasValue;
            }
          }
        }
      }
    }
    return false;
  }

  public static SOAddress CreateFromPOAddress(POAddress poAddress)
  {
    return new SOAddress()
    {
      CustomerID = poAddress.BAccountID,
      CustomerAddressID = poAddress.BAccountAddressID,
      RevisionID = poAddress.RevisionID,
      IsDefaultAddress = poAddress.IsDefaultAddress,
      AddressLine1 = poAddress.AddressLine1,
      AddressLine2 = poAddress.AddressLine2,
      AddressLine3 = poAddress.AddressLine3,
      City = poAddress.City,
      CountryID = poAddress.CountryID,
      State = poAddress.State,
      PostalCode = poAddress.PostalCode,
      Department = poAddress.Department,
      SubDepartment = poAddress.SubDepartment,
      StreetName = poAddress.StreetName,
      BuildingNumber = poAddress.BuildingNumber,
      BuildingName = poAddress.BuildingName,
      Floor = poAddress.Floor,
      UnitNumber = poAddress.UnitNumber,
      PostBox = poAddress.PostBox,
      Room = poAddress.Room,
      TownLocationName = poAddress.TownLocationName,
      DistrictName = poAddress.DistrictName
    };
  }

  public class PK : PrimaryKeyOf<SOAddress>.By<SOAddress.addressID>
  {
    public static SOAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (SOAddress) PrimaryKeyOf<SOAddress>.By<SOAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOAddress>.By<SOAddress.customerID>
    {
    }

    public class CustomerAddress : 
      PrimaryKeyOf<ARAddress>.By<ARAddress.addressID>.ForeignKeyOf<SOAddress>.By<SOAddress.customerAddressID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<SOAddress>.By<SOAddress.countryID>
    {
    }

    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<SOAddress>.By<SOAddress.countryID, SOAddress.state>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.AddressID" />
  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAddress.addressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.CustomerID" />
  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAddress.customerID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.CustomerAddressID" />
  public abstract class customerAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAddress.customerAddressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.IsDefaultAddress" />
  public abstract class isDefaultAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAddress.isDefaultAddress>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.OverrideAddress" />
  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAddress.overrideAddress>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.IsEncrypted" />
  public abstract class isEncrypted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAddress.isEncrypted>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.RevisionID" />
  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAddress.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.AddressLine1" />
  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.addressLine1>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.AddressLine2" />
  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.addressLine2>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.AddressLine3" />
  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.addressLine3>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.City" />
  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.city>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.CountryID" />
  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.countryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.State" />
  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.state>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.PostalCode" />
  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.department>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOAddress.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAddress.districtName>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.NoteID" />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOAddress.noteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.IsValidated" />
  public abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAddress.isValidated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOAddress.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOAddress.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOAddress.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOAddress.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOAddress.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOAddress.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOAddress.lastModifiedDateTime>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.Latitude" />
  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAddress.latitude>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOAddress.Longitude" />
  public abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAddress.longitude>
  {
  }
}
