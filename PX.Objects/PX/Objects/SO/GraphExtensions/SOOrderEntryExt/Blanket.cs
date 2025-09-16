// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.Blanket
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease.Exceptions;
using PX.Objects.PM;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.DAC.Unbound;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(null)]
public abstract class Blanket : PXGraphExtension<
#nullable disable
SOOrderEntry>
{
  public FbqlSelect<SelectFromBase<SOBlanketOrderLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOBlanketOrderLink.blanketType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  SOBlanketOrderLink.blanketNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  SOBlanketOrderLink>.View BlanketOrderChildrenList;
  public PXViewOf<SOBlanketOrderDisplayLink>.BasedOn<SelectFromBase<SOBlanketOrderDisplayLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOBlanketOrderDisplayLink.blanketType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  SOBlanketOrderDisplayLink.blanketNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>.ReadOnly BlanketOrderChildrenDisplayList;
  public 
  #nullable disable
  FbqlSelect<SelectFromBase<BlanketSOAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  BlanketSOAdjust.adjdOrderType, 
  #nullable disable
  Equal<P.AsString.ASCII>>>>, And<BqlOperand<
  #nullable enable
  BlanketSOAdjust.adjdOrderNbr, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, And<BqlOperand<
  #nullable enable
  BlanketSOAdjust.curyAdjdAmt, IBqlDecimal>.IsGreater<
  #nullable disable
  decimal0>>>>.And<BqlOperand<
  #nullable enable
  BlanketSOAdjust.voided, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, BlanketSOAdjust>.View BlanketAdjustments;
  [PXCopyPasteHiddenView]
  [PXVirtualDAC]
  public PXSelect<OpenBlanketSOLineSplit> BlanketSplits;
  public PXFilter<BlanketSOOverrideTaxZoneFilter> BlanketTaxZoneOverrideFilter;
  public PXAction<PX.Objects.SO.SOOrder> createChildOrders;
  public PXAction<PX.Objects.SO.SOOrder> processExpiredOrder;
  public PXAction<PX.Objects.SO.SOOrder> viewChildOrder;
  public PXAction<PX.Objects.SO.SOOrder> printBlanket;
  public PXAction<PX.Objects.SO.SOOrder> emailBlanket;
  public PXAction<PX.Objects.SO.SOOrder> addBlanketLine;
  public PXAction<PX.Objects.SO.SOOrder> addBlanketLineOK;
  private bool _updateLineCustomerLocation;
  public PXAction<PX.Objects.SO.SOOrder> overrideBlanketTaxZone;
  private SOOrderEntry _graphForReturnReceivedAllocationsToBlanket;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this.Base).RowUpdated;
    Blanket blanket = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) blanket, __vmethodptr(blanket, LateSOOrderUpdated));
    rowUpdated.AddHandler<PX.Objects.SO.SOOrder>(pxRowUpdated);
  }

  [PXMergeAttributes]
  [PXDefault(typeof (IIf<Where<PX.Objects.SO.SOLine.behavior, Equal<SOBehavior.bL>>, True, False>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.automaticDiscountsDisabled> e)
  {
  }

  protected virtual IEnumerable blanketOrderChildrenDisplayList()
  {
    PXView pxView = new PXView((PXGraph) this.Base, true, PXView.View.BqlSelect);
    PXViewOf<SOBlanketOrderMiscLink>.BasedOn<SelectFromBase<SOBlanketOrderMiscLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOBlanketOrderMiscLink.blanketType, Equal<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<SOBlanketOrderMiscLink.blanketNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>.ReadOnly readOnly = new PXViewOf<SOBlanketOrderMiscLink>.BasedOn<SelectFromBase<SOBlanketOrderMiscLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOBlanketOrderMiscLink.blanketType, Equal<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<SOBlanketOrderMiscLink.blanketNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>.ReadOnly((PXGraph) this.Base);
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    object[] objArray = new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current
    };
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref num2;
    int num4 = num1;
    ref int local2 = ref num3;
    List<SOBlanketOrderDisplayLink> list = GraphHelper.RowCast<SOBlanketOrderDisplayLink>((IEnumerable) pxView.Select(objArray, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, num4, ref local2)).ToList<SOBlanketOrderDisplayLink>();
    int num5 = 0;
    int num6 = 0;
    List<SOBlanketOrderDisplayLink> miscOrders = GraphHelper.RowCast<SOBlanketOrderDisplayLink>((IEnumerable) ((PXSelectBase) readOnly).View.Select(new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current
    }, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num5, num1, ref num6)).ToList<SOBlanketOrderDisplayLink>();
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = true;
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) list.Where<SOBlanketOrderDisplayLink>((Func<SOBlanketOrderDisplayLink, bool>) (o => o.ShipmentNbr != null || !miscOrders.Any<SOBlanketOrderDisplayLink>((Func<SOBlanketOrderDisplayLink, bool>) (mo => mo.OrderType == o.OrderType && mo.OrderNbr == o.OrderNbr)))));
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) miscOrders);
    return (IEnumerable) pxDelegateResult;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CreateChildOrders(PXAdapter adapter, [PXDate] DateTime? schedOrderDate)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    Blanket.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new Blanket.\u003C\u003Ec__DisplayClass12_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.schedOrderDate = schedOrderDate;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.massProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.list = adapter.Get<PX.Objects.SO.SOOrder>().ToList<PX.Objects.SO.SOOrder>();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass120, __methodptr(\u003CCreateChildOrders\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass120.list;
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.RecalcUnbilledTax" />
  [PXOverride]
  public virtual void RecalcUnbilledTax(Action base_RecalcUnbilledTax)
  {
    IEnumerable<\u003C\u003Ef__AnonymousType100<string, string>> datas = ((IEnumerable<PX.Objects.SO.SOLine>) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).SelectMain(Array.Empty<object>())).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (o => o.BlanketType != null && o.BlanketNbr != null)).Select(o => new
    {
      BlanketType = o.BlanketType,
      BlanketNbr = o.BlanketNbr
    }).Distinct();
    Lazy<SOOrderEntry> lazy = new Lazy<SOOrderEntry>((Func<SOOrderEntry>) (() => PXGraph.CreateInstance<SOOrderEntry>()));
    foreach (var data in datas)
    {
      SOOrderEntry soOrderEntry = lazy.Value;
      ((PXGraph) soOrderEntry).Clear((PXClearOption) 3);
      ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) data.BlanketNbr, new object[1]
      {
        (object) data.BlanketType
      }));
      if (this.Base.IsExternalTax(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.TaxZoneID))
        soOrderEntry.CalculateExternalTax(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current);
      ((PXGraph) soOrderEntry).Persist();
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ProcessExpiredOrder(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewChildOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<SOBlanketOrderDisplayLink>) this.BlanketOrderChildrenDisplayList).Current != null)
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) ((PXSelectBase<SOBlanketOrderDisplayLink>) this.BlanketOrderChildrenDisplayList).Current.OrderNbr, new object[1]
      {
        (object) ((PXSelectBase<SOBlanketOrderDisplayLink>) this.BlanketOrderChildrenDisplayList).Current.OrderType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Child Order");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintBlanket(PXAdapter adapter, string reportID = null)
  {
    return this.Base.Report(adapter.Apply<PXAdapter>((Action<PXAdapter>) (it => it.Menu = "Print Blanket Sales Order")), reportID ?? "SO641040");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailBlanket(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Base.Notification(adapter, notificationCD ?? "BLANKET SO");
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddBlanketLine(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current != null && ((PXSelectBase<OpenBlanketSOLineSplit>) this.BlanketSplits).AskExt() == 1)
    {
      bool flag = ((IQueryable<PXResult<PX.Objects.SO.SOLine>>) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>())).Any<PXResult<PX.Objects.SO.SOLine>>();
      foreach (OpenBlanketSOLineSplit blanketSoLineSplit in GraphHelper.RowCast<OpenBlanketSOLineSplit>(((PXSelectBase) this.BlanketSplits).Cache.Cached).Where<OpenBlanketSOLineSplit>((Func<OpenBlanketSOLineSplit, bool>) (res => res.Selected.GetValueOrDefault())))
      {
        foreach (PXResult<BlanketSOLineSplit, BlanketSOLine, SOBlanketOrderLink> pxResult in ((IEnumerable<PXResult<BlanketSOLineSplit>>) PXSelectBase<BlanketSOLineSplit, PXViewOf<BlanketSOLineSplit>.BasedOn<SelectFromBase<BlanketSOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BlanketSOLine>.On<BlanketSOLineSplit.FK.BlanketOrderLine>>, FbqlJoins.Left<SOBlanketOrderLink>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOBlanketOrderLink.blanketType, Equal<BlanketSOLineSplit.orderType>>>>, And<BqlOperand<SOBlanketOrderLink.blanketType, IBqlString>.IsEqual<BlanketSOLineSplit.orderNbr>>>, And<BqlOperand<SOBlanketOrderLink.orderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOBlanketOrderLink.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOLineSplit.orderType, Equal<BqlField<OpenBlanketSOLineSplit.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<BlanketSOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<OpenBlanketSOLineSplit.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<BlanketSOLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<OpenBlanketSOLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<BlanketSOLineSplit.splitLineNbr, IBqlInt>.IsEqual<BqlField<OpenBlanketSOLineSplit.splitLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[2]
        {
          (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current,
          (object) blanketSoLineSplit
        }, Array.Empty<object>())).AsEnumerable<PXResult<BlanketSOLineSplit>>().Cast<PXResult<BlanketSOLineSplit, BlanketSOLine, SOBlanketOrderLink>>().ToList<PXResult<BlanketSOLineSplit, BlanketSOLine, SOBlanketOrderLink>>())
        {
          BlanketSOLine blanketLine = PXResult<BlanketSOLineSplit, BlanketSOLine, SOBlanketOrderLink>.op_Implicit(pxResult);
          BlanketSOLineSplit blanketSplit = PXResult<BlanketSOLineSplit, BlanketSOLine, SOBlanketOrderLink>.op_Implicit(pxResult);
          SOBlanketOrderLink blanketOrderLink = PXResult<BlanketSOLineSplit, BlanketSOLine, SOBlanketOrderLink>.op_Implicit(pxResult);
          if (blanketOrderLink.BlanketNbr == null || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.BlanketOrderChildrenList).Cache.GetStatus((object) blanketOrderLink), (PXEntryStatus) 3, (PXEntryStatus) 4))
            this.CreateChildToBlanketLink(blanketLine.OrderType, blanketLine.OrderNbr, ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
          if (!flag)
          {
            PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
            copy.CustomerOrderNbr = blanketSplit.CustomerOrderNbr;
            if (copy.TaxZoneID != blanketLine.TaxZoneID)
            {
              copy.OverrideTaxZone = new bool?(true);
              copy.TaxZoneID = blanketLine.TaxZoneID;
            }
            ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(copy);
          }
          if ((string.IsNullOrEmpty(blanketSplit.CustomerOrderNbr) || blanketSplit.CustomerOrderNbr == ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CustomerOrderNbr) && blanketLine.TaxZoneID == ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.TaxZoneID)
          {
            this.AddChildLine(blanketSplit, blanketLine);
            flag = true;
          }
        }
      }
      ((PXSelectBase) this.BlanketSplits).Cache.Clear();
      ((PXSelectBase) this.BlanketSplits).View.Clear();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddBlanketLineOK(PXAdapter adapter)
  {
    ((PXSelectBase) this.BlanketSplits).View.Answer = (WebDialogResult) 1;
    return this.AddBlanketLine(adapter);
  }

  public virtual IEnumerable blanketSplits()
  {
    List<OpenBlanketSOLineSplit> blanketSoLineSplitList = new List<OpenBlanketSOLineSplit>();
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.Behavior != "SO")
      return (IEnumerable) blanketSoLineSplitList;
    bool flag = ((IQueryable<PXResult<PX.Objects.SO.SOLine>>) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>())).Any<PXResult<PX.Objects.SO.SOLine>>();
    SOOrderEntry soOrderEntry = this.Base;
    object[] objArray1 = new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current
    };
    object[] objArray2 = new object[2]
    {
      (object) !flag,
      (object) !flag
    };
    foreach (PXResult<BlanketSOLineSplit, BlanketSOLine> pxResult in ((IEnumerable<PXResult<BlanketSOLineSplit>>) PXSelectBase<BlanketSOLineSplit, PXViewOf<BlanketSOLineSplit>.BasedOn<SelectFromBase<BlanketSOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BlanketSOLine>.On<BlanketSOLineSplit.FK.BlanketOrderLine>>, FbqlJoins.Inner<BlanketSOOrder>.On<BlanketSOLineSplit.FK.BlanketOrder>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOLineSplit.completed, Equal<False>>>>, And<BqlOperand<BlanketSOLine.customerID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOOrder.customerID, IBqlInt>.FromCurrent>>>, And<BqlOperand<BlanketSOLine.customerLocationID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOOrder.customerLocationID, IBqlInt>.FromCurrent>>>, And<BqlOperand<BlanketSOLineSplit.schedOrderDate, IBqlDateTime>.IsLessEqual<BqlField<PX.Objects.SO.SOOrder.orderDate, IBqlDateTime>.FromCurrent>>>, And<BqlOperand<BlanketSOLineSplit.qty, IBqlDecimal>.IsGreater<BqlOperand<BlanketSOLineSplit.qtyOnOrders, IBqlDecimal>.Add<BlanketSOLineSplit.receivedQty>>>>, And<BqlOperand<BlanketSOOrder.curyID, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.curyID, IBqlString>.FromCurrent>>>, And<BqlOperand<BlanketSOOrder.taxCalcMode, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.taxCalcMode, IBqlString>.FromCurrent>>>, And<BqlOperand<BlanketSOOrder.isExpired, IBqlBool>.IsEqual<False>>>, And<BqlOperand<BlanketSOOrder.hold, IBqlBool>.IsEqual<False>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOLine.taxZoneID, Equal<BqlField<PX.Objects.SO.SOOrder.taxZoneID, IBqlString>.FromCurrent>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOLine.taxZoneID, IsNull>>>>.And<BqlOperand<Current<PX.Objects.SO.SOOrder.taxZoneID>, IBqlString>.IsNull>>>>.Or<BqlOperand<True, IBqlBool>.IsEqual<P.AsBool>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOLineSplit.customerOrderNbr, Equal<BqlField<PX.Objects.SO.SOOrder.customerOrderNbr, IBqlString>.FromCurrent>>>>, Or<BqlOperand<BlanketSOLineSplit.customerOrderNbr, IBqlString>.IsNull>>>.Or<BqlOperand<True, IBqlBool>.IsEqual<P.AsBool>>>>>.Config>.SelectMultiBound((PXGraph) soOrderEntry, objArray1, objArray2)).ToList<PXResult<BlanketSOLineSplit>>())
    {
      BlanketSOLineSplit split = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(pxResult);
      BlanketSOLine blanketSoLine = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(pxResult);
      if (!this.IsExpectingReturnAllocationsToBlanket(split))
      {
        OpenBlanketSOLineSplit blanketSoLineSplit1 = new OpenBlanketSOLineSplit()
        {
          OrderType = split.OrderType,
          OrderNbr = split.OrderNbr,
          LineNbr = split.LineNbr,
          SplitLineNbr = split.SplitLineNbr,
          InventoryID = split.InventoryID,
          SubItemID = split.SubItemID,
          TranDesc = blanketSoLine.TranDesc,
          SiteID = split.SiteID,
          CustomerID = blanketSoLine.CustomerID,
          CustomerLocationID = blanketSoLine.CustomerLocationID,
          CustomerOrderNbr = split.CustomerOrderNbr,
          SchedOrderDate = split.SchedOrderDate,
          UOM = blanketSoLine.UOM,
          BlanketOpenQty = split.BlanketOpenQty,
          TaxZoneID = blanketSoLine.TaxZoneID
        };
        OpenBlanketSOLineSplit blanketSoLineSplit2 = (OpenBlanketSOLineSplit) ((PXSelectBase) this.BlanketSplits).Cache.Locate((object) blanketSoLineSplit1);
        if (blanketSoLineSplit2 != null)
        {
          blanketSoLineSplitList.Add(blanketSoLineSplit2);
        }
        else
        {
          ((PXSelectBase) this.BlanketSplits).Cache.SetStatus((object) blanketSoLineSplit1, (PXEntryStatus) 5);
          blanketSoLineSplitList.Add(blanketSoLineSplit1);
        }
      }
    }
    return (IEnumerable) blanketSoLineSplitList;
  }

  protected virtual void CreateChildren(
    PX.Objects.SO.SOOrder blanket,
    DateTime? schedOrderDate,
    Blanket.CreateChildrenResult result)
  {
    if (blanket.Behavior != "BL")
      throw new InvalidOperationException();
    List<PXResult<BlanketSOLineSplit, BlanketSOLine>> list = ((IEnumerable<PXResult<BlanketSOLineSplit>>) PXSelectBase<BlanketSOLineSplit, PXViewOf<BlanketSOLineSplit>.BasedOn<SelectFromBase<BlanketSOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BlanketSOLine>.On<BlanketSOLineSplit.FK.BlanketOrderLine>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<BlanketSOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<BlanketSOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, BlanketSOLineSplit>, PX.Objects.SO.SOOrder, BlanketSOLineSplit>.SameAsCurrent>, And<BqlOperand<BlanketSOLineSplit.completed, IBqlBool>.IsEqual<False>>>, And<BqlOperand<BlanketSOLineSplit.qty, IBqlDecimal>.IsGreater<BqlOperand<BlanketSOLineSplit.qtyOnOrders, IBqlDecimal>.Add<BlanketSOLineSplit.receivedQty>>>>>.And<BqlOperand<BlanketSOLineSplit.schedOrderDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.SO.SOOrder[1]
    {
      blanket
    }, new object[1]{ (object) schedOrderDate })).AsEnumerable<PXResult<BlanketSOLineSplit>>().Cast<PXResult<BlanketSOLineSplit, BlanketSOLine>>().ToList<PXResult<BlanketSOLineSplit, BlanketSOLine>>();
    if (!list.Any<PXResult<BlanketSOLineSplit, BlanketSOLine>>())
      return;
    PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find((PXGraph) this.Base, blanket.OrderType);
    PX.Objects.CM.CurrencyInfo currencyInfo = ((PXSelectBase) this.Base.currencyinfo).View.SelectSingleBound(new object[1]
    {
      (object) blanket
    }, Array.Empty<object>()) as PX.Objects.CM.CurrencyInfo;
    foreach (IGrouping<\u003C\u003Ef__AnonymousType101<DateTime?, string, int?, string, string, string, string, string>, PXResult<BlanketSOLineSplit, BlanketSOLine>> source in list.GroupBy(r => new
    {
      SchedOrderDate = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).SchedOrderDate,
      CustomerOrderNbr = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).CustomerOrderNbr,
      CustomerLocationID = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).CustomerLocationID,
      TaxZoneID = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).TaxZoneID,
      ShipVia = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).ShipVia,
      FOBPoint = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).FOBPoint,
      ShipTermsID = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).ShipTermsID,
      ShipZoneID = PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(r).ShipZoneID
    }).OrderBy<IGrouping<\u003C\u003Ef__AnonymousType101<DateTime?, string, int?, string, string, string, string, string>, PXResult<BlanketSOLineSplit, BlanketSOLine>>, DateTime?>(g => g.Key.SchedOrderDate).ThenBy<IGrouping<\u003C\u003Ef__AnonymousType101<DateTime?, string, int?, string, string, string, string, string>, PXResult<BlanketSOLineSplit, BlanketSOLine>>, string>(g => g.Key.CustomerOrderNbr).ThenBy<IGrouping<\u003C\u003Ef__AnonymousType101<DateTime?, string, int?, string, string, string, string, string>, PXResult<BlanketSOLineSplit, BlanketSOLine>>, int?>(g => g.Key.CustomerLocationID).ThenBy<IGrouping<\u003C\u003Ef__AnonymousType101<DateTime?, string, int?, string, string, string, string, string>, PXResult<BlanketSOLineSplit, BlanketSOLine>>, string>(g => g.Key.TaxZoneID))
    {
      try
      {
        PX.Objects.SO.SOOrder child = this.CreateChild(new Blanket.CreateChildParameter()
        {
          BlanketOrder = blanket,
          BlanketOrderType = soOrderType,
          BlanketCurrency = currencyInfo,
          SchedOrderDate = source.Key.SchedOrderDate,
          CustomerOrderNbr = source.Key.CustomerOrderNbr,
          CustomerLocationID = source.Key.CustomerLocationID,
          TaxZoneID = source.Key.TaxZoneID,
          ShipVia = source.Key.ShipVia,
          FOBPoint = source.Key.FOBPoint,
          ShipTermsID = source.Key.ShipTermsID,
          ShipZoneID = source.Key.ShipZoneID,
          Lines = (IEnumerable<PXResult<BlanketSOLineSplit, BlanketSOLine>>) source
        });
        result.Created.Add(child);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError((Exception) ErrorProcessingEntityException.Create(((PXGraph) this.Base).Caches[typeof (BlanketSOLineSplit)], (IBqlTable) PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(source.First<PXResult<BlanketSOLineSplit, BlanketSOLine>>()), ex));
        result.LastError = ex;
        ++result.ErrorCount;
      }
      ((PXGraph) this.Base).Clear();
    }
  }

  protected virtual PX.Objects.SO.SOOrder CreateChild(Blanket.CreateChildParameter parameter)
  {
    PX.Objects.SO.SOOrder copy1 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Insert(new PX.Objects.SO.SOOrder()
    {
      OrderType = parameter.BlanketOrderType.DfltChildOrderType,
      BranchID = parameter.BlanketOrder.BranchID
    }));
    copy1.OrderDate = parameter.SchedOrderDate;
    copy1.CustomerID = parameter.BlanketOrder.CustomerID;
    copy1.CustomerLocationID = parameter.CustomerLocationID;
    PX.Objects.SO.SOOrder copy2 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(copy1));
    this.Base.ReloadCustomerCreditRule();
    copy2.BranchID = parameter.BlanketOrder.BranchID;
    copy2.ProjectID = parameter.BlanketOrder.ProjectID;
    copy2.TaxCalcMode = parameter.BlanketOrder.TaxCalcMode;
    copy2.TermsID = parameter.BlanketOrder.TermsID;
    copy2.PaymentMethodID = parameter.BlanketOrder.PaymentMethodID;
    copy2.PMInstanceID = parameter.BlanketOrder.PMInstanceID;
    copy2.CustomerOrderNbr = parameter.CustomerOrderNbr;
    if (parameter.TaxZoneID != this.GetDefaultLocationTaxZone(copy2.CustomerID, copy2.CustomerLocationID, copy2.BranchID))
      copy2.OverrideTaxZone = new bool?(true);
    copy2.TaxZoneID = parameter.TaxZoneID;
    copy2.ShipVia = parameter.ShipVia;
    copy2.FOBPoint = parameter.FOBPoint;
    copy2.ShipTermsID = parameter.ShipTermsID;
    copy2.ShipZoneID = parameter.ShipZoneID;
    PX.Objects.SO.SOOrder copy3 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(copy2));
    copy3.CuryID = parameter.BlanketOrder.CuryID;
    PX.Objects.SO.SOOrder doc = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(copy3);
    if (parameter.BlanketOrderType.UseCuryRateFromBL.GetValueOrDefault() && parameter.BlanketCurrency.BaseCuryID != parameter.BlanketCurrency.CuryID)
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Select(Array.Empty<object>()));
      PXCache<PX.Objects.CM.CurrencyInfo>.RestoreCopy(currencyInfo, parameter.BlanketCurrency);
      currencyInfo.CuryInfoID = doc.CuryInfoID;
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Update(currencyInfo);
    }
    this.CreateChildToBlanketLink(parameter.BlanketOrder, doc);
    using (((PXGraph) this.Base).FindImplementation<AffectedBlanketOrderByChildOrders>().SuppressedModeScope(parameter.BlanketOrder))
    {
      foreach (PXResult<BlanketSOLineSplit, BlanketSOLine> line in parameter.Lines)
      {
        PX.Objects.SO.SOLine soLine = this.AddChildLine(PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(line), PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(line));
        PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this.Base).Caches[typeof (BlanketSOLine)], (object) PXResult<BlanketSOLineSplit, BlanketSOLine>.op_Implicit(line), ((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOLine)], (object) soLine, parameter.BlanketOrderType.CopyLineNotesToChildOrder, parameter.BlanketOrderType.CopyLineFilesToChildOrder);
      }
    }
    PX.Objects.SO.SOOrder copy4 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Locate(doc));
    copy4.CuryControlTotal = copy4.CuryOrderTotal;
    PX.Objects.SO.SOOrder destinationOrder = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(copy4);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.TransferPayments(parameter.BlanketOrder, destinationOrder);
      ((PXAction) this.Base.Save).Press();
      transactionScope.Complete();
    }
    return ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
  }

  private void CreateChildToBlanketLink(PX.Objects.SO.SOOrder blanket, PX.Objects.SO.SOOrder doc)
  {
    this.CreateChildToBlanketLink(blanket.OrderType, blanket.OrderNbr, doc);
  }

  private void CreateChildToBlanketLink(
    string blanketOrderType,
    string blanketOrderNbr,
    PX.Objects.SO.SOOrder doc)
  {
    SOBlanketOrderLink blanketOrderLink1 = new SOBlanketOrderLink()
    {
      BlanketType = blanketOrderType,
      BlanketNbr = blanketOrderNbr,
      OrderType = doc.OrderType,
      OrderNbr = doc.OrderNbr,
      CuryInfoID = doc.CuryInfoID
    };
    SOBlanketOrderLink blanketOrderLink2 = ((PXSelectBase<SOBlanketOrderLink>) this.BlanketOrderChildrenList).Locate(blanketOrderLink1) ?? (SOBlanketOrderLink) PrimaryKeyOf<SOBlanketOrderLink>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr, SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>>.Find((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr, SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>) blanketOrderLink1, (PKFindOptions) 0);
    if (blanketOrderLink2 != null && !EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.BlanketOrderChildrenList).Cache.GetStatus((object) blanketOrderLink2), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    PXParentAttribute.SetParent(((PXSelectBase) this.BlanketOrderChildrenList).Cache, (object) blanketOrderLink1, typeof (PX.Objects.SO.SOOrder), (object) doc);
    ((PXSelectBase<SOBlanketOrderLink>) this.BlanketOrderChildrenList).Insert(blanketOrderLink1);
  }

  protected virtual PX.Objects.SO.SOLine AddChildLine(
    BlanketSOLineSplit blanketSplit,
    BlanketSOLine blanketLine)
  {
    PX.Objects.SO.SOLine soLine = new PX.Objects.SO.SOLine()
    {
      BranchID = blanketLine.BranchID
    };
    soLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(soLine));
    soLine.IsStockItem = blanketLine.IsStockItem;
    soLine.InventoryID = blanketSplit.InventoryID;
    soLine.SubItemID = blanketSplit.SubItemID;
    soLine.SiteID = blanketSplit.SiteID;
    soLine.TaxCategoryID = blanketLine.TaxCategoryID;
    soLine.TaskID = blanketLine.TaskID;
    soLine.CostCodeID = blanketLine.CostCodeID;
    soLine.AutomaticDiscountsDisabled = new bool?(true);
    if (blanketSplit.SchedShipDate.HasValue)
      soLine.ShipDate = blanketSplit.SchedShipDate;
    soLine.ShipComplete = blanketLine.ShipComplete;
    soLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine));
    soLine.UOM = blanketLine.UOM;
    soLine.ManualPrice = new bool?(true);
    soLine.LineType = blanketSplit.LineType;
    soLine.TranDesc = blanketLine.TranDesc;
    soLine.SalesPersonID = blanketLine.SalesPersonID;
    soLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine));
    Decimal? qty = blanketSplit.Qty;
    Decimal? nullable1 = blanketSplit.QtyOnOrders;
    Decimal? nullable2 = qty.HasValue & nullable1.HasValue ? new Decimal?(qty.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = blanketSplit.ReceivedQty;
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    Decimal? nullable5 = nullable4;
    soLine.OrderQty = nullable5;
    soLine.CuryUnitPrice = blanketLine.CuryUnitPrice;
    soLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine));
    PX.Objects.SO.SOLine soLine1 = soLine;
    nullable1 = nullable5;
    Decimal? nullable6 = blanketLine.CuryExtPrice;
    nullable3 = nullable1.HasValue & nullable6.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7 = blanketLine.OrderQty;
    Decimal? nullable8;
    if (!(nullable3.HasValue & nullable7.HasValue))
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = new Decimal?(nullable3.GetValueOrDefault() / nullable7.GetValueOrDefault());
    soLine1.CuryExtPrice = nullable8;
    soLine.BlanketType = blanketSplit.OrderType;
    soLine.BlanketNbr = blanketSplit.OrderNbr;
    soLine.BlanketLineNbr = blanketSplit.LineNbr;
    soLine.BlanketSplitLineNbr = blanketSplit.SplitLineNbr;
    soLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine));
    soLine.DiscountID = blanketLine.DiscountID;
    soLine.SkipLineDiscounts = blanketLine.SkipLineDiscounts;
    soLine.DiscPct = blanketLine.DiscPct;
    soLine.SalesAcctID = blanketLine.SalesAcctID;
    soLine.SalesSubID = blanketLine.SalesSubID;
    nullable7 = nullable5;
    nullable3 = blanketLine.OrderQty;
    if (nullable7.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable7.HasValue == nullable3.HasValue)
    {
      soLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine));
      soLine.CuryDiscAmt = blanketLine.CuryDiscAmt;
    }
    soLine = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine);
    bool? nullable9 = blanketSplit.POCreate;
    if (nullable9.GetValueOrDefault())
    {
      soLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(soLine);
      soLine.POCreate = new bool?(true);
      soLine.POSource = blanketLine.POSource;
      soLine.VendorID = blanketLine.VendorID;
      soLine = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine);
      nullable9 = blanketLine.POCreated;
      if (nullable9.GetValueOrDefault())
      {
        using (IEnumerator<PX.Objects.SO.SOLineSplit> enumerator = ((PXSelectBase) this.Base.splits).Cache.Inserted.Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (split =>
        {
          int? lineNbr1 = split.LineNbr;
          int? lineNbr2 = soLine.LineNbr;
          return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
        })).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            PX.Objects.SO.SOLineSplit current = enumerator.Current;
            current.POType = blanketSplit.POType;
            current.PONbr = blanketSplit.PONbr;
            current.POLineNbr = blanketSplit.POLineNbr;
            current.RefNoteID = blanketSplit.RefNoteID;
            current.Completed = blanketSplit.Completed;
            current.POCompleted = blanketSplit.POCompleted;
            current.POCancelled = blanketSplit.POCancelled;
            ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(current);
            INItemPlan inItemPlan1 = GraphHelper.Caches<INItemPlan>((PXGraph) this.Base).Locate(new INItemPlan()
            {
              PlanID = current.PlanID
            });
            INItemPlan inItemPlan2 = INItemPlan.PK.Find((PXGraph) this.Base, blanketSplit.PlanID);
            if (inItemPlan1 != null)
            {
              if (inItemPlan2 != null)
              {
                inItemPlan1.SupplyPlanID = inItemPlan2.SupplyPlanID;
                GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) this.Base), (object) inItemPlan1, true);
              }
            }
          }
        }
      }
    }
    else
    {
      nullable9 = blanketSplit.IsAllocated;
      if (nullable9.GetValueOrDefault() || !string.IsNullOrEmpty(blanketSplit.POReceiptNbr))
      {
        using (new Blanket.ChildOrderCreationFromBlanketScope())
        {
          foreach (PX.Objects.SO.SOLineSplit soLineSplit in ((PXSelectBase) this.Base.splits).Cache.Inserted.Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (split =>
          {
            int? lineNbr3 = split.LineNbr;
            int? lineNbr4 = soLine.LineNbr;
            return lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue;
          })))
          {
            soLineSplit.IsAllocated = blanketSplit.IsAllocated;
            if (!string.IsNullOrEmpty(blanketSplit.LotSerialNbr))
              soLineSplit.LotSerialNbr = blanketSplit.LotSerialNbr;
            soLineSplit.POReceiptType = blanketSplit.POReceiptType;
            soLineSplit.POReceiptNbr = blanketSplit.POReceiptNbr;
            ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(soLineSplit);
          }
        }
      }
    }
    soLine.ManualDisc = blanketLine.ManualDisc;
    return soLine;
  }

  protected virtual void TransferPayments(PX.Objects.SO.SOOrder blanketOrder, PX.Objects.SO.SOOrder destinationOrder)
  {
    if (this.Base.IsExternalTax(blanketOrder.TaxZoneID))
    {
      ((PXAction) this.Base.Save).Press();
      ((PXSelectBase) this.Base.Document).Cache.ClearQueryCache();
      ((PXSelectBase) this.Base.Document).Cache.Clear();
      PXSelectJoin<PX.Objects.SO.SOOrder, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document1 = this.Base.Document;
      PXSelectJoin<PX.Objects.SO.SOOrder, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.SO.SOOrder.customerID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> document2 = this.Base.Document;
      string orderNbr = destinationOrder.OrderNbr;
      object[] objArray = new object[1]
      {
        (object) destinationOrder.OrderType
      };
      PX.Objects.SO.SOOrder soOrder1;
      PX.Objects.SO.SOOrder soOrder2 = soOrder1 = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) document2).Search<PX.Objects.SO.SOOrder.orderNbr>((object) orderNbr, objArray));
      ((PXSelectBase<PX.Objects.SO.SOOrder>) document1).Current = soOrder1;
      destinationOrder = soOrder2;
    }
    Decimal valueOrDefault = destinationOrder.CuryUnpaidBalance.GetValueOrDefault();
    foreach (PXResult<BlanketSOAdjust> pxResult in ((PXSelectBase<BlanketSOAdjust>) this.BlanketAdjustments).Select(new object[2]
    {
      (object) blanketOrder.OrderType,
      (object) blanketOrder.OrderNbr
    }))
    {
      BlanketSOAdjust blanketAdjustment = PXResult<BlanketSOAdjust>.op_Implicit(pxResult);
      Decimal amount = Math.Min(blanketAdjustment.CuryAdjdAmt.GetValueOrDefault(), valueOrDefault);
      if (amount > 0M)
        this.TransferPayment(blanketOrder, blanketAdjustment, destinationOrder, amount);
      valueOrDefault -= amount;
      if (valueOrDefault <= 0M)
        break;
    }
  }

  protected virtual void TransferPayment(
    PX.Objects.SO.SOOrder blanketOrder,
    BlanketSOAdjust blanketAdjustment,
    PX.Objects.SO.SOOrder destinationOrder,
    Decimal amount)
  {
    SOAdjust adj = new SOAdjust()
    {
      AdjgDocType = blanketAdjustment.AdjgDocType,
      AdjgRefNbr = blanketAdjustment.AdjgRefNbr,
      BlanketRecordID = blanketAdjustment.RecordID,
      BlanketType = blanketAdjustment.AdjdOrderType,
      BlanketNbr = blanketAdjustment.AdjdOrderNbr,
      AdjdOrderType = destinationOrder.OrderType,
      AdjdOrderNbr = destinationOrder.OrderNbr,
      CuryAdjdAmt = new Decimal?(0M)
    };
    this.Base.CalculateApplicationBalance(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo).Current, ARPayment.PK.Find((PXGraph) this.Base, adj.AdjgDocType, adj.AdjgRefNbr) ?? throw new RowNotFoundException(((PXSelectBase) this.Base.Adjustments).Cache, new object[2]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    }), adj);
    SOAdjust copy = (SOAdjust) ((PXSelectBase) this.Base.Adjustments).Cache.CreateCopy((object) ((PXSelectBase<SOAdjust>) this.Base.Adjustments).Insert(adj));
    copy.CuryAdjdAmt = new Decimal?(amount);
    ((PXSelectBase<SOAdjust>) this.Base.Adjustments).Update(copy);
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.CalculatePaymentBalance(PX.Objects.AR.ARPayment,PX.Objects.SO.SOAdjust)" />
  [PXOverride]
  public void CalculatePaymentBalance(
    ARPayment payment,
    SOAdjust adj,
    Action<ARPayment, SOAdjust> baseMethod)
  {
    baseMethod(payment, adj);
    if (adj.BlanketNbr != null)
    {
      BlanketSOAdjust blanketSoAdjust = BlanketSOAdjust.PK.Find((PXGraph) this.Base, adj.BlanketRecordID.Value, adj.BlanketType, adj.BlanketNbr, adj.AdjgDocType, adj.AdjgRefNbr);
      ARPayment arPayment1 = payment;
      Decimal? curyDocBal = arPayment1.CuryDocBal;
      Decimal valueOrDefault1 = ((Decimal?) blanketSoAdjust?.CuryAdjgAmt).GetValueOrDefault();
      arPayment1.CuryDocBal = curyDocBal.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
      ARPayment arPayment2 = payment;
      Decimal? docBal = arPayment2.DocBal;
      Decimal valueOrDefault2 = ((Decimal?) blanketSoAdjust?.AdjAmt).GetValueOrDefault();
      arPayment2.DocBal = docBal.HasValue ? new Decimal?(docBal.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    }
    else
    {
      SOAdjust original = GraphHelper.Caches<SOAdjust>((PXGraph) this.Base).GetOriginal(adj);
      if (original == null || original.BlanketNbr == null)
        return;
      BlanketSOAdjust blanketSoAdjust1 = BlanketSOAdjust.PK.Find((PXGraph) this.Base, original.BlanketRecordID.Value, original.BlanketType, original.BlanketNbr, original.AdjgDocType, original.AdjgRefNbr);
      BlanketSOAdjust blanketSoAdjust2 = GraphHelper.Caches<BlanketSOAdjust>((PXGraph) this.Base).Locate(blanketSoAdjust1);
      if (blanketSoAdjust2 == null || blanketSoAdjust2 == blanketSoAdjust1)
        return;
      ARPayment arPayment3 = payment;
      Decimal? curyDocBal = arPayment3.CuryDocBal;
      Decimal num1 = ((Decimal?) blanketSoAdjust2?.CuryAdjgAmt).GetValueOrDefault() - ((Decimal?) blanketSoAdjust1?.CuryAdjgAmt).GetValueOrDefault();
      arPayment3.CuryDocBal = curyDocBal.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - num1) : new Decimal?();
      ARPayment arPayment4 = payment;
      Decimal? docBal = arPayment4.DocBal;
      Decimal num2 = ((Decimal?) blanketSoAdjust2?.AdjAmt).GetValueOrDefault() - ((Decimal?) blanketSoAdjust1?.AdjAmt).GetValueOrDefault();
      arPayment4.DocBal = docBal.HasValue ? new Decimal?(docBal.GetValueOrDefault() - num2) : new Decimal?();
    }
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.InitializeAdjustFields(PX.Objects.SO.SOAdjust,PX.Objects.AR.ARPayment,PX.Objects.CM.CurrencyInfo)" />
  [PXOverride]
  public void InitializeAdjustFields(
    SOAdjust adjustToFill,
    ARPayment paymentToBeAdapted,
    PX.Objects.CM.CurrencyInfo paymentCurrencyInfo,
    Action<SOAdjust, ARPayment, PX.Objects.CM.CurrencyInfo> baseInitializeAdjustFieldsAndAdaptPayment)
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.Behavior == "SO")
    {
      adjustToFill.BlanketNbr = (string) null;
      adjustToFill.BlanketType = (string) null;
      adjustToFill.BlanketRecordID = new int?();
      Dictionary<(string, string), (int?, int?)> dictionary = ((PXSelectBase) this.Base.Transactions).View.BqlSelect.WhereAnd<Where<BqlOperand<PX.Objects.SO.SOLine.blanketNbr, IBqlString>.IsNotNull>>().Select<PX.Objects.SO.SOLine>((PXGraph) this.Base, false).GroupBy<PX.Objects.SO.SOLine, (string, string)>((Func<PX.Objects.SO.SOLine, (string, string)>) (soLine => (soLine.BlanketNbr, soLine.BlanketType))).ToDictionary<IGrouping<(string, string), PX.Objects.SO.SOLine>, (string, string), (int?, int?)>((Func<IGrouping<(string, string), PX.Objects.SO.SOLine>, (string, string)>) (g => g.Key), (Func<IGrouping<(string, string), PX.Objects.SO.SOLine>, (int?, int?)>) (g => (g.First<PX.Objects.SO.SOLine>().SortOrder, g.First<PX.Objects.SO.SOLine>().LineNbr)));
      if (dictionary.Any<KeyValuePair<(string, string), (int?, int?)>>())
      {
        SOAdjust soAdjust = GraphHelper.RowCast<SOAdjust>((IEnumerable) PXSelectBase<SOBlanketOrderLink, PXViewOf<SOBlanketOrderLink>.BasedOn<SelectFromBase<SOBlanketOrderLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjdOrderNbr, Equal<SOBlanketOrderLink.blanketNbr>>>>>.And<BqlOperand<SOAdjust.adjdOrderType, IBqlString>.IsEqual<SOBlanketOrderLink.blanketType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.adjgRefNbr, Equal<BqlField<ARPayment.refNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOAdjust.adjgDocType, IBqlString>.IsEqual<BqlField<ARPayment.docType, IBqlString>.FromCurrent>>>, And<BqlOperand<SOBlanketOrderLink.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOBlanketOrderLink.orderType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
        {
          (object) paymentToBeAdapted
        }, Array.Empty<object>())).OrderBy<SOAdjust, SOAdjust>((Func<SOAdjust, SOAdjust>) (x => x), (IComparer<SOAdjust>) new Blanket.BlanketAdjustComparer(dictionary)).FirstOrDefault<SOAdjust>();
        if (soAdjust != null)
        {
          adjustToFill.BlanketNbr = soAdjust.AdjdOrderNbr;
          adjustToFill.BlanketType = soAdjust.AdjdOrderType;
          adjustToFill.BlanketRecordID = soAdjust.RecordID;
        }
      }
    }
    baseInitializeAdjustFieldsAndAdaptPayment(adjustToFill, paymentToBeAdapted, paymentCurrencyInfo);
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.IsBalanceRecalculationRequired(PX.Objects.SO.SOAdjust)" />
  [PXOverride]
  public bool IsBalanceRecalculationRequired(
    SOAdjust adj,
    Func<SOAdjust, bool> base_IsBalanceRecalculationRequired)
  {
    return base_IsBalanceRecalculationRequired(adj) || adj.IsBalanceRecalculationRequired.GetValueOrDefault();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.GetDefaultSODiscountCalculationOptions(PX.Objects.SO.SOOrder)" />
  /// </summary>
  [PXOverride]
  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultSODiscountCalculationOptions(
    PX.Objects.SO.SOOrder doc,
    Func<PX.Objects.SO.SOOrder, DiscountEngine.DiscountCalculationOptions> baseFunc)
  {
    DiscountEngine.DiscountCalculationOptions calculationOptions = baseFunc(doc);
    return this.AlterSODiscountCalculationOptions(doc, calculationOptions);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.GetDefaultSODiscountCalculationOptions(PX.Objects.SO.SOOrder,System.Boolean)" />
  /// </summary>
  [PXOverride]
  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultSODiscountCalculationOptions(
    PX.Objects.SO.SOOrder doc,
    bool doNotDeferDiscountCalculation,
    Func<PX.Objects.SO.SOOrder, bool, DiscountEngine.DiscountCalculationOptions> baseFunc)
  {
    DiscountEngine.DiscountCalculationOptions calculationOptions = baseFunc(doc, doNotDeferDiscountCalculation);
    return this.AlterSODiscountCalculationOptions(doc, calculationOptions);
  }

  protected virtual DiscountEngine.DiscountCalculationOptions AlterSODiscountCalculationOptions(
    PX.Objects.SO.SOOrder doc,
    DiscountEngine.DiscountCalculationOptions calculationOptions)
  {
    if (doc.Behavior == "BL")
      calculationOptions |= DiscountEngine.DiscountCalculationOptions.ExplicitlyAllowToCalculateAutomaticLineDiscounts;
    return calculationOptions;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.IsCurrencyEnabled(PX.Objects.SO.SOOrder)" />
  /// </summary>
  [PXOverride]
  public virtual bool IsCurrencyEnabled(PX.Objects.SO.SOOrder order, Func<PX.Objects.SO.SOOrder, bool> baseMethod)
  {
    if (baseMethod(order))
    {
      int? nullable = order.ChildLineCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = order.BlanketLineCntr;
        int num2 = 0;
        return nullable.GetValueOrDefault() == num2 & nullable.HasValue;
      }
    }
    return false;
  }

  [PXOverride]
  public virtual void InvoiceOrders(
    List<PX.Objects.SO.SOOrder> list,
    Dictionary<string, object> arguments,
    bool massProcess,
    PXQuickProcess.ActionFlow quickProcessFlow,
    Action<List<PX.Objects.SO.SOOrder>, Dictionary<string, object>, bool, PXQuickProcess.ActionFlow> baseMethod)
  {
    if (massProcess || list.Count != 1 || list.First<PX.Objects.SO.SOOrder>().Behavior != "BL")
    {
      baseMethod(list, arguments, massProcess, quickProcessFlow);
    }
    else
    {
      PX.Objects.SO.SOOrder soOrder = list.First<PX.Objects.SO.SOOrder>();
      PXResultset<PX.Objects.SO.SOOrderShipment> pxResultset = PXSelectBase<PX.Objects.SO.SOOrderShipment, PXViewOf<PX.Objects.SO.SOOrderShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOOrderShipment.FK.Order>>, FbqlJoins.Inner<PX.Objects.CM.CurrencyInfo>.On<PX.Objects.SO.SOOrder.FK.CurrencyInfo>>, FbqlJoins.Inner<SOAddress>.On<BqlOperand<SOAddress.addressID, IBqlInt>.IsEqual<PX.Objects.SO.SOOrder.billAddressID>>>, FbqlJoins.Inner<SOContact>.On<BqlOperand<SOContact.contactID, IBqlInt>.IsEqual<PX.Objects.SO.SOOrder.billContactID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Exists<Select<SOShipLine, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<PX.Objects.SO.SOOrderShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOOrderShipment.shipmentNbr>, Field<SOShipLine.origOrderType>.IsRelatedTo<PX.Objects.SO.SOOrderShipment.orderType>, Field<SOShipLine.origOrderNbr>.IsRelatedTo<PX.Objects.SO.SOOrderShipment.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrderShipment, SOShipLine>>, And<BqlOperand<SOShipLine.blanketType, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOShipLine.blanketNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>>>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.confirmed, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.createARDoc, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.SO.SOOrderShipment.invoiceNbr, IBqlString>.IsNull>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
      {
        (object) soOrder
      }, Array.Empty<object>());
      SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
      InvoiceList invoiceList = new InvoiceList((PXGraph) PXGraph.CreateInstance<SOShipmentEntry>());
      PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, soOrder.CustomerID);
      if (customer == null)
        throw new RowNotFoundException(((PXSelectBase) this.Base.customer).Cache, new object[1]
        {
          (object) soOrder.CustomerID
        });
      foreach (PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact> order in pxResultset)
        instance.InvoiceOrder(new InvoiceOrderArgs(order)
        {
          InvoiceDate = ((PXGraph) this.Base).Accessinfo.BusinessDate.Value,
          Customer = customer,
          List = invoiceList,
          QuickProcessFlow = quickProcessFlow,
          GroupByCustomerOrderNumber = true
        });
      List<PX.Objects.SO.SOOrder> list1 = GraphHelper.RowCast<PX.Objects.SO.SOOrder>((IEnumerable) PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderQty, Equal<decimal0>>>>, And<BqlOperand<PX.Objects.SO.SOOrder.hold, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.SO.SOOrder.cancelled, IBqlBool>.IsEqual<False>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.unbilledOrderQty, Greater<decimal0>>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.unbilledMiscTot, IBqlDecimal>.IsGreater<decimal0>>>>>.And<Exists<Select<SOBlanketOrderLink, Where<KeysRelation<CompositeKey<Field<SOBlanketOrderLink.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<SOBlanketOrderLink.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, SOBlanketOrderLink>, PX.Objects.SO.SOOrder, SOBlanketOrderLink>.And<KeysRelation<CompositeKey<Field<SOBlanketOrderLink.blanketType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<SOBlanketOrderLink.blanketNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, SOBlanketOrderLink>, PX.Objects.SO.SOOrder, SOBlanketOrderLink>.SameAsCurrent>>>>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
      {
        (object) soOrder
      }, Array.Empty<object>())).ToList<PX.Objects.SO.SOOrder>();
      if (list1.Any<PX.Objects.SO.SOOrder>())
        this.Base.InvoiceOrder(arguments, (IEnumerable<PX.Objects.SO.SOOrder>) list1, invoiceList, massProcess, quickProcessFlow, true);
      if (!invoiceList.Any<PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>>())
        throw new PXException("The child orders for this sales order do not contain any items that can be added to an invoice.");
      if (invoiceList.Count == 1 && !list1.Any<PX.Objects.SO.SOOrder>())
        throw new PXRedirectRequiredException((PXGraph) instance, "Invoice");
      PX.Objects.AR.ARInvoice arInvoice = invoiceList.Count == 1 ? PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(invoiceList.First<PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>>()) : throw new PXOperationCompletedException("The following invoices have been created: {0}", new object[1]
      {
        (object) string.Join(", ", invoiceList.Select<PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>, string>((Func<PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>, string>) (o => PXResult<PX.Objects.AR.ARInvoice, PX.Objects.SO.SOInvoice>.op_Implicit(o).RefNbr)))
      });
      ((PXGraph) instance).Clear();
      ((PXGraph) instance).SelectTimeStamp();
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) arInvoice.RefNbr, new object[1]
      {
        (object) arInvoice.DocType
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "Invoice");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID> e)
  {
    if (!(e.Row.Behavior == "BL"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) ProjectDefaultAttribute.NonProject();
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.projectID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.expireDate> e)
  {
    this.VerifyExpireDate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.expireDate>>) e).Cache, e.Row, (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.expireDate>, PX.Objects.SO.SOOrder, object>) e).NewValue, false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    bool isBlanket = e.Row?.Behavior == "BL";
    ((PXAction) this.addBlanketLine).SetVisible(e.Row?.Behavior == "SO");
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained;
    if (isBlanket)
    {
      chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row).For<PX.Objects.SO.SOOrder.requestDate>((Action<PXUIFieldAttribute>) (a => a.Enabled = a.Visible = false));
      chained = chained.SameFor<PX.Objects.SO.SOOrder.projectID>();
      chained = chained.SameFor<PX.Objects.SO.SOOrder.curyBilledPaymentTotal>();
      chained = chained.SameFor<PX.Objects.SO.SOOrder.curyDiscTot>();
      chained = chained.SameFor<PX.Objects.SO.SOOrder.curyFreightTot>();
      chained = chained.SameFor<PX.Objects.SO.SOOrder.disableAutomaticTaxCalculation>();
      chained.For<PX.Objects.SO.SOOrder.customerID>((Action<PXUIFieldAttribute>) (a =>
      {
        PXUIFieldAttribute pxuiFieldAttribute = a;
        int? childLineCntr = e.Row.ChildLineCntr;
        int num1 = 0;
        int num2 = childLineCntr.GetValueOrDefault() == num1 & childLineCntr.HasValue ? 1 : 0;
        pxuiFieldAttribute.Enabled = num2 != 0;
      }));
    }
    chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row).For<PX.Objects.SO.SOOrder.expireDate>((Action<PXUIFieldAttribute>) (a => a.Visible = isBlanket));
    chained = chained.SameFor<PX.Objects.SO.SOOrder.curyTransferredToChildrenPaymentTotal>();
    chained.For<PX.Objects.SO.SOOrder.blanketOpenQty>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = isBlanket;
      a.Enabled = false;
    }));
    chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.Transactions).Cache, (object) null).For<PX.Objects.SO.SOLine.customerOrderNbr>((Action<PXUIFieldAttribute>) (a => a.Visible = isBlanket));
    chained = chained.SameFor<PX.Objects.SO.SOLine.schedOrderDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.schedShipDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.pOCreateDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.taxZoneID>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.customerLocationID>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.shipVia>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.fOBPoint>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.shipTermsID>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.shipZoneID>();
    chained = chained.For<PX.Objects.SO.SOLine.qtyOnOrders>((Action<PXUIFieldAttribute>) (a => a.Visible = isBlanket));
    chained = chained.SameFor<PX.Objects.SO.SOLine.blanketOpenQty>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.unshippedQty>();
    chained = chained.For<PX.Objects.SO.SOLine.blanketNbr>((Action<PXUIFieldAttribute>) (a => a.Visible = !isBlanket));
    chained = chained.SameFor<PX.Objects.SO.SOLine.openQty>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.dRTermStartDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.dRTermEndDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.requestDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.shipDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.completeQtyMin>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.completeQtyMax>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.reasonCode>();
    chained.SameFor<PX.Objects.SO.SOLine.avalaraCustomerUsageType>();
    chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.Transactions).Cache, (object) null).For<PX.Objects.SO.SOLine.taskID>((Action<PXUIFieldAttribute>) (a => a.Visible &= !isBlanket));
    chained.SameFor<PX.Objects.SO.SOLine.costCodeID>();
    chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.splits).Cache, (object) null).For<PX.Objects.SO.SOLineSplit.customerOrderNbr>((Action<PXUIFieldAttribute>) (a => a.Enabled = a.Visible = isBlanket));
    chained = chained.SameFor<PX.Objects.SO.SOLineSplit.schedOrderDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLineSplit.schedShipDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLineSplit.pOCreateDate>();
    chained = chained.For<PX.Objects.SO.SOLineSplit.qtyOnOrders>((Action<PXUIFieldAttribute>) (a => a.Visible = isBlanket));
    chained.SameFor<PX.Objects.SO.SOLineSplit.blanketOpenQty>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster;
    if (isBlanket)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.Transactions).Cache, (object) null);
      chained = attributeAdjuster.For<PX.Objects.SO.SOLine.pOCreateDate>((Action<PXUIFieldAttribute>) (a => a.Visible = PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>()));
      chained = chained.SameFor<PX.Objects.SO.SOLine.pOCreate>();
      chained.SameFor<PX.Objects.SO.SOLine.pOSource>();
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.splits).Cache, (object) null);
      attributeAdjuster.For<PX.Objects.SO.SOLineSplit.pOCreateDate>((Action<PXUIFieldAttribute>) (a => a.Enabled = a.Visible = PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>()));
    }
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.recalcdiscountsfilter).Cache, (object) null);
    chained = attributeAdjuster.For<RecalcDiscountsParamFilter.calcDiscountsOnLinesWithDisabledAutomaticDiscounts>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      int num3;
      if (!isBlanket)
      {
        PX.Objects.SO.SOOrder row = e.Row;
        if (row == null)
        {
          num3 = 0;
        }
        else
        {
          int? blanketLineCntr = row.BlanketLineCntr;
          int num4 = 0;
          num3 = blanketLineCntr.GetValueOrDefault() > num4 & blanketLineCntr.HasValue ? 1 : 0;
        }
      }
      else
        num3 = 0;
      pxuiFieldAttribute.Visible = num3 != 0;
    }));
    chained.For<RecalcDiscountsParamFilter.overrideManualDocGroupDiscounts>((Action<PXUIFieldAttribute>) (a => a.Visible = !isBlanket));
    bool? nullable1;
    if (string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<PX.Objects.SO.SOOrder.expireDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row)) & isBlanket)
    {
      DateTime? expireDate = e.Row.ExpireDate;
      DateTime? nullable2 = ((PXGraph) this.Base).Accessinfo.BusinessDate;
      int num;
      if ((expireDate.HasValue & nullable2.HasValue ? (expireDate.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = e.Row.Hold;
        bool flag1 = false;
        if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
        {
          nullable1 = e.Row.Cancelled;
          bool flag2 = false;
          if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
          {
            nullable1 = e.Row.Completed;
            bool flag3 = false;
            if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
            {
              nullable1 = e.Row.IsExpired;
              bool flag4 = false;
              if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
              {
                nullable2 = e.Row.ExpireDate;
                DateTime? orderDate = e.Row.OrderDate;
                if ((nullable2.HasValue & orderDate.HasValue ? (nullable2.GetValueOrDefault() >= orderDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                {
                  num = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache.GetStatus((object) e.Row) != 2 ? 1 : 0;
                  goto label_13;
                }
              }
            }
          }
        }
      }
      num = 0;
label_13:
      bool flag = num != 0;
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.expireDate>((object) e.Row, (object) e.Row.ExpireDate, flag ? (Exception) new PXSetPropertyException("The sales order has expired. Change the date of expiration, or use the Process Expired Order command.", (PXErrorLevel) 2) : (Exception) null);
    }
    ((PXAction) this.Base.addInvoice).SetVisible(!isBlanket);
    attributeAdjuster = PXCacheEx.AdjustUI(((PXSelectBase) this.Base.Adjustments).Cache, (object) null);
    chained = attributeAdjuster.For<SOAdjust.curyAdjdBilledAmt>((Action<PXUIFieldAttribute>) (a => a.Visible = !isBlanket));
    chained = chained.For<SOAdjust.curyAdjdTransferredToChildrenAmt>((Action<PXUIFieldAttribute>) (a => a.Visible = isBlanket));
    chained.For<SOAdjust.blanketNbr>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      PX.Objects.SO.SOOrder row = e.Row;
      int num5;
      if (row == null)
      {
        num5 = 0;
      }
      else
      {
        int? blanketSoAdjustCntr = row.BlanketSOAdjustCntr;
        int num6 = 0;
        num5 = blanketSoAdjustCntr.GetValueOrDefault() > num6 & blanketSoAdjustCntr.HasValue ? 1 : 0;
      }
      pxuiFieldAttribute.Visible = num5 != 0;
    }));
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.Taxes).Cache, (object) null);
    chained = attributeAdjuster.For<SOTaxTran.taxZoneID>((Action<PXUIFieldAttribute>) (a => a.Visible = isBlanket));
    chained = chained.For<SOTaxTran.taxID>((Action<PXUIFieldAttribute>) (a => a.Enabled = !isBlanket));
    chained = chained.SameFor<SOTaxTran.curyTaxableAmt>();
    chained.SameFor<SOTaxTran.curyTaxAmt>();
    if (isBlanket)
      ((PXSelectBase) this.Base.Taxes).Cache.AllowInsert = false;
    PX.Objects.SO.SOOrder row1 = e.Row;
    int num7;
    if (row1 == null)
    {
      num7 = 0;
    }
    else
    {
      nullable1 = row1.IsExpired;
      num7 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num7 == 0)
      return;
    ((PXSelectBase) this.Base.Document).Cache.AllowDelete = false;
    ((PXSelectBase) this.Base.Transactions).AllowDelete = false;
    ((PXSelectBase) this.Base.Transactions).AllowInsert = false;
    ((PXAction) this.Base.addInvoice).SetEnabled(false);
    if (!((OrderedDictionary) ((PXGraph) this.Base).Actions).Contains((object) "showMatrixPanel"))
      return;
    ((PXGraph) this.Base).Actions["showMatrixPanel"].SetEnabled(false);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOOrder> e)
  {
    if (!(e.Row.Behavior == "BL"))
      return;
    if (((PXSelectBase<SOBlanketOrderLink>) this.BlanketOrderChildrenList).SelectSingle(Array.Empty<object>()) != null)
      throw new PXException("Cannot delete the {0} sales order. There are one or more child orders that are generated for this sales order.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr
      });
    if (((IQueryable<PXResult<SOAdjust>>) ((PXSelectBase<SOAdjust>) this.Base.Adjustments).Select(Array.Empty<object>())).Any<PXResult<SOAdjust>>())
      throw new PXException("Cannot delete the {0} sales order. There are one or more payments linked to this sales order. To delete the sales order, remove the payment application.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(e.Operation, (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    this.VerifyExpireDate(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder>>) e).Cache, e.Row, e.Row.ExpireDate, true);
  }

  private void VerifyExpireDate(PXCache cache, PX.Objects.SO.SOOrder row, DateTime? val, bool persist)
  {
    if (row.Behavior != "BL")
      return;
    DateTime? nullable1 = val;
    DateTime? nullable2 = row.OrderDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      string str = "The date of the expiration cannot be earlier the date of the sales order.";
      if (cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.expireDate>((object) row, (object) val, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 4)) & persist)
        throw new PXRowPersistingException(typeof (PX.Objects.SO.SOOrder.expireDate).Name, (object) val, str);
    }
    else
    {
      nullable2 = val;
      nullable1 = ((PXGraph) this.Base).Accessinfo.BusinessDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
      bool? nullable3 = row.Hold;
      bool flag1 = false;
      if (!(nullable3.GetValueOrDefault() == flag1 & nullable3.HasValue))
        return;
      nullable3 = row.Cancelled;
      bool flag2 = false;
      if (!(nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue))
        return;
      nullable3 = row.Completed;
      bool flag3 = false;
      if (!(nullable3.GetValueOrDefault() == flag3 & nullable3.HasValue))
        return;
      nullable3 = row.IsExpired;
      bool flag4 = false;
      if (!(nullable3.GetValueOrDefault() == flag4 & nullable3.HasValue) || ((PXGraph) this.Base).UnattendedMode)
        return;
      string str = "Changes cannot be saved. The sales order has expired. To save the changes, change the expiration date.";
      if (cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.expireDate>((object) row, (object) val, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 4)) & persist)
        throw new PXRowPersistingException(typeof (PX.Objects.SO.SOOrder.expireDate).Name, (object) val, str);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled> e)
  {
    if (!(e.Row.Behavior == "BL") || !((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>, PX.Objects.SO.SOOrder, object>) e).NewValue).GetValueOrDefault())
      return;
    if (((PXSelectBase<SOBlanketOrderLink>) this.BlanketOrderChildrenList).SelectSingle(Array.Empty<object>()) != null)
      throw new PXException("Cannot cancel the {0} sales order because one or more child orders are generated for this sales order.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr
      });
    if (((IQueryable<PXResult<SOAdjust>>) ((PXSelectBase<SOAdjust>) this.Base.Adjustments).Select(Array.Empty<object>())).Any<PXResult<SOAdjust>>())
      throw new PXException("Cannot cancel the {0} sales order. There are one or more payments linked to this sales order. To cancel the sales order, remove the payment application.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID> e)
  {
    int? blanketLineCntr = e.Row.BlanketLineCntr;
    int num = 0;
    if (!(blanketLineCntr.GetValueOrDefault() > num & blanketLineCntr.HasValue) || object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID>, PX.Objects.SO.SOOrder, object>) e).NewValue))
      return;
    this.Base.RaiseCustomerIDSetPropertyException(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerID>, PX.Objects.SO.SOOrder, object>) e).NewValue, "Customer cannot be changed. The sales order contains lines linked to a blanket sales order.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerLocationID> e)
  {
    this._updateLineCustomerLocation = true;
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.customerLocationID>>) e).ExternalCall || !(e.Row.Behavior == "BL") || this.Base.CustomerChanged || !((IQueryable<PXResult<PX.Objects.SO.SOLine>>) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>())).Any<PXResult<PX.Objects.SO.SOLine>>() || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Ask("Customer Location", "The customer location has been changed. Do you want to update the value of the Ship-To Location column in the sales order lines?", (MessageButtons) 4) != 7)
      return;
    this._updateLineCustomerLocation = false;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taxZoneID> e)
  {
    if (!(e.Row.Behavior == "BL") || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null || !(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.TaxZoneID == (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taxZoneID>, PX.Objects.SO.SOLine, object>) e).NewValue) || !ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.TaxZoneID))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taxZoneID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID> e)
  {
    if (object.Equals(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue, e.OldValue))
      return;
    if (e.Row.Behavior == "BL")
    {
      int? childLineCntr = e.Row.ChildLineCntr;
      int num = 0;
      if (childLineCntr.GetValueOrDefault() > num & childLineCntr.HasValue)
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, e.OldValue as int?)?.SiteCD;
        throw new PXSetPropertyException("Cannot change the warehouse because there are one or more child orders generated for this sales order line.");
      }
    }
    if (e.Row.BlanketNbr != null)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, e.OldValue as int?)?.SiteCD;
      throw new PXSetPropertyException("Cannot change the warehouse because the line is linked to a line of the {0} blanket sales order.", new object[1]
      {
        (object) e.Row.BlanketNbr
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e)
  {
    bool flag1 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.customerLocationID>((object) e.Row, (object) e.OldRow);
    bool flag2 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.taxZoneID>((object) e.Row, (object) e.OldRow);
    bool flag3 = ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, e.Row?.TaxZoneID);
    bool flag4 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.shipVia, PX.Objects.SO.SOOrder.fOBPoint, PX.Objects.SO.SOOrder.shipTermsID, PX.Objects.SO.SOOrder.shipZoneID>((object) e.Row, (object) e.OldRow);
    if (e.Row.Behavior == "BL")
    {
      bool flag5 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.overrideTaxZone>((object) e.Row, (object) e.OldRow);
      DateTime? nullable1 = e.Row.OrderDate;
      DateTime? nullable2 = e.OldRow.OrderDate;
      int num;
      if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        nullable2 = e.Row.OrderDate;
        nullable1 = e.OldRow.OrderDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        {
          nullable1 = e.OldRow.OrderDate;
          if (nullable1.HasValue)
            goto label_4;
        }
        num = 1;
        goto label_10;
      }
label_4:
      nullable1 = e.Row.ExpireDate;
      nullable2 = e.OldRow.ExpireDate;
      if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        nullable2 = e.Row.ExpireDate;
        nullable1 = e.OldRow.ExpireDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        {
          nullable1 = e.OldRow.ExpireDate;
          num = !nullable1.HasValue ? 1 : 0;
        }
        else
          num = 1;
      }
      else
        num = 0;
