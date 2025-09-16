// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.TransitSiteStatusByCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;

[PXHidden]
[TransitSiteStatusByCostCenter.Accumulator(BqlTable = typeof (INSiteStatusByCostCenter))]
public class TransitSiteStatusByCostCenter : INSiteStatusByCostCenter
{
  [PXForeignSelector(typeof (INTran.siteID))]
  [Site(true, IsKey = true)]
  [PXDefault]
  public override int? SiteID { get; set; }

  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    TransitSiteStatusByCostCenter>.By<TransitSiteStatusByCostCenter.inventoryID, TransitSiteStatusByCostCenter.subItemID, TransitSiteStatusByCostCenter.siteID, TransitSiteStatusByCostCenter.costCenterID>
  {
    public static TransitSiteStatusByCostCenter Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? costCenterID,
      PKFindOptions options = 0)
    {
      return (TransitSiteStatusByCostCenter) PrimaryKeyOf<TransitSiteStatusByCostCenter>.By<TransitSiteStatusByCostCenter.inventoryID, TransitSiteStatusByCostCenter.subItemID, TransitSiteStatusByCostCenter.siteID, TransitSiteStatusByCostCenter.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) costCenterID, options);
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.subItemID>
  {
  }

  public new abstract class siteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.siteID>
  {
  }

  public new abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.costCenterID>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.qtyOnHand>
  {
  }

  public new abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.qtyAvail>
  {
  }

  public new abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.qtyHardAvail>
  {
  }

  public new abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TransitSiteStatusByCostCenter.qtyActual>
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
      columns.Update<TransitSiteStatusByCostCenter.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitSiteStatusByCostCenter.qtyAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitSiteStatusByCostCenter.qtyHardAvail>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<TransitSiteStatusByCostCenter.qtyActual>((PXDataFieldAssign.AssignBehavior) 1);
      TransitSiteStatusByCostCenter a = (TransitSiteStatusByCostCenter) row;
      Decimal? qtyOnHand1 = a.QtyOnHand;
      Decimal num = 0M;
      if (qtyOnHand1.GetValueOrDefault() < num & qtyOnHand1.HasValue)
      {
        PXAccumulatorCollection accumulatorCollection = columns;
        Decimal? qtyOnHand2 = a.QtyOnHand;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) (qtyOnHand2.HasValue ? new Decimal?(-qtyOnHand2.GetValueOrDefault()) : new Decimal?());
        accumulatorCollection.Restrict<TransitSiteStatusByCostCenter.qtyOnHand>((PXComp) 3, (object) local);
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
        TransitSiteStatusByCostCenter b = (TransitSiteStatusByCostCenter) row;
        TransitSiteStatusByCostCenter a = (TransitSiteStatusByCostCenter) PrimaryKeyOf<TransitSiteStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<TransitSiteStatusByCostCenter.inventoryID, TransitSiteStatusByCostCenter.subItemID, TransitSiteStatusByCostCenter.siteID, TransitSiteStatusByCostCenter.costCenterID>>.Find(cache.Graph, (TypeArrayOf<IBqlField>.IFilledWith<TransitSiteStatusByCostCenter.inventoryID, TransitSiteStatusByCostCenter.subItemID, TransitSiteStatusByCostCenter.siteID, TransitSiteStatusByCostCenter.costCenterID>) b, (PKFindOptions) 0);
        TransitSiteStatusByCostCenter statusByCostCenter = this.Aggregate<TransitSiteStatusByCostCenter>(cache, a, b);
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
            PXForeignSelectorAttribute.GetValueExt<TransitSiteStatusByCostCenter.inventoryID>(cache, row),
            PXForeignSelectorAttribute.GetValueExt<TransitSiteStatusByCostCenter.subItemID>(cache, row)
          });
        throw;
      }
    }

    public override void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
    {
      if (PXDBOperationExt.Command(e.Operation) == 2 && e.TranStatus == null)
      {
        TransitSiteStatusByCostCenter row = (TransitSiteStatusByCostCenter) e.Row;
        string str = (string) null;
        Decimal? qtyOnHand = row.QtyOnHand;
        Decimal num = 0M;
        if (qtyOnHand.GetValueOrDefault() < num & qtyOnHand.HasValue)
          str = "The document cannot be released. The quantity in transit for the '{0} {1}' item will become negative. To proceed, adjust the quantity of the item in the document.";
        if (str != null)
          throw new PXException(str, new object[2]
          {
            PXForeignSelectorAttribute.GetValueExt<TransitSiteStatusByCostCenter.inventoryID>(cache, e.Row),
            PXForeignSelectorAttribute.GetValueExt<TransitSiteStatusByCostCenter.subItemID>(cache, e.Row)
          });
      }
      base.RowPersisted(cache, e);
    }
  }
}
