// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CompanyBAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXProjection(typeof (Select5<PX.Objects.GL.DAC.Organization, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.GL.DAC.Organization.bAccountID>>, InnerJoin<Address, On<Address.bAccountID, Equal<BAccountR.bAccountID>, And<Address.addressID, Equal<BAccountR.defAddressID>>>, InnerJoin<Contact, On<Contact.bAccountID, Equal<BAccountR.bAccountID>, And<Contact.contactID, Equal<BAccountR.defContactID>>>>>>>, Where<PX.Objects.GL.DAC.Organization.active, Equal<True>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID, Current<AccessInfo.branchID>>>>, Aggregate<GroupBy<PX.Objects.GL.DAC.Organization.organizationID>>>))]
[PXCacheName("Company with Busienss Account")]
[Serializable]
public class CompanyBAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AddressLine1;
  protected string _AddressLine2;
  protected string _AddressLine3;
  protected string _City;
  protected string _CountryID;
  protected string _State;
  protected string _PostalCode;
  protected string _Title;
  protected string _Salutation;
  protected string _FullName;
  protected string _EMail;
  protected string _Phone1;

  [PXDBString(60, IsUnicode = true, BqlField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Company")]
  [PXDefault]
  public virtual string AcctName { get; set; }

  [PXDefault]
  [Organization(true, BqlField = typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  public virtual int? OrganizationID { get; set; }

  [PXDefault]
  [PXDBInt(BqlField = typeof (BAccountR.bAccountID))]
  public virtual int? BAccountID { get; set; }

  [PXDBString(IsUnicode = true, BqlField = typeof (PX.Objects.GL.DAC.Organization.logoName))]
  [PXDefault]
  [PXUIField(DisplayName = "Logo")]
  public virtual string LogoName { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.addressLine1))]
  [PXUIField(DisplayName = "Address Line 1")]
  public virtual string AddressLine1
  {
    get => this._AddressLine1;
    set => this._AddressLine1 = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  public virtual string AddressLine2
  {
    get => this._AddressLine2;
    set => this._AddressLine2 = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.addressLine3))]
  [PXUIField(DisplayName = "Address Line 3")]
  public virtual string AddressLine3
  {
    get => this._AddressLine3;
    set => this._AddressLine3 = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.city))]
  [PXUIField(DisplayName = "City")]
  public virtual string City
  {
    get => this._City;
    set => this._City = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (Address.countryID))]
  [PXUIField(DisplayName = "Country")]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Address.state))]
  [PXUIField(DisplayName = "State")]
  public virtual string State
  {
    get => this._State;
    set => this._State = value;
  }

  [PXDBString(20, BqlField = typeof (Address.postalCode))]
  [PXUIField(DisplayName = "Postal Code")]
  public virtual string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.SubDepartment" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (Address.subDepartment))]
  [PXUIField(DisplayName = "Subdepartment")]
  [PXPersonalDataField]
  public virtual string SubDepartment { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.StreetName" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (Address.streetName))]
  [PXUIField(DisplayName = "Street Name")]
  [PXPersonalDataField]
  public virtual string StreetName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingNumber" />
  [PXDBString(16 /*0x10*/, IsUnicode = true, BqlField = typeof (Address.buildingNumber))]
  [PXUIField(DisplayName = "Building Number")]
  [PXPersonalDataField]
  public virtual string BuildingNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingName" />
  [PXDBString(35, IsUnicode = true, BqlField = typeof (Address.buildingName))]
  [PXUIField(DisplayName = "Building Name")]
  [PXPersonalDataField]
  public virtual string BuildingName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Floor" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (Address.floor))]
  [PXUIField(DisplayName = "Floor")]
  [PXPersonalDataField]
  public virtual string Floor { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.UnitNumber" />
  [PXDBString(16 /*0x10*/, IsUnicode = true, BqlField = typeof (Address.unitNumber))]
  [PXUIField(DisplayName = "Unit Number")]
  [PXPersonalDataField]
  public virtual string UnitNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostBox" />
  [PXDBString(16 /*0x10*/, IsUnicode = true, BqlField = typeof (Address.postBox))]
  [PXUIField(DisplayName = "Post Box")]
  [PXPersonalDataField]
  public virtual string PostBox { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Room" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (Address.room))]
  [PXUIField(DisplayName = "Room")]
  [PXPersonalDataField]
  public virtual string Room { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.TownLocationName" />
  [PXDBString(35, IsUnicode = true, BqlField = typeof (Address.townLocationName))]
  [PXUIField(DisplayName = "Town Location Name")]
  [PXPersonalDataField]
  public virtual string TownLocationName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.DistrictName" />
  [PXDBString(35, IsUnicode = true, BqlField = typeof (Address.districtName))]
  [PXUIField(DisplayName = "District Name")]
  [PXPersonalDataField]
  public virtual string DistrictName { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (Contact.title))]
  [Titles]
  [PXUIField(DisplayName = "Title")]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (Contact.salutation))]
  [PXUIField(DisplayName = "Position")]
  public virtual string Salutation
  {
    get => this._Salutation;
    set => this._Salutation = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (Contact.fullName))]
  [PXUIField(DisplayName = "Business Name")]
  public virtual string FullName
  {
    get => this._FullName;
    set => this._FullName = value;
  }

  [PXDBEmail(BqlField = typeof (Contact.eMail))]
  [PXUIField(DisplayName = "Email")]
  public virtual string EMail
  {
    get => this._EMail != null ? this._EMail.Trim() : (string) null;
    set => this._EMail = value;
  }

  [PhoneValidation]
  [PXDBString(50, BqlField = typeof (Contact.phone1))]
  [PXUIField(DisplayName = "Phone 1")]
  public virtual string Phone1
  {
    get => this._Phone1;
    set => this._Phone1 = value;
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.acctName>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CompanyBAccount.organizationID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CompanyBAccount.bAccountID>
  {
  }

  public abstract class logoName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.logoName>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.addressLine2>
  {
  }

  public abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.postalCode>
  {
  }

  public abstract class subDepartment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.streetName>
  {
  }

  public abstract class buildingNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.buildingNumber>
  {
  }

  public abstract class buildingName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.townLocationName>
  {
  }

  public abstract class districtName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CompanyBAccount.districtName>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.title>
  {
  }

  public abstract class salutation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.salutation>
  {
  }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.fullName>
  {
  }

  public abstract class eMail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.eMail>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyBAccount.phone1>
  {
  }
}
