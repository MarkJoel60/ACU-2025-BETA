// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRContactAccountDataSync.DocumentAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRContactAccountDataSync;

/// <exclude />
[PXHidden]
[Obsolete("Not used anymore.")]
public class DocumentAddress : PXMappedCacheExtension
{
  public virtual bool? IsDefaultAddress { get; set; }

  public virtual bool? OverrideAddress { get; set; }

  public virtual bool? IsValidated { get; set; }

  public virtual 
  #nullable disable
  string AddressLine1 { get; set; }

  public virtual string AddressLine2 { get; set; }

  public virtual string AddressLine3 { get; set; }

  public virtual string City { get; set; }

  public virtual string CountryID { get; set; }

  public virtual string State { get; set; }

  public virtual string PostalCode { get; set; }

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
}
