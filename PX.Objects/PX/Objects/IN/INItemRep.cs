// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemRep
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Item Replenishment Settings")]
[Serializable]
public class INItemRep : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _ReplenishmentClassID;
  protected string _ReplenishmentSource;
  protected string _ReplenishmentMethod;
  protected int? _ReplenishmentSourceSiteID;
  protected string _ReplenishmentPolicyID;
  protected int? _MaxShelfLife;
  protected DateTime? _LaunchDate;
  protected DateTime? _TerminationDate;
  protected Decimal? _ServiceLevel;
  protected Decimal? _SafetyStock;
  protected Decimal? _MinQty;
  protected Decimal? _MaxQty;
  protected Decimal? _TransferERQ;
  protected string _ForecastModelType;
  protected string _ForecastPeriodType;
  protected int? _HistoryDepth;
  protected Decimal? _ESSmoothingConstantL;
  protected Decimal? _ESSmoothingConstantT;
  protected Decimal? _ESSmoothingConstantS;
  protected bool? _AutoFitModel;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [StockItem(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID", Visible = false)]
  [PXParent(typeof (INItemRep.FK.InventoryItem))]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  public virtual string CuryID { get; set; }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<INReplenishmentClass.replenishmentClassID>))]
  public virtual string ReplenishmentClassID
  {
    get => this._ReplenishmentClassID;
    set => this._ReplenishmentClassID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [PXDefault("P", typeof (Coalesce<Search<INItemClassRep.replenishmentSource, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>, Search<INReplenishmentClass.replenishmentSource, Where<INReplenishmentClass.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>))]
  [PXFormula(typeof (Default<INItemRep.replenishmentClassID>))]
  [INReplenishmentSource.List]
  public virtual string ReplenishmentSource
  {
    get => this._ReplenishmentSource;
    set => this._ReplenishmentSource = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Method")]
  [PXDefault("N", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>), SourceField = typeof (INItemClassRep.replenishmentMethod))]
  [PXFormula(typeof (Default<INItemRep.replenishmentClassID>))]
  [INReplenishmentMethod.List]
  public virtual string ReplenishmentMethod
  {
    get => this._ReplenishmentMethod;
    set => this._ReplenishmentMethod = value;
  }

  [PXDefault(typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>, And<FeatureInstalled<FeaturesSet.warehouse>>>>>>))]
  [PXFormula(typeof (Default<INItemRep.replenishmentClassID>))]
  [ReplenishmentSourceSite(typeof (INItemClassRep.replenishmentSource), DisplayName = "Replenishment Warehouse", SetDefaultValue = false)]
  [PXForeignReference(typeof (INItemRep.FK.ReplenishmentSourceSite))]
  public virtual int? ReplenishmentSourceSiteID
  {
    get => this._ReplenishmentSourceSiteID;
    set => this._ReplenishmentSourceSiteID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Seasonality")]
  [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof (INReplenishmentPolicy.descr))]
  [PXDefault(typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>), SourceField = typeof (INItemClassRep.replenishmentPolicyID))]
  [PXFormula(typeof (Default<INItemRep.replenishmentClassID>))]
  public virtual string ReplenishmentPolicyID
  {
    get => this._ReplenishmentPolicyID;
    set => this._ReplenishmentPolicyID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Max. Shelf Life (Days)")]
  [PXDefault(0)]
  public virtual int? MaxShelfLife
  {
    get => this._MaxShelfLife;
    set => this._MaxShelfLife = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Launch Date")]
  [PXDefault(typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXFormula(typeof (Default<INItemRep.replenishmentClassID>))]
  public virtual DateTime? LaunchDate
  {
    get => this._LaunchDate;
    set => this._LaunchDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Termination Date")]
  [PXDefault(typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXFormula(typeof (Default<INItemRep.replenishmentClassID>))]
  public virtual DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
  }

  [PXDBDecimal(6, MinValue = 0.500001, MaxValue = 0.999999)]
  [PXUIField(DisplayName = "Service Level", Visible = true)]
  [PXDefault(TypeCode.Decimal, "0.840000", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXFormula(typeof (Default<INItemRep.replenishmentClassID>))]
  public virtual Decimal? ServiceLevel
  {
    get => this._ServiceLevel;
    set => this._ServiceLevel = value;
  }

  [PXDecimal(4, MinValue = 50.0001, MaxValue = 99.9999)]
  [PXUIField(DisplayName = "Service Level (%)", Visible = true)]
  [PXDefault(TypeCode.Decimal, "84.0000")]
  public virtual Decimal? ServiceLevelPct
  {
    [PXDependsOnFields(new Type[] {typeof (INItemRep.serviceLevel)})] get
    {
      Decimal? serviceLevel = this._ServiceLevel;
      Decimal num = 100.0M;
      return !serviceLevel.HasValue ? new Decimal?() : new Decimal?(serviceLevel.GetValueOrDefault() * num);
    }
    set
    {
      Decimal? nullable = value;
      Decimal num = 100.0M;
      this._ServiceLevel = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() / num) : new Decimal?();
    }
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Safety Stock")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SafetyStock
  {
    get => this._SafetyStock;
    set => this._SafetyStock = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Reorder Point")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Qty.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? MaxQty
  {
    get => this._MaxQty;
    set => this._MaxQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXUIField(DisplayName = "Transfer ERQ")]
  public virtual Decimal? TransferERQ
  {
    get => this._TransferERQ;
    set => this._TransferERQ = value;
  }

  [PXDBString(3, IsFixed = true)]
  [DemandForecastModelType.List]
  [PXUIField(DisplayName = "Demand Forecast Model")]
  [PXDefault("NNN", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXFormula(typeof (Default<INItemRep.forecastModelType>))]
  public virtual string ForecastModelType
  {
    get => this._ForecastModelType;
    set => this._ForecastModelType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [DemandPeriodType.List]
  [PXDefault("MT", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXUIField(DisplayName = "Forecast Period Type")]
  public virtual string ForecastPeriodType
  {
    get => this._ForecastPeriodType;
    set => this._ForecastPeriodType = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 20000)]
  [PXDefault(0, typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXUIField(DisplayName = "Periods to Analyze")]
  public virtual int? HistoryDepth
  {
    get => this._HistoryDepth;
    set => this._HistoryDepth = value;
  }

  [PXDBDecimal(9, MinValue = 0.0, MaxValue = 1.0)]
  [PXUIField(DisplayName = "Level Smoothing Constant")]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  public virtual Decimal? ESSmoothingConstantL
  {
    get => this._ESSmoothingConstantL;
    set => this._ESSmoothingConstantL = value;
  }

  [PXDBDecimal(9)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXUIField(DisplayName = "Trend Smoothing Constant")]
  public virtual Decimal? ESSmoothingConstantT
  {
    get => this._ESSmoothingConstantT;
    set => this._ESSmoothingConstantT = value;
  }

  [PXDBDecimal(9)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Select<INItemClassRep, Where<INItemClassRep.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<INItemClassRep.curyID, Equal<Current<INItemRep.curyID>>, And<INItemClassRep.replenishmentClassID, Equal<Current<INItemRep.replenishmentClassID>>>>>>))]
  [PXUIField(DisplayName = "Seasonality Smoothing Constant")]
  public virtual Decimal? ESSmoothingConstantS
  {
    get => this._ESSmoothingConstantS;
    set => this._ESSmoothingConstantS = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AutoFitModel
  {
    get => this._AutoFitModel;
    set => this._AutoFitModel = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemRep>.By<INItemRep.inventoryID, INItemRep.curyID, INItemRep.replenishmentClassID>
  {
    public static INItemRep Find(
      PXGraph graph,
      int? inventoryID,
      string curyID,
      string replenishmentClassID,
      PKFindOptions options = 0)
    {
      return (INItemRep) PrimaryKeyOf<INItemRep>.By<INItemRep.inventoryID, INItemRep.curyID, INItemRep.replenishmentClassID>.FindBy(graph, (object) inventoryID, (object) curyID, (object) replenishmentClassID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemRep>.By<INItemRep.inventoryID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<InventoryItemCurySettings>.By<INItemRep.curyID>
    {
    }

    public class ReplenishmentSourceSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemRep>.By<INItemRep.replenishmentSourceSiteID>
    {
    }

    public class ReplenishmentClass : 
      PrimaryKeyOf<INReplenishmentClass>.By<INReplenishmentClass.replenishmentClassID>.ForeignKeyOf<INItemRep>.By<INItemRep.replenishmentClassID>
    {
    }

    public class ReplenishmentPolicy : 
      PrimaryKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.replenishmentPolicyID>.ForeignKeyOf<INItemRep>.By<INItemRep.replenishmentPolicyID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemRep.inventoryID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemRep.curyID>
  {
  }

  public abstract class replenishmentClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.replenishmentClassID>
  {
  }

  public abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.replenishmentSource>
  {
  }

  public abstract class replenishmentMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.replenishmentMethod>
  {
  }

  public abstract class replenishmentSourceSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemRep.replenishmentSourceSiteID>
  {
  }

  public abstract class replenishmentPolicyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.replenishmentPolicyID>
  {
  }

  public abstract class maxShelfLife : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemRep.maxShelfLife>
  {
  }

  public abstract class launchDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemRep.launchDate>
  {
  }

  public abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemRep.terminationDate>
  {
  }

  public abstract class serviceLevel : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemRep.serviceLevel>
  {
  }

  public abstract class serviceLevelPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemRep.serviceLevelPct>
  {
  }

  public abstract class safetyStock : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemRep.safetyStock>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemRep.minQty>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemRep.maxQty>
  {
  }

  public abstract class transferERQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemRep.transferERQ>
  {
  }

  public abstract class forecastModelType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.forecastModelType>
  {
  }

  public abstract class forecastPeriodType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.forecastPeriodType>
  {
  }

  public abstract class historyDepth : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemRep.historyDepth>
  {
  }

  public abstract class eSSmoothingConstantL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemRep.eSSmoothingConstantL>
  {
  }

  public abstract class eSSmoothingConstantT : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemRep.eSSmoothingConstantT>
  {
  }

  public abstract class eSSmoothingConstantS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemRep.eSSmoothingConstantS>
  {
  }

  public abstract class autoFitModel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemRep.autoFitModel>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemRep.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemRep.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemRep.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemRep.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemRep.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemRep.Tstamp>
  {
  }
}
