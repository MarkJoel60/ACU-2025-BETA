// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.AddRelatedItemsToOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.RelatedItems;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class AddRelatedItemsToOrder : AddRelatedItemExt<SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.relatedItems>();

  protected override DateTime? GetDocumentDate(PX.Objects.SO.SOOrder document)
  {
    return new DateTime?(document.OrderDate.Value);
  }

  protected override PX.Objects.SO.SOLine FindFocusFor(PX.Objects.SO.SOLine line)
  {
    PXOrderedSelect<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>>>, OrderBy<Asc<PX.Objects.SO.SOLine.orderType, Asc<PX.Objects.SO.SOLine.orderNbr, Asc<PX.Objects.SO.SOLine.sortOrder, Asc<PX.Objects.SO.SOLine.lineNbr>>>>>> transactions = this.Base.Transactions;
    int? sortOrder = line.SortOrder;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) (sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?());
    object[] objArray = Array.Empty<object>();
    return PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) transactions).Search<PX.Objects.SO.SOLine.sortOrder>((object) local, objArray));
  }

  protected override RelatedItemHistory FindHistoryLine(int? lineNbr)
  {
    return PXResultset<RelatedItemHistory>.op_Implicit(((PXSelectBase<RelatedItemHistory>) this.RelatedItemsHistory).Search<RelatedItemHistory.relatedOrderLineNbr>((object) lineNbr, Array.Empty<object>()));
  }

  public override void Initialize()
  {
    base.Initialize();
    ((PXSelectBase<RelatedItemHistory>) this.RelatedItemsHistory).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RelatedItemHistory.orderType, Equal<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<RelatedItemHistory.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>();
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.behavior, NotEqual<SOBehavior.bL>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.dontApprove, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.hold, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.openShipmentCntr, Equal<Zero>>>>>.And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderQty, Equal<decimal0>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.status, NotEqual<SOOrderStatus.completed>>>>>.And<Brackets<BqlOperand<Selector<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrderType.requireShipping>, IBqlBool>.IsEqual<False>>>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.openLineCntr, IBqlInt>.IsGreater<Zero>>>>>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.dontApprove, Equal<False>>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.hold, IBqlBool>.IsEqual<True>>>>>, And<BqlOperand<Selector<PX.Objects.SO.SOOrder.customerID, PX.Objects.AR.Customer.suggestRelatedItems>, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<Selector<PX.Objects.SO.SOOrder.defaultOperation, SOOrderTypeOperation.iNDocType>, IBqlString>.IsNotEqual<INTranType.transfer>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.suggestRelatedItems> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<PX.Objects.SO.SOOrder.suggestRelatedItems>, Equal<True>>>>, And<BqlOperand<PX.Objects.SO.SOLine.operation, IBqlString>.IsEqual<SOOperation.issue>>>>.And<BqlOperand<PX.Objects.SO.SOLine.completed, IBqlBool>.IsNotEqual<True>>))]
  protected virtual void SOLine_SuggestRelatedItems_CacheAttached(PXCache cache)
  {
  }

  [SOLineRelatedItems(typeof (SubstitutableSOLine.suggestRelatedItems), typeof (SubstitutableSOLine.relatedItemsRelation), typeof (SubstitutableSOLine.relatedItemsRequired), DocumentDateField = typeof (PX.Objects.SO.SOOrder.orderDate))]
  protected virtual void SOLine_RelatedItems_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (PX.Objects.SO.SOOrder.orderType))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RelatedItemHistory.orderType> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXDBDefault(typeof (PX.Objects.SO.SOOrder.orderNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RelatedItemHistory.orderNbr> e)
  {
  }

  protected override PX.Objects.IN.RelatedItems.RelatedItemsFilter InitializeFilter(
    PX.Objects.SO.SOOrder document,
    PX.Objects.SO.SOLine line)
  {
    PX.Objects.IN.RelatedItems.RelatedItemsFilter relatedItemsFilter = base.InitializeFilter(document, line);
    relatedItemsFilter.OrderBehavior = document.Behavior;
    return relatedItemsFilter;
  }

  protected override Decimal? GetAvailableQty(PX.Objects.SO.SOLine line)
  {
    IStatus status = this.Base.ItemAvailabilityExt.FetchWithBaseUOM((ILSMaster) line, true, line.CostCenterID);
    return new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Base.Transactions).Cache, line.InventoryID, line.UOM, ((Decimal?) status?.QtyAvail).GetValueOrDefault(), INPrecision.QUANTITY));
  }

  protected override void FillRelatedItemHistory(
    RelatedItemHistory historyLine,
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    PX.Objects.SO.SOLine originalLine,
    PX.Objects.SO.SOLine relatedLine,
    RelatedItem relatedItem)
  {
    base.FillRelatedItemHistory(historyLine, filter, originalLine, relatedLine, relatedItem);
    historyLine.OriginalOrderLineNbr = originalLine.LineNbr;
    historyLine.RelatedOrderLineNbr = relatedLine.LineNbr;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.orderDate>((object) e.OldRow, (object) e.Row))
      return;
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
      this.ResetSubstitutionRequired(PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult));
  }
}
