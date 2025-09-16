// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPriceWorksheetMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Scopes;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

[Serializable]
public class APPriceWorksheetMaint : 
  PXGraph<APPriceWorksheetMaint, APPriceWorksheet>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXSelect<APPriceWorksheet> Document;
  [PXImport(typeof (APPriceWorksheet))]
  public PXSelectJoin<APPriceWorksheetDetail, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<APPriceWorksheetDetail.vendorID>>>, Where<APPriceWorksheetDetail.refNbr, Equal<Current<APPriceWorksheet.refNbr>>, And<PX.Objects.CR.BAccount.bAccountID, PX.Data.IsNotNull>>, PX.Data.OrderBy<Asc<APPriceWorksheetDetail.vendorID, Asc<APPriceWorksheetDetail.inventoryCD, Asc<APPriceWorksheetDetail.breakQty>>>>> Details;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSelect<APVendorPrice> APVendorPrices;
  public PXSelect<PX.Objects.AP.Vendor> Vendor;
  [PXCopyPasteHiddenView]
  public PXFilter<CopyPricesFilter> CopyPricesSettings;
  [PXCopyPasteHiddenView]
  public PXFilter<CalculatePricesFilter> CalculatePricesSettings;
  public PXSelect<PX.Objects.CM.CurrencyInfo> CuryInfo;
  [PXCopyPasteHiddenView]
  public PXSelect<INStockItemXRef> StockCrossReferences;
  [PXCopyPasteHiddenView]
  public PXSelect<INNonStockItemXRef> NonStockCrossReferences;
  public PXSetup<Company> company;
  private readonly bool _loadVendorsPricesUsingAlternateID;
  public PXInitializeState<APPriceWorksheet> initializeState;
  public PXAction<APPriceWorksheet> putOnHold;
  public PXAction<APPriceWorksheet> releaseFromHold;
  public PXAction<APPriceWorksheet> ReleasePriceWorksheet;
  public PXAction<APPriceWorksheet> copyPrices;
  public PXAction<APPriceWorksheet> calculatePrices;
  protected readonly string viewPriceCode;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Default<APPriceWorksheetDetail.vendorID>))]
  protected virtual void APPriceWorksheetDetail_PendingPrice_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDefault]
  [PXFormula(typeof (IsNull<Selector<APPriceWorksheetDetail.vendorID, PX.Objects.AP.Vendor.curyID>, Current<AccessInfo.baseCuryID>>))]
  protected virtual void APPriceWorksheetDetail_CuryID_CacheAttached(PXCache sender)
  {
  }

  public APPriceWorksheetMaint()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this._loadVendorsPricesUsingAlternateID = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.distributionModule>() && this.APSetup.Current.LoadVendorsPricesUsingAlternateID.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<APPriceWorksheetDetail.alternateID>(this.Details.Cache, (object) null, this._loadVendorsPricesUsingAlternateID);
    this.Details.Cache.Adjust<PXRestrictorAttribute>().For<APPriceWorksheetDetail.inventoryID>((System.Action<PXRestrictorAttribute>) (ra => ra.CacheGlobal = true));
    this.Details.View.Attributes.OfType<PXImportAttribute>().First<PXImportAttribute>().RowImporting += new EventHandler<PXImportAttribute.RowImportingEventArgs>(this.ImportAttributeRowImporting);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable releasePriceWorksheet(PXAdapter adapter)
  {
    List<APPriceWorksheet> apPriceWorksheetList = new List<APPriceWorksheet>();
    if (this.Document.Current != null)
    {
      this.Save.Press();
      apPriceWorksheetList.Add(this.Document.Current);
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => APPriceWorksheetMaint.ReleaseWorksheet(this.Document.Current)));
    }
    return (IEnumerable) apPriceWorksheetList;
  }

  public static void ReleaseWorksheet(APPriceWorksheet priceWorksheet)
  {
    PXGraph.CreateInstance<APPriceWorksheetMaint>().ReleaseWorksheetImpl(priceWorksheet);
  }

  public virtual void ReleaseWorksheetImpl(APPriceWorksheet priceWorksheet)
  {
    this.Document.Current = priceWorksheet;
    bool isPromotional = priceWorksheet.IsPromotional.GetValueOrDefault();
    System.Type[] byFields1 = new System.Type[5]
    {
      typeof (APVendorPrice.vendorID),
      typeof (APVendorPrice.siteID),
      typeof (APVendorPrice.uOM),
      typeof (APVendorPrice.curyID),
      typeof (APVendorPrice.breakQty)
    };
    System.Type[] byFields2 = new System.Type[1]
    {
      typeof (APVendorPrice.inventoryID)
    };
    GroupedCollection<APVendorPrice, GroupedCollection<APVendorPrice, List<APVendorPrice>>> collection = new List<APVendorPrice>().SplitBy<APVendorPrice, List<APVendorPrice>>(this.Caches<APVendorPrice>(), byFields1).SplitBy<APVendorPrice, List<APVendorPrice>>(byFields2, (GroupItemsLoadHandler<APVendorPrice>) (group => this.LoadInventoryPrices(group, isPromotional)));
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      using (new GroupedCollectionScope<APVendorPrice>((IGroupedCollection<APVendorPrice>) collection))
      {
        foreach (APPriceWorksheetDetail priceLine in (IEnumerable<APPriceWorksheetDetail>) ((IEnumerable<APPriceWorksheetDetail>) this.Details.SelectMain()).OrderBy<APPriceWorksheetDetail, int?>((Func<APPriceWorksheetDetail, int?>) (x => x.InventoryID)))
        {
          PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, priceLine.InventoryID);
          APVendorPrice salesPrice = this.CreateSalesPrice(priceLine, new bool?(isPromotional), new System.DateTime?(), new System.DateTime?());
          IOrderedEnumerable<APVendorPrice> source = collection.GetItems(salesPrice).OrderBy<APVendorPrice, System.DateTime?>((Func<APVendorPrice, System.DateTime?>) (x => x.EffectiveDate)).ThenBy<APVendorPrice, System.DateTime?>((Func<APVendorPrice, System.DateTime?>) (x => x.ExpirationDate));
          PXResultset<APVendorPrice> salesPrices = new PXResultset<APVendorPrice>();
          salesPrices.AddRange(source.Select<APVendorPrice, PXResult<APVendorPrice>>((Func<APVendorPrice, PXResult<APVendorPrice>>) (x => new PXResult<APVendorPrice>(x))));
          this.CreateVendorPricesOnWorksheetRelease(priceLine, salesPrices);
        }
      }
      priceWorksheet.Status = "R";
      this.Document.Update(priceWorksheet);
      this.Document.Current.Status = "R";
      this.Persist();
      transactionScope.Complete();
    }
  }

  protected virtual IEnumerable<APVendorPrice> LoadInventoryPrices(
    APVendorPrice group,
    bool isPromotional)
  {
    return (IEnumerable<APVendorPrice>) new PXViewOf<APVendorPrice>.BasedOn<SelectFromBase<APVendorPrice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APVendorPrice.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<APVendorPrice.isPromotionalPrice, IBqlBool>.IsEqual<P.AsBool>>>>.ReadOnly((PXGraph) this).SelectMain((object) group.InventoryID, (object) isPromotional);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  protected virtual void CreateVendorPricesOnWorksheetRelease(
    PXResult<APPriceWorksheetDetail, PX.Objects.IN.InventoryItem> row)
  {
    this.CreateVendorPricesOnWorksheetRelease((APPriceWorksheetDetail) row, this.GetVendorPricesByPriceLineForWorksheetRelease((APPriceWorksheetDetail) row));
  }

  protected virtual void CreateVendorPricesOnWorksheetRelease(
    APPriceWorksheetDetail priceLine,
    PXResultset<APVendorPrice> salesPrices)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, priceLine.InventoryID);
    System.DateTime? expirationDate = this.Document.Current.ExpirationDate;
    System.DateTime? effectiveDate = this.Document.Current.EffectiveDate;
    if (!this.Document.Current.IsPromotional.GetValueOrDefault() || !expirationDate.HasValue)
      this.ProcessReleaseForNonPromotionalVendorPrices(salesPrices, priceLine);
    else
      this.ProcessReleaseForPromotionalVendorPrices(salesPrices, priceLine);
    int? nullable1;
    if (this.APSetup.Current.RetentionType == "F")
    {
      nullable1 = this.APSetup.Current.NumberOfMonths;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        foreach (PXResult<APVendorPrice> salesPrice in salesPrices)
        {
          APVendorPrice apVendorPrice = (APVendorPrice) salesPrice;
          nullable1 = this.APSetup.Current.NumberOfMonths;
          int valueOrDefault = nullable1.GetValueOrDefault();
          System.DateTime? nullable2 = apVendorPrice.ExpirationDate;
          if (nullable2.HasValue)
          {
            System.DateTime dateTime = apVendorPrice.ExpirationDate.Value.AddMonths(valueOrDefault);
            nullable2 = effectiveDate;
            if ((nullable2.HasValue ? (dateTime < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              this.APVendorPrices.Delete(apVendorPrice);
          }
        }
      }
    }
    if (!this._loadVendorsPricesUsingAlternateID || Str.IsNullOrEmpty(priceLine.AlternateID) || PriceWorksheetAlternateItemAttribute.XRefsExists(this.Details.Cache, (object) priceLine))
      return;
    PXCache pxCache = inventoryItem.StkItem.GetValueOrDefault() ? this.StockCrossReferences.Cache : this.NonStockCrossReferences.Cache;
    INItemXRef instance = (INItemXRef) pxCache.CreateInstance();
    instance.InventoryID = priceLine.InventoryID;
    instance.AlternateType = "GLBL";
    instance.AlternateID = priceLine.AlternateID;
    instance.UOM = priceLine.UOM;
    instance.BAccountID = new int?(0);
    INItemXRef inItemXref1 = instance;
    nullable1 = priceLine.SubItemID;
    int? nullable3 = nullable1 ?? inventoryItem.DefaultSubItemID;
    inItemXref1.SubItemID = nullable3;
    INItemXRef inItemXref2 = (INItemXRef) pxCache.Insert((object) instance);
  }

  protected virtual PXResultset<APVendorPrice> GetVendorPricesByPriceLineForWorksheetRelease(
    APPriceWorksheetDetail priceLine)
  {
    return PXSelectBase<APVendorPrice, PXSelect<APVendorPrice, Where<APVendorPrice.vendorID, Equal<Required<APVendorPrice.vendorID>>, And<APVendorPrice.inventoryID, Equal<Required<APVendorPrice.inventoryID>>, And2<Where<APVendorPrice.siteID, Equal<Required<APVendorPrice.siteID>>, Or<APVendorPrice.siteID, PX.Data.IsNull, And<Required<APVendorPrice.siteID>, PX.Data.IsNull>>>, And<APVendorPrice.uOM, Equal<Required<APVendorPrice.uOM>>, And<APVendorPrice.curyID, Equal<Required<APVendorPrice.curyID>>, And<APVendorPrice.breakQty, Equal<Required<APVendorPrice.breakQty>>, And<APVendorPrice.isPromotionalPrice, Equal<Required<APVendorPrice.isPromotionalPrice>>>>>>>>>, PX.Data.OrderBy<Asc<APVendorPrice.effectiveDate, Asc<APVendorPrice.expirationDate>>>>.Config>.Select((PXGraph) this, (object) priceLine.VendorID, (object) priceLine.InventoryID, (object) priceLine.SiteID, (object) priceLine.SiteID, (object) priceLine.UOM, (object) priceLine.CuryID, (object) priceLine.BreakQty, (object) this.Document.Current.IsPromotional.GetValueOrDefault());
  }

  protected virtual void ProcessReleaseForNonPromotionalVendorPrices(
    PXResultset<APVendorPrice> salesPrices,
    APPriceWorksheetDetail priceLine)
  {
    System.DateTime? expirationDate1 = this.Document.Current.ExpirationDate;
    System.DateTime? effectiveDate1 = this.Document.Current.EffectiveDate;
    bool flag = true;
    if (salesPrices.Count == 0)
    {
      this.APVendorPrices.Insert(this.CreateSalesPrice(priceLine, new bool?(false), effectiveDate1, expirationDate1));
    }
    else
    {
      foreach (PXResult<APVendorPrice> salesPrice1 in salesPrices)
      {
        APVendorPrice salesPrice2 = (APVendorPrice) salesPrice1;
        System.DateTime? nullable1;
        if (this.APSetup.Current.RetentionType == "F")
        {
          if (!this.Document.Current.OverwriteOverlapping.GetValueOrDefault())
          {
            nullable1 = salesPrice2.EffectiveDate;
            System.DateTime? nullable2 = effectiveDate1;
            if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              System.DateTime? expirationDate2 = salesPrice2.ExpirationDate;
              nullable1 = effectiveDate1;
              if ((expirationDate2.HasValue & nullable1.HasValue ? (expirationDate2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                goto label_9;
            }
            nullable1 = salesPrice2.EffectiveDate;
            System.DateTime? nullable3 = effectiveDate1;
            if ((nullable1.HasValue & nullable3.HasValue ? (nullable1.GetValueOrDefault() <= nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0 || salesPrice2.ExpirationDate.HasValue)
              goto label_10;
label_9:
            flag = false;
          }
label_10:
          this.ProcessNonPromotionalPriceForFixedNumOfMonthsRetentionType(salesPrice2, priceLine, expirationDate1, effectiveDate1);
        }
        else
        {
          System.DateTime? effectiveDate2 = salesPrice2.EffectiveDate;
          nullable1 = effectiveDate1;
          if ((effectiveDate2.HasValue & nullable1.HasValue ? (effectiveDate2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          {
            System.DateTime dateTime;
            if (effectiveDate1.HasValue)
            {
              nullable1 = salesPrice2.ExpirationDate;
              dateTime = effectiveDate1.Value.AddDays(-1.0);
              if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
                goto label_14;
            }
            nullable1 = salesPrice2.EffectiveDate;
            System.DateTime? nullable4 = effectiveDate1;
            if (((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() <= nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0 || salesPrice2.ExpirationDate.HasValue) && (salesPrice2.EffectiveDate.HasValue || salesPrice2.ExpirationDate.HasValue))
            {
              System.DateTime? effectiveDate3 = salesPrice2.EffectiveDate;
              nullable1 = effectiveDate1;
              if ((effectiveDate3.HasValue & nullable1.HasValue ? (effectiveDate3.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
              {
                nullable1 = salesPrice2.EffectiveDate;
                if (nullable1.HasValue)
                  continue;
              }
              nullable1 = effectiveDate1;
              System.DateTime? expirationDate3 = salesPrice2.ExpirationDate;
              if ((nullable1.HasValue & expirationDate3.HasValue ? (nullable1.GetValueOrDefault() <= expirationDate3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                continue;
            }
            if (effectiveDate1.HasValue)
            {
              APVendorPrice apVendorPrice = salesPrice2;
              dateTime = effectiveDate1.Value;
              System.DateTime? nullable5 = new System.DateTime?(dateTime.AddDays(-1.0));
              apVendorPrice.ExpirationDate = nullable5;
              salesPrice2.EffectiveDate = new System.DateTime?();
              this.APVendorPrices.Update(salesPrice2);
              continue;
            }
            continue;
          }
label_14:
          this.APVendorPrices.Delete(salesPrice2);
        }
      }
      if (!flag)
        return;
      if (this.Document.Current.OverwriteOverlapping.GetValueOrDefault() || this.APSetup.Current.RetentionType == "L")
      {
        this.APVendorPrices.Insert(this.CreateSalesPrice(priceLine, new bool?(false), effectiveDate1, expirationDate1));
      }
      else
      {
        APVendorPrice worksheetRelease = this.GetMinVendorPriceForNonPromotionalPricesWorksheetRelease(priceLine, effectiveDate1);
        System.DateTime? nullable;
        if (worksheetRelease == null)
        {
          nullable = new System.DateTime?();
        }
        else
        {
          System.DateTime? effectiveDate4 = worksheetRelease.EffectiveDate;
          ref System.DateTime? local = ref effectiveDate4;
          nullable = local.HasValue ? new System.DateTime?(local.GetValueOrDefault().AddDays(-1.0)) : new System.DateTime?();
        }
        System.DateTime? expirationDate4 = nullable ?? expirationDate1;
        this.APVendorPrices.Insert(this.CreateSalesPrice(priceLine, new bool?(false), effectiveDate1, expirationDate4));
      }
    }
  }

  protected virtual void ProcessNonPromotionalPriceForFixedNumOfMonthsRetentionType(
    APVendorPrice salesPrice,
    APPriceWorksheetDetail priceLine,
    System.DateTime? worksheetExpirationDate,
    System.DateTime? worksheetEffectiveDate)
  {
    if (this.Document.Current.OverwriteOverlapping.GetValueOrDefault())
    {
      System.DateTime? nullable1;
      if (!worksheetExpirationDate.HasValue)
      {
        System.DateTime? effectiveDate = salesPrice.EffectiveDate;
        nullable1 = worksheetEffectiveDate;
        if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_6;
      }
      System.DateTime? nullable2;
      if (worksheetExpirationDate.HasValue)
      {
        nullable1 = salesPrice.EffectiveDate;
        nullable2 = worksheetEffectiveDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable2 = salesPrice.EffectiveDate;
          nullable1 = worksheetExpirationDate;
          if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            goto label_6;
        }
      }
      nullable1 = salesPrice.EffectiveDate;
      nullable2 = worksheetEffectiveDate;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable2 = salesPrice.ExpirationDate;
        if (!nullable2.HasValue)
          goto label_15;
      }
      nullable2 = salesPrice.EffectiveDate;
      if (!nullable2.HasValue)
      {
        nullable2 = salesPrice.ExpirationDate;
        if (!nullable2.HasValue)
          goto label_15;
      }
      nullable2 = salesPrice.EffectiveDate;
      if (!nullable2.HasValue)
      {
        nullable2 = salesPrice.ExpirationDate;
        nullable1 = worksheetEffectiveDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_15;
      }
      nullable1 = salesPrice.EffectiveDate;
      nullable2 = worksheetEffectiveDate;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
      nullable2 = worksheetEffectiveDate;
      nullable1 = salesPrice.ExpirationDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
label_15:
      if (this.Document.Current.IsPromotional.GetValueOrDefault() || !worksheetEffectiveDate.HasValue)
        return;
      salesPrice.ExpirationDate = new System.DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
      this.APVendorPrices.Update(salesPrice);
      return;
label_6:
      this.APVendorPrices.Delete(salesPrice);
    }
    else
    {
      System.DateTime? effectiveDate1 = salesPrice.EffectiveDate;
      System.DateTime? nullable3 = worksheetEffectiveDate;
      System.DateTime? nullable4;
      if ((effectiveDate1.HasValue & nullable3.HasValue ? (effectiveDate1.GetValueOrDefault() < nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable3 = salesPrice.ExpirationDate;
        nullable4 = worksheetEffectiveDate;
        if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0 && worksheetEffectiveDate.HasValue)
        {
          APVendorPrice copy = (APVendorPrice) this.APVendorPrices.Cache.CreateCopy((object) salesPrice);
          salesPrice.EffectiveDate = worksheetEffectiveDate;
          this.UpdateVendorPriceFromPriceLine(salesPrice, priceLine);
          this.APVendorPrices.Update(salesPrice);
          copy.ExpirationDate = new System.DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
          copy.RecordID = new int?();
          this.APVendorPrices.Insert(copy);
          return;
        }
      }
      nullable4 = salesPrice.EffectiveDate;
      nullable3 = worksheetEffectiveDate;
      if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() <= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable3 = salesPrice.ExpirationDate;
        if (!nullable3.HasValue && worksheetEffectiveDate.HasValue)
        {
          salesPrice.ExpirationDate = new System.DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
          this.APVendorPrices.Update(salesPrice);
          APPriceWorksheetDetail priceLine1 = priceLine;
          bool? isPromotional = new bool?(false);
          System.DateTime? effectiveDate2 = worksheetEffectiveDate;
          nullable3 = new System.DateTime?();
          System.DateTime? expirationDate = nullable3;
          this.APVendorPrices.Insert(this.CreateSalesPrice(priceLine1, isPromotional, effectiveDate2, expirationDate));
          return;
        }
      }
      nullable3 = salesPrice.EffectiveDate;
      nullable4 = worksheetEffectiveDate;
      if ((nullable3.HasValue == nullable4.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        nullable4 = salesPrice.ExpirationDate;
        nullable3 = worksheetExpirationDate;
        if ((nullable4.HasValue == nullable3.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          this.UpdateVendorPriceFromPriceLine(salesPrice, priceLine);
          this.APVendorPrices.Update(salesPrice);
          return;
        }
      }
      nullable3 = salesPrice.EffectiveDate;
      if (!nullable3.HasValue)
      {
        nullable3 = salesPrice.ExpirationDate;
        if (!nullable3.HasValue)
          goto label_30;
      }
      nullable3 = salesPrice.EffectiveDate;
      if (nullable3.HasValue)
        return;
      nullable3 = salesPrice.ExpirationDate;
      nullable4 = worksheetEffectiveDate;
      if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
label_30:
      salesPrice.ExpirationDate = new System.DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
      this.APVendorPrices.Update(salesPrice);
    }
  }

  protected virtual void UpdateVendorPriceFromPriceLine(
    APVendorPrice salesPrice,
    APPriceWorksheetDetail priceLine)
  {
    salesPrice.SalesPrice = priceLine.PendingPrice;
  }

  protected virtual APVendorPrice GetMinVendorPriceForNonPromotionalPricesWorksheetRelease(
    APPriceWorksheetDetail priceLine,
    System.DateTime? effectiveDate)
  {
    return (APVendorPrice) PXSelectBase<APVendorPrice, PXSelect<APVendorPrice, Where<APVendorPrice.vendorID, Equal<Required<APVendorPrice.vendorID>>, And<APVendorPrice.inventoryID, Equal<Required<APVendorPrice.inventoryID>>, And2<Where<APVendorPrice.siteID, Equal<Required<APVendorPrice.siteID>>, Or<APVendorPrice.siteID, PX.Data.IsNull, And<Required<APVendorPrice.siteID>, PX.Data.IsNull>>>, And<APVendorPrice.uOM, Equal<Required<APVendorPrice.uOM>>, And<APVendorPrice.curyID, Equal<Required<APVendorPrice.curyID>>, And<APVendorPrice.breakQty, Equal<Required<APVendorPrice.breakQty>>, And<APVendorPrice.effectiveDate, PX.Data.IsNotNull, PX.Data.And<Where<APVendorPrice.effectiveDate, GreaterEqual<Required<APVendorPrice.effectiveDate>>>>>>>>>>>, PX.Data.OrderBy<Asc<APVendorPrice.effectiveDate>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) priceLine.VendorID, (object) priceLine.InventoryID, (object) priceLine.SiteID, (object) priceLine.SiteID, (object) priceLine.UOM, (object) priceLine.CuryID, (object) priceLine.BreakQty, (object) effectiveDate);
  }

  protected virtual void ProcessReleaseForPromotionalVendorPrices(
    PXResultset<APVendorPrice> salesPrices,
    APPriceWorksheetDetail priceLine)
  {
    System.DateTime? expirationDate1 = this.Document.Current.ExpirationDate;
    System.DateTime? effectiveDate1 = this.Document.Current.EffectiveDate;
    foreach (PXResult<APVendorPrice> salesPrice in salesPrices)
    {
      APVendorPrice apVendorPrice1 = (APVendorPrice) salesPrice;
      System.DateTime? nullable1 = apVendorPrice1.EffectiveDate;
      System.DateTime? nullable2 = effectiveDate1;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        System.DateTime? expirationDate2 = apVendorPrice1.ExpirationDate;
        nullable1 = expirationDate1;
        if ((expirationDate2.HasValue & nullable1.HasValue ? (expirationDate2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          this.APVendorPrices.Delete(apVendorPrice1);
          continue;
        }
      }
      nullable1 = apVendorPrice1.EffectiveDate;
      System.DateTime? nullable3 = effectiveDate1;
      System.DateTime dateTime;
      if ((nullable1.HasValue & nullable3.HasValue ? (nullable1.GetValueOrDefault() <= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        System.DateTime? expirationDate3 = apVendorPrice1.ExpirationDate;
        nullable1 = expirationDate1;
        if ((expirationDate3.HasValue & nullable1.HasValue ? (expirationDate3.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable1 = apVendorPrice1.ExpirationDate;
          System.DateTime? nullable4 = effectiveDate1;
          if ((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0 && effectiveDate1.HasValue)
          {
            APVendorPrice apVendorPrice2 = apVendorPrice1;
            dateTime = effectiveDate1.Value;
            System.DateTime? nullable5 = new System.DateTime?(dateTime.AddDays(-1.0));
            apVendorPrice2.ExpirationDate = nullable5;
            this.APVendorPrices.Update(apVendorPrice1);
            continue;
          }
        }
      }
      System.DateTime? effectiveDate2 = apVendorPrice1.EffectiveDate;
      nullable1 = effectiveDate1;
      if ((effectiveDate2.HasValue & nullable1.HasValue ? (effectiveDate2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = apVendorPrice1.EffectiveDate;
        System.DateTime? nullable6 = expirationDate1;
        if ((nullable1.HasValue & nullable6.HasValue ? (nullable1.GetValueOrDefault() < nullable6.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          System.DateTime? expirationDate4 = apVendorPrice1.ExpirationDate;
          nullable1 = expirationDate1;
          if ((expirationDate4.HasValue & nullable1.HasValue ? (expirationDate4.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            APVendorPrice apVendorPrice3 = apVendorPrice1;
            dateTime = expirationDate1.Value;
            System.DateTime? nullable7 = new System.DateTime?(dateTime.AddDays(1.0));
            apVendorPrice3.EffectiveDate = nullable7;
            this.APVendorPrices.Update(apVendorPrice1);
            continue;
          }
        }
      }
      nullable1 = apVendorPrice1.EffectiveDate;
      System.DateTime? nullable8 = effectiveDate1;
      if ((nullable1.HasValue & nullable8.HasValue ? (nullable1.GetValueOrDefault() <= nullable8.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        System.DateTime? expirationDate5 = apVendorPrice1.ExpirationDate;
        nullable1 = expirationDate1;
        if ((expirationDate5.HasValue & nullable1.HasValue ? (expirationDate5.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable1 = apVendorPrice1.ExpirationDate;
          System.DateTime? nullable9 = effectiveDate1;
          if ((nullable1.HasValue & nullable9.HasValue ? (nullable1.GetValueOrDefault() > nullable9.GetValueOrDefault() ? 1 : 0) : 0) != 0 && effectiveDate1.HasValue)
          {
            APVendorPrice apVendorPrice4 = apVendorPrice1;
            dateTime = effectiveDate1.Value;
            System.DateTime? nullable10 = new System.DateTime?(dateTime.AddDays(-1.0));
            apVendorPrice4.ExpirationDate = nullable10;
            this.APVendorPrices.Update(apVendorPrice1);
          }
        }
      }
    }
    this.APVendorPrices.Insert(this.CreateSalesPrice(priceLine, new bool?(true), effectiveDate1, expirationDate1));
  }

  protected virtual APVendorPrice CreateSalesPrice(
    APPriceWorksheetDetail priceLine,
    bool? isPromotional,
    System.DateTime? effectiveDate,
    System.DateTime? expirationDate)
  {
    return new APVendorPrice()
    {
      VendorID = priceLine.VendorID,
      InventoryID = priceLine.InventoryID,
      SiteID = priceLine.SiteID,
      UOM = priceLine.UOM,
      BreakQty = priceLine.BreakQty,
      SalesPrice = priceLine.PendingPrice,
      CuryID = priceLine.CuryID,
      IsPromotionalPrice = isPromotional,
      EffectiveDate = effectiveDate,
      ExpirationDate = expirationDate
    };
  }

  public override IEnumerable<PXDataRecord> ProviderSelect(
    BqlCommand command,
    int topCount,
    params PXDataValue[] pars)
  {
    return base.ProviderSelect(command, topCount, pars);
  }

  public override void Persist()
  {
    this.CheckForDuplicateDetails();
    base.Persist();
  }

  [PXUIField(DisplayName = "Copy Prices", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable CopyPrices(PXAdapter adapter)
  {
    if (this.CopyPricesSettings.AskExt() == WebDialogResult.OK && this.CopyPricesSettings.Current != null)
    {
      int? nullable = this.CopyPricesSettings.Current.SourceVendorID;
      if (!nullable.HasValue)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.sourceVendorID>(adapter);
      if (this.CopyPricesSettings.Current.SourceCuryID == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.sourceCuryID>(adapter);
      if (!this.CopyPricesSettings.Current.EffectiveDate.HasValue)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.effectiveDate>(adapter);
      nullable = this.CopyPricesSettings.Current.DestinationVendorID;
      if (!nullable.HasValue)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.destinationVendorID>(adapter);
      if (this.CopyPricesSettings.Current.DestinationCuryID == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.destinationCuryID>(adapter);
      if (this.CopyPricesSettings.Current.DestinationCuryID != this.CopyPricesSettings.Current.SourceCuryID && this.CopyPricesSettings.Current.RateTypeID == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.rateTypeID>(adapter);
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => APPriceWorksheetMaint.CopyPricesProc(this.Document.Current, this.CopyPricesSettings.Current)));
    }
    return adapter.Get();
  }

  private IEnumerable SetErrorOnEmptyFieldAndReturn<TField>(PXAdapter adapter) where TField : IBqlField
  {
    this.CopyPricesSettings.Cache.RaiseExceptionHandling<TField>((object) this.CopyPricesSettings.Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{typeof (TField).Name}]"
    }));
    return adapter.Get();
  }

  public static void CopyPricesProc(APPriceWorksheet priceWorksheet, CopyPricesFilter copyFilter)
  {
    APPriceWorksheetMaint instance = PXGraph.CreateInstance<APPriceWorksheetMaint>();
    instance.Document.Update((APPriceWorksheet) instance.Document.Cache.CreateCopy((object) priceWorksheet));
    instance.CopyPricesInternalProcessing(copyFilter);
    PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.Same);
  }

  protected virtual void CopyPricesInternalProcessing(CopyPricesFilter copyFilter)
  {
    this.CopyPricesSettings.Current = copyFilter;
    EnumerableExtensions.ForEach<APPriceWorksheetDetail>(PXSelectBase<APVendorPrice, PXSelectJoinGroupBy<APVendorPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APVendorPrice.inventoryID>>>, Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.toDelete>, And<APVendorPrice.vendorID, Equal<Required<APVendorPrice.vendorID>>, And<APVendorPrice.curyID, Equal<Required<APVendorPrice.curyID>>, And2<AreSame<APVendorPrice.siteID, Required<APVendorPrice.siteID>>, And<APVendorPrice.isPromotionalPrice, Equal<Required<APVendorPrice.isPromotionalPrice>>, PX.Data.And<Where2<Where<APVendorPrice.effectiveDate, LessEqual<Required<APVendorPrice.effectiveDate>>, And<APVendorPrice.expirationDate, PX.Data.IsNull>>, PX.Data.Or<Where<APVendorPrice.effectiveDate, LessEqual<Required<APVendorPrice.effectiveDate>>, And<APVendorPrice.expirationDate, Greater<Required<APVendorPrice.effectiveDate>>>>>>>>>>>>>, PX.Data.Aggregate<GroupBy<APVendorPrice.vendorID, GroupBy<APVendorPrice.inventoryID, GroupBy<APVendorPrice.uOM, GroupBy<APVendorPrice.breakQty, GroupBy<APVendorPrice.curyID, GroupBy<APVendorPrice.siteID>>>>>>>, PX.Data.OrderBy<Asc<APVendorPrice.effectiveDate, Asc<APVendorPrice.expirationDate>>>>.Config>.Select((PXGraph) this, (object) copyFilter.SourceVendorID, (object) copyFilter.SourceCuryID, (object) copyFilter.SourceSiteID, (object) copyFilter.SourceSiteID, (object) copyFilter.IsPromotional.GetValueOrDefault(), (object) copyFilter.EffectiveDate, (object) copyFilter.EffectiveDate, (object) copyFilter.EffectiveDate).AsEnumerable<PXResult<APVendorPrice>>().Select<PXResult<APVendorPrice>, APPriceWorksheetDetail>((Func<PXResult<APVendorPrice>, APPriceWorksheetDetail>) (price => this.CreateWorksheetDetailFromVendorPriceOnCopying((APVendorPrice) price, copyFilter))), (System.Action<APPriceWorksheetDetail>) (newLine => this.Details.Update(newLine)));
    this.Save.Press();
    this.CopyPricesSettings.Cache.Clear();
  }

  protected virtual APPriceWorksheetDetail CreateWorksheetDetailFromVendorPriceOnCopying(
    APVendorPrice salesPrice,
    CopyPricesFilter copyFilter)
  {
    APPriceWorksheetDetail vendorPriceOnCopying = new APPriceWorksheetDetail()
    {
      VendorID = copyFilter.DestinationVendorID,
      InventoryID = salesPrice.InventoryID,
      SiteID = copyFilter.DestinationSiteID ?? salesPrice.SiteID,
      UOM = salesPrice.UOM,
      BreakQty = salesPrice.BreakQty,
      CuryID = copyFilter.DestinationCuryID
    };
    vendorPriceOnCopying.CurrentPrice = !(copyFilter.SourceCuryID == copyFilter.DestinationCuryID) ? new Decimal?(APPriceWorksheetMaint.ConvertSalesPrice(this, copyFilter.RateTypeID, copyFilter.SourceCuryID, copyFilter.DestinationCuryID, copyFilter.CurrencyDate, salesPrice.SalesPrice.GetValueOrDefault())) : new Decimal?(salesPrice.SalesPrice.GetValueOrDefault());
    return vendorPriceOnCopying;
  }

  public static Decimal ConvertSalesPrice(
    APPriceWorksheetMaint graph,
    string curyRateTypeID,
    string fromCuryID,
    string toCuryID,
    System.DateTime? curyEffectiveDate,
    Decimal salesPrice)
  {
    Decimal curyval = salesPrice;
    if (curyRateTypeID == null || curyRateTypeID == null || !curyEffectiveDate.HasValue)
      return curyval;
    PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) graph.CuryInfo.Cache.Update((object) new PX.Objects.CM.CurrencyInfo()
    {
      BaseCuryID = fromCuryID,
      CuryID = toCuryID,
      CuryRateTypeID = curyRateTypeID
    });
    info.SetCuryEffDate(graph.CuryInfo.Cache, (object) curyEffectiveDate);
    graph.CuryInfo.Cache.Update((object) info);
    PXCurrencyAttribute.CuryConvCury(graph.CuryInfo.Cache, info, salesPrice, out curyval);
    graph.CuryInfo.Cache.Delete((object) info);
    return curyval;
  }

  [PXUIField(DisplayName = "Calculate Pending Prices", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable CalculatePrices(PXAdapter adapter)
  {
    if (this.CalculatePricesSettings.AskExtRequired())
      this.CalculatePendingPrices(this.CalculatePricesSettings.Current);
    this.SelectTimeStamp();
    return adapter.Get();
  }

  private void CalculatePendingPrices(CalculatePricesFilter settings)
  {
    if (settings == null)
      return;
    foreach (PXResult<APPriceWorksheetDetail> pxResult1 in this.Details.Select())
    {
      APPriceWorksheetDetail worksheetDetail = (APPriceWorksheetDetail) pxResult1;
      PXResult<PX.Objects.IN.InventoryItem, InventoryItemCurySettings, INItemCost> pxResult2 = (PXResult<PX.Objects.IN.InventoryItem, InventoryItemCurySettings, INItemCost>) (PXResult<PX.Objects.IN.InventoryItem>) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<InventoryItemCurySettings.curyID, Equal<Current<CalculatePricesFilter.baseCuryID>>>>, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<Current<CalculatePricesFilter.baseCuryID>>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) worksheetDetail.InventoryID);
      PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) pxResult2;
      InventoryItemCurySettings curyItem = (InventoryItemCurySettings) pxResult2;
      INItemCost itemCost = (INItemCost) pxResult2;
      KeyValuePair<bool, Decimal> pricesCalculation = this.CalculateCorrectedAmountForPendingPricesCalculation(settings, inventoryItem, curyItem, itemCost, worksheetDetail);
      if (!pricesCalculation.Key)
      {
        Decimal num = pricesCalculation.Value;
        if (settings.CorrectionPercent.HasValue)
          num *= settings.CorrectionPercent.Value * 0.01M;
        short? rounding = settings.Rounding;
        if (rounding.HasValue)
        {
          Decimal d = num;
          rounding = settings.Rounding;
          int decimals = (int) rounding.Value;
          num = System.Math.Round(d, decimals, MidpointRounding.AwayFromZero);
        }
        APPriceWorksheetDetail copy = (APPriceWorksheetDetail) this.Details.Cache.CreateCopy((object) worksheetDetail);
        copy.PendingPrice = new Decimal?(num);
        this.Details.Update(copy);
      }
    }
  }

  protected virtual KeyValuePair<bool, Decimal> CalculateCorrectedAmountForPendingPricesCalculation(
    CalculatePricesFilter settings,
    PX.Objects.IN.InventoryItem inventoryItem,
    InventoryItemCurySettings curyItem,
    INItemCost itemCost,
    APPriceWorksheetDetail worksheetDetail)
  {
    bool key = false;
    Decimal num1 = 0M;
    switch (settings.PriceBasis)
    {
      case "L":
        Decimal? lastCost;
        int num2;
        if (!settings.UpdateOnZero.GetValueOrDefault())
        {
          lastCost = itemCost.LastCost;
          if (lastCost.HasValue)
          {
            lastCost = itemCost.LastCost;
            Decimal num3 = 0M;
            num2 = lastCost.GetValueOrDefault() == num3 & lastCost.HasValue ? 1 : 0;
          }
          else
            num2 = 1;
        }
        else
          num2 = 0;
        key = num2 != 0;
        if (key)
          return new KeyValuePair<bool, Decimal>(key, num1);
        lastCost = itemCost.LastCost;
        Decimal valueOrDefault1 = lastCost.GetValueOrDefault();
        num1 = this.AdjustByUOM(inventoryItem, worksheetDetail, valueOrDefault1);
        break;
      case "S":
        Decimal? nullable1 = inventoryItem.ValMethod == "T" ? curyItem.StdCost : itemCost.AvgCost;
        int num4;
        if (!settings.UpdateOnZero.GetValueOrDefault())
        {
          if (nullable1.HasValue)
          {
            Decimal? nullable2 = nullable1;
            Decimal num5 = 0M;
            num4 = nullable2.GetValueOrDefault() == num5 & nullable2.HasValue ? 1 : 0;
          }
          else
            num4 = 1;
        }
        else
          num4 = 0;
        key = num4 != 0;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        num1 = this.AdjustByUOM(inventoryItem, worksheetDetail, valueOrDefault2);
        break;
      case "N":
        Decimal? pendingPrice;
        int num6;
        if (!settings.UpdateOnZero.GetValueOrDefault())
        {
          pendingPrice = worksheetDetail.PendingPrice;
          if (pendingPrice.HasValue)
          {
            pendingPrice = worksheetDetail.PendingPrice;
            Decimal num7 = 0M;
            num6 = pendingPrice.GetValueOrDefault() == num7 & pendingPrice.HasValue ? 1 : 0;
          }
          else
            num6 = 1;
        }
        else
          num6 = 0;
        key = num6 != 0;
        pendingPrice = worksheetDetail.PendingPrice;
        num1 = pendingPrice.GetValueOrDefault();
        break;
      case "P":
        Decimal? currentPrice;
        int num8;
        if (!settings.UpdateOnZero.GetValueOrDefault())
        {
          currentPrice = worksheetDetail.CurrentPrice;
          if (currentPrice.HasValue)
          {
            currentPrice = worksheetDetail.CurrentPrice;
            Decimal num9 = 0M;
            num8 = currentPrice.GetValueOrDefault() == num9 & currentPrice.HasValue ? 1 : 0;
          }
          else
            num8 = 1;
        }
        else
          num8 = 0;
        key = num8 != 0;
        currentPrice = worksheetDetail.CurrentPrice;
        num1 = currentPrice.GetValueOrDefault();
        break;
      case "R":
        Decimal? recPrice;
        int num10;
        if (!settings.UpdateOnZero.GetValueOrDefault())
        {
          recPrice = curyItem.RecPrice;
          if (recPrice.HasValue)
          {
            recPrice = curyItem.RecPrice;
            Decimal num11 = 0M;
            num10 = recPrice.GetValueOrDefault() == num11 & recPrice.HasValue ? 1 : 0;
          }
          else
            num10 = 1;
        }
        else
          num10 = 0;
        key = num10 != 0;
        recPrice = curyItem.RecPrice;
        num1 = recPrice.GetValueOrDefault();
        break;
    }
    return new KeyValuePair<bool, Decimal>(key, num1);
  }

  protected virtual Decimal AdjustByUOM(
    PX.Objects.IN.InventoryItem inventoryItem,
    APPriceWorksheetDetail worksheetDetail,
    Decimal correctedAmtInBaseUnit)
  {
    if (!(inventoryItem.BaseUnit != worksheetDetail.UOM))
      return correctedAmtInBaseUnit;
    Decimal? result;
    return this.TryConvertToBase(this.Caches[typeof (PX.Objects.IN.InventoryItem)], inventoryItem.InventoryID, worksheetDetail.UOM, correctedAmtInBaseUnit, out result) ? result.Value : 0M;
  }

  protected virtual void CheckForDuplicateDetails()
  {
  }

  private Decimal GetItemPrice(
    int? inventoryID,
    string toCuryID,
    System.DateTime? curyEffectiveDate,
    string uom = null)
  {
    Decimal? fromInventoryItem = this.GetItemPriceFromInventoryItem(PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID), uom);
    return fromInventoryItem.HasValue ? APPriceWorksheetMaint.ConvertSalesPrice(this, new CMSetupSelect((PXGraph) this).Current.APRateTypeDflt, this.Accessinfo.BaseCuryID, toCuryID, curyEffectiveDate, fromInventoryItem.Value) : 0M;
  }

  /// <summary>
  /// Gets item price from inventory item. The extension point used by Lexware PriceUnit customization to adjust inventory item's price.
  /// </summary>
  /// <param name="inventoryItem">The inventory item.</param>
  protected virtual Decimal? GetItemPriceFromInventoryItem(PX.Objects.IN.InventoryItem inventoryItem)
  {
    return this.GetItemPriceFromInventoryItem(inventoryItem, inventoryItem?.BaseUnit);
  }

  protected virtual Decimal? GetItemPriceFromInventoryItem(PX.Objects.IN.InventoryItem inventoryItem, string uom)
  {
    if (inventoryItem == null)
      return new Decimal?();
    bool? stkItem = inventoryItem.StkItem;
    bool flag = false;
    Decimal? nullable = stkItem.GetValueOrDefault() == flag & stkItem.HasValue || inventoryItem.ValMethod == "T" ? InventoryItemCurySettings.PK.Find((PXGraph) this, (int?) inventoryItem?.InventoryID, this.Accessinfo.BaseCuryID).StdCost : (Decimal?) INItemCost.PK.Find((PXGraph) this, inventoryItem.InventoryID, this.Accessinfo.BaseCuryID)?.LastCost;
    return string.IsNullOrEmpty(uom) || uom == inventoryItem.BaseUnit ? nullable : new Decimal?(INUnitAttribute.ConvertToBase(this.Caches[typeof (PX.Objects.IN.InventoryItem)], inventoryItem.InventoryID, uom, nullable.GetValueOrDefault(), INPrecision.NOROUND));
  }

  private bool TryConvertToBase(
    PXCache cache,
    int? inventoryID,
    string uom,
    Decimal value,
    out Decimal? result)
  {
    result = new Decimal?();
    try
    {
      result = new Decimal?(INUnitAttribute.ConvertToBase(cache, inventoryID, uom, value, INPrecision.UNITCOST));
      return true;
    }
    catch (PXUnitConversionException ex)
    {
      return false;
    }
  }

  protected virtual void APPriceWorksheet_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    APPriceWorksheet row = (APPriceWorksheet) e.Row;
    if (row == null)
      return;
    bool isEnabled = row.Status == "H" || row.Status == "N";
    PXAction<APPriceWorksheet> copyPrices = this.copyPrices;
    bool? nullable = row.Hold;
    int num1 = nullable.GetValueOrDefault() & isEnabled ? 1 : 0;
    copyPrices.SetEnabled(num1 != 0);
    PXAction<APPriceWorksheet> calculatePrices = this.calculatePrices;
    nullable = row.Hold;
    int num2 = nullable.GetValueOrDefault() & isEnabled ? 1 : 0;
    calculatePrices.SetEnabled(num2 != 0);
    this.Details.Cache.AllowInsert = isEnabled;
    this.Details.Cache.AllowDelete = isEnabled;
    this.Details.Cache.AllowUpdate = isEnabled;
    this.Document.Cache.AllowDelete = row.Status != "R";
    PXUIFieldAttribute.SetEnabled<APPriceWorksheet.hold>(sender, (object) row, row.Status != "R");
    PXUIFieldAttribute.SetEnabled<APPriceWorksheet.descr>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<APPriceWorksheet.effectiveDate>(sender, (object) row, isEnabled);
    PXCache cache1 = sender;
    APPriceWorksheet data1 = row;
    int num3;
    if (isEnabled)
    {
      if (!(this.APSetup.Current.RetentionType != "L"))
      {
        if (this.APSetup.Current.RetentionType == "L")
        {
          nullable = row.IsPromotional;
          num3 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num3 = 0;
      }
      else
        num3 = 1;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<APPriceWorksheet.expirationDate>(cache1, (object) data1, num3 != 0);
    PXUIFieldAttribute.SetEnabled<APPriceWorksheet.isPromotional>(sender, (object) row, isEnabled);
    PXCache cache2 = sender;
    APPriceWorksheet data2 = row;
    int num4;
    if (isEnabled)
    {
      nullable = row.IsPromotional;
      if (!nullable.GetValueOrDefault())
      {
        num4 = this.APSetup.Current.RetentionType != "L" ? 1 : 0;
        goto label_13;
      }
    }
    num4 = 0;
label_13:
    PXUIFieldAttribute.SetEnabled<APPriceWorksheet.overwriteOverlapping>(cache2, (object) data2, num4 != 0);
    if (!(this.APSetup.Current.RetentionType == "L"))
    {
      nullable = row.IsPromotional;
      if (!nullable.GetValueOrDefault())
        goto label_16;
    }
    row.OverwriteOverlapping = new bool?(true);
label_16:
    if (!(this.APSetup.Current.RetentionType == "L"))
      return;
    nullable = row.IsPromotional;
    if (nullable.GetValueOrDefault())
      return;
    row.ExpirationDate = new System.DateTime?();
  }

  protected virtual void APPriceWorksheet_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    APPriceWorksheet row = (APPriceWorksheet) e.Row;
    if (row == null)
      return;
    APPriceWorksheet apPriceWorksheet = row;
    bool? hold = row.Hold;
    bool flag = false;
    string str = hold.GetValueOrDefault() == flag & hold.HasValue ? "N" : "H";
    apPriceWorksheet.Status = str;
  }

  protected virtual void APPriceWorksheet_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APPriceWorksheet row = (APPriceWorksheet) e.Row;
    if (row == null || !row.IsPromotional.GetValueOrDefault() || row.ExpirationDate.HasValue)
      return;
    sender.RaiseExceptionHandling<APPriceWorksheet.expirationDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[expirationDate]"
    }));
  }

  protected virtual void APPriceWorksheet_EffectiveDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APPriceWorksheet row = (APPriceWorksheet) e.Row;
    if (row == null)
      return;
    if (e.NewValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[effectiveDate]"
      });
    if (!row.IsPromotional.GetValueOrDefault())
      return;
    System.DateTime? expirationDate = row.ExpirationDate;
    if (!expirationDate.HasValue)
      return;
    expirationDate = row.ExpirationDate;
    System.DateTime newValue = (System.DateTime) e.NewValue;
    if ((expirationDate.HasValue ? (expirationDate.GetValueOrDefault() < newValue ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Expiration Date may not be less than Effective Date."));
  }

  protected virtual void APPriceWorksheet_ExpirationDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APPriceWorksheet row = (APPriceWorksheet) e.Row;
    if (row == null)
      return;
    bool? isPromotional = row.IsPromotional;
    if (isPromotional.GetValueOrDefault() && e.NewValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[expirationDate]"
      });
    isPromotional = row.IsPromotional;
    if (!isPromotional.GetValueOrDefault())
      return;
    System.DateTime? effectiveDate = row.EffectiveDate;
    if (!effectiveDate.HasValue)
      return;
    effectiveDate = row.EffectiveDate;
    System.DateTime newValue = (System.DateTime) e.NewValue;
    if ((effectiveDate.HasValue ? (effectiveDate.GetValueOrDefault() > newValue ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Expiration Date may not be less than Effective Date."));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APPriceWorksheetDetail, APPriceWorksheetDetail.subItemID> args)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, (int?) args.Row?.InventoryID);
    if (inventoryItem == null || !inventoryItem.DefaultSubItemOnEntry.GetValueOrDefault())
      return;
    args.NewValue = (object) inventoryItem.DefaultSubItemID;
  }

  protected virtual void APPriceWorksheetDetail_BreakQty_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APPriceWorksheetDetail row = (APPriceWorksheetDetail) e.Row;
  }

  protected virtual void APPriceWorksheetDetail_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    APPriceWorksheetDetail row = (APPriceWorksheetDetail) e.Row;
    if (row == null)
      return;
    if (e.ExternalCall)
    {
      int? nullable = row.VendorID;
      if (nullable.HasValue)
      {
        nullable = row.InventoryID;
        if (nullable.HasValue && row.CuryID != null && this.Document.Current != null && this.Document.Current.EffectiveDate.HasValue)
        {
          Decimal? currentPrice = row.CurrentPrice;
          Decimal num = 0M;
          if (currentPrice.GetValueOrDefault() == num & currentPrice.HasValue)
            row.CurrentPrice = new Decimal?(this.GetItemPrice(row.InventoryID, row.CuryID, this.Document.Current.EffectiveDate, row.UOM));
        }
      }
    }
    if (!this.IsImportFromExcel || this.DuplicateFinder == null)
      return;
    this.DuplicateFinder.AddItem(row);
  }

  protected virtual void APPriceWorksheetDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    APPriceWorksheetDetail row = (APPriceWorksheetDetail) e.Row;
    if (row == null || !e.ExternalCall || sender.ObjectsEqual<APPriceWorksheetDetail.uOM>(e.Row, e.OldRow) && sender.ObjectsEqual<APPriceWorksheetDetail.inventoryID>(e.Row, e.OldRow) && sender.ObjectsEqual<APPriceWorksheetDetail.curyID>(e.Row, e.OldRow))
      return;
    row.CurrentPrice = new Decimal?(this.GetItemPrice(row.InventoryID, row.CuryID, this.Document.Current.EffectiveDate, row.UOM));
  }

  protected virtual void APPriceWorksheetDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    APPriceWorksheetDetail row = (APPriceWorksheetDetail) e.Row;
    if (row == null)
      return;
    APPriceWorksheet current = this.Document.Current;
    Decimal? pendingPrice;
    if ((current != null ? (!current.Hold.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      pendingPrice = row.PendingPrice;
      if (!pendingPrice.HasValue)
      {
        sender.RaiseExceptionHandling<APPriceWorksheetDetail.pendingPrice>((object) row, (object) row.PendingPrice, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", PXErrorLevel.Error, new object[1]
        {
          (object) typeof (APPriceWorksheetDetail.pendingPrice).Name
        }));
        return;
      }
    }
    if (!row.VendorID.HasValue)
    {
      sender.RaiseExceptionHandling<APPriceWorksheetDetail.vendorID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[vendorID]"
      }));
    }
    else
    {
      if (this.Document.Current == null || this.Document.Current.Hold.GetValueOrDefault())
        return;
      pendingPrice = row.PendingPrice;
      if (pendingPrice.HasValue)
        return;
      sender.RaiseExceptionHandling<APPriceWorksheetDetail.pendingPrice>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[pendingPrice]"
      }));
    }
  }

  protected virtual void CopyPricesFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CopyPricesFilter row = (CopyPricesFilter) e.Row;
    if (row == null)
      return;
    bool isVisible = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>();
    bool flag = row.SourceCuryID != row.DestinationCuryID;
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.rateTypeID>(sender, (object) row, isVisible & flag);
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.currencyDate>(sender, (object) row, isVisible & flag);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.sourceCuryID>(sender, (object) row, isVisible);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.destinationCuryID>(sender, (object) row, isVisible);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.currencyDate>(sender, (object) row, isVisible & flag);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.rateTypeID>(sender, (object) row, isVisible & flag);
  }

  protected virtual void CopyPricesFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CopyPricesFilter row = (CopyPricesFilter) e.Row;
    if (row == null)
      return;
    if (!sender.ObjectsEqual<CopyPricesFilter.sourceVendorID>(e.Row, e.OldRow))
    {
      PXResult<PX.Objects.AP.Vendor> pxResult = (PXResult<PX.Objects.AP.Vendor>) PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.acctCD, Equal<Required<PX.Objects.AP.Vendor.acctCD>>>>.Config>.Select((PXGraph) this, (object) row.SourceVendorID);
      if (pxResult != null)
        row.SourceCuryID = ((PX.Objects.AP.Vendor) pxResult).CuryID;
    }
    if (sender.ObjectsEqual<CopyPricesFilter.destinationVendorID>(e.Row, e.OldRow))
      return;
    PXResult<PX.Objects.AP.Vendor> pxResult1 = (PXResult<PX.Objects.AP.Vendor>) PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.acctCD, Equal<Required<PX.Objects.AP.Vendor.acctCD>>>>.Config>.Select((PXGraph) this, (object) row.DestinationVendorID);
    if (pxResult1 == null)
      return;
    row.DestinationCuryID = ((PX.Objects.AP.Vendor) pxResult1).CuryID;
  }

  protected virtual void INNonStockItemXRef_SubItemID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void INNonStockItemXRef_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyINItemXRefBAccountID(e);
  }

  protected virtual void INStockItemXRef_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyINItemXRefBAccountID(e);
  }

  private void VerifyINItemXRefBAccountID(PXFieldVerifyingEventArgs e)
  {
    if (!(((INItemXRef) e.Row).AlternateType != "0VPN") || !(((INItemXRef) e.Row).AlternateType != "0CPN"))
      return;
    e.Cancel = true;
  }

  protected virtual System.Type[] GetAlternativeKeyFields()
  {
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (APPriceWorksheetDetail.vendorID),
      typeof (APPriceWorksheetDetail.inventoryID),
      typeof (APPriceWorksheetDetail.uOM),
      typeof (APPriceWorksheetDetail.siteID),
      typeof (APPriceWorksheetDetail.curyID),
      typeof (APPriceWorksheetDetail.taxID)
    };
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.supportBreakQty>())
      typeList.Add(typeof (APPriceWorksheetDetail.breakQty));
    return typeList.ToArray();
  }

  public DuplicatesSearchEngine<APPriceWorksheetDetail> DuplicateFinder { get; set; }

  private bool DontUpdateExistRecords
  {
    get
    {
      object obj;
      return this.IsImportFromExcel && PXExecutionContext.Current.Bag.TryGetValue("_DONT_UPDATE_EXIST_RECORDS", out obj) && true.Equals(obj);
    }
  }

  protected virtual void ImportAttributeRowImporting(
    object sender,
    PXImportAttribute.RowImportingEventArgs e)
  {
    if (e.Mode == PXImportAttribute.ImportMode.Value.InsertAllRecords)
      return;
    if (this.DuplicateFinder == null)
    {
      List<APPriceWorksheetDetail> list = PXSelectBase<APPriceWorksheetDetail, PXSelect<APPriceWorksheetDetail, Where<APPriceWorksheetDetail.refNbr, Equal<Current<APPriceWorksheetDetail.refNbr>>>>.Config>.Select((PXGraph) this).RowCast<APPriceWorksheetDetail>().ToList<APPriceWorksheetDetail>();
      this.DuplicateFinder = new DuplicatesSearchEngine<APPriceWorksheetDetail>(this.Details.Cache, (IEnumerable<System.Type>) this.GetAlternativeKeyFields(), (ICollection<APPriceWorksheetDetail>) list);
    }
    APPriceWorksheetDetail priceWorksheetDetail = this.DuplicateFinder.Find(e.Values);
    if (priceWorksheetDetail == null)
      return;
    if (e.Keys.Contains((object) "LineID"))
      e.Keys[(object) "LineID"] = (object) priceWorksheetDetail.LineID;
    else
      e.Keys.Add((object) "LineID", (object) priceWorksheetDetail.LineID);
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    this.DuplicateFinder = (DuplicatesSearchEngine<APPriceWorksheetDetail>) null;
  }
}
