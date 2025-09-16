// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABonusDetails
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
namespace PX.Objects.FA;

[PXCacheName("FA Bonus Details")]
[Serializable]
public class FABonusDetails : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BonusID;
  protected int? _LineNbr;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected Decimal? _BonusPercent;
  protected Decimal? _BonusMax;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<FABonus, Where<FABonus.bonusID, Equal<Current<FABonusDetails.bonusID>>>>))]
  [PXDBDefault(typeof (FABonus.bonusID))]
  public virtual int? BonusID
  {
    get => this._BonusID;
    set => this._BonusID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (FABonus))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Bonus, %", Required = true)]
  [PXDefault]
  public virtual Decimal? BonusPercent
  {
    get => this._BonusPercent;
    set => this._BonusPercent = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Max. Bonus")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BonusMax
  {
    get => this._BonusMax;
    set => this._BonusMax = value;
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

  public class PK : PrimaryKeyOf<FABonusDetails>.By<FABonusDetails.bonusID, FABonusDetails.lineNbr>
  {
    public static FABonusDetails Find(
      PXGraph graph,
      int? bonusID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FABonusDetails) PrimaryKeyOf<FABonusDetails>.By<FABonusDetails.bonusID, FABonusDetails.lineNbr>.FindBy(graph, (object) bonusID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Bonus : 
      PrimaryKeyOf<FABonus>.By<FABonus.bonusID>.ForeignKeyOf<FABonusDetails>.By<FABonusDetails.bonusID>
    {
    }
  }

  public abstract class bonusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABonusDetails.bonusID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABonusDetails.lineNbr>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABonusDetails.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FABonusDetails.endDate>
  {
  }

  public abstract class bonusPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FABonusDetails.bonusPercent>
  {
  }

  public abstract class bonusMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FABonusDetails.bonusMax>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABonusDetails.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABonusDetails.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABonusDetails.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABonusDetails.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FABonusDetails.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABonusDetails.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABonusDetails.lastModifiedDateTime>
  {
  }
}
