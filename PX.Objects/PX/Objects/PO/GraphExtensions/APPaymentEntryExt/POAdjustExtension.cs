// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APPaymentEntryExt.POAdjustExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APPaymentEntryExt;

public class POAdjustExtension : PXGraphExtension<APPaymentEntry.MultiCurrency, APPaymentEntry>
{
  [PXViewName("Orders to Apply")]
  public PXSelectJoin<POAdjust, LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<POAdjust.adjdOrderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<POAdjust.adjdOrderNbr>>>>, Where<POAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>>>> POAdjustments;
  public PXSelect<POOrderPrepayment, Where<POOrderPrepayment.orderType, Equal<Required<POOrderPrepayment.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Required<POOrderPrepayment.orderNbr>>, And<POOrderPrepayment.aPDocType, Equal<Current<APPayment.docType>>, And<POOrderPrepayment.aPRefNbr, Equal<Current<APPayment.refNbr>>>>>>> poOrderPrepayment;
  public PXFilter<LoadOrdersFilter> LoadOrders;
  public PXAction<APPayment> loadPOOrders;

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AP.APPaymentEntry.MultiCurrency.GetChildren" />.
  /// </summary>
  [PXOverride]
  public virtual PXSelectBase[] GetChildren(Func<PXSelectBase[]> baseMethod)
  {
    return ((IEnumerable<PXSelectBase>) baseMethod()).Union<PXSelectBase>((IEnumerable<PXSelectBase>) new PXSelectBase[1]
    {
      (PXSelectBase) this.POAdjustments
    }).ToArray<PXSelectBase>();
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    new \u003C\u003Ef__AnonymousType77<PXCache, string>[3]
    {
      new
      {
        cache = ((PXSelectBase) this.POAdjustments).Cache,
        fieldName = "adjdOrderNbr"
      },
      new
      {
        cache = ((PXSelectBase) this.LoadOrders).Cache,
        fieldName = "startOrderNbr"
      },
      new
      {
        cache = ((PXSelectBase) this.LoadOrders).Cache,
        fieldName = "endOrderNbr"
      }
    }.ToList().ForEach(s =>
    {
      PXSelectorAttribute selectorAttribute = s.cache.GetAttributes(s.fieldName).OfType<PXSelectorAttribute>().First<PXSelectorAttribute>();
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
        selectorAttribute.WhereAnd(s.cache, typeof (Where<PX.Objects.PO.POOrder.payToVendorID, Equal<Current<APPayment.vendorID>>>));
      else
        selectorAttribute.WhereAnd(s.cache, typeof (Where<PX.Objects.PO.POOrder.vendorID, Equal<Current<APPayment.vendorID>>>));
    });
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void POAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  public virtual IEnumerable LoadPOOrders(PXAdapter adapter)
  {
    // ISSUE: method pointer
    if (((PXSelectBase<LoadOrdersFilter>) this.LoadOrders).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CLoadPOOrders\u003Eb__8_0)), true) == 6)
      this.LoadPOOrdersByFilter();
    return adapter.Get();
  }

  protected virtual void LoadPOOrdersByFilter()
  {
    PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<PX.Objects.PO.POOrder.orderType, In3<POOrderType.regularOrder, POOrderType.dropShip>>>> pxSelect = new PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<PX.Objects.PO.POOrder.orderType, In3<POOrderType.regularOrder, POOrderType.dropShip>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base);
    LoadOrdersFilter current = ((PXSelectBase<LoadOrdersFilter>) this.LoadOrders).Current;
    if (current.BranchID.HasValue)
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.branchID, Equal<Current<LoadOrdersFilter.branchID>>>>();
    DateTime? nullable1 = current.FromDate;
    if (nullable1.HasValue)
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.orderDate, GreaterEqual<Current<LoadOrdersFilter.fromDate>>>>();
    nullable1 = current.ToDate;
    if (nullable1.HasValue)
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.orderDate, LessEqual<Current<LoadOrdersFilter.toDate>>>>();
    if (current.StartOrderNbr != null)
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.orderNbr, GreaterEqual<Current<LoadOrdersFilter.startOrderNbr>>>>();
    if (current.EndOrderNbr != null)
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.orderNbr, LessEqual<Current<LoadOrdersFilter.endOrderNbr>>>>();
    if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.payToVendorID, Equal<Current<APPayment.vendorID>>>>();
    else
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.vendorID, Equal<Current<APPayment.vendorID>>>>();
    ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).WhereAnd<Where<PX.Objects.PO.POOrder.curyUnprepaidTotal, Greater<decimal0>>>();
    if (current.OrderBy.GetValueOrDefault() == 1)
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).OrderByNew<OrderBy<Asc<PX.Objects.PO.POOrder.orderDate, Asc<PX.Objects.PO.POOrder.orderNbr>>>>();
    else
      ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).OrderByNew<OrderBy<Asc<PX.Objects.PO.POOrder.orderNbr>>>();
    foreach (PXResult<PX.Objects.PO.POOrder> pxResult in ((PXSelectBase<PX.Objects.PO.POOrder>) pxSelect).SelectWindowed(0, current.MaxNumberOfDocuments.GetValueOrDefault(), new object[1]
    {
      (object) current
    }))
    {
      PX.Objects.PO.POOrder poOrder = PXResult<PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      POAdjust poAdjust1 = new POAdjust()
      {
        AdjgDocType = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.DocType,
        AdjgRefNbr = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.RefNbr,
        AdjdOrderType = poOrder.OrderType,
        AdjdOrderNbr = poOrder.OrderNbr,
        AdjdDocType = "---",
        AdjdRefNbr = string.Empty,
        AdjNbr = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.AdjCntr
      };
      POAdjust poAdjust2 = PXResultset<POAdjust>.op_Implicit(((PXSelectBase<POAdjust>) new PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Current<POAdjust.adjgDocType>>, And<POAdjust.adjgRefNbr, Equal<Current<POAdjust.adjgRefNbr>>, And<POAdjust.adjdOrderType, Equal<Required<POAdjust.adjdOrderType>>, And<POAdjust.adjdOrderNbr, Equal<Required<POAdjust.adjdOrderNbr>>, And<POAdjust.released, Equal<False>, And<POAdjust.isRequest, Equal<False>>>>>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base)).Select(new object[2]
      {
        (object) poAdjust1.AdjdOrderType,
        (object) poAdjust1.AdjdOrderNbr
      }));
      if (poAdjust2 == null)
      {
        ((PXSelectBase<POAdjust>) this.POAdjustments).Insert(poAdjust1);
      }
      else
      {
        object obj;
        ((PXSelectBase) this.POAdjustments).Cache.RaiseFieldDefaulting<POAdjust.curyAdjgAmt>((object) poAdjust2, ref obj);
        Decimal? nullable2 = (Decimal?) obj;
        Decimal? curyAdjgAmt = poAdjust2.CuryAdjgAmt;
        if (nullable2.GetValueOrDefault() < curyAdjgAmt.GetValueOrDefault() & nullable2.HasValue & curyAdjgAmt.HasValue)
        {
          object copy = ((PXSelectBase) this.POAdjustments).Cache.CreateCopy((object) poAdjust2);
          ((PXSelectBase) this.POAdjustments).Cache.SetValueExt<POAdjust.curyAdjgAmt>(copy, obj);
          ((PXSelectBase) this.POAdjustments).Cache.Update(copy);
        }
      }
    }
  }

  [PXOverride]
  public virtual IEnumerable<AdjustmentStubGroup> GetAdjustmentsPrintList(
    Func<IEnumerable<AdjustmentStubGroup>> baseMethod)
  {
    if (((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.DocType != "PPM")
      return baseMethod();
    List<AdjustmentStubGroup> adjustmentsPrintList = this.AddPOAdjustments(baseMethod());
    AdjustmentStubGroup outstandingBalanceRow = this.GetOutstandingBalanceRow();
    if (outstandingBalanceRow != null)
      adjustmentsPrintList.Add(outstandingBalanceRow);
    return (IEnumerable<AdjustmentStubGroup>) adjustmentsPrintList;
  }

  [PXOverride]
  public virtual void SetDocTypeList(object row, Action<object> baseMethod)
  {
    baseMethod(row);
    if (!(row is APPayment payment) || !this.IsPrepaymentCheck(payment))
      return;
    this.AddPrepaymentToDocTypeList();
  }

  [PXOverride]
  public virtual void DeleteUnreleasedApplications(Action baseMethod)
  {
    baseMethod();
    if (((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current?.DocType != "PPM")
      return;
    EnumerableExtensions.ForEach<POAdjust>((IEnumerable<POAdjust>) ((PXSelectBase<POAdjust>) new PXSelectJoin<POAdjust, LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<POAdjust.adjdOrderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<POAdjust.adjdOrderNbr>>>>, Where<POAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<POAdjust.released, Equal<False>, And<POAdjust.isRequest, Equal<False>>>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base)).SelectMain(Array.Empty<object>()), (Action<POAdjust>) (poadjust => ((PXSelectBase<POAdjust>) this.POAdjustments).Delete(poadjust)));
  }

  [PXOverride]
  public virtual void VoidCheckProc(APPayment doc, Action<APPayment> baseMethod)
  {
    baseMethod(doc);
    ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.CuryPOApplAmt = new Decimal?(0M);
    ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Update(((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current);
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXFormula(typeof (Sub<APPayment.curyDocBal, Add<APPayment.curyApplAmt, Switch<Case<Where<APPayment.docType, Equal<APDocType.prepayment>>, APPayment.curyPOUnreleasedApplAmt>, decimal0>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APPayment.curyUnappliedBal> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<APPayment> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    bool? nullable;
    int num;
    if (eventArgs.Row.DocType == "PPM" && EnumerableExtensions.IsIn<string>(eventArgs.Row.Status, "H", "Z"))
    {
      nullable = eventArgs.Row.IsMigratedRecord;
      if (!nullable.GetValueOrDefault())
      {
        nullable = eventArgs.Row.PaymentsByLinesAllowed;
        if (!nullable.GetValueOrDefault())
        {
          num = this.IsPrepaymentCheck(((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current) ? 1 : 0;
          goto label_7;
        }
      }
    }
    num = 0;
label_7:
    bool flag = num != 0;
    ((PXSelectBase) this.POAdjustments).Cache.AllowDelete = flag;
    ((PXSelectBase) this.POAdjustments).Cache.AllowInsert = flag;
    ((PXSelectBase) this.POAdjustments).Cache.AllowUpdate = flag;
    ((PXAction) this.loadPOOrders).SetEnabled(flag);
    ((PXSelectBase) this.POAdjustments).Cache.AllowSelect = EnumerableExtensions.IsIn<string>(eventArgs.Row.DocType, "PPM", "VCK");
    nullable = eventArgs.Row.Hold;
    if (!nullable.GetValueOrDefault())
    {
      Decimal? curyPoFullApplAmt = eventArgs.Row.CuryPOFullApplAmt;
      Decimal? curyOrigDocAmt = eventArgs.Row.CuryOrigDocAmt;
      if (curyPoFullApplAmt.GetValueOrDefault() > curyOrigDocAmt.GetValueOrDefault() & curyPoFullApplAmt.HasValue & curyOrigDocAmt.HasValue && eventArgs.Row.DocType == "PPM")
      {
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APPayment>>) eventArgs).Cache.RaiseExceptionHandling<APPayment.curyPOApplAmt>((object) eventArgs.Row, (object) eventArgs.Row.CuryPOFullApplAmt, (Exception) new PXSetPropertyException<APPayment.curyPOApplAmt>("The value of the PO Applied Amount box cannot be greater than the value of the Payment Amount box.", (PXErrorLevel) 5));
        goto label_11;
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APPayment>>) eventArgs).Cache.RaiseExceptionHandling<APPayment.curyPOApplAmt>((object) eventArgs.Row, (object) eventArgs.Row.CuryPOFullApplAmt, (Exception) null);
label_11:
    PXUIFieldAttribute.SetVisible<APPayment.curyPOApplAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APPayment>>) eventArgs).Cache, (object) eventArgs.Row, eventArgs.Row.DocType == "PPM");
    if (!(eventArgs.Row.DocType == "PPM") || !eventArgs.Row.VendorID.HasValue || ((PXSelectBase<POAdjust>) this.POAdjustments).SelectSingle(Array.Empty<object>()) == null)
      return;
    PXUIFieldAttribute.SetEnabled<APPayment.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APPayment>>) eventArgs).Cache, (object) eventArgs.Row, false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<APAdjust, APAdjust.curyAdjgAmt> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    this.InsertUpdatePOAdjust(eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<APAdjust> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    this.InsertUpdatePOAdjust(eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APAdjust> eventArgs)
  {
    if (((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current?.DocType != "PPM" || eventArgs.Row?.AdjdDocType != "PPM")
      return;
    PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<POAdjust.adjNbr, Equal<Current<APPayment.adjCntr>>, And<POAdjust.adjdDocType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<POAdjust.adjdRefNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>>>> pxSelect = new PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<POAdjust.adjNbr, Equal<Current<APPayment.adjCntr>>, And<POAdjust.adjdDocType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<POAdjust.adjdRefNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base);
    object[] objArray = new object[2]
    {
      (object) eventArgs.Row.AdjdDocType,
      (object) eventArgs.Row.AdjdRefNbr
    };
    foreach (POAdjust poAdjust in ((PXSelectBase<POAdjust>) pxSelect).SelectMain(objArray))
    {
      poAdjust.ForceDelete = new bool?(true);
      ((PXSelectBase<POAdjust>) this.POAdjustments).Delete(poAdjust);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POAdjust, POAdjust.curyAdjgAmt> eventArgs)
  {
    if (eventArgs.Row == null || eventArgs.Row.IsRequest.GetValueOrDefault())
      return;
    PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base, eventArgs.Row.AdjdOrderType, eventArgs.Row.AdjdOrderNbr);
    if (poOrder == null || poOrder.Cancelled.GetValueOrDefault())
      return;
    Decimal valueOrDefault = ((Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POAdjust, POAdjust.curyAdjgAmt>, POAdjust, object>) eventArgs).NewValue).GetValueOrDefault();
    Decimal? curyAdjgAmt = eventArgs.Row.CuryAdjgAmt;
    Decimal num1 = valueOrDefault;
    if (curyAdjgAmt.GetValueOrDefault() >= num1 & curyAdjgAmt.HasValue || !eventArgs.Row.CuryAdjgAmt.HasValue && valueOrDefault <= 0M)
      return;
    bool flag;
    if (poOrder.CuryID == ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.CuryID)
    {
      Decimal? curyPrepaidTotal = poOrder.CuryPrepaidTotal;
      Decimal num2 = valueOrDefault;
      Decimal? nullable = curyPrepaidTotal.HasValue ? new Decimal?(curyPrepaidTotal.GetValueOrDefault() + num2) : new Decimal?();
      Decimal? unbilledOrderTotal = poOrder.CuryUnbilledOrderTotal;
      flag = nullable.GetValueOrDefault() > unbilledOrderTotal.GetValueOrDefault() & nullable.HasValue & unbilledOrderTotal.HasValue;
    }
    else
    {
      Decimal num3 = ((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().CuryConvBase(valueOrDefault);
      Decimal? prepaidTotal = poOrder.PrepaidTotal;
      Decimal num4 = num3;
      Decimal? nullable = prepaidTotal.HasValue ? new Decimal?(prepaidTotal.GetValueOrDefault() + num4) : new Decimal?();
      Decimal? unbilledOrderTotal = poOrder.UnbilledOrderTotal;
      flag = nullable.GetValueOrDefault() > unbilledOrderTotal.GetValueOrDefault() & nullable.HasValue & unbilledOrderTotal.HasValue;
    }
    if (flag)
      throw new PXSetPropertyException("The value of the Unbilled Prepayment Total box of the {0} purchase order cannot exceed its unbilled amount.", new object[1]
      {
        (object) poOrder.OrderNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<POAdjust, POAdjust.curyAdjgAmt> eventArgs)
  {
    this.SetPOAdjustAdjdAmt(eventArgs.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<POAdjust> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PXCache cache = ((PXSelectBase) this.POAdjustments).Cache;
    POAdjust row = eventArgs.Row;
    bool? nullable = eventArgs.Row.IsRequest;
    int num;
    if (!nullable.GetValueOrDefault())
    {
      nullable = eventArgs.Row.Released;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<POAdjust.curyAdjgAmt>(cache, (object) row, num != 0);
    PXUIFieldAttribute.SetEnabled<POAdjust.adjdOrderType>(((PXSelectBase) this.POAdjustments).Cache, (object) eventArgs.Row, eventArgs.Row.AdjdOrderNbr == null);
    PXUIFieldAttribute.SetEnabled<POAdjust.adjdOrderNbr>(((PXSelectBase) this.POAdjustments).Cache, (object) eventArgs.Row, eventArgs.Row.AdjdOrderNbr == null);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<POAdjust, POAdjust.curyAdjgAmt> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base, eventArgs.Row.AdjdOrderType, eventArgs.Row.AdjdOrderNbr);
    if (poOrder == null)
      return;
    if (poOrder.CuryID == ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.CuryID)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POAdjust, POAdjust.curyAdjgAmt>, POAdjust, object>) eventArgs).NewValue = (object) poOrder.CuryUnprepaidTotal;
    }
    else
    {
      Decimal num = ((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().CuryConvCury(poOrder.UnprepaidTotal.GetValueOrDefault());
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POAdjust, POAdjust.curyAdjgAmt>, POAdjust, object>) eventArgs).NewValue = (object) num;
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<POAdjust> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    bool? nullable = eventArgs.Row.Released;
    if (nullable.GetValueOrDefault())
    {
      nullable = eventArgs.Row.ForceDelete;
      if (!nullable.GetValueOrDefault())
        throw new PXException("You cannot remove the reference to the {0} purchase order because it is released.", new object[1]
        {
          (object) eventArgs.Row.AdjdOrderNbr
        });
    }
    nullable = eventArgs.Row.IsRequest;
    if (!nullable.GetValueOrDefault() || !(eventArgs.Row.AdjgDocType != "VCK"))
      return;
    nullable = eventArgs.Row.ForceDelete;
    if (!nullable.GetValueOrDefault())
      throw new PXException("You cannot remove the reference to the {0} purchase order because it is related to the {1} prepayment request.", new object[2]
      {
        (object) eventArgs.Row.AdjdOrderNbr,
        (object) eventArgs.Row.AdjdRefNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowDeleted<POAdjust> eventArgs)
  {
    if (eventArgs.Row == null || eventArgs.Row.AdjdOrderType == null || eventArgs.Row.AdjdOrderNbr == null)
      return;
    if (PXResultset<POAdjust>.op_Implicit(((PXSelectBase<POAdjust>) new PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<POAdjust.adjdOrderType, Equal<Required<POAdjust.adjdOrderType>>, And<POAdjust.adjdOrderNbr, Equal<Required<POAdjust.adjdOrderNbr>>, And<POAdjust.isRequest, NotEqual<True>>>>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base)).Select(new object[2]
    {
      (object) eventArgs.Row.AdjdOrderType,
      (object) eventArgs.Row.AdjdOrderNbr
    })) != null)
      return;
    POOrderPrepayment poOrderPrepayment = ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepayment).SelectSingle(new object[2]
    {
      (object) eventArgs.Row.AdjdOrderType,
      (object) eventArgs.Row.AdjdOrderNbr
    });
    if (poOrderPrepayment == null || poOrderPrepayment.IsRequest.GetValueOrDefault())
      return;
    ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepayment).Delete(poOrderPrepayment);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<POAdjust, POAdjust.adjdOrderNbr> eventArgs)
  {
    if (eventArgs.Row == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<POAdjust, POAdjust.adjdOrderNbr>, POAdjust, object>) eventArgs).OldValue == eventArgs.NewValue || eventArgs.Row.AdjdOrderType == null || eventArgs.Row.AdjdOrderNbr == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POAdjust, POAdjust.adjdOrderNbr>>) eventArgs).Cache.SetDefaultExt<POAdjust.adjdCuryInfoID>((object) eventArgs.Row);
    if (!(((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current?.DocType == "PPM") || eventArgs.Row.IsRequest.GetValueOrDefault())
      return;
    ((PXSelectBase) this.POAdjustments).Cache.SetDefaultExt<POAdjust.curyAdjgAmt>((object) eventArgs.Row);
    if (((PXSelectBase<POOrderPrepayment>) this.poOrderPrepayment).SelectSingle(new object[2]
    {
      (object) eventArgs.Row.AdjdOrderType,
      (object) eventArgs.Row.AdjdOrderNbr
    }) != null)
      return;
    ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepayment).Insert(new POOrderPrepayment()
    {
      OrderType = eventArgs.Row.AdjdOrderType,
      OrderNbr = eventArgs.Row.AdjdOrderNbr,
      APDocType = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.DocType,
      APRefNbr = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.RefNbr,
      IsRequest = new bool?(false),
      CuryAppliedAmt = new Decimal?(0M),
      AppliedAmt = new Decimal?(0M)
    });
  }

  protected virtual void SetPOAdjustAdjdAmt(POAdjust row)
  {
    if (row == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(row.AdjdCuryInfoID);
    if (currencyInfo.CuryID == ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.CuryID)
    {
      ((PXSelectBase) this.POAdjustments).Cache.SetValueExt<POAdjust.curyAdjdAmt>((object) row, (object) row.CuryAdjgAmt);
      ((PXSelectBase) this.POAdjustments).Cache.SetValueExt<POAdjust.adjdAmt>((object) row, (object) row.AdjgAmt);
    }
    else
    {
      Decimal num = currencyInfo.CuryConvCury(row.AdjgAmt.GetValueOrDefault());
      ((PXSelectBase) this.POAdjustments).Cache.SetValueExt<POAdjust.curyAdjdAmt>((object) row, (object) num);
      ((PXSelectBase) this.POAdjustments).Cache.SetValueExt<POAdjust.adjdAmt>((object) row, (object) row.AdjgAmt);
    }
  }

  protected virtual List<AdjustmentStubGroup> AddPOAdjustments(
    IEnumerable<AdjustmentStubGroup> adjustmentStubs)
  {
    POAdjust[] poadjustments = ((PXSelectBase<POAdjust>) this.POAdjustments).SelectMain(Array.Empty<object>());
    return adjustmentStubs.Where<AdjustmentStubGroup>((Func<AdjustmentStubGroup, bool>) (r => !((IEnumerable<POAdjust>) poadjustments).Any<POAdjust>((Func<POAdjust, bool>) (adj => adj.AdjdDocType == r.GroupedStubs.Key.AdjdDocType && adj.AdjdRefNbr == r.GroupedStubs.Key.AdjdRefNbr)))).Union<AdjustmentStubGroup>(((IEnumerable<POAdjust>) poadjustments).GroupBy<POAdjust, AdjustmentGroupKey, IAdjustmentStub>((Func<POAdjust, AdjustmentGroupKey>) (adj => new AdjustmentGroupKey()
    {
      Source = "P",
      AdjdDocType = adj.AdjdOrderType,
      AdjdRefNbr = adj.AdjdOrderNbr,
      AdjdCuryInfoID = adj.AdjdCuryInfoID
    }), (Func<POAdjust, IAdjustmentStub>) (adj => (IAdjustmentStub) adj)).Select<IGrouping<AdjustmentGroupKey, IAdjustmentStub>, AdjustmentStubGroup>((Func<IGrouping<AdjustmentGroupKey, IAdjustmentStub>, AdjustmentStubGroup>) (g => new AdjustmentStubGroup()
    {
      GroupedStubs = g
    }))).ToList<AdjustmentStubGroup>();
  }

  protected virtual AdjustmentStubGroup GetOutstandingBalanceRow()
  {
    Decimal? curyUnappliedBal = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.CuryUnappliedBal;
    if (curyUnappliedBal.HasValue)
    {
      Decimal? nullable = curyUnappliedBal;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        IEnumerable<IGrouping<AdjustmentGroupKey, IAdjustmentStub>> source = ((IEnumerable<IAdjustmentStub>) new IAdjustmentStub[1]
        {
          (IAdjustmentStub) new PX.Objects.PO.OutstandingBalanceRow()
          {
            CuryOutstandingBalance = curyUnappliedBal,
            OutstandingBalanceDate = ((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base).Accessinfo.BusinessDate
          }
        }).GroupBy<IAdjustmentStub, AdjustmentGroupKey, IAdjustmentStub>((Func<IAdjustmentStub, AdjustmentGroupKey>) (adj => new AdjustmentGroupKey()
        {
          Source = "T",
          AdjdDocType = "---",
          AdjdRefNbr = string.Empty,
          AdjdCuryInfoID = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.CuryInfoID
        }), (Func<IAdjustmentStub, IAdjustmentStub>) (adj => adj));
        return new AdjustmentStubGroup()
        {
          GroupedStubs = source.Single<IGrouping<AdjustmentGroupKey, IAdjustmentStub>>()
        };
      }
    }
    return (AdjustmentStubGroup) null;
  }

  protected virtual void AddPrepaymentToDocTypeList()
  {
    PXStringListAttribute stringListAttribute = ((PXSelectBase) ((PXGraphExtension<APPaymentEntry>) this).Base.Adjustments).Cache.GetAttributesReadonly<APAdjust.adjdDocType>((object) null).OfType<PXStringListAttribute>().FirstOrDefault<PXStringListAttribute>();
    if (stringListAttribute == null)
      return;
    List<string> stringList1 = new List<string>((IEnumerable<string>) stringListAttribute.ValueLabelDic.Keys);
    List<string> stringList2 = new List<string>((IEnumerable<string>) stringListAttribute.ValueLabelDic.Values);
    stringList1.Add("PPM");
    stringList2.Add("Prepayment");
    PXStringListAttribute.SetList<APAdjust.adjdDocType>(((PXSelectBase) ((PXGraphExtension<APPaymentEntry>) this).Base.Adjustments).Cache, (object) null, stringList1.ToArray(), stringList2.ToArray());
  }

  protected virtual bool IsPrepaymentCheck(APPayment payment)
  {
    bool flag1 = payment.DocType == "PPM";
    bool flag2 = payment.OrigModule == "PO";
    return !(flag1 & flag2) && flag1 && !((PXGraphExtension<APPaymentEntry>) this).Base.IsRequestPrepayment(payment);
  }

  [PXOverride]
  public virtual void RecalcApplAmounts(
    PXCache sender,
    APPayment row,
    bool aReadOnly,
    Action<PXCache, APPayment, bool> baseMethod)
  {
    PXFormulaAttribute.CalcAggregate<POAdjust.curyAdjgAmt>(((PXSelectBase) this.POAdjustments).Cache, (object) row, aReadOnly);
    if (baseMethod == null)
      return;
    baseMethod(sender, row, aReadOnly);
  }

  protected virtual void InsertUpdatePOAdjust(APAdjust apadjust)
  {
    APPayment current = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current;
    if ((current != null ? (EnumerableExtensions.IsNotIn<string>(current.DocType, "PPM", "VCK") ? 1 : 0) : 0) != 0 || apadjust?.AdjdDocType != "PPM")
      return;
    foreach (PXResult<POOrderPrepayment, PX.Objects.PO.POOrder> pxResult in ((PXSelectBase<POOrderPrepayment>) new PXSelectJoin<POOrderPrepayment, InnerJoin<PX.Objects.PO.POOrder, On<POOrderPrepayment.orderType, Equal<PX.Objects.PO.POOrder.orderType>, And<POOrderPrepayment.orderNbr, Equal<PX.Objects.PO.POOrder.orderNbr>>>>, Where<POOrderPrepayment.aPDocType, Equal<Required<APAdjust.adjdDocType>>, And<POOrderPrepayment.aPRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<POOrderPrepayment.aPDocType, Equal<APDocType.prepayment>>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base)).Select(new object[2]
    {
      (object) apadjust.AdjdDocType,
      (object) apadjust.AdjdRefNbr
    }))
    {
      PX.Objects.PO.POOrder poOrder = PXResult<POOrderPrepayment, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      POOrderPrepayment poOrderPrepayment = PXResult<POOrderPrepayment, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      POAdjust poAdjust = PXResultset<POAdjust>.op_Implicit(((PXSelectBase<POAdjust>) new PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<POAdjust.adjNbr, Equal<Current<APPayment.adjCntr>>, And<POAdjust.adjdDocType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<POAdjust.adjdRefNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base)).Select(new object[2]
      {
        (object) apadjust.AdjdDocType,
        (object) apadjust.AdjdRefNbr
      }));
      if (poAdjust == null)
        poAdjust = ((PXSelectBase<POAdjust>) this.POAdjustments).Insert(new POAdjust()
        {
          AdjgDocType = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.DocType,
          AdjgRefNbr = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.RefNbr,
          AdjdOrderType = poOrderPrepayment.OrderType,
          AdjdOrderNbr = poOrderPrepayment.OrderNbr,
          AdjdDocType = apadjust.AdjdDocType,
          AdjdRefNbr = apadjust.AdjdRefNbr,
          AdjNbr = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.AdjCntr,
          IsRequest = new bool?(true)
        });
      if (poOrder.CuryID == ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.CuryID)
      {
        ((PXSelectBase) this.POAdjustments).Cache.SetValueExt<POAdjust.curyAdjgAmt>((object) poAdjust, (object) apadjust.CuryAdjgAmt);
      }
      else
      {
        Decimal num = ((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().CuryConvCury(apadjust.AdjAmt.GetValueOrDefault());
        ((PXSelectBase) this.POAdjustments).Cache.SetValueExt<POAdjust.curyAdjgAmt>((object) poAdjust, (object) num);
      }
      ((PXSelectBase<POAdjust>) this.POAdjustments).Update(poAdjust);
    }
  }
}
