// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMShippingAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a pro forma shipping address. The records of this type are created and edited on the Pro Forma Invoices (PM307000) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.ProformaEntry" /> graph).
/// The DAC is based on the <see cref="T:PX.Objects.PM.PMAddress" /> DAC.
/// </summary>
[PXCacheName("PM Address")]
[Serializable]
public class PMShippingAddress : PMAddress
{
  public new abstract class addressID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMShippingAddress.addressID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMShippingAddress.customerID>
  {
  }

  public new abstract class customerAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMShippingAddress.customerAddressID>
  {
  }

  public new abstract class isDefaultBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMShippingAddress.isDefaultBillAddress>
  {
  }

  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMShippingAddress.overrideAddress>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMShippingAddress.revisionID>
  {
  }

  public new abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingAddress.addressLine1>
  {
  }

  public new abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingAddress.addressLine2>
  {
  }

  public new abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingAddress.addressLine3>
  {
  }

  public new abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingAddress.city>
  {
  }

  public new abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingAddress.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMShippingAddress.state>
  {
  }

  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMShippingAddress.postalCode>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMShippingAddress.noteID>
  {
  }

  public new abstract class isValidated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMShippingAddress.isValidated>
  {
  }
}
