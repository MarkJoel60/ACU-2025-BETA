// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemBox
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

[PXCacheName("IN Item Box")]
[Serializable]
public class INItemBox : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _BoxID;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected Decimal? _MaxQty;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXParent(typeof (INItemBox.FK.InventoryItem))]
  [PXDBInt(IsKey = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXSelector(typeof (PX.Objects.CS.CSBox.boxID))]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string BoxID
  {
    get => this._BoxID;
    set => this._BoxID = value;
  }

  [INUnit(typeof (INItemBox.inventoryID), DisplayName = "UOM", DirtyRead = true)]
  [PXDefault(typeof (Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<INItemBox.inventoryID>>>>))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (INItemBox.uOM), typeof (INItemBox.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty.")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXDecimal(6, MinValue = 0.0)]
  [PXUIField(DisplayName = "Max. Qty", Enabled = false)]
  public virtual Decimal? MaxQty
  {
    get => this._MaxQty;
    set => this._MaxQty = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  public class PK : PrimaryKeyOf<INItemBox>.By<INItemBox.inventoryID, INItemBox.boxID>
  {
    public static INItemBox Find(
      PXGraph graph,
      int? inventoryID,
      string boxID,
      PKFindOptions options = 0)
    {
      return (INItemBox) PrimaryKeyOf<INItemBox>.By<INItemBox.inventoryID, INItemBox.boxID>.FindBy(graph, (object) inventoryID, (object) boxID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemBox>.By<INItemBox.inventoryID>
    {
    }

    public class CSBox : 
      PrimaryKeyOf<PX.Objects.CS.CSBox>.By<PX.Objects.CS.CSBox.boxID>.ForeignKeyOf<INItemBox>.By<INItemBox.boxID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemBox.inventoryID>
  {
  }

  public abstract class boxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemBox.boxID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemBox.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBox.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBox.baseQty>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemBox.maxQty>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemBox.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemBox.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemBox.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemBox.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemBox.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemBox.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemBox.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemBox.lastModifiedDateTime>
  {
  }
}
