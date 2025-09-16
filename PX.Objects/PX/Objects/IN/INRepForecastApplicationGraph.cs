// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INRepForecastApplicationGraph
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INRepForecastApplicationGraph : PXGraph<
#nullable disable
INRepForecastApplicationGraph>
{
  public PXFilter<INRepForecastApplicationGraph.Filter> filter;
  public PXCancel<INRepForecastApplicationGraph.Filter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<INItemSite, INRepForecastApplicationGraph.Filter, LeftJoin<InventoryItem, On<INItemSite.FK.InventoryItem>, LeftJoin<INItemClass, On<InventoryItem.FK.ItemClass>>>, Where<INItemSite.siteID, Equal<Current<INItemSite.siteID>>, And<INItemSite.planningMethod, Equal<INPlanningMethod.inventoryReplenishment>, And2<Where<Current<INRepForecastApplicationGraph.Filter.itemClassCDWildcard>, IsNull, Or<INItemClass.itemClassCD, Like<Current<INRepForecastApplicationGraph.Filter.itemClassCDWildcard>>>>, And2<Where<INItemSite.replenishmentSource, Equal<INReplenishmentSource.transfer>, Or<INItemSite.replenishmentSource, Equal<INReplenishmentSource.purchased>>>, And<INItemSite.lastForecastDate, IsNotNull, And<Where<INItemSite.lastFCApplicationDate, IsNull, Or<INItemSite.lastFCApplicationDate, Less<INItemSite.lastForecastDate>>>>>>>>>> Records;
  public PXSelect<INItemSiteReplenishment, Where<INItemSiteReplenishment.siteID, Equal<Current<INItemSite.siteID>>, And<INItemSiteReplenishment.inventoryID, Equal<Current<INItemSiteReplenishment.inventoryID>>>>> subItemSettings;

  public IEnumerable records()
  {
    foreach (PXResult<INItemSite, InventoryItem> pxResult in PXSelectBase<INItemSite, PXSelectJoin<INItemSite, LeftJoin<InventoryItem, On<INItemSite.FK.InventoryItem>, LeftJoin<INItemClass, On<InventoryItem.FK.ItemClass>>>, Where<INItemSite.siteID, Equal<Current<INRepForecastApplicationGraph.Filter.siteID>>, And<INItemSite.planningMethod, Equal<INPlanningMethod.inventoryReplenishment>, And<INItemSite.lastForecastDate, IsNotNull, And2<Where<Current<INRepForecastApplicationGraph.Filter.itemClassCDWildcard>, IsNull, Or<INItemClass.itemClassCD, Like<Current<INRepForecastApplicationGraph.Filter.itemClassCDWildcard>>>>, And<Where<INItemSite.lastFCApplicationDate, IsNull, Or<INItemSite.lastFCApplicationDate, Less<INItemSite.lastForecastDate>>>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      yield return (object) pxResult;
  }

  public INRepForecastApplicationGraph()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<INItemSite>) this.Records).SetProcessDelegate<ReplenishmentStatsUpdateGraph>(new PXProcessingBase<INItemSite>.ProcessItemDelegate<ReplenishmentStatsUpdateGraph>((object) new INRepForecastApplicationGraph.\u003C\u003Ec__DisplayClass6_0()
    {
      filter = ((PXSelectBase<INRepForecastApplicationGraph.Filter>) this.filter).Current
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  protected virtual void Filter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    INRepForecastApplicationGraph.Filter row = (INRepForecastApplicationGraph.Filter) e.Row;
    PXUIFieldAttribute.SetEnabled<INRepForecastApplicationGraph.Filter.siteID>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.warehouse>());
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PXSelectBase) this.Records).Cache, (object) null).For<INItemSite.minQtySuggested>((Action<PXUIFieldAttribute>) (fa => fa.Enabled = true));
    chained = chained.SameFor<INItemSite.maxQtySuggested>();
    chained.SameFor<INItemSite.safetyStockSuggested>();
  }

  public static void UpdateReplenishmentProc(
    ReplenishmentStatsUpdateGraph graph,
    INItemSite aItem,
    INRepForecastApplicationGraph.Filter filter)
  {
    PXCache cach1 = ((PXGraph) graph).Caches[typeof (INItemSite)];
    PXCache cach2 = ((PXGraph) graph).Caches[typeof (INItemSiteReplenishment)];
    INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(((PXSelectBase<INItemSite>) graph.inItemsSite).Select(new object[2]
    {
      (object) aItem.SiteID,
      (object) aItem.InventoryID
    }));
    INItemSite copy1 = (INItemSite) cach1.CreateCopy((object) inItemSite);
    copy1.MinQty = aItem.MinQtySuggested;
    copy1.MinQtyOverride = new bool?(true);
    copy1.MaxQty = aItem.MaxQtySuggested;
    copy1.MaxQtyOverride = new bool?(true);
    copy1.SafetyStock = aItem.SafetyStockSuggested;
    copy1.SafetyStockOverride = new bool?(true);
    copy1.LastFCApplicationDate = aItem.LastForecastDate;
    cach1.Update((object) copy1);
    foreach (PXResult<INItemSiteReplenishment> pxResult in ((PXSelectBase<INItemSiteReplenishment>) graph.inSubItemsSite).Select(new object[2]
    {
      (object) aItem.SiteID,
      (object) aItem.InventoryID
    }))
    {
      INItemSiteReplenishment siteReplenishment1 = PXResult<INItemSiteReplenishment>.op_Implicit(pxResult);
      INItemSiteReplenishment copy2 = (INItemSiteReplenishment) cach2.CreateCopy((object) siteReplenishment1);
      INItemSiteReplenishment siteReplenishment2 = copy2;
      Decimal? nullable1 = copy2.MinQtySuggested;
      Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
      siteReplenishment2.MinQty = nullable2;
      INItemSiteReplenishment siteReplenishment3 = copy2;
      nullable1 = copy2.SafetyStockSuggested;
      Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault());
      siteReplenishment3.SafetyStock = nullable3;
      INItemSiteReplenishment siteReplenishment4 = copy2;
      nullable1 = copy2.MaxQtySuggested;
      Decimal? nullable4 = new Decimal?(nullable1.GetValueOrDefault());
      siteReplenishment4.MaxQty = nullable4;
      cach2.Update((object) copy2);
    }
    ((PXGraph) graph).Actions.PressSave();
  }

  [Serializable]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _SiteID;
    protected string _ItemClassCD;
    protected string _ReplenishmentPolicyID;

    [Site(DisplayName = "Warehouse")]
    [PXDefault]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField]
    [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
    public virtual string ItemClassCD
    {
      get => this._ItemClassCD;
      set => this._ItemClassCD = value;
    }

    [PXString(IsUnicode = true)]
    [PXUIField]
    [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
    public virtual string ItemClassCDWildcard
    {
      get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClassCD);
      set
      {
      }
    }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Seasonality")]
    [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof (INReplenishmentPolicy.descr))]
    public virtual string ReplenishmentPolicyID
    {
      get => this._ReplenishmentPolicyID;
      set => this._ReplenishmentPolicyID = value;
    }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INRepForecastApplicationGraph.Filter.siteID>
    {
    }

    public abstract class itemClassCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INRepForecastApplicationGraph.Filter.itemClassCD>
    {
    }

    public abstract class itemClassCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INRepForecastApplicationGraph.Filter.itemClassCDWildcard>
    {
    }

    public abstract class replenishmentPolicyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INRepForecastApplicationGraph.Filter.replenishmentPolicyID>
    {
    }
  }
}
