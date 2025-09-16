// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentSeason
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Replenishment Seasonality")]
[Serializable]
public class INReplenishmentSeason : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReplenishmentPolicyID;
  protected int? _SeasonID;
  protected bool? _Active;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected Decimal? _Factor;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBDefault(typeof (INReplenishmentPolicy.replenishmentPolicyID))]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXParent(typeof (INReplenishmentSeason.FK.ReplenishmentPolicy))]
  public virtual string ReplenishmentPolicyID
  {
    get => this._ReplenishmentPolicyID;
    set => this._ReplenishmentPolicyID = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? SeasonID
  {
    get => this._SeasonID;
    set => this._SeasonID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Season Start Date")]
  [PXDefault]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Season End Date")]
  [PXDefault]
  [PXVerifyEndDate(typeof (INReplenishmentSeason.startDate), AllowAutoChange = false)]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXUIField(DisplayName = "Factor")]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? Factor
  {
    get => this._Factor;
    set => this._Factor = value;
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
    PrimaryKeyOf<INReplenishmentSeason>.By<INReplenishmentSeason.replenishmentPolicyID, INReplenishmentSeason.seasonID>
  {
    public static INReplenishmentSeason Find(
      PXGraph graph,
      string replenishmentPolicyID,
      int? seasonID,
      PKFindOptions options = 0)
    {
      return (INReplenishmentSeason) PrimaryKeyOf<INReplenishmentSeason>.By<INReplenishmentSeason.replenishmentPolicyID, INReplenishmentSeason.seasonID>.FindBy(graph, (object) replenishmentPolicyID, (object) seasonID, options);
    }
  }

  public static class FK
  {
    public class ReplenishmentPolicy : 
      PrimaryKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.replenishmentPolicyID>.ForeignKeyOf<INReplenishmentSeason>.By<INReplenishmentSeason.replenishmentPolicyID>
    {
    }
  }

  public abstract class replenishmentPolicyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentSeason.replenishmentPolicyID>
  {
  }

  public abstract class seasonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentSeason.seasonID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INReplenishmentSeason.active>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentSeason.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentSeason.endDate>
  {
  }

  public abstract class factor : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INReplenishmentSeason.factor>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INReplenishmentSeason.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentSeason.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentSeason.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INReplenishmentSeason.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentSeason.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentSeason.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INReplenishmentSeason.Tstamp>
  {
  }
}
