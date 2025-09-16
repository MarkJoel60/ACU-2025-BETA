// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIReview
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.PhysicalInventory;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN;

public class INPIReview : INPIEntry
{
  public 
  #nullable disable
  PXAction<INPIHeader> actionsFolder;
  public PXAction<INPIHeader> setNotEnteredToZero;
  public PXAction<INPIHeader> setNotEnteredToSkipped;
  public PXAction<INPIHeader> updateCost;
  public PXAction<INPIHeader> completePI;
  public PXAction<INPIHeader> finishCounting;
  public PXAction<INPIHeader> cancelPI;
  public PXFilter<PXImportAttribute.CSVSettings> cSVSettings;
  public PXFilter<PXImportAttribute.XLSXSettings> xLSXSettings;

  public virtual void Configure(PXScreenConfiguration config)
  {
    INPIReview.Configure(config.GetScreenConfigurationContext<INPIReview, INPIHeader>());
  }

  protected static void Configure(WorkflowContext<INPIReview, INPIHeader> context)
  {
    BoundedTo<INPIReview, INPIHeader>.ActionCategory.IConfigured processingCategory = CommonActionCategories.Get<INPIReview, INPIHeader>(context).Processing;
    context.AddScreenConfigurationFor((Func<BoundedTo<INPIReview, INPIHeader>.ScreenConfiguration.IStartConfigScreen, BoundedTo<INPIReview, INPIHeader>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<INPIReview, INPIHeader>.ScreenConfiguration.IConfigured) ((BoundedTo<INPIReview, INPIHeader>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<INPIReview, PXAction<INPIHeader>>>) (g => g.finishCounting), (Func<BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured>) (a => (BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<INPIReview, PXAction<INPIHeader>>>) (g => g.updateCost), (Func<BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured>) (a => (BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<INPIReview, PXAction<INPIHeader>>>) (g => g.setNotEnteredToZero), (Func<BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured>) (a => (BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<INPIReview, PXAction<INPIHeader>>>) (g => g.setNotEnteredToSkipped), (Func<BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured>) (a => (BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<INPIReview, PXAction<INPIHeader>>>) (g => g.cancelPI), (Func<BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured>) (a => (BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<INPIReview, PXAction<INPIHeader>>>) (g => g.completePI), (Func<BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured>) (a => (BoundedTo<INPIReview, INPIHeader>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
    })).WithCategories((Action<BoundedTo<INPIReview, INPIHeader>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable ActionsFolder(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField(DisplayName = "Set Not Entered To Zero")]
  protected virtual IEnumerable SetNotEnteredToZero(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INPIReview.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new INPIReview.\u003C\u003Ec__DisplayClass5_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.header = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass50.header == null || !this.IsSetNotEnteredToZeroAllowed(cDisplayClass50.header))
      return adapter.Get();
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass50, __methodptr(\u003CSetNotEnteredToZero\u003Eb__0)));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Set Not Entered To Skipped")]
  protected virtual IEnumerable SetNotEnteredToSkipped(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INPIReview.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new INPIReview.\u003C\u003Ec__DisplayClass7_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.header = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass70.header == null || !this.IsSetNotEnteredToSkippedAllowed(cDisplayClass70.header))
      return adapter.Get();
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass70, __methodptr(\u003CSetNotEnteredToSkipped\u003Eb__0)));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Update Actual Cost")]
  protected virtual IEnumerable UpdateCost(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INPIReview.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new INPIReview.\u003C\u003Ec__DisplayClass9_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.header = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass90.header == null || !this.IsUpdateCostAllowed(cDisplayClass90.header))
      return adapter.Get();
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass90, __methodptr(\u003CUpdateCost\u003Eb__0)));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable CompletePI(PXAdapter adapter)
  {
    INPIReview inpiReview = this;
    foreach (INPIHeader h in GraphHelper.RowCast<INPIHeader>(adapter.Get()))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      INPIReview.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new INPIReview.\u003C\u003Ec__DisplayClass11_0();
      ((PXSelectBase<INPIHeader>) inpiReview.PIHeader).Current = h;
      if (!inpiReview.IsCompletePIAllowed(h))
        throw new PXException("Document Status is invalid for processing.");
      bool flag = false;
      foreach (PXResult<INPIDetail> pxResult in ((PXSelectBase<INPIDetail>) inpiReview.PIDetail).Select(Array.Empty<object>()))
      {
        INPIDetail inpiDetail = PXResult<INPIDetail>.op_Implicit(pxResult);
        if (inpiDetail.Status == "N" && inpiDetail.InventoryID.HasValue)
        {
          ((PXSelectBase) inpiReview.PIDetail).Cache.RaiseExceptionHandling<INPIDetail.lineNbr>((object) inpiDetail, (object) inpiDetail.LineNbr, (Exception) new PXSetPropertyException("Line data should be entered.", (PXErrorLevel) 5));
          flag = true;
        }
      }
      if (flag)
        throw new PXException("The PI count cannot be completed because one or more lines have not been entered.");
      ((PXAction) inpiReview.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass110.h = h;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) inpiReview, new PXToggleAsyncDelegate((object) cDisplayClass110, __methodptr(\u003CCompletePI\u003Eb__0)));
      yield return (object) h;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable FinishCounting(PXAdapter adapter)
  {
    INPIHeader current = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    if (current == null || !this.IsFinishCountingAllowed(current))
      return adapter.Get();
    current.Status = "E";
    ((PXSelectBase<INPIHeader>) this.PIHeader).Update(current);
    this.RecalcDemandCost();
    this.RecalcTotals();
    INPIClass inpiClass = INPIClass.PK.Find((PXGraph) this, current.PIClassID);
    if (inpiClass != null && inpiClass.UnlockSiteOnCountingFinish.GetValueOrDefault())
      this.CreatePILocksManager().UnlockInventory(false);
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable CancelPI(PXAdapter adapter)
  {
    INPIHeader current = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    if (current == null || !this.IsCancelPIAllowed(current))
      return adapter.Get();
    current.Status = "X";
    ((PXSelectBase<INPIHeader>) this.PIHeader).Update(current);
    this.CreatePILocksManager().UnlockInventory();
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual void CSVSettings_Mode_CacheAttached(PXCache sender)
  {
  }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual void XLSXSettings_Mode_CacheAttached(PXCache sender)
  {
  }

  public virtual void FinishEntering(INPIHeader p_h)
  {
    PX.Objects.IN.INSetup inSetup = PXResultset<PX.Objects.IN.INSetup>.op_Implicit(((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Select(Array.Empty<object>()));
    INPIHeader inpiHeader = ((PXSelectBase<INPIHeader>) this.PIHeader).Current = PXResultset<INPIHeader>.op_Implicit(((PXSelectBase<INPIHeader>) this.PIHeader).Search<INPIHeader.pIID>((object) p_h.PIID, Array.Empty<object>()));
    if (inpiHeader == null || inSetup == null || ((PXSelectBase<INSite>) this.insite).Current == null)
      return;
    if (!this.IsCompletePIAllowed(inpiHeader))
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.completePI).GetCaption(),
        (object) ((PXSelectBase) this.PIHeader).Cache.GetRowDescription((object) inpiHeader)
      });
    this.VerifyBookQty(inpiHeader);
    List<INPIEntry.ProjectedTranRec> projectedTranRecList = this.RecalcDemandCost(true);
    bool flag = true;
    INRegister current;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      INAdjustmentEntry adjustmentEntry = this.CreateAdjustmentEntry();
      ((PXSelectBase<PX.Objects.IN.INSetup>) adjustmentEntry.insetup).Current.RequireControlTotal = new bool?(false);
      ((PXSelectBase<PX.Objects.IN.INSetup>) adjustmentEntry.insetup).Current.HoldEntry = new bool?(false);
      ((PXSelectBase) adjustmentEntry.adjustment).Cache.Insert((object) new INRegister()
      {
        BranchID = ((PXSelectBase<INSite>) this.insite).Current.BranchID,
        OrigModule = "PI",
        PIID = inpiHeader.PIID
      });
      int num1 = 0;
      List<PXException> pxExceptionList = new List<PXException>();
      foreach (INPIEntry.ProjectedTranRec projectedTran in projectedTranRecList)
      {
        INLotSerClass inLotSerClass = PXResultset<INLotSerClass>.op_Implicit(PXSelectBase<INLotSerClass, PXViewOf<INLotSerClass>.BasedOn<SelectFromBase<INLotSerClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<InventoryItem>.On<InventoryItem.FK.LotSerialClass>>>.Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) projectedTran.InventoryID
        }));
        if (inLotSerClass != null && inLotSerClass.LotSerTrack == "S" && inLotSerClass.LotSerAssign == "R")
        {
          INSiteLotSerial inSiteLotSerial = PXResultset<INSiteLotSerial>.op_Implicit(PXSelectBase<INSiteLotSerial, PXViewOf<INSiteLotSerial>.BasedOn<SelectFromBase<INSiteLotSerial, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.inventoryID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.lotSerialNbr, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.siteID, NotEqual<P.AsInt>>>>>.And<BqlOperand<INSiteLotSerial.qtyOnHand, IBqlDecimal>.IsEqual<decimal1>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) projectedTran.InventoryID,
            (object) projectedTran.LotSerialNbr,
            (object) p_h.SiteID
          }));
          if (inSiteLotSerial != null)
          {
            InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, inSiteLotSerial.InventoryID);
            INSite inSite = INSite.PK.Find((PXGraph) this, inSiteLotSerial.SiteID);
            PXTrace.WriteError("The {0} item with the {1} serial number has already been received in {2}.", new object[3]
            {
              (object) inventoryItem.InventoryCD,
              (object) inSiteLotSerial.LotSerialNbr,
              (object) inSite.SiteCD
            });
            ++num1;
          }
          else
          {
            Sign sign = projectedTran.VarQtyPortion >= 0M ? Sign.Plus : Sign.Minus;
            int num2 = (int) Math.Abs(projectedTran.VarQtyPortion);
            projectedTran.VarCostPortion /= (Decimal) num2;
            projectedTran.VarQtyPortion = (Decimal) Sign.op_Multiply(sign, 1);
            for (; num2 > 0; --num2)
              this.SilentProduceAdjustment(adjustmentEntry, inpiHeader, projectedTran, (IList<PXException>) pxExceptionList);
          }
        }
        else
          this.SilentProduceAdjustment(adjustmentEntry, inpiHeader, projectedTran, (IList<PXException>) pxExceptionList);
      }
      this.HandleAdjustmentExceptions(adjustmentEntry, pxExceptionList);
      if (num1 > 0)
      {
        PXException pxException;
        if (num1 != 1)
          pxException = new PXException("{0} items with serial numbers have already been received. For details, see the trace log: Click Tools > Trace on the form title bar.", new object[1]
          {
            (object) num1
          });
        else
          pxException = new PXException("One item with a serial number has already been received. For details, see the trace log: Click Tools > Trace on the form title bar.");
        throw pxException;
      }
      flag = ((PXSelectBase) adjustmentEntry.transactions).Cache.Inserted.Cast<INTran>().All<INTran>((Func<INTran, bool>) (t => t.CostLayerType == "N"));
      ((PXAction) adjustmentEntry.Save).Press();
      inpiHeader.PIAdjRefNbr = ((PXSelectBase<INRegister>) adjustmentEntry.adjustment).Current.RefNbr;
      current = ((PXSelectBase<INRegister>) adjustmentEntry.adjustment).Current;
      ((PXSelectBase<INPIHeader>) this.PIHeader).Current = inpiHeader;
      inpiHeader.Status = "R";
      this.RecalcTotals();
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
    if (!((PXSelectBase<PX.Objects.IN.INSetup>) this.Setup).Current.AutoReleasePIAdjustment.GetValueOrDefault() || current == null)
      return;
    if (!flag)
      throw new PXException("The PI adjustment has been created with the Balanced status. Review and release the adjustment.");
    INDocumentRelease.ReleaseDoc(new List<INRegister>()
    {
      current
    }, false);
  }

  protected virtual INAdjustmentEntry CreateAdjustmentEntry()
  {
    return PXGraph.CreateInstance<INAdjustmentEntry>();
  }

  protected virtual void HandleAdjustmentExceptions(
    INAdjustmentEntry je,
    List<PXException> exceptions)
  {
    if (exceptions.Count > 0)
      throw exceptions.First<PXException>();
  }

  private void SilentProduceAdjustment(
    INAdjustmentEntry adjustmentGraph,
    INPIHeader header,
    INPIEntry.ProjectedTranRec projectedTran,
    IList<PXException> exceptionsList)
  {
    try
    {
      this.ProduceAdjustment(adjustmentGraph, header, projectedTran);
    }
    catch (PXException ex)
    {
      exceptionsList.Add(ex);
    }
  }

  protected virtual void ProduceAdjustment(
    INAdjustmentEntry adjustmentGraph,
    INPIHeader header,
    INPIEntry.ProjectedTranRec projectedTran)
  {
    if (projectedTran.AdjNotReceipt)
    {
      INTran tran = new INTran();
      tran.BranchID = ((PXSelectBase<INSite>) this.insite).Current.BranchID;
      tran.TranType = "ADJ";
      tran.PIID = header.PIID;
      tran.PILineNbr = new int?(projectedTran.LineNbr);
      tran.InvtAcctID = projectedTran.AcctID;
      tran.InvtSubID = projectedTran.SubID;
      tran.AcctID = new int?();
      tran.SubID = new int?();
      if (projectedTran.ProjectID.HasValue && projectedTran.TaskID.HasValue)
      {
        tran.ProjectID = projectedTran.ProjectID;
        tran.TaskID = projectedTran.TaskID;
        tran.CostCenterID = projectedTran.CostCenterID;
      }
      tran.CostCodeID = CostCodeAttribute.DefaultCostCode;
      tran.InventoryID = projectedTran.InventoryID;
      tran.SubItemID = projectedTran.SubItemID;
      tran.SiteID = header.SiteID;
      tran.LocationID = projectedTran.LocationID;
      tran.ManualCost = projectedTran.ManualCost;
      tran.UnitCost = projectedTran.UnitCost;
      tran.Qty = new Decimal?(projectedTran.VarQtyPortion);
      tran.TranCost = new Decimal?(projectedTran.VarCostPortion);
      tran.ReasonCode = projectedTran.ReasonCode;
      tran.IsSpecialOrder = new bool?(projectedTran.IsSpecialOrder);
      tran.SOOrderType = projectedTran.SOOrderType;
      tran.SOOrderNbr = projectedTran.SOOrderNbr;
      tran.SOOrderLineNbr = projectedTran.SOOrderLineNbr;
      adjustmentGraph.CostCenterDispatcherExt?.SetInventorySource(tran);
      INTran copy = PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) adjustmentGraph.transactions).Insert(tran));
      copy.OrigRefNbr = projectedTran.OrigRefNbr;
      copy.LotSerialNbr = projectedTran.LotSerialNbr;
      copy.ExpireDate = projectedTran.ExpireDate;
      PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) adjustmentGraph.transactions).Update(copy));
    }
    else
    {
      INTran copy1 = PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) adjustmentGraph.transactions).Insert(new INTran()
      {
        BranchID = ((PXSelectBase<INSite>) this.insite).Current.BranchID,
        TranType = "ADJ",
        PIID = header.PIID,
        PILineNbr = new int?(projectedTran.LineNbr)
      }));
      copy1.InvtAcctID = projectedTran.AcctID;
      copy1.InvtSubID = projectedTran.SubID;
      copy1.AcctID = new int?();
      copy1.SubID = new int?();
      if (projectedTran.ProjectID.HasValue && projectedTran.TaskID.HasValue)
      {
        copy1.ProjectID = projectedTran.ProjectID;
        copy1.TaskID = projectedTran.TaskID;
        copy1.CostCenterID = projectedTran.CostCenterID;
      }
      copy1.CostCodeID = CostCodeAttribute.DefaultCostCode;
      copy1.IsStockItem = new bool?(true);
      copy1.InventoryID = projectedTran.InventoryID;
      copy1.SubItemID = projectedTran.SubItemID;
      copy1.SiteID = header.SiteID;
      copy1.LocationID = projectedTran.LocationID;
      copy1.UOM = projectedTran.UOM;
      copy1.ManualCost = projectedTran.ManualCost;
      copy1.UnitCost = projectedTran.UnitCost;
      copy1.Qty = new Decimal?(projectedTran.VarQtyPortion);
      copy1.TranCost = new Decimal?(projectedTran.VarCostPortion);
      copy1.ReasonCode = projectedTran.ReasonCode;
      copy1.IsSpecialOrder = new bool?(projectedTran.IsSpecialOrder);
      copy1.SOOrderType = projectedTran.SOOrderType;
      copy1.SOOrderNbr = projectedTran.SOOrderNbr;
      copy1.SOOrderLineNbr = projectedTran.SOOrderLineNbr;
      adjustmentGraph.CostCenterDispatcherExt?.SetInventorySource(copy1);
      INTran copy2 = PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) adjustmentGraph.transactions).Update(copy1));
      copy2.LotSerialNbr = projectedTran.LotSerialNbr;
      copy2.ExpireDate = projectedTran.ExpireDate;
      PXCache<INTran>.CreateCopy(((PXSelectBase<INTran>) adjustmentGraph.transactions).Update(copy2));
    }
  }

  protected virtual void VerifyBookQty(INPIHeader header)
  {
    PXResultset<INPIDetail> pxResultset = PXSelectBase<INPIDetail, PXViewOf<INPIDetail>.BasedOn<SelectFromBase<INPIDetail, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocationStatus>.On<INPIDetail.FK.LocationStatus>>, FbqlJoins.Left<INLotSerialStatus>.On<INPIDetail.FK.LotSerialStatus>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.pIID, Equal<P.AsString>>>>>.And<BqlOperand<INPIDetail.varQty, IBqlDecimal>.IsLess<decimal0>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) header.PIID
    });
    int num1 = 0;
    PXException pxException = (PXException) null;
    foreach (PXResult<INPIDetail, INLocationStatus, INLotSerialStatus> pxResult in pxResultset)
    {
      INPIDetail inpiDetail = PXResult<INPIDetail, INLocationStatus, INLotSerialStatus>.op_Implicit(pxResult);
      INLotSerialStatus inLotSerialStatus = PXResult<INPIDetail, INLocationStatus, INLotSerialStatus>.op_Implicit(pxResult);
      Decimal bookQty = (inLotSerialStatus.LotSerialNbr != null ? (IStatus) inLotSerialStatus : (IStatus) ((PXResult) pxResult).GetItem<INLocationStatus>()).GetBookQty();
      Decimal? nullable1;
      ref Decimal? local = ref nullable1;
      Decimal num2 = bookQty;
      Decimal? nullable2 = inpiDetail.VarQty;
      Decimal valueOrDefault = nullable2.GetValueOrDefault();
      Decimal num3 = num2 + valueOrDefault;
      local = new Decimal?(num3);
      nullable2 = nullable1;
      Decimal num4 = 0M;
      if (nullable2.GetValueOrDefault() < num4 & nullable2.HasValue)
      {
        InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, inpiDetail.InventoryID);
        INLocation inLocation = INLocation.PK.Find((PXGraph) this, inpiDetail.LocationID);
        if (!string.IsNullOrEmpty(inpiDetail.LotSerialNbr))
        {
          object[] objArray = new object[5]
          {
            (object) inventoryItem.InventoryCD.TrimEnd(),
            (object) inLocation.LocationCD.TrimEnd(),
            null,
            null,
            null
          };
          nullable2 = nullable1;
          objArray[2] = (object) (nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?()).ToFormattedString();
          objArray[3] = (object) inventoryItem.BaseUnit;
          objArray[4] = (object) inpiDetail.LotSerialNbr;
          pxException = (PXException) new PXOperationCompletedWithErrorException("The book quantity of the {0} item with the {4} lot/serial number in the {1} location will become negative with the current physical quantity due to inventory transactions generated after the finished counting. Increase the physical quantity of the item by {2} {3}. For details, see the Inventory Transactions History (IN405000) report.", objArray);
        }
        else
        {
          object[] objArray = new object[4]
          {
            (object) inventoryItem.InventoryCD.TrimEnd(),
            (object) inLocation.LocationCD.TrimEnd(),
            null,
            null
          };
          nullable2 = nullable1;
          objArray[2] = (object) (nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?()).ToFormattedString();
          objArray[3] = (object) inventoryItem.BaseUnit;
          pxException = (PXException) new PXOperationCompletedWithErrorException("The book quantity of the {0} item in the {1} location will become negative with the current physical quantity due to inventory transactions generated after the finished counting. Increase the physical quantity of the item by {2} {3}. For details, see the Inventory Transactions History (IN405000) report.", objArray);
        }
        ++num1;
        PXTrace.WriteError((Exception) pxException);
      }
    }
    if (num1 > 0)
      throw num1 == 1 ? (object) pxException : (object) new PXOperationCompletedWithErrorException("The book quantity was decreased for several items. For details, see the trace log: Click Tools > Trace on the form title bar.");
  }

  protected override void _(PX.Data.Events.RowSelected<INPIHeader> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    ((PXAction) this.cancelPI).SetEnabled(this.IsCancelPIAllowed(e.Row));
    ((PXAction) this.finishCounting).SetEnabled(this.IsFinishCountingAllowed(e.Row));
    ((PXAction) this.updateCost).SetEnabled(this.IsUpdateCostAllowed(e.Row));
    ((PXAction) this.setNotEnteredToZero).SetEnabled(this.IsSetNotEnteredToZeroAllowed(e.Row));
    ((PXAction) this.setNotEnteredToSkipped).SetEnabled(this.IsSetNotEnteredToSkippedAllowed(e.Row));
    ((PXAction) this.completePI).SetEnabled(this.IsCompletePIAllowed(e.Row));
  }

  protected virtual bool IsCancelPIAllowed(INPIHeader h)
  {
    return EnumerableExtensions.IsIn<string>(h.Status, "N", "E");
  }

  protected virtual bool IsFinishCountingAllowed(INPIHeader h) => h.Status == "N";

  protected virtual bool IsUpdateCostAllowed(INPIHeader h) => h.Status == "E";

  protected virtual bool IsSetNotEnteredToZeroAllowed(INPIHeader h) => h.Status == "E";

  protected virtual bool IsSetNotEnteredToSkippedAllowed(INPIHeader h) => h.Status == "E";

  protected virtual bool IsCompletePIAllowed(INPIHeader h) => h.Status == "E";
}
