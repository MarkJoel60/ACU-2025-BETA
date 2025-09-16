// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceWorksheetMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.Repositories;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

[Serializable]
public class ARPriceWorksheetMaint : 
  PXGraph<ARPriceWorksheetMaint, ARPriceWorksheet>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXSelect<ARPriceWorksheet> Document;
  [PXImport(typeof (ARPriceWorksheet))]
  public PXSelectJoin<ARPriceWorksheetDetail, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<ARPriceWorksheetDetail.customerID>>>, Where<ARPriceWorksheetDetail.refNbr, Equal<Current<ARPriceWorksheet.refNbr>>, And<Where<ARPriceWorksheetDetail.priceType, NotEqual<PriceTypes.customer>, Or<PX.Objects.CR.BAccount.bAccountID, IsNotNull>>>>, OrderBy<Asc<ARPriceWorksheetDetail.priceType, Asc<ARPriceWorksheetDetail.priceCode, Asc<ARPriceWorksheetDetail.inventoryCD, Asc<ARPriceWorksheetDetail.breakQty>>>>>> Details;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXSelect<ARSalesPrice> ARSalesPrices;
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public PXSelect<PX.Objects.CR.BAccount, Where2<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>, And<Match<Current<AccessInfo.userName>>>>> CustomerCode;
  public PXSelect<PX.Objects.AR.Customer> Customer;
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public PXSelect<ARPriceClass> CustPriceClassCode;
  [PXCopyPasteHiddenView]
  public PXSelect<INStockItemXRef> StockCrossReferences;
  [PXCopyPasteHiddenView]
  public PXSelect<INNonStockItemXRef> NonStockCrossReferences;
  [PXCopyPasteHiddenView]
  public PXFilter<CopyPricesFilter> CopyPricesSettings;
  [PXCopyPasteHiddenView]
  public PXFilter<CalculatePricesFilter> CalculatePricesSettings;
  public PXSelect<PX.Objects.CM.CurrencyInfo> CuryInfo;
  public PXSetup<Company> company;
  private readonly bool _loadSalesPricesUsingAlternateID;
  public PXInitializeState<ARPriceWorksheet> initializeState;
  public PXAction<ARPriceWorksheet> putOnHold;
  public PXAction<ARPriceWorksheet> releaseFromHold;
  public PXAction<ARPriceWorksheet> ReleasePriceWorksheet;
  public PXAction<ARPriceWorksheet> copyPrices;
  public PXAction<ARPriceWorksheet> calculatePrices;
  protected readonly string viewPriceCode;
  private string _priceTypeName;

  protected CustomerRepository CustomerRepository { get; }

  public ARPriceWorksheetMaint()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    this.CustomerRepository = new CustomerRepository((PXGraph) this);
    this._loadSalesPricesUsingAlternateID = PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() && ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.LoadSalesPricesUsingAlternateID.GetValueOrDefault();
    PXCacheEx.Adjust<PXRestrictorAttribute>(((PXSelectBase) this.Details).Cache, (object) null).For<ARPriceWorksheetDetail.inventoryID>((Action<PXRestrictorAttribute>) (ra => ra.CacheGlobal = true));
    ((PXSelectBase) this.Details).View.Attributes.OfType<PXImportAttribute>().First<PXImportAttribute>().RowImporting += new EventHandler<PXImportAttribute.RowImportingEventArgs>(this.ImportAttributeRowImporting);
  }

  [PXMergeAttributes]
  [PXDBCalced(typeof (Use<PX.Objects.IN.InventoryItem.taxCategoryID>.AsString), typeof (string))]
  public virtual void _(
    PX.Data.Events.CacheAttached<ARPriceWorksheetDetail.taxCategoryID> e)
  {
  }

  public string GetPriceType(string viewname)
  {
    AddItemParameters current = (AddItemParameters) ((PXGraph) this).Caches[typeof (AddItemParameters)].Current;
    string priceType = "C";
    if (viewname.Contains(typeof (ARPriceWorksheetDetail).Name) && ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).Current != null)
      priceType = ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).Current.PriceType;
    if (viewname.Contains(typeof (AddItemParameters).Name) && current != null)
      priceType = current.PriceType;
    if (viewname.Contains(typeof (CopyPricesFilter).Name) && ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current != null)
      priceType = !viewname.Contains(typeof (CopyPricesFilter.sourcePriceCode).Name.First<char>().ToString().ToUpper() + string.Join<char>(string.Empty, typeof (CopyPricesFilter.sourcePriceCode).Name.Skip<char>(1))) ? ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.DestinationPriceType : ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.SourcePriceType;
    return priceType;
  }

  public virtual void Persist()
  {
    this.CheckForIncorrectPriceTypeDetails();
    this.CheckForEmptyPendingPriceDetails();
    this.CheckForDuplicateDetails();
    ((PXGraph) this).Persist();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable releasePriceWorksheet(PXAdapter adapter)
  {
    List<ARPriceWorksheet> arPriceWorksheetList = new List<ARPriceWorksheet>();
    if (((PXSelectBase<ARPriceWorksheet>) this.Document).Current != null)
    {
      ((PXAction) this.Save).Press();
      arPriceWorksheetList.Add(((PXSelectBase<ARPriceWorksheet>) this.Document).Current);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CreleasePriceWorksheet\u003Eb__27_0)));
    }
    return (IEnumerable) arPriceWorksheetList;
  }

  public static void ReleaseWorksheet(ARPriceWorksheet priceWorksheet)
  {
    PXGraph.CreateInstance<ARPriceWorksheetMaint>().ReleaseWorksheetImpl(priceWorksheet);
  }

  public virtual void ReleaseWorksheetImpl(ARPriceWorksheet priceWorksheet)
  {
    ((PXSelectBase<ARPriceWorksheet>) this.Document).Current = priceWorksheet;
    bool isPromotional = priceWorksheet.IsPromotional.GetValueOrDefault();
    bool isFairValue = priceWorksheet != null && priceWorksheet.IsFairValue.GetValueOrDefault();
    string taxCalcMode = priceWorksheet?.TaxCalcMode;
    System.Type[] byFields1 = new System.Type[7]
    {
      typeof (ARSalesPrice.priceType),
      typeof (ARSalesPrice.customerID),
      typeof (ARSalesPrice.custPriceClassID),
      typeof (ARSalesPrice.siteID),
      typeof (ARSalesPrice.uOM),
      typeof (ARSalesPrice.curyID),
      typeof (ARSalesPrice.breakQty)
    };
    System.Type[] byFields2 = new System.Type[1]
    {
      typeof (ARSalesPrice.inventoryID)
    };
    GroupedCollection<ARSalesPrice, GroupedCollection<ARSalesPrice, List<ARSalesPrice>>> collection = new List<ARSalesPrice>().SplitBy<ARSalesPrice, List<ARSalesPrice>>(GraphHelper.Caches<ARSalesPrice>((PXGraph) this), byFields1).SplitBy<ARSalesPrice, List<ARSalesPrice>>(byFields2, (GroupItemsLoadHandler<ARSalesPrice>) (group => this.LoadInventoryPrices(group, isPromotional, isFairValue)));
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      using (new GroupedCollectionScope<ARSalesPrice>((IGroupedCollection<ARSalesPrice>) collection))
      {
        foreach (ARPriceWorksheetDetail priceLine in (IEnumerable<ARPriceWorksheetDetail>) ((IEnumerable<ARPriceWorksheetDetail>) ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).SelectMain(Array.Empty<object>())).OrderBy<ARPriceWorksheetDetail, int?>((Func<ARPriceWorksheetDetail, int?>) (x => x.InventoryID)))
        {
          ARSalesPrice salesPrice = this.CreateSalesPrice(priceLine, new bool?(isPromotional), new DateTime?(), new DateTime?(), taxCalcMode);
          IOrderedEnumerable<ARSalesPrice> source = collection.GetItems(salesPrice).OrderBy<ARSalesPrice, DateTime?>((Func<ARSalesPrice, DateTime?>) (x => x.EffectiveDate)).ThenBy<ARSalesPrice, DateTime?>((Func<ARSalesPrice, DateTime?>) (x => x.ExpirationDate));
          PXResultset<ARSalesPrice> salesPrices = new PXResultset<ARSalesPrice>();
          salesPrices.AddRange(source.Select<ARSalesPrice, PXResult<ARSalesPrice>>((Func<ARSalesPrice, PXResult<ARSalesPrice>>) (x => new PXResult<ARSalesPrice>(x))));
          this.CreateSalesPricesOnWorksheetRelease(priceLine, salesPrices);
        }
      }
      priceWorksheet.Status = "R";
      ((PXSelectBase<ARPriceWorksheet>) this.Document).Update(priceWorksheet);
      ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.Status = "R";
      ((PXGraph) this).Persist();
      transactionScope.Complete();
    }
  }

  protected virtual IEnumerable<ARSalesPrice> LoadInventoryPrices(
    ARSalesPrice group,
    bool isPromotional,
    bool isFairValue)
  {
    return (IEnumerable<ARSalesPrice>) ((PXSelectBase<ARSalesPrice>) new PXViewOf<ARSalesPrice>.BasedOn<SelectFromBase<ARSalesPrice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARSalesPrice.inventoryID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARSalesPrice.isPromotionalPrice, Equal<P.AsBool>>>>>.And<BqlOperand<ARSalesPrice.isFairValue, IBqlBool>.IsEqual<P.AsBool>>>>>.ReadOnly((PXGraph) this)).SelectMain(new object[3]
    {
      (object) group.InventoryID,
      (object) isPromotional,
      (object) isFairValue
    });
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  protected virtual void CreateSalesPricesOnWorksheetRelease(
    PXResult<ARPriceWorksheetDetail, PX.Objects.IN.InventoryItem> row)
  {
    this.CreateSalesPricesOnWorksheetRelease(PXResult<ARPriceWorksheetDetail, PX.Objects.IN.InventoryItem>.op_Implicit(row), this.GetSalesPricesByPriceLineForWorksheetRelease(PXResult<ARPriceWorksheetDetail, PX.Objects.IN.InventoryItem>.op_Implicit(row)));
  }

  protected virtual void CreateSalesPricesOnWorksheetRelease(
    ARPriceWorksheetDetail priceLine,
    PXResultset<ARSalesPrice> salesPrices)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, priceLine.InventoryID);
    DateTime? expirationDate = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.ExpirationDate;
    DateTime? effectiveDate = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.EffectiveDate;
    if (!((PXSelectBase<ARPriceWorksheet>) this.Document).Current.IsPromotional.GetValueOrDefault() || !expirationDate.HasValue)
      this.ProcessReleaseForNonPromotionalSalesPrices(salesPrices, priceLine);
    else
      this.ProcessReleaseForPromotionalSalesPrices(salesPrices, priceLine);
    int? nullable1;
    if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType == "F")
    {
      nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.NumberOfMonths;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        foreach (PXResult<ARSalesPrice> salesPrice in salesPrices)
        {
          ARSalesPrice arSalesPrice = PXResult<ARSalesPrice>.op_Implicit(salesPrice);
          nullable1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.NumberOfMonths;
          int valueOrDefault = nullable1.GetValueOrDefault();
          DateTime? nullable2 = arSalesPrice.ExpirationDate;
          if (nullable2.HasValue)
          {
            DateTime dateTime = arSalesPrice.ExpirationDate.Value.AddMonths(valueOrDefault);
            nullable2 = effectiveDate;
            if ((nullable2.HasValue ? (dateTime < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Delete(arSalesPrice);
          }
        }
      }
    }
    if (!this._loadSalesPricesUsingAlternateID || Str.IsNullOrEmpty(priceLine.AlternateID) || PriceWorksheetAlternateItemAttribute.XRefsExists(((PXSelectBase) this.Details).Cache, (object) priceLine))
      return;
    PXCache pxCache = inventoryItem.StkItem.GetValueOrDefault() ? ((PXSelectBase) this.StockCrossReferences).Cache : ((PXSelectBase) this.NonStockCrossReferences).Cache;
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

  protected virtual PXResultset<ARSalesPrice> GetSalesPricesByPriceLineForWorksheetRelease(
    ARPriceWorksheetDetail priceLine)
  {
    return PXSelectBase<ARSalesPrice, PXSelect<ARSalesPrice, Where<ARSalesPrice.priceType, Equal<Required<ARSalesPrice.priceType>>, And2<Where2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>, And<ARSalesPrice.priceType, Equal<PriceTypes.customer>>>, Or2<Where<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>, And<ARSalesPrice.priceType, Equal<PriceTypes.customerPriceClass>>>, Or<Where<ARSalesPrice.custPriceClassID, IsNull, And<ARSalesPrice.customerID, IsNull, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>>>>>>>, And<ARSalesPrice.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPrice.siteID>>, Or<ARSalesPrice.siteID, IsNull, And<Required<ARSalesPrice.siteID>, IsNull>>>, And<ARSalesPrice.uOM, Equal<Required<ARSalesPrice.uOM>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And<ARSalesPrice.breakQty, Equal<Required<ARSalesPrice.breakQty>>, And<ARSalesPrice.isPromotionalPrice, Equal<Required<ARSalesPrice.isPromotionalPrice>>, And<ARSalesPrice.isFairValue, Equal<Required<ARSalesPrice.isFairValue>>>>>>>>>>>, OrderBy<Asc<ARSalesPrice.effectiveDate, Asc<ARSalesPrice.expirationDate>>>>.Config>.Select((PXGraph) this, new object[11]
    {
      (object) priceLine.PriceType,
      (object) priceLine.CustomerID,
      (object) priceLine.CustPriceClassID,
      (object) priceLine.InventoryID,
      (object) priceLine.SiteID,
      (object) priceLine.SiteID,
      (object) priceLine.UOM,
      (object) priceLine.CuryID,
      (object) priceLine.BreakQty,
      (object) ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.IsPromotional.GetValueOrDefault(),
      (object) ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.IsFairValue.GetValueOrDefault()
    });
  }

  protected virtual void ProcessReleaseForNonPromotionalSalesPrices(
    PXResultset<ARSalesPrice> salesPrices,
    ARPriceWorksheetDetail priceLine)
  {
    bool flag = true;
    DateTime? expirationDate1 = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.ExpirationDate;
    DateTime? effectiveDate1 = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.EffectiveDate;
    string taxCalcMode = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.TaxCalcMode;
    bool? isFairValue = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.IsFairValue;
    bool? isProrated = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.IsProrated;
    bool? discountable = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.Discountable;
    if (salesPrices.Count == 0)
    {
      ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Insert(this.CreateSalesPrice(priceLine, new bool?(false), isFairValue, isProrated, effectiveDate1, expirationDate1, taxCalcMode));
    }
    else
    {
      foreach (PXResult<ARSalesPrice> salesPrice1 in salesPrices)
      {
        ARSalesPrice salesPrice2 = PXResult<ARSalesPrice>.op_Implicit(salesPrice1);
        DateTime? nullable1;
        DateTime? nullable2;
        if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType == "F")
        {
          if (!((PXSelectBase<ARPriceWorksheet>) this.Document).Current.OverwriteOverlapping.GetValueOrDefault())
          {
            nullable1 = salesPrice2.EffectiveDate;
            nullable2 = effectiveDate1;
            if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              nullable2 = salesPrice2.ExpirationDate;
              nullable1 = effectiveDate1;
              if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                goto label_10;
            }
            nullable1 = salesPrice2.EffectiveDate;
            nullable2 = effectiveDate1;
            if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              nullable2 = salesPrice2.ExpirationDate;
              if (nullable2.HasValue)
                goto label_11;
            }
            else
              goto label_11;
label_10:
            flag = false;
          }
label_11:
          this.ProcessNonPromotionalPriceForFixedNumOfMonthsRetentionType(salesPrice2, priceLine, expirationDate1, effectiveDate1, isFairValue, isProrated);
        }
        else
        {
          nullable2 = salesPrice2.EffectiveDate;
          nullable1 = effectiveDate1;
          if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          {
            DateTime dateTime1 = effectiveDate1.Value;
            nullable1 = salesPrice2.ExpirationDate;
            DateTime dateTime2 = effectiveDate1.Value.AddDays(-1.0);
            if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) == 0)
            {
              nullable1 = salesPrice2.EffectiveDate;
              nullable2 = effectiveDate1;
              if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                nullable2 = salesPrice2.ExpirationDate;
                if (!nullable2.HasValue)
                  goto label_22;
              }
              nullable2 = salesPrice2.EffectiveDate;
              if (!nullable2.HasValue)
              {
                nullable2 = salesPrice2.ExpirationDate;
                if (!nullable2.HasValue)
                  goto label_22;
              }
              nullable2 = salesPrice2.EffectiveDate;
              nullable1 = effectiveDate1;
              if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
              {
                nullable1 = salesPrice2.EffectiveDate;
                if (nullable1.HasValue)
                  continue;
              }
              nullable1 = effectiveDate1;
              nullable2 = salesPrice2.ExpirationDate;
              if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
                continue;
label_22:
              DateTime dateTime3 = effectiveDate1.Value;
              salesPrice2.ExpirationDate = new DateTime?(effectiveDate1.Value.AddDays(-1.0));
              ARSalesPrice arSalesPrice = salesPrice2;
              nullable2 = new DateTime?();
              DateTime? nullable3 = nullable2;
              arSalesPrice.EffectiveDate = nullable3;
              ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(salesPrice2);
              continue;
            }
          }
          ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Delete(salesPrice2);
        }
      }
      if (!flag)
        return;
      if (((PXSelectBase<ARPriceWorksheet>) this.Document).Current.OverwriteOverlapping.GetValueOrDefault() || ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType == "L")
      {
        ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Insert(this.CreateSalesPrice(priceLine, new bool?(false), isFairValue, isProrated, effectiveDate1, expirationDate1, taxCalcMode));
      }
      else
      {
        ARSalesPrice worksheetRelease = this.GetMinSalesPriceForNonPromotionalPricesWorksheetRelease(priceLine, effectiveDate1);
        DateTime? nullable;
        if (worksheetRelease == null)
        {
          nullable = new DateTime?();
        }
        else
        {
          DateTime? effectiveDate2 = worksheetRelease.EffectiveDate;
          ref DateTime? local = ref effectiveDate2;
          nullable = local.HasValue ? new DateTime?(local.GetValueOrDefault().AddDays(-1.0)) : new DateTime?();
        }
        DateTime? expirationDate2 = nullable ?? expirationDate1;
        ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Insert(this.CreateSalesPrice(priceLine, new bool?(false), isFairValue, isProrated, effectiveDate1, expirationDate2, taxCalcMode));
      }
    }
  }

  protected virtual void ProcessNonPromotionalPriceForFixedNumOfMonthsRetentionType(
    ARSalesPrice salesPrice,
    ARPriceWorksheetDetail priceLine,
    DateTime? worksheetExpirationDate,
    DateTime? worksheetEffectiveDate)
  {
    this.ProcessNonPromotionalPriceForFixedNumOfMonthsRetentionType(salesPrice, priceLine, worksheetExpirationDate, worksheetEffectiveDate, new bool?(false), new bool?(false));
  }

  protected virtual void ProcessNonPromotionalPriceForFixedNumOfMonthsRetentionType(
    ARSalesPrice salesPrice,
    ARPriceWorksheetDetail priceLine,
    DateTime? worksheetExpirationDate,
    DateTime? worksheetEffectiveDate,
    bool? isFairValue,
    bool? isProrated)
  {
    if (((PXSelectBase<ARPriceWorksheet>) this.Document).Current.OverwriteOverlapping.GetValueOrDefault())
    {
      DateTime? nullable1;
      if (!worksheetExpirationDate.HasValue)
      {
        DateTime? effectiveDate = salesPrice.EffectiveDate;
        nullable1 = worksheetEffectiveDate;
        if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_6;
      }
      DateTime? nullable2;
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
      DateTime dateTime = worksheetEffectiveDate.Value;
      salesPrice.ExpirationDate = new DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
      ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(salesPrice);
      return;
label_6:
      ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Delete(salesPrice);
    }
    else
    {
      DateTime? effectiveDate = salesPrice.EffectiveDate;
      DateTime? nullable3 = worksheetEffectiveDate;
      DateTime? nullable4;
      if ((effectiveDate.HasValue & nullable3.HasValue ? (effectiveDate.GetValueOrDefault() < nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable3 = salesPrice.ExpirationDate;
        nullable4 = worksheetEffectiveDate;
        if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          DateTime dateTime = worksheetEffectiveDate.Value;
          ARSalesPrice copy = (ARSalesPrice) ((PXSelectBase) this.ARSalesPrices).Cache.CreateCopy((object) salesPrice);
          salesPrice.EffectiveDate = worksheetEffectiveDate;
          this.UpdateSalesPriceFromPriceLine(salesPrice, priceLine);
          ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(salesPrice);
          copy.ExpirationDate = new DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
          copy.RecordID = new int?();
          ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Insert(copy);
          return;
        }
      }
      nullable4 = salesPrice.EffectiveDate;
      nullable3 = worksheetEffectiveDate;
      if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() <= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable3 = salesPrice.ExpirationDate;
        if (!nullable3.HasValue)
        {
          DateTime dateTime = worksheetEffectiveDate.Value;
          salesPrice.ExpirationDate = new DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
          ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(salesPrice);
          ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Insert(this.CreateSalesPrice(priceLine, new bool?(false), isFairValue, isProrated, worksheetEffectiveDate, worksheetExpirationDate, ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.TaxCalcMode));
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
          this.UpdateSalesPriceFromPriceLine(salesPrice, priceLine);
          ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(salesPrice);
          return;
        }
      }
      nullable3 = salesPrice.EffectiveDate;
      if (!nullable3.HasValue)
      {
        nullable3 = salesPrice.ExpirationDate;
        if (!nullable3.HasValue)
          goto label_29;
      }
      nullable3 = salesPrice.EffectiveDate;
      if (nullable3.HasValue)
        return;
      nullable3 = salesPrice.ExpirationDate;
      nullable4 = worksheetEffectiveDate;
      if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
label_29:
      salesPrice.ExpirationDate = new DateTime?(worksheetEffectiveDate.Value.AddDays(-1.0));
      ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(salesPrice);
    }
  }

  protected virtual void UpdateSalesPriceFromPriceLine(
    ARSalesPrice salesPrice,
    ARPriceWorksheetDetail priceLine)
  {
    salesPrice.SalesPrice = priceLine.PendingPrice;
  }

  protected virtual ARSalesPrice GetMinSalesPriceForNonPromotionalPricesWorksheetRelease(
    ARPriceWorksheetDetail priceLine,
    DateTime? effectiveDate)
  {
    return PXResultset<ARSalesPrice>.op_Implicit(PXSelectBase<ARSalesPrice, PXSelect<ARSalesPrice, Where<ARSalesPrice.priceType, Equal<Required<ARSalesPrice.priceType>>, And2<Where2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>, And<ARSalesPrice.custPriceClassID, IsNull>>, Or2<Where<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>, And<ARSalesPrice.customerID, IsNull>>, Or<Where<ARSalesPrice.custPriceClassID, IsNull, And<ARSalesPrice.customerID, IsNull>>>>>, And<ARSalesPrice.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPrice.siteID>>, Or<ARSalesPrice.siteID, IsNull, And<Required<ARSalesPrice.siteID>, IsNull>>>, And<ARSalesPrice.uOM, Equal<Required<ARSalesPrice.uOM>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And<ARSalesPrice.breakQty, Equal<Required<ARSalesPrice.breakQty>>, And<ARSalesPrice.effectiveDate, IsNotNull, And<Where<ARSalesPrice.effectiveDate, GreaterEqual<Required<ARSalesPrice.effectiveDate>>>>>>>>>>>>, OrderBy<Asc<ARSalesPrice.effectiveDate>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[10]
    {
      (object) priceLine.PriceType,
      (object) priceLine.CustomerID,
      (object) priceLine.CustPriceClassID,
      (object) priceLine.InventoryID,
      (object) priceLine.SiteID,
      (object) priceLine.SiteID,
      (object) priceLine.UOM,
      (object) priceLine.CuryID,
      (object) priceLine.BreakQty,
      (object) effectiveDate
    }));
  }

  protected virtual void ProcessReleaseForPromotionalSalesPrices(
    PXResultset<ARSalesPrice> salesPrices,
    ARPriceWorksheetDetail priceLine)
  {
    DateTime? expirationDate1 = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.ExpirationDate;
    DateTime? effectiveDate1 = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.EffectiveDate;
    string taxCalcMode = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.TaxCalcMode;
    foreach (PXResult<ARSalesPrice> salesPrice in salesPrices)
    {
      ARSalesPrice arSalesPrice = PXResult<ARSalesPrice>.op_Implicit(salesPrice);
      DateTime? nullable1 = arSalesPrice.EffectiveDate;
      DateTime? nullable2 = effectiveDate1;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        DateTime? expirationDate2 = arSalesPrice.ExpirationDate;
        nullable1 = expirationDate1;
        if ((expirationDate2.HasValue & nullable1.HasValue ? (expirationDate2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Delete(arSalesPrice);
          continue;
        }
      }
      nullable1 = arSalesPrice.EffectiveDate;
      DateTime? nullable3 = effectiveDate1;
      if ((nullable1.HasValue & nullable3.HasValue ? (nullable1.GetValueOrDefault() <= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        DateTime? expirationDate3 = arSalesPrice.ExpirationDate;
        nullable1 = expirationDate1;
        if ((expirationDate3.HasValue & nullable1.HasValue ? (expirationDate3.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable1 = arSalesPrice.ExpirationDate;
          DateTime? nullable4 = effectiveDate1;
          if ((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            DateTime dateTime = effectiveDate1.Value;
            arSalesPrice.ExpirationDate = new DateTime?(effectiveDate1.Value.AddDays(-1.0));
            ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(arSalesPrice);
            continue;
          }
        }
      }
      DateTime? effectiveDate2 = arSalesPrice.EffectiveDate;
      nullable1 = effectiveDate1;
      if ((effectiveDate2.HasValue & nullable1.HasValue ? (effectiveDate2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = arSalesPrice.EffectiveDate;
        DateTime? nullable5 = expirationDate1;
        if ((nullable1.HasValue & nullable5.HasValue ? (nullable1.GetValueOrDefault() < nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          DateTime? expirationDate4 = arSalesPrice.ExpirationDate;
          nullable1 = expirationDate1;
          if ((expirationDate4.HasValue & nullable1.HasValue ? (expirationDate4.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            DateTime dateTime = expirationDate1.Value;
            arSalesPrice.EffectiveDate = new DateTime?(expirationDate1.Value.AddDays(1.0));
            ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(arSalesPrice);
            continue;
          }
        }
      }
      nullable1 = arSalesPrice.EffectiveDate;
      DateTime? nullable6 = effectiveDate1;
      if ((nullable1.HasValue & nullable6.HasValue ? (nullable1.GetValueOrDefault() <= nullable6.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        DateTime? expirationDate5 = arSalesPrice.ExpirationDate;
        nullable1 = expirationDate1;
        if ((expirationDate5.HasValue & nullable1.HasValue ? (expirationDate5.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable1 = arSalesPrice.ExpirationDate;
          DateTime? nullable7 = effectiveDate1;
          if ((nullable1.HasValue & nullable7.HasValue ? (nullable1.GetValueOrDefault() > nullable7.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            DateTime dateTime = effectiveDate1.Value;
            arSalesPrice.ExpirationDate = new DateTime?(effectiveDate1.Value.AddDays(-1.0));
            ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Update(arSalesPrice);
          }
        }
      }
    }
    ((PXSelectBase<ARSalesPrice>) this.ARSalesPrices).Insert(this.CreateSalesPrice(priceLine, new bool?(true), effectiveDate1, expirationDate1, taxCalcMode));
  }

  protected virtual ARSalesPrice CreateSalesPrice(
    ARPriceWorksheetDetail priceLine,
    bool? isPromotional,
    DateTime? effectiveDate,
    DateTime? expirationDate)
  {
    return this.CreateSalesPrice(priceLine, isPromotional, effectiveDate, expirationDate, "U");
  }

  protected virtual ARSalesPrice CreateSalesPrice(
    ARPriceWorksheetDetail priceLine,
    bool? isPromotional,
    DateTime? effectiveDate,
    DateTime? expirationDate,
    string taxCalcMode)
  {
    return this.CreateSalesPrice(priceLine, isPromotional, new bool?(false), new bool?(false), effectiveDate, expirationDate, taxCalcMode);
  }

  protected virtual ARSalesPrice CreateSalesPrice(
    ARPriceWorksheetDetail priceLine,
    bool? isPromotional,
    bool? isFairValue,
    bool? isProrated,
    DateTime? effectiveDate,
    DateTime? expirationDate)
  {
    return this.CreateSalesPrice(priceLine, isPromotional, isFairValue, isProrated, effectiveDate, expirationDate, "U");
  }

  protected virtual ARSalesPrice CreateSalesPrice(
    ARPriceWorksheetDetail priceLine,
    bool? isPromotional,
    bool? isFairValue,
    bool? isProrated,
    DateTime? effectiveDate,
    DateTime? expirationDate,
    string taxCalcMode)
  {
    return new ARSalesPrice()
    {
      PriceType = priceLine.PriceType,
      CustomerID = priceLine.CustomerID,
      CustPriceClassID = priceLine.CustPriceClassID,
      InventoryID = priceLine.InventoryID,
      AlternateID = priceLine.AlternateID,
      SiteID = priceLine.SiteID,
      UOM = priceLine.UOM,
      BreakQty = priceLine.BreakQty,
      SalesPrice = priceLine.PendingPrice,
      SkipLineDiscounts = priceLine.SkipLineDiscounts,
      CuryID = priceLine.CuryID,
      TaxCalcMode = taxCalcMode,
      IsPromotionalPrice = isPromotional,
      IsFairValue = isFairValue,
      IsProrated = isProrated,
      EffectiveDate = effectiveDate,
      ExpirationDate = expirationDate
    };
  }

  public virtual IEnumerable<PXDataRecord> ProviderSelect(
    BqlCommand command,
    int topCount,
    params PXDataValue[] pars)
  {
    return ((PXGraph) this).ProviderSelect(command, topCount, pars);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CopyPrices(PXAdapter adapter)
  {
    if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current != null)
      ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.TaxCalcMode = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.TaxCalcMode;
    if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).AskExt() == 1 && ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current != null)
    {
      if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.SourcePriceType != "B" && ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.SourcePriceCode == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.sourcePriceCode>(adapter);
      if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.SourceCuryID == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.sourceCuryID>(adapter);
      if (!((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.EffectiveDate.HasValue)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.effectiveDate>(adapter);
      if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.DestinationPriceType != "B" && ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.DestinationPriceCode == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.destinationPriceCode>(adapter);
      if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.DestinationCuryID == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.destinationCuryID>(adapter);
      if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.DestinationCuryID != ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.SourceCuryID && ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.RateTypeID == null)
        return this.SetErrorOnEmptyFieldAndReturn<CopyPricesFilter.rateTypeID>(adapter);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CCopyPrices\u003Eb__46_0)));
    }
    return adapter.Get();
  }

  private IEnumerable SetErrorOnEmptyFieldAndReturn<TField>(PXAdapter adapter) where TField : IBqlField
  {
    ((PXSelectBase) this.CopyPricesSettings).Cache.RaiseExceptionHandling<TField>((object) ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{typeof (TField).Name}]"
    }));
    return adapter.Get();
  }

  public static void CopyPricesProc(ARPriceWorksheet priceWorksheet, CopyPricesFilter copyFilter)
  {
    ARPriceWorksheetMaint instance = PXGraph.CreateInstance<ARPriceWorksheetMaint>();
    ((PXSelectBase<ARPriceWorksheet>) instance.Document).Update((ARPriceWorksheet) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) priceWorksheet));
    instance.CopyPricesInternalProcessing(copyFilter);
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
  }

  protected virtual void CopyPricesInternalProcessing(CopyPricesFilter copyFilter)
  {
    ((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current = copyFilter;
    string sourcePriceCode = copyFilter.SourcePriceCode;
    if (copyFilter.SourcePriceType == "C")
    {
      PX.Objects.AR.Customer byCd = this.CustomerRepository.FindByCD(copyFilter.SourcePriceCode);
      if (byCd != null)
        sourcePriceCode = byCd.BAccountID.ToString();
    }
    string destinationPriceCode = copyFilter.DestinationPriceCode;
    if (copyFilter.DestinationPriceType == "C")
    {
      PX.Objects.AR.Customer byCd = this.CustomerRepository.FindByCD(copyFilter.DestinationPriceCode);
      if (byCd != null)
        destinationPriceCode = byCd.BAccountID.ToString();
    }
    EnumerableExtensions.ForEach<ARPriceWorksheetDetail>(((IEnumerable<PXResult<ARSalesPrice>>) this.GetPricesForCopying(copyFilter, sourcePriceCode)).AsEnumerable<PXResult<ARSalesPrice>>().Select<PXResult<ARSalesPrice>, ARPriceWorksheetDetail>((Func<PXResult<ARSalesPrice>, ARPriceWorksheetDetail>) (price => this.CreateWorksheetDetailFromSalesPriceOnCopying(PXResult<ARSalesPrice>.op_Implicit(price), copyFilter, destinationPriceCode))), (Action<ARPriceWorksheetDetail>) (newLine => ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).Update(newLine)));
    ((PXAction) this.Save).Press();
    ((PXSelectBase) this.CopyPricesSettings).Cache.Clear();
  }

  protected virtual PXResultset<ARSalesPrice> GetPricesForCopying(
    CopyPricesFilter copyFilter,
    string sourcePriceCode)
  {
    object[] objArray = new object[14]
    {
      (object) copyFilter.TaxCalcMode,
      (object) copyFilter.SourcePriceType,
      copyFilter.SourcePriceType == "C" ? (object) sourcePriceCode : (object) (string) null,
      copyFilter.SourcePriceType == "P" ? (object) sourcePriceCode : (object) (string) null,
      (object) copyFilter.SourceCuryID,
      (object) copyFilter.SourceSiteID,
      (object) copyFilter.SourceSiteID,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    };
    bool? nullable = copyFilter.IsPromotional;
    objArray[7] = (object) nullable.GetValueOrDefault();
    nullable = copyFilter.IsFairValue;
    objArray[8] = (object) nullable.GetValueOrDefault();
    nullable = copyFilter.IsProrated;
    objArray[9] = (object) nullable.GetValueOrDefault();
    nullable = copyFilter.Discountable;
    objArray[10] = (object) nullable.GetValueOrDefault();
    objArray[11] = (object) copyFilter.EffectiveDate;
    objArray[12] = (object) copyFilter.EffectiveDate;
    objArray[13] = (object) copyFilter.EffectiveDate;
    return PXSelectBase<ARSalesPrice, PXSelectJoinGroupBy<ARSalesPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>>>, Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.toDelete>, And2<Where<ARSalesPrice.taxCalcMode, Equal<Required<ARPriceWorksheet.taxCalcMode>>, Or<Not<FeatureInstalled<FeaturesSet.netGrossEntryMode>>>>, And<ARSalesPrice.priceType, Equal<Required<ARSalesPrice.priceType>>, And2<Where2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>, And<ARSalesPrice.custPriceClassID, IsNull>>, Or2<Where<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>, And<ARSalesPrice.customerID, IsNull>>, Or<Where<ARSalesPrice.custPriceClassID, IsNull, And<ARSalesPrice.customerID, IsNull>>>>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And2<AreSame<ARSalesPrice.siteID, Required<ARSalesPrice.siteID>>, And<ARSalesPrice.isPromotionalPrice, Equal<Required<ARSalesPrice.isPromotionalPrice>>, And<ARSalesPrice.isFairValue, Equal<Required<ARSalesPrice.isFairValue>>, And<ARSalesPrice.isProrated, Equal<Required<ARSalesPrice.isProrated>>, And<ARSalesPrice.discountable, Equal<Required<ARSalesPrice.discountable>>, And<Where2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, IsNull>>, Or<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, Greater<Required<ARSalesPrice.effectiveDate>>>>>>>>>>>>>>>>>>, Aggregate<GroupBy<ARSalesPrice.priceType, GroupBy<ARSalesPrice.customerID, GroupBy<ARSalesPrice.custPriceClassID, GroupBy<ARSalesPrice.inventoryID, GroupBy<ARSalesPrice.uOM, GroupBy<ARSalesPrice.breakQty, GroupBy<ARSalesPrice.curyID, GroupBy<ARSalesPrice.siteID>>>>>>>>>, OrderBy<Asc<ARSalesPrice.effectiveDate, Asc<ARSalesPrice.expirationDate>>>>.Config>.Select((PXGraph) this, objArray);
  }

  protected virtual ARPriceWorksheetDetail CreateWorksheetDetailFromSalesPriceOnCopying(
    ARSalesPrice salesPrice,
    CopyPricesFilter copyFilter,
    string destinationPriceCode)
  {
    ARPriceWorksheetDetail salesPriceOnCopying = new ARPriceWorksheetDetail()
    {
      PriceType = copyFilter.DestinationPriceType,
      PriceCode = copyFilter.DestinationPriceCode?.TrimEnd(),
      InventoryID = salesPrice.InventoryID,
      SiteID = copyFilter.DestinationSiteID ?? salesPrice.SiteID,
      UOM = salesPrice.UOM,
      BreakQty = salesPrice.BreakQty,
      CuryID = copyFilter.DestinationCuryID,
      SkipLineDiscounts = salesPrice.SkipLineDiscounts
    };
    salesPriceOnCopying.CurrentPrice = !(copyFilter.SourceCuryID == copyFilter.DestinationCuryID) ? new Decimal?(ARPriceWorksheetMaint.ConvertSalesPrice(this, copyFilter.RateTypeID, copyFilter.SourceCuryID, copyFilter.DestinationCuryID, copyFilter.CurrencyDate, salesPrice.SalesPrice.GetValueOrDefault())) : new Decimal?(salesPrice.SalesPrice.GetValueOrDefault());
    if (((PXSelectBase<CopyPricesFilter>) this.CopyPricesSettings).Current.DestinationPriceType == "C")
      salesPriceOnCopying.CustomerID = new int?(Convert.ToInt32(destinationPriceCode));
    else
      salesPriceOnCopying.CustPriceClassID = destinationPriceCode;
    return salesPriceOnCopying;
  }

  public static Decimal ConvertSalesPrice(
    ARPriceWorksheetMaint graph,
    string curyRateTypeID,
    string fromCuryID,
    string toCuryID,
    DateTime? curyEffectiveDate,
    Decimal salesPrice)
  {
    Decimal curyval = salesPrice;
    if (curyRateTypeID == null || curyRateTypeID == null || !curyEffectiveDate.HasValue)
      return curyval;
    PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) graph.CuryInfo).Cache.Update((object) new PX.Objects.CM.CurrencyInfo()
    {
      BaseCuryID = fromCuryID,
      CuryID = toCuryID,
      CuryRateTypeID = curyRateTypeID
    });
    info.SetCuryEffDate(((PXSelectBase) graph.CuryInfo).Cache, (object) curyEffectiveDate);
    ((PXSelectBase) graph.CuryInfo).Cache.Update((object) info);
    PXCurrencyAttribute.CuryConvCury(((PXSelectBase) graph.CuryInfo).Cache, info, salesPrice, out curyval);
    ((PXSelectBase) graph.CuryInfo).Cache.Delete((object) info);
    return curyval;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CalculatePrices(PXAdapter adapter)
  {
    if (this.CalculatePricesSettings.AskExtRequired((WebDialogResult) 1, true))
      this.CalculatePendingPrices(((PXSelectBase<CalculatePricesFilter>) this.CalculatePricesSettings).Current);
    ((PXGraph) this).SelectTimeStamp();
    return adapter.Get();
  }

  private void CalculatePendingPrices(CalculatePricesFilter settings)
  {
    if (settings == null)
      return;
    foreach (PXResult<ARPriceWorksheetDetail> pxResult1 in ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).Select(Array.Empty<object>()))
    {
      ARPriceWorksheetDetail worksheetDetail = PXResult<ARPriceWorksheetDetail>.op_Implicit(pxResult1);
      PXResult<PX.Objects.IN.InventoryItem, InventoryItemCurySettings, INItemCost> pxResult2 = (PXResult<PX.Objects.IN.InventoryItem, InventoryItemCurySettings, INItemCost>) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<InventoryItemCurySettings.curyID, Equal<Current<CalculatePricesFilter.baseCuryID>>>>, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<Current<CalculatePricesFilter.baseCuryID>>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) worksheetDetail.InventoryID
      }));
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult2);
      InventoryItemCurySettings curyItem = PXResult<PX.Objects.IN.InventoryItem, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult2);
      INItemCost itemCost = PXResult<PX.Objects.IN.InventoryItem, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult2);
      INItemSite itemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) worksheetDetail.InventoryID,
        (object) worksheetDetail.SiteID
      }));
      KeyValuePair<bool, Decimal> pricesCalculation = this.CalculateCorrectedAmountForPendingPricesCalculation(settings, inventoryItem, curyItem, itemCost, itemSite, worksheetDetail);
      if (!pricesCalculation.Key)
      {
        Decimal num = pricesCalculation.Value;
        if (settings.CorrectionPercent.HasValue)
          num = num * settings.CorrectionPercent.Value * 0.01M;
        short? rounding = settings.Rounding;
        if (rounding.HasValue)
        {
          Decimal d = num;
          rounding = settings.Rounding;
          int decimals = (int) rounding.Value;
          num = Math.Round(d, decimals, MidpointRounding.AwayFromZero);
        }
        ARPriceWorksheetDetail copy = (ARPriceWorksheetDetail) ((PXSelectBase) this.Details).Cache.CreateCopy((object) worksheetDetail);
        copy.PendingPrice = new Decimal?(num);
        ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).Update(copy);
      }
    }
  }

  protected virtual KeyValuePair<bool, Decimal> CalculateCorrectedAmountForPendingPricesCalculation(
    CalculatePricesFilter settings,
    PX.Objects.IN.InventoryItem item,
    InventoryItemCurySettings curyItem,
    INItemCost itemCost,
    INItemSite itemSite,
    ARPriceWorksheetDetail worksheetDetail)
  {
    Decimal? nullable1 = (Decimal?) itemSite?.LastCost;
    Decimal? nullable2 = nullable1 ?? itemCost.LastCost;
    nullable1 = (Decimal?) itemSite?.AvgCost;
    Decimal? nullable3 = nullable1 ?? itemCost.AvgCost;
    nullable1 = (Decimal?) itemSite?.StdCost;
    Decimal? nullable4 = nullable1 ?? curyItem.StdCost;
    nullable1 = (Decimal?) itemSite?.RecPrice;
    Decimal? nullable5 = nullable1 ?? curyItem.RecPrice;
    nullable1 = (Decimal?) itemSite?.MarkupPct;
    Decimal markup = (nullable1 ?? item.MarkupPct.GetValueOrDefault()) * 0.01M;
    Func<Decimal?, bool> func1 = (Func<Decimal?, bool>) (cost =>
    {
      if (settings.UpdateOnZero.GetValueOrDefault())
        return false;
      if (!cost.HasValue)
        return true;
      Decimal? nullable6 = cost;
      Decimal num = 0M;
      return nullable6.GetValueOrDefault() == num & nullable6.HasValue;
    });
    Func<Decimal, Decimal> func2 = (Func<Decimal, Decimal>) (cost => cost + markup * cost);
    Decimal? result;
    Func<Decimal, Decimal> func3 = (Func<Decimal, Decimal>) (amt => !(item.BaseUnit != worksheetDetail.UOM) || !this.TryConvertToBase(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], item.InventoryID, worksheetDetail.UOM, amt, out result) ? amt : result.Value);
    bool key = false;
    Decimal num1 = 0M;
    switch (settings.PriceBasis)
    {
      case "L":
        key = func1(nullable2);
        if (key)
          return new KeyValuePair<bool, Decimal>(key, num1);
        Decimal num2 = func2(nullable2.GetValueOrDefault());
        num1 = func3(num2);
        break;
      case "S":
        Decimal? nullable7 = item.ValMethod == "T" ? nullable4 : nullable3;
        key = func1(nullable7);
        Decimal num3 = func2(nullable7.GetValueOrDefault());
        num1 = func3(num3);
        break;
      case "N":
        key = func1(worksheetDetail.PendingPrice);
        nullable1 = worksheetDetail.PendingPrice;
        num1 = nullable1.GetValueOrDefault();
        break;
      case "P":
        key = func1(worksheetDetail.CurrentPrice);
        nullable1 = worksheetDetail.CurrentPrice;
        num1 = nullable1.GetValueOrDefault();
        break;
      case "R":
        key = func1(nullable5);
        num1 = nullable5.GetValueOrDefault();
        break;
    }
    return new KeyValuePair<bool, Decimal>(key, num1);
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

  protected virtual void ARPriceWorksheet_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ARPriceWorksheet row = (ARPriceWorksheet) e.Row;
    if (row == null)
      return;
    bool flag1 = row.Status == "H" || row.Status == "N";
    PXAction<ARPriceWorksheet> releasePriceWorksheet = this.ReleasePriceWorksheet;
    bool? nullable = row.Hold;
    bool flag2 = false;
    int num1 = !(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) ? 0 : (row.Status == "N" ? 1 : 0);
    ((PXAction) releasePriceWorksheet).SetEnabled(num1 != 0);
    PXAction<ARPriceWorksheet> copyPrices = this.copyPrices;
    nullable = row.Hold;
    int num2 = !nullable.GetValueOrDefault() ? 0 : (row.Status == "H" ? 1 : 0);
    ((PXAction) copyPrices).SetEnabled(num2 != 0);
    PXAction<ARPriceWorksheet> calculatePrices = this.calculatePrices;
    nullable = row.Hold;
    int num3 = !nullable.GetValueOrDefault() ? 0 : (row.Status == "H" ? 1 : 0);
    ((PXAction) calculatePrices).SetEnabled(num3 != 0);
    ((PXSelectBase) this.Details).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Details).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.Document).Cache.AllowDelete = row.Status != "R";
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.hold>(sender, (object) row, row.Status != "R");
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.descr>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.effectiveDate>(sender, (object) row, flag1);
    PXCache pxCache1 = sender;
    ARPriceWorksheet arPriceWorksheet1 = row;
    int num4;
    if (flag1)
    {
      if (!(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType != "L"))
      {
        if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType == "L")
        {
          nullable = row.IsPromotional;
          num4 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num4 = 0;
      }
      else
        num4 = 1;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.expirationDate>(pxCache1, (object) arPriceWorksheet1, num4 != 0);
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.isPromotional>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.isFairValue>(sender, (object) row, flag1);
    PXCache pxCache2 = sender;
    ARPriceWorksheet arPriceWorksheet2 = row;
    int num5;
    if (flag1)
    {
      nullable = row.IsFairValue;
      num5 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.isProrated>(pxCache2, (object) arPriceWorksheet2, num5 != 0);
    PXCache pxCache3 = sender;
    ARPriceWorksheet arPriceWorksheet3 = row;
    int num6;
    if (flag1)
    {
      nullable = row.IsFairValue;
      num6 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.discountable>(pxCache3, (object) arPriceWorksheet3, num6 != 0);
    PXCache pxCache4 = sender;
    ARPriceWorksheet arPriceWorksheet4 = row;
    int num7;
    if (flag1)
    {
      nullable = row.IsPromotional;
      if (!nullable.GetValueOrDefault())
      {
        num7 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType != "L" ? 1 : 0;
        goto label_19;
      }
    }
    num7 = 0;
label_19:
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheet.overwriteOverlapping>(pxCache4, (object) arPriceWorksheet4, num7 != 0);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Details).Cache, "TaxCategoryID", PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>());
    if (!(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType == "L"))
    {
      nullable = row.IsPromotional;
      if (!nullable.GetValueOrDefault())
        goto label_22;
    }
    row.OverwriteOverlapping = new bool?(true);
label_22:
    if (!(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetentionType == "L"))
      return;
    nullable = row.IsPromotional;
    if (nullable.GetValueOrDefault())
      return;
    row.ExpirationDate = new DateTime?();
  }

  protected virtual void ARPriceWorksheet_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARPriceWorksheet row = (ARPriceWorksheet) e.Row;
    if (row == null)
      return;
    ARPriceWorksheet arPriceWorksheet = row;
    bool? hold = row.Hold;
    bool flag = false;
    string str = hold.GetValueOrDefault() == flag & hold.HasValue ? "N" : "H";
    arPriceWorksheet.Status = str;
    if (sender.ObjectsEqual<ARPriceWorksheet.skipLineDiscounts>(e.Row, e.OldRow))
      return;
    foreach (PXResult<ARPriceWorksheetDetail> pxResult in ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).Select(Array.Empty<object>()))
    {
      ARPriceWorksheetDetail priceWorksheetDetail = PXResult<ARPriceWorksheetDetail>.op_Implicit(pxResult);
      priceWorksheetDetail.SkipLineDiscounts = row.SkipLineDiscounts;
      ((PXSelectBase<ARPriceWorksheetDetail>) this.Details).Update(priceWorksheetDetail);
    }
  }

  protected virtual void ARPriceWorksheet_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARPriceWorksheet row = (ARPriceWorksheet) e.Row;
    if (row == null || !row.IsPromotional.GetValueOrDefault() || row.ExpirationDate.HasValue)
      return;
    sender.RaiseExceptionHandling<ARPriceWorksheet.expirationDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[expirationDate]"
    }));
  }

  protected virtual void ARPriceWorksheet_EffectiveDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARPriceWorksheet row = (ARPriceWorksheet) e.Row;
    if (row == null)
      return;
    if (e.NewValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[effectiveDate]"
      });
    if (!row.IsPromotional.GetValueOrDefault())
      return;
    DateTime? expirationDate = row.ExpirationDate;
    if (!expirationDate.HasValue)
      return;
    expirationDate = row.ExpirationDate;
    DateTime newValue = (DateTime) e.NewValue;
    if ((expirationDate.HasValue ? (expirationDate.GetValueOrDefault() < newValue ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Expiration Date may not be less than Effective Date.", Array.Empty<object>()));
  }

  protected virtual void ARPriceWorksheet_ExpirationDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARPriceWorksheet row = (ARPriceWorksheet) e.Row;
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
    DateTime? effectiveDate = row.EffectiveDate;
    if (!effectiveDate.HasValue)
      return;
    effectiveDate = row.EffectiveDate;
    DateTime newValue = (DateTime) e.NewValue;
    if ((effectiveDate.HasValue ? (effectiveDate.GetValueOrDefault() > newValue ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("Expiration Date may not be less than Effective Date.", Array.Empty<object>()));
  }

  protected virtual void ARPriceWorksheetDetail_PriceCode_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    ARPriceWorksheetDetail row = (ARPriceWorksheetDetail) e.Row;
    if (row == null || row.PriceType == null)
      return;
    if (row.PriceType == "C")
    {
      string str = row.PriceCode;
      if (string.IsNullOrEmpty(str) && row.CustomerID.HasValue)
      {
        PX.Objects.AR.Customer byId = this.CustomerRepository.FindByID(row.CustomerID);
        if (byId != null)
        {
          str = byId.AcctCD.TrimEnd();
          row.PriceCode = str;
        }
      }
      e.ReturnState = (object) str;
    }
    else
    {
      if (e.ReturnState == null)
        e.ReturnState = (object) row.CustPriceClassID?.TrimEnd();
      if (row.PriceCode != null)
        return;
      row.PriceCode = row.CustPriceClassID?.TrimEnd();
    }
  }

  protected virtual void ARPriceWorksheetDetail_PriceCode_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((ARPriceWorksheetDetail) e.Row != null && e.NewValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[priceCode]"
      });
  }

  protected virtual void ARPriceWorksheetDetail_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    ARPriceWorksheetDetail row = (ARPriceWorksheetDetail) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<ARPriceWorksheetDetail.priceCode>(sender, (object) row, row.PriceType != "B");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARPriceWorksheetDetail, ARPriceWorksheetDetail.subItemID> args)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, (int?) args.Row?.InventoryID);
    if (inventoryItem == null || !inventoryItem.DefaultSubItemOnEntry.GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARPriceWorksheetDetail, ARPriceWorksheetDetail.subItemID>, ARPriceWorksheetDetail, object>) args).NewValue = (object) inventoryItem.DefaultSubItemID;
  }

  protected virtual void ARPriceWorksheetDetail_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    ARPriceWorksheetDetail row = (ARPriceWorksheetDetail) e.Row;
    if (row == null)
      return;
    if (row.PriceType != null && row.PriceCode != null)
    {
      if (row.PriceType == "C")
      {
        PX.Objects.AR.Customer byCd = this.CustomerRepository.FindByCD(row.PriceCode);
        if (byCd != null)
        {
          row.CustomerID = byCd.BAccountID;
          row.CustPriceClassID = (string) null;
        }
      }
      else
      {
        row.CustomerID = new int?();
        row.CustPriceClassID = row.PriceCode;
      }
    }
    if (e.ExternalCall && row.InventoryID.HasValue && row.CuryID != null && ((PXSelectBase<ARPriceWorksheet>) this.Document).Current != null && ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.EffectiveDate.HasValue)
    {
      Decimal? currentPrice = row.CurrentPrice;
      Decimal num = 0M;
      if (currentPrice.GetValueOrDefault() == num & currentPrice.HasValue)
        row.CurrentPrice = new Decimal?(this.GetItemPrice((PXGraph) this, row.PriceType, row.PriceCode, row.InventoryID, row.CuryID, row.UOM, ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.EffectiveDate));
    }
    if (!((PXGraph) this).IsImportFromExcel || this.DuplicateFinder == null)
      return;
    this.DuplicateFinder.AddItem(row);
  }

  protected virtual void ARPriceWorksheetDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARPriceWorksheetDetail row = (ARPriceWorksheetDetail) e.Row;
    if (row == null)
      return;
    if (!sender.ObjectsEqual<ARPriceWorksheetDetail.priceType>(e.Row, e.OldRow))
      row.PriceCode = (string) null;
    if (e.ExternalCall && (!sender.ObjectsEqual<ARPriceWorksheetDetail.priceCode>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARPriceWorksheetDetail.uOM>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARPriceWorksheetDetail.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARPriceWorksheetDetail.curyID>(e.Row, e.OldRow)))
      row.CurrentPrice = new Decimal?(this.GetItemPrice((PXGraph) this, row.PriceType, row.PriceCode, row.InventoryID, row.CuryID, row.UOM, ((PXSelectBase<ARPriceWorksheet>) this.Document).Current.EffectiveDate));
    if (sender.ObjectsEqual<ARPriceWorksheetDetail.priceCode>(e.Row, e.OldRow))
      return;
    if (row.PriceType == "C")
    {
      PX.Objects.AR.Customer byCd = this.CustomerRepository.FindByCD(row.PriceCode);
      if (byCd == null)
        return;
      row.CustomerID = byCd.BAccountID;
      row.CustPriceClassID = (string) null;
    }
    else
    {
      row.CustomerID = new int?();
      row.CustPriceClassID = row.PriceCode;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARPriceWorksheetDetail, ARPriceWorksheetDetail.inventoryID> e)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, (int?) e.Row?.InventoryID);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARPriceWorksheetDetail, ARPriceWorksheetDetail.inventoryID>>) e).Cache.SetValueExt<ARPriceWorksheetDetail.taxCategoryID>((object) e.Row, (object) inventoryItem?.TaxCategoryID);
  }

  private void CheckForEmptyPendingPriceDetails()
  {
    ARPriceWorksheet current = ((PXSelectBase<ARPriceWorksheet>) this.Document).Current;
    if ((current != null ? (current.Hold.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      return;
    foreach (ARPriceWorksheetDetail priceWorksheetDetail in (IEnumerable<ARPriceWorksheetDetail>) GraphHelper.RowCast<ARPriceWorksheetDetail>((IEnumerable) PXSelectBase<ARPriceWorksheetDetail, PXSelect<ARPriceWorksheetDetail, Where<ARPriceWorksheetDetail.refNbr, Equal<Current<ARPriceWorksheetDetail.refNbr>>, And<ARPriceWorksheetDetail.pendingPrice, IsNull>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToArray<ARPriceWorksheetDetail>())
    {
      ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<ARPriceWorksheetDetail.pendingPrice>((object) priceWorksheetDetail, (object) priceWorksheetDetail.PendingPrice, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) typeof (ARPriceWorksheetDetail.pendingPrice).Name
      }));
      GraphHelper.MarkUpdated(((PXSelectBase) this.Details).Cache, (object) priceWorksheetDetail);
    }
  }

  protected virtual void CheckForDuplicateDetails()
  {
  }

  protected virtual object GetAcceptablePriceTypesParameter()
  {
    return (object) new string[3]{ "B", "C", "P" };
  }

  private void CheckForIncorrectPriceTypeDetails()
  {
    foreach (ARPriceWorksheetDetail priceWorksheetDetail in ((PXSelectBase) this.Details).View.BqlSelect.WhereAnd<Where<BqlOperand<ARPriceWorksheetDetail.priceType, IBqlString>.IsNotIn<P.AsString>>>().Select<ARPriceWorksheetDetail>((PXGraph) this, this.GetAcceptablePriceTypesParameter()))
    {
      ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<ARPriceWorksheetDetail.priceType>((object) priceWorksheetDetail, (object) priceWorksheetDetail.PriceType, (Exception) new PXSetPropertyException("Provided value does not pass validation rules defined for this field."));
      GraphHelper.MarkUpdated(((PXSelectBase) this.Details).Cache, (object) priceWorksheetDetail);
    }
  }

  private Decimal GetItemPrice(
    PXGraph graph,
    string priceType,
    string priceCode,
    int? inventoryID,
    string toCuryID,
    string uom,
    DateTime? curyEffectiveDate)
  {
    Decimal? fromInventoryItem = this.GetItemPriceFromInventoryItem(PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID), uom);
    return fromInventoryItem.HasValue ? ARPriceWorksheetMaint.ConvertSalesPrice(this, ((PXSelectBase<CMSetup>) new CMSetupSelect((PXGraph) this)).Current.ARRateTypeDflt, ((PXGraph) this).Accessinfo.BaseCuryID, toCuryID, curyEffectiveDate, fromInventoryItem.Value) : 0M;
  }

  protected Decimal GetItemPrice(
    PXGraph graph,
    string priceType,
    string priceCode,
    int? inventoryID,
    string toCuryID,
    DateTime? curyEffectiveDate)
  {
    return this.GetItemPrice(graph, priceType, priceCode, inventoryID, toCuryID, (string) null, curyEffectiveDate);
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
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, (int?) inventoryItem?.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID);
    if (itemCurySettings == null)
      return new Decimal?();
    return string.IsNullOrEmpty(uom) || uom == inventoryItem.BaseUnit ? itemCurySettings.BasePrice : new Decimal?(INUnitAttribute.ConvertToBase(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], inventoryItem.InventoryID, uom, itemCurySettings.BasePrice.GetValueOrDefault(), INPrecision.NOROUND));
  }

  protected virtual void ARPriceWorksheetDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    ARPriceWorksheetDetail row = (ARPriceWorksheetDetail) e.Row;
    if (row == null)
      return;
    int? nullable1;
    if (row.PriceType == "C")
    {
      nullable1 = row.CustomerID;
      if (!nullable1.HasValue)
        goto label_5;
    }
    if (!(row.PriceType == "P") || row.CustPriceClassID != null)
    {
      switch (row.PriceType)
      {
        case "C":
          PX.Objects.AR.Customer byCd = this.CustomerRepository.FindByCD(row.PriceCode);
          if (byCd == null)
            return;
          row.CustomerID = byCd.BAccountID;
          row.CustPriceClassID = (string) null;
          return;
        case "B":
        case "P":
          ARPriceWorksheetDetail priceWorksheetDetail = row;
          nullable1 = new int?();
          int? nullable2 = nullable1;
          priceWorksheetDetail.CustomerID = nullable2;
          row.CustPriceClassID = row.PriceCode;
          return;
        default:
          sender.RaiseExceptionHandling<ARPriceWorksheetDetail.priceType>((object) row, (object) row.PriceType, (Exception) new PXSetPropertyException("Provided value does not pass validation rules defined for this field."));
          return;
      }
    }
