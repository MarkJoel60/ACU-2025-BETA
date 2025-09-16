// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.Intercompany
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class Intercompany : PXGraphExtension<POOrderEntry>
{
  public PXAction<PX.Objects.PO.POOrder> generateSalesOrder;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable GenerateSalesOrder(PXAdapter adapter)
  {
    Intercompany intercompany = this;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    Intercompany.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new Intercompany.\u003C\u003Ec__DisplayClass2_0();
    ((PXAction) intercompany.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.po = ((PXSelectBase<PX.Objects.PO.POOrder>) intercompany.Base.Document).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.lines = GraphHelper.RowCast<PX.Objects.PO.POLine>((IEnumerable) ((PXSelectBase<PX.Objects.PO.POLine>) intercompany.Base.Transactions).Select(Array.Empty<object>())).ToList<PX.Objects.PO.POLine>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.copyProjectDetails = true;
    bool generateSalesOrder = true;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    intercompany.ValidateProject(cDisplayClass20.lines, out cDisplayClass20.copyProjectDetails, out generateSalesOrder);
    if (generateSalesOrder)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.shipAddress = PXResultset<POShipAddress>.op_Implicit(((PXSelectBase<POShipAddress>) intercompany.Base.Shipping_Address).Select(Array.Empty<object>()));
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.shipContact = PXResultset<POShipContact>.op_Implicit(((PXSelectBase<POShipContact>) intercompany.Base.Shipping_Contact).Select(Array.Empty<object>()));
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.discountLines = GraphHelper.RowCast<POOrderDiscountDetail>((IEnumerable) ((PXSelectBase<POOrderDiscountDetail>) intercompany.Base.DiscountDetails).Select(Array.Empty<object>())).ToList<POOrderDiscountDetail>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) intercompany.Base, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CGenerateSalesOrder\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    yield return (object) cDisplayClass20.po;
  }

  public virtual void ValidateProject(
    List<PX.Objects.PO.POLine> lines,
    out bool copyProjectDetails,
    out bool generateSalesOrder)
  {
    copyProjectDetails = true;
    generateSalesOrder = true;
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return;
    bool flag1 = true;
    bool flag2 = false;
    int? nullable1 = ProjectDefaultAttribute.NonProject();
    int? nullable2 = new int?();
    foreach (PX.Objects.PO.POLine line in lines)
    {
      if (!nullable2.HasValue)
        nullable2 = line.ProjectID;
      int? projectId = line.ProjectID;
      int? nullable3 = nullable1;
      if (!(projectId.GetValueOrDefault() == nullable3.GetValueOrDefault() & projectId.HasValue == nullable3.HasValue))
        flag2 = true;
      nullable3 = nullable2;
      projectId = line.ProjectID;
      if (!(nullable3.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable3.HasValue == projectId.HasValue))
      {
        flag1 = false;
        break;
      }
    }
    if (!flag2)
      return;
    if (flag1)
    {
      if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Ask("Warning", "Do you want to copy the project details to the sales order?", (MessageButtons) 4) == 6)
        return;
      copyProjectDetails = false;
    }
    else if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Ask("Warning", "The sales order will be created with the non-project code and project details will not be copied for the purchase order lines because the lines are assigned to different projects.~Do you want to proceed?~~To copy the project details to the related sales order, create a separate purchase order for each project.", (MessageButtons) 4) == 6)
      copyProjectDetails = false;
    else
      generateSalesOrder = false;
  }

  public virtual PX.Objects.SO.SOOrder GenerateIntercompanySalesOrder(
    PX.Objects.PO.POOrder po,
    POShipAddress shipAddress,
    POShipContact shipContact,
    IEnumerable<PX.Objects.PO.POLine> lines,
    IEnumerable<POOrderDiscountDetail> discountLines,
    string orderType,
    bool copyProjectDetails)
  {
    if (!string.IsNullOrEmpty(po.IntercompanySONbr) || EnumerableExtensions.IsNotIn<string>(po.OrderType, "RO", "DP"))
      throw new PXInvalidOperationException();
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, po.BranchID);
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, (int?) branch?.BAccountID);
    if (customer == null)
      throw new PXException("The {0} company or branch has not been extended to a customer. To create an intercompany sales order, extend the company or branch to a customer on the Companies (CS101500) or Branches (CS102000) form, respectively.", new object[1]
      {
        (object) branch?.BranchCD.TrimEnd()
      });
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(po.VendorID);
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    orderType = orderType ?? ((PXSelectBase<SOSetup>) instance.sosetup).Current.DfltIntercompanyOrderType;
    bool flag = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
      flag = PXResultset<SOSetupApproval>.op_Implicit(((PXSelectBase<SOSetupApproval>) instance.SetupApproval).Select(new object[1]
      {
        (object) orderType
      })) != null;
    PX.Objects.SO.SOOrder soOrder = new PX.Objects.SO.SOOrder()
    {
      OrderType = orderType,
      BranchID = new int?(branchByBaccountId.BranchID),
      Hold = new bool?(flag)
    };
    PX.Objects.SO.SOOrder copy1 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Insert(soOrder));
    copy1.CustomerID = customer.BAccountID;
    copy1.ProjectID = !copyProjectDetails || !lines.Any<PX.Objects.PO.POLine>() ? ProjectDefaultAttribute.NonProject() : lines.First<PX.Objects.PO.POLine>().ProjectID;
    copy1.IntercompanyPOType = po.OrderType;
    copy1.IntercompanyPONbr = po.OrderNbr;
    copy1.IntercompanyPOWithEmptyInventory = new bool?(lines.Any<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (pol => !pol.InventoryID.HasValue && pol.LineType != "DN")));
    PX.Objects.SO.SOOrder copy2 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy1));
    copy2.BranchID = new int?(branchByBaccountId.BranchID);
    copy2.OrderDate = po.OrderDate;
    copy2.RequestDate = po.ExpectedDate;
    copy2.CustomerOrderNbr = po.OrderNbr;
    using (new SOOrderEntry.SkipCalculateTotalDocDiscountScope())
    {
      copy2.CuryDiscTot = po.CuryDiscTot;
      copy2 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy2));
    }
    PX.Objects.SO.SOOrder copy3 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy2));
    copy3.DisableAutomaticDiscountCalculation = new bool?(true);
    PX.Objects.SO.SOOrder copy4 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy3));
    SharedRecordAttribute.CopyRecord<PX.Objects.SO.SOOrder.shipAddressID>(((PXSelectBase) instance.Document).Cache, (object) copy4, (object) shipAddress, true);
    SharedRecordAttribute.CopyRecord<PX.Objects.SO.SOOrder.shipContactID>(((PXSelectBase) instance.Document).Cache, (object) copy4, (object) shipContact, true);
    PX.Objects.SO.SOOrder copy5 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy4));
    copy5.CuryID = po.CuryID;
    PX.Objects.SO.SOOrder copy6 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy5));
    PX.Objects.CM.CurrencyInfo cm = ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(po.CuryInfoID).GetCM();
    PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) instance.currencyinfo).Select(Array.Empty<object>()));
    if (string.Equals(cm.BaseCuryID, currencyInfo.BaseCuryID, StringComparison.OrdinalIgnoreCase))
    {
      PXCache<PX.Objects.CM.CurrencyInfo>.RestoreCopy(currencyInfo, cm);
      currencyInfo.CuryInfoID = copy6.CuryInfoID;
    }
    foreach (PX.Objects.PO.POLine poLine in lines.Where<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (pol => pol.InventoryID.HasValue)))
    {
      PX.Objects.SO.SOLine soLine = new PX.Objects.SO.SOLine()
      {
        BranchID = new int?(branchByBaccountId.BranchID)
      };
      PX.Objects.SO.SOLine copy7 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Insert(soLine));
      copy7.InventoryID = poLine.InventoryID;
      copy7.SubItemID = poLine.SubItemID;
      copy7.RequestDate = poLine.PromisedDate;
      copy7.TaxCategoryID = poLine.TaxCategoryID;
      copy7.TaskID = copyProjectDetails ? poLine.TaskID : new int?();
      copy7.CostCodeID = copyProjectDetails ? poLine.CostCodeID : new int?();
      copy7.IntercompanyPOLineNbr = poLine.LineNbr;
      PX.Objects.SO.SOLine copy8 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Update(copy7));
      copy8.TranDesc = poLine.TranDesc;
      copy8.UOM = poLine.UOM;
      copy8.ManualPrice = new bool?(true);
      PX.Objects.SO.SOLine copy9 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Update(copy8));
      copy9.OrderQty = poLine.OrderQty;
      copy9.CuryUnitPrice = poLine.CuryUnitCost;
      PX.Objects.SO.SOLine copy10 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Update(copy9));
      copy10.CuryExtPrice = poLine.CuryLineAmt;
      PX.Objects.SO.SOLine copy11 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Update(copy10));
      copy11.DiscPct = poLine.DiscPct;
      PX.Objects.SO.SOLine copy12 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Update(copy11));
      copy12.CuryDiscAmt = poLine.CuryDiscAmt;
      ((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Update(copy12);
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorDiscounts>())
      {
        foreach (POOrderDiscountDetail discountLine in discountLines)
        {
          SOOrderDiscountDetail orderDiscountDetail = new SOOrderDiscountDetail()
          {
            IsManual = new bool?(true)
          };
          SOOrderDiscountDetail copy13 = PXCache<SOOrderDiscountDetail>.CreateCopy(((PXSelectBase<SOOrderDiscountDetail>) instance.DiscountDetails).Insert(orderDiscountDetail));
          copy13.CuryDiscountAmt = discountLine.CuryDiscountAmt;
          copy13.Description = discountLine.Description;
          copy13.CuryDiscountableAmt = discountLine.CuryDiscountableAmt;
          copy13.DiscountableQty = discountLine.DiscountableQty;
          ((PXSelectBase<SOOrderDiscountDetail>) instance.DiscountDetails).Update(copy13);
        }
      }
      else
      {
        SOOrderDiscountDetail orderDiscountDetail = new SOOrderDiscountDetail()
        {
          IsManual = new bool?(true)
        };
        SOOrderDiscountDetail copy14 = PXCache<SOOrderDiscountDetail>.CreateCopy(((PXSelectBase<SOOrderDiscountDetail>) instance.DiscountDetails).Insert(orderDiscountDetail));
        copy14.CuryDiscountAmt = po.CuryDiscTot;
        ((PXSelectBase<SOOrderDiscountDetail>) instance.DiscountDetails).Update(copy14);
      }
    }
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current != null)
    {
      PX.Objects.SO.SOOrder copy15 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current);
      copy15.CuryControlTotal = copy15.CuryOrderTotal;
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy15);
    }
    PXGraph.RowPersistedEvents rowPersisted1 = ((PXGraph) instance).RowPersisted;
    Intercompany intercompany1 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted1 = new PXRowPersisted((object) intercompany1, __vmethodptr(intercompany1, UpdatePOOrderOnSOOrderRowPersisted));
    rowPersisted1.AddHandler<PX.Objects.SO.SOOrder>(pxRowPersisted1);
    UniquenessChecker<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrder.intercompanyPOType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.SO.SOOrder.intercompanyPONbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>, PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>.SameAsCurrent>> uniquenessChecker = new UniquenessChecker<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrder.intercompanyPOType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.SO.SOOrder.intercompanyPONbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>, PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>.SameAsCurrent>>((IBqlTable) po);
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
      PXRowPersisted pxRowPersisted2 = new PXRowPersisted((object) intercompany2, __vmethodptr(intercompany2, UpdatePOOrderOnSOOrderRowPersisted));
      rowPersisted2.RemoveHandler<PX.Objects.SO.SOOrder>(pxRowPersisted2);
      ((PXGraph) instance).OnBeforeCommit -= new Action<PXGraph>(uniquenessChecker.OnBeforeCommitImpl);
    }
    return ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current;
  }

  protected virtual void UpdatePOOrderOnSOOrderRowPersisted(
    PXCache sender,
    PXRowPersistedEventArgs e)
  {
    PX.Objects.SO.SOOrder row = (PX.Objects.SO.SOOrder) e.Row;
    if (string.IsNullOrEmpty(row?.IntercompanyPONbr) || e.Operation != 2 || e.TranStatus != null)
      return;
    PX.Objects.PO.POOrder poOrder1 = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXViewOf<PX.Objects.PO.POOrder>.BasedOn<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderType, Equal<BqlField<PX.Objects.SO.SOOrder.intercompanyPOType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.intercompanyPONbr, IBqlString>.FromCurrent>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) new PX.Objects.SO.SOOrder[1]
    {
      row
    }, Array.Empty<object>()));
    poOrder1.VendorRefNbr = row.OrderNbr;
    poOrder1.IsIntercompanySOCreated = new bool?(true);
    PX.Objects.PO.POOrder poOrder2 = (PX.Objects.PO.POOrder) sender.Graph.Caches[typeof (PX.Objects.PO.POOrder)].Update((object) poOrder1);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.PO.POOrder> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    bool? nullable1 = eventArgs.Row.IsIntercompany;
    if (!nullable1.GetValueOrDefault())
      return;
    using (new PXReadBranchRestrictedScope())
    {
      PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrder.intercompanyPOType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.SO.SOOrder.intercompanyPONbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>, PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>.SameAsCurrent>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POOrder[1]
      {
        eventArgs.Row
      }, Array.Empty<object>()));
      eventArgs.Row.IntercompanySOType = soOrder?.OrderType;
      eventArgs.Row.IntercompanySONbr = soOrder?.OrderNbr;
      PX.Objects.PO.POOrder row1 = eventArgs.Row;
      bool? nullable2;
      if (soOrder == null)
      {
        nullable1 = new bool?();
        nullable2 = nullable1;
      }
      else
        nullable2 = soOrder.Cancelled;
      row1.IntercompanySOCancelled = nullable2;
      PX.Objects.PO.POOrder row2 = eventArgs.Row;
      bool? nullable3;
      if (soOrder == null)
      {
        nullable1 = new bool?();
        nullable3 = nullable1;
      }
      else
        nullable3 = soOrder.IntercompanyPOWithEmptyInventory;
      row2.IntercompanySOWithEmptyInventory = nullable3;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) eventArgs).Cache, (object) eventArgs.Row).For<PX.Objects.PO.POOrder.intercompanySOType>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = eventArgs.Row.IsIntercompany.GetValueOrDefault();
      a.Enabled = false;
    }));
    chained = chained.SameFor<PX.Objects.PO.POOrder.intercompanySONbr>();
    chained.For<PX.Objects.PO.POOrder.excludeFromIntercompanyProc>((Action<PXUIFieldAttribute>) (a => a.Visible = eventArgs.Row.IsIntercompany.GetValueOrDefault()));
    bool? nullable = eventArgs.Row.IsIntercompany;
    if (nullable.GetValueOrDefault() && eventArgs.Row.IntercompanySONbr != null)
    {
      nullable = eventArgs.Row.IntercompanySOCancelled;
      if (nullable.GetValueOrDefault())
      {
        ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.PO.POOrder.intercompanySONbr>((object) eventArgs.Row, (object) eventArgs.Row.IntercompanySONbr, (Exception) new PXSetPropertyException("The related {0} sales order has been canceled.", (PXErrorLevel) 2, new object[1]
        {
          (object) eventArgs.Row.IntercompanySONbr
        }));
        goto label_8;
      }
    }
    nullable = eventArgs.Row.IsIntercompanySOCreated;
    if (nullable.GetValueOrDefault() && eventArgs.Row.IntercompanySONbr == null)
    {
      ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.PO.POOrder.intercompanySONbr>((object) eventArgs.Row, (object) eventArgs.Row.IntercompanySONbr, (Exception) new PXSetPropertyException("The related sales order has been deleted.", (PXErrorLevel) 2));
    }
    else
    {
      nullable = eventArgs.Row.IntercompanySOWithEmptyInventory;
      if (nullable.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.PO.POOrder.intercompanySONbr>((object) eventArgs.Row, (object) eventArgs.Row.IntercompanySONbr, (Exception) new PXSetPropertyException("The lines without inventory ID cannot be copied to a sales order. The sales order has been created without these lines.", (PXErrorLevel) 2));
    }
label_8:
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POOrder.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) eventArgs).Cache, (object) eventArgs.Row, eventArgs.Row.IntercompanySONbr == null);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderQty> e)
  {
    PX.Objects.PO.POLine row = e.Row;
    if (row == null || !((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IsIntercompany.GetValueOrDefault())
      return;
    if (PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXSelectReadonly<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Required<PX.Objects.SO.SOLine.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Required<PX.Objects.SO.SOLine.orderNbr>>, And<PX.Objects.SO.SOLine.intercompanyPOLineNbr, Equal<Required<PX.Objects.SO.SOLine.intercompanyPOLineNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IntercompanySOType,
      (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IntercompanySONbr,
      (object) row.LineNbr
    })) != null)
      throw new PXSetPropertyException("The quantity cannot be changed because the {0} intercompany sales order has been created for the line.", (PXErrorLevel) 4, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IntercompanySONbr
      });
  }

  protected virtual void POOrder_Cancelled_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    PXFieldVerifying baseFunc)
  {
    PX.Objects.PO.POOrder row = (PX.Objects.PO.POOrder) e.Row;
    if (row != null)
    {
      bool? nullable = row.IsIntercompany;
      if (nullable.GetValueOrDefault() && row.IntercompanySONbr != null)
      {
        nullable = (bool?) e.NewValue;
        if (nullable.GetValueOrDefault())
        {
          PX.Objects.SO.SOOrder soOrder;
          using (new PXReadBranchRestrictedScope())
            soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrder.intercompanyPOType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.SO.SOOrder.intercompanyPONbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>, PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>.SameAsCurrent>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
            {
              e.Row
            }, Array.Empty<object>()));
          if (soOrder != null)
          {
            int? shipmentCntr = soOrder.ShipmentCntr;
            int num = 0;
            if (!(shipmentCntr.GetValueOrDefault() == num & shipmentCntr.HasValue))
              throw new PXException("The purchase order cannot be canceled because a shipment has been prepared for the related {0} sales order.", new object[1]
              {
                (object) row.IntercompanySONbr
              });
          }
        }
      }
    }
    baseFunc.Invoke(sender, e);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.PO.POOrder> eventArgs)
  {
    if (eventArgs.Row.IsIntercompany.GetValueOrDefault() && !string.IsNullOrEmpty(eventArgs.Row.IntercompanySONbr))
      throw new PXException("The purchase order cannot be deleted because the {0} sales order has been generated for it.", new object[1]
      {
        (object) eventArgs.Row.IntercompanySONbr
      });
  }

  [PXOverride]
  public virtual void Persist(Intercompany.PersistDelegate baseMethod)
  {
    PX.Objects.PO.POOrder current1 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.IsIntercompany;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0 && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.IntercompanySONbr))
    {
      PX.Objects.PO.POOrder current2 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current;
      int num2;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.Cancelled;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      int num3;
      if (num2 != 0)
      {
        nullable = (bool?) ((PXSelectBase) this.Base.Document).Cache.GetValueOriginal<PX.Objects.PO.POOrder.cancelled>((object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
        num3 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num3 = 0;
      if (num3 != 0)
      {
        using (new PXReadBranchRestrictedScope())
        {
          PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrder.intercompanyPOType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.SO.SOOrder.intercompanyPONbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>, PX.Objects.PO.POOrder, PX.Objects.SO.SOOrder>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
          if (soOrder != null)
          {
            int? shipmentCntr = soOrder.ShipmentCntr;
            int num4 = 0;
            if (shipmentCntr.GetValueOrDefault() == num4 & shipmentCntr.HasValue)
            {
              nullable = soOrder.Cancelled;
              if (!nullable.GetValueOrDefault())
              {
                SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
                ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = soOrder;
                using (PXTransactionScope transactionScope = new PXTransactionScope())
                {
                  ((PXAction) instance.cancelOrder).Press();
                  baseMethod();
                  transactionScope.Complete();
                  return;
                }
              }
            }
          }
        }
      }
    }
    baseMethod();
  }

  public delegate void PersistDelegate();
}
