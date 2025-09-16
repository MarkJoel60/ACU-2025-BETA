// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorUOMXRef
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[Serializable]
public class POVendorUOMXRef : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _VendorUOM;
  protected string _UOM;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [VendorActive]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(10, IsKey = true, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor UOM")]
  public virtual string VendorUOM
  {
    get => this._VendorUOM;
    set => this._VendorUOM = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POVendorInventory.inventoryID>>>>))]
  [INUnit(typeof (POVendorInventory.inventoryID), DisplayName = "UOM", IsKey = true)]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<POVendorUOMXRef>.By<POVendorUOMXRef.vendorID, POVendorUOMXRef.vendorUOM, POVendorUOMXRef.uOM>
  {
    public static POVendorUOMXRef Find(
      PXGraph graph,
      int? vendorID,
      string vendorUOM,
      string uOM,
      PKFindOptions options = 0)
    {
      return (POVendorUOMXRef) PrimaryKeyOf<POVendorUOMXRef>.By<POVendorUOMXRef.vendorID, POVendorUOMXRef.vendorUOM, POVendorUOMXRef.uOM>.FindBy(graph, (object) vendorID, (object) vendorUOM, (object) uOM, options);
    }
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorUOMXRef.vendorID>
  {
  }

  public abstract class vendorUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POVendorUOMXRef.vendorUOM>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POVendorUOMXRef.uOM>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POVendorUOMXRef.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POVendorUOMXRef.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POVendorUOMXRef.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POVendorUOMXRef.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POVendorUOMXRef.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POVendorUOMXRef.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POVendorUOMXRef.lastModifiedDateTime>
  {
  }
}
