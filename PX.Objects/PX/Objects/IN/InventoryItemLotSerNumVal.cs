// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemLotSerNumVal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// Represents a Auto-Incremental Value of a Stock Item.
/// Auto-Incremental Value of a Stock Item are available only if the <see cref="!:FeaturesSet.LotSerialTracking">Lot/Serial Tracking</see> feature is enabled.
/// The records of this type are created through the Stock Items (IN202500) form
/// (which corresponds to the <see cref="T:PX.Objects.IN.InventoryItemMaint" /> graph)
/// </summary>
[PXPrimaryGraph(typeof (InventoryItemMaint))]
[PXCacheName("Auto-Incremental Value of a Stock Item")]
[Serializable]
public class InventoryItemLotSerNumVal : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILotSerNumVal
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _LotSerNumVal;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  /// <summary>The unique identifier of the Inventory Item.</summary>
  /// <summary>
  /// The <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see>, to which the item is assigned.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.IN.InventoryItem.inventoryID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXParent(typeof (InventoryItemLotSerNumVal.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>
  /// The start value for the auto-incremented numbering segment.
  /// This field is relevant only if the <see cref="!:FeaturesSet.LotSerialTracking">Lot/Serial Tracking</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="!:INLotSerClass.LotSerNumVal" /> of the corresponding <see cref="!:LotSerClassID">Lot/Serial Class</see>
  /// if <see cref="P:PX.Objects.IN.INLotSerClass.LotSerNumShared" /> is set to <c>true</c>.
  /// </value>
  [PXDBString(30, IsUnicode = false, InputMask = "999999999999999999999999999999")]
  [PXUIField(DisplayName = "Auto-Incremental Value")]
  public virtual string LotSerNumVal
  {
    get => this._LotSerNumVal;
    set => this._LotSerNumVal = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<InventoryItemLotSerNumVal>.By<InventoryItemLotSerNumVal.inventoryID>
  {
    public static InventoryItemLotSerNumVal Find(
      PXGraph graph,
      int? inventoryID,
      PKFindOptions options = 0)
    {
      return (InventoryItemLotSerNumVal) PrimaryKeyOf<InventoryItemLotSerNumVal>.By<InventoryItemLotSerNumVal.inventoryID>.FindBy(graph, (object) inventoryID, options);
    }

    public static InventoryItemLotSerNumVal FindDirty(PXGraph graph, int? inventoryID)
    {
      return PXResultset<InventoryItemLotSerNumVal>.op_Implicit(PXSelectBase<InventoryItemLotSerNumVal, PXSelect<InventoryItemLotSerNumVal, Where<InventoryItemLotSerNumVal.inventoryID, Equal<Required<InventoryItemLotSerNumVal.inventoryID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
      {
        (object) inventoryID
      }));
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<InventoryItemLotSerNumVal>.By<InventoryItemLotSerNumVal.inventoryID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.inventoryID>
  {
  }

  public abstract class lotSerNumVal : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.lotSerNumVal>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    InventoryItemLotSerNumVal.Tstamp>
  {
  }
}
