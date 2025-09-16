// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Replenishment Line")]
[Serializable]
public class INReplenishmentLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RefNbr;
  protected int? _LineNbr;
  protected DateTime? _OrderDate;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _DestinationSiteID;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected long? _PlanID;
  protected string _POType;
  protected string _PONbr;
  protected int? _POLineNbr;
  protected string _SOType;
  protected string _SONbr;
  protected int? _SOLineNbr;
  protected int? _SOSplitLineNbr;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (INReplenishmentOrder.refNbr))]
  [PXParent(typeof (INReplenishmentLine.FK.ReplenishmentOrder))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (INReplenishmentOrder.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Order Date")]
  [PXDefault(typeof (INReplenishmentOrder.orderDate))]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [StockItem]
  [PXForeignReference(typeof (INReplenishmentLine.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (INReplenishmentLine.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [SiteAvail(typeof (INReplenishmentLine.inventoryID), typeof (INReplenishmentLine.subItemID), typeof (CostCenter.freeStock))]
  [PXForeignReference(typeof (INReplenishmentLine.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [SiteAvail(typeof (INReplenishmentLine.inventoryID), typeof (INReplenishmentLine.subItemID), typeof (CostCenter.freeStock))]
  [PXForeignReference(typeof (INReplenishmentLine.FK.DestinationSite))]
  public virtual int? DestinationSiteID
  {
    get => this._DestinationSiteID;
    set => this._DestinationSiteID = value;
  }

  [PXDefault(typeof (Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<INReplenishmentLine.inventoryID>>>>))]
  [INUnit(typeof (INReplenishmentLine.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (INReplenishmentLine.uOM), typeof (INReplenishmentLine.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [Vendor]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBLong]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, InputMask = "", IsUnicode = true)]
  [PXUIField]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBInt]
  [PXUIField]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  public virtual string SOType
  {
    get => this._SOType;
    set => this._SOType = value;
  }

  [PXDBString(15, InputMask = "", IsUnicode = true)]
  [PXUIField]
  public virtual string SONbr
  {
    get => this._SONbr;
    set => this._SONbr = value;
  }

  [PXDBInt]
  [PXUIField]
  public virtual int? SOLineNbr
  {
    get => this._SOLineNbr;
    set => this._SOLineNbr = value;
  }

  [PXDBInt]
  [PXUIField]
  public virtual int? SOSplitLineNbr
  {
    get => this._SOSplitLineNbr;
    set => this._SOSplitLineNbr = value;
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
    PrimaryKeyOf<INReplenishmentLine>.By<INReplenishmentLine.refNbr, INReplenishmentLine.lineNbr>
  {
    public static INReplenishmentLine Find(
      PXGraph graph,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (INReplenishmentLine) PrimaryKeyOf<INReplenishmentLine>.By<INReplenishmentLine.refNbr, INReplenishmentLine.lineNbr>.FindBy(graph, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ReplenishmentOrder : 
      PrimaryKeyOf<INReplenishmentOrder>.By<INReplenishmentOrder.refNbr>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.refNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.siteID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.vendorID, INReplenishmentLine.vendorLocationID>
    {
    }

    public class DestinationSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.destinationSiteID>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.planID>
    {
    }

    public class POOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.pOType, INReplenishmentLine.pONbr>
    {
    }

    public class POLine : 
      PrimaryKeyOf<PX.Objects.PO.POLine>.By<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.pOType, INReplenishmentLine.pONbr, INReplenishmentLine.pOLineNbr>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.sOType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.sOType, INReplenishmentLine.sONbr>
    {
    }

    public class SOLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.sOType, INReplenishmentLine.sONbr, INReplenishmentLine.sOLineNbr>
    {
    }

    public class SOLineSplit : 
      PrimaryKeyOf<PX.Objects.SO.SOLineSplit>.By<PX.Objects.SO.SOLineSplit.orderType, PX.Objects.SO.SOLineSplit.orderNbr, PX.Objects.SO.SOLineSplit.lineNbr, PX.Objects.SO.SOLineSplit.splitLineNbr>.ForeignKeyOf<INReplenishmentLine>.By<INReplenishmentLine.sOType, INReplenishmentLine.sONbr, INReplenishmentLine.sOLineNbr, INReplenishmentLine.sOSplitLineNbr>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentLine.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentLine.lineNbr>
  {
  }

  public abstract class orderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentLine.orderDate>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentLine.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentLine.siteID>
  {
  }

  public abstract class destinationSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentLine.destinationSiteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentLine.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INReplenishmentLine.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INReplenishmentLine.baseQty>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentLine.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentLine.vendorLocationID>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INReplenishmentLine.planID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentLine.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentLine.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentLine.pOLineNbr>
  {
  }

  public abstract class sOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentLine.sOType>
  {
  }

  public abstract class sONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentLine.sONbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentLine.sOLineNbr>
  {
  }

  public abstract class sOSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentLine.sOSplitLineNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INReplenishmentLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INReplenishmentLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INReplenishmentLine.Tstamp>
  {
  }
}
