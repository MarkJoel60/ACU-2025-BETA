// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPerformanceInfoTraceMessages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

[PXCacheName("Trace Messages Performance Info")]
public class SMPerformanceInfoTraceMessages : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public int? RecordId { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Message")]
  public 
  #nullable disable
  string MessageText { get; set; }

  public class PK : 
    PrimaryKeyOf<SMPerformanceInfoTraceMessages>.By<SMPerformanceInfoTraceMessages.recordId>
  {
    public static SMPerformanceInfoTraceMessages Find(
      PXGraph graph,
      int? recordId,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SMPerformanceInfoTraceMessages>.By<SMPerformanceInfoTraceMessages.recordId>.FindBy(graph, (object) recordId, options);
    }
  }

  public abstract class recordId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMPerformanceInfoTraceMessages.recordId>
  {
  }

  public abstract class messageText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPerformanceInfoTraceMessages.messageText>
  {
  }
}
