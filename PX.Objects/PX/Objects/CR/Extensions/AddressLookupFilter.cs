// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.AddressLookupFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions;

[PXHidden]
[Serializable]
public class AddressLookupFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAddressBase
{
  [PXString]
  [PXUIField(Visible = true)]
  public virtual 
  #nullable disable
  string SearchAddress { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string ViewName { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string AddressLine1 { get; set; }

  [PXDBString]
  [PXUIField(Visible = false)]
  public virtual string AddressLine2 { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string AddressLine3 { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string City { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string CountryID { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string State { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string PostalCode { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(Visible = false)]
  public virtual Decimal? Latitude { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(Visible = false)]
  public virtual Decimal? Longitude { get; set; }

  public abstract class searchAddress : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressLookupFilter.searchAddress>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressLookupFilter.viewName>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressLookupFilter.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressLookupFilter.addressLine2>
  {
  }

  public abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressLookupFilter.addressLine3>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressLookupFilter.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressLookupFilter.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddressLookupFilter.state>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddressLookupFilter.postalCode>
  {
  }

  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AddressLookupFilter.latitude>
  {
  }

  public abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AddressLookupFilter.longitude>
  {
  }
}
