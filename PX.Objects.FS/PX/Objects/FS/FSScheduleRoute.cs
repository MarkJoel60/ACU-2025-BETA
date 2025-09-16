// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSScheduleRoute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSScheduleRoute")]
[Serializable]
public class FSScheduleRoute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FSSchedule.scheduleID))]
  [PXParent(typeof (Select<FSSchedule, Where<FSSchedule.scheduleID, Equal<Current<FSScheduleRoute.scheduleID>>>>))]
  public virtual int? ScheduleID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route ID")]
  [PXDefault]
  [FSSelectorRouteID]
  public virtual int? DfltRouteID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Order")]
  [PXDefault]
  public virtual 
  #nullable disable
  string GlobalSequence { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route Friday")]
  [FSSelectorRouteID]
  public virtual int? RouteIDFriday { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route Monday")]
  [FSSelectorRouteID]
  public virtual int? RouteIDMonday { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route Saturday")]
  [FSSelectorRouteID]
  public virtual int? RouteIDSaturday { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route Sunday")]
  [FSSelectorRouteID]
  public virtual int? RouteIDSunday { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route Thursday")]
  [FSSelectorRouteID]
  public virtual int? RouteIDThursday { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route Tuesday")]
  [FSSelectorRouteID]
  public virtual int? RouteIDTuesday { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Route Wednesday")]
  [FSSelectorRouteID]
  public virtual int? RouteIDWednesday { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Sequence Friday")]
  public virtual string SequenceFriday { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Sequence Monday")]
  public virtual string SequenceMonday { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Sequence Saturday")]
  public virtual string SequenceSaturday { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Sequence Sunday")]
  public virtual string SequenceSunday { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Sequence Thursday")]
  public virtual string SequenceThursday { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Sequence Tuesday")]
  public virtual string SequenceTuesday { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Sequence Wednesday")]
  public virtual string SequenceWednesday { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Delivery Notes")]
  public virtual string DeliveryNotes { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Internal Notes")]
  public virtual string InternalNotes { get; set; }

  [PXUIField(DisplayName = "CreatedByID")]
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXUIField(DisplayName = "CreatedByScreenID")]
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "CreatedDateTime")]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXUIField(DisplayName = "LastModifiedByID")]
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "LastModifiedDateTime")]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sorting Index")]
  public virtual int? SortingIndex { get; set; }

  public class PK : PrimaryKeyOf<FSScheduleRoute>.By<FSScheduleRoute.scheduleID>
  {
    public static FSScheduleRoute Find(PXGraph graph, int? scheduleID, PKFindOptions options = 0)
    {
      return (FSScheduleRoute) PrimaryKeyOf<FSScheduleRoute>.By<FSScheduleRoute.scheduleID>.FindBy(graph, (object) scheduleID, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.scheduleID>
    {
    }

    public class DfltRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.dfltRouteID>
    {
    }

    public class MondayRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.routeIDMonday>
    {
    }

    public class TuesdayRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.routeIDTuesday>
    {
    }

    public class WednesdayRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.routeIDWednesday>
    {
    }

    public class ThursdayRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.routeIDThursday>
    {
    }

    public class FridayRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.routeIDFriday>
    {
    }

    public class SaturdayRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.routeIDSaturday>
    {
    }

    public class SundayRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSScheduleRoute>.By<FSScheduleRoute.routeIDSunday>
    {
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleRoute.scheduleID>
  {
  }

  public abstract class dfltRouteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleRoute.dfltRouteID>
  {
  }

  public abstract class globalSequence : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.globalSequence>
  {
  }

  public abstract class routeIDFriday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleRoute.routeIDFriday>
  {
  }

  public abstract class routeIDMonday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleRoute.routeIDMonday>
  {
  }

  public abstract class routeIDSaturday : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSScheduleRoute.routeIDSaturday>
  {
  }

  public abstract class routeIDSunday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleRoute.routeIDSunday>
  {
  }

  public abstract class routeIDThursday : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSScheduleRoute.routeIDThursday>
  {
  }

  public abstract class routeIDTuesday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleRoute.routeIDTuesday>
  {
  }

  public abstract class routeIDWednesday : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSScheduleRoute.routeIDWednesday>
  {
  }

  public abstract class sequenceFriday : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.sequenceFriday>
  {
  }

  public abstract class sequenceMonday : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.sequenceMonday>
  {
  }

  public abstract class sequenceSaturday : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.sequenceSaturday>
  {
  }

  public abstract class sequenceSunday : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.sequenceSunday>
  {
  }

  public abstract class sequenceThursday : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.sequenceThursday>
  {
  }

  public abstract class sequenceTuesday : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.sequenceTuesday>
  {
  }

  public abstract class sequenceWednesday : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.sequenceWednesday>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSScheduleRoute.noteID>
  {
  }

  public abstract class deliveryNotes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.deliveryNotes>
  {
  }

  public abstract class internalNotes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.internalNotes>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSScheduleRoute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSScheduleRoute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSScheduleRoute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSScheduleRoute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSScheduleRoute.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSScheduleRoute.Tstamp>
  {
  }

  public abstract class sortingIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSScheduleRoute.sortingIndex>
  {
  }
}
