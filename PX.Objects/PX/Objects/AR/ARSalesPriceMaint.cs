// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPriceMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Repositories;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARSalesPriceMaint : PXGraph<ARSalesPriceMaint>, IPXAuditSource
{
  public PXFilter<ARSalesPriceFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<ARSalesPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<ARSalesPrice.itemClassID>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<ARSalesPrice.customerID>>, LeftJoin<PX.Objects.IN.INSite, On<ARSalesPrice.siteID, Equal<PX.Objects.IN.INSite.siteID>>>>>>, Where2<Where<PX.Objects.CR.BAccount.bAccountID, IsNull, Or<Match<PX.Objects.CR.BAccount, Current<AccessInfo.userName>>>>, And2<Where<PX.Objects.IN.InventoryItem.inventoryID, IsNull, Or<Match<PX.Objects.IN.InventoryItem, Current<AccessInfo.userName>>>>, And2<Where<INItemClass.itemClassID, IsNull, Or<Match<INItemClass, Current<AccessInfo.userName>>>>, And2<Where<ARSalesPrice.siteID, IsNull, Or<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>, And<ARSalesPrice.itemStatus, NotIn3<INItemStatus.inactive, InventoryItemStatus.unknown, INItemStatus.toDelete>, And2<Where<ARSalesPrice.isFairValue, NotEqual<True>, Or<FeatureInstalled<FeaturesSet.aSC606>>>, And2<Where<Required<ARSalesPriceFilter.priceType>, Equal<PriceTypes.allPrices>, Or<ARSalesPrice.priceType, Equal<Required<ARSalesPriceFilter.priceType>>>>, And2<Where<Required<ARSalesPriceFilter.taxCalcMode>, Equal<PriceTaxCalculationMode.allModes>, Or<ARSalesPrice.taxCalcMode, Equal<Required<ARSalesPriceFilter.taxCalcMode>>>>, And2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPriceFilter.priceCode>>, Or<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPriceFilter.priceCode>>, Or<Required<ARSalesPriceFilter.priceCode>, IsNull>>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPriceFilter.siteID>>, Or<Required<ARSalesPriceFilter.siteID>, IsNull>>, And2<Where2<Where2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPriceFilter.effectiveAsOfDate>>, Or<ARSalesPrice.effectiveDate, IsNull>>, And<Where<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPriceFilter.effectiveAsOfDate>>, Or<ARSalesPrice.expirationDate, IsNull>>>>, Or<Required<ARSalesPriceFilter.effectiveAsOfDate>, IsNull>>, And<Where2<Where<Required<ARSalesPriceFilter.itemClassCD>, IsNull, Or<INItemClass.itemClassCD, Like<Required<ARSalesPriceFilter.itemClassCDWildcard>>>>, And2<Where<Required<ARSalesPriceFilter.inventoryPriceClassID>, IsNull, Or<Required<ARSalesPriceFilter.inventoryPriceClassID>, Equal<ARSalesPrice.priceClassID>>>, And2<Where<Required<ARSalesPriceFilter.ownerID>, IsNull, Or<Required<ARSalesPriceFilter.ownerID>, Equal<ARSalesPrice.priceManagerID>>>, And2<Where<Required<ARSalesPriceFilter.myWorkGroup>, Equal<False>, Or<ARSalesPrice.priceWorkgroupID, IsWorkgroupOfContact<CurrentValue<ARSalesPriceFilter.currentOwnerID>>>>, And2<Where<Required<ARSalesPriceFilter.workGroupID>, IsNull, Or<Required<ARSalesPriceFilter.workGroupID>, Equal<ARSalesPrice.priceWorkgroupID>>>, And<Where<ARSalesPrice.priceType, NotEqual<PriceTypes.customer>, Or<PX.Objects.CR.BAccount.bAccountID, IsNotNull>>>>>>>>>>>>>>>>>>>>, OrderBy<Asc<ARSalesPrice.inventoryCD, Asc<ARSalesPrice.priceType, Asc<ARSalesPrice.uOM, Asc<ARSalesPrice.breakQty, Asc<ARSalesPrice.effectiveDate>>>>>>> Records;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<ARSetup> arsetup;
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>> CustomerCode;
  public PXSelect<PX.Objects.AR.Customer> Customer;
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public PXSelect<ARPriceClass> CustPriceClassCode;
  protected readonly CustomerRepository CustomerRepository;
  public PXSave<ARSalesPriceFilter> Save;
  public PXCancel<ARSalesPriceFilter> Cancel;
  public PXAction<ARSalesPriceFilter> createWorksheet;

  public static ARSalesPriceMaint SingleARSalesPriceMaint
  {
    get
    {
      return PXContext.GetSlot<ARSalesPriceMaint>() ?? PXContext.SetSlot<ARSalesPriceMaint>(PXGraph.CreateInstance<ARSalesPriceMaint>());
    }
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PriceTypes.List]
  [PXUIField]
  public virtual void ARSalesPrice_PriceType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (ARSalesPriceFilter.priceCode))]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARSalesPrice.priceType, IBqlString>.IsEqual<PriceTypes.customer>>, RTrim<PX.Objects.AR.Customer.acctCD>, Case<Where<BqlOperand<ARSalesPrice.priceType, IBqlString>.IsEqual<PriceTypes.customerPriceClass>>, RTrim<ARSalesPrice.custPriceClassID>>>, Null>), typeof (string))]
  public virtual void ARSalesPrice_PriceCode_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (ARSalesPriceFilter.inventoryID))]
  public virtual void ARSalesPrice_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBCalced(typeof (PX.Objects.AR.Customer.acctCD), typeof (string))]
  [PXFormula(typeof (Selector<ARSalesPrice.customerID, PX.Objects.AR.Customer.acctCD>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARSalesPrice.customerCD> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXStringAttribute))]
  [PXDBLocalizableString(256 /*0x0100*/, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.descr>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARSalesPrice.description> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.taxCategoryID>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARSalesPrice.taxCategoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXStringAttribute))]
  [PXDBString(IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.inventoryCD>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARSalesPrice.inventoryCD> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXStringAttribute))]
  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.IN.InventoryItem.itemStatus))]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.itemStatus>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARSalesPrice.itemStatus> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXIntAttribute))]
  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.itemClassID>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARSalesPrice.itemClassID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXStringAttribute))]
  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.priceClassID))]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.priceClassID>))]
  public virtual void _(PX.Data.Events.CacheAttached<ARSalesPrice.priceClassID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXIntAttribute))]
  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.priceWorkgroupID))]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.priceWorkgroupID>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<ARSalesPrice.priceWorkgroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXIntAttribute))]
  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.priceManagerID))]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.priceManagerID>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<ARSalesPrice.priceManagerID> e)
  {
  }

  public virtual IEnumerable records()
  {
    if (PXView.MaximumRows == 1)
    {
      string[] sortColumns = PXView.SortColumns;
      if ((sortColumns != null ? (sortColumns.Length != 0 ? 1 : 0) : 0) != 0 && PXView.SortColumns[0].Equals("RecordID", StringComparison.OrdinalIgnoreCase))
      {
        object[] searches = PXView.Searches;
        if ((searches != null ? (searches.Length != 0 ? 1 : 0) : 0) != 0 && PXView.Searches[0] != null)
        {
          object obj = ((PXSelectBase) this.Records).Cache.Locate((object) new ARSalesPrice()
          {
            RecordID = new int?(Convert.ToInt32(PXView.Searches[0]))
          });
          if (obj != null)
            return (IEnumerable) new object[1]{ obj };
        }
      }
    }
    ARSalesPriceFilter current = ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current;
    string priceCode = ARSalesPriceMaint.ParsePriceCode((PXGraph) this, current.PriceType, current.PriceCode);
    BqlCommand bqlCommand = ((PXSelectBase) this.Records).View.BqlSelect;
    List<object> objectList = new List<object>()
    {
      (object) current.PriceType,
      (object) current.PriceType,
      (object) current.TaxCalcMode,
      (object) current.TaxCalcMode,
      current.PriceType == "C" ? (object) priceCode : (object) (string) null,
      current.PriceType == "P" ? (object) priceCode : (object) (string) null,
      (object) priceCode,
      (object) current.SiteID,
      (object) current.SiteID,
      (object) current.EffectiveAsOfDate,
      (object) current.EffectiveAsOfDate,
      (object) current.EffectiveAsOfDate,
      (object) current.ItemClassCD,
      (object) current.ItemClassCDWildcard,
      (object) current.InventoryPriceClassID,
      (object) current.InventoryPriceClassID,
      (object) current.OwnerID,
      (object) current.OwnerID,
      (object) current.MyWorkGroup,
      (object) current.WorkGroupID,
      (object) current.WorkGroupID
    };
    if (current.InventoryID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd<Where<ARSalesPrice.inventoryID, Equal<Required<ARSalesPriceFilter.inventoryID>>>>();
      objectList.Add((object) current.InventoryID);
    }
    return ARSalesPriceMaint.QSelect((PXGraph) this, bqlCommand, objectList.ToArray());
  }

  public static IEnumerable QSelect(PXGraph graph, BqlCommand bqlCommand, object[] viewParameters)
  {
    PXView pxView = new PXView(graph, false, bqlCommand);
    int startRow = PXView.StartRow;
    int num = 0;
    object[] currents = PXView.Currents;
    object[] objArray = viewParameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, objArray, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  public ARSalesPriceMaint()
  {
    this.CustomerRepository = new CustomerRepository((PXGraph) this);
    CrossItemAttribute.SetEnableAlternateSubstitution<ARSalesPrice.inventoryID>(((PXSelectBase) this.Records).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() && ((PXSelectBase<ARSetup>) this.arsetup).Current.LoadSalesPricesUsingAlternateID.GetValueOrDefault());
  }

  [PXUIField]
  public virtual IEnumerable CreateWorksheet(PXAdapter adapter)
  {
    if (((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current == null)
      return adapter.Get();
    ((PXAction) this.Save).Press();
    if (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() && ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.TaxCalcMode == "A")
      throw new PXException("To create a worksheet, select a tax calculation mode.");
    string priceCode = ARSalesPriceMaint.ParsePriceCode((PXGraph) this, ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType, ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceCode);
    ARPriceWorksheetMaint instance = PXGraph.CreateInstance<ARPriceWorksheetMaint>();
    ((PXSelectBase<ARPriceWorksheet>) instance.Document).Insert(new ARPriceWorksheet()
    {
      TaxCalcMode = ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.TaxCalcMode
    });
    int startRow = PXView.StartRow;
    int totalRows = 0;
    object[] parameters = new object[22]
    {
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.TaxCalcMode,
      ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType == "C" ? (object) priceCode : (object) (string) null,
      ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType == "P" ? (object) priceCode : (object) (string) null,
      (object) priceCode,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.InventoryID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.InventoryID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.SiteID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.SiteID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.EffectiveAsOfDate,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.EffectiveAsOfDate,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.EffectiveAsOfDate,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.ItemClassCD,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.ItemClassCDWildcard,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.InventoryPriceClassID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.InventoryPriceClassID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.OwnerID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.OwnerID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.MyWorkGroup,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.WorkGroupID,
      (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.WorkGroupID
    };
    Func<BqlCommand, List<object>> func = (Func<BqlCommand, List<object>>) (command => new PXView((PXGraph) this, false, command).Select(PXView.Currents, parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, ((PXSelectBase) this.Records).View.GetExternalFilters(), ref startRow, PXView.MaximumRows, ref totalRows));
    List<object> objectList = func(PXSelectBase<ARSalesPrice, PXSelectJoin<ARSalesPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>>, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<ARSalesPrice.inventoryID>, And<INItemCost.curyID, Equal<Current<AccessInfo.baseCuryID>>>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<ARSalesPrice.itemClassID>>>>>, Where<ARSalesPrice.itemStatus, NotIn3<INItemStatus.inactive, InventoryItemStatus.unknown, INItemStatus.toDelete>, And2<Where<Required<ARSalesPriceFilter.priceType>, Equal<PriceTypes.allPrices>, Or<ARSalesPrice.priceType, Equal<Required<ARSalesPriceFilter.priceType>>>>, And2<Where<ARSalesPrice.taxCalcMode, Equal<Required<ARSalesPriceFilter.taxCalcMode>>, Or<Not<FeatureInstalled<FeaturesSet.netGrossEntryMode>>>>, And2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPriceFilter.priceCode>>, Or<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPriceFilter.priceCode>>, Or<Required<ARSalesPriceFilter.priceCode>, IsNull>>>, And2<Where<ARSalesPrice.inventoryID, Equal<Required<ARSalesPriceFilter.inventoryID>>, Or<Required<ARSalesPriceFilter.inventoryID>, IsNull>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPriceFilter.siteID>>, Or<Required<ARSalesPriceFilter.siteID>, IsNull>>, And2<Where2<Where2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPriceFilter.effectiveAsOfDate>>, Or<ARSalesPrice.effectiveDate, IsNull>>, And<Where<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPriceFilter.effectiveAsOfDate>>, Or<ARSalesPrice.expirationDate, IsNull>>>>, Or<Required<ARSalesPriceFilter.effectiveAsOfDate>, IsNull>>, And<Where2<Where<Required<ARSalesPriceFilter.itemClassCD>, IsNull, Or<INItemClass.itemClassCD, Like<Required<ARSalesPriceFilter.itemClassCDWildcard>>>>, And2<Where<Required<ARSalesPriceFilter.inventoryPriceClassID>, IsNull, Or<Required<ARSalesPriceFilter.inventoryPriceClassID>, Equal<ARSalesPrice.priceClassID>>>, And2<Where<Required<ARSalesPriceFilter.ownerID>, IsNull, Or<Required<ARSalesPriceFilter.ownerID>, Equal<ARSalesPrice.priceManagerID>>>, And2<Where<Required<ARSalesPriceFilter.myWorkGroup>, Equal<False>, Or<ARSalesPrice.priceWorkgroupID, IsWorkgroupOfContact<CurrentValue<ARSalesPriceFilter.currentOwnerID>>>>, And<Where<Required<ARSalesPriceFilter.workGroupID>, IsNull, Or<Required<ARSalesPriceFilter.workGroupID>, Equal<ARSalesPrice.priceWorkgroupID>>>>>>>>>>>>>>>>>.Config>.GetCommand());
    List<object> groupedSalesPrices = func(PXSelectBase<ARSalesPrice, PXSelectJoinGroupBy<ARSalesPrice, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>>, LeftJoin<INItemCost, On<INItemCost.inventoryID, Equal<ARSalesPrice.inventoryID>, And<INItemCost.curyID, Equal<Current<AccessInfo.baseCuryID>>>>, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<ARSalesPrice.itemClassID>>>>>, Where<ARSalesPrice.itemStatus, NotIn3<INItemStatus.inactive, InventoryItemStatus.unknown, INItemStatus.toDelete>, And2<Where<Required<ARSalesPriceFilter.priceType>, Equal<PriceTypes.allPrices>, Or<ARSalesPrice.priceType, Equal<Required<ARSalesPriceFilter.priceType>>>>, And2<Where<ARSalesPrice.taxCalcMode, Equal<Required<ARSalesPriceFilter.taxCalcMode>>, Or<Not<FeatureInstalled<FeaturesSet.netGrossEntryMode>>>>, And2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPriceFilter.priceCode>>, Or<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPriceFilter.priceCode>>, Or<Required<ARSalesPriceFilter.priceCode>, IsNull>>>, And2<Where<ARSalesPrice.inventoryID, Equal<Required<ARSalesPriceFilter.inventoryID>>, Or<Required<ARSalesPriceFilter.inventoryID>, IsNull>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPriceFilter.siteID>>, Or<Required<ARSalesPriceFilter.siteID>, IsNull>>, And2<Where2<Where2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPriceFilter.effectiveAsOfDate>>, Or<ARSalesPrice.effectiveDate, IsNull>>, And<Where<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPriceFilter.effectiveAsOfDate>>, Or<ARSalesPrice.expirationDate, IsNull>>>>, Or<Required<ARSalesPriceFilter.effectiveAsOfDate>, IsNull>>, And<Where2<Where<Required<ARSalesPriceFilter.itemClassCD>, IsNull, Or<INItemClass.itemClassCD, Like<Required<ARSalesPriceFilter.itemClassCDWildcard>>>>, And2<Where<Required<ARSalesPriceFilter.inventoryPriceClassID>, IsNull, Or<Required<ARSalesPriceFilter.inventoryPriceClassID>, Equal<ARSalesPrice.priceClassID>>>, And2<Where<Required<ARSalesPriceFilter.ownerID>, IsNull, Or<Required<ARSalesPriceFilter.ownerID>, Equal<ARSalesPrice.priceManagerID>>>, And2<Where<Required<ARSalesPriceFilter.myWorkGroup>, Equal<False>, Or<ARSalesPrice.priceWorkgroupID, IsWorkgroupOfContact<CurrentValue<ARSalesPriceFilter.currentOwnerID>>>>, And<Where<Required<ARSalesPriceFilter.workGroupID>, IsNull, Or<Required<ARSalesPriceFilter.workGroupID>, Equal<ARSalesPrice.priceWorkgroupID>>>>>>>>>>>>>>>>, Aggregate<GroupBy<ARSalesPrice.priceType, GroupBy<ARSalesPrice.customerID, GroupBy<ARSalesPrice.custPriceClassID, GroupBy<ARSalesPrice.inventoryID, GroupBy<ARSalesPrice.uOM, GroupBy<ARSalesPrice.breakQty, GroupBy<ARSalesPrice.curyID, GroupBy<ARSalesPrice.siteID>>>>>>>>>>.Config>.GetCommand());
    if (objectList.Count > groupedSalesPrices.Count)
      throw new PXException("There are multiple price records (regular and promotional) that are effective on the same date. Use the Sales Price Worksheets (AR202010) form to create a worksheet by using the Copy Prices action.");
    this.CreateWorksheetDetailsFromSalesPrices(instance, groupedSalesPrices);
    throw new PXRedirectRequiredException((PXGraph) instance, "Create Price Worksheet");
  }

  /// <summary>
  /// Creates worksheet details from sales prices. Extended in Lexware Price Unit customization.
  /// </summary>
  /// <param name="graph">The ARPriceWorksheetMaint graph.</param>
  /// <param name="groupedSalesPrices">The grouped sales prices.</param>
  protected virtual void CreateWorksheetDetailsFromSalesPrices(
    ARPriceWorksheetMaint graph,
    List<object> groupedSalesPrices)
  {
    foreach (PXResult<ARSalesPrice> groupedSalesPrice in groupedSalesPrices)
    {
      ARSalesPrice price = PXResult<ARSalesPrice>.op_Implicit(groupedSalesPrice);
      ARPriceWorksheetDetail priceWorksheetDetail = new ARPriceWorksheetDetail()
      {
        RefNbr = ((PXSelectBase<ARPriceWorksheet>) graph.Document).Current.RefNbr,
        PriceType = price.PriceType
      };
      if (priceWorksheetDetail.PriceType == "C")
      {
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) price.CustomerID
        }));
        if (customer != null)
          priceWorksheetDetail.PriceCode = customer.AcctCD.TrimEnd();
        else
          continue;
      }
      else
        priceWorksheetDetail.PriceCode = price.CustPriceClassID != "BASE" ? price.CustPriceClassID?.TrimEnd() : (string) null;
      priceWorksheetDetail.CustomerID = price.CustomerID;
      priceWorksheetDetail.CustPriceClassID = price.CustPriceClassID != "BASE" ? price.CustPriceClassID : (string) null;
      ARPriceWorksheetDetail detail = ((PXSelectBase<ARPriceWorksheetDetail>) graph.Details).Insert(priceWorksheetDetail);
      this.FillWorksheetDetailFromSalesPriceOnWorksheetCreation(detail, price);
      ((PXSelectBase<ARPriceWorksheetDetail>) graph.Details).Update(detail);
    }
  }

  /// <summary>
  /// Fill worksheet detail from sales price on worksheet creation. Extended in Lexware Price Unit customization.
  /// </summary>
  /// <param name="detail">The detail.</param>
  /// <param name="price">The price.</param>
  protected virtual void FillWorksheetDetailFromSalesPriceOnWorksheetCreation(
    ARPriceWorksheetDetail detail,
    ARSalesPrice price)
  {
    detail.InventoryID = price.InventoryID;
    detail.UOM = price.UOM;
    detail.BreakQty = price.BreakQty;
    detail.CurrentPrice = price.SalesPrice;
    detail.CuryID = price.CuryID;
    detail.SiteID = price.SiteID;
    detail.SkipLineDiscounts = price.SkipLineDiscounts;
  }

  string IPXAuditSource.GetMainView() => "Records";

  IEnumerable<System.Type> IPXAuditSource.GetAuditedTables()
  {
    yield return typeof (ARSalesPrice);
  }

  public virtual void Persist()
  {
    this.ValidateSalesPrices();
    ((PXGraph) this).Persist();
  }

  protected virtual void ARSalesPrice_SalesPrice_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ARSalesPrice row))
    {
      e.NewValue = (object) 0M;
    }
    else
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
      if (inventoryItem == null || row.CuryID != ((PXGraph) this).Accessinfo.BaseCuryID)
        return;
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, inventoryItem.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID);
      e.NewValue = (object) (row.UOM == inventoryItem.BaseUnit ? (Decimal?) itemCurySettings?.BasePrice : new Decimal?(INUnitAttribute.ConvertToBase(sender, inventoryItem.InventoryID, row.UOM ?? inventoryItem.SalesUnit, ((Decimal?) itemCurySettings?.BasePrice).GetValueOrDefault(), INPrecision.UNITCOST)));
    }
  }

  protected virtual void ARSalesPrice_PriceType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ARSalesPrice))
      return;
    if (((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current != null && ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType != "A")
      e.NewValue = (object) ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType;
    else
      e.NewValue = (object) "B";
  }

  protected virtual void ARSalesPrice_SalesPrice_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARSalesPrice row) || !(this.MinGrossProfitValidation != "N"))
      return;
    DateTime? effectiveDate1 = row.EffectiveDate;
    if (!effectiveDate1.HasValue || !PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    if (inventoryItem == null)
      return;
    Decimal num1 = (Decimal) e.NewValue;
    if (row.UOM != inventoryItem.BaseUnit)
    {
      try
      {
        num1 = INUnitAttribute.ConvertFromBase(sender, inventoryItem.InventoryID, row.UOM, num1, INPrecision.UNITCOST);
      }
      catch (PXUnitConversionException ex)
      {
        sender.RaiseExceptionHandling<ARSalesPrice.salesPrice>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Failed to convet to Base Units and Check for Minimum Gross Profit requirement", (PXErrorLevel) 2));
        return;
      }
    }
    INItemCost cost = INItemCost.PK.Find((PXGraph) this, row.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID) ?? new INItemCost();
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, row.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID);
    Decimal num2 = PXPriceCostAttribute.MinPrice(inventoryItem, cost, itemCurySettings);
    if (row.CuryID != ((PXGraph) this).Accessinfo.BaseCuryID)
    {
      ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSetup<ARSetup>.Select((PXGraph) this, Array.Empty<object>()));
      if (string.IsNullOrEmpty(arSetup.DefaultRateTypeID))
        throw new PXSetPropertyException("Default Rate Type is not configured in Accounts Receivable Preferences.");
      string baseCuryId = ((PXGraph) this).Accessinfo.BaseCuryID;
      string curyId = row.CuryID;
      string defaultRateTypeId = arSetup.DefaultRateTypeID;
      effectiveDate1 = row.EffectiveDate;
      DateTime effectiveDate2 = effectiveDate1.Value;
      Decimal amount = num2;
      Decimal? customRate = new Decimal?(1M);
      num2 = this.ConvertAmt(baseCuryId, curyId, defaultRateTypeId, effectiveDate2, amount, customRate);
    }
    e.NewValue = (object) MinGrossProfitValidator.Validate<ARSalesPrice.salesPrice>(sender, (object) row, inventoryItem, this.MinGrossProfitValidation, new Decimal?(num1), new Decimal?(num2), (Decimal?) e.NewValue, new Decimal?(num2), MinGrossProfitValidator.Target.SalesPrice);
  }

  protected virtual void ARSalesPrice_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARSalesPrice.salesPrice>(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARSalesPrice> e)
  {
    if (string.IsNullOrEmpty(e.Row?.PriceCode))
      return;
    this.UpdateCustomerAndPriceClassFields(e.Row);
  }

  protected virtual void ARSalesPrice_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARSalesPrice row = (ARSalesPrice) e.Row;
    ARSalesPrice oldRow = (ARSalesPrice) e.OldRow;
    if (!sender.ObjectsEqual<ARSalesPrice.priceType>((object) row, (object) oldRow))
      row.PriceCode = (string) null;
    if (sender.ObjectsEqual<ARSalesPrice.priceCode>((object) row, (object) oldRow))
      return;
    this.UpdateCustomerAndPriceClassFields(row);
  }

  protected virtual void ARSalesPrice_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ARSalesPrice row = (ARSalesPrice) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.priceType>(sender, (object) row, ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType == "A");
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.inventoryID>(sender, (object) row, !((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.InventoryID.HasValue);
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.priceCode>(sender, (object) row, row.PriceType != "B");
  }

  protected virtual void ARSalesPrice_PriceCode_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    ARSalesPrice row = (ARSalesPrice) e.Row;
    if (row == null || row.PriceType == null)
      return;
    if (row.PriceType == "C")
    {
      string str = row.PriceCode;
      if (string.IsNullOrEmpty(str))
      {
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.CustomerID
        }));
        if (customer != null)
          row.PriceCode = str = customer.AcctCD;
      }
      e.ReturnState = (object) str;
    }
    else
    {
      if (e.ReturnState == null)
        e.ReturnState = (object) row.CustPriceClassID;
      if (row.PriceCode != null)
        return;
      row.PriceCode = row.CustPriceClassID;
    }
  }

  protected virtual void ARSalesPrice_PriceCode_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((ARSalesPrice) e.Row != null && e.NewValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[priceCode]"
      });
  }

  protected virtual void ARSalesPriceFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXCache pxCache1 = sender;
    object row1 = e.Row;
    string name1 = typeof (OwnedFilter.ownerID).Name;
    int num1;
    if (e.Row != null)
    {
      bool? nullable = (bool?) sender.GetValue(e.Row, typeof (OwnedFilter.myOwner).Name);
      bool flag = false;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    PXUIFieldAttribute.SetEnabled(pxCache1, row1, name1, num1 != 0);
    PXCache pxCache2 = sender;
    object row2 = e.Row;
    string name2 = typeof (OwnedFilter.workGroupID).Name;
    int num2;
    if (e.Row != null)
    {
      bool? nullable = (bool?) sender.GetValue(e.Row, typeof (OwnedFilter.myWorkGroup).Name);
      bool flag = false;
      num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 1;
    PXUIFieldAttribute.SetEnabled(pxCache2, row2, name2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<ARSalesPrice.priceCode>(sender, e.Row, ((ARSalesPriceFilter) e.Row).PriceType != "A" && ((ARSalesPriceFilter) e.Row).PriceType != "B");
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Records).Cache, "TaxCategoryID", PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>());
  }

  protected virtual void ARSalesPriceFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARSalesPriceFilter row = (ARSalesPriceFilter) e.Row;
    ARSalesPriceFilter oldRow = (ARSalesPriceFilter) e.OldRow;
    if (sender.ObjectsEqual<ARSalesPriceFilter.priceType>((object) oldRow, (object) row))
      return;
    row.PriceCode = (string) null;
  }

  protected virtual void ARSalesPrice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARSalesPrice row = (ARSalesPrice) e.Row;
    DateTime? nullable;
    if (row.IsPromotionalPrice.GetValueOrDefault())
    {
      nullable = row.ExpirationDate;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<ARSalesPrice.expirationDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[expirationDate]"
        }));
    }
    if (row.IsPromotionalPrice.GetValueOrDefault())
    {
      nullable = row.EffectiveDate;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<ARSalesPrice.effectiveDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[effectiveDate]"
        }));
    }
    nullable = row.ExpirationDate;
    DateTime? effectiveDate = row.EffectiveDate;
    if ((nullable.HasValue & effectiveDate.HasValue ? (nullable.GetValueOrDefault() < effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      sender.RaiseExceptionHandling<ARSalesPrice.expirationDate>((object) row, (object) row.ExpirationDate, (Exception) new PXSetPropertyException("The Expiration Date should not be earlier than the Effective Date.", (PXErrorLevel) 5));
    if (row.PriceType != "B" && row.PriceCode == null)
      sender.RaiseExceptionHandling<ARSalesPrice.priceCode>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[priceCode]"
      }));
    else
      this.UpdateCustomerAndPriceClassFields(row);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  protected virtual void ARSalesPriceInventoryIDCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  protected virtual void ARSalesPricePriceTypeCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
  }

  public string GetPriceType(string viewname)
  {
    if (viewname.Contains(typeof (ARSalesPriceFilter).Name) && ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current != null)
      return ((PXSelectBase<ARSalesPriceFilter>) this.Filter).Current.PriceType;
    return !viewname.Contains(typeof (ARSalesPrice).Name) || ((PXSelectBase<ARSalesPrice>) this.Records).Current == null ? "C" : ((PXSelectBase<ARSalesPrice>) this.Records).Current.PriceType;
  }

  protected virtual void ValidateSalesPrices()
  {
    foreach (ARSalesPrice price in NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Records).Cache.Inserted, ((PXSelectBase) this.Records).Cache.Updated))
    {
      DateTime? effectiveDate = (DateTime?) ARSalesPriceMaint.FindLastPrice((PXGraph) this, price)?.EffectiveDate;
      DateTime? nullable = price.EffectiveDate;
      if ((effectiveDate.HasValue & nullable.HasValue ? (effectiveDate.GetValueOrDefault() > nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable = price.ExpirationDate;
        if (!nullable.HasValue)
        {
          ((PXSelectBase) this.Records).Cache.RaiseExceptionHandling<ARSalesPrice.expirationDate>((object) price, (object) price.ExpirationDate, (Exception) new PXSetPropertyException((IBqlTable) price, "'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<ARSalesPrice.expirationDate>(((PXSelectBase) this.Records).Cache)
          }));
          throw new PXSetPropertyException((IBqlTable) price, "'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<ARSalesPrice.expirationDate>(((PXSelectBase) this.Records).Cache)
          });
        }
      }
      ARSalesPriceMaint.ValidateDuplicate((PXGraph) this, ((PXSelectBase) this.Records).Cache, price);
    }
  }

  public static void ValidateDuplicate(PXGraph graph, PXCache sender, ARSalesPrice price)
  {
    PXSelect<ARSalesPrice, Where<ARSalesPrice.priceType, Equal<Required<ARSalesPrice.priceType>>, And2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>, And<ARSalesPrice.custPriceClassID, IsNull, Or<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>, And<ARSalesPrice.customerID, IsNull, Or<ARSalesPrice.custPriceClassID, IsNull, And<ARSalesPrice.customerID, IsNull>>>>>>, And<ARSalesPrice.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPrice.siteID>>, Or<ARSalesPrice.siteID, IsNull, And<Required<ARSalesPrice.siteID>, IsNull>>>, And<ARSalesPrice.uOM, Equal<Required<ARSalesPrice.uOM>>, And<ARSalesPrice.isPromotionalPrice, Equal<Required<ARSalesPrice.isPromotionalPrice>>, And<ARSalesPrice.isFairValue, Equal<Required<ARSalesPrice.isFairValue>>, And<ARSalesPrice.breakQty, Equal<Required<ARSalesPrice.breakQty>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And<ARSalesPrice.recordID, NotEqual<Required<ARSalesPrice.recordID>>>>>>>>>>>>> pxSelect = new PXSelect<ARSalesPrice, Where<ARSalesPrice.priceType, Equal<Required<ARSalesPrice.priceType>>, And2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>, And<ARSalesPrice.custPriceClassID, IsNull, Or<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>, And<ARSalesPrice.customerID, IsNull, Or<ARSalesPrice.custPriceClassID, IsNull, And<ARSalesPrice.customerID, IsNull>>>>>>, And<ARSalesPrice.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPrice.siteID>>, Or<ARSalesPrice.siteID, IsNull, And<Required<ARSalesPrice.siteID>, IsNull>>>, And<ARSalesPrice.uOM, Equal<Required<ARSalesPrice.uOM>>, And<ARSalesPrice.isPromotionalPrice, Equal<Required<ARSalesPrice.isPromotionalPrice>>, And<ARSalesPrice.isFairValue, Equal<Required<ARSalesPrice.isFairValue>>, And<ARSalesPrice.breakQty, Equal<Required<ARSalesPrice.breakQty>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And<ARSalesPrice.recordID, NotEqual<Required<ARSalesPrice.recordID>>>>>>>>>>>>>(graph);
    string priceCode = ARSalesPriceMaint.ParsePriceCode(graph, price.PriceType, price.PriceCode);
    int? nullable1 = new int?();
    if (price.PriceType == "C" && priceCode != null)
      nullable1 = new int?(int.Parse(priceCode));
    object[] objArray1 = new object[12]
    {
      (object) price.PriceType,
      (object) nullable1,
      price.PriceType == "P" ? (object) priceCode : (object) (string) null,
      (object) price.InventoryID,
      (object) price.SiteID,
      (object) price.SiteID,
      (object) price.UOM,
      (object) price.IsPromotionalPrice,
      (object) price.IsFairValue,
      (object) price.BreakQty,
      (object) price.CuryID,
      (object) price.RecordID
    };
    foreach (PXResult<ARSalesPrice> pxResult in ((PXSelectBase<ARSalesPrice>) pxSelect).Select(objArray1))
    {
      ARSalesPrice salesPrice1 = PXResult<ARSalesPrice>.op_Implicit(pxResult);
      if (ARSalesPriceMaint.IsOverlapping(salesPrice1, price))
      {
        object[] objArray2 = new object[3]
        {
          (object) salesPrice1.SalesPrice,
          null,
          null
        };
        DateTime? nullable2 = salesPrice1.EffectiveDate;
        ref DateTime? local1 = ref nullable2;
        DateTime valueOrDefault;
        string str1;
        if (!local1.HasValue)
        {
          str1 = (string) null;
        }
        else
        {
          valueOrDefault = local1.GetValueOrDefault();
          str1 = valueOrDefault.ToShortDateString();
        }
        if (str1 == null)
          str1 = string.Empty;
        objArray2[1] = (object) str1;
        nullable2 = salesPrice1.ExpirationDate;
        ref DateTime? local2 = ref nullable2;
        string str2;
        if (!local2.HasValue)
        {
          str2 = (string) null;
        }
        else
        {
          valueOrDefault = local2.GetValueOrDefault();
          str2 = valueOrDefault.ToShortDateString();
        }
        if (str2 == null)
          str2 = string.Empty;
        objArray2[2] = (object) str2;
        PXSetPropertyException propertyException = new PXSetPropertyException("Duplicate Sales Price. This line overlaps with another Sales Price (Price: {0}, Effective Date: {1}, Expiration Date: {2})", (PXErrorLevel) 5, objArray2);
        sender.RaiseExceptionHandling<ARSalesPrice.uOM>((object) price, (object) price.UOM, (Exception) propertyException);
        throw propertyException;
      }
    }
  }

  public static ARSalesPrice FindLastPrice(PXGraph graph, ARSalesPrice price)
  {
    string priceCode = ARSalesPriceMaint.ParsePriceCode(graph, price.PriceType, price.PriceCode);
    return ((PXSelectBase<ARSalesPrice>) new PXSelect<ARSalesPrice, Where<ARSalesPrice.priceType, Equal<Required<ARSalesPrice.priceType>>, And2<Where2<Where<ARSalesPrice.customerID, Equal<Required<ARSalesPriceFilter.priceCode>>, And<ARSalesPrice.custPriceClassID, IsNull>>, Or<Where<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPriceFilter.priceCode>>, And<ARSalesPrice.customerID, IsNull>>>>, And<ARSalesPrice.inventoryID, Equal<Required<ARSalesPrice.inventoryID>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPrice.siteID>>, Or<ARSalesPrice.siteID, IsNull, And<Required<ARSalesPrice.siteID>, IsNull>>>, And<ARSalesPrice.uOM, Equal<Required<ARSalesPrice.uOM>>, And<ARSalesPrice.isPromotionalPrice, Equal<Required<ARSalesPrice.isPromotionalPrice>>, And<ARSalesPrice.isFairValue, Equal<Required<ARSalesPrice.isFairValue>>, And<ARSalesPrice.breakQty, Equal<Required<ARSalesPrice.breakQty>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And<ARSalesPrice.recordID, NotEqual<Required<ARSalesPrice.recordID>>>>>>>>>>>>, OrderBy<Desc<ARSalesPrice.effectiveDate>>>(graph)).SelectSingle(new object[12]
    {
      (object) price.PriceType,
      price.PriceType == "C" ? (object) priceCode : (object) (string) null,
      price.PriceType == "P" ? (object) priceCode : (object) (string) null,
      (object) price.InventoryID,
      (object) price.SiteID,
      (object) price.SiteID,
      (object) price.UOM,
      (object) price.IsPromotionalPrice,
      (object) price.IsFairValue,
      (object) price.BreakQty,
      (object) price.CuryID,
      (object) price.RecordID
    });
  }

  private static string ParsePriceCode(PXGraph graph, string priceType, string priceCode)
  {
    if (priceCode == null)
      return (string) null;
    if (priceType == "C")
    {
      PX.Objects.AR.Customer byCd = new CustomerRepository(graph).FindByCD(priceCode);
      if (byCd != null)
        return byCd.BAccountID.ToString();
    }
    return !(priceType == "P") ? (string) null : priceCode;
  }

  private Decimal ConvertAmt(
    string from,
    string to,
    string rateType,
    DateTime effectiveDate,
    Decimal amount,
    Decimal? customRate = 1M)
  {
    if (from == to)
      return amount;
    CurrencyRate curyRate1 = this.getCuryRate(from, to, rateType, effectiveDate);
    if (curyRate1 == null)
    {
      Decimal num = amount;
      Decimal? nullable = customRate;
      return (nullable.HasValue ? new Decimal?(num * nullable.GetValueOrDefault()) : new Decimal?()) ?? 1M;
    }
    if (!(curyRate1.CuryMultDiv == "M"))
    {
      Decimal num = amount;
      Decimal? curyRate2 = curyRate1.CuryRate;
      return (curyRate2.HasValue ? new Decimal?(num / curyRate2.GetValueOrDefault()) : new Decimal?()) ?? 1M;
    }
    Decimal num1 = amount;
    Decimal? curyRate3 = curyRate1.CuryRate;
    return (curyRate3.HasValue ? new Decimal?(num1 * curyRate3.GetValueOrDefault()) : new Decimal?()) ?? 1M;
  }

  private CurrencyRate getCuryRate(
    string from,
    string to,
    string curyRateType,
    DateTime curyEffDate)
  {
    return PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelectReadonly<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
    {
      (object) to,
      (object) from,
      (object) curyRateType,
      (object) curyEffDate
    }));
  }

  private string MinGrossProfitValidation
  {
    get
    {
      SOSetup soSetup = PXResultset<SOSetup>.op_Implicit(PXSelectBase<SOSetup, PXSelect<SOSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      return soSetup != null && !string.IsNullOrEmpty(soSetup.MinGrossProfitValidation) ? soSetup.MinGrossProfitValidation : "W";
    }
  }

  private void UpdateCustomerAndPriceClassFields(ARSalesPrice row)
  {
    switch (row.PriceType)
    {
      case "C":
        PX.Objects.AR.Customer byCd = this.CustomerRepository.FindByCD(row.PriceCode);
        if (byCd == null)
          break;
        row.CustomerID = byCd.BAccountID;
        row.CustPriceClassID = (string) null;
        break;
      case "P":
        row.CustomerID = new int?();
        row.CustPriceClassID = row.PriceCode;
        break;
      case "B":
        row.CustomerID = new int?();
        row.CustPriceClassID = (string) null;
        break;
    }
  }

  public static bool IsOverlapping(ARSalesPrice salesPrice1, ARSalesPrice salesPrice2)
  {
    DateTime? nullable1;
    DateTime? nullable2;
    if (salesPrice1.EffectiveDate.HasValue && salesPrice1.ExpirationDate.HasValue && salesPrice2.EffectiveDate.HasValue && salesPrice2.ExpirationDate.HasValue)
    {
      DateTime? effectiveDate = salesPrice1.EffectiveDate;
      nullable1 = salesPrice2.EffectiveDate;
      if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = salesPrice1.ExpirationDate;
        nullable2 = salesPrice2.EffectiveDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
      nullable2 = salesPrice1.EffectiveDate;
      nullable1 = salesPrice2.ExpirationDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = salesPrice1.ExpirationDate;
        nullable2 = salesPrice2.ExpirationDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
      nullable2 = salesPrice1.EffectiveDate;
      nullable1 = salesPrice2.EffectiveDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = salesPrice1.EffectiveDate;
        nullable2 = salesPrice2.ExpirationDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
    }
    nullable2 = salesPrice1.ExpirationDate;
    if (nullable2.HasValue)
    {
      nullable2 = salesPrice2.EffectiveDate;
      if (nullable2.HasValue)
      {
        nullable2 = salesPrice2.ExpirationDate;
        if (nullable2.HasValue)
        {
          nullable2 = salesPrice1.EffectiveDate;
          if (nullable2.HasValue)
            goto label_12;
        }
        nullable2 = salesPrice2.EffectiveDate;
        nullable1 = salesPrice1.ExpirationDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
    }
label_12:
    nullable1 = salesPrice1.EffectiveDate;
    if (nullable1.HasValue)
    {
      nullable1 = salesPrice2.ExpirationDate;
      if (nullable1.HasValue)
      {
        nullable1 = salesPrice2.EffectiveDate;
        if (nullable1.HasValue)
        {
          nullable1 = salesPrice1.ExpirationDate;
          if (nullable1.HasValue)
            goto label_17;
        }
        nullable1 = salesPrice2.ExpirationDate;
        nullable2 = salesPrice1.EffectiveDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_30;
      }
    }
label_17:
    nullable2 = salesPrice1.EffectiveDate;
    if (nullable2.HasValue)
    {
      nullable2 = salesPrice2.EffectiveDate;
      if (nullable2.HasValue)
      {
        nullable2 = salesPrice1.ExpirationDate;
        if (!nullable2.HasValue)
        {
          nullable2 = salesPrice2.ExpirationDate;
          if (!nullable2.HasValue)
            goto label_30;
        }
      }
    }
    nullable2 = salesPrice1.ExpirationDate;
    if (nullable2.HasValue)
    {
      nullable2 = salesPrice2.ExpirationDate;
      if (nullable2.HasValue)
      {
        nullable2 = salesPrice1.EffectiveDate;
        if (!nullable2.HasValue)
        {
          nullable2 = salesPrice2.EffectiveDate;
          if (!nullable2.HasValue)
            goto label_30;
        }
      }
    }
    nullable2 = salesPrice1.EffectiveDate;
    if (!nullable2.HasValue)
    {
      nullable2 = salesPrice1.ExpirationDate;
      if (!nullable2.HasValue)
        goto label_30;
    }
    nullable2 = salesPrice2.EffectiveDate;
    if (nullable2.HasValue)
      return false;
    nullable2 = salesPrice2.ExpirationDate;
    return !nullable2.HasValue;
label_30:
    return true;
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <returns>Sales Price.</returns>
  /// <remarks>AlwaysFromBaseCury flag in the SOSetup is considered when performing the calculation.</remarks>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date)
  {
    return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, inventoryID, new int?(), currencyinfo, UOM, date);
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="siteID">Warehouse</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <returns>Sales Price.</returns>
  /// <remarks>AlwaysFromBaseCury flag in the SOSetup is considered when performing the calculation.</remarks>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date)
  {
    return ARSalesPriceMaint.SingleARSalesPriceMaint.CalculateSalesPriceInt(sender, custPriceClass, inventoryID, siteID, currencyinfo, UOM, date);
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="custPriceClass">Customer Price Class</param>
  /// <param name="customerID">Customer</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="quantity">Quantity</param>
  /// <param name="date"></param>
  /// <param name="currentUnitPrice"></param>
  /// <returns>Sales Price.</returns>
  /// <remarks>AlwaysFromBaseCury flag in the SOSetup is considered when performing the calculation.</remarks>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice)
  {
    return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, customerID, inventoryID, new int?(), currencyinfo, UOM, quantity, date, currentUnitPrice, "T");
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="custPriceClass">Customer Price Class</param>
  /// <param name="customerID">Customer</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="quantity">Quantity</param>
  /// <param name="date"></param>
  /// <param name="currentUnitPrice"></param>
  /// <param name="taxCalcMode">Tax Calculation Mode</param>
  /// <returns>Sales Price.</returns>
  /// <remarks>AlwaysFromBaseCury flag in the SOSetup is considered when performing the calculation.</remarks>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode)
  {
    return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, customerID, inventoryID, new int?(), currencyinfo, UOM, quantity, date, currentUnitPrice, taxCalcMode);
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="custPriceClass">Customer Price Class</param>
  /// <param name="customerID">Customer</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="siteID">Warehouse</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="quantity">Quantity</param>
  /// <param name="date"></param>
  /// <param name="currentUnitPrice"></param>
  /// <returns>Sales Price.</returns>
  /// <remarks>AlwaysFromBaseCury flag in the SOSetup is considered when performing the calculation.</remarks>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice)
  {
    return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, UOM, quantity, date, currentUnitPrice, "T");
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="custPriceClass">Customer Price Class</param>
  /// <param name="customerID">Customer</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="siteID">Warehouse</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="quantity">Quantity</param>
  /// <param name="date"></param>
  /// <param name="currentUnitPrice"></param>
  /// <param name="taxCalcMode">Tax Calculation Mode</param>
  /// <returns>Sales Price.</returns>
  /// <remarks>AlwaysFromBaseCury flag in the SOSetup is considered when performing the calculation.</remarks>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode)
  {
    return ARSalesPriceMaint.SingleARSalesPriceMaint.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, UOM, quantity, date, currentUnitPrice, taxCalcMode);
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <param name="alwaysFromBaseCurrency">If true sales price is always calculated (converted) from Base Currency.</param>
  /// <returns>Sales Price.</returns>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, inventoryID, new int?(), currencyinfo, UOM, date, alwaysFromBaseCurrency);
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="siteID">Warehouse</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <param name="alwaysFromBaseCurrency">If true sales price is always calculated (converted) from Base Currency.</param>
  /// <returns>Sales Price.</returns>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return ARSalesPriceMaint.SingleARSalesPriceMaint.CalculateSalesPriceInt(sender, custPriceClass, inventoryID, siteID, currencyinfo, UOM, date, alwaysFromBaseCurrency);
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="custPriceClass">Customer Price Class</param>
  /// <param name="customerID">Customer</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="currencyinfo">Currency</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="quantity">Quantity</param>
  /// <param name="date">Date</param>
  /// <param name="alwaysFromBaseCurrency">If true sales price is always calculated (converted) from Base Currency.</param>
  /// <returns>Sales Price.</returns>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, customerID, inventoryID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, "T");
  }

  /// <summary>Calculates Sales Price.</summary>
  /// <param name="sender">Cache</param>
  /// <param name="custPriceClass">Customer Price Class</param>
  /// <param name="customerID">Customer</param>
  /// <param name="inventoryID">Inventory</param>
  /// <param name="currencyinfo">Currency info</param>
  /// <param name="quantity">Quantity</param>
  /// <param name="UOM">Unit of measure</param>
  /// <param name="date">Date</param>
  /// <param name="alwaysFromBaseCurrency">If true sales price is always calculated (converted) from Base Currency.</param>
  /// <param name="taxCalcMode">Tax Calculation Mode</param>
  /// <returns></returns>
  public static Decimal? CalculateSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    string taxCalcMode)
  {
    return ARSalesPriceMaint.SingleARSalesPriceMaint.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, new int?(), currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, taxCalcMode);
  }

  /// <summary>Calculates Fair Value Sales Price.</summary>
  public static ARSalesPriceMaint.SalesPriceItem CalculateFairValueSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return ARSalesPriceMaint.CalculateFairValueSalesPrice(sender, custPriceClass, customerID, inventoryID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, "T");
  }

  /// <summary>Calculates Fair Value Sales Price.</summary>
  public static ARSalesPriceMaint.SalesPriceItem CalculateFairValueSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    string taxCalcMode)
  {
    ARSalesPriceMaint.SalesPriceItem salesPriceItem = ARSalesPriceMaint.SingleARSalesPriceMaint.CalculateSalesPriceItem(sender, custPriceClass, customerID, inventoryID, new int?(), currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, true, taxCalcMode);
    Decimal? nullable = ARSalesPriceMaint.SingleARSalesPriceMaint.AdjustSalesPrice(sender, salesPriceItem, inventoryID, currencyinfo, UOM);
    return new ARSalesPriceMaint.SalesPriceItem(salesPriceItem?.UOM, nullable.GetValueOrDefault(), salesPriceItem?.CuryID, salesPriceItem?.PriceType, salesPriceItem != null && salesPriceItem.Prorated, salesPriceItem != null && salesPriceItem.Discountable);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, inventoryID, new int?(), currencyinfo, UOM, date);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date)
  {
    bool baseCurrencySetting = this.GetAlwaysFromBaseCurrencySetting(sender);
    return this.CalculateSalesPriceInt(sender, custPriceClass, new int?(), inventoryID, siteID, currencyinfo, new Decimal?(0M), UOM, date, baseCurrencySetting);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, new int?(), currencyinfo, UOM, quantity, date, currentUnitPrice);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, UOM, quantity, date, currentUnitPrice, "T");
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode)
  {
    bool baseCurrencySetting = this.GetAlwaysFromBaseCurrencySetting(sender);
    return new Decimal?(this.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), UOM, date, baseCurrencySetting, taxCalcMode).GetValueOrDefault());
  }

  public virtual bool GetAlwaysFromBaseCurrencySetting(PXCache sender)
  {
    ARSetup arSetup = (ARSetup) sender.Graph.Caches[typeof (ARSetup)].Current ?? PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
    return arSetup != null && arSetup.AlwaysFromBaseCury.GetValueOrDefault();
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual (ARSalesPriceMaint.SalesPriceItem, Decimal?) GetSalesPriceItemAndCalculatedPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode)
  {
    return this.GetSalesPriceItemAndCalculatedPrice(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, currencyinfo, UOM, quantity, date, currentUnitPrice, taxCalcMode);
  }

  public virtual (ARSalesPriceMaint.SalesPriceItem, Decimal?) GetSalesPriceItemAndCalculatedPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    Decimal? quantity,
    DateTime date,
    Decimal? currentUnitPrice,
    string taxCalcMode)
  {
    bool baseCurrencySetting = this.GetAlwaysFromBaseCurrencySetting(sender);
    ARSalesPriceMaint.SalesPriceItem salesPriceItem = this.CalculateSalesPriceItem(sender, custPriceClass, customerID, inventoryID, lotSerialNbr, siteID, currencyinfo, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), UOM, date, baseCurrencySetting, false, taxCalcMode);
    Decimal? nullable = this.AdjustSalesPrice(sender, salesPriceItem, inventoryID, currencyinfo, UOM);
    return (salesPriceItem, new Decimal?(nullable.GetValueOrDefault()));
  }

  public static void CheckNewUnitPrice<Line, Field>(PXCache sender, Line row, object newValue)
    where Line : class, IBqlTable, new()
    where Field : class, IBqlField
  {
    if (newValue != null && (Decimal) newValue != 0M)
      PXUIFieldAttribute.SetWarning<Field>(sender, (object) row, (string) null);
    else
      PXUIFieldAttribute.SetWarning<Field>(sender, (object) row, "Unit price has been set to zero because no effective unit price was found.");
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, inventoryID, new int?(), currencyinfo, UOM, date, alwaysFromBaseCurrency);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, new int?(), inventoryID, siteID, currencyinfo, new Decimal?(0M), UOM, date, alwaysFromBaseCurrency);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, new int?(), currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, false, "T");
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    string taxCalcMode)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, false, taxCalcMode);
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    bool isFairValue)
  {
    return this.CalculateSalesPriceInt(sender, custPriceClass, customerID, inventoryID, siteID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, isFairValue, "T");
  }

  public virtual Decimal? CalculateSalesPriceInt(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    bool isFairValue,
    string taxCalcMode)
  {
    ARSalesPriceMaint.SalesPriceItem salesPriceItem = this.CalculateSalesPriceItem(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, isFairValue, taxCalcMode);
    return this.AdjustSalesPrice(sender, salesPriceItem, inventoryID, currencyinfo, UOM);
  }

  public virtual ARSalesPriceMaint.SalesPriceItem CalculateSalesPriceItem(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    bool isFairValue)
  {
    return this.CalculateSalesPriceItem(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, isFairValue, "T");
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual ARSalesPriceMaint.SalesPriceItem CalculateSalesPriceItem(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    bool isFairValue,
    string taxCalcMode)
  {
    return this.CalculateSalesPriceItem(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, currencyinfo, quantity, UOM, date, alwaysFromBaseCurrency, isFairValue, "T");
  }

  public virtual ARSalesPriceMaint.SalesPriceItem CalculateSalesPriceItem(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool alwaysFromBaseCurrency,
    bool isFairValue,
    string taxCalcMode)
  {
    try
    {
      return this.FindSalesPrice(sender, custPriceClass, customerID, inventoryID, lotSerialNbr, siteID, currencyinfo.BaseCuryID, alwaysFromBaseCurrency ? currencyinfo.BaseCuryID : currencyinfo.CuryID, new Decimal?(Math.Abs(quantity.GetValueOrDefault())), UOM, date, isFairValue, taxCalcMode);
    }
    catch (PXUnitConversionException ex)
    {
      return (ARSalesPriceMaint.SalesPriceItem) null;
    }
  }

  public virtual Decimal? AdjustSalesPrice(
    PXCache sender,
    ARSalesPriceMaint.SalesPriceItem spItem,
    int? inventoryID,
    PX.Objects.CM.CurrencyInfo currencyinfo,
    string uom)
  {
    if (spItem == null || uom == null)
      return new Decimal?();
    Decimal curyval = spItem.Price;
    if (spItem.CuryID != currencyinfo.CuryID)
    {
      if (!currencyinfo.CuryRate.HasValue)
        throw new PXSetPropertyException("Currency Rate is not defined.", (PXErrorLevel) 2);
      PXCurrencyAttribute.CuryConvCury(sender, currencyinfo, spItem.Price, out curyval, true);
    }
    if (spItem.UOM != uom)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
      Decimal num = spItem.UOM != inventoryItem?.BaseUnit ? INUnitAttribute.ConvertFromBase(sender, inventoryID, spItem.UOM, curyval, INPrecision.UNITCOST) : curyval;
      curyval = uom != inventoryItem?.BaseUnit ? INUnitAttribute.ConvertToBase(sender, inventoryID, uom, num, INPrecision.UNITCOST) : num;
    }
    return new Decimal?(curyval);
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    string curyID,
    string UOM,
    DateTime date)
  {
    return this.FindSalesPrice(sender, custPriceClass, inventoryID, new int?(), curyID, UOM, date);
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? inventoryID,
    int? siteID,
    string curyID,
    string UOM,
    DateTime date)
  {
    return this.FindSalesPrice(sender, custPriceClass, new int?(), inventoryID, siteID, CurrencyCollection.GetBaseCurrency()?.CuryID, curyID, new Decimal?(0M), UOM, date);
  }

  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date)
  {
    return this.FindSalesPrice(sender, custPriceClass, customerID, inventoryID, new int?(), baseCuryID, curyID, quantity, UOM, date);
  }

  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    string taxCalcMode)
  {
    return this.FindSalesPrice(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, baseCuryID, curyID, quantity, UOM, date, false, taxCalcMode);
  }

  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date)
  {
    return this.FindSalesPrice(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, baseCuryID, curyID, quantity, UOM, date, false, "T");
  }

  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue)
  {
    return this.FindSalesPrice(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, baseCuryID, curyID, quantity, UOM, date, isFairValue, "T");
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue,
    string taxCalcMode)
  {
    return this.FindSalesPrice(sender, custPriceClass, customerID, inventoryID, (string) null, siteID, baseCuryID, curyID, quantity, UOM, date, isFairValue, taxCalcMode);
  }

  public virtual ARSalesPriceMaint.SalesPriceItem FindSalesPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue,
    string taxCalcMode)
  {
    ARSalesPriceMaint.SalesPriceItem salesPrice = this.SelectCustomItemPrice(sender, custPriceClass, customerID, inventoryID, lotSerialNbr, siteID, baseCuryID, curyID, quantity, UOM, date, isFairValue, taxCalcMode);
    if (salesPrice != null)
      return salesPrice;
    if (!RecordExistsSlot<ARSalesPrice, ARSalesPrice.recordID, Where<True, Equal<True>>>.IsRowsExists())
      return !isFairValue ? this.SelectDefaultItemPrice(sender, inventoryID, baseCuryID) : (ARSalesPriceMaint.SalesPriceItem) null;
    ARSalesPriceMaint.SalesPriceSelect salesPriceSelect = new ARSalesPriceMaint.SalesPriceSelect(sender, inventoryID, UOM, quantity.Value, isFairValue, taxCalcMode);
    salesPriceSelect.CustomerID = customerID;
    salesPriceSelect.CustPriceClass = custPriceClass;
    salesPriceSelect.CuryID = curyID;
    salesPriceSelect.SiteID = siteID;
    salesPriceSelect.Date = date;
    ARSalesPriceMaint.SalesPriceForCurrentUOM priceForCurrentUom = salesPriceSelect.ForCurrentUOM();
    ARSalesPriceMaint.SalesPriceForBaseUOM salesPriceForBaseUom = salesPriceSelect.ForBaseUOM();
    ARSalesPriceMaint.SalesPriceForSalesUOM priceForSalesUom = salesPriceSelect.ForSalesUOM();
    return priceForCurrentUom.SelectCustomerPrice() ?? salesPriceForBaseUom.SelectCustomerPrice() ?? priceForCurrentUom.SelectBasePrice() ?? salesPriceForBaseUom.SelectBasePrice() ?? (isFairValue ? (ARSalesPriceMaint.SalesPriceItem) null : this.SelectDefaultItemPrice(sender, inventoryID, baseCuryID)) ?? priceForSalesUom.SelectCustomerPrice() ?? priceForSalesUom.SelectBasePrice();
  }

  /// <summary>
  /// This method can be used to add new or override existing price selection logic.
  /// </summary>
  /// <returns>Returns SalesPriceItem. Standard price selection logic will be used if null is returned.</returns>
  public virtual ARSalesPriceMaint.SalesPriceItem SelectCustomItemPrice(
    PXCache sender,
    string custPriceClass,
    int? customerID,
    int? inventoryID,
    string lotSerialNbr,
    int? siteID,
    string baseCuryID,
    string curyID,
    Decimal? quantity,
    string UOM,
    DateTime date,
    bool isFairValue,
    string taxCalcMode)
  {
    return (ARSalesPriceMaint.SalesPriceItem) null;
  }

  /// <summary>
  /// Creates <see cref="T:PX.Objects.AR.ARSalesPriceMaint.SalesPriceItem" /> data container from sales price. Used by PriceUnit customization for Lexware.
  /// </summary>
  /// <param name="salesPrice">The sales price.</param>
  /// <param name="uom">The unit of measure.</param>
  /// <returns />
  protected virtual ARSalesPriceMaint.SalesPriceItem CreateSalesPriceItemFromSalesPrice(
    ARSalesPrice salesPrice,
    string uom)
  {
    if (salesPrice == null)
      return (ARSalesPriceMaint.SalesPriceItem) null;
    string uom1 = uom ?? salesPrice.UOM;
    Decimal valueOrDefault = salesPrice.SalesPrice.GetValueOrDefault();
    string curyId = salesPrice.CuryID;
    string priceType = salesPrice.PriceType;
    bool? nullable = salesPrice.IsProrated;
    int num = nullable.GetValueOrDefault() ? 1 : 0;
    ARSalesPriceMaint.SalesPriceItem itemFromSalesPrice = new ARSalesPriceMaint.SalesPriceItem(uom1, valueOrDefault, curyId, priceType, num != 0);
    nullable = salesPrice.IsPromotionalPrice;
    itemFromSalesPrice.IsPromotionalPrice = nullable.GetValueOrDefault();
    nullable = salesPrice.Discountable;
    itemFromSalesPrice.Discountable = nullable.GetValueOrDefault();
    nullable = salesPrice.SkipLineDiscounts;
    itemFromSalesPrice.SkipLineDiscounts = nullable.GetValueOrDefault();
    return itemFromSalesPrice;
  }

  /// <summary>
  /// Creates default <see cref="T:PX.Objects.AR.ARSalesPriceMaint.SalesPriceItem" /> data container from <see cref="T:PX.Objects.IN.InventoryItem" />. Used by PriceUnit customization for Lexware.
  /// </summary>
  /// <param name="inventoryItem">The inventory item.</param>
  /// <param name="curySettings">The inventory item currency settings.</param>
  /// <param name="baseCuryID">The base cury identifier.</param>
  /// <returns />
  protected virtual ARSalesPriceMaint.SalesPriceItem CreateDefaultSalesPriceItemFromInventoryItem(
    PX.Objects.IN.InventoryItem inventoryItem,
    InventoryItemCurySettings curySettings,
    string baseCuryID)
  {
    if (inventoryItem != null)
    {
      Decimal? basePrice;
      int num1;
      if (curySettings == null)
      {
        num1 = 1;
      }
      else
      {
        basePrice = curySettings.BasePrice;
        num1 = !basePrice.HasValue ? 1 : 0;
      }
      if (num1 == 0)
      {
        basePrice = curySettings.BasePrice;
        Decimal num2 = 0M;
        if (!(basePrice.GetValueOrDefault() == num2 & basePrice.HasValue))
        {
          string baseUnit = inventoryItem.BaseUnit;
          basePrice = curySettings.BasePrice;
          Decimal price = basePrice.Value;
          string curyid = baseCuryID;
          return new ARSalesPriceMaint.SalesPriceItem(baseUnit, price, curyid);
        }
      }
    }
    return (ARSalesPriceMaint.SalesPriceItem) null;
  }

  internal virtual ARSalesPriceMaint.SalesPriceItem SelectDefaultItemPrice(
    PXCache sender,
    int? inventoryID,
    string baseCuryID)
  {
    return ARSalesPriceMaint.SingleARSalesPriceMaint.CreateDefaultSalesPriceItemFromInventoryItem(PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID), InventoryItemCurySettings.PK.Find(sender.Graph, inventoryID, baseCuryID), baseCuryID);
  }

  internal class SalesPriceSelect
  {
    protected PXSelectBase<ARSalesPrice> SelectCommand;
    protected readonly PXCache Cache;

    public int? InventoryID { get; }

    public string UOM { get; }

    public Decimal Qty { get; }

    public int? CustomerID { get; set; }

    public string CustPriceClass { get; set; }

    public string CuryID { get; set; }

    public int? SiteID { get; set; }

    public DateTime Date { get; set; }

    public bool IsFairValue { get; set; }

    public string TaxCalcMode { get; set; }

    public SalesPriceSelect(PXCache cache, int? inventoryID, string uom, Decimal qty)
      : this(cache, inventoryID, uom, qty, false, "T")
    {
    }

    public SalesPriceSelect(
      PXCache cache,
      int? inventoryID,
      string uom,
      Decimal qty,
      bool isFairValue)
      : this(cache, inventoryID, uom, qty, isFairValue, "T")
    {
    }

    public SalesPriceSelect(
      PXCache cache,
      int? inventoryID,
      string uom,
      Decimal qty,
      bool isFairValue,
      string taxCalcMode)
    {
      this.Cache = cache;
      this.Qty = qty;
      this.InventoryID = inventoryID;
      this.UOM = uom;
      this.IsFairValue = isFairValue;
      this.TaxCalcMode = taxCalcMode;
      this.SelectCommand = (PXSelectBase<ARSalesPrice>) new PXSelect<ARSalesPrice, Where<ARSalesPrice.inventoryID, In<Required<ARSalesPrice.inventoryID>>, And<ARSalesPrice.isFairValue, Equal<Required<ARSalesPrice.isFairValue>>, And2<Where2<Where<ARSalesPrice.priceType, Equal<PriceTypes.customer>, And<ARSalesPrice.customerID, Equal<Required<ARSalesPrice.customerID>>>>, Or2<Where<ARSalesPrice.priceType, Equal<PriceTypes.customerPriceClass>, And<ARSalesPrice.custPriceClassID, Equal<Required<ARSalesPrice.custPriceClassID>>>>, Or<Where<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<Required<ARSalesPrice.customerID>, IsNull, And<Required<ARSalesPrice.custPriceClassID>, IsNull>>>>>>, And<ARSalesPrice.curyID, Equal<Required<ARSalesPrice.curyID>>, And2<Where<ARSalesPrice.siteID, Equal<Required<ARSalesPrice.siteID>>, Or<ARSalesPrice.siteID, IsNull>>, And2<Where2<Where<ARSalesPrice.breakQty, LessEqual<Required<ARSalesPrice.breakQty>>>, And<Where2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPrice.expirationDate>>>>, Or2<Where<ARSalesPrice.effectiveDate, LessEqual<Required<ARSalesPrice.effectiveDate>>, And<ARSalesPrice.expirationDate, IsNull>>, Or<Where<ARSalesPrice.expirationDate, GreaterEqual<Required<ARSalesPrice.expirationDate>>, And<ARSalesPrice.effectiveDate, IsNull, Or<ARSalesPrice.effectiveDate, IsNull, And<ARSalesPrice.expirationDate, IsNull>>>>>>>>>, And<Where<ARSalesPrice.taxCalcMode, Equal<PriceTaxCalculationMode.undefined>, Or<Where<ARSalesPrice.taxCalcMode, Equal<Required<ARSalesPrice.taxCalcMode>>, Or<Required<ARSalesPrice.taxCalcMode>, Equal<TaxCalculationMode.taxSetting>>>>>>>>>>>>, OrderBy<Asc<ARSalesPrice.priceType, Desc<ARSalesPrice.isPromotionalPrice, Desc<ARSalesPrice.siteID, Desc<ARSalesPrice.breakQty>>>>>>(cache.Graph);
    }

    protected ARSalesPrice SelectCustomerPrice(Decimal quantity, params object[] additionalParams)
    {
      return this.SelectCommand.SelectSingle(((IEnumerable<object>) new object[15]
      {
        (object) this.GetInventoryIDs(this.Cache, this.InventoryID),
        (object) this.IsFairValue,
        (object) this.CustomerID,
        (object) this.CustPriceClass,
        (object) this.CustomerID,
        (object) this.CustPriceClass,
        (object) this.CuryID,
        (object) this.SiteID,
        (object) quantity,
        (object) this.Date,
        (object) this.Date,
        (object) this.Date,
        (object) this.Date,
        (object) this.TaxCalcMode,
        (object) this.TaxCalcMode
      }).Concat<object>((IEnumerable<object>) additionalParams).ToArray<object>());
    }

    protected ARSalesPrice SelectBasePrice(Decimal quantity, params object[] additionalParams)
    {
      return this.SelectCommand.SelectSingle(((IEnumerable<object>) new object[15]
      {
        (object) this.GetInventoryIDs(this.Cache, this.InventoryID),
        (object) this.IsFairValue,
        (object) this.CustomerID,
        (object) this.CustPriceClass,
        null,
        null,
        (object) this.CuryID,
        (object) this.SiteID,
        (object) quantity,
        (object) this.Date,
        (object) this.Date,
        (object) this.Date,
        (object) this.Date,
        (object) this.TaxCalcMode,
        (object) this.TaxCalcMode
      }).Concat<object>((IEnumerable<object>) additionalParams).ToArray<object>());
    }

    /// <summary>
    /// This function is intended for customization purposes and allows to create an array of InventoryIDs to be selected.
    /// </summary>
    /// <param name="inventoryID">Original InventoryID</param>
    public virtual int?[] GetInventoryIDs(PXCache sender, int? inventoryID)
    {
      return new int?[1]{ inventoryID };
    }

    protected Decimal ConvertToBaseUOM()
    {
      return INUnitAttribute.ConvertToBase(this.Cache, this.InventoryID, this.UOM, this.Qty, INPrecision.QUANTITY);
    }

    public ARSalesPriceMaint.SalesPriceForCurrentUOM ForCurrentUOM()
    {
      ARSalesPriceMaint.SalesPriceForCurrentUOM priceForCurrentUom = new ARSalesPriceMaint.SalesPriceForCurrentUOM(this.Cache, this.InventoryID, this.UOM, this.Qty);
      priceForCurrentUom.CustomerID = this.CustomerID;
      priceForCurrentUom.CustPriceClass = this.CustPriceClass;
      priceForCurrentUom.CuryID = this.CuryID;
      priceForCurrentUom.SiteID = this.SiteID;
      priceForCurrentUom.Date = this.Date;
      priceForCurrentUom.IsFairValue = this.IsFairValue;
      priceForCurrentUom.TaxCalcMode = this.TaxCalcMode;
      return priceForCurrentUom;
    }

    public ARSalesPriceMaint.SalesPriceForBaseUOM ForBaseUOM()
    {
      ARSalesPriceMaint.SalesPriceForBaseUOM salesPriceForBaseUom = new ARSalesPriceMaint.SalesPriceForBaseUOM(this.Cache, this.InventoryID, this.UOM, this.Qty);
      salesPriceForBaseUom.CustomerID = this.CustomerID;
      salesPriceForBaseUom.CustPriceClass = this.CustPriceClass;
      salesPriceForBaseUom.CuryID = this.CuryID;
      salesPriceForBaseUom.SiteID = this.SiteID;
      salesPriceForBaseUom.Date = this.Date;
      salesPriceForBaseUom.IsFairValue = this.IsFairValue;
      salesPriceForBaseUom.TaxCalcMode = this.TaxCalcMode;
      return salesPriceForBaseUom;
    }

    public ARSalesPriceMaint.SalesPriceForSalesUOM ForSalesUOM()
    {
      ARSalesPriceMaint.SalesPriceForSalesUOM priceForSalesUom = new ARSalesPriceMaint.SalesPriceForSalesUOM(this.Cache, this.InventoryID, this.UOM, this.Qty);
      priceForSalesUom.CustomerID = this.CustomerID;
      priceForSalesUom.CustPriceClass = this.CustPriceClass;
      priceForSalesUom.CuryID = this.CuryID;
      priceForSalesUom.SiteID = this.SiteID;
      priceForSalesUom.Date = this.Date;
      priceForSalesUom.IsFairValue = this.IsFairValue;
      priceForSalesUom.TaxCalcMode = this.TaxCalcMode;
      return priceForSalesUom;
    }
  }

  internal class SalesPriceForCurrentUOM : ARSalesPriceMaint.SalesPriceSelect
  {
    public SalesPriceForCurrentUOM(PXCache cache, int? inventoryID, string uom, Decimal qty)
      : base(cache, inventoryID, uom, qty)
    {
      this.SelectCommand.WhereAnd<Where<ARSalesPrice.uOM, Equal<Required<ARSalesPrice.uOM>>>>();
    }

    public ARSalesPriceMaint.SalesPriceItem SelectCustomerPrice()
    {
      return this.SelectCustomerPrice(this.Qty, (object) this.UOM).With<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>((Func<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>) (p => ARSalesPriceMaint.SalesPriceItem.FromPrice(p, this.UOM)));
    }

    public ARSalesPriceMaint.SalesPriceItem SelectBasePrice()
    {
      return this.SelectBasePrice(this.Qty, (object) this.UOM).With<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>((Func<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>) (p => ARSalesPriceMaint.SalesPriceItem.FromPrice(p, this.UOM)));
    }
  }

  internal class SalesPriceForBaseUOM : ARSalesPriceMaint.SalesPriceSelect
  {
    private readonly Lazy<Decimal> _baseUnitQty;

    public Decimal BaseUnitQty => this._baseUnitQty.Value;

    public SalesPriceForBaseUOM(PXCache cache, int? inventoryID, string uom, Decimal qty)
      : base(cache, inventoryID, uom, qty)
    {
      this._baseUnitQty = new Lazy<Decimal>(new Func<Decimal>(((ARSalesPriceMaint.SalesPriceSelect) this).ConvertToBaseUOM));
      this.SelectCommand.Join<InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>, And<PX.Objects.IN.InventoryItem.baseUnit, Equal<ARSalesPrice.uOM>>>>>();
    }

    public ARSalesPriceMaint.SalesPriceItem SelectCustomerPrice()
    {
      return this.SelectCustomerPrice(this.BaseUnitQty).With<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>((Func<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>) (p => ARSalesPriceMaint.SalesPriceItem.FromPrice(p)));
    }

    public ARSalesPriceMaint.SalesPriceItem SelectBasePrice()
    {
      return this.SelectBasePrice(this.BaseUnitQty).With<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>((Func<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>) (p => ARSalesPriceMaint.SalesPriceItem.FromPrice(p)));
    }
  }

  internal class SalesPriceForSalesUOM : ARSalesPriceMaint.SalesPriceSelect
  {
    private readonly bool _usePriceAdjustmentMultiplier;
    private Decimal? _baseUnitQty;
    private readonly Lazy<Tuple<Decimal, string>> _salesInfo;

    public Decimal BaseUnitQty
    {
      get => this._baseUnitQty ?? (this._baseUnitQty = new Decimal?(this.ConvertToBaseUOM())).Value;
      set => this._baseUnitQty = new Decimal?(value);
    }

    private Tuple<Decimal, string> GetSalesInfo()
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(this.Cache.Graph, this.InventoryID);
      return Tuple.Create<Decimal, string>(INUnitAttribute.ConvertFromBase(this.Cache, this.InventoryID, inventoryItem.SalesUnit, this.BaseUnitQty, INPrecision.QUANTITY), inventoryItem.SalesUnit);
    }

    public Decimal SalesUnitQty => this._salesInfo.Value.Item1;

    public string SalesUOM => this._salesInfo.Value.Item2;

    public SalesPriceForSalesUOM(PXCache cache, int? inventoryID, string uom, Decimal qty)
      : base(cache, inventoryID, uom, qty)
    {
      this._salesInfo = new Lazy<Tuple<Decimal, string>>(new Func<Tuple<Decimal, string>>(this.GetSalesInfo));
      this.SelectCommand.Join<InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARSalesPrice.inventoryID>, And<PX.Objects.IN.InventoryItem.salesUnit, Equal<ARSalesPrice.uOM>>>>>();
      this._usePriceAdjustmentMultiplier = INUnitAttribute.UsePriceAdjustmentMultiplier(cache.Graph);
    }

    public ARSalesPriceMaint.SalesPriceItem SelectCustomerPrice()
    {
      return !this._usePriceAdjustmentMultiplier ? (ARSalesPriceMaint.SalesPriceItem) null : this.SelectCustomerPrice(this.SalesUnitQty).With<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>((Func<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>) (p => ARSalesPriceMaint.SalesPriceItem.FromPrice(p, this.SalesUOM)));
    }

    public ARSalesPriceMaint.SalesPriceItem SelectBasePrice()
    {
      return !this._usePriceAdjustmentMultiplier ? (ARSalesPriceMaint.SalesPriceItem) null : this.SelectBasePrice(this.SalesUnitQty).With<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>((Func<ARSalesPrice, ARSalesPriceMaint.SalesPriceItem>) (p => ARSalesPriceMaint.SalesPriceItem.FromPrice(p, this.SalesUOM)));
    }
  }

  public class SalesPriceItem
  {
    public string UOM { get; }

    public Decimal Price { get; }

    public string CuryID { get; }

    public string PriceType { get; }

    public bool IsPromotionalPrice { get; internal set; }

    public bool SkipLineDiscounts { get; internal set; }

    public bool Prorated { get; }

    public bool Discountable { get; internal set; }

    public SalesPriceItem(
      string uom,
      Decimal price,
      string curyid,
      string priceType = null,
      bool prorated = false,
      bool discountable = false)
    {
      this.UOM = uom;
      this.Price = price;
      this.CuryID = curyid;
      this.PriceType = priceType;
      this.Prorated = prorated;
      this.Discountable = discountable;
    }

    public static ARSalesPriceMaint.SalesPriceItem FromPrice(ARSalesPrice price, string uom = null)
    {
      return ARSalesPriceMaint.SingleARSalesPriceMaint.CreateSalesPriceItemFromSalesPrice(price, uom);
    }
  }
}
