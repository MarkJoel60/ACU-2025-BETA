// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransitLineLotSerialStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select2<INLotSerialStatusInTransit, InnerJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLotSerialStatusInTransit.locationID>>>>), Persistent = false)]
public class INTransitLineLotSerialStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _LotSerialNbr;
  protected int? _SiteID;
  protected int? _CostSiteID;
  protected int? _FromSiteID;
  protected int? _ToSiteID;
  protected string _OrigModule;
  protected int? _ToLocationID;
  protected Guid? _NoteID;
  protected string _TransferNbr;
  protected int? _TransferLineNbr;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOOrderLineNbr;
  protected string _SOShipmentType;
  protected string _SOShipmentNbr;
  protected int? _SOShipmentLineNbr;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyHardAvail;
  protected Decimal? _QtyInTransit;
  protected Decimal? _QtyInTransitToSO;
  protected Decimal? _QtyPOPrepared;
  protected Decimal? _QtyPOOrders;
  protected Decimal? _QtyPOReceipts;
  protected Decimal? _QtySOBackOrdered;
  protected Decimal? _QtySOPrepared;
  protected Decimal? _QtySOBooked;
  protected Decimal? _QtySOShipped;
  protected Decimal? _QtySOShipping;
  protected Decimal? _QtyINIssues;
  protected Decimal? _QtyINReceipts;
  protected Decimal? _QtyINAssemblyDemand;
  protected Decimal? _QtyINAssemblySupply;
  protected Decimal? _QtySOFixed;
  protected Decimal? _QtyPOFixedOrders;
  protected Decimal? _QtyPOFixedPrepared;
  protected Decimal? _QtyPOFixedReceipts;
  protected Decimal? _QtySODropShip;
  protected Decimal? _QtyPODropShipOrders;
  protected Decimal? _QtyPODropShipPrepared;
  protected Decimal? _QtyPODropShipReceipts;
  protected Decimal? _QtyNotAvail;
  protected Decimal? _QtyExpired;

  [StockItem(BqlField = typeof (INLotSerialStatusInTransit.inventoryID), IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(BqlField = typeof (INLotSerialStatusInTransit.subItemID), IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (INLotSerialStatusInTransit.lotSerialNbr), IsKey = true)]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [Site(BqlField = typeof (INLotSerialStatusInTransit.siteID))]
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
  [INTransitLineLotSerialStatus.origModule.List]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  [Location(typeof (INTransitLineLotSerialStatus.toSiteID), DisplayName = "To Location ID", BqlField = typeof (INTransitLine.toLocationID))]
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

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INTransitLine.transferNbr), IsKey = true)]
  public virtual string TransferNbr
  {
    get => this._TransferNbr;
    set => this._TransferNbr = value;
  }

  [PXDBInt(BqlField = typeof (INTransitLine.transferLineNbr), IsKey = true)]
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

  [PXDBString(15, InputMask = "", IsUnicode = true, BqlField = typeof (INTransitLine.sOShipmentNbr))]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  [PXDBInt(BqlField = typeof (INTransitLine.sOShipmentLineNbr))]
  public virtual int? SOShipmentLineNbr
  {
    get => this._SOShipmentLineNbr;
    set => this._SOShipmentLineNbr = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyHardAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Hard Available")]
  public virtual Decimal? QtyHardAvail
  {
    get => this._QtyHardAvail;
    set => this._QtyHardAvail = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyInTransit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyInTransitToSO))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransitToSO
  {
    get => this._QtyInTransitToSO;
    set => this._QtyInTransitToSO = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPOPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOPrepared
  {
    get => this._QtyPOPrepared;
    set => this._QtyPOPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPOOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOOrders
  {
    get => this._QtyPOOrders;
    set => this._QtyPOOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPOReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOReceipts
  {
    get => this._QtyPOReceipts;
    set => this._QtyPOReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtySOBackOrdered))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOBackOrdered
  {
    get => this._QtySOBackOrdered;
    set => this._QtySOBackOrdered = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtySOPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOPrepared
  {
    get => this._QtySOPrepared;
    set => this._QtySOPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtySOBooked))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOBooked
  {
    get => this._QtySOBooked;
    set => this._QtySOBooked = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtySOShipped))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShipped
  {
    get => this._QtySOShipped;
    set => this._QtySOShipped = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtySOShipping))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShipping
  {
    get => this._QtySOShipping;
    set => this._QtySOShipping = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyINIssues))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Issues")]
  public virtual Decimal? QtyINIssues
  {
    get => this._QtyINIssues;
    set => this._QtyINIssues = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyINReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Receipts")]
  public virtual Decimal? QtyINReceipts
  {
    get => this._QtyINReceipts;
    set => this._QtyINReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyINAssemblyDemand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Demanded by Kit Assembly")]
  public virtual Decimal? QtyINAssemblyDemand
  {
    get => this._QtyINAssemblyDemand;
    set => this._QtyINAssemblyDemand = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyINAssemblySupply))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Kit Assembly")]
  public virtual Decimal? QtyINAssemblySupply
  {
    get => this._QtyINAssemblySupply;
    set => this._QtyINAssemblySupply = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtySOFixed))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOFixed
  {
    get => this._QtySOFixed;
    set => this._QtySOFixed = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPOFixedOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedOrders
  {
    get => this._QtyPOFixedOrders;
    set => this._QtyPOFixedOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPOFixedPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedPrepared
  {
    get => this._QtyPOFixedPrepared;
    set => this._QtyPOFixedPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPOFixedReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedReceipts
  {
    get => this._QtyPOFixedReceipts;
    set => this._QtyPOFixedReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtySODropShip))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySODropShip
  {
    get => this._QtySODropShip;
    set => this._QtySODropShip = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPODropShipOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipOrders
  {
    get => this._QtyPODropShipOrders;
    set => this._QtyPODropShipOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPODropShipPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipPrepared
  {
    get => this._QtyPODropShipPrepared;
    set => this._QtyPODropShipPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyPODropShipReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipReceipts
  {
    get => this._QtyPODropShipReceipts;
    set => this._QtyPODropShipReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyNotAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyNotAvail
  {
    get => this._QtyNotAvail;
    set => this._QtyNotAvail = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusInTransit.qtyExpired))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyExpired
  {
    get => this._QtyExpired;
    set => this._QtyExpired = value;
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.lotSerialNbr>
  {
    public const int LENGTH = 100;
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineLotSerialStatus.siteID>
  {
  }

  public abstract class costSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.costSiteID>
  {
  }

  public abstract class fromSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.fromSiteID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineLotSerialStatus.toSiteID>
  {
  }

  public abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.origModule>
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

  public abstract class toLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.toLocationID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransitLineLotSerialStatus.noteID>
  {
  }

  public abstract class transferNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.transferNbr>
  {
  }

  public abstract class transferLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.transferLineNbr>
  {
  }

  public abstract class sOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.sOOrderLineNbr>
  {
  }

  public abstract class sOShipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.sOShipmentNbr>
  {
  }

  public abstract class sOShipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.sOShipmentLineNbr>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyOnHand>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyHardAvail>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPOReceipts>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyINAssemblySupply>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyPODropShipReceipts>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyNotAvail>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineLotSerialStatus.qtyExpired>
  {
  }
}
