// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemClassRep
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Item Class Replenishment")]
[Serializable]
public class INItemClassRep : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ItemClassID;
  protected 
  #nullable disable
  string _ReplenishmentClassID;
  protected string _ReplenishmentMethod;
  protected string _ReplenishmentSource;
  protected int? _ReplenishmentSourceSiteID;
  protected string _ReplenishmentPolicyID;
  protected short? _TransferLeadTime;
  protected Decimal? _TransferERQ;
  protected Decimal? _ServiceLevel;
  protected string _ForecastModelType;
  protected string _ForecastPeriodType;
  protected int? _HistoryDepth;
  protected Decimal? _ESSmoothingConstantL;
  protected Decimal? _ESSmoothingConstantT;
  protected Decimal? _ESSmoothingConstantS;
  protected bool? _AutoFitModel;
  protected DateTime? _LaunchDate;
  protected DateTime? _TerminationDate;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBDefault(typeof (INItemClass.itemClassID))]
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (INItemClassRep.FK.ItemClass))]
  public virtual int? ItemClassID
  {
    get => this._ItemClassID;
    set => this._ItemClassID = value;
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
  [PXUIField(DisplayName = "Method")]
  [PXDefault("N")]
  [INReplenishmentMethod.List]
  public virtual string ReplenishmentMethod
  {
    get => this._ReplenishmentMethod;
    set => this._ReplenishmentMethod = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Source")]
  [PXDefault("P", typeof (Select<INReplenishmentClass, Where<INReplenishmentClass.replenishmentClassID, Equal<Current<INItemClassRep.replenishmentClassID>>>>), SourceField = typeof (INReplenishmentClass.replenishmentSource))]
  [INReplenishmentSource.List]
  [PXFormula(typeof (Default<INItemClassRep.replenishmentClassID>))]
  public virtual string ReplenishmentSource
  {
    get => this._ReplenishmentSource;
    set => this._ReplenishmentSource = value;
  }

  [ReplenishmentSourceSite(typeof (INItemClassRep.replenishmentSource), DisplayName = "Replenishment Warehouse")]
  [PXForeignReference(typeof (INItemClassRep.FK.ReplenishmentSourceSite))]
  public virtual int? ReplenishmentSourceSiteID
  {
    get => this._ReplenishmentSourceSiteID;
    set => this._ReplenishmentSourceSiteID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Seasonality")]
  [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof (INReplenishmentPolicy.descr))]
  [PXDefault]
  public virtual string ReplenishmentPolicyID
  {
    get => this._ReplenishmentPolicyID;
    set => this._ReplenishmentPolicyID = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 20000)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Transfer Lead Time")]
  public virtual short? TransferLeadTime
  {
    get => this._TransferLeadTime;
    set => this._TransferLeadTime = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer ERQ")]
  public virtual Decimal? TransferERQ
  {
    get => this._TransferERQ;
    set => this._TransferERQ = value;
  }

  [PXDBDecimal(6, MinValue = 0.500001, MaxValue = 0.999999)]
  [PXUIField(DisplayName = "Service Level", Visible = true)]
  [PXDefault(TypeCode.Decimal, "0.8400000")]
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
    [PXDependsOnFields(new Type[] {typeof (INItemClassRep.serviceLevel)})] get
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

  [PXDBString(3, IsFixed = true)]
  [DemandForecastModelType.List]
  [PXUIField(DisplayName = "Demand Forecast Model")]
  [PXDefault("NNN")]
  public virtual string ForecastModelType
  {
    get => this._ForecastModelType;
    set => this._ForecastModelType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [DemandPeriodType.List]
  [PXDefault("MT")]
  [PXUIField(DisplayName = "Forecast Period Type")]
  public virtual string ForecastPeriodType
  {
    get => this._ForecastPeriodType;
    set => this._ForecastPeriodType = value;
  }

  [PXDBInt(MinValue = 0, MaxValue = 20000)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Periods to Analyze")]
  public virtual int? HistoryDepth
  {
    get => this._HistoryDepth;
    set => this._HistoryDepth = value;
  }

  [PXDBDecimal(9, MinValue = 0.0, MaxValue = 1.0)]
  [PXUIField(DisplayName = "Level Smoothing Constant")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ESSmoothingConstantL
  {
    get => this._ESSmoothingConstantL;
    set => this._ESSmoothingConstantL = value;
  }

  [PXDBDecimal(9)]
  [PXUIField(DisplayName = "Trend Smoothing Constant")]
  public virtual Decimal? ESSmoothingConstantT
  {
    get => this._ESSmoothingConstantT;
    set => this._ESSmoothingConstantT = value;
  }

  [PXDBDecimal(9)]
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

  [PXDBDate]
  [PXUIField(DisplayName = "Launch Date")]
  public virtual DateTime? LaunchDate
  {
    get => this._LaunchDate;
    set => this._LaunchDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Termination Date")]
  public virtual DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
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
    PrimaryKeyOf<INItemClassRep>.By<INItemClassRep.itemClassID, INItemClassRep.curyID, INItemClassRep.replenishmentClassID>
  {
    public static INItemClassRep Find(
      PXGraph graph,
      int? itemClassID,
      string curyID,
      string replenishmentClassID,
      PKFindOptions options = 0)
    {
      return (INItemClassRep) PrimaryKeyOf<INItemClassRep>.By<INItemClassRep.itemClassID, INItemClassRep.curyID, INItemClassRep.replenishmentClassID>.FindBy(graph, (object) itemClassID, (object) curyID, (object) replenishmentClassID, options);
    }
  }

  public static class FK
  {
    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INItemClassRep>.By<INItemClassRep.itemClassID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<InventoryItemCurySettings>.By<INItemClassRep.curyID>
    {
    }

    public class ReplenishmentClass : 
      PrimaryKeyOf<INReplenishmentClass>.By<INReplenishmentClass.replenishmentClassID>.ForeignKeyOf<INItemClassRep>.By<INItemClassRep.replenishmentClassID>
    {
    }

    public class ReplenishmentPolicy : 
      PrimaryKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.replenishmentPolicyID>.ForeignKeyOf<INItemClassRep>.By<INItemClassRep.replenishmentPolicyID>
    {
    }

    public class ReplenishmentSourceSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemClassRep>.By<INItemClassRep.replenishmentSourceSiteID>
    {
    }
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemClassRep.itemClassID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClassRep.curyID>
  {
  }

  public abstract class replenishmentClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.replenishmentClassID>
  {
  }

  public abstract class replenishmentMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.replenishmentMethod>
  {
  }

  public abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.replenishmentSource>
  {
  }

  public abstract class replenishmentSourceSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemClassRep.replenishmentSourceSiteID>
  {
  }

  public abstract class replenishmentPolicyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.replenishmentPolicyID>
  {
  }

  public abstract class transferLeadTime : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INItemClassRep.transferLeadTime>
  {
  }

  public abstract class transferERQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemClassRep.transferERQ>
  {
  }

  public abstract class serviceLevel : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClassRep.serviceLevel>
  {
  }

  public abstract class serviceLevelPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClassRep.serviceLevelPct>
  {
  }

  public abstract class forecastModelType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.forecastModelType>
  {
  }

  public abstract class forecastPeriodType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.forecastPeriodType>
  {
  }

  public abstract class historyDepth : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemClassRep.historyDepth>
  {
  }

  public abstract class eSSmoothingConstantL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClassRep.eSSmoothingConstantL>
  {
  }

  public abstract class eSSmoothingConstantT : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClassRep.eSSmoothingConstantT>
  {
  }

  public abstract class eSSmoothingConstantS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemClassRep.eSSmoothingConstantS>
  {
  }

  public abstract class autoFitModel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemClassRep.autoFitModel>
  {
  }

  public abstract class launchDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemClassRep.launchDate>
  {
  }

  public abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemClassRep.terminationDate>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemClassRep.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemClassRep.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemClassRep.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassRep.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemClassRep.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemClassRep.Tstamp>
  {
  }
}
