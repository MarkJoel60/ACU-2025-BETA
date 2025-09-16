// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoSQLWithTables
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.SM;

[PXCacheName("SQL With Tables Performance Info")]
[PXProjection(typeof (Select2<SMPerformanceInfoSQLText, InnerJoin<SMPerformanceInfoSQL, On<SMPerformanceInfoSQL.sqlId, Equal<SMPerformanceInfoSQLText.recordId>>, LeftJoin<SMPerformanceInfoStackTrace, On<SMPerformanceInfoStackTrace.recordId, Equal<SMPerformanceInfoSQL.stackTraceId>>>>>), Persistent = false)]
public class SMPerformanceInfoSQLWithTables : SMPerformanceInfoSQL
{
  private static 
  #nullable disable
  object _locker = new object();

  [PXDBString(BqlTable = typeof (SMPerformanceInfoSQLText))]
  [PXUIField(DisplayName = "Tables")]
  public string TableList { get; set; }

  [PXDBIdentity(BqlTable = typeof (SMPerformanceInfoSQLText))]
  [PXUIField(DisplayName = "Order")]
  public int? QueryOrderId { get; set; }

  [PXDBString(BqlTable = typeof (SMPerformanceInfoSQLText))]
  [PXUIField(DisplayName = "Query Hash")]
  public string SQLHash { get; set; }

  [PXDBString(BqlTable = typeof (SMPerformanceInfoSQLText))]
  [PXUIField(DisplayName = "SQL")]
  public string SQLText { get; set; }

  [PXString]
  [PXUIField(DisplayName = "SQL")]
  [JsonIgnore]
  public string SQLWithStackTrace => (string) null;

  [PXString]
  [PXUIField(DisplayName = "Query")]
  [JsonIgnore]
  public string SQLWithParams
  {
    get => Str.IsNullOrEmpty(this.SQLParams) ? this.SQLText : $"{this.SQLParams}\n{this.SQLText}";
  }

  [PXDBString(BqlTable = typeof (SMPerformanceInfoStackTrace))]
  [PXUIField(DisplayName = "Stack Trace")]
  public string StackTrace { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Parameters")]
  [JsonIgnore]
  public string ShortParams
  {
    get
    {
      if (Str.IsNullOrEmpty(this.SQLParams))
        return string.Empty;
      lock (SMPerformanceInfoSQLWithTables._locker)
        return Regex.Replace(this.SQLParams, "((DECLARE\\s+)?(@\\w+\\s+AS\\s+[\\w,\\(,\\)]+\\s+=\\s+)+)|(SET\\s+@\\w+\\s+=\\s+)", string.Empty, RegexOptions.IgnoreCase);
    }
  }

  public new class PK : 
    PrimaryKeyOf<SMPerformanceInfoSQLWithTables>.By<SMPerformanceInfoSQLWithTables.parentId, SMPerformanceInfoSQLWithTables.recordId>
  {
    public static SMPerformanceInfoSQLWithTables Find(
      PXGraph graph,
      int? parentId,
      int? recordId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfoSQLWithTables>.By<SMPerformanceInfoSQLWithTables.parentId, SMPerformanceInfoSQLWithTables.recordId>.FindBy(graph, (object) parentId, (object) recordId, options);
    }
  }

  public abstract class tableList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLWithTables.tableList>
  {
  }

  public new abstract class parentId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoSQLWithTables.parentId>
  {
  }

  public new abstract class recordId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoSQLWithTables.recordId>
  {
  }

  public abstract class queryOrderId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoSQLWithTables.queryOrderId>
  {
  }

  public abstract class sQLHash : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQLWithTables.sQLHash>
  {
  }

  public abstract class sqlText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLWithTables.sqlText>
  {
  }

  public abstract class stackTrace : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLWithTables.stackTrace>
  {
  }
}
