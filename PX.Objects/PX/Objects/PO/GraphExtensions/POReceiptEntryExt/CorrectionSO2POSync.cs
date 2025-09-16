// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.CorrectionSO2POSync
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.BQLConstants;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.SO;
using PX.Objects.SO.Models;
using PX.Objects.SO.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class CorrectionSO2POSync : PXGraphExtension<Correction, SO2POSync, POReceiptEntry>
{
  public static bool IsActive() => Correction.IsActive();

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine> e)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current?.OrigReceiptNbr == null || e.Row?.OrigReceiptNbr == null || !(e.Row.POType == "DP"))
      return;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.allowComplete>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.PO.POReceiptLine> e)
  {
    if (!e.Row.IsCorrection.GetValueOrDefault())
      return;
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      bool? nullable = e.Row.IsStockItem;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = e.Row.IsKit;
        if (nullable.GetValueOrDefault())
        {
          nullable = e.Row.IsAdjustedIN;
          if (nullable.GetValueOrDefault())
          {
            if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.PO.POReceiptLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.isAdjusted>((object) e.Row, (object) null, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The {0} item is a non-stock kit. Correction of receipt lines with non-stock kits or cancellation of receipts with non-stock kits is not supported.", (PXErrorLevel) 5, new object[1]
            {
              ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>((object) e.Row)
            })))
              throw new PXRowPersistingException("isAdjusted", (object) e.Row.IsAdjustedIN, "The {0} item is a non-stock kit. Correction of receipt lines with non-stock kits or cancellation of receipts with non-stock kits is not supported.", new object[1]
              {
                ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>((object) e.Row)
              });
          }
        }
      }
      if (!(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.POType != "DP"))
        return;
      nullable = e.Row.CanceledWithoutCorrection;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        nullable = e.Row.IsAdjustedIN;
        if (nullable.GetValueOrDefault())
          this.RecreateDemandPlan(e.Row);
      }
      ((PXGraphExtension<POReceiptEntry>) this).Base.ValidateUomConversionNotAdjusted(e.Row);
    }
    else
    {
      if (PXDBOperationExt.Command(e.Operation) != 3 || !EnumerableExtensions.IsIn<string>(e.Row.LineType, "GS", "NO"))
        return;
      this.RemoveZeroDemandPlan(e.Row);
    }
  }

  private void RecreateDemandPlan(PX.Objects.PO.POReceiptLine receiptLine)
  {
    if (EnumerableExtensions.IsNotIn<string>(receiptLine.LineType, "GS", "NO"))
      return;
    PX.Objects.PO.POLine referencedPoLine = ((PXGraphExtension<POReceiptEntry>) this).Base.GetReferencedPOLine(receiptLine.POType, receiptLine.PONbr, receiptLine.POLineNbr);
    if (!referencedPoLine.PlanID.HasValue)
      return;
    foreach (PX.Objects.SO.SOLineSplit parentSoSplit in this.GetParentSOSplits(referencedPoLine, receiptLine))
    {
      long? nullable;
      if (parentSoSplit.Completed.GetValueOrDefault())
      {
        nullable = parentSoSplit.PlanID;
        if (!nullable.HasValue)
        {
          parentSoSplit.PlanID = ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.InsertParentPlan(parentSoSplit, referencedPoLine, 0M);
          ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache.Update(parentSoSplit);
          continue;
        }
      }
      nullable = referencedPoLine.PlanID;
      long num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      {
        nullable = parentSoSplit.PlanID;
        if (nullable.HasValue)
        {
          INItemPlan inItemPlan = INItemPlan.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, parentSoSplit.PlanID, (PKFindOptions) 1);
          nullable = inItemPlan.SupplyPlanID;
          bool flag = !nullable.HasValue;
          if (!flag && INItemPlan.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, inItemPlan.SupplyPlanID, (PKFindOptions) 1) == null)
            flag = true;
          if (flag)
          {
            inItemPlan.SupplyPlanID = referencedPoLine.PlanID;
            ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.PlanCache.Update(inItemPlan);
          }
        }
      }
    }
  }

  private void RemoveZeroDemandPlan(PX.Objects.PO.POReceiptLine receiptLine)
  {
    foreach (PX.Objects.SO.SOLineSplit parentSoSplit in this.GetParentSOSplits(((PXGraphExtension<POReceiptEntry>) this).Base.GetReferencedPOLine(receiptLine.POType, receiptLine.PONbr, receiptLine.POLineNbr), receiptLine, true))
    {
      INItemPlan inItemPlan = INItemPlan.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, parentSoSplit.PlanID);
      Decimal? planQty = inItemPlan.PlanQty;
      Decimal num = 0M;
      if (planQty.GetValueOrDefault() == num & planQty.HasValue)
      {
        ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.PlanCache.Delete(inItemPlan);
        parentSoSplit.PlanID = new long?();
        ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache.Update(parentSoSplit);
      }
    }
  }

  private List<PX.Objects.SO.SOLineSplit> GetParentSOSplits(
    PX.Objects.PO.POLine poLine,
    PX.Objects.PO.POReceiptLine receiptLine,
    bool onlyWithPlans = false)
  {
    return GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Exists<SelectFromBase<POOrderEntry.SOLineSplit3, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrderEntry.SOLineSplit3.orderType, Equal<PX.Objects.SO.SOLineSplit.orderType>>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.orderNbr, IBqlString>.IsEqual<PX.Objects.SO.SOLineSplit.orderNbr>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.lineNbr, IBqlInt>.IsEqual<PX.Objects.SO.SOLineSplit.lineNbr>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.parentSplitLineNbr, IBqlInt>.IsEqual<PX.Objects.SO.SOLineSplit.splitLineNbr>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.pOReceiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.FromCurrent>>>>.And<BqlOperand<POOrderEntry.SOLineSplit3.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptNbr, IBqlString>.FromCurrent>>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.baseQty, IBqlDecimal>.IsNotEqual<decimal0>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.planID, IsNull>>>, And<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<False>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.IsNotNull>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POLine.orderType, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POLine.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POLine.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[2]
    {
      (object) poLine,
      (object) receiptLine
    }, new object[1]{ (object) onlyWithPlans })).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).ToList<PX.Objects.SO.SOLineSplit>();
  }

  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.PreReleaseReceipt(PX.Objects.PO.POReceipt,System.Lazy{PX.Objects.IN.INRegisterEntryBase})" />
  [PXOverride]
  public void PreReleaseReceipt(
    PX.Objects.PO.POReceipt doc,
    Lazy<INRegisterEntryBase> inRegisterEntry,
    Action<PX.Objects.PO.POReceipt, Lazy<INRegisterEntryBase>> base_PreReleaseReceipt)
  {
    base_PreReleaseReceipt(doc, inRegisterEntry);
    if ((!(doc.ReceiptType == "RT") ? 0 : (doc.OrigReceiptNbr != null ? 1 : 0)) == 0 || doc.POType == "DP")
      return;
    foreach (PXResult<PX.Objects.PO.POReceiptLine, POReceiptLineSplit, POReceiptLine2> pxResult in PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<POReceiptLineSplit>.On<POReceiptLineSplit.FK.ReceiptLine>>, FbqlJoins.Inner<POReceiptLine2>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine2.receiptType, Equal<PX.Objects.PO.POReceiptLine.origReceiptType>>>>, And<BqlOperand<POReceiptLine2.receiptNbr, IBqlString>.IsEqual<PX.Objects.PO.POReceiptLine.origReceiptNbr>>>>.And<BqlOperand<POReceiptLine2.lineNbr, IBqlInt>.IsEqual<PX.Objects.PO.POReceiptLine.origReceiptLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.SameAsCurrent>, And<BqlOperand<PX.Objects.PO.POReceiptLine.lineType, IBqlString>.IsIn<POLineType.goodsForSalesOrder, POLineType.nonStockForSalesOrder>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.isAdjustedIN, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine poReceiptLine1;
      POReceiptLineSplit receiptLineSplit1;
      POReceiptLine2 origReceiptLine;
      pxResult.Deconstruct(ref poReceiptLine1, ref receiptLineSplit1, ref origReceiptLine);
      PX.Objects.PO.POReceiptLine poReceiptLine2 = poReceiptLine1;
      POReceiptLineSplit receiptLineSplit2 = receiptLineSplit1;
      foreach (PX.Objects.SO.SOLineSplit soLineSplit in this.GetSOSplitsByReceiptLine((PX.Objects.PO.POReceiptLine) origReceiptLine, receiptLineSplit2.LotSerialNbr))
      {
        soLineSplit.POReceiptNbr = poReceiptLine2.ReceiptNbr;
        soLineSplit.POReceiptType = poReceiptLine2.ReceiptType;
        ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache.Update(soLineSplit);
      }
    }
  }

  private List<PX.Objects.SO.SOLineSplit> GetSOSplitsByReceiptLine(
    PX.Objects.PO.POReceiptLine origReceiptLine,
    string lotSerialNbr)
  {
    return GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POOrderEntry.SOLineSplit3>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<POOrderEntry.SOLineSplit3.orderType>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<POOrderEntry.SOLineSplit3.orderNbr>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<POOrderEntry.SOLineSplit3.lineNbr>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.parentSplitLineNbr, IBqlInt>.IsEqual<POOrderEntry.SOLineSplit3.splitLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.pOType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOType, IBqlString>.FromCurrent>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.FromCurrent>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Optional2<POReceiptLineSplit.lotSerialNbr>, IsNull>>>, Or<BqlOperand<Optional2<POReceiptLineSplit.lotSerialNbr>, IBqlString>.IsEqual<EmptyString>>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsNull>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<EmptyString>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<POReceiptLineSplit.lotSerialNbr, IBqlString>.AsOptional.NoDefault>>>>.Order<By<BqlField<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.Desc>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
    {
      (object) origReceiptLine
    }, (object[]) new string[3]
    {
      lotSerialNbr,
      lotSerialNbr,
      lotSerialNbr
    })).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).ToList<PX.Objects.SO.SOLineSplit>();
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.ResetCorrectionLineWithSameCury(PX.Objects.PO.POReceiptLine)" />
  [PXOverride]
  public void ResetCorrectionLineWithSameCury(
    PX.Objects.PO.POReceiptLine correctionLine,
    Action<PX.Objects.PO.POReceiptLine> base_ResetCorrectionLineWithSameCury)
  {
    base_ResetCorrectionLineWithSameCury(correctionLine);
    if (!EnumerableExtensions.IsIn<string>(correctionLine.LineType, "GS", "NO"))
      return;
    this.RemoveZeroDemandPlan(correctionLine);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.OnBeforeAddingReversalLine(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POReceiptLine,PX.Objects.PO.IItemPlanPOReceiptSource)" />
  [PXOverride]
  public void OnBeforeAddingReversalLine(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POReceiptLine origLine,
    IItemPlanPOReceiptSource split,
    Action<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine, IItemPlanPOReceiptSource> base_OnBeforeAddingReversalLine)
  {
    base_OnBeforeAddingReversalLine(line, origLine, split);
    if (!EnumerableExtensions.IsIn<string>(line.LineType, "GS", "NO"))
      return;
    this.ReduceSOAllocationOnReleaseCorrection(origLine, split);
  }

  protected virtual void ReduceSOAllocationOnReleaseCorrection(
    PX.Objects.PO.POReceiptLine origLine,
    IItemPlanPOReceiptSource posplit)
  {
    PX.Objects.PO.POLine poLine = ((PXGraphExtension<POReceiptEntry>) this).Base.ReopenPOLineIfNeeded(((PXGraphExtension<POReceiptEntry>) this).Base.GetReferencedPOLine(origLine.POType, origLine.PONbr, origLine.POLineNbr));
    if (!POOrderType.IsNormalType(origLine?.POType) || !EnumerableExtensions.IsIn<string>(origLine.LineType, "GS", "NO"))
      return;
    List<PX.Objects.SO.SOLineSplit> splitsByReceiptLine = this.GetSOSplitsByReceiptLine(origLine, posplit.LotSerialNbr);
    Decimal returnQty = origLine.LineType == "NO" ? origLine.BaseQty.GetValueOrDefault() : posplit.BaseQty.GetValueOrDefault();
    foreach (PX.Objects.SO.SOLineSplit soSplit in splitsByReceiptLine)
    {
      returnQty -= ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.UpdateSOAllocation(soSplit, poLine, returnQty, true);
      if (returnQty <= 0M)
        break;
    }
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.OnAfterCancelReceiptLine(PX.Objects.PO.POReceipt,PX.Objects.PO.POReceiptLine,PX.Objects.PO.POLine)" />
  [PXOverride]
  public void OnAfterCancelReceiptLine(
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    Action<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine> base_OnAfterCancelReceiptLine)
  {
    base_OnAfterCancelReceiptLine(receipt, line, poLine);
    if (receipt.POType != "DP")
      return;
    PXResult<PX.Objects.SO.SOLineSplit, SOLine4>[] relatedDemand = ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.GetRelatedDemand(line);
    if (!((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit, SOLine4>>) relatedDemand).Any<PXResult<PX.Objects.SO.SOLineSplit, SOLine4>>())
      return;
    this.RevertDemandOrdersForLine(line, poLine, relatedDemand);
  }

  [PXOverride]
  public void RevertDemandOrdersOrigReceipt(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    PXResult<PX.Objects.SO.SOLineSplit, SOLine4>[] demand,
    Action<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine, PXResult<PX.Objects.SO.SOLineSplit, SOLine4>[]> base_RevertDemandOrdersOrigReceipt)
  {
    base_RevertDemandOrdersOrigReceipt(line, poLine, demand);
    if (demand.Length != 1)
      throw new PXInvalidOperationException();
    this.RevertDemandOrdersForLine(PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, line.OrigReceiptType, line.OrigReceiptNbr, line.OrigReceiptLineNbr) ?? throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceiptLine>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), new object[3]
    {
      (object) line.OrigReceiptType,
      (object) line.OrigReceiptNbr,
      (object) line.OrigReceiptLineNbr
    }), poLine, demand);
  }

  protected virtual void RevertDemandOrdersForLine(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    PXResult<PX.Objects.SO.SOLineSplit, SOLine4>[] demand)
  {
    PX.Objects.SO.SOLineSplit soLineSplit1;
    SOLine4 soLine4_1;
    demand[0].Deconstruct(ref soLineSplit1, ref soLine4_1);
    PX.Objects.SO.SOLineSplit soLineSplit2 = soLineSplit1;
    SOLine4 soLine4_2 = soLine4_1;
    bool? completed = poLine.Completed;
    bool flag1 = false;
    bool flag2 = completed.GetValueOrDefault() == flag1 & completed.HasValue && soLineSplit2.Completed.GetValueOrDefault();
    bool flag3 = line.UOM == soLine4_2.UOM;
    PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit2);
    SOLine4 copy2 = PXCache<SOLine4>.CreateCopy(soLine4_2);
    Decimal? baseReceiptQty = line.BaseReceiptQty;
    Decimal? nullable1 = flag3 ? line.ReceiptQty : new Decimal?(INUnitAttribute.ConvertFromBase<SOLine4.inventoryID, SOLine4.uOM>((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOLineCache, (object) copy2, baseReceiptQty.GetValueOrDefault(), INPrecision.QUANTITY));
    if (flag2)
    {
      copy1.Completed = new bool?(false);
      copy1.POReceiptType = (string) null;
      copy1.POReceiptNbr = (string) null;
      copy1.POCompleted = new bool?(false);
      copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache.Update(copy1));
    }
    PX.Objects.SO.SOLineSplit soLineSplit3 = copy1;
    Decimal? baseShippedQty1 = soLineSplit3.BaseShippedQty;
    Decimal? nullable2 = baseReceiptQty;
    soLineSplit3.BaseShippedQty = baseShippedQty1.HasValue & nullable2.HasValue ? new Decimal?(baseShippedQty1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    if (flag3)
    {
      PX.Objects.SO.SOLineSplit soLineSplit4 = copy1;
      Decimal? shippedQty = soLineSplit4.ShippedQty;
      Decimal? nullable3 = nullable1;
      soLineSplit4.ShippedQty = shippedQty.HasValue & nullable3.HasValue ? new Decimal?(shippedQty.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    }
    else
      PXDBQuantityAttribute.CalcTranQty<PX.Objects.SO.SOLineSplit.shippedQty>((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache, (object) copy1);
    PX.Objects.SO.SOLineSplit soLineSplit5 = ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache.Update(copy1);
    bool flag4 = flag2 && copy2.Completed.GetValueOrDefault();
    if (flag4)
    {
      copy2.Completed = new bool?(false);
      copy2.OpenLine = new bool?(true);
      copy2 = PXCache<SOLine4>.CreateCopy(((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOLineCache.Update(copy2));
    }
    SOLine4 soLine4_3 = copy2;
    Decimal? baseShippedQty2 = soLine4_3.BaseShippedQty;
    short? lineSign = copy2.LineSign;
    Decimal? nullable4 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = baseReceiptQty;
    Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7;
    if (!(baseShippedQty2.HasValue & nullable6.HasValue))
    {
      nullable5 = new Decimal?();
      nullable7 = nullable5;
    }
    else
      nullable7 = new Decimal?(baseShippedQty2.GetValueOrDefault() - nullable6.GetValueOrDefault());
    soLine4_3.BaseShippedQty = nullable7;
    if (flag3)
    {
      SOLine4 soLine4_4 = copy2;
      Decimal? shippedQty = soLine4_4.ShippedQty;
      lineSign = copy2.LineSign;
      nullable5 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable8 = nullable1;
      Decimal? nullable9 = nullable5.HasValue & nullable8.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable8.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable10;
      if (!(shippedQty.HasValue & nullable9.HasValue))
      {
        nullable8 = new Decimal?();
        nullable10 = nullable8;
      }
      else
        nullable10 = new Decimal?(shippedQty.GetValueOrDefault() - nullable9.GetValueOrDefault());
      soLine4_4.ShippedQty = nullable10;
    }
    else
      PXDBQuantityAttribute.CalcBaseQty<SOLine4.baseShippedQty>((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOLineCache, (object) copy2);
    SOLine4 soLine4_5 = ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOLineCache.Update(copy2);
    PX.Objects.SO.SOOrder order = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOLineCache, (object) soLine4_5);
    if (order == null)
      throw new RowNotFoundException((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOOrderCache, new object[2]
      {
        (object) soLine4_5.OrderType,
        (object) soLine4_5.OrderNbr
      });
    if (flag4)
    {
      PX.Objects.SO.SOOrder soOrder = order;
      int? openLineCntr = soOrder.OpenLineCntr;
      soOrder.OpenLineCntr = openLineCntr.HasValue ? new int?(openLineCntr.GetValueOrDefault() + 1) : new int?();
      if (order.Completed.GetValueOrDefault())
        order.MarkOpen();
    }
    this.CancelOrderShipment(order, line);
    INItemPlan soToDropShipPlan = this.CreateSoToDropShipPlan(soLineSplit5);
    if (soToDropShipPlan != null)
    {
      soToDropShipPlan.SupplyPlanID = poLine.PlanID;
      INItemPlan inItemPlan = ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.PlanCache.Insert(soToDropShipPlan);
      soLineSplit5.PlanID = inItemPlan.PlanID;
    }
    demand[0] = new PXResult<PX.Objects.SO.SOLineSplit, SOLine4>(soLineSplit5, soLine4_5);
  }

  protected virtual void CancelOrderShipment(PX.Objects.SO.SOOrder order, PX.Objects.PO.POReceiptLine line)
  {
    PX.Objects.SO.SOOrderShipment soOrderShipment1 = PX.Objects.SO.SOOrderShipment.FromDropshipPOReceipt(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current, order, line);
    PX.Objects.SO.SOOrderShipment soOrderShipment2 = PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelect<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<INDocType.dropShip>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[3]
    {
      (object) soOrderShipment1.ShipmentNbr,
      (object) soOrderShipment1.OrderType,
      (object) soOrderShipment1.OrderNbr
    }));
    bool? nullable = soOrderShipment2 != null ? soOrderShipment2.Canceled : throw new RowNotFoundException((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOOrderShipmentCache, new object[3]
    {
      (object) soOrderShipment1.OrderType,
      (object) soOrderShipment1.OrderNbr,
      (object) soOrderShipment1.ShippingRefNoteID
    });
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    soOrderShipment2.Canceled = new bool?(true);
    ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOOrderShipmentCache.Update(soOrderShipment2);
    PX.Objects.SO.SOOrder soOrder = order;
    int? shipmentCntr = soOrder.ShipmentCntr;
    soOrder.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() - 1) : new int?();
    GraphHelper.MarkUpdated((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOOrderCache, (object) order, true);
  }

  protected virtual INItemPlan CreateSoToDropShipPlan(PX.Objects.SO.SOLineSplit soLineSplit)
  {
    if (!soLineSplit.PlanID.HasValue && !soLineSplit.Completed.GetValueOrDefault() && soLineSplit.POType == "DP")
    {
      INItemPlan soToDropShipPlan = ((PXGraph) PXGraph.CreateInstance<SOOrderEntry>()).FindImplementation<ItemPlan<SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>>().DefaultValues(new INItemPlan(), soLineSplit);
      if (soToDropShipPlan != null)
      {
        soToDropShipPlan.RefEntityType = typeof (PX.Objects.SO.SOOrder).FullName;
        return soToDropShipPlan;
      }
    }
    return (INItemPlan) null;
  }

  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.ActualizeAndValidatePOReceiptForReleasing(PX.Objects.PO.POReceipt)" />
  [PXOverride]
  public PX.Objects.PO.POReceipt ActualizeAndValidatePOReceiptForReleasing(
    PX.Objects.PO.POReceipt receipt,
    Func<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt> base_ActualizeAndValidatePOReceiptForReleasing)
  {
    if (receipt.OrigReceiptNbr != null && receipt.POType != "DP")
      this.ThrowIfItemIsShipped(receipt);
    return base_ActualizeAndValidatePOReceiptForReleasing(receipt);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.EnsureCanCorrect(PX.Objects.PO.POReceipt,System.Boolean)" />
  [PXOverride]
  public void EnsureCanCorrect(
    PX.Objects.PO.POReceipt receipt,
    bool isCancellation,
    Action<PX.Objects.PO.POReceipt, bool> base_EnsureCanCorrect)
  {
    base_EnsureCanCorrect(receipt, isCancellation);
    this.ThrowIfReceiptHasRelatedSOInvoice(receipt, isCancellation);
    if (!(!(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.POType == "DP") & isCancellation))
      return;
    this.ThrowIfItemIsShipped(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current, true);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.EnsureCanCorrectByOriginalReceipt(PX.Objects.PO.POReceipt,PX.Objects.PO.POReceipt,System.Boolean)" />
  [PXOverride]
  public void EnsureCanCorrectByOriginalReceipt(
    PX.Objects.PO.POReceipt correctionReceipt,
    PX.Objects.PO.POReceipt originalReceipt,
    bool useMessageForSave,
    Action<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt, bool> base_EnsureCanCorrectByOriginalReceipt)
  {
    base_EnsureCanCorrectByOriginalReceipt(correctionReceipt, originalReceipt, useMessageForSave);
    this.ThrowIfReceiptHasRelatedSOInvoice(originalReceipt);
  }

  private void ThrowIfItemIsShipped(PX.Objects.PO.POReceipt receipt, bool isCancellation = false)
  {
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.AsOptional>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.AsOptional>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.baseShippedQty, IBqlDecimal>.IsNotEqual<decimal0>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) null, (object[]) new string[2]
    {
      receipt.ReceiptType,
      isCancellation ? receipt.ReceiptNbr : receipt.OrigReceiptNbr
    })).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).ToList<PX.Objects.SO.SOLineSplit>())
    {
      PXResult<PX.Objects.SO.SOShipment> pxResult = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.Shipment>>, FbqlJoins.Inner<PX.Objects.SO.SOOrderShipment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<PX.Objects.SO.SOOrderShipment.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<PX.Objects.SO.SOOrderShipment.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOOrderShipment.orderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.orderType, IBqlString>.FromCurrent>>>>>.Where<KeysRelation<CompositeKey<Field<SOShipLine.origOrderType>.IsRelatedTo<PX.Objects.SO.SOLineSplit.orderType>, Field<SOShipLine.origOrderNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.orderNbr>, Field<SOShipLine.origLineNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.lineNbr>, Field<SOShipLine.origSplitLineNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.SO.SOLineSplit, SOShipLine>, PX.Objects.SO.SOLineSplit, SOShipLine>.SameAsCurrent>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
      {
        (object) soLineSplit
      }, Array.Empty<object>()));
      PX.Objects.SO.SOShipment soShipment = PXResult<PX.Objects.SO.SOShipment>.op_Implicit(pxResult);
      if (((PXResult) pxResult).GetItem<PX.Objects.SO.SOOrderShipment>().InvtRefNbr != null)
      {
        object obj = PXFieldState.UnwrapValue(((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache).GetValueExt<SOShipLineSplit.inventoryID>((object) soLineSplit));
        throw new PXException("The {0} receipt cannot be corrected because the {1} item is already shipped.", new object[2]
        {
          (object) receipt.OrigReceiptNbr,
          obj
        });
      }
      if (isCancellation || soShipment.Confirmed.GetValueOrDefault())
        throw new PXException("The receipt cannot be corrected or canceled because the {0} item is already included in the {1} shipment. To correct or cancel the receipt, correct the item quantity in the shipment first.", new object[2]
        {
          PXFieldState.UnwrapValue(((PXCache) ((PXGraphExtension<SO2POSync, POReceiptEntry>) this).Base1.SOSplitCache).GetValueExt<SOShipLineSplit.inventoryID>((object) soLineSplit)),
          (object) soShipment.ShipmentNbr
        });
    }
  }

  private void ThrowIfReceiptHasRelatedSOInvoice(PX.Objects.PO.POReceipt receipt, bool isCancellation = false)
  {
    PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.PO.POReceipt, PX.Objects.AR.ARTran>.op_Implicit(((IEnumerable<PXResult<PX.Objects.PO.POReceipt>>) PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.sOShipmentNbr, Equal<PX.Objects.PO.POReceipt.receiptNbr>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.sOShipmentType, IBqlString>.IsEqual<INDocType.dropShip>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.canceled, NotEqual<True>>>>, And<BqlOperand<PX.Objects.AR.ARTran.isCancellation, IBqlBool>.IsNotEqual<True>>>>.Or<BqlOperand<PX.Objects.AR.ARTran.released, IBqlBool>.IsNotEqual<True>>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.PO.POReceipt>>().Cast<PXResult<PX.Objects.PO.POReceipt, PX.Objects.AR.ARTran>>().FirstOrDefault<PXResult<PX.Objects.PO.POReceipt, PX.Objects.AR.ARTran>>());
    if (arTran != null)
      throw new PXException(isCancellation ? "The {0} receipt cannot be canceled because the {1} invoice has been created for the related {2} sales order." : "The {0} receipt cannot be corrected because the {1} invoice has been created for the related sales order ({2}).", new object[3]
      {
        (object) receipt.ReceiptNbr,
        (object) arTran.RefNbr,
        (object) arTran.SOOrderNbr
      });
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.ThrowIfNotAppropriateLineTypeExist(PX.Objects.PO.POReceipt,System.Boolean)" />
  [PXOverride]
  public void ThrowIfNotAppropriateLineTypeExist(
    PX.Objects.PO.POReceipt receipt,
    bool isCancellation,
    Action<PX.Objects.PO.POReceipt, bool> base_ThrowIfNotAppropriateLineTypeExist)
  {
    if ((!(receipt.POType == "DP") ? 0 : (GraphHelper.RowCast<PX.Objects.PO.POReceiptLine>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>) ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>()).Any<PX.Objects.PO.POReceiptLine>((Func<PX.Objects.PO.POReceiptLine, bool>) (line => EnumerableExtensions.IsIn<string>(line.LineType, "GI", "NS"))) ? 1 : 0)) != 0)
      throw new PXException(isCancellation ? "The {0} receipt cannot be canceled because it contains lines of the Goods for IN or Non-Stock type, or both." : "The {0} receipt cannot be corrected because it contains lines of the Goods for IN or Non-Stock type, or both.", new object[1]
      {
        (object) receipt.ReceiptNbr
      });
    base_ThrowIfNotAppropriateLineTypeExist(receipt, isCancellation);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.LinkReversalInventoryDocument(PX.Objects.PO.POReceipt)" />
  [PXOverride]
  public void LinkReversalInventoryDocument(
    PX.Objects.PO.POReceipt receipt,
    Action<PX.Objects.PO.POReceipt> base_LinkReversalInventoryDocument)
  {
    if (receipt.POType == "DP")
    {
      INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<INTran.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<INTran.tranType, IBqlString>.IsIn<INTranType.creditMemo, INTranType.return_>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
      {
        receipt
      }, Array.Empty<object>()));
      if (inTran == null)
        return;
      receipt.ReversalInvtDocType = inTran.DocType;
      receipt.ReversalInvtRefNbr = inTran.RefNbr;
    }
    else
      base_LinkReversalInventoryDocument(receipt);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.ReleaseCancellingInventoryDocument(PX.Objects.PO.POReceipt)" />
  [PXOverride]
  public void ReleaseCancellingInventoryDocument(
    PX.Objects.PO.POReceipt receipt,
    Action<PX.Objects.PO.POReceipt> base_ReleaseCancellingInventoryDocument)
  {
    if (receipt.POType == "DP")
    {
      ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.Save).Press();
      PX.Objects.IN.INRegister inRegister = this.PostReversalInventoryDocumentForDropship(receipt, true);
      if (inRegister == null)
        return;
      ((PXGraphExtension<POReceiptEntry>) this).Base.ReleaseINDocuments(new List<PX.Objects.IN.INRegister>()
      {
        inRegister
      }, "IN Document failed to release with the following error: '{0}'.");
    }
    else
      base_ReleaseCancellingInventoryDocument(receipt);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction.ReleaseCorrectingInventoryDocument(PX.Objects.PO.POReceipt,PX.Objects.IN.INRegister,PX.Objects.CS.DocumentList{PX.Objects.IN.INRegister},System.Collections.Generic.List{PX.Objects.IN.INRegister})" />
  [PXOverride]
  public void ReleaseCorrectingInventoryDocument(
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.IN.INRegister inRegister,
    DocumentList<PX.Objects.IN.INRegister> aINCreated,
    List<PX.Objects.IN.INRegister> forReleaseIN,
    Action<PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister, DocumentList<PX.Objects.IN.INRegister>, List<PX.Objects.IN.INRegister>> base_ReleaseCorrectingInventoryDocument)
  {
    if (receipt.POType == "DP")
    {
      PX.Objects.IN.INRegister inRegister1 = this.PostReversalInventoryDocumentForDropship(receipt, false);
      if (inRegister1 == null)
        return;
      aINCreated.Add(inRegister1);
      ((PXGraphExtension<POReceiptEntry>) this).Base.ReleaseINDocuments(new List<PX.Objects.IN.INRegister>()
      {
        inRegister1
      }, "IN Document failed to release with the following error: '{0}'.");
    }
    else
      base_ReleaseCorrectingInventoryDocument(receipt, inRegister, aINCreated, forReleaseIN);
  }

  private PX.Objects.IN.INRegister PostReversalInventoryDocumentForDropship(
    PX.Objects.PO.POReceipt receipt,
    bool isCancelation)
  {
    PXResultset<PX.Objects.SO.SOOrderShipment> pxResultset = PXSelectBase<PX.Objects.SO.SOOrderShipment, PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>, FbqlJoins.Left<PX.Objects.IN.INRegister>.On<PX.Objects.SO.SOOrderShipment.FK.INRegister>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.AsOptional>>>>>.And<BqlOperand<PX.Objects.SO.SOOrderShipment.shipmentType, IBqlString>.IsEqual<INDocType.dropShip>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) null, new object[1]
    {
      isCancelation ? (object) receipt.ReceiptNbr : (object) receipt.OrigReceiptNbr
    });
    InvoicePostingContext invoicePostingContext = this.GetInvoicePostingContext();
    DocumentList<PX.Objects.IN.INRegister> source = new DocumentList<PX.Objects.IN.INRegister>((PXGraph) invoicePostingContext.IssueEntry);
    foreach (PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.IN.INRegister> pxResult in pxResultset)
    {
      PX.Objects.SO.SOOrderShipment soOrderShipment1;
      PX.Objects.SO.SOOrder soOrder;
      PX.Objects.IN.INRegister inRegister1;
      pxResult.Deconstruct(ref soOrderShipment1, ref soOrder, ref inRegister1);
      PX.Objects.SO.SOOrderShipment soOrderShipment2 = soOrderShipment1;
      PX.Objects.IN.INRegister inRegister2 = inRegister1;
      if (soOrderShipment2.InvoiceNbr != null)
        throw new PXException("The {0} receipt cannot be corrected because the {1} invoice has been created for the related sales order ({2}).", new object[3]
        {
          (object) receipt.OrigReceiptNbr,
          (object) soOrderShipment2.InvoiceNbr,
          (object) soOrderShipment2.OrderNbr
        });
      if (inRegister2 != null)
      {
        bool? released = inRegister2.Released;
        bool flag = false;
        if (released.GetValueOrDefault() == flag & released.HasValue)
          throw new PXException(isCancelation ? "The {0} receipt cannot be canceled because the original issue ({1}) is not released. To be able to cancel the receipt, release the issue first." : "The {0} receipt cannot be released because the original issue ({1}) is not released. Release the issue before releasing the receipt.", new object[2]
          {
            (object) receipt.ReceiptNbr,
            (object) inRegister2.RefNbr
          });
      }
      POReceiptEntry clearPoReceiptEntry = invoicePostingContext.GetClearPOReceiptEntry();
      INIssueEntry issueEntry = invoicePostingContext.IssueEntry;
      PX.Objects.SO.SOOrderShipment SOOrderShipment = PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.IN.INRegister>.op_Implicit(pxResult);
      PX.Objects.SO.SOOrder SOOrder = PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.IN.INRegister>.op_Implicit(pxResult);
      PX.Objects.AR.ARInvoice Invoice = new PX.Objects.AR.ARInvoice();
      Invoice.DocDate = inRegister2.TranDate;
      Invoice.FinPeriodID = inRegister2.FinPeriodID;
      DocumentList<PX.Objects.IN.INRegister> CreatedDocuments = source;
      PX.Objects.PO.POReceipt Receipt = receipt;
      PostReceiptArgs args = new PostReceiptArgs(issueEntry, SOOrderShipment, SOOrder, Invoice, CreatedDocuments, Receipt, true);
      clearPoReceiptEntry.PostReceipt(args);
    }
    return source.FirstOrDefault<PX.Objects.IN.INRegister>();
  }

  protected virtual InvoicePostingContext GetInvoicePostingContext()
  {
    return new InvoicePostingContext(this.FinPeriodUtils);
  }
}
