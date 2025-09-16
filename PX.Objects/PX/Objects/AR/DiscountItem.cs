// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// For <see cref="T:PX.Objects.AR.DiscountSequence">discount sequences</see> applicable to certain
/// <see cref="T:PX.Objects.IN.InventoryItem">inventory items</see>, records of this type define
/// specific items to which the corresponding sequence applies. The entities of
/// this type can be edited on the Items tab of the Discounts (AR209500) form,
/// which corresponds to the <see cref="T:PX.Objects.AR.ARDiscountSequenceMaint" /> graph.
/// </summary>
[PXCacheName("Discount Item")]
[Serializable]
public class DiscountItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected int? _InventoryID;
  protected string _DiscountSequenceID;
  protected Decimal? _Amount;
  protected Decimal? _Quantity;
  protected string _UOM;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (DiscountSequence.discountID))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [InventoryIncludingTemplates(IsKey = true)]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<DiscountItem.inventoryID>>>>))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (DiscountSequence.discountSequenceID))]
  [PXParent(typeof (Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Current<DiscountItem.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<DiscountItem.discountID>>>>>))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBPriceCost(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField]
  public virtual Decimal? Quantity
  {
    get => this._Quantity;
    set => this._Quantity = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARSalesPrice.inventoryID>>>>))]
  [INUnit(typeof (DiscountItem.inventoryID), Visible = false)]
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
    PrimaryKeyOf<DiscountItem>.By<DiscountItem.discountID, DiscountItem.discountSequenceID, DiscountItem.inventoryID>
  {
    public static DiscountItem Find(
      PXGraph graph,
      string discountID,
      string discountSequenceID,
      int? inventoryID,
      PKFindOptions options = 0)
    {
      return (DiscountItem) PrimaryKeyOf<DiscountItem>.By<DiscountItem.discountID, DiscountItem.discountSequenceID, DiscountItem.inventoryID>.FindBy(graph, (object) discountID, (object) discountSequenceID, (object) inventoryID, options);
    }
  }

  public static class FK
  {
    public class DiscountSequence : 
      PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>.ForeignKeyOf<DiscountItem>.By<DiscountItem.discountID, DiscountItem.discountSequenceID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DiscountItem>.By<DiscountItem.inventoryID>
    {
    }
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountItem.discountID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscountItem.inventoryID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountItem.discountSequenceID>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountItem.amount>
  {
  }

  public abstract class quantity : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DiscountItem.quantity>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscountItem.uOM>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DiscountItem.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DiscountItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountItem.lastModifiedDateTime>
  {
  }
}
