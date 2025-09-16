// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.UpdateInventorySOExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class UpdateInventorySOExtension : 
  PXGraphExtension<UpdateInventoryExtension, SOOrderExtension, SOShipmentEntry>
{
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.UpdateInventoryExtension.GetPostShipmentArgs(PX.Objects.SO.SOShipment,PX.Objects.IN.INRegisterEntryBase,PX.Objects.CS.DocumentList{PX.Objects.IN.INRegister})" />
  /// .
  [PXOverride]
  public IEnumerable<PostShipmentArgs> GetPostShipmentArgs(
    PX.Objects.SO.SOShipment shiporder,
    INRegisterEntryBase docgraph,
    DocumentList<PX.Objects.IN.INRegister> list,
    Func<PX.Objects.SO.SOShipment, INRegisterEntryBase, DocumentList<PX.Objects.IN.INRegister>, IEnumerable<PostShipmentArgs>> base_GetPostShipmentArgs)
  {
    return this.GetPostShipmentArgsInternal(shiporder, docgraph, list);
  }

  [PXOverride]
  public (BqlCommand, object[]) GetShipLinesForDemandCommandAndParameters(
    PostShipmentArgs args,
    Func<PostShipmentArgs, (BqlCommand, object[])> base_GetShipLinesForDemandCommand)
  {
    (BqlCommand bqlCommand, object[] collection) = base_GetShipLinesForDemandCommand(args);
    return (bqlCommand.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.origOrderType, Equal<P.AsString>>>>>.And<BqlOperand<SOShipLine.origOrderNbr, IBqlString>.IsEqual<P.AsString>>>>(), new List<object>((IEnumerable<object>) collection)
    {
      (object) args.SourceDocType,
      (object) args.SourceDocNbr
    }.ToArray());
  }

  [PXOverride]
  public (BqlCommand, object[]) GetShipLinesToPostShipmentCommandAndParameters(
    PostShipmentArgs args,
    Func<PostShipmentArgs, (BqlCommand, object[])> base_GetShipLinesToPostShipmentCommand)
  {
    (BqlCommand bqlCommand, object[] collection) = base_GetShipLinesToPostShipmentCommand(args);
    return (BqlCommand.AppendJoin<LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<SOShipLine.origOrderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<SOShipLine.origOrderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<SOShipLine.origLineNbr>>>>>>(bqlCommand).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.origOrderType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<SOShipLine.origOrderNbr, IBqlString>.IsEqual<P.AsString>>>>(), new List<object>((IEnumerable<object>) collection)
    {
      (object) args.SourceDocType,
      (object) args.SourceDocNbr
    }.ToArray());
  }

  [PXOverride]
  public void SetINTranFromShipLine(
    INTran newline,
    PostShipmentArgs args,
    PX.Objects.GL.Branch branch,
    PXResult<SOShipLine> pxResult,
    Action<INTran, PostShipmentArgs, PX.Objects.GL.Branch, PXResult<SOShipLine>> base_SetINTranFromShipLine)
  {
    base_SetINTranFromShipLine(newline, args, branch, pxResult);
    SOShipLine line = PXResult<SOShipLine>.op_Implicit(pxResult);
    PX.Objects.AR.ARTran arTran = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) pxResult);
    SOShipLineSplit shipLineSplit = PXResult.Unwrap<SOShipLineSplit>((object) pxResult);
    PX.Objects.SO.SOLine soline = PXResult.Unwrap<PX.Objects.SO.SOLine>((object) pxResult);
    PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, args.SourceDocType);
    bool flag1 = arTran != null && arTran.Released.GetValueOrDefault();
    newline.BranchID = args.ShipmentType == "T" ? branch.BranchID : (flag1 ? arTran.BranchID : soline.BranchID);
    bool flag2 = !flag1 && line.OrigOrderNbr != null && line.ShipmentNbr != null && soOrderType != null && soOrderType.UseShippedNotInvoiced.GetValueOrDefault();
    newline.UpdateShippedNotInvoiced = new bool?(flag2);
    if (flag2)
    {
      newline.COGSAcctID = soOrderType.ShippedNotInvoicedAcctID;
      newline.COGSSubID = soOrderType.ShippedNotInvoicedSubID;
    }
    Lazy<bool?> lazy = new Lazy<bool?>((Func<bool?>) (() => INPostClass.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipLineSplit.InventoryID)?.PostClassID)?.COGSSubFromSales));
    int? nullable1;
    if (soOrderType.Behavior != "TR" && (soOrderType.ARDocType != "UND" || lazy.Value.GetValueOrDefault()))
    {
      INTran inTran1 = newline;
      nullable1 = arTran.AccountID;
      int? nullable2 = nullable1 ?? soline.SalesAcctID;
      inTran1.AcctID = nullable2;
      INTran inTran2 = newline;
      nullable1 = arTran.SubID;
      int? nullable3 = nullable1 ?? soline.SalesSubID;
      inTran2.SubID = nullable3;
      nullable1 = newline.AcctID;
      nullable1 = nullable1.HasValue ? newline.SubID : throw new PXException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.SO.SOLine.salesAcctID>((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base))
      });
      if (!nullable1.HasValue)
        throw new PXException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.SO.SOLine.salesSubID>((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base))
        });
    }
    newline.InventorySource = soline.InventorySource;
    nullable1 = line.InventoryID;
    int? inventoryId = shipLineSplit.InventoryID;
    if (!(nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue))
      newline.UnitCost = new Decimal?(this.GetNSKitComponentUnitCost(soline, line, shipLineSplit).GetValueOrDefault());
    else
      newline.UnitCost = this.GetINTranUnitCost(soline, line, shipLineSplit);
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.UpdateInventoryExtension.ShouldReleaseShipmentOnPost(PX.Objects.SO.Models.PostShipmentArgs,System.Collections.Generic.List{PX.Objects.IN.INItemPlan})" />
  /// .
  [PXOverride]
  public bool ShouldReleaseShipmentOnPost(
    PostShipmentArgs args,
    List<INItemPlan> reattachedPlans,
    Func<PostShipmentArgs, List<INItemPlan>, bool> base_ShouldReleaseShipmentOnPost)
  {
    bool flag1 = base_ShouldReleaseShipmentOnPost(args, reattachedPlans);
    INRegisterEntryBase inRegisterEntry = args.INRegisterEntry;
    if (args.SourceDocEntry.IsValueCreated && args.SourceDocEntry.Value is SOOrderEntry soOrderEntry)
      ((PXAction) soOrderEntry.Save).Press();
    foreach (PXResult<PX.Objects.SO.SOOrderShipment> pxResult in PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelect<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentType>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[2]
    {
      (object) args.ShipmentNbr,
      (object) args.ShipmentType
    }))
    {
      PX.Objects.SO.SOOrderShipment orderShipment = PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(pxResult);
      if (orderShipment.OrderType == args.SourceDocType && orderShipment.OrderNbr == args.SourceDocNbr)
      {
        orderShipment.InvtDocType = inRegisterEntry.INRegisterDataMember.Current.DocType;
        orderShipment.InvtRefNbr = inRegisterEntry.INRegisterDataMember.Current.RefNbr;
        orderShipment.InvtNoteID = inRegisterEntry.INRegisterDataMember.Current.NoteID;
        ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Update(orderShipment);
        this.UpdatePlansRefNoteID(orderShipment, orderShipment.InvtNoteID, (IEnumerable<INItemPlan>) reattachedPlans);
      }
      int num1 = flag1 ? 1 : 0;
      int num2;
      if (orderShipment.InvtRefNbr == null)
      {
        bool? createInDoc = orderShipment.CreateINDoc;
        bool flag2 = false;
        num2 = createInDoc.GetValueOrDefault() == flag2 & createInDoc.HasValue ? 1 : 0;
      }
      else
        num2 = 1;
      flag1 = (num1 & num2) != 0;
    }
    return flag1;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.UpdateInventoryExtension.SplitDemandAndAssignSupply(PX.Objects.SO.Models.PostShipmentArgs,PX.Objects.SO.SOShipLine,PX.Objects.IN.INItemPlan,PX.Objects.IN.INTranSplit,System.Nullable{System.Decimal})" />
  /// .
  [PXOverride]
  public Decimal? SplitDemandAndAssignSupply(
    PostShipmentArgs args,
    SOShipLine line,
    INItemPlan demandPlan,
    INTranSplit newsplit,
    Decimal? restShipQty,
    Func<PostShipmentArgs, SOShipLine, INItemPlan, INTranSplit, Decimal?, Decimal?> base_SplitDemandAndAssignSupply)
  {
    if (!(args.SourceDocEntry.Value is SOOrderEntry orderEntry) || !this.LoadDemandOrder(orderEntry, demandPlan))
      return new Decimal?(0M);
    demandPlan.SupplyPlanID = newsplit.PlanID;
    GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) orderEntry), (object) demandPlan, true);
    Decimal? nullable1 = demandPlan.PlanQty;
    Decimal? nullable2 = restShipQty;
    Decimal? baseQty1 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    PXResult<PX.Objects.SO.SOLineSplit> pxResult = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemPlan>.On<PX.Objects.SO.SOLineSplit.FK.ItemPlan>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<BqlField<SOShipLine.origOrderType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderNbr, Equal<BqlField<SOShipLine.origOrderNbr, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.lineNbr, Equal<BqlField<SOShipLine.origLineNbr, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.parentSplitLineNbr, Equal<BqlField<SOShipLine.origSplitLineNbr, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.completed, IBqlBool>.IsEqual<False>>>>>>>.Config>.SelectSingleBound((PXGraph) orderEntry, (object[]) new SOShipLine[1]
    {
      line
    }, Array.Empty<object>()));
    Decimal? nullable3;
    if (pxResult != null)
    {
      INItemPlan inItemPlan = PXResult.Unwrap<INItemPlan>((object) pxResult);
      Decimal? nullable4 = baseQty1;
      nullable1 = inItemPlan.PlanQty;
      Decimal? baseQty2 = nullable4.GetValueOrDefault() <= nullable1.GetValueOrDefault() & nullable4.HasValue & nullable1.HasValue ? baseQty1 : inItemPlan.PlanQty;
      this.InsertNewSOLineSplit(orderEntry, baseQty2, inItemPlan.PlanID);
      nullable1 = baseQty1;
      nullable3 = baseQty2;
      baseQty1 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    }
    nullable3 = baseQty1;
    Decimal num = 0M;
    if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
    {
      this.InsertNewSOLineSplit(orderEntry, baseQty1, new long?());
      baseQty1 = new Decimal?(0M);
    }
    return restShipQty;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.UpdateInventoryExtension.ShouldReleaseTransferOnPost(PX.Objects.SO.Models.PostShipmentArgs,PX.Objects.SO.SOShipment)" />
  /// .
  [PXOverride]
  public bool ShouldReleaseTransferOnPost(
    PostShipmentArgs args,
    PX.Objects.SO.SOShipment shipment,
    Func<PostShipmentArgs, PX.Objects.SO.SOShipment, bool> base_ShouldReleaseTransferOnPost)
  {
    return PXParentAttribute.SelectChildren(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.OrderList).Cache, (object) shipment, typeof (PX.Objects.SO.SOShipment)).Cast<PX.Objects.SO.SOOrderShipment>().All<PX.Objects.SO.SOOrderShipment>((Func<PX.Objects.SO.SOOrderShipment, bool>) (s => !s.CreateINDoc.GetValueOrDefault()));
  }

  private IEnumerable<PostShipmentArgs> GetPostShipmentArgsInternal(
    PX.Objects.SO.SOShipment shiporder,
    INRegisterEntryBase docgraph,
    DocumentList<PX.Objects.IN.INRegister> list)
  {
    SOShipmentEntry soShipmentEntry = ((PXGraphExtension<SOShipmentEntry>) this).Base;
    object[] objArray1 = new object[1]{ (object) shiporder };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder> pxResult in PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelectJoin<PX.Objects.SO.SOOrderShipment, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<PX.Objects.SO.SOOrderShipment.orderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<PX.Objects.SO.SOOrderShipment.orderNbr>>>>, Where<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<Current<PX.Objects.SO.SOShipment.shipmentType>>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Current<PX.Objects.SO.SOShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.invtRefNbr, IsNull>>>, OrderBy<Asc<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>.Config>.SelectMultiBound((PXGraph) soShipmentEntry, objArray1, objArray2))
      yield return new PostShipmentArgs(docgraph, PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>.op_Implicit(pxResult), PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder>.op_Implicit(pxResult), list, (PX.Objects.AR.ARInvoice) null);
  }

  private Decimal? GetNSKitComponentUnitCost(PX.Objects.SO.SOLine soline, SOShipLine line, SOShipLineSplit split)
  {
    if (line.Operation == "R")
    {
      INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.sOOrderType, Equal<BqlField<SOShipLineSplit.origOrderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<INTran.sOOrderNbr, IBqlString>.IsEqual<BqlField<SOShipLineSplit.origOrderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<INTran.sOOrderLineNbr, IBqlInt>.IsEqual<BqlField<SOShipLineSplit.origLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INTran.inventoryID, IBqlInt>.IsEqual<BqlField<SOShipLineSplit.inventoryID, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, (object[]) new SOShipLineSplit[1]
      {
        split
      }, Array.Empty<object>()));
      if (inTran != null)
        return inTran.UnitCost;
      INItemSite inItemSite = INItemSite.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, split.InventoryID, split.SiteID);
      if (inItemSite != null && inItemSite.TranUnitCost.HasValue)
        return inItemSite.TranUnitCost;
      PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soline.BranchID);
      return new Decimal?(((Decimal?) INItemCost.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, split.InventoryID, branch?.BaseCuryID)?.TranUnitCost).GetValueOrDefault());
    }
    PXResultset<INTran> source = PXSelectBase<INTran, PXSelectJoin<INTran, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.tranType, Equal<INTran.aRDocType>, And<PX.Objects.AR.ARTran.refNbr, Equal<INTran.aRRefNbr>, And<PX.Objects.AR.ARTran.lineNbr, Equal<INTran.aRLineNbr>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<PX.Objects.AR.ARTran.inventoryID, Equal<Required<PX.Objects.AR.ARTran.inventoryID>>, And<INTran.inventoryID, Equal<Required<INTran.inventoryID>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
    {
      (object) soline.InvoiceType,
      (object) soline.InvoiceNbr,
      (object) line.InventoryID,
      (object) split.InventoryID
    });
    if (!string.IsNullOrEmpty(split.LotSerialNbr))
    {
      INTran inTran = PXResult<INTran>.op_Implicit(((IEnumerable<PXResult<INTran>>) source).AsEnumerable<PXResult<INTran>>().Where<PXResult<INTran>>((Func<PXResult<INTran>, bool>) (intran => string.Equals(PXResult<INTran>.op_Implicit(intran).LotSerialNbr, split.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault<PXResult<INTran>>());
      if (inTran != null)
        return inTran.UnitCost;
    }
    return PXResultset<INTran>.op_Implicit(source)?.UnitCost;
  }

  private Decimal? GetINTranUnitCost(PX.Objects.SO.SOLine soline, SOShipLine line, SOShipLineSplit split)
  {
    if (line.Operation == "R" && !string.IsNullOrEmpty(line.LotSerialNbr) && PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, line.InventoryID)?.ValMethod == "S")
    {
      List<INTranCost> list = GraphHelper.RowCast<INTranCost>((IEnumerable) PXSelectBase<INTranCost, PXViewOf<INTranCost>.BasedOn<SelectFromBase<INTranCost, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTran>.On<INTranCost.FK.Tran>>, FbqlJoins.Inner<PX.Objects.AR.ARTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.tranType, Equal<INTran.aRDocType>>>>, And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<INTran.aRRefNbr>>>>.And<BqlOperand<PX.Objects.AR.ARTran.lineNbr, IBqlInt>.IsEqual<INTran.aRLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTranCost.lotSerialNbr, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.AR.ARTran.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
      {
        (object) line.LotSerialNbr,
        (object) soline.InvoiceType,
        (object) soline.InvoiceNbr,
        (object) soline.InvoiceLineNbr
      })).ToList<INTranCost>();
      Decimal? nullable = list.Sum<INTranCost>((Func<INTranCost, Decimal?>) (c => c.Qty));
      if (nullable.GetValueOrDefault() != 0M)
        return new Decimal?(INUnitAttribute.ConvertToBase(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, line.InventoryID, line.UOM, list.Sum<INTranCost>((Func<INTranCost, Decimal?>) (c => c.TranCost)).Value / nullable.Value, INPrecision.UNITCOST));
    }
    return line.UnitCost;
  }

  public virtual void UpdatePlansRefNoteID(
    PX.Objects.SO.SOOrderShipment orderShipment,
    Guid? refNoteID,
    IEnumerable<INItemPlan> reattachedPlans)
  {
    if (!reattachedPlans.Any<INItemPlan>())
      return;
    ((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).Persist((PXDBOperation) 3);
    ((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).Persisted(false);
    PXUpdateJoin<Set<INItemPlan.refNoteID, Required<INItemPlan.refNoteID>, Set<INItemPlan.refEntityType, PX.Objects.Common.Constants.DACName<PX.Objects.IN.INRegister>, Set<INItemPlan.kitInventoryID, Null>>>, INItemPlan, InnerJoin<SOShipLineSplit, On<SOShipLineSplit.planID, Equal<INItemPlan.planID>>>, Where<SOShipLineSplit.origOrderType, Equal<Required<SOShipLineSplit.origOrderType>>, And<SOShipLineSplit.origOrderNbr, Equal<Required<SOShipLineSplit.origOrderNbr>>, And<SOShipLineSplit.shipmentNbr, Equal<Required<SOShipLineSplit.shipmentNbr>>>>>>.Update((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
    {
      (object) refNoteID,
      (object) orderShipment.OrderType,
      (object) orderShipment.OrderNbr,
      (object) orderShipment.ShipmentNbr
    });
    byte[] numArray = PXDatabase.SelectTimeStamp();
    foreach (INItemPlan reattachedPlan in reattachedPlans)
      PXTimeStampScope.PutPersisted((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base), (object) reattachedPlan, new object[1]
      {
        (object) numArray
      });
  }

  private bool LoadDemandOrder(SOOrderEntry orderEntry, INItemPlan demandPlan)
  {
    PX.Objects.SO.SOLineSplit soLineSplit = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.Select((PXGraph) orderEntry, new object[1]
    {
      (object) demandPlan.PlanID
    }));
    if (soLineSplit == null)
      return false;
    ((PXSelectBase<PX.Objects.SO.SOOrder>) orderEntry.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) orderEntry.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) soLineSplit.OrderNbr, new object[1]
    {
      (object) soLineSplit.OrderType
    }));
    ((PXSelectBase<PX.Objects.SO.SOLine>) orderEntry.Transactions).Current = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOLine>) orderEntry.Transactions).Search<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>((object) soLineSplit.OrderType, (object) soLineSplit.OrderNbr, (object) soLineSplit.LineNbr, Array.Empty<object>()));
    ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Current = soLineSplit;
    return true;
  }

  protected virtual PX.Objects.SO.SOLineSplit InsertNewSOLineSplit(
    SOOrderEntry orderEntry,
    Decimal? baseQty,
    long? supplyPlanID)
  {
    using (orderEntry.LineSplittingExt.SuppressedModeScope(true))
    {
      PX.Objects.SO.SOLineSplit current = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Current;
      Decimal? baseQty1 = current.BaseQty;
      Decimal? nullable1 = baseQty;
      Decimal? nullable2 = baseQty1.HasValue & nullable1.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      nullable1 = nullable2;
      Decimal num1 = 0M;
      if (nullable1.GetValueOrDefault() <= num1 & nullable1.HasValue)
        return (PX.Objects.SO.SOLineSplit) null;
      PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(current);
      copy1.BaseQty = nullable2;
      PX.Objects.SO.SOLineSplit soLineSplit1 = copy1;
      PXCache cache1 = ((PXSelectBase) orderEntry.splits).Cache;
      int? inventoryId1 = copy1.InventoryID;
      string uom1 = copy1.UOM;
      nullable1 = copy1.BaseQty;
      Decimal num2 = nullable1.Value;
      Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromBase(cache1, inventoryId1, uom1, num2, INPrecision.QUANTITY));
      soLineSplit1.Qty = nullable3;
      PX.Objects.SO.SOLineSplit soLineSplit2 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Update(copy1);
      FbqlSelect<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<P.AsLong>>, INItemPlan>.View view = new FbqlSelect<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<P.AsLong>>, INItemPlan>.View((PXGraph) orderEntry);
      INItemPlan copy2 = PXCache<INItemPlan>.CreateCopy(PXResultset<INItemPlan>.op_Implicit(((PXSelectBase<INItemPlan>) view).Select(new object[1]
      {
        (object) soLineSplit2.PlanID
      })));
      copy2.SiteID = soLineSplit2.ToSiteID;
      int? nullable4 = copy2.CostCenterID;
      int num3 = 0;
      if (!(nullable4.GetValueOrDefault() == num3 & nullable4.HasValue))
        copy2.CostCenterID = (KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOLine.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOLine.orderNbr>, Field<PX.Objects.SO.SOLineSplit.lineNbr>.IsRelatedTo<PX.Objects.SO.SOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.FindParent((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soLineSplit2, (PKFindOptions) 0) ?? throw new RowNotFoundException(((PXSelectBase) ((PXGraphExtension<SOOrderExtension, SOShipmentEntry>) this).Base1.dummy_soline).Cache, new object[3]
        {
          (object) soLineSplit2.OrderType,
          (object) soLineSplit2.OrderNbr,
          (object) soLineSplit2.LineNbr
        })).CostCenterID;
      INItemPlan inItemPlan = GraphHelper.Caches<INItemPlan>((PXGraph) orderEntry).Update(copy2);
      PX.Objects.SO.SOLineSplit copy3 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit2);
      copy3.PlanID = new long?();
      PX.Objects.SO.SOLineSplit soLineSplit3 = copy3;
      nullable4 = new int?();
      int? nullable5 = nullable4;
      soLineSplit3.SplitLineNbr = nullable5;
      copy3.ShipmentNbr = (string) null;
      if (!supplyPlanID.HasValue)
      {
        copy3.IsAllocated = new bool?(false);
        PX.Objects.SO.SOLineSplit soLineSplit4 = copy3;
        nullable4 = new int?();
        int? nullable6 = nullable4;
        soLineSplit4.SiteID = nullable6;
        PX.Objects.SO.SOLineSplit soLineSplit5 = copy3;
        nullable4 = new int?();
        int? nullable7 = nullable4;
        soLineSplit5.CostCenterID = nullable7;
        copy3.ClearPOFlags();
        copy3.ClearSOReferences();
        copy3.POType = (string) null;
        copy3.PONbr = (string) null;
        PX.Objects.SO.SOLineSplit soLineSplit6 = copy3;
        nullable4 = new int?();
        int? nullable8 = nullable4;
        soLineSplit6.POLineNbr = nullable8;
        PX.Objects.SO.SOLineSplit soLineSplit7 = copy3;
        nullable4 = new int?();
        int? nullable9 = nullable4;
        soLineSplit7.VendorID = nullable9;
        copy3.RefNoteID = new Guid?();
      }
      copy3.BaseReceivedQty = new Decimal?(0M);
      copy3.ReceivedQty = new Decimal?(0M);
      copy3.BaseShippedQty = new Decimal?(0M);
      copy3.ShippedQty = new Decimal?(0M);
      copy3.BaseQty = new Decimal?(0M);
      copy3.Qty = new Decimal?(0M);
      PX.Objects.SO.SOLineSplit soLineSplit8 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Insert(copy3);
      Decimal? nullable10;
      if (!supplyPlanID.HasValue)
      {
        int? siteId = soLineSplit8.SiteID;
        PX.Objects.SO.SOLineSplit copy4 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit8);
        copy4.IsAllocated = new bool?(true);
        copy4.SiteID = soLineSplit2.SiteID;
        soLineSplit8 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Update(copy4);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
        statusByCostCenter1.InventoryID = soLineSplit8.InventoryID;
        statusByCostCenter1.SubItemID = soLineSplit8.SubItemID;
        statusByCostCenter1.SiteID = soLineSplit8.SiteID;
        statusByCostCenter1.CostCenterID = soLineSplit8.CostCenterID;
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = statusByCostCenter1;
        TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID> ifilledWith = PrimaryKeyOf<INSiteStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>>.Find((PXGraph) orderEntry, (TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>) statusByCostCenter2, (PKFindOptions) 0);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>((PXGraph) orderEntry).Locate(statusByCostCenter2);
        nullable1 = (Decimal?) ((INSiteStatusByCostCenter) ifilledWith)?.QtyHardAvail;
        Decimal valueOrDefault1;
        if (!nullable1.HasValue)
        {
          nullable10 = (Decimal?) statusByCostCenter3?.QtyHardAvail;
          valueOrDefault1 = (nullable10.HasValue ? new Decimal?(0M + nullable10.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        }
        else
          valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable10 = baseQty;
        Decimal valueOrDefault2 = nullable10.GetValueOrDefault();
        if (valueOrDefault1 < valueOrDefault2 & nullable10.HasValue)
        {
          PX.Objects.SO.SOLineSplit copy5 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit8);
          copy5.IsAllocated = new bool?(false);
          copy5.SiteID = siteId;
          PX.Objects.SO.SOLineSplit copy6 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Update(copy5));
          copy6.POCreate = new bool?(false);
          soLineSplit8 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Update(copy6);
        }
      }
      PX.Objects.SO.SOLineSplit copy7 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit8);
      copy7.BaseQty = baseQty;
      PX.Objects.SO.SOLineSplit soLineSplit9 = copy7;
      PXCache cache2 = ((PXSelectBase) orderEntry.splits).Cache;
      int? inventoryId2 = copy7.InventoryID;
      string uom2 = copy7.UOM;
      nullable10 = copy7.BaseQty;
      Decimal num4 = nullable10.Value;
      Decimal? nullable11 = new Decimal?(INUnitAttribute.ConvertFromBase(cache2, inventoryId2, uom2, num4, INPrecision.QUANTITY));
      soLineSplit9.Qty = nullable11;
      PX.Objects.SO.SOLineSplit soLineSplit10 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Update(copy7);
      if (supplyPlanID.HasValue)
      {
        INItemPlan copy8 = PXCache<INItemPlan>.CreateCopy(PXResultset<INItemPlan>.op_Implicit(((PXSelectBase<INItemPlan>) view).Select(new object[1]
        {
          (object) soLineSplit10.PlanID
        })));
        copy8.SupplyPlanID = supplyPlanID;
        copy8.PlanType = "93";
        copy8.SiteID = soLineSplit10.ToSiteID;
        copy8.CostCenterID = inItemPlan.CostCenterID;
        copy8.FixedSource = (string) null;
        GraphHelper.Caches<INItemPlan>((PXGraph) orderEntry).Update(copy8);
      }
      ((PXSelectBase<PX.Objects.SO.SOLineSplit>) orderEntry.splits).Current = soLineSplit2;
      return soLineSplit10;
    }
  }
}
