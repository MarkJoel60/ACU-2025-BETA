// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.LineSplittingExtension`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
  where TLine : class, IBqlTable, ILSPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  private BqlCommand _splitByLotSerialStatus;
  private Dictionary<ILotSerNumVal, ILotSerNumVal> _lotSerNumVals;
  private PXCache<TPrimary> _docCache;
  private PXCache<TLine> _lineCache;
  private PXCache<TSplit> _splitCache;
  private string _prefix;

  public bool IsCorrectionMode
  {
    get
    {
      return ((PXSelectBase) this.lsselect).AllowUpdate && !((PXSelectBase) this.lsselect).AllowInsert && !((PXSelectBase) this.lsselect).AllowDelete;
    }
  }

  public bool IsFullMode
  {
    get
    {
      return ((PXSelectBase) this.lsselect).AllowUpdate && ((PXSelectBase) this.lsselect).AllowInsert && ((PXSelectBase) this.lsselect).AllowDelete;
    }
  }

  protected PXDBOperation CurrentOperation { get; set; }

  protected LSSelect.Counters CurrentCounters { get; set; }

  protected Dictionary<TLine, LSSelect.Counters> LineCounters { get; } = new Dictionary<TLine, LSSelect.Counters>();

  protected BqlCommand SplitByLotSerialStatusCommand
  {
    get
    {
      return this._splitByLotSerialStatus ?? (this._splitByLotSerialStatus = BqlTemplate.OfCommand<Select<TSplit, Where<BqlPlaceholder.I, Equal<Required<BqlPlaceholder.I>>, And<BqlPlaceholder.S, Equal<Required<BqlPlaceholder.S>>, And<BqlPlaceholder.W, Equal<Required<BqlPlaceholder.W>>, And<BqlPlaceholder.L, Equal<Required<BqlPlaceholder.L>>, And<BqlPlaceholder.N, Equal<Required<BqlPlaceholder.N>>, And<BqlPlaceholder.D>>>>>>>>.Replace<BqlPlaceholder.I>(this.SplitInventoryField).Replace<BqlPlaceholder.S>(this.SplitSubItemField).Replace<BqlPlaceholder.W>(this.SplitSiteField).Replace<BqlPlaceholder.L>(this.SplitLocationField).Replace<BqlPlaceholder.N>(this.SplitLotSerialNbrField).Replace<BqlPlaceholder.D>(this.SplitsToDocumentCondition).ToCommand());
    }
  }

  /// <summary>
  /// Indicates that logic specific to UI is suppressed.
  /// Use <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.ForceUnattendedModeScope(System.Boolean)" /> to force this suppression for a code section.
  /// </summary>
  public bool UnattendedMode { get; private set; }

  /// <summary>
  /// Indicates that major internal logic is suppressed.
  /// Use <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.SuppressedModeScope(System.Nullable{PX.Data.PXDBOperation})" /> or <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.SuppressedModeScope(System.Boolean)" /> to activate suppression for a code section.
  /// </summary>
  public bool SuppressedMode { get; private set; }

  protected virtual ItemAvailabilityExtension<TGraph, TLine, TSplit> Availability
  {
    get => this.Base.FindImplementation<ItemAvailabilityExtension<TGraph, TLine, TSplit>>();
  }

  /// <summary>
  /// The condition of the <see cref="T:PX.Data.IBqlUnary" /> type
  /// that describes the rule of selecting <typeparamref name="TSplit" /> entities
  /// for a given (<see cref="T:PX.Data.Current`1" />) <typeparamref name="TPrimary" /> entity.
  /// Consider using a <typeparamref name="TSplit" />.FK.<typeparamref name="TPrimary" />.SameAsCurrent condition.
  /// </summary>
  protected abstract Type SplitsToDocumentCondition { get; }

  public abstract TSplit LineToSplit(TLine line);

  protected virtual void SetEditMode()
  {
  }

  /// <summary>
  /// The InventoryID field of the <typeparamref name="TLine" /> entity.
  /// </summary>
  protected virtual Type LineInventoryField { get; private set; }

  /// <summary>
  /// The LotSerialNbr field of the <typeparamref name="TLine" /> entity.
  /// </summary>
  protected virtual Type LineLotSerialNbrField { get; private set; }

  /// <summary>
  /// The Qty field of the <typeparamref name="TLine" /> entity.
  /// </summary>
  protected virtual Type LineQtyField { get; private set; }

  protected virtual bool TryInferLineFieldFromAttribute(PXEventSubscriberAttribute attr)
  {
    if (this.LineInventoryField == (Type) null && attr is BaseInventoryAttribute)
    {
      this.LineInventoryField = ((PXCache) this.LineCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (this.LineLotSerialNbrField == (Type) null && attr is INLotSerialNbrAttribute)
    {
      this.LineLotSerialNbrField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (!(this.LineQtyField == (Type) null) || !attr.FieldName.Equals("Qty", StringComparison.OrdinalIgnoreCase))
      return false;
    this.LineQtyField = ((PXCache) this.LineCache).GetBqlField(attr.FieldName);
    return true;
  }

  /// <summary>
  /// The InventoryID field of the <typeparamref name="TSplit" /> entity.
  /// </summary>
  protected virtual Type SplitInventoryField { get; private set; }

  /// <summary>
  /// The SubItemID field of the <typeparamref name="TSplit" /> entity.
  /// </summary>
  protected virtual Type SplitSubItemField { get; private set; }

  /// <summary>
  /// The SiteID field of the <typeparamref name="TSplit" /> entity.
  /// </summary>
  protected virtual Type SplitSiteField { get; private set; }

  /// <summary>
  /// The LocationID field of the <typeparamref name="TSplit" /> entity.
  /// </summary>
  protected virtual Type SplitLocationField { get; private set; }

  /// <summary>
  /// The LotSerialNbr field of the <typeparamref name="TSplit" /> entity.
  /// </summary>
  protected virtual Type SplitLotSerialNbrField { get; private set; }

  /// <summary>
  /// The UOM field of the <typeparamref name="TSplit" /> entity.
  /// </summary>
  protected virtual Type SplitUomField { get; private set; }

  /// <summary>
  /// The Qty field of the <typeparamref name="TSplit" /> entity.
  /// </summary>
  protected virtual Type SplitQtyField { get; private set; }

  protected virtual bool TryInferSplitFieldFromAttribute(PXEventSubscriberAttribute attr)
  {
    if (this.SplitInventoryField == (Type) null && attr is BaseInventoryAttribute)
    {
      this.SplitInventoryField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (this.SplitSubItemField == (Type) null && attr is SubItemAttribute)
    {
      this.SplitSubItemField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (this.SplitSiteField == (Type) null && attr is SiteAttribute)
    {
      this.SplitSiteField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (this.SplitLocationField == (Type) null && attr is LocationAttribute)
    {
      this.SplitLocationField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (this.SplitLotSerialNbrField == (Type) null && attr is INLotSerialNbrAttribute)
    {
      this.SplitLotSerialNbrField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (this.SplitUomField == (Type) null && attr is INUnitAttribute)
    {
      this.SplitUomField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
      return true;
    }
    if (!(this.SplitQtyField == (Type) null) || !(attr is PXDBQuantityAttribute quantityAttribute) || !(quantityAttribute.KeyField != (Type) null))
      return false;
    this.SplitQtyField = ((PXCache) this.SplitCache).GetBqlField(attr.FieldName);
    return true;
  }

  public virtual void Initialize()
  {
    this.UnattendedMode = this.Base.UnattendedMode;
    foreach (PXEventSubscriberAttribute attr in ((PXCache) this.LineCache).GetAttributesReadonly((string) null))
      this.TryInferLineFieldFromAttribute(attr);
    foreach (PXEventSubscriberAttribute attr in ((PXCache) this.SplitCache).GetAttributesReadonly((string) null))
      this.TryInferSplitFieldFromAttribute(attr);
    this.SubscribeForLineEvents();
    this.SubscribeForSplitEvents();
    this.AddMainView();
    this.AddLotSerOptionsView();
    this.SubscribeForLotSerOptionsEvents();
    this.AddShowSplitsAction();
    this.AddGenerateNumbersAction();
    this.InitializeLotSerNumVals();
    PXParentAttribute.SetLeaveChildren((PXCache) this.SplitCache, true, typeof (TLine));
  }

  public PXSelectBase<TLine> lsselect { get; private set; }

  protected virtual void AddMainView()
  {
    this.lsselect = (PXSelectBase<TLine>) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.LSView(this);
    this.Base.Views.Add(this.TypePrefixed("lsselect"), ((PXSelectBase) this.lsselect).View);
  }

  protected virtual void AddLotSerOptionsView()
  {
    PXViewCollection views = this.Base.Views;
    string str = this.TypePrefixed("LotSerOptions");
    // ISSUE: variable of a boxed type
    __Boxed<TGraph> local = (object) this.Base;
    Select<LSSelect.LotSerOptions> select = new Select<LSSelect.LotSerOptions>();
    LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> splittingExtension = this;
    // ISSUE: virtual method pointer
    PXSelectDelegate pxSelectDelegate = new PXSelectDelegate((object) splittingExtension, __vmethodptr(splittingExtension, GetLotSerialOpts));
    PXView pxView = new PXView((PXGraph) local, false, (BqlCommand) select, (Delegate) pxSelectDelegate);
    views.Add(str, pxView);
  }

  public virtual IEnumerable GetLotSerialOpts()
  {
    LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> splittingExtension = this;
    LSSelect.LotSerOptions lotSerOptions = new LSSelect.LotSerOptions();
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>) null;
    lotSerOptions.ExtensionName = splittingExtension.TypePrefixed("LotSerOptions");
    if ((object) splittingExtension.LineCurrent != null)
    {
      lotSerOptions.UnassignedQty = splittingExtension.LineCurrent.UnassignedQty;
      pxResult = splittingExtension.ReadInventoryItem(((ILSMaster) splittingExtension.LineCurrent).InventoryID);
    }
    if (pxResult != null && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult) != null)
    {
      INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
      bool flag1;
      bool flag2;
      using (splittingExtension.InvtMultModeScope(splittingExtension.LineCurrent))
      {
        INLotSerTrack.Mode tranTrackMode = splittingExtension.GetTranTrackMode((ILSMaster) splittingExtension.LineCurrent, inLotSerClass);
        flag1 = tranTrackMode == INLotSerTrack.Mode.None || (tranTrackMode & INLotSerTrack.Mode.Manual) != 0;
        flag2 = (tranTrackMode & INLotSerTrack.Mode.Create) != 0;
      }
      if (!flag1 && ((PXSelectBase) splittingExtension.lsselect).AllowUpdate)
      {
        ILotSerNumVal lotSerNum = splittingExtension.ReadLotSerNumVal(pxResult);
        string number = AutoNumberAttribute.NextNumber(lotSerNum == null || string.IsNullOrEmpty(lotSerNum.LotSerNumVal) ? new string('0', INLotSerialNbrAttribute.GetNumberLength((ILotSerNumVal) null)) : lotSerNum.LotSerNumVal);
        string nextNumber = INLotSerialNbrAttribute.GetNextNumber((PXCache) splittingExtension.LineCache, inLotSerClass, lotSerNum);
        string nextFormat = INLotSerialNbrAttribute.GetNextFormat(inLotSerClass, lotSerNum);
        lotSerOptions.StartNumVal = INLotSerialNbrAttribute.UpdateNumber(nextFormat, nextNumber, number);
        lotSerOptions.AllowGenerate = new bool?(flag2);
        lotSerOptions.Qty = !(inLotSerClass.LotSerTrack == "S") ? new Decimal?(splittingExtension.LineCurrent.UnassignedQty.GetValueOrDefault()) : new Decimal?((Decimal) (int) splittingExtension.LineCurrent.UnassignedQty.GetValueOrDefault());
        lotSerOptions.IsSerial = new bool?(inLotSerClass.LotSerTrack == "S");
      }
    }
    ((PXCache) GraphHelper.Caches<LSSelect.LotSerOptions>((PXGraph) splittingExtension.Base)).Clear();
    LSSelect.LotSerOptions lotSerialOpt = PXCache<LSSelect.LotSerOptions>.Insert((PXGraph) splittingExtension.Base, lotSerOptions);
    ((PXCache) GraphHelper.Caches<LSSelect.LotSerOptions>((PXGraph) splittingExtension.Base)).IsDirty = false;
    yield return (object) lotSerialOpt;
  }

  protected virtual bool IsLotSerOptionsEnabled(LSSelect.LotSerOptions opt)
  {
    return opt?.StartNumVal != null;
  }

  protected virtual void AddShowSplitsAction()
  {
    string name = this.TypePrefixed("ShowSplits");
    LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> splittingExtension = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) splittingExtension, __vmethodptr(splittingExtension, ShowSplits));
    this.showSplits = this.AddAction(name, "Line Details", true, handler, (PXCacheRights) 1);
  }

  public PXAction showSplits { get; protected set; }

  public virtual IEnumerable ShowSplits(PXAdapter adapter)
  {
    ((PXSelectBase) this.lsselect).View.AskExt(true);
    return adapter.Get();
  }

  protected virtual void AddGenerateNumbersAction()
  {
    string name = this.TypePrefixed("GenerateNumbers");
    LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> splittingExtension = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) splittingExtension, __vmethodptr(splittingExtension, GenerateNumbers));
    this.generateNumbers = this.AddAction(name, "Generate", true, handler, (PXCacheRights) 2);
  }

  public PXAction generateNumbers { get; protected set; }

  public virtual IEnumerable GenerateNumbers(PXAdapter adapter)
  {
    LSSelect.LotSerOptions lotSerOptions = (LSSelect.LotSerOptions) ((PXCache) GraphHelper.Caches<LSSelect.LotSerOptions>((PXGraph) this.Base)).Current;
    if (lotSerOptions == null || lotSerOptions.ExtensionName != this.TypePrefixed("LotSerOptions"))
      lotSerOptions = (LSSelect.LotSerOptions) NonGenericIEnumerableExtensions.FirstOrDefault_(this.GetLotSerialOpts());
    if (lotSerOptions != null && lotSerOptions.StartNumVal != null)
    {
      Decimal? nullable1 = lotSerOptions.Qty;
      if (nullable1.HasValue)
      {
        PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) this.LineCurrent).InventoryID);
        INLotSerClass lsclass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
        if (lsclass == null)
          return adapter.Get();
        ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
        string str1 = (string) null;
        INLotSerialNbrAttribute.LSParts lsParts = INLotSerialNbrAttribute.GetLSParts((PXCache) this.LineCache, lsclass, lotSerNum);
        string str2 = lotSerOptions.StartNumVal.Substring(lsParts.nidx, lsParts.nlen);
        string NumberStr = lotSerOptions.StartNumVal.Substring(0, lsParts.flen) + new string('0', lsParts.nlen) + lotSerOptions.StartNumVal.Substring(lsParts.lidx, lsParts.llen);
        try
        {
          this.LineCurrent.LotSerialNbr = (string) null;
          List<TSplit> splitList = new List<TSplit>();
          if (lsclass.LotSerTrack == "L")
          {
            foreach (TSplit selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) null, typeof (TLine)))
              splitList.Add(selectSibling);
          }
          if (!(lsclass.LotSerTrack != "L"))
          {
            nullable1 = lotSerOptions.Qty;
            Decimal num1 = 0M;
            if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
            {
              nullable1 = this.LineCurrent.BaseQty;
              Decimal num2 = 0M;
              if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
                goto label_16;
            }
            else
              goto label_16;
          }
          TLine lineCurrent = this.LineCurrent;
          nullable1 = lotSerOptions.Qty;
          Decimal deltaBaseQty = nullable1.Value;
          this.CreateNumbers(lineCurrent, deltaBaseQty, true);
label_16:
          foreach (TSplit selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) null, typeof (TLine)))
          {
            if (!string.IsNullOrEmpty(selectSibling.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(selectSibling.AssignedNbr, selectSibling.LotSerialNbr))
            {
              TSplit split = this.Clone(selectSibling);
              if (str1 != null)
                str2 = AutoNumberAttribute.NextNumber(str2);
              Decimal num = lotSerOptions.Qty.Value;
              nullable1 = selectSibling.Qty;
              Decimal valueOrDefault = nullable1.GetValueOrDefault();
              if (!(num == valueOrDefault & nullable1.HasValue) && lsclass.LotSerTrack == "L" && !splitList.Contains(selectSibling))
              {
                // ISSUE: variable of a boxed type
                __Boxed<TSplit> local1 = (object) selectSibling;
                nullable1 = lotSerOptions.Qty;
                Decimal? nullable2 = new Decimal?(nullable1.Value);
                local1.BaseQty = nullable2;
                // ISSUE: variable of a boxed type
                __Boxed<TSplit> local2 = (object) selectSibling;
                nullable1 = lotSerOptions.Qty;
                Decimal? nullable3 = new Decimal?(nullable1.Value);
                local2.Qty = nullable3;
              }
              str1 = INLotSerialNbrAttribute.UpdateNumber(selectSibling.AssignedNbr, NumberStr, str2);
              ((PXCache) this.SplitCache).SetValue((object) selectSibling, "LotSerialNbr", (object) str1);
              ((PXCache) this.SplitCache).RaiseFieldUpdated("Qty", (object) selectSibling, (object) split.Qty);
              this.SplitCache.RaiseRowUpdated(selectSibling, split);
            }
          }
        }
        catch (Exception ex)
        {
          this.UpdateParent(this.LineCurrent);
        }
        if (str1 != null)
          this.UpdateLotSerNumVal(lotSerNum, str2, pxResult);
        return adapter.Get();
      }
    }
    return adapter.Get();
  }

  protected PXAction AddAction(
    string name,
    string displayName,
    bool visible,
    PXButtonDelegate handler,
    PXCacheRights enableRights)
  {
    PXButtonAttribute pxButtonAttribute = new PXButtonAttribute()
    {
      DisplayOnMainToolbar = false
    };
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute()
    {
      DisplayName = PXMessages.LocalizeNoPrefix(displayName),
      MapEnableRights = enableRights
    };
    if (!visible)
      pxuiFieldAttribute.Visible = false;
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>()
    {
      (PXEventSubscriberAttribute) pxuiFieldAttribute,
      (PXEventSubscriberAttribute) pxButtonAttribute
    };
    PXAction instance = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(this.Base.PrimaryItemType ?? throw new PXException("Can't get the primary view type for the graph {0}", new object[1]
    {
      (object) ((object) this.Base).GetType().FullName
    })), (object) this.Base, (object) name, (object) handler, (object) subscriberAttributeList.ToArray());
    if (name == this.TypePrefixed("ShowSplits"))
      instance.SetIgnoresArchiveDisabling(true);
    this.Base.Actions[name] = instance;
    return instance;
  }

  protected virtual void SubscribeForLineEvents()
  {
    ((RowSelectedEvents) this.Base.RowSelected).AddAbstractHandler<TLine>(new Action<AbstractEvents.IRowSelected<TLine>>(this.EventHandler));
    ((RowInsertedEvents) this.Base.RowInserted).AddAbstractHandler<TLine>(new Action<AbstractEvents.IRowInserted<TLine>>(this.EventHandler));
    ((RowUpdatedEvents) this.Base.RowUpdated).AddAbstractHandler<TLine>(new Action<AbstractEvents.IRowUpdated<TLine>>(this.EventHandler));
    ((RowDeletedEvents) this.Base.RowDeleted).AddAbstractHandler<TLine>(new Action<AbstractEvents.IRowDeleted<TLine>>(this.EventHandler));
    ((RowPersistingEvents) this.Base.RowPersisting).AddAbstractHandler<TLine>(new Action<AbstractEvents.IRowPersisting<TLine>>(this.EventHandler));
    ((RowPersistedEvents) this.Base.RowPersisted).AddAbstractHandler<TLine>(new Action<AbstractEvents.IRowPersisted<TLine>>(this.EventHandler));
    if (this.LineInventoryField != (Type) null)
      ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<TLine, int>(this.LineInventoryField.Name, new Action<AbstractEvents.IFieldVerifying<TLine, IBqlField, int?>>(this.EventHandlerInventoryID));
    if (!(this.LineQtyField != (Type) null))
      return;
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<TLine, Decimal>(this.LineQtyField.Name, new Action<AbstractEvents.IFieldVerifying<TLine, IBqlField, Decimal?>>(this.EventHandlerQty));
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<TLine> e)
  {
  }

  protected virtual void EventHandler(AbstractEvents.IRowInserted<TLine> e)
  {
    if (this.SuppressedMode)
      return;
    using (this.OperationModeScope((PXDBOperation) 2, true))
    {
      using (this.SuppressedModeScope())
      {
        using (this.InvtMultModeScope(e.Row))
          this.EventHandlerInternal(e);
      }
    }
  }

  protected virtual bool NeedToEnsureSingleSplit(
    INLotSerTrack.Mode mode,
    [INLotSerTrack.List] string itemLotSerialTrack,
    TLine row,
    TLine oldRow = null)
  {
    bool flag1;
    bool flag2;
    if (mode == INLotSerTrack.Mode.None || (mode & INLotSerTrack.Mode.Create) > INLotSerTrack.Mode.None)
    {
      flag1 = true;
      Decimal? baseQty = row.BaseQty;
      Decimal num1 = 0M;
      int num2;
      if (!(baseQty.GetValueOrDefault() == num1 & baseQty.HasValue))
      {
        baseQty = row.BaseQty;
        Decimal num3 = 1M;
        num2 = baseQty.GetValueOrDefault() == num3 & baseQty.HasValue ? 1 : 0;
      }
      else
        num2 = 1;
      flag2 = num2 != 0;
    }
    else
    {
      if ((mode & INLotSerTrack.Mode.Issue) <= INLotSerTrack.Mode.None)
        return false;
      Decimal? nullable = row.BaseQty;
      Decimal num4 = 1M;
      flag2 = nullable.GetValueOrDefault() == num4 & nullable.HasValue;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local = (object) oldRow;
      int num5;
      if (local == null)
      {
        num5 = 0;
      }
      else
      {
        nullable = local.UnassignedQty;
        Decimal num6 = 0M;
        num5 = nullable.GetValueOrDefault() == num6 & nullable.HasValue ? 1 : 0;
      }
      flag1 = num5 != 0;
    }
    int num = this.AllowSplitCreationForLineWithEmptyLotSerialNbr(mode) ? 1 : (!string.IsNullOrEmpty(row.LotSerialNbr) ? 1 : 0);
    bool flag3 = itemLotSerialTrack != "S";
    if (num == 0)
      return false;
    return flag2 || flag3 & flag1;
  }

  protected virtual bool AllowSplitCreationForLineWithEmptyLotSerialNbr(INLotSerTrack.Mode mode)
  {
    return false;
  }

  protected virtual void EventHandlerInternal(AbstractEvents.IRowInserted<TLine> e)
  {
    e.Row.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache, ((ILSMaster) e.Row).InventoryID, e.Row.UOM, e.Row.Qty.Value, e.Row.BaseQty, INPrecision.QUANTITY));
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult1 = this.ReadInventoryItem(((ILSMaster) e.Row).InventoryID);
    if (pxResult1 != null && (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).StkItem.GetValueOrDefault() || !this.IncludeKitSpecDetail(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1))))
    {
      INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) e.Row, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1));
      if (tranTrackMode == INLotSerTrack.Mode.None || (tranTrackMode & INLotSerTrack.Mode.Create) > INLotSerTrack.Mode.None)
      {
        if (this.NeedToEnsureSingleSplit(tranTrackMode, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).LotSerTrack, e.Row))
          this.EnsureSingleSplit(e.Row, e.Row.BaseQty.Value);
        else
          this.CreateNumbers(e.Row, e.Row.BaseQty.Value);
        this.UpdateParent(e.Row);
      }
      else
      {
        Decimal? nullable;
        if ((tranTrackMode & INLotSerTrack.Mode.Issue) > INLotSerTrack.Mode.None)
        {
          nullable = e.Row.BaseQty;
          Decimal num1 = 0M;
          if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
          {
            TLine row = e.Row;
            nullable = e.Row.BaseQty;
            Decimal deltaBaseQty = nullable.Value;
            this.IssueNumbers(row, deltaBaseQty);
            nullable = e.Row.BaseQty;
            Decimal num2 = 0M;
            if (!(nullable.GetValueOrDefault() > num2 & nullable.HasValue))
              return;
            this.UpdateParent(e.Row);
            return;
          }
        }
        nullable = e.Row.BaseQty;
        Decimal num3 = 0M;
        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
          return;
        nullable = e.Row.UnassignedQty;
        Decimal num4 = 0M;
        if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
          return;
        e.Row.UnassignedQty = new Decimal?(0M);
      }
    }
    else
    {
      if (pxResult1 == null)
        return;
      Decimal? nullable1 = new Decimal?(0M);
      this.KitInProcessing = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1);
      PX.Objects.IN.InventoryItem inventoryItem1;
      try
      {
        foreach (PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem> pxResult2 in PXSelectBase<INKitSpecStkDet, PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecStkDet.FK.ComponentInventoryItem>>>.Where<BqlOperand<INKitSpecStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) ((ILSMaster) e.Row).InventoryID
        }))
        {
          INKitSpecStkDet inKitSpecStkDet;
          pxResult2.Deconstruct(ref inKitSpecStkDet, ref inventoryItem1);
          INKitSpecStkDet kititem = inKitSpecStkDet;
          PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
          if (inventoryItem2.ItemStatus == "IN")
            throw new PXException("The '{0}' component of the kit is inactive.", new object[1]
            {
              (object) inventoryItem2.InventoryCD
            });
          TLine stockComponentLine = this.CreateVirtualStockComponentLine(e.Row, kititem);
          ConvertedInventoryItemAttribute.ValidateRow((PXCache) this.LineCache, (object) stockComponentLine);
          try
          {
            Events.RowInserted<TLine> e1 = new Events.RowInserted<TLine>();
            ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TLine>>) e1).Cache = ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache;
            ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TLine>>) e1).Args = new PXRowInsertedEventArgs((object) stockComponentLine, e.ExternalCall);
            this.EventHandlerInternal((AbstractEvents.IRowInserted<TLine>) e1);
            Decimal? nullable2 = stockComponentLine.UnassignedQty;
            Decimal num = 0M;
            if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
            {
              nullable2 = nullable1;
              Decimal? unassignedQty = stockComponentLine.UnassignedQty;
              Decimal? nullable3 = kititem.DfltCompQty;
              Decimal? nullable4 = unassignedQty.HasValue & nullable3.HasValue ? new Decimal?(unassignedQty.GetValueOrDefault() / nullable3.GetValueOrDefault()) : new Decimal?();
              if (nullable2.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable2.HasValue & nullable4.HasValue)
              {
                nullable4 = stockComponentLine.UnassignedQty;
                nullable2 = kititem.DfltCompQty;
                Decimal? nullable5;
                if (!(nullable4.HasValue & nullable2.HasValue))
                {
                  nullable3 = new Decimal?();
                  nullable5 = nullable3;
                }
                else
                  nullable5 = new Decimal?(nullable4.GetValueOrDefault() / nullable2.GetValueOrDefault());
                nullable1 = nullable5;
              }
            }
          }
          catch (PXException ex)
          {
            throw new PXException((Exception) ex, "Failed to process Component '{0}' when processing kit '{1}'. {2}", new object[3]
            {
              (object) inventoryItem2.InventoryCD,
              (object) PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).InventoryCD,
              (object) ex.MessageNoPrefix
            });
          }
        }
      }
      finally
      {
        this.KitInProcessing = (PX.Objects.IN.InventoryItem) null;
      }
      foreach (PXResult<INKitSpecNonStkDet, PX.Objects.IN.InventoryItem> pxResult3 in PXSelectBase<INKitSpecNonStkDet, PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecNonStkDet.FK.ComponentInventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecNonStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.kitItem, Equal<True>>>>>.Or<BqlOperand<PX.Objects.IN.InventoryItem.nonStockShip, IBqlBool>.IsEqual<True>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ILSMaster) e.Row).InventoryID
      }))
      {
        INKitSpecNonStkDet kitSpecNonStkDet;
        pxResult3.Deconstruct(ref kitSpecNonStkDet, ref inventoryItem1);
        INKitSpecNonStkDet kititem = kitSpecNonStkDet;
        PX.Objects.IN.InventoryItem inventoryItem3 = inventoryItem1;
        TLine stockComponentLine = this.CreateVirtualNonStockComponentLine(e.Row, kititem);
        ConvertedInventoryItemAttribute.ValidateRow((PXCache) this.LineCache, (object) stockComponentLine);
        try
        {
          Events.RowInserted<TLine> e2 = new Events.RowInserted<TLine>();
          ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TLine>>) e2).Cache = ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache;
          ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TLine>>) e2).Args = new PXRowInsertedEventArgs((object) stockComponentLine, e.ExternalCall);
          this.EventHandlerInternal((AbstractEvents.IRowInserted<TLine>) e2);
        }
        catch (PXException ex)
        {
          throw new PXException((Exception) ex, "Failed to process Component '{0}' when processing kit '{1}'. {2}", new object[3]
          {
            (object) inventoryItem3.InventoryCD,
            (object) PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult1).InventoryCD,
            (object) ex.MessageNoPrefix
          });
        }
      }
      e.Row.UnassignedQty = nullable1;
      Decimal? nullable6 = nullable1;
      Decimal num5 = 0M;
      if (!(nullable6.GetValueOrDefault() > num5 & nullable6.HasValue))
        return;
      this.RaiseUnassignedExceptionHandling(e.Row);
    }
  }

  protected virtual bool IncludeKitSpecDetail(PX.Objects.IN.InventoryItem inventoryItem)
  {
    return inventoryItem.KitItem.GetValueOrDefault();
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<TLine> e)
  {
    if (this.SuppressedMode)
      return;
    using (this.OperationModeScope((PXDBOperation) 1, true))
    {
      using (this.SuppressedModeScope())
      {
        using (this.InvtMultModeScope(e.Row, e.OldRow))
          this.EventHandlerInternal(e);
      }
    }
  }

  protected virtual void EventHandlerInternal(AbstractEvents.IRowUpdated<TLine> e)
  {
    if ((object) e.OldRow != null)
    {
      int? nullable1 = ((ILSMaster) e.OldRow).InventoryID;
      int? inventoryId = ((ILSMaster) e.Row).InventoryID;
      short? invtMult;
      if (nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue)
      {
        invtMult = e.OldRow.InvtMult;
        int? nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
        invtMult = e.Row.InvtMult;
        nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !(e.OldRow.UOM == null ^ e.Row.UOM == null))
        {
          nullable1 = e.OldRow.CostCenterID;
          int? costCenterId = e.Row.CostCenterID;
          if (nullable1.GetValueOrDefault() == costCenterId.GetValueOrDefault() & nullable1.HasValue == costCenterId.HasValue)
            goto label_10;
        }
      }
      if (!this.Base.IsContractBasedAPI)
      {
        int? nullable3 = ((ILSMaster) e.OldRow).InventoryID;
        nullable1 = ((ILSMaster) e.Row).InventoryID;
        if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
        {
          e.Row.LotSerialNbr = (string) null;
          e.Row.ExpireDate = new DateTime?();
        }
        else
        {
          invtMult = e.OldRow.InvtMult;
          nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          invtMult = e.Row.InvtMult;
          nullable3 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
            this.ClearLotSerial(e);
        }
      }
      this.RaiseRowDeleted(e.OldRow);
      this.RaiseRowInserted(e.Row);
      return;
    }
