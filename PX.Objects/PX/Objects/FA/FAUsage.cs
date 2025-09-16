// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAUsage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Usage")]
[Serializable]
public class FAUsage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected int? _Number;
  protected DateTime? _MeasurementDate;
  protected DateTime? _ScheduledDate;
  protected Guid? _MeasuredBy;
  protected Decimal? _Value;
  protected Decimal? _Difference;
  protected Decimal? _DepreciationPercent;
  protected 
  #nullable disable
  string _UsageUOM;
  protected bool? _Depreciated;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FAUsage.assetID>>>>))]
  [PXParent(typeof (Select<FADetails, Where<FADetails.assetID, Equal<Current<FAUsage.assetID>>>>))]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (FixedAsset))]
  public virtual int? Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  [PXDBDate]
  [PXUIField]
  [PXFormula(null, typeof (MaxCalc<FADetails.lastMeasurementUsageDate>))]
  public virtual DateTime? MeasurementDate
  {
    get => this._MeasurementDate;
    set => this._MeasurementDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (FADetails.nextMeasurementUsageDate))]
  [PXUIField]
  public virtual DateTime? ScheduledDate
  {
    get => this._ScheduledDate;
    set => this._ScheduledDate = value;
  }

  [PXDBGuid(false)]
  [PXSelector(typeof (EPEmployee.userID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Measured By")]
  [PXDefault]
  public virtual Guid? MeasuredBy
  {
    get => this._MeasuredBy;
    set => this._MeasuredBy = value;
  }

  [PXDBDecimal(4)]
  [PXDefault]
  [PXUIField(DisplayName = "Value", Required = true)]
  public virtual Decimal? Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Difference with Previos Measurement Value")]
  public virtual Decimal? Difference
  {
    get => this._Difference;
    set => this._Difference = value;
  }

  [PXDBDecimal(4)]
  [PXDefault]
  [PXUIField(DisplayName = "Depreciation Rate", Enabled = false)]
  [PXFormula(typeof (DepreciatedPartTotalUsage<FAUsage.value, FAUsage.assetID>))]
  public virtual Decimal? DepreciationPercent
  {
    get => this._DepreciationPercent;
    set => this._DepreciationPercent = value;
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  [PXUIField(DisplayName = "UOM", Enabled = false)]
  [PXDefault(typeof (Search2<FAUsageSchedule.usageUOM, LeftJoin<FixedAsset, On<FixedAsset.usageScheduleID, Equal<FAUsageSchedule.scheduleID>>>, Where<FixedAsset.assetID, Equal<Current<FAUsage.assetID>>>>))]
  public virtual string UsageUOM
  {
    get => this._UsageUOM;
    set => this._UsageUOM = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Depreciated", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Depreciated
  {
    get => this._Depreciated;
    set => this._Depreciated = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public class PK : PrimaryKeyOf<FAUsage>.By<FAUsage.assetID, FAUsage.number>
  {
    public static FAUsage Find(PXGraph graph, int? assetID, int? number)
    {
      return (FAUsage) PrimaryKeyOf<FAUsage>.By<FAUsage.assetID, FAUsage.number>.FindBy(graph, (object) assetID, (object) number, (PKFindOptions) 0);
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAUsage.assetID>
  {
  }

  public abstract class number : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAUsage.number>
  {
  }

  public abstract class measurementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAUsage.measurementDate>
  {
  }

  public abstract class scheduledDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FAUsage.scheduledDate>
  {
  }

  public abstract class measuredBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAUsage.measuredBy>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAUsage.value>
  {
  }

  public abstract class difference : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAUsage.difference>
  {
  }

  public abstract class depreciationPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAUsage.depreciationPercent>
  {
  }

  public abstract class usageUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAUsage.usageUOM>
  {
  }

  public abstract class depreciated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAUsage.depreciated>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FAUsage.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAUsage.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAUsage.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAUsage.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAUsage.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAUsage.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAUsage.lastModifiedDateTime>
  {
  }
}
