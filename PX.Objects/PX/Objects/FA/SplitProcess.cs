// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.SplitProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.FA;

public class SplitProcess : PXGraph<
#nullable disable
SplitProcess>
{
  public PXCancel<SplitProcess.SplitFilter> Cancel;
  public PXFilter<SplitProcess.SplitFilter> Filter;
  public PXFilteredProcessing<SplitParams, SplitProcess.SplitFilter> Splits;
  public PXSelectJoin<Numbering, InnerJoin<FASetup, On<FASetup.assetNumberingID, Equal<Numbering.numberingID>>>> assetNumbering;
  public PXSetup<FASetup> fasetup;
  public PXAction<SplitProcess.SplitFilter> viewAsset;

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [InjectDependency]
  public IFABookPeriodUtils FABookPeriodUtils { get; set; }

  public SplitProcess()
  {
    FASetup current = ((PXSelectBase<FASetup>) this.fasetup).Current;
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault())
      throw new PXSetupNotEnteredException<FASetup>("This operation is not available in initialization mode. To exit the initialization mode, select the '{1}' checkbox on the '{0}' screen.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FASetup.updateGL>(((PXSelectBase) this.fasetup).Cache)
      });
  }

  public IEnumerable splits(PXAdapter adapter) => ((PXSelectBase) this.Splits).Cache.Inserted;

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable cancel(PXAdapter adapter)
  {
    SplitProcess splitProcess = this;
    IEnumerator enumerator = ((PXAction) new PXCancel<SplitProcess.SplitFilter>((PXGraph) splitProcess, "Cancel")).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        yield return ((PXSelectBase) splitProcess.Filter).Cache.Insert();
        yield break;
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    enumerator = (IEnumerator) null;
  }

  public void SplitFilter_SplitDate_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    SplitProcess.SplitFilter row = (SplitProcess.SplitFilter) e.Row;
    if (row == null)
      return;
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<SplitProcess.SplitFilter.assetID>>>, OrderBy<Desc<FABookBalance.updateGL>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (faBookBalance != null)
    {
      if (string.IsNullOrEmpty(faBookBalance.CurrDeprPeriod) && !string.IsNullOrEmpty(faBookBalance.LastDeprPeriod))
      {
        e.NewValue = (object) this.FABookPeriodUtils.GetFABookPeriodStartDate(this.FABookPeriodUtils.PeriodPlusPeriodsCount(faBookBalance.LastDeprPeriod, 1, faBookBalance.BookID, faBookBalance.AssetID), faBookBalance.BookID, faBookBalance.AssetID);
      }
      else
      {
        FABookPeriod bookPeriodOfDate = this.FABookPeriodRepository.FindFABookPeriodOfDate(((PXGraph) this).Accessinfo.BusinessDate, faBookBalance.BookID, faBookBalance.AssetID);
        e.NewValue = (object) (string.CompareOrdinal(faBookBalance.CurrDeprPeriod, bookPeriodOfDate.FinPeriodID) > 0 ? new DateTime?(this.FABookPeriodUtils.GetFABookPeriodStartDate(faBookBalance.CurrDeprPeriod, faBookBalance.BookID, faBookBalance.AssetID)) : ((PXGraph) this).Accessinfo.BusinessDate);
      }
    }
    else
      e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  public void _(
    PX.Data.Events.FieldVerifying<SplitProcess.SplitFilter.splitDate> e)
  {
    SplitProcess.SplitFilter row = (SplitProcess.SplitFilter) e.Row;
    if (row == null)
      return;
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    }));
    if (faDetails == null)
      return;
    DateTime? receiptDate = faDetails.ReceiptDate;
    if (!receiptDate.HasValue)
      return;
    receiptDate = faDetails.ReceiptDate;
    DateTime dateTime = receiptDate.Value;
    if (dateTime.CompareTo((object) (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SplitProcess.SplitFilter.splitDate>, object, object>) e).NewValue) > 0)
    {
      object[] objArray = new object[1];
      receiptDate = faDetails.ReceiptDate;
      dateTime = receiptDate.Value;
      objArray[0] = (object) dateTime.ToShortDateString();
      throw new PXSetPropertyException("The fixed asset cannot be split on a date earlier than the purchase date {0}.", objArray);
    }
  }

  public void _(PX.Data.Events.RowUpdated<SplitProcess.SplitFilter> e)
  {
    if (e.Row == null || e.Row.DeprBeforeSplit.GetValueOrDefault() || !string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<SplitProcess.SplitFilter.splitDate>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SplitProcess.SplitFilter>>) e).Cache, (object) e.Row)))
      return;
    foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.AssetID
    }))
    {
      FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
      string str1 = this.FABookPeriodUtils.PeriodPlusPeriodsCount(this.FABookPeriodRepository.FindFABookPeriodOfDate(e.Row.SplitDate, faBookBalance.BookID, faBookBalance.AssetID).FinPeriodID, -1, faBookBalance.BookID, faBookBalance.AssetID);
      string period = faBookBalance.LastDeprPeriod ?? faBookBalance.DeprFromPeriod;
      if ((period != null ? (period.CompareTo(str1) != 0 ? 1 : 0) : 1) != 0)
      {
        string str2 = FinPeriodIDFormattingAttribute.FormatForError(str1);
        string str3 = FinPeriodIDFormattingAttribute.FormatForError(period);
        FABook faBook = FABook.PK.Find((PXGraph) this, faBookBalance.BookID);
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SplitProcess.SplitFilter>>) e).Cache.RaiseExceptionHandling<SplitProcess.SplitFilter.splitDate>((object) e.Row, (object) e.Row.SplitDate, (Exception) new PXSetPropertyException("The fixed asset has not been depreciated from {0} for the {1} book. The depreciation will be suspended from {0} to {2} for the {1} book.", (PXErrorLevel) 2, new object[3]
        {
          (object) str3,
          (object) faBook?.BookCode,
          (object) str2
        }));
        break;
      }
    }
  }

  public void SplitFilter_AssetID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SplitProcess.SplitFilter.cost>(e.Row);
    sender.SetDefaultExt<SplitProcess.SplitFilter.qty>(e.Row);
    sender.SetDefaultExt<SplitProcess.SplitFilter.splitDate>(e.Row);
    sender.SetDefaultExt<SplitProcess.SplitFilter.splitPeriodID>(e.Row);
    ((PXSelectBase) this.Splits).Cache.IsDirty = false;
    ((PXSelectBase) this.Splits).Cache.Clear();
  }

  public void SplitFilter_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ((PXSelectBase) this.Splits).Cache.IsDirty = false;
    ((PXSelectBase) this.Splits).Cache.Clear();
  }

  public void SplitFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SplitProcess.SplitFilter row = (SplitProcess.SplitFilter) e.Row;
    if (row == null)
      return;
    PXFilteredProcessing<SplitParams, SplitProcess.SplitFilter> splits = this.Splits;
    string action = row.Action;
    object[] objArray = new object[10];
    objArray[0] = (object) row.SplitDate;
    objArray[1] = (object) row.SplitPeriodID;
    objArray[7] = (object) row.DeprBeforeSplit;
    objArray[9] = (object) row.AssetID;
    ((PXProcessingBase<SplitParams>) splits).SetProcessWorkflowAction(action, objArray);
    ((PXProcessing<SplitParams>) this.Splits).SetProcessVisible(false);
    ((PXProcessing<SplitParams>) this.Splits).SetProcessAllEnabled(row.AssetID.HasValue && row.SplitPeriodID != null && string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<SplitProcess.SplitFilter.splitDate>(sender, (object) row)));
    ((PXProcessing<SplitParams>) this.Splits).SetProcessAllCaption("Split");
    PXCache cache1 = ((PXSelectBase) this.Splits).Cache;
    int? assetId = row.AssetID;
    int num1 = assetId.HasValue ? 1 : 0;
    cache1.AllowInsert = num1 != 0;
    PXCache cache2 = ((PXSelectBase) this.Splits).Cache;
    assetId = row.AssetID;
    int num2 = assetId.HasValue ? 1 : 0;
    cache2.AllowUpdate = num2 != 0;
    ((PXSelectBase) this.Splits).Cache.AllowDelete = true;
    PXUIFieldAttribute.SetEnabled<SplitParams.cost>(((PXSelectBase) this.Splits).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SplitParams.splittedQty>(((PXSelectBase) this.Splits).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<SplitParams.ratio>(((PXSelectBase) this.Splits).Cache, (object) null, true);
    Numbering numbering = PXResultset<Numbering>.op_Implicit(((PXSelectBase<Numbering>) this.assetNumbering).Select(Array.Empty<object>()));
    PXCache cache3 = ((PXSelectBase) this.Splits).Cache;
    bool? nullable;
    int num3;
    if (numbering != null)
    {
      nullable = numbering.UserNumbering;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 1;
    PXUIFieldAttribute.SetEnabled<SplitParams.splittedAssetCD>(cache3, (object) null, num3 != 0);
    PXCache pxCache1 = sender;
    SplitProcess.SplitFilter splitFilter1 = row;
    nullable = ((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseDepr;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SplitProcess.SplitFilter.deprBeforeSplit>(pxCache1, (object) splitFilter1, num4 != 0);
    FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<SplitProcess.SplitFilter.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    }));
    PXCache pxCache2 = sender;
    SplitProcess.SplitFilter splitFilter2 = row;
    int num5;
    if (fixedAsset != null)
    {
      nullable = fixedAsset.Depreciable;
      num5 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 1;
    PXUIFieldAttribute.SetVisible<SplitProcess.SplitFilter.deprBeforeSplit>(pxCache2, (object) splitFilter2, num5 != 0);
  }

  public void SplitFilter_AssetID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    SplitProcess.SplitFilter row = (SplitProcess.SplitFilter) e.Row;
    FATran faTran = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Required<SplitProcess.SplitFilter.assetID>>, And<FATran.released, NotEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    FixedAsset fixedAsset1 = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      e.NewValue
    }));
    if (faTran != null)
    {
      if (row.AssetID.HasValue)
      {
        FixedAsset fixedAsset2 = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<SplitProcess.SplitFilter.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.AssetID
        }));
        e.NewValue = (object) fixedAsset2.AssetCD;
      }
      else
        e.NewValue = (object) null;
      throw new PXSetPropertyException("The {0} fixed asset contains unreleased transactions. Release them to continue processing the asset.", new object[1]
      {
        (object) fixedAsset1.AssetCD
      });
    }
  }

  public void SplitFilter_CurrPeriodID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (string.IsNullOrEmpty((string) e.NewValue))
      throw new PXSetPropertyException("Fixed asset not acquired or fully depreciated.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<SplitParams> e)
  {
    (Decimal MinValue, Decimal MaxValue) = AssetMaint.GetSignedRange(((SplitProcess.SplitFilter) ((PXCache) GraphHelper.Caches<SplitProcess.SplitFilter>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SplitParams>>) e).Cache.Graph)).Current).Cost);
    PXCacheEx.Adjust<PXDBDecimalAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SplitParams>>) e).Cache, (object) e.Row).For<SplitParams.cost>((Action<PXDBDecimalAttribute>) (decimalAttr =>
    {
      decimalAttr.MinValue = (double) MinValue;
      decimalAttr.MaxValue = (double) MaxValue;
    }));
  }

  protected void SplitParams_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    FixedAsset fixedAsset = (FixedAsset) PXSelectorAttribute.Select<SplitProcess.SplitFilter.assetID>(((PXSelectBase) this.Filter).Cache, (object) ((PXSelectBase<SplitProcess.SplitFilter>) this.Filter).Current);
    if (fixedAsset == null)
      return;
    SplitParams row = (SplitParams) e.Row;
    Guid? noteId = row.NoteID;
    PXCache<FixedAsset>.RestoreCopy((FixedAsset) row, fixedAsset);
    row.NoteID = noteId;
    ((PXSelectBase) this.Filter).Cache.RaiseRowUpdated(((PXSelectBase) this.Filter).Cache.Current, ((PXSelectBase) this.Filter).Cache.Current);
  }

  public void SplitParams_Cost_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    (Decimal MinValue, Decimal MaxValue) range;
    if (((PXSelectBase<SplitProcess.SplitFilter>) this.Filter).Current != null && e.NewValue is Decimal? && !AssetMaint.IsValueInSignedRange((Decimal?) e.NewValue, ((PXSelectBase<SplitProcess.SplitFilter>) this.Filter).Current.Cost, out range))
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0} and less than or equal to {1}.", new object[2]
      {
        (object) range.MinValue,
        (object) range.MaxValue
      });
  }

  public void SplitParams_SplittedAssetCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    Numbering numbering = PXResultset<Numbering>.op_Implicit(((PXSelectBase<Numbering>) this.assetNumbering).Select(Array.Empty<object>()));
    if (string.IsNullOrEmpty((string) e.NewValue) && numbering.UserNumbering.GetValueOrDefault())
      throw new PXSetPropertyException("Cannot create the fixed asset. To create a fixed asset, specify the Asset ID or clear the Manual Numbering check box on the Numbering Sequences (CS201010) form for the fixed asset sequence.");
  }

  public void SplitParams_Cost_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SplitParams row = (SplitParams) e.Row;
    SplitProcess.SplitFilter current = ((PXSelectBase<SplitProcess.SplitFilter>) this.Filter).Current;
    if (row == null || current == null)
      return;
    Decimal? nullable1 = current.Cost;
    if (nullable1.GetValueOrDefault() == 0M)
      return;
    nullable1 = row.SplittedQty;
    if (nullable1.HasValue)
    {
      nullable1 = row.SplittedQty;
      Decimal num = 0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        goto label_5;
    }
    Decimal? cost1 = row.Cost;
    Decimal? qty = current.Qty;
    nullable1 = cost1.HasValue & qty.HasValue ? new Decimal?(cost1.GetValueOrDefault() * qty.GetValueOrDefault()) : new Decimal?();
    Decimal? cost2 = current.Cost;
    object obj1 = (object) (nullable1.HasValue & cost2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / cost2.GetValueOrDefault()) : new Decimal?());
    sender.RaiseFieldUpdating<SplitParams.splittedQty>((object) row, ref obj1);
    row.SplittedQty = (Decimal?) obj1;
