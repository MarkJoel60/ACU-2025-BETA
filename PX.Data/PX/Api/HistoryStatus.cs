// Decompiled with JetBrains decompiler
// Type: PX.Api.HistoryStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Api;

internal static class HistoryStatus
{
  public static readonly 
  #nullable disable
  string[] Values = new string[6]
  {
    "N",
    "P",
    "I",
    "F",
    "E",
    "X"
  };
  public static readonly string[] Labels = new string[6]
  {
    "New",
    nameof (Prepared),
    nameof (Processed),
    "Partially Processed",
    nameof (Processed),
    "Partially Processed"
  };
  public const string Created = "N";
  public const string Prepared = "P";
  public const string Processed = "I";
  public const string PartiallyProcessed = "F";
  public const string ProcessedExp = "E";
  public const string PartiallyProcessedExp = "X";

  public static class UI
  {
    public const string Created = "New";
    public const string Prepared = "Prepared";
    public const string Processed = "Processed";
    public const string PartiallyProcessed = "Partially Processed";
    public const string ProcessedExp = "Processed";
    public const string PartiallyProcessedExp = "Partially Processed";
  }

  public class StringListAttribute : PXStringListAttribute
  {
    public StringListAttribute()
      : base(HistoryStatus.Values, HistoryStatus.Labels)
    {
    }
  }

  public class created : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  HistoryStatus.created>
  {
    public created()
      : base("N")
    {
    }
  }

  public class prepared : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  HistoryStatus.prepared>
  {
    public prepared()
      : base("P")
    {
    }
  }

  public class processed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  HistoryStatus.processed>
  {
    public processed()
      : base("I")
    {
    }
  }

  public class partiallyProcessed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    HistoryStatus.partiallyProcessed>
  {
    public partiallyProcessed()
      : base("F")
    {
    }
  }

  public class processedExp : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  HistoryStatus.processedExp>
  {
    public processedExp()
      : base("E")
    {
    }
  }

  public class partiallyProcessedExp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    HistoryStatus.partiallyProcessedExp>
  {
    public partiallyProcessedExp()
      : base("X")
    {
    }
  }
}
