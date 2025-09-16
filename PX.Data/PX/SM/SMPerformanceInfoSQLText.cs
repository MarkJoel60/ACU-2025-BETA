// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoSQLText
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("SQL Text Performance Info")]
public class SMPerformanceInfoSQLText : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public int? RecordId { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "SQL")]
  public 
  #nullable disable
  string SQLText { get; set; }

  [PXDBString(8)]
  [PXUIField(DisplayName = "Query Hash")]
  public string SQLHash { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Tables")]
  public string TableList { get; set; }

  [PXDBIdentity]
  [PXUIField(DisplayName = "Query ID")]
  public int? QueryOrderId { get; set; }

  public class PK : PrimaryKeyOf<SMPerformanceInfoSQLText>.By<SMPerformanceInfoSQLText.recordId>
  {
    public static SMPerformanceInfoSQLText Find(
      PXGraph graph,
      int? recordId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfoSQLText>.By<SMPerformanceInfoSQLText.recordId>.FindBy(graph, (object) recordId, options);
    }
  }

  public abstract class recordId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoSQLText.recordId>
  {
  }

  public abstract class sqlText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfoSQLText.sqlText>
  {
  }

  public abstract class sQLHash : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPerformanceInfoSQLText.sQLHash>
  {
  }

  public abstract class tableList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoSQLText.tableList>
  {
  }

  public abstract class queryOrderId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoSQLText.queryOrderId>
  {
  }
}
