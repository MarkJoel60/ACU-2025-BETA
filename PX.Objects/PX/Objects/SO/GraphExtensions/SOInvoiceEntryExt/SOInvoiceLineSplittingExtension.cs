// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.SOInvoiceLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class SOInvoiceLineSplittingExtension : 
  LineSplittingExtension<SOInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, ARTranAsSplit>
{
  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranAsSplit.tranType, Equal<BqlField<PX.Objects.AR.ARInvoice.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<ARTranAsSplit.refNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<ARTranAsSplit.lineType, IBqlString>.IsNotIn<SOLineType.freight, SOLineType.discount>>);
    }
  }

  protected override Type LineQtyField => typeof (PX.Objects.AR.ARTran.qty);

  public override ARTranAsSplit LineToSplit(PX.Objects.AR.ARTran line)
  {
    using (this.InvtMultModeScope(line))
    {
      ARTranAsSplit split = ARTranAsSplit.FromARTran(line);
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      return split;
    }
  }

  protected override void SubscribeForLineEvents()
  {
    base.SubscribeForLineEvents();
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.locationID, int?>>(this.EventHandler));
    ((FieldVerifyingEvents) ((PXGraph) this.Base).FieldVerifying).AddAbstractHandler<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM, string>(new Action<AbstractEvents.IFieldVerifying<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM, string>>(this.EventHandler));
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.locationID, int?> e)
  {
    if (e.Row == null)
      return;
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue) && !(e.Row.LineType != "GI"))
      return;
    ((ICancelEventArgs) e).Cancel = true;
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldVerifying<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM, string> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    string str = INTranType.TranTypeFromInvoiceType(e.Row.TranType, e.Row.Qty);
    if (pxResult == null)
      return;
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = str;
    invtMult = e.Row.InvtMult;
    int? invMult;
    if (!invtMult.HasValue)
    {
      nullable = new int?();
      invMult = nullable;
    }
    else
      invMult = new int?((int) invtMult.GetValueOrDefault());
    if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult))
      return;
    object objA;
    ((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache.RaiseFieldDefaulting<PX.Objects.AR.ARTran.uOM>((object) e.Row, ref objA);
    if (object.Equals(objA, (object) e.NewValue))
      return;
    e.NewValue = (string) objA;
    ((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.AR.ARTran.uOM>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("Serialized item adjustment can be made for zero or one '{0}' items. UOM was changed to match.", (PXErrorLevel) 2, new object[1]
    {
      objA
    }));
  }

  protected override void EventHandler(AbstractEvents.IRowSelected<PX.Objects.AR.ARTran> e)
  {
    base.EventHandler(e);
    if (e.Row == null)
      return;
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    bool flag1 = !(nullable.GetValueOrDefault() == num & nullable.HasValue);
    bool flag2 = flag1 && e.Row.LineType == "GI";
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARTran.subItemID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARTran.siteID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARTran.locationID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, flag2);
    PXPersistingCheck pxPersistingCheck = flag2 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARTran.subItemID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, pxPersistingCheck);
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARTran.siteID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, pxPersistingCheck);
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.AR.ARTran.locationID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, pxPersistingCheck);
  }

  protected override void EventHandler(AbstractEvents.IRowInserted<PX.Objects.AR.ARTran> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    base.EventHandler(e);
    if (!ARTranPlan.IsDirectLineNotLinkedToSO(e.Row))
      return;
    this.Availability.Check((ILSMaster) e.Row, new int?(0));
  }

  protected override void EventHandler(AbstractEvents.IRowDeleted<PX.Objects.AR.ARTran> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    base.EventHandler(e);
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<PX.Objects.AR.ARTran> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    if (e.Row.TranType != e.OldRow.TranType)
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetDefaultExt<PX.Objects.AR.ARTran.invtMult>((object) e.Row);
    base.EventHandler(e);
    if (!ARTranPlan.IsDirectLineNotLinkedToSO(e.Row))
      return;
    this.Availability.Check((ILSMaster) e.Row, new int?(0));
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<PX.Objects.AR.ARTran> e)
  {
    bool flag = false;
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      if (Math.Abs(e.Row.BaseQty.Value) >= 0.0000005M)
      {
        Decimal? unassignedQty = e.Row.UnassignedQty;
        Decimal num = 0.0000005M;
        if (!(unassignedQty.GetValueOrDefault() >= num & unassignedQty.HasValue))
        {
          unassignedQty = e.Row.UnassignedQty;
          num = -0.0000005M;
          if (!(unassignedQty.GetValueOrDefault() <= num & unassignedQty.HasValue))
            goto label_6;
        }
        if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.AR.ARTran.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number")))
          throw new PXRowPersistingException(typeof (PX.Objects.AR.ARTran.qty).Name, (object) e.Row.Qty, "One or more lines have unassigned Location and/or Lot/Serial Number");
      }
label_6:
      ((PXGraph) this.Base).FindImplementation<SOInvoiceItemAvailabilityExtension>()?.MemoOrderCheck(e.Row);
      if (!string.IsNullOrEmpty(e.Row?.LotSerialNbr))
      {
        PXCache<ARTranAsSplit> pxCache = GraphHelper.Caches<ARTranAsSplit>((PXGraph) this.Base);
        ARTranAsSplit arTranAsSplit = pxCache.Locate(ARTranAsSplit.FromARTran(e.Row));
        flag = arTranAsSplit != null && EnumerableExtensions.IsNotIn<PXEntryStatus>(pxCache.GetStatus(arTranAsSplit), (PXEntryStatus) 3, (PXEntryStatus) 4) && !string.IsNullOrEmpty(arTranAsSplit.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(arTranAsSplit.AssignedNbr, e.Row.LotSerialNbr);
      }
    }
    base.EventHandler(e);
    if (!flag || !this.GenerateLotSerialNumberOnPersist(e.Row))
      return;
    ((PXGraph) this.Base).FindImplementation<ARTranPlan>()?.RaiseRowUpdated(e.Row);
  }

  public override void EventHandlerQty(
    AbstractEvents.IFieldVerifying<PX.Objects.AR.ARTran, IBqlField, Decimal?> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    nullable = e.Row.InventoryID;
    if (!nullable.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem1;
    INLotSerClass inLotSerClass;
    this.ReadInventoryItem(e.Row.InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass);
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    INLotSerClass lotSerClass = inLotSerClass;
    string tranType = INTranType.TranTypeFromInvoiceType(e.Row.TranType, e.Row.Qty);
    invtMult = e.Row.InvtMult;
    int? invMult;
    if (!invtMult.HasValue)
    {
      nullable = new int?();
      invMult = nullable;
    }
    else
      invMult = new int?((int) invtMult.GetValueOrDefault());
    if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult) || !EnumerableExtensions.IsNotIn<Decimal?>(e.NewValue, new Decimal?(), new Decimal?(0M), new Decimal?(1M), new Decimal?(-1M)))
      return;
    e.NewValue = new Decimal?(e.NewValue.Value > 0M ? 1M : -1M);
    ((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.AR.ARTran.qty>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("Serialized item adjustment can be made for zero or one '{0}' items. Line quantity was changed to match.", (PXErrorLevel) 2, new object[1]
    {
      (object) inventoryItem2.BaseUnit
    }));
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<ARTranAsSplit, ARTranAsSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<ARTranAsSplit, ARTranAsSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<ARTranAsSplit, ARTranAsSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.locationID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<ARTranAsSplit, ARTranAsSplit.lotSerialNbr, string>(new Action<AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.lotSerialNbr, string>>(this.EventHandler));
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.invtMult, short?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    using (this.InvtMultModeScope(this.LineCurrent))
    {
      e.NewValue = this.LineCurrent.InvtMult;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.subItemID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    e.NewValue = this.LineCurrent.SubItemID;
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.locationID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    e.NewValue = this.LineCurrent.LocationID;
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<ARTranAsSplit, ARTranAsSplit.lotSerialNbr, string> e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    if (pxResult == null)
      return;
    if (!e.Row.InvtMult.HasValue)
    {
      object obj;
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<ARTranAsSplit.invtMult>((object) e.Row, ref obj);
    }
    INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) e.Row, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
    if (tranTrackMode != INLotSerTrack.Mode.None && (tranTrackMode & INLotSerTrack.Mode.Create) <= INLotSerTrack.Mode.None)
      return;
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    foreach (ARTranAsSplit number in INLotSerialNbrAttribute.CreateNumbers<ARTranAsSplit>(((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum, tranTrackMode, 1M))
    {
      e.NewValue = number.LotSerialNbr;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  public override void CreateNumbers(PX.Objects.AR.ARTran line, Decimal deltaBaseQty, bool forceAutoNextNbr)
  {
    if (this.ShouldSkipLotSerailNbrCreation(line))
      return;
    base.CreateNumbers(line, deltaBaseQty, forceAutoNextNbr);
  }

  public override void IssueNumbers(PX.Objects.AR.ARTran line, Decimal deltaBaseQty)
  {
    if (this.ShouldSkipLotSerailNbrCreation(line))
      return;
    base.IssueNumbers(line, deltaBaseQty);
  }

  protected virtual bool ShouldSkipLotSerailNbrCreation(PX.Objects.AR.ARTran line)
  {
    if (line.SubItemID.HasValue && line.LocationID.HasValue)
      return false;
    PX.Objects.IN.InventoryItem inventoryItem;
    INLotSerClass inLotSerClass1;
    this.ReadInventoryItem(line.InventoryID).Deconstruct(ref inventoryItem, ref inLotSerClass1);
    INLotSerClass inLotSerClass2 = inLotSerClass1;
    return inLotSerClass2.LotSerTrack != "N" && inLotSerClass2.LotSerAssign == "R";
  }

  protected override bool UseAvailabilityToIssueLotSerials(PX.Objects.AR.ARTran line, ARTranAsSplit split)
  {
    return false;
  }

  protected override PXSelectBase<INLotSerialStatusByCostCenter> GetSerialStatusCmdBase(
    PX.Objects.AR.ARTran line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    return (PXSelectBase<INLotSerialStatusByCostCenter>) new FbqlSelect<SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<INLotSerialStatusByCostCenter.FK.Location>>, FbqlJoins.Inner<INSiteLotSerial>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>>>>, And<BqlOperand<INSiteLotSerial.siteID, IBqlInt>.IsEqual<INLotSerialStatusByCostCenter.siteID>>>>.And<BqlOperand<INSiteLotSerial.lotSerialNbr, IBqlString>.IsEqual<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<BqlField<INLotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INLotSerialStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlOperand<INSiteLotSerial.qtyHardAvail, IBqlDecimal>.IsGreater<decimal0>>>, INLotSerialStatusByCostCenter>.View((PXGraph) this.Base);
  }

  protected override void AppendSerialStatusCmdWhere(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    PX.Objects.AR.ARTran line,
    INLotSerClass lotSerClass)
  {
    if (line.SubItemID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    if (line.LocationID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLocation.salesValid, IBqlBool>.IsEqual<True>>>();
    if (!string.IsNullOrEmpty(line.LotSerialNbr))
    {
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<BqlField<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.FromCurrent>>>();
    }
    else
    {
      if (!lotSerClass.IsManualAssignRequired.GetValueOrDefault())
        return;
      cmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
    }
  }

  public override void UpdateParent(
    PX.Objects.AR.ARTran line,
    ARTranAsSplit newSplit,
    ARTranAsSplit oldSplit,
    out Decimal baseQty)
  {
    PX.Objects.AR.ARTran arTran = this.Clone(line);
    base.UpdateParent(line, newSplit, oldSplit, out baseQty);
    if (((PXCache) this.LineCache).ObjectsEqual<PX.Objects.AR.ARTran.subItemID, PX.Objects.AR.ARTran.locationID, PX.Objects.AR.ARTran.lotSerialNbr, PX.Objects.AR.ARTran.expireDate>((object) arTran, (object) line))
      return;
    ((PXGraph) this.Base).FindImplementation<ARTranPlan>().RaiseRowUpdated(line);
  }

  protected override INLotSerTrack.Mode GetTranTrackMode(ILSMaster row, INLotSerClass lotSerClass)
  {
    string str = INTranType.TranTypeFromInvoiceType(row.TranType, row.Qty);
    INLotSerClass lotSerClass1 = lotSerClass;
    string tranType = str;
    short? invtMult = row.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    return INLotSerialNbrAttribute.TranTrackMode(lotSerClass1, tranType, invMult);
  }

  public class ARLotSerialNbrAttribute : INLotSerialNbrAttribute
  {
    public ARLotSerialNbrAttribute(Type InventoryType, Type SubItemType, Type LocationType)
      : base(InventoryType, SubItemType, LocationType, typeof (CostCenter.freeStock))
    {
    }

    public ARLotSerialNbrAttribute(
      Type InventoryType,
      Type SubItemType,
      Type LocationType,
      Type CostCenterType)
      : base(InventoryType, SubItemType, LocationType, CostCenterType)
    {
    }

    protected override bool IsTracked(
      ILSMaster row,
      INLotSerClass lotSerClass,
      string tranType,
      int? invMult)
    {
      string tranType1 = INTranType.TranTypeFromInvoiceType(tranType, row.Qty);
      int? nullable = invMult;
      int num = 0;
      return !(nullable.GetValueOrDefault() == num & nullable.HasValue) && base.IsTracked(row, lotSerClass, tranType1, invMult);
    }
  }

  public class ARExpireDateAttribute(Type InventoryType) : INExpireDateAttribute(InventoryType)
  {
    protected override bool IsTrackExpiration(PXCache sender, ILSMaster row)
    {
      short? invtMult = row.InvtMult;
      int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num = 0;
      return !(nullable.GetValueOrDefault() == num & nullable.HasValue) && base.IsTrackExpiration(sender, row);
    }
  }
}
