// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Correction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.BQLConstants;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Exceptions;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class Correction : 
  PXGraphExtension<POReceiptLineSplittingExtension, UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.PO.Reverse.POReceiptLineSplit, Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.Reverse.POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptType>, Field<PX.Objects.PO.Reverse.POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptNbr>, Field<PX.Objects.PO.Reverse.POReceiptLineSplit.lineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLine, PX.Objects.PO.Reverse.POReceiptLineSplit>, PX.Objects.PO.POReceiptLine, PX.Objects.PO.Reverse.POReceiptLineSplit>.SameAsCurrent>> ReverseSplits;
  public PXAction<PX.Objects.PO.POReceipt> correctReceipt;
  public PXAction<PX.Objects.PO.POReceipt> cancelReceipt;
  public PXAction<PX.Objects.PO.POReceipt> resetCorrectionLine;
  private bool _creatingCorrectionReceipt;
  private bool _updatingCurrencyInfo;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() || PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>();
  }

  public static bool HideCorrectionUI
  {
    get
    {
      return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() || PXAccess.FeatureInstalled<FeaturesSet.manufacturing>();
    }
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  protected virtual IEnumerable CorrectReceipt(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current == null)
      return adapter.Get();
    ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.Save).Press();
    this.EnsureCanCorrect(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current);
    this.CreateCorrectionReceiptAndRedirect(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  protected virtual IEnumerable CancelReceipt(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current == null)
      return adapter.Get();
    ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.Save).Press();
    this.EnsureCanCorrect(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current, true);
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Ask("Cancel Receipt", this.GetCancelReceiptMessage(), (MessageButtons) 4) == 6)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation<POReceiptEntry>((PXGraphExtension<POReceiptEntry>) this, new PXToggleAsyncDelegate((object) new Correction.\u003C\u003Ec__DisplayClass7_0()
      {
        receipt = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current
      }, __methodptr(\u003CCancelReceipt\u003Eb__0)));
    }
    return adapter.Get();
  }

  protected virtual string GetCancelReceiptMessage()
  {
    return "The purchase receipt will be canceled. Do you want to proceed with canceling the receipt?";
  }

  [PXUIField]
  [PXButton(ImageKey = "Cancel", ImageSet = "main", CommitChanges = true, DisplayOnMainToolbar = false)]
  public virtual IEnumerable ResetCorrectionLine(PXAdapter adapter)
  {
    PX.Objects.PO.POReceiptLine current1 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Current;
    if ((current1 != null ? (!current1.IsAdjusted.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return adapter.Get();
    PX.Objects.PO.POReceiptLine current2 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Current;
    PX.Objects.PO.POReceiptLine origPoReceiptLine = this.GetOrigPOReceiptLine(current2);
    PX.Objects.PO.POReceiptLine correctionLine = this.FillReceiptLineWithOrigLineValues(current2, origPoReceiptLine, ((PXGraphExtension<POReceiptEntry>) this).Base);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraphExtension<POReceiptEntry>) this).Base.MultiCurrencyExt.GetCurrencyInfo(origPoReceiptLine.CuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXGraphExtension<POReceiptEntry>) this).Base.MultiCurrencyExt.GetCurrencyInfo(correctionLine.CuryInfoID);
    DateTime? receiptDate1 = correctionLine.ReceiptDate;
    DateTime? receiptDate2 = origPoReceiptLine.ReceiptDate;
    Decimal? nullable1;
    Decimal? nullable2;
    if ((receiptDate1.HasValue == receiptDate2.HasValue ? (receiptDate1.HasValue ? (receiptDate1.GetValueOrDefault() == receiptDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
    {
      nullable1 = currencyInfo1.SampleCuryRate;
      Decimal? sampleCuryRate = currencyInfo2.SampleCuryRate;
      if (nullable1.GetValueOrDefault() == sampleCuryRate.GetValueOrDefault() & nullable1.HasValue == sampleCuryRate.HasValue)
      {
        nullable2 = currencyInfo1.SampleRecipRate;
        nullable1 = currencyInfo2.SampleRecipRate;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          this.ResetCorrectionLineWithSameCury(correctionLine);
          goto label_10;
        }
      }
    }
    nullable1 = currencyInfo1.CuryRate;
    nullable2 = currencyInfo2.CuryRate;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      correctionLine.CuryTranUnitCost = origPoReceiptLine.CuryTranUnitCost;
      nullable2 = origPoReceiptLine.CuryTranUnitCost;
      if (nullable2.HasValue)
      {
        PX.Objects.PO.POReceiptLine poReceiptLine = correctionLine;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo2;
        nullable2 = correctionLine.CuryTranUnitCost;
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        int? precision = new int?(6);
        Decimal? nullable3 = new Decimal?(currencyInfo3.CuryConvBase(valueOrDefault, precision));
        poReceiptLine.TranUnitCost = nullable3;
      }
      else
      {
        PX.Objects.PO.POReceiptLine poReceiptLine = correctionLine;
        nullable2 = new Decimal?();
        Decimal? nullable4 = nullable2;
        poReceiptLine.TranUnitCost = nullable4;
      }
    }
label_10:
    PX.Objects.PO.POReceiptLine row = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Update(correctionLine);
    ((PXGraphExtension<POReceiptEntry>) this).Base.UpdatePOLineCompleteFlag(row, false, (PX.Objects.PO.POLine) null);
    ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Update(row);
    return adapter.Get();
  }

  protected virtual void ResetCorrectionLineWithSameCury(PX.Objects.PO.POReceiptLine correctionLine)
  {
    correctionLine.IsAdjusted = new bool?(false);
    correctionLine.IsAdjustedIN = new bool?(false);
    foreach (PX.Objects.PO.Reverse.POReceiptLineSplit receiptLineSplit in ((PXSelectBase) this.ReverseSplits).View.SelectMultiBound((object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      correctionLine
    }, Array.Empty<object>()))
      ((PXSelectBase<PX.Objects.PO.Reverse.POReceiptLineSplit>) this.ReverseSplits).Delete(receiptLineSplit);
  }

  protected IDisposable CreatingCorrectionReceiptScope()
  {
    return (IDisposable) new SimpleScope((System.Action) (() => this._creatingCorrectionReceipt = true), (System.Action) (() => this._creatingCorrectionReceipt = false));
  }

  protected virtual void CreateCorrectionReceiptAndRedirect(PX.Objects.PO.POReceipt origReceipt)
  {
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    using (((PXGraph) instance).FindImplementation<Correction>().CreatingCorrectionReceiptScope())
    {
      PX.Objects.PO.POReceipt poReceipt1 = ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Insert(new PX.Objects.PO.POReceipt()
      {
        ReceiptType = origReceipt.ReceiptType,
        OrigReceiptNbr = origReceipt.ReceiptNbr
      });
      poReceipt1.VendorID = origReceipt.VendorID;
      poReceipt1.VendorLocationID = origReceipt.VendorLocationID;
      poReceipt1.BranchID = origReceipt.BranchID;
      poReceipt1.FinPeriodID = origReceipt.FinPeriodID;
      poReceipt1.TranPeriodID = origReceipt.TranPeriodID;
      poReceipt1.SiteID = origReceipt.SiteID;
      poReceipt1.WorkgroupID = origReceipt.WorkgroupID;
      poReceipt1.ProjectID = origReceipt.ProjectID;
      poReceipt1.POType = origReceipt.POType;
      poReceipt1.OrigPONbr = origReceipt.OrigPONbr;
      poReceipt1.ShipToBAccountID = origReceipt.ShipToBAccountID;
      poReceipt1.ShipToLocationID = origReceipt.ShipToLocationID;
      poReceipt1.SOOrderNbr = origReceipt.SOOrderNbr;
      poReceipt1.SOOrderType = origReceipt.SOOrderType;
      poReceipt1.OwnerID = origReceipt.OwnerID;
      poReceipt1.WMSSingleOrder = origReceipt.WMSSingleOrder;
      poReceipt1.ReceiptDate = origReceipt.ReceiptDate;
      poReceipt1.ReturnInventoryCostMode = origReceipt.ReturnInventoryCostMode;
      poReceipt1.AutoCreateInvoice = origReceipt.AutoCreateInvoice;
      poReceipt1.InvoiceNbr = origReceipt.InvoiceNbr;
      PX.Objects.PO.POReceipt poReceipt2 = ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Update(poReceipt1);
      if (!(poReceipt2.CuryID != origReceipt.CuryID))
      {
        int? branchId1 = poReceipt2.BranchID;
        int? branchId2 = origReceipt.BranchID;
        if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue)
          goto label_4;
      }
      PX.Objects.PO.POReceipt copy = PXCache<PX.Objects.PO.POReceipt>.CreateCopy(poReceipt2);
      copy.CuryID = origReceipt.CuryID;
      copy.BranchID = origReceipt.BranchID;
      poReceipt2 = ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Update(copy);
label_4:
      instance.CopyReceiptCurrencyInfoToReturn(((PXGraphExtension<POReceiptEntry>) this).Base.GetCurrencyInfo(origReceipt.CuryInfoID), instance.GetCurrencyInfo(poReceipt2.CuryInfoID));
      foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult in ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Select(Array.Empty<object>()))
      {
        PX.Objects.PO.POReceiptLine origLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
        this.FillReceiptLineWithOrigLineValues(((PXSelectBase<PX.Objects.PO.POReceiptLine>) instance.transactions).Insert(), origLine, instance);
      }
      foreach (PXResult<PX.Objects.PO.POOrderReceipt> pxResult in ((PXSelectBase<PX.Objects.PO.POOrderReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.ReceiptOrders).Select(Array.Empty<object>()))
      {
        PX.Objects.PO.POOrderReceipt poOrderReceipt = PXResult<PX.Objects.PO.POOrderReceipt>.op_Implicit(pxResult);
        instance.AddPOOrderReceipt(poOrderReceipt.POType, poOrderReceipt.PONbr);
      }
    }
    throw new PXRedirectRequiredException((PXGraph) instance, false, string.Empty);
  }

  private PX.Objects.PO.POReceiptLine FillReceiptLineWithOrigLineValues(
    PX.Objects.PO.POReceiptLine correctionLine,
    PX.Objects.PO.POReceiptLine origLine,
    POReceiptEntry graph)
  {
    POReceiptEntry poReceiptEntry = graph;
    PX.Objects.PO.POReceiptLine destLine = correctionLine;
    PX.Objects.PO.POReceiptLine srcLine = origLine;
    bool? allowEditUnitCost = origLine.AllowEditUnitCost;
    bool? returnOrigCost = allowEditUnitCost.HasValue ? new bool?(!allowEditUnitCost.GetValueOrDefault()) : new bool?();
    poReceiptEntry.CopyFromOrigReceiptLine(destLine, (IPOReturnLineSource) srcLine, true, returnOrigCost);
    correctionLine.OrigPlanType = origLine.OrigPlanType;
    correctionLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(correctionLine);
    correctionLine.POAccrualRefNoteID = origLine.POAccrualRefNoteID;
    correctionLine.POAccrualType = origLine.POAccrualType;
    correctionLine.POAccrualLineNbr = origLine.POAccrualLineNbr;
    correctionLine.ReceiptQty = origLine.ReceiptQty;
    correctionLine.BaseReceiptQty = origLine.BaseReceiptQty;
    correctionLine.BaseOrigQty = origLine.BaseReceiptQty;
    correctionLine.BaseReturnedQty = origLine.BaseReturnedQty;
    correctionLine.IntercompanyShipmentLineNbr = origLine.IntercompanyShipmentLineNbr;
    correctionLine.LotSerialNbrRequiredForDropship = origLine.LotSerialNbrRequiredForDropship;
    graph.PopulateReturnedQty((IPOReturnLineSource) correctionLine);
    correctionLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(correctionLine);
    Decimal? receiptQty = correctionLine.ReceiptQty;
    List<PX.Objects.PO.POReceiptLineSplit> list = GraphHelper.RowCast<PX.Objects.PO.POReceiptLineSplit>((IEnumerable) ((PXSelectBase) graph.splits).View.SelectMultiBound((object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      correctionLine
    }, Array.Empty<object>()).AsEnumerable<object>()).ToList<PX.Objects.PO.POReceiptLineSplit>();
    int index = 0;
    PXView view = ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.splits).View;
    object[] objArray1 = (object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      origLine
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (PX.Objects.PO.POReceiptLineSplit receiptLineSplit1 in view.SelectMultiBound(objArray1, objArray2))
    {
      PX.Objects.PO.POReceiptLineSplit receiptLineSplit2 = list.Count <= index ? ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) graph.splits).Insert() : list[index++];
      receiptLineSplit2.ReceiptType = correctionLine.ReceiptType;
      receiptLineSplit2.LineNbr = correctionLine.LineNbr;
      receiptLineSplit2.PONbr = receiptLineSplit1.PONbr;
      receiptLineSplit2.LineType = receiptLineSplit1.LineType;
      receiptLineSplit2.InvtMult = receiptLineSplit1.InvtMult;
      receiptLineSplit2.InventoryID = receiptLineSplit1.InventoryID;
      receiptLineSplit2.SiteID = receiptLineSplit1.SiteID;
      receiptLineSplit2.LocationID = receiptLineSplit1.LocationID;
      receiptLineSplit2.SubItemID = receiptLineSplit1.SubItemID;
      receiptLineSplit2.LotSerialNbr = receiptLineSplit1.LotSerialNbr;
      receiptLineSplit2.ExpireDate = receiptLineSplit1.ExpireDate;
      receiptLineSplit2.UOM = receiptLineSplit1.UOM;
      receiptLineSplit2.BaseQty = receiptLineSplit1.BaseQty;
      receiptLineSplit2.Qty = receiptLineSplit1.Qty;
      receiptLineSplit2.ProjectID = receiptLineSplit1.ProjectID;
      receiptLineSplit2.TaskID = receiptLineSplit1.TaskID;
      ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) graph.splits).Update(receiptLineSplit2);
    }
    for (; index < list.Count; ++index)
      ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) graph.splits).Delete(list[index]);
    correctionLine.ReceiptQty = receiptQty;
    correctionLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(correctionLine);
    Decimal? curyTranUnitCost = correctionLine.CuryTranUnitCost;
    Decimal num = 0M;
    if (curyTranUnitCost.GetValueOrDefault() == num & curyTranUnitCost.HasValue)
    {
      bool? nullable = correctionLine.IsKit;
      if (nullable.GetValueOrDefault())
      {
        nullable = correctionLine.IsStockItem;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          correctionLine.CuryTranUnitCost = new Decimal?();
          correctionLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) graph.transactions).Update(correctionLine);
        }
      }
    }
    return correctionLine;
  }

  protected virtual PX.Objects.PO.POReceipt ActualizeAndValidatePOReceiptForCancelReceipt(
    PX.Objects.PO.POReceipt aDoc)
  {
    ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Search<PX.Objects.PO.POReceipt.receiptNbr>((object) aDoc.ReceiptNbr, new object[1]
    {
      (object) aDoc.ReceiptType
    }));
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<PX.Objects.PO.POReceipt>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, "cancelReceipt", ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.cancelReceipt).GetCaption(),
        (object) ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Cache.GetRowDescription((object) ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current)
      });
    return ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current;
  }

  protected virtual void CancelReceiptInternal(PX.Objects.PO.POReceipt receipt)
  {
    receipt = this.ActualizeAndValidatePOReceiptForCancelReceipt(receipt);
    foreach (PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine> pxResult in PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.PO.POLine>.On<PX.Objects.PO.POReceiptLine.FK.OrderLine>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine poReceiptLine1 = PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine>.op_Implicit(pxResult);
      PX.Objects.PO.POLine poLine1 = PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine>.op_Implicit(pxResult);
      poReceiptLine1.CanceledWithoutCorrection = new bool?(true);
      PX.Objects.PO.POReceiptLine poReceiptLine2 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Update(poReceiptLine1);
      Decimal? nullable1;
      if (poLine1.OrderNbr != null)
      {
        poLine1 = (PX.Objects.PO.POLine) ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.poline).Cache.Locate((object) poLine1) ?? poLine1;
        Decimal? nullable2 = poReceiptLine2.ReceiptQty;
        Decimal num1 = 0M;
        if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
        {
          nullable2 = poReceiptLine2.ReceiptQty;
          Decimal num2 = nullable2.GetValueOrDefault();
          if (poReceiptLine2.InventoryID.HasValue && !string.IsNullOrEmpty(poReceiptLine2.UOM) && !string.IsNullOrEmpty(poLine1.UOM) && !string.Equals(poReceiptLine2.UOM, poLine1.UOM, StringComparison.OrdinalIgnoreCase))
          {
            PXCache cach = ((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Caches[typeof (PX.Objects.PO.POReceiptLine)];
            int? inventoryId = poReceiptLine2.InventoryID;
            string uom = poLine1.UOM;
            nullable2 = poReceiptLine2.BaseReceiptQty;
            Decimal num3 = nullable2.Value;
            num2 = INUnitAttribute.ConvertFromBase(cach, inventoryId, uom, num3, INPrecision.QUANTITY);
          }
          PX.Objects.PO.POLine poLine2 = poLine1;
          nullable2 = poLine2.CompletedQty;
          Decimal num4 = num2;
          short? invtMult = poReceiptLine2.InvtMult;
          Decimal? nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
          nullable1 = nullable3.HasValue ? new Decimal?(num4 * nullable3.GetValueOrDefault()) : new Decimal?();
          poLine2.CompletedQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          nullable1 = poLine1.OrderQty;
          Decimal num5 = nullable1.Value;
          nullable1 = poLine1.RcptQtyThreshold;
          Decimal num6 = nullable1.Value;
          Decimal num7 = num5 * num6 / 100.0M;
          nullable1 = poLine1.ReceivedQty;
          if (nullable1.Value < num7)
          {
            bool? nullable4 = poLine1.AllowComplete;
            if (nullable4.GetValueOrDefault())
            {
              PX.Objects.PO.POLine poLine3 = poLine1;
              poLine1.Completed = nullable4 = poLine1.Closed = new bool?(false);
              bool? nullable5 = nullable4;
              poLine3.AllowComplete = nullable5;
            }
          }
          poLine1 = ((PXSelectBase<PX.Objects.PO.POLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.poline).Update(poLine1);
        }
      }
      POAccrualStatus accrualStatus = POAccrualStatus.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poReceiptLine2.POAccrualRefNoteID, poReceiptLine2.POAccrualLineNbr, poReceiptLine2.POAccrualType, (PKFindOptions) 1);
      if (accrualStatus != null)
      {
        nullable1 = accrualStatus.ReceivedQty;
        bool nulloutReceivedQty = !nullable1.HasValue || !EnumerableExtensions.IsIn<string>(accrualStatus.ReceivedUOM, (string) null, poReceiptLine2.UOM);
        this.SubtractReceiptLineFromPOAccrualStatus(poReceiptLine2, accrualStatus, nulloutReceivedQty, receipt.POType == "DP", true);
        ((PXSelectBase<POAccrualStatus>) ((PXGraphExtension<UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>) this).Base2.poAccrualUpdate).Update(accrualStatus);
      }
      this.ReverseAccrualDetail(poReceiptLine2, true);
      this.OnAfterCancelReceiptLine(receipt, poReceiptLine2, poLine1);
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((SelectedEntityEvent<PX.Objects.PO.POReceipt>) PXEntityEventBase<PX.Objects.PO.POReceipt>.Container<PX.Objects.PO.POReceipt.Events>.Select((Expression<Func<PX.Objects.PO.POReceipt.Events, PXEntityEvent<PX.Objects.PO.POReceipt.Events>>>) (ev => ev.ReceiptCanceled))).FireOn((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, receipt);
      this.ReleaseCancellingInventoryDocument(receipt);
      transactionScope.Complete();
    }
  }

  protected virtual void OnAfterCancelReceiptLine(
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine)
  {
  }

  protected virtual void ReleaseCancellingInventoryDocument(PX.Objects.PO.POReceipt receipt)
  {
    PX.Objects.IN.INRegister inDocument = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.refNbr, Equal<BqlField<PX.Objects.PO.POReceipt.invtRefNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.IN.INRegister.docType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.invtDocType, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.IN.INRegister.released, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()));
    if (inDocument != null)
    {
      this.RemoveUnreleasedINReceipt(receipt, inDocument);
      receipt.InvtDocType = (string) null;
      receipt.InvtRefNbr = (string) null;
      ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Update(receipt);
      ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.Save).Press();
    }
    else
    {
      Lazy<INRegisterEntryBase> inRegisterEntry = Lazy.By<INRegisterEntryBase>((Func<INRegisterEntryBase>) (() => ((PXGraphExtension<POReceiptEntry>) this).Base.ReleaseContextExt.GetCleanINRegisterEntryWithInsertedHeader(receipt, true)));
      this.AddReversalLinesToInventory(inRegisterEntry, true);
      ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.Save).Press();
      if (!inRegisterEntry.IsValueCreated)
        return;
      PX.Objects.IN.INRegister current = inRegisterEntry.Value.INRegisterDataMember.Current;
      ((PXAction) inRegisterEntry.Value.Save).Press();
      ((PXGraphExtension<POReceiptEntry>) this).Base.ReleaseINDocuments(new List<PX.Objects.IN.INRegister>()
      {
        current
      }, "IN Document failed to release with the following error: '{0}'.");
    }
  }

  protected virtual bool RemoveUnreleasedINReceipt(PX.Objects.PO.POReceipt receipt, PX.Objects.IN.INRegister inDocument)
  {
    if (inDocument.DocType != "R")
      throw new PXInvalidOperationException();
    foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult1 in PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.SameAsCurrent.And<BqlOperand<PX.Objects.PO.POReceiptLine.baseReceiptQty, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult1);
      POAccrualStatus poAccrualStatus1 = POAccrualStatus.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poReceiptLine.POAccrualRefNoteID, poReceiptLine.POAccrualLineNbr, poReceiptLine.POAccrualType, (PKFindOptions) 1);
      POAccrualStatus poAccrualStatus2 = poAccrualStatus1;
      int? unreleasedReceiptCntr = poAccrualStatus2.UnreleasedReceiptCntr;
      poAccrualStatus2.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() - 1) : new int?();
      ((PXSelectBase<POAccrualStatus>) ((PXGraphExtension<UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>) this).Base2.poAccrualUpdate).Update(poAccrualStatus1);
      if (!EnumerableExtensions.IsNotIn<string>(poReceiptLine.LineType, "GS", "NO"))
      {
        PX.Objects.PO.POLine referencedPoLine = ((PXGraphExtension<POReceiptEntry>) this).Base.GetReferencedPOLine(poReceiptLine.POType, poReceiptLine.PONbr, poReceiptLine.POLineNbr);
        foreach (PX.Objects.SO.SOLineSplit parent in GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.baseQty, NotEqual<decimal0>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POLine.orderType, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POLine.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POLine.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[2]
        {
          (object) referencedPoLine,
          (object) poReceiptLine
        }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).ToList<PX.Objects.SO.SOLineSplit>())
        {
          long? planId = referencedPoLine.PlanID;
          long num = 0;
          if (planId.GetValueOrDefault() < num & planId.HasValue && poReceiptLine.LineType == "GS")
          {
            INItemPlan inItemPlan = INItemPlan.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, parent.PlanID, (PKFindOptions) 1);
            inItemPlan.SupplyPlanID = referencedPoLine.PlanID;
            GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Update(inItemPlan);
          }
          else if (poReceiptLine.LineType == "NO")
          {
            POReceiptEntry poReceiptEntry = ((PXGraphExtension<POReceiptEntry>) this).Base;
            object[] objArray1 = new object[2]
            {
              (object) poReceiptLine,
              (object) parent
            };
            object[] objArray2 = (object[]) new string[3]
            {
              referencedPoLine.LotSerialNbr,
              referencedPoLine.LotSerialNbr,
              referencedPoLine.LotSerialNbr
            };
            foreach (PX.Objects.SO.SOLineSplit soSplit in GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.FromCurrent>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Optional2<PX.Objects.PO.POReceiptLineSplit.lotSerialNbr>, IsNull>>>, Or<BqlOperand<Optional2<PX.Objects.PO.POReceiptLineSplit.lotSerialNbr>, IBqlString>.IsEqual<EmptyString>>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsNull>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<EmptyString>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.lotSerialNbr, IBqlString>.AsOptional.NoDefault>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.orderType, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.parentSplitLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.splitLineNbr, IBqlInt>.FromCurrent>>>.Order<PX.Data.BQL.Fluent.By<BqlField<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.Desc>>>.Config>.SelectMultiBound((PXGraph) poReceiptEntry, objArray1, objArray2)).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).ToList<PX.Objects.SO.SOLineSplit>())
            {
              INItemPlan plan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
              {
                (object) soSplit
              }, Array.Empty<object>()));
              if (plan != null && !EnumerableExtensions.IsNotIn<string>(plan.PlanType, "61", "60"))
              {
                Decimal valueOrDefault = poReceiptLine.BaseReceiptQty.GetValueOrDefault();
                SO2POSync implementation = ((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).FindImplementation<SO2POSync>();
                Decimal returnQty = implementation.SplitAllocation(soSplit, plan, valueOrDefault);
                if (parent.PONbr == referencedPoLine.OrderNbr && parent.POType == referencedPoLine.OrderType)
                {
                  int? poLineNbr = parent.POLineNbr;
                  int? lineNbr = referencedPoLine.LineNbr;
                  if (poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue)
                    parent.POCompleted = referencedPoLine.Completed;
                }
                implementation.UpdateParent(parent, referencedPoLine, returnQty);
              }
            }
          }
          foreach (PXResult<INItemPlan> pxResult2 in PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTranSplit>.On<BqlOperand<INTranSplit.planID, IBqlLong>.IsEqual<INItemPlan.supplyPlanID>>>, FbqlJoins.Inner<INTran>.On<INTranSplit.FK.Tran>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.refNbr, Equal<BqlField<PX.Objects.PO.POReceipt.invtRefNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<INTran.docType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.invtDocType, IBqlString>.FromCurrent>>>, And<BqlOperand<INItemPlan.demandPlanID, IBqlLong>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.FromCurrent>>>>.And<KeysRelation<CompositeKey<Field<INTran.pOReceiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptType>, Field<INTran.pOReceiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptNbr>, Field<INTran.pOReceiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLine, INTran>, PX.Objects.PO.POReceiptLine, INTran>.SameAsCurrent>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[3]
          {
            (object) receipt,
            (object) parent,
            (object) poReceiptLine
          }, Array.Empty<object>()))
          {
            INItemPlan inItemPlan = PXResult<INItemPlan>.op_Implicit(pxResult2);
            GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Delete(inItemPlan);
          }
        }
      }
    }
    INReceiptEntry instance = PXGraph.CreateInstance<INReceiptEntry>();
    instance.INRegisterDataMember.Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(instance.INRegisterDataMember.Search<PX.Objects.IN.INRegister.refNbr>((object) inDocument.RefNbr, Array.Empty<object>()));
    if (instance.INRegisterDataMember.Current.Released.GetValueOrDefault())
      throw new PXLockViolationException(typeof (PX.Objects.PO.POReceipt), (PXDBOperation) 1, ((IEnumerable<string>) ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Cache.Keys).Select<string, object>((Func<string, object>) (f => ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Cache.GetValue((object) receipt, f))).ToArray<object>());
    ((PXSelectBase) instance.INRegisterDataMember).Cache.AllowDelete = true;
    ((PXAction) instance.Delete).Press();
    return true;
  }

  protected virtual void EnsureCanCorrect(PX.Objects.PO.POReceipt receipt, bool isCancellation = false)
  {
    if (!this.ReceiptTypeAndStatusAllowCorrection(receipt))
      throw new PXInvalidOperationException();
    if (!isCancellation)
      this.ThrowIfINReceiptNotReleased(receipt);
    this.ThrowIfNotAppropriateLineTypeExist(receipt, isCancellation);
    this.ThrowIfReceiptHasAPDoc(receipt, isCancellation);
    this.ThrowIfReceiptIsReferencedInLandedCost(receipt, isCancellation);
    this.ThrowIfReceiptIsReturnedOrHasUnreleasedReturn(receipt, isCancellation);
    this.ThrowIfReceiptIsBeingPutAway(receipt, isCancellation);
  }

  protected virtual void EnsureCanCorrectByOriginalReceipt(
    PX.Objects.PO.POReceipt correctionReceipt,
    PX.Objects.PO.POReceipt originalReceipt,
    bool useMessageForSave = false)
  {
    this.ThrowIfReceiptHasAPDoc(originalReceipt, useMessageForSave: useMessageForSave);
    this.ThrowIfReceiptIsReferencedInLandedCost(originalReceipt, useMessageForSave: useMessageForSave);
    this.ThrowIfReceiptIsReturnedOrHasUnreleasedReturn(originalReceipt, useMessageForSave: useMessageForSave);
    this.ThrowIfReceiptIsBeingPutAway(originalReceipt, useMessageForSave: useMessageForSave);
  }

  private bool ReceiptTypeAndStatusAllowCorrection(PX.Objects.PO.POReceipt receipt)
  {
    if (receipt.ReceiptType == "RT" && receipt.Released.GetValueOrDefault())
    {
      bool? canceled = receipt.Canceled;
      bool flag1 = false;
      if (canceled.GetValueOrDefault() == flag1 & canceled.HasValue)
      {
        bool? isUnderCorrection = receipt.IsUnderCorrection;
        bool flag2 = false;
        return isUnderCorrection.GetValueOrDefault() == flag2 & isUnderCorrection.HasValue;
      }
    }
    return false;
  }

  private void ThrowIfINReceiptNotReleased(PX.Objects.PO.POReceipt receipt)
  {
    if (string.IsNullOrEmpty(receipt.InvtRefNbr) || string.IsNullOrEmpty(receipt.InvtDocType))
      return;
    PX.Objects.IN.INRegister inRegister = PX.Objects.IN.INRegister.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, receipt.InvtDocType, receipt.InvtRefNbr);
    if (inRegister == null)
      return;
    bool? released = inRegister.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
      throw new PXException("Before correcting the purchase receipt, release the {0} inventory receipt.", new object[1]
      {
        (object) inRegister.RefNbr
      });
  }

  private void ThrowIfReceiptHasAPDoc(
    PX.Objects.PO.POReceipt receipt,
    bool isCancellation = false,
    bool useMessageForSave = false)
  {
    PX.Objects.PO.POReceiptLine line1 = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.unbilledQty, IBqlDecimal>.IsNotEqual<PX.Objects.PO.POReceiptLine.receiptQty>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()));
    if (line1 != null)
      ThrowException(line1);
    PX.Objects.PO.POReceiptLine line2 = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.APTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.pOAccrualType, Equal<PX.Objects.PO.POReceiptLine.pOAccrualType>>>>, And<BqlOperand<PX.Objects.AP.APTran.pOAccrualRefNoteID, IBqlGuid>.IsEqual<PX.Objects.PO.POReceiptLine.pOAccrualRefNoteID>>>>.And<BqlOperand<PX.Objects.AP.APTran.pOAccrualLineNbr, IBqlInt>.IsEqual<PX.Objects.PO.POReceiptLine.pOAccrualLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.AP.APTran.released, IBqlBool>.IsEqual<False>>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()));
    if (line2 == null)
      return;
    ThrowException(line2);

    void ThrowException(PX.Objects.PO.POReceiptLine line)
    {
      bool flag = line.POAccrualType == "R";
      if (useMessageForSave)
        throw new PXException(flag ? "The receipt cannot be saved or released because at least one bill has been created for the original receipt ({0})." : "The receipt cannot be saved or released because at least one bill has been created for the {0} purchase order.", new object[1]
        {
          flag ? (object) line.ReceiptNbr : (object) line.PONbr
        });
      if (isCancellation)
        throw new PXException(flag ? "The {0} receipt cannot be canceled because at least one bill has been created for this receipt." : "The {0} receipt cannot be canceled because at least one bill has been created for the linked purchase order ({1}).", new object[2]
        {
          (object) line.ReceiptNbr,
          (object) line.PONbr
        });
      throw new PXException(flag ? "The {0} receipt cannot be corrected because at least one bill has been created for this receipt." : "The {0} receipt cannot be corrected because at least one bill has been created for the linked purchase order {1}.", new object[2]
      {
        (object) line.ReceiptNbr,
        (object) line.PONbr
      });
    }
  }

  private void ThrowIfReceiptIsReferencedInLandedCost(
    PX.Objects.PO.POReceipt receipt,
    bool isCancellation = false,
    bool useMessageForSave = false)
  {
    POLandedCostReceiptLine landedCostReceiptLine = PXResultset<POLandedCostReceiptLine>.op_Implicit(PXSelectBase<POLandedCostReceiptLine, PXViewOf<POLandedCostReceiptLine>.BasedOn<SelectFromBase<POLandedCostReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceipt>.On<POLandedCostReceiptLine.FK.Receipt>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()));
    if (landedCostReceiptLine != null)
      throw new PXException(isCancellation ? "The {0} receipt cannot be canceled because it is linked to the {1} landed cost document." : (useMessageForSave ? "The receipt cannot be saved or released because the original receipt ({0}) is linked to the {1} landed cost document." : "The {0} receipt cannot be corrected because it is linked to the {1} landed cost document."), new object[2]
      {
        (object) landedCostReceiptLine.POReceiptNbr,
        (object) landedCostReceiptLine.RefNbr
      });
  }

  private void ThrowIfReceiptIsReturnedOrHasUnreleasedReturn(
    PX.Objects.PO.POReceipt receipt,
    bool isCancelation = false,
    bool useMessageForSave = false)
  {
    PX.Objects.PO.POReceiptLine poReceiptLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POReceiptLine2>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.origReceiptNbr, Equal<POReceiptLine2.receiptNbr>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origReceiptType, IBqlString>.IsEqual<POReceiptLine2.receiptType>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.origReceiptLineNbr, IBqlInt>.IsEqual<POReceiptLine2.lineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine2.receiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<POReceiptLine2.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.IsEqual<POReceiptType.poreturn>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.released, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()));
    if (poReceiptLine != null)
    {
      if (useMessageForSave)
        throw new PXException("The receipt cannot be saved or released because there is at least one unreleased purchase return ({0}) prepared for the original purchase receipt ({1}). To correct the original receipt, delete or release the unreleased purchase return.", new object[2]
        {
          (object) poReceiptLine.ReceiptNbr,
          (object) receipt.ReceiptNbr
        });
      throw new PXException(isCancelation ? "The {0} receipt cannot be canceled because it has at least one unreleased purchase return. To be able to cancel the receipt, remove or release the following unreleased purchase returns: ({1})." : "The {0} receipt cannot be corrected because it has at least one unreleased purchase return. To be able to correct the receipt, remove or release the following unreleased purchase returns: ({1}).", new object[2]
      {
        (object) receipt.ReceiptNbr,
        (object) poReceiptLine.ReceiptNbr
      });
    }
    if (isCancelation && GraphHelper.RowCast<PX.Objects.PO.POReceiptLine>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>) ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>()).Any<PX.Objects.PO.POReceiptLine>((Func<PX.Objects.PO.POReceiptLine, bool>) (line =>
    {
      Decimal? baseReturnedQty = line.BaseReturnedQty;
      Decimal num = 0M;
      return !(baseReturnedQty.GetValueOrDefault() == num & baseReturnedQty.HasValue);
    })))
      throw new PXException("The {0} receipt cannot be canceled because the items from this receipt have been fully or partially returned.", new object[1]
      {
        (object) receipt.ReceiptNbr
      });
  }

  protected virtual void ThrowIfNotAppropriateLineTypeExist(PX.Objects.PO.POReceipt receipt, bool isCancellation)
  {
    foreach (PX.Objects.PO.POReceiptLine poReceiptLine in GraphHelper.RowCast<PX.Objects.PO.POReceiptLine>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>) ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>()))
    {
      if (EnumerableExtensions.IsIn<string>(poReceiptLine.LineType, "GR", "PG", "PN"))
        throw new PXException(isCancellation ? "The {0} receipt cannot be canceled because it has been created for a replenishment request, project, or subcontract. Cancellation of such purchase receipts is not supported." : "The {0} receipt cannot be corrected because it has been created for a replenishment request, project, or subcontract. Correction of such purchase receipts is not supported.", new object[1]
        {
          (object) receipt.ReceiptNbr
        });
      if (EnumerableExtensions.IsIn<string>(poReceiptLine.LineType, "GF", "NF", "GM", "NM"))
        throw new PXException("The {0} receipt cannot be corrected or canceled because it has been created for a service order or production order. Correction of such purchase receipts is not supported.", new object[1]
        {
          (object) receipt.ReceiptNbr
        });
      if (isCancellation)
      {
        bool? nullable = poReceiptLine.IsKit;
        if (nullable.GetValueOrDefault())
        {
          nullable = poReceiptLine.IsStockItem;
          bool flag = false;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
            throw new PXException("The {0} item is a non-stock kit. Correction of receipt lines with non-stock kits or cancellation of receipts with non-stock kits is not supported.", new object[1]
            {
              ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>((object) poReceiptLine)
            });
        }
      }
    }
  }

  private void ThrowIfReceiptIsBeingPutAway(
    PX.Objects.PO.POReceipt receipt,
    bool isCancelation = false,
    bool useMessageForSave = false)
  {
    if (receipt.POType == "DP")
      return;
    List<PX.Objects.IN.INRegister> list = GraphHelper.RowCast<PX.Objects.IN.INRegister>((IEnumerable) ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.RelatedTransfers).View.SelectMultiBound((object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()).AsEnumerable<object>()).ToList<PX.Objects.IN.INRegister>();
    if (list.Any<PX.Objects.IN.INRegister>((Func<PX.Objects.IN.INRegister, bool>) (transfer => !transfer.Released.GetValueOrDefault())))
      throw new PXException(isCancelation ? "The {0} receipt cannot be canceled because it is being put away on the Receive and Put Away (PO302020) form." : (useMessageForSave ? "The receipt cannot be saved or released because the original receipt ({0}) is being put away on the Receive and Put Away (PO302020) form." : "The {0} receipt cannot be corrected because it is being put away on the Receive and Put Away (PO302020) form."), new object[1]
      {
        (object) receipt.ReceiptNbr
      });
    PX.Objects.IN.INRegister inRegister = list.FirstOrDefault<PX.Objects.IN.INRegister>();
    if (inRegister != null)
      throw new PXException("The {0} receipt cannot be corrected or canceled because it has a related inventory transfer ({1}). Correction of such purchase receipts is not supported. You can review the transfer on the Put Away tab.", new object[2]
      {
        (object) receipt.ReceiptNbr,
        (object) inRegister.RefNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row.Released.GetValueOrDefault())
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.PO.POReceipt.invoiceNbr>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Cache, (object) e.Row, (PXPersistingCheck) 2);
    if (EnumerableExtensions.IsIn<PXDBOperation>((PXDBOperation) (e.Operation & 3), (PXDBOperation) 2, (PXDBOperation) 1) && e.Row.OrigReceiptNbr != null)
    {
      Decimal? orderQty = e.Row.OrderQty;
      Decimal num = 0M;
      if (orderQty.GetValueOrDefault() == num & orderQty.HasValue)
        throw new PXRowPersistingException("OrderQty", (object) e.Row.OrderQty, "The receipt total quantity is 0. If a total quantity of 0 has been received, use the Cancel Receipt command for the original receipt ({0}).", new object[1]
        {
          (object) e.Row.OrigReceiptNbr
        });
      if (e.Row.POType != "DP")
        this.ValidateFinPeriodID(e.Row, true);
    }
    if ((e.Operation & 3) != 2 || e.Row.OrigReceiptNbr == null)
      return;
    PX.Objects.PO.POReceipt originalReceipt = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, e.Row.ReceiptType, e.Row.OrigReceiptNbr);
    this.EnsureCanCorrectByOriginalReceipt(e.Row, originalReceipt, true);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.PO.POReceiptLine> e)
  {
    if ((e.Operation & 3) != 2 || !(e.Row.ReceiptType == "RN"))
      return;
    PX.Objects.PO.POReceipt poReceipt = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, e.Row.OrigReceiptType, e.Row.OrigReceiptNbr);
    if (poReceipt != null && poReceipt.IsUnderCorrection.GetValueOrDefault() || poReceipt != null && poReceipt.Canceled.GetValueOrDefault())
      throw new PXLockViolationException(typeof (PX.Objects.PO.POReceipt), (PXDBOperation) 2, (object[]) new string[2]
      {
        poReceipt.ReceiptType,
        poReceipt.ReceiptNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row == null || e.Row.OrigReceiptNbr == null)
      return;
    ((SelectedEntityEvent<PX.Objects.PO.POReceipt>) PXEntityEventBase<PX.Objects.PO.POReceipt>.Container<PX.Objects.PO.POReceipt.Events>.Select((Expression<Func<PX.Objects.PO.POReceipt.Events, PXEntityEvent<PX.Objects.PO.POReceipt.Events>>>) (ev => ev.CorrectionReceiptCreated))).FireOn((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row == null || e.Row.OrigReceiptNbr == null)
      return;
    ((SelectedEntityEvent<PX.Objects.PO.POReceipt>) PXEntityEventBase<PX.Objects.PO.POReceipt>.Container<PX.Objects.PO.POReceipt.Events>.Select((Expression<Func<PX.Objects.PO.POReceipt.Events, PXEntityEvent<PX.Objects.PO.POReceipt.Events>>>) (ev => ev.CorrectionReceiptDeleted))).FireOn((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row.IsUnderCorrection.GetValueOrDefault() || e.Row.Canceled.GetValueOrDefault())
    {
      PX.Objects.PO.POReceipt poReceipt = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.PO.POReceipt.origReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
      {
        e.Row
      }, Array.Empty<object>()));
      e.Row.CorrectionReceiptNbr = poReceipt?.ReceiptNbr;
    }
    if (!e.Row.Canceled.GetValueOrDefault())
      return;
    this.LinkReversalInventoryDocument(e.Row);
  }

  protected virtual void LinkReversalInventoryDocument(PX.Objects.PO.POReceipt receipt)
  {
    PX.Objects.IN.INRegister inRegister = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INTran>.On<KeysRelation<CompositeKey<Field<INTran.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INTran.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INTran>, PX.Objects.IN.INRegister, INTran>.And<BqlOperand<INTran.tranType, IBqlString>.IsEqual<INTranType.receipt>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.isCorrection, Equal<True>>>>, And<BqlOperand<PX.Objects.IN.INRegister.docType, IBqlString>.IsEqual<INDocType.issue>>>, And<BqlOperand<PX.Objects.IN.INRegister.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.IN.INRegister.pOReceiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<PX.Objects.PO.POReceipt.invtRefNbr>, IsNull>>>>.Or<BqlOperand<PX.Objects.IN.INRegister.refNbr, IBqlString>.IsNotEqual<BqlField<PX.Objects.PO.POReceipt.invtRefNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<INTran.refNbr, IBqlString>.IsNull>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()));
    if (inRegister == null)
      return;
    receipt.ReversalInvtDocType = inRegister.DocType;
    receipt.ReversalInvtRefNbr = inRegister.RefNbr;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row == null)
      return;
    bool? nullable;
    if (e.Row.CorrectionReceiptNbr != null)
    {
      nullable = e.Row.Canceled;
      if (nullable.GetValueOrDefault())
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceipt.receiptType>((object) e.Row, (object) null, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The current document has been corrected.", (PXErrorLevel) 3));
    }
    bool flag = e.Row.ReceiptType == "RT" && !Correction.HideCorrectionUI;
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POReceipt.origReceiptNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POReceipt.correctionReceiptNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POReceipt.reversalInvtRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetVisibility<PX.Objects.PO.POReceiptLine.isAdjusted>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache, (object) null, flag ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    ((PXAction) this.correctReceipt).SetVisible(flag);
    ((PXAction) this.cancelReceipt).SetVisible(flag);
    ((PXAction) this.correctReceipt).SetEnabled(flag);
    ((PXAction) this.cancelReceipt).SetEnabled(flag);
    bool val = e.Row.OrigReceiptNbr != null;
    if (val)
    {
      ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).AllowInsert = false;
      ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.addPOOrder).SetEnabled(false);
      ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.addPOOrderLine).SetEnabled(false);
      ((PXAction) ((PXGraphExtension<POReceiptEntry>) this).Base.addPOReceiptLine).SetEnabled(false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceipt.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceipt.vendorLocationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) e).Cache, (object) e.Row, false);
    }
    nullable = e.Row.Released;
    bool valueOrDefault = nullable.GetValueOrDefault();
    ((PXAction) this.resetCorrectionLine).SetVisible(val);
    ((PXAction) this.resetCorrectionLine).SetEnabled(val && !valueOrDefault);
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial.AccumulatorAttribute.SuppressValidateDuplicatesOnReceipt((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, val);
    SiteLotSerial.AccumulatorAttribute.SuppressValidateDuplicatesOnReceipt((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, val);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty>, PX.Objects.PO.POReceiptLine, object>) e).NewValue == null)
      return;
    Decimal? nullable = e.Row.ReturnedQty;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue || string.IsNullOrEmpty(e.Row?.OrigReceiptNbr) || !e.Row.IsCorrection.GetValueOrDefault())
      return;
    nullable = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty>, PX.Objects.PO.POReceiptLine, object>) e).NewValue;
    Decimal? returnedQty = e.Row.ReturnedQty;
    if (nullable.GetValueOrDefault() < returnedQty.GetValueOrDefault() & nullable.HasValue & returnedQty.HasValue)
      throw new PXSetPropertyException((IBqlTable) e.Row, "The quantity in the receipt line cannot be less than {0} because the line is fully or partially returned in the original purchase receipt ({1}).", new object[2]
      {
        (object) e.Row.ReturnedQty.ToString(),
        (object) e.Row.OrigReceiptNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt.receiptDate> e)
  {
    if (this._creatingCorrectionReceipt || string.IsNullOrEmpty(e.Row?.OrigReceiptNbr) || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt.receiptDate>, PX.Objects.PO.POReceipt, object>) e).NewValue == null)
      return;
    DateTime? receiptDate = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, e.Row.ReceiptType, e.Row.OrigReceiptNbr).ReceiptDate;
    DateTime? newValue = (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt.receiptDate>, PX.Objects.PO.POReceipt, object>) e).NewValue;
    if ((receiptDate.HasValue == newValue.HasValue ? (receiptDate.HasValue ? (receiptDate.GetValueOrDefault() != newValue.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult in PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.SameAsCurrent.And<BqlOperand<PX.Objects.PO.POReceiptLine.isStockItem, IBqlBool>.IsEqual<False>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
      if (poReceiptLine.IsKit.GetValueOrDefault())
        throw new PXSetPropertyException((IBqlTable) e.Row, "The {0} item is a non-stock kit. Correction of receipt lines with non-stock kits or cancellation of receipts with non-stock kits is not supported.", (PXErrorLevel) 4, new object[1]
        {
          ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>((object) poReceiptLine)
        });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt.finPeriodID> e)
  {
    if (string.IsNullOrEmpty(e.Row?.OrigReceiptNbr) || string.IsNullOrEmpty(e.Row.FinPeriodID) || !(e.Row.POType != "DP"))
      return;
    this.ValidateFinPeriodID(e.Row);
  }

  private void ValidateFinPeriodID(PX.Objects.PO.POReceipt receipt, bool isRowPersisting = false)
  {
    PX.Objects.PO.POReceipt poReceipt = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, receipt.ReceiptType, receipt.OrigReceiptNbr);
    if (!(poReceipt.FinPeriodID != receipt.FinPeriodID))
      return;
    PXCache pxCache = (PXCache) GraphHelper.Caches<PX.Objects.PO.POReceipt>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base);
    PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) receipt, "The posting period of the correction receipt differs from the {0} posting period of the original receipt ({1}). If you need to change the financial period, cancel the original receipt and create a new one.", (PXErrorLevel) 4, new object[2]
    {
      pxCache.GetValueExt<PX.Objects.PO.POReceipt.finPeriodID>((object) poReceipt),
      (object) receipt.OrigReceiptNbr
    });
    if (pxCache.RaiseExceptionHandling<PX.Objects.PO.POReceipt.finPeriodID>((object) receipt, (object) FinPeriodIDFormattingAttribute.FormatForDisplay(receipt.FinPeriodID), (Exception) propertyException) & isRowPersisting)
      throw new PXRowPersistingException("FinPeriodID", (object) FinPeriodIDFormattingAttribute.FormatForDisplay(receipt.FinPeriodID), "The posting period of the correction receipt differs from the {0} posting period of the original receipt ({1}). If you need to change the financial period, cancel the original receipt and create a new one.", new object[2]
      {
        pxCache.GetValueExt<PX.Objects.PO.POReceipt.finPeriodID>((object) poReceipt),
        (object) receipt.OrigReceiptNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.PO.POReceiptLine> e)
  {
    if (e.Row?.OrigReceiptNbr != null && ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current?.OrigReceiptNbr != null && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current), (PXEntryStatus) 3, (PXEntryStatus) 4))
      throw new PXSetPropertyException((IBqlTable) e.Row, "The line from the original receipt ({0}) cannot be deleted. If the item was not received in this receipt, change the item quantity to 0.", new object[1]
      {
        (object) e.Row.OrigReceiptNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine> e)
  {
    if (e.Row == null || ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current?.OrigReceiptNbr == null)
      return;
    bool? isStockItem = e.Row.IsStockItem;
    bool flag = false;
    if (isStockItem.GetValueOrDefault() == flag & isStockItem.HasValue)
    {
      bool? nullable = e.Row.IsKit;
      if (nullable.GetValueOrDefault())
      {
        nullable = e.Row.IsAdjustedIN;
        if (nullable.GetValueOrDefault())
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.isAdjusted>((object) e.Row, (object) null, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The {0} item is a non-stock kit. Correction of receipt lines with non-stock kits or cancellation of receipts with non-stock kits is not supported.", (PXErrorLevel) 5, new object[1]
          {
            ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>((object) e.Row)
          }));
        else
          ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.isAdjusted>((object) e.Row, (object) null, (Exception) null);
      }
    }
    if (e.Row.OrigReceiptNbr == null)
      return;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.allowOpen>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceiptLine>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POReceipt> e)
  {
    if (e.Row.OrigReceiptNbr == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.POReceipt>>) e).Cache.ObjectsEqual<PX.Objects.PO.POReceipt.receiptDate>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PX.Objects.PO.POReceiptLine poReceiptLine in ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).View.SelectMultiBound((object[]) new PX.Objects.PO.POReceipt[1]
    {
      e.Row
    }, Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine copy = PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(poReceiptLine);
      copy.ReceiptDate = e.Row.ReceiptDate;
      ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Update(copy);
    }
  }

  private void OnReceiptLineINAdjusted<Field>(PX.Objects.PO.POReceiptLine row, object oldValue, object newValue = null) where Field : IBqlField
  {
    if (!row.IsCorrection.GetValueOrDefault() || this._creatingCorrectionReceipt)
      return;
    bool flag1 = newValue != null;
    if (newValue == null)
      newValue = ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetValue<Field>((object) row);
    if (oldValue == null && newValue == null || oldValue != null && newValue != null && oldValue.Equals(newValue))
      return;
    PX.Objects.PO.POReceiptLine copy = flag1 ? PXCache<PX.Objects.PO.POReceiptLine>.CreateCopy(row) : (PX.Objects.PO.POReceiptLine) null;
    bool flag2 = false;
    if (!row.IsAdjusted.GetValueOrDefault())
    {
      ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.SetValueExt<PX.Objects.PO.POReceiptLine.isAdjusted>((object) row, (object) true);
      flag2 = true;
    }
    if (!row.IsAdjustedIN.GetValueOrDefault())
    {
      ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.SetValueExt<PX.Objects.PO.POReceiptLine.isAdjustedIN>((object) row, (object) true);
      flag2 = true;
    }
    if (!(flag1 & flag2))
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache, (object) row);
    ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.RaiseRowUpdated((object) row, (object) copy);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.siteID> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.siteID>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.siteID>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.locationID>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.locationID>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.receiptQty>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.uOM> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.uOM>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.uOM>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.curyUnitCost> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.curyUnitCost>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.curyUnitCost>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.curyExtCost> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.curyExtCost>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.curyExtCost>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.lotSerialNbr> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.lotSerialNbr>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.lotSerialNbr>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.expireDate> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.expireDate>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.expireDate>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptDate> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.receiptDate>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptDate>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.curyTranCost> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.curyTranCost>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.curyTranCost>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.pOAccrualAcctID> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.pOAccrualAcctID>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.pOAccrualAcctID>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.pOAccrualSubID> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.pOAccrualSubID>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.pOAccrualSubID>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.expenseAcctID> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.expenseAcctID>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.expenseAcctID>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.expenseSubID> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.expenseSubID>(e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.expenseSubID>, PX.Objects.PO.POReceiptLine, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.tranCost> e)
  {
    this.OnReceiptLineINAdjusted<PX.Objects.PO.POReceiptLine.tranCost>(e.Row, e.OldValue, ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.tranCost>>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.tranDesc> e)
  {
    if (!e.Row.IsCorrection.GetValueOrDefault() || this._creatingCorrectionReceipt)
      return;
    bool? isAdjusted = e.Row.IsAdjusted;
    bool flag = false;
    if (!(isAdjusted.GetValueOrDefault() == flag & isAdjusted.HasValue) || !((string) e.NewValue != (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.tranDesc>, PX.Objects.PO.POReceiptLine, object>) e).OldValue))
      return;
    isAdjusted = e.Row.IsAdjusted;
    if (isAdjusted.GetValueOrDefault())
      return;
    ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.SetValueExt<PX.Objects.PO.POReceiptLine.isAdjusted>((object) e.Row, (object) true);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POReceiptLine> e)
  {
    if (!e.Row.IsCorrection.GetValueOrDefault() || this._creatingCorrectionReceipt || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.POReceiptLine>>) e).Cache.ObjectsEqual<PX.Objects.PO.POReceiptLine.isAdjustedIN>((object) e.Row, (object) e.OldRow))
      return;
    if (EnumerableExtensions.IsIn<string>(e.Row.LineType, "GS", "NO"))
    {
      ItemPlan<POReceiptEntry, PX.Objects.PO.POOrder, PX.Objects.PO.POLine> implementation = ((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).FindImplementation<ItemPlan<POReceiptEntry, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>>();
      PX.Objects.PO.POLine referencedPoLine = ((PXGraphExtension<POReceiptEntry>) this).Base.GetReferencedPOLine(e.Row.POType, e.Row.PONbr, e.Row.POLineNbr);
      GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.poline).Cache, (object) referencedPoLine);
      PX.Objects.PO.POLine row = referencedPoLine;
      implementation.RaiseRowUpdated(row);
    }
    if (e.Row.IsAdjustedIN.GetValueOrDefault())
      this.GenerateReverseSplits(e.Row);
    this.GeneratePlansForAdjustedLine(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.PO.POReceiptLineSplit> e)
  {
    this.SetLineCorrectedFromSplit(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POReceiptLineSplit> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.POReceiptLineSplit>>) e).Cache.ObjectsEqual<PX.Objects.PO.POReceiptLineSplit.locationID, PX.Objects.PO.POReceiptLineSplit.qty, PX.Objects.PO.POReceiptLineSplit.lotSerialNbr, PX.Objects.PO.POReceiptLineSplit.expireDate>((object) e.Row, (object) e.OldRow))
      return;
    this.SetLineCorrectedFromSplit(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.isAdjusted> e)
  {
    if (this._creatingCorrectionReceipt)
      return;
    PX.Objects.PO.POReceiptLine row = e.Row;
    bool? nullable;
    int num;
    if (row == null)
    {
      num = 0;
    }
    else
    {
      nullable = row.IsCorrection;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0)
      return;
    nullable = e.Row.IsStockItem;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    nullable = e.Row.IsKit;
    if (nullable.GetValueOrDefault() && (bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.isAdjusted>, PX.Objects.PO.POReceiptLine, object>) e).NewValue && !this._updatingCurrencyInfo)
      throw new PXException("The {0} item is a non-stock kit. Correction of receipt lines with non-stock kits or cancellation of receipts with non-stock kits is not supported.", new object[1]
      {
        ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>((object) e.Row)
      });
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.PO.POReceiptLineSplit> e)
  {
    this.SetLineCorrectedFromSplit(e.Row);
  }

  public virtual void SetLineCorrectedFromSplit(PX.Objects.PO.POReceiptLineSplit row)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.OrigReceiptNbr == null || this.Base3.SuppressedMode || this._creatingCorrectionReceipt)
      return;
    PX.Objects.PO.POReceiptLine poReceiptLine = PXParentAttribute.SelectParent<PX.Objects.PO.POReceiptLine>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.splits).Cache, (object) row);
    bool? nullable = poReceiptLine.IsAdjusted;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      poReceiptLine.IsAdjusted = new bool?(true);
      using (this.Base3.SuppressedModeScope(true))
        poReceiptLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Update(poReceiptLine);
    }
    nullable = poReceiptLine.IsAdjustedIN;
    bool flag2 = false;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      return;
    poReceiptLine.IsAdjustedIN = new bool?(true);
    using (this.Base3.SuppressedModeScope(true))
      ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Update(poReceiptLine);
  }

  protected virtual void GenerateReverseSplits(PX.Objects.PO.POReceiptLine row)
  {
    if (!POLineType.IsStockNonDropShip(row.LineType))
      return;
    if (((PXSelectBase) this.ReverseSplits).View.SelectSingleBound((object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      row
    }, Array.Empty<object>()) != null)
      return;
    foreach (PXResult<PX.Objects.PO.POReceiptLineSplit> pxResult in PXSelectBase<PX.Objects.PO.POReceiptLineSplit, PXViewOf<PX.Objects.PO.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLineSplit.receiptType, Equal<BqlField<PX.Objects.PO.POReceiptLine.origReceiptType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLineSplit.receiptNbr, Equal<BqlField<PX.Objects.PO.POReceiptLine.origReceiptNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptLineNbr, IBqlInt>.FromCurrent>>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      row
    }, Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLineSplit receiptLineSplit = PXResult<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(pxResult);
      ((PXSelectBase<PX.Objects.PO.Reverse.POReceiptLineSplit>) this.ReverseSplits).Insert(new PX.Objects.PO.Reverse.POReceiptLineSplit()
      {
        ReceiptType = row.ReceiptType,
        ReceiptNbr = row.ReceiptNbr,
        LineNbr = row.LineNbr,
        ReceiptDate = row.ReceiptDate,
        PONbr = row.PONbr,
        LineType = receiptLineSplit.LineType,
        InvtMult = new short?((short) -1),
        InventoryID = receiptLineSplit.InventoryID,
        SubItemID = receiptLineSplit.SubItemID,
        SiteID = receiptLineSplit.SiteID,
        LocationID = receiptLineSplit.LocationID,
        LotSerialNbr = receiptLineSplit.LotSerialNbr,
        ExpireDate = receiptLineSplit.ExpireDate,
        UOM = receiptLineSplit.UOM,
        BaseQty = receiptLineSplit.BaseQty,
        Qty = receiptLineSplit.Qty
      });
    }
  }

  protected virtual void GeneratePlansForAdjustedLine(PX.Objects.PO.POReceiptLine row)
  {
    POReceiptLineSplitPlan implementation1 = ((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).FindImplementation<POReceiptLineSplitPlan>();
    if (implementation1 != null)
    {
      PXView view = ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.splits).View;
      object[] objArray1 = (object[]) new PX.Objects.PO.POReceiptLine[1]
      {
        row
      };
      object[] objArray2 = Array.Empty<object>();
      foreach (PX.Objects.PO.POReceiptLineSplit row1 in view.SelectMultiBound(objArray1, objArray2))
      {
        implementation1.RaiseRowUpdated(row1);
        GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.splits).Cache, (object) row1);
      }
    }
    POReceiptLineSplitPlanUnassigned implementation2 = ((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).FindImplementation<POReceiptLineSplitPlanUnassigned>();
    if (implementation2 == null)
      return;
    foreach (PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit> pxResult in PXSelectBase<PX.Objects.PO.Unassigned.POReceiptLineSplit, PXViewOf<PX.Objects.PO.Unassigned.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.Unassigned.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.Unassigned.POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptType>, Field<PX.Objects.PO.Unassigned.POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptNbr>, Field<PX.Objects.PO.Unassigned.POReceiptLineSplit.lineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLine, PX.Objects.PO.Unassigned.POReceiptLineSplit>, PX.Objects.PO.POReceiptLine, PX.Objects.PO.Unassigned.POReceiptLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      row
    }, Array.Empty<object>()))
    {
      PX.Objects.PO.Unassigned.POReceiptLineSplit row2 = PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit>.op_Implicit(pxResult);
      implementation2.RaiseRowUpdated(row2);
      GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<PX.Objects.PO.Unassigned.POReceiptLineSplit>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), (object) row2);
    }
  }

  /// <summary>
  /// <see cref="M:PX.Data.PXGraph.PrePersist" />
  /// </summary>
  [PXOverride]
  public virtual bool PrePersist(Func<bool> basePrePersist)
  {
    foreach (PX.Objects.PO.POReceipt poReceipt1 in ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Cache.Inserted)
    {
      if (poReceipt1.OrigReceiptNbr != null)
      {
        PX.Objects.PO.POReceipt poReceipt2 = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poReceipt1.ReceiptType, poReceipt1.OrigReceiptNbr);
        bool? nullable = poReceipt2.Released;
        if (nullable.GetValueOrDefault())
        {
          nullable = poReceipt2.Canceled;
          if (!nullable.GetValueOrDefault())
          {
            nullable = poReceipt2.IsUnderCorrection;
            if (!nullable.GetValueOrDefault())
              continue;
          }
        }
        throw new PXLockViolationException(typeof (PX.Objects.PO.POReceipt), (PXDBOperation) 2, (object[]) new string[2]
        {
          poReceipt1.ReceiptType,
          poReceipt1.OrigReceiptNbr
        });
      }
    }
    return basePrePersist();
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptLineSplittingExtension.GetOpenQtyForReceiptWithPO(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POLine)" />
  [PXOverride]
  public virtual Decimal? GetOpenQtyForReceiptWithPO(
    PX.Objects.PO.POReceiptLine receiptLine,
    PX.Objects.PO.POLine poLine,
    Func<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine, Decimal?> baseMethod)
  {
    if (!receiptLine.IsCorrection.GetValueOrDefault())
      return baseMethod(receiptLine, poLine);
    PX.Objects.PO.POReceiptLine poReceiptLine = PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, receiptLine.OrigReceiptType, receiptLine.OrigReceiptNbr, receiptLine.OrigReceiptLineNbr);
    Decimal num1;
    Decimal? nullable;
    if (receiptLine.UOM == poReceiptLine.UOM)
    {
      num1 = poReceiptLine.ReturnedQty.GetValueOrDefault();
    }
    else
    {
      PXCache<PX.Objects.PO.POReceiptLine> sender = GraphHelper.Caches<PX.Objects.PO.POReceiptLine>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base);
      PX.Objects.PO.POReceiptLine Row = receiptLine;
      string uom = receiptLine.UOM;
      nullable = poReceiptLine.BaseReturnedQty;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      num1 = INUnitAttribute.ConvertFromBase<PX.Objects.PO.POReceiptLine.inventoryID>((PXCache) sender, (object) Row, uom, valueOrDefault, INPrecision.QUANTITY);
    }
    nullable = baseMethod(receiptLine, poLine);
    Decimal num2 = num1;
    return !nullable.HasValue ? new Decimal?() : new Decimal?(nullable.GetValueOrDefault() - num2);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptLineSplittingExtension.AllowInsertAndDelete(PX.Objects.PO.POReceiptLine)" />
  [PXOverride]
  public virtual bool AllowInsertAndDelete(
    PX.Objects.PO.POReceiptLine receiptLine,
    Func<PX.Objects.PO.POReceiptLine, bool> baseMethod)
  {
    if (receiptLine != null)
    {
      bool? isCorrection = receiptLine.IsCorrection;
      bool flag = false;
      if (isCorrection.GetValueOrDefault() == flag & isCorrection.HasValue)
        goto label_6;
    }
    if (receiptLine == null || !receiptLine.IsStockItem.GetValueOrDefault())
    {
      if (receiptLine == null)
        return false;
      bool? isKit = receiptLine.IsKit;
      bool flag = false;
      return isKit.GetValueOrDefault() == flag & isKit.HasValue;
    }
label_6:
    return true;
  }

  protected IDisposable CreatingUpdatingCurrencyInfo()
  {
    return (IDisposable) new SimpleScope((System.Action) (() => this._updatingCurrencyInfo = true), (System.Action) (() => this._updatingCurrencyInfo = false));
  }

  /// Overrides <see cref="M:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.recalculateRowBaseValues(PX.Data.PXCache,System.Object,System.Collections.Generic.IEnumerable{PX.Objects.Extensions.MultiCurrency.CuryField})" />
  [PXOverride]
  public virtual void recalculateRowBaseValues(
    PXCache sender,
    object row,
    IEnumerable<CuryField> fields,
    Action<PXCache, object, IEnumerable<CuryField>> baseMethod)
  {
    using (this.CreatingUpdatingCurrencyInfo())
      baseMethod(sender, row, fields);
  }

  /// <summary>
  /// <see cref="M:PX.Objects.PO.POReceiptEntry.UpdateReceiptReleased(PX.Objects.IN.INRegister)" />
  /// </summary>
  [PXOverride]
  public virtual PX.Objects.PO.POReceipt UpdateReceiptReleased(
    PX.Objects.IN.INRegister inRegister,
    Func<PX.Objects.IN.INRegister, PX.Objects.PO.POReceipt> baseMethod)
  {
    PX.Objects.PO.POReceipt poReceipt1 = baseMethod(inRegister);
    if (poReceipt1.OrigReceiptNbr == null)
      return poReceipt1;
    ((SelectedEntityEvent<PX.Objects.PO.POReceipt>) PXEntityEventBase<PX.Objects.PO.POReceipt>.Container<PX.Objects.PO.POReceipt.Events>.Select((Expression<Func<PX.Objects.PO.POReceipt.Events, PXEntityEvent<PX.Objects.PO.POReceipt.Events>>>) (ev => ev.CorrectionReceiptReleased))).FireOn((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poReceipt1);
    if (poReceipt1.IsIntercompany.GetValueOrDefault())
    {
      PX.Objects.PO.POReceipt poReceipt2 = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poReceipt1.ReceiptType, poReceipt1.OrigReceiptNbr);
      poReceipt1.IntercompanyShipmentNbr = poReceipt2?.IntercompanyShipmentNbr;
    }
    return ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Update(poReceipt1);
  }

  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.OnBeforeReleaseReceiptCommit(PX.Objects.IN.INRegister,PX.Objects.CS.DocumentList{PX.Objects.IN.INRegister},System.Collections.Generic.List{PX.Objects.IN.INRegister})" />
  [PXOverride]
  public void OnBeforeReleaseReceiptCommit(
    PX.Objects.IN.INRegister inRegister,
    DocumentList<PX.Objects.IN.INRegister> aINCreated,
    List<PX.Objects.IN.INRegister> forReleaseIN,
    Action<PX.Objects.IN.INRegister, DocumentList<PX.Objects.IN.INRegister>, List<PX.Objects.IN.INRegister>> base_OnBeforeReleaseReceiptCommit)
  {
    PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current;
    if (current.OrigReceiptNbr == null)
      base_OnBeforeReleaseReceiptCommit(inRegister, aINCreated, forReleaseIN);
    else
      this.ReleaseCorrectingInventoryDocument(current, inRegister, aINCreated, forReleaseIN);
  }

  protected virtual void ReleaseCorrectingInventoryDocument(
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.IN.INRegister inRegister,
    DocumentList<PX.Objects.IN.INRegister> aINCreated,
    List<PX.Objects.IN.INRegister> forReleaseIN)
  {
    if (inRegister == null)
      return;
    ((PXGraphExtension<POReceiptEntry>) this).Base.ReleaseINDocuments(new List<PX.Objects.IN.INRegister>()
    {
      inRegister
    }, "IN Document failed to release with the following error: '{0}'.");
  }

  private PX.Objects.PO.POReceiptLine GetOrigPOReceiptLine(PX.Objects.PO.POReceiptLine line)
  {
    return PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, line.OrigReceiptType, line.OrigReceiptNbr, line.OrigReceiptLineNbr) ?? throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceiptLine>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), new object[3]
    {
      (object) line.OrigReceiptType,
      (object) line.OrigReceiptNbr,
      (object) line.OrigReceiptLineNbr
    });
  }

  /// <summary>
  /// <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.UpdatePOOnRelease.GetCompletedQtyDelta(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POLine)" />
  /// </summary>
  [PXOverride]
  public Decimal? GetCompletedQtyDelta(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    Func<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine, Decimal?> baseGetCompletedQtyDelta)
  {
    Decimal? completedQtyDelta = baseGetCompletedQtyDelta(line, poLine);
    if (line.IsCorrection.GetValueOrDefault())
    {
      PX.Objects.PO.POReceiptLine origPoReceiptLine = this.GetOrigPOReceiptLine(line);
      Decimal? nullable1 = completedQtyDelta;
      Decimal? nullable2 = baseGetCompletedQtyDelta(origPoReceiptLine, poLine);
      completedQtyDelta = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    }
    return completedQtyDelta;
  }

  [PXOverride]
  public PX.Objects.PO.POLine UpdatePOLineOnReceipt(
    PXResult<PX.Objects.PO.POReceiptLine> res,
    PX.Objects.PO.POLine poLine,
    Func<PXResult<PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POLine, PX.Objects.PO.POLine> baseUpdatePOLineOnReceipt)
  {
    poLine = baseUpdatePOLineOnReceipt(res, poLine);
    PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(res);
    if (!poReceiptLine.IsCorrection.GetValueOrDefault())
      return poLine;
    poLine = ((PXGraphExtension<POReceiptEntry>) this).Base.ReopenPOLineIfNeeded(poLine);
    if (EnumerableExtensions.IsIn<string>(poReceiptLine.LineType, "GS", "NO") && poLine.Completed.GetValueOrDefault() && poLine.PlanID.HasValue)
    {
      INItemPlan inItemPlan1 = INItemPlan.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poLine.PlanID, (PKFindOptions) 1);
      if (inItemPlan1 != null)
      {
        Decimal? planQty = inItemPlan1.PlanQty;
        Decimal num = 0M;
        if (!(planQty.GetValueOrDefault() == num & planQty.HasValue))
          goto label_12;
      }
      poLine.PlanID = new long?();
      GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.poline).Cache, (object) poLine, true);
      if (inItemPlan1 != null)
      {
        POReceiptEntry poReceiptEntry = ((PXGraphExtension<POReceiptEntry>) this).Base;
        object[] objArray1 = (object[]) new INItemPlan[1]
        {
          inItemPlan1
        };
        object[] objArray2 = Array.Empty<object>();
        foreach (INItemPlan inItemPlan2 in GraphHelper.RowCast<INItemPlan>((IEnumerable) ((IEnumerable<PXResult<INItemPlan>>) PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.supplyPlanID, IBqlLong>.IsEqual<BqlField<INItemPlan.planID, IBqlLong>.FromCurrent>>>.Config>.SelectMultiBound((PXGraph) poReceiptEntry, objArray1, objArray2)).AsEnumerable<PXResult<INItemPlan>>()).ToList<INItemPlan>())
        {
          inItemPlan2.SupplyPlanID = new long?();
          GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Update(inItemPlan2);
        }
        GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Delete(inItemPlan1);
      }
    }
