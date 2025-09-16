// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.WMS.ReceivePutAway
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.Extensions;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.WMS;
using PX.Objects.PO.GraphExtensions.POReceiptEntryExt;
using PX.Objects.PO.Scopes;
using PX.Objects.SO;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Objects.PO.WMS;

public class ReceivePutAway : WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>
{
  public 
  #nullable disable
  PXSetupOptional<POReceivePutAwaySetup, Where<BqlOperand<
  #nullable enable
  POReceivePutAwaySetup.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>>> Setup;
  public 
  #nullable disable
  PXAction<ScanHeader> ViewOrder;
  private bool redirectQtyErrorToReceivedQty;

  public RPAScanHeader RPAHeader
  {
    get => ScanHeaderExt.Get<RPAScanHeader>(this.Header) ?? new RPAScanHeader();
  }

  public ValueSetter<ScanHeader>.Ext<RPAScanHeader> RPASetter
  {
    get => this.HeaderSetter.With<RPAScanHeader>();
  }

  public int? DefaultLocationID
  {
    get => this.RPAHeader.DefaultLocationID;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<int?>((Expression<Func<RPAScanHeader, int?>>) (h => h.DefaultLocationID), value);
    }
  }

  public int? PutAwayToLocationID
  {
    get => this.RPAHeader.PutAwayToLocationID;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<int?>((Expression<Func<RPAScanHeader, int?>>) (h => h.PutAwayToLocationID), value);
    }
  }

  public bool? Released => this.RPAHeader.Released;

  public bool? ForceInsertLine
  {
    get => this.RPAHeader.ForceInsertLine;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<bool?>((Expression<Func<RPAScanHeader, bool?>>) (h => h.ForceInsertLine), value);
    }
  }

  public bool? AddTransferMode
  {
    get => this.RPAHeader.AddTransferMode;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<bool?>((Expression<Func<RPAScanHeader, bool?>>) (h => h.AddTransferMode), value);
    }
  }

  public Decimal? BaseExcessQty
  {
    get => this.RPAHeader.BaseExcessQty;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<Decimal?>((Expression<Func<RPAScanHeader, Decimal?>>) (h => h.BaseExcessQty), value);
    }
  }

  public int? PrevInventoryID
  {
    get => this.RPAHeader.PrevInventoryID;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<int?>((Expression<Func<RPAScanHeader, int?>>) (h => h.PrevInventoryID), value);
    }
  }

  public string ReceiptType
  {
    get => this.RPAHeader.ReceiptType;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<string>((Expression<Func<RPAScanHeader, string>>) (h => h.ReceiptType), value);
    }
  }

  public string TransferRefNbr
  {
    get => this.RPAHeader.TransferRefNbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<string>((Expression<Func<RPAScanHeader, string>>) (h => h.TransferRefNbr), value);
    }
  }

  public string PONbr
  {
    get => this.RPAHeader.PONbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<RPAScanHeader> rpaSetter = this.RPASetter;
      (^ref rpaSetter).Set<string>((Expression<Func<RPAScanHeader, string>>) (h => h.PONbr), value);
    }
  }

  public Decimal BaseQty
  {
    get
    {
      return INUnitAttribute.ConvertToBase(((PXSelectBase) this.Graph.transactions).Cache, this.InventoryID, this.UOM, this.Qty.GetValueOrDefault(), INPrecision.NOROUND);
    }
  }

  public virtual bool ExplicitConfirmation
  {
    get
    {
      return ((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ExplicitLineConfirmation.GetValueOrDefault();
    }
  }

  protected override string DocumentIsNotEditableMessage
  {
    get => "The document has become unavailable for editing. Contact your manager.";
  }

  public override bool DocumentIsEditable
  {
    get
    {
      if (EnumerableExtensions.IsIn<string>(this.Header.Mode, "RCPT", "TRRC"))
      {
        if (base.DocumentIsEditable)
        {
          PX.Objects.PO.POReceipt receipt1 = this.Receipt;
          int num;
          if (receipt1 == null)
          {
            num = 0;
          }
          else
          {
            bool? released = receipt1.Released;
            bool flag = false;
            num = released.GetValueOrDefault() == flag & released.HasValue ? 1 : 0;
          }
          if (num != 0)
          {
            PX.Objects.PO.POReceipt receipt2 = this.Receipt;
            if (receipt2 == null)
              return false;
            bool? received = receipt2.Received;
            bool flag = false;
            return received.GetValueOrDefault() == flag & received.HasValue;
          }
        }
        return false;
      }
      if (this.Header.Mode == "VRTN")
      {
        if (!base.DocumentIsEditable)
          return false;
        PX.Objects.PO.POReceipt receipt = this.Receipt;
        if (receipt == null)
          return false;
        bool? released = receipt.Released;
        bool flag = false;
        return released.GetValueOrDefault() == flag & released.HasValue;
      }
      if (!(this.Header.Mode == "PTAW"))
        throw new ArgumentOutOfRangeException("Mode");
      if (base.DocumentIsEditable)
      {
        PX.Objects.PO.POReceipt receipt3 = this.Receipt;
        bool? nullable;
        int num1;
        if (receipt3 == null)
        {
          num1 = 0;
        }
        else
        {
          nullable = receipt3.Released;
          num1 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num1 != 0)
        {
          PX.Objects.PO.POReceipt receipt4 = this.Receipt;
          int num2;
          if (receipt4 == null)
          {
            num2 = 0;
          }
          else
          {
            nullable = receipt4.Canceled;
            bool flag = false;
            num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
          }
          if (num2 != 0)
          {
            PX.Objects.PO.POReceipt receipt5 = this.Receipt;
            if (receipt5 == null)
              return false;
            nullable = receipt5.IsUnderCorrection;
            bool flag = false;
            return nullable.GetValueOrDefault() == flag & nullable.HasValue;
          }
        }
      }
      return false;
    }
  }

  protected override bool UseQtyCorrection
  {
    get
    {
      return !((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.UseDefaultQty.GetValueOrDefault();
    }
  }

  protected override bool CanOverrideQty
  {
    get => this.DocumentIsEditable && !this.LotSerialTrack.IsTrackedSerial;
  }

  public virtual bool ReceiveBySingleItem => this.LotSerialTrack.IsTrackedSerial;

  public virtual bool VerifyBeforeRelease
  {
    get
    {
      POReceivePutAwaySetup receivePutAwaySetup = POReceivePutAwaySetup.PK.Find((PXGraph) this.Graph, ((PXGraph) this.Graph).Accessinfo.BranchID);
      return receivePutAwaySetup != null && receivePutAwaySetup.VerifyReceiptsBeforeRelease.GetValueOrDefault();
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "View Order")]
  protected virtual IEnumerable viewOrder(PXAdapter adapter)
  {
    PX.Objects.PO.POReceiptLineSplit current = (PX.Objects.PO.POReceiptLineSplit) ((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceiptLineSplit>((PXGraph) this.Graph)).Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.PO.POReceiptLine poReceiptLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Graph, (object[]) new PX.Objects.PO.POReceiptLineSplit[1]
    {
      current
    }, Array.Empty<object>()));
    if (poReceiptLine == null)
      return adapter.Get();
    POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
    ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Search<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>((object) poReceiptLine.POType, (object) poReceiptLine.PONbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewOrder");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected override void _(PX.Data.Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    PXCache<PX.Objects.PO.POReceiptLineSplit> pxCache = GraphHelper.Caches<PX.Objects.PO.POReceiptLineSplit>((PXGraph) this.Graph);
    PX.Objects.PO.POReceipt poReceipt1 = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base, this.ReceiptType, this.RefNbr);
    bool? released;
    int num1;
    if (poReceipt1 == null)
    {
      num1 = 0;
    }
    else
    {
      released = poReceipt1.Released;
      bool flag = false;
      num1 = released.GetValueOrDefault() == flag & released.HasValue ? 1 : 0;
    }
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster;
    if (num1 == 0)
    {
      attributeAdjuster = PXCacheEx.AdjustUI((PXCache) pxCache, (object) null);
      attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
      ((PXCache) pxCache).AllowInsert = ((PXCache) pxCache).AllowUpdate = ((PXCache) pxCache).AllowDelete = false;
    }
    attributeAdjuster = PXCacheEx.AdjustUI((PXCache) pxCache, (object) null);
    attributeAdjuster.For<PX.Objects.PO.POReceiptLineSplit.qty>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      int num2;
      if (!(e.Row.Mode == "PTAW"))
      {
        PX.Objects.PO.POReceipt receipt = this.Receipt;
        num2 = receipt != null ? (!receipt.WMSSingleOrder.GetValueOrDefault() ? 1 : 0) : 1;
      }
      else
        num2 = 1;
      pxuiFieldAttribute.Visible = num2 != 0;
    }));
    if (string.IsNullOrEmpty(this.ReceiptType) || string.IsNullOrEmpty(this.RefNbr))
    {
      if (!string.IsNullOrEmpty(this.PONbr))
        return;
      ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.Document).Current = (PX.Objects.PO.POReceipt) null;
    }
    else
    {
      PXSelectJoin<PX.Objects.PO.POReceipt, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POReceipt.vendorID>>>, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Optional<PX.Objects.PO.POReceipt.receiptType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> document = ((PXGraphExtension<ReceivePutAway.Host>) this).Base.Document;
      released = this.Released;
      PX.Objects.PO.POReceipt poReceipt2;
      if (!released.GetValueOrDefault())
        poReceipt2 = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.Document).Search<PX.Objects.PO.POReceipt.receiptNbr>((object) this.RefNbr, new object[1]
        {
          (object) this.ReceiptType
        }));
      else
        poReceipt2 = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base, this.ReceiptType, this.RefNbr);
      ((PXSelectBase<PX.Objects.PO.POReceipt>) document).Current = poReceipt2;
    }
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.IN.INRegister.docType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.IN.INRegister.docType>, object, object>) e).NewValue = (object) "T";
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty> e)
  {
    foreach (PX.Objects.PO.POReceiptLineSplit receiptLineSplit in this.GetSplits(this.Receipt).Select<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceiptLineSplit>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceiptLineSplit>) (s => ((PXResult) s).GetItem<PX.Objects.PO.POReceiptLineSplit>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
    {
      int? lineNbr1 = r.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })))
    {
      if (this.redirectQtyErrorToReceivedQty)
        ((PXSelectBase) this.Graph.splits).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceiptLineSplit.receivedQty>((object) receiptLineSplit, (object) receiptLineSplit.ReceivedQty, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty>>) e).Exception);
      else
        ((PXSelectBase) this.Graph.splits).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceiptLineSplit.qty>((object) receiptLineSplit, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty>, PX.Objects.PO.POReceiptLine, object>) e).NewValue, ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty>>) e).Exception);
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POReceivePutAwayUserSetup> e)
  {
    e.Row.IsOverridden = new bool?(!e.Row.SameAs(((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current));
  }

  protected virtual void _(PX.Data.Events.RowInserted<POReceivePutAwayUserSetup> e)
  {
    e.Row.IsOverridden = new bool?(!e.Row.SameAs(((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POReceivePutAwayUserSetup> e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || !e.Row.DefaultLotSerialNumber.GetValueOrDefault())
      return;
    bool? defaultExpireDate = e.Row.DefaultExpireDate;
    bool flag = false;
    if (defaultExpireDate.GetValueOrDefault() == flag & defaultExpireDate.HasValue)
    {
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POReceivePutAwayUserSetup>>) e).Cache.Clear();
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POReceivePutAwayUserSetup>>) e).Cache.ClearQueryCache();
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POReceivePutAwayUserSetup>>) e).Cache.Current = (object) null;
      throw new PXException("The {0} check box cannot be cleared if the {1} check box is selected.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<POReceivePutAwayUserSetup.defaultExpireDate>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POReceivePutAwayUserSetup>>) e).Cache),
        (object) PXUIFieldAttribute.GetDisplayName<POReceivePutAwayUserSetup.defaultLotSerialNumber>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POReceivePutAwayUserSetup>>) e).Cache)
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<POReceivePutAwayUserSetup> e)
  {
    bool? nullable1;
    if (e.Row != null && e.Row.DefaultLotSerialNumber.GetValueOrDefault())
    {
      nullable1 = e.Row.DefaultExpireDate;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      {
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwayUserSetup>>) e).Cache.RaiseExceptionHandling<POReceivePutAwayUserSetup.defaultExpireDate>((object) e.Row, (object) e.Row.DefaultExpireDate, (Exception) new PXException("The {0} check box cannot be cleared if the {1} check box is selected.", new object[2]
        {
          (object) PXUIFieldAttribute.GetDisplayName<POReceivePutAwayUserSetup.defaultExpireDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwayUserSetup>>) e).Cache),
          (object) PXUIFieldAttribute.GetDisplayName<POReceivePutAwayUserSetup.defaultLotSerialNumber>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwayUserSetup>>) e).Cache)
        }));
        return;
      }
    }
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceivePutAwayUserSetup>>) e).Cache;
    POReceivePutAwayUserSetup row1 = e.Row;
    POReceivePutAwayUserSetup row2 = e.Row;
    bool? nullable2;
    if (row2 == null)
    {
      nullable1 = new bool?();
      nullable2 = nullable1;
    }
    else
      nullable2 = row2.DefaultExpireDate;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> local = (ValueType) nullable2;
    cache.RaiseExceptionHandling<POReceivePutAwayUserSetup.defaultExpireDate>((object) row1, (object) local, (Exception) null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt> e)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceiptLine>((PXGraph) this.Graph), (object) null).For<PX.Objects.PO.POReceiptLine.pOType>((Action<PXUIFieldAttribute>) (a => a.Visible = this.Header.Mode != "PTAW" || ((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base).IsMobile));
    chained = chained.SameFor<PX.Objects.PO.POReceiptLine.pONbr>();
    chained = chained.SameFor<PX.Objects.PO.POReceiptLine.sOOrderNbr>();
    chained.SameFor<PX.Objects.PO.POReceiptLine.sOShipmentNbr>();
  }

  [PXCustomizeBaseAttribute(typeof (BaseInventoryAttribute), "Visible", true)]
  [PXCustomizeBaseAttribute(typeof (BaseInventoryAttribute), "Enabled", false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLineSplit.inventoryID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SiteAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLineSplit.siteID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLineSplit.qty> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Received Qty.")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLineSplit.receivedQty> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLineSplit.putAwayQty> e)
  {
  }

  [BorrowedNote(typeof (PX.Objects.PO.POReceipt), typeof (POReceiptEntry))]
  protected virtual void _(PX.Data.Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  [PXMergeAttributes]
  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Receipt Nbr.", Enabled = false)]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.PO.POReceipt.vendorID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptType, Equal<BqlField<RPAScanHeader.receiptType, IBqlString>.FromCurrent>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AP.Vendor, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>, PX.Objects.PO.POReceipt>.SearchFor<PX.Objects.PO.POReceipt.receiptNbr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<WMSScanHeader.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlOperand<InventoryMultiplicator.decrease, IBqlShort>.When<BqlOperand<ScanHeader.mode, IBqlString>.IsEqual<ReceivePutAway.ReturnMode.value>>.Else<InventoryMultiplicator.increase>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<WMSScanHeader.inventoryMultiplicator> e)
  {
  }

  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.Persist" />
  [PXOverride]
  public virtual void Persist(Action base_Persist)
  {
    base_Persist();
    this.ReceiptType = this.Receipt?.ReceiptType;
    this.RefNbr = this.Receipt?.ReceiptNbr;
  }

  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.Copy(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POLine,System.Decimal,System.Decimal)" />
  [PXOverride]
  public virtual void Copy(
    PX.Objects.PO.POReceiptLine aDest,
    PX.Objects.PO.POLine aSrc,
    Decimal aQtyAdj,
    Decimal aBaseQtyAdj,
    Action<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine, Decimal, Decimal> base_Copy)
  {
    base_Copy(aDest, aSrc, aQtyAdj, aBaseQtyAdj);
    if (!this.SiteID.HasValue)
      return;
    int? siteId = aSrc.SiteID;
    int? nullable1 = this.SiteID;
    if (siteId.GetValueOrDefault() == nullable1.GetValueOrDefault() & siteId.HasValue == nullable1.HasValue)
      return;
    aDest.SiteID = this.SiteID;
    PX.Objects.PO.POReceiptLine poReceiptLine1 = aDest;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    poReceiptLine1.LocationID = nullable2;
    PX.Objects.PO.POReceiptLine poReceiptLine2 = aDest;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    poReceiptLine2.ProjectID = nullable3;
    PX.Objects.PO.POReceiptLine poReceiptLine3 = aDest;
    nullable1 = new int?();
    int? nullable4 = nullable1;
    poReceiptLine3.TaskID = nullable4;
  }

  [PXOverride]
  public virtual (bool shouldVerify, PXErrorLevel errorLevel) ShouldVerifyZeroQty(
    PX.Objects.PO.POReceipt receipt,
    Func<PX.Objects.PO.POReceipt, (bool shouldVerify, PXErrorLevel errorLevel)> base_ShouldVerifyZeroQty)
  {
    (bool, PXErrorLevel) valueTuple = base_ShouldVerifyZeroQty(receipt);
    if (this.CurrentMode is ReceivePutAway.ReceiveMode && valueTuple.Item1)
    {
      POReceivePutAwaySetup receivePutAwaySetup = POReceivePutAwaySetup.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this), ((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base).Accessinfo.BranchID);
      if ((receivePutAwaySetup != null ? (receivePutAwaySetup.KeepZeroLinesOnReceiptConfirmation.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return (true, (PXErrorLevel) 2);
    }
    return valueTuple;
  }

  public PX.Objects.PO.POReceipt Receipt
  {
    get
    {
      return (PX.Objects.PO.POReceipt) PrimaryKeyOf<PX.Objects.PO.POReceipt>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>>.Find((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base, (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>) ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.CurrentDocument).Current, (PKFindOptions) 0);
    }
  }

  protected virtual ScanMode<ReceivePutAway> GetDefaultMode()
  {
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXViewOf<UserPreferences>.BasedOn<SelectFromBase<UserPreferences, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>.Config>.Select((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base, Array.Empty<object>()));
    DefaultReceivePutAwayModeByUser extension = userPreferences != null ? PXCacheEx.GetExtension<DefaultReceivePutAwayModeByUser>((IBqlTable) userPreferences) : (DefaultReceivePutAwayModeByUser) null;
    ReceivePutAway.ReceiveMode defaultMode1 = this.ScanModes.OfType<ReceivePutAway.ReceiveMode>().FirstOrDefault<ReceivePutAway.ReceiveMode>();
    ReceivePutAway.ReceiveTransferMode defaultMode2 = this.ScanModes.OfType<ReceivePutAway.ReceiveTransferMode>().FirstOrDefault<ReceivePutAway.ReceiveTransferMode>();
    ReceivePutAway.ReturnMode defaultMode3 = this.ScanModes.OfType<ReceivePutAway.ReturnMode>().FirstOrDefault<ReceivePutAway.ReturnMode>();
    ReceivePutAway.PutAwayMode defaultMode4 = this.ScanModes.OfType<ReceivePutAway.PutAwayMode>().FirstOrDefault<ReceivePutAway.PutAwayMode>();
    if (extension?.RPAMode == "RCPT" && ((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowReceivingTab.GetValueOrDefault())
      return (ScanMode<ReceivePutAway>) defaultMode1;
    if (extension?.RPAMode == "TRRC" && ((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowReceiveTransferTab.GetValueOrDefault())
      return (ScanMode<ReceivePutAway>) defaultMode2;
    if (extension?.RPAMode == "VRTN" && ((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowReturningTab.GetValueOrDefault())
      return (ScanMode<ReceivePutAway>) defaultMode3;
    if (extension?.RPAMode == "PTAW" && ((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowPutAwayTab.GetValueOrDefault())
      return (ScanMode<ReceivePutAway>) defaultMode4;
    if (((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowReceivingTab.GetValueOrDefault())
      return (ScanMode<ReceivePutAway>) defaultMode1;
    if (((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowReceiveTransferTab.GetValueOrDefault())
      return (ScanMode<ReceivePutAway>) defaultMode2;
    if (((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowReturningTab.GetValueOrDefault())
      return (ScanMode<ReceivePutAway>) defaultMode3;
    return !((PXSelectBase<POReceivePutAwaySetup>) this.Setup).Current.ShowPutAwayTab.GetValueOrDefault() ? base.GetDefaultMode() : (ScanMode<ReceivePutAway>) defaultMode4;
  }

  protected virtual IEnumerable<ScanMode<ReceivePutAway>> CreateScanModes()
  {
    yield return (ScanMode<ReceivePutAway>) new ReceivePutAway.ReceiveMode();
    yield return (ScanMode<ReceivePutAway>) new ReceivePutAway.ReceiveTransferMode();
    yield return (ScanMode<ReceivePutAway>) new ReceivePutAway.ReturnMode();
    yield return (ScanMode<ReceivePutAway>) new ReceivePutAway.PutAwayMode();
  }

  public PXDelegateResult SortedResult(IEnumerable result)
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    ((List<object>) pxDelegateResult).AddRange(result.Cast<object>());
    pxDelegateResult.IsResultSorted = true;
    return pxDelegateResult;
  }

  public virtual IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>> GetSplits(
    PX.Objects.PO.POReceipt receipt,
    bool includeUnassigned = false,
    bool sortByBeingProcessed = false,
    Func<PX.Objects.PO.POReceiptLineSplit, bool> processedSeparator = null)
  {
    if (receipt == null)
      return (IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>) Array.Empty<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>();
    IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>> first = ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit>>) PXSelectBase<PX.Objects.PO.POReceiptLineSplit, PXViewOf<PX.Objects.PO.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLineSplit.receiptType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Graph, new object[2]
    {
      (object) receipt.ReceiptType,
      (object) receipt.ReceiptNbr
    })).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit>>().Cast<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>();
    IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>> source1;
    if (includeUnassigned)
    {
      IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>> second = ((IEnumerable<PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit>>) PXSelectBase<PX.Objects.PO.Unassigned.POReceiptLineSplit, PXViewOf<PX.Objects.PO.Unassigned.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.Unassigned.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.Unassigned.POReceiptLineSplit.FK.ReceiptLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.Unassigned.POReceiptLineSplit.receiptType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<PX.Objects.PO.Unassigned.POReceiptLineSplit.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Graph, new object[2]
      {
        (object) receipt.ReceiptType,
        (object) receipt.ReceiptNbr
      })).AsEnumerable<PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit>>().Cast<PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>().Select<PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>((Func<PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>) (r => new PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>(MakeAssigned(PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>.op_Implicit(r)), PXResult<PX.Objects.PO.Unassigned.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>.op_Implicit(r))));
      source1 = first.Concat<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>(second);
    }
    else
      source1 = first;
    List<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>> splits = new List<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>();
    if (sortByBeingProcessed)
    {
      if (processedSeparator == null)
        processedSeparator = (Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
        {
          Decimal? receivedQty = r.ReceivedQty;
          Decimal? qty = r.Qty;
          return receivedQty.GetValueOrDefault() == qty.GetValueOrDefault() & receivedQty.HasValue == qty.HasValue;
        });
      (IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>> source2, IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>> source3) = EnumerableExtensions.DisuniteBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>(source1, (Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, bool>) (r => processedSeparator(PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>.op_Implicit(r))));
      splits.AddRange((IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>) source3.OrderBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, bool>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, bool>) (r =>
      {
        bool? isUnassigned = ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLineSplit>().IsUnassigned;
        bool flag = false;
        return isUnassigned.GetValueOrDefault() == flag & isUnassigned.HasValue;
      })).ThenBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLineSplit>().InventoryID)).ThenBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, string>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, string>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLineSplit>().LotSerialNbr)));
      splits.AddRange((IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>) source2.OrderByDescending<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLineSplit>().InventoryID)).ThenByDescending<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, string>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, string>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLineSplit>().LotSerialNbr)));
    }
    else
      splits.AddRange((IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>) source1.OrderBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, string>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, string>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLine>().ReceiptNbr)).ThenBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, bool>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, bool>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLine>().PONbr == null)).ThenBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLine>().LineNbr)).ThenBy<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POReceiptLineSplit>().SplitLineNbr)));
    return (IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>) splits;

    static PX.Objects.PO.POReceiptLineSplit MakeAssigned(PX.Objects.PO.Unassigned.POReceiptLineSplit unassignedSplit)
    {
      return PropertyTransfer.Transfer<PX.Objects.PO.Unassigned.POReceiptLineSplit, PX.Objects.PO.POReceiptLineSplit>(unassignedSplit, new PX.Objects.PO.POReceiptLineSplit());
    }
  }

  protected virtual bool ApplyLinesQtyChanges(bool completePOLines)
  {
    return this.ApplyLinesQtyChanges(completePOLines, true);
  }

  protected virtual bool ApplyLinesQtyChanges(bool completePOLines, bool removeZeroLines)
  {
    PXSelectBase<PX.Objects.PO.POReceiptLine> transactions = (PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions;
    PXSelectBase<PX.Objects.PO.POReceiptLineSplit> splits = (PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Graph.splits;
    bool flag = false;
    foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult1 in transactions.Select(Array.Empty<object>()))
    {
      PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult1);
      transactions.Current = poReceiptLine;
      Decimal num1 = 0M;
      Decimal? nullable;
      foreach (PXResult<PX.Objects.PO.POReceiptLineSplit> pxResult2 in splits.Select(Array.Empty<object>()))
      {
        PX.Objects.PO.POReceiptLineSplit receiptLineSplit = PXResult<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(pxResult2);
        splits.Current = receiptLineSplit;
        int num2 = flag ? 1 : 0;
        Decimal? qty = splits.Current.Qty;
        nullable = splits.Current.ReceivedQty;
        int num3 = !(qty.GetValueOrDefault() == nullable.GetValueOrDefault() & qty.HasValue == nullable.HasValue) ? 1 : 0;
        flag = (num2 | num3) != 0;
        splits.Current.Qty = splits.Current.ReceivedQty;
        splits.UpdateCurrent();
        nullable = splits.Current.ReceivedQty;
        Decimal num4 = 0M;
        if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
        {
          if (removeZeroLines)
          {
            splits.DeleteCurrent();
            flag = true;
          }
        }
        else
        {
          Decimal num5 = num1;
          nullable = splits.Current.ReceivedQty;
          Decimal valueOrDefault = nullable.GetValueOrDefault();
          num1 = num5 + valueOrDefault;
        }
      }
      transactions.Current.ReceivedToDateQty = transactions.Current.Qty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) transactions).Cache, transactions.Current.InventoryID, transactions.Current.UOM, num1, INPrecision.NOROUND));
      transactions.UpdateCurrent();
      if (completePOLines)
      {
        nullable = transactions.Current.Qty;
        Decimal num6 = 0M;
        if (nullable.GetValueOrDefault() > num6 & nullable.HasValue)
          ((PXSelectBase) transactions).Cache.SetValueExt<PX.Objects.PO.POReceiptLine.allowComplete>((object) poReceiptLine, (object) true);
      }
      nullable = transactions.Current.Qty;
      Decimal num7 = 0M;
      if (nullable.GetValueOrDefault() == num7 & nullable.HasValue && removeZeroLines)
      {
        transactions.DeleteCurrent();
        flag = true;
      }
    }
    return flag;
  }

  protected virtual bool HasNonStockKit(PX.Objects.PO.POReceipt receipt)
  {
    return PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<PX.Objects.PO.POReceiptLine.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.kitItem, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Graph, new object[2]
    {
      (object) receipt.ReceiptType,
      (object) receipt.ReceiptNbr
    }).Count != 0;
  }

  protected virtual bool HasSingleSiteInLines(PX.Objects.PO.POReceipt receipt, out int? singleSiteID)
  {
    PXResultset<PX.Objects.PO.POReceiptLine> pxResultset = PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.siteID, IBqlInt>.IsNotNull>>.Aggregate<PX.Data.BQL.Fluent.To<GroupBy<PX.Objects.PO.POReceiptLine.siteID>>>>.Config>.Select((PXGraph) this.Graph, new object[2]
    {
      (object) receipt.ReceiptType,
      (object) receipt.ReceiptNbr
    });
    if (pxResultset.Count == 0)
    {
      singleSiteID = receipt.SiteID;
      return singleSiteID.HasValue;
    }
    if (pxResultset.Count == 1)
    {
      singleSiteID = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResultset).SiteID;
      return true;
    }
    singleSiteID = new int?();
    return false;
  }

  public virtual bool EnsureLocationPrimaryItem(int? inventoryID, int? locationID)
  {
    INLocation inLocation = INLocation.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this), locationID);
    if (inLocation != null)
    {
      int? nullable1;
      int? nullable2;
      if (inLocation.PrimaryItemValid == "I")
      {
        nullable1 = inventoryID;
        nullable2 = inLocation.PrimaryItemID;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          return false;
      }
      if (inLocation.PrimaryItemValid == "C")
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this), inventoryID);
        if (inventoryItem != null)
        {
          nullable2 = inventoryItem.ItemClassID;
          nullable1 = inLocation.PrimaryItemClassID;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            return false;
        }
      }
    }
    return true;
  }

  public static PX.Objects.PO.POReceipt ReleaseReceiptFromHold(
    PX.Objects.PO.POReceipt receipt,
    POReceiptEntry graph = null)
  {
    if (!receipt.Hold.GetValueOrDefault())
      return receipt;
    POReceiptEntry poReceiptEntry = graph ?? PXGraph.CreateInstance<POReceiptEntry>();
    ((PXSelectBase<PX.Objects.PO.POReceipt>) poReceiptEntry.Document).Current = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) poReceiptEntry.Document).Search<PX.Objects.PO.POReceipt.receiptNbr>((object) receipt.ReceiptNbr, new object[1]
    {
      (object) receipt.ReceiptType
    }));
    ((PXAction) poReceiptEntry.releaseFromHold).Press();
    return ((PXSelectBase<PX.Objects.PO.POReceipt>) poReceiptEntry.Document).Current;
  }

  private static async System.Threading.Tasks.Task ConfirmReceiveImpl(
    PX.Objects.PO.POReceipt receipt,
    bool printReceipt,
    CancellationToken cancellationToken)
  {
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      using (new AllowZeroReceiptQtyScope(receipt))
      {
        receipt = ReceivePutAway.ReleaseReceiptFromHold(receipt, instance);
        ((SelectedEntityEvent<PX.Objects.PO.POReceipt>) PXEntityEventBase<PX.Objects.PO.POReceipt>.Container<PX.Objects.PO.POReceipt.Events>.Select((Expression<Func<PX.Objects.PO.POReceipt.Events, PXEntityEvent<PX.Objects.PO.POReceipt.Events>>>) (e => e.ReceiveConfirmed))).FireOn((PXGraph) instance, receipt);
        PXSelectJoin<PX.Objects.PO.POReceipt, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POReceipt.vendorID>>>, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Optional<PX.Objects.PO.POReceipt.receiptType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> document = instance.Document;
        if (((PXSelectBase<PX.Objects.PO.POReceipt>) document).Current == null)
        {
          PX.Objects.PO.POReceipt poReceipt;
          ((PXSelectBase<PX.Objects.PO.POReceipt>) document).Current = poReceipt = receipt;
        }
        ((PXGraph) instance).Actions.PressSave();
      }
      transactionScope.Complete();
    }
    if (!printReceipt || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      return;
    Dictionary<string, string> reportParameters = new Dictionary<string, string>()
    {
      ["ReceiptType"] = receipt.ReceiptType,
      ["ReceiptNbr"] = receipt.ReceiptNbr
    };
    PX.Objects.AP.Vendor baccount = PX.Objects.AP.Vendor.PK.Find((PXGraph) instance, receipt.VendorID);
    await DeviceHubTools.PrintReportViaDeviceHub<PX.Objects.AP.Vendor>((PXGraph) instance, "PO646000", reportParameters, "Vendor", baccount, cancellationToken);
  }

  private static async System.Threading.Tasks.Task ReleaseReceiptImpl(
    PX.Objects.PO.POReceipt receipt,
    string printLabelsReportID,
    bool printReceipt,
    CancellationToken cancellationToken)
  {
    receipt = ReceivePutAway.ReleaseReceiptFromHold(receipt);
    POReleaseReceipt.ReleaseDoc(new List<PX.Objects.PO.POReceipt>()
    {
      receipt
    }, printLabelsReportID, false);
    if (!printReceipt || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      return;
    Lazy<INReceiptEntry> lazy = Lazy.By<INReceiptEntry>((Func<INReceiptEntry>) (() => PXGraph.CreateInstance<INReceiptEntry>()));
    await DeviceHubTools.PrintReportViaDeviceHub<PX.Objects.AP.Vendor>((PXGraph) lazy.Value, "PO646000", new Dictionary<string, string>()
    {
      ["ReceiptType"] = receipt.ReceiptType,
      ["ReceiptNbr"] = receipt.ReceiptNbr
    }, "Vendor", PX.Objects.AP.Vendor.PK.Find((PXGraph) lazy.Value, receipt.VendorID), cancellationToken);
  }

  public sealed class PutAwayMode : 
    BarcodeDrivenStateMachine<
    #nullable enable
    ReceivePutAway, ReceivePutAway.Host>.ScanMode
  {
    public const 
    #nullable disable
    string Value = "PTAW";

    public ReceivePutAway.PutAwayMode.Logic Body => this.Get<ReceivePutAway.PutAwayMode.Logic>();

    public virtual string Code => "PTAW";

    public virtual string Description => "Put Away";

    protected virtual IEnumerable<ScanState<ReceivePutAway>> CreateStates()
    {
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.PutAwayMode.ReceiptState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.PutAwayMode.SourceLocationState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.PutAwayMode.TargetLocationState();
      ReceivePutAway.PutAwayMode.InventoryItemState state = new ReceivePutAway.PutAwayMode.InventoryItemState();
      state.AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.VPN);
      yield return (ScanState<ReceivePutAway>) state;
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.PutAwayMode.LotSerialState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.PutAwayMode.ConfirmState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.PutAwayMode.CommandOrReceiptOnlyState();
    }

    protected virtual IEnumerable<ScanTransition<ReceivePutAway>> CreateTransitions()
    {
      return this.Body.PromptLocationForEveryLine ? ((ScanMode<ReceivePutAway>) this).StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<ReceivePutAway.PutAwayMode.ReceiptState>().NextTo<ReceivePutAway.PutAwayMode.SourceLocationState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.LotSerialState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.TargetLocationState>((Action<ReceivePutAway>) null))) : ((ScanMode<ReceivePutAway>) this).StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<ReceivePutAway.PutAwayMode.ReceiptState>().NextTo<ReceivePutAway.PutAwayMode.SourceLocationState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.TargetLocationState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.LotSerialState>((Action<ReceivePutAway>) null)));
    }

    protected virtual IEnumerable<ScanCommand<ReceivePutAway>> CreateCommands()
    {
      yield return (ScanCommand<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.RemoveCommand();
      yield return (ScanCommand<ReceivePutAway>) new BarcodeQtySupport<ReceivePutAway, ReceivePutAway.Host>.SetQtyCommand();
      yield return (ScanCommand<ReceivePutAway>) new ReceivePutAway.PutAwayMode.ReleaseTransferCommand();
    }

    protected virtual IEnumerable<ScanRedirect<ReceivePutAway>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<ReceivePutAway>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<ReceivePutAway>) this).ResetMode(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.PutAwayMode.ReceiptState>(fullReset && !((ScanMode<ReceivePutAway>) this).Basis.IsWithinReset);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.PutAwayMode.SourceLocationState>(fullReset || this.Body.PromptLocationForEveryLine && !this.Body.IsSingleLocation);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.PutAwayMode.TargetLocationState>(fullReset || this.Body.PromptLocationForEveryLine);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.PutAwayMode.InventoryItemState>(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.PutAwayMode.LotSerialState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ReceivePutAway.PutAwayMode.value>
    {
      public value()
        : base("PTAW")
      {
      }
    }

    public class Logic : BarcodeDrivenStateMachine<
    #nullable enable
    ReceivePutAway, ReceivePutAway.Host>.ScanExtension
    {
      public 
      #nullable disable
      FbqlSelect<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.PO.POReceiptLineSplit>.View PutAway;
      public FbqlSelect<SelectFromBase<POReceiptSplitToTransferSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTranSplit>.On<POReceiptSplitToTransferSplitLink.FK.TransferLineSplit>>, FbqlJoins.Inner<INTran>.On<POReceiptSplitToTransferSplitLink.FK.TransferLine>>>.Where<KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptType>, Field<POReceiptSplitToTransferSplitLink.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToTransferSplitLink.receiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>, PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>.SameAsCurrent>, POReceiptSplitToTransferSplitLink>.View TransferSplitLinks;
      public PXAction<ScanHeader> ReviewPutAway;
      public PXAction<ScanHeader> ViewTransferInfo;
      private Lazy<INTransferEntry> _transferEntryLazy;

      public Logic()
      {
        this._transferEntryLazy = Lazy.By<INTransferEntry>(new Func<INTransferEntry>(this.CreateTransferEntry));
      }

      public virtual bool CanPutAway
      {
        get
        {
          return ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.PutAway).SelectMain(Array.Empty<object>())).Any<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            Decimal? putAwayQty = s.PutAwayQty;
            Decimal? qty = s.Qty;
            return !(putAwayQty.GetValueOrDefault() == qty.GetValueOrDefault() & putAwayQty.HasValue == qty.HasValue);
          }));
        }
      }

      public virtual bool CanSwitchReceipt => true;

      public virtual IEnumerable putAway()
      {
        return (IEnumerable) this.Basis.SortedResult((IEnumerable) this.Basis.GetSplits(this.Basis.Receipt));
      }

      [PXButton(CommitChanges = true)]
      [PXUIField(DisplayName = "Review")]
      protected virtual IEnumerable reviewPutAway(PXAdapter adapter) => adapter.Get();

      [PXButton]
      [PXUIField(DisplayName = "Transfer Allocations")]
      protected virtual void viewTransferInfo()
      {
        ((PXSelectBase<POReceiptSplitToTransferSplitLink>) this.TransferSplitLinks).AskExt();
      }

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        if (e.Row == null)
          return;
        ((PXAction) this.ReviewPutAway).SetVisible(((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base).IsMobile && e.Row.Mode == "PTAW");
        if (string.IsNullOrEmpty(this.Basis.RefNbr) || !(e.Row.Mode == "PTAW"))
          return;
        this.Basis.TransferRefNbr = this.GetTransfer()?.RefNbr;
      }

      public virtual bool PromptLocationForEveryLine
      {
        get
        {
          return ((PXSelectBase<POReceivePutAwaySetup>) this.Basis.Setup).Current.RequestLocationForEachItemInPutAway.GetValueOrDefault();
        }
      }

      public virtual bool IsSingleLocation
      {
        get
        {
          return ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.PutAway).SelectMain(Array.Empty<object>())).GroupBy<PX.Objects.PO.POReceiptLineSplit, int?>((Func<PX.Objects.PO.POReceiptLineSplit, int?>) (s => s.LocationID)).Count<IGrouping<int?, PX.Objects.PO.POReceiptLineSplit>>() < 2;
        }
      }

      public virtual PX.Objects.IN.INRegister GetTransfer()
      {
        return PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>>>>, And<BqlOperand<PX.Objects.IN.INRegister.transferType, IBqlString>.IsEqual<INTransferType.oneStep>>>, And<BqlOperand<PX.Objects.IN.INRegister.released, IBqlBool>.IsEqual<False>>>>.And<KeysRelation<CompositeKey<Field<PX.Objects.IN.INRegister.pOReceiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.IN.INRegister.pOReceiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister>, PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister>.SameAsCurrent>>>.ReadOnly.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceipt[1]
        {
          this.Basis.Receipt
        }, Array.Empty<object>()));
      }

      public INTransferEntry TransferEntry => this._transferEntryLazy.Value;

      private INTransferEntry CreateTransferEntry()
      {
        INTransferEntry instance = PXGraph.CreateInstance<INTransferEntry>();
        ((FieldVerifyingEvents) ((PXGraph) instance).FieldVerifying).AddAbstractHandler<INTran.inventoryID>(new Action<AbstractEvents.IFieldVerifying<object, INTran.inventoryID, object>>(AbstractEvents.Cancel));
        ((FieldVerifyingEvents) ((PXGraph) instance).FieldVerifying).AddAbstractHandler<INTranSplit.inventoryID>(new Action<AbstractEvents.IFieldVerifying<object, INTranSplit.inventoryID, object>>(AbstractEvents.Cancel));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance).FieldDefaulting.AddHandler<INTran.pOReceiptType>(ReceivePutAway.PutAwayMode.Logic.\u003C\u003Ec.\u003C\u003E9__21_0 ?? (ReceivePutAway.PutAwayMode.Logic.\u003C\u003Ec.\u003C\u003E9__21_0 = new PXFieldDefaulting((object) ReceivePutAway.PutAwayMode.Logic.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateTransferEntry\u003Eb__21_0))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance).FieldDefaulting.AddHandler<INTran.pOReceiptNbr>(ReceivePutAway.PutAwayMode.Logic.\u003C\u003Ec.\u003C\u003E9__21_1 ?? (ReceivePutAway.PutAwayMode.Logic.\u003C\u003Ec.\u003C\u003E9__21_1 = new PXFieldDefaulting((object) ReceivePutAway.PutAwayMode.Logic.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateTransferEntry\u003Eb__21_1))));
        ((PXGraph) this.Basis.Graph).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)] = ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)];
        ((PXGraph) this.Basis.Graph).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)] = ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)];
        ((PXGraph) this.Basis.Graph).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)] = ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)];
        ((PXGraph) this.Basis.Graph).Caches[typeof (SiteLotSerial)] = ((PXGraph) instance).Caches[typeof (SiteLotSerial)];
        ((PXGraph) this.Basis.Graph).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)] = ((PXGraph) instance).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)];
        ((PXGraph) this.Basis.Graph).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
        ((PXGraph) this.Basis.Graph).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter));
        ((PXGraph) this.Basis.Graph).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter));
        ((PXGraph) this.Basis.Graph).Views.Caches.Remove(typeof (SiteLotSerial));
        ((PXGraph) this.Basis.Graph).Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial));
        this.OnTransferEntryCreated(instance);
        return instance;
      }

      protected virtual void OnTransferEntryCreated(INTransferEntry transferEntry)
      {
        if (this.Basis.TransferRefNbr == null)
          return;
        ((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).Current = this.GetTransfer();
      }

      /// Overrides <see cref="M:PX.BarcodeProcessing.BarcodeDrivenStateMachine`2.HandleScan(System.String)" />
      [PXOverride]
      public virtual bool HandleScan(string barcode, Func<string, bool> base_HandleScan)
      {
        using (this.Basis.Header?.Mode == "PTAW" ? this.GetReleaseModeScope() : (IDisposable) null)
          return base_HandleScan(barcode);
      }

      private IDisposable GetReleaseModeScope()
      {
        return ((PXGraph) this.Graph).FindImplementation<IItemPlanHandler<PX.Objects.PO.POReceiptLineSplit>>().ReleaseModeScope();
      }
    }

    public sealed class ReceiptState : ReceivePutAway.ReceiptState
    {
      protected override bool IsStateSkippable()
      {
        if (((ScanComponent<ReceivePutAway>) this).Basis.RefNbr == null)
          return false;
        return !((ScanComponent<ReceivePutAway>) this).Basis.Header.ProcessingSucceeded.GetValueOrDefault() || this.Get<ReceivePutAway.PutAwayMode.Logic>().CanPutAway;
      }

      protected override PX.Objects.PO.POReceipt GetByBarcode(string barcode)
      {
        return PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.PO.POReceipt.vendorID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsIn<POReceiptType.poreceipt, POReceiptType.transferreceipt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AP.Vendor, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.Order<PX.Data.BQL.Fluent.By<Desc<TestIf<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<POReceiptType.poreceipt>>>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
        {
          (object) barcode
        }));
      }

      protected virtual Validation Validate(PX.Objects.PO.POReceipt receipt)
      {
        bool? released = receipt.Released;
        bool flag = false;
        if (released.GetValueOrDefault() == flag & released.HasValue || receipt.IsUnderCorrection.GetValueOrDefault() || receipt.Canceled.GetValueOrDefault())
          return Validation.Fail("The {0} receipt cannot be processed because it has the {1} status.", new object[2]
          {
            (object) receipt.ReceiptNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.status>((IBqlTable) receipt)
          });
        if (EnumerableExtensions.IsNotIn<string>(receipt.ReceiptType, "RT", "RX"))
          return Validation.Fail("The {0} receipt cannot be processed because it has the {1} type.", new object[2]
          {
            (object) receipt.ReceiptNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.receiptType>((IBqlTable) receipt)
          });
        if (receipt.POType == "DP")
          return Validation.Fail("The {0} receipt cannot be processed because the related order has the {1} type.", new object[2]
          {
            (object) receipt.ReceiptNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.pOType>((IBqlTable) receipt)
          });
        ReceivePutAway.PutAwayMode.ReceiptState.Logic logic = this.Get<ReceivePutAway.PutAwayMode.ReceiptState.Logic>();
        int? singleSiteID;
        if (!((ScanComponent<ReceivePutAway>) this).Basis.HasSingleSiteInLines(receipt, out singleSiteID))
          return Validation.Fail("All items in the {0} receipt must be located in the same warehouse.", new object[1]
          {
            (object) receipt.ReceiptNbr
          });
        int? siteId = receipt.SiteID;
        logic.SiteID = siteId.HasValue ? receipt.SiteID : singleSiteID;
        Validation error;
        return ((ScanComponent<ReceivePutAway>) this).Basis.HasNonStockKit(receipt) ? Validation.Fail("The {0} receipt cannot be processed because it contains a non-stock kit item.", new object[1]
        {
          (object) receipt.ReceiptNbr
        }) : (!logic.CanBePutAway(receipt, out error) ? error : Validation.Ok);
      }

      protected override void Apply(PX.Objects.PO.POReceipt receipt)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = this.Get<ReceivePutAway.PutAwayMode.ReceiptState.Logic>().SiteID;
        base.Apply(receipt);
      }

      protected override void ClearState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = new int?();
        base.ClearState();
        ((ScanComponent<ReceivePutAway>) this).Basis.TransferRefNbr = (string) null;
        ((PXSelectBase<PX.Objects.IN.INRegister>) this.Get<ReceivePutAway.PutAwayMode.Logic>().TransferEntry.transfer).Current = (PX.Objects.IN.INRegister) null;
      }

      protected virtual void SetNextState()
      {
        bool? remove = ((ScanComponent<ReceivePutAway>) this).Basis.Remove;
        bool flag = false;
        if (remove.GetValueOrDefault() == flag & remove.HasValue && !this.Get<ReceivePutAway.PutAwayMode.Logic>().CanPutAway)
        {
          ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo("{0} {1}", new object[2]
          {
            (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<ReceivePutAway>) this).Basis.Info).Current.Message,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.Localize("{0} receipt put away.", new object[1]
            {
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.RefNbr
            })
          });
          ((ScanComponent<ReceivePutAway>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
        }
        else
          ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) this).SetNextState();
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public int? SiteID { get; set; }

        public virtual bool CanBePutAway(
        #nullable disable
        PX.Objects.PO.POReceipt receipt, out Validation error)
        {
          if (PXResultset<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLineSplit, PXViewOf<PX.Objects.PO.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLineSplit.receiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.putAwayQty, IBqlDecimal>.IsLess<PX.Objects.PO.POReceiptLineSplit.qty>>>>.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[2]
          {
            (object) receipt.ReceiptType,
            (object) receipt.ReceiptNbr
          })) == null)
          {
            if (PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>>>>, And<BqlOperand<PX.Objects.IN.INRegister.transferType, IBqlString>.IsEqual<INTransferType.oneStep>>>, And<BqlOperand<PX.Objects.IN.INRegister.released, IBqlBool>.IsEqual<False>>>>.And<KeysRelation<CompositeKey<Field<PX.Objects.IN.INRegister.pOReceiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.IN.INRegister.pOReceiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister>, PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister>.SameAsCurrent>>>.ReadOnly.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceipt[1]
            {
              receipt
            }, Array.Empty<object>())) == null)
            {
              error = Validation.Fail("The {0} receipt has already been put away in full.", new object[1]
              {
                (object) receipt.ReceiptNbr
              });
              return false;
            }
          }
          error = Validation.Ok;
          return true;
        }
      }

      [PXLocalizable]
      public new abstract class Msg : ReceivePutAway.ReceiptState.Msg
      {
        public const string AlreadyPutAwayInFull = "The {0} receipt has already been put away in full.";
      }
    }

    public sealed class SourceLocationState : 
      WarehouseManagementSystem<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.LocationState
    {
      protected override bool IsStateActive()
      {
        return base.IsStateActive() && !this.Get<ReceivePutAway.PutAwayMode.Logic>().IsSingleLocation;
      }

      protected virtual bool IsStateSkippable()
      {
        return ((ScanComponent<ReceivePutAway>) this).Basis.LocationID.HasValue;
      }

      protected override 
      #nullable disable
      string StatePrompt => "Scan the origin location.";

      protected override void ReportSuccess(INLocation location)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.Reporter.Info("{0} selected as origin location.", new object[1]
        {
          (object) location.LocationCD
        });
      }

      protected override Validation Validate(INLocation location)
      {
        if (!((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Get<ReceivePutAway.PutAwayMode.Logic>().PutAway).SelectMain(Array.Empty<object>())).All<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (t =>
        {
          int? locationId1 = t.LocationID;
          int? locationId2 = location.LocationID;
          return !(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue);
        })))
          return base.Validate(location);
        return Validation.Fail("{0} location not listed in receipt.", new object[1]
        {
          (object) location.LocationCD
        });
      }

      [PXLocalizable]
      public new abstract class Msg : 
        WarehouseManagementSystem<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.LocationState.Msg
      {
        public new const 
        #nullable disable
        string Prompt = "Scan the origin location.";
        public new const string Ready = "{0} selected as origin location.";
        public new const string NotSet = "Origin location not selected.";
        public const string MissingInReceipt = "{0} location not listed in receipt.";
      }
    }

    public sealed class TargetLocationState : 
      WarehouseManagementSystem<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.LocationState
    {
      public new const 
      #nullable disable
      string Value = "TLOC";

      public override string Code => "TLOC";

      protected override string StatePrompt => "Scan the destination location.";

      protected virtual bool IsStateSkippable()
      {
        return ((ScanComponent<ReceivePutAway>) this).Basis.PutAwayToLocationID.HasValue;
      }

      protected override void ReportSuccess(INLocation location)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.Reporter.Info("{0} selected as destination location.", new object[1]
        {
          (object) location.LocationCD
        });
      }

      protected override void Apply(INLocation location)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.PutAwayToLocationID = location.LocationID;
      }

      protected override void ClearState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.PutAwayToLocationID = new int?();
      }

      public new class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        ReceivePutAway.PutAwayMode.TargetLocationState.value>
      {
        public value()
          : base("TLOC")
        {
        }
      }

      [PXLocalizable]
      public new abstract class Msg : 
        WarehouseManagementSystem<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.LocationState.Msg
      {
        public const 
        #nullable disable
        string PromtWithDefaultLocation = "Scan the destination location: {0}.";
        public new const string Prompt = "Scan the destination location.";
        public new const string Ready = "{0} selected as destination location.";
        public new const string NotSet = "Destination location not selected.";
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        [PXOverride]
        public 
        #nullable disable
        ScanState<ReceivePutAway> DecorateScanState(
          ScanState<ReceivePutAway> original,
          Func<ScanState<ReceivePutAway>, ScanState<ReceivePutAway>> base_DecorateScanState)
        {
          ScanState<ReceivePutAway> scanState = base_DecorateScanState(original);
          if (!(scanState is ReceivePutAway.PutAwayMode.TargetLocationState targrtLocation))
            return scanState;
          this.InjectTargetLocationPromtWithDefaultLocation(targrtLocation);
          return scanState;
        }

        public virtual void InjectTargetLocationPromtWithDefaultLocation(
          ReceivePutAway.PutAwayMode.TargetLocationState targrtLocation)
        {
          ((MethodInterceptor<EntityState<ReceivePutAway, INLocation>, ReceivePutAway>.OfFunc<string>) ((EntityState<ReceivePutAway, INLocation>) targrtLocation).Intercept.StatePrompt).ByOverride((Func<ReceivePutAway, Func<string>, string>) ((basis, base_StatePrompt) =>
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) basis.Graph, basis.InventoryID);
            if (inventoryItem == null)
              return base_StatePrompt();
            if (inventoryItem.StkItem.GetValueOrDefault())
            {
              int? putawayLocationId = INItemSite.PK.Find((PXGraph) basis.Graph, basis.InventoryID, basis.SiteID).DfltPutawayLocationID;
              if (putawayLocationId.HasValue)
              {
                int valueOrDefault = putawayLocationId.GetValueOrDefault();
                return basis.Localize("Scan the destination location: {0}.", new object[1]
                {
                  (object) basis.SightOf<INLocation.locationCD>((IBqlTable) INLocation.PK.Find((PXGraph) basis.Graph, new int?(valueOrDefault)))
                });
              }
            }
            if (!inventoryItem.StkItem.GetValueOrDefault())
            {
              int? pickingLocationId = PX.Objects.IN.INSite.PK.Find((PXGraph) basis.Graph, basis.SiteID).NonStockPickingLocationID;
              if (pickingLocationId.HasValue)
              {
                int valueOrDefault = pickingLocationId.GetValueOrDefault();
                return basis.Localize("Scan the destination location: {0}.", new object[1]
                {
                  (object) basis.SightOf<INLocation.locationCD>((IBqlTable) INLocation.PK.Find((PXGraph) basis.Graph, new int?(valueOrDefault)))
                });
              }
            }
            return base_StatePrompt();
          }), new RelativeInject?());
        }
      }
    }

    public sealed class InventoryItemState : 
      WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState
    {
      protected virtual AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>> HandleAbsence(
        string barcode)
      {
        if (this.Get<ReceivePutAway.PutAwayMode.Logic>().IsSingleLocation)
        {
          if (((ScanComponent<ReceivePutAway>) this).Basis.TryProcessBy<ReceivePutAway.PutAwayMode.TargetLocationState>(barcode, (StateSubstitutionRule) 18))
            return AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Done);
        }
        else if (((ScanComponent<ReceivePutAway>) this).Basis.TryProcessBy<ReceivePutAway.PutAwayMode.SourceLocationState>(barcode, (StateSubstitutionRule) 18))
          return AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Done);
        return ((EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) this).HandleAbsence(barcode);
      }

      protected override Validation Validate(PXResult<INItemXRef, PX.Objects.IN.InventoryItem> entity)
      {
        Validation validation;
        if (((ScanComponent<ReceivePutAway>) this).Basis.HasFault<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>(entity, new Func<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>(((WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState) this).Validate), ref validation))
          return validation;
        if (!((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Get<ReceivePutAway.PutAwayMode.Logic>().PutAway).SelectMain(Array.Empty<object>())).All<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (t =>
        {
          int? inventoryId1 = t.InventoryID;
          int? inventoryId2 = ((PXResult) entity).GetItem<PX.Objects.IN.InventoryItem>().InventoryID;
          return !(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue);
        })))
          return Validation.Ok;
        return Validation.Fail("{0} item not listed in receipt.", new object[1]
        {
          (object) ((PXResult) entity).GetItem<PX.Objects.IN.InventoryItem>().InventoryCD
        });
      }

      [PXLocalizable]
      public new abstract class Msg : 
        WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState.Msg
      {
        public const string MissingInReceipt = "{0} item not listed in receipt.";
      }
    }

    public sealed class LotSerialState : 
      WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState
    {
      protected override Validation Validate(string lotSerial)
      {
        Validation validation;
        if (((ScanComponent<ReceivePutAway>) this).Basis.HasFault<string>(lotSerial, new Func<string, Validation>(((WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState) this).Validate), ref validation))
          return validation;
        if (!((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Get<ReceivePutAway.PutAwayMode.Logic>().PutAway).SelectMain(Array.Empty<object>())).All<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (t => !string.Equals(t.LotSerialNbr, lotSerial, StringComparison.OrdinalIgnoreCase))))
          return Validation.Ok;
        return Validation.Fail("{0} lot or serial number not listed in receipt.", new object[1]
        {
          (object) lotSerial
        });
      }

      [PXLocalizable]
      public new abstract class Msg : 
        WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState.Msg
      {
        public const string MissingInReceipt = "{0} lot or serial number not listed in receipt.";
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.ConfirmationState
    {
      public virtual 
      #nullable disable
      string Prompt
      {
        get
        {
          return ((ScanComponent<ReceivePutAway>) this).Basis.Localize("Confirm putting away {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.Qty,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.UOM
          });
        }
      }

      protected virtual bool ExecuteInTransaction => true;

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<ReceivePutAway.PutAwayMode.ConfirmState.Logic>().ProcessPutAway();
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public 
        #nullable disable
        ReceivePutAway.PutAwayMode.Logic Mode { get; private set; }

        public virtual void Initialize()
        {
          this.Mode = this.Basis.Get<ReceivePutAway.PutAwayMode.Logic>();
        }

        public virtual FlowStatus ProcessPutAway()
        {
          bool remove = this.Basis.Remove.GetValueOrDefault();
          IEnumerable<PX.Objects.PO.POReceiptLineSplit> source = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.PutAway).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
          {
            int? inventoryId1 = r.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = r.SubItemID;
              int? subItemId2 = this.Basis.SubItemID;
              if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              {
                int? locationId = r.LocationID;
                int? nullable = this.Basis.LocationID ?? r.LocationID;
                if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
                {
                  if (!remove)
                  {
                    Decimal? putAwayQty = r.PutAwayQty;
                    Decimal? qty = r.Qty;
                    return putAwayQty.GetValueOrDefault() < qty.GetValueOrDefault() & putAwayQty.HasValue & qty.HasValue;
                  }
                  Decimal? putAwayQty1 = r.PutAwayQty;
                  Decimal num = 0M;
                  return putAwayQty1.GetValueOrDefault() > num & putAwayQty1.HasValue;
                }
              }
            }
            return false;
          }));
          if (!source.Any<PX.Objects.PO.POReceiptLineSplit>())
          {
            FlowStatus flowStatus = FlowStatus.Fail("No items to put away.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          if (!this.Basis.EnsureLocationPrimaryItem(this.Basis.InventoryID, this.Basis.LocationID))
            return FlowStatus.Fail("Selected item is not allowed in this location.", Array.Empty<object>());
          Decimal num1 = Sign.op_Multiply(Sign.MinusIf(remove), this.Basis.BaseQty);
          Decimal? nullable1;
          if (!remove)
          {
            nullable1 = source.Sum<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, Decimal?>) (s =>
            {
              Decimal? qty = s.Qty;
              Decimal? putAwayQty = s.PutAwayQty;
              return !(qty.HasValue & putAwayQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - putAwayQty.GetValueOrDefault());
            }));
            Decimal num2 = num1;
            if (nullable1.GetValueOrDefault() < num2 & nullable1.HasValue)
              return FlowStatus.Fail("The put away quantity cannot be greater than the received quantity.", Array.Empty<object>());
          }
          Decimal? nullable2;
          if (remove)
          {
            nullable2 = source.Sum<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, Decimal?>) (s => s.PutAwayQty));
            Decimal num3 = num1;
            nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num3) : new Decimal?();
            Decimal num4 = 0M;
            if (nullable1.GetValueOrDefault() < num4 & nullable1.HasValue)
              return FlowStatus.Fail("The put away quantity cannot be negative.", Array.Empty<object>());
          }
          Decimal val2 = num1;
          foreach (PX.Objects.PO.POReceiptLineSplit receivedSplit in remove ? source.Reverse<PX.Objects.PO.POReceiptLineSplit>() : source)
          {
            Decimal num5;
            if (!remove)
            {
              nullable1 = receivedSplit.Qty;
              Decimal num6 = nullable1.Value;
              nullable1 = receivedSplit.PutAwayQty;
              Decimal num7 = nullable1.Value;
              num5 = Math.Min(num6 - num7, val2);
            }
            else
            {
              nullable1 = receivedSplit.PutAwayQty;
              num5 = -Math.Min(nullable1.Value, -val2);
            }
            Decimal qty = num5;
            FlowStatus flowStatus = this.SyncWithTransfer(receivedSplit, qty);
            bool? isError = ((FlowStatus) ref flowStatus).IsError;
            bool flag = false;
            if (!(isError.GetValueOrDefault() == flag & isError.HasValue))
              return flowStatus;
            PX.Objects.PO.POReceiptLineSplit receiptLineSplit = receivedSplit;
            nullable1 = receiptLineSplit.PutAwayQty;
            Decimal num8 = qty;
            Decimal? nullable3;
            if (!nullable1.HasValue)
            {
              nullable2 = new Decimal?();
              nullable3 = nullable2;
            }
            else
              nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num8);
            receiptLineSplit.PutAwayQty = nullable3;
            ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.PutAway).Update(receivedSplit);
            val2 -= qty;
            if (val2 == 0M)
              break;
          }
          this.Basis.ReportInfo(remove ? "{0} x {1} {2} removed from target location." : "{0} x {1} {2} added to target location.", new object[3]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }

        public virtual FlowStatus SyncWithTransfer(PX.Objects.PO.POReceiptLineSplit receivedSplit, Decimal qty)
        {
          bool flag1 = false;
          PXSelectBase<PX.Objects.IN.INRegister> transfer = (PXSelectBase<PX.Objects.IN.INRegister>) this.Mode.TransferEntry.transfer;
          PXSelectBase<INTran> transactions = (PXSelectBase<INTran>) this.Mode.TransferEntry.transactions;
          PXSelectBase<INTranSplit> splits = (PXSelectBase<INTranSplit>) this.Mode.TransferEntry.splits;
          if (transfer.Current == null)
          {
            PX.Objects.IN.INRegister instance = (PX.Objects.IN.INRegister) ((PXSelectBase) transfer).Cache.CreateInstance();
            instance.SiteID = this.Basis.SiteID;
            instance.ToSiteID = this.Basis.SiteID;
            instance.POReceiptType = this.Basis.Receipt.ReceiptType;
            instance.POReceiptNbr = this.Basis.Receipt.ReceiptNbr;
            instance.OrigModule = "PO";
            transfer.Insert(instance);
            flag1 = true;
          }
          INTranSplit[] inTranSplitArray1;
          if (!flag1)
            inTranSplitArray1 = GraphHelper.RowCast<INTranSplit>((IEnumerable) PXSelectBase<POReceiptSplitToTransferSplitLink, PXViewOf<POReceiptSplitToTransferSplitLink>.BasedOn<SelectFromBase<POReceiptSplitToTransferSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTranSplit>.On<POReceiptSplitToTransferSplitLink.FK.TransferLineSplit>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptType>, Field<POReceiptSplitToTransferSplitLink.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToTransferSplitLink.receiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>, PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>.SameAsCurrent>, And<BqlOperand<POReceiptSplitToTransferSplitLink.transferRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INTranSplit.toLocationID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[1]
            {
              (object) receivedSplit
            }, new object[2]
            {
              (object) transfer.Current.RefNbr,
              (object) this.Basis.PutAwayToLocationID
            })).ToArray<INTranSplit>();
          else
            inTranSplitArray1 = Array.Empty<INTranSplit>();
          INTranSplit[] first = inTranSplitArray1;
          INTranSplit[] inTranSplitArray2;
          if (!flag1)
            inTranSplitArray2 = GraphHelper.RowCast<INTranSplit>((IEnumerable) PXSelectBase<INTranSplit, PXViewOf<INTranSplit>.BasedOn<SelectFromBase<INTranSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTranSplit.refNbr, Equal<P.AsString>>>>, And<BqlOperand<INTranSplit.inventoryID, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INTranSplit.subItemID, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INTranSplit.siteID, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INTranSplit.locationID, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.locationID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INTranSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.lotSerialNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<INTranSplit.toLocationID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[1]
            {
              (object) receivedSplit
            }, new object[2]
            {
              (object) transfer.Current.RefNbr,
              (object) this.Basis.PutAwayToLocationID
            })).ToArray<INTranSplit>();
          else
            inTranSplitArray2 = Array.Empty<INTranSplit>();
          INTranSplit[] second = inTranSplitArray2;
          INTranSplit[] source = flag1 ? Array.Empty<INTranSplit>() : ((IEnumerable<INTranSplit>) first).Concat<INTranSplit>((IEnumerable<INTranSplit>) second).ToArray<INTranSplit>();
          bool flag2 = false;
          INTran inTran1;
          INTranSplit inSplit;
          if (source.Length == 0)
          {
            INTran inTran2 = transactions.With<PXSelectBase<INTran>, INTran>((Func<PXSelectBase<INTran>, INTran>) (_ => _.Insert() ?? _.Insert()));
            inTran2.InventoryID = receivedSplit.InventoryID;
            inTran2.SubItemID = receivedSplit.SubItemID;
            inTran2.LotSerialNbr = receivedSplit.LotSerialNbr;
            inTran2.ExpireDate = receivedSplit.ExpireDate;
            inTran2.UOM = receivedSplit.UOM;
            inTran2.SiteID = receivedSplit.SiteID;
            inTran2.LocationID = receivedSplit.LocationID;
            inTran2.ToSiteID = this.Basis.SiteID;
            inTran2.ToLocationID = this.Basis.PutAwayToLocationID;
            inTran2.POReceiptLineNbr = receivedSplit.LineNbr;
            bool flag3 = POLineType.IsNonStockNonServiceNonDropShip(receivedSplit.LineType);
            if (flag3)
            {
              inTran2.POLineType = receivedSplit.LineType;
              inTran2.InvtMult = new short?((short) 0);
            }
            INTran inTran3 = transactions.Update(inTran2);
            inTran3.LocationID = receivedSplit.LocationID;
            inTran3.ToLocationID = this.Basis.PutAwayToLocationID;
            inTran1 = transactions.Update(inTran3);
            inSplit = PXResultset<INTranSplit>.op_Implicit(splits.Search<INTranSplit.lineNbr>((object) inTran1.LineNbr, Array.Empty<object>()));
            if (inSplit == null)
            {
              INTranSplit inTranSplit = splits.With<PXSelectBase<INTranSplit>, INTranSplit>((Func<PXSelectBase<INTranSplit>, INTranSplit>) (_ => _.Insert() ?? _.Insert()));
              inTranSplit.LotSerialNbr = flag3 ? string.Empty : this.Basis.LotSerialNbr;
              inTranSplit.ExpireDate = this.Basis.ExpireDate;
              inTranSplit.ToSiteID = this.Basis.SiteID;
              inTranSplit.ToLocationID = this.Basis.PutAwayToLocationID;
              inSplit = splits.Update(inTranSplit);
            }
            flag2 = true;
          }
          else
          {
            INTranSplit inTranSplit1 = ((IEnumerable<INTranSplit>) source).FirstOrDefault<INTranSplit>((Func<INTranSplit, bool>) (s => string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase)));
            if (inTranSplit1 != null)
            {
              inTran1 = transactions.Current = PXResultset<INTran>.op_Implicit(transactions.Search<INTran.lineNbr>((object) inTranSplit1.LineNbr, Array.Empty<object>()));
              inSplit = PXResultset<INTranSplit>.op_Implicit(splits.Search<INTranSplit.splitLineNbr>((object) inTranSplit1.SplitLineNbr, Array.Empty<object>()));
            }
            else
            {
              inTran1 = transactions.Current = PXResultset<INTran>.op_Implicit(transactions.Search<INTran.lineNbr>((object) ((IEnumerable<INTranSplit>) source).First<INTranSplit>().LineNbr, Array.Empty<object>()));
              INTranSplit inTranSplit2 = splits.With<PXSelectBase<INTranSplit>, INTranSplit>((Func<PXSelectBase<INTranSplit>, INTranSplit>) (_ => _.Insert() ?? _.Insert()));
              inTranSplit2.LotSerialNbr = this.Basis.LotSerialNbr;
              inTranSplit2.ExpireDate = this.Basis.ExpireDate;
              inTranSplit2.ToSiteID = this.Basis.SiteID;
              inTranSplit2.ToLocationID = this.Basis.PutAwayToLocationID;
              inSplit = splits.Update(inTranSplit2);
              flag2 = true;
            }
          }
          Decimal? qty1 = inSplit.Qty;
          Decimal num1 = qty;
          Decimal? nullable = qty1.HasValue ? new Decimal?(qty1.GetValueOrDefault() + num1) : new Decimal?();
          Decimal num2 = 0M;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          {
            splits.Delete(inSplit);
          }
          else
          {
            INTranSplit inTranSplit = inSplit;
            nullable = inTranSplit.Qty;
            Decimal num3 = qty;
            inTranSplit.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num3) : new Decimal?();
            inSplit = splits.Update(inSplit);
          }
          INTran inTran4 = PXResultset<INTran>.op_Implicit(transactions.Search<INTran.lineNbr>((object) inTran1.LineNbr, Array.Empty<object>()));
          if (qty < 0M)
          {
            Decimal? qty2 = inTran4.Qty;
            Decimal num4 = qty;
            nullable = qty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() + num4) : new Decimal?();
            Decimal num5 = 0M;
            if (nullable.GetValueOrDefault() == num5 & nullable.HasValue)
            {
              transactions.Delete(inTran4);
            }
            else
            {
              INTran inTran5 = inTran4;
              nullable = inTran5.Qty;
              Decimal num6 = INUnitAttribute.ConvertFromBase(((PXSelectBase) transactions).Cache, inTran4.InventoryID, inTran4.UOM, qty, INPrecision.NOROUND);
              inTran5.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num6) : new Decimal?();
              transactions.Update(inTran4);
            }
          }
          ((PXAction) this.Mode.TransferEntry.Save).Press();
          if (flag1)
            this.Basis.TransferRefNbr = transfer.Current.RefNbr;
          if (flag2)
            inSplit = PXResultset<INTranSplit>.op_Implicit(splits.Search<INTranSplit.lineNbr, INTranSplit.splitLineNbr>((object) inSplit.LineNbr, (object) inSplit.SplitLineNbr, Array.Empty<object>()));
          return this.EnsureReceiptTransferSplitLink(receivedSplit, inSplit, qty);
        }

        protected virtual FlowStatus EnsureReceiptTransferSplitLink(
          PX.Objects.PO.POReceiptLineSplit poSplit,
          INTranSplit inSplit,
          Decimal deltaQty)
        {
          POReceiptSplitToTransferSplitLink[] array = GraphHelper.RowCast<POReceiptSplitToTransferSplitLink>((IEnumerable) PXSelectBase<POReceiptSplitToTransferSplitLink, PXViewOf<POReceiptSplitToTransferSplitLink>.BasedOn<SelectFromBase<POReceiptSplitToTransferSplitLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.transferDocType>.IsRelatedTo<INTranSplit.docType>, Field<POReceiptSplitToTransferSplitLink.transferRefNbr>.IsRelatedTo<INTranSplit.refNbr>, Field<POReceiptSplitToTransferSplitLink.transferLineNbr>.IsRelatedTo<INTranSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.transferSplitLineNbr>.IsRelatedTo<INTranSplit.splitLineNbr>>.WithTablesOf<INTranSplit, POReceiptSplitToTransferSplitLink>, INTranSplit, POReceiptSplitToTransferSplitLink>.SameAsCurrent.Or<KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptType>, Field<POReceiptSplitToTransferSplitLink.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToTransferSplitLink.receiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>, PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>.SameAsCurrent>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[2]
          {
            (object) inSplit,
            (object) poSplit
          }, Array.Empty<object>())).ToArray<POReceiptSplitToTransferSplitLink>();
          POReceiptSplitToTransferSplitLink transferSplitLink1 = ((IEnumerable<POReceiptSplitToTransferSplitLink>) array).FirstOrDefault<POReceiptSplitToTransferSplitLink>((Func<POReceiptSplitToTransferSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.transferDocType>.IsRelatedTo<INTranSplit.docType>, Field<POReceiptSplitToTransferSplitLink.transferRefNbr>.IsRelatedTo<INTranSplit.refNbr>, Field<POReceiptSplitToTransferSplitLink.transferLineNbr>.IsRelatedTo<INTranSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.transferSplitLineNbr>.IsRelatedTo<INTranSplit.splitLineNbr>>.WithTablesOf<INTranSplit, POReceiptSplitToTransferSplitLink>, INTranSplit, POReceiptSplitToTransferSplitLink>.Match(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), inSplit, link) && KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptType>, Field<POReceiptSplitToTransferSplitLink.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToTransferSplitLink.receiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>, PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>.Match(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), poSplit, link)));
          Decimal num1 = ((IEnumerable<POReceiptSplitToTransferSplitLink>) array).Where<POReceiptSplitToTransferSplitLink>((Func<POReceiptSplitToTransferSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.transferDocType>.IsRelatedTo<INTranSplit.docType>, Field<POReceiptSplitToTransferSplitLink.transferRefNbr>.IsRelatedTo<INTranSplit.refNbr>, Field<POReceiptSplitToTransferSplitLink.transferLineNbr>.IsRelatedTo<INTranSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.transferSplitLineNbr>.IsRelatedTo<INTranSplit.splitLineNbr>>.WithTablesOf<INTranSplit, POReceiptSplitToTransferSplitLink>, INTranSplit, POReceiptSplitToTransferSplitLink>.Match(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), inSplit, link))).Sum<POReceiptSplitToTransferSplitLink>((Func<POReceiptSplitToTransferSplitLink, Decimal>) (link => link.Qty.GetValueOrDefault()));
          Decimal num2 = ((IEnumerable<POReceiptSplitToTransferSplitLink>) array).Where<POReceiptSplitToTransferSplitLink>((Func<POReceiptSplitToTransferSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptType>, Field<POReceiptSplitToTransferSplitLink.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToTransferSplitLink.receiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>, PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>.Match(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), poSplit, link))).Sum<POReceiptSplitToTransferSplitLink>((Func<POReceiptSplitToTransferSplitLink, Decimal>) (link => link.Qty.GetValueOrDefault()));
          Decimal num3 = num1 + deltaQty;
          Decimal? nullable = inSplit.Qty;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          if (num3 > valueOrDefault1 & nullable.HasValue)
            return FlowStatus.Fail("Link quantity cannot be greater than the quantity of a transfer line split.", Array.Empty<object>());
          Decimal num4 = num2 + deltaQty;
          nullable = poSplit.Qty;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          if (num4 > valueOrDefault2 & nullable.HasValue)
            return FlowStatus.Fail("Link quantity cannot be greater than the quantity of a receipt line split.", Array.Empty<object>());
          int num5;
          if (transferSplitLink1 != null)
          {
            Decimal? qty = transferSplitLink1.Qty;
            Decimal num6 = deltaQty;
            nullable = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num6) : new Decimal?();
            Decimal num7 = 0M;
            num5 = nullable.GetValueOrDefault() < num7 & nullable.HasValue ? 1 : 0;
          }
          else
            num5 = deltaQty < 0M ? 1 : 0;
          if (num5 != 0)
            return FlowStatus.Fail("Link quantity cannot be negative.", Array.Empty<object>());
          POReceiptSplitToTransferSplitLink transferSplitLink2;
          if (transferSplitLink1 == null)
          {
            transferSplitLink2 = ((PXSelectBase<POReceiptSplitToTransferSplitLink>) this.Mode.TransferSplitLinks).Insert(new POReceiptSplitToTransferSplitLink()
            {
              ReceiptType = poSplit.ReceiptType,
              ReceiptNbr = poSplit.ReceiptNbr,
              ReceiptLineNbr = poSplit.LineNbr,
              ReceiptSplitLineNbr = poSplit.SplitLineNbr,
              TransferRefNbr = inSplit.RefNbr,
              TransferLineNbr = inSplit.LineNbr,
              TransferSplitLineNbr = inSplit.SplitLineNbr,
              Qty = new Decimal?(deltaQty)
            });
          }
          else
          {
            POReceiptSplitToTransferSplitLink transferSplitLink3 = transferSplitLink1;
            nullable = transferSplitLink3.Qty;
            Decimal num8 = deltaQty;
            transferSplitLink3.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num8) : new Decimal?();
            transferSplitLink2 = ((PXSelectBase<POReceiptSplitToTransferSplitLink>) this.Mode.TransferSplitLinks).Update(transferSplitLink1);
          }
          nullable = transferSplitLink2.Qty;
          Decimal num9 = 0M;
          if (nullable.GetValueOrDefault() == num9 & nullable.HasValue)
            ((PXSelectBase<POReceiptSplitToTransferSplitLink>) this.Mode.TransferSplitLinks).Delete(transferSplitLink2);
          return FlowStatus.Ok;
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm putting away {0} x {1} {2}.";
        public const string NothingToPutAway = "No items to put away.";
        public const string Overputting = "The put away quantity cannot be greater than the received quantity.";
        public const string Underputting = "The put away quantity cannot be negative.";
        public const string InventoryAdded = "{0} x {1} {2} added to target location.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from target location.";
        public const string LinkTransferOverpicking = "Link quantity cannot be greater than the quantity of a transfer line split.";
        public const string LinkReceiptOverpicking = "Link quantity cannot be greater than the quantity of a receipt line split.";
        public const string LinkUnderpicking = "Link quantity cannot be negative.";
      }
    }

    public sealed class CommandOrReceiptOnlyState : CommandOnlyStateBase<ReceivePutAway>
    {
      public virtual void MoveToNextState()
      {
      }

      public virtual string Prompt
      {
        get
        {
          return !((ScanComponent<ReceivePutAway>) this).Basis.Get<ReceivePutAway.PutAwayMode.Logic>().CanSwitchReceipt ? base.Prompt : "Use any command or scan the next receipt number to continue.";
        }
      }

      public virtual bool Process(string barcode)
      {
        if (!((ScanComponent<ReceivePutAway>) this).Basis.Get<ReceivePutAway.PutAwayMode.Logic>().CanSwitchReceipt)
          return base.Process(barcode);
        if (((ScanComponent<ReceivePutAway>) this).Basis.TryProcessBy<ReceivePutAway.PutAwayMode.ReceiptState>(barcode, (StateSubstitutionRule) 0))
        {
          ((ScanComponent<ReceivePutAway>) this).Basis.Clear<ReceivePutAway.PutAwayMode.ReceiptState>(true);
          ((ScanComponent<ReceivePutAway>) this).Basis.Reset(false);
          ((ScanComponent<ReceivePutAway>) this).Basis.SetScanState<ReceivePutAway.PutAwayMode.ReceiptState>((string) null, Array.Empty<object>());
          ((ScanState<ReceivePutAway>) ((ScanComponent<ReceivePutAway>) this).Basis.CurrentMode.FindState<ReceivePutAway.PutAwayMode.ReceiptState>(false)).Process(barcode);
          return true;
        }
        ((ScanComponent<ReceivePutAway>) this).Basis.Reporter.Error("Only commands or a receipt number can be used to continue.", Array.Empty<object>());
        return false;
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string UseCommandOrReceiptToContinue = "Use any command or scan the next receipt number to continue.";
        public const string OnlyCommandsAndReceiptsAreAllowed = "Only commands or a receipt number can be used to continue.";
      }
    }

    public sealed class ReleaseTransferCommand : 
      BarcodeDrivenStateMachine<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.ScanCommand
    {
      public virtual 
      #nullable disable
      string Code => "RELEASE";

      public virtual string ButtonName => "scanReleaseTransfer";

      public virtual string DisplayName => "Release Transfer";

      protected virtual bool IsEnabled
      {
        get
        {
          if (!((ScanComponent<ReceivePutAway>) this).Basis.DocumentIsEditable || ((ScanComponent<ReceivePutAway>) this).Basis.TransferRefNbr == null)
            return false;
          PX.Objects.IN.INRegister transfer = this.Get<ReceivePutAway.PutAwayMode.Logic>().GetTransfer();
          if (transfer == null)
            return false;
          bool? released = transfer.Released;
          bool flag = false;
          return released.GetValueOrDefault() == flag & released.HasValue;
        }
      }

      protected virtual bool Process()
      {
        return this.Get<ReceivePutAway.PutAwayMode.ReleaseTransferCommand.Logic>().ReleaseTransfer();
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public virtual bool ReleaseTransfer()
        {
          PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.Basis.Get<ReceivePutAway.PutAwayMode.Logic>().TransferEntry.transfer).Current;
          int num;
          if (current == null)
          {
            num = 1;
          }
          else
          {
            bool? released = current.Released;
            bool flag = false;
            num = !(released.GetValueOrDefault() == flag & released.HasValue) ? 1 : 0;
          }
          if (num != 0)
          {
            this.Basis.ReportError("No transfers to release.", Array.Empty<object>());
            return true;
          }
          this.Basis.Reset(false);
          this.Basis.Clear<ReceivePutAway.PutAwayMode.InventoryItemState>(true);
          this.Basis.WaitFor<PX.Objects.IN.INRegister>((Action<ReceivePutAway, PX.Objects.IN.INRegister>) ((basis, doc) => ReceivePutAway.PutAwayMode.ReleaseTransferCommand.Logic.ReleaseTransferImpl(doc))).WithDescription("Release of {0} transfer in progress.", new object[1]
          {
            (object) this.Basis.TransferRefNbr
          }).ActualizeDataBy((Func<ReceivePutAway, PX.Objects.IN.INRegister, PX.Objects.IN.INRegister>) ((basis, doc) => (PX.Objects.IN.INRegister) PrimaryKeyOf<PX.Objects.IN.INRegister>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>>.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) basis), (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>) doc, (PKFindOptions) 0))).OnSuccess((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.IN.INRegister>.ISuccessProcessor>) (x => x.Say("Transfer successfully released.", Array.Empty<object>()).ChangeStateTo<ReceivePutAway.PutAwayMode.ReceiptState>())).OnFail((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.IN.INRegister>.IResultProcessor>) (x => x.Say("Transfer not released.", Array.Empty<object>()))).BeginAwait(current);
          return true;
        }

        private static void ReleaseTransferImpl(
        #nullable disable
        PX.Objects.IN.INRegister transfer)
        {
          INTransferEntry instance = PXGraph.CreateInstance<INTransferEntry>();
          ((PXSelectBase<PX.Objects.IN.INRegister>) instance.transfer).Current = (PX.Objects.IN.INRegister) PrimaryKeyOf<PX.Objects.IN.INRegister>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>>.Find((PXGraph) instance, (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>) transfer, (PKFindOptions) 1);
          ((PXSelectBase) instance.transfer).Cache.SetValueExt<PX.Objects.IN.INRegister.hold>((object) ((PXSelectBase<PX.Objects.IN.INRegister>) instance.transfer).Current, (object) false);
          ((PXSelectBase<PX.Objects.IN.INRegister>) instance.transfer).UpdateCurrent();
          ((PXAction) instance.release).Press();
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Release Transfer";
        public const string NotPossible = "No transfers to release.";
        public const string InProgress = "Release of {0} transfer in progress.";
        public const string Success = "Transfer successfully released.";
        public const string Fail = "Transfer not released.";
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<ReceivePutAway>.SetMode<ReceivePutAway.PutAwayMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "PUTAWAY";

      public virtual string DisplayName => "Put Away";

      private string RefNbr { get; set; }

      public virtual bool IsPossible
      {
        get
        {
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSReceiving>() ? 1 : 0;
          POReceivePutAwaySetup receivePutAwaySetup = POReceivePutAwaySetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          return num != 0 && receivePutAwaySetup != null && receivePutAwaySetup.ShowPutAwayTab.GetValueOrDefault();
        }
      }

      protected virtual bool PrepareRedirect()
      {
        if (((ScanComponent<TForeignBasis>) this).Basis is ReceivePutAway basis && basis.RefNbr != null)
        {
          Validation? nullable = ((ScanMode<ReceivePutAway>) basis.FindMode<ReceivePutAway.PutAwayMode>()).TryValidate<PX.Objects.PO.POReceipt>(basis.Receipt).By<ReceivePutAway.PutAwayMode.ReceiptState>();
          if (nullable.HasValue)
          {
            Validation valueOrDefault = nullable.GetValueOrDefault();
            if (((Validation) ref valueOrDefault).IsError.GetValueOrDefault())
            {
              basis.ReportError(((Validation) ref valueOrDefault).Message, ((Validation) ref valueOrDefault).MessageArgs);
              return false;
            }
          }
          this.RefNbr = basis.RefNbr;
        }
        return true;
      }

      protected virtual void CompleteRedirect()
      {
        if (!(((ScanComponent<TForeignBasis>) this).Basis is ReceivePutAway basis) || !(basis.CurrentMode.Code != "VRTN") || this.RefNbr == null || !basis.TryProcessBy("RNBR", this.RefNbr, (StateSubstitutionRule) 253))
          return;
        basis.SetDefaultState((string) null, Array.Empty<object>());
        this.RefNbr = (string) null;
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Put Away";
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<
    #nullable enable
    ReceivePutAway>.Msg
    {
      public const 
      #nullable disable
      string Description = "Put Away";
      public const string Completed = "{0} receipt put away.";
    }

    [PXUIField(Visible = false)]
    public class ShowPutAway : 
      PXFieldAttachedTo<ScanHeader>.By<ReceivePutAway.Host>.AsBool.Named<ReceivePutAway.PutAwayMode.ShowPutAway>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(((PXSelectBase<POReceivePutAwaySetup>) this.Base.WMS.Setup).Current.ShowPutAwayTab.GetValueOrDefault() && row.Mode == "PTAW");
      }
    }

    public class ToLocationID : 
      PXFieldAttachedTo<PX.Objects.PO.POReceiptLineSplit>.By<ReceivePutAway.Host>.AsInteger
    {
      public override int? GetValue(PX.Objects.PO.POReceiptLineSplit Row) => new int?();

      protected override bool SuppressValueSetting => true;

      protected override PXFieldState AdjustStateByRow(PXFieldState state, PX.Objects.PO.POReceiptLineSplit row)
      {
        PXCache cache = ((PXSelectBase) this.Base.WMS.Get<ReceivePutAway.PutAwayMode.Logic>().TransferEntry.transactions).Cache;
        if (row == null)
        {
          state = (PXFieldState) cache.GetStateExt<INTran.toLocationID>((object) null);
        }
        else
        {
          PXResult<POReceiptSplitToTransferSplitLink, INTran>[] array = ((IEnumerable<PXResult<POReceiptSplitToTransferSplitLink>>) PXSelectBase<POReceiptSplitToTransferSplitLink, PXViewOf<POReceiptSplitToTransferSplitLink>.BasedOn<SelectFromBase<POReceiptSplitToTransferSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTran>.On<POReceiptSplitToTransferSplitLink.FK.TransferLine>>>.Where<KeysRelation<CompositeKey<Field<POReceiptSplitToTransferSplitLink.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptType>, Field<POReceiptSplitToTransferSplitLink.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToTransferSplitLink.receiptLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToTransferSplitLink.receiptSplitLineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLineSplit.splitLineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>, PX.Objects.PO.POReceiptLineSplit, POReceiptSplitToTransferSplitLink>.SameAsCurrent>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POReceiptLineSplit[1]
          {
            row
          }, Array.Empty<object>())).AsEnumerable<PXResult<POReceiptSplitToTransferSplitLink>>().Cast<PXResult<POReceiptSplitToTransferSplitLink, INTran>>().ToArray<PXResult<POReceiptSplitToTransferSplitLink, INTran>>();
          if (array.Length == 0)
          {
            state = (PXFieldState) cache.GetStateExt<INTran.toLocationID>((object) null);
            state.Value = (object) "";
          }
          else if (array.Length == 1 || ((IEnumerable<PXResult<POReceiptSplitToTransferSplitLink, INTran>>) array).GroupBy<PXResult<POReceiptSplitToTransferSplitLink, INTran>, int?>((Func<PXResult<POReceiptSplitToTransferSplitLink, INTran>, int?>) (l => ((PXResult) l).GetItem<INTran>().ToLocationID)).Count<IGrouping<int?, PXResult<POReceiptSplitToTransferSplitLink, INTran>>>() == 1)
          {
            INTran inTran = ((PXResult) array[0]).GetItem<INTran>();
            INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, inTran.ToLocationID);
            state = (PXFieldState) cache.GetStateExt<INTran.toLocationID>((object) inTran);
            state.Value = ((PXGraph) this.Base).IsMobile ? (object) inLocation?.Descr : (object) inLocation?.LocationCD;
          }
          else
          {
            state = (PXFieldState) cache.GetStateExt<INTran.toLocationID>((object) null);
            state.Value = (object) this.Base.WMS.Localize("<SPLIT>", Array.Empty<object>());
          }
        }
        state = PXFieldState.CreateInstance(state.Value, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, "toLocationID", (string) null, state.DisplayName, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(this.Base.WMS.Header.Mode == "PTAW"), new bool?(true), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
        return state;
      }
    }
  }

  public sealed class ReceiveMode : 
    BarcodeDrivenStateMachine<
    #nullable enable
    ReceivePutAway, ReceivePutAway.Host>.ScanMode
  {
    public const 
    #nullable disable
    string Value = "RCPT";

    public ReceivePutAway.ReceiveMode.Logic Body => this.Get<ReceivePutAway.ReceiveMode.Logic>();

    public virtual string Code => "RCPT";

    public virtual string Description => "Receive";

    protected virtual IEnumerable<ScanState<ReceivePutAway>> CreateStates()
    {
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ReceiptState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReceiveMode.SpecifyWarehouseState();
      yield return (ScanState<ReceivePutAway>) ((MethodInterceptor<EntityState<ReceivePutAway, INLocation>, ReceivePutAway>.OfPredicate) ((MethodInterceptor<EntityState<ReceivePutAway, INLocation>, ReceivePutAway>.OfPredicate) ((EntityState<ReceivePutAway, INLocation>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState()).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => basis.LocationID.HasValue), false, new RelativeInject?()).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => !basis.Get<ReceivePutAway.ReceiveMode.Logic>().IsLocationUserInputRequired), false, new RelativeInject?());
      yield return (ScanState<ReceivePutAway>) ((MethodInterceptor<EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>.AsAppendable) ((EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) ((MethodInterceptor<EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>.AsAppendable) ((EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) ((MethodInterceptor<EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, ReceivePutAway>.OfFunc<string>) ((EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState()
      {
        AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.VPN)
      }).Intercept.StatePrompt).ByOverride((Func<ReceivePutAway, Func<string>, string>) ((basis, base_StatePrompt) => basis.Remove.GetValueOrDefault() || !basis.PrevInventoryID.HasValue || basis.Get<ReceivePutAway.ReceiveMode.Logic>().CanReceiveOriginalLines ? base_StatePrompt() : "Scan the item or the next receipt number."), new RelativeInject?())).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((basis, barcode) => !basis.Get<ReceivePutAway.ReceiveMode.Logic>().IsSingleLocation && basis.TryProcessBy<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Done) : AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Skipped)), new RelativeInject?())).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((basis, barcode) =>
      {
        if (basis.TryProcessBy<ReceivePutAway.ReceiveMode.ReceiptState>(barcode, (StateSubstitutionRule) 1))
        {
          basis.Reset(true);
          basis.SetScanState<ReceivePutAway.ReceiveMode.ReceiptState>((string) null, Array.Empty<object>());
          if (((ScanState<ReceivePutAway>) basis.FindState<ReceivePutAway.ReceiveMode.ReceiptState>(false)).Process(barcode))
            return AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Done);
        }
        return AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Skipped);
      }), new RelativeInject?());
      yield return (ScanState<ReceivePutAway>) ((MethodInterceptor<EntityState<ReceivePutAway, string>, ReceivePutAway>.OfPredicate) ((EntityState<ReceivePutAway, string>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState()).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => !basis.Get<ReceivePutAway.ReceiveMode.Logic>().IsLotSerialUserInputRequired), false, new RelativeInject?());
      yield return (ScanState<ReceivePutAway>) ((MethodInterceptor<EntityState<ReceivePutAway, DateTime?>, ReceivePutAway>.OfPredicate) ((EntityState<ReceivePutAway, DateTime?>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState()).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => basis.Get<ReceivePutAway.ReceiveMode.Logic>().With<ReceivePutAway.ReceiveMode.Logic, bool>((Func<ReceivePutAway.ReceiveMode.Logic, bool>) (m => !m.IsLotSerialUserInputRequired || !m.IsExpireDateUserInputRequired))), false, new RelativeInject?());
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ConfirmState();
    }

    protected virtual IEnumerable<ScanTransition<ReceivePutAway>> CreateTransitions()
    {
      return !this.Body.IsSingleLocation && this.Body.PromptLocationForEveryLine ? ((ScanMode<ReceivePutAway>) this).StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<ReceivePutAway.ReceiveMode.ReceiptState>().NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>((Action<ReceivePutAway>) null))) : ((ScanMode<ReceivePutAway>) this).StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<ReceivePutAway.ReceiveMode.ReceiptState>().NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState>((Action<ReceivePutAway>) null)));
    }

    protected virtual IEnumerable<ScanCommand<ReceivePutAway>> CreateCommands()
    {
      yield return (ScanCommand<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.RemoveCommand();
      yield return (ScanCommand<ReceivePutAway>) new BarcodeQtySupport<ReceivePutAway, ReceivePutAway.Host>.SetQtyCommand();
      yield return (ScanCommand<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ReleaseReceiptCommand();
      yield return (ScanCommand<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ReleaseReceiptAndCompletePOLinesCommand();
      yield return (ScanCommand<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ConfirmReceiptCommand();
    }

    protected virtual IEnumerable<ScanQuestion<ReceivePutAway>> CreateQuestions()
    {
      yield return (ScanQuestion<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ConfirmState.OverreceiveQuestion();
    }

    protected virtual IEnumerable<ScanRedirect<ReceivePutAway>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<ReceivePutAway>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<ReceivePutAway>) this).ResetMode(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.ReceiveMode.ReceiptState>(fullReset && !((ScanMode<ReceivePutAway>) this).Basis.IsWithinReset);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>(fullReset || this.Body.PromptLocationForEveryLine && !this.Body.IsSingleLocation);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>(true);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState>(true);
      if (!fullReset)
        return;
      ((ScanMode<ReceivePutAway>) this).Basis.PONbr = (string) null;
      ((ScanMode<ReceivePutAway>) this).Basis.PrevInventoryID = new int?();
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ReceivePutAway.ReceiveMode.value>
    {
      public value()
        : base("RCPT")
      {
      }
    }

    public class Logic : BarcodeDrivenStateMachine<
    #nullable enable
    ReceivePutAway, ReceivePutAway.Host>.ScanExtension
    {
      public 
      #nullable disable
      FbqlSelect<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>, PX.Objects.PO.POReceiptLineSplit>.View Received;
      public FbqlSelect<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>, PX.Objects.PO.POReceiptLineSplit>.View ReceivedNotZero;
      public PXAction<ScanHeader> ReviewReceive;

      public virtual bool CanReceiveOriginalLines
      {
        get
        {
          PX.Objects.PO.POReceipt receipt = this.Basis.Receipt;
          if ((receipt != null ? (receipt.WMSSingleOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            PX.Objects.PO.POReceiptLine[] array = ((PXSelectBase) this.Basis.Graph.transactions).Cache.Inserted.ToArray<PX.Objects.PO.POReceiptLine>();
            if (array.Length <= 1)
            {
              int? nullable = array.Length == 0 ? new int?() : array[0].POLineNbr;
              if (PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<POOrderType.regularOrder>>>>, And<BqlOperand<PX.Objects.PO.POLine.orderNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofInt>, IsNull>>>>.Or<BqlOperand<PX.Objects.PO.POLine.lineNbr, IBqlInt>.IsNotEqual<P.AsInt>>>>, And<BqlOperand<PX.Objects.PO.POLine.orderQty, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlOperand<PX.Objects.PO.POLine.orderQty, IBqlDecimal>.IsGreater<PX.Objects.PO.POLine.receivedQty>>>>.ReadOnly.Config>.SelectWindowed(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), 0, 1, new object[3]
              {
                (object) this.Basis.Receipt.OrigPONbr,
                (object) nullable,
                (object) nullable
              })) != null)
                return true;
            }
          }
          PX.Objects.PO.POReceiptLineSplit[] source = ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Received).SelectMain(Array.Empty<object>());
          return !((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) source).Any<PX.Objects.PO.POReceiptLineSplit>() || ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) source).Any<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            Decimal? receivedQty = s.ReceivedQty;
            Decimal? qty = s.Qty;
            return receivedQty.GetValueOrDefault() < qty.GetValueOrDefault() & receivedQty.HasValue & qty.HasValue || this.GetExtendedRestQty(s) > 0M;
          }));
        }
      }

      public virtual bool IsUnboundSplit(PX.Objects.PO.POReceiptLineSplit s) => s.PONbr == null;

      public virtual IEnumerable received()
      {
        return (IEnumerable) this.Basis.SortedResult((IEnumerable) this.Basis.GetSplits(this.Basis.Receipt, true, processedSeparator: this.RowProcessedPredicate));
      }

      public virtual IEnumerable receivedNotZero()
      {
        return (IEnumerable) this.Basis.SortedResult((IEnumerable) this.Basis.GetSplits(this.Basis.Receipt, true, processedSeparator: this.RowProcessedPredicate).Where<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, bool>) (s =>
        {
          Decimal? receivedQty = PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>.op_Implicit(s).ReceivedQty;
          Decimal num = 0M;
          return receivedQty.GetValueOrDefault() > num & receivedQty.HasValue;
        })));
      }

      private Func<PX.Objects.PO.POReceiptLineSplit, bool> RowProcessedPredicate
      {
        get
        {
          PX.Objects.PO.POReceipt receipt = this.Basis.Receipt;
          return (receipt != null ? (receipt.WMSSingleOrder.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? (Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r => this.GetNormalRestQty(r) == 0M) : (Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
          {
            Decimal? receivedQty = r.ReceivedQty;
            Decimal? qty = r.Qty;
            return receivedQty.GetValueOrDefault() == qty.GetValueOrDefault() & receivedQty.HasValue == qty.HasValue;
          });
        }
      }

      [PXButton(CommitChanges = true)]
      [PXUIField(DisplayName = "Review")]
      protected virtual IEnumerable reviewReceive(PXAdapter adapter) => adapter.Get();

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        ((PXAction) this.ReviewReceive).SetVisible(((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base).IsMobile && e.Row?.Mode == "RCPT");
      }

      public bool PromptLocationForEveryLine
      {
        get
        {
          return ((PXSelectBase<POReceivePutAwaySetup>) this.Basis.Setup).Current.RequestLocationForEachItemInReceive.GetValueOrDefault();
        }
      }

      public bool IsSingleLocation
      {
        get
        {
          return PXSetupBase<ReceivePutAway.UserSetup, ReceivePutAway.Host, ScanHeader, POReceivePutAwayUserSetup, Where<POReceivePutAwayUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis)).SingleLocation.GetValueOrDefault();
        }
      }

      public bool IsLocationUserInputRequired
      {
        get
        {
          return this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>() && !this.Basis.DefaultLocationID.HasValue;
        }
      }

      public bool UseDefaultLotSerial
      {
        get
        {
          return PXSetupBase<ReceivePutAway.UserSetup, ReceivePutAway.Host, ScanHeader, POReceivePutAwayUserSetup, Where<POReceivePutAwayUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis)).DefaultLotSerialNumber.GetValueOrDefault();
        }
      }

      public bool IsLotSerialUserInputRequired
      {
        get
        {
          if (!this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>())
            return false;
          if (!this.UseDefaultLotSerial)
            return true;
          bool? autoNextNbr = this.Basis.SelectedLotSerialClass.AutoNextNbr;
          bool flag = false;
          return autoNextNbr.GetValueOrDefault() == flag & autoNextNbr.HasValue;
        }
      }

      public bool UseDefaultExpireDate
      {
        get
        {
          return PXSetupBase<ReceivePutAway.UserSetup, ReceivePutAway.Host, ScanHeader, POReceivePutAwayUserSetup, Where<POReceivePutAwayUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis)).DefaultExpireDate.GetValueOrDefault();
        }
      }

      public bool IsExpireDateUserInputRequired
      {
        get
        {
          if (!this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState>())
            return false;
          return !this.UseDefaultExpireDate || !this.EnsureExpireDateDefault().HasValue;
        }
      }

      public DateTime? EnsureExpireDateDefault()
      {
        DateTime? nullable = this.Basis.EnsureExpireDateDefault();
        if (nullable.HasValue)
          return nullable;
        IEnumerable<PX.Objects.PO.POReceiptLineSplit> source = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Received).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
        {
          int? inventoryId1 = s.InventoryID;
          int? inventoryId2 = this.Basis.InventoryID;
          return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
        }));
        if (!this.UseDefaultExpireDate)
          source = source.Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            Decimal? receivedQty = s.ReceivedQty;
            Decimal num = 0M;
            return receivedQty.GetValueOrDefault() > num & receivedQty.HasValue;
          }));
        return source.Select<PX.Objects.PO.POReceiptLineSplit, DateTime?>((Func<PX.Objects.PO.POReceiptLineSplit, DateTime?>) (s => s.ExpireDate)).FirstOrDefault<DateTime?>();
      }

      public Decimal GetOverallReceivedQty(PX.Objects.PO.POReceiptLineSplit split)
      {
        return this.GetSplitQuantities(split).overallReceivedQty;
      }

      public Decimal GetNormalReceivedQty(PX.Objects.PO.POReceiptLineSplit split)
      {
        return this.GetSplitQuantities(split).normalReceiptQty;
      }

      public Decimal GetNormalRestQty(PX.Objects.PO.POReceiptLineSplit split)
      {
        return this.GetSplitQuantities(split).normalRestQty;
      }

      public Decimal GetExtendedReceivedQty(PX.Objects.PO.POReceiptLineSplit split)
      {
        return this.GetSplitQuantities(split).extendedReceiptQty;
      }

      public Decimal GetExtendedRestQty(PX.Objects.PO.POReceiptLineSplit split)
      {
        return this.GetExtendedRestQty(split, true);
      }

      public Decimal GetExtendedRestQty(
        PX.Objects.PO.POReceiptLineSplit split,
        bool substituteSerialNumberedQuantities)
      {
        PX.Objects.PO.POReceiptLine poReceiptLine;
        PX.Objects.PO.POLine poLine1;
        this.SelectPOLineAndReceiptLineByReceiptSplit(split).Deconstruct(ref poReceiptLine, ref poLine1);
        PX.Objects.PO.POReceiptLine line = poReceiptLine;
        PX.Objects.PO.POLine poLine2 = poLine1;
        return !substituteSerialNumberedQuantities || !this.CheckIsSerialNumberedWhenReceivedItem(line) ? this.GetRegularSplitQuantities(line, split, poLine2).extendedRestQty : this.GetSplitQuantitiesForSerialNumberedWhenReceivedItems(line, split, poLine2).extendedRestQty;
      }

      protected (Decimal overallReceivedQty, Decimal normalReceiptQty, Decimal normalRestQty, Decimal extendedReceiptQty, Decimal extendedRestQty) GetSplitQuantities(
        PX.Objects.PO.POReceiptLineSplit split)
      {
        PX.Objects.PO.POReceiptLine poReceiptLine;
        PX.Objects.PO.POLine poLine1;
        this.SelectPOLineAndReceiptLineByReceiptSplit(split).Deconstruct(ref poReceiptLine, ref poLine1);
        PX.Objects.PO.POReceiptLine line = poReceiptLine;
        PX.Objects.PO.POLine poLine2 = poLine1;
        return !this.CheckIsSerialNumberedWhenReceivedItem(line) ? this.GetRegularSplitQuantities(line, split, poLine2) : this.GetSplitQuantitiesForSerialNumberedWhenReceivedItems(line, split, poLine2);
      }

      protected bool CheckIsSerialNumberedWhenReceivedItem(PX.Objects.PO.POReceiptLine line)
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), line.InventoryID);
        INLotSerClass inLotSerClass = INLotSerClass.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), inventoryItem?.LotSerClassID);
        return inLotSerClass != null && inLotSerClass.LotSerTrack == "S" && inLotSerClass.LotSerAssign == "R";
      }

      protected (Decimal overallReceivedQty, Decimal normalReceiptQty, Decimal normalRestQty, Decimal extendedReceiptQty, Decimal extendedRestQty) GetSplitQuantitiesForSerialNumberedWhenReceivedItems(
        PX.Objects.PO.POReceiptLine line,
        PX.Objects.PO.POReceiptLineSplit split,
        PX.Objects.PO.POLine poLine)
      {
        return (this.GetOverallReceivedQty(line, poLine), 1M, 1M - split.ReceivedQty.Value, 1M, 1M - split.ReceivedQty.Value);
      }

      protected (Decimal overallReceivedQty, Decimal normalReceiptQty, Decimal normalRestQty, Decimal extendedReceiptQty, Decimal extendedRestQty) GetRegularSplitQuantities(
        PX.Objects.PO.POReceiptLine line,
        PX.Objects.PO.POReceiptLineSplit split,
        PX.Objects.PO.POLine poLine)
      {
        Decimal overallReceivedQty = this.GetOverallReceivedQty(line, poLine);
        int num1 = poLine == null ? 1 : (poLine.OrderNbr == null ? 1 : 0);
        Decimal num2 = num1 != 0 ? line.BaseQty.Value : poLine.BaseOrderQty.Value;
        Decimal num3 = PXDBQuantityAttribute.Round(new Decimal?(Math.Max(0M, num2 - overallReceivedQty)));
        Decimal num4 = num1 != 0 ? 0M : poLine.BaseOrderQty.Value * (poLine.RcptQtyMax.Value - 100M) / 100M;
        Decimal num5 = PXDBQuantityAttribute.Round(new Decimal?(num4 + (num2 - overallReceivedQty)));
        return (overallReceivedQty, num2, num3, num4, num5);
      }

      protected Decimal GetOverallReceivedQty(PX.Objects.PO.POReceiptLine line, PX.Objects.PO.POLine poLine)
      {
        return (poLine == null ? 1 : (poLine.OrderNbr == null ? 1 : 0)) == 0 ? ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit>>) PXSelectBase<PX.Objects.PO.POReceiptLineSplit, PXViewOf<PX.Objects.PO.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.pOType>.IsRelatedTo<PX.Objects.PO.POLine.orderType>, Field<PX.Objects.PO.POReceiptLine.pONbr>.IsRelatedTo<PX.Objects.PO.POLine.orderNbr>, Field<PX.Objects.PO.POReceiptLine.pOLineNbr>.IsRelatedTo<PX.Objects.PO.POLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POLine, PX.Objects.PO.POReceiptLine>.SameAsCurrent>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POLine[1]
        {
          poLine
        }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit>>().Sum<PXResult<PX.Objects.PO.POReceiptLineSplit>>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit>, Decimal>) (s => ReceivedQty(PXResult<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(s), ((PXResult) s).GetItem<PX.Objects.PO.POReceiptLine>()).GetValueOrDefault())) : ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit>>) PXSelectBase<PX.Objects.PO.POReceiptLineSplit, PXViewOf<PX.Objects.PO.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptType>, Field<PX.Objects.PO.POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptNbr>, Field<PX.Objects.PO.POReceiptLineSplit.lineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLineSplit>, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLineSplit>.SameAsCurrent>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceiptLine[1]
        {
          line
        }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLineSplit>>().Sum<PXResult<PX.Objects.PO.POReceiptLineSplit>>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit>, Decimal>) (s => ReceivedQty(PXResult<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(s), line).GetValueOrDefault()));

        static Decimal? ReceivedQty(PX.Objects.PO.POReceiptLineSplit rls, PX.Objects.PO.POReceiptLine rl)
        {
          if (!rl.Released.GetValueOrDefault())
            return rls.BaseReceivedQty;
          Decimal? baseQty = rls.BaseQty;
          short? invtMult = rls.InvtMult;
          Decimal? nullable = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
          return !(baseQty.HasValue & nullable.HasValue) ? new Decimal?() : new Decimal?(baseQty.GetValueOrDefault() * nullable.GetValueOrDefault());
        }
      }

      protected PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine> SelectPOLineAndReceiptLineByReceiptSplit(
        PX.Objects.PO.POReceiptLineSplit split)
      {
        return (PXResult<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine>) PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.PO.POLine>.On<PX.Objects.PO.POReceiptLine.FK.OrderLine>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[3]
        {
          (object) split.ReceiptType,
          (object) split.ReceiptNbr,
          (object) split.LineNbr
        }));
      }
    }

    public sealed class ReceiptState : ReceivePutAway.ReceiptState
    {
      public bool OrderComesFirst { get; set; }

      public int LargeOrderLinesCount { get; set; } = 15;

      public ReceivePutAway.ReceiveMode.ReceiptState.Logic Body
      {
        get => this.Get<ReceivePutAway.ReceiveMode.ReceiptState.Logic>();
      }

      protected override PX.Objects.PO.POReceipt GetByBarcode(string barcode)
      {
        return PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.PO.POReceipt.vendorID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<POReceiptType.poreceipt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AP.Vendor, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
        {
          (object) barcode
        }));
      }

      protected virtual Validation Validate(PX.Objects.PO.POReceipt receipt)
      {
        this.Body.SiteID = receipt.SiteID;
        if (!this.Body.IsNewReceipt)
        {
          bool? released = receipt.Released;
          bool flag = false;
          if ((!(released.GetValueOrDefault() == flag & released.HasValue) || receipt.Hold.GetValueOrDefault() && !receipt.WMSSingleOrder.GetValueOrDefault() ? 1 : (receipt.Received.GetValueOrDefault() ? 1 : 0)) != 0)
            return Validation.Fail("The {0} receipt cannot be processed because it has the {1} status.", new object[2]
            {
              (object) receipt.ReceiptNbr,
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.status>((IBqlTable) receipt)
            });
          if (receipt.ReceiptType != "RT")
            return Validation.Fail("The {0} receipt cannot be processed because it has the {1} type.", new object[2]
            {
              (object) receipt.ReceiptNbr,
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.receiptType>((IBqlTable) receipt)
            });
          if (receipt.POType == "DP")
            return Validation.Fail("The {0} receipt cannot be processed because the related order has the {1} type.", new object[2]
            {
              (object) receipt.ReceiptNbr,
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.pOType>((IBqlTable) receipt)
            });
          int? singleSiteID;
          if (!((ScanComponent<ReceivePutAway>) this).Basis.HasSingleSiteInLines(receipt, out singleSiteID))
            return Validation.Fail("All items in the {0} receipt must be located in the same warehouse.", new object[1]
            {
              (object) receipt.ReceiptNbr
            });
          if (!receipt.SiteID.HasValue)
            this.Body.SiteID = singleSiteID;
          if (((ScanComponent<ReceivePutAway>) this).Basis.HasNonStockKit(receipt))
            Validation.Fail("The {0} receipt cannot be processed because it contains a non-stock kit item.", new object[1]
            {
              (object) receipt.ReceiptNbr
            });
        }
        return Validation.Ok;
      }

      protected override void Apply(PX.Objects.PO.POReceipt receipt)
      {
        int? nullable = ((PXSelectBase<POReceivePutAwaySetup>) ((ScanComponent<ReceivePutAway>) this).Basis.Setup).Current.DefaultReceivingLocation.GetValueOrDefault() ? (int?) PX.Objects.IN.INSite.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), this.Body.SiteID)?.ReceiptLocationID : new int?();
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = this.Body.SiteID;
        ((ScanComponent<ReceivePutAway>) this).Basis.LocationID = ((ScanComponent<ReceivePutAway>) this).Basis.DefaultLocationID = nullable;
        base.Apply(receipt);
        this.Body.InitQuantityLimitationsPerInventoryItem(receipt);
      }

      protected override void ClearState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = new int?();
        ((ScanComponent<ReceivePutAway>) this).Basis.DefaultLocationID = new int?();
        base.ClearState();
      }

      protected override void ReportSuccess(PX.Objects.PO.POReceipt receipt)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo(this.Body.IsNewReceipt ? "New receipt created and ready to be processed." : "{0} receipt loaded and ready to be processed.", new object[1]
        {
          (object) receipt.ReceiptNbr
        });
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public bool IsNewReceipt { get; private set; }

        public int? SiteID { get; set; }

        public virtual 
        #nullable disable
        PX.Objects.PO.POReceipt GetReceiptByBarcode(string barcode)
        {
          return (PX.Objects.PO.POReceipt) PXSelectorAttribute.Select<WMSScanHeader.refNbr>(((PXSelectBase) this.Basis.HeaderView).Cache, (object) this.Basis.Header, (object) barcode);
        }

        public virtual bool TryGetReceiptByOrder(string barcode, out PX.Objects.PO.POReceipt receipt)
        {
          receipt = (PX.Objects.PO.POReceipt) null;
          PX.Objects.PO.POOrder order = PX.Objects.PO.POOrder.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), "RO", barcode);
          if (order == null)
            return false;
          if (string.IsNullOrEmpty(this.Basis.PONbr) || this.Basis.PONbr.Equals(order.OrderNbr, StringComparison.OrdinalIgnoreCase))
          {
            if (!string.IsNullOrEmpty(this.Basis.RefNbr))
            {
              PX.Objects.PO.POReceipt receipt1 = this.Basis.Receipt;
              if ((receipt1 != null ? (receipt1.Released.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                goto label_6;
            }
            else
              goto label_6;
          }
          this.Basis.ReceiptType = (string) null;
          this.Basis.RefNbr = (string) null;
          this.Basis.PONbr = (string) null;
          this.Basis.SiteID = new int?();
label_6:
          if (order.Status != "N")
          {
            this.Basis.ReportError("The {0} order cannot be received because it has the {1} status.", new object[2]
            {
              (object) order.OrderNbr,
              (object) this.Basis.SightOf<PX.Objects.PO.POOrder.status>((IBqlTable) order)
            });
            return false;
          }
          int? nullable1 = PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<PX.Objects.PO.POLine.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.orderType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POLine.orderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POLine>, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>.SameAsCurrent>, And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.kitItem, IBqlBool>.IsEqual<True>>>.Aggregate<PX.Data.BQL.Fluent.To<Count>>>.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POOrder[1]
          {
            order
          }, Array.Empty<object>()).RowCount;
          if (nullable1.GetValueOrDefault() != 0)
          {
            this.Basis.ReportError("The {0} order cannot be processed because it contains a non-stock kit item.", new object[1]
            {
              (object) order.OrderNbr
            });
            return false;
          }
          PXResultset<PX.Objects.PO.POLine> pxResultset = PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.orderType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POLine.orderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POLine>, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>.SameAsCurrent.And<BqlOperand<PX.Objects.PO.POLine.siteID, IBqlInt>.IsNotNull>>.Aggregate<PX.Data.BQL.Fluent.To<GroupBy<PX.Objects.PO.POLine.siteID>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POOrder[1]
          {
            order
          }, Array.Empty<object>());
          if (pxResultset.Count == 1)
          {
            this.SiteID = PXResultset<PX.Objects.PO.POLine>.op_Implicit(pxResultset).SiteID;
          }
          else
          {
            this.SiteID = this.Basis.SiteID;
            nullable1 = this.SiteID;
            if (!nullable1.HasValue)
            {
              ReceivePutAway basis = this.Basis;
              this.SiteID = nullable1 = this.Basis.DefaultSiteID;
              int? nullable2 = nullable1;
              basis.SiteID = nullable2;
            }
            nullable1 = this.SiteID;
            if (!nullable1.HasValue)
            {
              this.Basis.PONbr = order.OrderNbr;
              this.Basis.SetScanState<ReceivePutAway.ReceiveMode.SpecifyWarehouseState>((string) null, Array.Empty<object>());
              return true;
            }
          }
          receipt = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrderReceipt>.On<PX.Objects.PO.POOrderReceipt.FK.Receipt>>, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<PX.Objects.PO.POReceipt.FK.Vendor>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.PO.POOrderReceipt.pOType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POOrderReceipt.pONbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POOrderReceipt>, PX.Objects.PO.POOrder, PX.Objects.PO.POOrderReceipt>.SameAsCurrent>, And<BqlOperand<PX.Objects.PO.POReceipt.released, IBqlBool>.IsEqual<False>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AP.Vendor, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.wMSSingleOrder, Equal<False>>>>>.Or<BqlOperand<PX.Objects.PO.POReceipt.createdByID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.siteID, Equal<P.AsInt>>>>>.Or<Not<Exists<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<PX.Objects.PO.POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLineSplit>>, And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.siteID, IBqlInt>.IsNotNull>>>.And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.siteID, IBqlInt>.IsNotEqual<P.AsInt>>>>>>>>.Order<By<BqlField<PX.Objects.PO.POReceipt.wMSSingleOrder, IBqlBool>.Desc, BqlField<PX.Objects.PO.POReceipt.hold, IBqlBool>.Asc>>>.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POOrder[1]
          {
            order
          }, new object[2]
          {
            (object) this.SiteID,
            (object) this.SiteID
          }));
          if (receipt == null)
          {
            try
            {
              this.Basis.PONbr = order.OrderNbr;
              nullable1 = PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.orderType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POLine.orderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POLine>, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>.SameAsCurrent>.Aggregate<PX.Data.BQL.Fluent.To<Count>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POOrder[1]
              {
                order
              }, Array.Empty<object>()).RowCount;
              int valueOrDefault = nullable1.GetValueOrDefault();
              receipt = this.CreateNewReceiptForOrder(order, valueOrDefault > this.Basis.FindState<ReceivePutAway.ReceiveMode.ReceiptState>(false).LargeOrderLinesCount);
            }
            catch (Exception ex)
            {
              this.Basis.PONbr = (string) null;
              this.HandleReceiptCreationError(order, ex);
              return true;
            }
          }
          return true;
        }

        protected virtual PX.Objects.PO.POReceipt CreateNewReceiptForOrder(
          PX.Objects.PO.POOrder order,
          bool createEmpty)
        {
          PX.Objects.PO.POReceipt poReceipt = createEmpty ? ((PXGraphExtension<ReceivePutAway.Host>) this).Base.CreateEmptyReceiptFrom(order) : this.Basis.Graph.CreateReceiptFrom(order);
          poReceipt.OrigPONbr = order.OrderNbr;
          poReceipt.WMSSingleOrder = new bool?(true);
          poReceipt.SiteID = this.SiteID;
          poReceipt.AutoCreateInvoice = new bool?(false);
          poReceipt.Hold = new bool?(true);
          poReceipt.Status = "H";
          PX.Objects.PO.POReceipt newReceiptForOrder = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Current = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Update(poReceipt);
          foreach (PX.Objects.PO.POReceiptLineSplit receiptLineSplit in ((PXSelectBase) this.Basis.Graph.splits).Cache.Inserted)
          {
            INLotSerClass lotSerialClassOf = this.Basis.GetLotSerialClassOf(PX.Objects.IN.InventoryItem.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), receiptLineSplit.InventoryID));
            if (lotSerialClassOf != null && EnumerableExtensions.IsIn<string>(lotSerialClassOf.LotSerTrack, "L", "S"))
            {
              bool? nullable1 = lotSerialClassOf.LotSerTrackExpiration;
              if (nullable1.GetValueOrDefault())
              {
                nullable1 = lotSerialClassOf.AutoNextNbr;
                if (nullable1.GetValueOrDefault())
                {
                  DateTime? expireDate = receiptLineSplit.ExpireDate;
                  if (!expireDate.HasValue)
                  {
                    expireDate = (DateTime?) GraphHelper.RowCast<INItemLotSerial>((IEnumerable) PXSelectBase<INItemLotSerial, PXViewOf<INItemLotSerial>.BasedOn<SelectFromBase<INItemLotSerial, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemLotSerial.inventoryID, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.inventoryID, IBqlInt>.FromCurrent>>.Order<By<Desc<BqlOperand<True, IBqlBool>.When<BqlOperand<INItemLotSerial.lotSerialNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.lotSerialNbr, IBqlString>.FromCurrent>>.Else<False>>, Desc<INItemLotSerial.expireDate>>>>.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceiptLineSplit[1]
                    {
                      receiptLineSplit
                    }, Array.Empty<object>())).FirstOrDefault<INItemLotSerial>()?.ExpireDate;
                    DateTime? nullable2 = expireDate ?? ((PXGraph) this.Basis.Graph).Accessinfo.BusinessDate;
                    ((PXSelectBase) this.Basis.Graph.splits).Cache.SetValueExt<PX.Objects.PO.POReceiptLineSplit.expireDate>((object) receiptLineSplit, (object) nullable2);
                  }
                }
              }
            }
            receiptLineSplit.Qty = new Decimal?(0M);
            ((PXSelectBase) this.Basis.Graph.splits).Cache.Update((object) receiptLineSplit);
          }
          this.Basis.SaveChanges();
          this.IsNewReceipt = true;
          return newReceiptForOrder;
        }

        protected virtual void HandleReceiptCreationError(PX.Objects.PO.POOrder order, Exception exception)
        {
          PXTrace.WriteError(exception);
          string str = this.Basis.Localize("The system could not create a purchase receipt for the {0} purchase order. Create it manually.", new object[1]
          {
            (object) order.OrderNbr
          }) + Environment.NewLine + exception.Message;
          if (exception is PXOuterException pxOuterException)
          {
            if (pxOuterException.InnerMessages.Length != 0)
              str = str + Environment.NewLine + string.Join(Environment.NewLine, pxOuterException.InnerMessages);
            else if (pxOuterException.Row != null)
              str = str + Environment.NewLine + string.Join(Environment.NewLine, PXUIFieldAttribute.GetErrors(((PXGraph) this.Basis.Graph).Caches[pxOuterException.Row.GetType()], pxOuterException.Row, Array.Empty<PXErrorLevel>()).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (kvp => kvp.Value)));
          }
          ((PXGraph) this.Basis.Graph).Clear();
          this.Basis.ReportError(str, Array.Empty<object>());
        }

        [PXOverride]
        public ScanState<ReceivePutAway> DecorateScanState(
          ScanState<ReceivePutAway> original,
          Func<ScanState<ReceivePutAway>, ScanState<ReceivePutAway>> base_DecorateScanState)
        {
          ScanState<ReceivePutAway> scanState = base_DecorateScanState(original);
          if (scanState is ReceivePutAway.ReceiveMode.ReceiptState receiptState)
          {
            this.InjectReceiptFetchByOrder(receiptState);
            this.InjectReceiptTrySwitchToTransferMode(receiptState);
            this.InjectReceiptTrySwitchToReturnMode(receiptState);
          }
          if (scanState is WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState itemState && ((ScanComponent<ReceivePutAway>) scanState).ModeCode == "RCPT")
            this.InjectInventoryItemValidateOverreceiving(itemState);
          if (scanState is WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState lotSerialState && ((ScanComponent<ReceivePutAway>) scanState).ModeCode == "RCPT")
            this.InjectLotSerialValidateOverreceiving(lotSerialState);
          return scanState;
        }

        public virtual void InjectReceiptFetchByOrder(
          ReceivePutAway.ReceiveMode.ReceiptState receiptState)
        {
          bool isSecondTry = false;
          ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, PX.Objects.PO.POReceipt>) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) receiptState).Intercept.GetByBarcode).ByOverride((Func<PX.Objects.PO.POReceipt, string, Func<string, PX.Objects.PO.POReceipt>, PX.Objects.PO.POReceipt>) ((basis, barcode, base_GetReceiptByBarcode) => !isSecondTry && receiptState.OrderComesFirst ? (PX.Objects.PO.POReceipt) null : base_GetReceiptByBarcode(barcode)), new RelativeInject?())).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) =>
          {
            if (isSecondTry)
              return AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Skipped);
            PX.Objects.PO.POReceipt receipt;
            if (basis.Get<ReceivePutAway.ReceiveMode.ReceiptState.Logic>().TryGetReceiptByOrder(barcode, out receipt))
              return AbsenceHandling.ReplaceWith<PX.Objects.PO.POReceipt>(receipt);
            if (receiptState.OrderComesFirst)
            {
              isSecondTry = true;
              try
              {
                if (basis.TryProcessBy<ReceivePutAway.ReceiveMode.ReceiptState>(barcode, (StateSubstitutionRule) (int) byte.MaxValue))
                  return AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Done);
              }
              finally
              {
                isSecondTry = false;
              }
            }
            return AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Skipped);
          }), new RelativeInject?());
        }

        public virtual void InjectReceiptTrySwitchToTransferMode(
          ReceivePutAway.ReceiveMode.ReceiptState receipt)
        {
          ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) receipt).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) => AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(basis.ProcessByMode<ReceivePutAway.ReceiveTransferMode>(barcode))), new RelativeInject?());
        }

        public virtual void InjectReceiptTrySwitchToReturnMode(
          ReceivePutAway.ReceiveMode.ReceiptState receipt)
        {
          ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) receipt).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) => AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(basis.ProcessByMode<ReceivePutAway.ReturnMode>(barcode))), new RelativeInject?());
        }

        public virtual void InjectInventoryItemValidateOverreceiving(
          WarehouseManagementSystem<
          #nullable enable
          ReceivePutAway, ReceivePutAway.Host>.InventoryItemState itemState)
        {
          ((MethodInterceptor<EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, ReceivePutAway>.OfFunc<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>.AsAppendable) ((EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) itemState).Intercept.Validate).ByAppend((Func<Validation, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>) ((basis, item) =>
          {
            PX.Objects.IN.InventoryItem inventoryItem = PXResult<INItemXRef, PX.Objects.IN.InventoryItem>.op_Implicit(item);
            if (!inventoryItem.InventoryID.HasValue || basis.RPAHeader.LimitedInventoryIds == null || basis.Remove.GetValueOrDefault())
              return Validation.Ok;
            Dictionary<int, Decimal?> limitedInventoryIds = basis.RPAHeader.LimitedInventoryIds;
            Decimal? nullable1;
            // ISSUE: explicit non-virtual call
            if ((limitedInventoryIds != null ? (__nonvirtual (limitedInventoryIds.TryGetValue(inventoryItem.InventoryID.Value, out nullable1)) ? 1 : 0) : 0) != 0 && nullable1.HasValue)
            {
              Decimal? nullable2 = nullable1;
              Decimal num = 0M;
              if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
                return Validation.Fail("The quantity on the receipt will exceed the maximum quantity specified for this item in the line of the purchase order.", Array.Empty<object>());
            }
            return Validation.Ok;
          }), new RelativeInject?());
        }

        public virtual void InjectLotSerialValidateOverreceiving(
          #nullable disable
          WarehouseManagementSystem<
          #nullable enable
          ReceivePutAway, ReceivePutAway.Host>.LotSerialState lotSerialState)
        {
          ((MethodInterceptor<EntityState<ReceivePutAway, string>, ReceivePutAway>.OfFunc<string, Validation>.AsAppendable) ((EntityState<ReceivePutAway, string>) lotSerialState).Intercept.Validate).ByAppend((Func<Validation, string, Validation>) ((basis, lotSerial) =>
          {
            if (!basis.InventoryID.HasValue || basis.RPAHeader.LimitedInventoryIds == null || basis.Remove.GetValueOrDefault())
              return Validation.Ok;
            Dictionary<int, Decimal?> limitedInventoryIds = basis.RPAHeader.LimitedInventoryIds;
            Decimal? nullable1;
            // ISSUE: explicit non-virtual call
            if ((limitedInventoryIds != null ? (__nonvirtual (limitedInventoryIds.TryGetValue(basis.InventoryID.Value, out nullable1)) ? 1 : 0) : 0) != 0 && nullable1.HasValue)
            {
              Decimal? nullable2 = nullable1;
              Decimal num = 0M;
              if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
                return Validation.Fail("The quantity on the receipt will exceed the maximum quantity specified for this item in the line of the purchase order.", Array.Empty<object>());
            }
            return Validation.Ok;
          }), new RelativeInject?());
        }

        public virtual void InitQuantityLimitationsPerInventoryItem(
        #nullable disable
        PX.Objects.PO.POReceipt receipt)
        {
          FbqlSelect<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.PO.POLine.inventoryID, IBqlInt>.IsNotNull>.Aggregate<To<GroupBy<PX.Objects.PO.POLine.inventoryID>, Count<PX.Objects.PO.POLine.rcptQtyAction>, Max<PX.Objects.PO.POLine.rcptQtyAction>>>.Having<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Count<PX.Objects.PO.POLine.rcptQtyAction>>, Equal<FunctionWrapper<One>>>>>>.And<BqlAggregatedOperand<Max<PX.Objects.PO.POLine.rcptQtyAction>, IBqlString>.IsEqual<POReceiptQtyAction.reject>>>, PX.Objects.PO.POLine>.View view = new FbqlSelect<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.PO.POLine.inventoryID, IBqlInt>.IsNotNull>.Aggregate<To<GroupBy<PX.Objects.PO.POLine.inventoryID>, Count<PX.Objects.PO.POLine.rcptQtyAction>, Max<PX.Objects.PO.POLine.rcptQtyAction>>>.Having<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Count<PX.Objects.PO.POLine.rcptQtyAction>>, Equal<FunctionWrapper<One>>>>>>.And<BqlAggregatedOperand<Max<PX.Objects.PO.POLine.rcptQtyAction>, IBqlString>.IsEqual<POReceiptQtyAction.reject>>>, PX.Objects.PO.POLine>.View(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis));
          object[] objArray;
          if (receipt.WMSSingleOrder.GetValueOrDefault())
          {
            ((PXSelectBase<PX.Objects.PO.POLine>) view).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderNbr, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POLine.orderType, IBqlString>.IsEqual<P.AsString>>>>();
            objArray = new object[2]
            {
              (object) receipt.OrigPONbr,
              (object) receipt.POType
            };
          }
          else
          {
            ((PXSelectBase<PX.Objects.PO.POLine>) view).Join<InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.FK.OrderLine>>>();
            ((PXSelectBase<PX.Objects.PO.POLine>) view).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.IsEqual<P.AsString.ASCII>>>>();
            objArray = new object[2]
            {
              (object) receipt.ReceiptNbr,
              (object) receipt.ReceiptType
            };
          }
          this.Basis.RPAHeader.LimitedInventoryIds = ((IEnumerable<PXResult<PX.Objects.PO.POLine>>) ((PXSelectBase<PX.Objects.PO.POLine>) view).Select(objArray)).ToDictionary<PXResult<PX.Objects.PO.POLine>, int, Decimal?>((Func<PXResult<PX.Objects.PO.POLine>, int>) (res => PXResult<PX.Objects.PO.POLine>.op_Implicit(res).InventoryID.Value), (Func<PXResult<PX.Objects.PO.POLine>, Decimal?>) (_ => new Decimal?()));
        }
      }

      [PXLocalizable]
      public new abstract class Msg : ReceivePutAway.ReceiptState.Msg
      {
        public const string ReadyNew = "New receipt created and ready to be processed.";
        public const string POOrderInvalid = "The {0} order cannot be received because it has the {1} status.";
        public const string POOrderMultiSites = "All items in the {0} order must be located in the same warehouse.";
        public const string POOrderHasNonStocKit = "The {0} order cannot be processed because it contains a non-stock kit item.";
        public const string POOrderUnableToCreateReceipt = "The system could not create a purchase receipt for the {0} purchase order. Create it manually.";
      }
    }

    public sealed class SpecifyWarehouseState : 
      WarehouseManagementSystem<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.WarehouseState
    {
      protected override bool UseDefaultWarehouse => false;

      protected virtual void SetNextState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.SetScanState<ReceivePutAway.ReceiveMode.ReceiptState>((string) null, Array.Empty<object>());
        if (!(((ScanComponent<ReceivePutAway>) this).Basis.CurrentState.Code == "RNBR"))
          return;
        ((ScanComponent<ReceivePutAway>) this).Basis.CurrentState.Process(((ScanComponent<ReceivePutAway>) this).Basis.PONbr);
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ConfirmationState
    {
      public virtual 
      #nullable disable
      string Prompt
      {
        get
        {
          return ((ScanComponent<ReceivePutAway>) this).Basis.Localize("Confirm receiving {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.Qty,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<ReceivePutAway.ReceiveMode.ConfirmState.Logic>().Confirm();
      }

      public class OverreceiveQuestion : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanQuestion
      {
        public virtual 
        #nullable disable
        string Code => "OVERRECEIVE";

        protected virtual string GetPrompt()
        {
          return ((ScanComponent<ReceivePutAway>) this).Basis.Localize("The following quantity of the item will be added to a receipt line that is not linked to a purchase order: {0} {1}. To confirm, click OK.", new object[2]
          {
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.BaseExcessQty,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SelectedInventoryItem.BaseUnit
          });
        }

        protected virtual void Confirm()
        {
          ((ScanComponent<ReceivePutAway>) this).Basis.ForceInsertLine = new bool?(true);
          if (!(((ScanComponent<ReceivePutAway>) this).Basis.CurrentState is ReceivePutAway.ReceiveMode.ConfirmState currentState))
            return;
          ((ConfirmationState<ReceivePutAway>) currentState).Confirm();
        }

        protected virtual void Reject()
        {
        }

        [PXLocalizable]
        public abstract class Msg
        {
          public const string Prompt = "The following quantity of the item will be added to a receipt line that is not linked to a purchase order: {0} {1}. To confirm, click OK.";
        }
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public 
        #nullable disable
        ReceivePutAway.ReceiveMode.Logic Mode { get; private set; }

        public virtual void Initialize()
        {
          this.Mode = this.Basis.Get<ReceivePutAway.ReceiveMode.Logic>();
        }

        public virtual FlowStatus Confirm()
        {
          FlowStatus error;
          if (!this.CanReceive(out error))
            return error;
          bool? remove = this.Basis.Remove;
          bool flag = false;
          return remove.GetValueOrDefault() == flag & remove.HasValue ? this.ProcessAdd() : this.ProcessRemove();
        }

        protected virtual bool CanReceive(out FlowStatus error)
        {
          if (!this.Basis.InventoryID.HasValue)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Item not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          if (this.Mode.IsLocationUserInputRequired && !this.Basis.LocationID.HasValue)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Location not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          if (this.Mode.IsLotSerialUserInputRequired && this.Basis.LotSerialNbr == null)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          error = FlowStatus.Ok;
          return true;
        }

        protected virtual FlowStatus ProcessAdd()
        {
          Decimal baseQty = this.Basis.BaseQty;
          if (this.Mode.IsLotSerialUserInputRequired && this.Basis.ReceiveBySingleItem && baseQty != 1M)
            return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState>() && !this.Mode.UseDefaultExpireDate && !this.Basis.ExpireDate.HasValue)
          {
            FlowStatus flowStatus = FlowStatus.Fail("Expiration date not selected.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          int? nullable1 = this.Basis.LocationID;
          if (!nullable1.HasValue)
          {
            nullable1 = this.Basis.DefaultLocationID;
            if (nullable1.HasValue)
              this.Basis.LocationID = this.Basis.DefaultLocationID;
          }
          if (!this.Basis.EnsureLocationPrimaryItem(this.Basis.InventoryID, this.Basis.LocationID))
            return FlowStatus.Fail("Selected item is not allowed in this location.", Array.Empty<object>());
          IEnumerable<PX.Objects.PO.POReceiptLineSplit> source = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            int? inventoryId1 = s.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
              return false;
            int? subItemId1 = s.SubItemID;
            int? subItemId2 = this.Basis.SubItemID;
            return subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue;
          }));
          bool? nullable2 = this.Basis.Receipt.WMSSingleOrder;
          if (nullable2.GetValueOrDefault() && this.Basis.Receipt.OrigPONbr != null && !source.Any<PX.Objects.PO.POReceiptLineSplit>())
          {
            this.Basis.Graph.AddPurchaseOrder(PX.Objects.PO.POOrder.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), "RO", this.Basis.Receipt.OrigPONbr), this.Basis.InventoryID, this.Basis.SubItemID);
            source = ((PXSelectBase) this.Mode.Received).Cache.Inserted.Cast<PX.Objects.PO.POReceiptLineSplit>();
          }
          IEnumerable<PX.Objects.PO.POReceiptLineSplit> array = (IEnumerable<PX.Objects.PO.POReceiptLineSplit>) source.OrderByDescending<PX.Objects.PO.POReceiptLineSplit, bool>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s => string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByDescending<PX.Objects.PO.POReceiptLineSplit, bool>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            int? locationId = s.LocationID;
            int? nullable3 = this.Basis.LocationID ?? s.LocationID;
            return locationId.GetValueOrDefault() == nullable3.GetValueOrDefault() & locationId.HasValue == nullable3.HasValue;
          })).ToArray<PX.Objects.PO.POReceiptLineSplit>();
          if (this.Mode.IsLotSerialUserInputRequired && this.Basis.ReceiveBySingleItem && array.Any<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            if (!string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              return false;
            Decimal? receivedQty = s.ReceivedQty;
            Decimal num = (Decimal) 1;
            return receivedQty.GetValueOrDefault() == num & receivedQty.HasValue;
          })))
          {
            FlowStatus flowStatus = FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          ReceivePutAway.ReceiveMode.ConfirmState.Logic.QuantitySpreader quantitySpreader = this.GetQuantitySpreader(baseQty, array);
          if (quantitySpreader.NewLineIsNeeded && quantitySpreader.ForbidOverreceiving)
          {
            if (quantitySpreader.AdmittedQty == 0M)
              return FlowStatus.Fail("The quantity on the receipt will exceed the maximum quantity specified for this item in the line of the purchase order.", Array.Empty<object>());
            return FlowStatus.Fail("The quantity on the receipt will exceed the maximum quantity specified for this item in the line of the purchase order. The remaining quantity to be received is {0}.", new object[1]
            {
              (object) quantitySpreader.AdmittedQty
            });
          }
          ReceivePutAway basis = this.Basis;
          nullable2 = this.Basis.ForceInsertLine;
          int? inventoryId;
          int num1;
          if (nullable2.GetValueOrDefault())
          {
            nullable1 = this.Basis.PrevInventoryID;
            inventoryId = this.Basis.InventoryID;
            num1 = nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue ? 1 : 0;
          }
          else
            num1 = 0;
          bool? nullable4 = new bool?(num1 != 0);
          basis.ForceInsertLine = nullable4;
          if (quantitySpreader.NewLineIsNeeded)
          {
            nullable2 = this.Basis.ForceInsertLine;
            bool flag = false;
            if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
            {
              this.Basis.BaseExcessQty = new Decimal?(quantitySpreader.BaseExcessQty);
              this.Basis.PrevInventoryID = this.Basis.InventoryID;
              return FlowStatus.Warn<ReceivePutAway.ReceiveMode.ConfirmState.OverreceiveQuestion>("The quantity of the {1} item exceeds the item's quantity in the {0} purchase receipt.", new object[2]
              {
                (object) this.Basis.RefNbr,
                (object) this.Basis.SelectedInventoryItem.InventoryCD
              });
            }
          }
          quantitySpreader.Spread();
          this.Basis.PrevInventoryID = this.Basis.InventoryID;
          this.Basis.BaseExcessQty = new Decimal?(0M);
          this.Basis.ReportInfo("{0} x {1} {2} added to receipt.", new object[3]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          if (quantitySpreader.ForbidOverreceiving)
          {
            inventoryId = this.Basis.InventoryID;
            if (inventoryId.HasValue)
            {
              Dictionary<int, Decimal?> limitedInventoryIds = this.Basis.RPAHeader.LimitedInventoryIds;
              inventoryId = this.Basis.InventoryID;
              int key = inventoryId.Value;
              Decimal? nullable5;
              ref Decimal? local = ref nullable5;
              if (limitedInventoryIds.TryGetValue(key, out local))
              {
                Decimal? nullable6 = nullable5;
                Decimal num2 = 0M;
                if (nullable6.GetValueOrDefault() == num2 & nullable6.HasValue)
                  this.Basis.Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>(true);
              }
            }
          }
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }

        protected virtual FlowStatus ProcessRemove()
        {
          if (!this.Basis.LocationID.HasValue && this.Basis.DefaultLocationID.HasValue)
            this.Basis.LocationID = this.Basis.DefaultLocationID;
          PX.Objects.PO.POReceiptLineSplit[] array1 = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
          {
            int? inventoryId1 = r.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
              return false;
            int? subItemId1 = r.SubItemID;
            int? subItemId2 = this.Basis.SubItemID;
            return subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue;
          })).ToArray<PX.Objects.PO.POReceiptLineSplit>();
          // ISSUE: method pointer
          PX.Objects.PO.POReceiptLineSplit[] array2 = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array1).Where<PX.Objects.PO.POReceiptLineSplit>(new Func<PX.Objects.PO.POReceiptLineSplit, bool>((object) this, __methodptr(\u003CProcessRemove\u003Eg__IsDeductable\u007C8_1))).ToArray<PX.Objects.PO.POReceiptLineSplit>();
          if (!((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array2).Any<PX.Objects.PO.POReceiptLineSplit>())
          {
            FlowStatus flowStatus = FlowStatus.Fail("No items to remove from receipt.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          Decimal baseQty = this.Basis.BaseQty;
          if (((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array2).Sum<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, Decimal>) (s => s.ReceivedQty.Value)) - baseQty < 0M)
            return FlowStatus.Fail("The received quantity cannot be negative.", Array.Empty<object>());
          Decimal deltaValue = 0M;
          if (this.Basis.ReceiveBySingleItem)
          {
            PX.Objects.PO.POReceiptLineSplit s1 = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array2).Reverse<PX.Objects.PO.POReceiptLineSplit>().First<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
            {
              Decimal? receivedQty = s.ReceivedQty;
              Decimal num = 0M;
              return !(receivedQty.GetValueOrDefault() == num & receivedQty.HasValue);
            }));
            if (this.Mode.IsUnboundSplit(s1))
            {
              ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Delete(s1);
            }
            else
            {
              s1.ReceivedQty = new Decimal?(0M);
              ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(s1);
            }
            deltaValue += 1M;
          }
          else
          {
            Decimal val2_1 = baseQty;
            // ISSUE: method pointer
            foreach (KeyValuePair<int?, PX.Objects.PO.POReceiptLineSplit[]> keyValuePair in (IEnumerable<KeyValuePair<int?, PX.Objects.PO.POReceiptLineSplit[]>>) ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array1).Reverse<PX.Objects.PO.POReceiptLineSplit>().GroupBy<PX.Objects.PO.POReceiptLineSplit, int?>((Func<PX.Objects.PO.POReceiptLineSplit, int?>) (s => s.LineNbr)).ToDictionary<IGrouping<int?, PX.Objects.PO.POReceiptLineSplit>, int?, PX.Objects.PO.POReceiptLineSplit[]>((Func<IGrouping<int?, PX.Objects.PO.POReceiptLineSplit>, int?>) (g => g.Key), (Func<IGrouping<int?, PX.Objects.PO.POReceiptLineSplit>, PX.Objects.PO.POReceiptLineSplit[]>) (g => g.OrderByDescending<PX.Objects.PO.POReceiptLineSplit, bool>(new Func<PX.Objects.PO.POReceiptLineSplit, bool>((object) this, __methodptr(\u003CProcessRemove\u003Eg__IsDeductable\u007C8_1))).ToArray<PX.Objects.PO.POReceiptLineSplit>())).OrderByDescending<KeyValuePair<int?, PX.Objects.PO.POReceiptLineSplit[]>, int?>((Func<KeyValuePair<int?, PX.Objects.PO.POReceiptLineSplit[]>, int?>) (kvp => kvp.Key)))
            {
              // ISSUE: method pointer
              Decimal num1 = Math.Min(((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) keyValuePair.Value).Where<PX.Objects.PO.POReceiptLineSplit>(new Func<PX.Objects.PO.POReceiptLineSplit, bool>((object) this, __methodptr(\u003CProcessRemove\u003Eg__IsDeductable\u007C8_1))).Sum<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, Decimal>) (s => s.ReceivedQty.Value)), val2_1);
              if (!(num1 == 0M))
              {
                HashSet<int> removedSplits = new HashSet<int>();
                bool flag = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) keyValuePair.Value).Any<PX.Objects.PO.POReceiptLineSplit>(new Func<PX.Objects.PO.POReceiptLineSplit, bool>(this.Mode.IsUnboundSplit));
                Decimal val2_2 = num1;
                Decimal num2 = 0M;
                Decimal? nullable;
                for (int index = 0; index < keyValuePair.Value.Length; ++index)
                {
                  PX.Objects.PO.POReceiptLineSplit receiptLineSplit1 = keyValuePair.Value[index];
                  if (IsDeductable(receiptLineSplit1))
                  {
                    nullable = receiptLineSplit1.ReceivedQty;
                    Decimal num3 = Math.Min(nullable.Value, val2_2);
                    if (index == keyValuePair.Value.LastIndex<PX.Objects.PO.POReceiptLineSplit>() && !this.Mode.IsUnboundSplit(receiptLineSplit1))
                    {
                      PX.Objects.PO.POReceiptLineSplit receiptLineSplit2 = receiptLineSplit1;
                      nullable = receiptLineSplit2.Qty;
                      Decimal num4 = num2;
                      receiptLineSplit2.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num4) : new Decimal?();
                      PX.Objects.PO.POReceiptLineSplit receiptLineSplit3 = receiptLineSplit1;
                      nullable = receiptLineSplit3.ReceivedQty;
                      Decimal num5 = num3;
                      receiptLineSplit3.ReceivedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num5) : new Decimal?();
                      ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(receiptLineSplit1);
                      flag = true;
                      deltaValue += num3;
                      break;
                    }
                    Decimal num6 = num3;
                    nullable = receiptLineSplit1.ReceivedQty;
                    Decimal valueOrDefault = nullable.GetValueOrDefault();
                    if (num6 < valueOrDefault & nullable.HasValue)
                    {
                      PX.Objects.PO.POReceiptLineSplit receiptLineSplit4 = receiptLineSplit1;
                      nullable = receiptLineSplit4.ReceivedQty;
                      Decimal num7 = num3;
                      receiptLineSplit4.ReceivedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num7) : new Decimal?();
                      ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(receiptLineSplit1);
                      deltaValue += num3;
                    }
                    else if (this.Mode.IsUnboundSplit(receiptLineSplit1))
                    {
                      ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Basis.Graph.transactions).Delete(PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Basis.Graph.transactions).Search<PX.Objects.PO.POReceiptLine.lineNbr>((object) receiptLineSplit1.LineNbr, Array.Empty<object>())));
                      Decimal num8 = deltaValue;
                      nullable = receiptLineSplit1.ReceivedQty;
                      Decimal num9 = nullable.Value;
                      deltaValue = num8 + num9;
                    }
                    else
                    {
                      removedSplits.Add(receiptLineSplit1.SplitLineNbr.Value);
                      Decimal num10 = num2;
                      nullable = receiptLineSplit1.Qty;
                      Decimal num11 = nullable.Value;
                      num2 = num10 + num11;
                      ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Delete(receiptLineSplit1);
                      Decimal num12 = deltaValue;
                      nullable = receiptLineSplit1.ReceivedQty;
                      Decimal num13 = nullable.Value;
                      deltaValue = num12 + num13;
                    }
                    val2_2 -= num3;
                    if (val2_2 == 0M)
                      break;
                  }
                  else
                    break;
                }
                if (!flag)
                {
                  PX.Objects.PO.POReceiptLineSplit receiptLineSplit5 = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) keyValuePair.Value).FirstOrDefault<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s => !IsDeductable(s))) ?? ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) keyValuePair.Value).First<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s => EnumerableExtensions.IsNotIn<int>(s.SplitLineNbr.Value, (IEnumerable<int>) removedSplits)));
                  PX.Objects.PO.POReceiptLineSplit receiptLineSplit6 = receiptLineSplit5;
                  nullable = receiptLineSplit6.Qty;
                  Decimal num14 = num2;
                  receiptLineSplit6.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num14) : new Decimal?();
                  ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(receiptLineSplit5);
                }
                val2_1 -= num1;
                if (val2_1 == 0M)
                  break;
              }
            }
          }
          if (this.Basis.Get<ReceivePutAway.ReceiveMode.ConfirmState.Logic>().IsCurrentItemQuantityLimited())
            this.Basis.Get<ReceivePutAway.ReceiveMode.ConfirmState.Logic>().AdjustQuantityLimitFor(deltaValue, this.Basis.InventoryID.Value);
          this.Basis.ReportInfo("{0} x {1} {2} removed from receipt.", new object[3]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;

          bool IsDeductable(PX.Objects.PO.POReceiptLineSplit r)
          {
            if (!string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              return false;
            int? locationId = r.LocationID;
            int? nullable = this.Basis.LocationID ?? r.LocationID;
            return locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue;
          }
        }

        protected virtual ReceivePutAway.ReceiveMode.ConfirmState.Logic.QuantitySpreader GetQuantitySpreader(
          Decimal qty,
          IEnumerable<PX.Objects.PO.POReceiptLineSplit> itemSplits)
        {
          ReceivePutAway.ReceiveMode.ConfirmState.Logic.QuantitySpreader quantitySpreader = this.Basis.Get<ReceivePutAway.ReceiveMode.ConfirmState.Logic.QuantitySpreader>();
          quantitySpreader.Prepare(qty, itemSplits);
          return quantitySpreader;
        }

        protected virtual void AdjustQuantityLimitFor(Decimal deltaValue, int inventoryID)
        {
          Dictionary<int, Decimal?> limitedInventoryIds1 = this.Basis.RPAHeader.LimitedInventoryIds;
          int key1 = inventoryID;
          Decimal? nullable1 = limitedInventoryIds1[key1];
          nullable1.GetValueOrDefault();
          Decimal? nullable2;
          if (!nullable1.HasValue)
          {
            Decimal num = 0M;
            Dictionary<int, Decimal?> dictionary = limitedInventoryIds1;
            int key2 = key1;
            nullable2 = new Decimal?(num);
            Decimal? nullable3 = nullable2;
            dictionary[key2] = nullable3;
          }
          Dictionary<int, Decimal?> limitedInventoryIds2 = this.Basis.RPAHeader.LimitedInventoryIds;
          int key3 = inventoryID;
          Dictionary<int, Decimal?> dictionary1 = limitedInventoryIds2;
          int key4 = key3;
          Decimal? nullable4 = limitedInventoryIds2[key3];
          Decimal num1 = deltaValue;
          Decimal? nullable5;
          if (!nullable4.HasValue)
          {
            nullable2 = new Decimal?();
            nullable5 = nullable2;
          }
          else
            nullable5 = new Decimal?(nullable4.GetValueOrDefault() + num1);
          dictionary1[key4] = nullable5;
        }

        protected virtual bool IsCurrentItemQuantityLimited()
        {
          if (this.Basis.RPAHeader.LimitedInventoryIds != null)
          {
            int? inventoryId = this.Basis.InventoryID;
            if (inventoryId.HasValue)
            {
              Dictionary<int, Decimal?> limitedInventoryIds = this.Basis.RPAHeader.LimitedInventoryIds;
              inventoryId = this.Basis.InventoryID;
              int key = inventoryId.Value;
              return limitedInventoryIds.ContainsKey(key);
            }
          }
          return false;
        }

        public class QuantitySpreader : 
          BarcodeDrivenStateMachine<
          #nullable enable
          ReceivePutAway, ReceivePutAway.Host>.ScanExtension
        {
          protected 
          #nullable disable
          ReceivePutAway.ReceiveMode.Logic Mode { get; set; }

          protected Decimal RestQty { get; set; }

          protected IEnumerable<PX.Objects.PO.POReceiptLineSplit> ExactSplits { get; set; }

          protected IEnumerable<PX.Objects.PO.POReceiptLineSplit> DonorSplits { get; set; }

          protected IEnumerable<PX.Objects.PO.POReceiptLineSplit> UnassignedSplits { get; set; }

          protected List<PX.Objects.PO.POReceiptLineSplit> AcceptorSplits { get; set; }

          public bool NewLineIsNeeded => this.BaseExcessQty > 0M;

          public Decimal BaseExcessQty { get; protected set; }

          public Decimal AdmittedQty { get; protected set; }

          public bool ForbidOverreceiving { get; protected set; }

          protected Decimal GetExtendedRestQty(PX.Objects.PO.POReceiptLineSplit split)
          {
            return this.Mode.GetExtendedRestQty(split);
          }

          public virtual void Prepare(
            Decimal qtyToSpread,
            IEnumerable<PX.Objects.PO.POReceiptLineSplit> itemSplits)
          {
            this.Mode = this.Basis.Get<ReceivePutAway.ReceiveMode.Logic>();
            this.RestQty = qtyToSpread;
            (this.UnassignedSplits, itemSplits) = EnumerableExtensions.DisuniteBy<PX.Objects.PO.POReceiptLineSplit>(itemSplits, (Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s => s.IsUnassigned.GetValueOrDefault()));
            this.DonorSplits = (IEnumerable<PX.Objects.PO.POReceiptLineSplit>) itemSplits.Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
            {
              if (!this.Mode.IsUnboundSplit(s))
              {
                Decimal? qty = s.Qty;
                Decimal? receivedQty = s.ReceivedQty;
                Decimal? nullable = qty.HasValue & receivedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - receivedQty.GetValueOrDefault()) : new Decimal?();
                Decimal num = 0M;
                if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
                  return this.GetExtendedRestQty(s) > 0M;
              }
              return true;
            })).ToArray<PX.Objects.PO.POReceiptLineSplit>();
            bool inputLocation = this.Mode.IsLocationUserInputRequired;
            bool inputLotSerial = this.Mode.IsLotSerialUserInputRequired;
            this.ExactSplits = (IEnumerable<PX.Objects.PO.POReceiptLineSplit>) this.DonorSplits.Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
            {
              int num1 = inputLocation ? 1 : 0;
              int? locationId1 = s.LocationID;
              int? locationId2 = this.Basis.LocationID;
              Decimal? receivedQty;
              int num2;
              if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
              {
                receivedQty = s.ReceivedQty;
                Decimal num3 = 0M;
                num2 = receivedQty.GetValueOrDefault() == num3 & receivedQty.HasValue ? 1 : 0;
              }
              else
                num2 = 1;
              if (!(num1 != 0).Implies(num2 != 0))
                return false;
              int num4 = inputLotSerial ? 1 : 0;
              int num5;
              if (!string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              {
                receivedQty = s.ReceivedQty;
                Decimal num6 = 0M;
                num5 = receivedQty.GetValueOrDefault() == num6 & receivedQty.HasValue ? 1 : 0;
              }
              else
                num5 = 1;
              return (num4 != 0).Implies(num5 != 0);
            })).ToArray<PX.Objects.PO.POReceiptLineSplit>();
            this.AcceptorSplits = new List<PX.Objects.PO.POReceiptLineSplit>();
            IEnumerable<IGrouping<\u003C\u003Ef__AnonymousType97<int?, string>, PX.Objects.PO.POReceiptLineSplit>> groupings = this.DonorSplits.Concat<PX.Objects.PO.POReceiptLineSplit>(this.UnassignedSplits).GroupBy(s => new
            {
              LineNbr = s.LineNbr,
              PONbr = s.PONbr
            });
            Decimal num7 = 0M;
            this.AdmittedQty = num7;
            Decimal num8 = num7;
            this.ForbidOverreceiving = this.Basis.Get<ReceivePutAway.ReceiveMode.ConfirmState.Logic>().IsCurrentItemQuantityLimited();
            foreach (IEnumerable<PX.Objects.PO.POReceiptLineSplit> source in groupings)
            {
              PX.Objects.PO.POReceiptLineSplit split = source.First<PX.Objects.PO.POReceiptLineSplit>();
              num8 += this.Mode.GetExtendedRestQty(split, true);
              this.AdmittedQty = this.ForbidOverreceiving ? this.Mode.GetExtendedRestQty(split, false) : this.AdmittedQty;
            }
            this.BaseExcessQty = Math.Max(0M, this.RestQty - num8);
            if (!this.ForbidOverreceiving)
              return;
            this.Basis.RPAHeader.LimitedInventoryIds[this.Basis.InventoryID.Value] = new Decimal?(this.AdmittedQty);
          }

          public virtual void Spread()
          {
            Decimal restQty = this.RestQty;
            if (this.RestQty > 0M)
              this.FulfillUnassigned();
            if (this.RestQty > 0M)
              this.FulfillDirectly();
            if (this.RestQty > 0M)
              this.FulfillFromDonor();
            if (this.RestQty > 0M)
              this.FulfillDirectlyWithThresholds();
            if (this.RestQty > 0M)
              this.FulfillFromDonorWithThresholds();
            if (this.RestQty > 0M && this.NewLineIsNeeded)
              this.Overreceive();
            if (!this.Basis.Get<ReceivePutAway.ReceiveMode.ConfirmState.Logic>().IsCurrentItemQuantityLimited())
              return;
            this.Basis.Get<ReceivePutAway.ReceiveMode.ConfirmState.Logic>().AdjustQuantityLimitFor(this.RestQty - restQty, this.Basis.InventoryID.Value);
          }

          protected virtual void SetSplitInfo(
            ILSMaster split,
            bool isAcceptor = false,
            bool forceSetExpireDate = false)
          {
            if (isAcceptor)
            {
              if (this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>())
                split.LocationID = new int?();
              if (!this.Mode.UseDefaultLotSerial)
                split.LotSerialNbr = (string) null;
              if (!this.Mode.UseDefaultExpireDate)
                split.ExpireDate = new DateTime?();
            }
            forceSetExpireDate |= isAcceptor;
            forceSetExpireDate |= !string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
            if (this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>() && this.Basis.LocationID.HasValue)
              split.LocationID = this.Basis.LocationID;
            if (this.Mode.IsLotSerialUserInputRequired && this.Basis.LotSerialNbr != null)
              split.LotSerialNbr = this.Basis.LotSerialNbr;
            DateTime? nullable = forceSetExpireDate ? this.Basis.ExpireDate : this.Mode.EnsureExpireDateDefault();
            if (!this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState>() || !(!this.Mode.UseDefaultExpireDate | forceSetExpireDate) || !nullable.HasValue)
              return;
            split.ExpireDate = nullable;
          }

          protected virtual void FulfillUnassigned()
          {
            foreach (PX.Objects.PO.POReceiptLineSplit unassignedSplit in this.UnassignedSplits)
            {
              if (this.RestQty == 0M)
                break;
              Decimal? nullable = unassignedSplit.Qty;
              Decimal num1 = Math.Min(nullable.Value, this.RestQty);
              if (num1 > 0M)
              {
                ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Current = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Search<PX.Objects.PO.POReceiptLine.lineNbr>((object) unassignedSplit.LineNbr, Array.Empty<object>()));
                PX.Objects.PO.POReceiptLineSplit receiptLineSplit1 = PXResultset<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Graph.splits).Search<PX.Objects.PO.POReceiptLineSplit.locationID, PX.Objects.PO.POReceiptLineSplit.lotSerialNbr>((object) this.Basis.LocationID, (object) this.Basis.LotSerialNbr, Array.Empty<object>()));
                if (receiptLineSplit1 == null)
                {
                  PX.Objects.PO.POReceiptLineSplit copy = PXCache<PX.Objects.PO.POReceiptLineSplit>.CreateCopy(unassignedSplit);
                  copy.IsUnassigned = new bool?(false);
                  copy.SplitLineNbr = new int?();
                  copy.PlanID = new long?();
                  copy.Qty = new Decimal?(num1);
                  copy.ReceivedQty = new Decimal?(num1);
                  this.SetSplitInfo((ILSMaster) copy, true);
                  this.AcceptorSplits.Add(((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Insert(copy));
                }
                else
                {
                  PX.Objects.PO.POReceiptLineSplit receiptLineSplit2 = receiptLineSplit1;
                  nullable = receiptLineSplit2.Qty;
                  Decimal num2 = num1;
                  receiptLineSplit2.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num2) : new Decimal?();
                  PX.Objects.PO.POReceiptLineSplit receiptLineSplit3 = receiptLineSplit1;
                  nullable = receiptLineSplit3.ReceivedQty;
                  Decimal num3 = num1;
                  receiptLineSplit3.ReceivedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num3) : new Decimal?();
                  ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(receiptLineSplit1);
                }
                this.RestQty -= num1;
              }
            }
          }

          protected virtual void FulfillDirectly()
          {
            foreach (PX.Objects.PO.POReceiptLineSplit exactSplit in this.ExactSplits)
            {
              if (this.RestQty == 0M)
                break;
              Decimal? nullable = exactSplit.Qty;
              Decimal num1 = nullable.Value;
              nullable = exactSplit.ReceivedQty;
              Decimal num2 = nullable.Value;
              Decimal num3 = Math.Min(num1 - num2, this.RestQty);
              if (num3 > 0M)
              {
                ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Current = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Search<PX.Objects.PO.POReceiptLine.lineNbr>((object) exactSplit.LineNbr, Array.Empty<object>()));
                PX.Objects.PO.POReceiptLineSplit receiptLineSplit = exactSplit;
                nullable = receiptLineSplit.ReceivedQty;
                Decimal num4 = num3;
                receiptLineSplit.ReceivedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num4) : new Decimal?();
                this.SetSplitInfo((ILSMaster) exactSplit);
                ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(exactSplit);
                this.RestQty -= num3;
              }
            }
          }

          protected virtual void FulfillFromDonor()
          {
            foreach (IGrouping<int?, PX.Objects.PO.POReceiptLineSplit> grouping in this.DonorSplits.Except<PX.Objects.PO.POReceiptLineSplit>(this.ExactSplits).GroupBy<PX.Objects.PO.POReceiptLineSplit, int?>((Func<PX.Objects.PO.POReceiptLineSplit, int?>) (s => s.LineNbr)))
            {
              Decimal num1 = 0M;
              PX.Objects.PO.POReceiptLineSplit split = (PX.Objects.PO.POReceiptLineSplit) null;
              foreach (PX.Objects.PO.POReceiptLineSplit receiptLineSplit1 in (IEnumerable<PX.Objects.PO.POReceiptLineSplit>) grouping)
              {
                if (!(this.RestQty == 0M))
                {
                  Decimal num2 = Math.Min(receiptLineSplit1.Qty.Value - receiptLineSplit1.ReceivedQty.Value, this.RestQty);
                  if (num2 > 0M)
                  {
                    ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Current = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Search<PX.Objects.PO.POReceiptLine.lineNbr>((object) receiptLineSplit1.LineNbr, Array.Empty<object>()));
                    PX.Objects.PO.POReceiptLineSplit receiptLineSplit2 = receiptLineSplit1;
                    Decimal? qty = receiptLineSplit2.Qty;
                    Decimal num3 = num2;
                    receiptLineSplit2.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - num3) : new Decimal?();
                    ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(receiptLineSplit1);
                    if (split == null)
                      split = PXCache<PX.Objects.PO.POReceiptLineSplit>.CreateCopy(receiptLineSplit1);
                    this.RestQty -= num2;
                    num1 += num2;
                  }
                }
                else
                  break;
              }
              if (split != null)
              {
                split.SplitLineNbr = new int?();
                split.PlanID = new long?();
                split.Qty = new Decimal?(num1);
                split.ReceivedQty = new Decimal?(num1);
                this.SetSplitInfo((ILSMaster) split, true);
                this.AcceptorSplits.Add(((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Insert(split));
              }
            }
          }

          protected virtual void FulfillDirectlyWithThresholds()
          {
            foreach (IGrouping<\u003C\u003Ef__AnonymousType97<int?, string>, PX.Objects.PO.POReceiptLineSplit> source in this.ExactSplits.Concat<PX.Objects.PO.POReceiptLineSplit>((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) this.AcceptorSplits).GroupBy(s => new
            {
              LineNbr = s.LineNbr,
              PONbr = s.PONbr
            }).OrderBy<IGrouping<\u003C\u003Ef__AnonymousType97<int?, string>, PX.Objects.PO.POReceiptLineSplit>, bool>(g => g.Key.PONbr == null))
            {
              if (this.RestQty == 0M)
                break;
              Decimal num1 = source.Key.PONbr == null ? this.RestQty : Math.Min(this.RestQty, this.GetExtendedRestQty(source.First<PX.Objects.PO.POReceiptLineSplit>()));
              foreach (PX.Objects.PO.POReceiptLineSplit split in source.Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
              {
                int num5 = this.Basis.ReceiveBySingleItem ? 1 : 0;
                Decimal? receivedQty = s.ReceivedQty;
                Decimal num6 = 0M;
                int num7 = receivedQty.GetValueOrDefault() == num6 & receivedQty.HasValue ? 1 : 0;
                return (num5 != 0).Implies(num7 != 0);
              })))
              {
                if (!(num1 == 0M))
                {
                  Decimal num2 = this.Basis.ReceiveBySingleItem ? 1M : num1;
                  if (num2 > 0M)
                  {
                    ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Current = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Search<PX.Objects.PO.POReceiptLine.lineNbr>((object) split.LineNbr, Array.Empty<object>()));
                    PX.Objects.PO.POReceiptLineSplit receiptLineSplit1 = split;
                    Decimal? nullable = receiptLineSplit1.ReceivedQty;
                    Decimal num3 = num2;
                    receiptLineSplit1.ReceivedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num3) : new Decimal?();
                    if (source.Key.PONbr == null)
                    {
                      PX.Objects.PO.POReceiptLineSplit receiptLineSplit2 = split;
                      nullable = receiptLineSplit2.Qty;
                      Decimal num4 = num2;
                      receiptLineSplit2.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num4) : new Decimal?();
                    }
                    this.SetSplitInfo((ILSMaster) split);
                    ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(split);
                    this.RestQty -= num2;
                    num1 -= num2;
                  }
                }
                else
                  break;
              }
            }
          }

          protected virtual void FulfillFromDonorWithThresholds()
          {
            foreach (IGrouping<\u003C\u003Ef__AnonymousType97<int?, string>, PX.Objects.PO.POReceiptLineSplit> source in this.DonorSplits.Except<PX.Objects.PO.POReceiptLineSplit>(this.ExactSplits.Concat<PX.Objects.PO.POReceiptLineSplit>((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) this.AcceptorSplits)).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
            {
              Decimal? nullable = s.Qty;
              Decimal num8 = nullable.Value;
              nullable = s.ReceivedQty;
              Decimal num9 = nullable.Value;
              return num8 - num9 > 0M || this.GetExtendedRestQty(s) > 0M;
            })).GroupBy(s => new
            {
              LineNbr = s.LineNbr,
              PONbr = s.PONbr
            }).Where<IGrouping<\u003C\u003Ef__AnonymousType97<int?, string>, PX.Objects.PO.POReceiptLineSplit>>(g => g.Key.PONbr != null))
            {
              if (this.RestQty == 0M)
                break;
              Decimal num1 = Math.Min(this.GetExtendedRestQty(source.First<PX.Objects.PO.POReceiptLineSplit>()), this.RestQty);
              if (num1 > 0M)
              {
                ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Current = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Search<PX.Objects.PO.POReceiptLine.lineNbr>((object) source.Key.LineNbr, Array.Empty<object>()));
                Decimal num2 = 0M;
                Decimal val2 = num1;
                foreach (PX.Objects.PO.POReceiptLineSplit split in (IEnumerable<PX.Objects.PO.POReceiptLineSplit>) source)
                {
                  if (!(val2 == 0M))
                  {
                    Decimal? nullable = split.Qty;
                    Decimal num3 = nullable.Value;
                    nullable = split.ReceivedQty;
                    Decimal num4 = nullable.Value;
                    Decimal num5 = Math.Min(num3 - num4, val2);
                    if (num5 > 0M)
                    {
                      PX.Objects.PO.POReceiptLineSplit receiptLineSplit = split;
                      Decimal? qty = receiptLineSplit.Qty;
                      Decimal num6 = num5;
                      receiptLineSplit.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - num6) : new Decimal?();
                      ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(split);
                      val2 -= num5;
                      num2 += num5;
                    }
                    if (val2 > 0M)
                    {
                      Decimal num7 = Math.Min(this.GetExtendedRestQty(split), val2);
                      if (num7 > 0M)
                      {
                        val2 -= num7;
                        num2 += num7;
                      }
                    }
                  }
                  else
                    break;
                }
                if (num2 > 0M)
                {
                  PX.Objects.PO.POReceiptLineSplit copy = PXCache<PX.Objects.PO.POReceiptLineSplit>.CreateCopy(source.First<PX.Objects.PO.POReceiptLineSplit>());
                  copy.SplitLineNbr = new int?();
                  copy.PlanID = new long?();
                  copy.Qty = new Decimal?(num2);
                  copy.ReceivedQty = new Decimal?(num2);
                  this.SetSplitInfo((ILSMaster) copy, true);
                  ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Insert(copy);
                  this.RestQty -= num2;
                }
              }
            }
          }

          protected virtual void Overreceive()
          {
            PXOrderedSelect<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Current<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Current<PX.Objects.PO.POReceipt.receiptNbr>>>>, OrderBy<Asc<PX.Objects.PO.POReceiptLine.receiptType, Asc<PX.Objects.PO.POReceiptLine.receiptNbr, Asc<PX.Objects.PO.POReceiptLine.sortOrder, Asc<PX.Objects.PO.POReceiptLine.lineNbr>>>>>> transactions = this.Graph.transactions;
            PX.Objects.PO.POReceiptLine poReceiptLine1 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).With<PXSelectBase<PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceiptLine>((Func<PXSelectBase<PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceiptLine>) (_ => _.Insert() ?? _.Insert()));
            ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).SetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>(poReceiptLine1, (object) this.Basis.InventoryID);
            ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).SetValueExt<PX.Objects.PO.POReceiptLine.subItemID>(poReceiptLine1, (object) this.Basis.SubItemID);
            ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).SetValueExt<PX.Objects.PO.POReceiptLine.siteID>(poReceiptLine1, (object) this.Basis.SiteID);
            ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).SetValueExt<PX.Objects.PO.POReceiptLine.uOM>(poReceiptLine1, (object) this.Basis.SelectedInventoryItem.BaseUnit);
            PX.Objects.PO.POReceiptLine poReceiptLine2 = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).Update(poReceiptLine1);
            poReceiptLine2.Qty = new Decimal?(this.RestQty);
            PX.Objects.PO.POReceiptLine split = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).Update(poReceiptLine2);
            this.SetSplitInfo((ILSMaster) split, forceSetExpireDate: true);
            ((PXSelectBase<PX.Objects.PO.POReceiptLine>) transactions).Update(split);
            foreach (PXResult<PX.Objects.PO.POReceiptLineSplit> pxResult in ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Graph.splits).Select(Array.Empty<object>()))
            {
              PX.Objects.PO.POReceiptLineSplit receiptLineSplit = PXResult<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(pxResult);
              receiptLineSplit.ReceivedQty = receiptLineSplit.Qty;
              ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Received).Update(receiptLineSplit);
            }
          }
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm receiving {0} x {1} {2}.";
        public const string QtyExceedsReceipt = "The quantity of the {1} item exceeds the item's quantity in the {0} purchase receipt.";
        public const string QtyAboveRemainsToBeReceived = "The quantity on the receipt will exceed the maximum quantity specified for this item in the line of the purchase order. The remaining quantity to be received is {0}.";
        public const string NothingToRemove = "No items to remove from receipt.";
        public const string Overreceiving = "The received quantity cannot be greater than the expected quantity.";
        public const string Underreceiving = "The received quantity cannot be negative.";
        public const string InventoryAdded = "{0} x {1} {2} added to receipt.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from receipt.";
        public const string ItemOrReceiptPrompt = "Scan the item or the next receipt number.";
      }
    }

    public sealed class ReleaseReceiptCommand : 
      BarcodeDrivenStateMachine<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.ScanCommand
    {
      public virtual 
      #nullable disable
      string Code => "RELEASE";

      public virtual string ButtonName => "scanReleaseReceipt";

      public virtual string DisplayName => "Release Receipt";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<ReceivePutAway>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        this.Get<ReceivePutAway.ReceiveMode.ReleaseReceiptCommand.Logic>().ReleaseReceipt(false);
        return true;
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public const 
        #nullable disable
        string ButtonName = "scanReleaseReceipt";

        protected virtual bool MatchesMode => this.Basis.CurrentMode is ReceivePutAway.ReceiveMode;

        protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
        {
          ((PXGraph) this.Graph).Actions["scanReleaseReceipt"]?.SetVisible(this.MatchesMode && !this.Basis.VerifyBeforeRelease);
        }

        public virtual void ReleaseReceipt(bool completePOLines)
        {
          bool flag = this.Basis.ApplyLinesQtyChanges(completePOLines);
          this.Basis.SaveChanges();
          this.Basis.Reset(false);
          this.Basis.Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>(true);
          POReceivePutAwayUserSetup putAwayUserSetup = PXSetupBase<ReceivePutAway.UserSetup, ReceivePutAway.Host, ScanHeader, POReceivePutAwayUserSetup, Where<POReceivePutAwayUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis));
          string printLabelsReportID = putAwayUserSetup.PrintInventoryLabelsAutomatically.GetValueOrDefault() ? putAwayUserSetup.InventoryLabelsReportID : (string) null;
          bool printReceipt = flag && putAwayUserSetup.PrintPurchaseReceiptAutomatically.GetValueOrDefault();
          (string, string) msg = (this.Success, this.Fail);
          this.Basis.AwaitFor<PX.Objects.PO.POReceipt>((Func<ReceivePutAway, PX.Objects.PO.POReceipt, CancellationToken, System.Threading.Tasks.Task>) ((basis, doc, ct) => ReceivePutAway.ReleaseReceiptImpl(doc, printLabelsReportID, printReceipt, ct))).WithDescription(this.InProcess, new object[1]
          {
            (object) this.Basis.RefNbr
          }).ActualizeDataBy((Func<ReceivePutAway, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt>) ((basis, doc) => (PX.Objects.PO.POReceipt) PrimaryKeyOf<PX.Objects.PO.POReceipt>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>>.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) basis), (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>) doc, (PKFindOptions) 0))).OnSuccess((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.PO.POReceipt>.ISuccessProcessor>) (x => x.Say(msg.Item1, Array.Empty<object>()).ChangeStateTo<ReceivePutAway.ReceiveMode.ReceiptState>())).OnFail((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.PO.POReceipt>.IResultProcessor>) (x => x.Say(msg.Item2, Array.Empty<object>()))).BeginAwait(this.Basis.Receipt);
        }

        protected virtual string InProcess => "Release of {0} receipt in progress.";

        protected virtual string Success => "Receipt successfully released.";

        protected virtual string Fail => "Receipt not released.";
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Release Receipt";
        public const string InProcess = "Release of {0} receipt in progress.";
        public const string Success = "Receipt successfully released.";
        public const string Fail = "Receipt not released.";
      }
    }

    public sealed class ConfirmReceiptCommand : 
      BarcodeDrivenStateMachine<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.ScanCommand
    {
      public virtual 
      #nullable disable
      string Code => "CONFIRM";

      public virtual string ButtonName => "scanConfirmReceipt";

      public virtual string DisplayName => "Confirm Receipt";

      protected virtual bool IsEnabled
      {
        get
        {
          return ((ScanComponent<ReceivePutAway>) this).Basis.DocumentIsEditable && this.Get<ReceivePutAway.ReceiveMode.ConfirmReceiptCommand.Logic>().HasStartedLines;
        }
      }

      protected virtual bool Process()
      {
        this.Get<ReceivePutAway.ReceiveMode.ConfirmReceiptCommand.Logic>().ConfirmReceive();
        return true;
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public const 
        #nullable disable
        string ButtonName = "scanConfirmReceipt";

        protected virtual bool MatchesMode => this.Basis.CurrentMode is ReceivePutAway.ReceiveMode;

        protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
        {
          ((PXGraph) this.Graph).Actions["scanConfirmReceipt"]?.SetVisible(this.MatchesMode && this.Basis.VerifyBeforeRelease);
        }

        public virtual bool HasStartedLines
        {
          get
          {
            return ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Basis.Get<ReceivePutAway.ReceiveMode.Logic>().Received).SelectMain(Array.Empty<object>())).Any<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (split =>
            {
              Decimal? receivedQty = split.ReceivedQty;
              Decimal num = 0M;
              return receivedQty.GetValueOrDefault() > num & receivedQty.HasValue;
            }));
          }
        }

        public virtual void ConfirmReceive()
        {
          bool removeZeroLines = !POReceivePutAwaySetup.PK.Find((PXGraph) this.Basis.Graph, ((PXGraph) this.Basis.Graph).Accessinfo.BranchID).KeepZeroLinesOnReceiptConfirmation.GetValueOrDefault();
          bool flag;
          using (new AllowZeroReceiptQtyScope(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Current))
          {
            flag = this.Basis.ApplyLinesQtyChanges(false, removeZeroLines);
            this.Basis.SaveChanges();
          }
          this.Basis.Reset(false);
          this.Basis.Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>(true);
          bool printReceipt = flag && PXSetupBase<ReceivePutAway.UserSetup, ReceivePutAway.Host, ScanHeader, POReceivePutAwayUserSetup, Where<POReceivePutAwayUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis)).PrintPurchaseReceiptAutomatically.GetValueOrDefault();
          (string, string) msg = (this.Success, this.Fail);
          this.Basis.AwaitFor<PX.Objects.PO.POReceipt>((Func<ReceivePutAway, PX.Objects.PO.POReceipt, CancellationToken, System.Threading.Tasks.Task>) ((basis, doc, ct) => ReceivePutAway.ConfirmReceiveImpl(doc, printReceipt, ct))).WithDescription(this.InProcess, new object[1]
          {
            (object) this.Basis.RefNbr
          }).ActualizeDataBy((Func<ReceivePutAway, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt>) ((basis, doc) => (PX.Objects.PO.POReceipt) PrimaryKeyOf<PX.Objects.PO.POReceipt>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>>.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) basis), (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>) doc, (PKFindOptions) 0))).OnSuccess((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.PO.POReceipt>.ISuccessProcessor>) (x => x.Say(msg.Item1, Array.Empty<object>()).ChangeStateTo<ReceivePutAway.ReceiveMode.ReceiptState>())).OnFail((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.PO.POReceipt>.IResultProcessor>) (x => x.Say(msg.Item2, Array.Empty<object>()))).BeginAwait(this.Basis.Receipt);
        }

        protected virtual string InProcess => "Confirmation of the {0} receipt is in progress.";

        protected virtual string Success => "Receipt successfully received.";

        protected virtual string Fail => "Receipt cannot be confirmed.";
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Confirm Receipt";
        public const string InProcess = "Confirmation of the {0} receipt is in progress.";
        public const string Success = "Receipt successfully received.";
        public const string Fail = "Receipt cannot be confirmed.";
      }
    }

    public sealed class ReleaseReceiptAndCompletePOLinesCommand : 
      BarcodeDrivenStateMachine<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.ScanCommand
    {
      public virtual 
      #nullable disable
      string Code => "COMPLETE*POLINES";

      public virtual string ButtonName => "scanCompletePOLines";

      public virtual string DisplayName => "Complete PO Lines";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<ReceivePutAway>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        this.Get<ReceivePutAway.ReceiveMode.ReleaseReceiptCommand.Logic>().ReleaseReceipt(true);
        return true;
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public const 
        #nullable disable
        string ButtonName = "scanCompletePOLines";

        protected virtual bool MatchesMode => this.Basis.CurrentMode is ReceivePutAway.ReceiveMode;

        protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
        {
          ((PXGraph) this.Graph).Actions["scanCompletePOLines"]?.SetVisible(this.MatchesMode && !this.Basis.VerifyBeforeRelease);
        }
      }

      [PXLocalizable]
      public abstract class Msg : ReceivePutAway.ReceiveMode.ReleaseReceiptCommand.Msg
      {
        public new const string DisplayName = "Complete PO Lines";
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<ReceivePutAway>.SetMode<ReceivePutAway.ReceiveMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "RECEIVE";

      public virtual string DisplayName => "PO Receive";

      private string RefNbr { get; set; }

      public virtual bool IsPossible
      {
        get
        {
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSReceiving>() ? 1 : 0;
          POReceivePutAwaySetup receivePutAwaySetup = POReceivePutAwaySetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          if (num == 0)
            return false;
          if (receivePutAwaySetup == null)
            return true;
          bool? showReceivingTab = receivePutAwaySetup.ShowReceivingTab;
          bool flag = false;
          return !(showReceivingTab.GetValueOrDefault() == flag & showReceivingTab.HasValue);
        }
      }

      protected virtual bool PrepareRedirect()
      {
        if (((ScanComponent<TForeignBasis>) this).Basis is ReceivePutAway basis && basis.CurrentMode.Code != "VRTN" && this.RefNbr != null)
        {
          Validation? nullable = ((ScanMode<ReceivePutAway>) basis.FindMode<ReceivePutAway.ReceiveMode>()).TryValidate<PX.Objects.PO.POReceipt>(basis.Receipt).By<ReceivePutAway.ReceiveMode.ReceiptState>();
          if (nullable.HasValue)
          {
            Validation valueOrDefault = nullable.GetValueOrDefault();
            if (((Validation) ref valueOrDefault).IsError.GetValueOrDefault())
            {
              basis.ReportError(((Validation) ref valueOrDefault).Message, ((Validation) ref valueOrDefault).MessageArgs);
              return false;
            }
          }
          this.RefNbr = basis.RefNbr;
        }
        return true;
      }

      protected virtual void CompleteRedirect()
      {
        if (!(((ScanComponent<TForeignBasis>) this).Basis is ReceivePutAway basis) || !(basis.CurrentMode.Code != "VRTN") || this.RefNbr == null || !basis.TryProcessBy("RNBR", this.RefNbr, (StateSubstitutionRule) 253))
          return;
        basis.SetDefaultState((string) null, Array.Empty<object>());
        this.RefNbr = (string) null;
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "PO Receive";
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<
    #nullable enable
    ReceivePutAway>.Msg
    {
      public const 
      #nullable disable
      string Description = "Receive";
      public const string Completed = "{0} receipt received.";
      public const string RestQty = "Remaining Qty.";
      public const string QtyAboveMaximum = "The quantity on the receipt will exceed the maximum quantity specified for this item in the line of the purchase order.";
    }

    [PXUIField(Visible = false)]
    public class ShowReceive : 
      PXFieldAttachedTo<ScanHeader>.By<ReceivePutAway.Host>.AsBool.Named<ReceivePutAway.ReceiveMode.ShowReceive>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(((PXSelectBase<POReceivePutAwaySetup>) this.Base.WMS.Setup).Current.ShowReceivingTab.GetValueOrDefault() && row.Mode == "RCPT");
      }
    }
  }

  public class Host : POReceiptEntry, ICaptionable
  {
    public ReceivePutAway WMS => ((PXGraph) this).FindImplementation<ReceivePutAway>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public new class QtySupport : 
    WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.QtySupport
  {
    protected virtual void OnPreviousHeaderRestored(ScanHeader headerBackup)
    {
      string transferRefNbr = ScanHeaderExt.Get<RPAScanHeader>(headerBackup).TransferRefNbr;
      if (string.IsNullOrEmpty(transferRefNbr) || !string.IsNullOrEmpty(this.Basis.TransferRefNbr))
        return;
      this.Basis.TransferRefNbr = transferRefNbr;
    }
  }

  public new class GS1Support : 
    WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.GS1Support
  {
  }

  public class UserSetup : 
    PXUserSetup<ReceivePutAway.UserSetup, ReceivePutAway.Host, ScanHeader, POReceivePutAwayUserSetup, POReceivePutAwayUserSetup.userID>
  {
  }

  public abstract class ReceiptState : 
    WarehouseManagementSystem<
    #nullable enable
    ReceivePutAway, ReceivePutAway.Host>.RefNbrState<
    #nullable disable
    PX.Objects.PO.POReceipt>
  {
    protected virtual string StatePrompt => "Scan the receipt number.";

    protected virtual PX.Objects.PO.POReceipt GetByBarcode(string barcode)
    {
      return PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.PO.POReceipt.vendorID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AP.Vendor, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
      {
        (object) barcode
      }));
    }

    protected virtual void Apply(PX.Objects.PO.POReceipt receipt)
    {
      ((ScanComponent<ReceivePutAway>) this).Basis.ReceiptType = receipt.ReceiptType;
      ((ScanComponent<ReceivePutAway>) this).Basis.RefNbr = receipt.ReceiptNbr;
      ((ScanComponent<ReceivePutAway>) this).Basis.TranDate = receipt.ReceiptDate;
      ((ScanComponent<ReceivePutAway>) this).Basis.TranType = receipt.ReceiptType == "RX" ? "TRX" : (receipt.ReceiptType == "RN" ? "RET" : "RCP");
      ((ScanComponent<ReceivePutAway>) this).Basis.NoteID = receipt.NoteID;
      ((PXSelectBase<PX.Objects.PO.POReceipt>) ((ScanComponent<ReceivePutAway>) this).Basis.Graph.Document).Current = receipt;
    }

    protected virtual void ClearState()
    {
      ((PXSelectBase<PX.Objects.PO.POReceipt>) ((ScanComponent<ReceivePutAway>) this).Basis.Graph.Document).Current = (PX.Objects.PO.POReceipt) null;
      ((ScanComponent<ReceivePutAway>) this).Basis.ReceiptType = (string) null;
      ((ScanComponent<ReceivePutAway>) this).Basis.RefNbr = (string) null;
      ((ScanComponent<ReceivePutAway>) this).Basis.TranDate = new DateTime?();
      ((ScanComponent<ReceivePutAway>) this).Basis.TranType = (string) null;
      ((ScanComponent<ReceivePutAway>) this).Basis.NoteID = new Guid?();
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<ReceivePutAway>) this).Basis.ReportError("{0} receipt not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual void ReportSuccess(PX.Objects.PO.POReceipt receipt)
    {
      ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo("{0} receipt loaded and ready to be processed.", new object[1]
      {
        (object) receipt.ReceiptNbr
      });
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the receipt number.";
      public const string Ready = "{0} receipt loaded and ready to be processed.";
      public const string Missing = "{0} receipt not found.";
      public const string InvalidStatus = "The {0} receipt cannot be processed because it has the {1} status.";
      public const string InvalidType = "The {0} receipt cannot be processed because it has the {1} type.";
      public const string InvalidOrderType = "The {0} receipt cannot be processed because the related order has the {1} type.";
      public const string MultiSites = "All items in the {0} receipt must be located in the same warehouse.";
      public const string HasNonStockKit = "The {0} receipt cannot be processed because it contains a non-stock kit item.";
    }
  }

  public static class FieldAttached
  {
    public abstract class To<TTable> : PXFieldAttachedTo<TTable>.By<ReceivePutAway.Host> where TTable : class, IBqlTable, new()
    {
    }

    [PXUIField(DisplayName = "Matched")]
    public class Fits : 
      PXFieldAttachedTo<PX.Objects.PO.POReceiptLineSplit>.By<ReceivePutAway.Host>.AsBool.Named<ReceivePutAway.FieldAttached.Fits>
    {
      public override bool? GetValue(PX.Objects.PO.POReceiptLineSplit row)
      {
        bool flag = true;
        int? nullable1;
        if (this.Base.WMS.LocationID.HasValue)
        {
          int num1 = flag ? 1 : 0;
          nullable1 = this.Base.WMS.LocationID;
          int? locationId = row.LocationID;
          int num2 = nullable1.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable1.HasValue == locationId.HasValue ? 1 : 0;
          flag = (num1 & num2) != 0;
        }
        if (this.Base.WMS.InventoryID.HasValue)
        {
          int num3 = flag ? 1 : 0;
          int? nullable2 = this.Base.WMS.InventoryID;
          nullable1 = row.InventoryID;
          int num4;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = this.Base.WMS.SubItemID;
            nullable2 = row.SubItemID;
            num4 = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue ? 1 : 0;
          }
          else
            num4 = 0;
          flag = (num3 & num4) != 0;
        }
        if (this.Base.WMS.LotSerialNbr != null)
          flag &= string.Equals(this.Base.WMS.LotSerialNbr, row.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
        return new bool?(flag);
      }
    }

    [PXUIField(Visible = false)]
    public class ShowLog : 
      PXFieldAttachedTo<ScanHeader>.By<ReceivePutAway.Host>.AsBool.Named<ReceivePutAway.FieldAttached.ShowLog>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(((PXSelectBase<POReceivePutAwaySetup>) this.Base.WMS.Setup).Current.ShowScanLogTab.GetValueOrDefault());
      }
    }
  }

  [PXUIField(DisplayName = "Remaining Qty.")]
  public class RestQty : 
    PXFieldAttachedTo<PX.Objects.PO.POReceiptLineSplit>.By<ReceivePutAway.Host>.AsDecimal.Named<ReceivePutAway.RestQty>
  {
    public override Decimal? GetValue(PX.Objects.PO.POReceiptLineSplit row)
    {
      Decimal num;
      if (!(this.Base.WMS.Header.Mode == "RCPT"))
      {
        if (!(this.Base.WMS.Header.Mode == "TRRC"))
        {
          num = 0M;
        }
        else
        {
          Decimal? qty = row.Qty;
          Decimal? receivedQty = row.ReceivedQty;
          num = Math.Max(0M, (qty.HasValue & receivedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - receivedQty.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault());
        }
      }
      else
        num = this.Base.WMS.Get<ReceivePutAway.ReceiveMode.Logic>().GetNormalRestQty(row);
      return new Decimal?(num);
    }

    protected override bool? Visible
    {
      get
      {
        return new bool?(this.Base.WMS.With<ReceivePutAway, bool>((Func<ReceivePutAway, bool>) (wms => EnumerableExtensions.IsIn<string>(wms.Header.Mode, (IEnumerable<string>) new string[2]
        {
          "RCPT",
          "TRRC"
        }))));
      }
    }
  }

  [PXLocalizable]
  public new abstract class Msg : WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.Msg
  {
    public const string ReceiptIsNotEditable = "The document has become unavailable for editing. Contact your manager.";
    public const string SetupLotSerialFlagsInconsistency = "The {0} check box cannot be cleared if the {1} check box is selected.";
  }

  public sealed class ReceiveTransferMode : 
    BarcodeDrivenStateMachine<
    #nullable enable
    ReceivePutAway, ReceivePutAway.Host>.ScanMode
  {
    public const string Value = "TRRC";

    public virtual string Code => "TRRC";

    public ReceivePutAway.ReceiveTransferMode.Logic Body
    {
      get => this.Get<ReceivePutAway.ReceiveTransferMode.Logic>();
    }

    public virtual string Description => "Receive Transfer";

    protected virtual IEnumerable<ScanState<ReceivePutAway>> CreateStates()
    {
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReceiveTransferMode.ReceiptState();
      yield return (ScanState<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState();
      yield return (ScanState<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState()
      {
        IsForTransfer = true
      };
      yield return (ScanState<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReceiveTransferMode.ConfirmState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReceiveTransferMode.TransferShipmentState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReceiveTransferMode.TransferOrderState();
    }

    protected virtual IEnumerable<ScanTransition<ReceivePutAway>> CreateTransitions()
    {
      return !this.Body.IsSingleLocation && this.Body.PromptLocationForEveryLine ? ((ScanMode<ReceivePutAway>) this).StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<ReceivePutAway.ReceiveTransferMode.ReceiptState>().NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>((Action<ReceivePutAway>) null))) : ((ScanMode<ReceivePutAway>) this).StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<ReceivePutAway.ReceiveTransferMode.ReceiptState>().NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>((Action<ReceivePutAway>) null)));
    }

    protected virtual IEnumerable<ScanCommand<ReceivePutAway>> CreateCommands()
    {
      yield return (ScanCommand<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.RemoveCommand();
      yield return (ScanCommand<ReceivePutAway>) new BarcodeQtySupport<ReceivePutAway, ReceivePutAway.Host>.SetQtyCommand();
      yield return (ScanCommand<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ReleaseReceiptCommand();
      yield return (ScanCommand<ReceivePutAway>) new ReceivePutAway.ReceiveMode.ConfirmReceiptCommand();
    }

    protected virtual IEnumerable<ScanRedirect<ReceivePutAway>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<ReceivePutAway>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<ReceivePutAway>) this).ResetMode(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.ReceiveTransferMode.ReceiptState>(fullReset && !((ScanMode<ReceivePutAway>) this).Basis.IsWithinReset);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>(fullReset || this.Body.PromptLocationForEveryLine && !this.Body.IsSingleLocation);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>(true);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.ExpireDateState>(true);
      ((ScanMode<ReceivePutAway>) this).Basis.AddTransferMode = new bool?(false);
    }

    public class Logic : BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanExtension
    {
      public FbqlSelect<
      #nullable disable
      SelectFromBase<
      #nullable enable
      PX.Objects.PO.POReceiptLineSplit, 
      #nullable disable
      TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<
      #nullable enable
      PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>, PX.Objects.PO.POReceiptLineSplit>.View ReceivedForTransfer;
      public PXAction<ScanHeader> ReviewTransferReceipt;
      public PXAction<ScanHeader> ViewTransferOrder;
      public PXAction<ScanHeader> ViewShipment;
      public PX.Objects.PO.POReceipt? Receipt;
      public PX.Objects.SO.SOShipment? Shipment;
      public PX.Objects.SO.SOOrder? Order;

      public bool IsLocationUserInputRequired
      {
        get
        {
          return this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>() && !this.Basis.DefaultLocationID.HasValue;
        }
      }

      public virtual bool IsLotSerialMissing(
        PXSelectBase<PX.Objects.PO.POReceiptLineSplit> splitView,
        string lotSerialNbr,
        out Validation error)
      {
        if (!this.Basis.LotSerialTrack.IsEnterable && ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.ReceivedForTransfer).SelectMain(Array.Empty<object>())).All<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (t => !string.Equals(t.LotSerialNbr, lotSerialNbr, StringComparison.OrdinalIgnoreCase))))
        {
          error = Validation.Fail("The {0} lot or serial number is not listed in the document.", new object[1]
          {
            (object) lotSerialNbr
          });
          return true;
        }
        error = Validation.Ok;
        return false;
      }

      public bool IsLotSerialUserInputRequired
      {
        get
        {
          return this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsEnterable;
        }
      }

      public virtual bool CanReceive
      {
        get
        {
          if (this.Basis.Receipt != null)
          {
            bool? nullable = this.Basis.Receipt.Received;
            if (!nullable.GetValueOrDefault())
            {
              nullable = this.Basis.Receipt.Released;
              if (!nullable.GetValueOrDefault())
                return ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.ReceivedForTransfer).SelectMain(Array.Empty<object>())).Any<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
                {
                  Decimal? receivedQty = s.ReceivedQty;
                  Decimal? qty = s.Qty;
                  return receivedQty.GetValueOrDefault() < qty.GetValueOrDefault() & receivedQty.HasValue & qty.HasValue;
                })) || this.PotentialItemsExist(this.Basis.Receipt);
            }
          }
          return false;
        }
      }

      public virtual IEnumerable receivedForTransfer()
      {
        return (IEnumerable) this.Basis.SortedResult((IEnumerable) this.Basis.GetSplits(this.Basis.Receipt, true, processedSeparator: (Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
        {
          Decimal? receivedQty = r.ReceivedQty;
          Decimal? qty = r.Qty;
          return receivedQty.GetValueOrDefault() == qty.GetValueOrDefault() & receivedQty.HasValue == qty.HasValue;
        })));
      }

      [PXButton(CommitChanges = true)]
      [PXUIField(DisplayName = "Review")]
      protected virtual IEnumerable reviewTransferReceipt(PXAdapter adapter) => adapter.Get();

      [PXButton(DisplayOnMainToolbar = false)]
      [PXUIField(DisplayName = "View Order")]
      protected virtual IEnumerable viewTransferOrder(PXAdapter adapter)
      {
        PX.Objects.PO.POReceiptLineSplit current = (PX.Objects.PO.POReceiptLineSplit) ((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceiptLineSplit>((PXGraph) this.Graph)).Current;
        if (current == null)
          return adapter.Get();
        PX.Objects.PO.POReceiptLine poReceiptLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Graph, (object[]) new PX.Objects.PO.POReceiptLineSplit[1]
        {
          current
        }, Array.Empty<object>()));
        if (poReceiptLine == null)
          return adapter.Get();
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) poReceiptLine.SOOrderNbr, new object[1]
        {
          (object) poReceiptLine.SOOrderType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewOrder");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }

      [PXButton(DisplayOnMainToolbar = false)]
      [PXUIField(DisplayName = "View Shipment")]
      protected virtual IEnumerable viewShipment(PXAdapter adapter)
      {
        PX.Objects.PO.POReceiptLineSplit current = (PX.Objects.PO.POReceiptLineSplit) ((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceiptLineSplit>((PXGraph) this.Graph)).Current;
        if (current == null)
          return adapter.Get();
        PX.Objects.PO.POReceiptLine poReceiptLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Graph, (object[]) new PX.Objects.PO.POReceiptLineSplit[1]
        {
          current
        }, Array.Empty<object>()));
        if (poReceiptLine == null)
          return adapter.Get();
        SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
        ((PXSelectBase<PX.Objects.SO.SOShipment>) instance.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) instance.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) poReceiptLine.SOShipmentNbr, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Shipment");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        ((PXAction) this.ReviewTransferReceipt).SetVisible(((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base).IsMobile && e.Row?.Mode == "TRRC");
      }

      public bool PromptLocationForEveryLine
      {
        get
        {
          return ((PXSelectBase<POReceivePutAwaySetup>) this.Basis.Setup).Current.RequestLocationForEachItemInReceive.GetValueOrDefault();
        }
      }

      public bool IsSingleLocation
      {
        get
        {
          return PXSetupBase<ReceivePutAway.UserSetup, ReceivePutAway.Host, ScanHeader, POReceivePutAwayUserSetup, Where<POReceivePutAwayUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis)).SingleLocation.GetValueOrDefault();
        }
      }

      public virtual void InjectLocationSkippable(
        WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState locationState)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, INLocation>, ReceivePutAway>.OfPredicate) ((EntityState<ReceivePutAway, INLocation>) locationState).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => basis.LocationID.HasValue), false, new RelativeInject?());
        ((MethodInterceptor<EntityState<ReceivePutAway, INLocation>, ReceivePutAway>.OfPredicate) ((EntityState<ReceivePutAway, INLocation>) locationState).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => !this.IsLocationUserInputRequired), false, new RelativeInject?());
      }

      public virtual void InjectInventoryItemTryProcessByLocation(
        WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState inventoryItemState)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>.AsAppendable) ((EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) inventoryItemState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((basis, barcode) => !this.IsSingleLocation && basis.TryProcessBy<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Done) : AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Skipped)), new RelativeInject?());
      }

      public virtual void InjectLotSerialPresenceValidation(
        WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState lotSerialState)
      {
        Validation error;
        ((MethodInterceptor<EntityState<ReceivePutAway, string>, ReceivePutAway>.OfFunc<string, Validation>.AsAppendable) ((EntityState<ReceivePutAway, string>) lotSerialState).Intercept.Validate).ByAppend((Func<Validation, string, Validation>) ((basis, lotSerialNbr) => !this.IsLotSerialMissing((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.ReceivedForTransfer, lotSerialNbr, out error) ? Validation.Ok : error), new RelativeInject?());
      }

      public virtual void InjectLotSerialSkippableWhenNotRequired(
        WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState lotSerialState)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, string>, ReceivePutAway>.OfPredicate) ((EntityState<ReceivePutAway, string>) lotSerialState).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => !basis.Get<ReceivePutAway.ReceiveMode.Logic>().IsLotSerialUserInputRequired), false, new RelativeInject?());
      }

      public virtual void InjectReceiptTryProcessByShipment(
        ReceivePutAway.ReceiveTransferMode.ReceiptState receiptState)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) receiptState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) =>
        {
          PX.Objects.SO.SOShipment soShipment;
          if (!basis.TryGet<PX.Objects.SO.SOShipment>().By<ReceivePutAway.ReceiveTransferMode.TransferShipmentState>(barcode, ref soShipment))
            return AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Skipped);
          if (!basis.TryProcessBy<ReceivePutAway.ReceiveTransferMode.TransferShipmentState>(barcode, (StateSubstitutionRule) 20))
            return AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Failed);
          PX.Objects.PO.POReceipt poReceipt = this.Receipt ?? (this.Shipment == null ? (PX.Objects.PO.POReceipt) null : this.CreateEmptyReceiptFrom(this.Shipment));
          return poReceipt != null ? AbsenceHandling.ReplaceWith<PX.Objects.PO.POReceipt>(poReceipt) : AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Failed);
        }), new RelativeInject?());
      }

      public virtual void InjectReceiptTryProcessByTransferOrder(
        ReceivePutAway.ReceiveTransferMode.ReceiptState receiptState)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) receiptState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) =>
        {
          PX.Objects.SO.SOOrder soOrder;
          if (!basis.TryGet<PX.Objects.SO.SOOrder>().By<ReceivePutAway.ReceiveTransferMode.TransferOrderState>(barcode, ref soOrder))
            return AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Skipped);
          if (!basis.TryProcessBy<ReceivePutAway.ReceiveTransferMode.TransferOrderState>(barcode, (StateSubstitutionRule) 20))
            return AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Failed);
          PX.Objects.PO.POReceipt poReceipt = this.Receipt ?? (this.Order == null ? (PX.Objects.PO.POReceipt) null : this.CreateEmptyReceiptFrom(this.Order));
          return poReceipt != null ? AbsenceHandling.ReplaceWith<PX.Objects.PO.POReceipt>(poReceipt) : AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(AbsenceHandling.Failed);
        }), new RelativeInject?());
      }

      public virtual void InjectReceiptTrySwitchToReceiveMode(
        ReceivePutAway.ReceiveTransferMode.ReceiptState receipt)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) receipt).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) => AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(basis.ProcessByMode<ReceivePutAway.ReceiveMode>(barcode))), new RelativeInject?());
      }

      public virtual void InjectReceiptTrySwitchToReturnMode(
        ReceivePutAway.ReceiveTransferMode.ReceiptState receipt)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) receipt).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) => AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(basis.ProcessByMode<ReceivePutAway.ReturnMode>(barcode))), new RelativeInject?());
      }

      public virtual void InjectShipmentTryProcessByOrder(
        ReceivePutAway.ReceiveTransferMode.TransferShipmentState shipment)
      {
        ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.SO.SOShipment>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.SO.SOShipment>) shipment).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.SO.SOShipment>, string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>) ((basis, barcode) =>
        {
          PX.Objects.SO.SOOrder soOrder;
          if (!basis.TryGet<PX.Objects.SO.SOOrder>().By<ReceivePutAway.ReceiveTransferMode.TransferOrderState>(barcode, ref soOrder))
            return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Skipped);
          return !basis.TryProcessBy<ReceivePutAway.ReceiveTransferMode.TransferOrderState>(barcode, (StateSubstitutionRule) 254) ? AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Failed) : AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Done);
        }), new RelativeInject?());
      }

      [PXOverride]
      public ScanState<ReceivePutAway> DecorateScanState(
        ScanState<ReceivePutAway> original,
        Func<ScanState<ReceivePutAway>, ScanState<ReceivePutAway>> base_DecorateScanState)
      {
        ScanState<ReceivePutAway> scanState = base_DecorateScanState(original);
        if (((ScanComponent<ReceivePutAway>) scanState).ModeCode == "TRRC")
        {
          switch (scanState)
          {
            case WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState locationState:
              this.InjectLocationSkippable(locationState);
              break;
            case WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState inventoryItemState:
              this.InjectInventoryItemTryProcessByLocation(inventoryItemState);
              break;
            case WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState lotSerialState:
              this.InjectLotSerialPresenceValidation(lotSerialState);
              this.InjectLotSerialSkippableWhenNotRequired(lotSerialState);
              break;
            case ReceivePutAway.ReceiveTransferMode.ReceiptState receiptState:
              this.InjectReceiptTryProcessByShipment(receiptState);
              this.InjectReceiptTryProcessByTransferOrder(receiptState);
              this.InjectReceiptTrySwitchToReceiveMode(receiptState);
              this.InjectReceiptTrySwitchToReturnMode(receiptState);
              break;
            case ReceivePutAway.ReceiveTransferMode.TransferShipmentState shipment:
              this.InjectShipmentTryProcessByOrder(shipment);
              break;
          }
        }
        return scanState;
      }

      public virtual PX.Objects.PO.POReceipt CreateEmptyReceiptFrom(PX.Objects.SO.SOShipment shipment)
      {
        PX.Objects.PO.POReceipt receipt = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.Document).Insert(new PX.Objects.PO.POReceipt()
        {
          ReceiptType = "RX",
          SiteID = shipment.DestinationSiteID
        });
        if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraphExtension<ReceivePutAway.Host>) this).Base.MultiCurrencyExt.GetCurrencyInfo(shipment.CuryInfoID);
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXGraphExtension<ReceivePutAway.Host>) this).Base.MultiCurrencyExt.GetCurrencyInfo(receipt.CuryInfoID);
          currencyInfo2.CuryID = currencyInfo1.CuryID;
          currencyInfo2.CuryRateTypeID = currencyInfo1.CuryRateTypeID;
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.MultiCurrencyExt.currencyinfo).Update(currencyInfo2);
          receipt.CuryID = currencyInfo3.CuryID;
          receipt = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.Document).Update(receipt);
        }
        receipt.WMSSingleOrder = new bool?(true);
        receipt.Hold = new bool?(true);
        receipt.Status = "H";
        ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Update(receipt);
        this.Basis.Get<ReceivePutAway.ReceiveTransferMode.Logic>().LinkShipment(receipt, shipment);
        this.Basis.SaveChanges();
        return receipt;
      }

      public virtual PX.Objects.PO.POReceipt CreateEmptyReceiptFrom(PX.Objects.SO.SOOrder order)
      {
        PX.Objects.PO.POReceipt receipt = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.Document).Insert(new PX.Objects.PO.POReceipt()
        {
          ReceiptType = "RX",
          SiteID = order.DestinationSiteID
        });
        if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraphExtension<ReceivePutAway.Host>) this).Base.MultiCurrencyExt.GetCurrencyInfo(order.CuryInfoID);
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXGraphExtension<ReceivePutAway.Host>) this).Base.MultiCurrencyExt.GetCurrencyInfo(receipt.CuryInfoID);
          currencyInfo2.CuryID = currencyInfo1.CuryID;
          currencyInfo2.CuryRateTypeID = currencyInfo1.CuryRateTypeID;
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.MultiCurrencyExt.currencyinfo).Update(currencyInfo2);
          receipt.CuryID = currencyInfo3.CuryID;
          receipt = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<ReceivePutAway.Host>) this).Base.Document).Update(receipt);
        }
        receipt.WMSSingleOrder = new bool?(true);
        receipt.Hold = new bool?(true);
        receipt.Status = "H";
        ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Update(receipt);
        this.Basis.Get<ReceivePutAway.ReceiveTransferMode.Logic>().LinkOrder(receipt, order);
        this.Basis.SaveChanges();
        return receipt;
      }

      public virtual IEnumerable<PX.Objects.SO.SOOrderShipment> GetLinkedDocuments()
      {
        return GraphHelper.RowCast<PX.Objects.SO.SOOrderShipment>((IEnumerable) PXSelectBase<PX.Objects.SO.SOOrderShipment, PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POReceiptToShipmentLink>.On<POReceiptToShipmentLink.FK.SOOrderShipment>>>.Where<KeysRelation<CompositeKey<Field<POReceiptToShipmentLink.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptToShipmentLink.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptToShipmentLink>, PX.Objects.PO.POReceipt, POReceiptToShipmentLink>.SameAsCurrent>>.ReadOnly.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceipt[1]
        {
          this.Basis.Receipt
        }, Array.Empty<object>()));
      }

      public bool TrySelectReceiptToLoad(
        IEnumerable<PX.Objects.PO.POReceipt> potentialReceipts,
        [NotNullWhen(true)] out PX.Objects.PO.POReceipt? selectedReceipt)
      {
        selectedReceipt = (PX.Objects.PO.POReceipt) null;
        if (!potentialReceipts.Any<PX.Objects.PO.POReceipt>())
          return false;
        Func<PX.Objects.PO.POReceipt, bool>[] funcArray = new Func<PX.Objects.PO.POReceipt, bool>[4]
        {
          (Func<PX.Objects.PO.POReceipt, bool>) (r =>
          {
            if (!r.Hold.GetValueOrDefault() || !r.WMSSingleOrder.GetValueOrDefault())
              return false;
            Guid? createdById = r.CreatedByID;
            Guid userId = ((PXGraph) this.Basis.Graph).Accessinfo.UserID;
            return createdById.HasValue && createdById.GetValueOrDefault() == userId;
          }),
          (Func<PX.Objects.PO.POReceipt, bool>) (r =>
          {
            if (r.Hold.GetValueOrDefault() || r.Received.GetValueOrDefault() || r.Released.GetValueOrDefault() || !r.WMSSingleOrder.GetValueOrDefault())
              return false;
            Guid? createdById = r.CreatedByID;
            Guid userId = ((PXGraph) this.Basis.Graph).Accessinfo.UserID;
            return createdById.HasValue && createdById.GetValueOrDefault() == userId;
          }),
          (Func<PX.Objects.PO.POReceipt, bool>) (r =>
          {
            if (!r.Hold.GetValueOrDefault() && !r.Received.GetValueOrDefault() && !r.Released.GetValueOrDefault())
            {
              bool? wmsSingleOrder = r.WMSSingleOrder;
              bool flag = false;
              if (wmsSingleOrder.GetValueOrDefault() == flag & wmsSingleOrder.HasValue)
              {
                Guid? createdById = r.CreatedByID;
                Guid userId = ((PXGraph) this.Basis.Graph).Accessinfo.UserID;
                return createdById.HasValue && createdById.GetValueOrDefault() == userId;
              }
            }
            return false;
          }),
          (Func<PX.Objects.PO.POReceipt, bool>) (r =>
          {
            if (!r.Hold.GetValueOrDefault() && !r.Received.GetValueOrDefault() && !r.Released.GetValueOrDefault())
            {
              bool? wmsSingleOrder = r.WMSSingleOrder;
              bool flag = false;
              if (wmsSingleOrder.GetValueOrDefault() == flag & wmsSingleOrder.HasValue)
              {
                Guid? createdById = r.CreatedByID;
                Guid userId = ((PXGraph) this.Basis.Graph).Accessinfo.UserID;
                return !createdById.HasValue || createdById.GetValueOrDefault() != userId;
              }
            }
            return false;
          })
        };
        foreach (Func<PX.Objects.PO.POReceipt, bool> predicate in funcArray)
        {
          selectedReceipt = potentialReceipts.FirstOrDefault<PX.Objects.PO.POReceipt>(predicate);
          if (selectedReceipt != null)
            return true;
        }
        return false;
      }

      protected virtual bool PotentialItemsExist(PX.Objects.PO.POReceipt receipt)
      {
        int? rowCount = PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTransitLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLine.transferNbr, Equal<INTran.refNbr>>>>>.And<BqlOperand<INTransitLine.transferLineNbr, IBqlInt>.IsEqual<INTran.lineNbr>>>>, FbqlJoins.Inner<INTransitLineStatus>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.transferNbr, Equal<INTransitLine.transferNbr>>>>>.And<BqlOperand<INTransitLineStatus.transferLineNbr, IBqlInt>.IsEqual<INTransitLine.transferLineNbr>>>>, FbqlJoins.Inner<POReceiptToShipmentLink>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.sOShipmentNbr, Equal<POReceiptToShipmentLink.sOShipmentNbr>>>>, And<BqlOperand<INTransitLineStatus.sOShipmentType, IBqlString>.IsEqual<POReceiptToShipmentLink.sOShipmentType>>>, And<BqlOperand<INTran.sOShipmentType, IBqlString>.IsEqual<POReceiptToShipmentLink.sOShipmentType>>>>.And<BqlOperand<INTran.sOShipmentNbr, IBqlString>.IsEqual<POReceiptToShipmentLink.sOShipmentNbr>>>>, FbqlJoins.Inner<INLocationStatusInTransit>.On<BqlOperand<INLocationStatusInTransit.locationID, IBqlInt>.IsEqual<INTransitLine.costSiteID>>>, FbqlJoins.Left<INLotSerialStatusInTransit>.On<BqlOperand<INLotSerialStatusInTransit.locationID, IBqlInt>.IsEqual<INTransitLine.costSiteID>>>, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.origDocType>.IsRelatedTo<INTran.docType>, Field<PX.Objects.PO.POReceiptLine.origRefNbr>.IsRelatedTo<INTran.refNbr>, Field<PX.Objects.PO.POReceiptLine.origLineNbr>.IsRelatedTo<INTran.lineNbr>>.WithTablesOf<INTran, PX.Objects.PO.POReceiptLine>, INTran, PX.Objects.PO.POReceiptLine>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.released, NotEqual<True>>>>>.Or<BqlOperand<PX.Objects.PO.POReceiptLine.iNReleased, IBqlBool>.IsNotEqual<True>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptNbr, IsNull>>>, And<BqlOperand<INTransitLineStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>, And<BqlOperand<INLocationStatusInTransit.qtyOnHand, IBqlDecimal>.IsNotEqual<Zero>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusInTransit.qtyOnHand, IsNull>>>>.Or<BqlOperand<INLotSerialStatusInTransit.qtyOnHand, IBqlDecimal>.IsNotEqual<Zero>>>>, And<BqlOperand<POReceiptToShipmentLink.receiptNbr, IBqlString>.IsEqual<BqlField<POReceiptToShipmentLink.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<POReceiptToShipmentLink.receiptType, IBqlString>.IsEqual<BqlField<POReceiptToShipmentLink.receiptType, IBqlString>.FromCurrent>>>.Aggregate<PX.Data.BQL.Fluent.To<Count>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base, (object[]) new PX.Objects.PO.POReceipt[1]
        {
          receipt
        }, Array.Empty<object>()).RowCount;
        int num = 0;
        return rowCount.GetValueOrDefault() > num & rowCount.HasValue;
      }

      public virtual void LinkShipment(PX.Objects.PO.POReceipt receipt, PX.Objects.SO.SOShipment shipment)
      {
        foreach (PX.Objects.SO.SOOrderShipment soLink in GraphHelper.RowCast<PX.Objects.SO.SOOrderShipment>((IEnumerable) PXSelectBase<PX.Objects.SO.SOOrderShipment, PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<POReceiptToShipmentLink>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptToShipmentLink.sOShipmentType, Equal<PX.Objects.SO.SOOrderShipment.shipmentType>>>>, And<BqlOperand<POReceiptToShipmentLink.sOShipmentNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>, And<BqlOperand<POReceiptToShipmentLink.receiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>.And<BqlOperand<POReceiptToShipmentLink.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptToShipmentLink.receiptNbr, IsNull>>>>.And<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrderShipment.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<PX.Objects.SO.SOOrderShipment.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment>.SameAsCurrent>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.SO.SOShipment[1]
        {
          shipment
        }, Array.Empty<object>())))
        {
          ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Current = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Search<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>((object) receipt.ReceiptType, (object) receipt.ReceiptNbr, Array.Empty<object>()));
          ((PXGraph) this.Basis.Graph).GetExtension<AddTransferDialog>().TryLinkTransferShipment(soLink);
        }
      }

      public virtual void LinkOrder(PX.Objects.PO.POReceipt receipt, PX.Objects.SO.SOOrder order)
      {
        foreach (PXResult<PX.Objects.SO.SOOrderShipment> pxResult in PXSelectBase<PX.Objects.SO.SOOrderShipment, PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<POReceiptToShipmentLink>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptToShipmentLink.sOShipmentType, Equal<PX.Objects.SO.SOOrderShipment.shipmentType>>>>, And<BqlOperand<POReceiptToShipmentLink.sOShipmentNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>, And<BqlOperand<POReceiptToShipmentLink.receiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>.And<BqlOperand<POReceiptToShipmentLink.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptToShipmentLink.receiptNbr, IsNull>>>>.And<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrderShipment.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOOrderShipment.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrderShipment>.SameAsCurrent>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.SO.SOOrder[1]
        {
          order
        }, Array.Empty<object>()))
        {
          ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Current = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Search<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>((object) receipt.ReceiptType, (object) receipt.ReceiptNbr, Array.Empty<object>()));
          ((PXGraph) this.Basis.Graph).GetExtension<AddTransferDialog>().TryLinkTransferShipment(PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(pxResult));
        }
      }
    }

    public sealed class ReceiptState : ReceivePutAway.ReceiptState
    {
      protected override PX.Objects.PO.POReceipt GetByBarcode(string barcode)
      {
        return PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<POReceiptType.transferreceipt>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
        {
          (object) barcode
        }));
      }

      protected virtual Validation Validate(PX.Objects.PO.POReceipt receipt)
      {
        this.Get<ReceivePutAway.ReceiveTransferMode.ReceiptState.Logic>().SiteID = receipt.SiteID;
        if (!this.Get<ReceivePutAway.ReceiveTransferMode.ReceiptState.Logic>().IsNewReceipt)
        {
          bool? released = receipt.Released;
          bool flag = false;
          if ((!(released.GetValueOrDefault() == flag & released.HasValue) || receipt.Hold.GetValueOrDefault() && !receipt.WMSSingleOrder.GetValueOrDefault() ? 1 : (receipt.Received.GetValueOrDefault() ? 1 : 0)) != 0)
            return Validation.Fail("The {0} transfer receipt cannot be processed because it has the {1} status.", new object[2]
            {
              (object) receipt.ReceiptNbr,
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.status>((IBqlTable) receipt)
            });
          if (receipt.ReceiptType != "RX")
            return Validation.Fail("The {0} transfer receipt cannot be processed because it has the {1} type.", new object[2]
            {
              (object) receipt.ReceiptNbr,
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.receiptType>((IBqlTable) receipt)
            });
          int? singleSiteID;
          if (!((ScanComponent<ReceivePutAway>) this).Basis.HasSingleSiteInLines(receipt, out singleSiteID))
            return Validation.Fail("All items in the {0} receipt must be located in the same warehouse.", new object[1]
            {
              (object) receipt.ReceiptNbr
            });
          if (!receipt.SiteID.HasValue)
            this.Get<ReceivePutAway.ReceiveTransferMode.ReceiptState.Logic>().SiteID = singleSiteID;
          if (((ScanComponent<ReceivePutAway>) this).Basis.HasNonStockKit(receipt))
            Validation.Fail("The {0} receipt cannot be processed because it contains a non-stock kit item.", new object[1]
            {
              (object) receipt.ReceiptNbr
            });
        }
        return Validation.Ok;
      }

      protected override void Apply(PX.Objects.PO.POReceipt receipt)
      {
        int? nullable = ((PXSelectBase<POReceivePutAwaySetup>) ((ScanComponent<ReceivePutAway>) this).Basis.Setup).Current.DefaultReceivingLocation.GetValueOrDefault() ? (int?) PX.Objects.IN.INSite.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), this.Get<ReceivePutAway.ReceiveTransferMode.ReceiptState.Logic>().SiteID)?.ReceiptLocationID : new int?();
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = this.Get<ReceivePutAway.ReceiveTransferMode.ReceiptState.Logic>().SiteID;
        ((ScanComponent<ReceivePutAway>) this).Basis.LocationID = ((ScanComponent<ReceivePutAway>) this).Basis.DefaultLocationID = nullable;
        base.Apply(receipt);
      }

      protected override void ClearState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = new int?();
        ((ScanComponent<ReceivePutAway>) this).Basis.DefaultLocationID = new int?();
        base.ClearState();
      }

      protected override void ReportMissing(string barcode)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportError("{0} transfer receipt not found.", new object[1]
        {
          (object) barcode
        });
      }

      protected override void ReportSuccess(PX.Objects.PO.POReceipt receipt)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo(this.Get<ReceivePutAway.ReceiveTransferMode.ReceiptState.Logic>().IsNewReceipt ? "{0} transfer receipt created and ready to be processed." : "{0} receipt loaded and ready to be processed.", new object[1]
        {
          (object) receipt.ReceiptNbr
        });
      }

      protected virtual void SetNextState()
      {
        bool? remove = ((ScanComponent<ReceivePutAway>) this).Basis.Remove;
        bool flag = false;
        if (remove.GetValueOrDefault() == flag & remove.HasValue && !((ScanComponent<ReceivePutAway>) this).Basis.Get<ReceivePutAway.ReceiveTransferMode.Logic>().CanReceive)
        {
          ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo("{0} {1}", new object[2]
          {
            (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<ReceivePutAway>) this).Basis.Info).Current.Message,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.Localize("{0} transfer receipt is received.", new object[1]
            {
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.RefNbr
            })
          });
          ((ScanComponent<ReceivePutAway>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
        }
        else
          ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) this).SetNextState();
      }

      public class Logic : 
        BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public bool IsNewReceipt { get; private set; }

        public int? SiteID { get; set; }
      }

      [PXLocalizable]
      public new abstract class Msg : ReceivePutAway.ReceiptState.Msg
      {
        public const string NotValidDocument = "The {0} transfer receipt cannot be processed because it has the {1} type.";
        public const string ReadyNew = "{0} transfer receipt created and ready to be processed.";
        public new const string Missing = "{0} transfer receipt not found.";
        public new const string InvalidStatus = "The {0} transfer receipt cannot be processed because it has the {1} status.";
      }
    }

    public sealed class TransferShipmentState : 
      BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.EntityState<PX.Objects.SO.SOShipment>
    {
      public virtual string Code => "SHPMNT";

      protected virtual string StatePrompt => "Scan the transfer document.";

      protected virtual PX.Objects.SO.SOShipment GetByBarcode(string barcode)
      {
        return PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
        {
          (object) barcode
        }));
      }

      protected virtual Validation Validate(PX.Objects.SO.SOShipment shipment)
      {
        bool valueOrDefault = ((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode.GetValueOrDefault();
        if (shipment.ShipmentType != "T")
          return Validation.Fail("The {0} transfer document cannot be processed because it has the {1} type.", new object[2]
          {
            (object) shipment.ShipmentNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.SO.SOShipment.shipmentType>((IBqlTable) shipment)
          });
        if (shipment.Status != "C")
          return !valueOrDefault ? Validation.Fail("The transfer receipt cannot be created because the {0} transfer document has not been processed in the source warehouse.", new object[2]
          {
            (object) shipment.ShipmentNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
          }) : Validation.Fail("The {0} transfer document cannot be added because it has not been processed in the source warehouse.", new object[2]
          {
            (object) shipment.ShipmentNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
          });
        if (valueOrDefault && ((ScanComponent<ReceivePutAway>) this).Basis.Receipt != null)
        {
          int? destinationSiteId = shipment.DestinationSiteID;
          int? siteId = ((ScanComponent<ReceivePutAway>) this).Basis.Receipt.SiteID;
          if (!(destinationSiteId.GetValueOrDefault() == siteId.GetValueOrDefault() & destinationSiteId.HasValue == siteId.HasValue))
            return Validation.Fail("The {0} transfer document cannot be added because it has a different destination warehouse.", new object[1]
            {
              (object) shipment.ShipmentNbr
            });
        }
        if (!valueOrDefault)
        {
          List<PX.Objects.PO.POReceipt> list = GraphHelper.RowCast<PX.Objects.PO.POReceipt>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POReceiptToShipmentLink>.On<POReceiptToShipmentLink.FK.Receipt>>>.Where<KeysRelation<CompositeKey<Field<POReceiptToShipmentLink.sOShipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<POReceiptToShipmentLink.sOShipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, POReceiptToShipmentLink>, PX.Objects.SO.SOShipment, POReceiptToShipmentLink>.SameAsCurrent>.Order<PX.Data.BQL.Fluent.By<BqlField<PX.Objects.PO.POReceipt.wMSSingleOrder, IBqlBool>.Desc>>>.ReadOnly.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), (object[]) new PX.Objects.SO.SOShipment[1]
          {
            shipment
          }, Array.Empty<object>())).ToList<PX.Objects.PO.POReceipt>();
          PX.Objects.PO.POReceipt selectedReceipt;
          if (this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().TrySelectReceiptToLoad((IEnumerable<PX.Objects.PO.POReceipt>) list, out selectedReceipt))
          {
            this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().Receipt = selectedReceipt;
            return Validation.Ok;
          }
        }
        if (CanCreateReceiptForShipment(shipment, BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis)))
          return Validation.Ok;
        return !valueOrDefault ? Validation.Fail("The transfer receipt for the {0} document cannot be processed because there is no transfer receipt with a valid status or the document has already been received in full.", new object[2]
        {
          (object) shipment.ShipmentNbr,
          (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
        }) : Validation.Fail("The {0} transfer document cannot be added because it has been received in full.", new object[2]
        {
          (object) shipment.ShipmentNbr,
          (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.SO.SOShipment.status>((IBqlTable) shipment)
        });

        static bool CanCreateReceiptForShipment(PX.Objects.SO.SOShipment shipment, PXGraph graph)
        {
          int? rowCount = PXSelectBase<INTransitLineStatus, PXViewOf<INTransitLineStatus>.BasedOn<SelectFromBase<INTransitLineStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.origDocType, Equal<INDocType.transfer>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.IsEqual<INTransitLineStatus.transferNbr>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.IsEqual<INTransitLineStatus.transferLineNbr>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.released, NotEqual<True>>>>>.Or<BqlOperand<PX.Objects.PO.POReceiptLine.iNReleased, IBqlBool>.IsNotEqual<True>>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.sOShipmentNbr, Equal<BqlField<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<INTransitLineStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsNull>>.Aggregate<PX.Data.BQL.Fluent.To<Count>>>.ReadOnly.Config>.SelectMultiBound(graph, new object[1]
          {
            (object) shipment
          }, Array.Empty<object>()).RowCount;
          int num = 0;
          return rowCount.GetValueOrDefault() > num & rowCount.HasValue;
        }
      }

      protected virtual void Apply(PX.Objects.SO.SOShipment entity)
      {
        ((EntityState<ReceivePutAway, PX.Objects.SO.SOShipment>) this).Apply(entity);
        if (((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode.GetValueOrDefault())
        {
          this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().LinkShipment(((ScanComponent<ReceivePutAway>) this).Basis.Receipt, entity);
          ((ScanComponent<ReceivePutAway>) this).Basis.SaveChanges();
        }
        else
          this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().Shipment = entity;
      }

      protected virtual void ReportMissing(string barcode)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportError("{0} transfer document not found.", new object[1]
        {
          (object) barcode
        });
      }

      protected virtual void ReportSuccess(PX.Objects.SO.SOShipment shipment)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo("{0} transfer document added to transfer receipt.", new object[1]
        {
          (object) shipment.ShipmentNbr
        });
      }

      protected virtual void SetNextState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode = ((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode.GetValueOrDefault() ? new bool?(false) : throw new InvalidOperationException("Shipment state must not participate in transitions");
        ((ScanComponent<ReceivePutAway>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Scan the transfer document.";
        public const string InvalidType = "The {0} transfer document cannot be processed because it has the {1} type.";
        public const string InvalidStatus = "The transfer receipt cannot be created because the {0} transfer document has not been processed in the source warehouse.";
        public const string InvalidStatusAddDoc = "The {0} transfer document cannot be added because it has not been processed in the source warehouse.";
        public const string CantLoadOrCreateReceipt = "The transfer receipt for the {0} document cannot be processed because there is no transfer receipt with a valid status or the document has already been received in full.";
        public const string ReceivedInFull = "The {0} transfer document cannot be added because it has been received in full.";
        public const string LinkedSuccessfully = "{0} transfer document added to transfer receipt.";
        public const string DifferentWarehouse = "The {0} transfer document cannot be added because it has a different destination warehouse.";
        public const string Missing = "{0} transfer document not found.";
      }
    }

    public sealed class TransferOrderState : 
      BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.EntityState<PX.Objects.SO.SOOrder>
    {
      public virtual string Code => "TRORDR";

      protected virtual string StatePrompt => "Scan the transfer order.";

      protected virtual PX.Objects.SO.SOOrder GetByBarcode(string barcode)
      {
        return GraphHelper.RowCast<PX.Objects.SO.SOOrder>((IEnumerable) PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
        {
          (object) barcode
        })).OrderByDescending<PX.Objects.SO.SOOrder, bool>((Func<PX.Objects.SO.SOOrder, bool>) (o => o.Behavior == "TR")).FirstOrDefault<PX.Objects.SO.SOOrder>();
      }

      protected virtual Validation Validate(PX.Objects.SO.SOOrder order)
      {
        bool valueOrDefault = ((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode.GetValueOrDefault();
        if (order.Behavior != "TR")
          return Validation.Fail("The {0} transfer document cannot be processed because it has the {1} type.", new object[2]
          {
            (object) order.OrderNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.SO.SOOrder.orderType>((IBqlTable) order)
          });
        if (valueOrDefault && ((ScanComponent<ReceivePutAway>) this).Basis.Receipt != null)
        {
          int? destinationSiteId = order.DestinationSiteID;
          int? siteId = ((ScanComponent<ReceivePutAway>) this).Basis.Receipt.SiteID;
          if (!(destinationSiteId.GetValueOrDefault() == siteId.GetValueOrDefault() & destinationSiteId.HasValue == siteId.HasValue))
            return Validation.Fail("The {0} transfer document cannot be added because it has a different destination warehouse.", new object[1]
            {
              (object) order.OrderNbr
            });
        }
        List<\u003C\u003Ef__AnonymousType98<PX.Objects.SO.SOShipment, bool, bool, PX.Objects.SO.SOOrderShipment>> list1 = ((IEnumerable<PXResult<PX.Objects.SO.SOShipment>>) PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrderShipment>.On<PX.Objects.SO.SOOrderShipment.FK.Shipment>>, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>, FbqlJoins.Left<INTransitLineStatus>.On<BqlOperand<INTransitLineStatus.sOShipmentNbr, IBqlString>.IsEqual<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>, FbqlJoins.Left<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.origDocType, Equal<INDocType.transfer>>>>, And<BqlOperand<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.origRefNbr, IBqlString>.IsEqual<INTransitLineStatus.transferNbr>>>>.And<BqlOperand<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.origLineNbr, IBqlInt>.IsEqual<INTransitLineStatus.transferLineNbr>>>>>.Where<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>.Aggregate<To<GroupBy<PX.Objects.SO.SOShipment.shipmentNbr>, GroupBy<PX.Objects.SO.SOShipment.shipmentType>, GroupBy<PX.Objects.SO.SOShipment.status>, GroupBy<PX.Objects.SO.SOShipment.destinationSiteID>, GroupBy<PX.Objects.SO.SOShipment.curyInfoID>, Sum<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.notNull>, Count>>>.ReadOnly.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
        {
          (object) order
        }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.SO.SOShipment>>().Select(r =>
        {
          PX.Objects.SO.SOShipment soShipment = ((PXResult) r).GetItem<PX.Objects.SO.SOShipment>();
          int num1 = ((PXResult) r).GetItem<PX.Objects.SO.SOShipment>().Status == "C" ? 1 : 0;
          int? notNull = ((PXResult) r).GetItem<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt>().NotNull;
          int? rowCount = ((PXResult) r).RowCount;
          int num2 = notNull.GetValueOrDefault() == rowCount.GetValueOrDefault() & notNull.HasValue == rowCount.HasValue ? 1 : 0;
          PX.Objects.SO.SOOrderShipment soOrderShipment = ((PXResult) r).GetItem<PX.Objects.SO.SOOrderShipment>();
          return new
          {
            shipment = soShipment,
            completed = num1 != 0,
            fullyReceived = num2 != 0,
            POReceiptLineIsNullAsBoolean = soOrderShipment
          };
        }).ToList();
        if (!list1.Any(sh => sh.completed))
          return !valueOrDefault ? Validation.Fail("The transfer receipt cannot be created because the {0} transfer document has not been processed in the source warehouse.", new object[1]
          {
            (object) order.OrderNbr
          }) : Validation.Fail("The {0} transfer document cannot be added because it has not been processed in the source warehouse.", new object[1]
          {
            (object) order.OrderNbr
          });
        if (!valueOrDefault)
        {
          List<PX.Objects.PO.POReceipt> list2 = GraphHelper.RowCast<PX.Objects.PO.POReceipt>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POReceiptToShipmentLink>.On<POReceiptToShipmentLink.FK.Receipt>>, FbqlJoins.Inner<PX.Objects.SO.SOOrderShipment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<POReceiptToShipmentLink.sOShipmentType>>>>>.And<BqlOperand<PX.Objects.SO.SOOrderShipment.shipmentNbr, IBqlString>.IsEqual<POReceiptToShipmentLink.sOShipmentNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.SO.SOOrderShipment.orderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>.Order<PX.Data.BQL.Fluent.By<BqlField<PX.Objects.PO.POReceipt.wMSSingleOrder, IBqlBool>.Desc>>>.ReadOnly.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), (object[]) new PX.Objects.SO.SOOrder[1]
          {
            order
          }, Array.Empty<object>())).ToList<PX.Objects.PO.POReceipt>();
          PX.Objects.PO.POReceipt selectedReceipt;
          if (this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().TrySelectReceiptToLoad((IEnumerable<PX.Objects.PO.POReceipt>) list2, out selectedReceipt))
          {
            this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().Receipt = selectedReceipt;
            return Validation.Ok;
          }
        }
        if (!list1.Any(sh => CanCreateReceiptFor(sh.shipment, BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis))))
          return !valueOrDefault ? Validation.Fail("The transfer receipt for the {0} document cannot be processed because there is no transfer receipt with a valid status or the document has already been received in full.", new object[1]
          {
            (object) order.OrderNbr
          }) : Validation.Fail("The {0} transfer document cannot be added because it has been received in full.", new object[1]
          {
            (object) order.OrderNbr
          });
        this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().Order = valueOrDefault ? this.Get<ReceivePutAway.ReceiveTransferMode.Logic>().Order : order;
        return Validation.Ok;

        static bool CanCreateReceiptFor(PX.Objects.SO.SOShipment shipment, PXGraph graph)
        {
          int? rowCount = PXSelectBase<INTransitLineStatus, PXViewOf<INTransitLineStatus>.BasedOn<SelectFromBase<INTransitLineStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.origDocType, Equal<INDocType.transfer>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.IsEqual<INTransitLineStatus.transferNbr>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.IsEqual<INTransitLineStatus.transferLineNbr>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.released, NotEqual<True>>>>>.Or<BqlOperand<PX.Objects.PO.POReceiptLine.iNReleased, IBqlBool>.IsNotEqual<True>>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.sOShipmentNbr, Equal<BqlField<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<INTransitLineStatus.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsNull>>.Aggregate<PX.Data.BQL.Fluent.To<Count>>>.ReadOnly.Config>.SelectMultiBound(graph, new object[1]
          {
            (object) shipment
          }, Array.Empty<object>()).RowCount;
          int num = 0;
          return rowCount.GetValueOrDefault() > num & rowCount.HasValue;
        }
      }

      protected virtual void Apply(PX.Objects.SO.SOOrder order)
      {
        ((EntityState<ReceivePutAway, PX.Objects.SO.SOOrder>) this).Apply(order);
        if (!((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode.GetValueOrDefault())
          return;
        ((ScanComponent<ReceivePutAway>) this).Basis.Get<ReceivePutAway.ReceiveTransferMode.Logic>().LinkOrder(((ScanComponent<ReceivePutAway>) this).Basis.Receipt, order);
        ((ScanComponent<ReceivePutAway>) this).Basis.SaveChanges();
      }

      protected virtual void ReportMissing(string barcode)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportError("{0} transfer document not found.", new object[1]
        {
          (object) barcode
        });
      }

      protected virtual void ReportSuccess(PX.Objects.SO.SOOrder order)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo("{0} transfer document added to transfer receipt.", new object[1]
        {
          (object) order.OrderNbr
        });
      }

      protected virtual void SetNextState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode = ((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode.GetValueOrDefault() ? new bool?(false) : throw new InvalidOperationException("Transfer order state must not participate in transitions");
        ((ScanComponent<ReceivePutAway>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Scan the transfer order.";
        public const string NoCompletedShipment = "The transfer receipt cannot be created because the {0} transfer document has not been processed in the source warehouse.";
        public const string NoCompletedShipmentAddDoc = "The {0} transfer document cannot be added because it has not been processed in the source warehouse.";
        public const string ReceivedInFull = "The transfer receipt for the {0} document cannot be processed because there is no transfer receipt with a valid status or the document has already been received in full.";
        public const string ReceivedInFullAddDoc = "The {0} transfer document cannot be added because it has been received in full.";
      }

      [PXHidden]
      [PXProjection(typeof (SelectFrom<PX.Objects.PO.POReceiptLine>))]
      public class POReceiptLineNotNullAsInt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
      {
        [PXDBString(BqlField = typeof (PX.Objects.PO.POReceiptLine.origDocType))]
        public virtual string? OrigDocType { get; set; }

        [PXDBString(BqlField = typeof (PX.Objects.PO.POReceiptLine.origRefNbr))]
        public virtual string? OrigRefNbr { get; set; }

        [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.origLineNbr))]
        public virtual int? OrigLineNbr { get; set; }

        [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceiptLine.released))]
        public virtual bool? Released { get; set; }

        /// <summary>
        /// Sets up when the <see cref="P:PX.Objects.IN.INTran.Released" /> sets up (on <see cref="M:PX.Objects.IN.INDocumentRelease.ReleaseDoc(System.Collections.Generic.List{PX.Objects.IN.INRegister},System.Boolean,System.Boolean,PX.Data.PXQuickProcess.ActionFlow)" />)
        /// </summary>
        [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceiptLine.iNReleased))]
        public virtual bool? INReleased { get; set; }

        /// <summary>
        /// Integer indicating that record is not null if equals 1.
        /// </summary>
        [PXInt]
        [PXDBCalced(typeof (BqlOperand<One, IBqlInt>.When<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsNotNull>.Else<Zero>), typeof (int))]
        public int? NotNull { get; set; }

        public abstract class origDocType : 
          BqlType<IBqlString, string>.Field<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.origDocType>
        {
        }

        public abstract class origRefNbr : 
          BqlType<IBqlString, string>.Field<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.origRefNbr>
        {
        }

        public abstract class origLineNbr : 
          BqlType<IBqlInt, int>.Field<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.origLineNbr>
        {
        }

        public abstract class released : 
          BqlType<IBqlBool, bool>.Field<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.released>
        {
        }

        public abstract class iNReleased : 
          BqlType<IBqlBool, bool>.Field<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.iNReleased>
        {
        }

        public abstract class notNull : 
          BqlType<IBqlInt, int>.Field<ReceivePutAway.ReceiveTransferMode.TransferOrderState.POReceiptLineNotNullAsInt.notNull>
        {
        }
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<ReceivePutAway>) this).Basis.Localize("Confirm receiving {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.Qty,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<ReceivePutAway.ReceiveTransferMode.ConfirmState.Logic>().Confirm();
      }

      public class Logic : 
        BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public ReceivePutAway.ReceiveTransferMode.Logic Mode { get; private set; }

        public virtual void Initialize()
        {
          this.Mode = this.Basis.Get<ReceivePutAway.ReceiveTransferMode.Logic>();
        }

        public virtual FlowStatus Confirm()
        {
          FlowStatus error;
          if (!this.CanReceive(out error))
            return error;
          bool? remove = this.Basis.Remove;
          bool flag = false;
          return remove.GetValueOrDefault() == flag & remove.HasValue ? this.ProcessAdd() : this.ProcessRemove();
        }

        protected virtual bool CanReceive(out FlowStatus error)
        {
          if (!this.Basis.InventoryID.HasValue)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Item not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          if (this.Mode.IsLocationUserInputRequired && !this.Basis.LocationID.HasValue)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Location not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          if (this.Mode.IsLotSerialUserInputRequired && this.Basis.LotSerialNbr == null)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          error = FlowStatus.Ok;
          return true;
        }

        protected virtual FlowStatus ProcessAdd()
        {
          Decimal baseQty = this.Basis.BaseQty;
          if (this.Mode.IsLotSerialUserInputRequired && this.Basis.ReceiveBySingleItem && baseQty != 1M)
            return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
          int? nullable = this.Basis.LocationID;
          if (!nullable.HasValue)
          {
            nullable = this.Basis.DefaultLocationID;
            if (nullable.HasValue)
              this.Basis.LocationID = this.Basis.DefaultLocationID;
          }
          if (!this.Basis.EnsureLocationPrimaryItem(this.Basis.InventoryID, this.Basis.LocationID))
            return FlowStatus.Fail("Selected item is not allowed in this location.", Array.Empty<object>());
          IEnumerable<PX.Objects.PO.POReceiptLineSplit> addedSplits = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.ReceivedForTransfer).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            int? inventoryId1 = s.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = s.SubItemID;
              int? subItemId2 = this.Basis.SubItemID;
              if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
                return string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
            }
            return false;
          }));
          if (!addedSplits.Any<PX.Objects.PO.POReceiptLineSplit>())
            this.TryAddRelatedDocSplits(out addedSplits);
          return this.ProcessAddItems(addedSplits.Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s => string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))));
        }

        protected virtual FlowStatus ProcessAddItems(IEnumerable<PX.Objects.PO.POReceiptLineSplit> itemSplits)
        {
          itemSplits = (IEnumerable<PX.Objects.PO.POReceiptLineSplit>) itemSplits.OrderByDescending<PX.Objects.PO.POReceiptLineSplit, bool>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            int? locationId = s.LocationID;
            int? nullable = this.Basis.LocationID ?? s.LocationID;
            return locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue;
          })).ToArray<PX.Objects.PO.POReceiptLineSplit>();
          PX.Objects.PO.POReceiptLineSplit itemSplit = itemSplits.FirstOrDefault<PX.Objects.PO.POReceiptLineSplit>();
          if (itemSplit == null)
            return !this.Mode.IsLotSerialUserInputRequired || string.IsNullOrEmpty(this.Basis.LotSerialNbr) ? FlowStatus.Fail("The transfer receipt does not have the {0} item. You cannot add this item to the transfer receipt.", new object[1]
            {
              (object) this.Basis.SelectedInventoryItem.InventoryCD
            }) : FlowStatus.Fail("The transfer document does not include the {0} item with the {1} lot or serial number. You cannot add this item to the transfer receipt.", new object[2]
            {
              (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
              (object) this.Basis.LotSerialNbr
            });
          Decimal deltaQty = this.Basis.BaseQty;
          while (!this.Mode.IsLotSerialUserInputRequired || !this.Basis.ReceiveBySingleItem || !itemSplits.Any<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            if (!string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              return false;
            Decimal? receivedQty = s.ReceivedQty;
            Decimal num = (Decimal) 1;
            return receivedQty.GetValueOrDefault() == num & receivedQty.HasValue;
          })))
          {
            deltaQty = this.FulfillDirectly(itemSplits, deltaQty);
            if (!(deltaQty > 0M) || !this.TryAddRelatedDocSplits(out itemSplits))
            {
              if (deltaQty > 0M)
              {
                FlowStatus flowStatus = this.FailOverreceiving(itemSplit);
                return ((FlowStatus) ref flowStatus).WithChangesDiscard;
              }
              this.Basis.ReportInfo("{0} x {1} {2} added to transfer receipt.", new object[3]
              {
                (object) this.Basis.SelectedInventoryItem.InventoryCD,
                (object) this.Basis.Qty,
                (object) this.Basis.UOM
              });
              return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
            }
          }
          FlowStatus flowStatus1 = FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
          flowStatus1 = ((FlowStatus) ref flowStatus1).WithModeReset;
          return ((FlowStatus) ref flowStatus1).WithChangesDiscard;
        }

        protected bool TryAddRelatedDocSplits(out IEnumerable<PX.Objects.PO.POReceiptLineSplit> addedSplits)
        {
          if (!this.Basis.InventoryID.HasValue || !this.Basis.SubItemID.HasValue)
          {
            addedSplits = (IEnumerable<PX.Objects.PO.POReceiptLineSplit>) Array.Empty<PX.Objects.PO.POReceiptLineSplit>();
            return false;
          }
          AddTransferDialog extension = ((PXGraph) this.Basis.Graph).GetExtension<AddTransferDialog>();
          bool flag = false;
          foreach (PX.Objects.SO.SOOrderShipment linkedDocument in this.Basis.Get<ReceivePutAway.ReceiveTransferMode.Logic>().GetLinkedDocuments())
          {
            AddTransferDialog addTransferDialog = extension;
            (string, string)? shipmentKey = new (string, string)?((linkedDocument.ShipmentType, linkedDocument.ShipmentNbr));
            (string, string)? orderKey = new (string, string)?((linkedDocument.OrderType, linkedDocument.OrderNbr));
            (string, string)? transferKey = new (string, string)?();
            int? nullable = this.Basis.InventoryID;
            int num1 = nullable.Value;
            nullable = this.Basis.SubItemID;
            int num2 = nullable.Value;
            (int, int)? itemKey = new (int, int)?((num1, num2));
            if (addTransferDialog.AddTransferDoc(shipmentKey, orderKey, transferKey, itemKey))
            {
              flag = true;
              break;
            }
          }
          addedSplits = ((PXSelectBase) this.Mode.ReceivedForTransfer).Cache.Inserted.Cast<PX.Objects.PO.POReceiptLineSplit>();
          return flag;
        }

        protected virtual Decimal FulfillDirectly(
          IEnumerable<PX.Objects.PO.POReceiptLineSplit> itemSplits,
          Decimal deltaQty)
        {
          Sign sign = Sign.Of(deltaQty);
          Decimal val2 = Math.Abs(deltaQty);
          foreach (PX.Objects.PO.POReceiptLineSplit itemSplit in itemSplits)
          {
            if (!(val2 == 0M))
            {
              Decimal? nullable;
              Decimal val1;
              if (!((Sign) ref sign).IsPlus)
              {
                nullable = itemSplit.ReceivedQty;
                val1 = nullable.Value;
              }
              else
              {
                nullable = itemSplit.Qty;
                Decimal num1 = nullable.Value;
                nullable = itemSplit.ReceivedQty;
                Decimal num2 = nullable.Value;
                val1 = num1 - num2;
              }
              Decimal num3 = Math.Min(val1, val2);
              if (num3 > 0M)
              {
                ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Current = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Graph.transactions).Search<PX.Objects.PO.POReceiptLine.lineNbr>((object) itemSplit.LineNbr, Array.Empty<object>()));
                PX.Objects.PO.POReceiptLineSplit receiptLineSplit = itemSplit;
                nullable = receiptLineSplit.ReceivedQty;
                Decimal num4 = Sign.op_Multiply(num3, sign);
                receiptLineSplit.ReceivedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num4) : new Decimal?();
                if (((Sign) ref sign).IsPlus && this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>() && this.Basis.LocationID.HasValue)
                  itemSplit.LocationID = this.Basis.LocationID;
                ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.ReceivedForTransfer).Update(itemSplit);
                val2 -= num3;
              }
            }
            else
              break;
          }
          return val2;
        }

        protected virtual FlowStatus FailOverreceiving(PX.Objects.PO.POReceiptLineSplit itemSplit)
        {
          return FlowStatus.Fail("The quantity of the {0} item exceeds the item's quantity in the {1} shipment.", new object[2]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.receiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceiptLineSplit[1]
            {
              itemSplit
            }, Array.Empty<object>())).SOShipmentNbr
          });
        }

        protected virtual FlowStatus ProcessRemove()
        {
          if (!this.Basis.LocationID.HasValue && this.Basis.DefaultLocationID.HasValue)
            this.Basis.LocationID = this.Basis.DefaultLocationID;
          // ISSUE: method pointer
          PX.Objects.PO.POReceiptLineSplit[] array = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.ReceivedForTransfer).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
          {
            int? inventoryId1 = r.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = r.SubItemID;
              int? subItemId2 = this.Basis.SubItemID;
              if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
                return string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
            }
            return false;
          })).ToArray<PX.Objects.PO.POReceiptLineSplit>()).Where<PX.Objects.PO.POReceiptLineSplit>(new Func<PX.Objects.PO.POReceiptLineSplit, bool>((object) this, __methodptr(\u003CProcessRemove\u003Eg__IsDeductable\u007C12_1))).ToArray<PX.Objects.PO.POReceiptLineSplit>();
          if (!((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array).Any<PX.Objects.PO.POReceiptLineSplit>())
          {
            FlowStatus flowStatus = FlowStatus.Fail("No items to remove from transfer receipt.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          if (this.FulfillDirectly((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array, Sign.op_Multiply(this.Basis.BaseQty, Sign.Minus)) > 0M)
          {
            FlowStatus flowStatus = FlowStatus.Fail("The received quantity cannot be negative.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithChangesDiscard;
          }
          foreach (PX.Objects.PO.POReceiptLineSplit receiptLineSplit in ((PXSelectBase) this.Graph.splits).Cache.Updated.Cast<PX.Objects.PO.POReceiptLineSplit>().Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            Decimal? receivedQty = s.ReceivedQty;
            Decimal num = 0M;
            return receivedQty.GetValueOrDefault() == num & receivedQty.HasValue;
          })))
          {
            PX.Objects.PO.POReceiptLine poReceiptLine = PX.Objects.PO.POReceiptLine.PK.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), this.Basis.Receipt.ReceiptType, this.Basis.Receipt.ReceiptNbr, receiptLineSplit.LineNbr, (PKFindOptions) 1);
            if (((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) PXParentAttribute.SelectChildren<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>(((PXSelectBase) this.Graph.splits).Cache, poReceiptLine)).All<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
            {
              Decimal? receivedQty = s.ReceivedQty;
              Decimal num = 0M;
              return receivedQty.GetValueOrDefault() == num & receivedQty.HasValue;
            })))
              ((PXSelectBase) this.Graph.transactions).Cache.Delete((object) poReceiptLine);
          }
          this.Basis.ReportInfo("{0} x {1} {2} removed from transfer receipt.", new object[3]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm receiving {0} x {1} {2}.";
        public const string NothingToRemove = "No items to remove from transfer receipt.";
        public const string Underreceiving = "The received quantity cannot be negative.";
        public const string InventoryAdded = "{0} x {1} {2} added to transfer receipt.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from transfer receipt.";
        public const string QtyExceedsTransferReceipt = "The quantity of the {0} item exceeds the item's quantity in the {1} shipment.";
        public const string ItemAbsenceInTransferReceipt = "The transfer receipt does not have the {0} item. You cannot add this item to the transfer receipt.";
        public const string LotSerItemAbsenceInTransferReceipt = "The transfer document does not include the {0} item with the {1} lot or serial number. You cannot add this item to the transfer receipt.";
      }
    }

    public class AlterReleaseReceiptCommand : 
      BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanExtension<ReceivePutAway.ReceiveMode.ReleaseReceiptCommand.Logic>
    {
      [PXOverride]
      public bool get_MatchesMode(Func<bool> base_MatchesMode)
      {
        return base_MatchesMode() || ((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode;
      }

      [PXOverride]
      public string get_InProcess(Func<string> base_InProcess)
      {
        return !(((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode) ? base_InProcess() : "Release of {0} transfer receipt in progress.";
      }

      [PXOverride]
      public string get_Success(Func<string> base_Success)
      {
        return !(((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode) ? base_Success() : "Transfer receipt successfully released.";
      }

      [PXOverride]
      public string get_Fail(Func<string> base_Fail)
      {
        return !(((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode) ? base_Fail() : "Transfer receipt not released.";
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string InProcess = "Release of {0} transfer receipt in progress.";
        public const string Success = "Transfer receipt successfully released.";
        public const string Fail = "Transfer receipt not released.";
      }
    }

    public sealed class AlterConfirmReceiptCommand : 
      BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanExtension<ReceivePutAway.ReceiveMode.ConfirmReceiptCommand.Logic>
    {
      [PXOverride]
      public bool get_MatchesMode(Func<bool> base_MatchesMode)
      {
        return base_MatchesMode() || ((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode;
      }

      [PXOverride]
      public string get_InProcess(Func<string> base_InProcess)
      {
        return !(((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode) ? base_InProcess() : "Confirmation of {0} transfer receipt in progress.";
      }

      [PXOverride]
      public string get_Success(Func<string> base_Success)
      {
        return !(((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode) ? base_Success() : "Transfer receipt successfully received. ";
      }

      [PXOverride]
      public string get_Fail(Func<string> base_Fail)
      {
        return !(((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis).CurrentMode is ReceivePutAway.ReceiveTransferMode) ? base_Fail() : "The transfer receipt cannot be confirmed.";
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string InProcess = "Confirmation of {0} transfer receipt in progress.";
        public const string Success = "Transfer receipt successfully received. ";
        public const string Fail = "The transfer receipt cannot be confirmed.";
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<ReceivePutAway>.SetMode<ReceivePutAway.ReceiveTransferMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "POTRANSFER";

      public virtual string DisplayName => "Receive Transfer";

      private string? RefNbr { get; set; }

      public virtual bool IsPossible
      {
        get
        {
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSReceiving>() ? 1 : 0;
          POReceivePutAwaySetup receivePutAwaySetup = POReceivePutAwaySetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          if (num == 0)
            return false;
          if (receivePutAwaySetup == null)
            return true;
          bool? receiveTransferTab = receivePutAwaySetup.ShowReceiveTransferTab;
          bool flag = false;
          return !(receiveTransferTab.GetValueOrDefault() == flag & receiveTransferTab.HasValue);
        }
      }

      protected virtual bool PrepareRedirect()
      {
        if (((ScanComponent<TForeignBasis>) this).Basis is ReceivePutAway basis && basis.RefNbr != null)
        {
          Validation? nullable = ((ScanMode<ReceivePutAway>) basis.FindMode<ReceivePutAway.ReceiveTransferMode>()).TryValidate<PX.Objects.PO.POReceipt>(basis.Receipt).By<ReceivePutAway.ReceiveTransferMode.ReceiptState>();
          if (nullable.HasValue)
          {
            Validation valueOrDefault = nullable.GetValueOrDefault();
            if (((Validation) ref valueOrDefault).IsError.GetValueOrDefault())
            {
              basis.ReportError(((Validation) ref valueOrDefault).Message, ((Validation) ref valueOrDefault).MessageArgs);
              return false;
            }
          }
          this.RefNbr = basis.RefNbr;
        }
        return true;
      }

      protected virtual void CompleteRedirect()
      {
        if (!(((ScanComponent<TForeignBasis>) this).Basis is ReceivePutAway basis) || !(basis.CurrentMode.Code != "VRTN") || this.RefNbr == null || !basis.TryProcessBy("RNBR", this.RefNbr, (StateSubstitutionRule) 253))
          return;
        basis.SetDefaultState((string) null, Array.Empty<object>());
        this.RefNbr = (string) null;
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Receive Transfer";
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<ReceivePutAway>.Msg
    {
      public const string Description = "Receive Transfer";
      public const string RestQty = "Remaining Qty.";
      public const string LotSerialMissing = "The {0} lot or serial number is not listed in the document.";
      public const string Completed = "{0} transfer receipt is received.";
    }

    [PXUIField(Visible = false)]
    public class ShowReceiveTransfer : 
      PXFieldAttachedTo<ScanHeader>.By<
      #nullable disable
      ReceivePutAway.Host>.AsBool.Named<
      #nullable enable
      ReceivePutAway.ReceiveTransferMode.ShowReceiveTransfer>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(((PXSelectBase<POReceivePutAwaySetup>) this.Base.WMS.Setup).Current.ShowReceiveTransferTab.GetValueOrDefault() && row.Mode == "TRRC");
      }
    }

    public class AddTransferButton : 
      BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanExtension
    {
      public static bool IsActive() => false;

      [PXOverride]
      public ScanMode<ReceivePutAway> DecorateScanMode(
        ScanMode<ReceivePutAway> original,
        Func<ScanMode<ReceivePutAway>, ScanMode<ReceivePutAway>> base_DecorateScanMode)
      {
        ScanMode<ReceivePutAway> scanMode = base_DecorateScanMode(original);
        if (!(scanMode is ReceivePutAway.ReceiveTransferMode mode))
          return scanMode;
        this.InjectReceiveTransferAddTransferCommand(mode);
        return scanMode;
      }

      public virtual void InjectReceiveTransferAddTransferCommand(
        ReceivePutAway.ReceiveTransferMode mode)
      {
        ((MethodInterceptor<ScanMode<ReceivePutAway>, ReceivePutAway>.OfFunc<IEnumerable<ScanCommand<ReceivePutAway>>>.AsAppendable) ((ScanMode<ReceivePutAway>) mode).Intercept.CreateCommands).ByAppend((Func<ReceivePutAway, IEnumerable<ScanCommand<ReceivePutAway>>>) (basis => (IEnumerable<ScanCommand<ReceivePutAway>>) new BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanCommand[1]
        {
          (BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanCommand) new ReceivePutAway.ReceiveTransferMode.AddTransferButton.AddTransferCommand()
        }), new RelativeInject?());
      }

      public sealed class AddTransferCommand : 
        BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanCommand
      {
        public virtual string Code => "ADDTRANSFER";

        public virtual string ButtonName => "scanAddTransfer";

        public virtual string DisplayName => "Add Transfer Document";

        protected virtual bool IsEnabled
        {
          get
          {
            return ((ScanComponent<ReceivePutAway>) this).Basis.DocumentIsEditable && !((ScanComponent<ReceivePutAway>) this).Basis.AddTransferMode.GetValueOrDefault();
          }
        }

        protected virtual bool Process()
        {
          this.Get<ReceivePutAway.ReceiveTransferMode.AddTransferButton.AddTransferCommand.Logic>().AddTransfer();
          return true;
        }

        public class Logic : 
          BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanExtension
        {
          public virtual void AddTransfer()
          {
            this.Basis.CurrentMode.Reset(false);
            this.Basis.AddTransferMode = new bool?(true);
            this.Basis.SetScanState<ReceivePutAway.ReceiveTransferMode.TransferShipmentState>((string) null, Array.Empty<object>());
          }

          protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
          {
            string buttonName1 = ((ScanCommand<ReceivePutAway>) new BarcodeQtySupport<ReceivePutAway, ReceivePutAway.Host>.SetQtyCommand()).ButtonName;
            string buttonName2 = ((ScanCommand<ReceivePutAway>) new ReceivePutAway.ReceiveTransferMode.AddTransferButton.AddTransferCommand()).ButtonName;
            if (!((OrderedDictionary) ((PXGraph) this.Graph).Actions).Contains((object) buttonName1) || !((OrderedDictionary) ((PXGraph) this.Graph).Actions).Contains((object) buttonName2))
              return;
            ((PXGraph) this.Graph).Actions.Move(buttonName1, buttonName2, true);
          }
        }

        [PXLocalizable]
        public abstract class Msg
        {
          public const string DisplayName = "Add Transfer Document";
        }
      }
    }
  }

  public sealed class ReturnMode : 
    BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.ScanMode
  {
    public const 
    #nullable disable
    string Value = "VRTN";

    public ReceivePutAway.ReturnMode.Logic Body => this.Get<ReceivePutAway.ReturnMode.Logic>();

    public virtual string Code => "VRTN";

    public virtual string Description => "Return";

    protected virtual IEnumerable<ScanState<ReceivePutAway>> CreateStates()
    {
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReturnMode.ReturnState();
      yield return (ScanState<ReceivePutAway>) ((MethodInterceptor<EntityState<ReceivePutAway, INLocation>, ReceivePutAway>.OfPredicate) ((EntityState<ReceivePutAway, INLocation>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState()).Intercept.IsStateSkippable).ByDisjoin((Func<ReceivePutAway, bool>) (basis => !basis.Get<ReceivePutAway.ReturnMode.Logic>().PromptLocationForEveryLine && basis.LocationID.HasValue), false, new RelativeInject?());
      yield return (ScanState<ReceivePutAway>) ((MethodInterceptor<EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>.AsAppendable) ((EntityState<ReceivePutAway, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState()
      {
        AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.VPN)
      }).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((basis, barcode) => !basis.Get<ReceivePutAway.ReturnMode.Logic>().IsSingleLocation && basis.TryProcessBy<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Done) : AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Skipped)), new RelativeInject?());
      yield return (ScanState<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReturnMode.ConfirmState();
      yield return (ScanState<ReceivePutAway>) new ReceivePutAway.ReturnMode.CommandOrReturnOnlyState();
    }

    protected virtual IEnumerable<ScanTransition<ReceivePutAway>> CreateTransitions()
    {
      return ((ScanMode<ReceivePutAway>) this).StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<ReceivePutAway.ReturnMode.ReturnState>().NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>((Action<ReceivePutAway>) null)));
    }

    protected virtual IEnumerable<ScanCommand<ReceivePutAway>> CreateCommands()
    {
      yield return (ScanCommand<ReceivePutAway>) new WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.RemoveCommand();
      yield return (ScanCommand<ReceivePutAway>) new BarcodeQtySupport<ReceivePutAway, ReceivePutAway.Host>.SetQtyCommand();
      yield return (ScanCommand<ReceivePutAway>) new ReceivePutAway.ReturnMode.ReleaseReturnCommand();
    }

    protected virtual IEnumerable<ScanRedirect<ReceivePutAway>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<ReceivePutAway>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<ReceivePutAway>) this).ResetMode(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<ReceivePutAway.ReturnMode.ReturnState>(fullReset && !((ScanMode<ReceivePutAway>) this).Basis.IsWithinReset);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>(fullReset || this.Body.PromptLocationForEveryLine);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>(fullReset);
      ((ScanMode<ReceivePutAway>) this).Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ReceivePutAway.ReturnMode.value>
    {
      public value()
        : base("VRTN")
      {
      }
    }

    public class Logic : BarcodeDrivenStateMachine<
    #nullable enable
    ReceivePutAway, ReceivePutAway.Host>.ScanExtension
    {
      public 
      #nullable disable
      FbqlSelect<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>, PX.Objects.PO.POReceiptLineSplit>.View Returned;
      public FbqlSelect<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLineSplit.FK.ReceiptLine>>>, PX.Objects.PO.POReceiptLineSplit>.View ReturnedNotZero;
      public PXAction<ScanHeader> ReviewReturn;

      public virtual bool CanReturn
      {
        get
        {
          return ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Returned).SelectMain(Array.Empty<object>())).Any<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (s =>
          {
            Decimal? receivedQty = s.ReceivedQty;
            Decimal? qty = s.Qty;
            return receivedQty.GetValueOrDefault() < qty.GetValueOrDefault() & receivedQty.HasValue & qty.HasValue;
          }));
        }
      }

      public virtual IEnumerable returned()
      {
        return (IEnumerable) this.Basis.SortedResult((IEnumerable) this.Basis.GetSplits(this.Basis.Receipt, true));
      }

      public virtual IEnumerable returnedNotZero()
      {
        return (IEnumerable) this.Basis.SortedResult((IEnumerable) this.Basis.GetSplits(this.Basis.Receipt, true).Where<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>>((Func<PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>, bool>) (s =>
        {
          Decimal? receivedQty = PXResult<PX.Objects.PO.POReceiptLineSplit, PX.Objects.PO.POReceiptLine>.op_Implicit(s).ReceivedQty;
          Decimal num = 0M;
          return receivedQty.GetValueOrDefault() > num & receivedQty.HasValue;
        })));
      }

      [PXButton(CommitChanges = true)]
      [PXUIField(DisplayName = "Review")]
      protected virtual IEnumerable reviewReturn(PXAdapter adapter) => adapter.Get();

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
      {
        ((PXAction) this.ReviewReturn).SetVisible(((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base).IsMobile && e.Row?.Mode == "VRTN");
      }

      public bool PromptLocationForEveryLine
      {
        get
        {
          return ((PXSelectBase<POReceivePutAwaySetup>) this.Basis.Setup).Current.RequestLocationForEachItemInReturn.GetValueOrDefault();
        }
      }

      public virtual bool IsSingleLocation
      {
        get
        {
          return ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Returned).SelectMain(Array.Empty<object>())).GroupBy<PX.Objects.PO.POReceiptLineSplit, int?>((Func<PX.Objects.PO.POReceiptLineSplit, int?>) (s => s.LocationID)).Count<IGrouping<int?, PX.Objects.PO.POReceiptLineSplit>>() < 2;
        }
      }
    }

    public sealed class ReturnState : ReceivePutAway.ReceiptState
    {
      private int? siteID;

      protected override string StatePrompt => "Scan the return number.";

      protected override PX.Objects.PO.POReceipt GetByBarcode(string barcode)
      {
        return PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.PO.POReceipt.vendorID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptNbr, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<POReceiptType.poreturn>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AP.Vendor, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) ((ScanComponent<ReceivePutAway>) this).Basis), new object[1]
        {
          (object) barcode
        }));
      }

      protected virtual Validation Validate(PX.Objects.PO.POReceipt receipt)
      {
        if (receipt.Released.GetValueOrDefault() || receipt.Hold.GetValueOrDefault())
          return Validation.Fail("The {0} return cannot be processed because it has the {1} status.", new object[2]
          {
            (object) receipt.ReceiptNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.status>((IBqlTable) receipt)
          });
        if (receipt.ReceiptType != "RN")
          return Validation.Fail("The {0} return cannot be processed because it has the {1} type.", new object[2]
          {
            (object) receipt.ReceiptNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.receiptType>((IBqlTable) receipt)
          });
        if (receipt.POType == "DP")
          return Validation.Fail("The {0} return cannot be processed because it has the {1} order type.", new object[2]
          {
            (object) receipt.ReceiptNbr,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<PX.Objects.PO.POReceipt.pOType>((IBqlTable) receipt)
          });
        int? singleSiteID;
        if (!((ScanComponent<ReceivePutAway>) this).Basis.HasSingleSiteInLines(receipt, out singleSiteID))
          return Validation.Fail("All items in the {0} return must be located in the same warehouse.", new object[1]
          {
            (object) receipt.ReceiptNbr
          });
        this.siteID = receipt.SiteID.HasValue ? receipt.SiteID : singleSiteID;
        if (((ScanComponent<ReceivePutAway>) this).Basis.HasNonStockKit(receipt))
          Validation.Fail("The {0} return cannot be processed because it contains a non-stock kit item.", new object[1]
          {
            (object) receipt.ReceiptNbr
          });
        return Validation.Ok;
      }

      protected override void Apply(PX.Objects.PO.POReceipt receipt)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = this.siteID;
        base.Apply(receipt);
      }

      protected override void ClearState()
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.SiteID = new int?();
        base.ClearState();
      }

      protected override void ReportSuccess(PX.Objects.PO.POReceipt receipt)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo("{0} return loaded and ready to be processed.", new object[1]
        {
          (object) receipt.ReceiptNbr
        });
      }

      protected override void ReportMissing(string barcode)
      {
        ((ScanComponent<ReceivePutAway>) this).Basis.ReportError("{0} return not found.", new object[1]
        {
          (object) barcode
        });
      }

      protected virtual void SetNextState()
      {
        bool? remove = ((ScanComponent<ReceivePutAway>) this).Basis.Remove;
        bool flag = false;
        if (remove.GetValueOrDefault() == flag & remove.HasValue && !this.Get<ReceivePutAway.ReturnMode.Logic>().CanReturn)
        {
          ((ScanComponent<ReceivePutAway>) this).Basis.ReportInfo("{0} {1}", new object[2]
          {
            (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<ReceivePutAway>) this).Basis.Info).Current.Message,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.Localize("{0} return processed.", new object[1]
            {
              (object) ((ScanComponent<ReceivePutAway>) this).Basis.RefNbr
            })
          });
          ((ScanComponent<ReceivePutAway>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
        }
        else
          ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) this).SetNextState();
      }

      [PXLocalizable]
      public new abstract class Msg
      {
        public const string Prompt = "Scan the return number.";
        public const string Ready = "{0} return loaded and ready to be processed.";
        public const string Missing = "{0} return not found.";
        public const string InvalidStatus = "The {0} return cannot be processed because it has the {1} status.";
        public const string InvalidType = "The {0} return cannot be processed because it has the {1} type.";
        public const string InvalidOrderType = "The {0} return cannot be processed because it has the {1} order type.";
        public const string MultiSites = "All items in the {0} return must be located in the same warehouse.";
        public const string HasNonStockKit = "The {0} return cannot be processed because it contains a non-stock kit item.";
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.ConfirmationState
    {
      public virtual 
      #nullable disable
      string Prompt
      {
        get
        {
          return ((ScanComponent<ReceivePutAway>) this).Basis.Localize("Confirm returning {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.Qty,
            (object) ((ScanComponent<ReceivePutAway>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<ReceivePutAway.ReturnMode.ConfirmState.Logic>().Confirm();
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public 
        #nullable disable
        ReceivePutAway.ReturnMode.Logic Mode { get; private set; }

        public virtual void Initialize()
        {
          this.Mode = this.Basis.Get<ReceivePutAway.ReturnMode.Logic>();
        }

        public virtual FlowStatus Confirm()
        {
          FlowStatus error;
          if (!this.CanReturn(out error))
            return error;
          bool? remove = this.Basis.Remove;
          bool flag = false;
          return remove.GetValueOrDefault() == flag & remove.HasValue ? this.ProcessAdd() : this.ProcessRemove();
        }

        protected virtual bool CanReturn(out FlowStatus error)
        {
          if (!this.Basis.InventoryID.HasValue)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Item not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          if (this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LocationState>() && !this.Basis.LocationID.HasValue)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Location not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          if (this.Basis.HasActive<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.LotSerialState>() && this.Basis.LotSerialNbr == null)
          {
            ref FlowStatus local = ref error;
            FlowStatus flowStatus = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
            FlowStatus withModeReset = ((FlowStatus) ref flowStatus).WithModeReset;
            local = withModeReset;
            return false;
          }
          error = FlowStatus.Ok;
          return true;
        }

        protected virtual FlowStatus ProcessAdd()
        {
          PX.Objects.PO.POReceiptLineSplit[] array = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Returned).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
          {
            int? inventoryId1 = r.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = r.SubItemID;
              int? subItemId2 = this.Basis.SubItemID;
              if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              {
                int? locationId = r.LocationID;
                int? nullable1 = this.Basis.LocationID ?? r.LocationID;
                if (locationId.GetValueOrDefault() == nullable1.GetValueOrDefault() & locationId.HasValue == nullable1.HasValue)
                {
                  Decimal? qty = r.Qty;
                  Decimal? receivedQty = r.ReceivedQty;
                  Decimal? nullable2 = qty.HasValue & receivedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - receivedQty.GetValueOrDefault()) : new Decimal?();
                  Decimal num = 0M;
                  return nullable2.GetValueOrDefault() > num & nullable2.HasValue;
                }
              }
            }
            return false;
          })).ToArray<PX.Objects.PO.POReceiptLineSplit>();
          Decimal baseQty = this.Basis.BaseQty;
          if (((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array).Sum<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, Decimal>) (s =>
          {
            Decimal? nullable = s.Qty;
            Decimal num1 = nullable.Value;
            nullable = s.ReceivedQty;
            Decimal num2 = nullable.Value;
            return num1 - num2;
          })) < baseQty)
            return FlowStatus.Fail("The returned quantity cannot be greater than the expected quantity.", Array.Empty<object>());
          Decimal num3 = baseQty;
          foreach (PX.Objects.PO.POReceiptLineSplit receiptLineSplit1 in array)
          {
            Decimal val1 = num3;
            Decimal? nullable = receiptLineSplit1.Qty;
            Decimal num4 = nullable.Value;
            nullable = receiptLineSplit1.ReceivedQty;
            Decimal num5 = nullable.Value;
            Decimal val2 = num4 - num5;
            Decimal num6 = Math.Min(val1, val2);
            PX.Objects.PO.POReceiptLineSplit receiptLineSplit2 = receiptLineSplit1;
            nullable = receiptLineSplit2.ReceivedQty;
            Decimal num7 = num6;
            receiptLineSplit2.ReceivedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num7) : new Decimal?();
            ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Returned).Update(receiptLineSplit1);
            num3 -= num6;
            if (num3 == 0M)
              break;
          }
          this.Basis.ReportInfo("{0} x {1} {2} added to return.", new object[3]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }

        protected virtual FlowStatus ProcessRemove()
        {
          PX.Objects.PO.POReceiptLineSplit[] array = ((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Returned).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, bool>) (r =>
          {
            int? inventoryId1 = r.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = r.SubItemID;
              int? subItemId2 = this.Basis.SubItemID;
              if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              {
                int? locationId = r.LocationID;
                int? nullable = this.Basis.LocationID ?? r.LocationID;
                if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
                {
                  Decimal? receivedQty = r.ReceivedQty;
                  Decimal num = 0M;
                  return receivedQty.GetValueOrDefault() > num & receivedQty.HasValue;
                }
              }
            }
            return false;
          })).ToArray<PX.Objects.PO.POReceiptLineSplit>();
          if (!((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array).Any<PX.Objects.PO.POReceiptLineSplit>())
          {
            FlowStatus flowStatus = FlowStatus.Fail("No items to remove from return.", Array.Empty<object>());
            return ((FlowStatus) ref flowStatus).WithModeReset;
          }
          Decimal baseQty = this.Basis.BaseQty;
          if (((IEnumerable<PX.Objects.PO.POReceiptLineSplit>) array).Sum<PX.Objects.PO.POReceiptLineSplit>((Func<PX.Objects.PO.POReceiptLineSplit, Decimal>) (s => s.ReceivedQty.Value)) - baseQty < 0M)
            return FlowStatus.Fail("The returned quantity cannot be negative.", Array.Empty<object>());
          Decimal num1 = baseQty;
          foreach (PX.Objects.PO.POReceiptLineSplit receiptLineSplit1 in array)
          {
            Decimal val1 = num1;
            Decimal? receivedQty = receiptLineSplit1.ReceivedQty;
            Decimal valueOrDefault = receivedQty.GetValueOrDefault();
            Decimal num2 = Math.Min(val1, valueOrDefault);
            PX.Objects.PO.POReceiptLineSplit receiptLineSplit2 = receiptLineSplit1;
            receivedQty = receiptLineSplit2.ReceivedQty;
            Decimal num3 = num2;
            receiptLineSplit2.ReceivedQty = receivedQty.HasValue ? new Decimal?(receivedQty.GetValueOrDefault() - num3) : new Decimal?();
            ((PXSelectBase<PX.Objects.PO.POReceiptLineSplit>) this.Mode.Returned).Update(receiptLineSplit1);
            num1 -= num2;
            if (num1 == 0M)
              break;
          }
          this.Basis.ReportInfo("{0} x {1} {2} removed from return.", new object[3]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }

        [PXOverride]
        public ScanState<ReceivePutAway> DecorateScanState(
          ScanState<ReceivePutAway> original,
          Func<ScanState<ReceivePutAway>, ScanState<ReceivePutAway>> base_DecorateScanState)
        {
          ScanState<ReceivePutAway> scanState = base_DecorateScanState(original);
          if (!(scanState is ReceivePutAway.ReturnMode.ReturnState returnState))
            return scanState;
          this.InjectReturnTrySwitchToReceiveMode(returnState);
          this.InjectReturnTrySwitchToTransferMode(returnState);
          return scanState;
        }

        public virtual void InjectReturnTrySwitchToReceiveMode(
          ReceivePutAway.ReturnMode.ReturnState returnState)
        {
          ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) returnState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) => AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(basis.ProcessByMode<ReceivePutAway.ReceiveMode>(barcode))), new RelativeInject?());
        }

        public virtual void InjectReturnTrySwitchToTransferMode(
          ReceivePutAway.ReturnMode.ReturnState returnState)
        {
          ((MethodInterceptor<EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>, ReceivePutAway>.OfFunc<string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>.AsAppendable) ((EntityState<ReceivePutAway, PX.Objects.PO.POReceipt>) returnState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.PO.POReceipt>, string, AbsenceHandling.Of<PX.Objects.PO.POReceipt>>) ((basis, barcode) => AbsenceHandling.Of<PX.Objects.PO.POReceipt>.op_Implicit(basis.ProcessByMode<ReceivePutAway.ReceiveTransferMode>(barcode))), new RelativeInject?());
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm returning {0} x {1} {2}.";
        public const string NothingToRemove = "No items to remove from return.";
        public const string Overreturning = "The returned quantity cannot be greater than the expected quantity.";
        public const string Underreturning = "The returned quantity cannot be negative.";
        public const string InventoryAdded = "{0} x {1} {2} added to return.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from return.";
      }
    }

    public sealed class CommandOrReturnOnlyState : CommandOnlyStateBase<ReceivePutAway>
    {
      public virtual void MoveToNextState()
      {
      }

      public virtual string Prompt => "Use any command or scan the next return number to continue.";

      public virtual bool Process(string barcode)
      {
        if (((ScanComponent<ReceivePutAway>) this).Basis.TryProcessBy<ReceivePutAway.ReturnMode.ReturnState>(barcode, (StateSubstitutionRule) 0))
        {
          ((ScanComponent<ReceivePutAway>) this).Basis.Clear<ReceivePutAway.ReturnMode.ReturnState>(true);
          ((ScanComponent<ReceivePutAway>) this).Basis.Reset(false);
          ((ScanComponent<ReceivePutAway>) this).Basis.SetScanState<ReceivePutAway.ReturnMode.ReturnState>((string) null, Array.Empty<object>());
          ((ScanState<ReceivePutAway>) ((ScanComponent<ReceivePutAway>) this).Basis.CurrentMode.FindState<ReceivePutAway.ReturnMode.ReturnState>(false)).Process(barcode);
          return true;
        }
        ((ScanComponent<ReceivePutAway>) this).Basis.Reporter.Error("Only commands or a return number can be used to continue.", Array.Empty<object>());
        return false;
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string UseCommandOrReturnToContinue = "Use any command or scan the next return number to continue.";
        public const string OnlyCommandsAndReturnsAreAllowed = "Only commands or a return number can be used to continue.";
      }
    }

    public sealed class ReleaseReturnCommand : 
      BarcodeDrivenStateMachine<
      #nullable enable
      ReceivePutAway, ReceivePutAway.Host>.ScanCommand
    {
      public virtual 
      #nullable disable
      string Code => "RELEASE";

      public virtual string ButtonName => "scanReleaseReturn";

      public virtual string DisplayName => "Release Return";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<ReceivePutAway>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        this.Get<ReceivePutAway.ReturnMode.ReleaseReturnCommand.Logic>().ReleaseReturn();
        return true;
      }

      public class Logic : 
        BarcodeDrivenStateMachine<
        #nullable enable
        ReceivePutAway, ReceivePutAway.Host>.ScanExtension
      {
        public virtual void ReleaseReturn()
        {
          this.Basis.ApplyLinesQtyChanges(false);
          this.Basis.SaveChanges();
          this.Basis.Reset(false);
          this.Basis.Clear<WarehouseManagementSystem<ReceivePutAway, ReceivePutAway.Host>.InventoryItemState>(true);
          this.Basis.WaitFor<PX.Objects.PO.POReceipt>((Action<ReceivePutAway, PX.Objects.PO.POReceipt>) ((basis, doc) => ReceivePutAway.ReturnMode.ReleaseReturnCommand.Logic.ReleaseReceiptImpl(doc))).WithDescription("Release of {0} return in progress.", new object[1]
          {
            (object) this.Basis.RefNbr
          }).ActualizeDataBy((Func<ReceivePutAway, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceipt>) ((basis, doc) => (PX.Objects.PO.POReceipt) PrimaryKeyOf<PX.Objects.PO.POReceipt>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>>.Find(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) basis), (TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>) doc, (PKFindOptions) 0))).OnSuccess((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.PO.POReceipt>.ISuccessProcessor>) (x => x.Say("Return successfully released.", Array.Empty<object>()).ChangeStateTo<ReceivePutAway.ReturnMode.ReturnState>())).OnFail((Action<ScanLongRunAwaiter<ReceivePutAway, PX.Objects.PO.POReceipt>.IResultProcessor>) (x => x.Say("Return not released.", Array.Empty<object>()))).BeginAwait(this.Basis.Receipt);
        }

        private static void ReleaseReceiptImpl(
        #nullable disable
        PX.Objects.PO.POReceipt receipt)
        {
          if (receipt.Hold.GetValueOrDefault())
          {
            POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
            ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Current = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Search<PX.Objects.PO.POReceipt.receiptNbr>((object) receipt.ReceiptNbr, new object[1]
            {
              (object) receipt.ReceiptType
            }));
            ((PXAction) instance.releaseFromHold).Press();
            receipt = ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Current;
          }
          POReleaseReceipt.ReleaseDoc(new List<PX.Objects.PO.POReceipt>()
          {
            receipt
          }, false);
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Release Return";
        public const string InProcess = "Release of {0} return in progress.";
        public const string Success = "Return successfully released.";
        public const string Fail = "Return not released.";
      }
    }

    public sealed class RedirectFrom<TForeignBasis> : 
      PX.BarcodeProcessing.RedirectFrom<TForeignBasis>.To<ReceivePutAway>.SetMode<ReceivePutAway.ReturnMode>
      where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
    {
      public virtual string Code => "PORETURN";

      public virtual string DisplayName => "PO Return";

      public virtual bool IsPossible
      {
        get
        {
          int num = PXAccess.FeatureInstalled<FeaturesSet.wMSReceiving>() ? 1 : 0;
          POReceivePutAwaySetup receivePutAwaySetup = POReceivePutAwaySetup.PK.Find(((ScanComponent<TForeignBasis>) this).Basis.Graph, ((ScanComponent<TForeignBasis>) this).Basis.Graph.Accessinfo.BranchID);
          return num != 0 && receivePutAwaySetup != null && receivePutAwaySetup.ShowReturningTab.GetValueOrDefault();
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "PO Return";
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<
    #nullable enable
    ReceivePutAway>.Msg
    {
      public const 
      #nullable disable
      string Description = "Return";
      public const string Completed = "{0} return processed.";
    }

    [PXUIField(Visible = false)]
    public class ShowReturn : 
      PXFieldAttachedTo<ScanHeader>.By<ReceivePutAway.Host>.AsBool.Named<ReceivePutAway.ReturnMode.ShowReturn>
    {
      public override bool? GetValue(ScanHeader row)
      {
        return new bool?(((PXSelectBase<POReceivePutAwaySetup>) this.Base.WMS.Setup).Current.ShowReturningTab.GetValueOrDefault() && row.Mode == "VRTN");
      }
    }
  }
}