label_5:
    sender.RaiseExceptionHandling<ARPriceWorksheetDetail.priceCode>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[priceCode]"
    }));
  }

  protected virtual void CopyPricesFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CopyPricesFilter row = (CopyPricesFilter) e.Row;
    if (row == null)
      return;
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    bool flag2 = row.SourceCuryID != row.DestinationCuryID;
    PXCache pxCache1 = sender;
    CopyPricesFilter copyPricesFilter1 = row;
    bool? alwaysFromBaseCury = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.AlwaysFromBaseCury;
    bool flag3 = false;
    int num1 = alwaysFromBaseCury.GetValueOrDefault() == flag3 & alwaysFromBaseCury.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.sourceCuryID>(pxCache1, (object) copyPricesFilter1, num1 != 0);
    PXCache pxCache2 = sender;
    CopyPricesFilter copyPricesFilter2 = row;
    alwaysFromBaseCury = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.AlwaysFromBaseCury;
    bool flag4 = false;
    int num2 = alwaysFromBaseCury.GetValueOrDefault() == flag4 & alwaysFromBaseCury.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.destinationCuryID>(pxCache2, (object) copyPricesFilter2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.rateTypeID>(sender, (object) row, row.SourceCuryID != row.DestinationCuryID);
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.currencyDate>(sender, (object) row, row.SourceCuryID != row.DestinationCuryID);
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.sourcePriceCode>(sender, (object) row, row.SourcePriceType != "B");
    PXUIFieldAttribute.SetEnabled<CopyPricesFilter.destinationPriceCode>(sender, (object) row, row.DestinationPriceType != "B");
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.sourceCuryID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.destinationCuryID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.currencyDate>(sender, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<CopyPricesFilter.rateTypeID>(sender, (object) row, flag1 & flag2);
  }

  protected virtual void CopyPricesFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CopyPricesFilter row = (CopyPricesFilter) e.Row;
    if (row == null)
      return;
    if (!sender.ObjectsEqual<CopyPricesFilter.sourcePriceType>(e.Row, e.OldRow))
      row.SourcePriceCode = (string) null;
    if (!sender.ObjectsEqual<CopyPricesFilter.destinationPriceType>(e.Row, e.OldRow))
      row.DestinationPriceCode = (string) null;
    if (!sender.ObjectsEqual<CopyPricesFilter.sourcePriceCode>(e.Row, e.OldRow) && row.SourcePriceType == "C")
    {
      PXResult<PX.Objects.AR.Customer> pxResult = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SourcePriceCode
      }));
      if (pxResult != null)
        row.SourceCuryID = PXResult<PX.Objects.AR.Customer>.op_Implicit(pxResult).CuryID;
    }
    if (sender.ObjectsEqual<CopyPricesFilter.destinationPriceCode>(e.Row, e.OldRow) || !(row.DestinationPriceType == "C"))
      return;
    PXResult<PX.Objects.AR.Customer> pxResult1 = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.acctCD, Equal<Required<PX.Objects.AR.Customer.acctCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DestinationPriceCode
    }));
    if (pxResult1 == null)
      return;
    row.DestinationCuryID = PXResult<PX.Objects.AR.Customer>.op_Implicit(pxResult1).CuryID;
  }

  protected virtual void INNonStockItemXRef_SubItemID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
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
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual System.Type[] GetAlternativeKeyFields()
  {
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (ARPriceWorksheetDetail.priceType),
      typeof (ARPriceWorksheetDetail.priceCode),
      typeof (ARPriceWorksheetDetail.inventoryID),
      typeof (ARPriceWorksheetDetail.uOM),
      typeof (ARPriceWorksheetDetail.siteID),
      typeof (ARPriceWorksheetDetail.curyID),
      typeof (ARPriceWorksheetDetail.taxID)
    };
    if (PXAccess.FeatureInstalled<FeaturesSet.supportBreakQty>())
      typeList.Add(typeof (ARPriceWorksheetDetail.breakQty));
    return typeList.ToArray();
  }

  public DuplicatesSearchEngine<ARPriceWorksheetDetail> DuplicateFinder { get; set; }

  public string PriceTypeName
  {
    get
    {
      if (this._priceTypeName == null)
        this._priceTypeName = typeof (ARPriceWorksheetDetail.priceType).Name;
      return this._priceTypeName;
    }
  }

  protected virtual void ImportAttributeRowImporting(
    object sender,
    PXImportAttribute.RowImportingEventArgs e)
  {
    if (e.Mode == 2)
      return;
    if (this.DuplicateFinder == null)
    {
      List<ARPriceWorksheetDetail> list = GraphHelper.RowCast<ARPriceWorksheetDetail>((IEnumerable) PXSelectBase<ARPriceWorksheetDetail, PXSelect<ARPriceWorksheetDetail, Where<ARPriceWorksheetDetail.refNbr, Equal<Current<ARPriceWorksheetDetail.refNbr>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<ARPriceWorksheetDetail>();
      this.DuplicateFinder = new DuplicatesSearchEngine<ARPriceWorksheetDetail>(((PXSelectBase) this.Details).Cache, (IEnumerable<System.Type>) this.GetAlternativeKeyFields(), (ICollection<ARPriceWorksheetDetail>) list);
    }
    e.Values[(object) this.PriceTypeName] = (object) e.Values[(object) this.PriceTypeName].ToString().ToUpper();
    ARPriceWorksheetDetail priceWorksheetDetail = this.DuplicateFinder.Find(e.Values);
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
    this.DuplicateFinder = (DuplicatesSearchEngine<ARPriceWorksheetDetail>) null;
  }
}
