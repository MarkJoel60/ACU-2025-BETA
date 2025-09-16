// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickerListEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
public class SOPickerListEntry : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXParent(typeof (SOPickerListEntry.FK.Worksheet))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (SOPicker.pickerNbr))]
  [PXUIField(DisplayName = "Picker Nbr.")]
  [PXParent(typeof (SOPickerListEntry.FK.Picker))]
  public virtual int? PickerNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOPicker))]
  [PXUIField(DisplayName = "Entry Nbr.")]
  public virtual int? EntryNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  public virtual string ShipmentNbr { get; set; }

  [Site(Enabled = false)]
  [PXForeignReference(typeof (SOPickerListEntry.FK.Site))]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [Location(typeof (SOPickerListEntry.siteID), Enabled = false)]
  [PXForeignReference(typeof (SOPickerListEntry.FK.Location))]
  [PXDefault]
  public virtual int? LocationID { get; set; }

  [INTote.UnassignableTote(Enabled = false)]
  [PXForeignReference(typeof (SOPickerListEntry.FK.Tote))]
  public virtual int? ToteID { get; set; }

  [Inventory(Enabled = false)]
  [PXForeignReference(typeof (SOPickerListEntry.FK.InventoryItem))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (SOPickerListEntry.inventoryID), Enabled = false)]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial", Enabled = false)]
  [PXDefault]
  public virtual string LotSerialNbr { get; set; }

  [PXDBDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial", Enabled = false)]
  public virtual DateTime? ExpireDate { get; set; }

  [INUnit(typeof (SOPickerListEntry.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string OrderLineUOM { get; set; }

  [INUnit(typeof (SOPickerListEntry.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (SOPickerListEntry.uOM), typeof (SOPickerListEntry.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBQuantity(typeof (SOPickerListEntry.uOM), typeof (SOPickerListEntry.basePickedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Picked Quantity", Enabled = false)]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BasePickedQty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [SOPickerListEntry.IsUnassigned]
  public virtual bool? IsUnassigned { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HasGeneratedLotSerialNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Quantity Confirmed", Enabled = false, FieldClass = "WMSPaperlessPicking")]
  public virtual bool? ForceCompleted { get; set; }

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
    PrimaryKeyOf<SOPickerListEntry>.By<SOPickerListEntry.worksheetNbr, SOPickerListEntry.pickerNbr, SOPickerListEntry.entryNbr>
  {
    public static SOPickerListEntry Find(
      PXGraph graph,
      string worksheetNbr,
      int? pickerNbr,
      int? entryNbr,
      PKFindOptions options = 0)
    {
      return (SOPickerListEntry) PrimaryKeyOf<SOPickerListEntry>.By<SOPickerListEntry.worksheetNbr, SOPickerListEntry.pickerNbr, SOPickerListEntry.entryNbr>.FindBy(graph, (object) worksheetNbr, (object) pickerNbr, (object) entryNbr, options);
    }
  }

  public static class FK
  {
    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.worksheetNbr>
    {
    }

    public class Picker : 
      PrimaryKeyOf<SOPicker>.By<SOPicker.worksheetNbr, SOPicker.pickerNbr>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.worksheetNbr, SOPickerListEntry.pickerNbr>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.shipmentNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.inventoryID, SOPickerListEntry.subItemID, SOPickerListEntry.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.locationID>
    {
    }

    public class Tote : 
      PrimaryKeyOf<INTote>.By<INTote.siteID, INTote.toteID>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.siteID, SOPickerListEntry.toteID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.inventoryID, SOPickerListEntry.subItemID, SOPickerListEntry.siteID, SOPickerListEntry.locationID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.subItemID>
    {
    }
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerListEntry.worksheetNbr>
  {
  }

  public abstract class pickerNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerListEntry.pickerNbr>
  {
  }

  public abstract class entryNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerListEntry.entryNbr>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerListEntry.shipmentNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerListEntry.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerListEntry.locationID>
  {
  }

  public abstract class toteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerListEntry.toteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerListEntry.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerListEntry.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerListEntry.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickerListEntry.expireDate>
  {
  }

  public abstract class orderLineUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerListEntry.orderLineUOM>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickerListEntry.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickerListEntry.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickerListEntry.baseQty>
  {
  }

  public abstract class pickedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOPickerListEntry.pickedQty>
  {
  }

  public abstract class basePickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPickerListEntry.basePickedQty>
  {
  }

  public abstract class isUnassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickerListEntry.isUnassigned>
  {
  }

  public abstract class hasGeneratedLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickerListEntry.hasGeneratedLotSerialNbr>
  {
  }

  public abstract class forceCompleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickerListEntry.forceCompleted>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPickerListEntry.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPickerListEntry.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerListEntry.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickerListEntry.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickerListEntry.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerListEntry.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickerListEntry.lastModifiedDateTime>
  {
  }

  public class IsUnassignedAttribute : 
    PXEventSubscriberAttribute,
    IPXRowInsertedSubscriber,
    IPXRowUpdatedSubscriber,
    IPXRowDeletedSubscriber
  {
    public void RowInserted(PXCache cache, PXRowInsertedEventArgs e)
    {
      SOPickerListEntry row = (SOPickerListEntry) e.Row;
      if (row.IsUnassigned.GetValueOrDefault() || !this.IsUnassignableOnIssue(cache.Graph, row.InventoryID))
        return;
      PXCache cache1 = cache;
      SOPickerListEntry assigned = row;
      Decimal? qty = row.Qty;
      Decimal? deltaQty = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
      this.UpdateUnassigned(cache1, assigned, deltaQty);
    }

    public void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
    {
      SOPickerListEntry row = (SOPickerListEntry) e.Row;
      SOPickerListEntry oldRow = (SOPickerListEntry) e.OldRow;
      if (row.IsUnassigned.GetValueOrDefault() || cache.ObjectsEqual<SOPickerListEntry.qty>((object) row, (object) oldRow) || !this.IsUnassignableOnIssue(cache.Graph, row.InventoryID))
        return;
      PXCache cache1 = cache;
      SOPickerListEntry assigned = row;
      Decimal? qty1 = oldRow.Qty;
      Decimal? qty2 = row.Qty;
      Decimal? deltaQty = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() - qty2.GetValueOrDefault()) : new Decimal?();
      this.UpdateUnassigned(cache1, assigned, deltaQty);
    }

    public void RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
    {
      SOPickerListEntry row = (SOPickerListEntry) e.Row;
      if (row.IsUnassigned.GetValueOrDefault() || !this.IsUnassignableOnIssue(cache.Graph, row.InventoryID) || ((PXCache) GraphHelper.Caches<SOPickingWorksheet>(cache.Graph)).Deleted.Cast<SOPickingWorksheet>().Any<SOPickingWorksheet>((Func<SOPickingWorksheet, bool>) (w => w.WorksheetNbr == row.WorksheetNbr)) || ((PXCache) GraphHelper.Caches<SOPicker>(cache.Graph)).Deleted.Cast<SOPicker>().Any<SOPicker>((Func<SOPicker, bool>) (p =>
      {
        string worksheetNbr1 = p.WorksheetNbr;
        int? pickerNbr1 = p.PickerNbr;
        string worksheetNbr2 = row.WorksheetNbr;
        int? pickerNbr2 = row.PickerNbr;
        string str = worksheetNbr2;
        if (!(worksheetNbr1 == str))
          return false;
        int? nullable1 = pickerNbr1;
        int? nullable2 = pickerNbr2;
        return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue;
      })))
        return;
      this.UpdateUnassigned(cache, row, row.Qty);
    }

    protected virtual void UpdateUnassigned(
      PXCache cache,
      SOPickerListEntry assigned,
      Decimal? deltaQty)
    {
      Decimal? nullable1 = deltaQty;
      Decimal num1 = 0M;
      SOPickerListEntry soPickerListEntry1;
      if (!(nullable1.GetValueOrDefault() > num1 & nullable1.HasValue) || !(this.GetLotSerClass(cache.Graph, assigned.InventoryID).LotSerTrack == "S"))
        soPickerListEntry1 = PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.isUnassigned, Equal<True>>>>, And<BqlOperand<SOPickerListEntry.worksheetNbr, IBqlString>.IsEqual<BqlField<SOPickerListEntry.worksheetNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOPickerListEntry.pickerNbr, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.pickerNbr, IBqlInt>.FromCurrent>>>, And<BqlOperand<SOPickerListEntry.inventoryID, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<SOPickerListEntry.subItemID, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<SOPickerListEntry.locationID, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.locationID, IBqlInt>.FromCurrent>>>, And<BqlOperand<SOPickerListEntry.orderLineUOM, IBqlString>.IsEqual<BqlField<SOPickerListEntry.orderLineUOM, IBqlString>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.shipmentNbr, Equal<BqlField<SOPickerListEntry.shipmentNbr, IBqlString>.FromCurrent>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.shipmentNbr, IsNull>>>>.And<BqlOperand<Current2<SOPickerListEntry.shipmentNbr>, IBqlString>.IsNull>>>>>.Config>.SelectSingleBound(cache.Graph, (object[]) new SOPickerListEntry[1]
        {
          assigned
        }, Array.Empty<object>()).TopFirst;
      else
        soPickerListEntry1 = (SOPickerListEntry) null;
      SOPickerListEntry soPickerListEntry2 = soPickerListEntry1;
      if (soPickerListEntry2 == null)
      {
        Decimal? nullable2 = deltaQty;
        Decimal num2 = 0M;
        if (nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue)
          return;
        SOPickerListEntry copy = PXCache<SOPickerListEntry>.CreateCopy(assigned);
        copy.EntryNbr = new int?();
        copy.LotSerialNbr = "";
        copy.ExpireDate = new DateTime?();
        copy.Qty = new Decimal?(0M);
        copy.BaseQty = new Decimal?(0M);
        copy.PickedQty = new Decimal?(0M);
        copy.BasePickedQty = new Decimal?(0M);
        copy.IsUnassigned = new bool?(true);
        soPickerListEntry2 = (SOPickerListEntry) cache.Insert((object) copy);
      }
      SOPickerListEntry soPickerListEntry3 = soPickerListEntry2;
      Decimal? qty1 = soPickerListEntry3.Qty;
      Decimal? nullable3 = deltaQty;
      soPickerListEntry3.Qty = qty1.HasValue & nullable3.HasValue ? new Decimal?(qty1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? qty2 = soPickerListEntry2.Qty;
      Decimal num3 = 0M;
      SOPickerListEntry soPickerListEntry4;
      if (qty2.GetValueOrDefault() == num3 & qty2.HasValue)
        soPickerListEntry4 = (SOPickerListEntry) cache.Delete((object) soPickerListEntry2);
      else
        soPickerListEntry4 = (SOPickerListEntry) cache.Update((object) soPickerListEntry2);
    }

    protected virtual bool IsUnassignableOnIssue(PXGraph graph, int? inventoryID)
    {
      return this.GetLotSerClass(graph, inventoryID).With<INLotSerClass, bool>((Func<INLotSerClass, bool>) (lsc =>
      {
        if (!(lsc.LotSerTrack != "N"))
          return false;
        return lsc.LotSerAssign == "U" || lsc.LotSerIssueMethod == "U";
      }));
    }

    protected virtual INLotSerClass GetLotSerClass(PXGraph graph, int? inventoryID)
    {
      return PX.Objects.IN.InventoryItem.PK.Find(graph, inventoryID).With<PX.Objects.IN.InventoryItem, INLotSerClass>((Func<PX.Objects.IN.InventoryItem, INLotSerClass>) (ii => INLotSerClass.PK.Find(graph, ii.LotSerClassID)));
    }
  }
}
