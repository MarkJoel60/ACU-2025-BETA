// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.Intercompany
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class Intercompany : PXGraphExtension<POReceiptEntry>
{
  public PXAction<PX.Objects.PO.POReceipt> generateSalesReturn;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable GenerateSalesReturn(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Intercompany intercompany = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    Intercompany.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new Intercompany.\u003C\u003Ec__DisplayClass2_0();
    ((PXAction) intercompany.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.poReturn = ((PXSelectBase<PX.Objects.PO.POReceipt>) intercompany.Base.Document).Current;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) intercompany.Base, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CGenerateSalesReturn\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) cDisplayClass20.poReturn;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public virtual PX.Objects.SO.SOOrder GenerateIntercompanySOReturn(
    PX.Objects.PO.POReceipt poReturn,
    string orderType)
  {
    if (!string.IsNullOrEmpty(poReturn.IntercompanySONbr) || poReturn.ReceiptType != "RN")
      throw new PXInvalidOperationException();
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, poReturn.BranchID);
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, (int?) branch?.BAccountID);
    if (customer == null)
      throw new PXException("The {0} company or branch has not been extended to a customer. To create an intercompany sales order, extend the company or branch to a customer on the Companies (CS101500) or Branches (CS102000) form, respectively.", new object[1]
      {
        (object) branch?.BranchCD.TrimEnd()
      });
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(poReturn.VendorID);
    List<PXResult<PX.Objects.PO.POReceiptLine>> linesForSoReturn = this.GetLinesForSOReturn(poReturn);
    int? nullable = (int?) linesForSoReturn.Select<PXResult<PX.Objects.PO.POReceiptLine>, PX.Objects.AR.ARTran>((Func<PXResult<PX.Objects.PO.POReceiptLine>, PX.Objects.AR.ARTran>) (res => PXResult.Unwrap<PX.Objects.AR.ARTran>((object) res))).FirstOrDefault<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (t => t != null && t.ProjectID.HasValue))?.ProjectID ?? ProjectDefaultAttribute.NonProject();
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    string str = orderType ?? ((PXSelectBase<SOSetup>) instance.sosetup).Current.DfltIntercompanyRMAType;
    bool flag = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
      flag = PXResultset<SOSetupApproval>.op_Implicit(((PXSelectBase<SOSetupApproval>) instance.SetupApproval).Select(new object[1]
      {
        (object) str
      })) != null;
    PX.Objects.SO.SOOrder soOrder = new PX.Objects.SO.SOOrder()
    {
      OrderType = str,
      BranchID = new int?(branchByBaccountId.BranchID),
      Hold = new bool?(flag)
    };
    PX.Objects.SO.SOOrder copy1 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Insert(soOrder));
    copy1.CustomerID = customer.BAccountID;
    copy1.ProjectID = nullable;
    copy1.IntercompanyPOReturnNbr = poReturn.ReceiptNbr;
    PX.Objects.SO.SOOrder copy2 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy1));
    copy2.OrderDate = poReturn.ReceiptDate;
    copy2.CustomerOrderNbr = poReturn.ReceiptNbr;
    PX.Objects.SO.SOOrder copy3 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy2));
    copy3.DisableAutomaticDiscountCalculation = new bool?(true);
    PX.Objects.SO.SOOrder copy4 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy3));
    copy4.BranchID = new int?(branchByBaccountId.BranchID);
    copy4.CuryID = poReturn.CuryID;
    PX.Objects.SO.SOOrder copy5 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy4));
    PX.Objects.CM.CurrencyInfo cm = this.Base.GetCurrencyInfo(poReturn.CuryInfoID).GetCM();
    PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) instance.currencyinfo).Select(Array.Empty<object>()));
    if (string.Equals(cm.BaseCuryID, currencyInfo.BaseCuryID, StringComparison.OrdinalIgnoreCase))
    {
      PXCache<PX.Objects.CM.CurrencyInfo>.RestoreCopy(currencyInfo, cm);
      currencyInfo.CuryInfoID = copy5.CuryInfoID;
    }
    this.AddSOReturnLines(instance, linesForSoReturn);
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current != null)
    {
      PX.Objects.SO.SOOrder copy6 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current);
      copy6.CuryControlTotal = copy6.CuryOrderTotal;
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy6);
    }
    PXGraph.RowPersistedEvents rowPersisted1 = ((PXGraph) instance).RowPersisted;
    Intercompany intercompany1 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted1 = new PXRowPersisted((object) intercompany1, __vmethodptr(intercompany1, UpdatePOReceiptOnSOOrderRowPersisted));
    rowPersisted1.AddHandler<PX.Objects.SO.SOOrder>(pxRowPersisted1);
    UniquenessChecker<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOOrder.intercompanyPOReturnNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>> uniquenessChecker = new UniquenessChecker<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOOrder.intercompanyPOReturnNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>((IBqlTable) poReturn);
    ((PXGraph) instance).OnBeforeCommit += new Action<PXGraph>(uniquenessChecker.OnBeforeCommitImpl);
    try
    {
      ((PXAction) instance.Save).Press();
    }
    finally
    {
      PXGraph.RowPersistedEvents rowPersisted2 = ((PXGraph) instance).RowPersisted;
      Intercompany intercompany2 = this;
      // ISSUE: virtual method pointer
      PXRowPersisted pxRowPersisted2 = new PXRowPersisted((object) intercompany2, __vmethodptr(intercompany2, UpdatePOReceiptOnSOOrderRowPersisted));
      rowPersisted2.RemoveHandler<PX.Objects.SO.SOOrder>(pxRowPersisted2);
      ((PXGraph) instance).OnBeforeCommit -= new Action<PXGraph>(uniquenessChecker.OnBeforeCommitImpl);
    }
    return ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current;
  }

  protected virtual void UpdatePOReceiptOnSOOrderRowPersisted(
    PXCache sender,
    PXRowPersistedEventArgs e)
  {
    PX.Objects.SO.SOOrder row = (PX.Objects.SO.SOOrder) e.Row;
    if (string.IsNullOrEmpty(row?.IntercompanyPOReturnNbr) || e.Operation != 2 || e.TranStatus != null)
      return;
    PXDatabase.Update<PX.Objects.PO.POReceipt>(new PXDataFieldParam[3]
    {
      (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.PO.POReceipt.isIntercompanySOCreated>((PXDbType) 2, (object) true),
      (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.PO.POReceipt.receiptType>((PXDbType) 3, (object) "RN"),
      (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.PO.POReceipt.receiptNbr>((PXDbType) 12, (object) row.IntercompanyPOReturnNbr)
    });
  }

  protected virtual void AddSOReturnLines(
    SOOrderEntry graph,
    List<PXResult<PX.Objects.PO.POReceiptLine>> linesForSOReturn)
  {
    foreach (PXResult<PX.Objects.PO.POReceiptLine> pxResult in linesForSOReturn)
    {
      PX.Objects.PO.POReceiptLine poReceiptLine = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(pxResult);
      PX.Objects.AR.ARTran arTran = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) pxResult);
      List<POReceiptLineSplit> list = GraphHelper.RowCast<POReceiptLineSplit>((IEnumerable) PXSelectBase<POReceiptLineSplit, PXViewOf<POReceiptLineSplit>.BasedOn<SelectFromBase<POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.receiptNbr>, Field<POReceiptLineSplit.lineNbr>.IsRelatedTo<PX.Objects.PO.POReceiptLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POReceiptLine, POReceiptLineSplit>, PX.Objects.PO.POReceiptLine, POReceiptLineSplit>.SameAsCurrent>>.ReadOnly.Config>.SelectMultiBound((PXGraph) graph, (object[]) new PX.Objects.PO.POReceiptLine[1]
      {
        poReceiptLine
      }, Array.Empty<object>())).ToList<POReceiptLineSplit>();
      PX.Objects.SO.SOLine soLine1 = new PX.Objects.SO.SOLine();
      PX.Objects.SO.SOLine copy1 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Insert(soLine1));
      copy1.InventoryID = poReceiptLine.InventoryID;
      copy1.SubItemID = poReceiptLine.SubItemID;
      copy1.TaxCategoryID = arTran?.TaxCategoryID;
      int? nullable1;
      if (arTran != null)
      {
        nullable1 = arTran.ProjectID;
        if (nullable1.HasValue)
        {
          copy1.ProjectID = arTran.ProjectID;
          copy1.TaskID = arTran.TaskID;
          copy1.CostCodeID = arTran.CostCodeID;
          goto label_6;
        }
      }
      copy1.ProjectID = ProjectDefaultAttribute.NonProject();
label_6:
      copy1.IntercompanyPOLineNbr = poReceiptLine.LineNbr;
      PX.Objects.SO.SOLine copy2 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Update(copy1));
      if (arTran != null)
      {
        nullable1 = arTran.SiteID;
        if (nullable1.HasValue)
          copy2.SiteID = arTran.SiteID;
      }
      if (arTran != null && arTran.AvalaraCustomerUsageType != null)
        copy2.AvalaraCustomerUsageType = arTran.AvalaraCustomerUsageType;
      copy2.TranDesc = poReceiptLine.TranDesc;
      copy2.UOM = poReceiptLine.UOM;
      copy2.ManualPrice = new bool?(true);
      copy2.ManualDisc = new bool?(true);
      PX.Objects.SO.SOLine copy3 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Update(copy2));
      PX.Objects.SO.SOLine soLine2 = copy3;
      short? invtMult = copy3.InvtMult;
      Decimal? nullable2 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3 = poReceiptLine.ReceiptQty;
      Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
      soLine2.OrderQty = nullable4;
      copy3.CuryUnitPrice = poReceiptLine.CuryUnitCost;
      PX.Objects.SO.SOLine copy4 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Update(copy3));
      PX.Objects.SO.SOLine soLine3 = copy4;
      invtMult = copy4.InvtMult;
      nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      nullable2 = poReceiptLine.CuryExtCost;
      Decimal? nullable5 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      soLine3.CuryExtPrice = nullable5;
      PX.Objects.SO.SOLine copy5 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Update(copy4));
      PX.Objects.SO.SOLine soLine4 = copy5;
      Decimal? nullable6;
      if (arTran == null)
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = arTran.DiscPct;
      nullable2 = nullable6;
      Decimal? nullable7 = nullable2 ?? poReceiptLine.DiscPct;
      soLine4.DiscPct = nullable7;
      if (arTran.RefNbr != null)
      {
        copy5 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Update(copy5));
        copy5.InvoiceType = arTran.TranType;
        copy5.InvoiceNbr = arTran.RefNbr;
        copy5.InvoiceLineNbr = arTran.LineNbr;
        copy5.InvoiceDate = arTran.TranDate;
        copy5.OrigOrderType = arTran.SOOrderType;
        copy5.OrigOrderNbr = arTran.SOOrderNbr;
        copy5.OrigLineNbr = arTran.SOOrderLineNbr;
        copy5.SalesPersonID = arTran.SalesPersonID;
        copy5.Commissionable = arTran.Commissionable;
      }
      PX.Objects.SO.SOLine line = ((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Update(copy5);
      if (list.Count > 1 || list.Any<POReceiptLineSplit>((Func<POReceiptLineSplit, bool>) (s => !string.IsNullOrEmpty(s.LotSerialNbr))))
      {
        graph.LineSplittingExt.RaiseRowDeleted(line);
        line.Operation = "R";
        ((PXSelectBase<PX.Objects.SO.SOLine>) graph.Transactions).Update(line);
        foreach (POReceiptLineSplit receiptLineSplit in list)
        {
          PX.Objects.SO.SOLineSplit copy6 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLineSplit>) graph.splits).Insert());
          copy6.SubItemID = receiptLineSplit.SubItemID;
          copy6.LotSerialNbr = receiptLineSplit.LotSerialNbr;
          copy6.ExpireDate = receiptLineSplit.ExpireDate;
          copy6.UOM = receiptLineSplit.UOM;
          PX.Objects.SO.SOLineSplit copy7 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLineSplit>) graph.splits).Update(copy6));
          copy7.Qty = receiptLineSplit.Qty;
          ((PXSelectBase<PX.Objects.SO.SOLineSplit>) graph.splits).Update(copy7);
        }
      }
    }
  }

  protected virtual List<PXResult<PX.Objects.PO.POReceiptLine>> GetLinesForSOReturn(
    PX.Objects.PO.POReceipt poReturn)
  {
    PXView pxView = new PXView((PXGraph) this.Base, true, (BqlCommand) new SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<POReceiptLine2>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine2.receiptType, Equal<PX.Objects.PO.POReceiptLine.origReceiptType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine2.receiptNbr, Equal<PX.Objects.PO.POReceiptLine.origReceiptNbr>>>>>.And<BqlOperand<POReceiptLine2.lineNbr, IBqlInt>.IsEqual<PX.Objects.PO.POReceiptLine.origReceiptLineNbr>>>>>, FbqlJoins.Left<PX.Objects.PO.POReceipt>.On<PX.Objects.PO.POReceiptLine.FK.OriginalReceipt>>, FbqlJoins.Left<SOShipLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.shipmentNbr, Equal<PX.Objects.PO.POReceipt.intercompanyShipmentNbr>>>>>.And<BqlOperand<SOShipLine.lineNbr, IBqlInt>.IsEqual<POReceiptLine2.intercompanyShipmentLineNbr>>>>, FbqlJoins.Left<PX.Objects.AR.ARTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.sOShipmentType, Equal<SOShipLine.shipmentType>>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOShipmentNbr, IBqlString>.IsEqual<SOShipLine.shipmentNbr>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOShipmentLineGroupNbr, IBqlInt>.IsEqual<SOShipLine.invoiceGroupNbr>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOShipmentLineNbr, IBqlInt>.IsEqual<SOShipLine.lineNbr>>>>.And<BqlOperand<PX.Objects.AR.ARTran.released, IBqlBool>.IsEqual<True>>>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.PO.POReceiptLine.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.PO.POReceiptLine.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine>.SameAsCurrent>());
    using (new PXFieldScope(pxView, new System.Type[2]
    {
      typeof (PX.Objects.PO.POReceiptLine),
      typeof (PX.Objects.AR.ARTran)
    }))
      return pxView.SelectMultiBound((object[]) new PX.Objects.PO.POReceipt[1]
      {
        poReturn
      }, Array.Empty<object>()).Cast<PXResult<PX.Objects.PO.POReceiptLine>>().ToList<PXResult<PX.Objects.PO.POReceiptLine>>();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.PO.POReceipt> eventArgs)
  {
    if (eventArgs.Row == null || eventArgs.Row.ReceiptType != "RN")
      return;
    bool? nullable1 = eventArgs.Row.IsIntercompany;
    if (!nullable1.GetValueOrDefault())
      return;
    using (new PXReadBranchRestrictedScope())
    {
      PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOOrder.intercompanyPOReturnNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POReceipt[1]
      {
        eventArgs.Row
      }, Array.Empty<object>()));
      eventArgs.Row.IntercompanySOType = soOrder?.OrderType;
      eventArgs.Row.IntercompanySONbr = soOrder?.OrderNbr;
      PX.Objects.PO.POReceipt row = eventArgs.Row;
      bool? nullable2;
      if (soOrder == null)
      {
        nullable1 = new bool?();
        nullable2 = nullable1;
      }
      else
        nullable2 = soOrder.Cancelled;
      row.IntercompanySOCancelled = nullable2;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POReceipt>>) eventArgs).Cache, (object) eventArgs.Row).For<PX.Objects.PO.POReceipt.intercompanyShipmentNbr>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = eventArgs.Row.ReceiptType == "RT" && eventArgs.Row.IsIntercompany.GetValueOrDefault();
      a.Enabled = false;
    })).For<PX.Objects.PO.POReceipt.intercompanySOType>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = eventArgs.Row.ReceiptType == "RN" && eventArgs.Row.IsIntercompany.GetValueOrDefault();
      a.Enabled = false;
    })).SameFor<PX.Objects.PO.POReceipt.intercompanySONbr>().For<PX.Objects.PO.POReceipt.excludeFromIntercompanyProc>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = eventArgs.Row.ReceiptType == "RN" && eventArgs.Row.IsIntercompany.GetValueOrDefault();
      a.Enabled = true;
    }));
    bool? nullable = eventArgs.Row.IsIntercompany;
    if (nullable.GetValueOrDefault() && eventArgs.Row.IntercompanySONbr != null)
    {
      nullable = eventArgs.Row.IntercompanySOCancelled;
      if (nullable.GetValueOrDefault())
      {
        ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceipt.intercompanySONbr>((object) eventArgs.Row, (object) eventArgs.Row.IntercompanySONbr, (Exception) new PXSetPropertyException("The related {0} return order has been canceled.", (PXErrorLevel) 2, new object[1]
        {
          (object) eventArgs.Row.IntercompanySONbr
        }));
        return;
      }
    }
    nullable = eventArgs.Row.IsIntercompanySOCreated;
    if (!nullable.GetValueOrDefault() || eventArgs.Row.IntercompanySONbr != null)
      return;
    ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.PO.POReceipt.intercompanySONbr>((object) eventArgs.Row, (object) eventArgs.Row.IntercompanySONbr, (Exception) new PXSetPropertyException("The related sales order has been deleted.", (PXErrorLevel) 2));
  }

  [PXOverride]
  public virtual int? GetNonStockExpenseAccount(
    PX.Objects.PO.POReceiptLine row,
    PX.Objects.IN.InventoryItem item,
    Func<PX.Objects.PO.POReceiptLine, PX.Objects.IN.InventoryItem, int?> baseFunc)
  {
    if (row != null && !row.AccrueCost.GetValueOrDefault() && row.PONbr == null && !item.StkItem.GetValueOrDefault())
    {
      PX.Objects.AP.Vendor current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current;
      if ((current != null ? (current.IsBranch.GetValueOrDefault() ? 1 : 0) : 0) != 0 && ((PXSelectBase<APSetup>) this.Base.apsetup).Current?.IntercompanyExpenseAccountDefault == "L")
        return ((PXSelectBase<PX.Objects.CR.Location>) this.Base.location).Current?.VExpenseAcctID;
    }
    return baseFunc(row, item);
  }
}
