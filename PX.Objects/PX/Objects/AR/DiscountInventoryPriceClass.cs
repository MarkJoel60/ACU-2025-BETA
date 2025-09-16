// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DiscountInventoryPriceClass
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
/// <see cref="T:PX.Objects.IN.INPriceClass">item price classes</see>, records of this type define
/// specific price classes to which the corresponding sequence applies. The entities of
/// this type can be edited on the Item Price Classes tab of the Discounts (AR209500) form,
/// which corresponds to the <see cref="T:PX.Objects.AR.ARDiscountSequenceMaint" /> graph.
/// </summary>
[PXCacheName("Discount for Inventory and Price Class")]
[Serializable]
public class DiscountInventoryPriceClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DiscountID;
  protected string _InventoryPriceClassID;
  protected string _DiscountSequenceID;
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

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (INPriceClass.priceClassID))]
  [PXUIField]
  public virtual string InventoryPriceClassID
  {
    get => this._InventoryPriceClassID;
    set => this._InventoryPriceClassID = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (DiscountSequence.discountSequenceID))]
  [PXParent(typeof (Select<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Current<DiscountInventoryPriceClass.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Current<DiscountInventoryPriceClass.discountID>>>>>))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
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
    PrimaryKeyOf<DiscountInventoryPriceClass>.By<DiscountInventoryPriceClass.discountID, DiscountInventoryPriceClass.discountSequenceID, DiscountInventoryPriceClass.inventoryPriceClassID>
  {
    public static DiscountInventoryPriceClass Find(
      PXGraph graph,
      string discountID,
      string discountSequenceID,
      string inventoryPriceClassID,
      PKFindOptions options = 0)
    {
      return (DiscountInventoryPriceClass) PrimaryKeyOf<DiscountInventoryPriceClass>.By<DiscountInventoryPriceClass.discountID, DiscountInventoryPriceClass.discountSequenceID, DiscountInventoryPriceClass.inventoryPriceClassID>.FindBy(graph, (object) discountID, (object) discountSequenceID, (object) inventoryPriceClassID, options);
    }
  }

  public static class FK
  {
    public class DiscountSequence : 
      PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>.ForeignKeyOf<DiscountInventoryPriceClass>.By<DiscountInventoryPriceClass.discountID, DiscountInventoryPriceClass.discountSequenceID>
    {
    }

    public class InventoryPriceClass : 
      PrimaryKeyOf<INPriceClass>.By<INPriceClass.priceClassID>.ForeignKeyOf<DiscountInventoryPriceClass>.By<DiscountInventoryPriceClass.inventoryPriceClassID>
    {
    }
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public class DiscountSequenceFK : 
    PrimaryKeyOf<DiscountSequence>.By<DiscountSequence.discountID, DiscountSequence.discountSequenceID>.ForeignKeyOf<DiscountInventoryPriceClass>.By<DiscountInventoryPriceClass.discountID, DiscountInventoryPriceClass.discountSequenceID>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public class InventoryPriceClassFK : 
    PrimaryKeyOf<INPriceClass>.By<INPriceClass.priceClassID>.ForeignKeyOf<DiscountInventoryPriceClass>.By<DiscountInventoryPriceClass.inventoryPriceClassID>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountInventoryPriceClass.discountID>
  {
  }

  public abstract class inventoryPriceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountInventoryPriceClass.inventoryPriceClassID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountInventoryPriceClass.discountSequenceID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    DiscountInventoryPriceClass.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountInventoryPriceClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountInventoryPriceClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountInventoryPriceClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    DiscountInventoryPriceClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscountInventoryPriceClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    DiscountInventoryPriceClass.lastModifiedDateTime>
  {
  }
}
