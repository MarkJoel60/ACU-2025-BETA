// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.AddRelatedItemsToInvoice
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
using PX.Objects.SO.BQL;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class AddRelatedItemsToInvoice : AddRelatedItemExt<SOInvoiceEntry, PX.Objects.SO.SOInvoice, PX.Objects.AR.ARTran>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.relatedItems>() && PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>();
  }

  protected override bool SplitSerialTrackingItems => true;

  protected override DateTime? GetDocumentDate(PX.Objects.SO.SOInvoice invoice)
  {
    return ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current?.DocDate;
  }

  protected override PX.Objects.AR.ARTran FindFocusFor(PX.Objects.AR.ARTran line)
  {
    PXOrderedSelect<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<Where<PX.Objects.AR.ARTran.lineType, IsNull, Or<PX.Objects.AR.ARTran.lineType, NotEqual<SOLineType.discount>>>>>>, OrderBy<Asc<PX.Objects.AR.ARTran.tranType, Asc<PX.Objects.AR.ARTran.refNbr, Asc<PX.Objects.AR.ARTran.sortOrder, Asc<PX.Objects.AR.ARTran.lineNbr>>>>>> transactions = this.Base.Transactions;
    int? sortOrder = line.SortOrder;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) (sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?());
    object[] objArray = Array.Empty<object>();
    return PXResultset<PX.Objects.AR.ARTran>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARTran>) transactions).Search<PX.Objects.AR.ARTran.sortOrder>((object) local, objArray));
  }

  protected override RelatedItemHistory FindHistoryLine(int? lineNbr)
  {
    return PXResultset<RelatedItemHistory>.op_Implicit(((PXSelectBase<RelatedItemHistory>) this.RelatedItemsHistory).Search<RelatedItemHistory.relatedInvoiceLineNbr>((object) lineNbr, Array.Empty<object>()));
  }

  public override void Initialize()
  {
    base.Initialize();
    ((PXSelectBase<RelatedItemHistory>) this.RelatedItemsHistory).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RelatedItemHistory.invoiceDocType, Equal<BqlField<PX.Objects.SO.SOInvoice.docType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<RelatedItemHistory.invoiceRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOInvoice.refNbr, IBqlString>.FromCurrent>>>>();
  }

  [PXMergeAttributes]
  [PXFormula(typeof (SuggestRelatedItemsIsTrue<PX.Objects.SO.SOInvoice.docType, BqlField<PX.Objects.AR.ARInvoice.released, IBqlBool>.FromCurrent, PX.Objects.SO.SOInvoice.customerID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOInvoice.suggestRelatedItems> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.lineType, NotIn3<SOLineType.discount, SOLineType.freight>>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOOrderLineNbr, IBqlInt>.IsNull>>>.And<SuggestRelatedItemsIsTrue<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.released, PX.Objects.AR.ARTran.customerID>>))]
  protected virtual void ARTran_SuggestRelatedItems_CacheAttached(PXCache cache)
  {
  }

  [PX.Objects.IN.RelatedItems.RelatedItems(typeof (SubstitutableARTran.suggestRelatedItems), typeof (SubstitutableARTran.relatedItemsRelation), typeof (SubstitutableARTran.relatedItemsRequired), DocumentDateField = typeof (PX.Objects.AR.ARInvoice.docDate))]
  protected virtual void ARTran_RelatedItems_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (PX.Objects.AR.ARInvoice.docType))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RelatedItemHistory.invoiceDocType> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.refNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<RelatedItemHistory.invoiceRefNbr> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row == null)
      return;
    this.SetRelatedItemsVisible(this.AllowRelatedItems(PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.Base.SODocument).Select(Array.Empty<object>()))));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice> e)
  {
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.ObjectsEqual<PX.Objects.AR.ARInvoice.customerID, PX.Objects.AR.ARInvoice.released>((object) e.Row, (object) e.OldRow))
    {
      PX.Objects.SO.SOInvoice soInvoice = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOInvoice>) this.Base.SODocument).Select(Array.Empty<object>()));
      object obj = PXFormulaAttribute.Evaluate<PX.Objects.SO.SOInvoice.suggestRelatedItems>(((PXSelectBase) this.Base.SODocument).Cache, (object) soInvoice);
      ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.Base.SODocument).SetValueExt<PX.Objects.SO.SOInvoice.suggestRelatedItems>(soInvoice, obj);
    }
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.ObjectsEqual<PX.Objects.AR.ARInvoice.docDate>((object) e.OldRow, (object) e.Row))
      return;
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Select(Array.Empty<object>()))
      this.ResetSubstitutionRequired(PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult));
  }

  protected override Decimal? GetAvailableQty(PX.Objects.AR.ARTran line)
  {
    IStatus status = this.Base.ItemAvailabilityExt.FetchSite(line, true);
    return new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Base.Transactions).Cache, line.InventoryID, line.UOM, ((Decimal?) status?.QtyAvail).GetValueOrDefault(), INPrecision.QUANTITY));
  }

  protected override void FillRelatedItemHistory(
    RelatedItemHistory historyLine,
    PX.Objects.IN.RelatedItems.RelatedItemsFilter filter,
    PX.Objects.AR.ARTran originalLine,
    PX.Objects.AR.ARTran relatedLine,
    RelatedItem relatedItem)
  {
    base.FillRelatedItemHistory(historyLine, filter, originalLine, relatedLine, relatedItem);
    historyLine.OriginalInvoiceLineNbr = originalLine.LineNbr;
    historyLine.RelatedInvoiceLineNbr = relatedLine.LineNbr;
  }
}