label_5:
    Decimal? cost3 = row.Cost;
    Decimal num1 = (Decimal) 100;
    Decimal? nullable2 = cost3.HasValue ? new Decimal?(cost3.GetValueOrDefault() * num1) : new Decimal?();
    nullable1 = current.Cost;
    object obj2 = (object) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?());
    sender.RaiseFieldUpdating<SplitParams.ratio>((object) row, ref obj2);
    row.Ratio = (Decimal?) obj2;
  }

  public void SplitParams_SplittedQty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SplitParams row = (SplitParams) e.Row;
    SplitProcess.SplitFilter current = ((PXSelectBase<SplitProcess.SplitFilter>) this.Filter).Current;
    if (row == null || current == null)
      return;
    Decimal? splittedQty1 = row.SplittedQty;
    Decimal? cost = current.Cost;
    Decimal? nullable1 = splittedQty1.HasValue & cost.HasValue ? new Decimal?(splittedQty1.GetValueOrDefault() * cost.GetValueOrDefault()) : new Decimal?();
    Decimal? qty1 = current.Qty;
    object obj1 = (object) (nullable1.HasValue & qty1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / qty1.GetValueOrDefault()) : new Decimal?());
    sender.RaiseFieldUpdating<SplitParams.cost>((object) row, ref obj1);
    row.Cost = (Decimal?) obj1;
    Decimal? splittedQty2 = row.SplittedQty;
    Decimal num = (Decimal) 100;
    Decimal? nullable2 = splittedQty2.HasValue ? new Decimal?(splittedQty2.GetValueOrDefault() * num) : new Decimal?();
    Decimal? qty2 = current.Qty;
    object obj2 = (object) (nullable2.HasValue & qty2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / qty2.GetValueOrDefault()) : new Decimal?());
    sender.RaiseFieldUpdating<SplitParams.ratio>((object) row, ref obj2);
    row.Ratio = (Decimal?) obj2;
  }

  public void SplitParams_Ratio_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SplitParams row = (SplitParams) e.Row;
    SplitProcess.SplitFilter current = ((PXSelectBase<SplitProcess.SplitFilter>) this.Filter).Current;
    if (row == null || current == null)
      return;
    Decimal? nullable = row.SplittedQty;
    if (nullable.HasValue)
    {
      nullable = row.SplittedQty;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        goto label_4;
    }
    Decimal? ratio1 = row.Ratio;
    Decimal? qty = current.Qty;
    nullable = ratio1.HasValue & qty.HasValue ? new Decimal?(ratio1.GetValueOrDefault() * qty.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = (Decimal) 100;
    object obj1 = (object) (nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() / num1) : new Decimal?());
    sender.RaiseFieldUpdating<SplitParams.splittedQty>((object) row, ref obj1);
    row.SplittedQty = (Decimal?) obj1;
