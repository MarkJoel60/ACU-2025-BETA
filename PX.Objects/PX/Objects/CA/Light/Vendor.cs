// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.Vendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[PXTable(new Type[] {typeof (BAccount.bAccountID)})]
[PXCacheName("Customer")]
[Serializable]
public class Vendor : BAccount
{
  [PXDBString(5, IsUnicode = true, BqlTable = typeof (Vendor))]
  public override 
  #nullable disable
  string CuryID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string VendorClassID { get; set; }

  public new class PK : PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>
  {
    public static Vendor Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (Vendor) PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<Vendor>.By<Vendor.acctCD>
  {
    public static Vendor Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (Vendor) PrimaryKeyOf<Vendor>.By<Vendor.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.acctName>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.curyID>
  {
  }

  public abstract class vendorClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.vendorClassID>
  {
  }
}
