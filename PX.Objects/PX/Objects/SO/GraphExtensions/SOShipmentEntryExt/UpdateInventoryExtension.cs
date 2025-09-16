// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.UpdateInventoryExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.Models;
using PX.Objects.SO.Services;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class UpdateInventoryExtension : PXGraphExtension<SOShipmentEntry>
{
  public PXAction<PX.Objects.SO.SOShipment> updateIN;

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable UpdateIN(PXAdapter adapter, List<PX.Objects.SO.SOShipment> shipmentList = null)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UpdateInventoryExtension.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new UpdateInventoryExtension.\u003C\u003Ec__DisplayClass1_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.shipments = shipmentList ?? adapter.Get<PX.Objects.SO.SOShipment>().ToList<PX.Objects.SO.SOShipment>();
    bool massProcess = adapter.MassProcess;
    PXQuickProcess.ActionFlow quickProcessFlow = adapter.QuickProcessFlow;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.quickProcessFlow = quickProcessFlow;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.massProcess = massProcess;
    // ISSUE: reference to a compiler-generated field
    if (!((PXGraph) this.Base).UnattendedMode && UpdateInventoryExtension.NeedWarningShipNotInvoicedUpdateIN((PXGraph) this.Base, ((PXSelectBase<SOSetup>) this.Base.sosetup).Current, (IEnumerable<PX.Objects.SO.SOShipment>) cDisplayClass10.shipments, true) && ((PXSelectBase) this.Base.Document).View.Ask((object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current, "Confirmation", "The shipped-not-invoiced account is not used. On the subsequent invoice, revenue might be posted to a different financial period not matching the COGS period. Update IN?", (MessageButtons) 4, (MessageIcon) 2) != 6)
    {
      // ISSUE: reference to a compiler-generated field
      return (IEnumerable) cDisplayClass10.shipments;
    }
    ((PXAction) this.Base.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation<SOShipmentEntry>((PXGraphExtension<SOShipmentEntry>) this, new PXToggleAsyncDelegate((object) cDisplayClass10, __methodptr(\u003CUpdateIN\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass10.shipments;
  }

  public static bool NeedWarningShipNotInvoicedUpdateIN(
    PXGraph graph,
    SOSetup setup,
    IEnumerable<PX.Objects.SO.SOShipment> shipments,
    bool validateEachShipmentLine = false)
  {
    if (!setup.UseShipDateForInvoiceDate.GetValueOrDefault())
    {
      bool flag = false;
      if (validateEachShipmentLine)
      {
        foreach (PX.Objects.SO.SOShipment shipment in shipments)
        {
          if (PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrderType>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderType.orderType, Equal<SOShipLine.origOrderType>>>>, And<BqlOperand<PX.Objects.SO.SOOrderType.aRDocType, IBqlString>.IsNotEqual<ARDocType.noUpdate>>>, And<BqlOperand<PX.Objects.SO.SOOrderType.requireShipping, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.SO.SOOrderType.useShippedNotInvoiced, IBqlBool>.IsEqual<False>>>>>.Where<BqlOperand<SOShipLine.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
          {
            (object) shipment.ShipmentNbr
          })) != null)
          {
            flag = true;
            break;
          }
        }
      }
      else if (PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderType, PXViewOf<PX.Objects.SO.SOOrderType>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrderType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrderType.active, Equal<True>>>>, And<BqlOperand<PX.Objects.SO.SOOrderType.aRDocType, IBqlString>.IsNotEqual<ARDocType.noUpdate>>>, And<BqlOperand<PX.Objects.SO.SOOrderType.requireShipping, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.SO.SOOrderType.useShippedNotInvoiced, IBqlBool>.IsEqual<False>>>>.Config>.SelectSingleBound(graph, (object[]) null, Array.Empty<object>())) != null)
        flag = true;
      if (flag)
        return shipments.Any<PX.Objects.SO.SOShipment>((Func<PX.Objects.SO.SOShipment, bool>) (shipment => shipment.Status == "F" && !IsNotBillable(shipment)));
    }
    return false;

    static bool IsNotBillable(PX.Objects.SO.SOShipment shipment)
    {
      if (shipment.Confirmed.GetValueOrDefault())
      {
        int? nullable = shipment.UnbilledOrderCntr;
        int num1 = 0;
        if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        {
          nullable = shipment.BilledOrderCntr;
          int num2 = 0;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          {
            nullable = shipment.ReleasedOrderCntr;
            int num3 = 0;
            return nullable.GetValueOrDefault() == num3 & nullable.HasValue;
          }
        }
      }
      return false;
    }
  }

  public virtual void PostShipment(
    INRegisterEntryFactory factory,
    PX.Objects.SO.SOShipment shiporder,
    DocumentList<PX.Objects.IN.INRegister> list)
  {
    ((PXGraph) this.Base).Clear();
    INRegisterEntryBase inRegisterEntry = factory.GetOrCreateINRegisterEntry(shiporder);
    ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) shiporder.ShipmentNbr, Array.Empty<object>()));
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: field reference
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<SOShipmentEntry, PX.Objects.SO.SOShipment>(this.Base, Expression.Lambda<Func<SOShipmentEntry, PXAction<PX.Objects.SO.SOShipment>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (UpdateInventoryExtension.updateIN))), parameterExpression), ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.updateIN).GetCaption(),
        (object) ((PXSelectBase) this.Base.Document).Cache.GetRowDescription((object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current)
      });
    ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.Status = shiporder.Status;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current, true);
    ((PXSelectBase) this.Base.Document).Cache.IsDirty = true;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.SetSuppressWorkflowOnUpdateIN();
      foreach (PostShipmentArgs postShipmentArg in this.GetPostShipmentArgs(shiporder, inRegisterEntry, list))
        this.PostShipment(postShipmentArg);
      transactionScope.Complete();
    }
  }

  public virtual void PostShipment(PostShipmentArgs args)
  {
    INRegisterEntryBase inRegisterEntry = args.INRegisterEntry;
    DocumentList<PX.Objects.IN.INRegister> documents = args.Documents;
    PX.Objects.AR.ARInvoice invoice = args.Invoice;
    try
    {
      List<INItemPlan> reattachedPlans = new List<INItemPlan>();
      using (inRegisterEntry.TranSplitPlanExt.ReleaseModeScope())
      {
        PXGraph.RowPersistedEvents rowPersisted = ((PXGraph) inRegisterEntry).RowPersisted;
        UpdateInventoryExtension inventoryExtension = this;
        // ISSUE: virtual method pointer
        PXRowPersisted pxRowPersisted = new PXRowPersisted((object) inventoryExtension, __vmethodptr(inventoryExtension, ShipmentINTranRowPersisted));
        rowPersisted.AddHandler<INTran>(pxRowPersisted);
        PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.IN.INSite.siteID, Equal<Current<PX.Objects.SO.SOShipment.siteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, Array.Empty<object>()));
        if (!((PXSelectBase) this.Base.Document).Cache.IsDirty)
        {
          ((PXGraph) this.Base).Clear();
          ((PXGraph) inRegisterEntry).Clear();
          ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) args.ShipmentNbr, Array.Empty<object>()));
        }
        ((PXSelectBase<INSetup>) inRegisterEntry.insetup).Current.HoldEntry = new bool?(false);
        ((PXSelectBase<INSetup>) inRegisterEntry.insetup).Current.RequireControlTotal = new bool?(false);
        bool flag1 = false;
        PX.Objects.IN.INRegister inRegister = documents.Find<PX.Objects.IN.INRegister.srcDocType, PX.Objects.IN.INRegister.srcRefNbr>((object) args.ShipmentType, (object) args.ShipmentNbr) ?? new PX.Objects.IN.INRegister();
        if (inRegister.RefNbr != null)
        {
          inRegisterEntry.INRegisterDataMember.Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXSelect<PX.Objects.IN.INRegister>.Config>.Search<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>((PXGraph) inRegisterEntry, (object) inRegister.DocType, (object) inRegister.RefNbr, Array.Empty<object>()));
          if (inRegisterEntry.INRegisterDataMember.Current != null && inRegisterEntry.INRegisterDataMember.Current.SrcRefNbr == null)
          {
            inRegisterEntry.INRegisterDataMember.Current.SrcDocType = args.ShipmentType;
            inRegisterEntry.INRegisterDataMember.Current.SrcRefNbr = args.ShipmentNbr;
          }
        }
        else
        {
          inRegister.BranchID = args.ShipmentType == "T" ? branch.BranchID : (int?) invoice?.BranchID ?? args.DefaultBranchID;
          inRegister.DocType = args.ShipmentType;
          inRegister.SiteID = args.SiteID;
          inRegister.ToSiteID = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.DestinationSiteID;
          if (inRegister.DocType == "T")
            inRegister.TransferType = "2";
          if (invoice == null)
          {
            inRegister.TranDate = args.ShipDate;
          }
          else
          {
            inRegister.TranDate = invoice.DocDate;
            inRegister.FinPeriodID = invoice.FinPeriodID;
          }
          inRegister.OrigModule = "SO";
          inRegister.SrcDocType = args.ShipmentType;
          inRegister.SrcRefNbr = args.ShipmentNbr;
          flag1 = true;
        }
        SOShipLine soShipLine = (SOShipLine) null;
        INTran inTran1 = (INTran) null;
        Dictionary<long?, List<INItemPlan>> dictionary = new Dictionary<long?, List<INItemPlan>>();
        (BqlCommand, object[]) commandAndParameters1 = this.GetShipLinesForDemandCommandAndParameters(args);
        foreach (PXResult<SOShipLine> pxResult in new PXView((PXGraph) this.Base, false, commandAndParameters1.Item1).SelectMulti(commandAndParameters1.Item2))
        {
          SOShipLineSplit soShipLineSplit = PXResult.Unwrap<SOShipLineSplit>((object) pxResult);
          INItemPlan inItemPlan = PXResult.Unwrap<INItemPlan>((object) pxResult);
          EnumerableEx.Ensure<long?, List<INItemPlan>>((IDictionary<long?, List<INItemPlan>>) dictionary, soShipLineSplit.PlanID, (Func<List<INItemPlan>>) (() => new List<INItemPlan>())).Add(inItemPlan);
        }
        (BqlCommand, object[]) commandAndParameters2 = this.GetShipLinesToPostShipmentCommandAndParameters(args);
        foreach (PXResult<SOShipLine> pxResult in new PXView((PXGraph) this.Base, false, commandAndParameters2.Item1).SelectMulti(commandAndParameters2.Item2))
        {
          SOShipLine line = PXResult<SOShipLine>.op_Implicit(pxResult);
          SOShipLineSplit soShipLineSplit1 = PXResult.Unwrap<SOShipLineSplit>((object) pxResult);
          INItemPlan inItemPlan = PXResult.Unwrap<INItemPlan>((object) pxResult);
          INPlanType inPlanType1 = INPlanType.PK.Find((PXGraph) this.Base, inItemPlan.PlanType);
          if ((object) inPlanType1 == null)
            inPlanType1 = new INPlanType();
          INPlanType inPlanType2 = inPlanType1;
          PX.Objects.AR.ARTran arTran = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) pxResult);
          SOShipLineSplit copy1 = PXCache<SOShipLineSplit>.CreateCopy(soShipLineSplit1);
          if (args.ShipmentNbr != "<NEW>" && args.ShipmentType != "H" && !args.Confirmed.GetValueOrDefault() || !line.Confirmed.GetValueOrDefault() || line.IsUnassigned.GetValueOrDefault() || soShipLineSplit1.LineType == "GI" && soShipLineSplit1.IsStockItem.GetValueOrDefault() && !soShipLineSplit1.Confirmed.GetValueOrDefault())
            throw new PXException("The system cannot process the unconfirmed shipment {0}.", new object[1]
            {
              (object) args.ShipmentNbr
            });
          long? nullable1 = inItemPlan.PlanID;
          if (nullable1.HasValue)
            ((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) this.Base)).SetStatus((object) inItemPlan, (PXEntryStatus) 0);
          Decimal? nullable2;
          int num1;
          if (soShipLineSplit1.IsStockItem.GetValueOrDefault())
          {
            nullable2 = soShipLineSplit1.Qty;
            Decimal num2 = 0M;
            num1 = !(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue) ? 1 : 0;
          }
          else
            num1 = 0;
          bool flag2 = num1 != 0;
          bool flag3 = false;
          if (inPlanType2.DeleteOnEvent.GetValueOrDefault() || !flag2)
          {
            if (!flag2)
              GraphHelper.Caches<INItemPlan>((PXGraph) this.Base).Delete(inItemPlan);
            else
              flag3 = true;
            GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<SOShipLineSplit>((PXGraph) this.Base), (object) soShipLineSplit1, true);
            soShipLineSplit1 = GraphHelper.Caches<SOShipLineSplit>((PXGraph) this.Base).Locate(soShipLineSplit1);
            if (soShipLineSplit1 != null)
            {
              SOShipLineSplit soShipLineSplit2 = soShipLineSplit1;
              nullable1 = new long?();
              long? nullable3 = nullable1;
              soShipLineSplit2.PlanID = nullable3;
              soShipLineSplit1.Released = new bool?(true);
            }
            ((PXCache) GraphHelper.Caches<SOShipLineSplit>((PXGraph) this.Base)).IsDirty = true;
            if (!flag2)
              continue;
          }
          else if (!string.IsNullOrEmpty(inPlanType2.ReplanOnEvent))
          {
            inItemPlan = PXCache<INItemPlan>.CreateCopy(inItemPlan);
            inItemPlan.PlanType = inPlanType2.ReplanOnEvent;
            GraphHelper.Caches<INItemPlan>((PXGraph) this.Base).Update(inItemPlan);
            GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<SOShipLineSplit>((PXGraph) this.Base), (object) soShipLineSplit1, true);
            ((PXCache) GraphHelper.Caches<SOShipLineSplit>((PXGraph) this.Base)).IsDirty = true;
          }
          int? nullable4;
          int? nullable5;
          if (soShipLineSplit1.IsStockItem.GetValueOrDefault())
          {
            if (GraphHelper.Caches<SOShipLine>((PXGraph) this.Base).ObjectsEqual(soShipLine, line))
            {
              nullable4 = line.InventoryID;
              nullable5 = soShipLineSplit1.InventoryID;
              if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
              {
                nullable5 = line.TaskID;
                if (nullable5.HasValue)
                {
                  nullable5 = line.LocationID;
                  nullable4 = soShipLineSplit1.LocationID;
                  if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
                    goto label_50;
                }
                else
                  goto label_50;
              }
            }
            if (flag1)
            {
              inRegisterEntry.INRegisterDataMember.Insert(inRegister);
              flag1 = false;
            }
            line.Released = new bool?(true);
            GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<SOShipLine>((PXGraph) this.Base), (object) line, true);
            ((PXCache) GraphHelper.Caches<SOShipLine>((PXGraph) this.Base)).IsDirty = true;
            inTran1 = new INTran()
            {
              DocType = inRegister.DocType
            };
            this.SetINTranFromShipLine(inTran1, args, branch, pxResult);
            inRegisterEntry.CostCenterDispatcherExt?.SetInventorySource(inTran1);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            PXFieldDefaulting pxFieldDefaulting = UpdateInventoryExtension.\u003C\u003Ec.\u003C\u003E9__4_1 ?? (UpdateInventoryExtension.\u003C\u003Ec.\u003C\u003E9__4_1 = new PXFieldDefaulting((object) UpdateInventoryExtension.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CPostShipment\u003Eb__4_1)));
            ((PXGraph) inRegisterEntry).FieldDefaulting.AddHandler<INTran.locationID>(pxFieldDefaulting);
            try
            {
              inTran1 = inRegisterEntry.LSSelectDataMember.Insert(inTran1);
            }
            finally
            {
              ((PXGraph) inRegisterEntry).FieldDefaulting.RemoveHandler<INTran.locationID>(pxFieldDefaulting);
            }
          }
