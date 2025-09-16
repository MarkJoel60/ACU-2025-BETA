// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorPriceMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APVendorPriceMaint : PXGraph<APVendorPriceMaint>, IPXAuditSource
{
  public PXSave<APVendorPriceFilter> Save;
  public PXCancel<APVendorPriceFilter> Cancel;
  public PXFilter<APVendorPriceFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<APVendorPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APVendorPrice.inventoryID>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>, LeftJoin<Vendor, On<APVendorPrice.vendorID, Equal<Vendor.bAccountID>>, LeftJoin<INSite, On<APVendorPrice.siteID, Equal<INSite.siteID>>>>>>, Where2<Where<Vendor.bAccountID, IsNull, Or<Match<Vendor, Current<AccessInfo.userName>>>>, And2<Where<PX.Objects.IN.InventoryItem.inventoryID, IsNull, Or<Match<PX.Objects.IN.InventoryItem, Current<AccessInfo.userName>>>>, And2<Where<INItemClass.itemClassID, IsNull, Or<Match<INItemClass, Current<AccessInfo.userName>>>>, And2<Where<APVendorPrice.siteID, IsNull, Or<Match<INSite, Current<AccessInfo.userName>>>>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.toDelete>, And2<Where<APVendorPrice.vendorID, Equal<Current<APVendorPriceFilter.vendorID>>, Or<Current<APVendorPriceFilter.vendorID>, IsNull>>, And2<Where<APVendorPrice.siteID, Equal<Current<APVendorPriceFilter.siteID>>, Or<Current<APVendorPriceFilter.siteID>, IsNull>>, And2<Where2<Where2<Where<APVendorPrice.effectiveDate, LessEqual<Optional2<APVendorPriceFilter.effectiveAsOfDate>>, Or<APVendorPrice.effectiveDate, IsNull>>, And<Where<APVendorPrice.expirationDate, GreaterEqual<Optional2<APVendorPriceFilter.effectiveAsOfDate>>, Or<APVendorPrice.expirationDate, IsNull>>>>, Or<Optional2<APVendorPriceFilter.effectiveAsOfDate>, IsNull>>, And<Where2<Where<Current<APVendorPriceFilter.itemClassCD>, IsNull, Or<INItemClass.itemClassCD, Like<Current<APVendorPriceFilter.itemClassCDWildcard>>>>, And2<Where<Current<APVendorPriceFilter.ownerID>, IsNull, Or<Current<APVendorPriceFilter.ownerID>, Equal<PX.Objects.IN.InventoryItem.productManagerID>>>, And2<Where<Current<APVendorPriceFilter.myWorkGroup>, Equal<boolFalse>, Or<PX.Objects.IN.InventoryItem.productWorkgroupID, IsWorkgroupOfContact<CurrentValue<APVendorPriceFilter.currentOwnerID>>>>, And2<Where<Current<APVendorPriceFilter.workGroupID>, IsNull, Or<Current<APVendorPriceFilter.workGroupID>, Equal<PX.Objects.IN.InventoryItem.productWorkgroupID>>>, And<Vendor.bAccountID, IsNotNull>>>>>>>>>>>>>>>>, OrderBy<Asc<PX.Objects.IN.InventoryItem.inventoryCD, Asc<APVendorPrice.uOM, Asc<APVendorPrice.breakQty, Asc<APVendorPrice.effectiveDate>>>>>> Records;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXAction<APVendorPriceFilter> createWorksheet;

  public static APVendorPriceMaint SingleAPVendorPriceMaint
  {
    get
    {
      return PXContext.GetSlot<APVendorPriceMaint>() ?? PXContext.SetSlot<APVendorPriceMaint>(PXGraph.CreateInstance<APVendorPriceMaint>());
    }
  }

  [Vendor]
  [PXDefault(typeof (APVendorPriceFilter.vendorID))]
  public virtual void APVendorPrice_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDefault(typeof (APVendorPriceFilter.inventoryID))]
  public virtual void APVendorPrice_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDefault]
  [PXFormula(typeof (IsNull<Selector<APVendorPrice.vendorID, Vendor.curyID>, Current<AccessInfo.baseCuryID>>))]
  public virtual void APVendorPrice_CuryID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Default<APVendorPrice.vendorID>))]
  public virtual void APVendorPrice_SalesPrice_CacheAttached(PXCache sender)
  {
  }

  public virtual IEnumerable records()
  {
    BqlCommand select = this.Records.View.BqlSelect;
    if (this.Filter.Current.InventoryID.HasValue)
      select = select.WhereAnd<Where<APVendorPrice.inventoryID, Equal<Current<APVendorPriceFilter.inventoryID>>>>();
    PXView pxView = new PXView((PXGraph) this, false, select);
    int startRow = PXView.StartRow;
    int num = 0;
    object[] currents = PXView.Currents;
    object[] parameters = new object[0];
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, filters, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  public APVendorPriceMaint()
  {
    this.FieldDefaulting.AddHandler<BAccountR.type>((PXFieldDefaulting) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) "VE";
    }));
    CrossItemAttribute.SetEnableAlternateSubstitution<APVendorPrice.inventoryID>(this.Records.Cache, (object) null, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.distributionModule>() && this.APSetup.Current.LoadVendorsPricesUsingAlternateID.GetValueOrDefault());
  }

  string IPXAuditSource.GetMainView() => "Records";

  IEnumerable<System.Type> IPXAuditSource.GetAuditedTables()
  {
    yield return typeof (APVendorPrice);
  }

  [PXUIField(DisplayName = "Create Price Worksheet", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable CreateWorksheet(PXAdapter adapter)
  {
    if (this.Filter.Current == null)
      return adapter.Get();
    this.Save.Press();
    APPriceWorksheetMaint instance = PXGraph.CreateInstance<APPriceWorksheetMaint>();
    APPriceWorksheet apPriceWorksheet = new APPriceWorksheet();
    instance.Document.Insert(apPriceWorksheet);
    int startRow = PXView.StartRow;
    int totalRows = 0;
    object[] parameters = new object[16 /*0x10*/]
    {
      (object) this.Filter.Current.VendorID,
      (object) this.Filter.Current.VendorID,
      (object) this.Filter.Current.InventoryID,
      (object) this.Filter.Current.InventoryID,
      (object) this.Filter.Current.SiteID,
      (object) this.Filter.Current.SiteID,
      (object) this.Filter.Current.EffectiveAsOfDate,
      (object) this.Filter.Current.EffectiveAsOfDate,
      (object) this.Filter.Current.EffectiveAsOfDate,
      (object) this.Filter.Current.ItemClassCD,
      (object) this.Filter.Current.ItemClassCDWildcard,
      (object) this.Filter.Current.OwnerID,
      (object) this.Filter.Current.OwnerID,
      (object) this.Filter.Current.MyWorkGroup,
      (object) this.Filter.Current.WorkGroupID,
      (object) this.Filter.Current.WorkGroupID
    };
    Func<BqlCommand, List<object>> func = (Func<BqlCommand, List<object>>) (command => new PXView((PXGraph) this, false, command).Select(PXView.Currents, parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, this.Records.View.GetExternalFilters(), ref startRow, PXView.MaximumRows, ref totalRows));
    List<object> objectList = func(PXSelectBase<APVendorPrice, PXSelectJoin<APVendorPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APVendorPrice.inventoryID>>, LeftJoin<PX.Objects.CR.BAccount, On<APVendorPrice.vendorID, Equal<PX.Objects.CR.BAccount.bAccountID>>, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<PX.Objects.CR.BAccount.baseCuryID>>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>>>>>, Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.toDelete>, And2<Where<APVendorPrice.vendorID, IsNull, Or<PX.Objects.CR.BAccount.bAccountID, IsNotNull>>, And2<Where<APVendorPrice.vendorID, Equal<Required<APVendorPriceFilter.vendorID>>, Or<Required<APVendorPriceFilter.vendorID>, IsNull>>, And2<Where<APVendorPrice.inventoryID, Equal<Required<APVendorPriceFilter.inventoryID>>, Or<Required<APVendorPriceFilter.inventoryID>, IsNull>>, And2<Where<APVendorPrice.siteID, Equal<Required<APVendorPriceFilter.siteID>>, Or<Required<APVendorPriceFilter.siteID>, IsNull>>, And2<Where2<Where2<Where<APVendorPrice.effectiveDate, LessEqual<Required<APVendorPriceFilter.effectiveAsOfDate>>, Or<APVendorPrice.effectiveDate, IsNull>>, And<Where<APVendorPrice.expirationDate, GreaterEqual<Required<APVendorPriceFilter.effectiveAsOfDate>>, Or<APVendorPrice.expirationDate, IsNull>>>>, Or<Required<APVendorPriceFilter.effectiveAsOfDate>, IsNull>>, And<Where2<Where<Required<APVendorPriceFilter.itemClassCD>, IsNull, Or<INItemClass.itemClassCD, Like<Required<APVendorPriceFilter.itemClassCDWildcard>>>>, And2<Where<Required<APVendorPriceFilter.ownerID>, IsNull, Or<Required<APVendorPriceFilter.ownerID>, Equal<PX.Objects.IN.InventoryItem.productManagerID>>>, And2<Where<Required<APVendorPriceFilter.myWorkGroup>, Equal<False>, Or<PX.Objects.IN.InventoryItem.productWorkgroupID, IsWorkgroupOfContact<CurrentValue<APVendorPriceFilter.currentOwnerID>>>>, And<Where<Required<APVendorPriceFilter.workGroupID>, IsNull, Or<Required<APVendorPriceFilter.workGroupID>, Equal<PX.Objects.IN.InventoryItem.productWorkgroupID>>>>>>>>>>>>>>>, OrderBy<Asc<PX.Objects.IN.InventoryItem.inventoryCD, Asc<APVendorPrice.uOM, Asc<APVendorPrice.breakQty, Desc<APVendorPrice.effectiveDate>>>>>>.Config>.GetCommand());
    List<object> groupedVendorPrices = func(PXSelectBase<APVendorPrice, PXSelectJoinGroupBy<APVendorPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APVendorPrice.inventoryID>>, LeftJoin<PX.Objects.CR.BAccount, On<APVendorPrice.vendorID, Equal<PX.Objects.CR.BAccount.bAccountID>>, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCost.curyID, Equal<PX.Objects.CR.BAccount.baseCuryID>>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>>>>>, Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<INItemStatus.toDelete>, And2<Where<APVendorPrice.vendorID, IsNull, Or<PX.Objects.CR.BAccount.bAccountID, IsNotNull>>, And2<Where<APVendorPrice.vendorID, Equal<Required<APVendorPriceFilter.vendorID>>, Or<Required<APVendorPriceFilter.vendorID>, IsNull>>, And2<Where<APVendorPrice.inventoryID, Equal<Required<APVendorPriceFilter.inventoryID>>, Or<Required<APVendorPriceFilter.inventoryID>, IsNull>>, And2<Where<APVendorPrice.siteID, Equal<Required<APVendorPriceFilter.siteID>>, Or<Required<APVendorPriceFilter.siteID>, IsNull>>, And2<Where2<Where2<Where<APVendorPrice.effectiveDate, LessEqual<Required<APVendorPriceFilter.effectiveAsOfDate>>, Or<APVendorPrice.effectiveDate, IsNull>>, And<Where<APVendorPrice.expirationDate, GreaterEqual<Required<APVendorPriceFilter.effectiveAsOfDate>>, Or<APVendorPrice.expirationDate, IsNull>>>>, Or<Required<APVendorPriceFilter.effectiveAsOfDate>, IsNull>>, And<Where2<Where<Required<APVendorPriceFilter.itemClassCD>, IsNull, Or<INItemClass.itemClassCD, Like<Required<APVendorPriceFilter.itemClassCDWildcard>>>>, And2<Where<Required<APVendorPriceFilter.ownerID>, IsNull, Or<Required<APVendorPriceFilter.ownerID>, Equal<PX.Objects.IN.InventoryItem.productManagerID>>>, And2<Where<Required<APVendorPriceFilter.myWorkGroup>, Equal<False>, Or<PX.Objects.IN.InventoryItem.productWorkgroupID, IsWorkgroupOfContact<CurrentValue<APVendorPriceFilter.currentOwnerID>>>>, And<Where<Required<APVendorPriceFilter.workGroupID>, IsNull, Or<Required<APVendorPriceFilter.workGroupID>, Equal<PX.Objects.IN.InventoryItem.productWorkgroupID>>>>>>>>>>>>>>>, Aggregate<GroupBy<APVendorPrice.vendorID, GroupBy<APVendorPrice.inventoryID, GroupBy<APVendorPrice.uOM, GroupBy<APVendorPrice.breakQty, GroupBy<APVendorPrice.curyID, GroupBy<APVendorPrice.siteID>>>>>>>, OrderBy<Asc<PX.Objects.IN.InventoryItem.inventoryCD, Asc<APVendorPrice.uOM, Asc<APVendorPrice.breakQty, Desc<APVendorPrice.effectiveDate>>>>>>.Config>.GetCommand());
    if (objectList.Count > groupedVendorPrices.Count)
      throw new PXException("There are multiple price records (regular and promotional) that are effective on the same date. Use the Vendor Price Worksheets (AP202010) form to create a worksheet by using the Copy Prices action.");
    this.CreateWorksheetDetailsFromVendorPrices(instance, groupedVendorPrices);
    throw new PXRedirectRequiredException((PXGraph) instance, "Create Price Worksheet");
  }

  /// <summary>
  /// Creates worksheet details from vendor prices. Extended in Lexware Price Unit customization.
  /// </summary>
  /// <param name="graph">The APPriceWorksheetMaint graph.</param>
  /// <param name="groupedVendorPrices">The grouped vendor prices.</param>
  protected virtual void CreateWorksheetDetailsFromVendorPrices(
    APPriceWorksheetMaint graph,
    List<object> groupedVendorPrices)
  {
    foreach (PXResult<APVendorPrice> groupedVendorPrice in groupedVendorPrices)
    {
      APVendorPrice price = (APVendorPrice) groupedVendorPrice;
      APPriceWorksheetDetail priceWorksheetDetail = new APPriceWorksheetDetail()
      {
        RefNbr = graph.Document.Current.RefNbr,
        VendorID = price.VendorID
      };
      APPriceWorksheetDetail detail = graph.Details.Insert(priceWorksheetDetail);
      this.FillWorksheetDetailFromVendorPriceOnWorksheetCreation(detail, price);
      graph.Details.Update(detail);
    }
  }

  /// <summary>
  /// Fill worksheet detail from vendor price on worksheet creation. Extended in Lexware Price Unit customization.
  /// </summary>
  /// <param name="detail">The detail.</param>
  /// <param name="price">The price.</param>
  protected virtual void FillWorksheetDetailFromVendorPriceOnWorksheetCreation(
    APPriceWorksheetDetail detail,
    APVendorPrice price)
  {
    detail.InventoryID = price.InventoryID;
    detail.UOM = price.UOM;
    detail.BreakQty = price.BreakQty;
    detail.CurrentPrice = price.SalesPrice;
    detail.CuryID = price.CuryID;
    detail.SiteID = price.SiteID;
  }

  public virtual void APVendorPriceFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXCache cache1 = sender;
    object row1 = e.Row;
    string name1 = typeof (PX.TM.OwnedFilter.ownerID).Name;
    int num1;
    if (e.Row != null)
    {
      bool? nullable = (bool?) sender.GetValue(e.Row, typeof (PX.TM.OwnedFilter.myOwner).Name);
      bool flag = false;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    PXUIFieldAttribute.SetEnabled(cache1, row1, name1, num1 != 0);
    PXCache cache2 = sender;
    object row2 = e.Row;
    string name2 = typeof (PX.TM.OwnedFilter.workGroupID).Name;
    int num2;
    if (e.Row != null)
    {
      bool? nullable = (bool?) sender.GetValue(e.Row, typeof (PX.TM.OwnedFilter.myWorkGroup).Name);
      bool flag = false;
      num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    PXUIFieldAttribute.SetEnabled(cache2, row2, name2, num2 != 0);
  }

  protected virtual void APVendorPrice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APVendorPrice row = (APVendorPrice) e.Row;
    System.DateTime? nullable;
    if (row.IsPromotionalPrice.GetValueOrDefault())
    {
      nullable = row.ExpirationDate;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<APVendorPrice.expirationDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[expirationDate]"
        }));
    }
    if (row.IsPromotionalPrice.GetValueOrDefault())
    {
      nullable = row.EffectiveDate;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<APVendorPrice.effectiveDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[effectiveDate]"
        }));
    }
    nullable = row.ExpirationDate;
    System.DateTime? effectiveDate = row.EffectiveDate;
    if ((nullable.HasValue & effectiveDate.HasValue ? (nullable.GetValueOrDefault() < effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    sender.RaiseExceptionHandling<APVendorPrice.effectiveDate>((object) row, (object) row.ExpirationDate, (Exception) new PXSetPropertyException("The Expiration Date should not be earlier than the Effective Date.", PXErrorLevel.RowError));
  }

  protected virtual void APVendorPrice_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APVendorPrice row))
      return;
    PXUIFieldAttribute.SetEnabled<APVendorPrice.vendorID>(sender, (object) row, !this.Filter.Current.VendorID.HasValue);
    PXUIFieldAttribute.SetEnabled<APVendorPrice.inventoryID>(sender, (object) row, !this.Filter.Current.InventoryID.HasValue);
  }

  public override void Persist()
  {
    this.ValidateVendorPrices();
    base.Persist();
    this.Records.Cache.Clear();
  }

  protected virtual void ValidateVendorPrices()
  {
    foreach (APVendorPrice apVendorPrice in NonGenericIEnumerableExtensions.Concat_(this.Records.Cache.Inserted, this.Records.Cache.Updated))
    {
      APVendorPrice lastPrice = APVendorPriceMaint.FindLastPrice((PXGraph) this, apVendorPrice);
      if (lastPrice != null)
      {
        System.DateTime? effectiveDate = lastPrice.EffectiveDate;
        System.DateTime? nullable = apVendorPrice.EffectiveDate;
        if ((effectiveDate.HasValue & nullable.HasValue ? (effectiveDate.GetValueOrDefault() > nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable = apVendorPrice.ExpirationDate;
          if (!nullable.HasValue)
          {
            this.Records.Cache.RaiseExceptionHandling<APVendorPrice.expirationDate>((object) apVendorPrice, (object) apVendorPrice.ExpirationDate, (Exception) new PXSetPropertyException((IBqlTable) apVendorPrice, "'{0}' cannot be empty.", new object[1]
            {
              (object) PXUIFieldAttribute.GetDisplayName<APVendorPrice.expirationDate>(this.Records.Cache)
            }));
            throw new PXSetPropertyException((IBqlTable) apVendorPrice, "'{0}' cannot be empty.", new object[1]
            {
              (object) PXUIFieldAttribute.GetDisplayName<APVendorPrice.expirationDate>(this.Records.Cache)
            });
          }
        }
      }
      APVendorPriceMaint.ValidateDuplicate((PXGraph) this, this.Records.Cache, apVendorPrice);
    }
  }

  public static void ValidateDuplicate(PXGraph graph, PXCache sender, APVendorPrice price)
  {
    foreach (PXResult<APVendorPrice> pxResult in new PXSelect<APVendorPrice, Where<APVendorPrice.vendorID, Equal<Required<APVendorPrice.vendorID>>, And<APVendorPrice.inventoryID, Equal<Required<APVendorPrice.inventoryID>>, And<APVendorPrice.uOM, Equal<Required<APVendorPrice.uOM>>, And<APVendorPrice.isPromotionalPrice, Equal<Required<APVendorPrice.isPromotionalPrice>>, And<APVendorPrice.breakQty, Equal<Required<APVendorPrice.breakQty>>, And<APVendorPrice.curyID, Equal<Required<APVendorPrice.curyID>>, And2<Where<APVendorPrice.siteID, Equal<Required<APVendorPrice.siteID>>, Or<APVendorPrice.siteID, IsNull, And<Required<APVendorPrice.siteID>, IsNull>>>, And<APVendorPrice.recordID, NotEqual<Required<APVendorPrice.recordID>>>>>>>>>>>(graph).Select((object) price.VendorID, (object) price.InventoryID, (object) price.UOM, (object) price.IsPromotionalPrice, (object) price.BreakQty, (object) price.CuryID, (object) price.SiteID, (object) price.SiteID, (object) price.RecordID))
    {
      APVendorPrice vendorPrice1 = (APVendorPrice) pxResult;
      if (APVendorPriceMaint.IsOverlapping(vendorPrice1, price))
      {
        PXCache pxCache = sender;
        APVendorPrice row = price;
        string uom = price.UOM;
        object[] objArray1 = new object[3]
        {
          (object) vendorPrice1.SalesPrice,
          null,
          null
        };
        System.DateTime? nullable;
        System.DateTime dateTime;
        string str1;
        if (!vendorPrice1.EffectiveDate.HasValue)
        {
          str1 = string.Empty;
        }
        else
        {
          nullable = vendorPrice1.EffectiveDate;
          dateTime = nullable.Value;
          str1 = dateTime.ToShortDateString();
        }
        objArray1[1] = (object) str1;
        nullable = vendorPrice1.ExpirationDate;
        string str2;
        if (!nullable.HasValue)
        {
          str2 = string.Empty;
        }
        else
        {
          nullable = vendorPrice1.ExpirationDate;
          dateTime = nullable.Value;
          str2 = dateTime.ToShortDateString();
        }
        objArray1[2] = (object) str2;
        PXSetPropertyException propertyException = new PXSetPropertyException("Duplicate Sales Price. This line overlaps with another Sales Price (Price: {0}, Effective Date: {1}, Expiration Date: {2})", PXErrorLevel.RowError, objArray1);
        pxCache.RaiseExceptionHandling<APVendorPrice.uOM>((object) row, (object) uom, (Exception) propertyException);
        object[] objArray2 = new object[3]
        {
          (object) vendorPrice1.SalesPrice,
          null,
          null
        };
        nullable = vendorPrice1.EffectiveDate;
        string str3;
        if (!nullable.HasValue)
        {
          str3 = string.Empty;
        }
        else
        {
          nullable = vendorPrice1.EffectiveDate;
          dateTime = nullable.Value;
          str3 = dateTime.ToShortDateString();
        }
        objArray2[1] = (object) str3;
        nullable = vendorPrice1.ExpirationDate;
        string str4;
        if (!nullable.HasValue)
        {
          str4 = string.Empty;
        }
        else
        {
          nullable = vendorPrice1.ExpirationDate;
          dateTime = nullable.Value;
          str4 = dateTime.ToShortDateString();
        }
        objArray2[2] = (object) str4;
        throw new PXSetPropertyException("Duplicate Sales Price. This line overlaps with another Sales Price (Price: {0}, Effective Date: {1}, Expiration Date: {2})", PXErrorLevel.RowError, objArray2);
      }
    }
  }

  public static bool IsOverlapping(APVendorPrice vendorPrice1, APVendorPrice vendorPrice2)
  {
    System.DateTime? nullable1;
    System.DateTime? nullable2;
    if (vendorPrice1.EffectiveDate.HasValue && vendorPrice1.ExpirationDate.HasValue && vendorPrice2.EffectiveDate.HasValue && vendorPrice2.ExpirationDate.HasValue)
    {
      System.DateTime? effectiveDate = vendorPrice1.EffectiveDate;
      nullable1 = vendorPrice2.EffectiveDate;
      if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = vendorPrice1.ExpirationDate;
        nullable2 = vendorPrice2.EffectiveDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
      nullable2 = vendorPrice1.EffectiveDate;
      nullable1 = vendorPrice2.ExpirationDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = vendorPrice1.ExpirationDate;
        nullable2 = vendorPrice2.ExpirationDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
      nullable2 = vendorPrice1.EffectiveDate;
      nullable1 = vendorPrice2.EffectiveDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = vendorPrice1.EffectiveDate;
        nullable2 = vendorPrice2.ExpirationDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
    }
    nullable2 = vendorPrice1.ExpirationDate;
    if (nullable2.HasValue)
    {
      nullable2 = vendorPrice2.EffectiveDate;
      if (nullable2.HasValue)
      {
        nullable2 = vendorPrice2.ExpirationDate;
        if (nullable2.HasValue)
        {
          nullable2 = vendorPrice1.EffectiveDate;
          if (nullable2.HasValue)
            goto label_12;
        }
        nullable2 = vendorPrice2.EffectiveDate;
        nullable1 = vendorPrice1.ExpirationDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
    }
label_12:
    nullable1 = vendorPrice1.EffectiveDate;
    if (nullable1.HasValue)
    {
      nullable1 = vendorPrice2.ExpirationDate;
      if (nullable1.HasValue)
      {
        nullable1 = vendorPrice2.EffectiveDate;
        if (nullable1.HasValue)
        {
          nullable1 = vendorPrice1.ExpirationDate;
          if (nullable1.HasValue)
            goto label_17;
        }
        nullable1 = vendorPrice2.ExpirationDate;
        nullable2 = vendorPrice1.EffectiveDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
    }
label_17:
    nullable2 = vendorPrice1.EffectiveDate;
    if (nullable2.HasValue)
    {
      nullable2 = vendorPrice2.EffectiveDate;
      if (nullable2.HasValue)
      {
        nullable2 = vendorPrice1.ExpirationDate;
        if (!nullable2.HasValue)
        {
          nullable2 = vendorPrice2.ExpirationDate;
          if (!nullable2.HasValue)
            goto label_30;
        }
      }
    }
    nullable2 = vendorPrice1.ExpirationDate;
    if (nullable2.HasValue)
    {
      nullable2 = vendorPrice2.ExpirationDate;
      if (nullable2.HasValue)
      {
        nullable2 = vendorPrice1.EffectiveDate;
        if (!nullable2.HasValue)
        {
          nullable2 = vendorPrice2.EffectiveDate;
          if (!nullable2.HasValue)
            goto label_30;
        }
      }
    }
    nullable2 = vendorPrice1.EffectiveDate;
    if (!nullable2.HasValue)
    {
      nullable2 = vendorPrice1.ExpirationDate;
      if (!nullable2.HasValue)
        goto label_30;
    }
    nullable2 = vendorPrice2.EffectiveDate;
    if (nullable2.HasValue)
      return false;
    nullable2 = vendorPrice2.ExpirationDate;
    return !nullable2.HasValue;
label_30:
    return true;
  }

  /// <summary>Calculates Unit Cost.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="siteID">Warehouse</param>
  /// <param name="curyID">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <returns>Unit Cost</returns>
  public static Decimal? CalculateUnitCost(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost,
    bool alwaysFromBaseCurrency = false)
  {
    return APVendorPriceMaint.CalculateUnitCost(sender, vendorID, vendorLocationID, inventoryID, new int?(), currencyinfo, UOM, quantity, date, currentUnitCost, alwaysFromBaseCurrency);
  }

  /// <summary>Calculates Unit Cost.</summary>
  /// <param name="sender">Cache.</param>
  /// <param name="vendorID">The vendor ID.</param>
  /// <param name="vendorLocationID">The vendor location ID.</param>
  /// <param name="inventoryID">Inventory.</param>
  /// <param name="siteID">Warehouse.</param>
  /// <param name="currencyinfo">The currency info.</param>
  /// <param name="UOM">Unit of measure.</param>
  /// <param name="quantity">The quantity.</param>
  /// <param name="date">Date.</param>
  /// <param name="currentUnitCost">The current unit cost.</param>
  /// <param name="alwaysFromBaseCurrency">(Optional) True to always from base currency.</param>
  /// <returns>Unit Cost.</returns>
  public static Decimal? CalculateUnitCost(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost,
    bool alwaysFromBaseCurrency = false)
  {
    return APVendorPriceMaint.SingleAPVendorPriceMaint.CalculateUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, siteID, currencyinfo, UOM, quantity, date, currentUnitCost, alwaysFromBaseCurrency);
  }

  /// <summary>Calculates Unit Cost in a given currency only.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="curyID">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <returns>Unit Cost</returns>
  public static Decimal? CalculateCuryUnitCost(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    string curyID,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost)
  {
    return APVendorPriceMaint.CalculateCuryUnitCost(sender, vendorID, vendorLocationID, inventoryID, new int?(), curyID, UOM, quantity, date, currentUnitCost);
  }

  /// <summary>Calculates Unit Cost in a given currency only.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="siteID">Warehouse</param>
  /// <param name="curyID">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <returns>Unit Cost</returns>
  public static Decimal? CalculateCuryUnitCost(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    int? siteID,
    string curyID,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost)
  {
    return APVendorPriceMaint.SingleAPVendorPriceMaint.CalculateCuryUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, siteID, curyID, UOM, quantity, date, currentUnitCost);
  }

  public virtual Decimal? CalculateUnitCostInt(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost,
    bool alwaysFromBaseCurrency = false)
  {
    return this.CalculateUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, new int?(), currencyinfo, UOM, quantity, date, currentUnitCost, alwaysFromBaseCurrency);
  }

  public virtual Decimal? CalculateUnitCostInt(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost,
    bool alwaysFromBaseCurrency = false)
  {
    string curyID = alwaysFromBaseCurrency ? currencyinfo.BaseCuryID : currencyinfo.CuryID;
    Decimal num = System.Math.Abs(quantity.GetValueOrDefault());
    APVendorPriceMaint.UnitCostItem unitCostInt = this.FindUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, siteID, currencyinfo.BaseCuryID, curyID, new Decimal?(num), UOM, date);
    return this.AdjustUnitCostInt(sender, unitCostInt, inventoryID, currencyinfo, UOM, currentUnitCost);
  }

  public virtual Decimal? CalculateCuryUnitCostInt(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    string curyID,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost)
  {
    return this.CalculateCuryUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, new int?(), curyID, UOM, quantity, date, currentUnitCost);
  }

  public virtual Decimal? CalculateCuryUnitCostInt(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    int? siteID,
    string curyID,
    string UOM,
    Decimal? quantity,
    System.DateTime date,
    Decimal? currentUnitCost)
  {
    APVendorPriceMaint.UnitCostItem unitCostInt = this.FindUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, siteID, curyID, curyID, new Decimal?(System.Math.Abs(quantity.GetValueOrDefault())), UOM, date);
    return this.AdjustUnitCostInt(sender, unitCostInt, inventoryID, (PX.Objects.CM.CurrencyInfo) null, UOM, currentUnitCost);
  }

  public static APVendorPriceMaint.UnitCostItem FindUnitCost(
    PXCache sender,
    int? inventoryID,
    string curyID,
    string UOM,
    System.DateTime date)
  {
    return APVendorPriceMaint.FindUnitCost(sender, inventoryID, new int?(), curyID, UOM, date);
  }

  public static APVendorPriceMaint.UnitCostItem FindUnitCost(
    PXCache sender,
    int? inventoryID,
    int? siteID,
    string curyID,
    string UOM,
    System.DateTime date)
  {
    return APVendorPriceMaint.SingleAPVendorPriceMaint.FindUnitCostInt(sender, inventoryID, siteID, curyID, UOM, date);
  }

  public static APVendorPriceMaint.UnitCostItem FindUnitCost(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    System.DateTime date)
  {
    return APVendorPriceMaint.FindUnitCost(sender, vendorID, vendorLocationID, inventoryID, new int?(), baseCuryID, curyID, quantity, UOM, date);
  }

  public static APVendorPriceMaint.UnitCostItem FindUnitCost(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    System.DateTime date)
  {
    return APVendorPriceMaint.SingleAPVendorPriceMaint.FindUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, siteID, baseCuryID, curyID, quantity, UOM, date);
  }

  public virtual APVendorPriceMaint.UnitCostItem FindUnitCostInt(
    PXCache sender,
    int? inventoryID,
    string curyID,
    string UOM,
    System.DateTime date)
  {
    return this.FindUnitCostInt(sender, new int?(), new int?(), inventoryID, new int?(), curyID, curyID, new Decimal?(0M), UOM, date);
  }

  public virtual APVendorPriceMaint.UnitCostItem FindUnitCostInt(
    PXCache sender,
    int? inventoryID,
    int? siteID,
    string curyID,
    string UOM,
    System.DateTime date)
  {
    return this.FindUnitCostInt(sender, new int?(), new int?(), inventoryID, siteID, curyID, curyID, new Decimal?(0M), UOM, date);
  }

  public virtual APVendorPriceMaint.UnitCostItem FindUnitCostInt(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    System.DateTime date)
  {
    return this.FindUnitCostInt(sender, vendorID, vendorLocationID, inventoryID, new int?(), baseCuryID, curyID, quantity, UOM, date);
  }

  /// <summary>Creates unit cost select command</summary>
  public virtual BqlCommand CreateUnitCostSelectCommand()
  {
    return (BqlCommand) new Select2<APVendorPrice, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<APVendorPrice.inventoryID>>>, Where<APVendorPrice.inventoryID, In<Required<APVendorPrice.inventoryID>>, And<APVendorPrice.vendorID, Equal<Required<APVendorPrice.vendorID>>, And<APVendorPrice.curyID, Equal<Required<APVendorPrice.curyID>>, And2<Where<APVendorPrice.siteID, Equal<Required<APVendorPrice.siteID>>, Or<APVendorPrice.siteID, IsNull>>, And2<Where2<Where<APVendorPrice.breakQty, LessEqual<Required<APVendorPrice.breakQty>>>, And<Where2<Where<APVendorPrice.effectiveDate, LessEqual<Required<APVendorPrice.effectiveDate>>, And<APVendorPrice.expirationDate, GreaterEqual<Required<APVendorPrice.expirationDate>>>>, Or2<Where<APVendorPrice.effectiveDate, LessEqual<Required<APVendorPrice.effectiveDate>>, And<APVendorPrice.expirationDate, IsNull>>, Or<Where<APVendorPrice.expirationDate, GreaterEqual<Required<APVendorPrice.expirationDate>>, And<APVendorPrice.effectiveDate, IsNull, Or<APVendorPrice.effectiveDate, IsNull, And<APVendorPrice.expirationDate, IsNull>>>>>>>>>, And<APVendorPrice.uOM, Equal<Required<APVendorPrice.uOM>>>>>>>>, OrderBy<Desc<APVendorPrice.isPromotionalPrice, Desc<APVendorPrice.siteID, Desc<APVendorPrice.vendorID, Desc<APVendorPrice.breakQty>>>>>>();
  }

  public virtual APVendorPriceMaint.UnitCostItem FindUnitCostInt(
    PXCache sender,
    int? vendorID,
    int? vendorLocationID,
    int? inventoryID,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    System.DateTime date)
  {
    int?[] inventoryIds = this.GetInventoryIDs(sender, inventoryID);
    PXView pxView = new PXView(sender.Graph, true, this.CreateUnitCostSelectCommand());
    APVendorPrice vendorPrice1 = pxView.SelectSingle((object) inventoryIds, (object) vendorID, (object) curyID, (object) siteID, (object) quantity, (object) date, (object) date, (object) date, (object) date, (object) UOM).With<object, APVendorPrice>((Func<object, APVendorPrice>) (_ => PXResult.Unwrap<APVendorPrice>(_)));
    if (vendorPrice1 != null)
      return this.CreateUnitCostFromVendorPrice(vendorPrice1, UOM);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
    Decimal num = INUnitAttribute.ConvertToBase(sender, inventoryID, UOM, quantity.Value, INPrecision.QUANTITY);
    APVendorPrice vendorPrice2 = pxView.SelectSingle((object) inventoryIds, (object) vendorID, (object) curyID, (object) siteID, (object) num, (object) date, (object) date, (object) date, (object) date, (object) inventoryItem?.BaseUnit).With<object, APVendorPrice>((Func<object, APVendorPrice>) (_ => PXResult.Unwrap<APVendorPrice>(_)));
    return vendorPrice2 != null ? this.CreateUnitCostFromVendorPrice(vendorPrice2, vendorPrice2.UOM) : (APVendorPriceMaint.UnitCostItem) null;
  }

  /// <summary>
  /// This function is intended for customization purposes and allows to create an array of InventoryIDs to be selected.
  /// </summary>
  /// <param name="inventoryID">Original InventoryID</param>
  public virtual int?[] GetInventoryIDs(PXCache sender, int? inventoryID)
  {
    return new int?[1]{ inventoryID };
  }

  /// <summary>
  /// Creates <see cref="T:PX.Objects.AP.APVendorPriceMaint.UnitCostItem" /> data container from vendor price. Used in PriceUnit customization for Lexware.
  /// </summary>
  /// <param name="vendorPrice">The vendor price.</param>
  /// <param name="uom">The unit of measure.</param>
  /// <returns />
  protected internal virtual APVendorPriceMaint.UnitCostItem CreateUnitCostFromVendorPrice(
    APVendorPrice vendorPrice,
    string uom)
  {
    return vendorPrice == null ? (APVendorPriceMaint.UnitCostItem) null : new APVendorPriceMaint.UnitCostItem(uom, vendorPrice.SalesPrice.GetValueOrDefault(), vendorPrice.CuryID);
  }

  public static Decimal? AdjustUnitCost(
    PXCache sender,
    APVendorPriceMaint.UnitCostItem ucItem,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? currentUnitCost)
  {
    return APVendorPriceMaint.SingleAPVendorPriceMaint.AdjustUnitCostInt(sender, ucItem, inventoryID, currencyinfo, UOM, currentUnitCost);
  }

  public virtual Decimal? AdjustUnitCostInt(
    PXCache sender,
    APVendorPriceMaint.UnitCostItem ucItem,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? currentUnitCost)
  {
    if (ucItem == null || UOM == null)
      return new Decimal?();
    Decimal curyval = ucItem.Cost;
    if (currencyinfo != null && ucItem.CuryID != currencyinfo.CuryID)
      PXCurrencyAttribute.CuryConvCury(sender, currencyinfo, ucItem.Cost, out curyval);
    if (ucItem.UOM == UOM)
      return new Decimal?(curyval);
    Decimal num = INUnitAttribute.ConvertFromBase(sender, inventoryID, ucItem.UOM, curyval, INPrecision.UNITCOST);
    return new Decimal?(INUnitAttribute.ConvertToBase(sender, inventoryID, UOM, num, INPrecision.UNITCOST));
  }

  public static void CheckNewUnitCost<Line, Field>(PXCache sender, Line row, object newValue)
    where Line : class, IBqlTable, new()
    where Field : class, IBqlField
  {
    if (newValue != null && (Decimal) newValue != 0M)
      PXUIFieldAttribute.SetWarning<Field>(sender, (object) row, (string) null);
    else
      PXUIFieldAttribute.SetWarning<Field>(sender, (object) row, "Unit cost has been set to zero because no effective unit cost was found.");
  }

  public static APVendorPrice FindLastPrice(PXGraph graph, APVendorPrice price)
  {
    return new PXSelect<APVendorPrice, Where<APVendorPrice.vendorID, Equal<Required<APVendorPrice.vendorID>>, And<APVendorPrice.inventoryID, Equal<Required<APVendorPrice.inventoryID>>, And2<Where<APVendorPrice.siteID, Equal<Required<APVendorPrice.siteID>>, Or<APVendorPrice.siteID, IsNull, And<Required<APVendorPrice.siteID>, IsNull>>>, And<APVendorPrice.uOM, Equal<Required<APVendorPrice.uOM>>, And<APVendorPrice.isPromotionalPrice, Equal<Required<APVendorPrice.isPromotionalPrice>>, And<APVendorPrice.breakQty, Equal<Required<APVendorPrice.breakQty>>, And<APVendorPrice.curyID, Equal<Required<APVendorPrice.curyID>>, And<APVendorPrice.recordID, NotEqual<Required<APVendorPrice.recordID>>>>>>>>>>, OrderBy<Desc<APVendorPrice.effectiveDate>>>(graph).SelectSingle((object) price.VendorID, (object) price.InventoryID, (object) price.SiteID, (object) price.SiteID, (object) price.UOM, (object) price.IsPromotionalPrice, (object) price.BreakQty, (object) price.CuryID, (object) price.RecordID);
  }

  public class UnitCostItem
  {
    public string UOM { get; }

    public Decimal Cost { get; }

    public string CuryID { get; }

    public UnitCostItem(string uom, Decimal cost, string curyid)
    {
      this.UOM = uom;
      this.Cost = cost;
      this.CuryID = curyid;
    }
  }
}