label_4:
    Decimal? ratio2 = row.Ratio;
    Decimal? cost = current.Cost;
    nullable = ratio2.HasValue & cost.HasValue ? new Decimal?(ratio2.GetValueOrDefault() * cost.GetValueOrDefault()) : new Decimal?();
    Decimal num2 = (Decimal) 100;
    object obj2 = (object) (nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() / num2) : new Decimal?());
    sender.RaiseFieldUpdating<SplitParams.cost>((object) row, ref obj2);
    row.Cost = (Decimal?) obj2;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewAsset(PXAdapter adapter)
  {
    if (((PXSelectBase<SplitParams>) this.Splits).Current != null)
    {
      AssetMaint instance = PXGraph.CreateInstance<AssetMaint>();
      ((PXSelectBase<FixedAsset>) instance.CurrentAsset).Current = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetCD, Equal<Current<SplitParams.splittedAssetCD>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (((PXSelectBase<FixedAsset>) instance.CurrentAsset).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewAsset));
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [Serializable]
  public class SplitFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _AssetID;
    protected Decimal? _Cost;
    protected Decimal? _Qty;
    protected DateTime? _SplitDate;
    protected string _SplitPeriodID;
    protected bool? _DeprBeforeSplit;

    [PXWorkflowMassProcessing(DisplayName = "Action")]
    public virtual string Action { get; set; }

    [PXDBInt]
    [PXSelector(typeof (Search2<FixedAsset.assetID, InnerJoin<FADetails, On<FADetails.assetID, Equal<FixedAsset.assetID>>>, Where<FixedAsset.recordType, Equal<FARecordType.assetType>, And<Where<FADetails.status, Equal<FixedAssetStatus.active>, Or<FADetails.status, Equal<FixedAssetStatus.fullyDepreciated>>>>>>), SubstituteKey = typeof (FixedAsset.assetCD), DescriptionField = typeof (FixedAsset.description))]
    [PXUIField(DisplayName = "Fixed Asset")]
    public virtual int? AssetID
    {
      get => this._AssetID;
      set => this._AssetID = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<FABookBalance.ytdDeprBase, Where<FABookBalance.assetID, Equal<Current<SplitProcess.SplitFilter.assetID>>, And<FABookBalance.ytdDeprBase, NotEqual<Zero>>>, OrderBy<Desc<FABookBalance.updateGL, Asc<FABookBalance.bookCD>>>>))]
    [PXUIField(DisplayName = "Cost", Enabled = false)]
    public virtual Decimal? Cost
    {
      get => this._Cost;
      set => this._Cost = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<FixedAsset.qty, Where<FixedAsset.assetID, Equal<Current<SplitProcess.SplitFilter.assetID>>>>))]
    [PXUIField(DisplayName = "Quantity", Enabled = false)]
    public virtual Decimal? Qty
    {
      get => this._Qty;
      set => this._Qty = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Split Date")]
    public virtual DateTime? SplitDate
    {
      get => this._SplitDate;
      set => this._SplitDate = value;
    }

    [PXUIField(DisplayName = "Split Period")]
    [FABookPeriodOpenInGLSelector(null, null, null, false, typeof (SplitProcess.SplitFilter.assetID), typeof (SplitProcess.SplitFilter.splitDate), null, null, null, null)]
    public virtual string SplitPeriodID
    {
      get => this._SplitPeriodID;
      set => this._SplitPeriodID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Depreciate before split")]
    public virtual bool? DeprBeforeSplit
    {
      get => this._DeprBeforeSplit;
      set => this._DeprBeforeSplit = value;
    }

    public abstract class action : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SplitProcess.SplitFilter.action>
    {
    }

    public abstract class assetID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SplitProcess.SplitFilter.assetID>
    {
    }

    public abstract class cost : BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SplitProcess.SplitFilter.cost>
    {
    }

    public abstract class qty : BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SplitProcess.SplitFilter.qty>
    {
    }

    public abstract class splitDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SplitProcess.SplitFilter.splitDate>
    {
    }

    public abstract class splitPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SplitProcess.SplitFilter.splitPeriodID>
    {
    }

    public abstract class deprBeforeSplit : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SplitProcess.SplitFilter.deprBeforeSplit>
    {
    }
  }
}
