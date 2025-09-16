// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOShipmentItemAvailabilityExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SOShipmentItemAvailabilityExtension : 
  ItemAvailabilityExtension<SOShipmentEntry, SOShipLine, PX.Objects.SO.SOShipLineSplit>
{
  protected override PX.Objects.SO.SOShipLineSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<SOShipmentLineSplittingExtension>().EnsureSplit(row);
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowPersistingEvents) ((PXGraph) this.Base).RowPersisting).AddAbstractHandler<PX.Objects.SO.SOShipLineSplit>(new Action<AbstractEvents.IRowPersisting<PX.Objects.SO.SOShipLineSplit>>(this.EventHandler));
  }

  protected virtual void EventHandler(AbstractEvents.IRowPersisting<PX.Objects.SO.SOShipLineSplit> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    int num1;
    if (e.Row.IsStockItem.GetValueOrDefault())
    {
      Decimal? baseQty = e.Row.BaseQty;
      Decimal num2 = 0M;
      num1 = !(baseQty.GetValueOrDefault() == num2 & baseQty.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.SO.SOShipLineSplit.subItemID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.SO.SOShipLineSplit.locationID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected override Decimal GetUnitRate(SOShipLine line)
  {
    return this.GetUnitRate<SOShipLine.inventoryID, SOShipLine.uOM>(line);
  }

  protected override string GetStatus(SOShipLine line)
  {
    string status = string.Empty;
    IStatus availability = this.FetchWithLineUOM(line, true, line.CostCenterID);
    if (availability != null)
    {
      status = this.FormatStatus(availability, line.UOM);
      this.Check((ILSMaster) line, availability);
    }
    return status;
  }

  private string FormatStatus(IStatus availability, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}", new object[4]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail)
    });
  }

  public virtual void OrderCheck(SOShipLine line)
  {
    if (((PXGraph) this.Base).FindImplementation<SOShipmentLineSplittingExtension>().UnattendedMode || line.OrigOrderNbr == null)
      return;
    SOLineSplit2 soLineSplit2 = PXResultset<SOLineSplit2>.op_Implicit(PXSelectBase<SOLineSplit2, PXSelect<SOLineSplit2, Where<SOLineSplit2.orderType, Equal<Current<SOShipLine.origOrderType>>, And<SOLineSplit2.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>, And<SOLineSplit2.lineNbr, Equal<Current<SOShipLine.origLineNbr>>, And<SOLineSplit2.splitLineNbr, Equal<Current<SOShipLine.origSplitLineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) line
    }, Array.Empty<object>()));
    SOLine2 soLine2 = PXResultset<SOLine2>.op_Implicit(PXSelectBase<SOLine2, PXSelect<SOLine2, Where<SOLine2.orderType, Equal<Current<SOShipLine.origOrderType>>, And<SOLine2.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>, And<SOLine2.lineNbr, Equal<Current<SOShipLine.origLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) line
    }, Array.Empty<object>()));
    if (soLineSplit2 != null && soLine2 != null)
    {
      Decimal? nullable1;
      Decimal? nullable2;
      Decimal? nullable3;
      Decimal? nullable4;
      if (soLineSplit2.IsAllocated.GetValueOrDefault())
      {
        Decimal? qty = soLineSplit2.Qty;
        nullable1 = soLine2.CompleteQtyMax;
        nullable2 = qty.HasValue & nullable1.HasValue ? new Decimal?(qty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal num = (Decimal) 100;
        Decimal? nullable5;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable5 = nullable1;
        }
        else
          nullable5 = new Decimal?(nullable2.GetValueOrDefault() / num);
        nullable3 = nullable5;
        nullable4 = soLineSplit2.ShippedQty;
        if (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue)
          throw new PXSetPropertyException((IBqlTable) line, "For item '{0} {1}' in order '{2} {3}', the quantity shipped is greater than the quantity allocated.", new object[4]
          {
            ((PXCache) this.LineCache).GetValueExt<SOShipLine.inventoryID>((object) line),
            ((PXCache) this.LineCache).GetValueExt<SOShipLine.subItemID>((object) line),
            ((PXCache) this.LineCache).GetValueExt<SOShipLine.origOrderType>((object) line),
            ((PXCache) this.LineCache).GetValueExt<SOShipLine.origOrderNbr>((object) line)
          });
      }
      short? lineSign = soLine2.LineSign;
      Decimal? nullable6;
      if (!lineSign.HasValue)
      {
        nullable2 = new Decimal?();
        nullable6 = nullable2;
      }
      else
        nullable6 = new Decimal?((Decimal) lineSign.GetValueOrDefault());
      nullable4 = nullable6;
      nullable3 = soLine2.OrderQty;
      Decimal? nullable7;
      if (!(nullable4.HasValue & nullable3.HasValue))
      {
        nullable2 = new Decimal?();
        nullable7 = nullable2;
      }
      else
        nullable7 = new Decimal?(nullable4.GetValueOrDefault() * nullable3.GetValueOrDefault());
      Decimal? nullable8 = nullable7;
      lineSign = soLine2.LineSign;
      Decimal? nullable9;
      if (!lineSign.HasValue)
      {
        nullable2 = new Decimal?();
        nullable9 = nullable2;
      }
      else
        nullable9 = new Decimal?((Decimal) lineSign.GetValueOrDefault());
      nullable3 = nullable9;
      nullable4 = soLine2.ShippedQty;
      Decimal? nullable10;
      if (!(nullable3.HasValue & nullable4.HasValue))
      {
        nullable2 = new Decimal?();
        nullable10 = nullable2;
      }
      else
        nullable10 = new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault());
      nullable2 = nullable8;
      nullable1 = soLine2.CompleteQtyMax;
      nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
      nullable3 = nullable10;
      Decimal? nullable11;
      if (!(nullable4.HasValue & nullable3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable11 = nullable1;
      }
      else
        nullable11 = new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault());
      nullable1 = nullable11;
      if (!(PXDBPriceCostAttribute.Round(nullable1.Value) < 0M))
      {
        Decimal? qty = soLineSplit2.Qty;
        Decimal? nullable12 = soLine2.CompleteQtyMax;
        nullable1 = qty.HasValue & nullable12.HasValue ? new Decimal?(qty.GetValueOrDefault() * nullable12.GetValueOrDefault() / 100M) : new Decimal?();
        nullable2 = soLineSplit2.ShippedQty;
        Decimal? nullable13;
        if (!(nullable1.HasValue & nullable2.HasValue))
        {
          nullable12 = new Decimal?();
          nullable13 = nullable12;
        }
        else
          nullable13 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
        nullable12 = nullable13;
        if (!(PXDBPriceCostAttribute.Round(nullable12.Value) < 0M))
          return;
      }
      throw new PXSetPropertyException((IBqlTable) line, "Item '{0} {1}' in order '{2} {3}' quantity shipped is greater than quantity ordered.", new object[4]
      {
        ((PXCache) this.LineCache).GetValueExt<SOShipLine.inventoryID>((object) line),
        ((PXCache) this.LineCache).GetValueExt<SOShipLine.subItemID>((object) line),
        ((PXCache) this.LineCache).GetValueExt<SOShipLine.origOrderType>((object) line),
        ((PXCache) this.LineCache).GetValueExt<SOShipLine.origOrderNbr>((object) line)
      });
    }
  }

  public override void Check(ILSMaster row, int? costCenterID)
  {
    base.Check(row, costCenterID);
    switch (row)
    {
      case SOShipLine line2:
        try
        {
          this.OrderCheck(line2);
          break;
        }
        catch (PXSetPropertyException ex)
        {
          ((PXCache) this.LineCache).RaiseExceptionHandling<SOShipLine.shippedQty>((object) line2, (object) line2.ShippedQty, (Exception) ex);
          break;
        }
      case PX.Objects.SO.SOShipLineSplit soShipLineSplit:
        SOShipLine line1 = PXParentAttribute.SelectParent<SOShipLine>((PXCache) this.SplitCache, (object) soShipLineSplit);
        try
        {
          this.OrderCheck(line1);
          break;
        }
        catch (PXSetPropertyException ex)
        {
          ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOShipLineSplit.qty>((object) soShipLineSplit, (object) soShipLineSplit.Qty, (Exception) ex);
          break;
        }
    }
  }

  protected override void Check(ILSMaster row, IStatus availability)
  {
    base.Check(row, availability);
    foreach (PXExceptionInfo ei in this.GetCheckErrorsQtyOnHand(row, availability))
      this.RaiseQtyExceptionHandling(row, ei, row.Qty);
  }

  protected virtual IEnumerable<PXExceptionInfo> GetCheckErrorsQtyOnHand(
    ILSMaster row,
    IStatus availability)
  {
    SOShipmentItemAvailabilityExtension availabilityExtension = this;
    if (!availabilityExtension.IsAvailableOnHandQty(row, availability))
    {
      string messageQtyOnHand = availabilityExtension.GetErrorMessageQtyOnHand(availabilityExtension.GetStatusLevel(availability));
      if (messageQtyOnHand != null)
        yield return new PXExceptionInfo((PXErrorLevel) 2, messageQtyOnHand, Array.Empty<object>());
    }
  }

  protected virtual bool IsAvailableOnHandQty(ILSMaster row, IStatus availability)
  {
    short? invtMult = row.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      Decimal? nullable = row.BaseQty;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue && availability != null)
      {
        Decimal? qtyOnHand = availability.QtyOnHand;
        Decimal? qty = row.Qty;
        nullable = qtyOnHand.HasValue & qty.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() - qty.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
        {
          PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current;
          int num3;
          if (current == null)
          {
            num3 = 0;
          }
          else
          {
            bool? confirmed = current.Confirmed;
            bool flag = false;
            num3 = confirmed.GetValueOrDefault() == flag & confirmed.HasValue ? 1 : 0;
          }
          if (num3 != 0)
            return false;
        }
      }
    }
    return true;
  }

  protected override void Summarize(IStatus allocated, IStatus existing)
  {
    base.Summarize(allocated, existing);
    allocated.QtyAvail = allocated.QtyHardAvail;
  }

  protected override void ExcludeCurrent(
    ILSDetail currentSplit,
    IStatus allocated,
    AvailabilitySigns signs)
  {
    Decimal? nullable1;
    Decimal? nullable2;
    if (Sign.op_Inequality(signs.SignQtyHardAvail, Sign.Zero))
    {
      IStatus status1 = allocated;
      nullable1 = status1.QtyAvail;
      Sign signQtyHardAvail1 = signs.SignQtyHardAvail;
      nullable2 = currentSplit.BaseQty;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      Decimal num1 = Sign.op_Multiply(signQtyHardAvail1, valueOrDefault1);
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() - num1);
      status1.QtyAvail = nullable3;
      IStatus status2 = allocated;
      nullable1 = status2.QtyNotAvail;
      Sign signQtyHardAvail2 = signs.SignQtyHardAvail;
      nullable2 = currentSplit.BaseQty;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num2 = Sign.op_Multiply(signQtyHardAvail2, valueOrDefault2);
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num2);
      status2.QtyNotAvail = nullable4;
      IStatus status3 = allocated;
      nullable1 = status3.QtyHardAvail;
      Sign signQtyHardAvail3 = signs.SignQtyHardAvail;
      nullable2 = currentSplit.BaseQty;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal num3 = Sign.op_Multiply(signQtyHardAvail3, valueOrDefault3);
      Decimal? nullable5;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable1.GetValueOrDefault() - num3);
      status3.QtyHardAvail = nullable5;
    }
    foreach (PX.Objects.SO.Unassigned.SOShipLineSplit unassignedDetail in this.SelectUnassignedDetails((PX.Objects.SO.SOShipLineSplit) currentSplit))
    {
      if (Sign.op_Inequality(signs.SignQtyHardAvail, Sign.Zero))
      {
        if (currentSplit.LocationID.HasValue)
        {
          int? locationId1 = currentSplit.LocationID;
          int? locationId2 = unassignedDetail.LocationID;
          if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
            continue;
        }
        if (currentSplit.LotSerialNbr == null || string.IsNullOrEmpty(unassignedDetail.LotSerialNbr) || string.Equals(currentSplit.LotSerialNbr, unassignedDetail.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))
        {
          IStatus status4 = allocated;
          nullable1 = status4.QtyAvail;
          Sign signQtyHardAvail4 = signs.SignQtyHardAvail;
          nullable2 = unassignedDetail.BaseQty;
          Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
          Decimal num4 = Sign.op_Multiply(signQtyHardAvail4, valueOrDefault4);
          Decimal? nullable6;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable6 = nullable2;
          }
          else
            nullable6 = new Decimal?(nullable1.GetValueOrDefault() - num4);
          status4.QtyAvail = nullable6;
          IStatus status5 = allocated;
          nullable1 = status5.QtyHardAvail;
          Sign signQtyHardAvail5 = signs.SignQtyHardAvail;
          nullable2 = unassignedDetail.BaseQty;
          Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
          Decimal num5 = Sign.op_Multiply(signQtyHardAvail5, valueOrDefault5);
          Decimal? nullable7;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable7 = nullable2;
          }
          else
            nullable7 = new Decimal?(nullable1.GetValueOrDefault() - num5);
          status5.QtyHardAvail = nullable7;
        }
      }
    }
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2026 R1.", true)]
  public bool? AdvancedCheck
  {
    get => new bool?();
    set
    {
    }
  }

  protected override void Optimize()
  {
    base.Optimize();
    foreach (PXResult<SOShipLine, INSiteStatusByCostCenter, INLocationStatusByCostCenter, INLotSerialStatusByCostCenter> pxResult in PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSiteStatusByCostCenter>.On<SOShipLine.FK.SiteStatusByCostCenter>>, FbqlJoins.Left<INLocationStatusByCostCenter>.On<SOShipLine.FK.LocationStatusByCostCenter>>, FbqlJoins.Left<INLotSerialStatusByCostCenter>.On<SOShipLine.FK.LotSerialStatusByCostCenter>>>.Where<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, SOShipLine>, PX.Objects.SO.SOShipment, SOShipLine>.SameAsCurrent>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      SOShipLine soShipLine;
      INSiteStatusByCostCenter statusByCostCenter1;
      INLocationStatusByCostCenter statusByCostCenter2;
      INLotSerialStatusByCostCenter statusByCostCenter3;
      pxResult.Deconstruct(ref soShipLine, ref statusByCostCenter1, ref statusByCostCenter2, ref statusByCostCenter3);
      INSiteStatusByCostCenter statusByCostCenter4 = statusByCostCenter1;
      INLocationStatusByCostCenter statusByCostCenter5 = statusByCostCenter2;
      INLotSerialStatusByCostCenter statusByCostCenter6 = statusByCostCenter3;
      PrimaryKeyOf<INSiteStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>) statusByCostCenter4, false);
      if (statusByCostCenter5.LocationID.HasValue)
        PrimaryKeyOf<INLocationStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INLocationStatusByCostCenter.inventoryID, INLocationStatusByCostCenter.subItemID, INLocationStatusByCostCenter.siteID, INLocationStatusByCostCenter.locationID, INLocationStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INLocationStatusByCostCenter.inventoryID, INLocationStatusByCostCenter.subItemID, INLocationStatusByCostCenter.siteID, INLocationStatusByCostCenter.locationID, INLocationStatusByCostCenter.costCenterID>) statusByCostCenter5, false);
      if (statusByCostCenter6 != null && statusByCostCenter6.LotSerialNbr != null)
        PrimaryKeyOf<INLotSerialStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INLotSerialStatusByCostCenter.inventoryID, INLotSerialStatusByCostCenter.subItemID, INLotSerialStatusByCostCenter.siteID, INLotSerialStatusByCostCenter.locationID, INLotSerialStatusByCostCenter.lotSerialNbr, INLotSerialStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INLotSerialStatusByCostCenter.inventoryID, INLotSerialStatusByCostCenter.subItemID, INLotSerialStatusByCostCenter.siteID, INLotSerialStatusByCostCenter.locationID, INLotSerialStatusByCostCenter.lotSerialNbr, INLotSerialStatusByCostCenter.costCenterID>) statusByCostCenter6, false);
    }
  }

  public virtual List<PX.Objects.SO.Unassigned.SOShipLineSplit> SelectUnassignedDetails(
    PX.Objects.SO.SOShipLineSplit assignedSplit)
  {
    PX.Objects.SO.Unassigned.SOShipLineSplit soShipLineSplit = new PX.Objects.SO.Unassigned.SOShipLineSplit()
    {
      ShipmentNbr = assignedSplit.ShipmentNbr,
      LineNbr = assignedSplit.LineNbr,
      SplitLineNbr = assignedSplit.SplitLineNbr
    };
    return PXParentAttribute.SelectSiblings((PXCache) GraphHelper.Caches<PX.Objects.SO.Unassigned.SOShipLineSplit>((PXGraph) this.Base), (object) soShipLineSplit, this.IsOptimizationEnabled ? typeof (PX.Objects.SO.SOShipment) : typeof (SOShipLine)).Cast<PX.Objects.SO.Unassigned.SOShipLineSplit>().Where<PX.Objects.SO.Unassigned.SOShipLineSplit>((Func<PX.Objects.SO.Unassigned.SOShipLineSplit, bool>) (us =>
    {
      int? inventoryId1 = us.InventoryID;
      int? inventoryId2 = assignedSplit.InventoryID;
      if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
        return false;
      int? lineNbr1 = us.LineNbr;
      int? lineNbr2 = assignedSplit.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).ToList<PX.Objects.SO.Unassigned.SOShipLineSplit>();
  }

  protected override void RaiseQtyExceptionHandling(
    SOShipLine line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<SOShipLine.shippedQty>((object) line, (object) newValue, (Exception) new PXSetPropertyException((IBqlTable) line, ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetStateExt<SOShipLine.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<SOShipLine.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<SOShipLine.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<SOShipLine.locationID>((object) line),
      ((PXCache) this.LineCache).GetValue<SOShipLine.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    PX.Objects.SO.SOShipLineSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOShipLineSplit.qty>((object) split, (object) newValue, (Exception) new PXSetPropertyException((IBqlTable) split, ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOShipLineSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOShipLineSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOShipLineSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOShipLineSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<PX.Objects.SO.SOShipLineSplit.lotSerialNbr>((object) split)
    }));
  }
}
