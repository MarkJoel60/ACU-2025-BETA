// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReceiptEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.IN;

public class INReceiptEntry : INRegisterEntryBase
{
  private INRegister copy;
  private List<Segment> _SubItemSeg;
  private Dictionary<short?, string> _SubItemSegVal;
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.receipt>>> receipt;
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.receipt>, And<INRegister.refNbr, Equal<Current<INRegister.refNbr>>>>> CurrentDocument;
  [PXImport(typeof (INRegister))]
  public PXSelect<INTran, Where<INTran.docType, Equal<INDocType.receipt>, And<INTran.refNbr, Equal<Current<INRegister.refNbr>>>>> transactions;
  [PXCopyPasteHiddenView]
  public PXSelect<INTranSplit, Where<INTranSplit.docType, Equal<INDocType.receipt>, And<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>>>>> splits;
  public PXSelect<INCostSubItemXRef> costsubitemxref;
  public PXSelect<INItemSite> initemsite;
  public PXAction<INRegister> iNItemLabels;

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Transfer Nbr.")]
  [INReceiptEntry.TransferNbrSelector(typeof (Search2<INRegister.refNbr, InnerJoin<INSite, On<INRegister.FK.ToSite>, InnerJoin<INTransferInTransit, On<INTransferInTransit.transferNbr, Equal<INRegister.refNbr>>, LeftJoin<INTran, On<INTran.origRefNbr, Equal<INTransferInTransit.transferNbr>, And<INTran.released, NotEqual<True>>>>>>, Where<INRegister.docType, Equal<INDocType.transfer>, And<INRegister.released, Equal<boolTrue>, And<INTran.refNbr, IsNull, And<Match<INSite, Current<AccessInfo.userName>>>>>>>))]
  [INReceiptEntry.TransferNbrRestrictor(typeof (Where<INRegister.origModule, Equal<BatchModule.moduleIN>>), "The {0} transfer receipt must be processed by using the Purchase Receipts (PO302000) form.", new Type[] {typeof (INRegister.refNbr)})]
  protected virtual void _(PX.Data.Events.CacheAttached<INRegister.transferNbr> e)
  {
  }

