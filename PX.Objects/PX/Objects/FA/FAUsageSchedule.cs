// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAUsageSchedule
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

[PXPrimaryGraph(typeof (UsageScheduleMaint))]
[PXCacheName("FA Usage Schedule")]
[Serializable]
public class FAUsageSchedule : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ScheduleID;
  protected 
  #nullable disable
  string _ScheduleCD;
  protected string _Description;
  protected int? _ReadUsageEveryValue;
  protected string _ReadUsageEveryPeriod;
  protected string _UsageUOM;
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
  public virtual int? ReadUsageEveryValue
  {
    get => this._ReadUsageEveryValue;
    set => this._ReadUsageEveryValue = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField]
  [FAUsageSchedule.readUsageEveryPeriod.List]
  public virtual string ReadUsageEveryPeriod
  {
    get => this._ReadUsageEveryPeriod;
    set => this._ReadUsageEveryPeriod = value;
  }

  [PXUIField(DisplayName = "UOM")]
  [INUnit(typeof (INTran.inventoryID))]
  public virtual string UsageUOM
  {
    get => this._UsageUOM;
    set => this._UsageUOM = value;
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

  public class PK : PrimaryKeyOf<FAUsageSchedule>.By<FAUsageSchedule.scheduleID>
  {
    public static FAUsageSchedule Find(PXGraph graph, int? scheduleID, PKFindOptions options = 0)
    {
      return (FAUsageSchedule) PrimaryKeyOf<FAUsageSchedule>.By<FAUsageSchedule.scheduleID>.FindBy(graph, (object) scheduleID, options);
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAUsageSchedule.scheduleID>
  {
  }

  public abstract class scheduleCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAUsageSchedule.scheduleCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAUsageSchedule.description>
  {
  }

  public abstract class readUsageEveryValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FAUsageSchedule.readUsageEveryValue>
  {
  }

  public abstract class readUsageEveryPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAUsageSchedule.readUsageEveryPeriod>
  {
    public const string Day = "D";
    public const string Week = "W";
    public const string Month = "M";
    public const string Year = "Y";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[4]{ "D", "W", "M", "Y" }, new string[4]
        {
          "Day",
          "Week",
          "Month",
          "Year"
        })
      {
      }
    }

    public class day : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FAUsageSchedule.readUsageEveryPeriod.day>
    {
      public day()
        : base("D")
      {
      }
    }

    public class week : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FAUsageSchedule.readUsageEveryPeriod.week>
    {
      public week()
        : base("W")
      {
      }
    }

    public class month : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FAUsageSchedule.readUsageEveryPeriod.month>
    {
      public month()
        : base("M")
      {
      }
    }

    public class year : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FAUsageSchedule.readUsageEveryPeriod.year>
    {
      public year()
        : base("Y")
      {
      }
    }
  }

  public abstract class usageUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAUsageSchedule.usageUOM>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FAUsageSchedule.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAUsageSchedule.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAUsageSchedule.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAUsageSchedule.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FAUsageSchedule.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAUsageSchedule.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAUsageSchedule.lastModifiedDateTime>
  {
  }
}
