// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUpdateStdCostProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN;

public class INUpdateStdCostProcess : PXGraph<INUpdateStdCostProcess>
{
  public INAdjustmentEntry je = PXGraph.CreateInstance<INAdjustmentEntry>();

  public DocumentList<INRegister> Adjustments { get; }

  public INUpdateStdCostProcess()
  {
    ((PXSelectBase<INSetup>) this.je.insetup).Current.RequireControlTotal = new bool?(false);
    ((PXSelectBase<INSetup>) this.je.insetup).Current.HoldEntry = new bool?(false);
    this.Adjustments = new DocumentList<INRegister>((PXGraph) this.je);
  }

  protected virtual ICollection<INCostStatus> LoadCostStatuses(INUpdateStdCostRecord stdCostRecord)
  {
    return (ICollection<INCostStatus>) ((PXGraph) this).TypedViews.GetView((BqlCommand) new SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemSite>.On<KeysRelation<CompositeKey<Field<INCostStatus.inventoryID>.IsRelatedTo<INItemSite.inventoryID>, Field<INCostStatus.costSiteID>.IsRelatedTo<INItemSite.siteID>>.WithTablesOf<INItemSite, INCostStatus>, INItemSite, INCostStatus>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.inventoryID, Equal<BqlField<INUpdateStdCostRecord.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemSite.active, IBqlBool>.IsEqual<True>>>>>, FbqlJoins.Inner<INSite>.On<INCostStatus.FK.CostSite>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSite.baseCuryID, Equal<BqlField<INUpdateStdCostRecord.curyID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCostStatus.costSiteID, Equal<BqlField<INUpdateStdCostRecord.siteID, IBqlInt>.FromCurrent>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INUpdateStdCostRecord.stdCostOverride>, Equal<False>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INUpdateStdCostRecord.pendingStdCostReset>, Equal<False>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.stdCostOverride, Equal<False>>>>>.And<BqlOperand<INItemSite.pendingStdCostReset, IBqlBool>.IsEqual<False>>>>>>>(), false).SelectMultiBound(new object[1]
    {
      (object) stdCostRecord
    }, Array.Empty<object>()).Select<object, (INCostStatus, INSite)>((Func<object, (INCostStatus, INSite)>) (x => (PXResult<INCostStatus>.op_Implicit((PXResult<INCostStatus>) x), ((PXResult) x).GetItem<INSite>()))).OrderBy<(INCostStatus, INSite), int?>((Func<(INCostStatus, INSite), int?>) (x => x.Site.BranchID)).Select<(INCostStatus, INSite), INCostStatus>((Func<(INCostStatus, INSite), INCostStatus>) (x => x.Layer)).ToArray<INCostStatus>();
  }

