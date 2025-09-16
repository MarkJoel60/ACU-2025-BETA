// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMUnbilledDailySummaryAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PMUnbilledDailySummaryAccum]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMUnbilledDailySummaryAccum : PMUnbilledDailySummary
{
  public new abstract class projectID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    PMUnbilledDailySummaryAccum.projectID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMUnbilledDailySummaryAccum.taskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMUnbilledDailySummaryAccum.accountGroupID>
  {
  }

  public new abstract class date : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMUnbilledDailySummaryAccum.date>
  {
  }

  public new abstract class billable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMUnbilledDailySummaryAccum.billable>
  {
  }

  public new abstract class nonBillable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMUnbilledDailySummaryAccum.nonBillable>
  {
  }
}
