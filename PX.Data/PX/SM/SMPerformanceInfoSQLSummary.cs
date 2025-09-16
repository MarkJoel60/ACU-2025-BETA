// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoSQLSummary
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

[PXCacheName("Trace With Messages Performance Info")]
[PXHidden]
public class SMPerformanceInfoSQLSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Statement ID")]
  public int? RecordId { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Tables")]
  public 
  #nullable disable
  string TableList { get; set; }

  [PXString]
  [PXUIField(DisplayName = "SQL Text")]
  public string SQLText { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Query Hash")]
  public string QueryHash { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Total SQL Time, ms")]
  public double? TotalSQLTime { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Executions")]
  public int? TotalExecutions { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Total Rows")]
  public int? TotalRows { get; set; }

  public abstract class recordId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQLSummary.recordId>
  {
  }

  public abstract class tableList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLSummary.tableList>
  {
  }

  public abstract class sqlText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLSummary.sqlText>
  {
  }

  public abstract class queryHash : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLSummary.queryHash>
  {
  }

  public abstract class totalSQLTime : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLSummary.totalSQLTime>
  {
  }

  public abstract class totalExecutions : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLSummary.totalExecutions>
  {
  }

  public abstract class totalRows : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLSummary.totalRows>
  {
  }
}
