// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoSQL
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

[Serializable]
public class SMPerformanceInfoSQL : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (SMPerformanceInfo.recordId))]
  public int? ParentId { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Query ID")]
  public int? RecordId { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Start Time")]
  public double? RequestStartTime { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "SQL Time, ms")]
  public double? SqlTimeMs { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Row Count")]
  public int? NRows { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Statement ID")]
  public int? SqlId { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "TraceID")]
  public int? StackTraceId { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Params")]
  public 
  #nullable disable
  string SQLParams { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, UseTimeZone = false, DisplayMask = "g")]
  [PXUIField(DisplayName = "Start Time")]
  public System.DateTime? RequestDateTime { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "From Cache")]
  public bool? QueryCache { get; set; }

  public class PK : 
    PrimaryKeyOf<SMPerformanceInfoSQL>.By<SMPerformanceInfoSQL.parentId, SMPerformanceInfoSQL.recordId>
  {
    public static SMPerformanceInfoSQL Find(
      PXGraph graph,
      int? parentId,
      int? recordId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfoSQL>.By<SMPerformanceInfoSQL.parentId, SMPerformanceInfoSQL.recordId>.FindBy(graph, (object) parentId, (object) recordId, options);
    }
  }

  public static class FK
  {
    public class PerformanceInfo : 
      PrimaryKeyOf<SMPerformanceInfo>.By<SMPerformanceInfo.recordId>.ForeignKeyOf<SMPerformanceInfoSQL>.By<SMPerformanceInfoSQL.parentId>
    {
    }
  }

  public abstract class parentId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQL.parentId>
  {
  }

  public abstract class recordId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQL.recordId>
  {
  }

  public abstract class requestStartTime : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfoSQL.requestStartTime>
  {
  }

  public abstract class sqlTimeMs : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  SMPerformanceInfoSQL.sqlTimeMs>
  {
  }

  public abstract class nRows : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQL.nRows>
  {
  }

  public abstract class sqlId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQL.sqlId>
  {
  }

  public abstract class stackTraceId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQL.stackTraceId>
  {
  }

  public abstract class sqlParams : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfoSQL.sqlParams>
  {
  }

  public abstract class requestDateTime : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    SMPerformanceInfoSQL.requestDateTime>
  {
  }

  public abstract class queryCache : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQL.queryCache>
  {
  }
}
