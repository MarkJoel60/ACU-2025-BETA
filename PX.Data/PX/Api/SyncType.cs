// Decompiled with JetBrains decompiler
// Type: PX.Api.SyncType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

internal static class SyncType
{
  public static readonly string[] Values = new string[3]
  {
    "F",
    "A",
    "N"
  };
  public static readonly string[] Labels = new string[3]
  {
    nameof (Full),
    "Incremental - All Records",
    "Incremental - New Only"
  };
  public const string Full = "F";
  public const string IncrementalAll = "A";
  public const string IncrementalNewOnly = "N";

  public static class UI
  {
    public const string Full = "Full";
    public const string IncrementalAll = "Incremental - All Records";
    public const string IncrementalNewOnly = "Incremental - New Only";
  }

  public class StringListAttribute : PXStringListAttribute
  {
    public StringListAttribute()
      : base(SyncType.Values, SyncType.Labels)
    {
    }
  }
}
