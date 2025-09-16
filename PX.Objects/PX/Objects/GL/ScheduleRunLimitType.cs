// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ScheduleRunLimitType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL;

public class ScheduleRunLimitType : ILabelProvider
{
  public const 
  #nullable disable
  string StopOnExecutionDate = "D";
  public const string StopAfterNumberOfExecutions = "M";
  [Obsolete("This constant has been renamed to StopOnExecutionDate and will be removed in Acumatica 8.0")]
  public const string RunTillDate = "D";
  [Obsolete("This constant has been renamed to StopAfterNumberOfExecutions and will be removed in Acumatica 8.0")]
  public const string RunMultipleTimes = "M";

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get
    {
      return (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "D",
          "Stop on Execution Date"
        },
        {
          "M",
          "Stop After Number of Executions"
        }
      };
    }
  }

  public class stopOnExecutionDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ScheduleRunLimitType.stopOnExecutionDate>
  {
    public stopOnExecutionDate()
      : base("D")
    {
    }
  }

  public class stopAfterNumberOfExecutions : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ScheduleRunLimitType.stopAfterNumberOfExecutions>
  {
    public stopAfterNumberOfExecutions()
      : base("M")
    {
    }
  }

  [Obsolete("This constant has been renamed to stopOnExecutionDateand will be removed in Acumatica 8.0")]
  public class runTillDate : ScheduleRunLimitType.stopOnExecutionDate
  {
  }

  [Obsolete("This constant has been renamed to stopAfterNumberOfExecutions and will be removed in Acumatica 8.0")]
  public class runMultipleTimes : ScheduleRunLimitType.stopAfterNumberOfExecutions
  {
  }
}
