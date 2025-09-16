// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.INTransitLineStatusSO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select5<INLocationStatusInTransit, InnerJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLocationStatusInTransit.locationID>>>, Where<INLocationStatusInTransit.siteID, Equal<SiteAnyAttribute.transitSiteID>>, Aggregate<GroupBy<INTransitLine.sOShipmentNbr, GroupBy<INTransitLine.sOShipmentLineNbr, GroupBy<INTransitLine.sOOrderType, GroupBy<INTransitLine.sOOrderNbr, GroupBy<INTransitLine.siteID, GroupBy<INTransitLine.toSiteID, GroupBy<INTransitLine.origModule, Sum<INLocationStatusInTransit.qtyOnHand, Sum<INLocationStatusInTransit.qtyInTransit, Sum<INLocationStatusInTransit.qtyInTransitToSO>>>>>>>>>>>>), Persistent = false)]
public class INTransitLineStatusSO : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SiteID;
  protected int? _CostSiteID;
  protected int? _FromSiteID;
  protected int? _ToSiteID;
  protected 
  #nullable disable
  string _OrigModule;
  protected int? _ToLocationID;
  protected Guid? _NoteID;
  protected Guid? _RefNoteID;
  protected string _TransferNbr;
  protected int? _TransferLineNbr;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOOrderLineNbr;
  protected string _SOShipmentType;
  protected string _SOShipmentNbr;
  protected int? _SOShipmentLineNbr;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyInTransit;
  protected Decimal? _QtyInTransitToSO;

  [StockItem(BqlField = typeof (INLocationStatusInTransit.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [Site(BqlField = typeof (INLocationStatusInTransit.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt(BqlField = typeof (INTransitLine.costSiteID))]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [Site(DisplayName = "From Warehouse ID", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransitLine.siteID))]
  public virtual int? FromSiteID
  {
    get => this._FromSiteID;
    set => this._FromSiteID = value;
  }

  [ToSite(DisplayName = "To Warehouse ID", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransitLine.toSiteID))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INTransitLine.origModule))]
  [PXDefault("IN")]
  [PXUIField]
  [INTransitLineStatusSO.origModule.List]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  [Location(typeof (INTransitLineStatusSO.toSiteID), DisplayName = "To Location ID", BqlField = typeof (INTransitLine.toLocationID))]
  public virtual int? ToLocationID
  {
    get => this._ToLocationID;
    set => this._ToLocationID = value;
  }

  [PXNote(BqlField = typeof (INTransitLine.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBGuid(false, BqlField = typeof (INTransitLine.refNoteID))]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INTransitLine.transferNbr))]
  public virtual string TransferNbr
  {
    get => this._TransferNbr;
    set => this._TransferNbr = value;
  }

  [PXDBInt(BqlField = typeof (INTransitLine.transferLineNbr))]
  public virtual int? TransferLineNbr
  {
    get => this._TransferLineNbr;
    set => this._TransferLineNbr = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INTransitLine.sOOrderType))]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, InputMask = "", IsUnicode = true, BqlField = typeof (INTransitLine.sOOrderNbr))]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBInt(BqlField = typeof (INTransitLine.sOOrderLineNbr))]
  public virtual int? SOOrderLineNbr
  {
    get => this._SOOrderLineNbr;
    set => this._SOOrderLineNbr = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INTransitLine.sOShipmentType))]
  public virtual string SOShipmentType
  {
    get => this._SOShipmentType;
    set => this._SOShipmentType = value;
  }

  [PXDBString(15, InputMask = "", IsUnicode = true, BqlField = typeof (INTransitLine.sOShipmentNbr), IsKey = true)]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  [PXDBInt(BqlField = typeof (INTransitLine.sOShipmentLineNbr), IsKey = true)]
  public virtual int? SOShipmentLineNbr
  {
    get => this._SOShipmentLineNbr;
    set => this._SOShipmentLineNbr = value;
  }

  [PXDBQuantity(BqlField = typeof (INLocationStatusInTransit.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity(BqlField = typeof (INLocationStatusInTransit.qtyInTransit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  [PXDBQuantity(BqlField = typeof (INLocationStatusInTransit.qtyInTransitToSO))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransitToSO
  {
    get => this._QtyInTransitToSO;
    set => this._QtyInTransitToSO = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.TranDate" />
  [PXDBDate(BqlField = typeof (INTransitLine.tranDate))]
  public virtual DateTime? TranDate { get; set; }

  public abstract class inventoryID : IBqlField, IBqlOperand
  {
  }

  public abstract class siteID : IBqlField, IBqlOperand
  {
  }

  public abstract class costSiteID : IBqlField, IBqlOperand
  {
  }

  public abstract class fromSiteID : IBqlField, IBqlOperand
  {
  }

  public abstract class toSiteID : IBqlField, IBqlOperand
  {
  }

  public abstract class origModule : IBqlField, IBqlOperand
  {
    public const string PI = "PI";

    public class List : PXStringListAttribute
    {
      public List()
        : base(new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("SO", "SO"),
          PXStringListAttribute.Pair("PO", "PO"),
          PXStringListAttribute.Pair("IN", "IN"),
          PXStringListAttribute.Pair("PI", "PI"),
          PXStringListAttribute.Pair("AP", "AP")
        })
      {
      }
    }
  }

  public abstract class toLocationID : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : IBqlField, IBqlOperand
  {
  }

  public abstract class refNoteID : IBqlField, IBqlOperand
  {
  }

  public abstract class transferNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class transferLineNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class sOOrderType : IBqlField, IBqlOperand
  {
  }

  public abstract class sOOrderNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class sOOrderLineNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class sOShipmentType : IBqlField, IBqlOperand
  {
  }

  public abstract class sOShipmentNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class sOShipmentLineNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class qtyOnHand : IBqlField, IBqlOperand
  {
  }

  public abstract class qtyInTransit : IBqlField, IBqlOperand
  {
  }

  public abstract class qtyInTransitToSO : IBqlField, IBqlOperand
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTransitLineStatusSO.tranDate>
  {
  }
}
