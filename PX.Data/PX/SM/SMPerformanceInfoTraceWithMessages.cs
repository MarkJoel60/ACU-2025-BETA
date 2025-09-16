// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoTraceWithMessages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXProjection(typeof (Select2<SMPerformanceInfoTraceMessages, InnerJoin<SMPerformanceInfoTraceEvents, On<SMPerformanceInfoTraceEvents.traceMessageId, Equal<SMPerformanceInfoTraceMessages.recordId>>, LeftJoin<SMPerformanceInfoStackTrace, On<SMPerformanceInfoStackTrace.recordId, Equal<SMPerformanceInfoTraceEvents.stackTraceId>>>>>), Persistent = false)]
[Serializable]
public class SMPerformanceInfoTraceWithMessages : SMPerformanceInfoTraceEvents
{
  [PXDBString(BqlTable = typeof (SMPerformanceInfoTraceMessages))]
  [PXUIField(DisplayName = "Message")]
  public 
  #nullable disable
  string MessageText { get; set; }

  [PXDBString(BqlTable = typeof (SMPerformanceInfoStackTrace))]
  [PXUIField(DisplayName = "Stack Trace")]
  public string StackTrace { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Message")]
  [JsonIgnore]
  public string MessageWithStackTrace
  {
    get
    {
      return string.IsNullOrEmpty(this.StackTrace) ? this.MessageText : $"{this.MessageText}\n\nStack Trace:\n\n{this.StackTrace}";
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Message")]
  public string ShortMessage
  {
    get
    {
      return string.IsNullOrEmpty(this.MessageText) || this.MessageText.Length <= 100 ? this.MessageText : this.MessageText.Substring(0, 100);
    }
  }

  public new class PK : 
    PrimaryKeyOf<SMPerformanceInfoTraceWithMessages>.By<SMPerformanceInfoTraceWithMessages.parentId, SMPerformanceInfoTraceWithMessages.recordId>
  {
    public static SMPerformanceInfoTraceWithMessages Find(
      PXGraph graph,
      int? parentId,
      int? recordId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfoTraceWithMessages>.By<SMPerformanceInfoTraceWithMessages.parentId, SMPerformanceInfoTraceWithMessages.recordId>.FindBy(graph, (object) parentId, (object) recordId, options);
    }
  }

  public new abstract class parentId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoTraceWithMessages.parentId>
  {
  }

  public new abstract class recordId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoTraceWithMessages.recordId>
  {
  }

  public abstract class messageText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoTraceWithMessages.messageText>
  {
  }

  public abstract class stackTrace : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoTraceWithMessages.stackTrace>
  {
  }
}
