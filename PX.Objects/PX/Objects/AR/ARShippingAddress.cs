// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARShippingAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("AR Address")]
[PXBreakInheritance]
[Serializable]
public class ARShippingAddress : ARAddress
{
  /// <summary>The name of the state.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (ARShippingAddress.countryID))]
  public override 
  #nullable disable
  string State
  {
    get => this._State;
    set => this._State = value;
  }

  /// <summary>The postal code.</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (ARShippingAddress.countryID))]
  [PXPersonalDataField]
  public override string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARShippingAddress.addressID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARShippingAddress.customerID>
  {
  }

  public new abstract class customerAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARShippingAddress.customerAddressID>
  {
  }

  public new abstract class isDefaultBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARShippingAddress.isDefaultBillAddress>
  {
  }

  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARShippingAddress.overrideAddress>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARShippingAddress.revisionID>
  {
  }

  public new abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingAddress.addressLine1>
  {
  }

  public new abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingAddress.addressLine2>
  {
  }

  public new abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingAddress.addressLine3>
  {
  }

  public new abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingAddress.city>
  {
  }

  public new abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingAddress.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARShippingAddress.state>
  {
  }

  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARShippingAddress.postalCode>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARShippingAddress.noteID>
  {
  }

  public new abstract class isValidated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARShippingAddress.isValidated>
  {
  }

  public new abstract class latitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARShippingAddress.latitude>
  {
  }

  public new abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARShippingAddress.longitude>
  {
  }
}
