// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentContact
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
/// Represents a contact that contains the information related to the shipping of the ordered items.
/// </summary>
/// <remarks>
/// The records of this type are created and edited on the <i>Shipments (SO302000)</i> form
/// (which corresponds to the <see cref="T:PX.Objects.SO.SOShipmentEntry" /> graph).
/// </remarks>
[PXBreakInheritance]
[PXCacheName("Shipment Contact")]
[Serializable]
public class SOShipmentContact : SOContact
{
  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.customerID" />
  [PXDBInt]
  [PXDBDefault(typeof (SOShipment.customerID))]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  SOShipmentContact>.By<SOShipmentContact.contactID>
  {
    public static SOShipmentContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (SOShipmentContact) PrimaryKeyOf<SOShipmentContact>.By<SOShipmentContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOShipmentContact>.By<SOShipmentContact.customerID>
    {
    }

    public class CustomerContact : 
      PrimaryKeyOf<ARContact>.By<ARContact.contactID>.ForeignKeyOf<SOShipmentContact>.By<SOShipmentContact.customerContactID>
    {
    }
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.contactID" />
  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentContact.contactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShipmentContact.CustomerID" />
  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentContact.customerID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.customerContactID" />
  public new abstract class customerContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentContact.customerContactID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.isDefaultContact" />
  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentContact.isDefaultContact>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.overrideContact" />
  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentContact.overrideContact>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.SO.SOContact.revisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentContact.revisionID>
  {
  }
}
