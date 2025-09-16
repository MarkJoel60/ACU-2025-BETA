// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INProductionEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INProductionEntry : INRegisterEntryBase
{
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.manufacturing>>> Document;
  [PXImport(typeof (INRegister))]
  public PXSelect<INTran, Where<INTran.docType, Equal<INDocType.manufacturing>, And<INTran.refNbr, Equal<Current<INRegister.refNbr>>>>> transactions;
  [PXCopyPasteHiddenView]
  public PXSelect<INTranSplit, Where<INTranSplit.docType, Equal<INDocType.manufacturing>, And<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>>>>> splits;
  public PXSetup<INSetup> Setup;

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXLineNbrAttribute))]
  [PXLineNbr(typeof (INRegister.lineCntr), DecrementOnDelete = false, ReuseGaps = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXRestrictorAttribute))]
  [PXRestrictor(typeof (Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.production>>), "The usage type of the reason code does not match the document type.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.reasonCode> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXLineNbrAttribute))]
  [PXLineNbr(typeof (INRegister.lineCntr), DecrementOnDelete = false, ReuseGaps = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranSplit.splitLineNbr> e)
  {
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<INTran.qty, INTran.unitCost>), typeof (SumCalc<INRegister.totalCost>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.tranCost> e)
  {
  }

  public INProductionEntry()
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    }
    OpenPeriodAttribute.SetValidatePeriod<INRegister.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<SiteStatusByCostCenter.negAvailQty>(INProductionEntry.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (INProductionEntry.\u003C\u003Ec.\u003C\u003E9__8_0 = new PXFieldDefaulting((object) INProductionEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__8_0))));
    ((PXAction) this.Delete).SetVisible(false);
    ((PXAction) this.Save).SetVisible(false);
    ((PXAction) this.Insert).SetVisible(false);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Document).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INRegister.refNbr>(((PXSelectBase) this.Document).Cache, (object) null, true);
    ((PXSelectBase) this.transactions).AllowInsert = ((PXSelectBase) this.transactions).AllowUpdate = ((PXSelectBase) this.transactions).AllowDelete = false;
  }

  public virtual bool CanClipboardCopyPaste() => false;

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType>, INRegister, object>) e).NewValue = (object) "M";
  }

  protected virtual void _(PX.Data.Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowInsert = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowUpdate = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowDelete = false;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowInsert = false;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowUpdate = false;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowDelete = false;
    PXUIFieldAttribute.SetVisible<INRegister.controlQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal.Value);
    PXUIFieldAttribute.SetVisible<INRegister.controlAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal.Value);
    PXUIFieldAttribute.SetVisible<INRegister.totalCost>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, e.Row.Released.GetValueOrDefault());
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.docType>, INTran, object>) e).NewValue = (object) "M";
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.tranType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.tranType>, INTran, object>) e).NewValue = (object) "III";
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.locationID> e)
  {
    if (((PXSelectBase<INRegister>) this.Document).Current == null || !(((PXSelectBase<INRegister>) this.Document).Current.OrigModule != "IN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.locationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTran, INTran.lotSerialNbr> e)
  {
    if (((PXSelectBase<INRegister>) this.Document).Current == null || !(((PXSelectBase<INRegister>) this.Document).Current.OrigModule != "IN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.lotSerialNbr>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTran, INTran.inventoryID> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.inventoryID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.inventoryID> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.inventoryID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.lotSerialNbr> e)
  {
    this.DefaultUnitCost(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.lotSerialNbr>>) e).Cache, e.Row, false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<INTran, INTran.origRefNbr> e)
  {
    this.DefaultUnitCost(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.origRefNbr>>) e).Cache, e.Row, false);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.tranCost> e)
  {
    if (e.Row.TranType != "ADJ" && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.tranCost>, INTran, object>) e).NewValue is Decimal newValue && newValue < 0M)
      throw new PXSetPropertyException((IBqlTable) e.Row, "The value must be greater than or equal to {0}.", (PXErrorLevel) 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID> e)
  {
    if (((PXSelectBase<INRegister>) this.Document).Current == null || !(((PXSelectBase<INRegister>) this.Document).Current.OrigModule != "IN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.lotSerialNbr> e)
  {
    if (((PXSelectBase<INRegister>) this.Document).Current == null || !(((PXSelectBase<INRegister>) this.Document).Current.OrigModule != "IN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.lotSerialNbr>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<INTranSplit> e)
  {
    if (e.TranStatus != 2 || !WebConfig.IsClusterEnabled)
      return;
    ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<INTranSplit>>) e).Cache.ClearQueryCacheObsolete();
  }

  public override PXSelectBase<INRegister> INRegisterDataMember
  {
    get => (PXSelectBase<INRegister>) this.Document;
  }

  public override PXSelectBase<INTran> INTranDataMember => (PXSelectBase<INTran>) this.transactions;

  public override PXSelectBase<INTran> LSSelectDataMember => this.LineSplittingExt.lsselect;

  public override PXSelectBase<INTranSplit> INTranSplitDataMember
  {
    get => (PXSelectBase<INTranSplit>) this.splits;
  }

  protected override string ScreenID => "IN308000";

  public INProductionEntry.LineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<INProductionEntry.LineSplittingExtension>();
  }

  public INProductionEntry.ItemAvailabilityExtension ItemAvailabilityExt
  {
    get => ((PXGraph) this).FindImplementation<INProductionEntry.ItemAvailabilityExtension>();
  }

  public override void DefaultUnitCost(PXCache cache, INTran tran, bool setZero = false)
  {
    if (((PXSelectBase<INRegister>) this.Document).Current != null && ((PXSelectBase<INRegister>) this.Document).Current.OrigModule == "PI")
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
    if (tran?.TranType != "ADJ")
    {
      base.DefaultUnitCost(cache, tran, setZero);
    }
    else
    {
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
  }

  public class LineSplittingExtension : INRegisterLineSplittingExtension<INProductionEntry>
  {
    public override void EventHandlerQty(
      AbstractEvents.IFieldVerifying<INTran, IBqlField, Decimal?> e)
    {
      if (e.Row.TranType == "ADJ")
        return;
      base.EventHandlerQty(e);
    }

    protected override bool IncludeKitSpecDetail(InventoryItem inventoryItem) => false;
  }

  public class ItemAvailabilityExtension : INRegisterItemAvailabilityExtension<INProductionEntry>
  {
  }
}
