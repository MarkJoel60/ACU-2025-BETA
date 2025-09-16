// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.GraphExtensions.ApInvoiceEntryAddSubcontractsExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.Subcontracts.AP.CacheExtensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP.GraphExtensions;

public class ApInvoiceEntryAddSubcontractsExtension : 
  PXGraphExtension<
  #nullable disable
  AddPOOrderLineExtension, AddPOOrderExtension, APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<POOrderRS, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  POOrderRS.orderNbr, 
  #nullable disable
  Equal<PX.Objects.PO.POLine.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.PO.POOrder.orderType, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.PO.POLine.orderType>>>>>, POOrderRS>.View Subcontracts;
  [PXCopyPasteHiddenView]
  public PXSelect<POLineRS> SubcontractLines;
  public PXSelect<PX.Objects.PO.POLine> POLines;
  public PXAction<PX.Objects.AP.APInvoice> viewSubcontractFromSubcontracts;
  public PXAction<PX.Objects.AP.APInvoice> viewSubcontractFromSubcontractLines;
  public PXAction<PX.Objects.AP.APInvoice> AddSubcontracts;
  public PXAction<PX.Objects.AP.APInvoice> AddSubcontract;
  public PXAction<PX.Objects.AP.APInvoice> AddSubcontractLines;
  public PXAction<PX.Objects.AP.APInvoice> AddSubcontractLine;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && !SiteMapExtension.IsTaxBillsAndAdjustmentsScreenId();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual void ViewSubcontractFromSubcontracts()
  {
    POOrderRS current = ((PXSelectBase<POOrderRS>) this.Subcontracts).Current;
    if (current == null)
      return;
    PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, current.OrderType, current.OrderNbr) ?? throw new PXException("The {0} {1} purchase order was not found.", new object[2]
    {
      (object) current.OrderType,
      (object) current.OrderNbr
    });
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<SubcontractEntry>(), (object) poOrder, (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual void ViewSubcontractFromSubcontractLines()
  {
    POLineRS current = ((PXSelectBase<POLineRS>) this.SubcontractLines).Current;
    if (current == null)
      return;
    PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, current.OrderType, current.OrderNbr) ?? throw new PXException("The {0} {1} purchase order was not found.", new object[2]
    {
      (object) current.OrderType,
      (object) current.OrderNbr
    });
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<SubcontractEntry>(), (object) poOrder, (PXRedirectHelper.WindowMode) 3);
  }

  public IEnumerable poOrdersList()
  {
    return (IEnumerable) ((PXGraphExtension<AddPOOrderExtension, APInvoiceEntry>) this).Base1.pOOrderslist().Cast<POOrderRS>().Where<POOrderRS>((Func<POOrderRS, bool>) (po => po.OrderType != "RS"));
  }

  public IEnumerable poOrderLinesList()
  {
    return (IEnumerable) this.Base2.pOOrderLinesList().Cast<POLineRS>().Where<POLineRS>((Func<POLineRS, bool>) (line => line.OrderType != "RS"));
  }

  public IEnumerable subcontracts()
  {
    foreach (object obj in ((PXGraphExtension<AddPOOrderExtension, APInvoiceEntry>) this).Base1.GetPOOrderList().Where<PXResult<POOrderRS, PX.Objects.PO.POLine>>((Func<PXResult<POOrderRS, PX.Objects.PO.POLine>, bool>) (x => ((PXResult) x).GetItem<POOrderRS>().OrderType == "RS" && ((PXResult) x).GetItem<POOrderRS>().Status != "M")))
      yield return obj;
  }

  public IEnumerable subcontractLines()
  {
    PoOrderFilterExt extension = PXCache<POOrderFilter>.GetExtension<PoOrderFilterExt>(((PXSelectBase<POOrderFilter>) this.Base2.orderfilter).Current);
    IEnumerable<POLineRS> source = this.Base2.pOOrderLinesList().Cast<POLineRS>().Where<POLineRS>((Func<POLineRS, bool>) (line => line.OrderType == "RS" && EnumerableExtensions.IsIn<string>(extension.SubcontractNumber, (string) null, line.OrderNbr) && line.Status != "M"));
    foreach (POLineRS poLineRs in source)
      poLineRs.BilledQty = new Decimal?(poLineRs.BilledQty.GetValueOrDefault());
    if (!extension.ShowUnbilledLines.GetValueOrDefault())
      source = source.Where<POLineRS>((Func<POLineRS, bool>) (line =>
      {
        Decimal? billedQty = line.BilledQty;
        Decimal num1 = 0M;
        if (!(billedQty.GetValueOrDefault() == num1 & billedQty.HasValue))
          return true;
        Decimal? billedAmt = line.BilledAmt;
        Decimal num2 = 0M;
        return !(billedAmt.GetValueOrDefault() == num2 & billedAmt.HasValue);
      }));
    return (IEnumerable) source;
  }

  [PXMergeAttributes]
  [ApInvoiceEntryAddSubcontractsExtension.POOrderTypeList]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APTran.pOOrderType> _)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<POLineRS.lineNbr> _)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice> e)
  {
    APInvoiceState documentState = ((PXGraphExtension<APInvoiceEntry>) this).Base.GetDocumentState(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice>>) e).Cache, e.Row);
    ((PXAction) this.AddSubcontracts).SetEnabled(((PXAction) ((PXGraphExtension<AddPOOrderExtension, APInvoiceEntry>) this).Base1.addPOOrder).GetEnabled());
    ((PXAction) this.AddSubcontracts).SetVisible(documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment);
    ((PXAction) this.AddSubcontractLines).SetEnabled(((PXAction) this.Base2.addPOOrderLine).GetEnabled());
    ((PXAction) this.AddSubcontractLines).SetVisible(documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment);
    PXUIFieldAttribute.SetVisible<POLineRS.unbilledQty>(((PXSelectBase) this.SubcontractLines).Cache, (object) null, documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment);
    PXUIFieldAttribute.SetVisible<POLineRS.curyUnbilledAmt>(((PXSelectBase) this.SubcontractLines).Cache, (object) null, documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment);
    PXUIFieldAttribute.SetVisible<POLineRS.billedQty>(((PXSelectBase) this.SubcontractLines).Cache, (object) null, documentState.IsDocumentDebitAdjustment);
    PXUIFieldAttribute.SetVisible<POLineRS.curyBilledAmt>(((PXSelectBase) this.SubcontractLines).Cache, (object) null, documentState.IsDocumentDebitAdjustment);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyDiscAmt>(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) null, !documentState.IsDocumentDebitAdjustment && !documentState.IsDocumentReleasedOrPrebooked);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.discPct>(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Cache, (object) null, !documentState.IsDocumentDebitAdjustment && !documentState.IsDocumentReleasedOrPrebooked);
    PXUIFieldAttribute.SetVisible<PoOrderFilterExt.showUnbilledLines>(((PXSelectBase) this.Base2.orderfilter).Cache, (object) ((PXSelectBase<POOrderFilter>) this.Base2.orderfilter).Current, documentState.IsDocumentDebitAdjustment);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APTran> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AP.APInvoice current1 = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.Released;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    int num2;
    if (num1 == 0)
    {
      PX.Objects.AP.APInvoice current2 = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.Prebooked;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num2 = 1;
    bool flag = num2 != 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyDiscAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current?.DocType != "ADR" && !flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.discPct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, (object) e.Row, ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current?.DocType != "ADR" && !flag);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<POOrderFilter.showBilledLines> e)
  {
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POOrderFilter.showBilledLines>, object, object>) e).NewValue = (object) (current.DocType == "ADR");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PoOrderFilterExt.showUnbilledLines> e)
  {
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PoOrderFilterExt.showUnbilledLines>, object, object>) e).NewValue = (object) (current.DocType != "ADR");
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable addSubcontracts(PXAdapter adapter)
  {
    return !this.ShouldAddSubcontracts() ? adapter.Get() : this.addSubcontract(adapter);
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable addSubcontract(PXAdapter adapter)
  {
    return ApInvoiceEntryAddSubcontractsExtension.AddLines(new Func<PXAdapter, IEnumerable>(((PXGraphExtension<AddPOOrderExtension, APInvoiceEntry>) this).Base1.AddPOOrder2), adapter);
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable addSubcontractLines(PXAdapter adapter)
  {
    return !this.ShouldAddSubcontractLines() ? adapter.Get() : this.addSubcontractLine(adapter);
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable addSubcontractLine(PXAdapter adapter)
  {
    return ApInvoiceEntryAddSubcontractsExtension.AddLines(new Func<PXAdapter, IEnumerable>(this.Base2.AddPOOrderLine2), adapter);
  }

  private static IEnumerable AddLines(Func<PXAdapter, IEnumerable> addLine, PXAdapter adapter)
  {
    try
    {
      return addLine(adapter);
    }
    catch (PXException ex) when (ex.MessageNoPrefix == "One purchase order line or multiple purchase order lines cannot be added to the bill. See Trace Log for details.")
    {
      throw new Exception("One subcontract line or multiple subcontract lines cannot be added to the bill. See Trace Log for details.");
    }
  }

  private bool ShouldAddSubcontracts()
  {
    // ISSUE: method pointer
    return this.IsAdditionSubcontractsAvailable() && WebDialogResultExtension.IsPositive(((PXSelectBase<POOrderRS>) this.Subcontracts).AskExt(new PXView.InitializePanel((object) this, __methodptr(AddSubcontractsPanelInitializeHandler)), true));
  }

  private bool ShouldAddSubcontractLines()
  {
    // ISSUE: method pointer
    return this.IsAdditionSubcontractsAvailable() && WebDialogResultExtension.IsPositive(((PXSelectBase<POLineRS>) this.SubcontractLines).AskExt(new PXView.InitializePanel((object) this, __methodptr(AddSubcontractLinesPanelInitializeHandler)), true));
  }

  [PXOverride]
  public virtual bool ShouldAddPOOrder() => this.IsAdditionSubcontractsAvailable();

  [PXOverride]
  public virtual bool ShouldAddPOOrderLine() => this.IsAdditionSubcontractsAvailable();

  private bool IsAdditionSubcontractsAvailable()
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null && (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.DocType == "INV" || ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.DocType == "ADR"))
    {
      bool? nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Prebooked;
        bool flag2 = false;
        return nullable.GetValueOrDefault() == flag2 & nullable.HasValue;
      }
    }
    return false;
  }

  private void AddSubcontractLinesPanelInitializeHandler(PXGraph graph, string view)
  {
    ApInvoiceEntryAddSubcontractsExtension.ClearViewCache((PXSelectBase) this.Base2.orderfilter);
    ApInvoiceEntryAddSubcontractsExtension.ClearViewCache((PXSelectBase) this.SubcontractLines);
  }

  private void AddSubcontractsPanelInitializeHandler(PXGraph graph, string view)
  {
    ApInvoiceEntryAddSubcontractsExtension.ClearViewCache((PXSelectBase) ((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base).GetExtension<LinkLineExtension>().filter);
    ApInvoiceEntryAddSubcontractsExtension.ClearViewCache((PXSelectBase) this.Subcontracts);
  }

  private static void ClearViewCache(PXSelectBase selectBase)
  {
    selectBase.Cache.ClearQueryCache();
    selectBase.View.Clear();
    selectBase.Cache.Clear();
  }

  public class POOrderTypeListAttribute : PXStringListAttribute
  {
    public POOrderTypeListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("RO", "Normal"),
        PXStringListAttribute.Pair("DP", "Drop-Ship"),
        PXStringListAttribute.Pair("PD", "Project Drop-Ship"),
        PXStringListAttribute.Pair("BL", "Blanket"),
        PXStringListAttribute.Pair("SB", "Standard"),
        PXStringListAttribute.Pair("RS", "Subcontract")
      })
    {
    }
  }
}
