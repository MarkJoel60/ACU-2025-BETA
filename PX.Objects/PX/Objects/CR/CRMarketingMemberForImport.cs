// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingMemberForImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[PXProjection(typeof (Select2<Contact, InnerJoin<PX.Objects.CR.Address, On<True, Equal<False>>, InnerJoin<PX.Objects.CR.Standalone.CRLead, On<True, Equal<False>>>>>), Persistent = false)]
[PXBreakInheritance]
[Serializable]
public class CRMarketingMemberForImport : Contact, IAddressLocation, IAddressBase, IAddressISO20022
{
  [PXDBInt]
  [PXUIField(DisplayName = "Marketing List ID")]
  [PXSelector(typeof (Search<CRMarketingList.marketingListID, Where<CRMarketingList.type, Equal<CRMarketingList.type.@static>>>), DescriptionField = typeof (CRMarketingList.mailListCode))]
  public virtual int? LinkMarketingListID { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Campaign ID")]
  [PXSelector(typeof (CRCampaign.campaignID), DescriptionField = typeof (CRCampaign.campaignName))]
  public virtual 
  #nullable disable
  string LinkCampaignID { get; set; }

  [PXDBIdentity(IsKey = true, BqlField = typeof (CRMarketingMemberForImport.contactID))]
  [PXUIField]
  [ContactSelector(true, new System.Type[] {typeof (ContactTypesAttribute.person), typeof (ContactTypesAttribute.employee)})]
  public override int? ContactID { get; set; }

  public virtual int? ExistingContactID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("PN")]
  [ContactTypes]
  [PXUIField(DisplayName = "Type")]
  public override string ContactType { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.CR.Address.addressType))]
  [PXDefault("BS")]
  [PX.Objects.CR.Address.AddressTypes.List]
  [PXUIField]
  public virtual string AddressType { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine1))]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string AddressLine1 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXPersonalDataField]
  public virtual string AddressLine2 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine3))]
  [PXUIField(DisplayName = "Address Line 3")]
  [PXPersonalDataField]
  public virtual string AddressLine3 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.city))]
  [PXUIField]
  [PXPersonalDataField]
  public virtual string City { get; set; }

  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100, BqlField = typeof (PX.Objects.CR.Address.countryID))]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.state))]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (CRMarketingMemberForImport.countryID))]
  public virtual string State { get; set; }

  [PXDBString(20, BqlField = typeof (PX.Objects.CR.Address.postalCode))]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (CRMarketingMemberForImport.countryID))]
  [PXPersonalDataField]
  public virtual string PostalCode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Department" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.department))]
  [PXUIField(DisplayName = "Department")]
  [PXPersonalDataField]
  public virtual string Department { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.SubDepartment" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.subDepartment))]
  [PXUIField(DisplayName = "Subdepartment")]
  [PXPersonalDataField]
  public virtual string SubDepartment { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.StreetName" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.streetName))]
  [PXUIField(DisplayName = "Street Name")]
  [PXPersonalDataField]
  public virtual string StreetName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingNumber" />
  [PXDBString(16 /*0x10*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.buildingNumber))]
  [PXUIField(DisplayName = "Building Number")]
  [PXPersonalDataField]
  public virtual string BuildingNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingName" />
  [PXDBString(35, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.buildingName))]
  [PXUIField(DisplayName = "Building Name")]
  [PXPersonalDataField]
  public virtual string BuildingName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Floor" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.floor))]
  [PXUIField(DisplayName = "Floor")]
  [PXPersonalDataField]
  public virtual string Floor { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.UnitNumber" />
  [PXDBString(16 /*0x10*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.unitNumber))]
  [PXUIField(DisplayName = "Unit Number")]
  [PXPersonalDataField]
  public virtual string UnitNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostBox" />
  [PXDBString(16 /*0x10*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.postBox))]
  [PXUIField(DisplayName = "Post Box")]
  [PXPersonalDataField]
  public virtual string PostBox { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Room" />
  [PXDBString(70, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.room))]
  [PXUIField(DisplayName = "Room")]
  [PXPersonalDataField]
  public virtual string Room { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.TownLocationName" />
  [PXDBString(35, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.townLocationName))]
  [PXUIField(DisplayName = "Town Location Name")]
  [PXPersonalDataField]
  public virtual string TownLocationName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.DistrictName" />
  [PXDBString(35, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.districtName))]
  [PXUIField(DisplayName = "District Name")]
  [PXPersonalDataField]
  public virtual string DistrictName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Latitude" />
  [PXDBDecimal(6, BqlField = typeof (PX.Objects.CR.Address.latitude))]
  [PXUIField(DisplayName = "Latitude", Visible = false)]
  public virtual Decimal? Latitude { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Longitude" />
  [PXDBDecimal(6, BqlField = typeof (PX.Objects.CR.Address.longitude))]
  [PXUIField(DisplayName = "Longitude", Visible = false)]
  public virtual Decimal? Longitude { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.CRLead.description))]
  [PXUIField]
  public virtual string Description { get; set; }

  public abstract class linkMarketingListID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRMarketingMemberForImport.linkMarketingListID>
  {
  }

  public abstract class linkCampaignID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.linkCampaignID>
  {
  }

  public new abstract class contactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRMarketingMemberForImport.contactID>
  {
  }

  public new abstract class contactType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.contactType>
  {
  }

  public abstract class addressType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.addressType>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.addressLine2>
  {
  }

  public abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingMemberForImport.city>
  {
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingMemberForImport.state>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.postalCode>
  {
  }

  public abstract class department : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.department>
  {
  }

  public abstract class subDepartment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.subDepartment>
  {
  }

  public abstract class streetName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.streetName>
  {
  }

  public abstract class buildingNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.buildingNumber>
  {
  }

  public abstract class buildingName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingMemberForImport.floor>
  {
  }

  public abstract class unitNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.unitNumber>
  {
  }

  public abstract class postBox : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRMarketingMemberForImport.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.townLocationName>
  {
  }

  public abstract class districtName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.districtName>
  {
  }

  public abstract class latitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRMarketingMemberForImport.latitude>
  {
  }

  public abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRMarketingMemberForImport.longitude>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRMarketingMemberForImport.description>
  {
  }
}
