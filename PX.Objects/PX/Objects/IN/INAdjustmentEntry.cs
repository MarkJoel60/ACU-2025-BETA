// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAdjustmentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.Services;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INAdjustmentEntry : INRegisterEntryBase
{
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.adjustment>>> adjustment;
  [PXCopyPasteHiddenFields(new Type[] {typeof (INRegister.ignoreAllocationErrors)})]
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.adjustment>, And<INRegister.refNbr, Equal<Current<INRegister.refNbr>>>>> CurrentDocument;
  [PXImport(typeof (INRegister))]
  public PXSelect<INTran, Where<INTran.docType, Equal<INDocType.adjustment>, And<INTran.refNbr, Equal<Current<INRegister.refNbr>>>>, OrderBy<Asc<INTran.docType, Asc<INTran.refNbr, Asc<INTran.sortOrder, Asc<INTran.lineNbr>>>>>> transactions;
  [PXCopyPasteHiddenView]
  public PXSelect<INTranSplit, Where<INTranSplit.tranType, Equal<Argument<string>>, And<INTranSplit.refNbr, Equal<Argument<string>>, And<INTranSplit.lineNbr, Equal<Argument<short?>>>>>> splits;

  public virtual void Splits(
    [PXDBString(3, IsFixed = true)] ref string INTran_tranType,
    [PXDBString(10, IsUnicode = true)] ref string INTran_refNbr,
    [PXDBShort] ref short? INTran_lineNbr)
  {
    ((PXSelectBase<INTran>) this.transactions).Current = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXSelect<INTran, Where<INTran.tranType, Equal<Required<INTran.tranType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.lineNbr, Equal<Required<INTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) INTran_tranType,
      (object) INTran_refNbr,
      (object) INTran_lineNbr
    }));
  }

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<INTran.qty, INTran.unitCost>), typeof (SumCalc<INRegister.totalCost>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.tranCost> e)
  {
  }

  [PXDefault(typeof (SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<INTran.inventoryID, IBqlInt>.FromCurrent>>), SourceField = typeof (InventoryItem.baseUnit), CacheGlobal = true)]
  [INUnit(typeof (INTran.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.uOM> e)
  {
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("ADJ")]
  [PXUIField(Enabled = false, Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.tranType> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXRestrictorAttribute))]
  [PXRestrictor(typeof (Where<PX.Objects.CS.ReasonCode.usage, Equal<Optional<INTran.docType>>, Or<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.vendorReturn>, And<Optional<INTran.origModule>, Equal<BatchModule.modulePO>>>>), "The usage type of the reason code does not match the document type.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.reasonCode> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Receipt Nbr.", Enabled = true)]
  [PXVerifySelector(typeof (Search2<INCostStatus.receiptNbr, InnerJoin<INCostSubItemXRef, On<INCostSubItemXRef.costSubItemID, Equal<INCostStatus.costSubItemID>>, InnerJoin<INLocation, On<INLocation.locationID, Equal<Optional<INTran.locationID>>>>>, Where<INCostStatus.inventoryID, Equal<Optional<INTran.inventoryID>>, And<INCostSubItemXRef.subItemID, Equal<Optional<INTran.subItemID>>, And2<Where<CostCenter.freeStock, Equal<Optional<INTran.costCenterID>>, And<Where<INCostStatus.costSiteID, Equal<Optional<INTran.siteID>>, And<INLocation.isCosted, Equal<boolFalse>, Or<INCostStatus.costSiteID, Equal<Optional<INTran.locationID>>>>>>>, Or<INCostStatus.costSiteID, Equal<Optional<INTran.costCenterID>>>>>>>), new Type[] {typeof (INCostStatus.receiptNbr), typeof (INCostStatus.receiptDate), typeof (INCostStatus.qtyOnHand), typeof (INCostStatus.totalCost)}, VerifyField = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.origRefNbr> e)
  {
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search<INItemSite.tranUnitCost, Where<INItemSite.inventoryID, Equal<Current<INTran.inventoryID>>, And<INItemSite.siteID, Equal<Current<INTran.siteID>>>>>, Search<INItemCost.tranUnitCost, Where<INItemCost.inventoryID, Equal<Current<INTran.inventoryID>>, And<INItemCost.curyID, EqualBaseCuryID<Current2<INRegister.branchID>>>>>>))]
  [PXUIField(DisplayName = "Unit Cost")]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.unitCost> e)
  {
  }

  [LocationAvail(typeof (INTranSplit.inventoryID), typeof (INTranSplit.subItemID), typeof (INTran.costCenterID), typeof (INTranSplit.siteID), typeof (INTranSplit.tranType), typeof (INTranSplit.invtMult))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranSplit.locationID> e)
  {
  }

  public INAdjustmentEntry()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    PXUIFieldAttribute.SetVisible<INTran.tranType>(((PXSelectBase) this.transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INTran.tranType>(((PXSelectBase) this.transactions).Cache, (object) null, false);
    OpenPeriodAttribute.SetValidatePeriod<INRegister.finPeriodID>(((PXSelectBase) this.adjustment).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXVerifySelectorAttribute.SetVerifyField<INTran.origRefNbr>(((PXSelectBase) this.transactions).Cache, (object) null, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType>, INRegister, object>) e).NewValue = (object) "A";
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
    bool? nullable = e.Row.Released;
    bool flag1 = false;
    bool flag2 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue && e.Row.OrigModule != "AP";
    bool flag3 = e.Row.OrigModule != "PO";
    bool flag4 = !string.IsNullOrEmpty(e.Row.PIID);
    nullable = e.Row.Released;
    if (nullable.GetValueOrDefault())
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, false);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowInsert = true;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowUpdate = flag2;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowDelete = flag2 & flag3;
    ((PXSelectBase) this.transactions).Cache.AllowInsert = flag2 & flag3 && !flag4;
    ((PXSelectBase) this.transactions).Cache.AllowUpdate = flag2;
    ((PXSelectBase) this.transactions).Cache.AllowDelete = flag2 & flag3 && !flag4;
    ((PXSelectBase) this.splits).Cache.AllowInsert = flag2;
    ((PXSelectBase) this.splits).Cache.AllowUpdate = flag2;
    ((PXSelectBase) this.splits).Cache.AllowDelete = flag2;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row1 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    int num1 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.controlQty>(cache1, (object) row1, num1 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row2 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    int num2 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.controlCost>(cache2, (object) row2, num2 != 0);
    PXUIFieldAttribute.SetVisible<INRegister.pIID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, !string.IsNullOrEmpty(e.Row.PIID));
    PXAction<INRegister> release = this.release;
    nullable = e.Row.Hold;
    bool flag5 = false;
    int num3;
    if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
    {
      nullable = e.Row.Released;
      bool flag6 = false;
      num3 = nullable.GetValueOrDefault() == flag6 & nullable.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    ((PXAction) release).SetEnabled(num3 != 0);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INRegister> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 3 || string.IsNullOrEmpty(e.Row?.PIID))
      return;
    PXGraph.CreateInstance<INPIController>().ReopenPI(e.Row.PIID);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.docType>, INTran, object>) e).NewValue = (object) "A";
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.unitCost> e)
  {
    this.SetupManualCostFlag(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.unitCost>>) e).Cache, e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<INTran, INTran.unitCost>>) e).ExternalCall);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.tranCost> e)
  {
    this.SetupManualCostFlag(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.tranCost>>) e).Cache, e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<INTran, INTran.tranCost>>) e).ExternalCall);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.lotSerialNbr> e)
  {
    this.DefaultUnitCost(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.lotSerialNbr>>) e).Cache, e.Row, false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.origRefNbr> e)
  {
    this.DefaultUnitCost(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.origRefNbr>>) e).Cache, e.Row, false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.costCenterID> e)
  {
    if (object.Equals(((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<INTran, INTran.costCenterID>, INTran, object>) e).OldValue, e.NewValue) || e.Row.OrigRefNbr == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.costCenterID>>) e).Cache.VerifyFieldAndRaiseException<INTran.origRefNbr>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.origRefNbr> e)
  {
    if (!(e.Row?.TranType == "RCA"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.origRefNbr>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<INTran> e)
  {
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, (int?) e.Row?.InventoryID);
    bool flag1 = inventoryItem == null || inventoryItem.ValMethod != "T";
    bool isPIAdjustment = !string.IsNullOrEmpty(((PXSelectBase<INRegister>) this.adjustment).Current?.PIID);
    int num1;
    if (isPIAdjustment)
    {
      INTran row = e.Row;
      int num2;
      if (row == null)
      {
        num2 = 0;
      }
      else
      {
        Decimal? qty = row.Qty;
        Decimal num3 = 0M;
        num2 = qty.GetValueOrDefault() < num3 & qty.HasValue ? 1 : 0;
      }
      if (num2 != 0)
      {
        num1 = inventoryItem?.ValMethod == "F" ? 1 : 0;
        goto label_7;
      }
    }
    num1 = 0;
label_7:
    bool flag2 = num1 != 0;
    int num4;
    if (isPIAdjustment)
    {
      INTran row = e.Row;
      if (row == null)
      {
        num4 = 0;
      }
      else
      {
        Decimal? qty = row.Qty;
        Decimal num5 = 0M;
        num4 = qty.GetValueOrDefault() > num5 & qty.HasValue ? 1 : 0;
      }
    }
    else
      num4 = 0;
    bool flag3 = num4 != 0;
    PXUIFieldAttribute.SetEnabled<INTran.branchID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    PXUIFieldAttribute.SetEnabled<INTran.inventoryID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    PXUIFieldAttribute.SetEnabled<INTran.siteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    PXUIFieldAttribute.SetEnabled<INTran.locationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    PXUIFieldAttribute.SetEnabled<INTran.qty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    PXUIFieldAttribute.SetEnabled<INTran.unitCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment & flag1 | flag3);
    PXUIFieldAttribute.SetEnabled<INTran.tranCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment | flag3);
    PXUIFieldAttribute.SetEnabled<INTran.expireDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    PXUIFieldAttribute.SetEnabled<INTran.origRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment | flag2);
    PXUIFieldAttribute.SetEnabled<INTran.reasonCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    PXUIFieldAttribute.SetEnabled<INTran.tranDesc>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, !isPIAdjustment);
    bool uomEnabled = this.GetUOMEnabled(isPIAdjustment, e.Row);
    PXUIFieldAttribute.SetEnabled<INTran.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, uomEnabled);
    PXCacheEx.Adjust<INLotSerialNbrAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row).For<INTran.lotSerialNbr>((Action<INLotSerialNbrAttribute>) (a => a.ForceDisable = isPIAdjustment));
    PXUIFieldAttribute.SetVisible<INTran.manualCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) null, isPIAdjustment);
    PXUIFieldAttribute.SetVisible<INTran.pILineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) null, isPIAdjustment);
  }

  protected virtual bool GetUOMEnabled(bool isPIAdjustment, INTran tran) => !isPIAdjustment;

  protected virtual void _(PX.Data.Events.RowUpdated<INTran> e)
  {
    if (!(InventoryItem.PK.Find((PXGraph) this, (int?) e.Row?.InventoryID)?.ValMethod == "T") || !(e.Row.TranType == "ADJ"))
      return;
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    Decimal? baseQty = e.Row.BaseQty;
    Decimal num2 = 0M;
    if (!(baseQty.GetValueOrDefault() == num2 & baseQty.HasValue))
      return;
    Decimal? tranCost = e.Row.TranCost;
    Decimal num3 = 0M;
    if (tranCost.GetValueOrDefault() == num3 & tranCost.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INTran>>) e).Cache.RaiseExceptionHandling<INTran.tranCost>((object) e.Row, (object) e.Row.TranCost, (Exception) new PXSetPropertyException("Cost only adjustments are not allowed for Standard Cost items."));
  }

  protected override void _(PX.Data.Events.RowPersisting<INTran> e)
  {
    base._(e);
    if (PXDBOperationExt.Command(e.Operation) == 3)
      return;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, (int?) e.Row?.InventoryID);
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, inventoryItem?.LotSerClassID);
    short? invtMult = e.Row.InvtMult;
    int? nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num1 = 0;
    Decimal? nullable2;
    int num2;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
    {
      if (!(inventoryItem?.ValMethod == "S"))
      {
        if (inLotSerClass != null && inLotSerClass.LotSerTrack != "N" && inLotSerClass.LotSerAssign == "R")
        {
          nullable2 = e.Row.Qty;
          Decimal num3 = 0M;
          if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
            goto label_5;
        }
        else
          goto label_5;
      }
      num2 = 1;
      goto label_7;
    }
