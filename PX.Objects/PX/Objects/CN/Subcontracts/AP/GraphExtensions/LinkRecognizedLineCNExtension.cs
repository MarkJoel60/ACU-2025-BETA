// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.GraphExtensions.LinkRecognizedLineCNExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.CN.Subcontracts.AP.CacheExtensions;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.Subcontracts.AP.GraphExtensions;

public class LinkRecognizedLineCNExtension : 
  PXGraphExtension<
  #nullable disable
  LinkRecognizedLineExtension, APInvoiceRecognitionEntry>
{
  public PXFilter<LinkSubcontractLineFilter> linkSubcontractLineFilter;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<POLineS, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.PO.POOrder>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  POLineS.orderNbr, 
  #nullable disable
  Equal<PX.Objects.PO.POOrder.orderNbr>>>>, And<BqlOperand<
  #nullable enable
  POLineS.orderType, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.PO.POOrder.orderType>>>>.And<BqlOperand<
  #nullable enable
  POLineS.orderType, IBqlString>.IsEqual<
  #nullable disable
  POOrderType.regularSubcontract>>>>>, POLineS>.View linkSubcontractTran;
  public PXAction<APRecognizedInvoice> linkSubcontractLine;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && PXAccess.FeatureInstalled<FeaturesSet.projectRelatedDocumentsRecognition>();
  }

  public virtual IEnumerable LinkSubcontractTran()
  {
    LinkSubcontractLineFilter current = ((PXSelectBase<LinkSubcontractLineFilter>) this.linkSubcontractLineFilter).Current;
    return (IEnumerable) this.GetAvailableSubcontractLines(((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Current, current?.POOrderNbr, (int?) current?.InventoryID, current?.UOM);
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LinkSubcontractLine(PXAdapter adapter)
  {
    if (!this.ValidateLinkLine())
      return adapter.Get();
    ((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache.ClearQueryCache();
    WebDialogResult webDialogResult;
    // ISSUE: method pointer
    if ((webDialogResult = ((PXSelectBase<LinkSubcontractLineFilter>) this.linkSubcontractLineFilter).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CLinkSubcontractLine\u003Eb__5_0)), true)) != null && webDialogResult == 6 && ((PXSelectBase) this.linkSubcontractTran).Cache.Updated.Count() > 0L)
    {
      APRecognizedTran apTran = this.Base1.ClearAPTranReferences((APRecognizedTran) ((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache.CreateCopy((object) ((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Current));
      foreach (POLineS order in ((PXSelectBase) this.linkSubcontractTran).Cache.Updated)
      {
        if (order.Selected.GetValueOrDefault())
        {
          this.Base1.LinkToOrder(apTran, order);
          break;
        }
      }
      ((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache.Update((object) apTran);
    }
    return adapter.Get();
  }

  protected void _(PX.Data.Events.RowSelected<APRecognizedInvoice> e)
  {
    if (e.Row == null)
      return;
    PXAction<APRecognizedInvoice> linkSubcontractLine = this.linkSubcontractLine;
    int num;
    if (e.Row.DocType == "INV")
    {
      int? nullable = e.Row.VendorID;
      if (nullable.HasValue)
      {
        nullable = e.Row.VendorLocationID;
        if (nullable.HasValue)
        {
          num = EnumerableExtensions.IsNotIn<string>(e.Row.RecognitionStatus, "P", "N") ? 1 : 0;
          goto label_6;
        }
      }
    }
    num = 0;
label_6:
    ((PXAction) linkSubcontractLine).SetEnabled(num != 0);
  }

  protected virtual void _(PX.Data.Events.RowSelected<APRecognizedTran> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<ApTranExt.subcontractLineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APRecognizedTran>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<APRecognizedTran, APRecognizedTran.recognizedPONumber> args)
  {
    if (!this.IsSubcontract((PX.Objects.AP.APTran) args.Row))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<APRecognizedTran, APRecognizedTran.recognizedPONumber>>) args).ReturnValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<APRecognizedTran, PX.Objects.PM.CacheExtensions.APRecognizedTranExt.recognizedPOLineNbr> args)
  {
    if (!this.IsSubcontract((PX.Objects.AP.APTran) args.Row))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<APRecognizedTran, PX.Objects.PM.CacheExtensions.APRecognizedTranExt.recognizedPOLineNbr>>) args).ReturnValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<APRecognizedTran, APRecognizedTran.pONumberJson> args)
  {
    if (!this.IsSubcontract((PX.Objects.AP.APTran) args.Row))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<APRecognizedTran, APRecognizedTran.pONumberJson>>) args).ReturnValue = (object) null;
  }

  private void PopulateFilterValues()
  {
    ((PXSelectBase<LinkSubcontractLineFilter>) this.linkSubcontractLineFilter).Current.InventoryID = (int?) ((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Current?.InventoryID;
    ((PXSelectBase<LinkSubcontractLineFilter>) this.linkSubcontractLineFilter).Current.UOM = ((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Current.UOM;
    ((PXSelectBase<LinkSubcontractLineFilter>) this.linkSubcontractLineFilter).Current.POOrderNbr = (string) null;
  }

  private void ClearResults()
  {
    ((PXSelectBase) this.linkSubcontractTran).View.Clear();
    ((PXSelectBase) this.linkSubcontractTran).Cache.Clear();
    ((PXSelectBase) this.linkSubcontractTran).Cache.ClearQueryCache();
  }

  protected virtual List<PXResult<POLineS, PX.Objects.PO.POOrder>> GetAvailableSubcontractLines(
    APRecognizedTran currentRecognizedTran,
    string pONbr,
    int? inventoryID,
    string uOM)
  {
    List<PXResult<POLineS, PX.Objects.PO.POOrder>> subcontractLines = new List<PXResult<POLineS, PX.Objects.PO.POOrder>>();
    if (currentRecognizedTran == null)
      return subcontractLines;
    Lazy<POAccrualSet> usedPOAccrual = this.GetUsedPoAccrual(currentRecognizedTran);
    string[] orderTypes = new string[1]{ "RS" };
    foreach (LinkLineOrder linkLineOrder in GraphHelper.RowCast<LinkLineOrder>((IEnumerable) this.Base1.GetLinkLineOrders(pONbr, inventoryID, uOM, (int?) ((PXSelectBase<APRecognizedInvoice>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Document).Current?.VendorLocationID, orderTypes)).AsEnumerable<LinkLineOrder>().Where<LinkLineOrder>((Func<LinkLineOrder, bool>) (l => !usedPOAccrual.Value.Contains(l))))
    {
      PXResult<POLineS, PX.Objects.PO.POOrder> pxResult = (PXResult<POLineS, PX.Objects.PO.POOrder>) PXResultset<POLineS>.op_Implicit(((PXSelectBase<POLineS>) this.Base1.POLineLink).Select(new object[3]
      {
        (object) linkLineOrder.OrderNbr,
        (object) linkLineOrder.OrderType,
        (object) linkLineOrder.OrderLineNbr
      }));
      if (((PXSelectBase) this.linkSubcontractTran).Cache.GetStatus((object) PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult)) != 1 && PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult).CompareReferenceKey((PX.Objects.AP.APTran) currentRecognizedTran))
      {
        ((PXSelectBase) this.linkSubcontractTran).Cache.SetValue<POLineS.selected>((object) PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult), (object) true);
        ((PXSelectBase) this.linkSubcontractTran).Cache.SetStatus((object) PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult), (PXEntryStatus) 1);
      }
      subcontractLines.Add(pxResult);
    }
    return subcontractLines;
  }

  [PXOverride]
  public virtual string[] GetAutoLinkAllowedOrderTypes(
    PX.Objects.CR.Location location,
    LinkRecognizedLineCNExtension.GetAutoLinkAllowedOrdersTypesDelegate baseMethod)
  {
    List<string> stringList = new List<string>() { "RS" };
    if (location.VAllowAPBillBeforeReceipt.GetValueOrDefault())
      stringList.AddRange((IEnumerable<string>) new POOrderType.ListAttribute().ValueLabelDic.Keys.ToList<string>());
    return stringList.ToArray();
  }

  [PXOverride]
  public void LinkToOrder(
    APRecognizedTran apTran,
    POLineS order,
    LinkRecognizedLineCNExtension.LinkToOrderDelegate baseMethod)
  {
    if (order.OrderType == "RS")
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base, apTran.InventoryID);
      if ((inventoryItem != null ? (inventoryItem.NonStockReceipt.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return;
      apTran.UOM = order.UOM;
    }
    baseMethod(apTran, order);
  }

  protected virtual bool ValidateLinkLine()
  {
    APRecognizedTran current = ((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Current;
    if (current == null || !(current.TranType == "INV"))
      return false;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base, ((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Current.InventoryID);
    if (inventoryItem == null || !inventoryItem.NonStockReceipt.GetValueOrDefault())
      return true;
    PXUIFieldAttribute.SetWarning<PX.Objects.AP.APTran.inventoryID>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) ((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Current, PXMessages.LocalizeNoPrefix("You cannot link this subcontract line to the current AP document line because the inventory item specified in the line requires a receipt. Select a line with the empty item code or with a non-stock item that is configured so that the system does not require a receipt for it."));
    return false;
  }

  protected virtual bool IsSubcontract(PX.Objects.AP.APTran apTran) => apTran?.POOrderType == "RS";

  private Lazy<POAccrualSet> GetUsedPoAccrual(APRecognizedTran currentRecognizedTran)
  {
    return new Lazy<POAccrualSet>((Func<POAccrualSet>) (() =>
    {
      POAccrualSet usedPoAccrual = new POAccrualSet((IEnumerable<PX.Objects.AP.APTran>) GraphHelper.RowCast<APRecognizedTran>((IEnumerable) ((PXSelectBase<APRecognizedTran>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Select(Array.Empty<object>())), ((PXSelectBase<APRecognizedInvoice>) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Document).Current?.DocType == "PPM" ? (IEqualityComparer<PX.Objects.AP.APTran>) new POLineComparer() : (IEqualityComparer<PX.Objects.AP.APTran>) new POAccrualComparer());
      usedPoAccrual.Remove((PX.Objects.AP.APTran) currentRecognizedTran);
      return usedPoAccrual;
    }));
  }

  public delegate string[] GetAutoLinkAllowedOrdersTypesDelegate(PX.Objects.CR.Location location);

  public delegate void LinkToOrderDelegate(APRecognizedTran apTran, POLineS order);
}
