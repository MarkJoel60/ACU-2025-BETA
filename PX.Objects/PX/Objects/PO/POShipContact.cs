// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POShipContact
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>
/// Represents a shipping location contact that contains the information related to the shipping of the ordered items.
/// </summary>
/// <remarks>
/// The records of this type are created and edited on the <i>Purchase Orders (PO301000)</i> form
/// (which corresponds to the <see cref="T:PX.Objects.PO.POOrderEntry" /> graph).
/// </remarks>
[PXCacheName("PO Shipping Contact")]
[Serializable]
public class POShipContact : POContact
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  POContact>.By<POShipContact.contactID>
  {
    public static POContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (POContact) PrimaryKeyOf<POContact>.By<POShipContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.contactID" />
  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POShipContact.contactID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.bAccountID" />
  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POShipContact.bAccountID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.bAccountContactID" />
  public new abstract class bAccountContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POShipContact.bAccountContactID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.revisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POShipContact.revisionID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.isDefaultContact" />
  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POShipContact.isDefaultContact>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.overrideContact" />
  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POShipContact.overrideContact>
  {
  }
}
