// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShippingAddress
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
/// The records of this type are created and edited on the following forms:
/// <list type="bullet">
/// <item><description><i>Invoices (SO303000)</i> (which corresponds to the <see cref="T:PX.Objects.SO.SOInvoiceEntry" /> graph)</description></item>
/// <item><description><i>Sales Orders (SO301000)</i> (which corresponds to the <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph)</description></item>
/// </list>
/// </remarks>
[PXBreakInheritance]
[PXCacheName("Shipping Address")]
[Serializable]
public class SOShippingAddress : SOAddress
{
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
  [PX.Objects.CR.State(typeof (SOShippingAddress.countryID))]
  public override string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.postalCode" />
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXPersonalDataField]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (SOShippingAddress.countryID))]
  public override string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  public new class PK : PrimaryKeyOf<SOShippingAddress>.By<SOShippingAddress.addressID>
  {
    public static SOShippingAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (SOShippingAddress) PrimaryKeyOf<SOShippingAddress>.By<SOShippingAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOShippingAddress>.By<SOShippingAddress.customerID>
    {
    }

    public class CustomerAddress : 
      PrimaryKeyOf<ARAddress>.By<ARAddress.addressID>.ForeignKeyOf<SOShippingAddress>.By<SOShippingAddress.customerAddressID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<SOShippingAddress>.By<SOShippingAddress.countryID>
    {
    }

    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<SOShippingAddress>.By<SOShippingAddress.countryID, SOShippingAddress.state>
    {
    }
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.addressID" />
  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShippingAddress.addressID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.customerID" />
  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShippingAddress.customerID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.customerAddressID" />
  public new abstract class customerAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShippingAddress.customerAddressID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.isDefaultAddress" />
  public new abstract class isDefaultAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShippingAddress.isDefaultAddress>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.overrideAddress" />
  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShippingAddress.overrideAddress>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.revisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShippingAddress.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShippingAddress.CountryID" />
  public new abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShippingAddress.countryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShippingAddress.State" />
  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShippingAddress.state>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShippingAddress.PostalCode" />
  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShippingAddress.postalCode>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.isValidated" />
  public new abstract class isValidated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShippingAddress.isValidated>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.latitude" />
  public new abstract class latitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShippingAddress.latitude>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOAddress.longitude" />
  public new abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShippingAddress.longitude>
  {
  }
}