label_10:
    e.Row.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, ((ILSMaster) e.Row).InventoryID, e.Row.UOM, e.Row.Qty.Value, e.Row.BaseQty, INPrecision.QUANTITY));
    DateTime? expireDate1 = e.Row.ExpireDate;
    DateTime? expireDate2 = e.OldRow.ExpireDate;
    if ((expireDate1.HasValue == expireDate2.HasValue ? (expireDate1.HasValue ? (expireDate1.GetValueOrDefault() == expireDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && !string.Equals(e.OldRow.LotSerialNbr, e.Row.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
      e.Row.ExpireDate = new DateTime?();
    if (!((ILSMaster) e.Row).InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem1;
    INLotSerClass inLotSerClass;
    this.ReadInventoryItem(((ILSMaster) e.Row).InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass);
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    INLotSerClass lotSerClass = inLotSerClass;
    if (inventoryItem2.StkItem.GetValueOrDefault() || !this.IncludeKitSpecDetail(inventoryItem2))
    {
      string lotSerTrack = lotSerClass.LotSerTrack;
      INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) e.Row, lotSerClass);
      if (tranTrackMode == INLotSerTrack.Mode.None || (tranTrackMode & INLotSerTrack.Mode.Create) > INLotSerTrack.Mode.None)
      {
        if (!this.IsPrimaryFieldsUpdated(e.Row, e.OldRow))
        {
          DateTime? expireDate3 = e.Row.ExpireDate;
          expireDate1 = e.OldRow.ExpireDate;
          if ((expireDate3.HasValue == expireDate1.HasValue ? (expireDate3.HasValue ? (expireDate3.GetValueOrDefault() != expireDate1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            goto label_21;
        }
        if (!this.IsCorrectionMode && (lotSerTrack == "N" || lotSerTrack == null))
        {
          this.RaiseRowDeleted(e.OldRow);
          this.RaiseRowInserted(e.Row);
          return;
        }
        this.UpdateNumbers(e.Row);
label_21:
        if (this.NeedToEnsureSingleSplit(tranTrackMode, lotSerTrack, e.Row))
        {
          TLine row = e.Row;
          Decimal? baseQty = e.Row.BaseQty;
          Decimal num1 = baseQty.Value;
          baseQty = e.OldRow.BaseQty;
          Decimal num2 = baseQty.Value;
          Decimal deltaBaseQty = num1 - num2;
          this.EnsureSingleSplit(row, deltaBaseQty);
        }
        else
        {
          Decimal? baseQty1 = e.Row.BaseQty;
          Decimal? baseQty2 = e.OldRow.BaseQty;
          if (baseQty1.GetValueOrDefault() > baseQty2.GetValueOrDefault() & baseQty1.HasValue & baseQty2.HasValue)
          {
            TLine row = e.Row;
            Decimal? baseQty3 = e.Row.BaseQty;
            Decimal num3 = baseQty3.Value;
            baseQty3 = e.OldRow.BaseQty;
            Decimal num4 = baseQty3.Value;
            Decimal deltaBaseQty = num3 - num4;
            this.CreateNumbers(row, deltaBaseQty);
          }
          else
          {
            Decimal? baseQty4 = e.Row.BaseQty;
            baseQty1 = e.OldRow.BaseQty;
            if (baseQty4.GetValueOrDefault() < baseQty1.GetValueOrDefault() & baseQty4.HasValue & baseQty1.HasValue)
            {
              TLine row = e.Row;
              baseQty1 = e.OldRow.BaseQty;
              Decimal num5 = baseQty1.Value;
              baseQty1 = e.Row.BaseQty;
              Decimal num6 = baseQty1.Value;
              Decimal deltaBaseQty = num5 - num6;
              this.TruncateNumbers(row, deltaBaseQty);
            }
            else if (this.IsTransferReceipt((ILSMaster) e.Row))
              this.UpdateNumbers(e.Row);
          }
        }
        this.UpdateParent(e.Row);
      }
      else
      {
        if ((tranTrackMode & INLotSerTrack.Mode.Issue) <= INLotSerTrack.Mode.None)
          return;
        Decimal? baseQty5;
        if (this.IsPrimaryFieldsUpdated(e.Row, e.OldRow) || !string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        {
          this.RaiseRowDeleted(e.OldRow);
          this.RaiseRowInserted(e.Row);
        }
        else if (this.NeedToEnsureSingleSplit(tranTrackMode, lotSerTrack, e.Row, e.OldRow))
        {
          TLine row = e.Row;
          baseQty5 = e.Row.BaseQty;
          Decimal num7 = baseQty5.Value;
          baseQty5 = e.OldRow.BaseQty;
          Decimal num8 = baseQty5.Value;
          Decimal deltaBaseQty = num7 - num8;
          this.EnsureSingleSplit(row, deltaBaseQty);
        }
        else
        {
          baseQty5 = e.Row.BaseQty;
          Decimal? baseQty6 = e.OldRow.BaseQty;
          if (baseQty5.GetValueOrDefault() > baseQty6.GetValueOrDefault() & baseQty5.HasValue & baseQty6.HasValue)
          {
            TLine row = e.Row;
            Decimal? baseQty7 = e.Row.BaseQty;
            Decimal num9 = baseQty7.Value;
            baseQty7 = e.OldRow.BaseQty;
            Decimal num10 = baseQty7.Value;
            Decimal deltaBaseQty = num9 - num10;
            this.IssueNumbers(row, deltaBaseQty);
          }
          else
          {
            Decimal? baseQty8 = e.Row.BaseQty;
            baseQty5 = e.OldRow.BaseQty;
            if (baseQty8.GetValueOrDefault() < baseQty5.GetValueOrDefault() & baseQty8.HasValue & baseQty5.HasValue)
            {
              TLine row = e.Row;
              baseQty5 = e.OldRow.BaseQty;
              Decimal num11 = baseQty5.Value;
              baseQty5 = e.Row.BaseQty;
              Decimal num12 = baseQty5.Value;
              Decimal deltaBaseQty = num11 - num12;
              this.TruncateNumbers(row, deltaBaseQty);
            }
            else if (this.IsTransferReceipt((ILSMaster) e.Row))
              this.UpdateNumbers(e.Row);
          }
        }
        baseQty5 = e.Row.BaseQty;
        Decimal num = 0M;
        if (!(baseQty5.GetValueOrDefault() > num & baseQty5.HasValue))
          return;
        this.UpdateParent(e.Row);
      }
    }
    else
    {
      Decimal? nullable4 = new Decimal?(0M);
      this.KitInProcessing = inventoryItem2;
      try
      {
        foreach (PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<INKitSpecStkDet, PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecStkDet.FK.ComponentInventoryItem>>>.Where<BqlOperand<INKitSpecStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) ((ILSMaster) e.Row).InventoryID
        }))
        {
          INKitSpecStkDet inKitSpecStkDet;
          pxResult.Deconstruct(ref inKitSpecStkDet, ref inventoryItem1);
          INKitSpecStkDet kititem = inKitSpecStkDet;
          PX.Objects.IN.InventoryItem inventoryItem3 = inventoryItem1;
          if (inventoryItem3.ItemStatus == "IN")
            throw new PXException("The '{0}' component of the kit is inactive.", new object[1]
            {
              (object) inventoryItem3.InventoryCD
            });
          TLine stockComponentLine1 = this.CreateVirtualStockComponentLine(e.Row, kititem);
          ConvertedInventoryItemAttribute.ValidateRow((PXCache) this.LineCache, (object) stockComponentLine1);
          TLine stockComponentLine2 = this.CreateVirtualStockComponentLine(e.OldRow, kititem);
          LSSelect.Counters counters;
          if (!this.LineCounters.TryGetValue(stockComponentLine1, out counters))
          {
            this.LineCounters[stockComponentLine1] = this.CurrentCounters = new LSSelect.Counters();
            foreach (TSplit selectSplit in this.SelectSplits(stockComponentLine1))
              this.UpdateCounters(this.CurrentCounters, selectSplit);
          }
          else
            this.CurrentCounters = counters;
          stockComponentLine2.BaseQty = new Decimal?(this.CurrentCounters.BaseQty);
          try
          {
            Events.RowUpdated<TLine> e1 = new Events.RowUpdated<TLine>();
            ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TLine>>) e1).Cache = ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache;
            ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TLine>>) e1).Args = new PXRowUpdatedEventArgs((object) stockComponentLine1, (object) stockComponentLine2, e.ExternalCall);
            this.EventHandlerInternal((AbstractEvents.IRowUpdated<TLine>) e1);
            Decimal? nullable5 = stockComponentLine1.UnassignedQty;
            Decimal num = 0M;
            if (nullable5.GetValueOrDefault() > num & nullable5.HasValue)
            {
              nullable5 = nullable4;
              Decimal? unassignedQty = stockComponentLine1.UnassignedQty;
              Decimal? dfltCompQty = kititem.DfltCompQty;
              Decimal? nullable6 = unassignedQty.HasValue & dfltCompQty.HasValue ? new Decimal?(unassignedQty.GetValueOrDefault() / dfltCompQty.GetValueOrDefault()) : new Decimal?();
              if (nullable5.GetValueOrDefault() < nullable6.GetValueOrDefault() & nullable5.HasValue & nullable6.HasValue)
              {
                nullable6 = stockComponentLine1.UnassignedQty;
                nullable5 = kititem.DfltCompQty;
                nullable4 = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / nullable5.GetValueOrDefault()) : new Decimal?();
              }
            }
          }
          catch (PXException ex)
          {
            throw new PXException((Exception) ex, "Failed to process Component '{0}' when processing kit '{1}'. {2}", new object[3]
            {
              (object) inventoryItem3.InventoryCD,
              (object) inventoryItem2.InventoryCD,
              (object) ex.MessageNoPrefix
            });
          }
        }
      }
      finally
      {
        this.KitInProcessing = (PX.Objects.IN.InventoryItem) null;
      }
      foreach (PXResult<INKitSpecNonStkDet, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<INKitSpecNonStkDet, PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecNonStkDet.FK.ComponentInventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecNonStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.kitItem, Equal<True>>>>>.Or<BqlOperand<PX.Objects.IN.InventoryItem.nonStockShip, IBqlBool>.IsEqual<True>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ILSMaster) e.Row).InventoryID
      }))
      {
        INKitSpecNonStkDet kitSpecNonStkDet;
        pxResult.Deconstruct(ref kitSpecNonStkDet, ref inventoryItem1);
        INKitSpecNonStkDet kititem = kitSpecNonStkDet;
        PX.Objects.IN.InventoryItem inventoryItem4 = inventoryItem1;
        if (inventoryItem4.ItemStatus == "IN")
          throw new PXException("The '{0}' component of the kit is inactive.", new object[1]
          {
            (object) inventoryItem4.InventoryCD
          });
        TLine stockComponentLine3 = this.CreateVirtualNonStockComponentLine(e.Row, kititem);
        ConvertedInventoryItemAttribute.ValidateRow((PXCache) this.LineCache, (object) stockComponentLine3);
        TLine stockComponentLine4 = this.CreateVirtualNonStockComponentLine(e.OldRow, kititem);
        stockComponentLine4.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, ((ILSMaster) stockComponentLine4).InventoryID, stockComponentLine4.UOM, stockComponentLine4.Qty.Value, stockComponentLine4.BaseQty, INPrecision.QUANTITY));
        try
        {
          Events.RowUpdated<TLine> e2 = new Events.RowUpdated<TLine>();
          ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TLine>>) e2).Cache = ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache;
          ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TLine>>) e2).Args = new PXRowUpdatedEventArgs((object) stockComponentLine3, (object) stockComponentLine4, e.ExternalCall);
          this.EventHandlerInternal((AbstractEvents.IRowUpdated<TLine>) e2);
        }
        catch (PXException ex)
        {
          throw new PXException((Exception) ex, "Failed to process Component '{0}' when processing kit '{1}'. {2}", new object[3]
          {
            (object) inventoryItem4.InventoryCD,
            (object) inventoryItem2.InventoryCD,
            (object) ex.MessageNoPrefix
          });
        }
      }
      e.Row.UnassignedQty = nullable4;
      Decimal? nullable7 = nullable4;
      Decimal num13 = 0M;
      if (!(nullable7.GetValueOrDefault() > num13 & nullable7.HasValue))
        return;
      this.RaiseUnassignedExceptionHandling(e.Row);
    }
  }

  protected virtual void ClearLotSerial(AbstractEvents.IRowUpdated<TLine> e)
  {
    if (string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
      e.Row.LotSerialNbr = (string) null;
    DateTime? expireDate = e.Row.ExpireDate;
    DateTime? nullable1 = e.OldRow.ExpireDate;
    if ((expireDate.HasValue == nullable1.HasValue ? (expireDate.HasValue ? (expireDate.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    // ISSUE: variable of a boxed type
    __Boxed<TLine> row = (object) e.Row;
    nullable1 = new DateTime?();
    DateTime? nullable2 = nullable1;
    row.ExpireDate = nullable2;
  }

  protected virtual void EventHandler(AbstractEvents.IRowDeleted<TLine> e)
  {
    using (this.OperationModeScope((PXDBOperation) 3, true))
    {
      using (this.SuppressedModeScope())
      {
        foreach (object selectChild in PXParentAttribute.SelectChildren((PXCache) this.SplitCache, (object) e.Row, typeof (TLine)))
          ((PXCache) this.SplitCache).Delete(selectChild);
      }
    }
  }

  protected virtual void EventHandlerInternal(AbstractEvents.IRowDeleted<TLine> e)
  {
    if ((object) e.Row != null)
      this.LineCounters.Remove(e.Row);
    foreach (TSplit selectSplit in this.SelectSplits(e.Row))
      this.SplitCache.Delete(selectSplit);
    if (!((ILSMaster) e.Row).InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem1;
    INLotSerClass inLotSerClass;
    this.ReadInventoryItem(((ILSMaster) e.Row).InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass);
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    if (inventoryItem2 == null)
      return;
    bool? nullable1 = inventoryItem2.StkItem;
    bool flag = false;
    if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
      return;
    nullable1 = inventoryItem2.KitItem;
    if (!nullable1.GetValueOrDefault())
      return;
    foreach (PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<INKitSpecStkDet, PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecStkDet.FK.ComponentInventoryItem>>>.Where<BqlOperand<INKitSpecStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((ILSMaster) e.Row).InventoryID
    }))
    {
      INKitSpecStkDet inKitSpecStkDet1;
      pxResult.Deconstruct(ref inKitSpecStkDet1, ref inventoryItem1);
      INKitSpecStkDet inKitSpecStkDet2 = inKitSpecStkDet1;
      PX.Objects.IN.InventoryItem inventoryItem3 = inventoryItem1;
      TLine line = this.Clone(e.Row);
      line.IsStockItem = new bool?(true);
      ((ILSMaster) line).InventoryID = inKitSpecStkDet2.CompInventoryID;
      line.SubItemID = inKitSpecStkDet2.CompSubItemID;
      line.UOM = inKitSpecStkDet2.UOM;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local = (object) line;
      Decimal? dfltCompQty = inKitSpecStkDet2.DfltCompQty;
      Decimal? baseQty = line.BaseQty;
      Decimal? nullable2 = dfltCompQty.HasValue & baseQty.HasValue ? new Decimal?(dfltCompQty.GetValueOrDefault() * baseQty.GetValueOrDefault()) : new Decimal?();
      local.Qty = nullable2;
      line.UnassignedQty = new Decimal?(0M);
      try
      {
        Events.RowDeleted<TLine> e1 = new Events.RowDeleted<TLine>();
        ((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TLine>>) e1).Cache = ((IGenericEventWith<PXRowDeletedEventArgs>) e).Cache;
        ((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TLine>>) e1).Args = new PXRowDeletedEventArgs((object) line, false);
        this.EventHandlerInternal((AbstractEvents.IRowDeleted<TLine>) e1);
      }
      catch (PXException ex)
      {
        throw new PXException((Exception) ex, "Failed to process Component '{0}' when processing kit '{1}'. {2}", new object[3]
        {
          (object) inventoryItem3.InventoryCD,
          (object) inventoryItem2.InventoryCD,
          (object) ex.MessageNoPrefix
        });
      }
    }
    foreach (PXResult<INKitSpecNonStkDet, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<INKitSpecNonStkDet, PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecNonStkDet.FK.ComponentInventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecNonStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.kitItem, Equal<True>>>>>.Or<BqlOperand<PX.Objects.IN.InventoryItem.nonStockShip, IBqlBool>.IsEqual<True>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((ILSMaster) e.Row).InventoryID
    }))
    {
      INKitSpecNonStkDet kitSpecNonStkDet1;
      pxResult.Deconstruct(ref kitSpecNonStkDet1, ref inventoryItem1);
      INKitSpecNonStkDet kitSpecNonStkDet2 = kitSpecNonStkDet1;
      PX.Objects.IN.InventoryItem inventoryItem4 = inventoryItem1;
      TLine line = this.Clone(e.Row);
      line.IsStockItem = new bool?(false);
      ((ILSMaster) line).InventoryID = kitSpecNonStkDet2.CompInventoryID;
      line.SubItemID = new int?();
      line.UOM = kitSpecNonStkDet2.UOM;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local = (object) line;
      Decimal? dfltCompQty = kitSpecNonStkDet2.DfltCompQty;
      Decimal? baseQty = line.BaseQty;
      Decimal? nullable3 = dfltCompQty.HasValue & baseQty.HasValue ? new Decimal?(dfltCompQty.GetValueOrDefault() * baseQty.GetValueOrDefault()) : new Decimal?();
      local.Qty = nullable3;
      line.UnassignedQty = new Decimal?(0M);
      try
      {
        Events.RowDeleted<TLine> e2 = new Events.RowDeleted<TLine>();
        ((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TLine>>) e2).Cache = ((IGenericEventWith<PXRowDeletedEventArgs>) e).Cache;
        ((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TLine>>) e2).Args = new PXRowDeletedEventArgs((object) line, false);
        this.EventHandlerInternal((AbstractEvents.IRowDeleted<TLine>) e2);
      }
      catch (PXException ex)
      {
        throw new PXException((Exception) ex, "Failed to process Component '{0}' when processing kit '{1}'. {2}", new object[3]
        {
          (object) inventoryItem4.InventoryCD,
          (object) inventoryItem2.InventoryCD,
          (object) ex.MessageNoPrefix
        });
      }
    }
  }

  protected Dictionary<TLine, TSplit[]> PersistedLinesToRelatedSplits { get; } = new Dictionary<TLine, TSplit[]>();

  protected virtual void EventHandler(AbstractEvents.IRowPersisting<TLine> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || !this.GenerateLotSerialNumberOnPersist(e.Row))
      return;
    TSplit[] splitArray = this.SelectSplits(e.Row, false);
    if ((object) e.Row != null)
      this.PersistedLinesToRelatedSplits[e.Row] = splitArray;
    foreach (TSplit split in splitArray)
    {
      using (this.SuppressedModeScope())
      {
        Events.RowPersisting<TSplit> e1 = new Events.RowPersisting<TSplit>();
        ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TSplit>>) e1).Cache = (PXCache) this.SplitCache;
        ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TSplit>>) e1).Args = new PXRowPersistingEventArgs(e.Operation, (object) split);
        this.EventHandler((AbstractEvents.IRowPersisting<TSplit>) e1);
      }
      if (!string.IsNullOrEmpty(e.Row.LotSerialNbr))
      {
        int? inventoryId1 = ((ILSMaster) split).InventoryID;
        int? inventoryId2 = ((ILSMaster) e.Row).InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        {
          e.Row.LotSerialNbr = split.LotSerialNbr;
          break;
        }
      }
    }
  }

  protected virtual void EventHandler(AbstractEvents.IRowPersisted<TLine> e)
  {
    if (e.TranStatus == 2)
    {
      this.RestoreLotSerNumbers();
      if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      {
        TSplit[] splitArray;
        if ((object) e.Row == null || !this.PersistedLinesToRelatedSplits.TryGetValue(e.Row, out splitArray))
          splitArray = this.SelectSplits(e.Row, false);
        foreach (TSplit split in splitArray)
        {
          Events.RowPersisted<TSplit> e1 = new Events.RowPersisted<TSplit>();
          ((Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TSplit>>) e1).Cache = (PXCache) this.SplitCache;
          ((Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TSplit>>) e1).Args = new PXRowPersistedEventArgs((object) split, e.Operation, e.TranStatus, e.Exception);
          this.EventHandler((AbstractEvents.IRowPersisted<TSplit>) e1);
          if (!string.IsNullOrEmpty(e.Row.LotSerialNbr))
          {
            int? inventoryId1 = ((ILSMaster) split).InventoryID;
            int? inventoryId2 = ((ILSMaster) e.Row).InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              e.Row.LotSerialNbr = split.LotSerialNbr;
              break;
            }
          }
        }
      }
    }
    if ((object) e.Row == null || e.TranStatus == null)
      return;
    this.PersistedLinesToRelatedSplits.Remove(e.Row);
  }

  public virtual void EventHandlerInventoryID(
    AbstractEvents.IFieldVerifying<TLine, IBqlField, int?> e)
  {
    if (this.Base.UnattendedMode || this.Base.IsImport)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.NewValue);
    if (inventoryItem == null || !inventoryItem.KitItem.GetValueOrDefault())
      return;
    bool? stkItem = inventoryItem.StkItem;
    bool flag = false;
    if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue && KitHasNoComponentsFor(e.NewValue))
    {
      e.NewValue = new int?();
      throw new PXSetPropertyException("Selected kit cannot be added. The kit has no components specified.", (PXErrorLevel) 4);
    }

    bool KitHasNoComponentsFor(int? inventoryID)
    {
      if (PXResultset<INKitSpecStkDet>.op_Implicit(PXSelectBase<INKitSpecStkDet, PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INKitSpecStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inventoryID
      })) != null)
        return false;
      return PXResultset<INKitSpecNonStkDet>.op_Implicit(PXSelectBase<INKitSpecNonStkDet, PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INKitSpecNonStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inventoryID
      })) == null;
    }
  }

  public virtual void EventHandlerQty(
    AbstractEvents.IFieldVerifying<TLine, IBqlField, Decimal?> e)
  {
    e.NewValue = this.VerifySNQuantity(((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache, (ILSMaster) e.Row, e.NewValue, this.LineQtyField.Name);
  }

  public virtual void RaiseRowInserted(TLine line)
  {
    Events.RowInserted<TLine> e = new Events.RowInserted<TLine>();
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TLine>>) e).Cache = (PXCache) this.LineCache;
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TLine>>) e).Args = new PXRowInsertedEventArgs((object) line, false);
    this.EventHandlerInternal((AbstractEvents.IRowInserted<TLine>) e);
  }

  public virtual void RaiseRowDeleted(TLine line)
  {
    Events.RowDeleted<TLine> e = new Events.RowDeleted<TLine>();
    ((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TLine>>) e).Cache = (PXCache) this.LineCache;
    ((Events.Event<PXRowDeletedEventArgs, Events.RowDeleted<TLine>>) e).Args = new PXRowDeletedEventArgs((object) line, false);
    this.EventHandlerInternal((AbstractEvents.IRowDeleted<TLine>) e);
  }

  protected virtual void SubscribeForSplitEvents()
  {
    ((RowInsertingEvents) this.Base.RowInserting).AddAbstractHandler<TSplit>(new Action<AbstractEvents.IRowInserting<TSplit>>(this.EventHandler));
    ((RowInsertedEvents) this.Base.RowInserted).AddAbstractHandler<TSplit>(new Action<AbstractEvents.IRowInserted<TSplit>>(this.EventHandler));
    ((RowUpdatedEvents) this.Base.RowUpdated).AddAbstractHandler<TSplit>(new Action<AbstractEvents.IRowUpdated<TSplit>>(this.EventHandler));
    ((RowDeletedEvents) this.Base.RowDeleted).AddAbstractHandler<TSplit>(new Action<AbstractEvents.IRowDeleted<TSplit>>(this.EventHandler));
    ((RowPersistingEvents) this.Base.RowPersisting).AddAbstractHandler<TSplit>(new Action<AbstractEvents.IRowPersisting<TSplit>>(this.EventHandler));
    ((RowPersistedEvents) this.Base.RowPersisted).AddAbstractHandler<TSplit>(new Action<AbstractEvents.IRowPersisted<TSplit>>(this.EventHandler));
    if (this.SplitUomField != (Type) null)
      ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<TSplit, string>(this.SplitUomField.Name, new Action<AbstractEvents.IFieldDefaulting<TSplit, IBqlField, string>>(this.EventHandlerUOM));
    if (!(this.SplitQtyField != (Type) null) || !(((PXCache) this.SplitCache).GetAttributesReadonly(this.SplitQtyField.Name).OfType<PXDBQuantityAttribute>().Select<PXDBQuantityAttribute, Type>((Func<PXDBQuantityAttribute, Type>) (qa => qa.KeyField)).FirstOrDefault<Type>() != (Type) null))
      return;
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<TSplit, Decimal>(this.SplitQtyField.Name, new Action<AbstractEvents.IFieldVerifying<TSplit, IBqlField, Decimal?>>(this.EventHandlerQty));
  }

  protected virtual void EventHandler(AbstractEvents.IRowInserting<TSplit> e)
  {
    if (!string.IsNullOrEmpty(e.Row.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(e.Row.AssignedNbr, e.Row.LotSerialNbr))
      return;
    if (this.SuppressedMode && this.CurrentOperation == 2)
      this.UpdateCounters(EnumerableEx.Ensure<TLine, LSSelect.Counters>((IDictionary<TLine, LSSelect.Counters>) this.LineCounters, this.LineCurrent, (Func<LSSelect.Counters>) (() => new LSSelect.Counters())), e.Row);
    PX.Objects.IN.InventoryItem inventoryItem1;
    INLotSerClass inLotSerClass;
    Decimal? nullable1;
    Decimal? nullable2;
    if (this.SuppressedMode && this.CurrentOperation == 1)
    {
      TSplit split1 = ((IEnumerable<TSplit>) this.SelectSplits(e.Row)).FirstOrDefault<TSplit>((Func<TSplit, bool>) (s => this.AreSplitsEqual(e.Row, s)));
      if ((object) split1 != null)
      {
        this.ReadInventoryItem(((ILSMaster) e.Row).InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass);
        INLotSerClass lotSerClass = inLotSerClass;
        if (!(lotSerClass.LotSerTrack != "S"))
        {
          nullable1 = split1.BaseQty;
          Decimal num = 0M;
          if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue) && this.GetTranTrackMode((ILSMaster) e.Row, lotSerClass) != INLotSerTrack.Mode.None)
            goto label_9;
        }
        TSplit split2 = this.Clone(split1);
        ref TSplit local1 = ref split1;
        // ISSUE: variable of a boxed type
        __Boxed<TSplit> local2 = (object) local1;
        nullable1 = local1.BaseQty;
        nullable2 = e.Row.BaseQty;
        Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        local2.BaseQty = nullable3;
        this.SetSplitQtyWithLine(split1, default (TLine));
        ((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache.RaiseRowUpdated((object) split1, (object) split2);
        GraphHelper.MarkUpdated(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) split1, true);
        PXDBQuantityAttribute.VerifyForDecimal(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) split1);
label_9:
        ((ICancelEventArgs) e).Cancel = true;
        PXCacheEx.Adjust<PXLineNbrAttribute>(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) null).For("SplitLineNbr", (Action<PXLineNbrAttribute>) (a => a.ClearLastDefaultValue()));
      }
    }
    if (!((ILSMaster) e.Row).InventoryID.HasValue || string.IsNullOrEmpty(e.Row.UOM))
    {
      ((ICancelEventArgs) e).Cancel = true;
      PXCacheEx.Adjust<PXLineNbrAttribute>(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) null).For("SplitLineNbr", (Action<PXLineNbrAttribute>) (a => a.ClearLastDefaultValue()));
    }
    if (((ICancelEventArgs) e).Cancel)
      return;
    this.ReadInventoryItem(((ILSMaster) e.Row).InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass);
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    INLotSerClass lotSerClass1 = inLotSerClass;
    if (this.GetTranTrackMode((ILSMaster) e.Row, lotSerClass1) != INLotSerTrack.Mode.None && lotSerClass1.LotSerTrack == "S")
    {
      nullable2 = e.Row.Qty;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      {
        nullable2 = this.LineCurrent.UnassignedQty;
        Decimal num2 = (Decimal) 1;
        if (nullable2.GetValueOrDefault() >= num2 & nullable2.HasValue)
          e.Row.Qty = new Decimal?((Decimal) 1);
      }
    }
    nullable2 = e.Row.BaseQty;
    if (nullable2.HasValue)
    {
      nullable2 = e.Row.BaseQty;
      Decimal num = 0M;
      if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
      {
        nullable2 = e.Row.BaseQty;
        nullable1 = e.Row.Qty;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !(e.Row.UOM != inventoryItem2.BaseUnit))
          goto label_21;
      }
    }
    // ISSUE: variable of a boxed type
    __Boxed<TSplit> row = (object) e.Row;
    PXCache cache = ((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache;
    int? inventoryId = ((ILSMaster) e.Row).InventoryID;
    string uom = e.Row.UOM;
    nullable1 = e.Row.Qty;
    Decimal valueOrDefault = nullable1.GetValueOrDefault();
    Decimal? baseQty = e.Row.BaseQty;
    Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertToBase(cache, inventoryId, uom, valueOrDefault, baseQty, INPrecision.QUANTITY));
    row.BaseQty = nullable4;
label_21:
    e.Row.UOM = inventoryItem2.BaseUnit;
    e.Row.Qty = e.Row.BaseQty;
  }

  protected virtual void EventHandler(AbstractEvents.IRowInserted<TSplit> e)
  {
    if (this.SuppressedMode)
      return;
    e.Row.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache, ((ILSMaster) e.Row).InventoryID, e.Row.UOM, e.Row.Qty.Value, e.Row.BaseQty, INPrecision.QUANTITY));
    this.DefaultLotSerialNbr(e.Row);
    if (!this.UnattendedMode)
      e.Row.ExpireDate = this.ExpireDateByLot((ILSMaster) e.Row, (ILSMaster) null);
    using (this.SuppressedModeScope())
    {
      TLine line = this.UpdateParent(e.Row, default (TSplit));
      if (this.UnattendedMode)
        return;
      this.Availability?.Check((ILSMaster) e.Row, line.CostCenterID);
    }
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<TSplit> e)
  {
    this.ExpireCachedItems(e.OldRow);
    if (this.SuppressedMode)
      return;
    if (!string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
      e.Row.ExpireDate = this.ExpireDateByLot((ILSMaster) e.Row, (ILSMaster) null);
    e.Row.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, ((ILSMaster) e.Row).InventoryID, e.Row.UOM, e.Row.Qty.Value, e.Row.BaseQty, INPrecision.QUANTITY));
    using (this.SuppressedModeScope())
    {
      TLine line = this.UpdateParent(e.Row, e.OldRow);
      if (this.UnattendedMode)
        return;
      this.Availability?.Check((ILSMaster) e.Row, line.CostCenterID);
    }
  }

  protected virtual void EventHandler(AbstractEvents.IRowDeleted<TSplit> e)
  {
    this.ExpireCachedItems(e.Row);
    if (this.SuppressedMode)
      return;
    using (this.SuppressedModeScope())
      this.UpdateParent(default (TSplit), e.Row);
  }

  protected Dictionary<TSplit, string> PersistedSplitsToLotSerialNbrs { get; } = new Dictionary<TSplit, string>();

  public virtual void EventHandler(AbstractEvents.IRowPersisting<TSplit> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || string.IsNullOrEmpty(e.Row.AssignedNbr) || !INLotSerialNbrAttribute.StringsEqual(e.Row.AssignedNbr, e.Row.LotSerialNbr))
      return;
    string number = string.Empty;
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) e.Row).InventoryID);
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    try
    {
      number = AutoNumberAttribute.NextNumber(lotSerNum?.LotSerNumVal, false);
    }
    catch (AutoNumberException ex)
    {
      this.ThrowEmptyLotSerNumVal(e.Row);
    }
    e.Row.LotSerialNbr = INLotSerialNbrAttribute.UpdateNumber(e.Row.AssignedNbr, e.Row.LotSerialNbr, number);
    try
    {
      this.PersistedSplitsToLotSerialNbrs.Add(e.Row, e.Row.LotSerialNbr);
    }
    catch (ArgumentException ex)
    {
      this.ThrowEmptyLotSerNumVal(e.Row);
    }
    this.UpdateLotSerNumVal(lotSerNum, number, pxResult);
    ((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseRowUpdated((object) e.Row, (object) this.Clone(e.Row));
  }

  public virtual void EventHandler(AbstractEvents.IRowPersisted<TSplit> e)
  {
    if (e.TranStatus == 2)
    {
      if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) && this.PersistedSplitsToLotSerialNbrs.ContainsKey(e.Row))
      {
        e.Row.LotSerialNbr = INLotSerialNbrAttribute.MakeNumber(e.Row.AssignedNbr, e.Row.LotSerialNbr, this.Base.Accessinfo.BusinessDate.Value);
        this.PersistedSplitsToLotSerialNbrs.Remove(e.Row);
      }
      if (this.UnattendedMode || !(e.Exception is PXOuterException exception) || (object) e.Row != exception.Row)
        return;
      TLine line = this.SelectLine(e.Row);
      foreach ((string, string) tuple in ((IEnumerable<string>) exception.InnerFields).Zip<string, string, (string, string)>((IEnumerable<string>) exception.InnerMessages, (Func<string, string, (string, string)>) ((f, m) => (f, m))))
      {
        if (!((PXCache) this.LineCache).RaiseExceptionHandling(tuple.Item1, (object) line, (object) null, (Exception) new PXSetPropertyException((IBqlTable) line, tuple.Item2)))
          exception.InnerRemove(tuple.Item1);
      }
    }
    else
    {
      if (e.TranStatus != 1)
        return;
      this.PersistedSplitsToLotSerialNbrs.Remove(e.Row);
    }
  }

  public virtual void EventHandlerUOM(
    AbstractEvents.IFieldDefaulting<TSplit, IBqlField, string> e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) e.Row).InventoryID);
    if (pxResult == null)
      return;
    e.NewValue = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit;
    ((ICancelEventArgs) e).Cancel = true;
  }

  public virtual void EventHandlerQty(
    AbstractEvents.IFieldVerifying<TSplit, IBqlField, Decimal?> e)
  {
    if (!this.IsTrackSerial(e.Row))
      return;
    Decimal? newValue = e.NewValue;
    if (!newValue.HasValue)
      return;
    Decimal valueOrDefault = newValue.GetValueOrDefault();
    if (!(valueOrDefault != 0M) || !(valueOrDefault != 1M))
      return;
    e.NewValue = new Decimal?(1M);
  }

  protected virtual void SubscribeForLotSerOptionsEvents()
  {
    ((RowPersistingEvents) this.Base.RowPersisting).AddAbstractHandler<LSSelect.LotSerOptions>(new Action<AbstractEvents.IRowPersisting<LSSelect.LotSerOptions>>(this.EventHandler));
    ((RowSelectedEvents) this.Base.RowSelected).AddAbstractHandler<LSSelect.LotSerOptions>(new Action<AbstractEvents.IRowSelected<LSSelect.LotSerOptions>>(this.EventHandler));
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<LSSelect.LotSerOptions, LSSelect.LotSerOptions.startNumVal, string>(new Action<AbstractEvents.IFieldVerifying<LSSelect.LotSerOptions, LSSelect.LotSerOptions.startNumVal, string>>(this.EventHandler));
    ((FieldSelectingEvents) this.Base.FieldSelecting).AddAbstractHandler<LSSelect.LotSerOptions, LSSelect.LotSerOptions.startNumVal>(new Action<AbstractEvents.IFieldSelecting<LSSelect.LotSerOptions, LSSelect.LotSerOptions.startNumVal>>(this.EventHandler));
  }

  protected virtual void EventHandler(
    AbstractEvents.IRowSelected<LSSelect.LotSerOptions> e)
  {
    bool flag = this.IsLotSerOptionsEnabled(e.Row);
    PXUIFieldAttribute.SetEnabled<LSSelect.LotSerOptions.startNumVal>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<LSSelect.LotSerOptions.qty>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, flag);
    PXDBDecimalAttribute.SetPrecision(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, "Qty", new int?(e.Row.IsSerial.GetValueOrDefault() ? 0 : CommonSetupDecPl.Qty));
    if (!string.IsNullOrEmpty(e.Row?.ExtensionName) && !(this.TypePrefixed("LotSerOptions") == e.Row.ExtensionName))
      return;
    PXAction generateNumbers = this.generateNumbers;
    LSSelect.LotSerOptions row = e.Row;
    int num = (row != null ? (row.AllowGenerate.GetValueOrDefault() ? 1 : 0) : 0) & (flag ? 1 : 0);
    generateNumbers.SetEnabled(num != 0);
  }

  protected virtual void EventHandler(
    AbstractEvents.IRowPersisting<LSSelect.LotSerOptions> e)
  {
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldVerifying<LSSelect.LotSerOptions, LSSelect.LotSerOptions.startNumVal, string> e)
  {
    if ((object) this.LineCurrent == null)
      return;
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) this.LineCurrent).InventoryID);
    LSSelect.LotSerOptions current = (LSSelect.LotSerOptions) ((PXCache) GraphHelper.Caches<LSSelect.LotSerOptions>((PXGraph) this.Base)).Current;
    if (pxResult == null || current == null)
      return;
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    INLotSerialNbrAttribute.LSParts lsParts = INLotSerialNbrAttribute.GetLSParts((PXCache) this.LineCache, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum);
    if (string.IsNullOrEmpty(e.NewValue) || e.NewValue.Length < lsParts.len)
    {
      current.StartNumVal = (string) null;
      throw new PXSetPropertyException("Lot/Serial Number must be {0} characters long", new object[1]
      {
        (object) lsParts.len
      });
    }
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldSelecting<LSSelect.LotSerOptions, LSSelect.LotSerOptions.startNumVal> e)
  {
    if (e.Row == null || e.Row.StartNumVal == null || (object) this.LineCurrent == null)
      return;
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) this.LineCurrent).InventoryID);
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    string displayMask = INLotSerialNbrAttribute.GetDisplayMask((PXCache) this.LineCache, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum);
    if (displayMask == null)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(displayMask.Length), new bool?(true), "StartNumVal", new bool?(false), new int?(1), displayMask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
  }

  private void InitializeLotSerNumVals()
  {
    this.Base.OnBeforePersist += new Action<PXGraph>(this.StoreLotSerNumVals);
    GraphHelper.EnsureCachePersistence<INLotSerClassLotSerNumVal>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<InventoryItemLotSerNumVal>((PXGraph) this.Base);
  }

  private bool RestoreLotSerNumbers()
  {
    if (this._lotSerNumVals == null)
      return false;
    PXCache<INLotSerClassLotSerNumVal> cache1 = GraphHelper.Caches<INLotSerClassLotSerNumVal>((PXGraph) this.Base);
    ((PXCache) cache1).Current = (object) null;
    RestoreLotSerNumVals((PXCache) cache1, ((PXCache) cache1).Cached);
    ((PXCache) cache1).Normalize();
    ((PXCache) cache1).ClearQueryCache();
    PXCache<InventoryItemLotSerNumVal> cache2 = GraphHelper.Caches<InventoryItemLotSerNumVal>((PXGraph) this.Base);
    ((PXCache) cache2).Current = (object) null;
    RestoreLotSerNumVals((PXCache) cache2, ((PXCache) cache2).Cached);
    ((PXCache) cache2).Normalize();
    ((PXCache) cache2).ClearQueryCache();
    this._lotSerNumVals = (Dictionary<ILotSerNumVal, ILotSerNumVal>) null;
    return true;

    void RestoreLotSerNumVals(PXCache cache, IEnumerable numbersCollection)
    {
      foreach (ILotSerNumVal key in numbersCollection.OfType<ILotSerNumVal>().ToList<ILotSerNumVal>())
      {
        ILotSerNumVal lotSerNumVal;
        if (this._lotSerNumVals.TryGetValue(key, out lotSerNumVal))
        {
          cache.RestoreCopy((object) key, (object) lotSerNumVal);
          this._lotSerNumVals.Remove(key);
        }
        else
          cache.Remove((object) key);
      }
    }
  }

  private void StoreLotSerNumVals(PXGraph graph)
  {
    this._lotSerNumVals = new Dictionary<ILotSerNumVal, ILotSerNumVal>();
    PXCache<INLotSerClassLotSerNumVal> cache1 = GraphHelper.Caches<INLotSerClassLotSerNumVal>(graph);
    StoreLotSerNumVals((PXCache) cache1, ((PXCache) cache1).Inserted);
    StoreLotSerNumVals((PXCache) cache1, ((PXCache) cache1).Updated);
    PXCache<InventoryItemLotSerNumVal> cache2 = GraphHelper.Caches<InventoryItemLotSerNumVal>(graph);
    StoreLotSerNumVals((PXCache) cache2, ((PXCache) cache2).Inserted);
    StoreLotSerNumVals((PXCache) cache2, ((PXCache) cache2).Updated);

    void StoreLotSerNumVals(PXCache cache, IEnumerable numbersCollection)
    {
      foreach (ILotSerNumVal numbers in numbersCollection)
        this._lotSerNumVals.Add(numbers, (ILotSerNumVal) cache.CreateCopy((object) numbers));
    }
  }

  public virtual void CreateNumbers(TLine line, Decimal deltaBaseQty)
  {
    this.CreateNumbers(line, deltaBaseQty, false);
  }

  public virtual void CreateNumbers(TLine line, Decimal deltaBaseQty, bool forceAutoNextNbr)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) line).InventoryID);
    TSplit split = this.LineToSplit(line);
    INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    if ((object) line != null)
      this.LineCounters.Remove(line);
    INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) line, inLotSerClass);
    int? nullable1;
    if (!forceAutoNextNbr && inLotSerClass.LotSerTrack == "S")
    {
      nullable1 = inLotSerClass.AutoSerialMaxCount;
      int num1 = 0;
      if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      {
        nullable1 = inLotSerClass.AutoSerialMaxCount;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = deltaBaseQty;
        if (nullable2.GetValueOrDefault() < num2 & nullable2.HasValue && (tranTrackMode & INLotSerTrack.Mode.Create) > INLotSerTrack.Mode.None)
        {
          nullable1 = inLotSerClass.AutoSerialMaxCount;
          deltaBaseQty = (Decimal) nullable1.GetValueOrDefault();
        }
      }
    }
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    foreach (TSplit number in INLotSerialNbrAttribute.CreateNumbers<TSplit>((PXCache) this.LineCache, inLotSerClass, lotSerNum, tranTrackMode, forceAutoNextNbr, deltaBaseQty))
    {
      string str = (tranTrackMode & INLotSerTrack.Mode.Create) > INLotSerTrack.Mode.None ? inLotSerClass.LotSerTrack : "N";
      // ISSUE: variable of a boxed type
      __Boxed<TSplit> local = (object) split;
      nullable1 = new int?();
      int? nullable3 = nullable1;
      local.SplitLineNbr = nullable3;
      split.LotSerialNbr = number.LotSerialNbr;
      split.AssignedNbr = number.AssignedNbr;
      split.LotSerClassID = number.LotSerClassID;
      if (split is ILSGeneratedDetail lsGeneratedDetail1 && number is ILSGeneratedDetail lsGeneratedDetail2)
        lsGeneratedDetail1.HasGeneratedLotSerialNbr = lsGeneratedDetail2.HasGeneratedLotSerialNbr;
      if (!string.IsNullOrEmpty(line.LotSerialNbr))
      {
        if (!(str == "L"))
        {
          if (str == "S")
          {
            Decimal? qty = line.Qty;
            Decimal num = 1M;
            if (!(qty.GetValueOrDefault() == num & qty.HasValue))
              goto label_15;
          }
          else
            goto label_15;
        }
        split.LotSerialNbr = line.LotSerialNbr;
      }
label_15:
      if (str == "S")
      {
        split.UOM = (string) null;
        split.Qty = new Decimal?(1M);
        split.BaseQty = new Decimal?(1M);
      }
      else
      {
        split.UOM = (string) null;
        split.BaseQty = new Decimal?(deltaBaseQty);
        split.Qty = new Decimal?(deltaBaseQty);
      }
      if (inLotSerClass.LotSerTrackExpiration.GetValueOrDefault())
        split.ExpireDate = this.ExpireDateByLot((ILSMaster) split, (ILSMaster) line);
      PXCache<TSplit>.Insert((PXGraph) this.Base, this.Clone(split));
      deltaBaseQty -= split.BaseQty.Value;
    }
    Decimal? nullable4;
    if (deltaBaseQty > 0M && (inLotSerClass.LotSerTrack != "S" || Decimal.Remainder(deltaBaseQty, 1M) == 0M))
    {
      ref TLine local1 = ref line;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local2 = (object) local1;
      nullable4 = local1.UnassignedQty;
      Decimal num = deltaBaseQty;
      Decimal? nullable5 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + num) : new Decimal?();
      local2.UnassignedQty = nullable5;
    }
    else if (deltaBaseQty > 0M)
    {
      TLine copy = PXCache<TLine>.CreateCopy(line);
      ref TLine local3 = ref line;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local4 = (object) local3;
      nullable4 = local3.BaseQty;
      Decimal num3 = deltaBaseQty;
      Decimal? nullable6 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - num3) : new Decimal?();
      local4.BaseQty = nullable6;
      this.SetLineQtyFromBase(line);
      nullable4 = copy.Qty;
      Decimal num4 = nullable4.Value;
      nullable4 = line.Qty;
      Decimal num5 = nullable4.Value;
      if (Math.Abs(num4 - num5) >= 0.0000005M)
      {
        ((PXCache) this.LineCache).RaiseFieldUpdated(this.LineQtyField.Name, (object) line, (object) copy.Qty);
        this.LineCache.RaiseRowUpdated(line, copy);
      }
    }
    nullable4 = line.UnassignedQty;
    Decimal num6 = 0M;
    if (!(nullable4.GetValueOrDefault() > num6 & nullable4.HasValue))
      return;
    this.RaiseUnassignedExceptionHandling(line);
  }

  public virtual void TruncateNumbers(TLine line, Decimal deltaBaseQty)
  {
    Decimal? nullable1;
    if (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(((ILSMaster) line).InventoryID)).LotSerTrack == "S" && Math.Abs(Decimal.Floor(deltaBaseQty) - deltaBaseQty) > 0.0000005M)
    {
      TLine copy = PXCache<TLine>.CreateCopy(line);
      ref TLine local1 = ref line;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local2 = (object) local1;
      nullable1 = local1.BaseQty;
      Decimal num = deltaBaseQty - Decimal.Truncate(deltaBaseQty);
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num) : new Decimal?();
      local2.BaseQty = nullable2;
      this.SetLineQtyFromBase(line);
      ((PXCache) this.LineCache).RaiseFieldUpdated(this.LineQtyField.Name, (object) line, (object) copy.Qty);
      this.LineCache.RaiseRowUpdated(line, copy);
      deltaBaseQty = Decimal.Truncate(deltaBaseQty);
    }
    if ((object) line != null)
      this.LineCounters.Remove(line);
    nullable1 = line.UnassignedQty;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
    {
      nullable1 = line.UnassignedQty;
      Decimal num2 = deltaBaseQty;
      if (nullable1.GetValueOrDefault() >= num2 & nullable1.HasValue)
      {
        ref TLine local3 = ref line;
        // ISSUE: variable of a boxed type
        __Boxed<TLine> local4 = (object) local3;
        nullable1 = local3.UnassignedQty;
        Decimal num3 = deltaBaseQty;
        Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num3) : new Decimal?();
        local4.UnassignedQty = nullable3;
        deltaBaseQty = 0M;
      }
      else
      {
        Decimal num4 = deltaBaseQty;
        nullable1 = line.UnassignedQty;
        Decimal num5 = nullable1.Value;
        deltaBaseQty = num4 - num5;
        line.UnassignedQty = new Decimal?(0M);
      }
    }
    foreach (TSplit split1 in this.SelectSplitsReversed(line))
    {
      Decimal num6 = deltaBaseQty;
      nullable1 = split1.BaseQty;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      if (num6 >= valueOrDefault & nullable1.HasValue)
      {
        Decimal num7 = deltaBaseQty;
        nullable1 = split1.BaseQty;
        Decimal num8 = nullable1.Value;
        deltaBaseQty = num7 - num8;
        this.SplitCache.Delete(split1);
        this.ExpireLotSerialStatusCacheFor(split1);
      }
      else
      {
        TSplit split2 = this.Clone(split1);
        ref TSplit local5 = ref split2;
        // ISSUE: variable of a boxed type
        __Boxed<TSplit> local6 = (object) local5;
        nullable1 = local5.BaseQty;
        Decimal num9 = deltaBaseQty;
        Decimal? nullable4 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num9) : new Decimal?();
        local6.BaseQty = nullable4;
        this.SetSplitQtyWithLine(split2, line);
        this.SplitCache.Update(split2);
        this.ExpireLotSerialStatusCacheFor(split1);
        break;
      }
    }
  }

  public virtual void UpdateNumbers(TLine line)
  {
    if ((object) line != null)
      this.LineCounters.Remove(line);
    foreach (TSplit selectSplit in this.SelectSplits(line))
    {
      TSplit split = this.Clone(selectSplit);
      int? nullable1 = line.LocationID;
      if (!nullable1.HasValue)
      {
        nullable1 = split.LocationID;
        if (nullable1.HasValue && this.SplitCache.GetStatus(split) == 2)
        {
          Decimal? qty = split.Qty;
          Decimal num = 0M;
          if (qty.GetValueOrDefault() == num & qty.HasValue)
          {
            this.SplitCache.Delete(split);
            continue;
          }
        }
      }
      // ISSUE: variable of a boxed type
      __Boxed<TSplit> local1 = (object) split;
      nullable1 = line.SubItemID;
      int? nullable2 = nullable1 ?? split.SubItemID;
      local1.SubItemID = nullable2;
      ((ILSMaster) split).SiteID = ((ILSMaster) line).SiteID;
      // ISSUE: variable of a boxed type
      __Boxed<TSplit> local2 = (object) split;
      nullable1 = line.LocationID;
      int? nullable3 = nullable1 ?? split.LocationID;
      local2.LocationID = nullable3;
      split.ExpireDate = this.ExpireDateByLot((ILSMaster) split, (ILSMaster) line);
      this.SplitCache.Update(split);
    }
  }

  public virtual void EnsureSingleSplit(TLine line, Decimal deltaBaseQty)
  {
    bool flag = false;
    if ((object) line != null)
      this.LineCounters.Remove(line);
    if (this.CurrentOperation == 1)
    {
      foreach (TSplit selectSplit in this.SelectSplits(line))
      {
        if (flag)
        {
          this.SplitCache.Delete(selectSplit);
          this.ExpireLotSerialStatusCacheFor(selectSplit);
        }
        else
        {
          TSplit split = this.Clone(selectSplit);
          split.SubItemID = line.SubItemID;
          ((ILSMaster) split).SiteID = ((ILSMaster) line).SiteID;
          split.LocationID = line.LocationID;
          split.LotSerialNbr = line.LotSerialNbr;
          split.ExpireDate = this.ExpireDateByLot((ILSMaster) split, (ILSMaster) line);
          split.BaseQty = line.BaseQty;
          this.SetSplitQtyWithLine(split, line);
          this.SplitCache.Update(split);
          this.ExpireLotSerialStatusCacheFor(selectSplit);
          flag = true;
        }
      }
    }
    if (flag)
      return;
    TSplit split1 = this.LineToSplit(line);
    split1.SplitLineNbr = new int?();
    split1.ExpireDate = this.ExpireDateByLot((ILSMaster) split1, (ILSMaster) line);
    this.DefaultLotSerialNbr(split1);
    if (string.IsNullOrEmpty(split1.LotSerialNbr) && !string.IsNullOrEmpty(line.LotSerialNbr))
      split1.LotSerialNbr = line.LotSerialNbr;
    this.SplitCache.Insert(split1);
    this.ExpireLotSerialStatusCacheFor(split1);
  }

  public virtual void IssueNumbers(TLine line, Decimal deltaBaseQty)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) line).InventoryID);
    PXDBOperation currentOperation = this.CurrentOperation;
    if (this.CurrentOperation == 1 && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S" && ((IEnumerable<TSplit>) this.SelectSplits(line)).Count<TSplit>() == 0)
      this.CurrentOperation = (PXDBOperation) 0;
    try
    {
      this.IssueNumbersInternal(line, deltaBaseQty);
    }
    finally
    {
      this.CurrentOperation = currentOperation;
    }
  }

  protected virtual void IssueNumbersInternal(TLine line, Decimal deltaBaseQty)
  {
    this.IssueNumbers(line, deltaBaseQty, (PXCache) GraphHelper.Caches<INLotSerialStatusByCostCenter>((PXGraph) this.Base), (PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>((PXGraph) this.Base));
  }

  protected void IssueNumbers(
    TLine line,
    Decimal deltaBaseQty,
    PXCache statusCache,
    PXCache statusAccumCache)
  {
    if ((object) line != null)
      this.LineCounters.Remove(line);
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) line).InventoryID);
    (Decimal deltaBaseQty1, TSplit lastSplit) = this.IssueLotSerials(line, deltaBaseQty, statusCache, statusAccumCache, pxResult);
    this.HandleRemainder(line, deltaBaseQty1, lastSplit, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
  }

  protected virtual (Decimal, TSplit) IssueLotSerials(
    TLine line,
    Decimal deltaBaseQty,
    PXCache statusCache,
    PXCache statusAccumCache,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    TSplit split = this.LineToSplit(line);
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item);
    if ((this.GetTranTrackMode((ILSMaster) line, lotSerClass) & INLotSerTrack.Mode.Issue) > INLotSerTrack.Mode.None)
    {
      List<ILotSerial> lotSerialStatuses = this.SelectSerialStatus(line, item);
      MoveItemToTopOfList(lotSerialStatuses, line.LotSerialNbr);
      foreach (ILotSerial lotSerial1 in lotSerialStatuses)
      {
        split.SplitLineNbr = new int?();
        split.SubItemID = lotSerial1.SubItemID;
        split.LocationID = lotSerial1.LocationID;
        split.LotSerialNbr = lotSerial1.LotSerialNbr;
        split.ExpireDate = lotSerial1.ExpireDate;
        split.UOM = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item).BaseUnit;
        Decimal? nullable;
        if (this.UseAvailabilityToIssueLotSerials(line, split))
        {
          IStatus instance = (IStatus) statusAccumCache.CreateInstance();
          statusCache.RestoreCopy((object) instance, (object) lotSerial1);
          IStatus accumavail = (IStatus) statusAccumCache.Insert((object) instance);
          Decimal? statusAvailableQty = this.GetSerialStatusAvailableQty(lotSerial1, accumavail);
          nullable = statusAvailableQty;
          Decimal num1 = 0M;
          if (!(nullable.GetValueOrDefault() <= num1 & nullable.HasValue))
          {
            nullable = statusAvailableQty;
            Decimal num2 = deltaBaseQty;
            if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
            {
              split.BaseQty = statusAvailableQty;
              deltaBaseQty -= statusAvailableQty.Value;
            }
            else if (lotSerClass.LotSerTrack == "S")
            {
              split.BaseQty = new Decimal?(1M);
              deltaBaseQty -= 1M;
            }
            else
            {
              split.BaseQty = new Decimal?(deltaBaseQty);
              deltaBaseQty = 0M;
            }
          }
          else
            continue;
        }
        else
        {
          if (statusCache.GetStatus((object) lotSerial1) == null)
            this.Summarize(statusCache, line, lotSerial1);
          Decimal? serialStatusQtyOnHand = this.GetSerialStatusQtyOnHand(lotSerial1);
          nullable = serialStatusQtyOnHand;
          Decimal num3 = 0M;
          if (!(nullable.GetValueOrDefault() <= num3 & nullable.HasValue))
          {
            nullable = serialStatusQtyOnHand;
            Decimal num4 = deltaBaseQty;
            if (nullable.GetValueOrDefault() <= num4 & nullable.HasValue)
            {
              split.BaseQty = serialStatusQtyOnHand;
              deltaBaseQty -= serialStatusQtyOnHand.Value;
            }
            else if (lotSerClass.LotSerTrack == "S")
            {
              split.BaseQty = new Decimal?(1M);
              deltaBaseQty -= 1M;
            }
            else
            {
              split.BaseQty = new Decimal?(deltaBaseQty);
              deltaBaseQty = 0M;
            }
            ILotSerial lotSerial2 = lotSerial1;
            nullable = lotSerial2.QtyOnHand;
            Decimal? baseQty = split.BaseQty;
            lotSerial2.QtyOnHand = nullable.HasValue & baseQty.HasValue ? new Decimal?(nullable.GetValueOrDefault() - baseQty.GetValueOrDefault()) : new Decimal?();
            statusCache.SetStatus((object) lotSerial1, (PXEntryStatus) 5);
          }
          else
            continue;
        }
        this.SetSplitQtyWithLine(split, line);
        PXCache<TSplit>.Insert((PXGraph) this.Base, this.Clone(split));
        if (deltaBaseQty <= 0M)
          break;
      }
    }
    return (deltaBaseQty, split);

    static void MoveItemToTopOfList(List<ILotSerial> lotSerialStatuses, string lotSerialNbr)
    {
      if (string.IsNullOrEmpty(lotSerialNbr))
        return;
      int index = lotSerialStatuses.FindIndex((Predicate<ILotSerial>) (x => string.Equals(x.LotSerialNbr?.Trim(), lotSerialNbr.Trim(), StringComparison.InvariantCultureIgnoreCase)));
      if (index <= 0)
        return;
      ILotSerial lotSerialStatuse = lotSerialStatuses[index];
      lotSerialStatuses.RemoveAt(index);
      lotSerialStatuses.Insert(0, lotSerialStatuse);
    }
  }

  protected virtual void HandleRemainder(
    TLine line,
    Decimal deltaBaseQty,
    TSplit lastSplit,
    INLotSerClass lotSerClass)
  {
    if (deltaBaseQty > 0M)
    {
      int? nullable1 = ((ILSMaster) line).InventoryID;
      if (nullable1.HasValue)
      {
        nullable1 = line.SubItemID;
        if (nullable1.HasValue)
        {
          nullable1 = ((ILSMaster) line).SiteID;
          if (nullable1.HasValue)
          {
            nullable1 = line.LocationID;
            if (nullable1.HasValue && !string.IsNullOrEmpty(line.LotSerialNbr) && !lotSerClass.IsManualAssignRequired.GetValueOrDefault() && lotSerClass.LotSerTrack != "S")
            {
              // ISSUE: variable of a boxed type
              __Boxed<TSplit> local1 = (object) lastSplit;
              nullable1 = new int?();
              int? nullable2 = nullable1;
              local1.SplitLineNbr = nullable2;
              lastSplit.BaseQty = new Decimal?(deltaBaseQty);
              this.SetSplitQtyWithLine(lastSplit, line);
              lastSplit.ExpireDate = this.ExpireDateByLot((ILSMaster) lastSplit, (ILSMaster) null);
              try
              {
                PXCache<TSplit>.Insert((PXGraph) this.Base, this.Clone(lastSplit));
              }
              catch
              {
                ref TLine local2 = ref line;
                // ISSUE: variable of a boxed type
                __Boxed<TLine> local3 = (object) local2;
                Decimal? unassignedQty = local2.UnassignedQty;
                Decimal num = deltaBaseQty;
                Decimal? nullable3 = unassignedQty.HasValue ? new Decimal?(unassignedQty.GetValueOrDefault() + num) : new Decimal?();
                local3.UnassignedQty = nullable3;
                this.RaiseUnassignedExceptionHandling(line);
              }
              finally
              {
                deltaBaseQty = 0M;
              }
            }
          }
        }
      }
    }
    if (!(deltaBaseQty != 0M))
      return;
    bool flag = lotSerClass.LotSerTrack == "S" && Decimal.Remainder(deltaBaseQty, 1M) != 0M;
    if (flag || deltaBaseQty < 0M)
    {
      TLine copy = PXCache<TLine>.CreateCopy(line);
      ref TLine local4 = ref line;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local5 = (object) local4;
      Decimal? baseQty = local4.BaseQty;
      Decimal num = deltaBaseQty;
      Decimal? nullable = baseQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - num) : new Decimal?();
      local5.BaseQty = nullable;
      this.SetLineQtyFromBase(line);
      ((PXCache) this.LineCache).RaiseFieldUpdated(this.LineQtyField.Name, (object) line, (object) copy.Qty);
      this.LineCache.RaiseRowUpdated(line, copy);
      if (flag)
        ((PXCache) this.LineCache).RaiseExceptionHandling(this.LineQtyField.Name, (object) line, (object) null, (Exception) new PXSetPropertyException("Invalid quantity specified for serial item. Line quantity was changed to match.", (PXErrorLevel) 2));
      else
        ((PXCache) this.LineCache).RaiseExceptionHandling(this.LineQtyField.Name, (object) line, (object) null, (Exception) new PXSetPropertyException("Insufficient quantity available. Line quantity was changed to match.", (PXErrorLevel) 2));
    }
    else
    {
      ref TLine local6 = ref line;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local7 = (object) local6;
      Decimal? unassignedQty = local6.UnassignedQty;
      Decimal num = deltaBaseQty;
      Decimal? nullable = unassignedQty.HasValue ? new Decimal?(unassignedQty.GetValueOrDefault() + num) : new Decimal?();
      local7.UnassignedQty = nullable;
      this.RaiseUnassignedExceptionHandling(line);
    }
  }

  protected virtual bool UseAvailabilityToIssueLotSerials(TLine line, TSplit split)
  {
    Sign signQtyAvail = this.Availability.GetAvailabilitySigns<SiteLotSerial>(split).SignQtyAvail;
    return ((Sign) ref signQtyAvail).IsMinus;
  }

  public virtual void Summarize(PXCache statusCache, TLine line, ILotSerial lotSerialRow)
  {
    PXView view = this.Base.TypedViews.GetView(this.SplitByLotSerialStatusCommand, false);
    object[] objArray1 = new object[1]{ (object) line };
    object[] objArray2 = new object[5]
    {
      (object) lotSerialRow.InventoryID,
      (object) lotSerialRow.SubItemID,
      (object) lotSerialRow.SiteID,
      (object) lotSerialRow.LocationID,
      (object) lotSerialRow.LotSerialNbr
    };
    foreach (TSplit split in view.SelectMultiBound(objArray1, objArray2))
    {
      IStatus status = (IStatus) lotSerialRow;
      Decimal? qtyOnHand = status.QtyOnHand;
      short? invtMult = split.InvtMult;
      Decimal? nullable1 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = split.BaseQty;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable4;
      if (!(qtyOnHand.HasValue & nullable3.HasValue))
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(qtyOnHand.GetValueOrDefault() + nullable3.GetValueOrDefault());
      status.QtyOnHand = nullable4;
    }
    statusCache.SetStatus((object) lotSerialRow, (PXEntryStatus) 5);
  }

  protected virtual Decimal? GetSerialStatusAvailableQty(ILotSerial lsmaster, IStatus accumavail)
  {
    Decimal? qtyAvail1 = ((IStatus) lsmaster).QtyAvail;
    Decimal? qtyAvail2 = accumavail.QtyAvail;
    return !(qtyAvail1.HasValue & qtyAvail2.HasValue) ? new Decimal?() : new Decimal?(qtyAvail1.GetValueOrDefault() + qtyAvail2.GetValueOrDefault());
  }

  protected virtual Decimal? GetSerialStatusQtyOnHand(ILotSerial lsmaster) => lsmaster.QtyOnHand;

  internal virtual List<ILotSerial> PerformSelectSerial<TLotSerialStatus>(
    PXSelectBase cmd,
    object[] pars)
    where TLotSerialStatus : ILotSerial, IBqlTable
  {
    return GraphHelper.RowCast<TLotSerialStatus>((IEnumerable) cmd.View.SelectMultiBound(pars, Array.Empty<object>())).Cast<ILotSerial>().ToList<ILotSerial>();
  }

  protected virtual INLotSerialStatus MakeINLotSerialStatus(ILSMaster item)
  {
    return new INLotSerialStatus()
    {
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      LocationID = item.LocationID,
      SubItemID = item.SubItemID,
      LotSerialNbr = item.LotSerialNbr
    };
  }

  protected virtual INLotSerialStatusByCostCenter MakeINLotSerialStatusByCostCenter(ILSMaster item)
  {
    INLotSerialStatusByCostCenter statusByCostCenter = new INLotSerialStatusByCostCenter()
    {
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      LocationID = item.LocationID,
      SubItemID = item.SubItemID,
      LotSerialNbr = item.LotSerialNbr
    };
    switch (item)
    {
      case TLine line2:
        statusByCostCenter.CostCenterID = line2.CostCenterID;
        break;
      case TSplit split:
        TLine line1 = this.SelectLine(split);
        statusByCostCenter.CostCenterID = new int?(((int?) line1?.CostCenterID).GetValueOrDefault());
        break;
    }
    return statusByCostCenter;
  }

  protected virtual void ExpireLotSerialStatusCacheFor(TSplit split)
  {
    this.ExpireCached<INLotSerialStatus>(this.MakeINLotSerialStatus((ILSMaster) split));
    this.ExpireCached<INLotSerialStatusByCostCenter>(this.MakeINLotSerialStatusByCostCenter((ILSMaster) split));
  }

  public virtual void UpdateParent(TLine line)
  {
    Decimal baseQty;
    this.UpdateParent(line, default (TSplit), default (TSplit), out baseQty);
    this.SetUnassignedQty(line, baseQty, true);
  }

  public virtual TLine UpdateParent(TSplit newSplit, TSplit oldSplit)
  {
    TSplit a = newSplit ?? oldSplit;
    TLine line = PXParentAttribute.SelectParent<TLine>((PXCache) this.SplitCache, (object) a);
    if ((object) line != null && (object) a != null && this.SameInventoryItem((ILSMaster) a, (ILSMaster) line))
    {
      TLine copy = PXCache<TLine>.CreateCopy(line);
      Decimal baseQty;
      this.UpdateParent(line, newSplit, oldSplit, out baseQty);
      using (this.InvtMultModeScope(line))
        this.SetUnassignedQty(line, baseQty, false);
      GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) line, true);
      if (Math.Abs(copy.Qty.Value - line.Qty.Value) >= 0.0000005M)
        ((PXCache) this.LineCache).RaiseFieldUpdated(this.LineQtyField.Name, (object) line, (object) copy.Qty);
      Decimal? nullable = copy.BaseQty;
      Decimal num1 = nullable.Value;
      nullable = line.BaseQty;
      Decimal num2 = nullable.Value;
      if (!(Math.Abs(num1 - num2) >= 0.0000005M))
      {
        nullable = copy.Qty;
        Decimal num3 = nullable.Value;
        nullable = line.Qty;
        Decimal num4 = nullable.Value;
        if (!(Math.Abs(num3 - num4) >= 0.0000005M))
          goto label_11;
      }
      this.LineCache.RaiseRowUpdated(line, copy);
    }
