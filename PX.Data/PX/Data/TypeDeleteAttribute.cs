// Decompiled with JetBrains decompiler
// Type: PX.Data.TypeDeleteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class TypeDeleteAttribute : PXIntListAttribute
{
  public const int _Any = 0;
  public const int _Failed = 1;
  public const int _Successful = 2;

  public TypeDeleteAttribute()
    : base(new int[3]{ 0, 1, 2 }, new string[3]
    {
      "Always",
      "If All Main Processing Options Failed or Skipped",
      "If Any Main Processing Option Succeeded"
    })
  {
  }
}
