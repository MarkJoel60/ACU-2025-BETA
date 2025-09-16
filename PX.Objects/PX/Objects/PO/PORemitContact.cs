// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PORemitContact
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
/// Represents a vendor location contact that contains the information related to the vendor to supply the ordered goods.
/// </summary>
/// <remarks>
/// The records of this type are created and edited on the <i>Purchase Orders (PO301000)</i> form
/// (which corresponds to the <see cref="T:PX.Objects.PO.POOrderEntry" /> graph).
/// </remarks>
[PXCacheName("PO Remittance Contact")]
[Serializable]
public class PORemitContact : POContact
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  PORemitContact>.By<PORemitContact.contactID>
  {
    public static PORemitContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (PORemitContact) PrimaryKeyOf<PORemitContact>.By<PORemitContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.contactID" />
  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PORemitContact.contactID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.bAccountID" />
  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PORemitContact.bAccountID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.bAccountContactID" />
  public new abstract class bAccountContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PORemitContact.bAccountContactID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.revisionID" />
  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PORemitContact.revisionID>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.isDefaultContact" />
  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PORemitContact.isDefaultContact>
  {
  }

  /// <inheritdoc cref="T:PX.Objects.PO.POContact.overrideContact" />
  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PORemitContact.overrideContact>
  {
  }
}