  [PXDefault(typeof (SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<INTran.inventoryID, IBqlInt>.FromCurrent>>), SourceField = typeof (InventoryItem.purchaseUnit), CacheGlobal = true)]
  [INUnit(typeof (INTran.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.uOM> e)
  {
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("RCP")]
  [PXUIField(Enabled = false, Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.tranType> e)
  {
  }

  [PXLookupButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable INItemLabels(PXAdapter adapter)
  {
    if (((PXSelectBase<INRegister>) this.receipt).Current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["RefNbr"] = ((PXSelectBase<INRegister>) this.receipt).Current.RefNbr
      }, "IN619200", (PXBaseRedirectException.WindowMode) 2, "Inventory Item Labels", (CurrentLocalization) null);
    return adapter.Get();
  }

  public INReceiptEntry()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    OpenPeriodAttribute.SetValidatePeriod<INRegister.finPeriodID>(((PXSelectBase) this.receipt).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXUIFieldAttribute.SetVisible<INTran.tranType>(((PXSelectBase) this.transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INTran.tranType>(((PXSelectBase) this.transactions).Cache, (object) null, false);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType>, INRegister, object>) e).NewValue = (object) "R";
  }

  protected override void _(PX.Data.Events.RowUpdated<INRegister> e)
  {
    base._(e);
    bool? requireControlTotal = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    bool flag1 = false;
    if (requireControlTotal.GetValueOrDefault() == flag1 & requireControlTotal.HasValue)
    {
      this.FillControlValue<INRegister.controlCost, INRegister.totalCost>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
      this.FillControlValue<INRegister.controlQty, INRegister.totalQty>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
    }
    else
    {
      if (!((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal.GetValueOrDefault())
        return;
      bool? nullable = e.Row.Hold;
      bool flag2 = false;
      if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
        return;
      nullable = e.Row.Released;
      bool flag3 = false;
      if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
        return;
      this.RaiseControlValueError<INRegister.controlCost, INRegister.totalCost>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
      this.RaiseControlValueError<INRegister.controlQty, INRegister.totalQty>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    INTran inTran = ((PXSelectBase<INTran>) this.transactions).Current ?? PXResultset<INTran>.op_Implicit(((PXSelectBase<INTran>) this.transactions).SelectWindowed(0, 1, Array.Empty<object>()));
    bool flag1 = e.Row.TransferNbr != null;
    bool flag2 = flag1 && e.Row.OrigModule == "PO";
    bool? nullable = e.Row.Released;
    bool flag3 = false;
    bool flag4 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue && e.Row.OrigModule == "IN";
    if (!flag4)
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, false);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowInsert = true;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    nullable = e.Row.Released;
    bool flag5 = false;
    int num1 = nullable.GetValueOrDefault() == flag5 & nullable.HasValue ? 1 : 0;
    cache1.AllowUpdate = num1 != 0;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowDelete = flag4;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowInsert = flag4 && !flag1;
    PXSelectBase<INTran> lsselect = this.LineSplittingExt.lsselect;
    nullable = e.Row.Released;
    bool flag6 = false;
    int num2 = nullable.GetValueOrDefault() == flag6 & nullable.HasValue ? 1 : 0;
    ((PXSelectBase) lsselect).AllowUpdate = num2 != 0;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowDelete = flag4 && !flag1;
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row1 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    int num3 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.controlQty>(cache2, (object) row1, num3 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row2 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    int num4 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.controlCost>(cache3, (object) row2, num4 != 0);
    PXUIFieldAttribute.SetEnabled<INRegister.transferNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowUpdate && inTran == null);
    PXUIFieldAttribute.SetEnabled<INRegister.branchID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowUpdate && !flag1);
    if (((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.Graph.IsImport && this.copy != null && ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.ObjectsEqual<INRegister.transferNbr, INRegister.released>((object) e.Row, (object) this.copy))
      return;
    if (flag1 && !flag2)
      PXUIFieldAttribute.SetEnabled<INTran.qty>(((PXSelectBase) this.transactions).Cache, (object) null, true);
    this.copy = PXCache<INRegister>.CreateCopy(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INRegister, INRegister.transferNbr> e)
  {
    INTran inTran1 = (INTran) null;
    int? nullable1 = new int?();
    INLocationStatusInTransit locationStatusInTransit1 = (INLocationStatusInTransit) null;
    Decimal newtranqty = 0M;
    string transferNbr = e.Row.TransferNbr;
    Decimal newtrancost = 0M;
    this.ParseSubItemSegKeys();
    PXLineNbrAttribute lineNbrAttribute = ((PXSelectBase) this.splits).Cache.GetAttributes<INTranSplit.splitLineNbr>().OfType<PXLineNbrAttribute>().FirstOrDefault<PXLineNbrAttribute>();
    using (new PXReadBranchRestrictedScope())
    {
      foreach (PXResult<INTransitLine, INLocationStatusInTransit, INTransitLineLotSerialStatus, INSite, InventoryItem, INTran> pxResult in PXSelectBase<INTransitLine, PXSelectJoin<INTransitLine, InnerJoin<INLocationStatusInTransit, On<INLocationStatusInTransit.locationID, Equal<INTransitLine.costSiteID>>, LeftJoin<INTransitLineLotSerialStatus, On<INTransitLine.transferNbr, Equal<INTransitLineLotSerialStatus.transferNbr>, And<INTransitLine.transferLineNbr, Equal<INTransitLineLotSerialStatus.transferLineNbr>>>, InnerJoin<INSite, On<INTransitLine.FK.ToSite>, InnerJoin<InventoryItem, On<INLocationStatusInTransit.FK.InventoryItem>, InnerJoin<INTran, On<INTran.docType, Equal<INDocType.transfer>, And<INTran.refNbr, Equal<INTransitLine.transferNbr>, And<INTran.lineNbr, Equal<INTransitLine.transferLineNbr>, And<INTran.invtMult, Equal<shortMinus1>>>>>>>>>>, Where<INTransitLine.transferNbr, Equal<Required<INTransitLine.transferNbr>>>, OrderBy<Asc<INTransitLine.transferNbr, Asc<INTransitLine.transferLineNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) transferNbr
      }))
      {
        INTransitLine inTransitLine = PXResult<INTransitLine, INLocationStatusInTransit, INTransitLineLotSerialStatus, INSite, InventoryItem, INTran>.op_Implicit(pxResult);
        INLocationStatusInTransit locationStatusInTransit2 = PXResult<INTransitLine, INLocationStatusInTransit, INTransitLineLotSerialStatus, INSite, InventoryItem, INTran>.op_Implicit(pxResult);
        INTransitLineLotSerialStatus lineLotSerialStatus = PXResult<INTransitLine, INLocationStatusInTransit, INTransitLineLotSerialStatus, INSite, InventoryItem, INTran>.op_Implicit(pxResult);
        INSite inSite = PXResult<INTransitLine, INLocationStatusInTransit, INTransitLineLotSerialStatus, INSite, InventoryItem, INTran>.op_Implicit(pxResult);
        InventoryItem inventoryItem = PXResult<INTransitLine, INLocationStatusInTransit, INTransitLineLotSerialStatus, INSite, InventoryItem, INTran>.op_Implicit(pxResult);
        INTran inTran2 = PXResult<INTransitLine, INLocationStatusInTransit, INTransitLineLotSerialStatus, INSite, InventoryItem, INTran>.op_Implicit(pxResult);
        Decimal? nullable2 = locationStatusInTransit2.QtyOnHand;
        Decimal num1 = 0M;
        if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
        {
          if (lineLotSerialStatus != null)
          {
            nullable2 = lineLotSerialStatus.QtyOnHand;
            Decimal num2 = 0M;
            if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
              continue;
          }
          int? nullable3 = nullable1;
          int? nullable4 = inTransitLine.TransferLineNbr;
          if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
          {
            this.UpdateTranCostQty(inTran1, newtranqty, newtrancost);
            newtrancost = 0M;
            newtranqty = 0M;
            if (!object.Equals((object) ((PXSelectBase<INRegister>) this.receipt).Current.BranchID, (object) inSite.BranchID))
            {
              INRegister copy = PXCache<INRegister>.CreateCopy(((PXSelectBase<INRegister>) this.receipt).Current);
              copy.BranchID = inSite.BranchID;
              ((PXSelectBase<INRegister>) this.receipt).Update(copy);
            }
            inTran1 = PXCache<INTran>.CreateCopy(inTran2);
            inTran1.OrigDocType = inTran1.DocType;
            inTran1.OrigTranType = inTran1.TranType;
            inTran1.OrigRefNbr = inTransitLine.TransferNbr;
            inTran1.OrigLineNbr = inTransitLine.TransferLineNbr;
            if (inTran2.TranType == "TRX")
            {
              inTran1.OrigNoteID = e.Row.NoteID;
              inTran1.OrigToLocationID = inTran2.ToLocationID;
              INTranSplit inTranSplit = PXResultset<INTranSplit>.op_Implicit(PXSelectBase<INTranSplit, PXSelectReadonly<INTranSplit, Where<INTranSplit.docType, Equal<Current<INTran.docType>>, And<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
              {
                (object) inTran2
              }, Array.Empty<object>()));
              inTran1.OrigIsLotSerial = new bool?(!string.IsNullOrEmpty(inTranSplit?.LotSerialNbr));
            }
            inTran1.BranchID = inSite.BranchID;
            inTran1.DocType = e.Row.DocType;
            inTran1.RefNbr = e.Row.RefNbr;
            inTran1.LineNbr = new int?((int) PXLineNbrAttribute.NewLineNbr<INTran.lineNbr>(((PXSelectBase) this.transactions).Cache, (object) e.Row));
            inTran1.InvtMult = new short?((short) 1);
            inTran1.SiteID = inTransitLine.ToSiteID;
            inTran1.LocationID = inTransitLine.ToLocationID;
            INTran inTran3 = inTran1;
            nullable4 = new int?();
            int? nullable5 = nullable4;
            inTran3.ToSiteID = nullable5;
            INTran inTran4 = inTran1;
            nullable4 = new int?();
            int? nullable6 = nullable4;
            inTran4.ToLocationID = nullable6;
            inTran1.BaseQty = new Decimal?(0M);
            inTran1.Qty = new Decimal?(0M);
            inTran1.UnitCost = new Decimal?(0M);
            inTran1.Released = new bool?(false);
            INTran inTran5 = inTran1;
            nullable4 = new int?();
            int? nullable7 = nullable4;
            inTran5.InvtAcctID = nullable7;
            INTran inTran6 = inTran1;
            nullable4 = new int?();
            int? nullable8 = nullable4;
            inTran6.InvtSubID = nullable8;
            inTran1.ReasonCode = (string) null;
            inTran1.ARDocType = (string) null;
            inTran1.ARRefNbr = (string) null;
            INTran inTran7 = inTran1;
            nullable4 = new int?();
            int? nullable9 = nullable4;
            inTran7.ARLineNbr = nullable9;
            INTran inTran8 = inTran1;
            nullable4 = new int?();
            int? nullable10 = nullable4;
            inTran8.ProjectID = nullable10;
            INTran inTran9 = inTran1;
            nullable4 = new int?();
            int? nullable11 = nullable4;
            inTran9.TaskID = nullable11;
            INTran inTran10 = inTran1;
            nullable4 = new int?();
            int? nullable12 = nullable4;
            inTran10.CostCodeID = nullable12;
            inTran1.TranCost = new Decimal?(0M);
            inTran1.NoteID = new Guid?();
            inTran1.ToCostLayerType = (string) null;
            INTran inTran11 = inTran1;
            nullable4 = new int?();
            int? nullable13 = nullable4;
            inTran11.ToSpecialOrderCostCenterID = nullable13;
            INTran inTran12 = inTran1;
            nullable4 = new int?();
            int? nullable14 = nullable4;
            inTran12.ToCostCenterID = nullable14;
            INTran inTran13 = inTran1;
            nullable4 = new int?();
            int? nullable15 = nullable4;
            inTran13.ToProjectID = nullable15;
            INTran inTran14 = inTran1;
            nullable4 = new int?();
            int? nullable16 = nullable4;
            inTran14.ToTaskID = nullable16;
            inTran1.CostCenterID = inTran2.ToCostCenterID;
            inTran1.CostLayerType = inTran2.ToCostLayerType;
            inTran1.InventorySource = inTran2.ToInventorySource;
            inTran1.SpecialOrderCostCenterID = inTran2.ToSpecialOrderCostCenterID;
            inTran1.ProjectID = inTran2.ToProjectID;
            inTran1.TaskID = inTran2.ToTaskID;
            ((PXSelectBase<INTranSplit>) this.splits).Current = (INTranSplit) null;
            inTran1 = ((PXSelectBase<INTran>) this.transactions).Insert(inTran1);
            ((PXSelectBase<INTran>) this.transactions).Current = inTran1;
            if (((PXSelectBase<INTranSplit>) this.splits).Current != null)
              ((PXSelectBase<INTranSplit>) this.splits).Delete(((PXSelectBase<INTranSplit>) this.splits).Current);
            nullable1 = inTransitLine.TransferLineNbr;
          }
          if (!((PXGraph) this).Caches[typeof (INLocationStatusInTransit)].ObjectsEqual((object) locationStatusInTransit1, (object) locationStatusInTransit2))
          {
            Decimal num3 = newtranqty;
            nullable2 = locationStatusInTransit2.QtyOnHand;
            Decimal num4 = nullable2.Value;
            newtranqty = num3 + num4;
            locationStatusInTransit1 = locationStatusInTransit2;
          }
          nullable2 = lineLotSerialStatus.QtyOnHand;
          INTranSplit inTranSplit1;
          Decimal num5;
          if (!nullable2.HasValue)
          {
            inTranSplit1 = new INTranSplit()
            {
              InventoryID = locationStatusInTransit2.InventoryID,
              IsStockItem = new bool?(true),
              FromSiteID = inTransitLine.SiteID,
              SubItemID = locationStatusInTransit2.SubItemID,
              LotSerialNbr = (string) null
            };
            nullable2 = locationStatusInTransit2.QtyOnHand;
            num5 = nullable2.Value;
          }
          else
          {
            inTranSplit1 = new INTranSplit()
            {
              InventoryID = lineLotSerialStatus.InventoryID,
              IsStockItem = new bool?(true),
              FromSiteID = lineLotSerialStatus.FromSiteID,
              SubItemID = lineLotSerialStatus.SubItemID,
              LotSerialNbr = lineLotSerialStatus.LotSerialNbr
            };
            nullable2 = lineLotSerialStatus.QtyOnHand;
            num5 = nullable2.Value;
          }
          inTranSplit1.DocType = e.Row.DocType;
          inTranSplit1.RefNbr = e.Row.RefNbr;
          inTranSplit1.LineNbr = inTran1.LineNbr;
          inTranSplit1.SplitLineNbr = new int?((int) PXLineNbrAttribute.NewLineNbr<INTranSplit.splitLineNbr>(((PXSelectBase) this.splits).Cache, (object) e.Row));
          inTranSplit1.UnitCost = new Decimal?(0M);
          inTranSplit1.InvtMult = new short?((short) 1);
          inTranSplit1.SiteID = inTransitLine.ToSiteID;
          INTranSplit inTranSplit2 = inTranSplit1;
          nullable4 = lineLotSerialStatus.ToLocationID;
          int? nullable17 = nullable4 ?? inTransitLine.ToLocationID;
          inTranSplit2.LocationID = nullable17;
          inTranSplit1.PlanID = new long?();
          inTranSplit1.Released = new bool?(false);
          INTranSplit inTranSplit3 = inTranSplit1;
          nullable4 = new int?();
          int? nullable18 = nullable4;
          inTranSplit3.ProjectID = nullable18;
          INTranSplit inTranSplit4 = inTranSplit1;
          nullable4 = new int?();
          int? nullable19 = nullable4;
          inTranSplit4.TaskID = nullable19;
          INTranSplit split = ((PXSelectBase<INTranSplit>) this.splits).Insert(inTranSplit1);
          this.UpdateCostSubItemID(split, inventoryItem);
          split.MaxTransferBaseQty = new Decimal?(num5);
          split.BaseQty = new Decimal?(num5);
          INTranSplit inTranSplit5 = split;
          nullable2 = split.BaseQty;
          Decimal? nullable20 = new Decimal?(nullable2.Value);
          inTranSplit5.Qty = nullable20;
          this.SetCostAttributes(inTran1, split, inventoryItem, transferNbr);
          Decimal num6 = newtrancost;
          nullable2 = split.BaseQty;
          Decimal num7 = nullable2.Value;
          nullable2 = split.UnitCost;
          Decimal num8 = nullable2.Value;
          Decimal num9 = num7 * num8;
          newtrancost = num6 + num9;
          split.UnitCost = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, split.UnitCost));
          ((PXSelectBase<INTranSplit>) this.splits).Update(split);
          lineNbrAttribute?.ClearLastDefaultValue();
        }
      }
    }
    this.UpdateTranCostQty(inTran1, newtranqty, newtrancost);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.docType>, INTran, object>) e).NewValue = (object) "R";
  }

  protected override void _(PX.Data.Events.RowInserted<INTran> e)
  {
    base._(e);
    if (e.Row == null || !EnumerableExtensions.IsIn<string>(e.Row.OrigModule, "SO", "PO"))
      return;
    this.OnForeignTranInsert(e.Row);
  }

  protected override void _(PX.Data.Events.RowPersisting<INTran> e)
  {
    base._(e);
    Decimal? nullable1;
    if (PXDBOperationExt.Command(e.Operation) == 1 && !string.IsNullOrEmpty(e.Row.POReceiptNbr))
    {
      Decimal? qty1 = e.Row.Qty;
      Decimal? nullable2 = e.Row.OrigQty;
      if (PXDBQuantityAttribute.Round(new Decimal?((qty1.HasValue & nullable2.HasValue ? new Decimal?(qty1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?()).Value)) > 0M)
      {
        PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache;
        INTran row = e.Row;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> qty2 = (ValueType) e.Row.Qty;
        object[] objArray = new object[1];
        nullable1 = e.Row.OrigQty;
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(-nullable1.GetValueOrDefault());
        objArray[0] = (object) nullable3;
        PXSetPropertyException propertyException = new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
        cache.RaiseExceptionHandling<INTran.qty>((object) row, (object) qty2, (Exception) propertyException);
      }
      else
      {
        Decimal? qty3 = e.Row.Qty;
        nullable2 = e.Row.OrigQty;
        if (PXDBQuantityAttribute.Round(new Decimal?((qty3.HasValue & nullable2.HasValue ? new Decimal?(qty3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?()).Value)) < 0M)
        {
          PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache;
          INTran row = e.Row;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> qty4 = (ValueType) e.Row.Qty;
          object[] objArray = new object[1];
          nullable1 = e.Row.OrigQty;
          Decimal? nullable4;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new Decimal?(-nullable1.GetValueOrDefault());
          objArray[0] = (object) nullable4;
          PXSetPropertyException propertyException = new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", objArray);
          cache.RaiseExceptionHandling<INTran.qty>((object) row, (object) qty4, (Exception) propertyException);
        }
      }
    }
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    nullable1 = e.Row.Qty;
    Decimal num1 = 0M;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
      return;
    nullable1 = e.Row.TranCost;
    Decimal num2 = 0M;
    if (!(nullable1.GetValueOrDefault() > num2 & nullable1.HasValue))
      return;
    if (e.Row.POReceiptNbr != null && e.Row.POReceiptLineNbr.HasValue && e.Row.POReceiptType != null)
    {
      PX.Objects.PO.POReceiptLine entity = new PX.Objects.PO.POReceiptLine()
      {
        ReceiptType = e.Row.POReceiptType,
        LineNbr = e.Row.POReceiptLineNbr,
        ReceiptNbr = e.Row.POReceiptNbr
      };
      throw new ErrorProcessingEntityException(((PXGraph) this).Caches[((object) entity).GetType()], (IBqlTable) entity, "Quantity cannot be zero when Ext. Cost is nonzero.");
    }
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache.RaiseExceptionHandling<INTran.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("Quantity cannot be zero when Ext. Cost is nonzero."));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.baseQty> e)
  {
    this.INTranSplitBaseQtyFieldVerifying(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.baseQty>>) e).Cache, INTran.FromINTranSplit(e.Row), (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.baseQty>, INTranSplit, object>) e).NewValue);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INTranSplit> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    this.INTranSplitBaseQtyFieldVerifying(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTranSplit>>) e).Cache, INTran.FromINTranSplit(e.Row), e.Row.BaseQty);
  }

  public virtual void INTranSplitBaseQtyFieldVerifying(PXCache cache, INTran row, Decimal? value)
  {
    if ((((PXSelectBase<INRegister>) this.CurrentDocument).Current == null ? 0 : (((PXSelectBase<INRegister>) this.CurrentDocument).Current.TransferNbr != null ? 1 : 0)) == 0)
      return;
    Decimal? nullable = value;
    Decimal? maxTransferBaseQty = row.MaxTransferBaseQty;
    if (!(nullable.GetValueOrDefault() > maxTransferBaseQty.GetValueOrDefault() & nullable.HasValue & maxTransferBaseQty.HasValue))
      return;
    maxTransferBaseQty = row.MaxTransferBaseQty;
    if (!maxTransferBaseQty.HasValue)
      return;
    PXCache pxCache = cache;
    INTran inTran = row;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) value;
    object[] objArray = new object[1];
    PXCache sender = cache;
    INTran Row = row;
    maxTransferBaseQty = row.MaxTransferBaseQty;
    Decimal num = maxTransferBaseQty.Value;
    objArray[0] = (object) INUnitAttribute.ConvertFromBase<INTranSplit.inventoryID, INTranSplit.uOM>(sender, (object) Row, num, INPrecision.QUANTITY);
    PXSetPropertyException<INTran.qty> propertyException = new PXSetPropertyException<INTran.qty>("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
    pxCache.RaiseExceptionHandling<INTranSplit.qty>((object) inTran, (object) local, (Exception) propertyException);
  }

  public virtual void UpdateTranCostQty(INTran newtran, Decimal newtranqty, Decimal newtrancost)
  {
    using (this.LineSplittingExt.SuppressedModeScope(true))
    {
      if (newtran == null)
        return;
      newtran.BaseQty = new Decimal?(newtranqty);
      newtran.Qty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.transactions).Cache, newtran.InventoryID, newtran.UOM, newtran.BaseQty.Value, INPrecision.QUANTITY));
      newtran.MaxTransferBaseQty = new Decimal?(newtranqty);
      INTran inTran = newtran;
      Decimal num = newtrancost;
      Decimal? qty = newtran.Qty;
      Decimal? nullable = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, qty.HasValue ? new Decimal?(num / qty.GetValueOrDefault()) : new Decimal?()));
      inTran.UnitCost = nullable;
      newtran.TranCost = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, newtrancost));
      ((PXSelectBase<INTran>) this.transactions).Update(newtran);
    }
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
    StringBuilder stringBuilder1 = new StringBuilder();
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
        stringBuilder1.Append(str2);
      }
      else
      {
        if (!this._SubItemSegVal.TryGetValue(segment.SegmentID, out str2))
          throw new PXException("Subitem Segmented Key missing one or more Consolidated values.");
        StringBuilder stringBuilder2 = stringBuilder1;
        string str3 = str2;
        length1 = segment.Length;
        int valueOrDefault = (int) length1.GetValueOrDefault();
        string str4 = str3.PadRight(valueOrDefault);
        stringBuilder2.Append(str4);
      }
      int num2 = num1;
      length1 = segment.Length;
      int num3 = (int) length1.Value;
      num1 = num2 + num3;
    }
    return stringBuilder1.ToString();
  }

  public object GetValueExt<Field>(PXCache cache, object data) where Field : class, IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }

  public virtual void UpdateCostSubItemID(INTranSplit split, InventoryItem item)
  {
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
  }

  public int? INTransitSiteID
  {
    get
    {
      if (!((PXSelectBase<INSetup>) this.insetup).Current.TransitSiteID.HasValue)
        throw new PXException("Please fill transite site id in inventory preferences.");
      return ((PXSelectBase<INSetup>) this.insetup).Current.TransitSiteID;
    }
  }

  public virtual PXView GetCostStatusCommand(
    INTranSplit split,
    InventoryItem item,
    string transferNbr,
    out object[] parameters)
  {
    int? inTransitSiteId = this.INTransitSiteID;
    BqlCommand bqlCommand;
    switch (item.ValMethod)
    {
      case "T":
      case "A":
      case "F":
        bqlCommand = (BqlCommand) new Select<INCostStatus, Where<INCostStatus.inventoryID, Equal<Required<INCostStatus.inventoryID>>, And<INCostStatus.costSiteID, Equal<Required<INCostStatus.costSiteID>>, And<INCostStatus.costSubItemID, Equal<Required<INCostStatus.costSubItemID>>, And<INCostStatus.layerType, Equal<INLayerType.normal>, And<INCostStatus.receiptNbr, Equal<Required<INCostStatus.receiptNbr>>>>>>>, OrderBy<Asc<INCostStatus.receiptDate, Asc<INCostStatus.receiptNbr>>>>();
        parameters = new object[4]
        {
          (object) split.InventoryID,
          (object) inTransitSiteId,
          (object) split.CostSubItemID,
          (object) transferNbr
        };
        break;
      case "S":
        bqlCommand = (BqlCommand) new Select<INCostStatus, Where<INCostStatus.inventoryID, Equal<Required<INCostStatus.inventoryID>>, And<INCostStatus.costSiteID, Equal<Required<INCostStatus.costSiteID>>, And<INCostStatus.costSubItemID, Equal<Required<INCostStatus.costSubItemID>>, And<INCostStatus.lotSerialNbr, Equal<Required<INCostStatus.lotSerialNbr>>, And<INCostStatus.layerType, Equal<INLayerType.normal>, And<INCostStatus.receiptNbr, Equal<Required<INCostStatus.receiptNbr>>>>>>>>>();
        parameters = new object[5]
        {
          (object) split.InventoryID,
          (object) inTransitSiteId,
          (object) split.CostSubItemID,
          (object) split.LotSerialNbr,
          (object) transferNbr
        };
        break;
      default:
        throw new PXException();
    }
    return new PXView((PXGraph) this, false, bqlCommand);
  }

  public virtual void SetCostAttributes(
    INTran tran,
    INTranSplit split,
    InventoryItem item,
    string transferNbr)
  {
    Decimal? baseQty = split.BaseQty;
    Decimal num = 0M;
    if (baseQty.GetValueOrDefault() == num & baseQty.HasValue || !split.BaseQty.HasValue)
      return;
    object[] parameters;
    INCostStatus inCostStatus = (INCostStatus) this.GetCostStatusCommand(split, item, transferNbr, out parameters).SelectSingle(parameters);
    tran.AcctID = inCostStatus.AccountID;
    tran.SubID = inCostStatus.SubID;
    split.UnitCost = new Decimal?(inCostStatus.TotalCost.Value / inCostStatus.QtyOnHand.Value);
  }

  protected virtual bool IsPMVisible
  {
    get
    {
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      return pmSetup != null && pmSetup.IsActive.GetValueOrDefault() && pmSetup.VisibleInIN.GetValueOrDefault();
    }
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    ((PXGraph) this).CopyPasteGetScript(isImportSimple, script, containers);
    INRegister inRegister = INRegister.PK.Find((PXGraph) this, "T", ((PXSelectBase<INRegister>) this.receipt).Current?.TransferNbr);
    if (inRegister == null || inRegister.OrigModule == null || !(inRegister.OrigModule != "IN"))
      return;
    int index = script.FindIndex((Predicate<Command>) (c => c.ObjectName == "receipt" && c.FieldName == "TransferNbr"));
    if (index < 0)
      return;
    script.RemoveAt(index);
    containers.RemoveAt(index);
  }

  public override PXSelectBase<INRegister> INRegisterDataMember
  {
    get => (PXSelectBase<INRegister>) this.receipt;
  }

  public override PXSelectBase<INTran> INTranDataMember => (PXSelectBase<INTran>) this.transactions;

  public override PXSelectBase<INTran> LSSelectDataMember => this.LineSplittingExt.lsselect;

  public override PXSelectBase<INTranSplit> INTranSplitDataMember
  {
    get => (PXSelectBase<INTranSplit>) this.splits;
  }

  protected override string ScreenID => "IN301000";

  public INReceiptEntry.LineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<INReceiptEntry.LineSplittingExtension>();
  }

  public class SiteStatusLookup : INSiteStatusLookupExt<INReceiptEntry>
  {
    protected override bool IsAddItemEnabled(INRegister doc)
    {
      return ((PXSelectBase) this.LSSelect).AllowDelete;
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<INSiteStatusFilter, INSiteStatusFilter.onlyAvailable> args)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INSiteStatusFilter, INSiteStatusFilter.onlyAvailable>, INSiteStatusFilter, object>) args).NewValue = (object) false;
    }
  }

  public class LineSplittingExtension : INRegisterLineSplittingExtension<INReceiptEntry>
  {
  }

  public class ItemAvailabilityExtension : INRegisterItemAvailabilityExtension<INReceiptEntry>
  {
  }

  public class TransferNbrSelectorAttribute : PXSelectorAttribute
  {
    protected BqlCommand _RestrictedSelect;
    protected PXView _outerview;

    public TransferNbrSelectorAttribute(Type searchType)
      : base(searchType)
    {
      this._RestrictedSelect = BqlCommand.CreateInstance(new Type[1]
      {
        typeof (Search2<INRegister.refNbr, InnerJoin<INSite, On<INRegister.FK.ToSite>>, Where<MatchWithBranch<INSite.branchID>>>)
      });
    }

    public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      INSite inSite = (INSite) null;
      using (new PXReadBranchRestrictedScope())
      {
        base.FieldVerifying(sender, e);
        INRegister inRegister = INRegister.PK.Find(sender.Graph, "T", e.NewValue as string);
        if (inRegister != null)
          inSite = INSite.PK.Find(sender.Graph, inRegister.ToSiteID);
      }
      if (inSite != null && !this._RestrictedSelect.Meet(sender.Graph.Caches[typeof (INSite)], (object) inSite, Array.Empty<object>()))
        throw new PXSetPropertyException("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
        {
          (object) ((PXEventSubscriberAttribute) this)._FieldName
        });
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this._outerview = new PXView(sender.Graph, true, this._Select);
      // ISSUE: method pointer
      PXView pxView = sender.Graph.Views[this._ViewName] = new PXView(sender.Graph, true, this._Select, (Delegate) new PXSelectDelegate((object) this, __methodptr(\u003CCacheAttached\u003Eb__4_0)));
      if (!this._DirtyRead)
        return;
      pxView.IsReadOnly = false;
    }

    public virtual BqlCommand WhereAnd(PXCache sender, Type whr)
    {
      if (this._outerview == null)
        return base.WhereAnd(sender, whr);
      this._outerview.WhereAnd(whr);
      return this._outerview.BqlSelect;
    }
  }

  public class TransferNbrRestrictorAttribute(Type where, string message, params Type[] pars) : 
    PXRestrictorAttribute(where, message, pars)
  {
    public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      using (new PXReadBranchRestrictedScope())
        base.FieldVerifying(sender, e);
    }
  }
}
