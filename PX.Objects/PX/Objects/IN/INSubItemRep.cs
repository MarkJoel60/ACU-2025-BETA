// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSubItemRep
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Subitem Replenishment Settings")]
[Serializable]
public class INSubItemRep : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _ReplenishmentClassID;
  protected int? _SubItemID;
  protected Decimal? _SafetyStock;
  protected Decimal? _MinQty;
  protected Decimal? _MaxQty;
  protected Decimal? _TransferERQ;
  protected string _ItemStatus;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [StockItem(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID", Visible = false)]
  [PXForeignReference(typeof (INSubItemRep.FK.InventoryItem))]
  [PXParent(typeof (INSubItemRep.FK.ItemReplenishment))]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  public virtual string CuryID { get; set; }

  [PXDefault(typeof (INItemRep.replenishmentClassID))]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Replenishment Class ID", Visible = false)]
  public virtual string ReplenishmentClassID
  {
    get => this._ReplenishmentClassID;
    set => this._ReplenishmentClassID = value;
  }

  [SubItem(typeof (INSubItemRep.inventoryID), DisplayName = "Subitem", IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Safety Stock")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemRep, Where<INItemRep.inventoryID, Equal<Current<INSubItemRep.inventoryID>>, And<INItemRep.curyID, Equal<Current<INSubItemRep.curyID>>, And<INItemRep.replenishmentClassID, Equal<Current<INSubItemRep.replenishmentClassID>>>>>>), SourceField = typeof (INItemRep.safetyStock))]
  public virtual Decimal? SafetyStock
  {
    get => this._SafetyStock;
    set => this._SafetyStock = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Reorder Point")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemRep, Where<INItemRep.inventoryID, Equal<Current<INSubItemRep.inventoryID>>, And<INItemRep.curyID, Equal<Current<INSubItemRep.curyID>>, And<INItemRep.replenishmentClassID, Equal<Current<INSubItemRep.replenishmentClassID>>>>>>), SourceField = typeof (INItemRep.minQty))]
  public virtual Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Qty.")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemRep, Where<INItemRep.inventoryID, Equal<Current<INSubItemRep.inventoryID>>, And<INItemRep.curyID, Equal<Current<INSubItemRep.curyID>>, And<INItemRep.replenishmentClassID, Equal<Current<INSubItemRep.replenishmentClassID>>>>>>), SourceField = typeof (INItemRep.maxQty))]
  public virtual Decimal? MaxQty
  {
    get => this._MaxQty;
    set => this._MaxQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Transfer ERQ")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemRep, Where<INItemRep.inventoryID, Equal<Current<INSubItemRep.inventoryID>>, And<INItemRep.curyID, Equal<Current<INSubItemRep.curyID>>, And<INItemRep.replenishmentClassID, Equal<Current<INSubItemRep.replenishmentClassID>>>>>>))]
  public virtual Decimal? TransferERQ
  {
    get => this._TransferERQ;
    set => this._TransferERQ = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [InventoryItemStatus.SubItemList]
  [PXDefault("AC")]
  public virtual string ItemStatus
  {
    get => this._ItemStatus;
    set => this._ItemStatus = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INSubItemRep>.By<INSubItemRep.replenishmentClassID, INSubItemRep.inventoryID, INSubItemRep.subItemID>
  {
    public static INSubItemRep Find(
      PXGraph graph,
      string replenishmentClassID,
      int? inventoryID,
      int? subItemID,
      PKFindOptions options = 0)
    {
      return (INSubItemRep) PrimaryKeyOf<INSubItemRep>.By<INSubItemRep.replenishmentClassID, INSubItemRep.inventoryID, INSubItemRep.subItemID>.FindBy(graph, (object) replenishmentClassID, (object) inventoryID, (object) subItemID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INSubItemRep>.By<INSubItemRep.inventoryID>
    {
    }

    public class ItemReplenishment : 
      PrimaryKeyOf<INItemRep>.By<INItemRep.inventoryID, INItemRep.curyID, INItemRep.replenishmentClassID>.ForeignKeyOf<INSubItemRep>.By<INSubItemRep.inventoryID, INSubItemRep.curyID, INSubItemRep.replenishmentClassID>
    {
    }

    public class ReplenishmentClass : 
      PrimaryKeyOf<INReplenishmentClass>.By<INReplenishmentClass.replenishmentClassID>.ForeignKeyOf<INSubItemRep>.By<INSubItemRep.replenishmentClassID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INSubItemRep>.By<INSubItemRep.subItemID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSubItemRep.inventoryID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSubItemRep.curyID>
  {
  }

  public abstract class replenishmentClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSubItemRep.replenishmentClassID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSubItemRep.subItemID>
  {
  }

  public abstract class safetyStock : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSubItemRep.safetyStock>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSubItemRep.minQty>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSubItemRep.maxQty>
  {
  }

  public abstract class transferERQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSubItemRep.transferERQ>
  {
  }

  public abstract class itemStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSubItemRep.itemStatus>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSubItemRep.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSubItemRep.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSubItemRep.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INSubItemRep.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSubItemRep.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSubItemRep.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INSubItemRep.Tstamp>
  {
  }
}
