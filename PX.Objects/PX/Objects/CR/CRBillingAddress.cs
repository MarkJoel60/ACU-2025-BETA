// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRBillingAddress
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
/// Represents a billing address that is specified in the document, such as
/// <see cref="P:PX.Objects.CR.CROpportunity.OpportunityAddressID"> an opportunity's billing address</see>.
/// A <see cref="T:PX.Objects.CR.CRBillingAddress" /> record is as a copy of the default location <see cref="T:PX.Objects.CR.Address" /> of the business account
/// and can be used to override the address specified in the document.
/// A <see cref="T:PX.Objects.CR.CRBillingAddress" /> record is independent from changes to the original <see cref="T:PX.Objects.CR.Address" /> record.
/// </summary>
[PXCacheName("Bill-To Address")]
[Serializable]
public class CRBillingAddress : CRAddress
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
    [PXDependsOnFields(new System.Type[] {typeof (CRBillingAddress.isDefaultAddress)})] get
    {
      return base.OverrideAddress;
    }
    set => base.OverrideAddress = value;
  }

  /// <summary>Primary key</summary>
  public new class PK : PrimaryKeyOf<
  #nullable disable
  CRBillingAddress>.By<CRBillingAddress.addressID>
  {
    public static CRBillingAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (CRBillingAddress) PrimaryKeyOf<CRBillingAddress>.By<CRBillingAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public new static class FK
  {
    /// <summary>Business Account</summary>
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<CRBillingAddress>.By<CRBillingAddress.bAccountID>
    {
    }

    /// <summary>Business Account Address</summary>
    public class BusinessAccountAddress : 
      PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<CRBillingAddress>.By<CRBillingAddress.bAccountAddressID>
    {
    }

    /// <summary>Country</summary>
    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<CRBillingAddress>.By<CRBillingAddress.countryID>
    {
    }

    /// <summary>State</summary>
    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<CRBillingAddress>.By<CRBillingAddress.countryID, CRBillingAddress.state>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.AddressID" />
  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRBillingAddress.addressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Address.BAccountID" />
  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRBillingAddress.bAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.BAccountID" />
  public new abstract class bAccountAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRBillingAddress.bAccountAddressID>
  {
  }

  public new abstract class isDefaultAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRBillingAddress.isDefaultAddress>
  {
  }

  public new abstract class isValidated : IBqlField, IBqlOperand
  {
  }

  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRBillingAddress.overrideAddress>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.RevisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRBillingAddress.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.CountryID" />
  public new abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRBillingAddress.countryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.State" />
  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRBillingAddress.state>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.CRAddress.PostalCode" />
  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRBillingAddress.postalCode>
  {
  }
}
