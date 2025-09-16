// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOBillingContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// Represents a contact that contains the information related to the billing of the ordered items.
/// </summary>
/// <remarks>
/// The records of this type are created and edited on the following forms:
/// <list type="bullet">
/// <item><description><i>Invoices (SO303000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOInvoiceEntry" /> graph)</description></item>
/// <item><description><i>Sales Orders (SO301000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph)</description></item>
/// </list>
/// </remarks>
[PXBreakInheritance]
[PXCacheName("Billing Contact")]
[Serializable]
public class SOBillingContact : SOContact
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  SOBillingContact>.By<SOBillingContact.contactID>
  {
    public static SOBillingContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (SOBillingContact) PrimaryKeyOf<SOBillingContact>.By<SOBillingContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOBillingContact>.By<SOBillingContact.customerID>
    {
    }

    public class CustomerContact : 
      PrimaryKeyOf<ARContact>.By<ARContact.contactID>.ForeignKeyOf<SOBillingContact>.By<SOBillingContact.customerContactID>
    {
    }
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.contactID" />
  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOBillingContact.contactID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.customerID" />
  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOBillingContact.customerID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.customerContactID" />
  public new abstract class customerContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOBillingContact.customerContactID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.isDefaultContact" />
  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOBillingContact.isDefaultContact>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.overrideContact" />
  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOBillingContact.overrideContact>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.revisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOBillingContact.revisionID>
  {
  }
}
