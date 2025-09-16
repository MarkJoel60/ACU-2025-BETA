// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PORemitAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>
/// Represents an address of the vendor location that contains the information related to the vendor to supply the ordered goods.
/// </summary>
/// <remarks>
/// The records of this type are created and edited on the <i>Purchase Orders (PO301000)</i> form
/// (which corresponds to the <see cref="T:PX.Objects.PO.POOrderEntry" /> graph).
/// </remarks>
[PXCacheName("PO Remittance Address")]
[Serializable]
public class PORemitAddress : POAddress
{
  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.bAccountID" />
  [PXDBInt]
  [PXDefault]
  public override int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.countryID" />
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

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.state" />
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (PORemitAddress.countryID))]
  public override string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.postalCode" />
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (PORemitAddress.countryID))]
  [PXPersonalDataField]
  public override string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  public new class PK : PrimaryKeyOf<PORemitAddress>.By<PORemitAddress.addressID>
  {
    public static PORemitAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (PORemitAddress) PrimaryKeyOf<PORemitAddress>.By<PORemitAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.addressID" />
  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PORemitAddress.addressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.PORemitAddress.BAccountID" />
  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PORemitAddress.bAccountID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.bAccountAddressID" />
  public new abstract class bAccountAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PORemitAddress.bAccountAddressID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.isDefaultAddress" />
  public new abstract class isDefaultAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PORemitAddress.isDefaultAddress>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.overrideAddress" />
  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PORemitAddress.overrideAddress>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.revisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PORemitAddress.revisionID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.PORemitAddress.CountryID" />
  public new abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PORemitAddress.countryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.PORemitAddress.State" />
  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PORemitAddress.state>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.PORemitAddress.PostalCode" />
  public new abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PORemitAddress.postalCode>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POAddress.isValidated" />
  public new abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PORemitAddress.isValidated>
  {
  }
}