label_11:
    return line;
  }

  protected virtual void SetUnassignedQty(TLine line, Decimal detailsBaseQty, bool allowNegative)
  {
    Decimal num = detailsBaseQty;
    Decimal? baseQty = line.BaseQty;
    Decimal valueOrDefault = baseQty.GetValueOrDefault();
    if (num < valueOrDefault & baseQty.HasValue | allowNegative)
    {
      line.UnassignedQty = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(line.BaseQty.Value - detailsBaseQty)));
    }
    else
    {
      line.UnassignedQty = new Decimal?(0M);
      line.BaseQty = new Decimal?(detailsBaseQty);
      this.SetLineQtyFromBase(line);
    }
  }

  /// <summary>
  /// Gets flag which indicates that line lot/serial value should be populated if line splits have same lot/serial value not considering other conditions.
  /// </summary>
  protected virtual bool ForceLineSingleLotSerialPopulation(int? inventoryID) => false;

  public virtual void UpdateParent(
    TLine line,
    TSplit newSplit,
    TSplit oldSplit,
    out Decimal baseQty)
  {
    this.CurrentCounters = (LSSelect.Counters) null;
    LSSelect.Counters counters;
    if (!this.LineCounters.TryGetValue(line, out counters))
    {
      this.LineCounters[line] = this.CurrentCounters = new LSSelect.Counters();
      foreach (TSplit selectSplit in this.SelectSplits(line))
        this.UpdateCounters(this.CurrentCounters, selectSplit);
    }
    else
    {
      this.CurrentCounters = counters;
      if ((object) newSplit != null)
        this.UpdateCounters(this.CurrentCounters, newSplit);
      int? key1;
      if ((object) oldSplit != null)
      {
        --this.CurrentCounters.RecordCount;
        oldSplit.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase((PXCache) this.SplitCache, ((ILSMaster) oldSplit).InventoryID, oldSplit.UOM, oldSplit.Qty.Value, oldSplit.BaseQty, INPrecision.QUANTITY));
        this.CurrentCounters.BaseQty -= oldSplit.BaseQty.Value;
        if (!oldSplit.ExpireDate.HasValue)
          --this.CurrentCounters.ExpireDatesNull;
        else if (this.CurrentCounters.ExpireDates.ContainsKey(oldSplit.ExpireDate) && --this.CurrentCounters.ExpireDates[oldSplit.ExpireDate] == 0)
          this.CurrentCounters.ExpireDates.Remove(oldSplit.ExpireDate);
        key1 = oldSplit.SubItemID;
        if (!key1.HasValue)
          --this.CurrentCounters.SubItemsNull;
        else if (this.CurrentCounters.SubItems.ContainsKey(oldSplit.SubItemID))
        {
          Dictionary<int?, int> subItems = this.CurrentCounters.SubItems;
          key1 = oldSplit.SubItemID;
          if (--subItems[key1] == 0)
            this.CurrentCounters.SubItems.Remove(oldSplit.SubItemID);
        }
        key1 = oldSplit.LocationID;
        if (!key1.HasValue)
          --this.CurrentCounters.LocationsNull;
        else if (this.CurrentCounters.Locations.ContainsKey(oldSplit.LocationID))
        {
          Dictionary<int?, int> locations = this.CurrentCounters.Locations;
          key1 = oldSplit.LocationID;
          if (--locations[key1] == 0)
            this.CurrentCounters.Locations.Remove(oldSplit.LocationID);
        }
        key1 = oldSplit.TaskID;
        if (!key1.HasValue)
        {
          --this.CurrentCounters.ProjectTasksNull;
        }
        else
        {
          KeyValuePair<int?, int?> key2 = new KeyValuePair<int?, int?>(oldSplit.ProjectID, oldSplit.TaskID);
          if (this.CurrentCounters.ProjectTasks.ContainsKey(key2) && --this.CurrentCounters.ProjectTasks[key2] == 0)
            this.CurrentCounters.ProjectTasks.Remove(key2);
        }
        if (oldSplit.LotSerialNbr == null)
          --this.CurrentCounters.LotSerNumbersNull;
        else if (this.CurrentCounters.LotSerNumbers.ContainsKey(oldSplit.LotSerialNbr))
        {
          if (!string.IsNullOrEmpty(oldSplit.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(oldSplit.AssignedNbr, oldSplit.LotSerialNbr))
            --this.CurrentCounters.UnassignedNumber;
          if (--this.CurrentCounters.LotSerNumbers[oldSplit.LotSerialNbr] == 0)
            this.CurrentCounters.LotSerNumbers.Remove(oldSplit.LotSerialNbr);
        }
      }
      if ((object) newSplit == null && (object) oldSplit != null)
      {
        if (this.CurrentCounters.ExpireDates.Count == 1 && this.CurrentCounters.ExpireDatesNull == 0)
        {
          foreach (DateTime? key3 in this.CurrentCounters.ExpireDates.Keys)
            this.CurrentCounters.ExpireDate = key3;
        }
        if (this.CurrentCounters.SubItems.Count == 1 && this.CurrentCounters.SubItemsNull == 0)
        {
          foreach (int? key4 in this.CurrentCounters.SubItems.Keys)
            this.CurrentCounters.SubItem = key4;
        }
        if (this.CurrentCounters.Locations.Count == 1 && this.CurrentCounters.LocationsNull == 0)
        {
          foreach (int? key5 in this.CurrentCounters.Locations.Keys)
            this.CurrentCounters.Location = key5;
        }
        if (this.CurrentCounters.ProjectTasks.Count == 1 && this.CurrentCounters.ProjectTasksNull == 0)
        {
          foreach (KeyValuePair<int?, int?> key6 in this.CurrentCounters.ProjectTasks.Keys)
          {
            LSSelect.Counters currentCounters1 = this.CurrentCounters;
            LSSelect.Counters currentCounters2 = this.CurrentCounters;
            ref int? local1 = ref key1;
            int? nullable;
            ref int? local2 = ref nullable;
            EnumerableExtensions.Deconstruct<int?, int?>(key6, ref local1, ref local2);
            currentCounters1.ProjectID = key1;
            currentCounters2.TaskID = nullable;
          }
        }
        if (this.CurrentCounters.LotSerNumbers.Count == 1 && this.CurrentCounters.LotSerNumbersNull == 0)
        {
          foreach (string key7 in this.CurrentCounters.LotSerNumbers.Keys)
            this.CurrentCounters.LotSerNumber = key7;
        }
      }
    }
    baseQty = this.CurrentCounters.BaseQty;
    switch (this.CurrentCounters.RecordCount)
    {
      case 0:
        line.LotSerialNbr = string.Empty;
        break;
      case 1:
        line.ExpireDate = this.CurrentCounters.ExpireDate;
        line.SubItemID = this.CurrentCounters.SubItem;
        line.LocationID = this.CurrentCounters.Location;
        line.LotSerialNbr = this.CurrentCounters.LotSerNumber;
        if (this.CurrentCounters.ProjectTasks.Count <= 0 || (object) newSplit == null || !this.CurrentCounters.ProjectID.HasValue)
          break;
        line.ProjectID = this.CurrentCounters.ProjectID;
        line.TaskID = this.CurrentCounters.TaskID;
        break;
      default:
        line.ExpireDate = this.CurrentCounters.ExpireDates.Count != 1 || this.CurrentCounters.ExpireDatesNull != 0 ? new DateTime?() : this.CurrentCounters.ExpireDate;
        line.SubItemID = this.CurrentCounters.SubItems.Count != 1 || this.CurrentCounters.SubItemsNull != 0 ? new int?() : this.CurrentCounters.SubItem;
        line.LocationID = this.CurrentCounters.Locations.Count != 1 || this.CurrentCounters.LocationsNull != 0 ? new int?() : this.CurrentCounters.Location;
        if (this.CurrentCounters.ProjectID.HasValue)
        {
          line.ProjectID = this.CurrentCounters.ProjectID;
          line.TaskID = this.CurrentCounters.TaskID;
        }
        PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) line).InventoryID);
        INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) line, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
        if (tranTrackMode == INLotSerTrack.Mode.None)
        {
          line.LotSerialNbr = string.Empty;
          break;
        }
        if (!this.ForceLineSingleLotSerialPopulation(((ILSMaster) line).InventoryID) && ((tranTrackMode & INLotSerTrack.Mode.Create) > INLotSerTrack.Mode.None || (tranTrackMode & INLotSerTrack.Mode.Issue) > INLotSerTrack.Mode.None))
        {
          line.LotSerialNbr = (string) null;
          break;
        }
        line.LotSerialNbr = this.CurrentCounters.LotSerNumbers.Count != 1 || this.CurrentCounters.LotSerNumbersNull != 0 ? (string) null : this.CurrentCounters.LotSerNumber;
        break;
    }
  }

  protected virtual TLine SelectLine(TSplit split)
  {
    return PXParentAttribute.SelectParent<TLine>((PXCache) this.SplitCache, (object) split);
  }

  protected virtual TSplit[] SelectSplits(TLine line, bool compareInventoryID = true)
  {
    return PXParentAttribute.SelectChildren((PXCache) this.SplitCache, (object) line, typeof (TLine)).Cast<TSplit>().Where<TSplit>((Func<TSplit, bool>) (split => !compareInventoryID || this.SameInventoryItem((ILSMaster) split, (ILSMaster) line))).ToArray<TSplit>();
  }

  protected virtual TSplit[] SelectSplits(TSplit split)
  {
    return PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) split, typeof (TLine)).Cast<TSplit>().Where<TSplit>((Func<TSplit, bool>) (sibling => this.SameInventoryItem((ILSMaster) sibling, (ILSMaster) split))).ToArray<TSplit>();
  }

  protected TSplit[] SelectSplitsOrdered(TLine line)
  {
    return this.SelectSplitsOrdered(this.LineToSplit(line));
  }

  protected virtual TSplit[] SelectSplitsOrdered(TSplit split)
  {
    return ((IEnumerable<TSplit>) this.SelectSplits(split)).OrderBy<TSplit, int?>((Func<TSplit, int?>) (s => s.SplitLineNbr)).ToArray<TSplit>();
  }

  protected TSplit[] SelectSplitsReversed(TLine line)
  {
    return this.SelectSplitsReversed(this.LineToSplit(line));
  }

  protected virtual TSplit[] SelectSplitsReversed(TSplit split)
  {
    return ((IEnumerable<TSplit>) this.SelectSplits(split)).OrderByDescending<TSplit, int?>((Func<TSplit, int?>) (s => s.SplitLineNbr)).ToArray<TSplit>();
  }

  protected virtual List<ILotSerial> SelectSerialStatus(
    TLine line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    return this.PerformSelectSerial<INLotSerialStatusByCostCenter>((PXSelectBase) this.GetSerialStatusCmd(line, item), new object[1]
    {
      (object) this.MakeINLotSerialStatusByCostCenter((ILSMaster) line)
    });
  }

  public virtual PXSelectBase<INLotSerialStatusByCostCenter> GetSerialStatusCmd(
    TLine line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    PXSelectBase<INLotSerialStatusByCostCenter> serialStatusCmdBase = this.GetSerialStatusCmdBase(line, item);
    this.AppendSerialStatusCmdWhere(serialStatusCmdBase, line, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item));
    this.AppendSerialStatusCmdOrderBy(serialStatusCmdBase, line, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item));
    return serialStatusCmdBase;
  }

  protected virtual PXSelectBase<INLotSerialStatusByCostCenter> GetSerialStatusCmdBase(
    TLine line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    return (PXSelectBase<INLotSerialStatusByCostCenter>) new FbqlSelect<SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<INLotSerialStatusByCostCenter.FK.Location>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<BqlField<INLotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INLotSerialStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>, INLotSerialStatusByCostCenter>.View((PXGraph) this.Base);
  }

  protected virtual void AppendSerialStatusCmdWhere(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    TLine line,
    INLotSerClass lotSerClass)
  {
    if (line.SubItemID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    if (line.LocationID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLocation.salesValid, IBqlBool>.IsEqual<True>>>();
    if (!lotSerClass.IsManualAssignRequired.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(line.LotSerialNbr))
      cmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<BqlField<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.FromCurrent>>>();
  }

  public virtual void AppendSerialStatusCmdOrderBy(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    TLine line,
    INLotSerClass lotSerClass)
  {
    switch (lotSerClass.LotSerIssueMethod)
    {
      case "F":
        cmd.OrderByNew<OrderBy<Asc<INLocation.pickPriority, Asc<INLotSerialStatusByCostCenter.receiptDate, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>();
        break;
      case "L":
        cmd.OrderByNew<OrderBy<Asc<INLocation.pickPriority, Desc<INLotSerialStatusByCostCenter.receiptDate, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>();
        break;
      case "E":
        cmd.OrderByNew<OrderBy<Asc<INLotSerialStatusByCostCenter.expireDate, Asc<INLocation.pickPriority, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>();
        break;
      case "S":
      case "U":
        cmd.OrderByNew<OrderBy<Asc<INLocation.pickPriority, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>();
        break;
      default:
        throw new PXException();
    }
  }

  protected virtual PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    int? inventoryID)
  {
    if (!inventoryID.HasValue)
      return (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
    if (inventoryItem == null)
      throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        (object) "Inventory Item",
        (object) inventoryID
      });
    INLotSerClass inLotSerClass;
    if (inventoryItem.StkItem.GetValueOrDefault())
    {
      inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, inventoryItem.LotSerClassID);
      if (inLotSerClass == null)
        throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
        {
          (object) "Lot/Serial Class",
          (object) inventoryItem.LotSerClassID
        });
    }
    else
      inLotSerClass = new INLotSerClass();
    return new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(inventoryItem, inLotSerClass);
  }

  protected virtual ILotSerNumVal ReadLotSerNumVal(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    return INLotSerialNbrAttribute.ReadLotSerNumVal((PXGraph) this.Base, item);
  }

  public PXCache<TPrimary> DocumentCache
  {
    get => this._docCache ?? (this._docCache = GraphHelper.Caches<TPrimary>((PXGraph) this.Base));
  }

  public TPrimary DocumentCurrent => this.DocumentCache.Rows.Current;

  public PXCache<TLine> LineCache
  {
    get => this._lineCache ?? (this._lineCache = GraphHelper.Caches<TLine>((PXGraph) this.Base));
  }

  public TLine LineCurrent => this.LineCache.Rows.Current;

  public virtual TLine Clone(TLine line) => PXCache<TLine>.CreateCopy(line);

  public virtual TLine CreateVirtualStockComponentLine(TLine line, INKitSpecStkDet kititem)
  {
    TLine stockComponentLine = this.Clone(line);
    stockComponentLine.IsStockItem = new bool?(true);
    ((ILSMaster) stockComponentLine).InventoryID = kititem.CompInventoryID;
    stockComponentLine.SubItemID = kititem.CompSubItemID;
    stockComponentLine.UOM = kititem.UOM;
    // ISSUE: variable of a boxed type
    __Boxed<TLine> local = (object) stockComponentLine;
    Decimal? dfltCompQty = kititem.DfltCompQty;
    Decimal? baseQty = stockComponentLine.BaseQty;
    Decimal? nullable = dfltCompQty.HasValue & baseQty.HasValue ? new Decimal?(dfltCompQty.GetValueOrDefault() * baseQty.GetValueOrDefault()) : new Decimal?();
    local.Qty = nullable;
    stockComponentLine.UnassignedQty = new Decimal?(0M);
    return stockComponentLine;
  }

  public virtual TLine CreateVirtualNonStockComponentLine(TLine line, INKitSpecNonStkDet kititem)
  {
    TLine stockComponentLine = this.Clone(line);
    stockComponentLine.IsStockItem = new bool?(false);
    ((ILSMaster) stockComponentLine).InventoryID = kititem.CompInventoryID;
    stockComponentLine.SubItemID = new int?();
    stockComponentLine.UOM = kititem.UOM;
    // ISSUE: variable of a boxed type
    __Boxed<TLine> local = (object) stockComponentLine;
    Decimal? dfltCompQty = kititem.DfltCompQty;
    Decimal? baseQty = stockComponentLine.BaseQty;
    Decimal? nullable = dfltCompQty.HasValue & baseQty.HasValue ? new Decimal?(dfltCompQty.GetValueOrDefault() * baseQty.GetValueOrDefault()) : new Decimal?();
    local.Qty = nullable;
    stockComponentLine.UnassignedQty = new Decimal?(0M);
    return stockComponentLine;
  }

  public PXCache<TSplit> SplitCache
  {
    get => this._splitCache ?? (this._splitCache = GraphHelper.Caches<TSplit>((PXGraph) this.Base));
  }

  public TSplit SplitCurrent => this.SplitCache.Rows.Current;

  public virtual TSplit Clone(TSplit split) => PXCache<TSplit>.CreateCopy(split);

  public virtual bool IsTransferReceipt(ILSMaster line)
  {
    if (!(line.TranType == "TRX"))
      return false;
    short? invtMult = line.InvtMult;
    return (invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1;
  }

  public virtual TSplit EnsureSplit(ILSMaster row)
  {
    if (row is TSplit split1)
      return split1;
    TSplit split2 = this.LineToSplit(row as TLine);
    PXParentAttribute.SetParent((PXCache) this.SplitCache, (object) split2, typeof (TLine), (object) row);
    if (!string.IsNullOrEmpty(row.LotSerialNbr))
      this.DefaultLotSerialNbr(split2);
    return split2;
  }

  protected virtual void UpdateLotSerNumVal(
    ILotSerNumVal lotSerNum,
    string value,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    if (lotSerNum == null)
    {
      ILotSerNumVal lotSerNumVal;
      if (!PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item).LotSerNumShared.GetValueOrDefault())
        lotSerNumVal = (ILotSerNumVal) new InventoryItemLotSerNumVal()
        {
          InventoryID = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item).InventoryID
        };
      else
        lotSerNumVal = (ILotSerNumVal) new INLotSerClassLotSerNumVal()
        {
          LotSerClassID = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item).LotSerClassID
        };
      lotSerNum = lotSerNumVal;
      lotSerNum.LotSerNumVal = value;
      this.Base.Caches[lotSerNum.GetType()].Insert((object) lotSerNum);
    }
    else
    {
      PXCache cach = this.Base.Caches[lotSerNum.GetType()];
      ILotSerNumVal copy = (ILotSerNumVal) cach.CreateCopy((object) lotSerNum);
      copy.LotSerNumVal = value;
      cach.Update((object) copy);
    }
  }

  protected virtual INLotSerTrack.Mode GetTranTrackMode(ILSMaster row, INLotSerClass lotSerClass)
  {
    string str = row.TranType;
    short? invtMult;
    int? nullable;
    if (lotSerClass.LotSerAssign == "U")
    {
      invtMult = row.InvtMult;
      nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue && row.IsIntercompany.GetValueOrDefault())
        str = "TRX";
    }
    INLotSerClass lotSerClass1 = lotSerClass;
    string tranType = str;
    invtMult = row.InvtMult;
    int? invMult;
    if (!invtMult.HasValue)
    {
      nullable = new int?();
      invMult = nullable;
    }
    else
      invMult = new int?((int) invtMult.GetValueOrDefault());
    return INLotSerialNbrAttribute.TranTrackMode(lotSerClass1, tranType, invMult);
  }

  protected virtual void SetLineQtyFromBase(TLine line)
  {
    line.Qty = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) this.LineCache, ((ILSMaster) line).InventoryID, line.UOM, line.BaseQty.Value, INPrecision.QUANTITY));
  }

  protected virtual void SetSplitQtyWithLine(TSplit split, TLine line)
  {
    line = line ?? this.SelectLine(split);
    int? inventoryId1 = ((ILSMaster) split).InventoryID;
    int? inventoryId2 = (int?) ((ILSMaster) line)?.InventoryID;
    Decimal? baseQty1;
    if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
    {
      Decimal? baseQty2 = split.BaseQty;
      baseQty1 = (Decimal?) line?.BaseQty;
      if (baseQty2.GetValueOrDefault() == baseQty1.GetValueOrDefault() & baseQty2.HasValue == baseQty1.HasValue && string.Equals(split.UOM, line?.UOM, StringComparison.OrdinalIgnoreCase))
      {
        split.Qty = line.Qty;
        return;
      }
    }
    // ISSUE: variable of a boxed type
    __Boxed<TSplit> local = (object) split;
    PXCache<TSplit> splitCache = this.SplitCache;
    int? inventoryId3 = ((ILSMaster) split).InventoryID;
    string uom = split.UOM;
    baseQty1 = split.BaseQty;
    Decimal num = baseQty1.Value;
    Decimal? nullable = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId3, uom, num, INPrecision.QUANTITY));
    local.Qty = nullable;
  }

  public virtual void DefaultLotSerialNbr(TSplit split)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) split).InventoryID);
    if (pxResult == null)
      return;
    INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) split, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
    if (tranTrackMode != INLotSerTrack.Mode.None && (tranTrackMode & INLotSerTrack.Mode.Create) <= INLotSerTrack.Mode.None)
      return;
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    foreach (TSplit number in INLotSerialNbrAttribute.CreateNumbers<TSplit>((PXCache) this.SplitCache, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum, tranTrackMode, 1M))
    {
      if (string.IsNullOrEmpty(split.LotSerialNbr))
        split.LotSerialNbr = number.LotSerialNbr;
      split.AssignedNbr = number.AssignedNbr;
      split.LotSerClassID = number.LotSerClassID;
      if (split is ILSGeneratedDetail lsGeneratedDetail1 && number is ILSGeneratedDetail lsGeneratedDetail2)
        lsGeneratedDetail1.HasGeneratedLotSerialNbr = lsGeneratedDetail2.HasGeneratedLotSerialNbr;
    }
  }

  protected virtual DateTime? ExpireDateByLot(ILSMaster item, ILSMaster master)
  {
    return LSSelect.ExpireDateByLot((PXGraph) this.Base, item, master);
  }

  protected virtual void ExpireCachedItems(TSplit split)
  {
    this.ExpireCached<INLotSerialStatus>(this.MakeINLotSerialStatus((ILSMaster) split));
    this.ExpireCached<INLotSerialStatusByCostCenter>(this.MakeINLotSerialStatusByCostCenter((ILSMaster) split));
  }

  protected virtual void ExpireCached<T>(T item) where T : class, IBqlTable, new()
  {
    PXCache pxCache = (PXCache) GraphHelper.Caches<T>((PXGraph) this.Base);
    object obj = pxCache.Locate((object) item);
    if (obj == null || !EnumerableExtensions.IsIn<PXEntryStatus>(pxCache.GetStatus(obj), (PXEntryStatus) 5, (PXEntryStatus) 0))
      return;
    pxCache.SetStatus(obj, (PXEntryStatus) 0);
    pxCache.Remove(obj);
    pxCache.ClearQueryCache();
  }

  protected virtual void UpdateCounters(LSSelect.Counters counters, TSplit split)
  {
    ++counters.RecordCount;
    split.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase((PXCache) this.SplitCache, ((ILSMaster) split).InventoryID, split.UOM, split.Qty.Value, split.BaseQty, INPrecision.QUANTITY));
    counters.BaseQty += split.BaseQty.Value;
    if (!split.ExpireDate.HasValue)
    {
      ++counters.ExpireDatesNull;
    }
    else
    {
      if (counters.ExpireDates.ContainsKey(split.ExpireDate))
        ++counters.ExpireDates[split.ExpireDate];
      else
        counters.ExpireDates[split.ExpireDate] = 1;
      counters.ExpireDate = split.ExpireDate;
    }
    if (!split.SubItemID.HasValue)
    {
      ++counters.SubItemsNull;
    }
    else
    {
      if (counters.SubItems.ContainsKey(split.SubItemID))
        ++counters.SubItems[split.SubItemID];
      else
        counters.SubItems[split.SubItemID] = 1;
      counters.SubItem = split.SubItemID;
    }
    if (!split.LocationID.HasValue)
    {
      ++counters.LocationsNull;
    }
    else
    {
      if (counters.Locations.ContainsKey(split.LocationID))
        ++counters.Locations[split.LocationID];
      else
        counters.Locations[split.LocationID] = 1;
      counters.Location = split.LocationID;
    }
    if (!split.TaskID.HasValue)
    {
      ++counters.ProjectTasksNull;
    }
    else
    {
      KeyValuePair<int?, int?> key = new KeyValuePair<int?, int?>(split.ProjectID, split.TaskID);
      if (counters.ProjectTasks.ContainsKey(key))
        ++counters.ProjectTasks[key];
      else
        counters.ProjectTasks[key] = 1;
      LSSelect.Counters counters1 = counters;
      LSSelect.Counters counters2 = counters;
      int? projectId = split.ProjectID;
      int? taskId = split.TaskID;
      int? nullable = projectId;
      counters1.ProjectID = nullable;
      counters2.TaskID = taskId;
    }
    if (split.LotSerialNbr == null)
    {
      ++counters.LotSerNumbersNull;
    }
    else
    {
      if (!string.IsNullOrEmpty(split.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(split.AssignedNbr, split.LotSerialNbr))
        ++counters.UnassignedNumber;
      if (counters.LotSerNumbers.ContainsKey(split.LotSerialNbr))
        ++counters.LotSerNumbers[split.LotSerialNbr];
      else
        counters.LotSerNumbers[split.LotSerialNbr] = 1;
      counters.LotSerNumber = split.LotSerialNbr;
    }
  }

  protected virtual void ThrowEmptyLotSerNumVal(TSplit split)
  {
    string str = ((PXCache) this.SplitCache).GetAttributesReadonly((string) null).OfType<InventoryAttribute>().Select<InventoryAttribute, string>((Func<InventoryAttribute, string>) (a => a.FieldName)).FirstOrDefault<string>();
    throw new PXException("Cannot generate the next lot/serial number for item {0}.", new object[1]
    {
      ((PXCache) this.SplitCache).GetValueExt((object) split, str)
    });
  }

  public virtual Decimal? VerifySNQuantity(
    PXCache cache,
    ILSMaster row,
    Decimal? newValue,
    string qtyFieldName)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(row.InventoryID);
    if (pxResult != null && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S")
    {
      if (newValue.HasValue)
      {
        try
        {
          Decimal num1 = INUnitAttribute.ConvertToBase(cache, row.InventoryID, row.UOM, newValue.Value, INPrecision.NOROUND);
          if (Decimal.Remainder(num1, 1M) > 0M)
          {
            Decimal num2 = (Decimal) Math.Pow(10.0, (double) CommonSetupDecPl.Qty);
            Decimal num3 = Math.Floor(num1);
            while (true)
            {
              newValue = new Decimal?(INUnitAttribute.ConvertFromBase(cache, row.InventoryID, row.UOM, num3, INPrecision.NOROUND));
              if (!(Decimal.Remainder(newValue.Value * num2, 1M) == 0M))
                ++num3;
              else
                break;
            }
            cache.RaiseExceptionHandling(qtyFieldName, (object) row, (object) null, (Exception) new PXSetPropertyException("Invalid quantity specified for serial item. Line quantity was changed to match.", (PXErrorLevel) 2));
          }
        }
        catch (PXUnitConversionException ex)
        {
        }
      }
    }
    return newValue;
  }

  protected string Prefix
  {
    get
    {
      return this._prefix ?? (this._prefix = CustomizedTypeManager.GetTypeNotCustomized(((object) this).GetType()).Name);
    }
  }

  protected string TypePrefixed(string name) => $"{this.Prefix}_{name}";

  protected virtual bool AreSplitsEqual(TSplit a, TSplit b)
  {
    if ((object) a == null || (object) b == null)
      return (object) a != null;
    int? nullable1 = ((ILSMaster) a).InventoryID;
    int? nullable2 = ((ILSMaster) b).InventoryID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      return false;
    if (!a.IsStockItem.GetValueOrDefault())
      return true;
    nullable2 = a.SubItemID;
    nullable1 = b.SubItemID;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
    {
      nullable1 = a.LocationID;
      nullable2 = b.LocationID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && (string.Equals(a.LotSerialNbr, b.LotSerialNbr, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(a.LotSerialNbr) && string.IsNullOrEmpty(b.LotSerialNbr)) && (string.IsNullOrEmpty(a.AssignedNbr) || !INLotSerialNbrAttribute.StringsEqual(a.AssignedNbr, a.LotSerialNbr)))
        return string.IsNullOrEmpty(b.AssignedNbr) || !INLotSerialNbrAttribute.StringsEqual(b.AssignedNbr, b.LotSerialNbr);
    }
    return false;
  }

  protected virtual bool SameInventoryItem(ILSMaster a, ILSMaster b)
  {
    int? inventoryId1 = a.InventoryID;
    int? inventoryId2 = b.InventoryID;
    return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
  }

  public virtual bool IsTrackSerial(TSplit split)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(((ILSMaster) split).InventoryID);
    if (pxResult == null)
      return false;
    string str = split.TranType;
    short? invtMult;
    int? nullable;
    if (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerAssign == "U")
    {
      invtMult = split.InvtMult;
      nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue && split.IsIntercompany.GetValueOrDefault())
        str = "TRX";
    }
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = str;
    invtMult = split.InvtMult;
    int? invMult;
    if (!invtMult.HasValue)
    {
      nullable = new int?();
      invMult = nullable;
    }
    else
      invMult = new int?((int) invtMult.GetValueOrDefault());
    return INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult);
  }

  public virtual bool IsIndivisibleComponent(PX.Objects.IN.InventoryItem inventory)
  {
    return this.KitInProcessing != null && !inventory.DecimalBaseUnit.GetValueOrDefault();
  }

  private PX.Objects.IN.InventoryItem KitInProcessing { get; set; }

  protected virtual bool IsPrimaryFieldsUpdated(TLine line, TLine oldLine)
  {
    int? subItemId1 = line.SubItemID;
    int? subItemId2 = oldLine.SubItemID;
    if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
    {
      int? siteId1 = ((ILSMaster) line).SiteID;
      int? siteId2 = ((ILSMaster) oldLine).SiteID;
      if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
      {
        int? locationId1 = line.LocationID;
        int? locationId2 = oldLine.LocationID;
        if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue)
        {
          int? projectId1 = line.ProjectID;
          int? projectId2 = oldLine.ProjectID;
          if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue)
          {
            int? taskId1 = line.TaskID;
            int? taskId2 = oldLine.TaskID;
            return !(taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue);
          }
        }
      }
    }
    return true;
  }

  protected virtual NotDecimalUnitErrorRedirectorScope<TDetailQty> ResolveNotDecimalUnitErrorRedirectorScope<TDetailQty>(
    object row)
    where TDetailQty : IBqlField
  {
    if (this.LineQtyField == (Type) null)
      throw new PXArgumentException("LineQtyField");
    return new NotDecimalUnitErrorRedirectorScope<TDetailQty>((PXCache) this.LineCache, row, this.LineQtyField);
  }

  public void ThrowFieldIsEmpty<Field>(PXCache cache, object row) where Field : IBqlField
  {
    if (cache.RaiseExceptionHandling<Field>(row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{typeof (Field).Name}]"
    })))
      throw new PXRowPersistingException(typeof (Field).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (Field).Name
      });
  }

  public virtual bool SkipUnassignedException { get; private set; }

  protected virtual void RaiseUnassignedExceptionHandling(TLine line)
  {
    if (this.SkipUnassignedException || PXUIFieldAttribute.GetErrorOnly((PXCache) this.LineCache, (object) line, this.LineQtyField.Name) != null)
      return;
    ((PXCache) this.LineCache).RaiseExceptionHandling(this.LineQtyField.Name, (object) line, (object) null, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number", (PXErrorLevel) 2));
  }

  protected virtual bool GenerateLotSerialNumberOnPersist(TLine line)
  {
    LSSelect.Counters counters;
    return !this.LineCounters.TryGetValue(line, out counters) || counters.UnassignedNumber != 0;
  }

  protected virtual IDisposable InvtMultModeScope(TLine line)
  {
    return (IDisposable) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.InvtMultScope(line);
  }

  protected virtual IDisposable InvtMultModeScope(TLine line, TLine oldLine)
  {
    return (IDisposable) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.InvtMultScope(line, oldLine);
  }

  public IDisposable KitProcessingScope(PX.Objects.IN.InventoryItem kitItem)
  {
    return kitItem == null ? (IDisposable) null : (IDisposable) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.KitProcessScope(this, kitItem);
  }

  /// <summary>Create a scope for changing current operation mode</summary>
  protected IDisposable OperationModeScope(
    PXDBOperation alterCurrentOperation,
    bool restoreToNormal = false)
  {
    return (IDisposable) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.OperationScope(this, alterCurrentOperation, restoreToNormal);
  }

  /// <summary>
  /// Create a scope for suppressing of the major internal logic
  /// </summary>
  public IDisposable SuppressedModeScope(bool suppress)
  {
    return !suppress ? (IDisposable) null : (IDisposable) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.SuppressionScope(this);
  }

  protected IDisposable SuppressedModeScope(PXDBOperation? alterCurrentOperation = null)
  {
    return (IDisposable) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.SuppressionScope(this, alterCurrentOperation);
  }

  /// <summary>Create a scope for suppressing of the UI logic</summary>
  public IDisposable ForceUnattendedModeScope(bool suppress)
  {
    return !suppress || this.UnattendedMode ? (IDisposable) null : (IDisposable) new LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>.ForcedUnattendedModeScope(this);
  }

  public virtual IDisposable SkipUnassignedExceptionScope()
  {
    this.SkipUnassignedException = true;
    return Disposable.Create((Action) (() => this.SkipUnassignedException = false));
  }

  protected class LSView : 
    FbqlSelect<SelectFromBase<TLine, TypeArrayOf<IFbqlJoin>.Empty>, TLine>.View,
    IEqualityComparer<TLine>
  {
    protected bool _AllowInsert = true;
    protected bool _AllowUpdate = true;
    protected bool _AllowDelete = true;

    public LSView(
      LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> lsParent)
      : base((PXGraph) lsParent.Base)
    {
      this.LSParent = lsParent;
    }

    public LSView(PXGraph graph)
      : base(graph)
    {
    }

    public LSView(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> LSParent { get; }

    public virtual bool AllowInsert
    {
      get => this._AllowInsert;
      set
      {
        this._AllowInsert = value;
        ((PXCache) this.LSParent.LineCache).AllowInsert = value;
        this.LSParent.SetEditMode();
      }
    }

    public virtual bool AllowUpdate
    {
      get => this._AllowUpdate;
      set
      {
        this._AllowUpdate = value;
        ((PXCache) this.LSParent.LineCache).AllowUpdate = value;
        ((PXCache) this.LSParent.SplitCache).AllowInsert = value;
        ((PXCache) this.LSParent.SplitCache).AllowUpdate = value;
        ((PXCache) this.LSParent.SplitCache).AllowDelete = value;
        this.LSParent.SetEditMode();
      }
    }

    public virtual bool AllowDelete
    {
      get => this._AllowDelete;
      set
      {
        this._AllowDelete = value;
        ((PXCache) this.LSParent.LineCache).AllowDelete = value;
        this.LSParent.SetEditMode();
      }
    }

    public virtual TLine Insert(TLine line)
    {
      using (this.LSParent.OperationModeScope((PXDBOperation) 3, true))
      {
        using (this.LSParent.SuppressedModeScope())
          return this.LSParent.LineCache.Insert(line);
      }
    }

    public bool Equals(TLine x, TLine y)
    {
      if (!PXCacheEx.GetComparer(((PXSelectBase) this).Cache).Equals((object) x, (object) y))
        return false;
      int? inventoryId1 = ((ILSMaster) x).InventoryID;
      int? inventoryId2 = ((ILSMaster) y).InventoryID;
      return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
    }

    public int GetHashCode(TLine obj)
    {
      return PXCacheEx.GetComparer(((PXSelectBase) this).Cache).GetHashCode((object) obj) * 37 + ((ILSMaster) obj).InventoryID.GetHashCode();
    }
  }

  public class InvtMultScope : IDisposable
  {
    protected readonly TLine _line;
    protected readonly TLine _oldLine;
    protected readonly bool? _reverse;
    protected readonly bool? _reverseOld;

    public InvtMultScope(TLine line)
    {
      this._reverse = new bool?(this.IsReverse(line));
      this._line = line;
      if (!this._reverse.GetValueOrDefault())
        return;
      ref readonly TLine local1 = ref this._line;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local2 = (object) local1;
      short? invtMult = local1.InvtMult;
      short? nullable1 = invtMult.HasValue ? new short?((short) ((int) invtMult.GetValueOrDefault() * -1)) : new short?();
      local2.InvtMult = nullable1;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> line1 = (object) this._line;
      Decimal? nullable2 = this._line.Qty;
      Decimal? nullable3 = new Decimal?(-1M * nullable2.Value);
      line1.Qty = nullable3;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> line2 = (object) this._line;
      nullable2 = this._line.BaseQty;
      Decimal? nullable4 = new Decimal?(-1M * nullable2.Value);
      line2.BaseQty = nullable4;
    }

    public InvtMultScope(TLine line, TLine oldLine)
      : this(line)
    {
      this._reverseOld = new bool?(this.IsReverse(oldLine));
      this._oldLine = oldLine;
      if (!this._reverseOld.GetValueOrDefault())
        return;
      ref readonly TLine local1 = ref this._oldLine;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local2 = (object) local1;
      short? invtMult = local1.InvtMult;
      short? nullable = invtMult.HasValue ? new short?((short) ((int) invtMult.GetValueOrDefault() * -1)) : new short?();
      local2.InvtMult = nullable;
      this._oldLine.Qty = new Decimal?(-1M * this._oldLine.Qty.Value);
      this._oldLine.BaseQty = new Decimal?(-1M * this._oldLine.BaseQty.Value);
    }

    protected virtual bool IsReverse(TLine line)
    {
      Decimal? qty = line.Qty;
      Decimal num = 0M;
      return qty.GetValueOrDefault() < num & qty.HasValue;
    }

    public virtual void Dispose()
    {
      TLine line1;
      short? invtMult;
      Decimal? nullable1;
      if (this._reverse.GetValueOrDefault())
      {
        line1 = this._line;
        ref TLine local1 = ref line1;
        // ISSUE: variable of a boxed type
        __Boxed<TLine> local2 = (object) local1;
        invtMult = local1.InvtMult;
        short? nullable2 = invtMult.HasValue ? new short?((short) ((int) invtMult.GetValueOrDefault() * -1)) : new short?();
        local2.InvtMult = nullable2;
        // ISSUE: variable of a boxed type
        __Boxed<TLine> line2 = (object) this._line;
        nullable1 = this._line.Qty;
        Decimal? nullable3 = new Decimal?(-1M * nullable1.Value);
        line2.Qty = nullable3;
        // ISSUE: variable of a boxed type
        __Boxed<TLine> line3 = (object) this._line;
        nullable1 = this._line.BaseQty;
        Decimal? nullable4 = new Decimal?(-1M * nullable1.Value);
        line3.BaseQty = nullable4;
      }
      if (!this._reverseOld.GetValueOrDefault())
        return;
      line1 = this._oldLine;
      ref TLine local3 = ref line1;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> local4 = (object) local3;
      invtMult = local3.InvtMult;
      short? nullable5 = invtMult.HasValue ? new short?((short) ((int) invtMult.GetValueOrDefault() * -1)) : new short?();
      local4.InvtMult = nullable5;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> oldLine1 = (object) this._oldLine;
      nullable1 = this._oldLine.Qty;
      Decimal? nullable6 = new Decimal?(-1M * nullable1.Value);
      oldLine1.Qty = nullable6;
      // ISSUE: variable of a boxed type
      __Boxed<TLine> oldLine2 = (object) this._oldLine;
      nullable1 = this._oldLine.BaseQty;
      Decimal? nullable7 = new Decimal?(-1M * nullable1.Value);
      oldLine2.BaseQty = nullable7;
    }
  }

  private class KitProcessScope : IDisposable
  {
    private readonly LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> _lsParent;

    public KitProcessScope(
      LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> lsParent,
      PX.Objects.IN.InventoryItem kitItem)
    {
      this._lsParent = lsParent;
      this._lsParent.KitInProcessing = kitItem;
    }

    void IDisposable.Dispose() => this._lsParent.KitInProcessing = (PX.Objects.IN.InventoryItem) null;
  }

  private class OperationScope : IDisposable
  {
    private readonly LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> _lsParent;
    private readonly PXDBOperation _initOperation;
    private readonly bool _restoreToNormal;

    public OperationScope(
      LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> lsParent,
      PXDBOperation alterCurrentOperation,
      bool restoreToNormal)
    {
      this._lsParent = lsParent;
      this._restoreToNormal = restoreToNormal;
      this._initOperation = this._lsParent.CurrentOperation;
      this._lsParent.CurrentOperation = alterCurrentOperation;
    }

    void IDisposable.Dispose()
    {
      this._lsParent.CurrentOperation = this._restoreToNormal ? (PXDBOperation) (object) 0 : this._initOperation;
    }
  }

  private class SuppressionScope : IDisposable
  {
    private readonly LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> _lsParent;
    private readonly bool _initSuppressedMode;

    public SuppressionScope(
      LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> lsParent,
      PXDBOperation? alterCurrentOperation = null)
    {
      this._lsParent = lsParent;
      this._initSuppressedMode = this._lsParent.SuppressedMode;
      this._lsParent.SuppressedMode = true;
    }

    void IDisposable.Dispose() => this._lsParent.SuppressedMode = this._initSuppressedMode;
  }

  private class ForcedUnattendedModeScope : IDisposable
  {
    private readonly LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> _lsParent;

    public ForcedUnattendedModeScope(
      LineSplittingExtension<TGraph, TPrimary, TLine, TSplit> lsParent)
    {
      this._lsParent = lsParent;
      this._lsParent.UnattendedMode = true;
    }

    void IDisposable.Dispose() => this._lsParent.UnattendedMode = false;
  }
}
