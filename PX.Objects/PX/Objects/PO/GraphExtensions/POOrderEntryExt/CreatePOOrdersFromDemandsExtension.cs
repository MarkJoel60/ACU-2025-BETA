// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class CreatePOOrdersFromDemandsExtension : PXGraphExtension<POOrderEntry>
{
  public virtual PXRedirectRequiredException CreatePOOrders(
    List<POFixedDemand> demands,
    POCreate.POCreateFilter processingSettings)
  {
    demands.Sort((Comparison<POFixedDemand>) ((a, b) => a.SorterString.CompareTo(b.SorterString)));
    bool flag = false;
    DocumentList<PX.Objects.PO.POOrder> createdOrders = new DocumentList<PX.Objects.PO.POOrder>((PXGraph) this.Base);
    Dictionary<string, DocumentList<PX.Objects.PO.POLine>> orderedByPlanType = new Dictionary<string, DocumentList<PX.Objects.PO.POLine>>();
    foreach (POFixedDemand demand in demands)
    {
      PXProcessing<POFixedDemand>.SetCurrentItem((object) demand);
      if (!(demand.FixedSource != "P"))
      {
        if (demand.VendorID.HasValue)
        {
          if (demand.VendorLocationID.HasValue)
          {
            try
            {
              (PXErrorLevel Level, string Text) tuple = this.ProcessDemand(demand, processingSettings, createdOrders, orderedByPlanType);
              if (((PXSelectBase) this.Base.Transactions).Cache.IsInsertedUpdatedDeleted)
              {
                ((PXAction) this.Base.Save).Press();
                string str = PXMessages.LocalizeFormatNoPrefixNLA("Purchase Order '{0}' created.", new object[1]
                {
                  (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderNbr
                });
                if (tuple.Level == 1)
                  PXProcessing<POFixedDemand>.SetInfo(str + Environment.NewLine + tuple.Text);
                else
                  PXProcessing<POFixedDemand>.SetWarning(str + Environment.NewLine + tuple.Text);
                if (createdOrders.Find((object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current) == null)
                {
                  createdOrders.Add(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current);
                  continue;
                }
                continue;
              }
              continue;
            }
            catch (Exception ex)
            {
              ((PXGraph) this.Base).Clear();
              PXProcessing<POFixedDemand>.SetError(ex);
              PXTrace.WriteError(ex);
              flag = true;
              continue;
            }
          }
        }
        PXProcessing<POFixedDemand>.SetWarning("Vendor and vendor location should be defined.");
      }
    }
    if (flag || createdOrders.Count != 1)
      return (PXRedirectRequiredException) null;
    using (new PXTimeStampScope((byte[]) null))
    {
      ((PXGraph) this.Base).Clear();
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) createdOrders[0].OrderNbr, new object[1]
      {
        (object) createdOrders[0].OrderType
      }));
      return new PXRedirectRequiredException((PXGraph) this.Base, "Purchase Order");
    }
  }

  protected virtual (PXErrorLevel Level, string Text) ProcessDemand(
    POFixedDemand demand,
    POCreate.POCreateFilter processingSettings,
    DocumentList<PX.Objects.PO.POOrder> createdOrders,
    Dictionary<string, DocumentList<PX.Objects.PO.POLine>> orderedByPlanType)
  {
    PODemandSourceInfo demandSource = this.MakeDemandSourceInfo(demand, processingSettings);
    this.ApplyDemandToPOOrder(demand, demandSource, createdOrders);
    DocumentList<PX.Objects.PO.POLine> createdLines = EnumerableEx.Ensure<string, DocumentList<PX.Objects.PO.POLine>>((IDictionary<string, DocumentList<PX.Objects.PO.POLine>>) orderedByPlanType, demand.PlanType, (Func<DocumentList<PX.Objects.PO.POLine>>) (() => new DocumentList<PX.Objects.PO.POLine>((PXGraph) this.Base)));
    this.ApplyDemandToPOLine(demand, demandSource, createdLines);
    return demandSource.Status;
  }

  protected virtual PODemandSourceInfo MakeDemandSourceInfo(
    POFixedDemand demand,
    POCreate.POCreateFilter processingSettings)
  {
    PODemandSourceInfo instance = GraphHelper.Caches<PODemandSourceInfo>((PXGraph) this.Base).Rows.CreateInstance();
    instance.BranchID = processingSettings.BranchID;
    instance.PurchDate = processingSettings.PurchDate;
    instance.POOrderType = "RO";
    return instance;
  }

  protected virtual PX.Objects.PO.POOrder ApplyDemandToPOOrder(
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    DocumentList<PX.Objects.PO.POOrder> createdOrders)
  {
    PX.Objects.PO.POOrder poOrder1 = this.FindPOOrder(demand, demandSource, createdOrders);
    if (poOrder1 != null)
    {
      if (!((PXSelectBase) this.Base.Document).Cache.ObjectsEqual((object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current, (object) poOrder1))
      {
        PXSelectJoin<PX.Objects.PO.POOrder, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POOrder.vendorID>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<PX.Objects.PO.POOrder.orderType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> document1 = this.Base.Document;
        PXSelectJoin<PX.Objects.PO.POOrder, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.PO.POOrder.vendorID>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<PX.Objects.PO.POOrder.orderType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> document2 = this.Base.Document;
        string orderNbr = poOrder1.OrderNbr;
        object[] objArray = new object[1]
        {
          (object) poOrder1.OrderType
        };
        PX.Objects.PO.POOrder poOrder2;
        PX.Objects.PO.POOrder poOrder3 = poOrder2 = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) document2).Search<PX.Objects.PO.POOrder.orderNbr>((object) orderNbr, objArray));
        ((PXSelectBase<PX.Objects.PO.POOrder>) document1).Current = poOrder2;
        poOrder1 = poOrder3;
      }
    }
    else
    {
      ((PXGraph) this.Base).Clear();
      poOrder1 = this.FillNewPOOrderFromDemand(new PX.Objects.PO.POOrder()
      {
        OrderType = demandSource.POOrderType
      }, demand, demandSource);
    }
    poOrder1.UpdateVendorCost = new bool?(false);
    return poOrder1;
  }

  protected virtual PX.Objects.PO.POOrder FindPOOrder(
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    DocumentList<PX.Objects.PO.POOrder> createdOrders)
  {
    List<FieldLookup> fieldLookupList = this.MakePOOrderLookup(demand, demandSource);
    if (demandSource.POOrderType == "RO" && createdOrders.Any<PX.Objects.PO.POOrder>((Func<PX.Objects.PO.POOrder, bool>) (o => o.ShipDestType == "L" && !o.SiteID.HasValue)))
      fieldLookupList.RemoveAll((Predicate<FieldLookup>) (fl => fl.Field == typeof (PX.Objects.PO.POOrder.siteID)));
    return createdOrders.Find(fieldLookupList.ToArray());
  }

  protected virtual List<FieldLookup> MakePOOrderLookup(
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    return new List<FieldLookup>()
    {
      (FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.orderType>((object) demandSource.POOrderType),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.vendorID>((object) demand.VendorID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.vendorLocationID>((object) demand.VendorLocationID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POOrder.siteID>((object) demand.POSiteID)
    };
  }

  protected virtual PX.Objects.PO.POOrder FillNewPOOrderFromDemand(
    PX.Objects.PO.POOrder poOrder,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    poOrder = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Insert(poOrder));
    poOrder.VendorID = demand.VendorID;
    poOrder.VendorLocationID = demand.VendorLocationID;
    poOrder.SiteID = demand.POSiteID;
    poOrder.OrderDate = demandSource.PurchDate;
    int num = !PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() ? 0 : (demandSource.BranchID.HasValue ? 1 : 0);
    if (num != 0)
      poOrder.BranchID = demandSource.BranchID;
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      poOrder.OverrideCurrency = new bool?(true);
    if (demand.DemandProjectID.HasValue)
      poOrder.ProjectID = demand.DemandProjectID;
    poOrder = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Update(poOrder);
    if (num != 0)
    {
      int? branchId1 = poOrder.BranchID;
      int? branchId2 = demandSource.BranchID;
      if (!(branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue))
      {
        poOrder.BranchID = demandSource.BranchID;
        poOrder = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Update(poOrder);
      }
    }
    return poOrder;
  }

  protected virtual PX.Objects.PO.POLine ApplyDemandToPOLine(
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    DocumentList<PX.Objects.PO.POLine> createdLines)
  {
    PX.Objects.PO.POLine poLine1 = this.FindPOLine(demand, demandSource, createdLines);
    PX.Objects.PO.POLine poLine2;
    if (poLine1 != null)
    {
      PX.Objects.PO.POLine copy = PXCache<PX.Objects.PO.POLine>.CreateCopy(PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<BqlField<PX.Objects.PO.POOrder.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POLine.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POOrder.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POLine.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) poLine1.LineNbr
      })));
      PX.Objects.PO.POLine poLine3 = copy;
      Decimal? orderQty1 = poLine3.OrderQty;
      Decimal? orderQty2 = demand.OrderQty;
      poLine3.OrderQty = orderQty1.HasValue & orderQty2.HasValue ? new Decimal?(orderQty1.GetValueOrDefault() + orderQty2.GetValueOrDefault()) : new Decimal?();
      poLine2 = ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Update(copy);
    }
    else
    {
      PX.Objects.PO.POLine poLine4 = ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Insert(this.FillNewPOLineFromDemand(new PX.Objects.PO.POLine(), demand, demandSource));
      this.CopyNoteAndFilesToNewPOLine(poLine4, demand, demandSource);
      poLine2 = PXCache<PX.Objects.PO.POLine>.CreateCopy(poLine4);
      createdLines.Add(poLine2);
    }
    GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) this.Base), (object) demand, true);
    demand.SupplyPlanID = poLine2.PlanID;
    return this.LinkPOLineToSource(poLine2, demand, demandSource);
  }

  protected virtual PX.Objects.PO.POLine FindPOLine(
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    DocumentList<PX.Objects.PO.POLine> createdLines)
  {
    List<FieldLookup> fieldLookupList = this.MakePOLineLookup(demand, demandSource);
    return createdLines.Find(fieldLookupList.ToArray());
  }

  protected virtual List<FieldLookup> MakePOLineLookup(
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    return new List<FieldLookup>()
    {
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.vendorID>((object) demand.VendorID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.vendorLocationID>((object) demand.VendorLocationID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.siteID>((object) demand.POSiteID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.inventoryID>((object) demand.InventoryID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.subItemID>((object) demand.SubItemID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.projectID>((object) demandSource.ProjectID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.taskID>((object) demandSource.TaskID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.costCodeID>((object) demandSource.CostCodeID),
      (FieldLookup) new FieldLookup<PX.Objects.PO.POLine.costCenterID>((object) demand.CostCenterID)
    };
  }

  protected virtual PX.Objects.PO.POLine FillNewPOLineFromDemand(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    poLine.VendorLocationID = demand.VendorLocationID;
    poLine.SiteID = demand.POSiteID;
    poLine.InventoryID = demand.InventoryID;
    poLine.SubItemID = demand.SubItemID;
    if (demandSource.ProjectID.HasValue)
      poLine.ProjectID = demandSource.ProjectID;
    int? nullable = demandSource.TaskID;
    if (nullable.HasValue)
      poLine.TaskID = demandSource.TaskID;
    nullable = demandSource.CostCodeID;
    if (nullable.HasValue)
      poLine.CostCodeID = demandSource.CostCodeID;
    poLine.OrderQty = demand.OrderQty;
    poLine.UOM = demand.DemandUOM;
    nullable = demand.InventoryID;
    if (nullable.HasValue && demandSource.AlternateID != null)
    {
      PXSelectBase<INItemXRef> pxSelectBase = (PXSelectBase<INItemXRef>) new FbqlSelect<SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemXRef.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemXRef.alternateID, IBqlString>.IsEqual<P.AsString>>>, INItemXRef>.View((PXGraph) this.Base);
      INItemXRef inItemXref1 = PXResultset<INItemXRef>.op_Implicit(pxSelectBase.Select(new object[2]
      {
        (object) demand.InventoryID,
        (object) demandSource.AlternateID
      }));
      if (inItemXref1 != null && inItemXref1.AlternateType == "GLBL")
      {
        if (poLine.AlternateID != null)
        {
          INItemXRef inItemXref2 = PXResultset<INItemXRef>.op_Implicit(pxSelectBase.Select(new object[2]
          {
            (object) poLine.InventoryID,
            (object) poLine.AlternateID
          }));
          if (inItemXref2 != null && inItemXref2.AlternateType == "GLBL")
            poLine.AlternateID = demandSource.AlternateID;
        }
        else
        {
          string str;
          GraphHelper.Caches<PX.Objects.PO.POLine>((PXGraph) this.Base).RaiseFieldDefaulting<string>((Expression<Func<PX.Objects.PO.POLine, string>>) (l => l.AlternateID), poLine, ref str);
          PXResultset<INItemXRef> pxResultset;
          if (str != null)
            pxResultset = pxSelectBase.Select(new object[2]
            {
              (object) demand.InventoryID,
              (object) str
            });
          else
            pxResultset = (PXResultset<INItemXRef>) null;
          INItemXRef inItemXref3 = PXResultset<INItemXRef>.op_Implicit(pxResultset);
          poLine.AlternateID = inItemXref3?.AlternateType == "0VPN" ? inItemXref3.AlternateID : demandSource.AlternateID;
        }
      }
    }
    return poLine;
  }

  protected virtual void CopyNoteAndFilesToNewPOLine(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
  }

  protected virtual PX.Objects.PO.POLine LinkPOLineToSource(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    return poLine;
  }
}
