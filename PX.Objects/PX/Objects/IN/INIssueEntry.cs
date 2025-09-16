// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INIssueEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INIssueEntry : INRegisterEntryBase
{
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.issue>>> issue;
  public PXSelect<INRegister, Where<INRegister.docType, Equal<INDocType.issue>, And<INRegister.refNbr, Equal<Current<INRegister.refNbr>>>>> CurrentDocument;
  [PXImport(typeof (INRegister))]
  public PXSelect<INTran, Where<INTran.docType, Equal<INDocType.issue>, And<INTran.refNbr, Equal<Current<INRegister.refNbr>>>>> transactions;
  [PXCopyPasteHiddenView]
  public PXSelect<INTranSplit, Where<INTranSplit.docType, Equal<INDocType.issue>, And<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>>>>> splits;

  [PXDefault(typeof (SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsEqual<BqlField<INTran.inventoryID, IBqlInt>.FromCurrent>>), SourceField = typeof (InventoryItem.salesUnit), CacheGlobal = true)]
  [INUnit(typeof (INTran.inventoryID))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.uOM> e)
  {
  }

  [LocationAvail(typeof (INTran.inventoryID), typeof (INTran.subItemID), typeof (INTran.costCenterID), typeof (INTran.siteID), typeof (Where<INTran.tranType, Equal<INTranType.invoice>, Or<INTran.tranType, Equal<INTranType.debitMemo>, Or<INTran.origModule, NotEqual<BatchModule.modulePO>, And<INTran.tranType, Equal<INTranType.issue>>>>>), typeof (Where<INTran.tranType, Equal<INTranType.receipt>, Or<INTran.tranType, Equal<INTranType.return_>, Or<INTran.tranType, Equal<INTranType.creditMemo>, Or<INTran.origModule, Equal<BatchModule.modulePO>, And<INTran.tranType, Equal<INTranType.issue>>>>>>), typeof (Where<INTran.tranType, Equal<INTranType.transfer>, And<INTran.invtMult, Equal<short1>, Or<INTran.tranType, Equal<INTranType.transfer>, And<INTran.invtMult, Equal<shortMinus1>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.locationID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXRestrictorAttribute))]
  [PXRestrictor(typeof (Where<PX.Objects.CS.ReasonCode.usage, Equal<Optional<INTran.docType>>, Or<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.vendorReturn>, And<Optional<INTran.origModule>, Equal<BatchModule.modulePO>>>>), "The usage type of the reason code does not match the document type.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.reasonCode> e)
  {
  }

  [LocationAvail(typeof (INTranSplit.inventoryID), typeof (INTranSplit.subItemID), typeof (INTran.costCenterID), typeof (INTranSplit.siteID), typeof (Where<INTranSplit.tranType, Equal<INTranType.invoice>, Or<INTranSplit.tranType, Equal<INTranType.debitMemo>, Or<INTranSplit.origModule, NotEqual<BatchModule.modulePO>, And<INTranSplit.tranType, Equal<INTranType.issue>>>>>), typeof (Where<INTranSplit.tranType, Equal<INTranType.receipt>, Or<INTranSplit.tranType, Equal<INTranType.return_>, Or<INTranSplit.tranType, Equal<INTranType.creditMemo>, Or<INTranSplit.origModule, Equal<BatchModule.modulePO>, And<INTranSplit.tranType, Equal<INTranType.issue>>>>>>), typeof (Where<INTranSplit.tranType, Equal<INTranType.transfer>, And<INTranSplit.invtMult, Equal<short1>, Or<INTranSplit.tranType, Equal<INTranType.transfer>, And<INTranSplit.invtMult, Equal<shortMinus1>>>>>))]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<INTranSplit.locationID> e)
  {
  }

  public INIssueEntry()
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    }
    OpenPeriodAttribute.SetValidatePeriod<INRegister.finPeriodID>(((PXSelectBase) this.issue).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    INTranType.IssueListAttribute issueListAttribute = new INTranType.IssueListAttribute();
    PXStringListAttribute.SetList<INTran.tranType>(((PXSelectBase) this.transactions).Cache, (object) null, issueListAttribute.AllowedValues, issueListAttribute.AllowedLabels);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<SiteStatusByCostCenter.negAvailQty>(INIssueEntry.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (INIssueEntry.\u003C\u003Ec.\u003C\u003E9__8_0 = new PXFieldDefaulting((object) INIssueEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__8_0))));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INRegister, INRegister.docType>, INRegister, object>) e).NewValue = (object) "I";
  }

  protected virtual void _(PX.Data.Events.RowInserting<INRegister> e)
  {
    if (!(e.Row.DocType == "0"))
      return;
    e.Cancel = true;
  }

  protected override void _(PX.Data.Events.RowUpdated<INRegister> e)
  {
    base._(e);
    bool? requireControlTotal = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    bool flag1 = false;
    if (requireControlTotal.GetValueOrDefault() == flag1 & requireControlTotal.HasValue)
    {
      this.FillControlValue<INRegister.controlAmount, INRegister.totalAmount>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
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
      this.RaiseControlValueError<INRegister.controlAmount, INRegister.totalAmount>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
      this.RaiseControlValueError<INRegister.controlQty, INRegister.totalQty>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<INRegister>>) e).Cache, e.Row);
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.tranType> e)
  {
    INRegister current = ((PXSelectBase<INRegister>) this.issue).Current;
    if ((current != null ? (!current.IsCorrection.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    string str = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.tranType>, INTran, object>) e).NewValue == "RCP" ? "The Receipt transaction type is used only in issues that are generated automatically for purchase receipt correction." : ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.tranType>, INTran, object>) e).NewValue == "ADJ" ? "The Adjustment transaction type is used only in issues that are generated automatically for purchase receipt correction." : (string) null);
    if (str != null)
      throw new PXSetPropertyException((IBqlTable) e.Row, str, (PXErrorLevel) 4);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = e.Row.Released;
    bool flag1 = false;
    bool flag2 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue && e.Row.OrigModule == "IN";
    if (!flag2)
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache, (object) e.Row, false);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowInsert = true;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    nullable = e.Row.Released;
    bool flag3 = false;
    int num1 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue ? 1 : 0;
    cache1.AllowUpdate = num1 != 0;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache.AllowDelete = flag2;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowInsert = flag2;
    PXSelectBase<INTran> lsselect = this.LineSplittingExt.lsselect;
    nullable = e.Row.Released;
    bool flag4 = false;
    int num2 = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 1 : 0;
    ((PXSelectBase) lsselect).AllowUpdate = num2 != 0;
    ((PXSelectBase) this.LineSplittingExt.lsselect).AllowDelete = flag2;
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row1 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    int num3 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.controlQty>(cache2, (object) row1, num3 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row2 = e.Row;
    nullable = ((PXSelectBase<INSetup>) this.insetup).Current.RequireControlTotal;
    int num4 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.controlAmount>(cache3, (object) row2, num4 != 0);
    PXCache cache4 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRegister>>) e).Cache;
    INRegister row3 = e.Row;
    nullable = e.Row.Released;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INRegister.totalCost>(cache4, (object) row3, num5 != 0);
    if (e.Row.DocType == "I")
      PXFormulaAttribute.SetAggregate<INTran.tranAmt>(((PXSelectBase) this.transactions).Cache, typeof (SumCalc<INRegister.totalAmount>), (Type) null);
    else
      PXFormulaAttribute.SetAggregate<INTran.tranAmt>(((PXSelectBase) this.transactions).Cache, (Type) null, (Type) null);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.docType>, INTran, object>) e).NewValue = (object) "I";
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.tranType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.tranType>, INTran, object>) e).NewValue = (object) "III";
  }

  protected override void _(PX.Data.Events.FieldUpdated<INTran, INTran.uOM> e)
  {
    this.DefaultUnitPrice(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.uOM>>) e).Cache, e.Row);
    base._(e);
  }

  protected override void _(PX.Data.Events.FieldUpdated<INTran, INTran.siteID> e)
  {
    this.DefaultUnitPrice(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.siteID>>) e).Cache, e.Row);
    base._(e);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.sOOrderNbr> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.sOOrderNbr>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTran, INTran.sOShipmentNbr> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.sOShipmentNbr>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode>, INTran, object>) e).NewValue);
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.reasonCode>>) e).Cancel = reasonCode != null && (EnumerableExtensions.IsNotIn<string>(e.Row.TranType, "III", "RET") && reasonCode.Usage == "S" || reasonCode.Usage == e.Row.DocType);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.locationID> e)
  {
    if (((PXSelectBase<INRegister>) this.issue).Current == null || !(((PXSelectBase<INRegister>) this.issue).Current.OrigModule != "IN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.locationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTran, INTran.lotSerialNbr> e)
  {
    if (((PXSelectBase<INRegister>) this.issue).Current == null || !(((PXSelectBase<INRegister>) this.issue).Current.OrigModule != "IN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.lotSerialNbr>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<INTran> e)
  {
    if (e.Row == null)
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache;
    INTran row1 = e.Row;
    short? invtMult1 = e.Row.InvtMult;
    int num1 = (invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<INTran.unitCost>(cache1, (object) row1, num1 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache;
    INTran row2 = e.Row;
    short? invtMult2 = e.Row.InvtMult;
    int num2 = (invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1 ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<INTran.tranCost>(cache2, (object) row2, num2 != 0);
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
    if (PXDBOperationExt.Command(e.Operation) != 1 || string.IsNullOrEmpty(e.Row.SOShipmentNbr))
      return;
    Decimal? qty1 = e.Row.Qty;
    Decimal? nullable1 = e.Row.OrigQty;
    if (PXDBQuantityAttribute.Round(new Decimal?((qty1.HasValue & nullable1.HasValue ? new Decimal?(qty1.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?()).Value)) > 0M)
    {
      PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache;
      INTran row = e.Row;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> qty2 = (ValueType) e.Row.Qty;
      object[] objArray = new object[1];
      Decimal? origQty = e.Row.OrigQty;
      Decimal? nullable2;
      if (!origQty.HasValue)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Decimal?(-origQty.GetValueOrDefault());
      objArray[0] = (object) nullable2;
      PXSetPropertyException propertyException = new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
      cache.RaiseExceptionHandling<INTran.qty>((object) row, (object) qty2, (Exception) propertyException);
    }
    else
    {
      Decimal? qty3 = e.Row.Qty;
      nullable1 = e.Row.OrigQty;
      if (!(PXDBQuantityAttribute.Round(new Decimal?((qty3.HasValue & nullable1.HasValue ? new Decimal?(qty3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?()).Value)) < 0M))
        return;
      PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<INTran>>) e).Cache;
      INTran row = e.Row;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> qty4 = (ValueType) e.Row.Qty;
      object[] objArray = new object[1];
      Decimal? origQty = e.Row.OrigQty;
      Decimal? nullable3;
      if (!origQty.HasValue)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(-origQty.GetValueOrDefault());
      objArray[0] = (object) nullable3;
      PXSetPropertyException propertyException = new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", objArray);
      cache.RaiseExceptionHandling<INTran.qty>((object) row, (object) qty4, (Exception) propertyException);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID> e)
  {
    if (((PXSelectBase<INRegister>) this.issue).Current == null || !(((PXSelectBase<INRegister>) this.issue).Current.OrigModule != "IN"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.lotSerialNbr> e)
  {
    if (((PXSelectBase<INRegister>) this.issue).Current == null || !(((PXSelectBase<INRegister>) this.issue).Current.OrigModule != "IN"))
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
    get => (PXSelectBase<INRegister>) this.issue;
  }

  public override PXSelectBase<INTran> INTranDataMember => (PXSelectBase<INTran>) this.transactions;

  public override PXSelectBase<INTran> LSSelectDataMember => this.LineSplittingExt.lsselect;

  public override PXSelectBase<INTranSplit> INTranSplitDataMember
  {
    get => (PXSelectBase<INTranSplit>) this.splits;
  }

  protected override string ScreenID => "IN302000";

  public INIssueEntry.LineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<INIssueEntry.LineSplittingExtension>();
  }

  public INIssueEntry.ItemAvailabilityExtension ItemAvailabilityExt
  {
    get => ((PXGraph) this).FindImplementation<INIssueEntry.ItemAvailabilityExtension>();
  }

  public class SiteStatusLookup : INSiteStatusLookupExt<INIssueEntry>
  {
    protected override bool IsAddItemEnabled(INRegister doc)
    {
      return ((PXSelectBase) this.LSSelect).AllowDelete;
    }
  }

  public class LineSplittingExtension : INRegisterLineSplittingExtension<INIssueEntry>
  {
  }

  public class ItemAvailabilityExtension : INRegisterItemAvailabilityExtension<INIssueEntry>
  {
  }
}
