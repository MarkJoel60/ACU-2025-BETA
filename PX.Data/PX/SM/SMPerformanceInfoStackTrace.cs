// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoStackTrace
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Stack Trace Performance Info")]
public class SMPerformanceInfoStackTrace : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public int? RecordId { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Stack Trace")]
  public 
  #nullable disable
  string StackTrace { get; set; }

  public class PK : 
    PrimaryKeyOf<SMPerformanceInfoStackTrace>.By<SMPerformanceInfoStackTrace.recordId>
  {
    public static SMPerformanceInfoStackTrace Find(
      PXGraph graph,
      int? recordId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfoStackTrace>.By<SMPerformanceInfoStackTrace.recordId>.FindBy(graph, (object) recordId, options);
    }
  }

  public abstract class recordId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPerformanceInfoStackTrace.recordId>
  {
  }

  public abstract class stackTrace : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoStackTrace.stackTrace>
  {
  }
}
