// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransitLineStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select5<INLocationStatusInTransit, InnerJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLocationStatusInTransit.locationID>>>, Aggregate<GroupBy<INTransitLine.transferNbr, GroupBy<INTransitLine.transferLineNbr, Sum<INLocationStatusInTransit.qtyOnHand, Sum<INLocationStatusInTransit.qtyInTransit, Sum<INLocationStatusInTransit.qtyInTransitToSO>>>>>>>), Persistent = false)]
public class INTransitLineStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  /// <inheritdoc cref="P:PX.Objects.IN.INLocationStatusByCostCenter.InventoryID" />
  [StockItem(BqlField = typeof (INLocationStatusInTransit.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INLocationStatusByCostCenter.SiteID" />
  [Site(BqlField = typeof (INLocationStatusInTransit.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.CostSiteID" />
  [PXDBInt(BqlField = typeof (INTransitLine.costSiteID))]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.SiteID" />
  [Site(DisplayName = "From Warehouse ID", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransitLine.siteID))]
  public virtual int? FromSiteID
  {
    get => this._FromSiteID;
    set => this._FromSiteID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.ToSiteID" />
  [ToSite(DisplayName = "To Warehouse ID", DescriptionField = typeof (INSite.descr), BqlField = typeof (INTransitLine.toSiteID))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.OrigModule" />
  [PXDBString(2, IsFixed = true, BqlField = typeof (INTransitLine.origModule))]
  [PXDefault("IN")]
  [PXUIField]
  [INTransitLineStatus.origModule.List]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.ToLocationID" />
  [Location(typeof (INTransitLineStatus.toSiteID), DisplayName = "To Location ID", BqlField = typeof (INTransitLine.toLocationID))]
  public virtual int? ToLocationID
  {
    get => this._ToLocationID;
    set => this._ToLocationID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.NoteID" />
  [PXNote(BqlField = typeof (INTransitLine.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.RefNoteID" />
  [PXDBGuid(false, BqlField = typeof (INTransitLine.refNoteID))]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.TransferNbr" />
  [PXDBString(15, IsUnicode = true, BqlField = typeof (INTransitLine.transferNbr), IsKey = true)]
  public virtual string TransferNbr
  {
    get => this._TransferNbr;
    set => this._TransferNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.TransferLineNbr" />
  [PXDBInt(BqlField = typeof (INTransitLine.transferLineNbr), IsKey = true)]
  public virtual int? TransferLineNbr
  {
    get => this._TransferLineNbr;
    set => this._TransferLineNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.SOOrderType" />
  [PXDBString(2, IsFixed = true, BqlField = typeof (INTransitLine.sOOrderType))]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.SOOrderNbr" />
  [PXDBString(15, InputMask = "", IsUnicode = true, BqlField = typeof (INTransitLine.sOOrderNbr))]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.SOOrderLineNbr" />
  [PXDBInt(BqlField = typeof (INTransitLine.sOOrderLineNbr))]
  public virtual int? SOOrderLineNbr
  {
    get => this._SOOrderLineNbr;
    set => this._SOOrderLineNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.SOShipmentType" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (INTransitLine.sOShipmentType))]
  public virtual string SOShipmentType
  {
    get => this._SOShipmentType;
    set => this._SOShipmentType = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.SOShipmentNbr" />
  [PXDBString(15, InputMask = "", IsUnicode = true, BqlField = typeof (INTransitLine.sOShipmentNbr))]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.SOShipmentLineNbr" />
  [PXDBInt(BqlField = typeof (INTransitLine.sOShipmentLineNbr))]
  public virtual int? SOShipmentLineNbr
  {
    get => this._SOShipmentLineNbr;
    set => this._SOShipmentLineNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INLocationStatusByCostCenter.QtyOnHand" />
  [PXDBQuantity(BqlField = typeof (INLocationStatusInTransit.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INLocationStatusByCostCenter.QtyInTransit" />
  [PXDBQuantity(BqlField = typeof (INLocationStatusInTransit.qtyInTransit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INLocationStatusByCostCenter.QtyInTransitToSO" />
  [PXDBQuantity(BqlField = typeof (INLocationStatusInTransit.qtyInTransitToSO))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransitToSO
  {
    get => this._QtyInTransitToSO;
    set => this._QtyInTransitToSO = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INTransitLine.TranDate" />
  [PXUIField]
  [PXDBDate(BqlField = typeof (INTransitLine.tranDate))]
  public virtual DateTime? TranDate { get; set; }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineStatus.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineStatus.siteID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineStatus.costSiteID>
  {
  }

  public abstract class fromSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineStatus.fromSiteID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineStatus.toSiteID>
  {
  }

  public abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineStatus.origModule>
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

  public abstract class toLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTransitLineStatus.toLocationID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransitLineStatus.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTransitLineStatus.refNoteID>
  {
  }

  public abstract class transferNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineStatus.transferNbr>
  {
  }

  public abstract class transferLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineStatus.transferLineNbr>
  {
  }

  public abstract class sOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineStatus.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineStatus.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineStatus.sOOrderLineNbr>
  {
  }

  public abstract class sOShipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineStatus.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTransitLineStatus.sOShipmentNbr>
  {
  }

  public abstract class sOShipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTransitLineStatus.sOShipmentLineNbr>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineStatus.qtyOnHand>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineStatus.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTransitLineStatus.qtyInTransitToSO>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTransitLineStatus.tranDate>
  {
  }
}