label_5:
    num2 = 2;
label_7:
    PXPersistingCheck pxPersistingCheck = (PXPersistingCheck) num2;
    PXDefaultAttribute.SetPersistingCheck<INTran.subID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache, (object) e.Row, (PXPersistingCheck) 0);
    PXDefaultAttribute.SetPersistingCheck<INTran.locationID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache, (object) e.Row, (PXPersistingCheck) 0);
    PXDefaultAttribute.SetPersistingCheck<INTran.lotSerialNbr>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache, (object) e.Row, pxPersistingCheck);
    if (((PXSelectBase<INRegister>) this.adjustment).Current != null && ((PXSelectBase<INRegister>) this.adjustment).Current.OrigModule != "PI" && inventoryItem != null && inventoryItem.ValMethod == "F" && e.Row.OrigRefNbr == null)
    {
      bool flag = false;
      if (e.Row != null && e.Row.POReceiptNbr != null)
      {
        nullable1 = e.Row.POReceiptLineNbr;
        if (nullable1.HasValue)
        {
          PX.Objects.PO.POReceiptLine poReceiptLine = PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) this, e.Row.POReceiptType, e.Row.POReceiptNbr, e.Row.POReceiptLineNbr);
          flag = poReceiptLine != null && POLineType.IsDropShip(poReceiptLine.LineType);
        }
      }
      if (!flag)
      {
        if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache.RaiseExceptionHandling<INTran.origRefNbr>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[origRefNbr]"
        })))
          throw new PXRowPersistingException(typeof (INTran.origRefNbr).Name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) typeof (INTran.origRefNbr).Name
          });
      }
    }
    if (inventoryItem?.ValMethod == "T" && e.Row.TranType == "ADJ")
    {
      invtMult = e.Row.InvtMult;
      nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num4 = 0;
      if (!(nullable1.GetValueOrDefault() == num4 & nullable1.HasValue))
      {
        nullable2 = e.Row.BaseQty;
        Decimal num5 = 0M;
        if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
        {
          nullable2 = e.Row.TranCost;
          Decimal num6 = 0M;
          if (!(nullable2.GetValueOrDefault() == num6 & nullable2.HasValue) && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache.RaiseExceptionHandling<INTran.tranCost>((object) e.Row, (object) e.Row.TranCost, (Exception) new PXSetPropertyException("Cost only adjustments are not allowed for Standard Cost items.")))
            throw new PXRowPersistingException(typeof (INTran.tranCost).Name, (object) e.Row.TranCost, "Cost only adjustments are not allowed for Standard Cost items.");
        }
      }
    }
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache.VerifyFieldAndRaiseException<INTran.origRefNbr>((object) e.Row);
  }

  protected virtual void SetupManualCostFlag(PXCache inTranCache, INTran tran, bool externalCall)
  {
    if (string.IsNullOrEmpty(((PXSelectBase<INRegister>) this.adjustment).Current?.PIID) || !externalCall || tran == null)
      return;
    Decimal? qty = tran.Qty;
    Decimal num = 0M;
    if (qty.GetValueOrDefault() <= num & qty.HasValue || tran.ManualCost.GetValueOrDefault())
      return;
    inTranCache.SetValueExt<INTran.manualCost>((object) tran, (object) true);
  }

  public override void DefaultUnitCost(PXCache cache, INTran tran, bool setZero = false)
  {
    if (((PXSelectBase<INRegister>) this.adjustment).Current != null && ((PXSelectBase<INRegister>) this.adjustment).Current.OrigModule == "PI")
      return;
    int? nullable1;
    int num1;
    if (tran == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = tran.InventoryID;
      num1 = !nullable1.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    object obj = (object) null;
    int? inventoryID;
    if (tran == null)
    {
      nullable1 = new int?();
      inventoryID = nullable1;
    }
    else
      inventoryID = tran.InventoryID;
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, inventoryID);
    if (inventoryItem.ValMethod == "S" && !string.IsNullOrEmpty(tran.LotSerialNbr))
    {
      INCostStatus inCostStatus = PXResultset<INCostStatus>.op_Implicit(PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLocation>.On<BqlOperand<INLocation.locationID, IBqlInt>.IsEqual<BqlField<INTran.locationID, IBqlInt>.FromCurrent>>>, FbqlJoins.Inner<INCostSubItemXRef>.On<BqlOperand<INCostSubItemXRef.costSubItemID, IBqlInt>.IsEqual<INCostStatus.costSubItemID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<BqlField<INTran.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INCostSubItemXRef.subItemID, IBqlInt>.IsEqual<BqlField<INTran.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCostStatus.lotSerialNbr, IBqlString>.IsEqual<BqlField<INTran.lotSerialNbr, IBqlString>.FromCurrent>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.isCosted, Equal<False>>>>, And<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<BqlField<INTran.siteID, IBqlInt>.FromCurrent>>>>.Or<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<BqlField<INTran.locationID, IBqlInt>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) tran
      }, Array.Empty<object>()));
      if (inCostStatus != null)
      {
        Decimal? qtyOnHand1 = inCostStatus.QtyOnHand;
        Decimal num2 = 0M;
        if (!(qtyOnHand1.GetValueOrDefault() == num2 & qtyOnHand1.HasValue))
        {
          Decimal? totalCost = inCostStatus.TotalCost;
          Decimal? qtyOnHand2 = inCostStatus.QtyOnHand;
          obj = (object) PXDBPriceCostAttribute.Round((totalCost.HasValue & qtyOnHand2.HasValue ? new Decimal?(totalCost.GetValueOrDefault() / qtyOnHand2.GetValueOrDefault()) : new Decimal?()).Value);
        }
      }
    }
    else if (inventoryItem.ValMethod == "F" && !string.IsNullOrEmpty(tran.OrigRefNbr))
    {
      INCostStatus inCostStatus = PXResultset<INCostStatus>.op_Implicit(PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLocation>.On<BqlOperand<INLocation.locationID, IBqlInt>.IsEqual<BqlField<INTran.locationID, IBqlInt>.FromCurrent>>>, FbqlJoins.Inner<INCostSubItemXRef>.On<BqlOperand<INCostSubItemXRef.costSubItemID, IBqlInt>.IsEqual<INCostStatus.costSubItemID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<BqlField<INTran.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INCostSubItemXRef.subItemID, IBqlInt>.IsEqual<BqlField<INTran.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCostStatus.receiptNbr, IBqlString>.IsEqual<BqlField<INTran.origRefNbr, IBqlString>.FromCurrent>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.isCosted, Equal<False>>>>, And<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<BqlField<INTran.siteID, IBqlInt>.FromCurrent>>>>.Or<BqlOperand<INCostStatus.costSiteID, IBqlInt>.IsEqual<BqlField<INTran.locationID, IBqlInt>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) tran
      }, Array.Empty<object>()));
      if (inCostStatus != null)
      {
        Decimal? qtyOnHand3 = inCostStatus.QtyOnHand;
        Decimal num3 = 0M;
        if (!(qtyOnHand3.GetValueOrDefault() == num3 & qtyOnHand3.HasValue))
        {
          Decimal? totalCost = inCostStatus.TotalCost;
          Decimal? qtyOnHand4 = inCostStatus.QtyOnHand;
          obj = (object) PXDBPriceCostAttribute.Round((totalCost.HasValue & qtyOnHand4.HasValue ? new Decimal?(totalCost.GetValueOrDefault() / qtyOnHand4.GetValueOrDefault()) : new Decimal?()).Value);
        }
      }
    }
    else
    {
      if (inventoryItem.ValMethod == "A")
        cache.RaiseFieldDefaulting<INTran.avgCost>((object) tran, ref obj);
      if (obj == null || (Decimal) obj == 0M)
        cache.RaiseFieldDefaulting<INTran.unitCost>((object) tran, ref obj);
    }
    Decimal? nullable2 = (Decimal?) cache.GetValue<INTran.qty>((object) tran);
    if (obj == null)
      return;
    if (!((Decimal) obj != 0M | setZero))
    {
      Decimal? nullable3 = nullable2;
      Decimal num4 = 0M;
      if (!(nullable3.GetValueOrDefault() < num4 & nullable3.HasValue))
        return;
    }
    if ((Decimal) obj < 0M)
      cache.RaiseFieldDefaulting<INTran.unitCost>((object) tran, ref obj);
    Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertToBase<INTran.inventoryID>(cache, (object) tran, tran.UOM, (Decimal) obj, INPrecision.UNITCOST));
    Decimal? nullable5 = nullable2;
    Decimal num5 = 0M;
    if (nullable5.GetValueOrDefault() == num5 & nullable5.HasValue)
      cache.SetValue<INTran.unitCost>((object) tran, (object) nullable4);
    else
      cache.SetValueExt<INTran.unitCost>((object) tran, (object) nullable4);
  }

  public override PXSelectBase<INRegister> INRegisterDataMember
  {
    get => (PXSelectBase<INRegister>) this.adjustment;
  }

  public override PXSelectBase<INTran> INTranDataMember => (PXSelectBase<INTran>) this.transactions;

  public override PXSelectBase<INTran> LSSelectDataMember => this.LineSplittingExt.lsselect;

  public override PXSelectBase<INTranSplit> INTranSplitDataMember
  {
    get => (PXSelectBase<INTranSplit>) this.splits;
  }

  protected override string ScreenID => "IN303000";

  public INAdjustmentEntry.LineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<INAdjustmentEntry.LineSplittingExtension>();
  }

  public INAdjustmentEntry.ItemAvailabilityExtension ItemAvailabilityExt
  {
    get => ((PXGraph) this).FindImplementation<INAdjustmentEntry.ItemAvailabilityExtension>();
  }

  public class SiteStatusLookup : INSiteStatusLookupExt<INAdjustmentEntry>
  {
    protected override bool IsAddItemEnabled(INRegister doc)
    {
      return ((PXSelectBase) this.Transactions).AllowDelete;
    }
  }

  public class LineSplittingExtension : INRegisterLineSplittingExtension<INAdjustmentEntry>
  {
    protected override void SubscribeForLineEvents()
    {
      base.SubscribeForLineEvents();
      ((FieldVerifyingEvents) ((PXGraph) this.Base).FieldVerifying).AddAbstractHandler<INTran, INTran.uOM, string>(new Action<AbstractEvents.IFieldVerifying<INTran, INTran.uOM, string>>(this.EventHandler));
    }

    protected override void ClearLotSerial(AbstractEvents.IRowUpdated<INTran> e)
    {
      if (!(e.Row.OrigModule != "PI"))
        return;
      base.ClearLotSerial(e);
    }

    protected override bool AllowSplitCreationForLineWithEmptyLotSerialNbr(INLotSerTrack.Mode mode)
    {
      return true;
    }

    public virtual void EventHandler(
      AbstractEvents.IFieldVerifying<INTran, INTran.uOM, string> e)
    {
      if (!e.Row.InventoryID.HasValue)
        return;
      InventoryItem inventoryItem;
      INLotSerClass inLotSerClass;
      this.ReadInventoryItem(e.Row.InventoryID).Deconstruct(ref inventoryItem, ref inLotSerClass);
      INLotSerClass lotSerClass = inLotSerClass;
      string tranType = e.Row.TranType;
      short? invtMult = e.Row.InvtMult;
      int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult))
        return;
      object objA;
      ((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache.RaiseFieldDefaulting<INTran.uOM>((object) e.Row, ref objA);
      if (object.Equals(objA, (object) e.NewValue))
        return;
      e.NewValue = (string) objA;
      ((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache.RaiseExceptionHandling<INTran.uOM>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("Serialized item adjustment can be made for zero or one '{0}' items. UOM was changed to match.", (PXErrorLevel) 2, new object[1]
      {
        objA
      }));
    }

    public override void EventHandlerQty(
      AbstractEvents.IFieldVerifying<INTran, IBqlField, Decimal?> e)
    {
      if (!e.Row.InventoryID.HasValue)
        return;
      InventoryItem inventoryItem1;
      INLotSerClass inLotSerClass;
      this.ReadInventoryItem(e.Row.InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass);
      InventoryItem inventoryItem2 = inventoryItem1;
      INLotSerClass lotSerClass = inLotSerClass;
      string tranType = e.Row.TranType;
      short? invtMult = e.Row.InvtMult;
      int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult) || !EnumerableExtensions.IsNotIn<Decimal?>(e.NewValue, new Decimal?(), new Decimal?(0M), new Decimal?(1M), new Decimal?(-1M)))
        return;
      AbstractEvents.IFieldVerifying<INTran, IBqlField, Decimal?> ifieldVerifying = e;
      Decimal? newValue = e.NewValue;
      Decimal num = 0M;
      Decimal? nullable = new Decimal?(newValue.GetValueOrDefault() > num & newValue.HasValue ? 1M : -1M);
      ifieldVerifying.NewValue = nullable;
      ((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache.RaiseExceptionHandling<INTran.qty>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("Serialized item adjustment can be made for zero or one '{0}' items. Line quantity was changed to match.", (PXErrorLevel) 2, new object[1]
      {
        (object) inventoryItem2.BaseUnit
      }));
    }

    public override void CreateNumbers(INTran line, Decimal deltaBaseQty, bool forceAutoNextNbr)
    {
      if (!line.InventoryID.HasValue)
        return;
      InventoryItem inventoryItem;
      INLotSerClass inLotSerClass1;
      this.ReadInventoryItem(line.InventoryID).Deconstruct(ref inventoryItem, ref inLotSerClass1);
      INLotSerClass inLotSerClass2 = inLotSerClass1;
      if (inLotSerClass2.LotSerTrack != "N" && inLotSerClass2.LotSerAssign != "U" && (!line.SubItemID.HasValue || !line.LocationID.HasValue))
        return;
      base.CreateNumbers(line, deltaBaseQty, forceAutoNextNbr);
    }

    public override void IssueNumbers(INTran line, Decimal deltaBaseQty)
    {
      if (!line.InventoryID.HasValue)
        return;
      InventoryItem inventoryItem;
      INLotSerClass inLotSerClass1;
      this.ReadInventoryItem(line.InventoryID).Deconstruct(ref inventoryItem, ref inLotSerClass1);
      INLotSerClass inLotSerClass2 = inLotSerClass1;
      if (inLotSerClass2.LotSerTrack != "N" && inLotSerClass2.LotSerAssign != "U" && (line.LotSerialNbr == null || !line.SubItemID.HasValue || !line.LocationID.HasValue))
        return;
      base.IssueNumbers(line, deltaBaseQty);
    }
  }

  public class ItemAvailabilityExtension : INRegisterItemAvailabilityExtension<INAdjustmentEntry>
  {
  }
}