label_10:
      bool flag6 = num != 0;
      if (flag2)
      {
        foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
        {
          PX.Objects.SO.SOLine soline = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
          if (flag3)
          {
            ((PXSelectBase) this.Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.taxZoneID>((object) soline, (object) e.Row?.TaxZoneID);
            GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) soline, true);
          }
          else if (!e.Row.OverrideTaxZone.GetValueOrDefault())
          {
            this.ReDefaultLineTaxZone(soline);
          }
          else
          {
            soline.TaxZoneID = e.Row.TaxZoneID;
            ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soline);
          }
        }
      }
      else if (flag5 && e.Row.TaxZoneID == null && !e.Row.OverrideTaxZone.GetValueOrDefault())
      {
        foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
          this.ReDefaultLineTaxZone(PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult));
      }
      if (flag1 && this._updateLineCustomerLocation)
      {
        foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
        {
          PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
          soLine.CustomerLocationID = e.Row.CustomerLocationID;
          ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine);
        }
      }
      if (!flag6)
        return;
      foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult), true);
      foreach (object obj in GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())))
        GraphHelper.MarkUpdated(((PXSelectBase) this.Base.splits).Cache, obj, true);
    }
    else
    {
      if (flag2 & flag3)
      {
        foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
        {
          PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.taxZoneID>((object) soLine, (object) e.Row?.TaxZoneID);
          GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) soLine, true);
        }
      }
      if (!(flag1 | flag4))
        return;
      foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
      {
        PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
        soLine.CustomerLocationID = e.Row.CustomerLocationID;
        soLine.ShipVia = e.Row.ShipVia;
        soLine.FOBPoint = e.Row.FOBPoint;
        soLine.ShipTermsID = e.Row.ShipTermsID;
        soLine.ShipZoneID = e.Row.ShipZoneID;
        GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) soLine, true);
      }
    }
  }

  protected virtual void LateSOOrderUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<PX.Objects.SO.SOOrder.cancelled>(e.Row, e.OldRow))
      return;
    foreach (PX.Objects.SO.SOLine selectSibling in PXParentAttribute.SelectSiblings(((PXSelectBase) this.Base.Transactions).Cache, (object) null, typeof (PX.Objects.SO.SOOrder)))
    {
      PX.Objects.SO.SOLine copy = PXCache<PX.Objects.SO.SOLine>.CreateCopy(selectSibling);
      selectSibling.Cancelled = ((PX.Objects.SO.SOOrder) e.Row).Cancelled;
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) selectSibling, true);
      if (!string.IsNullOrEmpty(selectSibling.BlanketNbr))
        this.OnChildSOLineUpdated(copy, selectSibling);
    }
  }

  private void ReDefaultLineTaxZone(PX.Objects.SO.SOLine soline)
  {
    object taxZoneID;
    ((PXSelectBase) this.Base.Transactions).Cache.RaiseFieldDefaulting<PX.Objects.SO.SOLine.taxZoneID>((object) soline, ref taxZoneID);
    if (taxZoneID != null && ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, (string) taxZoneID))
      taxZoneID = (object) null;
    soline.TaxZoneID = (string) taxZoneID;
    ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soline);
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOAdjust> e)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    if (!(current?.Behavior == "BL") || !current.IsExpired.GetValueOrDefault())
      return;
    SOAdjust row = e.Row;
    int num1;
    if (row == null)
    {
      num1 = 0;
    }
    else
    {
      Decimal? curyAdjdAmt = row.CuryAdjdAmt;
      Decimal num2 = 0M;
      num1 = curyAdjdAmt.GetValueOrDefault() > num2 & curyAdjdAmt.HasValue ? 1 : 0;
    }
    if (num1 == 0)
      return;
    bool? voided = e.Row.Voided;
    bool flag = false;
    if (!(voided.GetValueOrDefault() == flag & voided.HasValue) || !string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<SOAdjust.curyAdjdAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOAdjust>>) e).Cache, (object) e.Row)))
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOAdjust>>) e).Cache.RaiseExceptionHandling<SOAdjust.curyAdjdAmt>((object) e.Row, (object) e.Row.CuryAdjdAmt, (Exception) new PXSetPropertyException("This sales order with the Expired status has payments or prepayments applied. Make sure that the application is valid for this sales order.", (PXErrorLevel) 3));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerLocationID> e)
  {
    if (!(e.Row.Behavior == "BL"))
      return;
    PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    if ((current1 != null ? (!current1.OverrideTaxZone.GetValueOrDefault() ? 1 : 0) : 1) != 0 && !ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.TaxZoneID))
    {
      PX.Objects.SO.SOOrder current2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      if ((current2 != null ? (!current2.ExternalTaxesImportInProgress.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerLocationID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.taxZoneID>((object) e.Row);
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerLocationID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.shipVia>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerLocationID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.fOBPoint>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerLocationID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.shipTermsID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerLocationID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.shipZoneID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerLocationID>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.shipComplete>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipComplete> e)
  {
    if (!(e.Row?.Behavior == "BL") || ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipComplete>>) e).Cancel)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipComplete>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) PX.Objects.CR.Location.PK.Find((PXGraph) this.Base, e.Row.CustomerID, e.Row.CustomerLocationID)?.CShipComplete;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipComplete>>) e).Cancel = ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipComplete>, PX.Objects.SO.SOLine, object>) e).NewValue != null;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taxZoneID> e)
  {
    if (e.Row.Behavior == "BL")
    {
      PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      if ((current != null ? (!current.OverrideTaxZone.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taxZoneID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) this.GetDefaultLocationTaxZone(e.Row.CustomerID, e.Row.CustomerLocationID, (int?) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.BranchID);
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.taxZoneID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.TaxZoneID;
  }

  public virtual string GetDefaultLocationTaxZone(
    int? customerID,
    int? customerLocationID,
    int? branchID)
  {
    PX.Objects.CR.Location customerLocation = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXViewOf<PX.Objects.CR.Location>.BasedOn<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Location.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) customerID,
      (object) customerLocationID
    }));
    return this.Base.GetDefaultTaxZone(customerLocation, false, customerLocation?.CCarrierID, branchID);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipVia> e)
  {
    if (!(e.Row?.Behavior == "BL"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipVia>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).SelectSingle(new object[1]
    {
      (object) e.Row.CustomerLocationID
    })?.CCarrierID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipVia>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.fOBPoint> e)
  {
    if (!(e.Row?.Behavior == "BL"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.fOBPoint>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).SelectSingle(new object[1]
    {
      (object) e.Row.CustomerLocationID
    })?.CFOBPointID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.fOBPoint>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipTermsID> e)
  {
    if (!(e.Row?.Behavior == "BL"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipTermsID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).SelectSingle(new object[1]
    {
      (object) e.Row.CustomerLocationID
    })?.CShipTermsID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipTermsID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipZoneID> e)
  {
    if (!(e.Row?.Behavior == "BL"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipZoneID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).SelectSingle(new object[1]
    {
      (object) e.Row.CustomerLocationID
    })?.CShipZoneID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.shipZoneID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLine> e, PXRowSelected baseHandler)
  {
    baseHandler?.Invoke(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Args);
    if (e.Row == null)
      return;
    bool flag1 = e.Row?.Behavior == "BL";
    int num1 = !string.IsNullOrEmpty(e.Row?.BlanketNbr) ? 1 : 0;
    int num2;
    if (flag1)
    {
      int? childLineCntr = e.Row.ChildLineCntr;
      int num3 = 0;
      num2 = childLineCntr.GetValueOrDefault() > num3 & childLineCntr.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag2 = num2 != 0;
    int num4 = flag2 ? 1 : 0;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster;
    if ((num1 | num4) != 0)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row);
      attributeAdjuster.For<PX.Objects.SO.SOLine.uOM>((Action<PXUIFieldAttribute>) (a => a.Enabled = false)).SameFor<PX.Objects.SO.SOLine.inventoryID>();
    }
    if (flag2)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row);
      PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<PX.Objects.SO.SOLine.curyUnitPrice>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
      chained = chained.SameFor<PX.Objects.SO.SOLine.curyExtPrice>();
      chained.SameFor<PX.Objects.SO.SOLine.manualPrice>();
    }
    bool? nullable1;
    Decimal? nullable2;
    if (flag1)
    {
      attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row);
      attributeAdjuster.For<PX.Objects.SO.SOLine.pOSiteID>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
      PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      int num5;
      if (current == null)
      {
        num5 = 0;
      }
      else
      {
        nullable1 = current.IsExpired;
        num5 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      if (num5 != 0)
      {
        PXSetPropertyException propertyException = (PXSetPropertyException) null;
        nullable2 = e.Row.LineQtyHardAvail;
        if (!nullable2.HasValue)
        {
          object obj = (object) null;
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.RaiseFieldSelecting(this.Base.ItemAvailabilityExt.StatusField.Name, (object) e.Row, ref obj, false);
        }
        nullable2 = e.Row.LineQtyHardAvail;
        Decimal num6 = 0M;
        if (nullable2.GetValueOrDefault() > num6 & nullable2.HasValue)
        {
          propertyException = new PXSetPropertyException("One or more stock items are allocated for the line.", (PXErrorLevel) 3);
        }
        else
        {
          nullable1 = e.Row.POCreated;
          if (nullable1.GetValueOrDefault())
          {
            PXView view = ((PXSelectBase) this.Base.splits).View;
            object[] objArray1 = (object[]) new PX.Objects.SO.SOLine[1]
            {
              e.Row
            };
            object[] objArray2 = Array.Empty<object>();
            foreach (PX.Objects.SO.SOLineSplit soLineSplit in view.SelectMultiBound(objArray1, objArray2))
            {
              nullable1 = soLineSplit.Completed;
              if (!nullable1.GetValueOrDefault() && !string.IsNullOrEmpty(soLineSplit.PONbr))
              {
                propertyException = new PXSetPropertyException("The line is linked to a line of the {0} purchase order.", (PXErrorLevel) 3, new object[1]
                {
                  (object) soLineSplit.PONbr
                });
                break;
              }
            }
          }
        }
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOCreateDate>((object) e.Row, (object) e.Row.ExpireDate, (Exception) propertyException);
      }
      else if (string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<PX.Objects.SO.SOLine.pOCreateDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row)))
      {
        PXSetPropertyException propertyException = (PXSetPropertyException) null;
        nullable1 = e.Row.POCreate;
        if (nullable1.GetValueOrDefault())
        {
          DateTime? poCreateDate = e.Row.POCreateDate;
          DateTime? schedOrderDate = e.Row.SchedOrderDate;
          if ((poCreateDate.HasValue & schedOrderDate.HasValue ? (poCreateDate.GetValueOrDefault() > schedOrderDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            propertyException = new PXSetPropertyException("The date of purchase order creation is greater than the scheduled date of order creation for the line ({0}).", (PXErrorLevel) 2, new object[1]
            {
              (object) e.Row.SchedOrderDate.Value.ToShortDateString()
            });
        }
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOCreateDate>((object) e.Row, (object) e.Row.ExpireDate, (Exception) propertyException);
      }
    }
    int num7;
    if (flag1)
    {
      nullable1 = e.Row.Completed;
      if (!nullable1.GetValueOrDefault())
      {
        nullable2 = e.Row.BlanketOpenQty;
        Decimal num8 = 0M;
        num7 = nullable2.GetValueOrDefault() > num8 & nullable2.HasValue ? 1 : (e.Row.LineType == "MI" ? 1 : 0);
        goto label_37;
      }
    }
    num7 = 0;
label_37:
    bool isBlanketOpenLine = num7 != 0;
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.Transactions).Cache, (object) e.Row);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained1 = attributeAdjuster.For<PX.Objects.SO.SOLine.customerOrderNbr>((Action<PXUIFieldAttribute>) (a => a.Enabled = isBlanketOpenLine));
    chained1 = chained1.SameFor<PX.Objects.SO.SOLine.schedOrderDate>();
    chained1 = chained1.SameFor<PX.Objects.SO.SOLine.schedShipDate>();
    chained1 = chained1.SameFor<PX.Objects.SO.SOLine.pOCreateDate>();
    chained1 = chained1.SameFor<PX.Objects.SO.SOLine.customerLocationID>();
    chained1 = chained1.SameFor<PX.Objects.SO.SOLine.shipVia>();
    chained1 = chained1.SameFor<PX.Objects.SO.SOLine.fOBPoint>();
    chained1 = chained1.SameFor<PX.Objects.SO.SOLine.shipTermsID>();
    chained1.SameFor<PX.Objects.SO.SOLine.shipZoneID>();
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.Transactions).Cache, (object) e.Row);
    attributeAdjuster.For<PX.Objects.SO.SOLine.taxZoneID>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      int num9 = (current != null ? (!current.OverrideTaxZone.GetValueOrDefault() ? 1 : 0) : 1) & (isBlanketOpenLine ? 1 : 0);
      pxuiFieldAttribute.Enabled = num9 != 0;
    }));
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty> e)
  {
    if (e.Row.Behavior == "BL")
    {
      Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue;
      Decimal? qtyOnOrders = e.Row.QtyOnOrders;
      if (newValue.GetValueOrDefault() < qtyOnOrders.GetValueOrDefault() & newValue.HasValue & qtyOnOrders.HasValue)
      {
        object[] objArray = new object[1];
        qtyOnOrders = e.Row.QtyOnOrders;
        objArray[0] = (object) qtyOnOrders.Value.ToString("0.####");
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", objArray);
      }
    }
    else
    {
      if (string.IsNullOrEmpty(e.Row.BlanketNbr) || !(e.Row.LineType != "MI"))
        return;
      Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue;
      Decimal? orderQty1 = e.Row.OrderQty;
      Decimal? nullable1 = newValue.HasValue & orderQty1.HasValue ? new Decimal?(newValue.GetValueOrDefault() - orderQty1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = nullable1;
      Decimal num1 = 0M;
      if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
      {
        object[] source = PXParentAttribute.SelectChildren(((PXSelectBase) this.Base.splits).Cache, (object) e.Row, typeof (PX.Objects.SO.SOLine));
        if (this.GetOrigChildSplit(e.Row, source.Cast<PX.Objects.SO.SOLineSplit>()) != null)
          throw new PXSetPropertyException("Cannot change the quantity in the line because the allocation has not been transferred from linked {0} blanket sales order line. To change line quantity delete the line, add blanket SO line via the Add Blanket SO Line popup and change the quantity.", new object[1]
          {
            (object) e.Row.BlanketNbr
          });
      }
      nullable2 = nullable1;
      Decimal num2 = 0M;
      if (!(nullable2.GetValueOrDefault() > num2 & nullable2.HasValue) || !((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>>) e).ExternalCall)
        return;
      BlanketSOLineSplit blanketSoLineSplit = this.SelectParentSplit(e.Row);
      nullable2 = nullable1;
      Decimal? blanketOpenQty = blanketSoLineSplit.BlanketOpenQty;
      if (!(nullable2.GetValueOrDefault() > blanketOpenQty.GetValueOrDefault() & nullable2.HasValue & blanketOpenQty.HasValue))
        return;
      PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty> fieldVerifying = e;
      Decimal? orderQty2 = e.Row.OrderQty;
      nullable2 = blanketSoLineSplit.BlanketOpenQty;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) (orderQty2.HasValue & nullable2.HasValue ? new Decimal?(orderQty2.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?());
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) fieldVerifying).NewValue = (object) local;
      ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue, (Exception) new PXSetPropertyException("The quantity exceeds the open quantity in the line details of the linked line in the {0} blanket sales order.", new object[1]
      {
        (object) blanketSoLineSplit.OrderNbr
      }));
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine> e)
  {
    if (!(e.Row.Behavior == "BL") || !EnumerableExtensions.IsIn<PXDBOperation>(e.Operation, (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    DateTime? nullable1 = e.Row.SchedOrderDate;
    DateTime? nullable2 = (DateTime?) current?.OrderDate;
    string str1;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      nullable2 = e.Row.SchedOrderDate;
      nullable1 = (DateTime?) current?.ExpireDate;
      str1 = (nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? "The date of the child sales order cannot be later than the date of the expiration specified in the blanket sales order." : (string) null;
    }
    else
      str1 = "The date of the child sales order cannot be earlier than the date of the blanket sales order.";
    string str2 = str1;
    if (!string.IsNullOrEmpty(str2) && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.schedOrderDate>((object) e.Row, (object) e.Row.SchedOrderDate, (Exception) new PXSetPropertyException(str2, (PXErrorLevel) 4)))
      throw new PXRowPersistingException(typeof (PX.Objects.SO.SOLine.schedOrderDate).Name, (object) e.Row.SchedOrderDate, str2);
    nullable1 = e.Row.SchedShipDate;
    nullable2 = e.Row.SchedOrderDate;
    string str3;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      nullable2 = e.Row.SchedOrderDate;
      if (!nullable2.HasValue)
      {
        nullable2 = e.Row.SchedShipDate;
        nullable1 = (DateTime?) current?.OrderDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          str3 = "The date of the scheduled shipment cannot be earlier than the date of the blanket sales order.";
          goto label_12;
        }
      }
      nullable1 = e.Row.SchedShipDate;
      nullable2 = (DateTime?) current?.ExpireDate;
      str3 = (nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? "The date of the scheduled shipment cannot be later than the date of the expiration specified in the blanket sales order." : (string) null;
    }
    else
      str3 = "The date of the scheduled shipment cannot be earlier than the scheduled date of the child sales order.";
label_12:
    string str4 = str3;
    if (!string.IsNullOrEmpty(str4) && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.schedShipDate>((object) e.Row, (object) e.Row.SchedShipDate, (Exception) new PXSetPropertyException(str4, (PXErrorLevel) 4)))
      throw new PXRowPersistingException(typeof (PX.Objects.SO.SOLine.schedShipDate).Name, (object) e.Row.SchedShipDate, str4);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.SO.SOLine> e)
  {
    if (string.IsNullOrEmpty(e.Row.BlanketNbr) || e.Operation != 3)
      return;
    PXTranStatus tranStatus = e.TranStatus;
    if (tranStatus != null)
    {
      if (tranStatus != 2)
        return;
      this.ClearReturnReceivedAllocationsToBlanket();
    }
    else
      this.ReturnReceivedAllocationsToBlanket(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLineSplit> e)
  {
    if (!(e.Row?.Behavior == "BL"))
      return;
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (e.Row.POCreate.GetValueOrDefault())
    {
      DateTime? poCreateDate = e.Row.POCreateDate;
      DateTime? schedOrderDate = e.Row.SchedOrderDate;
      if ((poCreateDate.HasValue & schedOrderDate.HasValue ? (poCreateDate.GetValueOrDefault() > schedOrderDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        object[] objArray = new object[1];
        schedOrderDate = e.Row.SchedOrderDate;
        objArray[0] = (object) schedOrderDate.Value.ToShortDateString();
        propertyException = new PXSetPropertyException("The date of purchase order creation is greater than the scheduled date of order creation for the line ({0}).", (PXErrorLevel) 2, objArray);
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLineSplit>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOCreateDate>((object) e.Row, (object) e.Row.ExpireDate, (Exception) propertyException);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLineSplit>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.schedOrderDate>((object) e.Row, (object) e.Row.SchedOrderDate, (Exception) this.GetSchedOrderDateException(e.Row, current));
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLineSplit>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.schedShipDate>((object) e.Row, (object) e.Row.SchedShipDate, (Exception) this.GetSchedShipDateException(e.Row, current));
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.splits).Cache, (object) e.Row).For<PX.Objects.SO.SOLineSplit.customerOrderNbr>((Action<PXUIFieldAttribute>) (a => a.Enabled = !e.Row.Completed.GetValueOrDefault()));
    chained = chained.SameFor<PX.Objects.SO.SOLineSplit.schedOrderDate>();
    chained = chained.SameFor<PX.Objects.SO.SOLineSplit.schedShipDate>();
    chained.SameFor<PX.Objects.SO.SOLineSplit.pOCreateDate>();
  }

  private PXException GetSchedOrderDateException(PX.Objects.SO.SOLineSplit s, PX.Objects.SO.SOOrder order)
  {
    DateTime? schedOrderDate = s.SchedOrderDate;
    DateTime? nullable = (DateTime?) order?.OrderDate;
    string str1;
    if ((schedOrderDate.HasValue & nullable.HasValue ? (schedOrderDate.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      nullable = s.SchedOrderDate;
      DateTime? expireDate = (DateTime?) order?.ExpireDate;
      str1 = (nullable.HasValue & expireDate.HasValue ? (nullable.GetValueOrDefault() > expireDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? "The date of the child sales order cannot be later than the date of the expiration specified in the blanket sales order." : (string) null;
    }
    else
      str1 = "The date of the child sales order cannot be earlier than the date of the blanket sales order.";
    string str2 = str1;
    return !string.IsNullOrEmpty(str2) ? (PXException) new PXSetPropertyException(str2, (PXErrorLevel) 4) : (PXException) null;
  }

  private PXException GetSchedShipDateException(PX.Objects.SO.SOLineSplit s, PX.Objects.SO.SOOrder order)
  {
    DateTime? schedShipDate = s.SchedShipDate;
    DateTime? nullable1 = s.SchedOrderDate;
    string str1;
    if ((schedShipDate.HasValue & nullable1.HasValue ? (schedShipDate.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      nullable1 = s.SchedOrderDate;
      DateTime? nullable2;
      if (!nullable1.HasValue)
      {
        nullable1 = s.SchedShipDate;
        nullable2 = (DateTime?) order?.OrderDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          str1 = "The date of the scheduled shipment cannot be earlier than the date of the blanket sales order.";
          goto label_6;
        }
      }
      nullable2 = s.SchedShipDate;
      nullable1 = (DateTime?) order?.ExpireDate;
      str1 = (nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? "The date of the scheduled shipment cannot be later than the date of the expiration specified in the blanket sales order." : (string) null;
    }
    else
      str1 = "The date of the scheduled shipment cannot be earlier than the scheduled date of the child sales order.";
label_6:
    string str2 = str1;
    return !string.IsNullOrEmpty(str2) ? (PXException) new PXSetPropertyException(str2, (PXErrorLevel) 4) : (PXException) null;
  }

  private DateTime? CalcSchedOrderDate(PX.Objects.SO.SOLineSplit s)
  {
    if (s.Behavior == "BL")
    {
      bool? completed = s.Completed;
      bool flag = false;
      if (completed.GetValueOrDefault() == flag & completed.HasValue)
      {
        Decimal? qty = s.Qty;
        Decimal? qtyOnOrders = s.QtyOnOrders;
        Decimal? receivedQty = s.ReceivedQty;
        Decimal? nullable = qtyOnOrders.HasValue & receivedQty.HasValue ? new Decimal?(qtyOnOrders.GetValueOrDefault() + receivedQty.GetValueOrDefault()) : new Decimal?();
        if (qty.GetValueOrDefault() > nullable.GetValueOrDefault() & qty.HasValue & nullable.HasValue)
          return s.SchedOrderDate;
      }
    }
    return new DateTime?();
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOLineSplit> e)
  {
    this.OnSchedOrderDateUpdated(new DateTime?(), this.CalcSchedOrderDate(e.Row));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLineSplit> e)
  {
    this.OnSchedOrderDateUpdated(this.CalcSchedOrderDate(e.OldRow), this.CalcSchedOrderDate(e.Row));
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOLineSplit> e)
  {
    this.OnSchedOrderDateUpdated(this.CalcSchedOrderDate(e.Row), new DateTime?());
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLineSplit> e)
  {
    if (e.Row.Behavior != "BL" || EnumerableExtensions.IsNotIn<PXDBOperation>(e.Operation, (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    PXException orderDateException = this.GetSchedOrderDateException(e.Row, current);
    if (orderDateException != null && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLineSplit>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.schedOrderDate>((object) e.Row, (object) e.Row.SchedOrderDate, (Exception) orderDateException))
      throw new PXRowPersistingException(typeof (PX.Objects.SO.SOLineSplit.schedOrderDate).Name, (object) e.Row.SchedOrderDate, orderDateException.MessageNoPrefix);
    PXException shipDateException = this.GetSchedShipDateException(e.Row, current);
    if (shipDateException != null && ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLineSplit>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.schedShipDate>((object) e.Row, (object) e.Row.SchedShipDate, (Exception) shipDateException))
      throw new PXRowPersistingException(typeof (PX.Objects.SO.SOLineSplit.schedShipDate).Name, (object) e.Row.SchedShipDate, shipDateException.MessageNoPrefix);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.qty> e)
  {
    if (!(e.Row.Behavior == "BL"))
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.qty>, PX.Objects.SO.SOLineSplit, object>) e).NewValue;
    Decimal? qtyOnOrders1 = e.Row.QtyOnOrders;
    Decimal? receivedQty1 = e.Row.ReceivedQty;
    Decimal? nullable = qtyOnOrders1.HasValue & receivedQty1.HasValue ? new Decimal?(qtyOnOrders1.GetValueOrDefault() + receivedQty1.GetValueOrDefault()) : new Decimal?();
    if (newValue.GetValueOrDefault() < nullable.GetValueOrDefault() & newValue.HasValue & nullable.HasValue)
    {
      object[] objArray = new object[1];
      Decimal? qtyOnOrders2 = e.Row.QtyOnOrders;
      Decimal? receivedQty2 = e.Row.ReceivedQty;
      objArray[0] = (object) (qtyOnOrders2.HasValue & receivedQty2.HasValue ? new Decimal?(qtyOnOrders2.GetValueOrDefault() + receivedQty2.GetValueOrDefault()) : new Decimal?()).Value.ToString("0.####");
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", objArray);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<BlanketSOLineSplit> e)
  {
    Decimal? blanketOpenQty = e.Row.BlanketOpenQty;
    Decimal num = 0M;
    if (blanketOpenQty.GetValueOrDefault() >= num & blanketOpenQty.HasValue)
    {
      if (e.Row.LineType == "MI")
        return;
      Decimal? qty = e.Row.Qty;
      Decimal? qtyOnOrders = e.Row.QtyOnOrders;
      Decimal? receivedQty = e.Row.ReceivedQty;
      Decimal? nullable = qtyOnOrders.HasValue & receivedQty.HasValue ? new Decimal?(qtyOnOrders.GetValueOrDefault() + receivedQty.GetValueOrDefault()) : new Decimal?();
      if (qty.GetValueOrDefault() >= nullable.GetValueOrDefault() & qty.HasValue & nullable.HasValue)
        return;
    }
    foreach (PX.Objects.SO.SOLine soLine in GraphHelper.RowCast<PX.Objects.SO.SOLine>(NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Base.Transactions).Cache.Updated, ((PXSelectBase) this.Base.Transactions).Cache.Inserted)).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
    {
      if (l.BlanketType == e.Row.OrderType && l.BlanketNbr == e.Row.OrderNbr)
      {
        int? blanketLineNbr = l.BlanketLineNbr;
        int? nullable = e.Row.LineNbr;
        if (blanketLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & blanketLineNbr.HasValue == nullable.HasValue)
        {
          nullable = l.BlanketSplitLineNbr;
          int? splitLineNbr = e.Row.SplitLineNbr;
          return nullable.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & nullable.HasValue == splitLineNbr.HasValue;
        }
      }
      return false;
    })))
      ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) soLine, (object) soLine.OrderQty, (Exception) new PXException("The quantity exceeds the open quantity in the line details of the linked line in the {0} blanket sales order.", new object[1]
      {
        (object) e.Row.OrderNbr
      }));
    throw new PXRowPersistingException(typeof (BlanketSOLine.blanketOpenQty).Name, (object) e.Row.BlanketOpenQty, "The quantity exceeds the open quantity in the line details of the linked line in the {0} blanket sales order.", new object[1]
    {
      (object) e.Row.OrderNbr
    });
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine> e)
  {
    if (e.Row.Behavior == "BL")
    {
      DateTime? schedShipDate1 = e.Row.SchedShipDate;
      DateTime? schedShipDate2 = e.OldRow.SchedShipDate;
      if ((schedShipDate1.HasValue == schedShipDate2.HasValue ? (schedShipDate1.HasValue ? (schedShipDate1.GetValueOrDefault() != schedShipDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        schedShipDate2 = e.Row.SchedShipDate;
        schedShipDate1 = e.OldRow.SchedShipDate;
        if ((schedShipDate2.HasValue & schedShipDate1.HasValue ? (schedShipDate2.GetValueOrDefault() < schedShipDate1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        {
          schedShipDate1 = e.OldRow.SchedShipDate;
          if (schedShipDate1.HasValue)
            goto label_10;
        }
        using (List<object>.Enumerator enumerator = ((PXSelectBase) this.Base.splits).View.SelectMultiBound((object[]) new PX.Objects.SO.SOLine[1]
        {
          e.Row
        }, Array.Empty<object>()).GetEnumerator())
        {
          while (enumerator.MoveNext())
            GraphHelper.MarkUpdated(((PXSelectBase) this.Base.splits).Cache, (object) (PX.Objects.SO.SOLineSplit) enumerator.Current, true);
          return;
        }
      }
    }
label_10:
    if (string.IsNullOrEmpty(e.Row.BlanketNbr))
      return;
    this.OnChildSOLineUpdated(e.OldRow, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<BlanketSOLineSplit, BlanketSOLineSplit.effectiveChildLineCntr> e)
  {
    if (!(e.Row.LineType == "MI"))
      return;
    int? oldValue = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<BlanketSOLineSplit, BlanketSOLineSplit.effectiveChildLineCntr>, BlanketSOLineSplit, object>) e).OldValue;
    int? newValue = (int?) e.NewValue;
    int? nullable1 = oldValue;
    int? nullable2 = newValue;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    nullable2 = oldValue;
    int num1 = 0;
    if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
    {
      nullable2 = newValue;
      int num2 = 0;
      if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
        return;
    }
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<BlanketSOLineSplit, BlanketSOLineSplit.effectiveChildLineCntr>>) e).Cache;
    BlanketSOLineSplit row = e.Row;
    nullable2 = newValue;
    int num3 = 0;
    // ISSUE: variable of a boxed type
    __Boxed<bool> local = (ValueType) (nullable2.GetValueOrDefault() > num3 & nullable2.HasValue);
    cache.SetValueExt<BlanketSOLineSplit.completed>((object) row, (object) local);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<BlanketSOLineSplit> e)
  {
    if (!(e.Row.LineType == "MI") || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<BlanketSOLineSplit>>) e).Cache.ObjectsEqual<BlanketSOLineSplit.completed>((object) e.OldRow, (object) e.Row))
      return;
    BlanketSOLine blanketSoLine1 = PXParentAttribute.SelectParent<BlanketSOLine>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<BlanketSOLineSplit>>) e).Cache, (object) e.Row);
    if (blanketSoLine1 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base), new object[3]
      {
        (object) e.Row.OrderType,
        (object) e.Row.OrderNbr,
        (object) e.Row.LineNbr
      });
    if (e.Row.Completed.GetValueOrDefault())
    {
      if (GraphHelper.RowCast<BlanketSOLineSplit>((IEnumerable) PXParentAttribute.SelectChildren(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<BlanketSOLineSplit>>) e).Cache, (object) blanketSoLine1, typeof (BlanketSOLine))).All<BlanketSOLineSplit>((Func<BlanketSOLineSplit, bool>) (s => s.Completed.GetValueOrDefault())))
        blanketSoLine1.Completed = new bool?(true);
      BlanketSOLine blanketSoLine2 = blanketSoLine1;
      Decimal? closedQty = blanketSoLine2.ClosedQty;
      Decimal? qty = e.Row.Qty;
      blanketSoLine2.ClosedQty = closedQty.HasValue & qty.HasValue ? new Decimal?(closedQty.GetValueOrDefault() + qty.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      blanketSoLine1.Completed = new bool?(false);
      BlanketSOLine blanketSoLine3 = blanketSoLine1;
      Decimal? closedQty = blanketSoLine3.ClosedQty;
      Decimal? qty = e.Row.Qty;
      blanketSoLine3.ClosedQty = closedQty.HasValue & qty.HasValue ? new Decimal?(closedQty.GetValueOrDefault() - qty.GetValueOrDefault()) : new Decimal?();
    }
    GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base).Update(blanketSoLine1);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.taxZoneID> e,
    PXFieldDefaulting baseMethod)
  {
    baseMethod.Invoke(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.taxZoneID>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.taxZoneID>>) e).Args);
    if (e.Row == null || !(e.Row.Behavior == "BL"))
      return;
    if (!e.Row.OverrideTaxZone.GetValueOrDefault() && ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.taxZoneID>, PX.Objects.SO.SOOrder, object>) e).NewValue != null && !ExternalTaxBase<PXGraph>.IsExternalTax((PXGraph) this.Base, (string) ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.taxZoneID>, PX.Objects.SO.SOOrder, object>) e).NewValue))
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.taxZoneID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) null;
    }
    else
    {
      if (!e.Row.OverrideTaxZone.GetValueOrDefault())
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.taxZoneID>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) e.Row.TaxZoneID;
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable OverrideBlanketTaxZone(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current != null && ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.Behavior == "BL")
    {
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OverrideTaxZone.GetValueOrDefault() && PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>())) != null && !((PXGraph) this.Base).IsMobile && ((PXSelectBase<BlanketSOOverrideTaxZoneFilter>) this.BlanketTaxZoneOverrideFilter).Current != null && ((PXSelectBase<BlanketSOOverrideTaxZoneFilter>) this.BlanketTaxZoneOverrideFilter).AskExt() == 6)
      {
        if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.TaxZoneID != ((PXSelectBase<BlanketSOOverrideTaxZoneFilter>) this.BlanketTaxZoneOverrideFilter).Current.TaxZoneID)
        {
          PX.Objects.SO.SOOrder copy = (PX.Objects.SO.SOOrder) ((PXSelectBase) this.Base.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current);
          copy.TaxZoneID = ((PXSelectBase<BlanketSOOverrideTaxZoneFilter>) this.BlanketTaxZoneOverrideFilter).Current?.TaxZoneID;
          ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(copy);
        }
        if (((PXSelectBase<BlanketSOOverrideTaxZoneFilter>) this.BlanketTaxZoneOverrideFilter).Current.TaxZoneID == null)
        {
          foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
          {
            PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
            soLine.TaxZoneID = (string) null;
            ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine);
          }
        }
      }
      else
        ((PXSelectBase) this.Base.Document).Cache.SetValue<PX.Objects.SO.SOOrder.overrideTaxZone>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, (object) false);
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOLine> e)
  {
    PX.Objects.SO.SOLine row = e.Row;
    if (row == null || row.BlanketNbr == null || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null || !EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    if (PXResult<PX.Objects.SO.SOLine>.op_Implicit(((IQueryable<PXResult<PX.Objects.SO.SOLine>>) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>())).Where<PXResult<PX.Objects.SO.SOLine>>((Expression<Func<PXResult<PX.Objects.SO.SOLine>, bool>>) (x => ((PX.Objects.SO.SOLine) x).BlanketType == row.BlanketType && ((PX.Objects.SO.SOLine) x).BlanketNbr == row.BlanketNbr)).FirstOrDefault<PXResult<PX.Objects.SO.SOLine>>()) != null)
      return;
    SOBlanketOrderLink blanketOrderLink = PXResultset<SOBlanketOrderLink>.op_Implicit(PXSelectBase<SOBlanketOrderLink, PXViewOf<SOBlanketOrderLink>.BasedOn<SelectFromBase<SOBlanketOrderLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOBlanketOrderLink.blanketType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<SOBlanketOrderLink.blanketNbr, IBqlString>.IsEqual<P.AsString>>>>.And<KeysRelation<CompositeKey<Field<SOBlanketOrderLink.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<SOBlanketOrderLink.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, SOBlanketOrderLink>, PX.Objects.SO.SOOrder, SOBlanketOrderLink>.SameAsCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.SO.SOOrder[1]
    {
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current
    }, new object[2]
    {
      (object) row.BlanketType,
      (object) row.BlanketNbr
    }));
    if (blanketOrderLink == null)
      return;
    ((PXSelectBase<SOBlanketOrderLink>) this.BlanketOrderChildrenList).Delete(blanketOrderLink);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOBlanketOrderLink> e)
  {
    this.UpdateAdjustmentsOnBlanketLinkRemoval(e.Row);
  }

  protected virtual void UpdateAdjustmentsOnBlanketLinkRemoval(SOBlanketOrderLink link)
  {
    foreach (SOAdjust adj in GraphHelper.RowCast<SOAdjust>((IEnumerable) ((PXSelectBase<SOAdjust>) this.Base.Adjustments_Raw).Select(Array.Empty<object>())).Where<SOAdjust>((Func<SOAdjust, bool>) (adj => adj.BlanketNbr == link.BlanketNbr && adj.BlanketType == link.BlanketType)))
    {
      this.FillSOAdjustByPayment(adj);
      GraphHelper.Caches<SOAdjust>((PXGraph) this.Base).Update(adj);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOLine> e)
  {
    if (!(e.Row.Behavior == "BL"))
      return;
    int? childLineCntr = e.Row.ChildLineCntr;
    int num = 0;
    if (!(childLineCntr.GetValueOrDefault() > num & childLineCntr.HasValue))
      return;
    this.ThrowExceptionCannotDeleteBlanket(e.Row.OrderType, e.Row.OrderNbr, e.Row.LineNbr);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOLineSplit> e)
  {
    if (!(e.Row.Behavior == "BL"))
      return;
    int? childLineCntr = e.Row.ChildLineCntr;
    int num = 0;
    if (!(childLineCntr.GetValueOrDefault() > num & childLineCntr.HasValue))
      return;
    this.ThrowExceptionCannotDeleteBlanket(e.Row.OrderType, e.Row.OrderNbr, e.Row.LineNbr, e.Row.SplitLineNbr);
  }

  protected virtual void _(PX.Data.Events.RowInserted<SOOrderDiscountDetail> e)
  {
    this.CheckForLinesWithDiscountCalculationDisabled(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOOrderDiscountDetail> e)
  {
    this.CheckForLinesWithDiscountCalculationDisabled(e.Row);
  }

  protected virtual void CheckForLinesWithDiscountCalculationDisabled(
    SOOrderDiscountDetail discountDetail)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      int? blanketLineCntr = current.BlanketLineCntr;
      int num2 = 0;
      num1 = blanketLineCntr.GetValueOrDefault() > num2 & blanketLineCntr.HasValue ? 1 : 0;
    }
    if (num1 == 0)
      return;
    bool flag = false;
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      if (PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult).AutomaticDiscountsDisabled.GetValueOrDefault())
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    ((PXSelectBase) this.Base.DiscountDetails).Cache.RaiseExceptionHandling<SOOrderDiscountDetail.discountID>((object) discountDetail, (object) discountDetail.DiscountID, (Exception) new PXSetPropertyException("The sales order contains one or more lines that have not been included in calculation of group and document discounts. Verify the discounts.", (PXErrorLevel) 2));
  }

  protected virtual void ThrowExceptionCannotDeleteBlanket(
    string orderType,
    string orderNbr,
    int? lineNbr,
    int? splitNbr = null)
  {
    throw new PXException("Cannot delete the line because the line of the {0} sales order is linked to this line.", new object[1]
    {
      (object) PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.blanketType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.SO.SOLine.blanketNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.SO.SOLine.blanketLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.blanketSplitLineNbr, Equal<P.AsInt>>>>>.Or<BqlOperand<Required<Parameter.ofInt>, IBqlInt>.IsNull>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[5]
      {
        (object) orderType,
        (object) orderNbr,
        (object) lineNbr,
        (object) splitNbr,
        (object) splitNbr
      }))?.OrderNbr
    });
  }

  protected virtual void OnChildSOLineUpdated(PX.Objects.SO.SOLine oldLine, PX.Objects.SO.SOLine line)
  {
    PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
    bool flag1 = line.LineType == "MI";
    bool flag2 = !cache.ObjectsEqual<PX.Objects.SO.SOLine.cancelled>((object) oldLine, (object) line);
    bool flag3 = !cache.ObjectsEqual<PX.Objects.SO.SOLine.closedQty>((object) oldLine, (object) line);
    if (!flag2 && (!flag3 || flag1))
      return;
    int num1 = (!line.Cancelled.GetValueOrDefault() ? 1 : 0) - (!oldLine.Cancelled.GetValueOrDefault() ? 1 : 0);
    Decimal? nullable1 = !line.Cancelled.GetValueOrDefault() ? line.OrderQty : new Decimal?(0M);
    bool? nullable2 = oldLine.Cancelled;
    Decimal? nullable3 = !nullable2.GetValueOrDefault() ? oldLine.OrderQty : new Decimal?(0M);
    Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    nullable2 = line.Cancelled;
    Decimal? nullable5;
    if (nullable2.GetValueOrDefault())
    {
      nullable5 = new Decimal?(0M);
    }
    else
    {
      nullable2 = line.Completed;
      nullable5 = nullable2.GetValueOrDefault() ? line.ShippedQty : line.ClosedQty;
    }
    Decimal? nullable6 = nullable5;
    nullable2 = oldLine.Cancelled;
    Decimal? nullable7;
    if (nullable2.GetValueOrDefault())
    {
      nullable7 = new Decimal?(0M);
    }
    else
    {
      nullable2 = oldLine.Completed;
      nullable7 = nullable2.GetValueOrDefault() ? oldLine.ShippedQty : oldLine.ClosedQty;
    }
    Decimal? nullable8 = nullable7;
    Decimal? nullable9 = nullable6.HasValue & nullable8.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
    nullable2 = line.Cancelled;
    Decimal? nullable10 = !nullable2.GetValueOrDefault() ? line.ClosedQty : new Decimal?(0M);
    nullable2 = oldLine.Cancelled;
    Decimal? nullable11 = !nullable2.GetValueOrDefault() ? oldLine.ClosedQty : new Decimal?(0M);
    Decimal? nullable12 = nullable10.HasValue & nullable11.HasValue ? new Decimal?(nullable10.GetValueOrDefault() - nullable11.GetValueOrDefault()) : new Decimal?();
    BlanketSOLineSplit blanketSoLineSplit1 = this.SelectParentSplit(line);
    BlanketSOLineSplit blanketSoLineSplit2 = blanketSoLineSplit1;
    int? effectiveChildLineCntr = blanketSoLineSplit2.EffectiveChildLineCntr;
    int num2 = num1;
    blanketSoLineSplit2.EffectiveChildLineCntr = effectiveChildLineCntr.HasValue ? new int?(effectiveChildLineCntr.GetValueOrDefault() + num2) : new int?();
    BlanketSOLineSplit blanketSoLineSplit3 = blanketSoLineSplit1;
    Decimal? qtyOnOrders1 = blanketSoLineSplit3.QtyOnOrders;
    Decimal? nullable13 = nullable4;
    blanketSoLineSplit3.QtyOnOrders = qtyOnOrders1.HasValue & nullable13.HasValue ? new Decimal?(qtyOnOrders1.GetValueOrDefault() + nullable13.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable14;
    if (!flag1)
    {
      BlanketSOLineSplit blanketSoLineSplit4 = blanketSoLineSplit1;
      nullable13 = blanketSoLineSplit4.ShippedQty;
      Decimal? nullable15 = nullable9;
      blanketSoLineSplit4.ShippedQty = nullable13.HasValue & nullable15.HasValue ? new Decimal?(nullable13.GetValueOrDefault() + nullable15.GetValueOrDefault()) : new Decimal?();
      BlanketSOLineSplit blanketSoLineSplit5 = blanketSoLineSplit1;
      Decimal? closedQty1 = blanketSoLineSplit5.ClosedQty;
      nullable13 = nullable12;
      blanketSoLineSplit5.ClosedQty = closedQty1.HasValue & nullable13.HasValue ? new Decimal?(closedQty1.GetValueOrDefault() + nullable13.GetValueOrDefault()) : new Decimal?();
      nullable13 = nullable12;
      Decimal num3 = 0M;
      if (nullable13.GetValueOrDefault() > num3 & nullable13.HasValue)
      {
        nullable2 = blanketSoLineSplit1.Completed;
        if (!nullable2.GetValueOrDefault())
          goto label_12;
      }
      nullable13 = nullable12;
      Decimal num4 = 0M;
      if (nullable13.GetValueOrDefault() < num4 & nullable13.HasValue)
      {
        nullable2 = blanketSoLineSplit1.Completed;
        if (!nullable2.GetValueOrDefault())
          goto label_13;
      }
      else
        goto label_13;
label_12:
      BlanketSOLineSplit blanketSoLineSplit6 = blanketSoLineSplit1;
      Decimal? closedQty2 = blanketSoLineSplit1.ClosedQty;
      nullable14 = blanketSoLineSplit1.ReceivedQty;
      nullable13 = closedQty2.HasValue & nullable14.HasValue ? new Decimal?(closedQty2.GetValueOrDefault() + nullable14.GetValueOrDefault()) : new Decimal?();
      Decimal? qty = blanketSoLineSplit1.Qty;
      bool? nullable16 = new bool?(nullable13.GetValueOrDefault() >= qty.GetValueOrDefault() & nullable13.HasValue & qty.HasValue);
      blanketSoLineSplit6.Completed = nullable16;
    }
label_13:
    GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base).Update(blanketSoLineSplit1);
    BlanketSOLine blanketSoLine1 = PXParentAttribute.SelectParent<BlanketSOLine>(cache, (object) line);
    BlanketSOLine blanketSoLine2 = blanketSoLine1 != null ? blanketSoLine1 : throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base), new object[3]
    {
      (object) line.BlanketType,
      (object) line.BlanketNbr,
      (object) line.BlanketLineNbr
    });
    Decimal? qtyOnOrders2 = blanketSoLine2.QtyOnOrders;
    nullable13 = nullable4;
    Decimal? nullable17;
    if (!(qtyOnOrders2.HasValue & nullable13.HasValue))
    {
      nullable14 = new Decimal?();
      nullable17 = nullable14;
    }
    else
      nullable17 = new Decimal?(qtyOnOrders2.GetValueOrDefault() + nullable13.GetValueOrDefault());
    blanketSoLine2.QtyOnOrders = nullable17;
    if (!flag1)
    {
      BlanketSOLine blanketSoLine3 = blanketSoLine1;
      nullable13 = blanketSoLine3.ShippedQty;
      Decimal? nullable18 = nullable9;
      Decimal? nullable19;
      if (!(nullable13.HasValue & nullable18.HasValue))
      {
        nullable14 = new Decimal?();
        nullable19 = nullable14;
      }
      else
        nullable19 = new Decimal?(nullable13.GetValueOrDefault() + nullable18.GetValueOrDefault());
      blanketSoLine3.ShippedQty = nullable19;
      BlanketSOLine blanketSoLine4 = blanketSoLine1;
      Decimal? closedQty = blanketSoLine4.ClosedQty;
      nullable13 = nullable12;
      Decimal? nullable20;
      if (!(closedQty.HasValue & nullable13.HasValue))
      {
        nullable14 = new Decimal?();
        nullable20 = nullable14;
      }
      else
        nullable20 = new Decimal?(closedQty.GetValueOrDefault() + nullable13.GetValueOrDefault());
      blanketSoLine4.ClosedQty = nullable20;
      nullable13 = blanketSoLine1.ClosedQty;
      Decimal? nullable21 = blanketSoLine1.OrderQty;
      if (nullable13.GetValueOrDefault() > nullable21.GetValueOrDefault() & nullable13.HasValue & nullable21.HasValue)
        blanketSoLine1.ClosedQty = blanketSoLine1.OrderQty;
      nullable21 = nullable12;
      Decimal num5 = 0M;
      if (nullable21.GetValueOrDefault() > num5 & nullable21.HasValue)
      {
        nullable2 = blanketSoLine1.Completed;
        if (!nullable2.GetValueOrDefault())
          goto label_31;
      }
      nullable21 = nullable12;
      Decimal num6 = 0M;
      if (nullable21.GetValueOrDefault() < num6 & nullable21.HasValue)
      {
        nullable2 = blanketSoLine1.Completed;
        if (!nullable2.GetValueOrDefault())
          goto label_32;
      }
      else
        goto label_32;
label_31:
      BlanketSOLine blanketSoLine5 = blanketSoLine1;
      nullable21 = blanketSoLine1.ClosedQty;
      nullable13 = blanketSoLine1.OrderQty;
      bool? nullable22 = new bool?(nullable21.GetValueOrDefault() >= nullable13.GetValueOrDefault() & nullable21.HasValue & nullable13.HasValue);
      blanketSoLine5.Completed = nullable22;
    }
