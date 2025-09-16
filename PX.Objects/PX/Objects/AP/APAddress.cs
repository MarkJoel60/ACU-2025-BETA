// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAddress
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
namespace PX.Objects.AP;

/// <summary>A payment-specific remittance address.</summary>
[PXCacheName("AP Address")]
[Serializable]
public class APAddress : 
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
  protected int? _VendorID;
  protected int? _VendorAddressID;
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
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Address ID", Visible = false)]
  public virtual int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (APRegister.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  public virtual int? BAccountID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  public virtual int? VendorAddressID
  {
    get => this._VendorAddressID;
    set => this._VendorAddressID = value;
  }

  public virtual int? BAccountAddressID
  {
    get => this._VendorAddressID;
    set => this._VendorAddressID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Vendor Default", Visibility = PXUIVisibility.Visible)]
  [PXDefault(true)]
  [PXExtraKey]
  public virtual bool? IsDefaultAddress
  {
    get => this._IsDefaultAddress;
    set => this._IsDefaultAddress = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Override Address", Visibility = PXUIVisibility.Visible)]
  public virtual bool? OverrideAddress
  {
    [PXDependsOnFields(new System.Type[] {typeof (APAddress.isDefaultAddress)})] get
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

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 1", Visibility = PXUIVisibility.SelectorVisible)]
  [PXPersonalDataField]
  public virtual string AddressLine1
  {
    get => this._AddressLine1;
    set => this._AddressLine1 = value;
  }

  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXPersonalDataField]
  public virtual string AddressLine2
  {
    get => this._AddressLine2;
    set => this._AddressLine2 = value;
  }

  [PXDBString(70, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 3")]
  [PXPersonalDataField]
  public virtual string AddressLine3
  {
    get => this._AddressLine3;
    set => this._AddressLine3 = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "City", Visibility = PXUIVisibility.SelectorVisible)]
  [PXPersonalDataField]
  public virtual string City
  {
    get => this._City;
    set => this._City = value;
  }

  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (APAddress.countryID))]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (APAddress.countryID))]
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

  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBBool]
  [ValidatedAddress]
  [PXUIField(DisplayName = "Validated", Enabled = false, FieldClass = "Validate Address")]
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
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

  public class PK : PrimaryKeyOf<APAddress>.By<APAddress.addressID>
  {
    public static APAddress Find(PXGraph graph, int? addressID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APAddress>.By<APAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APAddress>.By<APAddress.vendorID>
    {
    }

    public class VendorAddress : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<APAddress>.By<APAddress.vendorAddressID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<APAddress>.By<APAddress.countryID>
    {
    }
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAddress.addressID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAddress.vendorID>
  {
  }

  public abstract class vendorAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAddress.vendorAddressID>
  {
  }

  public abstract class isDefaultAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAddress.isDefaultAddress>
  {
  }

  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAddress.overrideAddress>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAddress.revisionID>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.addressLine2>
  {
  }

  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.department>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddress.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddress.districtName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAddress.noteID>
  {
  }

  public abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAddress.isValidated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APAddress.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAddress.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddress.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APAddress.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAddress.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddress.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APAddress.lastModifiedDateTime>
  {
  }

  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAddress.latitude>
  {
  }

  public abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAddress.longitude>
  {
  }
}
