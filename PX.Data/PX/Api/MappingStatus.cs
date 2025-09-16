// Decompiled with JetBrains decompiler
// Type: PX.Api.MappingStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Api;

internal static class MappingStatus
{
  public static readonly 
  #nullable disable
  string[] Values = new string[4]{ "N", "P", "I", "F" };
  public static readonly string[] Labels = new string[4]
  {
    "New",
    nameof (Prepared),
    nameof (Processed),
    "Partially Processed"
  };
  public const string Created = "N";
  public const string Prepared = "P";
  public const string Processed = "I";
  public const string PartiallyProcessed = "F";

  public static class UI
  {
    public const string Created = "New";
    public const string Prepared = "Prepared";
    public const string Processed = "Processed";
    public const string PartiallyProcessed = "Partially Processed";
  }

  public class StringListAttribute : PXStringListAttribute
  {
    public StringListAttribute()
      : base(MappingStatus.Values, MappingStatus.Labels)
    {
    }
  }

  public class created : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MappingStatus.created>
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
  MappingStatus.prepared>
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
  MappingStatus.processed>
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
    MappingStatus.partiallyProcessed>
  {
    public partiallyProcessed()
      : base("F")
    {
    }
  }
}
