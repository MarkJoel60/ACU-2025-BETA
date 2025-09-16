// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Description;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

public class INItemSiteMaint : PXGraph<
#nullable disable
INItemSiteMaint, INItemSite>
{
  public PXFilter<PX.Objects.AP.Vendor> _Vendor_;
  public PXFilter<EPEmployee> _Employee_;
  public PXSelectJoin<INItemSite, LeftJoin<INSite, On<INItemSite.FK.Site>, LeftJoin<InventoryItem, On<INItemSite.FK.InventoryItem>>>, Where<INSite.siteID, IsNull, Or2<Match<INSite, Current<AccessInfo.userName>>, And<INSite.active, Equal<True>>>>, OrderBy<Asc<InventoryItem.inventoryCD>>> itemsiterecord;
  public PXSelectJoin<INItemSite, InnerJoin<InventoryItem, On<INItemSite.FK.InventoryItem>>, Where<INItemSite.inventoryID, Equal<Current<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Current<INItemSite.siteID>>>>> itemsitesettings;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> sitestatusbycostcenter;
  public PXSetup<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>> itemrecord;
  public PXSetup<INPostClass, Where<INPostClass.postClassID, Equal<Current<InventoryItem.postClassID>>>> postclass;
  public PXSetup<INLotSerClass, Where<INLotSerClass.lotSerClassID, Equal<Current<InventoryItem.lotSerClassID>>>> lotserclass;
  public PXSetup<INSite, Where<INSite.siteID, Equal<Current<INItemSite.siteID>>>> insite;
  public PXSelect<INItemSiteReplenishment, Where<INItemSiteReplenishment.siteID, Equal<Current<INItemSite.siteID>>, And<INItemSiteReplenishment.inventoryID, Equal<Current<INItemSite.inventoryID>>>>> subitemrecords;
  public PXSetup<INSetup> insetup;
  public PXSelectJoin<POVendorInventory, InnerJoin<InventoryItem, On<POVendorInventory.FK.InventoryItem>>> PreferredVendorItem;
  public PXSelect<InventoryItemCurySettings, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InventoryItemCurySettings.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INItemSite.inventoryID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  InventoryItemCurySettings.curyID, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>> ItemCurySettings;
  public PXSelect<ItemStats> itemstats;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost> itemcost;
  public PXAction<INItemSite> updateReplenishment;

  protected IEnumerable preferredVendorItem()
  {
    foreach (object obj in PXSelectBase<POVendorInventory, PXSelectJoin<POVendorInventory, InnerJoin<InventoryItem, On<POVendorInventory.FK.InventoryItem>>, Where<POVendorInventory.inventoryID, Equal<Current<INItemSite.inventoryID>>, And<POVendorInventory.vendorID, Equal<Current<INItemSite.preferredVendorID>>, And<POVendorInventory.subItemID, Equal<Current<InventoryItem.defaultSubItemID>>, And<POVendorInventory.purchaseUnit, Equal<InventoryItem.purchaseUnit>, And<Where<POVendorInventory.vendorLocationID, Equal<Current<INItemSite.preferredVendorLocationID>>, Or<POVendorInventory.vendorLocationID, IsNull>>>>>>>, OrderBy<Desc<POVendorInventory.vendorLocationID, Asc<POVendorInventory.recordID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()))
      yield return obj;
  }

  public INItemSiteMaint()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    ((PXSelectBase) this.PreferredVendorItem).Cache.AllowUpdate = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.PreferredVendorItem).Cache, (string) null, false);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.replenishment>() && PXAccess.FeatureInstalled<FeaturesSet.subItem>();
    ((PXSelectBase) this.subitemrecords).AllowSelect = flag;
    PXUIFieldAttribute.SetVisible<INItemSite.subItemOverride>(((PXSelectBase) this.itemsiterecord).Cache, (object) null, flag);
  }

  protected virtual List<KeyValuePair<string, List<FieldInfo>>> AdjustApiScript(
    List<KeyValuePair<string, List<FieldInfo>>> fieldsByView)
  {
    List<KeyValuePair<string, List<FieldInfo>>> keyValuePairList = ((PXGraph) this).AdjustApiScript(fieldsByView);
    foreach (KeyValuePair<string, List<FieldInfo>> keyValuePair in keyValuePairList)
    {
      if (keyValuePair.Key.Contains("itemsitesettings"))
      {
        List<FieldInfo> source = keyValuePair.Value;
        if (source != null)
        {
          FieldInfo fieldInfo1 = source.FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (x => x.FieldName == "ServiceLevelPct"));
          FieldInfo fieldInfo2 = source.FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (x => x.FieldName == "SafetyStock"));
          FieldInfo fieldInfo3 = source.FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (x => x.FieldName == "MinQty"));
          FieldInfo fieldInfo4 = source.FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (x => x.FieldName == "MaxQty"));
          if (fieldInfo1 != null)
          {
            source.Remove(fieldInfo1);
            source.Add(fieldInfo1);
          }
          if (fieldInfo2 != null)
          {
            source.Remove(fieldInfo2);
            source.Add(fieldInfo2);
          }
          if (fieldInfo3 != null)
          {
            source.Remove(fieldInfo3);
            source.Add(fieldInfo3);
          }
          if (fieldInfo4 != null)
          {
            source.Remove(fieldInfo4);
            source.Add(fieldInfo4);
          }
        }
      }
    }
    return keyValuePairList;
  }

  protected virtual void INItemSite_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    INItemSite row = (INItemSite) e.Row;
    if (row == null)
      return;
    int? nullable = row.InventoryID;
    if (!nullable.HasValue)
      return;
    nullable = row.SiteID;
    if (!nullable.HasValue)
      return;
    InventoryItem current = ((PXSelectBase<InventoryItem>) this.itemrecord).Current;
    if (((PXSelectBase<InventoryItem>) this.itemrecord).Current != null)
    {
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, current.InventoryID, ((PXSelectBase<INSite>) this.insite).Current.BaseCuryID);
      INItemSiteMaint.DefaultItemSiteByItem((PXGraph) this, row, current, ((PXSelectBase<INSite>) this.insite).Current, ((PXSelectBase<INPostClass>) this.postclass).Current, itemCurySettings);
    }
    ((PXSelectBase) this.itemrecord).Cache.IsDirty = true;
  }

  public static void DefaultItemSiteByItem(
    PXGraph graph,
    INItemSite itemsite,
    InventoryItem item,
    INSite site,
    INPostClass postclass,
    InventoryItemCurySettings itemCurySettings)
  {
    if (item == null)
      return;
    INItemSite inItemSite1 = itemsite;
    Decimal? nullable1 = (Decimal?) itemCurySettings?.PendingStdCost;
    Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
    inItemSite1.PendingStdCost = nullable2;
    itemsite.PendingStdCostDate = (DateTime?) itemCurySettings?.PendingStdCostDate;
    INItemSite inItemSite2 = itemsite;
    Decimal? nullable3;
    if (itemCurySettings == null)
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = itemCurySettings.StdCost;
    nullable1 = nullable3;
    Decimal? nullable4 = new Decimal?(nullable1.GetValueOrDefault());
    inItemSite2.StdCost = nullable4;
    itemsite.StdCostDate = (DateTime?) itemCurySettings?.StdCostDate;
    INItemSite inItemSite3 = itemsite;
    Decimal? nullable5;
    if (itemCurySettings == null)
    {
      nullable1 = new Decimal?();
      nullable5 = nullable1;
    }
    else
      nullable5 = itemCurySettings.LastStdCost;
    nullable1 = nullable5;
    Decimal? nullable6 = new Decimal?(nullable1.GetValueOrDefault());
    inItemSite3.LastStdCost = nullable6;
    INItemSite inItemSite4 = itemsite;
    Decimal? nullable7;
    if (itemCurySettings == null)
    {
      nullable1 = new Decimal?();
      nullable7 = nullable1;
    }
    else
      nullable7 = itemCurySettings.BasePrice;
    nullable1 = nullable7;
    Decimal? nullable8 = new Decimal?(nullable1.GetValueOrDefault());
    inItemSite4.BasePrice = nullable8;
    itemsite.MarkupPct = item.MarkupPct;
    INItemSite inItemSite5 = itemsite;
    Decimal? nullable9;
    if (itemCurySettings == null)
    {
      nullable1 = new Decimal?();
      nullable9 = nullable1;
    }
    else
      nullable9 = itemCurySettings.RecPrice;
    nullable1 = nullable9;
    Decimal? nullable10 = new Decimal?(nullable1.GetValueOrDefault());
    inItemSite5.RecPrice = nullable10;
    itemsite.ABCCodeID = item.ABCCodeID;
    itemsite.ABCCodeIsFixed = item.ABCCodeIsFixed;
    itemsite.MovementClassID = item.MovementClassID;
    itemsite.MovementClassIsFixed = item.MovementClassIsFixed;
    itemsite.PreferredVendorID = (int?) itemCurySettings?.PreferredVendorID;
    itemsite.PreferredVendorLocationID = (int?) itemCurySettings?.PreferredVendorLocationID;
    itemsite.ReplenishmentClassID = site?.ReplenishmentClassID;
    itemsite.DfltReceiptLocationID = site.ReceiptLocationID;
    itemsite.DfltShipLocationID = site.ShipLocationID;
    itemsite.PlanningMethod = item.PlanningMethod;
    if (itemCurySettings == null)
    {
      InventoryItemCurySettings itemCurySettings1 = PXResultset<InventoryItemCurySettings>.op_Implicit(PXSelectBase<InventoryItemCurySettings, PXViewOf<InventoryItemCurySettings>.BasedOn<SelectFromBase<InventoryItemCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItemCurySettings.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
      {
        (object) item.InventoryID
      }));
      if (itemCurySettings1 != null && itemCurySettings1.PreferredVendorID.HasValue)
      {
        PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find(graph, itemCurySettings1.PreferredVendorID);
        if (vendor != null && vendor.BaseCuryID == null)
        {
          itemsite.PreferredVendorID = itemCurySettings1.PreferredVendorID;
          itemsite.PreferredVendorLocationID = itemCurySettings1.PreferredVendorLocationID;
        }
      }
    }
    INItemSiteMaint.DefaultItemReplenishment(graph, itemsite);
    INItemSiteMaint.DefaultSubItemReplenishment(graph, itemsite);
  }

  public static void DefaultInvtAcctSub(
    PXGraph graph,
    INItemSite itemsite,
    InventoryItem item,
    INSite site,
    INPostClass postclass)
  {
    if (site != null && site.OverrideInvtAccSub.GetValueOrDefault())
    {
      itemsite.InvtAcctID = site.InvtAcctID;
      itemsite.InvtSubID = site.InvtSubID;
    }
    else
    {
      if (postclass == null)
        return;
      itemsite.InvtAcctID = INReleaseProcess.GetAccountDefaults<INPostClass.invtAcctID>(graph, item, site, postclass);
      itemsite.InvtSubID = INReleaseProcess.GetAccountDefaults<INPostClass.invtSubID>(graph, item, site, postclass);
    }
  }

  public static bool DefaultItemReplenishment(PXGraph graph, INItemSite itemsite)
  {
    if (itemsite == null || itemsite.PlanningMethod != "R")
      return false;
    INSite inSite = INSite.PK.Find(graph, itemsite.SiteID);
    INItemRep rep = PXResultset<INItemRep>.op_Implicit(PXSelectBase<INItemRep, PXSelect<INItemRep, Where<INItemRep.inventoryID, Equal<Current<INItemSite.inventoryID>>, And<INItemRep.curyID, Equal<Current<INSite.baseCuryID>>, And<INItemRep.replenishmentClassID, Equal<Current<INItemSite.replenishmentClassID>>>>>>.Config>.SelectSingleBound(graph, new object[2]
    {
      (object) itemsite,
      (object) inSite
    }, Array.Empty<object>())) ?? new INItemRep();
    return INItemSiteMaint.UpdateItemSiteReplenishment(itemsite, rep);
  }

  public static bool UpdateItemSiteReplenishment(INItemSite itemsite, INItemRep rep)
  {
    if (!itemsite.ReplenishmentPolicyOverride.GetValueOrDefault())
    {
      itemsite.ReplenishmentPolicyID = rep.ReplenishmentPolicyID;
      itemsite.ReplenishmentMethod = rep.ReplenishmentMethod ?? "N";
      itemsite.ReplenishmentSource = rep.ReplenishmentSource ?? "N";
      itemsite.ReplenishmentSourceSiteID = rep.ReplenishmentSourceSiteID;
    }
    if (itemsite.ReplenishmentMethod == "N")
    {
      itemsite.MaxShelfLifeOverride = new bool?(false);
      itemsite.LaunchDateOverride = new bool?(false);
      itemsite.TerminationDateOverride = new bool?(false);
      itemsite.ServiceLevelOverride = new bool?(false);
      itemsite.SafetyStockOverride = new bool?(false);
      itemsite.MinQtyOverride = new bool?(false);
      itemsite.MaxQtyOverride = new bool?(false);
    }
    if (!itemsite.MaxShelfLifeOverride.GetValueOrDefault())
      itemsite.MaxShelfLife = rep.MaxShelfLife;
    if (!itemsite.LaunchDateOverride.GetValueOrDefault())
      itemsite.LaunchDate = rep.LaunchDate;
    if (!itemsite.TerminationDateOverride.GetValueOrDefault())
      itemsite.TerminationDate = rep.TerminationDate;
    if (!itemsite.SafetyStockOverride.GetValueOrDefault())
      itemsite.SafetyStock = rep.SafetyStock;
    if (!itemsite.ServiceLevelOverride.GetValueOrDefault())
      itemsite.ServiceLevel = rep.ServiceLevel;
    if (!itemsite.MinQtyOverride.GetValueOrDefault())
      itemsite.MinQty = rep.MinQty;
    if (!itemsite.MaxQtyOverride.GetValueOrDefault())
      itemsite.MaxQty = rep.MaxQty;
    if (!itemsite.TransferERQOverride.GetValueOrDefault())
      itemsite.TransferERQ = rep.TransferERQ;
    return !itemsite.ReplenishmentPolicyOverride.GetValueOrDefault() || !itemsite.MaxShelfLifeOverride.GetValueOrDefault() || !itemsite.LaunchDateOverride.GetValueOrDefault() || !itemsite.TerminationDateOverride.GetValueOrDefault() || !itemsite.SafetyStockOverride.GetValueOrDefault() || !itemsite.ServiceLevelOverride.GetValueOrDefault() || !itemsite.MinQtyOverride.GetValueOrDefault() || !itemsite.MaxQtyOverride.GetValueOrDefault() || !itemsite.TransferERQOverride.GetValueOrDefault();
  }

  public static void DefaultSubItemReplenishment(PXGraph graph, INItemSite itemsite)
  {
    if (itemsite == null)
      return;
    foreach (PXResult<INItemSiteReplenishment> pxResult in PXSelectBase<INItemSiteReplenishment, PXSelect<INItemSiteReplenishment, Where<INItemSiteReplenishment.siteID, Equal<Current<INItemSite.siteID>>, And<INItemSiteReplenishment.inventoryID, Equal<Current<INItemSite.inventoryID>>>>>.Config>.SelectMultiBound(graph, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
    {
      INItemSiteReplenishment siteReplenishment = PXResult<INItemSiteReplenishment>.op_Implicit(pxResult);
      graph.Caches[typeof (INItemSiteReplenishment)].Delete((object) siteReplenishment);
    }
    foreach (PXResult<INSubItemRep> pxResult in PXSelectBase<INSubItemRep, PXSelect<INSubItemRep, Where<INSubItemRep.inventoryID, Equal<Current<INItemSite.inventoryID>>, And<INSubItemRep.replenishmentClassID, Equal<Current<INItemSite.replenishmentClassID>>>>>.Config>.Select(graph, new object[1]
    {
      (object) itemsite.InventoryID
    }))
    {
      INSubItemRep inSubItemRep = PXResult<INSubItemRep>.op_Implicit(pxResult);
      graph.Caches[typeof (INItemSiteReplenishment)].Insert((object) new INItemSiteReplenishment()
      {
        InventoryID = inSubItemRep.InventoryID,
        SiteID = itemsite.SiteID,
        SubItemID = inSubItemRep.SubItemID,
        SafetyStock = inSubItemRep.SafetyStock,
        MinQty = inSubItemRep.MinQty,
        MaxQty = inSubItemRep.MaxQty,
        TransferERQ = inSubItemRep.TransferERQ,
        ItemStatus = inSubItemRep.ItemStatus
      });
    }
    if (!graph.Caches[typeof (INItemSiteReplenishment)].IsDirty)
      return;
    graph.Views.Caches.Add(typeof (INItemSiteReplenishment));
  }

  protected virtual void INItemSite_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    INItemSite row = (INItemSite) e.Row;
    INItemSite oldRow = (INItemSite) e.OldRow;
    InventoryItem current = ((PXSelectBase<InventoryItem>) this.itemrecord).Current;
    if (!row.InventoryID.HasValue)
      sender.RaiseExceptionHandling<INItemSite.inventoryID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<INItemSite.inventoryID>(sender)
      }));
    else if (!row.SiteID.HasValue)
    {
      sender.RaiseExceptionHandling<INItemSite.siteID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<INItemSite.siteID>(sender)
      }));
    }
    else
    {
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(sender.Graph, current.InventoryID, ((PXSelectBase<INSite>) this.insite).Current.BaseCuryID);
      bool? nullable1 = oldRow.StdCostOverride;
      int num1;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.StdCostOverride;
        bool flag = false;
        num1 = nullable1.GetValueOrDefault() == flag & nullable1.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      bool flag1 = num1 != 0;
      nullable1 = oldRow.StdCostOverride;
      bool flag2 = false;
      int num2;
      if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
      {
        nullable1 = row.StdCostOverride;
        num2 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
      bool flag3 = num2 != 0;
      DateTime? nullable2;
      Decimal? nullable3;
      if (flag1)
      {
        if (itemCurySettings != null)
        {
          nullable2 = itemCurySettings.PendingStdCostDate;
          if (nullable2.HasValue)
            goto label_14;
        }
        Decimal? stdCost = (Decimal?) itemCurySettings?.StdCost;
        nullable3 = row.StdCost;
        if (!(stdCost.GetValueOrDefault() == nullable3.GetValueOrDefault() & stdCost.HasValue == nullable3.HasValue))
          goto label_21;
label_14:
        INItemSite inItemSite1 = row;
        Decimal? nullable4;
        if (itemCurySettings == null)
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = itemCurySettings.PendingStdCost;
        inItemSite1.PendingStdCost = nullable4;
        INItemSite inItemSite2 = row;
        DateTime? nullable5;
        if (itemCurySettings == null)
        {
          nullable2 = new DateTime?();
          nullable5 = nullable2;
        }
        else
          nullable5 = itemCurySettings.PendingStdCostDate;
        inItemSite2.PendingStdCostDate = nullable5;
        goto label_28;
      }
label_21:
      if (flag1)
      {
        INItemSite inItemSite3 = row;
        Decimal? nullable6;
        if (itemCurySettings == null)
        {
          nullable3 = new Decimal?();
          nullable6 = nullable3;
        }
        else
          nullable6 = itemCurySettings.StdCost;
        nullable3 = nullable6;
        Decimal? nullable7 = new Decimal?(nullable3.GetValueOrDefault());
        inItemSite3.PendingStdCost = nullable7;
        INItemSite inItemSite4 = row;
        nullable2 = new DateTime?();
        DateTime? nullable8 = nullable2;
        inItemSite4.PendingStdCostDate = nullable8;
        row.PendingStdCostReset = new bool?(true);
      }
      else if (flag3)
        row.PendingStdCostReset = new bool?(false);
label_28:
      nullable1 = oldRow.BasePriceOverride;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.BasePriceOverride;
        bool flag4 = false;
        if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
        {
          INItemSite inItemSite = row;
          Decimal? nullable9;
          if (itemCurySettings == null)
          {
            nullable3 = new Decimal?();
            nullable9 = nullable3;
          }
          else
            nullable9 = itemCurySettings.BasePrice;
          nullable3 = nullable9;
          Decimal? nullable10 = new Decimal?(nullable3.GetValueOrDefault());
          inItemSite.BasePrice = nullable10;
        }
      }
      nullable1 = oldRow.ABCCodeOverride;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.ABCCodeOverride;
        bool flag5 = false;
        if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
        {
          row.ABCCodeID = current.ABCCodeID;
          row.ABCCodeIsFixed = current.ABCCodeIsFixed;
        }
      }
      nullable1 = oldRow.MovementClassOverride;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.MovementClassOverride;
        bool flag6 = false;
        if (nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue)
        {
          row.ABCCodeID = current.MovementClassID;
          row.ABCCodeIsFixed = current.MovementClassIsFixed;
        }
      }
      nullable1 = oldRow.RecPriceOverride;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.RecPriceOverride;
        bool flag7 = false;
        if (nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue)
        {
          INItemSite inItemSite = row;
          Decimal? nullable11;
          if (itemCurySettings == null)
          {
            nullable3 = new Decimal?();
            nullable11 = nullable3;
          }
          else
            nullable11 = itemCurySettings.RecPrice;
          inItemSite.RecPrice = nullable11;
        }
      }
      nullable1 = oldRow.MarkupPctOverride;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.MarkupPctOverride;
        bool flag8 = false;
        if (nullable1.GetValueOrDefault() == flag8 & nullable1.HasValue)
          row.MarkupPct = current.MarkupPct;
      }
      nullable1 = oldRow.PreferredVendorOverride;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.PreferredVendorOverride;
        bool flag9 = false;
        if (nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue)
        {
          row.PreferredVendorID = itemCurySettings.PreferredVendorID;
          row.PreferredVendorLocationID = itemCurySettings.PreferredVendorLocationID;
          this.ClearInsertedVendorInventories();
        }
      }
      INItemSiteMaint.DefaultItemReplenishment((PXGraph) this, row);
      foreach (ItemStats itemStats in ((PXSelectBase) this.itemstats).Cache.Inserted)
        ((PXSelectBase) this.itemstats).Cache.Delete((object) itemStats);
      int? nullable12 = row.PreferredVendorID;
      if (!nullable12.HasValue)
      {
        INItemSite inItemSite = row;
        nullable12 = new int?();
        int? nullable13 = nullable12;
        inItemSite.PreferredVendorLocationID = nullable13;
      }
      DateTime lastCostTime = INReleaseProcess.GetLastCostTime(((PXSelectBase) this.itemstats).Cache);
      if (row.LastCostDate.HasValue)
      {
        Decimal? lastCost = row.LastCost;
        if (lastCost.HasValue)
        {
          lastCost = row.LastCost;
          Decimal num3 = 0M;
          if (!(lastCost.GetValueOrDefault() == num3 & lastCost.HasValue))
          {
            ItemStats itemStats1 = new ItemStats();
            itemStats1.InventoryID = row.InventoryID;
            itemStats1.SiteID = row.SiteID;
            ItemStats itemStats2 = ((PXSelectBase<ItemStats>) this.itemstats).Insert(itemStats1);
            itemStats2.LastCost = row.LastCost;
            itemStats2.LastCostDate = new DateTime?(lastCostTime);
            foreach (PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost in ((PXSelectBase) this.itemcost).Cache.Inserted)
              ((PXSelectBase) this.itemstats).Cache.Delete((object) itemCost);
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost1 = new PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost();
            itemCost1.InventoryID = row.InventoryID;
            itemCost1.CuryID = ((PXSelectBase<INSite>) this.insite).Current.BaseCuryID;
            PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost itemCost2 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemCost>) this.itemcost).Insert(itemCost1);
            itemCost2.LastCost = row.LastCost;
            itemCost2.LastCostDate = new DateTime?(lastCostTime);
          }
        }
      }
      bool? nullable14;
      if (oldRow.ReplenishmentClassID != row.ReplenishmentClassID)
      {
        nullable14 = row.SubItemOverride;
        if (!nullable14.GetValueOrDefault())
          goto label_78;
      }
      nullable14 = oldRow.SubItemOverride;
      if (nullable14.GetValueOrDefault())
      {
        nullable14 = row.SubItemOverride;
        bool flag10 = false;
        if (!(nullable14.GetValueOrDefault() == flag10 & nullable14.HasValue))
          goto label_79;
      }
      else
        goto label_79;
