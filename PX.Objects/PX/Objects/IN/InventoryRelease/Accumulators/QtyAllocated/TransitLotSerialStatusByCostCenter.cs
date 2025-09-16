// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.TransitLotSerialStatusByCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[TransitLotSerialStatusByCostCenter.Accumulator]
public class TransitLotSerialStatusByCostCenter : INLotSerialStatusByCostCenter
{
  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXSelectorMarker(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<TransitLotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>), CacheGlobal = true)]
  [PXDefault]
  public override int? InventoryID { get; set; }

  [SubItem(IsKey = true)]
  [PXForeignSelector(typeof (INTran.subItemID))]
  [PXDefault]
  public override int? SubItemID { get; set; }

  [Site(true, IsKey = true)]
  [PXDefault]
  public override int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (INTransitLine.costSiteID))]
  public override int? LocationID { get; set; }

  [PXDBString(100, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public override 
  #nullable disable
  string LotSerialNbr { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<TransitLotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>))]
  public virtual string LotSerClassID { get; set; }

  [PXDate]
  public override DateTime? ExpireDate { get; set; }

  [PXDBDate]
  public override DateTime? ReceiptDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLotSerClass.lotSerClassID, IBqlString>.IsEqual<BqlField<TransitLotSerialStatusByCostCenter.lotSerClassID, IBqlString>.FromCurrent>>))]
  public override string LotSerTrack { get; set; }

  public new class PK : 
    PrimaryKeyOf<TransitLotSerialStatusByCostCenter>.By<TransitLotSerialStatusByCostCenter.inventoryID, TransitLotSerialStatusByCostCenter.subItemID, TransitLotSerialStatusByCostCenter.siteID, TransitLotSerialStatusByCostCenter.locationID, TransitLotSerialStatusByCostCenter.lotSerialNbr, TransitLotSerialStatusByCostCenter.costCenterID>
  {
    public static TransitLotSerialStatusByCostCenter Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? locationID,
      string lotSerialNbr,
      int? costCenterID,
      PKFindOptions options = 0)
    {
      return (TransitLotSerialStatusByCostCenter) PrimaryKeyOf<TransitLotSerialStatusByCostCenter>.By<TransitLotSerialStatusByCostCenter.inventoryID, TransitLotSerialStatusByCostCenter.subItemID, TransitLotSerialStatusByCostCenter.siteID, TransitLotSerialStatusByCostCenter.locationID, TransitLotSerialStatusByCostCenter.lotSerialNbr, TransitLotSerialStatusByCostCenter.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) lotSerialNbr, (object) costCenterID, options);
    }
  }

  public new static class FK
  {
    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<TransitLotSerialStatusByCostCenter>.By<TransitLotSerialStatusByCostCenter.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<TransitLotSerialStatusByCostCenter>.By<TransitLotSerialStatusByCostCenter.inventoryID, TransitLotSerialStatusByCostCenter.subItemID, TransitLotSerialStatusByCostCenter.siteID, TransitLotSerialStatusByCostCenter.locationID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<TransitLotSerialStatusByCostCenter>.By<TransitLotSerialStatusByCostCenter.subItemID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<TransitLotSerialStatusByCostCenter>.By<TransitLotSerialStatusByCostCenter.inventoryID>
    {
    }

    public class ItemLotSerial : 
      PrimaryKeyOf<INItemLotSerial>.By<INItemLotSerial.inventoryID, INItemLotSerial.lotSerialNbr>.ForeignKeyOf<TransitLotSerialStatusByCostCenter>.By<TransitLotSerialStatusByCostCenter.inventoryID, TransitLotSerialStatusByCostCenter.lotSerialNbr>
    {
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.subItemID>
  {
  }

  public new abstract class siteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.lotSerialNbr>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.costCenterID>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.lotSerClassID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.qtyOnHand>
  {
  }

  public new abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.qtyAvail>
  {
  }

  public new abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.qtyHardAvail>
  {
  }

  public new abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.qtyActual>
  {
  }

  public new abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.expireDate>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.receiptDate>
  {
  }

  public new abstract class lotSerTrack : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TransitLotSerialStatusByCostCenter.lotSerTrack>
  {
  }

  public class AccumulatorAttribute : StatusAccumulatorAttribute
  {
    public AccumulatorAttribute() => this.SingleRecord = true;

    protected override bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.Update<TransitLotSerialStatusByCostCenter.receiptDate>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<TransitLotSerialStatusByCostCenter.lotSerTrack>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<TransitLotSerialStatusByCostCenter.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitLotSerialStatusByCostCenter.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitLotSerialStatusByCostCenter.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitLotSerialStatusByCostCenter.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      TransitLotSerialStatusByCostCenter statusByCostCenter = (TransitLotSerialStatusByCostCenter) row;
      Decimal? qtyOnHand1 = statusByCostCenter.QtyOnHand;
      Decimal num = 0M;
      if (qtyOnHand1.GetValueOrDefault() < num & qtyOnHand1.HasValue)
      {
        PXAccumulatorCollection accumulatorCollection = columns;
        Decimal? qtyOnHand2 = statusByCostCenter.QtyOnHand;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) (qtyOnHand2.HasValue ? new Decimal?(-qtyOnHand2.GetValueOrDefault()) : new Decimal?());
        accumulatorCollection.Restrict<TransitLotSerialStatusByCostCenter.qtyOnHand>((PXComp) 3, (object) local);
      }
      return true;
    }

    public override bool PersistInserted(PXCache cache, object row)
    {
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXLockViolationException ex)
      {
        TransitLotSerialStatusByCostCenter b = (TransitLotSerialStatusByCostCenter) row;
        TransitLotSerialStatusByCostCenter a = (TransitLotSerialStatusByCostCenter) PrimaryKeyOf<TransitLotSerialStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TransitLotSerialStatusByCostCenter.inventoryID, TransitLotSerialStatusByCostCenter.subItemID, TransitLotSerialStatusByCostCenter.siteID, TransitLotSerialStatusByCostCenter.locationID, TransitLotSerialStatusByCostCenter.lotSerialNbr, TransitLotSerialStatusByCostCenter.costCenterID>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<TransitLotSerialStatusByCostCenter.inventoryID, TransitLotSerialStatusByCostCenter.subItemID, TransitLotSerialStatusByCostCenter.siteID, TransitLotSerialStatusByCostCenter.locationID, TransitLotSerialStatusByCostCenter.lotSerialNbr, TransitLotSerialStatusByCostCenter.costCenterID>) b, (PKFindOptions) 0);
        TransitLotSerialStatusByCostCenter statusByCostCenter = this.Aggregate<TransitLotSerialStatusByCostCenter>(cache, a, b);
        string str = (string) null;
        Decimal? qtyOnHand = b.QtyOnHand;
        Decimal num1 = 0M;
        if (qtyOnHand.GetValueOrDefault() < num1 & qtyOnHand.HasValue)
        {
          qtyOnHand = statusByCostCenter.QtyOnHand;
          Decimal num2 = 0M;
          if (qtyOnHand.GetValueOrDefault() < num2 & qtyOnHand.HasValue)
            str = "The document cannot be released. The quantity in transit for the '{0} {1}' item with the '{2}' lot/serial number will become negative. To proceed, adjust the quantity of the item in the document.";
        }
        if (str != null)
          throw new PXException(str, new object[3]
          {
            PXForeignSelectorAttribute.GetValueExt<TransitLotSerialStatusByCostCenter.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<TransitLotSerialStatusByCostCenter.subItemID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<TransitLotSerialStatusByCostCenter.lotSerialNbr>(cache, row)
          });
        throw;
      }
    }

    public override void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) == 2 && e.TranStatus == null)
      {
        TransitLotSerialStatusByCostCenter row = (TransitLotSerialStatusByCostCenter) e.Row;
        string str = (string) null;
        Decimal? qtyOnHand = row.QtyOnHand;
        Decimal num = 0M;
        if (qtyOnHand.GetValueOrDefault() < num & qtyOnHand.HasValue)
          str = "The document cannot be released. The quantity in transit for the '{0} {1}' item with the '{2}' lot/serial number will become negative. To proceed, adjust the quantity of the item in the document.";
        if (str != null)
          throw new PXException(str, new object[3]
          {
            PXForeignSelectorAttribute.GetValueExt<TransitLotSerialStatusByCostCenter.inventoryID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<TransitLotSerialStatusByCostCenter.subItemID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<TransitLotSerialStatusByCostCenter.lotSerialNbr>(cache, e.Row)
          });
      }
      base.RowPersisted(cache, e);
    }
  }
}
