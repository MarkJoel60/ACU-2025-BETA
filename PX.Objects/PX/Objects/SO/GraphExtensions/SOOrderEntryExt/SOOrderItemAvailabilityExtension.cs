// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderItemAvailabilityExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrderItemAvailabilityExtension : 
  SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>
{
  protected override PX.Objects.SO.SOLineSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<SOOrderLineSplittingExtension>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(PX.Objects.SO.SOLine line)
  {
    return this.GetUnitRate<PX.Objects.SO.SOLine.inventoryID, PX.Objects.SO.SOLine.uOM>(line);
  }

  protected override string GetStatus(PX.Objects.SO.SOLine line)
  {
    string status = string.Empty;
    int num1;
    if ((line != null ? (!line.Completed.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      short? invtMult = (short?) line?.InvtMult;
      int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num2 = 0;
      num1 = !(nullable.GetValueOrDefault() == num2 & nullable.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool excludeCurrent = num1 != 0;
    IStatus availability = this.FetchWithLineUOM(line, excludeCurrent, line.CostCenterID);
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

  public override void Check(ILSMaster row, int? costCenterID)
  {
    base.Check(row, costCenterID);
    this.MemoCheck(row);
  }

  protected virtual void MemoCheck(ILSMaster row)
  {
    switch (row)
    {
      case PX.Objects.SO.SOLine soLine:
        this.MemoCheck(soLine);
        PX.Objects.SO.SOLineSplit split1 = this.EnsureSplit((ILSMaster) soLine);
        this.MemoCheck(soLine, split1, false);
        if (split1.LotSerialNbr != null)
          break;
        row.LotSerialNbr = (string) null;
        break;
      case PX.Objects.SO.SOLineSplit split2:
        PX.Objects.SO.SOLine line = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.SplitCache, (object) split2);
        this.MemoCheck(line);
        this.MemoCheck(line, split2, true);
        break;
    }
  }

  protected virtual bool MemoCheck(PX.Objects.SO.SOLine line) => this.MemoCheck(line, false);

  public virtual bool MemoCheck(PX.Objects.SO.SOLine line, bool persisting)
  {
    if (line.Operation == "I")
      return true;
    SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.ReturnedQtyResult returnedQtyResult = this.MemoCheckQty(line);
    if (!returnedQtyResult.Success)
    {
      SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.ReturnRecord[] returnRecords = returnedQtyResult.ReturnRecords;
      IEnumerable<string> values = returnRecords != null ? ((IEnumerable<SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.ReturnRecord>) returnRecords).Select<SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.ReturnRecord, string>((Func<SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.ReturnRecord, string>) (x => x.DocumentNbr)).Where<string>((Func<string, bool>) (nbr => nbr != line.OrderNbr)) : (IEnumerable<string>) null;
      this.RaiseErrorOnOrderQty((persisting ? 1 : 0) != 0, line, "The return quantity exceeds the quantity available for return for the related invoice line {0}, {1}. Decrease the quantity in the current line, or in the corresponding line of another return document or documents {2} that exist for the invoice line.", ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.invoiceNbr>((object) line), ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.inventoryID>((object) line), values == null ? (object) string.Empty : (object) string.Join(", ", values));
    }
    return returnedQtyResult.Success;
  }

  protected virtual SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.ReturnedQtyResult MemoCheckQty(
    PX.Objects.SO.SOLine line)
  {
    return this.MemoCheckQty(line.InventoryID, line.InvoiceType, line.InvoiceNbr, line.InvoiceLineNbr, line.OrigOrderType, line.OrigOrderNbr, line.OrigLineNbr);
  }

  public virtual (bool success, Decimal qtyAvailForReturn) MemoCheck(
    PX.Objects.SO.SOLine line,
    PX.Objects.SO.SOLineSplit split,
    bool triggeredBySplit,
    bool raiseException = true)
  {
    bool flag = true;
    Decimal num1 = 0M;
    if (line.InvoiceNbr != null)
    {
      int? nullable1 = split.InventoryID;
      if (nullable1.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, split.InventoryID);
        INLotSerClass inLotSerClass = inventoryItem == null || !inventoryItem.StkItem.GetValueOrDefault() ? new INLotSerClass() : INLotSerClass.PK.Find((PXGraph) this.Base, inventoryItem.LotSerClassID);
        if (EnumerableExtensions.IsIn<string>(inLotSerClass.LotSerTrack, "L", "S"))
        {
          nullable1 = split.SubItemID;
          if (nullable1.HasValue && !string.IsNullOrEmpty(split.LotSerialNbr))
          {
            PXResult<INTran, INTranSplit> pxResult1 = (PXResult<INTran, INTranSplit>) PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTranSplit>.On<INTranSplit.FK.Tran>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.sOOrderType, Equal<BqlField<PX.Objects.SO.SOLine.origOrderType, IBqlString>.AsOptional>>>>, And<BqlOperand<INTran.sOOrderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.origOrderNbr, IBqlString>.AsOptional>>>, And<BqlOperand<INTran.sOOrderLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLine.origLineNbr, IBqlInt>.AsOptional>>>, And<BqlOperand<INTran.aRDocType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.invoiceType, IBqlString>.AsOptional>>>, And<BqlOperand<INTran.aRRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.invoiceNbr, IBqlString>.AsOptional>>>, And<BqlOperand<INTranSplit.inventoryID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.inventoryID, IBqlInt>.AsOptional>>>, And<BqlOperand<INTranSplit.subItemID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.subItemID, IBqlInt>.AsOptional>>>>.And<BqlOperand<INTranSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.AsOptional>>>.Aggregate<To<GroupBy<INTranSplit.inventoryID>, GroupBy<INTranSplit.subItemID>, GroupBy<INTranSplit.lotSerialNbr>, Sum<INTranSplit.baseQty>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[2]
            {
              (object) line,
              (object) split
            }, Array.Empty<object>()));
            if (pxResult1 == null)
            {
              if (!string.IsNullOrEmpty(split.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(split.AssignedNbr, split.LotSerialNbr))
              {
                split.AssignedNbr = (string) null;
                split.LotSerialNbr = (string) null;
              }
              else
              {
                if (raiseException)
                  this.RaiseMemoQtyExceptionHanding(line, split, triggeredBySplit, (Exception) new PXSetPropertyException("Item '{0} {1}' in invoice '{2}' lot/serial number '{3}' is missing from invoice."));
                flag = false;
              }
              return (flag, 0M);
            }
            Decimal? nullable2 = ((PXResult) pxResult1).GetItem<INTranSplit>().BaseQty;
            PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit> pxResult2 = (PXResult<PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>) PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLineSplit>.On<PX.Objects.SO.SOLineSplit.FK.OrderLine>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.orderType, NotEqual<BqlField<PX.Objects.SO.SOLine.orderType, IBqlString>.AsOptional>>>>>.Or<BqlOperand<PX.Objects.SO.SOLine.orderNbr, IBqlString>.IsNotEqual<BqlField<PX.Objects.SO.SOLine.orderNbr, IBqlString>.AsOptional>>>>, And<BqlOperand<PX.Objects.SO.SOLine.origOrderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.origOrderType, IBqlString>.AsOptional>>>, And<BqlOperand<PX.Objects.SO.SOLine.origOrderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.origOrderNbr, IBqlString>.AsOptional>>>, And<BqlOperand<PX.Objects.SO.SOLine.origLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLine.origLineNbr, IBqlInt>.AsOptional>>>, And<BqlOperand<PX.Objects.SO.SOLine.invoiceType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.invoiceType, IBqlString>.AsOptional>>>, And<BqlOperand<PX.Objects.SO.SOLine.invoiceNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLine.invoiceNbr, IBqlString>.AsOptional>>>, And<BqlOperand<PX.Objects.SO.SOLine.operation, IBqlString>.IsEqual<SOOperation.receipt>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.inventoryID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.inventoryID, IBqlInt>.AsOptional>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.subItemID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.subItemID, IBqlInt>.AsOptional>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.AsOptional>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.baseBilledQty, Greater<decimal0>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.baseShippedQty, Greater<decimal0>>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.baseShippedQty, IBqlDecimal>.IsGreater<decimal0>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.completed, NotEqual<True>>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.completed, IBqlBool>.IsNotEqual<True>>>>>.Aggregate<To<GroupBy<PX.Objects.SO.SOLineSplit.inventoryID>, GroupBy<PX.Objects.SO.SOLineSplit.subItemID>, GroupBy<PX.Objects.SO.SOLineSplit.lotSerialNbr>, Sum<PX.Objects.SO.SOLineSplit.baseQty>, Sum<PX.Objects.SO.SOLineSplit.baseShippedQty>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[2]
            {
              (object) line,
              (object) split
            }, Array.Empty<object>()));
            Decimal? nullable3;
            if (pxResult2 != null)
            {
              if (inLotSerClass.LotSerTrack == "S")
              {
                if (raiseException)
                  this.RaiseMemoQtyExceptionHanding(line, split, triggeredBySplit, (Exception) new PXSetPropertyException("Item '{0} {1}' in invoice '{2}' serial number '{3}' is already returned."));
                flag = false;
              }
              else
              {
                nullable3 = ((PXResult) pxResult2).GetItem<PX.Objects.SO.SOLineSplit>().BaseShippedQty;
                Decimal valueOrDefault = nullable3.GetValueOrDefault();
                if (valueOrDefault == 0M)
                {
                  nullable3 = ((PXResult) pxResult2).GetItem<PX.Objects.SO.SOLineSplit>().BaseQty;
                  valueOrDefault = nullable3.GetValueOrDefault();
                }
                nullable3 = nullable2;
                Decimal num2 = valueOrDefault;
                nullable2 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num2) : new Decimal?();
              }
            }
            nullable3 = nullable2;
            Decimal? nullable4 = PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) split, typeof (PX.Objects.SO.SOLine)).Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
            {
              int? subItemId1 = s.SubItemID;
              int? subItemId2 = split.SubItemID;
              return subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && string.Equals(s.LotSerialNbr, split.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
            })).Sum<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, Decimal?>) (s => !s.Completed.GetValueOrDefault() ? s.BaseQty : s.BaseShippedQty));
            Decimal? nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            num1 = nullable5.GetValueOrDefault();
            nullable4 = nullable5;
            Decimal num3 = 0M;
            if (nullable4.GetValueOrDefault() < num3 & nullable4.HasValue)
            {
              if (raiseException)
                this.RaiseMemoQtyExceptionHanding(line, split, triggeredBySplit, (Exception) new PXSetPropertyException("Item '{0} {1}' in invoice '{2}' lot/serial number '{3}' quantity returned is greater than quantity invoiced."));
              flag = false;
            }
          }
        }
      }
    }
    return (flag, flag ? num1 : 0M);
  }

  protected void RaiseMemoQtyExceptionHanding(
    PX.Objects.SO.SOLine line,
    PX.Objects.SO.SOLineSplit split,
    bool triggeredBySplit,
    Exception e)
  {
    if (triggeredBySplit)
      ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.qty>((object) split, (object) split.Qty, (Exception) new PXSetPropertyException(e.Message, new object[4]
      {
        ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.inventoryID>((object) line),
        ((PXCache) this.SplitCache).GetValueExt<PX.Objects.SO.SOLineSplit.subItemID>((object) split),
        ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.invoiceNbr>((object) line),
        ((PXCache) this.SplitCache).GetValueExt<PX.Objects.SO.SOLineSplit.lotSerialNbr>((object) split)
      }));
    else
      ((PXCache) this.LineCache).RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) line, (object) line.OrderQty, (Exception) new PXSetPropertyException(e.Message, new object[4]
      {
        ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.inventoryID>((object) line),
        ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.subItemID>((object) line),
        ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.invoiceNbr>((object) line),
        ((PXCache) this.LineCache).GetValueExt<PX.Objects.SO.SOLine.lotSerialNbr>((object) line)
      }));
  }

  protected override void Optimize()
  {
    base.Optimize();
    foreach (PXResult<SOLineShort, INUnit, INSiteStatusByCostCenter> pxResult in PXSelectBase<SOLineShort, PXViewOf<SOLineShort>.BasedOn<SelectFromBase<SOLineShort, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INUnit>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.inventoryID, Equal<SOLineShort.inventoryID>>>>>.And<BqlOperand<INUnit.fromUnit, IBqlString>.IsEqual<SOLineShort.uOM>>>>, FbqlJoins.Inner<INSiteStatusByCostCenter>.On<SOLineShort.FK.SiteStatusByCostCenter>>>.Where<KeysRelation<CompositeKey<Field<SOLineShort.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<SOLineShort.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, SOLineShort>, PX.Objects.SO.SOOrder, SOLineShort>.SameAsCurrent>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      PrimaryKeyOf<INUnit>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INUnit.unitType, INUnit.inventoryID, INUnit.fromUnit>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INUnit.unitType, INUnit.inventoryID, INUnit.fromUnit>) PXResult<SOLineShort, INUnit, INSiteStatusByCostCenter>.op_Implicit(pxResult), false);
      PrimaryKeyOf<INSiteStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>) PXResult<SOLineShort, INUnit, INSiteStatusByCostCenter>.op_Implicit(pxResult), false);
    }
  }

  protected override void RaiseQtyExceptionHandling(
    PX.Objects.SO.SOLine line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) line, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.SO.SOLine.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.SO.SOLine.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.SO.SOLine.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<PX.Objects.SO.SOLine.locationID>((object) line),
      ((PXCache) this.LineCache).GetValue<PX.Objects.SO.SOLine.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    PX.Objects.SO.SOLineSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.qty>((object) split, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOLineSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOLineSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOLineSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<PX.Objects.SO.SOLineSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<PX.Objects.SO.SOLineSplit.lotSerialNbr>((object) split)
    }));
  }

  protected virtual void RaiseErrorOnOrderQty(
    bool onPersist,
    PX.Objects.SO.SOLine line,
    string errorMessage,
    params object[] args)
  {
    PXSetPropertyKeepPreviousException previousException = new PXSetPropertyKeepPreviousException(errorMessage, (PXErrorLevel) 4, args);
    short? nullable1 = line.LineSign;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = line.InvtMult;
    int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?((Decimal) (nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault())) : new Decimal?();
    Decimal? orderQty = line.OrderQty;
    Decimal? nullable5 = nullable4.HasValue & orderQty.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * orderQty.GetValueOrDefault()) : new Decimal?();
    if (((PXCache) this.LineCache).RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) line, (object) nullable5, (Exception) previousException) & onPersist)
      throw new PXRowPersistingException(typeof (PX.Objects.SO.SOLine.orderQty).Name, (object) nullable5, errorMessage, args);
  }
}
