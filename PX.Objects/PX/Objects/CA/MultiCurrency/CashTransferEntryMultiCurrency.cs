// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultiCurrency.CashTransferEntryMultiCurrency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CA.MultiCurrency;

public sealed class CashTransferEntryMultiCurrency : 
  MultiCurrencyGraph<
  #nullable disable
  CashTransferEntry, CATransfer>
{
  protected int? AccountProcessing;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo> currencyinfoout;

  protected override string Module => "CA";

  protected override MultiCurrencyGraph<CashTransferEntry, CATransfer>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<CashTransferEntry, CATransfer>.CurySourceMapping(typeof (CashAccount))
    {
      CuryID = typeof (CashAccount.curyID),
      CuryRateTypeID = typeof (CashAccount.curyRateTypeID),
      AllowOverrideCury = typeof (CashAccount.allowOverrideCury),
      AllowOverrideRate = typeof (CashAccount.allowOverrideRate)
    };
  }

  protected override MultiCurrencyGraph<CashTransferEntry, CATransfer>.DocumentMapping GetDocumentMapping()
  {
    return new MultiCurrencyGraph<CashTransferEntry, CATransfer>.DocumentMapping(typeof (CATransfer))
    {
      CuryID = typeof (CATransfer.inCuryID),
      CuryInfoID = typeof (CATransfer.inCuryInfoID),
      BAccountID = typeof (CATransfer.inAccountID),
      DocumentDate = typeof (CATransfer.inDate),
      BranchID = typeof (CATransfer.inBranchID)
    };
  }

  protected override PXSelectBase[] GetChildren()
  {
    return new PXSelectBase[4]
    {
      (PXSelectBase) this.Base.Transfer,
      (PXSelectBase) this.Base.Expenses,
      (PXSelectBase) this.Base.ExpenseTaxes,
      (PXSelectBase) this.Base.ExpenseTaxTrans
    };
  }

  protected override PX.Objects.Extensions.MultiCurrency.CurySource CurrentSourceSelect()
  {
    return PXResultset<PX.Objects.Extensions.MultiCurrency.CurySource>.op_Implicit(((PXSelectBase<PX.Objects.Extensions.MultiCurrency.CurySource>) this.CurySource).Select(new object[1]
    {
      (object) this.AccountProcessing
    }));
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
  public override object GetRow(PXCache sender, object row) => row ?? sender.Current;

  protected IEnumerable currencyInfoOut()
  {
    CashTransferEntryMultiCurrency entryMultiCurrency = this;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CATransfer.outCuryInfoID>>>>.Config>.Select((PXGraph) entryMultiCurrency.Base, Array.Empty<object>()));
    if (currencyInfo != null)
    {
      currencyInfo.IsReadOnly = new bool?(!((PXGraph) entryMultiCurrency.Base).UnattendedMode && !((PXSelectBase) entryMultiCurrency.Documents).AllowUpdate);
      yield return (object) currencyInfo;
    }
  }

  [PXMergeAttributes]
  [CurrencyInfo(typeof (PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID))]
  protected void _(Events.CacheAttached<CATransfer.outCuryInfoID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency")]
  protected void _(Events.CacheAttached<CATransfer.outCuryID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Currency")]
  protected void _(Events.CacheAttached<CATransfer.inCuryID> e)
  {
  }

  protected override void _(Events.RowInserting<Document> e)
  {
    this.AccountProcessing = (int?) ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<Document>>) e).Cache.GetValue<CATransfer.inAccountID>((object) e.Row);
    base._(e);
  }

  protected void _(Events.RowInserting<CATransfer> e)
  {
    this.AccountProcessing = e.Row.OutAccountID;
    this.DocumentRowInserting<CATransfer.outCuryInfoID, CATransfer.outCuryID>(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CATransfer>>) e).Cache, (object) e.Row);
    PXCache cache = ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CATransfer>>) e).Cache;
    CATransfer row = e.Row;
    Decimal? tranIn = e.Row.TranIn;
    Decimal? tranOut = e.Row.TranOut;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (tranIn.HasValue & tranOut.HasValue ? new Decimal?(tranIn.GetValueOrDefault() - tranOut.GetValueOrDefault()) : new Decimal?());
    cache.SetValueExt<CATransfer.rGOLAmt>((object) row, (object) local);
  }

  protected void _(
    Events.FieldUpdated<CATransfer, CATransfer.inAccountID> e)
  {
    this.AccountProcessing = e.Row.InAccountID;
    this.SourceFieldUpdated<CATransfer.inCuryInfoID, CATransfer.inCuryID, CATransfer.inDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CATransfer, CATransfer.inAccountID>>) e).Cache, (IBqlTable) e.Row);
  }

  protected void _(
    Events.FieldUpdated<CATransfer, CATransfer.outAccountID> e)
  {
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CATransfer, CATransfer.outAccountID>>) e).Cache.SetDefaultExt<CATransfer.outBranchID>((object) e.Row);
    if (((PXSelectBase<Document>) this.Documents).Current != null)
      ((PXSelectBase<Document>) this.Documents).Current.BranchID = e.Row.OutBranchID;
    this.AccountProcessing = e.Row.OutAccountID;
    this.SourceFieldUpdated<CATransfer.outCuryInfoID, CATransfer.outCuryID, CATransfer.outDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CATransfer, CATransfer.outAccountID>>) e).Cache, (IBqlTable) e.Row);
    this.AccountProcessing = e.Row.InAccountID;
    this.SourceFieldUpdated<CATransfer.inCuryInfoID, CATransfer.inCuryID, CATransfer.inDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CATransfer, CATransfer.outAccountID>>) e).Cache, (IBqlTable) e.Row);
  }

  protected void _(Events.FieldUpdated<CATransfer.curyTranIn> e)
  {
    CATransfer row = (CATransfer) e.Row;
    PXCache cache = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CATransfer.curyTranIn>>) e).Cache;
    CATransfer caTransfer = row;
    Decimal? tranIn = row.TranIn;
    Decimal? tranOut = row.TranOut;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (tranIn.HasValue & tranOut.HasValue ? new Decimal?(tranIn.GetValueOrDefault() - tranOut.GetValueOrDefault()) : new Decimal?());
    cache.SetValueExt<CATransfer.rGOLAmt>((object) caTransfer, (object) local);
  }

  protected void _(
    Events.FieldSelecting<CATransfer, CATransfer.outCuryID> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<CATransfer, CATransfer.outCuryID>>) e).ReturnValue = (object) this.CuryIDFieldSelecting<CATransfer.outCuryInfoID>(((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CATransfer, CATransfer.outCuryID>>) e).Cache, (object) e.Row);
  }

  protected void _(
    Events.FieldUpdated<CATransfer, CATransfer.outDate> e)
  {
    this.DateFieldUpdated<CATransfer.outCuryInfoID, CATransfer.outDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CATransfer, CATransfer.outDate>>) e).Cache, (IBqlTable) e.Row);
  }

  protected void _(
    Events.FieldSelecting<CATransfer, CashTransferEntryMultiCurrency.CATransferMultiCurrency.outCuryRate> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<CATransfer, CashTransferEntryMultiCurrency.CATransferMultiCurrency.outCuryRate>>) e).ReturnValue = (object) this.CuryRateFieldSelecting<CATransfer.outCuryInfoID>(((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CATransfer, CashTransferEntryMultiCurrency.CATransferMultiCurrency.outCuryRate>>) e).Cache, (object) e.Row);
  }

  protected override void _(Events.RowUpdating<Document> e)
  {
    this.AccountProcessing = (int?) ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<Document>>) e).Cache.GetValue<CATransfer.inAccountID>((object) e.Row);
    base._(e);
  }

  protected void _(Events.RowUpdating<CATransfer> e)
  {
    this.AccountProcessing = e.Row.OutAccountID;
    this.DocumentRowUpdating<CATransfer.outCuryInfoID, CATransfer.outCuryID>(((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CATransfer>>) e).Cache, (object) e.NewRow);
    Decimal? tranOut1 = e.NewRow.TranOut;
    Decimal? tranOut2 = e.Row.TranOut;
    if (tranOut1.GetValueOrDefault() == tranOut2.GetValueOrDefault() & tranOut1.HasValue == tranOut2.HasValue)
    {
      DateTime? inDate1 = e.NewRow.InDate;
      DateTime? inDate2 = e.Row.InDate;
      if ((inDate1.HasValue == inDate2.HasValue ? (inDate1.HasValue ? (inDate1.GetValueOrDefault() != inDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(e.NewRow.InCuryID != e.Row.InCuryID))
      {
        int? inAccountId1 = e.NewRow.InAccountID;
        int? inAccountId2 = e.Row.InAccountID;
        if (inAccountId1.GetValueOrDefault() == inAccountId2.GetValueOrDefault() & inAccountId1.HasValue == inAccountId2.HasValue)
          return;
      }
    }
    if (((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CATransfer>>) e).Cache.Graph.IsCopyPasteContext)
      return;
    this.CalcTranIn(e.NewRow);
  }

  protected override void _(Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    base._(e);
    if (((PXSelectBase<CATransfer>) this.Base.Transfer).Current == null)
      return;
    long? inCuryInfoId = ((PXSelectBase<CATransfer>) this.Base.Transfer).Current.InCuryInfoID;
    long? curyInfoId1 = e.Row.CuryInfoID;
    if (!(inCuryInfoId.GetValueOrDefault() == curyInfoId1.GetValueOrDefault() & inCuryInfoId.HasValue == curyInfoId1.HasValue))
    {
      long? outCuryInfoId = ((PXSelectBase<CATransfer>) this.Base.Transfer).Current.OutCuryInfoID;
      long? curyInfoId2 = e.Row.CuryInfoID;
      if (!(outCuryInfoId.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & outCuryInfoId.HasValue == curyInfoId2.HasValue))
        return;
    }
    this.CalcTranIn(((PXSelectBase<CATransfer>) this.Base.Transfer).Current);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transfer).Cache, (object) ((PXSelectBase<CATransfer>) this.Base.Transfer).Current);
  }

  protected void CalcTranIn(CATransfer row)
  {
    if (row.OutCuryID == row.InCuryID)
    {
      row.CuryTranIn = row.CuryTranOut;
      row.TranIn = row.TranOut;
    }
    else
    {
      try
      {
        PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.GetDefaultCurrencyInfo();
        row.CuryTranIn = new Decimal?(defaultCurrencyInfo.CuryConvCury(row.TranOut.GetValueOrDefault()));
        CATransfer caTransfer1 = row;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
        Decimal? nullable1 = row.CuryTranIn;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        Decimal? nullable2 = new Decimal?(currencyInfo.CuryConvBase(valueOrDefault));
        caTransfer1.TranIn = nullable2;
        CATransfer caTransfer2 = row;
        nullable1 = row.TranIn;
        Decimal? tranOut = row.TranOut;
        Decimal? nullable3 = nullable1.HasValue & tranOut.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - tranOut.GetValueOrDefault()) : new Decimal?();
        caTransfer2.RGOLAmt = nullable3;
      }
      catch (PXRateNotFoundException ex)
      {
      }
    }
  }

  protected override void _(Events.RowSelected<Document> e)
  {
    base._(e);
    PXUIFieldAttribute.SetEnabled<Document.curyID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Document>>) e).Cache, (object) e.Row, false);
  }

  protected void _(Events.RowSelected<CATransfer> e)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<CATransfer.inCuryID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CATransfer>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetVisible<CATransfer.outCuryID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CATransfer>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetVisible<CATransfer.rGOLAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CATransfer>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetVisible<CATransfer.baseCuryID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CATransfer>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<CATransfer.curyTranOut>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CATransfer>>) e).Cache, (object) e.Row, !((PXGraph) this.Base).Accessinfo.CuryViewState);
    PXUIFieldAttribute.SetEnabled<CATransfer.curyTranIn>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CATransfer>>) e).Cache, (object) e.Row, !((PXGraph) this.Base).Accessinfo.CuryViewState);
    if (!(e.Row?.OutCuryID == e.Row?.InCuryID))
      return;
    PXUIFieldAttribute.SetEnabled<CATransfer.curyTranIn>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CATransfer>>) e).Cache, (object) e.Row, false);
  }

  protected void _(Events.RowPersisting<CATransfer> e)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PX.Objects.CM.Extensions.CurrencyInfoAttribute.GetCurrencyInfo<CATransfer.outCuryInfoID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CATransfer>>) e).Cache, (object) e.Row);
    if (currencyInfo1 == null)
      currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<CATransfer.outCuryInfoID>>, And<PX.Objects.CM.Extensions.CurrencyInfo.curyID, Equal<Required<CATransfer.outCuryID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) e.Row.OutCuryInfoID,
        (object) e.Row.OutCuryInfoID
      }));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = PX.Objects.CM.Extensions.CurrencyInfoAttribute.GetCurrencyInfo<CATransfer.inCuryInfoID>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CATransfer>>) e).Cache, (object) e.Row);
    if (currencyInfo3 == null)
      currencyInfo3 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<CATransfer.inCuryInfoID>>, And<PX.Objects.CM.Extensions.CurrencyInfo.curyID, Equal<Required<CATransfer.inCuryID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) e.Row.InCuryInfoID,
        (object) e.Row.InCuryInfoID
      }));
    if ((currencyInfo2 != null ? (!currencyInfo2.CuryRate.HasValue ? 1 : 0) : 1) != 0)
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CATransfer>>) e).Cache.RaiseExceptionHandling<CATransfer.outCuryID>((object) e.Row, (object) e.Row.OutCuryID, (Exception) new PXSetPropertyException("Currency Rate is not defined."));
    if ((currencyInfo3 != null ? (!currencyInfo3.CuryRate.HasValue ? 1 : 0) : 1) == 0)
      return;
    ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CATransfer>>) e).Cache.RaiseExceptionHandling<CATransfer.inCuryID>((object) e.Row, (object) e.Row.InCuryID, (Exception) new PXSetPropertyException("Currency Rate is not defined."));
  }

  [PXOverride]
  public void SwapInOutFields(
    CATransfer currentTransfer,
    CATransfer reverseTransfer,
    Action<CATransfer, CATransfer> del)
  {
    del(currentTransfer, reverseTransfer);
    long? outCuryInfoId = reverseTransfer.OutCuryInfoID;
    reverseTransfer.OutCuryInfoID = reverseTransfer.InCuryInfoID;
    reverseTransfer.InCuryInfoID = outCuryInfoId;
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Current = (PX.Objects.CM.Extensions.CurrencyInfo) null;
    this.AccountProcessing = new int?();
  }

  [PXMergeAttributes]
  [CurrencyInfo(typeof (PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID))]
  protected void _(Events.CacheAttached<CAExpense.curyInfoID> e)
  {
  }

  protected void _(Events.RowInserting<CAExpense> e)
  {
    this.AccountProcessing = e.Row.CashAccountID;
    this.DocumentRowInserting<CAExpense.curyInfoID, CAExpense.curyID>(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CAExpense>>) e).Cache, (object) e.Row);
  }

  protected void _(Events.RowUpdating<CAExpense> e)
  {
    this.AccountProcessing = e.Row.CashAccountID;
    this.DocumentRowUpdating<CAExpense.curyInfoID, CAExpense.curyID>(((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CAExpense>>) e).Cache, (object) e.Row);
  }

  protected void _(
    Events.FieldUpdated<CAExpense, CAExpense.cashAccountID> e)
  {
    this.AccountProcessing = e.Row.CashAccountID;
    this.SourceFieldUpdated<CAExpense.curyInfoID, CAExpense.curyID, CAExpense.tranDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CAExpense, CAExpense.cashAccountID>>) e).Cache, (IBqlTable) e.Row);
  }

  protected void _(
    Events.FieldSelecting<CAExpense, CAExpense.curyID> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<CAExpense, CAExpense.curyID>>) e).ReturnValue = (object) this.CuryIDFieldSelecting<CAExpense.curyInfoID>(((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CAExpense, CAExpense.curyID>>) e).Cache, (object) e.Row);
  }

  protected void _(
    Events.FieldUpdated<CAExpense, CAExpense.adjCuryRate> e)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<CAExpense.curyInfoID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) e.Row.CuryInfoID
    }));
    if (!(e.Row.CuryID != currencyInfo1.BaseCuryID))
      return;
    currencyInfo1.CuryRate = e.Row.AdjCuryRate;
    currencyInfo1.RecipRate = new Decimal?(Math.Round(1M / e.Row.AdjCuryRate.Value, 8, MidpointRounding.AwayFromZero));
    currencyInfo1.CuryMultDiv = "M";
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
    Decimal? adjCuryRate = e.Row.AdjCuryRate;
    Decimal? nullable1 = new Decimal?(Math.Round(adjCuryRate.Value, 8, MidpointRounding.AwayFromZero));
    currencyInfo2.CuryRate = nullable1;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo1;
    adjCuryRate = e.Row.AdjCuryRate;
    Decimal? nullable2 = new Decimal?(Math.Round(adjCuryRate.Value, 8, MidpointRounding.AwayFromZero));
    currencyInfo3.RecipRate = nullable2;
    if (((PXSelectBase) this.currencyinfo).Cache.GetStatus((object) currencyInfo1) != null && ((PXSelectBase) this.currencyinfo).Cache.GetStatus((object) currencyInfo1) != 5)
      return;
    ((PXSelectBase) this.currencyinfo).Cache.SetStatus((object) currencyInfo1, (PXEntryStatus) 1);
  }

  protected void _(
    Events.FieldVerifying<CAExpense, CAExpense.adjCuryRate> e)
  {
    Decimal? newValue = (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<CAExpense, CAExpense.adjCuryRate>, CAExpense, object>) e).NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() <= num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected void _(
    Events.FieldSelecting<CAExpense, CAExpense.adjCuryRate> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<CAExpense, CAExpense.adjCuryRate>>) e).ReturnValue = (object) this.CuryRateFieldSelecting<CAExpense.curyInfoID>(((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CAExpense, CAExpense.adjCuryRate>>) e).Cache, (object) e.Row);
  }

  protected void _(Events.RowSelected<CAExpense> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<CAExpense.curyInfoID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) e.Row.CuryInfoID
    }));
    PXUIFieldAttribute.SetEnabled<CAExpense.adjCuryRate>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CAExpense>>) e).Cache, (object) e.Row, e.Row.CuryID != currencyInfo?.BaseCuryID);
    PXUIFieldAttribute.SetEnabled<CAExpense.curyTaxableAmt>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CAExpense>>) e).Cache, (object) e.Row, !((PXGraph) this.Base).Accessinfo.CuryViewState);
  }

  protected void _(
    Events.FieldUpdated<CAExpense, CAExpense.tranDate> e)
  {
    this.AccountProcessing = e.Row.CashAccountID;
    this.DateFieldUpdated<CAExpense.curyInfoID, CAExpense.tranDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CAExpense, CAExpense.tranDate>>) e).Cache, (IBqlTable) e.Row);
  }

  [PXNonInstantiatedExtension]
  public sealed class CATransferMultiCurrency : PXCacheExtension<CATransfer>
  {
    [PXDecimal]
    public Decimal? OutCuryRate { get; set; }

    public abstract class outCuryRate : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CashTransferEntryMultiCurrency.CATransferMultiCurrency.outCuryRate>
    {
    }
  }
}