label_12:
    return poLine;
  }

  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.ActualizeAndValidatePOReceiptForReleasing(PX.Objects.PO.POReceipt)" />
  [PXOverride]
  public PX.Objects.PO.POReceipt ActualizeAndValidatePOReceiptForReleasing(
    PX.Objects.PO.POReceipt receipt,
    Func<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt> base_ActualizeAndValidatePOReceiptForReleasing)
  {
    if (receipt.OrigReceiptNbr != null)
      this.ThrowIfOriginalIssueIsNotReleased(receipt);
    return base_ActualizeAndValidatePOReceiptForReleasing(receipt);
  }

  private void ThrowIfOriginalIssueIsNotReleased(PX.Objects.PO.POReceipt receipt)
  {
    PX.Objects.IN.INRegister inRegister = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.released, Equal<False>>>>, And<BqlOperand<PX.Objects.IN.INRegister.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.origReceiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.IN.INRegister.pOReceiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
    {
      receipt
    }, Array.Empty<object>()));
    if (inRegister != null)
      throw new PXException("The {0} receipt cannot be released because the original issue ({1}) is not released. Release the issue before releasing the receipt.", new object[2]
      {
        (object) receipt.ReceiptNbr,
        (object) inRegister.RefNbr
      });
  }

  /// <summary>
  /// <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.UpdatePOOnRelease.UpdatePOAccrualStatus(PX.Objects.PO.POAccrualStatus,PX.Objects.PO.POLine,PX.Objects.PO.POReceiptLine,PX.Objects.PO.POOrder,PX.Objects.PO.POReceipt)" />
  /// </summary>
  [PXOverride]
  public virtual POAccrualStatus UpdatePOAccrualStatus(
    POAccrualStatus origRow,
    PX.Objects.PO.POLine poLine,
    PX.Objects.PO.POReceiptLine rctLine,
    PX.Objects.PO.POOrder order,
    PX.Objects.PO.POReceipt receipt,
    Func<POAccrualStatus, PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POOrder, PX.Objects.PO.POReceipt, POAccrualStatus> baseMethod)
  {
    if (rctLine.IsCorrection.GetValueOrDefault() && origRow != null)
    {
      PX.Objects.PO.POReceiptLine receiptLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceiptLine.origReceiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
      {
        rctLine
      }, Array.Empty<object>()));
      POAccrualStatus copy = (POAccrualStatus) ((PXSelectBase) ((PXGraphExtension<UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>) this).Base2.poAccrualUpdate).Cache.CreateCopy((object) origRow);
      if (copy.Type == "R")
      {
        copy.ReceiptNbr = receipt.ReceiptNbr;
        copy.ReceiptType = receipt.ReceiptType;
      }
      bool isDSReceipt = receipt.POType == "DP";
      bool nulloutReceivedQty = origRow != null && (!origRow.ReceivedQty.HasValue || !EnumerableExtensions.IsIn<string>(origRow.ReceivedUOM, (string) null, rctLine.UOM));
      this.SubtractReceiptLineFromPOAccrualStatus(receiptLine, copy, nulloutReceivedQty, isDSReceipt);
      if (isDSReceipt)
      {
        Decimal? receiptQty = receiptLine.ReceiptQty;
        Decimal num1 = 0M;
        if (!(receiptQty.GetValueOrDefault() == num1 & receiptQty.HasValue))
        {
          receiptQty = rctLine.ReceiptQty;
          Decimal num2 = 0M;
          if (receiptQty.GetValueOrDefault() == num2 & receiptQty.HasValue)
          {
            POAccrualStatus poAccrualStatus = copy;
            int? unreleasedReceiptCntr = poAccrualStatus.UnreleasedReceiptCntr;
            poAccrualStatus.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() - 1) : new int?();
          }
        }
        receiptQty = receiptLine.ReceiptQty;
        Decimal num3 = 0M;
        if (receiptQty.GetValueOrDefault() == num3 & receiptQty.HasValue)
        {
          receiptQty = rctLine.ReceiptQty;
          Decimal num4 = 0M;
          if (!(receiptQty.GetValueOrDefault() == num4 & receiptQty.HasValue))
          {
            POAccrualStatus poAccrualStatus = copy;
            int? unreleasedReceiptCntr = poAccrualStatus.UnreleasedReceiptCntr;
            poAccrualStatus.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() + 1) : new int?();
          }
        }
      }
      ((PXSelectBase<POAccrualStatus>) ((PXGraphExtension<UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>) this).Base2.poAccrualUpdate).Update(copy);
    }
    return baseMethod(origRow, poLine, rctLine, order, receipt);
  }

  private void SubtractReceiptLineFromPOAccrualStatus(
    PX.Objects.PO.POReceiptLine receiptLine,
    POAccrualStatus accrualStatus,
    bool nulloutReceivedQty,
    bool isDSReceipt,
    bool isCancelation = false)
  {
    POAccrualStatus poAccrualStatus1 = accrualStatus;
    Decimal? nullable1 = poAccrualStatus1.ReceivedQty;
    short? invtMult;
    Decimal? nullable2;
    Decimal? nullable3;
    if (!nulloutReceivedQty)
    {
      invtMult = receiptLine.InvtMult;
      nullable2 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal? receiptQty = receiptLine.ReceiptQty;
      nullable3 = nullable2.HasValue & receiptQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * receiptQty.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable3 = new Decimal?();
    Decimal? nullable4 = nullable3;
    poAccrualStatus1.ReceivedQty = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    POAccrualStatus poAccrualStatus2 = accrualStatus;
    nullable4 = poAccrualStatus2.BaseReceivedQty;
    invtMult = receiptLine.InvtMult;
    Decimal? nullable5 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    nullable2 = receiptLine.BaseReceiptQty;
    nullable1 = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable6;
    if (!(nullable4.HasValue & nullable1.HasValue))
    {
      nullable2 = new Decimal?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new Decimal?(nullable4.GetValueOrDefault() - nullable1.GetValueOrDefault());
    poAccrualStatus2.BaseReceivedQty = nullable6;
    POAccrualStatus poAccrualStatus3 = accrualStatus;
    nullable1 = poAccrualStatus3.ReceivedCost;
    invtMult = receiptLine.InvtMult;
    nullable2 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    nullable5 = receiptLine.TranCostFinal ?? receiptLine.TranCost;
    nullable4 = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable5 = new Decimal?();
      nullable7 = nullable5;
    }
    else
      nullable7 = new Decimal?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault());
    poAccrualStatus3.ReceivedCost = nullable7;
    if (EnumerableExtensions.IsIn<string>(receiptLine.LineType, "FT", "SV"))
      return;
    if (!isDSReceipt)
    {
      if (!isCancelation)
        return;
      nullable4 = receiptLine.BaseQty;
      Decimal num = 0M;
      if (nullable4.GetValueOrDefault() == num & nullable4.HasValue)
        return;
      bool? nullable8 = receiptLine.INReleased;
      if (!nullable8.GetValueOrDefault())
      {
        nullable8 = receiptLine.IsCorrection;
        if (!nullable8.GetValueOrDefault())
          return;
      }
      POAccrualStatus poAccrualStatus4 = accrualStatus;
      int? unreleasedReceiptCntr = poAccrualStatus4.UnreleasedReceiptCntr;
      poAccrualStatus4.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() + 1) : new int?();
    }
    else if (PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INTran.pOReceiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptType>, Field<INTran.pOReceiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptNbr>, Field<INTran.pOReceiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLine, INTran>, PX.Objects.PO.POReceiptLine, INTran>.SameAsCurrent.And<BqlOperand<INTran.docType, IBqlString>.IsEqual<INDocType.issue>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      receiptLine
    }, Array.Empty<object>())) == null)
    {
      if (isCancelation)
      {
        nullable4 = receiptLine.ReceiptQty;
        Decimal num = 0M;
        if (nullable4.GetValueOrDefault() == num & nullable4.HasValue)
          return;
      }
      POAccrualStatus poAccrualStatus5 = accrualStatus;
      int? unreleasedReceiptCntr = poAccrualStatus5.UnreleasedReceiptCntr;
      poAccrualStatus5.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() - 1) : new int?();
    }
    else
    {
      POAccrualStatus poAccrualStatus6 = accrualStatus;
      int? unreleasedReceiptCntr = poAccrualStatus6.UnreleasedReceiptCntr;
      poAccrualStatus6.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() + 1) : new int?();
    }
  }

  /// <summary>
  /// <see cref="M:PX.Objects.PO.GraphExtensions.POReceiptEntryExt.UpdatePOOnRelease.UpdatePOReceiptLineAccrualDetail(PX.Objects.PO.POReceiptLine)" />
  /// </summary>
  [PXOverride]
  public virtual POAccrualDetail UpdatePOReceiptLineAccrualDetail(
    PX.Objects.PO.POReceiptLine receiptLine,
    Func<PX.Objects.PO.POReceiptLine, POAccrualDetail> baseMethod)
  {
    bool? isCorrection = receiptLine.IsCorrection;
    bool flag1 = false;
    if (isCorrection.GetValueOrDefault() == flag1 & isCorrection.HasValue)
      return baseMethod(receiptLine);
    this.ReverseAccrualDetail(receiptLine, false);
    POAccrualDetail poAccrualDetail = baseMethod(receiptLine);
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.POType != "DP")
    {
      bool? isAdjustedIn = receiptLine.IsAdjustedIN;
      bool flag2 = false;
      if (isAdjustedIn.GetValueOrDefault() == flag2 & isAdjustedIn.HasValue)
      {
        Decimal? baseReceiptQty = receiptLine.BaseReceiptQty;
        Decimal num = 0M;
        if (!(baseReceiptQty.GetValueOrDefault() == num & baseReceiptQty.HasValue))
        {
          INTran originalTran = ((PXGraphExtension<POReceiptEntry>) this).Base.GetOriginalTran(receiptLine);
          if (originalTran != null)
          {
            poAccrualDetail.Posted = new bool?(true);
            poAccrualDetail.UseOrigINDoc = new bool?(true);
            poAccrualDetail.OrigINDocRefNbr = originalTran.RefNbr;
            poAccrualDetail.OrigINDocType = originalTran.DocType;
            poAccrualDetail = ((PXSelectBase<POAccrualDetail>) ((PXGraphExtension<UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>) this).Base2.poAccrualDetailUpdate).Update(poAccrualDetail);
          }
        }
      }
    }
    return poAccrualDetail;
  }

  private void ReverseAccrualDetail(PX.Objects.PO.POReceiptLine receiptLine, bool isCancelation)
  {
    POAccrualDetail poAccrualDetail = PXResultset<POAccrualDetail>.op_Implicit(PXSelectBase<POAccrualDetail, PXViewOf<POAccrualDetail>.BasedOn<SelectFromBase<POAccrualDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.AsOptional>>>>, And<BqlOperand<POAccrualDetail.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.AsOptional>>>>.And<BqlOperand<POAccrualDetail.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.AsOptional>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) null, new object[3]
    {
      isCancelation ? (object) receiptLine.ReceiptType : (object) receiptLine.OrigReceiptType,
      isCancelation ? (object) receiptLine.ReceiptNbr : (object) receiptLine.OrigReceiptNbr,
      (object) (isCancelation ? receiptLine.LineNbr : receiptLine.OrigReceiptLineNbr)
    }));
    if (poAccrualDetail == null)
      return;
    poAccrualDetail.IsReversed = new bool?(true);
    poAccrualDetail.ReversedFinPeriodID = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.FinPeriodID;
    ((PXSelectBase<POAccrualDetail>) ((PXGraphExtension<UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>) this).Base2.poAccrualDetailUpdate).Update(poAccrualDetail);
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
    this.AddReversalLinesToInventory(inRegisterEntry);
  }

  protected virtual void AddReversalLinesToInventory(
    Lazy<INRegisterEntryBase> inRegisterEntry,
    bool isCancellation = false)
  {
    INTran inTran1 = (INTran) null;
    PX.Objects.PO.POReceiptLine poReceiptLine = (PX.Objects.PO.POReceiptLine) null;
    foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult in isCancellation ? PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.PO.POReceiptLineSplit>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.SameAsCurrent>.Order<PX.Data.BQL.Fluent.By<Asc<PX.Objects.PO.POReceiptLine.receiptType, Asc<PX.Objects.PO.POReceiptLine.receiptNbr, Asc<PX.Objects.PO.POReceiptLine.lineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, Array.Empty<object>()) : PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectJoin<PX.Objects.PO.POReceiptLine, LeftJoin<PX.Objects.PO.Reverse.POReceiptLineSplit, On<PX.Objects.PO.Reverse.POReceiptLineSplit.FK.ReceiptLine>, LeftJoin<INTran, On<INTran.FK.POReceiptLine>, LeftJoin<INItemPlan, On<INItemPlan.planID, Equal<PX.Objects.PO.POReceiptLineSplit.planID>>, InnerJoin<POReceiptLine2, On<POReceiptLine2.receiptType, Equal<PX.Objects.PO.POReceiptLine.origReceiptType>, And<POReceiptLine2.receiptNbr, Equal<PX.Objects.PO.POReceiptLine.origReceiptNbr>, And<POReceiptLine2.lineNbr, Equal<PX.Objects.PO.POReceiptLine.origReceiptLineNbr>>>>>>>>, Where2<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.SameAsCurrent, And<PX.Objects.PO.POReceiptLine.isAdjustedIN, Equal<True>, And<INTran.refNbr, IsNull>>>, OrderBy<Asc<PX.Objects.PO.POReceiptLine.receiptType, Asc<PX.Objects.PO.POReceiptLine.receiptNbr, Asc<PX.Objects.PO.POReceiptLine.lineNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine line = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
      IItemPlanPOReceiptSource split = !isCancellation ? (IItemPlanPOReceiptSource) ((PXResult) pxResult).GetItem<PX.Objects.PO.Reverse.POReceiptLineSplit>() : (IItemPlanPOReceiptSource) ((PXResult) pxResult).GetItem<PX.Objects.PO.POReceiptLineSplit>();
      PX.Objects.PO.POReceiptLine origLine = !isCancellation ? (PX.Objects.PO.POReceiptLine) ((PXResult) pxResult).GetItem<POReceiptLine2>() : line;
      this.OnBeforeAddingReversalLine(line, origLine, split);
      Lazy<INTran> lazy = Lazy.By<INTran>((Func<INTran>) (() => ((PXGraphExtension<POReceiptEntry>) this).Base.GetOriginalTran(line)));
      int num1 = POLineType.IsStockNonDropShip(line.LineType) ? 1 : 0;
      bool flag1 = POLineType.IsNonStockNonServiceNonDropShip(line.LineType);
      int num2 = flag1 ? 1 : 0;
      int num3;
      if ((num1 | num2) != 0)
      {
        Decimal? nullable = origLine.ReceiptQty;
        Decimal num4 = 0M;
        if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
        {
          nullable = origLine.BaseReceiptQty;
          Decimal num5 = 0M;
          num3 = !(nullable.GetValueOrDefault() == num5 & nullable.HasValue) ? 1 : 0;
        }
        else
          num3 = 1;
      }
      else
        num3 = 0;
      bool flag2 = num3 != 0;
      bool preserveExistingPlan = false;
      INItemPlan plan = (INItemPlan) null;
      bool? nullable1;
      if (!isCancellation)
      {
        plan = PXCache<INItemPlan>.CreateCopy(((PXResult) pxResult).GetItem<INItemPlan>());
        INPlanType inPlanType = INPlanType.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, plan.PlanType);
        if ((object) inPlanType == null)
          inPlanType = new INPlanType();
        INPlanType plantype = inPlanType;
        int num6;
        if (flag2 && !string.IsNullOrEmpty(plantype.PlanType))
        {
          nullable1 = plantype.DeleteOnEvent;
          num6 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        else
          num6 = 0;
        preserveExistingPlan = num6 != 0;
        ((PXGraphExtension<POReceiptEntry>) this).Base.ProcessPlanOnRelease(((PXSelectBase) this.ReverseSplits).Cache, split, plan, plantype, preserveExistingPlan);
      }
      int num7 = !((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.ObjectsEqual((object) poReceiptLine, (object) line) ? 1 : 0;
      int? inventoryId1 = split.InventoryID;
      int num8;
      if (inventoryId1.HasValue)
      {
        inventoryId1 = line.InventoryID;
        int? inventoryId2 = split.InventoryID;
        num8 = !(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue) ? 1 : 0;
      }
      else
        num8 = 0;
      bool flag3 = num8 != 0;
      int num9 = flag3 ? 1 : 0;
      if ((num7 | num9) != 0)
      {
        if (flag2)
        {
          INTran inTran2 = new INTran();
          inTran2.BranchID = line.BranchID;
          inTran2.TranType = "III";
          inTran2.POReceiptType = line.ReceiptType;
          inTran2.POReceiptNbr = line.ReceiptNbr;
          inTran2.POReceiptLineNbr = line.LineNbr;
          inTran2.POLineType = line.LineType;
          inTran2.OrigRefNbr = inRegisterEntry.Value.INRegisterDataMember.Current.OrigReceiptNbr;
          inTran2.AcctID = origLine.POAccrualAcctID;
          inTran2.SubID = origLine.POAccrualSubID;
          inTran2.ReclassificationProhibited = new bool?(true);
          inTran2.InvtAcctID = flag1 ? origLine.ExpenseAcctID : lazy.Value?.InvtAcctID;
          inTran2.InvtSubID = flag1 ? origLine.ExpenseSubID : lazy.Value?.InvtSubID;
          inTran2.OrigPlanType = (string) null;
          bool? nullable2;
          if (!flag3 || !POLineType.IsStockNonDropShip(line.LineType))
          {
            nullable2 = origLine.IsStockItem;
          }
          else
          {
            nullable1 = new bool?();
            nullable2 = nullable1;
          }
          inTran2.IsStockItem = nullable2;
          inTran2.InventoryID = !flag3 || !POLineType.IsStockNonDropShip(line.LineType) ? origLine.InventoryID : split.InventoryID;
          inTran2.SiteID = origLine.SiteID;
          inTran2.SubItemID = !flag3 || POLineType.IsNonStockNonServiceNonDropShip(line.LineType) ? origLine.SubItemID : split.SubItemID;
          inTran2.LocationID = !flag3 || POLineType.IsNonStockNonServiceNonDropShip(line.LineType) ? origLine.LocationID : split.LocationID;
          inTran2.UOM = !flag3 || POLineType.IsNonStockNonServiceNonDropShip(line.LineType) ? origLine.UOM : split.UOM;
          inTran2.UnitCost = !flag3 || POLineType.IsNonStockNonServiceNonDropShip(line.LineType) ? origLine.UnitCost : new Decimal?(0M);
          inTran2.TranDesc = !flag3 || POLineType.IsNonStockNonServiceNonDropShip(line.LineType) ? line.TranDesc : (string) null;
          inTran2.ReasonCode = line.ReasonCode;
          inTran2.InvtMult = INTranType.InvtMult("III");
          inTran2.Qty = POLineType.IsStockNonDropShip(line.LineType) ? new Decimal?(0M) : origLine.Qty;
          inTran2.AccrueCost = line.AccrueCost;
          inTran2.ProjectID = origLine.ProjectID;
          inTran2.TaskID = origLine.TaskID;
          inTran2.CostCodeID = origLine.CostCodeID;
          inTran2.IsIntercompany = line.IsIntercompany;
          inTran2.ExactCost = new bool?(true);
          INTran tran = inTran2;
          inRegisterEntry.Value.CostCenterDispatcherExt?.SetInventorySource(tran);
          try
          {
            inTran1 = inRegisterEntry.Value.LSSelectDataMember.Insert(tran);
          }
          catch (PXException ex)
          {
            throw new ErrorProcessingEntityException(((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Caches[((object) line).GetType()], (IBqlTable) line, ex);
          }
        }
        else
          inTran1 = (INTran) null;
      }
      poReceiptLine = line;
      if (inTran1 != null && !string.IsNullOrEmpty(split.ReceiptNbr))
      {
        nullable1 = line.IsStockItem;
        if (nullable1.GetValueOrDefault())
        {
          INTranSplit inTranSplit1 = INTranSplit.FromINTran(inTran1);
          inTranSplit1.SplitLineNbr = new int?();
          inTranSplit1.SubItemID = split.SubItemID;
          inTranSplit1.LocationID = split.LocationID;
          inTranSplit1.LotSerialNbr = split.LotSerialNbr;
          inTranSplit1.InventoryID = split.InventoryID;
          inTranSplit1.UOM = split.UOM;
          inTranSplit1.Qty = split.Qty;
          inTranSplit1.ExpireDate = split.ExpireDate;
          inTranSplit1.POLineType = line.LineType;
          inTranSplit1.OrigPlanType = (string) null;
          if (preserveExistingPlan)
          {
            inTranSplit1.PlanID = plan.PlanID;
            plan.OrigPlanID = new long?();
            plan.OrigPlanType = (string) null;
            plan.OrigNoteID = new Guid?();
            plan.OrigPlanLevel = new int?();
            plan.IgnoreOrigPlan = new bool?(false);
            plan.Reverse = new bool?(false);
            plan.RefNoteID = inRegisterEntry.Value.INRegisterDataMember.Current.NoteID;
            plan.RefEntityType = typeof (PX.Objects.IN.INRegister).FullName;
            ((PXGraph) inRegisterEntry.Value).Caches[typeof (INItemPlan)].Update((object) plan);
          }
          try
          {
            INTranSplit inTranSplit2 = inRegisterEntry.Value.INTranSplitDataMember.Insert(inTranSplit1);
            ((PXSelectBase) inRegisterEntry.Value.INTranSplitDataMember).Cache.RaiseRowUpdated((object) inTranSplit2, (object) inTranSplit2);
            goto label_37;
          }
          catch (PXException ex)
          {
            throw new ErrorProcessingEntityException(((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Caches[((object) line).GetType()], (IBqlTable) line, ex);
          }
        }
      }
      if (preserveExistingPlan)
        throw new PXInvalidOperationException();
label_37:
      if (inTran1 != null)
      {
        int? inventoryId3 = inTran1.InventoryID;
        inventoryId1 = line.InventoryID;
        if (inventoryId3.GetValueOrDefault() == inventoryId1.GetValueOrDefault() & inventoryId3.HasValue == inventoryId1.HasValue)
          inTran1.TranCost = origLine.TranCostFinal;
      }
    }
  }

  protected virtual void OnBeforeAddingReversalLine(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POReceiptLine origLine,
    IItemPlanPOReceiptSource split)
  {
  }

  /// overrides <see cref="M:PX.Objects.PO.POReceiptEntry.GetOriginalTran(PX.Objects.PO.POReceiptLine)" />
  [PXOverride]
  public INTran GetOriginalTran(
    PX.Objects.PO.POReceiptLine line,
    Func<PX.Objects.PO.POReceiptLine, INTran> base_GetOriginalTran)
  {
    if (line.Released.GetValueOrDefault())
    {
      INTran originalTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.docType, In3<INDocType.receipt, INDocType.issue>>>>, And<BqlOperand<INTran.tranType, IBqlString>.IsEqual<INTranType.receipt>>>, And<BqlOperand<INTran.pOReceiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.FromCurrent>>>, And<BqlOperand<INTran.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<INTran.pOReceiptLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
      {
        line
      }, Array.Empty<object>()));
      if (originalTran != null)
        return originalTran;
    }
    for (; line != null; line = PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, line.OrigReceiptType, line.OrigReceiptNbr, line.OrigReceiptLineNbr))
    {
      INTran originalTran = base_GetOriginalTran(line);
      if (originalTran != null)
        return originalTran;
    }
    return (INTran) null;
  }

  /// overrides <see cref="M:PX.Objects.PO.POReceiptEntry.HandleINReleaseException(System.Collections.Generic.List{PX.Objects.IN.INRegister},System.String,PX.Data.PXException)" />
  [PXOverride]
  public void HandleINReleaseException(
    List<PX.Objects.IN.INRegister> forReleaseIN,
    string defaultErrorMessage,
    PXException ex,
    Action<List<PX.Objects.IN.INRegister>, string, PXException> base_HandleINReleaseException)
  {
    if (forReleaseIN[0].IsCorrection.GetValueOrDefault())
    {
      UpdateQtyCostStatusImbalanceException qtyCostExc = ((Exception) ex).InnerException as UpdateQtyCostStatusImbalanceException;
      if (qtyCostExc != null)
      {
        PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current;
        using (IEnumerator<PX.Objects.PO.POReceiptLine> enumerator = ((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.Cached.Cast<PX.Objects.PO.POReceiptLine>().Where<PX.Objects.PO.POReceiptLine>((Func<PX.Objects.PO.POReceiptLine, bool>) (line =>
        {
          if (EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache.GetStatus((object) line), (PXEntryStatus) 3, (PXEntryStatus) 4) && line.ReceiptType == current.ReceiptType && line.ReceiptNbr == current.ReceiptNbr)
          {
            int? inventoryId1 = line.InventoryID;
            int? inventoryId2 = qtyCostExc.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = line.SubItemID;
              int? subItemId2 = qtyCostExc.SubItemID;
              return subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue;
            }
          }
          return false;
        })).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            INTran originalTran = ((PXGraphExtension<POReceiptEntry>) this).Base.GetOriginalTran(enumerator.Current);
            int? siteId1 = (int?) originalTran?.SiteID;
            int? siteId2 = qtyCostExc.SiteID;
            if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
              throw new PXException((Exception) ex, "The {0} item cannot be issued from the {1} warehouse with the {2} unit cost in the original purchase receipt.", new object[3]
              {
                PXForeignSelectorAttribute.GetValueExt<INTran.inventoryID>((PXCache) GraphHelper.Caches<INTran>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), (object) originalTran),
                PXForeignSelectorAttribute.GetValueExt<INTran.siteID>((PXCache) GraphHelper.Caches<INTran>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), (object) originalTran),
                (object) originalTran.UnitCost.Value.ToString("0.######")
              });
          }
          goto label_12;
        }
      }
    }
    if (forReleaseIN[0].IsCorrection.GetValueOrDefault() && ((Exception) ex).InnerException is CorrectionOversoldException)
      throw ex;
label_12:
    base_HandleINReleaseException(forReleaseIN, defaultErrorMessage, ex);
  }
}
