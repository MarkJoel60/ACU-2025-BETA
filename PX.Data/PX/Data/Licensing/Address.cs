// Decompiled with JetBrains decompiler
// Type: PX.Data.Licensing.Address
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Licensing;

[PXHidden]
[Serializable]
public class Address : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  public virtual int BAccountID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int AddressID { get; set; }

  [PXDBString(50, IsUnicode = true)]
  public virtual 
  #nullable disable
  string AddressLine1 { get; set; }

  [PXDBString(50, IsUnicode = true)]
  public virtual string AddressLine2 { get; set; }

  [PXDBString(50, IsUnicode = true)]
  public virtual string City { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string CountryID { get; set; }

  [PXDBString(50, IsUnicode = true)]
  public virtual string State { get; set; }

  [PXDBString(20)]
  public virtual string PostalCode { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Address.bAccountID>
  {
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Address.addressID>
  {
  }

  public abstract class addressLine1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.addressLine1>
  {
  }

  public abstract class addressLine2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.addressLine2>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.city>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.state>
  {
  }

  public abstract class postalCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Address.postalCode>
  {
  }
}
