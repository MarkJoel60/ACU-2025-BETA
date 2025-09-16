// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.TransitLocationStatusByCostCenter
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
[TransitLocationStatusByCostCenter.Accumulator]
public class TransitLocationStatusByCostCenter : INLocationStatusByCostCenter
{
  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXSelectorMarker(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<TransitLocationStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>), CacheGlobal = true)]
  [PXDefault]
  public override int? InventoryID { get; set; }

  [SubItem(IsKey = true)]
  [PXForeignSelector(typeof (INTran.subItemID))]
  [PXDefault]
  public override int? SubItemID { get; set; }

  [Site(true, IsKey = true)]
  [PXForeignSelector(typeof (INTran.siteID))]
  [PXDefault]
  public override int? SiteID
  {
    get => base.SiteID;
    set => base.SiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.locationID))]
  [PXDBDefault(typeof (INTransitLine.costSiteID))]
  public override int? LocationID
  {
    get => base.LocationID;
    set => base.LocationID = value;
  }

  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    TransitLocationStatusByCostCenter>.By<TransitLocationStatusByCostCenter.inventoryID, TransitLocationStatusByCostCenter.subItemID, TransitLocationStatusByCostCenter.siteID, TransitLocationStatusByCostCenter.locationID, TransitLocationStatusByCostCenter.costCenterID>
  {
    public static TransitLocationStatusByCostCenter Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? locationID,
      int? costCenterID,
      PKFindOptions options = 0)
    {
      return (TransitLocationStatusByCostCenter) PrimaryKeyOf<TransitLocationStatusByCostCenter>.By<TransitLocationStatusByCostCenter.inventoryID, TransitLocationStatusByCostCenter.subItemID, TransitLocationStatusByCostCenter.siteID, TransitLocationStatusByCostCenter.locationID, TransitLocationStatusByCostCenter.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) costCenterID, options);
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.subItemID>
  {
  }

  public new abstract class siteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.locationID>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.costCenterID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.qtyOnHand>
  {
  }

  public new abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.qtyAvail>
  {
  }

  public new abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.qtyHardAvail>
  {
  }

  public new abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitLocationStatusByCostCenter.qtyActual>
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
      columns.Update<TransitLocationStatusByCostCenter.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitLocationStatusByCostCenter.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitLocationStatusByCostCenter.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitLocationStatusByCostCenter.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      TransitLocationStatusByCostCenter a = (TransitLocationStatusByCostCenter) row;
      Decimal? qtyOnHand1 = a.QtyOnHand;
      Decimal num = 0M;
      if (qtyOnHand1.GetValueOrDefault() < num & qtyOnHand1.HasValue)
      {
        PXAccumulatorCollection accumulatorCollection = columns;
        Decimal? qtyOnHand2 = a.QtyOnHand;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) (qtyOnHand2.HasValue ? new Decimal?(-qtyOnHand2.GetValueOrDefault()) : new Decimal?());
        accumulatorCollection.Restrict<TransitLocationStatusByCostCenter.qtyOnHand>((PXComp) 3, (object) local);
      }
      if (cache.GetStatus(row) != 2 || !this.IsZero((IStatus) a))
        return true;
      cache.SetStatus(row, (PXEntryStatus) 4);
      return false;
    }

    public override bool PersistInserted(PXCache cache, object row)
    {
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXLockViolationException ex)
      {
        TransitLocationStatusByCostCenter b = (TransitLocationStatusByCostCenter) row;
        TransitLocationStatusByCostCenter a = (TransitLocationStatusByCostCenter) PrimaryKeyOf<TransitLocationStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TransitLocationStatusByCostCenter.inventoryID, TransitLocationStatusByCostCenter.subItemID, TransitLocationStatusByCostCenter.siteID, TransitLocationStatusByCostCenter.locationID, TransitLocationStatusByCostCenter.costCenterID>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<TransitLocationStatusByCostCenter.inventoryID, TransitLocationStatusByCostCenter.subItemID, TransitLocationStatusByCostCenter.siteID, TransitLocationStatusByCostCenter.locationID, TransitLocationStatusByCostCenter.costCenterID>) b, (PKFindOptions) 0);
        TransitLocationStatusByCostCenter statusByCostCenter = this.Aggregate<TransitLocationStatusByCostCenter>(cache, a, b);
        string str = (string) null;
        Decimal? qtyOnHand = b.QtyOnHand;
        Decimal num1 = 0M;
        if (qtyOnHand.GetValueOrDefault() < num1 & qtyOnHand.HasValue)
        {
          qtyOnHand = statusByCostCenter.QtyOnHand;
          Decimal num2 = 0M;
          if (qtyOnHand.GetValueOrDefault() < num2 & qtyOnHand.HasValue)
            str = "The document cannot be released. The quantity in transit for the '{0} {1}' item will become negative. To proceed, adjust the quantity of the item in the document.";
        }
        if (str != null)
          throw new PXException(str, new object[2]
          {
            PXForeignSelectorAttribute.GetValueExt<TransitLocationStatusByCostCenter.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<TransitLocationStatusByCostCenter.subItemID>(cache, row)
          });
        throw;
      }
    }

    public override void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) == 2 && e.TranStatus == null)
      {
        TransitLocationStatusByCostCenter row = (TransitLocationStatusByCostCenter) e.Row;
        string str = (string) null;
        Decimal? qtyOnHand = row.QtyOnHand;
        Decimal num = 0M;
        if (qtyOnHand.GetValueOrDefault() < num & qtyOnHand.HasValue)
          str = "The document cannot be released. The quantity in transit for the '{0} {1}' item will become negative. To proceed, adjust the quantity of the item in the document.";
        if (str != null)
          throw new PXException(str, new object[2]
          {
            PXForeignSelectorAttribute.GetValueExt<TransitLocationStatusByCostCenter.inventoryID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<TransitLocationStatusByCostCenter.subItemID>(cache, e.Row)
          });
      }
      base.RowPersisted(cache, e);
    }
  }
}
