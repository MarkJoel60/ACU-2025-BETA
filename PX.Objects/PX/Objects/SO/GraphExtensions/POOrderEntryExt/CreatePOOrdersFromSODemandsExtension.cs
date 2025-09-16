// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromSODemandsExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.POOrderEntryExt;
using PX.Objects.SO.POCreateExt;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.POOrderEntryExt;

public class CreatePOOrdersFromSODemandsExtension : 
  PXGraphExtension<
  #nullable disable
  CreatePOOrdersFromDemandsExtension, POOrderEntry>
{
  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.MakeDemandSourceInfo(PX.Objects.PO.POFixedDemand,PX.Objects.PO.POCreate.POCreateFilter)" />
  [PXOverride]
  public PODemandSourceInfo MakeDemandSourceInfo(
    POFixedDemand demand,
    POCreate.POCreateFilter processingSettings,
    Func<POFixedDemand, POCreate.POCreateFilter, PODemandSourceInfo> base_MakeDemandSourceInfo)
  {
    PODemandSourceInfo target = base_MakeDemandSourceInfo(demand, processingSettings);
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOOrder.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, new object[1]
    {
      (object) demand.RefNoteID
    }));
    POOrderEntry.SOLineSplit3 soLineSplit3 = PXResultset<POOrderEntry.SOLineSplit3>.op_Implicit(PXSelectBase<POOrderEntry.SOLineSplit3, PXViewOf<POOrderEntry.SOLineSplit3>.BasedOn<SelectFromBase<POOrderEntry.SOLineSplit3, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<POOrderEntry.SOLineSplit3.planID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.Select((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, new object[1]
    {
      (object) demand.PlanID
    }));
    if (soOrder != null && soLineSplit3 != null)
    {
      PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, soLineSplit3.OrderType, soLineSplit3.OrderNbr, soLineSplit3.LineNbr);
      if (EnumerableExtensions.IsIn<string>(soLineSplit3.POSource, "D", "L") && !soLineSplit3.IsValidForDropShip.GetValueOrDefault())
        throw new PXException("The line cannot be drop-shipped because it is split into multiple lines or allocated in the Line Details dialog box.");
      CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo demandSourceInfo = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(target);
      demandSourceInfo.Order = soOrder;
      demandSourceInfo.Line = soLine;
      demandSourceInfo.Split = soLineSplit3;
      demandSourceInfo.IsSingleSOOrder = new bool?(PXCacheEx.GetExtension<POCreate.POCreateFilter, SOxPOCreateFilter>(processingSettings).OrderNbr != null);
      if (EnumerableExtensions.IsIn<string>(demand.PlanType, "6D", "6E"))
        target.POOrderType = "DP";
      target.AlternateID = soLine.AlternateID;
      target.ProjectID = soLineSplit3.ProjectID;
      target.TaskID = soLineSplit3.TaskID;
      target.CostCodeID = soLineSplit3.CostCodeID;
    }
    return target;
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.MakePOOrderLookup(PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public List<FieldLookup> MakePOOrderLookup(
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<POFixedDemand, PODemandSourceInfo, List<FieldLookup>> base_MakePOOrderLookup)
  {
    List<FieldLookup> fieldLookupList = base_MakePOOrderLookup(demand, demandSource);
    (PX.Objects.SO.SOOrder order, PX.Objects.SO.SOLine line, POOrderEntry.SOLineSplit3 split) = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource);
    if (split != null)
    {
      bool flag = EnumerableExtensions.IsIn<string>(demand.PlanType, "6B", "6E");
      fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.bLOrderNbr>(flag ? (object) split.PONbr : (object) (string) null));
      if (demandSource.POOrderType == "DP")
      {
        fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.sOOrderType>((object) split.OrderType));
        fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.sOOrderNbr>((object) split.OrderNbr));
        fieldLookupList.RemoveAll((Predicate<FieldLookup>) (fl => fl.Field == typeof (PX.Objects.PO.POOrder.siteID)));
      }
      else if (demandSource.POOrderType != "RO")
      {
        fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.shipToBAccountID>((object) order.CustomerID));
        fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.shipToLocationID>((object) order.CustomerLocationID));
      }
      if (line.IsSpecialOrder.GetValueOrDefault())
        fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.curyID>((object) demand.CuryID));
    }
    return fieldLookupList;
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.FillNewPOOrderFromDemand(PX.Objects.PO.POOrder,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public PX.Objects.PO.POOrder FillNewPOOrderFromDemand(
    PX.Objects.PO.POOrder poOrder,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<PX.Objects.PO.POOrder, POFixedDemand, PODemandSourceInfo, PX.Objects.PO.POOrder> base_FillNewPOOrderFromDemand)
  {
    poOrder = base_FillNewPOOrderFromDemand(poOrder, demand, demandSource);
    (PX.Objects.SO.SOOrder order, PX.Objects.SO.SOLine line, POOrderEntry.SOLineSplit3 split) = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource);
    bool flag1 = EnumerableExtensions.IsIn<string>(demand.PlanType, "6B", "6E");
    poOrder.BLType = flag1 ? "BL" : (string) null;
    poOrder.BLOrderNbr = flag1 ? split.PONbr : (string) null;
    if (!string.IsNullOrEmpty(poOrder.BLOrderNbr))
    {
      PX.Objects.PO.POOrder parent = KeysRelation<CompositeKey<Field<PX.Objects.PO.POOrder.bLType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POOrder.bLOrderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POOrder>, PX.Objects.PO.POOrder, PX.Objects.PO.POOrder>.FindParent((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, poOrder, (PKFindOptions) 1);
      if (parent != null)
        poOrder.VendorRefNbr = parent.VendorRefNbr;
    }
    bool? nullable;
    if (poOrder.OrderType == "DP")
    {
      poOrder.SOOrderType = order.OrderType;
      poOrder.SOOrderNbr = order.OrderNbr;
      SOAddress source1 = PXResultset<SOAddress>.op_Implicit(PXSelectBase<SOAddress, PXViewOf<SOAddress>.BasedOn<SelectFromBase<SOAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOAddress.addressID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, new object[1]
      {
        (object) order.ShipAddressID
      }));
      nullable = source1.IsDefaultAddress;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        SharedRecordAttribute.CopyRecord<PX.Objects.PO.POOrder.shipAddressID>(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Cache, (object) poOrder, (object) source1, true);
      SOContact source2 = PXResultset<SOContact>.op_Implicit(PXSelectBase<SOContact, PXViewOf<SOContact>.BasedOn<SelectFromBase<SOContact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOContact.contactID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, new object[1]
      {
        (object) order.ShipContactID
      }));
      nullable = source2.IsDefaultContact;
      bool flag3 = false;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        SharedRecordAttribute.CopyRecord<PX.Objects.PO.POOrder.shipContactID>(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Cache, (object) poOrder, (object) source2, true);
      DateTime? expectedDate = poOrder.ExpectedDate;
      DateTime? requestDate = order.RequestDate;
      if ((expectedDate.HasValue & requestDate.HasValue ? (expectedDate.GetValueOrDefault() < requestDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        poOrder.ExpectedDate = order.RequestDate;
      poOrder.ShipDestType = "C";
      poOrder.ShipToBAccountID = order.CustomerID;
      poOrder.ShipToLocationID = order.CustomerLocationID;
    }
    else if (((PXSelectBase<POSetup>) ((PXGraphExtension<POOrderEntry>) this).Base.POSetup).Current.ShipDestType == "S")
    {
      poOrder.ShipDestType = "S";
      poOrder.SiteID = demand.POSiteID;
    }
    nullable = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource).IsSingleSOOrder;
    if (nullable.GetValueOrDefault())
    {
      poOrder.SOOrderType = order.OrderType;
      poOrder.SOOrderNbr = order.OrderNbr;
    }
    poOrder = ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Update(poOrder);
    if (line != null)
    {
      nullable = line.IsSpecialOrder;
      if (nullable.GetValueOrDefault() && poOrder.CuryID != demand.CuryID)
      {
        poOrder.CuryID = demand.CuryID;
        poOrder = ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Update(poOrder);
      }
    }
    return poOrder;
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.FindPOLine(PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo,PX.Objects.CS.DocumentList{PX.Objects.PO.POLine})" />
  [PXOverride]
  public PX.Objects.PO.POLine FindPOLine(
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    DocumentList<PX.Objects.PO.POLine> createdLines,
    Func<POFixedDemand, PODemandSourceInfo, DocumentList<PX.Objects.PO.POLine>, PX.Objects.PO.POLine> base_FindPOLine)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.OrderType != "RO")
      return (PX.Objects.PO.POLine) null;
    return demand.PlanType == "6B" ? (PX.Objects.PO.POLine) null : base_FindPOLine(demand, demandSource, createdLines);
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.MakePOLineLookup(PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public List<FieldLookup> MakePOLineLookup(
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<POFixedDemand, PODemandSourceInfo, List<FieldLookup>> base_MakePOLineLookup)
  {
    List<FieldLookup> fieldLookupList = base_MakePOLineLookup(demand, demandSource);
    POOrderEntry.SOLineSplit3 split = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource).Split;
    if (split != null)
    {
      fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POLine.requestedDate>((object) (DateTime?) split?.ShipDate));
      if (((PXSelectBase<POSetup>) ((PXGraphExtension<POOrderEntry>) this).Base.POSetup).Current.CopyLineDescrSO.GetValueOrDefault())
        fieldLookupList.Add((FieldLookup) new FieldLookup<PX.Objects.PO.POLine.tranDesc>((object) split.TranDesc));
      if (((PXSelectBase<POSetup>) ((PXGraphExtension<POOrderEntry>) this).Base.POSetup).Current.CopyLineNoteSO.GetValueOrDefault())
        fieldLookupList.Add((FieldLookup) new FieldLookupNoteText(PXNoteAttribute.GetNote((PXCache) GraphHelper.Caches<POOrderEntry.SOLineSplit3>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base), (object) split)));
    }
    return fieldLookupList;
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.FillNewPOLineFromDemand(PX.Objects.PO.POLine,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public PX.Objects.PO.POLine FillNewPOLineFromDemand(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<PX.Objects.PO.POLine, POFixedDemand, PODemandSourceInfo, PX.Objects.PO.POLine> base_FillNewPOLineFromDemand)
  {
    poLine = base_FillNewPOLineFromDemand(poLine, demand, demandSource);
    POOrderEntry.SOLineSplit3 split = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource).Split;
    if (split != null)
    {
      poLine.IsStockItem = split.IsStockItem;
      poLine.IsSpecialOrder = split.IsSpecialOrder;
      bool? nullable = split.IsSpecialOrder;
      if (nullable.GetValueOrDefault())
      {
        poLine.CuryUnitCost = split.CuryUnitCost;
        poLine.SOOrderType = split.OrderType;
        poLine.SOOrderNbr = split.OrderNbr;
        poLine.SOLineNbr = split.LineNbr;
      }
      if (((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.OrderType == "DP")
      {
        poLine.LineType = split.LineType != "GI" ? "NP" : "GP";
        poLine.RequestedDate = split.RequestDate;
        poLine.ExpenseSubID = new int?();
        if (split.LineType == "GN")
        {
          INPostClass inPostClass = INPostClass.PK.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, demand.InventoryID)?.PostClassID);
          if (inPostClass != null)
          {
            nullable = inPostClass.COGSSubFromSales;
            if (nullable.GetValueOrDefault())
              poLine.ExpenseSubID = split.SalesSubID;
          }
        }
      }
      else if (((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.OrderType == "RO")
      {
        poLine.LineType = split.LineType != "GI" ? "NO" : "GS";
        if (demand.PlanType != "90")
          poLine.RequestedDate = split.ShipDate;
      }
    }
    return poLine;
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.CopyNoteAndFilesToNewPOLine(PX.Objects.PO.POLine,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public void CopyNoteAndFilesToNewPOLine(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Action<PX.Objects.PO.POLine, POFixedDemand, PODemandSourceInfo> base_CopyNoteAndFilesToNewPOLine)
  {
    base_CopyNoteAndFilesToNewPOLine(poLine, demand, demandSource);
    POOrderEntry.SOLineSplit3 split = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource).Split;
    if (split == null)
      return;
    if (((PXSelectBase<POSetup>) ((PXGraphExtension<POOrderEntry>) this).Base.POSetup).Current.CopyLineDescrSO.GetValueOrDefault())
      poLine.TranDesc = split.TranDesc;
    if (!((PXSelectBase<POSetup>) ((PXGraphExtension<POOrderEntry>) this).Base.POSetup).Current.CopyLineNoteSO.GetValueOrDefault())
      return;
    PXNoteAttribute.SetNote(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Cache, (object) poLine, PXNoteAttribute.GetNote((PXCache) GraphHelper.Caches<POOrderEntry.SOLineSplit3>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base), (object) split));
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.LinkPOLineToSource(PX.Objects.PO.POLine,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public PX.Objects.PO.POLine LinkPOLineToSource(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<PX.Objects.PO.POLine, POFixedDemand, PODemandSourceInfo, PX.Objects.PO.POLine> base_LinkPOLineToSource)
  {
    poLine = base_LinkPOLineToSource(poLine, demand, demandSource);
    if (EnumerableExtensions.IsIn<string>(demand.PlanType, "6B", "6E"))
      poLine = this.LinkPOLineToBlanket(poLine, demand, demandSource);
    poLine = this.LinkPOLineToDropShip(poLine, demand, demandSource);
    poLine = this.LinkPOLineToSOLineSplit(poLine, demand, demandSource);
    return poLine;
  }

  protected virtual PX.Objects.PO.POLine LinkPOLineToSOLineSplit(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    POOrderEntry.SOLineSplit3 split = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource).Split;
    if (split != null)
    {
      split.POType = poLine.OrderType;
      split.PONbr = poLine.OrderNbr;
      split.POLineNbr = poLine.LineNbr;
      split.RefNoteID = ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.NoteID;
      string str = split.POSource == "L" ? "D" : (split.POSource == "B" ? "O" : (string) null);
      if (str != null)
      {
        split.POSource = str;
        POOrderEntry.SOLine5 soLine5 = PXResultset<POOrderEntry.SOLine5>.op_Implicit(((PXSelectBase<POOrderEntry.SOLine5>) ((PXGraphExtension<POOrderEntry>) this).Base.FixedDemandOrigSOLine).Select(new object[3]
        {
          (object) split.OrderType,
          (object) split.OrderNbr,
          (object) split.LineNbr
        }));
        if (soLine5 != null)
        {
          soLine5.POSource = str;
          GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.FixedDemandOrigSOLine).Cache, (object) soLine5, true);
        }
      }
      GraphHelper.EnsureCachePersistence<POOrderEntry.SOLineSplit3>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base);
      ((PXGraphExtension<POOrderEntry>) this).Base.UpdateSOLine(split, ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.VendorID, true);
      GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.FixedDemand).Cache, (object) split, true);
    }
    return poLine;
  }

  protected virtual PX.Objects.PO.POLine LinkPOLineToBlanket(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    string str = demand.PlanType == "6B" ? "66" : "6D";
    demand.FixedSource = "P";
    POOrderEntry.SOLineSplit3 split = CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource).Split;
    poLine.POType = split.POType;
    poLine.PONbr = split.PONbr;
    poLine.POLineNbr = split.POLineNbr;
    PX.Objects.PO.POLine parent = KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.pOType>.IsRelatedTo<PX.Objects.PO.POLine.orderType>, Field<PX.Objects.PO.POLine.pONbr>.IsRelatedTo<PX.Objects.PO.POLine.orderNbr>, Field<PX.Objects.PO.POLine.pOLineNbr>.IsRelatedTo<PX.Objects.PO.POLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POLine, PX.Objects.PO.POLine>, PX.Objects.PO.POLine, PX.Objects.PO.POLine>.FindParent((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, poLine, (PKFindOptions) 1);
    if (parent != null)
    {
      Decimal? nullable1 = demand.PlanQty;
      Decimal? nullable2 = parent.BaseOpenQty;
      if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
      {
        PX.Objects.PO.POLine poLine1 = poLine;
        nullable2 = poLine1.OrderQty;
        nullable1 = demand.OrderQty;
        poLine1.OrderQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        if (string.Equals(poLine.UOM, parent.UOM))
        {
          PX.Objects.PO.POLine poLine2 = poLine;
          nullable1 = poLine2.OrderQty;
          nullable2 = parent.OpenQty;
          poLine2.OrderQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          PXDBQuantityAttribute.CalcBaseQty<PX.Objects.PO.POLine.orderQty>(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Cache, (object) poLine);
          PX.Objects.PO.POLine poLine3 = poLine;
          nullable2 = poLine3.BaseOrderQty;
          nullable1 = parent.BaseOpenQty;
          poLine3.BaseOrderQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          PXDBQuantityAttribute.CalcTranQty<PX.Objects.PO.POLine.orderQty>(((PXSelectBase) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Cache, (object) poLine);
        }
        demandSource.Status = ((PXErrorLevel) 3, demandSource.Status.Text + PXMessages.LocalizeFormatNoPrefixNLA("Order Quantity reduced to Blanket Order: '{0}' Open Qty. for this item", new object[1]
        {
          (object) poLine.PONbr
        }));
      }
      poLine.CuryUnitCost = parent.CuryUnitCost;
      poLine.UnitCost = parent.UnitCost;
    }
    poLine = ((PXSelectBase<PX.Objects.PO.POLine>) ((PXGraphExtension<POOrderEntry>) this).Base.Transactions).Update(poLine);
    PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base);
    pxCache.RaiseRowDeleted((INItemPlan) demand);
    demand.PlanType = str;
    pxCache.RaiseRowInserted((INItemPlan) demand);
    return poLine;
  }

  protected virtual PX.Objects.PO.POLine LinkPOLineToDropShip(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    DropShipLinksExt extension = ((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base).GetExtension<DropShipLinksExt>();
    return extension != null ? extension.InsertDropShipLink(poLine, CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.Of(demandSource).Split) : poLine;
  }

  /// <exclude />
  public sealed class SOxPODemandSourceInfo : PXCacheExtension<PODemandSourceInfo>
  {
    public PX.Objects.SO.SOOrder Order { get; set; }

    public PX.Objects.SO.SOLine Line { get; set; }

    public POOrderEntry.SOLineSplit3 Split { get; set; }

    [PXBool]
    public bool? IsSingleSOOrder { get; set; }

    public static CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo Of(
      PODemandSourceInfo target)
    {
      return target == null ? (CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo) null : PXCacheEx.GetExtension<PODemandSourceInfo, CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo>(target);
    }

    public void Deconstruct(
      out PX.Objects.SO.SOOrder order,
      out PX.Objects.SO.SOLine line,
      out POOrderEntry.SOLineSplit3 split)
    {
      PX.Objects.SO.SOOrder order1 = this.Order;
      PX.Objects.SO.SOLine line1 = this.Line;
      POOrderEntry.SOLineSplit3 split1 = this.Split;
      order = order1;
      line = line1;
      split = split1;
    }

    public abstract class isSingleSOOrder : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CreatePOOrdersFromSODemandsExtension.SOxPODemandSourceInfo.isSingleSOOrder>
    {
    }
  }

  public class SpecialOrderCurrencyError : PX.Objects.SO.GraphExtensions.SpecialOrderCurrencyError<POOrderEntry>
  {
    public class ShowOnMakeDemandSourceInfo : 
      PXGraphExtension<CreatePOOrdersFromSODemandsExtension.SpecialOrderCurrencyError, CreatePOOrdersFromDemandsExtension, POOrderEntry>
    {
      /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.MakeDemandSourceInfo(PX.Objects.PO.POFixedDemand,PX.Objects.PO.POCreate.POCreateFilter)" />
      [PXOverride]
      public PODemandSourceInfo MakeDemandSourceInfo(
        POFixedDemand demand,
        POCreate.POCreateFilter processingSettings,
        Func<POFixedDemand, POCreate.POCreateFilter, PODemandSourceInfo> base_MakeDemandSourceInfo)
      {
        string orderCurrencyError = this.Base2.GetSpecialOrderCurrencyError(demand.CuryID, demand.VendorID, processingSettings.BranchID, true);
        if (!string.IsNullOrEmpty(orderCurrencyError))
          throw new PXException(orderCurrencyError);
        return base_MakeDemandSourceInfo(demand, processingSettings);
      }
    }
  }
}
