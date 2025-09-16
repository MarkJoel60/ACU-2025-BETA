// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRShippingAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Represents a shipping address that a user specifies in a document such as
/// <see cref="P:PX.Objects.CR.CROpportunity.OpportunityAddressID"> an opportunity's shipping address</see>.
/// A <see cref="T:PX.Objects.CR.CRShippingAddress" /> record is a copy of the default location <see cref="T:PX.Objects.CR.Address" /> of the business account
/// and can be used to override the address specified in the document.
/// A <see cref="T:PX.Objects.CR.CRShippingAddress" /> record is independent from changes to the original <see cref="T:PX.Objects.CR.Address" /> record.
/// </summary>
[PXCacheName("Shipping Address")]
[Serializable]
public class CRShippingAddress : CRAddress
{
  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.IsDefaultAddress" />
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public override bool? IsDefaultAddress
  {
    get => this._IsDefaultAddress;
    set => this._IsDefaultAddress = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.OverrideAddress" />
  [PXBool]
  [PXUIField]
  public override bool? OverrideAddress
  {
    [PXDependsOnFields(new System.Type[] {typeof (CRShippingAddress.isDefaultAddress)})] get
    {
      return base.OverrideAddress;
    }
    set => base.OverrideAddress = value;
  }

  /// <summary>Primary key</summary>
  public new class PK : PrimaryKeyOf<
  #nullable disable
  CRShippingAddress>.By<CRShippingAddress.addressID>
  {
    public static CRShippingAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (CRShippingAddress) PrimaryKeyOf<CRShippingAddress>.By<CRShippingAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public new static class FK
  {
    /// <summary>Business Account</summary>
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRShippingAddress>.By<CRShippingAddress.bAccountID>
    {
    }

    /// <summary>Business Account Address</summary>
    public class BusinessAccountAddress : 
      PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<CRShippingAddress>.By<CRShippingAddress.bAccountAddressID>
    {
    }

    /// <summary>Country</summary>
    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<CRShippingAddress>.By<CRShippingAddress.countryID>
    {
    }

    /// <summary>State</summary>
    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<CRShippingAddress>.By<CRShippingAddress.countryID, CRShippingAddress.state>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressID" />
  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRShippingAddress.addressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BAccountID" />
  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRShippingAddress.bAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.BAccountID" />
  public new abstract class bAccountAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRShippingAddress.bAccountAddressID>
  {
  }

  public new abstract class isDefaultAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRShippingAddress.isDefaultAddress>
  {
  }

  public new abstract class isValidated : IBqlField, IBqlOperand
  {
  }

  public new abstract class overrideAddress : IBqlField, IBqlOperand
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.RevisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRShippingAddress.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.CountryID" />
  public new abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRShippingAddress.countryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.State" />
  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRShippingAddress.state>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.PostalCode" />
  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRShippingAddress.postalCode>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Latitude" />
  public new abstract class latitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRShippingAddress.latitude>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.Longitude" />
  public new abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRShippingAddress.longitude>
  {
  }
}