label_32:
    BlanketSOOrder blanketSoOrder = PXParentAttribute.SelectParent<BlanketSOOrder>(((PXSelectBase) this.Base.Transactions).Cache, (object) line);
    blanketSoOrder.IsOpenTaxValid = new bool?(false);
    GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base).Update(blanketSoOrder);
    GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base).Update(blanketSoLine1);
  }

  protected virtual void OnSchedOrderDateUpdated(DateTime? oldDate, DateTime? newDate)
  {
    DateTime? nullable1 = oldDate;
    DateTime? nullable2 = newDate;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null || ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.Behavior != "BL" || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current), (PXEntryStatus) 3, (PXEntryStatus) 4))
      return;
    int num;
    if (newDate.HasValue)
    {
      if (oldDate.HasValue)
      {
        DateTime? nullable3 = newDate;
        DateTime? nullable4 = oldDate;
        num = nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0;
      }
      else
        num = 1;
    }
    else
      num = 0;
    if (num != 0)
    {
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.MinSchedOrderDate.HasValue)
      {
        DateTime? minSchedOrderDate = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.MinSchedOrderDate;
        DateTime? nullable5 = newDate;
        if ((minSchedOrderDate.HasValue & nullable5.HasValue ? (minSchedOrderDate.GetValueOrDefault() > nullable5.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.MinSchedOrderDate = newDate;
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).UpdateCurrent();
    }
    else
    {
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.MinSchedOrderDate.HasValue)
      {
        DateTime? minSchedOrderDate = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.MinSchedOrderDate;
        DateTime? nullable6 = oldDate;
        if ((minSchedOrderDate.HasValue & nullable6.HasValue ? (minSchedOrderDate.GetValueOrDefault() >= nullable6.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.MinSchedOrderDate = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>.SameAsCurrent>, And<BqlOperand<PX.Objects.SO.SOLineSplit.completed, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.qty, IBqlDecimal>.IsGreater<BqlOperand<PX.Objects.SO.SOLineSplit.qtyOnOrders, IBqlDecimal>.Add<PX.Objects.SO.SOLineSplit.receivedQty>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Min<PX.Objects.SO.SOLineSplit, DateTime?>((Func<PX.Objects.SO.SOLineSplit, DateTime?>) (s => s.SchedOrderDate));
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).UpdateCurrent();
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.ConfirmSingleLine(PX.Objects.SO.SOLine,PX.Objects.SO.SOShipLine,System.String,System.Boolean@)" />
  /// </summary>
  [PXOverride]
  public virtual void ConfirmSingleLine(
    PX.Objects.SO.SOLine line,
    SOShipLine shipline,
    string lineShippingRule,
    ref bool backorderExists,
    Blanket.ConfirmSingleLineDelegate base_ConfirmSingleLine)
  {
    base_ConfirmSingleLine(line, shipline, lineShippingRule, ref backorderExists);
    this.UpdateBlanketOrderShipmentCntr(line, 1);
  }

  [PXOverride]
  public virtual PX.Objects.SO.SOLine CorrectSingleLine(
    PX.Objects.SO.SOLine line,
    SOShipLine shipLine,
    bool lineSwitched,
    Dictionary<int?, (PX.Objects.SO.SOLine, Decimal?, Decimal?)> lineOpenQuantities,
    Blanket.CorrectSingleLineDelegate base_CorrectSingleLine)
  {
    PX.Objects.SO.SOLine soLine = base_CorrectSingleLine(line, shipLine, lineSwitched, lineOpenQuantities);
    this.UpdateBlanketOrderShipmentCntr(line, -1);
    return soLine;
  }

  private void UpdateBlanketOrderShipmentCntr(PX.Objects.SO.SOLine line, int diff)
  {
    if (string.IsNullOrEmpty(line.BlanketNbr))
      return;
    BlanketSOOrder blanketSoOrder1 = PXParentAttribute.SelectParent<BlanketSOOrder>(((PXSelectBase) this.Base.Transactions).Cache, (object) line);
    if (blanketSoOrder1 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base), new object[2]
      {
        (object) line.BlanketType,
        (object) line.BlanketNbr
      });
    if (blanketSoOrder1.ShipmentCntrUpdated.GetValueOrDefault())
      return;
    BlanketSOOrder blanketSoOrder2 = blanketSoOrder1;
    int? shipmentCntr = blanketSoOrder2.ShipmentCntr;
    int num = diff;
    blanketSoOrder2.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() + num) : new int?();
    blanketSoOrder1.ShipmentCntrUpdated = new bool?(true);
    GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base).Update(blanketSoOrder1);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.IsAddingPaymentsAllowed(PX.Objects.SO.SOOrder,PX.Objects.SO.SOOrderType)" />
  /// </summary>
  [PXOverride]
  public virtual bool IsAddingPaymentsAllowed(
    PX.Objects.SO.SOOrder order,
    PX.Objects.SO.SOOrderType orderType,
    Func<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrderType, bool> baseMethod)
  {
    if (order?.Behavior == "BL")
    {
      bool? nullable = order.IsExpired;
      if (!nullable.GetValueOrDefault())
      {
        nullable = order.Hold;
        if (!nullable.GetValueOrDefault())
        {
          int? childLineCntr = order.ChildLineCntr;
          int num = 0;
          if (childLineCntr.GetValueOrDefault() == num & childLineCntr.HasValue)
            goto label_5;
        }
      }
      return false;
    }
label_5:
    return baseMethod(order, orderType);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjdAmt> e)
  {
    if (object.Equals(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjdAmt>, SOAdjust, object>) e).NewValue, e.OldValue) || !(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.Behavior == "BL"))
      return;
    int? childLineCntr = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.ChildLineCntr;
    int num = 0;
    if (!(childLineCntr.GetValueOrDefault() == num & childLineCntr.HasValue))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjdAmt>, SOAdjust, object>) e).NewValue = e.OldValue;
      throw new PXSetPropertyException("The application amount cannot be changed because one or multiple child orders are linked to this blanket sales order.", (PXErrorLevel) 2);
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.VerifyAppliedToOrderAmount(PX.Objects.SO.SOOrder)" />
  /// </summary>
  [PXOverride]
  public virtual void VerifyAppliedToOrderAmount(PX.Objects.SO.SOOrder doc, Action<PX.Objects.SO.SOOrder> baseMethod)
  {
    if (doc?.Behavior == "BL")
    {
      int? childLineCntr = doc.ChildLineCntr;
      int num = 0;
      if (!(childLineCntr.GetValueOrDefault() == num & childLineCntr.HasValue))
        return;
    }
    baseMethod(doc);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<SOAdjust> e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 3 || e.TranStatus != null)
      return;
    PX.Objects.SO.SOOrder soOrder = !(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.OrderType == e.Row.AdjdOrderType) || !(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr == e.Row.AdjdOrderNbr) ? KeysRelation<CompositeKey<Field<SOAdjust.adjdOrderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<SOAdjust.adjdOrderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, SOAdjust>, PX.Objects.SO.SOOrder, SOAdjust>.FindParent((PXGraph) this.Base, e.Row, (PKFindOptions) 0) : ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    if (!(soOrder?.Behavior == "BL"))
      return;
    int? childLineCntr = soOrder.ChildLineCntr;
    int num = 0;
    if (!(childLineCntr.GetValueOrDefault() > num & childLineCntr.HasValue))
      return;
    this.ClearLinksToBlanketSOAdjust(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOAdjust> e)
  {
    if (!e.OldRow.BlanketRecordID.HasValue || e.Row.BlanketRecordID.HasValue)
      return;
    this.HandleSOAdjustBlanketReferenceRemoved(e.OldRow, e.Row);
  }

  protected virtual void HandleSOAdjustBlanketReferenceRemoved(
    SOAdjust oldSOAdjust,
    SOAdjust newSOAdjust)
  {
    object curyAdjdAmt = (object) newSOAdjust.CuryAdjdAmt;
    newSOAdjust.IsBalanceRecalculationRequired = new bool?(true);
    PXCache<SOAdjust> pxCache = GraphHelper.Caches<SOAdjust>((PXGraph) this.Base);
    try
    {
      ((PXCache) pxCache).RaiseFieldVerifying<SOAdjust.curyAdjdAmt>((object) newSOAdjust, ref curyAdjdAmt);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXCache) pxCache).RaiseExceptionHandling<SOAdjust.curyAdjdAmt>((object) newSOAdjust, curyAdjdAmt, (Exception) ex);
    }
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.UpdateBalanceOnCuryAdjdAmtUpdated(PX.Data.PXCache,PX.Data.PXFieldUpdatedEventArgs)" />
  [PXOverride]
  public void UpdateBalanceOnCuryAdjdAmtUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e,
    Action<PXCache, PXFieldUpdatedEventArgs> base_UpdateBalanceOnCuryAdjdAmtUpdated)
  {
    SOAdjust row = (SOAdjust) e.Row;
    if (row.IsBalanceRecalculationRequired.GetValueOrDefault())
    {
      this.TryRecalculateBalanceAndCuryInfo(row, sender);
      row.IsBalanceRecalculationRequired = new bool?(false);
    }
    else
      base_UpdateBalanceOnCuryAdjdAmtUpdated(sender, e);
  }

  [PXOverride]
  public virtual void PerformPersist(
    PXGraph.IPersistPerformer persister,
    Action<PXGraph.IPersistPerformer> baseMethod)
  {
    try
    {
      baseMethod(persister);
    }
    catch (PXSiteStatusByCostCenterPersistInsertedException ex)
    {
      foreach (PX.Objects.SO.SOLineSplit soLineSplit in NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Base.splits).Cache.Deleted, ((PXSelectBase) this.Base.splits).Cache.Updated).OfType<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
      {
        int? inventoryId1 = s.InventoryID;
        int? inventoryId2 = ex.InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        {
          int? subItemId1 = s.SubItemID;
          int? subItemId2 = ex.SubItemID;
          if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
          {
            int? siteId1 = s.SiteID;
            int? siteId2 = ex.SiteID;
            if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
            {
              int? costCenterId1 = s.CostCenterID;
              int? costCenterId2 = ex.CostCenterID;
              if (costCenterId1.GetValueOrDefault() == costCenterId2.GetValueOrDefault() & costCenterId1.HasValue == costCenterId2.HasValue)
              {
                bool? isAllocated = s.IsAllocated;
                bool flag = false;
                return isAllocated.GetValueOrDefault() == flag & isAllocated.HasValue;
              }
            }
          }
        }
        return false;
      })))
      {
        PX.Objects.SO.SOLineSplit split = soLineSplit;
        PX.Objects.SO.SOLine soLine = NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Base.Transactions).Cache.Deleted, ((PXSelectBase) this.Base.Transactions).Cache.Updated).OfType<PX.Objects.SO.SOLine>().SingleOrDefault<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
        {
          if (!(l.OrderType == split.OrderType) || !(l.OrderNbr == split.OrderNbr))
            return false;
          int? lineNbr1 = l.LineNbr;
          int? lineNbr2 = split.LineNbr;
          return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
        }));
        BlanketSOLineSplit blanketSoLineSplit = PXParentAttribute.SelectParent<BlanketSOLineSplit>(((PXSelectBase) this.Base.Transactions).Cache, (object) soLine);
        if (blanketSoLineSplit != null && blanketSoLineSplit.IsAllocated.GetValueOrDefault())
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, soLine.InventoryID);
          throw new PXException("The {0} child order cannot be deleted or canceled because the {1} item is allocated in the related {2} blanket sales order and the available quantity of the item is negative. To delete or cancel the child order, complete the related line with the item in the blanket sales order.", new object[3]
          {
            (object) soLine.OrderNbr,
            (object) inventoryItem.InventoryCD,
            (object) blanketSoLineSplit.OrderNbr
          });
        }
      }
      throw;
    }
  }

  protected virtual void ClearLinksToBlanketSOAdjust(SOAdjust adjustment)
  {
    PXDatabase.Update<SOAdjust>(new PXDataFieldParam[8]
    {
      (PXDataFieldParam) new PXDataFieldAssign<SOAdjust.blanketRecordID>((object) null),
      (PXDataFieldParam) new PXDataFieldAssign<SOAdjust.blanketNbr>((object) null),
      (PXDataFieldParam) new PXDataFieldAssign<SOAdjust.blanketType>((object) null),
      (PXDataFieldParam) new PXDataFieldRestrict<SOAdjust.blanketRecordID>((object) adjustment.RecordID),
      (PXDataFieldParam) new PXDataFieldRestrict<SOAdjust.blanketType>((object) adjustment.AdjdOrderType),
      (PXDataFieldParam) new PXDataFieldRestrict<SOAdjust.blanketNbr>((object) adjustment.AdjdOrderNbr),
      (PXDataFieldParam) new PXDataFieldRestrict<SOAdjust.adjgDocType>((object) adjustment.AdjgDocType),
      (PXDataFieldParam) new PXDataFieldRestrict<SOAdjust.adjgRefNbr>((object) adjustment.AdjgRefNbr)
    });
  }

  private BlanketSOLineSplit SelectParentSplit(PX.Objects.SO.SOLine row)
  {
    return PXParentAttribute.SelectParent<BlanketSOLineSplit>(((PXSelectBase) this.Base.Transactions).Cache, (object) row) ?? throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base), new object[4]
    {
      (object) row.BlanketType,
      (object) row.BlanketNbr,
      (object) row.BlanketLineNbr,
      (object) row.BlanketSplitLineNbr
    });
  }

  private PX.Objects.SO.SOLineSplit GetOrigChildSplit(PX.Objects.SO.SOLine line, IEnumerable<PX.Objects.SO.SOLineSplit> splits)
  {
    return splits.Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
    {
      if (!s.POCreate.GetValueOrDefault())
        return false;
      Decimal? receivedQty = s.ReceivedQty;
      Decimal num = 0M;
      return receivedQty.GetValueOrDefault() > num & receivedQty.HasValue;
    })).SingleOrDefault<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
    {
      BlanketSOLineSplit blanketSoLineSplit = this.SelectParentSplit(line);
      if (!(blanketSoLineSplit.POType == s.POType) || !(blanketSoLineSplit.PONbr == s.PONbr))
        return false;
      int? poLineNbr1 = blanketSoLineSplit.POLineNbr;
      int? poLineNbr2 = s.POLineNbr;
      return poLineNbr1.GetValueOrDefault() == poLineNbr2.GetValueOrDefault() & poLineNbr1.HasValue == poLineNbr2.HasValue;
    }));
  }

  private SOOrderEntry GetGraphForReturnReceivedAllocationsToBlanket()
  {
    if (this._graphForReturnReceivedAllocationsToBlanket == null)
    {
      this._graphForReturnReceivedAllocationsToBlanket = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXGraph) this.Base).OnBeforeCommit += new Action<PXGraph>(this.PersistReturnReceivedAllocationsToBlanket);
      ((PXGraph) this.Base).OnAfterPersist += new Action<PXGraph>(this.ClearAffectedCaches);
    }
    return this._graphForReturnReceivedAllocationsToBlanket;
  }

  private void ClearReturnReceivedAllocationsToBlanket()
  {
    if (this._graphForReturnReceivedAllocationsToBlanket == null)
      return;
    ((PXGraph) this.Base).OnAfterPersist -= new Action<PXGraph>(this.ClearAffectedCaches);
    ((PXGraph) this.Base).OnBeforeCommit -= new Action<PXGraph>(this.PersistReturnReceivedAllocationsToBlanket);
    this._graphForReturnReceivedAllocationsToBlanket = (SOOrderEntry) null;
  }

  private void PersistReturnReceivedAllocationsToBlanket(PXGraph graph)
  {
    ((PXAction) this._graphForReturnReceivedAllocationsToBlanket.Save).Press();
    ((PXGraph) this.Base).SelectTimeStamp();
  }

  private void ClearAffectedCaches(PXGraph graph)
  {
    ((PXSelectBase) this.BlanketSplits).Cache.Clear();
    ((PXSelectBase) this.Base.splits).Cache.Clear();
    ((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) this.Base)).Clear();
    ((PXCache) GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base)).Clear();
    ((PXCache) GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base)).Clear();
    ((PXCache) GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base)).Clear();
    ((PXGraph) this.Base).Clear((PXClearOption) 4);
    this.ClearReturnReceivedAllocationsToBlanket();
  }

  protected virtual void ReturnReceivedAllocationsToBlanket(PX.Objects.SO.SOLine row)
  {
    List<PX.Objects.SO.SOLineSplit> list = ((PXSelectBase) this.Base.splits).Cache.Deleted.Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
    {
      int? lineNbr1 = s.LineNbr;
      int? lineNbr2 = row.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue && s.OrderNbr == row.OrderNbr && s.OrderType == row.OrderType;
    })).ToList<PX.Objects.SO.SOLineSplit>();
    PX.Objects.SO.SOLineSplit origChildSplit = this.GetOrigChildSplit(row, (IEnumerable<PX.Objects.SO.SOLineSplit>) list);
    if (origChildSplit == null)
      return;
    SOOrderEntry allocationsToBlanket = this.GetGraphForReturnReceivedAllocationsToBlanket();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) allocationsToBlanket.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) allocationsToBlanket.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) row.BlanketNbr, new object[1]
    {
      (object) row.BlanketType
    }));
    ((PXSelectBase<PX.Objects.SO.SOLine>) allocationsToBlanket.Transactions).Current = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) allocationsToBlanket.Transactions).Search<PX.Objects.SO.SOLine.lineNbr>((object) row.BlanketLineNbr, Array.Empty<object>()));
    using (allocationsToBlanket.LineSplittingExt.SuppressedModeScope(true))
    {
      PX.Objects.SO.SOLineSplit soLineSplit1 = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLineSplit>) allocationsToBlanket.splits).Search<PX.Objects.SO.SOLineSplit.splitLineNbr>((object) row.BlanketSplitLineNbr, Array.Empty<object>()));
      if (soLineSplit1 == null)
        throw new RowNotFoundException(((PXSelectBase) allocationsToBlanket.splits).Cache, new object[4]
        {
          (object) row.BlanketType,
          (object) row.BlanketNbr,
          (object) row.BlanketLineNbr,
          (object) row.BlanketSplitLineNbr
        });
      Decimal? nullable1 = new Decimal?(0M);
      foreach (PX.Objects.SO.SOLineSplit soLineSplit2 in list.Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
      {
        int? parentSplitLineNbr = s.ParentSplitLineNbr;
        int? splitLineNbr = origChildSplit.SplitLineNbr;
        return parentSplitLineNbr.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & parentSplitLineNbr.HasValue == splitLineNbr.HasValue;
      })))
      {
        PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit2);
        copy.OrderType = (string) null;
        copy.OrderNbr = (string) null;
        copy.LineNbr = new int?();
        copy.SplitLineNbr = new int?();
        copy.Behavior = (string) null;
        copy.InvtMult = new short?();
        copy.ParentSplitLineNbr = soLineSplit1.SplitLineNbr;
        copy.PlanID = new long?();
        copy.OrderDate = new DateTime?();
        copy.SchedOrderDate = soLineSplit1.SchedOrderDate;
        copy.SchedShipDate = soLineSplit1.SchedShipDate;
        copy.POCreateDate = soLineSplit1.POCreateDate;
        copy.CustomerOrderNbr = soLineSplit1.CustomerOrderNbr;
        Decimal? nullable2 = nullable1;
        Decimal? baseQty = copy.BaseQty;
        nullable1 = nullable2.HasValue & baseQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
        ((PXSelectBase<PX.Objects.SO.SOLineSplit>) allocationsToBlanket.splits).Insert(copy);
      }
      ((PXSelectBase<PX.Objects.SO.SOLineSplit>) allocationsToBlanket.splits).Current = soLineSplit1;
      PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLineSplit>) allocationsToBlanket.splits).Current);
      PX.Objects.SO.SOLineSplit soLineSplit3 = copy1;
      Decimal? baseReceivedQty = soLineSplit3.BaseReceivedQty;
      Decimal? nullable3 = nullable1;
      soLineSplit3.BaseReceivedQty = baseReceivedQty.HasValue & nullable3.HasValue ? new Decimal?(baseReceivedQty.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      PX.Objects.SO.SOLineSplit soLineSplit4 = copy1;
      PXCache cache = ((PXSelectBase) allocationsToBlanket.splits).Cache;
      int? inventoryId = copy1.InventoryID;
      string uom = copy1.UOM;
      nullable3 = copy1.BaseReceivedQty;
      Decimal num = nullable3.Value;
      Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num, INPrecision.QUANTITY));
      soLineSplit4.ReceivedQty = nullable4;
      PX.Objects.SO.SOLineSplit soLineSplit5 = copy1;
      Decimal? receivedQty = copy1.ReceivedQty;
      Decimal? shippedQty = copy1.ShippedQty;
      nullable3 = receivedQty.HasValue & shippedQty.HasValue ? new Decimal?(receivedQty.GetValueOrDefault() + shippedQty.GetValueOrDefault()) : new Decimal?();
      Decimal? qty = copy1.Qty;
      bool? nullable5 = new bool?(nullable3.GetValueOrDefault() >= qty.GetValueOrDefault() & nullable3.HasValue & qty.HasValue);
      soLineSplit5.Completed = nullable5;
      ((PXSelectBase<PX.Objects.SO.SOLineSplit>) allocationsToBlanket.splits).Update(copy1);
    }
  }

  private bool IsExpectingReturnAllocationsToBlanket(BlanketSOLineSplit split)
  {
    if (GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base).GetStatus(split) == 1)
    {
      foreach (PX.Objects.SO.SOLine soLine in GraphHelper.RowCast<PX.Objects.SO.SOLine>(((PXSelectBase) this.Base.Transactions).Cache.Deleted).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
      {
        if (l.BlanketType == split.OrderType && l.BlanketNbr == split.OrderNbr)
        {
          int? blanketLineNbr = l.BlanketLineNbr;
          int? lineNbr = split.LineNbr;
          if (blanketLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & blanketLineNbr.HasValue == lineNbr.HasValue)
          {
            int? blanketSplitLineNbr = l.BlanketSplitLineNbr;
            int? splitLineNbr = split.SplitLineNbr;
            return blanketSplitLineNbr.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & blanketSplitLineNbr.HasValue == splitLineNbr.HasValue;
          }
        }
        return false;
      })))
      {
        PX.Objects.SO.SOLine deletedLine = soLine;
        List<PX.Objects.SO.SOLineSplit> list = ((PXSelectBase) this.Base.splits).Cache.Deleted.Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
        {
          int? lineNbr1 = s.LineNbr;
          int? lineNbr2 = deletedLine.LineNbr;
          return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue && s.OrderNbr == deletedLine.OrderNbr && s.OrderType == deletedLine.OrderType;
        })).ToList<PX.Objects.SO.SOLineSplit>();
        if (this.GetOrigChildSplit(deletedLine, (IEnumerable<PX.Objects.SO.SOLineSplit>) list) != null)
          return true;
      }
    }
    return false;
  }

  /// Uses <see cref="M:PX.Objects.SO.SOOrderEntry.FillSOAdjustByPayment(PX.Objects.SO.SOAdjust)" />
  [PXProtectedAccess(null)]
  protected abstract void FillSOAdjustByPayment(SOAdjust adj);

  /// Uses <see cref="M:PX.Objects.SO.SOOrderEntry.TryRecalculateBalanceAndCuryInfo(PX.Objects.SO.SOAdjust,PX.Data.PXCache)" />
  [PXProtectedAccess(null)]
  protected abstract bool TryRecalculateBalanceAndCuryInfo(SOAdjust adj, PXCache sender);

  public class CreateChildrenResult
  {
    public List<PX.Objects.SO.SOOrder> Created { get; set; } = new List<PX.Objects.SO.SOOrder>();

    public Exception LastError { get; set; }

    public int ErrorCount { get; set; }
  }

  public class CreateChildParameter
  {
    public PX.Objects.SO.SOOrder BlanketOrder { get; set; }

    public PX.Objects.SO.SOOrderType BlanketOrderType { get; set; }

    public PX.Objects.CM.CurrencyInfo BlanketCurrency { get; set; }

    public DateTime? SchedOrderDate { get; set; }

    public string CustomerOrderNbr { get; set; }

    public int? CustomerLocationID { get; set; }

    public string TaxZoneID { get; set; }

    public string ShipVia { get; set; }

    public string FOBPoint { get; set; }

    public string ShipTermsID { get; set; }

    public string ShipZoneID { get; set; }

    public IEnumerable<PXResult<BlanketSOLineSplit, BlanketSOLine>> Lines { get; set; }
  }

  protected class BlanketAdjustComparer : IComparer<SOAdjust>
  {
    private Dictionary<(string, string), (int?, int?)> orderDictionary;

    public BlanketAdjustComparer(
      Dictionary<(string, string), (int?, int?)> orderDictionary)
    {
      this.orderDictionary = orderDictionary;
    }

    public int Compare(SOAdjust x, SOAdjust y)
    {
      (int?, int?) tuple1;
      bool flag1 = this.orderDictionary.TryGetValue((x.AdjdOrderNbr, x.AdjdOrderType), out tuple1);
      (int?, int?) tuple2;
      bool flag2 = this.orderDictionary.TryGetValue((y.AdjdOrderNbr, y.AdjdOrderType), out tuple2);
      if (flag1 & flag2)
      {
        int? nullable1 = tuple1.Item1;
        int? nullable2 = tuple2.Item1;
        if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          return -1;
        nullable2 = tuple1.Item1;
        nullable1 = tuple2.Item1;
        if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
          return 1;
        nullable1 = tuple1.Item2;
        nullable2 = tuple2.Item2;
        if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          return -1;
        nullable2 = tuple1.Item2;
        nullable1 = tuple2.Item2;
        return nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue ? 1 : 0;
      }
      if (!flag1 && !flag2)
        return 0;
      return flag1 ? -1 : 1;
    }
  }

  public delegate void ConfirmSingleLineDelegate(
    PX.Objects.SO.SOLine line,
    SOShipLine shipline,
    string lineShippingRule,
    ref bool backorderExists);

  public delegate PX.Objects.SO.SOLine CorrectSingleLineDelegate(
    PX.Objects.SO.SOLine line,
    SOShipLine shipLine,
    bool lineSwitched,
    Dictionary<int?, (PX.Objects.SO.SOLine, Decimal?, Decimal?)> lineOpenQuantities);

  public class ChildOrderCreationFromBlanketScope : 
    FlaggedModeScopeBase<Blanket.ChildOrderCreationFromBlanketScope>
  {
  }
}
