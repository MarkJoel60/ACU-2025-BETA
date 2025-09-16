// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CROpportunityContactAddress.DocumentAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.Extensions.CROpportunityContactAddress;

public class DocumentAddress : PXMappedCacheExtension
{
  public virtual bool? IsDefaultAddress { get; set; }

  public virtual bool? OverrideAddress { get; set; }

  public virtual bool? IsValidated { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine1" />
  public virtual 
  #nullable disable
  string AddressLine1 { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine2" />
  public virtual string AddressLine2 { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressLine3" />
  public virtual string AddressLine3 { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.City" />
  public virtual string City { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.CountryID" />
  public virtual string CountryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.State" />
  public virtual string State { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostalCode" />
  public virtual string PostalCode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Department" />
  public virtual string Department { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.SubDepartment" />
  public virtual string SubDepartment { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.StreetName" />
  public virtual string StreetName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingNumber" />
  public virtual string BuildingNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BuildingName" />
  public virtual string BuildingName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Floor" />
  public virtual string Floor { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.UnitNumber" />
  public virtual string UnitNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostBox" />
  public virtual string PostBox { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Room" />
  public virtual string Room { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.TownLocationName" />
  public virtual string TownLocationName { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.DistrictName" />
  public virtual string DistrictName { get; set; }

  public abstract class isDefaultAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DocumentAddress.isDefaultAddress>
  {
  }

  public abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DocumentAddress.overrideAddress>
  {
  }

  public abstract class isValidated : IBqlField, IBqlOperand
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.addressLine2>
  {
  }

  public abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.postalCode>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.department>
  {
  }

  public abstract class subDepartment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.subDepartment>
  {
  }

  public abstract class streetName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.streetName>
  {
  }

  public abstract class buildingNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.buildingNumber>
  {
  }

  public abstract class buildingName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.buildingName>
  {
  }

  public abstract class floor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.floor>
  {
  }

  public abstract class unitNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.unitNumber>
  {
  }

  public abstract class postBox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.postBox>
  {
  }

  public abstract class room : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DocumentAddress.room>
  {
  }

  public abstract class townLocationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.townLocationName>
  {
  }

  public abstract class districtName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DocumentAddress.districtName>
  {
  }
}
