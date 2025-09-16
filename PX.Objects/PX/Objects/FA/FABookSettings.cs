// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Book Preferences")]
[Serializable]
public class FABookSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BookID;
  protected int? _AssetID;
  protected bool? _Depreciate;
  protected bool? _UpdateGL;
  protected int? _DepreciationMethodID;
  protected 
  #nullable disable
  string _MidMonthType;
  protected short? _MidMonthDay;
  protected bool? _Bonus;
  protected bool? _Sect179;
  protected string _AveragingConvention;
  protected Decimal? _UsefulLife;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField(DisplayName = "Book")]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FABookSettings.assetID>>, And<FixedAsset.recordType, Equal<FARecordType.classType>>>>), UseCurrent = true, LeaveChildren = false)]
  [PXUIField]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBBool]
  [PXDefault(typeof (Search<FixedAsset.depreciable, Where<FixedAsset.assetID, Equal<Current<FABookSettings.assetID>>>>))]
  [PXUIField]
  public virtual bool? Depreciate
  {
    get => this._Depreciate;
    set => this._Depreciate = value;
  }

  [PXBool]
  [PXDBScalar(typeof (Search<FABook.updateGL, Where<FABook.bookID, Equal<FABookSettings.bookID>>>))]
  [PXDefault(false, typeof (Search<FABook.updateGL, Where<FABook.bookID, Equal<Current<FABookSettings.bookID>>>>))]
  [PXUIField(DisplayName = "Posting Book", Enabled = false)]
  public virtual bool? UpdateGL
  {
    get => this._UpdateGL;
    set => this._UpdateGL = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<FADepreciationMethod.methodID, Where2<Where<FADepreciationMethod.usefulLife, Equal<Current<FABookSettings.usefulLife>>, Or<FADepreciationMethod.usefulLife, IsNull, Or<FADepreciationMethod.usefulLife, Equal<decimal0>>>>, And<Where<FADepreciationMethod.recordType, NotEqual<FARecordType.assetType>>>>>), new Type[] {typeof (FADepreciationMethod.methodCD), typeof (FADepreciationMethod.depreciationMethod), typeof (FADepreciationMethod.usefulLife), typeof (FADepreciationMethod.recoveryPeriod), typeof (FADepreciationMethod.averagingConvention), typeof (FADepreciationMethod.recordType), typeof (FADepreciationMethod.description)}, SubstituteKey = typeof (FADepreciationMethod.methodCD), DescriptionField = typeof (FADepreciationMethod.description))]
  [PXUIField]
  [PXDefault]
  [PXUIEnabled(typeof (FABookSettings.depreciate))]
  public virtual int? DepreciationMethodID
  {
    get => this._DepreciationMethodID;
    set => this._DepreciationMethodID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Mid-Period Type")]
  [PXFormula(typeof (Selector<FABookSettings.bookID, FABook.midMonthType>))]
  [FABook.midMonthType.List]
  [PXDefault]
  [PXUIRequired(typeof (FABookSettings.midMonthType.IsRequired<FABookSettings.depreciate, FABookSettings.averagingConvention>))]
  [PXUIEnabled(typeof (FABookSettings.midMonthType.IsRequired<FABookSettings.depreciate, FABookSettings.averagingConvention>))]
  public virtual string MidMonthType
  {
    get => this._MidMonthType;
    set => this._MidMonthType = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Mid-Period Day")]
  [PXFormula(typeof (Selector<FABookSettings.bookID, FABook.midMonthDay>))]
  [PXDefault]
  [PXUIRequired(typeof (FABookSettings.midMonthType.IsRequired<FABookSettings.depreciate, FABookSettings.averagingConvention>))]
  [PXUIEnabled(typeof (FABookSettings.midMonthType.IsRequired<FABookSettings.depreciate, FABookSettings.averagingConvention>))]
  public virtual short? MidMonthDay
  {
    get => this._MidMonthDay;
    set => this._MidMonthDay = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bonus")]
  [PXUIEnabled(typeof (FABookSettings.depreciate))]
  public virtual bool? Bonus
  {
    get => this._Bonus;
    set => this._Bonus = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Sect. 179")]
  [PXUIEnabled(typeof (FABookSettings.depreciate))]
  public virtual bool? Sect179
  {
    get => this._Sect179;
    set => this._Sect179 = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Averaging Convention")]
  [PXDefault(typeof (Search<FADepreciationMethod.averagingConvention, Where<FADepreciationMethod.methodID, Equal<Current<FABookSettings.depreciationMethodID>>>>))]
  [FAAveragingConvention.List]
  [PXUIEnabled(typeof (Switch<Case2<Where<FABookSettings.depreciate, Equal<True>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>>>>, True>, False>))]
  [PXFormula(typeof (Switch<Case2<Where<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>>>, FAAveragingConvention.fullDay, Case2<Where<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>, FAAveragingConvention.fullPeriod>>, FABookSettings.averagingConvention>))]
  public virtual string AveragingConvention
  {
    get => this._AveragingConvention;
    set => this._AveragingConvention = value;
  }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(typeof (Search<FADepreciationMethod.percentPerYear, Where<FADepreciationMethod.methodID, Equal<Current<FABookSettings.depreciationMethodID>>>>))]
  [PXUIField(DisplayName = "Percent per Year")]
  [PXUIEnabled(typeof (Switch<Case2<Where<FABookSettings.depreciate, Equal<True>, And<Where<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianDiminishingValue>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValue>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandDiminishingValueEvenly>>>>>>>>>, True>, False>))]
  public virtual Decimal? PercentPerYear { get; set; }

  [PXDBDecimal(4, MinValue = 0.0)]
  [PXDefault(typeof (FixedAsset.usefulLife))]
  [PXUIField(DisplayName = "Useful Life, Years")]
  [PXUIRequired(typeof (FABookSettings.depreciate))]
  [PXUIEnabled(typeof (Switch<Case2<Where<FABookSettings.depreciate, Equal<True>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.australianPrimeCost>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, And<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, NotEqual<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>>>>, True>, False>))]
  [PXFormula(typeof (Switch<Case2<Where<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.australianPrimeCost>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLine>, Or<Selector<FABookSettings.depreciationMethodID, FADepreciationMethod.depreciationMethod>, Equal<FADepreciationMethod.depreciationMethod.newZealandStraightLineEvenly>>>>, Div<decimal100, FABookSettings.percentPerYear>>, FABookSettings.usefulLife>))]
  public virtual Decimal? UsefulLife
  {
    get => this._UsefulLife;
    set => this._UsefulLife = value;
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

  public class PK : PrimaryKeyOf<FABookSettings>.By<FABookSettings.bookID, FABookSettings.assetID>
  {
    public static FABookSettings Find(
      PXGraph graph,
      int? bookID,
      int? assetID,
      PKFindOptions options = 0)
    {
      return (FABookSettings) PrimaryKeyOf<FABookSettings>.By<FABookSettings.bookID, FABookSettings.assetID>.FindBy(graph, (object) bookID, (object) assetID, options);
    }
  }

  public static class FK
  {
    public class Book : 
      PrimaryKeyOf<FABook>.By<FABook.bookID>.ForeignKeyOf<FABookSettings>.By<FABookSettings.bookID>
    {
    }

    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FABookSettings>.By<FABookSettings.assetID>
    {
    }

    public class DepreciationMethod : 
      PrimaryKeyOf<FADepreciationMethod>.By<FADepreciationMethod.methodID>.ForeignKeyOf<FABookSettings>.By<FABookSettings.depreciationMethodID>
    {
    }
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookSettings.bookID>
  {
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookSettings.assetID>
  {
  }

  public abstract class depreciate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookSettings.depreciate>
  {
  }

  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookSettings.updateGL>
  {
  }

  public abstract class depreciationMethodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FABookSettings.depreciationMethodID>
  {
  }

  public abstract class midMonthType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABookSettings.midMonthType>
  {
    public class IsRequired<depreciateField, averagingConventionField> : 
      IIf<Where<depreciateField, Equal<True>, And<Where<averagingConventionField, Equal<FAAveragingConvention.modifiedPeriod>, Or<averagingConventionField, Equal<FAAveragingConvention.modifiedPeriod2>>>>>, True, False>
      where depreciateField : IBqlField
      where averagingConventionField : IBqlField
    {
    }
  }

  public abstract class midMonthDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FABookSettings.midMonthDay>
  {
  }

  public abstract class bonus : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookSettings.bonus>
  {
  }

  public abstract class sect179 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookSettings.sect179>
  {
  }

  public abstract class averagingConvention : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookSettings.averagingConvention>
  {
  }

  public abstract class percentPerYear : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABookSettings.percentPerYear>
  {
  }

  public abstract class usefulLife : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABookSettings.usefulLife>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABookSettings.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABookSettings.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookSettings.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookSettings.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FABookSettings.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookSettings.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABookSettings.lastModifiedDateTime>
  {
  }
}
