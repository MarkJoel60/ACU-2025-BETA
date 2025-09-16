// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Address2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[Serializable]
public class Address2 : Address
{
  public new abstract class addressID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Address2.addressID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Address2.bAccountID>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Address2.revisionID>
  {
  }

  public new abstract class addressType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.addressType>
  {
  }

  public new abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.displayName>
  {
  }

  public new abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.addressLine1>
  {
  }

  public new abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.addressLine2>
  {
  }

  public new abstract class addressLine3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.addressLine3>
  {
  }

  public new abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.city>
  {
  }

  public new abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.state>
  {
  }

  public new abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.postalCode>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Address2.noteID>
  {
  }

  public new abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Address2.isValidated>
  {
  }

  public new abstract class taxLocationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address2.taxLocationCode>
  {
  }

  public new abstract class taxMunicipalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address2.taxMunicipalCode>
  {
  }

  public new abstract class taxSchoolCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address2.taxSchoolCode>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Address2.Tstamp>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Address2.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address2.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Address2.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    Address2.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Address2.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Address2.lastModifiedDateTime>
  {
  }

  public new abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Address2.latitude>
  {
  }

  public new abstract class longitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Address2.longitude>
  {
  }
}