  protected virtual INTran PrepareTransaction(
    INCostStatus layer,
    INSite site,
    InventoryItem inventoryItem,
    Decimal? tranCost)
  {
    try
    {
      this.ValidateProjectLocation(site, inventoryItem);
    }
    catch (PXException ex)
    {
      PXProcessing<INUpdateStdCostRecord>.SetError(((Exception) ex).Message);
      return (INTran) null;
    }
    INTran inTran = new INTran();
    inTran.TranType = !(layer.LayerType == "O") ? "ASC" : "NSC";
    inTran.BranchID = site.BranchID;
    inTran.InvtAcctID = layer.AccountID;
    inTran.InvtSubID = layer.SubID;
    INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem.PostClassID);
    inTran.AcctID = INReleaseProcess.GetAccountDefaults<INPostClass.stdCstRevAcctID>((PXGraph) this.je, inventoryItem, site, postclass);
    inTran.SubID = INReleaseProcess.GetAccountDefaults<INPostClass.stdCstRevSubID>((PXGraph) this.je, inventoryItem, site, postclass);
    inTran.InventoryID = layer.InventoryID;
    inTran.SubItemID = layer.CostSubItemID;
    inTran.SiteID = layer.CostSiteID;
    inTran.Qty = new Decimal?(0M);
    inTran.TranCost = tranCost;
    return inTran;
  }

  protected virtual void SaveAdjustment()
  {
    if (((PXSelectBase<INRegister>) this.je.adjustment).Current == null || !((PXGraph) this.je).IsDirty)
      return;
    ((PXAction) this.je.Save).Press();
  }

  protected virtual INRegister AddToAdjustment(INCostStatus layer, Decimal? tranCost)
  {
    Decimal? nullable = tranCost;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return (INRegister) null;
    INSite site = INSite.PK.Find((PXGraph) this, layer.CostSiteID);
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, layer.InventoryID);
    bool flag = true;
    INRegister adjustment = this.Adjustments.Find<INRegister.branchID>((object) site.BranchID);
    if (adjustment != null)
    {
      flag = false;
      if (PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.docType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.refNbr, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INTran.siteID, IBqlInt>.IsEqual<P.AsInt>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this.je, 0, 1, new object[4]
      {
        (object) adjustment.DocType,
        (object) adjustment.RefNbr,
        (object) layer.InventoryID,
        (object) layer.CostSiteID
      })) != null)
        return (INRegister) null;
      if (((PXSelectBase<INRegister>) this.je.adjustment).Current != adjustment)
      {
        this.SaveAdjustment();
        ((PXSelectBase<INRegister>) this.je.adjustment).Current = adjustment;
      }
    }
    INTran inTran = this.PrepareTransaction(layer, site, inventoryItem, tranCost);
    if (inTran == null)
      return (INRegister) null;
    if (flag)
    {
      this.SaveAdjustment();
      INRegister instance = (INRegister) ((PXSelectBase) this.je.adjustment).Cache.CreateInstance();
      instance.BranchID = site.BranchID;
      adjustment = (INRegister) ((PXSelectBase) this.je.adjustment).Cache.Insert((object) instance);
    }
    if (((PXSelectBase<INTran>) this.je.transactions).Insert(inTran) == null)
    {
      if (flag)
        ((PXGraph) this.je).Clear();
      return (INRegister) null;
    }
    if (flag)
      this.Adjustments.Add(adjustment);
    return adjustment;
  }

  public virtual void CreateAdjustments(
    INUpdateStdCostRecord itemsite,
    Func<INCostStatus, Decimal?> getTranCost)
  {
    List<INRegister> source = new List<INRegister>();
    foreach (INCostStatus loadCostStatuse in (IEnumerable<INCostStatus>) this.LoadCostStatuses(itemsite))
    {
      Decimal? tranCost = getTranCost(loadCostStatuse);
      INRegister adjustment = this.AddToAdjustment(loadCostStatuse, tranCost);
      if (adjustment != null && !source.Contains(adjustment))
        source.Add(adjustment);
    }
    if (!source.Any<INRegister>())
      return;
    this.SaveAdjustment();
    PXProcessing<INUpdateStdCostRecord>.SetInfo(PXMessages.LocalizeFormatNoPrefixNLA("The following adjustments have been created: {0}.", new object[1]
    {
      (object) string.Join(", ", source.Select<INRegister, string>((Func<INRegister, string>) (x => x.RefNbr)))
    }));
  }

  public virtual void UpdateStdCost(INUpdateStdCostRecord itemsite)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        bool? stdCostOverride = itemsite.StdCostOverride;
        bool flag1 = false;
        if (stdCostOverride.GetValueOrDefault() == flag1 & stdCostOverride.HasValue)
        {
          bool? pendingStdCostReset = itemsite.PendingStdCostReset;
          bool flag2 = false;
          if (pendingStdCostReset.GetValueOrDefault() == flag2 & pendingStdCostReset.HasValue)
          {
            PXResultset<INItemSite> source1 = PXSelectBase<INItemSite, PXSelectJoin<INItemSite, InnerJoin<INSite, On<INItemSite.FK.Site>>, Where<INSite.baseCuryID, Equal<Required<INSite.baseCuryID>>, And<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.valMethod, Equal<INValMethod.standard>, And<INItemSite.stdCostOverride, Equal<False>>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) itemsite.CuryID,
              (object) itemsite.InventoryID
            });
            if (((IQueryable<PXResult<INItemSite>>) source1).Any<PXResult<INItemSite>>((Expression<Func<PXResult<INItemSite>, bool>>) (x => ((INItemSite) x).Active == (bool?) false)))
            {
              PXResultset<INItemSite> source2 = source1;
              Expression<Func<PXResult<INItemSite>, bool>> predicate = (Expression<Func<PXResult<INItemSite>, bool>>) (x => ((INItemSite) x).Active == (bool?) true);
              foreach (PXResult<INItemSite> pxResult in (IEnumerable<PXResult<INItemSite>>) ((IQueryable<PXResult<INItemSite>>) source2).Where<PXResult<INItemSite>>(predicate))
              {
                INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
                PXUpdateJoin<Set<INItemSite.stdCost, INItemSite.pendingStdCost, Set<INItemSite.stdCostDate, Required<INItemSite.stdCostDate>, Set<INItemSite.pendingStdCost, decimal0, Set<INItemSite.pendingStdCostDate, Null, Set<INItemSite.lastStdCost, INItemSite.stdCost>>>>>, INItemSite, InnerJoin<INSite, On<INItemSite.FK.Site>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INSite.baseCuryID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<INItemSite.stdCostOverride, IBqlBool>.IsEqual<False>>>, And<BqlOperand<INItemSite.pendingStdCostDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>, And<BqlOperand<INItemSite.pendingStdCostReset, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<INSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.Update((PXGraph) this, new object[5]
                {
                  (object) itemsite.PendingStdCostDate,
                  (object) itemsite.InventoryID,
                  (object) itemsite.CuryID,
                  (object) itemsite.PendingStdCostDate,
                  (object) inItemSite.SiteID
                });
              }
            }
            else
              PXUpdateJoin<Set<INItemSite.stdCost, INItemSite.pendingStdCost, Set<INItemSite.stdCostDate, Required<INItemSite.stdCostDate>, Set<INItemSite.pendingStdCost, decimal0, Set<INItemSite.pendingStdCostDate, Null, Set<INItemSite.lastStdCost, INItemSite.stdCost>>>>>, INItemSite, InnerJoin<INSite, On<INItemSite.FK.Site>>, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INSite.baseCuryID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<INItemSite.stdCostOverride, IBqlBool>.IsEqual<False>>>, And<BqlOperand<INItemSite.pendingStdCostDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>>.And<BqlOperand<INItemSite.pendingStdCostReset, IBqlBool>.IsEqual<False>>>>.Update((PXGraph) this, new object[4]
              {
                (object) itemsite.PendingStdCostDate,
                (object) itemsite.InventoryID,
                (object) itemsite.CuryID,
                (object) itemsite.PendingStdCostDate
              });
            if (itemsite.CuryID == CurrencyCollection.GetBaseCurrency()?.CuryID)
              PXDatabase.Update<InventoryItem>(new PXDataFieldParam[7]
              {
                (PXDataFieldParam) new PXDataFieldAssign("StdCost", (PXDbType) 5, (object) itemsite.PendingStdCost),
                (PXDataFieldParam) new PXDataFieldAssign("StdCostDate", (PXDbType) 4, (object) itemsite.PendingStdCostDate),
                (PXDataFieldParam) new PXDataFieldAssign("PendingStdCost", (PXDbType) 5, (object) 0M),
                (PXDataFieldParam) new PXDataFieldAssign("PendingStdCostDate", (PXDbType) 4, (object) null),
                (PXDataFieldParam) new PXDataFieldAssign("LastStdCost", (PXDbType) 5, (object) itemsite.StdCost),
                (PXDataFieldParam) new PXDataFieldRestrict("InventoryID", (PXDbType) 8, (object) itemsite.InventoryID),
                (PXDataFieldParam) new PXDataFieldRestrict("PendingStdCostDate", (PXDbType) 4, new int?(4), (object) itemsite.PendingStdCostDate, (PXComp) 5)
              });
            PXDatabase.Update<InventoryItemCurySettings>(new PXDataFieldParam[8]
            {
              (PXDataFieldParam) new PXDataFieldAssign("StdCost", (PXDbType) 5, (object) itemsite.PendingStdCost),
              (PXDataFieldParam) new PXDataFieldAssign("StdCostDate", (PXDbType) 4, (object) itemsite.PendingStdCostDate),
              (PXDataFieldParam) new PXDataFieldAssign("PendingStdCost", (PXDbType) 5, (object) 0M),
              (PXDataFieldParam) new PXDataFieldAssign("PendingStdCostDate", (PXDbType) 4, (object) null),
              (PXDataFieldParam) new PXDataFieldAssign("LastStdCost", (PXDbType) 5, (object) itemsite.StdCost),
              (PXDataFieldParam) new PXDataFieldRestrict("InventoryID", (PXDbType) 8, (object) itemsite.InventoryID),
              (PXDataFieldParam) new PXDataFieldRestrict("CuryID", (PXDbType) 22, new int?(5), (object) itemsite.CuryID),
              (PXDataFieldParam) new PXDataFieldRestrict("PendingStdCostDate", (PXDbType) 4, new int?(4), (object) itemsite.PendingStdCostDate, (PXComp) 5)
            });
            goto label_19;
          }
        }
        List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>()
        {
          (PXDataFieldParam) new PXDataFieldAssign("StdCost", (PXDbType) 5, (object) itemsite.PendingStdCost),
          (PXDataFieldParam) new PXDataFieldAssign("StdCostDate", (PXDbType) 4, (object) (itemsite.PendingStdCostDate ?? ((PXGraph) this).Accessinfo.BusinessDate)),
          (PXDataFieldParam) new PXDataFieldAssign("PendingStdCost", (PXDbType) 5, (object) 0M),
          (PXDataFieldParam) new PXDataFieldAssign("PendingStdCostDate", (PXDbType) 4, (object) null),
          (PXDataFieldParam) new PXDataFieldAssign("PendingStdCostReset", (PXDbType) 2, (object) false),
          (PXDataFieldParam) new PXDataFieldAssign("LastStdCost", (PXDbType) 5, (object) itemsite.StdCost),
          (PXDataFieldParam) new PXDataFieldRestrict("InventoryID", (PXDbType) 8, (object) itemsite.InventoryID),
          (PXDataFieldParam) new PXDataFieldRestrict("SiteID", (PXDbType) 8, (object) itemsite.SiteID)
        };
        bool? pendingStdCostReset1 = itemsite.PendingStdCostReset;
        bool flag3 = false;
        if (pendingStdCostReset1.GetValueOrDefault() == flag3 & pendingStdCostReset1.HasValue)
          pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict("PendingStdCostDate", (PXDbType) 4, new int?(4), (object) itemsite.PendingStdCostDate, (PXComp) 5));
        PXDatabase.Update<INItemSite>(pxDataFieldParamList.ToArray());
