// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.INReleaseProcessExt.Correction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Exceptions;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.Accumulators;
using PX.Objects.IN.InventoryRelease.Accumulators.Documents;
using PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;
using PX.Objects.IN.InventoryRelease.DAC;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.INReleaseProcessExt;

public class Correction : PXGraphExtension<INReleaseProcess>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() || PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>();
  }

  [PXOverride]
  public void RegenerateInTranList(
    PXResultset<INTran, INTranSplit, PX.Objects.IN.InventoryItem> originalintranlist,
    Action<PXResultset<INTran, INTranSplit, PX.Objects.IN.InventoryItem>> base_RegenerateInTranList)
  {
    if (((PXSelectBase<PX.Objects.IN.INRegister>) this.Base.inregister).Current.IsCorrection.GetValueOrDefault())
    {
      foreach (PXResult<INTran> pxResult in PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INTran.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<INTran.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INTran.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INTran>, PX.Objects.IN.INRegister, INTran>.SameAsCurrent>, And<BqlOperand<INTran.tranType, IBqlString>.IsEqual<INTranType.receipt>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.valMethod, IBqlString>.IsNotEqual<INValMethod.standard>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
        this.GenerateAdjustmentForIssuedStock(PXResult<INTran>.op_Implicit(pxResult));
    }
    base_RegenerateInTranList(originalintranlist);
  }

  protected virtual void GenerateAdjustmentForIssuedStock(INTran receiptTran)
  {
    INTran origReceiptTran = this.GetOrigReceiptTran(receiptTran);
    if (origReceiptTran == null)
      return;
    Decimal? nullable1 = origReceiptTran.BaseQty;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return;
    nullable1 = receiptTran.BaseQty;
    Decimal num2 = 0M;
    if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
      return;
    nullable1 = receiptTran.TranCost;
    Decimal? baseQty = receiptTran.BaseQty;
    Decimal? nullable2 = nullable1.HasValue & baseQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / baseQty.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = origReceiptTran.TranCost;
    nullable1 = origReceiptTran.BaseQty;
    Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
    nullable1 = nullable2;
    nullable3 = nullable4;
    if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
      return;
    PX.Objects.IN.InventoryItem item = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, receiptTran.InventoryID);
    PXView receiptStatusView = this.Base.GetReceiptStatusView(item);
    foreach (IEnumerable<INTranSplit> source in GraphHelper.RowCast<INTranSplit>((IEnumerable) PXSelectBase<INTranSplit, PXViewOf<INTranSplit>.BasedOn<SelectFromBase<INTranSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INTranSplit.docType>.IsRelatedTo<INTran.docType>, Field<INTranSplit.refNbr>.IsRelatedTo<INTran.refNbr>, Field<INTranSplit.lineNbr>.IsRelatedTo<INTran.lineNbr>>.WithTablesOf<INTran, INTranSplit>, INTran, INTranSplit>.SameAsCurrent>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new INTran[1]
    {
      origReceiptTran
    }, Array.Empty<object>())).AsEnumerable<INTranSplit>().GroupBy(s => new
    {
      InventoryID = s.InventoryID,
      CostSubItemID = s.CostSubItemID,
      CostSiteID = s.CostSiteID,
      LotSerialNbr = item.ValMethod == "S" ? s.LotSerialNbr.ToLowerInvariant() : (string) null
    }))
    {
      INTranSplit inTranSplit = source.First<INTranSplit>();
      using (List<object>.Enumerator enumerator = receiptStatusView.SelectMultiBound(new object[2]
      {
        (object) receiptTran,
        (object) inTranSplit
      }, new object[2]
      {
        (object) origReceiptTran.InvtAcctID,
        (object) origReceiptTran.InvtSubID
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<ReadOnlyCostStatus, ReceiptStatus> current = (PXResult<ReadOnlyCostStatus, ReceiptStatus>) enumerator.Current;
          ReadOnlyCostStatus layer = PXResult<ReadOnlyCostStatus, ReceiptStatus>.op_Implicit(current);
          ReceiptStatus receiptStatus = PXResult<ReadOnlyCostStatus, ReceiptStatus>.op_Implicit(current);
          Decimal? nullable5 = new Decimal?(0M);
          Decimal? nullable6 = new Decimal?(0M);
          switch (item.ValMethod)
          {
            case "A":
              nullable3 = layer.QtyOnHand;
              Decimal num3 = 0M;
              if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
              {
                nullable3 = receiptStatus.QtyOnHand;
                Decimal num4 = 0M;
                if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
                {
                  nullable5 = layer.OrigQty;
                  nullable6 = layer.QtyOnHand;
                  break;
                }
              }
              nullable5 = receiptStatus.OrigQty;
              nullable6 = receiptStatus.QtyOnHand;
              break;
            case "S":
              nullable5 = receiptStatus.OrigQty;
              nullable6 = receiptStatus.QtyOnHand;
              break;
            case "F":
              nullable5 = layer.OrigQty;
              nullable6 = layer.QtyOnHand;
              break;
          }
          nullable3 = nullable5;
          Decimal num5 = 0M;
          if (!(nullable3.GetValueOrDefault() == num5 & nullable3.HasValue))
          {
            nullable3 = nullable6;
            nullable1 = nullable5;
            if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
            {
              INTran inTran1 = this.Base.Copy(origReceiptTran, layer, item);
              inTran1.DocType = receiptTran.DocType;
              inTran1.RefNbr = receiptTran.RefNbr;
              inTran1.InvtMult = new short?((short) 1);
              inTran1.POReceiptType = receiptTran.POReceiptType;
              inTran1.POReceiptNbr = receiptTran.POReceiptNbr;
              inTran1.POReceiptLineNbr = receiptTran.POReceiptLineNbr;
              inTran1.POLineType = receiptTran.POLineType;
              inTran1.AcctID = new int?();
              inTran1.SubID = new int?();
              INTran inTran2 = inTran1;
              INReleaseProcess graph = this.Base;
              Decimal? nullable7 = nullable4;
              Decimal? nullable8 = nullable2;
              nullable1 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable9 = nullable5;
              Decimal? nullable10 = nullable6;
              nullable3 = nullable9.HasValue & nullable10.HasValue ? new Decimal?(nullable9.GetValueOrDefault() - nullable10.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable11;
              if (!(nullable1.HasValue & nullable3.HasValue))
              {
                nullable10 = new Decimal?();
                nullable11 = nullable10;
              }
              else
                nullable11 = new Decimal?(nullable1.GetValueOrDefault() * nullable3.GetValueOrDefault());
              nullable10 = nullable11;
              Decimal num6 = nullable10.Value;
              Decimal? nullable12 = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) graph, num6));
              inTran2.TranCost = nullable12;
              inTran1.ReasonCode = this.Base.GetCorrectionReasonCode(receiptTran);
              ((PXSelectBase<INTranSplit>) this.Base.intransplit).Insert(INTranSplit.FromINTran(((PXSelectBase<INTran>) this.Base.intranselect).Insert(inTran1)));
            }
          }
        }
      }
    }
  }

  private INTran GetOrigReceiptTran(INTran tran)
  {
    for (PX.Objects.PO.POReceiptLine poReceiptLine = PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) this.Base, tran.POReceiptType, tran.POReceiptNbr, tran.POReceiptLineNbr); poReceiptLine != null; poReceiptLine = PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) this.Base, poReceiptLine.OrigReceiptType, poReceiptLine.OrigReceiptNbr, poReceiptLine.OrigReceiptLineNbr))
    {
      INTran origReceiptTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.docType, In3<INDocType.receipt, INDocType.issue>>>>, And<BqlOperand<INTran.tranType, IBqlString>.IsEqual<INTranType.receipt>>>, And<BqlOperand<INTran.pOReceiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptType, IBqlString>.FromCurrent>>>, And<BqlOperand<INTran.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<INTran.pOReceiptLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptLineNbr, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
      {
        poReceiptLine
      }, Array.Empty<object>()));
      if (origReceiptTran != null)
        return origReceiptTran;
    }
    return (INTran) null;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.ProcessLinkedAllocation(PX.Objects.IN.INRegister)" />
  /// </summary>
  [PXOverride]
  public void ProcessLinkedAllocation(
    PX.Objects.IN.INRegister doc,
    Action<PX.Objects.IN.INRegister> baseProcessLinkedAllocation)
  {
    baseProcessLinkedAllocation(doc);
    if (!doc.IsCorrection.GetValueOrDefault())
      return;
    PX.Objects.PO.POReceipt poReceipt = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.AsOptional>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.AsOptional>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.origReceiptNbr, IBqlString>.IsNotNull>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object[]) new string[2]
    {
      doc.POReceiptNbr,
      doc.POReceiptType
    }));
    if (poReceipt == null)
      return;
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IBqlString>.IsIn<BqlField<PX.Objects.PO.POReceipt.origReceiptNbr, IBqlString>.FromCurrent, BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.baseShippedQty, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) poReceipt
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).ToList<PX.Objects.SO.SOLineSplit>())
    {
      Decimal? baseShippedQty = soLineSplit.BaseShippedQty;
      Decimal? baseQty = soLineSplit.BaseQty;
      if (baseShippedQty.GetValueOrDefault() > baseQty.GetValueOrDefault() & baseShippedQty.HasValue & baseQty.HasValue)
      {
        PX.Objects.SO.SOShipment soShipment = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.Shipment>>>.Where<KeysRelation<CompositeKey<Field<SOShipLine.origOrderType>.IsRelatedTo<PX.Objects.SO.SOLineSplit.orderType>, Field<SOShipLine.origOrderNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.orderNbr>, Field<SOShipLine.origLineNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.lineNbr>, Field<SOShipLine.origSplitLineNbr>.IsRelatedTo<PX.Objects.SO.SOLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.SO.SOLineSplit, SOShipLine>, PX.Objects.SO.SOLineSplit, SOShipLine>.SameAsCurrent>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
        {
          (object) soLineSplit
        }, Array.Empty<object>()));
        throw new PXException("The receipt cannot be corrected or canceled because the {0} item is already included in the {1} shipment. To correct or cancel the receipt, correct the item quantity in the shipment first.", new object[2]
        {
          PXFieldState.UnwrapValue(((PXSelectBase) this.Base.solinesplit).Cache.GetValueExt<SOShipLineSplit.inventoryID>((object) soLineSplit)),
          (object) soShipment.ShipmentNbr
        });
      }
    }
  }

  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.ThrowNegativeQtyException(PX.Objects.IN.INTran,PX.Objects.IN.INTranSplit,PX.Objects.IN.INCostStatus)" />
  [PXOverride]
  public void ThrowNegativeQtyException(
    INTran tran,
    INTranSplit split,
    INCostStatus lastLayer,
    Action<INTran, INTranSplit, INCostStatus> base_ThrowNegativeQtyException)
  {
    if (((PXSelectBase<PX.Objects.IN.INRegister>) this.Base.inregister).Current.IsCorrection.GetValueOrDefault() && tran.POReceiptType == "RT" && tran.ExactCost.GetValueOrDefault())
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, tran.InventoryID);
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, tran.SiteID);
      if (split.ValMethod == "S" && !string.IsNullOrEmpty(split.LotSerialNbr))
        throw new CorrectionOversoldException("The purchase receipt cannot be corrected or canceled because the {0} item with the {1} lot or serial number has already been issued from the {2} warehouse.", new object[3]
        {
          (object) inventoryItem?.InventoryCD.TrimEnd(),
          (object) split.LotSerialNbr,
          (object) inSite?.SiteCD.TrimEnd()
        });
      if (split.ValMethod == "F" && !string.IsNullOrEmpty(lastLayer?.ReceiptNbr))
        throw new CorrectionOversoldException("The receipt cannot be corrected or canceled because the on-hand quantity of the {0} item received in the {1} receipt is negative or will become negative. Correction or cancellation of purchase receipts with negative on-hand quantities is not supported.", new object[2]
        {
          (object) inventoryItem?.InventoryCD.TrimEnd(),
          (object) lastLayer.ReceiptNbr
        });
      throw new CorrectionOversoldException(inventoryItem == null || !inventoryItem.NegQty.GetValueOrDefault() ? "The quantity of the {0} item in the {1} warehouse will become negative." : "The receipt cannot be corrected or canceled because the on-hand quantity of the {0} item is negative or will become negative in the {1} warehouse. Correction or cancellation of purchase receipts with negative on-hand quantities is not supported.", new object[2]
      {
        (object) inventoryItem?.InventoryCD.TrimEnd(),
        (object) inSite?.SiteCD.TrimEnd()
      });
    }
    base_ThrowNegativeQtyException(tran, split, lastLayer);
  }

  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.UpdatePOReceiptLineReleased(PX.Objects.IN.INTran)" />
  [PXOverride]
  public virtual POReceiptLineUpdate UpdatePOReceiptLineReleased(
    INTran tran,
    Func<INTran, POReceiptLineUpdate> base_UpdatePOReceiptLineReleased)
  {
    POReceiptLineUpdate receiptLineUpdate = base_UpdatePOReceiptLineReleased(tran);
    if (receiptLineUpdate != null && ((PXSelectBase<PX.Objects.IN.INRegister>) this.Base.inregister).Current.IsCorrection.GetValueOrDefault() && receiptLineUpdate.ReasonCode == null)
      receiptLineUpdate.ReasonCode = tran.ReasonCode;
    return receiptLineUpdate;
  }

  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.ProcessINTranCosts(System.Collections.Generic.IEnumerable{PX.Objects.IN.INTranCost},PX.Objects.GL.JournalEntry,PX.Objects.IN.INRegister,System.Lazy{PX.Objects.IN.INPIController},PX.Objects.IN.INTran)" />
  [PXOverride]
  public virtual INTran ProcessINTranCosts(
    IEnumerable<INTranCost> correctionINTranCosts,
    JournalEntry je,
    PX.Objects.IN.INRegister doc,
    Lazy<INPIController> piController,
    INTran prevTran,
    Func<IEnumerable<INTranCost>, JournalEntry, PX.Objects.IN.INRegister, Lazy<INPIController>, INTran, INTran> base_ProcessINTranCosts)
  {
    IEnumerable<INTranCost> inTranCosts = correctionINTranCosts.Where<INTranCost>((Func<INTranCost, bool>) (tranCost =>
    {
      bool? isCorrection = doc.IsCorrection;
      bool flag = false;
      return isCorrection.GetValueOrDefault() == flag & isCorrection.HasValue || tranCost.TranType == "RCP";
    }));
    prevTran = base_ProcessINTranCosts(inTranCosts, je, doc, piController, prevTran);
    if (doc.IsCorrection.GetValueOrDefault())
    {
      int? nullable1 = new int?();
      int? nullable2 = new int?();
      Decimal? nullable3 = new Decimal?(0M);
      Decimal? nullable4 = new Decimal?(0M);
      foreach (ItemCostHist itemCostHist in (IEnumerable<ItemCostHist>) GraphHelper.RowCast<ItemCostHist>(((PXSelectBase) this.Base.itemcosthist).Cache.Inserted).OrderBy<ItemCostHist, int?>((Func<ItemCostHist, int?>) (hist => hist.InventoryID)).ThenBy<ItemCostHist, int?>((Func<ItemCostHist, int?>) (hist => hist.SiteID)))
      {
        int? nullable5 = nullable1;
        int? nullable6 = itemCostHist.InventoryID;
        if (nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue)
        {
          nullable6 = nullable2;
          nullable5 = itemCostHist.CostSiteID;
          if (nullable6.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable6.HasValue == nullable5.HasValue)
            goto label_6;
        }
        nullable3 = new Decimal?(0M);
        nullable4 = new Decimal?(0M);
label_6:
        if (PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, itemCostHist.CostSiteID) != null)
        {
          PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats1 = new PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats();
          itemStats1.InventoryID = nullable1 = itemCostHist.InventoryID;
          itemStats1.SiteID = nullable2 = itemCostHist.CostSiteID;
          PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats>) this.Base.itemstats).Insert(itemStats1);
          Decimal? nullable7 = nullable3;
          Decimal? nullable8 = itemCostHist.FinPtdQtyReceived;
          Decimal num1 = 0M;
          Decimal? nullable9 = nullable8.GetValueOrDefault() > num1 & nullable8.HasValue ? itemCostHist.FinPtdQtyReceived : new Decimal?(0M);
          Decimal? nullable10 = itemCostHist.FinPtdQtyTransferIn;
          Decimal? nullable11;
          if (!(nullable9.HasValue & nullable10.HasValue))
          {
            nullable8 = new Decimal?();
            nullable11 = nullable8;
          }
          else
            nullable11 = new Decimal?(nullable9.GetValueOrDefault() + nullable10.GetValueOrDefault());
          Decimal? nullable12 = nullable11;
          Decimal? nullable13 = itemCostHist.FinPtdQtyAssemblyIn;
          Decimal? nullable14;
          if (!(nullable12.HasValue & nullable13.HasValue))
          {
            nullable10 = new Decimal?();
            nullable14 = nullable10;
          }
          else
            nullable14 = new Decimal?(nullable12.GetValueOrDefault() + nullable13.GetValueOrDefault());
          Decimal? nullable15 = nullable14;
          Decimal? nullable16;
          if (!(nullable7.HasValue & nullable15.HasValue))
          {
            nullable13 = new Decimal?();
            nullable16 = nullable13;
          }
          else
            nullable16 = new Decimal?(nullable7.GetValueOrDefault() + nullable15.GetValueOrDefault());
          nullable3 = nullable16;
          nullable15 = nullable4;
          nullable8 = itemCostHist.FinPtdCostReceived;
          Decimal num2 = 0M;
          nullable10 = nullable8.GetValueOrDefault() > num2 & nullable8.HasValue ? itemCostHist.FinPtdCostReceived : new Decimal?(0M);
          nullable9 = itemCostHist.FinPtdCostTransferIn;
          Decimal? nullable17;
          if (!(nullable10.HasValue & nullable9.HasValue))
          {
            nullable8 = new Decimal?();
            nullable17 = nullable8;
          }
          else
            nullable17 = new Decimal?(nullable10.GetValueOrDefault() + nullable9.GetValueOrDefault());
          nullable13 = nullable17;
          nullable12 = itemCostHist.FinPtdCostAssemblyIn;
          Decimal? nullable18;
          if (!(nullable13.HasValue & nullable12.HasValue))
          {
            nullable9 = new Decimal?();
            nullable18 = nullable9;
          }
          else
            nullable18 = new Decimal?(nullable13.GetValueOrDefault() + nullable12.GetValueOrDefault());
          nullable7 = nullable18;
          Decimal? nullable19;
          if (!(nullable15.HasValue & nullable7.HasValue))
          {
            nullable12 = new Decimal?();
            nullable19 = nullable12;
          }
          else
            nullable19 = new Decimal?(nullable15.GetValueOrDefault() + nullable7.GetValueOrDefault());
          nullable4 = nullable19;
          nullable7 = nullable3;
          Decimal num3 = 0M;
          if (!(nullable7.GetValueOrDefault() == num3 & nullable7.HasValue))
          {
            nullable7 = nullable4;
            Decimal num4 = 0M;
            if (!(nullable7.GetValueOrDefault() == num4 & nullable7.HasValue))
            {
              PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats3 = itemStats2;
              nullable7 = nullable4;
              nullable15 = nullable3;
              Decimal? nullable20;
              if (!(nullable7.HasValue & nullable15.HasValue))
              {
                nullable12 = new Decimal?();
                nullable20 = nullable12;
              }
              else
                nullable20 = new Decimal?(nullable7.GetValueOrDefault() / nullable15.GetValueOrDefault());
              nullable12 = nullable20;
              Decimal? nullable21 = new Decimal?(PXDBPriceCostAttribute.Round(nullable12.Value));
              itemStats3.LastCost = nullable21;
              itemStats2.LastCostDate = new DateTime?(INReleaseProcess.GetLastCostTime(((PXSelectBase) this.Base.itemstats).Cache));
              goto label_32;
            }
          }
          itemStats2.LastCost = new Decimal?(0M);
          itemStats2.LastCostDate = INItemStats.MinDate.get();
label_32:
          itemStats2.MaxCost = itemStats2.LastCost;
          itemStats2.MinCost = itemStats2.LastCost;
        }
      }
      correctionINTranCosts = GraphHelper.RowCast<INTranCost>(((PXSelectBase) this.Base.intrancost).Cache.Inserted).Where<INTranCost>((Func<INTranCost, bool>) (tranCost => tranCost.TranType != "RCP"));
      prevTran = base_ProcessINTranCosts(correctionINTranCosts, je, doc, piController, prevTran);
    }
    return prevTran;
  }
}
