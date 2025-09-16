// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// Represents an address that contains the information related to the shipping of the ordered items.
/// </summary>
/// <remarks>
/// The records of this type are created and edited on the <i>Shipments (SO302000)</i> form
/// (which corresponds to the <see cref="T:PX.Objects.SO.SOShipmentEntry" /> graph).
/// </remarks>
[PXBreakInheritance]
[PXCacheName("Shipment Address")]
[Serializable]
public class SOShipmentAddress : SOAddress
{
  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.customerID" />
  [PXDBInt]
  [PXDBDefault(typeof (SOShipment.customerID))]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.countryID" />
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public override 
  #nullable disable
  string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.state" />
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (SOShipmentAddress.countryID))]
  public override string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShipmentAddress.PostalCode" />
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXPersonalDataField]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (SOShipmentAddress.countryID))]
  public override string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  public new class PK : PrimaryKeyOf<SOShipmentAddress>.By<SOShipmentAddress.addressID>
  {
    public static SOShipmentAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (SOShipmentAddress) PrimaryKeyOf<SOShipmentAddress>.By<SOShipmentAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOShipmentAddress>.By<SOShipmentAddress.customerID>
    {
    }

    public class CustomerAddress : 
      PrimaryKeyOf<ARAddress>.By<ARAddress.addressID>.ForeignKeyOf<SOShipmentAddress>.By<SOShipmentAddress.customerAddressID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<SOShipmentAddress>.By<SOShipmentAddress.countryID>
    {
    }

    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<SOShipmentAddress>.By<SOShipmentAddress.countryID, SOShipmentAddress.state>
    {
    }
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.addressID" />
  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentAddress.addressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShipmentAddress.CustomerID" />
  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentAddress.customerID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.customerAddressID" />
  public new abstract class customerAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentAddress.customerAddressID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.isDefaultAddress" />
  public new abstract class isDefaultAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentAddress.isDefaultAddress>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.overrideAddress" />
  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentAddress.overrideAddress>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.revisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentAddress.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShipmentAddress.CountryID" />
  public new abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentAddress.countryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShipmentAddress.State" />
  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentAddress.state>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.postalCode" />
  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipmentAddress.postalCode>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.isValidated" />
  public new abstract class isValidated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentAddress.isValidated>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.latitude" />
  public new abstract class latitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipmentAddress.latitude>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.longitude" />
  public new abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipmentAddress.longitude>
  {
  }
}