label_19:
        this.CreateAdjustments(itemsite, (Func<INCostStatus, Decimal?>) (layer =>
        {
          Decimal? qtyOnHand = layer.QtyOnHand;
          Decimal? pendingStdCost = itemsite.PendingStdCost;
          Decimal? nullable = qtyOnHand.HasValue & pendingStdCost.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() * pendingStdCost.GetValueOrDefault()) : new Decimal?();
          Decimal num = PXDBCurrencyAttribute.BaseRound((PXGraph) this, nullable.Value);
          Decimal? totalCost = layer.TotalCost;
          if (totalCost.HasValue)
            return new Decimal?(num - totalCost.GetValueOrDefault());
          nullable = new Decimal?();
          return nullable;
        }));
        transactionScope.Complete();
      }
    }
  }

  public virtual void RevalueInventory(INUpdateStdCostRecord itemsite)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.CreateAdjustments(itemsite, (Func<INCostStatus, Decimal?>) (layer =>
        {
          Decimal? qtyOnHand = layer.QtyOnHand;
          Decimal? stdCost = itemsite.StdCost;
          Decimal? nullable = qtyOnHand.HasValue & stdCost.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() * stdCost.GetValueOrDefault()) : new Decimal?();
          Decimal num = PXDBCurrencyAttribute.BaseRound((PXGraph) this, nullable.Value);
          Decimal? totalCost = layer.TotalCost;
          if (totalCost.HasValue)
            return new Decimal?(num - totalCost.GetValueOrDefault());
          nullable = new Decimal?();
          return nullable;
        }));
        transactionScope.Complete();
      }
    }
  }

  /// <summary>
  /// Validates that there are no Quantities on hand on any Project Locations for the given Warehouse.
  /// </summary>
  /// <param name="site">Warehouse</param>
  public virtual void ValidateProjectLocation(INSite site, InventoryItem item)
  {
    List<PXResult<INLocationStatusByCostCenter>> list = ((IEnumerable<PXResult<INLocationStatusByCostCenter>>) ((PXSelectBase<INLocationStatusByCostCenter>) new PXSelectJoin<INLocationStatusByCostCenter, InnerJoin<INLocation, On<INLocationStatusByCostCenter.FK.Location>>, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.isCosted, Equal<True>, And<INLocation.taskID, IsNotNull, And<INLocationStatusByCostCenter.inventoryID, Equal<Required<INLocationStatusByCostCenter.inventoryID>>, And<INLocationStatusByCostCenter.qtyOnHand, NotEqual<decimal0>>>>>>>((PXGraph) this)).SelectWindowed(0, 1, new object[2]
    {
      (object) site.SiteID,
      (object) item.InventoryID
    })).ToList<PXResult<INLocationStatusByCostCenter>>();
    if (list.Count == 1)
    {
      INLocation inLocation = PXResult<INLocationStatusByCostCenter, INLocation>.op_Implicit((PXResult<INLocationStatusByCostCenter, INLocation>) list.First<PXResult<INLocationStatusByCostCenter>>());
      PMProject pmProject = PMProject.PK.Find((PXGraph) this, inLocation.ProjectID);
      throw new PXException("Inventory revaluation cannot be performed because some of the stock items with pending standard costs are located in the {0} warehouse location which is linked to the {1} project. To perform the inventory revaluation, transfer these stock items to a warehouse location that is not linked to any project.", new object[2]
      {
        (object) inLocation.LocationCD,
        (object) pmProject?.ContractCD
      });
    }
  }

  public virtual void ReleaseAdjustment()
  {
    if (!this.Adjustments.Any<INRegister>())
      return;
    INDocumentRelease.ReleaseDoc((List<INRegister>) this.Adjustments, false);
  }
}
