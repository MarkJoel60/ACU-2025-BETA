// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Performance Info")]
[Serializable]
public class SMPerformanceInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  internal const int SqlDigestMaxLength = 1024 /*0x0400*/;

  [PXDBIdentity(IsKey = true)]
  public int? RecordId { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, UseTimeZone = false, DisplayMask = "g")]
  [PXUIField(DisplayName = "Request Start Time", Enabled = false)]
  public System.DateTime? RequestStartTime { get; set; }

  [PXDBString(255 /*0xFF*/, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "URL", Enabled = false)]
  public 
  #nullable disable
  string ScreenId { get; set; }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Command Target", Enabled = false)]
  public string CommandTarget { get; set; }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Command Name", Enabled = false)]
  public string CommandName { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Server Time, ms", Enabled = false)]
  public double? RequestTimeMs { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Server CPU, ms", Enabled = false)]
  public double? RequestCpuTimeMs { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "SQL Count", Enabled = false)]
  public int? SqlCounter { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "SQL Time, ms", Enabled = false)]
  public double? SqlTimeMs { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Select Count", Enabled = false)]
  public int? SelectCounter { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Select Time, ms", Enabled = false)]
  public double? SelectTimeMs { get; set; }

  [PXDBString(20)]
  [PXUIField(DisplayName = "Username", Enabled = false)]
  public string UserId { get; set; }

  [PXDBLong]
  [PXUIField(DisplayName = "Managed Memory Bytes", Enabled = false)]
  public long? MemBefore { get; set; }

  [PXDBLong]
  [PXUIField(DisplayName = "Peak Memory Bytes", Enabled = false)]
  public long? MemDelta { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Managed Memory", Enabled = false)]
  public double? MemBeforeMb { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Peak Memory", Enabled = false)]
  public double? MemDeltaMb { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Memory Working Set", Enabled = false)]
  public double? MemoryWorkingSet { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Client Time", Enabled = false)]
  public int? ScriptTimeMs { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Session Load Time, ms", Enabled = false)]
  public double? SessionLoadTimeMs { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Session Save Time, ms", Enabled = false)]
  public double? SessionSaveTimeMs { get; set; }

  [PXDBString(512 /*0x0200*/)]
  [PXUIField(DisplayName = "Headers", Enabled = false)]
  public string Headers { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Installation ID", Enabled = false)]
  public string InstallationId { get; set; }

  [PXDBString(1024 /*0x0400*/)]
  [PXUIField(DisplayName = "SQL Digest", Enabled = false)]
  public string SqlDigest { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Tenant", Enabled = false)]
  public string TenantId { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Screen", Enabled = false)]
  public string InternalScreenId { get; set; }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Screen", Enabled = false)]
  public string UrlToScreen { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Items to Process", Enabled = false)]
  public int? ProcessingItems { get; set; }

  [PXDBString(50)]
  [RequestTypeList]
  [PXUIField(DisplayName = "Request Type", Enabled = false)]
  public string RequestType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public int? Status { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Exceptions Count", Enabled = false)]
  public int? ExceptionCounter { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Events Count", Enabled = false)]
  public int? EventCounter { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "SQL Rows", Enabled = false)]
  public int? SqlRows { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Wait Time", Enabled = false)]
  public double? WaitTime { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsChecked { get; set; }

  [PXImage(HeaderImage = "ac@pin")]
  [PXUIField(DisplayName = "Is Pinned", IsReadOnly = true)]
  public string IsPinned { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Logged SQL Count", Enabled = false)]
  public int? LoggedSqlCounter { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Logged Exceptions Count", Enabled = false)]
  public int? LoggedExceptionCounter { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Logged Events Count", Enabled = false)]
  public int? LoggedEventCounter { get; set; }

  [PXString]
  public string ID { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public class PK : PrimaryKeyOf<SMPerformanceInfo>.By<SMPerformanceInfo.recordId>
  {
    public static SMPerformanceInfo Find(PXGraph graph, int? recordId, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfo>.By<SMPerformanceInfo.recordId>.FindBy(graph, (object) recordId, options);
    }
  }

  public abstract class recordId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfo.recordId>
  {
  }

  public abstract class requestStartTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SMPerformanceInfo.requestStartTime>
  {
  }

  public abstract class screenId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.screenId>
  {
  }

  public abstract class commandTarget : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.commandTarget>
  {
  }

  public abstract class commandName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.commandName>
  {
  }

  public abstract class requestTimeMs : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfo.requestTimeMs>
  {
  }

  public abstract class requestCpuTimeMs : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfo.requestCpuTimeMs>
  {
  }

  public abstract class sqlCounter : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfo.sqlCounter>
  {
  }

  public abstract class sqlTimeMs : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  SMPerformanceInfo.sqlTimeMs>
  {
  }

  public abstract class selectCounter : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfo.selectCounter>
  {
  }

  public abstract class selectTimeMs : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfo.selectTimeMs>
  {
  }

  public abstract class userId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.userId>
  {
  }

  public abstract class memBefore : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SMPerformanceInfo.memBefore>
  {
  }

  public abstract class memDelta : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SMPerformanceInfo.memDelta>
  {
  }

  public abstract class memBeforeMb : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SMPerformanceInfo.memBeforeMb>
  {
  }

  public abstract class memDeltaMb : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SMPerformanceInfo.memDeltaMb>
  {
  }

  public abstract class memoryWorkingSet : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SMPerformanceInfo.memoryWorkingSet>
  {
  }

  public abstract class scriptTimeMs : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfo.scriptTimeMs>
  {
  }

  public abstract class sessionLoadTimeMs : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfo.sessionLoadTimeMs>
  {
  }

  public abstract class sessionSaveTimeMs : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfo.sessionSaveTimeMs>
  {
  }

  public abstract class headers : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.headers>
  {
  }

  public abstract class installationId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.installationId>
  {
  }

  public abstract class sqlDigest : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.sqlDigest>
  {
  }

  public abstract class tenantId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.tenantId>
  {
  }

  public abstract class internalScreenId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.internalScreenId>
  {
  }

  public abstract class urlToScreen : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.urlToScreen>
  {
  }

  public abstract class processingItems : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.processingItems>
  {
  }

  public abstract class requestType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.requestType>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.status>
  {
  }

  public abstract class exceptionCounter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.exceptionCounter>
  {
  }

  public abstract class eventCounter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.eventCounter>
  {
  }

  public abstract class sqlRows : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.sqlRows>
  {
  }

  public abstract class waitTime : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.waitTime>
  {
  }

  public abstract class isChecked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMPerformanceInfo.isChecked>
  {
  }

  public abstract class loggedSqlCounter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfo.loggedSqlCounter>
  {
  }

  public abstract class loggedExceptionCounter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.loggedExceptionCounter>
  {
  }

  public abstract class loggedEventCounter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfo.loggedEventCounter>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfo.id>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPerformanceInfo.noteID>
  {
  }
}
