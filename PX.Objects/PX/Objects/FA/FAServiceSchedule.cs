// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAServiceSchedule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXPrimaryGraph(typeof (ServiceScheduleMaint))]
[PXCacheName("FA Service Schedule")]
[Serializable]
public class FAServiceSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ScheduleID;
  protected 
  #nullable disable
  string _ScheduleCD;
  protected string _Description;
  protected int? _ServiceEveryValue;
  protected string _ServiceEveryPeriod;
  protected Decimal? _ServiceAfterUsageValue;
  protected string _ServiceAfterUsageUOM;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity]
  [PXUIField]
  public virtual int? ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBString(10, IsKey = true, IsUnicode = true)]
  [PXUIField]
  public virtual string ScheduleCD
  {
    get => this._ScheduleCD;
    set => this._ScheduleCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField]
  public virtual int? ServiceEveryValue
  {
    get => this._ServiceEveryValue;
    set => this._ServiceEveryValue = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField]
  [FAUsageSchedule.readUsageEveryPeriod.List]
  public virtual string ServiceEveryPeriod
  {
    get => this._ServiceEveryPeriod;
    set => this._ServiceEveryPeriod = value;
  }

  [PXDBDecimal(4)]
  [PXDefault]
  [PXUIField(DisplayName = "Service after Usage", TabOrder = 4)]
  public virtual Decimal? ServiceAfterUsageValue
  {
    get => this._ServiceAfterUsageValue;
    set => this._ServiceAfterUsageValue = value;
  }

  [INUnit(typeof (INTran.inventoryID), TabOrder = 5)]
  public virtual string ServiceAfterUsageUOM
  {
    get => this._ServiceAfterUsageUOM;
    set => this._ServiceAfterUsageUOM = value;
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

  public class PK : PrimaryKeyOf<FAServiceSchedule>.By<FAServiceSchedule.scheduleID>
  {
    public static FAServiceSchedule Find(PXGraph graph, int? scheduleID, PKFindOptions options = 0)
    {
      return (FAServiceSchedule) PrimaryKeyOf<FAServiceSchedule>.By<FAServiceSchedule.scheduleID>.FindBy(graph, (object) scheduleID, options);
    }
  }

  public class UK : PrimaryKeyOf<FAServiceSchedule>.By<FAServiceSchedule.scheduleCD>
  {
    public static FAServiceSchedule Find(PXGraph graph, string scheduleCD, PKFindOptions options = 0)
    {
      return (FAServiceSchedule) PrimaryKeyOf<FAServiceSchedule>.By<FAServiceSchedule.scheduleCD>.FindBy(graph, (object) scheduleCD, options);
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAServiceSchedule.scheduleID>
  {
  }

  public abstract class scheduleCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAServiceSchedule.scheduleCD>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAServiceSchedule.description>
  {
  }

  public abstract class serviceEveryValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FAServiceSchedule.serviceEveryValue>
  {
  }

  public abstract class serviceEveryPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAServiceSchedule.serviceEveryPeriod>
  {
  }

  public abstract class serviceAfterUsageValue : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FAServiceSchedule.serviceAfterUsageValue>
  {
  }

  public abstract class serviceAfterUsageUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAServiceSchedule.serviceAfterUsageUOM>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FAServiceSchedule.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAServiceSchedule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAServiceSchedule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAServiceSchedule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FAServiceSchedule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAServiceSchedule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAServiceSchedule.lastModifiedDateTime>
  {
  }
}
