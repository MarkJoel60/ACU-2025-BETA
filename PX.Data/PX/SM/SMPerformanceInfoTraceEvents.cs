// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoTraceEvents
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Trace Events Performance Info")]
public class SMPerformanceInfoTraceEvents : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (SMPerformanceInfo.recordId))]
  public int? ParentId { get; set; }

  [PXDBIdentity(IsKey = true)]
  public int? RecordId { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Start Time")]
  public double? RequestStartTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "TraceID")]
  public int? TraceMessageId { get; set; }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Source")]
  public 
  #nullable disable
  string Source { get; set; }

  [PXDBString(50)]
  [TraceTypeList]
  [PXUIField(DisplayName = "Event Type")]
  public string TraceType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "TraceID")]
  public int? StackTraceId { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, UseTimeZone = false, DisplayMask = "g")]
  [PXUIField(DisplayName = "Start Time")]
  public System.DateTime? EventDateTime { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Exception Type")]
  public string ExceptionType { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Event Details")]
  public string EventDetails { get; set; }

  public class PK : 
    PrimaryKeyOf<SMPerformanceInfoTraceEvents>.By<SMPerformanceInfoTraceEvents.parentId, SMPerformanceInfoTraceEvents.recordId>
  {
    public static SMPerformanceInfoTraceEvents Find(
      PXGraph graph,
      int? parentId,
      int? recordId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfoTraceEvents>.By<SMPerformanceInfoTraceEvents.parentId, SMPerformanceInfoTraceEvents.recordId>.FindBy(graph, (object) parentId, (object) recordId, options);
    }
  }

  public static class FK
  {
    public class PerformanceInfoTraceMessages : 
      PrimaryKeyOf<SMPerformanceInfoTraceMessages>.By<SMPerformanceInfoTraceMessages.recordId>.ForeignKeyOf<SMPerformanceInfoTraceEvents>.By<SMPerformanceInfoTraceEvents.traceMessageId>
    {
    }

    public class PerformanceInfoStackTrace : 
      PrimaryKeyOf<SMPerformanceInfoStackTrace>.By<SMPerformanceInfoStackTrace.recordId>.ForeignKeyOf<SMPerformanceInfoTraceEvents>.By<SMPerformanceInfoTraceEvents.stackTraceId>
    {
    }

    public class PerformanceInfo : 
      PrimaryKeyOf<SMPerformanceInfo>.By<SMPerformanceInfo.recordId>.ForeignKeyOf<SMPerformanceInfoTraceEvents>.By<SMPerformanceInfoTraceEvents.parentId>
    {
    }
  }

  public abstract class parentId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoTraceEvents.parentId>
  {
  }

  public abstract class recordId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoTraceEvents.recordId>
  {
  }

  public abstract class requestStartTime : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.requestStartTime>
  {
  }

  public abstract class traceMessageId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.traceMessageId>
  {
  }

  public abstract class source : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.source>
  {
  }

  public abstract class traceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.traceType>
  {
  }

  public abstract class stackTraceId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.stackTraceId>
  {
  }

  public abstract class eventDateTime : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.eventDateTime>
  {
  }

  public abstract class exceptionType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.exceptionType>
  {
  }

  public abstract class eventDetails : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoTraceEvents.eventDetails>
  {
  }
}
