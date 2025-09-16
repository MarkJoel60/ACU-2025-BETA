// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingWorksheetLine
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
namespace PX.Objects.SO;

[PXCacheName]
public class SOPickingWorksheetLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXUIField(DisplayName = "Worksheet Nbr.", Visible = false, Enabled = false)]
  [PXParent(typeof (SOPickingWorksheetLine.FK.Worksheet))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOPickingWorksheet))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [Site(Enabled = false)]
  [PXForeignReference(typeof (SOPickingWorksheetLine.FK.Site))]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [Location(typeof (SOPickingWorksheetLine.siteID), Enabled = false)]
  [PXForeignReference(typeof (SOPickingWorksheetLine.FK.Location))]
  public virtual int? LocationID { get; set; }

  [Inventory(Enabled = false)]
  [PXForeignReference(typeof (SOPickingWorksheetLine.FK.InventoryItem))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (SOPickingWorksheetLine.inventoryID), Enabled = false)]
  public virtual int? SubItemID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial", Enabled = false)]
  public virtual string LotSerialNbr { get; set; }

  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial", Enabled = false)]
  public virtual DateTime? ExpireDate { get; set; }

  [INUnit(typeof (SOPickingWorksheetLine.inventoryID), DisplayName = "UOM", Enabled = false)]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (SOPickingWorksheetLine.uOM), typeof (SOPickingWorksheetLine.baseQty))]
  [PXUIField(DisplayName = "Shipped Qty.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Shipped Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Ordered Qty.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigOrderQty { get; set; }

  [PXDBBaseQuantity(typeof (SOPickingWorksheetLine.uOM), typeof (SOPickingWorksheetLine.origOrderQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrigOrderQty { get; set; }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenOrderQty { get; set; }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Unassigned Qty.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnassignedQty { get; set; }

  [PXDBQuantity(typeof (SOPickingWorksheetLine.uOM), typeof (SOPickingWorksheetLine.basePickedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Picked Qty.", Enabled = false)]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BasePickedQty { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created At", Enabled = false, IsReadOnly = true)]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By", Enabled = false, IsReadOnly = true)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified At", Enabled = false, IsReadOnly = true)]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.worksheetNbr, SOPickingWorksheetLine.lineNbr>
  {
    public static SOPickingWorksheetLine Find(
      PXGraph graph,
      string worksheetNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOPickingWorksheetLine) PrimaryKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.worksheetNbr, SOPickingWorksheetLine.lineNbr>.FindBy(graph, (object) worksheetNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.worksheetNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.inventoryID, SOPickingWorksheetLine.subItemID, SOPickingWorksheetLine.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.inventoryID, SOPickingWorksheetLine.subItemID, SOPickingWorksheetLine.siteID, SOPickingWorksheetLine.locationID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.subItemID>
    {
    }
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLine.worksheetNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLine.lineNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLine.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLine.locationID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLine.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLine.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetLine.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingWorksheetLine.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickingWorksheetLine.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickingWorksheetLine.baseQty>
  {
  }

  public abstract class origOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLine.origOrderQty>
  {
  }

  public abstract class baseOrigOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLine.baseOrigOrderQty>
  {
  }

  public abstract class openOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLine.openOrderQty>
  {
  }

  public abstract class unassignedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLine.unassignedQty>
  {
  }

  public abstract class pickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLine.pickedQty>
  {
  }

  public abstract class basePickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLine.basePickedQty>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingWorksheetLine.tranDesc>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPickingWorksheetLine.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingWorksheetLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingWorksheetLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetLine.lastModifiedDateTime>
  {
  }
}
