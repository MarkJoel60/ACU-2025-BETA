// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillingAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a billing address that is specified in the <see cref="T:PX.Objects.PM.PMProject">project</see> for billing purposes.
/// These settings are initially populated with the information specified on the <strong>Billing Settings</strong> tab
/// of the Customers (AR303000) form, but you can override any of the default settings.
/// This record reflects changes made to the original <see cref="T:PX.Objects.CR.Address">address</see> record. The entities of this type are
/// created and edited on the Projects (PM301000) form (which corresponds to the <see cref="T:PX.Objects.PM.ProjectEntry" /> graph)
/// </summary>
[PXCacheName("PM Billing Address")]
[PXBreakInheritance]
[PXProjection(typeof (MappedSelect<PMBillingAddress, From<BqlTableMapper<PMProjectBillingAddress, PMProjectBillingAddressMapped>, Union<BqlTableMapper<PMCustomerBillingAddress, PMCustomerBillingAddressMapped>>>>))]
[Serializable]
public class PMBillingAddress : PMAddress, IBqlTable, IBqlTableSystemDataStorage
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  PMBillingAddress>.By<PMBillingAddress.addressID>
  {
    public static PMAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (PMAddress) PrimaryKeyOf<PMBillingAddress>.By<PMBillingAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingAddress.addressID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingAddress.customerID>
  {
  }

  public new abstract class customerAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMBillingAddress.customerAddressID>
  {
  }

  public new abstract class isDefaultBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingAddress.isDefaultBillAddress>
  {
  }

  public new abstract class overrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMBillingAddress.overrideAddress>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMBillingAddress.revisionID>
  {
  }

  public new abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingAddress.addressLine1>
  {
  }

  public new abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingAddress.addressLine2>
  {
  }

  public new abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingAddress.addressLine3>
  {
  }

  public new abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingAddress.city>
  {
  }

  public new abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingAddress.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMBillingAddress.state>
  {
  }

  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMBillingAddress.postalCode>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMBillingAddress.noteID>
  {
  }

  public new abstract class isValidated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMBillingAddress.isValidated>
  {
  }
}
