// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMSiteAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Represents a project site address. The records of this type are created and edited on the Projects (PM301000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProjectEntry" />
/// graph). The DAC is based on the <see cref="T:PX.Objects.PM.PMAddress" /> DAC.</summary>
[PXCacheName("PM Address")]
[PXBreakInheritance]
[Serializable]
public class PMSiteAddress : PMAddress
{
  /// <summary>The revision ID of the original record.</summary>
  [PXDBInt]
  [PXDefault(0)]
  [AddressRevisionID]
  public override int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  /// <summary>The first address line.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 1")]
  [PXPersonalDataField]
  public override 
  #nullable disable
  string AddressLine1
  {
    get => this._AddressLine1;
    set => this._AddressLine1 = value;
  }

  /// <summary>The second address line.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 2")]
  [PXPersonalDataField]
  public override string AddressLine2
  {
    get => this._AddressLine2;
    set => this._AddressLine2 = value;
  }

  /// <summary>The name of the city or inhabited locality.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "City")]
  [PXPersonalDataField]
  public override string City { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Country" /> record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Country.CountryID" /> field.
  /// </value>
  [PXDBString(2, IsUnicode = true)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public override string CountryID { get; set; }

  /// <summary>The name of the state.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (PMSiteAddress.countryID))]
  public override string State { get; set; }

  /// <summary>The postal code.</summary>
  [PXDBString(20, IsUnicode = false)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (PMSiteAddress.countryID))]
  [PXPersonalDataField]
  public override string PostalCode { get; set; }

  /// <summary>The latitude of the address.</summary>
  [PXDBDecimal(6, MaxValue = 90.0, MinValue = -90.0)]
  [PXUIField(DisplayName = "Latitude")]
  public override Decimal? Latitude { get; set; }

  /// <summary>The longitude of the address.</summary>
  [PXDBDecimal(6, MaxValue = 180.0, MinValue = -180.0)]
  [PXUIField(DisplayName = "Longitude")]
  public override Decimal? Longitude { get; set; }

  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMSiteAddress.addressID>
  {
  }

  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMSiteAddress.overrideAddress>
  {
  }

  public new abstract class isDefaultBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMSiteAddress.isDefaultBillAddress>
  {
  }

  public new abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMSiteAddress.isValidated>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMSiteAddress.revisionID>
  {
  }

  public new abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSiteAddress.addressLine1>
  {
  }

  public new abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSiteAddress.addressLine2>
  {
  }

  public new abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMSiteAddress.city>
  {
  }

  public new abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMSiteAddress.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMSiteAddress.state>
  {
  }

  public new abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMSiteAddress.postalCode>
  {
  }

  public new abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMSiteAddress.latitude>
  {
  }

  public new abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMSiteAddress.longitude>
  {
  }
}
