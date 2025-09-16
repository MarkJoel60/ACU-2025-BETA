// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DAC.Projections.SiteInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.PO.DAC.Projections;

/// <summary>
/// A projection that is used in the Purchase Receipt (PO646000) report to display information about the warehouse.
/// </summary>
[PXCacheName("Site Info")]
[PXProjection(typeof (Select2<INSite, InnerJoin<PX.Objects.CR.Address, On<INSite.FK.Address>, InnerJoin<PX.Objects.CR.Contact, On<INSite.FK.Contact>>>, Where<BqlOperand<INSite.siteID, IBqlInt>.IsNotEqual<SiteAnyAttribute.transitSiteID>>>))]
public class SiteInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.IN.INSite.SiteID" />
  [PXDBInt(BqlField = typeof (INSite.siteID))]
  public virtual int? SiteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INSite.Descr" />
  [PXDBString(60, IsUnicode = true, BqlField = typeof (INSite.descr))]
  [PXUIField]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine1" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine1))]
  [PXUIField]
  public virtual string AddressLine1 { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine2" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  public virtual string AddressLine2 { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine3" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.addressLine3))]
  [PXUIField(DisplayName = "Address Line 3")]
  public virtual string AddressLine3 { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.City" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.city))]
  [PXUIField]
  public virtual string City { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.CountryID" />
  [PXDBString(100, BqlField = typeof (PX.Objects.CR.Address.countryID))]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.State" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.state))]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (SiteInfo.countryID))]
  public virtual string State { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostalCode" />
  [PXDBString(20, BqlField = typeof (PX.Objects.CR.Address.postalCode))]
  [PXUIField(DisplayName = "Postal Code")]
  public virtual string PostalCode { get; set; }

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

  /// <inheritdoc cref="P:PX.Objects.CR.Contact.Phone1" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.phone1))]
  [PXUIField]
  [PXPhone]
  public virtual string Phone1 { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Contact.Fax" />
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.fax))]
  [PXUIField(DisplayName = "Fax")]
  public virtual string Fax { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Contact.Attention" />
  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Contact.attention))]
  [PXUIField(DisplayName = "Attention")]
  public virtual string Attention { get; set; }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteInfo.siteID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.descr>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.addressLine2>
  {
  }

  public abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.postalCode>
  {
  }

  public abstract class subDepartment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.streetName>
  {
  }

  public abstract class buildingNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.buildingNumber>
  {
  }

  public abstract class buildingName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteInfo.townLocationName>
  {
  }

  public abstract class districtName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.districtName>
  {
  }

  public abstract class phone1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.phone1>
  {
  }

  public abstract class fax : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.fax>
  {
  }

  public abstract class attention : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteInfo.attention>
  {
  }
}