label_78:
      INItemSiteMaint.DefaultSubItemReplenishment((PXGraph) this, row);
label_79:
      nullable14 = row.PreferredVendorOverride;
      if (!nullable14.GetValueOrDefault())
        return;
      nullable12 = current.DefaultSubItemID;
      if (!nullable12.HasValue)
        return;
      nullable12 = row.PreferredVendorID;
      int? preferredVendorId = oldRow.PreferredVendorID;
      if (nullable12.GetValueOrDefault() == preferredVendorId.GetValueOrDefault() & nullable12.HasValue == preferredVendorId.HasValue)
      {
        int? vendorLocationId = row.PreferredVendorLocationID;
        nullable12 = oldRow.PreferredVendorLocationID;
        if (vendorLocationId.GetValueOrDefault() == nullable12.GetValueOrDefault() & vendorLocationId.HasValue == nullable12.HasValue)
          return;
      }
      nullable12 = row.PreferredVendorID;
      if (!nullable12.HasValue)
      {
        this.ClearInsertedVendorInventories();
      }
      else
      {
        if (PXResultset<POVendorInventory>.op_Implicit(PXSelectBase<POVendorInventory, PXSelect<POVendorInventory, Where<POVendorInventory.inventoryID, Equal<Current<INItemSite.inventoryID>>, And<POVendorInventory.subItemID, Equal<Current<InventoryItem.defaultSubItemID>>, And<POVendorInventory.purchaseUnit, Equal<Current<InventoryItem.purchaseUnit>>, And<POVendorInventory.vendorID, Equal<Current<INItemSite.preferredVendorID>>, And<Where2<Where<Current<INItemSite.preferredVendorLocationID>, IsNotNull, And<POVendorInventory.vendorLocationID, Equal<Current<INItemSite.preferredVendorLocationID>>>>, Or<Where<Current<INItemSite.preferredVendorLocationID>, IsNull, And<POVendorInventory.vendorLocationID, IsNull>>>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
        {
          (object) row,
          (object) current
        }, Array.Empty<object>())) != null)
          return;
        this.ClearInsertedVendorInventories();
        ((PXSelectBase<POVendorInventory>) this.PreferredVendorItem).Insert(new POVendorInventory()
        {
          InventoryID = current.InventoryID,
          SubItemID = current.DefaultSubItemID,
          PurchaseUnit = current.PurchaseUnit,
          VendorID = row.PreferredVendorID,
          VendorLocationID = row.PreferredVendorLocationID
        });
      }
    }
  }

  private void ClearInsertedVendorInventories()
  {
    foreach (POVendorInventory poVendorInventory in ((PXSelectBase) this.PreferredVendorItem).Cache.Inserted)
      ((PXSelectBase) this.PreferredVendorItem).Cache.Delete((object) poVendorInventory);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemSite, INItemSite.dfltReceiptLocationID> e)
  {
    this.UpdateItemDefaultLocation<InventoryItemCurySettings.dfltReceiptLocationID, INItemSite.dfltReceiptLocationID>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemSite, INItemSite.dfltPutawayLocationID> e)
  {
    this.UpdateItemDefaultLocation<InventoryItemCurySettings.dfltPutawayLocationID, INItemSite.dfltPutawayLocationID>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemSite, INItemSite.dfltShipLocationID> e)
  {
    this.UpdateItemDefaultLocation<InventoryItemCurySettings.dfltShipLocationID, INItemSite.dfltShipLocationID>(e.Row);
  }

  protected virtual void UpdateItemDefaultLocation<TSourceField, TDestinationField>(
    INItemSite itemSite)
    where TSourceField : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TSourceField>
    where TDestinationField : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TDestinationField>
  {
    InventoryItemCurySettings itemCurySettings = ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).SelectSingle(new object[1]
    {
      (object) ((PXSelectBase<INSite>) this.insite).Current?.BaseCuryID
    });
    if (itemCurySettings == null || itemSite == null)
      return;
    int? siteId = itemSite.SiteID;
    int? dfltSiteId = itemCurySettings.DfltSiteID;
    if (!(siteId.GetValueOrDefault() == dfltSiteId.GetValueOrDefault() & siteId.HasValue == dfltSiteId.HasValue))
      return;
    ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).SetValueExt<TDestinationField>(itemCurySettings, ((PXCache) GraphHelper.Caches<INItemSite>((PXGraph) this)).GetValue<TSourceField>((object) itemSite));
    ((PXSelectBase<InventoryItemCurySettings>) this.ItemCurySettings).Update(itemCurySettings);
  }

  protected virtual void INItemSite_LastCost_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ((INItemSite) e.Row).LastCostDate = new DateTime?(DateTime.Now);
  }

  protected virtual void INItemSite_ReplenishmentSource_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is INItemSite row) || !(row.ReplenishmentSource == "O") && !(row.ReplenishmentSource == "D"))
      return;
    sender.SetValueExt<INItemSite.replenishmentMethod>((object) row, (object) "N");
  }

  protected virtual void INItemSite_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INItemSite row1 = (INItemSite) e.Row;
    if (row1 == null)
      return;
    bool flag1 = e.Row != null && INReplenishmentSource.IsTransfer(row1.ReplenishmentSource);
    bool flag2 = e.Row != null && row1.ReplenishmentMethod == "F";
    PXCache pxCache1 = sender;
    object row2 = e.Row;
    bool? nullable1;
    int num1;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).StdCostOverride;
      num1 = nullable1.Value ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.pendingStdCost>(pxCache1, row2, num1 != 0);
    PXCache pxCache2 = sender;
    object row3 = e.Row;
    int num2;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).StdCostOverride;
      num2 = nullable1.Value ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.pendingStdCostDate>(pxCache2, row3, num2 != 0);
    PXCache pxCache3 = sender;
    object row4 = e.Row;
    int num3;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).BasePriceOverride;
      num3 = nullable1.Value ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.pendingBasePrice>(pxCache3, row4, num3 != 0);
    PXCache pxCache4 = sender;
    object row5 = e.Row;
    int num4;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).BasePriceOverride;
      num4 = nullable1.Value ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.pendingBasePriceDate>(pxCache4, row5, num4 != 0);
    PXCache pxCache5 = sender;
    object row6 = e.Row;
    int num5;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).ABCCodeOverride;
      num5 = nullable1.Value ? 1 : 0;
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.aBCCodeID>(pxCache5, row6, num5 != 0);
    PXCache pxCache6 = sender;
    object row7 = e.Row;
    int num6;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).ABCCodeOverride;
      num6 = nullable1.Value ? 1 : 0;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.aBCCodeIsFixed>(pxCache6, row7, num6 != 0);
    PXCache pxCache7 = sender;
    object row8 = e.Row;
    int num7;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).MovementClassOverride;
      num7 = nullable1.Value ? 1 : 0;
    }
    else
      num7 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.movementClassID>(pxCache7, row8, num7 != 0);
    PXCache pxCache8 = sender;
    object row9 = e.Row;
    int num8;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).MovementClassOverride;
      num8 = nullable1.Value ? 1 : 0;
    }
    else
      num8 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.movementClassIsFixed>(pxCache8, row9, num8 != 0);
    PXCache pxCache9 = sender;
    object row10 = e.Row;
    int num9;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).PreferredVendorOverride;
      num9 = nullable1.Value ? 1 : 0;
    }
    else
      num9 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.preferredVendorID>(pxCache9, row10, num9 != 0);
    PXCache pxCache10 = sender;
    object row11 = e.Row;
    int num10;
    if (e.Row != null)
    {
      nullable1 = ((INItemSite) e.Row).PreferredVendorOverride;
      num10 = nullable1.Value ? 1 : 0;
    }
    else
      num10 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.preferredVendorLocationID>(pxCache10, row11, num10 != 0);
    int num11;
    if (row1 != null)
    {
      nullable1 = row1.ReplenishmentPolicyOverride;
      num11 = nullable1.Value ? 1 : 0;
    }
    else
      num11 = 0;
    bool flag3 = num11 != 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.replenishmentPolicyID>(sender, e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<INItemSite.replenishmentSource>(sender, e.Row, flag3);
    PXUIFieldAttribute.SetEnabled<INItemSite.replenishmentMethod>(sender, e.Row, flag3 && row1.ReplenishmentSource != "O" && row1.ReplenishmentSource != "D");
    PXUIFieldAttribute.SetEnabled<INItemSite.replenishmentSourceSiteID>(sender, e.Row, flag3 && (row1.ReplenishmentSource == "O" || row1.ReplenishmentSource == "D" || row1.ReplenishmentSource == "T" || row1.ReplenishmentSource == "P"));
    bool flag4 = row1 != null && row1.ReplenishmentMethod != "N";
    PXUIFieldAttribute.SetEnabled<INItemSite.maxShelfLifeOverride>(sender, e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<INItemSite.launchDateOverride>(sender, e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<INItemSite.terminationDateOverride>(sender, e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<INItemSite.serviceLevelOverride>(sender, e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<INItemSite.safetyStockOverride>(sender, e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<INItemSite.minQtyOverride>(sender, e.Row, flag4);
    PXUIFieldAttribute.SetEnabled<INItemSite.maxQtyOverride>(sender, e.Row, flag4);
    PXCache pxCache11 = sender;
    object row12 = e.Row;
    int num12;
    if (row1 != null)
    {
      nullable1 = row1.MaxShelfLifeOverride;
      num12 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num12 = 0;
    int num13 = flag4 ? 1 : 0;
    int num14 = num12 & num13;
    PXUIFieldAttribute.SetEnabled<INItemSite.maxShelfLife>(pxCache11, row12, num14 != 0);
    PXCache pxCache12 = sender;
    object row13 = e.Row;
    int num15;
    if (row1 != null)
    {
      nullable1 = row1.LaunchDateOverride;
      num15 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num15 = 0;
    int num16 = flag4 ? 1 : 0;
    int num17 = num15 & num16;
    PXUIFieldAttribute.SetEnabled<INItemSite.launchDate>(pxCache12, row13, num17 != 0);
    PXCache pxCache13 = sender;
    object row14 = e.Row;
    int num18;
    if (row1 != null)
    {
      nullable1 = row1.TerminationDateOverride;
      num18 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num18 = 0;
    int num19 = flag4 ? 1 : 0;
    int num20 = num18 & num19;
    PXUIFieldAttribute.SetEnabled<INItemSite.terminationDate>(pxCache13, row14, num20 != 0);
    PXCache pxCache14 = sender;
    object row15 = e.Row;
    int num21;
    if (row1 != null)
    {
      nullable1 = row1.ServiceLevelOverride;
      num21 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num21 = 0;
    int num22 = flag4 ? 1 : 0;
    int num23 = num21 & num22;
    PXUIFieldAttribute.SetEnabled<INItemSite.serviceLevel>(pxCache14, row15, num23 != 0);
    PXCache pxCache15 = sender;
    object row16 = e.Row;
    int num24;
    if (row1 != null)
    {
      nullable1 = row1.ServiceLevelOverride;
      num24 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num24 = 0;
    int num25 = flag4 ? 1 : 0;
    int num26 = num24 & num25;
    PXUIFieldAttribute.SetEnabled<INItemSite.serviceLevelPct>(pxCache15, row16, num26 != 0);
    PXCache pxCache16 = sender;
    object row17 = e.Row;
    int num27;
    if (row1 != null)
    {
      nullable1 = row1.SafetyStockOverride;
      num27 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num27 = 0;
    int num28 = flag4 ? 1 : 0;
    int num29 = num27 & num28;
    PXUIFieldAttribute.SetEnabled<INItemSite.safetyStock>(pxCache16, row17, num29 != 0);
    PXCache pxCache17 = sender;
    object row18 = e.Row;
    int num30;
    if (row1 != null)
    {
      nullable1 = row1.MinQtyOverride;
      num30 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num30 = 0;
    int num31 = flag4 ? 1 : 0;
    int num32 = num30 & num31;
    PXUIFieldAttribute.SetEnabled<INItemSite.minQty>(pxCache17, row18, num32 != 0);
    PXCache pxCache18 = sender;
    object row19 = e.Row;
    int num33;
    if (row1 != null)
    {
      nullable1 = row1.MaxQtyOverride;
      num33 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num33 = 0;
    int num34 = flag4 ? 1 : 0;
    int num35 = num33 & num34;
    PXUIFieldAttribute.SetEnabled<INItemSite.maxQty>(pxCache18, row19, num35 != 0);
    PXCache pxCache19 = sender;
    object row20 = e.Row;
    int num36;
    if (row1 != null)
    {
      nullable1 = row1.TransferERQOverride;
      num36 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num36 = 0;
    int num37 = flag1 ? 1 : 0;
    int num38 = num36 & num37 & (flag2 ? 1 : 0) & (flag4 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<INItemSite.transferERQ>(pxCache19, row20, num38 != 0);
    PXUIFieldAttribute.SetEnabled<INItemSite.transferERQOverride>(sender, e.Row, row1 != null & flag1 & flag2);
    PXCache pxCache20 = sender;
    object row21 = e.Row;
    int num39;
    if (row1 != null)
    {
      nullable1 = row1.MarkupPctOverride;
      num39 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num39 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.markupPct>(pxCache20, row21, num39 != 0);
    PXCache pxCache21 = sender;
    object row22 = e.Row;
    int num40;
    if (row1 != null)
    {
      nullable1 = row1.RecPriceOverride;
      num40 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num40 = 0;
    PXUIFieldAttribute.SetEnabled<INItemSite.recPrice>(pxCache21, row22, num40 != 0);
    PXCache cache1 = ((PXSelectBase) this.subitemrecords).Cache;
    PXCache cache2 = ((PXSelectBase) this.subitemrecords).Cache;
    PXCache cache3 = ((PXSelectBase) this.subitemrecords).Cache;
    nullable1 = ((INItemSite) e.Row).SubItemOverride;
    int num41;
    bool flag5 = (num41 = nullable1.GetValueOrDefault() ? 1 : 0) != 0;
    cache3.AllowDelete = num41 != 0;
    int num42;
    bool flag6 = (num42 = flag5 ? 1 : 0) != 0;
    cache2.AllowUpdate = num42 != 0;
    int num43 = flag6 ? 1 : 0;
    cache1.AllowInsert = num43 != 0;
    ((PXAction) this.updateReplenishment).SetEnabled(((PXSelectBase) this.subitemrecords).Cache.AllowInsert);
    bool flag7 = row1?.ReplenishmentSource == "T";
    PXUIFieldAttribute.SetRequired<INItemSite.replenishmentSourceSiteID>(sender, flag7);
    int? replenishmentSourceSiteId;
    if (flag7)
    {
      replenishmentSourceSiteId = row1.ReplenishmentSourceSiteID;
      if (!replenishmentSourceSiteId.HasValue)
      {
        sender.RaiseExceptionHandling<INItemSite.replenishmentSourceSiteID>(e.Row, (object) row1.ReplenishmentSourceSiteID, (Exception) new PXSetPropertyException("Replenishment Warehouse cannot be empty.", (PXErrorLevel) 4));
        goto label_76;
      }
    }
    int? nullable2;
    if (flag1)
    {
      replenishmentSourceSiteId = row1.ReplenishmentSourceSiteID;
      nullable2 = row1.SiteID;
      if (replenishmentSourceSiteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & replenishmentSourceSiteId.HasValue == nullable2.HasValue)
      {
        sender.RaiseExceptionHandling<INItemSite.replenishmentSourceSiteID>(e.Row, (object) row1.ReplenishmentSourceSiteID, (Exception) new PXSetPropertyException("Replenishment Source Warehouse must be different from current Warehouse", (PXErrorLevel) 2));
        goto label_76;
      }
    }
    if (EnumerableExtensions.IsIn<string>(PXUIFieldAttribute.GetError<INItemSite.replenishmentSourceSiteID>(sender, e.Row), "Replenishment Warehouse cannot be empty.", "Replenishment Source Warehouse must be different from current Warehouse"))
      sender.RaiseExceptionHandling<INItemSite.replenishmentSourceSiteID>(e.Row, (object) row1.ReplenishmentSourceSiteID, (Exception) null);
label_76:
    ((PXSelectBase) this.itemrecord).Cache.IsDirty = false;
    if (row1 == null)
      return;
    nullable2 = row1.InvtAcctID;
    if (nullable2.HasValue)
      return;
    try
    {
      INItemSiteMaint.DefaultInvtAcctSub((PXGraph) this, row1, ((PXSelectBase<InventoryItem>) this.itemrecord).Current, ((PXSelectBase<INSite>) this.insite).Current, ((PXSelectBase<INPostClass>) this.postclass).Current);
    }
    catch (PXMaskArgumentException ex)
    {
    }
  }

  public virtual void INItemSite_InvtAcctID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    bool? nullable;
    if ((e.Operation & 3) == 1)
    {
      nullable = (bool?) sender.GetValueOriginal<INItemSite.overrideInvtAcctSub>(e.Row);
      if (nullable.GetValueOrDefault())
      {
        nullable = ((INItemSite) e.Row).OverrideInvtAcctSub;
        if (!nullable.GetValueOrDefault())
        {
          sender.SetValue<INItemSite.invtAcctID>(e.Row, (object) null);
          e.Value = (object) null;
          return;
        }
      }
    }
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    nullable = ((INItemSite) e.Row).OverrideInvtAcctSub;
    if (nullable.GetValueOrDefault())
      return;
    e.ExcludeFromInsertUpdate();
  }

  public virtual void INItemSite_InvtSubID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    bool? nullable;
    if ((e.Operation & 3) == 1)
    {
      nullable = (bool?) sender.GetValueOriginal<INItemSite.overrideInvtAcctSub>(e.Row);
      if (nullable.GetValueOrDefault())
      {
        nullable = ((INItemSite) e.Row).OverrideInvtAcctSub;
        if (!nullable.GetValueOrDefault())
        {
          sender.SetValue<INItemSite.invtSubID>(e.Row, (object) null);
          e.Value = (object) null;
          return;
        }
      }
    }
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    nullable = ((INItemSite) e.Row).OverrideInvtAcctSub;
    if (nullable.GetValueOrDefault())
      return;
    e.ExcludeFromInsertUpdate();
  }

  public virtual int Persist(Type cacheType, PXDBOperation operation)
  {
    if (cacheType == typeof (INUnit) && operation == 1)
      ((PXGraph) this).Persist(cacheType, (PXDBOperation) 2);
    return ((PXGraph) this).Persist(cacheType, operation);
  }

  public virtual void Persist()
  {
    foreach (INItemSiteReplenishment siteReplenishment in ((PXSelectBase) this.subitemrecords).Cache.Inserted)
    {
      PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> sitestatusbycostcenter = this.sitestatusbycostcenter;
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
      statusByCostCenter.InventoryID = siteReplenishment.InventoryID;
      statusByCostCenter.SubItemID = siteReplenishment.SubItemID;
      statusByCostCenter.SiteID = siteReplenishment.SiteID;
      statusByCostCenter.CostCenterID = new int?(0);
      statusByCostCenter.PersistEvenZero = new bool?(true);
      ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>) sitestatusbycostcenter).Insert(statusByCostCenter);
    }
    ((PXGraph) this).Persist();
  }

  protected virtual void INItemSite_DfltShipLocationID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null)
      return;
    INItemSite row = (INItemSite) e.Row;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this, (int?) e.NewValue);
    if (inLocation == null || (inLocation.SalesValid ?? true) || ((PXSelectBase<INItemSite>) this.itemsiterecord).Ask("Warning", "Issues are not allowed from this Location. Continue ?", (MessageButtons) 4, false) != 7)
      return;
    e.NewValue = (object) row.DfltShipLocationID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INItemSiteReplenishment_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    INItemSiteReplenishment row = (INItemSiteReplenishment) e.Row;
    PXUIFieldAttribute.SetEnabled<INItemSiteReplenishment.safetyStockSuggested>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INItemSiteReplenishment.minQtySuggested>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INItemSiteReplenishment.maxQtySuggested>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INItemSiteReplenishment.demandPerDayAverage>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INItemSiteReplenishment.demandPerDayMSE>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<INItemSiteReplenishment.demandPerDayMAD>(sender, (object) null, false);
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField]
  protected virtual IEnumerable UpdateReplenishment(PXAdapter adapter)
  {
    foreach (PXResult<INItemSite> pxResult in adapter.Get())
    {
      INItemSite inItemSite = PXResult<INItemSite>.op_Implicit(pxResult);
      if (inItemSite.SubItemOverride.GetValueOrDefault() && ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem.GetValueOrDefault())
      {
        PXView view = ((PXSelectBase) this.subitemrecords).View;
        object[] objArray = new object[1]
        {
          (object) inItemSite
        };
        foreach (INItemSiteReplenishment siteReplenishment1 in view.SelectMulti(objArray))
        {
          INItemSiteReplenishment copy = PXCache<INItemSiteReplenishment>.CreateCopy(siteReplenishment1);
          INItemSiteReplenishment siteReplenishment2 = copy;
          Decimal? nullable1 = inItemSite.SafetyStock;
          Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
          siteReplenishment2.SafetyStock = nullable2;
          INItemSiteReplenishment siteReplenishment3 = copy;
          nullable1 = inItemSite.MinQty;
          Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault());
          siteReplenishment3.MinQty = nullable3;
          INItemSiteReplenishment siteReplenishment4 = copy;
          nullable1 = inItemSite.MaxQty;
          Decimal? nullable4 = new Decimal?(nullable1.GetValueOrDefault());
          siteReplenishment4.MaxQty = nullable4;
          INItemSiteReplenishment siteReplenishment5 = copy;
          nullable1 = inItemSite.TransferERQ;
          Decimal? nullable5 = new Decimal?(nullable1.GetValueOrDefault());
          siteReplenishment5.TransferERQ = nullable5;
          ((PXSelectBase<INItemSiteReplenishment>) this.subitemrecords).Update(copy);
        }
      }
      yield return (object) inItemSite;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INItemSite, INItemSite.productManagerOverride> eventArguments)
  {
    if (eventArguments.Row == null || eventArguments.Row.ProductManagerOverride.GetValueOrDefault())
      return;
    ((PXSelectBase) this.itemsiterecord).Cache.SetDefaultExt<INItemSite.productWorkgroupID>((object) eventArguments.Row);
    ((PXSelectBase) this.itemsiterecord).Cache.SetDefaultExt<INItemSite.productManagerID>((object) eventArguments.Row);
  }
}
