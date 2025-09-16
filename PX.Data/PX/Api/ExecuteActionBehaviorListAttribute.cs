// Decompiled with JetBrains decompiler
// Type: PX.Api.ExecuteActionBehaviorListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

internal class ExecuteActionBehaviorListAttribute : PXStringListAttribute
{
  public const string EachRecordValue = "E";
  public const string FirstDetailLineValue = "F";
  public const string LastDetailLineValue = "L";
  public const string EachRecordLabel = "For Each Record";
  public const string FirstDetailLineLabel = "Once, for First Detail Line";
  public const string LastDetailLineLabel = "Once, for Last Detail Line";

  public ExecuteActionBehaviorListAttribute()
    : base(new string[3]{ "E", "F", "L" }, new string[3]
    {
      "For Each Record",
      "Once, for First Detail Line",
      "Once, for Last Detail Line"
    })
  {
  }
}
