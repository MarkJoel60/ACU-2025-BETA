// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSShippingAddress
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Field Service Shipping Address")]
[Serializable]
public class FSShippingAddress : FSAddress
{
  [PXExtraKey]
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public override bool? IsDefaultAddress
  {
    get => this._IsDefaultAddress;
    set => this._IsDefaultAddress = value;
  }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  FSShippingAddress>.By<FSShippingAddress.addressID>
  {
    public static FSShippingAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (FSShippingAddress) PrimaryKeyOf<FSShippingAddress>.By<FSShippingAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public new static class FK
  {
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<FSShippingAddress>.By<FSShippingAddress.bAccountID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<FSShippingAddress>.By<FSShippingAddress.countryID>
    {
    }

    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<FSShippingAddress>.By<FSShippingAddress.countryID, FSShippingAddress.state>
    {
    }
  }

  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSShippingAddress.addressID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSShippingAddress.bAccountID>
  {
  }

  public new abstract class bAccountAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSShippingAddress.bAccountAddressID>
  {
  }

  public new abstract class isDefaultAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSShippingAddress.isDefaultAddress>
  {
  }

  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSShippingAddress.overrideAddress>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSShippingAddress.revisionID>
  {
  }

  public new abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSShippingAddress.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSShippingAddress.state>
  {
  }
}
