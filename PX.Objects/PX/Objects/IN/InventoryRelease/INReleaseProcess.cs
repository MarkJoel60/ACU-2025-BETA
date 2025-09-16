// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.INReleaseProcess
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
using PX.Objects.CM;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.GraphExtensions.INReleaseProcessExt;
using PX.Objects.IN.InventoryRelease.Accumulators;
using PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses;
using PX.Objects.IN.InventoryRelease.Accumulators.Documents;
using PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.InventoryRelease.Accumulators.Statistics.ItemCustomer;
using PX.Objects.IN.InventoryRelease.DAC;
using PX.Objects.IN.InventoryRelease.Exceptions;
using PX.Objects.IN.InventoryRelease.Utility;
using PX.Objects.IN.PhysicalInventory;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.PO.LandedCosts;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SO2PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable disable
namespace PX.Objects.IN.InventoryRelease;

public class INReleaseProcess : PXGraph<INReleaseProcess>
{
  public PXSelect<INCostSubItemXRef> costsubitemxref;
  public PXSelect<INItemSite> initemsite;
  public PXSelect<OversoldCostStatus> oversoldcoststatus;
  public PXSelect<UnmanagedCostStatus> unmanagedcoststatus;
  public PXSelect<FIFOCostStatus> fifocoststatus;
  public PXSelect<AverageCostStatus> averagecoststatus;
  public PXSelect<StandardCostStatus> standardcoststatus;
  public PXSelect<SpecificCostStatus> specificcoststatus;
  public PXSelect<SpecificTransitCostStatus> specifictransitcoststatus;
  public PXSelect<ReceiptStatus> receiptstatus;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter> lotnumberedstatusbycostcenter;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial> itemlotserial;
  public PXSelect<SiteLotSerial> sitelotserial;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter> locationstatusbycostcenter;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> sitestatusbycostcenter;
  public PXSelect<INTransitLine> intransitline;
  public PXSelect<TransitSiteStatusByCostCenter> transitsitestatusbycostcenter;
  public PXSelect<TransitLocationStatusByCostCenter> transitlocationstatusbycostcenter;
  public PXSelect<TransitLotSerialStatusByCostCenter> transitlotnumberedstatusbycostcenter;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats> itemstats;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost> itemcost;
  public PXSelect<ItemSiteHist> itemsitehist;
  public PXSelect<ItemSiteHistByCostCenterD> itemsitehistbycostcenterd;
  public PXSelect<ItemSiteHistDay> itemsitehistday;
  public PXSelect<ItemCostHist> itemcosthist;
  public PXSelect<ItemSalesHist> itemsaleshist;
  public PXSelect<ItemCustSalesHist> itemcustsaleshist;
  public PXSelect<ItemCustSalesStats> itemcustsalesstats;
  public PXSelect<ItemCustDropShipStats> itemcustdropshipstats;
  public PXSelect<ItemSalesHistD> itemsaleshistd;
  public PXSelect<PX.Objects.IN.INRegister> inregister;
  public PXSelect<INTran> intranselect;
  public PXSelect<INTranSplit> intransplit;
  public PXAccumSelect<INTranCost> intrancost;
  public PXSelect<SOShipLineUpdate> soshiplineupdate;
  public PXSelect<ARTranUpdate> artranupdate;
  public PXSelect<POReceiptLineUpdate> poreceiptlineupdate;
  public PXSelect<INTranUpdate> intranupdate;
  public PXSelect<INTranCostUpdate> intrancostupdate;
  public PXSelect<INTranSplitAdjustmentUpdate> intransplitadjustmentupdate;
  public PXSelect<INTranSplitUpdate> intransplitupdate;
  public PXSelect<PX.Objects.SO.SOLineSplit> solinesplit;
  public PXSelect<PX.Objects.SO.SOOrder> soorder;
  public PXSelect<INItemLotSerial> initemlotserialreadonly;
  public PXSetup<INSetup> insetup;
  public PXSetup<Company> companysetup;
  protected PX.Objects.CS.ReasonCode _ReceiptReasonCode;
  protected PX.Objects.CS.ReasonCode _IssuesReasonCode;
  protected PX.Objects.CS.ReasonCode _AdjustmentReasonCode;
  protected PX.Objects.CS.ReasonCode _AssemblyDisassemblyReasonCode;
  protected PX.Objects.CS.ReasonCode _TransferReasonCode;
  protected PXCache<INTranCost> transfercosts;
  private long _CostStatus_Identity = long.MinValue;
  private bool WIPCalculated;
  private Decimal? WIPVariance = new Decimal?(0M);
  private List<Segment> _SubItemSeg;
  private Dictionary<short?, string> _SubItemSegVal;

  public INTranSplitPlan TranSplitPlanExt => ((PXGraph) this).FindImplementation<INTranSplitPlan>();

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  public bool AutoPost => ((PXSelectBase<INSetup>) this.insetup).Current.AutoPost.Value;

  public bool UpdateGL => ((PXSelectBase<INSetup>) this.insetup).Current.UpdateGL.Value;

  public bool SummPost => ((PXSelectBase<INSetup>) this.insetup).Current.SummPost.Value;

  public PX.Objects.CS.ReasonCode ReceiptReasonCode
  {
    get
    {
      if (this._ReceiptReasonCode == null)
        this._ReceiptReasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, ((PXSelectBase<INSetup>) this.insetup).Current.ReceiptReasonCode);
      return this._ReceiptReasonCode;
    }
  }

  public PX.Objects.CS.ReasonCode IssuesReasonCode
  {
    get
    {
      if (this._IssuesReasonCode == null)
        this._IssuesReasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, ((PXSelectBase<INSetup>) this.insetup).Current.IssuesReasonCode);
      return this._IssuesReasonCode;
    }
  }

  public PX.Objects.CS.ReasonCode AdjustmentReasonCode
  {
    get
    {
      if (this._AdjustmentReasonCode == null)
        this._AdjustmentReasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, ((PXSelectBase<INSetup>) this.insetup).Current.AdjustmentReasonCode);
      return this._AdjustmentReasonCode;
    }
  }

  public PX.Objects.CS.ReasonCode AssemblyDisassemblyReasonCode
  {
    get
    {
      if (this._AssemblyDisassemblyReasonCode == null)
        this._AssemblyDisassemblyReasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, ((PXSelectBase<INSetup>) this.insetup).Current.AssemblyDisassemblyReasonCode);
      return this._AssemblyDisassemblyReasonCode;
    }
  }

  public PX.Objects.CS.ReasonCode TransferReasonCode
  {
    get
    {
      return this._TransferReasonCode ?? (this._TransferReasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, ((PXSelectBase<INSetup>) this.insetup).Current.TransferReasonCode));
    }
  }

  public int? ARClearingAcctID => ((PXSelectBase<INSetup>) this.insetup).Current.ARClearingAcctID;

  public int? ARClearingSubID => ((PXSelectBase<INSetup>) this.insetup).Current.ARClearingSubID;

  public int? INTransitSiteID => ((PXSelectBase<INSetup>) this.insetup).Current.TransitSiteID;

  public int? INTransitAcctID => ((PXSelectBase<INSetup>) this.insetup).Current.INTransitAcctID;

  public int? INTransitSubID => ((PXSelectBase<INSetup>) this.insetup).Current.INTransitSubID;

  public int? INProgressAcctID => ((PXSelectBase<INSetup>) this.insetup).Current.INProgressAcctID;

  public int? INProgressSubID => ((PXSelectBase<INSetup>) this.insetup).Current.INProgressSubID;

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    ((PXGraph) this).Clear((PXClearOption) 4);
    if (this.transfercosts != null)
      ((PXCache) this.transfercosts).Clear();
    this.WIPCalculated = false;
    this.WIPVariance = new Decimal?(0M);
  }

  public INReleaseProcess()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    this.transfercosts = (PXCache<INTranCost>) new PXNoEventsCache<INTranCost>((PXGraph) this);
    PXDBDefaultAttribute.SetDefaultForInsert<INTran.docType>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<INTran.refNbr>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<INTran.tranDate>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<INTran.origModule>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<INTranSplit.refNbr>(((PXSelectBase) this.intransplit).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<INTranSplit.tranDate>(((PXSelectBase) this.intransplit).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForInsert<INTranSplit.origModule>(((PXSelectBase) this.intransplit).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTran.docType>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTran.refNbr>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTran.tranDate>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTran.origModule>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXCacheEx.Adjust<FinPeriodIDAttribute>(((PXSelectBase) this.intranselect).Cache, (object) null).For<INTran.finPeriodID>((Action<FinPeriodIDAttribute>) (attr => attr.HeaderFindingMode = FinPeriodIDAttribute.HeaderFindingModes.Parent));
    PXDBDefaultAttribute.SetDefaultForUpdate<INTranSplit.refNbr>(((PXSelectBase) this.intransplit).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTranSplit.tranDate>(((PXSelectBase) this.intransplit).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTranSplit.origModule>(((PXSelectBase) this.intransplit).Cache, (object) null, false);
    OpenPeriodAttribute.SetValidatePeriod<PX.Objects.IN.INRegister.finPeriodID>(((PXSelectBase) this.inregister).Cache, (object) null, PeriodValidation.Nothing);
    this.ParseSubItemSegKeys();
    PXDimensionSelectorAttribute.SetSuppressViewCreation(((PXSelectBase) this.intranselect).Cache);
    PXDimensionSelectorAttribute.SetSuppressViewCreation(((PXSelectBase) this.intrancost).Cache);
    PXFormulaAttribute.SetAggregate<INTran.qty>(((PXSelectBase) this.intranselect).Cache, (Type) null, (Type) null);
    PXFormulaAttribute.SetAggregate<INTran.tranCost>(((PXSelectBase) this.intranselect).Cache, (Type) null, (Type) null);
  }

  public virtual void InitCacheMapping(Dictionary<Type, Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (INCostStatus), typeof (INCostStatus));
  }

  public virtual JournalEntry CreateJournalEntry()
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.GL.GLTran.projectID>(INReleaseProcess.\u003C\u003Ec.\u003C\u003E9__96_0 ?? (INReleaseProcess.\u003C\u003Ec.\u003C\u003E9__96_0 = new PXFieldVerifying((object) INReleaseProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateJournalEntry\u003Eb__96_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PX.Objects.GL.GLTran.taskID>(INReleaseProcess.\u003C\u003Ec.\u003C\u003E9__96_1 ?? (INReleaseProcess.\u003C\u003Ec.\u003C\u003E9__96_1 = new PXFieldVerifying((object) INReleaseProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateJournalEntry\u003Eb__96_1))));
    if (this.UpdateGL)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      INReleaseProcess.\u003C\u003Ec__DisplayClass96_0 cDisplayClass960 = new INReleaseProcess.\u003C\u003Ec__DisplayClass96_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass960.subCache = ((PXGraph) this).Caches[typeof (PX.Objects.GL.Sub)];
      ((PXGraph) this).Caches[typeof (PX.Objects.GL.Sub)] = ((PXGraph) instance).Caches[typeof (PX.Objects.GL.Sub)];
      // ISSUE: method pointer
      ((PXGraph) instance).RowPersisting.AddHandler<PX.Objects.GL.Sub>(new PXRowPersisting((object) cDisplayClass960, __methodptr(\u003CCreateJournalEntry\u003Eb__2)));
      // ISSUE: method pointer
      ((PXGraph) instance).RowPersisted.AddHandler<PX.Objects.GL.Sub>(new PXRowPersisted((object) cDisplayClass960, __methodptr(\u003CCreateJournalEntry\u003Eb__3)));
    }
    return instance;
  }

  public virtual PostGraph CreatePostGraph() => PXGraph.CreateInstance<PostGraph>();

  protected virtual PILocksInspector CreateLocksInspector(int siteID)
  {
    return new PILocksInspector(siteID);
  }

  protected virtual void StandardCostStatus_UnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is StandardCostStatus row))
      return;
    INItemSite inItemSite = INReleaseProcess.SelectItemSite(sender.Graph, row.InventoryID, row.CostSiteID);
    if (inItemSite == null)
      return;
    e.NewValue = (object) inItemSite.StdCost;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void StandardCostStatus_CostID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._CostStatus_Identity++;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void AverageCostStatus_CostID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._CostStatus_Identity++;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FIFOCostStatus_CostID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._CostStatus_Identity++;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SpecificCostStatus_CostID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._CostStatus_Identity++;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void OversoldCostStatus_CostID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._CostStatus_Identity++;
    ((CancelEventArgs) e).Cancel = true;
  }

  [LocationAvail(typeof (INTranSplit.inventoryID), typeof (INTranSplit.subItemID), typeof (INTran.costCenterID), typeof (INTranSplit.siteID), typeof (INTranSplit.tranType), typeof (INTranSplit.invtMult))]
  public virtual void INTranSplit_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  [PXDefault]
  protected virtual void SOLineSplit_OrderNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault]
  protected virtual void SOLineSplit_OrderDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void SOLineSplit_SiteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void SOLineSplit_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXParentAttribute), "UseCurrent", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBBaseCuryAttribute))]
  [PXDBBaseCury(null, null)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.tranCost> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXParentAttribute), "UseCurrent", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranSplit.docType> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (IIf<Where<INTranCost.costType, In3<INTranCost.costType.normal, INTranCost.costType.dropShip>>, INTranCost.qty, decimal0>), typeof (SumCalc<INTran.costedQty>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranCost.qty> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (IIf<Where<INTranCost.costType, In3<INTranCost.costType.normal, INTranCost.costType.dropShip>>, INTranCost.tranCost, decimal0>), typeof (SumCalc<INTran.tranCost>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranCost.tranCost> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (IIf<Where<INTranCost.costType, In3<INTranCost.costType.normal, INTranCost.costType.dropShip>>, INTranCost.tranAmt, decimal0>), typeof (SumCalc<INTran.tranAmt>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranCost.tranAmt> e)
  {
  }

  protected virtual void INTran_UnitCost_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1 || ((INTran) e.Row).OverrideUnitCost.GetValueOrDefault())
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void INTran_UnitPrice_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void INRegister_TotalQty_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void INRegister_TotalAmount_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void INRegister_TotalCost_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1 || !EnumerableExtensions.IsNotIn<string>(((PX.Objects.IN.INRegister) e.Row).DocType, "I", "A"))
      return;
    e.ExcludeFromInsertUpdate();
  }

  public virtual void INItemSite_InvtAcctID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1 || ((INItemSite) e.Row).OverrideInvtAcctSub.GetValueOrDefault())
      return;
    e.ExcludeFromInsertUpdate();
  }

  public virtual void INItemSite_InvtSubID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1 || ((INItemSite) e.Row).OverrideInvtAcctSub.GetValueOrDefault())
      return;
    e.ExcludeFromInsertUpdate();
  }

  public virtual void UpdateSiteStatus(INTran tran, INTranSplit split, INLocation whseloc)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.SiteID = split.SiteID;
    statusByCostCenter1.CostCenterID = tran.CostCenterID;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = statusByCostCenter1;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>((PXGraph) this).Insert(statusByCostCenter2);
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter4 = statusByCostCenter3;
    Decimal? nullable1 = statusByCostCenter4.QtyOnHand;
    Decimal num1 = (Decimal) split.InvtMult.Value * split.BaseQty.Value;
    statusByCostCenter4.QtyOnHand = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter5 = statusByCostCenter3;
    nullable1 = statusByCostCenter5.QtyAvail;
    bool? nullable2 = whseloc.InclQtyAvail;
    Decimal? nullable3;
    Decimal num2;
    if (!nullable2.GetValueOrDefault())
    {
      num2 = 0M;
    }
    else
    {
      Decimal num3 = (Decimal) split.InvtMult.Value;
      nullable3 = split.BaseQty;
      Decimal num4 = nullable3.Value;
      num2 = num3 * num4;
    }
    Decimal num5 = num2;
    Decimal? nullable4;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable4 = nullable3;
    }
    else
      nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num5);
    statusByCostCenter5.QtyAvail = nullable4;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter6 = statusByCostCenter3;
    nullable1 = statusByCostCenter6.QtyHardAvail;
    nullable2 = whseloc.InclQtyAvail;
    Decimal num6;
    if (!nullable2.GetValueOrDefault())
    {
      num6 = 0M;
    }
    else
    {
      Decimal num7 = (Decimal) split.InvtMult.Value;
      nullable3 = split.BaseQty;
      Decimal num8 = nullable3.Value;
      num6 = num7 * num8;
    }
    Decimal num9 = num6;
    Decimal? nullable5;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable5 = nullable3;
    }
    else
      nullable5 = new Decimal?(nullable1.GetValueOrDefault() + num9);
    statusByCostCenter6.QtyHardAvail = nullable5;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter7 = statusByCostCenter3;
    nullable1 = statusByCostCenter7.QtyActual;
    nullable2 = whseloc.InclQtyAvail;
    Decimal num10;
    if (!nullable2.GetValueOrDefault())
    {
      num10 = 0M;
    }
    else
    {
      Decimal num11 = (Decimal) split.InvtMult.Value;
      nullable3 = split.BaseQty;
      Decimal num12 = nullable3.Value;
      num10 = num11 * num12;
    }
    Decimal num13 = num10;
    Decimal? nullable6;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() + num13);
    statusByCostCenter7.QtyActual = nullable6;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter8 = statusByCostCenter3;
    nullable1 = statusByCostCenter8.QtyNotAvail;
    nullable2 = whseloc.InclQtyAvail;
    Decimal num14;
    if (!nullable2.GetValueOrDefault())
    {
      Decimal num15 = (Decimal) split.InvtMult.Value;
      nullable3 = split.BaseQty;
      Decimal num16 = nullable3.Value;
      num14 = num15 * num16;
    }
    else
      num14 = 0M;
    Decimal num17 = num14;
    Decimal? nullable7;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable7 = nullable3;
    }
    else
      nullable7 = new Decimal?(nullable1.GetValueOrDefault() + num17);
    statusByCostCenter8.QtyNotAvail = nullable7;
    statusByCostCenter3.SkipQtyValidation = split.SkipQtyValidation;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter9 = statusByCostCenter3;
    nullable2 = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IgnoreAllocationErrors;
    short? invtMult;
    int num18;
    if (!nullable2.GetValueOrDefault())
    {
      nullable2 = statusByCostCenter3.ValidateHardAvailQtyForAdjustments;
      if (!nullable2.Value)
      {
        if (split.DocType == "A")
        {
          nullable1 = split.BaseQty;
          Decimal num19 = 0M;
          if (nullable1.GetValueOrDefault() > num19 & nullable1.HasValue)
          {
            invtMult = split.InvtMult;
            num18 = (invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1 ? 1 : 0;
            goto label_32;
          }
        }
        num18 = 0;
      }
      else
        num18 = 1;
    }
    else
      num18 = 0;
label_32:
    bool? nullable8 = new bool?(num18 != 0);
    statusByCostCenter9.ValidateHardAvailQtyForAdjustments = nullable8;
    if (!(tran.TranType == "TRX") || this.IsOneStepTransfer())
      return;
    TransitSiteStatusByCostCenter statusByCostCenter10 = new TransitSiteStatusByCostCenter();
    statusByCostCenter10.InventoryID = split.InventoryID;
    statusByCostCenter10.SubItemID = split.SubItemID;
    statusByCostCenter10.SiteID = this.INTransitSiteID;
    statusByCostCenter10.CostCenterID = !this.IsIngoingTransfer(tran) ? tran.ToCostCenterID : tran.CostCenterID;
    TransitSiteStatusByCostCenter statusByCostCenter11 = ((PXSelectBase<TransitSiteStatusByCostCenter>) this.transitsitestatusbycostcenter).Insert(statusByCostCenter10);
    TransitSiteStatusByCostCenter statusByCostCenter12 = statusByCostCenter11;
    nullable1 = statusByCostCenter12.QtyOnHand;
    invtMult = split.InvtMult;
    Decimal num20 = (Decimal) invtMult.Value;
    nullable3 = split.BaseQty;
    Decimal num21 = nullable3.Value;
    Decimal num22 = num20 * num21;
    Decimal? nullable9;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable9 = nullable3;
    }
    else
      nullable9 = new Decimal?(nullable1.GetValueOrDefault() - num22);
    statusByCostCenter12.QtyOnHand = nullable9;
    TransitSiteStatusByCostCenter statusByCostCenter13 = statusByCostCenter11;
    nullable1 = statusByCostCenter13.QtyAvail;
    invtMult = split.InvtMult;
    Decimal num23 = (Decimal) invtMult.Value;
    nullable3 = split.BaseQty;
    Decimal num24 = nullable3.Value;
    Decimal num25 = num23 * num24;
    Decimal? nullable10;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable10 = nullable3;
    }
    else
      nullable10 = new Decimal?(nullable1.GetValueOrDefault() - num25);
    statusByCostCenter13.QtyAvail = nullable10;
    TransitSiteStatusByCostCenter statusByCostCenter14 = statusByCostCenter11;
    nullable1 = statusByCostCenter14.QtyHardAvail;
    invtMult = split.InvtMult;
    Decimal num26 = (Decimal) invtMult.Value;
    nullable3 = split.BaseQty;
    Decimal num27 = nullable3.Value;
    Decimal num28 = num26 * num27;
    Decimal? nullable11;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable11 = nullable3;
    }
    else
      nullable11 = new Decimal?(nullable1.GetValueOrDefault() - num28);
    statusByCostCenter14.QtyHardAvail = nullable11;
    TransitSiteStatusByCostCenter statusByCostCenter15 = statusByCostCenter11;
    nullable1 = statusByCostCenter15.QtyActual;
    invtMult = split.InvtMult;
    Decimal num29 = (Decimal) invtMult.Value;
    nullable3 = split.BaseQty;
    Decimal num30 = nullable3.Value;
    Decimal num31 = num29 * num30;
    Decimal? nullable12;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable12 = nullable3;
    }
    else
      nullable12 = new Decimal?(nullable1.GetValueOrDefault() - num31);
    statusByCostCenter15.QtyActual = nullable12;
  }

  public virtual void UpdateLocationStatus(INTran tran, INTranSplit split)
  {
    short? invtMult1 = split.InvtMult;
    int? nullable = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    nullable = split.SiteID;
    string PIID;
    if (this.CreateLocksInspector(nullable.Value).IsInventoryLocationLocked(split.InventoryID, split.LocationID, ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.PIID, out PIID))
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (INTranSplit)];
      throw new PXException("You cannot change the quantity of the {0} item in the {1} location of the {2} warehouse because this item and location are used in the {3} physical inventory document. To review all locked items, see the Physical Inventory Locked Items (IN409000) inquiry.", new object[4]
      {
        PXForeignSelectorAttribute.GetValueExt<INTranSplit.inventoryID>(cach, (object) split),
        PXForeignSelectorAttribute.GetValueExt<INTranSplit.locationID>(cach, (object) split),
        PXForeignSelectorAttribute.GetValueExt<INTranSplit.siteID>(cach, (object) split),
        (object) PIID
      });
    }
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.SiteID = split.SiteID;
    statusByCostCenter1.LocationID = split.LocationID;
    statusByCostCenter1.CostCenterID = tran.CostCenterID;
    statusByCostCenter1.RelatedPIID = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.PIID;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter2 = statusByCostCenter1;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter3 = GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter>((PXGraph) this).Insert(statusByCostCenter2);
    statusByCostCenter3.NegQty = split.TranType == "ADJ" ? new bool?(false) : statusByCostCenter3.NegQty;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter4 = statusByCostCenter3;
    Decimal? qtyOnHand = statusByCostCenter4.QtyOnHand;
    short? invtMult2 = split.InvtMult;
    Decimal num2 = (Decimal) invtMult2.Value * split.BaseQty.Value;
    statusByCostCenter4.QtyOnHand = qtyOnHand.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() + num2) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter5 = statusByCostCenter3;
    Decimal? qtyAvail = statusByCostCenter5.QtyAvail;
    invtMult2 = split.InvtMult;
    Decimal num3 = (Decimal) invtMult2.Value * split.BaseQty.Value;
    statusByCostCenter5.QtyAvail = qtyAvail.HasValue ? new Decimal?(qtyAvail.GetValueOrDefault() + num3) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter6 = statusByCostCenter3;
    Decimal? qtyHardAvail = statusByCostCenter6.QtyHardAvail;
    invtMult2 = split.InvtMult;
    Decimal num4 = (Decimal) invtMult2.Value * split.BaseQty.Value;
    statusByCostCenter6.QtyHardAvail = qtyHardAvail.HasValue ? new Decimal?(qtyHardAvail.GetValueOrDefault() + num4) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter7 = statusByCostCenter3;
    Decimal? qtyActual = statusByCostCenter7.QtyActual;
    invtMult2 = split.InvtMult;
    Decimal num5 = (Decimal) invtMult2.Value * split.BaseQty.Value;
    statusByCostCenter7.QtyActual = qtyActual.HasValue ? new Decimal?(qtyActual.GetValueOrDefault() + num5) : new Decimal?();
    statusByCostCenter3.SkipQtyValidation = split.SkipQtyValidation;
    if (!(tran.TranType == "TRX") || this.IsOneStepTransfer())
      return;
    this.TwoStepTransferNonLot(tran, split);
  }

  protected virtual TransitLocationStatusByCostCenter TwoStepTransferNonLot(
    INTran tran,
    INTranSplit split)
  {
    INTransitLine inTransitLine;
    if (this.IsIngoingTransfer(tran))
      inTransitLine = ((PXSelectBase<INTransitLine>) this.intransitline).Locate(new INTransitLine()
      {
        TransferNbr = tran.OrigRefNbr,
        TransferLineNbr = tran.OrigLineNbr
      });
    else
      inTransitLine = this.GetTransitLine(tran);
    TransitLocationStatusByCostCenter statusByCostCenter1 = new TransitLocationStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.SiteID = this.INTransitSiteID;
    statusByCostCenter1.LocationID = inTransitLine.CostSiteID;
    statusByCostCenter1.CostCenterID = !this.IsIngoingTransfer(tran) ? tran.ToCostCenterID : tran.CostCenterID;
    TransitLocationStatusByCostCenter statusByCostCenter2 = ((PXSelectBase<TransitLocationStatusByCostCenter>) this.transitlocationstatusbycostcenter).Insert(statusByCostCenter1);
    TransitLocationStatusByCostCenter statusByCostCenter3 = statusByCostCenter2;
    Decimal? nullable1 = statusByCostCenter3.QtyOnHand;
    short? invtMult = split.InvtMult;
    Decimal num1 = (Decimal) invtMult.Value;
    Decimal? nullable2 = split.BaseQty;
    Decimal num2 = nullable2.Value;
    Decimal num3 = num1 * num2;
    Decimal? nullable3;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable3 = nullable2;
    }
    else
      nullable3 = new Decimal?(nullable1.GetValueOrDefault() - num3);
    statusByCostCenter3.QtyOnHand = nullable3;
    TransitLocationStatusByCostCenter statusByCostCenter4 = statusByCostCenter2;
    nullable1 = statusByCostCenter4.QtyAvail;
    invtMult = split.InvtMult;
    Decimal num4 = (Decimal) invtMult.Value;
    nullable2 = split.BaseQty;
    Decimal num5 = nullable2.Value;
    Decimal num6 = num4 * num5;
    Decimal? nullable4;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(nullable1.GetValueOrDefault() - num6);
    statusByCostCenter4.QtyAvail = nullable4;
    TransitLocationStatusByCostCenter statusByCostCenter5 = statusByCostCenter2;
    nullable1 = statusByCostCenter5.QtyHardAvail;
    invtMult = split.InvtMult;
    Decimal num7 = (Decimal) invtMult.Value;
    nullable2 = split.BaseQty;
    Decimal num8 = nullable2.Value;
    Decimal num9 = num7 * num8;
    Decimal? nullable5;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new Decimal?(nullable1.GetValueOrDefault() - num9);
    statusByCostCenter5.QtyHardAvail = nullable5;
    TransitLocationStatusByCostCenter statusByCostCenter6 = statusByCostCenter2;
    nullable1 = statusByCostCenter6.QtyActual;
    invtMult = split.InvtMult;
    Decimal num10 = (Decimal) invtMult.Value;
    nullable2 = split.BaseQty;
    Decimal num11 = nullable2.Value;
    Decimal num12 = num10 * num11;
    Decimal? nullable6;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() - num12);
    statusByCostCenter6.QtyActual = nullable6;
    return statusByCostCenter2;
  }

  public virtual INTransitLine GetTransitLine(INTran tran)
  {
    INTransitLine inTransitLine1 = new INTransitLine();
    inTransitLine1.TransferNbr = tran.RefNbr;
    inTransitLine1.TransferLineNbr = tran.LineNbr;
    INTransitLine inTransitLine2 = ((PXSelectBase<INTransitLine>) this.intransitline).Locate(inTransitLine1);
    INTransitLine transitLine;
    if (inTransitLine2 == null)
    {
      inTransitLine1.SOOrderType = tran.SOOrderType;
      inTransitLine1.SOOrderNbr = tran.SOOrderNbr;
      inTransitLine1.SOOrderLineNbr = tran.SOOrderLineNbr;
      inTransitLine1.SOShipmentType = tran.SOShipmentType;
      inTransitLine1.SOShipmentNbr = tran.SOShipmentNbr;
      inTransitLine1.SOShipmentLineNbr = tran.SOShipmentLineNbr;
      inTransitLine1.OrigModule = tran.SOOrderNbr == null ? "IN" : "SO";
      inTransitLine1.SiteID = tran.SiteID;
      inTransitLine1.ToSiteID = tran.ToSiteID;
      inTransitLine1.RefNoteID = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.NoteID;
      inTransitLine1.TranDate = tran.TranDate;
      INTranSplit inTranSplit = PXResultset<INTranSplit>.op_Implicit(PXSelectBase<INTranSplit, PXSelectReadonly<INTranSplit, Where<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>, And<INTranSplit.docType, Equal<Current<INTran.docType>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) tran
      }, Array.Empty<object>()));
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, tran.ToSiteID);
      INItemSite inItemSite = INItemSite.PK.Find((PXGraph) this, tran.InventoryID, (int?) inSite?.SiteID);
      inTransitLine1.IsLotSerial = new bool?(!string.IsNullOrEmpty(inTranSplit?.LotSerialNbr));
      inTransitLine1.ToLocationID = tran.ToLocationID ?? (int?) inItemSite?.DfltReceiptLocationID ?? inSite.ReceiptLocationID;
      inTransitLine1.NoteID = new Guid?(PXNoteAttribute.GetNoteID<INTransitLine.noteID>(((PXSelectBase) this.intransitline).Cache, (object) inTransitLine1).Value);
      foreach (Note note in ((PXGraph) this).Caches[typeof (Note)].Inserted)
      {
        Guid? noteId1 = note.NoteID;
        Guid? noteId2 = inTransitLine1.NoteID;
        if ((noteId1.HasValue == noteId2.HasValue ? (noteId1.HasValue ? (noteId1.GetValueOrDefault() == noteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          note.GraphType = typeof (INTransferEntry).FullName;
          break;
        }
      }
      transitLine = ((PXSelectBase<INTransitLine>) this.intransitline).Insert(inTransitLine1);
    }
    else
      transitLine = inTransitLine2;
    return transitLine;
  }

  public virtual PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter AccumulatedLotSerialStatusByCostCenter(
    INTran tran,
    INTranSplit split,
    INLotSerClass lsclass)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.SiteID = split.SiteID;
    statusByCostCenter1.LocationID = split.LocationID;
    statusByCostCenter1.LotSerialNbr = split.LotSerialNbr;
    statusByCostCenter1.CostCenterID = !this.IsIngoingTransfer(tran) ? tran.ToCostCenterID : tran.CostCenterID;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>) this.lotnumberedstatusbycostcenter).Insert(statusByCostCenter1);
    DateTime? nullable = statusByCostCenter2.ExpireDate;
    if (!nullable.HasValue)
      statusByCostCenter2.ExpireDate = split.ExpireDate;
    nullable = statusByCostCenter2.ReceiptDate;
    if (!nullable.HasValue)
      statusByCostCenter2.ReceiptDate = split.TranDate;
    statusByCostCenter2.LotSerTrack = lsclass.LotSerTrack;
    return statusByCostCenter2;
  }

  public virtual TransitLotSerialStatusByCostCenter AccumulatedTransitLotSerialStatusByCostCenter(
    INTran tran,
    INTranSplit split,
    INLotSerClass lsclass,
    INTransitLine tl)
  {
    TransitLotSerialStatusByCostCenter statusByCostCenter1 = new TransitLotSerialStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.SiteID = this.INTransitSiteID;
    statusByCostCenter1.LocationID = tl.CostSiteID;
    statusByCostCenter1.LotSerialNbr = split.LotSerialNbr;
    statusByCostCenter1.CostCenterID = !this.IsIngoingTransfer(tran) ? tran.ToCostCenterID : tran.CostCenterID;
    TransitLotSerialStatusByCostCenter statusByCostCenter2 = ((PXSelectBase<TransitLotSerialStatusByCostCenter>) this.transitlotnumberedstatusbycostcenter).Insert(statusByCostCenter1);
    DateTime? nullable = statusByCostCenter2.ExpireDate;
    if (!nullable.HasValue)
      statusByCostCenter2.ExpireDate = split.ExpireDate;
    nullable = statusByCostCenter2.ReceiptDate;
    if (!nullable.HasValue)
      statusByCostCenter2.ReceiptDate = split.TranDate;
    statusByCostCenter2.LotSerTrack = lsclass.LotSerTrack;
    return statusByCostCenter2;
  }

  public virtual void ReceiveLot(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    short? invtMult = split.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 1;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue) || this.IsOneStepTransfer() || !(lsclass.LotSerTrack != "N") || !(lsclass.LotSerAssign == "R"))
      return;
    this.UpdateLotSerialStatusByCostCenter(tran, split, lsclass);
  }

  public virtual void IssueLot(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    short? invtMult1 = split.InvtMult;
    if ((invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?()).GetValueOrDefault() != -1)
      return;
    INLotSerClass lotSerClass = lsclass;
    string tranType = tran.TranType;
    short? invtMult2 = tran.InvtMult;
    int? invMult = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
    if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult) && (!(lsclass.LotSerTrack != "N") || !(lsclass.LotSerAssign == "R")) || !(lsclass.LotSerAssign == "R"))
      return;
    this.UpdateLotSerialStatusByCostCenter(tran, split, lsclass);
  }

  public virtual void TransferLot(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    if (this.IsOneStepTransfer())
    {
      short? invtMult = split.InvtMult;
      int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num = 1;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue && tran.OrigLineNbr.HasValue && lsclass.LotSerTrack != "N" && lsclass.LotSerAssign == "R")
        this.OneStepTransferLot(tran, split, item, lsclass);
    }
    if (!(tran.TranType == "TRX") || this.IsOneStepTransfer() || !(lsclass.LotSerTrack != "N") || !(lsclass.LotSerAssign == "R"))
      return;
    this.TwoStepTransferLot(tran, split, item, lsclass);
  }

  protected virtual PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter OneStepTransferLot(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    DateTime? receiptDate = (DateTime?) ((PXResult) PXResultset<INTranSplit>.op_Implicit(PXSelectBase<INTranSplit, PXSelectJoin<INTranSplit, InnerJoin<INTran, On<INTranSplit.FK.Tran>, InnerJoin<INLotSerialStatusByCostCenter, On<INLotSerialStatusByCostCenter.inventoryID, Equal<INTranSplit.inventoryID>, And<INLotSerialStatusByCostCenter.subItemID, Equal<INTranSplit.subItemID>, And<INLotSerialStatusByCostCenter.siteID, Equal<INTranSplit.siteID>, And<INLotSerialStatusByCostCenter.locationID, Equal<INTranSplit.locationID>, And<INLotSerialStatusByCostCenter.lotSerialNbr, Equal<INTranSplit.lotSerialNbr>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<INTran.costCenterID>>>>>>>>>, Where<INTranSplit.docType, Equal<Current<INTran.origDocType>>, And<INTranSplit.refNbr, Equal<Current<INTran.origRefNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.origLineNbr>>, And<INTranSplit.lotSerialNbr, Equal<Current<INTranSplit.lotSerialNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
    {
      (object) tran,
      (object) split
    }, Array.Empty<object>())))?.GetItem<INLotSerialStatusByCostCenter>()?.ReceiptDate;
    return this.UpdateLotSerialStatusByCostCenter(tran, split, lsclass, receiptDate);
  }

  protected virtual TransitLotSerialStatusByCostCenter TwoStepTransferLot(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    TransitLotSerialStatusByCostCenter statusByCostCenter1;
    if (this.IsIngoingTransfer(tran))
    {
      INTransitLine tl = ((PXSelectBase<INTransitLine>) this.intransitline).Locate(new INTransitLine()
      {
        TransferNbr = tran.OrigRefNbr,
        TransferLineNbr = tran.OrigLineNbr
      });
      statusByCostCenter1 = this.AccumulatedTransitLotSerialStatusByCostCenter(tran, split, lsclass, tl);
      DateTime? receiptDate = (DateTime?) INLotSerialStatusByCostCenter.PK.Find((PXGraph) this, split.InventoryID, split.SubItemID, ((PXSelectBase<INSetup>) this.insetup).Current.TransitSiteID, tl.CostSiteID, split.LotSerialNbr, tran.CostCenterID)?.ReceiptDate;
      if (receiptDate.HasValue)
        this.TwoStepTransferLotComplement(tran, split, item, lsclass, receiptDate.Value);
    }
    else
    {
      INTransitLine transitLine = this.GetTransitLine(tran);
      statusByCostCenter1 = this.AccumulatedTransitLotSerialStatusByCostCenter(tran, split, lsclass, transitLine);
      DateTime? receiptDate = (DateTime?) INLotSerialStatusByCostCenter.PK.Find((PXGraph) this, split.InventoryID, split.SubItemID, split.SiteID, split.LocationID, split.LotSerialNbr, tran.CostCenterID)?.ReceiptDate;
      if (receiptDate.HasValue)
        statusByCostCenter1.ReceiptDate = receiptDate;
    }
    TransitLotSerialStatusByCostCenter statusByCostCenter2 = statusByCostCenter1;
    Decimal? qtyOnHand = statusByCostCenter2.QtyOnHand;
    Decimal num1 = (Decimal) split.InvtMult.Value;
    Decimal? nullable1 = split.BaseQty;
    Decimal num2 = nullable1.Value;
    Decimal num3 = num1 * num2;
    Decimal? nullable2;
    if (!qtyOnHand.HasValue)
    {
      nullable1 = new Decimal?();
      nullable2 = nullable1;
    }
    else
      nullable2 = new Decimal?(qtyOnHand.GetValueOrDefault() - num3);
    statusByCostCenter2.QtyOnHand = nullable2;
    TransitLotSerialStatusByCostCenter statusByCostCenter3 = statusByCostCenter1;
    Decimal? nullable3 = statusByCostCenter3.QtyAvail;
    Decimal num4 = (Decimal) split.InvtMult.Value;
    nullable1 = split.BaseQty;
    Decimal num5 = nullable1.Value;
    Decimal num6 = num4 * num5;
    Decimal? nullable4;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable3.GetValueOrDefault() - num6);
    statusByCostCenter3.QtyAvail = nullable4;
    TransitLotSerialStatusByCostCenter statusByCostCenter4 = statusByCostCenter1;
    nullable3 = statusByCostCenter4.QtyHardAvail;
    Decimal num7 = (Decimal) split.InvtMult.Value;
    nullable1 = split.BaseQty;
    Decimal num8 = nullable1.Value;
    Decimal num9 = num7 * num8;
    Decimal? nullable5;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable5 = nullable1;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() - num9);
    statusByCostCenter4.QtyHardAvail = nullable5;
    TransitLotSerialStatusByCostCenter statusByCostCenter5 = statusByCostCenter1;
    nullable3 = statusByCostCenter5.QtyActual;
    Decimal num10 = (Decimal) split.InvtMult.Value;
    nullable1 = split.BaseQty;
    Decimal num11 = nullable1.Value;
    Decimal num12 = num10 * num11;
    Decimal? nullable6;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable6 = nullable1;
    }
    else
      nullable6 = new Decimal?(nullable3.GetValueOrDefault() - num12);
    statusByCostCenter5.QtyActual = nullable6;
    return statusByCostCenter1;
  }

  protected virtual PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter TwoStepTransferLotComplement(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass,
    DateTime receiptDate)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter = this.AccumulatedLotSerialStatusByCostCenter(tran, split, lsclass);
    statusByCostCenter.ReceiptDate = new DateTime?(receiptDate);
    return statusByCostCenter;
  }

  public virtual INItemLotSerial UpdateItemLotSerial(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    if (this.IsOneStepTransferWithinSite())
      return (INItemLotSerial) null;
    short? invtMult1 = split.InvtMult;
    if ((invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 && !string.IsNullOrEmpty(split.LotSerialNbr) && lsclass.LotSerTrack != "N" && (lsclass.LotSerAssign == "R" || lsclass.LotSerAssign == "U" && EnumerableExtensions.IsIn<string>(split.TranType, "CRM", "RET")))
    {
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial1 = this.AccumulatedItemLotSerial(split, lsclass);
      if (!tran.OrigLineNbr.HasValue && itemLotSerial1.ExpireDate.HasValue && !itemLotSerial1.UpdateExpireDate.HasValue)
        itemLotSerial1.UpdateExpireDate = new bool?(true);
      if (split.TranType == "ADJ")
      {
        short? invtMult2 = split.InvtMult;
        if ((invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 && split.ExpireDate.HasValue)
        {
          itemLotSerial1.UpdateExpireDate = new bool?(true);
          itemLotSerial1.ExpireDate = split.ExpireDate;
        }
      }
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial2 = itemLotSerial1;
      Decimal? qtyOnHand = itemLotSerial2.QtyOnHand;
      short? invtMult3 = split.InvtMult;
      Decimal num1 = (Decimal) invtMult3.Value;
      Decimal? nullable1 = split.BaseQty;
      Decimal num2 = nullable1.Value;
      Decimal num3 = num1 * num2;
      Decimal? nullable2;
      if (!qtyOnHand.HasValue)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Decimal?(qtyOnHand.GetValueOrDefault() + num3);
      itemLotSerial2.QtyOnHand = nullable2;
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial3 = itemLotSerial1;
      Decimal? nullable3 = itemLotSerial3.QtyAvail;
      invtMult3 = split.InvtMult;
      Decimal num4 = (Decimal) invtMult3.Value;
      nullable1 = split.BaseQty;
      Decimal num5 = nullable1.Value;
      Decimal num6 = num4 * num5;
      Decimal? nullable4;
      if (!nullable3.HasValue)
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(nullable3.GetValueOrDefault() + num6);
      itemLotSerial3.QtyAvail = nullable4;
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial4 = itemLotSerial1;
      nullable3 = itemLotSerial4.QtyHardAvail;
      invtMult3 = split.InvtMult;
      Decimal num7 = (Decimal) invtMult3.Value;
      nullable1 = split.BaseQty;
      Decimal num8 = nullable1.Value;
      Decimal num9 = num7 * num8;
      Decimal? nullable5;
      if (!nullable3.HasValue)
      {
        nullable1 = new Decimal?();
        nullable5 = nullable1;
      }
      else
        nullable5 = new Decimal?(nullable3.GetValueOrDefault() + num9);
      itemLotSerial4.QtyHardAvail = nullable5;
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial5 = itemLotSerial1;
      nullable3 = itemLotSerial5.QtyActual;
      invtMult3 = split.InvtMult;
      Decimal num10 = (Decimal) invtMult3.Value;
      nullable1 = split.BaseQty;
      Decimal num11 = nullable1.Value;
      Decimal num12 = num10 * num11;
      Decimal? nullable6;
      if (!nullable3.HasValue)
      {
        nullable1 = new Decimal?();
        nullable6 = nullable1;
      }
      else
        nullable6 = new Decimal?(nullable3.GetValueOrDefault() + num12);
      itemLotSerial5.QtyActual = nullable6;
      if (split.IsIntercompany.GetValueOrDefault())
        itemLotSerial1.IsIntercompany = new bool?(true);
      return (INItemLotSerial) itemLotSerial1;
    }
    short? invtMult4 = split.InvtMult;
    if ((invtMult4.HasValue ? new int?((int) invtMult4.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      Decimal? baseQty = split.BaseQty;
      Decimal num13 = 0M;
      if (!(baseQty.GetValueOrDefault() == num13 & baseQty.HasValue) && !string.IsNullOrEmpty(split.LotSerialNbr) && lsclass.LotSerTrack != "N")
      {
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial6 = this.AccumulatedItemLotSerial(split, lsclass);
        if (itemLotSerial6.ExpireDate.HasValue && !itemLotSerial6.UpdateExpireDate.HasValue)
          itemLotSerial6.UpdateExpireDate = new bool?(lsclass.LotSerAssign == "U");
        if (lsclass.LotSerTrack == "S" || lsclass.LotSerAssign == "R")
        {
          PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial7 = itemLotSerial6;
          Decimal? qtyOnHand = itemLotSerial7.QtyOnHand;
          short? invtMult5 = split.InvtMult;
          Decimal num14 = (Decimal) invtMult5.Value;
          Decimal? nullable7 = split.BaseQty;
          Decimal num15 = nullable7.Value;
          Decimal num16 = num14 * num15;
          Decimal? nullable8;
          if (!qtyOnHand.HasValue)
          {
            nullable7 = new Decimal?();
            nullable8 = nullable7;
          }
          else
            nullable8 = new Decimal?(qtyOnHand.GetValueOrDefault() + num16);
          itemLotSerial7.QtyOnHand = nullable8;
          PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial8 = itemLotSerial6;
          Decimal? nullable9 = itemLotSerial8.QtyAvail;
          invtMult5 = split.InvtMult;
          Decimal num17 = (Decimal) invtMult5.Value;
          nullable7 = split.BaseQty;
          Decimal num18 = nullable7.Value;
          Decimal num19 = num17 * num18;
          Decimal? nullable10;
          if (!nullable9.HasValue)
          {
            nullable7 = new Decimal?();
            nullable10 = nullable7;
          }
          else
            nullable10 = new Decimal?(nullable9.GetValueOrDefault() + num19);
          itemLotSerial8.QtyAvail = nullable10;
          PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial9 = itemLotSerial6;
          nullable9 = itemLotSerial9.QtyHardAvail;
          invtMult5 = split.InvtMult;
          Decimal num20 = (Decimal) invtMult5.Value;
          nullable7 = split.BaseQty;
          Decimal num21 = nullable7.Value;
          Decimal num22 = num20 * num21;
          Decimal? nullable11;
          if (!nullable9.HasValue)
          {
            nullable7 = new Decimal?();
            nullable11 = nullable7;
          }
          else
            nullable11 = new Decimal?(nullable9.GetValueOrDefault() + num22);
          itemLotSerial9.QtyHardAvail = nullable11;
          PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial10 = itemLotSerial6;
          nullable9 = itemLotSerial10.QtyActual;
          invtMult5 = split.InvtMult;
          Decimal num23 = (Decimal) invtMult5.Value;
          nullable7 = split.BaseQty;
          Decimal num24 = nullable7.Value;
          Decimal num25 = num23 * num24;
          Decimal? nullable12;
          if (!nullable9.HasValue)
          {
            nullable7 = new Decimal?();
            nullable12 = nullable7;
          }
          else
            nullable12 = new Decimal?(nullable9.GetValueOrDefault() + num25);
          itemLotSerial10.QtyActual = nullable12;
        }
        if (split.IsIntercompany.GetValueOrDefault())
          itemLotSerial6.IsIntercompany = new bool?(true);
        return (INItemLotSerial) itemLotSerial6;
      }
    }
    return (INItemLotSerial) null;
  }

  public virtual INSiteLotSerial UpdateSiteLotSerial(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    if (string.IsNullOrEmpty(split.LotSerialNbr) || lsclass.LotSerTrack == "N")
      return (INSiteLotSerial) null;
    short? invtMult;
    bool? nullable1;
    if (this.IsOneStepTransferWithinSite())
    {
      invtMult = split.InvtMult;
      INLocation inLocation1 = INLocation.PK.Find((PXGraph) this, (invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 ? split.FromLocationID : split.LocationID);
      invtMult = split.InvtMult;
      INLocation inLocation2 = INLocation.PK.Find((PXGraph) this, (invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 ? split.LocationID : split.ToLocationID);
      bool? inclQtyAvail = (bool?) inLocation1?.InclQtyAvail;
      nullable1 = (bool?) inLocation2?.InclQtyAvail;
      if (inclQtyAvail.GetValueOrDefault() == nullable1.GetValueOrDefault() & inclQtyAvail.HasValue == nullable1.HasValue)
        return (INSiteLotSerial) null;
    }
    invtMult = split.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 && lsclass.LotSerAssign == "R")
    {
      SiteLotSerial siteLotSerial1 = this.AccumulatedSiteLotSerial(split, lsclass);
      int? nullable2 = tran.OrigLineNbr;
      DateTime? expireDate;
      if (!nullable2.HasValue)
      {
        expireDate = siteLotSerial1.ExpireDate;
        if (expireDate.HasValue)
        {
          nullable1 = siteLotSerial1.UpdateExpireDate;
          if (!nullable1.HasValue)
            siteLotSerial1.UpdateExpireDate = new bool?(true);
        }
      }
      if (split.TranType == "ADJ")
      {
        expireDate = split.ExpireDate;
        if (expireDate.HasValue)
        {
          siteLotSerial1.UpdateExpireDate = new bool?(true);
          siteLotSerial1.ExpireDate = split.ExpireDate;
        }
      }
      SiteLotSerial siteLotSerial2 = siteLotSerial1;
      Decimal? nullable3 = siteLotSerial2.QtyOnHand;
      invtMult = split.InvtMult;
      Decimal num1 = (Decimal) invtMult.Value;
      Decimal? nullable4 = split.BaseQty;
      Decimal num2 = nullable4.Value;
      Decimal num3 = num1 * num2;
      Decimal? nullable5;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable5 = nullable4;
      }
      else
        nullable5 = new Decimal?(nullable3.GetValueOrDefault() + num3);
      siteLotSerial2.QtyOnHand = nullable5;
      nullable2 = split.LocationID;
      INLocation inLocation = INLocation.PK.Find((PXGraph) this, nullable2 ?? tran.LocationID);
      SiteLotSerial siteLotSerial3 = siteLotSerial1;
      nullable3 = siteLotSerial3.QtyAvail;
      nullable1 = inLocation.InclQtyAvail;
      Decimal num4;
      if (!nullable1.GetValueOrDefault())
      {
        num4 = 0M;
      }
      else
      {
        invtMult = split.InvtMult;
        Decimal num5 = (Decimal) invtMult.Value;
        nullable4 = split.BaseQty;
        Decimal num6 = nullable4.Value;
        num4 = num5 * num6;
      }
      Decimal num7 = num4;
      Decimal? nullable6;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable6 = nullable4;
      }
      else
        nullable6 = new Decimal?(nullable3.GetValueOrDefault() + num7);
      siteLotSerial3.QtyAvail = nullable6;
      SiteLotSerial siteLotSerial4 = siteLotSerial1;
      nullable3 = siteLotSerial4.QtyHardAvail;
      nullable1 = inLocation.InclQtyAvail;
      Decimal num8;
      if (!nullable1.GetValueOrDefault())
      {
        num8 = 0M;
      }
      else
      {
        invtMult = split.InvtMult;
        Decimal num9 = (Decimal) invtMult.Value;
        nullable4 = split.BaseQty;
        Decimal num10 = nullable4.Value;
        num8 = num9 * num10;
      }
      Decimal num11 = num8;
      Decimal? nullable7;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable7 = nullable4;
      }
      else
        nullable7 = new Decimal?(nullable3.GetValueOrDefault() + num11);
      siteLotSerial4.QtyHardAvail = nullable7;
      SiteLotSerial siteLotSerial5 = siteLotSerial1;
      nullable3 = siteLotSerial5.QtyActual;
      nullable1 = inLocation.InclQtyAvail;
      Decimal num12;
      if (!nullable1.GetValueOrDefault())
      {
        num12 = 0M;
      }
      else
      {
        invtMult = split.InvtMult;
        Decimal num13 = (Decimal) invtMult.Value;
        nullable4 = split.BaseQty;
        Decimal num14 = nullable4.Value;
        num12 = num13 * num14;
      }
      Decimal num15 = num12;
      Decimal? nullable8;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable8 = nullable4;
      }
      else
        nullable8 = new Decimal?(nullable3.GetValueOrDefault() + num15);
      siteLotSerial5.QtyActual = nullable8;
      SiteLotSerial siteLotSerial6 = siteLotSerial1;
      nullable3 = siteLotSerial6.QtyNotAvail;
      nullable1 = inLocation.InclQtyAvail;
      Decimal num16;
      if (!nullable1.GetValueOrDefault())
      {
        invtMult = split.InvtMult;
        Decimal num17 = (Decimal) invtMult.Value;
        nullable4 = split.BaseQty;
        Decimal num18 = nullable4.Value;
        num16 = num17 * num18;
      }
      else
        num16 = 0M;
      Decimal num19 = num16;
      Decimal? nullable9;
      if (!nullable3.HasValue)
      {
        nullable4 = new Decimal?();
        nullable9 = nullable4;
      }
      else
        nullable9 = new Decimal?(nullable3.GetValueOrDefault() + num19);
      siteLotSerial6.QtyNotAvail = nullable9;
      return (INSiteLotSerial) siteLotSerial1;
    }
    invtMult = split.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      Decimal? baseQty1 = split.BaseQty;
      Decimal num20 = 0M;
      if (!(baseQty1.GetValueOrDefault() == num20 & baseQty1.HasValue))
      {
        SiteLotSerial siteLotSerial7 = this.AccumulatedSiteLotSerial(split, lsclass);
        SiteLotSerial siteLotSerial8 = siteLotSerial7;
        nullable1 = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IgnoreAllocationErrors;
        int num21;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = siteLotSerial7.ValidateHardAvailQtyForAdjustments;
          if (!nullable1.Value)
          {
            if (split.DocType == "A")
            {
              Decimal? baseQty2 = split.BaseQty;
              Decimal num22 = 0M;
              if (baseQty2.GetValueOrDefault() > num22 & baseQty2.HasValue)
              {
                invtMult = split.InvtMult;
                num21 = (invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1 ? 1 : 0;
                goto label_51;
              }
            }
            num21 = 0;
          }
          else
            num21 = 1;
        }
        else
          num21 = 0;
label_51:
        bool? nullable10 = new bool?(num21 != 0);
        siteLotSerial8.ValidateHardAvailQtyForAdjustments = nullable10;
        if (siteLotSerial7.ExpireDate.HasValue)
        {
          nullable1 = siteLotSerial7.UpdateExpireDate;
          if (!nullable1.HasValue)
            siteLotSerial7.UpdateExpireDate = new bool?(false);
        }
        if (lsclass.LotSerTrack == "S" || lsclass.LotSerAssign == "R")
        {
          SiteLotSerial siteLotSerial9 = siteLotSerial7;
          Decimal? nullable11 = siteLotSerial9.QtyOnHand;
          invtMult = split.InvtMult;
          Decimal num23 = (Decimal) invtMult.Value;
          Decimal? nullable12 = split.BaseQty;
          Decimal num24 = nullable12.Value;
          Decimal num25 = num23 * num24;
          Decimal? nullable13;
          if (!nullable11.HasValue)
          {
            nullable12 = new Decimal?();
            nullable13 = nullable12;
          }
          else
            nullable13 = new Decimal?(nullable11.GetValueOrDefault() + num25);
          siteLotSerial9.QtyOnHand = nullable13;
          INLocation inLocation = INLocation.PK.Find((PXGraph) this, split.LocationID ?? tran.LocationID);
          SiteLotSerial siteLotSerial10 = siteLotSerial7;
          nullable11 = siteLotSerial10.QtyAvail;
          nullable1 = inLocation.InclQtyAvail;
          Decimal num26;
          if (!nullable1.GetValueOrDefault())
          {
            num26 = 0M;
          }
          else
          {
            invtMult = split.InvtMult;
            Decimal num27 = (Decimal) invtMult.Value;
            nullable12 = split.BaseQty;
            Decimal num28 = nullable12.Value;
            num26 = num27 * num28;
          }
          Decimal num29 = num26;
          Decimal? nullable14;
          if (!nullable11.HasValue)
          {
            nullable12 = new Decimal?();
            nullable14 = nullable12;
          }
          else
            nullable14 = new Decimal?(nullable11.GetValueOrDefault() + num29);
          siteLotSerial10.QtyAvail = nullable14;
          SiteLotSerial siteLotSerial11 = siteLotSerial7;
          nullable11 = siteLotSerial11.QtyHardAvail;
          nullable1 = inLocation.InclQtyAvail;
          Decimal num30;
          if (!nullable1.GetValueOrDefault())
          {
            num30 = 0M;
          }
          else
          {
            invtMult = split.InvtMult;
            Decimal num31 = (Decimal) invtMult.Value;
            nullable12 = split.BaseQty;
            Decimal num32 = nullable12.Value;
            num30 = num31 * num32;
          }
          Decimal num33 = num30;
          Decimal? nullable15;
          if (!nullable11.HasValue)
          {
            nullable12 = new Decimal?();
            nullable15 = nullable12;
          }
          else
            nullable15 = new Decimal?(nullable11.GetValueOrDefault() + num33);
          siteLotSerial11.QtyHardAvail = nullable15;
          SiteLotSerial siteLotSerial12 = siteLotSerial7;
          nullable11 = siteLotSerial12.QtyActual;
          nullable1 = inLocation.InclQtyAvail;
          Decimal num34;
          if (!nullable1.GetValueOrDefault())
          {
            num34 = 0M;
          }
          else
          {
            invtMult = split.InvtMult;
            Decimal num35 = (Decimal) invtMult.Value;
            nullable12 = split.BaseQty;
            Decimal num36 = nullable12.Value;
            num34 = num35 * num36;
          }
          Decimal num37 = num34;
          Decimal? nullable16;
          if (!nullable11.HasValue)
          {
            nullable12 = new Decimal?();
            nullable16 = nullable12;
          }
          else
            nullable16 = new Decimal?(nullable11.GetValueOrDefault() + num37);
          siteLotSerial12.QtyActual = nullable16;
          SiteLotSerial siteLotSerial13 = siteLotSerial7;
          nullable11 = siteLotSerial13.QtyNotAvail;
          nullable1 = inLocation.InclQtyAvail;
          Decimal num38;
          if (!nullable1.GetValueOrDefault())
          {
            invtMult = split.InvtMult;
            Decimal num39 = (Decimal) invtMult.Value;
            nullable12 = split.BaseQty;
            Decimal num40 = nullable12.Value;
            num38 = num39 * num40;
          }
          else
            num38 = 0M;
          Decimal num41 = num38;
          Decimal? nullable17;
          if (!nullable11.HasValue)
          {
            nullable12 = new Decimal?();
            nullable17 = nullable12;
          }
          else
            nullable17 = new Decimal?(nullable11.GetValueOrDefault() + num41);
          siteLotSerial13.QtyNotAvail = nullable17;
        }
        return (INSiteLotSerial) siteLotSerial7;
      }
    }
    return (INSiteLotSerial) null;
  }

  public virtual void UpdateLotSerialStatus(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLotSerClass lsclass)
  {
    this.ReceiveLot(tran, split, item, lsclass);
    this.IssueLot(tran, split, item, lsclass);
    this.TransferLot(tran, split, item, lsclass);
  }

  protected virtual PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter UpdateLotSerialStatusByCostCenter(
    INTran tran,
    INTranSplit split,
    INLotSerClass lsclass,
    DateTime? overrideReceiptDate = null)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.SiteID = split.SiteID;
    statusByCostCenter1.LocationID = split.LocationID;
    statusByCostCenter1.LotSerialNbr = split.LotSerialNbr;
    statusByCostCenter1.CostCenterID = tran.CostCenterID;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter2 = statusByCostCenter1;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter3 = GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>((PXGraph) this).Insert(statusByCostCenter2);
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter4 = statusByCostCenter3;
    DateTime? nullable1;
    if (!statusByCostCenter4.ExpireDate.HasValue)
      statusByCostCenter4.ExpireDate = nullable1 = split.ExpireDate;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter5 = statusByCostCenter3;
    if (!statusByCostCenter5.ReceiptDate.HasValue)
      statusByCostCenter5.ReceiptDate = nullable1 = overrideReceiptDate ?? split.TranDate;
    statusByCostCenter3.LotSerTrack = lsclass.LotSerTrack;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter6 = statusByCostCenter3;
    Decimal? nullable2 = statusByCostCenter6.QtyOnHand;
    Decimal num1 = (Decimal) split.InvtMult.Value * split.BaseQty.Value;
    statusByCostCenter6.QtyOnHand = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num1) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter7 = statusByCostCenter3;
    nullable2 = statusByCostCenter7.QtyAvail;
    short? invtMult = split.InvtMult;
    Decimal num2 = (Decimal) invtMult.Value * split.BaseQty.Value;
    statusByCostCenter7.QtyAvail = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter8 = statusByCostCenter3;
    nullable2 = statusByCostCenter8.QtyHardAvail;
    invtMult = split.InvtMult;
    Decimal num3 = (Decimal) invtMult.Value * split.BaseQty.Value;
    statusByCostCenter8.QtyHardAvail = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num3) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter9 = statusByCostCenter3;
    nullable2 = statusByCostCenter9.QtyActual;
    invtMult = split.InvtMult;
    Decimal num4 = (Decimal) invtMult.Value * split.BaseQty.Value;
    statusByCostCenter9.QtyActual = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num4) : new Decimal?();
    return statusByCostCenter3;
  }

  public virtual PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial AccumulatedItemLotSerial(
    INTranSplit split,
    INLotSerClass lsclass)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial();
    itemLotSerial1.InventoryID = split.InventoryID;
    itemLotSerial1.LotSerialNbr = split.LotSerialNbr;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>) this.itemlotserial).Insert(itemLotSerial1);
    if (!itemLotSerial2.ExpireDate.HasValue)
      itemLotSerial2.ExpireDate = split.ExpireDate;
    itemLotSerial2.LotSerTrack = lsclass.LotSerTrack;
    return itemLotSerial2;
  }

  public virtual SiteLotSerial AccumulatedSiteLotSerial(INTranSplit split, INLotSerClass lsclass)
  {
    SiteLotSerial siteLotSerial1 = new SiteLotSerial();
    siteLotSerial1.InventoryID = split.InventoryID;
    siteLotSerial1.LotSerialNbr = split.LotSerialNbr;
    siteLotSerial1.SiteID = split.SiteID;
    SiteLotSerial siteLotSerial2 = ((PXSelectBase<SiteLotSerial>) this.sitelotserial).Insert(siteLotSerial1);
    if (!siteLotSerial2.ExpireDate.HasValue)
      siteLotSerial2.ExpireDate = split.ExpireDate;
    siteLotSerial2.LotSerTrack = lsclass.LotSerTrack;
    return siteLotSerial2;
  }

  public virtual INCostStatus AccumulatedCostStatus(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item)
  {
    int? costSiteId = split.CostSiteID;
    int? inTransitSiteId = this.INTransitSiteID;
    bool flag = costSiteId.GetValueOrDefault() == inTransitSiteId.GetValueOrDefault() & costSiteId.HasValue == inTransitSiteId.HasValue;
    switch (item.ValMethod)
    {
      case "T":
        if (tran.TranType == "NSC")
          return this.AccumOversoldCostStatus(tran, split, item);
        INCostStatus inCostStatus1 = (INCostStatus) new StandardCostStatus();
        inCostStatus1.AccountID = tran.InvtAcctID;
        inCostStatus1.SubID = tran.InvtSubID;
        inCostStatus1.InventoryID = tran.InventoryID;
        inCostStatus1.CostSiteID = split.CostSiteID;
        inCostStatus1.SiteID = split.SiteID;
        inCostStatus1.CostSubItemID = split.CostSubItemID;
        inCostStatus1.ReceiptNbr = "ZZZ";
        inCostStatus1.LayerType = "N";
        return (INCostStatus) ((PXSelectBase) this.standardcoststatus).Cache.Insert((object) inCostStatus1);
      case "A":
        INCostStatus inCostStatus2 = (INCostStatus) new AverageCostStatus();
        inCostStatus2.AccountID = tran.InvtAcctID;
        inCostStatus2.SubID = tran.InvtSubID;
        inCostStatus2.InventoryID = tran.InventoryID;
        inCostStatus2.CostSiteID = split.CostSiteID;
        inCostStatus2.SiteID = split.SiteID;
        inCostStatus2.CostSubItemID = split.CostSubItemID;
        inCostStatus2.ReceiptNbr = "ZZZ";
        inCostStatus2.LayerType = "N";
        return (INCostStatus) ((PXSelectBase) this.averagecoststatus).Cache.Insert((object) inCostStatus2);
      case "F":
        INCostStatus inCostStatus3 = (INCostStatus) new FIFOCostStatus();
        inCostStatus3.AccountID = tran.InvtAcctID;
        inCostStatus3.SubID = tran.InvtSubID;
        inCostStatus3.InventoryID = tran.InventoryID;
        inCostStatus3.CostSiteID = split.CostSiteID;
        inCostStatus3.SiteID = split.SiteID;
        inCostStatus3.CostSubItemID = split.CostSubItemID;
        inCostStatus3.ReceiptDate = tran.TranDate;
        inCostStatus3.ReceiptNbr = tran.OrigRefNbr ?? tran.RefNbr;
        inCostStatus3.LayerType = "N";
        return (INCostStatus) ((PXSelectBase) this.fifocoststatus).Cache.Insert((object) inCostStatus3);
      case "S":
        INCostStatus inCostStatus4 = flag ? (INCostStatus) new SpecificTransitCostStatus() : (INCostStatus) new SpecificCostStatus();
        inCostStatus4.AccountID = tran.InvtAcctID;
        inCostStatus4.SubID = tran.InvtSubID;
        inCostStatus4.InventoryID = tran.InventoryID;
        inCostStatus4.CostSiteID = split.CostSiteID;
        inCostStatus4.SiteID = split.SiteID;
        inCostStatus4.CostSubItemID = split.CostSubItemID;
        inCostStatus4.LotSerialNbr = split.LotSerialNbr;
        inCostStatus4.ReceiptDate = tran.TranDate;
        inCostStatus4.ReceiptNbr = tran.RefNbr;
        inCostStatus4.LayerType = "N";
        short? invtMult = tran.InvtMult;
        int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue && (tran.TranType == "INV" || tran.TranType == "DRM" || tran.TranType == "CRM"))
          inCostStatus4.LotSerialNbr = string.Empty;
        return !flag ? (INCostStatus) ((PXSelectBase) this.specificcoststatus).Cache.Insert((object) inCostStatus4) : (INCostStatus) ((PXSelectBase) this.specifictransitcoststatus).Cache.Insert((object) inCostStatus4);
      default:
        throw new PXException();
    }
  }

  public virtual int? GetTransitCostSiteID(INTran tran) => this.INTransitSiteID;

  public virtual INCostStatus AccumOversoldCostStatus(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item)
  {
    if (!(item.ValMethod == "S"))
    {
      bool? negQty = item.NegQty;
      bool flag = false;
      if (!(negQty.GetValueOrDefault() == flag & negQty.HasValue) || !(tran.TranType != "NSC"))
      {
        string valMethod = item.ValMethod;
        if (!(valMethod == "T") && !(valMethod == "A") && !(valMethod == "F"))
          throw new PXInvalidOperationException();
        INCostStatus inCostStatus = (INCostStatus) new OversoldCostStatus();
        inCostStatus.AccountID = tran.InvtAcctID;
        inCostStatus.SubID = tran.InvtSubID;
        inCostStatus.InventoryID = tran.InventoryID;
        inCostStatus.CostSiteID = split.CostSiteID;
        inCostStatus.SiteID = split.SiteID;
        inCostStatus.CostSubItemID = split.CostSubItemID;
        inCostStatus.ReceiptDate = new DateTime?(new DateTime(1900, 1, 1));
        inCostStatus.ReceiptNbr = "OVERSOLD";
        inCostStatus.LayerType = "O";
        inCostStatus.ValMethod = item.ValMethod;
        return (INCostStatus) ((PXSelectBase) this.oversoldcoststatus).Cache.Insert((object) inCostStatus);
      }
    }
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, tran.SiteID);
    INLocation inLocation = INLocation.PK.Find((PXGraph) this, tran.LocationID);
    if (item.ValMethod == "S")
      throw new PXException("Updating item '{0} {1}' in warehouse '{2} {3}' lot/serial number '{4}' quantity on hand will go negative.", new object[5]
      {
        (object) item.InventoryCD?.TrimEnd(),
        (object) string.Empty,
        (object) inSite?.SiteCD?.TrimEnd(),
        (object) inLocation?.LocationCD?.TrimEnd(),
        (object) split.LotSerialNbr
      });
    throw new PXException("Inventory quantity for {0} in warehouse '{1} {2}' will go negative.", new object[3]
    {
      (object) item.InventoryCD?.TrimEnd(),
      (object) inSite?.SiteCD?.TrimEnd(),
      (object) inLocation?.LocationCD?.TrimEnd()
    });
  }

  public virtual INCostStatus AccumUnmanagedCostStatus(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item)
  {
    INCostStatus inCostStatus = (INCostStatus) new UnmanagedCostStatus();
    inCostStatus.AccountID = tran.InvtAcctID;
    inCostStatus.SubID = tran.InvtSubID;
    inCostStatus.InventoryID = tran.InventoryID;
    inCostStatus.CostSiteID = split.CostSiteID;
    inCostStatus.SiteID = split.SiteID;
    inCostStatus.CostSubItemID = split.CostSubItemID;
    inCostStatus.ReceiptNbr = "UNMANAGED";
    inCostStatus.LayerType = "U";
    inCostStatus.ValMethod = item.ValMethod;
    return (INCostStatus) ((PXSelectBase) this.unmanagedcoststatus).Cache.Insert((object) inCostStatus);
  }

  public virtual INCostStatus AccumOversoldCostStatus(INCostStatus layer)
  {
    INCostStatus inCostStatus = (INCostStatus) new OversoldCostStatus();
    PXCache<INCostStatus>.RestoreCopy(inCostStatus, layer);
    inCostStatus.QtyOnHand = new Decimal?(0M);
    inCostStatus.TotalCost = new Decimal?(0M);
    return (INCostStatus) ((PXSelectBase) this.oversoldcoststatus).Cache.Insert((object) inCostStatus);
  }

  public virtual PXView GetReceiptStatusView(PX.Objects.IN.InventoryItem item)
  {
    List<Type> typeList = new List<Type>()
    {
      typeof (Select2<,,>),
      typeof (ReadOnlyCostStatus),
      typeof (LeftJoin<ReceiptStatus, On<ReceiptStatus.inventoryID, Equal<ReadOnlyCostStatus.inventoryID>, And<ReceiptStatus.costSubItemID, Equal<ReadOnlyCostStatus.costSubItemID>, And<ReceiptStatus.costSiteID, Equal<ReadOnlyCostStatus.costSiteID>, And<ReceiptStatus.accountID, Equal<ReadOnlyCostStatus.accountID>, And<ReceiptStatus.subID, Equal<ReadOnlyCostStatus.subID>>>>>>>),
      typeof (Where2<,>),
      typeof (Where<ReadOnlyCostStatus.inventoryID, Equal<Current<INTranSplit.inventoryID>>, And<ReadOnlyCostStatus.costSubItemID, Equal<Current<INTranSplit.costSubItemID>>, And<ReadOnlyCostStatus.costSiteID, Equal<Current<INTranSplit.costSiteID>>, And<ReadOnlyCostStatus.layerType, Equal<INLayerType.normal>, And<ReadOnlyCostStatus.accountID, Equal<Required<INTran.invtAcctID>>, And<ReadOnlyCostStatus.subID, Equal<Required<INTran.invtSubID>>>>>>>>)
    };
    switch (item.ValMethod)
    {
      case "T":
        typeList.Add(typeof (And<Where<True, Equal<False>>>));
        break;
      case "F":
        typeList.Add(typeof (And<Where<ReadOnlyCostStatus.receiptNbr, Equal<Current<INTran.origRefNbr>>>>));
        break;
      case "S":
        typeList.Add(typeof (And<Where<ReadOnlyCostStatus.lotSerialNbr, Equal<ReceiptStatus.lotSerialNbr>, And<ReceiptStatus.docType, Equal<INDocType.receipt>, And<ReceiptStatus.receiptNbr, Equal<Current<INTran.origRefNbr>>, And<ReadOnlyCostStatus.lotSerialNbr, Equal<Current<INTranSplit.lotSerialNbr>>>>>>>));
        break;
      case "A":
        typeList.Add(typeof (And<Where<ReadOnlyCostStatus.receiptNbr, Equal<INLayerRef.zzz>, And<ReceiptStatus.docType, Equal<INDocType.receipt>, And<ReceiptStatus.receiptNbr, Equal<Current<INTran.origRefNbr>>>>>>));
        break;
    }
    return ((PXGraph) this).TypedViews.GetView(BqlCommand.CreateInstance(typeList.ToArray()), false);
  }

  public virtual PXView GetReceiptStatusByKeysView(INCostStatus layer)
  {
    List<Type> typeList = new List<Type>()
    {
      typeof (Select<,>),
      typeof (ReadOnlyReceiptStatus),
      typeof (Where<,,>),
      typeof (ReadOnlyReceiptStatus.qtyOnHand),
      typeof (Greater<decimal0>),
      typeof (And<,,>),
      typeof (ReadOnlyReceiptStatus.inventoryID),
      typeof (Equal<Required<INCostStatus.inventoryID>>),
      typeof (And<,,>),
      typeof (ReadOnlyReceiptStatus.costSiteID),
      typeof (Equal<Required<INCostStatus.costSiteID>>),
      typeof (And<,,>),
      typeof (ReadOnlyReceiptStatus.costSubItemID),
      typeof (Equal<Required<INCostStatus.costSubItemID>>),
      typeof (And<,,>),
      typeof (ReadOnlyReceiptStatus.accountID),
      typeof (Equal<Required<INCostStatus.accountID>>)
    };
    if (layer.ValMethod == "S")
    {
      typeList.Add(typeof (And<,,>));
      typeList.Add(typeof (ReadOnlyReceiptStatus.subID));
      typeList.Add(typeof (Equal<Required<INCostStatus.subID>>));
      typeList.Add(typeof (And<,>));
      typeList.Add(typeof (ReadOnlyReceiptStatus.lotSerialNbr));
      typeList.Add(typeof (Equal<Required<INCostStatus.lotSerialNbr>>));
    }
    else
    {
      typeList.Add(typeof (And<,>));
      typeList.Add(typeof (ReadOnlyReceiptStatus.subID));
      typeList.Add(typeof (Equal<Required<INCostStatus.subID>>));
    }
    return ((PXGraph) this).TypedViews.GetView(BqlCommand.CreateInstance(typeList.ToArray()), false);
  }

  public virtual PXView GetCostStatusCommand(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    out object[] parameters,
    bool correctImbalance,
    string fifoLayerNbr)
  {
    int? nullable = !this.IsIngoingTransfer(tran) ? split.CostSiteID : this.GetTransitCostSiteID(tran);
    if (correctImbalance || this.IsIngoingTransfer(tran))
      fifoLayerNbr = tran.OrigRefNbr;
    BqlCommand bqlCommand;
    switch (item.ValMethod)
    {
      case "A":
      case "T":
      case "F":
        bqlCommand = (BqlCommand) new Select<ReadOnlyCostStatus, Where<ReadOnlyCostStatus.inventoryID, Equal<Required<ReadOnlyCostStatus.inventoryID>>, And<ReadOnlyCostStatus.costSiteID, Equal<Required<ReadOnlyCostStatus.costSiteID>>, And<ReadOnlyCostStatus.costSubItemID, Equal<Required<ReadOnlyCostStatus.costSubItemID>>, And<INCostStatus.layerType, Equal<INLayerType.normal>>>>>, OrderBy<Asc<ReadOnlyCostStatus.receiptDate, Asc<ReadOnlyCostStatus.receiptNbr>>>>();
        if (item.ValMethod == "F" && fifoLayerNbr != null || this.IsIngoingTransfer(tran))
          bqlCommand = bqlCommand.WhereAnd<Where<ReadOnlyCostStatus.receiptNbr, Equal<Required<ReadOnlyCostStatus.receiptNbr>>>>();
        else if (fifoLayerNbr == null)
          bqlCommand = bqlCommand.WhereAnd<Where<ReadOnlyCostStatus.qtyOnHand, Greater<decimal0>>>();
        parameters = new object[4]
        {
          (object) split.InventoryID,
          (object) nullable,
          (object) split.CostSubItemID,
          (object) fifoLayerNbr
        };
        break;
      case "S":
        bqlCommand = (BqlCommand) new Select<ReadOnlyCostStatus, Where<ReadOnlyCostStatus.inventoryID, Equal<Required<ReadOnlyCostStatus.inventoryID>>, And<ReadOnlyCostStatus.costSiteID, Equal<Required<ReadOnlyCostStatus.costSiteID>>, And<ReadOnlyCostStatus.costSubItemID, Equal<Required<ReadOnlyCostStatus.costSubItemID>>, And<ReadOnlyCostStatus.lotSerialNbr, Equal<Required<ReadOnlyCostStatus.lotSerialNbr>>, And<INCostStatus.layerType, Equal<INLayerType.normal>>>>>>>();
        if (this.IsIngoingTransfer(tran))
          bqlCommand = bqlCommand.WhereAnd<Where<ReadOnlyCostStatus.receiptNbr, Equal<Required<ReadOnlyCostStatus.receiptNbr>>>>();
        parameters = new object[5]
        {
          (object) split.InventoryID,
          (object) nullable,
          (object) split.CostSubItemID,
          (object) split.LotSerialNbr,
          (object) (tran.OrigRefNbr ?? string.Empty)
        };
        break;
      default:
        throw new PXException();
    }
    return ((PXGraph) this).TypedViews.GetView(bqlCommand, false);
  }

  /// <summary>
  /// Switches the account/sub in the cost layer to another cost layer if the target cost layer does not exist or empty
  /// </summary>
  protected virtual INCostStatus SwitchLayerAccountSub(
    INCostStatus layer,
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    IEnumerable<INCostStatus> costStatuses)
  {
    short? invtMult = split.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num1 = -1;
    bool negQtyAdj = (nullable.GetValueOrDefault() == num1 & nullable.HasValue ? 1 : 0) != 0;
    int num2;
    if (split.TranType == "ADJ")
    {
      Decimal? baseQty = split.BaseQty;
      Decimal num3 = 0M;
      num2 = baseQty.GetValueOrDefault() == num3 & baseQty.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag = num2 != 0;
    if (negQtyAdj | flag && EnumerableExtensions.IsIn<string>(item.ValMethod, "A", "F", "S"))
    {
      IEnumerable<INCostStatus> source = costStatuses.Where<INCostStatus>((Func<INCostStatus, bool>) (s =>
      {
        if (!(s.LayerType == "N"))
          return false;
        Decimal? qtyOnHand = s.QtyOnHand;
        Decimal num4 = 0M;
        return qtyOnHand.GetValueOrDefault() > num4 & qtyOnHand.HasValue;
      }));
      if (source.FirstOrDefault<INCostStatus>((Func<INCostStatus, bool>) (s =>
      {
        int? accountId1 = s.AccountID;
        int? accountId2 = layer.AccountID;
        if (!(accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue))
          return false;
        int? subId1 = s.SubID;
        int? subId2 = layer.SubID;
        return subId1.GetValueOrDefault() == subId2.GetValueOrDefault() & subId1.HasValue == subId2.HasValue;
      })) == null)
      {
        INCostStatus layer1 = source.OrderByDescending<INCostStatus, Decimal?>((Func<INCostStatus, Decimal?>) (s => !negQtyAdj ? s.TotalCost : s.QtyOnHand)).FirstOrDefault<INCostStatus>();
        if (layer1 != null)
          return this.AccumulatedCostStatus(layer1, item, tran);
      }
    }
    return layer;
  }

  public virtual bool IsUnmanagedTran(INTran tran)
  {
    short? invtMult = tran.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    return nullable.GetValueOrDefault() == num & nullable.HasValue && tran.IsCostUnmanaged.GetValueOrDefault();
  }

  public virtual void ReceiveCost(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    bool correctImbalance)
  {
    short? invtMult1 = tran.InvtMult;
    int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
    int num1 = 1;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue && tran.TranType != "TRX")
    {
      short? invtMult2 = split.InvtMult;
      nullable1 = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
      int num2 = 1;
      if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue || item.ValMethod != "T" && !correctImbalance || item.ValMethod == "T" & correctImbalance)
        goto label_3;
    }
    short? invtMult3 = tran.InvtMult;
    nullable1 = invtMult3.HasValue ? new int?((int) invtMult3.GetValueOrDefault()) : new int?();
    int num3 = 0;
    if ((!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue) || this.IsUnmanagedTran(tran)) && (!tran.ExactCost.GetValueOrDefault() || correctImbalance))
      return;
label_3:
    INCostStatus layer = this.AccumulatedCostStatus(tran, split, item);
    PXView pxView = (PXView) null;
    bool flag = !((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsCorrection.GetValueOrDefault() && tran.TranType == "RCP";
    IEnumerable<INCostStatus> inCostStatuses;
    if (flag)
    {
      inCostStatuses = (IEnumerable<INCostStatus>) Array<INCostStatus>.Empty;
    }
    else
    {
      object[] parameters;
      pxView = this.GetCostStatusCommand(tran, split, item, out parameters, false, layer.ReceiptNbr);
      inCostStatuses = pxView.SelectMulti(parameters).Cast<INCostStatus>();
    }
    layer = this.SwitchLayerAccountSub(layer, tran, split, item, inCostStatuses);
    INCostStatus unmodifiedLayer = PXCache<INCostStatus>.CreateCopy(layer);
    INTranCost costtran = new INTranCost();
    costtran.InvtAcctID = layer.AccountID;
    costtran.InvtSubID = layer.SubID;
    costtran.COGSAcctID = tran.COGSAcctID;
    costtran.COGSSubID = tran.COGSSubID;
    costtran.CostID = layer.CostID;
    costtran.InventoryID = layer.InventoryID;
    costtran.CostSiteID = layer.CostSiteID;
    costtran.CostSubItemID = layer.CostSubItemID;
    costtran.DocType = tran.DocType;
    costtran.TranType = tran.TranType;
    costtran.RefNbr = tran.RefNbr;
    costtran.LineNbr = tran.LineNbr;
    costtran.CostDocType = tran.DocType;
    costtran.CostRefNbr = tran.RefNbr;
    costtran.SiteID = tran.SiteID;
    costtran.InvtMult = split.InvtMult;
    costtran.FinPeriodID = tran.FinPeriodID;
    costtran.TranPeriodID = tran.TranPeriodID;
    costtran.TranDate = tran.TranDate;
    costtran.TranAmt = new Decimal?(0M);
    INTranCost backupCostTran = ((PXSelectBase<INTranCost>) this.intrancost).Locate(costtran);
    backupCostTran = backupCostTran != null ? PXCache<INTranCost>.CreateCopy(backupCostTran) : (INTranCost) null;
    PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) costtran, typeof (INTran), (object) tran);
    INTranCost inTranCost1 = ((PXSelectBase<INTranCost>) this.intrancost).Insert(costtran);
    costtran = PXCache<INTranCost>.CreateCopy(inTranCost1);
    INTranCost inTranCost2 = costtran;
    Decimal? nullable2 = inTranCost2.Qty;
    Decimal? baseQty = split.BaseQty;
    inTranCost2.Qty = nullable2.HasValue & baseQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = split.BaseQty;
    Decimal num4 = 0M;
    if (nullable3.GetValueOrDefault() == num4 & nullable3.HasValue)
    {
      nullable3 = tran.BaseQty;
      Decimal num5 = 0M;
      if (nullable3.GetValueOrDefault() == num5 & nullable3.HasValue)
      {
        INTranCost inTranCost3 = costtran;
        nullable3 = inTranCost3.TranCost;
        nullable2 = tran.TranCost;
        inTranCost3.TranCost = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        goto label_15;
      }
    }
    if (this.UseStandardCost(item.ValMethod, tran))
    {
      INTranCost inTranCost4 = costtran;
      nullable2 = layer.UnitCost;
      Decimal num6 = nullable2.Value;
      nullable2 = costtran.Qty;
      Decimal num7 = nullable2.Value;
      Decimal? nullable4 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, num6 * num7));
      inTranCost4.TranCost = nullable4;
    }
    else
    {
      INTranCost inTranCost5 = costtran;
      nullable2 = inTranCost5.TranCost;
      nullable3 = tran.UnitCost;
      Decimal num8 = nullable3.Value;
      nullable3 = split.BaseQty;
      Decimal num9 = nullable3.Value;
      Decimal num10 = PXCurrencyAttribute.BaseRound((PXGraph) this, num8 * num9);
      Decimal? nullable5;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(nullable2.GetValueOrDefault() + num10);
      inTranCost5.TranCost = nullable5;
    }
label_15:
    INTranCost inTranCost6 = costtran;
    nullable2 = inTranCost6.TranAmt;
    nullable3 = split.BaseQty;
    Decimal num11 = nullable3.Value;
    nullable3 = tran.UnitPrice;
    Decimal num12 = nullable3.Value;
    Decimal num13 = PXCurrencyAttribute.BaseRound((PXGraph) this, num11 * num12);
    Decimal? nullable6;
    if (!nullable2.HasValue)
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = new Decimal?(nullable2.GetValueOrDefault() + num13);
    inTranCost6.TranAmt = nullable6;
    Decimal num14 = (Decimal) costtran.InvtMult.Value;
    nullable3 = costtran.Qty;
    Decimal? nullable7 = inTranCost1.Qty;
    nullable2 = nullable3.HasValue & nullable7.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable8;
    if (!nullable2.HasValue)
    {
      nullable7 = new Decimal?();
      nullable8 = nullable7;
    }
    else
      nullable8 = new Decimal?(num14 * nullable2.GetValueOrDefault());
    Decimal? nullable9 = nullable8;
    INCostStatus inCostStatus1 = layer;
    nullable2 = inCostStatus1.QtyOnHand;
    nullable7 = nullable9;
    Decimal? nullable10;
    if (!(nullable2.HasValue & nullable7.HasValue))
    {
      nullable3 = new Decimal?();
      nullable10 = nullable3;
    }
    else
      nullable10 = new Decimal?(nullable2.GetValueOrDefault() + nullable7.GetValueOrDefault());
    inCostStatus1.QtyOnHand = nullable10;
    INCostStatus inCostStatus2 = layer;
    nullable7 = inCostStatus2.PositiveTranQty;
    nullable3 = nullable9;
    Decimal num15 = 0M;
    nullable2 = nullable3.GetValueOrDefault() > num15 & nullable3.HasValue ? nullable9 : new Decimal?(0M);
    Decimal? nullable11;
    if (!(nullable7.HasValue & nullable2.HasValue))
    {
      nullable3 = new Decimal?();
      nullable11 = nullable3;
    }
    else
      nullable11 = new Decimal?(nullable7.GetValueOrDefault() + nullable2.GetValueOrDefault());
    inCostStatus2.PositiveTranQty = nullable11;
    INCostStatus inCostStatus3 = layer;
    nullable2 = inCostStatus3.TotalCost;
    short? invtMult4 = costtran.InvtMult;
    Decimal num16 = (Decimal) invtMult4.Value;
    Decimal? nullable12 = costtran.TranCost;
    Decimal? tranCost1 = inTranCost1.TranCost;
    nullable3 = nullable12.HasValue & tranCost1.HasValue ? new Decimal?(nullable12.GetValueOrDefault() - tranCost1.GetValueOrDefault()) : new Decimal?();
    nullable7 = nullable3.HasValue ? new Decimal?(num16 * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13;
    if (!(nullable2.HasValue & nullable7.HasValue))
    {
      nullable3 = new Decimal?();
      nullable13 = nullable3;
    }
    else
      nullable13 = new Decimal?(nullable2.GetValueOrDefault() + nullable7.GetValueOrDefault());
    inCostStatus3.TotalCost = nullable13;
    INCostStatus exactCostStatus = inCostStatuses.FirstOrDefault<INCostStatus>((Func<INCostStatus, bool>) (s =>
    {
      int? accountId1 = s.AccountID;
      int? accountId2 = layer.AccountID;
      if (!(accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue))
        return false;
      int? subId1 = s.SubID;
      int? subId2 = layer.SubID;
      return subId1.GetValueOrDefault() == subId2.GetValueOrDefault() & subId1.HasValue == subId2.HasValue;
    }));
    Action<INCostStatus> action = (Action<INCostStatus>) (backup =>
    {
      PXCache<INCostStatus>.RestoreCopy(layer, unmodifiedLayer);
      if (exactCostStatus != null && backup != null)
        PXCache<INCostStatus>.RestoreCopy(exactCostStatus, backup);
      if (backupCostTran == null)
        ((PXSelectBase<INTranCost>) this.intrancost).Delete(costtran);
      else
        PXCache<INTranCost>.RestoreCopy(((PXSelectBase<INTranCost>) this.intrancost).Locate(backupCostTran), backupCostTran);
    });
    if (exactCostStatus != null)
    {
      INCostStatus copy = PXCache<INCostStatus>.CreateCopy(exactCostStatus);
      INCostStatus inCostStatus4 = exactCostStatus;
      nullable7 = inCostStatus4.QtyOnHand;
      invtMult4 = costtran.InvtMult;
      Decimal num17 = (Decimal) invtMult4.Value;
      Decimal? qty = costtran.Qty;
      nullable12 = inTranCost1.Qty;
      nullable3 = qty.HasValue & nullable12.HasValue ? new Decimal?(qty.GetValueOrDefault() - nullable12.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable14;
      if (!nullable3.HasValue)
      {
        nullable12 = new Decimal?();
        nullable14 = nullable12;
      }
      else
        nullable14 = new Decimal?(num17 * nullable3.GetValueOrDefault());
      nullable2 = nullable14;
      Decimal? nullable15;
      if (!(nullable7.HasValue & nullable2.HasValue))
      {
        nullable3 = new Decimal?();
        nullable15 = nullable3;
      }
      else
        nullable15 = new Decimal?(nullable7.GetValueOrDefault() + nullable2.GetValueOrDefault());
      inCostStatus4.QtyOnHand = nullable15;
      INCostStatus inCostStatus5 = exactCostStatus;
      nullable2 = inCostStatus5.TotalCost;
      invtMult4 = costtran.InvtMult;
      Decimal num18 = (Decimal) invtMult4.Value;
      nullable12 = costtran.TranCost;
      Decimal? tranCost2 = inTranCost1.TranCost;
      nullable3 = nullable12.HasValue & tranCost2.HasValue ? new Decimal?(nullable12.GetValueOrDefault() - tranCost2.GetValueOrDefault()) : new Decimal?();
      nullable7 = nullable3.HasValue ? new Decimal?(num18 * nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable16;
      if (!(nullable2.HasValue & nullable7.HasValue))
      {
        nullable3 = new Decimal?();
        nullable16 = nullable3;
      }
      else
        nullable16 = new Decimal?(nullable2.GetValueOrDefault() + nullable7.GetValueOrDefault());
      inCostStatus5.TotalCost = nullable16;
      GraphHelper.MarkUpdated(pxView.Cache, (object) exactCostStatus);
      nullable7 = split.BaseQty;
      Decimal num19 = 0M;
      if (!(nullable7.GetValueOrDefault() == num19 & nullable7.HasValue))
      {
        nullable7 = tran.BaseQty;
        Decimal num20 = 0M;
        if (!(nullable7.GetValueOrDefault() == num20 & nullable7.HasValue))
        {
          nullable7 = exactCostStatus.QtyOnHand;
          Decimal num21 = 0M;
          if (nullable7.GetValueOrDefault() < num21 & nullable7.HasValue)
          {
            action(copy);
            throw new PXNegativeQtyImbalanceException();
          }
        }
      }
      nullable7 = split.BaseQty;
      Decimal num22 = 0M;
      if (!(nullable7.GetValueOrDefault() == num22 & nullable7.HasValue))
      {
        nullable7 = tran.BaseQty;
        Decimal num23 = 0M;
        if (!(nullable7.GetValueOrDefault() == num23 & nullable7.HasValue))
        {
          nullable7 = exactCostStatus.QtyOnHand;
          Decimal num24 = 0M;
          if (nullable7.GetValueOrDefault() == num24 & nullable7.HasValue)
          {
            nullable7 = exactCostStatus.TotalCost;
            Decimal num25 = 0M;
            if (!(nullable7.GetValueOrDefault() == num25 & nullable7.HasValue))
            {
              action(copy);
              throw new PXQtyCostImbalanceException();
            }
          }
        }
      }
    }
    else
    {
      invtMult4 = costtran.InvtMult;
      int? nullable17;
      if (!invtMult4.HasValue)
      {
        nullable1 = new int?();
        nullable17 = nullable1;
      }
      else
        nullable17 = new int?((int) invtMult4.GetValueOrDefault());
      nullable1 = nullable17;
      if (nullable1.GetValueOrDefault() == -1)
      {
        action((INCostStatus) null);
        throw new PXNegativeQtyImbalanceException();
      }
      invtMult4 = costtran.InvtMult;
      int? nullable18;
      if (!invtMult4.HasValue)
      {
        nullable1 = new int?();
        nullable18 = nullable1;
      }
      else
        nullable18 = new int?((int) invtMult4.GetValueOrDefault());
      nullable1 = nullable18;
      if (nullable1.GetValueOrDefault() == 1 && !flag)
        exactCostStatus = this.InsertArtificialCostLayer(pxView.Cache, layer);
    }
    Decimal? tranCost3 = costtran.TranCost;
    nullable7 = split.BaseQty;
    Decimal num26 = 0M;
    if (nullable7.GetValueOrDefault() == num26 & nullable7.HasValue)
    {
      nullable7 = tran.BaseQty;
      Decimal num27 = 0M;
      if (nullable7.GetValueOrDefault() == num27 & nullable7.HasValue)
      {
        PXCache<INTranCost>.RestoreCopy(inTranCost1, costtran);
        goto label_75;
      }
    }
    Decimal? nullable19;
    if (item.ValMethod != "T")
    {
      nullable12 = tran.CostedQty;
      nullable19 = costtran.Qty;
      nullable3 = nullable12.HasValue & nullable19.HasValue ? new Decimal?(nullable12.GetValueOrDefault() + nullable19.GetValueOrDefault()) : new Decimal?();
      Decimal? qty = inTranCost1.Qty;
      Decimal? nullable20;
      if (!(nullable3.HasValue & qty.HasValue))
      {
        nullable19 = new Decimal?();
        nullable20 = nullable19;
      }
      else
        nullable20 = new Decimal?(nullable3.GetValueOrDefault() - qty.GetValueOrDefault());
      nullable7 = nullable20;
      nullable2 = tran.UnitCost;
      Decimal num28 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable7.HasValue & nullable2.HasValue ? new Decimal?(nullable7.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
      nullable2 = tran.TranCost;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      Decimal num29 = num28 - valueOrDefault1;
      nullable2 = costtran.TranCost;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num30 = num29 - valueOrDefault2;
      nullable2 = inTranCost1.TranCost;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal num31;
      if ((num31 = num30 + valueOrDefault3) != 0M)
      {
        INTranCost inTranCost7 = costtran;
        nullable2 = inTranCost7.TranCost;
        Decimal num32 = num31;
        Decimal? nullable21;
        if (!nullable2.HasValue)
        {
          nullable7 = new Decimal?();
          nullable21 = nullable7;
        }
        else
          nullable21 = new Decimal?(nullable2.GetValueOrDefault() + num32);
        inTranCost7.TranCost = nullable21;
        INCostStatus inCostStatus6 = layer;
        nullable2 = inCostStatus6.TotalCost;
        invtMult4 = costtran.InvtMult;
        Decimal num33 = (Decimal) invtMult4.Value * num31;
        Decimal? nullable22;
        if (!nullable2.HasValue)
        {
          nullable7 = new Decimal?();
          nullable22 = nullable7;
        }
        else
          nullable22 = new Decimal?(nullable2.GetValueOrDefault() + num33);
        inCostStatus6.TotalCost = nullable22;
      }
    }
    costtran = ((PXSelectBase<INTranCost>) this.intrancost).Update(costtran);
label_75:
    nullable2 = tran.BaseQty;
    Decimal num34 = 0M;
    Decimal? nullable23;
    if (!(nullable2.GetValueOrDefault() == num34 & nullable2.HasValue))
    {
      nullable2 = tran.BaseQty;
      nullable7 = tran.CostedQty;
      if (nullable2.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable2.HasValue == nullable7.HasValue)
      {
        if (this.UseStandardCost(item.ValMethod, tran))
        {
          INTranCost inTranCost8 = costtran;
          nullable7 = inTranCost8.VarCost;
          Decimal? origTranCost = tran.OrigTranCost;
          nullable3 = tran.TranCost;
          Decimal? nullable24;
          if (!(origTranCost.HasValue & nullable3.HasValue))
          {
            nullable19 = new Decimal?();
            nullable24 = nullable19;
          }
          else
            nullable24 = new Decimal?(origTranCost.GetValueOrDefault() - nullable3.GetValueOrDefault());
          nullable2 = nullable24;
          Decimal? nullable25;
          if (!(nullable7.HasValue & nullable2.HasValue))
          {
            nullable3 = new Decimal?();
            nullable25 = nullable3;
          }
          else
            nullable25 = new Decimal?(nullable7.GetValueOrDefault() + nullable2.GetValueOrDefault());
          inTranCost8.VarCost = nullable25;
          tran.TranCost = tran.OrigTranCost;
          goto label_133;
        }
        INTranCost inTranCost9 = costtran;
        nullable2 = inTranCost9.TranCost;
        nullable3 = tran.OrigTranCost;
        nullable23 = tran.TranCost;
        Decimal? nullable26;
        if (!(nullable3.HasValue & nullable23.HasValue))
        {
          nullable19 = new Decimal?();
          nullable26 = nullable19;
        }
        else
          nullable26 = new Decimal?(nullable3.GetValueOrDefault() - nullable23.GetValueOrDefault());
        nullable7 = nullable26;
        Decimal? nullable27;
        if (!(nullable2.HasValue & nullable7.HasValue))
        {
          nullable23 = new Decimal?();
          nullable27 = nullable23;
        }
        else
          nullable27 = new Decimal?(nullable2.GetValueOrDefault() + nullable7.GetValueOrDefault());
        inTranCost9.TranCost = nullable27;
        INCostStatus inCostStatus7 = layer;
        nullable7 = inCostStatus7.TotalCost;
        invtMult4 = costtran.InvtMult;
        Decimal? nullable28;
        if (!invtMult4.HasValue)
        {
          nullable19 = new Decimal?();
          nullable28 = nullable19;
        }
        else
          nullable28 = new Decimal?((Decimal) invtMult4.GetValueOrDefault());
        nullable23 = nullable28;
        nullable19 = tran.OrigTranCost;
        nullable12 = tran.TranCost;
        nullable3 = nullable19.HasValue & nullable12.HasValue ? new Decimal?(nullable19.GetValueOrDefault() - nullable12.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable29;
        if (!(nullable23.HasValue & nullable3.HasValue))
        {
          nullable12 = new Decimal?();
          nullable29 = nullable12;
        }
        else
          nullable29 = new Decimal?(nullable23.GetValueOrDefault() * nullable3.GetValueOrDefault());
        nullable2 = nullable29;
        Decimal? nullable30;
        if (!(nullable7.HasValue & nullable2.HasValue))
        {
          nullable3 = new Decimal?();
          nullable30 = nullable3;
        }
        else
          nullable30 = new Decimal?(nullable7.GetValueOrDefault() + nullable2.GetValueOrDefault());
        inCostStatus7.TotalCost = nullable30;
        tran.TranCost = tran.OrigTranCost;
        goto label_133;
      }
    }
    nullable2 = tran.BaseQty;
    Decimal num35 = 0M;
    if (!(nullable2.GetValueOrDefault() == num35 & nullable2.HasValue))
    {
      nullable2 = tran.BaseQty;
      nullable3 = tran.CostedQty;
      nullable7 = nullable3.HasValue ? new Decimal?(-nullable3.GetValueOrDefault()) : new Decimal?();
      if (nullable2.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable2.HasValue == nullable7.HasValue)
      {
        if (this.UseStandardCost(item.ValMethod, tran))
        {
          INTranCost inTranCost10 = costtran;
          nullable7 = inTranCost10.VarCost;
          Decimal num36 = -1M;
          nullable12 = tran.OrigTranCost;
          Decimal? nullable31;
          if (!nullable12.HasValue)
          {
            nullable19 = new Decimal?();
            nullable31 = nullable19;
          }
          else
            nullable31 = new Decimal?(num36 * nullable12.GetValueOrDefault());
          nullable3 = nullable31;
          Decimal? tranCost4 = tran.TranCost;
          Decimal? nullable32;
          if (!(nullable3.HasValue & tranCost4.HasValue))
          {
            nullable12 = new Decimal?();
            nullable32 = nullable12;
          }
          else
            nullable32 = new Decimal?(nullable3.GetValueOrDefault() - tranCost4.GetValueOrDefault());
          nullable2 = nullable32;
          inTranCost10.VarCost = nullable7.HasValue & nullable2.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          tran.TranCost = tran.OrigTranCost;
        }
        else
        {
          INTranCost inTranCost11 = costtran;
          nullable2 = inTranCost11.TranCost;
          Decimal num37 = -1M;
          nullable12 = tran.OrigTranCost;
          Decimal? nullable33;
          if (!nullable12.HasValue)
          {
            nullable19 = new Decimal?();
            nullable33 = nullable19;
          }
          else
            nullable33 = new Decimal?(num37 * nullable12.GetValueOrDefault());
          nullable23 = nullable33;
          nullable3 = tran.TranCost;
          Decimal? nullable34;
          if (!(nullable23.HasValue & nullable3.HasValue))
          {
            nullable12 = new Decimal?();
            nullable34 = nullable12;
          }
          else
            nullable34 = new Decimal?(nullable23.GetValueOrDefault() - nullable3.GetValueOrDefault());
          nullable7 = nullable34;
          Decimal? nullable35;
          if (!(nullable2.HasValue & nullable7.HasValue))
          {
            nullable3 = new Decimal?();
            nullable35 = nullable3;
          }
          else
            nullable35 = new Decimal?(nullable2.GetValueOrDefault() + nullable7.GetValueOrDefault());
          inTranCost11.TranCost = nullable35;
          INCostStatus inCostStatus8 = layer;
          nullable7 = inCostStatus8.TotalCost;
          invtMult4 = costtran.InvtMult;
          Decimal? nullable36;
          if (!invtMult4.HasValue)
          {
            nullable12 = new Decimal?();
            nullable36 = nullable12;
          }
          else
            nullable36 = new Decimal?((Decimal) invtMult4.GetValueOrDefault());
          nullable3 = nullable36;
          Decimal num38 = -1M;
          Decimal? nullable37 = tran.OrigTranCost;
          nullable12 = nullable37.HasValue ? new Decimal?(num38 * nullable37.GetValueOrDefault()) : new Decimal?();
          nullable19 = tran.TranCost;
          Decimal? nullable38;
          if (!(nullable12.HasValue & nullable19.HasValue))
          {
            nullable37 = new Decimal?();
            nullable38 = nullable37;
          }
          else
            nullable38 = new Decimal?(nullable12.GetValueOrDefault() - nullable19.GetValueOrDefault());
          nullable23 = nullable38;
          Decimal? nullable39;
          if (!(nullable3.HasValue & nullable23.HasValue))
          {
            nullable19 = new Decimal?();
            nullable39 = nullable19;
          }
          else
            nullable39 = new Decimal?(nullable3.GetValueOrDefault() * nullable23.GetValueOrDefault());
          nullable2 = nullable39;
          Decimal? nullable40;
          if (!(nullable7.HasValue & nullable2.HasValue))
          {
            nullable23 = new Decimal?();
            nullable40 = nullable23;
          }
          else
            nullable40 = new Decimal?(nullable7.GetValueOrDefault() + nullable2.GetValueOrDefault());
          inCostStatus8.TotalCost = nullable40;
          tran.TranCost = tran.OrigTranCost;
        }
      }
    }
label_133:
    nullable2 = tran.BaseQty;
    Decimal num39 = 0M;
    if (!(nullable2.GetValueOrDefault() == num39 & nullable2.HasValue))
    {
      nullable2 = tran.BaseQty;
      nullable7 = tran.CostedQty;
      if (!(nullable2.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable2.HasValue == nullable7.HasValue))
      {
        nullable7 = tran.BaseQty;
        nullable23 = tran.CostedQty;
        Decimal? nullable41;
        if (!nullable23.HasValue)
        {
          nullable3 = new Decimal?();
          nullable41 = nullable3;
        }
        else
          nullable41 = new Decimal?(-nullable23.GetValueOrDefault());
        nullable2 = nullable41;
        if (!(nullable7.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable7.HasValue == nullable2.HasValue))
          goto label_146;
      }
      INTranCost inTranCost12 = costtran;
      nullable2 = inTranCost12.TranAmt;
      nullable23 = tran.OrigTranAmt;
      nullable3 = tran.TranAmt;
      Decimal? nullable42;
      if (!(nullable23.HasValue & nullable3.HasValue))
      {
        nullable19 = new Decimal?();
        nullable42 = nullable19;
      }
      else
        nullable42 = new Decimal?(nullable23.GetValueOrDefault() - nullable3.GetValueOrDefault());
      nullable7 = nullable42;
      Decimal? nullable43;
      if (!(nullable2.HasValue & nullable7.HasValue))
      {
        nullable3 = new Decimal?();
        nullable43 = nullable3;
      }
      else
        nullable43 = new Decimal?(nullable2.GetValueOrDefault() + nullable7.GetValueOrDefault());
      inTranCost12.TranAmt = nullable43;
      tran.TranAmt = tran.OrigTranAmt;
    }
label_146:
    if (exactCostStatus == null || !EnumerableExtensions.IsIn<string>(item.ValMethod, "S", "A", "F"))
      return;
    INCostStatus inCostStatus9 = exactCostStatus;
    nullable7 = inCostStatus9.TotalCost;
    invtMult4 = costtran.InvtMult;
    Decimal num40 = (Decimal) invtMult4.Value;
    nullable23 = costtran.TranCost;
    nullable19 = tranCost3;
    Decimal? nullable44;
    if (!(nullable23.HasValue & nullable19.HasValue))
    {
      nullable12 = new Decimal?();
      nullable44 = nullable12;
    }
    else
      nullable44 = new Decimal?(nullable23.GetValueOrDefault() - nullable19.GetValueOrDefault());
    nullable3 = nullable44;
    Decimal? nullable45;
    if (!nullable3.HasValue)
    {
      nullable19 = new Decimal?();
      nullable45 = nullable19;
    }
    else
      nullable45 = new Decimal?(num40 * nullable3.GetValueOrDefault());
    nullable2 = nullable45;
    Decimal? nullable46;
    if (!(nullable7.HasValue & nullable2.HasValue))
    {
      nullable3 = new Decimal?();
      nullable46 = nullable3;
    }
    else
      nullable46 = new Decimal?(nullable7.GetValueOrDefault() + nullable2.GetValueOrDefault());
    inCostStatus9.TotalCost = nullable46;
  }

  protected virtual INCostStatus InsertArtificialCostLayer(PXCache cache, INCostStatus layer)
  {
    INCostStatus instance = (INCostStatus) cache.CreateInstance();
    instance.AccountID = layer.AccountID;
    instance.SubID = layer.SubID;
    instance.InventoryID = layer.InventoryID;
    instance.CostSiteID = layer.CostSiteID;
    instance.SiteID = layer.SiteID;
    instance.CostSubItemID = layer.CostSubItemID;
    instance.LotSerialNbr = layer.LotSerialNbr;
    instance.ReceiptDate = layer.ReceiptDate;
    instance.ReceiptNbr = layer.ReceiptNbr;
    instance.LayerType = layer.LayerType;
    instance.ValMethod = layer.ValMethod;
    instance.QtyOnHand = layer.QtyOnHand;
    instance.UnitCost = layer.UnitCost;
    instance.TotalCost = layer.TotalCost;
    return (INCostStatus) cache.Insert((object) instance);
  }

  public virtual void DropshipCost(INTran tran, INTranSplit split, PX.Objects.IN.InventoryItem item)
  {
    if (!this.IsUnmanagedTran(tran))
      return;
    INCostStatus inCostStatus = this.AccumUnmanagedCostStatus(tran, split, item);
    INTranCost copy = PXCache<INTranCost>.CreateCopy(((PXSelectBase<INTranCost>) this.intrancost).Insert(new INTranCost()
    {
      InvtAcctID = inCostStatus.AccountID,
      InvtSubID = inCostStatus.SubID,
      COGSAcctID = tran.COGSAcctID,
      COGSSubID = tran.COGSSubID,
      CostID = inCostStatus.CostID,
      InventoryID = inCostStatus.InventoryID,
      CostSiteID = inCostStatus.CostSiteID,
      CostSubItemID = inCostStatus.CostSubItemID,
      DocType = tran.DocType,
      TranType = tran.TranType,
      RefNbr = tran.RefNbr,
      LineNbr = tran.LineNbr,
      CostDocType = tran.DocType,
      CostRefNbr = tran.RefNbr,
      InvtMult = split.InvtMult,
      FinPeriodID = tran.FinPeriodID,
      TranPeriodID = tran.TranPeriodID,
      TranDate = tran.TranDate,
      SiteID = tran.SiteID,
      TranAmt = new Decimal?(0M),
      IsVirtual = new bool?(true)
    }));
    PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) copy, typeof (INTran), (object) tran);
    INTranCost inTranCost1 = copy;
    Decimal? qty = inTranCost1.Qty;
    Decimal? baseQty = split.BaseQty;
    inTranCost1.Qty = qty.HasValue & baseQty.HasValue ? new Decimal?(qty.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable1 = split.Qty;
    INTranCost inTranCost2;
    if (nullable1.GetValueOrDefault() != 0M)
    {
      copy.CostType = "D";
      INTranCost inTranCost3 = copy;
      nullable1 = inTranCost3.TranCost;
      Decimal? origTranCost = tran.OrigTranCost;
      Decimal? nullable2 = tran.BaseQty;
      Decimal num1 = PXCurrencyAttribute.BaseRound((PXGraph) this, (origTranCost.HasValue & nullable2.HasValue ? new Decimal?(origTranCost.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?()).Value * split.BaseQty.Value);
      inTranCost3.TranCost = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
      INTranCost inTranCost4 = copy;
      Decimal? tranAmt = inTranCost4.TranAmt;
      nullable2 = split.BaseQty;
      Decimal num2 = nullable2.Value;
      nullable2 = tran.OrigTranAmt;
      nullable1 = tran.BaseQty;
      Decimal num3 = (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?()).Value;
      Decimal num4 = PXCurrencyAttribute.BaseRound((PXGraph) this, num2 * num3);
      inTranCost4.TranAmt = tranAmt.HasValue ? new Decimal?(tranAmt.GetValueOrDefault() + num4) : new Decimal?();
      inTranCost2 = ((PXSelectBase<INTranCost>) this.intrancost).Update(copy);
    }
    else
    {
      if (!(tran.TranType == "ADJ"))
        return;
      copy.CostType = "P";
      copy.TranCost = tran.TranCost;
      copy.TranAmt = tran.TranAmt;
      inTranCost2 = ((PXSelectBase<INTranCost>) this.intrancost).Update(copy);
    }
  }

  public void ReceiveOverSold<TLayer, InventoryID, CostSubItemID, CostSiteID>(PX.Objects.IN.INRegister doc)
    where TLayer : INCostStatus
    where InventoryID : IBqlField
    where CostSubItemID : IBqlField
    where CostSiteID : IBqlField
  {
    foreach (TLayer layer in ((PXGraph) this).Caches[typeof (TLayer)].Inserted)
    {
      Decimal? qtyOnHand1 = layer.QtyOnHand;
      Decimal num1 = 0M;
      if (qtyOnHand1.GetValueOrDefault() > num1 & qtyOnHand1.HasValue)
      {
        foreach (PXResult<INTranCost, INTran, ReadOnlyCostStatus> pxResult in PXSelectBase<INTranCost, PXSelectReadonly2<INTranCost, InnerJoin<INTran, On2<INTranCost.FK.Tran, And<INTran.docType, Equal<INTranCost.costDocType>, And<INTran.refNbr, Equal<INTranCost.costRefNbr>>>>, InnerJoin<ReadOnlyCostStatus, On<ReadOnlyCostStatus.costID, Equal<INTranCost.costID>, And<ReadOnlyCostStatus.layerType, Equal<INLayerType.oversold>>>>>, Where<INTranCost.inventoryID, Equal<Current<InventoryID>>, And<INTranCost.costSubItemID, Equal<Current<CostSubItemID>>, And<INTranCost.costSiteID, Equal<Current<CostSiteID>>, And<INTranCost.isOversold, Equal<True>, And<INTranCost.oversoldQty, Greater<decimal0>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) layer
        }, Array.Empty<object>()))
        {
          INTranCost inTranCost1 = PXResult<INTranCost, INTran, ReadOnlyCostStatus>.op_Implicit(pxResult);
          INTran inTran = PXResult<INTranCost, INTran, ReadOnlyCostStatus>.op_Implicit(pxResult);
          INTranCost copy1 = PXCache<INTranCost>.CreateCopy(inTranCost1);
          copy1.CostDocType = doc.DocType;
          copy1.CostRefNbr = doc.RefNbr;
          INTranCostUpdate inTranCostUpdate1 = ((PXSelectBase<INTranCostUpdate>) this.intrancostupdate).Insert(new INTranCostUpdate()
          {
            DocType = copy1.DocType,
            RefNbr = copy1.RefNbr,
            LineNbr = copy1.LineNbr,
            CostID = copy1.CostID,
            CostDocType = inTran.DocType,
            CostRefNbr = inTran.RefNbr,
            ValMethod = PXResult<INTranCost, INTran, ReadOnlyCostStatus>.op_Implicit(pxResult).ValMethod
          });
          INTranCost inTranCost2 = copy1;
          Decimal? nullable1 = inTranCost2.OversoldQty;
          Decimal? oversoldQty = inTranCostUpdate1.OversoldQty;
          inTranCost2.OversoldQty = nullable1.HasValue & oversoldQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + oversoldQty.GetValueOrDefault()) : new Decimal?();
          INTranCost inTranCost3 = copy1;
          Decimal? nullable2 = inTranCost3.OversoldTranCost;
          nullable1 = inTranCostUpdate1.OversoldTranCost;
          inTranCost3.OversoldTranCost = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          nullable1 = copy1.OversoldQty;
          Decimal num2 = 0M;
          if (!(nullable1.GetValueOrDefault() <= num2 & nullable1.HasValue))
          {
            INCostStatus inCostStatus1 = this.AccumOversoldCostStatus((INCostStatus) PXResult<INTranCost, INTran, ReadOnlyCostStatus>.op_Implicit(pxResult));
            nullable1 = layer.QtyOnHand;
            Decimal num3 = 0M;
            if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
            {
              // ISSUE: variable of a boxed type
              __Boxed<TLayer> local = (object) layer;
              nullable1 = layer.TotalCost;
              nullable2 = layer.QtyOnHand;
              Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
              local.AvgCost = nullable3;
            }
            nullable1 = PXResult<INTranCost, INTran, ReadOnlyCostStatus>.op_Implicit(pxResult).QtyOnHand;
            Decimal? qtyOnHand2 = inCostStatus1.QtyOnHand;
            nullable2 = nullable1.HasValue & qtyOnHand2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + qtyOnHand2.GetValueOrDefault()) : new Decimal?();
            Decimal num4 = 0M;
            if (!(nullable2.GetValueOrDefault() == num4 & nullable2.HasValue))
            {
              INCostStatus inCostStatus2 = inCostStatus1;
              nullable1 = PXResult<INTranCost, INTran, ReadOnlyCostStatus>.op_Implicit(pxResult).TotalCost;
              Decimal? totalCost = inCostStatus1.TotalCost;
              nullable2 = nullable1.HasValue & totalCost.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + totalCost.GetValueOrDefault()) : new Decimal?();
              Decimal? qtyOnHand3 = PXResult<INTranCost, INTran, ReadOnlyCostStatus>.op_Implicit(pxResult).QtyOnHand;
              nullable1 = inCostStatus1.QtyOnHand;
              Decimal? nullable4 = qtyOnHand3.HasValue & nullable1.HasValue ? new Decimal?(qtyOnHand3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable5;
              if (!(nullable2.HasValue & nullable4.HasValue))
              {
                nullable1 = new Decimal?();
                nullable5 = nullable1;
              }
              else
                nullable5 = new Decimal?(nullable2.GetValueOrDefault() / nullable4.GetValueOrDefault());
              inCostStatus2.AvgCost = nullable5;
            }
            Decimal? nullable6 = copy1.OversoldQty;
            nullable2 = layer.QtyOnHand;
            if (nullable6.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable6.HasValue & nullable2.HasValue)
            {
              INTranCost copy2 = PXCache<INTranCost>.CreateCopy(copy1);
              copy2.TranDate = doc.TranDate;
              copy2.TranPeriodID = doc.TranPeriodID;
              copy2.FinPeriodID = doc.FinPeriodID;
              copy2.CostDocType = doc.DocType;
              copy2.CostRefNbr = doc.RefNbr;
              copy2.TranAmt = new Decimal?(0M);
              copy2.Qty = new Decimal?(0M);
              copy2.OversoldQty = new Decimal?(0M);
              copy2.OversoldTranCost = new Decimal?(0M);
              copy2.TranCost = new Decimal?(0M);
              copy2.VarCost = new Decimal?(0M);
              PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) copy2, typeof (INTran), (object) inTran);
              INTranCost inTranCost4 = ((PXSelectBase<INTranCost>) this.intrancost).Insert(copy2);
              INTranCost copy3 = PXCache<INTranCost>.CreateCopy(inTranCost4);
              copy3.IsOversold = new bool?(false);
              INTranCost inTranCost5 = copy3;
              nullable2 = inTranCost5.Qty;
              nullable6 = copy1.OversoldQty;
              Decimal? nullable7;
              if (!(nullable2.HasValue & nullable6.HasValue))
              {
                nullable1 = new Decimal?();
                nullable7 = nullable1;
              }
              else
                nullable7 = new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault());
              inTranCost5.Qty = nullable7;
              if (inCostStatus1.ValMethod == "T")
              {
                INTranCost inTranCost6 = copy3;
                nullable6 = copy3.Qty;
                nullable2 = inCostStatus1.AvgCost;
                Decimal? nullable8;
                if (!(nullable6.HasValue & nullable2.HasValue))
                {
                  nullable1 = new Decimal?();
                  nullable8 = nullable1;
                }
                else
                  nullable8 = new Decimal?(nullable6.GetValueOrDefault() * nullable2.GetValueOrDefault());
                Decimal? nullable9 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, nullable8));
                inTranCost6.TranCost = nullable9;
                INTranCost inTranCost7 = copy3;
                nullable2 = copy3.Qty;
                nullable6 = inCostStatus1.AvgCost;
                Decimal? nullable10;
                if (!(nullable2.HasValue & nullable6.HasValue))
                {
                  nullable1 = new Decimal?();
                  nullable10 = nullable1;
                }
                else
                  nullable10 = new Decimal?(nullable2.GetValueOrDefault() * nullable6.GetValueOrDefault());
                Decimal num5 = -PXCurrencyAttribute.BaseRound((PXGraph) this, nullable10);
                nullable6 = copy3.Qty;
                nullable2 = inCostStatus1.UnitCost;
                Decimal? nullable11;
                if (!(nullable6.HasValue & nullable2.HasValue))
                {
                  nullable1 = new Decimal?();
                  nullable11 = nullable1;
                }
                else
                  nullable11 = new Decimal?(nullable6.GetValueOrDefault() * nullable2.GetValueOrDefault());
                Decimal num6 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable11);
                Decimal? nullable12 = new Decimal?(num5 + num6);
                inTranCost7.VarCost = nullable12;
              }
              else
              {
                INTranCost inTranCost8 = copy3;
                nullable2 = inTranCost8.TranCost;
                nullable6 = copy1.OversoldTranCost;
                Decimal? nullable13;
                if (!(nullable2.HasValue & nullable6.HasValue))
                {
                  nullable1 = new Decimal?();
                  nullable13 = nullable1;
                }
                else
                  nullable13 = new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault());
                inTranCost8.TranCost = nullable13;
              }
              nullable1 = copy3.TranCost;
              nullable6 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
              nullable2 = inTranCost4.TranCost;
              Decimal? nullable14;
              if (!(nullable6.HasValue & nullable2.HasValue))
              {
                nullable1 = new Decimal?();
                nullable14 = nullable1;
              }
              else
                nullable14 = new Decimal?(nullable6.GetValueOrDefault() + nullable2.GetValueOrDefault());
              Decimal? nullable15 = nullable14;
              nullable1 = copy3.Qty;
              nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
              nullable6 = inTranCost4.Qty;
              Decimal? nullable16;
              if (!(nullable2.HasValue & nullable6.HasValue))
              {
                nullable1 = new Decimal?();
                nullable16 = nullable1;
              }
              else
                nullable16 = new Decimal?(nullable2.GetValueOrDefault() + nullable6.GetValueOrDefault());
              Decimal? nullable17 = nullable16;
              INCostStatus inCostStatus3 = inCostStatus1;
              nullable6 = inCostStatus3.TotalCost;
              nullable2 = nullable15;
              Decimal? nullable18;
              if (!(nullable6.HasValue & nullable2.HasValue))
              {
                nullable1 = new Decimal?();
                nullable18 = nullable1;
              }
              else
                nullable18 = new Decimal?(nullable6.GetValueOrDefault() + nullable2.GetValueOrDefault());
              inCostStatus3.TotalCost = nullable18;
              INCostStatus inCostStatus4 = inCostStatus1;
              nullable2 = inCostStatus4.QtyOnHand;
              nullable6 = nullable17;
              Decimal? nullable19;
              if (!(nullable2.HasValue & nullable6.HasValue))
              {
                nullable1 = new Decimal?();
                nullable19 = nullable1;
              }
              else
                nullable19 = new Decimal?(nullable2.GetValueOrDefault() + nullable6.GetValueOrDefault());
              inCostStatus4.QtyOnHand = nullable19;
              INTranCostUpdate inTranCostUpdate2 = inTranCostUpdate1;
              nullable6 = inTranCostUpdate2.OversoldQty;
              nullable2 = nullable17;
              Decimal? nullable20;
              if (!(nullable6.HasValue & nullable2.HasValue))
              {
                nullable1 = new Decimal?();
                nullable20 = nullable1;
              }
              else
                nullable20 = new Decimal?(nullable6.GetValueOrDefault() - nullable2.GetValueOrDefault());
              inTranCostUpdate2.OversoldQty = nullable20;
              INTranCostUpdate inTranCostUpdate3 = inTranCostUpdate1;
              nullable2 = inTranCostUpdate3.OversoldTranCost;
              nullable6 = nullable15;
              Decimal? nullable21;
              if (!(nullable2.HasValue & nullable6.HasValue))
              {
                nullable1 = new Decimal?();
                nullable21 = nullable1;
              }
              else
                nullable21 = new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault());
              inTranCostUpdate3.OversoldTranCost = nullable21;
              inTranCostUpdate1.ResetOversoldFlag = new bool?(true);
              ((PXSelectBase<INTranCost>) this.intrancost).Update(copy3);
              INTranCost copy4 = PXCache<INTranCost>.CreateCopy(copy1);
              copy4.IsOversold = new bool?(false);
              copy4.CostID = layer.CostID;
              copy4.InvtAcctID = layer.AccountID;
              copy4.InvtSubID = layer.SubID;
              copy4.TranDate = doc.TranDate;
              copy4.TranPeriodID = doc.TranPeriodID;
              copy4.FinPeriodID = doc.FinPeriodID;
              copy4.Qty = copy1.OversoldQty;
              copy4.OversoldQty = new Decimal?(0M);
              copy4.OversoldTranCost = new Decimal?(0M);
              INTranCost inTranCost9 = copy4;
              nullable6 = copy4.Qty;
              nullable2 = layer.AvgCost;
              Decimal? nullable22;
              if (!(nullable6.HasValue & nullable2.HasValue))
              {
                nullable1 = new Decimal?();
                nullable22 = nullable1;
              }
              else
                nullable22 = new Decimal?(nullable6.GetValueOrDefault() * nullable2.GetValueOrDefault());
              Decimal? nullable23 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, nullable22));
              inTranCost9.TranCost = nullable23;
              if (layer.ValMethod == "T")
              {
                INTranCost inTranCost10 = copy4;
                nullable2 = copy4.Qty;
                nullable6 = layer.AvgCost;
                Decimal? nullable24;
                if (!(nullable2.HasValue & nullable6.HasValue))
                {
                  nullable1 = new Decimal?();
                  nullable24 = nullable1;
                }
                else
                  nullable24 = new Decimal?(nullable2.GetValueOrDefault() * nullable6.GetValueOrDefault());
                Decimal num7 = -PXCurrencyAttribute.BaseRound((PXGraph) this, nullable24);
                nullable6 = copy4.Qty;
                nullable2 = layer.UnitCost;
                Decimal? nullable25;
                if (!(nullable6.HasValue & nullable2.HasValue))
                {
                  nullable1 = new Decimal?();
                  nullable25 = nullable1;
                }
                else
                  nullable25 = new Decimal?(nullable6.GetValueOrDefault() * nullable2.GetValueOrDefault());
                Decimal num8 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable25);
                Decimal? nullable26 = new Decimal?(num7 + num8);
                inTranCost10.VarCost = nullable26;
              }
              copy4.TranAmt = new Decimal?(0M);
              copy4.CostDocType = doc.DocType;
              copy4.CostRefNbr = doc.RefNbr;
              PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) copy4, typeof (INTran), (object) inTran);
              ((PXSelectBase) this.intrancost).Cache.Insert((object) copy4);
              ref TLayer local1 = ref layer;
              // ISSUE: variable of a boxed type
              __Boxed<TLayer> local2 = (object) local1;
              nullable2 = local1.TotalCost;
              nullable6 = copy1.OversoldQty;
              nullable1 = layer.AvgCost;
              Decimal num9 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable6.HasValue & nullable1.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?());
              Decimal? nullable27;
              if (!nullable2.HasValue)
              {
                nullable1 = new Decimal?();
                nullable27 = nullable1;
              }
              else
                nullable27 = new Decimal?(nullable2.GetValueOrDefault() - num9);
              local2.TotalCost = nullable27;
              ref TLayer local3 = ref layer;
              // ISSUE: variable of a boxed type
              __Boxed<TLayer> local4 = (object) local3;
              nullable2 = local3.QtyOnHand;
              nullable1 = copy1.OversoldQty;
              Decimal? nullable28;
              if (!(nullable2.HasValue & nullable1.HasValue))
              {
                nullable6 = new Decimal?();
                nullable28 = nullable6;
              }
              else
                nullable28 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
              local4.QtyOnHand = nullable28;
            }
            else
            {
              nullable1 = layer.QtyOnHand;
              Decimal num10 = 0M;
              if (nullable1.GetValueOrDefault() > num10 & nullable1.HasValue)
              {
                INTranCost copy5 = PXCache<INTranCost>.CreateCopy(copy1);
                copy5.IsOversold = new bool?(true);
                copy5.TranDate = doc.TranDate;
                copy5.TranPeriodID = doc.TranPeriodID;
                copy5.FinPeriodID = doc.FinPeriodID;
                copy5.CostDocType = doc.DocType;
                copy5.CostRefNbr = doc.RefNbr;
                copy5.TranAmt = new Decimal?(0M);
                copy5.Qty = new Decimal?(0M);
                copy5.OversoldQty = new Decimal?(0M);
                copy5.OversoldTranCost = new Decimal?(0M);
                copy5.TranCost = new Decimal?(0M);
                copy5.VarCost = new Decimal?(0M);
                PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) copy5, typeof (INTran), (object) inTran);
                INTranCost inTranCost11 = ((PXSelectBase<INTranCost>) this.intrancost).Insert(copy5);
                INTranCost copy6 = PXCache<INTranCost>.CreateCopy(inTranCost11);
                INTranCost inTranCost12 = copy6;
                nullable1 = inTranCost12.Qty;
                nullable2 = layer.QtyOnHand;
                Decimal? nullable29;
                if (!(nullable1.HasValue & nullable2.HasValue))
                {
                  nullable6 = new Decimal?();
                  nullable29 = nullable6;
                }
                else
                  nullable29 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
                inTranCost12.Qty = nullable29;
                if (inCostStatus1.ValMethod == "T")
                {
                  INTranCost inTranCost13 = copy6;
                  nullable2 = copy6.Qty;
                  nullable1 = inCostStatus1.AvgCost;
                  Decimal? nullable30;
                  if (!(nullable2.HasValue & nullable1.HasValue))
                  {
                    nullable6 = new Decimal?();
                    nullable30 = nullable6;
                  }
                  else
                    nullable30 = new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault());
                  Decimal? nullable31 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, nullable30));
                  inTranCost13.TranCost = nullable31;
                  INTranCost inTranCost14 = copy6;
                  nullable1 = copy6.Qty;
                  nullable2 = inCostStatus1.UnitCost;
                  Decimal? nullable32;
                  if (!(nullable1.HasValue & nullable2.HasValue))
                  {
                    nullable6 = new Decimal?();
                    nullable32 = nullable6;
                  }
                  else
                    nullable32 = new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault());
                  Decimal num11 = -PXCurrencyAttribute.BaseRound((PXGraph) this, nullable32);
                  nullable2 = copy6.Qty;
                  nullable1 = inCostStatus1.AvgCost;
                  Decimal? nullable33;
                  if (!(nullable2.HasValue & nullable1.HasValue))
                  {
                    nullable6 = new Decimal?();
                    nullable33 = nullable6;
                  }
                  else
                    nullable33 = new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault());
                  Decimal num12 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable33);
                  Decimal? nullable34 = new Decimal?(num11 + num12);
                  inTranCost14.VarCost = nullable34;
                }
                else
                {
                  INTranCost inTranCost15 = copy6;
                  nullable6 = copy6.Qty;
                  Decimal? oversoldTranCost = copy1.OversoldTranCost;
                  nullable1 = nullable6.HasValue & oversoldTranCost.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * oversoldTranCost.GetValueOrDefault()) : new Decimal?();
                  nullable2 = copy1.OversoldQty;
                  Decimal? nullable35 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?()));
                  inTranCost15.TranCost = nullable35;
                }
                Decimal? nullable36 = copy6.TranCost;
                Decimal? nullable37;
                if (!nullable36.HasValue)
                {
                  nullable6 = new Decimal?();
                  nullable37 = nullable6;
                }
                else
                  nullable37 = new Decimal?(-nullable36.GetValueOrDefault());
                nullable2 = nullable37;
                nullable1 = inTranCost11.TranCost;
                Decimal? nullable38;
                if (!(nullable2.HasValue & nullable1.HasValue))
                {
                  nullable36 = new Decimal?();
                  nullable38 = nullable36;
                }
                else
                  nullable38 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
                Decimal? nullable39 = nullable38;
                nullable36 = copy6.Qty;
                Decimal? nullable40;
                if (!nullable36.HasValue)
                {
                  nullable6 = new Decimal?();
                  nullable40 = nullable6;
                }
                else
                  nullable40 = new Decimal?(-nullable36.GetValueOrDefault());
                nullable1 = nullable40;
                nullable2 = inTranCost11.Qty;
                Decimal? nullable41;
                if (!(nullable1.HasValue & nullable2.HasValue))
                {
                  nullable36 = new Decimal?();
                  nullable41 = nullable36;
                }
                else
                  nullable41 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
                Decimal? nullable42 = nullable41;
                INCostStatus inCostStatus5 = inCostStatus1;
                nullable2 = inCostStatus5.TotalCost;
                nullable1 = nullable39;
                Decimal? nullable43;
                if (!(nullable2.HasValue & nullable1.HasValue))
                {
                  nullable36 = new Decimal?();
                  nullable43 = nullable36;
                }
                else
                  nullable43 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
                inCostStatus5.TotalCost = nullable43;
                INCostStatus inCostStatus6 = inCostStatus1;
                nullable1 = inCostStatus6.QtyOnHand;
                nullable2 = nullable42;
                Decimal? nullable44;
                if (!(nullable1.HasValue & nullable2.HasValue))
                {
                  nullable36 = new Decimal?();
                  nullable44 = nullable36;
                }
                else
                  nullable44 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
                inCostStatus6.QtyOnHand = nullable44;
                INTranCostUpdate inTranCostUpdate4 = inTranCostUpdate1;
                nullable2 = inTranCostUpdate4.OversoldTranCost;
                nullable1 = nullable39;
                Decimal? nullable45;
                if (!(nullable2.HasValue & nullable1.HasValue))
                {
                  nullable36 = new Decimal?();
                  nullable45 = nullable36;
                }
                else
                  nullable45 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
                inTranCostUpdate4.OversoldTranCost = nullable45;
                INTranCostUpdate inTranCostUpdate5 = inTranCostUpdate1;
                nullable1 = inTranCostUpdate5.OversoldQty;
                nullable2 = nullable42;
                Decimal? nullable46;
                if (!(nullable1.HasValue & nullable2.HasValue))
                {
                  nullable36 = new Decimal?();
                  nullable46 = nullable36;
                }
                else
                  nullable46 = new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault());
                inTranCostUpdate5.OversoldQty = nullable46;
                ((PXSelectBase<INTranCost>) this.intrancost).Update(copy6);
                INTranCost copy7 = PXCache<INTranCost>.CreateCopy(copy1);
                copy7.IsOversold = new bool?(false);
                copy7.CostID = layer.CostID;
                copy7.InvtAcctID = layer.AccountID;
                copy7.InvtSubID = layer.SubID;
                copy7.TranDate = doc.TranDate;
                copy7.TranPeriodID = doc.TranPeriodID;
                copy7.FinPeriodID = doc.FinPeriodID;
                copy7.Qty = layer.QtyOnHand;
                copy7.OversoldQty = new Decimal?(0M);
                copy7.OversoldTranCost = new Decimal?(0M);
                copy7.TranCost = layer.TotalCost;
                if (layer.ValMethod == "T")
                {
                  INTranCost inTranCost16 = copy7;
                  nullable1 = layer.TotalCost;
                  Decimal? nullable47;
                  if (!nullable1.HasValue)
                  {
                    nullable36 = new Decimal?();
                    nullable47 = nullable36;
                  }
                  else
                    nullable47 = new Decimal?(-nullable1.GetValueOrDefault());
                  nullable2 = nullable47;
                  nullable1 = copy7.Qty;
                  nullable36 = layer.UnitCost;
                  Decimal? nullable48;
                  if (!(nullable1.HasValue & nullable36.HasValue))
                  {
                    nullable6 = new Decimal?();
                    nullable48 = nullable6;
                  }
                  else
                    nullable48 = new Decimal?(nullable1.GetValueOrDefault() * nullable36.GetValueOrDefault());
                  Decimal num13 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable48);
                  Decimal? nullable49;
                  if (!nullable2.HasValue)
                  {
                    nullable36 = new Decimal?();
                    nullable49 = nullable36;
                  }
                  else
                    nullable49 = new Decimal?(nullable2.GetValueOrDefault() + num13);
                  inTranCost16.VarCost = nullable49;
                }
                copy7.TranAmt = new Decimal?(0M);
                copy7.CostDocType = doc.DocType;
                copy7.CostRefNbr = doc.RefNbr;
                PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) copy7, typeof (INTran), (object) inTran);
                ((PXSelectBase) this.intrancost).Cache.Insert((object) copy7);
                layer.TotalCost = new Decimal?(0M);
                layer.QtyOnHand = new Decimal?(0M);
              }
            }
          }
        }
      }
    }
  }

  public virtual void ReceiveOversold(PX.Objects.IN.INRegister doc)
  {
    this.ReceiveOverSold<FIFOCostStatus, FIFOCostStatus.inventoryID, FIFOCostStatus.costSubItemID, FIFOCostStatus.costSiteID>(doc);
    this.ReceiveOverSold<AverageCostStatus, AverageCostStatus.inventoryID, AverageCostStatus.costSubItemID, AverageCostStatus.costSiteID>(doc);
    this.ReceiveOverSold<StandardCostStatus, StandardCostStatus.inventoryID, StandardCostStatus.costSubItemID, StandardCostStatus.costSiteID>(doc);
  }

  public virtual INCostStatus AccumulatedCostStatus(
    INCostStatus layer,
    PX.Objects.IN.InventoryItem item,
    INTran tran)
  {
    if (layer.LayerType == "O")
    {
      INCostStatus inCostStatus = (INCostStatus) new OversoldCostStatus();
      inCostStatus.AccountID = layer.AccountID;
      inCostStatus.SubID = layer.SubID;
      inCostStatus.InventoryID = layer.InventoryID;
      inCostStatus.CostSiteID = layer.CostSiteID;
      inCostStatus.SiteID = layer.SiteID;
      inCostStatus.CostSubItemID = layer.CostSubItemID;
      inCostStatus.ReceiptDate = layer.ReceiptDate;
      inCostStatus.ReceiptNbr = layer.ReceiptNbr;
      inCostStatus.LayerType = layer.LayerType;
      return (INCostStatus) ((PXSelectBase) this.oversoldcoststatus).Cache.Insert((object) inCostStatus);
    }
    switch (item.ValMethod)
    {
      case "A":
        INCostStatus inCostStatus1 = (INCostStatus) new AverageCostStatus();
        inCostStatus1.AccountID = layer.AccountID;
        inCostStatus1.SubID = layer.SubID;
        inCostStatus1.InventoryID = layer.InventoryID;
        inCostStatus1.CostSiteID = layer.CostSiteID;
        inCostStatus1.SiteID = layer.SiteID;
        inCostStatus1.CostSubItemID = layer.CostSubItemID;
        inCostStatus1.LayerType = layer.LayerType;
        inCostStatus1.ReceiptNbr = layer.ReceiptNbr;
        return (INCostStatus) ((PXSelectBase) this.averagecoststatus).Cache.Insert((object) inCostStatus1);
      case "T":
        INCostStatus inCostStatus2 = (INCostStatus) new StandardCostStatus();
        inCostStatus2.AccountID = layer.AccountID;
        inCostStatus2.SubID = layer.SubID;
        inCostStatus2.InventoryID = layer.InventoryID;
        inCostStatus2.CostSiteID = layer.CostSiteID;
        inCostStatus2.SiteID = layer.SiteID;
        inCostStatus2.CostSubItemID = layer.CostSubItemID;
        inCostStatus2.LayerType = layer.LayerType;
        inCostStatus2.ReceiptNbr = layer.ReceiptNbr;
        return (INCostStatus) ((PXSelectBase) this.standardcoststatus).Cache.Insert((object) inCostStatus2);
      case "F":
        INCostStatus inCostStatus3 = (INCostStatus) new FIFOCostStatus();
        inCostStatus3.AccountID = layer.AccountID;
        inCostStatus3.SubID = layer.SubID;
        inCostStatus3.InventoryID = layer.InventoryID;
        inCostStatus3.CostSiteID = layer.CostSiteID;
        inCostStatus3.SiteID = layer.SiteID;
        inCostStatus3.CostSubItemID = layer.CostSubItemID;
        inCostStatus3.ReceiptDate = layer.ReceiptDate;
        inCostStatus3.ReceiptNbr = layer.ReceiptNbr;
        inCostStatus3.LayerType = layer.LayerType;
        return (INCostStatus) ((PXSelectBase) this.fifocoststatus).Cache.Insert((object) inCostStatus3);
      case "S":
        int? nullable = layer.CostSiteID;
        int? transitCostSiteId = this.GetTransitCostSiteID(tran);
        INCostStatus inCostStatus4 = !(nullable.GetValueOrDefault() == transitCostSiteId.GetValueOrDefault() & nullable.HasValue == transitCostSiteId.HasValue) ? (INCostStatus) new SpecificCostStatus() : (INCostStatus) new SpecificTransitCostStatus();
        inCostStatus4.AccountID = layer.AccountID;
        inCostStatus4.SubID = layer.SubID;
        inCostStatus4.InventoryID = layer.InventoryID;
        inCostStatus4.CostSiteID = layer.CostSiteID;
        inCostStatus4.SiteID = layer.SiteID;
        inCostStatus4.CostSubItemID = layer.CostSubItemID;
        inCostStatus4.LotSerialNbr = layer.LotSerialNbr;
        inCostStatus4.ReceiptDate = layer.ReceiptDate;
        inCostStatus4.ReceiptNbr = layer.ReceiptNbr;
        inCostStatus4.LayerType = layer.LayerType;
        int? costSiteId = layer.CostSiteID;
        nullable = this.GetTransitCostSiteID(tran);
        return costSiteId.GetValueOrDefault() == nullable.GetValueOrDefault() & costSiteId.HasValue == nullable.HasValue ? (INCostStatus) ((PXSelectBase) this.specifictransitcoststatus).Cache.Insert((object) inCostStatus4) : (INCostStatus) ((PXSelectBase) this.specificcoststatus).Cache.Insert((object) inCostStatus4);
      default:
        throw new PXException();
    }
  }

  public virtual INCostStatus AccumulatedTransferCostStatus(
    INCostStatus layer,
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item)
  {
    bool flag = !this.IsIngoingTransfer(tran) && !this.IsOneStepTransfer();
    switch (item.ValMethod)
    {
      case "A":
        INCostStatus inCostStatus1 = (INCostStatus) new AverageCostStatus();
        inCostStatus1.AccountID = flag ? this.INTransitAcctID : tran.InvtAcctID;
        inCostStatus1.SubID = flag ? this.INTransitSubID : tran.InvtSubID;
        inCostStatus1.InventoryID = layer.InventoryID;
        inCostStatus1.CostSiteID = flag ? this.GetTransitCostSiteID(tran) : split.CostSiteID;
        inCostStatus1.SiteID = flag ? this.INTransitSiteID : split.SiteID;
        inCostStatus1.CostSubItemID = layer.CostSubItemID;
        inCostStatus1.LayerType = "N";
        inCostStatus1.ReceiptNbr = flag ? tran.RefNbr : "ZZZ";
        return (INCostStatus) ((PXSelectBase) this.averagecoststatus).Cache.Insert((object) inCostStatus1);
      case "T":
        INCostStatus inCostStatus2 = (INCostStatus) new StandardCostStatus();
        inCostStatus2.AccountID = flag ? this.INTransitAcctID : tran.InvtAcctID;
        inCostStatus2.SubID = flag ? this.INTransitSubID : tran.InvtSubID;
        inCostStatus2.InventoryID = layer.InventoryID;
        inCostStatus2.CostSiteID = flag ? this.GetTransitCostSiteID(tran) : split.CostSiteID;
        inCostStatus2.SiteID = flag ? this.INTransitSiteID : split.SiteID;
        inCostStatus2.CostSubItemID = layer.CostSubItemID;
        inCostStatus2.LayerType = "N";
        inCostStatus2.ReceiptNbr = flag ? tran.RefNbr : "ZZZ";
        return (INCostStatus) ((PXSelectBase) this.standardcoststatus).Cache.Insert((object) inCostStatus2);
      case "F":
        INCostStatus inCostStatus3 = (INCostStatus) new FIFOCostStatus();
        inCostStatus3.AccountID = flag ? this.INTransitAcctID : tran.InvtAcctID;
        inCostStatus3.SubID = flag ? this.INTransitSubID : tran.InvtSubID;
        inCostStatus3.InventoryID = layer.InventoryID;
        inCostStatus3.CostSiteID = flag ? this.GetTransitCostSiteID(tran) : split.CostSiteID;
        inCostStatus3.SiteID = flag ? this.INTransitSiteID : split.SiteID;
        inCostStatus3.CostSubItemID = layer.CostSubItemID;
        DateTime? nullable;
        string str;
        if (this.SameWarehouseTransfer(tran, split))
        {
          nullable = layer.ReceiptDate;
          str = layer.ReceiptNbr;
        }
        else
        {
          nullable = tran.TranDate;
          str = tran.RefNbr;
        }
        inCostStatus3.ReceiptDate = nullable;
        inCostStatus3.ReceiptNbr = str;
        inCostStatus3.LayerType = "N";
        return (INCostStatus) ((PXSelectBase) this.fifocoststatus).Cache.Insert((object) inCostStatus3);
      case "S":
        INCostStatus inCostStatus4 = !flag ? (INCostStatus) new SpecificCostStatus() : (INCostStatus) new SpecificTransitCostStatus();
        inCostStatus4.AccountID = flag ? this.INTransitAcctID : tran.InvtAcctID;
        inCostStatus4.SubID = flag ? this.INTransitSubID : tran.InvtSubID;
        inCostStatus4.InventoryID = layer.InventoryID;
        inCostStatus4.CostSiteID = flag ? this.GetTransitCostSiteID(tran) : split.CostSiteID;
        inCostStatus4.SiteID = flag ? this.INTransitSiteID : split.SiteID;
        inCostStatus4.CostSubItemID = layer.CostSubItemID;
        inCostStatus4.LotSerialNbr = layer.LotSerialNbr;
        if (this.SameWarehouseTransfer(tran, split))
        {
          inCostStatus4.ReceiptNbr = layer.ReceiptNbr;
          inCostStatus4.ReceiptDate = layer.ReceiptDate;
        }
        else
        {
          inCostStatus4.ReceiptNbr = tran.RefNbr;
          inCostStatus4.ReceiptDate = tran.TranDate;
        }
        inCostStatus4.LayerType = "N";
        return !flag ? (INCostStatus) ((PXSelectBase) this.specificcoststatus).Cache.Insert((object) inCostStatus4) : (INCostStatus) ((PXSelectBase) this.specifictransitcoststatus).Cache.Insert((object) inCostStatus4);
      default:
        throw new PXException();
    }
  }

  protected virtual bool SameWarehouseTransfer(INTran tran, INTranSplit split)
  {
    if (!this.IsOneStepTransfer())
      return false;
    if (!split.FromSiteID.HasValue && tran.OrigDocType != null && tran.OrigRefNbr != null && tran.OrigLineNbr.HasValue)
    {
      INTran inTran = INTran.PK.Find((PXGraph) this, tran.OrigDocType, tran.OrigRefNbr, tran.OrigLineNbr);
      split.FromSiteID = inTran.SiteID;
    }
    int? fromSiteId = split.FromSiteID;
    int? siteId = tran.SiteID;
    return fromSiteId.GetValueOrDefault() == siteId.GetValueOrDefault() & fromSiteId.HasValue == siteId.HasValue;
  }

  public virtual void IssueCost(
    INCostStatus layer,
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    ref Decimal QtyUnCosted)
  {
    INCostStatus issueCost = this.AccumulatedCostStatus(layer, item, tran);
    INTranCost inTranCost1 = new INTranCost();
    inTranCost1.InvtAcctID = issueCost.AccountID;
    inTranCost1.InvtSubID = issueCost.SubID;
    inTranCost1.COGSAcctID = tran.COGSAcctID;
    inTranCost1.COGSSubID = tran.COGSSubID;
    inTranCost1.CostID = issueCost.CostID;
    inTranCost1.InventoryID = issueCost.InventoryID;
    inTranCost1.CostSiteID = issueCost.CostSiteID;
    inTranCost1.CostSubItemID = issueCost.CostSubItemID;
    inTranCost1.IsOversold = new bool?(issueCost.LayerType == "O");
    inTranCost1.DocType = tran.DocType;
    inTranCost1.TranType = tran.TranType;
    inTranCost1.RefNbr = tran.RefNbr;
    inTranCost1.LineNbr = tran.LineNbr;
    inTranCost1.CostDocType = tran.DocType;
    inTranCost1.CostRefNbr = tran.RefNbr;
    inTranCost1.IsVirtual = new bool?(this.IsIngoingTransfer(tran));
    inTranCost1.CostType = this.IsTransitTransfer(tran, layer) ? "T" : "N";
    INTranCost inTranCost2 = inTranCost1;
    short? invtMult;
    short? nullable1;
    if (!this.IsIngoingTransfer(tran))
    {
      nullable1 = split.InvtMult;
    }
    else
    {
      invtMult = split.InvtMult;
      nullable1 = new short?((short) (invtMult.HasValue ? new int?((int) -invtMult.GetValueOrDefault()) : new int?()).Value);
    }
    inTranCost2.InvtMult = nullable1;
    inTranCost1.FinPeriodID = tran.FinPeriodID;
    inTranCost1.TranPeriodID = tran.TranPeriodID;
    inTranCost1.TranDate = tran.TranDate;
    inTranCost1.SiteID = tran.SiteID;
    inTranCost1.TranAmt = new Decimal?(0M);
    if (tran.DocType == "R" && this.IsIngoingTransfer(tran))
    {
      tran.AcctID = issueCost.AccountID;
      tran.SubID = issueCost.SubID;
    }
    PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) inTranCost1, typeof (INTran), (object) tran);
    INTranCost copy = PXCache<INTranCost>.CreateCopy(((PXSelectBase<INTranCost>) this.intrancost).Insert(inTranCost1));
    Decimal? qty = copy.Qty;
    Decimal? tranCost1 = copy.TranCost;
    Decimal? qtyOnHand1 = layer.QtyOnHand;
    Decimal num1 = QtyUnCosted;
    Decimal? nullable2;
    Decimal? nullable3;
    bool? nullable4;
    Decimal? nullable5;
    if (qtyOnHand1.GetValueOrDefault() <= num1 & qtyOnHand1.HasValue)
    {
      QtyUnCosted -= layer.QtyOnHand.Value;
      INTranCost inTranCost3 = copy;
      nullable2 = inTranCost3.TranAmt;
      Decimal num2 = layer.QtyOnHand.Value;
      nullable3 = tran.UnitPrice;
      Decimal num3 = nullable3.Value;
      Decimal num4 = PXCurrencyAttribute.BaseRound((PXGraph) this, num2 * num3);
      Decimal? nullable6;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable2.GetValueOrDefault() + num4);
      inTranCost3.TranAmt = nullable6;
      INTranCost inTranCost4 = copy;
      nullable2 = inTranCost4.Qty;
      nullable3 = layer.QtyOnHand;
      inTranCost4.Qty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      INTranCost inTranCost5 = copy;
      nullable3 = inTranCost5.TranCost;
      nullable2 = layer.TotalCost;
      inTranCost5.TranCost = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      if (this.UseStandardCost(issueCost.ValMethod, tran) && !this.IsIngoingTransfer(tran) && !this.IsOneStepTransfer())
      {
        INTranCost inTranCost6 = copy;
        nullable2 = inTranCost6.VarCost;
        Decimal num5 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, layer.QtyOnHand.Value * layer.UnitCost.Value);
        Decimal? totalCost = layer.TotalCost;
        nullable3 = totalCost.HasValue ? new Decimal?(num5 - totalCost.GetValueOrDefault()) : new Decimal?();
        inTranCost6.VarCost = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        nullable4 = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsCorrection;
        if (nullable4.GetValueOrDefault())
        {
          nullable4 = tran.ExactCost;
          if (nullable4.GetValueOrDefault() && QtyUnCosted == 0M)
          {
            Decimal? nullable7;
            ref Decimal? local = ref nullable7;
            Decimal? qtyOnHand2 = layer.QtyOnHand;
            Decimal? origTranCost = tran.OrigTranCost;
            nullable3 = qtyOnHand2.HasValue & origTranCost.HasValue ? new Decimal?(qtyOnHand2.GetValueOrDefault() * origTranCost.GetValueOrDefault()) : new Decimal?();
            nullable2 = tran.BaseQty;
            Decimal num6 = PXDBCurrencyAttribute.BaseRound((PXGraph) this, (nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?()).Value);
            local = new Decimal?(num6);
            nullable3 = nullable7;
            nullable2 = layer.TotalCost;
            if (!(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue))
            {
              INTranCost inTranCost7 = copy;
              nullable2 = inTranCost7.VarCost;
              Decimal? nullable8 = nullable7;
              nullable5 = layer.TotalCost;
              nullable3 = nullable8.HasValue & nullable5.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable9;
              if (!(nullable2.HasValue & nullable3.HasValue))
              {
                nullable5 = new Decimal?();
                nullable9 = nullable5;
              }
              else
                nullable9 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
              inTranCost7.VarCost = nullable9;
              tran.ReasonCode = this.GetCorrectionReasonCode(tran);
            }
          }
        }
      }
      layer.QtyOnHand = new Decimal?(0M);
      layer.TotalCost = new Decimal?(0M);
    }
    else
    {
      INTranCost inTranCost8 = copy;
      nullable3 = inTranCost8.TranAmt;
      Decimal num7 = PXCurrencyAttribute.BaseRound((PXGraph) this, QtyUnCosted * tran.UnitPrice.Value);
      inTranCost8.TranAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num7) : new Decimal?();
      if (PXCurrencyAttribute.IsNullOrEmpty(layer.UnitCost))
      {
        INCostStatus inCostStatus = layer;
        nullable3 = layer.TotalCost;
        Decimal num8 = nullable3.Value;
        nullable3 = layer.QtyOnHand;
        Decimal num9 = nullable3.Value;
        Decimal? nullable10 = new Decimal?(num8 / num9);
        inCostStatus.UnitCost = nullable10;
      }
      INCostStatus inCostStatus1 = layer;
      nullable3 = inCostStatus1.QtyOnHand;
      Decimal num10 = QtyUnCosted;
      inCostStatus1.QtyOnHand = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num10) : new Decimal?();
      INCostStatus inCostStatus2 = layer;
      nullable3 = inCostStatus2.TotalCost;
      Decimal? tranCost2 = copy.TranCost;
      inCostStatus2.TotalCost = nullable3.HasValue & tranCost2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + tranCost2.GetValueOrDefault()) : new Decimal?();
      INCostStatus inCostStatus3 = layer;
      nullable2 = inCostStatus3.TotalCost;
      nullable5 = copy.Qty;
      Decimal num11 = QtyUnCosted;
      nullable3 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + num11) : new Decimal?();
      nullable5 = layer.UnitCost;
      Decimal num12 = nullable5.Value;
      Decimal? nullable11;
      if (!nullable3.HasValue)
      {
        nullable5 = new Decimal?();
        nullable11 = nullable5;
      }
      else
        nullable11 = new Decimal?(nullable3.GetValueOrDefault() * num12);
      Decimal num13 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable11);
      Decimal? nullable12;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable12 = nullable3;
      }
      else
        nullable12 = new Decimal?(nullable2.GetValueOrDefault() - num13);
      inCostStatus3.TotalCost = nullable12;
      INTranCost inTranCost9 = copy;
      nullable2 = inTranCost9.Qty;
      Decimal num14 = QtyUnCosted;
      Decimal? nullable13;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable13 = nullable3;
      }
      else
        nullable13 = new Decimal?(nullable2.GetValueOrDefault() + num14);
      inTranCost9.Qty = nullable13;
      INTranCost inTranCost10 = copy;
      nullable2 = copy.Qty;
      nullable3 = layer.UnitCost;
      Decimal num15 = nullable3.Value;
      Decimal? nullable14;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable14 = nullable3;
      }
      else
        nullable14 = new Decimal?(nullable2.GetValueOrDefault() * num15);
      Decimal? nullable15 = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, nullable14));
      inTranCost10.TranCost = nullable15;
      QtyUnCosted = 0M;
    }
    nullable2 = qty;
    nullable3 = copy.Qty;
    Decimal? nullable16;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable5 = new Decimal?();
      nullable16 = nullable5;
    }
    else
      nullable16 = new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    Decimal? nullable17 = nullable16;
    nullable3 = tranCost1;
    nullable2 = copy.TranCost;
    Decimal? nullable18;
    if (!(nullable3.HasValue & nullable2.HasValue))
    {
      nullable5 = new Decimal?();
      nullable18 = nullable5;
    }
    else
      nullable18 = new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault());
    Decimal? nullable19 = nullable18;
    INCostStatus inCostStatus4 = issueCost;
    nullable2 = inCostStatus4.QtyOnHand;
    nullable3 = nullable17;
    Decimal? nullable20;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable5 = new Decimal?();
      nullable20 = nullable5;
    }
    else
      nullable20 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
    inCostStatus4.QtyOnHand = nullable20;
    INCostStatus inCostStatus5 = issueCost;
    nullable3 = inCostStatus5.PositiveTranQty;
    nullable5 = nullable17;
    Decimal num16 = 0M;
    nullable2 = nullable5.GetValueOrDefault() > num16 & nullable5.HasValue ? nullable17 : new Decimal?(0M);
    Decimal? nullable21;
    if (!(nullable3.HasValue & nullable2.HasValue))
    {
      nullable5 = new Decimal?();
      nullable21 = nullable5;
    }
    else
      nullable21 = new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault());
    inCostStatus5.PositiveTranQty = nullable21;
    INCostStatus inCostStatus6 = issueCost;
    nullable2 = inCostStatus6.TotalCost;
    nullable3 = nullable19;
    Decimal? nullable22;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable5 = new Decimal?();
      nullable22 = nullable5;
    }
    else
      nullable22 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
    inCostStatus6.TotalCost = nullable22;
    this.TransferCost(tran, split, item, issueCost, copy, nullable17.Value, nullable19.Value);
    INTranCost inTranCost11 = ((PXSelectBase<INTranCost>) this.intrancost).Update(copy);
    invtMult = tran.InvtMult;
    Decimal? nullable23;
    if (!invtMult.HasValue)
    {
      nullable2 = new Decimal?();
      nullable23 = nullable2;
    }
    else
      nullable23 = new Decimal?((Decimal) invtMult.GetValueOrDefault());
    nullable3 = nullable23;
    Decimal num17 = 1M;
    if (nullable3.GetValueOrDefault() == num17 & nullable3.HasValue)
    {
      nullable3 = tran.BaseQty;
      nullable5 = tran.CostedQty;
      nullable2 = nullable5.HasValue ? new Decimal?(-nullable5.GetValueOrDefault()) : new Decimal?();
      if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
      {
        if (item.ValMethod != "S")
        {
          INTranCost inTranCost12 = inTranCost11;
          Decimal num18 = -1M;
          nullable5 = tran.OrigTranCost;
          nullable2 = nullable5.HasValue ? new Decimal?(num18 * nullable5.GetValueOrDefault()) : new Decimal?();
          nullable3 = tran.TranCost;
          Decimal? nullable24;
          if (!(nullable2.HasValue & nullable3.HasValue))
          {
            nullable5 = new Decimal?();
            nullable24 = nullable5;
          }
          else
            nullable24 = new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
          inTranCost12.VarCost = nullable24;
        }
        if (item.ValMethod != "T")
        {
          INTran inTran = tran;
          nullable3 = tran.TranCost;
          Decimal? nullable25;
          if (!nullable3.HasValue)
          {
            nullable2 = new Decimal?();
            nullable25 = nullable2;
          }
          else
            nullable25 = new Decimal?(-nullable3.GetValueOrDefault());
          inTran.TranCost = nullable25;
        }
        else
          tran.TranCost = tran.OrigTranCost;
      }
    }
    nullable3 = tran.BaseQty;
    Decimal num19 = 0M;
    if (!(nullable3.GetValueOrDefault() == num19 & nullable3.HasValue))
    {
      nullable3 = tran.BaseQty;
      nullable2 = tran.CostedQty;
      if (!(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue))
      {
        nullable2 = tran.BaseQty;
        nullable5 = tran.CostedQty;
        nullable3 = nullable5.HasValue ? new Decimal?(-nullable5.GetValueOrDefault()) : new Decimal?();
        if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
          goto label_69;
      }
      INTranCost inTranCost13 = inTranCost11;
      nullable3 = inTranCost13.TranAmt;
      nullable5 = tran.OrigTranAmt;
      Decimal? tranAmt = tran.TranAmt;
      nullable2 = nullable5.HasValue & tranAmt.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - tranAmt.GetValueOrDefault()) : new Decimal?();
      inTranCost13.TranAmt = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      tran.TranAmt = tran.OrigTranAmt;
    }
label_69:
    nullable4 = inTranCost11.IsOversold;
    if (!nullable4.GetValueOrDefault())
      return;
    inTranCost11.OversoldQty = inTranCost11.Qty;
    inTranCost11.OversoldTranCost = inTranCost11.TranCost;
  }

  public virtual ReceiptStatus AssignQty(ReadOnlyReceiptStatus layer, ref Decimal QtyUnAssigned)
  {
    ReceiptStatus receiptStatus1 = new ReceiptStatus();
    receiptStatus1.ReceiptID = layer.ReceiptID;
    receiptStatus1.DocType = layer.DocType;
    receiptStatus1.ReceiptNbr = layer.ReceiptNbr;
    receiptStatus1.SubID = layer.SubID;
    receiptStatus1.AccountID = layer.AccountID;
    receiptStatus1.CostSiteID = layer.CostSiteID;
    receiptStatus1.OrigQty = layer.OrigQty;
    receiptStatus1.ReceiptDate = layer.ReceiptDate;
    receiptStatus1.LotSerialNbr = layer.LotSerialNbr == null || layer.ValMethod != "S" ? string.Empty : layer.LotSerialNbr;
    receiptStatus1.LayerType = layer.LayerType;
    receiptStatus1.InventoryID = layer.InventoryID;
    receiptStatus1.CostSubItemID = layer.CostSubItemID;
    if (QtyUnAssigned < 0M)
    {
      Decimal? qtyOnHand1 = layer.QtyOnHand;
      Decimal num1 = -QtyUnAssigned;
      if (qtyOnHand1.GetValueOrDefault() <= num1 & qtyOnHand1.HasValue)
      {
        QtyUnAssigned += layer.QtyOnHand.Value;
        ReceiptStatus receiptStatus2 = receiptStatus1;
        Decimal? qtyOnHand2 = layer.QtyOnHand;
        Decimal? nullable = qtyOnHand2.HasValue ? new Decimal?(-qtyOnHand2.GetValueOrDefault()) : new Decimal?();
        receiptStatus2.QtyOnHand = nullable;
        layer.QtyOnHand = new Decimal?(0M);
      }
      else
      {
        ReadOnlyReceiptStatus onlyReceiptStatus = layer;
        Decimal? qtyOnHand3 = onlyReceiptStatus.QtyOnHand;
        Decimal num2 = QtyUnAssigned;
        onlyReceiptStatus.QtyOnHand = qtyOnHand3.HasValue ? new Decimal?(qtyOnHand3.GetValueOrDefault() + num2) : new Decimal?();
        receiptStatus1.QtyOnHand = new Decimal?(QtyUnAssigned);
        QtyUnAssigned = 0M;
      }
    }
    else
    {
      receiptStatus1.QtyOnHand = new Decimal?(QtyUnAssigned);
      ReadOnlyReceiptStatus onlyReceiptStatus = layer;
      Decimal? qtyOnHand = onlyReceiptStatus.QtyOnHand;
      Decimal num = QtyUnAssigned;
      onlyReceiptStatus.QtyOnHand = qtyOnHand.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() + num) : new Decimal?();
    }
    return ((PXSelectBase<ReceiptStatus>) this.receiptstatus).Insert(receiptStatus1);
  }

  public virtual void IssueQty(INCostStatus layer)
  {
    Decimal valueOrDefault = layer.QtyOnHand.GetValueOrDefault();
    if (valueOrDefault >= 0M)
      return;
    PXView statusByKeysView = this.GetReceiptStatusByKeysView(layer);
    // ISSUE: method pointer
    foreach (ReadOnlyReceiptStatus onlyReceiptStatus in (IEnumerable<ReadOnlyReceiptStatus>) GraphHelper.RowCast<ReadOnlyReceiptStatus>((IEnumerable) statusByKeysView.SelectMulti(new object[6]
    {
      (object) layer.InventoryID,
      (object) layer.CostSiteID,
      (object) layer.CostSubItemID,
      (object) layer.AccountID,
      (object) layer.SubID,
      (object) layer.LotSerialNbr
    })).AsEnumerable<ReadOnlyReceiptStatus>().OrderByDescending<ReadOnlyReceiptStatus, bool>(new Func<ReadOnlyReceiptStatus, bool>((object) this, __methodptr(\u003CIssueQty\u003Eg__IsOrigReceiptStatusForCorrection\u007C163_0))).ThenBy<ReadOnlyReceiptStatus, DateTime?>((Func<ReadOnlyReceiptStatus, DateTime?>) (rs => rs.ReceiptDate)).ThenBy<ReadOnlyReceiptStatus, long?>((Func<ReadOnlyReceiptStatus, long?>) (rs => rs.ReceiptID)))
    {
      ReceiptStatus receiptStatus = this.AssignQty(onlyReceiptStatus, ref valueOrDefault);
      if (IsOrigReceiptStatusForCorrection(onlyReceiptStatus) && receiptStatus != null)
      {
        receiptStatus.OverrideOrigQty = new bool?(true);
        receiptStatus.OrigQty = layer.OrigQtyOnHand;
      }
      statusByKeysView.Cache.SetStatus((object) onlyReceiptStatus, (PXEntryStatus) 5);
      if (valueOrDefault == 0M)
        break;
    }

    bool IsOrigReceiptStatusForCorrection(ReadOnlyReceiptStatus rs)
    {
      return ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsCorrection.GetValueOrDefault() && string.Equals(rs.DocType, "R", StringComparison.OrdinalIgnoreCase) && string.Equals(rs.ReceiptNbr, ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.OrigReceiptNbr, StringComparison.OrdinalIgnoreCase);
    }
  }

  public virtual void IssueCost(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    bool correctImbalance)
  {
    short? invtMult1 = tran.InvtMult;
    int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
    int num1 = -1;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue) || !(!tran.ExactCost.GetValueOrDefault() | correctImbalance))
    {
      short? invtMult2 = tran.InvtMult;
      nullable1 = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
      int num2 = 1;
      if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
      {
        short? invtMult3 = split.InvtMult;
        nullable1 = invtMult3.HasValue ? new int?((int) invtMult3.GetValueOrDefault()) : new int?();
        int num3 = -1;
        if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue && (item.ValMethod == "T" && !correctImbalance || item.ValMethod != "T" & correctImbalance))
          goto label_6;
      }
      if (!(tran.TranType == "TRX"))
        return;
      if (this.IsOneStepTransfer())
      {
        short? invtMult4 = split.InvtMult;
        nullable1 = invtMult4.HasValue ? new int?((int) invtMult4.GetValueOrDefault()) : new int?();
        int num4 = -1;
        if (!(nullable1.GetValueOrDefault() == num4 & nullable1.HasValue))
          return;
      }
    }
label_6:
    object[] parameters;
    PXView costStatusCommand = this.GetCostStatusCommand(tran, split, item, out parameters, correctImbalance, (string) null);
    if (costStatusCommand == null)
      return;
    INCostStatus lastLayer = (INCostStatus) null;
    Decimal? nullable2 = split.BaseQty;
    Decimal QtyUnCosted = nullable2.Value;
    foreach (INCostStatus layer in costStatusCommand.SelectMulti(parameters))
    {
      lastLayer = layer;
      nullable2 = layer.QtyOnHand;
      Decimal num5 = 0M;
      if (nullable2.GetValueOrDefault() > num5 & nullable2.HasValue)
      {
        this.IssueCost(layer, tran, split, item, ref QtyUnCosted);
        GraphHelper.MarkUpdated(costStatusCommand.Cache, (object) layer);
        if (QtyUnCosted == 0M)
          break;
      }
    }
    short? invtMult5 = tran.InvtMult;
    int? nullable3 = invtMult5.HasValue ? new int?((int) invtMult5.GetValueOrDefault()) : new int?();
    int num6 = 1;
    if (nullable3.GetValueOrDefault() == num6 & nullable3.HasValue && QtyUnCosted > 0M)
    {
      if (item.ValMethod == "T" && !correctImbalance)
        throw new PXQtyCostImbalanceException();
      this.ThrowNegativeQtyException(tran, split, lastLayer);
    }
    if (EnumerableExtensions.IsIn<string>(tran.POReceiptType, "RN", "RT") && tran.ExactCost.GetValueOrDefault() && QtyUnCosted > 0M)
      this.ThrowNegativeQtyException(tran, split, lastLayer);
    if (QtyUnCosted > 0M && (!this.IsIngoingTransfer(tran) || this.IsOneStepTransfer()))
    {
      if (tran.DocType == "P")
      {
        short? invtMult6 = tran.InvtMult;
        int? nullable4;
        if (!invtMult6.HasValue)
        {
          nullable3 = new int?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new int?((int) invtMult6.GetValueOrDefault());
        nullable3 = nullable4;
        if (nullable3.GetValueOrDefault() == -1 && tran.ReasonCode == null)
          throw new PXException("The Reason Code column cannot be empty for stock components with insufficient quantity on hand. Specify the reason code for the {0} component.", new object[1]
          {
            (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, tran.InventoryID)?.InventoryCD
          });
      }
      INCostStatus copy = PXCache<INCostStatus>.CreateCopy(this.AccumOversoldCostStatus(tran, split, item));
      copy.QtyOnHand = new Decimal?(QtyUnCosted);
      copy.TotalCost = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, QtyUnCosted * copy.UnitCost.Value));
      this.IssueCost(copy, tran, split, item, ref QtyUnCosted);
    }
    if (QtyUnCosted > 0M)
      throw new PXException("Internal Error: {0}.", new object[1]
      {
        (object) 500
      });
  }

  protected virtual void ThrowNegativeQtyException(
    INTran tran,
    INTranSplit split,
    INCostStatus lastLayer)
  {
    object costSubItemId = (object) split.CostSubItemID;
    ((PXSelectBase) this.intranselect).Cache.RaiseFieldSelecting<INTran.subItemID>((object) tran, ref costSubItemId, true);
    object valueExt = ((PXSelectBase) this.intranselect).Cache.GetValueExt<INTran.inventoryID>((object) tran);
    if (this.IsIngoingTransfer(tran))
    {
      if (split.ValMethod == "S")
        throw new PXException("The document cannot be released. The quantity in transit for the '{0} {1}' item with the '{2}' lot/serial number will become negative. To proceed, adjust the quantity of the item in the document.", new object[3]
        {
          valueExt,
          costSubItemId,
          (object) split.LotSerialNbr
        });
      throw new PXException("The document cannot be released. The quantity in transit for the '{0} {1}' item will become negative. To proceed, adjust the quantity of the item in the document.", new object[2]
      {
        valueExt,
        costSubItemId
      });
    }
    if (split.ValMethod == "S" && !string.IsNullOrEmpty(split.LotSerialNbr))
      throw new PXException("Adjustment cannot be released because the adjustment quantity exceeds the on-hand quantity of the '{0} {1}' item with the {2} lot or serial number.", new object[3]
      {
        valueExt,
        costSubItemId,
        (object) split.LotSerialNbr
      });
    if (split.ValMethod == "F" && !string.IsNullOrEmpty(lastLayer?.ReceiptNbr))
    {
      if (tran.POReceiptType == "RN" && tran.ExactCost.GetValueOrDefault())
        throw new PXException("The document cannot be released because the return quantity exceeds the on-hand quantity of the {0} {1} item received by the {2} receipt.", new object[3]
        {
          valueExt,
          costSubItemId,
          (object) lastLayer.ReceiptNbr
        });
      throw new PXException("Adjustment cannot be released because the adjustment quantity exceeds the on-hand quantity of the '{0} {1}' item received by the {2} receipt.", new object[3]
      {
        valueExt,
        costSubItemId,
        (object) lastLayer.ReceiptNbr
      });
    }
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, split.CostSiteID);
    if (inSite != null)
      throw new PXException("The available quantity of the {0} {1} item is not sufficient in the {2} warehouse.", new object[3]
      {
        valueExt,
        costSubItemId,
        (object) inSite.SiteCD
      });
    PXResult<PX.Objects.IN.INSite, INLocation> pxResult = (PXResult<PX.Objects.IN.INSite, INLocation>) PXResultset<PX.Objects.IN.INSite>.op_Implicit(PXSelectBase<PX.Objects.IN.INSite, PXSelectReadonly2<PX.Objects.IN.INSite, InnerJoin<INLocation, On<INLocation.FK.Site>>, Where<INLocation.locationID, Equal<Required<INLocation.locationID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) split.CostSiteID
    }));
    if (pxResult != null)
      throw new PXException("Updating item '{0} {1}' in location '{2}' of warehouse '{3}' quantity available will go negative.", new object[4]
      {
        valueExt,
        costSubItemId,
        (object) ((PXResult) pxResult).GetItem<INLocation>().LocationCD,
        (object) ((PXResult) pxResult).GetItem<PX.Objects.IN.INSite>().SiteCD
      });
    throw new PXException("The available quantity of the {0} {1} item is not sufficient in the {2} warehouse.", new object[3]
    {
      valueExt,
      costSubItemId,
      (object) split.CostSiteID
    });
  }

  public virtual void TransferCost(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INCostStatus issueCost,
    INTranCost issueTranCost,
    Decimal issuedQty,
    Decimal issuedCost)
  {
    if (tran.TranType != "TRX")
      return;
    if (this.IsOneStepTransfer())
    {
      foreach (INTran inTran in ((PXSelectBase) this.intranselect).Cache.Cached)
      {
        int? nullable1 = inTran.OrigLineNbr;
        int? lineNbr = tran.LineNbr;
        if (nullable1.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable1.HasValue == lineNbr.HasValue && inTran.OrigRefNbr == tran.RefNbr && inTran.OrigDocType == tran.DocType)
        {
          foreach (INTranSplit selectChild in PXParentAttribute.SelectChildren(((PXSelectBase) this.intransplit).Cache, (object) inTran, typeof (INTran)))
          {
            int? nullable2 = split.ToLocationID;
            nullable1 = selectChild.LocationID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            {
              nullable1 = split.ToLocationID;
              if (!nullable1.HasValue)
              {
                nullable1 = inTran.LocationID;
                nullable2 = split.ToLocationID;
                if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
                  continue;
              }
              else
                continue;
            }
            split = (INTranSplit) ((PXSelectBase) this.intransplit).Cache.CreateCopy((object) split);
            split.FromSiteID = split.SiteID;
            split.FromLocationID = split.LocationID;
            INTranSplit inTranSplit1 = split;
            nullable2 = selectChild.ToSiteID;
            int? nullable3 = nullable2 ?? tran.ToSiteID;
            inTranSplit1.SiteID = nullable3;
            split.ToSiteID = split.SiteID;
            INTranSplit inTranSplit2 = split;
            nullable2 = selectChild.ToLocationID;
            int? nullable4 = nullable2 ?? tran.ToLocationID;
            inTranSplit2.LocationID = nullable4;
            split.ToLocationID = split.LocationID;
            split.CostSiteID = selectChild.CostSiteID;
            break;
          }
          tran = inTran;
          break;
        }
      }
    }
    INCostStatus layer = this.AccumulatedTransferCostStatus(issueCost, tran, split, item);
    INCostStatus inCostStatus1 = layer;
    Decimal? qtyOnHand = inCostStatus1.QtyOnHand;
    Decimal num1 = issuedQty;
    inCostStatus1.QtyOnHand = qtyOnHand.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() - num1) : new Decimal?();
    Decimal? nullable5;
    Decimal num2;
    if (this.UseStandardCost(layer.ValMethod, tran))
    {
      int? costSiteId = layer.CostSiteID;
      int? transitCostSiteId = this.GetTransitCostSiteID(tran);
      if (!(costSiteId.GetValueOrDefault() == transitCostSiteId.GetValueOrDefault() & costSiteId.HasValue == transitCostSiteId.HasValue))
      {
        nullable5 = layer.UnitCost;
        num2 = -PXCurrencyAttribute.BaseRound((PXGraph) this, nullable5.Value * issuedQty);
        goto label_23;
      }
    }
    num2 = -issuedCost;
label_23:
    INCostStatus inCostStatus2 = layer;
    nullable5 = inCostStatus2.TotalCost;
    Decimal num3 = num2;
    inCostStatus2.TotalCost = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + num3) : new Decimal?();
    INTranCost inTranCost1 = issueTranCost;
    INTranCost inTranCost2 = new INTranCost();
    inTranCost2.IsVirtual = new bool?(!this.IsOneStepTransfer() && !this.IsIngoingTransfer(tran));
    inTranCost2.CostType = this.IsTransitTransfer(tran, layer) ? "T" : "N";
    inTranCost2.InvtAcctID = layer.AccountID;
    inTranCost2.InvtSubID = layer.SubID;
    inTranCost2.InventoryID = layer.InventoryID;
    inTranCost2.CostSiteID = layer.CostSiteID;
    inTranCost2.CostSubItemID = layer.CostSubItemID;
    inTranCost2.COGSAcctID = tran.COGSAcctID;
    inTranCost2.COGSSubID = tran.COGSSubID;
    inTranCost2.FinPeriodID = tran.FinPeriodID;
    inTranCost2.TranPeriodID = tran.TranPeriodID;
    inTranCost2.TranDate = tran.TranDate;
    inTranCost2.SiteID = tran.SiteID;
    inTranCost2.CostID = layer.CostID;
    inTranCost2.DocType = tran.DocType;
    inTranCost2.TranType = tran.TranType;
    inTranCost2.RefNbr = tran.RefNbr;
    inTranCost2.LineNbr = tran.LineNbr;
    inTranCost2.CostDocType = tran.DocType;
    inTranCost2.CostRefNbr = tran.RefNbr;
    INTranCost inTranCost3 = inTranCost2;
    short? invtMult = inTranCost1.InvtMult;
    short? nullable6 = invtMult.HasValue ? new short?(-invtMult.GetValueOrDefault()) : new short?();
    inTranCost3.InvtMult = nullable6;
    INTranCost inTranCost4 = ((PXSelectBase<INTranCost>) this.intrancost).Insert(inTranCost2);
    PXParentAttribute.SetParent(((PXSelectBase) this.intrancost).Cache, (object) inTranCost4, typeof (INTran), (object) tran);
    INTranCost inTranCost5 = inTranCost4;
    nullable5 = inTranCost5.QtyOnHand;
    Decimal num4 = -issuedQty;
    inTranCost5.QtyOnHand = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + num4) : new Decimal?();
    if (this.UseStandardCost(layer.ValMethod, tran))
    {
      INTranCost inTranCost6 = inTranCost4;
      nullable5 = inTranCost6.TranCost;
      Decimal num5 = num2;
      inTranCost6.TranCost = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + num5) : new Decimal?();
      INTranCost inTranCost7 = inTranCost4;
      nullable5 = inTranCost7.VarCost;
      Decimal num6 = -issuedCost - num2;
      inTranCost7.VarCost = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + num6) : new Decimal?();
    }
    else
    {
      INTranCost inTranCost8 = inTranCost4;
      nullable5 = inTranCost8.TranCost;
      Decimal num7 = -issuedCost;
      inTranCost8.TranCost = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + num7) : new Decimal?();
    }
    ((PXSelectBase<INTranCost>) this.intrancost).Update(inTranCost4);
    nullable5 = tran.BaseQty;
    Decimal num8 = 0M;
    if (nullable5.GetValueOrDefault() == num8 & nullable5.HasValue)
      return;
    nullable5 = tran.BaseQty;
    Decimal? costedQty = tran.CostedQty;
    if (!(nullable5.GetValueOrDefault() == costedQty.GetValueOrDefault() & nullable5.HasValue == costedQty.HasValue) || !(layer.ValMethod == "T"))
      return;
    tran.TranCost = tran.OrigTranCost;
  }

  public virtual void AssembleCost(INTran tran, INTranSplit split, PX.Objects.IN.InventoryItem item)
  {
    short? invtMult1;
    if ((tran.TranType == "ASY" || tran.TranType == "DSY") && tran.AssyType == "K")
    {
      invtMult1 = tran.InvtMult;
      int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
      int num1 = 1;
      if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      {
        tran.TranCost = new Decimal?(0M);
        foreach (INTranCost inTranCost in ((PXSelectBase) this.intrancost).Cache.Inserted)
        {
          if (string.Equals(inTranCost.CostDocType, tran.DocType) && string.Equals(inTranCost.CostRefNbr, tran.RefNbr))
          {
            invtMult1 = inTranCost.InvtMult;
            int? nullable2 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
            int num2 = -1;
            if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
            {
              INTran inTran = tran;
              Decimal? tranCost1 = inTran.TranCost;
              Decimal? tranCost2 = inTranCost.TranCost;
              inTran.TranCost = tranCost1.HasValue & tranCost2.HasValue ? new Decimal?(tranCost1.GetValueOrDefault() + tranCost2.GetValueOrDefault()) : new Decimal?();
            }
          }
        }
        foreach (INTran inTran1 in ((PXSelectBase) this.intranselect).Cache.Updated)
        {
          if (string.Equals(inTran1.DocType, tran.DocType) && string.Equals(inTran1.RefNbr, tran.RefNbr) && inTran1.AssyType == "O")
          {
            short? invtMult2 = inTran1.InvtMult;
            int? nullable3 = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
            int num3 = -1;
            if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
            {
              INTran inTran2 = tran;
              Decimal? tranCost3 = inTran2.TranCost;
              Decimal? tranCost4 = inTran1.TranCost;
              inTran2.TranCost = tranCost3.HasValue & tranCost4.HasValue ? new Decimal?(tranCost3.GetValueOrDefault() + tranCost4.GetValueOrDefault()) : new Decimal?();
            }
          }
        }
      }
    }
    if (!(tran.TranType == "ASY") && !(tran.TranType == "DSY") || !(tran.AssyType == "C") && !(tran.AssyType == "O"))
      return;
    invtMult1 = tran.InvtMult;
    int? nullable4 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
    int num4 = 1;
    if (!(nullable4.GetValueOrDefault() == num4 & nullable4.HasValue))
      return;
    if (!this.WIPCalculated)
    {
      foreach (INTranCost inTranCost in ((PXSelectBase) this.intrancost).Cache.Inserted)
      {
        if (string.Equals(inTranCost.CostDocType, tran.DocType) && string.Equals(inTranCost.CostRefNbr, tran.RefNbr))
        {
          invtMult1 = inTranCost.InvtMult;
          int? nullable5 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
          int num5 = -1;
          if (nullable5.GetValueOrDefault() == num5 & nullable5.HasValue)
          {
            Decimal? wipVariance = this.WIPVariance;
            Decimal? tranCost = inTranCost.TranCost;
            this.WIPVariance = wipVariance.HasValue & tranCost.HasValue ? new Decimal?(wipVariance.GetValueOrDefault() + tranCost.GetValueOrDefault()) : new Decimal?();
          }
        }
      }
      this.WIPCalculated = true;
    }
    Decimal? wipVariance1 = this.WIPVariance;
    Decimal? tranCost5 = tran.TranCost;
    this.WIPVariance = wipVariance1.HasValue & tranCost5.HasValue ? new Decimal?(wipVariance1.GetValueOrDefault() - tranCost5.GetValueOrDefault()) : new Decimal?();
  }

  public virtual bool IsIngoingTransfer(INTran tran)
  {
    if (!(tran.TranType == "TRX"))
      return false;
    short? invtMult = tran.InvtMult;
    Decimal? nullable = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    Decimal num = 1M;
    return nullable.GetValueOrDefault() == num & nullable.HasValue;
  }

  protected virtual bool IsTransitTransfer(INTran tran, INCostStatus layer)
  {
    if (!(tran.TranType == "TRX"))
      return false;
    int? costSiteId = layer.CostSiteID;
    int? transitCostSiteId = this.GetTransitCostSiteID(tran);
    return costSiteId.GetValueOrDefault() == transitCostSiteId.GetValueOrDefault() & costSiteId.HasValue == transitCostSiteId.HasValue;
  }

  public virtual void UpdateCostStatus(
    INTran prev_tran,
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item)
  {
    if (!object.Equals((object) prev_tran, (object) tran))
    {
      this.AssembleCost(tran, split, item);
      Decimal? nullable1 = tran.BaseQty;
      Decimal num1 = 0M;
      if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
      {
        tran.CostedQty = new Decimal?(0M);
        tran.OrigTranCost = tran.TranCost;
        tran.OrigTranAmt = tran.TranAmt;
        tran.TranCost = new Decimal?(0M);
        tran.TranAmt = new Decimal?(0M);
        nullable1 = tran.OrigTranCost;
        Decimal num2 = nullable1.Value;
        nullable1 = tran.BaseQty;
        Decimal num3 = nullable1.Value;
        nullable1 = tran.UnitCost;
        Decimal num4 = nullable1.Value;
        Decimal num5 = PXCurrencyAttribute.BaseRound((PXGraph) this, num3 * num4);
        if (Math.Abs(num2 - num5) > 0.00005M)
        {
          INTran inTran = tran;
          nullable1 = tran.OrigTranCost;
          Decimal num6 = nullable1.Value;
          nullable1 = tran.BaseQty;
          Decimal num7 = nullable1.Value;
          Decimal? nullable2 = new Decimal?(num6 / num7);
          inTran.UnitCost = nullable2;
          if (EnumerableExtensions.IsIn<string>(tran.TranType, "ASY", "DSY"))
            tran.OverrideUnitCost = new bool?(true);
        }
        nullable1 = tran.OrigTranAmt;
        Decimal num8 = nullable1.Value;
        nullable1 = tran.BaseQty;
        Decimal num9 = nullable1.Value;
        nullable1 = tran.UnitPrice;
        Decimal num10 = nullable1.Value;
        Decimal num11 = PXCurrencyAttribute.BaseRound((PXGraph) this, num9 * num10);
        if (Math.Abs(num8 - num11) > 0.00005M)
        {
          INTran inTran = tran;
          nullable1 = tran.OrigTranAmt;
          Decimal num12 = nullable1.Value;
          nullable1 = tran.BaseQty;
          Decimal num13 = nullable1.Value;
          Decimal? nullable3 = new Decimal?(num12 / num13);
          inTran.UnitPrice = nullable3;
        }
      }
      else
      {
        tran.CostedQty = new Decimal?(0M);
        tran.UnitCost = new Decimal?(0M);
        tran.UnitPrice = new Decimal?(0M);
      }
    }
    this.DropshipCost(tran, split, item);
    try
    {
      this.ReceiveCost(tran, split, item, false);
      this.IssueCost(tran, split, item, false);
    }
    catch (PXNegativeQtyImbalanceException ex)
    {
      this.IssueCost(tran, split, item, true);
    }
    catch (PXQtyCostImbalanceException ex1)
    {
      try
      {
        this.ReceiveCost(tran, split, item, true);
        this.IssueCost(tran, split, item, true);
      }
      catch (PXNegativeQtyImbalanceException ex2)
      {
        this.ThrowNegativeQtyException(tran, split, (INCostStatus) null);
      }
    }
  }

  private void ProceedReceiveQtyForLayer(INCostStatus layer)
  {
    if (layer.LayerType != "N" || layer.ValMethod == "F")
      return;
    Decimal? qtyOnHand = layer.QtyOnHand;
    Decimal num1 = 0M;
    if (qtyOnHand.GetValueOrDefault() < num1 & qtyOnHand.HasValue)
    {
      this.IssueQty(layer);
    }
    else
    {
      PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current;
      qtyOnHand = layer.QtyOnHand;
      Decimal num2 = 0M;
      if (!(qtyOnHand.GetValueOrDefault() > num2 & qtyOnHand.HasValue))
      {
        if (!(current.DocType == "R") && !current.IsCorrection.GetValueOrDefault())
          return;
        qtyOnHand = layer.QtyOnHand;
        Decimal num3 = 0M;
        if (!(qtyOnHand.GetValueOrDefault() == num3 & qtyOnHand.HasValue))
          return;
      }
      ReceiptStatus receiptStatus1 = new ReceiptStatus();
      receiptStatus1.InventoryID = layer.InventoryID;
      receiptStatus1.CostSiteID = layer.CostSiteID;
      receiptStatus1.CostSubItemID = layer.CostSubItemID;
      receiptStatus1.DocType = current.IsCorrection.GetValueOrDefault() ? "R" : current.DocType;
      ReceiptStatus receiptStatus2 = receiptStatus1;
      bool? isCorrection = current.IsCorrection;
      string str = isCorrection.GetValueOrDefault() ? current.OrigReceiptNbr : current.RefNbr;
      receiptStatus2.ReceiptNbr = str;
      receiptStatus1.ReceiptDate = current.TranDate;
      ReceiptStatus receiptStatus3 = receiptStatus1;
      isCorrection = current.IsCorrection;
      bool? nullable = new bool?(isCorrection.GetValueOrDefault());
      receiptStatus3.OverrideOrigQty = nullable;
      receiptStatus1.OrigQty = layer.OrigQtyOnHand;
      receiptStatus1.ValMethod = layer.ValMethod;
      receiptStatus1.AccountID = layer.AccountID;
      receiptStatus1.SubID = layer.SubID;
      receiptStatus1.LotSerialNbr = layer.LotSerialNbr == null || layer.ValMethod != "S" ? string.Empty : layer.LotSerialNbr;
      receiptStatus1.QtyOnHand = layer.QtyOnHand;
      ((PXSelectBase<ReceiptStatus>) this.receiptstatus).Insert(receiptStatus1);
    }
  }

  private void ReceiveQty()
  {
    foreach (INCostStatus layer in ((PXSelectBase) this.averagecoststatus).Cache.Inserted)
      this.ProceedReceiveQtyForLayer(layer);
    foreach (INCostStatus layer in ((PXSelectBase) this.standardcoststatus).Cache.Inserted)
      this.ProceedReceiveQtyForLayer(layer);
    foreach (INCostStatus layer in ((PXSelectBase) this.specificcoststatus).Cache.Inserted)
      this.ProceedReceiveQtyForLayer(layer);
  }

  protected static void UpdateHistoryField<FinHistoryField, TranHistoryField>(
    PXGraph graph,
    object data,
    Decimal? value,
    bool IsFinField)
    where FinHistoryField : IBqlField
    where TranHistoryField : IBqlField
  {
    PXCache cach = graph.Caches[BqlCommand.GetItemType(typeof (FinHistoryField))];
    if (IsFinField)
    {
      Decimal? nullable = (Decimal?) cach.GetValue<FinHistoryField>(data);
      cach.SetValue<FinHistoryField>(data, (object) (nullable.GetValueOrDefault() + value.GetValueOrDefault()));
    }
    else
    {
      Decimal? nullable = (Decimal?) cach.GetValue<TranHistoryField>(data);
      cach.SetValue<TranHistoryField>(data, (object) (nullable.GetValueOrDefault() + value.GetValueOrDefault()));
    }
  }

  public static void UpdateCostHist(
    PXGraph graph,
    INReleaseProcess.INHistBucket bucket,
    INTranCost tran,
    int? siteID,
    string PeriodID,
    bool FinFlag)
  {
    ItemCostHist itemCostHist1 = new ItemCostHist();
    itemCostHist1.InventoryID = tran.InventoryID;
    itemCostHist1.CostSiteID = tran.CostSiteID;
    itemCostHist1.SiteID = siteID;
    itemCostHist1.AccountID = tran.InvtAcctID;
    itemCostHist1.SubID = tran.InvtSubID;
    itemCostHist1.CostSubItemID = tran.CostSubItemID;
    itemCostHist1.FinPeriodID = PeriodID;
    ItemCostHist itemCostHist2 = (ItemCostHist) graph.Caches[typeof (ItemCostHist)].Insert((object) itemCostHist1);
    PXGraph graph1 = graph;
    ItemCostHist data1 = itemCostHist2;
    Decimal? tranCost1 = tran.TranCost;
    Decimal signReceived1 = bucket.SignReceived;
    Decimal? nullable1 = tranCost1.HasValue ? new Decimal?(tranCost1.GetValueOrDefault() * signReceived1) : new Decimal?();
    int num1 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostReceived, INItemCostHist.tranPtdCostReceived>(graph1, (object) data1, nullable1, num1 != 0);
    PXGraph graph2 = graph;
    ItemCostHist data2 = itemCostHist2;
    Decimal? tranCost2 = tran.TranCost;
    Decimal signIssued1 = bucket.SignIssued;
    Decimal? nullable2 = tranCost2.HasValue ? new Decimal?(tranCost2.GetValueOrDefault() * signIssued1) : new Decimal?();
    int num2 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostIssued, INItemCostHist.tranPtdCostIssued>(graph2, (object) data2, nullable2, num2 != 0);
    PXGraph graph3 = graph;
    ItemCostHist data3 = itemCostHist2;
    Decimal? tranCost3 = tran.TranCost;
    Decimal signSales1 = bucket.SignSales;
    Decimal? nullable3 = tranCost3.HasValue ? new Decimal?(tranCost3.GetValueOrDefault() * signSales1) : new Decimal?();
    int num3 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCOGS, INItemCostHist.tranPtdCOGS>(graph3, (object) data3, nullable3, num3 != 0);
    PXGraph graph4 = graph;
    ItemCostHist data4 = itemCostHist2;
    Decimal? tranCost4 = tran.TranCost;
    Decimal signCreditMemos1 = bucket.SignCreditMemos;
    Decimal? nullable4 = tranCost4.HasValue ? new Decimal?(tranCost4.GetValueOrDefault() * signCreditMemos1) : new Decimal?();
    int num4 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCOGSCredits, INItemCostHist.tranPtdCOGSCredits>(graph4, (object) data4, nullable4, num4 != 0);
    PXGraph graph5 = graph;
    ItemCostHist data5 = itemCostHist2;
    Decimal? tranCost5 = tran.TranCost;
    Decimal signDropShip1 = bucket.SignDropShip;
    Decimal? nullable5 = tranCost5.HasValue ? new Decimal?(tranCost5.GetValueOrDefault() * signDropShip1) : new Decimal?();
    int num5 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCOGSDropShips, INItemCostHist.tranPtdCOGSDropShips>(graph5, (object) data5, nullable5, num5 != 0);
    PXGraph graph6 = graph;
    ItemCostHist data6 = itemCostHist2;
    Decimal? tranCost6 = tran.TranCost;
    Decimal signTransferIn1 = bucket.SignTransferIn;
    Decimal? nullable6 = tranCost6.HasValue ? new Decimal?(tranCost6.GetValueOrDefault() * signTransferIn1) : new Decimal?();
    int num6 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostTransferIn, INItemCostHist.tranPtdCostTransferIn>(graph6, (object) data6, nullable6, num6 != 0);
    PXGraph graph7 = graph;
    ItemCostHist data7 = itemCostHist2;
    Decimal? tranCost7 = tran.TranCost;
    Decimal signTransferOut1 = bucket.SignTransferOut;
    Decimal? nullable7 = tranCost7.HasValue ? new Decimal?(tranCost7.GetValueOrDefault() * signTransferOut1) : new Decimal?();
    int num7 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostTransferOut, INItemCostHist.tranPtdCostTransferOut>(graph7, (object) data7, nullable7, num7 != 0);
    PXGraph graph8 = graph;
    ItemCostHist data8 = itemCostHist2;
    Decimal? tranCost8 = tran.TranCost;
    Decimal signAdjusted1 = bucket.SignAdjusted;
    Decimal? nullable8 = tranCost8.HasValue ? new Decimal?(tranCost8.GetValueOrDefault() * signAdjusted1) : new Decimal?();
    int num8 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostAdjusted, INItemCostHist.tranPtdCostAdjusted>(graph8, (object) data8, nullable8, num8 != 0);
    PXGraph graph9 = graph;
    ItemCostHist data9 = itemCostHist2;
    Decimal? tranCost9 = tran.TranCost;
    Decimal signAssemblyIn1 = bucket.SignAssemblyIn;
    Decimal? nullable9 = tranCost9.HasValue ? new Decimal?(tranCost9.GetValueOrDefault() * signAssemblyIn1) : new Decimal?();
    int num9 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostAssemblyIn, INItemCostHist.tranPtdCostAssemblyIn>(graph9, (object) data9, nullable9, num9 != 0);
    PXGraph graph10 = graph;
    ItemCostHist data10 = itemCostHist2;
    Decimal? tranCost10 = tran.TranCost;
    Decimal signAssemblyOut1 = bucket.SignAssemblyOut;
    Decimal? nullable10 = tranCost10.HasValue ? new Decimal?(tranCost10.GetValueOrDefault() * signAssemblyOut1) : new Decimal?();
    int num10 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostAssemblyOut, INItemCostHist.tranPtdCostAssemblyOut>(graph10, (object) data10, nullable10, num10 != 0);
    PXGraph graph11 = graph;
    ItemCostHist data11 = itemCostHist2;
    Decimal? tranCost11 = tran.TranCost;
    Decimal signAmAssemblyIn1 = bucket.SignAMAssemblyIn;
    Decimal? nullable11 = tranCost11.HasValue ? new Decimal?(tranCost11.GetValueOrDefault() * signAmAssemblyIn1) : new Decimal?();
    int num11 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostAMAssemblyIn, INItemCostHist.tranPtdCostAMAssemblyIn>(graph11, (object) data11, nullable11, num11 != 0);
    PXGraph graph12 = graph;
    ItemCostHist data12 = itemCostHist2;
    Decimal? tranCost12 = tran.TranCost;
    Decimal signAmAssemblyOut1 = bucket.SignAMAssemblyOut;
    Decimal? nullable12 = tranCost12.HasValue ? new Decimal?(tranCost12.GetValueOrDefault() * signAmAssemblyOut1) : new Decimal?();
    int num12 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCostAMAssemblyOut, INItemCostHist.tranPtdCostAMAssemblyOut>(graph12, (object) data12, nullable12, num12 != 0);
    PXGraph graph13 = graph;
    ItemCostHist data13 = itemCostHist2;
    Decimal? qty1 = tran.Qty;
    Decimal signReceived2 = bucket.SignReceived;
    Decimal? nullable13 = qty1.HasValue ? new Decimal?(qty1.GetValueOrDefault() * signReceived2) : new Decimal?();
    int num13 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyReceived, INItemCostHist.tranPtdQtyReceived>(graph13, (object) data13, nullable13, num13 != 0);
    PXGraph graph14 = graph;
    ItemCostHist data14 = itemCostHist2;
    Decimal? qty2 = tran.Qty;
    Decimal signIssued2 = bucket.SignIssued;
    Decimal? nullable14 = qty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() * signIssued2) : new Decimal?();
    int num14 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyIssued, INItemCostHist.tranPtdQtyIssued>(graph14, (object) data14, nullable14, num14 != 0);
    PXGraph graph15 = graph;
    ItemCostHist data15 = itemCostHist2;
    Decimal? qty3 = tran.Qty;
    Decimal signSales2 = bucket.SignSales;
    Decimal? nullable15 = qty3.HasValue ? new Decimal?(qty3.GetValueOrDefault() * signSales2) : new Decimal?();
    int num15 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtySales, INItemCostHist.tranPtdQtySales>(graph15, (object) data15, nullable15, num15 != 0);
    PXGraph graph16 = graph;
    ItemCostHist data16 = itemCostHist2;
    Decimal? qty4 = tran.Qty;
    Decimal signCreditMemos2 = bucket.SignCreditMemos;
    Decimal? nullable16 = qty4.HasValue ? new Decimal?(qty4.GetValueOrDefault() * signCreditMemos2) : new Decimal?();
    int num16 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyCreditMemos, INItemCostHist.tranPtdQtyCreditMemos>(graph16, (object) data16, nullable16, num16 != 0);
    PXGraph graph17 = graph;
    ItemCostHist data17 = itemCostHist2;
    Decimal? qty5 = tran.Qty;
    Decimal signDropShip2 = bucket.SignDropShip;
    Decimal? nullable17 = qty5.HasValue ? new Decimal?(qty5.GetValueOrDefault() * signDropShip2) : new Decimal?();
    int num17 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyDropShipSales, INItemCostHist.tranPtdQtyDropShipSales>(graph17, (object) data17, nullable17, num17 != 0);
    PXGraph graph18 = graph;
    ItemCostHist data18 = itemCostHist2;
    Decimal? qty6 = tran.Qty;
    Decimal signTransferIn2 = bucket.SignTransferIn;
    Decimal? nullable18 = qty6.HasValue ? new Decimal?(qty6.GetValueOrDefault() * signTransferIn2) : new Decimal?();
    int num18 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyTransferIn, INItemCostHist.tranPtdQtyTransferIn>(graph18, (object) data18, nullable18, num18 != 0);
    PXGraph graph19 = graph;
    ItemCostHist data19 = itemCostHist2;
    Decimal? qty7 = tran.Qty;
    Decimal signTransferOut2 = bucket.SignTransferOut;
    Decimal? nullable19 = qty7.HasValue ? new Decimal?(qty7.GetValueOrDefault() * signTransferOut2) : new Decimal?();
    int num19 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyTransferOut, INItemCostHist.tranPtdQtyTransferOut>(graph19, (object) data19, nullable19, num19 != 0);
    PXGraph graph20 = graph;
    ItemCostHist data20 = itemCostHist2;
    Decimal? qty8 = tran.Qty;
    Decimal signAdjusted2 = bucket.SignAdjusted;
    Decimal? nullable20 = qty8.HasValue ? new Decimal?(qty8.GetValueOrDefault() * signAdjusted2) : new Decimal?();
    int num20 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyAdjusted, INItemCostHist.tranPtdQtyAdjusted>(graph20, (object) data20, nullable20, num20 != 0);
    PXGraph graph21 = graph;
    ItemCostHist data21 = itemCostHist2;
    Decimal? qty9 = tran.Qty;
    Decimal signAssemblyIn2 = bucket.SignAssemblyIn;
    Decimal? nullable21 = qty9.HasValue ? new Decimal?(qty9.GetValueOrDefault() * signAssemblyIn2) : new Decimal?();
    int num21 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyAssemblyIn, INItemCostHist.tranPtdQtyAssemblyIn>(graph21, (object) data21, nullable21, num21 != 0);
    PXGraph graph22 = graph;
    ItemCostHist data22 = itemCostHist2;
    Decimal? qty10 = tran.Qty;
    Decimal signAssemblyOut2 = bucket.SignAssemblyOut;
    Decimal? nullable22 = qty10.HasValue ? new Decimal?(qty10.GetValueOrDefault() * signAssemblyOut2) : new Decimal?();
    int num22 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyAssemblyOut, INItemCostHist.tranPtdQtyAssemblyOut>(graph22, (object) data22, nullable22, num22 != 0);
    PXGraph graph23 = graph;
    ItemCostHist data23 = itemCostHist2;
    Decimal? qty11 = tran.Qty;
    Decimal signAmAssemblyIn2 = bucket.SignAMAssemblyIn;
    Decimal? nullable23 = qty11.HasValue ? new Decimal?(qty11.GetValueOrDefault() * signAmAssemblyIn2) : new Decimal?();
    int num23 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyAMAssemblyIn, INItemCostHist.tranPtdQtyAMAssemblyIn>(graph23, (object) data23, nullable23, num23 != 0);
    PXGraph graph24 = graph;
    ItemCostHist data24 = itemCostHist2;
    Decimal? qty12 = tran.Qty;
    Decimal signAmAssemblyOut2 = bucket.SignAMAssemblyOut;
    Decimal? nullable24 = qty12.HasValue ? new Decimal?(qty12.GetValueOrDefault() * signAmAssemblyOut2) : new Decimal?();
    int num24 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdQtyAMAssemblyOut, INItemCostHist.tranPtdQtyAMAssemblyOut>(graph24, (object) data24, nullable24, num24 != 0);
    PXGraph graph25 = graph;
    ItemCostHist data25 = itemCostHist2;
    Decimal? tranAmt1 = tran.TranAmt;
    Decimal signSales3 = bucket.SignSales;
    Decimal? nullable25 = tranAmt1.HasValue ? new Decimal?(tranAmt1.GetValueOrDefault() * signSales3) : new Decimal?();
    int num25 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdSales, INItemCostHist.tranPtdSales>(graph25, (object) data25, nullable25, num25 != 0);
    PXGraph graph26 = graph;
    ItemCostHist data26 = itemCostHist2;
    Decimal? tranAmt2 = tran.TranAmt;
    Decimal signCreditMemos3 = bucket.SignCreditMemos;
    Decimal? nullable26 = tranAmt2.HasValue ? new Decimal?(tranAmt2.GetValueOrDefault() * signCreditMemos3) : new Decimal?();
    int num26 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdCreditMemos, INItemCostHist.tranPtdCreditMemos>(graph26, (object) data26, nullable26, num26 != 0);
    PXGraph graph27 = graph;
    ItemCostHist data27 = itemCostHist2;
    Decimal? tranAmt3 = tran.TranAmt;
    Decimal signDropShip3 = bucket.SignDropShip;
    Decimal? nullable27 = tranAmt3.HasValue ? new Decimal?(tranAmt3.GetValueOrDefault() * signDropShip3) : new Decimal?();
    int num27 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCostHist.finPtdDropShipSales, INItemCostHist.tranPtdDropShipSales>(graph27, (object) data27, nullable27, num27 != 0);
    PXGraph graph28 = graph;
    ItemCostHist data28 = itemCostHist2;
    Decimal? qty13 = tran.Qty;
    Decimal signYtd1 = bucket.SignYtd;
    Decimal? nullable28 = qty13.HasValue ? new Decimal?(qty13.GetValueOrDefault() * signYtd1) : new Decimal?();
    int num28 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<ItemCostHist.finYtdQty, INItemCostHist.tranYtdQty>(graph28, (object) data28, nullable28, num28 != 0);
    PXGraph graph29 = graph;
    ItemCostHist data29 = itemCostHist2;
    Decimal? tranCost13 = tran.TranCost;
    Decimal signYtd2 = bucket.SignYtd;
    Decimal? nullable29 = tranCost13.HasValue ? new Decimal?(tranCost13.GetValueOrDefault() * signYtd2) : new Decimal?();
    int num29 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<ItemCostHist.finYtdCost, INItemCostHist.tranYtdCost>(graph29, (object) data29, nullable29, num29 != 0);
  }

  public static void UpdateCostHist(PXGraph graph, INTranCost costtran, INTran intran)
  {
    INReleaseProcess.INHistBucket bucket = new INReleaseProcess.INHistBucket(costtran, intran);
    INReleaseProcess.UpdateCostHist(graph, bucket, costtran, intran.SiteID, costtran.FinPeriodID, true);
    INReleaseProcess.UpdateCostHist(graph, bucket, costtran, intran.SiteID, costtran.TranPeriodID, false);
  }

  protected virtual void UpdateCostHist(INTranCost costtran, INTran intran)
  {
    INReleaseProcess.UpdateCostHist((PXGraph) this, costtran, intran);
  }

  protected virtual void UpdateSalesHist(
    INReleaseProcess.INHistBucket bucket,
    INTranCost tran,
    string PeriodID,
    bool FinFlag)
  {
    ItemSalesHist itemSalesHist1 = new ItemSalesHist();
    itemSalesHist1.InventoryID = tran.InventoryID;
    itemSalesHist1.CostSiteID = tran.CostSiteID;
    itemSalesHist1.CostSubItemID = tran.CostSubItemID;
    itemSalesHist1.FinPeriodID = PeriodID;
    ItemSalesHist itemSalesHist2 = ((PXSelectBase<ItemSalesHist>) this.itemsaleshist).Insert(itemSalesHist1);
    ItemSalesHist data1 = itemSalesHist2;
    Decimal? tranCost = tran.TranCost;
    Decimal signSales1 = bucket.SignSales;
    Decimal? nullable1 = tranCost.HasValue ? new Decimal?(tranCost.GetValueOrDefault() * signSales1) : new Decimal?();
    int num1 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdCOGS, INItemSalesHist.tranPtdCOGS>((PXGraph) this, (object) data1, nullable1, num1 != 0);
    ItemSalesHist data2 = itemSalesHist2;
    Decimal? nullable2 = tran.TranCost;
    Decimal signCreditMemos1 = bucket.SignCreditMemos;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signCreditMemos1) : new Decimal?();
    int num2 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdCOGSCredits, INItemSalesHist.tranPtdCOGSCredits>((PXGraph) this, (object) data2, nullable3, num2 != 0);
    ItemSalesHist data3 = itemSalesHist2;
    nullable2 = tran.TranCost;
    Decimal signDropShip1 = bucket.SignDropShip;
    Decimal? nullable4 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signDropShip1) : new Decimal?();
    int num3 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdCOGSDropShips, INItemSalesHist.tranPtdCOGSDropShips>((PXGraph) this, (object) data3, nullable4, num3 != 0);
    ItemSalesHist data4 = itemSalesHist2;
    nullable2 = tran.Qty;
    Decimal signSales2 = bucket.SignSales;
    Decimal? nullable5 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signSales2) : new Decimal?();
    int num4 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdQtySales, INItemSalesHist.tranPtdQtySales>((PXGraph) this, (object) data4, nullable5, num4 != 0);
    ItemSalesHist data5 = itemSalesHist2;
    nullable2 = tran.Qty;
    Decimal signCreditMemos2 = bucket.SignCreditMemos;
    Decimal? nullable6 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signCreditMemos2) : new Decimal?();
    int num5 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdQtyCreditMemos, INItemSalesHist.tranPtdQtyCreditMemos>((PXGraph) this, (object) data5, nullable6, num5 != 0);
    ItemSalesHist data6 = itemSalesHist2;
    nullable2 = tran.Qty;
    Decimal signDropShip2 = bucket.SignDropShip;
    Decimal? nullable7 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signDropShip2) : new Decimal?();
    int num6 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdQtyDropShipSales, INItemSalesHist.tranPtdQtyDropShipSales>((PXGraph) this, (object) data6, nullable7, num6 != 0);
    ItemSalesHist data7 = itemSalesHist2;
    nullable2 = tran.TranAmt;
    Decimal signSales3 = bucket.SignSales;
    Decimal? nullable8 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signSales3) : new Decimal?();
    int num7 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdSales, INItemSalesHist.tranPtdSales>((PXGraph) this, (object) data7, nullable8, num7 != 0);
    ItemSalesHist data8 = itemSalesHist2;
    nullable2 = tran.TranAmt;
    Decimal signCreditMemos3 = bucket.SignCreditMemos;
    Decimal? nullable9 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signCreditMemos3) : new Decimal?();
    int num8 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdCreditMemos, INItemSalesHist.tranPtdCreditMemos>((PXGraph) this, (object) data8, nullable9, num8 != 0);
    ItemSalesHist data9 = itemSalesHist2;
    nullable2 = tran.TranAmt;
    Decimal signDropShip3 = bucket.SignDropShip;
    Decimal? nullable10 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signDropShip3) : new Decimal?();
    int num9 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finPtdDropShipSales, INItemSalesHist.tranPtdDropShipSales>((PXGraph) this, (object) data9, nullable10, num9 != 0);
    ItemSalesHist data10 = itemSalesHist2;
    nullable2 = tran.TranCost;
    Decimal signSales4 = bucket.SignSales;
    Decimal? nullable11 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signSales4) : new Decimal?();
    int num10 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdCOGS, INItemSalesHist.tranYtdCOGS>((PXGraph) this, (object) data10, nullable11, num10 != 0);
    ItemSalesHist data11 = itemSalesHist2;
    nullable2 = tran.TranCost;
    Decimal signCreditMemos4 = bucket.SignCreditMemos;
    Decimal? nullable12 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signCreditMemos4) : new Decimal?();
    int num11 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdCOGSCredits, INItemSalesHist.tranYtdCOGSCredits>((PXGraph) this, (object) data11, nullable12, num11 != 0);
    ItemSalesHist data12 = itemSalesHist2;
    nullable2 = tran.TranCost;
    Decimal signDropShip4 = bucket.SignDropShip;
    Decimal? nullable13 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signDropShip4) : new Decimal?();
    int num12 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdCOGSDropShips, INItemSalesHist.tranYtdCOGSDropShips>((PXGraph) this, (object) data12, nullable13, num12 != 0);
    ItemSalesHist data13 = itemSalesHist2;
    nullable2 = tran.Qty;
    Decimal signSales5 = bucket.SignSales;
    Decimal? nullable14 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signSales5) : new Decimal?();
    int num13 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdQtySales, INItemSalesHist.tranYtdQtySales>((PXGraph) this, (object) data13, nullable14, num13 != 0);
    ItemSalesHist data14 = itemSalesHist2;
    nullable2 = tran.Qty;
    Decimal signCreditMemos5 = bucket.SignCreditMemos;
    Decimal? nullable15 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signCreditMemos5) : new Decimal?();
    int num14 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdQtyCreditMemos, INItemSalesHist.tranYtdQtyCreditMemos>((PXGraph) this, (object) data14, nullable15, num14 != 0);
    ItemSalesHist data15 = itemSalesHist2;
    nullable2 = tran.Qty;
    Decimal signDropShip5 = bucket.SignDropShip;
    Decimal? nullable16 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signDropShip5) : new Decimal?();
    int num15 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdQtyDropShipSales, INItemSalesHist.tranYtdQtyDropShipSales>((PXGraph) this, (object) data15, nullable16, num15 != 0);
    ItemSalesHist data16 = itemSalesHist2;
    nullable2 = tran.TranAmt;
    Decimal signSales6 = bucket.SignSales;
    Decimal? nullable17 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signSales6) : new Decimal?();
    int num16 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdSales, INItemSalesHist.tranYtdSales>((PXGraph) this, (object) data16, nullable17, num16 != 0);
    ItemSalesHist data17 = itemSalesHist2;
    nullable2 = tran.TranAmt;
    Decimal signCreditMemos6 = bucket.SignCreditMemos;
    Decimal? nullable18 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signCreditMemos6) : new Decimal?();
    int num17 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdCreditMemos, INItemSalesHist.tranYtdCreditMemos>((PXGraph) this, (object) data17, nullable18, num17 != 0);
    ItemSalesHist data18 = itemSalesHist2;
    nullable2 = tran.TranAmt;
    Decimal signDropShip6 = bucket.SignDropShip;
    Decimal? nullable19 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * signDropShip6) : new Decimal?();
    int num18 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSalesHist.finYtdDropShipSales, INItemSalesHist.tranYtdDropShipSales>((PXGraph) this, (object) data18, nullable19, num18 != 0);
  }

  protected virtual void UpdateSalesHist(INTranCost costtran, INTran intran)
  {
    INReleaseProcess.INHistBucket bucket = new INReleaseProcess.INHistBucket(costtran, intran);
    this.UpdateSalesHist(bucket, costtran, costtran.FinPeriodID, true);
    this.UpdateSalesHist(bucket, costtran, costtran.TranPeriodID, false);
  }

  protected virtual void UpdateSalesHistD(INTran intran)
  {
    INReleaseProcess.UpdateSalesHistD((PXGraph) this, intran);
  }

  public static void UpdateSalesHistD(PXGraph graph, INTran intran)
  {
    INReleaseProcess.INHistBucket inHistBucket = new INReleaseProcess.INHistBucket(intran);
    if (!intran.TranDate.HasValue)
      return;
    Decimal? baseQty1 = intran.BaseQty;
    Decimal signSales1 = inHistBucket.SignSales;
    Decimal? nullable1 = baseQty1.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() * signSales1) : new Decimal?();
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() <= num & nullable1.HasValue || !intran.SubItemID.HasValue)
      return;
    ItemSalesHistD itemSalesHistD1 = new ItemSalesHistD();
    itemSalesHistD1.InventoryID = intran.InventoryID;
    itemSalesHistD1.SiteID = intran.SiteID;
    itemSalesHistD1.SubItemID = intran.SubItemID;
    itemSalesHistD1.SDate = intran.TranDate;
    ItemSalesHistD itemSalesHistD2 = (ItemSalesHistD) graph.Caches[typeof (ItemSalesHistD)].Insert((object) itemSalesHistD1);
    DateTime date = intran.TranDate.Value;
    itemSalesHistD2.SYear = new int?(date.Year);
    itemSalesHistD2.SMonth = new int?(date.Month);
    itemSalesHistD2.SDay = new int?(date.Day);
    itemSalesHistD2.SQuater = new int?((date.Month + 2) / 3);
    itemSalesHistD2.SDayOfWeek = new int?((int) date.DayOfWeek);
    ItemSalesHistD itemSalesHistD3 = itemSalesHistD2;
    Decimal? qtyIssues = itemSalesHistD3.QtyIssues;
    Decimal? baseQty2 = intran.BaseQty;
    Decimal signSales2 = inHistBucket.SignSales;
    Decimal? nullable2 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signSales2) : new Decimal?();
    itemSalesHistD3.QtyIssues = qtyIssues.HasValue & nullable2.HasValue ? new Decimal?(qtyIssues.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    INItemSite inItemSite = INReleaseProcess.SelectItemSite(graph, intran.InventoryID, intran.SiteID);
    if (inItemSite == null || inItemSite.ReplenishmentPolicyID == null)
      return;
    INReplenishmentPolicy replenishmentPolicy = INReplenishmentPolicy.PK.Find(graph, inItemSite.ReplenishmentPolicyID);
    if (replenishmentPolicy == null || replenishmentPolicy.CalendarID == null)
      return;
    PXResult<CSCalendar, CSCalendarExceptions> pxResult = (PXResult<CSCalendar, CSCalendarExceptions>) PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelectJoin<CSCalendar, LeftJoin<CSCalendarExceptions, On<CSCalendarExceptions.calendarID, Equal<CSCalendar.calendarID>, And<CSCalendarExceptions.date, Equal<Required<CSCalendarExceptions.date>>>>>, Where<CSCalendar.calendarID, Equal<Required<CSCalendar.calendarID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) date,
      (object) replenishmentPolicy.CalendarID
    }));
    if (pxResult == null)
      return;
    CSCalendar csCalendar = PXResult<CSCalendar, CSCalendarExceptions>.op_Implicit(pxResult);
    CSCalendarExceptions calendarExceptions = PXResult<CSCalendar, CSCalendarExceptions>.op_Implicit(pxResult);
    if (calendarExceptions.Date.HasValue)
    {
      itemSalesHistD2.DemandType1 = new int?(calendarExceptions.WorkDay.GetValueOrDefault() ? 1 : 0);
      itemSalesHistD2.DemandType2 = new int?(!calendarExceptions.WorkDay.GetValueOrDefault() ? 1 : 0);
    }
    else
    {
      itemSalesHistD2.DemandType1 = new int?(csCalendar.IsWorkDay(date) ? 1 : 0);
      itemSalesHistD2.DemandType2 = new int?(!csCalendar.IsWorkDay(date) ? 1 : 0);
    }
  }

  protected virtual void UpdateCustSalesStats(INTran intran)
  {
    INReleaseProcess.UpdateCustSalesStats((PXGraph) this, intran);
  }

  public static void UpdateCustSalesStats(PXGraph graph, INTran intran)
  {
    INReleaseProcess.INHistBucket inHistBucket = new INReleaseProcess.INHistBucket(intran);
    if (!intran.TranDate.HasValue)
      return;
    Decimal? baseQty = intran.BaseQty;
    Decimal num = 0M;
    if (baseQty.GetValueOrDefault() == num & baseQty.HasValue || !intran.BAccountID.HasValue || !intran.SubItemID.HasValue || intran.ARRefNbr == null)
      return;
    if (inHistBucket.SignSales != 0M)
    {
      INReleaseProcess.UpdateCustStats<ItemCustSalesStats, INItemCustSalesStats.lastDate, INItemCustSalesStats.lastQty, INItemCustSalesStats.lastUnitPrice>(graph, intran);
    }
    else
    {
      if (!(inHistBucket.SignDropShip != 0M))
        return;
      INReleaseProcess.UpdateCustStats<ItemCustDropShipStats, INItemCustSalesStats.dropShipLastDate, INItemCustSalesStats.dropShipLastQty, INItemCustSalesStats.dropShipLastUnitPrice>(graph, intran);
    }
  }

  private static void UpdateCustStats<TStatus, TLastDate, TLastQty, TLastUnitPrice>(
    PXGraph graph,
    INTran intran)
    where TStatus : INItemCustSalesStats, new()
    where TLastDate : IBqlField
    where TLastQty : IBqlField
    where TLastUnitPrice : IBqlField
  {
    TStatus status1 = new TStatus();
    status1.InventoryID = intran.InventoryID;
    status1.SubItemID = intran.SubItemID;
    status1.SiteID = intran.SiteID;
    status1.BAccountID = intran.BAccountID;
    PXCache cach = graph.Caches[typeof (TStatus)];
    TStatus status2 = (TStatus) cach.Insert((object) status1);
    DateTime? nullable1 = (DateTime?) cach.GetValue<TLastDate>((object) status2);
    if (nullable1.HasValue)
    {
      DateTime? nullable2 = nullable1;
      DateTime? tranDate = intran.TranDate;
      if ((nullable2.HasValue & tranDate.HasValue ? (nullable2.GetValueOrDefault() < tranDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    cach.SetValue<TLastDate>((object) status2, (object) intran.TranDate);
    cach.SetValue<TLastQty>((object) status2, (object) intran.BaseQty);
    Decimal? nullable3;
    if (!(Math.Abs(intran.TranAmt.Value - PXCurrencyAttribute.BaseRound(cach.Graph, intran.BaseQty.Value * intran.UnitPrice.Value)) < 0.00005M))
    {
      Decimal? tranAmt = intran.TranAmt;
      Decimal? baseQty = intran.BaseQty;
      nullable3 = tranAmt.HasValue & baseQty.HasValue ? new Decimal?(tranAmt.GetValueOrDefault() / baseQty.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable3 = intran.UnitPrice;
    Decimal? nullable4 = nullable3;
    cach.SetValue<TLastUnitPrice>((object) status2, (object) nullable4);
  }

  protected virtual void UpdateCustSalesHist(
    INReleaseProcess.INHistBucket bucket,
    INTranCost tran,
    string PeriodID,
    bool FinFlag,
    INTran intran)
  {
    if (!intran.BAccountID.HasValue)
      return;
    ItemCustSalesHist itemCustSalesHist1 = new ItemCustSalesHist();
    itemCustSalesHist1.InventoryID = tran.InventoryID;
    itemCustSalesHist1.CostSiteID = tran.CostSiteID;
    itemCustSalesHist1.CostSubItemID = tran.CostSubItemID;
    itemCustSalesHist1.FinPeriodID = PeriodID;
    itemCustSalesHist1.BAccountID = intran.BAccountID;
    ItemCustSalesHist itemCustSalesHist2 = ((PXSelectBase<ItemCustSalesHist>) this.itemcustsaleshist).Insert(itemCustSalesHist1);
    ItemCustSalesHist data1 = itemCustSalesHist2;
    Decimal? nullable1 = tran.TranCost;
    Decimal signSales1 = bucket.SignSales;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signSales1) : new Decimal?();
    int num1 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdCOGS, INItemCustSalesHist.tranPtdCOGS>((PXGraph) this, (object) data1, nullable2, num1 != 0);
    ItemCustSalesHist data2 = itemCustSalesHist2;
    nullable1 = tran.TranCost;
    Decimal signCreditMemos1 = bucket.SignCreditMemos;
    Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signCreditMemos1) : new Decimal?();
    int num2 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdCOGSCredits, INItemCustSalesHist.tranPtdCOGSCredits>((PXGraph) this, (object) data2, nullable3, num2 != 0);
    ItemCustSalesHist data3 = itemCustSalesHist2;
    nullable1 = tran.TranCost;
    Decimal signDropShip1 = bucket.SignDropShip;
    Decimal? nullable4 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signDropShip1) : new Decimal?();
    int num3 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdCOGSDropShips, INItemCustSalesHist.tranPtdCOGSDropShips>((PXGraph) this, (object) data3, nullable4, num3 != 0);
    ItemCustSalesHist data4 = itemCustSalesHist2;
    nullable1 = tran.Qty;
    Decimal signSales2 = bucket.SignSales;
    Decimal? nullable5 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signSales2) : new Decimal?();
    int num4 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdQtySales, INItemCustSalesHist.tranPtdQtySales>((PXGraph) this, (object) data4, nullable5, num4 != 0);
    ItemCustSalesHist data5 = itemCustSalesHist2;
    nullable1 = tran.Qty;
    Decimal signCreditMemos2 = bucket.SignCreditMemos;
    Decimal? nullable6 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signCreditMemos2) : new Decimal?();
    int num5 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdQtyCreditMemos, INItemCustSalesHist.tranPtdQtyCreditMemos>((PXGraph) this, (object) data5, nullable6, num5 != 0);
    ItemCustSalesHist data6 = itemCustSalesHist2;
    nullable1 = tran.Qty;
    Decimal signDropShip2 = bucket.SignDropShip;
    Decimal? nullable7 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signDropShip2) : new Decimal?();
    int num6 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdQtyDropShipSales, INItemCustSalesHist.tranPtdQtyDropShipSales>((PXGraph) this, (object) data6, nullable7, num6 != 0);
    ItemCustSalesHist data7 = itemCustSalesHist2;
    nullable1 = tran.TranAmt;
    Decimal signSales3 = bucket.SignSales;
    Decimal? nullable8 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signSales3) : new Decimal?();
    int num7 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdSales, INItemCustSalesHist.tranPtdSales>((PXGraph) this, (object) data7, nullable8, num7 != 0);
    ItemCustSalesHist data8 = itemCustSalesHist2;
    nullable1 = tran.TranAmt;
    Decimal signCreditMemos3 = bucket.SignCreditMemos;
    Decimal? nullable9 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signCreditMemos3) : new Decimal?();
    int num8 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdCreditMemos, INItemCustSalesHist.tranPtdCreditMemos>((PXGraph) this, (object) data8, nullable9, num8 != 0);
    ItemCustSalesHist data9 = itemCustSalesHist2;
    nullable1 = tran.TranAmt;
    Decimal signDropShip3 = bucket.SignDropShip;
    Decimal? nullable10 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signDropShip3) : new Decimal?();
    int num9 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finPtdDropShipSales, INItemCustSalesHist.tranPtdDropShipSales>((PXGraph) this, (object) data9, nullable10, num9 != 0);
    ItemCustSalesHist data10 = itemCustSalesHist2;
    nullable1 = tran.TranCost;
    Decimal signSales4 = bucket.SignSales;
    Decimal? nullable11 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signSales4) : new Decimal?();
    int num10 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdCOGS, INItemCustSalesHist.tranYtdCOGS>((PXGraph) this, (object) data10, nullable11, num10 != 0);
    ItemCustSalesHist data11 = itemCustSalesHist2;
    nullable1 = tran.TranCost;
    Decimal signCreditMemos4 = bucket.SignCreditMemos;
    Decimal? nullable12 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signCreditMemos4) : new Decimal?();
    int num11 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdCOGSCredits, INItemCustSalesHist.tranYtdCOGSCredits>((PXGraph) this, (object) data11, nullable12, num11 != 0);
    ItemCustSalesHist data12 = itemCustSalesHist2;
    nullable1 = tran.TranCost;
    Decimal signDropShip4 = bucket.SignDropShip;
    Decimal? nullable13 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signDropShip4) : new Decimal?();
    int num12 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdCOGSDropShips, INItemCustSalesHist.tranYtdCOGSDropShips>((PXGraph) this, (object) data12, nullable13, num12 != 0);
    ItemCustSalesHist data13 = itemCustSalesHist2;
    nullable1 = tran.Qty;
    Decimal signSales5 = bucket.SignSales;
    Decimal? nullable14 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signSales5) : new Decimal?();
    int num13 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdQtySales, INItemCustSalesHist.tranYtdQtySales>((PXGraph) this, (object) data13, nullable14, num13 != 0);
    ItemCustSalesHist data14 = itemCustSalesHist2;
    nullable1 = tran.Qty;
    Decimal signCreditMemos5 = bucket.SignCreditMemos;
    Decimal? nullable15 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signCreditMemos5) : new Decimal?();
    int num14 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdQtyCreditMemos, INItemCustSalesHist.tranYtdQtyCreditMemos>((PXGraph) this, (object) data14, nullable15, num14 != 0);
    ItemCustSalesHist data15 = itemCustSalesHist2;
    nullable1 = tran.Qty;
    Decimal signDropShip5 = bucket.SignDropShip;
    Decimal? nullable16 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signDropShip5) : new Decimal?();
    int num15 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdQtyDropShipSales, INItemCustSalesHist.tranYtdQtyDropShipSales>((PXGraph) this, (object) data15, nullable16, num15 != 0);
    ItemCustSalesHist data16 = itemCustSalesHist2;
    nullable1 = tran.TranAmt;
    Decimal signSales6 = bucket.SignSales;
    Decimal? nullable17 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signSales6) : new Decimal?();
    int num16 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdSales, INItemCustSalesHist.tranYtdSales>((PXGraph) this, (object) data16, nullable17, num16 != 0);
    ItemCustSalesHist data17 = itemCustSalesHist2;
    nullable1 = tran.TranAmt;
    Decimal signCreditMemos6 = bucket.SignCreditMemos;
    Decimal? nullable18 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signCreditMemos6) : new Decimal?();
    int num17 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdCreditMemos, INItemCustSalesHist.tranYtdCreditMemos>((PXGraph) this, (object) data17, nullable18, num17 != 0);
    ItemCustSalesHist data18 = itemCustSalesHist2;
    nullable1 = tran.TranAmt;
    Decimal signDropShip6 = bucket.SignDropShip;
    Decimal? nullable19 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * signDropShip6) : new Decimal?();
    int num18 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemCustSalesHist.finYtdDropShipSales, INItemCustSalesHist.tranYtdDropShipSales>((PXGraph) this, (object) data18, nullable19, num18 != 0);
  }

  protected virtual void UpdateCustSalesHist(INTranCost costtran, INTran intran)
  {
    INReleaseProcess.INHistBucket bucket = new INReleaseProcess.INHistBucket(costtran, intran);
    this.UpdateCustSalesHist(bucket, costtran, costtran.FinPeriodID, true, intran);
    this.UpdateCustSalesHist(bucket, costtran, costtran.TranPeriodID, false, intran);
  }

  protected static void UpdateSiteHist(
    PXGraph graph,
    INReleaseProcess.INHistBucket bucket,
    INTranSplit tran,
    string PeriodID,
    bool FinFlag)
  {
    ItemSiteHist itemSiteHist1 = new ItemSiteHist();
    itemSiteHist1.InventoryID = tran.InventoryID;
    itemSiteHist1.SiteID = tran.SiteID;
    itemSiteHist1.SubItemID = tran.SubItemID;
    itemSiteHist1.LocationID = tran.LocationID;
    itemSiteHist1.FinPeriodID = PeriodID;
    ItemSiteHist itemSiteHist2 = (ItemSiteHist) graph.Caches[typeof (ItemSiteHist)].Insert((object) itemSiteHist1);
    PXGraph graph1 = graph;
    ItemSiteHist data1 = itemSiteHist2;
    Decimal? baseQty1 = tran.BaseQty;
    Decimal signReceived = bucket.SignReceived;
    Decimal? nullable1 = baseQty1.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() * signReceived) : new Decimal?();
    int num1 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyReceived, INItemSiteHist.tranPtdQtyReceived>(graph1, (object) data1, nullable1, num1 != 0);
    PXGraph graph2 = graph;
    ItemSiteHist data2 = itemSiteHist2;
    Decimal? baseQty2 = tran.BaseQty;
    Decimal signIssued = bucket.SignIssued;
    Decimal? nullable2 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signIssued) : new Decimal?();
    int num2 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyIssued, INItemSiteHist.tranPtdQtyIssued>(graph2, (object) data2, nullable2, num2 != 0);
    PXGraph graph3 = graph;
    ItemSiteHist data3 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signSales = bucket.SignSales;
    Decimal? nullable3 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signSales) : new Decimal?();
    int num3 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtySales, INItemSiteHist.tranPtdQtySales>(graph3, (object) data3, nullable3, num3 != 0);
    PXGraph graph4 = graph;
    ItemSiteHist data4 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signCreditMemos = bucket.SignCreditMemos;
    Decimal? nullable4 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signCreditMemos) : new Decimal?();
    int num4 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyCreditMemos, INItemSiteHist.tranPtdQtyCreditMemos>(graph4, (object) data4, nullable4, num4 != 0);
    PXGraph graph5 = graph;
    ItemSiteHist data5 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signDropShip = bucket.SignDropShip;
    Decimal? nullable5 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signDropShip) : new Decimal?();
    int num5 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyDropShipSales, INItemSiteHist.tranPtdQtyDropShipSales>(graph5, (object) data5, nullable5, num5 != 0);
    PXGraph graph6 = graph;
    ItemSiteHist data6 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signTransferIn = bucket.SignTransferIn;
    Decimal? nullable6 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signTransferIn) : new Decimal?();
    int num6 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyTransferIn, INItemSiteHist.tranPtdQtyTransferIn>(graph6, (object) data6, nullable6, num6 != 0);
    PXGraph graph7 = graph;
    ItemSiteHist data7 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signTransferOut = bucket.SignTransferOut;
    Decimal? nullable7 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signTransferOut) : new Decimal?();
    int num7 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyTransferOut, INItemSiteHist.tranPtdQtyTransferOut>(graph7, (object) data7, nullable7, num7 != 0);
    PXGraph graph8 = graph;
    ItemSiteHist data8 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signAdjusted = bucket.SignAdjusted;
    Decimal? nullable8 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signAdjusted) : new Decimal?();
    int num8 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyAdjusted, INItemSiteHist.tranPtdQtyAdjusted>(graph8, (object) data8, nullable8, num8 != 0);
    PXGraph graph9 = graph;
    ItemSiteHist data9 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signAssemblyIn = bucket.SignAssemblyIn;
    Decimal? nullable9 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signAssemblyIn) : new Decimal?();
    int num9 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyAssemblyIn, INItemSiteHist.tranPtdQtyAssemblyIn>(graph9, (object) data9, nullable9, num9 != 0);
    PXGraph graph10 = graph;
    ItemSiteHist data10 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signAssemblyOut = bucket.SignAssemblyOut;
    Decimal? nullable10 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signAssemblyOut) : new Decimal?();
    int num10 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finPtdQtyAssemblyOut, INItemSiteHist.tranPtdQtyAssemblyOut>(graph10, (object) data10, nullable10, num10 != 0);
    PXGraph graph11 = graph;
    ItemSiteHist data11 = itemSiteHist2;
    baseQty2 = tran.BaseQty;
    Decimal signYtd = bucket.SignYtd;
    Decimal? nullable11 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signYtd) : new Decimal?();
    int num11 = FinFlag ? 1 : 0;
    INReleaseProcess.UpdateHistoryField<INItemSiteHist.finYtdQty, INItemSiteHist.tranYtdQty>(graph11, (object) data11, nullable11, num11 != 0);
  }

  protected virtual void UpdateSiteHistDay(INTran tran, INTranSplit split)
  {
    INReleaseProcess.UpdateSiteHistDay((PXGraph) this, tran, split);
  }

  public static void UpdateSiteHistDay(PXGraph graph, INTran tran, INTranSplit split)
  {
    INReleaseProcess.INHistBucket inHistBucket = new INReleaseProcess.INHistBucket(split);
    ItemSiteHistDay itemSiteHistDay = new ItemSiteHistDay();
    itemSiteHistDay.InventoryID = split.InventoryID;
    itemSiteHistDay.SiteID = split.SiteID;
    itemSiteHistDay.SubItemID = split.SubItemID;
    itemSiteHistDay.LocationID = split.LocationID;
    DateTime dateTime = split.TranDate.Value;
    itemSiteHistDay.SDate = new DateTime?(dateTime);
    ItemSiteHistDay data1 = (ItemSiteHistDay) graph.Caches[typeof (ItemSiteHistDay)].Insert((object) itemSiteHistDay);
    PXGraph graph1 = graph;
    ItemSiteHistDay data2 = data1;
    Decimal? baseQty1 = split.BaseQty;
    Decimal signReceived = inHistBucket.SignReceived;
    Decimal? nullable1 = baseQty1.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() * signReceived) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyReceived, INItemSiteHistDay.qtyReceived>(graph1, (object) data2, nullable1, true);
    PXGraph graph2 = graph;
    ItemSiteHistDay data3 = data1;
    Decimal? baseQty2 = split.BaseQty;
    Decimal signIssued = inHistBucket.SignIssued;
    Decimal? nullable2 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * signIssued) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyIssued, INItemSiteHistDay.qtyIssued>(graph2, (object) data3, nullable2, true);
    PXGraph graph3 = graph;
    ItemSiteHistDay data4 = data1;
    Decimal? baseQty3 = split.BaseQty;
    Decimal signSales = inHistBucket.SignSales;
    Decimal? nullable3 = baseQty3.HasValue ? new Decimal?(baseQty3.GetValueOrDefault() * signSales) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtySales, INItemSiteHistDay.qtySales>(graph3, (object) data4, nullable3, true);
    PXGraph graph4 = graph;
    ItemSiteHistDay data5 = data1;
    Decimal? baseQty4 = split.BaseQty;
    Decimal signCreditMemos = inHistBucket.SignCreditMemos;
    Decimal? nullable4 = baseQty4.HasValue ? new Decimal?(baseQty4.GetValueOrDefault() * signCreditMemos) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyCreditMemos, INItemSiteHistDay.qtyCreditMemos>(graph4, (object) data5, nullable4, true);
    PXGraph graph5 = graph;
    ItemSiteHistDay data6 = data1;
    Decimal? baseQty5 = split.BaseQty;
    Decimal signDropShip = inHistBucket.SignDropShip;
    Decimal? nullable5 = baseQty5.HasValue ? new Decimal?(baseQty5.GetValueOrDefault() * signDropShip) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyDropShipSales, INItemSiteHistDay.qtyDropShipSales>(graph5, (object) data6, nullable5, true);
    PXGraph graph6 = graph;
    ItemSiteHistDay data7 = data1;
    Decimal? baseQty6 = split.BaseQty;
    Decimal signTransferIn = inHistBucket.SignTransferIn;
    Decimal? nullable6 = baseQty6.HasValue ? new Decimal?(baseQty6.GetValueOrDefault() * signTransferIn) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyTransferIn, INItemSiteHistDay.qtyTransferIn>(graph6, (object) data7, nullable6, true);
    PXGraph graph7 = graph;
    ItemSiteHistDay data8 = data1;
    Decimal? baseQty7 = split.BaseQty;
    Decimal signTransferOut = inHistBucket.SignTransferOut;
    Decimal? nullable7 = baseQty7.HasValue ? new Decimal?(baseQty7.GetValueOrDefault() * signTransferOut) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyTransferOut, INItemSiteHistDay.qtyTransferOut>(graph7, (object) data8, nullable7, true);
    PXGraph graph8 = graph;
    ItemSiteHistDay data9 = data1;
    Decimal? baseQty8 = split.BaseQty;
    Decimal signAdjusted = inHistBucket.SignAdjusted;
    Decimal? nullable8 = baseQty8.HasValue ? new Decimal?(baseQty8.GetValueOrDefault() * signAdjusted) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyAdjusted, INItemSiteHistDay.qtyAdjusted>(graph8, (object) data9, nullable8, true);
    PXGraph graph9 = graph;
    ItemSiteHistDay data10 = data1;
    Decimal? baseQty9 = split.BaseQty;
    Decimal signAssemblyIn = inHistBucket.SignAssemblyIn;
    Decimal? nullable9 = baseQty9.HasValue ? new Decimal?(baseQty9.GetValueOrDefault() * signAssemblyIn) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyAssemblyIn, INItemSiteHistDay.qtyAssemblyIn>(graph9, (object) data10, nullable9, true);
    PXGraph graph10 = graph;
    ItemSiteHistDay data11 = data1;
    Decimal? baseQty10 = split.BaseQty;
    Decimal signAssemblyOut = inHistBucket.SignAssemblyOut;
    Decimal? nullable10 = baseQty10.HasValue ? new Decimal?(baseQty10.GetValueOrDefault() * signAssemblyOut) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyAssemblyOut, INItemSiteHistDay.qtyAssemblyOut>(graph10, (object) data11, nullable10, true);
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyDebit, INItemSiteHistDay.qtyDebit>(graph, (object) data1, inHistBucket.SignYtd > 0M ? split.BaseQty : new Decimal?(0M), true);
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.qtyCredit, INItemSiteHistDay.qtyCredit>(graph, (object) data1, inHistBucket.SignYtd < 0M ? split.BaseQty : new Decimal?(0M), true);
    PXGraph graph11 = graph;
    ItemSiteHistDay data12 = data1;
    Decimal? baseQty11 = split.BaseQty;
    Decimal signYtd = inHistBucket.SignYtd;
    Decimal? nullable11 = baseQty11.HasValue ? new Decimal?(baseQty11.GetValueOrDefault() * signYtd) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistDay.endQty, INItemSiteHistDay.endQty>(graph11, (object) data12, nullable11, true);
  }

  public static void UpdateSiteHist(PXGraph graph, INTran tran, INTranSplit split)
  {
    INReleaseProcess.INHistBucket bucket = new INReleaseProcess.INHistBucket(split);
    INReleaseProcess.UpdateSiteHist(graph, bucket, split, tran.FinPeriodID, true);
    INReleaseProcess.UpdateSiteHist(graph, bucket, split, tran.TranPeriodID, false);
  }

  protected virtual void UpdateSiteHist(INTran tran, INTranSplit split)
  {
    INReleaseProcess.UpdateSiteHist((PXGraph) this, tran, split);
  }

  public static void UpdateSiteHistByCostCenterD(PXGraph graph, INTran tran, INTranSplit split)
  {
    INReleaseProcess.INHistBucket inHistBucket = new INReleaseProcess.INHistBucket(split);
    ItemSiteHistByCostCenterD histByCostCenterD1 = new ItemSiteHistByCostCenterD();
    histByCostCenterD1.InventoryID = split.InventoryID;
    histByCostCenterD1.SiteID = split.SiteID;
    histByCostCenterD1.SubItemID = split.SubItemID;
    histByCostCenterD1.CostCenterID = tran.CostCenterID;
    histByCostCenterD1.SDate = split.TranDate;
    ItemSiteHistByCostCenterD histByCostCenterD2 = histByCostCenterD1;
    ItemSiteHistByCostCenterD histByCostCenterD3 = GraphHelper.Caches<ItemSiteHistByCostCenterD>(graph).Insert(histByCostCenterD2);
    INReleaseProcess.FillDateFields(histByCostCenterD3, split.TranDate.Value);
    if (split.TranType == "TRX")
    {
      short? invtMult = split.InvtMult;
      if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
      {
        int? siteId = split.SiteID;
        int? toSiteId = split.ToSiteID;
        if (siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue)
        {
          inHistBucket.SignTransferIn = -1M;
          inHistBucket.SignTransferOut = 0M;
        }
      }
    }
    PXGraph graph1 = graph;
    ItemSiteHistByCostCenterD data1 = histByCostCenterD3;
    Decimal? baseQty = split.BaseQty;
    Decimal signReceived = inHistBucket.SignReceived;
    Decimal? nullable1 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signReceived) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyReceived, INItemSiteHistByCostCenterD.qtyReceived>(graph1, (object) data1, nullable1, true);
    PXGraph graph2 = graph;
    ItemSiteHistByCostCenterD data2 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signIssued = inHistBucket.SignIssued;
    Decimal? nullable2 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signIssued) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyIssued, INItemSiteHistByCostCenterD.qtyIssued>(graph2, (object) data2, nullable2, true);
    PXGraph graph3 = graph;
    ItemSiteHistByCostCenterD data3 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signSales = inHistBucket.SignSales;
    Decimal? nullable3 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signSales) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtySales, INItemSiteHistByCostCenterD.qtySales>(graph3, (object) data3, nullable3, true);
    PXGraph graph4 = graph;
    ItemSiteHistByCostCenterD data4 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signCreditMemos = inHistBucket.SignCreditMemos;
    Decimal? nullable4 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signCreditMemos) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyCreditMemos, INItemSiteHistByCostCenterD.qtyCreditMemos>(graph4, (object) data4, nullable4, true);
    PXGraph graph5 = graph;
    ItemSiteHistByCostCenterD data5 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signDropShip = inHistBucket.SignDropShip;
    Decimal? nullable5 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signDropShip) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyDropShipSales, INItemSiteHistByCostCenterD.qtyDropShipSales>(graph5, (object) data5, nullable5, true);
    PXGraph graph6 = graph;
    ItemSiteHistByCostCenterD data6 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signTransferIn = inHistBucket.SignTransferIn;
    Decimal? nullable6 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signTransferIn) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyTransferIn, INItemSiteHistByCostCenterD.qtyTransferIn>(graph6, (object) data6, nullable6, true);
    PXGraph graph7 = graph;
    ItemSiteHistByCostCenterD data7 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signTransferOut = inHistBucket.SignTransferOut;
    Decimal? nullable7 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signTransferOut) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyTransferOut, INItemSiteHistByCostCenterD.qtyTransferOut>(graph7, (object) data7, nullable7, true);
    PXGraph graph8 = graph;
    ItemSiteHistByCostCenterD data8 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signAdjusted = inHistBucket.SignAdjusted;
    Decimal? nullable8 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signAdjusted) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyAdjusted, INItemSiteHistByCostCenterD.qtyAdjusted>(graph8, (object) data8, nullable8, true);
    PXGraph graph9 = graph;
    ItemSiteHistByCostCenterD data9 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signAssemblyIn = inHistBucket.SignAssemblyIn;
    Decimal? nullable9 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signAssemblyIn) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyAssemblyIn, INItemSiteHistByCostCenterD.qtyAssemblyIn>(graph9, (object) data9, nullable9, true);
    PXGraph graph10 = graph;
    ItemSiteHistByCostCenterD data10 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signAssemblyOut = inHistBucket.SignAssemblyOut;
    Decimal? nullable10 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signAssemblyOut) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyAssemblyOut, INItemSiteHistByCostCenterD.qtyAssemblyOut>(graph10, (object) data10, nullable10, true);
    if (split.SkipCostUpdate.GetValueOrDefault())
      return;
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyDebit, INItemSiteHistByCostCenterD.qtyDebit>(graph, (object) histByCostCenterD3, inHistBucket.SignYtd > 0M ? split.BaseQty : new Decimal?(0M), true);
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.qtyCredit, INItemSiteHistByCostCenterD.qtyCredit>(graph, (object) histByCostCenterD3, inHistBucket.SignYtd < 0M ? split.BaseQty : new Decimal?(0M), true);
    PXGraph graph11 = graph;
    ItemSiteHistByCostCenterD data11 = histByCostCenterD3;
    baseQty = split.BaseQty;
    Decimal signYtd = inHistBucket.SignYtd;
    Decimal? nullable11 = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * signYtd) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.endQty, INItemSiteHistByCostCenterD.endQty>(graph11, (object) data11, nullable11, true);
  }

  protected static void FillDateFields(ItemSiteHistByCostCenterD hist, DateTime date)
  {
    hist.SYear = new int?(date.Year);
    hist.SMonth = new int?(date.Month);
    hist.SDay = new int?(date.Day);
    hist.SQuater = new int?((date.Month + 2) / 3);
    hist.SDayOfWeek = new int?((int) date.DayOfWeek);
  }

  protected virtual void UpdateSiteHistByCostCenterD(INTran tran, INTranSplit split)
  {
    INReleaseProcess.UpdateSiteHistByCostCenterD((PXGraph) this, tran, split);
  }

  public static void UpdateSiteHistByCostCenterDCost(
    PXGraph graph,
    INTranCost costtran,
    INTran tran)
  {
    INReleaseProcess.INHistBucket inHistBucket = new INReleaseProcess.INHistBucket(costtran, tran);
    ItemSiteHistByCostCenterD histByCostCenterD1 = new ItemSiteHistByCostCenterD();
    histByCostCenterD1.InventoryID = tran.InventoryID;
    histByCostCenterD1.SiteID = tran.SiteID;
    histByCostCenterD1.CostCenterID = tran.CostCenterID;
    histByCostCenterD1.SubItemID = costtran.CostSubItemID;
    histByCostCenterD1.SDate = tran.TranDate;
    ItemSiteHistByCostCenterD histByCostCenterD2 = histByCostCenterD1;
    ItemSiteHistByCostCenterD histByCostCenterD3 = GraphHelper.Caches<ItemSiteHistByCostCenterD>(graph).Insert(histByCostCenterD2);
    INReleaseProcess.FillDateFields(histByCostCenterD3, tran.TranDate.Value);
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.costDebit, INItemSiteHistByCostCenterD.costDebit>(graph, (object) histByCostCenterD3, inHistBucket.SignYtd > 0M ? costtran.TranCost : new Decimal?(0M), true);
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.costCredit, INItemSiteHistByCostCenterD.costCredit>(graph, (object) histByCostCenterD3, inHistBucket.SignYtd < 0M ? costtran.TranCost : new Decimal?(0M), true);
    PXGraph graph1 = graph;
    ItemSiteHistByCostCenterD data = histByCostCenterD3;
    Decimal signYtd = inHistBucket.SignYtd;
    Decimal? tranCost = costtran.TranCost;
    Decimal? nullable = tranCost.HasValue ? new Decimal?(signYtd * tranCost.GetValueOrDefault()) : new Decimal?();
    INReleaseProcess.UpdateHistoryField<INItemSiteHistByCostCenterD.endCost, INItemSiteHistByCostCenterD.endCost>(graph1, (object) data, nullable, true);
  }

  protected virtual void UpdateSiteHistByCostCenterDCost(INTranCost costtran, INTran tran)
  {
    INReleaseProcess.UpdateSiteHistByCostCenterDCost((PXGraph) this, costtran, tran);
  }

  public int? GetAcctID<Field>(
    string AcctDefault,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass)
    where Field : IBqlField
  {
    return INReleaseProcess.GetAcctID<Field>((PXGraph) this, AcctDefault, InventoryAccountServiceHelper.Params(item, site, postclass));
  }

  public static int? GetAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass)
    where Field : IBqlField
  {
    return INReleaseProcess.GetAcctID<Field>(graph, AcctDefault, InventoryAccountServiceHelper.Params(item, site, postclass));
  }

  public int? GetAcctID<Field>(string AcctDefault, InventoryAccountServiceParams @params) where Field : IBqlField
  {
    return INReleaseProcess.GetAcctID<Field>((PXGraph) this, AcctDefault, @params);
  }

  public static int? GetAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    InventoryAccountServiceParams @params)
    where Field : IBqlField
  {
    return graph.GetService<IInventoryAccountService>().GetAcctID<Field>(graph, AcctDefault, @params);
  }

  public int? GetSubID<Field>(
    string AcctDefault,
    string SubMask,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass)
    where Field : IBqlField
  {
    return INReleaseProcess.GetSubID<Field>((PXGraph) this, AcctDefault, SubMask, InventoryAccountServiceHelper.Params(item, site, postclass));
  }

  public static int? GetSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass)
    where Field : IBqlField
  {
    return INReleaseProcess.GetSubID<Field>(graph, AcctDefault, SubMask, InventoryAccountServiceHelper.Params(item, site, postclass));
  }

  public int? GetSubID<Field>(
    string AcctDefault,
    string SubMask,
    InventoryAccountServiceParams @params)
    where Field : IBqlField
  {
    return INReleaseProcess.GetSubID<Field>((PXGraph) this, AcctDefault, SubMask, @params);
  }

  public static int? GetSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    InventoryAccountServiceParams @params)
    where Field : IBqlField
  {
    return graph.GetService<IInventoryAccountService>().GetSubID<Field>(graph, AcctDefault, SubMask, @params);
  }

  public static int? GetPOAccrualAcctID<Field>(
    PXGraph graph,
    string AcctDefault,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass,
    PX.Objects.AP.Vendor vendor)
    where Field : IBqlField
  {
    return graph.GetService<IInventoryAccountService>().GetPOAccrualAcctID<Field>(graph, AcctDefault, item, site, postclass, vendor);
  }

  public static int? GetPOAccrualSubID<Field>(
    PXGraph graph,
    string AcctDefault,
    string SubMask,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass,
    PX.Objects.AP.Vendor vendor)
    where Field : IBqlField
  {
    return graph.GetService<IInventoryAccountService>().GetPOAccrualSubID<Field>(graph, AcctDefault, SubMask, item, site, postclass, vendor);
  }

  public virtual int? GetReasonCodeSubID(
    PX.Objects.CS.ReasonCode tranreasoncode,
    PX.Objects.CS.ReasonCode defreasoncode,
    InventoryAccountServiceParams inventoryParams)
  {
    return INReleaseProcess.GetReasonCodeSubID((PXGraph) this, !tranreasoncode.AccountID.HasValue ? defreasoncode : tranreasoncode, inventoryParams, typeof (INPostClass.reasonCodeSubID));
  }

  public virtual int? GetReasonCodeSubID(
    PX.Objects.CS.ReasonCode reasoncode,
    InventoryAccountServiceParams inventoryParams)
  {
    return INReleaseProcess.GetReasonCodeSubID((PXGraph) this, reasoncode, inventoryParams);
  }

  public static int? GetReasonCodeSubID(
    PXGraph graph,
    PX.Objects.CS.ReasonCode reasoncode,
    InventoryAccountServiceParams inventoryParams)
  {
    return reasoncode.AccountID.HasValue ? INReleaseProcess.GetReasonCodeSubID(graph, reasoncode, inventoryParams, typeof (INPostClass.reasonCodeSubID)) : new int?();
  }

  private static int? GetReasonCodeSubID(
    PXGraph graph,
    PX.Objects.CS.ReasonCode reasoncode,
    InventoryAccountServiceParams inventoryParams,
    Type fieldType)
  {
    int? nullable1 = (int?) graph.Caches[typeof (PX.Objects.CS.ReasonCode)].GetValue<PX.Objects.CS.ReasonCode.subID>((object) reasoncode);
    int? nullable2 = (int?) graph.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue((object) inventoryParams.Item, fieldType.Name);
    int? nullable3 = (int?) graph.Caches[typeof (PX.Objects.IN.INSite)].GetValue((object) inventoryParams.Site, fieldType.Name);
    int? nullable4 = (int?) graph.Caches[typeof (INPostClass)].GetValue((object) inventoryParams.Postclass, fieldType.Name);
    int? defaultExpenseSubId = (int?) inventoryParams.Project?.DefaultExpenseSubID;
    int? nullable5 = defaultExpenseSubId ?? nullable1;
    defaultExpenseSubId = (int?) inventoryParams.Task?.DefaultExpenseSubID;
    int? nullable6 = defaultExpenseSubId ?? nullable1;
    object reasonCodeSubId = (object) PX.Objects.IN.ReasonCodeSubAccountMaskAttribute.MakeSub<PX.Objects.CS.ReasonCode.subMask>(graph, reasoncode.SubMask, new object[6]
    {
      (object) nullable1,
      (object) nullable2,
      (object) nullable3,
      (object) nullable4,
      (object) nullable5,
      (object) nullable6
    }, new Type[6]
    {
      typeof (PX.Objects.CS.ReasonCode.subID),
      typeof (PX.Objects.IN.InventoryItem.reasonCodeSubID),
      typeof (PX.Objects.IN.INSite.reasonCodeSubID),
      typeof (INPostClass.reasonCodeSubID),
      typeof (PX.Objects.PM.PMProject.defaultExpenseSubID),
      typeof (PX.Objects.PM.PMTask.defaultExpenseSubID)
    });
    PX.Objects.IN.Services.InventoryAccountService.RaiseFieldUpdating<PX.Objects.CS.ReasonCode.subID>(graph.Caches[typeof (PX.Objects.CS.ReasonCode)], (object) reasoncode, ref reasonCodeSubId);
    return (int?) reasonCodeSubId;
  }

  public virtual int? GetCogsAcctID(InventoryAccountServiceParams @params)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.cOGSAcctID>((PXGraph) this, @params);
  }

  public virtual int? GetCogsSubID(InventoryAccountServiceParams @params)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.cOGSSubID>((PXGraph) this, @params);
  }

  public virtual int? GetInvtAcctID(
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass,
    INTran tran,
    bool useTran)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.invtAcctID>((PXGraph) this, InventoryAccountServiceHelper.Params(item, site, postclass, useTran ? tran : (INTran) null));
  }

  public virtual int? GetInvtSubID(
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass,
    INTran tran,
    bool useTran)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.invtSubID>((PXGraph) this, InventoryAccountServiceHelper.Params(item, site, postclass, useTran ? tran : (INTran) null));
  }

  public virtual int? GetSalesAcctID(InventoryAccountServiceParams @params)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.salesAcctID>((PXGraph) this, @params);
  }

  public virtual int? GetSalesSubID(InventoryAccountServiceParams @params)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.salesSubID>((PXGraph) this, @params);
  }

  public virtual int? GetStdCostVarAcctID(
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass,
    INTran tran,
    bool useTran)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.stdCstVarAcctID>((PXGraph) this, InventoryAccountServiceHelper.Params(item, site, postclass, useTran ? tran : (INTran) null));
  }

  public virtual int? GetStdCostVarSubID(
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass,
    INTran tran,
    bool useTran)
  {
    return INReleaseProcess.GetAccountDefaults<INPostClass.stdCstVarSubID>((PXGraph) this, InventoryAccountServiceHelper.Params(item, site, postclass, useTran ? tran : (INTran) null));
  }

  public static int? GetAccountDefaults<Field>(
    PXGraph graph,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass)
    where Field : IBqlField
  {
    return INReleaseProcess.GetAccountDefaults<Field>(graph, InventoryAccountServiceHelper.Params(item, site, postclass, (IProjectAccountsSource) null, (IProjectTaskAccountsSource) null));
  }

  public static int? GetAccountDefaults<Field>(
    PXGraph graph,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass,
    INTran tran)
    where Field : IBqlField
  {
    return INReleaseProcess.GetAccountDefaults<Field>(graph, InventoryAccountServiceHelper.Params(item, site, postclass, tran));
  }

  public static int? GetAccountDefaults<Field>(PXGraph graph, InventoryAccountServiceParams @params) where Field : IBqlField
  {
    if (typeof (Field) == typeof (INPostClass.invtAcctID))
      return INReleaseProcess.GetAcctID<Field>(graph, @params.Item.StkItem.GetValueOrDefault() || !(@params.Postclass.InvtAcctDefault == "W") ? @params.Postclass.InvtAcctDefault : "I", @params);
    if (typeof (Field) == typeof (INPostClass.invtSubID))
      return INReleaseProcess.GetSubID<Field>(graph, @params.Postclass.InvtAcctDefault, @params.Postclass.InvtSubMask, @params);
    if (typeof (Field) == typeof (INPostClass.cOGSAcctID))
      return INReleaseProcess.GetAcctID<Field>(graph, @params.Item.StkItem.GetValueOrDefault() || !(@params.Postclass.COGSAcctDefault == "W") ? @params.Postclass.COGSAcctDefault : "I", @params);
    if (typeof (Field) == typeof (INPostClass.cOGSSubID))
      return INReleaseProcess.GetSubID<Field>(graph, @params.Postclass.COGSAcctDefault, @params.Postclass.COGSSubMask, @params);
    if (typeof (Field) == typeof (INPostClass.salesAcctID))
      return INReleaseProcess.GetAcctID<Field>(graph, @params.Postclass.SalesAcctDefault, @params);
    if (typeof (Field) == typeof (INPostClass.salesSubID))
      return INReleaseProcess.GetSubID<Field>(graph, @params.Postclass.SalesAcctDefault, @params.Postclass.SalesSubMask, @params);
    if (typeof (Field) == typeof (INPostClass.stdCstVarAcctID))
      return INReleaseProcess.GetAcctID<Field>(graph, @params.Postclass.StdCstVarAcctDefault, @params);
    if (typeof (Field) == typeof (INPostClass.stdCstVarSubID))
      return INReleaseProcess.GetSubID<Field>(graph, @params.Postclass.StdCstVarAcctDefault, @params.Postclass.StdCstVarSubMask, @params);
    if (typeof (Field) == typeof (INPostClass.stdCstRevAcctID))
      return INReleaseProcess.GetAcctID<Field>(graph, @params.Postclass.StdCstRevAcctDefault, @params);
    if (typeof (Field) == typeof (INPostClass.stdCstRevSubID))
      return INReleaseProcess.GetSubID<Field>(graph, @params.Postclass.StdCstRevAcctDefault, @params.Postclass.StdCstRevSubMask, @params);
    throw new PXException();
  }

  public static INItemSite SelectItemSite(PXGraph graph, int? InventoryID, int? SiteID)
  {
    return GraphHelper.Caches<INItemSite>(graph).Locate(new INItemSite()
    {
      InventoryID = InventoryID,
      SiteID = SiteID
    }) ?? INItemSite.PK.Find(graph, InventoryID, SiteID);
  }

  public virtual void UpdateSOTransferPlans(long? oldPlanID, long? newPlandID)
  {
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) oldPlanID
    }))
    {
      INItemPlan inItemPlan = PXResult<INItemPlan>.op_Implicit(pxResult);
      PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) this);
      INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan);
      INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this, copy.PlanType);
      ((PXCache) pxCache).SetStatus((object) copy, (PXEntryStatus) 0);
      copy.SupplyPlanID = newPlandID;
      if (inPlanType.ReplanOnEvent == "95")
        copy.PlanType = inPlanType.ReplanOnEvent;
      if (copy.PlanType == "95" && !newPlandID.HasValue)
        pxCache.Delete(copy);
      else
        pxCache.Update(copy);
    }
  }

  public virtual INTransitLine GetCachedTransitLine(int? costsiteid)
  {
    foreach (INTransitLine cachedTransitLine in ((PXSelectBase) this.intransitline).Cache.Cached)
    {
      int? costSiteId = cachedTransitLine.CostSiteID;
      int? nullable = costsiteid;
      if (costSiteId.GetValueOrDefault() == nullable.GetValueOrDefault() & costSiteId.HasValue == nullable.HasValue)
        return cachedTransitLine;
    }
    return (INTransitLine) null;
  }

  public virtual void UpdateTransitPlans()
  {
    if (((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.DocType != "R")
      return;
    List<INTransitLine> inTransitLineList = new List<INTransitLine>();
    List<INItemPlan> inItemPlanList = new List<INItemPlan>();
    foreach (TransitLotSerialStatusByCostCenter statusByCostCenter in ((PXSelectBase) this.transitlotnumberedstatusbycostcenter).Cache.Inserted)
    {
      TransitLotSerialStatusByCostCenter status = statusByCostCenter;
      Decimal? nullable = status.QtyOnHand;
      Decimal num1 = 0M;
      if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      {
        INTransitLine cachedTransitLine = inTransitLineList.Find((Predicate<INTransitLine>) (x =>
        {
          int? costSiteId = x.CostSiteID;
          int? locationId = status.LocationID;
          return costSiteId.GetValueOrDefault() == locationId.GetValueOrDefault() & costSiteId.HasValue == locationId.HasValue;
        }));
        if (cachedTransitLine == null)
        {
          cachedTransitLine = this.GetCachedTransitLine(status.LocationID);
          inTransitLineList.Add(cachedTransitLine);
        }
        PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) this);
        INItemPlan inItemPlan1 = (INItemPlan) null;
        List<INItemPlan> list = GraphHelper.RowCast<INItemPlan>((IEnumerable) PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<INTransitLine.noteID>>, And<INItemPlan.inventoryID, Equal<Current<TransitLotSerialStatusByCostCenter.inventoryID>>, And<INItemPlan.subItemID, Equal<Current<TransitLotSerialStatusByCostCenter.subItemID>>, And<INItemPlan.lotSerialNbr, Equal<Current<TransitLotSerialStatusByCostCenter.lotSerialNbr>>, And<INItemPlan.costCenterID, Equal<Current<TransitLotSerialStatusByCostCenter.costCenterID>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[2]
        {
          (object) cachedTransitLine,
          (object) status
        }, Array.Empty<object>())).ToList<INItemPlan>();
        foreach (INItemPlan inItemPlan2 in list)
        {
          ((PXCache) pxCache).SetStatus((object) inItemPlan2, (PXEntryStatus) 0);
          if (inItemPlan1 == null)
          {
            inItemPlan1 = PXCache<INItemPlan>.CreateCopy(inItemPlan2);
            pxCache.Delete(inItemPlan2);
            if (EnumerableExtensions.IsIn<string>(inItemPlan1.PlanType, "42", "44"))
              inItemPlan1.DemandPlanID = new long?();
            INItemPlan inItemPlan3 = inItemPlan1;
            nullable = inItemPlan3.PlanQty;
            Decimal? qtyOnHand = status.QtyOnHand;
            inItemPlan3.PlanQty = nullable.HasValue & qtyOnHand.HasValue ? new Decimal?(nullable.GetValueOrDefault() + qtyOnHand.GetValueOrDefault()) : new Decimal?();
            inItemPlan1.PlanID = new long?();
            inItemPlan1 = pxCache.Insert(inItemPlan1);
          }
          else
          {
            INItemPlan inItemPlan4 = inItemPlan1;
            Decimal? planQty = inItemPlan4.PlanQty;
            nullable = inItemPlan2.PlanQty;
            inItemPlan4.PlanQty = planQty.HasValue & nullable.HasValue ? new Decimal?(planQty.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
            pxCache.Delete(inItemPlan2);
            inItemPlan1 = pxCache.Update(inItemPlan1);
          }
        }
        if (inItemPlan1 != null)
        {
          Decimal? planQty = inItemPlan1.PlanQty;
          Decimal num2 = 0M;
          if (planQty.GetValueOrDefault() <= num2 & planQty.HasValue)
          {
            pxCache.Delete(inItemPlan1);
            inItemPlan1 = (INItemPlan) null;
          }
        }
        foreach (INItemPlan inItemPlan5 in list)
          this.UpdateSOTransferPlans(inItemPlan5.PlanID, (long?) inItemPlan1?.PlanID);
      }
    }
    foreach (TransitLocationStatusByCostCenter statusByCostCenter in ((PXSelectBase) this.transitlocationstatusbycostcenter).Cache.Inserted)
    {
      TransitLocationStatusByCostCenter status = statusByCostCenter;
      if (inTransitLineList.Find((Predicate<INTransitLine>) (x =>
      {
        int? costSiteId = x.CostSiteID;
        int? locationId = status.LocationID;
        return costSiteId.GetValueOrDefault() == locationId.GetValueOrDefault() & costSiteId.HasValue == locationId.HasValue;
      })) == null)
      {
        Decimal? nullable = status.QtyOnHand;
        Decimal num3 = 0M;
        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
        {
          INTransitLine cachedTransitLine = this.GetCachedTransitLine(status.LocationID);
          PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) this);
          INItemPlan inItemPlan6 = (INItemPlan) null;
          List<INItemPlan> list = GraphHelper.RowCast<INItemPlan>((IEnumerable) PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<INTransitLine.noteID>>, And<INItemPlan.inventoryID, Equal<Current<TransitLocationStatusByCostCenter.inventoryID>>, And<INItemPlan.subItemID, Equal<Current<TransitLocationStatusByCostCenter.subItemID>>, And<INItemPlan.costCenterID, Equal<Current<TransitLocationStatusByCostCenter.costCenterID>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[2]
          {
            (object) cachedTransitLine,
            (object) status
          }, Array.Empty<object>())).ToList<INItemPlan>();
          foreach (INItemPlan inItemPlan7 in list)
          {
            if (inItemPlan6 == null)
            {
              inItemPlan6 = PXCache<INItemPlan>.CreateCopy(inItemPlan7);
              pxCache.Delete(inItemPlan7);
              if (EnumerableExtensions.IsIn<string>(inItemPlan6.PlanType, "42", "44"))
                inItemPlan6.DemandPlanID = new long?();
              INItemPlan inItemPlan8 = inItemPlan6;
              nullable = inItemPlan8.PlanQty;
              Decimal? qtyOnHand = status.QtyOnHand;
              inItemPlan8.PlanQty = nullable.HasValue & qtyOnHand.HasValue ? new Decimal?(nullable.GetValueOrDefault() + qtyOnHand.GetValueOrDefault()) : new Decimal?();
              inItemPlan6.PlanID = new long?();
              inItemPlan6 = pxCache.Insert(inItemPlan6);
            }
            else
            {
              INItemPlan inItemPlan9 = inItemPlan6;
              Decimal? planQty = inItemPlan9.PlanQty;
              nullable = inItemPlan7.PlanQty;
              inItemPlan9.PlanQty = planQty.HasValue & nullable.HasValue ? new Decimal?(planQty.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
              pxCache.Delete(inItemPlan7);
              inItemPlan6 = pxCache.Update(inItemPlan6);
            }
          }
          if (inItemPlan6 != null)
          {
            Decimal? planQty = inItemPlan6.PlanQty;
            Decimal num4 = 0M;
            if (planQty.GetValueOrDefault() <= num4 & planQty.HasValue)
            {
              pxCache.Delete(inItemPlan6);
              inItemPlan6 = (INItemPlan) null;
            }
          }
          foreach (INItemPlan inItemPlan10 in list)
            this.UpdateSOTransferPlans(inItemPlan10.PlanID, (long?) inItemPlan6?.PlanID);
        }
      }
    }
  }

  public virtual void UpdateItemSite(InventoryAccountServiceParams @params, PX.Objects.CS.ReasonCode reasoncode)
  {
    INTran inTran1 = @params.INTran;
    PX.Objects.IN.InventoryItem inventoryItem1 = @params.Item;
    PX.Objects.IN.INSite site1 = @params.Site;
    INPostClass postclass1 = @params.Postclass;
    IProjectAccountsSource project1 = @params.Project;
    IProjectTaskAccountsSource task1 = @params.Task;
    if (inventoryItem1.StkItem.GetValueOrDefault())
    {
      INItemSite itemsite1 = INReleaseProcess.SelectItemSite((PXGraph) this, inTran1.InventoryID, inTran1.SiteID);
      if (itemsite1 == null)
      {
        INItemSite itemsite2 = new INItemSite();
        itemsite2.InventoryID = inTran1.InventoryID;
        itemsite2.SiteID = inTran1.SiteID;
        InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, inventoryItem1.InventoryID, site1.BaseCuryID);
        INItemSiteMaint.DefaultItemSiteByItem((PXGraph) this, itemsite2, inventoryItem1, site1, postclass1, itemCurySettings);
        itemsite1 = ((PXSelectBase<INItemSite>) this.initemsite).Insert(itemsite2);
      }
      if (!itemsite1.InvtAcctID.HasValue)
        INItemSiteMaint.DefaultInvtAcctSub((PXGraph) this, itemsite1, inventoryItem1, site1, postclass1);
      if (!inTran1.InvtAcctID.HasValue)
      {
        inTran1.InvtAcctID = itemsite1.InvtAcctID;
        inTran1.InvtSubID = itemsite1.InvtSubID;
      }
    }
    else
    {
      string tranType = inTran1.TranType;
      if (tranType != null && tranType.Length == 3)
      {
        switch (tranType[1])
        {
          case 'C':
            if (tranType == "RCP")
              break;
            goto label_24;
          case 'D':
            if (tranType == "ADJ")
            {
              if (!inTran1.InvtAcctID.HasValue && !this.IsUnmanagedTran(inTran1))
              {
                inTran1.InvtAcctID = this.GetInvtAcctID(inventoryItem1, (PX.Objects.IN.INSite) null, postclass1, inTran1, false);
                inTran1.InvtSubID = this.GetInvtSubID(inventoryItem1, (PX.Objects.IN.INSite) null, postclass1, inTran1, false);
                goto label_25;
              }
              goto label_25;
            }
            goto label_24;
          case 'E':
            if (tranType == "RET")
            {
              if (!(inTran1.SOShipmentType == "H"))
                throw new PXException("Invalid Transaction Type.");
              goto label_18;
            }
            goto label_24;
          case 'I':
            if (tranType == "III")
              break;
            goto label_24;
          case 'N':
            if (tranType == "INV")
              goto label_18;
            goto label_24;
          case 'R':
            if (tranType == "DRM" || tranType == "CRM")
              goto label_18;
            goto label_24;
          case 'S':
            if (tranType == "ASY" || tranType == "DSY")
              goto label_18;
            goto label_24;
          default:
            goto label_24;
        }
        if (!inTran1.InvtAcctID.HasValue)
        {
          inTran1.InvtAcctID = this.GetCogsAcctID(InventoryAccountServiceHelper.Params(inventoryItem1, (PX.Objects.IN.INSite) null, postclass1, inTran1, project1, task1));
          inTran1.InvtSubID = this.GetCogsSubID(InventoryAccountServiceHelper.Params(inventoryItem1, (PX.Objects.IN.INSite) null, postclass1, inTran1, project1, task1));
          goto label_25;
        }
        goto label_25;
label_18:
        if (!inTran1.InvtAcctID.HasValue && !this.IsUnmanagedTran(inTran1))
        {
          inTran1.InvtAcctID = this.GetInvtAcctID(inventoryItem1, (PX.Objects.IN.INSite) null, postclass1, inTran1, false);
          inTran1.InvtSubID = this.GetInvtSubID(inventoryItem1, (PX.Objects.IN.INSite) null, postclass1, inTran1, false);
          goto label_25;
        }
        goto label_25;
      }
label_24:
      throw new PXException("Invalid Transaction Type.");
    }
label_25:
    string tranType1 = inTran1.TranType;
    if (tranType1 != null && tranType1.Length == 3)
    {
      switch (tranType1[2])
      {
        case 'C':
          if (tranType1 == "ASC" || tranType1 == "NSC")
            goto label_62;
          goto label_87;
        case 'I':
          if (tranType1 == "III")
            break;
          goto label_87;
        case 'J':
          if (tranType1 == "ADJ")
            goto label_62;
          goto label_87;
        case 'M':
          if (tranType1 == "DRM" || tranType1 == "CRM")
            goto label_56;
          goto label_87;
        case 'P':
          if (tranType1 == "RCP")
          {
            if (!inTran1.AcctID.HasValue)
            {
              inTran1.AcctID = reasoncode.AccountID ?? this.ReceiptReasonCode.AccountID;
              inTran1.SubID = this.GetReasonCodeSubID(reasoncode, this.ReceiptReasonCode, @params);
              inTran1.ReasonCode = inTran1.ReasonCode ?? this.ReceiptReasonCode.ReasonCodeID;
            }
            if (!inTran1.COGSAcctID.HasValue)
              return;
            inTran1.COGSAcctID = new int?();
            inTran1.COGSSubID = new int?();
            return;
          }
          goto label_87;
        case 'T':
          if (tranType1 == "RET")
            break;
          goto label_87;
        case 'V':
          if (tranType1 == "INV")
            goto label_56;
          goto label_87;
        case 'X':
          if (tranType1 == "TRX")
          {
            if (!inTran1.AcctID.HasValue)
            {
              if (!this.IsOneStepTransferWithinSite() && (!this.INTransitAcctID.HasValue || !this.INTransitSubID.HasValue))
                throw new PXException("The document cannot be released because the in-transit account or subaccount is not specified on the Inventory Preferences (IN101000) form.");
              inTran1.AcctID = this.INTransitAcctID;
              inTran1.SubID = this.INTransitSubID;
              inTran1.ReclassificationProhibited = new bool?(true);
              INTran inTran2 = inTran1;
              if (inTran2.ReasonCode == null)
              {
                string reasonCodeId;
                inTran2.ReasonCode = reasonCodeId = this.TransferReasonCode?.ReasonCodeID;
              }
            }
            if (!inTran1.COGSAcctID.HasValue)
              return;
            inTran1.COGSAcctID = new int?();
            inTran1.COGSSubID = new int?();
            return;
          }
          goto label_87;
        case 'Y':
          if (tranType1 == "ASY" || tranType1 == "DSY")
          {
            if (inTran1.TranType == "ASY")
              inTran1.ReasonCode = inTran1.ReasonCode ?? this.AssemblyDisassemblyReasonCode?.ReasonCodeID;
            if (!inTran1.AcctID.HasValue)
            {
              if (!this.INProgressAcctID.HasValue || !this.INProgressSubID.HasValue)
                throw new PXException("The document cannot be released because the work-in-progress account or subaccount is not specified on the Inventory Preferences (IN101000) form.");
              inTran1.AcctID = this.INProgressAcctID;
              inTran1.SubID = this.INProgressSubID;
              inTran1.ReclassificationProhibited = new bool?(true);
            }
            if (!inTran1.COGSAcctID.HasValue)
              return;
            inTran1.COGSAcctID = new int?();
            inTran1.COGSSubID = new int?();
            return;
          }
          goto label_87;
        default:
          goto label_87;
      }
      if (!(inTran1.SOShipmentType == "H"))
      {
        if ((inTran1.POReceiptType == "RN" && EnumerableExtensions.IsIn<string>(reasoncode.Usage, "N", "I") || inTran1.POReceiptType == "RT" && ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsCorrection.GetValueOrDefault()) && (inTran1.AcctID.HasValue || inTran1.COGSAcctID.HasValue))
          return;
        if (inTran1.AcctID.HasValue && inTran1.OrigModule != "SO")
        {
          inTran1.AcctID = new int?();
          inTran1.SubID = new int?();
        }
        if (inTran1.COGSAcctID.HasValue)
          return;
        if (reasoncode.Usage == "I" || string.IsNullOrEmpty(reasoncode.Usage) && ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.OrigModule != "SO")
        {
          inTran1.COGSAcctID = reasoncode.AccountID ?? this.IssuesReasonCode.AccountID;
          inTran1.COGSSubID = this.GetReasonCodeSubID(reasoncode, this.IssuesReasonCode, @params);
          inTran1.ReasonCode = inTran1.ReasonCode ?? this.IssuesReasonCode.ReasonCodeID;
          return;
        }
        inTran1.COGSAcctID = this.GetCogsAcctID(InventoryAccountServiceHelper.Params(inventoryItem1, site1, postclass1, inTran1, project1, task1));
        inTran1.COGSSubID = this.GetCogsSubID(InventoryAccountServiceHelper.Params(inventoryItem1, site1, postclass1, inTran1, project1, task1, inTran1.OrigModule == "SO"));
        return;
      }
label_56:
      if (!inTran1.AcctID.HasValue)
      {
        inTran1.AcctID = this.GetSalesAcctID(InventoryAccountServiceHelper.Params(inventoryItem1, site1, postclass1, inTran1, project1, task1));
        inTran1.SubID = this.GetSalesSubID(InventoryAccountServiceHelper.Params(inventoryItem1, site1, postclass1, inTran1, project1, task1));
      }
      if (inTran1.COGSAcctID.HasValue)
        return;
      if (reasoncode.Usage == "I")
      {
        inTran1.COGSAcctID = reasoncode.AccountID;
        inTran1.COGSSubID = this.GetReasonCodeSubID(reasoncode, @params);
        return;
      }
      inTran1.COGSAcctID = this.GetCogsAcctID(@params.UsingTransaction());
      INTran inTran3 = inTran1;
      PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
      PX.Objects.IN.INSite site2 = site1;
      INPostClass postclass2 = postclass1;
      INTran tran = inTran1;
      IProjectAccountsSource project2 = project1;
      IProjectTaskAccountsSource task2 = task1;
      short? invtMult1 = inTran1.InvtMult;
      int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
      int num1 = 0;
      int num2 = !(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue) ? 1 : 0;
      int? cogsSubId = this.GetCogsSubID(InventoryAccountServiceHelper.Params(inventoryItem2, site2, postclass2, tran, project2, task2, num2 != 0));
      inTran3.COGSSubID = cogsSubId;
      return;
label_62:
      if (!inTran1.AcctID.HasValue)
      {
        inTran1.AcctID = reasoncode.AccountID ?? this.AdjustmentReasonCode.AccountID;
        inTran1.SubID = this.GetReasonCodeSubID(reasoncode, this.AdjustmentReasonCode, @params);
        inTran1.ReasonCode = inTran1.ReasonCode ?? this.AdjustmentReasonCode.ReasonCodeID;
      }
      if (!inTran1.COGSAcctID.HasValue)
      {
        short? invtMult2 = inTran1.InvtMult;
        int? nullable2 = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
        int num3 = 0;
        if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
        {
          if (inventoryItem1.ValMethod == "T")
          {
            inTran1.COGSAcctID = this.GetStdCostVarAcctID(inventoryItem1, site1, postclass1, inTran1, false);
            inTran1.COGSSubID = this.GetStdCostVarSubID(inventoryItem1, site1, postclass1, inTran1, false);
          }
          else
          {
            inTran1.COGSAcctID = this.GetCogsAcctID(InventoryAccountServiceHelper.Params(inventoryItem1, site1, postclass1, inTran1, project1, task1));
            inTran1.COGSSubID = this.GetCogsSubID(@params.UsingTransaction());
          }
        }
      }
      if (!inTran1.COGSAcctID.HasValue)
        return;
      short? invtMult3 = inTran1.InvtMult;
      int? nullable3 = invtMult3.HasValue ? new int?((int) invtMult3.GetValueOrDefault()) : new int?();
      int num4 = 1;
      if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
        return;
      inTran1.COGSAcctID = new int?();
      inTran1.COGSSubID = new int?();
      return;
    }
label_87:
    throw new PXException("Invalid Transaction Type.");
  }

  private void SegregateBatch(
    JournalEntry je,
    int? branchID,
    string curyID,
    DateTime? docDate,
    string finPeriodID,
    string description)
  {
    je.created.Consolidate = ((PXSelectBase<GLSetup>) je.glsetup).Current.ConsolidatedPosting.GetValueOrDefault();
    je.Segregate("IN", branchID, curyID, docDate, finPeriodID, description, new Decimal?(), (string) null, (Batch) null);
  }

  public virtual void WriteGLSales(JournalEntry je, INTran intran)
  {
    if (!this.UpdateGL || !intran.SalesMult.HasValue || !string.IsNullOrEmpty(intran.SOOrderNbr) || !string.IsNullOrEmpty(intran.ARRefNbr))
      return;
    PX.Objects.GL.GLTran tran1 = new PX.Objects.GL.GLTran();
    tran1.SummPost = new bool?(this.SummPost);
    tran1.BranchID = intran.BranchID;
    tran1.AccountID = this.ARClearingAcctID;
    tran1.SubID = this.ARClearingSubID;
    PX.Objects.GL.GLTran glTran1 = tran1;
    short? salesMult = intran.SalesMult;
    int? nullable1 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num1 = 1;
    Decimal? nullable2 = nullable1.GetValueOrDefault() == num1 & nullable1.HasValue ? intran.TranAmt : new Decimal?(0M);
    glTran1.CuryDebitAmt = nullable2;
    PX.Objects.GL.GLTran glTran2 = tran1;
    salesMult = intran.SalesMult;
    int? nullable3 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num2 = 1;
    Decimal? nullable4 = nullable3.GetValueOrDefault() == num2 & nullable3.HasValue ? intran.TranAmt : new Decimal?(0M);
    glTran2.DebitAmt = nullable4;
    PX.Objects.GL.GLTran glTran3 = tran1;
    salesMult = intran.SalesMult;
    int? nullable5 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num3 = 1;
    Decimal? nullable6 = nullable5.GetValueOrDefault() == num3 & nullable5.HasValue ? new Decimal?(0M) : intran.TranAmt;
    glTran3.CuryCreditAmt = nullable6;
    PX.Objects.GL.GLTran glTran4 = tran1;
    salesMult = intran.SalesMult;
    int? nullable7 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num4 = 1;
    Decimal? nullable8 = nullable7.GetValueOrDefault() == num4 & nullable7.HasValue ? new Decimal?(0M) : intran.TranAmt;
    glTran4.CreditAmt = nullable8;
    tran1.TranType = intran.TranType;
    tran1.TranClass = "N";
    tran1.RefNbr = intran.RefNbr;
    tran1.InventoryID = intran.InventoryID;
    PX.Objects.GL.GLTran glTran5 = tran1;
    salesMult = intran.SalesMult;
    int? nullable9 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num5 = 1;
    Decimal? nullable10;
    if (!(nullable9.GetValueOrDefault() == num5 & nullable9.HasValue))
    {
      Decimal? qty = intran.Qty;
      nullable10 = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable10 = intran.Qty;
    glTran5.Qty = nullable10;
    tran1.UOM = intran.UOM;
    tran1.TranDesc = intran.TranDesc;
    tran1.TranDate = intran.TranDate;
    tran1.TranPeriodID = intran.TranPeriodID;
    tran1.FinPeriodID = intran.FinPeriodID;
    tran1.ProjectID = ProjectDefaultAttribute.NonProject();
    tran1.CostCodeID = CostCodeAttribute.DefaultCostCode;
    tran1.Released = new bool?(true);
    PX.Objects.GL.GLTran glTran6 = tran1;
    bool? summPost = tran1.SummPost;
    int? nullable11 = summPost.GetValueOrDefault() ? new int?() : intran.LineNbr;
    glTran6.TranLineNbr = nullable11;
    this.InsertGLSalesDebit(je, tran1, new INReleaseProcess.GLTranInsertionContext()
    {
      INTran = intran
    });
    PX.Objects.GL.GLTran tran2 = new PX.Objects.GL.GLTran();
    tran2.SummPost = new bool?(this.SummPost);
    tran2.BranchID = intran.BranchID;
    tran2.AccountID = intran.AcctID;
    tran2.SubID = this.GetValueInt<INTran.subID>((PXGraph) je, (object) intran);
    PX.Objects.GL.GLTran glTran7 = tran2;
    salesMult = intran.SalesMult;
    int? nullable12 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num6 = 1;
    Decimal? nullable13 = nullable12.GetValueOrDefault() == num6 & nullable12.HasValue ? new Decimal?(0M) : intran.TranAmt;
    glTran7.CuryDebitAmt = nullable13;
    PX.Objects.GL.GLTran glTran8 = tran2;
    salesMult = intran.SalesMult;
    int? nullable14 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num7 = 1;
    Decimal? nullable15 = nullable14.GetValueOrDefault() == num7 & nullable14.HasValue ? new Decimal?(0M) : intran.TranAmt;
    glTran8.DebitAmt = nullable15;
    PX.Objects.GL.GLTran glTran9 = tran2;
    salesMult = intran.SalesMult;
    int? nullable16 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num8 = 1;
    Decimal? nullable17 = nullable16.GetValueOrDefault() == num8 & nullable16.HasValue ? intran.TranAmt : new Decimal?(0M);
    glTran9.CuryCreditAmt = nullable17;
    PX.Objects.GL.GLTran glTran10 = tran2;
    salesMult = intran.SalesMult;
    int? nullable18 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num9 = 1;
    Decimal? nullable19 = nullable18.GetValueOrDefault() == num9 & nullable18.HasValue ? intran.TranAmt : new Decimal?(0M);
    glTran10.CreditAmt = nullable19;
    tran2.TranType = intran.TranType;
    tran2.TranClass = "N";
    tran2.RefNbr = intran.RefNbr;
    tran2.InventoryID = intran.InventoryID;
    PX.Objects.GL.GLTran glTran11 = tran2;
    salesMult = intran.SalesMult;
    int? nullable20 = salesMult.HasValue ? new int?((int) salesMult.GetValueOrDefault()) : new int?();
    int num10 = 1;
    Decimal? nullable21;
    if (!(nullable20.GetValueOrDefault() == num10 & nullable20.HasValue))
    {
      nullable21 = intran.Qty;
    }
    else
    {
      Decimal? qty = intran.Qty;
      nullable21 = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
    }
    glTran11.Qty = nullable21;
    tran2.UOM = intran.UOM;
    tran2.TranDesc = intran.TranDesc;
    tran2.TranDate = intran.TranDate;
    tran2.TranPeriodID = intran.TranPeriodID;
    tran2.FinPeriodID = intran.FinPeriodID;
    tran2.ProjectID = ProjectDefaultAttribute.NonProject();
    tran2.CostCodeID = CostCodeAttribute.DefaultCostCode;
    tran2.Released = new bool?(true);
    PX.Objects.GL.GLTran glTran12 = tran2;
    summPost = tran2.SummPost;
    int? nullable22 = summPost.GetValueOrDefault() ? new int?() : intran.LineNbr;
    glTran12.TranLineNbr = nullable22;
    this.InsertGLSalesCredit(je, tran2, new INReleaseProcess.GLTranInsertionContext()
    {
      INTran = intran
    });
  }

  public virtual PX.Objects.GL.GLTran InsertGLSalesDebit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual PX.Objects.GL.GLTran InsertGLSalesCredit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public int? GetValueInt<SourceField>(PXGraph target, object item) where SourceField : IBqlField
  {
    PXCache cach1 = ((PXGraph) this).Caches[BqlCommand.GetItemType(typeof (SourceField))];
    PXCache cach2 = target.Caches[BqlCommand.GetItemType(typeof (SourceField))];
    object obj = item;
    object valueExt = cach1.GetValueExt<SourceField>(obj);
    if (valueExt is PXFieldState)
      valueExt = ((PXFieldState) valueExt).Value;
    if (valueExt != null)
      cach2.RaiseFieldUpdating<SourceField>(item, ref valueExt);
    return (int?) valueExt;
  }

  public virtual void UpdateARTranCost(INTran tran) => this.UpdateARTranCost(tran, tran.TranCost);

  public virtual void UpdateARTranCost(INTran tran, Decimal? TranCost)
  {
    if (tran.ARRefNbr == null)
      return;
    ARTranUpdate arTranUpdate1 = ((PXSelectBase<ARTranUpdate>) this.artranupdate).Insert(new ARTranUpdate()
    {
      TranType = tran.ARDocType,
      RefNbr = tran.ARRefNbr,
      LineNbr = tran.ARLineNbr
    });
    short? invtMult = tran.InvtMult;
    int? nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    short? nullable2 = !(nullable1.GetValueOrDefault() == num & nullable1.HasValue) ? tran.InvtMult : INTranType.InvtMult(tran.TranType);
    int? nullable3 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
    short? nullable4 = INTranType.InvtMultFromInvoiceType(tran.ARDocType);
    int? nullable5 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
    Decimal? nullable6;
    if (!(nullable3.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable3.HasValue == nullable5.HasValue) && tran.TranType != "ADJ")
    {
      nullable6 = TranCost;
      TranCost = nullable6.HasValue ? new Decimal?(-nullable6.GetValueOrDefault()) : new Decimal?();
    }
    SOSetup soSetup = PXResultset<SOSetup>.op_Implicit(PXSelectBase<SOSetup, PXSelect<SOSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (soSetup == null || !(soSetup.SalesProfitabilityForNSKits == "K") || !tran.IsComponentItem.GetValueOrDefault())
    {
      ARTranUpdate arTranUpdate2 = arTranUpdate1;
      nullable6 = arTranUpdate2.TranCost;
      Decimal? nullable7 = TranCost;
      arTranUpdate2.TranCost = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
    }
    arTranUpdate1.IsTranCostFinal = new bool?(true);
  }

  public virtual void OnTranReleased(INTran tran)
  {
    this.UpdateARTranReleased(tran);
    this.UpdatePOReceiptLineReleased(tran);
  }

  protected virtual void UpdateARTranReleased(INTran tran)
  {
    if (tran.ARRefNbr == null)
      return;
    ((PXSelectBase<ARTranUpdate>) this.artranupdate).Insert(new ARTranUpdate()
    {
      TranType = tran.ARDocType,
      RefNbr = tran.ARRefNbr,
      LineNbr = tran.ARLineNbr
    }).InvtReleased = new bool?(true);
  }

  public virtual POReceiptLineUpdate UpdatePOReceiptLineReleased(INTran tran)
  {
    if (string.IsNullOrEmpty(tran.POReceiptType) || string.IsNullOrEmpty(tran.POReceiptNbr) || !tran.POReceiptLineNbr.HasValue)
      return (POReceiptLineUpdate) null;
    PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current;
    bool? nullable = current.IsTaxAdjustmentTran;
    if (!nullable.GetValueOrDefault())
    {
      nullable = current.IsPPVTran;
      if (!nullable.GetValueOrDefault())
      {
        POReceiptLineUpdate receiptLineUpdate = ((PXSelectBase<POReceiptLineUpdate>) this.poreceiptlineupdate).Insert(new POReceiptLineUpdate()
        {
          ReceiptType = tran.POReceiptType,
          ReceiptNbr = tran.POReceiptNbr,
          LineNbr = tran.POReceiptLineNbr
        });
        receiptLineUpdate.INReleased = tran.Released;
        return ((PXSelectBase<POReceiptLineUpdate>) this.poreceiptlineupdate).Update(receiptLineUpdate);
      }
    }
    return (POReceiptLineUpdate) null;
  }

  private bool IsUpdatablePOReturnTranCostFinal(INTran tran, PX.Objects.IN.InventoryItem item)
  {
    if (!(tran.TranType == "III") || tran.ExactCost.GetValueOrDefault() && !(item.ValMethod != "T") || !(tran.POReceiptType == "RN") || string.IsNullOrEmpty(tran.POReceiptNbr) || !tran.POReceiptLineNbr.HasValue)
      return false;
    Decimal? qty = tran.Qty;
    Decimal num = 0M;
    return !(qty.GetValueOrDefault() == num & qty.HasValue);
  }

  public virtual POReceiptLineUpdate UpdatePOReceiptLineCost(
    INTran tran,
    INTranCost tranCost,
    PX.Objects.IN.InventoryItem item)
  {
    if ((!this.IsUpdatablePOReturnTranCostFinal(tran, item) || !(tran.DocType == tranCost.CostDocType) ? 0 : (tran.RefNbr == tranCost.CostRefNbr ? 1 : 0)) == 0)
      return (POReceiptLineUpdate) null;
    POReceiptLineUpdate receiptLineUpdate1 = ((PXSelectBase<POReceiptLineUpdate>) this.poreceiptlineupdate).Insert(new POReceiptLineUpdate()
    {
      ReceiptType = tran.POReceiptType,
      ReceiptNbr = tran.POReceiptNbr,
      LineNbr = tran.POReceiptLineNbr
    });
    receiptLineUpdate1.UpdateTranCostFinal = new bool?(true);
    POReceiptLineUpdate receiptLineUpdate2 = receiptLineUpdate1;
    Decimal? tranCostFinal = receiptLineUpdate2.TranCostFinal;
    short? invtMult = tranCost.InvtMult;
    Decimal? nullable1 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2 = tranCost.TranCost;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4;
    if (!(tranCostFinal.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(tranCostFinal.GetValueOrDefault() - nullable3.GetValueOrDefault());
    receiptLineUpdate2.TranCostFinal = nullable4;
    return ((PXSelectBase<POReceiptLineUpdate>) this.poreceiptlineupdate).Update(receiptLineUpdate1);
  }

  public virtual PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats UpdateItemStatsLastPurchaseDate(
    INTran tran)
  {
    if (!(tran.OrigModule != "PO") && !(tran.TranType != "RCP"))
    {
      short? invtMult = tran.InvtMult;
      if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1)
      {
        PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats1 = new PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats();
        itemStats1.InventoryID = tran.InventoryID;
        itemStats1.SiteID = tran.SiteID;
        PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats>) this.itemstats).Insert(itemStats1);
        itemStats2.LastPurchaseDate = tran.TranDate;
        return ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats>) this.itemstats).Update(itemStats2);
      }
    }
    return (PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats) null;
  }

  public virtual void WriteGLNonStockCosts(
    JournalEntry je,
    INTran intran,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site)
  {
    bool flag1 = this.IsProjectDropShip(intran);
    bool flag2 = intran?.SOShipmentType == "H" | flag1 && (intran.POReceiptType == "RN" || intran.POReceiptType == "RT" && EnumerableExtensions.IsIn<string>(intran.TranType, "CRM", "RET"));
    bool? nullable1 = item.StkItem;
    bool flag3 = false;
    int? nullable2;
    short? invtMult;
    Decimal? qty;
    if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue | flag1)
    {
      nullable2 = intran.COGSAcctID;
      if (!nullable2.HasValue)
      {
        nullable2 = intran.AcctID;
        if (!nullable2.HasValue)
          goto label_10;
      }
      PX.Objects.GL.GLTran tran = new PX.Objects.GL.GLTran();
      tran.SummPost = new bool?(this.SummPost);
      tran.BranchID = intran.BranchID;
      PX.Objects.GL.GLTran glTran1 = tran;
      invtMult = intran.InvtMult;
      nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num1 = 0;
      int? nullable3 = nullable2.GetValueOrDefault() == num1 & nullable2.HasValue ? intran.AcctID : intran.InvtAcctID;
      glTran1.AccountID = nullable3;
      PX.Objects.GL.GLTran glTran2 = tran;
      invtMult = intran.InvtMult;
      nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num2 = 0;
      int? nullable4 = nullable2.GetValueOrDefault() == num2 & nullable2.HasValue ? intran.SubID : this.GetValueInt<INTran.invtSubID>((PXGraph) je, (object) intran);
      glTran2.SubID = nullable4;
      PX.Objects.GL.GLTran glTran3 = tran;
      invtMult = intran.InvtMult;
      nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num3 = 1;
      Decimal? nullable5 = nullable2.GetValueOrDefault() == num3 & nullable2.HasValue | flag2 ? intran.TranCost : new Decimal?(0M);
      glTran3.CuryDebitAmt = nullable5;
      PX.Objects.GL.GLTran glTran4 = tran;
      invtMult = intran.InvtMult;
      nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num4 = 1;
      Decimal? nullable6 = nullable2.GetValueOrDefault() == num4 & nullable2.HasValue | flag2 ? intran.TranCost : new Decimal?(0M);
      glTran4.DebitAmt = nullable6;
      PX.Objects.GL.GLTran glTran5 = tran;
      invtMult = intran.InvtMult;
      nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num5 = 1;
      Decimal? nullable7 = nullable2.GetValueOrDefault() == num5 & nullable2.HasValue | flag2 ? new Decimal?(0M) : intran.TranCost;
      glTran5.CuryCreditAmt = nullable7;
      PX.Objects.GL.GLTran glTran6 = tran;
      invtMult = intran.InvtMult;
      nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num6 = 1;
      Decimal? nullable8 = nullable2.GetValueOrDefault() == num6 & nullable2.HasValue | flag2 ? new Decimal?(0M) : intran.TranCost;
      glTran6.CreditAmt = nullable8;
      tran.TranType = intran.TranType;
      tran.TranClass = "N";
      tran.RefNbr = intran.RefNbr;
      tran.InventoryID = intran.InventoryID;
      PX.Objects.GL.GLTran glTran7 = tran;
      invtMult = intran.InvtMult;
      nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num7 = 1;
      Decimal? nullable9;
      if (!(nullable2.GetValueOrDefault() == num7 & nullable2.HasValue | flag2))
      {
        qty = intran.Qty;
        nullable9 = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable9 = intran.Qty;
      glTran7.Qty = nullable9;
      tran.UOM = intran.UOM;
      tran.TranDesc = intran.TranDesc;
      tran.TranDate = intran.TranDate;
      tran.TranPeriodID = intran.TranPeriodID;
      tran.FinPeriodID = intran.FinPeriodID;
      tran.ProjectID = ProjectDefaultAttribute.NonProject();
      tran.CostCodeID = CostCodeAttribute.DefaultCostCode;
      tran.Released = new bool?(true);
      PX.Objects.GL.GLTran glTran8 = tran;
      nullable1 = tran.SummPost;
      int? nullable10;
      if (!nullable1.GetValueOrDefault())
      {
        nullable10 = intran.LineNbr;
      }
      else
      {
        nullable2 = new int?();
        nullable10 = nullable2;
      }
      glTran8.TranLineNbr = nullable10;
      this.InsertGLNonStockCostDebit(je, tran, new INReleaseProcess.GLTranInsertionContext()
      {
        INTran = intran,
        Item = item,
        Site = site
      });
    }
label_10:
    nullable1 = item.StkItem;
    bool flag4 = false;
    if (!(nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue | flag1))
      return;
    nullable2 = intran.COGSAcctID;
    if (!nullable2.HasValue)
    {
      nullable2 = intran.AcctID;
      if (!nullable2.HasValue)
        return;
    }
    PX.Objects.GL.GLTran tran1 = new PX.Objects.GL.GLTran();
    tran1.SummPost = new bool?(this.SummPost);
    tran1.BranchID = intran.BranchID;
    PX.Objects.GL.GLTran glTran9 = tran1;
    nullable2 = intran.COGSAcctID;
    int? nullable11 = nullable2 ?? intran.AcctID;
    glTran9.AccountID = nullable11;
    PX.Objects.GL.GLTran glTran10 = tran1;
    nullable2 = this.GetValueInt<INTran.cOGSSubID>((PXGraph) je, (object) intran);
    int? nullable12 = nullable2 ?? this.GetValueInt<INTran.subID>((PXGraph) je, (object) intran);
    glTran10.SubID = nullable12;
    PX.Objects.GL.GLTran glTran11 = tran1;
    invtMult = intran.InvtMult;
    nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num8 = 1;
    Decimal? nullable13 = nullable2.GetValueOrDefault() == num8 & nullable2.HasValue | flag2 ? new Decimal?(0M) : intran.TranCost;
    glTran11.CuryDebitAmt = nullable13;
    PX.Objects.GL.GLTran glTran12 = tran1;
    invtMult = intran.InvtMult;
    nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num9 = 1;
    Decimal? nullable14 = nullable2.GetValueOrDefault() == num9 & nullable2.HasValue | flag2 ? new Decimal?(0M) : intran.TranCost;
    glTran12.DebitAmt = nullable14;
    PX.Objects.GL.GLTran glTran13 = tran1;
    invtMult = intran.InvtMult;
    nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num10 = 1;
    Decimal? nullable15 = nullable2.GetValueOrDefault() == num10 & nullable2.HasValue | flag2 ? intran.TranCost : new Decimal?(0M);
    glTran13.CuryCreditAmt = nullable15;
    PX.Objects.GL.GLTran glTran14 = tran1;
    invtMult = intran.InvtMult;
    nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num11 = 1;
    Decimal? nullable16 = nullable2.GetValueOrDefault() == num11 & nullable2.HasValue | flag2 ? intran.TranCost : new Decimal?(0M);
    glTran14.CreditAmt = nullable16;
    tran1.TranType = intran.TranType;
    tran1.TranClass = "N";
    tran1.RefNbr = intran.RefNbr;
    tran1.InventoryID = intran.InventoryID;
    PX.Objects.GL.GLTran glTran15 = tran1;
    invtMult = intran.InvtMult;
    nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num12 = 1;
    Decimal? nullable17;
    if (!(nullable2.GetValueOrDefault() == num12 & nullable2.HasValue | flag2))
    {
      nullable17 = intran.Qty;
    }
    else
    {
      qty = intran.Qty;
      nullable17 = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
    }
    glTran15.Qty = nullable17;
    tran1.UOM = intran.UOM;
    tran1.TranDesc = intran.TranDesc;
    tran1.TranDate = intran.TranDate;
    tran1.TranPeriodID = intran.TranPeriodID;
    tran1.FinPeriodID = intran.FinPeriodID;
    tran1.ProjectID = ProjectDefaultAttribute.NonProject();
    tran1.CostCodeID = CostCodeAttribute.DefaultCostCode;
    tran1.ReclassificationProhibited = new bool?(flag1);
    tran1.Released = new bool?(true);
    PX.Objects.GL.GLTran glTran16 = tran1;
    nullable1 = tran1.SummPost;
    int? nullable18;
    if (!nullable1.GetValueOrDefault())
    {
      nullable18 = intran.LineNbr;
    }
    else
    {
      nullable2 = new int?();
      nullable18 = nullable2;
    }
    glTran16.TranLineNbr = nullable18;
    this.InsertGLNonStockCostCredit(je, tran1, new INReleaseProcess.GLTranInsertionContext()
    {
      INTran = intran,
      Item = item,
      Site = site
    });
  }

  public virtual PX.Objects.GL.GLTran InsertGLNonStockCostDebit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual PX.Objects.GL.GLTran InsertGLNonStockCostCredit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual void WriteGLCosts(
    JournalEntry je,
    INTranCost trancost,
    InventoryAccountServiceParams inventoryParams,
    PX.Objects.CS.ReasonCode reasoncode,
    INLocation location)
  {
    INTran inTran = inventoryParams.INTran;
    PX.Objects.IN.InventoryItem inventoryItem = inventoryParams.Item;
    PX.Objects.IN.INSite site = inventoryParams.Site;
    INPostClass postclass = inventoryParams.Postclass;
    IProjectAccountsSource project = inventoryParams.Project;
    IProjectTaskAccountsSource task = inventoryParams.Task;
    bool flag1 = inventoryItem.ValMethod == "T";
    int num1;
    if (inTran?.SOShipmentType == "H" && inTran.POReceiptNbr != null)
    {
      short? invtMult = trancost.InvtMult;
      int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num2 = 0;
      num1 = nullable.GetValueOrDefault() == num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    int num3 = flag1 ? 1 : 0;
    bool flag2 = (num1 & num3) != 0;
    bool flag3 = inTran?.SOShipmentType == "H" && (inTran.POReceiptType == "RN" || inTran.POReceiptType == "RT" && EnumerableExtensions.IsIn<string>(inTran.TranType, "CRM", "RET"));
    short? invtMult1;
    int? nullable1;
    int? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    if (trancost.COGSAcctID.HasValue || inTran.AcctID.HasValue)
    {
      PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
      glTran1.SummPost = new bool?(trancost.TranType == "TRX" && inTran.DocType == trancost.CostDocType || this.SummPost);
      glTran1.TranDate = trancost.TranDate;
      glTran1.TranPeriodID = trancost.TranPeriodID;
      glTran1.FinPeriodID = trancost.FinPeriodID;
      invtMult1 = trancost.InvtMult;
      int? nullable5 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
      int num4 = 0;
      if (nullable5.GetValueOrDefault() == num4 & nullable5.HasValue)
      {
        glTran1.BranchID = inTran.BranchID;
        glTran1.AccountID = inTran.AcctID;
        glTran1.SubID = inTran.SubID;
        glTran1.ReclassificationProhibited = inTran.ReclassificationProhibited;
      }
      else
      {
        glTran1.BranchID = site.BranchID;
        glTran1.AccountID = trancost.InvtAcctID;
        glTran1.SubID = this.GetValueInt<INTranCost.invtSubID>((PXGraph) je, (object) trancost);
        glTran1.ReclassificationProhibited = new bool?(true);
        nullable1 = inTran.BranchID;
        nullable2 = site.BranchID;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, glTran1.TranPeriodID);
      }
      if (flag2)
      {
        PX.Objects.GL.GLTran glTran2 = glTran1;
        Decimal? nullable6;
        if (!flag3)
        {
          nullable6 = new Decimal?(0M);
        }
        else
        {
          Decimal? tranCost = trancost.TranCost;
          nullable3 = trancost.VarCost;
          nullable6 = tranCost.HasValue & nullable3.HasValue ? new Decimal?(tranCost.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        }
        glTran2.CuryDebitAmt = nullable6;
        PX.Objects.GL.GLTran glTran3 = glTran1;
        Decimal? nullable7;
        if (!flag3)
        {
          nullable7 = new Decimal?(0M);
        }
        else
        {
          nullable3 = trancost.TranCost;
          nullable4 = trancost.VarCost;
          nullable7 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        }
        glTran3.DebitAmt = nullable7;
        PX.Objects.GL.GLTran glTran4 = glTran1;
        Decimal? nullable8;
        if (!flag3)
        {
          nullable4 = trancost.TranCost;
          nullable3 = trancost.VarCost;
          nullable8 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable8 = new Decimal?(0M);
        glTran4.CuryCreditAmt = nullable8;
        PX.Objects.GL.GLTran glTran5 = glTran1;
        Decimal? nullable9;
        if (!flag3)
        {
          nullable3 = trancost.TranCost;
          nullable4 = trancost.VarCost;
          nullable9 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable9 = new Decimal?(0M);
        glTran5.CreditAmt = nullable9;
      }
      else
      {
        PX.Objects.GL.GLTran glTran6 = glTran1;
        invtMult1 = trancost.InvtMult;
        int? nullable10;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable10 = nullable1;
        }
        else
          nullable10 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable10;
        int num5 = 1;
        Decimal? nullable11 = nullable2.GetValueOrDefault() == num5 & nullable2.HasValue | flag3 ? trancost.TranCost : new Decimal?(0M);
        glTran6.CuryDebitAmt = nullable11;
        PX.Objects.GL.GLTran glTran7 = glTran1;
        invtMult1 = trancost.InvtMult;
        int? nullable12;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable12 = nullable1;
        }
        else
          nullable12 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable12;
        int num6 = 1;
        Decimal? nullable13 = nullable2.GetValueOrDefault() == num6 & nullable2.HasValue | flag3 ? trancost.TranCost : new Decimal?(0M);
        glTran7.DebitAmt = nullable13;
        PX.Objects.GL.GLTran glTran8 = glTran1;
        invtMult1 = trancost.InvtMult;
        int? nullable14;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable14 = nullable1;
        }
        else
          nullable14 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable14;
        int num7 = 1;
        Decimal? nullable15 = nullable2.GetValueOrDefault() == num7 & nullable2.HasValue | flag3 ? new Decimal?(0M) : trancost.TranCost;
        glTran8.CuryCreditAmt = nullable15;
        PX.Objects.GL.GLTran glTran9 = glTran1;
        invtMult1 = trancost.InvtMult;
        int? nullable16;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable16 = nullable1;
        }
        else
          nullable16 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable16;
        int num8 = 1;
        Decimal? nullable17 = nullable2.GetValueOrDefault() == num8 & nullable2.HasValue | flag3 ? new Decimal?(0M) : trancost.TranCost;
        glTran9.CreditAmt = nullable17;
      }
      glTran1.TranType = trancost.TranType;
      glTran1.TranClass = "N";
      PX.Objects.GL.GLTran glTran10 = glTran1;
      int num9;
      if (trancost.CostDocType == inTran.DocType && trancost.CostRefNbr == inTran.RefNbr)
      {
        nullable2 = glTran1.AccountID;
        if (nullable2.HasValue)
        {
          nullable2 = glTran1.SubID;
          num9 = nullable2.HasValue ? 1 : 0;
          goto label_39;
        }
      }
      num9 = 0;
label_39:
      bool? nullable18 = new bool?(num9 != 0);
      glTran10.ZeroPost = nullable18;
      glTran1.RefNbr = trancost.RefNbr;
      glTran1.InventoryID = trancost.InventoryID;
      PX.Objects.GL.GLTran glTran11 = glTran1;
      invtMult1 = trancost.InvtMult;
      int? nullable19;
      if (!invtMult1.HasValue)
      {
        nullable1 = new int?();
        nullable19 = nullable1;
      }
      else
        nullable19 = new int?((int) invtMult1.GetValueOrDefault());
      nullable2 = nullable19;
      int num10 = 1;
      Decimal? nullable20;
      if (!(nullable2.GetValueOrDefault() == num10 & nullable2.HasValue | flag3))
      {
        nullable4 = trancost.Qty;
        if (!nullable4.HasValue)
        {
          nullable3 = new Decimal?();
          nullable20 = nullable3;
        }
        else
          nullable20 = new Decimal?(-nullable4.GetValueOrDefault());
      }
      else
        nullable20 = trancost.Qty;
      glTran11.Qty = nullable20;
      glTran1.UOM = inventoryItem.BaseUnit;
      glTran1.TranDesc = inTran.TranDesc;
      glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
      glTran1.CostCodeID = CostCodeAttribute.DefaultCostCode;
      glTran1.Released = new bool?(true);
      PX.Objects.GL.GLTran glTran12 = glTran1;
      int? nullable21;
      if (!glTran1.SummPost.GetValueOrDefault())
      {
        nullable21 = inTran.LineNbr;
      }
      else
      {
        nullable2 = new int?();
        nullable21 = nullable2;
      }
      glTran12.TranLineNbr = nullable21;
      this.InsertGLCostsDebit(je, glTran1, new INReleaseProcess.GLTranInsertionContext()
      {
        TranCost = trancost,
        INTran = inTran,
        Item = inventoryItem,
        Site = site,
        PostClass = postclass,
        ReasonCode = reasoncode,
        Location = location
      });
    }
    bool? nullable22;
    if (!flag1)
    {
      nullable22 = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsCorrection;
      if (!nullable22.GetValueOrDefault())
        goto label_89;
    }
    nullable2 = trancost.COGSAcctID;
    if (!nullable2.HasValue)
    {
      nullable2 = inTran.AcctID;
      if (!nullable2.HasValue)
        goto label_89;
    }
    nullable4 = trancost.VarCost;
    if (Math.Abs(nullable4.GetValueOrDefault()) > 0.00005M)
    {
      PX.Objects.GL.GLTran tran = new PX.Objects.GL.GLTran();
      tran.SummPost = new bool?(this.SummPost);
      tran.BranchID = inTran.BranchID;
      tran.AccountID = flag1 ? this.GetStdCostVarAcctID(inventoryItem, site, postclass, inTran, false) : reasoncode.AccountID;
      tran.SubID = flag1 ? this.GetStdCostVarSubID(inventoryItem, site, postclass, inTran, false) : this.GetReasonCodeSubID(reasoncode, inventoryParams);
      if (flag2)
      {
        tran.CuryDebitAmt = flag3 ? new Decimal?(0M) : trancost.VarCost;
        tran.DebitAmt = flag3 ? new Decimal?(0M) : trancost.VarCost;
        tran.CuryCreditAmt = flag3 ? trancost.VarCost : new Decimal?(0M);
        tran.CreditAmt = flag3 ? trancost.VarCost : new Decimal?(0M);
      }
      else
      {
        PX.Objects.GL.GLTran glTran13 = tran;
        invtMult1 = trancost.InvtMult;
        int? nullable23;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable23 = nullable1;
        }
        else
          nullable23 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable23;
        int num11 = 1;
        Decimal? nullable24 = nullable2.GetValueOrDefault() == num11 & nullable2.HasValue | flag3 ? trancost.VarCost : new Decimal?(0M);
        glTran13.CuryDebitAmt = nullable24;
        PX.Objects.GL.GLTran glTran14 = tran;
        invtMult1 = trancost.InvtMult;
        int? nullable25;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable25 = nullable1;
        }
        else
          nullable25 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable25;
        int num12 = 1;
        Decimal? nullable26 = nullable2.GetValueOrDefault() == num12 & nullable2.HasValue | flag3 ? trancost.VarCost : new Decimal?(0M);
        glTran14.DebitAmt = nullable26;
        PX.Objects.GL.GLTran glTran15 = tran;
        invtMult1 = trancost.InvtMult;
        int? nullable27;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable27 = nullable1;
        }
        else
          nullable27 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable27;
        int num13 = 1;
        Decimal? nullable28 = nullable2.GetValueOrDefault() == num13 & nullable2.HasValue | flag3 ? new Decimal?(0M) : trancost.VarCost;
        glTran15.CuryCreditAmt = nullable28;
        PX.Objects.GL.GLTran glTran16 = tran;
        invtMult1 = trancost.InvtMult;
        int? nullable29;
        if (!invtMult1.HasValue)
        {
          nullable1 = new int?();
          nullable29 = nullable1;
        }
        else
          nullable29 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable29;
        int num14 = 1;
        Decimal? nullable30 = nullable2.GetValueOrDefault() == num14 & nullable2.HasValue | flag3 ? new Decimal?(0M) : trancost.VarCost;
        glTran16.CreditAmt = nullable30;
      }
      tran.TranType = trancost.TranType;
      tran.TranClass = "N";
      PX.Objects.GL.GLTran glTran17 = tran;
      int num15;
      if (trancost.CostDocType == inTran.DocType && trancost.CostRefNbr == inTran.RefNbr)
      {
        nullable2 = tran.AccountID;
        if (nullable2.HasValue)
        {
          nullable2 = tran.SubID;
          num15 = nullable2.HasValue ? 1 : 0;
          goto label_75;
        }
      }
      num15 = 0;
label_75:
      bool? nullable31 = new bool?(num15 != 0);
      glTran17.ZeroPost = nullable31;
      tran.RefNbr = trancost.RefNbr;
      tran.InventoryID = trancost.InventoryID;
      PX.Objects.GL.GLTran glTran18 = tran;
      Decimal? nullable32;
      if (!flag1)
      {
        nullable32 = new Decimal?(0M);
      }
      else
      {
        invtMult1 = trancost.InvtMult;
        int? nullable33;
        if (!invtMult1.HasValue)
        {
          nullable2 = new int?();
          nullable33 = nullable2;
        }
        else
          nullable33 = new int?((int) invtMult1.GetValueOrDefault());
        nullable2 = nullable33;
        if (!(nullable2.GetValueOrDefault() == 1 | flag3))
        {
          nullable4 = trancost.Qty;
          if (!nullable4.HasValue)
          {
            nullable3 = new Decimal?();
            nullable32 = nullable3;
          }
          else
            nullable32 = new Decimal?(-nullable4.GetValueOrDefault());
        }
        else
          nullable32 = trancost.Qty;
      }
      glTran18.Qty = nullable32;
      tran.UOM = inventoryItem.BaseUnit;
      tran.TranDesc = inTran.TranDesc;
      tran.TranDate = trancost.TranDate;
      tran.TranPeriodID = trancost.TranPeriodID;
      tran.FinPeriodID = trancost.FinPeriodID;
      tran.ProjectID = ProjectDefaultAttribute.NonProject();
      tran.CostCodeID = CostCodeAttribute.DefaultCostCode;
      tran.Released = new bool?(true);
      PX.Objects.GL.GLTran glTran19 = tran;
      nullable22 = tran.SummPost;
      int? nullable34;
      if (!nullable22.GetValueOrDefault())
      {
        nullable34 = inTran.LineNbr;
      }
      else
      {
        nullable2 = new int?();
        nullable34 = nullable2;
      }
      glTran19.TranLineNbr = nullable34;
      this.InsertGLCostsCredit(je, tran, new INReleaseProcess.GLTranInsertionContext()
      {
        TranCost = trancost,
        INTran = inTran,
        Item = inventoryItem,
        Site = site,
        PostClass = postclass,
        ReasonCode = reasoncode,
        Location = location
      });
    }
label_89:
    nullable2 = trancost.COGSAcctID;
    if (!nullable2.HasValue)
    {
      nullable2 = inTran.AcctID;
      if (!nullable2.HasValue)
        goto label_153;
    }
    if (trancost.TranType == "TRX" && (trancost.CostDocType != inTran.DocType || trancost.CostRefNbr != inTran.RefNbr))
    {
      trancost.COGSAcctID = this.GetCogsAcctID(InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, project, task));
      trancost.COGSSubID = this.GetCogsSubID(InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, project, task));
    }
    if ((trancost.TranType == "ASY" || trancost.TranType == "DSY") && reasoncode != null)
    {
      nullable2 = reasoncode.AccountID;
      if (nullable2.HasValue && (trancost.CostDocType != inTran.DocType || trancost.CostRefNbr != inTran.RefNbr))
      {
        trancost.COGSAcctID = reasoncode.AccountID;
        trancost.COGSSubID = this.GetReasonCodeSubID(reasoncode, inventoryParams);
      }
    }
    if (inTran != null)
    {
      nullable22 = inTran.UpdateShippedNotInvoiced;
      if (nullable22.GetValueOrDefault() && trancost.TranType == "RCP" && (trancost.CostDocType != inTran.DocType || trancost.CostRefNbr != inTran.RefNbr))
      {
        trancost.COGSAcctID = this.GetCogsAcctID(InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, project, task));
        trancost.COGSSubID = this.GetCogsSubID(InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, project, task));
      }
    }
    if (this.IsUpdatablePOReturnTranCostFinal(inTran, inventoryItem) && (trancost.CostDocType != inTran.DocType || trancost.CostRefNbr != inTran.RefNbr))
    {
      trancost.COGSAcctID = INReleaseProcess.GetAcctID<INPostClass.pPVAcctID>((PXGraph) this, postclass.PPVAcctDefault, inventoryItem, site, postclass);
      try
      {
        trancost.COGSSubID = INReleaseProcess.GetSubID<INPostClass.pPVSubID>((PXGraph) this, postclass.PPVAcctDefault, postclass.PPVSubMask, inventoryItem, site, postclass);
      }
      catch (PXException ex)
      {
        throw new PXException("PPV Subaccount mask cannot be assembled correctly. Please, check settings for the Inventory Posting Class");
      }
    }
    PX.Objects.GL.GLTran tran1 = new PX.Objects.GL.GLTran();
    tran1.SummPost = new bool?((trancost.TranType == "TRX" || trancost.TranType == "ASY" || trancost.TranType == "DSY") && inTran.DocType == trancost.CostDocType || this.SummPost);
    PX.Objects.GL.GLTran glTran20 = tran1;
    nullable2 = trancost.COGSAcctID;
    int? nullable35;
    if (nullable2.HasValue)
    {
      nullable35 = inTran.BranchID;
    }
    else
    {
      nullable2 = inTran.DestBranchID;
      nullable35 = nullable2 ?? inTran.BranchID;
    }
    glTran20.BranchID = nullable35;
    PX.Objects.GL.GLTran glTran21 = tran1;
    nullable2 = trancost.COGSAcctID;
    int? nullable36 = nullable2 ?? inTran.AcctID;
    glTran21.AccountID = nullable36;
    PX.Objects.GL.GLTran glTran22 = tran1;
    nullable2 = this.GetValueInt<INTranCost.cOGSSubID>((PXGraph) je, (object) trancost);
    int? nullable37 = nullable2 ?? this.GetValueInt<INTran.subID>((PXGraph) je, (object) inTran);
    glTran22.SubID = nullable37;
    if (flag2)
    {
      tran1.CuryDebitAmt = flag3 ? new Decimal?(0M) : trancost.TranCost;
      tran1.DebitAmt = flag3 ? new Decimal?(0M) : trancost.TranCost;
      tran1.CuryCreditAmt = flag3 ? trancost.TranCost : new Decimal?(0M);
      tran1.CreditAmt = flag3 ? trancost.TranCost : new Decimal?(0M);
    }
    else
    {
      Decimal? nullable38;
      if (!(inventoryItem.ValMethod == "T"))
      {
        nullable22 = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsCorrection;
        if (!nullable22.GetValueOrDefault())
        {
          nullable38 = new Decimal?(0M);
          goto label_112;
        }
      }
      nullable38 = trancost.VarCost;
label_112:
      Decimal? nullable39 = nullable38;
      PX.Objects.GL.GLTran glTran23 = tran1;
      invtMult1 = trancost.InvtMult;
      int? nullable40;
      if (!invtMult1.HasValue)
      {
        nullable2 = new int?();
        nullable40 = nullable2;
      }
      else
        nullable40 = new int?((int) invtMult1.GetValueOrDefault());
      nullable2 = nullable40;
      Decimal? nullable41;
      if (!(nullable2.GetValueOrDefault() == 1 | flag3))
      {
        nullable4 = trancost.TranCost;
        nullable3 = nullable39;
        nullable41 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable41 = new Decimal?(0M);
      glTran23.CuryDebitAmt = nullable41;
      PX.Objects.GL.GLTran glTran24 = tran1;
      invtMult1 = trancost.InvtMult;
      int? nullable42;
      if (!invtMult1.HasValue)
      {
        nullable2 = new int?();
        nullable42 = nullable2;
      }
      else
        nullable42 = new int?((int) invtMult1.GetValueOrDefault());
      nullable2 = nullable42;
      Decimal? nullable43;
      if (!(nullable2.GetValueOrDefault() == 1 | flag3))
      {
        nullable3 = trancost.TranCost;
        nullable4 = nullable39;
        nullable43 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable43 = new Decimal?(0M);
      glTran24.DebitAmt = nullable43;
      PX.Objects.GL.GLTran glTran25 = tran1;
      invtMult1 = trancost.InvtMult;
      int? nullable44;
      if (!invtMult1.HasValue)
      {
        nullable2 = new int?();
        nullable44 = nullable2;
      }
      else
        nullable44 = new int?((int) invtMult1.GetValueOrDefault());
      nullable2 = nullable44;
      Decimal? nullable45;
      if (!(nullable2.GetValueOrDefault() == 1 | flag3))
      {
        nullable45 = new Decimal?(0M);
      }
      else
      {
        nullable4 = trancost.TranCost;
        nullable3 = nullable39;
        nullable45 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      }
      glTran25.CuryCreditAmt = nullable45;
      PX.Objects.GL.GLTran glTran26 = tran1;
      invtMult1 = trancost.InvtMult;
      int? nullable46;
      if (!invtMult1.HasValue)
      {
        nullable2 = new int?();
        nullable46 = nullable2;
      }
      else
        nullable46 = new int?((int) invtMult1.GetValueOrDefault());
      nullable2 = nullable46;
      Decimal? nullable47;
      if (!(nullable2.GetValueOrDefault() == 1 | flag3))
      {
        nullable47 = new Decimal?(0M);
      }
      else
      {
        nullable3 = trancost.TranCost;
        nullable4 = nullable39;
        nullable47 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      }
      glTran26.CreditAmt = nullable47;
    }
    tran1.TranType = trancost.TranType;
    tran1.TranClass = "N";
    PX.Objects.GL.GLTran glTran27 = tran1;
    int num16;
    if (trancost.CostDocType == inTran.DocType && trancost.CostRefNbr == inTran.RefNbr)
    {
      nullable2 = tran1.AccountID;
      if (nullable2.HasValue)
      {
        nullable2 = tran1.SubID;
        num16 = nullable2.HasValue ? 1 : 0;
        goto label_141;
      }
    }
    num16 = 0;
label_141:
    bool? nullable48 = new bool?(num16 != 0);
    glTran27.ZeroPost = nullable48;
    tran1.RefNbr = trancost.RefNbr;
    tran1.InventoryID = trancost.InventoryID;
    PX.Objects.GL.GLTran glTran28 = tran1;
    invtMult1 = trancost.InvtMult;
    int? nullable49;
    if (!invtMult1.HasValue)
    {
      nullable1 = new int?();
      nullable49 = nullable1;
    }
    else
      nullable49 = new int?((int) invtMult1.GetValueOrDefault());
    nullable2 = nullable49;
    int num17 = 1;
    Decimal? nullable50;
    if (!(nullable2.GetValueOrDefault() == num17 & nullable2.HasValue | flag3))
    {
      nullable50 = trancost.Qty;
    }
    else
    {
      nullable4 = trancost.Qty;
      if (!nullable4.HasValue)
      {
        nullable3 = new Decimal?();
        nullable50 = nullable3;
      }
      else
        nullable50 = new Decimal?(-nullable4.GetValueOrDefault());
    }
    glTran28.Qty = nullable50;
    tran1.UOM = inventoryItem.BaseUnit;
    tran1.TranDesc = inTran.TranDesc;
    tran1.TranDate = trancost.TranDate;
    tran1.TranPeriodID = trancost.TranPeriodID;
    tran1.FinPeriodID = trancost.FinPeriodID;
    tran1.ProjectID = ProjectDefaultAttribute.NonProject();
    tran1.CostCodeID = CostCodeAttribute.DefaultCostCode;
    tran1.Released = new bool?(true);
    PX.Objects.GL.GLTran glTran29 = tran1;
    nullable22 = tran1.SummPost;
    int? nullable51;
    if (!nullable22.GetValueOrDefault())
    {
      nullable51 = inTran.LineNbr;
    }
    else
    {
      nullable2 = new int?();
      nullable51 = nullable2;
    }
    glTran29.TranLineNbr = nullable51;
    this.InsertGLCostsOversold(je, tran1, new INReleaseProcess.GLTranInsertionContext()
    {
      TranCost = trancost,
      INTran = inTran,
      Item = inventoryItem,
      Site = site,
      PostClass = postclass,
      ReasonCode = reasoncode,
      Location = location
    });
label_153:
    if (this.WIPCalculated && inTran.AssyType == "K" && string.Equals(trancost.CostDocType, inTran.DocType) && string.Equals(trancost.CostRefNbr, inTran.RefNbr))
    {
      PX.Objects.GL.GLTran tran2 = new PX.Objects.GL.GLTran();
      tran2.SummPost = new bool?(true);
      tran2.ZeroPost = new bool?(false);
      tran2.BranchID = inTran.BranchID;
      tran2.AccountID = this.INProgressAcctID;
      tran2.SubID = this.INProgressSubID;
      tran2.ReclassificationProhibited = new bool?(true);
      tran2.CuryDebitAmt = new Decimal?(0M);
      tran2.DebitAmt = new Decimal?(0M);
      tran2.CuryCreditAmt = this.WIPVariance;
      tran2.CreditAmt = this.WIPVariance;
      tran2.TranType = inTran.TranType;
      tran2.TranClass = "N";
      tran2.RefNbr = inTran.RefNbr;
      tran2.InventoryID = inTran.InventoryID;
      tran2.TranDesc = PXMessages.LocalizeNoPrefix("Production Variance");
      tran2.TranDate = inTran.TranDate;
      tran2.TranPeriodID = inTran.TranPeriodID;
      tran2.FinPeriodID = inTran.FinPeriodID;
      tran2.ProjectID = ProjectDefaultAttribute.NonProject();
      tran2.CostCodeID = CostCodeAttribute.DefaultCostCode;
      tran2.Released = new bool?(true);
      PX.Objects.GL.GLTran glTran30 = tran2;
      nullable2 = new int?();
      int? nullable52 = nullable2;
      glTran30.TranLineNbr = nullable52;
      this.InsertGLCostsVarianceCredit(je, tran2, new INReleaseProcess.GLTranInsertionContext()
      {
        TranCost = trancost,
        INTran = inTran,
        Item = inventoryItem,
        Site = site,
        PostClass = postclass,
        ReasonCode = reasoncode,
        Location = location
      });
    }
    if (!this.WIPCalculated || !(inTran.AssyType == "K") || !string.Equals(trancost.CostDocType, inTran.DocType) || !string.Equals(trancost.CostRefNbr, inTran.RefNbr))
      return;
    PX.Objects.GL.GLTran tran3 = new PX.Objects.GL.GLTran();
    tran3.SummPost = new bool?(this.SummPost);
    tran3.BranchID = inTran.BranchID;
    tran3.AccountID = reasoncode.AccountID;
    tran3.SubID = this.GetReasonCodeSubID(reasoncode, inventoryParams);
    tran3.CuryDebitAmt = this.WIPVariance;
    tran3.DebitAmt = this.WIPVariance;
    tran3.CuryCreditAmt = new Decimal?(0M);
    tran3.CreditAmt = new Decimal?(0M);
    tran3.TranType = inTran.TranType;
    tran3.TranClass = "N";
    tran3.RefNbr = inTran.RefNbr;
    tran3.InventoryID = inTran.InventoryID;
    tran3.TranDesc = PXMessages.LocalizeNoPrefix("Production Variance");
    tran3.TranDate = inTran.TranDate;
    tran3.TranPeriodID = inTran.TranPeriodID;
    tran3.FinPeriodID = inTran.FinPeriodID;
    tran3.ProjectID = ProjectDefaultAttribute.NonProject();
    tran3.CostCodeID = CostCodeAttribute.DefaultCostCode;
    tran3.Released = new bool?(true);
    PX.Objects.GL.GLTran glTran31 = tran3;
    nullable2 = new int?();
    int? nullable53 = nullable2;
    glTran31.TranLineNbr = nullable53;
    this.InsertGLCostsVarianceDebit(je, tran3, new INReleaseProcess.GLTranInsertionContext()
    {
      TranCost = trancost,
      INTran = inTran,
      Item = inventoryItem,
      Site = site,
      PostClass = postclass,
      ReasonCode = reasoncode,
      Location = location
    });
    this.WIPCalculated = false;
    this.WIPVariance = new Decimal?(0M);
  }

  public virtual PX.Objects.GL.GLTran InsertGLCostsDebit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual PX.Objects.GL.GLTran InsertGLCostsCredit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual PX.Objects.GL.GLTran InsertGLCostsOversold(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual PX.Objects.GL.GLTran InsertGLCostsVarianceCredit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public virtual PX.Objects.GL.GLTran InsertGLCostsVarianceDebit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public object GetValueExt<Field>(PXCache cache, object data) where Field : class, IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }

  public virtual void ParseSubItemSegKeys()
  {
    if (this._SubItemSeg != null)
      return;
    this._SubItemSeg = new List<Segment>();
    foreach (PXResult<Segment> pxResult in PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<SubItemAttribute.dimensionName>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      this._SubItemSeg.Add(PXResult<Segment>.op_Implicit(pxResult));
    this._SubItemSegVal = new Dictionary<short?, string>();
    foreach (PXResult<SegmentValue> pxResult in PXSelectBase<SegmentValue, PXSelectJoin<SegmentValue, InnerJoin<Segment, On<Segment.dimensionID, Equal<SegmentValue.dimensionID>, And<Segment.segmentID, Equal<SegmentValue.segmentID>>>>, Where<SegmentValue.dimensionID, Equal<SubItemAttribute.dimensionName>, And<Segment.isCosted, Equal<boolFalse>, And<SegmentValue.isConsolidatedValue, Equal<boolTrue>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      SegmentValue segmentValue = PXResult<SegmentValue>.op_Implicit(pxResult);
      try
      {
        this._SubItemSegVal.Add(new short?(segmentValue.SegmentID.Value), segmentValue.Value);
      }
      catch (Exception ex)
      {
        object[] objArray = new object[2]
        {
          (object) segmentValue.SegmentID,
          (object) segmentValue.DimensionID
        };
        throw new PXException(ex, "The '{0}' segment of the '{1}' segmented key has more than one value with the Aggregation check box selected  on the Segment Values (CS203000) form.", objArray);
      }
    }
  }

  public virtual string MakeCostSubItemCD(string SubItemCD)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num1 = 0;
    foreach (Segment segment in this._SubItemSeg)
    {
      string str1 = SubItemCD;
      int startIndex = num1;
      short? length1 = segment.Length;
      int length2 = (int) length1.Value;
      string str2 = str1.Substring(startIndex, length2);
      if (segment.IsCosted.GetValueOrDefault() || str2.TrimEnd() == string.Empty)
      {
        stringBuilder.Append(str2);
      }
      else
      {
        if (!this._SubItemSegVal.TryGetValue(segment.SegmentID, out str2))
          throw new PXException("Subitem Segmented Key missing one or more Consolidated values.");
        string str3 = str2;
        length1 = segment.Length;
        int totalWidth = (int) length1.Value;
        str2 = str3.PadRight(totalWidth);
        stringBuilder.Append(str2);
      }
      int num2 = num1;
      length1 = segment.Length;
      int num3 = (int) length1.Value;
      num1 = num2 + num3;
    }
    return stringBuilder.ToString();
  }

  public virtual void UpdateCrossReference(
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    INLocation whseloc)
  {
    if (item.ValMethod != "T" && item.ValMethod != "S" && whseloc == null)
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<INTran.locationID>(((PXSelectBase) this.intranselect).Cache)
      });
    if (!split.SubItemID.HasValue)
      throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<INTran.subItemID>(((PXSelectBase) this.intranselect).Cache)
      });
    INCostSubItemXRef data = new INCostSubItemXRef();
    data.SubItemID = split.SubItemID;
    data.CostSubItemID = split.SubItemID;
    string valueExt = (string) this.GetValueExt<INCostSubItemXRef.costSubItemID>(((PXSelectBase) this.costsubitemxref).Cache, (object) data);
    data.CostSubItemID = new int?();
    string str = PXAccess.FeatureInstalled<FeaturesSet.subItem>() ? this.MakeCostSubItemCD(valueExt) : valueExt;
    ((PXSelectBase) this.costsubitemxref).Cache.SetValueExt<INCostSubItemXRef.costSubItemID>((object) data, (object) str);
    INCostSubItemXRef inCostSubItemXref = ((PXSelectBase<INCostSubItemXRef>) this.costsubitemxref).Update(data);
    if (((PXSelectBase) this.costsubitemxref).Cache.GetStatus((object) inCostSubItemXref) == 1)
      ((PXSelectBase) this.costsubitemxref).Cache.SetStatus((object) inCostSubItemXref, (PXEntryStatus) 0);
    split.CostSubItemID = inCostSubItemXref.CostSubItemID;
    INTranSplit inTranSplit = split;
    int? costCenterId = tran.CostCenterID;
    int num = 0;
    int? nullable = costCenterId.GetValueOrDefault() == num & costCenterId.HasValue ? (!EnumerableExtensions.IsNotIn<string>(item.ValMethod, "T", "S") || !whseloc.IsCosted.GetValueOrDefault() ? split.SiteID : whseloc.LocationID) : tran.CostCenterID;
    inTranSplit.CostSiteID = nullable;
  }

  public virtual void ReleaseDocProcR(JournalEntry je, PX.Objects.IN.INRegister doc, bool releaseFromHold = false)
  {
    int num = 5;
    while (true)
    {
      try
      {
        this.ReleaseDocProc(je, doc, releaseFromHold);
        break;
      }
      catch (PXRestartOperationException ex)
      {
        if (num-- < 0)
        {
          if (((Exception) ex).InnerException != null)
            throw ((Exception) ex).InnerException;
          throw;
        }
        ((PXGraph) this).Clear();
      }
    }
  }

  public virtual void UpdateSplitDestinationLocation(INTran tran, INTranSplit split, int? value)
  {
    split.ToLocationID = value;
    if (this.IsOneStepTransferWithinSite())
    {
      INLocation parent1 = (INLocation) PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.locationID>.FindParent((PXGraph) this, (INTranSplit.locationID) split, (PKFindOptions) 0);
      INLocation parent2 = (INLocation) PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.toLocationID>.FindParent((PXGraph) this, (INTranSplit.toLocationID) split, (PKFindOptions) 0);
      split.SkipCostUpdate = new bool?((parent1 != null ? (!parent1.IsCosted.GetValueOrDefault() ? 1 : 0) : 1) != 0 && (parent2 == null || !parent2.IsCosted.GetValueOrDefault()));
      split.SkipQtyValidation = new bool?(parent2 != null && parent2.IsSorting.GetValueOrDefault());
    }
    GraphHelper.MarkUpdated(((PXSelectBase) this.intransplit).Cache, (object) split, true);
  }

  public virtual bool IsOneStepTransfer()
  {
    PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current;
    return current.DocType == "T" && current.TransferType == "1";
  }

  public virtual bool IsOneStepTransferWithinSite()
  {
    PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current;
    if (!(current.DocType == "T") || !(current.TransferType == "1"))
      return false;
    int? siteId = current.SiteID;
    int? toSiteId = current.ToSiteID;
    return siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue;
  }

  public virtual void ValidateTransferDocIntegrity(PX.Objects.IN.INRegister doc)
  {
    foreach (PXResult<INTranSplit, INTran, INItemPlan> pxResult in PXSelectBase<INTranSplit, PXSelectReadonly2<INTranSplit, InnerJoin<INTran, On<INTranSplit.FK.Tran>, LeftJoin<INItemPlan, On<INTranSplit.FK.ItemPlan>>>, Where<INTranSplit.docType, Equal<Required<INTranSplit.docType>>, And<INTranSplit.refNbr, Equal<Required<INTranSplit.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      INTranSplit split = PXResult<INTranSplit, INTran, INItemPlan>.op_Implicit(pxResult);
      INTran inTran = PXResult<INTranSplit, INTran, INItemPlan>.op_Implicit(pxResult);
      INItemPlan plan = PXResult<INTranSplit, INTran, INItemPlan>.op_Implicit(pxResult);
      if (!(split.TransferType != doc.TransferType))
      {
        if (split.TransferType == "2")
        {
          bool? nullable = this.TranSplitPlanExt.IsTwoStepTransferPlanValid(split, plan);
          bool flag = false;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
            goto label_5;
        }
        if (split.TransferType == "1")
        {
          short? invtMult = split.InvtMult;
          int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          int num = -1;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          {
            nullable = inTran.ToLocationID;
            if (!nullable.HasValue)
              throw new PXException("The document cannot be released because the To Location ID column on the Details tab is empty for at least one transfer line.");
            continue;
          }
          continue;
        }
        continue;
      }
label_5:
      throw new PXException("The database record that corresponds to the {0} transfer is corrupted. Please contact your Acumatica support provider.", new object[1]
      {
        (object) doc.RefNbr
      });
    }
  }

  public virtual void ValidateKitAssembly(PX.Objects.IN.INRegister doc)
  {
    if (doc.DocType != "P")
      return;
    PXResult pxResult = (PXResult) PXResultset<INKitRegister>.op_Implicit(PXSelectBase<INKitRegister, PXViewOf<INKitRegister>.BasedOn<SelectFromBase<INKitRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<BqlOperand<INLocation.locationID, IBqlInt>.IsEqual<INKitRegister.locationID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitRegister.docType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<INKitRegister.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    if (pxResult != null && pxResult[typeof (INLocation)] is INLocation inLocation && !inLocation.AssemblyValid.GetValueOrDefault())
      throw new PXException("Assemblies are not allowed in the {0} location. Change the location, or select the Assembly Allowed check box on the Warehouses (IN204000) form for the current location.", new object[1]
      {
        (object) inLocation.LocationCD
      });
  }

  public virtual void ValidateOrigLCReceiptIsReleased(PX.Objects.IN.INRegister doc)
  {
    if (doc.DocType != "A")
      return;
    foreach (PXResult<INTran, INTran2> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, LeftJoin<INTran2, On<INTran2.docType, In3<INDocType.receipt, INDocType.issue>, And<INTran2.pOReceiptType, Equal<INTran.pOReceiptType>, And<INTran2.pOReceiptNbr, Equal<INTran.pOReceiptNbr>, And<INTran2.pOReceiptLineNbr, Equal<INTran.pOReceiptLineNbr>>>>>>, Where<INTran.docType, Equal<Current<PX.Objects.IN.INRegister.docType>>, And<INTran.refNbr, Equal<Current<PX.Objects.IN.INRegister.refNbr>>, And<INTran.tranType, In3<INTranType.adjustment, INTranType.receiptCostAdjustment>, And<INTran.pOReceiptType, IsNotNull, And<INTran.pOReceiptNbr, IsNotNull, And<INTran.pOReceiptLineNbr, IsNotNull>>>>>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) new PX.Objects.IN.INRegister[1]
    {
      doc
    }, Array.Empty<object>()))
    {
      INTran inTran = PXResult<INTran, INTran2>.op_Implicit(pxResult);
      INTran2 inTran2 = PXResult<INTran, INTran2>.op_Implicit(pxResult);
      if (inTran.TranType == "RCA" && (inTran2 != null ? (!inTran2.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        throw new PXException(!doc.IsPPVTran.GetValueOrDefault() ? (!doc.IsTaxAdjustmentTran.GetValueOrDefault() ? "IN Receipt# '{0}' created from PO Receipt# '{1}' must be released before the Landed Cost may be processed" : "The {0} inventory receipt created from the {1} purchase receipt must be released before the tax adjustment transaction.") : "The {0} inventory receipt created from the {1} purchase receipt must be released before the purchase price variance transaction.", new object[2]
        {
          (object) (inTran2?.RefNbr ?? string.Empty),
          (object) inTran.POReceiptNbr
        });
      if (inTran.TranType == "ADJ" && string.IsNullOrEmpty(inTran.ARRefNbr))
      {
        PX.Objects.PO.POReceiptLine poReceiptLine = PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) this, inTran.POReceiptType, inTran.POReceiptNbr, inTran.POReceiptLineNbr);
        if (POLineType.IsDropShip(poReceiptLine?.LineType))
        {
          if (string.IsNullOrEmpty(inTran2?.RefNbr))
          {
            PX.Objects.SO.SOLineSplit soLineSplit = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOType, Equal<BqlField<PX.Objects.PO.POReceiptLine.pOType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, new object[1]
            {
              (object) poReceiptLine
            }, Array.Empty<object>()));
            if (soLineSplit != null)
              throw new PXException("The inventory issue related to the {0} purchase receipt was not found. Prepare and release a sales invoice for the {1} sales order with the {2} type first.", new object[3]
              {
                (object) poReceiptLine.ReceiptNbr,
                (object) soLineSplit.OrderNbr,
                (object) soLineSplit.OrderType
              });
          }
          else
          {
            inTran.ARDocType = inTran2.ARDocType;
            inTran.ARRefNbr = inTran2.ARRefNbr;
            inTran.ARLineNbr = inTran2.ARLineNbr;
            GraphHelper.MarkUpdated(((PXSelectBase) this.intranselect).Cache, (object) inTran, true);
          }
        }
      }
    }
  }

  private void ValidateUnreleasedStandartCostAdjustments(INTran tran, PX.Objects.IN.InventoryItem item)
  {
    if (item.ValMethod != "T")
      return;
    PXDataField[] pxDataFieldArray = new PXDataField[8]
    {
      (PXDataField) new PXDataField<INTran.refNbr>(),
      (PXDataField) new PXDataFieldValue<INTran.released>((PXDbType) 2, (object) false),
      (PXDataField) new PXDataFieldValue<INTran.inventoryID>((PXDbType) 8, (object) tran.InventoryID),
      (PXDataField) new PXDataFieldValue<INTran.siteID>((PXDbType) 8, (object) tran.SiteID),
      (PXDataField) new PXDataFieldValue<INTran.refNbr>((PXDbType) 3, new int?(), (object) tran.RefNbr, (PXComp) 1),
      (PXDataField) new PXDataFieldValue<INTran.docType>((PXDbType) 3, new int?(1), (object) "A", (PXComp) 0),
      null,
      null
    };
    PXDataFieldValue<INTran.tranType> pxDataFieldValue1 = new PXDataFieldValue<INTran.tranType>((PXDbType) 3, new int?(3), (object) "ASC", (PXComp) 0);
    ((PXDataFieldValue) pxDataFieldValue1).OpenBrackets = 1;
    pxDataFieldArray[6] = (PXDataField) pxDataFieldValue1;
    PXDataFieldValue<INTran.tranType> pxDataFieldValue2 = new PXDataFieldValue<INTran.tranType>((PXDbType) 3, new int?(3), (object) "NSC", (PXComp) 0);
    ((PXDataFieldValue) pxDataFieldValue2).CloseBrackets = 1;
    ((PXDataFieldValue) pxDataFieldValue2).OrOperator = true;
    pxDataFieldArray[7] = (PXDataField) pxDataFieldValue2;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INTran>(pxDataFieldArray))
    {
      if (pxDataRecord != null)
        throw new PXException("The document cannot be released because the {0} inventory item is included in the {1} unreleased standard cost inventory adjustment. To release the document, you need to release the {1} inventory adjustment by using the Adjustments (IN303000) or Release IN Documents (IN501000) form.", new object[2]
        {
          (object) item.InventoryCD,
          (object) pxDataRecord.GetString(0)
        });
    }
  }

  public virtual void ReleaseDocProc(JournalEntry je, PX.Objects.IN.INRegister doc, bool releaseFromHold = false)
  {
    doc = this.ActualizeAndValidateINRegister(doc, releaseFromHold);
    if (doc.DocType == "T")
      this.ValidateTransferDocIntegrity(doc);
    this.ValidateKitAssembly(doc);
    this.ValidateOrigLCReceiptIsReleased(doc);
    using (this.TranSplitPlanExt.ReleaseModeScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.SegregateBatch(je, doc.BranchID, doc.BranchBaseCuryID, doc.TranDate, doc.FinPeriodID, doc.TranDesc);
        INTran objA = (INTran) null;
        int? nullable1 = new int?();
        if (this.IsOneStepTransfer())
        {
          foreach (PXResult<INTran, INTranSplit> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<INTranSplit, On<INTranSplit.FK.Tran>>, Where<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.docType, Equal<INDocType.transfer>, And<INTran.invtMult, Equal<shortMinus1>>>>>, OrderBy<Asc<INTran.tranType, Asc<INTran.refNbr, Asc<INTran.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) doc.DocType,
            (object) doc.RefNbr
          }))
          {
            INTran inTran1 = PXResult<INTran, INTranSplit>.op_Implicit(pxResult);
            INTranSplit split = ((PXSelectBase<INTranSplit>) this.intransplit).Locate(PXResult<INTran, INTranSplit>.op_Implicit(pxResult)) ?? PXResult<INTran, INTranSplit>.op_Implicit(pxResult);
            this.UpdateSplitDestinationLocation(inTran1, split, inTran1.ToLocationID);
            if (!object.Equals((object) objA, (object) inTran1))
            {
              INTran inTran2 = ((PXSelectBase<INTran>) this.intranselect).Insert(this.CreatePositiveOneStepTransferINTran(doc, inTran1, split));
              objA = inTran1;
              nullable1 = inTran2.LineNbr;
            }
            INTranSplit copy = PXCache<INTranSplit>.CreateCopy(split);
            copy.LineNbr = nullable1;
            copy.SplitLineNbr = new int?((int) PXLineNbrAttribute.NewLineNbr<INTranSplit.splitLineNbr>(((PXSelectBase) this.intransplit).Cache, (object) doc));
            copy.InvtMult = new short?((short) 1);
            copy.SiteID = inTran1.ToSiteID;
            copy.LocationID = inTran1.ToLocationID;
            copy.FromSiteID = split.SiteID;
            copy.FromLocationID = split.LocationID;
            copy.SkipCostUpdate = split.SkipCostUpdate;
            copy.PlanID = new long?();
            ((PXSelectBase<INTranSplit>) this.intransplit).Insert(copy);
          }
        }
        PXResultset<INTran, INTranSplit, PX.Objects.IN.InventoryItem> originalintranlist = new PXResultset<INTran, INTranSplit, PX.Objects.IN.InventoryItem>();
        foreach (PXResult<INTran, PX.Objects.IN.InventoryItem, INLocation, INLotSerClass> pxResult in PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<PX.Objects.IN.InventoryItem, On<INTran.FK.InventoryItem>, LeftJoin<INLocation, On<INTran.FK.Location>, InnerJoin<INLotSerClass, On<PX.Objects.IN.InventoryItem.FK.LotSerialClass>>>>, Where<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.tranType, Equal<INTranType.receiptCostAdjustment>>>>, OrderBy<Asc<INTran.tranType, Asc<INTran.refNbr, Asc<INTran.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        }))
        {
          PX.Objects.IN.InventoryItem inventoryItem = PXResult<INTran, PX.Objects.IN.InventoryItem, INLocation, INLotSerClass>.op_Implicit(pxResult);
          INLocation whseloc = PXResult<INTran, PX.Objects.IN.InventoryItem, INLocation, INLotSerClass>.op_Implicit(pxResult);
          INTran tran = PXResult<INTran, PX.Objects.IN.InventoryItem, INLocation, INLotSerClass>.op_Implicit(pxResult);
          INTranSplit split = INTranSplit.FromINTran(tran);
          this.UpdateCrossReference(tran, split, inventoryItem, whseloc);
          ((PXResultset<INTran>) originalintranlist).Add((PXResult<INTran>) new PXResult<INTran, INTranSplit, PX.Objects.IN.InventoryItem>(tran, split, inventoryItem));
        }
        this.RegenerateInTranList(originalintranlist);
        if (((PXSelectBase) this.intranselect).Cache.IsDirty)
        {
          ((PXGraph) this).Persist(typeof (INTran), (PXDBOperation) 2);
          ((PXGraph) this).Persist(typeof (INTranSplit), (PXDBOperation) 2);
          ((PXGraph) this).Persist(typeof (INItemPlan), (PXDBOperation) 2);
          byte[] timeStamp = ((PXGraph) this).TimeStamp;
          try
          {
            ((PXGraph) this).TimeStamp = PXDatabase.SelectTimeStamp();
            ((PXSelectBase) this.intranselect).Cache.Persisted(false);
            ((PXSelectBase) this.intransplit).Cache.Persisted(false);
            ((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) this)).Persisted(false);
          }
          finally
          {
            ((PXGraph) this).TimeStamp = timeStamp;
          }
        }
        foreach (PXResult<INTransitLine> pxResult in PXSelectBase<INTransitLine, PXSelectJoin<INTransitLine, InnerJoin<INTran, On<INTran.origRefNbr, Equal<INTransitLine.transferNbr>, And<INTran.origLineNbr, Equal<INTransitLine.transferLineNbr>>>>, Where<INTran.docType, Equal<Current<PX.Objects.IN.INRegister.docType>>, And<INTran.refNbr, Equal<Current<PX.Objects.IN.INRegister.refNbr>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) doc
        }, Array.Empty<object>()))
          PXResult<INTransitLine>.op_Implicit(pxResult);
        foreach (PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite> pxResult1 in PXSelectBase<INTranSplit, PXSelectJoin<INTranSplit, InnerJoin<INTran, On<INTranSplit.FK.Tran>, InnerJoin<INItemPlan, On<INTranSplit.FK.ItemPlan>, LeftJoinSingleTable<INItemSite, On<INItemSite.inventoryID, Equal<INTran.inventoryID>, And<INItemSite.siteID, Equal<INTran.toSiteID>>>, LeftJoin<PX.Objects.IN.INSite, On<INTran.FK.ToSite>>>>>, Where<INTranSplit.docType, Equal<Required<INTranSplit.docType>>, And<INTranSplit.refNbr, Equal<Required<INTranSplit.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        }))
        {
          INTranSplit split = PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite>.op_Implicit(pxResult1);
          INTran tran = PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite>.op_Implicit(pxResult1);
          INItemPlan plan = PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite>.op_Implicit(pxResult1);
          INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this, plan.PlanType);
          PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite> pxResult2 = pxResult1;
          INItemSite itemsite = pxResult2 != null ? PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite>.op_Implicit(pxResult2) : new INItemSite();
          PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite> pxResult3 = pxResult1;
          PX.Objects.IN.INSite site = pxResult3 != null ? PXResult<INTranSplit, INTran, INItemPlan, INItemSite, PX.Objects.IN.INSite>.op_Implicit(pxResult3) : new PX.Objects.IN.INSite();
          PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) this);
          ((PXCache) pxCache).SetStatus((object) plan, (PXEntryStatus) 0);
          if ((this.IsOneStepTransfer() || inPlanType.DeleteOnEvent.GetValueOrDefault() || !(inPlanType.ReplanOnEvent == "42") ? (!(inPlanType.PlanType == "62") || !(doc.OrigModule == "SO") ? 0 : (doc.DocType == "T" ? 1 : 0)) : 1) != 0)
            this.ReattachPlanToTransit(plan, tran, split, itemsite, site);
          else if (inPlanType.DeleteOnEvent.GetValueOrDefault())
          {
            pxCache.Delete(plan);
            GraphHelper.MarkUpdated(((PXSelectBase) this.intransplit).Cache, (object) split, true);
            INTranSplit inTranSplit = (INTranSplit) ((PXSelectBase) this.intransplit).Cache.Locate((object) split);
            if (inTranSplit != null)
              inTranSplit.PlanID = new long?();
          }
          else if (!string.IsNullOrEmpty(inPlanType.ReplanOnEvent))
          {
            INItemPlan copy = PXCache<INItemPlan>.CreateCopy(plan);
            copy.PlanType = inPlanType.ReplanOnEvent;
            pxCache.Update(copy);
          }
        }
        this.ProcessLinkedAllocation(doc);
        PXFormulaAttribute.SetAggregate<INTranCost.qty>(((PXSelectBase) this.intrancost).Cache, typeof (SumCalc<INTran.costedQty>), (Type) null);
        PXFormulaAttribute.SetAggregate<INTranCost.tranCost>(((PXSelectBase) this.intrancost).Cache, typeof (SumCalc<INTran.tranCost>), (Type) null);
        PXFormulaAttribute.SetAggregate<INTranCost.tranAmt>(((PXSelectBase) this.intrancost).Cache, typeof (SumCalc<INTran.tranAmt>), (Type) null);
        INTran prev_tran = (INTran) null;
        IEnumerable<PXResult<INTran>> source = ((IEnumerable<PXResult<INTran>>) this.GetMainSelect(doc).Select(new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        })).AsEnumerable<PXResult<INTran>>();
        bool flag1 = false;
        foreach (PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation> pxResult in source)
        {
          INTran tran = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          PX.Objects.IN.INSite site = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          INTranSplit split = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          PX.Objects.IN.InventoryItem inventoryItem = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          this.ValidateTran(doc, tran, split, inventoryItem, site);
          INTran copy = PXCache<INTran>.CreateCopy(tran);
          copy.TranDate = doc.TranDate;
          copy.TranPeriodID = doc.TranPeriodID;
          ((PXSelectBase) this.intranselect).Cache.SetDefaultExt<INTran.finPeriodID>((object) copy);
          ((PXSelectBase<INTran>) this.intranselect).Update(copy);
          flag1 = true;
        }
        if (!flag1 && doc.OrigModule == "IN" && EnumerableExtensions.IsIn<string>(doc.DocType, "R", "I", "T"))
          throw new PXException("The document cannot be released because it does not contain any lines on the Details tab.");
        this.ValidateFinPeriod(doc, source.Cast<PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>>());
        foreach (PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation> pxResult in source)
        {
          INTran inTran3 = (INTran) ((PXSelectBase) this.intranselect).Cache.Locate((object) ((PXResult) pxResult).GetItem<INTran>()) ?? ((PXResult) pxResult).GetItem<INTran>();
          INTranSplit inTranSplit1 = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          INTranSplit split = inTranSplit1.RefNbr != null ? inTranSplit1 : INTranSplit.FromINTran(inTran3);
          PX.Objects.IN.InventoryItem inventoryItem = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          PX.Objects.IN.INSite site = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          PX.Objects.CS.ReasonCode reasoncode = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          INLocation whseloc = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult).LocationID.HasValue ? PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult) : INLocation.PK.Find((PXGraph) this, inTran3.LocationID);
          INPostClass postclass = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          INLotSerClass lsclass = PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite, INPostClass, INLotSerClass, INTranSplit, PX.Objects.CS.ReasonCode, INLocation>.op_Implicit(pxResult);
          PX.Objects.PM.Lite.PMProject project;
          PX.Objects.PM.Lite.PMTask task;
          this.TryToGetProjectAndTask((PXResult) pxResult, out project, out task);
          PXParentAttribute.SetParent(((PXSelectBase) this.intranselect).Cache, (object) inTran3, typeof (PX.Objects.IN.INRegister), (object) ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current);
          PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.StoreResult((PXGraph) this, (PX.Objects.IN.InventoryItem.inventoryID) inventoryItem, false);
          PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.Dirty.StoreResult((PXGraph) this, (PX.Objects.IN.INSite.siteID) site, false);
          PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.StoreResult((PXGraph) this, (PX.Objects.CS.ReasonCode.reasonCodeID) reasoncode, false);
          PrimaryKeyOf<INLocation>.By<INLocation.locationID>.Dirty.StoreResult((PXGraph) this, (INLocation.locationID) whseloc, false);
          PrimaryKeyOf<INPostClass>.By<INPostClass.postClassID>.StoreResult((PXGraph) this, (INPostClass.postClassID) postclass, false);
          PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.StoreResult((PXGraph) this, (INLotSerClass.lotSerClassID) lsclass, false);
          PrimaryKeyOf<PX.Objects.PM.Lite.PMProject>.By<PX.Objects.PM.Lite.PMProject.contractID>.Dirty.StoreResult((PXGraph) this, (PX.Objects.PM.Lite.PMProject.contractID) project, false);
          PrimaryKeyOf<PX.Objects.PM.Lite.PMTask>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PM.Lite.PMTask.projectID, PX.Objects.PM.Lite.PMTask.taskID>>.StoreResult((PXGraph) this, (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PM.Lite.PMTask.projectID, PX.Objects.PM.Lite.PMTask.taskID>) task, false);
          INTran copy = PXCache<INTran>.CreateCopy(inTran3);
          copy.Released = new bool?(true);
          INTran inTran4 = ((PXSelectBase<INTran>) this.intranselect).Update(copy);
          if (split.CreatedDateTime.HasValue)
          {
            INTranSplit inTranSplit2 = ((PXSelectBase<INTranSplit>) this.intransplit).Locate(split) ?? split;
            inTranSplit2.TranDate = doc.TranDate;
            inTranSplit2.Released = new bool?(true);
            split = ((PXSelectBase<INTranSplit>) this.intransplit).Update(inTranSplit2);
          }
          int num1;
          if (inTran4.TranType == "TRX")
          {
            short? invtMult = inTran4.InvtMult;
            int? nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
            int num2 = 0;
            if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
            {
              num1 = POLineType.IsNonStockNonServiceNonDropShip(inTran4.POLineType) ? 1 : 0;
              goto label_62;
            }
          }
          num1 = 0;
label_62:
          bool flag2 = num1 != 0;
          Decimal? nullable3;
          if (!(inTran4.TranType == "ADJ") && !(inTran4.TranType == "NSC") && !(inTran4.TranType == "ASC") && !(inTran4.TranType == "RCA"))
          {
            nullable3 = inTran4.Qty;
            Decimal num3 = 0M;
            if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
              goto label_76;
          }
          if (!flag2)
          {
            if (inventoryItem.StkItem.GetValueOrDefault() && !this.IsProjectDropShip(inTran4))
            {
              this.UpdateCrossReference(inTran4, split, inventoryItem, whseloc);
              this.UpdateItemSite(InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, inTran4, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task), reasoncode);
              nullable3 = split.BaseQty;
              Decimal num4 = 0M;
              if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
              {
                this.UpdateSiteStatus(inTran4, split, whseloc);
                this.UpdateLocationStatus(inTran4, split);
                this.UpdateLotSerialStatus(inTran4, split, inventoryItem, lsclass);
                this.UpdateSiteHist(inTran4, split);
                this.UpdateSiteHistByCostCenterD(inTran4, split);
                this.UpdateSiteHistDay(inTran4, split);
              }
              this.UpdateItemLotSerial(inTran4, split, inventoryItem, lsclass);
              this.UpdateSiteLotSerial(inTran4, split, inventoryItem, lsclass);
              if (!split.SkipCostUpdate.GetValueOrDefault())
                this.UpdateCostStatus(prev_tran, inTran4, split, inventoryItem);
              prev_tran = inTran4;
            }
            else
            {
              if (inTran4.AssyType == "K" || inTran4.AssyType == "C")
                throw new PXException("Non-Stock Kit Assembly is not allowed.");
              if (this.IsProjectDropShip(inTran4))
              {
                this.WriteGLNonStockCosts(je, inTran4, inventoryItem, site);
              }
              else
              {
                this.UpdateItemSite(InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, inTran4, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task), reasoncode);
                this.AssembleCost(inTran4, split, inventoryItem);
                this.WriteGLNonStockCosts(je, inTran4, inventoryItem, site);
                this.UpdateARTranCost(inTran4);
              }
            }
          }
label_76:
          this.OnTranReleased(inTran4);
        }
        this.UpdateTransitPlans();
        if (((PXSelectBase<INSetup>) this.insetup).Current.ReplanBackOrders.GetValueOrDefault())
          this.ReplanBackOrders();
        PXFormulaAttribute.SetAggregate<INTranCost.qty>(((PXSelectBase) this.intrancost).Cache, (Type) null, (Type) null);
        PXFormulaAttribute.SetAggregate<INTranCost.tranCost>(((PXSelectBase) this.intrancost).Cache, (Type) null, (Type) null);
        PXFormulaAttribute.SetAggregate<INTranCost.tranAmt>(((PXSelectBase) this.intrancost).Cache, (Type) null, (Type) null);
        if (EnumerableExtensions.IsIn<string>(doc.DocType, "I", "A"))
        {
          PXFormulaAttribute.SetAggregate<INTran.tranCost>(((PXSelectBase) this.intranselect).Cache, typeof (SumCalc<PX.Objects.IN.INRegister.totalCost>), (Type) null);
          try
          {
            PXFormulaAttribute.CalcAggregate<INTran.tranCost>(((PXSelectBase) this.intranselect).Cache, (object) doc);
          }
          finally
          {
            PXFormulaAttribute.SetAggregate<INTran.tranCost>(((PXSelectBase) this.intranselect).Cache, (Type) null, (Type) null);
          }
        }
        this.SetOriginalQty();
        this.ReceiveOversold(doc);
        this.ReceiveQty();
        Dictionary<INTranSplit, List<INTranSplit>> dictionary = new Dictionary<INTranSplit, List<INTranSplit>>((IEqualityComparer<INTranSplit>) new INTranSplitCostComparer());
        foreach (INTranSplit key in ((PXSelectBase) this.intransplit).Cache.Updated)
        {
          List<INTranSplit> inTranSplitList;
          if (!dictionary.TryGetValue(key, out inTranSplitList))
            dictionary[key] = inTranSplitList = new List<INTranSplit>();
          inTranSplitList.Add(key);
        }
        foreach (INTranCost trancost in ((PXSelectBase) this.intrancost).Cache.Inserted)
        {
          if (string.Equals(trancost.CostType, "N", StringComparison.OrdinalIgnoreCase))
          {
            INTran inTran = PXParentAttribute.SelectParent<INTran>(((PXSelectBase) this.intrancost).Cache, (object) trancost);
            if (inTran == null)
              throw new RowNotFoundException(((PXSelectBase) this.intranselect).Cache, new object[3]
              {
                (object) trancost.DocType,
                (object) trancost.RefNbr,
                (object) trancost.LineNbr
              });
            this.UpdateAdditionalCost(LandedCostAllocationService.Instance.GetOriginalInTran((PXGraph) this, inTran.POReceiptType, inTran.POReceiptNbr, inTran.POReceiptLineNbr), trancost);
            if (trancost.CostDocType == inTran.DocType && trancost.CostRefNbr == inTran.RefNbr)
            {
              INTranSplit key = new INTranSplit()
              {
                DocType = trancost.DocType,
                RefNbr = trancost.RefNbr,
                LineNbr = trancost.LineNbr,
                CostSiteID = trancost.CostSiteID,
                CostSubItemID = trancost.CostSubItemID,
                ValMethod = trancost.LotSerialNbr != null ? "S" : "A",
                LotSerialNbr = trancost.LotSerialNbr
              };
              List<INTranSplit> inTranSplitList;
              if (dictionary.TryGetValue(key, out inTranSplitList))
              {
                foreach (INTranSplit inTranSplit in inTranSplitList)
                {
                  Decimal? nullable4 = inTranSplit.TotalQty;
                  Decimal? qty = trancost.Qty;
                  inTranSplit.TotalQty = nullable4.HasValue & qty.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + qty.GetValueOrDefault()) : new Decimal?();
                  Decimal? totalCost = inTranSplit.TotalCost;
                  nullable4 = trancost.TranCost;
                  inTranSplit.TotalCost = totalCost.HasValue & nullable4.HasValue ? new Decimal?(totalCost.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                }
              }
            }
            else
            {
              INTranSplitUpdate inTranSplitUpdate1 = ((PXSelectBase<INTranSplitUpdate>) this.intransplitupdate).Insert(new INTranSplitUpdate()
              {
                DocType = trancost.DocType,
                RefNbr = trancost.RefNbr,
                LineNbr = trancost.LineNbr,
                CostSiteID = trancost.CostSiteID,
                CostSubItemID = trancost.CostSubItemID
              });
              INTranSplitUpdate inTranSplitUpdate2 = inTranSplitUpdate1;
              Decimal? totalQty = inTranSplitUpdate2.TotalQty;
              Decimal? qty = trancost.Qty;
              inTranSplitUpdate2.TotalQty = totalQty.HasValue & qty.HasValue ? new Decimal?(totalQty.GetValueOrDefault() + qty.GetValueOrDefault()) : new Decimal?();
              INTranSplitUpdate inTranSplitUpdate3 = inTranSplitUpdate1;
              Decimal? totalCost1 = inTranSplitUpdate3.TotalCost;
              Decimal? totalCost2 = trancost.TotalCost;
              inTranSplitUpdate3.TotalCost = totalCost1.HasValue & totalCost2.HasValue ? new Decimal?(totalCost1.GetValueOrDefault() + totalCost2.GetValueOrDefault()) : new Decimal?();
            }
          }
        }
        foreach (INTranCost inTranCost in ((PXSelectBase) this.intrancost).Cache.Inserted)
        {
          if (string.Equals(inTranCost.CostType, "N", StringComparison.OrdinalIgnoreCase))
          {
            INTran inTran = PXParentAttribute.SelectParent<INTran>(((PXSelectBase) this.intrancost).Cache, (object) inTranCost);
            if (inTran == null)
              throw new RowNotFoundException(((PXSelectBase) this.intranselect).Cache, new object[3]
              {
                (object) inTranCost.DocType,
                (object) inTranCost.RefNbr,
                (object) inTranCost.LineNbr
              });
            if (!(inTranCost.CostDocType == inTran.DocType) || !(inTranCost.CostRefNbr == inTran.RefNbr))
            {
              INTranUpdate inTranUpdate = ((PXSelectBase<INTranUpdate>) this.intranupdate).Insert(new INTranUpdate()
              {
                DocType = inTran.DocType,
                RefNbr = inTranCost.RefNbr,
                LineNbr = inTranCost.LineNbr
              });
              Decimal? tranCost = inTranUpdate.TranCost;
              Decimal? totalCost = inTranCost.TotalCost;
              inTranUpdate.TranCost = tranCost.HasValue & totalCost.HasValue ? new Decimal?(tranCost.GetValueOrDefault() + totalCost.GetValueOrDefault()) : new Decimal?();
            }
          }
        }
        INTran prevTran = (INTran) null;
        Lazy<INPIController> piController = new Lazy<INPIController>((Func<INPIController>) (() => PXGraph.CreateInstance<INPIController>()));
        this.ProcessINTranCosts(GraphHelper.RowCast<INTranCost>(((PXSelectBase) this.intrancost).Cache.Inserted), je, doc, piController, prevTran);
        if (doc.DocType == "A" && !string.IsNullOrEmpty(doc.PIID))
          piController.Value.ReleasePI(doc.PIID);
        foreach (ItemCostHist itemCostHist in ((PXSelectBase) this.itemcosthist).Cache.Inserted)
        {
          if (PX.Objects.IN.INSite.PK.Find((PXGraph) this, itemCostHist.CostSiteID) != null)
          {
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats1 = new PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats();
            itemStats1.InventoryID = itemCostHist.InventoryID;
            itemStats1.SiteID = itemCostHist.CostSiteID;
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats>) this.itemstats).Insert(itemStats1);
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats3 = itemStats2;
            Decimal? nullable5 = itemStats3.QtyOnHand;
            Decimal? nullable6 = itemCostHist.FinYtdQty;
            Decimal? nullable7;
            Decimal? nullable8;
            if (!(nullable5.HasValue & nullable6.HasValue))
            {
              nullable7 = new Decimal?();
              nullable8 = nullable7;
            }
            else
              nullable8 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
            itemStats3.QtyOnHand = nullable8;
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats4 = itemStats2;
            nullable6 = itemStats4.TotalCost;
            nullable5 = itemCostHist.FinYtdCost;
            Decimal? nullable9;
            if (!(nullable6.HasValue & nullable5.HasValue))
            {
              nullable7 = new Decimal?();
              nullable9 = nullable7;
            }
            else
              nullable9 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
            itemStats4.TotalCost = nullable9;
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats5 = itemStats2;
            nullable5 = itemStats5.QtyReceived;
            Decimal? finPtdQtyReceived = itemCostHist.FinPtdQtyReceived;
            Decimal num5 = 0M;
            Decimal? nullable10 = finPtdQtyReceived.GetValueOrDefault() > num5 & finPtdQtyReceived.HasValue ? itemCostHist.FinPtdQtyReceived : new Decimal?(0M);
            Decimal? nullable11 = itemCostHist.FinPtdQtyTransferIn;
            nullable7 = nullable10.HasValue & nullable11.HasValue ? new Decimal?(nullable10.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable12 = itemCostHist.FinPtdQtyAssemblyIn;
            Decimal? nullable13;
            if (!(nullable7.HasValue & nullable12.HasValue))
            {
              nullable11 = new Decimal?();
              nullable13 = nullable11;
            }
            else
              nullable13 = new Decimal?(nullable7.GetValueOrDefault() + nullable12.GetValueOrDefault());
            nullable6 = nullable13;
            Decimal? nullable14;
            if (!(nullable5.HasValue & nullable6.HasValue))
            {
              nullable12 = new Decimal?();
              nullable14 = nullable12;
            }
            else
              nullable14 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
            itemStats5.QtyReceived = nullable14;
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats6 = itemStats2;
            nullable6 = itemStats6.CostReceived;
            Decimal? finPtdCostReceived = itemCostHist.FinPtdCostReceived;
            Decimal num6 = 0M;
            nullable11 = finPtdCostReceived.GetValueOrDefault() > num6 & finPtdCostReceived.HasValue ? itemCostHist.FinPtdCostReceived : new Decimal?(0M);
            nullable10 = itemCostHist.FinPtdCostTransferIn;
            nullable12 = nullable11.HasValue & nullable10.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new Decimal?();
            nullable7 = itemCostHist.FinPtdCostAssemblyIn;
            Decimal? nullable15;
            if (!(nullable12.HasValue & nullable7.HasValue))
            {
              nullable10 = new Decimal?();
              nullable15 = nullable10;
            }
            else
              nullable15 = new Decimal?(nullable12.GetValueOrDefault() + nullable7.GetValueOrDefault());
            nullable5 = nullable15;
            Decimal? nullable16;
            if (!(nullable6.HasValue & nullable5.HasValue))
            {
              nullable7 = new Decimal?();
              nullable16 = nullable7;
            }
            else
              nullable16 = new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault());
            itemStats6.CostReceived = nullable16;
          }
        }
        PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, doc.BranchID);
        if (branch == null)
          throw new RowNotFoundException(((PXGraph) this).Caches[typeof (PX.Objects.GL.Branch)], new object[1]
          {
            (object) doc.BranchID
          });
        foreach (PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats stats in ((PXSelectBase) this.itemstats).Cache.Cached)
        {
          if (((PXSelectBase) this.itemstats).Cache.GetStatus((object) stats) != null)
          {
            bool? isCorrection = doc.IsCorrection;
            bool flag3 = false;
            if (isCorrection.GetValueOrDefault() == flag3 & isCorrection.HasValue)
            {
              if (stats.QtyReceived.GetValueOrDefault() != 0M && stats.CostReceived.GetValueOrDefault() != 0M)
              {
                PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats itemStats = stats;
                Decimal? costReceived = stats.CostReceived;
                Decimal? qtyReceived = stats.QtyReceived;
                Decimal? nullable17 = new Decimal?(PXDBPriceCostAttribute.Round((costReceived.HasValue & qtyReceived.HasValue ? new Decimal?(costReceived.GetValueOrDefault() / qtyReceived.GetValueOrDefault()) : new Decimal?()).Value));
                itemStats.LastCost = nullable17;
                stats.LastCostDate = new DateTime?(INReleaseProcess.GetLastCostTime(((PXSelectBase) this.itemstats).Cache));
              }
              else
                stats.LastCost = new Decimal?(0M);
              stats.MaxCost = stats.LastCost;
              stats.MinCost = stats.LastCost;
            }
            this.UpdateItemCost((INItemStats) stats, branch.BaseCuryID);
          }
        }
        this.PersistJournalEntry(je, doc);
        doc = this.MarkDocumentReleased(doc);
        ((PXGraph) this).Actions.PressSave();
        this.PersistLotSerialAttributes(doc);
        transactionScope.Complete();
      }
    }
  }

  protected virtual INTran ProcessINTranCosts(
    IEnumerable<INTranCost> inTranCosts,
    JournalEntry je,
    PX.Objects.IN.INRegister doc,
    Lazy<INPIController> piController,
    INTran prevTran)
  {
    foreach (INTranCost inTranCost in inTranCosts)
    {
      if (!string.Equals(inTranCost.CostType, "T", StringComparison.OrdinalIgnoreCase))
      {
        INTran inTran1 = PXParentAttribute.SelectParent<INTran>(((PXSelectBase) this.intrancost).Cache, (object) inTranCost);
        if (inTran1 == null)
          throw new RowNotFoundException(((PXSelectBase) this.intranselect).Cache, new object[3]
          {
            (object) inTranCost.DocType,
            (object) inTranCost.RefNbr,
            (object) inTranCost.LineNbr
          });
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inTranCost.InventoryID);
        INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem?.PostClassID) ?? new INPostClass();
        PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, inTran1.SiteID);
        PX.Objects.CS.ReasonCode reasoncode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, inTran1.ReasonCode);
        INLocation location = INLocation.PK.Find((PXGraph) this, inTran1.LocationID);
        PX.Objects.PM.Lite.PMProject project = PX.Objects.PM.Lite.PMProject.PK.Find((PXGraph) this, inTran1.ProjectID);
        PX.Objects.PM.Lite.PMTask task = PX.Objects.PM.Lite.PMTask.PK.Find((PXGraph) this, inTran1.ProjectID, inTran1.TaskID);
        this.UpdateCostHist(inTranCost, inTran1);
        this.UpdateSalesHist(inTranCost, inTran1);
        this.UpdateCustSalesHist(inTranCost, inTran1);
        this.UpdateSiteHistByCostCenterDCost(inTranCost, inTran1);
        short? invtMult1;
        int? nullable1;
        Decimal? nullable2;
        if (!object.Equals((object) prevTran, (object) inTran1))
        {
          if (inTran1.DocType == inTranCost.CostDocType && inTran1.RefNbr == inTranCost.CostRefNbr)
          {
            this.UpdateSalesHistD(inTran1);
            this.UpdateCustSalesStats(inTran1);
            this.WriteGLSales(je, inTran1);
          }
          if (!inTran1.OverrideUnitCost.GetValueOrDefault())
          {
            invtMult1 = inTran1.InvtMult;
            int? nullable3;
            if (!invtMult1.HasValue)
            {
              nullable1 = new int?();
              nullable3 = nullable1;
            }
            else
              nullable3 = new int?((int) invtMult1.GetValueOrDefault());
            nullable1 = nullable3;
            if (nullable1.GetValueOrDefault() != -1)
            {
              short? invtMult2 = inTran1.InvtMult;
              int? nullable4;
              if (!invtMult2.HasValue)
              {
                nullable1 = new int?();
                nullable4 = nullable1;
              }
              else
                nullable4 = new int?((int) invtMult2.GetValueOrDefault());
              nullable1 = nullable4;
              if (nullable1.GetValueOrDefault() == 1)
              {
                nullable1 = inTran1.OrigLineNbr;
                if (!nullable1.HasValue)
                  goto label_21;
              }
              else
                goto label_21;
            }
            nullable2 = inTran1.Qty;
            Decimal num1 = 0M;
            if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
            {
              nullable2 = inTran1.TranCost;
              Decimal num2 = nullable2.Value;
              nullable2 = inTran1.Qty;
              Decimal num3 = nullable2.Value;
              nullable2 = inTran1.UnitCost;
              Decimal num4 = nullable2.Value;
              Decimal num5 = PXCurrencyAttribute.BaseRound((PXGraph) this, num3 * num4);
              if (!(Math.Abs(num2 - num5) > 0.00005M))
                goto label_21;
            }
            else
              goto label_21;
          }
          INTran inTran2 = inTran1;
          nullable2 = inTran1.TranCost;
          Decimal num6 = nullable2.Value;
          nullable2 = inTran1.Qty;
          Decimal num7 = nullable2.Value;
          Decimal? nullable5 = new Decimal?(PXDBPriceCostAttribute.Round(num6 / num7));
          inTran2.UnitCost = nullable5;
          inTran1.OverrideUnitCost = new bool?(true);
        }
label_21:
        this.UpdateARTranCost(inTran1, inTranCost.TranCost);
        this.UpdatePOReceiptLineCost(inTran1, inTranCost, inventoryItem);
        this.UpdateItemStatsLastPurchaseDate(inTran1);
        Decimal? nullable6;
        Decimal? nullable7;
        if (doc.DocType == "A" && !string.IsNullOrEmpty(doc.PIID))
        {
          nullable1 = inTran1.PILineNbr;
          if (nullable1.HasValue)
          {
            INPIController inpiController = piController.Value;
            string piid = doc.PIID;
            nullable1 = inTran1.PILineNbr;
            int piLineNbr = nullable1.Value;
            invtMult1 = inTranCost.InvtMult;
            Decimal? nullable8;
            if (!invtMult1.HasValue)
            {
              nullable6 = new Decimal?();
              nullable8 = nullable6;
            }
            else
              nullable8 = new Decimal?((Decimal) invtMult1.GetValueOrDefault());
            nullable2 = nullable8;
            nullable7 = inTranCost.TranCost;
            Decimal? nullable9;
            if (!(nullable2.HasValue & nullable7.HasValue))
            {
              nullable6 = new Decimal?();
              nullable9 = nullable6;
            }
            else
              nullable9 = new Decimal?(nullable2.GetValueOrDefault() * nullable7.GetValueOrDefault());
            nullable6 = nullable9;
            Decimal valueOrDefault = nullable6.GetValueOrDefault();
            inpiController.AccumulateFinalCost(piid, piLineNbr, valueOrDefault);
          }
        }
        if (inTran1.SOShipmentNbr != null)
        {
          invtMult1 = inTran1.InvtMult;
          nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
          int num8 = 0;
          if (!(nullable1.GetValueOrDefault() == num8 & nullable1.HasValue) && inTran1.DocType == inTranCost.CostDocType && inTran1.RefNbr == inTranCost.CostRefNbr)
          {
            SOShipLineUpdate soShipLineUpdate1 = ((PXSelectBase<SOShipLineUpdate>) this.soshiplineupdate).Insert(new SOShipLineUpdate()
            {
              ShipmentType = inTran1.SOShipmentType,
              ShipmentNbr = inTran1.SOShipmentNbr,
              LineNbr = inTran1.SOShipmentLineNbr
            });
            SOShipLineUpdate soShipLineUpdate2 = soShipLineUpdate1;
            nullable2 = soShipLineUpdate2.ExtCost;
            nullable7 = inTranCost.TranCost;
            Decimal? nullable10;
            if (!(nullable2.HasValue & nullable7.HasValue))
            {
              nullable6 = new Decimal?();
              nullable10 = nullable6;
            }
            else
              nullable10 = new Decimal?(nullable2.GetValueOrDefault() + nullable7.GetValueOrDefault());
            soShipLineUpdate2.ExtCost = nullable10;
            SOShipLineUpdate soShipLineUpdate3 = soShipLineUpdate1;
            nullable7 = soShipLineUpdate1.ExtCost;
            nullable6 = inTran1.Qty;
            Decimal num9 = 0M;
            Decimal? nullable11;
            if (nullable6.GetValueOrDefault() == num9 & nullable6.HasValue)
            {
              nullable6 = new Decimal?();
              nullable11 = nullable6;
            }
            else
              nullable11 = inTran1.Qty;
            nullable2 = nullable11;
            Decimal? nullable12;
            if (!(nullable7.HasValue & nullable2.HasValue))
            {
              nullable6 = new Decimal?();
              nullable12 = nullable6;
            }
            else
              nullable12 = new Decimal?(nullable7.GetValueOrDefault() / nullable2.GetValueOrDefault());
            nullable6 = nullable12;
            Decimal? nullable13 = new Decimal?(PXDBPriceCostAttribute.Round(nullable6.GetValueOrDefault()));
            soShipLineUpdate3.UnitCost = nullable13;
          }
        }
        prevTran = inTran1;
        this.WriteGLCosts(je, inTranCost, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, inTran1, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task), reasoncode, location);
      }
    }
    return prevTran;
  }

  protected virtual void TryToGetProjectAndTask(
    PXResult item,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task)
  {
    project = (PX.Objects.PM.Lite.PMProject) null;
    task = (PX.Objects.PM.Lite.PMTask) null;
  }

  protected virtual void PersistJournalEntry(JournalEntry je, PX.Objects.IN.INRegister doc)
  {
    if (!this.UpdateGL)
      return;
    if (!((PXSelectBase) je.GLTranModuleBatNbr).Cache.IsInsertedUpdatedDeleted)
    {
      int? siteId = doc.SiteID;
      int? toSiteId = doc.ToSiteID;
      if (siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue && !(doc.DocType != "T"))
        return;
    }
    ((PXAction) je.Save).Press();
    doc.BatchNbr = ((PXSelectBase<Batch>) je.BatchModule).Current.BatchNbr;
  }

  protected virtual PX.Objects.IN.INRegister ActualizeAndValidateINRegister(
    PX.Objects.IN.INRegister doc,
    bool releaseFromHold)
  {
    ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Search<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>((object) doc.DocType, (object) doc.RefNbr, Array.Empty<object>()));
    bool? nullable;
    if (releaseFromHold)
    {
      ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.Hold = new bool?(false);
    }
    else
    {
      nullable = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.Hold;
      if (nullable.GetValueOrDefault())
        throw new PXException("Document is On Hold and cannot be released.");
    }
    nullable = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.Released;
    if (nullable.GetValueOrDefault())
      throw new PXException("Document Status is invalid for processing.");
    ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.SrcDocType = doc.SrcDocType;
    ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.SrcRefNbr = doc.SrcRefNbr;
    GraphHelper.MarkUpdated(((PXSelectBase) this.inregister).Cache, (object) ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current, true);
    return ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current;
  }

  protected virtual INTran CreatePositiveOneStepTransferINTran(
    PX.Objects.IN.INRegister doc,
    INTran tran,
    INTranSplit split)
  {
    INTran copy = PXCache<INTran>.CreateCopy(tran);
    copy.OrigDocType = copy.DocType;
    copy.OrigTranType = copy.TranType;
    copy.OrigRefNbr = copy.RefNbr;
    copy.OrigLineNbr = copy.LineNbr;
    if (tran.TranType == "TRX")
    {
      copy.OrigNoteID = doc.NoteID;
      copy.OrigToLocationID = tran.ToLocationID;
      copy.OrigIsLotSerial = new bool?(!string.IsNullOrEmpty(split.LotSerialNbr));
    }
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, tran.ToSiteID);
    copy.BranchID = inSite.BranchID;
    copy.LineNbr = new int?((int) PXLineNbrAttribute.NewLineNbr<INTran.lineNbr>(((PXSelectBase) this.intranselect).Cache, (object) doc));
    copy.InvtMult = new short?((short) 1);
    copy.SiteID = copy.ToSiteID;
    copy.LocationID = copy.ToLocationID;
    copy.ProjectID = copy.ToProjectID;
    copy.TaskID = copy.ToTaskID;
    copy.CostCodeID = copy.ToCostCodeID;
    copy.CostLayerType = copy.ToCostLayerType;
    copy.CostCenterID = copy.ToCostCenterID;
    copy.DestBranchID = new int?();
    copy.ToSiteID = new int?();
    copy.ToLocationID = new int?();
    copy.ToProjectID = new int?();
    copy.ToTaskID = new int?();
    copy.ToCostCodeID = new int?();
    copy.ToCostLayerType = (string) null;
    copy.ToCostCenterID = new int?();
    copy.InvtAcctID = new int?();
    copy.InvtSubID = new int?();
    copy.ARDocType = (string) null;
    copy.ARRefNbr = (string) null;
    copy.ARLineNbr = new int?();
    copy.NoteID = new Guid?();
    return copy;
  }

  public static DateTime GetLastCostTime(PXCache itemstatsCache)
  {
    PXDBLastChangeDateTimeAttribute dateTimeAttribute = itemstatsCache.GetAttributesReadonly<INItemStats.lastCostDate>().OfType<PXDBLastChangeDateTimeAttribute>().FirstOrDefault<PXDBLastChangeDateTimeAttribute>();
    return dateTimeAttribute == null ? DateTime.Now : dateTimeAttribute.GetDate();
  }

  protected virtual void UpdateItemCost(INItemStats stats, string baseCuryID)
  {
    if (stats == null)
      throw new PXArgumentException(nameof (stats), "'{0}' cannot be empty.", new object[1]
      {
        (object) nameof (stats)
      });
    if (baseCuryID == null)
      throw new PXArgumentException(nameof (baseCuryID), "'{0}' cannot be empty.", new object[1]
      {
        (object) nameof (baseCuryID)
      });
    Decimal? nullable1 = stats.LastCost;
    if (!nullable1.HasValue)
      throw new FieldIsEmptyException(((PXSelectBase) this.itemstats).Cache, (object) stats, typeof (INItemStats.lastCost), Array.Empty<object>());
    int? nullable2 = stats.SiteID;
    if (!nullable2.HasValue)
      throw new FieldIsEmptyException(((PXSelectBase) this.itemstats).Cache, (object) stats, typeof (PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStats.siteID), Array.Empty<object>());
    PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost1 = new PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost();
    itemCost1.InventoryID = stats.InventoryID;
    itemCost1.CuryID = baseCuryID;
    PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost>) this.itemcost).Insert(itemCost1);
    PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost3 = itemCost2;
    nullable1 = itemCost3.QtyOnHand;
    Decimal? nullable3 = stats.QtyOnHand;
    itemCost3.QtyOnHand = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost4 = itemCost2;
    nullable3 = itemCost4.TotalCost;
    nullable1 = stats.TotalCost;
    itemCost4.TotalCost = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost5 = itemCost2;
    nullable1 = stats.LastCost;
    Decimal val1 = nullable1.Value;
    nullable1 = itemCost2.MaxCost;
    Decimal valueOrDefault = nullable1.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(Math.Max(val1, valueOrDefault));
    itemCost5.MaxCost = nullable4;
    Decimal? minCost = itemCost2.MinCost;
    nullable1 = new Decimal?();
    Decimal? nullable5 = nullable1;
    Decimal? nullable6 = new Decimal?(0M);
    if (!EnumerableExtensions.IsIn<Decimal?>(minCost, nullable5, nullable6))
    {
      nullable1 = stats.LastCost;
      Decimal num = 0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        nullable1 = stats.LastCost;
        nullable3 = itemCost2.MinCost;
        if (!(nullable1.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue))
          goto label_12;
      }
      else
        goto label_12;
    }
    itemCost2.MinCost = stats.LastCost;
label_12:
    if (!EnumerableExtensions.IsIn<DateTime?>(itemCost2.LastCostDate, new DateTime?(), INItemStats.MinDate.get()))
    {
      DateTime? lastCostDate = stats.LastCostDate;
      DateTime? nullable7 = INItemStats.MinDate.get();
      if ((lastCostDate.HasValue == nullable7.HasValue ? (lastCostDate.HasValue ? (lastCostDate.GetValueOrDefault() != nullable7.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        return;
      nullable2 = itemCost2.LastCostSiteID;
      int? siteId = stats.SiteID;
      if (!(nullable2.GetValueOrDefault() < siteId.GetValueOrDefault() & nullable2.HasValue & siteId.HasValue))
        return;
    }
    itemCost2.LastCostSiteID = stats.SiteID;
    itemCost2.LastCost = stats.LastCost;
    itemCost2.LastCostDate = stats.LastCostDate;
  }

  protected virtual PX.Objects.IN.INRegister MarkDocumentReleased(PX.Objects.IN.INRegister doc)
  {
    PX.Objects.IN.INRegister copy = PXCache<PX.Objects.IN.INRegister>.CreateCopy(doc);
    ((SelectedEntityEvent<PX.Objects.IN.INRegister>) PXEntityEventBase<PX.Objects.IN.INRegister>.Container<PX.Objects.IN.INRegister.Events>.Select((Expression<Func<PX.Objects.IN.INRegister.Events, PXEntityEvent<PX.Objects.IN.INRegister.Events>>>) (e => e.DocumentReleased))).FireOn((PXGraph) this, doc);
    if (!PXCacheEx.GetDifference(((PXSelectBase) this.inregister).Cache, (IBqlTable) copy, (IBqlTable) doc, false).Any<KeyValuePair<string, (object, object)>>())
    {
      doc.Status = "R";
      doc.Released = new bool?(true);
      doc.ReleasedToVerify = new bool?(false);
      doc = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Update(doc);
    }
    return doc;
  }

  public virtual PXSelectBase<INTran> GetMainSelect(PX.Objects.IN.INRegister doc)
  {
    PXSelectJoin<INTran, InnerJoin<PX.Objects.IN.InventoryItem, On<INTran.FK.InventoryItem>, InnerJoin<PX.Objects.IN.INSite, On<INTran.FK.Site>, LeftJoin<INPostClass, On<PX.Objects.IN.InventoryItem.FK.PostClass>, LeftJoin<INLotSerClass, On<PX.Objects.IN.InventoryItem.FK.LotSerialClass>, LeftJoin<INTranSplit, On<INTranSplit.FK.Tran>, LeftJoin<PX.Objects.CS.ReasonCode, On<INTran.FK.ReasonCode>, LeftJoin<INLocation, On<INTranSplit.FK.Location>>>>>>>>, Where<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>>>> mainSelect = new PXSelectJoin<INTran, InnerJoin<PX.Objects.IN.InventoryItem, On<INTran.FK.InventoryItem>, InnerJoin<PX.Objects.IN.INSite, On<INTran.FK.Site>, LeftJoin<INPostClass, On<PX.Objects.IN.InventoryItem.FK.PostClass>, LeftJoin<INLotSerClass, On<PX.Objects.IN.InventoryItem.FK.LotSerialClass>, LeftJoin<INTranSplit, On<INTranSplit.FK.Tran>, LeftJoin<PX.Objects.CS.ReasonCode, On<INTran.FK.ReasonCode>, LeftJoin<INLocation, On<INTranSplit.FK.Location>>>>>>>>, Where<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>>>>((PXGraph) this);
    this.OverrideMainSelectOrderBy(doc, (PXSelectBase<INTran>) mainSelect);
    return (PXSelectBase<INTran>) mainSelect;
  }

  public virtual void OverrideMainSelectOrderBy(PX.Objects.IN.INRegister doc, PXSelectBase<INTran> mainSelect)
  {
    switch (doc.DocType)
    {
      case "I":
      case "T":
        mainSelect.OrderByNew<OrderBy<Asc<INTran.docType, Asc<INTran.refNbr, Desc<INTran.invtMult, Asc<INTran.lineNbr>>>>>>();
        break;
      case "A":
        mainSelect.OrderByNew<OrderBy<Asc<INTran.docType, Asc<INTran.refNbr, Desc<INTranSplit.invtMult, Desc<INTran.tranCost, Asc<INTran.lineNbr>>>>>>>();
        break;
      default:
        mainSelect.OrderByNew<OrderBy<Asc<INTran.docType, Asc<INTran.refNbr, Asc<INTran.invtMult, Asc<INTran.lineNbr>>>>>>();
        break;
    }
  }

  protected virtual void ProcessLinkedAllocation(PX.Objects.IN.INRegister doc)
  {
    if (EnumerableExtensions.IsIn<string>(doc.OrigModule, "IN", "PI"))
      return;
    string poReceiptType = (string) null;
    string poReceiptNbr = (string) null;
    List<PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>> list = new List<PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>>();
    foreach (PXResult<INItemPlan, INTranSplit, INTran, INItemPlanDemand> pxResult in PXSelectBase<INItemPlan, PXSelectJoin<INItemPlan, InnerJoin<INTranSplit, On<INTranSplit.planID, Equal<INItemPlan.supplyPlanID>>, InnerJoin<INTran, On<INTranSplit.FK.Tran>, LeftJoin<INItemPlanDemand, On<INItemPlan.FK.DemandItemPlan>>>>, Where<INTranSplit.docType, Equal<Required<PX.Objects.IN.INRegister.docType>>, And<INTranSplit.refNbr, Equal<Required<PX.Objects.IN.INRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      INTran inTran = PXResult<INItemPlan, INTranSplit, INTran, INItemPlanDemand>.op_Implicit(pxResult);
      INTranSplit inTranSplit = PXResult<INItemPlan, INTranSplit, INTran, INItemPlanDemand>.op_Implicit(pxResult);
      INItemPlan inItemPlan = PXResult<INItemPlan, INTranSplit, INTran, INItemPlanDemand>.op_Implicit(pxResult);
      INPlanType copy = INPlanType.PK.Find((PXGraph) this, inItemPlan.PlanType);
      INLocation inLocation = INLocation.PK.Find((PXGraph) this, inTranSplit.LocationID);
      if (inLocation != null && !inLocation.InclQtyAvail.GetValueOrDefault() && copy.ReplanOnEvent == "61")
      {
        copy = PXCache<INPlanType>.CreateCopy(copy);
        copy.ReplanOnEvent = "60";
      }
      list.Add(new PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>(inItemPlan, PXResult<INItemPlan, INTranSplit, INTran, INItemPlanDemand>.op_Implicit(pxResult), PXResult<INItemPlan, INTranSplit, INTran, INItemPlanDemand>.op_Implicit(pxResult), copy, PXResult<INItemPlan, INTranSplit, INTran, INItemPlanDemand>.op_Implicit(pxResult)));
      if (string.IsNullOrEmpty(poReceiptNbr))
      {
        poReceiptType = inTran.POReceiptType;
        poReceiptNbr = inTran.POReceiptNbr;
      }
    }
    this.ProcessLinkedAllocation(list, poReceiptType, poReceiptNbr);
  }

  protected virtual void ProcessLinkedAllocation(
    List<PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>> list,
    string poReceiptType,
    string poReceiptNbr)
  {
  }

  private void ReattachPlanToTransit(
    INItemPlan plan,
    INTran tran,
    INTranSplit split,
    INItemSite itemsite,
    PX.Objects.IN.INSite site)
  {
    PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) this);
    pxCache.Delete(plan);
    INTransitLine transitLine = this.GetTransitLine(tran);
    INItemPlan copy = PXCache<INItemPlan>.CreateCopy(plan);
    copy.PlanType = "42";
    copy.PlanID = new long?();
    copy.SiteID = tran.ToSiteID;
    copy.RefNoteID = transitLine.NoteID;
    copy.RefEntityType = typeof (INTransitLine).FullName;
    copy.LocationID = tran.ToLocationID ?? itemsite.DfltReceiptLocationID ?? site.ReceiptLocationID;
    copy.CostCenterID = tran.ToCostCenterID;
    copy.DemandPlanID = new long?();
    INItemPlan inItemPlan1 = pxCache.Insert(copy);
    bool flag = transitLine.IsFixedInTransit.GetValueOrDefault();
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) plan.PlanID
    }))
    {
      INItemPlan inItemPlan2 = PXResult<INItemPlan>.op_Implicit(pxResult);
      int num = pxCache.GetStatus(inItemPlan2) == 1 ? 1 : 0;
      INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this, num != 0 ? ((INItemPlan) PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.Find((PXGraph) this, (INItemPlan.planID) inItemPlan2, (PKFindOptions) 0)).PlanType : inItemPlan2.PlanType);
      flag = ((flag ? 1 : 0) | (!(inPlanType.ReplanOnEvent == "95") ? 0 : (inPlanType.PlanType == "93" ? 1 : 0))) != 0;
      if (num == 0)
      {
        inItemPlan2.SupplyPlanID = inItemPlan1.PlanID;
        if (inPlanType.ReplanOnEvent == "95")
        {
          inItemPlan2.PlanType = inPlanType.ReplanOnEvent;
          pxCache.Update(inItemPlan2);
        }
        else
          GraphHelper.MarkUpdated((PXCache) pxCache, (object) inItemPlan2, true);
      }
    }
    if (flag)
    {
      inItemPlan1.PlanType = "44";
      pxCache.Update(inItemPlan1);
      split.IsFixedInTransit = new bool?(true);
      transitLine.IsFixedInTransit = new bool?(true);
      ((PXSelectBase<INTransitLine>) this.intransitline).Update(transitLine);
    }
    split.PlanID = new long?();
    split.ToSiteID = tran.ToSiteID;
    this.UpdateSplitDestinationLocation(tran, split, tran.ToLocationID ?? itemsite.DfltReceiptLocationID ?? site.ReceiptLocationID);
    GraphHelper.MarkUpdated(((PXSelectBase) this.intransplit).Cache, (object) split, true);
  }

  private void UpdateAdditionalCost(INTran ortran, INTranCost trancost)
  {
    if (ortran == null || trancost.TranCost.GetValueOrDefault() == 0M || !(trancost.CostDocType == "A"))
      return;
    Decimal? nullable = trancost.Qty;
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    short? invtMult = trancost.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 1)
      return;
    INTranSplitAdjustmentUpdate adjustmentUpdate = ((PXSelectBase<INTranSplitAdjustmentUpdate>) this.intransplitadjustmentupdate).Insert(new INTranSplitAdjustmentUpdate()
    {
      DocType = ortran.DocType,
      RefNbr = ortran.RefNbr,
      LineNbr = ortran.LineNbr,
      CostSiteID = trancost.CostSiteID,
      LotSerialNbr = trancost.LotSerialNbr,
      CostSubItemID = trancost.CostSubItemID
    });
    nullable = adjustmentUpdate.AdditionalCost;
    Decimal? tranCost = trancost.TranCost;
    adjustmentUpdate.AdditionalCost = nullable.HasValue & tranCost.HasValue ? new Decimal?(nullable.GetValueOrDefault() + tranCost.GetValueOrDefault()) : new Decimal?();
  }

  public virtual INTran Copy(INTran tran, ReadOnlyCostStatus layer, PX.Objects.IN.InventoryItem item)
  {
    INTran inTran = new INTran();
    inTran.BranchID = tran.BranchID;
    inTran.DocType = tran.DocType;
    inTran.RefNbr = tran.RefNbr;
    inTran.TranType = "ADJ";
    inTran.IsStockItem = tran.IsStockItem;
    inTran.InventoryID = tran.InventoryID;
    inTran.SubItemID = tran.SubItemID;
    inTran.SiteID = tran.SiteID;
    inTran.LocationID = tran.LocationID;
    inTran.UOM = tran.UOM;
    inTran.Qty = new Decimal?(0M);
    inTran.AcctID = tran.AcctID;
    inTran.SubID = tran.SubID;
    inTran.COGSAcctID = tran.COGSAcctID;
    inTran.COGSSubID = tran.COGSSubID;
    if (layer != null)
    {
      inTran.InvtAcctID = layer.AccountID;
      inTran.InvtSubID = layer.SubID;
      inTran.OrigRefNbr = item.ValMethod == "F" || item.ValMethod == "S" ? layer.ReceiptNbr : (string) null;
      inTran.LotSerialNbr = item.ValMethod == "S" ? layer.LotSerialNbr : string.Empty;
    }
    else
    {
      inTran.InvtAcctID = new int?();
      inTran.InvtSubID = new int?();
      inTran.OrigRefNbr = (string) null;
      inTran.LotSerialNbr = string.Empty;
    }
    inTran.POReceiptType = tran.POReceiptType;
    inTran.POReceiptNbr = tran.POReceiptNbr;
    inTran.POReceiptLineNbr = tran.POReceiptLineNbr;
    inTran.POLineType = tran.POLineType;
    inTran.ProjectID = tran.ProjectID;
    inTran.TaskID = tran.TaskID;
    inTran.CostCodeID = tran.CostCodeID;
    inTran.CostCenterID = tran.CostCenterID;
    inTran.ReasonCode = tran.ReasonCode;
    inTran.UnitCost = tran.UnitCost;
    inTran.CostLayerType = tran.CostLayerType;
    inTran.ToCostLayerType = tran.ToCostLayerType;
    return inTran;
  }

  public virtual void RegenerateInTranList(
    PXResultset<INTran, INTranSplit, PX.Objects.IN.InventoryItem> originalintranlist)
  {
    foreach (PXResult<INTran, INTranSplit, PX.Objects.IN.InventoryItem> pxResult1 in (PXResultset<INTran>) originalintranlist)
    {
      INTran tran = PXResult<INTran, INTranSplit, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult1);
      INTranSplit inTranSplit = PXResult<INTran, INTranSplit, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult1);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<INTran, INTranSplit, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult1);
      Decimal? nullable1 = new Decimal?(0M);
      Decimal? nullable2 = new Decimal?(0M);
      Decimal? tranCost = tran.TranCost;
      ReadOnlyCostStatus layer1 = (ReadOnlyCostStatus) null;
      INTran originalInTran = LandedCostAllocationService.Instance.GetOriginalInTran((PXGraph) this, tran.POReceiptType, tran.POReceiptNbr, tran.POReceiptLineNbr);
      if (originalInTran != null)
      {
        PXView receiptStatusView = this.GetReceiptStatusView(inventoryItem);
        bool flag = false;
        object[] objArray1 = new object[2]
        {
          (object) tran,
          (object) inTranSplit
        };
        object[] objArray2 = new object[2]
        {
          (object) originalInTran.InvtAcctID,
          (object) originalInTran.InvtSubID
        };
        Decimal? nullable3;
        Decimal? nullable4;
        Decimal? nullable5;
        foreach (PXResult<ReadOnlyCostStatus, ReceiptStatus> pxResult2 in receiptStatusView.SelectMultiBound(objArray1, objArray2))
        {
          ReadOnlyCostStatus layer2 = PXResult<ReadOnlyCostStatus, ReceiptStatus>.op_Implicit(pxResult2);
          ReceiptStatus receiptStatus = PXResult<ReadOnlyCostStatus, ReceiptStatus>.op_Implicit(pxResult2);
          if (!flag)
          {
            if (inventoryItem.ValMethod == "F" && receiptStatus != null && receiptStatus.ReceiptNbr != null)
              flag = true;
            Decimal? nullable6 = new Decimal?();
            Decimal? nullable7 = new Decimal?();
            switch (inventoryItem.ValMethod)
            {
              case "A":
                Decimal? qtyOnHand = layer2.QtyOnHand;
                Decimal num1 = 0M;
                if (qtyOnHand.GetValueOrDefault() == num1 & qtyOnHand.HasValue)
                {
                  qtyOnHand = receiptStatus.QtyOnHand;
                  Decimal num2 = 0M;
                  if (!(qtyOnHand.GetValueOrDefault() == num2 & qtyOnHand.HasValue))
                  {
                    nullable6 = layer2.OrigQty;
                    nullable7 = layer2.QtyOnHand;
                    break;
                  }
                }
                nullable6 = receiptStatus.OrigQty;
                nullable7 = receiptStatus.QtyOnHand;
                break;
              case "S":
                nullable6 = receiptStatus.OrigQty;
                nullable7 = receiptStatus.QtyOnHand;
                break;
              case "F":
                nullable6 = layer2.OrigQty;
                nullable7 = layer2.QtyOnHand;
                break;
            }
            Decimal? nullable8 = nullable7;
            Decimal num3 = 0M;
            if (nullable8.GetValueOrDefault() > num3 & nullable8.HasValue)
              layer1 = layer2;
            int num4;
            if (((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current != null)
            {
              bool? nullable9 = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsPPVTran;
              if (!nullable9.GetValueOrDefault())
              {
                nullable9 = ((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsTaxAdjustmentTran;
                num4 = nullable9.GetValueOrDefault() ? 1 : 0;
              }
              else
                num4 = 1;
            }
            else
              num4 = 0;
            if (num4 != 0)
            {
              nullable8 = tranCost;
              Decimal num5 = 0M;
              if (nullable8.GetValueOrDefault() < num5 & nullable8.HasValue)
              {
                Decimal? totalCost = layer2.TotalCost;
                nullable3 = nullable7;
                Decimal num6 = 0M;
                Decimal? nullable10;
                if (!(nullable3.GetValueOrDefault() == num6 & nullable3.HasValue))
                {
                  Decimal? nullable11 = nullable7;
                  Decimal? nullable12 = tranCost;
                  nullable3 = nullable11.HasValue & nullable12.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * nullable12.GetValueOrDefault()) : new Decimal?();
                  nullable4 = nullable6;
                  if (!(nullable3.HasValue & nullable4.HasValue))
                  {
                    nullable12 = new Decimal?();
                    nullable10 = nullable12;
                  }
                  else
                    nullable10 = new Decimal?(nullable3.GetValueOrDefault() / nullable4.GetValueOrDefault());
                }
                else
                  nullable10 = new Decimal?(0M);
                nullable5 = nullable10;
                Decimal? nullable13;
                if (!(totalCost.HasValue & nullable5.HasValue))
                {
                  nullable4 = new Decimal?();
                  nullable13 = nullable4;
                }
                else
                  nullable13 = new Decimal?(totalCost.GetValueOrDefault() + nullable5.GetValueOrDefault());
                nullable8 = nullable13;
                Decimal num7 = 0M;
                if (nullable8.GetValueOrDefault() < num7 & nullable8.HasValue)
                  break;
              }
            }
            nullable8 = nullable7;
            Decimal num8 = 0M;
            if (!(nullable8.GetValueOrDefault() == num8 & nullable8.HasValue))
            {
              INTran inTran1 = this.Copy(tran, layer2, inventoryItem);
              inTran1.InvtMult = new short?((short) 1);
              nullable8 = nullable6;
              Decimal num9 = 0M;
              Decimal? nullable14;
              if (!(nullable8.GetValueOrDefault() == num9 & nullable8.HasValue))
              {
                nullable8 = nullable7;
                nullable5 = nullable6;
                nullable14 = nullable8.GetValueOrDefault() < nullable5.GetValueOrDefault() & nullable8.HasValue & nullable5.HasValue ? nullable7 : nullable6;
              }
              else
                nullable14 = nullable7;
              INTran inTran2 = inTran1;
              Decimal? nullable15 = nullable14;
              nullable4 = tranCost;
              Decimal? nullable16;
              if (!(nullable15.HasValue & nullable4.HasValue))
              {
                nullable3 = new Decimal?();
                nullable16 = nullable3;
              }
              else
                nullable16 = new Decimal?(nullable15.GetValueOrDefault() * nullable4.GetValueOrDefault());
              nullable5 = nullable16;
              nullable8 = nullable6;
              Decimal? nullable17;
              if (!(nullable5.HasValue & nullable8.HasValue))
              {
                nullable4 = new Decimal?();
                nullable17 = nullable4;
              }
              else
                nullable17 = new Decimal?(nullable5.GetValueOrDefault() / nullable8.GetValueOrDefault());
              nullable4 = nullable17;
              Decimal? nullable18 = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) this, nullable4.Value));
              inTran2.TranCost = nullable18;
              if (inventoryItem.ValMethod == "S")
              {
                INTran inTran3 = inTran1;
                nullable5 = inTran3.TranCost;
                Decimal? nullable19 = nullable2;
                Decimal? nullable20 = nullable14;
                Decimal? nullable21 = nullable19.HasValue & nullable20.HasValue ? new Decimal?(nullable19.GetValueOrDefault() + nullable20.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable22 = tranCost;
                Decimal? nullable23;
                if (!(nullable21.HasValue & nullable22.HasValue))
                {
                  nullable20 = new Decimal?();
                  nullable23 = nullable20;
                }
                else
                  nullable23 = new Decimal?(nullable21.GetValueOrDefault() * nullable22.GetValueOrDefault());
                Decimal? nullable24 = nullable23;
                Decimal? nullable25 = nullable6;
                Decimal? nullable26;
                if (!(nullable24.HasValue & nullable25.HasValue))
                {
                  nullable22 = new Decimal?();
                  nullable26 = nullable22;
                }
                else
                  nullable26 = new Decimal?(nullable24.GetValueOrDefault() / nullable25.GetValueOrDefault());
                nullable15 = nullable26;
                nullable3 = nullable1;
                Decimal? nullable27;
                if (!(nullable15.HasValue & nullable3.HasValue))
                {
                  nullable25 = new Decimal?();
                  nullable27 = nullable25;
                }
                else
                  nullable27 = new Decimal?(nullable15.GetValueOrDefault() - nullable3.GetValueOrDefault());
                nullable8 = nullable27;
                nullable4 = inTran1.TranCost;
                Decimal? nullable28;
                if (!(nullable8.HasValue & nullable4.HasValue))
                {
                  nullable3 = new Decimal?();
                  nullable28 = nullable3;
                }
                else
                  nullable28 = new Decimal?(nullable8.GetValueOrDefault() - nullable4.GetValueOrDefault());
                Decimal num10 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable28);
                Decimal? nullable29;
                if (!nullable5.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable29 = nullable4;
                }
                else
                  nullable29 = new Decimal?(nullable5.GetValueOrDefault() + num10);
                inTran3.TranCost = nullable29;
              }
              nullable5 = nullable1;
              nullable4 = inTran1.TranCost;
              Decimal? nullable30;
              if (!(nullable5.HasValue & nullable4.HasValue))
              {
                nullable8 = new Decimal?();
                nullable30 = nullable8;
              }
              else
                nullable30 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
              nullable1 = nullable30;
              nullable4 = nullable2;
              nullable5 = nullable14;
              Decimal? nullable31;
              if (!(nullable4.HasValue & nullable5.HasValue))
              {
                nullable8 = new Decimal?();
                nullable31 = nullable8;
              }
              else
                nullable31 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
              nullable2 = nullable31;
              // ISSUE: method pointer
              ((PXSelectBase<INTran>) this.intranselect).Insert(inTran1).Call<INTran>(new Action<INTran>((object) this, __methodptr(\u003CRegenerateInTranList\u003Eg__RegenerateRelatedSplit\u007C281_0)));
            }
            nullable5 = nullable7;
            nullable4 = nullable6;
            if (nullable5.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue)
            {
              nullable4 = nullable6;
              Decimal num11 = 0M;
              if (!(nullable4.GetValueOrDefault() == num11 & nullable4.HasValue))
              {
                INTran inTran4 = this.Copy(tran, layer2, inventoryItem);
                inTran4.InvtMult = new short?((short) 0);
                nullable4 = nullable6;
                nullable5 = nullable7;
                Decimal? nullable32;
                if (!(nullable4.HasValue & nullable5.HasValue))
                {
                  nullable8 = new Decimal?();
                  nullable32 = nullable8;
                }
                else
                  nullable32 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
                Decimal? nullable33 = nullable32;
                INTran inTran5 = inTran4;
                nullable8 = nullable33;
                nullable3 = tranCost;
                nullable5 = nullable8.HasValue & nullable3.HasValue ? new Decimal?(nullable8.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
                nullable4 = nullable6;
                Decimal? nullable34;
                if (!(nullable5.HasValue & nullable4.HasValue))
                {
                  nullable3 = new Decimal?();
                  nullable34 = nullable3;
                }
                else
                  nullable34 = new Decimal?(nullable5.GetValueOrDefault() / nullable4.GetValueOrDefault());
                nullable3 = nullable34;
                Decimal? nullable35 = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) this, nullable3.Value));
                inTran5.TranCost = nullable35;
                if (inventoryItem.ValMethod == "S")
                {
                  INTran inTran6 = inTran4;
                  nullable5 = inTran6.TranCost;
                  Decimal? nullable36 = nullable2;
                  Decimal? nullable37 = nullable33;
                  Decimal? nullable38 = nullable36.HasValue & nullable37.HasValue ? new Decimal?(nullable36.GetValueOrDefault() + nullable37.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable39 = tranCost;
                  Decimal? nullable40;
                  if (!(nullable38.HasValue & nullable39.HasValue))
                  {
                    nullable37 = new Decimal?();
                    nullable40 = nullable37;
                  }
                  else
                    nullable40 = new Decimal?(nullable38.GetValueOrDefault() * nullable39.GetValueOrDefault());
                  Decimal? nullable41 = nullable40;
                  Decimal? nullable42 = nullable6;
                  Decimal? nullable43;
                  if (!(nullable41.HasValue & nullable42.HasValue))
                  {
                    nullable39 = new Decimal?();
                    nullable43 = nullable39;
                  }
                  else
                    nullable43 = new Decimal?(nullable41.GetValueOrDefault() / nullable42.GetValueOrDefault());
                  nullable8 = nullable43;
                  Decimal? nullable44 = nullable1;
                  Decimal? nullable45;
                  if (!(nullable8.HasValue & nullable44.HasValue))
                  {
                    nullable42 = new Decimal?();
                    nullable45 = nullable42;
                  }
                  else
                    nullable45 = new Decimal?(nullable8.GetValueOrDefault() - nullable44.GetValueOrDefault());
                  nullable4 = nullable45;
                  nullable3 = inTran4.TranCost;
                  Decimal? nullable46;
                  if (!(nullable4.HasValue & nullable3.HasValue))
                  {
                    nullable44 = new Decimal?();
                    nullable46 = nullable44;
                  }
                  else
                    nullable46 = new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault());
                  Decimal num12 = PXCurrencyAttribute.BaseRound((PXGraph) this, nullable46);
                  Decimal? nullable47;
                  if (!nullable5.HasValue)
                  {
                    nullable3 = new Decimal?();
                    nullable47 = nullable3;
                  }
                  else
                    nullable47 = new Decimal?(nullable5.GetValueOrDefault() + num12);
                  inTran6.TranCost = nullable47;
                }
                nullable5 = nullable1;
                nullable3 = inTran4.TranCost;
                Decimal? nullable48;
                if (!(nullable5.HasValue & nullable3.HasValue))
                {
                  nullable4 = new Decimal?();
                  nullable48 = nullable4;
                }
                else
                  nullable48 = new Decimal?(nullable5.GetValueOrDefault() + nullable3.GetValueOrDefault());
                nullable1 = nullable48;
                nullable3 = nullable2;
                nullable5 = nullable33;
                Decimal? nullable49;
                if (!(nullable3.HasValue & nullable5.HasValue))
                {
                  nullable4 = new Decimal?();
                  nullable49 = nullable4;
                }
                else
                  nullable49 = new Decimal?(nullable3.GetValueOrDefault() + nullable5.GetValueOrDefault());
                nullable2 = nullable49;
                // ISSUE: method pointer
                ((PXSelectBase<INTran>) this.intranselect).Insert(inTran4).Call<INTran>(new Action<INTran>((object) this, __methodptr(\u003CRegenerateInTranList\u003Eg__RegenerateRelatedSplit\u007C281_0)));
              }
            }
          }
          else
            break;
        }
        nullable3 = tranCost;
        nullable4 = nullable1;
        nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
        Decimal num = 0M;
        if (!(nullable5.GetValueOrDefault() == num & nullable5.HasValue))
        {
          INTran inTran7 = this.Copy(tran, layer1, inventoryItem);
          inTran7.InvtMult = new short?((short) 0);
          INTran inTran8 = inTran7;
          nullable5 = tranCost;
          nullable4 = nullable1;
          Decimal? nullable50;
          if (!(nullable5.HasValue & nullable4.HasValue))
          {
            nullable3 = new Decimal?();
            nullable50 = nullable3;
          }
          else
            nullable50 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
          nullable3 = nullable50;
          Decimal? nullable51 = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) this, nullable3.Value));
          inTran8.TranCost = nullable51;
          // ISSUE: method pointer
          ((PXSelectBase<INTran>) this.intranselect).Insert(inTran7).Call<INTran>(new Action<INTran>((object) this, __methodptr(\u003CRegenerateInTranList\u003Eg__RegenerateRelatedSplit\u007C281_0)));
        }
        ((PXSelectBase) this.intranselect).Cache.SetStatus((object) tran, (PXEntryStatus) 3);
      }
    }
  }

  private void SetOriginalQty(INCostStatus layer)
  {
    if (layer.LayerType != "N")
      return;
    if (((PXSelectBase<PX.Objects.IN.INRegister>) this.inregister).Current.IsCorrection.GetValueOrDefault())
    {
      layer.OverrideOrigQty = new bool?(true);
      layer.OrigQtyOnHand = layer.PositiveTranQty;
      if (!(layer.ValMethod != "A"))
        return;
      layer.OrigQty = layer.PositiveTranQty;
    }
    else
    {
      Decimal? qtyOnHand = layer.QtyOnHand;
      Decimal num = 0M;
      if (!(qtyOnHand.GetValueOrDefault() > num & qtyOnHand.HasValue))
        return;
      layer.OrigQtyOnHand = layer.QtyOnHand;
      if (!(layer.ValMethod != "A"))
        return;
      layer.OrigQty = layer.QtyOnHand;
    }
  }

  private void SetOriginalQty()
  {
    foreach (INCostStatus layer in ((PXSelectBase) this.averagecoststatus).Cache.Inserted)
      this.SetOriginalQty(layer);
    foreach (INCostStatus layer in ((PXSelectBase) this.specificcoststatus).Cache.Inserted)
      this.SetOriginalQty(layer);
    foreach (INCostStatus layer in ((PXSelectBase) this.specifictransitcoststatus).Cache.Inserted)
      this.SetOriginalQty(layer);
    foreach (INCostStatus layer in ((PXSelectBase) this.fifocoststatus).Cache.Inserted)
      this.SetOriginalQty(layer);
  }

  public virtual void ReplanBackOrders()
  {
    INReleaseProcess.ReplanBackOrders((PXGraph) this, false);
  }

  public static void ReplanBackOrders(PXGraph graph)
  {
    INReleaseProcess.ReplanBackOrders(graph, true);
  }

  public static void ReplanBackOrders(PXGraph graph, bool ForceReplan)
  {
    List<INItemPlan> inItemPlanList = new List<INItemPlan>();
    foreach (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 in graph.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)].Inserted)
    {
      Decimal? nullable = statusByCostCenter1.QtyOnHand;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() <= num1 & nullable.HasValue)
      {
        nullable = statusByCostCenter1.QtyAvail;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue && !ForceReplan)
          continue;
      }
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.PK.Find(graph, statusByCostCenter1.InventoryID, statusByCostCenter1.SubItemID, statusByCostCenter1.SiteID, statusByCostCenter1.CostCenterID);
      nullable = statusByCostCenter1.QtyHardAvail;
      Decimal num3 = nullable.Value;
      Decimal num4;
      if (statusByCostCenter2 == null)
      {
        num4 = 0M;
      }
      else
      {
        nullable = statusByCostCenter2.QtyHardAvail;
        num4 = nullable.Value;
      }
      Decimal num5 = num3 + num4;
      if (num5 > 0M)
      {
        foreach (PXResult<INItemPlan, PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrderType, PX.Objects.SO.SOLineSplit> pxResult in PXSelectBase<INItemPlan, PXSelectJoin<INItemPlan, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.noteID, Equal<INItemPlan.refNoteID>>, InnerJoin<PX.Objects.SO.SOOrderType, On<PX.Objects.SO.SOOrder.FK.OrderType>, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.planID, Equal<INItemPlan.planID>>>>>, Where<INItemPlan.inventoryID, Equal<Required<INItemPlan.inventoryID>>, And<INItemPlan.subItemID, Equal<Required<INItemPlan.subItemID>>, And<INItemPlan.siteID, Equal<Required<INItemPlan.siteID>>, And<INItemPlan.costCenterID, Equal<Required<INItemPlan.costCenterID>>, And<PX.Objects.SO.SOOrderType.requireAllocation, Equal<False>, And<INItemPlan.planType, In3<INPlanConstants.plan60, INPlanConstants.plan68>>>>>>>, OrderBy<Asc<INItemPlan.planDate, Asc<INItemPlan.planType, Desc<INItemPlan.planQty>>>>>.Config>.Select(graph, new object[4]
        {
          (object) statusByCostCenter1.InventoryID,
          (object) statusByCostCenter1.SubItemID,
          (object) statusByCostCenter1.SiteID,
          (object) statusByCostCenter1.CostCenterID
        }))
        {
          INItemPlan inItemPlan = PXResult<INItemPlan, PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrderType, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
          PX.Objects.SO.SOLineSplit soLineSplit = PXResult<INItemPlan, PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrderType, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
          nullable = soLineSplit.BaseQty;
          Decimal num6 = nullable.Value;
          nullable = soLineSplit.BaseShippedQty;
          Decimal num7 = nullable.Value;
          Decimal num8 = num6 - num7;
          if (num8 <= num5)
          {
            num5 -= num8;
            if (inItemPlan.PlanType == "68")
              inItemPlanList.Add(inItemPlan);
          }
          else if (num5 > 0M)
          {
            if (inItemPlan.PlanType == "68")
            {
              PX.Objects.SO.SOLine soLine = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXSelectJoin<PX.Objects.SO.SOLine, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.SO.SOLineSplit.lineNbr>>>>>, Where<PX.Objects.SO.SOLineSplit.planID, Equal<Required<PX.Objects.SO.SOLineSplit.planID>>>>.Config>.Select(graph, new object[1]
              {
                (object) inItemPlan.PlanID
              }));
              if (soLine != null && soLine.ShipComplete != "C")
              {
                inItemPlanList.Add(inItemPlan);
                num5 = 0M;
              }
            }
            else
              num5 = 0M;
          }
          if (num5 <= 0M)
            break;
        }
      }
    }
    PXCache cach = graph.Caches[typeof (INItemPlan)];
    foreach (INItemPlan inItemPlan in inItemPlanList)
    {
      INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan);
      copy.PlanType = "60";
      cach.Update((object) copy);
    }
  }

  public virtual void ValidateTran(
    PX.Objects.IN.INRegister doc,
    INTran tran,
    INTranSplit split,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site)
  {
    if (!site.Active.GetValueOrDefault())
      throw new PXException("Warehouse '{0}' is inactive", new object[1]
      {
        (object) site.SiteCD
      });
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, split.InventoryID);
    bool? stkItem;
    int num1;
    if (inventoryItem == null)
    {
      num1 = 0;
    }
    else
    {
      stkItem = inventoryItem.StkItem;
      num1 = stkItem.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
    {
      stkItem = item.StkItem;
      if (stkItem.GetValueOrDefault())
      {
        int? inventoryId1 = item.InventoryID;
        int? inventoryId2 = split.InventoryID;
        if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
          throw new PXException("The inventory ID of the {0} item on the Details tab does not match the inventory ID in the Line Details dialog box. The document cannot be released. Please contact your Acumatica support provider.", new object[1]
          {
            (object) item.InventoryCD
          });
      }
    }
    this.ValidateTran(tran);
    if (doc.DocType == "T")
    {
      short? invtMult = tran.InvtMult;
      if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
      {
        int? siteId1 = doc.SiteID;
        int? siteId2 = tran.SiteID;
        if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
        {
          int? toSiteId1 = doc.ToSiteID;
          int? toSiteId2 = tran.ToSiteID;
          if (toSiteId1.GetValueOrDefault() == toSiteId2.GetValueOrDefault() & toSiteId1.HasValue == toSiteId2.HasValue)
          {
            if (split.RefNbr != null)
            {
              int? siteId3 = doc.SiteID;
              int? siteId4 = split.SiteID;
              if (siteId3.GetValueOrDefault() == siteId4.GetValueOrDefault() & siteId3.HasValue == siteId4.HasValue)
              {
                int? toSiteId3 = doc.ToSiteID;
                int? toSiteId4 = split.ToSiteID;
                if (toSiteId3.GetValueOrDefault() == toSiteId4.GetValueOrDefault() & toSiteId3.HasValue == toSiteId4.HasValue)
                  goto label_17;
              }
            }
            else
              goto label_17;
          }
        }
        throw new PXException("The document is corrupted because the warehouse in the document differs from the warehouse in the {0} line. Remove the line and add it again to update the warehouse.", new object[1]
        {
          (object) tran.LineNbr
        });
      }
    }
label_17:
    if (split.RefNbr == null && tran.DocType != "A")
    {
      short? invtMult = tran.InvtMult;
      int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num2 = 0;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        Decimal? qty = tran.Qty;
        Decimal num3 = 0M;
        if (!(qty.GetValueOrDefault() == num3 & qty.HasValue))
        {
          stkItem = item.StkItem;
          if (stkItem.GetValueOrDefault())
            throw new PXException("The system cannot release the document because allocation of the {0} item in the {1} warehouse is not found. Reallocate the item in the document line #{2}.", new object[3]
            {
              ((PXSelectBase) this.intranselect).Cache.GetValueExt<INTran.inventoryID>((object) tran),
              ((PXSelectBase) this.intranselect).Cache.GetValueExt<INTran.siteID>((object) tran),
              ((PXSelectBase) this.intranselect).Cache.GetValueExt<INTran.lineNbr>((object) tran)
            });
        }
      }
    }
    this.ValidateBaseQty(doc, tran, split);
    this.ValidateUnreleasedStandartCostAdjustments(tran, item);
  }

  public virtual void ValidateTran(INTran tran)
  {
    Decimal? unassignedQty = tran.UnassignedQty;
    Decimal num = 0M;
    if (!(unassignedQty.GetValueOrDefault() == num & unassignedQty.HasValue))
      this.RaiseUnassignedQtyNotZeroException(tran);
    this.ValidateMultiplier(tran);
    ConvertedInventoryItemAttribute.ValidateRow(((PXSelectBase) this.intranselect).Cache, (object) tran);
  }

  public virtual void RaiseUnassignedQtyNotZeroException(INTran tran)
  {
    throw new PXException("One or more lines for item '{0}' have unassigned Location and/or Lot/Serial Number", new object[1]
    {
      (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, tran.InventoryID)?.InventoryCD
    });
  }

  protected virtual void ValidateMultiplier(INTran tran)
  {
    string tranType = tran.TranType;
    if (tranType == null || tranType.Length != 3)
      return;
    switch (tranType[2])
    {
      case 'A':
        if (!(tranType == "RCA"))
          return;
        goto label_35;
      case 'B':
        return;
      case 'C':
        if (!(tranType == "ASC") && !(tranType == "NSC"))
          return;
        short? invtMult1 = tran.InvtMult;
        if ((invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1)
          return;
        throw new PXException("The multiplier must be equal to {0}.", new object[1]
        {
          (object) 1
        });
      case 'D':
        if (!(tranType == "UND"))
          return;
        goto label_35;
      case 'I':
        if (!(tranType == "III"))
          return;
        goto label_32;
      case 'J':
        if (!(tranType == "ADJ"))
          return;
        goto label_28;
      case 'K':
        return;
      case 'L':
        return;
      case 'M':
        switch (tranType)
        {
          case "CRM":
            goto label_28;
          case "DRM":
            goto label_32;
          default:
            return;
        }
      case 'P':
        if (!(tranType == "RCP"))
          return;
        goto label_28;
      case 'T':
        if (!(tranType == "RET"))
          return;
        goto label_28;
      case 'U':
        return;
      case 'V':
        if (!(tranType == "INV"))
          return;
        goto label_32;
      case 'W':
        return;
      case 'X':
        if (!(tranType == "TRX"))
          return;
        if (POLineType.IsNonStockNonServiceNonDropShip(tran.POLineType))
        {
          if (!POLineType.IsNonStockNonServiceNonDropShip(tran.POLineType))
            return;
          goto label_35;
        }
        break;
      case 'Y':
        if (!(tranType == "ASY") && !(tranType == "DSY"))
          return;
        break;
      default:
        return;
    }
    if (!EnumerableExtensions.IsNotIn<short?>(tran.InvtMult, new short?((short) -1), new short?((short) 1)))
      return;
    throw new PXException("The multiplier must be equal to {0} or {1}.", new object[2]
    {
      (object) -1,
      (object) 1
    });
label_28:
    if (!EnumerableExtensions.IsNotIn<short?>(tran.InvtMult, new short?((short) 0), new short?((short) 1)))
      return;
    throw new PXException("The multiplier must be equal to {0} or {1}.", new object[2]
    {
      (object) 0,
      (object) 1
    });
label_32:
    if (!EnumerableExtensions.IsNotIn<short?>(tran.InvtMult, new short?((short) -1), new short?((short) 0)))
      return;
    throw new PXException("The multiplier must be equal to {0} or {1}.", new object[2]
    {
      (object) -1,
      (object) 0
    });
label_35:
    short? invtMult2 = tran.InvtMult;
    int? nullable = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      throw new PXException("The multiplier must be equal to {0}.", new object[1]
      {
        (object) 0
      });
  }

  protected virtual void ValidateBaseQty(PX.Objects.IN.INRegister doc, INTran tran, INTranSplit split)
  {
    Decimal? nullable;
    int num1;
    if (split == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = split.Qty;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    PXCache cache = ((PXSelectBase) this.intranselect).Cache;
    INTran Row = tran;
    string uom = split.UOM;
    nullable = split.Qty;
    Decimal num2 = nullable.Value;
    Decimal num3 = INUnitAttribute.ConvertToBase<INTran.inventoryID>(cache, (object) Row, uom, num2, INPrecision.QUANTITY);
    nullable = split.BaseQty;
    Decimal num4 = num3;
    if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
      throw new PXException("The {0} document with the {1} number cannot be released due to incorrect base quantity in the line {2}. You can try to delete the line and create a new one, or you can contact your Acumatica support provider.", new object[3]
      {
        (object) PXStringListAttribute.GetLocalizedLabel<PX.Objects.IN.INRegister.docType>(((PXSelectBase) this.inregister).Cache, (object) doc),
        (object) tran.RefNbr,
        (object) tran.LineNbr
      });
  }

  protected virtual void ValidateFinPeriod(
    PX.Objects.IN.INRegister doc,
    IEnumerable<PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>> records)
  {
    Func<PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>, int?[]> getBranchIDs;
    if (doc.DocType == "T" && doc.TransferType == "1")
    {
      PX.Objects.IN.INSite siteTo = PX.Objects.IN.INSite.PK.Find((PXGraph) this, doc.ToSiteID);
      getBranchIDs = (Func<PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>, int?[]>) (row => new int?[3]
      {
        PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>.op_Implicit(row).BranchID,
        PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>.op_Implicit(row).BranchID,
        siteTo.BranchID
      });
    }
    else
      getBranchIDs = (Func<PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>, int?[]>) (row => new int?[2]
      {
        PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>.op_Implicit(row).BranchID,
        PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>.op_Implicit(row).BranchID
      });
    this.FinPeriodUtils.ValidateMasterFinPeriod<PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>>(records, (Func<PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>, string>) (row => PXResult<INTran, PX.Objects.IN.InventoryItem, PX.Objects.IN.INSite>.op_Implicit(row).TranPeriodID), getBranchIDs, typeof (OrganizationFinPeriod.iNClosed));
  }

  public virtual bool IsProjectDropShip(INTran tran)
  {
    return POLineType.IsProjectDropShip(tran?.POLineType);
  }

  protected virtual bool UseStandardCost(string valMethod, INTran tran) => valMethod == "T";

  protected virtual void PersistLotSerialAttributes(PX.Objects.IN.INRegister doc)
  {
  }

  public string GetCorrectionReasonCode(INTran receiptTran)
  {
    if (receiptTran.ReasonCode != null)
      return receiptTran.ReasonCode;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, receiptTran.InventoryID);
    INPostClass inPostClass = INPostClass.PK.Find((PXGraph) this, inventoryItem?.PostClassID);
    return !string.IsNullOrEmpty(inPostClass?.CorrectionReasonCode) ? inPostClass.CorrectionReasonCode : throw new PXException("The receipt cannot be released because the Purchase Receipt Correction reason code is not specified on the Posting Classes (IN206000) form for the {0} item.", new object[1]
    {
      (object) inventoryItem?.InventoryCD.TrimEnd()
    });
  }

  public class SO2POSync : SO2POSyncFromPOReceiptExtension<INReleaseProcess>
  {
    [PXOverride]
    public void ProcessLinkedAllocation(
      List<PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>> list,
      string poReceiptType,
      string poReceiptNbr,
      Action<List<PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>>, string, string> base_ProcessLinkedAllocation)
    {
      base_ProcessLinkedAllocation(list, poReceiptType, poReceiptNbr);
      List<PXResult<INItemPlan, INPlanType>> poDemands = list?.ConvertAll<PXResult<INItemPlan, INPlanType>>((Converter<PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>, PXResult<INItemPlan, INPlanType>>) (x => new PXResult<INItemPlan, INPlanType>(PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>.op_Implicit(x), PXResult<INItemPlan, INTranSplit, INTran, INPlanType, INItemPlanDemand>.op_Implicit(x))));
      this.Process((poReceiptType, poReceiptNbr), (IEnumerable<PXResult<INItemPlan, INPlanType>>) poDemands);
    }
  }

  public class PXSelectOversold<InventoryID, CostSubItemID, CostSiteID>(PXGraph graph) : 
    PXSelectReadonly2<INTranCost, InnerJoin<INTran, On2<INTranCost.FK.Tran, And<INTran.docType, Equal<INTranCost.costDocType>, And<INTran.refNbr, Equal<INTranCost.costRefNbr>>>>, InnerJoin<ReadOnlyCostStatus, On<ReadOnlyCostStatus.costID, Equal<INTranCost.costID>, And<ReadOnlyCostStatus.layerType, Equal<INLayerType.oversold>>>>>, Where<INTranCost.inventoryID, Equal<Current<InventoryID>>, And<INTranCost.costSubItemID, Equal<Current<CostSubItemID>>, And<INTranCost.costSiteID, Equal<Current<CostSiteID>>, And<INTranCost.isOversold, Equal<True>, And<INTranCost.oversoldQty, Greater<decimal0>>>>>>>(graph)
    where InventoryID : IBqlField
    where CostSubItemID : IBqlField
    where CostSiteID : IBqlField
  {
  }

  public class INHistBucket
  {
    public Decimal SignReceived;
    public Decimal SignIssued;
    public Decimal SignSales;
    public Decimal SignCreditMemos;
    public Decimal SignDropShip;
    public Decimal SignTransferIn;
    public Decimal SignTransferOut;
    public Decimal SignAdjusted;
    public Decimal SignAssemblyIn;
    public Decimal SignAssemblyOut;
    public Decimal SignAMAssemblyIn;
    public Decimal SignAMAssemblyOut;
    public Decimal SignYtd;

    public INHistBucket(INTran tran)
      : this(tran.TranType, tran.InvtMult, new short?((short) Math.Sign(tran.BaseQty.GetValueOrDefault())), tran.OrigModule)
    {
    }

    public INHistBucket(INTranCost costtran, INTran intran)
      : this(costtran.TranType, costtran.InvtMult, new short?((short) Math.Sign(costtran.Qty.GetValueOrDefault())), intran.OrigModule)
    {
      if (!(costtran.TranType == "TRX") && !(costtran.TranType == "ASY") && !(costtran.TranType == "DSY") || !(costtran.CostDocType != intran.DocType) && !(costtran.CostRefNbr != intran.RefNbr))
        return;
      this.SignTransferOut = 0M;
      this.SignSales = 1M;
    }

    public INHistBucket(INTranSplit tran)
      : this(tran.TranType, tran.InvtMult, new short?((short) Math.Sign(tran.BaseQty.GetValueOrDefault())), tran.OrigModule)
    {
    }

    public INHistBucket(string TranType, short? InvtMult, short? qtySign, string origModule)
    {
      this.SignYtd = (Decimal) InvtMult.Value;
      if (TranType != null && TranType.Length == 3)
      {
        switch (TranType[2])
        {
          case 'C':
            if (TranType == "ASC" || TranType == "NSC")
            {
              this.SignAdjusted = 1M;
              return;
            }
            goto label_48;
          case 'I':
            if (TranType == "III")
            {
              switch (origModule)
              {
                case "SO":
                  this.SignSales = 1M;
                  return;
                case "AM":
                  this.SignAssemblyOut = 1M;
                  this.SignAMAssemblyOut = 1M;
                  return;
                case "PO":
                  this.SignReceived = -1M;
                  return;
                default:
                  this.SignIssued = 1M;
                  return;
              }
            }
            else
              goto label_48;
          case 'J':
            if (TranType == "ADJ")
            {
              short? nullable1 = InvtMult;
              Decimal? nullable2 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
              Decimal num1 = 0M;
              if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
              {
                this.SignAdjusted = 1M;
                this.SignSales = 1M;
                return;
              }
              nullable1 = qtySign;
              Decimal? nullable3 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
              Decimal num2 = 0M;
              if (nullable3.GetValueOrDefault() == num2 & nullable3.HasValue)
              {
                this.SignAdjusted = 1M;
                return;
              }
              if (origModule == "AM")
              {
                nullable1 = InvtMult;
                Decimal? nullable4 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
                Decimal num3 = 1M;
                if (nullable4.GetValueOrDefault() == num3 & nullable4.HasValue)
                {
                  this.SignAssemblyIn = 1M;
                  this.SignAMAssemblyIn = 1M;
                  return;
                }
                this.SignAssemblyOut = 1M;
                this.SignAMAssemblyOut = 1M;
                return;
              }
              nullable1 = InvtMult;
              Decimal? nullable5 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
              Decimal num4 = 1M;
              if (nullable5.GetValueOrDefault() == num4 & nullable5.HasValue)
              {
                this.SignReceived = 1M;
                return;
              }
              this.SignIssued = 1M;
              return;
            }
            goto label_48;
          case 'M':
            switch (TranType)
            {
              case "DRM":
                break;
              case "CRM":
                if (this.SignYtd == 0M)
                {
                  this.SignDropShip = -1M;
                  return;
                }
                this.SignCreditMemos = 1M;
                return;
              default:
                goto label_48;
            }
            break;
          case 'P':
            if (TranType == "RCP")
            {
              if (origModule == "AM")
              {
                this.SignAssemblyIn = 1M;
                this.SignAMAssemblyIn = 1M;
                return;
              }
              this.SignReceived = 1M;
              return;
            }
            goto label_48;
          case 'T':
            if (TranType == "RET")
            {
              switch (origModule)
              {
                case "SO":
                  this.SignCreditMemos = 1M;
                  return;
                case "AM":
                  this.SignAssemblyOut = -1M;
                  this.SignAMAssemblyOut = -1M;
                  return;
                case "IN":
                  this.SignReceived = 1M;
                  return;
                default:
                  this.SignIssued = -1M;
                  return;
              }
            }
            else
              goto label_48;
          case 'V':
            if (TranType == "INV")
              break;
            goto label_48;
          case 'X':
            if (TranType == "TRX")
            {
              short? nullable6 = InvtMult;
              Decimal? nullable7 = nullable6.HasValue ? new Decimal?((Decimal) nullable6.GetValueOrDefault()) : new Decimal?();
              Decimal num = 1M;
              if (nullable7.GetValueOrDefault() == num & nullable7.HasValue)
              {
                this.SignTransferIn = 1M;
                return;
              }
              this.SignTransferOut = 1M;
              return;
            }
            goto label_48;
          case 'Y':
            if (TranType == "ASY" || TranType == "DSY")
            {
              short? nullable8 = InvtMult;
              Decimal? nullable9 = nullable8.HasValue ? new Decimal?((Decimal) nullable8.GetValueOrDefault()) : new Decimal?();
              Decimal num = 1M;
              if (nullable9.GetValueOrDefault() == num & nullable9.HasValue)
              {
                this.SignAssemblyIn = 1M;
                return;
              }
              this.SignAssemblyOut = 1M;
              return;
            }
            goto label_48;
          default:
            goto label_48;
        }
        if (this.SignYtd == 0M)
        {
          this.SignDropShip = 1M;
          return;
        }
        this.SignSales = 1M;
        return;
      }
label_48:
      throw new PXException();
    }
  }

  public class GLTranInsertionContext
  {
    public virtual INTranCost TranCost { get; set; }

    public virtual INTran INTran { get; set; }

    public virtual INPostClass PostClass { get; set; }

    public virtual PX.Objects.IN.InventoryItem Item { get; set; }

    public virtual PX.Objects.IN.INSite Site { get; set; }

    public virtual PX.Objects.CS.ReasonCode ReasonCode { get; set; }

    public virtual INLocation Location { get; set; }
  }
}