label_50:
          soShipLine = line;
          INTranSplit inTranSplit1 = INTranSplit.FromINTran(inTran1);
          INTranSplit inTranSplit2 = inTranSplit1;
          nullable4 = new int?();
          int? nullable6 = nullable4;
          inTranSplit2.SplitLineNbr = nullable6;
          inTranSplit1.SubItemID = soShipLineSplit1.SubItemID;
          inTranSplit1.LocationID = soShipLineSplit1.LocationID;
          inTranSplit1.LotSerialNbr = soShipLineSplit1.LotSerialNbr;
          inTranSplit1.ExpireDate = soShipLineSplit1.ExpireDate;
          inTranSplit1.UOM = soShipLineSplit1.UOM;
          inTranSplit1.Qty = soShipLineSplit1.Qty;
          INTranSplit inTranSplit3 = inTranSplit1;
          nullable2 = new Decimal?();
          Decimal? nullable7 = nullable2;
          inTranSplit3.BaseQty = nullable7;
          if (line.ShipmentType == "T")
            inTranSplit1.TransferType = "2";
          if (flag3)
          {
            inTranSplit1.PlanID = inItemPlan.PlanID;
            reattachedPlans.Add(inItemPlan);
          }
          PXParentAttribute.SetParent(((PXSelectBase) inRegisterEntry.INTranSplitDataMember).Cache, (object) inTranSplit1, typeof (INTran), (object) inTran1);
          INTranSplit newsplit = inRegisterEntry.INTranSplitDataMember.Insert(inTranSplit1);
          nullable1 = copy1.PlanID;
          List<INItemPlan> demandPlans;
          if (nullable1.HasValue && dictionary.TryGetValue(copy1.PlanID, out demandPlans))
            this.SplitDemandAndAssignSupply(args, line, copy1, newsplit, demandPlans);
          nullable4 = line.InventoryID;
          nullable5 = soShipLineSplit1.InventoryID;
          if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
          {
            INTran copy2 = PXCache<INTran>.CreateCopy(inTran1);
            PXCache cache = ((PXSelectBase) inRegisterEntry.LSSelectDataMember).Cache;
            INTran inTran2 = inTran1;
            nullable2 = inTran1.Qty;
            Decimal? nullable8 = inTran1.UnitCost;
            Decimal? nullable9;
            Decimal? nullable10;
            if (!(nullable2.HasValue & nullable8.HasValue))
            {
              nullable9 = new Decimal?();
              nullable10 = nullable9;
            }
            else
              nullable10 = new Decimal?(nullable2.GetValueOrDefault() * nullable8.GetValueOrDefault());
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) nullable10;
            cache.SetValueExt<INTran.tranCost>((object) inTran2, (object) local);
            bool flag4 = string.Equals(inTran1.UOM, arTran.UOM, StringComparison.OrdinalIgnoreCase);
            bool flag5 = arTran.DrCr == "C" && arTran.SOOrderLineOperation == "R" || arTran.DrCr == "D" && arTran.SOOrderLineOperation == "I";
            INTran inTran3 = inTran1;
            Decimal? nullable11;
            if (!flag5)
            {
              nullable11 = arTran.TranAmt;
            }
            else
            {
              nullable8 = arTran.TranAmt;
              if (!nullable8.HasValue)
              {
                nullable2 = new Decimal?();
                nullable11 = nullable2;
              }
              else
                nullable11 = new Decimal?(-nullable8.GetValueOrDefault());
            }
            nullable8 = nullable11;
            Decimal? nullable12 = new Decimal?(nullable8.GetValueOrDefault());
            inTran3.TranAmt = nullable12;
            nullable8 = flag4 ? arTran.Qty : arTran.BaseQty;
            if (nullable8.GetValueOrDefault() != 0M)
            {
              nullable5 = arTran.SOShipmentLineNbr;
              if (nullable5.HasValue)
              {
                nullable5 = arTran.TaskID;
                if (!nullable5.HasValue)
                  goto label_75;
              }
              nullable8 = inTran1.TranAmt;
              Decimal? nullable13;
              Decimal? nullable14;
              if (!flag4)
              {
                nullable9 = inTran1.BaseQty;
                nullable13 = arTran.BaseQty;
                nullable14 = nullable9.HasValue & nullable13.HasValue ? new Decimal?(nullable9.GetValueOrDefault() / nullable13.GetValueOrDefault()) : new Decimal?();
              }
              else
              {
                nullable13 = inTran1.Qty;
                nullable9 = arTran.Qty;
                nullable14 = nullable13.HasValue & nullable9.HasValue ? new Decimal?(nullable13.GetValueOrDefault() / nullable9.GetValueOrDefault()) : new Decimal?();
              }
              nullable2 = nullable14;
              Decimal? nullable15;
              if (!(nullable8.HasValue & nullable2.HasValue))
              {
                nullable9 = new Decimal?();
                nullable15 = nullable9;
              }
              else
                nullable15 = new Decimal?(nullable8.GetValueOrDefault() * nullable2.GetValueOrDefault());
              object obj = (object) nullable15;
              ((PXSelectBase) inRegisterEntry.LSSelectDataMember).Cache.RaiseFieldUpdating<INTran.tranAmt>((object) inTran1, ref obj);
              inTran1.TranAmt = (Decimal?) obj;
            }
