// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingWorksheetLineSplit
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
public class SOPickingWorksheetLineSplit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXUIField(DisplayName = "Worksheet Nbr.", Visible = false, Enabled = false)]
  [PXParent(typeof (SOPickingWorksheetLineSplit.FK.Worksheet))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (SOPickingWorksheetLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  [PXParent(typeof (SOPickingWorksheetLineSplit.FK.WorksheetLine))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOPickingWorksheet))]
  [PXUIField(DisplayName = "Split Nbr.", Visible = false, Enabled = false)]
  public virtual int? SplitNbr { get; set; }

  [Site(Enabled = false)]
  [PXForeignReference(typeof (SOPickingWorksheetLineSplit.FK.Site))]
  [PXDefault(typeof (SOPickingWorksheetLine.siteID))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (SOPickingWorksheetLineSplit.siteID), Enabled = false)]
  [PXForeignReference(typeof (SOPickingWorksheetLineSplit.FK.Location))]
  [PXDefault]
  public virtual int? LocationID { get; set; }

  [Inventory(Enabled = false)]
  [PXForeignReference(typeof (SOPickingWorksheetLineSplit.FK.InventoryItem))]
  [PXDefault(typeof (SOPickingWorksheetLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (SOPickingWorksheetLineSplit.inventoryID), Enabled = false)]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial", Enabled = false)]
  [PXDefault("")]
  public virtual string LotSerialNbr { get; set; }

  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial", Enabled = false)]
  public virtual DateTime? ExpireDate { get; set; }

  [INUnit(typeof (SOPickingWorksheetLineSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (SOPickingWorksheetLineSplit.uOM), typeof (SOPickingWorksheetLineSplit.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBQuantity(typeof (SOPickingWorksheetLineSplit.uOM), typeof (SOPickingWorksheetLineSplit.basePickedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Picked Quantity", Enabled = false)]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BasePickedQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnassigned { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HasGeneratedLotSerialNbr { get; set; }

  [Location(typeof (SOPickingWorksheetLineSplit.siteID), DisplayName = "Sorting Location", Enabled = false)]
  [PXForeignReference(typeof (SOPickingWorksheetLineSplit.FK.SortingLocation))]
  public virtual int? SortingLocationID { get; set; }

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
    PrimaryKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.worksheetNbr, SOPickingWorksheetLineSplit.lineNbr, SOPickingWorksheetLineSplit.splitNbr>
  {
    public static SOPickingWorksheetLineSplit Find(
      PXGraph graph,
      string worksheetNbr,
      int? lineNbr,
      int? splitNbr,
      PKFindOptions options = 0)
    {
      return (SOPickingWorksheetLineSplit) PrimaryKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.worksheetNbr, SOPickingWorksheetLineSplit.lineNbr, SOPickingWorksheetLineSplit.splitNbr>.FindBy(graph, (object) worksheetNbr, (object) lineNbr, (object) splitNbr, options);
    }
  }

  public static class FK
  {
    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.worksheetNbr>
    {
    }

    public class WorksheetLine : 
      PrimaryKeyOf<SOPickingWorksheetLine>.By<SOPickingWorksheetLine.worksheetNbr, SOPickingWorksheetLine.lineNbr>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.worksheetNbr, SOPickingWorksheetLineSplit.lineNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.inventoryID, SOPickingWorksheetLineSplit.subItemID, SOPickingWorksheetLineSplit.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.inventoryID, SOPickingWorksheetLineSplit.subItemID, SOPickingWorksheetLineSplit.siteID, SOPickingWorksheetLineSplit.locationID>
    {
    }

    public class SortingLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.sortingLocationID>
    {
    }

    public class SortingLocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.inventoryID, SOPickingWorksheetLineSplit.subItemID, SOPickingWorksheetLineSplit.siteID, SOPickingWorksheetLineSplit.locationID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOPickingWorksheetLineSplit>.By<SOPickingWorksheetLineSplit.subItemID>
    {
    }
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.worksheetNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLineSplit.lineNbr>
  {
  }

  public abstract class splitNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLineSplit.splitNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingWorksheetLineSplit.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.locationID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingWorksheetLineSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickingWorksheetLineSplit.qty>
  {
  }

  public abstract class baseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.baseQty>
  {
  }

  public abstract class pickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.pickedQty>
  {
  }

  public abstract class basePickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.basePickedQty>
  {
  }

  public abstract class isUnassigned : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.isUnassigned>
  {
  }

  public abstract class hasGeneratedLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.hasGeneratedLotSerialNbr>
  {
  }

  public abstract class sortingLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.sortingLocationID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingWorksheetLineSplit.lastModifiedDateTime>
  {
  }
}
