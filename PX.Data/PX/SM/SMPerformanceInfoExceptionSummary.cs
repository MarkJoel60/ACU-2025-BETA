// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoExceptionSummary
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("SQL Performance Info")]
[PXHidden]
public class SMPerformanceInfoExceptionSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "TraceID")]
  public int? TraceMessageId { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Tenant")]
  public 
  #nullable disable
  string Tenant { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Exception Type")]
  public string ExceptionType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Exception Message")]
  public string ExceptionMessage { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, UseTimeZone = false, DisplayMask = "g")]
  [PXUIField(DisplayName = "Last Occurred")]
  public System.DateTime? LastOccured { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Last Screen")]
  public string LastUrl { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Last Command Target")]
  public string LastCommandTarget { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Last Command Name")]
  public string LastCommandName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Last Stack Trace")]
  public string LastStackTrace { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Count")]
  public int? Count { get; set; }

  public abstract class traceMessageId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.traceMessageId>
  {
  }

  public abstract class tenant : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.tenant>
  {
  }

  public abstract class exceptionType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.exceptionType>
  {
  }

  public abstract class exceptionMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.exceptionMessage>
  {
  }

  public abstract class lastOccured : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.lastOccured>
  {
  }

  public abstract class lastUrl : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.lastUrl>
  {
  }

  public abstract class lastCommandTarget : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.lastCommandTarget>
  {
  }

  public abstract class lastCommandName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.lastCommandName>
  {
  }

  public abstract class lastStackTrace : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.lastStackTrace>
  {
  }

  public abstract class count : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoExceptionSummary.count>
  {
  }
}