label_75:
            ((PXSelectBase) inRegisterEntry.LSSelectDataMember).Cache.RaiseRowUpdated((object) inTran1, (object) copy2);
          }
        }
      }
      if (((PXSelectBase) inRegisterEntry.LSSelectDataMember).Cache.IsDirty)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          ((PXAction) inRegisterEntry.Save).Press();
          ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.Released = new bool?(this.ShouldReleaseShipmentOnPost(args, reattachedPlans));
          this.UpdateStatusOnPostShipment(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
          GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current, true);
          ((PXAction) this.Base.Save).Press();
          PX.Objects.IN.INRegister inRegister;
          if ((inRegister = documents.Find((object) inRegisterEntry.INRegisterDataMember.Current)) == null)
            documents.Add(inRegisterEntry.INRegisterDataMember.Current);
          else
            ((PXSelectBase) inRegisterEntry.INRegisterDataMember).Cache.RestoreCopy((object) inRegister, (object) inRegisterEntry.INRegisterDataMember.Current);
          transactionScope.Complete();
        }
      }
      else
      {
        PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current;
        int? nullable = current.BilledOrderCntr;
        int num3 = 0;
        int num4;
        if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
        {
          nullable = current.UnbilledOrderCntr;
          int num5 = 0;
          if (nullable.GetValueOrDefault() == num5 & nullable.HasValue)
          {
            nullable = current.ReleasedOrderCntr;
            int num6 = 0;
            num4 = nullable.GetValueOrDefault() == num6 & nullable.HasValue ? 1 : 0;
            goto label_95;
          }
        }
        num4 = 0;
label_95:
        if (num4 == 0 || !this.ShouldReleaseTransferOnPost(args, current))
          return;
        current.Released = new bool?(true);
        this.UpdateStatusOnPostShipment(current);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) current);
        ((PXAction) this.Base.Save).Press();
      }
    }
    finally
    {
      PXGraph.RowPersistedEvents rowPersisted = ((PXGraph) inRegisterEntry).RowPersisted;
      UpdateInventoryExtension inventoryExtension = this;
      // ISSUE: virtual method pointer
      PXRowPersisted pxRowPersisted = new PXRowPersisted((object) inventoryExtension, __vmethodptr(inventoryExtension, ShipmentINTranRowPersisted));
      rowPersisted.RemoveHandler<INTran>(pxRowPersisted);
    }
  }

  public virtual void ShipmentINTranRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    INTran row = e.Row as INTran;
    if (e.Operation != 2 || e.TranStatus != null || row == null)
      return;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<INTran>(new PXDataField[6]
    {
      (PXDataField) new PXDataField<INTran.refNbr>(),
      (PXDataField) new PXDataFieldValue<INTran.sOShipmentType>((object) row.SOShipmentType),
      (PXDataField) new PXDataFieldValue<INTran.sOShipmentNbr>((object) row.SOShipmentNbr),
      (PXDataField) new PXDataFieldValue<INTran.sOShipmentLineNbr>((object) row.SOShipmentLineNbr),
      (PXDataField) new PXDataFieldValue<INTran.docType>((object) row.DocType),
      (PXDataField) new PXDataFieldValue<INTran.refNbr>((object) row.RefNbr, (PXComp) 1)
    }))
    {
      if (pxDataRecord != null)
        throw new PXException("Another process has added the '{0}' record. {1}", new object[2]
        {
          (object) sender.DisplayName,
          (object) "Your changes will be lost."
        });
    }
  }

  /// <summary>Returns command and parameters for it that returns <see cref="T:PX.Data.PXResultset`3" /> of SOShipLine, SOShipLineSplit, INItemPlan for arguments provided.</summary>
  protected virtual (BqlCommand, object[]) GetShipLinesForDemandCommandAndParameters(
    PostShipmentArgs args)
  {
    return (BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Select2<SOShipLine, InnerJoin<SOShipLineSplit, On<SOShipLineSplit.shipmentNbr, Equal<SOShipLine.shipmentNbr>, And<SOShipLineSplit.lineNbr, Equal<SOShipLine.lineNbr>>>, InnerJoin<INItemPlan, On<INItemPlan.supplyPlanID, Equal<SOShipLineSplit.planID>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.shipmentType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<SOShipLine.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>>)
    }), (object[]) new string[2]
    {
      args.ShipmentType,
      args.ShipmentNbr
    });
  }

  protected virtual (BqlCommand, object[]) GetShipLinesToPostShipmentCommandAndParameters(
    PostShipmentArgs args)
  {
    return (BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Select2<SOShipLine, InnerJoin<SOShipLineSplit, On<SOShipLineSplit.shipmentNbr, Equal<SOShipLine.shipmentNbr>, And<SOShipLineSplit.lineNbr, Equal<SOShipLine.lineNbr>>>, LeftJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.sOShipmentNbr, Equal<SOShipLine.shipmentNbr>, And<PX.Objects.AR.ARTran.sOShipmentType, NotEqual<INDocType.dropShip>, And<PX.Objects.AR.ARTran.lineType, Equal<SOShipLine.lineType>, And<PX.Objects.AR.ARTran.sOOrderType, Equal<SOShipLine.origOrderType>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<SOShipLine.origOrderNbr>, And<PX.Objects.AR.ARTran.sOOrderLineNbr, Equal<SOShipLine.origLineNbr>, And<PX.Objects.AR.ARTran.sOShipmentLineGroupNbr, Equal<SOShipLine.invoiceGroupNbr>, And<PX.Objects.AR.ARTran.canceled, Equal<False>, And<PX.Objects.AR.ARTran.isCancellation, Equal<False>>>>>>>>>>, LeftJoin<INTran, On<INTran.sOShipmentNbr, Equal<SOShipLine.shipmentNbr>, And<INTran.sOShipmentType, NotEqual<INDocType.dropShip>, And<INTran.sOShipmentLineNbr, Equal<SOShipLine.lineNbr>>>>, LeftJoin<INItemPlan, On<INItemPlan.planID, Equal<SOShipLineSplit.planID>>>>>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.shipmentType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<SOShipLine.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INTran.refNbr, IBqlString>.IsNull>>, OrderBy<Asc<SOShipLine.shipmentNbr, Asc<SOShipLine.lineNbr>>>>)
    }), (object[]) new string[2]
    {
      args.ShipmentType,
      args.ShipmentNbr
    });
  }

  protected virtual void SetINTranFromShipLine(
    INTran newline,
    PostShipmentArgs args,
    PX.Objects.GL.Branch branch,
    PXResult<SOShipLine> pxResult)
  {
    SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult);
    SOShipLineSplit soShipLineSplit = PXResult.Unwrap<SOShipLineSplit>((object) pxResult);
    PX.Objects.AR.ARTran arTran = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) pxResult);
    newline.TranType = soShipLine.TranType;
    newline.SOShipmentNbr = soShipLine.ShipmentNbr;
    newline.SOShipmentType = soShipLine.ShipmentType;
    newline.SOShipmentLineNbr = soShipLine.LineNbr;
    newline.SOOrderType = soShipLine.OrigOrderType;
    newline.SOOrderNbr = soShipLine.OrigOrderNbr;
    newline.SOOrderLineNbr = soShipLine.OrigLineNbr;
    newline.SOLineType = soShipLine.LineType;
    newline.ARDocType = arTran.TranType;
    newline.ARRefNbr = arTran.RefNbr;
    newline.ARLineNbr = arTran.LineNbr;
    newline.BAccountID = soShipLine.CustomerID;
    newline.BranchID = args.ShipmentType == "T" ? branch.BranchID : arTran.BranchID;
    newline.UpdateShippedNotInvoiced = new bool?(false);
    newline.IsSpecialOrder = soShipLine.IsSpecialOrder;
    newline.ProjectID = soShipLine.ProjectID;
    newline.TaskID = soShipLine.TaskID;
    newline.CostCodeID = soShipLine.CostCodeID;
    newline.IsStockItem = soShipLineSplit.IsStockItem;
    newline.InventoryID = soShipLineSplit.InventoryID;
    newline.SiteID = soShipLine.SiteID;
    newline.ToSiteID = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.DestinationSiteID;
    newline.InvtMult = soShipLine.InvtMult;
    newline.IsIntercompany = soShipLine.IsIntercompany;
    newline.Qty = new Decimal?(0M);
    int? inventoryId1 = soShipLine.InventoryID;
    int? inventoryId2 = soShipLineSplit.InventoryID;
    if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
    {
      newline.IsComponentItem = soShipLineSplit.IsComponentItem;
      newline.SubItemID = soShipLineSplit.SubItemID;
      newline.UOM = soShipLineSplit.UOM;
      newline.UnitPrice = new Decimal?(0M);
      newline.UnitCost = new Decimal?(0M);
      newline.TranDesc = (string) null;
    }
    else
    {
      newline.SubItemID = soShipLine.SubItemID;
      newline.UOM = soShipLine.UOM;
      newline.UnitPrice = new Decimal?(INUnitAttribute.ConvertFromTo<INTran.inventoryID>(((PXSelectBase) args.INRegisterEntry.LSSelectDataMember).Cache, (object) newline, newline.UOM, arTran.UOM, arTran.UnitPrice.GetValueOrDefault(), INPrecision.UNITCOST));
      newline.UnitCost = soShipLine.UnitCost;
      newline.TranDesc = soShipLine.TranDesc;
      newline.ReasonCode = soShipLine.ReasonCode;
      newline.OrigUOM = soShipLine.UOM;
      newline.OrigFullQty = soShipLine.ShippedQty;
      newline.BaseOrigFullQty = soShipLine.BaseShippedQty;
    }
  }

  protected virtual void SplitDemandAndAssignSupply(
    PostShipmentArgs args,
    SOShipLine line,
    SOShipLineSplit splitcopy,
    INTranSplit newsplit,
    List<INItemPlan> demandPlans)
  {
    Decimal? restShipQty = line.BaseShippedQty;
    foreach (INItemPlan demandPlan in demandPlans)
    {
      Decimal? nullable1 = restShipQty;
      Decimal? nullable2 = demandPlan.PlanQty;
      if (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      {
        demandPlan.SupplyPlanID = newsplit.PlanID;
        GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) args.INRegisterEntry), (object) demandPlan, true);
        nullable2 = restShipQty;
        nullable1 = demandPlan.PlanQty;
        restShipQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        nullable1 = restShipQty;
        nullable2 = this.SplitDemandAndAssignSupply(args, line, demandPlan, newsplit, restShipQty);
        restShipQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
    }
  }

  protected virtual void UpdateStatusOnPostShipment(PX.Objects.SO.SOShipment shipment)
  {
    int? unbilledOrderCntr = shipment.UnbilledOrderCntr;
    int num1 = 0;
    if (!(unbilledOrderCntr.GetValueOrDefault() == num1 & unbilledOrderCntr.HasValue))
      return;
    int? billedOrderCntr = shipment.BilledOrderCntr;
    int num2 = 0;
    if (!(billedOrderCntr.GetValueOrDefault() == num2 & billedOrderCntr.HasValue))
      return;
    int? releasedOrderCntr = shipment.ReleasedOrderCntr;
    int num3 = 0;
    if (!(releasedOrderCntr.GetValueOrDefault() == num3 & releasedOrderCntr.HasValue) || !shipment.Released.GetValueOrDefault())
      return;
    shipment.Status = "C";
  }

  protected virtual IEnumerable<PostShipmentArgs> GetPostShipmentArgs(
    PX.Objects.SO.SOShipment shiporder,
    INRegisterEntryBase docgraph,
    DocumentList<PX.Objects.IN.INRegister> list)
  {
    return Enumerable.Empty<PostShipmentArgs>();
  }

  protected virtual bool ShouldReleaseShipmentOnPost(
    PostShipmentArgs args,
    List<INItemPlan> reattachedPlans)
  {
    return true;
  }

  protected virtual bool ShouldReleaseTransferOnPost(PostShipmentArgs args, PX.Objects.SO.SOShipment shipment)
  {
    return false;
  }

  protected virtual Decimal? SplitDemandAndAssignSupply(
    PostShipmentArgs args,
    SOShipLine line,
    INItemPlan demandPlan,
    INTranSplit newsplit,
    Decimal? restShipQty)
  {
    return new Decimal?(0M);
  }

  [PXInternalUseOnly]
  protected virtual void SetSuppressWorkflowOnUpdateIN()
  {
    PXTransactionScope.SetSuppressWorkflow(true);
  }
}
